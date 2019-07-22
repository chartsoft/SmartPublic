using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Smart.Win.Controls
{
    partial class SplashForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm));
            this._lblMsg = new DevExpress.XtraEditors.LabelControl();
            this._picBG = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._picBG)).BeginInit();
            this.SuspendLayout();
            // 
            // _lblMsg
            // 
            this._lblMsg.Appearance.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblMsg.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._lblMsg.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this._lblMsg.Location = new System.Drawing.Point(16, 53);
            this._lblMsg.Name = "_lblMsg";
            this._lblMsg.Size = new System.Drawing.Size(205, 42);
            this._lblMsg.TabIndex = 2;
            this._lblMsg.Text = "系统载入中…";
            // 
            // _picBG
            // 
            //this._picBG.SetBackColor(System.Drawing.Color.Transparent);
            this._picBG.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_picBG.BackgroundImage")));
            this._picBG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this._picBG.Dock = System.Windows.Forms.DockStyle.Fill;
            this._picBG.Location = new System.Drawing.Point(0, 0);
            this._picBG.Name = "_picBG";
            this._picBG.Size = new System.Drawing.Size(237, 151);
            this._picBG.TabIndex = 0;
            this._picBG.TabStop = false;
            // 
            // SplashForm
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 151);
            this.Controls.Add(this._lblMsg);
            this.Controls.Add(this._picBG);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashForm";
            this.Load += new System.EventHandler(this.SplashForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._picBG)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox _picBG;
        private LabelControl _lblMsg;
    }
}