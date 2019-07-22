using System;

namespace Smart.Net45.Attribute
{
    /// <summary>
    /// Dapper特性,只能声明到class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class , Inherited = false)]
    public class DbTableAttribute : System.Attribute
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
        /// 更新和插入忽略列
        /// </summary>
        public  string Ignore { get; set; } 
        /// <summary>
        /// 自增
        /// </summary>
        public  bool AutoIncrement { get; set; }

        /// <summary>
        /// 忽略查询
        /// </summary>
        public string IgnoreQuery { get; set; }
    }
}
