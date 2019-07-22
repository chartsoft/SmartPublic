using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Smart.Net45.Helper;

namespace Smart.Net45.Extends
{
    /// <summary>
    /// Enumerable扩展方法
    /// </summary>
    public static class EnumerableExtends
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        public static bool IsNullOrEmpty(this IEnumerable lst)
        {
            if (lst == null) return true;
            return !lst.GetEnumerator().MoveNext();
        }
        /// <summary>
        /// 是否不为空
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this IEnumerable lst)
        {
            return !IsNullOrEmpty(lst);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> lst)
        {
            if (lst == null) return true;
            return !lst.Any();
        }

        /// <summary>
        /// 是否不为空
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> lst)
        {
            return !IsNullOrEmpty(lst);
        }
        /// <summary>
        /// 安全ForEach,当lst不为空，且lst中满足predicate条件的项存在时才执行循环操作
        /// </summary>
        /// <param name="lst">数据</param>
        /// <param name="action">循环执行方法体</param>
        /// <param name="predicate">循环筛选谓词，true执行，false不执行</param>
        /// <param name="changeList">循环执行方法体 是否变更了数据</param>
        public static void SafeForEach<T>(this IEnumerable<T> lst, Action<T> action, Func<T, bool> predicate = null, bool changeList = false)
        {
            ArgumentGuard.ArgumentNotNull("action", action);
            if (lst == null) { return; }
            var trueList = lst.Where(one => predicate == null || predicate(one));
            if (changeList) { trueList = trueList.ToArray(); }

            var enumerable = trueList as T[] ?? trueList.ToArray();
            if (enumerable.IsNullOrEmpty()) return;
            foreach (var t in enumerable)
            {
                action(t);
            }
        }
        /// <summary>
        /// 将一个集合分割成1,2,3的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="separator">分割符号</param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> enumerable, string separator=",")
        {
            return string.Join(separator, enumerable);
        }
        /// <summary>
        /// 先排序然后依次判断两个集合里面的元素是否相等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceEnumerable"></param>
        /// <param name="targeteEnumerable"></param>
        /// <returns></returns>
        public static bool SequenceSortEqual<T>(this IEnumerable<T> sourceEnumerable, IEnumerable<T> targeteEnumerable)
        {
            var sourceList = sourceEnumerable.ToList();
            sourceList.Sort();
            var targeteList = targeteEnumerable.ToList();
            targeteList.Sort();
            return sourceList.SequenceEqual(targeteList);
        }
        /// <summary>
        /// 转成dapper参数 特殊处理
        /// </summary>
        /// <param name="sourceEnumerable"></param>
        /// <param name="sqlParam"></param>
        /// <returns></returns>
        public static DynamicParameters ToPgsqlDapper(this IEnumerable<object> sourceEnumerable, out string sqlParam)
        {
            var dynamicParameters = new DynamicParameters();
            var i = 0;
            var keysList = sourceEnumerable.Select(c =>
            {
                i++;
                dynamicParameters.Add($"p{i}", c);
                return $"@p{i}";
            });
            sqlParam = keysList.Join();
            return dynamicParameters;
        }
        /// <summary>
        /// lst集合forEach循环扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst"></param>
        /// <param name="action">执行方法(index,item)={}</param>
        public static void ForEach<T>(this IEnumerable<T> lst, Action<int, T> action)
        {
            const string throws = "lst is Not Null";
            if (lst == null) throw new ArgumentNullException(throws);
            var list = lst.ToList();
            for (var i = 0; i < list.Count; i++) action(i, list[i]);
        }
        /// <summary>
        /// 切片(范围)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static IEnumerable<T> SliceExt<T>(this IEnumerable<T> lst, string format)
        {
            format = format.Replace("^", "-").Replace("..", "$");
            var aery = format.Split('$');
            var startIndex = 0;
            int endIndex;
            var enumerable1 = lst as T[] ?? lst.ToArray();
            var enumerable = lst as T[] ?? enumerable1.ToArray();
            switch (aery.Length)
            {
                case 0:
                    endIndex = enumerable1.Length - 1;
                    break;
                case 1:
                    startIndex = aery[0].FormatSliceRange(enumerable.Length);
                    endIndex = aery[0].FormatSliceRange(enumerable.Length, false);
                    break;
                case 2:

                    startIndex = aery[0].FormatSliceRange(enumerable.Length);
                    endIndex = aery[1].FormatSliceRange(enumerable.Length, false);
                    break;
                default:
                    throw new Exception("slice format err");
            }

            if (startIndex > endIndex)
                throw new Exception("startIndex cannot be greater than endIndex");
            return startIndex == endIndex ? new List<T> { enumerable1[endIndex] } :
                enumerable1.Skip(startIndex).Take(endIndex - startIndex);
        }

        private static int FormatSliceRange(this string format, int len, bool isStart = true)
        {
            if (format.IsNullOrEmpty()) return isStart ? 0 : len;
            else
                return format.CastTo<int>() >= 0
                    ? format.CastTo<int>()
                    : len + format.CastTo<int>();

        }
    }
}
