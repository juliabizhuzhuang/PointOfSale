using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace WindowsFormsPOS
{
    public partial class frmPOSForm : Form
    {
        int transactionID;
        public frmPOSForm()
        {
            InitializeComponent();
        }
        public void LoadTransaction(string strSearch)
        {
            try
            {
                SQLConn.sqL = "SELECT t.SaleDate,t.id,t.InvoiceNo,t.ProductNo,t.ItemPrice, t.Quantity, t.Discount," +
                    "P.Description, Barcode, UnitPrice, StocksOnHand, ReorderLevel, CategoryName FROM TransactionDetails t, Product as P LEFT JOIN Category C ON P.CategoryNo = C.CategoryNo WHERE p.ProductNo=t.ProductNo and P.Description LIKE  @Description OR P.Barcode LIKE @Barcode   ORDER BY Description";
                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.cmd.Parameters.AddWithValue("@Description", "%" + strSearch + "%");
                SQLConn.cmd.Parameters.AddWithValue("@Barcode", "%" + strSearch + "%");
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                ListViewItem x = null;
                ListView1.Items.Clear();


                while (SQLConn.dr.Read() == true)
                {
                    x = new ListViewItem(SQLConn.dr["Id"].ToString());
                    x.SubItems.Add(SQLConn.dr["SaleDate"].ToString());
                    x.SubItems.Add(SQLConn.dr["InvoiceNo"].ToString());
                    x.SubItems.Add(SQLConn.dr["Description"].ToString());
                    x.SubItems.Add(SQLConn.dr["Barcode"].ToString());
                    x.SubItems.Add(SQLConn.dr["CategoryName"].ToString());
                    x.SubItems.Add(String.Format(SQLConn.dr["UnitPrice"].ToString(), "#,##0.00"));
                    x.SubItems.Add(SQLConn.dr["ItemPrice"].ToString());
                    x.SubItems.Add(SQLConn.dr["Quantity"].ToString());
                    x.SubItems.Add(SQLConn.dr["PaymentMethod"].ToString());
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
            frmAddEditPOSTrans aeP = new frmAddEditPOSTrans(init);
            aeP.ShowDialog();
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
                        transactionID = Convert.ToInt32(ListView1.FocusedItem.Text);
                        frmAddEditPOSTrans aeP = new frmAddEditPOSTrans(transactionID);
                        aeP.ShowDialog();
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
            SQLConn.strSearch = Interaction.InputBox("ENTER PRODUCT NAME OR BARCODE.", "Search Product", " ");

            if (SQLConn.strSearch.Length >= 1)
            {
                LoadTransaction(SQLConn.strSearch.Trim());
            }
            else if (string.IsNullOrEmpty(SQLConn.strSearch))
            {
                return;
            }
        }

        private void frmListProduct_Load(object sender, EventArgs e)
        {
            LoadTransaction("");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
