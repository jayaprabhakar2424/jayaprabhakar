using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class Default2 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader dr;
    string query;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            con = new SqlConnection();
            con.ConnectionString = "server=.;database=Students;uid=sa;pwd=microdots";
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
        catch (SqlException ex)
        {
            Label1.Text = "error";

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            query = "insert into jp values('" + TextBox1.Text + "'," + TextBox2.Text + ",'" + TextBox3.Text + "','" + TextBox4.Text + "'," + TextBox5.Text + "," + TextBox6.Text + ",'" + TextBox7.Text + "','" + TextBox8.Text + "' )";
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            Label1.Text = "Record Inserted";
        }
        catch
        {
            Label1.Text = "error";
        }
    }
}