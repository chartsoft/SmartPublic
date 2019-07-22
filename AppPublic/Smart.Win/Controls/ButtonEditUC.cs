using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Smart.Win.Controls
{
    /// <summary>
    /// ButtonEdit控件
    /// </summary>
    public class ButtonEditUC : ButtonEdit
    {

        /// <summary>
        /// 设置响应删除事件
        /// </summary>
        private void SetCanDeleteValue()
        {
            if (CustomCanDeleteValue)
            {
                this.KeyDown += _buttonEdit_KeyDown;
            }
            else {
                this.KeyDown -= _buttonEdit_KeyDown;
            }
        }

        /// <summary>
        /// 设置是否可以选择样式
        /// </summary>
        private void SetisSelectable()
        {
            if (CustomIsSelectable)
            {
                for (var i = 0; i < Properties.Buttons.Count; i++)
                {
                    Properties.Buttons[i].Visible = true;
                }
                Properties.AppearanceReadOnly.BackColor = Color.White;
                Properties.AppearanceReadOnly.Options.UseBackColor = true;
            }
            else
            {
                for (var i = 0; i < Properties.Buttons.Count; i++)
                {
                    Properties.Buttons[i].Visible = false;
                }
                Properties.AppearanceReadOnly.Options.UseBackColor = false;
            }
        }

        /// <summary>
        /// 值得样式
        /// </summary>
        private void SetValueStyle()
        {
            if (CustomUseDeleteStyle)
            {
                Properties.Appearance.ForeColor = Color.Red;
                Properties.Appearance.Font = new Font("宋体", 9F, FontStyle.Strikeout, GraphicsUnit.Point, 134);
                Properties.Appearance.Options.UseFont = true;
                Properties.Appearance.Options.UseForeColor = true;
            }
            else
            {
                Properties.Appearance.ForeColor = Color.Black;
                Properties.Appearance.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
                Properties.Appearance.Options.UseFont = false;
                Properties.Appearance.Options.UseForeColor = false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _buttonEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.Tag = null;
                this.Text = string.Empty;
            }
        }

        private bool _CustomUseDeleteStyle;
        /// <summary>
        /// 选中的对象是否使用删除样式
        /// </summary>
        [DXCategory("Custom")]
        [Description("选中的对象是否使用删除样式。")]
        public bool CustomUseDeleteStyle
        {
            get { return _CustomUseDeleteStyle; }
            set {
                _CustomUseDeleteStyle = value;
                SetValueStyle(); 
            }
        }

        private bool _CustomCanDeleteValue;
        /// <summary>
        /// 按键Delete清楚选择值是否可用
        /// </summary>
        [DXCategory("Custom")]
        [Description("按键Delete清楚选择值是否可用。")]
        public bool CustomCanDeleteValue
        {
            get { return _CustomCanDeleteValue; }
            set
            {
                _CustomCanDeleteValue = value;
                SetCanDeleteValue(); 
            }
        }

        private bool _CustomIsSelectable;
        /// <summary>
        /// 选择按钮是否可用，双击文本框是否可用
        /// </summary>
        [DXCategory("Custom")]
        [Description("选择按钮是否可用，双击文本框是否可用。")]
        public bool CustomIsSelectable 
        {
            get { return _CustomIsSelectable; }
            set {
                _CustomIsSelectable = value;
                SetisSelectable(); 
            }
        }
    }
}
