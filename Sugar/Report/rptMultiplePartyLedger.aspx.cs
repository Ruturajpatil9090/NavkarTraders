using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Data;
using System.Collections;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.UI.HtmlControls;
using iTextSharp.text.pdf.parser;

public partial class Report_rptMultiplePartyLedger : System.Web.UI.Page
{
    #region[variables]
    string tblPrefix = string.Empty;
    string tblmain = string.Empty;
    string qry = string.Empty;
    string Chargetype = string.Empty;
    string FromDt = string.Empty;
    string ToDt = string.Empty;
    string DrCr = string.Empty;
    string tblGLEDGER = string.Empty;
    string tblDetails = string.Empty;
    string AccountMasterTable = string.Empty;
    string cityMasterTable = string.Empty;
    double netdebit = 0.00; double netcredit = 0.00;
    string GroupCode = string.Empty;
    int tempcounter = 0;
    #endregion

    #region[page load]
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        tblmain = "qrymstaccountmaster";
        Chargetype = Request.QueryString["Ac_type"];
        FromDt = Request.QueryString["FromDt"];
        ToDt = Request.QueryString["ToDt"];
        DrCr = Request.QueryString["DrCr"];
        GroupCode = Request.QueryString["GroupCode"];
        tblGLEDGER = tblPrefix + "GLEDGER";
        tblDetails = tblPrefix + "VoucherDetails";
        AccountMasterTable = tblPrefix + "AccountMaster";
        cityMasterTable = tblPrefix + "CityMaster";
        //email = txtEmail.Text;
        string partyname = clsCommon.getString("select Ac_name_e from " + AccountMasterTable + "  where Ac_type=" + Chargetype + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        Page.Title = partyname;
        if (!Page.IsPostBack)
        {
            lblCompanyName.Text = Session["Company_Name"].ToString();
            if (DrCr != "DrCr")
            {
                this.OnlyCrORDr();
            }
            else
            {
                this.BindData();
            }
        }

    }
    #endregion

    private void OnlyCrORDr()
    {
        try
        {
            string mail = "";
            // pnlPopup.Style["display"] = "none";
            if (Chargetype != string.Empty)
            {
                string ccmail = clsCommon.getString("Select Email_Id_cc from " + AccountMasterTable + "  where  Ac_Code=" + Chargetype + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                mail = ccmail + "," + clsCommon.getString("Select Email_Id from " + AccountMasterTable + "  where  Ac_Code=" + Chargetype + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            }

            //string partyname = clsCommon.getString("select Ac_name_e from " + AccountMasterTable + "  where Ac_type=" + Chargetype + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            //lblAc_Name_E.Text = " (" + accode + ")&nbsp;" + partyname;
            //lblfr.Text = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            // lblToDt.Text = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            // fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            //todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from " + tblGLEDGER + " where DOC_DATE < '" + FromDt + "' and Ac_type=" + Chargetype + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE ";
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
            qry = "select TRAN_TYPE,DOC_NO,Convert(varchar(10),DOC_DATE,102) as DOC_DATE ,NARRATION,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,UNIT_Code,DRCR,SORT_TYPE,SORT_NO from " + tblGLEDGER +
                    " where Ac_type=" + Chargetype + " and DOC_DATE between '" + FromDt + "' and '" + ToDt + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by DOC_DATE asc ";
            ds = clsDAL.SimpleQuery(qry);

            DataTable dtT = new DataTable();
            //dtT = null;

            dtT.Columns.Add("Date", typeof(string));
            dtT.Columns.Add("Narration", typeof(string));
            dtT.Columns.Add("Debit", typeof(double));
            dtT.Columns.Add("Credit", typeof(double));
            dtT.Columns.Add("Balance", typeof(double));
            dtT.Columns.Add("DrCr", typeof(string));

            //if (dt.Rows.Count > 0)
            //{
            dt = ds.Tables[0];

            DataRow dr = dtT.NewRow();
            //  old dr[0] = dt.Rows[0]["TRAN_TYPE"].ToString();
            dr[0] = "OP";
            dr[1] = 0.00;
            dr[2] = DateTime.Parse(FromDt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
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

                    //dr[0] = dt.Rows[i]["TRAN_TYPE"].ToString();
                    // dr[1] = dt.Rows[i]["DOC_NO"].ToString();
                    if (dt.Rows[i]["DOC_DATE"].ToString() != string.Empty)
                    {
                        string s = dt.Rows[i]["DOC_DATE"].ToString();
                        dr[0] = DateTime.Parse(s, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    }
                    string SORT_TYPE = dt.Rows[i]["SORT_TYPE"].ToString();
                    string SORT_NO = dt.Rows[i]["SORT_NO"].ToString();
                    dr[1] = Server.HtmlDecode(dt.Rows[i]["NARRATION"].ToString() + "(" + SORT_TYPE + " " + SORT_NO + ")");

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

            //dtT.DataSource = dT;
            //grdDetail.DataBind();
            //grdDetail.FooterRow.Cells[3].Text = "Total";
            //if (DrCr == "Dr")
            //{
            //    grdDetail.FooterRow.Cells[4].Text = netdebit.ToString();
            //}
            //else
            //{
            //    grdDetail.FooterRow.Cells[5].Text = netcredit.ToString();
            //}
            //if (netdebit - netcredit != 0)
            //{
            //    double balance = netdebit - netcredit;
            //    if (balance > 0)
            //    {
            //        dtT.FooterRow.Cells[7].Text = "Dr";
            //    }
            //    if (balance < 0)
            //    {
            //        grdDetail.FooterRow.Cells[7].Text = "Cr";
            //    }
            //    grdDetail.FooterRow.Cells[6].Text = Math.Abs(balance).ToString();
            //}
            //else
            //{
            //    grdDetail.FooterRow.Cells[6].Text = "Nil";
            //}
            //grdDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            //grdDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            //grdDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            //grdDetail.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Center;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    #region[Bind Data]
    private void BindData()
    {
        try
        {
            using (clsDataProvider obj = new clsDataProvider())
            {
                DataSet ds = new DataSet();
                DataTable dt1 = new DataTable();
                //string ac_code = clsCommon.getString("Select Ac_Code from " + tblPrefix + "qryAccountsList where Ac_type='" + Chargetype + "'and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

                string ac_code = "";
                if (Chargetype == "G")
                {
                   string fromDate =DateTime.Parse(FromDt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    string ToDate = DateTime.Parse(ToDt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    lblDate.Text = "From :" + fromDate + " To: " + ToDate;
                    lblType.Text = "Balance Sheet Group Wise <br/>" + clsCommon.getString("select [group_Name_E] from " + tblPrefix
                        + "BSGroupMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [group_Code]=" + GroupCode); ;

                    ac_code = "Select Ac_Code from qrymultiplegledger where Group_Code='" + GroupCode
                        + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

                    //ac_code = "Select a.Ac_Code,g.amount from " + tblPrefix + "qryAccountsList a left outer join " + tblGLEDGER
                    //    + " g  where g.Amount > 0 and a.Group_Code='" + GroupCode
                    //   + "' and a.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                }
                else
                {
                    lblType.Text = "Account Wise";
                    ac_code = "Select Ac_Code from qrymultiplegledger where Company_Code="
                        + Convert.ToInt32(Session["Company_Code"].ToString());
                }


                ds = obj.GetDataSet(ac_code);

                //dt1.Columns.Add("Ac_Code",typeof(Int32));

                List<string> myIds = new List<string>();
                dt1 = ds.Tables[0];
                foreach (DataRow dr in dt1.Rows)
                {
                  
                    myIds.Add(Convert.ToString(dr["Ac_Code"]));
                }
                string result = string.Join(",", myIds);

                string qry = "select DISTINCT(g.AC_CODE),a.Ac_Name_E from " + tblGLEDGER + " g left outer join NT_1_AccountMaster " +
                    " a ON g.AC_CODE=a.Ac_Code and g.COMPANY_CODE=a.Company_Code where g.AC_CODE in(" + result + ") and g.COMPANY_CODE=" +
                    Convert.ToInt32(Session["Company_Code"].ToString()) + " and g.DOC_DATE between '" + FromDt + "' and '" + ToDt + "' order by a.Ac_Name_E";





                //string qry1 = "select dictinct(AC_CODE) from " + tblmain + " where AC_CODE="+ qry+" and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_type='" + Chargetype + "'";
                // qry = "select Ac_type,AC_CODE,Ac_Name_E from " + tblmain + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_type='" + Chargetype + "'";
                //qry = "select Ac_type,AC_CODE,Ac_Name_E from " + tblmain + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_type='" + Chargetype + "'";

                #region[fill ds]
                ds = obj.GetDataSet(qry);
                DataTable dt = new DataTable();
                // dt.Columns.Add("Ac_Type", typeof(string));
                dt.Columns.Add("Doc_No", typeof(string));
                dt.Columns.Add("Ac_Name_E", typeof(string));
                dt1 = null;
                dt1 = new DataTable();
                myIds = null;
                myIds = new List<string>();
                dt1 = ds.Tables[0];
                foreach (DataRow dr in dt1.Rows)
                {
                    myIds.Add(Convert.ToString(dr["Ac_Code"]));
                }
                string result2 = string.Join(",", myIds);


                string qryGetOpAc = "select distinct(g.AC_CODE),a.Ac_Name_E from NT_1_GLEDGER g left outer join NT_1_AccountMaster" +
                    " a ON g.AC_CODE=a.Ac_Code and g.COMPANY_CODE=a.Company_Code  where g.DOC_DATE<'" + FromDt + "' and g.AC_CODE IN(" + result
                    + ") and g.AC_CODE NOT IN(" + result2 + ") and g.COMPANY_CODE=" +
                                    Convert.ToInt32(Session["Company_Code"].ToString()) + "";




                DataSet dsOPAc = new DataSet();
                dsOPAc = obj.GetDataSet(qryGetOpAc);

                ds.Merge(dsOPAc);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            // dr["Ac_Type"] = ds.Tables[0].Rows[i]["Ac_Type"].ToString();
                            dr["Doc_No"] = ds.Tables[0].Rows[i]["Ac_Code"].ToString();
                            //string partyname = ds.Tables[0].Rows[i]["Ac_Code"].ToString();
                            dr["Ac_Name_E"] = ds.Tables[0].Rows[i]["Ac_Name_E"].ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                    dtl.DataSource = dt;
                    dtl.DataBind();
                }
                #endregion
            }
        }
        catch
        {
        }
    }
    #endregion

    #region[ItemDataBound Method]
    protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        GridView grdDetail = (GridView)e.Item.FindControl("grdDetail");
        Label lbldoc_no = (Label)e.Item.FindControl("lbldoc_no");
        //lbl.Text = " (" + accode + ")&nbsp;" + clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + "  where  Ac_Code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        // lblFromDt.Text = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        //lblToDt.Text = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
        FromDt = DateTime.Parse(FromDt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        ToDt = DateTime.Parse(ToDt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from " + tblGLEDGER
            + " where DOC_DATE < '" + FromDt + "' and Ac_Code=" + lbldoc_no.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
            + " group by AC_CODE";

        //ds = obj.GetDataSet(qry);
        ds = clsDAL.SimpleQuery(qry);
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
        qry = "select TRAN_TYPE,DOC_NO,Convert(varchar(10),DOC_DATE,102) as DOC_DATE ,NARRATION,AMOUNT,ADJUSTED_AMOUNT,AC_CODE,UNIT_Code,DRCR,SORT_TYPE,SORT_NO from " + tblGLEDGER +
                " where AC_CODE=" + lbldoc_no.Text + " and DOC_DATE between '" + FromDt + "' and '" + ToDt
                + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " order by DOC_DATE asc ";

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

        //if (dt.Rows.Count > 0)
        //{
        dt = ds.Tables[0];

        DataRow dr = dtT.NewRow();
        //  old dr[0] = dt.Rows[0]["TRAN_TYPE"].ToString();
        dr[0] = "OP";
        dr[1] = 0.00;
        dr[2] = DateTime.Parse(FromDt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
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
                if (dt.Rows[i]["DOC_DATE"].ToString() != string.Empty)
                {
                    string s = dt.Rows[i]["DOC_DATE"].ToString();
                    dr[2] = DateTime.Parse(s, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
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
        grdDetail.FooterRow.Cells[4].Text = netdebit.ToString();
        grdDetail.FooterRow.Cells[5].Text = netcredit.ToString();
        Label lblBillAmount_Total = (Label)e.Item.FindControl("lblBillAmount_Total");
        //lblBillAmount_Total.Text =  Math.Abs(Math.Round(netdebit, 2)).ToString();
        if (netdebit - netcredit != 0)
        {


            double balance = netdebit - netcredit;
            if (balance > 0)
            {
                // lblBillAmount_Total.Text ="Total:"+ Math.Abs(Math.Round(balance, 2)).ToString();
                grdDetail.FooterRow.Cells[7].Text = "Dr";
            }
            if (balance < 0)
            {
                //lblBillAmount_Total.Text = "Total:" + Math.Abs(Math.Round(balance, 2)).ToString();
                grdDetail.FooterRow.Cells[7].Text = "Cr";
            }
            // lblBillAmount_Total.Text = "Total:" + Math.Abs(Math.Round(netdebit, 2)).ToString();
            grdDetail.FooterRow.Cells[6].Text = Math.Abs(Math.Round(balance, 2)).ToString();
        }
        else
        {
            //lblBillAmount_Total.Text = "Total:" + "NIll";
            grdDetail.FooterRow.Cells[6].Text = "Nil";
        }
        grdDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
        grdDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
        grdDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
        grdDetail.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Center;

        netdebit = 0;
        netcredit = 0;

        //Response.ClearContent();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Customers.xls"));
        //Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        ////dtl.DataBind();
        //grdDetail.AllowPaging = false;

        ////this.bindData();
        ////Change the Header Row back to white color
        //grdDetail.HeaderRow.Style.Add("background-color", "#FFFFFF");
        ////Applying stlye to gridview header cells
        //for (int i = 0; i < grdDetail.HeaderRow.Cells.Count; i++)
        //{
        //    grdDetail.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
        //}
        //grdDetail.RenderControl(htw);
        //Response.Write(sw.ToString());
        //Response.End();


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
    protected void grdDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridView grdDetail = (GridView)e.ToString();
        //grdDetail.PageIndex = e.NewPageIndex;
        //if (DrCr != "DrCr")
        //{
        //    this.OnlyCrORDr();
        //}
        //else
        //{
        //    this.BindData();
        //}
    }
    #endregion


    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {

        //Get the data from database into datatable
        //GridView GridView1 = dtl.Items[0].FindControl("grdDetail") as GridView;
        //GridView GridView1 = new GridView();

        //BindData();

        Response.Clear();

        Response.Buffer = true;

        Response.AddHeader("content-disposition",

         "attachment;filename=DataTable.xls");

        Response.Charset = "";

        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);

        //for (int i = 0; i < GridView1.Rows.Count; i++)
        //{

        //    //Apply text style to each Row

        //    //GridView1.Rows[i].Attributes.Add("");

        //}

        pnlMain.RenderControl(hw);

        //style to format numbers to string

        string style = @"<style> .textmode { mso-number-format:\@; } </style>";

        Response.Write(style);

        Response.Output.Write(sw.ToString());

        Response.Flush();

        Response.End();








        //GridView grd = dtl.Items[0].FindControl("grdDetail") as GridView;
        //string attachment = "attachment; filename=Export.xls";
        //Response.ClearContent();
        //Response.AddHeader("content-disposition", attachment);
        //Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        //// Create a form to contain the grid
        //HtmlForm frm = new HtmlForm();
        //pnlMain.Parent.Controls.Add(frm);
        //frm.Attributes["runat"] = "server";
        //frm.Controls.Add(grd);
        //frm.RenderControl(htw);

        ////GridView1.RenderControl(htw);
        //Response.Write(sw.ToString());
        //Response.End();

    }



    protected void btnPDFMail_Click(object sender, EventArgs e)
    {
        try
        {
            GridView grd = dtl.Items[0].FindControl("grdDetail") as GridView;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=MultipleLedger.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter swr = new StringWriter();
            HtmlTextWriter htmlwr = new HtmlTextWriter(swr);
            grd.AllowPaging = false;
            // BindData();
            pnlMain.RenderControl(htmlwr);

            //  string s1 = swr.ToString().Replace("font-size: medium", "font-size: medium");
            //  StringReader sr = new StringReader(s1.ToString());
            StringReader srr = new StringReader(swr.ToString().Replace("../Images", Server.MapPath("~/Images")));


            //StringReader srr = new StringReader(swr.ToString());
            Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //Document pdfdoc = new Document(PageSize.A4);
            HTMLWorker htmlparser = new HTMLWorker(pdfdoc);
            PdfWriter.GetInstance(pdfdoc, Response.OutputStream);

            pdfdoc.Open();
            htmlparser.Parse(srr);
            pdfdoc.Close();


            Response.Write(pdfdoc);
            Response.End();


            // GridView grd = dtl.Items[0].FindControl("grdDetail") as GridView;
            // //sGridView grd= e
            //// DataList dt = (DataList)FindControl("dtl");
            // Response.ContentType = "application/pdf";
            // Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            // Response.Cache.SetCacheability(HttpCacheability.NoCache);

            // StringWriter sw = new StringWriter();
            // HtmlTextWriter hw = new HtmlTextWriter(sw);
            // grd.RenderControl(hw);
            // string s1 = sw.ToString().Replace("font-size: medium", "font-size: xx-small");
            //// StringReader sr = new StringReader(s1.ToString());
            // StringReader sr = new StringReader(s1.ToString().Replace("../Images", Server.MapPath("~/Images")));

            // Document pdfDoc = new Document(iTextSharp.text.PageSize.A4);
            // var writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            // pdfDoc.Open();
            // XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            // pdfDoc.Close();
            // Response.Write(pdfDoc);
            // Response.End();
        }
        catch
        {
        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {

    }

}