using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Smart.Standard.Enum;
using Smart.Standard.Extends;

namespace Smart.Standard.ExpressionVisitor
{
    /// <summary>
    /// 参数化的sql语句
    /// </summary>
    public class ExpressionParameterizedSqlWriter : ExpressionVisitor
    {
        private readonly StringBuilder _mSb;
        /// <summary>
        /// 
        /// </summary>
        public ExpressionType MNodeType;
        /// <summary>
        /// 
        /// </summary>
        public Expression MLeft;
        /// <summary>
        /// 
        /// </summary>
        public readonly List<object> MParameters;
        private readonly DataBaseType _dataBaseType ;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="dataBaseType"></param>
        public ExpressionParameterizedSqlWriter(List<object> parameters, DataBaseType dataBaseType=DataBaseType.MsSql)
        {
            _dataBaseType = dataBaseType;
            _mSb = new StringBuilder();
            MNodeType = 0;
            MLeft = null;
            if (parameters == null)
            {
                parameters = new List<object>();
            }
            MParameters = parameters;
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string Translate(Expression expression)
        {
            expression = Evaluator.PartialEval(expression);
            Visit(expression);
            return _mSb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        protected override Expression VisitBinary(BinaryExpression b)
        {
            _mSb.Append("(");

            var left = b.Left;

            Visit(left);

            _mSb.Append(ExpressionHelper.GetOperator(b.NodeType));

            MNodeType = b.NodeType;

            var right = b.Right;

            Visit(right);

            _mSb.Append(")");

            MNodeType = 0;

            return b;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (m.Member.DeclaringType == typeof(string))
            {
                switch (m.Member.Name)
                {
                    case "Length":
                        _mSb.Append("LEN(");
                        Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                }
            }
            else if (m.Member.DeclaringType == typeof(DateTime) || m.Member.DeclaringType == typeof(DateTimeOffset))
            {
                switch (m.Member.Name)
                {
                    case "Year":
                        _mSb.Append("YEAR(");
                        Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                    case "Month":
                        _mSb.Append("MONTH(");
                        Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                    case "Day":
                        _mSb.Append("DAY(");
                        Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                    case "Hour":
                        _mSb.Append("DATEPART(HOUR, ");
                        Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                    case "Minute":
                        _mSb.Append("DATEPART(MINUTE, ");
                        Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                    case "Second":
                        _mSb.Append("DATEPART(SECOND, ");
                        Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                    case "Millisecond":
                        _mSb.Append("DATEPART(MILLISECOND, ");
                        Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                    case "DayOfWeek":
                        _mSb.Append("(DATEPART(WEEKDAY, ");
                        Visit(m.Expression);
                        _mSb.Append(") - 1)");
                        break;
                    case "DayOfYear":
                        _mSb.Append("(DATEPART(DAYOFYEAR, ");
                        Visit(m.Expression);
                        _mSb.Append(") - 1)");
                        break;
                }
            }
            else
            {
                switch (_dataBaseType)
                {
                    case DataBaseType.MsSql:
                        _mSb.AppendFormat("{0}", m.Member.Name);
                        break;
                    case DataBaseType.PostgreSql:
                        _mSb.AppendFormat("\"{0}\"", m.Member.Name);
                        break;
                    default:
                        throw new ArgumentNullException("未发现sql类型");
                }
                MLeft = m;
            }
            return m;
        }
        /// <summary>
        /// 
        /// </summary>
        protected override Expression VisitConstant(ConstantExpression c)
        {
            var value = c.Value;
            switch (value)
            {
                case null:
                    if (MNodeType == ExpressionType.Equal)
                    {
                        _mSb.Remove(_mSb.Length - 3, 3);//移除" = "
                        _mSb.Append(" IS NULL ");
                    }
                    else if (MNodeType == ExpressionType.NotEqual)
                    {
                        _mSb.Remove(_mSb.Length - 3, 3);
                        _mSb.Append(" IS NOT NULL ");
                    }
                    else
                    {
                        _mSb.Append(" NULL ");
                    }
                    break;
                case IEnumerable _ when !(value is string):
                    foreach (var item in (IEnumerable)value)
                    {
                        Expression constant = Expression.Constant(item);
                        Visit(constant);
                        _mSb.Append(",");
                    }
                    _mSb.Remove(_mSb.Length - 1, 1);//移除末尾,(逗号)
                    break;
                default:
                    if (MLeft == null && value is bool)//p=>true
                    {
                        _mSb.AppendFormat("({0})", ((bool)value) ? "1 = 1" : "1 = 0");
                    }
                    else
                    {
                        _mSb.Append($"@p{MParameters.Count}");
                        MParameters.Add(c.Value);
                    }
                    break;
            }

            MLeft = null;//访问过右值清空左值引用
            return c;
        }
        /// <summary>
        /// 
        /// </summary>
        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    _mSb.Append(" NOT ");
                    Visit(u.Operand);
                    break;
                default:
                    throw new NotSupportedException($"不支持的NodeType: {u.NodeType}");

            }
            return u;
        }
        private void VistSql(Expression sb)
        {
            var res = sb.ToString().Substring(1);
            res = res.Substring(0, res.Length - 1);

            _mSb.Append(res);
            //return res;
        }
        /// <summary>
        /// 
        /// </summary>
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            switch (m.Method.Name)
            {
                case "Equals":
                    base.Visit(m.Object);
                    _mSb.Append(" = ");
                    base.Visit(m.Arguments[0]);
                    return m;
                case "ToString":
                    if (m.Object.Type != typeof(string))
                    {
                        _mSb.Append(" CAST(");
                        base.Visit(m.Object);
                        _mSb.Append(" AS NVARCHAR)");
                    }
                    else
                    {
                        base.Visit(m.Object);
                    }
                    return m;
                default:
                    if (m.Method.DeclaringType == typeof(string))
                    {
                        switch (m.Method.Name)
                        {
                            case "StartsWith":
                                _mSb.Append("(");
                                base.Visit(m.Object);
                                _mSb.Append(" LIKE ");
                                base.Visit(m.Arguments[0]);
                                //'%' + @p4 + '%'
                                //_mSb.Append(" + '%')");
                                _mSb.Append("%')");
                                return m;
                            case "EndsWith":
                                _mSb.Append("(");
                                base.Visit(m.Object);
                                // _mSb.Append(" LIKE '%' + ");
                                _mSb.Append(" LIKE '%");
                                base.Visit(m.Arguments[0]);
                                _mSb.Append(")");
                                return m;
                            case "Contains":
                                _mSb.Append("(");
                                base.Visit(m.Object);
                                //dapper的写法
                                //_mSb.Append(" LIKE '%' + ");
                                _mSb.Append(" LIKE ");
                                //VistSql(m.Arguments[0]);  

                                base.Visit(m.Arguments[0]);
                                //_mSb.Append(" + '%')");
                                _mSb.Append(")");
                                return m;
                            case "IsNullOrEmpty":
                                _mSb.Append("(");
                                base.Visit(m.Arguments[0]);
                                _mSb.Append(" IS NULL OR ");
                                base.Visit(m.Arguments[0]);
                                _mSb.Append(" = '' )");
                                return m;
                            case "ToUpper":
                                _mSb.Append("UPPER(");
                                base.Visit(m.Object);
                                _mSb.Append(")");
                                return m;
                            case "ToLower":
                                _mSb.Append("LOWER(");
                                base.Visit(m.Object);
                                _mSb.Append(")");
                                return m;
                            case "Trim":
                                _mSb.Append("LTRIM(RTRIM(");
                                base.Visit(m.Object);
                                _mSb.Append("))");
                                return m;
                            case "TrimStart":
                                _mSb.Append("LTRIM(");
                                base.Visit(m.Object);
                                _mSb.Append(")");
                                return m;
                            case "TrimEnd":
                                _mSb.Append("RTRIM(");
                                base.Visit(m.Object);
                                _mSb.Append(")");
                                return m;
                        }
                    }
                    else if (m.Method.DeclaringType == typeof(Enumerable))//Linq Contains
                    {
                        switch (m.Method.Name)
                        {
                            case "Contains":
                                _mSb.Append("(");
                                base.Visit(m.Arguments[1]);
                                _mSb.Append(" IN (");
                                base.Visit(m.Arguments[0]);
                                _mSb.Append("))");
                                return m;
                        }
                    }
                    else if (typeof(IList).IsAssignableFrom(m.Method.DeclaringType))//List Contains
                    {
                        if (m.Method.Name == "Contains")
                        {
                            _mSb.Append("(");
                            base.Visit(m.Arguments[0]);
                            _mSb.Append(" IN (");
                            base.Visit(m.Object);
                            _mSb.Append("))");
                            return m;
                        }
                    }
                    else if (m.Method.DeclaringType == typeof(DateTime))
                    {
                        switch (m.Method.Name)
                        {
                            case "AddYears":
                                _mSb.Append(" DATEADD(YYYY,");
                                Visit(m.Arguments[0]);
                                _mSb.Append(",");
                                Visit(m.Object);
                                _mSb.Append(")");
                                return m;
                            case "AddMonths":
                                _mSb.Append(" DATEADD(MM,");
                                Visit(m.Arguments[0]);
                                _mSb.Append(",");
                                Visit(m.Object);
                                _mSb.Append(")");
                                return m;
                            case "AddDays":
                                _mSb.Append(" DATEADD(DD,");
                                Visit(m.Arguments[0]);
                                _mSb.Append(",");
                                Visit(m.Object);
                                _mSb.Append(")");
                                return m;
                            case "AddHours":
                                _mSb.Append(" DATEADD(HH,");
                                Visit(m.Arguments[0]);
                                _mSb.Append(",");
                                Visit(m.Object);
                                _mSb.Append(")");
                                return m;
                            case "AddMinutes":
                                _mSb.Append(" DATEADD(MI,");
                                Visit(m.Arguments[0]);
                                _mSb.Append(",");
                                Visit(m.Object);
                                _mSb.Append(")");
                                return m;
                            case "AddSeconds":
                                _mSb.Append(" DATEADD(SS,");
                                Visit(m.Arguments[0]);
                                _mSb.Append(",");
                                Visit(m.Object);
                                _mSb.Append(")");
                                return m;
                            case "AddMilliseconds":
                                _mSb.Append(" DATEADD(MS,");
                                Visit(m.Arguments[0]);
                                _mSb.Append(",");
                                Visit(m.Object);
                                _mSb.Append(")");
                                return m;
                        }
                    }
                    else if(m.Method.DeclaringType==typeof(System.Enum))
                    {                  
                        if (m.Method.Name == "HasFlag")
                        {
                            //Visit(m.Arguments[0]);
                            //_mSb.Append(",");
                            //Visit(m.Object);
                            _mSb.Append("(");
                            Visit(m.Object);
                            _mSb.Append("&");
                            Visit(m.Arguments[0]);                                                   
                            _mSb.Append("=");
                            Visit(m.Arguments[0]);
                            _mSb.Append(")");
                            return m;

                        }
                    }
                    break;
            }
            throw new NotSupportedException($"不支持的方法: {m.Method.DeclaringType}.{m.Method.Name}");
        }
    }
}
