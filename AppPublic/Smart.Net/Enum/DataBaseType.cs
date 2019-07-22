namespace Smart.Net45.Enum
{
    /// <summary>
    /// 数据库类型枚举
    /// </summary>
    [Attribute.EnumDescription("数据库类型")]
    public enum DataBaseType
    {
        /// <summary>
        /// MsSql数据库
        /// </summary>
        [Attribute.EnumDescription("SqlServer数据库")]
        MsSql =1,
        /// <summary>
        /// pgsql数据库
        /// </summary>
        [Net45.Attribute.EnumDescription("PgSql数据库")]
        PostgreSql =2
    }
}
