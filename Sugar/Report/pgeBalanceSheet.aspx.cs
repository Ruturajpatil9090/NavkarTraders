using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Report_pgeBalanceSheet : System.Web.UI.Page
{
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string tblPrefix = string.Empty;
    string user = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        user = Session["user"].ToString();

        if (!Page.IsPostBack)
        {
            txtToDt.Text = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            txtFromDate.Text = clsGV.Start_Date;
            txtTooDate.Text = clsGV.To_date;
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            { }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }
    protected void btnBalanceSheet_Click(object sender, EventArgs e)
    {
        try
        {
            string dt = txtToDt.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + dt + "')", true);
        }
        catch
        {

        }
    }
    protected void btnProfitLoss_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    string dt = txtToDt.Text;
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:PL('" + dt + "')", true);
        //}
        //catch
        //{

        //}
        try
        {
            string dt = txtToDt.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:PLSP('" + dt + "')", true);
        }
        catch
        {

        }
    }
    protected void btnBalanceSheetNew_Click(object sender, EventArgs e)
    {
        try
        {
            string dt = txtToDt.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky1", "javascript:sp1('" + dt + "')", true);
        }
        catch
        {

        }
    }
    protected void btnProfitLoss2_Click(object sender, EventArgs e)
    {

        try
        {
            string fdt = txtFromDate.Text;
            string tdt = txtTooDate.Text;
            fdt = DateTime.Parse(fdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            tdt = DateTime.Parse(tdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kys", "javascript:PLSP2('" + fdt + "','" + tdt + "')", true);
        }
        catch
        {

        }
    }
    protected void btnddreport_Click(object sender, EventArgs e) 
    {
        try
        {
            string dt = txtToDt.Text;
            dt = DateTime.Parse(dt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky1", "javascript:dd('" + dt + "')", true);
        }
        catch
        {

        }
    }

    protected void btndd1report_Click(object sender, EventArgs e)
    {
        try
        {
            string dt = txtToDt.Text;
            dt = DateTime.Parse(dt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky1", "javascript:dd1('" + dt + "')", true);
        }
        catch
        {

        }
    }
    protected void btndd1summeryrpt_Click(object sender, EventArgs e)
    {
        try
        {
            string dt = txtToDt.Text;
            dt = DateTime.Parse(dt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky1", "javascript:dd1sum('" + dt + "')", true);
        }
        catch
        {

        }
    }

    protected void btnBalSheetNew_Click(object sender, EventArgs e)
    {
        try
        {
            string dt = txtToDt.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky1", "javascript:balsheet1('" + dt + "')", true);
        }
        catch
        {

        }
    }

    protected void btncrystal_Click(object sender, EventArgs e)
    {
        try
        {
            string dt = txtToDt.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky1", "javascript:crystal('" + dt + "')", true);
        }
        catch
        {

        }
    }

}