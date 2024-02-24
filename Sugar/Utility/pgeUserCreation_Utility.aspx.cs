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
using System.Globalization;

public partial class Sugar_Utility_pgeUserCreation_Utility : System.Web.UI.Page
{
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    string cs = string.Empty;
    string tblHead = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblHead = "tbluser";
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
        dummy.Columns.Add("User_Id");
        dummy.Columns.Add("userfullname");
        dummy.Columns.Add("User_Type");
        dummy.Columns.Add("EmailId");
        dummy.Columns.Add("Mobile");
        dummy.Columns.Add("uid");

        dummy.Rows.Add();
        gvCustomers.DataSource = dummy;
        gvCustomers.DataBind();
    }
    [WebMethod]
    public static string GetCustomers(string searchTerm, int pageIndex, int PageSize, int Company_Code)
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
            name += "( User_Id like '%" + aa + "%' or userfullname like '%" + aa + "%' or User_Type like '%" + aa + "%' or EmailId like '%" + aa + "%' or Mobile like '%" + aa + "%' or uid like '%" + aa + "%') and";


        }
        name = name.Remove(name.Length - 3);
        //string query = "SELECT ROW_NUMBER() OVER ( order by DOC_NO ASC) AS RowNumber,DOC_NO as CustomerID,Ac_Name_E as ContactName,"
        //+ "AMOUNT ,TRAN_TYPE as City FROM qryGledgernew where " + name + "";

        string query = "SELECT ROW_NUMBER() OVER ( order by User_Id ASC) AS RowNumber,User_Id,userfullname,"
       + "User_Type ,EmailId,Mobile,uid FROM tbluser   where Company_Code=" + Company_Code + " and " + name + " order by User_Id desc ";

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

                    DataRow[] results = ds.Tables[0].Select(f1 + f2, "User_Id desc");
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
        qry = " select User_Id,userfullname,"
       + "User_Type ,EmailId,Mobile,uid FROM " + tblHead + " limit 15";
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