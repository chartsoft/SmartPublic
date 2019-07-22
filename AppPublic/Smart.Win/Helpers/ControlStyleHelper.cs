using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraNavBar;
using DevExpress.XtraTab;
using DevExpress.XtraTabbedMdi;
using Smart.Win.Controls;

namespace Smart.Win.Helpers
{
    /// <summary>
    /// 控件样式设置帮助类
    /// </summary>
    public static class ControlStyleHelper
    {

        /// <summary>
        /// 弹出页面样式
        /// </summary>
        public static void SetPopupStyle(XtraForm form)
        {
            SetPopupStyle(form, null, false);
        }

        /// <summary>
        /// 弹出页面样式
        /// </summary>
        /// <param name="form">form</param>
        /// <param name="resize">是否允许重新设置Size</param>
        /// <param name="icon"></param>
        public static void SetPopupStyle(XtraForm form, Icon icon, bool resize)
        {
            if (!resize)
            {
                form.MaximizeBox = false;
                form.SizeGripStyle = SizeGripStyle.Hide;
                form.FormBorderStyle = FormBorderStyle.FixedSingle;
            }
            form.MinimizeBox = false;
            form.MinimumSize = form.Size;
            form.WindowState = FormWindowState.Normal;
            form.StartPosition = FormStartPosition.CenterParent;
            if (icon != null)
            {
                form.Icon = icon;
            }
        }

        /// <summary>
        /// 设置日期选择控件样式
        /// </summary>
        /// <param name="ctr">日期选择控件</param>
        /// <param name="style"></param>
        public static void SetDateEditStyle(DateEdit ctr, TextEditStyles style)
        {
            ctr.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            ctr.Properties.DisplayFormat.FormatType = FormatType.Custom;
            ctr.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            ctr.Properties.EditFormat.FormatType = FormatType.Custom;
            ctr.Properties.Mask.EditMask = "yyyy-MM-dd";
            ctr.Properties.TextEditStyle = style;

        }
        /// <summary>
        /// 设置Spin控件样式
        /// </summary>
        /// <param name="ctr">Spin控件</param>
        /// <param name="align">对齐HorzAlignment</param>
        /// <param name="isFloat">是否Float</param>
        /// <param name="style">TextEditStyle</param>
        public static void SetSpinEditStyle(SpinEdit ctr, HorzAlignment align, bool isFloat, TextEditStyles style)
        {
            ctr.Properties.Appearance.TextOptions.HAlignment = align;
            ctr.Properties.IsFloatValue = isFloat;
            ctr.Properties.TextEditStyle = style;
        }

        /// <summary>
        /// 设置Label文字居中
        /// </summary>
        /// <param name="lbl"></param>
        public static void SetLabelTextCenter(LabelControl lbl)
        {
            lbl.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lbl.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            lbl.AutoSizeMode = LabelAutoSizeMode.None;
        }
        /// <summary>
        /// 设置控件NullText
        /// </summary>
        /// <param name="editor">控件</param>
        /// <param name="nullText">Null文字</param>
        /// <param name="initText">初始文字</param>
        public static void SetNullText(BaseEdit editor, string nullText, string initText)
        {
            editor.Properties.NullText = nullText;
            editor.Text = string.IsNullOrEmpty(initText) ? null : initText;
            editor.ForeColor = Color.FromArgb(128, 128, 128);
            editor.EditValueChanged += (s, e) =>
            {
                if (string.IsNullOrEmpty(editor.Text))
                {
                    editor.EditValue = null;
                    editor.ForeColor = Color.FromArgb(128, 128, 128);
                }
                else
                {
                    editor.ForeColor = Color.FromArgb(32, 31, 53);
                }
            };
        }

        #region [MainForm]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loading"></param>
        public static void SetLoadingCircle(LoadingCircle loading)
        {
            loading.Enabled = true;
            loading.Active = true;
            loading.InnerCircleRadius = 20;
            loading.OuterCircleRadius = 24;
            loading.NumberSpoke = 10;
            loading.BackColor = Color.Transparent;
            loading.RotationSpeed = 120;
            loading.SpokeThickness = 10;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nav"></param>
        public static void SetNavControl(NavBarControl nav)
        {
            nav.OptionsNavPane.ShowOverflowButton = false;
            nav.OptionsNavPane.ShowOverflowPanel = false;
            nav.PaintStyleKind = NavBarViewKind.NavigationPane;
            nav.SkinExplorerBarViewScrollStyle = SkinExplorerBarViewScrollStyle.ScrollBar;
            nav.StoreDefaultPaintStyleName = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bar"></param>
        public static void SetMainMenu(Bar bar)
        {
            bar.OptionsBar.MultiLine = false;
            bar.OptionsBar.UseWholeRow = true;
            bar.OptionsBar.AllowQuickCustomization = false;
            bar.OptionsBar.DrawDragBorder = false;
            bar.OptionsBar.DisableClose = false;
            bar.OptionsBar.DisableCustomization = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="midManager"></param>
        public static void SetMidManager(XtraTabbedMdiManager midManager)
        {
            midManager.ClosePageButtonShowMode = ClosePageButtonShowMode.InAllTabPageHeaders;
            midManager.FloatOnDoubleClick = DefaultBoolean.False;
            midManager.FloatOnDrag = DefaultBoolean.False;
        }


        #endregion

    }
}
