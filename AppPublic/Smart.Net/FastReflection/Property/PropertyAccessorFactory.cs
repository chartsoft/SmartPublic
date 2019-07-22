using System.Reflection;
using Smart.Net45.FastReflection.Factory;

namespace Smart.Net45.FastReflection.Property
{
    /// <summary>
    /// 属性存取器工厂
    /// </summary>
    public class PropertyAccessorFactory : IFastReflectionFactory<PropertyInfo, IPropertyAccessor>
    {
        /// <summary>
        /// 创建IPropertyAccessor实例
        /// </summary>
        /// <param name="key">属性信息</param>
        /// <returns>IPropertyAccessor实例</returns>
        public IPropertyAccessor Create(PropertyInfo key)
        {
            return new PropertyAccessor(key);
        }

        /// <summary>
        /// 创建IPropertyAccessor实例
        /// </summary>
        /// <param name="key">属性信息</param>
        /// <returns>IPropertyAccessor实例</returns>
        IPropertyAccessor IFastReflectionFactory<PropertyInfo, IPropertyAccessor>.Create(PropertyInfo key)
        {
            return Create(key);
        }

    }
}
