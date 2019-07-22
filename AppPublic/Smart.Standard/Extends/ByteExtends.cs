using System.IO;
using System.Text;
using Smart.Standard.Helper;

namespace Smart.Standard.Extends
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class ByteExtends
    {

        /// <summary>
        /// byte数组转16进制字符串
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <returns>16进制字符串</returns>
        public static string ToHexString(this byte[] bytes)
        {
            if (bytes == null || bytes.Length <= 0) return string.Empty;
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// byte数组转16进制字符串
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <returns>16进制字符串</returns>
        public static string ToHexString2(this byte[] bytes)
        {
            if (bytes == null || bytes.Length <= 0) return string.Empty;
            //utf8转 GBK十六进制码
            var utf8Str = Encoding.UTF8.GetString(bytes);
            var bytes2 = Encoding.GetEncoding("GBK").GetBytes(utf8Str);
            var sb = new StringBuilder();
            foreach (var b in bytes2)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        public static Stream BytesToStream(this byte[] bytes)
        {
            return StreamByteHelper.BytesToStream(bytes);
        }

      
    }
}
