using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace Smart.Win.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class TreeFilterHelper
    {

        #region [Field]

        private readonly TreeList _tree;
        private readonly TextEdit _edit;
        private readonly List<TreeListNode> _collapseNodes = new List<TreeListNode>();

        #endregion

        #region [Public]

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tree">要实现筛选功能的TreeList控件</param>
        /// <param name="edit"></param>
        /// <param name="immediate"></param>
        public TreeFilterHelper(TreeList tree, TextEdit edit, bool immediate)
        {
            if (tree == null||edit == null) { return; }
            _tree = tree;
            _edit = edit;
            InitTreeList();
            if (immediate)
            {
                _edit.TextChanged += (sender, e) =>
                {
                    FilterText(_edit.Text.Trim());
                };
            }
        }

        private void InitTreeList()
        {
            _tree.OptionsBehavior.EnableFiltering = true;
            _tree.OptionsFilter.FilterMode = FilterMode.Extended;
            _tree.HideFindPanel();
        }

        /// <summary>
        /// 筛选文字
        /// </summary>
        /// <param name="text">要筛选的文字</param>
        public void FilterText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                ClearFilter();
            }
            else
            {
                if (_collapseNodes.Count == 0)
                {
                    if (_tree.HasChildren)
                    {
                        AppendCollapseNodes(_tree.Nodes);
                    }
                }
                _tree.SuspendLayout();
                _tree.ExpandAll();
                _tree.ApplyFindFilter(text);
                _tree.ResumeLayout();
            }
        }

        private void AppendCollapseNodes(TreeListNodes nodes)
        {
            var eor = nodes.GetEnumerator();
            while (eor.MoveNext())
            {
                var node = eor.Current as TreeListNode;
                if (node == null) { continue; }
                if (!node.Expanded)
                {
                    _collapseNodes.Add(node);
                }
                if (node.HasChildren)
                {
                    AppendCollapseNodes(node.Nodes);
                }
            }
        }
       
        /// <summary>
        /// 清除筛选
        /// </summary>
        public void ClearFilter()
        {
            _tree.ApplyFindFilter(string.Empty);
            if (_collapseNodes == null) return;
            _tree.SuspendLayout();
            _collapseNodes.ForEach(one => one.Expanded = false);
            _collapseNodes.Clear();
            _tree.ResumeLayout();
        }

        #endregion

    }
}
