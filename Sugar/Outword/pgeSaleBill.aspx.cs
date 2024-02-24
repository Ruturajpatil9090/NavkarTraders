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

public partial class Sugar_Outword_pgeSaleBill : System.Web.UI.Page
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
    int Sale_Id = 0;

    public static SqlConnection con = null;
    public static SqlCommand cmd = null;
    public static SqlTransaction myTran = null;
    string cs = string.Empty;
    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "Sale_Head";
            tblDetails = "Sale_Detail";
            AccountMasterTable = tblPrefix + "AccountMaster";
            SystemMasterTable = tblPrefix + "SystemMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            qryCommon = tblPrefix + "";
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
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    Action = Request.QueryString["Action"];
                    if (Action == "1")
                    {

                        hdnf.Value = Request.QueryString["Sale_Id"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");

                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        //this.showLastRecord();
                        //this.enableDisableNavigateButtons();
                        //setFocusControl(btnEdit);

                    }
                    else
                    {
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");
                        this.NextNumber();
                       
                       
                        
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
    #endregion

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
                txtGST_Rate.Enabled = false;
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
                txtGST_Rate.Enabled = false;
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
                txtGST_Rate.Enabled = true;
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
                txtGST_Rate.Enabled = true;
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
                txtGST_Rate.Enabled = false;
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
                txtGST_Rate.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                txtBrand_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;
                txtSGST.Enabled = false;
                txtCGST.Enabled = false;
                txtIGST.Enabled = false;
                txtHamali_Rate.Enabled = false;
                txtHamali.Enabled = false;
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
                txtGST_Rate.Enabled = true;
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
                txtGST_Rate.Enabled = true;
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
                " and Year_Code='" + Session["year"].ToString() + "'"));
            if (counts == 0)
            {
                txtDoc_No.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(Doc_No) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                    " and Year_Code='" + Session["year"].ToString() + "'")) + 1;
                txtDoc_No.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(Sale_Id) as Sale_Id from " + tblHead + " "));
            if (counts == 0)
            {
                lblSale_Id.Text = "1";
                Sale_Id = 1;
            }
            else
            {
                Sale_Id = Convert.ToInt32(clsCommon.getString("SELECT max(Sale_Id) as Sale_Id from " + tblHead)) + 1;
                lblSale_Id.Text = Sale_Id.ToString();
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
        this.getMaxCode();
        txtDoc_No.Enabled = false;
        
        string gstname = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster where Doc_no=1  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
       
        this.NextNumber();
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
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "system", "javascript:DeleteConform();", true);
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

        hdnf.Value = Request.QueryString["Sale_Id"];
        if (hdnf.Value == "0" || hdnf.Value == "")
        {
            int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(Sale_Id),0) as Sale_Id from Sale_Head "));

            hdnf.Value = Convert.ToString(maxno);
        }
        Response.Redirect("pgeSaleBill.aspx?Sale_Id=" + hdnf.Value + "&Action=" + 1);


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
                string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
            }
           
         
        
            if (hdnfClosePopup.Value == "txtBroker")
            {
                lblPopupHead.Text = "--Select Broker--";
              
                string qry = "select Ac_Code,Ac_Name_E,cityname from " + qryAccountList + "  where Locked=0 and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%')  and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtItem_Code")
            {
                lblPopupHead.Text = "--Select Item--";
                string qry = "select System_Code,System_Name_E as Item_Name from " + SystemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                this.showPopup(qry);
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
           
            string qryDisplay = "select * from " + qryHead + " where Sale_Id=" + hdnf.Value + " ";
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
                    " where Sale_Id>" + Convert.ToInt32(hdnf.Value) +
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
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Sale_Id<" + Convert.ToInt32(hdnf.Value) + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
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
        query = "select Sale_Id from " + tblHead + " where Sale_Id=(select MIN(Sale_Id) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
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
                string query = "SELECT top 1 [Sale_Id] from " + tblHead + " where Sale_Id<" + Convert.ToInt32(hdnf.Value) +
                            "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY Sale_Id DESC  ";
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
                string query = "SELECT top 1 [Sale_Id] from " + tblHead + " where Sale_Id>" + Convert.ToInt32(hdnf.Value) +
                            "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY Sale_Id asc  ";
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
            query = "select Sale_Id from " + tblHead + " where Sale_Id=(select MAX(Sale_Id) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") " +
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

                        hdnfyearcode.Value = dt.Rows[0]["Year_Code"].ToString();
                        hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();
                        txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();
                        hdnf.Value = dt.Rows[0]["Sale_Id"].ToString();
                        hdnfsaledoc.Value = txtDoc_No.Text;
                        lblSale_Id.Text = hdnf.Value;
                        hdnfSale_Id.Value = lblSale_Id.Text;
                        txtDoc_Date.Text = dt.Rows[0]["doc_dateConverted"].ToString();
                        txtAc_Code.Text = dt.Rows[0]["Ac_Code"].ToString();
                        lblPartyName.Text = dt.Rows[0]["billtoname"].ToString();
                        txtBroker.Text = dt.Rows[0]["Broker"].ToString();
                        lblBrokerName.Text = dt.Rows[0]["CarporateBillTo_Name"].ToString();
                        txtLR_No.Text = dt.Rows[0]["LR_No"].ToString();
                        txtTruck_No.Text = dt.Rows[0]["Truck_No"].ToString();
                        txtCGST.Text = dt.Rows[0]["CGST"].ToString();
                        txtCGST_Amount.Text = dt.Rows[0]["CGST_Amount"].ToString();
                        txtSGST.Text = dt.Rows[0]["SGST"].ToString();
                        txtSGST_Amount.Text = dt.Rows[0]["SGST_Amount"].ToString();
                        txtIGST.Text = dt.Rows[0]["IGST"].ToString();
                        txtIGST_Amount.Text = dt.Rows[0]["IGST_Amount"].ToString();
                        txtTaxable_Amount.Text = dt.Rows[0]["Taxable_Amount"].ToString();
                        txtHamaliAmount.Text = dt.Rows[0]["HamaliAmount"].ToString();
                        txtpostage.Text = dt.Rows[0]["postage"].ToString();
                        txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                      
                        {

                            btnEdit.Enabled = true;
                            btnDelete.Enabled = false;
                            pnlgrdDetail.Enabled = false;
                        }

                        txtTCS_Par.Text = dt.Rows[0]["TCS_Par"].ToString();
                        txtTCS_Amount.Text = dt.Rows[0]["TCS_Amount"].ToString();
                       
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

                        qry = "select Sale_Id ,Item_Code,itemname as item_Name,Brand_Code,Brand_Name,Qty,Wt_Per,Wt_Qty,Rate,Value,GST_Code,GST_Code,GST_Rate,SGST,IGST,CGST,Hamali_Rate,Hamali,SaleSale_Id,'' as rowAction,'' as SrNo,ic from " + qryDetail + " where Sale_Id=" + hdnf.Value;
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
        //csCalculations();
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
                        string id = clsCommon.getString("select Sale_Id from " + tblDetails + " where doc_no=" + txtDoc_No.Text + " and Sale_Id=" + lblID.Text + "  And Company_Code = " + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
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
                    dt.Columns.Add((new DataColumn("Sale_Id", typeof(Int32))));
                    #region [Write here columns]
                    dt.Columns.Add((new DataColumn("Item_Code", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("item_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Brand_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Brand_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Qty", typeof(string))));
                    dt.Columns.Add((new DataColumn("Wt_Per", typeof(double))));
                    dt.Columns.Add((new DataColumn("Wt_Qty", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("Rate", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("Value", typeof(double))));
                    dt.Columns.Add((new DataColumn("GST_Code", typeof(double))));
                    dt.Columns.Add((new DataColumn("GST_Rate", typeof(int))));
                    dt.Columns.Add((new DataColumn("SGST", typeof(int))));
                    dt.Columns.Add((new DataColumn("CGST", typeof(int))));
                    dt.Columns.Add((new DataColumn("IGST", typeof(int))));
                    dt.Columns.Add((new DataColumn("Hamali_Rate", typeof(int))));
                    dt.Columns.Add((new DataColumn("Hamali", typeof(int))));
                    dt.Columns.Add((new DataColumn("SaleSale_Id", typeof(int))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                  
                    dr = dt.NewRow();
                    dr["Sale_Id"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("Sale_Id", typeof(int))));
                #region [Write here columns]
                dt.Columns.Add((new DataColumn("Item_Code", typeof(Int32))));
                dt.Columns.Add((new DataColumn("item_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Brand_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Brand_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Qty", typeof(string))));
                dt.Columns.Add((new DataColumn("Wt_Per", typeof(double))));
                dt.Columns.Add((new DataColumn("Wt_Qty", typeof(Int32))));
                dt.Columns.Add((new DataColumn("Rate", typeof(Int32))));
                dt.Columns.Add((new DataColumn("Value", typeof(double))));
                dt.Columns.Add((new DataColumn("GST_Code", typeof(double))));
                dt.Columns.Add((new DataColumn("GST_Rate", typeof(int))));
                dt.Columns.Add((new DataColumn("SGST", typeof(int))));
                dt.Columns.Add((new DataColumn("CGST", typeof(int))));
                dt.Columns.Add((new DataColumn("IGST", typeof(int))));
                dt.Columns.Add((new DataColumn("Hamali_Rate", typeof(int))));
                dt.Columns.Add((new DataColumn("Hamali", typeof(int))));
                dt.Columns.Add((new DataColumn("SaleSale_Id", typeof(int))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
               
                dr = dt.NewRow();
                dr["Sale_Id"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            dr["Item_Code"] = txtItem_Code.Text;
            dr["item_Name"] = lblItamName.Text;
            dr["Brand_Code"] = txtBrand_Code.Text;
            dr["Brand_Name"] = lblBrandname.Text;
            dr["ic"] = clsCommon.getString("select systemid from qrymstitem where system_code=" + txtItem_Code.Text + " and company_code=" + Session["company_code"].ToString() + "");
          
            #endregion
            if (btnAdddetails.Text == "ADD")
            {
                dr["saledetailid"] = 0;
                dt.Rows.Add(dr);
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
          

            txtItem_Code.Text = "";
            lblItamName.Text = "";
           
            txtRate.Text = "";
           
            txtBrand_Code.Text = string.Empty;
            lblBrandname.Text = string.Empty;
           
            btnAdddetails.Text = "ADD";

           // csCalculations();
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
        txtGST_Rate.Text = string.Empty;
        
        txtHamali_Rate.Text = string.Empty;
        txtHamali.Text = string.Empty;
        btnAdddetails.Text = string.Empty;
        btnClosedetails.Text = string.Empty;
        btnAdddetails.Text = "ADD";

        setFocusControl(txtItem_Code);
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
      
    }
    #endregion

    #region [DeleteDetailsRow]
    private void DeleteDetailsRow(GridViewRow gridViewRow, string action)
    {
      
    }
    #endregion

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
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
            e.Row.Cells[0].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[3].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[4].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[5].ControlStyle.Width = new Unit("250px");
            e.Row.Cells[6].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[7].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[8].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[9].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[10].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[11].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[12].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[13].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[14].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[15].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[16].ControlStyle.Width = new Unit("40px");
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
                int do_id = Convert.ToInt32(clsCommon.getString("select Sale_Id from " + tblHead + " where Doc_No=" + txtEditDoc_No.Text +
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
        //csCalculations();
       
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
        showLastRecord();

    }

    #region [txtAc_Code_TextChanged]
    protected void txtAc_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAc_Code.Text;
        strTextBox = "txtAc_Code";
        //csCalculations();
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
        //csCalculations();
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
        //csCalculations();
    }
    #endregion

    #region [txtTruck_No_TextChanged]
    protected void txtTruck_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTruck_No.Text;
        strTextBox = "txtTruck_No";
        //csCalculations();
    }
    #endregion

    #region [txtItem_Code_TextChanged]
    protected void txtItem_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtItem_Code.Text;
        strTextBox = "txtItem_Code";
        //csCalculations();
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
        //csCalculations();
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
        //csCalculations();
    }
    #endregion

    #region [txtWt_Per_TextChanged]
    protected void txtWt_Per_TextChanged(object sender, EventArgs e)
    {
        searchString = txtWt_Per.Text;
        strTextBox = "txtWt_Per";
        //csCalculations();
    }
    #endregion

    #region [txtWt_Qty_TextChanged]
    protected void txtWt_Qty_TextChanged(object sender, EventArgs e)
    {
        searchString = txtWt_Qty.Text;
        strTextBox = "txtWt_Qty";
        //csCalculations();
    }
    #endregion

    #region [txtRate_TextChanged]
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRate.Text;
        strTextBox = "txtRate";
        //csCalculations();
    }
    #endregion

    #region [txtValue_TextChanged]
    protected void txtValue_TextChanged(object sender, EventArgs e)
    {
        searchString = txtValue.Text;
        strTextBox = "txtValue";
        //csCalculations();
    }
    #endregion

    #region [txtGST_Code_TextChanged]
    protected void txtGST_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGST_Code.Text;
        strTextBox = "txtGST_Code";
        //csCalculations();
    }
    #endregion

    #region [txtGST_Rate_TextChanged]
    protected void txtGST_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGST_Rate.Text;
        strTextBox = "txtGST_Rate";
        //csCalculations();
    }
    #endregion

   

    #region [txtHamali_Rate_TextChanged]
    protected void txtHamali_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtHamali_Rate.Text;
        strTextBox = "txtHamali_Rate";
        //csCalculations();
    }
    #endregion

    #region [txtHamali_TextChanged]
    protected void txtHamali_TextChanged(object sender, EventArgs e)
    {
        searchString = txtHamali.Text;
        strTextBox = "txtHamali";
        //csCalculations();
    }
    #endregion

    #region [txtCGST_TextChanged]
    protected void txtCGST_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCGST.Text;
        strTextBox = "txtCGST";
        //csCalculations();
    }
    #endregion

    #region [txtSGST_TextChanged]
    protected void txtSGST_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSGST.Text;
        strTextBox = "txtSGST";
        //csCalculations();
    }
    #endregion


    #region [txtIGST_TextChanged]
    protected void txtIGST_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIGST.Text;
        strTextBox = "txtIGST";
        //csCalculations();
    }
    #endregion

    #region [txtHamaliAmount_TextChanged]
    protected void txtHamaliAmount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtHamaliAmount.Text;
        strTextBox = "txtHamaliAmount";
        //csCalculations();
    }
    #endregion

    #region [txtpostage_TextChanged]
    protected void txtpostage_TextChanged(object sender, EventArgs e)
    {
        searchString = txtpostage.Text;
        strTextBox = "txtpostage";
        //csCalculations();
    }
    #endregion

    #region [txtAmount_TextChanged]
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAmount.Text;
        strTextBox = "txtAmount";
        //csCalculations();
    }
    #endregion

    #region [txtTCS_Par_TextChanged]
    protected void txtTCS_Par_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCS_Par.Text;
        strTextBox = "txtTCS_Par";
        //csCalculations();
    }
    #endregion

    #region [txtTCS_Amount_TextChanged]
    protected void txtTCS_Amount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTCS_Amount.Text;
        strTextBox = "txtTCS_Amount";
        //csCalculations();
    }
    #endregion

}