using Smart.Standard.Attribute;

namespace Smart.Standard.Enum
{
    /// <summary>
    /// 数据库类型枚举
    /// </summary>
    [EnumDescription("数据库类型")]
    public enum DataBaseType
    {
        /// <summary>
        /// SqlServer数据库
        /// </summary>
        [EnumDescription("SqlServer数据库")]
        MsSql =1,
        /// <summary>
        /// PgSql数据库
        /// </summary>
        [EnumDescription("PgSql数据库")]
        PostgreSql =2
    }
}
