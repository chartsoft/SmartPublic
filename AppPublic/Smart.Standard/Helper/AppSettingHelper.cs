using System;
using System.Configuration;
using System.IO;
using System.Xml;
using Smart.Standard.Extends;

namespace Smart.Standard.Helper
{

    /// <summary>
    /// Config文件操作
    /// </summary>
    public class AppSettingHelper
    {

        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <param name="key"></param>
        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key]?.Trim();
        }
        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">AppSetting键</param>
        /// <returns></returns>
        public static T GetValue<T>(string key)
        {
            var val = GetValue(key);
            return string.IsNullOrWhiteSpace(val) ? default(T) : val.CastTo<T>();
        }
        /// <summary>
        /// 根据Key修改Value
        /// </summary>
        /// <param name="key">要修改的Key</param>
        /// <param name="value">要修改为的值</param>
        /// <param name="xmlFileRelativePath">xml文件相对位置
        /// <para>例如：<![CDATA[Xml\\appSettings.config]]></para>
        /// </param>
        public static void SetValue(string key, string value, string xmlFileRelativePath)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFileRelativePath);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"配置文件{filePath}不存在");
            }
            var doc = new XmlDocument();
            doc.Load(filePath);
            var node = doc.SelectSingleNode("//appSettings");//??xDoc.CreateNode();
            if (node == null)
            {
                throw new Exception("配置节点不存在");
                //配置节不存在
                //ExceptionHelper.ThrowProgramException(UtilityErrors.ConfigFileErrorServerIpNotIp);
            }
            var elem1 = (XmlElement)node.SelectSingleNode("//add[@key='" + key + "']");

            if (elem1 != null) elem1.SetAttribute("value", value);
            else
            {
                var elem2 = doc.CreateElement("add");
                elem2.SetAttribute("key", key);
                elem2.SetAttribute("value", value);
                node.AppendChild(elem2);
            }
            doc.Save(filePath);
        }
    }
}
