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
using System.Text;

public partial class Sugar_pgeBSgroupMaster : System.Web.UI.Page
{
    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string qryCommon = string.Empty;
    string searchString = string.Empty;
    string group_type1 = string.Empty;
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
    StringBuilder Head_Fields = null;
    StringBuilder Head_Values = null;
    StringBuilder Head_Update = null;

    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;

    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;
    #endregion
    #region text and label data Declaration
    Int32 group_Code = 0;
    string group_name = string.Empty;
    string group_name_r = string.Empty;
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
        tblHead = tblPrefix + "BSGroupMaster";
        AccountMasterTable = tblPrefix + "AccountMaster";
        user = Session["user"].ToString();
        //group_type1 = drpgroupSection.SelectedValue;
        cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        con = new SqlConnection(cs);
        Head_Fields = new StringBuilder();
        Head_Values = new StringBuilder();
        Head_Update = new StringBuilder();

        Maindt = new DataTable();
        dr = null;
        Maindt.Columns.Add("Querys", typeof(string));
        dr = Maindt.NewRow();
        if (!Page.IsPostBack)
        {
            hdnfyearcode.Value = Session["year"].ToString();
            hdnfcompanycode.Value = Session["Company_Code"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                Action = Request.QueryString["Action"];
                hdnfTran_type.Value = Request.QueryString["group_Type"];
                if (Action == "1")
                {
                    hdnf.Value = Request.QueryString["bsid"];
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
                    hdnfTran_type.Value = drpgroupSection.SelectedValue;
                    setFocusControl(txtgroupName);
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
                        string comcode = Session["Company_Code"].ToString();
                        if (hdnfcompanycode.Value != comcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same !')", true);
                            return false;
                        }

                       
                        hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();
                        txtgroupCode.Text = dt.Rows[0]["group_Code"].ToString();
                        txtgroupName.Text = dt.Rows[0]["group_Name_E"].ToString();
                        txtgroupnamer.Text = dt.Rows[0]["group_Name_R"].ToString();
                        drpGroupSummary.SelectedValue = dt.Rows[0]["group_Summary"].ToString();
                        drpgroupSection.SelectedValue = dt.Rows[0]["group_Type"].ToString();
                        txtGroupOrder.Text = dt.Rows[0]["group_Order"].ToString();
                        lblbsid.Text = dt.Rows[0]["bsid"].ToString();
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
            hdnf.Value = txtgroupCode.Text;

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
            //    " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and group_Code=" + hdnf.Value;

            string qryDisplay = "select * from " + tblHead +
                " where bsid =" + hdnf.Value;
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
            btntxtgroupCode.Text = "Choose No";
            btntxtgroupCode.Enabled = false;
            lblMsg.Text = string.Empty;
            drpgroupSection.Enabled = false;
            drpGroupSummary.Enabled = false;
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
            btntxtgroupCode.Text = "Change No";
            btntxtgroupCode.Enabled = true;
            txtEditDoc_No.Enabled = false;
            drpgroupSection.Enabled = true;
            drpGroupSummary.Enabled = true;
            drpgroupSection.Text = hdnfTran_type.Value;
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
            btntxtgroupCode.Text = "Choose No";
            btntxtgroupCode.Enabled = false;
            drpgroupSection.Enabled = false;
            drpGroupSummary.Enabled = false;
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
            btntxtgroupCode.Text = "Choose No";
            btntxtgroupCode.Enabled = true;
            lblMsg.Text = string.Empty;
            drpgroupSection.Enabled = true;
            drpGroupSummary.Enabled = true;
            txtEditDoc_No.Enabled = false;
        }
    }

    protected void txtgroupCode_TextChanged(object sender, EventArgs e)
    {
        #region code
        try
        {
            int n;
            bool isNumeric = int.TryParse(txtgroupCode.Text, out n);

            if (isNumeric == true)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string txtValue = "";
                if (txtgroupCode.Text != string.Empty)
                {
                    txtValue = txtgroupCode.Text;

                    string qry = "select * from " + tblHead + " where  group_Code='" + txtValue + "' " +
                        "  and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";

                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                //Record Found
                                hdnf.Value = dt.Rows[0]["group_Code"].ToString();

                                if (ViewState["mode"] != null)
                                {
                                    if (ViewState["mode"].ToString() == "I")
                                    {
                                        lblMsg.Text = "** Doc No (" + txtValue + ") Already Exist";
                                        lblMsg.ForeColor = System.Drawing.Color.Red;
                                        this.getMaxCode();
                                        //txtDoc_no.Enabled = false;

                                        btnSave.Enabled = true;   //IMP                                       
                                        setFocusControl(txtgroupName);
                                    }

                                    if (ViewState["mode"].ToString() == "U")
                                    {
                                        //fetch record
                                        qry = getDisplayQuery();
                                        bool recordExist = this.fetchRecord(qry);
                                        if (recordExist == true)
                                        {
                                            txtgroupCode.Enabled = false;
                                            setFocusControl(txtgroupName);

                                            hdnf.Value = txtgroupCode.Text;
                                        }
                                    }
                                }
                            }
                            else   //Record Not Found
                            {
                                if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                {
                                    lblMsg.Text = "";
                                    setFocusControl(txtgroupName);
                                    txtgroupCode.Enabled = false;
                                    btnSave.Enabled = true;   //IMP
                                }
                                if (ViewState["mode"].ToString() == "U")
                                {
                                    this.makeEmptyForm("E");
                                    lblMsg.Text = "** Record Not Found";
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    txtgroupCode.Text = string.Empty;
                                    setFocusControl(txtgroupCode);
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = string.Empty;
                    setFocusControl(txtgroupCode);
                }
            }
            else
            {
                this.makeEmptyForm("A");
                lblMsg.Text = "Doc No is numeric";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                clsButtonNavigation.enableDisable("E");
                txtgroupCode.Text = string.Empty;
                setFocusControl(txtgroupCode);
            }
        }
        catch
        {

        }
        #endregion
    }

    protected void btntxtgroupCode_Click(object sender, EventArgs e)
    {
        try
        {
            if (btntxtgroupCode.Text == "Change No")
            {
                pnlPopup.Style["display"] = "none";
                txtgroupCode.Text = string.Empty;
                txtgroupCode.Enabled = true;

                btnSave.Enabled = false;
                setFocusControl(txtgroupCode);
            }

            if (btntxtgroupCode.Text == "Choose No")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtgroupCode";
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
        if (hdnfClosePopup.Value == "txtgroupCode" || hdnfClosePopup.Value == "txtEditDoc_No")
        {
            if (btntxtgroupCode.Text == "Change No")
            {
                pnlPopup.Style["display"] = "none";
                txtgroupCode.Text = string.Empty;
                txtgroupCode.Enabled = true;

                btnSave.Enabled = false;
                setFocusControl(txtgroupCode);
                hdnfClosePopup.Value = "Close";
            }

            if (btntxtgroupCode.Text == "Choose No")
            {
                lblPopupHead.Text = "--Select Group--";
                string qry = "SELECT  group_Code,group_Name_E from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (group_Code like '%" + txtSearchText.Text + "%' or group_Name_E like '%" + txtSearchText.Text + "%')";
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

    protected void drpgroupSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnfTran_type.Value = drpgroupSection.SelectedValue;
        setFocusControl(txtGroupOrder);
    }
    protected void drpGroupSummary_SelectedIndexChanged(object sender, EventArgs e)
    {
        setFocusControl(drpgroupSection);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        string docno = string.Empty;
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        hdnfTran_type.Value = drpgroupSection.SelectedValue;
        this.NextNumber();
        setFocusControl(txtgroupName);

    }

    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                obj.code = "group_Code";

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
                                    txtgroupCode.Text = ds.Tables[0].Rows[0][0].ToString();
                                    txtgroupCode.Enabled = false;
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
            #endregion

            #region declaration
            group_Code = txtgroupCode.Text != string.Empty ? Convert.ToInt32(txtgroupCode.Text) : 0;
            bsid = lblbsid.Text != string.Empty ? Convert.ToInt32(lblbsid.Text) : 0;
            group_name = txtgroupName.Text;
            group_name_r = txtgroupnamer.Text;
            group_type = drpgroupSection.SelectedValue;
            group_Summary = drpGroupSummary.SelectedValue;
            group_order = txtGroupOrder.Text != string.Empty ? Convert.ToInt32(txtGroupOrder.Text) : 0;
            Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            userinfo = clsGV.userInfo + DateTime.Now.ToString("dd/MM/yyyy:HHmmss");
            retValue = string.Empty;
            strRev = string.Empty;

            Created_By = Session["user"].ToString();
            Modified_By = Session["user"].ToString();


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
                Head_Fields.Append("group_Code,");
                Head_Values.Append("'" + Doc_No + "',");
                Head_Fields.Append("group_Name_E,");
                Head_Values.Append("'" + group_name + "',");
                Head_Fields.Append("group_Name_R,");
                Head_Values.Append("'" + group_name_r + "',"); 
                Head_Fields.Append("group_Type,");
                Head_Values.Append("'" + group_type + "',");
                Head_Fields.Append("group_Summary,");
                Head_Values.Append("'" + group_Summary + "',");
                Head_Fields.Append("group_Order,");
                Head_Values.Append("'" + group_order + "',");
                //Head_Fields.Append("bsid,");
                //Head_Values.Append("'" + bsid + "',");
                Head_Fields.Append("Company_Code,");
                Head_Values.Append("'" + Company_Code + "' ,");
                Head_Fields.Append("Created_By");
                Head_Values.Append("'" + Created_By + "'");
                #endregion

                flag = 1;
                qry = "insert into " + tblHead + "(" + Head_Fields + ") values(" + Head_Values + ")";
                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = qry;
                Maindt.Rows.Add(dr);



            }
            else
            {
                #region Create Update Query
                Head_Update.Append("group_Name_E=");
                Head_Update.Append("'" + group_name + "',");
                Head_Update.Append("group_Type=");
                Head_Update.Append("'" + group_type + "',");
                Head_Update.Append("group_Summary=");
                Head_Update.Append("'" + group_Summary + "',");
                Head_Update.Append("group_Order=");
                Head_Update.Append("'" + group_order + "',");
                Head_Update.Append("Modified_By=");
                Head_Update.Append("'" + Modified_By + "'");
                #endregion

                flag = 2;
                qry = "update " + tblHead + " set " + Head_Update + " where Company_Code='" + Company_Code
                    + "' and Group_Code='" + txtgroupCode.Text + "' and bsid='"+ bsid +"'";

                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = qry;
                Maindt.Rows.Add(dr);



            }
            #endregion
            string msg = DOPurcSaleCRUD.DOPurcSaleCRUD_OP(flag, Maindt);
            if (msg == "Insert")
            {
                hdnf.Value = bsid.ToString();
                clsButtonNavigation.enableDisable("S");
                this.makeEmptyForm("S");
                qry = getDisplayQuery();
                this.fetchRecord(qry);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Added !')", true);
            }
            else if (msg == "Update")
            {
                hdnf.Value = lblbsid.ToString();
                clsButtonNavigation.enableDisable("S");
                this.makeEmptyForm("S");
                qry = getDisplayQuery();
                this.fetchRecord(qry);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Update !')", true);
            }

        }
        catch
        {

            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('check Entry AND Try Again !')", true);


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
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        txtgroupCode.Enabled = false;
        this.makeEmptyForm("E");
        setFocusControl(txtgroupName);

    }




    #region navigateRecord
    private void navigateRecord()
    {
        try
        {
            if (hdnf.Value != string.Empty)
            {
                ViewState["mode"] = "U";
                txtgroupCode.Text = hdnf.Value;

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
                string str = clsCommon.getString("select Ac_Code from " + AccountMasterTable + " where Group_Code=" + txtgroupCode.Text + "");
                if (str == "0")   //Gledger does not contain this account then delete
                {
                    string currentDoc_No = txtgroupCode.Text;
                    int flag = 3;
                    string strrev = "";

                    qry = "delete from " + tblHead + " where Group_Code='" + currentDoc_No + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                    dr = null;
                    dr = Maindt.NewRow();
                    dr["Querys"] = qry;
                    Maindt.Rows.Add(dr);

                    string msg = DOPurcSaleCRUD.DOPurcSaleCRUD_OP(flag, Maindt);
                    if (msg == "Delete")
                    {
                        Response.Redirect("../Master/PgeGroupMasterUtility.aspx");
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
                hdnf.Value = txtgroupCode.Text;
                showLastRecord();
            }
        }
        catch
        {
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
       
        string type = Request.QueryString["group_Type"];

        hdnf.Value = clsCommon.getString("select max(bsid) from nt_1_bsgroupmaster where Company_Code="
            + Session["Company_Code"].ToString());

        Response.Redirect("pgeBSgroupMaster.aspx?bsid=" + hdnf.Value + "&Action=" + 1 + "&group_Type=" + type);
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
                    string docno = clsCommon.getString("select bsid from " + tblHead + " where Group_Code =" + txtEditDoc_No.Text + " and Company_Code="
            + Session["Company_Code"].ToString());
                    hdnf.Value = docno;
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

    protected void txtgroupName_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(txtgroupnamer);
    }

    protected void txtgroupnamer_TextChanged(object sender, EventArgs e)
    {
        setFocusControl(drpGroupSummary);
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
            counts = Convert.ToInt32(clsCommon.getString("select count(Group_Code) as A from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'"));
            if (counts == 0)
            {
                txtgroupCode.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(Group_Code) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'")) + 1;
                txtgroupCode.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(bsid) as bsid from " + tblHead + " "));
            if (counts == 0)
            {
                lblbsid.Text = "1";
                bsid = 1;
            }
            else
            {
                bsid = Convert.ToInt32(clsCommon.getString("SELECT max(bsid) as bsid from " + tblHead)) + 1;
                lblbsid.Text = bsid.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }
    #endregion
}
