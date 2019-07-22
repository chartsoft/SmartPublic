using System;
using Microsoft.Extensions.Configuration;

namespace Smart.Standard.Core.AppConfig
{
    /// <summary>
    /// ConfigJson
    /// </summary>
    public class ConfigJson
    {
        /// <summary>
        /// 读配置文件的read
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AppsettingsRead(string key)
        {
         //return   AppConfigurtaionServices.Configuration[$"Appsettings:{key}"];
            return GetValue("Appsettings", key);
        }
        /// <summary>
        /// 读配置文件的连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReadConnectionString(string key)
        {
            return  AppConfigurtaionServices.Configuration.GetConnectionString(key);
        }
        /// <summary>
        /// 读配置文件json
        /// "Appsettings:SystemName" 二级
        /// "ServiceUrl"一级
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Obsolete("请用GetValue代替")]
        public static string ReadConfiguration(string key)
        {
            return AppConfigurtaionServices.Configuration[key];
        }
        /// <summary>
        /// 通过键和节点获取值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string node, string key)
        {
            return AppConfigurtaionServices.Configuration[$"{node}:{key}"];
        }
    }
}
