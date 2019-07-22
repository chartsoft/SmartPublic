using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace SmartSolution.Utilities.Win.Controls
{
    /// <summary>
    /// 
    /// </summary>
    partial class ColumnShowUc
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(ColumnShowUc));
            this.cmdHideAll = new DevExpress.XtraEditors.SimpleButton();
            this.cmdShowAll = new DevExpress.XtraEditors.SimpleButton();
            this.ctrButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.ctrButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ctrGroupShow = new DevExpress.XtraEditors.GroupControl();
            this.ctrListShow = new DevExpress.XtraEditors.ImageListBoxControl();
            this.ctrImageList = new DevExpress.Utils.ImageCollection();
            this.ctrPanelBottom = new System.Windows.Forms.FlowLayoutPanel();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.ctrListHide = new DevExpress.XtraEditors.ImageListBoxControl();
            this.ctrBar = new DevExpress.XtraBars.BarManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.ctrPopupItemUp = new DevExpress.XtraBars.BarButtonItem();
            this.ctrPopupItemDown = new DevExpress.XtraBars.BarButtonItem();
            this.ctrPopupHide = new DevExpress.XtraBars.BarButtonItem();
            this.ctrPopupItemShow = new DevExpress.XtraBars.BarButtonItem();
            this.ctrPopup = new DevExpress.XtraBars.PopupMenu();
            this.ctrPopup2 = new DevExpress.XtraBars.PopupMenu();
            this.cmdReset = new DevExpress.XtraEditors.SimpleButton();
            this.ctrPanelBottomOuter = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ctrGroupShow)).BeginInit();
            this.ctrGroupShow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrListShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrImageList)).BeginInit();
            this.ctrPanelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrListHide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrPopup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrPopup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrPanelBottomOuter)).BeginInit();
            this.ctrPanelBottomOuter.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdHideAll
            // 
            this.cmdHideAll.Location = new System.Drawing.Point(12, 8);
            this.cmdHideAll.Name = "cmdHideAll";
            this.cmdHideAll.Size = new System.Drawing.Size(73, 23);
            this.cmdHideAll.TabIndex = 6;
            this.cmdHideAll.Text = "隐藏全部》";
            this.cmdHideAll.Visible = false;
            this.cmdHideAll.Click += new System.EventHandler(this.cmdHideAll_Click);
            // 
            // cmdShowAll
            // 
            this.cmdShowAll.Location = new System.Drawing.Point(91, 8);
            this.cmdShowAll.Name = "cmdShowAll";
            this.cmdShowAll.Size = new System.Drawing.Size(73, 23);
            this.cmdShowAll.TabIndex = 9;
            this.cmdShowAll.Text = "《显示全部";
            this.cmdShowAll.Visible = false;
            this.cmdShowAll.Click += new System.EventHandler(this.cmdShowAll_Click);
            // 
            // ctrButtonOK
            // 
            this.ctrButtonOK.Location = new System.Drawing.Point(170, 8);
            this.ctrButtonOK.Name = "ctrButtonOK";
            this.ctrButtonOK.Size = new System.Drawing.Size(50, 23);
            this.ctrButtonOK.TabIndex = 1;
            this.ctrButtonOK.Text = "确定";
            this.ctrButtonOK.Click += new System.EventHandler(this.ctrButtonOK_Click);
            // 
            // ctrButtonCancel
            // 
            this.ctrButtonCancel.Location = new System.Drawing.Point(226, 8);
            this.ctrButtonCancel.Name = "ctrButtonCancel";
            this.ctrButtonCancel.Size = new System.Drawing.Size(50, 23);
            this.ctrButtonCancel.TabIndex = 2;
            this.ctrButtonCancel.Text = "取消";
            this.ctrButtonCancel.Click += new System.EventHandler(this.ctrButtonCancel_Click);
            // 
            // ctrGroupShow
            // 
            this.ctrGroupShow.Controls.Add(this.ctrListShow);
            this.ctrGroupShow.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctrGroupShow.Location = new System.Drawing.Point(5, 5);
            this.ctrGroupShow.Name = "ctrGroupShow";
            this.ctrGroupShow.Size = new System.Drawing.Size(200, 323);
            this.ctrGroupShow.TabIndex = 10;
            this.ctrGroupShow.Text = "显示列(双击隐藏，右键排序)";
            // 
            // ctrListShow
            // 
            this.ctrListShow.AllowDrop = true;
            this.ctrListShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrListShow.ImageList = this.ctrImageList;
            this.ctrListShow.Location = new System.Drawing.Point(2, 23);
            this.ctrListShow.Name = "ctrListShow";
            this.ctrListShow.Size = new System.Drawing.Size(196, 298);
            this.ctrListShow.TabIndex = 13;
            this.ctrListShow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctrListShow_MouseDown);
            // 
            // ctrImageList
            // 
            this.ctrImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ctrImageList.ImageStream")));
            this.ctrImageList.Images.SetKeyName(0, "show.png");
            this.ctrImageList.Images.SetKeyName(1, "control_stop.png");
            // 
            // ctrPanelBottom
            // 
            this.ctrPanelBottom.AutoScroll = true;
            this.ctrPanelBottom.Controls.Add(this.ctrButtonCancel);
            this.ctrPanelBottom.Controls.Add(this.ctrButtonOK);
            this.ctrPanelBottom.Controls.Add(this.cmdShowAll);
            this.ctrPanelBottom.Controls.Add(this.cmdHideAll);
            this.ctrPanelBottom.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctrPanelBottom.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.ctrPanelBottom.Location = new System.Drawing.Point(116, 0);
            this.ctrPanelBottom.Name = "ctrPanelBottom";
            this.ctrPanelBottom.Padding = new System.Windows.Forms.Padding(5);
            this.ctrPanelBottom.Size = new System.Drawing.Size(289, 42);
            this.ctrPanelBottom.TabIndex = 11;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.ctrListHide);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl1.Location = new System.Drawing.Point(210, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(200, 323);
            this.groupControl1.TabIndex = 12;
            this.groupControl1.Text = "隐藏列(双击显示)";
            // 
            // ctrListHide
            // 
            this.ctrListHide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrListHide.ImageList = this.ctrImageList;
            this.ctrListHide.Location = new System.Drawing.Point(2, 23);
            this.ctrListHide.Name = "ctrListHide";
            this.ctrListHide.Size = new System.Drawing.Size(196, 298);
            this.ctrListHide.TabIndex = 14;
            this.ctrListHide.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctrListHide_MouseDown);
            // 
            // ctrBar
            // 
            this.ctrBar.DockControls.Add(this.barDockControlTop);
            this.ctrBar.DockControls.Add(this.barDockControlBottom);
            this.ctrBar.DockControls.Add(this.barDockControlLeft);
            this.ctrBar.DockControls.Add(this.barDockControlRight);
            this.ctrBar.Form = this;
            this.ctrBar.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ctrPopupItemUp,
            this.ctrPopupItemDown,
            this.ctrPopupHide,
            this.ctrPopupItemShow});
            this.ctrBar.MaxItemId = 4;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(5, 5);
            this.barDockControlTop.Size = new System.Drawing.Size(405, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(5, 370);
            this.barDockControlBottom.Size = new System.Drawing.Size(405, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(5, 5);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 365);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(410, 5);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 365);
            // 
            // ctrPopupItemUp
            // 
            this.ctrPopupItemUp.Caption = "上移";
            this.ctrPopupItemUp.Id = 0;
            this.ctrPopupItemUp.Name = "ctrPopupItemUp";
            this.ctrPopupItemUp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ctrPopupItemUp_ItemClick);
            // 
            // ctrPopupItemDown
            // 
            this.ctrPopupItemDown.Caption = "下移";
            this.ctrPopupItemDown.Id = 1;
            this.ctrPopupItemDown.Name = "ctrPopupItemDown";
            this.ctrPopupItemDown.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ctrPopupItemDown_ItemClick);
            // 
            // ctrPopupHide
            // 
            this.ctrPopupHide.Caption = "隐藏";
            this.ctrPopupHide.Id = 2;
            this.ctrPopupHide.Name = "ctrPopupHide";
            this.ctrPopupHide.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ctrPopupHide_ItemClick);
            // 
            // ctrPopupItemShow
            // 
            this.ctrPopupItemShow.Caption = "显示";
            this.ctrPopupItemShow.Id = 3;
            this.ctrPopupItemShow.Name = "ctrPopupItemShow";
            this.ctrPopupItemShow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ctrPopupItemShow_ItemClick);
            // 
            // ctrPopup
            // 
            this.ctrPopup.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.ctrPopupItemUp),
            new DevExpress.XtraBars.LinkPersistInfo(this.ctrPopupItemDown),
            new DevExpress.XtraBars.LinkPersistInfo(this.ctrPopupHide)});
            this.ctrPopup.Manager = this.ctrBar;
            this.ctrPopup.Name = "ctrPopup";
            // 
            // ctrPopup2
            // 
            this.ctrPopup2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.ctrPopupItemShow)});
            this.ctrPopup2.Manager = this.ctrBar;
            this.ctrPopup2.Name = "ctrPopup2";
            // 
            // cmdReset
            // 
            this.cmdReset.Location = new System.Drawing.Point(18, 10);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(50, 23);
            this.cmdReset.TabIndex = 10;
            this.cmdReset.Text = "重置";
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // ctrPanelBottomOuter
            // 
            this.ctrPanelBottomOuter.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ctrPanelBottomOuter.Appearance.Options.UseBackColor = true;
            this.ctrPanelBottomOuter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ctrPanelBottomOuter.Controls.Add(this.ctrPanelBottom);
            this.ctrPanelBottomOuter.Controls.Add(this.cmdReset);
            this.ctrPanelBottomOuter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrPanelBottomOuter.Location = new System.Drawing.Point(5, 328);
            this.ctrPanelBottomOuter.Name = "ctrPanelBottomOuter";
            this.ctrPanelBottomOuter.Size = new System.Drawing.Size(405, 42);
            this.ctrPanelBottomOuter.TabIndex = 17;
            // 
            // ColumnShowUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.ctrGroupShow);
            this.Controls.Add(this.ctrPanelBottomOuter);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximumSize = new System.Drawing.Size(415, 375);
            this.MinimumSize = new System.Drawing.Size(415, 375);
            this.Name = "ColumnShowUc";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(415, 375);
            this.Load += new System.EventHandler(this.ColumnShowUC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ctrGroupShow)).EndInit();
            this.ctrGroupShow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrListShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrImageList)).EndInit();
            this.ctrPanelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrListHide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrPopup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrPopup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctrPanelBottomOuter)).EndInit();
            this.ctrPanelBottomOuter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SimpleButton cmdShowAll;
        private SimpleButton cmdHideAll;
        private SimpleButton ctrButtonOK;
        private SimpleButton ctrButtonCancel;
        private GroupControl ctrGroupShow;
        private FlowLayoutPanel ctrPanelBottom;
        private GroupControl groupControl1;
        private ImageListBoxControl ctrListShow;
        private ImageListBoxControl ctrListHide;
        private ImageCollection ctrImageList;
        private BarManager ctrBar;
        private BarDockControl barDockControlTop;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarButtonItem ctrPopupItemUp;
        private BarButtonItem ctrPopupItemDown;
        private BarButtonItem ctrPopupHide;
        private PopupMenu ctrPopup;
        private BarButtonItem ctrPopupItemShow;
        private PopupMenu ctrPopup2;
        private PanelControl ctrPanelBottomOuter;
        private SimpleButton cmdReset;

    }
}