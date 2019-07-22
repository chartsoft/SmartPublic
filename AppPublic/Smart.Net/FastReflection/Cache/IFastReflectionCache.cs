namespace Smart.Net45.FastReflection.Cache
{

    /// <summary>
    /// 快速反射缓存 泛型接口
    /// </summary>
    /// <typeparam name="TKey">元数据信息</typeparam>
    /// <typeparam name="TValue">对应存取器</typeparam>
    public interface IFastReflectionCache<TKey, TValue>
    {

        /// <summary>
        /// 获取缓存的Invoker 或 Accessor对象
        /// </summary>
        /// <param name="key">MethodInfo、ConstructorInfo、PropertyInfo、 FieldInfo</param>
        /// <returns>Invoker 或 Accessor对象</returns>
        TValue Get(TKey key);
    }
}
