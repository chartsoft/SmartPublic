using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Smart.Net45.Enum;

namespace Smart.Net45.ExpressionVisitor
{
    /// <summary>
    /// Sql语句
    /// </summary>
    public class ExpressionSqlWriter : ExpressionVisitor
    {
        private StringBuilder _mSb;
        private ExpressionType _mNodeType;
        private Expression _mLeft;
        private readonly DataBaseType _dataBaseType;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBaseType"></param>
        public ExpressionSqlWriter(DataBaseType dataBaseType=DataBaseType.MsSql)
        {
            _dataBaseType = dataBaseType;
            _mSb = new StringBuilder();
            _mNodeType = 0;
            _mLeft = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string Translate(Expression expression)
        {
            expression = Evaluator.PartialEval(expression);
            base.Visit(expression);
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

            Expression left = b.Left;

            base.Visit(left);

            _mSb.Append(ExpressionHelper.GetOperator(b.NodeType));

            _mNodeType = b.NodeType;

            Expression right = b.Right;

            base.Visit(right);

            _mSb.Append(")");

            _mNodeType = 0;

            return b;
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
        /// <param name="m"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (m.Member.DeclaringType == typeof(string))
            {
                switch (m.Member.Name)
                {
                    case "Length":
                        _mSb.Append("LEN(");
                        base.Visit(m.Expression);
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
                        base.Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                    case "Month":
                        _mSb.Append("MONTH(");
                        base.Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                    case "Day":
                        _mSb.Append("DAY(");
                        base.Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                    case "Hour":
                        _mSb.Append("DATEPART(HOUR, ");
                        base.Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                    case "Minute":
                        _mSb.Append("DATEPART(MINUTE, ");
                        base.Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                    case "Second":
                        _mSb.Append("DATEPART(SECOND, ");
                        base.Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                    case "Millisecond":
                        _mSb.Append("DATEPART(MILLISECOND, ");
                        base.Visit(m.Expression);
                        _mSb.Append(")");
                        break;
                    case "DayOfWeek":
                        _mSb.Append("(DATEPART(WEEKDAY, ");
                        base.Visit(m.Expression);
                        _mSb.Append(") - 1)");
                        break;
                    case "DayOfYear":
                        _mSb.Append("(DATEPART(DAYOFYEAR, ");
                        base.Visit(m.Expression);
                        _mSb.Append(") - 1)");
                        break;
                }
            }
            else
            {
                //
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
                _mSb.AppendFormat("{0}", m.Member.Name);
                _mLeft = m;
            }
            return m;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression c)
        {
            object value = c.Value;
            if (value == null)
            {
                if (_mNodeType == ExpressionType.Equal)
                {
                    _mSb.Remove(_mSb.Length - 3, 3);//移除" = "
                    _mSb.Append(" IS NULL ");
                }
                else if (_mNodeType == ExpressionType.NotEqual)
                {
                    _mSb.Remove(_mSb.Length - 3, 3);
                    _mSb.Append(" IS NOT NULL ");
                }
                else
                {
                    _mSb.Append(" NULL ");
                }
            }
            else if (value is Guid) //guid
            {
                _mSb.AppendFormat("'{0}'", c.Value);
            }
            else if (value is IEnumerable && !(value is string))
            {
                foreach (var item in (IEnumerable)value)
                {
                    Expression constant = Expression.Constant(item);
                    base.Visit(constant);
                    _mSb.Append(",");
                }
                _mSb.Remove(_mSb.Length - 1, 1);//移除末尾,(逗号)
            }
            else if (_mLeft == null && value is bool)//p=>true
            {
                _mSb.AppendFormat("({0})", ((bool)value) ? "1 = 1" : "1 = 0");
            }
            else
            {
                switch (Type.GetTypeCode(value.GetType()))
                {
                    case TypeCode.Boolean:
                        _mSb.Append(((bool)c.Value) ? "1" : "0");
                        break;
                    case TypeCode.DateTime:
                        _mSb.AppendFormat("'{0}'", ((DateTime)c.Value).ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;
                    case TypeCode.String:
                    case TypeCode.Char:                       
                        //_mSb.AppendFormat("{0}", c.Value);
                       _mSb.AppendFormat("'{0}'", c.Value);
                        break;
                    default:
                        _mSb.Append(c.Value);
                        break;
                }
            }

            _mLeft = null;//访问过右值清空左值引用
            return c;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    _mSb.Append(" NOT ");
                    this.Visit(u.Operand);
                    break;
                default:
                    throw new NotSupportedException($"不支持的NodeType: {u.NodeType}");

            }
            return u;
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="m"></param>
       /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.Name == "Equals")
            {
                base.Visit(m.Object);
                _mSb.Append(" = ");
                base.Visit(m.Arguments[0]);
                return m;
            }
            else if (m.Method.Name == "ToString")
            {
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
            }
            else if (m.Method.DeclaringType == typeof(string))
            {
                switch (m.Method.Name)
                {
                    case "StartsWith":
                        _mSb.Append("(");
                        base.Visit(m.Object);
                        _mSb.Append(" LIKE ");
                        base.Visit(m.Arguments[0]);
                        _mSb.Append("%')");
                        return m;
                    case "EndsWith":
                        _mSb.Append("(");
                        base.Visit(m.Object);
                        _mSb.Append(" LIKE '%");
                        base.Visit(m.Arguments[0]);
                        _mSb.Append(")");
                        return m;
                    case "Contains":
                        _mSb.Append("(");
                        base.Visit(m.Object);
                        _mSb.Append(" LIKE '%");
                        VistSql(m.Arguments[0]);
                        //base.Visit(m.Arguments[0]);                       
                        _mSb.Append("%')");
                        //var s= m.Arguments[0].ToString();
                        
                       // _mSb.Replace($"{m.Arguments[0]}", m.Arguments[0].ToString().Replace("'",""));
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
                switch (m.Method.Name)
                {
                    case "Contains":
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
                        base.Visit(m.Arguments[0]);
                        _mSb.Append(",");
                        base.Visit(m.Object);
                        _mSb.Append(")");
                        return m;
                    case "AddMonths":
                        _mSb.Append(" DATEADD(MM,");
                        base.Visit(m.Arguments[0]);
                        _mSb.Append(",");
                        base.Visit(m.Object);
                        _mSb.Append(")");
                        return m;
                    case "AddDays":
                        _mSb.Append(" DATEADD(DD,");
                        base.Visit(m.Arguments[0]);
                        _mSb.Append(",");
                        base.Visit(m.Object);
                        _mSb.Append(")");
                        return m;
                    case "AddHours":
                        _mSb.Append(" DATEADD(HH,");
                        base.Visit(m.Arguments[0]);
                        _mSb.Append(",");
                        base.Visit(m.Object);
                        _mSb.Append(")");
                        return m;
                    case "AddMinutes":
                        _mSb.Append(" DATEADD(MI,");
                        base.Visit(m.Arguments[0]);
                        _mSb.Append(",");
                        base.Visit(m.Object);
                        _mSb.Append(")");
                        return m;
                    case "AddSeconds":
                        _mSb.Append(" DATEADD(SS,");
                        base.Visit(m.Arguments[0]);
                        _mSb.Append(",");
                        base.Visit(m.Object);
                        _mSb.Append(")");
                        return m;
                    case "AddMilliseconds":
                        _mSb.Append(" DATEADD(MS,");
                        base.Visit(m.Arguments[0]);
                        _mSb.Append(",");
                        base.Visit(m.Object);
                        _mSb.Append(")");
                        return m;
                }
            }
            throw new NotSupportedException($"不支持的方法: {m.Method.DeclaringType}.{m.Method.Name}");
        }
    }
}