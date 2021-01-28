using System;
using System.Windows.Forms;




namespace WindowsFormsPOS
{
    public partial class frmEnterPassword : Form
    {
        //  frmPOS POSForm;
        public frmEnterPassword()
        {
            InitializeComponent();
            //POSForm = pos;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //     this.POSForm.IsPasswordCancel = true;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //      this.POSForm.IsPasswordCorrect = IsPasswordCorrect();
            this.Close();
        }

        private bool IsPasswordCorrect()
        {
            bool ret = false;

            try
            {
                SQLConn.sqL = "SELECT * FROM Staff WHERE Role ='Admin' AND UPassword = '" + txtAdminPassword.Text + "'";
                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {
                    ret = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SQLConn.cmd.Dispose();
                SQLConn.conn.Close();
            }

            return ret;
        }

        private void txtAdminPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOkay.PerformClick();
            }
        }


    }
}
