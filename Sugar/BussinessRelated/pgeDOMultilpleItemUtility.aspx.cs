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
public partial class Sugar_BussinessRelated_pgeDOMultilpleItemUtility : System.Web.UI.Page
{
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    string cs = string.Empty;
    string qryCommon = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        qryCommon = "";
        cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        con = new SqlConnection(cs);
        if (!IsPostBack)
        {
            BindDummyRow();
        }
        SetFocus(btnAdd);
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
        dummy.Columns.Add("mill_rate");
        dummy.Columns.Add("sale_rate");
        dummy.Columns.Add("Tender_Commission");
        dummy.Columns.Add("desp_type");
        dummy.Columns.Add("truck_no");
        dummy.Columns.Add("SB_No");
        dummy.Columns.Add("EWay_Bill_No");
        dummy.Columns.Add("Delivery_Type");
        dummy.Columns.Add("transportshortname");
        dummy.Columns.Add("MM_Rate");
        dummy.Columns.Add("vasuli_rate");
        dummy.Columns.Add("doid");

        dummy.Rows.Add();
        gvCustomers.DataSource = dummy;
        gvCustomers.DataBind();
    }
    [WebMethod]
    public static string GetCustomers(string searchTerm, int pageIndex, string Trantype, string Trantype1, int PageSize, int Company_Code, int year)
    {
        string searchtxt = "";
        string delimStr = "";
        char[] delimiter = delimStr.ToCharArray();
        string words = "";
        string[] split = null;
        string name = string.Empty;
        //DateTime endD = DateTime.Parse(EndDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        //DateTime d = new DateTime();
        //if (FromDate != "" && ToDate == "")
        //{
        //    d = DateTime.Parse(FromDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));

        //    d = d.AddDays(+(Convert.ToInt32(30)));
        //    ToDate = d.ToString("yyyy/MM/dd");

        //}
        //if (ToDate == "")
        //{

        //    d = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
        //    if (endD > d)
        //    {
        //        ToDate = DateTime.Now.ToString("yyyy/MM/dd");
        //    }
        //    else
        //    {
        //        ToDate = endD.ToString("yyyy/MM/dd");
        //        d = endD;
        //    }

        //}
        //if (FromDate == "")
        //{
        //    d = d.AddDays(-(Convert.ToInt32(30)));
        //    FromDate = d.ToString("yyyy/MM/dd");

        //}
        searchtxt = searchTerm;
        words = searchTerm;
        split = words.Split(delimiter);
        foreach (var s in split)
        {
            string aa = s.ToString();
            // name += "Doc_No Like '%" + aa + "%'or";
            name += "( mill.Short_Name like '%" + aa + "%' or salebillto.Ac_Name_E like  '%" + aa + "%' or CONVERT(varchar(10), dbo.nt_1_deliveryorder.doc_date, 103) like '%" + aa +
                "%' or dbo.nt_1_deliveryorder.doc_no like '%" + aa + "%' or dbo.nt_1_deliveryorder.desp_type like '%" + aa + "%' or dbo.nt_1_deliveryorder.quantal like '%" + aa + "%' or dbo.nt_1_deliveryorder.sale_rate like '%"
                + aa + "%' or dbo.nt_1_deliveryorder.SB_No like '%" + aa + "%'or dbo.nt_1_deliveryorder.doid like '%" + aa + "%' or  dbo.nt_1_deliveryorder.truck_no like '%" + aa + "%' or  shipto.Ac_Name_E like '%"
                + aa + "%' or  shiptocity.city_name_e like '%" + aa + "%'or  salebillcity.city_name_e like '%" + aa + "%' or  dbo.nt_1_deliveryorder.EWay_Bill_No like '%" + aa + "%' or  dbo.nt_1_deliveryorder.Delivery_Type like '%"
                + aa + "%' or transport.Ac_Name_E like '%" + aa + "%' or dbo.nt_1_deliveryorder.MM_Rate like '%" + aa + "%') and";


        }
        name = name.Remove(name.Length - 3);

        string query = "";
        if (Trantype == "AI")
        {
            //   query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,doc_no,doc_dateConverted as doc_date,"
            //+ " purc_no,purc_order,millshortname,quantal,billtoshortname,salebillcityname,shiptoshortname,shiptocityname,sale_rate,Tender_Commission,desp_type ,truck_no,SB_No,EWay_Bill_No,Delivery_Type,transportshortname,MM_Rate,doid FROM qryDoutility   where " + name + " and Company_Code=" + Company_Code + " " +
            //" and Year_Code=" + year + " order by doc_no desc";

            //query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,  dbo.nt_1_deliveryorder.doc_no, CONVERT(varchar(10), dbo.nt_1_deliveryorder.doc_date, 103) AS doc_date, dbo.nt_1_deliveryorder.purc_no," +
            //          " dbo.nt_1_deliveryorder.tenderdetailid AS purc_order, mill.Short_Name AS millshortname, dbo.nt_1_deliveryorder.quantal, salebillto.Ac_Name_E AS billtoshortname, " +
            //          " salebillcity.city_name_e AS salebillcityname, shipto.Ac_Name_E AS shiptoshortname, shiptocity.city_name_e AS shiptocityname, dbo.nt_1_deliveryorder.sale_rate, " +
            //          " dbo.nt_1_deliveryorder.Tender_Commission, dbo.nt_1_deliveryorder.tran_type AS desp_type, dbo.nt_1_deliveryorder.truck_no, dbo.nt_1_deliveryorder.SB_No, " +
            //          "dbo.nt_1_deliveryorder.EWay_Bill_No, dbo.nt_1_deliveryorder.Delivery_Type, transport.Ac_Name_E AS transportshortname, dbo.nt_1_deliveryorder.Memo_Advance AS MM_Rate, " +
            //         " dbo.nt_1_deliveryorder.doid" +
            //           " FROM dbo.nt_1_citymaster AS salebillcity RIGHT OUTER JOIN" +
            //         " dbo.nt_1_accountmaster AS salebillto ON salebillcity.cityid = salebillto.cityid  RIGHT OUTER JOIN " +
            //          " dbo.nt_1_deliveryorder ON salebillto.accoid = dbo.nt_1_deliveryorder.sb LEFT OUTER JOIN " +
            //          " dbo.nt_1_accountmaster AS transport ON dbo.nt_1_deliveryorder.tc = transport.accoid LEFT OUTER JOIN " +
            //          " dbo.nt_1_accountmaster AS shipto ON dbo.nt_1_deliveryorder.st = shipto.accoid LEFT OUTER JOIN " +
            //         " dbo.nt_1_accountmaster AS mill ON dbo.nt_1_deliveryorder.mc = mill.accoid LEFT OUTER JOIN " +
            //          " dbo.nt_1_citymaster AS shiptocity ON shipto.cityid = shiptocity.cityid  where " + name + " and dbo.nt_1_deliveryorder.company_code=" + Company_Code + " " +
            //          " and dbo.nt_1_deliveryorder.Year_Code=" + year + " order by  dbo.nt_1_deliveryorder.doc_no desc";

            query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,dbo.nt_1_deliveryorder.doc_no, CONVERT(varchar(10), dbo.nt_1_deliveryorder.doc_date, 103) AS doc_date, " +
                     "dbo.nt_1_deliveryorder.purc_no, dbo.nt_1_deliveryorder.tenderdetailid AS purc_order, mill.Short_Name AS millshortname, dbo.nt_1_deliveryorder.quantal, " +
                     "salebillto.Ac_Name_E AS billtoshortname, salebillcity.city_name_e AS salebillcityname, shiptocity.city_name_e AS shiptocityname, dbo.nt_1_deliveryorder.sale_rate, " +
                     "dbo.nt_1_deliveryorder.Tender_Commission, dbo.nt_1_deliveryorder.tran_type AS desp_type, dbo.nt_1_deliveryorder.truck_no, dbo.nt_1_deliveryorder.SB_No, " +
                     "dbo.nt_1_deliveryorder.EWay_Bill_No, dbo.nt_1_deliveryorder.Delivery_Type, dbo.nt_1_deliveryorder.doid, shipto.Short_Name AS shiptoshortname, " +
                     "transport.Short_Name AS transportshortname, dbo.nt_1_deliveryorder.mill_rate, dbo.nt_1_deliveryorder.MM_Rate, dbo.nt_1_deliveryorder.vasuli_rate " +
                     "FROM         dbo.nt_1_citymaster AS salebillcity RIGHT OUTER JOIN " +
                     "dbo.nt_1_accountmaster AS salebillto ON salebillcity.cityid = salebillto.cityid RIGHT OUTER JOIN " +
                     "dbo.nt_1_deliveryorder ON salebillto.accoid = dbo.nt_1_deliveryorder.sb LEFT OUTER JOIN " +
                     "dbo.nt_1_accountmaster AS transport ON dbo.nt_1_deliveryorder.tc = transport.accoid LEFT OUTER JOIN " +
                     "dbo.nt_1_accountmaster AS shipto ON dbo.nt_1_deliveryorder.st = shipto.accoid LEFT OUTER JOIN " +
                     "dbo.nt_1_accountmaster AS mill ON dbo.nt_1_deliveryorder.mc = mill.accoid LEFT OUTER JOIN " +
                     "dbo.nt_1_citymaster AS shiptocity ON shipto.cityid = shiptocity.cityid where " + name + " and dbo.nt_1_deliveryorder.company_code=" + Company_Code + " " +
                     " and dbo.nt_1_deliveryorder.Year_Code=" + year + " order by  dbo.nt_1_deliveryorder.doc_no desc";
        }
        else
        {
            if (Trantype != "DO")
            {
                if (Trantype != "DD")
                {
                    if (Trantype1 != "AI")
                    {
                        //   query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,doc_no,doc_dateConverted as doc_date,"
                        //+ " purc_no,purc_order,millshortname,quantal,billtoshortname,salebillcityname,shiptoshortname,shiptocityname,sale_rate,Tender_Commission,desp_type ,truck_no,SB_No,EWay_Bill_No,Delivery_Type,transportshortname,MM_Rate,doid FROM qryDoutility   where " + name + " and desp_type='" + Trantype + "' and Delivery_Type='" + Trantype1 + "' and Company_Code=" + Company_Code + " " +
                        //" and Year_Code=" + year + " order by doc_no desc";


                        query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,  dbo.nt_1_deliveryorder.doc_no, CONVERT(varchar(10), dbo.nt_1_deliveryorder.doc_date, 103) AS doc_date, dbo.nt_1_deliveryorder.purc_no," +
                        " dbo.nt_1_deliveryorder.tenderdetailid AS purc_order, mill.Short_Name AS millshortname, dbo.nt_1_deliveryorder.quantal, salebillto.Ac_Name_E AS billtoshortname, " +
                        " salebillcity.city_name_e AS salebillcityname, shipto.Ac_Name_E AS shiptoshortname, shiptocity.city_name_e AS shiptocityname, dbo.nt_1_deliveryorder.sale_rate, " +
                        " dbo.nt_1_deliveryorder.Tender_Commission, dbo.nt_1_deliveryorder.tran_type AS desp_type, dbo.nt_1_deliveryorder.truck_no, dbo.nt_1_deliveryorder.SB_No, " +
                        "dbo.nt_1_deliveryorder.EWay_Bill_No, dbo.nt_1_deliveryorder.Delivery_Type, transport.Ac_Name_E AS transportshortname, dbo.nt_1_deliveryorder.Memo_Advance AS MM_Rate, " +
                       " dbo.nt_1_deliveryorder.doid" +
                         " FROM dbo.nt_1_citymaster AS salebillcity RIGHT OUTER JOIN" +
                       " dbo.nt_1_accountmaster AS salebillto ON salebillcity.cityid = salebillto.cityid  RIGHT OUTER JOIN " +
                        " dbo.nt_1_deliveryorder ON salebillto.accoid = dbo.nt_1_deliveryorder.sb LEFT OUTER JOIN " +
                        " dbo.nt_1_accountmaster AS transport ON dbo.nt_1_deliveryorder.tc = transport.accoid LEFT OUTER JOIN " +
                        " dbo.nt_1_accountmaster AS shipto ON dbo.nt_1_deliveryorder.st = shipto.accoid LEFT OUTER JOIN " +
                       " dbo.nt_1_accountmaster AS mill ON dbo.nt_1_deliveryorder.mc = mill.accoid LEFT OUTER JOIN " +
                        " dbo.nt_1_citymaster AS shiptocity ON shipto.cityid = shiptocity.cityid  where " + name + " and dbo.nt_1_deliveryorder.desp_type='" + Trantype + "' and dbo.nt_1_deliveryorder.Delivery_Type='" + Trantype1 + "' and dbo.nt_1_deliveryorder.company_code=" + Company_Code + " " +
                        " and dbo.nt_1_deliveryorder.Year_Code=" + year + " order by  dbo.nt_1_deliveryorder.doc_no desc";


                    }


                    else
                    {
                        //   query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,doc_no,doc_dateConverted as doc_date,"
                        //+ " purc_no,purc_order,millshortname,quantal,billtoshortname,salebillcityname,shiptoshortname,shiptocityname,sale_rate,Tender_Commission,desp_type ,truck_no,SB_No,EWay_Bill_No,Delivery_Type,transportshortname,MM_Rate,doid FROM qryDoutility   where " + name + " and desp_type='" + Trantype + "' and Company_Code=" + Company_Code + " " +
                        //" and Year_Code=" + year + " order by doc_no desc";


                        query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,  dbo.nt_1_deliveryorder.doc_no, CONVERT(varchar(10), dbo.nt_1_deliveryorder.doc_date, 103) AS doc_date, dbo.nt_1_deliveryorder.purc_no," +
                      " dbo.nt_1_deliveryorder.tenderdetailid AS purc_order, mill.Short_Name AS millshortname, dbo.nt_1_deliveryorder.quantal, salebillto.Ac_Name_E AS billtoshortname, " +
                      " salebillcity.city_name_e AS salebillcityname, shipto.Ac_Name_E AS shiptoshortname, shiptocity.city_name_e AS shiptocityname, dbo.nt_1_deliveryorder.sale_rate, " +
                      " dbo.nt_1_deliveryorder.Tender_Commission, dbo.nt_1_deliveryorder.tran_type AS desp_type, dbo.nt_1_deliveryorder.truck_no, dbo.nt_1_deliveryorder.SB_No, " +
                      "dbo.nt_1_deliveryorder.EWay_Bill_No, dbo.nt_1_deliveryorder.Delivery_Type, transport.Ac_Name_E AS transportshortname, dbo.nt_1_deliveryorder.Memo_Advance AS MM_Rate, " +
                     " dbo.nt_1_deliveryorder.doid" +
                       " FROM dbo.nt_1_citymaster AS salebillcity RIGHT OUTER JOIN" +
                     " dbo.nt_1_accountmaster AS salebillto ON salebillcity.cityid = salebillto.cityid  RIGHT OUTER JOIN " +
                      " dbo.nt_1_deliveryorder ON salebillto.accoid = dbo.nt_1_deliveryorder.sb LEFT OUTER JOIN " +
                      " dbo.nt_1_accountmaster AS transport ON dbo.nt_1_deliveryorder.tc = transport.accoid LEFT OUTER JOIN " +
                      " dbo.nt_1_accountmaster AS shipto ON dbo.nt_1_deliveryorder.st = shipto.accoid LEFT OUTER JOIN " +
                     " dbo.nt_1_accountmaster AS mill ON dbo.nt_1_deliveryorder.mc = mill.accoid LEFT OUTER JOIN " +
                      " dbo.nt_1_citymaster AS shiptocity ON shipto.cityid = shiptocity.cityid  where " + name + " and desp_type='" + Trantype + "'and dbo.nt_1_deliveryorder.company_code=" + Company_Code + " " +
                      " and dbo.nt_1_deliveryorder.Year_Code=" + year + " order by  dbo.nt_1_deliveryorder.doc_no desc";



                    }
                }
                else
                {
                    query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,  dbo.nt_1_deliveryorder.doc_no, CONVERT(varchar(10), dbo.nt_1_deliveryorder.doc_date, 103) AS doc_date, dbo.nt_1_deliveryorder.purc_no," +
                    " dbo.nt_1_deliveryorder.tenderdetailid AS purc_order, mill.Short_Name AS millshortname, dbo.nt_1_deliveryorder.quantal, salebillto.Ac_Name_E AS billtoshortname, " +
                    " salebillcity.city_name_e AS salebillcityname, shipto.Ac_Name_E AS shiptoshortname, shiptocity.city_name_e AS shiptocityname, dbo.nt_1_deliveryorder.sale_rate, " +
                    " dbo.nt_1_deliveryorder.Tender_Commission, dbo.nt_1_deliveryorder.tran_type AS desp_type, dbo.nt_1_deliveryorder.truck_no, dbo.nt_1_deliveryorder.SB_No, " +
                    "dbo.nt_1_deliveryorder.EWay_Bill_No, dbo.nt_1_deliveryorder.Delivery_Type, transport.Ac_Name_E AS transportshortname, dbo.nt_1_deliveryorder.Memo_Advance AS MM_Rate, " +
                   " dbo.nt_1_deliveryorder.doid" +
                     " FROM dbo.nt_1_citymaster AS salebillcity RIGHT OUTER JOIN" +
                   " dbo.nt_1_accountmaster AS salebillto ON salebillcity.cityid = salebillto.cityid  RIGHT OUTER JOIN " +
                    " dbo.nt_1_deliveryorder ON salebillto.accoid = dbo.nt_1_deliveryorder.sb LEFT OUTER JOIN " +
                    " dbo.nt_1_accountmaster AS transport ON dbo.nt_1_deliveryorder.tc = transport.accoid LEFT OUTER JOIN " +
                    " dbo.nt_1_accountmaster AS shipto ON dbo.nt_1_deliveryorder.st = shipto.accoid LEFT OUTER JOIN " +
                   " dbo.nt_1_accountmaster AS mill ON dbo.nt_1_deliveryorder.mc = mill.accoid LEFT OUTER JOIN " +
                    " dbo.nt_1_citymaster AS shiptocity ON shipto.cityid = shiptocity.cityid  where " + name + " and dbo.nt_1_deliveryorder.desp_type='" + Trantype + "' and dbo.nt_1_deliveryorder.Delivery_Type='" + Trantype1 + "' and dbo.nt_1_deliveryorder.company_code=" + Company_Code + " " +
                    " and dbo.nt_1_deliveryorder.Year_Code=" + year + " and dbo.nt_1_deliveryorder.purc_no=0 order by  dbo.nt_1_deliveryorder.doc_no desc";

                }
            }
            else
            {
                //   query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,doc_no,doc_dateConverted as doc_date,"
                //+ " purc_no,purc_order,millshortname,quantal,billtoshortname,salebillcityname,shiptoshortname,shiptocityname,sale_rate,Tender_Commission,desp_type 
                // ,truck_no,SB_No,EWay_Bill_No,Delivery_Type,transportshortname,MM_Rate,doid FROM qryDoutility   
                //     where " + name + " and desp_type='" + Trantype + "' and Company_Code=" + Company_Code + " " +
                //" and Year_Code=" + year + " order by doc_no desc";


                query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,  dbo.nt_1_deliveryorder.doc_no, CONVERT(varchar(10), dbo.nt_1_deliveryorder.doc_date, 103) as doc_date, dbo.nt_1_deliveryorder.purc_no," +
               " dbo.nt_1_deliveryorder.tenderdetailid AS purc_order, mill.Short_Name AS millshortname, dbo.nt_1_deliveryorder.quantal, salebillto.Ac_Name_E AS billtoshortname, " +
               " salebillcity.city_name_e AS salebillcityname, shipto.Ac_Name_E AS shiptoshortname, shiptocity.city_name_e AS shiptocityname, dbo.nt_1_deliveryorder.sale_rate, " +
               " dbo.nt_1_deliveryorder.Tender_Commission, dbo.nt_1_deliveryorder.tran_type AS desp_type, dbo.nt_1_deliveryorder.truck_no, dbo.nt_1_deliveryorder.SB_No, " +
               "dbo.nt_1_deliveryorder.EWay_Bill_No, dbo.nt_1_deliveryorder.Delivery_Type, transport.Ac_Name_E AS transportshortname, dbo.nt_1_deliveryorder.Memo_Advance AS MM_Rate, " +
              " dbo.nt_1_deliveryorder.doid" +
                " FROM dbo.nt_1_citymaster AS salebillcity RIGHT OUTER JOIN" +
              " dbo.nt_1_accountmaster AS salebillto ON salebillcity.cityid = salebillto.cityid  RIGHT OUTER JOIN " +
               " dbo.nt_1_deliveryorder ON salebillto.accoid = dbo.nt_1_deliveryorder.sb LEFT OUTER JOIN " +
               " dbo.nt_1_accountmaster AS transport ON dbo.nt_1_deliveryorder.tc = transport.accoid LEFT OUTER JOIN " +
               " dbo.nt_1_accountmaster AS shipto ON dbo.nt_1_deliveryorder.st = shipto.accoid LEFT OUTER JOIN " +
              " dbo.nt_1_accountmaster AS mill ON dbo.nt_1_deliveryorder.mc = mill.accoid LEFT OUTER JOIN " +
               " dbo.nt_1_citymaster AS shiptocity ON shipto.cityid = shiptocity.cityid  where " + name + " and desp_type='" + Trantype +
               "'and dbo.nt_1_deliveryorder.company_code=" + Company_Code + " " +
               " and dbo.nt_1_deliveryorder.Year_Code=" + year + " order by  dbo.nt_1_deliveryorder.doc_no desc";

            }
        }
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


    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
    protected void txtDOC_DATE_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {

    }

}