using System.Collections.Generic;

namespace Smart.Win.Supports.TreeSupport
{
    /// <summary>
    /// 树状支持辅类
    /// </summary>
    internal class TreeSupportHelper
    {

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="dataList">数据列表</param>
        internal static void InitTree<T>(List<T> dataList) where T : TreeSupport<T>
        {
            if (dataList == null || dataList.Count == 0) return;
            var nodeDic = new Dictionary<string, T>();
            foreach (var node in dataList)
            {
                nodeDic[node.NodeId] = node;
                node.Children.Clear();
            }
            foreach (var node in dataList)
            {
                if (node.NodeParentId == "" || !nodeDic.ContainsKey(node.NodeParentId)) continue;
                var parent = nodeDic[node.NodeParentId];
                parent.Children.Add(node);
                node.SetParent(parent);
            }
        }

    }



}
