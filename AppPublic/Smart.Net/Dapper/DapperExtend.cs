using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Smart.Net45.Attribute;
using Smart.Net45.Enum;
using Smart.Net45.Extends;
using Smart.Net45.FastReflection;
using Smart.Net45.Model;

namespace Smart.Net45.Dapper
{
    /// <summary>
    /// Dapper扩展类，支持简单的lambda表达式
    /// </summary>
    public static class DapperExtend
    {
        #region [Query]
        /// <summary>
        /// 根据lambda表达式查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="expression">lambda表达式</param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction">事务</param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(this IDbConnection db, Expression<Func<T, bool>> expression, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, bool buffered = true, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            string clounms;
            var ignoreQuerys = arr.IgnoreQuery.Split(',').ToList();
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    clounms = typeof(T).GetPropertiesCache().Select(c => c.Name).Where(c => !ignoreQuerys.Contains(c)).Join();
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    clounms = typeof(T).GetPropertiesCache().Select(c => c.Name.AddDoubleQuotes()).Where(c => !ignoreQuerys.Contains(c))
                        .Join();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sb = new StringBuilder();
            sb.Append($"SELECT {clounms} FROM {tableName} WHERE ");
            sb.Append(expression.ToSql(out DynamicParameters dynamicParameters, dataBaseType));
            return db.Query<T>(sb.ToString(), dynamicParameters, dbTransaction, buffered, commandTimeout);
        }
        /// <summary>
        /// 根据lambda表达式查询异步执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="expression"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection db, Expression<Func<T, bool>> expression, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            string clounms;
            var ignoreQuerys= arr.IgnoreQuery.Split(',').ToList(); 
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    clounms=typeof(T).GetPropertiesCache().Select(c => c.Name).Where(c => !ignoreQuerys.Contains(c)).Join();
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    clounms = typeof(T).GetPropertiesCache().Select(c => c.Name.AddDoubleQuotes()).Where(c => !ignoreQuerys.Contains(c))
                        .Join();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sb = new StringBuilder();           
            sb.Append($"SELECT {clounms} FROM {tableName} WHERE ");
            sb.Append(expression.ToSql(out DynamicParameters dynamicParameters, dataBaseType));
            return db.QueryAsync<T>(sb.ToString(), dynamicParameters, dbTransaction, commandTimeout);

        }
        #endregion

        #region [Insert]
        /// <summary>
        /// 根据模型插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static object Insert<T>(this IDbConnection db, T model, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            switch (dataBaseType)
            {
                case DataBaseType.PostgreSql:
                    return InsertPgSql(db, model, dbTransaction, commandTimeout);
                case DataBaseType.MsSql:
                    return InsetMsSql(db, model, dbTransaction, commandTimeout);
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
        }
        /// <summary>
        /// 根据模型插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<object> InsertAsync<T>(this IDbConnection db, T model, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            switch (dataBaseType)
            {
                case DataBaseType.PostgreSql:
                    return InsertPgSqlAsync(db, model, dbTransaction, commandTimeout);
                case DataBaseType.MsSql:
                    return InsetMsSqlAsync(db, model, dbTransaction, commandTimeout);
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="modelsList"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int BulkInsert<T>(this IDbConnection db, IEnumerable<T> modelsList, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();

            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var columns = GetIgnoreCloums<T>(dataBaseType);
            var param = GetIgnoreParams<T>();
            var sql = $"INSERT INTO {tableName}({columns}) VALUES({param})";
            return db.Execute(sql, modelsList, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 批量插入异步方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="modelsList"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> BulkInsertAsync<T>(this IDbConnection db, IEnumerable<T> modelsList, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var columns = GetIgnoreCloums<T>(dataBaseType);
            var param = GetIgnoreParams<T>();
            var sql = $"INSERT INTO {tableName}({columns}) VALUES({param})";
            return db.ExecuteAsync(sql, modelsList, dbTransaction, commandTimeout);
        }
        #endregion

        #region [Update]
        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int Update<T>(this IDbConnection db, T model, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {

            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException($"{arr.PrimaryKey}未设置主键");
            var sql = $"UPDATE {tableName} SET { GetIgnoreWhereParams<T>(dataBaseType)} WHERE {UpdatePrimaryKeys<T>(dataBaseType)}";
            return db.Execute(sql, model, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 根据模型更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> UpdateAsync<T>(this IDbConnection db, T model, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {

            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException($"{arr.PrimaryKey}未设置主键");
            var sql = $"UPDATE {tableName} SET { GetIgnoreWhereParams<T>(dataBaseType)} WHERE {UpdatePrimaryKeys<T>(dataBaseType)}";
            return db.ExecuteAsync(sql, model, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 根据模型更新可以指定列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="changeClos"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> UpdateAsync<T>(this IDbConnection db, T model, string[] changeClos, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException($"{arr.PrimaryKey}未设置主键");
            string tableName;
            string sqlParse;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    sqlParse = GetIgnorePropertyInfo<T>().Where(c => changeClos.Contains(c.Name)).Select(c => $"{c.Name}=@{c.Name}").Join();
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    sqlParse = GetIgnorePropertyInfo<T>().Where(c => changeClos.Contains(c.Name)).Select(c => $"{c.Name.AddDoubleQuotes()}=@{c.Name}").Join();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"UPDATE {tableName} SET {sqlParse} WHERE {UpdatePrimaryKeys<T>(dataBaseType)}";
            return db.ExecuteAsync(sql, model);
        }
        /// <summary>
        /// 根据模型更新可以指定列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="changeClos"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int Update<T>(this IDbConnection db, T model, string[] changeClos, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException($"{arr.PrimaryKey}未设置主键");
            string tableName;
            string sqlParse;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    sqlParse = GetIgnorePropertyInfo<T>().Where(c => changeClos.Contains(c.Name)).Select(c => $"{c.Name}=@{c.Name}").Join();
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    sqlParse = GetIgnorePropertyInfo<T>().Where(c => changeClos.Contains(c.Name)).Select(c => $"{c.Name.AddDoubleQuotes()}=@{c.Name}").Join();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"UPDATE {tableName} SET {sqlParse} WHERE {UpdatePrimaryKeys<T>(dataBaseType)}";
            return db.Execute(sql, model);
        }
        /// <summary>
        /// 根据lambda表达式更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="expression"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int UpdateWhere<T>(this IDbConnection db, T model, Expression<Func<T, bool>> expression, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"UPDATE {tableName} SET {GetIgnoreWhereParams<T>(dataBaseType)} WHERE {expression.ToSql(out DynamicParameters dynamicParameters, dataBaseType)}";
            dynamicParameters.AddDynamicParams(model);
            return db.Execute(sql, dynamicParameters, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 根据lambda表达式更新指定列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="changeClos"></param>
        /// <param name="expression"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int UpdateWhere<T>(this IDbConnection db, T model, string[] changeClos, Expression<Func<T, bool>> expression,
            DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null,
            int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            string sqlParse;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    sqlParse = GetIgnorePropertyInfo<T>().Where(c => changeClos.Contains(c.Name)).Select(c => $"{c.Name}=@{c.Name}").Join();
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    sqlParse = GetIgnorePropertyInfo<T>().Where(c => changeClos.Contains(c.Name)).Select(c => $"{c.Name.AddDoubleQuotes()}=@{c.Name}").Join();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"UPDATE {tableName} SET {sqlParse} WHERE {expression.ToSql(out DynamicParameters dynamicParameters, dataBaseType)}";
            dynamicParameters.AddDynamicParams(model);
            return db.Execute(sql, dynamicParameters, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 根据lambda表达式异步更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="expression"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> UpdateWhereAsync<T>(this IDbConnection db, T model, Expression<Func<T, bool>> expression, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"UPDATE {tableName} SET {GetIgnoreWhereParams<T>(dataBaseType)} WHERE {expression.ToSql(out DynamicParameters dynamicParameters, dataBaseType)}";
            dynamicParameters.AddDynamicParams(model);
            return db.ExecuteAsync(sql, dynamicParameters, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 根据lambda表达式更新指定列异步更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="changeClos"></param>
        /// <param name="expression"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> UpdateWhereAsync<T>(this IDbConnection db, T model, string[] changeClos, Expression<Func<T, bool>> expression,
            DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null,
            int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            string sqlParse;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    sqlParse = GetIgnorePropertyInfo<T>().Where(c => changeClos.Contains(c.Name)).Select(c => $"{c.Name}=@{c.Name}").Join();
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    sqlParse = GetIgnorePropertyInfo<T>().Where(c => changeClos.Contains(c.Name)).Select(c => $"{c.Name.AddDoubleQuotes()}=@{c.Name}").Join();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"UPDATE {tableName} SET {sqlParse} WHERE {expression.ToSql(out DynamicParameters dynamicParameters, dataBaseType)}";
            dynamicParameters.AddDynamicParams(model);
            return db.ExecuteAsync(sql, dynamicParameters, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="modelsList"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int BulkUpdate<T>(this IDbConnection db, IEnumerable<T> modelsList, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            //没有主键
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException($"{arr.PrimaryKey}未设置主键");
            var sql = $"UPDATE {tableName} SET {GetIgnoreWhereParams<T>(dataBaseType)} WHERE {UpdatePrimaryKeys<T>(dataBaseType)}";
            return db.Execute(sql, modelsList, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 批量异步更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="modelsList"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> BulkUpdateAsync<T>(this IDbConnection db, IEnumerable<T> modelsList, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            //没有主键
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException($"{arr.PrimaryKey}未设置主键");
            var sql = $"UPDATE {tableName} SET {GetIgnoreWhereParams<T>(dataBaseType)} WHERE {UpdatePrimaryKeys<T>(dataBaseType)}";
            return db.ExecuteAsync(sql, modelsList, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 批量更新指定列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="modelsList"></param>
        /// <param name="changeClos"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int BulkUpdate<T>(this IDbConnection db, IEnumerable<T> modelsList, string[] changeClos, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            string sqlParse;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    sqlParse = GetIgnorePropertyInfo<T>().Where(c => changeClos.Contains(c.Name)).Select(c => $"{c.Name}=@{c.Name}").Join();
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    sqlParse = GetIgnorePropertyInfo<T>().Where(c => changeClos.Contains(c.Name)).Select(c => $"{c.Name.AddDoubleQuotes()}=@{c.Name}").Join();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            //没有主键
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException($"{arr.PrimaryKey}未设置主键");
            var sql = $"UPDATE {tableName} SET {sqlParse} WHERE {UpdatePrimaryKeys<T>(dataBaseType)}";
            return db.Execute(sql, modelsList, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 批量异步更新指定列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="modelsList"></param>
        /// <param name="changeClos"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> BulkUpdateAsync<T>(this IDbConnection db, IEnumerable<T> modelsList, string[] changeClos, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            string sqlParse;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    sqlParse = GetIgnorePropertyInfo<T>().Where(c => changeClos.Contains(c.Name)).Select(c => $"{c.Name}=@{c.Name}").Join();
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    sqlParse = GetIgnorePropertyInfo<T>().Where(c => changeClos.Contains(c.Name)).Select(c => $"{c.Name.AddDoubleQuotes()}=@{c.Name}").Join();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            //没有主键
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException($"{arr.PrimaryKey}未设置主键");
            var sql = $"UPDATE {tableName} SET {sqlParse} WHERE {UpdatePrimaryKeys<T>(dataBaseType)}";
            return db.ExecuteAsync(sql, modelsList, dbTransaction, commandTimeout);
        }
        #endregion

        #region [Delete]

        /// <summary>
        /// 通过主键删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="primaryKey"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int DeleteByKey<T>(this IDbConnection db, object primaryKey, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException("未设置主键");
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"DELETE FROM {tableName} WHERE {UpdatePrimaryKeys<T>(dataBaseType)}";
            if (arr.PrimaryKey.Split(',').Length != 1)
                return db.Execute(sql, primaryKey, dbTransaction, commandTimeout);
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(arr.PrimaryKey, primaryKey);
            return db.Execute(sql, dynamicParameters, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 通过主键删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="primaryKey"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> DeleteByKeyAsync<T>(this IDbConnection db, object primaryKey, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException("未设置主键");
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"DELETE FROM {tableName} WHERE {UpdatePrimaryKeys<T>(dataBaseType)}";
            if (arr.PrimaryKey.Split(',').Length != 1)
                return db.ExecuteAsync(sql, primaryKey, dbTransaction, commandTimeout);
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(arr.PrimaryKey, primaryKey);
            return db.ExecuteAsync(sql, dynamicParameters, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 根据lambel表达式删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="whereExpression"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int DeleteByWhere<T>(this IDbConnection db, Expression<Func<T, bool>> whereExpression, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"DELETE FROM {tableName} WHERE {whereExpression.ToSql(out DynamicParameters dynamicParameters, dataBaseType)}";
            return db.Execute(sql, dynamicParameters, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 通过lambda表达式删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="whereExpression"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> DeleteByWhereAsync<T>(this IDbConnection db, Expression<Func<T, bool>> whereExpression, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"DELETE FROM {tableName} WHERE {whereExpression.ToSql(out DynamicParameters dynamicParameters, dataBaseType)}";
            return db.ExecuteAsync(sql, dynamicParameters, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 根据主键批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="primaryKeys"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int BulkDeleTeByKeys<T>(this IDbConnection db, object[] primaryKeys, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null,
            int? commandTimeout = null)
        {

            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    return BulkDeleTeByKeysMsSql<T>(db, primaryKeys, dbTransaction, commandTimeout);
                case DataBaseType.PostgreSql:
                    return BulkDeleTeByKeysPgSql<T>(db, primaryKeys, dbTransaction, commandTimeout);
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
        }
        /// <summary>
        /// 根据主键批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="primaryKeys"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> BulkDeleTeByKeysAsync<T>(this IDbConnection db, object[] primaryKeys, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null,
            int? commandTimeout = null)
        {

            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    return BulkDeleTeByKeysMsSqlAsync<T>(db, primaryKeys, dbTransaction, commandTimeout);
                case DataBaseType.PostgreSql:
                    return BulkDeleTeByKeysPgSqlAsync<T>(db, primaryKeys, dbTransaction, commandTimeout);
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
        }
        /// <summary>
        /// 根据模型删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int DeleteByModel<T>(this IDbConnection db, T model, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"DELETE FROM {tableName} WHERE {GetWhereParams<T>(dataBaseType, " AND ")}";
            return db.Execute(sql, model, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 根据模型删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> DeleteByModelAsync<T>(this IDbConnection db, T model, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"DELETE FROM {tableName} WHERE {GetWhereParams<T>(dataBaseType, " AND ")}";
            return db.ExecuteAsync(sql, model, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 根据模型批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="modelList"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int DeleteByModels<T>(this IDbConnection db, IEnumerable<T> modelList, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"DELETE FROM {tableName} WHERE {GetWhereParams<T>(dataBaseType, " AND ")}";
            return db.Execute(sql, modelList, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 根据模型批量异步删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="modelList"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> DeleteByModelsAsync<T>(this IDbConnection db, IEnumerable<T> modelList, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"DELETE FROM {tableName} WHERE {GetWhereParams<T>(dataBaseType, " AND ")}";
            return db.ExecuteAsync(sql, modelList, dbTransaction, commandTimeout);
        }
        #endregion

        #region [SingleModel]
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="primaryKey"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static T GetModelByKey<T>(this IDbConnection db, object primaryKey, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException("未设置主键");
            var sql = $"SELECT  * FROM {tableName} WHERE {UpdatePrimaryKeys<T>(dataBaseType)} ";
            if (arr.PrimaryKey.Split(',').Length != 1) return db.QuerySingleOrDefault<T>(sql, primaryKey, dbTransaction, commandTimeout);
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(arr.PrimaryKey, primaryKey);
            return db.QuerySingleOrDefault<T>(sql, dynamicParameters, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="primaryKey"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<T> GetModelByKeyAsync<T>(this IDbConnection db, object primaryKey, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException("未设置主键");
            var sql = $"SELECT  * FROM {tableName} WHERE {UpdatePrimaryKeys<T>(dataBaseType)} ";
            if (arr.PrimaryKey.Split(',').Length != 1) return db.QuerySingleOrDefaultAsync<T>(sql, primaryKey, dbTransaction, commandTimeout);
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(arr.PrimaryKey, primaryKey);
            return db.QuerySingleOrDefaultAsync<T>(sql, dynamicParameters, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 根据lambda表达式获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="whereExpression"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static T GetModelWhere<T>(this IDbConnection db, Expression<Func<T, bool>> whereExpression, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"SELECT  * FROM {tableName} WHERE {whereExpression.ToSql(out DynamicParameters dynamicParameters, dataBaseType)} ";
            return db.QueryFirstOrDefault<T>(sql, dynamicParameters, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 根据lambda表达式获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="whereExpression"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<T> GetModelWhereAsync<T>(this IDbConnection db, Expression<Func<T, bool>> whereExpression, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"SELECT  * FROM {tableName} WHERE {whereExpression.ToSql(out DynamicParameters dynamicParameters, dataBaseType)} ";
            return db.QueryFirstOrDefaultAsync<T>(sql, dynamicParameters, dbTransaction, commandTimeout);
        }
        #endregion

        #region [Count|Exit]
        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="whereExpression"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int GetCount<T>(this IDbConnection db, Expression<Func<T, bool>> whereExpression, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"SELECT  count(1) FROM {tableName} WHERE {whereExpression.ToSql(out DynamicParameters dynamicParameters, dataBaseType)} ";
            return db.ExecuteScalar<int>(sql, dynamicParameters, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 获取列表数异步方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="whereExpression"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> GetCountAsync<T>(this IDbConnection db, Expression<Func<T, bool>> whereExpression, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            var sql = $"SELECT  count(1) FROM {tableName} WHERE {whereExpression.ToSql(out DynamicParameters dynamicParameters, dataBaseType)} ";
            return db.ExecuteScalarAsync<int>(sql, dynamicParameters, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 根据一个主键判断一条记录是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="primaryKey"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static bool ExitKey<T>(this IDbConnection db, object primaryKey, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            if (arr.PrimaryKey.Split(',').Length != 1) throw new Exception("该方法不支持复合主键或者未设置主键");
            var sql = $"SELECT  count(1) FROM {tableName} WHERE {UpdatePrimaryKeys<T>(dataBaseType)}";
            if (arr.PrimaryKey.Split(',').Length != 1) return db.ExecuteScalar<int>(sql, primaryKey, dbTransaction, commandTimeout) > 0;
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(arr.PrimaryKey, primaryKey);
            return db.ExecuteScalar<int>(sql, dynamicParameters, dbTransaction, commandTimeout) > 0;
        }
        /// <summary>
        /// 根据一个主键判断一条记录是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="primaryKey"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<bool> ExitKeyAsync<T>(this IDbConnection db, object primaryKey, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            string tableName;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    tableName = arr.TableName;
                    break;
                case DataBaseType.PostgreSql:
                    tableName = arr.TableName.AddDoubleQuotes();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            if (arr.PrimaryKey.Split(',').Length != 1) throw new Exception("该方法不支持复合主键或者未设置主键");
            var sql = $"SELECT  count(1) FROM {tableName} WHERE {UpdatePrimaryKeys<T>(dataBaseType)}";
            if (arr.PrimaryKey.Split(',').Length != 1) return await db.ExecuteScalarAsync<int>(sql, primaryKey, dbTransaction, commandTimeout) > 0;
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(arr.PrimaryKey, primaryKey);
            return await db.ExecuteScalarAsync<int>(sql, dynamicParameters, dbTransaction, commandTimeout) > 0;
        }
        /// <summary>
        /// 根据lambda表达式判断一条记录是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="whereExpression"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static bool ExitWhere<T>(this IDbConnection db, Expression<Func<T, bool>> whereExpression, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            return db.GetCount(whereExpression, dataBaseType, dbTransaction, commandTimeout) > 0;
        }
        /// <summary>
        /// 根据lambda表达式判断一条记录是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="whereExpression"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<bool> ExitWhereAsync<T>(this IDbConnection db, Expression<Func<T, bool>> whereExpression, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            return await db.GetCountAsync(whereExpression, dataBaseType, dbTransaction, commandTimeout) > 0;
        }
        #endregion

        #region[Page]
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="param"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static PageResult<T> Page<T>(this IDbConnection db, string sql, int pageIndex, int pageSize, object param = null, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, bool buffered = true, int? commandTimeout = null)
        {
            var pageResult = new PageResult<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var isSql = PagingHelper.SplitSql(sql, out var parts);
            if (!isSql) throw new Exception("sql语句有误");
            var sqlCount = parts.SqlCount;
            pageResult.TotalCount = db.ExecuteScalar<int>(sqlCount, param);

            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    if (parts.SqlOrderBy.IsNullOrEmpty()) parts.Sql += "(select 0) Order By";
                    pageResult.Rows = db.Query<T>($"{parts.Sql} OFFSET {(pageIndex - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY", param, dbTransaction, buffered, commandTimeout);
                    break;
                case DataBaseType.PostgreSql:
                    pageResult.Rows = db.Query<T>($"{parts.Sql} limit {pageSize} offset {(pageIndex - 1) * pageSize}", param, dbTransaction, buffered, commandTimeout);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }

            return pageResult;
        }
        /// <summary>
        /// 分页异步方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="param"></param>
        /// <param name="dataBaseType"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<PageResult<T>> PageAsync<T>(this IDbConnection db, string sql, int pageIndex, int pageSize, object param = null, DataBaseType dataBaseType = DataBaseType.MsSql, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {


            var pageResult = new PageResult<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var isSql = PagingHelper.SplitSql(sql, out var parts);
            if (!isSql) throw new Exception("sql语句有误");
            var sqlCount = parts.SqlCount;
            pageResult.TotalCount = await db.ExecuteScalarAsync<int>(sqlCount, param);

            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    if (parts.SqlOrderBy.IsNullOrEmpty()) parts.Sql += "(select 0) Order By";
                    pageResult.Rows = await db.QueryAsync<T>($"{parts.Sql} OFFSET {(pageIndex - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY", param, dbTransaction, commandTimeout);
                    break;
                case DataBaseType.PostgreSql:
                    pageResult.Rows = await db.QueryAsync<T>($"{parts.Sql} limit {pageSize} offset {(pageIndex - 1) * pageSize}", param, dbTransaction, commandTimeout);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }

            return pageResult;
        }
        #endregion

        #region [Supports]

        private static IEnumerable<PropertyInfo> GetAllPropertyInfo<T>()
        {
            return typeof(T).GetPropertiesCache();
        }
        private static string GetWhereParams<T>(DataBaseType dataBaseType, string split = ",")
        {
            string str;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    str = GetAllPropertyInfo<T>().Select(c => $"{c.Name}=@{c.Name}").Join(split);
                    break;
                case DataBaseType.PostgreSql:
                    str = GetAllPropertyInfo<T>().Select(c => $"{c.Name.AddDoubleQuotes()}=@{c.Name}").Join(split);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            return str;
        }
        private static string GetIgnoreParams<T>()
        {
            return GetIgnorePropertyInfos<T>().Select(c => $"@{c.Name}").Join();
        }
        /// <summary>
        /// 忽略列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static IEnumerable<PropertyInfo> GetIgnorePropertyInfos<T>()
        {
            var atrr = GetDbTableAttr<T>();
            var ignoresList = string.IsNullOrEmpty(atrr.Ignore) ? new List<string>() : atrr.Ignore.Split(',').ToList();
            if (atrr.AutoIncrement) ignoresList.Add(atrr.PrimaryKey.Split(',')[0]);
            return !ignoresList.Any() ? GetAllPropertyInfo<T>() : GetAllPropertyInfo<T>().Where(c => !ignoresList.Contains(c.Name));

        }
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static DbTableAttribute GetDbTableAttr<T>()
        {
            return typeof(T).GetAttribute<DbTableAttribute>();
        }
        /// <summary>
        /// 去忽略列的其他列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static string GetIgnoreCloums<T>(DataBaseType dataBaseType)
        {
            string str;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    str = GetIgnorePropertyInfos<T>().Select(c => c.Name).Join();
                    break;
                case DataBaseType.PostgreSql:
                    str = GetIgnorePropertyInfos<T>().Select(c => c.Name.AddDoubleQuotes()).Join();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            return str;
        }
        /// <summary>
        /// 去忽略列的其他列带参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static string GetIgnoreWhereParams<T>(DataBaseType dataBaseType)
        {
            string str;
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    str = GetIgnorePropertyInfos<T>().Select(c => $"{c.Name}=@{c.Name}").Join();
                    break;
                case DataBaseType.PostgreSql:
                    str = GetIgnorePropertyInfos<T>().Select(c => $"{c.Name.AddDoubleQuotes()}=@{c.Name}").Join();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }
            return str;
        }
        private static IEnumerable<PropertyInfo> GetIgnorePropertyInfo<T>()
        {
            var arr = GetDbTableAttr<T>();
            var ignoresList = string.IsNullOrEmpty(arr.Ignore) ? new List<string>() : arr.Ignore.Split(',').ToList();
            if (arr.AutoIncrement) ignoresList.Add(arr.PrimaryKey.Split(',')[0]);
            return !ignoresList.Any() ? GetAllPropertyInfo<T>().ToList() : GetAllPropertyInfo<T>().Where(c => !ignoresList.Contains(c.Name)).ToList();
        }
        private static string UpdatePrimaryKeys<T>(DataBaseType dataBaseType)
        {
            var arr = GetDbTableAttr<T>();
            var primaryKeys = arr.PrimaryKey.Split(',');
            switch (dataBaseType)
            {
                case DataBaseType.MsSql:
                    return primaryKeys.Select(c => $"{c}=@{c}").Join(" AND ");
                case DataBaseType.PostgreSql:
                    return primaryKeys.Select(c => $"{c.AddDoubleQuotes()}=@{c}").Join(" AND ");
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataBaseType), dataBaseType, null);
            }

        }
        #endregion

        #region [Sql]
        /// <summary>
        /// Pgsql插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        private static object InsertPgSql<T>(this IDbConnection db, T model, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            var columns = GetIgnoreCloums<T>(DataBaseType.PostgreSql);
            var param = GetIgnoreParams<T>();
            var sql = new StringBuilder($"INSERT INTO {arr.TableName.AddDoubleQuotes()} ({columns}) VALUES({param}) RETURNING ");
            sql.Append(!string.IsNullOrEmpty(arr.PrimaryKey)
                ? arr.PrimaryKey.Split(',').Select(c => c.AddDoubleQuotes()).Join()
                : GetAllPropertyInfo<T>().ToArray()[0].Name.AddDoubleQuotes());
            return string.IsNullOrEmpty(arr.PrimaryKey) || !arr.PrimaryKey.Contains(",") ? db.ExecuteScalar<object>(sql.ToString(), model, dbTransaction, commandTimeout)
                : db.QuerySingle<object>(sql.ToString(), model, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// Pgsql插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        private static Task<object> InsertPgSqlAsync<T>(this IDbConnection db, T model, IDbTransaction dbTransaction = null, int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            var columns = GetIgnoreCloums<T>(DataBaseType.PostgreSql);
            var param = GetIgnoreParams<T>();
            var sql = new StringBuilder($"INSERT INTO {arr.TableName.AddDoubleQuotes()} ({columns}) VALUES({param}) RETURNING ");
            sql.Append(!string.IsNullOrEmpty(arr.PrimaryKey)
                ? arr.PrimaryKey.Split(',').Select(c => c.AddDoubleQuotes()).Join()
                : GetAllPropertyInfo<T>().ToArray()[0].Name.AddDoubleQuotes());
            return string.IsNullOrEmpty(arr.PrimaryKey) || !arr.PrimaryKey.Contains(",") ? db.ExecuteScalarAsync<object>(sql.ToString(), model, dbTransaction, commandTimeout)
                : db.QuerySingleAsync<object>(sql.ToString(), model, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// MsSql插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        private static object InsetMsSql<T>(this IDbConnection db, T model, IDbTransaction dbTransaction = null,
            int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            var columns = GetIgnoreCloums<T>(DataBaseType.MsSql);
            var param = GetIgnoreParams<T>();
            var sql = new StringBuilder($"INSERT INTO {arr.TableName}({columns}) VALUES({param})");
            if (arr.AutoIncrement)
            {
                sql.Append(";SELECT @@IDENTITY");
                return db.ExecuteScalar<int>(sql.ToString(), model, dbTransaction, commandTimeout);
            }
            var res = db.Execute(sql.ToString(), model, dbTransaction, commandTimeout);
            if (res <= 0) return null;
            if (string.IsNullOrEmpty(arr.PrimaryKey))
                return model.GetType().GetPropertyValue(GetAllPropertyInfo<T>().Select(c => c.Name).ToArray()[0]);
            var dynamicParameters = new DynamicParameters();
            foreach (var item in arr.PrimaryKey.Split(','))
            {
                dynamicParameters.Add(item, model.GetType().GetPropertyValue(item));
            }
            return dynamicParameters;
        }

        /// <summary>
        /// MsSql插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        private static async Task<object> InsetMsSqlAsync<T>(this IDbConnection db, T model, IDbTransaction dbTransaction = null,
            int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            var columns = GetIgnoreCloums<T>(DataBaseType.MsSql);
            var param = GetIgnoreParams<T>();
            var sql = new StringBuilder($"INSERT INTO {arr.TableName}({columns}) VALUES({param})");
            if (arr.AutoIncrement)
            {
                sql.Append(";SELECT @@IDENTITY");
                return db.ExecuteScalarAsync<int>(sql.ToString(), model, dbTransaction, commandTimeout);
            }
            var res = await db.ExecuteAsync(sql.ToString(), model, dbTransaction, commandTimeout);
            if (res <= 0) return null;
            if (string.IsNullOrEmpty(arr.PrimaryKey))
                return model.GetType().GetPropertyValue(GetAllPropertyInfo<T>().Select(c => c.Name).ToArray()[0]);
            var dynamicParameters = new DynamicParameters();
            foreach (var item in arr.PrimaryKey.Split(','))
            {
                dynamicParameters.Add(item, model.GetType().GetPropertyValue(item));
            }
            return dynamicParameters;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="primaryKeys"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        private static int BulkDeleTeByKeysMsSql<T>(this IDbConnection db, object[] primaryKeys, IDbTransaction dbTransaction = null,
            int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException("未设置主键");
            if (arr.PrimaryKey.Split(',').Length == 1)
            {
                var sb = new StringBuilder($"DELETE FROM {arr.TableName} WHERE {arr.PrimaryKey} IN @p0 ");
                return db.Execute(sb.ToString(), new { p0 = primaryKeys.ToArray() }, dbTransaction, commandTimeout);
            }
            var str = new StringBuilder($"DELETE FROM {arr.TableName} WHERE  ");
            str.Append(arr.PrimaryKey.Split(',').Select(c => $"{c} IN @{c}").Join(" AND "));
            return db.Execute(str.ToString(), primaryKeys, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="primaryKeys"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        private static Task<int> BulkDeleTeByKeysMsSqlAsync<T>(this IDbConnection db, object[] primaryKeys, IDbTransaction dbTransaction = null,
            int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException("未设置主键");
            if (arr.PrimaryKey.Split(',').Length == 1)
            {
                var sb = new StringBuilder($"DELETE FROM {arr.TableName} WHERE {arr.PrimaryKey} IN @p0 ");
                return db.ExecuteAsync(sb.ToString(), new { p0 = primaryKeys.ToArray() }, dbTransaction, commandTimeout);
            }
            var str = new StringBuilder($"DELETE FROM {arr.TableName} WHERE  ");
            str.Append(arr.PrimaryKey.Split(',').Select(c => $"{c} IN @{c}").Join(" AND "));
            return db.ExecuteAsync(str.ToString(), primaryKeys, dbTransaction, commandTimeout);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="primaryKeys"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        private static int BulkDeleTeByKeysPgSql<T>(this IDbConnection db, IEnumerable<object> primaryKeys, IDbTransaction dbTransaction = null,
            int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();

            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException("未设置主键");
            if (arr.PrimaryKey.Split(',').Length != 1) throw new NotSupportedException("PgSql该方法暂时不支持复合主键");
            var sb = new StringBuilder($"DELETE FROM {arr.TableName.AddDoubleQuotes()} WHERE {arr.PrimaryKey.AddDoubleQuotes()} IN ");
            var dynamicParameters = new DynamicParameters();
            var i = 0;
            var keysList = primaryKeys.Select(c =>
            {
                i++;
                dynamicParameters.Add($"p{i}", c);
                return $"@p{i}";
            });
            sb = sb.Append($"({keysList.Join()})");
            return db.Execute(sb.ToString(), dynamicParameters, dbTransaction, commandTimeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="primaryKeys"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        private static Task<int> BulkDeleTeByKeysPgSqlAsync<T>(this IDbConnection db, IEnumerable<object> primaryKeys, IDbTransaction dbTransaction = null,
            int? commandTimeout = null)
        {
            var arr = GetDbTableAttr<T>();
            if (string.IsNullOrEmpty(arr.PrimaryKey)) throw new NotSupportedException("未设置主键");
            if (arr.PrimaryKey.Split(',').Length != 1) throw new NotSupportedException("PgSql该方法暂时不支持复合主键");
            var sb = new StringBuilder($"DELETE FROM {arr.TableName.AddDoubleQuotes()} WHERE {arr.PrimaryKey.AddDoubleQuotes()} IN ");
            var dynamicParameters = new DynamicParameters();
            var i = 0;
            var keysList = primaryKeys.Select(c =>
            {
                i++;
                dynamicParameters.Add($"p{i}", c);
                return $"@p{i}";
            });
            sb = sb.Append($"({keysList.Join()})");
            return db.ExecuteAsync(sb.ToString(), dynamicParameters, dbTransaction, commandTimeout);
        }
        #endregion
    }
}
