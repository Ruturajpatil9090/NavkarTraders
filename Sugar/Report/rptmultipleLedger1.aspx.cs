using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Report_rptmultipleLedger1 : System.Web.UI.Page
{
    #region data section

    double netdebit = 0.00; double netcredit = 0.00;
    string tblGLEDGER = string.Empty;
    string tblPrefix = string.Empty;
    string prefix = string.Empty;
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
    string fromdt = string.Empty;
    string todt = string.Empty;

    public static DataTable[] dtAll = null;

    public DataSet dsAll = null;
    public DataSet dsFinal = null;

    static WebControl objAsp = null;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
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
        tblGLEDGER = tblPrefix + "GLEDGER";
        tblDetails = tblPrefix + "VoucherDetails";
        AccountMasterTable = tblPrefix + "AccountMaster";
        cityMasterTable = tblPrefix + "CityMaster";

        if (!Page.IsPostBack)
        {
            dtAll = new DataTable[20];
            //  dtAll = null;
            bindData();


        }
    }

    private void bindData()
    {
        try
        {

            dsAll = new DataSet();

            string groupcode = Request.QueryString["groupcode"];

            string fromdt = Request.QueryString["fromdt"];
            string todt = Request.QueryString["todt"];
            fromdt = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            todt = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");


            DataTable dtMain = new DataTable();
            dtMain.Columns.Add("Ac_Code", typeof(Int32));
            dtMain.Columns.Add("Ac_Name_E", typeof(string));


            DataTable dtT = new DataTable();
            //   dtT = null;
            dtT.Columns.Add("TranType", typeof(string));
            dtT.Columns.Add("DocNo", typeof(Int32));
            dtT.Columns.Add("Date", typeof(string));
            dtT.Columns.Add("Narration", typeof(string));
            dtT.Columns.Add("Debit", typeof(double));
            dtT.Columns.Add("Credit", typeof(double));
            dtT.Columns.Add("Balance", typeof(double));
            dtT.Columns.Add("DrCr", typeof(string));

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string qry = "select AC_CODE,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from " + tblGLEDGER + " where DOC_DATE < '" + fromdt + "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by AC_CODE ";

            qry = "select [DOC_NO],[AC_CODE],[Ac_Name_E],[DOC_DATE],[TRAN_TYPE] from [dbo].[qryGledgernew] where [Group_Code]=" + groupcode + " and DOC_DATE < '" + todt + "'  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by [DOC_NO],[AC_CODE],[Ac_Name_E],[DOC_DATE],[TRAN_TYPE] order by [AC_CODE],[DOC_DATE],[TRAN_TYPE],[DOC_NO]";

            qry = "select [TRAN_TYPE], [DOC_NO],[AC_CODE],[Ac_Name_E],[AMOUNT],[DRCR] ,DOC_DATE,[NARRATION] from [dbo].[qryGledgernew] where [Group_Code]=" + groupcode + " and [COMPANY_CODE]=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  order by [AC_CODE],[DOC_DATE],[TRAN_TYPE],[DOC_NO] ";
            ds = clsDAL.SimpleQuery(qry);

            double opBal = 0.0;
            Int32 accode = -1;
            double amt = 0.00;

            if (ds != null)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    Int32 v = 0;
                    v = Convert.ToInt32(dt.Rows[0]["AC_CODE"].ToString());
                    DataRow drr = dtMain.NewRow();
                    drr[0] = Convert.ToInt32(dt.Rows[0]["AC_CODE"].ToString());
                    drr[1] = dt.Rows[0]["Ac_Name_E"].ToString();
                    dtMain.Rows.Add(drr);

                    int j = 0;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        accode = Convert.ToInt32(dt.Rows[i]["AC_CODE"].ToString());
                        if (accode == v)
                        {

                            qry = "select SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as  OpBal from " + tblGLEDGER + " where DOC_DATE < '" + fromdt + "' and Ac_code=" + accode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                            string str = clsCommon.getString(qry);
                            if (str != string.Empty)
                            {
                                opBal = Convert.ToDouble(str);
                            }
                            //if (dt.Rows[i]["DRCR"].ToString() == "D")
                            //{
                            //    opBal = opBal + (Convert.ToInt32(dt.Rows[i]["AMOUNT"].ToString()));
                            //}
                            //else
                            //{
                            //    opBal = opBal - (Convert.ToInt32(dt.Rows[i]["AMOUNT"].ToString()));
                            //}
                            DataRow dr = dtT.NewRow();
                            dr["TranType"] = dt.Rows[i]["TRAN_TYPE"].ToString();
                            dr["DocNo"] = dt.Rows[i]["DOC_NO"].ToString();
                            dr["Date"] = dt.Rows[i]["DOC_DATE"].ToString();
                            dr["Narration"] = dt.Rows[i]["NARRATION"].ToString();
                            //dr["Debit"] = dt.Rows[i]["TRAN_TYPE"].ToString();
                            //dr["Credit"] = dt.Rows[i]["TRAN_TYPE"].ToString();
                            //dr["Balance"] = dt.Rows[i]["TRAN_TYPE"].ToString();
                            //dr["DrCr"] = dt.Rows[i]["TRAN_TYPE"].ToString();

                            //dr[""]=

                            if (dt.Rows[i]["DRCR"].ToString() == "D")
                            {
                                amt = amt + Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());

                                dr[4] = string.Format("{0:0.00}", Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString()));
                                dr[5] = 0.00;
                                netdebit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                            }
                            else
                            {
                                amt = amt - Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                                netcredit += Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());

                                dr[4] = 0.00;
                                dr[5] = Convert.ToDouble(dt.Rows[i]["AMOUNT"].ToString());
                            }

                            if (amt > 0)
                            {
                                dr[6] = Math.Round(Convert.ToDouble(amt), 2);
                                dr[7] = "Dr";
                            }
                            else
                            {
                                dr[6] = 0 - Math.Round(amt, 2);
                                dr[7] = "Cr";
                            }

                            dtT.Rows.Add(dr);
                        }
                        else
                        {
                            v = Convert.ToInt32(dt.Rows[i]["AC_CODE"].ToString());

                            drr = dtMain.NewRow();
                            drr[0] = Convert.ToInt32(dt.Rows[i]["AC_CODE"].ToString());
                            drr[1] = dt.Rows[i]["Ac_Name_E"].ToString();
                            dtMain.Rows.Add(drr);

                            //    DataTable dt1  = new DataTable();
                            //   dt1 = dtT;
                            dsAll.Tables.Add(dtT);

                            //   dtT.Rows.Clear();
                            dtT = new DataTable();
                            dtT.Columns.Add("TranType", typeof(string));
                            dtT.Columns.Add("DocNo", typeof(Int32));
                            dtT.Columns.Add("Date", typeof(string));
                            dtT.Columns.Add("Narration", typeof(string));
                            dtT.Columns.Add("Debit", typeof(double));
                            dtT.Columns.Add("Credit", typeof(double));
                            dtT.Columns.Add("Balance", typeof(double));
                            dtT.Columns.Add("DrCr", typeof(string));


                            dsFinal = dsAll;


                            //   dsAll.Tables[j] = (DataTable)dtT;

                            //   dtAll[j] = new DataTable();

                            //if (j != 0)
                            //{
                            //    DataTable dtTbl = dtAll[j - 1];
                            //    dtAll[j] = (DataTable)dtT;

                            //    dtAll[j - 1] = dtTbl;
                            //}
                            //else
                            //{
                            //  //  DataTable dtTbl = dtAll[j - 1];
                            //    dtAll[j] = (DataTable)dtT;
                            //}

                            j++;


                            i = i - 1;
                            amt = 0.00;

                            ViewState["tbl"] = (DataTable)dtT;
                        }
                    }

                    dtl_Group.DataSource = dtMain;
                    dtl_Group.DataBind();

                }
            }
        }
        catch (Exception ec)
        {
            Response.Write(ec.Message);
        }
    }

    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tempcounter = tempcounter + 1;
            if (tempcounter == 10)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }

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

    protected void dtl_Group_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            string fromdt = Request.QueryString["fromdt"];
            string todt = Request.QueryString["todt"];
            Label lblCompany = (Label)e.Item.FindControl("lblCompany");
            Label lblFromDt = (Label)e.Item.FindControl("lblFromDt");
            Label lblToDt = (Label)e.Item.FindControl("lblToDt");
            Label lblPartyCode = (Label)e.Item.FindControl("lblPartyCode");
            lblFromDt.Text = DateTime.Parse(fromdt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            lblToDt.Text = DateTime.Parse(todt, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");

            GridView grdDetail = (GridView)e.Item.FindControl("grdDetail");

            int j = e.Item.ItemIndex;
            lblCompany.Text = Session["Company_Name"].ToString();
            this.bindGrid(lblPartyCode.Text, grdDetail, j);


        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    private void bindGrid(string p, GridView grdDetail, int j)
    {
        try
        {
            DataTable dt = new DataTable();
            //if (ViewState["tbl"] != null)
            //{
            //    dt = (DataTable)ViewState["tbl"];

            //   dt = dtAll[j];
            dt = dsAll.Tables[j];
            grdDetail.DataSource = dt;
            grdDetail.DataBind();

            grdDetail.FooterRow.Cells[3].Text = "Total";
            grdDetail.FooterRow.Cells[4].Text = netdebit.ToString();
            grdDetail.FooterRow.Cells[5].Text = netcredit.ToString();
            if (netdebit - netcredit != 0)
            {
                grdDetail.FooterRow.Cells[6].Text = Math.Round((netdebit - netcredit), 2).ToString();
            }
            else
            {
                grdDetail.FooterRow.Cells[6].Text = "Nil";

            }
            grdDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            grdDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;


            //   netcredit = 0.00;
            //   netdebit = 0.00;
            // }
        }
        catch (Exception ee)
        {

        }
    }
}