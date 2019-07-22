using System;
using System.IO;

namespace Smart.Standard.Helper
{
    /// <summary>
    /// 流、字节、图片辅助类
    /// </summary>
    public class StreamByteHelper
    {
        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        public static byte[] StreamToBytes(Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        public static Stream BytesToStream(byte[] bytes)
        {
            return bytes == null ? null : new MemoryStream(bytes);
        }
    
    }
}