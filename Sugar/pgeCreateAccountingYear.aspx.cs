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

public partial class Sugar_pgeCreateAccountingYear : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string qryCommon = string.Empty;
    string qryDisplay = string.Empty;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    int flag = 0;
    int count = 0;
    string cs = string.Empty;
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;

    string yearcode = string.Empty;
    string Start_Date = string.Empty;
    string End_Date = string.Empty;
    string year = string.Empty;
    int company_code = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        tblHead = "AccountingYear";
        user = Session["user"].ToString();

        cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        con = new SqlConnection(cs);
        if (!IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                clsButtonNavigation.enableDisable("N");
                ViewState["mode"] = "I";
                this.showLastRecord();
                txtFromDt.Enabled = false;
                txtToDt.Enabled = false;
                txtYearCode.Enabled = false;
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        //this.makeEmptyForm("A");
        this.getMaxCode();
        txtFromDt.Text = "";
        txtToDt.Text = "";
        txtYear.Text = "";
        txtFromDt.Enabled = true;
        txtToDt.Enabled = true;
        txtYearCode.Enabled = true;
        this.NextNumber();
    }


    #region [getMaxCode]
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
                obj.code = "yearCode";

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
                                    if (ds.Tables[0].Rows[0][0].ToString() != "1")
                                    {
                                        txtYearCode.Text = ds.Tables[0].Rows[0][0].ToString();
                                        txtYearCode.Enabled = false;
                                    }
                                    else
                                    {
                                        txtYearCode.Text = "";
                                    }
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string strRev = "";
            yearcode = txtYearCode.Text;
            Start_Date = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            End_Date = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            year = txtYear.Text;
            company_code = Convert.ToInt32(Session["Company_Code"].ToString());
            DataSet ds = new DataSet();
            if (btnSave.Text == "Save")
            {
                this.NextNumber();
                flag = 1;
                string Head_Fields = "yearCode,Start_Date,End_Date,year,Company_Code";
                string Head_Values = "'" + yearcode + "','" + Start_Date + "','" + End_Date + "','" + year + "','" + company_code + "'";

                qry = "insert into " + tblHead + " (" + Head_Fields + ") values (" + Head_Values + ")";

            }
            else
            {

                flag = 2;

                string Head_Update = "Start_Date='" + Start_Date + "',End_Date='" + End_Date + "',year='" + year + "',Company_Code='" + company_code + "' where Company_Code=" + company_code + " and yearCode=" + yearcode + "";
                qry = "update accountingyear set " + Head_Update + "";
            }

            Thread thred = new Thread(() => { count = DataStore(qry, flag); }); //Calling DataStore Method Using Thread
            thred.Start(); //Thread Operation Start
            thred.Join();


            if (count == 1)
            {
                clsButtonNavigation.enableDisable("S");
                this.enableDisableNavigateButtons();
                hdnf.Value = txtYearCode.Text;
                txtFromDt.Enabled = false;
                txtToDt.Enabled = false;
                qry = getDisplayQuery();
                this.fetchRecord(qry);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Added !')", true);
            }
            else if (count == 2)
            {
                clsButtonNavigation.enableDisable("S");
                this.enableDisableNavigateButtons();
                hdnf.Value = txtYearCode.Text;
                txtFromDt.Enabled = false;
                txtToDt.Enabled = false;
                qry = getDisplayQuery();
                this.fetchRecord(qry);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Updated !')", true);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        // this.makeEmptyForm("E");
        txtFromDt.Enabled = true;
        txtToDt.Enabled = true;
        txtYearCode.Enabled = false;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (hdconfirm.Value == "Yes")
        {
            qry = "delete from accountingyear where yearCode=" + txtYearCode.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "";
            Thread thred = new Thread(() => { count = DataStore(qry, flag); }); //Calling DataStore Method Using Thread
            thred.Start(); //Thread Operation Start
            thred.Join();
            if (count == 3)
            {
                clsButtonNavigation.enableDisable("N");
                this.enableDisableNavigateButtons();
                hdnf.Value = txtYearCode.Text;
                txtFromDt.Enabled = false;
                txtToDt.Enabled = false;
                string max = clsCommon.getString("select max(yearCode) as id from accountingyear where Company_Code=" + Session["Company_Code"].ToString() + "");
                hdnf.Value = max;
                qry = getDisplayQuery();
                this.fetchRecord(qry);

            }
        }

    }
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
        string str = clsCommon.getString("select count(yearCode) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

        if (str != "0")
        {
            clsButtonNavigation.enableDisable("S");
            this.enableDisableNavigateButtons();
            // this.makeEmptyForm("S");
            txtFromDt.Enabled = false;
            txtToDt.Enabled = false;
            txtYearCode.Enabled = false;
        }
        else
        {
            clsButtonNavigation.enableDisable("N");
            this.enableDisableNavigateButtons();
            //this.makeEmptyForm("N");

            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            txtYearCode.Enabled = false;
        }
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select yearCode from " + tblHead + " where yearCode=(select MIN(yearCode) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ") and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtYearCode.Text != string.Empty)
            {
                string query = "SELECT top 1 [yearCode] from " + tblHead + " where yearCode<" + Convert.ToInt32(hdnf.Value) +
                                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by yearCode desc";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
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
            if (txtYearCode.Text != string.Empty)
            {
                string query = "SELECT top 1 [yearCode] from " + tblHead + " where yearCode>" + Convert.ToInt32(hdnf.Value) +
                                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by yearCode asc";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
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
            string query = "";
            query = "select yearCode from " + tblHead + " where yearCode=(select MAX(yearCode) from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ") and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
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
                        txtYearCode.Text = dt.Rows[0]["yearCode"].ToString();
                        txtFromDt.Text = DateTime.Parse(dt.Rows[0]["Start_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                        txtToDt.Text = DateTime.Parse(dt.Rows[0]["End_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                        txtYear.Text = dt.Rows[0]["year"].ToString();
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
    protected string getDisplayQuery()
    {
        try
        {
            string qryDisplay = "select * from " + tblHead + " where yearCode=" + hdnf.Value + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
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
                txtYearCode.Text = hdnf.Value;
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
                // this.makeEmptyForm("S");
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

    #region [showLastRecord]
    private void showLastRecord()
    {
        try
        {
            string qry = string.Empty;
            qry = "select max(yearCode) as yearCode from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
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
                        hdnf.Value = dt.Rows[0]["yearCode"].ToString();
                        qry = getDisplayQuery();
                        bool recordExist = this.fetchRecord(qry);
                        if (recordExist == true)
                        {
                            btnAdd.Focus();
                        }
                        else                     //new code
                        {
                            btnEdit.Enabled = true;
                            btnDelete.Enabled = true;
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
    #endregion

    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        query = "select count(*) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
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

        if (txtYearCode.Text != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [yearCode] from " + tblHead + " where yearCode>" + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " ORDER BY yearCode asc  ";
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
                query = "SELECT top 1 [yearCode] from " + tblHead + " where yearCode<" + Convert.ToInt32(hdnf.Value) +
                    " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " ORDER BY yearCode asc  ";
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
    protected void txtFromDt_TextChanged(object sender, EventArgs e)
    {
        string substr = "";
        string date = txtFromDt.Text;
        int index = date.LastIndexOf('/');
        string year = date.Substring(index + 1, 4);
        string predate = txtYear.Text;
        if (!string.IsNullOrEmpty(predate))
        {
            int ix = predate.IndexOf('-');
            substr = predate.Substring(ix + 1, 4);
        }
        txtYear.Text = year + "-" + substr;
    }
    protected void txtToDt_TextChanged(object sender, EventArgs e)
    {
        string predate = txtYear.Text.ToString();
        string substr = predate.Substring(0, 5);
        string date = txtToDt.Text;
        int index = date.LastIndexOf('/');
        string year = date.Substring(index + 1, 4);
        txtYear.Text = substr + year;
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

    private void NextNumber()
    {
        try
        {
            int count = Convert.ToInt32(clsCommon.getString("select isnull(max(yearCode),0) as id from  accountingyear where Company_Code=" + Session["Company_Code"].ToString() + "")) + 1;
            if (count == 0)
            {
                yearcode = "1";
                txtYearCode.Text = "1";
            }
            else
            {
                yearcode = count.ToString();
                txtYearCode.Text = count.ToString();
            }
        }
        catch
        {
        }
    }
}