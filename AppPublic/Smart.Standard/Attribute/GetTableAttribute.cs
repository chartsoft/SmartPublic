using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using Smart.Standard.Extends;
using Smart.Standard.FastReflection;

namespace Smart.Standard.Attribute
{
    public class GetTableAttribute
    {
        #region [Public]

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DbTableAttribute GetDbTableAttr<T>()
        {
            return typeof(T).GetAttribute<DbTableAttribute>();
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetWhereParams<T>()
        {
            var list = typeof(T).GetPropertiesCache();
            return list.Select(c => $"{c.Name}=@{c.Name}").ParseJoin();
        }
        /// <summary>
        /// 获取列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetColums<T>()
        {
            return typeof(T).GetPropertiesCache().Select(c => c.Name).ParseJoin();
        }
        /// <summary>
        /// 只获取参数
        /// </summary>
        /// <returns></returns>
        public static string GeteParams<T>()
        {
            return typeof(T).GetPropertiesCache().Select(c => $"@{c.Name}").ParseJoin();
        }
        /// <summary>
        /// 去忽略列的其他列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetIgnoreCloums<T>()
        {
            return GetIgnorePropertyInfos<T>().Select(c => c.Name).ParseJoin();
        }
        /// <summary>
        /// 去忽略列的其他列带参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetIgnoreCloumsParams<T>()
        {
            return GetIgnorePropertyInfos<T>().Select(c => $"{c.Name}=@{c.Name}").ParseJoin();
        }

        #endregion


        #region [private]
        /// <summary>
        /// 忽略列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static IEnumerable<PropertyInfo> GetIgnorePropertyInfos<T>()
        {
            var atrr = GetDbTableAttr<T>();
            var ignoresList = string.IsNullOrEmpty(atrr.Ignore) ? new List<string>() : atrr.Ignore.Split(',').ToList();
            if (atrr.AutoIncrement) ignoresList.Add(atrr.PrimaryKey.Split(',')[0]);
            return !ignoresList.Any() ? typeof(T).GetPropertiesCache().ToList() : typeof(T).GetPropertiesCache().Where(c => !ignoresList.Contains(c.Name)).ToList();

        }


        #endregion
    }
}
