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
public partial class Sugar_Master_pgeGSTState_Master : System.Web.UI.Page
{
    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string qryCommon = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string AccountMasterTable = string.Empty;
    static WebControl objAsp = null;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    string Action = string.Empty;
    string AccountNo = string.Empty;
    string cs = string.Empty;



    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    #endregion
    #region text and label data Declaration
    Int32 State_Code = 0;
    string state_name = string.Empty;
    string group_type = string.Empty;
    string group_Summary = string.Empty;
    Int32 group_order = 0;
    Int32 Company_Code = 0;
    string userinfo = string.Empty;
    string retValue = string.Empty;
    string strRev = string.Empty;
    int count = 0;
    string Bsid = string.Empty;
    Int32 bsid = 0;
    Int32 Doc_No = 0;
    int counts = 0;

    string Created_By = string.Empty;
    string Modified_By = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        tblHead = "gststatemaster";
        AccountMasterTable = tblPrefix + "AccountMaster";
        user = Session["user"].ToString();
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
                    hdnf.Value = Request.QueryString["State_Code"];
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
                    setFocusControl(txtStateName);
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
                        //string comcode = Session["Company_Code"].ToString();
                        //if (hdnfcompanycode.Value != comcode)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same !')", true);
                        //    return false;
                        //}

                       
                     //   hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();
                        txtStateCode.Text = dt.Rows[0]["State_Code"].ToString();
                        txtStateName.Text = dt.Rows[0]["State_Name"].ToString();
                        //hdnf.Value = txtStateCode.Text;
                        lblbsid.Text = dt.Rows[0]["State_Code"].ToString();
                        Label lblCreated = (Label)Master.FindControl("MasterlblCreatedBy");
                        Label lblModified = (Label)Master.FindControl("MasterlblModifiedBy");
                        //if (lblCreated != null)
                        //{
                        //    lblCreated.Text = "Created By: " + dt.Rows[0]["Created_By"].ToString();
                        //}
                        //if (lblModified != null)
                        //{
                        //    lblModified.Text = "Modified By: " + dt.Rows[0]["Modified_By"].ToString();
                        //}
                        recordExist = true;
                        lblMsg.Text = "";
                    }
                }
            }
            hdnf.Value = txtStateCode.Text;

            return recordExist;
        }
        catch
        {
            return false;
        }
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

    private string getDisplayQuery()
    {
        try
        {
            //string qryDisplay = "select * from " + tblHead +
            //    " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and State_Code=" + hdnf.Value;

            string qryDisplay = "select * from " + tblHead +
                " where State_Code =" + hdnf.Value;
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }

    private void makeEmptyForm(string dAction)
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
            btntxtStateCode.Text = "Choose No";
            btntxtStateCode.Enabled = false;
            lblMsg.Text = string.Empty;
            txtEditDoc_No.Enabled = true;


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
            btntxtStateCode.Text = "Change No";
            btntxtStateCode.Enabled = true;
            txtEditDoc_No.Enabled = false;

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
            btntxtStateCode.Text = "Choose No";
            btntxtStateCode.Enabled = false;

            txtEditDoc_No.Enabled = true;
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
            btntxtStateCode.Text = "Choose No";
            btntxtStateCode.Enabled = true;
            lblMsg.Text = string.Empty;

            txtEditDoc_No.Enabled = false;
        }
    }

    protected void txtStateCode_TextChanged(object sender, EventArgs e)
    {
        #region code
        try
        {
            int n;
            bool isNumeric = int.TryParse(txtStateCode.Text, out n);

            if (isNumeric == true)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string txtValue = "";
                if (txtStateCode.Text != string.Empty)
                {
                    txtValue = txtStateCode.Text;

                    string qry = "select * from " + tblHead + " where  State_Code='" + txtValue + "' ";

                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                //Record Found
                                hdnf.Value = dt.Rows[0]["State_Code"].ToString();

                                if (ViewState["mode"] != null)
                                {
                                    if (ViewState["mode"].ToString() == "I")
                                    {
                                        lblMsg.Text = "** Doc No (" + txtValue + ") Already Exist";
                                        lblMsg.ForeColor = System.Drawing.Color.Red;
                                        this.getMaxCode();
                                        //txtDoc_no.Enabled = false;

                                        btnSave.Enabled = true;   //IMP                                       
                                        setFocusControl(txtStateName);
                                    }

                                    if (ViewState["mode"].ToString() == "U")
                                    {
                                        //fetch record
                                        qry = getDisplayQuery();
                                        bool recordExist = this.fetchRecord(qry);
                                        if (recordExist == true)
                                        {
                                            txtStateCode.Enabled = false;
                                            setFocusControl(txtStateName);

                                            hdnf.Value = txtStateCode.Text;
                                        }
                                    }
                                }
                            }
                            else   //Record Not Found
                            {
                                if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                {
                                    lblMsg.Text = "";
                                    setFocusControl(txtStateName);
                                    txtStateCode.Enabled = false;
                                    btnSave.Enabled = true;   //IMP
                                }
                                if (ViewState["mode"].ToString() == "U")
                                {
                                    this.makeEmptyForm("E");
                                    lblMsg.Text = "** Record Not Found";
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    txtStateCode.Text = string.Empty;
                                    setFocusControl(txtStateCode);
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = string.Empty;
                    setFocusControl(txtStateCode);
                }
            }
            else
            {
                this.makeEmptyForm("A");
                lblMsg.Text = "Doc No is numeric";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                clsButtonNavigation.enableDisable("E");
                txtStateCode.Text = string.Empty;
                setFocusControl(txtStateCode);
            }
        }
        catch
        {

        }
        #endregion
    }

    protected void btntxtStateCode_Click(object sender, EventArgs e)
    {
        try
        {
            if (btntxtStateCode.Text == "Change No")
            {
                pnlPopup.Style["display"] = "none";
                txtStateCode.Text = string.Empty;
                txtStateCode.Enabled = true;

                btnSave.Enabled = false;
                setFocusControl(txtStateCode);
            }

            if (btntxtStateCode.Text == "Choose No")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtStateCode";
                btnSearch_Click(sender, e);
            }
        }
        catch
        {
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (searchString != string.Empty && strTextBox == hdnfClosePopup.Value)
        {
            txtSearchText.Text = searchString;
        }
        else
        {
            txtSearchText.Text = searchString;
        }
        if (hdnfClosePopup.Value == "txtStateCode" || hdnfClosePopup.Value == "txtEditDoc_No")
        {
            if (btntxtStateCode.Text == "Change No")
            {
                pnlPopup.Style["display"] = "none";
                txtStateCode.Text = string.Empty;
                txtStateCode.Enabled = true;

                btnSave.Enabled = false;
                setFocusControl(txtStateCode);
                hdnfClosePopup.Value = "Close";
            }

            if (btntxtStateCode.Text == "Choose No")
            {
                lblPopupHead.Text = "--Select Group--";
                string qry = "SELECT  State_Code,State_Name from " + tblHead + " where  (State_Code like '%" + txtSearchText.Text + "%' or State_Name like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }
        }
    }

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

    //protected void drpgroupSection_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    setFocusControl(txtGroupOrder);
    //}
    //protected void drpGroupSummary_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    setFocusControl(drpgroupSection);
    //}
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string docno = string.Empty;
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        this.NextNumber();
        setFocusControl(txtStateName);
    }

    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                //obj.tableName = tblHead + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                obj.tableName = tblHead;
                obj.code = "State_Code";

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
                                    txtStateCode.Text = ds.Tables[0].Rows[0][0].ToString();
                                    txtStateCode.Enabled = false;
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

    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            #region validation
            if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
                return;

            }
            //bool isValidated = true;
            //if (txtStateCode.Text != string.Empty)
            //{
            //    if (ViewState["mode"] != null)
            //    {
            //        if (ViewState["mode"].ToString() == "I")
            //        {
            //            string str = clsCommon.getString("select State_Name from " + tblHead + " where State_Code=" + txtStateCode.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //            if (str != string.Empty)
            //            {
            //                lblMsg.Text = "Code " + txtStateCode.Text + " already exist";
            //                this.getMaxCode();
            //                isValidated = true;
            //            }
            //            else
            //            {
            //                isValidated = true;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    isValidated = false;
            //    setFocusControl(txtStateCode);
            //}

            //if (txtStateName.Text != string.Empty)
            //{
            //    isValidated = true;
            //}
            //else
            //{
            //    isValidated = false;
            //    setFocusControl(txtStateName);
            //}
            #endregion

            #region declaration
            State_Code = txtStateCode.Text != string.Empty ? Convert.ToInt32(txtStateCode.Text) : 0;
            // bsid = lblbsid.Text != string.Empty ? Convert.ToInt32(lblbsid.Text) : 0;
            state_name = txtStateName.Text;

            Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            userinfo = clsGV.userInfo + DateTime.Now.ToString("dd/MM/yyyy:HHmmss");
            retValue = string.Empty;
            strRev = string.Empty;

            Created_By = Session["user"].ToString();
            Modified_By = Session["user"].ToString();

            string Head_Fields = string.Empty;
            string Head_Values = string.Empty;
            string Head_Update = string.Empty;
            int flag = 0;
            int no = 0;
            string op = string.Empty;
            string returnmaxno = string.Empty;
            //Created_By = Session["user"].ToString();
            //lblCreatedBy.Text = Created_By.ToString();
            #endregion

            #region save
            if (btnSave.Text == "Save")
            {
                //// String UUID = Guid.NewGuid().ToString();
                //Random r = new Random();
                //no = r.Next();

                this.NextNumber();
                #region Create Head Insert  Query
                Head_Fields = "State_Code,";
                Head_Values = "'" + Doc_No + "',";
                Head_Fields = Head_Fields + "State_Name";
                Head_Values = Head_Values + "'" + state_name + "'";
                //Head_Fields = Head_Fields + "Company_Code,";
                //Head_Values = Head_Values + "'" + Company_Code + "' ,";
                //Head_Fields = Head_Fields + "Created_By";
                //Head_Values = Head_Values + "'" + Created_By + "'";
                #endregion

                flag = 1;
                qry = "insert into " + tblHead + "(" + Head_Fields + ") values(" + Head_Values + ")";


                Thread thred = new Thread(() => { count = DataStore(qry, flag); }); //Calling DataStore Method Using Thread
                thred.Start(); //Thread Operation Start
                thred.Join();

                if (count == 1)
                {
                    hdnf.Value = Doc_No.ToString();
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
                Head_Update = Head_Update + "State_Name=";
                Head_Update = Head_Update + "'" + state_name + "'";
                //Head_Update = Head_Update + "Modified_By=";
                //Head_Update = Head_Update + "'" + Modified_By + "'";
                #endregion

                flag = 2;
                //qry = "update " + tblHead + " set " + Head_Update + " where Company_Code='" + Company_Code + "' and State_Code='" + txtStateCode.Text + "'";
                qry = "update " + tblHead + " set " + Head_Update + " where  State_Code='" + txtStateCode.Text + "'";



                Thread thred = new Thread(() => { count = DataStore(qry, flag); });
                thred.Start();
                thred.Join();
                if (count == 2)
                {
                    hdnf.Value = lblbsid.Text;
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

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('check Entry AND Try Again !')", true);


        }

    }





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
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        txtStateCode.Enabled = false;
        this.makeEmptyForm("E");
        setFocusControl(txtStateName);

    }




    #region navigateRecord
    private void navigateRecord()
    {
        try
        {
            if (hdnf.Value != string.Empty)
            {
                ViewState["mode"] = "U";
                txtStateCode.Text = hdnf.Value;

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



    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                //string str = clsCommon.getString("select Ac_Code from " + AccountMasterTable + " where State_Code=" + txtStateCode.Text + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
                string str = clsCommon.getString("select Ac_Code from " + AccountMasterTable + " where GSTStateCode=" + txtStateCode.Text);
                if (str == "0")   //Gledger does not contain this account then delete
                {
                    string currentDoc_No = txtStateCode.Text;
                    int flag = 3;
                    string strrev = "";

                    qry = "delete from " + tblHead + " where State_Code='" + currentDoc_No + "' ";
                    Thread thred = new Thread(() => { count = DataStore(qry, flag); }); //Calling DataStore Method Using Thread
                    thred.Start(); //Thread Operation Start
                    thred.Join();

                    if (count == 3)
                    {
                        Response.Redirect("../Master/PgeGSTStateMasterUtility.aspx");
                    }


                }
                else
                {
                    lblMsg.Text = "Cannot delete this State code , it is in use";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                hdnf.Value = txtStateCode.Text;
                showLastRecord();
            }
        }
        catch
        {
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Action = "1";
        if (Action == "2")
        {
            btnAdd_Click(this, new EventArgs());

        }
        else
        {
            string max = clsCommon.getString("select isnull(max(State_Code),0) as id from " + tblHead);
            //hdnfTran_type.Value = drpTrnType.SelectedValue;
            hdnf.Value = max;
           // hdnf.Value = Request.QueryString["State_Code"];
            clsButtonNavigation.enableDisable("N");
            pnlPopup.Style["display"] = "none";
            this.makeEmptyForm("N");
            ViewState["mode"] = "I";
            this.showLastRecord();
            setFocusControl(btnEdit);
        }

    }
    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        {
            try
            {
                bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);
                if (a == false)
                {
                    searchString = txtEditDoc_No.Text;
                    pnlPopup.Style["display"] = "block";
                    hdnfClosePopup.Value = "txtEditDoc_No";
                    btnSearch_Click(this, new EventArgs());
                }
                else
                {
      //              string docno = clsCommon.getString("select systemid from " + tblHead + " where Doc_no =" + txtEditDoc_No.Text + " and Company_Code="
      //+ Session["Company_Code"].ToString());
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
    protected void txtStateName_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(btnSave);
    }

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);

    }
    protected void txtGroupOrder_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(btnSave);
    }
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }
    }

    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select count(State_Code) as A from " + tblHead));
            if (counts == 0)
            {
                txtStateCode.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(State_Code) as Doc_No from " + tblHead)) + 1;
                txtStateCode.Text = Doc_No.ToString();
            }

            //counts = Convert.ToInt32(clsCommon.getString("SELECT count(bsid) as bsid from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'"));
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
}