using System.Drawing;
using System.Security;
using DevExpress.Utils.Win;
using Smart.Win.Enums;

namespace Smart.Win.Entities
{
    /// <summary>
    /// 提醒信息选项
    /// </summary>
    public class ToastOptions
    {
        /// <summary>
        /// 获取<see cref="ToastOptions"/>默认新实例
        /// </summary>
        /// <returns><see cref="ToastOptions"/>默认新实例</returns>
        [SecuritySafeCritical]
        public static ToastOptions NewInstance() => new ToastOptions();

        /// <summary>
        /// 获取<see cref="ToastOptions"/>默认新实例
        /// </summary>
        /// <param name="kind">消息类型</param>
        /// <returns><see cref="ToastOptions"/>默认新实例</returns>
        [SecuritySafeCritical]
        public static ToastOptions NewInstance(ToastKinds kind) => new ToastOptions(kind);

        /// <summary>
        /// 提醒信息选项
        /// </summary>
        public ToastOptions()
        {
            Anchor = PopupToolWindowAnchor.Top;
            AnimationKind = PopupToolWindowAnimation.Slide;
            CloseOnOuterClick = true;
            PositionX = PositionY = 0;
        }
        /// <summary>
        /// 提醒信息选项
        /// </summary>
        /// <param name="kind">消息类型</param>
        public ToastOptions(ToastKinds kind) : this()
        {
            KindOptions = new ToastKindOptions(kind);
        }

        /// <summary>
        /// 提醒消息类型选项
        /// </summary>
        public ToastKindOptions KindOptions { get; set; }

        /// <summary>
        /// 消息显示位置
        /// </summary>
        public PopupToolWindowAnchor Anchor { get; set; }
        /// <summary>
        /// 动画类型
        /// </summary>
        public PopupToolWindowAnimation AnimationKind { get; set; }
        /// <summary>
        /// 外部点击关闭
        /// </summary>
        public bool CloseOnOuterClick { get; set; }
        /// <summary>
        /// X坐标位置（左上角）
        /// </summary>
        public int PositionX { get; set; }
        /// <summary>
        ///  Y坐标位置（左上角）
        /// </summary>
        public int PositionY { get; set; }
        /// <summary>
        /// Anchor为Manual时是使用的自定义位置
        /// </summary>
        public Point CustomPosition => new Point(PositionX, PositionY);
    }
}
