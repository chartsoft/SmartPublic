using Smart.Net45.Attribute;

namespace Smart.Net45.Enum
{
    /// <summary>
    /// .Net版本枚举
    /// </summary>
    [EnumDescription(".Net版本枚举")]
    public enum Netkinds
    {
        /// <summary>
        /// .NET2.0
        /// </summary>
        [EnumDescription("v2.0")]
        Net2 = 1,
        /// <summary>
        /// .NET4.0
        /// </summary>
        [EnumDescription("v4.0")]
        Net4 = 2,
        /// <summary>
        /// 无托管代码兼容.NetCore
        /// </summary>
        [EnumDescription("无托管代码")]
        Nun = 3,
    }
}
