﻿using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;


namespace WindowsFormsPOS
{
    public partial class frmReportStocksIn : Form
    {
        DateTime StartDate;
        DateTime EndDate;
        public frmReportStocksIn(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            StartDate = startDate;
            EndDate = endDate;
        }

        private void frmReportStocksIn_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                SQLConn.sqL = "SELECT ProductCode, Description, SUM(Quantity) as Quantity, STR_TO_DATE(REPLACE(DateIn, '-', '/'), '%m/%d/%Y') as DateIn FROM Product as P, StockIn as S WHERE S.ProductNo = P.ProductNo AND STR_TO_DATE(REPLACE(DateIn, '-', '/'), '%m/%d/%Y') BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.ToString("yyyy-MM-dd") + "' GROUP BY P.ProductNo, DateIN ORDER BY DateIn, Description";
                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.da = new System.Data.SqlClient.SqlDataAdapter(SQLConn.cmd);

                this.dsReportC.StocksIn.Clear();
                SQLConn.da.Fill(this.dsReportC.StocksIn);
                ReportParameter startDate = new ReportParameter("StartDate", StartDate.ToString());
                ReportParameter endDate = new ReportParameter("EndDate", EndDate.ToString());
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter []{ startDate, endDate });

                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.ZoomPercent = 90;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;

                this.reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.ToString());
            }
        }
    }
}
