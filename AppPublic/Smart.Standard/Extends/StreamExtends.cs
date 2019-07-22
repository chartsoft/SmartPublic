using System.IO;
using Smart.Standard.Helper;

namespace Smart.Standard.Extends
{
    /// <summary>
    /// 流扩展方法
    /// </summary>
    public static class StreamExtends
    {
        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        public static byte[] StreamToBytes(this Stream stream)
        {
            return StreamByteHelper.StreamToBytes(stream);
        }
    

    }
}
