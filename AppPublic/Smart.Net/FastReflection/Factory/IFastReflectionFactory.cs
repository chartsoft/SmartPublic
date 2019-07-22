namespace Smart.Net45.FastReflection.Factory
{
    /// <summary>
    /// 快速反射工厂 泛型接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IFastReflectionFactory<TKey, TValue>
    {

        /// <summary>
        /// 创建对应的 Invoker 或 Accessor对象
        /// </summary>
        /// <param name="key">MethodInfo、ConstructorInfo、PropertyInfo、 FieldInfo</param>
        /// <returns>Invoker 或 Accessor对象</returns>
        TValue Create(TKey key);
    }
}
