using System;
using Smart.Win.Supports;

namespace Smart.Win.Entitys
{
    /// <summary>
    /// 时间戳数据
    /// </summary>
    [Serializable]
    public class TimestampData<T> : ITimestamp
    {

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
