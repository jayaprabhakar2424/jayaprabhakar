using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Default3 : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter adp;
    DataSet ds = new DataSet();
    //string constr = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
    string constr = "server=.;database=Students;uid=sa;pwd=microdots";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            GridViewload();
        }

    }
    #region user defined fuctions
    public void GridViewload()
    {
        using (con = new SqlConnection(constr))
        {
            using (cmd = new SqlCommand())
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select * from jp";
                adp = new SqlDataAdapter(cmd);
                ds.Clear();
                adp.Fill(ds);
                GridView1.DataSourceID = null;
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }
    }
    #endregion
    protected void gridrow_edit(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        GridViewload();
    }
    protected void gridupdate_edit(object sender, GridViewUpdateEventArgs e)
    {

        Label cusid = (Label)GridView1.Rows[e.RowIndex].FindControl("CusIDLabel");
        TextBox Name = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TxtName");
        TextBox BookingNumber = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TxtBooking Number");
        TextBox From = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TxtFrom");
        TextBox To = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TxtTo");
        TextBox JourneyDate = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TxtJourney Date");
        TextBox Time = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TxtTime");
        TextBox ACNonAC = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TxtAC/NonAC");
        TextBox SleeperSemiSleeper = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TxtSleeper/SemiSleeper");
        using (con = new SqlConnection(constr))
        {
            using (cmd = new SqlCommand())
            {
                try
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_customer";
                    cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@cusid", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@Booking Number", SqlDbType.BigInt, 10));
                    cmd.Parameters.Add(new SqlParameter("@From", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@To", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@Journey Date", SqlDbType.BigInt, 10));
                    cmd.Parameters.Add(new SqlParameter("@Time", SqlDbType.BigInt, 10));
                    cmd.Parameters.Add(new SqlParameter("@AC/Non AC", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@Sleeper/SemiSleeper", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add("@errorout", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.Parameters["@action"].Value = "update";
                    cmd.Parameters["@cusid"].Value = Convert.ToInt32(cusid.Text);
                    cmd.Parameters["@Name"].Value = Convert.ToString(Name.Text);
                    cmd.Parameters["@Booking Number"].Value = Convert.ToString(BookingNumber.Text);
                    cmd.Parameters["@From"].Value = Convert.ToInt64(From.Text);
                    cmd.Parameters["@To"].Value = Convert.ToString(To.Text);
                    cmd.Parameters["@Journey Date"].Value = Convert.ToString(JourneyDate.Text);
                    cmd.Parameters["@Time"].Value = Convert.ToString(Time.Text);
                    cmd.Parameters["@AC/NonAC"].Value = Convert.ToString(ACNonAC.Text);
                    cmd.Parameters["@Sleeper/SemiSleeper"].Value = Convert.ToString(SleeperSemiSleeper.Text);
                    cmd.Parameters["@createdby"].Value = "admin";
                    cmd.ExecuteNonQuery();
                    int err = Convert.ToInt32(cmd.Parameters["@errorout"].Value);
                    GridViewload();
                    GridView1.DataBind();
                    msglabel.Visible = true;
                    msglabel.Text = "Data Updated Successfully";
                }
                catch
                {
                    msglabel.Text = "Error";
                }
            }
        }
        GridView1.EditIndex = -1;
        GridViewload();
        GridView1.DataBind();
        msglabel.Visible = true;
        msglabel.Text = "Data Deleted Successfully";
    }
    protected void gridcommand_edit(object sender, GridViewCommandEventArgs e)
    {
        TextBox Name = null, BookingNumber = null, From = null, To = null, JourneyDate = null, Time = null, ACNonAC = null, SleeperSemiSleeper = null;
        if (e.CommandName == "Insert")
        {
            Name = (TextBox)GridView1.FooterRow.FindControl("TxtAddName");
            BookingNumber = (TextBox)GridView1.FooterRow.FindControl("TxtAddBooking Number");
            From = (TextBox)GridView1.FooterRow.FindControl("TxtAddFrom");
            To = (TextBox)GridView1.FooterRow.FindControl("TxtAddTo");
            JourneyDate = (TextBox)GridView1.FooterRow.FindControl("TxtAddJourneyDate");
            Time = (TextBox)GridView1.FooterRow.FindControl("TxtAddTime");
            ACNonAC = (TextBox)GridView1.FooterRow.FindControl("TxtAddACNonAC");
            SleeperSemiSleeper = (TextBox)GridView1.FooterRow.FindControl("TxtAddSleeperSemiSleeper");
            using (con = new SqlConnection(constr))
            {
                using (cmd = new SqlCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "SP_customer";
                        cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.VarChar, 50));
                        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50));
                        cmd.Parameters.Add(new SqlParameter("@Booking Number", SqlDbType.BigInt, 10));
                        cmd.Parameters.Add(new SqlParameter("@From", SqlDbType.VarChar, 50));
                        cmd.Parameters.Add(new SqlParameter("@To", SqlDbType.VarChar, 50));
                        cmd.Parameters.Add(new SqlParameter("@Journey Date", SqlDbType.BigInt, 10));
                        cmd.Parameters.Add(new SqlParameter("@Time", SqlDbType.BigInt, 10));
                        cmd.Parameters.Add(new SqlParameter("@AC/NonAC", SqlDbType.VarChar, 50));
                        cmd.Parameters.Add(new SqlParameter("@Sleeper/SemiSleeper", SqlDbType.VarChar, 50));
                        cmd.Parameters.Add(new SqlParameter("@createdby", SqlDbType.VarChar, 50));
                        cmd.Parameters.Add("@errorout", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.Parameters["@action"].Value = "insert";
                        cmd.Parameters["@Name"].Value = Convert.ToString(Name.Text);
                        cmd.Parameters["@BookingNumber"].Value = Convert.ToString(BookingNumber.Text);
                        cmd.Parameters["@From"].Value = Convert.ToInt64(From.Text);
                        cmd.Parameters["@To"].Value = Convert.ToString(To.Text);
                        cmd.Parameters["@JourneyDate"].Value = Convert.ToString(JourneyDate.Text);
                        cmd.Parameters["@Time"].Value = Convert.ToString(Time.Text);
                        cmd.Parameters["@ACNonAC"].Value = Convert.ToString(ACNonAC.Text);
                        cmd.Parameters["@SleeperSemiSleeper"].Value = Convert.ToString(SleeperSemiSleeper.Text);
                        cmd.Parameters["@createdby"].Value = "admin";
                        cmd.ExecuteNonQuery();
                        int err =Convert.ToInt32(cmd.Parameters["@errorout"].Value);
                        GridViewload();
                     }
                   catch
                    {
                        msglabel.Text = "Error";
                    }
    
                }

            }
            GridView1.EditIndex = -1;
            GridViewload();
            GridView1.DataBind();
            msglabel.Visible = true;
            msglabel.Text = "Data Inserted Successfully";
        }
    }
   protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridViewload();
    }
   protected void pageindex_changing(object sender, GridViewPageEventArgs e)
  {
    GridView1.PageIndex = e.NewPageIndex;
    GridViewload();
  }

}