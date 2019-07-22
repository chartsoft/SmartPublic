using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Smart.Win.Entitys;

namespace Smart.Win.Helpers
{
    /// <summary>
    /// RepositoryComboBox辅助类
    /// </summary>
    public class RepositoryImageComboBoxHelper
    {
        /// <summary>
        /// 绑定对象列表到下拉框
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="ctr">下拉控件</param>
        /// <param name="datas">数据列表</param>
        /// <param name="displayFunc">取得显示文字方法</param>
        /// <param name="header">请选择等文字</param>
        /// <param name="idFunc"></param>
        public static void BindListToRepositoryCombo<T>(RepositoryItemImageComboBox ctr, List<T> datas, Func<T, string> displayFunc, Func<T, string> idFunc, string header = null) where T : class, new()
        {
            if (!string.IsNullOrEmpty(header))
            {
                ctr.Items.Add(header);
            }
            if (datas != null && datas.Count > 0)
            {
                var bindDatas = datas.Select(old => new IdTextData { Data = old, Text = displayFunc(old), Id = idFunc(old) }).ToList();
                ctr.Items.AddRange(bindDatas.Select(data => new ImageComboBoxItem(data.Text, data.Id)).ToArray());
            }
            ctr.TextEditStyle = TextEditStyles.DisableTextEditor;
        }
    }
}
