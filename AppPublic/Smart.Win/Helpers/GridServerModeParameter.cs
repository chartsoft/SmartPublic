using System.Collections.Generic;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;

namespace Smart.Win.Helpers
{

    /// <summary>
    /// 排序列改变事件
    /// </summary>
    /// <param name="column">当前排序列</param>
    /// <param name="order">排序顺序</param>
    public delegate void SortColumnChangedEventHandler(GridColumn column, ColumnSortOrder order);


    /// <summary>
    /// XtraGrid服务端模式参数
    /// </summary>
    public class GridServerModeParameter
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="allowSortColumns">允许排序列</param>
        /// <param name="canSortImage">可排序列图片</param>
        /// <param name="asscendImage">升序图片</param>
        /// <param name="descendImage">降序图片</param>
        /// <param name="sortColumn">默认排序列</param>
        /// <param name="sortOrder">默认排序顺序</param>
        /// <param name="sortColumnChanged">排序列改变委托方法</param>
        public GridServerModeParameter(List<GridColumn> allowSortColumns, int canSortImage, int asscendImage, int descendImage, GridColumn sortColumn, ColumnSortOrder sortOrder, SortColumnChangedEventHandler sortColumnChanged)
        {
            AllowSortColumns = allowSortColumns;
            CanSortImage = canSortImage;
            AsscendImage = asscendImage;
            DescendImage = descendImage;
            SortColumn = sortColumn;
            SortOrder = sortOrder;
            SortColumnChanged = sortColumnChanged;
        }

        /// <summary>
        /// 允许排序列
        /// </summary>
        public List<GridColumn> AllowSortColumns { get; private set; }

        /// <summary>
        /// 允许排序图片
        /// </summary>
        public int CanSortImage { get; private set; }

        /// <summary>
        /// 升序图片
        /// </summary>
        public int AsscendImage { get; private set; }

        /// <summary>
        /// 降序图片
        /// </summary>
        public int DescendImage { get; private set; }

        /// <summary>
        /// 排序列
        /// </summary>
        public GridColumn SortColumn { get; set; }

        /// <summary>
        /// 排序顺序
        /// </summary>
        public ColumnSortOrder SortOrder { get; set; }

        /// <summary>
        /// 排序列改变委托
        /// </summary>
        public SortColumnChangedEventHandler SortColumnChanged { get; private set; }

        /// <summary>
        /// 激发排序列改变事件
        /// </summary>
        public void OnSortColumnChanged()
        {
            if (SortColumnChanged != null)
            {
                SortColumnChanged(SortColumn, SortOrder);
            }
        }

    }
}