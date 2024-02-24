using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Receipt
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Receipt : System.Web.Services.WebService
{

   

    [WebMethod]
    public void Receiptpayment()
    {
        string cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        List<receiptPayment> payment = new List<receiptPayment>();
        using (SqlConnection con = new SqlConnection(cs))
        {
            string qry = "select Doc_No,cashbank,tran_type,doc_date,tranid  from nt_1_transacthead where Company_code=" + Session["Company_Code"].ToString() + " " +
              " and Year_Code=" + Session["Year"].ToString() + "  order by doc_no desc ";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                receiptPayment receipt = new receiptPayment();
                receipt.Doc_No = Convert.ToInt32(dt.Rows[i]["Doc_No"].ToString());
                receipt.cashbank = Convert.ToInt32(dt.Rows[i]["cashbank"].ToString());
                receipt.tran_type = dt.Rows[i]["tran_type"].ToString();
                receipt.tranid = Convert.ToInt32(dt.Rows[i]["tranid"].ToString());
                receipt.date = dt.Rows[i]["tran_type"].ToString();
                payment.Add(receipt);
            }
        }
    }

    public class receiptPayment
    {
        public int Doc_No { get; set; }
        public int cashbank { get; set; }
        public string tran_type { get; set; }
        public int tranid { get; set; }
        public string date { get; set; }
    }
}
