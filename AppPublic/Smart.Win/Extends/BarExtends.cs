using System.Windows.Forms;
using DevExpress.Utils.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace Smart.Win.Extends
{
    /// <summary>
    /// Bar扩展类
    /// </summary>
    public static class BarExtends
    {

        /// <summary>
        /// 停靠到GroupControl
        /// </summary>
        public static void DockToGroup(this Bar bar, GroupControl group, DockStyle dock = DockStyle.Top)
        {
            DockToContainer(bar, group, dock);
        }

        /// <summary>
        /// 停靠到PanelControl
        /// </summary>
        /// <param name="bar">Toolbar控件</param>
        /// <param name="panel">Panel控件</param>
        /// <param name="dock">Dock位置</param>
        public static void DockToPanel(this Bar bar, PanelControl panel, DockStyle dock = DockStyle.Top)
        {
            DockToContainer(bar, panel, dock);
        }
        /// <summary>
        /// 设置Bar样式
        /// </summary>
        /// <param name="bar"><see cref="Bar"/>控件</param>
        public static void SetStandaloneStyle(this Bar bar)
        {
            if (bar == null)  return;
            bar.CanDockStyle = BarCanDockStyle.Standalone;
            bar.DockStyle = BarDockStyle.Standalone;
            bar.OptionsBar.AllowQuickCustomization = false;
            bar.OptionsBar.DrawDragBorder = false;
            bar.OptionsBar.MultiLine = true;
            bar.OptionsBar.UseWholeRow = true;
            if (bar.Manager != null) bar.Manager.AllowShowToolbarsPopup = false;
        }

        /// <summary>
        /// 停靠到容器
        /// </summary>
        private static void DockToContainer(this Bar bar, PanelBase panel, DockStyle dock)
        {
            //Bar样式设置
            bar.OptionsBar.AllowQuickCustomization = false;
            bar.OptionsBar.DrawDragBorder = false;
            bar.OptionsBar.MultiLine = true;
            bar.OptionsBar.UseWholeRow = true;
            //创建DockControl
            bar.Manager.BeginUpdate();
            var barDock = new StandaloneBarDockControl { Dock = dock };
            bar.StandaloneBarDockControl = barDock;
            bar.Manager.DockControls.Add(barDock);
            panel.Controls.Add(barDock);
            bar.Manager.EndUpdate();
        }

    }
}
