using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.ViewInfo;
using Smart.Net45.Extends;
using Smart.Win.Extends;
using Smart.Win.Supports.TreeSupport;

namespace Smart.Win.Helpers
{

    /// <summary>
    /// XtraTree辅助类
    /// </summary>
    public class TreeListHelper
    {

        #region [BindXtraTree]

        /// <summary>
        /// 绑定树
        /// </summary>
        /// <typeparam name="T">数据列表</typeparam>
        /// <param name="tree">树控件</param>
        /// <param name="datas">数据</param>
        /// <param name="selectData">选中数据</param>
        /// <param name="expandLevel">展开层级，0折叠，1展开全部，2展开到第二级，3展开到第三级…</param>
        public static void BindTree<T>(TreeList tree, List<T> datas, T selectData, int expandLevel) where T : TreeSupport<T>
        {
            if (tree == null || datas == null) return;
            tree.BeginUpdate();
            var roots = GetRootDatas(datas);
            AppendNodes(tree, roots, expandLevel, null);
            SelectNode(tree, selectData);
            tree.SetTag(WinUtilityConsts.TreeListBindDatasTagKey, datas);
            tree.EndUpdate();
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        private static void AppendNodes<T>(TreeList tree, List<T> childs, int expandLevel, TreeListNode pNode) where T : TreeSupport<T>
        {
            if (tree == null || childs == null || childs.Count <= 0) return;
            var currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            tree.BeginUnboundLoad();
            foreach (var child in childs)
            {
                var colDatas = new List<object>();
                foreach (TreeListColumn col in tree.Columns)
                {
                    if (string.IsNullOrEmpty(col.FieldName)) continue;
                    var fieldValue = child.GetPropertyValue(col.FieldName);
                    colDatas.Add(fieldValue);
                }
                var node = tree.AppendNode(colDatas.ToArray(), pNode);
                node.SetTag(WinUtilityConsts.TreeListNodeBindDataTagKey,child);
                if (child.Children != null && child.Children.Count > 0)
                {
                    AppendNodes(tree, child.Children, expandLevel, node);
                }
                node.Expanded = (expandLevel == 1) || (expandLevel > child.NodeLevel);

            }
            tree.EndUnboundLoad();
            Cursor.Current = currentCursor;
        }

        private static List<T> GetRootDatas<T>(List<T> dataList) where T : TreeSupport<T>
        {
            if (dataList == null && dataList.Count <= 1) return dataList;
            var dataDic = new Dictionary<string, T>();
            foreach (var data in dataList)
            {
                dataDic[data.NodeId] = data;
                data.Children.Clear();
                data.SetParent(null);
            }
            foreach (var node in dataList)
            {
                if (!dataDic.ContainsKey(node.NodeParentId)) continue;
                dataDic[node.NodeParentId].Children.Add(node);
                node.SetParent(dataDic[node.NodeParentId]);
            }
            var roots = dataList.FindAll(data => data.Parent == null);
            foreach (var root in roots)
            {
                SetNodeLevel(root, 1);
            }
            return roots;
        }

        private static void SetNodeLevel<T>(T data, int level) where T : TreeSupport<T>
        {
            data.SetNodeLevel(level);
            level++;
            if (data.Children == null || data.Children.Count <= 0) return;
            foreach (var child in data.Children)
            {
                SetNodeLevel(child, level);
            }
        }

        #endregion

        #region [SelectNode]

        /// <summary>
        /// 选中节点数据
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="tree">树控件</param>
        /// <param name="data">选中数据</param>
        public static void SelectNode<T>(TreeList tree, T data) where T : TreeSupport<T>
        {
            if (data != null && !string.IsNullOrEmpty(data.NodeId))
            {
                SelectNode(tree, data.NodeId);
            }
        }

        /// <summary>
        /// 选中节点ID
        /// </summary>
        /// <param name="tree">树控件</param>
        /// <param name="nodeId">选中数据ID</param>
        public static void SelectNode(TreeList tree, string nodeId)
        {
            if (tree == null || tree.Nodes == null || tree.Nodes.Count <= 0 || string.IsNullOrEmpty(nodeId)) return;
            tree.BeginUpdate();
            SelectNodeNest(tree.Nodes, nodeId);
            tree.EndUpdate();
        }

        private static void SelectNodeNest(TreeListNodes nodes, string nodeId)
        {
            foreach (TreeListNode node in nodes)
            {
                var data = node.GetTag<ITreeSupport>(WinUtilityConsts.TreeListNodeBindDataTagKey);
                if (data == null) throw new InvalidOperationException("只有通过XtraTreeHelper绑定的树才能调用此方法选中节点");
                if (data.NodeId == nodeId)
                {
                    node.TreeList.FocusedNode = node;
                    return;
                }
                if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    SelectNodeNest(node.Nodes, nodeId);
                }
            }
        }

        #endregion

        #region [FindNode]

        /// <summary>
        /// 选中节点数据
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="tree">树控件</param>
        /// <param name="data">选中数据</param>
        public static TreeListNode FindNode<T>(TreeList tree, T data) where T : TreeSupport<T>
        {
            return FindNode(tree, data.NodeId);
        }

        /// <summary>
        /// 选中节点ID
        /// </summary>
        /// <param name="tree">树控件</param>
        /// <param name="nodeId">选中数据ID</param>
        public static TreeListNode FindNode(TreeList tree, string nodeId)
        {
            if (tree == null || tree.Nodes == null || tree.Nodes.Count <= 0 || string.IsNullOrEmpty(nodeId)) return null;
            tree.BeginUpdate();
            var findNode = FindNodeNest(tree.Nodes, nodeId);
            tree.EndUpdate();
            return findNode;
        }

        private static TreeListNode FindNodeNest(TreeListNodes nodes, string nodeId)
        {
            foreach (TreeListNode node in nodes)
            {
                var data = node.GetTag<ITreeSupport>(WinUtilityConsts.TreeListNodeBindDataTagKey);
                if (data == null) throw new InvalidOperationException("只有通过XtraTreeHelper绑定的树才能调用此方法选中节点");
                if (data.NodeId == nodeId) return node;
                if (node.Nodes == null || node.Nodes.Count <= 0) continue;
                var findNode = FindNodeNest(node.Nodes, nodeId);
                if (findNode != null) return findNode;
            }
            return null;
        }

        #endregion

        #region [ExpandLevel]

        /// <summary>
        /// 展开树
        /// </summary>
        /// <typeparam name="T">数据列表</typeparam>
        /// <param name="tree">树控件</param>
        /// <param name="expandLevel">展开层级，0折叠，1展开全部，2展开到第二级，3展开到第三级…</param>
        public static void ExpandTree<T>(TreeList tree, int expandLevel) where T : TreeSupport<T>
        {
            if (tree == null) { throw new ArgumentException("TreeList为空"); }
            tree.SuspendLayout();
            tree.BeginUpdate();
            if (expandLevel <= 0) { tree.CollapseAll(); }
            else if (expandLevel == 1) { tree.ExpandAll(); }
            else
            {
                ExpandTreeNest(tree.Nodes, expandLevel);
            }
            tree.EndUpdate();
            tree.ResumeLayout(true);
        }

        /// <summary>
        /// 遍历递归展开
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="expandLevel">展开层级</param>
        private static void ExpandTreeNest(TreeListNodes nodes, int expandLevel)
        {
            if (nodes == null || nodes.Count <= 0) return;
            foreach (TreeListNode node in nodes)
            {
                var data = node.GetTag<ITreeSupport>(WinUtilityConsts.TreeListNodeBindDataTagKey);
                if (data == null || data.NodeLevel >= expandLevel) continue;
                node.Expanded = true;
                if (node.Nodes != null && node.Nodes.Count > 0 && data.NodeLevel < expandLevel - 1)
                {
                    ExpandTreeNest(node.Nodes, expandLevel);
                }
                else if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    foreach (TreeListNode cNode in node.Nodes)
                    {
                        cNode.Expanded = false;
                    }
                }
            }
        }

        #endregion

        #region [AppendNode]

        /// <summary>
        /// 添加节点数据
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="tree">树控件</param>
        /// <param name="data">数据</param>
        /// <param name="afterWho">在哪个节点后</param>
        /// <param name="select">是否选中添加数据</param>
        public static TreeListNode AppendNode<T>(TreeList tree, T data, string afterWho, bool select) where T : TreeSupport<T>
        {
            if (tree == null) { throw new ArgumentException("TreeList为空"); }
            tree.BeginUpdate();
            TreeListNode parentNode = null;
            if (!string.IsNullOrEmpty(data.NodeParentId))
            {
                parentNode = FindNode(tree, data.NodeParentId);
            }
            var colDatas = new List<object>();
            foreach (TreeListColumn col in tree.Columns)
            {
                if (string.IsNullOrEmpty(col.FieldName)) continue;
                var fieldValue = data.GetPropertyValue(col.FieldName);
                colDatas.Add(fieldValue);
            }
            if (parentNode != null)
            {
                var pData = parentNode.GetTag<T>(WinUtilityConsts.TreeListNodeBindDataTagKey);
                if (pData != null)
                {
                    pData.Children.Add(data);
                    data.SetParent(pData);
                }
            }
            else
            {
                var dataSource = tree.GetTag<List<T>>(WinUtilityConsts.TreeListBindDatasTagKey);
                if (dataSource != null)
                {
                    dataSource.Add(data);
                }
            }
            var appendNode = tree.AppendNode(colDatas.ToArray(), parentNode);
            appendNode.SetTag(WinUtilityConsts.TreeListNodeBindDataTagKey,data);
            if (!string.IsNullOrEmpty(afterWho))
            {
                var afterWhoNode = FindNode(tree, afterWho);
                if (afterWhoNode != null && ReferenceEquals(afterWhoNode.ParentNode, parentNode))
                {
                    // 增加顶级节点处理
                    var nodes = afterWhoNode.ParentNode == null ? tree.Nodes : parentNode.Nodes;
                    var afterWhoIndex = nodes.IndexOf(afterWhoNode);
                    tree.SetNodeIndex(appendNode, afterWhoIndex + 1);
                }
            }
            if (select)
            {
                appendNode.Selected = true;
            }
            tree.Refresh();
            tree.EndUpdate();
            return appendNode;
        }

        #endregion

        #region [InsertNode]


        /// <summary>
        /// 插入节点数据
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="tree">树控件</param>
        /// <param name="data">数据</param>
        /// <param name="beforWho">在哪个节点前</param>
        /// <param name="select">是否选中添加数据</param>
        public static TreeListNode InsertNode<T>(TreeList tree, T data, string beforWho, bool select) where T : TreeSupport<T>
        {
            if (tree == null) { throw new ArgumentException("TreeList为空"); }
            tree.BeginUpdate();
            TreeListNode parentNode = null;
            if (!string.IsNullOrEmpty(data.NodeParentId))
            {
                parentNode = FindNode(tree, data.NodeParentId);
            }
            var colDatas = new List<object>();
            foreach (TreeListColumn col in tree.Columns)
            {
                if (string.IsNullOrEmpty(col.FieldName)) continue;
                var fieldValue = data.GetPropertyValue(col.FieldName);
                colDatas.Add(fieldValue);
            }
            if (parentNode != null)
            {
                var pData = parentNode.GetTag<T>(WinUtilityConsts.TreeListNodeBindDataTagKey);
                if (pData != null)
                {
                    pData.Children.Add(data);
                    data.SetParent(pData);
                }
            }
            else
            {
                var dataSource = tree.GetTag<List<T>>(WinUtilityConsts.TreeListBindDatasTagKey);
                if (dataSource != null)
                {
                    dataSource.Add(data);
                }
            }
            var appendNode = tree.AppendNode(colDatas.ToArray(), parentNode);
            appendNode.SetTag(WinUtilityConsts.TreeListNodeBindDataTagKey,data);
            if (!string.IsNullOrEmpty(beforWho))
            {
                var afterWhoNode = FindNode(tree, beforWho);
                if (afterWhoNode != null && ReferenceEquals(afterWhoNode.ParentNode, parentNode))
                {
                    // 增加顶级节点处理
                    var nodes = afterWhoNode.ParentNode == null ? tree.Nodes : parentNode.Nodes;
                    var afterWhoIndex = nodes.IndexOf(afterWhoNode);
                    tree.SetNodeIndex(appendNode, afterWhoIndex);
                }
            }
            if (select)
            {
                appendNode.Selected = true;
            }
            //tree.Refresh();
            tree.EndUpdate();
            return appendNode;
        }

        #endregion

        #region [UpdateNode]

        /// <summary>
        /// 更新节点
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="tree">树控件</param>
        /// <param name="data">数据</param>
        /// <param name="select">是否选中修改节点</param>
        public static TreeListNode UpdateNode<T>(TreeList tree, T data, bool select) where T : TreeSupport<T>
        {
            if (tree == null || data == null || string.IsNullOrEmpty(data.NodeId)) return null;
            tree.BeginUpdate();
            var findNode = InnerUpdateNode(tree, data);
            tree.EndUpdate();
            return findNode;
        }

        #endregion

        #region UpdateNodeList

        /// <summary>
        /// 批量更新节点列表
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="tree">树控件</param>
        /// <param name="dataList">数据列表</param>
        public static void UpdateNodeList<T>(TreeList tree, List<T> dataList) where T : TreeSupport<T>
        {
            tree.BeginUpdate();
            foreach (var data in dataList) { InnerUpdateNode(tree, data); }
            tree.Refresh();
            tree.EndUpdate();
        }

        private static TreeListNode InnerUpdateNode<T>(TreeList tree, T data) where T : TreeSupport<T>
        {
            if (tree == null || data == null || string.IsNullOrEmpty(data.NodeId)) return null;
            var findNode = FindNode(tree, data.NodeId);
            if (findNode == null) return null;
            var oldData = findNode.GetTag<T>(WinUtilityConsts.TreeListNodeBindDataTagKey);
            if (oldData == null) { oldData = data; }
            else
            {
                data.SetNodeLevel(oldData.NodeLevel);
                data.SetParent(oldData.Parent);
                // 保存原节点的子节点，避免在data == oldData的情况下被清空掉
                var childrenNode = oldData.Children.ToList();
                data.Children.Clear();
                data.Children.AddRange(childrenNode);
                ReplaceNodeData(findNode, data, oldData);
            }
            foreach (TreeListColumn col in tree.Columns)
            {
                if (string.IsNullOrEmpty(col.FieldName)) continue;
                var fieldValue = data.GetPropertyValue(col.FieldName);
                oldData.SetPropertyValue(col.FieldName, fieldValue);
                findNode.SetValue(col.FieldName, fieldValue);
            }
            return findNode;
        }

        private static void ReplaceNodeData<T>(TreeListNode findNode, T data, T oldData) where T : TreeSupport<T>
        {
            if (oldData.Parent != null)
            {
                var index = oldData.Parent.Children.IndexOf(oldData);
                oldData.Parent.Children.RemoveAt(index);
                oldData.Parent.Children.Insert(index, data);
            }
            foreach (var childNode in data.Children)
            {
                childNode.SetParent(data);
            }
            findNode.SetTag(WinUtilityConsts.TreeListNodeBindDataTagKey, data);
        }

        #endregion

        #region [RemoveNode]

        /// <summary>
        /// 移除节点
        /// </summary>
        /// <param name="tree">树控件</param>
        /// <param name="nodeData">节点数据</param>
        public static void RemoveNode<T>(TreeList tree, T nodeData) where T : TreeSupport<T>
        {
            if (tree == null || nodeData == null || string.IsNullOrEmpty(nodeData.NodeId)) return;
            tree.BeginUpdate();
            var findNode = FindNode(tree, nodeData);
            if (findNode == null) return;
            if (findNode.ParentNode == null)
            {
                // 顶级节点
                var dataSource = tree.GetTag<IList>(WinUtilityConsts.TreeListBindDatasTagKey);
                var findData = findNode.GetTag<T>(WinUtilityConsts.TreeListNodeBindDataTagKey);
                if (dataSource != null && findData != null)
                    dataSource.Remove(findData);
                tree.Nodes.Remove(findNode);
            }
            else
            {
                var parentData = findNode.ParentNode.GetTag<T>(WinUtilityConsts.TreeListNodeBindDataTagKey);
                var findData = findNode.GetTag<T>(WinUtilityConsts.TreeListNodeBindDataTagKey);
                if (parentData != null && parentData.Children != null && findData != null) { parentData.Children.Remove(findData); }
                findNode.ParentNode.Nodes.Remove(findNode);
            }
            tree.EndUpdate();
        }

        #endregion

        #region [MoveUpNode]

        /// <summary>
        /// 上移节点
        /// </summary>
        /// <param name="tree">Tree控件</param>
        /// <param name="nodeData">上移节点ID</param>
        public static void MoveUpNode<T>(TreeList tree, T nodeData) where T : TreeSupport<T>
        {
            if (tree == null || nodeData == null || string.IsNullOrEmpty(nodeData.NodeId)) return;
            tree.BeginUpdate();
            var findNode = FindNode(tree, nodeData);
            if (findNode != null)
            {
                var nodes = findNode.ParentNode == null ? tree.Nodes : findNode.ParentNode.Nodes;
                var prevNode = findNode.PrevNode;
                if (prevNode != null && !ReferenceEquals(findNode, prevNode))
                {
                    var index = nodes.IndexOf(findNode);
                    var prevIndex = nodes.IndexOf(prevNode);
                    tree.SetNodeIndex(findNode, prevIndex);
                    tree.SetNodeIndex(prevNode, index);
                }
                findNode.Selected = true;
                MoveNodeDataUp<T>(findNode);
                tree.Refresh();
            }
            tree.EndUpdate();
        }

        /// <summary>
        /// 向下移动指定节点的数据
        /// </summary>
        /// <param name="findNode"></param>
        private static void MoveNodeDataUp<T>(TreeListNode findNode) where T : TreeSupport<T>
        {
            if (findNode.ParentNode == null) return;
            var parentData = findNode.ParentNode.GetTag<TreeSupport<T>>(WinUtilityConsts.TreeListNodeBindDataTagKey);
            if (parentData == null) return;
            var children = parentData.Children;
            var nodeData = findNode.GetTag<T>(WinUtilityConsts.TreeListNodeBindDataTagKey);
            if (children == null || nodeData == null) return;
            var index = children.IndexOf(nodeData);
            if (index <= 0) return;
            children.Move(x => x == nodeData, index - 1);
        }

        #endregion

        #region [MoveTop]

        /// <summary>
        /// 移动节点到顶部
        /// </summary>
        /// <param name="tree">Tree控件</param>
        /// <param name="nodeData">节点数据</param>
        public static void MoveTop<T>(TreeList tree, T nodeData) where T : TreeSupport<T>
        {
            if (tree == null || nodeData == null || string.IsNullOrWhiteSpace(nodeData.NodeId)) return;
            var nodeId = nodeData.NodeId;
            tree.BeginUpdate();
            var findNode = FindNode(tree, nodeId);
            if (findNode == null) return;

            var brotherNodes = findNode.ParentNode == null ? tree.Nodes : findNode.ParentNode.Nodes;
            var nodeIndex = brotherNodes.IndexOf(findNode);

            tree.SetNodeIndex(findNode, 0);
            foreach (var treeListNode in brotherNodes)
            {
                var node = treeListNode as TreeListNode;
                var index = tree.GetNodeIndex(node);
                if (index < nodeIndex && findNode != treeListNode) { tree.SetNodeIndex(node, index + 1); }
            }
            MoveNodeDataTop<T>(findNode);
            findNode.Selected = true;
            tree.Refresh();
            tree.EndUpdate();
        }

        /// <summary>
        /// 向下移动指定节点的数据
        /// </summary>
        /// <param name="findNode"></param>
        private static void MoveNodeDataTop<T>(TreeListNode findNode) where T : TreeSupport<T>
        {
            if (findNode.ParentNode == null) return;
            var parentData = findNode.ParentNode.GetTag<TreeSupport<T>>(WinUtilityConsts.TreeListNodeBindDataTagKey);
            if (parentData == null) return;
            var children = parentData.Children;
            var nodeData = findNode.GetTag<TreeSupport<T>>(WinUtilityConsts.TreeListNodeBindDataTagKey);
            if (children == null || nodeData == null) return;
            children.MoveToBeginning(x => x == nodeData);
        }

        #endregion

        #region [MoveBottom]

        /// <summary>
        /// 移动节点到底部
        /// </summary>
        /// <param name="tree">Tree控件</param>
        /// <param name="nodeData">上移节点ID</param>
        public static void MoveBottom<T>(TreeList tree, T nodeData) where T : TreeSupport<T>
        {
            if (tree == null || nodeData == null || string.IsNullOrEmpty(nodeData.NodeId)) return;
            tree.BeginUpdate();
            var findNode = FindNode(tree, nodeData);
            if (findNode == null) return;
            var brotherNodes = findNode.ParentNode == null ? tree.Nodes : findNode.ParentNode.Nodes;
            var nodeIndex = brotherNodes.IndexOf(findNode);
            tree.SetNodeIndex(findNode, brotherNodes.Count - 1);
            foreach (var treeListNode in brotherNodes)
            {
                var node = (TreeListNode)treeListNode;
                var index = tree.GetNodeIndex(node);
                if (index > nodeIndex && findNode != treeListNode) { tree.SetNodeIndex(node, index - 1); }
            }
            MoveNodeDataBottom<T>(findNode);
            findNode.Selected = true;
            tree.Refresh();
            tree.EndUpdate();
        }

        /// <summary>
        /// 移动指定节点的数据到底部
        /// </summary>
        /// <param name="findNode"></param>
        private static void MoveNodeDataBottom<T>(TreeListNode findNode) where T : TreeSupport<T>
        {
            if (findNode.ParentNode == null) return;
            var parentData = findNode.ParentNode.GetTag<T>(WinUtilityConsts.TreeListNodeBindDataTagKey);
            if (parentData == null) return;
            var children = parentData.Children;
            var nodeData = findNode.GetTag<T>(WinUtilityConsts.TreeListNodeBindDataTagKey);
            if (children == null || nodeData == null) return;
            children.MoveToEnd(x => x == nodeData);
        }

        #endregion

        #region [MoveDownNode]

        /// <summary>
        /// 下移节点
        /// </summary>
        /// <param name="tree">TreeList控件</param>
        /// <param name="nodeData">节点数据</param>
        public static void MoveDownNode<T>(TreeList tree, T nodeData) where T : TreeSupport<T>
        {
            if (tree == null || nodeData == null || string.IsNullOrEmpty(nodeData.NodeId)) return;
            tree.BeginUpdate();
            var findNode = FindNode(tree, nodeData);
            if (findNode == null) return;
            var nodes = findNode.ParentNode == null ? tree.Nodes : findNode.ParentNode.Nodes;
            var nextNode = findNode.NextNode;
            if (nextNode != null && !ReferenceEquals(findNode, nextNode))
            {
                var index = nodes.IndexOf(findNode);
                var nextIndex = nodes.IndexOf(nextNode);
                tree.SetNodeIndex(findNode, nextIndex);
                tree.SetNodeIndex(nextNode, index);
            }
            MoveNodeDataDown<T>(findNode);
            findNode.Selected = true;
            tree.Refresh();
            tree.EndUpdate();
        }

        /// <summary>
        /// 向下移动指定节点的数据
        /// </summary>
        /// <param name="findNode"></param>
        private static void MoveNodeDataDown<T>(TreeListNode findNode) where T : TreeSupport<T>
        {
            if (findNode.ParentNode == null) return;
            var parentData = findNode.ParentNode.GetTag<T>(WinUtilityConsts.TreeListNodeBindDataTagKey);
            if (parentData == null) return;
            var children = parentData.Children;
            var nodeData = findNode.GetTag<T>(WinUtilityConsts.TreeListNodeBindDataTagKey);
            if (children == null || nodeData == null) return;
            var index = children.IndexOf(nodeData);
            if (index < 0 || index == children.Count - 1) return;
            children.Move(x => x == nodeData, index + 1);
        }

        #endregion
        /// <summary>
        /// 固定列宽
        /// </summary>
        /// <param name="column">列</param>
        /// <param name="width">宽度</param>
        public static void FixTreeListColumnWidth(TreeListColumn column, int width)
        {
            column.MinWidth = column.Width = width;
            column.OptionsColumn.FixedWidth = true;
        }

        /// <summary>
        /// 向TreeList注册单元格中的提示信息
        ///    用户可根据节点，列来指定提示内容
        /// </summary>
        /// <typeparam name="TEntity">treeList上绑定的实体的类型</typeparam>
        /// <param name="treeList">要注册的TreeList</param>
        /// <param name="showTipsCondition">显示提示信息的条件</param>
        /// <param name="getCellHintText">获取显示文本的委托</param>
        /// <param name="getCellHintTitle">获取显示标题的委托，如果为空则显示列标题</param>
        public static void RegisteTreeCellHint<TEntity>(TreeList treeList, Func<TreeListHitInfo, bool> showTipsCondition, Func<TEntity, string> getCellHintText, Func<TreeListNode, TreeListColumn, string> getCellHintTitle = null) where TEntity : class
        {
            RegisteTreeCellHint(treeList, showTipsCondition,
                (node, column) => getCellHintText(treeList.GetDataRecordByNode(node) as TEntity), getCellHintTitle);
        }

        /// <summary>
        /// 向TreeList注册单元格中的提示信息
        ///    用户可根据节点，列来指定提示内容
        /// </summary>
        /// <param name="treeList">要注册的TreeList</param>
        /// <param name="showTipsCondition">显示提示信息的条件</param>
        /// <param name="getCellHintText">获取显示文本的委托</param>
        /// <param name="getCellHintTitle">获取显示标题的委托，如果为空则显示列标题</param>
        public static void RegisteTreeCellHint(TreeList treeList, Func<TreeListHitInfo, bool> showTipsCondition, Func<TreeListNode, TreeListColumn, string> getCellHintText, Func<TreeListNode, TreeListColumn, string> getCellHintTitle = null)
        {
            if (treeList == null)
                throw new ArgumentNullException(nameof(treeList));
            if (showTipsCondition == null)
                throw new ArgumentNullException(nameof(showTipsCondition));
            if (getCellHintText == null)
                throw new ArgumentNullException(nameof(getCellHintText));

            var toolTipController = treeList.ToolTipController;
            if (toolTipController == null)
            {
                toolTipController = new ToolTipController { ToolTipType = ToolTipType.SuperTip };
                treeList.ToolTipController = toolTipController;
            }

            toolTipController.GetActiveObjectInfo += (sender, e) =>
            {
                if (e.SelectedControl != treeList) return;
                var info = e.Info;
                try
                {
                    var hi = treeList.CalcHitInfo(e.ControlMousePosition);
                    if (hi.HitInfoType != HitInfoType.Cell || !showTipsCondition(hi)) return;
                    var hintText = getCellHintText(hi.Node, hi.Column);
                    var hintTitle = getCellHintTitle == null ? hi.Column.Caption : getCellHintTitle(hi.Node, hi.Column);
                    info = new ToolTipControlInfo(new TreeListCellToolTipInfo(hi.Node, hi.Column, null), hintText, hintTitle);
                }
                finally
                {
                    e.Info = info;
                }
            };
        }

    }
}