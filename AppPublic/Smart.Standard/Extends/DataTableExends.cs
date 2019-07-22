using System;
using System.Collections.Generic;
using System.Data;

namespace Smart.Standard.Extends
{
    /// <summary>
    /// dataTable扩展类
    /// </summary>
    public static class DataTableExends
    {
        /// <summary>  
        /// dataTable转list
        /// </summary>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public static IEnumerable<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            var ts = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                var t = new T();
                var propertys = t.GetType().GetProperties();
                foreach (var pi in propertys)
                {
                    var tempName = pi.Name;
                    if (!dt.Columns.Contains(tempName)) continue;
                    if (!pi.CanWrite) continue;
                    var value = dr[tempName];
                    if (value != DBNull.Value)
                        pi.SetValue(t, value, null);
                }
                ts.Add(t);
            }
            return ts;
        }

     
    }
}
