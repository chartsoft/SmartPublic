using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smart.Net45.Extends;

namespace Smart.Net45.Helper
{
    /// <summary>
    /// 相对路径和绝对路径相互转换
    /// </summary>
    public class SmartPathHelper
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
            if (relativePath.IsNullOrEmpty() && absolutePath.IsNotNullOrEmpty())
            {
                RelativePath = AbsolutePath.Replace(Directory.GetCurrentDirectory(), "");
            }

            if (relativePath.IsNotNullOrEmpty() && absolutePath.IsNullOrEmpty())
            {
                AbsolutePath = Directory.GetCurrentDirectory() + relativePath;
            }

        }

        /// <summary>
        /// 相对路径
        /// </summary>
        public string RelativePath { get; }

        /// <summary>
        /// 绝对路径
        /// </summary>
        public string AbsolutePath { get;}

        private string ToRelativePath(string absolutePath, string relativeTo)
        {
            //from - www.cnphp6.com

            string[] absoluteDirectories = absolutePath.Split('\\');
            string[] relativeDirectories = relativeTo.Split('\\');

            //Get the shortest of the two paths
            int length = absoluteDirectories.Length < relativeDirectories.Length ? absoluteDirectories.Length : relativeDirectories.Length;

            //Use to determine where in the loop we exited
            int lastCommonRoot = -1;
            int index;

            //Find common root
            for (index = 0; index < length; index++)
                if (absoluteDirectories[index] == relativeDirectories[index])
                    lastCommonRoot = index;
                else
                    break;

            //If we didn't find a common prefix then throw
            if (lastCommonRoot == -1)
                throw new ArgumentException("Paths do not have a common base");

            //Build up the relative path
            StringBuilder relativePath = new StringBuilder();

            //Add on the ..
            for (index = lastCommonRoot + 1; index < absoluteDirectories.Length; index++)
                if (absoluteDirectories[index].Length > 0)
                    relativePath.Append("..\\");

            //Add on the folders
            for (index = lastCommonRoot + 1; index < relativeDirectories.Length - 1; index++)
                relativePath.Append(relativeDirectories[index] + "\\");
            relativePath.Append(relativeDirectories[relativeDirectories.Length - 1]);

            return relativePath.ToString();
        }
    }
}
