using System;
using System.Collections.Generic;

namespace Smart.Net45.Extends
{
    /// <summary>
    /// Int扩展类
    /// </summary>
    public static class IntExtends
    {

        private static readonly Dictionary<int, string> Base64Code = new Dictionary<int, string>
        {

            {   0  ,"0"}, {   1  ,"1"}, {   2  ,"2"}, {   3  ,"3"}, {   4  ,"4"}, {   5  ,"5"}, {   6  ,"6"}, {   7  ,"7"}, {   8  ,"8"}, {   9  ,"9"},

            {   10  ,"a"}, {   11  ,"b"}, {   12  ,"c"}, {   13  ,"d"}, {   14  ,"e"}, {   15  ,"f"}, {   16  ,"g"}, {   17  ,"h"}, {   18  ,"i"}, {   19  ,"j"},

            {   20  ,"k"}, {   21  ,"l"}, {   22  ,"m"}, {   23  ,"n"}, {   24  ,"o"}, {   25  ,"p"}, {   26  ,"q"}, {   27  ,"r"}, {   28  ,"s"}, {   29  ,"t"},

            {   30  ,"u"}, {   31  ,"v"}, {   32  ,"w"}, {   33  ,"x"}, {   34  ,"y"}, {   35  ,"z"}, {   36  ,"A"}, {   37  ,"B"}, {   38  ,"C"}, {   39  ,"D"},

            {   40  ,"E"}, {   41  ,"F"}, {   42  ,"G"}, {   43  ,"H"}, {   44  ,"I"}, {   45  ,"J"}, {   46  ,"K"}, {   47  ,"L"}, {   48  ,"M"}, {   49  ,"N"},

            {   50  ,"O"}, {   51  ,"P"}, {   52  ,"Q"}, {   53  ,"R"}, {   54  ,"S"}, {   55  ,"T"}, {   56  ,"U"}, {   57  ,"V"}, {   58  ,"W"}, {   59  ,"X"},

            {   60  ,"Y"}, {   61  ,"Z"}, {   62  ,"-"}, {   63  ,"_"},

        };
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="states"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static bool HasFlag(this int states,int flag)
        {
            return (states & flag) == flag;
        }
        /// <summary>
        /// 判断是否存在该状态
        /// </summary>
        /// <param name="states"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static bool HasFlag(this int states, System.Enum flag)
        {
            return (states & flag.CastTo<int>()) == flag.CastTo<int>();
        }
        /// <summary>
        /// 10进制转换成多进制
        /// </summary>
        /// <param name="x"></param>
        /// <param name="ary"></param>
        /// <returns></returns>
        public static  string ToAry(this long x, int ary)
        {
            var a = "";
            while (x >= 1)

            {
                int index = Convert.ToInt16(x - (x / ary) * ary);
                a = Base64Code[index] + a;
                x /= ary;
            }
            return a;
        }
        /// <summary>
        /// 10进制转换成多进制
        /// </summary>
        /// <param name="x"></param>
        /// <param name="ary"></param>
        /// <returns></returns>
        public static string ToAry(this int x, int ary)
        {
            var a = "";
            while (x >= 1)

            {
                int index = Convert.ToInt16(x - (x / ary) * ary);
                a = Base64Code[index] + a;
                x /= ary;
            }
            return a;
        }
    }
}
