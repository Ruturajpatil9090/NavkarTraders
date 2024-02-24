using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using System.Threading;
using System.Data.SqlClient;

public partial class Sugar_BussinessRelated_PgeDoInformationUtility : System.Web.UI.Page
{
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    string cs = string.Empty;
    string qryCommon = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        qryCommon = "qrydohead";
        cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        con = new SqlConnection(cs);
        if (!IsPostBack)
        {
            BindDummyRow();
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        //SetFocus(btnAdd);
    }
    private void BindDummyRow()
    {
        DataTable dummy = new DataTable();
        dummy.Columns.Add("doc_no");
        dummy.Columns.Add("doc_date");
        dummy.Columns.Add("purc_no");
        dummy.Columns.Add("purc_order");
        dummy.Columns.Add("millshortname");
        dummy.Columns.Add("quantal");
        dummy.Columns.Add("billtoshortname");
        dummy.Columns.Add("salebillcityname");
        dummy.Columns.Add("shiptoshortname");
        dummy.Columns.Add("shiptocityname");
        dummy.Columns.Add("sale_rate");
        dummy.Columns.Add("Tender_Commission");
        dummy.Columns.Add("desp_type");
        dummy.Columns.Add("truck_no");
        dummy.Columns.Add("SB_No");
        dummy.Columns.Add("EWay_Bill_No");
        dummy.Columns.Add("Delivery_Type");
        dummy.Columns.Add("transportshortname");
        dummy.Columns.Add("MM_Rate");
        dummy.Columns.Add("doid");

        dummy.Rows.Add();
        gvCustomers.DataSource = dummy;
        gvCustomers.DataBind();
    }
    [WebMethod]
    public static string GetCustomers(string searchTerm, int pageIndex, string Trantype, string Trantype1, string Trantype2, int PageSize, int Company_Code, int year, string fromdate, string todate)
    {
        string searchtxt = "";
        string delimStr = "";
        char[] delimiter = delimStr.ToCharArray();
        string words = "";
        string[] split = null;
        string name = string.Empty;
   
        searchtxt = searchTerm;
        words = searchTerm;
        split = words.Split(delimiter);
        foreach (var s in split)
        {
            string aa = s.ToString();
            // name += "Doc_No Like '%" + aa + "%'or";
            name += "( millshortname like '%" + aa + "%' or billtoshortname like  '%" + aa + "%' or doc_dateConverted like '%" + aa +
                "%' or doc_no like '%" + aa + "%' or desp_type like '%" + aa + "%' or quantal like '%" + aa + "%' or sale_rate like '%"
                + aa + "%' or SB_No like '%" + aa + "%'or doid like '%" + aa + "%' or  truck_no like '%" + aa + "%' or  shiptoshortname like '%" 
                + aa + "%' or  shiptocityname like '%" + aa + "%'or  salebillcityname like '%" + aa + "%' or  EWay_Bill_No like '%" + aa + "%' or  Delivery_Type like '%"
                + aa + "%' or transportshortname like '%" + aa + "%' or MM_Rate like '%" + aa + "%') and";


        }
        name = name.Remove(name.Length - 3);
        fromdate = DateTime.Parse(fromdate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        todate = DateTime.Parse(todate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

        string query = "";
        if (Trantype1 =="E")
        {
            query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,doc_no,doc_dateConverted as doc_date,"
         + " purc_no,purc_order,millshortname,quantal,billtoshortname,salebillcityname,shiptoshortname,shiptocityname,sale_rate,Tender_Commission,desp_type ,truck_no,SB_No,EWay_Bill_No,Delivery_Type,transportshortname,MM_Rate,doid FROM qrydohead   where " + name + " and desp_type='" + Trantype + "' and Company_Code=" + Company_Code + " " +
         " and Year_Code=" + year + " AND    ISEInvoice='" + Trantype2 + "' and doc_date between '"+fromdate+"' and '"+todate+"' order by doc_no desc";
        }
        else if (Trantype1 == "M")
        {
            query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,doc_no,doc_dateConverted as doc_date,"
        + " purc_no,purc_order,millshortname,quantal,billtoshortname,salebillcityname,shiptoshortname,shiptocityname,sale_rate,Tender_Commission,desp_type ,truck_no,SB_No,EWay_Bill_No,Delivery_Type,transportshortname,MM_Rate,doid FROM qrydohead   where " + name + " and desp_type='" + Trantype + "' and Company_Code=" + Company_Code + " " +
        " and Year_Code=" + year + " AND   MailSend='" + Trantype2 + "' and doc_date between '" + fromdate + "' and '" + todate + "' order by doc_no desc";

        }
        else
        {
            query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,doc_no,doc_dateConverted as doc_date,"
        + " purc_no,purc_order,millshortname,quantal,billtoshortname,salebillcityname,shiptoshortname,shiptocityname,sale_rate,Tender_Commission,desp_type ,truck_no,SB_No,EWay_Bill_No,Delivery_Type,transportshortname,MM_Rate,doid FROM qrydohead   where " + name + " and desp_type='" + Trantype + "' and Company_Code=" + Company_Code + " " +
        " and Year_Code=" + year + " AND   IsPayment='" + Trantype2 + "' and doc_date between '" + fromdate + "' and '" + todate + "' order by doc_no desc";
        }

         //   query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,doc_no,doc_dateConverted as doc_date,"
         //+ " purc_no,purc_order,millshortname,quantal,billtoshortname,salebillcityname,shiptoshortname,shiptocityname,sale_rate,Tender_Commission,desp_type ,truck_no,SB_No,EWay_Bill_No,Delivery_Type,transportshortname,MM_Rate,doid FROM qrydohead   where " + name + " and desp_type='" + Trantype + "' and Company_Code=" + Company_Code + " " +
         //" and Year_Code=" + year + " AND   MailSend='" + Trantype1 + "' AND ISEInvoice='" + Trantype2 + "' AND IsPayment='" + Trantype3 + "' order by doc_no desc";
        SqlCommand cmd = new SqlCommand(query);
        cmd.CommandType = CommandType.Text;
        return GetData(cmd, pageIndex, PageSize).GetXml();

    }
    private static DataSet GetData(SqlCommand cmd, int pageIndex, int PageSize)
    {
        string RecordCount = "";
        string cs1 = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs1))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {

                cmd.Connection = con;
                sda.SelectCommand = cmd;
                DataSet dsreturn = new DataSet();
                using (DataSet ds = new DataSet())
                {
                    sda.Fill(ds);
                    int number = 1;
                    DataTable dtnew = new DataTable();
                    dtnew = ds.Tables[0];
                    for (int i = 0; i < dtnew.Rows.Count; i++)
                    {
                        dtnew.Rows[i][0] = number;
                        number = number + 1;

                    }
                    string f1 = " RowNumber >=(" + pageIndex + " -1) * (" + PageSize + "+1) and RowNumber<=";
                    string f2 = "(((" + pageIndex + " -1) * " + PageSize + " +1) +" + PageSize + ")-1";

                    DataRow[] results = ds.Tables[0].Select(f1 + f2, "doc_no desc");
                    if (results.Count() > 0)
                    {
                        DataTable dt1 = results.CopyToDataTable();
                        dt1.TableName = "Customers";
                        DataTable dt = new DataTable("Pager");
                        dt.Columns.Add("PageIndex");
                        dt.Columns.Add("PageSize");
                        dt.Columns.Add("RecordCount");
                        dt.Rows.Add();
                        RecordCount = ds.Tables[0].Rows.Count.ToString();

                        dt.Rows[0]["PageIndex"] = pageIndex;
                        dt.Rows[0]["PageSize"] = PageSize;
                        dt.Rows[0]["RecordCount"] = RecordCount;

                        dsreturn = new DataSet();
                        dsreturn.Tables.Add(dt1);
                        dsreturn.Tables.Add(dt);
                        return dsreturn;
                    }
                    else
                    {
                        return dsreturn;
                    }

                }
            }
        }
    }  
  
}