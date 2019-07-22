using Smart.Standard.Consts;
using Smart.Standard.Helper;

namespace Smart.Standard.ParseProviders
{
    /// <summary>
    /// Xml序列化提供者
    /// </summary>
    public class XmlParseProvider : IParseProvider
    {

        /// <summary>
        /// Xml序列化提供者
        /// </summary>
        public static string ProviderName => SmartConsts.XmlParseProviderName;

        /// <summary>
        /// 打包
        /// </summary>
        public string Pack<T>(T data)
        {
            return SerilizeHelper.SerilizeToXml(data);
        }

        /// <inheritdoc />
        /// <summary>
        /// 解析
        /// </summary>
        public T Parse<T>(string value)
        {
            return SerilizeHelper.DeserilizeXml<T>(value);
        }
        /// <inheritdoc />
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Format(string str)
        {
            return SerilizeHelper.FormatXml(str);
        }


        /// <inheritdoc />
        /// <summary>
        /// Xml序列化提供者
        /// </summary>
        public string Name => ProviderName;
    }
}
