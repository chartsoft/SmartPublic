using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace Smart.Win.Controls
{

    /// <summary>
    /// 基本导出报表
    /// </summary>
    public partial class BasicExportReport : XtraReport
    {
        /// <summary>
        /// 
        /// </summary>
        public BasicExportReport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化导出报表
        /// </summary>
        /// <param name="model"></param>
        public void InitReport<T>(BasicExportModel<T> model) where T:class
        {
            if (model == null) return;
            GenerateReport(model);
        }

        private void GenerateReport<T>(BasicExportModel<T> model) where T : class
        {
            ReportBasicSetting(model);
            xrLabelTitle.Text = model.TextTitle;
            xrLabelHead.Text = model.TextHead;
            xrLabelFoot.Text = model.TextFoot;
            BottomMargin.Visible = !string.IsNullOrEmpty(model.TextFoot);
            TopMargin.Visible = !string.IsNullOrEmpty(model.TextHead);
            xrLabelTitle.Visible = !string.IsNullOrEmpty(model.TextTitle);
            var xrTable = GenerateTable();
            Detail.Controls.Add(xrTable);
            ((ISupportInitialize)(xrTable)).BeginInit();
            ((ISupportInitialize)(this)).BeginInit();

            if (model.DataHead != null && model.DataHead.Count > 0)
            {
                var colIndex = 1;
                var rowIndex = 1;
                var xrRowHead = GenerateTableRow("xrTableRowHead");
                var headEor = model.DataHead.GetEnumerator();
                while (headEor.MoveNext())
                {
                    xrRowHead.Cells.Add(GenerateTableCellHead(colIndex, headEor.Current.Value));
                    colIndex++;
                }
                xrTable.Rows.Add(xrRowHead);
                if (model.Data != null && model.Data.Count > 0)
                {
                    foreach (var data in model.Data)
                    {
                        var xrRowBody = GenerateTableRow("xrTableRowBody" + rowIndex++);
                       // PropertyInfo[] propertys = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.Public);
                        var bodyEor = model.DataHead.GetEnumerator();
                        colIndex = 1;
                        while (bodyEor.MoveNext())
                        {
                            var property = typeof(T).GetProperty(bodyEor.Current.Key);
                            var valueO = property.GetValue(data, null);
                            var valueS=string.Empty;
                            if (valueO != null)
                            {
                                if (valueO is DateTime || valueO is DateTime?)
                                {
                                    valueS = ((DateTime)valueO).ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    valueS = valueO.ToString();
                                }
                            }
                           xrRowBody.Cells.Add(GenerateTableCellBody("xrCellBody" + colIndex++, valueS));
                        }
                        xrTable.Rows.Add(xrRowBody);
                    }
                }

            }
            ((ISupportInitialize)(xrTable)).EndInit();
            ((ISupportInitialize)(this)).EndInit();
        }

        private static XRTable GenerateTable()
        {
            var xrTable = new XRTable();
            xrTable.Borders = ((BorderSide.Left | BorderSide.Top)
                               | BorderSide.Right)
                              | BorderSide.Bottom;
            xrTable.LocationFloat = new PointFloat(1.000063F, 34.00002F);
            xrTable.Name = "xrTable";
            xrTable.SizeF = new SizeF(785.9999F, 25F);
            xrTable.StylePriority.UseBorders = false;
            xrTable.StylePriority.UseTextAlignment = false;
            xrTable.TextAlignment = TextAlignment.MiddleCenter;
            return xrTable;
        }

        private static XRTableCell GenerateTableCellBody(string name,string text)
        {
            var xrCellBody = new XRTableCell();
            xrCellBody.Name = name;
            xrCellBody.Text = text;
            xrCellBody.Weight = 1;
            return xrCellBody;
        }

        private static XRTableCell GenerateTableCellHead(int curIndex, string cellText)
        {
            var xrCellHead = new XRTableCell();
            xrCellHead.BackColor = Color.FromArgb(255, 128, 0);
            xrCellHead.ForeColor = Color.White;
            xrCellHead.Name = "xrCellHead" + curIndex;
            xrCellHead.StylePriority.UseBackColor = false;
            xrCellHead.StylePriority.UseForeColor = false;
            xrCellHead.Text = cellText;
            xrCellHead.Weight = 1;
            return xrCellHead;
        }

        private static XRTableRow GenerateTableRow(string rowName)
        {
            var xrRow = new XRTableRow();
            xrRow.Name = rowName;
            xrRow.Weight = 1;
            return xrRow;
        }

        private void ReportBasicSetting<T>(BasicExportModel<T> model) where T : class
        {
            DisplayName = $"导出{model.TextTitle}";
            Margins = new Margins(20, 20, 30, 30);
            PageHeight = 1169;
            PageWidth = 827;
            PaperKind = System.Drawing.Printing.PaperKind.A4;
        }

    }
}
