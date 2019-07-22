using System;

namespace Smart.Net45.Attribute
{
    /// <inheritdoc />
    /// <summary>
    /// 缓存特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CacheAttribute : System.Attribute 
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 缓存符合主键处理key
        /// </summary>
        public  string CacheKey { get; set; }
    }
}
