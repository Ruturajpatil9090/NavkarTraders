using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_pgeAccountType : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["user"].ToString();
        tblPrefix = Session["tblPrefix"].ToString();
        //   AccountMasterTable = tblPrefix + "AccountMaster";
        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                //txtFromDate.Text = clsGV.Start_Date;
                //txtToDate.Text = clsGV.End_Date;
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }
    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        setFocusControl(drpAcType);

    }
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string AcType = drpAcType.SelectedValue.ToString();

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + AcType + "')", true);

    }
}