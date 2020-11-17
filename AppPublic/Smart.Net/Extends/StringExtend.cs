using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Smart.Net45.Enum;
using Smart.Net45.Helper;
using Smart.Net45.Helper.Encrypt;
using Smart.Net45.ParseProviders;

namespace Smart.Net45.Extends
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtend
    {
        #region [CutStringLength]

        /// <summary>
        /// 截取标题字数
        /// </summary>
        /// <param name="str">准备截取的字符串</param>
        /// <param name="len">截取长度</param>
        /// <param name="suffix">超过长度显示字符串</param>
        /// <returns></returns>
        public static string CutStringLength(this string str, int len, string suffix)
        {
            var intLen = str.Length;
            var start = 0;
            var end = intLen;
            var single = 0;
            var chars = str.ToCharArray();
            for (var i = 0; i < chars.Length; i++)
            {
                if (Convert.ToInt32(chars[i]) > 255)//针对汉字
                {
                    start += 2;
                }
                else//针对字母
                {
                    start += 1;
                    single++;
                }
                if (start >= len)
                {

                    if (end % 2 == 0)
                    {
                        if (single % 2 == 0)
                        {
                            end = i + 1;
                        }
                        else
                        {
                            end = i;
                        }
                    }
                    else
                    {
                        end = i + 1;
                    }
                    break;
                }
            }
            var temp = str.Substring(0, end);
            if (str.Length > end)
            {
                return temp + suffix;
            }
            return temp + suffix;
        }
        #endregion

        #region [GetChineseInitial]
        /// <summary>
        /// 取得拼音首字母
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string GetChineseInitial(this string str)
        {
            var len = str.Length;
            var myStr = "";
            for (var i = 0; i < len; i++)
            {
                myStr += GetOneCharacterSpell(str.Substring(i, 1));
            }
            return myStr;
        }

        private static string GetOneCharacterSpell(string cnChar)
        {
            var arrCn = Encoding.Default.GetBytes(cnChar);
            if (arrCn.Length > 1)
            {
                int area = arrCn[0];
                int pos = arrCn[1];
                var code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (var i = 0; i < 26; i++)
                {
                    var max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            return cnChar;
        }
        #endregion

        #region [ToHash]

        /// <summary>
        /// Hash加密
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="hashAlgorithm">哈希类型</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">未实现对应的哈希Algorithm</exception>
        public static string ToHash(this string str, HashAlgorithmKinds hashAlgorithm)
        {
            var retrieval = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                var data = Encoding.Default.GetBytes(str);
                switch (hashAlgorithm)
                {
                    case HashAlgorithmKinds.Md5:
                        {
                            #region md5
                            //MD5 md5 = new MD5CryptoServiceProvider();
                            //md5.TransformFinalBlock(data, 0, data.Length);
                            //retval = md5.Hash.Aggregate(retval, (current, b) => current + Convert.ToString(b, 16).ToUpper(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            //md5.Clear();
                            retrieval= Md5Helper.Encrypt(str);
                            #endregion
                            break;
                        }
                    case HashAlgorithmKinds.Sha160:
                        {
                            #region sha160
                            SHA1 sha1 = new SHA1Managed();
                            sha1.TransformFinalBlock(data, 0, data.Length);
                            retrieval = sha1.Hash.Aggregate(retrieval, (current, b) => current + Convert.ToString(b, 16).ToUpper(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            sha1.Clear();
                            #endregion
                            break;
                        }
                    case HashAlgorithmKinds.Sha256:
                        {
                            #region sha256
                            SHA256 sha2 = new SHA256Managed();
                            sha2.TransformFinalBlock(data, 0, data.Length);
                            retrieval = sha2.Hash.Aggregate(retrieval, (current, b) => current + Convert.ToString(b, 16).ToUpper(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            sha2.Clear();
                            #endregion
                            break;
                        }
                    case HashAlgorithmKinds.Sha384:
                        {
                            #region sha384
                            SHA384 sha3 = new SHA384Managed();
                            sha3.TransformFinalBlock(data, 0, data.Length);
                            retrieval = sha3.Hash.Aggregate(retrieval, (current, b) => current + Convert.ToString(b, 16).ToUpper(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            sha3.Clear();
                            #endregion
                            break;
                        }
                    case HashAlgorithmKinds.Sha512:
                        {
                            #region sha512
                            SHA512 sha5 = new SHA512Managed();
                            sha5.TransformFinalBlock(data, 0, data.Length);
                            retrieval = sha5.Hash.Aggregate(retrieval, (current, b) => current + Convert.ToString(b, 16).ToUpper(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            sha5.Clear();
                            #endregion
                            break;
                        }
                    case HashAlgorithmKinds.Sha1:
                        {
                            #region sha1
                            SHA1 sha1 = new SHA1Managed();
                            sha1.TransformFinalBlock(data, 0, data.Length);
                            retrieval = sha1.Hash.Aggregate(retrieval, (current, b) => current + Convert.ToString(b, 16).ToUpper(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            sha1.Clear();
                            #endregion
                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException($"未实现{hashAlgorithm}对应的哈希Algorithm");
                        }
                }
            }
            return retrieval;
        }
        /// <summary>  
        /// DES加密算法
        /// </summary>  
        /// <param name="pToEncrypt">需要加密的字符串</param>  
        /// <param name="sKey">密钥</param>  
        /// <returns></returns>  
        public static string DesEncrypt(this string pToEncrypt, string sKey)
        {
            sKey = sKey.ToHash(HashAlgorithmKinds.Md5).Substring(0, 8);
            using (var des = new DESCryptoServiceProvider())
            {
                var inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
                des.Key = Encoding.ASCII.GetBytes(sKey);
                des.IV = Encoding.ASCII.GetBytes(sKey);
                var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                var str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }

        }
        /// <summary>  
        /// DES解密算法
        /// </summary>  
        /// <param name="pToDecrypt">需要解密的字符串</param>  
        /// <param name="sKey">密钥</param>  
        /// <returns></returns>  
        public static string DesDecrypt(this string pToDecrypt, string sKey)
        {
            sKey = sKey.ToHash(HashAlgorithmKinds.Md5).Substring(0, 8);
            var inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (var des = new DESCryptoServiceProvider())
            {
                des.Key = Encoding.ASCII.GetBytes(sKey);
                des.IV = Encoding.ASCII.GetBytes(sKey);
                var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                var str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }

        /// <summary>
        /// rsa加密
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string RsaEncrypt(this string pToDecrypt, string sKey)
        {
            return RsaHelper.Encrypt(pToDecrypt, sKey);
        }

        /// <summary>
        /// rsa解密
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string RsaDecrypt(this string pToDecrypt, string sKey)
        {
            return RsaHelper.Decrypt(pToDecrypt, sKey);
        }
        #endregion

        #region [Left]

        /// <summary>
        /// 取左
        /// </summary>
        public static string Left(this string str, int length)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            return str;
        }

        #endregion

        #region [Right]

        /// <summary>
        /// 取右
        /// </summary>
        public static string Right(this string str, int length)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            if (str.Length > length)
            {
                str = str.Substring(str.Length - length, length);
            }
            return str;
        }

        #endregion

        #region [Middle]

        /// <summary>
        /// 取中
        /// </summary>
        public static string Middle(this string value, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            value = value.Substring(startIndex, length);
            return value;
        }

        #endregion

        #region [Is Type]

        /// <summary>
        /// 是否是日期
        /// </summary>
        public static bool IsDate(this string value)
        {
            return DateTime.TryParse(value, out _);
        }
        /// <summary>
        /// 是否Int型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInt(this string value)
        {
            return int.TryParse(value, out _);
        }
        /// <summary>
        /// 是否Bool型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsBool(this string value)
        {
            return bool.TryParse(value, out _);
        }
        /// <summary>
        /// 是否Decimal型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Isdecimal(this string value)
        {
            return decimal.TryParse(value, out _);
        }
        /// <summary>
        /// 是否Tinyint型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsTinyint(this string value)
        {
            return byte.TryParse(value, out _);
        }

        /// <summary>
        /// 判断是否为IP地址
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIp(this string value)
        {
            return IPAddress.TryParse(value, out _);

        }
        /// <summary>
        /// 正则匹配
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexKinds"></param>
        /// <returns></returns>
        public static bool IsRegex(this string value, RegexKinds regexKinds)
        {
            return Regex.IsMatch(value, regexKinds.GetConstValue());
        }
        #endregion

        #region ToDateTime

        /// <summary>
        /// 将string转为DateTime，如果字符串不是正确的日期值，将返回DateTime.MinValue
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string source)
        {
            return ToDateTimeNullable(source) ?? DateTime.MinValue;
        }

        /// <summary>
        /// 将string转为DateTime，如果字符串不是正确的日期值，将返回null
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeNullable(this string source)
        {
            if (DateTime.TryParse(source, out var dateTime))
                return dateTime;
            return null;
        }

        #endregion

        #region ToInt32

        /// <summary>
        /// 将字符串转为数字,如果转换失败则返回默认值
        /// </summary>
        /// <param name="source">字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static int ToInt(this string source, int defaultValue = 0)
        {
            return int.TryParse(source, out var i) ? i : defaultValue;
        }

        #endregion

        #region  [Split/Join]

        /// <summary>
        /// 将字符串前面加逗号扩展方法
        /// </summary>
        /// <param name="source">字符串</param>
        /// <returns>添加了逗号分隔的字符串</returns>
        public static string AddCommaSplit(this string source)
        {
            return "," + source;
        }
        /// <summary>
        /// 加双引号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AddDoubleQuotes(this string str)
        {
            return $"\"{str}\"";
        }
        #endregion

        #region [Parse]

        /// <summary>
        /// 解析Json实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="val">值</param>
        /// <returns>实体实例</returns>
        public static T ParseJson<T>(this string val) where T : class, new()
        {
            var provider = ParserFactory.JsonParser;
            return provider.Parse<T>(val);
        }

        /// <summary>
        /// 解析Xml实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="val">值</param>
        /// <returns>实体实例</returns>
        public static T ParseXml<T>(this string val) where T : class, new()
        {
            var provider = ParserFactory.XmlParser;
            return provider.Parse<T>(val);
        }
        /// <summary>
        /// 解析实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="val">值</param>
        /// <param name="providerName"> </param>
        /// <returns>实体实例</returns>
        public static T ParseEntity<T>(this string val, string providerName) where T : class, new()
        {
            var provider = ParserFactory.GetParser(providerName);
            if (provider == null) throw new Exception($"不支持解析提供器{providerName}");
            return provider.Parse<T>(val);
        }

        #endregion

        #region [Format]
        /// <summary>
        /// 格式化json字符串
        /// </summary>
        /// <param name="jsonstr"></param>
        /// <returns></returns>
        public static string FormatJson(this string jsonstr)
        {
            var provider = ParserFactory.JsonParser;
            return provider.Format(jsonstr);
        }
        /// <summary>
        /// 格式化xml字符串
        /// </summary>
        /// <param name="xmlstr"></param>
        /// <returns></returns>
        public static string FormatXml(this string xmlstr)
        {
            var provider = ParserFactory.XmlParser;
            return provider.Format(xmlstr);
        }
        #endregion

        #region [PinYin]

        /// <summary>
        /// 获取拼音
        /// </summary>
        /// <param name="source">要获取拼音的文字</param>
        /// <param name="mode">获取模式</param>
        /// <param name="spliter">字之间的分隔符</param>
        /// <returns></returns>
        public static string GetPinYin(this string source, PinYinKinds mode = PinYinKinds.Simple, string spliter = " ")
        {
            return PinYinHelper.GetPinYin(source, mode, spliter);
        }

        /// <summary>
        /// 获取词组所有可能的拼音组合
        /// </summary>
        /// <param name="source">要获取拼音的文字</param>
        /// <param name="singleWordSpliter">单个字之间的分隔符</param>
        /// <param name="wordTermSpliter">词字之间的分隔符</param>
        /// <returns></returns>
        public static string GetPinYinCombination(this string source, string singleWordSpliter = "",
            string wordTermSpliter = " ")
        {
            return PinYinHelper.GetPinYinCombination(source, singleWordSpliter, wordTermSpliter);
        }
        /// <summary>
        /// 获取首字母缩写词
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetAcronym(this string source)
        {
            return PinYinHelper.GetAcronym(source);
        }
        #endregion

        #region [ToHexString]
        /// <summary>
        /// 转换16进制字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToHexString( this string str)
        {
            var by = System.Text.Encoding.Default.GetBytes(str);
            return by.ToHexString();
        }

        #endregion
        /// <summary>
        /// 清除Html字符
        /// </summary>
        public static string ClearAllHtml(this string str)
        {
            str = Regex.Replace(str, @"<[^>]*>", "", RegexOptions.IgnoreCase);
            return str;
        }

        #region [Slice]
        /// <summary>
        /// 切片slice
        /// </summary>
        /// <param name="str"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string SliceExt(this string str, string format)
        {
            return str.SliceExt<char>(format).Join("");
        }

        #endregion

        #region [FirstChar]

        /// <summary>
        /// 首字母小写写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToLower(this string input)
        {
            if (input.IsNullOrEmpty())
                return input;
            var str = input.First().ToString().ToLower() + input.Substring(1);
            return str;
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string input)
        {
            if (input.IsNullOrEmpty())
                return input;
            var str = input.First().ToString().ToUpper() + input.Substring(1);
            return str;

        }

        #endregion
    }
}
