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

public partial class Sugar_Transaction_pgeGroupSetting : System.Web.UI.Page
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
    int fedefaultAccountCode = 0;
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
    Int32 party_pa = 0;
    Int32 supplier_sa = 0;
    Int32 bank_ba = 0;
    Int32 cash_ca = 0;
    Int32 relatives_re = 0;
    Int32 fixedAssets_fi = 0;
    Int32 interestParty_intr = 0;
    Int32 incomeExpenses_inc = 0;
    Int32 trading_tr = 0;
    Int32 mill_mi = 0;
    Int32 transport_trn = 0;
    Int32 broker_br = 0;
    Int32 retailparty_ret = 0;
    Int32 cashRetailParty = 0;
    
    string Auto_voucher = string.Empty;
    #endregion
    #region Head part Declaration
    string createdBy = string.Empty;
    string modifiedBy = string.Empty;
   
    string retValue = string.Empty;
    string strRev = string.Empty;

    int companyCode = 0;
    
    string Head_Update = string.Empty;
    string Head_Insert = string.Empty;
    string Head_Delete = string.Empty;
    string Head_Values = string.Empty;
    string Head_Fields = string.Empty;
    string drpvalue = string.Empty;
    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            con = new SqlConnection(cs);
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead =  "groupSetting";
            tblDetails = tblPrefix + "";
            qryCommon = tblPrefix + "bsgroupmaster";
            qryAccountList = tblPrefix + "qryAccountsList";
            pnlPopup.Style["display"] = "none";
            user = Session["user"].ToString();
            if (!Page.IsPostBack)
            {
                
                hdnfcompanyCode.Value = Session["Company_Code"].ToString();
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

         //etFocusControl(drpDeliveryType);
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

                btntxtparty_pa.Enabled = false;
                btntxtsupplier_sa.Enabled = false;
                btntxtbank_ba.Enabled = false;
                btntxtrelatives_re.Enabled = false;
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

                btntxtparty_pa.Enabled = true;
                btntxtsupplier_sa.Enabled = true;
                btntxtbank_ba.Enabled = true;
                btntxtrelatives_re.Enabled = true;
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

                btntxtparty_pa.Enabled = false;
                btntxtsupplier_sa.Enabled = false;
                btntxtbank_ba.Enabled = false;
                btntxtrelatives_re.Enabled = false;
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

                btntxtparty_pa.Enabled = true;
                btntxtsupplier_sa.Enabled = true;
                btntxtbank_ba.Enabled = true;
                btntxtrelatives_re.Enabled = true;
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
            bool isvalidate = false;
            string qry = "";
            #region validation
            if (hdnfcompanyCode.Value != Session["Company_Code"].ToString())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company Code & current company Code is not same!')", true);
                return;

            }
            //if (txtfixedassets_fi.text != string.empty && txtfixedassets_fi.text != "0")
            //{
            //    isvalidate = false; 
            //}
            //else
            //{
            //    txtfixedassets_fi.text = string.empty;
            //    setfocuscontrol(txtfixedassets_fi);
            //    return;
            //}
            //if (txtmill_mipaymnetdate.text != string.empty && txtmill_mipaymnetdate.text != "0")
            //{
            //    isvalidate = true;
            //}
            //else
            //{
            //    txtmill_mipaymnetdate.text = string.empty;
            //    setfocuscontrol(txtmill_mipaymnetdate);
            //    return;
            //}
            ////if (txtincomeExpenses_inc.Text != string.Empty && txtincomeExpenses_inc.Text != "0")
            ////{
            ////    isvalidate = false;
            ////}
            ////else
            ////{
            ////    txtincomeExpenses_inc.Text = string.Empty;
            ////    setFocusControl(txtincomeExpenses_inc);
            ////    return;
            ////}

            ////if (txtinterestParty_intr.Text != string.Empty && txtinterestParty_intr.Text != "0")
            ////{
            ////    isvalidate = false;
            ////}
            ////else
            ////{
            ////    txtinterestParty_intr.Text = string.Empty;
            ////    setFocusControl(txtinterestParty_intr);
            ////    return;
            ////}
            ////if (txttrading_tr.Text != string.Empty && txttrading_tr.Text != "0")
            ////{
            ////    isvalidate = false;
            ////}
            ////else
            ////{
            ////    txttrading_tr.Text = string.Empty;
            ////    setFocusControl(txttrading_tr);
            ////    return;
            ////}

            ////if (txtmill_mi.Text != string.Empty && txtmill_mi.Text != "0")
            ////{
            ////    isvalidate = false;
            ////}
            ////else
            ////{
            ////    txtmill_mi.Text = string.Empty;
            ////    setFocusControl(txtmill_mi);
            ////    return;
            ////}

            ////if (txttransport_trn.Text != string.Empty && txttransport_trn.Text != "0")
            ////{
            ////    isvalidate = false;
            ////}
            ////else
            ////{
            ////    txttransport_trn.Text = string.Empty;
            ////    setFocusControl(txttransport_trn);
            ////    return;
            ////}

            ////if (txtbroker_br.Text != string.Empty && txtbroker_br.Text != "0")
            ////{
            ////    isvalidate = false;
            ////}
            ////else
            ////{
            ////    txtbroker_br.Text = string.Empty;
            ////    setFocusControl(txtbroker_br);
            ////    return;
            ////}
            ////if (txtretailparty_ret.Text != string.Empty && txtretailparty_ret.Text != "0")
            ////{
            ////    isvalidate = false;
            ////}
            ////else
            ////{
            ////    txtretailparty_ret.Text = string.Empty;
            ////    setFocusControl(txtretailparty_ret);
            ////    return;
            ////}
            ////if (txtcashRetailParty_caret.Text != string.Empty && txtcashRetailParty_caret.Text != "0")
            ////{
            ////    isvalidate = false;
            ////}
            ////else
            ////{
            ////    txtcashRetailParty_caret.Text = string.Empty;
            ////    setFocusControl(txtcashRetailParty_caret);
            ////    return;
            ////}
            #endregion

            #region -Head part declearation

            party_pa = txtparty_pa.Text != string.Empty ? Convert.ToInt32(txtparty_pa.Text) : 0;
          supplier_sa = txtsupplier_sa.Text != string.Empty ? Convert.ToInt32(txtsupplier_sa.Text) : 0;
          bank_ba = txtbank_ba.Text != string.Empty ? Convert.ToInt32(txtbank_ba.Text) : 0;
           cash_ca= txtcash_ca.Text != string.Empty ? Convert.ToInt32(txtcash_ca.Text) : 0;
            relatives_re = txtrelatives_re.Text != string.Empty ? Convert.ToInt32(txtrelatives_re.Text) : 0;
            fixedAssets_fi = txtfixedAssets_fi.Text != string.Empty ? Convert.ToInt32(txtfixedAssets_fi.Text) : 0;
            interestParty_intr = txtinterestParty_intr.Text != string.Empty ? Convert.ToInt32(txtinterestParty_intr.Text) : 0;
            incomeExpenses_inc = txtincomeExpenses_inc.Text != string.Empty ? Convert.ToInt32(txtincomeExpenses_inc.Text) : 0;
            trading_tr = txttrading_tr.Text != string.Empty ? Convert.ToInt32(txttrading_tr.Text) : 0;
            mill_mi = txtmill_mi.Text != string.Empty ? Convert.ToInt32(txtmill_mi.Text) : 0;
            transport_trn = txttransport_trn.Text != string.Empty ? Convert.ToInt32(txttransport_trn.Text) : 0;
            broker_br = txtbroker_br.Text != string.Empty ? Convert.ToInt32(txtbroker_br.Text) : 0;
            retailparty_ret = txtretailparty_ret.Text != string.Empty ? Convert.ToInt32(txtretailparty_ret.Text) : 0;
            cashRetailParty_caret = txtcashRetailParty_caret.Text != string.Empty ? Convert.ToInt32(txtcashRetailParty_caret.Text) : 0;
            //drpvalue = drpDeliveryType.Text;

            hdnfpa.Value = clsCommon.getString("select bsid from NT_1_bsgroupmaster where    group_Code=" + txtparty_pa.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
            hdnfsa.Value = clsCommon.getString("select bsid from NT_1_bsgroupmaster where    group_Code=" + txtsupplier_sa.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
            hdnfba.Value = clsCommon.getString("select bsid from NT_1_bsgroupmaster where    group_Code=" + txtbank_ba.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
            hdnfca.Value = clsCommon.getString("select bsid from NT_1_bsgroupmaster where    group_Code=" + txtcash_ca.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
            hdnfre.Value = clsCommon.getString("select bsid from NT_1_bsgroupmaster where    group_Code=" + txtrelatives_re.Text+ " and Company_Code=" + Session["Company_Code"].ToString() + " ");
            hdnffi.Value = clsCommon.getString("select bsid from NT_1_bsgroupmaster where    group_Code=" + txtfixedAssets_fi.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
            hdnfintr.Value = clsCommon.getString("select bsid from NT_1_bsgroupmaster where    group_Code=" + txtinterestParty_intr.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
            hdnfinc.Value = clsCommon.getString("select bsid from NT_1_bsgroupmaster where    group_Code=" + txtincomeExpenses_inc.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
            hdnftr.Value = clsCommon.getString("select bsid from NT_1_bsgroupmaster where    group_Code=" + txttrading_tr.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
            hdnfmi.Value = clsCommon.getString("select bsid from NT_1_bsgroupmaster where    group_Code=" + txtmill_mi.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
            hdnftrn.Value = clsCommon.getString("select bsid from NT_1_bsgroupmaster where    group_Code=" + txttransport_trn.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
            hdnfbr.Value = clsCommon.getString("select bsid from NT_1_bsgroupmaster where    group_Code=" + txtbroker_br.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
            hdnfret.Value = clsCommon.getString("select bsid from NT_1_bsgroupmaster where    group_Code=" + txtretailparty_ret.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
            hdnfcaret.Value = clsCommon.getString("select bsid from NT_1_bsgroupmaster where    group_Code=" + txtcashRetailParty_caret.Text + " and Company_Code=" + Session["Company_Code"].ToString() + " ");
     
            string retValue = string.Empty;
            string strRev = string.Empty;
            int companyCode = Convert.ToInt32(Session["Company_Code"].ToString());
           
            //int year_Code = Convert.ToInt32(Session["year"].ToString());
            //int Branch_Code = Convert.ToInt32(Session["Branch_Code"].ToString());
            string userinfo = clsGV.userInfo + DateTime.Now.ToString("dd/MM/yyyy:HHmmss");
            string createdBy = Session["user"].ToString();
            string modifiedBy = Session["user"].ToString();
            string Head_Fields = string.Empty;
            string Head_Values = string.Empty;
            string Head_Update = string.Empty;
            int flag = 0;
            string op = string.Empty;
            string returnmaxno = string.Empty;
            #endregion-End of Head part declearation
            string checkyear = clsCommon.getString("select companyCode from groupSetting where companyCode=" + Convert.ToInt32(Session["Company_Code"]).ToString());
            if (checkyear == null || checkyear == string.Empty || checkyear == "0")
            {
                Head_Fields = Head_Fields + "party,";
                Head_Values = Head_Values + "'" + party_pa + "',";
                
                Head_Fields = Head_Fields + "supplier,";
                Head_Values = Head_Values + "'" +supplier_sa+ "',";
                
                Head_Fields = Head_Fields + "bank,";
                Head_Values = Head_Values + "'" + bank_ba + "',";
                
                Head_Fields = Head_Fields + "cash,";
                Head_Values = Head_Values + "'" + cash_ca + "',";
                
                Head_Fields = Head_Fields + "relatives,";
                Head_Values = Head_Values + "'" + relatives_re + "',";
                
                Head_Fields = Head_Fields + "fixedAssets,";
                Head_Values = Head_Values + "'" + fixedAssets_fi + "',";
                
                Head_Fields = Head_Fields + "interestParty,";
                Head_Values = Head_Values + "'" + interestParty_intr + "',";
                
                Head_Fields = Head_Fields + "incomeExpenses,";
                Head_Values = Head_Values + "'" + incomeExpenses_inc + "',";
                
                Head_Fields = Head_Fields + "trading,";
                Head_Values = Head_Values + "'" + trading_tr + "',";
                
                Head_Fields = Head_Fields + "mill,";
                Head_Values = Head_Values + "'" + mill_mi + "',";
                
                Head_Fields = Head_Fields + "transport,";
                Head_Values = Head_Values + "'" + transport_trn + "',";
                
                Head_Fields = Head_Fields + "brokers,";
                Head_Values = Head_Values + "'" + broker_br + "',";
                
                Head_Fields = Head_Fields + "retailparty,";
                Head_Values = Head_Values + "'" + retailparty_ret + "',";
                
                Head_Fields = Head_Fields + "cashRetailParty,";
                Head_Values = Head_Values + "'" + cashRetailParty_caret + "',";
                
                Head_Fields = Head_Fields + "companyCode,";
                Head_Values = Head_Values + "'" + companyCode + "',";
                
                Head_Fields = Head_Fields + "createdBy,";
                Head_Values = Head_Values + "'" + createdBy + "',";
                
                Head_Fields = Head_Fields + "pa,";
                Head_Values = Head_Values + "'" + hdnfpa.Value + "',";
                
                Head_Fields = Head_Fields + "sa,";
                Head_Values = Head_Values + "'" + hdnfsa.Value + "',";
                
                Head_Fields = Head_Fields + "ba,";
                Head_Values = Head_Values + "'" + hdnfba.Value + "',";
                
                Head_Fields = Head_Fields + "ca,";
                Head_Values = Head_Values + "'" + hdnfca.Value + "',";
                
                Head_Fields = Head_Fields + "re,";
                Head_Values = Head_Values + "'" + hdnfre.Value + "',";
                
                Head_Fields = Head_Fields + "fi,";
                Head_Values = Head_Values + "'" + hdnffi.Value + "',";
                
                Head_Fields = Head_Fields + "intr,";
                Head_Values = Head_Values + "'" + hdnfintr.Value + "',";
                
                Head_Fields = Head_Fields + "inc,";
                Head_Values = Head_Values + "'" + hdnfinc.Value + "',";

                Head_Fields = Head_Fields + "tr,";
                Head_Values = Head_Values + "'" + hdnftr.Value + "',";

                Head_Fields = Head_Fields + "mi,";
                Head_Values = Head_Values + "'" + hdnfmi.Value + "',";

                Head_Fields = Head_Fields + "trn,";
                Head_Values = Head_Values + "'" + hdnftrn.Value + "',";

                Head_Fields = Head_Fields + "br,";
                Head_Values = Head_Values + "'" + hdnfbr.Value + "',";

                Head_Fields = Head_Fields + "ret,";
                Head_Values = Head_Values + "'" + hdnfret.Value + "',";

                Head_Fields = Head_Fields + "caret";
                Head_Values = Head_Values + "'" + hdnfcaret.Value + "'";
                
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
                string s = clsCommon.getString("select companyCode from " + tblHead + " where companyCode=" + Convert.ToInt32(Session["Company_Code"]).ToString());
                if (s != string.Empty)
                {


                    #region Create Update Query
                    
                    Head_Update = Head_Update + "party=";
                    Head_Update = Head_Update + "'" + party_pa + "',";

                    Head_Update = Head_Update + "supplier=";
                    Head_Update = Head_Update + "'" + supplier_sa + "',";
                    
                    Head_Update = Head_Update + "bank=";
                    Head_Update = Head_Update + "'" + bank_ba + "',";
                    
                    Head_Update = Head_Update + "cash=";
                    Head_Update = Head_Update + "'" + cash_ca + "',";
                    
                    Head_Update = Head_Update + "relatives=";
                    Head_Update = Head_Update + "'" + relatives_re + "',";
                    
                    Head_Update = Head_Update + "fixedAssets=";
                    Head_Update = Head_Update + "'" + fixedAssets_fi + "',";
                    
                    Head_Update = Head_Update + "interestParty=";
                    Head_Update = Head_Update + "'" + interestParty_intr + "',";
                    
                    Head_Update = Head_Update + "incomeExpenses=";
                    Head_Update = Head_Update + "'" + incomeExpenses_inc + "',";
                    
                    Head_Update = Head_Update + "trading=";
                    Head_Update = Head_Update + "'" + trading_tr + "',";
                    
                    Head_Update = Head_Update + "mill=";
                    Head_Update = Head_Update + "'" + mill_mi + "',";
                    
                    Head_Update = Head_Update + "transport=";
                    Head_Update = Head_Update + "'" + transport_trn + "',";
                    
                    Head_Update = Head_Update + "brokers=";
                    Head_Update = Head_Update + "'" + broker_br + "',";
                    
                    Head_Update = Head_Update + "retailparty=";
                    Head_Update = Head_Update + "'" + retailparty_ret + "',";
                    
                    Head_Update = Head_Update + "cashRetailParty=";
                    Head_Update = Head_Update + "'" + cashRetailParty_caret + "',";                   
                    
                    Head_Update = Head_Update + "modifiedBy=";
                    Head_Update = Head_Update + "'" + modifiedBy + "',";
                    
                    Head_Update = Head_Update + "pa=";
                    Head_Update = Head_Update + "'" + hdnfpa.Value + "',";
                    
                    Head_Update = Head_Update + "sa=";
                    Head_Update = Head_Update + "'" + hdnfsa.Value + "',";
                    
                    Head_Update = Head_Update + "ba=";
                    Head_Update = Head_Update + "'" + hdnfba.Value + "',";
                    
                    Head_Update = Head_Update + "ca=";
                    Head_Update = Head_Update + "'" + hdnfca.Value + "',";
                    
                    Head_Update = Head_Update + "re=";
                    Head_Update = Head_Update + "'" + hdnfre.Value + "',";
                    
                    Head_Update = Head_Update + "fi=";
                    Head_Update = Head_Update + "'" + hdnffi.Value + "',";
                    
                    Head_Update = Head_Update + "intr=";
                    Head_Update = Head_Update + "'" + hdnfintr.Value + "',";
                    
                    Head_Update = Head_Update + "inc=";
                    Head_Update = Head_Update + "'" + hdnfinc.Value + "',";
                    
                    Head_Update = Head_Update + "tr=";
                    Head_Update = Head_Update + "'" + hdnftr.Value + "',";
                    
                    Head_Update = Head_Update + "mi=";
                    Head_Update = Head_Update + "'" + hdnfmi.Value + "',";
                    
                    Head_Update = Head_Update + "trn=";
                    Head_Update = Head_Update + "'" + hdnftrn.Value + "',";
                    
                    Head_Update = Head_Update + "br=";
                    Head_Update = Head_Update + "'" + hdnfbr.Value + "',";
                    
                    Head_Update = Head_Update + "ret=";
                    Head_Update = Head_Update + "'" + hdnfret.Value + "',";
                    
                    Head_Update = Head_Update + "caret=";
                    Head_Update = Head_Update + "'" + hdnfcaret.Value + "'";
                
                    #endregion
                }
                flag = 2;
                qry = "update " + tblHead + " set " + Head_Update + " where companyCode='" + companyCode + "'";
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
                        string comcode = Session["Company_Code"].ToString();
                        if (hdnfcompanyCode.Value != comcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records companyCode & current companyCode is not same !')", true);
                            return false;
                        }
                //        txtmill_mipaymnetdate.Text = dt.Rows[0]["Mill_Payment_date"].ToString();
                  //      drpDeliveryType.SelectedValue = dt.Rows[0]["dispatchType"].ToString();

                    //    hdnfcompanyCode.Value = dt.Rows[0]["Company_Code"].ToString();
                        txtparty_pa.Text = dt.Rows[0]["party"].ToString();
                        lblparty_pa.Text = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtparty_pa.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        txtsupplier_sa.Text = dt.Rows[0]["supplier"].ToString();
                        lblsupplier_sa.Text = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtsupplier_sa.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        txtbank_ba.Text = dt.Rows[0]["bank"].ToString();
                        lblbank_ba.Text = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtbank_ba.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        txtcash_ca.Text = dt.Rows[0]["cash"].ToString();
                        lblcash_ca.Text = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtcash_ca.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        txtrelatives_re.Text = dt.Rows[0]["relatives"].ToString();
                        lblrelatives_re.Text = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtrelatives_re.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                         txtfixedAssets_fi.Text = dt.Rows[0]["fixedAssets"].ToString();
                        lblfixedAssets_fi.Text = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtfixedAssets_fi.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                         txtinterestParty_intr.Text = dt.Rows[0]["interestParty"].ToString();
                        lblinterestParty_intr.Text = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtinterestParty_intr.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        txtincomeExpenses_inc.Text = dt.Rows[0]["incomeExpenses"].ToString();
                        lblincomeExpenses_inc.Text = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtincomeExpenses_inc.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        txttrading_tr.Text = dt.Rows[0]["trading"].ToString();
                        lbltrading_tr.Text = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txttrading_tr.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        txtmill_mi.Text = dt.Rows[0]["mill"].ToString();
                        lblmill_mi.Text = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtmill_mi.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        txttransport_trn.Text = dt.Rows[0]["transport"].ToString();
                        lbltransport_trn.Text = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txttransport_trn.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        txtbroker_br.Text = dt.Rows[0]["brokers"].ToString();
                        lblbroker_br.Text = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtbroker_br.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                         txtretailparty_ret.Text = dt.Rows[0]["retailparty"].ToString();
                         lblretailparty_ret.Text = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtretailparty_ret.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                        txtcashRetailParty_caret.Text = dt.Rows[0]["cashRetailParty"].ToString();
                        lblcashRetailParty_caret.Text = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtcashRetailParty_caret.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                      
                         Label lblCreated = (Label)Master.FindControl("MasterlblcreatedBy");
                        Label lblModified = (Label)Master.FindControl("MasterlblmodifiedBy");
                        if (lblCreated != null)
                            
                        {
                            lblCreated.Text = "createdBy: " + dt.Rows[0]["createdBy"].ToString();
                        }
                        if (lblModified != null)
                        {
                            lblModified.Text = "modifiedBy: " + dt.Rows[0]["modifiedBy"].ToString();
                        }
                       
                        foreach (System.Web.UI.Control c in pnlMain.Controls)
                        {
                            if (c is System.Web.UI.WebControls.TextBox)
                            {
                                string name = c.ClientID;
                                string newStr = name.Remove(0, 20);
                                newStr = newStr + ".Text";
                                string textname = ((System.Web.UI.WebControls.TextBox)c).Text;
                                string acname = clsCommon.getString("select group_Name_E from nt_1_bsgroupmaster where group_Code=" + textname + "");
                                if (newStr == "txtparty_pa")
                                {
                                    lblparty_pa.Text = acname;
                                }
                                if (newStr == "txtsupplier_sa")
                                {
                                    lblsupplier_sa.Text = acname;
                                }
                                if (newStr == "txtbank_ba")
                                {
                                    lblbank_ba.Text = acname;
                                }
                                if (newStr == "txtcash_ca")
                                {
                                    lblcash_ca.Text = acname;
                                }
                                if (newStr == "txtrelatives_re")
                                {
                                    lblrelatives_re.Text = acname;
                                }
                                if (newStr == "txtfixedAssets_fi")
                                {
                                    lblfixedAssets_fi.Text = acname;
                                }
                                if (newStr == "txtinterestParty_intr")
                                {
                                    lblinterestParty_intr.Text = acname;
                                }
                                if (newStr == "txtincomeExpenses_inc")
                                {
                                    lblincomeExpenses_inc.Text = acname;
                                }
                                if (newStr == "txttrading_tr")
                                {
                                    lbltrading_tr.Text = acname;
                                }
                                if (newStr == "txtmill_mi")
                                {
                                    lblmill_mi.Text = acname;
                                }
                                if (newStr == "txttransport_trn")
                                {
                                    lbltransport_trn.Text = acname;
                                }
                                if (newStr == "txtbroker_br")
                                {
                                    lblbroker_br.Text = acname;
                                }
                                if (newStr == "txtretailparty_ret")
                                {
                                    lblretailparty_ret.Text = acname;
                                }
                                if (newStr == "txtcashRetailParty_caret")
                                {
                                    lblcashRetailParty_caret.Text = acname;
                                }
                               
                            }

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

    protected void txtcashRetailParty_caret_TextChanged(object sender, EventArgs e)
    {
        searchString = txtcashRetailParty_caret.Text;
        strTextBox = "txtcashRetailParty_caret";
        csCalculations();

    }
    //protected void txtmill_mipaymnetdate_TextChanged(object sender, EventArgs e)
    //{
    //    searchString = txtmill_mipaymnetdate.Text;
    //    strTextBox = "txtmill_mipaymnetdate";
    //    csCalculations();

    //}
    protected void btntxtcashRetailParty_caret_Click(object sender, EventArgs e)
    {
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtcashRetailParty_caret";
            btnSearch_Click(sender, e);
        }
         
        
           
        
    }
 
    
  
    //protected void drpDeliveryType_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = "select * from " + tblHead + " where companyCode=" + Convert.ToInt32(Session["Company_Code"].ToString());
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
            if (v != "txtfixedAssets_fi")
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

    //protected void txtRoundOff_TextChanged(object sender, EventArgs e)
    //{
    //    searchString = txtRoundOff.Text;
    //    strTextBox = "txtRoundOff";
    //    csCalculations();
    //}
    //protected void txtbtnRoundOff_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        pnlPopup.Style["display"] = "block";
    //        hdnfClosePopup.Value = "txtRoundOff";
    //        btnSearch_Click(sender, e);
    //    }
    //    catch
    //    {
    //    }
    //}

    #region [txtparty_pa_TextChanged]
    protected void txtparty_pa_TextChanged(object sender, EventArgs e)
    {
        searchString = txtparty_pa.Text;
        strTextBox = "txtparty_pa";
        csCalculations();
    }
    #endregion

    #region [btntxtparty_pa_Click]
    protected void btntxtparty_pa_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnfpopup.Value == "0")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtparty_pa";
                btnSearch_Click(sender, e);
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [txtsupplier_sa_TextChanged]
    protected void txtsupplier_sa_TextChanged(object sender, EventArgs e)
    {
        searchString = txtsupplier_sa.Text;
        strTextBox = "txtsupplier_sa";
        csCalculations();
    }
    #endregion

    #region [btntxtsupplier_sa_Click]
    protected void btntxtsupplier_sa_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtsupplier_sa";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtbank_ba_TextChanged]
    protected void txtbank_ba_TextChanged(object sender, EventArgs e)
    {
        searchString = txtbank_ba.Text;
        strTextBox = "txtbank_ba";
        csCalculations();
    }
    #endregion

    #region [btntxtbank_ba_Click]
    protected void btntxtbank_ba_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtbank_ba";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtcash_ca_TextChanged]
    protected void txtcash_ca_TextChanged(object sender, EventArgs e)
    {
        searchString = txtcash_ca.Text;
        strTextBox = "txtcash_ca";
        csCalculations();
    }
    #endregion

    #region [btntxtcash_ca_Click]
    protected void btntxtcash_ca_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtcash_ca";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtrelatives_re_TextChanged]
    protected void txtrelatives_re_TextChanged(object sender, EventArgs e)
    {
        searchString = txtrelatives_re.Text;
        strTextBox = "txtrelatives_re";
        csCalculations();
    }
    #endregion

    #region [btntxtrelatives_re_Click]
    protected void btntxtrelatives_re_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtrelatives_re";
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
          //  pnlPopup.Style["display"] = "block";
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

            if (hdnfClosePopup.Value == "txtparty_pa")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( group_Code like '%" + aa + "%' or group_Name_E like '%" + aa + "%' or group_Type like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select party_pa--";
                string qry = "select group_Code,group_Name_E,group_Type from " + qryCommon
                    + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                this.showPopup(qry);
            }

            //if (hdnfClosePopup.Value == "txtPurchasetdsac")
            //{
            //    foreach (var s in split)
            //    {
            //        string aa = s.ToString();

            //        name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or cityname like '%" + aa + "%') and";
            //    }
            //    name = name.Remove(name.Length - 3);
            //    lblPopupHead.Text = "--Select Commission AC--";
            //    string qry = "select Ac_Code,Ac_Name_E,cityname from " + qryCommon
            //        + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
            //    this.showPopup(qry);
            //}

            //if (hdnfClosePopup.Value == "txtSaletdsAc")
            //{
            //    foreach (var s in split)
            //    {
            //        string aa = s.ToString();

            //        name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or cityname like '%" + aa + "%') and";
            //    }
            //    name = name.Remove(name.Length - 3);
            //    lblPopupHead.Text = "--Select Commission AC--";
            //    string qry = "select Ac_Code,Ac_Name_E,cityname from " + qryCommon
            //        + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
            //    this.showPopup(qry);
            //}
            if (hdnfClosePopup.Value == "txttransport_trn")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( group_Code like '%" + aa + "%' or group_Name_E like '%" + aa + "%' or group_Type like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Commission AC--";
                string qry = "select group_Code,group_Name_E,group_Type from " + qryCommon
                    + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txttransport_trn")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( group_Code like '%" + aa + "%' or group_Name_E like '%" + aa + "%' or group_Type like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Commission AC--";
                string qry = "select group_Code,group_Name_E,group_Type from " + qryCommon
                    + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                this.showPopup(qry);
            }
            //if (hdnfClosePopup.Value == "txtFreight_Ac")
            //{
            //    foreach (var s in split)
            //    {
            //        string aa = s.ToString();

            //        name += "( State_Code like '%" + aa + "%' or State_Name like '%" + aa + "%') or";
            //    }
            //    name = name.Remove(name.Length - 3);
            //    lblPopupHead.Text = "--Select Interest AC--";
            //    string qry = "Select State_Code,State_Name from txtFreight_Ac where " + qryCommon
            //        + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
            //    this.showPopup(qry);
            //}

            if (hdnfClosePopup.Value == "txtsupplier_sa")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( group_Code like '%" + aa + "%' or group_Name_E like '%" + aa + "%' or group_Type like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Interest AC--";
                string qry = "select group_Code,group_Name_E,group_Type from " + qryCommon
                    + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtbank_ba")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( group_Code like '%" + aa + "%' or group_Name_E like '%" + aa + "%' or group_Type like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Transport AC--";
                string qry = "select group_Code,group_Name_E,group_Type from " + qryCommon
                    + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtcash_ca")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( group_Code like '%" + aa + "%' or group_Name_E like '%" + aa + "%' or group_Type like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Postage AC--";
                string qry = "select group_Code,group_Name_E,group_Type from " + qryCommon
                    + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtrelatives_re")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( group_Code like '%" + aa + "%' or group_Name_E like '%" + aa + "%' or group_Type like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select Self AC--";
                string qry = "select group_Code,group_Name_E,group_Type from " + qryCommon
                    + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtfixedAssets_fi")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( group_Code like '%" + aa + "%' or group_Name_E like '%" + aa + "%' or group_Type like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 2);
                string qry = "select group_Code,group_Name_E,group_Type from " + qryCommon   + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtparty_pa" || hdnfClosePopup.Value == "txtsupplier_sa" || hdnfClosePopup.Value == "txtbank_ba" || hdnfClosePopup.Value == "txtcash_ca"||
          hdnfClosePopup.Value == "txtbank_ba" || hdnfClosePopup.Value == "txtinterestParty_intr" || hdnfClosePopup.Value == "txtincomeExpenses_inc" || hdnfClosePopup.Value == "txttrading_tr"
                || hdnfClosePopup.Value == "txtmill_mi" || hdnfClosePopup.Value == "txttransport_trn" || hdnfClosePopup.Value == "txtbroker_br" || hdnfClosePopup.Value == "txtretailparty_ret"
                || hdnfClosePopup.Value == "txtcashRetailParty_caret")
            {
                foreach (var s in split)
                {
                    string aa = s.ToString();

                    name += "( group_Code like '%" + aa + "%' or group_Name_E like '%" + aa + "%' or group_Type like '%" + aa + "%') and";
                }
                name = name.Remove(name.Length - 3);
                lblPopupHead.Text = "--Select GST Account--";
                string qry = "select group_Code,group_Name_E,group_Type from " + qryCommon
                    + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
                this.showPopup(qry);
            }
          ////if (hdnfClosePopup.Value == "txtPurchaseTCSAc")
          //  {
          //      foreach (var s in split)
          //      {
          //          string aa = s.ToString();

          //          name += "( group_Code like '%" + aa + "%' or group_Name_E like '%" + aa + "%' or group_Type like '%" + aa + "%') and";
          //      }
          //      name = name.Remove(name.Length - 3);
          //      lblPopupHead.Text = "--Select PurchaseTCS AC--";
          //      string qry = "select group_Code,group_Name_E,group_Type from " + qryCommon
          //          + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
          //      this.showPopup(qry);
          //  }
          //  if (hdnfClosePopup.Value == "txtSaleTCSAc")
          //  {
          //      foreach (var s in split)
          //      {
          //          string aa = s.ToString();

          //          name += "( group_Code like '%" + aa + "%' or group_Name_E like '%" + aa + "%' or group_Type like '%" + aa + "%') and";
          //      }
          //      name = name.Remove(name.Length - 3);
          //      lblPopupHead.Text = "--Select SaleTCS AC--";
          //      string qry = "select group_Code,group_Name_E,group_Type from " + qryCommon
          //          + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
          //      this.showPopup(qry);
          //  }
          //  if (hdnfClosePopup.Value == "txthamali" || hdnfClosePopup.Value == "txtPacking" || hdnfClosePopup.Value == "txtSupercost" || hdnfClosePopup.Value == "txtMarketSase" || hdnfClosePopup.Value == "txtother_amount_ac")
          //  {
          //      foreach (var s in split)
          //      {
          //          string aa = s.ToString();

          //          name += "( group_Code like '%" + aa + "%' or group_Name_E like '%" + aa + "%' or group_Type like '%" + aa + "%') and";
          //      }
          //      name = name.Remove(name.Length - 3);
          //      lblPopupHead.Text = "--Select  AC--";
          //      string qry = "select group_Code,group_Name_E,group_Type from " + qryCommon
          //          + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
          //      this.showPopup(qry);
          //  }

          //  if (hdnfClosePopup.Value == "txtReturnSaleCGST" || hdnfClosePopup.Value == "txtReturnSaleSGST" || hdnfClosePopup.Value == "txtReturnSaleIGST" || hdnfClosePopup.Value == "txtReturnPurchaseCGST" || hdnfClosePopup.Value == "txtReturnPurchaseSGST" || hdnfClosePopup.Value == "txtReturnPurchaseIGST")
          //  {
          //      foreach (var s in split)
          //      {
          //          string aa = s.ToString();

          //          name += "( group_Code like '%" + aa + "%' or group_Name_E like '%" + aa + "%' or group_Type like '%" + aa + "%') and";
          //      }
          //      name = name.Remove(name.Length - 3);
          //      lblPopupHead.Text = "--Select  AC--";
          //      string qry = "select group_Code,group_Name_E,group_Type from " + qryCommon
          //          + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " + name;
          //      this.showPopup(qry);
          //  }
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

            if (hdnfClosePopup.Value == "txtparty_pa")
            {
                setFocusControl(txtparty_pa);
            }

            if (hdnfClosePopup.Value == "txtsupplier_sa")
            {
                setFocusControl(txtsupplier_sa);
            }
            if (hdnfClosePopup.Value == "txtbank_ba")
            {
                setFocusControl(txtbank_ba);
            }
            if (hdnfClosePopup.Value == "txtcash_ca")
            {
                setFocusControl(txtcash_ca);
            }
            if (hdnfClosePopup.Value == "txtrelatives_re")
            {
                setFocusControl(txtrelatives_re);
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
            hdnfpopup.Value = null;

           //f (strTextBox == "txtSaletdsAc")
          //{
            //    if (txtSaletdsAc.Text != string.Empty)
            //    {
            //        string str = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code=" + txtSaletdsAc.Text + "");
            //        if (str != string.Empty && str != "0"=
            //        {
            //            lblsaletdsacname.Text = str;

            //            setFocusControl(txtSaletdsAc);
            //        }
            //        else
            //        {
            //            lblsaletdsacname.Text = string.Empty;
            //            txtSaletdsAc.Text = string.Empty;
            //            setFocusControl(txtSaletdsAc);
            //        }
            //    }
            //    else
            //    {
            //        lblsaletdsacname.Text = string.Empty;
            //        txtSaletdsAc.Text = string.Empty;
            //        setFocusControl(txtSaletdsAc);
            //    }
            //}
            //if (strTextBox == "txtPurchasetdsac")
            //{
            //    if (txtPurchasetdsac.Text != string.Empty)
            //    {
            //        string str = clsCommon.getString("select Ac_Name_E from " + qryCommon + " where Ac_Code=" + txtPurchasetdsac.Text + "");
            //        if (str != string.Empty && str != "0")
            //        {
            //            lblpurchasetdsname.Text = str;

            //            setFocusControl(txtPurchasetdsac);
            //        }
            //        else
            //        {
            //            lblpurchasetdsname.Text = string.Empty;
            //            txtPurchasetdsac.Text = string.Empty;
            //            setFocusControl(txtPurchasetdsac);
            //        }
            //    }
            //    else
            //    {
            //        lblpurchasetdsname.Text = string.Empty;
            //        txtPurchasetdsac.Text = string.Empty;
            //        setFocusControl(txtPurchasetdsac);
            //    }
            //}
            if (strTextBox == "txtparty_pa")
            {
                if (txtparty_pa.Text != string.Empty)
                {
                    //hdnfpartycode.Value = clsCommon.getString("select bsid from nt_1_bsgroupmaster where group_Code=" + txtparty_pa.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                    
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtparty_pa.Text + "");
                    if (str != string.Empty && str != "0")
                    {
                        lblparty_pa.Text = str;

                        setFocusControl(txtsupplier_sa);
                    }
                    else
                    {
                        lblparty_pa.Text = string.Empty;
                        txtparty_pa.Text = string.Empty;
                        setFocusControl(txtsupplier_sa);
                    }
                }
                else
                {
                    lblparty_pa.Text = string.Empty;
                    txtparty_pa.Text = string.Empty;
                    setFocusControl(txtsupplier_sa);
                }
            }
            if (strTextBox == "txttransport_trn")
            {
                if (txttransport_trn.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txttransport_trn.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                    if (str != string.Empty && str != "0")
                    {
                        lbltransport_trn.Text = str;

                        setFocusControl(txtcashRetailParty_caret);
                    }
                    else
                    {
                        lbltransport_trn.Text = string.Empty;
                        txttransport_trn.Text = string.Empty;
                        setFocusControl(txttransport_trn);
                    }
                }
                else
                {
                    lbltransport_trn.Text = string.Empty;
                    txttransport_trn.Text = string.Empty;
                    setFocusControl(txttransport_trn);
                }
            }
            if (strTextBox == "txttransport_trn")
            {
                if (txttransport_trn.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txttransport_trn.Text + " and Company_Code=" + Session["Company_Code"].ToString() + "");
                    if (str != string.Empty && str != "0")
                    {
                        lbltransport_trn.Text = str;

                        setFocusControl(btnUpdate);
                    }
                    else
                    {
                        lbltransport_trn.Text = string.Empty;
                        txttransport_trn.Text = string.Empty;
                        setFocusControl(txttransport_trn);
                    }
                }
                else
                {
                    lbltransport_trn.Text = string.Empty;
                    txttransport_trn.Text = string.Empty;
                    setFocusControl(txttransport_trn);
                }
            }
            if (strTextBox == "txtsupplier_sa")
            {
                if (txtsupplier_sa.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtsupplier_sa.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblsupplier_sa.Text = str;
                        setFocusControl(txtbank_ba);
                    }
                    else
                    {
                        lblsupplier_sa.Text = string.Empty;
                        txtsupplier_sa.Text = string.Empty;
                        setFocusControl(txtbank_ba);
                    }
                }
                else
                {
                    lblsupplier_sa.Text = string.Empty;
                    txtsupplier_sa.Text = string.Empty;
                    setFocusControl(txtbank_ba);
                }
            }
            if (strTextBox == "txtbank_ba")
            {
                if (txtbank_ba.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtbank_ba.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblbank_ba.Text = str;
                        setFocusControl(txtcash_ca);
                    }
                    else
                    {
                        lblbank_ba.Text = string.Empty;
                        txtbank_ba.Text = string.Empty;
                        setFocusControl(txtcash_ca);
                    }
                }
                else
                {
                    lblbank_ba.Text = string.Empty;
                    txtbank_ba.Text = string.Empty;
                    setFocusControl(txtcash_ca);
                }
            }

            if (strTextBox == "txtcash_ca")
            {
                if (txtbank_ba.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtcash_ca.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblcash_ca.Text = str;
                        setFocusControl(txtrelatives_re);
                    }
                    else
                    {
                        lblcash_ca.Text = string.Empty;
                        txtcash_ca.Text = string.Empty;
                        setFocusControl(txtrelatives_re);
                    }
                }
                else
                {
                    lblcash_ca.Text = string.Empty;
                    txtcash_ca.Text = string.Empty;
                    setFocusControl(txtrelatives_re);
                }
            }


            //if (strTextBox == "txtcash_ca")
            //{
            //    if (txtcash_ca.Text != string.Empty)
            //    {
            //        string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtcash_ca.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //        if (str != string.Empty && str != "0")
            //        {
            //            lblcash_ca.Text = str;
            //            setFocusControl(txtrelatives_re);
            //        }
            //        else
            //        {
            //            lblcash_ca.Text = string.Empty;
            //            txtcash_ca.Text = string.Empty;
            //            setFocusControl(txtrelatives_re);
            //        }
            //    }
            //    else
            //    {
            //        lblcash_ca.Text = string.Empty;
            //        txtcash_ca.Text = string.Empty;
            //        setFocusControl(txtrelatives_re);
            //    }
            //}

            if (strTextBox == "txtrelatives_re")
            {
                if (txtrelatives_re.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtrelatives_re.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblrelatives_re.Text = str;
                        setFocusControl(txtfixedAssets_fi);
                    }
                    else
                    {
                        lblrelatives_re.Text = string.Empty;
                        txtrelatives_re.Text = string.Empty;
                        setFocusControl(txtfixedAssets_fi);
                    }
                }
                else
                {
                    lblrelatives_re.Text = string.Empty;
                    txtrelatives_re.Text = string.Empty;
                    setFocusControl(txtfixedAssets_fi);
                }
            }

            if (strTextBox == "txtfixedAssets_fi")
            {
                if (txtfixedAssets_fi.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtfixedAssets_fi.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblfixedAssets_fi.Text = str;
                        setFocusControl(txtinterestParty_intr);
                    }
                    else
                    {
                        lblfixedAssets_fi.Text = string.Empty;
                        txtfixedAssets_fi.Text = string.Empty;
                        setFocusControl(txtinterestParty_intr);
                    }
                }
                else
                {
                    lblfixedAssets_fi.Text = string.Empty;
                    txtfixedAssets_fi.Text = string.Empty;
                    setFocusControl(txtinterestParty_intr);
                }
            }


            if (strTextBox == "txtinterestParty_intr")
            {
                if (txtinterestParty_intr.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtinterestParty_intr.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblinterestParty_intr.Text = str;
                        setFocusControl(txtincomeExpenses_inc);
                    }
                    else
                    {
                        lblinterestParty_intr.Text = string.Empty;
                        txtinterestParty_intr.Text = string.Empty;
                        setFocusControl(txtincomeExpenses_inc);
                    }
                }
                else
                {
                    lblinterestParty_intr.Text = string.Empty;
                    txtinterestParty_intr.Text = string.Empty;
                    setFocusControl(txtincomeExpenses_inc);
                }
            }
            if (strTextBox == "txtincomeExpenses_inc")
            {
                if (txtincomeExpenses_inc.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtincomeExpenses_inc.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblincomeExpenses_inc.Text = str;
                        setFocusControl(txttrading_tr);
                    }
                    else
                    {
                        lblincomeExpenses_inc.Text = string.Empty;
                        txtincomeExpenses_inc.Text = string.Empty;
                        setFocusControl(txttrading_tr);
                    }
                }
                else
                {
                    lblincomeExpenses_inc.Text = string.Empty;
                    txtincomeExpenses_inc.Text = string.Empty;
                    setFocusControl(txttrading_tr);
                }
            }
            if (strTextBox == "txttrading_tr")
            {
                if (txttrading_tr.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txttrading_tr.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lbltrading_tr.Text = str;
                        setFocusControl(txtmill_mi);
                    }
                    else
                    {
                        lbltrading_tr.Text = string.Empty;
                        txttrading_tr.Text = string.Empty;
                        setFocusControl(txtmill_mi);
                    }
                }
                else
                {
                    lbltrading_tr.Text = string.Empty;
                    txttrading_tr.Text = string.Empty;
                    setFocusControl(txtmill_mi);
                }
            }

            if (strTextBox == "txtmill_mi")
            {
                if (txtmill_mi.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtmill_mi.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblmill_mi.Text = str;
                        setFocusControl(txttransport_trn);
                    }
                    else
                    {
                        lblmill_mi.Text = string.Empty;
                        txtmill_mi.Text = string.Empty;
                        setFocusControl(txttransport_trn);
                    }
                }
                else
                {
                    lblmill_mi.Text = string.Empty;
                    txtmill_mi.Text = string.Empty;
                    setFocusControl(txttransport_trn);
                }
            }

            if (strTextBox == "txttransport_trn")
            {
                if (txttransport_trn.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txttransport_trn.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lbltransport_trn.Text = str;
                        setFocusControl(txtbroker_br);
                    }
                    else
                    {
                        lbltransport_trn.Text = string.Empty;
                        txttransport_trn.Text = string.Empty;
                        setFocusControl(txtbroker_br);
                    }
                }
                else
                {
                    lbltransport_trn.Text = string.Empty;
                    txttransport_trn.Text = string.Empty;
                    setFocusControl(txtbroker_br);
                }
            }

            if (strTextBox == "txtbroker_br")
            {
                if (txtbroker_br.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtbroker_br.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblbroker_br.Text = str;
                        setFocusControl(txtretailparty_ret);
                    }
                    else
                    {
                        lblbroker_br.Text = string.Empty;
                        txtbroker_br.Text = string.Empty;
                        setFocusControl(txtretailparty_ret);
                    }
                }
                else
                {
                    lblbroker_br.Text = string.Empty;
                    txtbroker_br.Text = string.Empty;
                    setFocusControl(txtretailparty_ret);
                }
            }

            if (strTextBox == "txtretailparty_ret")
            {
                if (txtretailparty_ret.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtretailparty_ret.Text + " and Company_Code="
                        + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblretailparty_ret.Text = str;
                        setFocusControl(txtcashRetailParty_caret);
                    }
                    else
                    {
                        lblretailparty_ret.Text = string.Empty;
                        txtretailparty_ret.Text = string.Empty;
                        setFocusControl(txtcashRetailParty_caret);
                    }
                }
                else
                {
                    lblretailparty_ret.Text = string.Empty;
                    txtretailparty_ret.Text = string.Empty;
                    setFocusControl(txtcashRetailParty_caret);
                }
            }

            if (strTextBox == "txtcashRetailParty_caret")
            {
                if (txtcashRetailParty_caret.Text != string.Empty)
                {
                    string str = clsCommon.getString("select group_Name_E from " + qryCommon + " where group_Code=" + txtcashRetailParty_caret.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (str != string.Empty && str != "0")
                    {
                        lblcashRetailParty_caret.Text = str;
                        setFocusControl(btnUpdate);
                    }
                    else
                    {
                        lblcashRetailParty_caret.Text = string.Empty;
                        txtcashRetailParty_caret.Text = string.Empty;
                        setFocusControl(btnUpdate);
                    }
                }
                else
                {
                    lblcashRetailParty_caret.Text = string.Empty;
                    txtcashRetailParty_caret.Text = string.Empty;
                    setFocusControl(btnUpdate);
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

    #region [txtfixedAssets_fi_TextChanged]
    protected void txtfixedAssets_fi_TextChanged(object sender, EventArgs e)
    {
        searchString = txtfixedAssets_fi.Text;
        strTextBox = "txtfixedAssets_fi";
        csCalculations();
    }
    #endregion

    #region [btntxtfixedAssets_fi_Click]
    protected void btntxtfixedAssets_fi_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtfixedAssets_fi";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtinterestParty_intr_TextChanged]
    protected void txtinterestParty_intr_TextChanged(object sender, EventArgs e)
    {
        searchString = txtinterestParty_intr.Text;
        strTextBox = "txtinterestParty_intr";
        csCalculations();
    }
    #endregion

    #region [btntxtinterestParty_intr_Click]
    protected void btntxtinterestParty_intr_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtinterestParty_intr";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtincomeExpenses_inc_TextChanged]
    protected void txtincomeExpenses_inc_TextChanged(object sender, EventArgs e)
    {
        searchString = txtincomeExpenses_inc.Text;
        strTextBox = "txtincomeExpenses_inc";
        csCalculations();
    }
    #endregion

    #region [btntxtincomeExpenses_inc_Click]
    protected void btntxtincomeExpenses_inc_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtincomeExpenses_inc";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txttrading_tr_TextChanged]
    protected void txttrading_tr_TextChanged(object sender, EventArgs e)
    {
        searchString = txttrading_tr.Text;
        strTextBox = "txttrading_tr";
        csCalculations();
    }
    #endregion

    #region [btntxttrading_tr_Click]
    protected void btntxttrading_tr_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txttrading_tr";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtmill_mi_TextChanged]
    protected void txtmill_mi_TextChanged(object sender, EventArgs e)
    {
        searchString = txtmill_mi.Text;
        strTextBox = "txtmill_mi";
        csCalculations();
    }
    #endregion

    #region [btntxtmill_mi_Click]
    protected void btntxtmill_mi_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtmill_mi";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txttransport_trn_TextChanged]
    protected void txttransport_trn_TextChanged(object sender, EventArgs e)
    {
        searchString = txttransport_trn.Text;
        strTextBox = "txttransport_trn";
        csCalculations();
    }
    #endregion

    #region [btntxttransport_trn_Click]
    protected void btntxttransport_trn_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txttransport_trn";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtbroker_br_TextChanged]
    protected void txtbroker_br_TextChanged(object sender, EventArgs e)
    {
        searchString = txtbroker_br.Text;
        strTextBox = "txtbroker_br";
        csCalculations();
    }
    #endregion

    #region [btntxtbroker_br_Click]
    protected void btntxtbroker_br_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtbroker_br";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtretailparty_ret_TextChanged]
    protected void txtretailparty_ret_TextChanged(object sender, EventArgs e)
    {
        searchString = txtretailparty_ret.Text;
        strTextBox = "txtretailparty_ret";
        csCalculations();
    }
    #endregion

    #region [btntxtretailparty_ret_Click]
    protected void btntxtretailparty_ret_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtretailparty_ret";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    public int cashRetailParty_caret { get; set; }

}

  
