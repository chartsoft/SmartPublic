using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile;
using SmartSolution.Utilities.Supports;
using SmartSolution.Utilities.Win.Helpers;

namespace SmartSolution.Utilities.Win.Extends
{
    /// <summary>
    /// XtraGrid TileView辅助类
    /// </summary>
    public static class TileViewExtends
    {
        /// <summary>
        /// 取Grid选中数据
        /// </summary>
        public static T GetFocusData<T>(this TileView view) where T : class
        {
            return view.FocusedRowHandle >= 0 ? view.GetRow(view.FocusedRowHandle) as T : default(T);
        }
        /// <summary>
        /// 绑定行右键弹出菜单
        /// </summary>
        /// <param name="view">表格</param>
        /// <param name="popup">弹出菜单</param>
        /// <param name="beforeShow">显示前处理</param>
        public static void BindPopup<T>(this TileView view, PopupMenu popup, Predicate<T> beforeShow = null) where T : class, IKey
        {
            var helper = view.GetTag<TileViewPopupHelper<T>>(WinUtilityConsts.TileViewPopupMenuHelperTagKey);
            if (helper == null)
            {
                helper = new TileViewPopupHelper<T>();
                view.SetTag(WinUtilityConsts.TileViewPopupMenuHelperTagKey, helper);
            }
            helper.BindPopupMenu(view, popup, beforeShow);
        }

        /// <summary>
        /// 解绑右键菜单
        /// </summary>
        /// <param name="view">表格</param>
        /// <param name="popup">弹出菜单</param>
        public static void UnbindPopup<T>(this TileView view, PopupMenu popup) where T : class, IKey
        {
            var helper = view?.GetTag<TileViewPopupHelper<T>>(WinUtilityConsts.TileViewPopupMenuHelperTagKey);
            helper?.UnbindPopupMenu(view, popup);
        }

    }
}
