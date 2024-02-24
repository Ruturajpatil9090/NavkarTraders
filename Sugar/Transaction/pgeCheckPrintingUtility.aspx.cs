using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Services;

public partial class Foundman_Transaction_pgeCheckPrintingUtility : System.Web.UI.Page
{
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlTransaction myTran = null;
    string cs = string.Empty;
    string qryCommon = string.Empty;
    int PageSize;

    protected void Page_Load(object sender, EventArgs e)
    {
        qryCommon = "qryCheckPrinting";
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
        dummy.Columns.Add("Doc_No");
        dummy.Columns.Add("Doc_Date");
        dummy.Columns.Add("Cash_BankName");
        dummy.Columns.Add("Ac_Code_Name");
        dummy.Columns.Add("ChqCaption");
        dummy.Columns.Add("Amount");
        dummy.Columns.Add("Company_Code");
        dummy.Columns.Add("Year_Code");
        dummy.Columns.Add("Check_Id");


        dummy.Rows.Add();
        gvCustomers.DataSource = dummy;
        gvCustomers.DataBind();


    }

    [WebMethod]
    public static string GetCustomers(string searchTerm, int pageIndex,int PageSize, int Company_Code, int year)
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

            name += "( Doc_No like '%" + aa + "%' or Ac_Code_Name like '%" + aa + "%' or ChqCaption like '%" + aa + "%' or Amount like '%" + aa + "%') and";

        }
        name = name.Remove(name.Length - 3);

        string qry = " select distinct ROW_NUMBER() OVER ( order by  Doc_No) AS RowNumber,Doc_No,CONVERT(varchar(10), Doc_Date, 103) AS Doc_Date,Cash_BankName,Ac_Code_Name,ChqCaption,Amount,Company_Code,Year_Code,Check_Id from qryCheckPrinting "
            + " where Company_Code=" + Convert.ToInt32(Company_Code)
            + " and Year_code=" + year + " and (" + name + ") order by Doc_No desc";


        SqlCommand cmd = new SqlCommand(qry);
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

                    DataRow[] results = dtnew.Select(f1 + f2, "Doc_No desc");
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
        // DataSet ds = new DataSet();
        // DataTable dt = new DataTable();
        // string qry = string.Empty;
        // qry = " select Ac_Code,Ac_Name_E,"
        //+ "Ac_City_E ,Ac_GST_No,Ac_Address_E FROM " + qryCommon + " limit 15";
        // ds = clsDAL.SimpleQuery(qry);
        // if (ds != null)
        // {
        //     dt = ds.Tables[0];
        //     if (dt.Rows.Count > 0)
        //     {
        //         gvCustomers.DataSource = dt;
        //         gvCustomers.DataBind();
        //         ViewState["currentTable"] = dt;
        //     }
        // }
        // else
        // {
        //     gvCustomers.DataSource = null;
        //     gvCustomers.DataBind();
        //     ViewState["currentTable"] = null;
        // }


    }
}