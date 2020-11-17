using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Smart.Net45.Helper.Encrypt
{
    /// <summary>
    /// RSA ECC
    /// 可逆非对称加密 
    /// 非对称加密算法的优点是密钥管理很方便，缺点是速度慢。
    /// </summary>
    public class RsaHelper
    {
        /// <summary>
        /// 获取加密/解密对
        /// 给你一个，是无法推算出另外一个的
        /// 
        /// Encrypt   Decrypt
        /// </summary>
        /// <returns>Encrypt   Decrypt</returns>
        public static KeyValuePair<string, string> GetKeyPair()
        {
            var rsa = new RSACryptoServiceProvider();
            var publicKey = rsa.ToXmlString(false);
            var privateKey = rsa.ToXmlString(true);
            return new KeyValuePair<string, string>(publicKey, privateKey);
        }

        /// <summary>
        /// 加密：内容+加密key
        /// </summary>
        /// <param name="content"></param>
        /// <param name="encryptKey">加密key</param>
        /// <returns></returns>
        public static string Encrypt(string content, string encryptKey)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(encryptKey);
            var byteConverter = new UnicodeEncoding();
            var dataToEncrypt = byteConverter.GetBytes(content);
            var resultBytes = rsa.Encrypt(dataToEncrypt, false);
            return Convert.ToBase64String(resultBytes);
        }

        /// <summary>
        /// 解密  内容+解密key
        /// </summary>
        /// <param name="content"></param>
        /// <param name="decryptKey">解密key</param>
        /// <returns></returns>
        public static string Decrypt(string content, string decryptKey)
        {
            var dataToDecrypt = Convert.FromBase64String(content);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(decryptKey);
            var resultBytes = rsa.Decrypt(dataToDecrypt, false);
            var byteConverter = new UnicodeEncoding();
            return byteConverter.GetString(resultBytes);
        }

    }
}
