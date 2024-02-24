using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

public partial class Sugar_Utility_pgeUser_Creation : System.Web.UI.Page
{
    #region data section
    string qry = string.Empty;
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string tblDetailReport = string.Empty;
    string qryCommonDetail = string.Empty;
    string qryCommonReport = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    string AccountMasterTable = string.Empty;
    string qryAccountList = string.Empty;
    static WebControl objAsp = null;
    string cs = string.Empty;
    string Action = string.Empty;
    int DID = 0;
    int flag = 0;
    int count = 0;
    int Doc_No = 0;
    int id = 0;
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    string msg = string.Empty;

    #endregion

    #region -Head part declearation
    int Company_Code = 0;
    int Branch_Code = 0;
    int Year_Code = 0;
    string Created_By = string.Empty;
    string Modified_By = string.Empty;
    string Created_Date = string.Empty;
    string Modified_Date = string.Empty;
    string Head_Update = string.Empty;
    string Head_Insert = string.Empty;
    string Head_Delete = string.Empty;
    string Head_Values = string.Empty;
    string Head_Fields = string.Empty;
    int IsSave = 0;

    int User_Id = 0;
    string userFull_name = string.Empty;
    string User_Name = string.Empty;
    string User_Type = string.Empty;
    string Password = string.Empty;
    string EmailId = string.Empty;
    string EmailPassword = string.Empty;
    string smtpServerPort = string.Empty;
    int AuthoGroupID = 0;
    int Ac_Code = 0;
    string Mobile_no = string.Empty;
    string userauth = string.Empty;
    string LastActivityDate = string.Empty;
    int RetryAttempts = 0;
    int IsLocked = 0;
    string LockedDateTime = string.Empty;
    int uid = 0;
    int counts = 0;

    #endregion-End of Head part declearation

    #region Detail part Declaration

    int Detail_Id = 0;

    //int User_IdDetail = 1;
    int Program_Name = 1;
    int Tran_Type = 2;
    int Permission = 3;
    //int Company_CodeDetail = 4;
    //int Year_CodeDetail = 6;
    //int udid = 7;
    //int uid_detail = 8;

    //int Pagetype = 9;
    int Rowaction = 4;
    int Srno = 5;

    int Grid_Id = 0;
    int Grid_Userid = 0;
    string Grid_Programname = string.Empty;
    string Grid_Trantype = string.Empty;
    string Grid_Permission = string.Empty;
    string pagrtype = string.Empty;
    int GridCompany_Code = 0;
    int GridYear_Code = 0;
    int Grid_udid = 0;
    int Grid_uid = 0;

    string Detail_Insert = string.Empty;
    string Detail_Update = string.Empty;
    string Detail_Delete = string.Empty;
    string Detail_Fields = string.Empty;
    string Detail_Values = string.Empty;
    #endregion

    #region[Detail part Report declaration]
    int Detail_Id_Report = 0;
    //int User_IdReport = 3;
    int Program_Name_Report = 1;
    int Tran_Type_Report = 2;
    int Permission_report = 3;
    //int Company_CodeReport = 7;
    //int Year_CodeReport = 8;
    //int udrid = 9;
    //int uidReport = 10;

    int RowactionResult = 4;
    int SrnoResult = 5;

    int GridDetail_IdReport = 0;
    int GridUser_IdReport = 0;
    string GridProgram_NameReport = string.Empty;
    string GridTran_TypeReport = string.Empty;
    string GridPermissionReport = string.Empty;
    int GridCompany_CodeReport = 0;
    int GridYear_CodeReport = 0;
    int Gridudrid = 0;
    int GriduidReport = 0;

    string Detail_Insert_Report = string.Empty;
    string Detail_Update_Report = string.Empty;
    string Detail_Delete_Report = string.Empty;
    string Detail_Fields_Report = string.Empty;
    string Detail_Values_Report = string.Empty;
    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {

        tblPrefix = Session["tblPrefix"].ToString();
        tblHead = "tbluser";
        tblDetails = "tbluserdetail";
        tblDetailReport = "tbluserdetail_report";
        qryCommonDetail = "qryusercreationdetail";
        qryCommonReport = "qryusercreationreport";
        user = Session["user"].ToString();
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
                    hdnf.Value = Request.QueryString["uid"];
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
                    setFocusControl(txtuserfullname);
                    btnAdd_Click(this, new EventArgs());
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
    #endregion

    #region [txtUser_Id_TextChanged]
    protected void txtUser_Id_TextChanged(object sender, EventArgs e)
    {
    }
    #endregion

    #region [btntxtUser_Id_Click]
    protected void btntxtUser_Id_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region [txtUser_Name_TextChanged]
    protected void txtUser_Name_TextChanged(object sender, EventArgs e)
    {
    }
    #endregion

    #region [txtPassword_TextChanged]
    protected void txtPassword_TextChanged(object sender, EventArgs e)
    {
    }
    #endregion

    #region [txtEmailId_TextChanged]
    protected void txtEmailId_TextChanged(object sender, EventArgs e)
    {
    }
    #endregion

    #region [txtEmailPassword_TextChanged]
    protected void txtEmailPassword_TextChanged(object sender, EventArgs e)
    {
    }
    #endregion

    #region [txtMobile_TextChanged]
    protected void txtMobile_TextChanged(object sender, EventArgs e)
    {
    }
    #endregion

    #region [drpUser_Type_TextChanged]
    protected void drpUser_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion

    #region [drpUseAuthority_SelectedIndexChanged]
    protected void drpUseAuthority_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion

    #region [btnOpenDetailsPopup_Click]
    protected void btnOpenDetailsPopup_Click(object sender, EventArgs e)
    {

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
        setFocusControl(txtuserfullname);
        pnlPopupDetails.Style["display"] = "none";
        //Int32 User_Id = Convert.ToInt32(clsCommon.getString("select AUTO_INCREMENT(" + tblHead + ") as Doc_No"));
        int User_Id = Convert.ToInt32(clsCommon.getString("select max(User_Id) as Doc_No from tbluser"));
        if (User_Id != 0)
        {
            int doc_no = User_Id + 1;
            User_Id = doc_no;
        }
        else
        {
            User_Id = 1;
        }
        txtUser_Id.Text = Convert.ToString(User_Id);
        #region Details
        qry = "select  Row_number() over (order by Tran_Type) as Detail_Id,Program_Name,Tran_Type,Page_Type,'' as Permission,'' as rowAction,'' as SrNo from program_master where Page_Type='p' ";
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
                    //dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    //dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["rowAction"] = "A";
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
        pnlgrdDetail.Enabled = true;
        #endregion


        #region Details REPORT
        qry = "select  Row_number() over (order by Tran_Type) as Detail_Id_Report,Program_Name as Program_Name_Report,Tran_Type as Tran_Type_Report,'' as Permission_report ,'' as rowAction,'' as SrNo from program_master where Page_Type='R' ";
        DataSet ds1 = new DataSet();
        DataTable dt1 = new DataTable();
        ds1 = clsDAL.SimpleQuery(qry);
        if (ds1 != null)
        {
            if (ds1.Tables.Count > 0)
            {
                dt1 = ds1.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    //dt1.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    //dt1.Columns.Add(new DataColumn("SrNo", typeof(int)));
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        dt1.Rows[i]["rowAction"] = "A";
                        dt1.Rows[i]["SrNo"] = i + 1;
                    }
                    grdDetail_Report.DataSource = dt1;
                    grdDetail_Report.DataBind();
                    ViewState["currentTable1"] = dt1;
                }
                else
                {
                    grdDetail_Report.DataSource = null;
                    grdDetail_Report.DataBind();
                    ViewState["currentTable1"] = null;
                }
            }
            else
            {
                grdDetail_Report.DataSource = null;
                grdDetail_Report.DataBind();
                ViewState["currentTable1"] = null;
            }
        }
        else
        {
            grdDetail_Report.DataSource = null;
            grdDetail_Report.DataBind();
            ViewState["currentTable1"] = null;
        }
        pnlgrdDetailReport.Enabled = true;
        #endregion
    }
    #endregion

    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(uid),0) as uid from tbluser "));

        hdnf.Value = Convert.ToString(maxno);
        clsButtonNavigation.enableDisable("S");
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                string currentDoc_No = txtUser_Id.Text;
                int flag = 3;


                Head_Delete = "delete from " + tblHead + " where User_Id='" + currentDoc_No + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                Thread thred = new Thread(() => { count = DataStore(flag); }); //Calling DataStore Method Using Threadthred.Start(); //Thread Operation Start
                thred.Start(); //Thread Operation Start
                thred.Join();


                //if (count == 3)
                //{
                //    Response.Redirect("../Master/pgeGSTRateMaster_Data.aspx");
                //}
            }
            else
            {
                lblMsg.Text = "Cannot delete this Group , it is in use";
                lblMsg.ForeColor = System.Drawing.Color.Red;
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
        clsButtonNavigation.enableDisable("E");
        pnlgrdDetail.Enabled = true;
        pnlgrdDetailReport.Enabled = true;
        this.makeEmptyForm("E");
        txtUser_Id.Enabled = false;
        setFocusControl(txtuserfullname);
        #region[Detail Edit]
        Int32 countviewstate = grdDetail.Rows.Count;
        int counttable = Convert.ToInt32(clsCommon.getString("Select count(Doc_No) from program_master where page_type='P'"));

        if (countviewstate == counttable)
        {

        }
        else
        {
            qry = "select " + countviewstate + "+ Row_number() over(order by Tran_Type) as Detail_Id,Program_Name,Tran_Type,'' as Permission" +
                ",'' as rowAction,'' as SrNo from Program_Master where Doc_No>"
                + countviewstate + " and page_type='P' order by Doc_No";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dtview = new DataTable();

            dtview = (DataTable)ViewState["currentTable"];

            ds = clsDAL.SimpleQuery(qry);
            dt = ds.Tables[0];
            if (dtview == null)
            {
                dtview = dt.Copy();
            }
            if (dtview != null)
            {
                if (dtview.Rows.Count > 0)
                {
                    if (dtview.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["rowAction"] = "A";
                            dt.Rows[i]["SrNo"] = countviewstate + 1;
                            countviewstate = countviewstate + 1;
                        }
                        dtview.Merge(dt, true, MissingSchemaAction.Ignore);
                        dtview = dtview.AsEnumerable()
                       .GroupBy(r => new { Itemid = r.Field<string>("Program_Name"), PacktypeId = r.Field<string>("Tran_Type") },
                            (key, rows) => rows.OrderByDescending(r => r.Field<string>("rowAction")).First())
                            // .Where(g => g.Field<string>("Modul") !="")
                            //.Select(g => g.First())
                       .CopyToDataTable();
                        grdDetail.DataSource = dtview;
                        //grdDetail.DataSource = dt;
                        grdDetail.DataBind();
                        ViewState["currentTable"] = dtview;
                        for (int i = 0; i < dtview.Rows.Count; i++)
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            CheckBox chkpermission = (CheckBox)grdDetail.Rows[i].Cells[3].FindControl("chkpermission");
                            string a = dtview.Rows[i]["Permission"].ToString();
                            if (a == "Y")
                            {
                                chkpermission.Text = "";
                                chkpermission.Checked = true;

                            }
                            else
                            {
                                chkpermission.Text = "";
                                chkpermission.Checked = false;
                            }

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
            }
            else
            {
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                ViewState["currentTable"] = null;
            }

        }
        #endregion

        #region[Detail Edit from second grid]
        Int32 countviewstate1 = grdDetail_Report.Rows.Count;
        int counttable1 = Convert.ToInt32(clsCommon.getString("Select count(Doc_No) from program_master where page_type='R'"));

        if (countviewstate1 == counttable1)
        {

        }
        else
        {
            qry = "select " + countviewstate1 + "+ Row_number() over(order by Tran_Type) as Detail_Id_Report,Program_Name as Program_Name_Report,Tran_Type as Tran_Type_Report ,'' as Permission_report,'' as rowAction,'' as SrNo from program_master where Doc_No>" + countviewstate1 + " and page_type='R' order by Doc_No";
            DataSet ds1 = new DataSet();
            DataTable dt1 = new DataTable();
            DataTable dtview1 = new DataTable();

            dtview1 = (DataTable)ViewState["currentTable1"];
            ds1 = clsDAL.SimpleQuery(qry);
            dt1 = ds1.Tables[0];
            if (dtview1 == null)
            {
                dtview1 = dt1.Copy();
            }
            if (dtview1 != null)
            {
                if (dtview1.Rows.Count > 0)
                {

                    if (dtview1.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            dt1.Rows[i]["rowAction"] = "A";
                            dt1.Rows[i]["SrNo"] = countviewstate1 + 1;
                            countviewstate1 = countviewstate1 + 1;
                        }

                        dtview1.Merge(dt1, true, MissingSchemaAction.Ignore);
                        //DataTable distinctTable = dtview1.DefaultView.ToTable(true, "Program_Name_Report", "Tran_Type_Report", "Permission_report", "rowAction", "SrNo");
                        DataTable dt_Barcode = new DataTable();
                        //            dtview1 = dtview1.AsEnumerable()
                        //.GroupBy(r => new { Itemid = r.Field<string>("Program_Name_Report"), PacktypeId = r.Field<string>("Tran_Type_Report") })
                        //.Select(g => g.First())
                        //.CopyToDataTable();
                        dtview1 = dtview1.AsEnumerable()
                      .GroupBy(r => new { Itemid = r.Field<string>("Program_Name_Report"), PacktypeId = r.Field<string>("Tran_Type_Report") },
                           (key, rows) => rows.OrderByDescending(r => r.Field<string>("rowAction")).First())
                            // .Where(g => g.Field<string>("Modul") !="")
                            //.Select(g => g.First())
                      .CopyToDataTable();


                        grdDetail_Report.DataSource = dtview1;
                        //grdDetail_Report.DataSource = dt1;
                        grdDetail_Report.DataBind();
                        ViewState["currentTable1"] = dtview1;
                        for (int i = 0; i < dtview1.Rows.Count; i++)
                        {
                            CheckBox chkpermission = (CheckBox)grdDetail_Report.Rows[i].Cells[3].FindControl("chkpermission");
                            string a = dtview1.Rows[i]["Permission_report"].ToString();
                            if (a == "Y")
                            {
                                chkpermission.Text = "";
                                chkpermission.Checked = true;

                            }
                            else
                            {
                                chkpermission.Text = "";
                                chkpermission.Checked = false;
                            }

                        }
                    }
                    else
                    {
                        grdDetail_Report.DataSource = null;
                        grdDetail_Report.DataBind();
                        ViewState["currentTable1"] = null;
                    }
                }
                else
                {
                    grdDetail_Report.DataSource = null;
                    grdDetail_Report.DataBind();
                    ViewState["currentTable1"] = null;
                }
            }
            else
            {
                grdDetail_Report.DataSource = null;
                grdDetail_Report.DataBind();
                ViewState["currentTable1"] = null;
            }

        }
        #endregion


    }
    #endregion

    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {

        #region [Validation Part]
        bool isValidated = true;
        if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
            return;

        }
        if (btnSave.Text == "Save")
        {
            string username = clsCommon.getString("Select User_Name from tbluser where User_Name='" + txtUser_Name.Text +
                "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            if (username == string.Empty && username == "0")
            {
                isValidated = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('User name is already Exists!!!!')", true);
                setFocusControl(txtUser_Name);
                return;

            }
            else
            {
                isValidated = true;
            }

        }
        if (txtUser_Id.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtUser_Id);
            return;
        }
        if (txtUser_Name.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtUser_Name);
            return;
        }
        if (txtPassword.Text != string.Empty)
        {
            isValidated = true;
            EncryptPass encr = new EncryptPass();
            hdnfpass.Value = encr.Encrypt(txtPassword.Text);
        }
        else
        {
            isValidated = false;
            setFocusControl(txtPassword);
            return;
        }
        if (txtEmailId.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtEmailId);
            return;
        }
        if (txtEmailPassword.Text != string.Empty)
        {
            isValidated = true;
            EncryptPass encr = new EncryptPass();
            hdnfEpass.Value= encr.Encrypt(txtEmailPassword.Text);
        }
        else
        {
            isValidated = false;
            setFocusControl(txtEmailPassword);
            return;
        }
        if (txtMobile.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtMobile);
            return;
        }


        #endregion

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "system", "javascript:validation();", true);

    }
    #endregion

    #region DataStore
    private int DataStore(int flag)
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
            if (flag == 1)
            {
                cmd = new SqlCommand(Head_Insert, con, myTran);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(Detail_Insert, con, myTran);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(Detail_Insert_Report, con, myTran);
                cmd.ExecuteNonQuery();
                myTran.Commit();
                Thread.Sleep(100);

                count = 1;
            }
            else if (flag == 2)
            {
                if (Head_Update != "")
                {
                    cmd = new SqlCommand(Head_Update, con, myTran);
                    cmd.ExecuteNonQuery();
                }
                if (Detail_Update != "")
                {
                    cmd = new SqlCommand(Detail_Update, con, myTran);
                    cmd.ExecuteNonQuery();
                }
                if (Detail_Insert != "")
                {
                    cmd = new SqlCommand(Detail_Insert, con, myTran);
                    cmd.ExecuteNonQuery();
                }
                if (Detail_Delete != "")
                {
                    cmd = new SqlCommand(Detail_Delete, con, myTran);
                    cmd.ExecuteNonQuery();
                }

                if (Detail_Update_Report != "")
                {
                    cmd = new SqlCommand(Detail_Update_Report, con, myTran);
                    cmd.ExecuteNonQuery();
                }
                if (Detail_Insert_Report != "")
                {
                    cmd = new SqlCommand(Detail_Insert_Report, con, myTran);
                    cmd.ExecuteNonQuery();
                }
                myTran.Commit();
                Thread.Sleep(100);
                count = 2;
            }
            else
            {
                cmd = new SqlCommand(Detail_Delete, con, myTran);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(Head_Delete, con, myTran);
                cmd.ExecuteNonQuery();
                myTran.Commit();
                Thread.Sleep(100);
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
    #endregion
    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select count(User_Id) as User_Id from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'"));
            if (counts == 0)
            {
                txtUser_Id.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(User_Id) as User_Id from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'")) + 1;
                txtUser_Id.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(uid) as uid from " + tblHead + " "));
            if (counts == 0)
            {
                lbluid.Text = "1";

            }
            else
            {
                id = Convert.ToInt32(clsCommon.getString("SELECT max(uid) as uid from " + tblHead)) + 1;
                lbluid.Text = id.ToString();
            }
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

                btnOpenDetailsPopup.Enabled = false;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                grdDetail_Report.DataSource = null;
                grdDetail_Report.DataBind();
                ViewState["currentTable"] = null;
                ViewState["currentTable1"] = null;
                btnSave.Text = "Save";
                //btntxtUser_Id.Text = "Choose No";
                //btntxtUser_Id.Enabled = false;
                //txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtUser_Name.Enabled = false;
                txtPassword.Enabled = false;
                txtEmailId.Enabled = false;
                txtEmailPassword.Enabled = false;
                txtMobile.Enabled = false;
                drpUser_Type.Enabled = false;
                drpUserSecurity.Enabled = false;
                txtPassword.Text = string.Empty;
                txtEmailPassword.Text = string.Empty;
                txtPassword.Attributes["value"] = "";
                txtEmailPassword.Attributes["value"] = "";
                //drpUserAuthority.Enabled = false;
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
                //btntxtUser_Id.Text = "Change No";
                //btntxtUser_Id.Enabled = true;
                //txtEditDoc_No.Enabled = false;
                txtUser_Id.Enabled = false;
                txtuserfullname.Enabled = true;
                ViewState["currentTable"] = null;
                ViewState["currentTable1"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                grdDetail_Report.DataSource = null;
                grdDetail_Report.DataBind();
                //ViewState["currentTable"] = null;

                txtUser_Name.Enabled = true;
                txtPassword.Enabled = true;
                txtEmailId.Enabled = true;
                txtEmailPassword.Enabled = true;
                txtMobile.Enabled = true;
                drpUser_Type.Enabled = true;
                drpUserSecurity.Enabled = true;
                txtPassword.Text = string.Empty;
                txtEmailPassword.Text = string.Empty;
                txtPassword.Attributes["value"] = "";
                txtEmailPassword.Attributes["value"] = "";
                //drpUserAuthority.Enabled = false;
                #region set Business logic for save
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
                //btntxtUser_Id.Text = "Choose No";
                //btntxtUser_Id.Enabled = false;
                //txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtUser_Name.Enabled = false;
                txtPassword.Enabled = false;
                txtEmailId.Enabled = false;
                txtEmailPassword.Enabled = false;
                txtMobile.Enabled = false;
                drpUser_Type.Enabled = false;
                drpUserSecurity.Enabled = false;
                //drpUserAuthority.Enabled = false;

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
                //btntxtUser_Id.Text = "Choose No";
                //btntxtUser_Id.Enabled = true;
                //txtEditDoc_No.Enabled = false;
                lblMsg.Text = string.Empty;
                txtUser_Name.Enabled = true;
                txtPassword.Enabled = true;
                txtEmailId.Enabled = true;
                txtEmailPassword.Enabled = true;
                txtMobile.Enabled = true;
                drpUser_Type.Enabled = true;
                drpUserSecurity.Enabled = true;
                //drpUserAuthority.Enabled = true;
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
                btnEdit.Focus();
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

                        txtUser_Id.Text = dt.Rows[0]["User_Id"].ToString();
                        hdnfdoc.Value = txtUser_Id.Text;
                        lbluid.Text = dt.Rows[0]["uid"].ToString();
                        hdnfid.Value = lbluid.Text;
                        txtUser_Name.Text = dt.Rows[0]["User_Name"].ToString();

                        EncryptPass encr = new EncryptPass();
                        txtPassword.Text = encr.Decrypt(dt.Rows[0]["Password"].ToString());
                        txtPassword.Attributes["value"] = txtPassword.Text;
                        txtEmailId.Text = dt.Rows[0]["EmailId"].ToString();
                        txtEmailPassword.Text = encr.Decrypt(dt.Rows[0]["EmailPassword"].ToString());
                        txtEmailPassword.Attributes["value"] = txtEmailPassword.Text;
                        txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                        txtuserfullname.Text = dt.Rows[0]["userfullname"].ToString();

                        drpUser_Type.SelectedValue = dt.Rows[0]["User_Type"].ToString();

                        string User_Type = dt.Rows[0]["User_Type"].ToString();
                        if (User_Type == "U")
                        {
                            drpUser_Type.SelectedValue = "U";
                        }
                        else
                        {
                            drpUser_Type.SelectedValue = "A";
                        }

                        drpUserSecurity.SelectedValue = dt.Rows[0]["User_Security"].ToString();

                        string UserSecurity = dt.Rows[0]["User_Security"].ToString();
                        if (UserSecurity == "Y")
                        {
                            drpUserSecurity.SelectedValue = "Y";
                        }
                        else
                        {
                            drpUserSecurity.SelectedValue = "N";
                        }
                        recordExist = true;
                        lblMsg.Text = "";
                        #region Details
                        qry = "select Detail_Id,Program_Name,Tran_Type,Permission,'' as rowAction,'' as SrNo from " + tblDetails +
                            " where  uid=" + lbluid.Text;
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
                                    for (int i = 0; i < grdDetail.Rows.Count; i++)
                                    {
                                        CheckBox chkpermission = (CheckBox)grdDetail.Rows[i].Cells[3].FindControl("chkpermission");
                                        // string a = grdDetail.Rows[i].Cells[3].Text;
                                        string a = dt.Rows[i][3].ToString();
                                        if (a == "Y")
                                        {
                                            chkpermission.Text = "";
                                            chkpermission.Checked = true;

                                        }
                                        else
                                        {
                                            chkpermission.Text = "";
                                            chkpermission.Checked = false;
                                        }

                                    }

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
                    }
                    pnlgrdDetail.Enabled = false;

                    #region Details Report
                    qry = "select Detail_Id as Detail_Id_Report ,Program_Name as Program_Name_Report ,Tran_Type as Tran_Type_Report,Permission as Permission_report,'' as rowAction,'' as SrNo from " + tblDetailReport +
                        " where  uid=" + lbluid.Text;
                    //qry = "select Detail_Id  ,Program_Name  ,Tran_Type,Permission ,'' as rowAction,'' as SrNo from " + tblDetailReport + " where  User_Id=" + txtUser_Id.Text;
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
                                grdDetail_Report.DataSource = dt;
                                grdDetail_Report.DataBind();
                                for (int i = 0; i < grdDetail_Report.Rows.Count; i++)
                                {
                                    CheckBox chkpermission = (CheckBox)grdDetail_Report.Rows[i].Cells[3].FindControl("chkpermission");
                                    string a = dt.Rows[i][3].ToString();
                                    if (a == "Y")
                                    {
                                        chkpermission.Text = "";
                                        chkpermission.Checked = true;

                                    }
                                    else
                                    {
                                        chkpermission.Text = "";
                                        chkpermission.Checked = false;
                                    }

                                }

                                ViewState["currentTable1"] = dt;
                            }
                            else
                            {
                                grdDetail_Report.DataSource = null;
                                grdDetail_Report.DataBind();
                                ViewState["currentTable1"] = null;
                            }
                        }
                        else
                        {
                            grdDetail_Report.DataSource = null;
                            grdDetail_Report.DataBind();
                            ViewState["currentTable1"] = null;
                        }
                    }
                    else
                    {
                        grdDetail_Report.DataSource = null;
                        grdDetail_Report.DataBind();
                        ViewState["currentTable1"] = null;
                    }
                    #endregion
                    pnlgrdDetailReport.Enabled = false;


                }

            }
            hdnf.Value = txtUser_Id.Text;
            //this.enableDisableNavigateButtons();
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

            if (strTextBox == "txtUser_Id")
            {
                setFocusControl(txtUser_Id);
            }
            if (strTextBox == "txtUser_Name")
            {
                setFocusControl(txtUser_Name);
            }
            if (strTextBox == "txtPassword")
            {
                setFocusControl(txtPassword);
            }
            if (strTextBox == "txtEmailId")
            {
                setFocusControl(txtEmailId);
            }
            if (strTextBox == "txtEmailPassword")
            {
                setFocusControl(txtEmailPassword);
            }
            if (strTextBox == "txtMobile")
            {
                setFocusControl(txtMobile);
            }
            if (strTextBox == "drpUser_Type")
            {
                setFocusControl(drpUserSecurity);
            }
            if (strTextBox == "drpUserSecurity")
            {
                setFocusControl(drpUserSecurity);
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
            //string qryDisplay = " select * from " + qryCommonDetail + " ";
            string qryDisplay = " select * from " + tblHead + " where uid=" + hdnf.Value + " ";
            return qryDisplay;
        }
        catch
        {
            return "";
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
    #region [DeleteDetailsRow]
    private void DeleteDetailsRowResult(GridViewRow gridViewRow, string action)
    {
        try
        {
        }
        catch { }
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
                string IDExisting = clsCommon.getString("select Detail_Id from " + tblDetails + " where User_Id='" + hdnf.Value + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
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
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {

    }
    #endregion
    #region [showDetailsRowResult]
    private void showDetailsRowResult(GridViewRow gvrow)
    {

    }
    #endregion
    #region [RowCommandReport]
    protected void grdDetail_Report_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        if (grdDetail_Report.Rows[rowindex].Cells[RowactionResult].Text != "D" && grdDetail_Report.Rows[rowindex].Cells[RowactionResult].Text != "R")//add row action id
                        {
                            pnlPopupDetails.Style["display"] = "none";
                            this.showDetailsRowResult(grdDetail_Report.Rows[rowindex]);

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
                        this.DeleteDetailsRowResult(grdDetail_Report.Rows[rowindex], action);
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
            //e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
            //e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[Detail_Id].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[Program_Name].ControlStyle.Width = Unit.Percentage(30);
            e.Row.Cells[Tran_Type].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[Permission].ControlStyle.Width = Unit.Percentage(10);
            // e.Row.Cells[Pagetype].ControlStyle.Width = Unit.Percentage(20);
            e.Row.Cells[Rowaction].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[Srno].ControlStyle.Width = Unit.Percentage(10);

            //e.Row.Cells[0].Style["overflow"] = "hidden";
            //e.Row.Cells[1].Style["overflow"] = "hidden";
            e.Row.Cells[Detail_Id].Style["overflow"] = "hidden";
            e.Row.Cells[Program_Name].Style["overflow"] = "hidden";
            e.Row.Cells[Tran_Type].Style["overflow"] = "hidden";
            e.Row.Cells[Permission].Style["overflow"] = "hidden";
            //e.Row.Cells[Pagetype].Style["overflow"] = "hidden";
            e.Row.Cells[Rowaction].Style["overflow"] = "hidden";
            e.Row.Cells[Srno].Style["overflow"] = "hidden";

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                //e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[Detail_Id].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[Program_Name].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[Tran_Type].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[Permission].HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells[Pagetype].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[Rowaction].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[Srno].HorizontalAlign = HorizontalAlign.Center;

            }

            e.Row.Cells[Rowaction].Visible = true;
            e.Row.Cells[Srno].Visible = true;

        }
        catch
        {
        }
    }
    #endregion

    #region [grdDetail_Report_RowDataBound]
    protected void grdDetail_Report_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //e.Row.Cells[0].ControlStyle.Width = Unit.Percentage(10);
            //e.Row.Cells[1].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[Detail_Id_Report].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[Program_Name_Report].ControlStyle.Width = Unit.Percentage(30);
            e.Row.Cells[Tran_Type_Report].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[Permission_report].ControlStyle.Width = Unit.Percentage(10);
            // e.Row.Cells[Pagetype].ControlStyle.Width = Unit.Percentage(20);
            e.Row.Cells[RowactionResult].ControlStyle.Width = Unit.Percentage(10);
            e.Row.Cells[SrnoResult].ControlStyle.Width = Unit.Percentage(10);

            //e.Row.Cells[0].Style["overflow"] = "hidden";
            //e.Row.Cells[1].Style["overflow"] = "hidden";
            e.Row.Cells[Detail_Id_Report].Style["overflow"] = "hidden";
            e.Row.Cells[Program_Name_Report].Style["overflow"] = "hidden";
            e.Row.Cells[Tran_Type_Report].Style["overflow"] = "hidden";
            e.Row.Cells[Permission_report].Style["overflow"] = "hidden";
            //e.Row.Cells[Pagetype].Style["overflow"] = "hidden";
            e.Row.Cells[RowactionResult].Style["overflow"] = "hidden";
            e.Row.Cells[SrnoResult].Style["overflow"] = "hidden";

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                //e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[Detail_Id_Report].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[Program_Name_Report].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[Tran_Type_Report].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[Permission_report].HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells[Pagetype].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[RowactionResult].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[SrnoResult].HorizontalAlign = HorizontalAlign.Center;

            }

            e.Row.Cells[RowactionResult].Visible = true;
            e.Row.Cells[SrnoResult].Visible = true;

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

    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
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




    #region [imgBtnClose_Click]
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
    }
    #endregion

    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);

    }



    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
    }
    #endregion

    #region [drpUserSecurity_SelectedIndexChanged]
    protected void drpUserSecurity_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
}