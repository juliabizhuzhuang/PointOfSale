﻿using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace WindowsFormsPOS
{
    public partial class frmListCategory : Form
    {
        public int catgoryID;

        public frmListCategory()
        {
            InitializeComponent();
        }

        public void LoadCategories(string strSearch)
        {
            try
            {
                SQLConn.sqL = "SELECT * FROM CATEGORY WHERE CategoryName LIKE '" + strSearch + "%' ORDER By CategoryName";
                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader(CommandBehavior.CloseConnection);

                ListViewItem x = null;
                ListView1.Items.Clear();

                while (SQLConn.dr.Read() == true)
                {
                    x = new ListViewItem(SQLConn.dr["CategoryNo"].ToString());
                    x.SubItems.Add(SQLConn.dr["CategoryName"].ToString());
                    x.SubItems.Add(SQLConn.dr["Description"].ToString());
                    ListView1.Items.Add(x);
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
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            SQLConn.adding = true;
            SQLConn.updating = false;
            int init = 0;
            frmAddEditCategory aeC = new frmAddEditCategory(init);
            aeC.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ListView1.Items.Count == 0)
            {
                MessageBox.Show("Please select record to update", "Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                if (ListView1.FocusedItem != null)
                if (string.IsNullOrEmpty(ListView1.FocusedItem.Text))
                {

                }
                else
                {
                    SQLConn.adding = false;
                    SQLConn.updating = true;
                    catgoryID = Convert.ToInt32(ListView1.FocusedItem.Text);
                    frmAddEditCategory aeC = new frmAddEditCategory(catgoryID);
                    aeC.ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show("Please select record to update", "Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SQLConn.strSearch = Interaction.InputBox("ENTER CATEGORY NAME.", "Search Category", " ");

            if (SQLConn.strSearch.Length >= 1)
            {
                LoadCategories(SQLConn.strSearch.Trim());
            }
            else if (string.IsNullOrEmpty(SQLConn.strSearch))
            {
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListCategory_Load(object sender, EventArgs e)
        {
            LoadCategories("");
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
