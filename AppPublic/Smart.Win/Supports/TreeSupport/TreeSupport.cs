using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Smart.Win.Supports.TreeSupport
{
    /// <summary>
    /// 树支持
    /// </summary>
    [Serializable]
    public abstract class TreeSupport<T> : ITreeSupport<T> where T : TreeSupport<T>
    {
        private List<T> _children;
        /// <summary>
        /// 子节点列表
        /// </summary>
        [XmlIgnore]
        public List<T> Children => _children ?? (_children = new List<T>());

        /// <summary>
        /// 父节点
        /// </summary>
        [XmlIgnore]
        public T Parent { get; private set; }

        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="parent">父节点</param>
        public void SetParent(T parent)
        {
            Parent = parent;
        }

        /// <summary>
        /// 父节点Id
        /// </summary>
        public abstract string NodeParentId { get; }

        /// <summary>
        /// 节点Id
        /// </summary>
        public abstract string NodeId { get; }

        /// <summary>
        /// 是否有孩子节点
        /// </summary>
        public bool HaveChild => _children != null && _children.Any();

        /// <summary>
        /// 是否有父亲节点
        /// </summary>
        public bool HaveParent => Parent != null;

        /// <summary>
        /// 子节点数量
        /// </summary>
        public int ChildNum => _children == null ? 0 : _children.Count;

        /// <summary>
        /// 节点层级
        /// </summary>
        public int NodeLevel { get; private set; }
        /// <summary>
        /// 设置节点层级
        /// </summary>
        /// <param name="nodeLevel">节点层级</param>
        public void SetNodeLevel(int nodeLevel)
        {
            NodeLevel = nodeLevel;
        }

    }
}
