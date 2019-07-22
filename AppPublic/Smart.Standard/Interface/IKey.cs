namespace Smart.Standard.Interface
{

    /// <summary>
    /// 键支持
    /// <remarks>
    /// 此接口用于实体，将ID主键都转为string主键
    /// </remarks>
    /// </summary>
    public interface IKey
    {
        /// <summary>
        /// 主键
        /// </summary>
        string Key { get; }
    }

}
