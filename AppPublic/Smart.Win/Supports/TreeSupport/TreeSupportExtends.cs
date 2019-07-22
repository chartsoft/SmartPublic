using System;
using System.Collections.Generic;
using System.Linq;

namespace Smart.Win.Supports.TreeSupport
{
    /// <summary>
    /// 树状支持辅类
    /// </summary>
    public static class TreeSupportExtends
    {

        #region [GetRoots]{GetRoots}

        /// <summary>
        /// 取得根节点
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static List<T> GetRoots<T>(this List<T> dataList) where T : TreeSupport<T>
        {
            if (dataList == null || dataList.Count <= 0) return new List<T>();
            dataList.InitTree();
            var result = dataList.FindAll(item => item.Parent == null);
            foreach (var data in result)
            {
                data.SetNodeLevel(1);
                SetNestChildLevel(data);
            }
            foreach (var data in dataList)
            {
                data.SetParent(null);
            }
            return result;
        }

        /// <summary>
        /// 取得根节点
        /// </summary>
        /// <param name="dataList">数据列表</param>
        /// <param name="func">是否返回数据，返回true，可在委托方法中预处理数据</param>
        /// <returns></returns>
        public static List<T> GetRoots<T>(this List<T> dataList, Predicate<T> func) where T : TreeSupport<T>
        {
            if (dataList == null || dataList.Count <= 0) return new List<T>();
            InitTree(dataList);
            var returnData = dataList.Where(data => func(data)).ToList();
            if (returnData.Count <= 0) return new List<T>();
            {
                var result = returnData.FindAll(item => item.Parent == null);
                foreach (var data in result)
                {
                    data.SetNodeLevel(1);
                    SetNestChildLevel(data);
                }
                foreach (var data in returnData)
                {
                    data.SetParent(null);
                }
                return result;
            }
        }

        private static void SetNestChildLevel<T>(T root) where T : TreeSupport<T>
        {
            if (root.Children == null || root.Children.Count <= 0) return;
            foreach (var child in root.Children)
            {
                child.SetNodeLevel(root.NodeLevel + 1);
                SetNestChildLevel(child);
            }
        }

        #endregion


        #region [InitTree]{InitTree,InitTreeForSerilize}

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="dataList">数据列表</param>
        public static List<T> InitTree<T>(this List<T> dataList) where T : TreeSupport<T>
        {
            if (dataList == null || dataList.Count == 0) return dataList;
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
            return dataList;
        }

        /// <summary>
        /// 提供无循环引用的可序列化树支持
        /// </summary>
        /// <param name="dataList">数据列表</param>
        public static List<T> InitTreeForSerilize<T>(this List<T> dataList) where T : TreeSupport<T>
        {
            if (dataList == null || dataList.Count <= 0) return new List<T>();
            InitTree(dataList);
            var roots = dataList.FindAll(t => t.Parent == null);
            var result = new List<T>();
            foreach (var root in roots)
            {
                result.Add(root);
                root.SetNodeLevel(1);
                GetNestChilds(result, root);
            }
            foreach (var data in dataList)
            {
                data.Children.Clear();
                data.SetParent(null);
            }
            dataList.Clear();
            dataList.AddRange(result);
            return dataList;
        }

        private static void GetNestChilds<T>(ICollection<T> result, T root) where T : TreeSupport<T>
        {
            if (root.Children == null || root.Children.Count <= 0) return;
            foreach (var child in root.Children)
            {
                child.SetNodeLevel(root.NodeLevel + 1);
                result.Add(child);
                GetNestChilds(result, child);
            }
        }

        #endregion

    }
}
