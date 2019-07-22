using System.ComponentModel;
using DevExpress.XtraEditors;

namespace Smart.Win.Controls
{
    /// <summary>
    /// 
    /// </summary>
    partial class WaitUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._lblMsg = new DevExpress.XtraEditors.LabelControl();
            this._loadingCircle = new LoadingCircle();
            this._panelContent = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this._panelContent)).BeginInit();
            this._panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lblMsg
            // 
            this._lblMsg.Location = new System.Drawing.Point(73, 31);
            this._lblMsg.Name = "_lblMsg";
            this._lblMsg.Size = new System.Drawing.Size(96, 14);
            this._lblMsg.TabIndex = 1;
            this._lblMsg.Text = "处理中，请稍候...";
            // 
            // _loadingCircle
            // 
            this._loadingCircle.Active = true;
            //this._loadingCircle.SetBackColor(System.Drawing.Color.Transparent);
            this._loadingCircle.Color = System.Drawing.Color.DarkGray;
            this._loadingCircle.InnerCircleRadius = 5;
            this._loadingCircle.Location = new System.Drawing.Point(5, 5);
            this._loadingCircle.Name = "_loadingCircle";
            this._loadingCircle.NumberSpoke = 12;
            this._loadingCircle.OuterCircleRadius = 11;
            this._loadingCircle.RotationSpeed = 100;
            this._loadingCircle.Size = new System.Drawing.Size(64, 64);
            this._loadingCircle.SpokeThickness = 2;
            this._loadingCircle.StylePreset = LoadingCircle.StylePresets.MacOSX;
            this._loadingCircle.TabIndex = 2;
            this._loadingCircle.Text = "loadingCircle1";
            // 
            // _panelContent
            // 
            this._panelContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this._panelContent.Controls.Add(this._loadingCircle);
            this._panelContent.Controls.Add(this._lblMsg);
            this._panelContent.Location = new System.Drawing.Point(20, 21);
            this._panelContent.Name = "_panelContent";
            this._panelContent.Size = new System.Drawing.Size(233, 73);
            this._panelContent.TabIndex = 3;
            // 
            // WaitUC
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panelContent);
            this.Name = "WaitUC";
            this.Size = new System.Drawing.Size(336, 205);
            this.Load += new System.EventHandler(this.WaitUC_Load);
            this.SizeChanged += new System.EventHandler(this.WaitUC_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this._panelContent)).EndInit();
            this._panelContent.ResumeLayout(false);
            this._panelContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private LabelControl _lblMsg;
        private LoadingCircle _loadingCircle;
        private PanelControl _panelContent;
    }
}
