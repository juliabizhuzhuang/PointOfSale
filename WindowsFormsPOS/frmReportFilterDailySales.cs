using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace WindowsFormsPOS
{
    public partial class frmReportFilterDailySales : Form
    {
        public frmReportFilterDailySales()
        {
            InitializeComponent();
        }

        private void rbUser_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbInvoice_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((rbUser.Checked == false) && (rbInvoice.Checked == false))
            {
                Interaction.MsgBox("Please select report by User or Invoice No", MsgBoxStyle.Information, "Select Report");
                return;
            }


            if (rbUser.Checked == true)
            {
                frmReportDailSalesByUser R = new frmReportDailSalesByUser(DateTimePicker1.Value);
                R.Show();
            }
            else
            {
                frmReportDailSalesByUser R = new frmReportDailSalesByUser(DateTimePicker1.Value);
                R.Show();

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
