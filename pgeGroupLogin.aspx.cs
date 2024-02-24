using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class pgeGroupLogin : System.Web.UI.Page
{
    DataSet ds = null;
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string serverName = string.Empty;
            string databaseName = string.Empty;

            clsDAL._connection.Close();

            serverName = "ASHISH\\SQLEXPRESS;";
            databaseName = "GroupDB";
            clsDAL.strConnectionString = "Data Source=" + serverName + "Initial Catalog=" + databaseName + ";Integrated Security=True;";

            string groupCode=string.Empty;

            string qry = "";
            qry = "select * from GroupMaster where UserNm='" + txtGroupUsername.Text + "' and Password='" + txtPass.Text + "'";
            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //RAHUL ; dont yse below calls
                        groupCode=dt.Rows[0]["GroupCode"].ToString();
                        clsDAL._connection.Close();

                        databaseName="AccoWeb_"+groupCode;

                        clsDAL.strConnectionString = "Data Source=" + serverName + "Initial Catalog=" + databaseName + ";Integrated Security=True;";
                        clsDAL.OpenConnection();
                        Response.Redirect("pgeCreateCompany.aspx");

                     //   qry = "select * from Company";
                     //   ds = clsDAL.SimpleQuery(qry);

                    }
                }
            }

        }
        catch
        {

        }

    }
}