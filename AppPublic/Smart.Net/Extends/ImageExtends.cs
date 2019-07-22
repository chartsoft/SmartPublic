using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smart.Net45.Helper;

namespace Smart.Net45.Extends
{
    /// <summary>
    /// Image扩展类
    /// </summary>
    public static class ImageExtends
    {
        /// <summary>
        ///  图片转换成字节流 
        /// </summary>
        public static byte[] ImageToByteArray(this Image img)
        {
            return StreamByteImageHelper.ImageToByteArray(img);
        }
    }
}
