using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;
using System.Data.SqlClient;
using System.Configuration;
public partial class Sugar_pgeLetter : System.Web.UI.Page
{
    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string qryAccountList = string.Empty;
    int defaultAccountCode = 0;
    static WebControl objAsp = null;
    string user = string.Empty;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
     string Action = string.Empty;
    int count = 0;
    Int32 maxDoc_No = 0;
    int counts = 0;

    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    string cs = string.Empty;
    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            user = Session["user"].ToString();
            tblHead = tblPrefix + "Letter";
            tblDetails = tblPrefix + "";
            AccountMasterTable = tblPrefix + "AccountMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            qryCommon = tblPrefix + "qryTenderList";
            qryAccountList = tblPrefix + "qryAccountsList";
            pnlPopup.Style["display"] = "none";

            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);

            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    Action = Request.QueryString["Action"];
                    if (Action == "1")
                    {
                        hdnf.Value = Request.QueryString["Doc_no"];
                        clsButtonNavigation.enableDisable("N");
                        pnlPopup.Style["display"] = "none";
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
                        setFocusControl(txtdoc_date);
                    }
                    //pnlPopup.Style["display"] = "none";
                    //ViewState["currentTable"] = null;
                    //clsButtonNavigation.enableDisable("N");
                    //this.makeEmptyForm("N");
                    //ViewState["mode"] = "I";
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
                                    txtDoc_no.Text = ds.Tables[0].Rows[0][0].ToString();
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
                txtEditDoc_No.Enabled = true;
                btnSave.Text = "Save";
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = false;
                lblMsg.Text = string.Empty;
                btnSaveNew.Enabled = false;
                #region set Business logic for save
                calenderExtenderDate.Enabled = false;
                CalendarExtendertxtref_dt.Enabled = false;
                btntxtac_code.Enabled = false;
                #endregion
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
                btnSaveNew.Enabled = false;
                btnSave.Text = "Save";
                btntxtdoc_no.Text = "Change No";
                btntxtdoc_no.Enabled = false;

                txtDoc_no.Enabled = false;
                txtEditDoc_No.Enabled = false;
                #region set Business logic for save
                calenderExtenderDate.Enabled = true;
                CalendarExtendertxtref_dt.Enabled = true;
                txtdoc_date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                btntxtac_code.Enabled = true;
                #endregion
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
                btnSaveNew.Enabled = false;
                #region set Business logic for save
                calenderExtenderDate.Enabled = false;
                CalendarExtendertxtref_dt.Enabled = false;
                btntxtac_code.Enabled = false;
                #endregion
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
                btnSaveNew.Enabled = true;
                btntxtdoc_no.Text = "Choose No";
                btntxtdoc_no.Enabled = true;
                lblMsg.Text = string.Empty;
                #region set Business logic for save
                calenderExtenderDate.Enabled = true;
                CalendarExtendertxtref_dt.Enabled = true;
                btntxtac_code.Enabled = true;
                #endregion
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
        #region
        //try
        //{
        //    string qry = string.Empty;
        //    qry = "select max(doc_no) as doc_no from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
        //    DataSet ds = new DataSet();
        //    DataTable dt = new DataTable();
        //    ds = clsDAL.SimpleQuery(qry);
        //    if (ds != null)
        //    {
        //        if (ds.Tables.Count > 0)
        //        {
        //            dt = ds.Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                hdnf.Value = dt.Rows[0]["doc_no"].ToString();
        //                qry = getDisplayQuery();
        //                bool recordExist = this.fetchRecord(qry);
        //                if (recordExist == true)
        //                {
        //                    btnEdit.Focus();
        //                }
        //                else                     //new code
        //                {
        //                    btnEdit.Enabled = false;
        //                    btnDelete.Enabled = false;
        //                }
        //            }
        //        }
        //    }
        //    this.enableDisableNavigateButtons();
        //}
        //catch
        //{
        //}
        #endregion
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
        query = "select count(*) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'";
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
        if (txtDoc_no.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no>" + Convert.ToInt32(hdnf.Value) + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY doc_no asc  ";
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
                query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no<" + Convert.ToInt32(hdnf.Value) + "and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " ORDER BY doc_no asc  ";
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
            }
        }
                #endregion
        #endregion
    }
    #endregion

    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select doc_no from " + tblHead + " where doc_no=(select MIN(doc_no) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
            if (txtDoc_no.Text != string.Empty)
            {
                string query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no<" + Convert.ToInt32(hdnf.Value) +
                            "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY doc_no DESC  ";
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
            if (txtDoc_no.Text != string.Empty)
            {
                string query = "SELECT top 1 [doc_no] from " + tblHead + " where doc_no>" + Convert.ToInt32(hdnf.Value) +
                            "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                            " ORDER BY doc_no asc  ";
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
            query = "select doc_no from " + tblHead + " where doc_no=(select MAX(doc_no) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion

    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;

            counts = Convert.ToInt32(clsCommon.getString("select count(Doc_no) as A from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code='"+Session["year"] +"' "));
            if (counts == 0)
            {
                txtDoc_no.Text = "1";
                maxDoc_No = 1;
            }
            else
            {
                maxDoc_No = Convert.ToInt32(clsCommon.getString("SELECT max(Doc_no) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code='" + Session["year"] + "'")) + 1;
                txtDoc_no.Text = maxDoc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(Doc_no) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code='" + Session["year"] + "'"));
            
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
        //this.getMaxCode();
        this.NextNumber();
       // pnlPopupDetails.Style["display"] = "none";
        setFocusControl(txtdoc_date);
    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");

        this.makeEmptyForm("E");
        txtDoc_no.Enabled = false;
        setFocusControl(txtdoc_date);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        #region
        //try
        //{
        //    string currentDoc_No = txtDoc_no.Text;
        //    if (hdconfirm.Value == "Yes")
        //    {

        //        DataSet ds = new DataSet();
        //        string strrev = "";
        //        using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
        //        {
        //            obj.flag = 3;
        //            obj.tableName = tblHead;
        //            obj.columnNm = "   Doc_No=" + currentDoc_No +
        //            " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

        //            obj.values = "none";
        //            ds = obj.insertAccountMaster(ref strrev);
        //            if (strrev == "-3")
        //            {
        //                obj.flag = 3;
        //                obj.tableName = tblDetails;
        //                obj.columnNm = "   Doc_No=" + currentDoc_No + " '" +
        //                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());

        //                obj.values = "none";
        //                ds = obj.insertAccountMaster(ref strrev);
        //            }
        //        }
        //        string query = "";

        //        if (strrev == "-3")
        //        {
        //            query = "SELECT top 1 [Doc_No] from " + tblHead + "  where Doc_No>" + Convert.ToInt32(currentDoc_No) +
        //                   "   and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
        //                    " ORDER BY Doc_No asc  ";


        //            hdnf.Value = clsCommon.getString(query);

        //            if (hdnf.Value == string.Empty)
        //            {
        //                query = "SELECT top 1 [Doc_No] from " + tblHead + "  where Doc_No<" + Convert.ToInt32(currentDoc_No) +
        //                     "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
        //                    " ORDER BY Doc_No desc  ";
        //                hdnf.Value = clsCommon.getString(query);
        //            }

        //            if (hdnf.Value != string.Empty)
        //            {

        //                query = getDisplayQuery();
        //                bool recordExist = this.fetchRecord(query);


        //                this.makeEmptyForm("S");
        //                clsButtonNavigation.enableDisable("S");
        //            }

        //            else
        //            {
        //                this.makeEmptyForm("N");
        //                //new code

        //                clsButtonNavigation.enableDisable("N");         //No record exist  Last record deleted.
        //                btnEdit.Enabled = false;
        //                btnDelete.Enabled = false;
        //            }
        //        }
        //        this.enableDisableNavigateButtons();
        //    }
        //}

        //catch
        //{
        //}
        #endregion

        try
        {
            if (hdconfirm.Value == "Yes")
            {
               
                string str = clsCommon.getString("select Ac_Code from " + AccountMasterTable + " where Doc_no=" + txtDoc_no.Text + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");

                if (str == string.Empty)   //Gledger does not contain this account then delete
                {
                    string currentDoc_No = txtDoc_no.Text;
                    int flag = 3;
                    string strrev = "";

                    qry = "delete from " + tblHead + " where Doc_no='" + currentDoc_No + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString())+"";
                    Thread thred = new Thread(() => { count = DataStore(qry, flag); }); //Calling DataStore Method Using Thread
                    thred.Start(); //Thread Operation Start
                    thred.Join();

                    if (count == 3)
                    {
                        Response.Redirect("../Master/pgeLetterUtility.aspx");
                    }


                }
                else
                {
                    lblMsg.Text = "Cannot delete this Group , it is in use";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                hdnf.Value = txtDoc_no.Text;
                showLastRecord();
            }
        }
        catch
        {
        }
    }
    #endregion
    private int DataStore(string Query, int flag)
    {
        int count = 0;
        try
        {
            //Connection open
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            ///Execution
            myTran = con.BeginTransaction();
            //cmd.CommandText = qry;
            //cmd.Connection = con;
            //cmd.Transaction = myTran;
            cmd = new SqlCommand(Query, con, myTran);

            cmd.ExecuteNonQuery();
            myTran.Commit();
            Thread.Sleep(100);
            if (flag == 1)
            {
                count = 1;
            }
            else if (flag == 2)
            {
                count = 2;
            }
            else
            {
                count = 3;
            }

            return count;
        }
        catch
        {
            if (myTran != null)
            {
                myTran.Rollback();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('check Entry AND Try Again !')", true);

            }
            return count;

        }
        finally
        {
            con.Close();
        }

    }
    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (hdnf.Value != string.Empty)
        {
            string query = getDisplayQuery(); ;
            bool recordExist = this.fetchRecord(query);
        }
        else
        {
            this.showLastRecord();
        }

        clsButtonNavigation.enableDisable("S");
        this.enableDisableNavigateButtons();
        this.makeEmptyForm("S");
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
                        txtDoc_no.Text = dt.Rows[0]["DOC_NO"].ToString();
                        txtdoc_date.Text = dt.Rows[0]["DOC_DATE1"].ToString();
                        txtac_code.Text = dt.Rows[0]["AC_CODE"].ToString();
                        txtac_name.Text = dt.Rows[0]["AC_NAME"].ToString();
                        txtaddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                        txtCity.Text = dt.Rows[0]["CITY"].ToString();
                        txtPinCode.Text = dt.Rows[0]["PINCODE"].ToString();
                        txtkind_att.Text = dt.Rows[0]["KIND_ATT"].ToString();
                        txtsubject.Text = dt.Rows[0]["SUBJECT"].ToString();
                        txtref_no.Text = dt.Rows[0]["REF_NO"].ToString();
                        txtref_dt.Text = dt.Rows[0]["REF_DATE1"].ToString();
                        txtmatter.Text = dt.Rows[0]["MATTER"].ToString();
                        txtauthorised_person.Text = dt.Rows[0]["AUTHORISED_PERSON"].ToString();
                        txtdesignation.Text = dt.Rows[0]["DESIGNATION"].ToString();
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

                    }
                }
            }
            this.enableDisableNavigateButtons();
            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion



    #region gerDisplayQuery
    private string gerDisplayQuery()
    {
        try
        {
            string qryDisplay = "";
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = "select *,CONVERT(VARCHAR(10),DOC_DATE,103) as DOC_DATE1,CONVERT(VARCHAR(10),REF_DT,103) as REF_DATE1 from " + tblHead + " where DOC_NO=" + hdnf.Value + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
                txtDoc_no.Text = hdnf.Value;
                hdnfSuffix.Value = "";
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

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Width = new Unit("60px");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].Width = new Unit("80px");
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
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
    #region [txtdoc_no_TextChanged]
    protected void txtdoc_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_no.Text;
        strTextBox = "txtdoc_no";
        csCalculations();
    }
    #endregion


    #region [btntxtdoc_no_Click]
    protected void btntxtdoc_no_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtdoc_no";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtdoc_date_TextChanged]
    protected void txtdoc_date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtdoc_date.Text;
        strTextBox = "txtdoc_date";
        csCalculations();
    }
    #endregion

    #region [txtac_code_TextChanged]
    protected void txtac_code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtac_code.Text;
        strTextBox = "txtac_code";
        csCalculations();
    }
    #endregion

    #region [btntxtac_code_Click]
    protected void btntxtac_code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtac_code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtac_name_TextChanged]
    protected void txtac_name_TextChanged(object sender, EventArgs e)
    {
        searchString = txtac_name.Text;
        strTextBox = "txtac_name";
        csCalculations();
    }
    #endregion

    #region [txtaddress_TextChanged]
    protected void txtaddress_TextChanged(object sender, EventArgs e)
    {
        searchString = txtaddress.Text;
        strTextBox = "txtaddress";
        csCalculations();
    }
    #endregion

    #region [txtkind_att_TextChanged]
    protected void txtkind_att_TextChanged(object sender, EventArgs e)
    {
        searchString = txtkind_att.Text;
        strTextBox = "txtkind_att";
        csCalculations();
    }
    #endregion

    #region [txtsubject_TextChanged]
    protected void txtsubject_TextChanged(object sender, EventArgs e)
    {
        searchString = txtsubject.Text;
        strTextBox = "txtsubject";
        csCalculations();
    }
    #endregion

    #region [txtref_no_TextChanged]
    protected void txtref_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtref_no.Text;
        strTextBox = "txtref_no";
        csCalculations();
    }
    #endregion

    #region [txtref_dt_TextChanged]
    protected void txtref_dt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtref_dt.Text;
        strTextBox = "txtref_dt";
        csCalculations();
    }
    #endregion

    #region [txtmatter_TextChanged]
    protected void txtmatter_TextChanged(object sender, EventArgs e)
    {
        searchString = txtmatter.Text;
        strTextBox = "txtmatter";
        csCalculations();
    }
    #endregion

    #region [txtauthorised_person_TextChanged]
    protected void txtauthorised_person_TextChanged(object sender, EventArgs e)
    {
        searchString = txtauthorised_person.Text;
        strTextBox = "txtauthorised_person";
        csCalculations();
    }
    #endregion

    #region [txtdesignation_TextChanged]
    protected void txtdesignation_TextChanged(object sender, EventArgs e)
    {
        searchString = txtdesignation.Text;
        strTextBox = "txtdesignation";
        csCalculations();
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
                      
                        pnlPopup.Style["display"] = "block";
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                     
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

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //if (searchString != string.Empty)
            //{
            //    txtSearchText.Text = searchString;
            //}
            //if (hdnfClosePopup.Value == "txtdoc_no")
            //{
            //    lblPopupHead.Text = "--Select Doc No--";
            //    string qry = "select DOC_NO,Convert(varchar(10),DOC_DATE,103) as LETTER_DATE,AC_NAME,SUBJECT from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
            //        " and (DOC_NO like '%" + txtSearchText.Text + "%' or DOC_DATE like '%" + txtSearchText.Text + "%' or AC_NAME like '%" + txtSearchText.Text + "%')";
            //    this.showPopup(qry);
            //}
            //if (hdnfClosePopup.Value == "txtac_code")
            //{
            //    lblPopupHead.Text = "--Select Party--";
            //    string qry = "select a.Ac_Code,a.Ac_Name_E,c.city_name_e from " + tblPrefix + "AccountMaster a left outer join " + tblPrefix + "CityMaster c on a.City_Code=c.city_code and a.Company_Code=c.company_code where a.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
            //        " and (a.Ac_Code like '%" + txtSearchText.Text + "%' or a.Ac_Name_E like '%" + txtSearchText.Text + "%' or c.city_name_e like '%" + txtSearchText.Text + "%' ) order by a.Ac_Name_E desc";
            //    this.showPopup(qry);
            //}

            pnlPopup.Style["display"] = "block";
            string searchtxt = searchString;
            string delimStr = "";
            char[] delimiter = delimStr.ToCharArray();
            string words = searchString;
            string[] split = null;
            string name = string.Empty;
            if (searchString != string.Empty || strTextBox == hdnfClosePopup.Value)
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

            if (hdnfClosePopup.Value == "txtdoc_no" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                if (btntxtdoc_no.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtDoc_no.Text = string.Empty;
                    txtDoc_no.Enabled = true;

                    btnSave.Enabled = false;
                    setFocusControl(txtDoc_no);
                    hdnfClosePopup.Value = "Close";
                }

                if (btntxtdoc_no.Text == "Choose No")
                {
                    lblPopupHead.Text = "--Select Group--";

                    string qry = "select DOC_NO,Convert(varchar(10),DOC_DATE,103) as LETTER_DATE,AC_NAME,SUBJECT from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +

                        " and (DOC_NO like '%" + txtSearchText.Text + "%' or AC_NAME like '%" + txtSearchText.Text + "%')";


                    this.showPopup(qry);
                }
            }
            if (hdnfClosePopup.Value == "txtac_code")
            {
                //lblPopupHead.Text = "--Select Purchase AC--";
                //string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and" +
                //    " (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName  like '%" + txtSearchText.Text + "%' )";
                //this.showPopup(qry);
                string qry = " Locked=0   and dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                     + " and (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%'  or city_name_e like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E desc";

                this.showPopupAccountMaster(qry);
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
                        hdnfpagecount.Value = grdPopup.PageCount.ToString();
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                        hdnfpagecount.Value = "0";
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
        #region [Validation Part]
        bool isValidated = true;
        if (txtDoc_no.Text != string.Empty)
        {
            if (ViewState["mode"] != null)
            {
                if (ViewState["mode"].ToString() == "I")
                {
                    string str = clsCommon.getString("select Doc_No from " + tblHead + " where Doc_No=" + txtDoc_no.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                    if (str != string.Empty&&str!="0")
                    {
                        lblMsg.Text = "Code " + txtDoc_no.Text + " already exist";
                        this.NextNumber();
                        hdnf.Value = txtDoc_no.Text;
                        isValidated = true;
                    }
                    else
                    {
                        isValidated = true;
                    }
                }
            }
        }
        else
        {
            isValidated = false;
            setFocusControl(txtDoc_no);
        }
        if (txtdoc_date.Text != string.Empty)
        {
            string dt = DateTime.Parse(txtdoc_date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            if (clsCommon.isValidDate(dt) == true)
            {
                //setFocusControl(txtac_code);
                isValidated = true;
            }
            else
            {
                txtdoc_date.Text = string.Empty;
                setFocusControl(txtdoc_date);
                isValidated = false;
                return;
            }
        }
        else
        {
            isValidated = false;
            setFocusControl(txtdoc_date);
            return;
        }
        if (txtac_code.Text != string.Empty)
        {
            string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtac_code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            if (str != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtac_code);
                return;
            }
        }
        else
        {
            isValidated = false;
            setFocusControl(txtac_code);
            return;
        }
        if (txtac_name.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtac_name);
            return;
        }
        if (txtaddress.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtaddress);
            return;
        }

        if (txtsubject.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtsubject);
            return;
        }
        if (txtref_no.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtref_no);
            return;
        }
        if (txtref_dt.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtref_dt);
            return;
        }
        if (txtmatter.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtmatter);
            return;
        }

        #endregion

        #region -Head part declearation
        Int32 DOC_NO = txtDoc_no.Text != string.Empty ? Convert.ToInt32(txtDoc_no.Text) : 0;
        string DOC_DATE = DateTime.Parse(txtdoc_date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        Int32 AC_CODE = txtac_code.Text != string.Empty ? Convert.ToInt32(txtac_code.Text) : 0;
        string AC_NAME = txtac_name.Text;
        string ADDRESS = txtaddress.Text;
        string CITY = txtCity.Text;
        string PINCODE = txtPinCode.Text;
        string KIND_ATT = txtkind_att.Text;
        string SUBJECT = txtsubject.Text;
        string REF_NO = txtref_no.Text;
        string REF_DT = DateTime.Parse(txtref_dt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        string s = txtmatter.Text;
        string MATTER = s.Replace("\r\n", "<br/>");
        string AUTHORISED_PERSON = txtauthorised_person.Text;
        string DESIGNATION = txtdesignation.Text;
        string retValue = string.Empty;
        string strRev = string.Empty;
        int Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
        int Year_Code = Convert.ToInt32(Session["year"].ToString());
        int year_Code = Convert.ToInt32(Session["year"].ToString());
        int Branch_Code = Convert.ToInt32(Session["Branch_Code"].ToString());
        string userinfo = clsGV.userInfo + DateTime.Now.ToString("dd/MM/yyyy:HHmmss");
        #endregion-End of Head part declearation

        #region save Head Master
        using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
        {
            if (ViewState["mode"] != null)
            {
                DataSet ds = new DataSet();
                if (ViewState["mode"].ToString() == "I")
                {
                    obj.flag = 1;
                    obj.tableName = tblHead;
                    obj.columnNm = "DOC_NO,DOC_DATE,AC_CODE,AC_NAME,ADDRESS,CITY,PINCODE,KIND_ATT,SUBJECT,REF_NO,REF_DT,MATTER,AUTHORISED_PERSON,DESIGNATION,Company_Code,Year_Code,Branch_Code,Created_By";
                    obj.values = "'" + DOC_NO + "','" + DOC_DATE + "','" + AC_CODE + "','" + AC_NAME + "','" + ADDRESS + "','" + CITY + "','" + PINCODE + "','" + KIND_ATT + "','" + SUBJECT + "','" + REF_NO + "','" + REF_DT + "','" + MATTER + "','" + AUTHORISED_PERSON + "','" + DESIGNATION + "','" + Convert.ToInt32(Session["Company_Code"].ToString()) + "','" + Convert.ToInt32(Session["year"].ToString()) + "','" + Convert.ToInt32(Session["Branch_Code"].ToString()) + "','" + user + "'";
                    ds = obj.insertAccountMaster(ref strRev);
                    retValue = strRev;
                }
                else
                {
                    //Update Mode
                    obj.flag = 2;
                    obj.tableName = tblHead;
                    obj.columnNm = "DOC_NO='" + DOC_NO + "',DOC_DATE='" + DOC_DATE + "',AC_CODE='" + AC_CODE + "',AC_NAME='" + AC_NAME + "',ADDRESS='" + ADDRESS + "',CITY='" + CITY + "',PINCODE='" + PINCODE + "',KIND_ATT='" + KIND_ATT + "',SUBJECT='" + SUBJECT + "',REF_NO='" + REF_NO + "',REF_DT='" + REF_DT + "',MATTER='" + MATTER + "',AUTHORISED_PERSON='" + AUTHORISED_PERSON + "',DESIGNATION='" + DESIGNATION + "',Modified_By='" + user + "' where DOC_NO='" + DOC_NO + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
                    obj.values = "none";
                    ds = new DataSet();
                    ds = obj.insertAccountMaster(ref strRev);
                    retValue = strRev;
                }
                if (retValue == "-1")
                {
                    clsButtonNavigation.enableDisable("S");
                    this.enableDisableNavigateButtons();
                    this.makeEmptyForm("S");
                    hdnf.Value = txtDoc_no.Text;
                    string qry = getDisplayQuery();
                    this.fetchRecord(qry);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Successfully Added!');", true);
                }
                if (retValue == "-2" || retValue == "-3")
                {
                    clsButtonNavigation.enableDisable("S");
                    this.enableDisableNavigateButtons();
                    this.makeEmptyForm("S");
                    hdnf.Value = txtDoc_no.Text;
                    string qry = getDisplayQuery();
                    this.fetchRecord(qry);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Successfully Updated !');", true);
                }
            }
        }
        #endregion
    }
    #endregion

    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtdoc_no")
            {
                #region code
                try
                {
                    int n;
                    bool isNumeric = int.TryParse(txtDoc_no.Text, out n);

                    if (isNumeric == true)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        string txtValue = "";
                        if (txtDoc_no.Text != string.Empty)
                        {
                            txtValue = txtDoc_no.Text;

                            string qry = "select * from " + tblHead + " where  Doc_No=" + txtValue +
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
                                        hdnf.Value = dt.Rows[0]["Doc_No"].ToString();

                                        if (ViewState["mode"] != null)
                                        {
                                            if (ViewState["mode"].ToString() == "I")
                                            {
                                                lblMsg.Text = "** unit_code (" + txtValue + ") Already Exist";
                                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                                this.getMaxCode();
                                                //txtdoc_no.Enabled = false;
                                                hdnf.Value = txtDoc_no.Text;
                                                btnSave.Enabled = true;   //IMP                                       
                                                setFocusControl(txtac_code);
                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = getDisplayQuery();
                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    txtDoc_no.Enabled = false;
                                                    setFocusControl(txtac_code);
                                                    hdnf.Value = txtDoc_no.Text;
                                                }
                                            }
                                        }
                                    }
                                    else   //Record Not Found
                                    {
                                        if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                        {
                                            lblMsg.Text = "";
                                            setFocusControl(txtac_code);
                                            txtDoc_no.Enabled = false;
                                            btnSave.Enabled = true;   //IMP
                                        }
                                        if (ViewState["mode"].ToString() == "U")
                                        {
                                            this.makeEmptyForm("E");
                                            lblMsg.Text = "** Record Not Found";
                                            lblMsg.ForeColor = System.Drawing.Color.Red;
                                            txtDoc_no.Text = string.Empty;
                                            setFocusControl(txtDoc_no);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = string.Empty;
                            setFocusControl(txtDoc_no);
                        }
                    }
                    else
                    {
                        this.makeEmptyForm("A");
                        lblMsg.Text = "Doc No is numeric";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        clsButtonNavigation.enableDisable("E");
                        txtDoc_no.Text = string.Empty;
                        setFocusControl(txtDoc_no);
                    }
                }
                catch
                {

                }
                #endregion
            }
            if (strTextBox == "txtdoc_date")
            {
                if (txtdoc_date.Text != string.Empty)
                {
                    try
                    {
                        string dt = DateTime.Parse(txtdoc_date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                        if (clsCommon.isValidDate(dt) == true)
                        {
                            setFocusControl(txtac_code);
                        }
                        else
                        {
                            txtdoc_date.Text = string.Empty;
                            setFocusControl(txtdoc_date);
                        }
                    }
                    catch
                    {
                        txtdoc_date.Text = string.Empty;
                        setFocusControl(txtdoc_date);
                    }
                }
                else
                {
                    setFocusControl(txtdoc_date);
                }
            }
            if (strTextBox == "txtac_code")
            {
                string acname = "";
                if (txtac_code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtac_code.Text);
                    if (a == false)
                    {
                        btntxtac_code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtac_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {

                            txtac_name.Text = acname;
                            txtaddress.Text = clsCommon.getString("select Address_E from " + AccountMasterTable + " where Ac_Code=" + txtac_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            string citycode = clsCommon.getString("select City_Code from " + AccountMasterTable + " where Ac_Code=" + txtac_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            txtCity.Text = clsCommon.getString("select city_name_e from " + cityMasterTable + " where city_code=" + citycode + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            txtPinCode.Text = clsCommon.getString("select Pincode from " + AccountMasterTable + " where Ac_Code=" + txtac_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                            setFocusControl(txtac_name);
                        }
                        else
                        {
                            txtac_code.Text = string.Empty;
                            txtac_name.Text = acname;
                            setFocusControl(txtac_code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtac_code);
                }
            }
            if (strTextBox == "txtac_name")
            {
                setFocusControl(txtaddress);
            }
            if (strTextBox == "txtaddress")
            {
                setFocusControl(txtkind_att);
            }
            if (strTextBox == "txtkind_att")
            {
                setFocusControl(txtsubject);
            }
            if (strTextBox == "txtsubject")
            {
                setFocusControl(txtref_no);
            }
            if (strTextBox == "txtref_no")
            {
                setFocusControl(txtref_dt);
            }
            if (strTextBox == "txtref_dt")
            {
                setFocusControl(txtmatter);
            }
            if (strTextBox == "txtmatter")
            {
                setFocusControl(txtauthorised_person);
            }
            if (strTextBox == "txtauthorised_person")
            {
                setFocusControl(txtdesignation);
            }
            if (strTextBox == "txtdesignation")
            {
                setFocusControl(btnSave);
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ki", "javascript:pl('" + txtDoc_no.Text + "')", true);
    }
    protected void btnPrePrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ki", "javascript:prepl('" + txtDoc_no.Text + "')", true);
    }
    protected void btnSaveNew_Click(object sender, EventArgs e)
    {
        try
        {
            #region [Validation Part]
            bool isValidated = true;
            //string newDocNo = string.Empty;
            Int32 newDocNo =0;
            string DocNoNew = string.Empty;
            if (txtDoc_no.Text != string.Empty)
            {
                if (ViewState["mode"] != null)
                {
                    if (ViewState["mode"].ToString() == "U")
                    {
                       // newDocNo = NewDocNo(newDocNo);
                        newDocNo = Convert.ToInt32(clsCommon.getString("SELECT max(Doc_no) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code='" + Session["year"] + "'")) + 1;
                        DocNoNew = newDocNo.ToString();
                        string str = clsCommon.getString("select Doc_No from " + tblHead + " where Doc_No=" + newDocNo + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                        if (str != string.Empty && str != "0")
                        {
                            //lblMsg.Text = "Code " + newDocNo + " already exist";
                            //newDocNo = NewDocNo(newDocNo);
                            //hdnf.Value = newDocNo;
                            //isValidated = true;

                            lblMsg.Text = "Code " + newDocNo + " already exist";
                            newDocNo = Convert.ToInt32(clsCommon.getString("SELECT max(Doc_no) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code='" + Session["year"] + "'")) + 1;
                            DocNoNew=newDocNo.ToString();
                            hdnf.Value = DocNoNew;
                            isValidated = true;


                        }
                        else
                        {
                            isValidated = true;
                        }
                    }
                }
            }
            else
            {
                isValidated = false;
                setFocusControl(txtDoc_no);
            }
            if (txtdoc_date.Text != string.Empty)
            {
                string dt = DateTime.Parse(txtdoc_date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                if (clsCommon.isValidDate(dt) == true)
                {
                    //setFocusControl(txtac_code);
                    isValidated = true;
                }
                else
                {
                    txtdoc_date.Text = string.Empty;
                    setFocusControl(txtdoc_date);
                    isValidated = false;
                    return;
                }
            }
            else
            {
                isValidated = false;
                setFocusControl(txtdoc_date);
                return;
            }
            if (txtac_code.Text != string.Empty)
            {
                string str = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code=" + txtac_code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (str != string.Empty)
                {
                    isValidated = true;
                }
                else
                {
                    isValidated = false;
                    setFocusControl(txtac_code);
                    return;
                }
            }
            else
            {
                isValidated = false;
                setFocusControl(txtac_code);
                return;
            }
            if (txtac_name.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtac_name);
                return;
            }
            if (txtaddress.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtaddress);
                return;
            }

            if (txtsubject.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtsubject);
                return;
            }
            if (txtref_no.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtref_no);
                return;
            }
            if (txtref_dt.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtref_dt);
                return;
            }
            if (txtmatter.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtmatter);
                return;
            }

            #endregion

            #region -Head part declearation
            Int32 DOC_NO = DocNoNew != string.Empty ? Convert.ToInt32(DocNoNew) : 0;
            string DOC_DATE = DateTime.Parse(System.DateTime.Now.ToString("dd/MM/yyyy"), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            Int32 AC_CODE = txtac_code.Text != string.Empty ? Convert.ToInt32(txtac_code.Text) : 0;
            string AC_NAME = txtac_name.Text;
            string ADDRESS = txtaddress.Text;
            string CITY = txtCity.Text;
            string PINCODE = txtPinCode.Text;
            string KIND_ATT = txtkind_att.Text;
            string SUBJECT = txtsubject.Text;
            string REF_NO = txtref_no.Text;
            string REF_DT = DateTime.Parse(System.DateTime.Now.ToString("dd/MM/yyyy"), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            string s = txtmatter.Text;
            string MATTER = s.Replace("\r\n", "<br/>");
            string AUTHORISED_PERSON = txtauthorised_person.Text;
            string DESIGNATION = txtdesignation.Text;
            string retValue = string.Empty;
            string strRev = string.Empty;
            int Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            int Year_Code = Convert.ToInt32(Session["year"].ToString());
            int year_Code = Convert.ToInt32(Session["year"].ToString());
            int Branch_Code = Convert.ToInt32(Session["Branch_Code"].ToString());
            string userinfo = clsGV.userInfo + DateTime.Now.ToString("dd/MM/yyyy:HHmmss");
            #endregion-End of Head part declearation

            using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
            {
                if (ViewState["mode"] != null)
                {
                    DataSet ds = new DataSet();
                    if (ViewState["mode"].ToString() == "U")
                    {
                        obj.flag = 1;
                        obj.tableName = tblHead;
                        obj.columnNm = "DOC_NO,DOC_DATE,AC_CODE,AC_NAME,ADDRESS,CITY,PINCODE,KIND_ATT,SUBJECT,REF_NO,REF_DT,MATTER,AUTHORISED_PERSON,DESIGNATION,Company_Code,Year_Code,Branch_Code,Created_By";
                        obj.values = "'" + DOC_NO + "','" + DOC_DATE + "','" + AC_CODE + "','" + AC_NAME + "','" + ADDRESS + "','" + CITY + "','" + PINCODE + "','" + KIND_ATT + "','" + SUBJECT + "','" + REF_NO + "','" + REF_DT + "','" + MATTER + "','" + AUTHORISED_PERSON + "','" + DESIGNATION + "','" + Convert.ToInt32(Session["Company_Code"].ToString()) + "','" + Convert.ToInt32(Session["year"].ToString()) + "','" + Convert.ToInt32(Session["Branch_Code"].ToString()) + "','" + user + "'";
                        ds = obj.insertAccountMaster(ref strRev);
                        retValue = strRev;
                    }
                    if (retValue == "-1")
                    {
                        clsButtonNavigation.enableDisable("S");
                        this.enableDisableNavigateButtons();
                        this.makeEmptyForm("S");
                        hdnf.Value = DocNoNew;
                        string qry = getDisplayQuery();
                        this.fetchRecord(qry);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Successfully Saved !');", true);

                        //clsButtonNavigation.enableDisable("S");
                        //this.enableDisableNavigateButtons();
                        //this.makeEmptyForm("S");
                        //hdnf.Value = txtDoc_no.Text;
                        //string qry = getDisplayQuery();
                        //this.fetchRecord(qry);
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Successfully Added!');", true);
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string NewDocNo(string newDocNo)
    {
        try
        {
            DataSet ds = null;
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
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
                                if (ViewState["mode"].ToString() == "U")
                                {
                                    newDocNo = ds.Tables[0].Rows[0][0].ToString();
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
        return newDocNo;
    }
    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);

            if (a == false)
            {
                string text = txtEditDoc_No.Text;
                txtSearchText.Text = text;
                btntxtdoc_no_Click(this, new EventArgs());
            }
            else
            {
                hdnf.Value = txtEditDoc_No.Text;
                string qryDisplay = "select *,CONVERT(VARCHAR(10),DOC_DATE,103) as DOC_DATE1,CONVERT(VARCHAR(10),REF_DT,103) as REF_DATE1 from " + tblHead + " where DOC_NO=" + hdnf.Value + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
                this.fetchRecord(qryDisplay);
                setFocusControl(txtEditDoc_No);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
