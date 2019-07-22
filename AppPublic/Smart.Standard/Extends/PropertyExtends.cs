using System.Reflection;

namespace Smart.Standard.Extends
{
    /// <summary>
    /// 属性扩展
    /// </summary>
    public static class PropertyExtends
    {
        /// <summary>
        /// 取得对应属性的T自定义标签实例
        /// </summary>
        /// <typeparam name="T">自定义标签类型</typeparam>
        /// <param name="property">属性信息</param>
        /// <returns>自定义标签实例</returns>
        public static T GetAttribute<T>(this PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes(true);
            foreach (var att in attributes)
            {
                if (att is T variable)
                    return variable;
            }
            return default(T);
        }

    }
}
