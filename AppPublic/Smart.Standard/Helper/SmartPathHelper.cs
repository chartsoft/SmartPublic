using System.IO;
using System.Runtime.InteropServices;
using Smart.Standard.Extends;

namespace Smart.Standard.Helper
{
    /// <summary>
    /// 相对路径和绝对路径相互转换
    /// </summary>
  public  class SmartPathHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="absolutePath">绝对路径</param>
        public SmartPathHelper(string relativePath = "", string absolutePath = "")
        {
            RelativePath = relativePath;
            AbsolutePath = absolutePath;
            var str = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? relativePath.Replace("/", "\\")
                : relativePath;
            if (relativePath.IsNullOrEmpty() && absolutePath.IsNotNullOrEmpty())
            {
                RelativePath = AbsolutePath.Replace(Directory.GetCurrentDirectory(), "");
            }

            if (relativePath.IsNotNullOrEmpty() && absolutePath.IsNullOrEmpty())
            {
                AbsolutePath = Directory.GetCurrentDirectory() + str;
            }

        }
        /// <summary>
        /// 相对路径
        /// </summary>
        public string RelativePath { get; set; }
        /// <summary>
        /// 绝对路径
        /// </summary>
        public string AbsolutePath { get; set; }
    }
}
