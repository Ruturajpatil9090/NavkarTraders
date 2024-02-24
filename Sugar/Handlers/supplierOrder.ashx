<%@ WebHandler Language="C#" Class="supplierOrder" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Data.SqlClient;


public class supplierOrder : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (context.Request.QueryString["Doc_No"] != null && context.Request.QueryString["Doc_No"].ToString() != string.Empty)
        {
            if (context.Request.QueryString["Company_Code"] != null && context.Request.QueryString["Company_Code"].ToString() != string.Empty)
            {
                if (context.Request.QueryString["Year_Code"] != null && context.Request.QueryString["Year_Code"].ToString() != string.Empty)
                {
                    string Doc_No = context.Request.QueryString["Doc_No"].ToString();
                    string Company_Code = context.Request.QueryString["Company_Code"].ToString();
                    string Year_Code = context.Request.QueryString["Year_Code"].ToString();
                    using (clsDataProvider objDataProvider = new clsDataProvider())
                    {
                        SqlParameter[] param = new SqlParameter[4];
                        param[0] = new SqlParameter("@Doc_No", Doc_No);
                        param[1] = new SqlParameter("@Company_Code", Company_Code);
                        param[2] = new SqlParameter("@Year_Code", Year_Code);
                        DataTable dtDoDetails = objDataProvider.CallStoredProc("SP_SupplierOrdersms", param);
                        if (dtDoDetails.Rows.Count > 0)
                        {
                            dtDoDetails.Columns.Add((new DataColumn("Partname", typeof(string))));
                            string partname = "";
                            DataRow dr = null;
                            for (int i = 0; i < dtDoDetails.Rows.Count; i++)
                            {
                                partname += dtDoDetails.Rows[i]["Part_Name_E"].ToString() + " " +
                                    " Quantity: " + dtDoDetails.Rows[i]["Qty"].ToString() + " " +
                                     " Rate: " + dtDoDetails.Rows[i]["Rate"].ToString() + " ";
                            }
                            dtDoDetails.Rows[0]["Partname"] = partname;
                            //  dr = dtDoDetails.NewRow();
                            // dr["Partname"] = partname;
                        }
                        if (dtDoDetails.Rows.Count > 0)
                        {
                            DataRow dr = dtDoDetails.Rows[0];
                            string data = JsonConvert.SerializeObject(dtDoDetails);
                            context.Response.Write(data);
                        }
                        else
                        {
                            context.Response.Write("0");
                        }
                    }
                }
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}