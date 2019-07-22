using System;

namespace Smart.Win.Supports
{
    /// <summary>
    /// 时间戳支持
    /// </summary>
    public interface ITimestamp
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        DateTime Timestamp { get; set; }
    }
}
