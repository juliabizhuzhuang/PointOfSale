using System;
using System.Windows.Forms;

namespace WindowsFormsPOS
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            SQLConn.getData();

        }

        private void Login()
        {
            try
            {
                SQLConn.sqL = "SELECT * FROM Staff WHERE Username = '" + txtusername.Text + "' AND UPassword = '" + txtPassword.Text + "'";
                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);//System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {

                    if (SQLConn.dr["Role"].ToString().ToUpper() == "ADMIN")
                    {
                        frmMain m = new frmMain(SQLConn.dr["Username"].ToString(), Convert.ToInt32(SQLConn.dr["StaffID"]));
                        m.Show();
                        this.Hide();
                    }
                    else
                    {

                    }
                }
                else
                {
                    MessageBox.Show("Invalid Password. Please try again.", "Login");
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
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                frmDatabaseConfig dc = new frmDatabaseConfig();
                dc.ShowDialog();
            }
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            Login();
        }

   
    }
}
