using System;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraGrid.Views.WinExplorer;
using Smart.Net45.Interface;
using Smart.Win.Enums;
using Smart.Win.Helpers;
using Smart.Win.Supports;

namespace Smart.Win.Extends
{
    /// <summary>
    /// ColumnView辅助类
    /// </summary>
    public static partial class ColumnViewExtends
    {

        /// <summary>
        /// 获取View类型
        /// </summary>
        /// <param name="view"><see cref="ColumnView"/></param>
        /// <returns><see cref="ColumnViewKinds"/>枚举值</returns>
        public static ColumnViewKinds GetViewKind(this ColumnView view)
        {
            if (view is AdvBandedGridView) return ColumnViewKinds.AdvBandedGridView;
            if (view is BandedGridView) return ColumnViewKinds.BandedGridView;
            if (view is GridView) return ColumnViewKinds.GridView;
            if (view is LayoutView) return ColumnViewKinds.LayoutView;
            if (view is CardView) return ColumnViewKinds.CardView;
            if (view is TileView) return ColumnViewKinds.TileView;
            if (view is WinExplorerView) return ColumnViewKinds.WinExplorerView;
            return ColumnViewKinds.ColumnView;
        }

        /// <summary>
        /// 取Grid选中数据
        /// </summary>
        public static T GetFocusData<T>(this ColumnView view) where T : class
        {
            return view?.FocusedRowHandle >= 0 ? view.GetRow(view.FocusedRowHandle) as T : default(T);
        }

        /// <summary>
        /// 绑定行右键弹出菜单
        /// </summary>
        /// <param name="view">表格</param>
        /// <param name="popup">弹出菜单</param>
        /// <param name="beforeShow">显示前处理</param>
        public static void BindPopup<T>(this ColumnView view, PopupMenu popup, BaseHitInfoModelPredicate<T> beforeShow = null) where T : class, IKey
        {
            var helper = view.GetTag<ColumnViewPopupHelper<T>>(WinUtilityConsts.ColumnViewPopupMenuHelperTagKey);
            if (helper == null)
            {
                helper = new ColumnViewPopupHelper<T>();
                view.SetTag(WinUtilityConsts.ColumnViewPopupMenuHelperTagKey, helper);
            }
            helper.BindPopupMenu(view, popup, beforeShow);
        }

        /// <summary>
        /// 解绑右键菜单
        /// </summary>
        /// <param name="view">表格</param>
        /// <param name="popup">弹出菜单</param>
        public static void UnbindPopup<T>(this ColumnView view, PopupMenu popup) where T : class, IKey
        {
            var helper = view?.GetTag<ColumnViewPopupHelper<T>>(WinUtilityConsts.ColumnViewPopupMenuHelperTagKey);
            helper?.UnbindPopupMenu(view, popup);
        }

        #region [CastViewKind]

        /// <summary>
        /// 转换为GridView
        /// </summary>
        /// <param name="view"><see cref="ColumnView"/></param>
        /// <param name="throwException">是否抛出异常</param>
        /// <returns><see cref="GridView"/>实例，不抛出异常，则转换失败返回null</returns>
        public static GridView ToGridView(this ColumnView view, bool throwException = true)
        {
            if (view is GridView gridView) return gridView;
            if (!throwException) return null;
            throw new Exception( "ColumnView转GridView");
        }

        /// <summary>
        /// 转换为 BandedGridView
        /// </summary>
        /// <param name="view"><see cref="ColumnView"/></param>
        /// <param name="throwException">是否抛出异常</param>
        /// <returns><see cref="BandedGridView"/>实例，不抛出异常，则转换失败返回null</returns>
        public static BandedGridView ToBandedGridView(this ColumnView view, bool throwException = true)
        {
            if (view is BandedGridView gridView) return gridView;
            if (!throwException) return null;
            throw new Exception( "ColumnView转BandedGridView");
        }
        /// <summary>
        /// 转换为AdvBandedGridView
        /// </summary>
        /// <param name="view"><see cref="ColumnView"/></param>
        /// <param name="throwException">是否抛出异常</param>
        /// <returns><see cref="AdvBandedGridView"/>实例，不抛出异常，则转换失败返回null</returns>
        public static AdvBandedGridView ToAdvBandedGridView(this ColumnView view, bool throwException = true)
        {
            if (view is AdvBandedGridView gridView) return gridView;
            if (!throwException) return null;
            throw new Exception("ColumnView转AdvBandedGridView");

        }
        /// <summary>
        /// 转换为LayoutView
        /// </summary>
        /// <param name="view"><see cref="ColumnView"/></param>
        /// <param name="throwException">是否抛出异常</param>
        /// <returns><see cref="LayoutView"/>实例，不抛出异常，则转换失败返回null</returns>
        public static LayoutView ToLayoutView(this ColumnView view, bool throwException = true)
        {
            if (view is LayoutView gridView) return gridView;
            if (!throwException) return null;
            throw new Exception("ColumnView转LayoutView");
        }
        /// <summary>
        /// 转换为CardView
        /// </summary>
        /// <param name="view"><see cref="ColumnView"/></param>
        /// <param name="throwException">是否抛出异常</param>
        /// <returns><see cref="CardView"/>实例，不抛出异常，则转换失败返回null</returns>
        public static CardView ToCardView(this ColumnView view, bool throwException = true)
        {
            if (view is CardView gridView) return gridView;
            if (!throwException) return null;
            throw new Exception("ColumnView转CardView");
        }
        /// <summary>
        /// 转换为TileView
        /// </summary>
        /// <param name="view"><see cref="ColumnView"/></param>
        /// <param name="throwException">是否抛出异常</param>
        /// <returns><see cref="TileView"/>实例，不抛出异常，则转换失败返回null</returns>
        public static TileView ToTileView(this ColumnView view, bool throwException = true)
        {
            if (view is TileView gridView) return gridView;
            if (!throwException) return null;
            throw new Exception("ColumnView转TileView");
        }
        /// <summary>
        /// 转换为WinExplorerView
        /// </summary>
        /// <param name="view"><see cref="ColumnView"/></param>
        /// <param name="throwException">是否抛出异常</param>
        /// <returns><see cref="WinExplorerView"/>实例，不抛出异常，则转换失败返回null</returns>
        public static WinExplorerView ToWinExplorerView(this ColumnView view, bool throwException = true)
        {
            if (view is WinExplorerView gridView) return gridView;
            if (!throwException) return null;
            throw new Exception("ColumnView转WinExplorerView");
        }


        #endregion

    }
}
