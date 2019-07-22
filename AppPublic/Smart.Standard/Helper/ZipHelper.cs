using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using Smart.Standard.Enum;
using Smart.Standard.Extends;

namespace Smart.Standard.Helper
{

    /// <summary>
    /// 使用SharpZipLib来完成打包解包
    /// </summary>
    public class ZipHelper
    {
        /// <summary>
        /// 打包
        /// </summary>
        /// <param name="zipFileName">输出压缩文件名称</param>
        /// <param name="sourceFolderName">需要压缩的文件夹名称</param>
        /// <returns>成功true,失败false</returns>
        public static bool Pack(string zipFileName, string sourceFolderName)
        {
            try
            {
                var fastZip = CreateZipComponent();
                
                fastZip.CreateZip(zipFileName, sourceFolderName, true, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }           
        }

        /// <summary>
        /// 解包
        /// </summary>
        /// <param name="zipFileName">压缩文件名称</param>
        /// <param name="targetFolderName">解压缩的目标文件夹名称</param>
        /// <returns>成功true,失败false</returns>
        public static bool Unpack(string zipFileName, string targetFolderName)
        {
            try
            {
                var fastZip = CreateZipComponent();
                fastZip.ExtractZip(zipFileName, targetFolderName, FastZip.Overwrite.Always, null, null, null, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        private static FastZip CreateZipComponent()
        {
            var fastZip = new FastZip
            {
                CreateEmptyDirectories = true,
                RestoreAttributesOnExtract = true,
                RestoreDateTimeOnExtract = true
            };


            return fastZip;
        }
        /// <summary>
        /// 压缩文件(附带密码)
        /// </summary>
        /// <param name="inputFileName">待压缩的文件名称</param>
        /// <param name="outZipFileName">压缩后的文件名称</param>
        /// <param name="password">压缩密码</param>
        /// <returns>成功返回True</returns>
        public static bool ZipFileWithPassword(string inputFileName, string outZipFileName, string password)
        {
            if (!File.Exists(inputFileName))
            {
                throw new FileNotFoundException();
            }

            var streamToZip = new FileStream(inputFileName, FileMode.Open, FileAccess.Read);
            
            var zipFileStream = File.Create(outZipFileName);

            var zipOutputStream = new ZipOutputStream(zipFileStream);
            var f = new FileInfo(inputFileName);

            var zipEntry = new ZipEntry(f.Name);
            zipOutputStream.Password = password;
            zipOutputStream.PutNextEntry(zipEntry);
            //把密码用MD5加密的字符写入文件头
            var passwordMd5 = password.ToHash(HashAlgorithmKinds.Md5);
            var passwordData = Encoding.Unicode.GetBytes(passwordMd5);

            var dataLength = passwordData.Length + 4;
            var headerData = BitConverter.GetBytes(dataLength);
            //前4位为真实数据存放起始位置
            zipOutputStream.Write(headerData,0,headerData.Length);
            //写入密码数据
            zipOutputStream.Write(passwordData, 0,passwordData.Length);

            var buffer = new byte[1024];
            var size = streamToZip.Read(buffer, 0, buffer.Length);
            
            zipOutputStream.Write(buffer, 0, size);

            try
            {
                while (size < streamToZip.Length)
                {
                    var sizeRead = streamToZip.Read(buffer, 0, buffer.Length);
                    zipOutputStream.Write(buffer, 0, sizeRead);
                    size += sizeRead;
                }
            }
            finally
            {
                zipOutputStream.Finish();
                zipOutputStream.Close();
                streamToZip.Close();
            }
            return true;
        }
        /// <summary>
        /// 解压文件(附带密码)
        /// </summary>
        /// <param name="zipFileName">压缩文件名称</param>
        /// <param name="unZipFileName">解压后文件名</param>
        /// <param name="password">密码</param>
        /// <returns>成功返回true</returns>
        public static bool UnZipFileWithPassword(string zipFileName, string unZipFileName, string password)
        {
            using (var zs = new ZipInputStream(File.OpenRead(zipFileName)))
            {

                var theEntry = zs.GetNextEntry();
                if (!theEntry.IsCrypted)
                {
                    throw new Exception("不是合法的文件。");
                }
                zs.Password = password;
                var passwordMd5 = password.ToHash(HashAlgorithmKinds.Md5);
                var dir = Path.GetDirectoryName(unZipFileName);

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir ?? throw new InvalidOperationException());

                using (var streamWriter = File.Create(unZipFileName))
                {
                    var buffer = new byte[1024];
                    //读取头数据
                    try
                    {
                        zs.Read(buffer, 0, 4);
                    }
                    catch (ZipException zipEx)
                    {
                        if (zipEx.Message == "Invalid password")
                            throw new Exception("解压密码错误。");
                    }

                    var headerLength = BitConverter.ToInt32(buffer, 0);
                    
                    //写输出参数
                    var passwordData = new byte[headerLength - 4];
                    zs.Read(passwordData, 0, headerLength - 4);
                    var usedPassword = Encoding.Unicode.GetString(passwordData);
                    
                    if (usedPassword != passwordMd5)
                    {
                        throw new Exception("解压密码错误。");
                    }

                    var size = zs.Read(buffer, 0, 1024);
                    while (size>0)
                    {
                        streamWriter.Write(buffer, 0, size);
                        size = zs.Read(buffer, 0, 1024);
                    }
                }
            }
            return true;
        }
    }
}
