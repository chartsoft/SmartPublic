using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Smart.Win.Controls
{
    partial class WaitForm
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
            this._groupLoad = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.pictureMain = new DevExpress.XtraEditors.PictureEdit();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._groupLoad)).BeginInit();
            this._groupLoad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _groupLoad
            // 
            this._groupLoad.Controls.Add(this.simpleButton1);
            this._groupLoad.Controls.Add(this.pictureMain);
            this._groupLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupLoad.Location = new System.Drawing.Point(0, 0);
            this._groupLoad.Name = "_groupLoad";
            this._groupLoad.Size = new System.Drawing.Size(204, 80);
            this._groupLoad.TabIndex = 0;
            this._groupLoad.Text = "正在处理请求，请稍候...";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.Location = new System.Drawing.Point(242, 3);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(18, 17);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "×";
            this.simpleButton1.Visible = false;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // pictureMain
            // 
            this.pictureMain.EditValue = global::Smart.Win.Properties.Resources.Executing;
            this.pictureMain.Location = new System.Drawing.Point(19, 42);
            this.pictureMain.Name = "pictureMain";
            this.pictureMain.Properties.ReadOnly = true;
            this.pictureMain.Properties.ShowMenu = false;
            this.pictureMain.Size = new System.Drawing.Size(166, 13);
            this.pictureMain.TabIndex = 0;
            // 
            // pictureBox1
            // 
            //this.pictureBox1.SetBackColor(System.Drawing.Color.Transparent);
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Smart.Win.Properties.Resources.Overlay;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(204, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // WaitForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.Red;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 80);
            this.Controls.Add(this._groupLoad);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WaitForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormExecuting";
            this.TransparencyKey = System.Drawing.Color.Red;
            this.Load += new System.EventHandler(this.WaitForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._groupLoad)).EndInit();
            this._groupLoad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureMain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupControl _groupLoad;
        private PictureEdit pictureMain;
        private SimpleButton simpleButton1;
        private PictureBox pictureBox1;
    }
}

