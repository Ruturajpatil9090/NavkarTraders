using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

public partial class Report_rptPartyWiseSugarStock : System.Web.UI.Page
{
    double grandTotal = 0.00;
    double grandTotalDisp = 0.00;
    double grandTotalBal = 0.00;
    SqlConnection con = null;
    DataSet ds;
    DataSet bindqry;
    DataTable d2;
    string qry = string.Empty;
    DataTable dt;
    string tblPrefix = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        string cs = string.Empty;
        cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        con = new SqlConnection(cs);
        tblPrefix = Session["tblPrefix"].ToString();
        user = Session["user"].ToString();
        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {

                grandTotal = 0.0;
                grandTotalDisp = 0.0;
                grandTotalBal = 0.0;
                BindList();
                string fromDt = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                string toDt = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                //this.ifrmCorporateSell.Attributes.Add("src", "rptCarporateBalance.aspx?fromDt=" + fromDt + "&toDt=" + toDt + "&Branch_Code=");
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }

        }
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "khs", "javascript:pst();", true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "khsss", "javascript:lwst();", true);
    }

    private void BindList()
    {
        try
        {
            //using (clsDataProvider obj = new clsDataProvider())
            //{


            //qry = "select Buyer,buyername  from qrytenderdobalanceview  where  Buyer_Quantal!=0 and BALANCE!=0 and " +
            //    " Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " +
            //    " year_code=" + Convert.ToInt32(Session["year"].ToString()) + " group by Buyer,buyername  order by buyername  asc";

           // qry = "select Buyer,buyername  from qrytenderdobalanceview  where  Buyer_Quantal!=0 and BALANCE!=0 and " +
           //    " Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) 
           //+ " group by Buyer,buyername  order by buyername  asc";

            qry = "select Buyer,qrybuyer.Ac_Name_E AS buyername from qrytenderdobalanceviewraw LEFT OUTER JOIN" +
                 " dbo.qrymstaccountmaster AS qrybuyer ON dbo.qrytenderdobalanceviewraw.buyerid = qrybuyer.accoid" +
                 " where  Buyer_Quantal!=0 and BALANCE!=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                 + " group by Buyer,Ac_Name_E  order by Ac_Name_E  asc";




            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);

            //qry = "select  Tender_No,tenderid,millshortname,Grade,Mill_Rate,(Sale_Rate+Commission_Rate) as Sale_Rate,Lifting_DateConverted as Tender_Date,Buyer_Quantal," +
            // "DESPATCH,BALANCE,tenderdoshortname,Buyer,tenderdetailid from " +
            //"qrytenderdobalanceview where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and "
            // + "  year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and  Buyer_Quantal!=0 "
            //+ " GROUP BY Tender_No,tenderid,millshortname,Grade,Mill_Rate,Sale_Rate,Tender_Date,Buyer_Quantal,DESPATCH,BALANCE,tenderdoname,Lifting_DateConverted,tenderdoshortname,Buyer,Commission_Rate,tenderdetailid order by millshortname ";

          //  qry = "select  Tender_No,tenderid,millshortname,Grade,Mill_Rate,(Sale_Rate+Commission_Rate) as Sale_Rate,Lifting_DateConverted as Tender_Date,Buyer_Quantal," +
          // "DESPATCH,BALANCE,tenderdoshortname,Buyer,tenderdetailid from " +
          //"qrytenderdobalanceview where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and "
          //  + "  Buyer_Quantal!=0 "
          //+ " GROUP BY Tender_No,tenderid,millshortname,Grade,Mill_Rate,Sale_Rate,Tender_Date,Buyer_Quantal,DESPATCH,BALANCE,tenderdoname,Lifting_DateConverted,tenderdoshortname,Buyer,Commission_Rate,tenderdetailid order by millshortname ";

            qry = "select  dbo.qrytenderhead.Tender_No, dbo.qrytenderdobalanceviewraw.tenderid,dbo.qrytenderhead.millshortname,dbo.qrytenderhead.Grade"+
                  ",dbo.qrytenderhead.Mill_Rate,(dbo.qrytenderdobalanceviewraw.Sale_Rate+dbo.qrytenderdobalanceviewraw.Commission_Rate) as Sale_Rate,"+
                  " dbo.qrytenderhead.Lifting_DateConverted as Tender_Date,dbo.qrytenderdobalanceviewraw.Buyer_Quantal,dbo.qrytenderdobalanceviewraw.DESPATCH"+
                  ",dbo.qrytenderdobalanceviewraw.BALANCE,dbo.qrytenderhead.tenderdoshortname,dbo.qrytenderdobalanceviewraw.Buyer,dbo.qrytenderdobalanceviewraw.tenderdetailid"+
                  " FROM dbo.qrytenderdobalanceviewraw LEFT OUTER JOIN dbo.qrymstaccountmaster AS qrybuyerparty ON dbo.qrytenderdobalanceviewraw.buyerpartyid = qrybuyerparty.accoid LEFT OUTER JOIN"+
                  " dbo.qrymstaccountmaster AS qrybuyer ON dbo.qrytenderdobalanceviewraw.buyerid = qrybuyer.accoid LEFT OUTER JOIN"+
                  " dbo.qrytenderhead ON dbo.qrytenderdobalanceviewraw.tenderid = dbo.qrytenderhead.tenderid"+
                  " where  dbo.qrytenderhead.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and   Buyer_Quantal!=0  " +
                  " GROUP BY Tender_No,dbo.qrytenderdobalanceviewraw.tenderid,millshortname,Grade,Mill_Rate,Sale_Rate,Tender_Date,Buyer_Quantal,"+
                  " DESPATCH,BALANCE,tenderdoname,Lifting_DateConverted,tenderdoshortname,Buyer,Commission_Rate,tenderdetailid order by millshortname ";
          
            
            
            bindqry = clsDAL.SimpleQuery(qry);
            d2 = bindqry.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblCmpName.Text = clsCommon.getString("Select Company_Name_E from  Company where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                    lblTransportName.Text = "Balance Stock As On <b>" + System.DateTime.Now.ToString("dd/MM/yyyy hh:MM:ss tt") + "</b>";
                    dtlist.DataSource = dt;
                    dtlist.DataBind();
                    lblQntlGrandTotal.Text = grandTotal.ToString();
                    lblGrandDispTotal.Text = grandTotalDisp.ToString();
                    lblGrandBalTotal.Text = grandTotalBal.ToString();
                }
                else
                {
                    dtlist.DataSource = null;
                    dtlist.DataBind();
                }
            }
            else
            {
                dtlist.DataSource = null;
                dtlist.DataBind();
            }



            //}
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            clsDAL.CloseConnection();
        }
    }
    protected void lnkDO_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnkOV = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkOV.NamingContainer;
            string fromdt1 = clsGV.Start_Date;
            string todt1 = clsGV.To_date;
            string accode = lnkOV.Text;

            Int16 Action = 1;
            //String counts = clsCommon.getString("SELECT tenderid  from nt_1_tender where Tender_No=" + accode + 
            //    " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString() + "");
            String counts = clsCommon.getString("SELECT tenderid  from nt_1_tender where Tender_No=" + accode +
              " and Company_Code=" + Session["Company_Code"].ToString() + "");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + accode + "','" + counts + "','" + Action + "')", true);
            lnkOV.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void dtlist_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            //using (clsDataProvider obj = new clsDataProvider())
            //{
            DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
            Label lblPartyCode = (Label)e.Item.FindControl("lblPartyCode");
            Label lblDispTotal = (Label)e.Item.FindControl("lblDispTotal");
            Label lblBalTotal = (Label)e.Item.FindControl("lblBalTotal");
            string partycode = lblPartyCode.Text;



            //qry = "select  Tender_No,tenderid,millshortname,Grade,Mill_Rate,Sale_Rate,Lifting_DateConverted as Tender_Date,Buyer_Quantal,DESPATCH,BALANCE,tenderdoshortname from " +
            //    "qrytenderdobalanceview where Buyer=" + partycode + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and "
            //     + "  year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and  Buyer_Quantal!=0 "
            //    + " GROUP BY Tender_No,tenderid,millshortname,Grade,Mill_Rate,Sale_Rate,Tender_Date,Buyer_Quantal,DESPATCH,BALANCE,tenderdoname order by millshortname ";
            //DataSet ds2 = new DataSet();

            ////ds = obj.GetDataSet(qry);
            //ds2 = clsDAL.SimpleQuery(qry);

            DataView view = new DataView(d2, "Buyer='" + partycode + "'", "Buyer", DataViewRowState.CurrentRows);
            DataTable selectedvalue = view.ToTable(true, "Tender_No", "millshortname", "Grade", "Mill_Rate", "Sale_Rate", "Tender_Date", "tenderdoshortname", "Buyer_Quantal",
                "DESPATCH", "BALANCE", "Buyer", "tenderdetailid");

            if (selectedvalue.Rows.Count > 0)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("Tender_No", typeof(string)));
                dt.Columns.Add(new DataColumn("millshortname", typeof(string)));
                dt.Columns.Add(new DataColumn("Grade", typeof(string)));
                dt.Columns.Add(new DataColumn("Mill_Rate", typeof(string)));
                dt.Columns.Add(new DataColumn("Sale_Rate", typeof(string)));
                dt.Columns.Add(new DataColumn("Tender_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("tenderdoname", typeof(string)));
                dt.Columns.Add(new DataColumn("Buyer_Quantal", typeof(double)));
                dt.Columns.Add(new DataColumn("DESPATCH", typeof(double)));
                dt.Columns.Add(new DataColumn("BALANCE", typeof(double)));
                for (int i = 0; i < selectedvalue.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Tender_No"] = selectedvalue.Rows[i]["Tender_No"].ToString();
                    dr["millshortname"] = selectedvalue.Rows[i]["millshortname"].ToString();
                    dr["Grade"] = selectedvalue.Rows[i]["Grade"].ToString();
                    dr["Mill_Rate"] = selectedvalue.Rows[i]["Mill_Rate"].ToString();
                    dr["Sale_Rate"] = selectedvalue.Rows[i]["Sale_Rate"].ToString();
                    dr["Tender_Date"] = selectedvalue.Rows[i]["Tender_Date"].ToString();
                    dr["tenderdoname"] = selectedvalue.Rows[i]["tenderdoshortname"].ToString();
                    dr["Buyer_Quantal"] = selectedvalue.Rows[i]["Buyer_Quantal"].ToString();
                    dr["DESPATCH"] = selectedvalue.Rows[i]["DESPATCH"].ToString();
                    double Balance = Convert.ToDouble(selectedvalue.Rows[i]["BALANCE"].ToString());
                    dr["BALANCE"] = Balance;
                    if (Balance != 0)
                    {
                        dt.Rows.Add(dr);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    double qntlTotal = Convert.ToDouble(dt.Compute("SUM(Buyer_Quantal)", string.Empty));
                    grandTotal = grandTotal + qntlTotal;
                    double disp = Convert.ToDouble(dt.Compute("SUM(DESPATCH)", string.Empty));
                    lblDispTotal.Text = disp.ToString();
                    grandTotalDisp = grandTotalDisp + disp;
                    double bal = Convert.ToDouble(dt.Compute("SUM(BALANCE)", string.Empty));
                    lblBalTotal.Text = bal.ToString();
                    grandTotalBal = grandTotalBal + bal;
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
        pnlMain.RenderControl(tw);
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

    protected void btnmillwiseliftingwise_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "khs", "javascript:pst();", true);
    }

    protected void btnmillwise_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "khsss", "javascript:lwst();", true);
    }
    protected void btnSelfStock_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "khss", "javascript:sst();", true);
    }
}