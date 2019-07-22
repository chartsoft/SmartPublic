namespace Smart.Win.Supports.TreeSupport
{
    /// <summary>
    /// 树状支持
    /// </summary>
    public interface ITreeSupport
    {
        /// <summary>
        /// 节点父ID
        /// </summary>
        string NodeParentId { get; }
        /// <summary>
        /// 节点ID
        /// </summary>
        string NodeId { get; }
        /// <summary>
        /// 节点层级
        /// </summary>
        int NodeLevel { get; }     
        /// <summary>
        /// 设置节点层级
        /// </summary>
        /// <param name="nodeLevel">节点层级</param>
        void SetNodeLevel(int nodeLevel);

    }

}
