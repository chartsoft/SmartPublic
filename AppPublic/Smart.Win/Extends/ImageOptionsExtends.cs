using System.Drawing;
using DevExpress.Utils;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;

namespace Smart.Win.Extends
{

    /// <summary>
    /// 图标选项扩展
    /// </summary>
    public static class ImageOptionsExtends
    {

        /// <summary>
        /// 设置SVG图标
        /// </summary>
        public static ImageOptions SetSvgIcon(this ImageOptions options, SvgImage svg, int width = 16, int height = 16)
        {
            options.SvgImageSize = new Size(width, height);
            options.SvgImage = svg;
            return options;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="align"></param>
        /// <param name="indent"></param>
        public static SimpleButtonImageOptions SetSvgIconPosition(this SimpleButtonImageOptions options, ImageAlignToText align, int indent)
        {
            options.ImageToTextAlignment = ImageAlignToText.LeftCenter;
            options.ImageToTextIndent = 10;
            return options;
        }

    }
}
