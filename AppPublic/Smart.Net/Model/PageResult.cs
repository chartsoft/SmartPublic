using System.Collections.Generic;

namespace Smart.Net45.Model
{
    /// <summary>
    /// 分页
    /// </summary>
    public class PageResult<T>
    {
        /// <summary>
        /// 查询记录
        /// </summary>
        public IEnumerable<T> Rows { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public  int TotalCount { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public  int PageIndex { get; set; }
        /// <summary>
        /// 每页显示数
        /// </summary>
        public int PageSize { get; set; }

    }
}
