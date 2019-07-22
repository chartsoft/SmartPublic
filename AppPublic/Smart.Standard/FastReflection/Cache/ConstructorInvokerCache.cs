﻿using System.Reflection;
using Smart.Standard.FastReflection.Constructor;
using Smart.Standard.FastReflection.Factory;

namespace Smart.Standard.FastReflection.Cache
{
    /// <summary>
    /// ConstructorInvoker缓存
    /// </summary>
    public class ConstructorInvokerCache : FastReflectionCache<ConstructorInfo, IConstructorInvoker>
    {
        /// <summary>
        /// 创建IConstructorInvoker接口实例
        /// </summary>
        /// <param name="key">ConstructorInfo</param>
        /// <returns>IConstructorInvoker接口实例</returns>
        protected override IConstructorInvoker Create(ConstructorInfo key)
        {
            return FastReflectionFactories.ConstructorInvokerFactory.Create(key);
        }
    }
}
