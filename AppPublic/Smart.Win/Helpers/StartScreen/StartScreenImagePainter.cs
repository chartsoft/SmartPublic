using System.Drawing;
using DevExpress.Utils.Drawing;
using DevExpress.XtraSplashScreen;

namespace Smart.Win.Helpers.StartScreen
{
    /// <summary>
    /// 自定义启动图绘制器
    /// </summary>
    public class StartScreenImagePainter : ICustomImagePainter
    {
        /// <summary>
        /// 默认字体
        /// </summary>
        private static readonly Font DefaultFont = new Font("Segoe UI", 8.25f);
        /// <summary>
        /// 字体笔刷
        /// </summary>
        private static readonly SolidBrush FontBrush = new SolidBrush(Color.FromArgb(242, 242, 242));
        /// <summary>
        /// 绘制信息
        /// </summary>
        public void Draw(GraphicsCache cache, Rectangle bounds)
        {
            cache.Graphics.DrawString("®版本", DefaultFont, FontBrush, 97f, 400f);
            cache.Graphics.DrawString("Copyright ® 2000-2017 WitsWay Inc.", DefaultFont, FontBrush, 97f, 430f);
        }

    }
}
