using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList;
using Smart.Win.Extends;
using Smart.Win.Supports.TreeSupport;


namespace Smart.Win.Helpers
{

    /// <summary>
    /// TreeList右键菜单辅助类
    /// </summary>
    public class TreeListPopupHelper<T> where T : TreeSupport<T>
    {

        private readonly Dictionary<PopupMenu, string> _popupDic = new Dictionary<PopupMenu, string>();

        private readonly Dictionary<string, Predicate<T>> _funcDic = new Dictionary<string, Predicate<T>>();

        #region [BindPopup]

        /// <summary>
        /// 绑定弹出右键菜单到树，此方法只支持一个树上注册一个PopupMenu
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="popup"></param>
        /// <param name="showPredicate">谓词委托，返回true显示右键，false不显示</param>
        public void BindPopupMenu(TreeList tree, PopupMenu popup, Predicate<T> showPredicate = null)
        {
            if (tree == null || popup == null)
                throw new Exception("BindPopupMenu时传入的tree或popup为空");
            var key = $"_key_{tree.Name}_{popup.Name}";
            if (_funcDic.ContainsKey(key)) return;
            _popupDic[popup] = key;
            _funcDic[key] = showPredicate;
            tree.MouseUp += TreeListMouseUp;
        }

        private void TreeListMouseUp(object sender, MouseEventArgs e)
        {
            var treeCtr = sender as TreeList;
            var hi = treeCtr?.CalcHitInfo(e.Location);
            if (hi?.Node == null || e.Button != MouseButtons.Right) return;
            hi.Node.Selected = true;
            var data = hi.Node.GetTag<T>(WinUtilityConsts.TreeListNodeBindDataTagKey);
            if (data == null) return;
            //处理Predicate
            var helper = treeCtr.GetTag<TreeListPopupHelper<T>>(WinUtilityConsts.TreeListPopupMenuHelperTagKey);
            if (helper == null) return;
            var eor = _popupDic.GetEnumerator();
            while (eor.MoveNext())
            {
                var popup = eor.Current.Key;
                var funcKey = eor.Current.Value;
                if (_funcDic.ContainsKey(funcKey) && _funcDic[funcKey] != null && _funcDic[funcKey](data))
                {
                    popup.ShowPopup(Control.MousePosition);
                    return;
                }
                popup.ShowPopup(Control.MousePosition);
                return;
            }
        }

        #endregion

    }
}