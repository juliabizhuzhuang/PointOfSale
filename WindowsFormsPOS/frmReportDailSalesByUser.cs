using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace WindowsFormsPOS
{
    public partial class frmReportDailSalesByUser : Form
    {
        DateTime ReportDate;
        public frmReportDailSalesByUser(DateTime reportDate)
        {

            InitializeComponent();

            ReportDate = reportDate;
        }

        private void frmReportDailSalesByUser_Load(object sender, EventArgs e)
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
                    SQLConn.sqL = "SELECT CONCAT(Lastname, ', ', Firstname, ' ', MI) as StaffName, ProductCode, P.Description, REPLACE(TD.TDate, '-', '/') as TDate, TD.TTime,TD.ItemPrice, SUM(TD.Quantity) as totalQuantity, (TD.ItemPrice * SUM(TD.Quantity)) as TotalPrice  FROM Product as P, TransactionDetails as T, TransactionDetails as TD, Staff as St WHERE P.ProductNo = TD.ProductNo AND TD.InvoiceNo = T.InvoiceNo AND St.StaffID = T.StaffID AND  TD.TDate = '" + ReportDate.ToString("MM/dd/yyyy") + "' AND T.Status != 1 GROUP BY  St.StaffID,St.MI,St.firstname,St.lastname,P.ProductCode,P.Description, TD.TDate, TD.TTime, TD.ItemPrice ORDER By TD.TDate";
                }
                else
                {
                    SQLConn.sqL = "SELECT CONCAT(Lastname, ', ', Firstname, ' ', MI) as StaffName, ProductCode, P.Description, REPLACE(TD.TDate, '-', '/') as TDate, TD.TTime,TD.ItemPrice, SUM(TD.Quantity) as totalQuantity, (TD.ItemPrice * SUM(TD.Quantity)) as TotalPrice  FROM Product as P, TransactionDetails as T, TransactionDetails as TD, Staff as St WHERE P.ProductNo = TD.ProductNo AND TD.InvoiceNo = T.InvoiceNo AND St.StaffID = T.StaffID AND  TD.TDate = '" + ReportDate.ToString("MM/dd/yyyy") + "' GROUP BY  St.StaffID,St.firstname,St.MI,St.lastname, P.ProductCode,P.Description,TD.TTime, TD.TDate , TD.ItemPrice ORDER By TD.TDate";
                }

                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.da = new System.Data.SqlClient.SqlDataAdapter(SQLConn.cmd);
                this.dsReportC.DailySalesByStaff.Clear();
                SQLConn.da.Fill(this.dsReportC.DailySalesByStaff);

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
