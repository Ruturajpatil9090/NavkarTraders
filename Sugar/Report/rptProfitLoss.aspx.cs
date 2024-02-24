using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System.Configuration;


public partial class Report_rptProfitLoss : System.Web.UI.Page
{
    string f = "../GSReports/ProfitLoss_.htm";
    string f_Main = "../Report/rptProfitLoss";
    string email = string.Empty;
    string qryCommon = string.Empty;
    string tblPrefix = string.Empty;
    string cityMasterTable = string.Empty;
    string tblHead = string.Empty;
    double netDebit = 0.00;
    double netCredit = 0.00;
    double netProfit = 0.00;
    double netLoss = 0.00;
    double totalDebit = 0.00;
    double totalCredit = 0.00;
    string s;
    double p_netDebit = 0.00;
    double p_netCredit = 0.00;
    double p_netProfit = 0.00;
    double p_netLoss = 0.00;
    double p_totalDebit = 0.00;
    double p_totalCredit = 0.00;
    string prefix = string.Empty;
    string frmdt = string.Empty;
    string todate = string.Empty;
    DataSet ds = null;
    DataTable dt = null;
    string cs = string.Empty;
    SqlConnection con = null;
    private static SqlDataAdapter _adapter = null;
    private static DataSet _ds = null;
    // string tblPrefix = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        con = new SqlConnection(cs);

        if (!Page.IsPostBack)
        {
            user = Session["user"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {

                if (Session["tblPrefix"] != null)
                {
                    tblPrefix = Session["tblPrefix"].ToString();
                }
                else
                {
                    prefix = clsCommon.getString("Select tblPrefix from tblPrefix");
                    tblPrefix = prefix.ToString();
                }
                qryCommon = "qrygledger";
                tblHead = tblPrefix + "BSGroupMaster";
                cityMasterTable = tblPrefix + "CityMaster";
                ViewState["VNO"] = Request.QueryString["VNO"];
                ViewState["mailID"] = Request.QueryString["mailID"];
                ViewState["pageBreak"] = Request.QueryString["pageBreak"];

                ViewState["BalDate"] = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                frmdt = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                todate = Request.QueryString["dt"];
                todate = DateTime.Parse(todate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

                lblCompany.Text = Session["Company_Name"].ToString();
                lblCompanyAddr.Text = clsGV.CompanyAddress;
                this.bindData();
                this.bindData_p();
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }

    private void bindData()
    {
        try
        {

            //XElement root = new XElement("ROOT");
            //XElement child1 = new XElement("Head");
            //int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
            //int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
            //string Cwhere = todate; // DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            //child1.SetAttributeValue("Company_Code", Company_Code);
            //child1.SetAttributeValue("year", Year_Code);
            //child1.SetAttributeValue("to_date", Cwhere);
            //child1.SetAttributeValue("from_Date", frmdt);

            //root.Add(child1);
            //string XMLReport = root.ToString();
            //XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
            //XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
            //string xmlfile = XMLReport;

            //ds = clsDAL.xmlExecuteDMLQryReport1("Report_ProfitLoss", xmlfile);


            int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
            int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
            string Cwhere = todate;



            using (clsDataProvider objDataProvider = new clsDataProvider())
            {
                con.Open();

                SqlCommand sqlcmd = new SqlCommand("Report_ProfitLoss", con);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("Company_Code", Company_Code);
                sqlcmd.Parameters.AddWithValue("year_code", Year_Code);
                sqlcmd.Parameters.AddWithValue("to_date", Cwhere);
                sqlcmd.Parameters.AddWithValue("from_Date", frmdt);
                sqlcmd.ExecuteNonQuery();

                _adapter = new SqlDataAdapter(sqlcmd);
                _ds = new DataSet();
                _adapter.Fill(_ds);
                ds = _ds;
                con.Close();
            }





            DataTable dtLeft = new DataTable();
            DataTable dtRight = new DataTable();


            dtLeft.Columns.Add("Group_Code", typeof(string));
            dtLeft.Columns.Add("groupname", typeof(string));
            dtLeft.Columns.Add("groupamount", typeof(double));
            dtLeft.Columns.Add("summary", typeof(string));

            dtRight.Columns.Add("Group_Code", typeof(string));
            dtRight.Columns.Add("groupname", typeof(string));
            dtRight.Columns.Add("groupamount", typeof(double));
            dtRight.Columns.Add("summary", typeof(string));


            //  DataTable dt = new DataTable();
            DataTable dtTemp = new DataTable();
            //string qry = "";
            //qry = "select Group_Code,BSGroupName,group_Summary,group_Order," +
            //    "SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance   from "
            //    + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
            //    + " and group_Type='T' and DOC_DATE<='" + ViewState["BalDate"].ToString()
            //    + "' and DOC_DATE>='" + frmdt + "' group by Group_Code,BSGroupName,group_Summary," +
            //    "group_Order order by group_Order";
            // ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //if (dt.Rows[i]["group_Summary"].ToString() == "Y")
                            //{                                       
                            double bal = Convert.ToDouble(dt.Rows[i]["Balance"].ToString()) + 0.00;
                            if (bal != 0)
                            {
                                if (bal > 0)
                                {
                                    DataRow dr = dtRight.NewRow();
                                    dr["Group_Code"] = dt.Rows[i]["Group_Code"].ToString();
                                    dr["groupname"] = dt.Rows[i]["BSGroupName"].ToString();
                                    dr["groupamount"] = (bal).ToString("#.##");
                                    dr["summary"] = dt.Rows[i]["group_Summary"].ToString();
                                    dtRight.Rows.Add(dr);

                                    netDebit += bal;
                                }
                                else
                                {
                                    DataRow dr = dtLeft.NewRow();
                                    dr["Group_Code"] = dt.Rows[i]["Group_Code"].ToString();
                                    dr["groupname"] = dt.Rows[i]["BSGroupName"].ToString();
                                    dr["groupamount"] = (-bal).ToString("#.##");
                                    dr["summary"] = dt.Rows[i]["group_Summary"].ToString();
                                    dtLeft.Rows.Add(dr);
                                    netCredit += -bal;
                                }
                            }
                        }


                        dtl_TradingDebit.DataSource = dtLeft;
                        dtl_TradingDebit.DataBind();

                        dtl_TradingCredit.DataSource = dtRight;
                        dtl_TradingCredit.DataBind();

                        lblNetDebit.Text = netDebit.ToString();
                        lblNetCredit.Text = netCredit.ToString();

                        if (netCredit - netDebit > 0)
                        {
                            netProfit = (netCredit - netDebit);
                            lblnetProfit.Text = Convert.ToDecimal((netProfit).ToString()).ToString("0.00");
                        }
                        else
                        {
                            netLoss = (netDebit - netCredit);
                            lblnetLoss.Text = Convert.ToDecimal(netLoss.ToString()).ToString("0.00");
                        }

                        if (netProfit == 0)
                        {
                            lblnetprofithead.Text = "";
                            lblnetProfit.Text = "";
                        }
                        if (netLoss == 0)
                        {
                            lblnetLoss.Text = "";
                            lblnetlosshead.Text = "";

                        }

                        lblTotal_L.Text = Convert.ToDecimal((netDebit + netProfit).ToString()).ToString("0.00");
                        lblTotal_R.Text = Convert.ToDecimal((netCredit + netLoss).ToString()).ToString("0.00");
                    }
                }
            }

        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
    }

    protected void dtl_TradingDebit_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        #region
        //try
        //{
        //    DataList dtl_TradingDebitInner = (DataList)e.Item.FindControl("dtl_TradingDebitInner");
        //    Label lblsummaryL = (Label)e.Item.FindControl("lblsummaryL");
        //    Label lblGroupCodeL = (Label)e.Item.FindControl("lblGroupCodeL");

        //    DataTable dtLInner = new DataTable();
        //    dtLInner.Columns.Add("AC_CODE", typeof(string));
        //    dtLInner.Columns.Add("acname", typeof(string));
        //    dtLInner.Columns.Add("acamount", typeof(double));

        //    if (lblsummaryL.Text == "Y")
        //    {

        //    }
        //    else    //summary no show detail part
        //    {

        //        DataRow[] results = dt.Select("Group_Code=" + lblGroupCodeL.Text + " and Balance <> 0 ");
        //        foreach (DataRow row in results)
        //        {
        //            double bal = Convert.ToDouble(row[4]);
        //            if (bal < 0)         //  >0  right side asset
        //            {
        //                DataRow dr = dtLInner.NewRow();
        //                dr["acamount"] = (-bal).ToString();
        //                dtLInner.Rows.Add(dr);
        //            }
        //            else
        //            {
        //                DataRow dr = dtLInner.NewRow();
        //                dr["acamount"] = (-bal).ToString();
        //                dtLInner.Rows.Add(dr);
        //            }
        //        }
        //        dtl_TradingDebitInner.DataSource = dtLInner;
        //        dtl_TradingDebitInner.DataBind();

        //    }
        //}
        //catch (Exception exxx)
        //{
        //    //Response.Write("left item command err:" + exxx.Message);
        //}
        #endregion

        try
        {
            string qry = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            DataTable dtLInner = new DataTable();
            dtLInner.Columns.Add("acname", typeof(string));
            dtLInner.Columns.Add("acamount", typeof(double));
            dtLInner.Columns.Add("AC_CODE", typeof(double));

            DataList dtl_TradingDebitInner = (DataList)e.Item.FindControl("dtl_TradingDebitInner");

            Label lblsummaryL = (Label)e.Item.FindControl("lblsummaryL");
            Label lblGroupCodeL = (Label)e.Item.FindControl("lblGroupCodeL");

            if (lblsummaryL.Text == "Y")
            {

            }
            else    //summary no show detail part
            {
                qry = "";
                qry =
                "select AC_CODE, Ac_Name_E,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance " +
                " from " + qryCommon + " " +
                " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE<='" + ViewState["BalDate"].ToString()
                + "'  and DOC_DATE>='" + frmdt + "' and Group_Code=" + lblGroupCodeL.Text + " " +
                " group by AC_CODE,Ac_Name_E having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) !=0";
                ds = clsDAL.SimpleQuery(qry);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                double bal = Convert.ToDouble(dt.Rows[i]["Balance"].ToString());
                                if (bal < 0)         //  >0  right side asset
                                {
                                    DataRow dr = dtLInner.NewRow();
                                    dr["AC_CODE"] = dt.Rows[i]["AC_CODE"].ToString();
                                    dr["acname"] = dt.Rows[i]["Ac_Name_E"].ToString();
                                    dr["acamount"] = (-bal).ToString();

                                    dtLInner.Rows.Add(dr);
                                }
                                else
                                {
                                    DataRow dr = dtLInner.NewRow();
                                    dr["AC_CODE"] = dt.Rows[i]["AC_CODE"].ToString();
                                    dr["acname"] = dt.Rows[i]["Ac_Name_E"].ToString();
                                    dr["acamount"] = (-bal).ToString();
                                    dtLInner.Rows.Add(dr);
                                }

                            }
                            dtl_TradingDebitInner.DataSource = dtLInner;
                            dtl_TradingDebitInner.DataBind();


                        }
                    }
                }
            }
        }
        catch (Exception exxx)
        {
            Response.Write("left item command err:" + exxx.Message);
        }
    }

    protected void dtl_TradingCredit_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            DataList dtl_TradingCreditInner = (DataList)e.Item.FindControl("dtl_TradingCreditInner");

            Label lblsummaryR = (Label)e.Item.FindControl("lblsummaryR");
            Label lblGroupCodeR = (Label)e.Item.FindControl("lblGroupCodeR");

            string qry = "";


            DataTable dtRInner = new DataTable();
            dtRInner.Columns.Add("AC_CODE", typeof(string));
            dtRInner.Columns.Add("acname", typeof(string));
            dtRInner.Columns.Add("acamount", typeof(double));



            if (lblsummaryR.Text == "Y")
            {

            }
            else    //summary no show detail part
            {
                qry = "";
                qry =
                "select AC_CODE, Ac_Name_E,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance " +
                " from " + qryCommon + " " +
                " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE<='" + todate
                + "'  and DOC_DATE>='" + frmdt + "' and Group_Code=" + lblGroupCodeR.Text + " " +
                " group by AC_CODE,Ac_Name_E having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) !=0";
                ds = clsDAL.SimpleQuery(qry);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                double bal = Convert.ToDouble(dt.Rows[i]["Balance"].ToString());
                                if (bal > 0)         //  <0  left side liabilities
                                {
                                    DataRow dr = dtRInner.NewRow();
                                    dr["AC_CODE"] = dt.Rows[i]["AC_CODE"].ToString();
                                    dr["acname"] = dt.Rows[i]["Ac_Name_E"].ToString();
                                    dr["acamount"] = (bal).ToString();
                                    dtRInner.Rows.Add(dr);
                                }
                                else
                                {
                                    DataRow dr = dtRInner.NewRow();
                                    dr["AC_CODE"] = dt.Rows[i]["AC_CODE"].ToString();
                                    dr["acname"] = dt.Rows[i]["Ac_Name_E"].ToString();
                                    dr["acamount"] = (-bal).ToString();

                                    dtRInner.Rows.Add(dr);
                                }
                            }
                            if (dtRInner.Rows.Count > 0)
                            {
                                dtl_TradingCreditInner.DataSource = dtRInner;
                                dtl_TradingCreditInner.DataBind();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ee)
        {
            Response.Write("right item command err:" + ee.Message);
        }
    }



    private void bindData_p()
    {
        try
        {
            DataTable dtLeft = new DataTable();
            DataTable dtRight = new DataTable();

            dtLeft.Columns.Add("Group_Code", typeof(string));
            dtLeft.Columns.Add("groupname", typeof(string));
            dtLeft.Columns.Add("groupamount", typeof(double));
            dtLeft.Columns.Add("summary", typeof(string));

            dtRight.Columns.Add("Group_Code", typeof(string));
            dtRight.Columns.Add("groupname", typeof(string));
            dtRight.Columns.Add("groupamount", typeof(double));
            dtRight.Columns.Add("summary", typeof(string));

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dtTemp = new DataTable();
            string qry = "";
            qry = "select Group_Code,group_Name_E as BSGroupName,group_Summary,group_Order,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance   from "
                + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and group_Type='P' and DOC_DATE<='"
                + todate + "'  and DOC_DATE>='" + frmdt + "' and DOC_DATE<='" + todate + "' group by Group_Code,group_Name_E,group_Summary,group_Order order by group_Order";
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                        if (netProfit > 0)
                        {
                            p_netProfit = netProfit;
                            DataRow drNew = dtLeft.NewRow();
                            drNew["Group_Code"] = 0;
                            drNew["groupname"] = "Gross Profit";
                            drNew["groupamount"] = netProfit;
                            drNew["summary"] = "N";
                            dtLeft.Rows.Add(drNew);
                        }
                        if (netLoss > 0)
                        {
                            p_netLoss = netLoss;
                            DataRow drNew = dtRight.NewRow();
                            drNew["Group_Code"] = 0;
                            drNew["groupname"] = "Gross Profit";
                            drNew["groupamount"] = netLoss;
                            drNew["summary"] = "N";
                            dtRight.Rows.Add(drNew);
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //if (dt.Rows[i]["group_Summary"].ToString() == "Y")
                            //{               
                            lblNetCredit1.Text = Convert.ToString(p_netDebit);
                            lblNetDebit1.Text = Convert.ToString(p_netCredit);
                            double bal = Convert.ToDouble(dt.Rows[i]["Balance"].ToString()) + 0.00;
                            if (bal != 0)
                            {
                                if (bal > 0)
                                {
                                    DataRow dr = dtRight.NewRow();
                                    dr["Group_Code"] = dt.Rows[i]["Group_Code"].ToString();
                                    dr["groupname"] = dt.Rows[i]["BSGroupName"].ToString();
                                    dr["groupamount"] = (bal).ToString("#.##");
                                    dr["summary"] = dt.Rows[i]["group_Summary"].ToString();
                                    dtRight.Rows.Add(dr);
                                    p_netDebit += bal;
                                    lblNetCredit1.Text = Convert.ToString(p_netDebit);
                                }
                                else
                                {
                                    DataRow dr = dtLeft.NewRow();
                                    dr["Group_Code"] = dt.Rows[i]["Group_Code"].ToString();
                                    dr["groupname"] = dt.Rows[i]["BSGroupName"].ToString();
                                    dr["groupamount"] = (-bal).ToString("#.##");
                                    dr["summary"] = dt.Rows[i]["group_Summary"].ToString();
                                    dtLeft.Rows.Add(dr);
                                    p_netCredit += -bal + p_netProfit;
                                    lblNetDebit1.Text = Convert.ToString(p_netCredit);
                                }
                            }
                        }


                        dtl_ProfitDebit.DataSource = dtLeft;
                        dtl_ProfitDebit.DataBind();

                        double expensesTotal = 0.0;
                        double incomeTotal = 0.0;
                        dtl_ProfitCredit.DataSource = dtRight;
                        dtl_ProfitCredit.DataBind();
                        if (dtLeft.Rows.Count > 0)
                        {
                            incomeTotal = Convert.ToDouble(dtLeft.Compute("SUM(groupamount)", string.Empty));
                            lblNetDebit1.Text = Convert.ToDecimal(incomeTotal.ToString()).ToString("0.00");
                        }

                        if (dtRight.Rows.Count > 0)
                        {
                            expensesTotal = Convert.ToDouble(dtRight.Compute("SUM(groupamount)", string.Empty));
                            lblNetCredit1.Text = Convert.ToDecimal(expensesTotal.ToString()).ToString("0.00");
                        }


                        //Label5.Text = p_netDebit.ToString();
                        //Label8.Text = p_netCredit.ToString();
                        double netlossnew = 0.00;
                        double totnetprofit = 0.00;
                        double totnetloss = 0.00;
                        if ((incomeTotal - expensesTotal) > 0)
                        {
                            lblNetProfitAnkush.Text = Convert.ToString(Math.Round((incomeTotal - expensesTotal), 2));
                            totnetprofit = Convert.ToDouble(lblNetProfitAnkush.Text);
                        }
                        else
                        {
                            lblNetLossAnkush.Text = Convert.ToString(Math.Round((incomeTotal - expensesTotal), 2));
                            totnetloss = Convert.ToDouble(lblNetLossAnkush.Text);
                        }

                        //if (p_netDebit - p_netCredit > 0)
                        //{
                        //    p_netProfit = (p_netDebit - p_netCredit);
                        //    netlossnew = p_netProfit;
                        //    lblNetLossAnkush.Text = (netlossnew).ToString();
                        //    Label9.Text = "";
                        //}
                        //else
                        //{
                        //    p_netLoss = (p_netCredit - p_netDebit);
                        //    lblNetProfitAnkush.Text = p_netLoss.ToString();
                        //    Label11.Text = "";
                        //}

                        //if (p_netProfit == 0)
                        //{
                        //    //lblnetprofithead.Text = "";
                        //    lblNetLossAnkush.Text = "";
                        //}
                        //if (p_netLoss == 0)
                        //{
                        //    //lblnetLoss.Text = "";
                        //    lblNetProfitAnkush.Text = "";

                        //}

                        // lblTotalDebitAll.Text = Convert.ToDecimal((p_netCredit + totnetloss).ToString()).ToString("0.00");
                        //lblTotalCreditAll.Text = Convert.ToDecimal((totnetprofit + p_netDebit).ToString()).ToString("0.00");
                        //lblTotalCreditAll.Text = Convert.ToDecimal((expensesTotal + incomeTotal).ToString()).ToString("0.00");
                        lblTotalDebitAll.Text = Convert.ToDecimal((incomeTotal + totnetloss).ToString()).ToString("0.00");
                        lblTotalCreditAll.Text = Convert.ToDecimal((totnetprofit + expensesTotal).ToString()).ToString("0.00");

                    }
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void dtl_ProfitDebit_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            string qry = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            DataTable dtLInner = new DataTable();
            dtLInner.Columns.Add("AC_CODE", typeof(string));
            dtLInner.Columns.Add("acname", typeof(string));
            dtLInner.Columns.Add("acamount", typeof(double));


            DataList dtl_ProfitDebitInner = (DataList)e.Item.FindControl("dtl_ProfitDebitInner");

            Label lblsummaryL = (Label)e.Item.FindControl("lblsummaryL");
            Label lblGroupCodeL = (Label)e.Item.FindControl("lblGroupCodeL");

            if (lblsummaryL.Text == "Y")
            {

            }
            else    //summary no show detail part
            {
                qry = "";
                qry =
                "select AC_CODE, Ac_Name_E,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance " +
                " from " + qryCommon + " " +
                " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE<='" + todate
                + "'  and DOC_DATE>='" + frmdt + "' and Group_Code=" + lblGroupCodeL.Text + " " +
                " group by AC_CODE,Ac_Name_E having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) !=0";
                ds = clsDAL.SimpleQuery(qry);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                double bal = Convert.ToDouble(dt.Rows[i]["Balance"].ToString());
                                if (bal < 0)         //  >0  right side asset
                                {
                                    DataRow dr = dtLInner.NewRow();
                                    dr["AC_CODE"] = dt.Rows[i]["AC_CODE"].ToString();
                                    dr["acname"] = dt.Rows[i]["Ac_Name_E"].ToString();
                                    dr["acamount"] = (-bal).ToString();

                                    dtLInner.Rows.Add(dr);
                                }
                                else
                                {
                                    DataRow dr = dtLInner.NewRow();
                                    dr["AC_CODE"] = dt.Rows[i]["AC_CODE"].ToString();
                                    dr["acname"] = dt.Rows[i]["Ac_Name_E"].ToString();
                                    dr["acamount"] = (-bal).ToString();

                                    dtLInner.Rows.Add(dr);
                                }

                            }
                            // dtLInner.Compute("SUM(acamount)", string.Empty);

                            dtl_ProfitDebitInner.DataSource = dtLInner;
                            dtl_ProfitDebitInner.DataBind();
                        }
                    }
                }
            }
        }
        catch (Exception exxx)
        {
            Response.Write("left item command err:" + exxx.Message);
        }
    }


    protected void dtl_ProfitCredit_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            string qry = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            DataTable dtRInner = new DataTable();
            dtRInner.Columns.Add("AC_CODE", typeof(string));
            dtRInner.Columns.Add("acname", typeof(string));
            dtRInner.Columns.Add("acamount", typeof(double));


            DataList dtl_ProfitCreditInner = (DataList)e.Item.FindControl("dtl_ProfitCreditInner");

            Label lblsummaryR = (Label)e.Item.FindControl("lblsummaryR");
            Label lblGroupCodeR = (Label)e.Item.FindControl("lblGroupCodeR");

            if (lblsummaryR.Text == "Y")
            {

            }
            else    //summary no show detail part
            {
                qry = "";
                qry =
                "select AC_CODE, Ac_Name_E,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance " +
                " from " + qryCommon + " " +
                " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE<='" + todate
                + "'  and DOC_DATE>='" + frmdt + "' and Group_Code=" + lblGroupCodeR.Text + " " +
                " group by AC_CODE,Ac_Name_E having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) !=0";
                ds = clsDAL.SimpleQuery(qry);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                double bal = Convert.ToDouble(dt.Rows[i]["Balance"].ToString());
                                if (bal > 0)         //  <0  left side liabilities
                                {
                                    DataRow dr = dtRInner.NewRow();
                                    dr["AC_CODE"] = dt.Rows[i]["AC_CODE"].ToString();
                                    dr["acname"] = dt.Rows[i]["Ac_Name_E"].ToString();
                                    dr["acamount"] = (bal).ToString();

                                    dtRInner.Rows.Add(dr);
                                }
                                else
                                {
                                    DataRow dr = dtRInner.NewRow();
                                    dr["AC_CODE"] = dt.Rows[i]["AC_CODE"].ToString();
                                    dr["acname"] = dt.Rows[i]["Ac_Name_E"].ToString();
                                    dr["acamount"] = (-bal).ToString();

                                    dtRInner.Rows.Add(dr);
                                }
                            }
                            if (dtRInner.Rows.Count > 0)
                            {
                                dtl_ProfitCreditInner.DataSource = dtRInner;
                                dtl_ProfitCreditInner.DataBind();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ee)
        {
            Response.Write("right item command err:" + ee.Message);
        }
    }



    protected void lnkDO_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkOV = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkOV.NamingContainer;
            Label lblaccode = (Label)item.FindControl("lblaccode");
            string fromdt1 = clsGV.Start_Date;
            string todt1 = clsGV.To_date;
            string accode = lblaccode.Text;
            // Session["VOUC_NO"] = no;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + accode + "','" + fromdt1 + "','" + todt1 + "','DrCr')", true);
            lnkOV.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }



    public string HttpContent(string url)
    {

        System.Net.WebRequest objRequest = System.Net.HttpWebRequest.Create(url.Trim());
        StreamReader sr = new StreamReader(objRequest.GetResponse().GetResponseStream());
        s = sr.ReadToEnd();
        sr.Close();

        return s;
    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            #region pdf code success
            /*    Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=pankaj.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            PrintPanel.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 0, 0, 0, 0);
            PrintPanel.Style.Add("font-size", "10px");



            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();*/
            #endregion
            email = txtEmail.Text.ToString();
            string strHtml = string.Empty;
            #region email pdf
            /* using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter txtwrt = new HtmlTextWriter(sw))
                {


                    PrintPanel.RenderControl(txtwrt);
                    //StringBuilder sb = new StringBuilder(sw);
                    StringReader sr = new StringReader(sw.ToString());
                    strHtml = sr.ReadToEnd();
                    Document pdfDoc = new Document(PageSize.A4, 0, 0, 0, 0);
                    HTMLWorker htmlParser = new HTMLWorker(pdfDoc);
                    PrintPanel.Style.Add("font-size", "10px");
                    using (MemoryStream ms = new MemoryStream())
                    {
                        PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();
                        htmlParser.Parse(sr);
                        //var parsedHtmlElement = HTMLWorker.ParseToList(new StringReader(sw.ToString()),null);
                        //foreach (var htmlElement in parsedHtmlElement)
                        //{
                        //    pdfDoc.Add(htmlElement as IElement);
                        //}
                        pdfDoc.Close();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=report.pdf");
                        //Response.Cache.SetCacheability(HttpCacheability.NoCache);

                        byte[] bytes = ms.ToArray();
                        ms.Close();

                        string mailFrom = Session["EmailId"].ToString();
                        string smtpPort = "587";
                        string emailPassword = Session["EmailPassword"].ToString();
                        MailMessage msg = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                        SmtpServer.Host = clsGV.Email_Address;
                        msg.From = new MailAddress(mailFrom);
                        msg.To.Add(email);
                        msg.Body = "Profit And Loss";
                        msg.Attachments.Add(new Attachment(new MemoryStream(bytes), "report.pdf"));
                        msg.IsBodyHtml = true;
                        //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";
                        msg.Subject = "Profit And Loss Report" + " " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
                        msg.IsBodyHtml = true;
                        if (smtpPort != string.Empty)
                        {
                            SmtpServer.Port = Convert.ToInt32(smtpPort);
                        }
                                             SmtpServer.EnableSsl = true;
                        SmtpServer.UseDefaultCredentials = false;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                            System.Security.Cryptography.X509Certificates.X509Chain chain,
                            System.Net.Security.SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };
                        SmtpServer.Send(msg);
                    }
                }
            }*/
            #endregion
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=UserDetails.pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //this.Page.RenderControl(hw);
            //StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A4);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            //pdfDoc.Close();
            //Response.Write(pdfDoc);
            //Response.End();
            //string mailFrom = Session["EmailId"].ToString();
            //string smtpPort = "587";
            //string emailPassword = Session["EmailPassword"].ToString();
            //MailMessage msg = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            //SmtpServer.Host = clsGV.Email_Address;
            //msg.From = new MailAddress(mailFrom);
            //msg.To.Add(email);
            //msg.Body = "Profit And Loss";
            //msg.Attachments.Add(new Attachment(new MemoryStream(bytes), "report.pdf"));
            //msg.IsBodyHtml = true;
            ////msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";
            //msg.Subject = "Profit And Loss Report" + " " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
            //msg.IsBodyHtml = true;
            //if (smtpPort != string.Empty)
            //{
            //    SmtpServer.Port = Convert.ToInt32(smtpPort);
            //}
            //                     SmtpServer.EnableSsl = true;
            //SmtpServer.UseDefaultCredentials = false;
            //SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
            //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
            //    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            //    System.Security.Cryptography.X509Certificates.X509Chain chain,
            //    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            //{
            //    return true;
            //};
            //SmtpServer.Send(msg);

            StringWriter sw = new StringWriter();
            HtmlTextWriter tw = new HtmlTextWriter(sw);
            PrintPanel.RenderControl(tw);
            s = sw.ToString();

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
            catch (Exception)
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

            string mailFrom = Session["EmailId"].ToString();
            string smtpPort = "587";
            string emailPassword = Session["EmailPassword"].ToString();
            MailMessage msg = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            SmtpServer.Host = clsGV.Email_Address;
            msg.From = new MailAddress(mailFrom);
            msg.To.Add(email);
            msg.Body = "Profit And Loss";
            msg.Attachments.Add(new Attachment(Server.MapPath(f)));
            msg.IsBodyHtml = true;
            //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";
            msg.Subject = "Profit And Loss Report" + " " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
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
        catch (Exception e1)
        {
            //Response.Write("mail err:" + e1);
            Response.Write("<script>alert('Error sending Mail');</script>");
            return;
        }
        Response.Write("<script>alert('Mail sent successfully');</script>");


        //s = null;
        //byte[] reqHtml;
        //string url = HttpContext.Current.Request.Url.AbsoluteUri;
        //int fromStr = url.LastIndexOf(@"/");
        //string ext = url.Substring(fromStr + 1);
        //int toStr = ext.IndexOf(@".");
        //string pgeName = ext.Substring(0, toStr);
        //UTF8Encoding utf8 = new UTF8Encoding();
        //reqHtml = client.DownloadData(url);
        //s = utf8.GetString(reqHtml);
        //Stream data = client.OpenRead(HttpContext.Current.Request.Url.AbsoluteUri);
        //StreamReader reader = new StreamReader(data);
        //string s = reader.ReadToEnd();
        //data.Close();
        //reader.Close();
        //string myurl = "http://localhost:25347/AccoWebAnkush/Report/rptProfitLoss.aspx";
        //var url = HttpContext.Current.Request.Url.AbsoluteUri;
        //StringWriter str_wtr = new StringWriter();
        //HtmlTextWriter txtwrt = new HtmlTextWriter(str_wtr);
        //GetHtml(url);

        //Page.RenderControl(txtwrt);
        //string s = str_wtr.ToString();
        //Render(txtwrt);


        //try
        //{
        //    using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
        //    {
        //        using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
        //        {
        //            w.WriteLine(s);
        //        }
        //    }
        //}
        //catch (Exception ee)
        //{
        //    f = f_Main + ".htm";
        //    using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
        //    {
        //        using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
        //        {
        //            w.WriteLine(s);
        //        }
        //    }
        //}

        // Response.Redirect("../Report/rptProfitLoss.aspx", false);
    }

    //protected override void Render(HtmlTextWriter writer)
    //{
    //    StringBuilder sbOut = new StringBuilder();
    //    StringWriter swOut = new StringWriter(sbOut);
    //    HtmlTextWriter htwOut = new HtmlTextWriter(swOut);
    //    base.Render(htwOut);
    //    s = sbOut.ToString();

    //    // Send sOut as an Email
    //    writer.Write(s);
    //}
    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    /* Verifies that the control is rendered */
    //}

    public static string GetHtml(string urlAddr)
    {
        if (urlAddr == null || string.IsNullOrEmpty(urlAddr))
        {
            throw new ArgumentNullException("urlAddr");
        }
        else
        {
            string result;

            //1.Create the request object
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddr);
            request.Method = "GET";
            request.KeepAlive = true;
            request.ContentType = "application/json";
            request.AllowAutoRedirect = true;
            request.MaximumAutomaticRedirections = 2000;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.PreAuthenticate = false;
            request.UnsafeAuthenticatedConnectionSharing = true;
            request.Proxy = new WebProxy(urlAddr, true);
            request.UseDefaultCredentials = true;

            //2.Add the container with the active 
            CookieContainer cc = new CookieContainer();
            var s = request.RequestUri;

            //3.Must assing a cookie container for the request to pull the cookies
            request.CookieContainer = cc;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //Stream sd = response.GetResponseStream();

            //Encoding enc = Encoding.GetEncoding(1252);
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                //Close and clean up the StreamReader
                sr.Close();
            }
            return result;

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        StringBuilder StrHtmlGenerate = new StringBuilder();
        StringBuilder StrExport = new StringBuilder();
        StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
        StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
        StrExport.Append("<DIV  style='font-size:12px;'>");
        StringWriter sw = new StringWriter();
        HtmlTextWriter tw = new HtmlTextWriter(sw);
        PrintPanel.RenderControl(tw);
        string sim = sw.ToString();
        StrExport.Append(sim);
        StrExport.Append("</div></body></html>");
        string strFile = "report.xls";
        string strcontentType = "application/excel";
        Response.ClearContent();
        Response.ClearHeaders();
        Response.BufferOutput = true;
        Response.ContentType = strcontentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
        Response.Write(StrExport.ToString());
        Response.Flush();
        Response.Close();
        Response.End();
    }
}