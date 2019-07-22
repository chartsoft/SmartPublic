using System.Reflection;
using Smart.Net45.FastReflection.Factory;

namespace Smart.Net45.FastReflection.Constructor
{
    /// <summary>
    /// 构造函数Invoker工厂
    /// </summary>
    public class ConstructorInvokerFactory : IFastReflectionFactory<ConstructorInfo, IConstructorInvoker>
    {
        /// <summary>
        /// 创建IConstructorInvoker实例
        /// </summary>
        /// <param name="key">构造函数信息</param>
        /// <returns>IConstructorInvoker实例</returns>
        public IConstructorInvoker Create(ConstructorInfo key)
        {
            return new ConstructorInvoker(key);
        }

        /// <summary>
        /// 创建IConstructorInvoker实例
        /// </summary>
        /// <param name="key">构造函数信息</param>
        /// <returns>IConstructorInvoker实例</returns>
        IConstructorInvoker IFastReflectionFactory<ConstructorInfo, IConstructorInvoker>.Create(ConstructorInfo key)
        {
            return Create(key);
        }

    }
}
