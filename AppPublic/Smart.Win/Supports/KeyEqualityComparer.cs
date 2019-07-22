using System.Collections.Generic;
using Smart.Net45.Interface;

namespace Smart.Win.Supports
{
    /// <summary>
    /// IKey接口对象相等比较器
    /// </summary>
    /// <typeparam name="T">实现IKey的类型</typeparam>
    public class KeyEqualityComparer<T> : EqualityComparer<T> where T : IKey
    {

        /// <summary>
        /// 相等比较
        /// </summary>
        public override bool Equals(T x, T y)
        {
            var xKey = x as IKey;
            var yKey = y as IKey;
            return xKey != null && yKey != null && !string.IsNullOrEmpty(xKey.Key) && !string.IsNullOrEmpty(yKey.Key) && xKey.Key.Equals(yKey);
        }

        /// <summary>
        /// 取得对象HashCode
        /// </summary>
        public override int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }

    /// <summary>
    /// Key相等比较器
    /// </summary>
    public class KeyEqualityComparer : IEqualityComparer<IKey>
    {
        /// <summary>
        /// 默认比较器实例
        /// </summary>
        public static readonly KeyEqualityComparer Default = new KeyEqualityComparer();

        /// <summary>
        /// 确定指定的对象是否相等
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(IKey x, IKey y)
        {
            if (x == null && y == null)
                return true;

            return x != null && y != null && x.Key == y.Key;
        }

        /// <summary>
        /// 返回指定对象的哈希代码
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(IKey obj)
        {
            if (obj == null || obj.Key == null)
                return 0;
            return obj.Key.GetHashCode();
        }
    }

}
