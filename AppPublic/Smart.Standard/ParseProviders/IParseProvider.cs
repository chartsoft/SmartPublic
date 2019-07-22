namespace Smart.Standard.ParseProviders
{
    /// <summary>
    /// 解析提供者接口
    /// </summary>
    public interface IParseProvider
    {
        /// <summary>
        /// 提供者名称
        /// </summary>
        string Name { get;}
        /// <summary>
        /// 封包对象
        /// </summary>
        /// <typeparam name="T">要封包的对象类型</typeparam>
        /// <param name="data">要封包的对象</param>
        /// <returns>封包后的string字符串</returns>
        string Pack<T>(T data);
        /// <summary>
        /// 解包对象
        /// </summary>
        /// <typeparam name="T">解包后的对象类型</typeparam>
        /// <param name="value">要解包的字符串</param>
        /// <returns>解包后的对象</returns>
        T Parse<T>(string value);
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string Format(string str);
    }
}
