using System.Drawing;
using DevExpress.XtraBars;

namespace Smart.Win.Helpers
{
    /// <summary>
    /// Bar辅助类
    /// </summary>
    public class BarHelper
    {
        /// <summary>
        /// 设置Toolbar的样式
        /// </summary>
        /// <param name="toolBar"></param>
        public static void SetToolBarStyle(Bar toolBar)
        {
            if (toolBar == null) return;
            toolBar.OptionsBar.AllowQuickCustomization = false;
            toolBar.OptionsBar.UseWholeRow = true;
            toolBar.OptionsBar.DrawDragBorder = false;
            if (toolBar.Manager != null) toolBar.Manager.AllowShowToolbarsPopup = false;
        }

        /// <summary>
        /// 设置用于填充空白的静态文本条的长度
        /// </summary>
        /// <param name="barStaticItem">静态文本条</param>
        /// <param name="length">长度</param>
        public static void SetBarStaticButtonLength(BarStaticItem barStaticItem, int length)
        {
            if (barStaticItem == null) return;
            if (length < 0) length = 0;
            barStaticItem.Appearance.Font = new Font("Tahoma", 1F, FontStyle.Regular, GraphicsUnit.Pixel);
            barStaticItem.Appearance.Options.UseFont = true;
            barStaticItem.Caption = new string(' ', length);
        }
    }
}
