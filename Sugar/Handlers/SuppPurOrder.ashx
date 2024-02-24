<%@ WebHandler Language="C#" Class="SuppPurOrder" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;


public class SuppPurOrder : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        if (context.Request.QueryString["Doc_No"] != null && context.Request.QueryString["Doc_No"].ToString() != string.Empty)
        {
            if (context.Request.QueryString["Company_Code"] != null && context.Request.QueryString["Company_Code"].ToString() != string.Empty)
            {
                if (context.Request.QueryString["Year_Code"] != null && context.Request.QueryString["Year_Code"].ToString() != string.Empty)
                {
                    string Doc_No = context.Request.QueryString["Doc_No"].ToString();
                    string Company_Code = context.Request.QueryString["Company_Code"].ToString();
                    string Year_Code = context.Request.QueryString["Year_Code"].ToString();
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