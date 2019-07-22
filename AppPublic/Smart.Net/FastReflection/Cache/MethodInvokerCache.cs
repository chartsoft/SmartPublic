using System.Reflection;
using Smart.Net45.FastReflection.Factory;
using Smart.Net45.FastReflection.Method;

namespace Smart.Net45.FastReflection.Cache
{
    /// <summary>
    /// 方法Invoker缓存
    /// </summary>
    public class MethodInvokerCache : FastReflectionCache<MethodInfo, IMethodInvoker>
    {
        /// <summary>
        /// 创建IMethodInvoker接口实例
        /// </summary>
        /// <param name="key">MethodInfo</param>
        /// <returns>IMethodInvoker接口实例</returns>
        protected override IMethodInvoker Create(MethodInfo key)
        {
            return FastReflectionFactories.MethodInvokerFactory.Create(key);
        }
    }
}
