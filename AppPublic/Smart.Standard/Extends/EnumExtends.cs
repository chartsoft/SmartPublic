using System;
using System.Collections.Generic;
using System.Linq;
using Smart.Standard.Attribute;
using Smart.Standard.Enum;

namespace Smart.Standard.Extends
{
    /// <summary>
    /// 枚举扩展方法
    /// </summary>
    public static class EnumExtends
    {
        /// <summary>
        /// 得到对枚举的描述文本
        /// </summary>
        /// <param name="emuEnum"></param>
        /// <returns></returns>
        [Obsolete("请用GetEnumClassText代替")]
        public static string GetEnumText(this System.Enum emuEnum)
        {
            return EnumDescription.GetEnumText(emuEnum.GetType());
        }
        /// <summary>
        /// 得到对枚举的类描述文本
        /// </summary>
        /// <param name="emuEnum"></param>
        /// <returns></returns>
        public static string GetEnumClassText(this System.Enum emuEnum)
        {
            return EnumDescription.GetEnumClassText(emuEnum.GetType());
        }
        /// <summary>
        /// 获取枚举文本
        /// </summary>
        /// <param name="emEnum"></param>
        /// <returns></returns>
        [Obsolete("请用GetEnumDisplayText代替")]
        public static string GetFieldText(this System.Enum emEnum)
        {
            return EnumDescription.GetFieldText(emEnum);
        }
        /// <summary>
        /// 获取枚举文本
        /// </summary>
        /// <param name="emEnum"></param>
        /// <returns></returns>
        public static string GetEnumDisplayText(this System.Enum emEnum)
        {
            return EnumDescription.GetEnumDisplayText(emEnum);
        }
        /// <summary>
        /// 获取枚举EnumDescription类型
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static EnumDescription GetFieldInfo(this System.Enum enumValue)
        {
            return EnumDescription.GetFieldInfo(enumValue);
        }
        /// <summary>
        /// 获取枚举定义的常量值
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetConstValue(this System.Enum enumValue)
        {
            return EnumDescription.GetConstValue(enumValue);
        }
        /// <summary>
        /// 添加枚举
        /// </summary>
        /// <param name="enumValue"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static TEnum AddFlags<TEnum>(this TEnum enumValue, TEnum flags) where TEnum : struct
        {
                enumValue = (enumValue.CastTo<int>() | flags.CastTo<int>()).CastTo<TEnum>();
            return enumValue;
        }

        /// <summary>
        /// 移除枚举
        /// </summary>
        /// <param name="enumValue"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static TEnum RemoveFlags<TEnum>(this TEnum enumValue, TEnum flags) where TEnum : struct
        {
            foreach (var item in flags.ToIntArray())
            {
                if ((enumValue.CastTo<int>() & item.CastTo<int>()) == item.CastTo<int>())
                    enumValue = (enumValue.CastTo<int>() ^ item.CastTo<int>()).CastTo<TEnum>();
            }           
            return enumValue;
        }
        /// <summary>
        /// 转换成数组
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="statesKinds"></param>
        /// <returns></returns>
        public static IEnumerable<int> ToIntArray<TEnum>(this TEnum statesKinds) where TEnum : struct
        {
            var intList = new List<int>();
            foreach (var o in System.Enum.GetValues(statesKinds.GetType()))
            {
                var b = (statesKinds.CastTo<int>() & o.CastTo<int>()) == o.CastTo<int>();
                if (b) intList.Add(o.CastTo<int>());
            }
            return intList;
        }
    }
}
