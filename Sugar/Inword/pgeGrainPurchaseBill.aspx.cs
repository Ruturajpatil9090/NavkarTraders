using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;

public partial class Sugar_Inword_pgeGrainPurchaseBill : System.Web.UI.Page
{
    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string SystemMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string qryHead = string.Empty;
    string qryDetail = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    int defaultAccountCode = 0;
    string trntype = "SB";
    string user = string.Empty;
    string qryAccountList = string.Empty;
    string GLedgerTable = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string Action = string.Empty;
    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;

    int Doc_No = 0;
    int PS_Id = 0;
    private static DataTable dtAccountMaster = null;
    private static DataTable dtItemMaster = null;
    private static DataTable dtBrandMaster = null;
    public static SqlConnection con = null;
    public static SqlCommand cmd = null;
    public static SqlTransaction myTran = null;
    string cs = string.Empty;

    string GLEDGER_Delete = string.Empty;
    string Head_Delete = string.Empty;
    int flag = 0;
    string msg = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "Purchase_Head";
            tblDetails = "Purchase_Detail";
            AccountMasterTable = tblPrefix + "AccountMaster";
            SystemMasterTable = tblPrefix + "SystemMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            qryCommon = "qryGrainpurchaseHeadDetail";
            qryHead = "";
            qryDetail = "";
            qryAccountList = "qrymstaccountmaster";
            GLedgerTable = tblPrefix + "GLEDGER";
            pnlPopup.Style["display"] = "none";
            user = Session["user"].ToString();
            Maindt = new DataTable();
            dr = null;
            Maindt.Columns.Add("Querys", typeof(string));
            dr = Maindt.NewRow();
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);
            if (!Page.IsPostBack)
            {
                hdnfyearcode.Value = Session["year"].ToString();
                hdnfcompanycode.Value = Session["Company_Code"].ToString();
                hdnfCash_Credit.Value = drpCashCredit.SelectedValue;
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    pnlPopup.Style["display"] = "none";
                    ViewState["currentTable"] = null;
                    clsButtonNavigation.enableDisable("N");
                    this.makeEmptyForm("N");
                    ViewState["mode"] = "I";
                    this.NextNumber();
                    hdnf.Value = lblSale_Id.Text;
                    // hdnf.Value = Session["AW_No"].ToString();
                    //trntype = Session["RP_TYPE"].ToString();
                    //   drpTrnType.SelectedValue = Session["RP_TYPE"].ToString();
                    //Session["AW_No"] = null;
                    //Session["RP_TYPE"] = null;
                    string qrydis = getDisplayQuery();
                    this.fetchRecord(qrydis);
                    this.enableDisableNavigateButtons();

                   
                    string qry = "select * from qrymstaccountmaster where  Company_Code=" + Session["Company_Code"].ToString();
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    ds = clsDAL.SimpleQuery(qry);
                    dt = ds.Tables[0];
                    dtAccountMaster = dt;

                    string qry1 = "select * from " + SystemMasterTable + " where System_Type='I' and  Company_Code=" + Session["Company_Code"].ToString();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    ds1 = clsDAL.SimpleQuery(qry1);
                    dt1 = ds1.Tables[0];
                    dtItemMaster = dt1;


                    string qry11 = "select * from Brand_Master where  Company_Code=" + Session["Company_Code"].ToString();
                    DataSet ds11 = new DataSet();
                    DataTable dt11 = new DataTable();
                    ds11 = clsDAL.SimpleQuery(qry11);
                    dt11 = ds11.Tables[0];
                    dtBrandMaster = dt11;


                    Action = Request.QueryString["Action"];
                    hdnfCash_Credit.Value = Request.QueryString["Cash_Credit"];
                    if (Action == "1")
                    {

                        hdnf.Value = Request.QueryString["PS_Id"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");

                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        this.showLastRecord();
                        //this.enableDisableNavigateButtons();
                        setFocusControl(btnEdit);

                    }
                    else
                    {
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        this.NextNumber();

                        drpCashCredit.SelectedValue = hdnfCash_Credit.Value;

                        setFocusControl(txtAc_Code);


                    }


                    #region oldcode comment

                    #endregion
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

        }
        catch
        {
        }

    }
    #region [getMaxCode]
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                obj.code = "Doc_No";
                ds = new DataSet();
                ds = obj.getMaxCode();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ViewState["mode"] != null)
                            {
                                if (ViewState["mode"].ToString() == "I")
                                {
                                    txtDoc_No.Text = ds.Tables[0].Rows[0][0].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [makeEmptyForm]
    private void makeEmptyForm(string dAction)
    {
        try
        {
            if (dAction == "N")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                    if (c is System.Web.UI.WebControls.Label)
                    {
                        ((System.Web.UI.WebControls.Label)c).Text = "";
                    }
                }
                pnlPopup.Style["display"] = "none";

                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                #region logic
                lblPartyName.Text = "";
                lblBrokerName.Text = "";
                btntxtAc_Code.Enabled = false;
                btntxtBroker.Enabled = false;
                btntxtDoc_No.Enabled = false;
                calenderExtenderDate.Enabled = false;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = false;
                txtItem_Code.Enabled = false;
                btntxtItem_Code.Enabled = false;
                txtQty.Enabled = false;
                txtWt_Per.Enabled = false;
                txtWt_Qty.Enabled = false;
                txtRate.Enabled = false;
                txtValue.Enabled = false;
                txtGST_Code.Enabled = false;
                btntxtGSTRateCode.Enabled = false;
                txtSGST.Enabled = false;
                txtCGST.Enabled = false;
                txtIGST.Enabled = false;
                txtHamali_Rate.Enabled = false;
                txtHamali.Enabled = false;
                #endregion

                txtItem_Code.Enabled = false;
                btntxtItem_Code.Enabled = false;
                txtQty.Enabled = false;
                txtWt_Per.Enabled = false;
                txtWt_Qty.Enabled = false;
                txtRate.Enabled = false;
                txtValue.Enabled = false;
                txtGST_Code.Enabled = false;

                txtSGST.Enabled = false;
                txtCGST.Enabled = false;
                txtIGST.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                txtBrand_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;
                txtHamali_Rate.Enabled = false;
                txtHamali.Enabled = false;

            }
            if (dAction == "A")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Change No";
                btntxtDoc_No.Enabled = false;
                txtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = false;
                #region set Business logic for save
                lblPartyName.Text = "";
                lblBrokerName.Text = "";
                btntxtAc_Code.Enabled = true;
                btntxtBroker.Enabled = true;
                txtSGST.Enabled = true;
                txtCGST.Enabled = true;
                txtIGST.Enabled = true;
                calenderExtenderDate.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = true;
                txtItem_Code.Enabled = true;
                btntxtItem_Code.Enabled = true;
                txtQty.Enabled = true;
                txtWt_Per.Enabled = true;
                txtWt_Qty.Enabled = true;
                txtRate.Enabled = true;
                txtValue.Enabled = true;
                txtGST_Code.Enabled = true;

                txtSGST.Enabled = true;
                txtCGST.Enabled = true;
                txtIGST.Enabled = true;
                txtHamali_Rate.Enabled = true;
                txtHamali.Enabled = true;
                #endregion

                txtItem_Code.Enabled = true;
                btntxtItem_Code.Enabled = true;
                txtQty.Enabled = true;
                txtWt_Per.Enabled = true;
                txtWt_Qty.Enabled = true;
                txtRate.Enabled = true;
                txtValue.Enabled = true;
                txtGST_Code.Enabled = true;
                btntxtGSTRateCode.Enabled = true;
                txtSGST.Enabled = true;
                txtCGST.Enabled = true;
                txtIGST.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                txtHamali_Rate.Enabled = true;
                txtTCS_Par.Text = Session["TCSRate"].ToString();
                txtBrand_Code.Enabled = true;
                btntxtBrand_Code.Enabled = true;
                txtHamali.Enabled = true;


                string docdate = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                DateTime dEnd = DateTime.Parse(docdate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                DateTime ss = DateTime.Parse("2021/07/01", System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

                if (dEnd >= ss)
                {
                    txtTCS_Par.Text = "0.00";
                    txtTDS.Text = Session["PurchaseTDSRate"].ToString(); ;

                }
                else
                {
                    txtTCS_Par.Text = Session["TCSRate"].ToString();
                    txtTDS.Text = "0.00";

                }

            }
            if (dAction == "S")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                }
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                pnlgrdDetail.Enabled = false;

                #region logic
                btntxtAc_Code.Enabled = false;
                btntxtBroker.Enabled = false;
                calenderExtenderDate.Enabled = false;
                txtItem_Code.Enabled = false;
                btntxtItem_Code.Enabled = false;
                txtQty.Enabled = false;
                txtWt_Per.Enabled = false;
                txtWt_Qty.Enabled = false;
                txtRate.Enabled = false;
                txtValue.Enabled = false;
                txtGST_Code.Enabled = false;

                txtSGST.Enabled = false;
                txtCGST.Enabled = false;
                txtIGST.Enabled = false;
                txtHamali_Rate.Enabled = false;
                txtHamali.Enabled = false;
                #endregion

                txtItem_Code.Enabled = false;
                btntxtItem_Code.Enabled = false;
                txtQty.Enabled = false;
                txtWt_Per.Enabled = false;
                txtWt_Qty.Enabled = false;
                txtRate.Enabled = false;
                txtValue.Enabled = false;
                txtGST_Code.Enabled = false;
                btntxtGSTRateCode.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                txtBrand_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;
                txtSGST.Enabled = false;
                txtCGST.Enabled = false;
                txtIGST.Enabled = false;
                txtHamali_Rate.Enabled = false;
                txtHamali.Enabled = false;
                txtSGST.Text = "";
                txtCGST.Text = "";
                txtIGST.Text = "";
            }
            if (dAction == "E")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                txtDoc_No.Enabled = true;
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                lblMsg.Text = string.Empty;
                pnlgrdDetail.Enabled = true;

                txtCGST.Text = "";
                txtSGST.Text = "";
                txtIGST.Text = "";
                #region logic
                btntxtAc_Code.Enabled = true;
                btntxtBroker.Enabled = true;
                calenderExtenderDate.Enabled = true;
                txtItem_Code.Enabled = true;
                btntxtItem_Code.Enabled = true;
                txtQty.Enabled = true;
                txtWt_Per.Enabled = true;
                txtWt_Qty.Enabled = true;
                txtRate.Enabled = true;
                txtValue.Enabled = true;
                txtGST_Code.Enabled = true;

                txtHamali_Rate.Enabled = true;
                txtHamali.Enabled = true;
                #endregion

                txtItem_Code.Enabled = true;
                btntxtItem_Code.Enabled = true;
                txtQty.Enabled = true;
                txtWt_Per.Enabled = true;
                txtWt_Qty.Enabled = true;
                txtRate.Enabled = true;
                txtValue.Enabled = true;
                txtGST_Code.Enabled = true;
                btntxtGSTRateCode.Enabled = true;
                txtCGST.Enabled = true;
                txtSGST.Enabled = true;
                txtIGST.Enabled = true;

                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                txtBrand_Code.Enabled = true;
                btntxtBrand_Code.Enabled = true;
                txtHamali_Rate.Enabled = true;
                txtHamali.Enabled = true;
            }
            #region Always check this
            #endregion
        }
        catch
        {
        }
    }
    #endregion

    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion

    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select count(Doc_No) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                " and Year_Code='" + Session["year"].ToString() + "' and Cash_Credit='" + hdnfCash_Credit.Value + "' "));
            if (counts == 0)
            {
                txtDoc_No.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(Doc_No) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                    " and Year_Code='" + Session["year"].ToString() + "'  and Cash_Credit='" + hdnfCash_Credit.Value + "' ")) + 1;
                txtDoc_No.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(PS_Id) as PS_Id from " + tblHead + " "));
            if (counts == 0)
            {
                lblSale_Id.Text = "1";
                PS_Id = 1;
            }
            else
            {
                PS_Id = Convert.ToInt32(clsCommon.getString("SELECT max(PS_Id) as PS_Id from " + tblHead));
                lblSale_Id.Text = PS_Id.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }
    #endregion

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        hdnfCash_Credit.Value = drpCashCredit.SelectedValue;
        this.getMaxCode();
        txtDoc_No.Enabled = false;

        string gstname = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=1  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        this.NextNumber();
        setFocusControl(txtDoc_Date);
    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        pnlgrdDetail.Enabled = true;
        this.makeEmptyForm("E");
        txtDoc_No.Enabled = false;
        setFocusControl(txtItem_Code);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    if (hdconfirm.Value == "Yes")
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "system", "javascript:DeleteConform();", true);
        //    }
        //}
        //catch
        //{

        //}
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                string currentDoc_No = lblSale_Id.Text;
                string Cash_Credit = drpCashCredit.SelectedValue;
                Head_Delete = "delete from " + tblHead + " where PS_Id='" + currentDoc_No + "' and Cash_Credit='" + Cash_Credit + "'";

                string Detail_Deleteqry = "delete from " + tblDetails + " where PS_Id='" + currentDoc_No + "' and Cash_Credit='" + Cash_Credit + "'";

                GLEDGER_Delete = "delete from nt_1_gledger where TRAN_TYPE='PG'  and CASHCREDIT='" + Cash_Credit + "' and Doc_No=" + txtDoc_No.Text + " " +
                    " and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " +
                    " Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";



                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = GLEDGER_Delete;
                Maindt.Rows.Add(dr);

                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = Detail_Deleteqry;
                Maindt.Rows.Add(dr);

                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = Head_Delete;
                Maindt.Rows.Add(dr);



                msg = DOPurcSaleCRUD.DOPurcSaleCRUD_OP(flag, Maindt);

                if (msg == "Delete")
                {
                    Response.Redirect("../Inword/pgeGrainPurchaseBillUtility.aspx");
                }
            }
        }

        catch
        {
        }
    }
    #endregion

    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        hdnf.Value = Request.QueryString["PS_Id"];
        if (hdnf.Value == "0" || hdnf.Value == "")
        {
            int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(PS_Id),0) as PS_Id from Purchase_Head where Cash_Credit='" + drpCashCredit.SelectedValue + "' and Year_Code=" + Session["year"] + " and Company_Code=" + Session["Company_Code"]));

            hdnf.Value = Convert.ToString(maxno);
            hdnfCash_Credit.Value = drpCashCredit.SelectedValue;
        }
        //Response.Redirect("pgeGrainPurchaseBill.aspx");
        Response.Redirect("pgeGrainPurchaseBill.aspx?PS_Id=" + hdnf.Value + "&Action=" + 1 + "&Cash_Credit=" + hdnfCash_Credit.Value);


    }
    #endregion

    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (hdnfyearcode.Value != Session["year"].ToString())
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records year code & current year code is not same!')", true);
            return;

        }
        if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
            return;

        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Purchase", "javascript:pagevalidation();", true);


    }

    #endregion

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtsort = new DataTable();
            dtsort = dtAccountMaster;

            DataTable dtsortItem = new DataTable();
            dtsortItem = dtItemMaster;

            DataTable dtsortBrand = new DataTable();
            dtsortBrand = dtBrandMaster;
            if (searchString != string.Empty)
            {
                txtSearchText.Text = searchString;
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
            }
            if (hdnfClosePopup.Value == "txtDoc_No" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                if (btntxtDoc_No.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtDoc_No.Text = string.Empty;
                    txtDoc_No.Enabled = true;

                    btnSave.Enabled = false;
                    setFocusControl(txtDoc_No);
                    hdnfClosePopup.Value = "Close";
                }
                if (btntxtDoc_No.Text == "Choose No")
                {
                    lblPopupHead.Text = "--Select DOC No--";
                    string qry = "select doc_no,doc_date,PartyName,PartyCity from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                        " and (doc_no like '%" + txtSearchText.Text + "%' or doc_date like '%" + txtSearchText.Text + "%' or PartyName like '%" + txtSearchText.Text + "%' or PartyCity like '%" + txtSearchText.Text + "%')";
                    this.showPopup(qry);
                }
            }

            if (hdnfClosePopup.Value == "txtAc_Code")
            {
                lblPopupHead.Text = "--Select Party Code--";
                //string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                //this.showPopup(qry);
                DataRow[] row = dtsort.Select("Convert(Ac_Code,'System.String') like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text +
                   "%' or CityName like '%" + txtSearchText.Text + "%' and Locked=0 ");
                if (row != null && row.Length > 0)
                {
                    dtsort = row.AsEnumerable().CopyToDataTable();
                }
                DataView view1 = new DataView(dtsort);
                DataTable dtAcData = view1.ToTable(true, "Ac_Code", "Ac_Name_E", "CityName");
                if (dtAcData.Rows.Count > 0)
                {
                    grdPopup.DataSource = dtAcData;
                    grdPopup.DataBind();
                    hdHelpPageCount.Value = grdPopup.PageCount.ToString();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "kj", "javascript:SelectRow(0, {0});", true);
                    //grdPopup.Rows[0].Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});",grdPopup.Rows[0].RowIndex);
                }
                else
                {
                    grdPopup.DataSource = null;
                    grdPopup.DataBind();
                    hdHelpPageCount.Value = "0";
                }
                setFocusControl(txtSearchText);
            }



            if (hdnfClosePopup.Value == "txtBroker")
            {
                lblPopupHead.Text = "--Select Broker--";

                //string qry = "select Ac_Code,Ac_Name_E,cityname from " + qryAccountList + "  where Locked=0 and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%')  and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' order by Ac_Name_E";
                //this.showPopup(qry);
                DataRow[] row = dtsort.Select("Convert(Ac_Code,'System.String') like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text +
                   "%' or CityName like '%" + txtSearchText.Text + "%' and Locked=0 ");
                if (row != null && row.Length > 0)
                {
                    dtsort = row.AsEnumerable().CopyToDataTable();
                }
                DataView view1 = new DataView(dtsort);
                DataTable dtAcData = view1.ToTable(true, "Ac_Code", "Ac_Name_E", "CityName");
                if (dtAcData.Rows.Count > 0)
                {
                    grdPopup.DataSource = dtAcData;
                    grdPopup.DataBind();
                    hdHelpPageCount.Value = grdPopup.PageCount.ToString();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "kj", "javascript:SelectRow(0, {0});", true);
                    //grdPopup.Rows[0].Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});",grdPopup.Rows[0].RowIndex);
                }
                else
                {
                    grdPopup.DataSource = null;
                    grdPopup.DataBind();
                    hdHelpPageCount.Value = "0";
                }
                setFocusControl(txtSearchText);
            }
            if (hdnfClosePopup.Value == "txtItem_Code")
            {
                lblPopupHead.Text = "--Select Item--";
                //string qry = "select System_Code,System_Name_E as Item_Name from " + SystemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                //this.showPopup(qry);
                DataRow[] row = dtsortItem.Select("Convert(System_Code,'System.String') like '%" + txtSearchText.Text + "%' or System_Name_E like '%" + txtSearchText.Text +
                  "%'");
                if (row != null && row.Length > 0)
                {
                    dtsortItem = row.AsEnumerable().CopyToDataTable();
                }
                DataView view1 = new DataView(dtsortItem);
                DataTable dtAcData = view1.ToTable(true, "System_Code", "System_Name_E");
                if (dtAcData.Rows.Count > 0)
                {
                    grdPopup.DataSource = dtAcData;
                    grdPopup.DataBind();
                    hdHelpPageCount.Value = grdPopup.PageCount.ToString();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "kj", "javascript:SelectRow(0, {0});", true);
                    //grdPopup.Rows[0].Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});",grdPopup.Rows[0].RowIndex);
                }
                else
                {
                    grdPopup.DataSource = null;
                    grdPopup.DataBind();
                    hdHelpPageCount.Value = "0";
                }
                setFocusControl(txtSearchText);
            }

            if (hdnfClosePopup.Value == "txtGST_Code")
            {
                lblPopupHead.Text = "--Select Item--";
                string qry = "select Doc_no,GST_Name,Rate from " + tblPrefix + "GSTRateMaster where ( Doc_no like '%" + txtSearchText.Text + "%' or GST_Name like '%" + txtSearchText.Text + "%' or Rate like '%" + txtSearchText.Text + "%') order by GST_Name"; ;
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBrand_Code")
            {
                lblPopupHead.Text = "--Select Item--";
                //string qry = "select Code AS Brand_code,English_Name as Brand_Name from Brand_Master where ( Code like '%" + txtSearchText.Text + "%' or English_Name like '%" + txtSearchText.Text + "%' ) and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
                //this.showPopup(qry);

                DataRow[] row = dtBrandMaster.Select("Convert(Code,'System.String') like '%" + txtSearchText.Text + "%' or Marka like '%" + txtSearchText.Text +
                  "%' and Convert(Mal_Code,'System.String')=" + txtItem_Code.Text);
                if (row != null && row.Length > 0)
                {
                    dtsortBrand = row.AsEnumerable().CopyToDataTable();
                }

                DataTable results = dtsortBrand.Select("Mal_Code =" + txtItem_Code.Text).CopyToDataTable();
                //  acname = results.Rows[0]["Ac_Name_E"].ToString();
                DataView view1 = new DataView(results);
                DataTable dtAcData = view1.ToTable(true, "Code", "Marka", "Mal_Code");
                if (dtAcData.Rows.Count > 0)
                {
                    grdPopup.DataSource = dtAcData;
                    grdPopup.DataBind();
                    hdHelpPageCount.Value = grdPopup.PageCount.ToString();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "kj", "javascript:SelectRow(0, {0});", true);
                    //grdPopup.Rows[0].Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});",grdPopup.Rows[0].RowIndex);
                }
                else
                {
                    grdPopup.DataSource = null;
                    grdPopup.DataBind();
                    hdHelpPageCount.Value = "0";
                }
                setFocusControl(txtSearchText);
            }

        }
        catch
        {
        }
    }
    #endregion

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {

            string qryDisplay = "select * from " + qryCommon + " where PS_Id=" + hdnf.Value + " ";
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion

    #region [showLastRecord]
    private void showLastRecord()
    {
        try
        {
            string qry = string.Empty;
            qry = getDisplayQuery();
            bool recordExist = this.fetchRecord(qry);
            if (recordExist == true)
            {
                btnAdd.Focus();
            }
            else                     //new code
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        query = "select count(*) from " + tblHead + " where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(query);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    RecordCount = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
        }
        if (RecordCount != 0 && RecordCount == 1)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
        }
        else if (RecordCount != 0 && RecordCount > 1)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = true;
        }
        if (txtDoc_No.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [Doc_No] from " + tblHead +
                    " where PS_Id>" + Convert.ToInt32(hdnf.Value) +
                    " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  ORDER BY Doc_No asc  ";
                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //next record exist
                            btnNext.Enabled = true;
                            btnLast.Enabled = true;
                        }
                        else
                        {
                            //next record does not exist
                            btnNext.Enabled = false;
                            btnLast.Enabled = false;
                        }
                    }
                }
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where PS_Id<" + Convert.ToInt32(hdnf.Value) + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  ORDER BY Doc_No asc  ";
                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //previous record exist
                            btnPrevious.Enabled = true;
                            btnFirst.Enabled = true;
                        }
                        else
                        {
                            btnPrevious.Enabled = false;
                            btnFirst.Enabled = false;
                        }
                    }
                }
                #endregion
            }
        }

        #endregion
    }
    #endregion

    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        string query = "";
        query = "select PS_Id from " + tblHead + " where PS_Id=(select MIN(PS_Id) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
            " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
        hdnf.Value = clsCommon.getString(query);
        navigateRecord();

    }
    #endregion

    #region [Previous]
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDoc_No.Text != string.Empty)
            {
                string query = "SELECT top 1 [PS_Id] from " + tblHead + " where PS_Id<" + Convert.ToInt32(hdnf.Value) +
                            "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY PS_Id DESC  ";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [Next]
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDoc_No.Text != string.Empty)
            {
                string query = "SELECT top 1 [PS_Id] from " + tblHead + " where PS_Id>" + Convert.ToInt32(hdnf.Value) +
                            "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY PS_Id asc  ";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
            }
        }
        catch
        {
        }
    }
    #endregion

    #region navigateRecord
    private void navigateRecord()
    {
        try
        {
            if (hdnf.Value != string.Empty)
            {
                ViewState["mode"] = "U";
                //txtDoc_No.Text = hdnf.Value;
                hdnfSuffix.Value = "";
                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                this.enableDisableNavigateButtons();
                this.makeEmptyForm("S");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Focus();
                }

            }
            else
            {
                showLastRecord();
            }
        }
        catch
        {

        }
    }
    #endregion

    #region [Last]
    protected void btnLast_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select PS_Id from " + tblHead + " where PS_Id=(select MAX(PS_Id) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();

        }
        catch
        {
        }
    }
    #endregion

    #region [fetchrecord]
    private bool fetchRecord(string qry)
    {
        try
        {
            bool recordExist = false;
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
                        string yearcode = Session["year"].ToString();
                        if (hdnfyearcode.Value != yearcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records year code & current year code is not same !')", true);
                            return false;
                        }
                        string comcode = Session["Company_Code"].ToString();
                        if (hdnfcompanycode.Value != comcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same !')", true);
                            return false;
                        }
                        drpCashCredit.Text = dt.Rows[0]["Cash_Credit"].ToString();
                        hdnfyearcode.Value = dt.Rows[0]["Year_Code"].ToString();
                        hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();
                        txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();
                        hdnf.Value = dt.Rows[0]["PS_Id"].ToString();
                        hdnfPSdoc.Value = txtDoc_No.Text;
                        lblSale_Id.Text = hdnf.Value;
                        hdnfPS_Id.Value = lblSale_Id.Text;
                        txtDoc_Date.Text = dt.Rows[0]["doc_dateConverted"].ToString();
                        txtAc_Code.Text = dt.Rows[0]["Ac_Code"].ToString();
                        lblPartyName.Text = dt.Rows[0]["partyname"].ToString();
                        txtBroker.Text = dt.Rows[0]["Broker"].ToString();
                        lblBrokerName.Text = dt.Rows[0]["brokername"].ToString();
                        txtLR_No.Text = dt.Rows[0]["LR_No"].ToString();
                        txtTruck_No.Text = dt.Rows[0]["Truck_No"].ToString();

                        txtCGST_Amount.Text = dt.Rows[0]["CGST_Amount"].ToString();

                        txtSGST_Amount.Text = dt.Rows[0]["SGST_Amount"].ToString();

                        txtIGST_Amount.Text = dt.Rows[0]["IGST_Amount"].ToString();
                        txtTaxable_Amount.Text = dt.Rows[0]["Taxable_Amount"].ToString();
                        txtHamaliAmount.Text = dt.Rows[0]["Hamali"].ToString();
                        txtpostage.Text = dt.Rows[0]["postage"].ToString();
                        txtAmount.Text = dt.Rows[0]["Amount"].ToString();


                        txtTCS_Par.Text = dt.Rows[0]["TCS_Par"].ToString();
                        txtTCS_Amount.Text = dt.Rows[0]["TCS_Amount"].ToString();
                        txtbillNo.Text = dt.Rows[0]["Bill_No"].ToString();
                        hdnfac.Value = dt.Rows[0]["ac"].ToString();
                        hdnfbc.Value = dt.Rows[0]["bc"].ToString();
                        txtTDS.Text = dt.Rows[0]["TDS_Rate"].ToString();
                        txtTDSAmt.Text = dt.Rows[0]["TDS_Amt"].ToString();

                        Label lblCreated = (Label)Master.FindControl("MasterlblCreatedBy");
                        Label lblModified = (Label)Master.FindControl("MasterlblModifiedBy");

                        if (lblCreated != null)
                        {
                            lblCreated.Text = "Created By: " + dt.Rows[0]["Created_By"].ToString();
                        }
                        if (lblModified != null)
                        {
                            lblModified.Text = "Modified By: " + dt.Rows[0]["Modified_By"].ToString();
                        }

                        string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");


                        recordExist = true;
                        lblMsg.Text = "";

                        #region  Details

                        //qry = "select ID ,Item_Code,item_Name as item_Name,Brand_Code,Brand_Name,Qty,Wt_Per,Wt_Qty,Rate,Value,GST_Code,GST_Rate,CGST,SGST,IGST,Hamali_Rate,Hamaliamt as Hamali,PSDetail_Id,'' as rowAction,'' as SrNo,ic from " + qryCommon + " where PS_Id=" + hdnf.Value;
                        qry = "select ID ,Item_Code,item_Name as item_Name,Brand_Code,Brand_Name,Qty,Wt_Per,Wt_Qty,Rate,Value,GST_Code,GST_Rate,CGST,SGST,IGST,isnull(Hamali_Rate,0) as Hamali_Rate,isnull(Hamaliamt,0) as Hamali,PSDetail_Id,'' as rowAction,'' as SrNo,ic,purcac,pac from " + qryCommon + " where PS_Id=" + hdnf.Value;
                        
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {

                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        dt.Rows[i]["rowAction"] = "N";
                                        dt.Rows[i]["SrNo"] = i + 1;
                                    }
                                    grdDetail.DataSource = dt;
                                    grdDetail.DataBind();
                                    ViewState["currentTable"] = dt;
                                }
                                else
                                {
                                    grdDetail.DataSource = null;
                                    grdDetail.DataBind();
                                    ViewState["currentTable"] = null;
                                }
                            }
                            else
                            {
                                grdDetail.DataSource = null;
                                grdDetail.DataBind();
                                ViewState["currentTable"] = null;
                            }
                        }
                        else
                        {
                            grdDetail.DataSource = null;
                            grdDetail.DataBind();
                            ViewState["currentTable"] = null;
                        }
                        #endregion

                        var item = "";
                        if (grdDetail.Rows[0].Cells[3].Text == "")
                        {
                            item = "0";
                        }
                        else
                        {
                            item = grdDetail.Rows[0].Cells[3].Text;
                        }
                        hdnfPurchaseAc.Value = clsCommon.getString("select Purchase_AC from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
                        hdnfPurchaseAcid.Value = clsCommon.getString("select PurcAcid from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
                        pnlgrdDetail.Enabled = false;
                    }
                    else
                    {
                        grdDetail.DataSource = null;
                        grdDetail.DataBind();
                        ViewState["currentTable"] = null;
                    }
                }

            }

            return recordExist;

        }
        catch
        {
            return false;
        }
        // csCalculations();
    }
    #endregion

    #region [btnOpenDetailsPopup_Click]

    #endregion

    #region [btnAdddetails_Click]
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        try
        {
            bool isValidated = true;
            if (txtRate.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtRate);
                return;
            }
            int rowIndex = 1;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();
            if (ViewState["currentTable"] != null)
            {
                dt = (DataTable)ViewState["currentTable"];
                if (dt.Rows[0]["ID"].ToString().Trim() != "")
                {
                    if (btnAdddetails.Text == "ADD")
                    {
                        dr = dt.NewRow();
                        #region calculate rowindex
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                        }
                        if (index.Length > 0)
                        {
                            for (int i = 0; i < index.Length; i++)
                            {
                                if (index[i] > maxIndex)
                                {
                                    maxIndex = index[i];
                                }
                            }
                            rowIndex = maxIndex + 1;
                        }
                        else
                        {
                            rowIndex = maxIndex;          //1
                        }
                        #endregion
                        //rowIndex = dt.Rows.Count + 1;
                        dr["ID"] = rowIndex;     //auto
                        dr["rowAction"] = "A";
                        dr["SrNo"] = 0;
                    }
                    else
                    {
                        // update row
                        int n = Convert.ToInt32(lblNo.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["ID"] = rowIndex;
                        dr["SrNo"] = 0;
                        #region decide whether actual row is updating or virtual [rowAction]
                        string id = clsCommon.getString("select detail_id from " + tblDetails + " where doc_no=" + txtDoc_No.Text +
                            " and PS_Id=" + lblSale_Id.Text + "  And Company_Code = " + Convert.ToInt32(Session["Company_Code"].ToString()) +
                            " and Cash_Credit='" + drpCashCredit.SelectedValue + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                        if (id != string.Empty && id != "0")
                        {
                            dr["rowAction"] = "U";   //actual row
                        }
                        else
                        {
                            dr["rowAction"] = "A";    //virtual row
                        }
                        #endregion
                    }
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add((new DataColumn("ID", typeof(Int32))));
                    #region [Write here columns]
                    dt.Columns.Add((new DataColumn("Item_Code", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("item_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Brand_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Brand_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Qty", typeof(double))));
                    dt.Columns.Add((new DataColumn("Wt_Per", typeof(double))));
                    dt.Columns.Add((new DataColumn("Wt_Qty", typeof(double))));
                    dt.Columns.Add((new DataColumn("Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Value", typeof(double))));
                    dt.Columns.Add((new DataColumn("GST_Code", typeof(double))));
                    dt.Columns.Add((new DataColumn("GST_Rate", typeof(string))));
                    dt.Columns.Add((new DataColumn("CGST", typeof(double))));
                    dt.Columns.Add((new DataColumn("SGST", typeof(double))));
                    dt.Columns.Add((new DataColumn("IGST", typeof(double))));
                    dt.Columns.Add((new DataColumn("Hamali_Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Hamali", typeof(double))));
                    dt.Columns.Add((new DataColumn("PSDetail_Id", typeof(int))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dt.Columns.Add(new DataColumn("ic", typeof(Int32)));
                    dt.Columns.Add(new DataColumn("purcac", typeof(Int32)));
                    dt.Columns.Add(new DataColumn("pac", typeof(Int32)));
                    dr = dt.NewRow();
                    dr["ID"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("ID", typeof(int))));
                #region [Write here columns]
                dt.Columns.Add((new DataColumn("Item_Code", typeof(Int32))));
                dt.Columns.Add((new DataColumn("item_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Brand_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Brand_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Qty", typeof(double))));
                dt.Columns.Add((new DataColumn("Wt_Per", typeof(double))));
                dt.Columns.Add((new DataColumn("Wt_Qty", typeof(double))));
                dt.Columns.Add((new DataColumn("Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Value", typeof(double))));
                dt.Columns.Add((new DataColumn("GST_Code", typeof(double))));
                dt.Columns.Add((new DataColumn("GST_Rate", typeof(string))));
                dt.Columns.Add((new DataColumn("CGST", typeof(double))));
                dt.Columns.Add((new DataColumn("SGST", typeof(double))));
                dt.Columns.Add((new DataColumn("IGST", typeof(double))));
                dt.Columns.Add((new DataColumn("Hamali_Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Hamali", typeof(double))));
                dt.Columns.Add((new DataColumn("PSDetail_Id", typeof(int))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dt.Columns.Add(new DataColumn("ic", typeof(int)));
                dt.Columns.Add(new DataColumn("purcac", typeof(int)));
                dt.Columns.Add(new DataColumn("pac", typeof(int)));
                dr = dt.NewRow();
                dr["ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            dr["Item_Code"] = txtItem_Code.Text;
            dr["item_Name"] = lblItamName.Text;
            dr["Brand_Code"] = txtBrand_Code.Text;
            dr["Brand_Name"] = lblBrandname.Text;
            dr["ic"] = clsCommon.getString("select systemid from qrymstitem where system_code=" + txtItem_Code.Text + " and company_code=" + Session["company_code"].ToString() + "");

            if (txtQty.Text != string.Empty)
            {
                dr["Qty"] = txtQty.Text;
            }
            else
            {
                setFocusControl(txtQty);
            }
            if (txtWt_Per.Text != string.Empty)
            {
                dr["Wt_Per"] = txtWt_Per.Text;
            }
            else
            {
                setFocusControl(txtWt_Per);
            }

            if (txtWt_Qty.Text != string.Empty)
            {
                dr["Wt_Qty"] = txtWt_Qty.Text;
            }
            else
            {
                setFocusControl(txtWt_Qty);
            }
            if (txtRate.Text != string.Empty)
            {
                dr["Rate"] = txtRate.Text;
            }
            else
            {
                setFocusControl(txtRate);
            }
            if (txtValue.Text != string.Empty)
            {
                dr["Value"] = txtValue.Text;
            }
            else
            {
                setFocusControl(txtValue);
            }
            if (txtGST_Code.Text != string.Empty)
            {
                dr["GST_Code"] = txtGST_Code.Text;
            }
            else
            {
                setFocusControl(txtGST_Code);
            }
            if (lblGSTRateName.Text != string.Empty)
            {
                dr["GST_Rate"] = lblGSTRateName.Text;
            }
            else
            {
                setFocusControl(lblGSTRateName);
            }

            if (txtCGST.Text != string.Empty)
            {
                dr["CGST"] = txtCGST.Text;
            }
            else
            {
                setFocusControl(txtCGST);
            }
            if (txtSGST.Text != string.Empty)
            {
                dr["SGST"] = txtSGST.Text;
            }
            else
            {
                setFocusControl(txtSGST);
            }
            if (txtIGST.Text != string.Empty)
            {
                dr["IGST"] = txtIGST.Text;
            }
            else
            {
                setFocusControl(txtIGST);
            }
            if (txtHamali_Rate.Text != string.Empty)
            {
                dr["Hamali_Rate"] = txtHamali_Rate.Text;
            }
            else
            {
                dr["Hamali_Rate"] = 0;
                setFocusControl(txtHamali_Rate);
            }
           
            if (txtHamali.Text != string.Empty)
            {
                dr["Hamali"] = txtHamali.Text;
            }
            else
            {
                dr["Hamali"] = 0;
                setFocusControl(txtHamali);
            }

            #endregion
            if (btnAdddetails.Text == "ADD")
            {
                dr["PSDetail_Id"] = 0;
                dt.Rows.Add(dr);
            }

            if (hdnfPurchaseAc.Value != string.Empty)
            {
                dr["purcac"] = hdnfPurchaseAc.Value;
                dr["pac"] = hdnfPurchaseAcid.Value;
            }
            else
            {
                dr["purcac"] = 0;
                dr["pac"] = 0;
            }






            #region set sr no
            DataRow drr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    drr = (DataRow)dt.Rows[i];
                    drr["SrNo"] = i + 1;
                }
            }
            #endregion
            grdDetail.DataSource = dt;
            grdDetail.DataBind();
            ViewState["currentTable"] = dt;
            if (btnAdddetails.Text == "ADD")
            {
                //pnlPopupDetails.Style["display"] = "block";
                setFocusControl(txtItem_Code);
            }
            else
            {
                //pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                //btnOpenDetailsPopup.Focus();
            }
            // Empty Code->

            var item = "";
            if (grdDetail.Rows[0].Cells[3].Text == "")
            {
                item = "0";
            }
            else
            {
                item = grdDetail.Rows[0].Cells[3].Text;
            }
            //hdnfPurchaseAc.Value = clsCommon.getString("select Purchase_AC from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");
            //hdnfPurchaseAcid.Value = clsCommon.getString("select PurcAcid from qrymstitem where System_Code=" + item + " and company_code=" + Session["Company_Code"].ToString() + "");

            txtItem_Code.Text = "";
            lblItamName.Text = "";

            txtRate.Text = "";

            txtBrand_Code.Text = string.Empty;
            lblBrandname.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtWt_Per.Text = string.Empty;
            txtWt_Qty.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtValue.Text = string.Empty;
            txtGST_Code.Text = string.Empty;
            lblGSTRateName.Text = string.Empty;
            txtCGST.Text = string.Empty;
            txtSGST.Text = string.Empty;
            txtIGST.Text = string.Empty;
            txtHamali_Rate.Text = string.Empty;
            txtHamali.Text = string.Empty;
            btnAdddetails.Text = "ADD";
            txtCGST.Text = string.Empty;
            txtSGST.Text = string.Empty;
            txtIGST.Text = string.Empty;
            csCalculations();
            btnAdddetails.Enabled = true;
            grdDetail.Enabled = true;
            setFocusControl(txtItem_Code);
        }
        catch
        {
        }
    }
    #endregion

    #region [btnClosedetails_Click]
    protected void btnClosedetails_Click(object sender, EventArgs e)
    {
        txtItem_Code.Text = string.Empty;
        btntxtItem_Code.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtWt_Per.Text = string.Empty;
        txtWt_Qty.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtValue.Text = string.Empty;
        txtGST_Code.Text = string.Empty;
        lblGSTRateName.Text = string.Empty;
        txtCGST.Text = string.Empty;
        txtSGST.Text = string.Empty;
        txtIGST.Text = string.Empty;

        txtHamali_Rate.Text = string.Empty;
        txtHamali.Text = string.Empty;
        btnAdddetails.Text = string.Empty;
        // btnClosedetails.Text = string.Empty;
        btnAdddetails.Text = "ADD";

        setFocusControl(txtItem_Code);
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[21].Text);
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);
        txtItem_Code.Text = Server.HtmlDecode(gvrow.Cells[3].Text);
        lblItamName.Text = clsCommon.getString("Select System_Name_E from " + tblPrefix + "SystemMaster where System_Code=" + Server.HtmlDecode(gvrow.Cells[3].Text.ToString()) + " and System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        txtBrand_Code.Text = Server.HtmlDecode(gvrow.Cells[5].Text);
        lblBrandname.Text = Server.HtmlDecode(gvrow.Cells[6].Text.ToString());
        txtQty.Text = Server.HtmlDecode(gvrow.Cells[7].Text);
        txtWt_Per.Text = Server.HtmlDecode(gvrow.Cells[8].Text);
        txtWt_Qty.Text = Server.HtmlDecode(gvrow.Cells[9].Text);
        txtRate.Text = Server.HtmlDecode(gvrow.Cells[10].Text);
        txtValue.Text = Server.HtmlDecode(gvrow.Cells[11].Text);
        txtGST_Code.Text = Server.HtmlDecode(gvrow.Cells[12].Text);
        lblGSTRateName.Text = Server.HtmlDecode(gvrow.Cells[13].Text);
        txtCGST.Text = Server.HtmlDecode(gvrow.Cells[14].Text);
        txtSGST.Text = Server.HtmlDecode(gvrow.Cells[15].Text);
        txtIGST.Text = Server.HtmlDecode(gvrow.Cells[16].Text);
        txtHamali_Rate.Text = Server.HtmlDecode(gvrow.Cells[17].Text);
        txtHamali.Text = Server.HtmlDecode(gvrow.Cells[18].Text);


    }
    #endregion

    #region [DeleteDetailsRow]
    private void DeleteDetailsRow(GridViewRow gridViewRow, string action)
    {
        try
        {
            int rowIndex = gridViewRow.RowIndex;
            if (ViewState["currentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["currentTable"];
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["PSDetail_Id"].ToString());
                string IDExisting = clsCommon.getString("select detail_id from " + tblDetails + " where PSDetail_Id=" + ID + " ");
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[20].Text = "D";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[20].Text = "N";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "N";
                    }
                }
                else
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[20].Text = "R";       //R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[20].Text = "A";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "A";
                    }
                }
                ViewState["currentTable"] = dt;
            }
            csCalculations();
        }
        catch
        {
        }
    }
    #endregion

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;

        if (e.Row.RowType != DataControlRowType.Pager)
        {
            if (v == "txtAc_Code" || v == "txtBroker")
            {
                e.Row.Cells[0].Width = new Unit("30px");
                e.Row.Cells[1].Width = new Unit("300px");
                e.Row.Cells[2].Width = new Unit("100px");

            }
            if (v == "txtItem_Code")
            {
                e.Row.Cells[0].Width = new Unit("30px");
                e.Row.Cells[1].Width = new Unit("200px");
            }
            if (v == "txtGST_Code" || v == "txtBrand_Code")
            {
                e.Row.Cells[0].Width = new Unit("90px");
                e.Row.Cells[1].Width = new Unit("150px");
                e.Row.Cells[2].Width = new Unit("90px");
            }
        }

    }
    #endregion

    #region [grdPopup_RowCreated]
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
    #endregion

    #region [RowCommand]
    protected void grdDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int rowindex = row.RowIndex;
            if (e.CommandArgument == "lnk")
            {
                switch (e.CommandName)
                {
                    case "EditRecord":
                        if (grdDetail.Rows[rowindex].Cells[14].Text != "D" && grdDetail.Rows[rowindex].Cells[14].Text != "R")
                        {
                            //pnlPopupDetails.Style["display"] = "block";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "Update";
                            // setFocusControl(txtItem_Code);
                        }
                        break;
                    case "DeleteRecord":
                        string action = "";
                        LinkButton lnkDelete = (LinkButton)e.CommandSource;
                        if (lnkDelete.Text == "Delete")
                        {
                            action = "Delete";
                            lnkDelete.Text = "Open";
                        }
                        else
                        {
                            action = "Open";
                            lnkDelete.Text = "Delete";
                        }
                        this.DeleteDetailsRow(grdDetail.Rows[rowindex], action);
                        break;
                }
            }
        }
        catch
        {
        }

    }
    #endregion

    #region [grdDetail_RowDataBound]
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            // {
            e.Row.Cells[10].Visible = true;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[21].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[22].HorizontalAlign = HorizontalAlign.Center;

            e.Row.Cells[0].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[3].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[4].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[5].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[6].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[7].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[8].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[9].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[10].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[11].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[12].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[13].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[14].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[15].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[16].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[17].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[18].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[19].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[20].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[21].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[22].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[23].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[24].ControlStyle.Width = new Unit("40px");
            int i = 1;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                i++;
                foreach (TableCell cell in e.Row.Cells)
                {
                    string s = cell.Text.ToString();
                    if (cell.Text.Length > 20)
                    {
                        cell.Text = cell.Text.Substring(0, 31) + "..";
                        cell.ToolTip = s;
                    }
                }

            }
            //}
        }
        catch
        {
        }
    }
    #endregion

    #region [imgBtnClose_Click]
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch
        {
        }
    }
    #endregion

    #region [Popup Button Code]
    protected void showPopup(string qry)
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
    #endregion



    #region [txtSearchText_TextChanged]
    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch
        {
        }
    }
    #endregion

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }

    #region [txtEditDoc_No_TextChanged]
    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);

            if (a == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Enter Only Numbers!')", true);
            }
            else
            {
                int do_id = Convert.ToInt32(clsCommon.getString("select PS_Id from " + tblHead + " where Doc_No=" + txtEditDoc_No.Text +
                    " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" +
                    " " + Session["year"].ToString() + ""));



            }
        }
        catch (Exception)
        {
            throw;
        }

    }
    #endregion

    #region [txtDoc_No_TextChanged]
    protected void txtDoc_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_No.Text;
        strTextBox = "txtDoc_No";
        csCalculations();

    }
    #endregion

    protected void btntxtDoc_No_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtDoc_No";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    #region [txtDoc_Date_TextChanged]
    protected void txtDoc_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_Date.Text;
        strTextBox = "txtDoc_Date";
        // csCalculations();
    }
    #endregion

    protected void drpCashCredit_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAdd.Focus();
        hdnfCash_Credit.Value = drpCashCredit.SelectedValue;
        string max = clsCommon.getString("select isnull(max(PS_Id),0) as id from " + tblHead + " where Cash_Credit='" + hdnfCash_Credit.Value + "'");
        hdnf.Value = max;

        clsButtonNavigation.enableDisable("S");
        this.makeEmptyForm("S");
        this.showLastRecord();

    }

    #region [txtAc_Code_TextChanged]
    protected void txtAc_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAc_Code.Text;
        strTextBox = "txtAc_Code";
        csCalculations();
    }
    #endregion

    protected void btntxtAc_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtAc_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    #region [txtBroker_TextChanged]
    protected void txtBroker_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBroker.Text;
        strTextBox = "txtBroker";
        csCalculations();
    }
    #endregion

    protected void btntxtBroker_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBroker";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    #region [txtLR_No_TextChanged]
    protected void txtLR_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtLR_No.Text;
        strTextBox = "txtLR_No";
        csCalculations();
    }
    #endregion

    #region [txtTruck_No_TextChanged]
    protected void txtTruck_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTruck_No.Text;
        strTextBox = "txtTruck_No";
        csCalculations();
    }
    #endregion

    #region [txtItem_Code_TextChanged]
    protected void txtItem_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtItem_Code.Text;
        strTextBox = "txtItem_Code";
        csCalculations();
    }
    #endregion

    #region [btntxtItem_Code_Click]
    protected void btntxtItem_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtItem_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtBrand_Code_TextChanged]
    protected void txtBrand_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBrand_Code.Text;
        strTextBox = "txtBrand_Code";
        csCalculations();
    }
    #endregion

    #region [btntxtBrand_Code_Click]
    protected void btntxtBrand_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBrand_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtQty_TextChanged]
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        searchString = txtQty.Text;
        strTextBox = "txtQty";
        csCalculations();
    }
    #endregion

    #region [txtWt_Per_TextChanged]
    protected void txtWt_Per_TextChanged(object sender, EventArgs e)
    {
        searchString = txtWt_Per.Text;
        strTextBox = "txtWt_Per";
        csCalculations();
    }
    #endregion

    #region [txtWt_Qty_TextChanged]
    protected void txtWt_Qty_TextChanged(object sender, EventArgs e)
    {
        searchString = txtWt_Qty.Text;
        strTextBox = "txtWt_Qty";
        csCalculations();
    }
    #endregion

    #region [txtRate_TextChanged]
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRate.Text;
        strTextBox = "txtRate";
        csCalculations();
    }
    #endregion

    #region [txtValue_TextChanged]
    protected void txtValue_TextChanged(object sender, EventArgs e)
    {
        searchString = txtValue.Text;
        strTextBox = "txtValue";
        csCalculations();
    }
    #endregion

    #region [txtGST_Code_TextChanged]
    protected void txtGST_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGST_Code.Text;
        strTextBox = "txtGST_Code";
        csCalculations();
    }
    #endregion




    #region [txtHamali_Rate_TextChanged]
    protected void txtHamali_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtHamali_Rate.Text;
        strTextBox = "txtHamali_Rate";
        csCalculations();
    }
    #endregion

    #region [txtHamali_TextChanged]
    protected void txtHamali_TextChanged(object sender, EventArgs e)
    {
        searchString = txtHamali.Text;
        strTextBox = "txtHamali";
        csCalculations();
    }
    #endregion

    #region [txtCGST_TextChanged]
    protected void txtCGST_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCGST.Text;
        strTextBox = "txtCGST";
        csCalculations();
    }
    #endregion

    #region [txtSGST_TextChanged]
    protected void txtSGST_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSGST.Text;
        strTextBox = "txtSGST";
        csCalculations();
    }
    #endregion


    #region [txtIGST_TextChanged]
    protected void txtIGST_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIGST.Text;
        strTextBox = "txtIGST";
        csCalculations();
    }
    #endregion

    #region [txtHamaliAmount_TextChanged]
    protected void txtHamaliAmount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtHamaliAmount.Text;
        strTextBox = "txtHamaliAmount";
        csCalculations();
    }
    #endregion

    #region [txtpostage_TextChanged]
    protected void txtpostage_TextChanged(object sender, EventArgs e)
    {
        searchString = txtpostage.Text;
        strTextBox = "txtpostage";
        csCalculations();
    }
    #endregion

    #region [txtAmount_TextChanged]
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAmount.Text;
        strTextBox = "txtAmount";
        csCalculations();
    }
    #endregion

    #region [txtTCS_Par_TextChanged]
    protected void txtTCS_Par_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCS_Par.Text;
        strTextBox = "txtTCS_Par";
        csCalculations();
    }
    #endregion

    #region [txtTCS_Amount_TextChanged]
    protected void txtTCS_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCS_Amount.Text;
        strTextBox = "txtTCS_Amount";
        csCalculations();
    }
    #endregion
    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtDoc_No")
            {
                #region code
                try
                {
                    int n;
                    bool isNumeric = int.TryParse(txtDoc_No.Text, out n);

                    if (isNumeric == true)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        string txtValue = "";
                        if (txtDoc_No.Text != string.Empty)
                        {
                            txtValue = txtDoc_No.Text;
                            string qry = "select * from " + tblHead + " where  doc_no='" + txtValue + "' " +
                                "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

                            ds = clsDAL.SimpleQuery(qry);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    dt = ds.Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        //Record Found
                                        hdnf.Value = dt.Rows[0]["doc_no"].ToString();

                                        if (ViewState["mode"] != null)
                                        {
                                            if (ViewState["mode"].ToString() == "I")
                                            {
                                                lblMsg.Text = "** Doc No (" + txtValue + ") Already Exist";
                                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                                this.getMaxCode();
                                                //txtDoc_no.Enabled = false;
                                                hdnf.Value = txtDoc_No.Text;
                                                btnSave.Enabled = true;   //IMP                                       
                                                setFocusControl(txtDoc_Date);
                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = getDisplayQuery();
                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    txtDoc_No.Enabled = false;
                                                    setFocusControl(txtDoc_Date);
                                                    pnlgrdDetail.Enabled = true;
                                                    hdnf.Value = txtDoc_No.Text;

                                                }
                                            }
                                        }
                                    }
                                    else   //Record Not Found
                                    {
                                        if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                        {
                                            lblMsg.Text = "";
                                            setFocusControl(txtDoc_Date);
                                            txtDoc_No.Enabled = false;
                                            btnSave.Enabled = true;   //IMP
                                        }
                                        if (ViewState["mode"].ToString() == "U")
                                        {
                                            this.makeEmptyForm("E");
                                            lblMsg.Text = "** Record Not Found";
                                            lblMsg.ForeColor = System.Drawing.Color.Red;
                                            txtDoc_No.Text = string.Empty;
                                            setFocusControl(txtDoc_No);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = string.Empty;
                            setFocusControl(txtDoc_No);
                        }
                    }
                    else
                    {
                        this.makeEmptyForm("A");
                        lblMsg.Text = "Doc No is numeric";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        clsButtonNavigation.enableDisable("E");
                        txtDoc_No.Text = string.Empty;
                        setFocusControl(txtDoc_No);
                    }
                }
                catch
                {

                }
                #endregion
            }


            if (strTextBox == "txtDOC_DATE")
            {
                try
                {
                    string dt = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    if (clsCommon.isValidDate(dt) == true)
                    {
                        setFocusControl(txtAc_Code);
                    }
                    else
                    {
                        txtDoc_Date.Text = "";
                        setFocusControl(txtDoc_Date);
                    }
                }
                catch
                {
                    txtDoc_Date.Text = "";
                    setFocusControl(txtDoc_Date);
                }
            }
            if (strTextBox == "txtAc_Code")
            {
                string acname = "";
                if (txtAc_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtAc_Code.Text);
                    if (a == false)
                    {
                        btntxtAc_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        //acname = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        DataTable results = dtAccountMaster.Select("Ac_Code =" + txtAc_Code.Text).CopyToDataTable();
                        acname = results.Rows[0]["Ac_Name_E"].ToString();
                        if (acname != string.Empty && acname != "0")
                        {
                            //  hdnfac.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            hdnfac.Value = results.Rows[0]["accoid"].ToString();
                            //   hdnfAcShort.Value = clsCommon.getString("select short_Name from " + qryAccountList + " where Ac_Code=" + txtAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            hdnfAcShort.Value = results.Rows[0]["accoid"].ToString();

                            lblPartyName.Text = acname;
                            setFocusControl(txtLR_No);

                            //AmtCalculation();
                        }
                        else
                        {
                            txtAc_Code.Text = string.Empty;
                            lblPartyName.Text = acname;
                            setFocusControl(txtAc_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtAc_Code);
                }
            }

            if (strTextBox == "txtBroker")
            {
                string brokername = "";
                if (txtBroker.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBroker.Text);
                    if (a == false)
                    {
                        btntxtBroker_Click(this, new EventArgs());
                    }
                    else
                    {
                        //  hdnfbc.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtBroker.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        // brokername = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtBroker.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        DataTable results = dtAccountMaster.Select("Ac_Code =" + txtBroker.Text).CopyToDataTable();
                        brokername = results.Rows[0]["Ac_Name_E"].ToString();
                        hdnfbc.Value = results.Rows[0]["accoid"].ToString();


                        if (brokername != string.Empty )
                        {
                            //hdnfbk.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtBroker.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            lblBrokerName.Text = brokername;
                            setFocusControl(txtLR_No);
                        }
                        else
                        {
                            txtBroker.Text = string.Empty;
                            lblBrokerName.Text = brokername;
                            setFocusControl(txtBroker);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtBroker);
                }
            }
            if (strTextBox == "txtLR_No")
            {
                setFocusControl(txtTruck_No);
            }
            if (strTextBox == "txtTruck_No")
            {
                setFocusControl(txtbillNo);
            }
            if (strTextBox == "txtbillNo")
            {
                setFocusControl(txtItem_Code);
            }
            if (strTextBox == "txtItem_Code")
            {
                string itemname = "";
                if (txtItem_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtItem_Code.Text);
                    if (a == false)
                    {
                        btntxtItem_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        DataTable results = dtItemMaster.Select("System_Code =" + txtItem_Code.Text).CopyToDataTable();
                        itemname = results.Rows[0]["System_Name_E"].ToString();


                        //  itemname = clsCommon.getString("select System_Name_E from " + SystemMasterTable + " where System_Code=" + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
                        if (itemname != string.Empty && itemname != "0")
                        {
                            txtWt_Per.Text = results.Rows[0]["KgPerKatta"].ToString();
                            txtGST_Code.Text = results.Rows[0]["Gst_Code"].ToString();
                            lblGSTRateName.Text = clsCommon.getString("select Rate from nt_1_gstratemaster where Doc_no=" + txtGST_Code.Text +
                                "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            lblItamName.Text = itemname;
                            hdnfPurchaseAc.Value = results.Rows[0]["Purchase_AC"].ToString();
                            hdnfPurchaseAcid.Value = results.Rows[0]["pac"].ToString();
                            setFocusControl(txtBrand_Code);
                        }
                        else
                        {
                            txtItem_Code.Text = string.Empty;
                            lblItamName.Text = itemname;
                            setFocusControl(txtItem_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtItem_Code);
                }
            }
            if (strTextBox == "txtBrand_Code")
            {
                string acname = "";
                if (txtBrand_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBrand_Code.Text);
                    if (a == false)
                    {
                        btntxtBrand_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        // DataSet ds = clsDAL.SimpleQuery("select Marka,Wt_Per from Brand_Master where Code=" + txtBrand_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        DataSet dsnew = new DataSet();
                        DataTable dtadd = new DataTable();

                        DataTable results = dtBrandMaster.Select("Code =" + txtBrand_Code.Text).CopyToDataTable();

                        //dtadd = dtBrandMaster;
                        // dsnew.Tables.Add(dtadd);
                        if (results.Rows.Count > 0)
                        {
                            //   DataTable dt = dsnew.Tables[0];
                            lblBrandname.Text = results.Rows[0]["Marka"].ToString();
                            setFocusControl(txtQty);
                            // txtWtper.Text = dt.Rows[0]["Wt_Per"].ToString();
                        }
                        else
                        {
                            lblBrandname.Text = "";
                            setFocusControl(txtQty);

                        }




                    }
                }
                else
                {
                    setFocusControl(txtBrand_Code);
                }
            }
            if (strTextBox == "txtQty")
            {
                setFocusControl(txtRate);
            }
            if (strTextBox == "txtWt_Per")
            {
                setFocusControl(txtWt_Qty);
            }
            if (strTextBox == "txtWt_Qty")
            {
                setFocusControl(txtRate);
            }
            if (strTextBox == "txtRate")
            {
                setFocusControl(txtValue);
            }
            if (strTextBox == "txtRate")
            {
                setFocusControl(txtHamali_Rate);
            }
            if (strTextBox == "txtHamali_Rate")
            {
                setFocusControl(btnAdddetails);
            }
            if (strTextBox == "txtGST_Code")
            {
                string gstname = "";
                if (txtGST_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtGST_Code.Text);
                    if (a == false)
                    {
                        btntxtGSTRateCode_Click(this, new EventArgs());
                    }
                    else
                    {
                        gstname = clsCommon.getString("select rate from " + tblPrefix + "GSTRateMaster where Doc_no=" + txtGST_Code.Text + " ");
                        if (gstname != string.Empty && gstname != "0")
                        {
                            lblGSTRateName.Text = gstname;
                            setFocusControl(txtHamali_Rate);
                        }
                        else
                        {
                            txtGST_Code.Text = string.Empty;
                            lblGSTRateName.Text = gstname;
                            setFocusControl(txtGST_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtGST_Code);
                }
            }
            if (strTextBox == "txtCGST")
            {
                setFocusControl(txtSGST);
            }
            if (strTextBox == "txtSGST")
            {
                setFocusControl(txtIGST);
            }
            if (strTextBox == "txtIGST")
            {
                setFocusControl(txtHamali_Rate);
            }
            if (strTextBox == "txtHamali_Rate")
            {
                setFocusControl(btnAdddetails);
            }
            //if (strTextBox == "txtHamali")
            //{
            //    setFocusControl(btnAdddetails);
            //}








            #region calculation part
            double qty = txtQty.Text != string.Empty ? Convert.ToDouble(txtQty.Text) : 0.0;
            double Wt_Per = txtWt_Per.Text != string.Empty ? Convert.ToDouble(txtWt_Per.Text) : 0.0;
            double rate = txtRate.Text != string.Empty ? Convert.ToDouble(txtRate.Text) : 0.0;
            double hamalirate = txtHamali_Rate.Text.Trim() != string.Empty ? Convert.ToDouble(txtHamali_Rate.Text.Trim()) : 0.0;
            Int32 itemcode = txtItem_Code.Text != string.Empty ? Convert.ToInt32(txtItem_Code.Text) : 0;
            double NetWt = 0.00;
            double value = 0.00;
            double hamaliamt1 = 0.00;
            NetWt = qty * Wt_Per;
            txtWt_Qty.Text = NetWt.ToString();

            double NET_Wt = txtWt_Qty.Text != string.Empty ? Convert.ToDouble(txtWt_Qty.Text) : 0.0;
            string RatePer = clsCommon.getString("select RatePer from " + SystemMasterTable + " where System_Code=" + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
            //DataTable results1 = dtItemMaster.Select("System_Code =" +itemcode ).CopyToDataTable();

            // string RatePer = results1.Rows[0]["RatePer"].ToString();
            if (RatePer == "Q")
            {
                value = Math.Round(qty * rate, 2);
            }
            else
            {
                value = Math.Round((NET_Wt / 100) * rate, 2);
            }
            txtValue.Text = value.ToString();
            hamaliamt1 = qty * hamalirate;
            txtHamali.Text = hamaliamt1.ToString();
            #region GSTCalculation
            string aaa = "";
            if (txtAc_Code.Text.Trim() != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtAc_Code.Text);
                if (a == true)
                {
                    aaa = clsCommon.getString("select isnull(GSTStateCode,0) from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAc_Code.Text + "");
                }
            }
            int partygstStateCode = 0;
            if (aaa.Trim().ToString() != "")
            {
                partygstStateCode = Convert.ToInt32(aaa);
            }

            if (Session["CompanyGSTStateCode"] == null || Session["CompanyGSTStateCode"].ToString() == string.Empty)
            {
                Session["CompanyGSTStateCode"] = clsCommon.getString("select GSTStateCode from nt_1_companyparameters where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " +
                    "  Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
            }

            int companyGstStateCode = Convert.ToInt32(Session["CompanyGSTStateCode"].ToString());
            string GSTRateCode = txtGST_Code.Text != string.Empty ? Convert.ToString(txtGST_Code.Text) : "0";
            double GSTRate = Convert.ToDouble(clsCommon.getString("select Rate from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            double cgstrate = Convert.ToDouble(clsCommon.getString("select CGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            double sgstrate = Convert.ToDouble(clsCommon.getString("select SGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));
            double igstrate = Convert.ToDouble(clsCommon.getString("select IGST from " + tblPrefix + "GSTRateMaster where Doc_no=" + GSTRateCode + " "));

            double CGSTAmountForPS = 0.00;
            double SGSTAmountForPS = 0.00;
            double IGSTAmountForPS = 0.00;


            double taxmillamt = txtValue.Text != string.Empty ? Convert.ToDouble(txtValue.Text) : 0.0;
            if (companyGstStateCode == partygstStateCode)
            {
                double cgsttaxAmountOnMR = Math.Round((taxmillamt * cgstrate / 100), 2);
                CGSTAmountForPS = Math.Round(cgsttaxAmountOnMR, 2);

                double sgsttaxAmountOnMR = Math.Round((taxmillamt * sgstrate / 100), 2);
                SGSTAmountForPS = Math.Round(sgsttaxAmountOnMR, 2);
            }
            else
            {
                double igsttaxAmountOnMR = ((taxmillamt) * igstrate / 100);
                IGSTAmountForPS = Math.Round(igsttaxAmountOnMR, 2);
            }

            txtCGST.Text = CGSTAmountForPS.ToString();
            txtSGST.Text = SGSTAmountForPS.ToString();
            txtIGST.Text = IGSTAmountForPS.ToString();
            #endregion

            #region calculate subtotal
            double taxableamt = 0.00;
            double cgstamt = 0.00;
            double sgstamt = 0.00;
            double igstamt = 0.00;
            double hamaliamt = 0.00;
            if (grdDetail.Rows.Count > 0)
            {
                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    if (grdDetail.Rows[i].Cells[20].Text != "D")
                    {
                        double totvalue = Convert.ToDouble(grdDetail.Rows[i].Cells[11].Text.Trim());
                        taxableamt = taxableamt + totvalue;

                        double totcgstamt = Convert.ToDouble(grdDetail.Rows[i].Cells[14].Text.Trim());
                        cgstamt = cgstamt + totcgstamt;

                        double totsgstamt = Convert.ToDouble(grdDetail.Rows[i].Cells[15].Text.Trim());
                        sgstamt = sgstamt + totsgstamt;

                        double totigstamt = Convert.ToDouble(grdDetail.Rows[i].Cells[16].Text.Trim());
                        igstamt = igstamt + totigstamt;

                        double tothamaliamt = Convert.ToDouble(grdDetail.Rows[i].Cells[18].Text.Trim());
                        hamaliamt = hamaliamt + tothamaliamt;
                    }
                }
                txtTaxable_Amount.Text = taxableamt.ToString();
                txtCGST_Amount.Text = cgstamt.ToString();
                txtSGST_Amount.Text = sgstamt.ToString();
                txtIGST_Amount.Text = igstamt.ToString();
                txtHamaliAmount.Text = hamaliamt.ToString();

            }
            #endregion




            double billAmt = 0.00;
            double taxableAmount = txtTaxable_Amount.Text != string.Empty ? Convert.ToDouble(txtTaxable_Amount.Text) : 0.0;
            CGSTAmountForPS = txtCGST_Amount.Text != string.Empty ? Convert.ToDouble(txtCGST_Amount.Text) : 0.0;
            SGSTAmountForPS = txtSGST_Amount.Text != string.Empty ? Convert.ToDouble(txtSGST_Amount.Text) : 0.0;
            IGSTAmountForPS = txtIGST_Amount.Text != string.Empty ? Convert.ToDouble(txtIGST_Amount.Text) : 0.0;
            double HAMALIAmountForPS = txtHamaliAmount.Text != string.Empty ? Convert.ToDouble(txtHamaliAmount.Text) : 0.0;
            double PSTAGEAmountForPS = txtpostage.Text != string.Empty ? Convert.ToDouble(txtpostage.Text) : 0.0;

            double billAmountForCalculation = taxableAmount + CGSTAmountForPS + SGSTAmountForPS + IGSTAmountForPS + HAMALIAmountForPS + PSTAGEAmountForPS;
            // billAmountForCalculation = billAmountForCalculation + other + Roundoffamnt;

            billAmt = Math.Round(billAmountForCalculation, 2);
            txtAmount.Text = billAmt.ToString();



            #region TCS Calculation
            double TCS_Rate = 0.000;
            double TCS_Amt = 0.00;
            double Bill_Amt = 0.00;
            double Net_Payable_Amt = 0.00;
            if (txtTCS_Amount.Text == string.Empty || txtTCS_Amount.Text == "0")
            {
                txtTCS_Amount.Text = "0";
            }
            else
            {
                TCS_Amt = Convert.ToDouble(txtTCS_Amount.Text);
            }
            TCS_Rate = Convert.ToDouble(txtTCS_Par.Text);
            Bill_Amt = Convert.ToDouble(txtAmount.Text);
            if (TCS_Rate != 0)
            {
                TCS_Amt = Math.Round(((Bill_Amt * TCS_Rate) / 100), 2);
            }

            //Net_Payable_Amt = Math.Round((Bill_Amt + TCS_Amt - TDS_Amt), 2);


            txtTCS_Amount.Text = TCS_Amt.ToString();
            //txtTCSNet_Payable.Text = Net_Payable_Amt.ToString();
            #endregion
            #region TDS Calculation
            double TDS_Rate = 0.000;
            double TDS_Amt = 0.00;
            double subtotal = 0.00;
            //TDS_Rate = Convert.ToDouble(txtTDS.Text);
            ////TDS_Rate = txtTDS.Text != string.Empty ? Convert.ToDouble(txtTDS.Text) : 0.00;
            ////Bill_Amount = Convert.ToDouble(txtBILL_AMOUNT.Text);
            ////TDS_Amt = Math.Round(((subtotal * TDS_Rate) / 100), 2);
            ////txtTDSAmt.Text = TDS_Amt.ToString();

            if (txtTDSAmt.Text == string.Empty || txtTDSAmt.Text == "0")
            {
                txtTDSAmt.Text = "0";
            }
            else
            {
                TDS_Amt = Convert.ToDouble(txtTDSAmt.Text);
            }
            TDS_Rate = Convert.ToDouble(txtTDS.Text);
            subtotal = Convert.ToDouble(txtTaxable_Amount.Text);
           
                TDS_Amt = Math.Round(((subtotal * TDS_Rate) / 100), 2);
            
            txtTDSAmt.Text = TDS_Amt.ToString();
            #endregion
            #endregion



        }
        catch
        {
        }
    }
    #endregion

    protected void btntxtGSTRateCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGST_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void txtbillNo_TextChanged(object sender, EventArgs e)
    {

    }
    private void AmtCalculation()
    {
        string Amt = string.Empty;
        double Amount = 0.00;
        double Amountf = 0.00;
        double PSBalAmt = 0.00;
        double balancelimit = Convert.ToDouble(Session["BalanceLimit"]);
        PSBalAmt = Convert.ToDouble("0" + txtTaxable_Amount.Text);


        DataTable dtpan = new DataTable();
        DataSet dspan = new DataSet();
        #region[balancelimit]
        string panno = clsCommon.getString("select CompanyPan from qrymstaccountmaster where Ac_Code=" + txtAc_Code.Text
           + " and Company_Code=" + Session["Company_Code"].ToString() + "");

        dspan = clsDAL.SimpleQuery("select Ac_Code from qrymstaccountmaster where CompanyPan='" + panno
           + "' and Company_Code=" + Session["Company_Code"].ToString() + "");
        dtpan = dspan.Tables[0];


        double Amt2 = 0.00;
        double sbamt2 = 0.00;
        if (panno != string.Empty)
        {
            if (dtpan.Rows.Count > 0)
            {
                for (int i = 0; i < dtpan.Rows.Count; i++)
                {
                    double Amt1 = 0.00;

                    Amt1 = Convert.ToDouble(clsCommon.getString("select isnull(sum(AMOUNT),0) as AMOUNT from NT_1_GLEDGER where AC_CODE=" + dtpan.Rows[i]["Ac_Code"].ToString()
                   + " and DRCR='C' and TRAN_TYPE in('PS','PR','RP') and YEAR_CODE='" + Convert.ToInt32(Session["year"].ToString()) + "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())));
                    Amt2 += Amt1;
                }

                Amt = Amt2.ToString();
            }
        #endregion
        }
        if (Amt == string.Empty || Amt == "")
        {
            Amt = "0.00";
        }
        Amountf = Convert.ToDouble(Amt);
        if (btnSave.Text == "Save")
        {
            Amount = Amountf + PSBalAmt;
        }
        else
        {
            Amount = Amountf;
        }
        if (Amount > balancelimit)
        {
            // txtTDS.Text = Session["SaleTDSRate"].ToString();
            txtTCS_Par.Text = "0.00";
        }
        else
        {
            // txtTDS.Text = "0.00";
            txtTCS_Par.Text = "0.00";
        }
    }
    protected void txtTDS_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDS.Text;
        strTextBox = "txtTDS";

        txtTCS_Par.Text = "0.00";
        txtTCS_Amount.Text = "0.00";
        csCalculations();
    }
    protected void txtTDSAmt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTDSAmt.Text;
        strTextBox = "txtTDSAmt";
        csCalculations();
    }
}