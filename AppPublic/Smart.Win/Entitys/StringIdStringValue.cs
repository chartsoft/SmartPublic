using System;

namespace Smart.Win.Entitys
{
    /// <summary>
    /// ID↔值
    /// </summary>
    [Serializable]
    public class StringIdStringValue
    {
        /// <summary>
        /// 
        /// </summary>
        public StringIdStringValue() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="val"></param>
        public StringIdStringValue(string id, string val) { Id = id; Value = val; }
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
