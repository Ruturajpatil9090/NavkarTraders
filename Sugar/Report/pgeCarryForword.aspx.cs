using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;

public partial class Sugar_Report_pgeCarryForword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btncarryforword_Click(object sender, EventArgs e)
    {
        try
        {
            string fromdate = Session["Start_Date"].ToString();
            fromdate = DateTime.Parse(fromdate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            DateTime dToday = Convert.ToDateTime(fromdate);

            string dtdate = "";

            DateTime Post_Date = dToday + TimeSpan.FromDays(-1);

            dtdate = Post_Date.ToString("yyyy-MM-dd");

            string qry1 = " delete from stock where Company_Code="
               + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Tran_Type='OP'";
            DataSet ds1 = clsDAL.SimpleQuery(qry1);


            string qry = "select 'OP' as Tran_Type,'OP' as Cash_Credit, '" + dtdate + "' as Doc_Date ,Item_Code,Brand_Code as Brand_Code,Wt_Per as Wt_per,'D' as DRCR,Year_Code,balance as qty,Company_Code from qrygrainstockbalance where Company_Code="
                + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + (Convert.ToInt32(Session["year"].ToString()) - 1 + " and balance!=0 ");
            DataSet ds = clsDAL.SimpleQuery(qry);
            //  DataSet ds = GetData();
            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            SqlBulkCopy bulk = new SqlBulkCopy(con);
            bulk.DestinationTableName = "stock";
            foreach (DataColumn col in ds.Tables[0].Columns)
                bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);
            con.Open();
            bulk.WriteToServer(ds.Tables[0]);
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Sucessfully Inserted!')", true);
           // Response.Write('<script>window.close();</script>');
            // btncarryforword.Attributes.Add'OnClick', 'window.close();');
            //ScriptManager.RegisterClientScriptBlock(this,Page.GetType(), "script", "window.close();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>self.close();</script>");

        }
        catch (Exception ex)
        {
            throw (ex);
        }

    }
}