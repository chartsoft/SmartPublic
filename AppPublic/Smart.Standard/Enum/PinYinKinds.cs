namespace Smart.Standard.Enum
{
    /// <summary>
    /// 获取拼音类型枚举
    /// </summary>
    public enum PinYinKinds
    {
        /// <summary>
        /// 简单模式，返回不带声调的拼音，遇到多音字则返回第一条
        /// </summary>
        Simple = 0,

        /// <summary>
        /// 返回带声调的拼音
        /// </summary>
        WithTone = 1,
        /// <summary>
        /// 返回所有多音字的拼音
        /// </summary>
        WithMultiplePronunciations = 2,
        /// <summary>
        /// 返回所有多音字，并且带声调
        /// </summary>
        WithToneAndMultiplePronunciations = 4
    }
}
