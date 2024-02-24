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

public partial class Sugar_Master_pgeEway_Bill : System.Web.UI.Page
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

    #region text and label data Declaration
    string clientid = string.Empty;
    string clientsecretkey = string.Empty;
    string tokenurl = string.Empty;
    string genewaybillurl = string.Empty;
    string username = string.Empty;
    string password = string.Empty;
    string gstin = string.Empty;
    string eenvoice_url = string.Empty;
    string Cancle_Envoice = string.Empty;
    string smsApi = string.Empty;
    string sender_id = string.Empty;
    string accusage = string.Empty;
    string ModeofPayment = string.Empty;
    string AccountDetails = string.Empty;
    string Branch = string.Empty;
    #endregion

    #region Head part Declaration
    string Created_By = string.Empty;
    string Modified_By = string.Empty;
    string Created_Date = string.Empty;
    string Modified_Date = string.Empty;
    string retValue = string.Empty;
    string strRev = string.Empty;

    int Company_Code = 0;
    string Head_Update = string.Empty;
    string Head_Insert = string.Empty;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "eway_bill";
            user = Session["user"].ToString();
            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    string qry = "Select * from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
                    DataSet ds = new DataSet();
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        txtclientid.Text = ds.Tables[0].Rows[0]["E_UserName"].ToString();
                        txtclientsecretkey.Text = ds.Tables[0].Rows[0]["E_UserPassword"].ToString();
                        txttokenurl.Text = ds.Tables[0].Rows[0]["E_UrlAddress_Token"].ToString();
                        txtgenewaybillurl.Text = ds.Tables[0].Rows[0]["E_UrlAddress_Bill"].ToString();
                        txtusername.Text = ds.Tables[0].Rows[0]["E_UserName_Gov"].ToString();
                        txtpassword.Text = ds.Tables[0].Rows[0]["E_UserPassword_Gov"].ToString();
                        txtgstin.Text = ds.Tables[0].Rows[0]["E_Company_GSTno"].ToString();

                        txte_envoice.Text = ds.Tables[0].Rows[0]["E_Envoice"].ToString();
                        txtCancle_Envoice.Text = ds.Tables[0].Rows[0]["EInvoiceCancle"].ToString();
                        txtsmsApi.Text = ds.Tables[0].Rows[0]["smsApi"].ToString();
                        txtSenderid.Text = ds.Tables[0].Rows[0]["Sender_id"].ToString();
                        txtAccusage.Text = ds.Tables[0].Rows[0]["Accusage"].ToString();

                        txtMode_of_Payment.Text = ds.Tables[0].Rows[0]["Mode_of_Payment"].ToString();
                        txtAccount_Details.Text = ds.Tables[0].Rows[0]["Account_Details"].ToString();
                        txtBranch.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                    }

                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
            }
            setFocusControl(txtclientid);
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
            clientid = txtclientid.Text;
            clientsecretkey = txtclientsecretkey.Text;
            tokenurl = txttokenurl.Text;
            genewaybillurl = txtgenewaybillurl.Text;
            username = txtusername.Text;
            password = txtpassword.Text;
            gstin = txtgstin.Text;
            eenvoice_url = txte_envoice.Text;
            Cancle_Envoice=txtCancle_Envoice.Text;
            smsApi = txtsmsApi.Text;
            sender_id = txtSenderid.Text;
            accusage = txtAccusage.Text;

            ModeofPayment = txtMode_of_Payment.Text;
            AccountDetails = txtAccount_Details.Text;
            Branch = txtBranch.Text;

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

            string s = clsCommon.getString("select * from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString());
            if (s == null || s == string.Empty || s == "0")
            {
                Head_Fields = Head_Fields + "E_UserName,";
                Head_Values = Head_Values + "'" + clientid + "',";
                Head_Fields = Head_Fields + "E_UserPassword,";
                Head_Values = Head_Values + "'" + clientsecretkey + "',";
                Head_Fields = Head_Fields + "E_UrlAddress_Token,";
                Head_Values = Head_Values + "'" + tokenurl + "',";
                Head_Fields = Head_Fields + "E_UrlAddress_Bill,";
                Head_Values = Head_Values + "'" + genewaybillurl + "',";
                Head_Fields = Head_Fields + "E_UserName_Gov,";
                Head_Values = Head_Values + "'" + username + "',";
                Head_Fields = Head_Fields + "E_UserPassword_Gov,";
                Head_Values = Head_Values + "'" + password + "',";
                Head_Fields = Head_Fields + "E_Company_GSTno,";
                Head_Values = Head_Values + "'" + gstin + "',";
                Head_Fields = Head_Fields + "Company_Code,";
                Head_Values = Head_Values + "'" + Company_Code + "',";

                Head_Fields = Head_Fields + "E_Envoice,";
                Head_Values = Head_Values + "'" + eenvoice_url + "',";
                Head_Fields = Head_Fields + "smsApi,";
                Head_Values = Head_Values + "'" + smsApi + "',";
                Head_Fields = Head_Fields + "Sender_id,";
                Head_Values = Head_Values + "'" + sender_id + "',";
                Head_Fields = Head_Fields + "Accusage,";
                Head_Values = Head_Values + "'" + accusage + "',";

                Head_Fields = Head_Fields + "Mode_of_Payment,";
                Head_Values = Head_Values + "'" + ModeofPayment + "',";
                Head_Fields = Head_Fields + "Account_Details,";
                Head_Values = Head_Values + "'" + AccountDetails + "',";
                Head_Fields = Head_Fields + "Branch,";
                Head_Values = Head_Values + "'" + Branch + "',";
                Head_Fields = Head_Fields + "EInvoiceCancle";
                Head_Values = Head_Values + "'" + Cancle_Envoice + "'";

                flag = 1;
                qry = "insert into " + tblHead + "(" + Head_Fields + ") values(" + Head_Values + ")";


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
                    Head_Update = Head_Update + "E_UserName=";
                    Head_Update = Head_Update + "'" + clientid + "',";
                    Head_Update = Head_Update + "E_UserPassword=";
                    Head_Update = Head_Update + "'" + clientsecretkey + "',";
                    Head_Update = Head_Update + "E_UrlAddress_Token=";
                    Head_Update = Head_Update + "'" + tokenurl + "',";
                    Head_Update = Head_Update + "E_UrlAddress_Bill=";
                    Head_Update = Head_Update + "'" + genewaybillurl + "',";
                    Head_Update = Head_Update + "E_UserName_Gov=";
                    Head_Update = Head_Update + "'" + username + "',";
                    Head_Update = Head_Update + "E_UserPassword_Gov=";
                    Head_Update = Head_Update + "'" + password + "',";
                    Head_Update = Head_Update + "E_Company_GSTno=";
                    Head_Update = Head_Update + "'" + gstin + "',";

                    Head_Update = Head_Update + "E_Envoice=";
                    Head_Update = Head_Update + "'" + eenvoice_url + "',";
                    Head_Update = Head_Update + "smsApi=";
                    Head_Update = Head_Update + "'" + smsApi + "',";
                    Head_Update = Head_Update + "Sender_id=";
                    Head_Update = Head_Update + "'" + sender_id + "',";
                    Head_Update = Head_Update + "Accusage=";
                    Head_Update = Head_Update + "'" + accusage + "',";

                    Head_Update = Head_Update + "Mode_of_Payment=";
                    Head_Update = Head_Update + "'" + ModeofPayment + "',";
                    Head_Update = Head_Update + "Account_Details=";
                    Head_Update = Head_Update + "'" + AccountDetails + "',";
                    Head_Update = Head_Update + "Branch=";
                    Head_Update = Head_Update + "'" + Branch + "',";
                    Head_Update = Head_Update + "EInvoiceCancle=";
                    Head_Update = Head_Update + "'" + Cancle_Envoice + "'";


                    #endregion
                }
                flag = 2;
                qry = "update " + tblHead + " set " + Head_Update + " where Company_Code='" + Company_Code + "' ";
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

                        txtclientid.Text = ds.Tables[0].Rows[0]["E_UserName"].ToString();
                        txtclientsecretkey.Text = ds.Tables[0].Rows[0]["E_UserPassword"].ToString();
                        txttokenurl.Text = ds.Tables[0].Rows[0]["E_UrlAddress_Token"].ToString();
                        txtgenewaybillurl.Text = ds.Tables[0].Rows[0]["E_UrlAddress_Bill"].ToString();
                        txtusername.Text = ds.Tables[0].Rows[0]["E_UserName_Gov"].ToString();
                        txtpassword.Text = ds.Tables[0].Rows[0]["E_UserPassword_Gov"].ToString();
                        txtgstin.Text = ds.Tables[0].Rows[0]["E_Company_GSTno"].ToString();
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