using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Smart.Standard.Extends
{
    /// <summary>
    /// 方法扩展
    /// </summary>
    public static class MethodExtends
    {
        /// <summary>
        /// 取得自定义特性
        /// </summary>
        /// <typeparam name="T">自定义Attribute类型</typeparam>
        /// <param name="method">方法信息</param>
        /// <returns>T实例</returns>
        public static T GetAttribute<T>(this MethodInfo method)
        {
            var attributes = method.GetCustomAttributes(true);
            foreach (var att in attributes)
            {
                if (att is T variable)
                    return variable;
            }
            return default(T);
        }
        /// <summary>
        /// 枚举类型根据名称字典获取中文名称串的扩展方法
        /// </summary>
        /// <param name="en"></param>
        /// <param name="dict">该枚举类型的中文名称字典</param>
        /// <returns>转换后的中文名称串，如果缺少中文定义将抛异常</returns>
        public static string GetCnNames(this System.Enum en, Dictionary<System.Enum, string> dict)
        {
            var namestring = en.ToString();
            var enNames = namestring.Split(',');
            var sb = new StringBuilder();
            for (var i = 0; i < enNames.Length; i++)
            {
                var cnName = dict[(System.Enum)System.Enum.Parse(en.GetType(), enNames[i])];
                if (string.IsNullOrEmpty(cnName))
                {
                    throw new Exception($"缺少枚举{enNames[i]}的中文名称定义");
                }
                sb.Append(cnName);
                if (i < enNames.Length - 1)
                    sb.Append(",");
            }
            return sb.ToString();
        }
    }
}
