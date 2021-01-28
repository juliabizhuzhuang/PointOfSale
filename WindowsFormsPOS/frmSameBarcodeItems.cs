﻿using System;
using System.Windows.Forms;

namespace WindowsFormsPOS
{
    public partial class frmSameBarcodeItems : Form
    {
        string productBarcode = "";
        //  frmPOS POS;
        public frmSameBarcodeItems(string dpbarcode, Form pos)
        {
            InitializeComponent();
            productBarcode = dpbarcode;
            // POS = pos as frmPOS;
        }

        public void LoadProducts(string barcode)
        {
            try
            {
                SQLConn.sqL = "SELECT ProductNo, Barcode, ProductCOde, P.Description,  UnitPrice FROM Product as P WHERE P.Barcode LIKE  @Barcode   ORDER BY Description";
                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.cmd.Parameters.AddWithValue("@Barcode", barcode);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                ListViewItem x = null;
                ListView1.Items.Clear();


                while (SQLConn.dr.Read() == true)
                {
                    x = new ListViewItem(SQLConn.dr["ProductNo"].ToString());
                    x.SubItems.Add(SQLConn.dr["Barcode"].ToString());
                    x.SubItems.Add(SQLConn.dr["ProductCode"].ToString());
                    x.SubItems.Add(SQLConn.dr["Description"].ToString());
                    x.SubItems.Add(String.Format(SQLConn.dr["UnitPrice"].ToString(), "#,##0.00"));


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

        private void frmSameBarcodeItems_Load(object sender, EventArgs e)
        {
            LoadProducts(productBarcode);
            ListView1.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (ListView1.FocusedItem.SubItems[3].Text == "")
            {
                MessageBox.Show("Please select item!", "Select Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.Close();

        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {


            this.Close();

        }

        private void frmSameBarcodeItems_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch
            { }
        }

        private void frmSameBarcodeItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ListView1.FocusedItem.SubItems[3].Text == "")
                {
                    MessageBox.Show("Please select item!", "Select Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                this.Close();
            }
        }
    }
}
