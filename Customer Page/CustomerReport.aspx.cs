using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Customer_Page_CustomerReport : System.Web.UI.Page
{
    string Ac_Code = string.Empty;
    string Company_Code = string.Empty;
    string qry = string.Empty;
    string tblPrefix=string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Ac_Code = Request.QueryString["Ac_Code"].ToString();
        Company_Code = Request.QueryString["Company_Code"].ToString();
        tblPrefix="NT_1_";
        if (!Page.IsPostBack)
        {
            this.CustInfo();
        }
    }
    protected void CustInfo()
    {
        qry = "Select Ac_Code,Ac_Name_E,Address_E,Mobile_No from " + tblPrefix + "AccountMaster WHERE Ac_Code=" + Ac_Code + " AND Company_Code=" + Company_Code + "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                txtCustAccNo.Text = dt.Rows[0]["Ac_Code"].ToString();
                lblCustName.Text = dt.Rows[0]["Ac_Name_E"].ToString();
                txtCustAddress.Text = dt.Rows[0]["Address_E"].ToString();
                lblCustMobile.Text = dt.Rows[0]["Mobile_No"].ToString();
            }
        }
    }
    protected void lnk_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("../pgeloginForm.aspx", false);
    }
}