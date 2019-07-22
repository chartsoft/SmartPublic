using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Smart.Standard.Enum;
using Smart.Standard.ParseProviders;

namespace Smart.Standard.Extends
{
    /// <summary>
    /// Object扩展方法
    /// </summary>
    public static class ObjectExtends
    {

        #region [RemoveEvent]

        /// <summary>
        /// 移除事件绑定
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="eventDelegate">事件委托</param>
        /// <param name="eventName">事件名称</param>
        public static void RemoveEvent<T>(this T entity, Delegate eventDelegate, string eventName) where T : class
        {
            if (entity == null || eventDelegate == null || string.IsNullOrEmpty(eventName)) { return; }
            var invokeList = eventDelegate.GetInvocationList();
            if (invokeList.Length == 0) { return; }
            var e = typeof(T).GetEvent(eventName);
            if (e == null) { return; }
            foreach (var del in invokeList)
            {
                e.RemoveEventHandler(entity, del);
            }
        }

        #endregion

        #region [ConvertTo]

        /// <summary>
        /// 转换对象为特定的类型，默认值为 default(T)
        /// </summary>
        /// <typeparam name = "T">要转换的类型</typeparam>
        /// <param name = "value">要转换的object</param>
        /// <returns>转换后T</returns>
        public static T ConvertTo<T>(this object value)
        {
            return value.ConvertTo(default(T));
        }

        /// <summary>
        /// 转换对象为特定的类型，默认值为 default(T)
        /// </summary>
        /// <typeparam name = "T">要转换的类型</typeparam>
        /// <param name = "value">要转换的object</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换后T</returns>
        public static T ConvertTo<T>(this object value, T defaultValue)
        {
            if (value == null) return defaultValue;
            var targetType = typeof(T);
            if (value.GetType() == targetType) return (T)value;
            var converter = TypeDescriptor.GetConverter(value);
            if (converter.CanConvertTo(targetType))
                return (T)converter.ConvertTo(value, targetType);
            converter = TypeDescriptor.GetConverter(targetType);
            if (converter.CanConvertFrom(value.GetType()))
                return (T)converter.ConvertFrom(value);
            return defaultValue;
        }

        /// <summary>
        /// 转换对象为特定的类型，默认值为 default(T)
        /// </summary>
        /// <typeparam name = "T">要转换的类型</typeparam>
        /// <param name = "value">要转换的object</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="ignoreException">忽略异常</param>
        /// <returns>转换后T</returns>
        public static T ConvertTo<T>(this object value, T defaultValue, bool ignoreException)
        {
            if (!ignoreException) return value.ConvertTo<T>();
            try
            {
                return value.ConvertTo<T>();
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion

        #region [CanCovertTo]

        /// <summary>
        /// 对象是否能被转换为特定的类型
        /// </summary>
        /// <param name="value">对象</param>
        public static bool CanConvertTo<T>(this object value)
        {
            var targetType = typeof(T);
            return value.CanConvertTo(targetType);
        }

        /// <summary>
        /// 对象是否能被转换为特定的类型
        /// </summary>
        /// true则value可以转换为T，否则false
        /// <param name="targetType">类型</param>
        /// <param name="value">对象</param>
        public static bool CanConvertTo(this object value, Type targetType)
        {
            if (value == null || targetType == null) return false;
            try
            {
                return CastTo(value, targetType) != null;
            }
            catch
            {
                return false;
            }
            ////var converter = TypeDescriptor.GetConverter(value);
            ////if (converter != null)
            ////{
            ////    if (converter.CanConvertTo(targetType))
            ////    {
            ////        try
            ////        {
            ////            converter.ConvertTo(value, targetType);
            ////            return true;
            ////        }
            ////        catch
            ////        {
            ////            return false;
            ////        }
            ////    }
            ////}
            ////converter = TypeDescriptor.GetConverter(targetType);
            ////if (converter != null)
            ////{
            ////    if (converter.CanConvertFrom(value.GetType()))
            ////    {
            ////        try
            ////        {
            ////            converter.ConvertFrom(value);
            ////            return true;
            ////        }
            ////        catch
            ////        {
            ////            return false;
            ////        }
            ////    }
            ////}
        }
        #endregion

        #region [InvokeMethod]

        /// <summary>
        /// 通过反射动态调用方法methodName
        /// </summary>
        /// <param name = "obj">持有方法的对象</param>
        /// <param name = "methodName">方法名</param>
        /// <param name = "parameters">参数列表</param>
        /// <returns>方法返回值</returns>
        public static object InvokeMethod(this object obj, string methodName, params object[] parameters)
        {
            return InvokeMethod<object>(obj, methodName, parameters);
        }

        /// <summary>
        /// 通过反射动态调用方法methodName，返回强类型对象
        /// </summary>
        /// <typeparam name = "T">期望返回的类型</typeparam>
        /// <param name = "obj">持有方法的对象</param>
        /// <param name = "methodName">方法名</param>
        /// <param name = "parameters">参数列表</param>
        /// <returns>返回值</returns>
        public static T InvokeMethod<T>(this object obj, string methodName, params object[] parameters)
        {
            var type = obj.GetType();
            var method = type.GetMethod(methodName, parameters.Select(o => o.GetType()).ToArray());

            if (method == null)
                throw new ArgumentException($"Method '{methodName}' not found.", methodName);

            var value = method.Invoke(obj, parameters);
            return (value is T variable ? variable : default(T));
        }

        #endregion

        #region [GetPropertyValue]

        /// <summary>
        /// 通过反射动态获取属性值
        /// </summary>
        /// <param name = "obj">持有属性的对象</param>
        /// <param name = "propertyName">属性名</param>
        /// <returns>属性值</returns>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return GetPropertyValue<object>(obj, propertyName, null);
        }


        /// <summary>
        /// 取得强类型属性值
        /// </summary>
        /// <typeparam name = "T">返回值类型</typeparam>
        /// <param name = "obj">持有属性的对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name = "propertyName">属性名</param>
        /// <returns>属性值</returns>
        public static T GetPropertyValue<T>(this object obj, string propertyName, T defaultValue)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);

            if (property == null)
                throw new ArgumentException($"属性'{propertyName}'不存在.", propertyName);

            var value = property.GetValue(obj, null);
            return (value is T variable ? variable : defaultValue);
        }

        #endregion

        #region [SetPropertyValue]

        /// <summary>
        /// 动态设置属性值
        /// </summary>
        /// <param name = "obj">要设置属性的对象</param>
        /// <param name = "propertyName">属性名</param>
        /// <param name = "value">值</param>
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);

            if (property == null)
                throw new ArgumentException($"属性'{propertyName}'不存在.", propertyName);
            if (property.CanWrite)
                property.SetValue(obj, value, null);
            //throw new ArgumentException(string.Format("属性'{0}'不可写.", propertyName), propertyName);
        }

        #endregion

        #region [GetAttribute]

        /// <summary>
        /// 取得对象的标签实例
        /// </summary>
        /// <typeparam name = "T">要取得的的标签类型</typeparam>
        /// <param name = "obj">要获取的对象实例</param>
        /// <returns>标签实例</returns>
        public static T GetAttribute<T>(this object obj) where T : System.Attribute
        {
            return GetAttribute<T>(obj, true);
        }

        /// <summary>
        /// 取得对象的标签实例
        /// </summary>
        /// <typeparam name = "T">要取得的的标签类型</typeparam>
        /// <param name = "obj">要获取的对象实例</param>
        /// <param name="includeInherited">是否包括继承的类型</param>
        /// <returns>标签实例</returns>
        public static T GetAttribute<T>(this object obj, bool includeInherited) where T : System.Attribute
        {
            var type = (obj as Type) ?? obj.GetType();
            var attributes = type.GetCustomAttributes(typeof(T), includeInherited);
            return attributes.FirstOrDefault() as T;
        }
        #endregion

        #region [GetAttributes]

        /// <summary>
        /// 取得所有的T类型标签
        /// </summary>
        /// <typeparam name = "T">标签类型</typeparam>
        /// <param name = "obj">获取对象</param>
        /// <returns>所有T类型标签</returns>
        public static IEnumerable<T> GetAttributes<T>(this object obj) where T : System.Attribute
        {
            return GetAttributes<T>(obj, false);
        }

        /// <summary>
        /// 取得所有的T类型标签
        /// </summary>
        /// <typeparam name = "T">标签类型</typeparam>
        /// <param name = "obj">获取对象</param>
        /// <param name="includeInherited">是否包括继承的类型</param>
        /// <returns>所有T类型标签</returns>
        public static IEnumerable<T> GetAttributes<T>(this object obj, bool includeInherited) where T : System.Attribute
        {
            return ((obj as Type) ?? obj.GetType()).GetCustomAttributes(typeof(T), includeInherited).OfType<T>().Select(attribute => attribute);
        }

        #endregion

        #region [IsType]

        /// <summary>
        /// 对象是否为T类型
        /// </summary>
        public static bool IsType<T>(this object obj)
        {
            var t = typeof(T);
            return (obj.GetType() == t);
        }

        /// <summary>
        /// 取得对应类型默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetTypeDefaultValue<T>(this T value)
        {
            return default(T);
        }

        #endregion

        #region [AsString]

        /// <summary>
        /// 转换为string
        /// </summary>
        /// <returns>
        /// target=null返回null，否则等同于ToString()
        /// </returns>
        /// <param name = "target">要ToString的对象</param>
        public static string AsString(this object target)
        {
            return target?.ToString();
        }

        #endregion

        #region [Cast]
        /// <summary>
        /// 转换类型
        /// </summary>
        public static T CastTo<T>(this object value)
        {
            if (value == null || value.ToString().Length.Equals(0)) return default(T);
            if (typeof(T) == typeof(string) && value.ToString().Length == 0) return (T)Convert.ChangeType(string.Empty, typeof(string)); //特殊处理空字符串的转换
            var convertType = typeof(T);
            if (convertType.IsGenericType && convertType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var nullableConverter = new NullableConverter(convertType);
                convertType = nullableConverter.UnderlyingType;
            }
            else if (convertType.IsEnum)
            {
                return (T)Convert.ChangeType(value, typeof(int));
            }
            else if (value is T)
            {
                return (T)value;
            }
            return (T)Convert.ChangeType(value, convertType);
        }

        /// <summary>
        /// 转换类型
        /// </summary>
        public static object CastTo(this object value, Type convertType)
        {
            if (value == null || value.ToString().Length == 0 || convertType == null) return null;
            if (convertType == typeof(string) && value.ToString().Length == 0) return string.Empty;//特殊处理空字符串的转换
            if (convertType.IsGenericType && convertType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var nullableConverter = new NullableConverter(convertType);
                convertType = nullableConverter.UnderlyingType;
            }
            else if (convertType.IsEnum)
            {
                return Convert.ChangeType(value, typeof(int));
            }
            return Convert.ChangeType(value, convertType);
        }

        #endregion

        #region [Clone]

        /// <summary>
        /// 克隆对象(深度克隆)
        /// </summary>
        /// <typeparam name="T">克隆对象类型</typeparam>
        /// <param name="source">克隆源</param>
        /// <returns>深度克隆对象</returns>
        public static T Clone<T>(this T source) where T : new()
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("对象不支持序列化，请在对象上添加[Serializable]标签");
            }
            // null对象返回“类型默认值”
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
        #endregion

        #region[Pack]
        /// <summary>
        /// Json打包对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>序列化后字符串</returns>
        public static string PackJson<T>(this T entity)
        {
            return Pack(entity, JsonParseProvider.ProviderName);
        }
        /// <summary>
        /// Xml打包对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>序列化后字符串</returns>
        public static string PackXml<T>(this T entity)
        {
            return Pack(entity, XmlParseProvider.ProviderName);
        }
        /// <summary>
        /// 打包对象
        /// </summary>
        /// <typeparam name="T">克隆对象类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="parserName">解析器名称</param>
        /// <returns>序列化后字符串</returns>
        public static string Pack<T>(this T entity, string parserName)
        {
            if (string.IsNullOrWhiteSpace(parserName) || entity == null) return string.Empty;
            var parser = ParserFactory.GetParser(parserName, true);
            if (parser == null)
                throw new Exception($"不支持数据提供器{parserName}");
            return parser.Pack(entity);
        }
        #endregion

        #region [Ellipsis]

        /// <summary>
        /// 银行数字计算方法
        /// </summary>
        /// <param name="number">未处理的数字</param>
        /// <param name="digits">保留几位</param>
        /// <param name="ellipsisKinds">算法枚举默认四舍六入五成双</param>
        /// <returns></returns>
        public static decimal Ellipsis(this object number,int digits=2,EllipsisKinds ellipsisKinds=EllipsisKinds.Atwain)
        {
            var d = number.CastTo<double>();
            decimal result;
            var ies = Math.Pow(10, digits);
            switch (ellipsisKinds)
            {
                case EllipsisKinds.RoundingDown:
                    result = (Math.Floor(d * ies) / ies).CastTo<decimal>();
                    break;
                case EllipsisKinds.WindUp:
                    result = (Math.Ceiling(d * ies) / ies).CastTo<decimal>();
                    break;
                case EllipsisKinds.Round:
                    result = decimal.Parse(d.ToString($"f{digits}"));
                    break;
                case EllipsisKinds.Atwain:
                    result = Math.Round(d, digits).CastTo<decimal>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ellipsisKinds), ellipsisKinds, null);
            }

            return result;
        }

        #endregion

    }
}