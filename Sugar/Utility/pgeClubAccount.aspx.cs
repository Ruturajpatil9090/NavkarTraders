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

public partial class Sugar_pgeClubAccount : System.Web.UI.Page
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
    int check;
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
                hdnfpopup.Value = "1";
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
                hdnfpopup.Value = "1";
            }
            
        }
        catch
        {
        }
    }

    private void ExecuteSP(string TableName, string ColumnName, int OldAcCode, int NewAcCode, string WhereCondition, string autoid, string accoind, string Wrong_AcCode)
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

            if (hdconfirm.Value == "Yes")
            {
                try
                {
                    DataSet ds = new DataSet();

                    string[] stringArr;
                    string r = string.Empty;
                    string[] stringautid;
                    string a = string.Empty;
                    string Ac_CodeToReplace = txtac_code.Text.Trim();
                    string Wrong_AcCode = txtWrongAccoun.Text.Trim();
                    string accoid = lblaccoid.Text.Trim();
                    string WrongAccoid = lblWrongAccoid.Text.Trim();

                    //clubing from Carporate Sale Table
                    stringArr = new string[] { "ac_code", "unit_code" };
                    stringautid = new string[] { "ac", "uc" };

                    for (int i = 0; i < stringArr.Length; i++)
                    {

                        ExecuteSP("carporatehead", stringArr[i], Convert.ToInt32(Wrong_AcCode), Convert.ToInt32(Ac_CodeToReplace), "Company_Code=" +
                            Convert.ToInt32(Session["Company_Code"].ToString()), stringautid[i], accoid,Wrong_AcCode);
                    }

                    //delivery order Table
                    stringArr = new string[] { "mill_code", "voucher_by", "broker", "transport", "GETPASSCODE", "Ac_Code" };
                    stringautid = new string[] { "mc", "vb", "bk", "tc", "gp", "vb" };

                    for (int j = 0; j < stringArr.Length; j++)
                    {
                        ExecuteSP(tblPrefix + "deliveryorder", stringArr[j], Convert.ToInt32(Wrong_AcCode), Convert.ToInt32(Ac_CodeToReplace), "company_code="
                            + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()), stringautid[j], accoid, Wrong_AcCode);
                    }

                    //DO details
                    ExecuteSP(tblPrefix + "dodetails", "Bank_Code", Convert.ToInt32(Wrong_AcCode), Convert.ToInt32(Ac_CodeToReplace), "Company_Code="
                        + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()), "bc", accoid, Wrong_AcCode);

                    //GLEDGER
                    stringArr = new string[] { "AC_CODE" };
                    stringautid = new string[] { "ac" };

                    for (int k = 0; k < stringArr.Length; k++)
                    {
                        ExecuteSP(tblPrefix + "gledger", stringArr[k], Convert.ToInt32(Wrong_AcCode), Convert.ToInt32(Ac_CodeToReplace), "Company_Code=" +
                            Convert.ToInt32(Session["Company_Code"].ToString()), stringautid[k], accoid, Wrong_AcCode);

                    }

                    //PartyUnit
                    stringArr = new string[] { "Ac_Code", "Unit_code" };
                    stringautid = new string[] { "ac", "uc" };

                    for (int k = 0; k < stringArr.Length; k++)
                    {
                        ExecuteSP(tblPrefix + "partyunit", stringArr[k], Convert.ToInt32(Wrong_AcCode), Convert.ToInt32(Ac_CodeToReplace), "Company_Code=" +
                            Convert.ToInt32(Session["Company_Code"].ToString()), stringautid[k], accoid, Wrong_AcCode);
                    }


                    //Sugar Purchase
                    stringArr = new string[] { "Ac_Code", "Unit_Code", "mill_code", "BROKER" };
                    stringautid = new string[] { "ac", "uc", "mc", "bk" };

                    for (int k = 0; k < stringArr.Length; k++)
                    {

                        ExecuteSP(tblPrefix + "sugarpurchase", stringArr[k], Convert.ToInt32(Wrong_AcCode), Convert.ToInt32(Ac_CodeToReplace), "Company_Code="
                          + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()), stringautid[k], accoid, Wrong_AcCode);
                    }


                    //Sugar Purchase Return
                    stringArr = new string[] { "Ac_Code", "Unit_Code", "mill_code", "BROKER" };
                    stringautid = new string[] { "ac", "uc", "mc", "bc" };

                    for (int k = 0; k < stringArr.Length; k++)
                    {
                        ExecuteSP(tblPrefix + "sugarpurchasereturn", stringArr[k], Convert.ToInt32(Wrong_AcCode), Convert.ToInt32(Ac_CodeToReplace), "Company_Code=" +
                            Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()), stringautid[k], accoid, Wrong_AcCode);
                    }


                    //Sugar sale
                    stringArr = new string[] { "Ac_Code", "Unit_Code", "mill_code", "BROKER", "Transport_Code" };
                    stringautid = new string[] { "ac", "uc", "mc", "bk", "tc" };

                    for (int k = 0; k < stringArr.Length; k++)
                    {
                        ExecuteSP(tblPrefix + "sugarsale", stringArr[k], Convert.ToInt32(Wrong_AcCode), Convert.ToInt32(Ac_CodeToReplace), "Company_Code=" +
                            Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()), stringautid[k], accoid, Wrong_AcCode);
                    }


                    //Sugar sale return
                    stringArr = new string[] { "Ac_Code", "Unit_Code", "mill_code", "BROKER", "Transport_Code" };
                    stringautid = new string[] { "ac", "uc", "mc", "bc", "tc" };

                    for (int k = 0; k < stringArr.Length; k++)
                    {
                        ExecuteSP(tblPrefix + "sugarsalereturn", stringArr[k], Convert.ToInt32(Wrong_AcCode), Convert.ToInt32(Ac_CodeToReplace), "Company_Code=" +
                            Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()), stringautid[k], accoid, Wrong_AcCode);
                    }


                    //Tender
                    stringArr = new string[] { "Mill_Code", "Payment_To", "Tender_From", "Tender_DO", "Voucher_By", "Broker" };
                    stringautid = new string[] { "mc", "pt", "tf", "td", "vb", "bk" };

                    for (int k = 0; k < stringArr.Length; k++)
                    {
                        ExecuteSP(tblPrefix + "tender", stringArr[k], Convert.ToInt32(Wrong_AcCode), Convert.ToInt32(Ac_CodeToReplace), "Company_Code=" +
                            Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()), stringautid[k], accoid, Wrong_AcCode);
                    }

                    //TenderDetails
                    stringArr = new string[] { "Buyer", "Buyer_Party" };
                    stringautid = new string[] { "buyerid", "buyerpartyid" };

                    for (int k = 0; k < stringArr.Length; k++)
                    {
                        ExecuteSP(tblPrefix + "tenderdetails", stringArr[k], Convert.ToInt32(Wrong_AcCode), Convert.ToInt32(Ac_CodeToReplace), "Company_Code="
                            + Convert.ToInt32(Session["Company_Code"].ToString()) + " and year_code=" + Convert.ToInt32(Session["year"].ToString()), stringautid[k], accoid, Wrong_AcCode);
                    }


                    //Transact
                    stringArr = new string[] { "debit_ac", "credit_ac", "Unit_Code" };
                    stringautid = new string[] { "da", "ca", "uc" };

                    for (int k = 0; k < stringArr.Length; k++)
                    {
                        ExecuteSP(tblPrefix + "transactdetail", stringArr[k], Convert.ToInt32(Wrong_AcCode), Convert.ToInt32(Ac_CodeToReplace), "Company_Code=" +
                            Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()), stringautid[k], accoid, Wrong_AcCode);
                    }

                    //UTR
                    stringArr = new string[] { "bank_ac", "mill_code" };
                    stringautid = new string[] { "bc", "mc" };

                    for (int k = 0; k < stringArr.Length; k++)
                    {
                        ExecuteSP(tblPrefix + "utr", stringArr[k], Convert.ToInt32(Wrong_AcCode), Convert.ToInt32(Ac_CodeToReplace), "Company_Code="
                            + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()), stringautid[k], accoid, Wrong_AcCode);
                    }

                }
                catch (Exception)
                {

                    throw;
                }
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
            hdnfpopup.Value = null;


            if (strTextBox == "txtac_code")
            {
                if (txtac_code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtac_code.Text);
                    if (a == false)
                    {
                        hdnfpopup.Value = "0";
                        btntxtac_code_Click(this, new EventArgs());
                    }
                    else
                    {
                        qry = "select Ac_Name_E,accoid from " + qrycommon + " where Ac_Code='" + txtac_code.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                //txtac_code.Text = ds.Tables[0].Rows[0]["Ac_Code"].ToString();
                                lblParty_name.Text = ds.Tables[0].Rows[0]["Ac_Name_E"].ToString();
                                lblaccoid.Text = ds.Tables[0].Rows[0]["accoid"].ToString();
                                setFocusControl(txtWrongAccoun);


                            }
                        }
                    }
                }
            }
            if (strTextBox == "txtWrongAccoun")
            {
                if (txtWrongAccoun.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtWrongAccoun.Text);
                    if (a == false)
                    {
                        hdnfpopup.Value = "0";
                        btnWrongAccount_Click(this, new EventArgs());
                    }
                    else
                    {

                        qry = "select Ac_Name_E,accoid from " + qrycommon + " where Ac_Code='" + txtWrongAccoun.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                //txtac_code.Text = ds.Tables[0].Rows[0]["Ac_Code"].ToString();
                                lblWrongAccount.Text = ds.Tables[0].Rows[0]["Ac_Name_E"].ToString();
                                lblWrongAccoid.Text = ds.Tables[0].Rows[0]["accoid"].ToString();
                                setFocusControl(btnClubAccount);


                            }
                            else
                            {
                                lblWrongAccount.Text = string.Empty;
                                txtWrongAccoun.Text = string.Empty;
                                setFocusControl(btnClubAccount);
                            }
                        }


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