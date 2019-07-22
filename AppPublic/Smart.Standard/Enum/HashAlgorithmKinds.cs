using System;
using System.Runtime.Serialization;

namespace Smart.Standard.Enum
{
    /// <summary>
    /// Hash算法类型
    /// </summary>
    [Serializable]
    [DataContract]
    public enum HashAlgorithmKinds
    {
        ///// <summary>
        ///// KeyedHashAlgorithm
        ///// </summary>
        //KeyedHashAlgorithm,
        /// <summary>
        /// MD5
        /// </summary>
        [EnumMember]
        Md5,
        /// <summary>
        /// RIPEMD160
        /// </summary>
        [Obsolete("暂时未实现该加密算法")]
        [EnumMember]
        Ripemd160,
        /// <summary>
        /// SHA1
        /// </summary>
        [EnumMember]
        Sha160,
        /// <summary>
        /// SHA256
        /// </summary>
        [EnumMember]
        Sha256,
        /// <summary>
        /// SHA384
        /// </summary>
        [EnumMember]
        Sha384,
        /// <summary>
        /// SHA512
        /// </summary>
        [EnumMember]
        Sha512,
        /// <summary>
        ///  Sha1
        /// </summary>
        [EnumMember]
        Sha1
    }
}