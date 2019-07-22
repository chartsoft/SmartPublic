using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Smart.Standard.ExpressionVisitor
{
    /// <summary>
    /// Evaluator�๫����һ����̬������PartialEval���������Ե��ø÷������������ʽ�е���Щ������ֻ���¾���ʵ��ֵ�ĳ����ڵ㡣 
    /// ��������еĴ󲿷�����������Ļ��֣����Ը���������ʵ�ʵ�������΢������ģ���Ϊ��������ʹ��LambaExpression.Compile�����롱�����һ��ί�У�Ȼ�󱻵��á�
    /// ��������SubtreeVisitor.Evaluate�����п�����һ�㡣
    /// ȷ����������Ĺ��̷��������С�����ͨ��Nominator���е����¶��ϵĲ��裬ȷ����Щ�ڵ���ܱ�������������Ȼ����SubtreeEvaluator�����϶��µĲ��裬�ҵ���ʾ��������������߽ڵ㡣
    /// �����������ṩ�ĺ������в����������Բ�������Ҫȷ��ĳЩ�����ڵ��Ƿ���Ը����������κ�����ʽ������Ĭ������ʽ�ǳ���ExpresssionType.Parameter֮����κνڵ㶼���Ը�����ֵ�� 
    /// ����֮�⣬һ�����ָ��������ӽڵ��޷��ڱ��ؼ��㣬�򸸽ڵ�Ҳ���ܡ���ˣ��޷������������ε��κνڵ㣬�����������С�һ�ж������������滻Ϊ������ 
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
        /// ��������
        /// </summary>
        private class SubtreeEvaluator : ExpressionVisitor
        {
            readonly HashSet<Expression> _mCandidates;

            public SubtreeEvaluator(HashSet<Expression> candidates)
            {
                _mCandidates = candidates;
            }

            /// <summary>
            /// ��������ֵ
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
        /// ָ����ָ����ѡ��
        /// </summary>
        class Nominator : ExpressionVisitor
        {
            Func<Expression, bool> m_fnCanBeEvaluated;
            HashSet<Expression> m_candidates;//��ѡ��
            bool m_cannotBeEvaluated;//true ��������

            public Nominator(Func<Expression, bool> fnCanBeEvaluated)
            {
                m_fnCanBeEvaluated = fnCanBeEvaluated;
            }

            /// <summary>
            /// ָ����ѡ��
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
                    //��εݹ��е��ƣ�ע��saveCannotBeEvaluated��m_cannotBeEvaluated������ֵ�ı仯
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