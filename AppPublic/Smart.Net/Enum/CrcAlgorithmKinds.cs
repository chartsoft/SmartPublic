using System;
using System.Runtime.Serialization;

namespace Smart.Net45.Enum
{
    /// <summary>
    /// CRC算法类型
    /// </summary>
    [Serializable]
    [DataContract]
    public enum CrcAlgorithmKinds
    {
        /// <summary>
        /// Crc32
        /// </summary>
        [EnumMember]
        Crc32,
        /// <summary>
        /// Adler32
        /// </summary>
        [EnumMember]
        Adler32,
        /// <summary>
        /// StrangeCrc
        /// </summary>
        [EnumMember]
        StrangeCrc
    }
}