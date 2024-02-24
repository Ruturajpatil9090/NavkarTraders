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

public partial class Sugar_Master_PgeLocalVoucherUtility : System.Web.UI.Page
{
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    string cs = string.Empty;
    string qryCommon = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        qryCommon = "qrycommissionbill";
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
        dummy.Columns.Add("Ac_Name_E");
        dummy.Columns.Add("item_code");
        dummy.Columns.Add("millname");
        dummy.Columns.Add("qntl");
        dummy.Columns.Add("Rate");
        dummy.Columns.Add("commission_amount");
        dummy.Columns.Add("texable_amount");
        dummy.Columns.Add("bill_amount");
        dummy.Columns.Add("TDS_Per");
        dummy.Columns.Add("ackno");
        dummy.Columns.Add("commissionid");
        dummy.Columns.Add("Tran_Type");
        dummy.Columns.Add("link_no");

        dummy.Rows.Add();
        gvCustomers.DataSource = dummy;
        gvCustomers.DataBind();
    }


    [WebMethod]
    public static string GetCustomers(string searchTerm, int pageIndex, string Trantype, int PageSize, int Company_Code, int year)
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
            name += "( Ac_Name_E like '%" + aa + "%' or millname like '%" + aa + "%' or doc_no like '%" + aa + "%' or doc_dateConverted like '%" + aa + "%' or qntl like '%" + aa + "%' or texable_amount like '%" + aa + "%' or bill_amount like '%" + aa + "%' or commissionid like '%" + aa + "%' or Tran_Type like '%" + aa
                + "%' or link_no like '%" + aa + "%' or item_code like '%" + aa + "%'or TDS_Per like '%" + aa + "%'or ackno like '%" + aa + "%' ) and";


        }
        name = name.Remove(name.Length - 3);

        string query = "SELECT ROW_NUMBER() OVER ( order by doc_no ASC) AS RowNumber,doc_no,doc_dateConverted as doc_date,"
       + "Ac_Name_E ,item_code,millname,qntl,Rate,commission_amount,texable_amount,bill_amount,TDS_Per,ackno,commissionid,Tran_Type,link_no FROM qrycommissionbill   where " + name + " and tran_type='" + Trantype + "' and Company_Code=" + Company_Code + " " +
       " and Year_Code=" + year + " order by doc_no desc,doc_date desc ";

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
       + "Ac_Name_E ,item_code, millname,qntl,Rate,commission_amount,texable_amount,bill_amount,TDS_Per,ackno,commissionid,Tran_Type,link_no FROM " + qryCommon + " limit 15";
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