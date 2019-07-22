using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smart.Net45.Helper;

namespace Smart.Net45.Extends
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
            return StreamByteImageHelper.StreamToBytes(stream);
        }
    

    }
}
