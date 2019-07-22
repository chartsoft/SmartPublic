using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Smart.Net45.Attribute;
using Smart.Net45.Extends;
using Smart.Win.Entitys;

namespace Smart.Win.Helpers
{
    /// <summary>
    /// 下拉控件辅助类
    /// </summary>
    public class RadioGroupHelper
    {

        /// <summary>
        /// 绑定枚举到单选组
        /// </summary>
        /// <param name="ctr">控件</param>
        /// <param name="except">不绑定项</param>
        /// <typeparam name="T">枚举类型</typeparam>
        public static void BindEnumToRadioGroup<T>(RadioGroup ctr, List<T> except = null)
        {
            var t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ArgumentException(t.FullName + "不是枚举类型");
            }
            var eds = EnumDescription.GetFieldInfos(typeof(T));
            if (eds != null && eds.Count > 0)
            {
                if (except == null) { except = new List<T>(); }
                var exceptValue = except.Select(old => old.CastTo<int>()).ToList();
                foreach (var ed in eds)
                {
                    if (!exceptValue.Contains(ed.EnumValue))
                    {
                        var item = new RadioGroupItem(ed.EnumValue, ed.EnumDisplayText);
                        ctr.Properties.Items.Add(item);
                    }
                }
            }
            if (ctr.Properties.Items.Count > 0)
            {
                ctr.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 绑定枚举到单选组
        /// </summary>
        /// <param name="ctr">控件</param>
        /// <param name="except">不绑定项</param>
        /// <param name="enumType">枚举类型</param>
        public static void BindEnumToRadioGroup(RadioGroup ctr, Type enumType, List<int> except = null)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException(enumType.FullName + "不是枚举类型");
            }
            var eds = EnumDescription.GetFieldInfos(enumType);
            if (eds != null && eds.Count > 0)
            {
                if (except == null) { except = new List<int>(); }
                var exceptValue = except.Select(old => old.CastTo<int>()).ToList();
                foreach (var ed in eds)
                {
                    if (!exceptValue.Contains(ed.EnumValue))
                    {
                        var item = new RadioGroupItem(ed.EnumValue, ed.EnumDisplayText) {Tag = ed};
                        ctr.Properties.Items.Add(item);
                    }
                }
            }
            if (ctr.Properties.Items.Count > 0)
            {
                ctr.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 绑定枚举到单选组
        /// </summary>
        /// <param name="ctr">控件</param>
        /// <param name="list">绑定的枚举列表</param>
        /// <typeparam name="T">枚举类型</typeparam>
        public static void BindEnumList<T>(RadioGroup ctr, List<T> list) where T : struct
        {
            if (list.IsNullOrEmpty())
                return;

            var t = typeof(T);
            if (!t.IsEnum)
                throw new ArgumentException(t.FullName + "不是枚举类型");

            var eds = EnumDescription.GetFieldInfos(typeof(T));
            var enumValueList = list.Select(x => x.CastTo<int>()).ToList();

            foreach (var ed in eds.Where(x => enumValueList.Contains(x.EnumValue)))
                ctr.Properties.Items.Add(new RadioGroupItem(ed.EnumValue, ed.EnumDisplayText));
        }
        /// <summary>
        /// 绑定枚举到单选组
        /// </summary>
        /// <param name="ctr">控件</param>
        /// <param name="list">绑定的枚举列表</param>
        /// <param name="enumType">枚举类型</param>
        public static void BindEnumList(RadioGroup ctr, Type enumType, List<int> list)
        {
            if (list.IsNullOrEmpty())
                return;

            if (!enumType.IsEnum)
                throw new ArgumentException(enumType.FullName + "不是枚举类型");

            var eds = EnumDescription.GetFieldInfos(enumType);
            var enumValueList = list.Select(x => x.CastTo<int>()).ToList();

            foreach (var ed in eds.Where(x => enumValueList.Contains(x.EnumValue)))
                ctr.Properties.Items.Add(new RadioGroupItem(ed.EnumValue, ed.EnumDisplayText));
        }
        /// <summary>
        /// 绑定对象列表到下拉框
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="ctr">下拉控件</param>
        /// <param name="datas">数据列表</param>
        /// <param name="displayFunc">取得显示文字方法</param>
        /// <param name="valueFunc">取得值方法</param>
        public static void BindListToRadioGroup<T>(RadioGroup ctr, List<T> datas, Func<T, string> displayFunc, Func<T, object> valueFunc)
        {
            if (datas != null && datas.Count > 0)
            {
                var bindDatas = datas.Select(old => new IdTextValueData<T> { Data = old, Text = displayFunc(old), Value = valueFunc(old) }).ToList();
                foreach (var data in bindDatas)
                {
                    var item = new RadioGroupItem(data.Value, data.Text);
                    ctr.Properties.Items.Add(item);
                }
                ctr.Tag = bindDatas;
            }
            if (ctr.Properties.Items.Count > 0)
            {
                ctr.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 设置选中项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="ctr">控件</param>
        /// <param name="data">数据</param>
        public static void SetSelectEnum<T>(RadioGroup ctr, T data)
        {
            var t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ArgumentException(t.FullName + "必须是枚举类型");
            }

            ctr.EditValue = data.CastTo<int>();
        }

        /// <summary>
        /// 设置选中项
        /// </summary>
        /// <param name="ctr">控件</param>
        /// <param name="val">数据</param>
        public static void SetSelectValue<T>(RadioGroup ctr, T val)
        {
            var dataSource = ctr.Tag as List<IdTextValueData<T>>;
            if (dataSource == null)
                return;

            var selectedData = dataSource.FirstOrDefault(x => x.Data.Equals(val));
            ctr.SelectedIndex = selectedData == null ? -1 : dataSource.IndexOf(selectedData);
        }

        /// <summary>
        /// 获取选中项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="ctr">控件</param>
        /// <returns>选中数据</returns>
        public static T GetSelectEnum<T>(RadioGroup ctr)
        {
            var t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ArgumentException(t.FullName + "必须是枚举类型");
            }

            return ctr.EditValue.CastTo<T>();
        }

        /// <summary>
        /// 获取选中项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="ctr">控件</param>
        /// <returns>选中数据</returns>
        public static T GetSelectData<T>(RadioGroup ctr)
        {
            if (ctr.EditValue != null)
            {
                var dataSource = ctr.Tag as List<IdTextValueData<T>>;
                IdTextValueData<T> selectedData = null;
                if (dataSource != null && ctr.SelectedIndex >= 0 && ctr.SelectedIndex < dataSource.Count)
                    selectedData = dataSource[ctr.SelectedIndex];

                if (selectedData != null)
                    return selectedData.Data;
            }
            return default(T);
        }
    }
}