using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_Report_rptJaggeryStockBook : System.Web.UI.Page
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
            fromDTnew = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM/dd/yyyy");
            toDTnew = DateTime.Parse(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM/dd/yyyy");
            this.BindList();
        }

    }
    private void BindList()
    {
        try
        {
            //using (clsDataProvider obj = new clsDataProvider())
            //{
            qry = "Select  distinct([Item_Code]),[itemname] from " + tblPrefix + "qryJABalance where balnceqty<>0 and doc_date between '" + fromDTnew + "' and '" + toDTnew + "' and Company_Code="
                + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]=" + Convert.ToInt32(Session["year"].ToString()) + " order by Item_Code";
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
                DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
                qry = "select [doc_no],CONVERT(VARCHAR(10),DOC_DATE,103) as DOC_DATE,[supplierName],[itemname],[awakqty],ISNULL(NULLIF(despatch,NULL),0) as Qty, " +
                    "[balnceqty] from "
                    + tblPrefix + "qryJABalance where balnceqty<>0 and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                    + " and doc_date between '" + fromDTnew + "' and '" + toDTnew + "' and Item_Code=" + item_code.Text;
                DataSet dsMill = new DataSet();
                dsMill = clsDAL.SimpleQuery(qry);
                //dsMill = obj.GetDataSet(qry);
                double Totalbalance = 0.0;


                if (dsMill != null)
                {
                    if (dsMill.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtMill = new DataTable();
                        dtMill = dsMill.Tables[0];
                        // Label lblInward = (Label)e.Item.FindControl("lblInward");
                        //Label lblNetInwardValue = (Label)e.Item.FindControl("lblNetInwardValue");
                        // Label lblOutward = (Label)e.Item.FindControl("lblOutward");
                        //Label lblNetOutwardValue = (Label)e.Item.FindControl("lblNetOutwardValue");
                        // lblInward.Text = netInward.ToString();
                        //lblNetInwardValue.Text = NetInwardValue.ToString();
                        // lblOutward.Text = lblOutward.ToString();
                        // lblBalance.Text = bal.ToString();


                        Totalbalance = Convert.ToDouble(dtMill.Compute("SUM(balnceqty)", string.Empty));


                        Label lblbalance = (Label)e.Item.FindControl("lblbalance");


                        lblbalance.Text = Totalbalance.ToString();

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