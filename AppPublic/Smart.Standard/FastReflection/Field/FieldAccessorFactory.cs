using System.Reflection;
using Smart.Standard.FastReflection.Factory;

namespace Smart.Standard.FastReflection.Field
{
    /// <summary>
    /// 字段存取器工厂
    /// </summary>
    public class FieldAccessorFactory : IFastReflectionFactory<FieldInfo, IFieldAccessor>
    {

        /// <summary>
        /// 创建字段访问器 接口实现
        /// </summary>
        /// <param name="key">字段信息</param>
        /// <returns>字段访问器实例</returns>
        public IFieldAccessor Create(FieldInfo key)
        {
            return new FieldAccessor(key);
        }

        /// <summary>
        /// 创建字段访问器 接口显示实现
        /// </summary>
        /// <param name="key">字段信息</param>
        /// <returns>字段访问器实例</returns>
        IFieldAccessor IFastReflectionFactory<FieldInfo, IFieldAccessor>.Create(FieldInfo key)
        {
            return Create(key);
        }

    }
}
