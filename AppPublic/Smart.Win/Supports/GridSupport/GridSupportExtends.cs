using System.Collections.Generic;

namespace Smart.Win.Supports.GridSupport
{

    /// <summary>
    /// Grid表格支持
    /// </summary>
    public static class GridSupportExtends
    {
        /// <summary>
        /// 初始化行号
        /// </summary>
        /// <param name="dataList">数据列表</param>
        public static List<T> InitRowNo<T>(this List<T> dataList) where T : GridSupport<T>
        {
            if (dataList == null || dataList.Count <= 0) return new List<T>();
            var rowNo = 1;
            dataList.ForEach(data => ((IGridSupport)data).GridRowNo = rowNo++);
            return dataList;
        }

        /// <summary>
        /// 初始化序号
        /// </summary>
        /// <param name="dataList">数据列表</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        public static List<T> InitDataNo<T>(List<T> dataList, int pageIndex, int pageSize) where T : GridSupport<T>
        {
            if (dataList == null || dataList.Count <= 0) return new List<T>();
            var rowNo = (pageIndex - 1) * pageSize + 1;
            dataList.ForEach(data => ((IGridSupport)data).GridDataNo = rowNo++);
            return dataList;
        }

    }
}

