using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Smart.Win.Extends
{
    /// <summary>
    /// XtraGrid辅助类
    /// </summary>
    public static class GridControlExtends
    {


        /// <summary>
        /// 取Grid选中数据
        /// </summary>
        public static List<T> GetBindList<T>(this GridControl grid) where T : class
        {
            return grid.DataSource as List<T>;
        }


        /// <summary>
        /// 绑定CtrlC
        /// </summary>
        /// <param name="gridControl"></param>
        /// <param name="columns"></param>
        public static void BindCopyToCtrlC(this GridControl gridControl, params GridColumn[] columns)
        {
            gridControl.KeyDown += (sender, e) =>
            {
                if (e.Control && e.KeyValue == 67)
                {
                    var gc = sender as GridControl;

                    if (gc != null && gc.Focused)
                    {
                        var gv = gc.FocusedView as GridView;
                        if (gv != null)
                        {
                            if (columns == null || columns.Length == 0)
                            {
                                CopyTextToClipboard(gv);
                            }
                            else if (columns.Length > 0 && columns.Contains(gv.FocusedColumn))
                            {
                                CopyTextToClipboard(gv);
                            }
                            else
                            {
                                Clipboard.Clear();
                            }
                            e.Handled = true;
                        }
                    }
                }
            };
        }

        private static void CopyTextToClipboard(GridView gv)
        {
            var val = gv.GetFocusedValue();
            var str = (val ?? "").ToString();
            if (!string.IsNullOrEmpty(str) && !(val is Bitmap))
            {
                Clipboard.SetText(str);
            }
            else
            {
                Clipboard.Clear();
            }
        }

        /// <summary>
        /// 向Grid注册提示信息
        /// </summary>
        /// <typeparam name="TEntity">grid绑定的实体类型</typeparam>
        /// <param name="gridControl">要注册的Grid</param>
        /// <param name="showTipsCondition">显示提示信息的条件</param>
        /// <param name="getCellHintText">获取显示文本的委托</param>
        /// <param name="getCellHintTitle">获取显示标题的委托，如果为空则显示列标题</param>
        public static void RegisteGridCellHint<TEntity>(this GridControl gridControl, Func<GridHitInfo, bool> showTipsCondition, Func<TEntity, string> getCellHintText, Func<GridView, int, GridColumn, string> getCellHintTitle = null) where TEntity : class
        {
            RegisteGridCellHint(gridControl, showTipsCondition,
                (view, rowHandler, column) => getCellHintText(view.GetRow(rowHandler) as TEntity),
                getCellHintTitle
                );
        }
        /// <summary>
        /// 向Grid注册单元格中的提示信息
        ///    用户可根据表格，列，行来指定提示内容
        /// </summary>
        /// <param name="gridControl">要注册的Grid</param>
        /// <param name="showTipsCondition">显示提示信息的条件</param>
        /// <param name="getCellHintText">获取显示文本的委托</param>
        /// <param name="getCellHintTitle">获取显示标题的委托，如果为空则显示列标题</param>
        public static void RegisteGridCellHint(this GridControl gridControl, Func<GridHitInfo, bool> showTipsCondition, Func<GridView, int, GridColumn, string> getCellHintText, Func<GridView, int, GridColumn, string> getCellHintTitle = null)
        {
            if (gridControl == null)
                throw new ArgumentNullException(nameof(gridControl));
            if (showTipsCondition == null)
                throw new ArgumentNullException(nameof(showTipsCondition));
            if (getCellHintText == null)
                throw new ArgumentNullException(nameof(getCellHintText));

            var toolTipController = gridControl.ToolTipController;
            if (toolTipController == null)
            {
                toolTipController = new ToolTipController { ToolTipType = ToolTipType.SuperTip };
                gridControl.ToolTipController = toolTipController;
            }

            toolTipController.GetActiveObjectInfo += (sender, e) =>
            {
                if (e.SelectedControl != gridControl) return;
                var info = e.Info;
                try
                {
                    var view = gridControl.GetViewAt(e.ControlMousePosition) as GridView;
                    if (view == null)
                        return;

                    var hi = view.CalcHitInfo(e.ControlMousePosition);
                    if (hi.InRowCell && showTipsCondition(hi))
                    {
                        var hintText = getCellHintText(view, hi.RowHandle, hi.Column);
                        var hintTitle = getCellHintTitle == null ? hi.Column.Caption : getCellHintTitle(view, hi.RowHandle, hi.Column);
                        info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), hintText, hintTitle);
                    }
                }
                finally
                {
                    e.Info = info;
                }
            };
        }
    }
}
