using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace Smart.Win.Builders
{

    /// <summary>
    /// 创建配置管理的窗体
    /// </summary>
    public class ConfigFormBuilder
    {
        /// <summary>
        /// 窗体用户控件
        /// </summary>
        private XtraUserControl _abmConfigUc;
        /// <summary>
        /// _viewControl
        /// </summary>
        private BackstageViewControl _viewControl;
        /// <summary>
        /// 左边菜单标题
        /// </summary>
        private string _tabCaption;
        /// <summary>
        /// 左边菜单是否选中
        /// </summary>
        private bool _isSelected;
        /// <summary>
        /// 左边菜单tabindex
        /// </summary>
        private int _tabIndex;
        /// <summary>
        /// 设置左边菜单的标题
        /// </summary>
        public ConfigFormBuilder SetTabItemCaption(string caption)
        {
            _tabCaption = caption;
            return this;
        }
        /// <summary>
        /// 设置左边菜单是否选中
        /// </summary>
        public ConfigFormBuilder SetSelected()
        {
            _isSelected = true;
            return this;
        }
        /// <summary>
        /// 设置左边菜单的tabindex
        /// </summary>
        public ConfigFormBuilder SetTabIndex(int index)
        {
            _tabIndex = index;
            return this;
        }
        /// <summary>
        /// 添加配置窗体
        /// </summary>
        public ConfigFormBuilder AddConfigForm(BackstageViewControl viewControl, XtraUserControl abmConfigUC)
        {
            _abmConfigUc = abmConfigUC;
            _abmConfigUc.Dock = DockStyle.Fill;
            _viewControl = viewControl;
            return this;
        }
        /// <summary>
        /// 生成窗体
        /// </summary>
        public void Builder()
        {
            var backstageViewClientControl3 = new BackstageViewClientControl();
            var backstageViewTabItem3 = new BackstageViewTabItem();

            backstageViewClientControl3.Location = new System.Drawing.Point(192, 0);
            backstageViewClientControl3.Name = "backstageViewClientControl" + backstageViewClientControl3.GetHashCode();
            backstageViewClientControl3.Size = new System.Drawing.Size(1000, 633);
            backstageViewClientControl3.TabIndex = _tabIndex;

            backstageViewTabItem3.Caption = _tabCaption;
            backstageViewTabItem3.ContentControl = backstageViewClientControl3;
            backstageViewTabItem3.Name = "backstageViewTabItem" + backstageViewTabItem3.GetHashCode();
            backstageViewTabItem3.Selected = _isSelected;

            backstageViewClientControl3.Controls.Add(_abmConfigUc);

            _viewControl.Controls.Add(backstageViewClientControl3);
            _viewControl.Items.Add(backstageViewTabItem3);
            if (_isSelected) _viewControl.SelectedTabIndex = _tabIndex;

        }
    }
}
