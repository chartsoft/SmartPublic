using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Smart.Win.Controls
{
    /// <summary>
    /// Ribbon按钮
    /// </summary>
    public class RibbonMenuButton : Button
    {
        #region About Constructor
        /// <summary>
        /// 
        /// </summary>
        public RibbonMenuButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                          ControlStyles.UserPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.Opaque, false);
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            BackColor = Color.Transparent;

            timer1.Interval = 5;
            timer1.Tick += timer1_Tick;

        }

        /// <summary>
        /// 
        /// </summary>
        public sealed override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            A0 = ColorBase.A;
            R0 = ColorBase.R;
            G0 = ColorBase.G;
            B0 = ColorBase.B;
            _colorStroke = _baseStroke;

            var r = new Rectangle(new Point(-1, -1), new Size(this.Width + _radius, this.Height + _radius));
            #region Transform to SmoothRectangle Region
            if (this.Size != null)
            {
                var pathregion = new GraphicsPath();
                DrawArc(r, pathregion);
                this.Region = new Region(pathregion);
            }
            #endregion
        }
        #endregion

        #region About Image Settings
        private e_imagelocation _imagelocation;
        /// <summary>
        /// 
        /// </summary>
        public enum e_imagelocation
        {
            /// <summary>
            /// 
            /// </summary>
            Top,
            /// <summary>
            /// 
            /// </summary>
            Bottom,
            /// <summary>
            /// 
            /// </summary>
            Left,
            /// <summary>
            /// 
            /// </summary>
            Right,
            /// <summary>
            /// 
            /// </summary>
            None
        }
        /// <summary>
        /// 
        /// </summary>
        public e_imagelocation ImageLocation
        {
            get { return _imagelocation; }
            set { _imagelocation = value; this.Refresh(); }
        }
        private int _imageoffset;
        /// <summary>
        /// 
        /// </summary>
        public int ImageOffset
        {
            get { return _imageoffset; }
            set { _imageoffset = value; }
        }
        private Point _maximagesize;
        /// <summary>
        /// 
        /// </summary>
        public Point MaxImageSize
        {
            get { return _maximagesize; }
            set { _maximagesize = value; }
        }

        private Image _grayImage;
        private int _imageHashCode;

        #endregion

        #region About Button Settings
        private e_showbase _showbase;
        private e_showbase _tempshowbase;
        /// <summary>
        /// 
        /// </summary>
        public enum e_showbase
        {
            /// <summary>
            /// 
            /// </summary>
            Yes,
            /// <summary>
            /// 
            /// </summary>
            No
        }
        /// <summary>
        /// 
        /// </summary>
        public e_showbase ShowBase
        {
            get { return _showbase; }
            set { _showbase = value; this.Refresh(); }
        }
        private int _radius = 6;
        /// <summary>
        /// 
        /// </summary>
        public int Radius
        {
            get { return _radius; }
            set { if (_radius > 0) _radius = value; this.Refresh(); }
        }
        private e_groupPos _grouppos;
        /// <summary>
        /// 
        /// </summary>
        public enum e_groupPos
        {
            /// <summary>
            /// 
            /// </summary>
            None,
            /// <summary>
            /// 
            /// </summary>
            Left,
            /// <summary>
            /// 
            /// </summary>
            Center,
            /// <summary>
            /// 
            /// </summary>
            Right,
            /// <summary>
            /// 
            /// </summary>
            Top,
            /// <summary>
            /// 
            /// </summary>
            Bottom
        }
        /// <summary>
        /// 
        /// </summary>
        public e_groupPos GroupPos
        {
            get { return _grouppos; }
            set { _grouppos = value; this.Refresh(); }
        }

        private e_arrow _arrow;
        /// <summary>
        /// 
        /// </summary>
        public enum e_arrow
        {
            /// <summary>
            /// 
            /// </summary>
            None,
            /// <summary>
            /// 
            /// </summary>
            ToRight,
            /// <summary>
            /// 
            /// </summary>
            ToDown
        }
        /// <summary>
        /// 
        /// </summary>
        public e_arrow Arrow
        {
            get { return _arrow; }
            set { _arrow = value; this.Refresh(); }
        }
        private e_splitbutton _splitbutton;
        /// <summary>
        /// 
        /// </summary>
        public enum e_splitbutton
        {
            /// <summary>
            /// 
            /// </summary>
            No,
            /// <summary>
            /// 
            /// </summary>
            Yes
        }
        /// <summary>
        /// 
        /// </summary>
        public e_splitbutton SplitButton
        {
            get { return _splitbutton; }
            set { _splitbutton = value; this.Refresh(); }
        }
        private int _splitdistance;
        /// <summary>
        /// 
        /// </summary>
        public int SplitDistance
        {
            get { return _splitdistance; }
            set { _splitdistance = value; this.Refresh(); }
        }
        private string _title = "";
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; this.Refresh(); }
        }
        private bool _keeppress;
        /// <summary>
        /// 
        /// </summary>
        public bool KeepPress
        {
            get { return _keeppress; }
            set { _keeppress = value; }
        }
        private bool _ispressed;
        /// <summary>
        /// 
        /// </summary>
        public bool IsPressed
        {
            get { return _ispressed; }
            set { _ispressed = value; }
        }


        #endregion

        #region Menu Pos
        private Point _menupos = new Point(0, 0);
        /// <summary>
        /// 
        /// </summary>
        public Point MenuPos
        {
            get { return _menupos; }
            set { _menupos = value; }
        }
        #endregion

        #region Colors
        private Color _baseColor = Color.FromArgb(209, 209, 209);
        private Color _onColor = Color.FromArgb(255, 255, 255);
        private Color _pressColor = Color.FromArgb(255, 255, 255);

        private Color _baseStroke = Color.FromArgb(255, 255, 255);
        private Color _onStroke = Color.FromArgb(255, 255, 255);
        private Color _pressStroke = Color.FromArgb(255, 255, 255);
        private Color _colorStroke = Color.FromArgb(255, 255, 255);
        private int A0, R0, G0, B0;
        /// <summary>
        /// 
        /// </summary>
        public Color ColorBase
        {
            get { return _baseColor; }
            set
            {
                _baseColor = value;
                R0 = _baseColor.R;
                B0 = _baseColor.B;
                G0 = _baseColor.G;
                A0 = _baseColor.A;
                var hsb = new RibbonColor(_baseColor);
                if (hsb.BC < 50)
                {
                    hsb.SetBrightness(60);
                }
                else
                {
                    hsb.SetBrightness(30);
                }
                if (_baseColor.A > 0)
                    _baseStroke = Color.FromArgb(100, hsb.GetColor());
                else
                    _baseStroke = Color.FromArgb(0, hsb.GetColor());
                this.Refresh();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Color ColorOn
        {
            get { return _onColor; }
            set
            {
                _onColor = value;

                var hsb = new RibbonColor(_onColor);
                if (hsb.BC < 50)
                {
                    hsb.SetBrightness(60);
                }
                else
                {
                    hsb.SetBrightness(30);
                }
                if (_baseStroke.A > 0)
                    _onStroke = Color.FromArgb(100, hsb.GetColor());
                else
                    _onStroke = Color.FromArgb(0, hsb.GetColor());
                this.Refresh();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Color ColorPress
        {
            get { return _pressColor; }
            set
            {
                _pressColor = value;

                var hsb = new RibbonColor(_pressColor);
                if (hsb.BC < 50)
                {
                    hsb.SetBrightness(60);
                }
                else
                {
                    hsb.SetBrightness(30);
                }
                if (_baseStroke.A > 0)
                    _pressStroke = Color.FromArgb(100, hsb.GetColor());
                else
                    _pressStroke = Color.FromArgb(0, hsb.GetColor());
                this.Refresh();

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Color ColorBaseStroke
        {
            get { return _baseStroke; }
            set { _baseStroke = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Color ColorOnStroke
        {
            get { return _onStroke; }
            set { _onStroke = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Color ColorPressStroke
        {
            get { return _pressStroke; }
            set { _pressStroke = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="h"></param>
        /// <param name="s"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public Color GetColorIncreased(Color color, int h, int s, int b)
        {
            var _color = new RibbonColor(color);
            var ss = _color.GetSaturation();
            float vc = b + _color.GetBrightness();
            float hc = h + _color.GetHue();
            float sc = s + ss;


            _color.VC = vc;
            _color.HC = hc;
            _color.SC = sc;

            return _color.GetColor();


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="A"></param>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public Color GetColor(int A, int R, int G, int B)
        {
            if (A + A0 > 255) { A = 255; } else { A = A + A0; }
            if (R + R0 > 255) { R = 255; } else { R = R + R0; }
            if (G + G0 > 255) { G = 255; } else { G = G + G0; }
            if (B + B0 > 255) { B = 255; } else { B = B + B0; }

            return Color.FromArgb(A, R, G, B);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pevent"></param>
        [DebuggerStepThrough]
        protected override void OnPaint(PaintEventArgs pevent)
        {
            #region Variables & Conf
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            var r = new Rectangle(new Point(-1, -1), new Size(this.Width + _radius, this.Height + _radius));
            #endregion

            #region Paint
            var path = new GraphicsPath();
            var rp = new Rectangle(new Point(0, 0), new Size(this.Width - 1, this.Height - 1)); DrawArc(rp, path);
            FillGradients(g, path);
            DrawImage(g);
            DrawString(g);
            DrawArrow(g);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            var r = new Rectangle(new Point(-1, -1), new Size(this.Width + _radius, this.Height + _radius));
            if (this.Size != null)
            {
                var pathregion = new GraphicsPath();
                DrawArc(r, pathregion);
                this.Region = new Region(pathregion);
            }
            base.OnResize(e);
        }

        /// <summary>
        /// 释放由 <see cref="T:System.Windows.Forms.ButtonBase"/> 占用的非托管资源，还可以另外再释放托管资源。
        /// </summary>
        /// <param name="disposing">为 true 则释放托管资源和非托管资源；为 false 则仅释放非托管资源。</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                timer1.Dispose();
            }
        }

        #region Paint Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="pa"></param>
        public void FillGradients(Graphics gr, GraphicsPath pa)
        {
            var origin = this.Height / 3; var end = this.Height; var oe = (end - origin) / 2;
            LinearGradientBrush lgbrush; Rectangle rect;
            if (_showbase == e_showbase.Yes)
            {
                rect = new Rectangle(new Point(0, 0), new Size(this.Width - 1, this.Height - 1));
                pa = new GraphicsPath();
                DrawArc(rect, pa);
                lgbrush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical);

                #region Main Gradient
                var pos = new float[4];
                pos[0] = 0.0F; pos[1] = 0.3F; pos[2] = 0.35F; pos[3] = 1.0F;
                var colors = new Color[4];
                if (i_mode == 0)
                {
                    colors[0] = GetColor(0, 35, 24, 9);
                    colors[1] = GetColor(0, 13, 8, 3);
                    colors[2] = Color.FromArgb(A0, R0, G0, B0);
                    colors[3] = GetColor(0, 28, 29, 14);
                }
                else
                {
                    colors[0] = GetColor(0, 0, 50, 100);
                    colors[1] = GetColor(0, 0, 0, 30);
                    colors[2] = Color.FromArgb(A0, R0, G0, B0);
                    colors[3] = GetColor(0, 0, 50, 100);
                }
                var mix = new ColorBlend();
                mix.Colors = colors;
                mix.Positions = pos;
                lgbrush.InterpolationColors = mix;
                gr.FillPath(lgbrush, pa);
                #endregion

                #region Fill Band
                rect = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height / 3));
                pa = new GraphicsPath(); var _rtemp = _radius; _radius = _rtemp - 1;
                DrawArc(rect, pa);
                if (A0 > 80)
                {
                    gr.FillPath(new SolidBrush(Color.FromArgb(60, 255, 255, 255)), pa);
                }
                _radius = _rtemp;
                #endregion

                #region SplitFill
                if (_splitbutton == e_splitbutton.Yes & mouse)
                {
                    FillSplit(gr);
                }
                #endregion

                #region Shadow
                if (i_mode == 2)
                {
                    rect = new Rectangle(1, 1, this.Width - 2, this.Height);
                    pa = new GraphicsPath();
                    DrawShadow(rect, pa);
                    gr.DrawPath(new Pen(Color.FromArgb(50, 20, 20, 20), 2.0F), pa);

                }
                else
                {
                    rect = new Rectangle(1, 1, this.Width - 2, this.Height - 1);
                    pa = new GraphicsPath();
                    DrawShadow(rect, pa);
                    if (A0 > 80)
                    {
                        gr.DrawPath(new Pen(Color.FromArgb(100, 250, 250, 250), 3.0F), pa);
                    }
                }
                #endregion

                #region SplitLine

                if (_splitbutton == e_splitbutton.Yes)
                {
                    if (_imagelocation == e_imagelocation.Top)
                    {
                        switch (i_mode)
                        {
                            case 1:
                                gr.DrawLine(new Pen(_onStroke), new Point(1, this.Height - _splitdistance), new Point(this.Width - 1, this.Height - _splitdistance));
                                break;
                            case 2:
                                gr.DrawLine(new Pen(_pressStroke), new Point(1, this.Height - _splitdistance), new Point(this.Width - 1, this.Height - _splitdistance));
                                break;
                            default:
                                break;
                        }
                    }
                    else if (_imagelocation == e_imagelocation.Left)
                    {
                        switch (i_mode)
                        {
                            case 1:
                                gr.DrawLine(new Pen(_onStroke), new Point(this.Width - _splitdistance, 0), new Point(this.Width - _splitdistance, this.Height));
                                break;
                            case 2:
                                gr.DrawLine(new Pen(_pressStroke), new Point(this.Width - _splitdistance, 0), new Point(this.Width - _splitdistance, this.Height));
                                break;
                            default:
                                break;
                        }
                    }

                }
                #endregion

                rect = new Rectangle(new Point(0, 0), new Size(this.Width - 1, this.Height - 1));
                pa = new GraphicsPath();
                DrawArc(rect, pa);
                gr.DrawPath(new Pen(_colorStroke, 0.9F), pa);

                pa.Dispose(); lgbrush.Dispose();


            }
        }

        private int offsetx;
        private int offsety;
        private int imageheight;
        private int imagewidth;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gr"></param>
        public void DrawImage(Graphics gr)
        {
            var imageToDisplay = GetImageToDraw();

            if (imageToDisplay != null)
            {
                offsety = _imageoffset; offsetx = _imageoffset;
                if (_imagelocation == e_imagelocation.Left | _imagelocation == e_imagelocation.Right)
                {
                    imageheight = this.Height - offsety * 2;
                    if (imageheight > _maximagesize.Y & _maximagesize.Y != 0)
                    { imageheight = _maximagesize.Y; }
                    imagewidth = (int)((Convert.ToDouble(imageheight) / imageToDisplay.Height) * imageToDisplay.Width);
                }
                else if (_imagelocation == e_imagelocation.Top | _imagelocation == e_imagelocation.Bottom)
                {
                    imagewidth = this.Width - offsetx * 2;
                    if (imagewidth > _maximagesize.X & _maximagesize.X != 0)
                    { imagewidth = _maximagesize.X; }
                    imageheight = (int)((Convert.ToDouble(imagewidth) / imageToDisplay.Width) * imageToDisplay.Height);

                }
                switch (_imagelocation)
                {
                    case e_imagelocation.Left:
                        gr.DrawImage(imageToDisplay, new Rectangle(offsetx, offsety, imagewidth, imageheight));
                        break;
                    case e_imagelocation.Right:
                        gr.DrawImage(imageToDisplay, new Rectangle(this.Width - imagewidth - offsetx, offsety, imagewidth, imageheight));
                        break;
                    case e_imagelocation.Top:
                        offsetx = this.Width / 2 - imagewidth / 2;
                        gr.DrawImage(imageToDisplay, new Rectangle(offsetx, offsety, imagewidth, imageheight));
                        break;
                    case e_imagelocation.Bottom:
                        gr.DrawImage(imageToDisplay, new Rectangle(offsetx, this.Height - imageheight - offsety, imagewidth, imageheight));
                        break;
                    default:
                        break;
                }
            }
        }

        private Image GetImageToDraw()
        {
            var image = this.Image;
            if (image == null)
            {
                _grayImage = null;
                _imageHashCode = 0;
            }
            // 灰度图像不存在或者已经发生改变
            else if (_grayImage == null || image.GetHashCode() != _imageHashCode)
            {
                //计算灰度图像，并记录原图像的hash值
                _grayImage = CalcGrayImage(image);
                _imageHashCode = image.GetHashCode();
            }

            return this.Enabled ? image : _grayImage;
        }

        private Image CalcGrayImage(Image image)
        {
            var bitmap = new Bitmap(image);
            for (var i = 0; i < bitmap.Width; i++)
            {
                for (var j = 0; j < bitmap.Height; j++)
                {
                    var color = bitmap.GetPixel(i, j);
                    var rgb = (int)(color.R * .3 + color.G * .59 + color.B * .11);
                    bitmap.SetPixel(i, j, Color.FromArgb(rgb, rgb, rgb));
                }
            }
            bitmap.MakeTransparent(Color.Black);

            return bitmap;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gr"></param>
        public void DrawString(Graphics gr)
        {
            if (this.Text != "")
            {
                var textwidth = (int)gr.MeasureString(this.Text, this.Font).Width;
                var textheight = (int)gr.MeasureString(this.Text, this.Font).Height;

                var extraoffset = 0;
                var fontb = new Font(this.Font, FontStyle.Bold);
                if (_title != "")
                {
                    extraoffset = textheight / 2;
                }

                var s1 = this.Text; var s2 = "";
                var jump = this.Text.IndexOf("\\n");

                if (jump != -1)
                {
                    s2 = s1.Substring(jump + 3); s1 = s1.Substring(0, jump);
                }

                #region Calc Color Brightness
                var __color = new RibbonColor(Color.FromArgb(R0, G0, B0));
                var forecolor = new RibbonColor(this.ForeColor);
                Color _forecolor;

                if (__color.GetBrightness() > 50)
                {
                    forecolor.BC = 1;
                    forecolor.SC = 80;
                }
                else
                {
                    forecolor.BC = 99;
                    forecolor.SC = 20;
                }
                _forecolor = forecolor.GetColor();
                if (!this.Enabled)
                {
                    _forecolor = Color.Gray;
                }
                #endregion

                switch (_imagelocation)
                {
                    case e_imagelocation.Left:
                        if (Title != "")
                        {
                            gr.DrawString(Title, fontb, new SolidBrush(_forecolor), new PointF(offsetx + imagewidth + 4, this.Font.Size / 2));
                            gr.DrawString(s1, this.Font, new SolidBrush(_forecolor), new PointF(offsetx + imagewidth + 4, 2 * this.Font.Size + 1));
                            gr.DrawString(s2, this.Font, new SolidBrush(_forecolor), new PointF(offsetx + imagewidth + 4, 3 * this.Font.Size + 4));
                        }
                        else
                        {
                            gr.DrawString(s1, this.Font, new SolidBrush(_forecolor), new PointF(offsetx + imagewidth + 4, this.Height / 2 - this.Font.Size + 1));
                        }

                        break;
                    case e_imagelocation.Right:
                        gr.DrawString(Title, fontb, new SolidBrush(_forecolor), new PointF(offsetx, this.Height / 2 - this.Font.Size + 1 - extraoffset));
                        gr.DrawString(this.Text, this.Font, new SolidBrush(_forecolor), new PointF(offsetx, extraoffset + this.Height / 2 - this.Font.Size + 1));
                        break;
                    case e_imagelocation.Top:
                        gr.DrawString(this.Text, this.Font, new SolidBrush(_forecolor), new PointF(this.Width / 2 - textwidth / 2 - 1, offsety + imageheight));
                        break;
                    case e_imagelocation.Bottom:
                        gr.DrawString(this.Text, this.Font, new SolidBrush(_forecolor), new PointF(this.Width / 2 - textwidth / 2 - 1, this.Height - imageheight - textheight - 1));
                        break;
                    default:
                        break;
                }

                fontb.Dispose();

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="re"></param>
        /// <param name="pa"></param>
        public void DrawArc(Rectangle re, GraphicsPath pa)
        {
            int _radiusX0Y0 = _radius, _radiusXFY0 = _radius, _radiusX0YF = _radius, _radiusXFYF = _radius;
            switch (_grouppos)
            {
                case e_groupPos.Left:
                    _radiusXFY0 = 1; _radiusXFYF = 1;
                    break;
                case e_groupPos.Center:
                    _radiusX0Y0 = 1; _radiusX0YF = 1; _radiusXFY0 = 1; _radiusXFYF = 1;
                    break;
                case e_groupPos.Right:
                    _radiusX0Y0 = 1; _radiusX0YF = 1;
                    break;
                case e_groupPos.Top:
                    _radiusX0YF = 1; _radiusXFYF = 1;
                    break;
                case e_groupPos.Bottom:
                    _radiusX0Y0 = 1; _radiusXFY0 = 1;
                    break;
            }
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
        /// <param name="pa"></param>
        public void DrawShadow(Rectangle re, GraphicsPath pa)
        {
            int _radiusX0Y0 = _radius, _radiusXFY0 = _radius, _radiusX0YF = _radius, _radiusXFYF = _radius;
            switch (_grouppos)
            {
                case e_groupPos.Left:
                    _radiusXFY0 = 1; _radiusXFYF = 1;
                    break;
                case e_groupPos.Center:
                    _radiusX0Y0 = 1; _radiusX0YF = 1; _radiusXFY0 = 1; _radiusXFYF = 1;
                    break;
                case e_groupPos.Right:
                    _radiusX0Y0 = 1; _radiusX0YF = 1;
                    break;
                case e_groupPos.Top:
                    _radiusX0YF = 1; _radiusXFYF = 1;
                    break;
                case e_groupPos.Bottom:
                    _radiusX0Y0 = 1; _radiusXFY0 = 1;
                    break;
            }
            pa.AddArc(re.X, re.Y, _radiusX0Y0, _radiusX0Y0, 180, 90);
            pa.AddArc(re.Width - _radiusXFY0, re.Y, _radiusXFY0, _radiusXFY0, 270, 90);
            pa.AddArc(re.Width - _radiusXFYF, re.Height - _radiusXFYF, _radiusXFYF, _radiusXFYF, 0, 90);
            pa.AddArc(re.X, re.Height - _radiusX0YF, _radiusX0YF, _radiusX0YF, 90, 90);
            pa.CloseFigure();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gr"></param>
        public void DrawArrow(Graphics gr)
        {
            var _size = 1;

            var __color = new RibbonColor(Color.FromArgb(R0, G0, B0));
            var forecolor = new RibbonColor(this.ForeColor);
            Color _forecolor;

            if (__color.GetBrightness() > 50)
            {
                forecolor.BC = 1;
                forecolor.SC = 80;
            }
            else
            {
                forecolor.BC = 99;
                forecolor.SC = 20;
            }
            _forecolor = forecolor.GetColor();

            switch (_arrow)
            {
                case e_arrow.ToDown:
                    if (_imagelocation == e_imagelocation.Left)
                    {
                        var points = new Point[3];
                        points[0] = new Point(this.Width - 8 * _size - _imageoffset, this.Height / 2 - _size / 2);
                        points[1] = new Point(this.Width - 2 * _size - _imageoffset, this.Height / 2 - _size / 2);
                        points[2] = new Point(this.Width - 5 * _size - _imageoffset, this.Height / 2 + _size * 2);
                        gr.FillPolygon(new SolidBrush(_forecolor), points);
                    }
                    else if (_imagelocation == e_imagelocation.Top)
                    {
                        var points = new Point[3];
                        points[0] = new Point(this.Width / 2 + 8 * _size - _imageoffset, this.Height - _imageoffset - 5 * _size);
                        points[1] = new Point(this.Width / 2 + 2 * _size - _imageoffset, this.Height - _imageoffset - 5 * _size);
                        points[2] = new Point(this.Width / 2 + 5 * _size - _imageoffset, this.Height - _imageoffset - 2 * _size);
                        gr.FillPolygon(new SolidBrush(_forecolor), points);
                    }
                    break;
                case e_arrow.ToRight:
                    if (_imagelocation == e_imagelocation.Left)
                    {
                        var arrowxpos = this.Width - _splitdistance + 2 * _imageoffset;
                        var points = new Point[3];
                        points[0] = new Point(arrowxpos + 4, this.Height / 2 - 4 * _size);
                        points[1] = new Point(arrowxpos + 8, this.Height / 2);
                        points[2] = new Point(arrowxpos + 4, this.Height / 2 + 4 * _size);
                        gr.FillPolygon(new SolidBrush(_forecolor), points);
                    }
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gr"></param>
        public void FillSplit(Graphics gr)
        {
            var _tranp = Color.FromArgb(200, 255, 255, 255);
            var x1 = this.Width - _splitdistance; var x2 = 0;
            var y1 = this.Height - _splitdistance; var y2 = 0;
            var btransp = new SolidBrush(_tranp);
            #region Horizontal
            if (_imagelocation == e_imagelocation.Left)
            {
                if (xmouse < this.Width - _splitdistance & mouse) //Small button
                {
                    var _r = new Rectangle(x1 + 1, 1, this.Width - 2, this.Height - 1);
                    var p = new GraphicsPath();
                    var _rtemp = _radius; _radius = 4;
                    DrawArc(_r, p);
                    _radius = _rtemp;
                    gr.FillPath(btransp, p);

                }
                else if (mouse) //Big Button
                {
                    var _r = new Rectangle(x2 + 1, 1, this.Width - _splitdistance - 1, this.Height - 1);
                    var p = new GraphicsPath();
                    var _rtemp = _radius; _radius = 4;
                    DrawArc(_r, p);
                    _radius = _rtemp;
                    gr.FillPath(btransp, p);
                }

            }
            #endregion

            #region Vertical
            else if (_imagelocation == e_imagelocation.Top)
            {
                if (ymouse < this.Height - _splitdistance & mouse) //Small button
                {
                    var _r = new Rectangle(1, y1 + 1, this.Width - 1, this.Height - 1);
                    var p = new GraphicsPath();
                    var _rtemp = _radius; _radius = 4;
                    DrawArc(_r, p);
                    _radius = _rtemp;
                    gr.FillPath(btransp, p);
                }
                else if (mouse) //Big Button
                {
                    var _r = new Rectangle(1, y2 + 1, this.Width - 1, this.Height - _splitdistance - 1);
                    var p = new GraphicsPath();
                    var _rtemp = _radius; _radius = 4;
                    DrawArc(_r, p);
                    _radius = _rtemp;
                    gr.FillPath(btransp, p);
                }
            }
            #endregion
            btransp.Dispose();

        }
        #endregion

        #region About Fading
        private Timer timer1 = new Timer();
        private int i_factor = 35;
        /// <summary>
        /// 
        /// </summary>
        public int FadingSpeed
        {
            get { return i_factor; }
            set
            {
                if (value > -1)
                {
                    i_factor = value;
                }
            }
        }

        private int i_fR = 1;
        private int i_fG = 1;
        private int i_fB = 1;
        private int i_fA = 1;

        [DebuggerStepThrough]
        private void timer1_Tick(object sender, EventArgs e)
        {
            #region Entering
            if (i_mode == 1)
            {
                if (Math.Abs(ColorOn.R - R0) > i_factor)
                { i_fR = i_factor; }
                else { i_fR = 1; }
                if (Math.Abs(ColorOn.G - G0) > i_factor)
                { i_fG = i_factor; }
                else { i_fG = 1; }
                if (Math.Abs(ColorOn.B - B0) > i_factor)
                { i_fB = i_factor; }
                else { i_fB = 1; }

                if (ColorOn.R < R0)
                {
                    R0 -= i_fR;
                }
                else if (ColorOn.R > R0)
                {
                    R0 += i_fR;
                }

                if (ColorOn.G < G0)
                {
                    G0 -= i_fG;
                }
                else if (ColorOn.G > G0)
                {
                    G0 += i_fG;
                }

                if (ColorOn.B < B0)
                {
                    B0 -= i_fB;
                }
                else if (ColorOn.B > B0)
                {
                    B0 += i_fB;
                }

                if (ColorOn == Color.FromArgb(R0, G0, B0))
                {
                    timer1.Stop();
                }
                else
                {
                    this.Refresh();
                }
            }
            #endregion

            #region Leaving
            if (i_mode == 0)
            {

                if (Math.Abs(ColorBase.R - R0) < i_factor)
                { i_fR = 1; }
                else { i_fR = i_factor; }
                if (Math.Abs(ColorBase.G - G0) < i_factor)
                { i_fG = 1; }
                else { i_fG = i_factor; }
                if (Math.Abs(ColorBase.B - B0) < i_factor)
                { i_fB = 1; }
                else { i_fB = i_factor; }
                if (Math.Abs(ColorBase.A - A0) < i_factor)
                { i_fA = 1; }
                else { i_fA = i_factor; }

                if (ColorBase.R < R0)
                {
                    R0 -= i_fR;
                }
                else if (ColorBase.R > R0)
                {
                    R0 += i_fR;
                }
                if (ColorBase.G < G0)
                {
                    G0 -= i_fG;
                }
                else if (ColorBase.G > G0)
                {
                    G0 += i_fG;
                }
                if (ColorBase.B < B0)
                {
                    B0 -= i_fB;
                }
                else if (ColorBase.B > B0)
                {
                    B0 += i_fB;
                }
                if (ColorBase.A < A0)
                {
                    A0 -= i_fA;
                }
                else if (ColorBase.A > A0)
                {
                    A0 += i_fA;
                }
                if (ColorBase == Color.FromArgb(A0, R0, G0, B0))
                {
                    timer1.Stop();

                }
                else
                {

                    this.Refresh();
                }

            }
            #endregion

            this.Refresh();
        }
        #endregion

        #region Mouse Events

        private int i_mode; //0 Entering, 1 Out,2 Press
        private int xmouse;
        private int ymouse;
        private bool mouse;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _colorStroke = ColorOnStroke;
            _tempshowbase = _showbase;
            _showbase = e_showbase.Yes;
            i_mode = 1;
            xmouse = PointToClient(Cursor.Position).X;
            mouse = true;
            A0 = 200;
            if (i_factor == 0)
            {
                R0 = _onColor.R; G0 = _onColor.G; B0 = _onColor.B;
            }
            timer1.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            UpdateLeave();
        }
        /// <summary>
        /// 
        /// </summary>
        public void UpdateLeave()
        {
            if (_keeppress == false | (_keeppress & _ispressed == false))
            {
                _colorStroke = ColorBaseStroke;
                _showbase = _tempshowbase;
                i_mode = 0;
                mouse = false;
                if (i_factor == 0)
                {
                    R0 = _baseColor.R; G0 = _baseColor.G; B0 = _baseColor.B;
                    this.Refresh();
                }
                else
                {
                    timer1.Stop();
                    timer1.Start();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mevent"></param>
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            R0 = ColorPress.R; G0 = ColorPress.G; B0 = ColorPress.B;
            _colorStroke = ColorPressStroke;
            _showbase = e_showbase.Yes;
            i_mode = 2;
            xmouse = PointToClient(Cursor.Position).X;
            ymouse = PointToClient(Cursor.Position).Y;
            mouse = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mevent"></param>
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            R0 = ColorOn.R; G0 = ColorOn.G; B0 = ColorOn.B;
            _colorStroke = ColorOnStroke;
            _showbase = e_showbase.Yes;
            i_mode = 1;
            mouse = true;

            #region ClickSplit
            if (_imagelocation == e_imagelocation.Left & xmouse > this.Width - _splitdistance & _splitbutton == e_splitbutton.Yes)
            {
                if (_arrow == e_arrow.ToDown)
                {
                    if (this.ContextMenuStrip != null)
                        this.ContextMenuStrip.Opacity = 1.0;
                    this.ContextMenuStrip.Show(this, 0, this.Height);

                }
                else if (_arrow == e_arrow.ToRight)
                {
                    if (this.ContextMenuStrip != null)
                    {
                        var menu = this.ContextMenuStrip;
                        this.ContextMenuStrip.Opacity = 1.0;
                        if (MenuPos.Y == 0)
                        {
                            this.ContextMenuStrip.Show(this, this.Width + 2, -this.Height);
                        }
                        else
                        {
                            this.ContextMenuStrip.Show(this, this.Width + 2, MenuPos.Y);
                        }
                    }

                }
            }
            else if (_imagelocation == e_imagelocation.Top & ymouse > this.Height - _splitdistance & _splitbutton == e_splitbutton.Yes)
            {
                if (_arrow == e_arrow.ToDown)
                {
                    if (this.ContextMenuStrip != null)
                        this.ContextMenuStrip.Show(this, 0, this.Height);
                }
            }
            #endregion
            else
            {
                base.OnMouseUp(mevent);

                #region Keep Press
                if (_keeppress)
                {
                    _ispressed = true;

                    try
                    {
                        foreach (Control _control in this.Parent.Controls)
                        {
                            if (typeof(RibbonMenuButton) == _control.GetType() & _control.Name != this.Name)
                            {
                                ((RibbonMenuButton)(_control))._ispressed = false;
                                ((RibbonMenuButton)(_control)).UpdateLeave();
                            }
                        }
                    }
                    catch { }


                }
                #endregion

            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mevent"></param>
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            if (mouse & SplitButton == e_splitbutton.Yes)
            {
                xmouse = PointToClient(Cursor.Position).X;
                ymouse = PointToClient(Cursor.Position).Y;
                this.Refresh();
            }
            base.OnMouseMove(mevent);
        }


        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    public class RibbonColor
    {

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        public RibbonColor(Color color)
        {
            rc = color.R;
            gc = color.G;
            bc = color.B;
            ac = color.A;

            HSV();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="brightness"></param>
        public RibbonColor(uint alpha, int hue, int saturation, int brightness)
        {
            hc = hue;
            sc = saturation;
            vc = brightness;
            ac = alpha;

            GetColor();
        }
        #endregion

        #region Alpha
        private uint ac; //Alpha > -1
        /// <summary>
        /// 
        /// </summary>
        public uint AC { get { return ac; } set { Math.Min(value, 255); } }
        #endregion

        #region RGB
        private int rc, gc, bc; //RGB Components > -1 
        /// <summary>
        /// 
        /// </summary>
        public int RC { get { return rc; } set { rc = Math.Min(value, 255); } }
        /// <summary>
        /// 
        /// </summary>
        public int GC { get { return gc; } set { gc = Math.Min(value, 255); } }
        /// <summary>
        /// 
        /// </summary>
        public int BC { get { return bc; } set { bc = Math.Min(value, 255); } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Color GetColor()
        {

            int conv;
            double hue, sat, val;
            int basis;

            hue = hc / 100.0f;
            sat = sc / 100.0f;
            val = vc / 100.0f;

            if ((float)sc == 0) // Gray Colors
            {
                conv = (int)(255.0f * val);
                rc = gc = bc = conv;
                return Color.FromArgb(rc, gc, bc);
            }

            basis = (int)(255.0f * (1.0 - sat) * val);

            switch ((int)((float)hc / 60.0f))
            {
                case 0:
                    rc = (int)(255.0f * val);
                    gc = (int)((255.0f * val - basis) * (hc / 60.0f) + basis);
                    bc = basis;
                    break;

                case 1:
                    rc = (int)((255.0f * val - basis) * (1.0f - ((hc % 60) / 60.0f)) + basis);
                    gc = (int)(255.0f * val);
                    bc = basis;
                    break;

                case 2:
                    rc = basis;
                    gc = (int)(255.0f * val);
                    bc = (int)((255.0f * val - basis) * ((hc % 60) / 60.0f) + basis);
                    break;

                case 3:
                    rc = basis;
                    gc = (int)((255.0f * val - basis) * (1.0f - ((hc % 60) / 60.0f)) + basis);
                    bc = (int)(255.0f * val);
                    break;

                case 4:
                    rc = (int)((255.0f * val - basis) * ((hc % 60) / 60.0f) + basis);
                    gc = basis;
                    bc = (int)(255.0f * val);
                    break;

                case 5:
                    rc = (int)(255.0f * val);
                    gc = basis;
                    bc = (int)((255.0f * val - basis) * (1.0f - ((hc % 60) / 60.0f)) + basis);
                    break;
            }
            return Color.FromArgb((int)ac, rc, gc, bc);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public uint GetRed()
        {
            return GetColor().R;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public uint GetGreen()
        {
            return GetColor().G;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public uint GetBlue()
        {
            return GetColor().B;
        }

        #endregion

        #region HSV

        private int hc, sc, vc;
        /// <summary>
        /// 
        /// </summary>
        public float HC { get { return hc; } set { hc = (int)Math.Min(value, 359); hc = Math.Max(hc, 0); } }
        /// <summary>
        /// 
        /// </summary>
        public float SC { get { return sc; } set { sc = (int)Math.Min(value, 100); sc = Math.Max(sc, 0); } }
        /// <summary>
        /// 
        /// </summary>
        public float VC { get { return vc; } set { vc = (int)Math.Min(value, 100); vc = Math.Max(vc, 0); } }
        /// <summary>
        /// 
        /// </summary>
        public enum C
        {
            /// <summary>
            /// 
            /// </summary>
            Red,

            /// <summary>
            /// 
            /// </summary>
            Green,

            /// <summary>
            /// 
            /// </summary>
            Blue,

            /// <summary>
            /// 
            /// </summary>
            None
        }
        private int maxval, minval;
        private C CompMax, CompMin;

        private void HSV()
        {
            hc = GetHue();
            sc = GetSaturation();
            vc = GetBrightness();
        }
        /// <summary>
        /// 
        /// </summary>
        public void CMax()
        {
            if (rc > gc)
            {
                if (rc < bc) { maxval = bc; CompMax = C.Blue; }
                else { maxval = rc; CompMax = C.Red; }
            }
            else
            {
                if (gc < bc) { maxval = bc; CompMax = C.Blue; }
                else { maxval = gc; CompMax = C.Green; }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void CMin()
        {
            if (rc < gc)
            {
                if (rc > bc) { minval = bc; CompMin = C.Blue; }
                else { minval = rc; CompMin = C.Red; }
            }
            else
            {
                if (gc > bc) { minval = bc; CompMin = C.Blue; }
                else { minval = gc; CompMin = C.Green; }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetBrightness()  //Brightness is from 0 to 100
        {
            CMax(); return 100 * maxval / 255;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetSaturation() //Saturation from 0 to 100
        {
            CMax(); CMin();
            if (CompMin == C.Blue) { }
            if (CompMax == C.None)
                return 0;
            if (maxval != minval)
            {
                var d_sat = Decimal.Divide(minval, maxval);
                d_sat = Decimal.Subtract(1, d_sat);
                d_sat = Decimal.Multiply(d_sat, 100);
                return Convert.ToUInt16(d_sat);
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetHue()
        {
            CMax(); CMin();

            if (maxval == minval)
            {
                return 0;
            }
            if (CompMax == C.Red)
            {
                if (gc >= bc)
                {
                    var d1 = Decimal.Divide((gc - bc), (maxval - minval));
                    return Convert.ToUInt16(60 * d1);
                }
                else
                {
                    var d1 = Decimal.Divide((bc - gc), (maxval - minval));
                    d1 = 60 * d1;
                    return Convert.ToUInt16(360 - d1);
                }
            }
            if (CompMax == C.Green)
            {
                if (bc >= rc)
                {
                    var d1 = Decimal.Divide((bc - rc), (maxval - minval));
                    d1 = 60 * d1;
                    return Convert.ToUInt16(120 + d1);
                }
                else
                {
                    var d1 = Decimal.Divide((rc - bc), (maxval - minval));
                    d1 = 60 * d1;
                    return Convert.ToUInt16(120 - d1);
                }


            }
            if (CompMax == C.Blue)
            {
                if (rc >= gc)
                {
                    var d1 = Decimal.Divide((rc - gc), (maxval - minval));
                    d1 = 60 * d1;
                    return Convert.ToUInt16(240 + d1);
                }
                else
                {
                    var d1 = Decimal.Divide((gc - rc), (maxval - minval));
                    d1 = 60 * d1;
                    return Convert.ToUInt16(240 - d1);
                }
            }
            return 0;
        }  //Hue from 0 to 100

        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsDark()
        {
            if (BC > 50)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        public void IncreaseBrightness(int val)
        {
            VC = VC + val;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        public void SetBrightness(int val)
        {
            VC = val;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        public void IncreaseHue(int val)
        {
            HC = HC + val;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        public void SetHue(int val)
        {
            HC = val;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        public void IncreaseSaturation(int val)
        {
            SC = SC + val;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        public void SetSaturation(int val)
        {
            SC = val;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="h"></param>
        /// <param name="s"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public Color IncreaseHSV(int h, int s, int b)
        {
            HC = HC + h;
            SC = SC + s;
            VC = VC + b;
            return GetColor();
        }

        #endregion

    }
}

