using System;
using System.Linq.Expressions;

namespace Smart.Net45.ExpressionVisitor
{
    internal static class ExpressionHelper
    {
        /// <summary>
        /// 获取Expression NodeType表示的操作符
        /// </summary>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        public static string GetOperator(ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.And:
                    return "&";
                case ExpressionType.AndAlso:
                    return " AND ";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return " OR ";
                case ExpressionType.Equal:
                    return " = ";
                case ExpressionType.NotEqual:
                    return " <> ";
                case ExpressionType.GreaterThan:
                    return " > ";
                case ExpressionType.GreaterThanOrEqual:
                    return " >= ";
                case ExpressionType.LessThan:
                    return " < ";
                case ExpressionType.LessThanOrEqual:
                    return " <= ";
            }
            throw new NotSupportedException($"不支持的NodeType: {nodeType}");
        }

    }
}
