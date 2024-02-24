using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Configuration;

public partial class Sugar_Utility_pgeGenerateCustomerLogin : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    string cs = string.Empty;
    static WebControl objAsp = null;
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    string qry = string.Empty;
    int company_code = 0;
    DataSet ds = new DataSet();
    DataTable Maindt = null;
    DataRow dr = null;
    string Detail_Insert = string.Empty;
    string Detail_Update = string.Empty;
    string Detail_Delete = string.Empty;
    string Detail_Fields = string.Empty;
    string Detail_Values = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        con = new SqlConnection(cs);
        tblPrefix = Session["tblPrefix"].ToString();
        tblHead = "tbluser";
        company_code = Convert.ToInt32(Session["Company_Code"].ToString());
        user = Session["user"].ToString();
        Maindt = new DataTable();
        dr = null;
        Maindt.Columns.Add("Querys", typeof(string));
        dr = Maindt.NewRow();
        if (!Page.IsPostBack)
        {
            btnGenerate.Text = "Create Login";
            btnShowGeneratedAccounts.Text = "Show Generated";
            pnlAccounts.Visible = true;
            pnlGeneratedAccounts.Visible = false;


            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                this.BindGrid();
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
            SetFocus(txtSearch);
        }
    }
    protected void BindGrid()
    {
        //qry = "Select Ac_Code,Ac_Name_E,Email_Id,Mobile_No from " + tblPrefix + "accountmaster where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' AND Ac_Code NOT IN(1,2,3) AND Ac_Code NOT IN(Select User_Name from tbluser WHERE User_Type='C' and Company_Code=" + company_code + ") and (Ac_Code like '%" + txtSearch.Text + "%' or Ac_Name_E like '%" + txtSearch.Text + "%') order by Ac_Code";
        qry = "Select Ac_Code,Ac_Name_E,Email_Id,Mobile_No from " + tblPrefix + "accountmaster where  Ac_Code NOT IN(1,2,3) AND Ac_Code NOT IN(Select User_Name from tbluser WHERE User_Type='C' and Company_Code=" + company_code + ") and (Ac_Code like '%" + txtSearch.Text + "%' or Ac_Name_E like '%" + txtSearch.Text + "%') order by Ac_Code";
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                grdAccounts.DataSource = dt;
                grdAccounts.DataBind();
            }
            else
            {
                grdAccounts.DataSource = null;
                grdAccounts.DataBind();
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindGrid();
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void grdAccounts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAccounts.PageIndex = e.NewPageIndex;
        this.BindGrid();
        grdAccounts.DataBind();
    }
    protected void grdAccounts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int i = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Width = new Unit("80px");
            e.Row.Cells[1].Width = new Unit("380px");
            e.Row.Cells[2].Width = new Unit("280px");
            e.Row.Cells[3].Width = new Unit("180px");
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;

            i++;
            foreach (TableCell cell in e.Row.Cells)
            {
                string s = cell.Text.ToString();
                if (cell.Text.Length > 40)
                {
                    cell.Text = cell.Text.Substring(0, 40) + "..";
                    cell.ToolTip = s;
                }
            }

        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> list = new List<string>();
            string[] arr;
            string accountcodes = string.Empty;
            string str = "";
            string qry = "";
            string Ac_Code = string.Empty;
            string Email = string.Empty;
            string Mobile = string.Empty;
            string Acc_Name = string.Empty;
            string password = string.Empty;
            #region Detail Fields
            Detail_Fields = "User_Name,";
            Detail_Fields = Detail_Fields + "Password,";
            Detail_Fields = Detail_Fields + "User_Type,";
            Detail_Fields = Detail_Fields + "Company_Code,";
            Detail_Fields = Detail_Fields + "uid,";
            Detail_Fields = Detail_Fields + "User_Id";

            #endregion
            if (btnGenerate.Text == "Create Login")
            {

                int uid = Convert.ToInt32(clsCommon.getString("select max(uid) as uid from " + tblHead + " "));

                if (uid == 0)
                {
                    uid = 0;
                }
                else
                {

                }
                int UID = Convert.ToInt32(clsCommon.getString("select max(User_Id) as User_Id from " + tblHead + " "));
                if (UID == 0)
                {
                    UID = 0;
                }
                else
                { 
                }
                if (grdAccounts.Rows.Count > 0)
                {

                    for (int i = 0; i < grdAccounts.Rows.Count; i++)
                    {
                        CheckBox chk = grdAccounts.Rows[i].FindControl("grdAccountsCB") as CheckBox;
                        if (chk != null && chk.Checked == true)
                        {
                            uid = uid + 1;
                            UID = UID + 1;
                            Ac_Code = grdAccounts.Rows[i].Cells[0].Text.ToString();
                            Acc_Name = grdAccounts.Rows[i].Cells[1].Text.ToString();
                            Email = Server.HtmlDecode(grdAccounts.Rows[i].Cells[2].Text.ToString());
                            Mobile = Server.HtmlDecode(grdAccounts.Rows[i].Cells[3].Text.ToString());
                            GeneratePassword gp = new GeneratePassword();
                            password = gp.NewPassword();
                            Detail_Values = Detail_Values + "('" + Ac_Code + "','" + password + "','C','" + Convert.ToInt32(Session["Company_Code"].ToString()) + "','" + uid + "','"+UID+"'),";
                            string Acid = Convert.ToString(clsCommon.getString("select accoid from nt_1_accountmaster where Ac_Code=" + Ac_Code + ""));
                            list.Add(Acid);
                        }
                    }
                    if (Detail_Values.Length > 0)
                    {
                        Detail_Values = Detail_Values.Remove(Detail_Values.Length - 1);
                        Detail_Insert = "insert into " + tblHead + "(" + Detail_Fields + ") values " + Detail_Values + "";
                        dr = null;
                        dr = Maindt.NewRow();
                        dr["Querys"] = Detail_Insert;
                        Maindt.Rows.Add(dr);
                    }
                    arr = list.ToArray();
                    accountcodes = string.Join(",", arr);

                    qry = "update " + tblPrefix + "accountmaster SET Is_Login=1 WHERE accoid IN (" + accountcodes + ")";
                    dr = null;
                    dr = Maindt.NewRow();
                    dr["Querys"] = qry;
                    Maindt.Rows.Add(dr);
                    int flag = 2;
                    string msg = DOPurcSaleCRUD.DOPurcSaleCRUD_OP(flag, Maindt);
                    if (msg == "Update")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Update !')", true);
                    }
                }
            }
            if (btnGenerate.Text == "Delete Login")
            {
                if (grdGeneratedAccounts.Rows.Count > 0)
                {
                    for (int i = 0; i < grdGeneratedAccounts.Rows.Count; i++)
                    {
                        CheckBox chk = grdGeneratedAccounts.Rows[i].FindControl("grdAccountsCB") as CheckBox;
                        if (chk != null && chk.Checked == true)
                        {
                            Ac_Code = grdGeneratedAccounts.Rows[i].Cells[0].Text.ToString();
                            
                            list.Add(Ac_Code);
                           
                        }
                    }
                    arr = list.ToArray();
                    accountcodes = string.Join(",", arr);
                    DataSet ds = new DataSet();
                    qry = "delete from tbluser where User_Name IN(" + accountcodes + ") and user_type='C' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                    dr = null;
                    dr = Maindt.NewRow();
                    dr["Querys"] = qry;
                    Maindt.Rows.Add(dr);

                    qry = "update " + tblPrefix + "accountmaster SET Is_Login=NULL WHERE Ac_Code IN(" + accountcodes + ")";
                    dr = null;
                    dr = Maindt.NewRow();
                    dr["Querys"] = qry;
                    Maindt.Rows.Add(dr);
                    int flag = 3;
                    string msg = DOPurcSaleCRUD.DOPurcSaleCRUD_OP(flag, Maindt);
                    if (msg == "Delete")
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Deleted !')", true);
                        Response.Redirect("../Utility/pgeGenerateCustomerLogin.aspx", false);
                    }
                }
            }
            BindGrid();
          
        }
        catch
        {
        }
    }

    protected void btnShowGeneratedAccounts_Click(object sender, EventArgs e)
    {
        if (btnShowGeneratedAccounts.Text == "Show Generated")
        {
            pnlGeneratedAccounts.Visible = true;
            pnlAccounts.Visible = false;
            btnGenerate.Text = "Delete Login";
            btnShowGeneratedAccounts.Text = "Show Not Generated";
            //qry = "Select Ac_Code,Ac_Name_E,Email_Id,Mobile_No from " + tblPrefix + "accountmaster where Company_Code=" + company_code + " AND Is_Login=1";
            qry = "Select Ac_Code,Ac_Name_E,Email_Id,Mobile_No from " + tblPrefix + "accountmaster where Is_Login=1";
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        grdGeneratedAccounts.DataSource = dt;
                        grdGeneratedAccounts.DataBind();
                    }
                    else
                    {
                        grdGeneratedAccounts.DataSource = null;
                        grdGeneratedAccounts.DataBind();
                    }
                }
            }
        }
        else
        {
            pnlGeneratedAccounts.Visible = false;
            pnlAccounts.Visible = true;
            btnGenerate.Text = "Create Login";
            btnShowGeneratedAccounts.Text = "Show Generated";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        grdAccounts.DataSource = null;
        grdGeneratedAccounts = null;
    }
}