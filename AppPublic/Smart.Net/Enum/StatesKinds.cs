using System;
using Smart.Net45.Attribute;

namespace Smart.Net45.Enum
{
    /// <summary>
    /// 状态枚举
    /// </summary>
    [Net45.Attribute.EnumDescription("状态枚举")]
    [Flags]
    public enum StatesKinds
    {
        /// <summary>
        /// 可用
        /// </summary>
        [Net45.Attribute.EnumDescription("可用")]
        Enable = 1,
        /// <summary>
        /// 不可用
        /// </summary>
        [Net45.Attribute.EnumDescription("不可用")]
        DissEnable = 1 << 1,
        /// <summary>
        /// 移除
        /// </summary>
        [Net45.Attribute.EnumDescription("移除")]
        Remove = 1 << 2,
        /// <summary>
        /// 到期
        /// </summary>
        [Net45.Attribute.EnumDescription("到期")]
        Expire = 1 << 3,
        /// <summary>
        /// 锁定
        /// </summary>
        [Net45.Attribute.EnumDescription("锁定")]
        Lock = 1 << 4,
        /// <summary>
        /// 解锁
        /// </summary>
        [Net45.Attribute.EnumDescription("解锁")]
        UnLock = 1 << 5,
        /// <summary>
        /// 未审核
        /// </summary>
        [EnumDescription("未审核")]
        NotAudit = 1 << 6,
        /// <summary>
        /// 审核通过
        /// </summary>
        [EnumDescription("审核通过")]
        AuditPass = 1 << 7,
        /// <summary>
        /// 审核未通过
        /// </summary>
        [EnumDescription("审核未通过")]
        AuditRefuse = 1 << 8
    }
}
