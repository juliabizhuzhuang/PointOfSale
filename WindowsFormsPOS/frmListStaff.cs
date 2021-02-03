using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace WindowsFormsPOS
{
    public partial class frmListStaff : Form
    {


        public int staffID;

        public frmListStaff()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStaffs("");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLConn.adding = true;
            SQLConn.updating = false;
            int init = 0;
            frmAddEditStaff f2 = new frmAddEditStaff(init);
            f2.ShowDialog();
        }


        public void LoadStaffs(string search)
        {
            try
            {
                SQLConn.sqL = "SELECT StaffID, CONCAT(Lastname, ', ', Firstname, ' ', MI) as ClientName, CONCAT(Street, ', ', Barangay, ', ', City , ', ', Province) as Address, ContactNo, username, role FROM Staff WHERE LASTNAME LIKE '" + search.Trim() + "%' ORDER By Lastname";
                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                ListViewItem x = null;
                ListView1.Items.Clear();

                while (SQLConn.dr.Read() == true)
                {
                    x = new ListViewItem(SQLConn.dr["StaffId"].ToString());
                    x.SubItems.Add(SQLConn.dr["ClientName"].ToString());
                    x.SubItems.Add(SQLConn.dr["ContactNo"].ToString());
                    x.SubItems.Add(SQLConn.dr["Address"].ToString());
                    x.SubItems.Add(SQLConn.dr["username"].ToString());
                    x.SubItems.Add(SQLConn.dr["Role"].ToString());

                    ListView1.Items.Add(x);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (ListView1.Items.Count == 0)
            {
                MessageBox.Show("Please select record to update", "Add Stocks", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }
            try
            {
                if (ListView1.FocusedItem !=null)
                if (string.IsNullOrEmpty(ListView1.FocusedItem.Text))
                {

                }
                else
                {
                    SQLConn.adding = false;
                    SQLConn.updating = true;
                    staffID = Convert.ToInt32(ListView1.FocusedItem.Text);
                    frmAddEditStaff f2 = new frmAddEditStaff(staffID);
                    f2.ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show("Please select record to update", "Add Stocks", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

             SQLConn.strSearch = Interaction.InputBox("ENTER LAST NAME OF THE STAFF.", "Search Staff", " ");

            if (SQLConn.strSearch.Length >= 1)
            {
                LoadStaffs(SQLConn.strSearch.Trim());
            }
            else if (string.IsNullOrEmpty(SQLConn.strSearch))
            {
                return;
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
