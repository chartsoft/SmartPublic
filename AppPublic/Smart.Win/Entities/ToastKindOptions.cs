using System.Drawing;
using Smart.Win.Enums;
using Smart.Win.Properties;

namespace Smart.Win.Entities
{
    /// <summary>
    /// 提醒信息类型选项
    /// </summary>
    public class ToastKindOptions
    {
        /// <summary>
        /// 提醒信息类型选项
        /// </summary>
        public ToastKindOptions()
        {
            FontColor = Color.Black;
            BackColor = Color.LightGray;
        }
        /// <summary>
        /// 提醒信息类型选项
        /// </summary>
        /// <param name="kind"></param>
        public ToastKindOptions(ToastKinds kind)
        {
            switch (kind)
            {
                case ToastKinds.Success:
                    {
                        FontColor = Color.SeaGreen;
                        BackColor = Color.PaleGreen;
                        IconImage = Resources.LightGreen;
                        break;
                    }
                case ToastKinds.Error:
                    {
                        FontColor = Color.DarkRed;
                        BackColor = Color.LightCoral;
                        IconImage = Resources.LightRed;
                        break;
                    }
                case ToastKinds.Info:
                    {
                        FontColor = Color.DodgerBlue;
                        BackColor = Color.Aqua;
                        IconImage = Resources.LightBlue;
                        break;
                    }
                case ToastKinds.Warning:
                    {
                        FontColor = Color.Coral;
                        BackColor = Color.Gold;
                        IconImage = Resources.LightYellow;
                        break;
                    }
                default:
                    {
                        FontColor = Color.Black;
                        BackColor = Color.LightGray;
                        break;
                    }
            }
        }
        /// <summary>
        /// 文字颜色
        /// </summary>
        public Color FontColor { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackColor { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public Bitmap IconImage { get; set; }

        ///// <summary>
        ///// 释放非托管资源
        ///// </summary>
        //public override void ReleaseUnManagedResource()
        //{
        //    IconImage?.Dispose();
        //}
    }
}
