using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MasterPage2 : System.Web.UI.MasterPage
{
    public string MasterPrefix = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String str = Session["Group_Code"].ToString();
            // MasterPrefix = Session["tblPrefix"].ToString();
            string companyName = clsCommon.getString("select Company_Name_E from Company where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
            string branchName = clsCommon.getString("select Branch from BranchMaster where  Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Branch_Id='" + Session["Branch_Code"].ToString() + "'");

            lblCompanyName.Text = companyName;
            if (branchName != string.Empty)
            {
                lblBranch.Text = branchName;
            }
            else
            {
                lblBranch.Text = "Default";
            }
            lblUser.Text = Session["user"].ToString();

            lblYear.Text = Session["accountingYear"].ToString();
            // lblUser.Text = System.Net.Dns.GetHostName();

            //string name = Environment.MachineName;
            //string name = System.Net.Dns.GetHostName();
            //string name = System.Windows.Forms.SystemInformation.ComputerName;
            //string name = System.Environment.GetEnvironmentVariable(“COMPUTERNAME”);


            string rd = HttpContext.Current.Request.Url.AbsolutePath;


        }
        catch (Exception exx)
        {
            Response.Redirect("../pgeLoginForm.aspx");
            //  Response.Redirect("http://localhost:1148/HomePage/pgeloginForm.aspx");
            //    Response.Redirect("http://192.168.1.75/Home/pgeloginForm.aspx");
        }
    }

    protected void lnkAllCompanies_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/pgeStartup.aspx?groupcode=" + Session["GroupCode"].ToString());
    }
    public Label lblCreatedBy
    {
        get { return MasterlblCreatedBy; }
    }
    public Label lblModifiedBy
    {
        get { return MasterlblModifiedBy; }
    }
}
