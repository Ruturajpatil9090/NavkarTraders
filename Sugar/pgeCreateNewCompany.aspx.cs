using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using System.Threading;
using System.Configuration;

public partial class Sugar_pgeCreateNewCompany : System.Web.UI.Page
{
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string tblPrefix = string.Empty;
    string user = string.Empty;

    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    #region
    int Company_Code = 0;
    string Company_Name_E = string.Empty;
    string Company_Name_R = string.Empty;
    string Address_E = string.Empty;
    string Address_R = string.Empty;
    string City_E = string.Empty;
    string City_R = string.Empty;
    string State_E = string.Empty;
    string State_R = string.Empty;
    string Mobile_No = string.Empty;
    string PIN = string.Empty;
    string Pan_No = string.Empty;
    string FSSAI_No = string.Empty;
    string CST = string.Empty;
    string TIN = string.Empty;
    string GST = string.Empty;
    string PHONE = string.Empty;
    string Group_Code = string.Empty;
    string Created_By = string.Empty;
    string Modified_By = string.Empty;
    int count = 0;
    string cs = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        user = Session["user"].ToString();
        cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        con = new SqlConnection(cs);
        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                clsButtonNavigation.enableDisable("N");
                this.makeEmptyForm("N");
                ViewState["mode"] = "I";
                this.showLastRecord();
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
        try
        {
            string u = Session["user"].ToString();
        }
        catch
        {
            Response.Redirect("http://localhost:3445/HomePage/pgeloginForm.aspx");
        }

    }
    private void showLastRecord()
    {
        try
        {
            string qry = string.Empty;
            qry = "select max(Company_Code) as Company_Code from Company";
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
                        bool recordExist = this.fetchRecord(dt.Rows[0]["Company_Code"].ToString());
                        if (recordExist == true)
                        {
                            btnEdit.Focus();
                        }
                    }
                }
            }

            this.enableDisableNavigateButtons();
        }
        catch
        {

        }
    }

    #region ---- makeEmptyForm ----
    private void makeEmptyForm(string dAction)
    {
        try
        {
            if (dAction == "N")
            {
                foreach (System.Web.UI.Control c in Panel1.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                    //if (c is System.Web.UI.WebControls.DropDownList)
                    //    ((System.Web.UI.WebControls.DropDownList)c).SelectedIndex = 0;
                    //if (c is System.Web.UI.WebControls.CheckBox)
                    //    ((System.Web.UI.WebControls.CheckBox)c).Checked = false;                
                }
                btnSave.Text = "Save";
                txtCompanyCode.Enabled = false;
            }

            if (dAction == "A")
            {
                foreach (System.Web.UI.Control c in Panel1.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                btnSave.Text = "Save";
                txtCompanyCode.Enabled = false;
            }

            if (dAction == "S")
            {
                foreach (System.Web.UI.Control c in Panel1.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                }
                txtCompanyCode.Enabled = false;
            }

            if (dAction == "E")
            {
                foreach (System.Web.UI.Control c in Panel1.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                txtCompanyCode.Enabled = false;
            }

            lblMsg.Text = "";
        }
        catch
        {

        }
    }
    #endregion

    #region btnAdd_Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");

        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        this.NextNumber();
        SetFocus(txtCompanyName);

    }
    #endregion

    #region --- getMaxCode ---
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;

            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = "Company";
                obj.code = "Company_Code";

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
                                    txtCompanyCode.Text = ds.Tables[0].Rows[0][0].ToString();
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

    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        count = saveData();
        clsButtonNavigation.enableDisable("S");
        this.makeEmptyForm("S");

        if (count == 1)
        {
            lblMsg.Text = "Successfully Added!";
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
        else if (count == 2)
        {
            lblMsg.Text = "Successfully Update!";
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
        //if (retValue != "-3" && retValue != "-2" && retValue != "")
        //{
        //    lblMsg.Text = "Successfully Added!";
        //    lblMsg.ForeColor = System.Drawing.Color.LimeGreen;

        //    clsCheckDatabase.createCompanyTables(retValue, txtCompanyName.Text);

        //    //#region Default Branch Entry
        //    //DataSet ds = new DataSet();
        //    //int grpCode=Convert.ToInt32(Session["Group_Code"].ToString());
        //    //int CompanyCode= Convert.ToInt32(hdnf.Value);
        //    //string defaultBranch="Default";
        //    //string qry = "insert into BranchMaster (Group_Code,Company_Code,Branch) values('" + grpCode + "','" + CompanyCode + "','" + defaultBranch + "')";
        //    //ds = clsDAL.SimpleQuery(qry);

        //    //#endregion

        //    Response.Redirect("pgeStartup.aspx?groupcode=" + Session["Group_Code"].ToString() + "&uname=" + Session["uname"].ToString() + "&pass=" + Session["pass"].ToString() + "");
        //}

    }
    #endregion

    #region [saveData]
    private int saveData()
    {
        try
        {
            int flag = 0;
            string str = "";
            DataSet ds = null;
            #region Head Part
            Company_Code = Convert.ToInt32(txtCompanyCode.Text);
            Company_Name_E = txtCompanyName.Text;
            Company_Name_R = txtRegionalName.Text;
            Address_E = txtAddress.Text;
            Address_R = txtRegionalAddress.Text;
            City_E = txtcityE.Text;
            City_R = txtcityR.Text;
            State_E = txtstateE.Text;
            State_R = txtstateR.Text;
            Mobile_No = txtMob.Text;
            PIN = txtpin.Text;
            Pan_No = txtPanNo.Text;
            FSSAI_No = txtFSSAI.Text;
            CST = txtCSTNo.Text;
            TIN = txtTinNo.Text;
            GST = txtGST.Text;
            PHONE = txtPhone.Text;
            Group_Code = Session["Group_Code"].ToString();
            Created_By = Session["user"].ToString();
            Modified_By = Session["user"].ToString(); ;
            #endregion

            if (btnSave.Text == "Save")
            {
                this.NextNumber();
                #region
                string Head_Fields = "Company_Code,";
                string Head_Values = " '" + Company_Code + " ',";
                Head_Fields = Head_Fields + "Company_Name_E,";
                Head_Values = Head_Values + " '" + Company_Name_E + " ',";
                Head_Fields = Head_Fields + "Company_Name_R,";
                Head_Values = Head_Values + "  '" + Company_Name_R + " ',";
                Head_Fields = Head_Fields + "Address_E,";
                Head_Values = Head_Values + "  '" + Address_E + " ',";
                Head_Fields = Head_Fields + "Address_R,";
                Head_Values = Head_Values + "  '" + Address_R + " ',";
                Head_Fields = Head_Fields + "City_E,";
                Head_Values = Head_Values + "  '" + City_E + " ',";
                Head_Fields = Head_Fields + "City_R,";
                Head_Values = Head_Values + "  '" + City_R + " ',";
                Head_Fields = Head_Fields + "State_E,";
                Head_Values = Head_Values + "  '" + State_E + " ',";
                Head_Fields = Head_Fields + "State_R,";
                Head_Values = Head_Values + "  '" + State_R + " ',";
                Head_Fields = Head_Fields + "Mobile_No,";
                Head_Values = Head_Values + "  '" + Mobile_No + " ',";
                Head_Fields = Head_Fields + "PIN,";
                Head_Values = Head_Values + "  '" + PIN + " ',";
                Head_Fields = Head_Fields + "Pan_No,";
                Head_Values = Head_Values + "  '" + Pan_No + " ',";
                Head_Fields = Head_Fields + "FSSAI_No,";
                Head_Values = Head_Values + "  '" + FSSAI_No + " ',";
                Head_Fields = Head_Fields + "CST,";
                Head_Values = Head_Values + "  '" + CST + " ',";
                Head_Fields = Head_Fields + "TIN,";
                Head_Values = Head_Values + "  '" + TIN + " ',";
                Head_Fields = Head_Fields + "GST,";
                Head_Values = Head_Values + "  '" + GST + " ',";
                Head_Fields = Head_Fields + "PHONE,";
                Head_Values = Head_Values + "  '" + PHONE + " ',";
                Head_Fields = Head_Fields + "Group_Code,";
                Head_Values = Head_Values + "  '" + Group_Code + " ',";
                Head_Fields = Head_Fields + "Created_By";
                Head_Values = Head_Values + "  '" + Created_By + " '";

                qry = "insert into Company (" + Head_Fields + ") values (" + Head_Values + ")";
                #endregion
                flag = 1;
            }
            else
            {
                #region

                string Head_Update = "Company_Name_E=";
                Head_Update = Head_Update + " '" + Company_Name_E + " ',";
                Head_Update = Head_Update + "Company_Name_R=";
                Head_Update = Head_Update + "  '" + Company_Name_R + " ',";
                Head_Update = Head_Update + "Address_E=";
                Head_Update = Head_Update + "  '" + Address_E + " ',";
                Head_Update = Head_Update + "Address_R=";
                Head_Update = Head_Update + "  '" + Address_R + " ',";
                Head_Update = Head_Update + "City_E=";
                Head_Update = Head_Update + "  '" + City_E + " ',";
                Head_Update = Head_Update + "City_R=";
                Head_Update = Head_Update + "  '" + City_R + " ',";
                Head_Update = Head_Update + "State_E=";
                Head_Update = Head_Update + "  '" + State_E + " ',";
                Head_Update = Head_Update + "State_R=";
                Head_Update = Head_Update + "  '" + State_R + " ',";
                Head_Update = Head_Update + "Mobile_No=";
                Head_Update = Head_Update + "  '" + Mobile_No + " ',";
                Head_Update = Head_Update + "PIN=";
                Head_Update = Head_Update + "  '" + PIN + " ',";
                Head_Update = Head_Update + "Pan_No=";
                Head_Update = Head_Update + "  '" + Pan_No + " ',";
                Head_Update = Head_Update + "FSSAI_No=";
                Head_Update = Head_Update + "  '" + FSSAI_No + " ',";
                Head_Update = Head_Update + "CST=";
                Head_Update = Head_Update + "  '" + CST + " ',";
                Head_Update = Head_Update + "TIN=";
                Head_Update = Head_Update + "  '" + TIN + " ',";
                Head_Update = Head_Update + "GST=";
                Head_Update = Head_Update + "  '" + GST + " ',";
                Head_Update = Head_Update + "PHONE=";
                Head_Update = Head_Update + "  '" + PHONE + " ',";
                Head_Update = Head_Update + "Group_Code=";
                Head_Update = Head_Update + "  '" + Group_Code + " ',";
                Head_Update = Head_Update + "Modified_By=";
                Head_Update = Head_Update + "  '" + Modified_By + " '";

                qry = "update Company set " + Head_Update + " where Company_Code='" + Company_Code + "'";
                #endregion
                flag = 2;
            }
            Thread thred = new Thread(() => { count = DataStore(qry, flag); }); //Calling DataStore Method Using Thread
            thred.Start(); //Thread Operation Start
            thred.Join();

            hdnf.Value = count.ToString();


            return count;

        }
        catch
        {
            return 0;
        }
    }


    #endregion

    #region NextNumber
    private void NextNumber()
    {
        try
        {
            int count = Convert.ToInt32(clsCommon.getString("select isnull(max(Company_Code),0) as code from company")) + 1;
            if (count == 0)
            {
                txtCompanyCode.Text = "1";
                Company_Code = 1;
            }
            else
            {
                txtCompanyCode.Text = count.ToString();
                Company_Code = count;
            }
        }
        catch
        {

        }
    }
    #endregion
    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        btnSave.Text = "Update";
        clsButtonNavigation.enableDisable("E");
        this.makeEmptyForm("E");
        SetFocus(txtCompanyName);

    }
    #endregion

    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("N");
        this.makeEmptyForm("N");
        this.showLastRecord();
    }
    #endregion

    #region [txtCompanyName_TextChanged]
    protected void txtCompanyName_TextChanged(object sender, EventArgs e)
    {
        if (txtCompanyName.Text != string.Empty)
        {
            btnSave.Enabled = true;
            txtRegionalName.Focus();
        }
        else
        {
            btnSave.Enabled = false;
        }
    }
    #endregion

    #region [txtCompanyCode_TextChanged]
    protected void txtCompanyCode_TextChanged(object sender, EventArgs e)
    {
        #region code
        try
        {
            int n;
            bool isNumeric = int.TryParse(txtCompanyCode.Text, out n);
            //if(isiNan()
            if (isNumeric == true)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string txtValue = "";
                if (txtCompanyCode.Text != string.Empty)
                {
                    txtValue = txtCompanyCode.Text;

                    string qry = "select * from Company where Company_Code='" + txtValue + "' ";
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                if (ViewState["mode"] != null)
                                {
                                    if (ViewState["mode"].ToString() == "I")
                                    {
                                        //Response.Write("<script>alert('Code Already Exist');</script>");
                                        lblMsg.Text = "** Company Code Already Exist";
                                        lblMsg.ForeColor = System.Drawing.Color.Red;
                                        this.getMaxCode();
                                        txtCompanyCode.Focus();
                                    }

                                    if (ViewState["mode"].ToString() == "U")
                                    {
                                        //fetch record
                                        bool recordExist = this.fetchRecord(txtValue);
                                        if (recordExist == true)
                                        {
                                            // btnEdit.Enabled = true;
                                            // btnEdit.Focus();
                                            txtCompanyName.Focus();
                                            txtCompanyCode.Enabled = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (ViewState["mode"].ToString() == "I")
                                {
                                    lblMsg.Text = "";
                                    txtCompanyName.Focus();
                                    txtCompanyCode.Enabled = false;
                                }
                                if (ViewState["mode"].ToString() == "U")
                                {
                                    // Response.Write("<script>alert('Record Not Found');</script>");

                                    this.makeEmptyForm("A");
                                    lblMsg.Text = "** Record Not Found";
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    txtCompanyCode.Text = string.Empty;
                                    txtCompanyCode.Focus();
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = string.Empty;
                    txtCompanyCode.Focus();
                }
            }
            else
            {
                this.makeEmptyForm("A");
                lblMsg.Text = "Company code is numeric";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                clsButtonNavigation.enableDisable("E");
                txtCompanyCode.Text = string.Empty;
                txtCompanyCode.Focus();

            }
        }
        catch
        {

        }
        #endregion
    }
    #endregion

    #region [fetchRecord]
    public bool fetchRecord(string txtValue)
    {
        try
        {
            bool recordExist = false;
            string qry = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            qry = "select * from Company where Company_Code=" + txtValue;
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        hdnf.Value = txtValue;
                        txtCompanyCode.Text = txtValue;
                        txtCompanyName.Text = dt.Rows[0]["Company_Name_E"].ToString();
                        txtRegionalName.Text = dt.Rows[0]["Company_Name_R"].ToString();
                        txtAddress.Text = dt.Rows[0]["Address_E"].ToString();
                        txtRegionalAddress.Text = dt.Rows[0]["Address_R"].ToString();
                        txtcityE.Text = dt.Rows[0]["City_E"].ToString();
                        txtcityR.Text = dt.Rows[0]["City_R"].ToString();
                        txtstateE.Text = dt.Rows[0]["State_E"].ToString();
                        txtstateR.Text = dt.Rows[0]["State_R"].ToString();
                        txtMob.Text = dt.Rows[0]["Mobile_No"].ToString();
                        txtpin.Text = dt.Rows[0]["PIN"].ToString();
                        txtPanNo.Text = dt.Rows[0]["Pan_No"].ToString();
                        txtFSSAI.Text = dt.Rows[0]["FSSAI_No"].ToString();
                        txtCSTNo.Text = dt.Rows[0]["CST"].ToString();
                        txtTinNo.Text = dt.Rows[0]["TIN"].ToString();
                        txtGST.Text = dt.Rows[0]["GST"].ToString();
                        txtPhone.Text = dt.Rows[0]["PHONE"].ToString();
                        recordExist = true;
                        lblMsg.Text = "";
                    }
                }
            }
            //   this.enableDisableNavigateButtons();
            return recordExist;

        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons

        //if (ViewState["mode"].ToString() == "U")
        //{

        int RecordCount = 0;

        string query = "";
        query = "select count(*) from Company";
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
            //  btnLast.Focus();
        }

        if (txtCompanyCode.Text != string.Empty)
        {

            #region check for next or previous record exist or not
            ds = new DataSet();
            dt = new DataTable();

            query = "SELECT top 1 [Company_Code] from Company where Company_Code>" + Convert.ToInt32(hdnf.Value) + " ORDER BY Company_Code asc  ";
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
                    }
                    else
                    {
                        //next record does not exist
                        btnNext.Enabled = false;
                    }
                }
            }

            ds = new DataSet();
            dt = new DataTable();

            query = "SELECT top 1 [Company_Code] from Company where Company_Code<" + Convert.ToInt32(hdnf.Value) + " ORDER BY Company_Code asc  ";
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
                    }
                    else
                    {
                        btnPrevious.Enabled = false;
                    }
                }
            }

        }
            #endregion
        // }

        #endregion
    }
    #endregion

    #region [btnFirst_Click]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select Company_Code from Company where Company_Code=(select MIN(Company_Code) from Company) ";
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
                        txtCompanyCode.Text = dt.Rows[0][0].ToString();
                        ViewState["mode"] = "U";
                        clsButtonNavigation.enableDisable("E");
                        bool recordExist = this.fetchRecord(dt.Rows[0][0].ToString());
                        if (recordExist == true)
                        {
                            btnEdit.Enabled = true;
                            btnEdit.Focus();
                        }

                        this.enableDisableNavigateButtons();
                        this.makeEmptyForm("S");

                    }
                }
            }


        }
        catch
        {

        }
    }
    #endregion

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCompanyCode.Text != string.Empty)
            {
                string query = "";
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                query = "SELECT top 1 [Company_Code] from Company where Company_Code<" + Convert.ToInt32(hdnf.Value) + " ORDER BY Company_Code DESC  ";

                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["mode"] = "U";
                            clsButtonNavigation.enableDisable("E");
                            bool recordExist = this.fetchRecord(dt.Rows[0]["Company_Code"].ToString());
                            if (recordExist == true)
                            {
                                btnEdit.Enabled = true;
                                btnEdit.Focus();
                            }
                            this.enableDisableNavigateButtons();
                            this.makeEmptyForm("S");
                        }
                    }
                }
            }

        }
        catch
        {

        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCompanyCode.Text != string.Empty)
            {
                string query = "";
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                query = "SELECT top 1 [Company_Code] from Company where Company_Code>" + Convert.ToInt32(hdnf.Value) + " ORDER BY Company_Code asc  ";

                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["mode"] = "U";
                            clsButtonNavigation.enableDisable("E");
                            bool recordExist = this.fetchRecord(dt.Rows[0]["Company_Code"].ToString());
                            if (recordExist == true)
                            {
                                btnEdit.Enabled = true;
                                btnEdit.Focus();
                            }
                            this.enableDisableNavigateButtons();
                            this.makeEmptyForm("S");
                        }
                    }
                }
            }

        }
        catch
        {

        }
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCompanyCode.Text != string.Empty)
            {
                string query = "";
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                query = "select Company_Code from Company where Company_Code=(select MAX(Company_Code) from Company) ";

                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["mode"] = "U";
                            clsButtonNavigation.enableDisable("E");
                            bool recordExist = this.fetchRecord(dt.Rows[0]["Company_Code"].ToString());
                            if (recordExist == true)
                            {
                                btnEdit.Enabled = true;
                                btnEdit.Focus();
                            }
                            this.enableDisableNavigateButtons();
                            this.makeEmptyForm("S");
                        }
                    }
                }
            }

        }
        catch
        {

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (hdconfirm.Value == "Yes")
            {
                int flag = 3;
                string query = string.Empty;
                query = "delete from Company where Company_Code =" + hdnf.Value;
                Thread thred = new Thread(() => { count = DataStore(query, flag); }); //Calling DataStore Method Using Thread
                thred.Start(); //Thread Operation Start
                thred.Join();
                if (count == 3)
                {
                    lblMsg.Text = "Successfully Deleted";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    this.showLastRecord();
                    // this.makeEmptyForm("N");
                    clsButtonNavigation.enableDisable("S");
                }



            }
        }
        catch
        {

        }

    }




    //private void HandleCustomPostbackEvent(string ctrlName, string args)
    //{
    //     onblur="__doPostBack('tbOnBlur','OnBlur');" 
    //    //Since this will get called for every postback, we only
    //    // want to handle a specific combination of control
    //    // and argument.
    //    if (ctrlName == txtCompanyCode.UniqueID && args == "OnBlur")
    //    {

    //        if (ViewState["mode"] != null)
    //        {

    //            if (ViewState["mode"].ToString() == "U")
    //            {
    //                if (txtCompanyCode.Text != string.Empty)
    //                {
    //                    bool recordExist = this.fetchRecord(txtCompanyCode.Text);
    //                    if (recordExist == true)
    //                    {
    //                        txtCompanyCode.Enabled = false;
    //                        txtCompanyName.Focus();
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                txtCompanyCode.Enabled = false;
    //                txtCompanyName.Focus();
    //            }

    //        }
    //    }
    //}


    [WebMethod]
    public static clsCompany[] BindDatatable(string str)
    {
        DataTable dt = new DataTable();
        List<clsCompany> details = new List<clsCompany>();

        clsCompany obj = new clsCompany();

        string qry = "select * from Company where Company_Code=" + str;
        DataSet ds = new DataSet();
        //
        //DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    obj.Company_Name_E = dt.Rows[0]["Company_Name_E"].ToString();
                    obj.Company_Name_R = dt.Rows[0]["Company_Name_R"].ToString();
                    obj.City_E = dt.Rows[0]["City_E"].ToString();
                    obj.City_R = dt.Rows[0]["City_R"].ToString();
                    obj.State_E = dt.Rows[0]["State_E"].ToString();
                    obj.State_R = dt.Rows[0]["State_R"].ToString();
                    obj.Mobile_No = dt.Rows[0]["Mobile_No"].ToString();
                    obj.Pan_No = dt.Rows[0]["Pan_No"].ToString();
                    obj.PIN = dt.Rows[0]["PIN"].ToString();
                    obj.Address_E = dt.Rows[0]["Address_E"].ToString();
                    obj.Address_R = dt.Rows[0]["Address_R"].ToString();

                    details.Add(obj);
                }
            }
        }
        return details.ToArray();

    }
    #region DataStore
    private int DataStore(string Query, int flag)
    {
        int count = 0;
        try
        {
            //Connection open

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


            }
            return count;

        }
        finally
        {
            con.Close();
        }

    }
    #endregion
}