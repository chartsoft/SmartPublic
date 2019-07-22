using System;
using System.Collections.Generic;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using Smart.Win.Helpers;
using Smart.Win.Supports.TreeSupport;
using SmartSolution.Utilities.Win;

namespace Smart.Win.Extends
{
    /// <summary>
    /// XtraTree扩展类
    /// </summary>
    public static class TreeListExtends
    {
        /// <summary>
        /// 设置TreeList行高
        /// </summary>
        public static void SetRowHeight(this TreeList tree)
        {
            tree.ColumnPanelRowHeight = 30;
            tree.RowHeight = 24;
            tree.OptionsSelection.EnableAppearanceFocusedCell = false;
            tree.OptionsSelection.EnableAppearanceFocusedRow = true;
            tree.OptionsView.FocusRectStyle = DrawFocusRectStyle.CellFocus;
        }



        /// <summary>
        /// 设置边框
        /// </summary>
        /// <param name="tree">树表格</param>
        /// <param name="showBorder">是否显示边框</param>
        public static void SetBorder(this TreeList tree, bool showBorder = false)
        {
            tree.BorderStyle = showBorder ? BorderStyles.Default : BorderStyles.NoBorder;
        }

        /// <summary>
        /// 初始化TreeList筛选器
        /// </summary>
        public static T GetFocusData<T>(this TreeList tree) where T : TreeSupport<T>
        {
            var data = tree?.FocusedNode?.GetTag<T>(WinUtilityConsts.TreeListNodeBindDataTagKey);
            return data;
        }

        /// <summary>
        /// 初始化TreeList筛选器
        /// </summary>
        public static List<T> GetTreeDatas<T>(this TreeList tree) where T : TreeSupport<T>
        {
            return tree?.GetTag<List<T>>(WinUtilityConsts.TreeListBindDatasTagKey);
        }
        /// <summary>
        /// 初始化TreeList筛选器
        /// </summary>
        public static void FilterInit(this TreeList tree, TextEdit editor, bool immediate = true)
        {
            var filter = tree.GetTag<TreeFilterHelper>(WinUtilityConsts.TreeListFilterTagKey);
            if (filter != null) return;
            filter = new TreeFilterHelper(tree, editor, immediate);
            tree.SetTag(WinUtilityConsts.TreeListFilterTagKey, filter);
        }

        /// <summary>
        /// TreeList筛选文字
        /// </summary>
        public static void FilterText(this TreeList tree, string text)
        {
            var filter = tree.GetTag<TreeFilterHelper>(WinUtilityConsts.TreeListFilterTagKey);
            filter?.FilterText(text);
        }

        /// <summary>
        /// 清除TreeList筛选
        /// </summary>
        public static void FilterClear(this TreeList tree)
        {
            var filter = tree.GetTag<TreeFilterHelper>(WinUtilityConsts.TreeListFilterTagKey);
            filter?.ClearFilter();
        }

        /// <summary>
        /// 为TreeList绑定PopupMenu
        /// </summary>
        public static void BindPopup<T>(this TreeList tree, PopupMenu popup, Predicate<T> func = null) where T : TreeSupport<T>
        {
            var helper = tree.GetTag<TreeListPopupHelper<T>>(WinUtilityConsts.TreeListPopupMenuHelperTagKey);
            if (helper == null)
            {
                helper = new TreeListPopupHelper<T>();
                tree.SetTag(WinUtilityConsts.TreeListPopupMenuHelperTagKey, helper);
            }
            helper.BindPopupMenu(tree, popup, func);
        }

        /// <summary>
        /// 设置gridview格式
        /// </summary>
        public static void SetStyle(this TreeList tree, bool editable = false, params TreeListColumn[] columns)
        {
            if (!editable)
                TreeStyleHelper.SetXtraTreeStyle(tree, false);
            else
                TreeStyleHelper.SetXtraTreeStyle(tree, columns);
        }



    }
}
