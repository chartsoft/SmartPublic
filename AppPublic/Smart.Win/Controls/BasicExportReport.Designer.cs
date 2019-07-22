using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Smart.Win.Controls
{
    partial class BasicExportReport
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabelTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabelHead = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabelFoot = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelTitle});
            this.Detail.HeightF = 1184F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabelTitle
            // 
            this.xrLabelTitle.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrLabelTitle.LocationFloat = new DevExpress.Utils.PointFloat(0.999999F, 0.9999911F);
            this.xrLabelTitle.Name = "xrLabelTitle";
            this.xrLabelTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelTitle.SizeF = new System.Drawing.SizeF(785.9999F, 33F);
            this.xrLabelTitle.StylePriority.UseBorders = false;
            this.xrLabelTitle.StylePriority.UseTextAlignment = false;
            this.xrLabelTitle.Text = "地区列表";
            this.xrLabelTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelHead});
            this.TopMargin.HeightF = 30F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.TopMargin.Visible = false;
            // 
            // xrLabelHead
            // 
            this.xrLabelHead.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabelHead.Name = "xrLabelHead";
            this.xrLabelHead.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelHead.SizeF = new System.Drawing.SizeF(786.9999F, 22.99999F);
            this.xrLabelHead.StylePriority.UseTextAlignment = false;
            this.xrLabelHead.Text = "车辆管理信息系统";
            this.xrLabelHead.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelFoot});
            this.BottomMargin.HeightF = 30F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.BottomMargin.Visible = false;
            // 
            // xrLabelFoot
            // 
            this.xrLabelFoot.LocationFloat = new DevExpress.Utils.PointFloat(1.000055F, 0F);
            this.xrLabelFoot.Name = "xrLabelFoot";
            this.xrLabelFoot.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelFoot.SizeF = new System.Drawing.SizeF(785.9999F, 22.99995F);
            this.xrLabelFoot.StylePriority.UseTextAlignment = false;
            this.xrLabelFoot.Text = "车辆管理信息系统";
            this.xrLabelFoot.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // BasicExportReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.DisplayName = "导出地区列表";
            this.Margins = new System.Drawing.Printing.Margins(20, 20, 30, 30);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DetailBand Detail;
        private TopMarginBand TopMargin;
        private BottomMarginBand BottomMargin;
        private XRLabel xrLabelTitle;
        private XRLabel xrLabelFoot;
        private XRLabel xrLabelHead;
    }
}
