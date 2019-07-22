using System.Collections.Generic;

namespace Smart.Win.Supports.TreeSupport
{
    /// <summary>
    /// 树状支持
    /// </summary>
    public interface ITreeSupport<T> : ITreeSupport
    {

        /// <summary>
        /// 子节点
        /// </summary>
        List<T> Children { get; }
        /// <summary>
        /// 父节点
        /// </summary>
        T Parent { get; }
        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="parent">父节点</param>
        void SetParent(T parent);
    }
}
