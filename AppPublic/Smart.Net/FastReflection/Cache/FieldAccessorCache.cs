using System.Reflection;
using Smart.Net45.FastReflection.Factory;
using Smart.Net45.FastReflection.Field;

namespace Smart.Net45.FastReflection.Cache
{
    /// <summary>
    /// 字段存取器缓存
    /// </summary>
    public class FieldAccessorCache : FastReflectionCache<FieldInfo, IFieldAccessor>
    {
        /// <summary>
        /// 创建IFieldAccessor接口实例
        /// </summary>
        /// <param name="key">FieldInfo</param>
        /// <returns>IFieldAccessor接口实例</returns>
        protected override IFieldAccessor Create(FieldInfo key)
        {
            return FastReflectionFactories.FieldAccessorFactory.Create(key);
        }
    }
}
