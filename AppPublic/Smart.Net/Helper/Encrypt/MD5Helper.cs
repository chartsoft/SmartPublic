using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Smart.Net45.Helper.Encrypt
{
    /// <summary>
    /// 不可逆加密
    /// 1 防止被篡改
    /// 2 防止明文存储
    /// 3 防止抵赖，数字签名
    /// </summary>
    public class Md5Helper
    {
        #region MD5
        /// <summary>
        /// MD5加密,和动网上的16/32位MD5加密结果相同,
        /// 使用的UTF8编码
        /// </summary>
        /// <param name="source">待加密字串</param>
        /// <param name="length">16或32值之一,其它则采用.net默认MD5加密算法</param>
        /// <returns>加密后的字串</returns>
        public static string Encrypt(string source, int length = 32)//默认参数
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;
            var provider = CryptoConfig.CreateFromName("MD5") as HashAlgorithm;
            var bytes = Encoding.UTF8.GetBytes(source);
            if(provider==null) throw new Exception("provider is null");
            var hashValue = provider.ComputeHash(bytes);
            var sb = new StringBuilder();
            switch (length)
            {
                case 16://16位密文是32位密文的9到24位字符
                    for (var i = 4; i < 12; i++)
                    {
                        sb.Append(hashValue[i].ToString("x2"));
                    }
                    break;
                case 32:
                    for (var i = 0; i < 16; i++)
                    {
                        sb.Append(hashValue[i].ToString("x2"));
                    }
                    break;
                default:
                    foreach (var t in hashValue)
                    {
                        sb.Append(t.ToString("x2"));
                    }
                    break;
            }
            return sb.ToString();
        }
        #endregion MD5

        #region MD5摘要
        /// <summary>
        /// 获取文件的MD5摘要
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string AbstractFile(string fileName)
        {
            using (var file = new FileStream(fileName, FileMode.Open))
            {
                return AbstractFile(file);
            }
        }

        /// <summary>
        /// 根据stream获取文件摘要
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string AbstractFile(Stream stream)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var retVal = md5.ComputeHash(stream);

            var sb = new StringBuilder();
            foreach (var t in retVal)
            {
                sb.Append(t.ToString("x2"));
            }
            return sb.ToString();
        }
        #endregion
    }
}
