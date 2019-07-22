using System;
using System.IO;
using System.Security.Permissions;
using Smart.Net45.CheckSum;
using Smart.Net45.Enum;
using Smart.Net45.Helper;

namespace Smart.Net45.Extends
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 比较两个文件是否相同
        /// <para>通过CRC32/MD5/SHA1三种比较，防止哈希碰撞</para>
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <returns></returns>
        public static bool FileCompare(string file1, string file2)
        {
            if (string.IsNullOrWhiteSpace(file1)) throw new ArgumentNullException(nameof(file1), "参数file1为空");
            if (string.IsNullOrWhiteSpace(file2)) throw new ArgumentNullException(nameof(file2), "参数file2为空");
            if (!File.Exists(file1)) throw new FileNotFoundException(file1 + "不存在");
            if (!File.Exists(file2)) throw new FileNotFoundException(file2 + "不存在");
            return CrcValue(file1, CrcAlgorithmKinds.Adler32) == CrcValue(file2, CrcAlgorithmKinds.Adler32)
                && Md5Value(file1) == Md5Value(file2)
                && Sha1Value(file1) == Sha1Value(file2);
        }
        /// <summary>
        /// 文件MD5值
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>返回MD5值</returns>
        public static string Md5Value(string fileName)
        {
            return HashFile(fileName, HashAlgorithmKinds.Md5);
        }
        /// <summary>
        /// 文件哈希值
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>返回哈希值</returns>
        public static string Sha1Value(string fileName)
        {
            return HashFile(fileName, HashAlgorithmKinds.Sha160);
        }
        /// <summary>
        /// CRC校验值
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="crcKind">CRC校验类型</param>
        /// <returns>校验值</returns>
        public static long CrcValue(string fileName, CrcAlgorithmKinds crcKind)
        {
            if (!File.Exists(fileName)) return 0;
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);
                ICheckSum checker = new Crc32();
                checker.Update(buffur);
                return checker.Value;
            }
        }
        /// <summary>
        /// 计算文件的哈希值
        /// </summary>
        /// <param name="fileName">要计算哈希值的文件名和路径</param>
        /// <param name="hashKind">Hash算法类型</param>
        /// <returns>哈希值16进制字符串</returns>
        public static string HashFile(string fileName, HashAlgorithmKinds hashKind)
        {
            if (!File.Exists(fileName)) return string.Empty;
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return HashAlgorithmHelper.HashData(fs, hashKind);
            }
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="dstPath">目标路径eg:C:\Temp\a.txt</param>
        /// <param name="buffer">要保存的二进制流buffer</param>
        /// <param name="start">buffer的起始读取位</param>
        /// <param name="length">buffer的读取长度</param>
        public static void SaveFile(string dstPath, byte[] buffer, int start, int length)
        {
            var fp = new FileIOPermission(FileIOPermissionAccess.Write | FileIOPermissionAccess.Append, Path.GetFullPath(dstPath));
            fp.Demand();
            fp.Assert();
            var dstFile = new FileInfo(dstPath);
            using (var dst = dstFile.Open(FileMode.Create, FileAccess.Write, FileShare.None))
            {
                dst.Write(buffer, start, length);
            }
        }

    }
}
