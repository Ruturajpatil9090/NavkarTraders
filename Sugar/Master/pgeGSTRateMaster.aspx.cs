using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;


public partial class Sugar_pgeGSTRateMaster : System.Web.UI.Page
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
    int defaultAccountCode = 0;
    string GLedgerTable = string.Empty;
    string qry = string.Empty;
    string Action = string.Empty;
    string isAuthenticate = string.Empty;
    static WebControl objAsp = null;
    string user = string.Empty;

    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    #endregion

    #region text and label data Declaration
    Int32 doc_No = 0;
    string gst_name = string.Empty;
    double rate = 0.00;
    double igst = 0.0;
    double sgst = 0.0;
    double cgst = 0.0;
    int docid = 0;
    string remark = string.Empty;
    int gstcode = 0;
    Int32 Company_Code = 0;
    string userinfo = string.Empty;
    string retValue = string.Empty;
    string strRev = string.Empty;
    string cs = string.Empty;

    int count = 0;
    Int32 maxDoc_No = 0;
    int counts = 0;

    string Created_By = string.Empty;
    string Modified_By = string.Empty;
    int year = 0;
    int branch = 0;
    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "GSTRateMaster";
            tblDetails = tblPrefix + "";
            AccountMasterTable = tblPrefix + "AccountMaster";
            // cityMasterTable = tblPrefix + "CityMaster";
            qryCommon = tblPrefix + "qryGSTRateMaster";
            GLedgerTable = tblPrefix + "GLEDGER";
            pnlPopup.Style["display"] = "none";
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
                        setFocusControl(txtGST_Name);
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
    #endregion

    #region [getMaxCode]
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;

            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
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
                                    txtdoc_no.Text = ds.Tables[0].Rows[0][0].ToString();
                                    txtdoc_no.Enabled = false;
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

    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            //where Company_Code='" + Session["Company_Code"].ToString() + "'
            counts = Convert.ToInt32(clsCommon.getString("select count(Doc_no) as A from " + tblHead + " "));
            if (counts == 0)
            {
                txtdoc_no.Text = "1";
                maxDoc_No = 1;
            }
            else
            {
                maxDoc_No = Convert.ToInt32(clsCommon.getString("SELECT max(Doc_no) as Doc_No from " + tblHead + " ")) + 1;
                txtdoc_no.Text = maxDoc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(Doc_no) as Doc_No from " + tblHead + " "));
            //if (counts == 0)
            //{
            //    lblbsid.Text = "1";
            //    bsid = 1;
            //}
            //else
            //{
            //    bsid = Convert.ToInt32(clsCommon.getString("SELECT max(bsid) as bsid from " + tblHead)) + 1;
            //    lblbsid.Text = bsid.ToString();
            //}
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
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
                btntxtDOC_NO.Text = "Choose No";
                txtEditDoc_No.Enabled = true;
                btntxtDOC_NO.Enabled = false;
                lblMsg.Text = string.Empty;

                //lblCode_Name.Text=string.Empty;
                //lblCode_Name.Text = "";
                //btntxtGST_Code.Enabled = false;

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
                btntxtDOC_NO.Text = "Change No";
                btntxtDOC_NO.Enabled = false;

                txtdoc_no.Enabled = false;
                txtEditDoc_No.Enabled = false;
                //lblMsg.Text = "";

                #region set Business logic for save
                //lblCode_Name.Text = string.Empty;
                //txtGST_Code.Enabled = false;
                //btntxtGST_Code.Enabled = false;
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
                btntxtDOC_NO.Text = "Choose No";
                btntxtDOC_NO.Enabled = false;

                txtEditDoc_No.Enabled = true;
                #region set Business logic for save

                //btntxtGST_Code.Enabled = false;
                //txtGST_Code.Enabled = false;

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

                btntxtDOC_NO.Text = "Choose No";
                btntxtDOC_NO.Enabled = true;
                lblMsg.Text = string.Empty;
                txtEditDoc_No.Enabled = false;


                #region logic
                //txtGST_Code.Enabled = false;
                //btntxtGST_Code.Enabled = false;

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



    #region navigateRecord
    private void navigateRecord()
    {
        try
        {
            if (hdnf.Value != string.Empty)
            {
                ViewState["mode"] = "U";
                txtdoc_no.Text = hdnf.Value;
                hdnfSuffix.Value = "";
                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Focus();
                }

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

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        //this.getMaxCode();
        this.NextNumber();
        setFocusControl(txtGST_Name);

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

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        this.makeEmptyForm("E");
        txtdoc_no.Enabled = false;
        setFocusControl(txtGST_Name);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                //string str = clsCommon.getString("select Ac_Code from " + AccountMasterTable + " where Doc_no=" + txtdoc_no.Text + "");
                string str = clsCommon.getString("select Ac_Code from " + AccountMasterTable + " where Doc_no=" + txtdoc_no.Text + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");

                if (str == string.Empty)   //Gledger does not contain this account then delete
                {
                    string currentDoc_No = txtdoc_no.Text;
                    int flag = 3;
                    string strrev = "";

                    qry = "delete from " + tblHead + " where Doc_no='" + currentDoc_No + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                    Thread thred = new Thread(() => { count = DataStore(qry, flag); }); //Calling DataStore Method Using Thread
                    thred.Start(); //Thread Operation Start
                    thred.Join();

                    if (count == 3)
                    {
                        Response.Redirect("../Master/PgeGSTRateMasterUtility.aspx");
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
                hdnf.Value = txtdoc_no.Text;
                showLastRecord();
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

        Action = Request.QueryString["Action"];
        if (Action == "2")
        {
            btnAdd_Click(this, new EventArgs());
        }
        else
        {
            hdnf.Value = Request.QueryString["Doc_no"];
            clsButtonNavigation.enableDisable("N");
            pnlPopup.Style["display"] = "none";
            this.makeEmptyForm("N");
            ViewState["mode"] = "I";
            this.showLastRecord();
            setFocusControl(btnEdit);
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
                        string comcode = Session["Company_Code"].ToString();
                        if (hdnfcompanycode.Value != comcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same !')", true);
                            return false;
                        }

                       
                        hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();
                        txtdoc_no.Text = dt.Rows[0]["Doc_No"].ToString();
                        txtGST_Name.Text = dt.Rows[0]["GST_Name"].ToString();
                        txtRate.Text = dt.Rows[0]["Rate"].ToString();
                        txtIGST.Text = dt.Rows[0]["IGST"].ToString();
                        txtSGST.Text = dt.Rows[0]["SGST"].ToString();
                        txtCGST.Text = dt.Rows[0]["CGST"].ToString();
                        //txtGST_Code.Text = dt.Rows[0]["Doc_no"].ToString();
                        //lblCode_Name.Text = dt.Rows[0]["GST_Name"].ToString();
                        txtRemark.Text = dt.Rows[0]["Remark"].ToString();



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
            hdnf.Value = txtdoc_no.Text;
            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string qry = "";
            #region validation
            bool isValidated = true;
            if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
                return;

            }
            if (txtdoc_no.Text != string.Empty)
            {
                if (ViewState["mode"].ToString() == "I")
                {

                    string str = clsCommon.getString("select Doc_No from " + tblHead + " where  Doc_No='" + txtdoc_no.Text + "'");


                    if (str != "0")
                    {
                        lblMsg.Text = "Doc No " + txtdoc_no.Text + " already exist";
                        getMaxCode();
                        isValidated = true;
                        return;
                    }
                    else
                    {
                        isValidated = true;
                    }
                }
                else
                {
                    isValidated = true;
                }
            }
            else
            {
                isValidated = false;
                setFocusControl(txtdoc_no);
                hdnf.Value = txtdoc_no.Text;
                return;
            }

            if (txtGST_Name.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtGST_Name);
                return;
            }
            #endregion

            #region -Head part declearation

            doc_No = txtdoc_no.Text != string.Empty ? Convert.ToInt32(txtdoc_no.Text) : 0;
            gst_name = txtGST_Name.Text.ToUpper();
            rate = txtRate.Text != string.Empty ? Convert.ToDouble(txtRate.Text) : 0.00;
            igst = txtIGST.Text != string.Empty ? Convert.ToDouble(txtIGST.Text) : 0.00;
            sgst = txtSGST.Text != string.Empty ? Convert.ToDouble(txtSGST.Text) : 0.00;
            cgst = txtCGST.Text != string.Empty ? Convert.ToDouble(txtCGST.Text) : 0.00;
            remark = txtRemark.Text;
            // gstcode = txtGST_Code.Text != string.Empty ? Convert.ToInt32(txtGST_Code.Text) : 0;
            Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            userinfo = clsGV.userInfo + DateTime.Now.ToString("dd/MM/yyyy:HHmmss");
            retValue = string.Empty;
            strRev = string.Empty;

            Created_By = Session["user"].ToString();
            Modified_By = Session["user"].ToString();
            year = Convert.ToInt32(Session["year"].ToString());
            branch = Convert.ToInt32(Session["Branch_Code"].ToString());

            string Head_Fields = string.Empty;
            string Head_Values = string.Empty;
            string Head_Update = string.Empty;
            int flag = 0;
            int no = 0;
            string op = string.Empty;
            string returnmaxno = string.Empty;
            #endregion-End of Head part declearation



            #region save
            if (btnSave.Text == "Save")
            {
                // String UUID = Guid.NewGuid().ToString();
                //Random r = new Random();
                //no = r.Next();

                this.NextNumber();
                #region Create Head Insert  Query
                Head_Fields = "doc_no,";
                Head_Values = "'" + doc_No + "',";
                Head_Fields = Head_Fields + "GST_Name,";
                Head_Values = Head_Values + "'" + gst_name + "',";
                Head_Fields = Head_Fields + "Rate,";
                Head_Values = Head_Values + "'" + rate + "',";
                Head_Fields = Head_Fields + "IGST,";
                Head_Values = Head_Values + "'" + igst + "',";
                Head_Fields = Head_Fields + "SGST,";
                Head_Values = Head_Values + "'" + sgst + "',";
                Head_Fields = Head_Fields + "CGST,";
                Head_Values = Head_Values + "'" + cgst + "',";
                Head_Fields = Head_Fields + "Remark,";
                Head_Values = Head_Values + "'" + remark + "' ,";

                Head_Fields = Head_Fields + "Company_Code,";
                Head_Values = Head_Values + "'" + Company_Code + "' ,";
                Head_Fields = Head_Fields + "Year_Code,";
                Head_Values = Head_Values + "'" + year + "' ,";
                Head_Fields = Head_Fields + "Branch_Code,";
                Head_Values = Head_Values + "'" + branch + "' ,";
                Head_Fields = Head_Fields + "Created_By";
                Head_Values = Head_Values + "'" + Created_By + "'";
                #endregion

                flag = 1;
                qry = "insert into " + tblHead + "(" + Head_Fields + ") values(" + Head_Values + ")";


                Thread thred = new Thread(() => { count = DataStore(qry, flag); }); //Calling DataStore Method Using Thread
                thred.Start(); //Thread Operation Start
                thred.Join();

                if (count == 1)
                {
                    hdnf.Value = maxDoc_No.ToString();
                    clsButtonNavigation.enableDisable("S");
                    this.makeEmptyForm("S");
                    qry = getDisplayQuery();
                    this.fetchRecord(qry);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Added !')", true);
                }

            }
            else
            {
                #region Create Update Query

                Head_Update = Head_Update + "GST_Name=";
                Head_Update = Head_Update + "'" + gst_name + "',";
                Head_Update = Head_Update + "Rate=";
                Head_Update = Head_Update + "'" + rate + "',";
                Head_Update = Head_Update + "IGST=";
                Head_Update = Head_Update + "'" + igst + "',";
                Head_Update = Head_Update + "SGST=";
                Head_Update = Head_Update + "'" + sgst + "',";
                Head_Update = Head_Update + "CGST=";
                Head_Update = Head_Update + "'" + cgst + "',";
                Head_Update = Head_Update + "Remark=";
                Head_Update = Head_Update + "'" + remark + "',";

                Head_Update = Head_Update + "Modified_By=";
                Head_Update = Head_Update + "'" + Modified_By + "'";
                #endregion

                flag = 2;
                qry = "update " + tblHead + " set " + Head_Update + " where Company_Code='" + Company_Code + "' and Doc_no='" + txtdoc_no.Text + "'";


                Thread thred = new Thread(() => { count = DataStore(qry, flag); });
                thred.Start();
                thred.Join();
                if (count == 2)
                {

                    hdnf.Value = txtdoc_no.Text;
                    clsButtonNavigation.enableDisable("S");
                    this.makeEmptyForm("S");
                    qry = getDisplayQuery();
                    this.fetchRecord(qry);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Update !')", true);
                }


            }
            #endregion

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
            if (strTextBox == "txtdoc_no")
            {
                #region code
                try
                {
                    int n;
                    bool isNumeric = int.TryParse(txtdoc_no.Text, out n);

                    if (isNumeric == true)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        string txtValue = "";
                        if (txtdoc_no.Text != string.Empty)
                        {
                            txtValue = txtdoc_no.Text;

                            string qry = "select * from " + tblHead + " where   Doc_No='" + txtValue + "' " +
                                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

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
                                                lblMsg.Text = "** Doc No (" + txtValue + ") Already Exist";
                                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                                this.getMaxCode();
                                                txtdoc_no.Enabled = false;
                                                btnSave.Enabled = true;   //IMP
                                                setFocusControl(txtGST_Name);
                                            }

                                            if (ViewState["mode"].ToString() == "U")
                                            {
                                                //fetch record
                                                qry = "select * from " + tblHead + " where Doc_No=" + hdnf.Value +
                                                   " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());


                                                bool recordExist = this.fetchRecord(qry);
                                                if (recordExist == true)
                                                {
                                                    txtdoc_no.Enabled = false;
                                                    setFocusControl(txtGST_Name);

                                                    //   hdnf.Value = txtdoc_no.Text;
                                                    //   hdnfSuffix.Value = txtSUFFIX.Text.Trim();
                                                }
                                            }
                                        }
                                    }
                                    else   //Record Not Found
                                    {
                                        if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                        {
                                            lblMsg.Text = "";
                                            setFocusControl(txtGST_Name);
                                            txtdoc_no.Enabled = false;
                                            btnSave.Enabled = true;   //IMP
                                        }
                                        if (ViewState["mode"].ToString() == "U")
                                        {
                                            this.makeEmptyForm("E");
                                            lblMsg.Text = "** Record Not Found";
                                            lblMsg.ForeColor = System.Drawing.Color.Red;
                                            txtdoc_no.Text = string.Empty;
                                            setFocusControl(txtdoc_no);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = string.Empty;
                            setFocusControl(txtdoc_no);
                        }
                    }
                    else
                    {
                        this.makeEmptyForm("A");
                        lblMsg.Text = "Doc No is numeric";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        clsButtonNavigation.enableDisable("E");
                        txtdoc_no.Text = string.Empty;
                        setFocusControl(txtdoc_no);
                    }
                }
                catch
                {

                }
                #endregion
            }

            //if (strTextBox == "txtGST_Code")
            //{
            //    string gstcodetext = txtGST_Code.Text.Trim();
            //    if (gstcodetext != string.Empty)
            //    {
            //        bool a = clsCommon.isStringIsNumeric(gstcodetext);
            //        if (a == false)
            //        {
            //            btntxtGST_Code_Click(this, new EventArgs());
            //        }
            //        else
            //        {
            //            //string gstStateName = clsCommon.getString("select Code_Name from " + tblPrefix + "GSTCodeMaster where Doc_No=" + txtGST_Code.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
            //            string gstName = clsCommon.getString("select GST_Name from " + tblPrefix + "gstratemaster where Doc_No=" + gstcodetext + "");

            //            if (gstName != string.Empty)
            //            {
            //                lblCode_Name.Text = gstName;
            //                setFocusControl(btnSave);

            //            }
            //            else
            //            {
            //                txtGST_Code.Text = string.Empty;
            //                lblCode_Name.Text = string.Empty;


            //            }
            //        }
            //    }
            //    else
            //    {
            //        lblCode_Name.Text = "";
            //        setFocusControl(txtGST_Code);
            //        setFocusControl(btnSave);
            //    }
            //    return;
            //}

            if (strTextBox == "txtGST_Name")
            {
                setFocusControl(txtRate);
            }
            if (strTextBox == "txtRate")
            {
                setFocusControl(txtIGST);
            }

        }
        catch
        {

        }
    }
    #endregion

    //protected void txtGST_Code_TextChanged(object sender, EventArgs e)
    //{
    //    searchString = txtGST_Code.Text;
    //    strTextBox = "txtGST_Code";
    //    csCalculations();

    //}

    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        {
            try
            {
                bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);
                if (a == false)
                {
                    searchString = txtEditDoc_No.Text;
                    strTextBox = "txtEditDoc_No";
                    pnlPopup.Style["display"] = "block";
                    hdnfClosePopup.Value = "txtEditDoc_No";
                    btnSearch_Click(this, new EventArgs());
                }
                else
                {
                   
                    hdnf.Value = txtEditDoc_No.Text;
                    string qry1 = getDisplayQuery();
                    fetchRecord(qry1);
                    setFocusControl(txtEditDoc_No);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }



    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            //string qryDisplay = "select * from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Doc_No=" + hdnf.Value;
            string qryDisplay = "select * from " + tblHead +
                " where Doc_no =" + hdnf.Value;
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion

    #region [txtdoc_no_TextChanged]
    protected void txtdoc_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtdoc_no.Text;
        strTextBox = "txtdoc_no";
        csCalculations();
    }
    #endregion

    #region [btntxtDOC_NO_Click]
    protected void btntxtDOC_NO_Click(object sender, EventArgs e)
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
        //try
        //{
        //    if (btntxtDOC_NO.Text == "Change No")
        //    {
        //        pnlPopup.Style["display"] = "none";
        //        txtdoc_no.Text = string.Empty;
        //        txtdoc_no.Enabled = true;
        //        btnSave.Enabled = false;
        //        setFocusControl(txtdoc_no);
        //    }

        //    if (btntxtDOC_NO.Text == "Choose No")
        //    {
        //        pnlPopup.Style["display"] = "block";
        //        hdnfClosePopup.Value = "txtdoc_no";
        //        btnSearch_Click(sender, e);
        //    }
        //}
        //catch
        //{
        //}
    }
    #endregion

    #region [btntxtGST_Code_Click]
    protected void btntxtGST_Code_Click(object sender, EventArgs e)
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

            if (hdnfClosePopup.Value == "txtdoc_no" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                if (btntxtDOC_NO.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtdoc_no.Text = string.Empty;
                    txtdoc_no.Enabled = true;

                    btnSave.Enabled = false;
                    setFocusControl(txtdoc_no);
                    hdnfClosePopup.Value = "Close";
                }


                //if (btntxtDOC_NO.Text == "Choose No")
                //{
                //    lblPopupHead.Text = "--Select DOC No--";
                //    string qry = "select doc_no,doc_date,PartyShortname,PartyCity from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                //        " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                //        " and (doc_no like '%" + txtSearchText.Text + "%' or doc_date like '%" + txtSearchText.Text + "%' or PartyShortname like '%" + txtSearchText.Text + "%' or PartyCity like '%" + txtSearchText.Text + "%')";
                //    this.showPopup(qry);
                //}
            }
            //if (searchString != string.Empty && strTextBox == hdnfClosePopup.Value)
            //{
            //    txtSearchText.Text = searchString;
            //}
            //else
            //{
            //    txtSearchText.Text = searchString;
            //}
            //if (hdnfClosePopup.Value == "txtdoc_no" || hdnfClosePopup.Value == "txtEditDoc_No")
            //{
            //    lblPopupHead.Text = "--Select City--";
            //    string qry = "select Doc_No,GST_Name,Rate from " + qryCommon + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (GST_Name like '%" + txtSearchText.Text + "%' or Doc_No like '%" + txtSearchText.Text + "%')";
            //    this.showPopup(qry);
            //}

            if (hdnfClosePopup.Value == "txtGST_Code")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( Doc_No like '%" + aa + "%' or GST_Name like '%" + aa + "%'  ) and";
                }
                lblPopupHead.Text = "--Select Code--";
                string qry = "select Doc_No,GST_Name from " + tblPrefix + "gstratemaster where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (GST_Name like '%" + txtSearchText.Text + "%' or Doc_No like '%" + txtSearchText.Text + "%')";
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

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);

    }

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

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string v = hdnfClosePopup.Value;
            if (e.Row.RowType != DataControlRowType.Pager)
            {

                e.Row.Cells[0].ControlStyle.Width = new Unit("40px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("200px");
            }
            if (v == "txtDoc_No")
            {
                e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(10);
                e.Row.Cells[2].ControlStyle.Width = Unit.Percentage(10);


            }


        }
        catch
        {

        }
    }
    #endregion
}