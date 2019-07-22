using System.Drawing;
using DevExpress.XtraEditors;

namespace Smart.Win.Helpers
{
    /// <summary>
    /// Button样式辅助类
    /// </summary>
    public class ButtonStyleHelper
    {


        /// <summary>
        /// 设置Button样式
        /// </summary>
        public static void StyleButton(SimpleButton btn, Image img, int? width = null, string text = null, bool isShowText = true, int? height = null)
        {
            if (width <= 0) { width = null; }

            btn.Image = img;

            if (isShowText)
            {
                btn.Font = new Font("宋体", 9);
                btn.Text = string.IsNullOrEmpty(text) ? btn.Text : text;
                btn.Width = width ?? (btn.Text.Length * 12 + 32);
            }
            else
            {
                btn.Text = "";
                btn.Width = 24;
                btn.ImageLocation = ImageLocation.MiddleCenter;
            }
            if (btn is DropDownButton)
            {
                btn.Width += 16;
            }

            btn.Height = height ?? 24;
        }
    
    }
}
