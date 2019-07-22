using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Smart.Net45.Attribute;
using Smart.Net45.Extends;
using Smart.Net45.Interface;
using Smart.Win.Entitys;
using Smart.Win.Supports;

namespace Smart.Win.Helpers
{
    /// <summary>
    /// 下拉控件辅助类
    /// </summary>
    public class ComboBoxHelper
    {
        /// <summary>
        /// 绑定枚举到下拉框
        /// </summary>
        /// <param name="ctr">控件</param>
        /// <param name="header">请选择等文字</param>
        /// <param name="except">不绑定项</param>
        /// <typeparam name="T">枚举类型</typeparam>
        public static void BindEnumToCombo<T>(ComboBoxEdit ctr, string header = null, List<T> except = null) where T : struct
        {
            var enumType = typeof(T);
            if (except == null)
                except = new List<T>();
            var exceptValue = except.Select(old => { return old.CastTo<int>(); }).ToList();
            BindEnumToCombo(ctr, enumType, header, exceptValue);
        }

        /// <summary>
        /// 绑定枚举到下拉框
        /// </summary>
        /// <param name="ctr">控件</param>
        /// <param name="enumType">枚举类型</param>
        /// <param name="header">请选择等文字</param>
        /// <param name="exceptValue">不绑定项</param>
        public static void BindEnumToCombo(ComboBoxEdit ctr, Type enumType, string header, List<int> exceptValue)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException(enumType.FullName + "不是枚举类型");
            }
            if (!string.IsNullOrEmpty(header))
            {
                ctr.Properties.Items.Add(header);
            }
            var eds = EnumDescription.GetFieldInfos(enumType);
            if (eds != null && eds.Count > 0 && exceptValue != null && exceptValue.Count > 0)
            {
                foreach (var ed in eds)
                {
                    if (!exceptValue.Contains(ed.EnumValue))
                    {
                        ctr.Properties.Items.Add(ed);
                    }
                }
            }
            else if (eds != null && eds.Count > 0)
            {
                ctr.Properties.Items.AddRange(eds);
            }
            ctr.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            if (ctr.Properties.Items.Count > 0)
            {
                ctr.SelectedIndex = 0;
            }
        }


        /// <summary>
        /// 绑定枚举到下拉框
        /// </summary>
        /// <param name="ctr">控件</param>
        /// <param name="enumType">枚举类型</param>
        /// <param name="header">请选择等文字</param>
        /// <param name="exceptValue">不绑定项</param>
        public static void BindEnumToCombo(RepositoryItemComboBox ctr, Type enumType, string header, List<int> exceptValue)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException(enumType.FullName + "不是枚举类型");
            }

            BindEnumToComboItems(enumType, header, exceptValue, ctr.Items);

            ctr.TextEditStyle = TextEditStyles.DisableTextEditor;
        }

        private static void BindEnumToComboItems(Type enumType, string header, List<int> exceptValue, ComboBoxItemCollection x)
        {
            if (!string.IsNullOrEmpty(header))
            {
                x.Add(header);
            }
            var eds = EnumDescription.GetFieldInfos(enumType);
            if (eds != null && eds.Count > 0 && exceptValue != null && exceptValue.Count > 0)
            {
                foreach (var ed in eds)
                {
                    if (!exceptValue.Contains(ed.EnumValue))
                    {
                        x.Add(ed);
                    }
                }
            }
            else if (eds != null && eds.Count > 0)
            {
                x.AddRange(eds);
            }
        }

        /// <summary>
        /// 绑定对象列表到下拉框
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="ctr">下拉控件</param>
        /// <param name="datas">数据列表</param>
        /// <param name="displayFunc">取得显示文字方法</param>
        /// <param name="header">请选择等文字</param>
        public static void BindListToCombo<T>(ComboBoxEdit ctr, List<T> datas, Func<T, string> displayFunc, string header = null) where T : class
        {
            if (!string.IsNullOrEmpty(header))
            {
                ctr.Properties.Items.Add(header);
            }
            if (datas != null && datas.Count > 0)
            {
                var bindDatas = datas.Select(old => new IdTextData { Data = old, Text = displayFunc(old) }).ToList();
                ctr.Properties.Items.AddRange(bindDatas);
            }
            ctr.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            if (ctr.Properties.Items.Count > 0)
            {
                ctr.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定对象列表到下拉框
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="ctr">下拉控件</param>
        /// <param name="datas">数据列表</param>
        /// <param name="displayFunc">取得显示文字方法</param>
        /// <param name="idFunc">取得标识的方法</param>
        /// <param name="header">请选择等文字</param>
        public static void BindListToCombo<T>(RepositoryItemComboBox ctr, List<T> datas, Func<T, string> displayFunc, Func<T, string> idFunc, string header = null) where T : class
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

        /// <summary>
        /// 设置选中项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="ctr">控件</param>
        /// <param name="data">数据</param>
        public static void SetSelectEnum<T>(ComboBoxEdit ctr, T data) where T : struct
        {
            var t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ArgumentException(t.FullName + "必须是枚举类型");
            }
            SetSelectEnum(ctr, data.CastTo<int>());
        }

        /// <summary>
        /// 设置选中项
        /// </summary>
        /// <param name="ctr">控件</param>
        /// <param name="data">数据</param>
        public static void SetSelectEnum(ComboBoxEdit ctr, int data)
        {
            foreach (var item in ctr.Properties.Items)
            {
                var ed = item as EnumDescription;
                if (ed != null && ed.EnumValue == data.CastTo<int>())
                {
                    ctr.SelectedItem = ed;
                }
            }
        }

        /// <summary>
        /// 设置选中项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="ctr">控件</param>
        /// <param name="data">数据</param>
        public static void SetSelectData<T>(ComboBoxEdit ctr, T data) where T : class
        {
            if (ctr == null || data == null) { return; }
            foreach (var item in ctr.Properties.Items)
            {
                var itd = item as IdTextData;
                if (itd != null && ((typeof(T) == typeof(string) && data.ToString() == itd.Data.ToString()) 
                    || ReferenceEquals(itd.Data, data)))
                {
                    ctr.SelectedItem = itd;
                    break;
                }
            }
        }

        /// <summary>
        /// 设置选中项
        /// </summary>
        /// <param name="ctr">控件</param>
        /// <param name="key">数据键</param>
        public static void SetSelectKey(ComboBoxEdit ctr, string key)
        {
            if (ctr == null || string.IsNullOrWhiteSpace(key)) { return; }
            foreach (var item in ctr.Properties.Items)
            {
                var itd = item as IdTextData;
                if (itd?.GetData<IKey>()?.Key == key)
                {
                    ctr.SelectedItem = itd;
                    break;
                }
            }
        }

        /// <summary>
        /// 获取选中项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="ctr">控件</param>
        /// <returns>选中数据</returns>
        public static T GetSelectEnum<T>(ComboBoxEdit ctr) where T : struct
        {
            var t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ArgumentException(t.FullName + "必须是枚举类型");
            }
            var ed = ctr.SelectedItem as EnumDescription;
            if (ed != null)
            {
                return ed.EnumValue.CastTo<T>();
            }
            return default(T);
        }
        /// <summary>
        /// 获取选中项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="ctr">控件</param>
        /// <returns>选中数据</returns>
        public static T GetSelectData<T>(ComboBoxEdit ctr) where T : class
        {
            var data = ctr.SelectedItem as IdTextData;
            return data?.GetData<T>();
        }
    }
}