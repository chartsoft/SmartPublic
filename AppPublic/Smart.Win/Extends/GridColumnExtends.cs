using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;

namespace Smart.Win.Extends
{
    /// <summary>
    /// XtraGrid辅助类
    /// </summary>
    public static class GridColumnExtends
    {

        /// <summary>
        /// 固定列宽
        /// </summary>
        /// <param name="column">列</param>
        /// <param name="width">宽度</param>
        public static void FixColumnWidth(this GridColumn column, int width)
        {
            column.MaxWidth = column.MinWidth = column.Width = width;
        }

        /// <summary>
        /// 固定列宽
        /// </summary>
        /// <param name="column">列</param>
        /// <param name="width">宽度</param>
        public static void AutoColumnWidth(this GridColumn column, int width)
        {
            column.MaxWidth = 0;
            column.MinWidth = 0;
            column.Width = width;
        }

        /// <summary>
        /// 居中文字
        /// </summary>
        /// <param name="column">列</param>
        /// <param name="centerHeader">列头居中否</param>
        /// <param name="centerCell">列单元格居中否</param>
        public static void CenterText(this GridColumn column, bool centerHeader, bool centerCell)
        {
            if (centerCell)
            {
                column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
            if (centerHeader)
            {
                column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            }
        }

    }
}
