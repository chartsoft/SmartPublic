using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Smart.Standard.FastReflection.Field
{
    /// <summary>
    /// 字段访问器接口
    /// </summary>
    public interface IFieldAccessor
    {

        /// <summary>
        /// 获取字段值
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <returns>对应字段的值</returns>
        object GetValue(object instance);

        /// <summary>
        /// 设置字段值
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="value">要给对应字段赋予的值</param>
        void SetValue(object instance, object value);

    }

    /// <summary>
    /// 字段访问器
    /// </summary>
    public class FieldAccessor : IFieldAccessor
    {

        
        private Func<object, object> getter;

        /// <summary>
        /// 字段信息
        /// </summary>
        public FieldInfo FieldInfo { get; private set; }

        /// <summary>
        /// 字段访问器
        /// </summary>
        /// <param name="fieldInfo">字段信息</param>
        public FieldAccessor(FieldInfo fieldInfo)
        {
            FieldInfo = fieldInfo;
            getter = GetDelegate(fieldInfo);
        }
        /// <summary>
        /// 动态委托创建
        /// <remarks>
        /// <![CDATA[
        /// 签名：(object)(((TInstance)instance).Field)
        /// ]]>
        /// </remarks>
        /// </summary>
        /// <param name="fieldInfo">字段信息</param>
        /// <returns>获取字段值 委托方法</returns>
        private Func<object, object> GetDelegate(FieldInfo fieldInfo)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var instanceCast = fieldInfo.IsStatic ? null : Expression.Convert(instance, fieldInfo.ReflectedType);
            var fieldAccess = Expression.Field(instanceCast, fieldInfo);
            var castFieldValue = Expression.Convert(fieldAccess, typeof(object));
            var lambda = Expression.Lambda<Func<object, object>>(castFieldValue, instance);
            return lambda.Compile();
        }

        /// <summary>
        /// IFieldAccessor接口实现
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <returns>属性值</returns>
        public object GetValue(object instance)
        {
            return getter(instance);
        }

        /// <summary>
        /// IFieldAccessor接口实现
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="value">字段值</param>
        public void SetValue(object instance,object value)
        {
            FieldInfo.SetValue(instance, value);
        }

        /// <summary>
        /// IFieldAccessor接口实现
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <returns>属性值</returns>
        object IFieldAccessor.GetValue(object instance)
        {
            return GetValue(instance);
        }

        /// <summary>
        /// IFieldAccessor接口显式实现
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="value">字段值</param>
        void IFieldAccessor.SetValue(object instance,object value)
        {
            FieldInfo.SetValue(instance, value);
        }
    }
}
