using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;
using ClosedXML.Excel;
using System.Text;

public partial class Report_pgeTrialBalanceScreen : System.Web.UI.Page
{
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    string tblPrefix = string.Empty;
    DataTable dtData;
    static WebControl objAsp = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["user"].ToString();
        tblPrefix = Session["tblPrefix"].ToString();
        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                //grdDetail.UseAccessibleHeader = true;
                //grdDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                txtDate.Text = clsGV.End_Date;
                txtFromDt.Text = clsGV.Start_Date;
                txtToDt.Text = clsGV.To_date;
                ViewState["sortOrder"] = "";
                ViewState["qry"] = null;
                ViewState["DrCr"] = null;
                ViewState["filterDt"] = null;
                //Data("", "", "", "");
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
        //this.RegisterPostBackControl();
    }
    private void RegisterPostBackControl()
    {
        foreach (GridViewRow row in grdDetail.Rows)
        {
            LinkButton lnkAcName = row.FindControl("lnkAcName") as LinkButton;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkAcName);
        }
    }
    public void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    public static DataTable Data(string qry, string DrCr, string sortExp, string sortDir)
    {
        try
        {
            DataTable dtT = new DataTable();
            dtT.Columns.Add("accode", typeof(Int32));
            dtT.Columns.Add("acname", typeof(string));
            dtT.Columns.Add("city", typeof(string));
            dtT.Columns.Add("debitAmt", typeof(double));
            dtT.Columns.Add("creditAmt", typeof(double));
            dtT.Columns.Add("mobile", typeof(string));
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataView dv;
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dv = new DataView();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = dtT.NewRow();
                            dr["accode"] = dt.Rows[i]["AC_CODE"].ToString();
                            dr["acname"] = dt.Rows[i]["Ac_Name_E"].ToString();
                            dr["city"] = dt.Rows[i]["CityName"].ToString();
                            dr["mobile"] = dt.Rows[i]["Mobile_No"].ToString();
                            double bal = Convert.ToDouble(ds.Tables[0].Rows[i]["Balance"].ToString());
                            if (DrCr == "Dr")
                            {
                                if (bal > 0)
                                {
                                    dr["debitAmt"] = bal.ToString();
                                    dr["creditAmt"] = 0.00;
                                    dtT.Rows.Add(dr);
                                }
                            }
                            else if (DrCr == "Cr")
                            {
                                if (bal < 0)
                                {
                                    dr["debitAmt"] = 0.00;
                                    dr["creditAmt"] = Math.Abs(bal);
                                    dtT.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                if (bal > 0)
                                {
                                    // groupdebitamt += bal;
                                    dr["debitAmt"] = bal.ToString();
                                    dr["creditAmt"] = 0.00;
                                }
                                else
                                {
                                    //  groupcreditamt += -bal;
                                    dr["debitAmt"] = 0.00;
                                    dr["creditAmt"] = Math.Abs(bal);
                                }
                                dtT.Rows.Add(dr);
                            }
                        }
                        dv = sortingDT(sortExp, sortDir, dtT, dv);
                        dtT = (DataTable)dv.ToTable();
                    }
                }
            }
            return dtT;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static DataView sortingDT(string sortExp, string sortDir, DataTable dtT, DataView dv)
    {
        dv = dtT.DefaultView;
        if (sortExp != string.Empty)
        {
            dv.Sort = string.Format("{0} {1}", sortExp, sortDir);
        }
        return dv;
    }


    protected void Command_Click(object sender, CommandEventArgs e)
    {
        try
        {
            grddepreciation.DataSource = null;
            grddepreciation.DataBind();
            string qry = "";
            string Ac_type = drpType.SelectedValue.ToString();
            string DOC_DTAE = DateTime.Parse(txtDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            if (Ac_type == "A")
            {
                qry = "select AC_CODE,Ac_Name_E,CityName, SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance ,Mobile_No" +
                            " from qrygledger where  DOC_DATE<='" + DOC_DTAE + "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())
                            + "  group by AC_CODE,Ac_Name_E,CityName,Mobile_No having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) !=0 order by Ac_Name_E";

              
            }
            else
            {
                qry = "select AC_CODE,Ac_Name_E,CityName, SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance ,Mobile_No" +
                " from qrygledger where Ac_type='" + Ac_type + "' and DOC_DATE<='" + DOC_DTAE + "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString()) 
                + "  group by AC_CODE,Ac_Name_E,CityName,Mobile_No having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) !=0 order by Ac_Name_E";

              
            }
            dtData = new DataTable();

            switch (e.CommandName)
            {
                case "DrCr":
                    ViewState["qry"] = qry;
                    ViewState["DrCr"] = "DrCr";
                    ViewState["filterDt"] = null;
                    dtData = Data(qry, "DrCr", "", "");
                    break;

                case "Dr":
                    ViewState["qry"] = qry;
                    ViewState["DrCr"] = "Dr";
                    ViewState["filterDt"] = null;
                    dtData = Data(qry, "Dr", "", "");
                    break;

                case "Cr":
                    ViewState["qry"] = qry;
                    ViewState["DrCr"] = "Cr";
                    ViewState["filterDt"] = null;
                    dtData = Data(qry, "Cr", "", "");
                    break;
            }
            ViewState["gridData"] = dtData;
            grdDetail.DataSource = dtData;
            grdDetail.DataBind();
            this.RegisterPostBackControl();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void Command1_Click(object sender, CommandEventArgs e)
    {
        try
        {
            grddepreciation.DataSource = null;
            grddepreciation.DataBind();
            string qry = "";
            string Ac_type = drpType.SelectedValue.ToString();
            string DOC_DTAE = DateTime.Parse(txtDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            string FromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            string ToDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            if (Ac_type == "A")
            {
                qry = "select AC_CODE,Ac_Name_E,CityName, SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance ,Mobile_No" +
                            " from qrygledger where  DOC_DATE between '" + FromDT + "' and '" + ToDT + "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())
                            + "  group by AC_CODE,Ac_Name_E,CityName,Mobile_No having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) !=0 order by Ac_Name_E";


            }
            else
            {
                qry = "select AC_CODE,Ac_Name_E,CityName, SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance ,Mobile_No" +
                " from qrygledger where Ac_type='" + Ac_type + "' and DOC_DATE between '" + FromDT + "' and '" + ToDT + "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + "  group by AC_CODE,Ac_Name_E,CityName,Mobile_No having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) !=0 order by Ac_Name_E";


            }
            dtData = new DataTable();

            switch (e.CommandName)
            {
                case "DrCr":
                    ViewState["qry"] = qry;
                    ViewState["DrCr"] = "DrCr";
                    ViewState["filterDt"] = null;
                    dtData = Data(qry, "DrCr", "", "");
                    break;

                case "Dr":
                    ViewState["qry"] = qry;
                    ViewState["DrCr"] = "Dr";
                    ViewState["filterDt"] = null;
                    dtData = Data(qry, "Dr", "", "");
                    break;

                case "Cr":
                    ViewState["qry"] = qry;
                    ViewState["DrCr"] = "Cr";
                    ViewState["filterDt"] = null;
                    dtData = Data(qry, "Cr", "", "");
                    break;
            }
            ViewState["gridData"] = dtData;
            grdDetail.DataSource = dtData;
            grdDetail.DataBind();
            this.RegisterPostBackControl();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtT = new DataTable();
            dtT.Columns.Add("mobile", typeof(string));
            dtT.Columns.Add("msgBody", typeof(string));

            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdDetail.Rows[i].Cells[7].FindControl("chkIsPrint");
                if (chk.Checked)
                {
                    TextBox txtMobile = (TextBox)grdDetail.Rows[i].Cells[5].FindControl("txtMobile");
                    Label lblDebit = (Label)grdDetail.Rows[i].Cells[3].FindControl("lblDebit");
                    string debitAmount = lblDebit.Text;
                    string mobile = txtMobile.Text;
                    if (mobile != string.Empty)
                    {
                        if (debitAmount != "0")
                        {
                            string msgAPI = clsGV.msgAPI;
                            msgAPI = msgAPI + "mobile=" + mobile + "&message=" + Session["Company_Name"].ToString() + ":- Your A/c shows debit balance Rs." + debitAmount + ". Please Send Urgently";
                            clsCommon.apicall(msgAPI);
                        }
                    }
                }
            }
        }
        catch
        {

        }
    }
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtMobile = (TextBox)e.Row.Cells[5].FindControl("txtMobile");
            txtMobile.Text = e.Row.Cells[6].Text;

            if (txtMobile.Text.Contains("&nb"))
            {
                txtMobile.Text = "";
            }
            e.Row.Cells[6].Visible = false;
        }
        
    }
    protected void grdDetail_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            //int rowIndex = row.RowIndex;
            //string accode = grdDetail.Rows[rowIndex].Cells[0].Text.ToString();
            //string fromdt = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            //string todt = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + accode + "','" + fromdt + "','" + todt + "','DrCr')", true);
            //string rowID = "row" + rowIndex;
            //int lastCount = grdDetail.Rows.Count - rowIndex;
            //int remain = lastCount - 9;
            //if (remain <= 0)
            //{
            //    grdDetail.Rows[grdDetail.Rows.Count - 1].Cells[1].Focus();
            //}
            //else
            //{
            //    grdDetail.Rows[rowIndex + 9].Cells[1].Focus();
            //}
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ks", "javascript:ChangeRowColor(" + "'" + rowID + "'" + ")", true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void grdDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //foreach (GridViewRow row in grdDetail.Rows)
            //{
            //    if (row.RowIndex == grdDetail.SelectedIndex)
            //    {
            //        row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
            //    }
            //    else
            //    {
            //        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            //    }
            //}
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void grdDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        string rowID = String.Empty;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            rowID = "row" + e.Row.RowIndex;
            e.Row.Attributes.Add("id", "row" + e.Row.RowIndex);
            e.Row.Attributes.Add("onclick", "ChangeRowColor(" + "'" + rowID + "'" + ")");
        }
    }
    protected void drpFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bool isValidated = true;
            if (txtFromRs.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtFromRs);
                return;
            }
            if (txtToRs.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtToRs);
                return;

            }
            string filterExpr = drpFilter.SelectedValue.ToString();
            dtData = new DataTable();
            dtData = (DataTable)ViewState["gridData"];
            double FromRs = Convert.ToDouble(txtFromRs.Text);
            double ToRs = Convert.ToDouble(txtToRs.Text);
            DataRow[] result;
            if (filterExpr == "C")
            {
                result = dtData.Select("creditAmt >= " + FromRs + " and creditAmt <= " + ToRs + "");
            }
            else
            {
                result = dtData.Select("debitAmt >= " + FromRs + " and debitAmt <= " + ToRs + "");
            }

            DataTable dtClone = dtData.Clone();
            foreach (DataRow row in result)
            {
                dtClone.ImportRow(row);
            }
            ViewState["filterDt"] = dtClone;
            grdDetail.DataSource = dtClone;
            grdDetail.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }
            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }
    protected void lnkAcName_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkAcName = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkAcName.NamingContainer;
            int rowIndex = row.RowIndex;
            Label lblAc_Code = grdDetail.Rows[rowIndex].Cells[0].FindControl("lblAc_Code") as Label;

            string accode = lblAc_Code.Text.ToString();
            string fromdt = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            string todt = DateTime.Parse(txtDate.Text.ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + accode + "','" + fromdt + "','" + todt + "','DrCr')", true);
            string rowID = "row" + rowIndex;
            int lastCount = grdDetail.Rows.Count - rowIndex;
            int remain = lastCount - 9;
            if (remain <= 0)
            {
                grdDetail.Rows[grdDetail.Rows.Count - 1].Cells[1].Focus();
            }
            else
            {
                grdDetail.Rows[rowIndex + 9].Cells[1].Focus();
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ks", "javascript:ChangeRowColor(" + "'" + rowID + "'" + ")", true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void grdDetail_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable sortedDt;
            string qry2 = string.Empty;
            string drcr2 = string.Empty;
            if (ViewState["qry"] != null)
            {
                qry2 = ViewState["qry"].ToString();
            }
            if (ViewState["DrCr"] != null)
            {
                drcr2 = ViewState["DrCr"].ToString();
            }
            if (ViewState["filterDt"] != null)
            {
                sortedDt = new DataTable();
                sortedDt = (DataTable)ViewState["filterDt"];
                DataView dv = new DataView();
                dv = sortingDT(e.SortExpression, sortOrder, sortedDt, dv);
                sortedDt = (DataTable)dv.ToTable();
            }
            else
            {
                sortedDt = new DataTable();
                sortedDt = Data(qry2, drcr2, e.SortExpression, sortOrder);
            }
            ViewState["gridData"] = sortedDt;
            grdDetail.DataSource = sortedDt;
            grdDetail.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
           DataTable dtGrid = new DataTable();
            dtGrid = (DataTable)ViewState["gridData"];
            string attachment = "attachment; filename=trialbalacescreen.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dtGrid.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dtGrid.Rows)
            {
                tab = "";
                for (i = 0; i < dtGrid.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.End();
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dtGrid);

        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.Charset = "";
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment;filename=GridView.xlsx");
        //        using (MemoryStream MyMemoryStream = new MemoryStream())
        //        {
        //            wb.SaveAs(MyMemoryStream);
        //            MyMemoryStream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        }
        catch (Exception ex)
        {
          
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnPrintGrid_Click(object sender, EventArgs e)
    {
        //grdDetail.PagerSettings.Visible = false;
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
        ////sb.Append("printWin.document.close();");
        //sb.Append("printWin.focus();");
        //sb.Append("printWin.print();");
        ////sb.Append("printWin.close();};");
        //sb.Append("</script>");
        //ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
       // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "<script type='text/javascript'>SB();</script>", false);
        //grdDetail.PagerSettings.Visible = true;
        //grdDetail.DataBind();
    }
    protected void btntransfertoJV_Command(object sender, CommandEventArgs e)
    {
        string company_code = Session["Company_Code"].ToString();
        string year_code = Session["year"].ToString();

        //int ddid = 1;
        string postageac = clsCommon.getString("select POSTAGE_AC from nt_1_companyparameters where Company_Code=" + company_code + " and Year_Code=" + year_code);
        string postageacid = clsCommon.getString("select accoid from nt_1_accountmaster where Company_Code=" + company_code + " and Ac_Code=" + postageac);
        //string doc_date = DateTime.Now.ToString("yyyy-MM-dd");

        DateTime d2 = Convert.ToDateTime(txtToDt.Text);
        string doc_date = d2.ToString("yyyy-MM-dd"); 

        string XML = "";
        XML = XML + "<ROOT><JVHead tran_type='JV' doc_date='" + doc_date + "' cashbank='0' " +
   "total='0' company_code='" + company_code + "' year_code='" + year_code + "' cb='0' tranid=''>";
        int ddid = 1;
        int Order_Code = 1;
        for (int i = 0; i < grdDetail.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)grdDetail.Rows[i].Cells[7].FindControl("chkIsPrint");
            if (chk.Checked)
            {


                Label lblAc_Code = (Label)grdDetail.Rows[i].Cells[0].FindControl("lblAc_Code");
                string accode = lblAc_Code.Text;
                string accodeid = clsCommon.getString("select accoid from nt_1_accountmaster where Company_Code=" + company_code + " and Ac_Code=" + accode);
                string AC_Name = clsCommon.getString("select Ac_Name_E from nt_1_accountmaster where Company_Code=" + company_code + " and Ac_Code=" + accode); 
                Label lblDebit = (Label)grdDetail.Rows[i].Cells[3].FindControl("lblDebit");
                string debitamt = lblDebit.Text;
                Label lblCredit = (Label)grdDetail.Rows[i].Cells[4].FindControl("lblCredit");
                string creditamt = lblCredit.Text;
                string Grid_amount = "";
                string DRCRGrid = "";


                if (debitamt != "0")
                {
                    XML = XML + "<JVDetailInsert Tran_Type='JV' doc_no='0' doc_date='" + doc_date + "' detail_id='" + ddid + "' debit_ac='" + accode + "' " +
                             "credit_ac='0' Unit_Code='0' amount='" + debitamt + "' " +
                    "narration='Transfer To Round OF' narration2='' Company_Code='" + company_code + "' Year_Code='" + year_code + "' Branch_Code='0' " +
                    "Created_By='" + Session["USER"].ToString() + "' Modified_By='" + Session["USER"].ToString() + "' Voucher_No='0' Voucher_Type='' Adjusted_Amount='0.00' " +
                    "Tender_No='0' TenderDetail_ID='0' drpFilterValue='' CreditAcAdjustedAmount='0.00' Branch_name='' " +
                    "YearCodeDetail='0' tranid='' ca='0' uc='0' tenderdetailid='0' sbid='0' da='" + accodeid + "' trandetailid='0' drcr='D'/>";
                    ddid = ddid + 1;

                    XML = XML + "<JVDetailInsert Tran_Type='JV' doc_no='0' doc_date='" + doc_date + "' detail_id='" + ddid + "' debit_ac='" + postageac + "' " +
                            "credit_ac='0' Unit_Code='0' amount='" + debitamt + "' " +
                   "narration='Transfer To Party " + AC_Name + "' narration2='' Company_Code='" + company_code + "' Year_Code='" + year_code + "' Branch_Code='0' " +
                   "Created_By='" + Session["USER"].ToString() + "' Modified_By='" + Session["USER"].ToString() + "' Voucher_No='0' Voucher_Type='' Adjusted_Amount='0.00' " +
                   "Tender_No='0' TenderDetail_ID='0' drpFilterValue='' CreditAcAdjustedAmount='0.00' Branch_name='' " +
                   "YearCodeDetail='0' tranid='' ca='0' uc='0' tenderdetailid='0' sbid='0' da='" + postageacid + "' trandetailid='0' drcr='C'/>";
                    ddid = ddid + 1;


                    XML = XML + "<Ledger TRAN_TYPE='JV' CASHCREDIT='' DOC_NO='0' DOC_DATE='" + doc_date + "' AC_CODE='" + accode + "' " +
                                                        "UNIT_code='0' NARRATION='Transfer To Round OF' AMOUNT='" + debitamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + company_code + "' " +
                                                        "YEAR_CODE='" + year_code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='0' " +
                                                        "SORT_TYPE='JV' SORT_NO='0' ac='" + accodeid + "' vc='0' progid='3' tranid='0'/>";

                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='JV' CASHCREDIT='' DOC_NO='0' DOC_DATE='" + doc_date + "' AC_CODE='" + postageac + "' " +
                                                       "UNIT_code='0' NARRATION='Transfer To Party " + AC_Name + "' AMOUNT='" + debitamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + company_code + "' " +
                                                       "YEAR_CODE='" + year_code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='0' " +
                                                       "SORT_TYPE='JV' SORT_NO='0' ac='" + postageacid + "' vc='0' progid='3' tranid='0'/>";

                    Order_Code = Order_Code + 1;

                }
                else
                {

                    XML = XML + "<JVDetailInsert Tran_Type='JV' doc_no='0' doc_date='" + doc_date + "' detail_id='" + ddid + "' debit_ac='" + accode + "' " +
                           "credit_ac='0' Unit_Code='0' amount='" + creditamt + "' " +
                  "narration='Transfer To Round OF' narration2='' Company_Code='" + company_code + "' Year_Code='" + year_code + "' Branch_Code='0' " +
                  "Created_By='" + Session["USER"].ToString() + "' Modified_By='" + Session["USER"].ToString() + "' Voucher_No='0' Voucher_Type='' Adjusted_Amount='0.00' " +
                  "Tender_No='0' TenderDetail_ID='0' drpFilterValue='' CreditAcAdjustedAmount='0.00' Branch_name='' " +
                  "YearCodeDetail='0' tranid='' ca='0' uc='0' tenderdetailid='0' sbid='0' da='" + accodeid + "' trandetailid='0' drcr='C'/>";
                    ddid = ddid + 1;


                    XML = XML + "<JVDetailInsert Tran_Type='JV' doc_no='0' doc_date='" + doc_date + "' detail_id='" + ddid + "' debit_ac='" + postageac + "' " +
                            "credit_ac='' Unit_Code='0' amount='" + creditamt + "' " +
                   "narration='Transfer To Party " + AC_Name + "' narration2='' Company_Code='" + company_code + "' Year_Code='" + year_code + "' Branch_Code='0' " +
                   "Created_By='" + Session["USER"].ToString() + "' Modified_By='" + Session["USER"].ToString() + "' Voucher_No='0' Voucher_Type='' Adjusted_Amount='0.00' " +
                   "Tender_No='0' TenderDetail_ID='0' drpFilterValue='' CreditAcAdjustedAmount='0.00' Branch_name='' " +
                   "YearCodeDetail='0' tranid='' ca='0' uc='0' tenderdetailid='0' sbid='0' da='" + postageacid + "' trandetailid='0' drcr='D'/>";
                    ddid = ddid + 1;


                    XML = XML + "<Ledger TRAN_TYPE='JV' CASHCREDIT='' DOC_NO='0' DOC_DATE='" + doc_date + "' AC_CODE='" + accode + "' " +
                                                      "UNIT_code='0' NARRATION='Transfer To Round OF' AMOUNT='" + creditamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + company_code + "' " +
                                                      "YEAR_CODE='" + year_code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='0' " +
                                                      "SORT_TYPE='JV' SORT_NO='0' ac='" + accodeid + "' vc='0' progid='3' tranid='0'/>";

                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='JV' CASHCREDIT='' DOC_NO='0' DOC_DATE='" + doc_date + "' AC_CODE='" + postageac + "' " +
                                                       "UNIT_code='0' NARRATION='Transfer To Party  " + AC_Name + " ' AMOUNT='" + creditamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + company_code + "' " +
                                                       "YEAR_CODE='" + year_code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='0' " +
                                                       "SORT_TYPE='JV' SORT_NO='0' ac='" + postageacid + "' vc='0' progid='3' tranid='0'/>";

                    Order_Code = Order_Code + 1;
                }

            }


        }


        XML = XML + "</JVHead></ROOT>";
        string spname = "JournalVoucher";
        string ss = xmlExecuteDMLQry.ExecuteXMLQryJV(XML, "Save", spname);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Update !')", true);
        //grdDetail.DataSource = null;
        //grdDetail.DataBind();


        //Response.Redirect("~/Report/pgeTrialBalanceScreen.aspx", false);
    }


    protected void btndepriciation_Click(object sender, EventArgs e)
    {
        grdDetail.DataSource = null;
        grdDetail.DataBind();
        double depreciationamt = 0.00;
        //double depreciationamtbefore = 0.00;
        //double depreciationamtafter = 0.00;
        string sixmonth = "";
        string startdate = Session["Start_Date"].ToString();
        string asondate = DateTime.Parse(txtDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        DateTime ddt = Convert.ToDateTime(startdate);
        DateTime dtasondate = Convert.ToDateTime(asondate);
        sixmonth = "30-09-" + ddt.Year;
        string sixmonthcon = DateTime.Parse(sixmonth, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        string lastdate = DateTime.Parse(Session["End_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        string startdate1 = DateTime.Parse(Session["Start_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

        string qry = "SELECT * FROM  nt_1_accountmaster WHERE Ac_type='F' AND company_code=" + Session["Company_Code"].ToString();
        DataSet ds = clsDAL.SimpleQuery(qry);
        DataTable dt = ds.Tables[0];
        DataTable dtreturn = new DataTable();
        dtreturn.Columns.Add("accode", typeof(string));
        dtreturn.Columns.Add("acname", typeof(string));
        dtreturn.Columns.Add("OpeningBalance", typeof(double));
        dtreturn.Columns.Add("Before", typeof(double));
        dtreturn.Columns.Add("After", typeof(double));
        dtreturn.Columns.Add("Deletion", typeof(double));
        dtreturn.Columns.Add("Balance", typeof(double));
        dtreturn.Columns.Add("Depamount", typeof(double));
        dtreturn.Columns.Add("Finalamount", typeof(double));
        dtreturn.Columns.Add("InterestRate", typeof(double));

        if (dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double depreciationamtbefore = 0.00;
                double depreciationamtafter = 0.00;
                DataRow dr = dtreturn.NewRow();
                string accode = dt.Rows[i]["AC_CODE"].ToString();
                string acname = dt.Rows[i]["Ac_Name_E"].ToString();
                double intrate = Convert.ToDouble(dt.Rows[i]["Ac_rate"]);
                qry = "select SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance" +
                                " from qrygledger where AC_CODE=" + accode + " and DOC_DATE<'" + startdate1 + "' and COMPANY_CODE=" + Convert.ToInt32(Session["Company_Code"].ToString())
                                + "  group by AC_CODE,Ac_Name_E,CityName,Mobile_No having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) !=0 order by Ac_Name_E";
                double opbalance = Convert.ToDouble(clsCommon.getString(qry));
                if (opbalance != 0)
                {
                    depreciationamt = 0;
                }
                string qryd = "select sum(amount) as dr from qrygledger where drcr='D' and company_code=" + Session["Company_Code"].ToString() +
                    " and AC_CODE=" + accode + " and DOC_DATE between '" + startdate1 + "' and '" + sixmonthcon + "'";
                double debitamount = Convert.ToDouble(clsCommon.getString(qryd));
                if (debitamount != 0)
                {
                    depreciationamtbefore = debitamount;
                    //depreciationamtbefore = depreciationamt + Math.Round(((debitamount * intrate) / 100), 0);
                    // depreciationamt = depreciationamtbefore;
                }
                string qryd1 = "select sum(amount) as dr from qrygledger where drcr='D' and company_code=" + Session["Company_Code"].ToString() +
               " and AC_CODE=" + accode + " and DOC_DATE>'" + sixmonthcon + "' and DOC_DATE<'" + lastdate + "'";
                double debitamount1 = Convert.ToDouble(clsCommon.getString(qryd1));
                if (debitamount1 != 0)
                {
                    depreciationamtafter = debitamount1;
                    //depreciationamtafter = depreciationamt + Math.Round(((debitamount1 * (intrate / 2)) / 100), 0);
                    //depreciationamt = depreciationamtafter;
                }
                double deletion = Convert.ToDouble(clsCommon.getString("select sum(amount) as cr from qrygledger where drcr='C' and company_code=" + Session["Company_Code"].ToString() +
               " and AC_CODE=" + accode + " and DOC_DATE between '" + startdate1 + "' and '" + lastdate + "'"));
                TimeSpan days = dtasondate - ddt;

                double days1 = days.TotalDays;
                depreciationamt = Math.Round(((opbalance + depreciationamtbefore) * intrate) / 100, 0);
                depreciationamt = depreciationamt + Math.Round(((depreciationamtafter * (intrate / 2)) / 100), 0);


                double balance = opbalance + depreciationamtbefore + depreciationamtafter - deletion;
                double finalamount = balance - depreciationamt;
                dr[0] = accode;
                dr[1] = acname;
                dr[2] = opbalance;
                dr[3] = depreciationamtbefore;
                dr[4] = depreciationamtafter;
                dr[5] = deletion;
                dr[6] = balance;
                dr[7] = depreciationamt;
                dr[8] = finalamount;
                dr[9] = intrate;
                dtreturn.Rows.Add(dr);

            }

        }
        double openningbalance = Convert.ToDouble(dtreturn.Compute("SUM(OpeningBalance)", string.Empty));
        double Beforetotal = Convert.ToDouble(dtreturn.Compute("SUM(Before)", string.Empty));
        double aftertotal = Convert.ToDouble(dtreturn.Compute("SUM(After)", string.Empty));
        double deletionamt = Convert.ToDouble(dtreturn.Compute("SUM(Deletion)", string.Empty));
        double balancetotal = Convert.ToDouble(dtreturn.Compute("SUM(Balance)", string.Empty));
        double depamttotamt = Convert.ToDouble(dtreturn.Compute("SUM(Depamount)", string.Empty));
        double finalamtttotal = Convert.ToDouble(dtreturn.Compute("SUM(Finalamount)", string.Empty));


        grddepreciation.DataSource = dtreturn;
        grddepreciation.DataBind();

        grddepreciation.FooterRow.Cells[2].Text = openningbalance.ToString();
        grddepreciation.FooterRow.Cells[3].Text = Beforetotal.ToString();
        grddepreciation.FooterRow.Cells[4].Text = aftertotal.ToString();
        grddepreciation.FooterRow.Cells[5].Text = deletionamt.ToString();
        grddepreciation.FooterRow.Cells[6].Text = balancetotal.ToString();
        grddepreciation.FooterRow.Cells[7].Text = depamttotamt.ToString();
        grddepreciation.FooterRow.Cells[8].Text = finalamtttotal.ToString();

        grddepreciation.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
        grddepreciation.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
        grddepreciation.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;

        grddepreciation.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
        grddepreciation.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
        grddepreciation.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
        grddepreciation.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

        btndeppost.Visible = true;
    }
    protected void btndeppost_Click(object sender, EventArgs e)
    {
        string asondate = DateTime.Parse(txtDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        string qry = clsCommon.getString("delete from nt_1_gledger where DOC_NO=9999 and TRAN_TYPE='DP' and COMPANY_CODE="
            + Session["Company_code"] + " and YEAR_CODE=" + Session["year"]);
        //string DepreciationAC = Session["DepreciationAC"];
        //string DepreciationAC
        if (grddepreciation.Rows.Count > 0)
        {
            string insertqry = "insert into nt_1_gledger (TRAN_TYPE,DOC_NO,DOC_DATE,AC_CODE,NARRATION,AMOUNT,COMPANY_CODE,YEAR_CODE,ORDER_CODE,DRCR,SORT_TYPE,SORT_NO,ac) values ";
            string insertvalues = "";
            for (int i = 0; i < grddepreciation.Rows.Count; i++)
            {
                string accode = grddepreciation.Rows[i].Cells[0].Text;
                string accoid = clsCommon.getString("SELECT accoid FROM  nt_1_accountmaster WHERE AC_CODE=" + accode + " AND company_code=" + Session["Company_Code"].ToString());

                string acname = grddepreciation.Rows[i].Cells[1].Text;
                double depamt = Convert.ToDouble(grddepreciation.Rows[i].Cells[7].Text);
                double deprate = Convert.ToDouble(grddepreciation.Rows[i].Cells[9].Text);
                string after = grddepreciation.Rows[i].Cells[4].Text;
                double op = Convert.ToDouble(grddepreciation.Rows[i].Cells[2].Text);
                double befoer = Convert.ToDouble(grddepreciation.Rows[i].Cells[3].Text);
                string narrattion = acname + ",On Amount:" + (op + befoer) + ",(DepRate:" + deprate + "),After Amount:" + after + "(" + deprate / 2 + ")";

                if (depamt > 0)
                {
                    insertvalues = insertvalues + "('DP'," + "9999,'" + asondate + "'," + accode + ",'" + narrattion + "'," + depamt + "," + Session["Company_Code"] +
                        "," + Session["year"] + ",1,'C','DP',9999," + accoid + "),";
                    insertvalues = insertvalues + "('DP'," + "9999,'" + asondate + "'," + Session["DepreciationAC"].ToString() + ",'" + narrattion + "'," + depamt + "," + Session["Company_Code"] +
                      "," + Session["year"] + ",2,'D','DP',9999," + Session["DepreciationACid"].ToString() + "),";

                }

            }

            string a = insertqry + insertvalues;
            a = a.Remove(a.Length - 1);

            string run = clsCommon.getString(a);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Update !')", true);

        }
    }
    protected void btndeletedeppost_Click(object sender, EventArgs e)
    {
        string qry = clsCommon.getString("delete from nt_1_gledger where DOC_NO=9999 and TRAN_TYPE='DP' and COMPANY_CODE="
           + Session["Company_code"] + " and YEAR_CODE=" + Session["year"]);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Deleted !')", true);

    }
}