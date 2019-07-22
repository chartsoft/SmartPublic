using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smart.Net45.Helper;


namespace Smart.Net45.Extends
{
    /// <summary>
    /// 字符扩展类
    /// </summary>
    public static class CharExtends
    {
        #region[PinYin]
        /// <summary>
        /// 返回单个简体中文字的拼音个数
        /// </summary>
        /// <param name="inputChar">简体中文单字</param>      
        public static short GetPinYinCount(this char inputChar)
        {
            return PinYinHelper.GetPinYinCount(inputChar);
        }
        /// <summary>
        /// 返回单个简体中文字的拼音列表
        /// </summary>
        /// <param name="inputChar">简体中文单字</param> 
        public static ReadOnlyCollection<string> GetPinYinWithTone(this char inputChar)
        {
            return PinYinHelper.GetPinYinWithTone(inputChar);
        }
        #endregion
    }
}
