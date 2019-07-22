using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Smart.Win.Controls
{
    partial class PagerUC
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
            this.lblSarca = new DevExpress.XtraEditors.LabelControl();
            this.lblScaba = new DevExpress.XtraEditors.LabelControl();
            this._lblTotalPage = new DevExpress.XtraEditors.LabelControl();
            this._btnLastPage = new System.Windows.Forms.LinkLabel();
            this._btnNextPage = new System.Windows.Forms.LinkLabel();
            this._btnPrevPage = new System.Windows.Forms.LinkLabel();
            this._btnFirstPage = new System.Windows.Forms.LinkLabel();
            this._comboCurrentPage = new DevExpress.XtraEditors.ComboBoxEdit();
            this._panelPageControl = new DevExpress.XtraEditors.PanelControl();
            this._panelPageInfo = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this._lblTotalNum = new DevExpress.XtraEditors.LabelControl();
            this._comboPageSize = new DevExpress.XtraEditors.ComboBoxEdit();
            this._panelPager = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this._comboCurrentPage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelPageControl)).BeginInit();
            this._panelPageControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelPageInfo)).BeginInit();
            this._panelPageInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._comboPageSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelPager)).BeginInit();
            this._panelPager.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSarca
            // 
            this.lblSarca.Location = new System.Drawing.Point(105, 6);
            this.lblSarca.Name = "lblSarca";
            this.lblSarca.Size = new System.Drawing.Size(12, 14);
            this.lblSarca.TabIndex = 5;
            this.lblSarca.Text = "第";
            // 
            // lblScaba
            // 
            this.lblScaba.Location = new System.Drawing.Point(175, 6);
            this.lblScaba.Name = "lblScaba";
            this.lblScaba.Size = new System.Drawing.Size(17, 14);
            this.lblScaba.TabIndex = 6;
            this.lblScaba.Text = "页/";
            // 
            // _lblTotalPage
            // 
            this._lblTotalPage.Location = new System.Drawing.Point(198, 6);
            this._lblTotalPage.Name = "_lblTotalPage";
            this._lblTotalPage.Size = new System.Drawing.Size(19, 14);
            this._lblTotalPage.TabIndex = 8;
            this._lblTotalPage.Text = "0页";
            // 
            // _btnLastPage
            // 
            this._btnLastPage.AutoSize = true;
            this._btnLastPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnLastPage.Enabled = false;
            this._btnLastPage.Location = new System.Drawing.Point(284, 6);
            this._btnLastPage.Name = "_btnLastPage";
            this._btnLastPage.Size = new System.Drawing.Size(31, 14);
            this._btnLastPage.TabIndex = 4;
            this._btnLastPage.TabStop = true;
            this._btnLastPage.Text = "尾页";
            // 
            // _btnNextPage
            // 
            this._btnNextPage.AutoSize = true;
            this._btnNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnNextPage.Enabled = false;
            this._btnNextPage.Location = new System.Drawing.Point(235, 6);
            this._btnNextPage.Name = "_btnNextPage";
            this._btnNextPage.Size = new System.Drawing.Size(43, 14);
            this._btnNextPage.TabIndex = 3;
            this._btnNextPage.TabStop = true;
            this._btnNextPage.Text = "下一页";
            // 
            // _btnPrevPage
            // 
            this._btnPrevPage.AutoSize = true;
            this._btnPrevPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnPrevPage.Enabled = false;
            this._btnPrevPage.Location = new System.Drawing.Point(56, 6);
            this._btnPrevPage.Name = "_btnPrevPage";
            this._btnPrevPage.Size = new System.Drawing.Size(43, 14);
            this._btnPrevPage.TabIndex = 2;
            this._btnPrevPage.TabStop = true;
            this._btnPrevPage.Text = "上一页";
            // 
            // _btnFirstPage
            // 
            this._btnFirstPage.AutoSize = true;
            this._btnFirstPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnFirstPage.Enabled = false;
            this._btnFirstPage.Location = new System.Drawing.Point(19, 6);
            this._btnFirstPage.Name = "_btnFirstPage";
            this._btnFirstPage.Size = new System.Drawing.Size(31, 14);
            this._btnFirstPage.TabIndex = 1;
            this._btnFirstPage.TabStop = true;
            this._btnFirstPage.Text = "首页";
            // 
            // _comboCurrentPage
            // 
            this._comboCurrentPage.Location = new System.Drawing.Point(125, 5);
            this._comboCurrentPage.Name = "_comboCurrentPage";
            this._comboCurrentPage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._comboCurrentPage.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this._comboCurrentPage.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this._comboCurrentPage.Size = new System.Drawing.Size(46, 20);
            this._comboCurrentPage.TabIndex = 7;
            // 
            // _panelPageControl
            // 
            this._panelPageControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this._panelPageControl.Controls.Add(this.lblScaba);
            this._panelPageControl.Controls.Add(this._lblTotalPage);
            this._panelPageControl.Controls.Add(this._comboCurrentPage);
            this._panelPageControl.Controls.Add(this._btnFirstPage);
            this._panelPageControl.Controls.Add(this.lblSarca);
            this._panelPageControl.Controls.Add(this._btnPrevPage);
            this._panelPageControl.Controls.Add(this._btnLastPage);
            this._panelPageControl.Controls.Add(this._btnNextPage);
            this._panelPageControl.Dock = System.Windows.Forms.DockStyle.Right;
            this._panelPageControl.Location = new System.Drawing.Point(369, 0);
            this._panelPageControl.Name = "_panelPageControl";
            this._panelPageControl.Size = new System.Drawing.Size(321, 30);
            this._panelPageControl.TabIndex = 9;
            // 
            // _panelPageInfo
            // 
            this._panelPageInfo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this._panelPageInfo.Controls.Add(this.labelControl1);
            this._panelPageInfo.Controls.Add(this.labelControl2);
            this._panelPageInfo.Controls.Add(this._lblTotalNum);
            this._panelPageInfo.Controls.Add(this._comboPageSize);
            this._panelPageInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this._panelPageInfo.Location = new System.Drawing.Point(0, 0);
            this._panelPageInfo.Name = "_panelPageInfo";
            this._panelPageInfo.Size = new System.Drawing.Size(192, 30);
            this._panelPageInfo.TabIndex = 10;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 17;
            this.labelControl1.Text = "每页";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(88, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(17, 14);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "条/";
            // 
            // _lblTotalNum
            // 
            this._lblTotalNum.Location = new System.Drawing.Point(107, 8);
            this._lblTotalNum.Name = "_lblTotalNum";
            this._lblTotalNum.Size = new System.Drawing.Size(31, 14);
            this._lblTotalNum.TabIndex = 16;
            this._lblTotalNum.Text = "共0条";
            // 
            // _comboPageSize
            // 
            this._comboPageSize.EditValue = "";
            this._comboPageSize.Location = new System.Drawing.Point(37, 5);
            this._comboPageSize.Name = "_comboPageSize";
            this._comboPageSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._comboPageSize.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this._comboPageSize.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this._comboPageSize.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this._comboPageSize.Size = new System.Drawing.Size(48, 20);
            this._comboPageSize.TabIndex = 15;
            // 
            // _panelPager
            // 
            this._panelPager.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this._panelPager.Controls.Add(this._panelPageControl);
            this._panelPager.Controls.Add(this._panelPageInfo);
            this._panelPager.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelPager.Location = new System.Drawing.Point(0, 0);
            this._panelPager.Name = "_panelPager";
            this._panelPager.Size = new System.Drawing.Size(690, 30);
            this._panelPager.TabIndex = 11;
            // 
            // PagerUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panelPager);
            this.Name = "PagerUC";
            this.Size = new System.Drawing.Size(690, 30);
            this.Load += new System.EventHandler(this.DataPagerUC_Load);
            ((System.ComponentModel.ISupportInitialize)(this._comboCurrentPage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelPageControl)).EndInit();
            this._panelPageControl.ResumeLayout(false);
            this._panelPageControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelPageInfo)).EndInit();
            this._panelPageInfo.ResumeLayout(false);
            this._panelPageInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._comboPageSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelPager)).EndInit();
            this._panelPager.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LinkLabel _btnFirstPage;
        private LinkLabel _btnPrevPage;
        private LinkLabel _btnNextPage;
        private LinkLabel _btnLastPage;
        private LabelControl lblSarca;
        private LabelControl lblScaba;
        private LabelControl _lblTotalPage;
        private ComboBoxEdit _comboCurrentPage;
        private PanelControl _panelPageControl;
        private PanelControl _panelPageInfo;
        private LabelControl labelControl1;
        private LabelControl labelControl2;
        private LabelControl _lblTotalNum;
        private ComboBoxEdit _comboPageSize;
        private PanelControl _panelPager;
    }
}
