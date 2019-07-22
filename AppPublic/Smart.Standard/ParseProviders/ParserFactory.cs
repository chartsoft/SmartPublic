using System;
using System.Collections.Generic;

namespace Smart.Standard.ParseProviders
{
    /// <summary>
    /// 序列化提供者工厂
    /// </summary>
    public class ParserFactory
    {

        private static Dictionary<string, IParseProvider> _providers;
        //当前支持的所有提供者
        private static Dictionary<string, IParseProvider> Providers
        {
            get
            {
                if (_providers != null) return _providers;
                lock (typeof(ParserFactory))
                {
                    _providers = new Dictionary<string, IParseProvider>();
                    //Json
                    IParseProvider jsonProvider = new JsonParseProvider();
                    _providers[jsonProvider.Name] = jsonProvider;
                    //XML
                    IParseProvider xmlProvider = new XmlParseProvider();
                    _providers[xmlProvider.Name] = xmlProvider;
                }
                return _providers;
            }
        }

        /// <summary>
        /// 获取对应providerName的IParseProvider接口实例对象
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="noParserThrow">不存在对应名称的解析器，true则抛出异常，false则返回null</param>
        /// <returns>对应providerName的IParseProvider接口实例对象</returns>
        public static IParseProvider GetParser(string providerName, bool noParserThrow = false)
        {
            if (noParserThrow && !Providers.ContainsKey(providerName))
                throw new Exception($"不支持解析提供器{providerName}");
            return Providers.ContainsKey(providerName) ? Providers[providerName] : null;
        }
        /// <summary>
        /// Json解析器
        /// </summary>
        public static IParseProvider JsonParser => Providers[JsonParseProvider.ProviderName];

        /// <summary>
        /// Xml解析器
        /// </summary>
        public static IParseProvider XmlParser => Providers[XmlParseProvider.ProviderName];
    }
}
