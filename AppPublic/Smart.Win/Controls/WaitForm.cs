using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SmartSolution.Utilities.Win;

namespace Smart.Win.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class WaitForm : XtraForm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public WaitForm(string msg = "请稍后…")
        {
            InitializeComponent();
            _groupLoad.Text = msg;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void WaitForm_Load(object sender, EventArgs e)
        {
            UtilityHelper.CenterControl(_groupLoad, this);
            this.StartPosition = (this.Parent == null) ?
                FormStartPosition.CenterScreen : FormStartPosition.CenterParent;
        }
    }
}