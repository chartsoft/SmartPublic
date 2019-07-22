using Smart.Net45.Attribute;

namespace Smart.Net45.Enum
{
    /// <summary>
    /// 小数尾数处理方法
    /// </summary>
    [EnumDescription("小数尾数处理方法")]
    public enum EllipsisKinds
    {
        /// <summary>
        /// 去尾法
        /// </summary>
        [EnumDescription("去尾法")]
        RoundingDown = 1,
        /// <summary>
        /// 收尾
        /// </summary>
        [EnumDescription("收尾法")]
        WindUp = 2,
        /// <summary>
        /// 四舍五入
        /// </summary>
        [EnumDescription("四舍五入")]
        Round = 3,
        /// <summary>
        /// 四舍六入五成双
        /// </summary>
        [EnumDescription("四舍六入五成双")]
        Atwain = 4,
    }
}
