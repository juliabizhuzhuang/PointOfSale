using System;
using System.Windows.Forms;

namespace WindowsFormsPOS
{
    public partial class frmAddProductQuick : Form
    {
        int categoryID;
        //  frmPOS POS;
        public frmAddProductQuick(Form callingForm)
        {
            InitializeComponent();
            //    POS = callingForm as frmPOS;
        }

        private void GetProductNo()
        {
            try
            {
                SQLConn.sqL = "SELECT ProductNo FROM Product ORDER BY ProductNo DESC";
                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {
                    lblProductNo.Text = (Convert.ToInt32(SQLConn.dr["ProductNo"]) + 1).ToString();
                }
                else
                {
                    lblProductNo.Text = "1";
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


        private void AddProducts()
        {

            string barcode = "";

            if (txtBarcode.Text.Trim() == "")
            {
                barcode = "NO BARCODE";
            }
            else
            {
                barcode = txtBarcode.Text;
            }

            try
            {
                SQLConn.sqL = "INSERT INTO Product(ProductCode, Description, Barcode, UnitPrice, StocksOnHand, ReorderLevel, CategoryNo) VALUES('" + txtProductCode.Text + "', '" + txtDescription.Text + "', '" + barcode + "', '" + txtUnitPrice.Text.Replace(",", "") + "', '" + txtStocksOnHand.Text.Replace(",", "") + "', '" + txtReorderLevel.Text + "', '" + categoryID + "')";
                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.cmd.ExecuteNonQuery();
                MessageBox.Show("Product successfully added.", "Add Product", MessageBoxButtons.OK,MessageBoxIcon.Information);
                AddStockIn();
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

        private void AddStockIn()
        {
            try
            {
                SQLConn.sqL = "INSERT INTO StockIn(ProductNo, Quantity, DateIn) Values('" + lblProductNo.Text + "', '" + txtStocksOnHand.Text + "', '" + System.DateTime.Now.ToString("MM/dd/yyyy") + "')";
                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.cmd.ExecuteNonQuery();
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



        public string Category
        {
            get { return txtCategory.Text; }
            set { txtCategory.Text = value; }
        }

        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            frmSelectCategory flc = new frmSelectCategory(this, true);
            flc.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (txtDescription.Text == "")
            {
                MessageBox.Show("Please enter product description");
                return;
            }

            if (txtUnitPrice.Text == "")
            {
                MessageBox.Show("Please enter Stocks On Hand");
                return;
            }


            if (txtStocksOnHand.Text == "")
            {
                MessageBox.Show("Please enter Stocks On Hand");
                return;
            }


            AddProducts();
            //  this.Hide();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddProductQuick_Load(object sender, EventArgs e)
        {
            GetProductNo();
        }
    }
}
