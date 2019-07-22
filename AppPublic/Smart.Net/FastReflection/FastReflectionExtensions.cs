using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Smart.Net45.Extends;
using Smart.Net45.FastReflection.Cache;

namespace Smart.Net45.FastReflection
{
    /// <summary>
    /// 快速反射扩展方法
    /// </summary>
    public static class FastReflectionExtensions
    {
        #region[Field|Extends]
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> TypeProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> ComputedProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        /// <summary>
        /// 方法Invoke执行
        /// </summary>
        /// <param name="methodInfo">反射方法信息</param>
        /// <param name="instance">对象实例</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>方法执行结果</returns>
        public static T FastInvoke<T>(this MethodInfo methodInfo, object instance, params object[] parameters)
        {
            return FastInvoke(methodInfo, instance, parameters).CastTo<T>();
        }
        /// <summary>
        /// 方法Invoke执行
        /// </summary>
        /// <param name="methodInfo">反射方法信息</param>
        /// <param name="instance">对象实例</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>方法执行结果</returns>
        public static object FastInvoke(this MethodInfo methodInfo, object instance, params object[] parameters)
        {
            return FastReflectionCaches.MethodInvokerCache.Get(methodInfo).Invoke(instance, parameters);
        }
        /// <summary>
        /// 构造函数反射调用
        /// </summary>
        /// <param name="constructorInfo">反射构造函数信息</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>对象实例</returns>
        public static object FastInvoke<T>(this ConstructorInfo constructorInfo, params object[] parameters)
        {
            return FastInvoke(constructorInfo, parameters).CastTo<T>();
        }
        /// <summary>
        /// 构造函数反射调用
        /// </summary>
        /// <param name="constructorInfo">反射构造函数信息</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>对象实例</returns>
        public static object FastInvoke(this ConstructorInfo constructorInfo, params object[] parameters)
        {
            return FastReflectionCaches.ConstructorInvokerCache.Get(constructorInfo).Invoke(parameters);
        }
        /// <summary>
        /// 通过反射获取属性值
        /// </summary>
        /// <param name="propertyInfo">反射属性信息</param>
        /// <param name="instance">对象实例</param>
        /// <returns>属性值</returns>
        public static T FastGetValue<T>(this PropertyInfo propertyInfo, object instance)
        {
            return FastGetValue(propertyInfo, instance).CastTo<T>();
        }
        /// <summary>
        /// 通过反射获取属性值
        /// </summary>
        /// <param name="propertyInfo">反射属性信息</param>
        /// <param name="instance">对象实例</param>
        /// <returns>属性值</returns>
        public static object FastGetValue(this PropertyInfo propertyInfo, object instance)
        {
            return FastReflectionCaches.PropertyAccessorCache.Get(propertyInfo).GetValue(instance);
        }
        /// <summary>
        /// 通过反射设置值
        /// </summary>
        /// <param name="propertyInfo">反射属性信息</param>
        /// <param name="instance">对象实例</param>
        /// <param name="value">属性值</param>
        public static void FastSetValue(this PropertyInfo propertyInfo, object instance, object value)
        {
            FastReflectionCaches.PropertyAccessorCache.Get(propertyInfo).SetValue(instance, value);
        }
        /// <summary>
        /// 通过反射获取字段值
        /// </summary>
        /// <param name="fieldInfo">反射字段信息</param>
        /// <param name="instance">对象实例</param>
        /// <returns>字段值</returns>
        public static T FastGetValue<T>(this FieldInfo fieldInfo, object instance)
        {
            return FastGetValue(fieldInfo, instance).CastTo<T>();
        }

        /// <summary>
        /// 通过反射获取字段值
        /// </summary>
        /// <param name="fieldInfo">反射字段信息</param>
        /// <param name="instance">对象实例</param>
        /// <returns>字段值</returns>
        public static object FastGetValue(this FieldInfo fieldInfo, object instance)
        {
            return FastReflectionCaches.FieldAccessorCache.Get(fieldInfo).GetValue(instance);
        }

        /// <summary>
        /// 通过反射设置字段值
        /// </summary>
        /// <param name="fieldInfo">反射字段信息</param>
        /// <param name="instance">对象实例</param>
        /// <param name="value">要设置的值</param>
        /// <returns>字段值</returns>
        public static void FastSetValue(this FieldInfo fieldInfo, object instance, object value)
        {
            FastReflectionCaches.FieldAccessorCache.Get(fieldInfo).SetValue(instance, value);
        }

        /// <summary>
        /// 获取一个类的所有字段集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesCache(this Type type)
        {
            if (ComputedProperties.TryGetValue(type.TypeHandle, out var pi)) return pi.ToList();
            var computedProperties = TypePropertiesCache(type).ToList();
            ComputedProperties[type.TypeHandle] = computedProperties;
            return computedProperties;
        }
        #endregion

        #region[Supports]
        private static IEnumerable<PropertyInfo> TypePropertiesCache(Type type)
        {
            if (TypeProperties.TryGetValue(type.TypeHandle, out var pis)) return pis.ToList();
            var properties = type.GetProperties().ToArray();
            TypeProperties[type.TypeHandle] = properties;
            return properties.ToList();
        }
        #endregion
    }
}
