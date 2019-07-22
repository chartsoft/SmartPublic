using System;
using System.IO;
using System.Drawing;

namespace Smart.Net45.Helper
{
    /// <summary>
    /// 流、字节、图片辅助类
    /// </summary>
    public class StreamByteImageHelper
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

        /// <summary> 
        /// 字节数组转换成图片 
        /// </summary> 
        public static Image ByteArrayToImage(byte[] bytes)
        {
            try
            {
                var ms = new MemoryStream(bytes);
                var img = Image.FromStream(ms);
                return img;
            }
            catch (Exception)
            {
                
                return null;
            }
        }

        /// <summary>
        ///  图片转换成字节流 
        /// </summary>
        public static byte[] ImageToByteArray(Image img)
        {
            var imgconv = new ImageConverter();
            var b = (byte[])imgconv.ConvertTo(img, typeof(byte[]));
            return b;
        }


    }
}