using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Smart.Net45.Attribute;
using Smart.Net45.Extends;
using Smart.Win.Entitys;

namespace Smart.Win.Helpers
{
    /// <summary>
    /// RepositoryComboBox辅助类
    /// </summary>
    public class RepositoryComboBoxHelper
    {
        /// <summary>
        /// 绑定枚举到下拉框
        /// </summary>
        /// <param name="ctr">控件</param>
        /// <param name="header">请选择等文字</param>
        /// <param name="except">不绑定项</param>
        /// <typeparam name="T">枚举类型</typeparam>
        public static void BindEnumToRepositoryCombo<T>(RepositoryItemComboBox ctr, string header = null, List<T> except = null)
        {
            var t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ArgumentException(t.FullName + "不是枚举类型");
            }
            if (!string.IsNullOrEmpty(header))
            {
                ctr.Items.Add(header);
            }
            var eds = EnumDescription.GetFieldInfos(typeof(T));
            if (eds != null && eds.Count > 0 && except != null && except.Count > 0)
            {
                var exceptValue = except.Select(old => old.CastTo<int>()).ToList();
                foreach (var ed in eds)
                {
                    if (!exceptValue.Contains(ed.EnumValue))
                    {
                        ctr.Items.Add(ed);
                    }
                }
            }
            else if (eds != null && eds.Count > 0)
            {
                ctr.Items.AddRange(eds);
            }
            ctr.TextEditStyle = TextEditStyles.DisableTextEditor;
        }

        /// <summary>
        /// 绑定对象列表到下拉框
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="ctr">下拉控件</param>
        /// <param name="datas">数据列表</param>
        /// <param name="displayFunc">取得显示文字方法</param>
        /// <param name="header">请选择等文字</param>
        public static void BindListToCombo<T>(RepositoryItemComboBox ctr, List<T> datas, Func<T, string> displayFunc, string header = null) where T : class
        {
            if (!string.IsNullOrEmpty(header))
            {
                ctr.Items.Add(header);
            }
            if (datas != null && datas.Count > 0)
            {
                var bindDatas = datas.Select(old => new IdTextData { Data = old, Text = displayFunc(old) }).ToList();
                ctr.Items.AddRange(bindDatas);
            }
            ctr.TextEditStyle = TextEditStyles.DisableTextEditor;
        }
    }
}
