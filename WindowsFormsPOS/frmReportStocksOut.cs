﻿using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace WindowsFormsPOS
{
    public partial class frmReportStocksOut : Form
    {
        DateTime StartDate;
        DateTime EndDate;

        public frmReportStocksOut(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();

            StartDate = startDate;
            EndDate = endDate;
        }

        private void frmReportStocksOut_Load(object sender, EventArgs e)
        {

            LoadReport();
            this.reportViewer1.RefreshReport();
        }

        private void LoadReport()
        {
            try
            {
                if (InvoiceSetting() == 1)
                {
                    SQLConn.sqL = "SELECT ProductCode, P.Description, TDate as DateOut, SUM(TD.Quantity) as Quantity, TD.ItemPrice as Price, (SUM(TD.Quantity) * TD.ItemPrice) as TotalAmount FROM Product as P, Transactions as T, TransactionDetails as TD WHERE P.ProductNo = TD.ProductNo AND TD.InvoiceNo = T.InvoiceNo AND STR_TO_DATE(REPLACE(TDate, '-', '/'), '%m/%d/%Y') BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.ToString("yyyy-MM-dd") + "' AND T.Status != 1 GROUP BY P.ProductNo, TDate ORDER By TDate, Description";
                }
                else
                {
                    SQLConn.sqL = "SELECT ProductCode, P.Description,TDate as DateOut, SUM(TD.Quantity) as Quantity, TD.ItemPrice as Price, (SUM(TD.Quantity) * TD.ItemPrice) as TotalAmount FROM Product as P, Transactions as T, TransactionDetails as TD WHERE P.ProductNo = TD.ProductNo AND TD.InvoiceNo = T.InvoiceNo AND STR_TO_DATE(REPLACE(TDate, '-', '/'), '%m/%d/%Y') BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.ToString("yyyy-MM-dd") + "' GROUP BY P.ProductNo, TDate ORDER By TDate, Description";
                }

                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.da = new System.Data.SqlClient.SqlDataAdapter(SQLConn.cmd);

               // this.dsReportC.StocksOut.Clear();
              //  SQLConn.da.Fill(this.dsReportC.StocksOut);

                ReportParameter startDate = new ReportParameter("StartDate", StartDate.ToString());
                ReportParameter endDate = new ReportParameter("EndDate", EndDate.ToString());
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { startDate, endDate });

                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomPercent = 90;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;

                this.reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        int InvoiceSetting()
        {
            int ret = 0;
            try
            {
                SQLConn.sqL = "SELECT HInvoice FROM Company";
                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();


                if (SQLConn.dr.Read() == true)
                {
                    ret = Convert.ToInt32(SQLConn.dr["HInvoice"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SQLConn.cmd.Dispose();
                SQLConn.conn.Close();
            }

            return ret;
        }
    }
}
