using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Smart.Win.Controls
{
    /// <summary>
    /// ͸����ť
    /// </summary>
    [DefaultEvent("Click")]
    public sealed partial class GlassButton : UserControl
    {
        #region Constructor

        /// <summary>
        /// ͸����ť
        /// </summary>
        public GlassButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(69, 105, 152);

            InitializeComponent();

            ButtonText = "Cloud button";
            iconSpacingX = 5;
            iconSpacingY = 5;

            textSpacingX = 5;
            textSpacingY = 5;

            #region MouseDown colors
            mouseDownColors = new Color[7];
            mouseDownColors[0] = Color.FromArgb(208, 193, 135);
            mouseDownColors[1] = Color.FromArgb(217, 208, 171);
            mouseDownColors[2] = Color.FromArgb(234, 220, 167);
            mouseDownColors[3] = Color.FromArgb(247, 239, 205);
            mouseDownColors[4] = Color.FromArgb(247, 215, 112);
            mouseDownColors[5] = Color.FromArgb(247, 200, 49);
            mouseDownColors[6] = Color.FromArgb(247, 221, 132);
            #endregion

            #region MouseOn colors
            mouseOnColors = new Color[7];
            mouseOnColors[0] = Color.FromArgb(191, 168, 113);
            mouseOnColors[1] = Color.FromArgb(208, 185, 129);
            mouseOnColors[2] = Color.FromArgb(243, 231, 182);
            mouseOnColors[3] = Color.FromArgb(255, 248, 217);
            mouseOnColors[4] = Color.FromArgb(255, 226, 133);
            mouseOnColors[5] = Color.FromArgb(255, 213, 77);
            mouseOnColors[6] = Color.FromArgb(255, 232, 151);
            #endregion

            #region Normal colors
            normalColors = new Color[7];
            normalColors[0] = Color.Transparent;//Color.FromArgb(161, 189, 207);
            normalColors[1] = Color.Transparent;// Color.FromArgb(202, 214, 212);
            normalColors[2] = Color.Transparent;// Color.FromArgb(188, 208, 221);
            normalColors[3] = Color.Transparent;// Color.FromArgb(246, 249, 253);
            normalColors[4] = Color.Transparent;// Color.FromArgb(229, 238, 249);
            normalColors[5] = Color.Transparent;// Color.FromArgb(210, 225, 244);
            normalColors[6] = Color.Transparent;// Color.FromArgb(233, 242, 253);
            #endregion

            #region Disabled colors
            disabledColors = new Color[7];
            disabledColors[0] = Color.FromArgb(191, 168, 113);
            disabledColors[1] = Color.FromArgb(208, 185, 129);
            disabledColors[2] = Color.FromArgb(243, 231, 182);
            disabledColors[3] = Color.FromArgb(255, 248, 217);
            disabledColors[4] = Color.FromArgb(255, 226, 133);
            disabledColors[5] = Color.FromArgb(255, 213, 77);
            disabledColors[6] = Color.FromArgb(255, 232, 151);
            //disabledColors[0] = Color.FromArgb(212, 212, 212);
            //disabledColors[1] = Color.FromArgb(239, 239, 239);
            //disabledColors[2] = Color.FromArgb(224, 224, 224);
            //disabledColors[3] = Color.FromArgb(251, 251, 251);
            //disabledColors[4] = Color.FromArgb(244, 244, 244);
            //disabledColors[5] = Color.FromArgb(235, 235, 235);
            //disabledColors[6] = Color.FromArgb(236, 236, 236);
            #endregion
        }

        #endregion

        #region Default button colors

        private Color borderLineColor = Color.FromArgb(161, 189, 207);
        private Color borderPointColor = Color.FromArgb(202, 214, 212);
        private Color borderPoint2Color = Color.FromArgb(188, 208, 221);
        private Color upperGradientColor1 = Color.FromArgb(246, 249, 253);
        private Color upperGradientColor2 = Color.FromArgb(229, 238, 249);
        private Color lowerGradientColor1 = Color.FromArgb(210, 225, 244);
        private Color lowerGradientColor2 = Color.FromArgb(233, 242, 253);

        #endregion

        #region Tools

        private Pen borderPen;
        private Brush gradientBrush;

        #endregion

        #region Drawing the button
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Sometimes when button is disabled in design-time, it doesn't work
            // so let's put this codeblock in here. Due to be removed in future

            //try
            //{
                if (this.Enabled == false)
                {
                    borderLineColor = disabledColors[0];
                    borderPointColor = disabledColors[1];
                    borderPoint2Color = disabledColors[2];
                    upperGradientColor1 = disabledColors[3];
                    upperGradientColor2 = disabledColors[4];
                    lowerGradientColor1 = disabledColors[5];
                    lowerGradientColor2 = disabledColors[6];
                }
            //}
            //catch
            //{
            //}

            #region Long borderlines

            borderPen = new Pen(borderLineColor);

            var point1 = new Point(0, 2);
            var point2 = new Point(0, this.Height - 3);

            e.Graphics.DrawLine(borderPen, point1, point2);

            point1 = new Point(this.Width - 1, 2);
            point2 = new Point(this.Width - 1, this.Height - 3);

            e.Graphics.DrawLine(borderPen, point1, point2);

            point1 = new Point(2, 0);
            point2 = new Point(this.Width - 3, 0);

            e.Graphics.DrawLine(borderPen, point1, point2);

            point1 = new Point(2, this.Height - 1);
            point2 = new Point(this.Width - 3, this.Height - 1);

            e.Graphics.DrawLine(borderPen, point1, point2);
            #endregion

            #region Border points

            borderPen = new Pen(borderPointColor);

            e.Graphics.DrawEllipse(borderPen, 1, 0, 1, 1);
            e.Graphics.DrawEllipse(borderPen, 0, 1, 1, 1);

            e.Graphics.DrawEllipse(borderPen, 1, this.Height - 1, 1, 1);
            e.Graphics.DrawEllipse(borderPen, 0, this.Height - 2, 1, 1);

            e.Graphics.DrawEllipse(borderPen, this.Width - 2, 0, 1, 1);
            e.Graphics.DrawEllipse(borderPen, this.Width - 1, 1, 1, 1);

            e.Graphics.DrawEllipse(borderPen, this.Width - 2, this.Height - 1, 1, 1);
            e.Graphics.DrawEllipse(borderPen, this.Width - 1, this.Height - 2, 1, 1);

            borderPen = new Pen(borderPoint2Color);

            e.Graphics.DrawEllipse(borderPen, 1, 1, 1, 1);
            e.Graphics.DrawEllipse(borderPen, 1, this.Height - 2, 1, 1);
            e.Graphics.DrawEllipse(borderPen, this.Width - 2, 1, 1, 1);
            e.Graphics.DrawEllipse(borderPen, this.Width - 2, this.Height - 2, 1, 1);

            #endregion

            #region Gradients

            // Upper gradient
            var upperGradientPoint1 = new PointF(1, 1);
            var upperGradientPoint2 = new PointF(1, this.Height / 2.4F);

            gradientBrush = new LinearGradientBrush(upperGradientPoint1, upperGradientPoint2, upperGradientColor1, upperGradientColor2);
            e.Graphics.FillRectangle(gradientBrush, upperGradientPoint1.X, upperGradientPoint1.Y, this.Width - 2, upperGradientPoint2.Y);


            // Lower gradient
            var lowerGradientPoint1 = new PointF(upperGradientPoint2.X, upperGradientPoint2.Y);
            var lowerGradientPoint2 = new PointF(upperGradientPoint2.X, this.Height - 1);

            gradientBrush = new LinearGradientBrush(lowerGradientPoint1, lowerGradientPoint2, lowerGradientColor1, lowerGradientColor2);
            e.Graphics.FillRectangle(gradientBrush, lowerGradientPoint1.X, lowerGradientPoint1.Y, this.Width - 2, this.Height - upperGradientPoint2.Y - 1);

            #endregion

            if (textImageRelation == ButtonTextImageRelation.TextAboveImage)
            {
                drawIcon(e.Graphics);
                drawText(e.Graphics);
            }

            else
            {
                drawText(e.Graphics);
                drawIcon(e.Graphics);
            }
        }

        PointF textPoint;
        Brush textBrush;

        private FontStyle usedFontStyle = FontStyle.Regular;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [Description("What style should the font be in the button")]
        [DefaultValue(FontStyle.Regular)]
        public FontStyle FontsStyle
        {
            get
            {
                return usedFontStyle;
            }

            set
            {
                usedFontStyle = value;
            }
        }

        private int fontSize = 12;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [Description("Font's size on the button")]
        [DefaultValue(12)]
        public int FontSize
        {
            get
            {
                return fontSize;
            }

            set
            {
                fontSize = value;
            }
        }

        private float iconTransparency;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [Description("Icon's transparency on the button (0f - 1f)")]
        [DefaultValue(1f)]
        public float IconTransparency
        {
            get
            {
                return iconTransparency;
            }

            set
            {
                // Check if the value is between 0 and 1f
                if (value > 0f && value < 1f)
                {
                    iconTransparency = value;
                    try
                    {
                        transparentIcon = MakeTransparentImage((Image)icon, iconTransparency);
                        transparentDisabledIcon = MakeTransparentImage((Image)IconDisabled, iconTransparency);
                    }
                    catch
                    {
                        // Not yet ready or something?
                    }
                }
            }
        }        

        private void drawText(Graphics graphics)
        {
            var font = new Font("Segoe UI", fontSize, usedFontStyle, GraphicsUnit.Pixel);

            if (this.Enabled == true)
            {
                textBrush = new SolidBrush(this.ForeColor);
            }

            else
            {
                textBrush = new SolidBrush(Color.DarkGray);
            }

            switch (textAlign)
            {
                case ButtonTextAlign.Left:
                    textPoint = new PointF(textSpacingX, this.Height / 2 - graphics.MeasureString(text, font).Height / 2);
                    break;

                case ButtonTextAlign.Right:
                    textPoint = new PointF(this.Width - graphics.MeasureString(text, font).Width - textSpacingX, this.Height / 2 - graphics.MeasureString(text, font).Height / 2);
                    break;

                case ButtonTextAlign.Up:
                    textPoint = new PointF(this.Width / 2 - graphics.MeasureString(text, font).Width / 2, textSpacingY);
                    break;

                case ButtonTextAlign.Down:
                    textPoint = new PointF(this.Width / 2 - graphics.MeasureString(text, font).Width / 2, this.Height - graphics.MeasureString(text, font).Height - textSpacingY);
                    break;

                case ButtonTextAlign.UpperLeft:
                    textPoint = new PointF(textSpacingX, textSpacingY);
                    break;

                case ButtonTextAlign.UpperRight:
                    textPoint = new PointF(this.Width - graphics.MeasureString(text, font).Width - textSpacingX, textSpacingY);
                    break;

                case ButtonTextAlign.BottomLeft:
                    textPoint = new PointF(textSpacingX, this.Height - graphics.MeasureString(text, font).Height - textSpacingY);
                    break;

                case ButtonTextAlign.BottomRight:
                    textPoint = new PointF(this.Width - graphics.MeasureString(text, font).Width - textSpacingX, this.Height - graphics.MeasureString(text, font).Height - textSpacingY);
                    break;

                case ButtonTextAlign.Center:
                    textPoint = new PointF(this.Width / 2 - graphics.MeasureString(text, font).Width / 2, this.Height / 2 - graphics.MeasureString(text, font).Height / 2);
                    break;
            }

            if (text != null)
            {
                graphics.DrawString(text, font, textBrush, textPoint);
            }
        }

        private PointF imagePoint;

        private void drawIcon(Graphics graphics)
        {
            try
            {
                if (icon != null)
                {
                    switch (iconAlign)
                    {
                        case IconBitmapAlign.Left:
                            imagePoint = new PointF(iconSpacingX, this.Height / 2 - icon.Height / 2);
                            break;

                        case IconBitmapAlign.Right:
                            imagePoint = new PointF(this.Width - icon.Width - iconSpacingX, this.Height / 2 - icon.Height / 2);
                            break;

                        case IconBitmapAlign.Up:
                            imagePoint = new PointF(this.Width / 2 - icon.Width / 2, iconSpacingY);
                            break;

                        case IconBitmapAlign.Down:
                            imagePoint = new PointF(this.Width / 2 - icon.Width / 2, this.Height - iconSpacingY - icon.Height);
                            break;

                        case IconBitmapAlign.UpperLeft:
                            imagePoint = new PointF(iconSpacingX, iconSpacingY);
                            break;

                        case IconBitmapAlign.UpperRight:
                            imagePoint = new PointF(this.Width - icon.Width - iconSpacingX, iconSpacingY);
                            break;

                        case IconBitmapAlign.BottomLeft:
                            imagePoint = new PointF(iconSpacingX, this.Height - icon.Height - iconSpacingY);
                            break;

                        case IconBitmapAlign.BottomRight:
                            imagePoint = new PointF(this.Width - icon.Width - iconSpacingX, this.Height - icon.Height - iconSpacingY);
                            break;

                        case IconBitmapAlign.Center:
                            imagePoint = new PointF((this.Width / 2) - (icon.Width / 2), (this.Height / 2) - (icon.Height / 2));
                            //MessageBox.Show("" + (this.Width / 2));
                            break;
                    }
                    if (this.Enabled == true)
                    {
                        // If icon's transparency is 0 or greater, not 1
                        if (iconTransparency != 1)
                        {
                            graphics.DrawImage(transparentIcon, imagePoint.X, imagePoint.Y, icon.Width, icon.Height);
                        }
                        else
                        {
                            graphics.DrawImage(icon, imagePoint.X, imagePoint.Y, icon.Width, icon.Height);
                        }

                    }
                    else
                    {
                        if (iconTransparency != 1)
                        {
                            graphics.DrawImage(transparentDisabledIcon, imagePoint.X, imagePoint.Y, icon.Width, icon.Height);
                        }
                        else
                        {
                            graphics.DrawImage(IconDisabled, imagePoint.X, imagePoint.Y, icon.Width, icon.Height);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Button properties

        #region Button's text

        // Text of the button
        /// <summary>
        /// 
        /// </summary>
        private string text;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [Category("Text")]
        public string ButtonText
        {
            get
            {
                return text;
            }

            set
            {
                text = value;

                // Paint the control to show the modified text
                this.Invalidate();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public enum ButtonTextAlign
        {
            /// <summary>
            /// 
            /// </summary>
            Left = 0,

            /// <summary>
            /// 
            /// </summary>
            Right = 1,

            /// <summary>
            /// 
            /// </summary>
            Up = 2,

            /// <summary>
            /// 
            /// </summary>
            Down = 3,

            /// <summary>
            /// 
            /// </summary>
            UpperLeft = 4,

            /// <summary>
            /// 
            /// </summary>
            UpperRight = 5,

            /// <summary>
            /// 
            /// </summary>
            BottomLeft = 6,

            /// <summary>
            /// 
            /// </summary>
            BottomRight = 7,

            /// <summary>
            /// 
            /// </summary>
            Center = 8
        }

        private ButtonTextAlign textAlign = ButtonTextAlign.Center;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [DefaultValue(ButtonTextAlign.Center)]
        public ButtonTextAlign TextAlign
        {
            get
            {
                return textAlign;
            }

            set
            {
                textAlign = value;
                this.Invalidate();
            }
        }

        int textSpacingX;
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public int TextSpacingX
        {
            get
            {
                return textSpacingX;
            }

            set
            {
                textSpacingX = value;
                this.Invalidate();
            }
        }

        int textSpacingY;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public int TextSpacingY
        {
            get
            {
                return textSpacingY;
            }

            set
            {
                textSpacingY = value;
                this.Invalidate();
            }
        }

        #endregion

        //#region Border smoothing

        //// Defines if the border in MouseOn mode is smooth or sharp.
        //// Smooth looks smoother, but sharp is more clear and sharp.
        //private bool smoothBorder = false;

        //[Browsable(true)]
        //[Description("Is the border smooth or sharp in MouseOn event")]
        //[DefaultValue(true)]
        //public bool SmoothBorder
        //{
        //    get
        //    {
        //        return this.smoothBorder;
        //    }

        //    set
        //    {
        //        this.smoothBorder = value;
        //    }
        //}
        //#endregion

        #region Button's icon

        Bitmap icon;
        Bitmap transparentIcon;
        Bitmap transparentDisabledIcon;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public Bitmap Icon
        {
            get
            {
                return icon;
            }

            set
            {
                icon = value;
                this.Invalidate();
                generateDisabledIcon();
                if(icon!=null)
                {
                // Generate a new transparent icon for normal and disabled states
                try
                {
                    transparentIcon = MakeTransparentImage(icon, iconTransparency);
                    transparentDisabledIcon = MakeTransparentImage(IconDisabled, iconTransparency);
                }
                catch
                {
                    // Not yet ready or something?
                }
                    }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public enum IconBitmapAlign
        {
            /// <summary>
            /// 
            /// </summary>
            Left = 0,

            /// <summary>
            /// 
            /// </summary>
            Right = 1,

            /// <summary>
            /// 
            /// </summary>
            Up = 2,

            /// <summary>
            /// 
            /// </summary>
            Down = 3,

            /// <summary>
            /// 
            /// </summary>
            UpperLeft = 4,

            /// <summary>
            /// 
            /// </summary>
            UpperRight = 5,

            /// <summary>
            /// 
            /// </summary>
            BottomLeft = 6,

            /// <summary>
            /// 
            /// </summary>
            BottomRight = 7,

            /// <summary>
            /// 
            /// </summary>
            Center = 8
        }

        private IconBitmapAlign iconAlign = IconBitmapAlign.Left;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public IconBitmapAlign IconAlign
        {
            get
            {
                return iconAlign;
            }

            set
            {
                iconAlign = value;
                this.Invalidate();
            }
        }

        int iconSpacingX;
        int iconSpacingY;

        //[Browsable(true)]

        //[Description("Icon spacing"), Editor(typeof(SpacingEditor), typeof(UITypeEditor))]
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public int IconSpacingX
        {
            get
            {
                return iconSpacingX;
            }

            set
            {

                iconSpacingX = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public int IconSpacingY
        {
            get
            {
                return iconSpacingY;
            }

            set
            {
                iconSpacingY = value;
                this.Invalidate();
            }
        }

        #endregion

        #region TextImageRelation

        /// <summary>
        /// 
        /// </summary>
        public enum ButtonTextImageRelation
        {
            /// <summary>
            /// 
            /// </summary>
            ImageAboveText = 0,

            /// <summary>
            /// 
            /// </summary>
            TextAboveImage = 1
        }

        private ButtonTextImageRelation textImageRelation = ButtonTextImageRelation.TextAboveImage;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public ButtonTextImageRelation TextImageRelation
        {
            get
            {
                return textImageRelation;
            }

            set
            {
                textImageRelation = value;
                this.Invalidate();
            }
        }
        #endregion

        #endregion

        #region Define Colors

        #region Arrays
        /// <summary>
        /// 
        /// </summary>
        private Color[] mouseOnColors;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public Color[] MouseOn_Colors
        {
            get
            {
                return mouseOnColors;
            }

            set
            {
                mouseOnColors = value;
                // Because the colors don't update when they are changed
                // it's possible to do that by calling CloudButtonOwn_MouseLeave event
                CloudButtonOwn_MouseLeave(this, null);
                this.Invalidate();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private Color[] mouseDownColors;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public Color[] MouseDown_Colors
        {
            get
            {
                return mouseDownColors;
            }

            set
            {
                mouseDownColors = value;
                // Because the colors don't update when they are changed
                // it's possible to do that by calling CloudButtonOwn_MouseLeave event
                CloudButtonOwn_MouseLeave(this, null);
                this.Invalidate();
            }
        }

        private Color[] normalColors;
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public Color[] Normal_Colors
        {
            get
            {
                return normalColors;
            }

            set
            {
                normalColors = value;
                // Because the colors don't update when they are changed
                // it's possible to do that by calling CloudButtonOwn_MouseLeave event
                CloudButtonOwn_MouseLeave(this, null);
                this.Invalidate();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private Color[] disabledColors;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public Color[] DisabledColors
        {
            get
            {
                return disabledColors;
            }

            set
            {
                disabledColors = value;
                this.Invalidate();
            }
        }

        #endregion


        bool normalToMouseEnter;
        bool mouseDownToMouseEnter;

        private void CloudButtonOwn_MouseEnter(object sender, EventArgs e)
        {
            // If we want a smooth border, default colors are assigned
            //if (smoothBorder == true)
            //{
            //    borderLineColor = Color.FromArgb(222, 208, 156);
            //    borderPointColor = Color.FromArgb(228, 220, 186);
            //}

            // ...otherwise we use more sharp color in MouseOn mode
            //else
            //{
            //}

            // If the button is disabled, it doesn't look at mouse events

            if (this.Enabled == true)
            {
                if (areAnimationsEnabled == false)
                {
                    borderLineColor = Color.FromArgb(191, 168, 113);
                    borderPointColor = Color.FromArgb(208, 185, 129);
                    borderPoint2Color = Color.FromArgb(243, 231, 182);
                    upperGradientColor1 = Color.FromArgb(255, 248, 217);
                    upperGradientColor2 = Color.FromArgb(255, 226, 133);
                    lowerGradientColor1 = Color.FromArgb(255, 213, 77);
                    lowerGradientColor2 = Color.FromArgb(255, 232, 151);
                }

                else
                {
                    normalToMouseEnter = true;
                    timerFade.Start();
                }

                this.Invalidate();
                //    }
                //    else
                //    {
                //        CloudButtonOwn_MouseLeave(this, null);
                //this.Invalidate();
                //    }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void CMouseLeave()
        {
            if (areAnimationsEnabled == false)
                {
                    borderLineColor = normalColors[0];
                    borderPointColor = normalColors[1];
                    borderPoint2Color = normalColors[2];
                    upperGradientColor1 = normalColors[3];
                    upperGradientColor2 = normalColors[4];
                    lowerGradientColor1 = normalColors[5];
                    lowerGradientColor2 = normalColors[6];
                }

                else
                {
                    normalToMouseEnter = false;
                    timerFade.Start();
                }
        }

        private void CloudButtonOwn_MouseLeave(object sender, EventArgs e)
        {
            //borderLineColor = Color.FromArgb(161, 189, 207);
            //borderPointColor = Color.FromArgb(202, 214, 212);
            //borderPoint2Color = Color.FromArgb(188, 208, 221);
            //upperGradientColor1 = Color.FromArgb(246, 249, 253);
            //upperGradientColor2 = Color.FromArgb(229, 238, 249);
            //lowerGradientColor1 = Color.FromArgb(210, 225, 244);
            //lowerGradientColor2 = Color.FromArgb(233, 242, 253);

            borderLineColor = normalColors[0];
            borderPointColor = normalColors[1];
            borderPoint2Color = normalColors[2];
            upperGradientColor1 = normalColors[3];
            upperGradientColor2 = normalColors[4];
            lowerGradientColor1 = normalColors[5];
            lowerGradientColor2 = normalColors[6];

            if (isButtonSelected == false)
            {
                if (areAnimationsEnabled == false)
                {
                    borderLineColor = normalColors[0];
                    borderPointColor = normalColors[1];
                    borderPoint2Color = normalColors[2];
                    upperGradientColor1 = normalColors[3];
                    upperGradientColor2 = normalColors[4];
                    lowerGradientColor1 = normalColors[5];
                    lowerGradientColor2 = normalColors[6];
                }

                else
                {
                    normalToMouseEnter = false;
                    timerFade.Start();
                }
            }


            this.Invalidate();
        }

        //bool isMouseClickedWhileOperation = false;

        bool isButtonSelected;

        private void CloudButtonOwn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                borderLineColor = Color.FromArgb(208, 193, 135);
                borderPointColor = Color.FromArgb(217, 208, 171);
                borderPoint2Color = Color.FromArgb(234, 220, 167);
                upperGradientColor1 = Color.FromArgb(247, 239, 205);
                upperGradientColor2 = Color.FromArgb(247, 215, 112);
                lowerGradientColor1 = Color.FromArgb(247, 200, 49);
                lowerGradientColor2 = Color.FromArgb(247, 221, 132);

                //borderLineColor = mouseDownColors[0];
                //borderPointColor = mouseDownColors[1];
                //borderPoint2Color = mouseDownColors[2];
                //upperGradientColor1 = mouseDownColors[3];
                //upperGradientColor2 = mouseDownColors[4];
                //lowerGradientColor1 = mouseDownColors[5];
                //lowerGradientColor2 = mouseDownColors[6];

                // If there is some process going on
                //if (timerFade.Enabled == true && mouseOnToMouseClick == false)
                //{
                //    //isMouseClickedWhileOperation = true;
                //}

                //else
                //{
                //    //borderLineColor = mouseDownColors[0];
                //    //borderPointColor = mouseDownColors[1];
                //    //borderPoint2Color = mouseDownColors[2];
                //    //upperGradientColor1 = mouseDownColors[3];
                //    //upperGradientColor2 = mouseDownColors[4];
                //    //lowerGradientColor1 = mouseDownColors[5];
                //    //lowerGradientColor2 = mouseDownColors[6];
                //}

                isButtonSelected = true;

                this.Invalidate();
            }
        }

        private void CloudButtonOwn_MouseUp(object sender, MouseEventArgs e)
        {
            CloudButtonOwn_MouseEnter(this, null);
            timerFade.Start();
            mouseDownToMouseEnter = true;
        }

        #endregion

        private void CloudButtonOwn_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Capture == true)
            {

            }
        }

        int index;

        private void timerFade_Tick(object sender, EventArgs e)
        {
            if (normalToMouseEnter)
            {
                if (index <= count)
                {
                    borderLineColor = getReadyColor(mouseOnColors[0], normalColors[0]);
                    borderPointColor = getReadyColor(mouseOnColors[1], normalColors[1]);
                    borderPoint2Color = getReadyColor(mouseOnColors[2], normalColors[2]);
                    upperGradientColor1 = getReadyColor(mouseOnColors[3], normalColors[3]);
                    upperGradientColor2 = getReadyColor(mouseOnColors[4], normalColors[4]);
                    lowerGradientColor1 = getReadyColor(mouseOnColors[5], normalColors[5]);
                    lowerGradientColor2 = getReadyColor(mouseOnColors[6], normalColors[6]);

                    index++;
                }

                else
                {
                    if (leaveIsHappened)
                    {
                        setNormalColors();
                        leaveIsHappened = false;
                    }
                    timerFade.Stop();
                    index = 0;
                    normalToMouseEnter = false;
                }
            }

            else
            {
                if (index <= count)
                {
                    borderLineColor = getReadyColor(normalColors[0], mouseOnColors[0]);
                    borderPointColor = getReadyColor(normalColors[1], mouseOnColors[1]);
                    borderPoint2Color = getReadyColor(normalColors[2], mouseOnColors[2]);
                    upperGradientColor1 = getReadyColor(normalColors[3], mouseOnColors[3]);
                    upperGradientColor2 = getReadyColor(normalColors[4], mouseOnColors[4]);
                    lowerGradientColor1 = getReadyColor(normalColors[5], mouseOnColors[5]);
                    lowerGradientColor2 = getReadyColor(normalColors[6], mouseOnColors[6]);

                    index++;
                }

                else
                {
                    if (leaveIsHappened)
                    {
                        setNormalColors();
                        leaveIsHappened = false;
                    }
                    timerFade.Stop();
                    index = 0;
                }
            }

            if (mouseDownToMouseEnter)
            {
                if (index <= count)
                {
                    borderLineColor = getReadyColor(mouseDownColors[0], mouseOnColors[0]);
                    borderPointColor = getReadyColor(mouseDownColors[1], mouseOnColors[1]);
                    borderPoint2Color = getReadyColor(mouseDownColors[2], mouseOnColors[2]);
                    upperGradientColor1 = getReadyColor(mouseDownColors[3], mouseOnColors[3]);
                    upperGradientColor2 = getReadyColor(mouseDownColors[4], mouseOnColors[4]);
                    lowerGradientColor1 = getReadyColor(mouseDownColors[5], mouseOnColors[5]);
                    lowerGradientColor2 = getReadyColor(mouseDownColors[6], mouseOnColors[6]);

                    index++;
                }

                else
                {
                    if (leaveIsHappened)
                    {
                        setNormalColors();
                        leaveIsHappened = false;
                    }
                    timerFade.Stop();
                    index = 0;
                    mouseDownToMouseEnter = false;

                    //// If mouse clicked while operation
                    //if (isMouseClickedWhileOperation == true)
                    //{
                    //    borderLineColor = mouseDownColors[0];
                    //    borderPointColor = mouseDownColors[1];
                    //    borderPoint2Color = mouseDownColors[2];
                    //    upperGradientColor1 = mouseDownColors[3];
                    //    upperGradientColor2 = mouseDownColors[4];
                    //    lowerGradientColor1 = mouseDownColors[5];
                    //    lowerGradientColor2 = mouseDownColors[6];
                    //}
                }
            }

            if (normalToDisabled)
            {
                if (index <= count)
                {
                    borderLineColor = getReadyColor(normalColors[0], disabledColors[0]);
                    borderPointColor = getReadyColor(normalColors[1], disabledColors[1]);
                    borderPoint2Color = getReadyColor(normalColors[2], disabledColors[2]);
                    upperGradientColor1 = getReadyColor(normalColors[3], disabledColors[3]);
                    upperGradientColor2 = getReadyColor(normalColors[4], disabledColors[4]);
                    lowerGradientColor1 = getReadyColor(normalColors[5], disabledColors[5]);
                    lowerGradientColor2 = getReadyColor(normalColors[6], disabledColors[6]);

                    index++;
                }

                else
                {
                    if (leaveIsHappened)
                    {
                        setNormalColors();
                        leaveIsHappened = false;
                    }
                    timerFade.Stop();
                    index = 0;
                    normalToDisabled = false;
                    count = 10;
                    //// If mouse clicked while operation
                    //if (isMouseClickedWhileOperation == true)
                    //{
                    //    borderLineColor = mouseDownColors[0];
                    //    borderPointColor = mouseDownColors[1];
                    //    borderPoint2Color = mouseDownColors[2];
                    //    upperGradientColor1 = mouseDownColors[3];
                    //    upperGradientColor2 = mouseDownColors[4];
                    //    lowerGradientColor1 = mouseDownColors[5];
                    //    lowerGradientColor2 = mouseDownColors[6];
                    //}
                }
            }

            if (disabledToNormal)
            {
                if (index <= count)
                {
                    borderLineColor = getReadyColor(disabledColors[0], normalColors[0]);
                    borderPointColor = getReadyColor(disabledColors[1], normalColors[1]);
                    borderPoint2Color = getReadyColor(disabledColors[2], normalColors[2]);
                    upperGradientColor1 = getReadyColor(disabledColors[3], normalColors[3]);
                    upperGradientColor2 = getReadyColor(disabledColors[4], normalColors[4]);
                    lowerGradientColor1 = getReadyColor(disabledColors[5], normalColors[5]);
                    lowerGradientColor2 = getReadyColor(disabledColors[6], normalColors[6]);

                    index++;
                }

                else
                {
                    if (leaveIsHappened)
                    {
                        setNormalColors();
                        leaveIsHappened = false;
                    }
                    timerFade.Stop();
                    index = 0;
                    disabledToNormal = false;
                    count = 10;
                    // If mouse clicked while operation
                    //if (isMouseClickedWhileOperation == true)
                    //{
                    //    borderLineColor = mouseDownColors[0];
                    //    borderPointColor = mouseDownColors[1];
                    //    borderPoint2Color = mouseDownColors[2];
                    //    upperGradientColor1 = mouseDownColors[3];
                    //    upperGradientColor2 = mouseDownColors[4];
                    //    lowerGradientColor1 = mouseDownColors[5];
                    //    lowerGradientColor2 = mouseDownColors[6];
                    //}
                }
            }

            this.Invalidate();
        }

        int count = 10;

        private bool normalToDisabled;
        private bool disabledToNormal;

        private bool areAnimationsEnabled = false;

        //[Browsable(true)]
        //[Description("Are fading animations enabled in button events")]
        //[DefaultValue(false)]
        //public bool EnableAnimation
        //{
        //    get
        //    {
        //        return this.areAnimationsEnabled;
        //    }

        //    set
        //    {
        //        this.areAnimationsEnabled = value;
        //    }
        //}

        private void setNormalColors()
        {
            borderLineColor = normalColors[0];
            borderPointColor = normalColors[1];
            borderPoint2Color = normalColors[2];
            upperGradientColor1 = normalColors[3];
            upperGradientColor2 = normalColors[4];
            lowerGradientColor1 = normalColors[5];
            lowerGradientColor2 = normalColors[6];

            this.Invalidate();
        }

        private Color getReadyColor(Color color1, Color color2)
        {
            //Color colorFirst = Color.FromArgb(color1.R * index, color1.G * index, color1.B * index);
            //Color colorSecond = Color.FromArgb(color2.R * 100 - index, color2.G * 100 - index, color2.B * 100 - index);

            var FCR = color1.R * index;
            var FCG = color1.G * index;
            var FCB = color1.B * index;
            var SCR = color2.R * (count - index);
            var SCG = color2.G * (count - index);
            var SCB = color2.B * (count - index);

            var readyR = (FCR + SCR) / count;
            var readyG = (FCG + SCG) / count;
            var readyB = (FCB + SCB) / count;

            //if (readyR > 255)
            //{
            //    readyR = 255;
            //}

            //if (readyR < 0)
            //{
            //    readyR = 0;
            //}

            //if (readyG > 255)
            //{
            //    readyG = 255;
            //}

            //if (readyG < 0)
            //{
            //    readyG = 0;
            //}

            //if (readyB > 255)
            //{
            //    readyB = 255;
            //}

            //if (readyB < 0)
            //{
            //    readyB = 0;
            //}

            var readyColor = Color.FromArgb(readyR, readyG, readyB);

            return readyColor;
        }

        Bitmap IconDisabled;

        private void CloudButton_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled == true)
            {
                //borderLineColor = disabledColors[0];
                //borderPointColor = disabledColors[1];
                //borderPoint2Color = disabledColors[2];
                //upperGradientColor1 = disabledColors[3];
                //upperGradientColor2 = disabledColors[4];
                //lowerGradientColor1 = disabledColors[5];
                //lowerGradientColor2 = disabledColors[6];

                //index = 0;
                count = 30;
                normalToDisabled = true;
                timerFade.Start();
            }

            else
            {
                count = 30;
                disabledToNormal = true;
                timerFade.Start();
                //borderLineColor = normalColors[0];
                //borderPointColor = normalColors[1];
                //borderPoint2Color = normalColors[2];
                //upperGradientColor1 = normalColors[3];
                //upperGradientColor2 = normalColors[4];
                //lowerGradientColor1 = normalColors[5];
                //lowerGradientColor2 = normalColors[6];
            }
        }

        private void generateDisabledIcon()
        {
            try
            {
                if (icon != null)
                {
                    IconDisabled = new Bitmap(icon.Width, icon.Height);
                    var graphics = Graphics.FromImage(IconDisabled);

                    for (var i = 0; i < icon.Width; i++)
                    {
                        for (var j = 0; j < icon.Height; j++)
                        {
                            if (icon.GetPixel(i, j).A == 0)
                            {
                                var pointpen = new Pen(Color.FromArgb(0, 255, 255, 255));

                                graphics.DrawEllipse(pointpen, i, j, 1, 1);
                            }
                            else
                            {
                                var rgb = (icon.GetPixel(i, j).R + icon.GetPixel(i, j).G + icon.GetPixel(i, j).B) / 3;
                                int a = icon.GetPixel(i, j).A;
                                //Pen pointpen = new Pen(Color.FromArgb(rgb, rgb, rgb));
                                //pointpen.Width = 1;

                                //graphics.DrawEllipse(pointpen, i, j, 1, 1);
                                IconDisabled.SetPixel(i, j, Color.FromArgb(a, rgb, rgb, rgb));
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        // Following code generates transparent image
        private static Bitmap MakeTransparentImage(Image image, float alpha)
        {

            if (image == null)

                throw new ArgumentNullException(nameof(image));

            if (alpha < 0.0f || alpha > 1.0f)

                throw new ArgumentOutOfRangeException(nameof(alpha), "Must be between 0.0 and 1.0.");



            var colorMatrix = new ColorMatrix();

            colorMatrix.Matrix33 = 1.0f - alpha;

            var bmp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);

            using (var attrs = new ImageAttributes())
            {

                attrs.SetColorMatrix(colorMatrix);

                using (var g = Graphics.FromImage(bmp))
                {

                    g.DrawImage(image, new Rectangle(Point.Empty, new Size(image.Width, image.Height)), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attrs);

                }

            }

            return bmp;

        }

        bool leaveIsHappened;

        private void CloudButton_Leave(object sender, EventArgs e)
        {
            leaveIsHappened = true;

            borderLineColor = normalColors[0];
            borderPointColor = normalColors[1];
            borderPoint2Color = normalColors[2];
            upperGradientColor1 = normalColors[3];
            upperGradientColor2 = normalColors[4];
            lowerGradientColor1 = normalColors[5];
            lowerGradientColor2 = normalColors[6];
            //isButtonSelected = false;

            this.Invalidate();
        }
    }
}