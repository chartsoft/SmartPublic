using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Smart.Standard.ExpressionVisitor
{
    /// <summary>
    /// Evaluator类公开了一个静态方法“PartialEval”，您可以调用该方法来评估表达式中的这些子树，只留下具有实际值的常量节点。 
    /// 这个代码中的大部分是最大子树的划分，可以隔离评估。实际的评估是微不足道的，因为子树可以使用LambaExpression.Compile“编译”，变成一个委托，然后被调用。
    /// 您可以在SubtreeVisitor.Evaluate方法中看到这一点。
    /// 确定最大子树的过程分两步进行。首先通过Nominator类中的自下而上的步骤，确定哪些节点可能被孤立地评估，然后在SubtreeEvaluator中自上而下的步骤，找到表示提名的子树的最高节点。
    /// 提名者由您提供的函数进行参数化，可以采用您想要确定某些给定节点是否可以隔离评估的任何启发式方法。默认启发式是除了ExpresssionType.Parameter之外的任何节点都可以隔离求值。 
    /// 除此之外，一般规则指出，如果子节点无法在本地计算，则父节点也不能。因此，无法评估参数上游的任何节点，并保留在树中。一切都将被评估并替换为常量。 
    /// </summary>
    internal static class Evaluator
    {
        public static Expression PartialEval(Expression expression)
        {
            return PartialEval(expression, CanBeEvaluatedLocally);
        }


        private static bool CanBeEvaluatedLocally(Expression expression)
        {
            return expression.NodeType != ExpressionType.Parameter
                && expression.NodeType != ExpressionType.Lambda;
        }

        public static Expression PartialEval(Expression expression, Func<Expression, bool> fnCanBeEvaluated)
        {
            HashSet<Expression> candidates = new Nominator(fnCanBeEvaluated).Nominate(expression);
            return new SubtreeEvaluator(candidates).Eval(expression);
        }

        /// <summary>
        /// 子树评估
        /// </summary>
        private class SubtreeEvaluator : ExpressionVisitor
        {
            readonly HashSet<Expression> _mCandidates;

            public SubtreeEvaluator(HashSet<Expression> candidates)
            {
                _mCandidates = candidates;
            }

            /// <summary>
            /// 绑定子树的值
            /// </summary>
            /// <param name="exp"></param>
            /// <returns></returns>
            public Expression Eval(Expression exp)
            {
                return Visit(exp);
            }

            protected override Expression Visit(Expression exp)
            {
                if (exp == null)
                {
                    return null;
                }
                if (_mCandidates.Contains(exp))
                {
                    return this.Evaluate(exp);
                }
                return base.Visit(exp);
            }

            private Expression Evaluate(Expression exp)
            {
                if (exp.NodeType == ExpressionType.Constant)
                {
                    return exp;
                }
                LambdaExpression lambda = Expression.Lambda(exp);
                Delegate fn = lambda.Compile();
                return Expression.Constant(fn.DynamicInvoke(null), exp.Type);
            }
        }

        /// <summary>
        /// 指定者指定候选人
        /// </summary>
        class Nominator : ExpressionVisitor
        {
            Func<Expression, bool> m_fnCanBeEvaluated;
            HashSet<Expression> m_candidates;//候选人
            bool m_cannotBeEvaluated;//true 不可评估

            public Nominator(Func<Expression, bool> fnCanBeEvaluated)
            {
                m_fnCanBeEvaluated = fnCanBeEvaluated;
            }

            /// <summary>
            /// 指定候选人
            /// </summary>
            /// <param name="expression"></param>
            /// <returns></returns>
            public HashSet<Expression> Nominate(Expression expression)
            {
                m_candidates = new HashSet<Expression>();
                this.Visit(expression);
                return m_candidates;
            }

            protected override Expression Visit(Expression exp)
            {
                if (exp != null)
                {
                    //这段递归有点绕，注意saveCannotBeEvaluated，m_cannotBeEvaluated这两个值的变化
                    bool saveCannotBeEvaluated = m_cannotBeEvaluated;
                    m_cannotBeEvaluated = false;
                    base.Visit(exp);
                    if (!m_cannotBeEvaluated)
                    {
                        if (m_fnCanBeEvaluated(exp))
                        {
                            m_candidates.Add(exp);
                        }
                        else
                        {
                            m_cannotBeEvaluated = true;
                        }
                    }
                    m_cannotBeEvaluated |= saveCannotBeEvaluated;
                }
                return exp;
            }
        }
    }
}