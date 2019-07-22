using Smart.Standard.Attribute;

namespace Smart.Standard.Enum
{
    /// <summary>
    /// 异常枚举
    /// </summary>
    public enum ExceptionKinds
    {
        /// <summary>
        /// 数据异常
        /// </summary>
        [EnumDescription("数据异常")]
        DataError=1001,
        /// <summary>
        /// 业务异常
        /// </summary>
        [EnumDescription("业务异常")]
        BusinessError =1002,
        /// <summary>
        /// 逻辑异常
        /// </summary>
        [EnumDescription("逻辑异常")]
        LogicError =1003,
        /// <summary>
        /// 不支持类型异常
        /// </summary>
        [EnumDescription("不支持类型异常")]
        NotsupportError =1004,
        /// <summary>
        /// 程序异常
        /// </summary>
        [EnumDescription("程序异常")]
        ProgramError = 1005,
        /// <summary>
        /// 非空异常
        /// </summary>
        [EnumDescription("非空异常")]
        NonemptyError =1006,
        /// <summary>
        /// 范围异常
        /// </summary>
        [EnumDescription("范围异常")]
        RangeError = 1007
    }
}
