using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_Report_rptJaggeryBalanceNetWt : System.Web.UI.Page
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
    string toDTnew1;

    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        if (!IsPostBack)
        {
            fromDT = Request.QueryString["Fromdate"];
            toDT = Request.QueryString["Todate"];
            lblCompanyName.Text = Session["Company_Name"].ToString();

            fromDTnew = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM-dd-yyyy");
            toDTnew = DateTime.Parse(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM-dd-yyyy");

            toDTnew1 = DateTime.Parse(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");

            lblfrotodate.Text = "From " + fromDT + " To " + toDT;
            this.BindList();
        }

    }
    private void BindList()
    {
        try
        {

            qry = "delete from NT_1_Awakhead_Temp";
            ds = clsDAL.SimpleQuery(qry);

            qry = "delete from NT_1_AwakDetail_Temp";
            ds = clsDAL.SimpleQuery(qry);

            qry = "INSERT INTO NT_1_Awakhead_Temp SELECT * FROM NT_1_Awakhead WHERE DOC_DATE <= '" + toDTnew1 + "' and TRAN_TYPE='JW' and Company_Code="
                + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]<=" + Convert.ToInt32(Session["year"].ToString());
            ds = clsDAL.SimpleQuery(qry);

            qry = "INSERT INTO NT_1_AwakDetail_Temp SELECT * FROM NT_1_AwakDetail WHERE doc_no in(select doc_no from NT_1_Awakhead_Temp group by doc_no ) and TRAN_TYPE='JW' and Company_Code="
                + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]=" + Convert.ToInt32(Session["year"].ToString());
            ds = clsDAL.SimpleQuery(qry);


            //  qry = "Select [doc_no],CONVERT(VARCHAR(10),DOC_DATE,103) as DOC_DATE,[itemname],[supplierName],[awakqty],[Net_Wt]," +
            //     "[balnceqty],[balNetweight],Purc_Rate,bal  from [NT_1_qryJaggeryBalanceNetWt_Temp] where " +
            //" balNetweight != 0 and DOC_DATE between'" + fromDTnew + "' and '" + toDTnew + "' and " +
            // " Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]=" + Convert.ToInt32(Session["year"].ToString());

            qry = "Select [doc_no],CONVERT(VARCHAR(10),DOC_DATE,103) as DOC_DATE,[itemname],[supplierName],[awakqty],[Net_Wt]," +
               "[balnceqty],[balNetweight],Purc_Rate,bal  from [NT_1_qryJaggeryBalanceNetWt_Temp] where " +
          " balNetweight != 0 and DOC_DATE <= '" + toDTnew + "' and " +
           " Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]=" + Convert.ToInt32(Session["year"].ToString());





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

                    double pqty = 0.0;
                    double pnetwt = 0.0;
                    double bqty = 0.0;
                    double nbnetwt = 0.0;
                    double bal = 0.0;
                    //double roundsupercost = 0.0;
                    //// roundsupercost = Convert.ToDouble(dt.Compute("SuperCost", string.Empty));


                    pqty = Convert.ToDouble(dt.Compute("SUM(awakqty)", string.Empty));
                    pnetwt = Convert.ToDouble(dt.Compute("SUM(Net_Wt)", string.Empty));
                    bqty = Convert.ToDouble(dt.Compute("SUM(balnceqty)", string.Empty));
                    nbnetwt = Convert.ToDouble(dt.Compute("SUM(balNetweight)", string.Empty));
                    bal = Convert.ToDouble(dt.Compute("SUM(bal)", string.Empty));

                    ////supercost=Math.Round(supercost,2);

                    lblpqty.Text = pqty.ToString();
                    lblpnetwt.Text = pnetwt.ToString();
                    lblbqty.Text = bqty.ToString();
                    lblbnetwt.Text = nbnetwt.ToString();
                    lblbal.Text = bal.ToString();


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
        Label lblpartypan = (Label)e.Item.FindControl("lblpartypan");
        Label lblsupplier = (Label)e.Item.FindControl("lblsupplier");
        Label lblaccode = (Label)e.Item.FindControl("lblaccode");
        string str = clsCommon.getString("select CompanyPan from NT_1_AccountMaster where Ac_Code=" + lblaccode.Text +
                       " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //lblpartypan.Text = str.ToString();

        //try
        //{
        //    using (clsDataProvider obj = new clsDataProvider())
        //    {
        //        Label item_code = (Label)e.Item.FindControl("lblItemCode");
        //        stritemcode = item_code.Text;

        //      //  string date = DateTime.Parse(stritemcode, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM/dd/yyyy");
        //        //stritemcode = item_code.Text.ToString("dd/MM/yyyy")
        //        DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
        //        qry = "Select  top(1) AC_CODE as AC_CODE,DOC_DATE,SUM(purchasevalue) as Item_vaue,SUM(tdsamount)as tds,SUM(adat)as adat " +
        //    "from NT_1_qryAwak where DOC_DATE between'" + fromDTnew + "' and '" + toDTnew + "' and " +
        //    "Company_Code=3 and [Year_Code]=1 group by AC_CODE,DOC_DATE order by DOC_DATE";
        //        DataSet dsMill = new DataSet();
        //        dsMill = clsDAL.SimpleQuery(qry);
        //        //dsMill = obj.GetDataSet(qry);
        //        double Totalbalance = 0.0;
        //        Int32 qty = 0;
        //        double itemvalue = 0.0;
        //        double mcess = 0.0;
        //        double scost = 0.0;
        //        double levi = 0.0;
        //        double adat = 0.0;
        //        double tdsamount = 0.0;
        //        double billamount = 0.0;


        //        if (dsMill != null)
        //        {

        //                DataTable dtMill = new DataTable();
        //                dtMill = dsMill.Tables[0];

        //                dtlDetails.DataSource = dtMill;
        //                dtlDetails.DataBind();


        //        }
        //        //}
        //    }
        //}
        //catch (Exception)
        //{
        //    throw;
        //}
    }

}