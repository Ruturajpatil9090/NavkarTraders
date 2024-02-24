using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_Report_rptPurchaseStockGst : System.Web.UI.Page
{

    string tblPrefix = string.Empty;
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    string stritemcode = "1";
    string fromDTnew;
    string toDTnew;

    double grandqty = 0.00;
    double granditemvalue = 0.00;
    double grandIGST_Amount = 0.00;
    double grandCGST_Amount = 0.00;
    double grandSGST_Amount = 0.00;
    double grandadat = 0.00;
    double grandbillamount = 0.00;
    double grandtdsamount = 0.00;
    double grandpayable = 0.00;
    string tdsrate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        if (!IsPostBack)
        {
            fromDT = Request.QueryString["Fromdate"];
            toDT = Request.QueryString["Todate"];
            tdsrate = Request.QueryString["tdsrate"];
            lblCompanyName.Text = Session["Company_Name"].ToString();
            lbldaterange.Text = "Report From " + fromDT + " To " + toDT;
            //fromDTnew = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM/dd/yyyy");
            //toDTnew = DateTime.Parse(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM/dd/yyyy");
            fromDTnew = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            toDTnew = DateTime.Parse(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            this.BindList();
        }

    }

    private void BindList()
    {
        try
        {
            //using (clsDataProvider obj = new clsDataProvider())
            //{

            if (tdsrate == string.Empty || tdsrate == "0")
            {
                qry = "Select distinct(CONVERT(VARCHAR(10),DOC_DATE,102)) as DOC_DATE from NT_1_qryAwakPurchase where DOC_DATE between'" + fromDTnew + "' and '" + toDTnew + "' and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]=" + Convert.ToInt32(Session["year"].ToString()) + " order by DOC_DATE";
            }
            else
            {
                qry = "Select distinct(CONVERT(VARCHAR(10),DOC_DATE,102)) as DOC_DATE from NT_1_qryAwakPurchase where tdsperc=" + tdsrate + " and DOC_DATE between'" + fromDTnew + "' and '" + toDTnew + "' and Company_Code="
                   + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]=" + Convert.ToInt32(Session["year"].ToString()) + " order by DOC_DATE";
            }
            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            //ds = obj.GetDataSet(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    dtl.DataSource = dt;
                    dtl.DataBind();
                    lblgrandnetwt.Text = grandqty.ToString();
                    lblgranditemvalue.Text = granditemvalue.ToString();
                    lblgrandIGST_Amount.Text = grandIGST_Amount.ToString();
                    lblgrandCGST_Amount.Text = grandCGST_Amount.ToString();

                    lblgrandSGST_Amount.Text = grandSGST_Amount.ToString();
                    //lblgrnadadat.Text = grandadat.ToString();
                    lblgrandamount.Text = grandbillamount.ToString();
                    lblgrandtdsamnt.Text = grandtdsamount.ToString();
                    lblgrandpayable.Text = grandpayable.ToString();

                }
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

    protected void dtl_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            using (clsDataProvider obj = new clsDataProvider())
            {
                Label item_code = (Label)e.Item.FindControl("lblItemCode");
                stritemcode = item_code.Text;

                //string date = DateTime.Parse(stritemcode, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM/dd/yyyy");
                string date = DateTime.Parse(stritemcode, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

                //stritemcode = item_code.Text.ToString("dd/MM/yyyy");
                DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
                qry = "select distinct doc_no,CONVERT(VARCHAR(10),DOC_DATE,103) as DOC_DATE,Supplier,netqty,purchasevalue,MARKETSES,supercost,levihead,adat,tdsamount,AMOUNT,"
                    + " convert(DECIMAL(10,2),round((AMOUNT)-(tdsamount),2)) as payable,billno,IGST_Amount,CGST_Amount,SGST_Amount from NT_1_qryAwakPurchase where Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                    + " and DOC_DATE='" + date + "'";
                DataSet dsMill = new DataSet();
                dsMill = clsDAL.SimpleQuery(qry);
                //dsMill = obj.GetDataSet(qry);
                double Totalbalance = 0.0;
                Int32 qty = 0;
                double itemvalue = 0.0;
                double IGST_Amount = 0.0;
                double CGST_Amount = 0.0;
                double SGST_Amount = 0.0;
                double adat = 0.0;
                double tdsamount = 0.0;
                double billamount = 0.0;
                double payable = 0.0;

                if (dsMill != null)
                {
                    if (dsMill.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtMill = new DataTable();
                        dtMill = dsMill.Tables[0];
                        Label lblqty = (Label)e.Item.FindControl("lblqty");
                        Label lblitemvalue = (Label)e.Item.FindControl("lblitemvalue");
                        Label lblIGST_Amount = (Label)e.Item.FindControl("lblIGST_Amount");
                        Label lblCGST_Amount = (Label)e.Item.FindControl("lblCGST_Amount");
                        Label lblSGST_Amount = (Label)e.Item.FindControl("lblSGST_Amount");
                        Label lbladat = (Label)e.Item.FindControl("lbladat");
                        Label lbltdsamt = (Label)e.Item.FindControl("lbltdsamt");
                        Label lblbillamount = (Label)e.Item.FindControl("lblbillamount");
                        Label lblpayable = (Label)e.Item.FindControl("lblpayable");
                        //lblInward.Text = netInward.ToString();
                        //lblNetInwardValue.Text = NetInwardValue.ToString();
                        //lblOutward.Text = lblOutward.ToString();
                        //lblBalance.Text = bal.ToString();

                        qty = Convert.ToInt32(dtMill.Compute("SUM(netqty)", string.Empty));
                        itemvalue = Convert.ToDouble(dtMill.Compute("SUM(purchasevalue)", string.Empty));
                        IGST_Amount = Convert.ToDouble(dtMill.Compute("SUM(IGST_Amount)", string.Empty));
                        CGST_Amount = Convert.ToDouble(dtMill.Compute("SUM(CGST_Amount)", string.Empty));
                        SGST_Amount = Convert.ToDouble(dtMill.Compute("SUM(SGST_Amount)", string.Empty));
                        adat = Convert.ToDouble(dtMill.Compute("SUM(adat)", string.Empty));
                        tdsamount = Convert.ToDouble(dtMill.Compute("SUM(tdsamount)", string.Empty));
                        billamount = Convert.ToDouble(dtMill.Compute("SUM(AMOUNT)", string.Empty));
                        //itemvalue = Convert.ToDouble(dtMill.Compute("SUM(purchasevalue)", string.Empty));
                        // Label lblbalance = (Label)e.Item.FindControl("lblbalance");
                        payable = Convert.ToDouble(dtMill.Compute("SUM(payable)", string.Empty));

                        grandqty += qty;
                        granditemvalue += itemvalue;
                        grandIGST_Amount += IGST_Amount;
                        grandCGST_Amount += CGST_Amount;
                        grandSGST_Amount += SGST_Amount;
                        grandadat += adat;
                        grandtdsamount += tdsamount;
                        grandbillamount += billamount;
                        grandpayable += payable;



                        lblqty.Text = qty.ToString();
                        lblitemvalue.Text = itemvalue.ToString();
                        lblIGST_Amount.Text = IGST_Amount.ToString();
                        lblCGST_Amount.Text = CGST_Amount.ToString();
                        lblSGST_Amount.Text = SGST_Amount.ToString();
                        lbladat.Text = adat.ToString();
                        lbltdsamt.Text = tdsamount.ToString();
                        lblbillamount.Text = billamount.ToString();
                        lblpayable.Text = payable.ToString();
                        dtlDetails.DataSource = dtMill;
                        dtlDetails.DataBind();

                    }
                }
                //}
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}