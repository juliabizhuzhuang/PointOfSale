using System;
using System.Windows.Forms;

namespace WindowsFormsPOS
{
    public partial class frmMain : Form
    {
        string Username;
        int StaffID;
        public frmMain(string username, int staffID)
        {
            this.InitializeComponent();

            Username = username;
            StaffID = staffID;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            SQLConn.getData();
            this.lbluser.Text = "Login user : " + Username.ToUpper();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit System", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmListStaff f1 = new frmListStaff();
            f1.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = "Date-Time : " + DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmDatabaseConfig db = new frmDatabaseConfig();
            db.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmListCategory lc = new frmListCategory();
            lc.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit System", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmListProduct lp = new frmListProduct();
            lp.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmPOSForm lp = new frmPOSForm();
            lp.ShowDialog();
        }

        private void picMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmReportFilterDailySales FR = new frmReportFilterDailySales();
            FR.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmReportFilterStocks rf = new frmReportFilterStocks();
            rf.ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSystemSetting ss = new frmSystemSetting();
            ss.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmListReturns lr = new frmListReturns();
            lr.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void hTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
