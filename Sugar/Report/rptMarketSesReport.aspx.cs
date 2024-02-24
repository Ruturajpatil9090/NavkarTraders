using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_Report_rptMarketSesReport : System.Web.UI.Page
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
            lblfrotodate.Text = "From " + fromDT + " To " + toDT;
            this.BindList();
        }

    }
    private void BindList()
    {
        try
        {



            qry = "Select doc_no,billno as BillNo,CONVERT(VARCHAR(10),DOC_DATE,103) as DOC_DATE,Ac_Name_E as Customer,netqty," +
                "SUM(purchasevalue)as Item_Amt,convert(DECIMAL(10,2),round(((purchasevalue)*(cessrate))/100,2)) as Cess, " +
                " convert(DECIMAL(10,2),round(((purchasevalue)*(superrate))/100,2)) as SuperCost" +
                " from NT_1_qryAwakMarketSes where " +
           " CessApplicable<>'Y' and DOC_DATE between'" + fromDTnew + "' and '" + toDTnew + "' and " +
            " Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]=" + Convert.ToInt32(Session["year"].ToString())
            + " group by doc_no,billno,DOC_DATE,netqty,Ac_Name_E,cessrate,purchasevalue,superrate";


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

                    double amt = 0.0;
                    double qty = 0.0;
                    double cess = 0.0;
                    double supercost = 0.0;
                    double roundsupercost = 0.0;
                    // roundsupercost = Convert.ToDouble(dt.Compute("SuperCost", string.Empty));


                    amt = Convert.ToDouble(dt.Compute("SUM(Item_Amt)", string.Empty));
                    qty = Convert.ToDouble(dt.Compute("SUM(netqty)", string.Empty));
                    cess = Convert.ToDouble(dt.Compute("SUM(Cess)", string.Empty));
                    supercost = Convert.ToDouble(dt.Compute("SUM(SuperCost)", string.Empty));

                    //supercost=Math.Round(supercost,2);

                    lblamt.Text = amt.ToString();
                    lblqty.Text = qty.ToString();
                    lblcess.Text = cess.ToString();
                    lblsupercost.Text = supercost.ToString();



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