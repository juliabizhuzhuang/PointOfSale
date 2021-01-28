using System;
using System.Windows.Forms;

namespace WindowsFormsPOS
{
    public partial class frmDatabaseConfig : Form
    {
        public frmDatabaseConfig()
        {
            InitializeComponent();
        }

        private string TstServerName;
        private string TstPortName;
        private string TstUserName;
        private string TstPwd;
        private string TstDBName;

        private void cmdTest_Click(object sender, EventArgs e)
        {
            //Test database connection

            TstServerName = txtServerHost.Text;
            TstPortName = txtPort.Text;
            TstUserName = txtUserName.Text;
            TstPwd = txtPassword.Text;
            TstDBName = txtDatabase.Text;


            try
            {
                SQLConn.conn.ConnectionString = "Server = '" + TstServerName + "';  " + "Port = '" + TstPortName + "'; " + "Database = '" + TstDBName + "'; " + "user id = '" + TstUserName + "'; " + "password = '" + TstPwd + "'";


                SQLConn.conn.Open();
                MessageBox.Show("Test connection successful",  "Database Settings", MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
            catch
            {
                MessageBox.Show("The system failed to establish a connection",  "Database Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            SQLConn.DisconnMy();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            TstServerName = txtServerHost.Text;
            TstPortName = txtPort.Text;
            TstUserName = txtUserName.Text;
            TstPwd = txtPassword.Text;
            TstDBName = txtDatabase.Text;

            try
            {
                SQLConn.conn.ConnectionString = "Server = '" + TstServerName + "';  " + "Port = '" + TstPortName + "'; " + "Database = '" + TstDBName + "'; " + "user id = '" + TstUserName + "'; " + "password = '" + TstPwd + "'";
                SQLConn.conn.Open();

                SQLConn.DBName = txtDatabase.Text;
                SQLConn.ServerName = txtServerHost.Text;
                SQLConn.UserName = txtUserName.Text;
                SQLConn.PortName = txtPort.Text;
                SQLConn.Pwd = txtPassword.Text;

                SQLConn.SaveData();
                this.Close();
            }
            catch
            {
                MessageBox.Show("The system failed to establish a connection",  "Database Settings", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            SQLConn.DisconnMy();
            //save database
        }

        private void frmDatabaseConfig_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(178, 127);
            txtServerHost.Text = SQLConn.ServerName;
            txtPort.Text = SQLConn.PortName;
            txtUserName.Text = SQLConn.UserName;
            txtPassword.Text = SQLConn.Pwd;
            txtDatabase.Text = SQLConn.DBName;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
