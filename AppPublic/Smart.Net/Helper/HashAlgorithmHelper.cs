using System;
using System.IO;
using System.Security.Cryptography;
using Smart.Net45.Enum;

namespace Smart.Net45.Helper
{
    /// <summary>
    /// HashAlgorithm帮助类
    /// </summary>
    public class HashAlgorithmHelper
    {
        /// <summary>
        /// 计算哈希值
        /// </summary>
        /// <param name="stream">要计算哈希值的 Stream</param>
        /// <param name="hashKind">Hash算法类型</param>
        /// <returns>哈希值字节数组</returns>
        /// <exception cref="NotImplementedException">未实现“Hash算法类型”对应哈希算法</exception>
        public static string HashData(Stream stream, HashAlgorithmKinds hashKind)
        {
            var algorithm = HashAlgorithm.Create(hashKind.ToString());
            if (algorithm == null) throw new NotImplementedException($"未实现{hashKind}对应哈希算法");
            var hashBytes = algorithm.ComputeHash(stream);
            return ByteArrayToHexString(hashBytes);
        }

        /// <summary> 
        /// 字节数组转换为16进制表示的字符串
        /// </summary>
        private static string ByteArrayToHexString(byte[] buf)
        {
            return BitConverter.ToString(buf).Replace("-", "");
        }
    }


}
