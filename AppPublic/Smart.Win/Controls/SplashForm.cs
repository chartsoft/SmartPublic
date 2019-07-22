using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SmartSolution.Utilities.Win.Controls;

namespace Smart.Win.Controls
{
    /// <summary>
    /// 加载窗体
    /// </summary>
    public partial class SplashForm : XtraForm
    {
        private readonly Action _work;
        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="work"></param>
        /// <param name="icon">窗体图标</param>
        public SplashForm(Action work,Icon icon=null)
        {
            InitializeComponent();
            _work = work;
            Icon = icon;
            InitControl();
        }

        private void InitControl()
        {
            //ControlStyleHelper.SetLoadingCircle(_loadingCircle);
            //_loadingCircle.Parent = _picBG;
            _lblMsg.Parent = _picBG;
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            SplashNotice.SplashNoticeMsg += msg => {
                MethodInvoker invoker = delegate {
                    _lblMsg.Text = msg;
                };
                Invoke(invoker);
            };
            var worker = new BackgroundWorker();
            worker.RunWorkerCompleted += (s1, e1) => {
                MethodInvoker invoker = SplashNotice.OnFinishWork;
                Invoke(invoker);
            };
            worker.DoWork += worker_DoWork;
            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _work?.Invoke();
        }
    }
}
