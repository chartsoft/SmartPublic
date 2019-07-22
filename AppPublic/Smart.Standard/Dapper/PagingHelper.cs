using System.Text.RegularExpressions;

namespace Smart.Standard.Dapper
{
    /// <summary>
    /// 
    /// </summary>
    public class PagingHelper
    {
        /// <summary>
        /// 正则列
        /// </summary>
        public static Regex RxColumns = new Regex(@"\A\s*SELECT\s+((?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|.)*?)(?<!,\s+)\bFROM\b", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);
        /// <summary>
        /// 正则分页
        /// </summary>
        public static Regex RxOrderBy = new Regex(@"(?!.*(?:\s+FROM[\s\(]+))ORDER\s+BY\s+([\w\.\[\]\(\)\s""`,]+)(?!.*\))", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);
        /// <summary>
        /// sql参数
        /// </summary>
        public struct SqlParts
        {
            /// <summary>
            /// 
            /// </summary>
            public string Sql;
            /// <summary>
            /// 
            /// </summary>
            public string SqlCount;
            /// <summary>
            /// 
            /// </summary>
            public string SqlSelectRemoved;
            /// <summary>
            /// 
            /// </summary>
            public string SqlOrderBy;
            /// <summary>
            /// 
            /// </summary>
            public string SqlUnordered;
            /// <summary>
            /// 
            /// </summary>
            public string SqlColumns;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parts"></param>
        /// <returns></returns>
        public static bool SplitSql(string sql, out SqlParts parts)
        {
            parts.Sql = sql;
            parts.SqlSelectRemoved = null;
            parts.SqlCount = null;
            parts.SqlOrderBy = null;
            parts.SqlUnordered = sql.Trim().Trim(';');
            parts.SqlColumns = "*";

            // Extract the columns from "SELECT <whatever> FROM"
            var m = RxColumns.Match(sql);
            if (!m.Success) return false;

            // Save column list  [and replace with COUNT(*)]
            Group g = m.Groups[1];
            parts.SqlSelectRemoved = sql.Substring(g.Index);

            // Look for the last "ORDER BY <whatever>" clause not part of a ROW_NUMBER expression
            var matches = RxOrderBy.Matches(parts.SqlUnordered);
            if (matches.Count > 0)
            {
                m = matches[matches.Count - 1];
                g = m.Groups[0];
                parts.SqlOrderBy = g.ToString();
                parts.SqlUnordered = RxOrderBy.Replace(parts.SqlUnordered, "", 1, m.Index);
            }

            parts.SqlCount = $@"SELECT COUNT(*) FROM ({parts.SqlUnordered}) dapper_tbl";

            return true;
        }
    }
}
