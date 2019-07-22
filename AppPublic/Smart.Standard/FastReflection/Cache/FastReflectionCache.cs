using System.Collections.Generic;
using System.Threading;

namespace Smart.Standard.FastReflection.Cache
{

    /// <summary>
    /// FastReflectionCache 缓存基类
    /// </summary>
    /// <typeparam name="TKey">MethodInfo、ConstructorInfo、PropertyInfo、 FieldInfo</typeparam>
    /// <typeparam name="TValue">Invoker 或 Accessor对象</typeparam>
    public abstract class FastReflectionCache<TKey, TValue> : IFastReflectionCache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> cache = new Dictionary<TKey, TValue>();
        private readonly ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 获取缓存的Invoker 或 Accessor对象
        /// </summary>
        /// <param name="key">MethodInfo、ConstructorInfo、PropertyInfo、 FieldInfo</param>
        /// <returns>Invoker 或 Accessor对象</returns>
        public TValue Get(TKey key)
        {
            TValue value;
            try
            {
                rwLock.EnterUpgradeableReadLock();
                var cacheHit = cache.TryGetValue(key, out value);
                if (cacheHit) return value;
                rwLock.EnterWriteLock();
                if (!cache.TryGetValue(key, out value))
                {
                    value = Create(key);
                    cache[key] = value;
                }
            }
            finally
            {
                if (rwLock.IsUpgradeableReadLockHeld)
                {
                    rwLock.ExitUpgradeableReadLock();
                }
                if (rwLock.IsWriteLockHeld)
                {
                    rwLock.ExitWriteLock();
                }
            }
            return value;
        }

        /// <summary>
        /// 执行真正的 Invoker 或 Accessor对象创建
        /// 需在子类中实现具体创建逻辑
        /// </summary>
        /// <param name="key">MethodInfo、ConstructorInfo、PropertyInfo、 FieldInfo</param>
        /// <returns> Invoker 或 Accessor对象</returns>
        protected abstract TValue Create(TKey key);
    }
}
