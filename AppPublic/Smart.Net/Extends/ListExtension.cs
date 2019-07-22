using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Smart.Net45.Helper;

namespace Smart.Net45.Extends
{
    /// <summary>
    /// <see cref="System.Collections.Generic.List{T}"/> 扩展方法
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// 移动到最后
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="itemSelector"></param>
        public static void MoveToEnd<T>(this List<T> list, Predicate<T> itemSelector)
        {
            ArgumentGuard.ArgumentNotNull("list", list);
            if (list.Count > 1)
                list.Move(itemSelector, list.Count - 1);
        }
        /// <summary>
        /// 移动到最顶端
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="itemSelector"></param>
        public static void MoveToBeginning<T>(this List<T> list, Predicate<T> itemSelector)
        {
            ArgumentGuard.ArgumentNotNull("list", list);
            list.Move(itemSelector, 0);
        }
        /// <summary>
        /// 上移
        /// </summary>
        public static void MoveUp<T>(this List<T> list, T item)
        {
            if (list.IsNullOrEmpty() || item == null)
                return;

            var currIndex = list.IndexOf(item);

            if (currIndex <= 0)
                return;

            // 交换当前元素与上一元素
            list[currIndex] = list[currIndex - 1];
            list[currIndex - 1] = item;
        }
        /// <summary>
        /// 下移
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public static void MoveDown<T>(this List<T> list, T item)
        {
            if (list.IsNullOrEmpty() || item == null)
                return;

            var currIndex = list.IndexOf(item);

            if (currIndex < 0 || currIndex == list.Count - 1)
                return;

            // 交换当前元素与下一元素
            list[currIndex] = list[currIndex + 1];
            list[currIndex + 1] = item;
        }
        /// <summary>
        /// 移动一个元素到
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="itemSelector"></param>
        /// <param name="newIndex"></param>
        public static void Move<T>(this List<T> list, Predicate<T> itemSelector, int newIndex)
        {
            ArgumentGuard.ArgumentNotNull("list", list);
            ArgumentGuard.ArgumentNotNull("itemSelector", itemSelector);

            if (newIndex < 0)
                return;

            var currentIndex = list.FindIndex(itemSelector);
            if (currentIndex < 0)
                return;

            if (currentIndex == newIndex)
                return;

            // Copy the item
            var item = list[currentIndex];

            // Remove the item from the list
            list.RemoveAt(currentIndex);

            // Finally insert the item at the new index
            list.Insert(newIndex, item);
        }
        /// <summary>
        /// IEnumerable转化成DataTable
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                var t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }
            foreach (var item in items)
            {
                var values = new object[props.Length];

                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }
            return tb;
        }
        #region [Supports]
        private static Type GetCoreType(Type t)
        {
            if (t != null && t.IsNullableType())
            {
                return !t.IsValueType ? t : Nullable.GetUnderlyingType(t);
            }
            return t;
        }
        #endregion
    }
}
