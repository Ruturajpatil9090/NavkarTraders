using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_Report_rptPurchaseRegisterTDS : System.Web.UI.Page
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
    double grandmcess = 0.00;
    double grandscost = 0.00;
    double grandlevi = 0.00;
    double grandadat = 0.00;
    double AC_CODE = 0.00;
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
                    //lblgrandnetwt.Text = grandqty.ToString();
                    //lblgranditemvalue.Text = granditemvalue.ToString();
                    //lblgrandmcess.Text = grandmcess.ToString();
                    //lblgrandscost.Text = grandscost.ToString();

                    //lblgrandlevi.Text = grandlevi.ToString();
                    //lblgrnadadat.Text = grandadat.ToString();
                    //lblAC_CODE.Text =AC_CODE.ToString();
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
                //qry = "select distinct doc_no,CONVERT(VARCHAR(10),DOC_DATE,103) as DOC_DATE,Supplier,netqty,purchasevalue,MARKETSES,supercost,levihead,adat,tdsamount,AMOUNT,"
                //    + " convert(DECIMAL(10,2),round((AMOUNT)-(tdsamount),2)) as payable,billno from NT_1_qryAwakPurchase where Company_Code="
                //    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                //    + " and DOC_DATE='" + date + "'";
                qry = "select distinct doc_no,CONVERT(VARCHAR(10),DOC_DATE,103) as DOC_DATE,AC_CODE,Supplier,tdsamount,"
                  + " convert(DECIMAL(10,2),round((AMOUNT)-(tdsamount),2)) as payable,billno from NT_1_qryAwakPurchase where Company_Code="
                  + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                  + " and DOC_DATE='" + date + "'";
                DataSet dsMill = new DataSet();
                dsMill = clsDAL.SimpleQuery(qry);
                //dsMill = obj.GetDataSet(qry);
                double Totalbalance = 0.0;
                //Int32 qty = 0;
                //double itemvalue = 0.0;
                //double mcess = 0.0;
                //double scost = 0.0;
                //double levi = 0.0;
                //double adat = 0.0;
                double tdsamount = 0.0;
                double AC_CODE = 0.0;
                double payable = 0.0;

                if (dsMill != null)
                {
                    if (dsMill.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtMill = new DataTable();
                        dtMill = dsMill.Tables[0];
                        //Label lblqty = (Label)e.Item.FindControl("lblqty");
                        //Label lblitemvalue = (Label)e.Item.FindControl("lblitemvalue");
                        //Label lblmarketsess = (Label)e.Item.FindControl("lblmarketsess");
                        //Label lblsupercost = (Label)e.Item.FindControl("lblsupercost");
                        //Label lbllevi = (Label)e.Item.FindControl("lbllevi");
                        //Label lbladat = (Label)e.Item.FindControl("lbladat");
                        Label lbltdsamt = (Label)e.Item.FindControl("lbltdsamt");
                        Label lblAC_CODE = (Label)e.Item.FindControl("lblAC_CODE");
                        Label lblpayable = (Label)e.Item.FindControl("lblpayable");
                        //lblInward.Text = netInward.ToString();
                        //lblNetInwardValue.Text = NetInwardValue.ToString();
                        //lblOutward.Text = lblOutward.ToString();
                        //lblBalance.Text = bal.ToString();

                        //qty = Convert.ToInt32(dtMill.Compute("SUM(netqty)", string.Empty));
                        //itemvalue = Convert.ToDouble(dtMill.Compute("SUM(purchasevalue)", string.Empty));
                        //mcess = Convert.ToDouble(dtMill.Compute("SUM(MARKETSES)", string.Empty));
                        //scost = Convert.ToDouble(dtMill.Compute("SUM(supercost)", string.Empty));
                        //levi = Convert.ToDouble(dtMill.Compute("SUM(levihead)", string.Empty));
                        //adat = Convert.ToDouble(dtMill.Compute("SUM(adat)", string.Empty));
                        tdsamount = Convert.ToDouble(dtMill.Compute("SUM(tdsamount)", string.Empty));
                        AC_CODE = Convert.ToDouble(dtMill.Compute("SUM(AC_CODE)", string.Empty));
                        //itemvalue = Convert.ToDouble(dtMill.Compute("SUM(purchasevalue)", string.Empty));
                        // Label lblbalance = (Label)e.Item.FindControl("lblbalance");
                        payable = Convert.ToDouble(dtMill.Compute("SUM(payable)", string.Empty));

                        //grandqty += qty;
                        //granditemvalue += itemvalue;
                        //grandmcess += mcess;
                        //grandscost += scost;
                        //grandlevi += levi;
                        //grandadat += adat;
                        grandtdsamount += tdsamount;
                      AC_CODE += AC_CODE;
                        grandpayable += payable;



                        //lblqty.Text = qty.ToString();
                        //lblitemvalue.Text = itemvalue.ToString();
                        //lblmarketsess.Text = mcess.ToString();
                        //lblsupercost.Text = scost.ToString();
                        //lbllevi.Text = levi.ToString();
                        //lbladat.Text = adat.ToString();
                        lbltdsamt.Text = tdsamount.ToString();
                        lblAC_CODE.Text = AC_CODE.ToString();
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