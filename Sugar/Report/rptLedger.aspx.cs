using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Net.Mime;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;

public partial class Reports_rptLedger : System.Web.UI.Page
{

    #region data section
    string f = "../GSReports/Ledger_.htm";
    string f_Main = "../Report/rptLedger";
    double netdebit = 0.00; double netcredit = 0.00;
    int company_code;
    int year_code;
    string tblPrefix = string.Empty;
    string tblGLEDGER = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string qryCommon = string.Empty;
    string cityMasterTable = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string TranTyp = string.Empty;
    int defaultAccountCode = 0;
    int tempcounter = 0;
    string email = string.Empty;
    static WebControl objAsp = null;
    string prefix = string.Empty;
    string accode = string.Empty;
    string fromdt = string.Empty;
    string todt = string.Empty;
    string DrCr = string.Empty;
    string drcronly = string.Empty;
    string frmdt = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        accode = Request.QueryString["accode"];
        fromdt = Request.QueryString["fromdt"];
        todt = Request.QueryString["todt"];
        DrCr = Request.QueryString["DrCr"];

        company_code = Convert.ToInt32(Session["Company_Code"].ToString());
        year_code = Convert.ToInt32(Session["year"].ToString());
        if (Session["tblPrefix"] != null)
        {
            tblPrefix = Session["tblPrefix"].ToString();
        }
        else
        {
            prefix = clsCommon.getString("Select tblPrefix from tblPrefix");
            tblPrefix = prefix.ToString();
        }
        tblGLEDGER = tblPrefix + "GLEDGER";
        tblDetails = tblPrefix + "VoucherDetails";
        AccountMasterTable = "qrymstaccountmaster";
        cityMasterTable = tblPrefix + "CityMaster";
        email = txtEmail.Text;
        string partyname = clsCommon.getString("select Ac_name_e from " + AccountMasterTable + "  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        Page.Title = partyname;
        if (!Page.IsPostBack)
        {
            lblCompany.Text = Session["Company_Name"].ToString();
            if (DrCr != "DrCr")
            {
                this.OnlyCrORDr();
            }
            else
            {
                this.bindData();
                // this. GetData(accode, company_code);

            }
        }
    }


    private void GetData(string accode, int company_Code)
    {
        // double netdebit = 0.00; double netcredit = 0.00;
        DataTable dt = new DataTable();
        DataTable dtreturn = new DataTable();
        string frmdt = Session["Start_Date"].ToString();
        // string conn = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        string cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText = "gledger";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("PartyCode", accode);
        cmd.Parameters.AddWithValue("FromDate", fromdt);
        cmd.Parameters.AddWithValue("ToDate", todt);
        cmd.Parameters.AddWithValue("companycode", Session["Company_Code"].ToString());
        cmd.Parameters.AddWithValue("yearcode", Session["year"].ToString());
        DataTable dtTable = new DataTable();
        DataSet dsSet = new DataSet();
        SqlDataAdapter _adapter = new SqlDataAdapter(cmd);
        // ds = new DataSet();
        _adapter.Fill(dsSet);
        con.Close();
        dtTable = dsSet.Tables[0];
        DataTable dtopinng = new DataTable();
        if (dtTable.Rows.Count > 0)
        {

            if (dtTable.Rows.Count > 0)
            {
                dtreturn.Columns.Add("TranType", typeof(string));
                dtreturn.Columns.Add("DocNo", typeof(Int32));
                dtreturn.Columns.Add("Date", typeof(string));
                dtreturn.Columns.Add("Narration", typeof(string));
                dtreturn.Columns.Add("Debit", typeof(double));
                dtreturn.Columns.Add("Credit", typeof(double));
                dtreturn.Columns.Add("Balance", typeof(double));
                dtreturn.Columns.Add("DrCr", typeof(string));
                dtreturn.Columns.Add("DO_NO", typeof(string));
                //dtreturn.Columns.Add("Ac_Name_E", typeof(string));
                DataView view = new DataView(dtTable);
                DataTable distinctValues = view.ToTable(true, "AC_CODE");
                //for selecting All accode

                for (int j = 0; j < distinctValues.Rows.Count; j++)
                {

                    DataView view1 = new DataView(dtTable, "AC_CODE='" + distinctValues.Rows[j]["AC_CODE"].ToString() + "'", "AC_CODE", DataViewRowState.CurrentRows);
                    DataTable dtAcData = view1.ToTable(true, "TRAN_TYPE", "DOC_NO", "DOC_DATE", "AC_CODE", "NARRATION", "AMOUNT", "DRCR", "group_Type", "DO_NO");

                    string groputype = dtAcData.Rows[0]["group_Type"].ToString();
                    string qry = "";
                    DataView view11;
                    DataTable newdt;
                    if (groputype == "B")
                    {
                        dtopinng = dsSet.Tables[1];
                        view11 = new DataView(dtopinng, "AC_CODE='" + distinctValues.Rows[j]["AC_CODE"].ToString() + "'", "AC_CODE", DataViewRowState.CurrentRows);
                        newdt = view11.ToTable(true, "AC_CODE", "OpBal");



                        //qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from GLEDGER " +
                        //    " where DOC_DATE < '" + Fromdt + "' and Ac_code=" + distinctValues.Rows[j]["AC_CODE"].ToString() + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE";
                    }
                    else
                    {
                        dtopinng = dsSet.Tables[2];
                        view11 = new DataView(dtopinng, "AC_CODE='" + distinctValues.Rows[j]["AC_CODE"].ToString() + "'", "AC_CODE", DataViewRowState.CurrentRows);
                        newdt = view11.ToTable(true, "AC_CODE", "OpBal");
                        //qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from GLEDGER"
                        //   + " where DOC_DATE >= '" + frmdt + "' and DOC_DATE < '" + Fromdt + "' and Ac_code=" + distinctValues.Rows[j]["AC_CODE"].ToString() + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE";
                    }

                    //for All data add opening balance
                    //  = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from GLEDGER where DOC_DATE >= '" + frmdt + "' and DOC_DATE < '" + Fromdt + "' and Ac_code=" + distinctValues.Rows[j]["AC_CODE"].ToString() + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE";
                    DataSet ds = new DataSet();
                    ds.Tables.Add(newdt);
                    double opBal = 0.0;


                    //dtopinng = xml_ds.Tables[1];
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            newdt = ds.Tables[0];
                            if (newdt.Rows.Count > 0)
                            {
                                opBal = Convert.ToDouble(newdt.Rows[0][1].ToString());
                                //nar = dt.Rows[0][2].ToString();
                                //if (!string.IsNullOrEmpty(nar.Trim().ToString()))
                                //{
                                //    nar = nar.Remove(0, 15);
                                //}
                            }
                        }
                    }


                    if (groputype != "B")
                    {
                        opBal = 0;
                    }
                    DataRow dr = dtreturn.NewRow();
                    dr[0] = "OP";
                    dr[1] = 0.00;
                    dr[2] = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    dr[3] = "Opening Balance";
                    if (opBal > 0)
                    {
                        dr[4] = Math.Round(opBal, 2);
                        dr[5] = 0.00;
                        dr[6] = Math.Round(opBal, 2);
                        dr[7] = "Dr";
                        netdebit += opBal;
                    }
                    else
                    {
                        dr[4] = 0.00;
                        dr[5] = Math.Round(-opBal, 2);
                        dr[6] = dr[5];
                        dr[7] = "Cr";
                        netcredit += -opBal;
                    }
                    dr[8] = dtAcData.Rows[j]["DO_NO"].ToString();
                    //dr[9] = dtAcData.Rows[0]["Ac_Name_E"].ToString();
                    dtreturn.Rows.Add(dr);
                    //Adding records
                    for (int i = 0; i < dtAcData.Rows.Count; i++)
                    {
                        dr = dtreturn.NewRow();
                        dr[0] = dtAcData.Rows[i]["TRAN_TYPE"].ToString();
                        dr[1] = dtAcData.Rows[i]["DOC_NO"].ToString();

                        if (dtAcData.Rows[i]["DOC_DATE"].ToString() != string.Empty)
                        {
                            string s = dtAcData.Rows[i]["DOC_DATE"].ToString();
                            dr[2] = DateTime.Parse(s, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                        }

                        string SORT_TYPE = dtAcData.Rows[i]["TRAN_TYPE"].ToString();
                        string SORT_NO = dtAcData.Rows[i]["DOC_NO"].ToString();
                        //dr[3] = Server.HtmlDecode(dtAcData.Rows[i]["NARRATION"].ToString() + "(" + SORT_TYPE + " " + SORT_NO + ")");
                        dr[3] = Server.HtmlDecode(dtAcData.Rows[i]["NARRATION"].ToString());
                        if (dtAcData.Rows[i]["DRCR"].ToString() == "D")
                        {
                            opBal = opBal + Convert.ToDouble(dtAcData.Rows[i]["AMOUNT"].ToString());

                            dr[4] = string.Format("{0:0.00}", Convert.ToDouble(dtAcData.Rows[i]["AMOUNT"].ToString()));
                            dr[5] = 0.00;
                            netdebit += Convert.ToDouble(dtAcData.Rows[i]["AMOUNT"].ToString());
                        }
                        else
                        {
                            opBal = opBal - Math.Abs(Convert.ToDouble(dtAcData.Rows[i]["AMOUNT"].ToString()));
                            netcredit += Math.Abs(Convert.ToDouble(dtAcData.Rows[i]["AMOUNT"].ToString()));

                            dr[4] = 0.00;
                            dr[5] = Math.Abs(Convert.ToDouble(dtAcData.Rows[i]["AMOUNT"].ToString()));
                        }
                        if (opBal > 0)
                        {
                            dr[6] = Math.Round(Convert.ToDouble(opBal), 2);
                            dr[7] = "Dr";
                        }
                        else
                        {
                            dr[6] = 0 - Math.Round(opBal, 2);
                            dr[7] = "Cr";
                        }
                        //dr[8] = distinctValues.Rows[j]["AC_CODE"].ToString();
                        //dr[9] = dtAcData.Rows[i]["Ac_Name_E"].ToString();
                        dtreturn.Rows.Add(dr);


                    }


                }

            }


        }

        grdDetail.DataSource = dtreturn;
        grdDetail.DataBind();
        grdDetail.FooterRow.Cells[3].Text = "Total";
        grdDetail.FooterRow.Cells[4].Text = Convert.ToDecimal(netdebit.ToString()).ToString("0.00");
        grdDetail.FooterRow.Cells[5].Text = Convert.ToDecimal(netcredit.ToString()).ToString("0.00");
        if (netdebit - netcredit != 0)
        {
            double balance = netdebit - netcredit;
            if (balance > 0)
            {
                grdDetail.FooterRow.Cells[7].Text = "Dr";
            }
            if (balance < 0)
            {
                grdDetail.FooterRow.Cells[7].Text = "Cr";
            }
            grdDetail.FooterRow.Cells[6].Text = Convert.ToDecimal(Math.Abs(Math.Round(balance, 2)).ToString()).ToString("0.00");
        }
        else
        {
            grdDetail.FooterRow.Cells[6].Text = "Nil";
        }
        grdDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
        grdDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
        grdDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
        grdDetail.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Center;



        // return dtreturn;
    }

    private void OnlyCrORDr()
    {
        try
        {
            string mail = "";
            // pnlPopup.Style["display"] = "none";
            if (accode != string.Empty)
            {
                string ccmail = clsCommon.getString("Select Email_Id_cc from " + AccountMasterTable + "  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                mail = ccmail + "," + clsCommon.getString("Select Email_Id from " + AccountMasterTable + "  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            }
            if (mail != string.Empty)
            {
                txtEmail.Text = mail;
            }
            else
            {
                email = txtEmail.Text.ToString();
            }
            if (DrCr.Equals("Cr"))
            {
                drcronly = "C";
            }
            else
            {
                drcronly = "D";
            }
            string partyname = clsCommon.getString("select Ac_name_e from " + AccountMasterTable + "  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            lblParty.Text = " (" + accode + ")&nbsp;" + partyname;

            string cityname = clsCommon.getString("select cityname from " + AccountMasterTable + "  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            lblcity.Text = " (" + accode + ")&nbsp;" + cityname;

            lblFromDt.Text = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            lblToDt.Text = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from " + tblGLEDGER + " where DOC_DATE < '"
                + fromdt + "' and Ac_code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE ";
            ds = clsDAL.SimpleQuery(qry);
            double opBal = 0.0;

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        opBal = Convert.ToDouble(dt.Rows[0][1].ToString());
                    }
                }
            }
            //qry = "select TRAN_TYPE,DOC_NO,Convert(varchar(10),DOC_DATE,102) as DOC_DATE ,NARRATION,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,UNIT_Code,DRCR,SORT_TYPE,SORT_NO,'' as DO_NO from " + tblGLEDGER +
            //        " where AC_CODE=" + accode + " and DOC_DATE between '" + fromdt + "' and '" + todt + "' and  Company_Code="
            //        + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by DOC_DATE asc,SORT_TYPE,SORT_NO,ORDER_CODE ";


            qry = "select TRAN_TYPE,DOC_NO,Convert(varchar(10),DOC_DATE,102) as DOC_DATE ,NARRATION,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,UNIT_Code,DRCR,SORT_TYPE,SORT_NO,'' as DO_NO from " + tblGLEDGER +
                  " where AC_CODE=" + accode + "  and drcr='" + drcronly + "' and DOC_DATE between '" + fromdt + "' and '" + todt + "' and  Company_Code="
                  + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by DOC_DATE asc,SORT_TYPE,SORT_NO,ORDER_CODE ";
            ds = clsDAL.SimpleQuery(qry);

            DataTable dtT = new DataTable();
            //dtT = null;
            dtT.Columns.Add("TranType", typeof(string));
            dtT.Columns.Add("DocNo", typeof(Int32));
            dtT.Columns.Add("Date", typeof(string));
            dtT.Columns.Add("Narration", typeof(string));
            dtT.Columns.Add("Debit", typeof(double));
            dtT.Columns.Add("Credit", typeof(double));
            dtT.Columns.Add("Balance", typeof(double));
            dtT.Columns.Add("DrCr", typeof(string));
            dtT.Columns.Add("DO_NO", typeof(string));

            //if (dt.Rows.Count > 0)
            //{
            dt = ds.Tables[0];

            DataRow dr = dtT.NewRow();
            //  old dr[0] = dt.Rows[0]["TRAN_TYPE"].ToString();
            dr[0] = "OP";
            dr[1] = 0.00;
            dr[2] = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            dr[3] = "Opening Balance ";

            if (opBal > 0)
            {
                dr[4] = Math.Round(opBal, 2);
                dr[5] = 0.00;
                dr[6] = Math.Round(opBal, 2);
                dr[7] = "Dr";
                netdebit += opBal;
            }
            else
            {
                dr[4] = 0.00;
                dr[5] = Math.Round(-opBal, 2);
                dr[6] = dr[5];
                dr[7] = "Cr";
                netcredit += -opBal;
            }
            dtT.Rows.Add(dr);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dtT.NewRow();

                    dr[0] = dt.Rows[i]["TRAN_TYPE"].ToString();
                    dr[1] = dt.Rows[i]["DOC_NO"].ToString();
                    if (dt.Rows[i]["DOC_DATE"].ToString() != string.Empty)
                    {
                        string s = dt.Rows[i]["DOC_DATE"].ToString();
                        dr[2] = DateTime.Parse(s, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    }
                    dr[8] = dt.Rows[i]["DO_NO"].ToString();

                    if (dt.Rows[i]["TRAN_TYPE"].ToString() == "SB")
                    {
                        string do_no = clsCommon.getString("select [DO_No] from NT_1_SugarSale where doc_no='" + dt.Rows[i]["DOC_NO"].ToString() + "' and  Company_Code="
                            + Convert.ToInt32(Session["Company_Code"].ToString())
                            + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                        dr[8] = do_no;
                    }
                    if (dt.Rows[i]["TRAN_TYPE"].ToString() == "PS")
                    {
                        string do_no = clsCommon.getString("select PURCNO from NT_1_SugarPurchase where doc_no='" + dt.Rows[i]["DOC_NO"].ToString() + "' and  Company_Code="
                            + Convert.ToInt32(Session["Company_Code"].ToString())
                            + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                        dr[8] = do_no;
                    }
                    string SORT_TYPE = dt.Rows[i]["SORT_TYPE"].ToString();
                    string SORT_NO = dt.Rows[i]["SORT_NO"].ToString();
                    dr[3] = Server.HtmlDecode(dt.Rows[i]["NARRATION"].ToString() + "(" + SORT_TYPE + " " + SORT_NO + ")");

                    if (dt.Rows[i]["DRCR"].ToString() == "D")
                    {
                        opBal = opBal + Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());

                        dr[4] = string.Format("{0:0.00}", Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString()));
                        dr[5] = 0.00;
                        netdebit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                    }
                    else
                    {
                        opBal = opBal - Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                        netcredit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());

                        dr[4] = 0.00;
                        dr[5] = Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                    }
                    if (DrCr == "Dr")
                    {
                        if (opBal > 0)
                        {
                            dr[6] = Math.Round(Convert.ToDouble(opBal), 2);
                            dr[7] = "Dr";
                            dtT.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        if (opBal < 0)
                        {
                            dr[6] = 0 - Math.Round(opBal, 2);
                            dr[7] = "Cr";
                            dtT.Rows.Add(dr);
                        }
                    }

                }
            }
            grdDetail.DataSource = dtT;
            grdDetail.DataBind();
            grdDetail.FooterRow.Cells[3].Text = "Total";
            if (DrCr == "Dr")
            {
                grdDetail.FooterRow.Cells[4].Text = netdebit.ToString();
            }
            else
            {
                grdDetail.FooterRow.Cells[5].Text = netcredit.ToString();
            }
            if (netdebit - netcredit != 0)
            {
                double balance = netdebit - netcredit;
                if (balance > 0)
                {
                    grdDetail.FooterRow.Cells[7].Text = "Dr";
                }
                if (balance < 0)
                {
                    grdDetail.FooterRow.Cells[7].Text = "Cr";
                }
                grdDetail.FooterRow.Cells[6].Text = Math.Abs(balance).ToString();
            }
            else
            {
                grdDetail.FooterRow.Cells[6].Text = "Nil";
            }
            grdDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Center;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    private void bindData()
    {

        try
        {
            using (clsDataProvider obj = new clsDataProvider())
            {
                string mail = "";
                // pnlPopup.Style["display"] = "none";
                if (accode != string.Empty)
                {
                    string ccmail = clsCommon.getString("Select Email_Id_cc from " + AccountMasterTable + "  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    if (ccmail == "0")
                    {
                        ccmail = string.Empty;
                    }
                    mail = ccmail + "," + clsCommon.getString("Select Email_Id from " + AccountMasterTable + "  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                }
                if (mail != string.Empty)
                {
                    txtEmail.Text = mail;
                }
                else
                {
                    email = txtEmail.Text.ToString();
                }
                lblParty.Text = " (" + accode + ")&nbsp;" + clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + "  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                lblcity.Text = clsCommon.getString("select cityname from " + AccountMasterTable + "  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                lblFromDt.Text = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                lblToDt.Text = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();


                string groupcode = clsCommon.getString("select Group_Code from " + AccountMasterTable + " where Ac_Code=" + accode + " and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()));
                int groupcd = Convert.ToInt32(groupcode);
                string groputype = string.Empty;
                if (groupcd > 0)
                {
                    groputype = clsCommon.getString("select group_Type from   NT_1_BSGroupMaster where group_Code=" + groupcd + " and Company_Code="
                   + Convert.ToInt32(Session["Company_Code"].ToString()));

                }
                string qry;
                frmdt = Session["Start_Date"].ToString();
                if (groputype == "B")
                {
                    qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from " + tblGLEDGER
                       + " where DOC_DATE < '" + fromdt + "' and Ac_code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE";
                }
                else
                {
                    qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from " + tblGLEDGER
                       + " where DOC_DATE >= '" + fromdt + "' and DOC_DATE < '" + fromdt + "' and Ac_code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE";
                }

                ds = clsDAL.SimpleQuery(qry);
                //DataSet ds = new DataSet();
                //DataTable dt = new DataTable();
                //string qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from " + tblGLEDGER
                //    + " where DOC_DATE < '" + fromdt + "' and Ac_code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                //    + " group by AC_CODE";
                //ds = obj.GetDataSet(qry);

                //ds = clsDAL.SimpleQuery(qry);
                double opBal = 0.0;
                string nar = string.Empty;
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            opBal = Convert.ToDouble(dt.Rows[0][1].ToString());
                            //nar = dt.Rows[0][2].ToString();
                            //if (!string.IsNullOrEmpty(nar.Trim().ToString()))
                            //{
                            //    nar = nar.Remove(0, 15);
                            //}
                        }
                    }
                }
                if (groputype != "B")
                {
                    opBal = 0;
                }
                qry = "select TRAN_TYPE,DOC_NO,Convert(varchar(10),DOC_DATE,102) as DOC_DATE ,NARRATION,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,UNIT_Code,DRCR,SORT_TYPE,SORT_NO,'' as DO_NO from " + tblGLEDGER +
                        " where AC_CODE=" + accode + " and DOC_DATE between '" + fromdt + "' and '" + todt + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by DOC_DATE asc,SORT_TYPE,SORT_NO,ORDER_CODE ";

                //qry = " select TRAN_TYPE,DOC_NO,DOC_DATEConverted as DOC_DATE1,NARRATION,AMOUNT,ADJUSTED_AMOUNT,Ac_Code,UNIT_Code,DRCR,SORT_TYPE,SORT_NO,'' as DO_NO " +
                //    " from qrygledger  where AC_CODE=" + accode + " and DOC_DATE between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by DOC_DATE ,DOC_NO asc ,ORDER_CODE ";
                ds = clsDAL.SimpleQuery(qry);
                //qry = "select TRAN_TYPE,DOC_NO,Convert(varchar(10),DOC_DATE,102) as DOC_DATE ,NARRATION,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,UNIT_Code,DRCR,SORT_TYPE,SORT_NO,'' as DO_NO from " + tblGLEDGER +
                //        " where AC_CODE=" + accode + " and DOC_DATE between '" + fromdt + "' and '" + todt + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by DOC_DATE asc,SORT_TYPE,SORT_NO,ORDER_CODE ";
                //ds = clsDAL.SimpleQuery(qry);

                DataTable dtT = new DataTable();
                //dtT = null;
                dtT.Columns.Add("TranType", typeof(string));
                dtT.Columns.Add("DocNo", typeof(Int32));
                dtT.Columns.Add("Date", typeof(string));
                dtT.Columns.Add("Narration", typeof(string));
                dtT.Columns.Add("Debit", typeof(double));
                dtT.Columns.Add("Credit", typeof(double));
                dtT.Columns.Add("Balance", typeof(double));
                dtT.Columns.Add("DrCr", typeof(string));
                dtT.Columns.Add("DO_NO", typeof(string));

                //if (dt.Rows.Count > 0)
                //{
                dt = ds.Tables[0];

                DataRow dr = dtT.NewRow();
                //  old dr[0] = dt.Rows[0]["TRAN_TYPE"].ToString();
                dr[0] = "OP";
                dr[1] = 0.00;
                dr[2] = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                dr[3] = "Opening Balance";
                if (opBal > 0)
                {
                    dr[4] = Math.Round(opBal, 2);
                    dr[5] = 0.00;
                    dr[6] = Math.Round(opBal, 2);
                    dr[7] = "Dr";
                    netdebit += opBal;
                }
                else
                {
                    dr[4] = 0.00;
                    dr[5] = Math.Round(-opBal, 2);
                    dr[6] = dr[5];
                    dr[7] = "Cr";
                    netcredit += -opBal;
                }
                dtT.Rows.Add(dr);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dtT.NewRow();

                        dr[0] = dt.Rows[i]["TRAN_TYPE"].ToString();
                        dr[1] = dt.Rows[i]["DOC_NO"].ToString();
                        dr[8] = dt.Rows[i]["DO_NO"].ToString();

                        if (dt.Rows[i]["TRAN_TYPE"].ToString() == "SB")
                        {
                            string sbno = dt.Rows[i]["DOC_NO"].ToString();
                            if (sbno == "0")
                            {
                                dr[8] = "0";
                            }
                            else
                            {
                                string do_no = clsCommon.getString("select DO_No from NT_1_SugarSale where doc_no='" + dt.Rows[i]["DOC_NO"].ToString() + "' and  Company_Code="
                                   + Convert.ToInt32(Session["Company_Code"].ToString())
                                   + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                                dr[8] = do_no;
                            }
                        }
                        if (dt.Rows[i]["TRAN_TYPE"].ToString() == "PS")
                        {
                            string do_no = clsCommon.getString("select PURCNO from NT_1_SugarPurchase where doc_no='" + dt.Rows[i]["DOC_NO"].ToString() + "' and  Company_Code="
                                + Convert.ToInt32(Session["Company_Code"].ToString())
                                + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                            dr[8] = do_no;
                        }
                        if (dt.Rows[i]["DOC_DATE"].ToString() != string.Empty)
                        {
                            string s = dt.Rows[i]["DOC_DATE"].ToString();
                            dr[2] = DateTime.Parse(s, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                        }
                        string SORT_TYPE = dt.Rows[i]["SORT_TYPE"].ToString();
                        string SORT_NO = dt.Rows[i]["SORT_NO"].ToString();
                        //dr[3] = Server.HtmlDecode(dt.Rows[i]["NARRATION"].ToString() + "(" + SORT_TYPE + " " + SORT_NO + ")");
                        dr[3] = Server.HtmlDecode(dt.Rows[i]["NARRATION"].ToString());



                        if (dt.Rows[i]["DRCR"].ToString() == "D")
                        {
                            opBal = opBal + Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());

                            dr[4] = string.Format("{0:0.00}", Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString()));
                            dr[5] = 0.00;
                            netdebit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                        }
                        else
                        {
                            opBal = opBal - Math.Abs(Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString()));
                            netcredit += Math.Abs(Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString()));

                            dr[4] = 0.00;
                            dr[5] = Math.Abs(Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString()));
                        }

                        if (opBal > 0)
                        {
                            dr[6] = Math.Round(Convert.ToDouble(opBal), 2);
                            dr[7] = "Dr";
                        }
                        else
                        {
                            dr[6] = 0 - Math.Round(opBal, 2);
                            dr[7] = "Cr";
                        }
                        dtT.Rows.Add(dr);
                    }
                }
                grdDetail.DataSource = dtT;
                grdDetail.DataBind();
                grdDetail.FooterRow.Cells[3].Text = "Total";
                grdDetail.FooterRow.Cells[4].Text = Convert.ToDecimal(netdebit.ToString()).ToString("0.00");
                grdDetail.FooterRow.Cells[5].Text = Convert.ToDecimal(netcredit.ToString()).ToString("0.00");
                if (netdebit - netcredit != 0)
                {
                    double balance = netdebit - netcredit;
                    if (balance > 0)
                    {
                        grdDetail.FooterRow.Cells[7].Text = "Dr";
                    }
                    if (balance < 0)
                    {
                        grdDetail.FooterRow.Cells[7].Text = "Cr";
                    }
                    grdDetail.FooterRow.Cells[6].Text = Convert.ToDecimal(Math.Abs(Math.Round(balance, 2)).ToString()).ToString("0.00");
                }
                else
                {
                    grdDetail.FooterRow.Cells[6].Text = "Nil";
                }
                grdDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                grdDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                grdDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                grdDetail.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tempcounter = tempcounter + 1;
            //if (tempcounter == 10)
            //{
            //    e.Row.Attributes.Add("style", "page-break-after: always;");
            //    tempcounter = 0;
            //}
            if (e.Row.Cells[4].Text == "0")
            {
                e.Row.Cells[4].Text = "";
            }
            if (e.Row.Cells[5].Text == "0")
            {
                e.Row.Cells[5].Text = "";
            }
            if (e.Row.Cells[6].Text == "0")
            {
                e.Row.Cells[6].Text = "Nil";
            }
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            string mail = txtEmail.Text;
            if (txtEmail.Text != string.Empty)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter tw = new HtmlTextWriter(sw);
                    PrintPanel.RenderControl(tw);
                    string s = sw.ToString();
                    s = s.Replace("../Images", "http://" + clsGV.Website + "/Images");
                    byte[] array = Encoding.UTF8.GetBytes(s);
                    ms.Write(array, 0, array.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    ContentType contentType = new ContentType();
                    contentType.MediaType = MediaTypeNames.Application.Octet;
                    contentType.Name = "Ledger.htm";
                    Attachment attachment = new Attachment(ms, contentType);

                    string mailFrom = Session["EmailId"].ToString();
                    string smtpPort = "587";
                    string emailPassword = Session["EmailPassword"].ToString();
                    MailMessage msg = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(clsGV.Email_Address, 587);
                    SmtpServer.Host = clsGV.Email_Address;
                    msg.From = new MailAddress(mailFrom);
                    msg.To.Add(mail);
                    msg.Body = "Ledger";
                    msg.Attachments.Add(attachment);
                    msg.IsBodyHtml = true;
                    msg.Subject = "Ledger Report";
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
        catch (Exception e1)
        {
            Response.Write("mail err:" + e1);
            return;
        }
        Response.Write("<script>alert('Mail sent successfully');</script>");
    }

    protected void btnPDf_Click(object sender, EventArgs e)
    {
        #region[Pdf comment]
        //try
        //{
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);
        //    tblmn.RenderControl(hw);
        //    string s1 = sw.ToString().Replace("font-size: medium", "font-size: xx-small");
        //    //StringReader sr = new StringReader(s1.ToString());
        //    StringReader sr = new StringReader(s1.ToString().Replace("../Images", Server.MapPath("~/Images")));

        //    Document pdfDoc = new Document(iTextSharp.text.PageSize.A4);
        //    var writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

        //    pdfDoc.Open();
        //    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //    pdfDoc.Close();
        //    Response.Write(pdfDoc);
        //    Response.End();
        //}
        //catch
        //{
        //}
        #endregion

        string filepath = "";
        Attachment attachment = null;
        try
        {
            email = txtEmail.Text.ToString();

            //Label lblSB_No = (Label)dtlist.Items[0].FindControl("lblSB_No");
            string fileName = "Ledger_" + lblAcCode.Text + ".pdf";
            filepath = "~/PAN/" + fileName;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw1 = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw1);
            tblMain.RenderControl(hw);
            string s1 = sw1.ToString().Replace("font-size: medium", "font-size: xx-small");
            StringReader sr = new StringReader(s1.ToString().Replace("../Images", Server.MapPath("~/Images")));
            Document pdfDoc = new Document(iTextSharp.text.PageSize.LEDGER);

            //Document pdfDoc = new Document(iTextSharp.text.PageSize.A4, 30f, 30f, 30f, 250f);
            var writer = PdfWriter.GetInstance(pdfDoc, new FileStream(Server.MapPath(filepath), FileMode.Create));
            Font tblfont = new Font();
            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();

            ContentType contentType = new ContentType();
            contentType.MediaType = MediaTypeNames.Application.Pdf;
            contentType.Name = fileName;
            attachment = new Attachment(Server.MapPath(filepath), contentType);

            string mailFrom = Session["EmailId"].ToString();
            string smtpPort = "587";
            string emailPassword = Session["EmailPassword"].ToString();
            MailMessage msg = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            SmtpServer.Host = clsGV.Email_Address;
            msg.From = new MailAddress(mailFrom);
            msg.To.Add(email);
            msg.Body = "LEDGER";
            msg.Attachments.Add(attachment);
            msg.IsBodyHtml = true;
            string lorry = string.Empty;
            string millshort = string.Empty;
            string party = string.Empty;
            //if (ViewState["lorry"] != null)
            //{
            //    lorry = "Lorry:" + ViewState["lorry"].ToString();
            //}
            //if (ViewState["millshort"] != null)
            //{
            //    millshort = "Mill:" + ViewState["millshort"].ToString();
            //}
            //if (ViewState["Party_Name"] != null)
            //{
            //    party = "Getpass:" + ViewState["Party_Name"].ToString();
            //}
            msg.Subject = "LEDGER DETAILS:" + lblParty.Text;
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
            attachment.Dispose();
            if (File.Exists(Server.MapPath(filepath)))
            {
                File.Delete(Server.MapPath(filepath));
            }
        }
        catch (Exception e1)
        {
            //Response.Write("mail err:" + e1);
            Response.Write("<script>alert('Error sending Mail');</script>");
            return;
        }
        finally
        {
            attachment.Dispose();
            if (File.Exists(Server.MapPath(filepath)))
            {
                File.Delete(Server.MapPath(filepath));
            }
        }
        Response.Write("<script>alert('Mail sent successfully');</script>");


    }

    private void CreateHtml()
    {
        StringWriter sw = new StringWriter();
        HtmlTextWriter tw = new HtmlTextWriter(sw);
        PrintPanel.RenderControl(tw);
        string s = sw.ToString();
        try
        {
            using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine(s);
                }
            }
        }
        catch (Exception ee)
        {
            f = f_Main + ".htm";
            using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine(s);
                }
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        //grdDetail.AllowPaging = false;
        //grdDetail.DataBind();
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //grdDetail.RenderControl(hw);
        //string gridHTML = sw.ToString().Replace("\"", "'")
        //    .Replace(System.Environment.NewLine, "");
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<script type = 'text/javascript'>");
        //sb.Append("window.onload = new function(){");
        //sb.Append("var printWin = window.open('', '', 'left=0");
        //sb.Append(",top=0,width=1000,height=600,status=0');");
        //sb.Append("printWin.document.write(\"");
        //sb.Append(gridHTML);
        //sb.Append("\");");
        //sb.Append("printWin.document.close();");
        //sb.Append("printWin.focus();");
        //sb.Append("printWin.print();");
        //sb.Append("printWin.close();};");
        //sb.Append("</script>");
        //ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        //grdDetail.AllowPaging = true;
        //grdDetail.DataBind();
        //StringBuilder StrHtmlGenerate = new StringBuilder();
        //StringBuilder StrExport = new StringBuilder();
        //StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
        //StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
        //StrExport.Append("<DIV  style='font-size:12px;'>");
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter tw = new HtmlTextWriter(sw);
        //PrintPanel.RenderControl(tw);
        //string sim = sw.ToString();
        //StrExport.Append(sim);
        //StrExport.Append("</div></body></html>");
        //string strFile = "report.xls";
        //string strcontentType = "application/excel";
        //Response.ClearContent();
        //Response.ClearHeaders();
        //Response.BufferOutput = true;
        //Response.ContentType = strcontentType;
        //Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
        //Response.Write(StrExport.ToString());
        //Response.Flush();
        //Response.Close();
        //Response.End();


        //grdDetail.AllowPaging = false;
        //grdDetail.DataBind();
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //grdDetail.RenderControl(hw);
        //string gridHTML = sw.ToString().Replace("\"", "'")
        //    .Replace(System.Environment.NewLine, "");
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<script type = 'text/javascript'>");
        //sb.Append("window.onload = new function(){");
        //sb.Append("var printWin = window.open('', '', 'left=0");
        //sb.Append(",top=0,width=1000,height=600,status=0');");
        //sb.Append("printWin.document.write(\"");
        //sb.Append(gridHTML);
        //sb.Append("\");");
        //sb.Append("printWin.document.close();");
        //sb.Append("printWin.focus();");
        //sb.Append("printWin.print();");
        //sb.Append("printWin.close();};");
        //sb.Append("</script>");
        //ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        //grdDetail.AllowPaging = true;
        //grdDetail.DataBind();
        string report = "Report";

        Export(grdDetail, report);
    }
    private void Export(GridView grd, string Name)
    {
        StringBuilder StrHtmlGenerate = new StringBuilder();
        StringBuilder StrExport = new StringBuilder();
        StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
        StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
        StrExport.Append("<DIV  style='font-size:12px;'>");
        StringWriter sw = new StringWriter();
        HtmlTextWriter tw = new HtmlTextWriter(sw);
        grd.RenderControl(tw);
        string sim = sw.ToString();
        StrExport.Append(sim);
        StrExport.Append("</div></body></html>");
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + Name + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        string style = @"<style> .textmode { } </style>";
        Response.Write(style);
        Response.Output.Write(StrExport.ToString());
        Response.Flush();
        Response.End();
    }
    protected void btpGrdprint_Click(object sender, EventArgs e)
    {
        grdDetail.AllowPaging = false;
        grdDetail.DataBind();
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        grdDetail.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'")
            .Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=0,width=1000,height=600,status=0');");
        sb.Append("printWin.document.write(\"");
        sb.Append(gridHTML);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        grdDetail.AllowPaging = true;
        grdDetail.DataBind();
    }

    protected void lnkGo_Click(object sender, EventArgs e)
    {
        LinkButton lnkGo = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkGo.NamingContainer;
        int rowIndex = row.RowIndex;
        string TranType = grdDetail.Rows[rowIndex].Cells[0].Text;
        string No = lnkGo.Text;

        string sessionName = GetSessionName(TranType);
        string reciptPayment = string.Empty;

        string dtt = "01/07/2017";
        string dtxml = "01/01/2019";
        DateTime dt2xml = DateTime.ParseExact(dtxml, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        string dt11 = grdDetail.Rows[rowIndex].Cells[2].Text.Trim();
        dt11 = dt11.Replace("-", "/");
        DateTime dt = DateTime.ParseExact(dtt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime dt1 = DateTime.ParseExact(dt11, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        if (No != "0")
        {
            if (TranType.Contains("BP") || TranType.Contains("CR") || TranType.Contains("CP") || TranType.Contains("BR"))
            {
                Session["RP_NO"] = No;
                Session["RP_TYPE"] = sessionName;
                if (dt1 > dt2xml)
                {
                    Int32 action = 1;

                    Int32 count = Convert.ToInt32(clsCommon.getString("select tranid from nt_1_transacthead where doc_no='" + No + "' and tran_type='" + sessionName + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ReciptPayment", "javascript:RPxml('" + count + "','" + action + "','" + sessionName + "');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ReciptPayment", "javascript:RP();", true);
                }
            }
            else if (TranType.Contains("DN") || TranType.Contains("CN") || TranType.Contains("DS") || TranType.Contains("CS"))
            {
                if (dt1 > dt2xml)
                {
                    Int32 action = 1;

                    Int32 count = Convert.ToInt32(clsCommon.getString("select dcid from debitnotehead where doc_no='" + No + "' and tran_type='" + TranType + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ReciptPayment", "javascript:DNxml('" + count + "','" + action + "','" + TranType + "');", true);
                }
            }
            else
            {
                if (dt1 < dt)//for old links
                {
                    Session[sessionName] = No;

                    if (sessionName == "SB_NO")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:SBOld();", true);
                    }
                    else if (sessionName == "LV_NO")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:LVOld();", true);

                    }
                    else if (sessionName == "DO_NO")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:DOOld();", true);
                    }
                    else if (sessionName == "PURC_NO")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:PSOld();", true);
                    }
                    else if (sessionName == "RS_NO")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:RSOld();", true);
                    }
                    else
                    {
                        Session[sessionName] = No;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:" + TranType + "();", true);
                    }

                }
                else if (dt1 > dt2xml)
                {
                    Session[sessionName] = No;

                    if (sessionName == "SB_NO")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select saleid from nt_1_sugarsale where doc_no='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:SBxml('" + count + "','" + action + "');", true);
                    }
                    else if (sessionName == "LV_NO")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select commissionid from commission_bill where doc_no='" + No +
                            "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='"
                            + Convert.ToInt32(Session["year"]).ToString() + "' and Tran_Type='LV'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:LVxml('" + count + "','" + action + "','LV');", true);

                    }
                    else if (sessionName == "CV_NO")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select commissionid from commission_bill where doc_no='"
                            + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() +
                            "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "' and Tran_Type='CV'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:CVxml('" + count + "','" + action + "','CV');", true);

                    }
                    else if (sessionName == "DO_NO")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select doid from nt_1_deliveryorder where doc_no='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:DOxml('" + count + "','" + action + "');", true);

                    }
                    else if (sessionName == "PURC_NO")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select purchaseid from nt_1_sugarpurchase where doc_no='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:PSxml('" + count + "','" + action + "');", true);

                    }
                    else if (sessionName == "UT_NO")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select utrid from nt_1_utr where doc_no='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:UTxml('" + count + "','" + action + "');", true);

                    }
                    else if (sessionName == "JV_NO")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select tranid from nt_1_transacthead where doc_no='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "' and tran_type='JV'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:xml('" + count + "','" + action + "');", true);

                    }
                    else if (sessionName == "RS_NO")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select srid from nt_1_sugarsalereturn where doc_no='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:RSxml('" + count + "','" + action + "');", true);

                    }
                    else if (sessionName == "RB_NO")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select rbid from nt_1_rentbillhead where doc_no='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:RBxml('" + count + "','" + action + "');", true);

                    }
                    else if (sessionName == "PR_NO")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select prid from nt_1_sugarpurchasereturn where doc_no='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:PRxml('" + count + "','" + action + "');", true);

                    }
                    else if (sessionName == "RP_NO")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select retailid from Retail_HeadPurchase where doc_no='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:RPBxml('" + count + "','" + action + "');", true);

                    }
                    else if (sessionName == "RR_NO")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select retailid from Retail_Head where doc_no='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                        string tran_type = clsCommon.getString("select CashCredit from Retail_Head where doc_no='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:RRxml('" + count + "','" + action + "','" + tran_type + "');", true);

                    }
                    else if (sessionName == "CB_NO")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select csid from ColdStorage where doc_no='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:CBxml('" + count + "','" + action + "');", true);

                    }
                    else if (sessionName == "OI")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select Oid from other_input_gst where Doc_No='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:OIxml('" + count + "','" + action + "');", true);

                    }
                    else if (sessionName == "JS_No")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select doc_no from NT_1_JSaleHead where doc_no='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:JSxml('" + count + "','" + action + "');", true);
                    }
                    else if (sessionName == "AW_No")
                    {
                        Int32 action = 1;
                        Int32 count = Convert.ToInt32(clsCommon.getString("select doc_no from NT_1_Awakhead where doc_no='" + No + "'   and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "'"));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:AWxml('" + count + "','" + action + "');", true);
                    }
                    else
                    {
                        Session[sessionName] = No;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:" + TranType + "();", true);
                    }
                }
                else
                {
                    Session[sessionName] = No;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:" + TranType + "();", true);
                }
            }
        }
        lnkGo.Focus();
    }

    private static string GetSessionName(string TranType)
    {
        string SessionName = string.Empty;
        switch (TranType)
        {
            case "DO":
                SessionName = "DO_NO";
                break;
            case "LV":
                SessionName = "LV_NO";
                break;
            case "CV":
                SessionName = "CV_NO";
                break;
            case "OV":
                SessionName = "VOUC_NO";
                break;
            case "SB":
                SessionName = "SB_NO";
                break;
            case "PS":
                SessionName = "PURC_NO";
                break;
            case "RS":
                SessionName = "RS_NO";
                break;
            case "PR":
                SessionName = "PR_NO";
                break;
            case "UI":
                SessionName = "UT_NO";
                break;
            case "JV":
                SessionName = "JV_NO";
                break;
            case "OP":
                SessionName = "UT_NO";
                break;
            case "CR":
                SessionName = "CR";
                break;
            case "CP":
                SessionName = "CP";
                break;
            case "BR":
                SessionName = "BR";
                break;
            case "BP":
                SessionName = "BP";
                break;
            case "RB":
                SessionName = "RB_NO";
                break;
            case "RP":
                SessionName = "RP_NO";
                break;
            case "RR":
                SessionName = "RR_NO";
                break;
            case "CB":
                SessionName = "CB_NO";
                break;
            case "OI":
                SessionName = "OI";
                break;
            case "JS":
                SessionName = "JS_No";
                break;
            case "AW":
                SessionName = "AW_No";
                break;
            default:
                SessionName = "";
                break;
        }
        return SessionName;
    }

    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetail.PageIndex = e.NewPageIndex;
        if (DrCr != "DrCr")
        {
            this.OnlyCrORDr();
        }
        else
        {
            this.bindData();
        }
    }

    protected void btnPDfDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            PrintPanelmain.RenderControl(hw);
            string s1 = sw.ToString().Replace("align: center", "align: left");
            //StringReader sr = new StringReader(s1.ToString());
            StringReader sr = new StringReader(s1.ToString().Replace("../Images", Server.MapPath("~/Images")));

            Document pdfDoc = new Document(iTextSharp.text.PageSize.LEDGER);
            var writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
        catch
        {
        }
    }

    protected void 
        lnkGo1_Click(object sender, EventArgs e)
    {
        LinkButton lnkGo1 = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkGo1.NamingContainer;
        int rowIndex = row.RowIndex;
        Int16 Action = 1;
        string No = lnkGo1.Text;
        string accode = lnkGo1.Text;
        Session["DO_No"] = No;
        Int32 counts = Convert.ToInt32(clsCommon.getString("SELECT doid  from nt_1_deliveryorder where doc_no=" + accode + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "' "));

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dome", "javascript:DOxml('" + counts + "','" + Action + "');", true);
        #region

        //string dtt = "01-07-2017";
        //string dt11 = grdDetail.Rows[rowIndex].Cells[2].Text.Trim();
        //DateTime dt = DateTime.ParseExact(dtt, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        //DateTime dt1 = DateTime.ParseExact(dt11, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        //if (No != "0")
        //{
        //    if (dt1 < dt)//for old links
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dome", "javascript:DOxml('" + counts + "','" + Action + "');", true);


        //    }

        //    else
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dome", "javascript:DOxml('" + counts + "','" + Action + "');", true);


        //    }
        //}
        #endregion
    }


}
