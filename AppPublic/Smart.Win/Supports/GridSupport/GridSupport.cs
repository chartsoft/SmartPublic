using System;

namespace Smart.Win.Supports.GridSupport
{
    /// <summary>
    /// Grid表格支持
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    [Serializable]
    public abstract class GridSupport<T> : IGridSupport where T : GridSupport<T>
    {
        /// <summary>
        /// 表格行 唯一标识
        /// </summary>
        public abstract string GridRowId { get; }
        /// <summary>
        /// 行号
        /// </summary>
        public int GridRowNo { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int GridDataNo { get; set; }

    }
}

