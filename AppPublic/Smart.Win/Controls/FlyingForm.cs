using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Smart.Win.Controls
{
    /// <summary>
    /// 浮动消息层
    /// </summary>
    public sealed class FlyingForm : Form
    {
        /// <summary>
        /// 图标和文本之间的间距（像素）
        /// </summary>
        private const int IconTextSpacing = 3;

        /// <summary>
        /// 基准点。用于指导本窗体显示位置
        /// </summary>
        public Point BasePoint { get; set; }

        private string _tipText;
        /// <summary>
        /// 提示图标
        /// </summary>
        public Image TipIcon { get; set; }

        /// <summary>
        /// 提示文本
        /// </summary>
        public string TipText
        {
            get { return _tipText ?? string.Empty; }
            set { _tipText = value; }
        }

        /// <summary>
        /// 停留时长（毫秒）
        /// </summary>
        [DefaultValue(500)]
        public int Delay { get; set; }

        /// <summary>
        /// 是否允许浮动
        /// </summary>
        [DefaultValue(true)]
        public bool Floating { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Point SourcePoint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Point TargetPoint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Size SourceSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Size TargetSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Image Image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(200)]
        public int Speed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //显示后不激活，即不抢焦点
        protected override bool ShowWithoutActivation => true;
        /// <summary>
        /// 
        /// </summary>
        public FlyingForm()
        {
            //双缓冲。有必要
            SetStyle(ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            InitializeComponent();
            Delay = 500;
            Floating = true;
            TopMost = true;

            _timer.Tick += timer_Tick;
            Load += FlyingForm_Load;
            Shown += FlyingForm_Shown;
            FormClosing += FlyingForm_FormClosing;
        }

        private void FlyingForm_Load(object sender, EventArgs e)
        {
            Size = SourceSize;
            Location = SourcePoint;

            var deltaX = SourcePoint.X - TargetPoint.X;
            var deltaY = SourcePoint.Y - TargetPoint.Y;

            var totalDistence = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            Delay = (int)(totalDistence * 1000 / Speed);

            //上浮窗体动画。采用异步，以不阻塞透明渐变动画的进行
            ThreadPool.QueueUserWorkItem(obj =>
            {
                var startTime = DateTime.Now;
                var percentageFinished = 0d;

                while (this.IsHandleCreated && percentageFinished < 1)
                {
                    this.BeginInvoke(new Action<object>(arg =>
                    {
                        percentageFinished = Speed * (DateTime.Now - startTime).TotalMilliseconds / 1000 / totalDistence;

                        Size = SourceSize + ScaleSize(TargetSize - SourceSize, percentageFinished);

                        Location = new Point(SourcePoint.X + (int)(percentageFinished * (TargetPoint.X - SourcePoint.X)), (int)(SourcePoint.Y + percentageFinished * (TargetPoint.Y - SourcePoint.Y)));
                        Opacity = 1 - percentageFinished;
                    }), (object)null);


                    System.Threading.Thread.Sleep(30);
                }
            });
        }

        private Size ScaleSize(Size size, double ratio)
        {
            return new Size((int)(size.Width * ratio), (int)(size.Height * ratio));
        }

        private void FlyingForm_Shown(object sender, EventArgs e)
        {
            _timer.Interval = 100;
            //因为timer.Interval不能为0
            if (Delay > 0)
            {
                _timer.Interval = Delay;
                _timer.Start();
            }
            else
            {
                Close();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            Close();
        }

        private void FlyingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //透明渐隐动画
            while (Opacity > 0)
            {
                Opacity -= 0.1;
                Application.DoEvents();
                System.Threading.Thread.Sleep(20);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var clip = GetPaddedRectangle();//得到作图区域
            var g = e.Graphics;
            //g.DrawRectangle(Pens.Red, clip);//debug

            //画图标
            if (TipIcon != null)
            {
                g.DrawImageUnscaled(TipIcon, clip.Location);
            }
            //画文本
            if (TipText.Length != 0)
            {
                if (TipIcon != null)
                {
                    clip.X += TipIcon.Width + IconTextSpacing;
                }
                TextRenderer.DrawText(g, TipText, this.Font, clip, this.ForeColor, TextFormatFlags.VerticalCenter);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (Image != null)
                e.Graphics.DrawImage(Image, ClientRectangle);
            //画边框
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, SystemColors.ControlDark, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// 获取刨去Padding的内容区
        /// </summary>
        private Rectangle GetPaddedRectangle()
        {
            var r = this.ClientRectangle;
            r.X += this.Padding.Left;
            r.Y += this.Padding.Top;
            r.Width -= this.Padding.Horizontal;
            r.Height -= this.Padding.Vertical;
            return r;
        }

        #region 设计器内容
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer.Dispose();//这货必须显示释放
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            _timer = new Timer();
            this.SuspendLayout();

            this.AutoScaleMode = AutoScaleMode.None;
            //this.ClientSize = new System.Drawing.Size(100, 100);
            BackColor = Color.White;
            this.Font = new Font(SystemFonts.MessageBoxFont.FontFamily, 12);
            FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(20, 10, 20, 10);
            this.Name = "FlyingForm";
            ShowInTaskbar = false;

            this.ResumeLayout(false);
        }

        private Timer _timer;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePoint"></param>
        /// <param name="targetPoint"></param>
        /// <param name="sourceSize"></param>
        /// <param name="targetSize"></param>
        /// <param name="image"></param>
        /// <param name="speed"></param>
        public static void Fly(Point sourcePoint, Point targetPoint, Size sourceSize, Size targetSize, Image image, int speed = 200)
        {
            new FlyingForm
            {
                SourcePoint = sourcePoint,
                TargetPoint = targetPoint,
                SourceSize = sourceSize,
                TargetSize = targetSize,
                Image = image,
                Speed = 1000
            }.Show();
        }

    }
}