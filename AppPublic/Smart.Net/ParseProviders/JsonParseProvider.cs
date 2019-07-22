using Smart.Net45.Consts;
using Smart.Net45.Helper;

namespace Smart.Net45.ParseProviders
{
    /// <inheritdoc />
    /// <summary>
    /// Json序列化提供者
    /// </summary>
    public class JsonParseProvider : IParseProvider
    {

        /// <summary>
        /// Json序列化提供者
        /// </summary>
        public static string ProviderName => SmartConsts.JsonParseProviderName;

        /// <inheritdoc />
        /// <summary>
        /// 打包
        /// </summary>
        public string Pack<T>(T data)
        {
            return SerilizeHelper.SerilizeToJson(data);
        }

        /// <inheritdoc />
        /// <summary>
        /// 解析
        /// </summary>
        public T Parse<T>(string value)
        {
            return SerilizeHelper.DeserilizeJson<T>(value);
        }
        /// <inheritdoc />
        /// <summary>
        /// 格式化json字符串
        /// </summary>
        /// <param name="jsonstr"></param>
        /// <returns></returns>
        public string Format(string jsonstr)
        {
            return SerilizeHelper.FormatJson(jsonstr);
        }
        /// <inheritdoc />
        /// <summary>
        /// Json序列化提供者
        /// </summary>
        public string Name => ProviderName;
    }
}
