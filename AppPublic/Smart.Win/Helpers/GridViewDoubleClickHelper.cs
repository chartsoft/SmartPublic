using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using Smart.Net45.Interface;
using Smart.Win.Extends;
using Smart.Win.Supports;
using SmartSolution.Utilities.Win;

namespace Smart.Win.Helpers
{

    /// <summary>
    /// GridView鼠标双击辅助类
    /// </summary>
    public class GridControlDoubleClickHelper<T> where T : class, IKey
    {

        private readonly Dictionary<string, Action<T>> _actionDic = new Dictionary<string, Action<T>>();
        /// <summary>
        /// 获取Action
        /// </summary>
        /// <param name="key">GridView的Name作为存储键</param>
        /// <returns>Action实例</returns>
        public Action<T> GetAction(string key)
        {
            return _actionDic.ContainsKey(key) ? _actionDic[key] : null;
        }
        #region [BindPopup]

        /// <summary>
        /// 绑定Grid双击事件
        /// </summary>
        /// <param name="view">GridView</param>
        /// <param name="action">操作</param>
        public void BindDoubleClick(GridView view, Action<T> action)
        {
            if (view == null || action == null) return;
            var key = view.Name;
            if (_actionDic.ContainsKey(key)) return;
            _actionDic[key] = action;
            view.MouseDown += GridViewMouseDown;
        }

        private void GridViewMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks < 2) return;
            if (e.Button != MouseButtons.Left) return;
            var gridView = sender as GridView;
            var hi = gridView?.CalcHitInfo(e.Location);
            if (hi == null || !hi.InRow) return;
            var data = gridView.GetFocusData<T>();
            if (data == null) return;
            //处理Action
            var helper = gridView.GetTag<GridControlDoubleClickHelper<T>>(WinUtilityConsts.GridViewDoubleClickHelperTagKey);
            if (helper == null) return;
            var action = helper.GetAction(gridView.Name);
            action(data);
        }

        /// <summary>
        /// 解绑行双击
        /// </summary>
        /// <param name="view">GridView控件</param>
        public void UnbindDoubleClick(GridView view)
        {
            if (view == null) return;
            var key = view.Name;
            if (!_actionDic.ContainsKey(key)) return;
            _actionDic.Remove(key);
            view.MouseDown -= GridViewMouseDown;
        }

        #endregion

    }
}