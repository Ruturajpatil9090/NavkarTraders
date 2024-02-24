using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_Report_rptBillWiseCustomer : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string fromBill;
    string toBill;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    string stritemcode;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        if (!IsPostBack)
        {
            fromBill = Request.QueryString["FromBill"];
            toBill = Request.QueryString["ToBill"];
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
            qry = "select distinct (Bill_No) from NT_1_qryBillWiseCustomer where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                + " and Year_Code="
                + Convert.ToInt32(Session["year"].ToString()) +
                " and Bill_No between " + fromBill + " and " + toBill;
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

                // string date = DateTime.Parse(stritemcode, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM/dd/yyyy");
                //stritemcode = item_code.Text.ToString("dd/MM/yyyy");
                //item_code.Text = date;
                DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");

                qry = "select Bill_No,Customer_name from NT_1_qryBillWiseCustomer where Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code="
                    + Convert.ToInt32(Session["year"].ToString()) + " and Bill_No between " + stritemcode + " and " + stritemcode;
                ;
                DataSet dsMill = new DataSet();
                dsMill = clsDAL.SimpleQuery(qry);

                if (dsMill != null)
                {
                    if (dsMill.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtMill = new DataTable();
                        dtMill = dsMill.Tables[0];



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