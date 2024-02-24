using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Configuration;
public partial class Sugar_Master_pgeWhatsAppURL : System.Web.UI.Page
{
    #region data section
    string qry = string.Empty;
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    string cs = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string qryAccountList = string.Empty;
    int flag = 0;
    int count = 0;
    int counts = 0;
    static WebControl objAsp = null;
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    #endregion


    #region Head part Declaration
    string Created_By = string.Empty;
    string Modified_By = string.Empty;
    string Created_Date = string.Empty;
    string Modified_Date = string.Empty;
    string retValue = string.Empty;
    string strRev = string.Empty;

    //int Company_Code = 0;
    string Head_Update = string.Empty;
    string Head_Insert = string.Empty;
    #endregion

    #region text and label data Declaration
    string Instance_Id = string.Empty;
    string Access_token = string.Empty;
    string Mobile_NoWa = string.Empty;
    string OtpEmail = string.Empty;
    string OtpPassword = string.Empty;
    string gitAuthToken = string.Empty;
    string gitRepo = string.Empty;
    string gitauthKey = string.Empty;
    string WaTitle = string.Empty;
    string Mobile_No = string.Empty;
    
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "tblWhatsAppURL";
            user = Session["user"].ToString();
            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    string qry = "Select * from tblWhatsAppURL where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
                    DataSet ds = new DataSet();
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        txtInstance_Id.Text = ds.Tables[0].Rows[0]["Instance_Id"].ToString();
                        txtAccess_token.Text = ds.Tables[0].Rows[0]["Access_token"].ToString();
                        txtMobile_NoWa.Text = ds.Tables[0].Rows[0]["Mobile_NoWa"].ToString();
                        txtOtpEmail.Text = ds.Tables[0].Rows[0]["OtpEmail"].ToString();
                        txtOtpPassword.Text = ds.Tables[0].Rows[0]["OtpPassword"].ToString();
                        txtAuthToken.Text = ds.Tables[0].Rows[0]["gitAuthToken"].ToString();
                        txtrepo.Text = ds.Tables[0].Rows[0]["gitRepo"].ToString();
                        txtgitauthKey.Text = ds.Tables[0].Rows[0]["gitauthKey"].ToString();
                        txtTitle.Text = ds.Tables[0].Rows[0]["WaTitle"].ToString();
                        txtMobNO.Text = ds.Tables[0].Rows[0]["Mobile_No"].ToString();
                    }

                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
            }
            setFocusControl(txtInstance_Id);
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();

            #region -Head part declearations
            Instance_Id = txtInstance_Id.Text;
            Access_token = txtAccess_token.Text;
            Mobile_NoWa = txtMobile_NoWa.Text;
            OtpEmail = txtOtpEmail.Text;
            OtpPassword = txtOtpPassword.Text;
            gitAuthToken = txtAuthToken.Text;
            gitRepo = txtrepo.Text;
            gitauthKey = txtgitauthKey.Text;
            WaTitle = txtTitle.Text;
            Mobile_No = txtMobNO.Text;

            string retValue = string.Empty;
            string strRev = string.Empty;
            int Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            string userinfo = clsGV.userInfo + DateTime.Now.ToString("dd/MM/yyyy:HHmmss");
            string Head_Fields = string.Empty;
            string Head_Values = string.Empty;
            int flag = 0;
            string op = string.Empty;
            string returnmaxno = string.Empty;
            #endregion

            string s = clsCommon.getString("select * from tblWhatsAppURL where Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString());
           if (s == null || s == string.Empty || s == "0")
            {
               
                Head_Fields = Head_Fields + "Instance_Id,";
                Head_Values = Head_Values + "'" + Instance_Id + "',";
                Head_Fields = Head_Fields + "Access_token,";
                Head_Values = Head_Values + "'" + Access_token + "',";
                //Head_Fields = Head_Fields + "Drive_URL";
                //Head_Values = Head_Values + "'" + Drive_URL + "',";
                Head_Fields = Head_Fields + "Company_Code,";
                Head_Values = Head_Values + "'" + Company_Code + "',";
                Head_Fields = Head_Fields + "Mobile_NoWa,";
                Head_Values = Head_Values + "'" + Mobile_NoWa + "',";
                Head_Fields = Head_Fields + "OtpEmail,";
                Head_Values = Head_Values + "'" + OtpEmail + "',";
                Head_Fields = Head_Fields + "OtpPassword,";
                Head_Values = Head_Values + "'" + OtpPassword + "',";
                Head_Fields = Head_Fields + "gitAuthToken,";
                Head_Values = Head_Values + "'" + gitAuthToken + "',";
                Head_Fields = Head_Fields + "gitRepo,";
                Head_Values = Head_Values + "'" + gitRepo + "',";
                Head_Fields = Head_Fields + "gitauthKey,";
                Head_Values = Head_Values + "'" + gitauthKey + "',";
                Head_Fields = Head_Fields + "WaTitle,";
                Head_Values = Head_Values + "'" + WaTitle + "',";
                Head_Fields = Head_Fields + "Mobile_No";
                Head_Values = Head_Values + "'" + Mobile_No + "'";
                flag = 1;
                
                qry = "DELETE FROM " + tblHead + " where Company_Code='" + Company_Code + "' insert into " + tblHead + "(" + Head_Fields + ") values(" + Head_Values + ")";


                Thread thred = new Thread(() => { count = DataStore(qry, flag); }); //Calling DataStore Method Using Thread
                thred.Start(); //Thread Operation Start
                thred.Join();

                if (count == 1)
                {
                    qry = getDisplayQuery();
                    this.fetchRecord(qry);
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert(' Record Successfully Saved!')", true);
            }
            else
            {
                string s1 = clsCommon.getString("select * from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (s1 != string.Empty)
                {
                    #region Create Update Query
                    Head_Update = Head_Update + "Instance_Id=";
                    Head_Update = Head_Update + "'" + Instance_Id + "',";
                    Head_Update = Head_Update + "Access_token=";
                    Head_Update = Head_Update + "'" + Access_token + "',";
                    Head_Update = Head_Update + "Mobile_NoWa=";
                    Head_Update = Head_Update + "'" + Mobile_NoWa + "',";
                    Head_Update = Head_Update + "OtpEmail=";
                    Head_Update = Head_Update + "'" + OtpEmail + "',";
                    Head_Update = Head_Update + "OtpPassword=";
                    Head_Update = Head_Update + "'" + OtpPassword + "',";
                    Head_Update = Head_Update + "gitAuthToken=";
                    Head_Update = Head_Update + "'" + gitAuthToken + "',"; 
                    Head_Update = Head_Update + "gitRepo=";
                    Head_Update = Head_Update + "'" + gitRepo + "',";
                    Head_Update = Head_Update + "gitauthKey=";
                    Head_Update = Head_Update + "'" + gitauthKey + "',";
                    Head_Update = Head_Update + "WaTitle=";
                    Head_Update = Head_Update + "'" + WaTitle + "',";
                    Head_Update = Head_Update + "Mobile_No=";
                    Head_Update = Head_Update + "'" + Mobile_No + "'";
                    //Head_Update = Head_Update + "Drive_URL=";
                    //Head_Update = Head_Update + "'" + Drive_URL + "'";
                    #endregion
                }
                flag = 2;
                qry = "update " + tblHead + " set " + Head_Update + " where Company_Code='" + Company_Code + "'";
                Thread thred = new Thread(() => { count = DataStore(qry, flag); });
                thred.Start();
                thred.Join();
                if (count == 2)
                {
                    qry = getDisplayQuery();
                    this.fetchRecord(qry);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Update !')", true);
                }
            }
        }

        catch (Exception)
        {
            throw;
        }
    }

    #region DataStore
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
            myTran = con.BeginTransaction();

           if (flag == 1)
            { 
                cmd = new SqlCommand(Query, con, myTran);
                cmd.ExecuteNonQuery();
                myTran.Commit();
                Thread.Sleep(100);

                count = 1;
            }
            else if (flag == 2)
            {
                if (Query != "")
                {
                    cmd = new SqlCommand(Query, con, myTran);
                    cmd.ExecuteNonQuery();
                }
                myTran.Commit();
                Thread.Sleep(100);
                count = 2;
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

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = "select * from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
            return qryDisplay;
        }
        catch
        {
            return "";
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

                        txtInstance_Id.Text = ds.Tables[0].Rows[0]["Instance_Id"].ToString();
                        txtAccess_token.Text = ds.Tables[0].Rows[0]["Access_token"].ToString();
                        //txtDrive_URL.Text = ds.Tables[0].Rows[0]["Drive_URL"].ToString();
                        
                    }
                }

            }
            recordExist = true;
            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion



}

