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


public partial class Sugar_Master_PgeSaleHeadUtility : System.Web.UI.Page
{
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    string cs = string.Empty;
    string qryCommon = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        qryCommon = "qrysalehead";
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
        dummy.Columns.Add("billtoShortname");
        dummy.Columns.Add("billtogstno");
        dummy.Columns.Add("shiptoshortname");
        dummy.Columns.Add("NETQNTL");
        dummy.Columns.Add("Bill_Amount");
        dummy.Columns.Add("millshortname");
        dummy.Columns.Add("EWay_Bill_No");
        dummy.Columns.Add("ackno");
        dummy.Columns.Add("saleid");
        dummy.Columns.Add("IsDeleted");
        dummy.Columns.Add("DO_No");
        

        dummy.Rows.Add();
        gvCustomers.DataSource = dummy;
        gvCustomers.DataBind();
    }
    [WebMethod]
    public static string GetCustomers(string searchTerm, int pageIndex, int PageSize, int Company_Code, int year)
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
            name += "( billto.Short_Name like '%" + aa + "%' or shipto.Short_Name like '%" + aa +
                    "%' or millto.Short_Name like '%" + aa + "%' or dbo.nt_1_sugarsale.doc_no like '%" + aa +
                    "%' or dbo.nt_1_sugarsale.doc_date like '%" + aa + "%' or billto.Gst_No like '%" + aa + 
                    "%' or dbo.nt_1_sugarsale.NETQNTL like '%" + aa + "%' or dbo.nt_1_sugarsale.Bill_Amount like '%" + aa + "%' or dbo.nt_1_sugarsale.saleid like '%" + aa + "%' ) and";


        }
        name = name.Remove(name.Length - 3);
      
       // string query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,doc_no,doc_dateConverted as doc_date,"
       //+ "billtoShortname ,billtogstno,shiptoshortname,NETQNTL,Bill_Amount,millshortname,EWay_Bill_No,ackno,saleid,IsDeleted FROM qrysalehead   where " + name + " and Company_Code=" + Company_Code + " " +
       //" and Year_Code=" + year + " order by doc_no desc,doc_date desc";

        string query = " SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,dbo.nt_1_sugarsale.doc_no, CONVERT(varchar(10), dbo.nt_1_sugarsale.doc_date, 103) AS doc_date, " +
                       " billto.Short_Name AS billtoShortname,billto.Gst_No AS billtogstno, shipto.Short_Name AS shiptoshortname, dbo.nt_1_sugarsale.NETQNTL, dbo.nt_1_sugarsale.Bill_Amount, " +
                       " millto.Short_Name AS millshortname, dbo.nt_1_sugarsale.EWay_Bill_No, dbo.nt_1_sugarsale.ackno, dbo.nt_1_sugarsale.saleid, dbo.nt_1_sugarsale.IsDeleted, dbo.nt_1_sugarsale.DO_No " +
                       " FROM         dbo.nt_1_sugarsale LEFT OUTER JOIN " +
                       " dbo.nt_1_accountmaster AS shipto ON dbo.nt_1_sugarsale.uc = shipto.accoid LEFT OUTER JOIN " +
                       " dbo.nt_1_accountmaster AS millto ON dbo.nt_1_sugarsale.mc = millto.accoid LEFT OUTER JOIN " +
                       " dbo.nt_1_accountmaster AS billto ON dbo.nt_1_sugarsale.ac = billto.accoid " +
                       " where " + name + " and dbo.nt_1_sugarsale.Company_Code=" + Company_Code + " and dbo.nt_1_sugarsale.Year_Code=" + year + " order by doc_no desc,doc_date desc";

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
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string qry = string.Empty;
        qry = " select doc_no,doc_dateConverted as doc_date,"
       + "billtoShortname ,billtogstno,shiptoshortname,NETQNTL,Bill_Amount,millshortname,EWay_Bill_No,ackno,saleid,IsDeleted,DO_No FROM " + qryCommon + " limit 15";
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                gvCustomers.DataSource = dt;
                gvCustomers.DataBind();
                ViewState["currentTable"] = dt;
            }
        }
        else
        {
            gvCustomers.DataSource = null;
            gvCustomers.DataBind();
            ViewState["currentTable"] = null;
        }
    }
}