using System.Collections.Generic;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace Smart.Win.Controls
{
    /// <summary>
    /// 地区信息导出模型
    /// </summary>
    public class BasicExportModel<T> where T:class
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string TextTitle { get; set; }
        /// <summary>
        /// 数据表头
        /// </summary>
        public Dictionary<string, string> DataHead
        {
            get;
            set;
        }
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Data { get; set; }
        /// <summary>
        /// 页脚
        /// </summary>
        public string TextFoot { get; set; }
        /// <summary>
        /// 页眉
        /// </summary>
        public string TextHead { get; set; }

        /// <summary>
        /// 设置数据源标头
        /// </summary>
        /// <param name="gridView"></param>
        public void SetDataHead(GridView gridView)
        {
            DataHead = new Dictionary<string, string>();
            foreach (GridColumn item in gridView.VisibleColumns)
            {
                DataHead.Add(item.FieldName, item.Caption);
            }
        }
    }
}
