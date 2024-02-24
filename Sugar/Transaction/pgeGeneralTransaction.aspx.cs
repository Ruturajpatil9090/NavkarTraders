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

public partial class Sugar_Transaction_pgeGeneralTransaction : System.Web.UI.Page
{
    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string AccountMasterView = string.Empty;
    string SystemMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string qryHead = string.Empty;
    string qryDetail = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string trntype = "PS";
    int defaultAccountCode = 0;
    string qryAccountList = string.Empty;
    string GLedgerTable = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    string Action = string.Empty;
    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;

    int Doc_No = 0;
    int purchase_id = 0;
    #endregion

    public static SqlConnection con = null;
    public static SqlCommand cmd = null;
    public static SqlTransaction myTran = null;
    string cs = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "nt_1_GeneralTran_Head";
            tblDetails ="nt_1_GeneralTran_Details";
            AccountMasterTable = tblPrefix + "AccountMaster";
            SystemMasterTable = tblPrefix + "SystemMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            qryCommon = "qryGeneralTrasaction";
            qryHead = "qryGeneralTrasaction";
            qryDetail = "qryGeneralTrasaction";
            qryAccountList = "qrymstaccountmaster";
            GLedgerTable = tblPrefix + "GLEDGER";
            pnlPopup.Style["display"] = "none";
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
                        hdnf.Value = Request.QueryString["gtranid"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");
                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        this.showLastRecord();
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
                        setFocusControl(txtDOC_DATE);
                        // PurcSaleDo_CoomonFields purc = new PurcSaleDo_CoomonFields();

                    }
                    
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
                int gtranid = Convert.ToInt32(clsCommon.getString("select gtranid from nt_1_GeneralTran_Head where doc_no=" + txtEditDoc_No.Text + ""));

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:PUrchaseOPen('" + gtranid + "')", true);

            }
        }
        catch (Exception)
        {
            throw;
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
                obj.tableName = tblHead;
                obj.code = "doc_no";
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
                                    txtDOC_NO.Text = ds.Tables[0].Rows[0][0].ToString();
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
                txtEditDoc_No.Enabled = true;
                pnlPopup.Style["display"] = "none";
                // btnOpenDetailsPopup.Enabled = false;
                btnSave.Text = "Save";
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                lblMsg.Text = string.Empty;
                #region logic
                lblCompanyName.Text = "";
                lblYearname.Text = "";
                lblDebitAc_Code.Text = "";
                lblCreditAc_Code.Text = "";
                btntxtCompany_CODE.Enabled = false;
                btntxtYear_Code.Enabled = false;
                btntxtDebitAc_Code.Enabled = false;
                btntxtdoc_no.Enabled = false;
                btntxtCreditAc_Code.Enabled = false;
                calenderExtenderDate.Enabled = false;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = false;
                #endregion
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
               

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
                txtEditDoc_No.Enabled = false;
                btnSave.Text = "Save";
                btntxtdoc_no.Text = "Change No";
                btntxtdoc_no.Enabled = true;
                txtDOC_DATE.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                #region set Business logic for save
                lblCompanyName.Text = "";
                lblYearname.Text = "";
                lblDebitAc_Code.Text = "";
                lblCreditAc_Code.Text = "";
               
                btntxtCompany_CODE.Enabled = true;
                btntxtYear_Code.Enabled = true;
                btntxtDebitAc_Code.Enabled = true;
                btntxtCreditAc_Code.Enabled = true;
               
                calenderExtenderDate.Enabled = true;
               
                // btnOpenDetailsPopup.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = true;
                txtDOC_NO.Enabled = false;
                btntxtdoc_no.Enabled = false;
                #endregion
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
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
                txtEditDoc_No.Enabled = true;
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                // btnOpenDetailsPopup.Enabled = false;
                pnlgrdDetail.Enabled = false;
                btntxtCompany_CODE.Enabled = false;
                #region logic
                btntxtYear_Code.Enabled = false;
                btntxtDebitAc_Code.Enabled = false;
                btntxtCreditAc_Code.Enabled = false;
                calenderExtenderDate.Enabled = false;
               #endregion
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
               
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
                txtEditDoc_No.Enabled = false;
                txtDOC_NO.Enabled = false;
                btntxtdoc_no.Enabled = false;
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = true;
                lblMsg.Text = string.Empty;
                // btnOpenDetailsPopup.Enabled = true;
                pnlgrdDetail.Enabled = true;
                btntxtCompany_CODE.Enabled = true;
             
                #region logic
                btntxtYear_Code.Enabled = true;
                btntxtDebitAc_Code.Enabled = true;
                btntxtCreditAc_Code.Enabled = true;
                calenderExtenderDate.Enabled = true;
                #endregion

                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                
            }
        }
        catch
        {
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


    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select doc_no from " + tblHead + " where doc_no=(select MIN(doc_no) from " + tblHead+") " ;
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion

    #region [Previous]
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDOC_NO.Text != string.Empty)
            {
                string query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no<" + Convert.ToInt32(hdnf.Value) + " ORDER BY doc_no DESC";
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
            if (txtDOC_NO.Text != string.Empty)
            {
                string query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no>" + Convert.ToInt32(hdnf.Value) + " ORDER BY doc_no asc  ";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
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
            query = "select DOC_NO from " + tblHead + " where DOC_NO=(select MAX(DOC_NO) from " + tblHead + ") ";
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
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
        this.NextNumber();
        setFocusControl(txtDOC_DATE);
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
        txtDOC_NO.Enabled = false;
        btntxtdoc_no.Enabled = false;
        setFocusControl(txtCompany_CODE);
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
        hdnf.Value = Request.QueryString["gtranid"];
        if (hdnf.Value == "0" || hdnf.Value == "")
        {
            int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(gtranid),0) as gtranid from nt_1_GeneralTran_Head "));

            hdnf.Value = Convert.ToString(maxno);
        }
        Response.Redirect("pgeGeneralTransaction.aspx?gtranid=" + hdnf.Value + "&Action=" + 1);
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
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //string yearcode = Session["year"].ToString();
                        //if (hdnfyearcode.Value != yearcode)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records year code & current year code is not same !')", true);
                        //    return false;
                        //}
                        //string comcode = Session["Company_Code"].ToString();
                        //if (hdnfcompanycode.Value != comcode)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same !')", true);
                        //    return false;
                        //}

                        //hdnfyearcode.Value = dt.Rows[0]["Year_Code"].ToString();
                        //hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();

                        txtDOC_NO.Text = dt.Rows[0]["DOC_NO"].ToString();
                        hdnfdocno.Value = txtDOC_NO.Text;
                        lblPurchase_Id.Text = hdnf.Value;
                        hdnfgtranid.Value = lblPurchase_Id.Text;
                       
                        txtDOC_DATE.Text = dt.Rows[0]["doc_dateConverted"].ToString();
                        
                       
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
                        recordExist = true;
                        lblMsg.Text = "";

                      


                        #region  Details
                        qry = "select detail_id as ID,Company_Code,Company_Name_E as Company_Name,Year_Code,year as Year,Debit_ac,DebitAc_Name,Credit_ac,CreditAc_NAme,amount as Amount,narration,gtrandetailid," +
                            " '' as rowAction,dac,cac,'' as SrNo from " + qryDetail + " where gtranid=" + hdnf.Value + " ";
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    //dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                    //dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
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
                }
            }
           return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = "select * from " + qryHead + " where gtranid=" + hdnf.Value + " ";
            return qryDisplay;
        }
        catch
        {
            return "";
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
                txtDOC_NO.Text = hdnf.Value;
                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Focus();
                }
                // this.enableDisableNavigateButtons();
                this.makeEmptyForm("S");
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

    #region [btnOpenDetailsPopup_Click]
    protected void btnOpenDetailsPopup_Click(object sender, EventArgs e)
    {
        btnAdddetails.Text = "ADD";
        // pnlPopupDetails.Style["display"] = "block";
        //txtPACKING.Text = "50";
        //txtITEM_CODE.Text = "";
        //txtQUANTAL.Text = "";
        //txtBAGS.Text = "";
        //txtRATE.Text = "";
        //txtITEMAMOUNT.Text = "";
        //txtITEM_NARRATION.Text = "";
        //LBLITEMNAME.Text = "";
        //setFocusControl(txtITEM_CODE);
    }
    #endregion

    #region [btnAdddetails_Click]
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        try
        {
            bool isValidated = true;
            if (txtAMOUNT.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtAMOUNT);
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
                        //     rowIndex = dt.Rows.Count + 1;
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
                        string id = clsCommon.getString("select detail_id from " + tblDetails + " where doc_no=" + txtDOC_NO.Text + " and Detail_ID=" + lblID.Text + "");
                        if (id != "0")
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
                    dt.Columns.Add((new DataColumn("Company_Code", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("Company_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Year_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Year", typeof(string))));
                    dt.Columns.Add((new DataColumn("Debit_ac", typeof(int))));
                    dt.Columns.Add((new DataColumn("DebitAc_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Credit_ac", typeof(int))));
                    dt.Columns.Add((new DataColumn("CreditAc_NAme", typeof(string))));
                    dt.Columns.Add((new DataColumn("Amount", typeof(double))));
                    dt.Columns.Add((new DataColumn("narration", typeof(string))));
                    dt.Columns.Add((new DataColumn("gtrandetailid", typeof(int))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add(new DataColumn("dac", typeof(int)));
                    dt.Columns.Add(new DataColumn("cac", typeof(int)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                  
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
                dt.Columns.Add((new DataColumn("Company_Code", typeof(Int32))));
                dt.Columns.Add((new DataColumn("Company_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Year_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Year", typeof(string))));
                dt.Columns.Add((new DataColumn("Debit_ac", typeof(int))));
                dt.Columns.Add((new DataColumn("DebitAc_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Credit_ac", typeof(int))));
                dt.Columns.Add((new DataColumn("CreditAc_NAme", typeof(string))));
                dt.Columns.Add((new DataColumn("Amount", typeof(double))));
                dt.Columns.Add((new DataColumn("narration", typeof(string))));
                dt.Columns.Add((new DataColumn("gtrandetailid", typeof(int))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add(new DataColumn("dac", typeof(int)));
                dt.Columns.Add(new DataColumn("cac", typeof(int)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                

                dr = dt.NewRow();
                dr["ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            dr["Company_Code"] = txtCompany_CODE.Text;
            dr["Company_Name"] = lblCompanyName.Text;
            dr["Year_Code"] = txtYear_Code.Text;
            dr["Year"] = lblYearname.Text;
            dr["Debit_ac"] = txtDebitAc_Code.Text;
            dr["DebitAc_Name"] = lblDebitAc_Code.Text;
            dr["Credit_ac"] = txtCreditAc_Code.Text;
            dr["CreditAc_NAme"] = lblCreditAc_Code.Text;
            if (txtAMOUNT.Text != string.Empty)
            {
                dr["Amount"] = txtAMOUNT.Text;
            }
            else
            {
                setFocusControl(txtAMOUNT);
            }
            dr["narration"] = txtNARRATION.Text;
            dr["dac"] = clsCommon.getString("select isnull(accoid,0) as acid from " + qryAccountList + " where Ac_Code=" + txtDebitAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            dr["cac"] = clsCommon.getString("select isnull(accoid,0) as acid from " + qryAccountList + " where Ac_Code=" + txtCreditAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
           
            
            #endregion
            if (btnAdddetails.Text == "ADD")
            {
                dr["gtrandetailid"] = 0;
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
                // pnlPopupDetails.Style["display"] = "block";
                setFocusControl(txtCompany_CODE);
            }
            else
            {
                // pnlPopupDetails.Style["display"] = "none";
                setFocusControl(txtCompany_CODE);
                // btnOpenDetailsPopup.Focus();
            }
            // Empty Code->
           
          
            txtCompany_CODE.Text = "";
            lblCompanyName.Text = string.Empty;
            txtYear_Code.Text = "";
            lblYearname.Text = string.Empty;
            txtDebitAc_Code.Text = "";
            lblDebitAc_Code.Text = string.Empty;
            txtCreditAc_Code.Text = "";
            lblCreditAc_Code.Text = string.Empty;
            txtNARRATION.Text = "";
            txtAMOUNT.Text = "";
            csCalculations();
            setFocusControl(txtCompany_CODE);
            btnAdddetails.Text = "ADD";
        }
        catch
        {
        }
    }
    #endregion

    #region [btnClosedetails_Click]
    protected void btnClosedetails_Click(object sender, EventArgs e)
    {
        txtCompany_CODE.Text = string.Empty;
        lblCompanyName.Text = string.Empty;
        txtYear_Code.Text = string.Empty;
        lblYearname.Text = string.Empty;
        txtDebitAc_Code.Text = string.Empty;
        lblDebitAc_Code.Text = string.Empty;
        txtCreditAc_Code.Text = string.Empty;
        lblCreditAc_Code.Text = string.Empty;
        txtNARRATION.Text = string.Empty;
        txtAMOUNT.Text = string.Empty;
        btnAdddetails.Text = string.Empty;
        btnClosedetails.Text = string.Empty;
        btnAdddetails.Text = "ADD";
        btnClosedetails.Text = "Close";
        setFocusControl(txtCompany_CODE);
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[17].Text);
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);
        txtCompany_CODE.Text = Server.HtmlDecode(gvrow.Cells[3].Text);
        lblCompanyName.Text = clsCommon.getString("Select Company_Name_E from company where Company_Code=" + Server.HtmlDecode(gvrow.Cells[3].Text.ToString()));
        txtYear_Code.Text = Server.HtmlDecode(gvrow.Cells[5].Text);
        lblYearname.Text = clsCommon.getString("Select year from accountingyear where yearCode=" + Server.HtmlDecode(gvrow.Cells[5].Text.ToString()));
        txtDebitAc_Code.Text = Server.HtmlDecode(gvrow.Cells[7].Text);
        lblDebitAc_Code.Text = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + Server.HtmlDecode(gvrow.Cells[7].Text.ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        txtCreditAc_Code.Text = Server.HtmlDecode(gvrow.Cells[9].Text);
        lblCreditAc_Code.Text = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + Server.HtmlDecode(gvrow.Cells[9].Text.ToString()) + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        txtAMOUNT.Text = Server.HtmlDecode(gvrow.Cells[11].Text.ToString());
        txtNARRATION.Text = Server.HtmlDecode(gvrow.Cells[12].Text);
      
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
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["gtrandetailid"].ToString());
                string IDExisting = clsCommon.getString("select detail_id from " + tblDetails + " where gtrandetailid=" + ID + " ");
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[14].Text = "D";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[14].Text = "N";
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
                        grdDetail.Rows[rowIndex].Cells[14].Text = "R";       //R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[14].Text = "A";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "A";
                    }
                }
                ViewState["currentTable"] = dt;
            }
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
            if (v == "txtDebitAc_Code" || v == "txtCreditAc_Code")
            {
                e.Row.Cells[0].Width = new Unit("30px");
                e.Row.Cells[1].Width = new Unit("300px");
                e.Row.Cells[2].Width = new Unit("100px");

            }
            if (v == "txtCompany_CODE" || v == "txtYear_Code")
            {
                e.Row.Cells[0].Width = new Unit("30px");
                e.Row.Cells[1].Width = new Unit("200px");
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
                            // pnlPopupDetails.Style["display"] = "block";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "Update";
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
        csCalculations();
    }
    #endregion

    #region [grdDetail_RowDataBound]
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            e.Row.Cells[10].Visible = true;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[3].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[4].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[5].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[6].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[7].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[8].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[9].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[10].ControlStyle.Width = new Unit("150px");
            e.Row.Cells[11].ControlStyle.Width = new Unit("100px");
            //e.Row.Cells[10].Visible = false;

            e.Row.Cells[12].ControlStyle.Width = new Unit("160px");
            e.Row.Cells[13].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[14].ControlStyle.Width = new Unit("20px");
            e.Row.Cells[15].ControlStyle.Width = new Unit("60px");
            e.Row.Cells[16].ControlStyle.Width = new Unit("60px");
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

    #region [txtDOC_NO_TextChanged]
    protected void txtDOC_NO_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDOC_NO.Text;
        strTextBox = "txtDOC_NO";
        csCalculations();
    }
    #endregion

    #region [btntxtDOC_NO_Click]
    protected void btntxtDOC_NO_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtDOC_NO";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtDOC_DATE_TextChanged]
    protected void txtDOC_DATE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDOC_DATE.Text;
        strTextBox = "txtDOC_DATE";
        csCalculations();
    }
    #endregion

    #region [txtCompany_CODE_TextChanged]
    protected void txtCompany_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCompany_CODE.Text;
        strTextBox = "txtCompany_CODE";
        csCalculations();
    }
    #endregion

    #region [btntxtCompany_CODE_Click]
    protected void btntxtCompany_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtCompany_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtYear_Code_TextChanged]
    protected void txtYear_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtYear_Code.Text;
        strTextBox = "txtYear_Code";
        csCalculations();
    }
    #endregion

    #region [btntxtYear_Code_Click]
    protected void btntxtYear_Code_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtYear_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtDebitAc_Code_TextChanged]
    protected void txtDebitAc_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDebitAc_Code.Text;
        strTextBox = "txtDebitAc_Code";
        csCalculations();
    }
    #endregion

    #region [btntxtDebitAc_Code_Click]
    protected void btntxtDebitAc_Code_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtDebitAc_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtAMOUNT_TextChanged]
    protected void txtAMOUNT_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAMOUNT.Text;
        strTextBox = "txtAMOUNT";
        csCalculations();
    }
    #endregion

    #region [txtCreditAc_Code_TextChanged]
    protected void txtCreditAc_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCreditAc_Code.Text;
        strTextBox = "txtCreditAc_Code";
        csCalculations();
    }
    #endregion

    #region [btntxtCreditAc_Code_Click]
    protected void btntxtCreditAc_Code_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtCreditAc_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtNARRATION_TextChanged]
    protected void txtNARRATION_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNARRATION.Text;
        strTextBox = "txtNARRATION";
        csCalculations();
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
                        pnlPopup.Style["display"] = "block";
                        txtSearchText.Text = string.Empty;
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = "0";
                        pnlPopup.Style["display"] = "block";
                    }
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [Account Master Popup Button Code]
    protected void showPopupAccountMaster(string qry)
    {
        try
        {
            setFocusControl(txtSearchText);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.myaccountmaster(qry);

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
                        pnlPopup.Style["display"] = "block";
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = "0";
                        pnlPopup.Style["display"] = "block";
                    }
                }
            }
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
            if (hdnfClosePopup.Value == "txtCompany_CODE")
            {
                setFocusControl(txtCompany_CODE);
            }
            if (hdnfClosePopup.Value == "txtYear_Code")
            {
                setFocusControl(txtYear_Code);
            }
            if (hdnfClosePopup.Value == "txtDebitAc_Code")
            {
                setFocusControl(txtDebitAc_Code);
            }
            if (hdnfClosePopup.Value == "txtCreditAc_Code")
            {
                setFocusControl(txtCreditAc_Code);
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
    #endregion

    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion

    #region [txtSearchText_TextChanged]
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
                setFocusControl(btnSearch);
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (hdnfyearcode.Value != Session["year"].ToString())
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records year code & current year code is not same!')", true);
            return;
            // Response.Redirect("~/Sugar/pgeHome.aspx", false);
        }
        if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
            return;

        }
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Purchase", "javascript:pagevalidation();", true);

        }
        catch
        {

        }

    }
    #endregion

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            string searchtxt = searchString;
            string delimStr = "";
            char[] delimiter = delimStr.ToCharArray();
            string words = searchString;
            string[] split = null;
            string name = string.Empty;
            if (searchString != string.Empty)
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

            if (hdnfClosePopup.Value == "txtDOC_NO" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                if (btntxtdoc_no.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtDOC_NO.Text = string.Empty;
                    txtDOC_NO.Enabled = true;

                    btnSave.Enabled = false;
                    setFocusControl(txtDOC_NO);
                    hdnfClosePopup.Value = "Close";
                }


                if (btntxtdoc_no.Text == "Choose No")
                {
                    lblPopupHead.Text = "--Select DOC No--";
                    string qry = "select doc_no,doc_date,Company_Name_E,year from " + qryCommon + " where (doc_no like '%" + txtSearchText.Text + "%' or doc_date like '%" + txtSearchText.Text + "%' or Company_Name_E like '%" + txtSearchText.Text + "%' or year like '%" + txtSearchText.Text + "%')";
                    this.showPopup(qry);
                }
            }
           if (hdnfClosePopup.Value == "txtCompany_CODE")
            {
                lblPopupHead.Text = "--Select Item--";
                if (txtCompany_CODE.Text != string.Empty)
                {
                    split = txtCompany_CODE.Text.Split(delimiter);
                }
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Company_Code like '%" + aa + "%' or Company_Name_E like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                string qry = "select Company_Code,Company_Name_E as Company_Name from company where " + name + "";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtYear_Code")
            {
                lblPopupHead.Text = "--Select Item--";
                if (txtYear_Code.Text != string.Empty)
                {
                    split = txtYear_Code.Text.Split(delimiter);
                }
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( yearCode like '%" + aa + "%' or year like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                string qry = "select yearCode,year as Year from accountingyear where " + name + "";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtDebitAc_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += " ( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' or city_name_e like '%" + aa + "%' or Short_Name like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Mill--";
                //string qry = "select " + qryAccountList + ".Ac_Code," + qryAccountList + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + qryAccountList +
                //    " left outer join " + cityMasterTable + " on " + qryAccountList + ".City_Code=" + cityMasterTable + ".city_code and " + qryAccountList + ".Company_Code=" + cityMasterTable + ".company_code where " + qryAccountList + ".Company_Code="
                //    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Locked=0  " +
                //    " and (" + qryAccountList + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + qryAccountList + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + qryAccountList + ".Ac_Name_E";
                //this.showPopup(qry);

                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                  + " and (" + name + ") order by Ac_Name_E ";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtCreditAc_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += " ( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' or city_name_e like '%" + aa + "%' or Short_Name like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Broker--";
                //string qry = "select " + qryAccountList + ".Ac_Code," + qryAccountList + ".Ac_Name_E," + cityMasterTable + ".city_name_e as City from " + qryAccountList +
                //    " left outer join " + cityMasterTable + " on " + qryAccountList + ".City_Code=" + cityMasterTable + ".city_code and " + qryAccountList + ".Company_Code=" + cityMasterTable + ".company_code where " + qryAccountList + ".Company_Code="
                //    + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    " and Locked=0 and (" + qryAccountList + ".Ac_Code like '%" + txtSearchText.Text + "%' or " + qryAccountList + ".Ac_Name_E like '%" + txtSearchText.Text + "%' or " + cityMasterTable + ".city_name_e like '%" + txtSearchText.Text + "%') order by " + qryAccountList + ".Ac_Name_E";
                //this.showPopup(qry);

                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
              + " and (" + name + ") order by Ac_Name_E ";

                this.showPopupAccountMaster(qry);
            }
           
        }
        catch
        {
        }
    }
    #endregion

    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtDOC_NO")
            {
                #region code
                try
                {
                    int n;
                    bool isNumeric = int.TryParse(txtDOC_NO.Text, out n);

                    if (isNumeric == true)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        string txtValue = "";
                        if (txtDOC_NO.Text != string.Empty)
                        {
                            txtValue = txtDOC_NO.Text;

                            string qry = "select * from " + tblHead + " where  doc_no='" + txtValue;
                               

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
                                                txtDOC_NO.Enabled = false;
                                                hdnf.Value = txtDOC_NO.Text;
                                                btnSave.Enabled = true;   //IMP                                       
                                                setFocusControl(txtDOC_DATE);
                                            }
                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = getDisplayQuery();
                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    pnlgrdDetail.Enabled = true;
                                                    txtDOC_NO.Enabled = false;
                                                    setFocusControl(txtDOC_DATE);
                                                    hdnf.Value = txtDOC_NO.Text;
                                                }
                                            }
                                        }
                                    }
                                    else   //Record Not Found
                                    {
                                        if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                        {
                                            lblMsg.Text = "";
                                            setFocusControl(txtDOC_DATE);
                                            txtDOC_NO.Enabled = false;
                                            btnSave.Enabled = true;   //IMP
                                        }
                                        if (ViewState["mode"].ToString() == "U")
                                        {
                                            this.makeEmptyForm("E");
                                            lblMsg.Text = "** Record Not Found";
                                            lblMsg.ForeColor = System.Drawing.Color.Red;
                                            txtDOC_NO.Text = string.Empty;
                                            setFocusControl(txtDOC_NO);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = string.Empty;
                            setFocusControl(txtDOC_NO);
                        }
                    }
                    else
                    {
                        this.makeEmptyForm("A");
                        lblMsg.Text = "Doc No is numeric";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        clsButtonNavigation.enableDisable("E");
                        txtDOC_NO.Text = string.Empty;
                        setFocusControl(txtDOC_NO);
                    }
                }
                catch
                {

                }
                #endregion
            }

            if (strTextBox == "txtCompany_CODE")
            {
                string acname = "";
                if (txtCompany_CODE.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtCompany_CODE.Text);
                    if (a == false)
                    {
                        btntxtCompany_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        DataSet ds = clsDAL.SimpleQuery("select Company_Name_E from company where Company_Code=" + txtCompany_CODE.Text);
                        if (ds != null)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                lblCompanyName.Text = dt.Rows[0]["Company_Name_E"].ToString();
                                setFocusControl(txtYear_Code);
                                // txtWtper.Text = dt.Rows[0]["Wt_Per"].ToString();
                            }
                            else
                            {
                                lblCompanyName.Text = "";
                                setFocusControl(txtCompany_CODE);

                            }
                        }
                        else
                        {
                            lblCompanyName.Text = "";
                            setFocusControl(txtCompany_CODE);
                        }


                    }
                }
                else
                {
                    setFocusControl(txtCompany_CODE);
                }
            }
            if (strTextBox == "txtDOC_DATE")
            {
                try
                {
                    string dt = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    //DateTime startDate = DateTime.Parse(clsGV.Start_Date);
                    //TimeSpan oneDay = new TimeSpan(864000000000);
                    //DateTime dayBeforeStartDate = startDate.Subtract(oneDay);
                    if (clsCommon.isValidDate(dt) == true || dt == "31/03/2017")
                    {
                        setFocusControl(txtCompany_CODE);
                    }
                    else
                    {
                        txtDOC_DATE.Text = "";
                        setFocusControl(txtDOC_DATE);
                    }
                }
                catch
                {
                    txtDOC_DATE.Text = "";
                    setFocusControl(txtDOC_DATE);
                }
            }
            if (strTextBox == "txtYear_Code")
            {
                string acname = "";
                if (txtYear_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtYear_Code.Text);
                    if (a == false)
                    {
                        btntxtYear_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select year from accountingyear where yearCode=" + txtYear_Code.Text);
                        //hdnfac.Value = clsCommon.getString("select isnull(accoid,0) as acid from " + qryAccountList + " where Ac_Code=" + txtAC_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (acname != string.Empty)
                        {

                            lblYearname.Text = acname;
                            setFocusControl(txtDebitAc_Code);

                            
                        }
                        else
                        {
                            txtYear_Code.Text = string.Empty;
                            lblYearname.Text = acname;
                            setFocusControl(txtYear_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtYear_Code);
                }
            }
            if (strTextBox == "txtDebitAc_Code")
            {
                string acname = "";
                if (txtDebitAc_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtDebitAc_Code.Text);
                    if (a == false)
                    {
                        btntxtDebitAc_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                       
                            lblMsg.Text = "";
                            lblPopupHead.Text = "--Select DebitAc_Code--";

                            acname = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtDebitAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            hdnfDebitAc.Value = clsCommon.getString("select isnull(accoid,0) as acid from " + qryAccountList + " where Ac_Code=" + txtDebitAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                            if (acname != string.Empty)
                            {
                                lblDebitAc_Code.Text = acname;
                                setFocusControl(txtCreditAc_Code);
                                
                            }
                            else
                            {
                                txtDebitAc_Code.Text = string.Empty;
                                lblDebitAc_Code.Text = acname;
                                setFocusControl(txtDebitAc_Code);
                            }
                       
                    }
                }
                else
                {
                    setFocusControl(txtDebitAc_Code);
                }
            }
            if (strTextBox == "txtCreditAc_Code")
            {
                string millName = "";

                if (txtCreditAc_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtCreditAc_Code.Text);
                    if (a == false)
                    {
                        btntxtCreditAc_Code_Click(this, new EventArgs());
                        
                    }
                    else
                    {
                        millName = clsCommon.getString("select Ac_Name_E from " + qryAccountList + " where Ac_Code=" + txtCreditAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        hdnfCreditAc.Value = clsCommon.getString("select isnull(accoid,0) as acid from " + qryAccountList + " where Ac_Code=" + txtCreditAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        if (millName != string.Empty && millName != "0")
                        {
                            lblCreditAc_Code.Text = millName;
                            setFocusControl(txtAMOUNT);
                           
                        }
                        else
                        {
                            txtCreditAc_Code.Text = string.Empty;
                            lblCreditAc_Code.Text = millName;
                            setFocusControl(txtCreditAc_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtCreditAc_Code);
                }
            }
            if (strTextBox == "txtAMOUNT")
            {
                setFocusControl(txtNARRATION);
            }
           
            if (strTextBox == "txtNARRATION")
            {
                setFocusControl(btnAdddetails);
            }
            
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
   
    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select isnull(count(doc_no),0) as doc_no from " + tblHead));
            if (counts == 0)
            {
                txtDOC_NO.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(doc_no) as doc_no from " + tblHead) + 1);
                txtDOC_NO.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT isnull(count(gtranid),0) as gtranid from " + tblHead + " "));
            if (counts == 0)
            {
                lblPurchase_Id.Text = "1";
                purchase_id = 1;
            }
            else
            {
                purchase_id = Convert.ToInt32(clsCommon.getString("SELECT max(gtranid) as gtranid from " + tblHead)) + 1;
                lblPurchase_Id.Text = purchase_id.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }
    #endregion
    protected void txtmill_inv_date_TextChanged(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string NewInsert(string PurchaseInsertUpdate, string PurchaseDetail_Insert, string PurchaseDetail_Update, string PurchaseDetail_Delete, string Gledger_Delete, string Gledger_Insert, string status)
    {
        try
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "HeadDetail_CrudOpration";
            cmd.CommandType = CommandType.StoredProcedure;
            //an out parameter


            //an in parameter
            cmd.Parameters.AddWithValue("QryInsertAndUpdate", PurchaseInsertUpdate);
            cmd.Parameters["QryInsertAndUpdate"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Insert", PurchaseDetail_Insert);
            cmd.Parameters["QryDetail_Insert"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Update", PurchaseDetail_Update);
            cmd.Parameters["QryDetail_Update"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryDetail_Delete", PurchaseDetail_Delete);
            cmd.Parameters["QryDetail_Delete"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryGledger_Delete", Gledger_Delete);
            cmd.Parameters["QryGledger_Delete"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("QryGledger_Insert", Gledger_Insert);
            cmd.Parameters["QryGledger_Insert"].Direction = ParameterDirection.Input;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter _adapter = new SqlDataAdapter(cmd);
            ds = new DataSet();
            _adapter.Fill(ds);
            string msgReturn = "";
            if (status == "Save")
            {
                msgReturn = "Record Successfully Added";
            }
            else if (status == "Update")
            {
                msgReturn = "Record Successfully Update";
            }
            return msgReturn;

        }
        catch
        {
            con.Close();
            return "";
        }
        finally
        {
            con.Close();
        }
    }
   
}