using System;
using System.Collections.Generic;
using System.Linq;

using Smart.Standard.Attribute;
using Smart.Standard.Extends;

namespace Smart.Standard.Helper
{
    /// <summary>
    /// 缓存信息配置
    /// </summary>
    public abstract class CacheHelper<TValue>
    {
        /// <summary>
        /// 集合
        /// </summary>
        public readonly Dictionary<string, Dictionary<object, TValue>> Dictionary = new Dictionary<string, Dictionary<object, TValue>>();
        /// <summary>
        /// 
        /// </summary>
        protected CacheHelper()
        {
            var ditem = GetList().ToDictionary(item => item.GetPropertyValue("11"));
            Dictionary.Add("22", ditem);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<TValue> GetList();


    }
}
