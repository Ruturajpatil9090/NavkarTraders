using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Net.Mime;
public partial class Report_rptNewDispatchRegister : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string qryCommon = string.Empty;
    string trnType = "DO";
    string GLedgerTable = string.Empty;
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string partyEmail = string.Empty;
    string f = "../GSReports/DispatchRegister_.htm";
    string f_Main = "../Report/rptDispatchRegister";
    string v = "../GSReports/Voucher";
    string v_main = "../Report/rptVoucher";
    string Branch_Code = string.Empty;
    string qry = "";
    string cityMasterTable = string.Empty;
    DataTable dDate = new DataTable();
    DataTable dtlistbind = new DataTable();
    DataSet ds2 = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();//"NT_1_";
        qryCommon = tblPrefix + "qryDeliveryOrderListReport";
        fromDT = Request.QueryString["fromDT"];
        toDT = Request.QueryString["toDT"];
        partyEmail = Request.QueryString["email"];
        string isAuthenticate = string.Empty;
        string user = string.Empty;
        user = Session["user"].ToString();
        Branch_Code = Request.QueryString["Branch_Code"];
        cityMasterTable = tblPrefix + "CityMaster";
        if (!IsPostBack)
        {
            //this.BindReport();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                qry = " select distinct doc_dateConverted as Do_Date,doc_date as dt from qrydohead where doc_date between  '" + fromDT + "' and '" + toDT + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='" + trnType + "' order by doc_date ";
                DataSet ds = new DataSet();

                ds = clsDAL.SimpleQuery(qry);


                qry = " select doc_no ,mill_code,millshortname as mill,FreightPerQtl,mill_rate as millrate,quantal as qntl "
                  + ",shiptoname as party,getpassname as getpass,truck_no as lorry,transportname as transport,"
                  + " brokername as brokername,FreightPerQtl as frieght,grade as grade,sale_rate as salerate,purc_no as tn,"
                  + " purc_order as tdn,DO as DO,narration1 as narration,narration4 as narr4,shiptoname,"
                  + " Carporate_Sale_No,memo_no as refno, voucher_no as VN,voucher_type as vtype,FreightPerQtl as advancefrieght,SB_No,doc_dateConverted,MM_Rate,Tender_Commission,Delivery_Type,saleid from qrydohead " +
                  " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                  + " and tran_type='" + trnType + "' and Delivery_Type !='D'  and purc_no!=0 and SB_No!=0 and doc_date between  '" + fromDT + "' and '" + toDT + "' order by millShortName asc ";



                ds2 = clsDAL.SimpleQuery(qry);


                dtlistbind = ds2.Tables[0];
                if (ds != null)
                {
                    dDate = ds.Tables[0];
                    if (dDate.Rows.Count > 0)
                    {
                        dtlist.DataSource = dDate;
                        dtlist.DataBind();
                    }
                }

            }

            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }

        }
    }


    protected void dtlist_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            //using (clsDataProvider obj = new clsDataProvider())
            //{
            DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
            Label lblDate = (Label)e.Item.FindControl("lblDate");
            string date = DateTime.Parse(lblDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            Label lblQntlTotal = (Label)e.Item.FindControl("lblQntlTotal");

            DataView view1 = new DataView(dtlistbind, "doc_dateConverted='" + lblDate.Text + "'", "doc_dateConverted", DataViewRowState.CurrentRows);
            DataTable dtAcData = view1.ToTable(true, "doc_no", "mill_code", "mill", "Carporate_Sale_No", "narr4", "shiptoname", "VN", "vtype", "SB_No", "millrate", "doc_dateConverted",
                 "qntl", "lorry", "party", "getpass", "transport", "DO", "brokername", "salerate", "grade", "frieght", "refno", "tn", "tdn", "FreightPerQtl", "narration", "advancefrieght", "MM_Rate", "Tender_Commission", "Delivery_Type", "saleid");

            if (dtAcData.Rows.Count > 0)
            {
                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("doc_no", typeof(string)));
                dt.Columns.Add(new DataColumn("mill", typeof(string)));
                dt.Columns.Add(new DataColumn("millrate", typeof(string)));
                dt.Columns.Add(new DataColumn("qntl", typeof(double)));
                dt.Columns.Add(new DataColumn("party", typeof(string)));
                dt.Columns.Add(new DataColumn("getpass", typeof(string)));
                dt.Columns.Add(new DataColumn("lorry", typeof(string)));
                dt.Columns.Add(new DataColumn("transport", typeof(string)));
                dt.Columns.Add(new DataColumn("DO", typeof(string)));
                dt.Columns.Add(new DataColumn("brokername", typeof(string)));
                dt.Columns.Add(new DataColumn("salerate", typeof(string)));
                dt.Columns.Add(new DataColumn("grade", typeof(string)));
                dt.Columns.Add(new DataColumn("frieght", typeof(string)));
                dt.Columns.Add(new DataColumn("refno", typeof(string)));
                dt.Columns.Add(new DataColumn("tn", typeof(string)));
                dt.Columns.Add(new DataColumn("tdn", typeof(string)));
                dt.Columns.Add(new DataColumn("narration", typeof(string)));
                dt.Columns.Add(new DataColumn("advancefrieght", typeof(string)));
                dt.Columns.Add(new DataColumn("SB_No", typeof(string)));
                dt.Columns.Add(new DataColumn("FinalAmount", typeof(string)));
                dt.Columns.Add(new DataColumn("narr4", typeof(string)));
                dt.Columns.Add(new DataColumn("PaymentTo", typeof(string)));
                dt.Columns.Add(new DataColumn("shiptoname", typeof(string)));
                dt.Columns.Add(new DataColumn("tscrate", typeof(string)));
                dt.Columns.Add(new DataColumn("netpayble", typeof(string)));
                dt.Columns.Add(new DataColumn("tcsamount", typeof(string)));
                for (int i = 0; i < dtAcData.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    string Carporate_Sale_No = dtAcData.Rows[i]["Carporate_Sale_No"].ToString();
                    // string Carporate_Sale_No = ds2.Tables[0].Rows[i]["Carporate_Sale_No"].ToString();
                    string narration4 = dtAcData.Rows[i]["narr4"].ToString();
                    string shiptoname = dtAcData.Rows[i]["shiptoname"].ToString();
                    //  dr["shiptoname"] = "Ship To :" + shiptoname;
                    if (!string.IsNullOrEmpty(Carporate_Sale_No))
                    {
                        string SellingType = clsCommon.getString("Select selling_type from carporatehead where Doc_No=" + Carporate_Sale_No + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        string pdsunitname = clsCommon.getString("Select carporatepartyunitname from qrycarporateheaddetail where Doc_No=" + Carporate_Sale_No + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        if (SellingType == "P")
                        {
                            dr["narr4"] = "Bill To: " + pdsunitname;
                        }
                        else
                        {
                            dr["narr4"] = "Bill To: " + narration4;
                        }
                    }
                    string finalamount = "";
                    string DO_NO = dtAcData.Rows[i]["doc_no"].ToString();
                    string voucNo = dtAcData.Rows[i]["SB_No"].ToString();
                    string vtype = dtAcData.Rows[i]["vtype"].ToString();
                    dr["doc_no"] = DO_NO;
                    string SB_No = dtAcData.Rows[i]["saleid"].ToString();

                    dr["SB_No"] = voucNo + " SB";

                    dr["mill"] = dtAcData.Rows[i]["mill"].ToString();
                    dr["millrate"] = dtAcData.Rows[i]["millrate"].ToString();
                    dr["qntl"] = dtAcData.Rows[i]["qntl"].ToString();
                    dr["lorry"] = dtAcData.Rows[i]["lorry"].ToString();
                    string s = dtAcData.Rows[i]["party"].ToString();
                    string getpass = dtAcData.Rows[i]["getpass"].ToString();
                    if (s != getpass)
                    {
                        dr["getpass"] = "Getpass: " + getpass;
                    }
                    s = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
                    dr["party"] = s;
                    dr["transport"] = dtAcData.Rows[i]["transport"].ToString();
                    dr["DO"] = dtAcData.Rows[i]["DO"].ToString();
                    dr["brokername"] = dtAcData.Rows[i]["brokername"].ToString();
                    double salerate = Math.Abs(Convert.ToDouble(dtAcData.Rows[i]["salerate"].ToString()));
                    dr["grade"] = dtAcData.Rows[i]["grade"].ToString();
                    dr["frieght"] = dtAcData.Rows[i]["frieght"].ToString();
                    dr["refno"] = dtAcData.Rows[i]["refno"].ToString();
                    string tn = dtAcData.Rows[i]["tn"].ToString();
                    dr["tn"] = tn;
                    string tdn = dtAcData.Rows[i]["tdn"].ToString();
                    dr["tdn"] = tdn;
                    string smRate = clsCommon.getString("Select Commission_Rate from " + tblPrefix + "Tenderdetails where Tender_No=" + tn + " and ID=" + tdn + " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and year_code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                    double comrate = smRate != string.Empty ? Convert.ToDouble(smRate) : 0.00;
                    double CommRate = Math.Abs(comrate);
                    string m = dtAcData.Rows[i]["FreightPerQtl"].ToString();
                    double mnew = m != string.Empty ? Convert.ToDouble(m) : 0.00;
                    string momorate = dtAcData.Rows[i]["MM_Rate"].ToString();
                    string DoComminstion = dtAcData.Rows[i]["Tender_Commission"].ToString();
                    double cm = DoComminstion != string.Empty ? Convert.ToDouble(DoComminstion) : 0.00;
                    if (m != "0")
                    {
                        m = " + " + m;
                    }
                    else
                    {
                        m = "";
                    }

                    string CommRatestr = "";
                    if (CommRate != 0)
                    {
                        CommRatestr = " + " + CommRate;
                    }
                    else
                    {
                        CommRatestr = "";
                    }
                    double sr = salerate - CommRate;
                    double CGST = 0.00;
                    double SGST = 0.00;
                    double IGST = 0.00;

                    CGST = Convert.ToDouble(clsCommon.getString("select CGSTRate from " + tblPrefix + "SugarSale where saleid=" + SB_No + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ""));
                    SGST = Convert.ToDouble(clsCommon.getString("select SGSTRate from " + tblPrefix + "SugarSale where saleid=" + SB_No + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ""));
                    IGST = Convert.ToDouble(clsCommon.getString("select IGSTRate from " + tblPrefix + "SugarSale where saleid=" + SB_No + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ""));

                    //   double GstCgst = CGST.ToString();
                    //  double GstSgst = SGST.ToString();
                    //  double GstIgst = IGST.ToString();
                    // double total = (GstCgst) + (GstSgst) + (GstIgst);
                    double total = CGST + IGST + SGST;
                    string deliverytype = dtAcData.Rows[i]["Delivery_Type"].ToString();

                    if (deliverytype == "C")
                    {
                        string mmratr = momorate;
                        if (mmratr != "0.00")
                        {
                            mmratr = "+" + momorate;
                        }
                        else
                        {
                            mmratr = "";
                        }
                        string Commistion = DoComminstion;
                        if (Commistion != "0.00")
                        {
                            Commistion = "+" + Commistion;
                        }
                        else
                        {
                            Commistion = "";
                        }
                        dr["salerate"] = salerate + Commistion + mmratr;
                    }
                    else if (deliverytype == "A")
                    {

                        double sale = salerate - mnew;
                        string mmratr = momorate;
                        if (mmratr != "0.00")
                        {
                            mmratr = "+" + momorate;
                        }
                        else
                        {
                            mmratr = "";
                        }
                        string GstRatenew = "+" + total;
                        dr["salerate"] = sale + mmratr + GstRatenew;
                    }
                    else
                    {
                        double sale = salerate - mnew;
                        string mmratr = momorate;
                        if (mmratr != "0.00")
                        {
                            mmratr = "+" + momorate;
                        }
                        else
                        {
                            mmratr = "";
                        }
                        dr["salerate"] = sale + mmratr;
                    }

                    //  dr["salerate"] = sr + CommRatestr + m;
                    dr["narration"] = dtAcData.Rows[i]["narration"].ToString();
                    string PaymentToCode = clsCommon.getString("Select Payment_To from " + tblPrefix + "Tender where Tender_No=" + tn + " and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                    string PaymentTo = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + PaymentToCode);
                    string millcode = dtAcData.Rows[i]["mill_code"].ToString();
                    if (PaymentToCode != millcode)
                    {
                        dr["PaymentTo"] = "Payment To:" + PaymentTo;
                    }
                    if (!string.IsNullOrEmpty(SB_No))
                    {
                        finalamount = clsCommon.getString("select TCS_Net_Payable from " + tblPrefix + "SugarSale where saleid=" + SB_No + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                    }
                    else
                    {
                        //  finalamount = clsCommon.getString("select Voucher_Amount from " + tblPrefix + "Voucher where Doc_No=" + voucNo + " and Tran_Type='" + vtype + "' and DO_No=" + DO_NO + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                    }
                    dr["advancefrieght"] = momorate;
                    dr["FinalAmount"] = finalamount;

                    string TCSRate = clsCommon.getString("select TCS_Rate from " + tblPrefix + "SugarSale where saleid=" + SB_No + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                    dr["tscrate"] = TCSRate;

                    string tcsamount = clsCommon.getString("select TCS_Amt from " + tblPrefix + "SugarSale where saleid=" + SB_No + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                    dr["tcsamount"] = tcsamount;

                    string Netpayble = clsCommon.getString("select Bill_Amount from " + tblPrefix + "SugarSale where saleid=" + SB_No + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                    dr["netpayble"] = Netpayble;
                    dt.Rows.Add(dr);
                }
                if (dt.Rows.Count > 0)
                {
                    lblQntlTotal.Text = Convert.ToString(dt.Compute("SUM(qntl)", string.Empty));

                    dtlDetails.DataSource = dt;
                    dtlDetails.DataBind();
                }
            }
            //}

        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void dtlVoucher_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            Label lblAddr = (Label)e.Item.FindControl("lblCompanyAddr");
            lblAddr.Text = clsGV.CompanyAddress;

            Label lblPartyCity = (Label)e.Item.FindControl("lblPartyCity");
            lblPartyCity.Text = clsCommon.getString("select city_name_e from " + cityMasterTable + " where city_code=" + lblPartyCity.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));


            Label lblPhone = (Label)e.Item.FindControl("lblCompanyMobile");
            lblPhone.Text = clsGV.CompanyPhone;


            //    Label lblMillEmail = (Label)e.Item.FindControl("lblMillEmail");
            //   lblMillEmail.Text = millEmail;


            if (ViewState["pageBreak"] != null)
            {
                if (ViewState["pageBreak"].ToString() == "N")
                {
                    System.Web.UI.HtmlControls.HtmlTable tb = (System.Web.UI.HtmlControls.HtmlTable)e.Item.FindControl("tbMain");
                    tb.Style["page-break-after"] = "avoid";
                }
                else
                {
                    System.Web.UI.HtmlControls.HtmlTable tb = (System.Web.UI.HtmlControls.HtmlTable)e.Item.FindControl("tbMain");
                    tb.Style["page-break-after"] = "always";
                }
            }
        }
        catch (Exception exx1)
        {
            //  Unable to cast object of type 'System.Web.UI.HtmlControls.HtmlTable' to type 'System.Web.UI.WebControls.Table'.
            Response.Write(exx1.Message);
        }
    }
    //protected void lnkTenderNo_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LinkButton lnkTenderNo = (LinkButton)sender;
    //        DataListItem item = (DataListItem)lnkTenderNo.NamingContainer;
    //      //  string No = lnkTenderNo.Text;
    //        string accode = lnkTenderNo.Text;
    //     //   Session["TN_NO"] = No;
    //        Int16 Action = 1;
    //        Int32 counts = Convert.ToInt32(clsCommon.getString("SELECT doid  from nt_1_deliveryorder where doid=" + accode + "  "));
    //        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tnjhj", "javascript:TN();", true);
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:DO('" + counts + "','" + Action + "')", true);
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    protected void btnPrintOrMail_Click(object sender, EventArgs e)
    {
        try
        {
            string dono = "";
            var path = ""; int j = 0;
            List<string> DocsOV = new List<string>();
            List<string> DocsLV = new List<string>();
            List<string> DocsSB = new List<string>();
            string voucType = "";
            bool isprint = false;
            Dictionary<string, int> DictDocs = new Dictionary<string, int>();
            for (int i = 0; i < dtlist.Items.Count; i++)
            {
                DataList dtlDetails = (DataList)dtlist.Items[i].FindControl("dtlDetails");
                for (int a = 0; a < dtlDetails.Items.Count; a++)
                {
                    v = "../GSReports/Voucher";
                    CheckBox chkPrint = dtlDetails.Items[a].FindControl("chkPrint") as CheckBox;
                    CheckBox chkMail = dtlDetails.Items[a].FindControl("chkMail") as CheckBox;
                    Label lblDocNo = (Label)dtlDetails.Items[a].FindControl("lblDoNo");
                    string ins = "a";
                    if (chkPrint.Checked == true)
                    {
                        dono = lblDocNo.Text;
                        ins += j;
                        string getpassmail = clsCommon.getString("select Email_Id from " + tblPrefix + "AccountMaster where Ac_Code=(select GETPASSCODE from " + tblPrefix + "deliveryorder where doc_no=" + dono + " and tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")");
                        string SB_No = clsCommon.getString("Select SB_No from " + tblPrefix + "deliveryorder where doc_no=" + dono + " and tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                        if (SB_No == string.Empty || SB_No == "0")
                        {
                            string vtype = clsCommon.getString("Select voucher_type from " + tblPrefix + "deliveryorder where doc_no=" + dono + " and tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                            string voucherno = clsCommon.getString("Select voucher_no from " + tblPrefix + "deliveryorder where doc_no=" + dono + " and tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                            voucType = vtype;
                            if (voucType != "PS")
                            {
                                if (vtype == "OV")
                                {
                                    DocsOV.Add(voucherno);
                                }
                                if (vtype == "LV")
                                {
                                    DocsLV.Add(voucherno);
                                }
                                //Page.ClientScript.RegisterClientScriptBlock(GetType(), ins, "javascript:vp('" + voucherno + "','" + vtype + "');", true);
                                j += 1;
                                isprint = true;
                            }
                        }
                        else
                        {
                            string salebillno = clsCommon.getString("Select SB_No from " + tblPrefix + "deliveryorder where doc_no=" + dono + " and tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                            if (salebillno != "0" && !string.IsNullOrEmpty(salebillno))
                            {
                                DocsSB.Add(salebillno);
                            }
                        }
                    }
                    if (chkMail.Checked == true)
                    {
                        dono = lblDocNo.Text;
                        string getpassmail = clsCommon.getString("select Email_Id from " + tblPrefix + "AccountMaster where Ac_Code=(select VOUCHER_BY from " + tblPrefix + "deliveryorder where doc_no=" + dono + " and tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        string ccmail = clsCommon.getString("select Email_Id_cc from " + tblPrefix + "AccountMaster where Ac_Code=(select VOUCHER_BY from " + tblPrefix + "deliveryorder where doc_no=" + dono + " and tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ") and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        string vtype = clsCommon.getString("Select voucher_type from " + tblPrefix + "deliveryorder where doc_no=" + dono + " and tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                        string voucherno = clsCommon.getString("Select voucher_no from " + tblPrefix + "deliveryorder where doc_no=" + dono + " and tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                        string salebillno = clsCommon.getString("Select SB_No from " + tblPrefix + "deliveryorder where doc_no=" + dono + " and tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
                        path = v;

                        try
                        {
                            if (vtype != "PS")
                            {
                                string qry = "";
                                DataSet ds = new DataSet();
                                DataTable dt = new DataTable();

                                qry = "select * from " + tblPrefix + "qryVoucherList where Doc_No=" + voucherno + " and Tran_Type='" + vtype + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
                                DataSet dsV = new DataSet();
                                dsV = clsDAL.SimpleQuery(qry);
                                if (dsV.Tables[0].Rows.Count > 0)
                                {
                                    dsV.Tables[0].Columns.Add(new DataColumn("CT", typeof(string)));
                                    dsV.Tables[0].Columns.Add(new DataColumn("PartyNameC", typeof(string)));
                                    dsV.Tables[0].Columns.Add(new DataColumn("PartyAddressC", typeof(string)));
                                    dsV.Tables[0].Columns.Add(new DataColumn("party_cityC", typeof(string)));
                                    dsV.Tables[0].Columns.Add(new DataColumn("Cst_noC", typeof(string)));
                                    dsV.Tables[0].Columns.Add(new DataColumn("Gst_NoC", typeof(string)));
                                    dsV.Tables[0].Columns.Add(new DataColumn("Tin_NoC", typeof(string)));
                                    dsV.Tables[0].Columns.Add(new DataColumn("Local_Lic_NoC", typeof(string)));
                                    dsV.Tables[0].Columns.Add(new DataColumn("ECC_NoC", typeof(string)));
                                    dsV.Tables[0].Columns.Add(new DataColumn("CompanyPanC", typeof(string)));
                                    dsV.Tables[0].Columns.Add(new DataColumn("BrokerShortNew", typeof(string)));
                                    dsV.Tables[0].Columns.Add(new DataColumn("driver_no", typeof(string)));

                                    dsV.Tables[0].Columns.Add(new DataColumn("InWords", typeof(string)));
                                    dsV.Tables[0].Columns.Add(new DataColumn("party_city", typeof(string)));
                                    dsV.Tables[0].Columns.Add(new DataColumn("party_state", typeof(string)));
                                    string partyCityCode = dsV.Tables[0].Rows[0]["City_Code"].ToString();
                                    string partyCity = clsCommon.getString("select city_name_e from " + cityMasterTable + " where city_code='" + partyCityCode + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                    string partyState = clsCommon.getString("select state from " + cityMasterTable + " where city_code='" + partyCityCode + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                    dsV.Tables[0].Rows[0]["party_city"] = partyCity;
                                    dsV.Tables[0].Rows[0]["party_state"] = partyState;

                                    if (!string.IsNullOrWhiteSpace(dsV.Tables[0].Rows[0]["ASN_No"].ToString()))
                                    {
                                        dsV.Tables[0].Rows[0]["ASN_No"] = "ASN / GRN No: " + dsV.Tables[0].Rows[0]["ASN_No"].ToString();
                                    }

                                    if (!string.IsNullOrWhiteSpace(dsV.Tables[0].Rows[0]["Local_Lic_No"].ToString()))
                                    {
                                        dsV.Tables[0].Rows[0]["Local_Lic_No"] = "LIC No: " + dsV.Tables[0].Rows[0]["Local_Lic_No"].ToString();
                                    }
                                    if (!string.IsNullOrWhiteSpace(dsV.Tables[0].Rows[0]["Tin_No"].ToString()))
                                    {
                                        dsV.Tables[0].Rows[0]["Tin_No"] = "TIN: " + dsV.Tables[0].Rows[0]["Tin_No"].ToString();
                                    }
                                    if (!string.IsNullOrWhiteSpace(dsV.Tables[0].Rows[0]["Cst_no"].ToString()))
                                    {
                                        dsV.Tables[0].Rows[0]["Cst_no"] = "CST: " + dsV.Tables[0].Rows[0]["Cst_no"].ToString();
                                    }
                                    if (!string.IsNullOrWhiteSpace(dsV.Tables[0].Rows[0]["Gst_no"].ToString()))
                                    {
                                        dsV.Tables[0].Rows[0]["Gst_no"] = "GST: " + dsV.Tables[0].Rows[0]["Gst_no"].ToString();
                                    }
                                    if (!string.IsNullOrWhiteSpace(dsV.Tables[0].Rows[0]["CompanyPan"].ToString()))
                                    {
                                        dsV.Tables[0].Rows[0]["CompanyPan"] = "PAN: " + dsV.Tables[0].Rows[0]["CompanyPan"].ToString();
                                    }
                                    if (!string.IsNullOrWhiteSpace(dsV.Tables[0].Rows[0]["ECC_No"].ToString()))
                                    {
                                        dsV.Tables[0].Rows[0]["ECC_No"] = "ECC: " + dsV.Tables[0].Rows[0]["ECC_No"].ToString();
                                    }

                                    string Delivery_Type = dsV.Tables[0].Rows[0]["Delivery_Type"].ToString();

                                    if (Delivery_Type == "N")
                                    {
                                        double LESSDIFF = dsV.Tables[0].Rows[0]["Diff_Rate"].ToString() != string.Empty ? Convert.ToDouble(dsV.Tables[0].Rows[0]["Diff_Rate"].ToString()) : 0.00;
                                        double BANK_COMMISSION = dsV.Tables[0].Rows[0]["BANK_COMMISSION"].ToString() != string.Empty ? Convert.ToDouble(dsV.Tables[0].Rows[0]["BANK_COMMISSION"].ToString()) : 0.00;
                                        double Brokrage = dsV.Tables[0].Rows[0]["Brokrage"].ToString() != string.Empty ? Convert.ToDouble(dsV.Tables[0].Rows[0]["Brokrage"].ToString()) : 0.00;
                                        double RATEDIFF = dsV.Tables[0].Rows[0]["RATEDIFF"].ToString() != string.Empty ? Convert.ToDouble(dsV.Tables[0].Rows[0]["RATEDIFF"].ToString()) : 0.00;
                                        double Commission_Amount = dsV.Tables[0].Rows[0]["Commission_Amount"].ToString() != string.Empty ? Convert.ToDouble(dsV.Tables[0].Rows[0]["Commission_Amount"].ToString()) : 0.00;
                                        double FREIGHT = dsV.Tables[0].Rows[0]["FREIGHT"].ToString() != string.Empty ? Convert.ToDouble(dsV.Tables[0].Rows[0]["FREIGHT"].ToString()) : 0.00;
                                        double Postage = dsV.Tables[0].Rows[0]["Postage"].ToString() != string.Empty ? Convert.ToDouble(dsV.Tables[0].Rows[0]["Postage"].ToString()) : 0.00;
                                        double Interest = dsV.Tables[0].Rows[0]["Interest"].ToString() != string.Empty ? Convert.ToDouble(dsV.Tables[0].Rows[0]["Interest"].ToString()) : 0.00;
                                        double Cash_Ac_Amount = dsV.Tables[0].Rows[0]["Cash_Ac_Amount"].ToString() != string.Empty ? Convert.ToDouble(dsV.Tables[0].Rows[0]["Cash_Ac_Amount"].ToString()) : 0.00;
                                        double OTHER_Expenses = dsV.Tables[0].Rows[0]["OTHER_Expenses"].ToString() != string.Empty ? Convert.ToDouble(dsV.Tables[0].Rows[0]["OTHER_Expenses"].ToString()) : 0.00;
                                        double Transport_Amount = dsV.Tables[0].Rows[0]["Transport_Amount"].ToString() != string.Empty ? Convert.ToDouble(dsV.Tables[0].Rows[0]["Transport_Amount"].ToString()) : 0.00;

                                        double TotalAmount = LESSDIFF + BANK_COMMISSION + Brokrage + RATEDIFF + Commission_Amount + FREIGHT + Postage + Interest + Cash_Ac_Amount + OTHER_Expenses + Transport_Amount;

                                        dsV.Tables[0].Rows[0]["FREIGHT"] = TotalAmount;
                                        dsV.Tables[0].Rows[0]["Diff_Rate"] = 0.00;
                                        dsV.Tables[0].Rows[0]["BANK_COMMISSION"] = 0.00;
                                        dsV.Tables[0].Rows[0]["Brokrage"] = 0.00;
                                        dsV.Tables[0].Rows[0]["RATEDIFF"] = 0.00;
                                        dsV.Tables[0].Rows[0]["Commission_Amount"] = 0.00;
                                        dsV.Tables[0].Rows[0]["Postage"] = 0.00;
                                        dsV.Tables[0].Rows[0]["Interest"] = 0.00;
                                        dsV.Tables[0].Rows[0]["Cash_Ac_Amount"] = 0.00;
                                        dsV.Tables[0].Rows[0]["OTHER_Expenses"] = 0.00;

                                    }
                                    else
                                    {
                                        double OTHER_Expenses = dsV.Tables[0].Rows[0]["OTHER_Expenses"].ToString() != string.Empty ? Convert.ToDouble(dsV.Tables[0].Rows[0]["OTHER_Expenses"].ToString()) : 0.00;
                                        double Transport_Amount = dsV.Tables[0].Rows[0]["Transport_Amount"].ToString() != string.Empty ? Convert.ToDouble(dsV.Tables[0].Rows[0]["Transport_Amount"].ToString()) : 0.00;

                                        double otherAmount = OTHER_Expenses + Transport_Amount;
                                        if (otherAmount != 0)
                                        {
                                            dsV.Tables[0].Rows[0]["OTHER_Expenses"] = otherAmount;
                                        }
                                        else
                                        {
                                            dsV.Tables[0].Rows[0]["OTHER_Expenses"] = 0.00;
                                        }
                                    }


                                    dsV.Tables[0].Columns.Add(new DataColumn("VoucherNo", typeof(string)));
                                    double vouchamt = Convert.ToDouble(dsV.Tables[0].Rows[0]["Voucher_Amount"].ToString());
                                    dsV.Tables[0].Rows[0]["InWords"] = clsNoToWord.ctgword(dsV.Tables[0].Rows[0]["Voucher_Amount"].ToString());

                                    string millshort = dsV.Tables[0].Rows[0]["millshortname"].ToString();
                                    string qntl = dsV.Tables[0].Rows[0]["Quantal"].ToString();
                                    string SR = dsV.Tables[0].Rows[0]["Sale_Rate"].ToString();
                                    string broker = dsV.Tables[0].Rows[0]["BrokerShort"].ToString();
                                    if (broker != "Self")
                                    {
                                        dsV.Tables[0].Rows[0]["BrokerShortNew"] = "Broker: " + broker;
                                    }


                                    string narration = dsV.Tables[0].Rows[0]["Narration1"].ToString();
                                    string finalNarration = "";
                                    if (broker != "Self")
                                    {
                                        finalNarration = millshort + "-" + qntl + "-" + SR + "-" + broker;
                                    }
                                    else
                                    {
                                        finalNarration = millshort + "-" + qntl + "-" + SR;
                                    }

                                    dsV.Tables[0].Rows[0]["Narration1"] = finalNarration;

                                    string ac_code = dsV.Tables[0].Rows[0]["Ac_Code"].ToString();
                                    string unit_code = dsV.Tables[0].Rows[0]["Unit_Code"].ToString();

                                    string Do_No = dsV.Tables[0].Rows[0]["DO_No"].ToString();

                                    string Driver_no = clsCommon.getString("Select driver_no from " + tblPrefix + "deliveryorder where doc_no=" + Do_No + " and tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));

                                    if (!string.IsNullOrWhiteSpace(Driver_no))
                                    {
                                        dsV.Tables[0].Rows[0]["driver_no"] = "Driver Mobile:" + Driver_no;
                                    }

                                    ViewState["Qntl"] = dsV.Tables[0].Rows[0]["Quantal"].ToString();
                                    ViewState["lorry"] = dsV.Tables[0].Rows[0]["Lorry_No"].ToString();
                                    ViewState["PartyName"] = dsV.Tables[0].Rows[0]["PartyName"].ToString();
                                    if (ac_code != unit_code)
                                    {
                                        if (unit_code != "0")
                                        {
                                            dsV.Tables[0].Rows[0]["CT"] = "Consigned To,";
                                            string PartyNameC = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_Code=" + unit_code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                            string PartyAddressC = clsCommon.getString("Select Address_E from " + tblPrefix + "AccountMaster where Ac_Code=" + unit_code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                            string city_code = clsCommon.getString("Select City_Code from " + tblPrefix + "AccountMaster where Ac_Code=" + unit_code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                            string party_cityC = clsCommon.getString("Select 'City:<b>'+city_name_e+'</b>&nbsp;&nbsp;&nbsp;State:<b>'+state+'</b>' from " + tblPrefix + "CityMaster where city_code=" + city_code + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                            string Cst_noC = clsCommon.getString("Select Cst_no from " + tblPrefix + "AccountMaster where Ac_Code=" + unit_code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                            string Gst_NoC = clsCommon.getString("Select Gst_No from " + tblPrefix + "AccountMaster where Ac_Code=" + unit_code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                            string Tin_NoC = clsCommon.getString("Select Tin_No from " + tblPrefix + "AccountMaster where Ac_Code=" + unit_code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                            string Local_Lic_NoC = clsCommon.getString("Select Local_Lic_No from " + tblPrefix + "AccountMaster where Ac_Code=" + unit_code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                            string ECC_NoC = clsCommon.getString("Select ECC_No from " + tblPrefix + "AccountMaster where Ac_Code=" + unit_code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                            string CompanyPanC = clsCommon.getString("Select CompanyPan from " + tblPrefix + "AccountMaster where Ac_Code=" + unit_code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                                            ViewState["PartyName"] = PartyNameC;
                                            dsV.Tables[0].Rows[0]["PartyNameC"] = PartyNameC;
                                            dsV.Tables[0].Rows[0]["PartyAddressC"] = PartyAddressC;
                                            dsV.Tables[0].Rows[0]["party_cityC"] = party_cityC;

                                            if (!string.IsNullOrWhiteSpace(Local_Lic_NoC))
                                            {
                                                dsV.Tables[0].Rows[0]["Local_Lic_NoC"] = "LIC No: " + Local_Lic_NoC;
                                            }
                                            else
                                            {
                                                dsV.Tables[0].Rows[0]["Local_Lic_NoC"] = Local_Lic_NoC;
                                            }
                                            if (!string.IsNullOrWhiteSpace(Tin_NoC))
                                            {
                                                dsV.Tables[0].Rows[0]["Tin_NoC"] = "TIN: " + Tin_NoC;
                                            }
                                            else
                                            {
                                                dsV.Tables[0].Rows[0]["Tin_NoC"] = Tin_NoC;
                                            }
                                            if (!string.IsNullOrWhiteSpace(Cst_noC))
                                            {
                                                dsV.Tables[0].Rows[0]["Cst_noC"] = "CST: " + Cst_noC;
                                            }
                                            else
                                            {
                                                dsV.Tables[0].Rows[0]["Cst_noC"] = Cst_noC;
                                            }
                                            if (!string.IsNullOrWhiteSpace(Gst_NoC))
                                            {
                                                dsV.Tables[0].Rows[0]["Gst_NoC"] = "GST: " + Gst_NoC;
                                            }
                                            else
                                            {
                                                dsV.Tables[0].Rows[0]["Gst_NoC"] = Gst_NoC;
                                            }
                                            if (!string.IsNullOrWhiteSpace(CompanyPanC))
                                            {
                                                dsV.Tables[0].Rows[0]["CompanyPanC"] = "PAN: " + CompanyPanC;
                                            }
                                            else
                                            {
                                                dsV.Tables[0].Rows[0]["CompanyPanC"] = CompanyPanC;
                                            }
                                            if (!string.IsNullOrWhiteSpace(ECC_NoC))
                                            {
                                                dsV.Tables[0].Rows[0]["ECC_NoC"] = "ECC: " + ECC_NoC;
                                            }
                                            else
                                            {
                                                dsV.Tables[0].Rows[0]["ECC_NoC"] = ECC_NoC;
                                            }
                                        }
                                    }

                                    dt = dsV.Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        dtlVoucher.DataSource = dt;
                                        dtlVoucher.DataBind();
                                        if (getpassmail != string.Empty)
                                        {
                                            using (MemoryStream ms = new MemoryStream())
                                            {
                                                StringWriter sw = new StringWriter();
                                                HtmlTextWriter tw = new HtmlTextWriter(sw);
                                                pnlVoucher.RenderControl(tw);
                                                string s = sw.ToString();
                                                s = s.Replace("../Images", "http://" + clsGV.Website + "/Images");

                                                byte[] array = Encoding.UTF8.GetBytes(s);
                                                ms.Write(array, 0, array.Length);
                                                ms.Seek(0, SeekOrigin.Begin);
                                                ContentType contentType = new ContentType();
                                                contentType.MediaType = MediaTypeNames.Application.Octet;
                                                contentType.Name = path + voucherno + "(" + vtype + ")" + ".htm";
                                                Attachment attachment = new Attachment(ms, contentType);

                                                string mailFrom = Session["EmailId"].ToString();
                                                string smtpPort = "587";
                                                string emailPassword = Session["EmailPassword"].ToString();
                                                MailMessage msg = new MailMessage();
                                                SmtpClient SmtpServer = new SmtpClient(clsGV.Email_Address, 587);
                                                SmtpServer.Host = clsGV.Email_Address;
                                                msg.From = new MailAddress(mailFrom);
                                                msg.To.Add(ccmail + "," + getpassmail);
                                                msg.Body = "Voucher";
                                                msg.Attachments.Add(attachment);
                                                msg.IsBodyHtml = true;
                                                msg.Subject = "V.No:" + voucherno + " " + ViewState["lorry"].ToString() + " Qt:" + ViewState["Qntl"].ToString() + " " + ViewState["PartyName"].ToString();
                                                msg.IsBodyHtml = true;
                                                if (smtpPort != string.Empty)
                                                {
                                                    SmtpServer.Port = Convert.ToInt32(smtpPort);
                                                }
                                                SmtpServer.EnableSsl = true;
                                                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                                                SmtpServer.UseDefaultCredentials = false;
                                                SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
                                                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object k,
                                                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                                                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
                                                {
                                                    return true;
                                                };
                                                SmtpServer.Send(msg);
                                            }
                                            #region previous mail sending code
                                            /*  StringWriter sw = new StringWriter();
                                            HtmlTextWriter tw = new HtmlTextWriter(sw);
                                            pnlVoucher.RenderControl(tw);
                                            string op = sw.ToString();
                                            op = op.Replace("../Images", "http://" + clsGV.Website + "/Images");
                                            try
                                            {
                                                v = path + voucherno + "(" + vtype + ")" + ".htm";

                                                using (FileStream fs = new FileStream(Server.MapPath(v), FileMode.Create))
                                                {
                                                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                                                    {
                                                        w.WriteLine(op);
                                                        //fs.Close();
                                                    }
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            try
                                            {
                                                string mailFrom = Session["EmailId"].ToString();
                                                string smtpPort = "587";
                                                string emailPassword = Session["EmailPassword"].ToString();
                                                MailMessage msg = new MailMessage();
                                                SmtpClient SmtpServer = new SmtpClient(clsGV.Email_Address, 587);
                                                SmtpServer.Host = clsGV.Email_Address;
                                                msg.From = new MailAddress(mailFrom);
                                                msg.To.Add(ccmail + "," + getpassmail);
                                                msg.Body = "Voucher";
                                                msg.Attachments.Add(new Attachment(Server.MapPath(v)));
                                                //File.Create(path).Close();
                                                msg.IsBodyHtml = true;
                                                //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

                                                msg.Subject = "V.No:" + voucherno + " " + ViewState["lorry"].ToString() + " Qt:" + ViewState["Qntl"].ToString() + " " + ViewState["PartyName"].ToString();
                                                msg.IsBodyHtml = true;
                                                if (smtpPort != string.Empty)
                                                {
                                                    SmtpServer.Port = Convert.ToInt32(smtpPort);
                                                }
                                                                    SmtpServer.EnableSsl = true;
                                                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                                                //SmtpServer.Credentials = CredentialCache.DefaultNetworkCredentials;
                                                SmtpServer.UseDefaultCredentials = false;
                                                SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
                                                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object k,
                                                   System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                                   System.Security.Cryptography.X509Certificates.X509Chain chain,
                                                   System.Net.Security.SslPolicyErrors sslPolicyErrors)
                                                {
                                                    return true;
                                                };
                                                SmtpServer.Send(msg);
                                            }
                                            catch (Exception e1)
                                            {
                                                //Response.Write("mail err:" + e1);
                                                Response.Write("<script>alert('Error sending Mail');</script>");
                                                return;
                                            }*/
                                            #endregion
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (salebillno != "0" && !string.IsNullOrEmpty(salebillno))
                                {
                                    qry = "select s.doc_no as #,CONVERT(VARCHAR(10),s.doc_date,103) as dt,s.FROM_STATION as From_Place,a.Pincode as Party_Pin,s.TO_STATION as To_Place,s.LORRYNO as lorry,s.wearhouse,s.subTotal as Sub_Total,ISNULL(s.freight,0.00) as Less_Frieght," +
                                        " ISNULL(s.cash_advance,0.00) as Cash_Advance,s.RateDiff,ISNULL(s.bank_commission,0.00) as Bank_Commission,ISNULL(s.OTHER_AMT,0.00) as Other_Expenses,s.Bill_Amount as Bill_Amount,a.Ac_Name_E as Party_Name,a.Address_E as Party_Address," +
                                        " a.Local_Lic_No as Party_SLN,a.Tin_No as Party_TIN,a.ECC_No as Party_Ecc,a.Cst_no as Party_Cst,a.Gst_No as Party_Gst,a.CompanyPan as Party_PAN,c.city_name_e as Party_City,c.state as Party_State,b.Ac_Name_E as Mill_Name,a.Email_Id,a.Email_Id_cc,('Off.Phone: <b>'+a.OffPhone+'</b> &nbsp;&nbsp;Mobile: <b>'+a.Mobile_No+'</b>') as Party_Phone from " + tblPrefix + "SugarSale s" +
                                        " left outer join " + tblPrefix + "AccountMaster a on s.Unit_Code=a.Ac_Code and s.Company_Code=a.Company_Code left outer join " + tblPrefix + "AccountMaster b on s.mill_code=b.Ac_Code and s.Company_Code=b.Company_Code" +
                                        " left outer join " + tblPrefix + "CityMaster c on a.City_Code=c.city_code and a.Company_Code=c.company_code where s.doc_no IN(" + salebillno + ") and s.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and s.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
                                    DataSet dsbill = new DataSet();
                                    dsbill = clsDAL.SimpleQuery(qry);
                                    if (dsbill.Tables[0].Rows.Count > 0)
                                    {
                                        for (int d = 0; d < dsbill.Tables[0].Rows.Count; d++)
                                        {
                                            if (!string.IsNullOrWhiteSpace(dsbill.Tables[0].Rows[d]["Party_TIN"].ToString()))
                                            {
                                                dsbill.Tables[0].Rows[d]["Party_TIN"] = "&nbsp;&nbsp;TIN: " + dsbill.Tables[0].Rows[d]["Party_TIN"].ToString();
                                            }
                                            if (!string.IsNullOrWhiteSpace(dsbill.Tables[0].Rows[d]["Party_Ecc"].ToString()))
                                            {
                                                dsbill.Tables[0].Rows[d]["Party_Ecc"] = "&nbsp;&nbsp;ECC: " + dsbill.Tables[0].Rows[d]["Party_Ecc"].ToString();
                                            }
                                            if (!string.IsNullOrWhiteSpace(dsbill.Tables[0].Rows[d]["Party_Cst"].ToString()))
                                            {
                                                dsbill.Tables[0].Rows[d]["Party_Cst"] = "&nbsp;&nbsp;CST: " + dsbill.Tables[0].Rows[d]["Party_Cst"].ToString();
                                            }
                                            if (!string.IsNullOrWhiteSpace(dsbill.Tables[0].Rows[d]["Party_Gst"].ToString()))
                                            {
                                                dsbill.Tables[0].Rows[d]["Party_Gst"] = "&nbsp;&nbsp;GST: " + dsbill.Tables[0].Rows[d]["Party_Gst"].ToString();
                                            }
                                            if (!string.IsNullOrWhiteSpace(dsbill.Tables[0].Rows[d]["Party_SLN"].ToString()))
                                            {
                                                dsbill.Tables[0].Rows[d]["Party_SLN"] = "&nbsp;&nbsp;Sugar Lic No: " + dsbill.Tables[0].Rows[d]["Party_SLN"].ToString();
                                            }
                                            if (!string.IsNullOrWhiteSpace(dsbill.Tables[0].Rows[d]["Party_PAN"].ToString()))
                                            {
                                                dsbill.Tables[0].Rows[d]["Party_PAN"] = "&nbsp;&nbsp;PAN: " + dsbill.Tables[0].Rows[d]["Party_PAN"].ToString();
                                            }
                                        }

                                        string DO_no = clsCommon.getString("Select DO_No from " + tblPrefix + "SugarSale where doc_no=" + salebillno + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ");
                                        string carporateNo = clsCommon.getString("Select Carporate_Sale_No from " + tblPrefix + "deliveryorder where doc_no=" + DO_no + " and tran_type='DO' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ");

                                        if (!string.IsNullOrEmpty(carporateNo))
                                        {
                                            string SellingType = clsCommon.getString("Select SellingType from " + tblPrefix + "CarporateSale where Doc_No=" + carporateNo + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ");
                                            if (SellingType == "P")
                                            {
                                                dsbill.Tables[0].Rows[0]["Less_Frieght"] = 0.00;
                                                dsbill.Tables[0].Rows[0]["Cash_Advance"] = 0.00;
                                            }
                                        }
                                        DataTable dtbill = new DataTable();
                                        dtbill = dsbill.Tables[0];
                                        DataTable dt1 = new DataTable();
                                        dt1 = dsbill.Tables[0];
                                        if (dtbill.Rows.Count > 0)
                                        {
                                            string salebillmails =  dtbill.Rows[0]["Email_Id_cc"].ToString() + "," + dtbill.Rows[0]["Email_Id"].ToString();
                                            dtlSaleBill.DataSource = dtbill;
                                            dtlSaleBill.DataBind();

                                            if (salebillmails != string.Empty)
                                            {
                                                using (MemoryStream ms = new MemoryStream())
                                                {
                                                    StringWriter sw = new StringWriter();
                                                    HtmlTextWriter tw = new HtmlTextWriter(sw);
                                                    pnlSaleBill.RenderControl(tw);
                                                    string s = sw.ToString();
                                                    s = s.Replace("../Images", "http://" + clsGV.Website + "/Images");

                                                    byte[] array = Encoding.UTF8.GetBytes(s);
                                                    ms.Write(array, 0, array.Length);
                                                    ms.Seek(0, SeekOrigin.Begin);
                                                    ContentType contentType = new ContentType();
                                                    contentType.MediaType = MediaTypeNames.Application.Octet;
                                                    contentType.Name = "Salebill.htm";
                                                    Attachment attachment = new Attachment(ms, contentType);

                                                    string mailFrom = Session["EmailId"].ToString();
                                                    string smtpPort = "587";
                                                    string emailPassword = Session["EmailPassword"].ToString();
                                                    MailMessage msg = new MailMessage();
                                                    SmtpClient SmtpServer = new SmtpClient(clsGV.Email_Address, 587);
                                                    SmtpServer.Host = clsGV.Email_Address;
                                                    msg.From = new MailAddress(mailFrom);
                                                    msg.To.Add(salebillmails);
                                                    msg.Body = "Sale Bill";
                                                    msg.Attachments.Add(attachment);
                                                    msg.IsBodyHtml = true;
                                                    msg.Subject = "Sales Bill " + DateTime.Now.ToString("dd/MM/yyyy");
                                                    msg.IsBodyHtml = true;
                                                    if (smtpPort != string.Empty)
                                                    {
                                                        SmtpServer.Port = Convert.ToInt32(smtpPort);
                                                    }
                                                    SmtpServer.EnableSsl = true;
                                                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                                                    SmtpServer.UseDefaultCredentials = false;
                                                    SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
                                                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object k,
                                                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                                                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                                                    {
                                                        return true;
                                                    };
                                                    SmtpServer.Send(msg);
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception exx)
                        {
                            Response.Write("voucher Number not present");
                        }
                    }
                }
            }
            if (isprint == true)
            {
                var OVNo = String.Join(",", DocsOV);
                var LVNo = String.Join(",", DocsLV);
                var SBNo = string.Join(",", DocsSB);
                if (OVNo != string.Empty)
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ank", "javascript:vp('" + OVNo + "','OV');", true);
                }
                if (LVNo != string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ankush", "javascript:vp('" + LVNo + "','LV');", true);
                }
                if (SBNo != string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sb", "javascript:sb('" + SBNo + "');", true);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void dtlSaleBill_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            Label lblSB_No = (Label)e.Item.FindControl("lblSB_No");
            Label lblBillAmount = (Label)e.Item.FindControl("lblBillAmount");
            //Label lblCityStatePin = (Label)e.Item.FindControl("lblCityStatePin");
            //Label lblCmpMobile = (Label)e.Item.FindControl("lblCmpMobile");

            Label lblCompany = (Label)e.Item.FindControl("lblCompany");
            lblCompany.Text = "M/S. " + Session["Company_Name"].ToString();
            Label lblAl1 = (Label)e.Item.FindControl("lblAl1");
            Label lblAl2 = (Label)e.Item.FindControl("lblAl2");
            Label lblAl3 = (Label)e.Item.FindControl("lblAl3");
            Label lblAl4 = (Label)e.Item.FindControl("lblAl4");
            Label lblOtherDetails = (Label)e.Item.FindControl("lblOtherDetails");

            #region Address
            string qry = "Select * from tblVoucherHeadAddress where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("LeftAddress", typeof(string)));
                dt.Columns.Add(new DataColumn("MiddlePart", typeof(string)));
                dt.Columns.Add(new DataColumn("RightAddress", typeof(string)));

                string AL1 = ds.Tables[0].Rows[0]["AL1"].ToString();
                string AL2 = ds.Tables[0].Rows[0]["AL2"].ToString();
                string AL3 = ds.Tables[0].Rows[0]["AL3"].ToString();
                string AL4 = ds.Tables[0].Rows[0]["AL4"].ToString();
                string OtherDetails = ds.Tables[0].Rows[0]["Other"].ToString();

                string rnl = AL1.Replace("\n", "<br/>");
                var TabSpace = new String(' ', 4);
                string ab = rnl.Replace("\t", TabSpace);
                string la = ab.Replace(" ", "&nbsp;");
                lblAl1.Text = la;


                string rnl1 = AL2.Replace("\n", "<br/>");
                var TabSpace1 = new String(' ', 4);
                string ab1 = rnl1.Replace("\t", TabSpace1);
                string la1 = ab1.Replace(" ", "&nbsp;");
                lblAl2.Text = la1;

                string rnl2 = AL3.Replace("\n", "<br/>");
                var TabSpace2 = new String(' ', 4);
                string ab2 = rnl2.Replace("\t", TabSpace2);
                string la2 = ab2.Replace(" ", "&nbsp;");
                lblAl3.Text = la2;

                string rnl3 = AL4.Replace("\n", "<br/>");
                var TabSpace3 = new String(' ', 4);
                string ab3 = rnl3.Replace("\t", TabSpace2);
                string la3 = ab3.Replace(" ", "&nbsp;");
                lblAl4.Text = la3;

                string rnl4 = OtherDetails.Replace("\n", "<br/>");
                var TabSpace4 = new String(' ', 4);
                string ab4 = rnl4.Replace("\t", TabSpace2);
                string la4 = ab4.Replace(" ", "&nbsp;");
                lblOtherDetails.Text = la4;

            }
            #endregion

            Label lblInwords = (Label)e.Item.FindControl("lblInwords");
            Label lblNameCmp = (Label)e.Item.FindControl("lblNameCmp");
            DataList dtItemDetails = (DataList)e.Item.FindControl("dtItemDetails");
            string sbNo = lblSB_No.Text;
            lblInwords.Text = clsNoToWord.ctgword(lblBillAmount.Text);


            string city = clsCommon.getString("Select City_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string Pin = clsCommon.getString("Select PIN from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string state = clsCommon.getString("Select State_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

            //lblCityStatePin.Text = city + " (" + Pin + ") " + state;
            //lblCmpMobile.Text = clsCommon.getString("Select Mobile_No from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //lblCompanyName.Text = clsCommon.getString("Select Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //lblCompanyAddress.Text = clsCommon.getString("Select Address_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            qry = " select s.System_Name_E+' '+ISNULL(d.narration,'') as Item  ,d.bags as Bags,d.packing as Packing,d.Quantal as Qntl,d.rate as Rate,d.item_Amount as Value from " + tblPrefix + "sugarsaleDetails d" +
                " left outer join " + tblPrefix + "SystemMaster s on d.item_code=s.System_Code and d.Company_Code=s.Company_Code and s.System_Type='I' where d.doc_no=" + sbNo + " and d.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " order by d.detail_id";

            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblNameCmp.Text = clsCommon.getString("Select Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    dtItemDetails.DataSource = dt;
                    dtItemDetails.DataBind();
                }
            }


            Image imgSign = (Image)e.Item.FindControl("imgSign");
            string imgurl = clsCommon.getString("Select ImagePath from tblSign where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            imgSign.ImageUrl = imgurl;

        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void dtlVoucher_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            Label lblSignCmpName = (Label)e.Item.FindControl("lblSignCmpName");
            lblSignCmpName.Text = clsCommon.getString("Select Company_Name_E from Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

            Label lblCompany = (Label)e.Item.FindControl("lblCompany");
            lblCompany.Text = "M/S. " + Session["Company_Name"].ToString();
            Label lblAl1 = (Label)e.Item.FindControl("lblAl1");
            Label lblAl2 = (Label)e.Item.FindControl("lblAl2");
            Label lblAl3 = (Label)e.Item.FindControl("lblAl3");
            Label lblAl4 = (Label)e.Item.FindControl("lblAl4");
            Label lblOtherDetails = (Label)e.Item.FindControl("lblOtherDetails");

            #region Address
            string qry = "Select * from tblVoucherHeadAddress where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("LeftAddress", typeof(string)));
                dt.Columns.Add(new DataColumn("MiddlePart", typeof(string)));
                dt.Columns.Add(new DataColumn("RightAddress", typeof(string)));

                string AL1 = ds.Tables[0].Rows[0]["AL1"].ToString();
                string AL2 = ds.Tables[0].Rows[0]["AL2"].ToString();
                string AL3 = ds.Tables[0].Rows[0]["AL3"].ToString();
                string AL4 = ds.Tables[0].Rows[0]["AL4"].ToString();
                string OtherDetails = ds.Tables[0].Rows[0]["Other"].ToString();

                string rnl = AL1.Replace("\n", "<br/>");
                var TabSpace = new String(' ', 4);
                string ab = rnl.Replace("\t", TabSpace);
                string la = ab.Replace(" ", "&nbsp;");
                lblAl1.Text = la;


                string rnl1 = AL2.Replace("\n", "<br/>");
                var TabSpace1 = new String(' ', 4);
                string ab1 = rnl1.Replace("\t", TabSpace1);
                string la1 = ab1.Replace(" ", "&nbsp;");
                lblAl2.Text = la1;

                string rnl2 = AL3.Replace("\n", "<br/>");
                var TabSpace2 = new String(' ', 4);
                string ab2 = rnl2.Replace("\t", TabSpace2);
                string la2 = ab2.Replace(" ", "&nbsp;");
                lblAl3.Text = la2;

                string rnl3 = AL4.Replace("\n", "<br/>");
                var TabSpace3 = new String(' ', 4);
                string ab3 = rnl3.Replace("\t", TabSpace2);
                string la3 = ab3.Replace(" ", "&nbsp;");
                lblAl4.Text = la3;

                string rnl4 = OtherDetails.Replace("\n", "<br/>");
                var TabSpace4 = new String(' ', 4);
                string ab4 = rnl4.Replace("\t", TabSpace2);
                string la4 = ab4.Replace(" ", "&nbsp;");
                lblOtherDetails.Text = la4;

            }
            #endregion

            System.Web.UI.WebControls.Image imgSign = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgSign");
            string imgurl = clsCommon.getString("Select ImagePath from tblSign where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            string url = Server.MapPath(imgurl);
            imgSign.ImageUrl = imgurl;

        }
        catch (Exception)
        {

            throw;
        }
    }
}

