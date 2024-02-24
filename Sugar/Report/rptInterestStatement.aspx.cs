using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Net.Mime;
using System.Net.Mail;

public partial class Report_rptInterestStatement : System.Web.UI.Page
{

    #region data section

    double netdebit = 0.00; double netcredit = 0.00; Int32 netDays = 0; double netInterest = 0.00;

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
    static WebControl objAsp = null;
    IFormatProvider ifrDT = CultureInfo.CreateSpecificCulture("en-GB");
    string DrCr = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblGLEDGER = tblPrefix + "GLEDGER";
            tblDetails = tblPrefix + "VoucherDetails";
            AccountMasterTable = tblPrefix + "AccountMaster";
            cityMasterTable = tblPrefix + "CityMaster";
            DrCr = Request.QueryString["DrCr"];
            lblCompany.Text = Session["Company_Name"].ToString();
            //http://localhost:1041/AccoWeb/Report/rptInterestStatement.aspx?accode=50047&fromdt=2014/04/01&todt=2015/03/31&intRate=15&intDays=365

            //set url parameters
            //Request.QueryString["accode"] = "4545";
            //Request.QueryString["fromdt"] = "2014/04/01";
            //Request.QueryString["todt"] = "2015/03/31";
            //Request.QueryString["intRate"] = "15";
            //Request.QueryString["intDays"] = "365";
            string accode = Request.QueryString["accode"];
            txtEmail.Text = clsCommon.getString("select Email_Id from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + accode);
            if (DrCr == "Dr")
            {
                OnlyDr();
            }
            else
            {
                bindData();
            }
        }
    }

    private void bindData()
    {
        try
        {
            string accode = Request.QueryString["accode"];
            string fromdt = Request.QueryString["fromdt"];
            string todt = Request.QueryString["todt"];
            string intrate = Request.QueryString["intRate"];
            double intRate = 0.00;
            Int32 intDays = 0;
            if (Request.QueryString["intRate"] != null)
            {
                if (Request.QueryString["intRate"] != string.Empty)
                {
                    intRate = Convert.ToDouble(Request.QueryString["intRate"]);
                }
            }
            if (Request.QueryString["intDays"] != null)
            {
                if (Request.QueryString["intDays"] != string.Empty)
                {
                    intDays = Convert.ToInt32(Request.QueryString["intDays"]);
                }
            }
            lblInterestRate.Text = intrate + "%";
            lblParty.Text = clsCommon.getString("select Ac_name_e from qrymstaccountmaster  where  Ac_Code=" + accode + " and Company_Code="
                + Convert.ToInt32(Session["Company_Code"].ToString()));
            lblFromDt.Text = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            lblToDt.Text = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");


            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            //Opening Balance Calculation
            string qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from " + tblGLEDGER
                + " where DOC_DATE < '" + fromdt + "' and Ac_code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE ";
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
            //Bind Grid
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            Int32 used_days = 0;
            double used_Amount = 0.00;
            double intAmount = 0.00;

            //qry = "select TRAN_TYPE,DOC_NO,Convert(varchar(10),DOC_DATE,102) as DOC_DATE ,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,DRCR from " + tblGLEDGER +
            //        " where AC_CODE=" + accode + " and DOC_DATE between '" + fromdt + "' and '" + todt + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by DOC_DATE asc";



            qry = "select TRAN_TYPE,DOC_NO,DOC_DATE,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,DRCR from nt_1_gledger " +
                   " where AC_CODE=" + accode + " and DOC_DATE between '" + fromdt + "' and '" + todt + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by DOC_DATE asc";
            ds = clsDAL.SimpleQuery(qry);


            DataTable dtT = new DataTable();
            dtT.Columns.Add("Tran_Type", typeof(string));
            dtT.Columns.Add("Date", typeof(DateTime));
            dtT.Columns.Add("Debit_Amount", typeof(double));
            dtT.Columns.Add("Credit_Amount", typeof(double));
            dtT.Columns.Add("Balance", typeof(double));
            dtT.Columns.Add("Bal_DC", typeof(string));
            dtT.Columns.Add("Days", typeof(Int32));
            dtT.Columns.Add("Interest", typeof(double));
            dtT.Columns.Add("Int_DC", typeof(string));

            dt = ds.Tables[0];

            DataRow dr = dtT.NewRow();
            //  old dr[0] = dt.Rows[0]["TRAN_TYPE"].ToString();
            dr[0] = "OP";
            //  dr[1] = 0.00;
            dr[1] = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd-MM-yyyy");
            // dr[3] = "Opening Balance";
            if (opBal > 0)
            {
                dr[2] = Math.Round(opBal, 2);
                dr[3] = 0.00;
                dr[4] = Math.Round(opBal, 2);
                dr[5] = "Dr";
                netdebit += opBal;
            }
            else
            {
                dr[2] = 0.00;
                dr[3] = Math.Round(-opBal, 2);
                dr[4] = Math.Round(opBal, 2);// dr[5];
                dr[5] = "Cr";
                netcredit += -opBal;
            }
            dtT.Rows.Add(dr);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dtT.NewRow();

                    dr[0] = dt.Rows[i]["TRAN_TYPE"].ToString();
                    //   dr[1] = dt.Rows[i]["DOC_NO"].ToString();
                    if (dt.Rows[i]["DOC_DATE"].ToString() != string.Empty)
                    {
                        string s = dt.Rows[i]["DOC_DATE"].ToString();

                        dr[1] = DateTime.Parse(s, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

                        //calculate date Difference & Interest of those days

                        if (i - 1 >= 0)
                        {
                            string dt1a = s.Replace('.', '-');
                            dt1a = DateTime.Parse(dt1a, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                            string dt2a = dt.Rows[i - 1]["DOC_DATE"].ToString().Replace('.', '-');
                            dt2a = DateTime.Parse(dt2a, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

                            dt1 = DateTime.Parse(s, ifrDT);
                            dt2 = DateTime.Parse(dt.Rows[i - 1]["DOC_DATE"].ToString(), ifrDT);

                            string strdays = clsCommon.getString("select datediff(day,'" + dt2a + "','" + dt1a + "') as dayss");
                            if (strdays != string.Empty)
                            {
                                used_days = Convert.ToInt32(strdays);
                                dr[6] = used_days;
                            }
                        }
                        else
                        {
                            string dt1a = s.Replace('.', '-');
                            dt1a = DateTime.Parse(dt1a, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                            string dt2a = DateTime.Parse(dtT.Rows[0][1].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                            dt1 = DateTime.Parse(s, ifrDT);
                            dt2 = DateTime.Parse(dtT.Rows[0][1].ToString(), ifrDT);

                            string strdays = clsCommon.getString("select datediff(day,'" + dt2a + "','" + dt1a + "') as dayss");
                            if (strdays != string.Empty)
                            {
                                used_days = Convert.ToInt32(strdays);
                                dr[6] = used_days;
                            }
                        }
                        netDays = netDays + used_days;
                    }
                    //      dr[3] = dt.Rows[i]["NARRATION"].ToString();
                    if (dt.Rows[i]["DRCR"].ToString() == "D")
                    {
                        opBal = opBal + Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                        dr[2] = string.Format("{0:0.00}", Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString()));
                        dr[3] = 0.00;
                        //netdebit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                        netdebit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());

                    }
                    else
                    {
                        opBal = opBal - Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                        netcredit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                        dr[2] = 0.00;
                        dr[3] = Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                    }

                    if (opBal > 0)
                    {
                        dr[4] = Math.Round(Convert.ToDouble(opBal), 2);
                        dr[5] = "Dr";
                        if (i - 1 >= 0)
                        {
                            used_Amount = Convert.ToDouble(dtT.Rows[i]["Balance"].ToString());
                            intAmount = Math.Round(((used_Amount * intRate / 100) / intDays) * used_days, 2);
                            dr[7] = intAmount;
                            dr[8] = "Dr";
                            if (dtT.Rows[i]["Bal_DC"].ToString() == "Cr")
                            {

                                dr[8] = "Cr";
                                netInterest = netInterest - intAmount;
                            }
                            else
                            {
                                dr[8] = "Dr";
                                netInterest = netInterest + intAmount;

                            }
                        }
                        else
                        {
                            used_Amount = Convert.ToDouble(dtT.Rows[0]["Balance"].ToString());
                            intAmount = Math.Round(((used_Amount * intRate / 100) / intDays) * used_days, 2);
                            dr[7] = intAmount;
                            dr[8] = "Dr";
                            netInterest = netInterest + intAmount;
                        }

                        //  netInterest = netInterest + intAmount;
                    }
                    else
                    {
                        dr[4] = Math.Abs(Math.Round(opBal, 2));
                        dr[5] = "Cr";
                        if ((i - 1) >= 0)
                        {
                            used_Amount = Convert.ToDouble(dtT.Rows[i]["Balance"].ToString());
                            intAmount = Math.Abs(Math.Round(((used_Amount * (intRate / 100)) / intDays) * used_days, 2));
                            dr[7] = intAmount;
                            // if (dr[8] == "C")
                            if (dtT.Rows[i]["Bal_DC"].ToString() == "Cr")
                            {

                                dr[8] = "Cr";
                                netInterest = netInterest - intAmount;
                            }
                            else
                            {
                                dr[8] = "Dr";
                                netInterest = netInterest + intAmount;

                            }

                        }
                        else
                        {
                            used_Amount = Convert.ToDouble(dtT.Rows[0]["Balance"].ToString());
                            intAmount = Math.Abs(Math.Round(((used_Amount * (intRate / 100)) / intDays) * used_days, 2));
                            dr[7] = intAmount;
                            if (i != 0)
                            {
                                if (dtT.Rows[i - 1]["Bal_DC"].ToString() == "Dr")
                                {
                                    netInterest = netInterest + intAmount;

                                    dr[8] = "Dr";
                                }
                                else
                                {
                                    netInterest = netInterest - intAmount;

                                    dr[8] = "Cr";
                                }
                            }
                            else
                            {
                                if (used_Amount > 0)
                                {
                                    netInterest = netInterest + intAmount;

                                    dr[8] = "Dr";
                                }
                                else
                                {
                                    netInterest = netInterest - intAmount;

                                    dr[8] = "Cr";
                                }
                            }
                        }


                    }

                    dtT.Rows.Add(dr);
                }
                //add last row 

                dr = dtT.NewRow();
                dr[0] = "";
                // dr[1] = todt;
                dr[1] = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd-MM-yyyy");

                dr[2] = 0.00;
                dr[3] = 0.00;
                dr[4] = 0.00;// dtT.Rows[dtT.Rows.Count - 1][4];
                dr[5] = dtT.Rows[dtT.Rows.Count - 1][5];
                string drcmp = Convert.ToString(dtT.Rows[dtT.Rows.Count - 1][5]);
                used_Amount = Convert.ToDouble(dtT.Rows[dtT.Rows.Count - 1][4]); //Convert.ToDouble(dr[4]);

                string dt1all = DateTime.Parse(dtT.Rows[dtT.Rows.Count - 1][1].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                string dt2all = todt;
                dt1 = DateTime.Parse(dtT.Rows[dtT.Rows.Count - 1][1].ToString(), ifrDT);
                dt2 = DateTime.Parse(todt, ifrDT);

                string strdays1 = clsCommon.getString("select datediff(day,'" + dt1all + "','" + dt2all + "')+1 as dayss");
                if (strdays1 != string.Empty)
                {
                    used_days = Convert.ToInt32(strdays1);
                    dr[6] = used_days;

                    netDays = netDays + used_days;
                }
                intAmount = Math.Round(((used_Amount * (intRate / 100)) / intDays) * (used_days), 2);
                if (drcmp == "Cr")
                {
                    netInterest = netInterest - intAmount;

                }
                else
                {
                    netInterest = netInterest + intAmount;
                }

                dr[7] = intAmount;
                //  if (netInterest > 0)
                if (dtT.Rows[dtT.Rows.Count - 1]["Bal_DC"].ToString() == "Dr")
                {
                    dr[8] = "Dr";
                }
                else
                {
                    dr[8] = "Cr";
                }
                dtT.Rows.Add(dr);
            }

            else
            {
                dr = dtT.NewRow();
                dr[0] = "";
                // dr[1] = lblToDt.Text;
                dr[1] = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd-MM-yyyy");

                dr[3] = "0";


                if (opBal > 0)
                {


                    dr[4] = Math.Round(opBal, 2);
                    dr[5] = "Dr";

                }
                else
                {

                    dr[4] = Math.Round(opBal, 2);// dr[5];
                    dr[5] = "Cr";

                }

                string dt1all = DateTime.Parse(dtT.Rows[dtT.Rows.Count - 1][1].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                string dt2all = todt;
                dt1 = DateTime.Parse(dtT.Rows[dtT.Rows.Count - 1][1].ToString(), ifrDT);
                dt2 = DateTime.Parse(todt, ifrDT);

                string strdays1 = clsCommon.getString("select datediff(day,'" + dt1all + "','" + dt2all + "')+1 as dayss");


                //  string strdays1 = clsCommon.getString("select datediff('" + dt2all + "','" + dt1all + "')+1 as dayss");
                // string strdays1 = clsCommon.getString("select datediff(day,'" + dt2all + "','" + dt1all + "')+1 as dayss");
                if (strdays1 != string.Empty)
                {
                    used_days = Convert.ToInt32(strdays1);
                    dr[6] = used_days;


                }


                intAmount = Math.Round(((opBal * (intRate / 100)) / intDays) * (used_days), 2);
                netInterest = intAmount;
                dr[7] = intAmount;
                if (intAmount > 0)
                {
                    dr[8] = "Dr";
                }
                else
                {
                    dr[8] = "Cr";
                }


                dtT.Rows.Add(dr);
                //grdDetail.FooterRow.Cells[0].Text = lblToDt.Text;

            }

            grdDetail.DataSource = dtT;
            grdDetail.DataBind();

            grdDetail.FooterRow.Cells[0].Text = "Total";
            grdDetail.FooterRow.Cells[2].Text = netdebit.ToString();
            grdDetail.FooterRow.Cells[3].Text = netcredit.ToString();
            if (netdebit - netcredit != 0)
            {
                grdDetail.FooterRow.Cells[4].Text = Math.Round((netdebit - netcredit), 2).ToString();
            }
            else
            {
                grdDetail.FooterRow.Cells[4].Text = "Nil";

            }
            grdDetail.FooterRow.Cells[6].Text = netDays.ToString();
            grdDetail.FooterRow.Cells[7].Text = Math.Round(netInterest, 2).ToString();
            if (netInterest > 0)
            {
                grdDetail.FooterRow.Cells[8].Text = "Dr";
            }
            else
            {
                grdDetail.FooterRow.Cells[8].Text = "Cr";
            }
            grdDetail.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private void OnlyDr()
    {
        try
        {
            string accode = Request.QueryString["accode"];
            string fromdt = Request.QueryString["fromdt"];
            string todt = Request.QueryString["todt"];
            double intRate = 0.00;
            Int32 intDays = 0;
            if (Request.QueryString["intRate"] != null)
            {
                if (Request.QueryString["intRate"] != string.Empty)
                {
                    intRate = Convert.ToDouble(Request.QueryString["intRate"]);
                }
            }
            if (Request.QueryString["intDays"] != null)
            {
                if (Request.QueryString["intDays"] != string.Empty)
                {
                    intDays = Convert.ToInt32(Request.QueryString["intDays"]);
                }
            }

            lblParty.Text = clsCommon.getString("select Ac_name_e from qrymstaccountmaster  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            lblFromDt.Text = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            lblToDt.Text = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            //Opening Balance Calculation
            string qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from " + tblGLEDGER + " where DOC_DATE < '" + fromdt + "' and Ac_code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE ";
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
            //Bind Grid
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            Int32 used_days = 0;
            double used_Amount = 0.00;
            double intAmount = 0.00;

            //qry = "select TRAN_TYPE,DOC_NO,Convert(varchar(10),DOC_DATE,102) as DOC_DATE,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,DRCR from " + tblGLEDGER +
            //      " where AC_CODE=" + accode + " and DOC_DATE between '" + fromdt + "' and '" + todt + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by DOC_DATE asc";



            qry = "select TRAN_TYPE,DOC_NO,DOC_DATE,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,DRCR from nt_1_gledger " +
                " where AC_CODE=" + accode + " and DOC_DATE between '" + fromdt + "' and '" + todt + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by DOC_DATE asc";
            ds = clsDAL.SimpleQuery(qry);

            DataTable dtT = new DataTable();
            dtT.Columns.Add("Tran_Type", typeof(string));
            dtT.Columns.Add("Date", typeof(string));
            dtT.Columns.Add("Debit_Amount", typeof(double));
            dtT.Columns.Add("Credit_Amount", typeof(double));
            dtT.Columns.Add("Balance", typeof(double));
            dtT.Columns.Add("Bal_DC", typeof(string));
            dtT.Columns.Add("Days", typeof(Int32));
            dtT.Columns.Add("Interest", typeof(double));
            dtT.Columns.Add("Int_DC", typeof(string));

            dt = ds.Tables[0];
            DataRow dr = dtT.NewRow();
            //  old dr[0] = dt.Rows[0]["TRAN_TYPE"].ToString();
            dr[0] = "OP";
            //  dr[1] = 0.00;
            dr[1] = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd-MM-yyyy");
            // dr[3] = "Opening Balance";
            if (opBal > 0)
            {
                dr[2] = Math.Round(opBal, 2);
                dr[3] = 0.00;
                dr[4] = Math.Round(opBal, 2);
                dr[5] = "Dr";
                netdebit += opBal;
            }
            else
            {
                dr[2] = 0.00;
                dr[3] = Math.Round(-opBal, 2);
                dr[4] = Math.Round(opBal, 2);// dr[5];
                dr[5] = "Cr";
                netcredit += -opBal;
            }
            dtT.Rows.Add(dr);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dtT.NewRow();

                    dr[0] = dt.Rows[i]["TRAN_TYPE"].ToString();
                    //   dr[1] = dt.Rows[i]["DOC_NO"].ToString();
                    if (dt.Rows[i]["DOC_DATE"].ToString() != string.Empty)
                    {
                        string s = dt.Rows[i]["DOC_DATE"].ToString();

                        dr[1] = DateTime.Parse(s, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                        //calculate date Difference & Interest of those days
                        if (i - 1 >= 0)
                        {
                            string dt1a = s.Replace('.', '-');
                            dt1a = DateTime.Parse(dt1a, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                            string dt2a = dt.Rows[i - 1]["DOC_DATE"].ToString().Replace('.', '-');
                            dt2a = DateTime.Parse(dt2a, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

                            dt1 = DateTime.Parse(s, ifrDT);
                            dt2 = DateTime.Parse(dt.Rows[i - 1]["DOC_DATE"].ToString(), ifrDT);

                            //string strdays = clsCommon.getString("select datediff(day,'" + dt2a + "','" + dt1a + "') as dayss");

                            string strdays = clsCommon.getString("select datediff(day,'" + dt2a + "','" + dt1a + "') as dayss");
                            if (strdays != string.Empty)
                            {
                                used_days = Convert.ToInt32(strdays);
                                dr[6] = used_days;
                            }
                        }
                        else
                        {
                            string dt1a = s.Replace('.', '-');
                            dt1a = DateTime.Parse(dt1a, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                            string dt2a = DateTime.Parse(dtT.Rows[0][1].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                            dt1 = DateTime.Parse(s, ifrDT);
                            dt2 = DateTime.Parse(dtT.Rows[0][1].ToString(), ifrDT);

                            string strdays = clsCommon.getString("select datediff(day,'" + dt2a + "','" + dt1a + "') as dayss");
                            if (strdays != string.Empty)
                            {
                                used_days = Convert.ToInt32(strdays);
                                dr[6] = used_days;
                            }
                        }
                        netDays = netDays + used_days;
                    }
                    //      dr[3] = dt.Rows[i]["NARRATION"].ToString();
                    if (dt.Rows[i]["DRCR"].ToString() == "D")
                    {
                        opBal = opBal + Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                        dr[2] = string.Format("{0:0.00}", Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString()));
                        dr[3] = 0.00;
                        netdebit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                    }
                    else
                    {
                        opBal = opBal - Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                        netcredit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                        dr[2] = 0.00;
                        dr[3] = Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                    }

                    if (opBal > 0)
                    {
                        dr[4] = Math.Round(Convert.ToDouble(opBal), 2);
                        dr[5] = "Dr";
                        if (i - 1 >= 0)
                        {
                            used_Amount = Convert.ToDouble(dtT.Rows[i]["Balance"].ToString());
                            intAmount = Math.Round(((used_Amount * intRate / 100) / intDays) * used_days, 2);
                            dr[7] = intAmount;
                            dr[8] = "Dr";
                        }
                        else
                        {
                            used_Amount = Convert.ToDouble(dtT.Rows[0]["Balance"].ToString());
                            intAmount = Math.Round(((used_Amount * intRate / 100) / intDays) * used_days, 2);
                            dr[7] = intAmount;
                            dr[8] = "Dr";
                        }

                        netInterest = netInterest + intAmount;
                    }
                    else
                    {
                        dr[4] = Math.Round(opBal, 2);
                        dr[5] = "Cr";
                        if (i - 1 >= 0)
                        {
                            used_Amount = Convert.ToDouble(dtT.Rows[i]["Balance"].ToString());
                            intAmount = 0;// Math.Round(((used_Amount * (intRate / 100)) / intDays) * used_days, 2);
                            dr[7] = intAmount;
                            dr[8] = "Cr";
                        }
                        else
                        {
                            used_Amount = Convert.ToDouble(dtT.Rows[0]["Balance"].ToString());
                            intAmount = 0;// Math.Round(((used_Amount * (intRate / 100)) / intDays) * used_days, 2);
                            dr[7] = intAmount;
                            dr[8] = "Cr";
                        }

                        netInterest = netInterest - intAmount;
                    }

                    dtT.Rows.Add(dr);
                }
                //add last row 

                dr = dtT.NewRow();
                dr[0] = "";
                dr[1] = todt;
                dr[2] = 0.00;
                dr[3] = 0.00;
                dr[4] = dtT.Rows[dtT.Rows.Count - 1][4];
                dr[5] = dtT.Rows[dtT.Rows.Count - 1][5];

                used_Amount = Convert.ToDouble(dr[4]);

                string dt1all = DateTime.Parse(dtT.Rows[dtT.Rows.Count - 1][1].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
                string dt2all = todt;
                dt1 = DateTime.Parse(dtT.Rows[dtT.Rows.Count - 1][1].ToString(), ifrDT);
                dt2 = DateTime.Parse(todt, ifrDT);

                string strdays1 = clsCommon.getString("select datediff(day,'" + dt1all + "','" + dt2all + "')+1 as dayss");
                if (strdays1 != string.Empty)
                {
                    used_days = Convert.ToInt32(strdays1);
                    dr[6] = used_days;

                    netDays = netDays + used_days;
                }
                //   intAmount = Math.Round(((used_Amount * (intRate / 100)) / intDays) * (used_days + 1), 2);

                intAmount = Math.Round(((used_Amount * (intRate / 100)) / intDays) * (used_days), 2);
                netInterest = netInterest + intAmount;
                dr[7] = intAmount;
                if (intAmount > 0)
                {
                    dr[8] = "Dr";
                }
                else
                {
                    dr[8] = "Cr";
                }
                dtT.Rows.Add(dr);
            }

            grdDetail.DataSource = dtT;
            grdDetail.DataBind();

            grdDetail.FooterRow.Cells[0].Text = "Total";
            grdDetail.FooterRow.Cells[2].Text = netdebit.ToString();
            grdDetail.FooterRow.Cells[3].Text = netcredit.ToString();
            if (netdebit - netcredit != 0)
            {
                grdDetail.FooterRow.Cells[4].Text = Math.Round((netdebit - netcredit), 2).ToString();
            }
            else
            {
                grdDetail.FooterRow.Cells[4].Text = "Nil";

            }

            grdDetail.FooterRow.Cells[6].Text = netDays.ToString();
            grdDetail.FooterRow.Cells[7].Text = Math.Round(netInterest, 2).ToString();
            if (netInterest > 0)
            {
                grdDetail.FooterRow.Cells[8].Text = "Dr";
            }
            else
            {
                grdDetail.FooterRow.Cells[8].Text = "Cr";
            }
            grdDetail.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
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
                    contentType.Name = "InterestStatement.htm";
                    Attachment attachment = new Attachment(ms, contentType);

                    string mailFrom = Session["EmailId"].ToString();
                    string smtpPort = "587";
                    string emailPassword = Session["EmailPassword"].ToString();
                    MailMessage msg = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                    SmtpServer.Host = clsGV.Email_Address;
                    msg.From = new MailAddress(mailFrom);
                    msg.To.Add(mail);
                    msg.Body = "Interest Statement";
                    msg.Attachments.Add(attachment);
                    msg.IsBodyHtml = true;
                    msg.Subject = "Interest Statement";
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
}