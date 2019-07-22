using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SmartSolution.Utilities.Win.Controls;

namespace Smart.Win.Controls
{
    /// <summary>
    /// Ribbon≤Àµ•
    /// </summary>
    public class RibbonMenu : ContextMenuStrip
    {
        /// <summary>
        /// 
        /// </summary>
        public RibbonMenu()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
                          ControlStyles.UserPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.Opaque, false);
            this.BackColor = Color.Transparent;
            this.DropShadowEnabled = true;

            this.Renderer = new RibbonMenuRenderer();
        }

        private int _radius = 5;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var path = new GraphicsPath();
            var re = new Rectangle(0, 0, this.Width-1, this.Height-1);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            if (re.Size.Width > 5 & re.Size.Height > 5)
            {
              //  Rectangle rs = new Rectangle(1, 2, this.Width-1, this.Height-1);
              //  path = new GraphicsPath(); DrawArc(rs, path);
              //  FillShadow(rs, g);
                
                path = new GraphicsPath(); DrawArc(re, path);
                g.FillPath(new SolidBrush(Color.FromArgb(250, 250, 250)), path);
                
                var raffected = new Rectangle(1, 1, 24, this.Height - 3);
              // e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(233, 238, 238)), raffected);
              //  e.Graphics.DrawLine(new Pen(Color.FromArgb(197, 197, 197)), raffected.Right - 2, 1, raffected.Right - 2, raffected.Height + 1);
              //  e.Graphics.DrawLine(new Pen(Color.FromArgb(245, 245, 245)), raffected.Right - 1, 1, raffected.Right - 1, raffected.Height + 1);
                g.DrawPath(new Pen(Color.FromArgb(134, 134, 134)), path);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintGrip(PaintEventArgs e)
        {
            //base.OnPaintGrip(e);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="re"></param>
        /// <param name="pa"></param>
        public void DrawArc(Rectangle re, GraphicsPath pa)
        {
            int _radiusX0Y0 = _radius, _radiusXFY0 = _radius, _radiusX0YF = _radius, _radiusXFYF = _radius;
            pa.AddArc(re.X, re.Y, _radiusX0Y0, _radiusX0Y0, 180, 90);
            pa.AddArc(re.Width - _radiusXFY0, re.Y, _radiusXFY0, _radiusXFY0, 270, 90);
            pa.AddArc(re.Width - _radiusXFYF, re.Height - _radiusXFYF, _radiusXFYF, _radiusXFYF, 0, 90);
            pa.AddArc(re.X, re.Height - _radiusX0YF, _radiusX0YF, _radiusX0YF, 90, 90);
            pa.CloseFigure();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="re"></param>
        /// <returns></returns>
        public Rectangle Deflate(Rectangle re)
        {
            return new Rectangle(re.X + 1, re.Y + 1, re.Width - 2, re.Height - 2);
        }
        
    }
    /// <summary>
    /// Ribbon≤Àµ•“ı”∞
    /// </summary>
    public class RibbonMenuShadow : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public RibbonMenuShadow()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
                          ControlStyles.UserPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.Opaque, false);
            TransparencyKey = Color.Fuchsia;
            this.Padding = new Padding(1);
            FormBorderStyle = FormBorderStyle.None;
            
        }

        private int _radius = 8;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.Default;
            var reg = new Rectangle(0, 0, this.Width, this.Height);
            var path = new GraphicsPath();
            DrawArc(reg, path);
            this.Region = new Region(path);
           // g.FillRectangle(new SolidBrush(Color.Fuchsia), reg);

            var r = new Rectangle(10, 10, 110, 110); 
            path = new GraphicsPath(); DrawArc(r, path);
            
            g.FillPath(new SolidBrush(Color.FromArgb(10,10,10,10)), path);
            g.DrawPath(Pens.Red, path);
            //base.OnPaint(e);
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="re"></param>
        /// <param name="pa"></param>
        public void DrawArc(Rectangle re, GraphicsPath pa)
        {
            int _radiusX0Y0 = _radius, _radiusXFY0 = _radius, _radiusX0YF = _radius, _radiusXFYF = _radius;
            pa.AddArc(re.X, re.Y, _radiusX0Y0, _radiusX0Y0, 180, 90);
            pa.AddArc(re.Width - _radiusXFY0, re.Y, _radiusXFY0, _radiusXFY0, 270, 90);
            pa.AddArc(re.Width - _radiusXFYF, re.Height - _radiusXFYF, _radiusXFYF, _radiusXFYF, 0, 90);
            pa.AddArc(re.X, re.Height - _radiusX0YF, _radiusX0YF, _radiusX0YF, 90, 90);
            pa.CloseFigure();
        }
    }
}
