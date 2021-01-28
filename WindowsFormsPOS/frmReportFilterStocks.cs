using System;
using System.Windows.Forms;

namespace WindowsFormsPOS
{
    public partial class frmReportFilterStocks : Form
    {
        public frmReportFilterStocks()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((rbStocksIn.Checked == false) && (rbStocksOut.Checked == false))
            {
                MessageBox.Show("Please select report by Stocks-In or Stocks-Out", "Select Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (rbStocksIn.Checked == true)
            {
                frmReportStocksIn rs = new frmReportStocksIn(dtpStartDate.Value, dtpEndDate.Value);
                rs.Show();
            }

            if (rbStocksOut.Checked == true)
            {
                frmReportStocksOut rs = new frmReportStocksOut(dtpStartDate.Value, dtpEndDate.Value);
                rs.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbStocksIn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStocksIn.Checked == true)
            {
                rbStocksOut.Checked = false;
            }
        }

        private void rbStocksOut_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStocksOut.Checked == true)
            {
                rbStocksIn.Checked = false;
            }
        }
    }
}
