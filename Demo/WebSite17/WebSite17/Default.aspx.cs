using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page 
{
    string con = "server=VIJAY-PC;database=demo;uid=sa;pwd=manager";
  SqlConnection sqlcon;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showgrid();
        }        
    }
    public void showgrid()
    {
        DataTable dt = new DataTable();
        sqlcon = new SqlConnection(con );
        sqlcon.Open();
        SqlDataAdapter sda = new SqlDataAdapter();
        string strQuery = "select * from employee ";
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = sqlcon;
        sda.SelectCommand = cmd;
        sda.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        //ASPxGridView1.DataSource = dt;
        //ASPxGridView1.DataBind();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        showgrid();

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
       
        GridView1.EditIndex = e.NewEditIndex;
        showgrid();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        

        Label lb = (Label)GridView1.Rows[e.RowIndex].FindControl("Label6");
        DropDownList ddl = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("DropDownList1");
        RadioButtonList rbl = (RadioButtonList)GridView1.Rows[e.RowIndex].FindControl("RadioButtonList1");
        CheckBoxList chb = (CheckBoxList)GridView1.Rows[e.RowIndex].FindControl("CheckBoxList2");
        TextBox tx1 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox1");
        TextBox tx2 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox2");
        TextBox tx3 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBox3");
        sqlcon = new SqlConnection(con);
        sqlcon.Open();
        string sql = "update employee set emp_name='" + tx1.Text + "',emp_address='" + tx2.Text + "',salary='" +
            tx3.Text + "',department='" + ddl.SelectedValue.ToString() + "',maritalstatus='" +
            rbl.SelectedValue.ToString() + "',Active_status='" + chb.SelectedValue.ToString() + "' where emp_id='" +
            lb.Text + "'";

        SqlCommand cmd = new SqlCommand(sql);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = sqlcon;
        cmd.ExecuteNonQuery ();
        GridView1.EditIndex = -1;
        showgrid ();



    }
   public DataTable load_department()
    {
       DataTable dt = new DataTable();
       sqlcon = new SqlConnection(con);
       sqlcon.Open();
       string sql = "select * from department";
       SqlCommand cmd = new SqlCommand(sql);
       cmd.CommandType = CommandType.Text;
       cmd.Connection = sqlcon;
       SqlDataAdapter sd = new SqlDataAdapter(cmd);
       sd.Fill(dt);
       return dt;

    }
   protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
   {
     DataRowView drv = e.Row.DataItem as DataRowView;
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
          
          if ((e.Row.RowState & DataControlRowState.Edit) > 0)
          {
              DropDownList dp= (DropDownList)e.Row.FindControl ("DropDownList1");
              DataTable dt = load_department();
              for (int i = 0; i < dt.Rows.Count; i++)
              {
                   ListItem lt = new ListItem();
                   lt.Text = dt.Rows[i][0].ToString();
                   dp.Items.Add(lt);
              }
              dp.SelectedValue = drv[3].ToString();
              RadioButtonList rbtnl = (RadioButtonList)e.Row.FindControl("RadioButtonList1");
              rbtnl.SelectedValue = drv[5].ToString();
              CheckBoxList chkb = (CheckBoxList)e.Row.FindControl("CheckBoxList2");
              chkb.SelectedValue = drv[6].ToString();

          }         
          
       }
   }
}

