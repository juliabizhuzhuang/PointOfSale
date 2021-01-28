using System;
using System.Windows.Forms;


namespace WindowsFormsPOS
{
    public partial class frmListProductStocksIn : Form
    {
        int productID;
        public frmListProductStocksIn(int prodID)
        {
            InitializeComponent();
            productID = prodID;
        }

        private void GetProductInfo()
        {

            try
            {

                SQLConn.sqL = "SELECT ProductCode, Description, UnitPrice, StocksOnHand FROM Product WHERE ProductNo =" + productID + "";
                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.dr = SQLConn.cmd.ExecuteReader();

                if (SQLConn.dr.Read() == true)
                {
                    lblProductCode.Text = SQLConn.dr[0].ToString();
                    lblDescription.Text = SQLConn.dr[1].ToString();
                    lblPrice.Text = String.Format(SQLConn.dr[2].ToString(), "#.000").ToString();
                    lblCurrentStocks.Text = SQLConn.dr[3].ToString();
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

        private void AddStockIn()
        {
            try
            {
                SQLConn.sqL = "INSERT INTO StockIn(ProductNo, Quantity, DateIn) Values('" + productID + "', '" + txtQuantity.Text + "', '" + System.DateTime.Now.ToString("MM/dd/yyyy") + "')";
                SQLConn.ConnDB();
                SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                SQLConn.cmd.ExecuteNonQuery();
                MessageBox.Show("Stocks successfully added.", "Add Stocks", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                UpdateProductQuantity();
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

        private void UpdateProductQuantity()
        {
            try
            {
                SQLConn.sqL = "UPDATE Product SET StocksOnhand = StocksOnHand + '" + txtQuantity.Text.Replace(",", "") + "' WHERE ProductNo = '" + productID + "'";
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

        private void frmListProductStocksIn_Load(object sender, EventArgs e)
        {
            GetProductInfo();
            txtQuantity.Text = "";
            txtTotalStocks.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddStockIn();
            if (System.Windows.Forms.Application.OpenForms["frmListProduct"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["frmListProduct"] as frmListProduct).LoadProducts("");
            }

            this.Close();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            txtTotalStocks.Text = String.Format(lblCurrentStocks.Text + txtQuantity.Text, "#,##0.00");
        }
    }
}
