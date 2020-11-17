using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Smart.Net45.FastReflection;

namespace Smart.Net45.Extends
{
    /// <summary>
    /// 对象映射器类
    ///     用于将对象的属性值按名称映射到另外一个对象
    ///     
    /// </summary>
    public static class ObjectMapper
    {
        #region [Fields]
        private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> TypePropertiesDictionary = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
        #endregion

        #region [Methods]
        /// <summary>
        /// 获取指定类型的对象，并从源对象获取同名属性值
        /// </summary>
        /// <typeparam name="TSource">源对象类型</typeparam>
        /// <typeparam name="TTarget">目标对象类型</typeparam>
        /// <param name="sourceObject">源对象</param>
        /// <param name="customeConvertAction">对实体执行的自定义操作，不应该有对模型的修改代码</param>
        /// <returns>目标对象</returns>
        public static TTarget Map<TSource, TTarget>(this TSource sourceObject, Action<TSource, TTarget> customeConvertAction = null) where TTarget : new()
        {
            if (sourceObject == null)
                return default(TTarget);

            var result = ConvertObject<TTarget>(sourceObject);

            customeConvertAction?.Invoke(sourceObject, result);

            return result;
        }
        /// <summary>
        /// 获取指定类型的对象，并从源对象获取同名属性值
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="sourceObject"></param>
        /// <param name="customeConvertAction"></param>
        /// <returns></returns>
        public static TTarget Map<TTarget>(this object sourceObject,
            Action<object, TTarget> customeConvertAction = null)where TTarget : new()
        {
            if (sourceObject == null)
                return default(TTarget);

            var result = ConvertObject<TTarget>(sourceObject);

            customeConvertAction?.Invoke(sourceObject, result);

            return result;
        }

        /// <summary>
        /// 获取指定类型的对象的列表，并从源列表中对象获取同名属性值
        /// </summary>
        /// <typeparam name="TSource">源对象类型</typeparam>
        /// <typeparam name="TTarget">目标对象类型</typeparam>
        /// <param name="sourceObjectList">源对象列表</param>
        /// <param name="customeConvertAction">对实体执行的自定义操作，不应该有对模型的修改代码</param>
        /// <returns>目标对象列表</returns>
        public static IEnumerable<TTarget> Map<TSource, TTarget>(this IEnumerable<TSource> sourceObjectList, Action<TSource, TTarget> customeConvertAction = null) where TTarget : new()
        {
            return sourceObjectList.Select(sourceObject => Map(sourceObject, customeConvertAction));
        }

        /// <summary>
        /// 获取指定类型的对象的列表，并从源列表中对象获取同名属性值
        /// </summary>
        
        /// <typeparam name="TTarget">目标对象类型</typeparam>
        /// <param name="sourceObjectList">源对象列表</param>
        /// <param name="customeConvertAction">对实体执行的自定义操作，不应该有对模型的修改代码</param>
        /// <returns>目标对象列表</returns>
        public static IEnumerable<TTarget> Map<TTarget>(this IEnumerable<object> sourceObjectList, Action<object, TTarget> customeConvertAction = null) where TTarget : new()
        {
            return sourceObjectList.Select(sourceObject => Map(sourceObject, customeConvertAction));
        }
        /// <summary>
        /// 获取指定类型的对象，并从源对象获取同名属性值
        /// </summary>
        /// <typeparam name="TTarget">目标对象类型</typeparam>
        /// <param name="sourceObject">源对象</param>
        /// <returns>目标对象</returns>
        private static TTarget ConvertObject< TTarget>(this object sourceObject)
            where TTarget : new()
        {
            var sourceType = sourceObject.GetType();
            var targetType = typeof(TTarget);

            var sourceTypePropertiesDictionary = GetOrCreatePropertiesDictionary(sourceType);
            var targetTypePropertiesDictionary = GetOrCreatePropertiesDictionary(targetType);

            var targetObject = new TTarget();

            foreach (var targetPropInfo in targetTypePropertiesDictionary.Values)
            {
                // 目标属性可以，存在同名源属性，且源属性可读
                if (targetPropInfo.CanWrite && sourceTypePropertiesDictionary.TryGetValue(targetPropInfo.Name, out var sourcePropInfo)
                    && sourcePropInfo.CanRead)
                {
                    var propValue = sourcePropInfo.FastGetValue(sourceObject);
                    if (targetPropInfo.PropertyType != sourcePropInfo.PropertyType &&
                        !targetPropInfo.PropertyType.IsSubclassOf(sourcePropInfo.PropertyType))

                        try
                        {
                            // 有可能类型转换不成功，但是不应该影响其他属性的赋值
                            propValue = propValue.CastTo(targetPropInfo.PropertyType);
                        }
                        catch (Exception)
                        {
                            // 继续下一个属性的处理
                            continue;
                        }

                    targetPropInfo.FastSetValue(targetObject, propValue);
                }

            }

            return targetObject;
        }
        /// <summary>
        /// 从字典中获取指定类型的属性列表，如果不存在则创建
        /// </summary>
        /// <param name="sourceType">指定类型</param>
        /// <returns>所有属性列表</returns>
        private static Dictionary<string, PropertyInfo> GetOrCreatePropertiesDictionary(Type sourceType)
        {
            if (!TypePropertiesDictionary.TryGetValue(sourceType, out var sourceTypeProperties))
            {
                sourceTypeProperties = sourceType.GetProperties().ToDictionary(p => p.Name);
                TypePropertiesDictionary[sourceType] = sourceTypeProperties;
            }

            return sourceTypeProperties;
        }

        #endregion
    }
}
