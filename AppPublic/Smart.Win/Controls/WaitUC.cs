using System;
using DevExpress.XtraEditors;
using Smart.Win.Helpers;
using SmartSolution.Utilities.Win;

namespace Smart.Win.Controls
{
    public partial class WaitUC : XtraUserControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public WaitUC(string msg="请稍后…")
        {
            InitializeComponent();
            ControlStyleHelper.SetLoadingCircle(_loadingCircle);
            _lblMsg.Text = msg;
        }

        private void WaitUC_SizeChanged(object sender, EventArgs e)
        {
            UtilityHelper.CenterControl(_panelContent,this);
        }

        private void WaitUC_Load(object sender, EventArgs e)
        {

        }
    }
}
