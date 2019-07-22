using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Smart.Net45.Extends;

namespace Smart.Net45.Attribute
{
    /// <inheritdoc />
    /// <summary>
    /// 把枚举值按照指定的文本显示
    /// <example>
    /// EnumDescription.GetEnumText(typeof(MyEnum));
    /// EnumDescription.GetFieldText(MyEnum.EnumField);
    /// EnumDescription.GetFieldInfos(typeof(MyEnum));  
    /// </example>
    /// </summary>
    public partial class EnumDescription
    {
        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        private static readonly ConcurrentDictionary<string, List<Net45.Attribute.EnumDescription>> CachedEnum = new ConcurrentDictionary<string, List<Net45.Attribute.EnumDescription>>();
        /// <summary>
        /// 得到对枚举的描述文本
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        [Obsolete("请用GetEnumClassText代替")]
        public static string GetEnumText(Type enumType)
        {
            var eds = (Net45.Attribute.EnumDescription[])enumType.GetCustomAttributes(typeof(Net45.Attribute.EnumDescription), false);
            return eds.Length != 1 ? string.Empty : eds[0].EnumDisplayText;
        }
        /// <summary>
        /// 得到对枚举类的描述文本
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static string GetEnumClassText(Type enumType)
        {
            var eds = (EnumDescription[])enumType.GetCustomAttributes(typeof(Net45.Attribute.EnumDescription), false);
            return eds.Length != 1 ? string.Empty : eds[0].EnumDisplayText;

        }
        /// <summary>
        /// 获得指定枚举类型中，指定值的描述文本。
        /// </summary>
        /// <param name="enumValue">枚举值，不要作任何类型转换</param>
        /// <returns>描述字符串</returns>
        [Obsolete("请用GetEnumDisplayText代替")]
        public static string GetFieldText(object enumValue)
        {
            var fi = GetFieldInfo(enumValue);
            return fi == null ? string.Empty : fi.EnumDisplayText;
        }
        /// <summary>
        /// 获得指定枚举类型中，指定值的描述文本。
        /// </summary>
        /// <param name="enumValue">枚举值，不要作任何类型转换</param>
        /// <returns>描述字符串</returns>
        public static string GetEnumDisplayText(object enumValue)
        {
            var fi = GetFieldInfo(enumValue);
            return fi == null ? string.Empty : fi.EnumDisplayText;
        }
        /// <summary>
        /// 获得指定枚举类型中，指定值的描述文本。
        /// </summary>
        /// <param name="enumValue">枚举值，不要作任何类型转换</param>
        /// <returns>描述字符串</returns>
        public static Net45.Attribute.EnumDescription GetFieldInfo(object enumValue)
        {
            var descriptions = GetFieldInfos(enumValue.GetType());
            return descriptions.FirstOrDefault(ed => ed.FieldName == enumValue.ToString());
        }
        /// <summary>
        /// 获取指定枚举中ConstValue的值
        /// </summary>
        /// <param name="enumVaule"></param>
        /// <returns></returns>
        public static string GetConstValue(object enumVaule)
        {
            var fi = GetFieldInfo(enumVaule);
            return fi == null ? string.Empty : fi.ConstValue;
        }
        /// <summary>
        /// 得到枚举类型定义的所有文本
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        /// <param name="enumType">枚举类型</param>
        public static List<Net45.Attribute.EnumDescription> GetFieldInfos(Type enumType)
        {
            List<Net45.Attribute.EnumDescription> descriptions;
            try
            {
                if (!CachedEnum.ContainsKey(enumType.FullName))
                {
                    Locker.EnterWriteLock();
                    if (!CachedEnum.ContainsKey(enumType.FullName))
                    {
                        descriptions = new List<Net45.Attribute.EnumDescription>();
                        var fields = enumType.GetFields();
                        foreach (var fi in fields)
                        {
                            var attrs = fi.GetCustomAttributes(typeof(Net45.Attribute.EnumDescription), false);
                            if (attrs.Length <= 0) continue;
                            var ed = (Net45.Attribute.EnumDescription)attrs[0];
                            ed.EnumValue = (int)fi.GetValue(null);
                            ed.FieldName = fi.Name;
                            descriptions.Add(ed);
                        }
                        CachedEnum[enumType.FullName] = descriptions;
                    }
                    Locker.ExitWriteLock();
                }
            }
            finally
            {
                if (Locker.IsWriteLockHeld)
                {
                    Locker.ExitWriteLock();
                }
            }
            descriptions = CachedEnum[enumType.FullName];
            if (descriptions.Count <= 0)
            {
                throw new NotSupportedException("枚举类型[" + enumType.Name + "]未定义属性EnumValueDescription");
            }
            return descriptions;
        }
        /// <param name="enums">得到枚举类型定义所有文本</param>
        /// <returns></returns>
        public static List<Net45.Attribute.EnumDescription> GetFieldInfos(object enums)
        {
          return  GetFieldInfos(enums.GetType());
        }
        /// <summary>
        /// 列标签显示
        /// </summary>
        public static string GetFlagShow<TEnum>(TEnum enumValue, string spliter) where TEnum : struct
        {
            var show = new StringBuilder();
            var enumType = typeof(TEnum);

            var enumInt = enumValue.CastTo<int>();
            if (enumInt == 0) return string.Empty;
            var allTag = GetFieldInfos(enumType);
            allTag.ForEach(tag =>
            {
                if ((enumInt & tag.EnumValue) <= 0) return;
                if (show.Length > 0) { show.Append(spliter); }
                show.Append(tag.EnumDisplayText);
            });
            return show.ToString();
        }
        /// <summary>
        /// 得到枚举项列表
        /// </summary>
        /// <param name="ignores">忽略枚举项</param>
        public static IEnumerable<T> GetEnumList<T>(params T[] ignores)
        {
            var enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("EnumDescription只支持枚举类型");
            }
            var edList = GetFieldInfos(enumType);
            var result = edList.Select(ed => ed.EnumValue.CastTo<T>());
            if (ignores == null || ignores.Length == 0) return result;
            return result.TakeWhile(ed => !ignores.Contains(ed)).ToList();
        }


    }
}