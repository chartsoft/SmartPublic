using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using SmartSolution.Utilities.Win.Controls;

namespace Smart.Win.Controls
{
    /// <summary>
    /// 加载窗体
    /// </summary>
    public partial class SplashForm2 : DemoSplashScreen
    {
        private Action work;
        private bool notify;
        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="work">后台任务</param>
        /// <param name="acceptNotify">是否接受通知</param>
        public SplashForm2(Action work, bool acceptNotify)
        {
            InitializeComponent();
            labelControl1.Text = string.Empty;
            //this.pictureEdit1.Image = null;
            //this.pictureEdit1.Visible = false;
            this.Icon = null;
            this.work = work;
            notify = acceptNotify;
        }
        /// <summary>
        /// 初始化窗体
        /// </summary>
        /// <param name="icon">窗体图标</param>
        /// <param name="productImg">产品图片</param>
        /// <param name="corpImg">企业图片</param>
        /// <param name="copyright">版权</param>
        /// <param name="loadText">加载文字</param>
        public void InitForm(Icon icon, Bitmap productImg = null, Bitmap corpImg = null, string copyright = null, string loadText = "启动中，请稍后…")
        {
            if (icon != null)
            {
                this.Icon = icon;
            }
            if (productImg != null)
            {
                this.pictureEdit2.Image = productImg;
            }
            if (corpImg != null)
            {
                this.pictureEdit1.Image = corpImg;
            }
            labelControl1.Text = string.IsNullOrWhiteSpace(copyright) ?
                $"Copyright © { DateTime.Now.Year}"
                :
                copyright;
            labelControl2.Text = loadText;
        }

        private Stopwatch watch = new Stopwatch();

        private void SplashForm_Load(object sender, EventArgs e)
        {

            if (notify)
            {
                SplashNotice.SplashNoticeMsg += msg =>
                {
                    MethodInvoker invoker = delegate {
                        labelControl2.Text = msg;
                    };
                    this.Invoke(invoker);
                };
            }
            if (work != null)
            {
                watch.Reset();
                watch.Start();
                var worker = new BackgroundWorker();
                worker.RunWorkerCompleted += (s1, e1) =>
                {
                    MethodInvoker invoker = delegate {
                        SplashNotice.OnFinishWork();
                    };
                    this.Invoke(invoker);
                };
                worker.DoWork += worker_DoWork;
                worker.RunWorkerAsync();
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (work != null)
            {
                work();
            }
            watch.Stop();
            if (watch.ElapsedMilliseconds < 1000)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
