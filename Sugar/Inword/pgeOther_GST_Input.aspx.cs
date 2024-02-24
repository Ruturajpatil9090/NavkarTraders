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


public partial class Sugar_Inword_pgeOther_GST_Input : System.Web.UI.Page
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
    string isAuthenticate = string.Empty;
    static WebControl objAsp = null;
    string user = string.Empty;
    string cs = string.Empty;
    string Action = string.Empty;
    #endregion

    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;

    #region text and label data Declaration
    int DOC_NO = 0;
    string tran_type = "OI";
    string doc_date = string.Empty;
    double sgst = 0.00;
    double cgst = 0.00;
    double igst = 0.00;
    int expac = 0;
    string naration = string.Empty;
    int oid = 0;
    int eaid = 0;
    string createddate = string.Empty;
    string modifieddate = string.Empty;
    int gstid = 0;
    int Doc_No = 0;
    int counts = 0;
    int count = 0;
    int company_code = 0;
    string userinfo = string.Empty;
    string retValue = string.Empty;
    string strRev = string.Empty;
    string Created_By = string.Empty;
    string Modified_By = string.Empty;
    int year = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            setFocusControl(btnAdd);
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "other_input_gst";
            tblDetails = "";
            qryCommon = "qryotherinputgst";
            pnlPopup.Style["display"] = "none";
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

                        hdnf.Value = Request.QueryString["Oid"];

                        // ViewState["currentTable"] = null;
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
                        setFocusControl(txtCGST_Amt);
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

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        this.NextNumber();
        setFocusControl(txtCGST_Amt);

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
                txtDoc_No.Text = hdnf.Value;
                hdnfSuffix.Value = "";
                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Focus();
                }
                //  this.enableDisableNavigateButtons();
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
    #region Generate Next Number
    private void NextNumber()
    {
        try
        {
            int counts = 0;
            counts = Convert.ToInt32(clsCommon.getString("select count(Doc_No) as A from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'"));
            if (counts == 0)
            {
                txtDoc_No.Text = "1";
                Doc_No = 1;
            }
            else
            {
                Doc_No = Convert.ToInt32(clsCommon.getString("SELECT max(Doc_No) as Doc_No from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'")) + 1;
                txtDoc_No.Text = Doc_No.ToString();
            }

            counts = Convert.ToInt32(clsCommon.getString("SELECT count(Oid) as bsid from " + tblHead + ""));
            if (counts == 0)
            {
                lblDocNo.Text = "1";
                gstid = 1;
            }
            else
            {
                gstid = Convert.ToInt32(clsCommon.getString("SELECT max(Oid) as bsid from " + tblHead)) + 1;
                lblDocNo.Text = gstid.ToString();
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Next Record Error!')", true);
        }
    }
    #endregion
    private string getDisplayQuery()
    {
        try
        {

            string qryDisplay = "select * from " + qryCommon + " where Oid =" + hdnf.Value;
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #region [btnSave_Click ]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnfyearcode.Value != Session["year"].ToString())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records year code & current year code is not same!')", true);
                return;
                // Response.Redirect("~/Sugar/pgeHome.aspx", false);
            }
            if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
                return;

            }
            #region [Head part declaration]
            doc_date = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            DOC_NO = txtDoc_No.Text != string.Empty ? Convert.ToInt32(txtDoc_No.Text) : 0;
            cgst = txtCGST_Amt.Text != string.Empty ? Convert.ToDouble(txtCGST_Amt.Text) : 0.00;
            sgst = txtSGST_Amt.Text != string.Empty ? Convert.ToDouble(txtSGST_Amt.Text) : 0.00;
            igst = txtIGST_Amt.Text != string.Empty ? Convert.ToDouble(txtIGST_Amt.Text) : 0.00;
            expac = txtExps.Text != string.Empty ? Convert.ToInt32(txtExps.Text) : 0;
            naration = txtNarration.Text;
            oid = lblDocNo.Text != string.Empty ? Convert.ToInt32(lblDocNo.Text) : 0;

            try
            {
                eaid = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + txtExps.Text + " and Company_Code=" + Session["Company_Code"].ToString() + ""));
            }
            catch
            {
            }

            company_code = Convert.ToInt32(Session["Company_Code"].ToString());

            retValue = string.Empty;
            strRev = string.Empty;
            createddate = DateTime.Now.ToString("yyyy/MM/dd");
            modifieddate = DateTime.Now.ToString("yyyy/MM/dd");
            Created_By = Session["user"].ToString();
            Modified_By = Session["user"].ToString();
            year = Convert.ToInt32(Session["year"].ToString());

            string Head_Fields = string.Empty;
            string Head_Values = string.Empty;
            string Head_Update = string.Empty;
            int flag = 0;
            int no = 0;
            string op = string.Empty;
            string returnmaxno = string.Empty;
            #endregion


            if (btnSave.Text == "Save")
            {
                this.NextNumber();
                #region Create Head Insert  Query
                Head_Fields = "Doc_No,";
                Head_Values = "'" + DOC_NO + "',";
                Head_Fields = Head_Fields + "Doc_Date,";
                Head_Values = Head_Values + "'" + doc_date + "',";
                Head_Fields = Head_Fields + "SGST_Amt,";
                Head_Values = Head_Values + "'" + sgst + "',";
                Head_Fields = Head_Fields + "CGST_Amt,";
                Head_Values = Head_Values + "'" + cgst + "',";
                Head_Fields = Head_Fields + "IGST_Amt,";
                Head_Values = Head_Values + "'" + igst + "',";
                Head_Fields = Head_Fields + "Exps_Ac,";
                Head_Values = Head_Values + "'" + expac + "',";
                Head_Fields = Head_Fields + "Narration,";
                Head_Values = Head_Values + "'" + naration + "' ,";
                Head_Fields = Head_Fields + "TRAN_TYPE,";
                Head_Values = Head_Values + "'" + tran_type + "' ,";
                Head_Fields = Head_Fields + "Oid,";
                Head_Values = Head_Values + "'" + oid + "' ,";
                Head_Fields = Head_Fields + "ea,";
                Head_Values = Head_Values + "'" + eaid + "' ,";
                Head_Fields = Head_Fields + "Company_Code,";
                Head_Values = Head_Values + "'" + company_code + "' ,";
                Head_Fields = Head_Fields + "Year_Code,";
                Head_Values = Head_Values + "'" + year + "' ,";
                Head_Fields = Head_Fields + "Created_By,";
                Head_Values = Head_Values + "'" + Created_By + "',";
                Head_Fields = Head_Fields + "Created_Date";
                Head_Values = Head_Values + "'" + createddate + "'";
                #endregion
                flag = 1;
                qry = "insert into " + tblHead + "(" + Head_Fields + ") values(" + Head_Values + ")";


                Thread thred = new Thread(() => { count = DataStore(qry, flag); }); //Calling DataStore Method Using Thread
                thred.Start(); //Thread Operation Start
                thred.Join();

                //if (count == 1)
                //{
                //    hdnf.Value = lblDocNo.ToString();
                //    clsButtonNavigation.enableDisable("S");
                //    this.makeEmptyForm("S");
                //    qry = getDisplayQuery();
                //    this.fetchRecord(qry);
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Added !')", true);
                //}
            }
            else
            {
                #region Create Update Query

                Head_Update = Head_Update + "SGST_Amt=";
                Head_Update = Head_Update + "'" + sgst + "',";
                Head_Update = Head_Update + "CGST_Amt=";
                Head_Update = Head_Update + "'" + cgst + "',";
                Head_Update = Head_Update + "IGST_Amt=";
                Head_Update = Head_Update + "'" + igst + "',";
                Head_Update = Head_Update + "Exps_Ac=";
                Head_Update = Head_Update + "'" + expac + "',";
                Head_Update = Head_Update + "Narration=";
                Head_Update = Head_Update + "'" + naration + "',";
                Head_Update = Head_Update + "ea=";
                Head_Update = Head_Update + "'" + eaid + "',";

                Head_Update = Head_Update + "Modified_By=";
                Head_Update = Head_Update + "'" + Modified_By + "',";
                Head_Update = Head_Update + "Modified_Date=";
                Head_Update = Head_Update + "'" + modifieddate + "'";
                #endregion

                flag = 2;
                qry = "update " + tblHead + " set " + Head_Update + " where Company_Code='" + company_code + "' and YEAR_CODE='" + year + "' and Doc_no='" + txtDoc_No.Text + "'";


                Thread thred = new Thread(() => { count = DataStore(qry, flag); });
                thred.Start();
                thred.Join();
                //if (count == 2)
                //{

                //    hdnf.Value = txtDoc_No.Text;
                //    clsButtonNavigation.enableDisable("S");
                //    this.makeEmptyForm("S");
                //    qry = getDisplayQuery();
                //    this.fetchRecord(qry);

                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Update !')", true);
                //}


            }
            #region Gledger Effect
            string deleteledger = Convert.ToString(clsCommon.getString("delete from nt_1_gledger where TRAN_TYPE='OI' and  DOC_NO=" + DOC_NO +
              " and company_code='" + Session["Company_Code"].ToString() + "' and YEAR_CODE='" + year + "' "));
            string ledgerfleid = "";
            Head_Values = "";
            string values = "";
            int ordercode = 1;
            qry = "";
            int cgstacid = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["PurchaseCGSTAc"] +
               " and company_code='" + Session["Company_Code"].ToString() + "'"));
            int sgstacid = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["PurchaseSGSTAc"] +
                " and company_code='" + Session["Company_Code"].ToString() + "'"));
            int igstacid = Convert.ToInt32(clsCommon.getString("select accoid from qrymstaccountmaster where Ac_Code=" + Session["PurchaseIGSTAc"] +
                " and company_code='" + Session["Company_Code"].ToString() + "'"));
            string gledger = "nt_1_gledger";
            string narration = txtNarration.Text != string.Empty ? txtNarration.Text : "";
            double Bill_Amount = cgst + sgst + igst;
            #region[EXPAcCode]
            if (Bill_Amount > 0)
            {
                ledgerfleid = ledgerfleid + "TRAN_TYPE," + " ";
                Head_Values = Head_Values + "'OI',";
                ledgerfleid = ledgerfleid + "DOC_NO," + " ";
                Head_Values = Head_Values + "'" + DOC_NO + "',";
                ledgerfleid = ledgerfleid + "DOC_DATE," + " ";
                Head_Values = Head_Values + "'" + doc_date + "',";
                ledgerfleid = ledgerfleid + "AC_CODE," + " ";
                Head_Values = Head_Values + "'" + expac + "',";
                ledgerfleid = ledgerfleid + "NARRATION," + " ";
                Head_Values = Head_Values + "'" + narration + "',";


               // Head_Values = Head_Values + "'Other purchase',";
                ledgerfleid = ledgerfleid + "AMOUNT," + " ";
                Head_Values = Head_Values + "'" + Bill_Amount + "',";
                ledgerfleid = ledgerfleid + "YEAR_CODE," + " ";
                Head_Values = Head_Values + "'" + year + "',";
                ledgerfleid = ledgerfleid + "COMPANY_CODE," + " ";
                Head_Values = Head_Values + "'" + company_code + "',";
                ledgerfleid = ledgerfleid + "ORDER_CODE," + " ";
                Head_Values = Head_Values + "'" + ordercode + "',";
                ledgerfleid = ledgerfleid + "DRCR," + " ";
                Head_Values = Head_Values + "'C',";
                ledgerfleid = ledgerfleid + "SORT_TYPE," + " ";
                Head_Values = Head_Values + "'OI',";
                ledgerfleid = ledgerfleid + "SORT_NO," + " ";
                Head_Values = Head_Values + "'" + DOC_NO + "',";
                ledgerfleid = ledgerfleid + "ac" + " ";
                Head_Values = Head_Values + "'" + eaid + "'";
                ordercode = ordercode + 1;
                flag = 1;
                qry = qry + "insert into " + gledger + "(" + ledgerfleid + ") values(" + Head_Values + ") ";
            }
            #endregion
            ledgerfleid = "";
            Head_Values = "";
            #region[cgst]
            if (cgst > 0)
            {
                ledgerfleid = ledgerfleid + "TRAN_TYPE," + "";
                Head_Values = Head_Values + "'OI',";
                ledgerfleid = ledgerfleid + "DOC_NO," + " ";
                Head_Values = Head_Values + "'" + DOC_NO + "',";
                ledgerfleid = ledgerfleid + "DOC_DATE," + " ";
                Head_Values = Head_Values + "'" + doc_date + "',";
                ledgerfleid = ledgerfleid + "AC_CODE," + " ";
                Head_Values = Head_Values + "'" + Session["PurchaseCGSTAc"] + "',";
                ledgerfleid = ledgerfleid + "NARRATION," + " ";
                Head_Values = Head_Values + "'" + narration + "',";
                ledgerfleid = ledgerfleid + "DRCR_HEAD," + " ";
                Head_Values = Head_Values + "'" + expac + "',";
                // Head_Values = Head_Values + "'Other purchase',";
                ledgerfleid = ledgerfleid + "AMOUNT," + " ";
                Head_Values = Head_Values + "'" + cgst + "',";
                ledgerfleid = ledgerfleid + "YEAR_CODE," + " ";
                Head_Values = Head_Values + "'" + year + "',";
                ledgerfleid = ledgerfleid + "COMPANY_CODE," + " ";
                Head_Values = Head_Values + "'" + company_code + "',";
                ledgerfleid = ledgerfleid + "ORDER_CODE," + " ";
                Head_Values = Head_Values + "'" + ordercode + "',";
                ledgerfleid = ledgerfleid + "DRCR," + " ";
                Head_Values = Head_Values + "'D',";
                ledgerfleid = ledgerfleid + "SORT_TYPE," + " ";
                Head_Values = Head_Values + "'OI',";
                ledgerfleid = ledgerfleid + "SORT_NO," + " ";
                Head_Values = Head_Values + "'" + DOC_NO + "',";
                ledgerfleid = ledgerfleid + "ac" + " ";
                Head_Values = Head_Values + "'" + cgstacid + "'";

                ordercode = ordercode + 1;
                flag = 1;
                qry = qry + "insert into " + gledger + "(" + ledgerfleid + ") values(" + Head_Values + ") ";
            }
            #endregion

            ledgerfleid = "";
            Head_Values = "";

            #region[sgst]
            if (sgst > 0)
            {
                ledgerfleid = ledgerfleid + "TRAN_TYPE," + "";
                Head_Values = Head_Values + "'OI',";
                ledgerfleid = ledgerfleid + "DOC_NO," + " ";
                Head_Values = Head_Values + "'" + DOC_NO + "',";
                ledgerfleid = ledgerfleid + "DOC_DATE," + " ";
                Head_Values = Head_Values + "'" + doc_date + "',";
                ledgerfleid = ledgerfleid + "AC_CODE," + " ";
                Head_Values = Head_Values + "'" + Session["PurchaseSGSTAc"] + "',";
                ledgerfleid = ledgerfleid + "NARRATION," + " ";

                Head_Values = Head_Values + "'" + narration + "',";
                ledgerfleid = ledgerfleid + "DRCR_HEAD," + " ";
                Head_Values = Head_Values + "'" + expac + "',";
                //  Head_Values = Head_Values + "'Other purchase',";
                ledgerfleid = ledgerfleid + "AMOUNT," + " ";
                Head_Values = Head_Values + "'" + sgst + "',";
                ledgerfleid = ledgerfleid + "YEAR_CODE," + " ";
                Head_Values = Head_Values + "'" + year + "',";
                ledgerfleid = ledgerfleid + "COMPANY_CODE," + " ";
                Head_Values = Head_Values + "'" + company_code + "',";
                ledgerfleid = ledgerfleid + "ORDER_CODE," + " ";
                Head_Values = Head_Values + "'" + ordercode + "',";
                ledgerfleid = ledgerfleid + "DRCR," + " ";
                Head_Values = Head_Values + "'D',";
                ledgerfleid = ledgerfleid + "SORT_TYPE," + " ";
                Head_Values = Head_Values + "'OI',";
                ledgerfleid = ledgerfleid + "SORT_NO," + " ";
                Head_Values = Head_Values + "'" + DOC_NO + "',";
                ledgerfleid = ledgerfleid + "ac" + " ";
                Head_Values = Head_Values + "'" + sgstacid + "'";
                ordercode = ordercode + 1;
                flag = 1;
                qry = qry + "insert into " + gledger + "(" + ledgerfleid + ") values(" + Head_Values + ") ";
            }
            #endregion

            ledgerfleid = "";
            Head_Values = "";

            #region[igst]
            if (igst > 0)
            {
                ledgerfleid = ledgerfleid + "TRAN_TYPE," + "";
                Head_Values = Head_Values + "'OI',";
                ledgerfleid = ledgerfleid + "DOC_NO," + " ";
                Head_Values = Head_Values + "'" + DOC_NO + "',";
                ledgerfleid = ledgerfleid + "DOC_DATE," + " ";
                Head_Values = Head_Values + "'" + doc_date + "',";
                ledgerfleid = ledgerfleid + "AC_CODE," + " ";
                Head_Values = Head_Values + "'" + Session["PurchaseIGSTAc"] + "',";
                ledgerfleid = ledgerfleid + "NARRATION," + " ";
                Head_Values = Head_Values + "'" + narration + "',";
                ledgerfleid = ledgerfleid + "DRCR_HEAD," + " ";
                Head_Values = Head_Values + "'" + expac + "',";
                //  Head_Values = Head_Values + "'Other purchase',";
                ledgerfleid = ledgerfleid + "AMOUNT," + " ";
                Head_Values = Head_Values + "'" + igst + "',";
                ledgerfleid = ledgerfleid + "YEAR_CODE," + " ";
                Head_Values = Head_Values + "'" + year + "',";
                ledgerfleid = ledgerfleid + "COMPANY_CODE," + " ";
                Head_Values = Head_Values + "'" + company_code + "',";
                ledgerfleid = ledgerfleid + "ORDER_CODE," + " ";
                Head_Values = Head_Values + "'" + ordercode + "',";
                ledgerfleid = ledgerfleid + "DRCR," + " ";
                Head_Values = Head_Values + "'D',";
                ledgerfleid = ledgerfleid + "SORT_TYPE," + " ";
                Head_Values = Head_Values + "'OI',";
                ledgerfleid = ledgerfleid + "SORT_NO," + " ";
                Head_Values = Head_Values + "'" + DOC_NO + "',";
                ledgerfleid = ledgerfleid + "ac" + " ";
                Head_Values = Head_Values + "'" + igstacid + "'";
                ordercode = ordercode + 1;
                flag = 1;
                qry = qry + "insert into " + gledger + "(" + ledgerfleid + ") values(" + Head_Values + ") ";
            }
            #endregion

            if (btnSave.Text == "Save")
            {
                flag = 1;
            }
            else
            {
                flag = 2;
            }
            Thread thred1 = new Thread(() => { count = DataStore(qry, flag); });
            thred1.Start();
            thred1.Join();
            if (count == 1)
            {
                hdnf.Value = lblDocNo.ToString();
                clsButtonNavigation.enableDisable("S");
                this.makeEmptyForm("S");
                qry = getDisplayQuery();
                this.fetchRecord(qry);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Added !')", true);
            }

            if (count == 2)
            {

                hdnf.Value = txtDoc_No.Text;
                clsButtonNavigation.enableDisable("S");
                this.makeEmptyForm("S");
                qry = getDisplayQuery();
                this.fetchRecord(qry);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Update !')", true);
            }

            #endregion


        }
        catch
        {
        }
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
    #region [btnEdit_Click ]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        this.makeEmptyForm("E");
        txtDoc_No.Enabled = false;
        setFocusControl(txtCGST_Amt);
    }
    #endregion

    #region [btnDelete_Click ]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                ////string str = clsCommon.getString("select Ac_Code from " + AccountMasterTable + " where city_code=" + txtDoc_No.Text + "");
                //if (str == string.Empty)   //Gledger does not contain this account then delete
                //{
                string currentDoc_No = txtDoc_No.Text;
                int flag = 3;
                string strrev = "";

                qry = "delete from " + tblHead + " where Doc_No='" + currentDoc_No + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                Thread thred = new Thread(() => { count = DataStore(qry, flag); }); //Calling DataStore Method Using Thread
                thred.Start(); //Thread Operation Start
                thred.Join();

                if (count == 3)
                {
                    Response.Redirect("../Inword/pgeOtherGSTInput_Utility.aspx");
                }


                //}
                //else
                //{
                //    lblMsg.Text = "Cannot delete this Group , it is in use";
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //}
            }
            else
            {
                hdnf.Value = txtDoc_No.Text;
                showLastRecord();
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [btnCancel_Click ]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        int maxno = Convert.ToInt32(clsCommon.getString("select isnull(max(Oid),0) as Oid from other_input_gst "));

        hdnf.Value = Convert.ToString(maxno);
        clsButtonNavigation.enableDisable("S");
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);
    }
    #endregion

    #region [txtEditDoc_No_TextChanged]
    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
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
                hdnf.Value = txtEditDoc_No.Text;
                //string qry1 = getDisplayQuery();
                //fetchRecord(qry1);
                setFocusControl(txtEditDoc_No);
            }
        }
        catch (Exception)
        {
            throw;
        }

    }
    #endregion

    #region [txtDoc_No_TextChanged]
    protected void txtDoc_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_No.Text;
        strTextBox = "txtDoc_No";
        csCalculations();
    }
    #endregion

    #region [btntxtDoc_No_Click]
    protected void btntxtDoc_No_Click(object sender, EventArgs e)
    {
        try
        {
            //pnlPopup.Style["display"] = "block";
            //hdnfClosePopup.Value = "txtDoc_No";
            //btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtDoc_Date_TextChanged]
    protected void txtDoc_Date_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtDoc_Date.Text;
        //strTextBox = "txtDoc_Date";
        //csCalculations();
    }
    #endregion
    #region [txtIGST_Amt_TextChanged]
    protected void txtIGST_Amt_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtIGST_Amt.Text;
        //strTextBox = "txtIGST_Amt";
        //csCalculations();
    }
    #endregion
    #region [txtCGST_Amt_TextChanged]
    protected void txtCGST_Amt_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtCGST_Amt.Text;
        //strTextBox = "txtCGST_Amt";
        //csCalculations();
    }
    #endregion
    #region [txtSGST_Amt_TextChanged]
    protected void txtSGST_Amt_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtSGST_Amt.Text;
        //strTextBox = "txtSGST_Amt";
        //csCalculations();
    }
    #endregion
    #region [txtExps_TextChanged]

    protected void txtExps_TextChanged(object sender, EventArgs e)
    {
        searchString = txtExps.Text;
        strTextBox = "txtExps";
        csCalculations();
    }
    #endregion

    #region [btntxtExps_Click]
    protected void btntxtExps_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtExps";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtNarration_TextChanged]
    protected void txtNarration_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtNarration.Text;
        //strTextBox = "txtNarration";
        //csCalculations();
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

    #region [txtSearchText_TextChanged]
    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {
        try
        {
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
            if (hdnfClosePopup.Value == "txtDoc_No" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                //lblPopupHead.Text = "--Select City--";
                //string qry = "select city_code,city_name_e from " + tblHead + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and (city_name_e like '%" + txtSearchText.Text + "%' or city_code like '%" + txtSearchText.Text + "%')";
                //this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtExps")
            {

                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%'  ) and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select GST State Code--";
                string qry = "select Ac_Code,Ac_Name_E from qrymstaccountmaster where (Ac_Name_E like '%" + txtSearchText.Text + "%' or Ac_Code like '%" + txtSearchText.Text + "%')";
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

    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion

    #region [grdPopup_PageIndexChanging]
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
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
        try
        {
            string v = hdnfClosePopup.Value;
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                if (v == "txtExps")
                {
                    e.Row.Cells[0].Width = new Unit("40px");
                    e.Row.Cells[0].ControlStyle.Width = new Unit("120px");
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }

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
            if (strTextBox == "txtEditDoc_No")
            {
                setFocusControl(txtEditDoc_No);
            }
            if (strTextBox == "txtDoc_No")
            {
                setFocusControl(txtDoc_No);
            }
            if (strTextBox == "txtDoc_Date")
            {
                try
                {
                    string dt = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    if (clsCommon.isValidDateforOp(dt) == true || dt == "")
                    {
                        setFocusControl(txtCGST_Amt);
                    }
                    else
                    {
                        txtDoc_Date.Text = "";
                        setFocusControl(txtDoc_Date);
                    }
                }
                catch
                {
                    txtDoc_Date.Text = "";
                    setFocusControl(txtDoc_Date);
                }
            }
            if (strTextBox == "txtCGST_Amt")
            {
                setFocusControl(txtCGST_Amt);
            }
            if (strTextBox == "txtSGST_Amt")
            {
                setFocusControl(txtSGST_Amt);
            }
            if (strTextBox == "txtIGST_Amt")
            {
                setFocusControl(txtIGST_Amt);
            }


            if (strTextBox == "txtExps")
            {
                string Expcodetext = txtExps.Text.Trim();
                if (Expcodetext != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(Expcodetext);
                    if (a == false)
                    {
                        btntxtExps_Click(this, new EventArgs());
                    }
                    else
                    {
                        string gstStateName = clsCommon.getString("select Ac_Name_E from  qrymstaccountmaster where Ac_Code=" + Expcodetext + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (gstStateName != string.Empty)
                        {
                            lblExps.Text = gstStateName;
                            setFocusControl(txtNarration);

                        }
                        else
                        {
                            txtExps.Text = string.Empty;
                            lblExps.Text = string.Empty;
                        }
                    }
                }
                else
                {
                    lblExps.Text = "";
                    setFocusControl(txtNarration);
                    setFocusControl(btnSave);
                }
                return;
            }
            //if (strTextBox == "txtExps")
            //{

            //    string acname = "";
            //    if (txtExps.Text != string.Empty)
            //    {
            //        bool a = clsCommon.isStringIsNumeric(txtExps.Text);
            //        if (a == false)
            //        {
            //            btntxtExps_Click(this, new EventArgs());
            //        }
            //        else
            //        {
            //            acname = clsCommon.getString("select Ac_Name_E from  Account_Master where Ac_Code=" + txtExps.Text
            //                + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //            if (acname != string.Empty)
            //            {
            //                lblExps.Text = acname;
            //                //setFocusControl(btnAdddetails);
            //                setFocusControl(txtNarration);
            //            }
            //            else
            //            {
            //                txtExps.Text = string.Empty;
            //                lblExps.Text = acname;
            //                setFocusControl(txtNarration);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        setFocusControl(txtExps);
            //    }
            //}

            if (strTextBox == "txtNarration")
            {
                setFocusControl(txtNarration);
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
                        string yearcode = Session["year"].ToString();
                        if (hdnfyearcode.Value != yearcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records year code & current year code is not same !')", true);
                            return false;
                        }
                        string comcode = Session["Company_Code"].ToString();
                        if (hdnfcompanycode.Value != comcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same !')", true);
                            return false;
                        }
                        txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_Date.Text = dt.Rows[0]["Doc_DateConv"].ToString();
                        txtCGST_Amt.Text = dt.Rows[0]["CGST_Amt"].ToString();
                        txtSGST_Amt.Text = dt.Rows[0]["SGST_Amt"].ToString();
                        txtIGST_Amt.Text = dt.Rows[0]["IGST_Amt"].ToString();
                        txtNarration.Text = dt.Rows[0]["Narration"].ToString();
                        txtExps.Text = dt.Rows[0]["Exps_Ac"].ToString();
                        //string gstStateName = clsCommon.getString("select Ac_Name_E from  qrymstaccountmaster where Ac_Code=" + txtExps.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        //lblExps.Text = gstStateName;
                        lblExps.Text = dt.Rows[0]["Ac_Name_E"].ToString();
                        lblexpid.Text = dt.Rows[0]["ea"].ToString();
                        lblDocNo.Text = dt.Rows[0]["Doc_No"].ToString();
                        hdnfyearcode.Value = dt.Rows[0]["Year_Code"].ToString();
                        hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();
                        hdnf.Value = txtDoc_No.Text;
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
            return recordExist;
        }
        catch
        {
            return false;
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
                // btnAdddetails.Enabled = false;
                // btnClosedetails.Enabled = false;

                btnSave.Text = "Save";
                btnSave.Enabled = false;
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtCGST_Amt.Enabled = false;
                txtSGST_Amt.Enabled = false;
                txtIGST_Amt.Enabled = false;
                txtNarration.Enabled = false;
                btntxtExps.Enabled = false;
                ViewState["currentTable"] = null;
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
                btntxtDoc_No.Text = "Change No";
                btntxtDoc_No.Enabled = true;
                txtEditDoc_No.Enabled = false;
                txtDoc_No.Enabled = false;
                //btnAdddetails.Enabled = true;
                //btnClosedetails.Enabled = true;
                ViewState["currentTable"] = null;
                txtDoc_Date.Enabled = true;
                btntxtExps.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtCGST_Amt.Enabled = true;
                txtSGST_Amt.Enabled = true;
                txtIGST_Amt.Enabled = true;
                txtNarration.Enabled = true;
                lblExps.Text = string.Empty;
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
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtCGST_Amt.Enabled = false;
                txtSGST_Amt.Enabled = false;
                txtIGST_Amt.Enabled = false;
                txtNarration.Enabled = false;
                btntxtExps.Enabled = false;
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
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = true;
                txtEditDoc_No.Enabled = false;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = true;
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtCGST_Amt.Enabled = true;
                txtSGST_Amt.Enabled = true;
                txtIGST_Amt.Enabled = true;
                txtNarration.Enabled = true;
                btntxtExps.Enabled = true;
                setFocusControl(txtDoc_Date);
            }
            #region Always check this
            #endregion
        }
        catch
        {
        }
    }
    #endregion
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
}