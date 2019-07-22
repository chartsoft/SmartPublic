using System.Text;
using Smart.Standard.Extends;

namespace Smart.Standard.Helper
{
    /// <summary>
    /// Sql操作辅助类
    /// </summary>
    public class SqlHelper
    {
        private StringBuilder _sql = new StringBuilder();
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sql"></param>
        public SqlHelper(string sql)
        {
            _sql.Append(sql);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlHelper()
        {
            _sql.Append(string.Empty);
        }
        /// <summary>
        /// Append
        /// </summary>
        public SqlHelper Append(string sql)
        {
            _sql = _sql.Append(sql);
            return this;
        }
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SqlHelper Where(string sql)
        {
            Append($" WHERE {sql} ");
            return this;
        }
        /// <summary>
        /// And
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SqlHelper And(string sql)
        {
            Append($" AND {sql}");
            return this;
        }
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public SqlHelper Select(params object[] columns)
        {
            Append($" SELECT {columns.Join()} ");
            return this;
        }
        /// <summary>
        /// From
        /// </summary>
        /// <param name="tables"></param>
        /// <returns></returns>
        public SqlHelper From(params object[] tables)
        {
            Append($" FROM {tables.Join()}");
            return this;
        }
        /// <summary>
        /// GroupBy
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public SqlHelper GroupBy(params object[] columns)
        {
            Append($" GROUP BY {columns.Join()}");
            return this;
        }
        /// <summary>
        /// InnerJoin
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public SqlHelper InnerJoin(string table)
        {
            Append($" INNER JOIN {table}");
            return this;
        }
        /// <summary>
        /// LeftJoin
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public SqlHelper LeftJoin(string table)
        {
            Append($" LEFT JOIN {table}");
            return this;
        }
        /// <summary>
        /// RightJoin
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public SqlHelper RightJoin(string table)
        {
            Append($" RIGHT JOIN {table}");
            return this;
        }
        /// <summary>
        /// On
        /// </summary>
        /// <param name="onClause"></param>
        /// <returns></returns>
        public SqlHelper On(string onClause)
        {
            Append($" ON {onClause}");
            return this;
        }

        /// <summary>
        ///  ORDER BY
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public SqlHelper OrderBy(params object[] columns)
        {
            Append($" ORDER BY {columns.Join()}");
            return this;
        }
        /// <summary>
        /// OR
        /// </summary>
        /// <param name="orClause"></param>
        /// <returns></returns>
        public SqlHelper Or( string orClause)
        {
            Append($" OR {orClause}");
            return this;

        }
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _sql.ToString();
        }



    }
}
