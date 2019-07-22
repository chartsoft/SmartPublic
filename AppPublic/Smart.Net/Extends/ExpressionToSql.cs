using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using Smart.Net45.Enum;
using Smart.Net45.ExpressionVisitor;

namespace Smart.Net45.Extends
{
    /// <summary>
    /// lambda表达式转sql语句
    /// </summary>
    public static class ExpressionToSql
    {
        /// <summary>
        /// 将lambda表达式转换成带参数的dynamicParameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="dynamicParameters"></param>
        /// <param name="dataBaseType"></param>
        /// <returns></returns>
        public static string ToSql<T>(this Expression<Func<T, bool>> condition, out DynamicParameters dynamicParameters,DataBaseType dataBaseType=DataBaseType.MsSql)
        {
            var parameters = new List<object>();
            var dyparams = new DynamicParameters();
            var writer = new ExpressionParameterizedSqlWriter(parameters,dataBaseType);
            var sql = writer.Translate(condition);
            for (var i = 0; i < parameters.Count; i++)
            {
                dyparams.Add($"p{i}", sql.Contains($"LIKE @p{i}") ? $"%{parameters[i]}%" : parameters[i]);
            }
            dynamicParameters = dyparams;
            return sql;
        }
        /// <summary>
        /// 将lambda表达式转换成sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="dataBaseType"></param>
        /// <returns></returns>
        public static string ToSql<T>(this Expression<Func<T, bool>> condition, DataBaseType dataBaseType = DataBaseType.MsSql)
        {
            var writer = new ExpressionSqlWriter(dataBaseType);
            return writer.Translate(condition);
        }
        /// <summary>
        /// 将lambda表达式转换成bject[]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="objects"></param>
        /// <param name="dataBaseType"></param>
        /// <returns></returns>
        public static string ToSql<T>(this Expression<Func<T, bool>> condition, out object[] objects, DataBaseType dataBaseType = DataBaseType.MsSql)
        {
            var parameters = new List<object>();
            var writer = new ExpressionParameterizedSqlWriter(parameters,dataBaseType);
            var sql = writer.Translate(condition);
            objects = parameters.ToArray();
            return sql;
        }

        /// <summary>
        /// 获取属性名称
        /// </summary>
        public static string GetPropertyName<T>(this Expression<Func<T, object>> expr)
        {
            switch (expr.Body)
            {
                case MemberExpression memberExpression:
                    return memberExpression.Member.Name;
                case UnaryExpression expression:
                    return ((MemberExpression)expression.Operand).Member.Name;
                case ParameterExpression parameterExpression:
                    return parameterExpression.Type.Name;
            }
            return string.Empty;
        }
        /// <summary>
        /// 获取属性名称
        /// </summary>
        public static IEnumerable<string> GetPropertyName<T>(this Expression<Func<T, object>>[] expr)
        {
            return expr.Select(GetPropertyName).ToList();
        }
    }
}
