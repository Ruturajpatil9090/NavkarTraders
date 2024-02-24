using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_Report_rptPendingBills : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        if (!IsPostBack)
        {
            fromDT = Request.QueryString["Fromdate"];
            toDT = Request.QueryString["Todate"];
            lblCompanyName.Text = Session["Company_Name"].ToString();
            //fromDTnew = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM/dd/yyyy");
            //toDTnew = DateTime.Parse(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM/dd/yyyy");
            //stritemcode = item_code.Text.ToString("dd/MM/yyyy")

            this.BindList();
        }

    }
    private void BindList()
    {
        try
        {
            qry = "select DOC_DATE from NT_1_qryPendingBillsFromJawak where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                " and Year_Code="
                + Convert.ToInt32(Session["year"].ToString()) +
                "  union select DOC_DATE from NT_1_qryJaggaryPendingBill " +
                "where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]="
                + Convert.ToInt32(Session["year"].ToString());
            //using (clsDataProvider obj = new clsDataProvider())
            //{
            //qry = "Select  distinct(CONVERT(VARCHAR(10),DOC_DATE,103)) as DOC_DATE from qryAwakBalance where DOC_DATE between '" + fromDTnew + "' and '" + toDTnew + "' and Company_Code="
            //    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]=" + Convert.ToInt32(Session["year"].ToString()) + " order by DOC_DATE";
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

                string date = DateTime.Parse(stritemcode, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM/dd/yyyy");
                //stritemcode = item_code.Text.ToString("dd/MM/yyyy");
                item_code.Text = date;
                DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
                //qry = "select * from NT_1_qryJaggaryPendingBill where DOC_DATE="+date+;
                //qry = "select distinct(Bill_No),Customer_Name from NT_1_qryJaggaryPendingBill where Company_Code="
                //    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                //    + " and DOC_DATE='" + date + "'";
                qry = "select Bill_No,Customer_Name,TRAN_TYPE from NT_1_qryPendingBillsFromJawak where Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code="
                    + Convert.ToInt32(Session["year"].ToString()) +
                    " union select Bill_No,Customer_Name,TRAN_TYPE from NT_1_qryJaggaryPendingBill where Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code="
                    + Convert.ToInt32(Session["year"].ToString());
                DataSet dsMill = new DataSet();
                dsMill = clsDAL.SimpleQuery(qry);
                //dsMill = obj.GetDataSet(qry);
                //double Totalbalance = 0.0;
                //Int32 qty = 0;
                //double itemvalue = 0.0;
                //double mcess = 0.0;
                //double scost = 0.0;
                //double levi = 0.0;
                //double adat = 0.0;
                //double tdsamount = 0.0;
                //double billamount = 0.0;


                if (dsMill != null)
                {
                    if (dsMill.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtMill = new DataTable();
                        dtMill = dsMill.Tables[0];
                        Label lblqty = (Label)e.Item.FindControl("lblqty");
                        Label lblitemvalue = (Label)e.Item.FindControl("lblitemvalue");
                        Label lblmarketsess = (Label)e.Item.FindControl("lblmarketsess");
                        Label lblsupercost = (Label)e.Item.FindControl("lblsupercost");
                        Label lbllevi = (Label)e.Item.FindControl("lbllevi");
                        Label lbladat = (Label)e.Item.FindControl("lbladat");
                        Label lbltdsamt = (Label)e.Item.FindControl("lbltdsamt");
                        Label lblbillamount = (Label)e.Item.FindControl("lblbillamount");
                        //lblInward.Text = netInward.ToString();
                        //lblNetInwardValue.Text = NetInwardValue.ToString();
                        //lblOutward.Text = lblOutward.ToString();
                        //lblBalance.Text = bal.ToString();

                        //qty = Convert.ToInt32(dtMill.Compute("SUM(netqty)", string.Empty));
                        //itemvalue = Convert.ToDouble(dtMill.Compute("SUM(purchasevalue)", string.Empty));
                        //mcess = Convert.ToDouble(dtMill.Compute("SUM(MARKETSES)", string.Empty));
                        //scost = Convert.ToDouble(dtMill.Compute("SUM(supercost)", string.Empty));
                        //levi = Convert.ToDouble(dtMill.Compute("SUM(levi)", string.Empty));
                        //adat = Convert.ToDouble(dtMill.Compute("SUM(adat)", string.Empty));
                        //tdsamount = Convert.ToDouble(dtMill.Compute("SUM(tdsamount)", string.Empty));
                        //billamount = Convert.ToDouble(dtMill.Compute("SUM(AMOUNT)", string.Empty));
                        ////itemvalue = Convert.ToDouble(dtMill.Compute("SUM(purchasevalue)", string.Empty));
                        //// Label lblbalance = (Label)e.Item.FindControl("lblbalance");


                        //lblqty.Text = qty.ToString();
                        //lblitemvalue.Text = itemvalue.ToString();
                        //lblmarketsess.Text = mcess.ToString();
                        //lblsupercost.Text = scost.ToString();
                        //lbllevi.Text = levi.ToString();
                        //lbladat.Text = adat.ToString();
                        //lbltdsamt.Text = tdsamount.ToString();
                        //lblbillamount.Text = billamount.ToString();

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