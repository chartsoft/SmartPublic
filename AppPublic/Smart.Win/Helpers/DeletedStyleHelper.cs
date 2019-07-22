using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using Smart.Net45.Helper;

namespace Smart.Win.Helpers
{

    /// <summary>
    /// 文字删除样式的帮助类
    /// </summary>
    public class DeletedStyleHelper
    {
        /// <summary>
        /// 向Grid注册删除样式的显示
        /// </summary>
        /// <typeparam name="T">数据源类型</typeparam>
        /// <param name="gridView">要显示删除样式的表格</param>
        /// <param name="funcIsDeleted">范围数据是否被删除的委托</param>
        /// <param name="fieldName">要显示删除样式的列字段名</param>
        /// <returns></returns>
        public static GridDeletedStyleHelper<T> RegisterGrid<T>(GridView gridView, Predicate<T> funcIsDeleted, string fieldName) where T : class
        {
            return new GridDeletedStyleHelper<T>(gridView, funcIsDeleted, fieldName);
        }

        /// <summary>
        /// 向Grid注册删除样式的显示
        /// </summary>
        /// <typeparam name="T">数据源类型</typeparam>
        /// <param name="gridView">要显示删除样式的表格</param>
        /// <param name="funcIsDeleted">范围数据是否被删除的委托</param>
        /// <param name="column">要显示删除样式的列</param>
        /// <returns></returns>
        public static GridDeletedStyleHelper<T> RegisterGrid<T>(GridView gridView, Predicate<T> funcIsDeleted, GridColumn column) where T : class
        {
            return new GridDeletedStyleHelper<T>(gridView, funcIsDeleted, column.FieldName);
        }

        /// <summary>
        /// 向TreeList注册删除样式的显示
        /// </summary>
        /// <typeparam name="T">数据源类型</typeparam>
        /// <param name="treeList">要显示删除样式的TreeList</param>
        /// <param name="funcIsDeleted">范围数据是否被删除的委托</param>
        /// <param name="fieldName">要显示删除样式的列字段名</param>
        /// <returns></returns>
        public static TreeDeletedStyleHelper<T> RegisterTreeList<T>(TreeList treeList, Predicate<T> funcIsDeleted, string fieldName) where T : class
        {
            return new TreeDeletedStyleHelper<T>(treeList, funcIsDeleted, fieldName);
        }

        /// <summary>
        /// 向TreeList注册删除样式的显示
        /// </summary>
        /// <typeparam name="T">数据源类型</typeparam>
        /// <param name="treeList">要显示删除样式的TreeList</param>
        /// <param name="funcIsDeleted">范围数据是否被删除的委托</param>
        /// <param name="column">要显示删除样式的列</param>
        /// <returns></returns>
        public static TreeDeletedStyleHelper<T> RegisterTreeList<T>(TreeList treeList, Predicate<T> funcIsDeleted, TreeListColumn column) where T : class
        {
            return new TreeDeletedStyleHelper<T>(treeList, funcIsDeleted, column.FieldName);
        }
    }

    /// <summary>
    /// 设置Grid单元格文字删除样式的帮助类
    /// </summary>
    /// <typeparam name="T">数据源类型</typeparam>
    public class GridDeletedStyleHelper<T>
    {
        private readonly string _setColumnFieldName; // 需要设置样式的列名
        private readonly GridView _gridView;
        private readonly GridControl _gridControl;
        private readonly Predicate<T> _funcIsDeleted;
        private readonly bool _setAllColumn; // 是否所有列都需要设置样式
        private Font _strikeoutFont; // 带删除线的字体，缓存下来，避免多次创建

        /// <summary>
        /// 设置Grid单元格文字删除样式的帮助类
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="funcIsDeleted"></param>
        /// <param name="fieldName"></param>
        public GridDeletedStyleHelper(GridView gridView, Predicate<T> funcIsDeleted, string fieldName = "")
        {
            ArgumentGuard.ArgumentNotNull("gridView", gridView);
            ArgumentGuard.ArgumentNotNull("funcIsDeleted", funcIsDeleted);

            _gridView = gridView;
            _gridControl = gridView.GridControl;
            _funcIsDeleted = funcIsDeleted;
            _setAllColumn = string.IsNullOrWhiteSpace(fieldName);
            _setColumnFieldName = fieldName;

            gridView.CustomDrawCell -= GridView_CustomDrawCell;
            gridView.CustomDrawCell += GridView_CustomDrawCell;
        }

        private void GridView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            var dataSource = GetDataSource();
            if (dataSource != null && e.RowHandle >= 0)
            {
                var dataSourceHandler = _gridView.GetDataSourceRowIndex(e.RowHandle);
                if (dataSourceHandler >= 0 && dataSourceHandler < dataSource.Count)
                {
                    var data = dataSource[dataSourceHandler];
                    if (_funcIsDeleted(data) && ColumnNeedSet(e.Column))
                    {
                        if (_strikeoutFont == null)
                            _strikeoutFont = new Font(e.Appearance.Font, FontStyle.Strikeout);
                        e.Appearance.Font = _strikeoutFont;

                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns></returns>
        private IList<T> GetDataSource()
        {
            var dataSource = _gridControl.DataSource;

            if (dataSource is IList<T>)
                return (IList<T>)dataSource;
            if (dataSource is IEnumerable<T>)
                return ((IEnumerable<T>)dataSource).ToList();
            if (dataSource is IEnumerable)
                return ((IEnumerable)dataSource).OfType<T>().ToList();

            return new List<T>();
        }

        /// <summary>
        /// 列是否需要设置样式
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool ColumnNeedSet(GridColumn column)
        {
            return _setAllColumn || column.FieldName == _setColumnFieldName;
        }
    }

    /// <summary>
    /// 设置TreeList单元格文字删除样式的帮助类
    /// </summary>
    /// <typeparam name="T">数据源类型</typeparam>
    public class TreeDeletedStyleHelper<T> where T : class
    {
        private readonly string _setColumnFieldName;
        private readonly Predicate<T> _funcIsDeleted;
        private readonly bool _setAllColumn;
        private Font _strikeoutFont;

        /// <summary>
        /// 设置TreeList单元格文字删除样式的帮助类
        /// </summary>
        public TreeDeletedStyleHelper(TreeList treeList, Predicate<T> funcIsDeleted, string fieldName = "")
        {
            ArgumentGuard.ArgumentNotNull("treeList", treeList);
            ArgumentGuard.ArgumentNotNull("funcIsDeleted", funcIsDeleted);

            _funcIsDeleted = funcIsDeleted;
            _setAllColumn = string.IsNullOrWhiteSpace(fieldName);
            _setColumnFieldName = fieldName;

            treeList.CustomDrawNodeCell += TreeList_CustomDrawNodeCell;
            treeList.CustomDrawNodeCell += TreeList_CustomDrawNodeCell;
        }

        private void TreeList_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            var data = e.Node.Tag as T;
            if (data != null && _funcIsDeleted(data) && ColumnNeedSet(e.Column))
            {
                if (_strikeoutFont == null)
                    _strikeoutFont = new Font(e.Appearance.Font, FontStyle.Strikeout);
                e.Appearance.Font = _strikeoutFont;

                e.Appearance.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 列是否需要设置样式
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool ColumnNeedSet(TreeListColumn column)
        {
            return _setAllColumn || column.FieldName == _setColumnFieldName;
        }
    }
}
