using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.International.Converters.PinYinConverter;
using Smart.Net45.Enum;

namespace Smart.Net45.Helper
{
    /// <summary>
    /// 拼音助手
    /// </summary>
    public  class PinYinHelper
    {
     
        #region[Support]
      

        private static readonly Regex RegexTone = new Regex(@"\d", RegexOptions.Compiled);

        private static string RemoveTone(string source)
        {
            return RegexTone.Replace(source, string.Empty);
        }

        private static string UpperFirstChar(string source)
        {
            var chars = source.ToLower().ToCharArray();
            chars[0] = UpperChar(chars[0]);
            return new string(chars);
        }

        private static char UpperChar(char c)
        {
            if (c <= 'z' && c >= 'a')
                return (char)(c - 32);
            return c;
        }
        private static readonly Regex AcronymRegex = new Regex("[a-z]", RegexOptions.Compiled);
        #endregion

        #region [Method]
        /// <summary>
        /// 返回单个简体中文字的拼音个数
        /// </summary>
        /// <param name="inputChar">简体中文单字</param>      
        public static short GetPinYinCount(char inputChar)
        {
            var chineseChar = new ChineseChar(inputChar);
            return chineseChar.PinyinCount;
        }
        /// <summary>
        /// 返回单个简体中文字的拼音列表
        /// </summary>
        /// <param name="inputChar">简体中文单字</param>      
        public static ReadOnlyCollection<string> GetPinYinWithTone(char inputChar)
        {
            var chineseChar = new ChineseChar(inputChar);
            return chineseChar.Pinyins;
        }
        /// <summary>
        /// 获取拼音
        /// </summary>
        /// <param name="source">要获取拼音的文字</param>
        /// <param name="mode">获取模式</param>
        /// <param name="spliter">字之间的分隔符</param>
        /// <returns></returns>
        public static string GetPinYin(string source, PinYinKinds mode = PinYinKinds.Simple, string spliter = " ")
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;

            var sb = new StringBuilder();
            foreach (var c in source)
            {
                if (ChineseChar.IsValidChar(c))
                {
                    var pinYins = GetPinYinWithTone(c);

                    switch (mode)
                    {
                        case PinYinKinds.Simple:
                            sb.Append(UpperFirstChar(RemoveTone(pinYins[0])));
                            break;
                        case PinYinKinds.WithTone:
                            sb.Append(UpperFirstChar(pinYins[0]));
                            break;
                        case PinYinKinds.WithMultiplePronunciations:
                            sb.Append(string.Join(",", pinYins.Where(x => !string.IsNullOrWhiteSpace(x))
                                                            .Select(RemoveTone)
                                                            .Select(UpperFirstChar)
                                                            .Distinct()));
                            break;
                        case PinYinKinds.WithToneAndMultiplePronunciations:
                            sb.Append(string.Join(",", pinYins.Where(x => !string.IsNullOrWhiteSpace(x))
                                                            .Select(UpperFirstChar)));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
                    }
                }
                else
                {
                    sb.Append(UpperChar(c));
                }
                sb.Append(spliter);
            }

            return sb.Remove(sb.Length - 1, 1).ToString();
        }
        /// <summary>
        /// 获取词组所有可能的拼音组合
        /// </summary>
        /// <param name="source">要获取拼音的文字</param>
        /// <param name="singleWordSpliter">单个字之间的分隔符</param>
        /// <param name="wordTermSpliter">词字之间的分隔符</param>
        /// <returns></returns>
        public static string GetPinYinCombination(string source, string singleWordSpliter = "", string wordTermSpliter = " ")
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;

            var result = new List<List<string>>();

            foreach (var c in source)
            {
                if (ChineseChar.IsValidChar(c))
                {
                    var pinYins = GetPinYinWithTone(c)
                        .Where(x => x != null)
                        .Select(RemoveTone)
                        .Distinct()
                        .Select(UpperFirstChar)
                        .ToList();
                    result.Add(pinYins);
                }
                else
                {
                    result.Add(new List<string> { UpperChar(c).ToString() });
                }
            }

            return string.Join(wordTermSpliter, Combinate(result, singleWordSpliter));
        }
        /// <summary>
        /// 组合所有List元素
        /// </summary>
        /// <param name="input"></param>
        /// <param name="singleWordSpliter"></param>
        /// <returns></returns>
        public static List<string> Combinate(List<List<string>> input, string singleWordSpliter = "")
        {
            return input.Aggregate(new List<string>(),
                (current, arr) => !current.Any() ?
                    arr :
                    (from s1 in current
                     from s2 in arr
                     select s1 + singleWordSpliter + s2)
                    .ToList());
        }
        /// <summary>
        /// 获取首字母缩写词
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetAcronym(string source)
        {
            return AcronymRegex.Replace(source, "");
        }
        #endregion


    }

}
