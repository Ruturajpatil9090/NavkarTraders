using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;

public partial class Sugar_Inword_pgeColdStorageInword : System.Web.UI.Page
{
    #region data section
    string qry = string.Empty;
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string qryCommon = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string isAuthenticate = string.Empty;
    string trntype = string.Empty;
    static WebControl objAsp = null;
    string user = string.Empty;
    string cs = string.Empty;

    int Detail_Id = 2;
    int Item_Code = 3;
    int Itemaname = 4;
    int Brand_Code = 5;
    int brandname = 6;
    int Qty = 7;
    int Wtper = 8;
    int Netkg = 9;
    int Rowaction = 10;
    int Srno = 11;
    int ic = 12;
    int csdetailid = 13;

    int Doc_No = 0;
    int Sale_id = 0;
    #endregion

    //DataTable Maindt = null;
    //DataTable SalePurcdt = null;
    //DataRow dr = null;

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "ColdStorageHead";
            tblDetails = "ColdStorageHeadDetail";
            qryCommon = "qryColdStorageHeadDetail";
            user = Session["user"].ToString();
            pnlPopup.Style["display"] = "none";

            //Maindt = new DataTable();
            //dr = null;
            //Maindt.Columns.Add("Querys", typeof(string));
            //dr = Maindt.NewRow();

            if (!Page.IsPostBack)
            {
                hdnfyearcode.Value = Session["year"].ToString();
                hdnfcompanycode.Value = Session["Company_Code"].ToString();
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    string Action = Request.QueryString["Action"];

                    if (Action == "1")
                    {
                        hdnf.Value = Request.QueryString["csid"];
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");
                        pnlgrdDetail.Enabled = true;
                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";

                        this.showLastRecord();
                        setFocusControl(btnEdit);
                    }
                    else
                    {
                        btnAdd_Click(sender, e);
                    }
                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
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
                obj.tableName = tblHead + " where ";
                obj.code = "Doc_no";
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
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                grdDetail.DataSource = null; grdDetail.DataBind();
                ViewState["currentTable"] = null;
                ViewState["currentTable"] = null;
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                //drpTran_Type.Enabled = false;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtChallan_No.Enabled = false;
                txtChallan_Date.Enabled = false;
                CalendarExtenderDatetxtChallan_Date.Enabled = false;
                txtVahical_No.Enabled = false;
                txtParty_Code.Enabled = false;
                btntxtParty_Code.Enabled = false;
                txtItem_Code.Enabled = false;
                btntxtItem_Code.Enabled = false;
                txtBrand_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;
                txtQty.Enabled = false;
                txtWtper.Enabled = false;
                txtNetkg.Enabled = false;
                btnBack.Enabled = true;

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
                btntxtDoc_No.Enabled = true;
                txtEditDoc_No.Enabled = false;
                txtDoc_No.Enabled = false;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null; grdDetail.DataBind();
                ViewState["currentTable"] = null;
                txtItem_Code.Enabled = true;
                lblItemname.Text = string.Empty;
                btntxtItem_Code.Enabled = true;
                txtBrand_Code.Enabled = true;
                lblBrandname.Text = string.Empty;
                btntxtBrand_Code.Enabled = true;
                txtQty.Enabled = true;
                txtWtper.Enabled = true;
                txtNetkg.Enabled = true;
                txtDoc_Date.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtChallan_No.Enabled = true;
                txtChallan_Date.Enabled = true;
                txtChallan_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtChallan_Date.Enabled = true;
                txtVahical_No.Enabled = true;
                txtParty_Code.Enabled = true;
                lblParty_Code.Text = string.Empty;
                lblpartyStatecode.Text = string.Empty;
                btntxtParty_Code.Enabled = true;

                #region set Business logic for save
                #endregion

                btnBack.Enabled = false;
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
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtChallan_No.Enabled = false;
                txtChallan_Date.Enabled = false;
                CalendarExtenderDatetxtChallan_Date.Enabled = false;
                txtVahical_No.Enabled = false;
                txtParty_Code.Enabled = false;
                btntxtParty_Code.Enabled = false;
                txtItem_Code.Enabled = false;
                btntxtItem_Code.Enabled = false;
                txtBrand_Code.Enabled = false;
                btntxtBrand_Code.Enabled = false;
                txtQty.Enabled = false;
                txtWtper.Enabled = false;
                txtNetkg.Enabled = false;
                txtItem_Code.Text = string.Empty;
                btntxtItem_Code.Enabled = false;
                txtBrand_Code.Text = string.Empty;
                btntxtBrand_Code.Enabled = false;
                txtQty.Text = string.Empty;
                txtWtper.Text = string.Empty;
                txtNetkg.Text = string.Empty;

                btnAdddetails.Text = "ADD";
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                btnBack.Enabled = true;
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
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = true;
                txtEditDoc_No.Enabled = false;
                lblMsg.Text = string.Empty;

                txtDoc_Date.Enabled = true;
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtChallan_No.Enabled = true;
                txtChallan_Date.Enabled = true;
                CalendarExtenderDatetxtChallan_Date.Enabled = true;
                txtVahical_No.Enabled = true;
                txtParty_Code.Enabled = true;
                btntxtParty_Code.Enabled = true;
                txtItem_Code.Enabled = true;
                btntxtItem_Code.Enabled = true;
                txtBrand_Code.Enabled = true;
                btntxtBrand_Code.Enabled = true;
                txtQty.Enabled = true;
                txtWtper.Enabled = true;
                txtNetkg.Enabled = true;

                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                btnBack.Enabled = false;
            }
            #region Always check this
            #endregion
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
    #region [enableDisableNavigateButtons]

    private void enableDisableNavigateButtons()
    {

    }
    #endregion

    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select doc_no from " + tblHead + " where doc_no=(select MIN(doc_no) from " +
               tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
               " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

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
            if (txtDoc_No.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No< " + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
                string query = "";
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No> " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY Doc_No asc ";
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
            query = "select DOC_NO from " + tblHead + " where DOC_NO=(select MAX(DOC_NO) from " + tblHead +
     " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
     " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            //hdnf.Value = clsCommon.getString(query);
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
        pnlgrdDetail.Enabled = true;
        this.makeEmptyForm("A");
        setFocusControl(txtDoc_Date);
        pnlPopupDetails.Style["display"] = "none";
        this.NextNumber();
    }
    #endregion
    private void NextNumber()
    {
        //try
        //{
        //    int Doc_No = Convert.ToInt32(clsCommon.getString("SELECT isnull(max(doc_no),0) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and  Year_Code='" + Session["Year"].ToString())) + 1;
        //    txtDoc_No.Text = Doc_No.ToString();

        //}
        //catch
        //{
        //}

        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select count(doc_no) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                " and Year_Code='" + Session["year"].ToString() + "'"));
            if (counts == 0)
            {
                txtDoc_No.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(doc_no) as doc_no from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' " +
                    " and Year_Code='" + Session["year"].ToString() + "'")) + 1;
                txtDoc_No.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(csid) as saleid from " + tblHead + " "));
            if (counts == 0)
            {
                lblDoc_No.Text = "1";
                Sale_id = 1;
            }
            else
            {
                Sale_id = Convert.ToInt32(clsCommon.getString("SELECT max(csid) as saleid from " + tblHead)) + 1;
                lblDoc_No.Text = Sale_id.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        pnlgrdDetail.Enabled = true;
        this.makeEmptyForm("E");
        txtDoc_No.Enabled = false;
        setFocusControl(txtDoc_Date);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                string str = string.Empty;
                DataSet xml_ds = new DataSet();
                if (str == string.Empty)
                {
                    string CheckDocNo = "";
                    string doc_no = clsCommon.getString("select Doc_No from qryColdStorageHeadDetailBill where ltid in( " + hdnfcsdetail.Value + ") and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + "");
                    if (doc_no != string.Empty && doc_no == "0")
                    {


                        string currentDoc_No = txtDoc_No.Text;
                        DataSet ds = new DataSet();
                        string strrev = "";
                        using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                        {
                            XElement root = new XElement("ROOT");
                            XElement child1 = new XElement("RetailHead");
                            int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
                            int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
                            string strRev = string.Empty;
                            child1.SetAttributeValue("Doc_No", txtDoc_No.Text);
                            child1.SetAttributeValue("Company_Code", Company_Code);
                            child1.SetAttributeValue("Year_Code", Year_Code);
                            child1.SetAttributeValue("csid", hdnfid.Value);
                            root.Add(child1);
                            string XMLReport = root.ToString();
                            XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
                            XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
                            string spname = "ColdStorageInword";
                            string xmlfile = XMLReport;
                            string op = "";
                            string returnmaxno = "";
                            int flag = 10;
                            xml_ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);
                            strrev = op;

                        }
                        string query = "";
                        if (strrev == "-3")
                        {
                            Response.Redirect("../Inword/pgeColdStorageInwordUtility.aspx", false);
                        }
                        this.enableDisableNavigateButtons();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('This Record Use in Cold Storage DocNo: "+doc_no+"')", true);

                    }
                }
                else
                {
                    lblMsg.Text = "Cannot delete this Group , it is in use";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
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
       // hdnf.Value = Request.QueryString["csid"];

       // hdnf.Value = clsCommon.getString("select max(csid) from " + tblHead + " where Company_Code=" + Session["Company_Code"].ToString() + " " +
       //" and Year_Code=" + Session["year"].ToString());

       // Response.Redirect("pgeColdStorageInword.aspx?csid=" + hdnf.Value + "&Action=" + 1);
        string max = clsCommon.getString("select max(csid) as id from ColdStorageHead where Company_Code=" + Session["Company_Code"].ToString() + " " +
                  " and Year_Code=" + Session["year"].ToString());
        hdnf.Value = max;

        clsButtonNavigation.enableDisable("S");
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);
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
                        Label lblCreatedDate = (Label)Master.FindControl("MasterlblCreatedDate");
                        Label lblModifiedDate = (Label)Master.FindControl("MasterlblModifiedDate");
                        if (lblCreatedDate != null)
                        {
                            if (dt.Rows[0]["Created_Date"].ToString() == string.Empty)
                            {
                                lblCreatedDate.Text = "";
                            }
                            else
                            {
                                lblCreatedDate.Text = "Created Date" + dt.Rows[0]["Created_Date"].ToString();
                            }
                        }
                        if (lblModifiedDate != null)
                        {
                            if (dt.Rows[0]["Modified_Date"].ToString() == string.Empty)
                            {
                                lblModifiedDate.Text = "";
                            }
                            else
                            {
                                lblModifiedDate.Text = "Modified Date" + dt.Rows[0]["Modified_Date"].ToString();
                            }
                        }
                        //txtEditDoc_No.Text = dt.Rows[0]["EditDoc_No"].ToString();
                        hdnf.Value = dt.Rows[0]["csid"].ToString();
                        txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();
                        hdnfdoc.Value = txtDoc_No.Text;
                        hdnfid.Value = hdnf.Value;
                        hdnfacid.Value = dt.Rows[0]["Ac_Id"].ToString();
                        //lblDoc_No.Text = dt.Rows[0][""].ToString();
                        //drpTran_Type.SelectedValue=dt.Rows[0]["Tran_Type"].ToString();
                        txtDoc_Date.Text = dt.Rows[0]["Doc_Dateconvert"].ToString();
                        txtChallan_No.Text = dt.Rows[0]["Inword_No"].ToString();
                        txtChallan_Date.Text = dt.Rows[0]["Inword_Dateconvert"].ToString();
                        txtVahical_No.Text = dt.Rows[0]["Vahical_No"].ToString();
                        txtParty_Code.Text = dt.Rows[0]["Ac_Code"].ToString();
                        lblParty_Code.Text = dt.Rows[0]["Ac_Name_E"].ToString();
                        lblpartyStatecode.Text = dt.Rows[0]["GSTStateCode"].ToString();
                        //txtParty_Code.Text = dt.Rows[0]["Item_Code"].ToString();
                        //lblItemname.Text = dt.Rows[0]["System_Name_E"].ToString();
                        //txtBrand_Code.Text = dt.Rows[0]["Brand_Code"].ToString();
                        //lblBrandname.Text = dt.Rows[0]["brandname"].ToString();
                        //txtQty.Text = dt.Rows[0]["No_Of_Bags"].ToString();
                        //txtWtper.Text = dt.Rows[0]["Wt_Per"].ToString();
                        //txtNetkg.Text = dt.Rows[0]["Net_Wt"].ToString();

                        recordExist = true;
                        lblMsg.Text = "";
                        #region Details
                        qry = "SELECT Detail_Id ,Item_Code,System_Name_E as Item_Code_Name ,Brand_Code ,brandname as Brand_Code_Name,No_Of_Bags as Qty ,Wt_Per as Wtper ,Net_Wt as Netkg ," +
                            "'' as rowAction,'' as SrNo,ic,csdetailid from " + qryCommon +
                            " where csid=" + hdnf.Value + " and Company_Code=" + Session["Company_Code"].ToString() +
                            " and Year_Code=" + Session["year"].ToString();
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    // dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                    //dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                                    string csid = "";
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        csid = csid+dt.Rows[i]["csdetailid"].ToString() + ",";
                                        dt.Rows[i]["rowAction"] = "N";
                                        dt.Rows[i]["SrNo"] = i + 1;
                                    }
                                    csid = csid.Remove(csid.Length - 1 , 1);
                                    hdnfcsdetail.Value = csid.ToString();
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
                        pnlgrdDetail.Enabled = false;
                    }
                }
            }
            hdnf.Value = txtDoc_No.Text;
            this.enableDisableNavigateButtons();
            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtEditDoc_No")
            {
                setFocusControl(txtEditDoc_No);
            }
            if (strTextBox == "txtDoc_No")
            {
                setFocusControl(txtDoc_No);
            }

            if (strTextBox == "txtDoc_Date")
            {
                try
                {
                    //string dt = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    ////if (clsCommon.isValidDateforOp(dt) == true || dt == "")
                    //if (dt == "")
                    //{
                    //    setFocusControl(txtDoc_Date);
                    //}
                    //else
                    //{
                    //    txtDoc_Date.Text = "";
                    //    setFocusControl(txtDoc_Date);
                    //}
                }
                catch
                {
                    txtDoc_Date.Text = "";
                    setFocusControl(txtDoc_Date);
                }
            }
            if (strTextBox == "txtChallan_No")
            {
                setFocusControl(txtChallan_No);
            }
            if (strTextBox == "txtChallan_Date")
            {
                try
                {
                    //    string dt = DateTime.Parse(txtChallan_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    //    //if (clsCommon.isValidDateforOp(dt) == true || dt == "")
                    //    if (dt == "")
                    //    {
                    //        setFocusControl(txtChallan_Date);
                    //    }
                    //    else
                    //    {
                    //        txtChallan_Date.Text = "";
                    //        setFocusControl(txtChallan_Date);
                    //    }
                }
                catch
                {
                    txtChallan_Date.Text = "";
                    setFocusControl(txtChallan_Date);
                }
            }
            if (strTextBox == "txtVahical_No")
            {
                setFocusControl(txtVahical_No);
            }
            if (strTextBox == "txtParty_Code")
            {
                string acname = "";
                if (txtParty_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtParty_Code.Text);
                    if (a == false)
                    {
                        btntxtParty_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtParty_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {
                            lblParty_Code.Text = acname;
                            string acnamestatecode = clsCommon.getString("select GSTStateCode from qrymstaccountmaster where Ac_Code=" + txtParty_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            //hdnfpartystatecode.Value = acnamestatecode;
                            lblpartyStatecode.Text = acnamestatecode;
                            hdnfacid.Value = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtParty_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            setFocusControl(txtItem_Code);

                        }
                        else
                        {
                            txtParty_Code.Text = string.Empty;
                            lblParty_Code.Text = acname;
                            setFocusControl(txtParty_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtParty_Code);
                }
            }

            if (strTextBox == "txtItem_Code")
            {
                string acname = "";
                if (txtItem_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtItem_Code.Text);
                    if (a == false)
                    {
                        btntxtItem_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select System_Name_E from NT_1_SystemMaster where System_Type='I' and System_Code=" + txtItem_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {
                            lblItemname.Text = acname;
                            setFocusControl(txtBrand_Code);
                        }
                        else
                        {
                            txtItem_Code.Text = string.Empty;
                            lblItemname.Text = acname;
                            setFocusControl(txtItem_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(btnSave);
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
                        acname = clsCommon.getString("select Marka from Brand_Master where Mal_Code=" + txtItem_Code.Text + "  and Code=" + txtBrand_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty && acname !="0")
                        {
                            lblBrandname.Text = acname;

                            setFocusControl(txtQty);

                        }
                        else
                        {
                            txtBrand_Code.Text = string.Empty;
                            lblBrandname.Text = acname;
                            setFocusControl(txtBrand_Code);
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
                setFocusControl(txtWtper);
            }
            if (strTextBox == "txtWtper")
            {
                setFocusControl(txtNetkg);
            }
            if (strTextBox == "txtNetkg")
            {
                setFocusControl(btnAdddetails);
            }
            //calculation();
        }
        catch
        {
        }
    }
    #endregion

    #region calculation
    private void calculation()
    {
        double Qty = txtQty.Text != string.Empty ? Convert.ToDouble(txtQty.Text) : 0.00;
        double Wtper = txtWtper.Text != string.Empty ? Convert.ToDouble(txtWtper.Text) : 0.00;
        double Netkg = txtNetkg.Text != string.Empty ? Convert.ToDouble(txtNetkg.Text) : 0.00;

        Netkg = Qty * Wtper;

        txtNetkg.Text = Netkg.ToString();
    }
    #endregion
    #region
    private void gridcalculation()
    {
        double netwt = 0.00;

        if (grdDetail.Rows.Count > 0)
        {
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                if (grdDetail.Rows[i].Cells[Rowaction].Text != "D")
                {
                    double wt = Convert.ToDouble(grdDetail.Rows[i].Cells[Wtper].Text.Trim());
                    netwt = netwt + wt;
                }
            }
        }
    }

    #endregion

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = " select * from " + qryCommon + " where csid=" + hdnf.Value + " ";
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
                txtDoc_No.Text = hdnf.Value;
                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Focus();
                }
                this.enableDisableNavigateButtons();
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
        pnlPopupDetails.Style["display"] = "block";
    }
    #endregion

    #region [btnAdddetails_Click]
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 1;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();
            if (ViewState["currentTable"] != null)
            {
                dt = (DataTable)ViewState["currentTable"];
                if (dt.Rows[0]["Detail_Id"].ToString().Trim() != "")
                {
                    if (btnAdddetails.Text == "ADD")
                    {
                        dr = dt.NewRow();
                        #region calculate rowindex
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = Convert.ToInt32(dt.Rows[i]["Detail_Id"].ToString());
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
                        dr["Detail_Id"] = rowIndex;     //auto
                        dr["rowAction"] = "A";
                        dr["SrNo"] = 0;
                    }
                    else
                    {
                        //update row
                        int n = Convert.ToInt32(lblNo.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["Detail_Id"] = rowIndex;
                        dr["SrNo"] = 0;
                        #region decide whether actual row is updating or virtual [rowAction]
                        string id = clsCommon.getString("select Detail_Id from " + tblDetails + " where Detail_Id='" + lblID.Text + "' and Doc_No=" + txtDoc_No.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (id != string.Empty)
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
                    dt.Columns.Add((new DataColumn("Detail_Id", typeof(int))));
                    #region [Write here columns]

                    dt.Columns.Add((new DataColumn("Item_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Item_Code_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Brand_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Brand_Code_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Qty", typeof(double))));
                    dt.Columns.Add((new DataColumn("Wtper", typeof(double))));
                    dt.Columns.Add((new DataColumn("Netkg", typeof(double))));

                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dt.Columns.Add((new DataColumn("ic", typeof(int))));
                    dt.Columns.Add((new DataColumn("csdetailid", typeof(int))));

                    dr = dt.NewRow();
                    dr["Detail_Id"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("Detail_Id", typeof(int))));
                #region [Write here columns]
                dt.Columns.Add((new DataColumn("Item_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Item_Code_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Brand_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Brand_Code_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Qty", typeof(double))));
                dt.Columns.Add((new DataColumn("Wtper", typeof(double))));
                dt.Columns.Add((new DataColumn("Netkg", typeof(double))));

                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dt.Columns.Add((new DataColumn("ic", typeof(int))));
                dt.Columns.Add((new DataColumn("csdetailid", typeof(int))));
                dr = dt.NewRow();
                dr["Detail_Id"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            //dr["Tran_Type"] = txtTran_Type.Text;
            dr["Item_Code"] = txtItem_Code.Text;
            dr["Item_Code_Name"] = lblItemname.Text;
            if (txtBrand_Code.Text == string.Empty)
            {
                dr["Brand_Code"] = 0;
                dr["Brand_Code_Name"] = "";
            }
            else
            {
                dr["Brand_Code"] = txtBrand_Code.Text;
                dr["Brand_Code_Name"] = lblBrandname.Text;
            }
            dr["Qty"] = txtQty.Text;
            dr["Wtper"] = txtWtper.Text;
            dr["Netkg"] = txtNetkg.Text;

            ds = clsDAL.SimpleQuery("select * from qrymstitem where System_Code=" + txtItem_Code.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
            if (ds != null)
            {
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    dr["ic"] = Convert.ToInt32(dt1.Rows[0]["systemid"].ToString());
                }

            }

            #endregion
            if (btnAdddetails.Text == "ADD")
            {
                dr["csdetailid"] = 0;
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
                pnlPopupDetails.Style["display"] = "none";
                //setFocusControl(txtTran_Type);
            }
            else
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
            }
            // Empty Code->
            //   txtTran_Type.Text = string.Empty;
            txtItem_Code.Text = string.Empty;
            lblItemname.Text = string.Empty;
            txtBrand_Code.Text = string.Empty;
            lblBrandname.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtWtper.Text = string.Empty;
            txtNetkg.Text = string.Empty;
            lblBrandname.Text = string.Empty;

            setFocusControl(txtItem_Code);
            btnAdddetails.Text = "ADD";
            //gridcalculation();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DO", "javascript:GridCalculations()", true);
        }
        catch
        {
        }
    }
    #endregion

    #region [btnClosedetails_Click]
    protected void btnClosedetails_Click(object sender, EventArgs e)
    {
        lblNo.Text = string.Empty;
        lblID.Text = string.Empty;
        //txtTran_Type.Text = string.Empty;
        txtItem_Code.Text = string.Empty;
        lblItemname.Text = string.Empty;
        txtBrand_Code.Text = string.Empty;
        lblBrandname.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtWtper.Text = string.Empty;
        txtNetkg.Text = string.Empty;

        btnAdddetails.Text = "ADD";
        setFocusControl(txtItem_Code);
        pnlPopupDetails.Style["display"] = "none";
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[Srno].Text);//srno row id;
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);//Detail id;
        //txtTran_Type.Text = Server.HtmlDecode(gvrow.Cells[Tran_Type].Text);
        txtItem_Code.Text = Server.HtmlDecode(gvrow.Cells[Item_Code].Text);
        lblItemname.Text = Server.HtmlDecode(gvrow.Cells[Itemaname].Text);
        txtBrand_Code.Text = Server.HtmlDecode(gvrow.Cells[Brand_Code].Text);
        lblBrandname.Text = Server.HtmlDecode(gvrow.Cells[brandname].Text.ToString());
        txtQty.Text = Server.HtmlDecode(gvrow.Cells[Qty].Text);
        txtWtper.Text = Server.HtmlDecode(gvrow.Cells[Wtper].Text);
        txtNetkg.Text = Server.HtmlDecode(gvrow.Cells[Netkg].Text);

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
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["Detail_Id"].ToString());
                string IDExisting = clsCommon.getString("select Doc_No from " + tblDetails + " where Detail_Id='" + ID + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "D";// rowAction Index add 
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "N";// Add rowaction id
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
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "R";       // add row action R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "A";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "A";
                    }
                }
                ViewState["currentTable"] = dt;
                gridcalculation();
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
            // if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            // e.Row.Cells[2].Width = new Unit("120px");
            e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Rowaction].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Srno].ControlStyle.Width = new Unit("30px");
            //--------------------------------------------------

            //--------------------------------------------------
            e.Row.Cells[Item_Code].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Item_Code].Style["overflow"] = "hidden";
            e.Row.Cells[Item_Code].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[Itemaname].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Itemaname].Style["overflow"] = "hidden";
            e.Row.Cells[Itemaname].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Brand_Code].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Brand_Code].Style["overflow"] = "hidden";
            e.Row.Cells[Brand_Code].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[brandname].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[brandname].Style["overflow"] = "hidden";
            e.Row.Cells[brandname].HorizontalAlign = HorizontalAlign.Left;

            //--------------------------------------------------
            e.Row.Cells[Qty].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Qty].Style["overflow"] = "hidden";
            e.Row.Cells[Qty].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Wtper].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Wtper].Style["overflow"] = "hidden";
            e.Row.Cells[Wtper].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Netkg].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[Netkg].Style["overflow"] = "hidden";
            e.Row.Cells[Netkg].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[ic].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[ic].Style["overflow"] = "hidden";
            e.Row.Cells[ic].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[csdetailid].ControlStyle.Width = new Unit("50px");
            e.Row.Cells[csdetailid].Style["overflow"] = "hidden";
            e.Row.Cells[csdetailid].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            //     e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            //    e.Row.Cells[0].Style["overflow" ] = "hiden";
            e.Row.Cells[Rowaction].Visible = true;
            e.Row.Cells[Srno].Visible = true;

            //}
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
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            e.Row.Cells[0].Width = new Unit("120px");
            e.Row.Cells[0].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            //    e.Row.Cells[0].Style["overflow" ] = "hidden";
            //    e.Row.Cells[0].Visible =true;
        }
    }
    #endregion
    #region [grdPopup_PageIndexChanging]
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
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
                        if (grdDetail.Rows[rowindex].Cells[Rowaction].Text != "D" && grdDetail.Rows[rowindex].Cells[Rowaction].Text != "R")//add row action id
                        {
                            pnlPopupDetails.Style["display"] = "none";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "UPDATE";
                            setFocusControl(txtItem_Code);
                        }
                        break;
                    case "DeleteRecord":
                        string action = "";

                        DataTable dt1 = (DataTable)ViewState["currentTable"];
                        int refid = Convert.ToInt32(dt1.Rows[rowindex]["csdetailid"].ToString());

                        string reftenderid = clsCommon.getString(" select doc_no from qryColdStorageHeadDetailBill where ltid=" + refid + "");
                        string doid = clsCommon.getString(" select doc_no from qryColdStorageHeadDetailBill where ltid=" + refid + "");

                          string concat = string.Empty;

                       
                        if (reftenderid != string.Empty && reftenderid != "0")
                        {
                            concat = concat + "Cold Storage Inword DocNo: " + reftenderid + "";
                        }
                        if (doid == "0" && reftenderid == "0")
                        {

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
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('This Record Use In " + concat + "')", true);
                        }
                        break;
                }
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [txtEditDoc_No_TextChanged]
    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);

            if (a == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Only Numbers Allow!')", true);
            }
            else
            {
                //string qry = "select * from " + qryHead + " where doc_no='" + txtEditDoc_No.Text + "' and company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code="+Session["year"].ToString()+"";
                //this.fetchRecord(qry);
                //setFocusControl(txtEditDoc_No);

                string qry = "select * from " + qryCommon + " where doc_no='" + txtEditDoc_No.Text + "' and " +
                    "company_code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Session["year"].ToString();
                this.fetchRecord(qry);
                setFocusControl(txtEditDoc_No);

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
    #region [btntxtDoc_No_Click]
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
    #endregion

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            string searchtxt = "";
            string delimStr = "";
            char[] delimiter = delimStr.ToCharArray();
            string words = "";
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
                    foreach (var s in split)
                    {
                        string aa = s.ToString();
                        name += "doc_no Like '%" + aa + "%'or";
                    }
                    name = name.Remove(name.Length - 2);

                    lblPopupHead.Text = "--Select DOC No--";
                    string qry = "select doc_no,doc_date,PartyName,PartyCity from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                        " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                        " and " + name + " order by PartyName asc";
                    this.showPopup(qry);
                }
            }


            if (hdnfClosePopup.Value == "txtParty_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "Ac_Code Like '%" + aa + "%'or Ac_Name_E like '%" + aa + "%' or city_name_e like '%" + aa + "%' or";
                }
                name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select--";
                //string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //    " and Locked=0  and (" + name + ") order by Ac_Name_E";

                //this.showPopup(qry);
                string qry = " Locked=0  and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
             + " and (" + name + ") order by Ac_Name_E ";

                this.showPopupAccountMaster(qry);
            }
            if (hdnfClosePopup.Value == "txtShipto")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "Ac_Code Like '%" + aa + "%'or Ac_Name_E like '%" + aa + "%' or CityName like '%" + aa + "%' or";
                }
                name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Locked=0  and (" + name + ") order by Ac_Name_E";

                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtParty_name_new")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "Ac_Code Like '%" + aa + "%'or Ac_Name_E like '%" + aa + "%' or CityName like '%" + aa + "%' or";
                }
                name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Ac_type='CR' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Locked=0  and (" + name + ") order by Ac_Name_E";

                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBroker_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "Ac_Code Like '%" + aa + "%'or Ac_Name_E like '%" + aa + "%' or CityName like '%" + aa + "%' or";
                }
                name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select Party Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code=" +
                    Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and Locked=0  and Ac_type='BR' and (" + name + ") order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtItem_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "System_Code Like '%" + aa + "%'or System_Name_E like '%" + aa + "%' or";
                }
                name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select Item--";
                string qry = "select System_Code,System_Name_E as Item_Name from  NT_1_SystemMaster "
                    + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " AND (" + name + ")";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtBrand_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "Code Like '%" + aa + "%'or Marka like '%" + aa + "%' or";
                }
                name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select--";
                string qry = " select Code,Marka from Brand_Master where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                    + "and Mal_Code=" + txtItem_Code.Text + "and (" + name + ")";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtGST_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    name += "GST_Name Like '%" + aa + "%'or";
                }
                name = name.Remove(name.Length - 2);
                lblPopupHead.Text = "--Select Item--";
                string qry = "select Doc_no,GST_Name,Rate from " + tblPrefix + "GSTRateMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    " and (" + name + ") order by GST_Name"; ;
                this.showPopup(qry);
            }
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
                searchString = txtSearchText.Text;
                strTextBox = hdnfClosePopup.Value;
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
        try
        {
            //if (hdnfyearcode.Value != Session["year"].ToString())
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records year code & current year code is not same!')", true);
            //    return;
            //    // Response.Redirect("~/Sugar/pgeHome.aspx", false);
            //}
            //if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
            //    return;

            //}
            string date = txtChallan_Date.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "retailSale", "javascript:pagevalidation();", true);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtDoc_Date_TextChanged]
    protected void txtDoc_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_Date.Text;
        strTextBox = "txtDoc_Date";
        csCalculations();
    }
    #endregion
    #region [txtChallan_No_TextChanged]
    protected void txtChallan_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtChallan_No.Text;
        strTextBox = "txtChallan_No";
        csCalculations();
    }
    #endregion
    #region [txtChallan_Date_TextChanged]
    protected void txtChallan_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtChallan_Date.Text;
        strTextBox = "txtChallan_Date";
        csCalculations();
    }
    #endregion
    #region [txtVahical_No_TextChanged]
    protected void txtVahical_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtVahical_No.Text;
        strTextBox = "txtVahical_No";
        csCalculations();
    }
    #endregion
    #region [txtParty_Code_TextChanged]
    protected void txtParty_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtParty_Code.Text;
        strTextBox = "txtParty_Code";
        csCalculations();
    }
    #endregion
    #region [btntxtParty_Code_Click]
    protected void btntxtParty_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtParty_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [btntxtBroker_Code_Click]
    protected void btntxtBroker_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBroker_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
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


    #region [txtWtper_TextChanged]
    protected void txtWtper_TextChanged(object sender, EventArgs e)
    {
        searchString = txtWtper.Text;
        strTextBox = "txtWtper";
        csCalculations();
    }
    #endregion


    #region [txtNetkg_TextChanged]
    protected void txtNetkg_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNetkg.Text;
        strTextBox = "txtNetkg";
        csCalculations();
    }
    #endregion
    protected void btntxtParty_name_new_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtParty_name_new";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void btntxtShipto_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtShipto";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

}