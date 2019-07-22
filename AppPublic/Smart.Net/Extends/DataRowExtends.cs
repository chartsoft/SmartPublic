using System;
using System.Data;

namespace Smart.Net45.Extends
{
    /// <summary>
    /// DataRow扩展方法
    /// </summary>
    public static class DataRowExtends
    {
        /// <summary>
        /// 字段是否为空或者不存在
        /// </summary>
        /// <param name="dr">数据源</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns>不存在或者为Null返回true。</returns>
        public static bool FieldIsNullOrNotExist(this DataRow dr, string fieldName)
        {
            return !dr.Table.Columns.Contains(fieldName) || dr[fieldName] == DBNull.Value;
        }
    }
}
