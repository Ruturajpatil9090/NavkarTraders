using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;

public partial class Sugar_Master_pgeSharesCompanyParameter : System.Web.UI.Page
{
    #region data section
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    //string AccountMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    int defaultAccountCode = 0;
    string qryAccountList = string.Empty;
    static WebControl objAsp = null;
    string autovoucher = string.Empty;
    string user = string.Empty;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string cs = string.Empty;
    int flag = 0;
    int count = 0;
    int counts = 0;
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    #endregion

    #region text and label data Declaration
    Int32 EquitySale_ac = 0;
    Int32 EquityPurchase_ac = 0;
    Int32 EquityExpenses_ac = 0;
    Int32 FNOSale_ac = 0;
    Int32 FNOPurchase_ac = 0;
    Int32 FNOExpenses_ac = 0;
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
    string Head_Update = string.Empty;
    string Head_Insert = string.Empty;
    string Head_Delete = string.Empty;
    string Head_Values = string.Empty;
    string Head_Fields = string.Empty;
    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "SharesCompanyParameter";
            tblDetails = tblPrefix + "";
            qryCommon = "qrymstaccountmaster";
            qryAccountList = tblPrefix + "qryAccountsList";
            pnlPopup.Style["display"] = "none";
            user = Session["user"].ToString();
            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    pnlPopup.Style["display"] = "none";
                    ViewState["currentTable"] = null;
                    //clsButtonNavigation.enableDisable("N");
                    // this.makeEmptyForm("N");
                    ViewState["mode"] = "I";
                    this.showLastRecord();
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
            setFocusControl(txtEquitySale_ac);
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
                btnUpdate.Text = "Save";
                lblMsg.Text = string.Empty;
                #region set Business logic for save

                btntxtEquitySale_ac.Enabled = false;
                btntxtEquityPurchase_ac.Enabled = false;
                btntxtEquityExpenses_ac.Enabled = false;
                btntxtFNOSale_ac.Enabled = false;
                btntxtFNOPurchase_ac.Enabled = false;
                btntxtFNOExpenses_ac.Enabled = false;
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
                btnUpdate.Text = "Save";

                #region set Business logic for save

                btntxtEquitySale_ac.Enabled = true;
                btntxtEquityPurchase_ac.Enabled = true;
                btntxtEquityExpenses_ac.Enabled = true;
                btntxtFNOSale_ac.Enabled = true;
                btntxtFNOPurchase_ac.Enabled = true;
                btntxtFNOExpenses_ac.Enabled = true;
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
                #region set Business logic for save

                btntxtEquitySale_ac.Enabled = false;
                btntxtEquityPurchase_ac.Enabled = false;
                btntxtEquityExpenses_ac.Enabled = false;
                btntxtFNOSale_ac.Enabled = false;
                btntxtFNOPurchase_ac.Enabled = false;
                btntxtFNOExpenses_ac.Enabled = false;
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

                lblMsg.Text = string.Empty;
                #region set Business logic for save

                btntxtEquitySale_ac.Enabled = true;
                btntxtEquityPurchase_ac.Enabled = true;
                btntxtEquityExpenses_ac.Enabled = true;
                btntxtFNOSale_ac.Enabled = true;
                btntxtFNOPurchase_ac.Enabled = true;
                btntxtFNOExpenses_ac.Enabled = true;
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
            string qry = this.getDisplayQuery();
            this.fetchRecord(qry);
            btnUpdate.Enabled = true;
        }

        catch
        {

        }
    }
    #endregion

    #region [btnUpdate_Click]
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        try
        {
            bool isvalidate = true;
            string qry = "";
            //#region validation
            //if (txtFNOExpenses_ac.Text != string.Empty && txtFNOExpenses_ac.Text != "0")
            //{
            //    isvalidate = true;
            //}
            //else
            //{
            //    txtFNOExpenses_ac.Text = string.Empty;
            //    setFocusControl(txtFNOExpenses_ac);
            //    return;
            //}

            //#endregion

            #region -Head part declearation

            EquitySale_ac = txtEquitySale_ac.Text != string.Empty ? Convert.ToInt32(txtEquitySale_ac.Text) : 0;
            EquityPurchase_ac = txtEquityPurchase_ac.Text != string.Empty ? Convert.ToInt32(txtEquityPurchase_ac.Text) : 0;
            EquityExpenses_ac = txtEquityExpenses_ac.Text != string.Empty ? Convert.ToInt32(txtEquityExpenses_ac.Text) : 0;
            FNOSale_ac = txtFNOSale_ac.Text != string.Empty ? Convert.ToInt32(txtFNOSale_ac.Text) : 0;
            FNOPurchase_ac = txtFNOPurchase_ac.Text != string.Empty ? Convert.ToInt32(txtFNOPurchase_ac.Text) : 0;
            FNOExpenses_ac = txtFNOExpenses_ac.Text != string.Empty ? Convert.ToInt32(txtFNOExpenses_ac.Text) : 0;

            string retValue = string.Empty;
            string strRev = string.Empty;
            int Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
            int Year_Code = Convert.ToInt32(Session["year"].ToString());
            //int year_Code = Convert.ToInt32(Session["year"].ToString());
            //int Branch_Code = Convert.ToInt32(Session["Branch_Code"].ToString());
            string userinfo = clsGV.userInfo + DateTime.Now.ToString("dd/MM/yyyy:HHmmss");
            string Created_By = Session["user"].ToString();
            string Modified_By = Session["user"].ToString();
            string Head_Fields = string.Empty;
            string Head_Values = string.Empty;
            string Head_Update = string.Empty;
            int flag = 0;
            string op = string.Empty;
            string returnmaxno = string.Empty;
            #endregion-End of Head part declearation
            string checkyear = clsCommon.getString("select Year_Code from SharesCompanyParameter where Year_Code=" + Convert.ToInt32(Session["year"]).ToString());
            if (checkyear == null || checkyear == string.Empty || checkyear == "0")
            {
                Head_Fields = Head_Fields + "EquitySale,";
                Head_Values = Head_Values + "'" + EquitySale_ac + "',";
                Head_Fields = Head_Fields + "EquityPurchase,";
                Head_Values = Head_Values + "'" + EquityPurchase_ac + "',";
                Head_Fields = Head_Fields + "EquityExpenses,";
                Head_Values = Head_Values + "'" + EquityExpenses_ac + "',";
                Head_Fields = Head_Fields + "FNOSale,";
                Head_Values = Head_Values + "'" + FNOSale_ac + "',";
                Head_Fields = Head_Fields + "FNOPurchase,";
                Head_Values = Head_Values + "'" + FNOPurchase_ac + "',";
                Head_Fields = Head_Fields + "FNOExpenses,";
                Head_Values = Head_Values + "'" + FNOExpenses_ac + "',";
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
                    hdnf.Value = counts.ToString();
                    //clsButtonNavigation.enableDisable("S");
                    //this.makeEmptyForm("S");
                    qry = getDisplayQuery();
                    this.fetchRecord(qry);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Added !')", true);
                }
            }
            else
            {
                string s = clsCommon.getString("select Year_Code from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                if (s != string.Empty)
                {
                    #region Create Update Query
                    Head_Update = Head_Update + "EquitySale=";
                    Head_Update = Head_Update + "'" + EquitySale_ac + "',";
                    Head_Update = Head_Update + "EquityPurchase=";
                    Head_Update = Head_Update + "'" + EquityPurchase_ac + "',";
                    Head_Update = Head_Update + "EquityExpenses=";
                    Head_Update = Head_Update + "'" + EquityExpenses_ac + "',";
                    Head_Update = Head_Update + "FNOSale=";
                    Head_Update = Head_Update + "'" + FNOSale_ac + "',";
                    Head_Update = Head_Update + "FNOPurchase=";
                    Head_Update = Head_Update + "'" + FNOPurchase_ac + "',";
                    Head_Update = Head_Update + "FNOExpenses=";
                    Head_Update = Head_Update + "'" + FNOExpenses_ac + "'";

                    #endregion
                }
                flag = 2;
                qry = "update " + tblHead + " set " + Head_Update + " where Company_Code='" + Company_Code + "' and Year_Code='" + Year_Code + "'";
                Thread thred = new Thread(() => { count = DataStore(qry, flag); });
                thred.Start();
                thred.Join();
                if (count == 2)
                {
                    //hdnf.Value = lblbsid.Text;
                    //clsButtonNavigation.enableDisable("S");
                    //this.makeEmptyForm("S");
                    qry = getDisplayQuery();
                    this.fetchRecord(qry);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Update !')", true);
                }


            }

        }
        catch
        {

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
            ///Execution
            myTran = con.BeginTransaction();
            //cmd.CommandText = qry;
            //cmd.Connection = con;
            //cmd.Transaction = myTran;
            if (flag == 1)
            {
                cmd = new SqlCommand(Query, con, myTran);
                cmd.ExecuteNonQuery();
                //cmd = new SqlCommand(Detail_Insert, con, myTran);
                //cmd.ExecuteNonQuery();
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

                        txtEquitySale_ac.Text = dt.Rows[0]["EquitySale"].ToString();
                        lblEquitySale_name.Text = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code=" + txtEquitySale_ac.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        txtEquityPurchase_ac.Text = dt.Rows[0]["EquityPurchase"].ToString();
                        lblEquityPurchase_name.Text = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code=" + txtEquityPurchase_ac.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        txtEquityExpenses_ac.Text = dt.Rows[0]["EquityExpenses"].ToString();
                        lblEquityExpenses_name.Text = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code=" + txtEquityExpenses_ac.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        txtFNOSale_ac.Text = dt.Rows[0]["FNOSale"].ToString();
                        lblFNOSale_name.Text = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code=" + txtFNOSale_ac.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        txtFNOPurchase_ac.Text = dt.Rows[0]["FNOPurchase"].ToString();
                        lblFNOPurchase_name.Text = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code=" + txtFNOPurchase_ac.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        txtFNOExpenses_ac.Text = dt.Rows[0]["FNOExpenses"].ToString();
                        lblFNOExpenses_name.Text = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code=" + txtFNOExpenses_ac.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

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

                        foreach (System.Web.UI.Control c in pnlMain.Controls)
                        {
                            if (c is System.Web.UI.WebControls.TextBox)
                            {
                                string name = c.ClientID;
                                string newStr = name.Remove(0, 20);
                                newStr = newStr + ".Text";
                                string textname = ((System.Web.UI.WebControls.TextBox)c).Text;
                                string acname = clsCommon.getString("select Ac_Name_E from nt_1_accountmaster where Ac_Code=" + textname + "");
                                if (newStr == "txtEquitySale_ac")
                                {
                                    lblEquitySale_name.Text = acname;
                                }
                                if (newStr == "txtEquityPurchase_ac")
                                {
                                    lblEquityPurchase_name.Text = acname;
                                }
                                if (newStr == "txtEquityExpenses_ac")
                                {
                                    lblEquityExpenses_name.Text = acname;
                                }
                                if (newStr == "txtFNOSale_ac")
                                {
                                    lblFNOSale_name.Text = acname;
                                }
                                if (newStr == "txtFNOPurchase_ac")
                                {
                                    lblFNOPurchase_name.Text = acname;
                                }
                                if (newStr == "txtFNOExpenses_ac")
                                {
                                    lblFNOExpenses_name.Text = acname;
                                }

                            }
                        }
                        SharesParameterId();


                        recordExist = true;
                        lblMsg.Text = "";
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

    private void SharesParameterId()
    {
        try
        {
            Session["EquitySale_ac"] = txtEquitySale_ac.Text;
            Session["EquitySale_acid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtEquitySale_ac.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "  ");
            Session["EquityPurchase_ac"] = txtEquityPurchase_ac.Text;
            Session["EquityPurchase_acid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtEquityPurchase_ac.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "  ");
            Session["EquityExpenses_ac"] = txtEquityExpenses_ac.Text;
            Session["EquityExpenses_acid"] = clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtEquityExpenses_ac.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "  ");
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
            string qryDisplay = "select * from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (v != "txtFNOExpenses_ac")
            {
                e.Row.Cells[0].Width = new Unit("60px");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].Width = new Unit("80px");
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].Width = new Unit("80px");
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {

                e.Row.Cells[0].Width = new Unit("60px");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].Width = new Unit("80px");
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            }
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

    #region [txtEquitySale_ac_TextChanged]
    protected void txtEquitySale_ac_TextChanged(object sender, EventArgs e)
    {
        searchString = txtEquitySale_ac.Text;
        strTextBox = "txtEquitySale_ac";
        csCalculations();
    }
    #endregion

    #region [btntxtEquitySale_ac_Click]
    protected void btntxtEquitySale_ac_Click(object sender, EventArgs e)
    {
        try
        {
           
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtEquitySale_ac";
                btnSearch_Click(sender, e);
            
        }
        catch
        {
        }
    }
    #endregion

    #region [txtEquityPurchase_ac_TextChanged]
    protected void txtEquityPurchase_ac_TextChanged(object sender, EventArgs e)
    {
        searchString = txtEquityPurchase_ac.Text;
        strTextBox = "txtEquityPurchase_ac";
        csCalculations();
    }
    #endregion

    #region [btntxtEquityPurchase_ac_Click]
    protected void btntxtEquityPurchase_ac_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtEquityPurchase_ac";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtEquityExpenses_ac_TextChanged]
    protected void txtEquityExpenses_ac_TextChanged(object sender, EventArgs e)
    {
        searchString = txtEquityExpenses_ac.Text;
        strTextBox = "txtEquityExpenses_ac";
        csCalculations();
    }
    #endregion

    #region [btntxtEquityExpenses_ac_Click]
    protected void btntxtEquityExpenses_ac_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtEquityExpenses_ac"; 
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtFNOSale_ac_TextChanged]
    protected void txtFNOSale_ac_TextChanged(object sender, EventArgs e)
    {
        searchString = txtFNOSale_ac.Text;
        strTextBox = "txtFNOSale_ac";
        csCalculations();
    }
    #endregion

    #region [btntxtFNOSale_ac_Click]
    protected void btntxtFNOSale_ac_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtFNOSale_ac";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtFNOPurchase_ac_TextChanged]
    protected void txtFNOPurchase_ac_TextChanged(object sender, EventArgs e)
    {
        searchString = txtFNOPurchase_ac.Text;
        strTextBox = "txtFNOPurchase_ac";
        csCalculations();
    }
    #endregion

    #region [btntxtFNOPurchase_ac_Click]
    protected void btntxtFNOPurchase_ac_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtFNOPurchase_ac";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtFNOExpenses_ac_TextChanged]
    protected void txtFNOExpenses_ac_TextChanged(object sender, EventArgs e)
    {
        searchString = txtFNOExpenses_ac.Text;
        strTextBox = "txtFNOExpenses_ac";
        csCalculations();
    }
    #endregion

    #region [btntxtFNOExpenses_ac_Click]
    protected void btntxtFNOExpenses_ac_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtFNOExpenses_ac";
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
            //if (searchString != string.Empty)
            //{
            //    txtSearchText.Text = searchString;
            

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

            if (hdnfClosePopup.Value == "txtEquitySale_ac")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or cityname like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Equity Sale AC--";
                string qry = "select Ac_Code,Ac_Name_E,cityname from " + qryCommon
                    + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtEquityPurchase_ac")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or cityname like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Equity Purchase AC--";
                string qry = "select Ac_Code,Ac_Name_E,cityname from " + qryCommon
                    + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtEquityExpenses_ac")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or cityname like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Equity Expenses AC--";
                string qry = "select Ac_Code,Ac_Name_E,cityname from " + qryCommon
                    + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtFNOSale_ac")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or cityname like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select FNO Sale AC--";
                string qry = "select Ac_Code,Ac_Name_E,cityname from " + qryCommon
                    + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtFNOPurchase_ac")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or cityname like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select FNO Purchase AC--";
                string qry = "select Ac_Code,Ac_Name_E,cityname from " + qryCommon
                    + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtFNOExpenses_ac")
            {
                {
                    foreach (var s in split)
                    {
                        string aa = s.ToString();

                        name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or cityname like '%" + aa + "%') and";
                    }
                    name = name.Remove(name.Length - 3);
                    lblPopupHead.Text = "--Select FNO Expenses AC--";
                    string qry = "select Ac_Code,Ac_Name_E,cityname from " + qryCommon
                        + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                    this.showPopup(qry);
                }
            }

        }
        catch
        {
        }
    }

    private void branchgrid(string qry)
    {
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
                }
                else
                {
                    grdPopup.DataSource = null;
                    grdPopup.DataBind();
                }
            }
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

            if (hdnfClosePopup.Value == "txtEquitySale_ac")
            {
                setFocusControl(txtEquitySale_ac);
            }

            if (hdnfClosePopup.Value == "txtEquityPurchase_ac")
            {
                setFocusControl(txtEquityPurchase_ac);
            }
            if (hdnfClosePopup.Value == "txtEquityExpenses_ac")
            {
                setFocusControl(txtEquityExpenses_ac);
            }
            if (hdnfClosePopup.Value == "txtFNOSale_ac")
            {
                setFocusControl(txtFNOSale_ac);
            }
            if (hdnfClosePopup.Value == "txtFNOPurchase_ac")
            {
                setFocusControl(txtFNOPurchase_ac);
            }
            if (hdnfClosePopup.Value == "txtFNOExpenses_ac")
            {
                setFocusControl(txtFNOExpenses_ac);
            }

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


    #region csCalculations
    private void csCalculations()
    {
        try
        {
          //  hdnfpopup.Value = null;


            if (strTextBox == "txtEquitySale_ac")
            {
                if (txtEquitySale_ac.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtEquitySale_ac.Text);
                    if (a == false)
                    {
                        btntxtEquitySale_ac_Click(this, new EventArgs());
                    }
                    else
                    {
                        string str = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code=" + txtEquitySale_ac.Text + "");
                        if (str != string.Empty && str != "0")
                        {
                            lblEquitySale_name.Text = str;

                            setFocusControl(txtEquityPurchase_ac);
                        }
                        else
                        {
                            lblEquitySale_name.Text = string.Empty;
                            txtEquitySale_ac.Text = string.Empty;
                            setFocusControl(txtEquitySale_ac);
                        }
                    }
                }
                else
                {
                 
                    setFocusControl(txtEquitySale_ac);
                }
            }

            if (strTextBox == "txtEquityPurchase_ac")
            {
                if (txtEquityPurchase_ac.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtEquityPurchase_ac.Text);
                    if (a == false)
                    {
                        btntxtEquityPurchase_ac_Click(this, new EventArgs());
                    }
                    else
                    {
                        string str = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code=" + txtEquityPurchase_ac.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (str != string.Empty && str != "0")
                        {
                            lblEquityPurchase_name.Text = str;
                            setFocusControl(txtEquityExpenses_ac);
                        }
                        else
                        {
                            lblEquityPurchase_name.Text = string.Empty;
                            txtEquityPurchase_ac.Text = string.Empty;
                            setFocusControl(txtEquityExpenses_ac);
                        }
                    }
                }
                else
                {
                    lblEquityPurchase_name.Text = string.Empty;
                    txtEquityPurchase_ac.Text = string.Empty;
                    setFocusControl(txtEquityExpenses_ac);
                }
            }
            if (strTextBox == "txtEquityExpenses_ac")
            {
                if (txtEquityExpenses_ac.Text != string.Empty)
                {
                    string str = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code=" + txtEquityExpenses_ac.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblEquityExpenses_name.Text = str;
                        setFocusControl(txtFNOSale_ac);
                    }
                    else
                    {
                        lblEquityExpenses_name.Text = string.Empty;
                        txtEquityExpenses_ac.Text = string.Empty;
                        setFocusControl(txtFNOSale_ac);
                    }
                }
                else
                {
                    lblEquityExpenses_name.Text = string.Empty;
                    txtEquityExpenses_ac.Text = string.Empty;
                    setFocusControl(txtFNOSale_ac);
                }

                //string millName = string.Empty;
                //searchString = txtEquityExpenses_ac.Text;
                //if (txtEquityExpenses_ac.Text != string.Empty)
                //{
                //    bool a = clsCommon.isStringIsNumeric(txtEquityExpenses_ac.Text);
                //    if (a == false)
                //    {
                //        btntxtEquitySale_ac_Click(this, new EventArgs());
                //    }
                //    else
                //    {
                //        millName = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code='" + txtEquityExpenses_ac.Text + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
                //        if (millName != string.Empty)
                //        {
                //            lblEquityExpenses_name.Text = millName;
                //            setFocusControl(txtFNOSale_ac);
                //        }
                //        else
                //        {
                //            txtEquityExpenses_ac.Text = string.Empty;
                //            lblEquityExpenses_name.Text = string.Empty;
                //            setFocusControl(txtEquityExpenses_ac);
                //        }
                //    }
                //}
                //else
                //{
                //    txtEquityExpenses_ac.Text = string.Empty;
                //    lblEquityExpenses_name.Text = millName;
                //    setFocusControl(txtEquityExpenses_ac);
                //}
            }

            if (strTextBox == "txtFNOSale_ac")
            {
                if (txtFNOSale_ac.Text != string.Empty)
                {
                    string str = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code=" + txtFNOSale_ac.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblFNOSale_name.Text = str;
                        setFocusControl(txtFNOPurchase_ac);
                    }
                    else
                    {
                        lblFNOSale_name.Text = string.Empty;
                        txtFNOSale_ac.Text = string.Empty;
                        setFocusControl(txtFNOPurchase_ac);
                    }
                }
                else
                {
                    lblFNOSale_name.Text = string.Empty;
                    txtFNOSale_ac.Text = string.Empty;
                    setFocusControl(txtFNOPurchase_ac);
                }
            }

            if (strTextBox == "txtFNOPurchase_ac")
            {
                if (txtFNOPurchase_ac.Text != string.Empty)
                {
                    string str = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code=" + txtFNOPurchase_ac.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblFNOPurchase_name.Text = str;
                        setFocusControl(txtFNOExpenses_ac);
                    }
                    else
                    {
                        lblFNOPurchase_name.Text = string.Empty;
                        txtFNOPurchase_ac.Text = string.Empty;
                        setFocusControl(txtFNOExpenses_ac);
                    }
                }
                else
                {
                    lblFNOPurchase_name.Text = string.Empty;
                    txtFNOPurchase_ac.Text = string.Empty;
                    setFocusControl(txtFNOExpenses_ac);
                }
            }

            if (strTextBox == "txtFNOExpenses_ac")
            {
                if (txtFNOExpenses_ac.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtFNOExpenses_ac.Text);
                    if (a == false)
                    {
                        btntxtFNOExpenses_ac_Click(this, new EventArgs());
                    }
                    else
                    {
                      //  string str = clsCommon.getString("Select State_Name from GSTStateMaster where State_Code=" + txtFNOExpenses_ac.Text + "");
                        string str = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code=" + txtFNOExpenses_ac.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (str != string.Empty && str != "0")
                        {
                            lblFNOExpenses_name.Text = str;
                            setFocusControl(btnUpdate);
                        }
                        else
                        {
                            lblFNOExpenses_name.Text = string.Empty;
                            txtFNOExpenses_ac.Text = string.Empty;
                            setFocusControl(txtFNOExpenses_ac);
                        }
                    }
                }
                else
                {
                    lblFNOExpenses_name.Text = string.Empty;
                    txtFNOExpenses_ac.Text = string.Empty;
                    setFocusControl(txtFNOExpenses_ac);
                }
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
}


