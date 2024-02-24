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

public partial class Sugar_Utility_PgePostDate : System.Web.UI.Page
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
    string postdate = string.Empty;
    string inworddate = string.Empty;
    string outworddate = string.Empty;
    #endregion

    #region Head part Declaration
    string Created_By = string.Empty;
    string Modified_By = string.Empty;
    string Created_Date = string.Empty;
    string Modified_Date = string.Empty;
    string retValue = string.Empty;
    string strRev = string.Empty;

    int Company_Code = 0;
    int Year_Code = 0;
    int year = 0;
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
            tblHead = "post_date";
            user = Session["user"].ToString();
            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    string qry = "Select * from qrypostdate where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
                    DataSet ds = new DataSet();
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        txtDoc_Date.Text = ds.Tables[0].Rows[0]["postdateconversion"].ToString();
                        txtInwordDate.Text = ds.Tables[0].Rows[0]["inworddateconversion"].ToString();
                        txtOutWord.Text = ds.Tables[0].Rows[0]["outworddateconversion"].ToString();
                        //Tables[0].Rows[0]["Outword_Date"].ToString();

                    }

                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
            }
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

                        //txtDoc_Date.Text =dt.Rows[0]["Post_Date"].ToString();
                        //txtInwordDate.Text = dt.Rows[0]["Inword_Date"].ToString();
                        //txtOutWord.Text = dt.Rows[0]["Outword_Date"].ToString();


                        txtDoc_Date.Text = Convert.ToDateTime(dt.Rows[0]["Post_Date"]).ToString("dd/MM/yyyy");
                        txtInwordDate.Text = Convert.ToDateTime(dt.Rows[0]["Inword_Date"]).ToString("dd/MM/yyyy");
                        txtOutWord.Text = Convert.ToDateTime(dt.Rows[0]["Outword_Date"]).ToString("dd/MM/yyyy");
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

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = "select * from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["Year"].ToString()) + "";
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();

            #region -Head part declearations
            postdate = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            // DateTime inworddate1 = Convert.ToDateTime(txtInwordDate.Text);
            inworddate = DateTime.Parse(txtInwordDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            //  DateTime outworddate1 = Convert.ToDateTime(txtOutWord.Text);
            outworddate = DateTime.Parse(txtOutWord.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");


            string retValue = string.Empty;
            string strRev = string.Empty;
            int Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            int Year_Code = Convert.ToInt32(Session["year"].ToString());
            string userinfo = clsGV.userInfo + DateTime.Now.ToString("dd/MM/yyyy:HHmmss");
            string Head_Fields = string.Empty;
            string Head_Values = string.Empty;
            int flag = 0;
            string op = string.Empty;
            string returnmaxno = string.Empty;
            #endregion

            string s = clsCommon.getString("select Year_Code from post_date where Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString() +
                " and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'");
            if (s == null || s == "0")
            {
                Head_Fields = Head_Fields + "Post_Date,";
                Head_Values = Head_Values + "'" + postdate + "',";
                Head_Fields = Head_Fields + "Inword_Date,";
                Head_Values = Head_Values + "'" + inworddate + "',";
                Head_Fields = Head_Fields + "Outword_Date,";
                Head_Values = Head_Values + "'" + outworddate + "',";
                Head_Fields = Head_Fields + "Company_Code,";
                Head_Values = Head_Values + "'" + Company_Code + "',";
                Head_Fields = Head_Fields + "Year_Code";
                Head_Values = Head_Values + "'" + Year_Code + "'";

                flag = 1;
                qry = "insert into " + tblHead + "(" + Head_Fields + ") values(" + Head_Values + ")";


                Thread thred = new Thread(() => { count = DataStore(qry, flag); }); //Calling DataStore Method Using Thread
                thred.Start(); //Thread Operation Start
                thred.Join();

                if (count == 1)
                {
                    Session["Post_Date"] = txtDoc_Date.Text;
                    Session["Outword_Date"] = txtOutWord.Text;
                    Session["Inword_Date"] = txtInwordDate.Text;
                    qry = getDisplayQuery();
                    this.fetchRecord(qry);
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert(' Record Successfully Saved!')", true);
            }
            else
            {
                string s1 = clsCommon.getString("select * from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'");
                if (s1 != string.Empty)
                {
                    #region Create Update Query
                    Head_Update = Head_Update + "Post_Date=";
                    Head_Update = Head_Update + "'" + postdate + "',";
                    Head_Update = Head_Update + "Inword_Date=";
                    Head_Update = Head_Update + "'" + inworddate + "',";
                    Head_Update = Head_Update + "Outword_Date=";
                    Head_Update = Head_Update + "'" + outworddate + "'";
                    //Head_Update = Head_Update + "Company_Code=";
                    //Head_Update = Head_Update + "'" + Company_Code + "',";
                    //Head_Update = Head_Update + "Year_Code=";
                    //Head_Update = Head_Update + "'" + Year_Code + "'";
                    #endregion
                }
                flag = 2;
                qry = "update " + tblHead + " set " + Head_Update + " where Company_Code='" + Company_Code + "' and Year_Code='" + Year_Code + "' ";
                Thread thred = new Thread(() => { count = DataStore(qry, flag); });
                thred.Start();
                thred.Join();
                if (count == 2)
                {
                    Session["Post_Date"] = txtDoc_Date.Text;
                    Session["Outword_Date"] = txtOutWord.Text;
                    Session["Inword_Date"] = txtInwordDate.Text;
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
}