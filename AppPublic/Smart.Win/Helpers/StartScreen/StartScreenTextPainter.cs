using System.Drawing;
using DevExpress.Utils.Drawing;
using DevExpress.XtraSplashScreen;

namespace Smart.Win.Helpers.StartScreen
{
    /// <summary>
    /// 加载中文字 图片绘制器
    /// </summary>
    public class StartScreenTextPainter : ICustomImagePainter
    {
        /// <summary>
        /// 绘制计数器
        /// </summary>
        internal static int Counter = 0;
        /// <summary>
        /// 默认字体
        /// </summary>
        private static readonly Font DefaultFont = new Font("Segoe UI", 6.75f, FontStyle.Bold);
        /// <summary>
        /// 字体刷
        /// </summary>
        private static readonly SolidBrush FontBrush = new SolidBrush(SkinAppearance.LabelAndCaptionInLayoutColor);

        /// <summary>
        /// 绘制加载文字
        /// </summary>
        void ICustomImagePainter.Draw(GraphicsCache cache, Rectangle bounds)
        {
            var text = "加载中";
            for (var i = 0; i < Counter % 4; i++)
            {
                text += '.';
            }
            cache.Graphics.DrawString(text, DefaultFont, FontBrush, 62f, 45f);
        }

    }
}