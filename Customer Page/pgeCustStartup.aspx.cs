using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Customer_Page_pgeCustStartup : System.Web.UI.Page
{
    string Ac_Code = string.Empty;
    string tblPrefix = string.Empty;
    string qry = string.Empty;
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = "NT_1_";
        Ac_Code = Request.QueryString["Ac_Code"].ToString();
        if (!Page.IsPostBack)
        {
            this.fillGrid();
        }
    }
    private void fillGrid()
    {
        try
        {
            qry = "Select C.Company_Name_E,C.Company_Code from Company C inner join " + tblPrefix + "AccountMaster A" +
                  " ON A.Company_Code=C.Company_Code where Ac_Code=" + Ac_Code + "";
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        grdCompany.DataSource = dt;
                        grdCompany.DataBind();
                    }
                    else
                    {
                        grdCompany.DataSource = null;
                        grdCompany.DataBind();
                    }
                }
            }
        }
        catch
        {
        }
    }
    protected void grdCompany_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int rowIndex = row.RowIndex;
            Int32 Company_Code = 0;
            Company_Code = Convert.ToInt32(grdCompany.Rows[rowIndex].Cells[0].Text);
            qry = "Select Is_Login from " + tblPrefix + "AccountMaster WHERE Company_Code=" + Company_Code + " AND Ac_Code=" + Ac_Code + "";
            string isLogin = clsCommon.getString(qry);
            int yes = 0;
            yes = int.Parse(isLogin);
            if (yes == 1)
            {
                Response.Redirect("../Customer Page/CustomerReport.aspx?Ac_Code=" + Ac_Code + " &Company_Code=" + Company_Code + "", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Access Denies!')", true);
            }
        }
        catch
        {
        }
    }
}