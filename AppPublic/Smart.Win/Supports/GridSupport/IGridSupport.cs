namespace Smart.Win.Supports.GridSupport
{
    /// <summary>
    /// 行号/序号
    /// </summary>
    public interface IGridSupport
    {
        /// <summary>
        /// 行ID
        /// </summary>
        string GridRowId { get; }
        /// <summary>
        /// 行号
        /// </summary>
        int GridRowNo { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        int GridDataNo { get; set; }
    }
}
