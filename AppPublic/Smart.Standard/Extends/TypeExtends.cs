using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Smart.Standard.Extends
{
    /// <summary>
    /// Type扩展
    /// </summary>
    public static class TypeExtends
    {
        /// <summary>
        /// 取得type的T特性实例
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="type">Type实例</param>
        /// <returns>T实例</returns>
        public static T GetAttribute<T>(this Type type)
        {
            var attributes = type.GetCustomAttributes(true);
            foreach (var att in attributes)
            {
                if (att is T variable)
                {
                    return variable;
                }
            }
            return default(T);
        }

        /// <summary>
        /// 取得Type实例对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetInstance(this Type type)
        {
            return type.Assembly.CreateInstance(type.FullName ?? throw new InvalidOperationException());
        }

        /// <summary>
        /// 返回一个类所有方法的集合
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isfilter"></param>
        /// <returns></returns>
        public static IEnumerable<MethodInfo> GetMethodInfos(this Type type, bool isfilter = false)
        {
            var types = type.GetMethods();
            //var listFilter = SmartConsts.FilterReflex.Split(',');
            return !isfilter ? types : types.Where(c => c.DeclaringType!=null&&c.DeclaringType==type);
        }
        /// <summary>
        /// 获取所有基类的子类
        /// </summary>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypesByBase(this Type baseType)
        {
            var subTypeQuery = from t in Assembly.GetCallingAssembly().GetTypes()
                where t.IsSubclassOf(baseType)
                select t;
            return subTypeQuery;
        }

        /// <summary>
        /// 获取所有基类的子类
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="dllName"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypesByBase(this Type baseType, string dllName)
        {
            var subTypeQuery = from t in Assembly.Load(dllName).GetTypes()
                where t.IsSubclassOf(baseType)
                select t;
            return subTypeQuery;
        }
        /// <summary>
        /// 判断一个是不是基类的子类
        /// </summary>
        /// <param name="type"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static bool IsSubClassOf(this Type type, Type baseType)
        {
            var b = type.BaseType;
            while (b != null)
            {
                if (b == baseType)
                {
                    return true;
                }
                b = b.BaseType;
            }
            return false;
        }
        #region [Numeric]

        /// <summary>
        /// 判断是否为数值类型。
        /// </summary>
        /// <param name="t">要判断的类型</param>
        /// <returns>是否为数值类型</returns>
        public static bool IsNumericType(this Type t)
        {
            var tc = Type.GetTypeCode(t);
            return (t.IsPrimitive && t.IsValueType && !t.IsEnum && tc != TypeCode.Char && tc != TypeCode.Boolean) || tc == TypeCode.Decimal;
        }

        /// <summary>
        /// 判断是否为可空数值类型。
        /// </summary>
        /// <param name="t">要判断的类型</param>
        /// <returns>是否为可空数值类型</returns>
        public static bool IsNumericOrNullableNumericType(this Type t)
        {
            return t.IsNumericType() || (t.IsNullableType() && Nullable.GetUnderlyingType(t).IsNumericType());
        }

        /// <summary>
        /// 
        /// 判断是否为可空类型。
        /// 注意，直接调用可空对象的.GetType()方法返回的会是其泛型值的实际类型，用其进行此判断肯定返回false。
        /// </summary>
        /// <param name="t">要判断的类型</param>
        /// <returns>是否为可空类型</returns>
        public static bool IsNullableType(this Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 取属性的显示名称
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static string GetPropertyDisplayName(this Type type, string propertyName)
        {
            return type.GetPropertyAttribute<DisplayAttribute>(propertyName)?.Name;
        }

        /// <summary>
        /// 取属性的attribute
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="propertyName">属性名</param>
        /// <typeparam name="T">attribute</typeparam>
        /// <returns>attribute</returns>
        public static T GetPropertyAttribute<T>(this Type type, string propertyName)
        {
            var property = type.GetProperty(propertyName);
            return property == null ? default(T) : property.GetAttribute<T>();
        }

        #endregion

    }
}
