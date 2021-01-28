using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace Test
{
    public class MySqlConnection { }

    public class MySqlConnection()
    {
        string connetionString;

        SqlConnection cnn;

        connetionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=POSDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cnn = new SqlConnection(connetionString);
        cnn.Open();
            MessageBox.Show("Connection Open  !");
            SqlCommand cmd = new SqlCommand();// Creating instance of SqlCommand  
        cmd.Connection = conn; // set the connection to instance of SqlCommand  
            cmd.CommandText = "insert into student_detail values (" + txtrollno.Text + ",'" + txtname.Text + "','" + txtcourse.Text + "')"; // set  
            //the sql command ( Statement )  
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Saved");
    }
}
