using System.Reflection;
using Smart.Standard.FastReflection.Factory;

namespace Smart.Standard.FastReflection.Method
{

    /// <summary>
    /// 方法Invoker工厂
    /// </summary>
    public class MethodInvokerFactory : IFastReflectionFactory<MethodInfo, IMethodInvoker>
    {
        /// <summary>
        /// 创建IMethodInvoker接口实例
        /// </summary>
        /// <param name="key">方法信息</param>
        /// <returns>IMethodInvoker接口实例</returns>
        public IMethodInvoker Create(MethodInfo key)
        {
            return new MethodInvoker(key);
        }

        /// <summary>
        /// 创建IMethodInvoker接口实例
        /// </summary>
        /// <param name="key">方法信息</param>
        /// <returns>IMethodInvoker接口实例</returns>
        IMethodInvoker IFastReflectionFactory<MethodInfo, IMethodInvoker>.Create(MethodInfo key)
        {
            return Create(key);
        }

    }
}
