using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("login1.aspx");

        if (!Page.IsPostBack)
        {
            if (!Page.IsPostBack)
            {
                //clsButtonNavigation.demoFun();


                //set all  table prefixes as per the company name
                //string companyCode = "101";
                //string prefix="abc_"+companyCode+"_";

                //Session["prefix"] = null;

            }
          //  pgeCreateCompany.findControl("");
        }
    }
    
}
