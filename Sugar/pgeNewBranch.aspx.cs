using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_pgeNewBranch : System.Web.UI.Page
{
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    string tblPrefix = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        user = Session["user"].ToString();
        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                drpCompany.Focus();
                fillDrpCompany();
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }
    private void fillDrpCompany()
    {
        try
        {
            string qry = "select Company_Code,Company_Name_E from Company ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ListItem li = new ListItem("---Select---", "0");

            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        drpCompany.DataSource = dt;
                        drpCompany.DataTextField = dt.Columns[1].ToString();
                        drpCompany.DataValueField = dt.Columns[0].ToString();
                        drpCompany.DataBind();
                    }
                }
            }
            drpCompany.Items.Insert(0, li);
        }
        catch
        {

        }
    }

    protected void btnAddBranch_Click(object sender, EventArgs e)
    {
        try
        {
            string retValue = "";
            int Company_Code = Convert.ToInt32(drpCompany.SelectedValue);
            string Branch = txtBranchName.Text;
            using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
            {
                obj.flag = 1;
                obj.tableName = "BranchMaster";
                obj.columnNm = "Company_Code,Branch,Created_By";
                obj.values = "'" + Company_Code + "','" + Branch + "','" + user + "'";
                DataSet ds = new DataSet();
                ds = obj.insertAccountMaster(ref retValue);
            }
            drpCompany.SelectedIndex = 0;
            txtBranchName.Text = string.Empty;
        }
        catch
        {
        }
    }

    protected void btnCancelBranch_Click(object sender, EventArgs e)
    {
        drpCompany.SelectedIndex = 0;
        txtBranchName.Text = string.Empty;

    }
}