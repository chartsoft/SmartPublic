using System;
using System.Windows.Forms;
using Smart.Win.Entities;
using Smart.Win.Enums;
using Smart.Win.Helpers;

namespace Smart.Win
{
    /// <summary>
    /// WinForm通用辅助类
    /// </summary>
    public static partial class UtilityHelper
    {

        /// <summary>
        /// 提示
        /// </summary>
        public static void ShowToast(ToastKinds kind, ToastOptions options, string msg, Form frm)
        {
            var frm2 = frm ?? (Application.OpenForms.Count > 0 ? Application.OpenForms[0] : null);
            if (frm2 == null) throw new ArgumentException($"ShowToast参数{nameof(frm)}为空");
            ToastMessageHelper.ShowToastMessage(options, frm2, msg);
        }

        /// <summary>
        /// 显示提示（内容为用户控件）
        /// </summary>
        /// <param name="options">选项</param>
        /// <param name="ctr">控件</param>
        /// <param name="frm">窗体</param>
        /// <returns></returns>
        public static void ShowToast(ToastOptions options, Control ctr, Form frm = null)
        {
            var frm2 = frm ?? ctr.FindForm();
            if (frm2 == null) throw new ArgumentException($"ShowToast参数{nameof(frm)}为空");
            if (ctr == null) throw new ArgumentException($"ShowToast参数{nameof(ctr)}为空");
            ToastMessageHelper.ShowControlToast(options, frm2, ctr);
        }

    }

}