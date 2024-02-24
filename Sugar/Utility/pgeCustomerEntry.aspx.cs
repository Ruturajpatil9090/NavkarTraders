using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sugar_Utility_pgeCustomerEntry : System.Web.UI.Page
{
    #region data section
    string qry = string.Empty;
    string qrycommon = string.Empty;
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    string cs = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string qryAccountList = string.Empty;
    string accountmasterlist = string.Empty;
    string AccountMasterTable = string.Empty;
    string searchString = string.Empty;
    int flag = 0;
    int count = 0;
    int counts = 0;
    static WebControl objAsp = null;
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    #endregion

    #region text and label data Declaration
    string Account = string.Empty;
    string WrongAccount = string.Empty;
    #endregion

    #region Head part Declaration
    string Created_By = string.Empty;
    string Modified_By = string.Empty;
    string Created_Date = string.Empty;
    string Modified_Date = string.Empty;
    string retValue = string.Empty;
    string strRev = string.Empty;

    int Company_Code = 0;
    int Year_Code = 0;
    string Head_Update = string.Empty;
    string Head_Insert = string.Empty;
    string Head_Delete = string.Empty;

    #endregion
    string customercode = string.Empty;
    int check;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);
            user = Session["user"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "";
            AccountMasterTable = tblPrefix + "AccountMaster";
            qrycommon = "qrymstaccountmaster";
            pnlPopup.Style["display"] = "none";
            customercode = Request.QueryString["CustomerCode"];

            //accountmasterlist = tblPrefix + "qryAccountsList";
            if (!Page.IsPostBack)
            {

                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    pnlPopup.Style["display"] = "none";
                    ViewState["currentTable"] = null;
                    //clsButtonNavigation.enableDisable("N");
                    // this.makeEmptyForm("N");
                    ViewState["mode"] = "I";
                    txtac_code.Text = customercode;
                    string qry = clsCommon.getString("select Client_Name from ClientRegistration   where Client_Code=" + txtac_code.Text);
                    lblParty_name.Text = qry;
                    //this.showLastRecord();
                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
            }
            if (objAsp != null)
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
            if (hdnfClosePopup.Value == "Close" || hdnfClosePopup.Value == "")
            {
                pnlPopup.Style["display"] = "none";
            }
            else
            {
                pnlPopup.Style["display"] = "block";
                objAsp = btnSearch;
            }
            setFocusControl(txtac_code);
        }
        catch
        {
        }
    }

    #region[txtac_code_TextChanged]
    protected void txtac_code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtac_code.Text;
        strTextBox = "txtac_code";
        csCalculations();

    }
    #endregion


    protected void btntxtac_code_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnfpopup.Value == "0")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtac_code";
                btnSearch_Click(sender, e);
            }
            else
            {
                pnlPopup.Style["display"] = "none";

            }
        }
        catch
        {
        }
    }
    #region[txtWrongAccoun_TextChanged]
    protected void txtWrongAccoun_TextChanged(object sender, EventArgs e)
    {

        searchString = txtWrongAccoun.Text;
        strTextBox = "txtWrongAccoun";
        csCalculations();

    }
    #endregion
    protected void btnWrongAccount_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnfpopup.Value == "0")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtWrongAccoun";
                btnSearch_Click(sender, e);
            }
            else
            {
                pnlPopup.Style["display"] = "none";

            }
        }
        catch
        {
        }
    }

    private void ExecuteSP(string TableName, string ColumnName, int OldAcCode, int NewAcCode, string WhereCondition, string autoid, string accoind)
    {

        Head_Update = "update " + TableName + " set " + ColumnName + "=" + NewAcCode + " , " + autoid + "=" + accoind +
            " where " + WhereCondition + " and " + ColumnName + "=" + OldAcCode;
        DataSet ds = clsDAL.SimpleQuery(Head_Update);
    }

    protected void btnClubAccount_Click(object sender, EventArgs e)
    {
        if (hdnfpopup.Value != "0")
        {

            if (txtac_code.Text == string.Empty)
            {
                setFocusControl(txtac_code);
                return;
            }

            if (txtWrongAccoun.Text == string.Empty)
            {
                setFocusControl(txtWrongAccoun);
                return;
            }


            try
            {
                string accode = txtac_code.Text;
                string updateaccode = txtWrongAccoun.Text;

                string accoid = clsCommon.getString("select accoid from NT_1_AccountMaster where Ac_code=" + updateaccode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                string update = clsCommon.getString("update ClientRegistration set accoid=" +accoid+" where Client_Code="+accode);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Client Updated Sucessfully!!!')", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Account", "javascript:Accmaster('" + accoid + "','1');", true);

                hdnfpopup.Value = "0";
            }
            catch (Exception)
            {

                throw;
            }

        }
        else
        {
            check = 1;
            hdnfpopup.Value = "1";
        }
    }
    protected void imgBtnClose_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (hdnfClosePopup.Value == "txtac_code")
            {
                setFocusControl(txtac_code);
            }
            if (hdnfClosePopup.Value == "txtWrongAccoun")
            {
                setFocusControl(txtWrongAccoun);
            }
            hdnfClosePopup.Value = "Close";
            pnlPopup.Style["display"] = "none";
            txtSearchText.Text = string.Empty;
            grdPopup.DataSource = null;
            grdPopup.DataBind();
        }
        catch
        {
        }
    }

    #region csCalculations
    private void csCalculations()
    {
        try
        {
            //hdnfpopup.Value = null;


            if (strTextBox == "txtac_code")
            {
                if (txtac_code.Text != string.Empty)
                {
                    qry = "select Client_Name  from ClientRegistration   where Client_Code=" + txtac_code.Text;

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            //txtac_code.Text = ds.Tables[0].Rows[0]["Ac_Code"].ToString();
                            lblParty_name.Text = ds.Tables[0].Rows[0]["Client_Name"].ToString();
                            // lblaccoid.Text = ds.Tables[0].Rows[0]["accoid"].ToString();
                            setFocusControl(txtWrongAccoun);


                        }
                    }
                }
            }

            if (strTextBox == "txtWrongAccoun")
            {
                if (txtWrongAccoun.Text != string.Empty)
                {
                    string str = clsCommon.getString("select Ac_Name_E from " + qrycommon
                        + " where Ac_Code=" + txtWrongAccoun.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty)
                    {
                        lblWrongAccount.Text = str;
                        check = 0;
                        setFocusControl(btnClubAccount);
                    }
                    else
                    {
                        lblWrongAccount.Text = string.Empty;
                        txtWrongAccoun.Text = string.Empty;
                        setFocusControl(btnClubAccount);
                    }
                }
                else
                {
                    lblWrongAccount.Text = string.Empty;
                    txtWrongAccoun.Text = string.Empty;
                    setFocusControl(btnClubAccount);
                }
            }


        }
        catch
        {
        }
    }
    #endregion



    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (hdnfClosePopup.Value == "Close")
            {
                txtSearchText.Text = string.Empty;
                pnlPopup.Style["display"] = "none";
                grdPopup.DataSource = null;
                grdPopup.DataBind();
                if (objAsp != null)
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
            }
            else
            {
                pnlPopup.Style["display"] = "block";

                searchString = txtSearchText.Text;
                strTextBox = hdnfClosePopup.Value;

                setFocusControl(btnSearch);
            }
        }
        catch
        {
        }
    }
    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (searchString != string.Empty)
            {
                txtSearchText.Text = searchString;
            }
            string searchtxt = searchString;
            string delimStr = "";
            char[] delimiter = delimStr.ToCharArray();
            string words = searchString;
            string[] split = null;
            string name = string.Empty;
            if (searchString != string.Empty && strTextBox == hdnfClosePopup.Value)
            {
                txtSearchText.Text = searchString;
                searchtxt = txtSearchText.Text;
                words = txtSearchText.Text;
                split = words.Split(delimiter);
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
                searchtxt = txtSearchText.Text;
                words = txtSearchText.Text;
                split = words.Split(delimiter);
            }

            if (hdnfClosePopup.Value == "txtac_code" || hdnfClosePopup.Value == "txtWrongAccoun")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or cityname like '%" + aa + "%' or accoid like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Party--";
                string qry = "select Ac_Code,Ac_Name_E,CityName,accoid from " + qrycommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    "  and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' or accoid like '%" + txtSearchText.Text + "%' )";
                this.showPopup(qry);
            }

        }
        catch
        {

        }
    }

    private void showPopup(string qry)
    {
        try
        {
            setFocusControl(txtSearchText);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        grdPopup.DataSource = dt;
                        grdPopup.DataBind();

                        hdHelpPageCount.Value = grdPopup.PageCount.ToString();
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = "0";
                    }
                }
            }
        }
        catch
        {
        }
    }
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }
    protected void grdPopup_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
            (e.Row.RowState == DataControlRowState.Normal ||
           e.Row.RowState == DataControlRowState.Alternate))
            {
                e.Row.TabIndex = -1;
                e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
                e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
                e.Row.Attributes["onselectstart"] = "javascript:return false;";
                // e.Row.Attributes["onkeyup"] = "javascript:return selectRow(event);";
            }
        }
        catch
        {
            throw;
        }

    }
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((v != "txtac_code") || (v != "txtWrongAccoun"))
            {
                e.Row.Cells[0].Width = new Unit("60px");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].Width = new Unit("80px");
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].Width = new Unit("80px");
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].Width = new Unit("80px");
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }
}