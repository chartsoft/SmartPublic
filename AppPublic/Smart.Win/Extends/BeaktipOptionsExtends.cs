using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using Smart.Win.Entities;
using SmartSolution.Utilities.Win;

namespace Smart.Win.Extends
{
    /// <summary>
    /// FlyoutPanel控件扩展
    /// </summary>
    public static class BeaktipOptionsExtends
    {
        /// <summary>
        /// 移除Beaktip绑定
        /// </summary>
        /// <param name="hoverCtr">Hover控件</param>
        public static void UnbindBeak(this Control hoverCtr)
        {
            var tagOptions = hoverCtr.GetTag<BeaktipOptions>(WinUtilityConsts.BeakTooltipBeakPanelOptionsTagKey);
            if (tagOptions == null) return;
            hoverCtr.MouseEnter -= HoverControlMouseEnter;
            hoverCtr.RemoveTag(WinUtilityConsts.BeakTooltipBeakPanelOptionsTagKey);
        }

        /// <summary>
        /// 绑定Beaktip
        /// </summary>·
        /// <param name="options">
        /// <para>Beak显示选项：</para>
        /// <para>BeakLocation:显示位置</para>
        /// <para>AnimationType:动画类型</para>
        /// <para>BackColor:背景色</para>
        /// <para>BorderColor:边框色</para>
        /// <para>CloseOnOuterClick:外部点击关闭</para>
        /// </param>
        /// <param name="hoverCtr">Hover控件</param>
        /// <param name="contentCtr">要显示的控件</param>
        public static void BindBeak(this BeaktipOptions options, Control hoverCtr, Control contentCtr)
        {
            if (options == null || hoverCtr == null || contentCtr == null) return;
            var tagOptions = hoverCtr.GetTag<BeaktipOptions>(WinUtilityConsts.BeakTooltipBeakPanelOptionsTagKey);
            if (tagOptions != null) return;
            //fPanel为空
            hoverCtr.MouseEnter += HoverControlMouseEnter;
            var fPanel = options.TipPanel;
            fPanel.OptionsBeakPanel.BeakLocation = options.BeakLocation;
            fPanel.Parent = hoverCtr.Parent;
            fPanel.Controls.Add(contentCtr);
            fPanel.AutoSize = true;
            hoverCtr.SetTag(WinUtilityConsts.BeakTooltipBeakPanelOptionsTagKey, options);
        }

        /// <summary>
        /// 鼠标移入
        /// </summary>
        private static void HoverControlMouseEnter(object s, System.EventArgs e)
        {
            var hCtr = s as Control;
            var hOptions = hCtr.GetTag<BeaktipOptions>(WinUtilityConsts.BeakTooltipBeakPanelOptionsTagKey);
            var hPanel = hOptions.TipPanel;
            hPanel.OptionsBeakPanel.BeakLocation = hOptions.BeakLocation;
            if (hPanel.FlyoutPanelState.IsActive) return;
            hPanel.ShowBeakForm(GetBeakPoint(hCtr, hOptions));
        }

        /// <summary>
        /// 取得Beak显示点
        /// </summary>
        /// <param name="ctr">要显示的控件</param>
        /// <param name="options">显示选项</param>
        /// <returns><see cref="Point"/>Beak显示点</returns>
        private static Point GetBeakPoint(Control ctr, BeaktipOptions options)
        {
            var pt = new Point(0, ctr.Height / 2);
            var beakLocation = options.BeakLocation;
            if (beakLocation == BeakPanelBeakLocation.Right)
            {
                return ctr.PointToScreen(pt);
            }
            if (beakLocation == BeakPanelBeakLocation.Left)
            {
                pt.X += ctr.Width;
                return ctr.PointToScreen(pt);
            }
            pt = new Point(ctr.Width / 2, 0);
            if (beakLocation == BeakPanelBeakLocation.Top)
            {
                pt.Y += ctr.Height;
            }
            return ctr.PointToScreen(pt);
        }

    }
}
