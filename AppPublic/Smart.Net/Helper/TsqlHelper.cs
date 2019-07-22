using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Smart.Net45.Helper
{
    /// <summary>
    /// TSQL辅助类
    /// </summary>
    public class TsqlHelper
    {
        /// <summary>
        /// 解析TSQL语句参数
        /// </summary>
        /// <param name="tsqlString">TSQL语句字符串</param>
        /// <returns>返回参数列表</returns>
        public static IEnumerable<string> AnalyseTsqlParameters(string tsqlString)
        {
            var result = new List<string>();
            //Regex paramReg = new Regex(@"@\w*");
            //Sql中包括@@rowcount之类的变量的情况，不应该算作参数
            var paramReg = new Regex(@"[^@@](?<p>@\w+)");
            var matches = paramReg.Matches(string.Concat(tsqlString, " "));
            foreach (Match m in matches)
            {
                var willAddItem = m.Groups["p"].Value;
                var isExist = result.Any(item => string.Equals(willAddItem.ToLower(), item.ToLower()));
                if (isExist) { continue; }

                result.Add(willAddItem);
            }
            return result;

        }
        /// <summary>
        /// SQLType转换为对应的DbType
        /// </summary>
        /// <param name="sqlTypeWithPrecision">SQLType字符（包含长度信息）</param>
        /// <returns>返回DbType枚举</returns>
        public static DbType SqlTypeStringToDbType(string sqlTypeWithPrecision)
        {
            DbType dbType;//默认为Object
            //去掉长度信息
            var startIndex = sqlTypeWithPrecision.IndexOf("(", StringComparison.Ordinal);
            var sqlTypeString = startIndex != -1 ? sqlTypeWithPrecision.Remove(startIndex) : sqlTypeWithPrecision;
            switch (sqlTypeString)
            {
                case "int":
                    dbType = DbType.Int32;
                    break;
                case "varchar":
                    dbType = DbType.String;
                    break;
                case "bit":
                    dbType = DbType.Boolean;
                    break;
                case "datetime":
                    dbType = DbType.DateTime;
                    break;
                case "decimal":
                    dbType = DbType.Decimal;
                    break;
                case "float":
                    dbType = DbType.Double;
                    break;
                case "image":
                    dbType = DbType.Binary;
                    break;
                case "money":
                    dbType = DbType.Currency;
                    break;
                case "ntext":
                    dbType = DbType.String;
                    break;
                case "nvarchar":
                    dbType = DbType.String;
                    break;
                case "smalldatetime":
                    dbType = DbType.DateTime;
                    break;
                case "smallint":
                    dbType = DbType.Int16;
                    break;
                case "text":
                    dbType = DbType.String;
                    break;
                case "bigint":
                    dbType = DbType.Int64;
                    break;
                case "binary":
                    dbType = DbType.Binary;
                    break;
                case "char":
                    dbType = DbType.String;
                    break;
                case "nchar":
                    dbType = DbType.String;
                    break;
                case "numeric":
                    dbType = DbType.Decimal;
                    break;
                case "real":
                    dbType = DbType.Double;
                    break;
                case "smallmoney":
                    dbType = DbType.Currency;
                    break;
                case "sql_variant":
                    dbType = DbType.String;
                    break;
                case "timestamp":
                    dbType = DbType.Int32;
                    break;
                case "tinyint":
                    dbType = DbType.Int16;
                    break;
                case "uniqueidentifier":
                    dbType = DbType.Guid;
                    break;
                case "varbinary":
                    dbType = DbType.Binary;
                    break;
                case "xml":
                    dbType = DbType.Xml;
                    break;
                default:
                    throw new Exception("暂时未实现该类型");
            }
            return dbType;
        }
    }
}
