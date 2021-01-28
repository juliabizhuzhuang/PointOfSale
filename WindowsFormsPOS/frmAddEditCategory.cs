using System;
using System.Windows.Forms;

namespace WindowsFormsPOS
{
    public partial class frmAddEditCategory : Form
    {
            int categoryID;
            public frmAddEditCategory(int catID)
            {
                InitializeComponent();

                categoryID = catID;
            }

            private void GetCategoryNo()
            {
                try
                {
                    SQLConn.sqL = "SELECT CategoryNo FROM Category ORDER BY CategoryNo DESC";
                    SQLConn.ConnDB();
                    SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                    SQLConn.dr = SQLConn.cmd.ExecuteReader();

                    if (SQLConn.dr.Read() == true)
                    {
                        lblCategoryNo.Text = (Convert.ToInt32(SQLConn.dr["CategoryNo"]) + 1).ToString();
                    }
                    else
                    {
                        lblCategoryNo.Text = "1";
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

            private void LoadUpdateCategory()
            {
                try
                {
                    SQLConn.sqL = "SELECT * FROM Category WHERE CategoryNo = '" + categoryID + "'";
                    SQLConn.ConnDB();
                    SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                    SQLConn.dr = SQLConn.cmd.ExecuteReader();

                    if (SQLConn.dr.Read() == true)
                    {
                        lblCategoryNo.Text = SQLConn.dr["CategoryNo"].ToString();
                        txtCatName.Text = SQLConn.dr["CategoryName"].ToString();
                        txtDescription.Text = SQLConn.dr["Description"].ToString();
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

            private void AddCategory()
            {
                try
                {
                    SQLConn.sqL = "INSERT INTO Category(CategoryName, Description) VALUES('" + txtCatName.Text + "', '" + txtDescription.Text + "')";
                    SQLConn.ConnDB();
                    SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                    SQLConn.cmd.ExecuteNonQuery();
                    MessageBox.Show("New category successfully added.",  "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            private void UpdateCategory()
            {
                try
                {
                    SQLConn.sqL = "UPDATE Category SET CategoryName= '" + txtCatName.Text + "', Description = '" + txtDescription.Text + "' WHERE CategoryNo = '" + categoryID + "'";
                    SQLConn.ConnDB();
                    SQLConn.cmd = new System.Data.SqlClient.SqlCommand(SQLConn.sqL, SQLConn.conn);
                    SQLConn.cmd.ExecuteNonQuery();
                    MessageBox.Show("Category successfully updated.",  "Update Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            private void CLearFields()
            {
                lblCategoryNo.Text = "";
                txtCatName.Text = "";
                txtDescription.Text = "";
            }

            private void frmAddEditCategory_Load(object sender, EventArgs e)
            {
                if (SQLConn.adding == true)
                {
                    lblTitle.Text = "Adding New Category";
                    CLearFields();
                    GetCategoryNo();
                }
                else
                {
                    lblTitle.Text = "Updating Category";
                    LoadUpdateCategory();
                }
            }

            private void button3_Click(object sender, EventArgs e)
            {
                if (SQLConn.adding == true)
                {
                    AddCategory();
                }
                else
                {
                    UpdateCategory();

                }
                if (System.Windows.Forms.Application.OpenForms["frmListCategory"] != null)
                {
                    (System.Windows.Forms.Application.OpenForms["frmListCategory"] as frmListCategory).LoadCategories("");
                }

                this.Close();
            }

            private void button4_Click(object sender, EventArgs e)
            {
                this.Close();
            }

        }
    }
