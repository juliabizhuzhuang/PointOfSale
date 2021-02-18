using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;


namespace WindowsFormsPOS
{
    public partial class frmReportDailSalesByInvoice : Form
    {
        DateTime ReportDate;
        DateTime StartDate;
        DateTime EndDate;
        public frmReportDailSalesByInvoice(DateTime reportdate)
        {
            InitializeComponent();
            ReportDate = reportdate;
        }

        private void frmReportDailSalesByInvoice_Load(object sender, EventArgs e)
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
                    SQLConn.sqL = "SELECT CONCAT(Lastname, ', ', Firstname, ' ', MI) as StaffName, ProductCode, P.Description, TD.TDate as TDate, TTime,TD.ItemPrice, SUM(TD.Quantity) as totalQuantity, (TD.ItemPrice * SUM(TD.Quantity)) as TotalPrice  FROM Product as P, TransactionDetails as T, TransactionDetails as TD, Staff as St WHERE P.ProductNo = TD.ProductNo AND TD.InvoiceNo = T.InvoiceNo AND St.StaffID = T.StaffID AND  TD.TDate = '" + ReportDate.ToString("MM/dd/yyyy") + "' AND T.Status != 1 GROUP BY  St.StaffID,  St.firstname,St.lastname, ST.MI,P.ProductCode,P.Description, TD.TDate, TD.ItemPrice  ORDER By TD.TDate";
                }
                else
                {
                    SQLConn.sqL = "SELECT CONCAT(Lastname, ', ', Firstname, ' ', MI) as StaffName, ProductCode, P.Description, TD.TDate as TDate, TTime,TD.ItemPrice, SUM(TD.Quantity) as totalQuantity, (TD.ItemPrice * SUM(TD.Quantity)) as TotalPrice  FROM Product as P, TransactionDetails as T, TransactionDetails as TD, Staff as St WHERE P.ProductNo = TD.ProductNo AND TD.InvoiceNo = T.InvoiceNo AND St.StaffID = T.StaffID AND  TDate = '" + ReportDate.ToString("MM/dd/yyyy") + "' GROUP BY  St.StaffID,  St.firstname,St.lastname, ST.MI,P.ProductCode,P.Description, TD.TDate , TD.ItemPrice ORDER By TD.TDate";
                }

                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.da = new System.Data.SqlClient.SqlDataAdapter(SQLConn.cmd);
                this.dsReportC.DailySalesByInvoice.Clear();
                SQLConn.da.Fill(this.dsReportC.DailySalesByInvoice);
                
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

