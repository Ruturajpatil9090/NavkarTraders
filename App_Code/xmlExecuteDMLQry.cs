using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;

/// <summary>
/// Summary description for xmlExecuteDMLQry
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class xmlExecuteDMLQry : System.Web.Services.WebService
{

    public xmlExecuteDMLQry()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string ExecuteXMLQry(string XML, string status, string spname)
    {
        string msgReturn = "";
        try
        {
            //msgReturn = XML.Length.ToString();
            //   XML.Replace("''", """");
            int pos = 0;
            msgReturn = GetWordAtPosition(XML, pos);
            XDocument XDoc = XDocument.Parse(XML, LoadOptions.None);
            XML = XDoc.ToString(SaveOptions.DisableFormatting);
            //SqlCommand cmd1 = new SqlCommand();

            //cmd1.CommandText = spname;
            //cmd1.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            //string spname = "DeliveryOrder";
            string xmlfile = XML;
            string op = "";
            string returnmaxno = "";
            int flag;

            if (status == "Update")
            {
                flag = 2;
            }
            else if (status == "Save")
            {
                flag = 1;
            }
            else if (status == "Transfer")
            {
                flag = 6;
            }
            else
            {
                flag = 3;
            }
            ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);

            if (flag != 3)
            {
                return returnmaxno;
            }
            else
            {
                return op;
            }
        }
        catch (Exception exx)
        {

            return "";
        }
    }

    public static string GetWordAtPosition(string text, int position)
    {
        if (text.Length - 1 < position || text[position] == ' ') return null;

        int start = position;
        int end = position;
        while (text[start] != ' ' && start > 0) start--;
        while (text[end] != ' ' && end < text.Length - 1) end++;

        return text.Substring(start == 0 ? 0 : start + 1, end - start - 1);

    }

    [WebMethod]
    public string ExecuteXMLQryDO(string XML, string status, string spname, string Type)
    {
        string msgReturn = "";
        try
        {
            //msgReturn = XML.Length.ToString();
            //   XML.Replace("''", """");
            int pos = 6473;
            msgReturn = GetWordAtPosition(XML, pos);
            XDocument XDoc = XDocument.Parse(XML, LoadOptions.None);
            XML = XDoc.ToString(SaveOptions.DisableFormatting);
            //SqlCommand cmd1 = new SqlCommand();

            //cmd1.CommandText = spname;
            //cmd1.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            //string spname = "DeliveryOrder";
            string xmlfile = XML;
            string op = "";
            string returnmaxno = "";
            int flag;
            if (Type == "DI")
            {
                if (status == "Update")
                {
                    flag = 2;
                }
                else if (status == "Save")
                {
                    flag = 1;
                }
                else
                {
                    flag = 10;
                }
            }
            else
            {
                if (status == "Update")
                {
                    flag = 4;
                }
                else if (status == "Save")
                {
                    flag = 3;
                }
                else
                {
                    flag = 10;
                }
            }
            ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);

            return returnmaxno;
        }
        catch (Exception exx)
        {
           // DOPurcSaleCRUD.LogError(exx);
            //clsNoToWord n = new clsNoToWord();
            //n.WriteToFile(exx.ToString());
            return "";
        }
    }

    public static string GenrateEWaybill(string urlAddress1, string username, string password, string gstin, string requestid, string token, string json)
    {
        string jsonData = string.Empty;
        System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(urlAddress1);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.Headers.Add("username", username);
        request.Headers.Add("password", password);
        request.Headers.Add("gstin", gstin);
        request.Headers.Add("requestid", requestid);
        request.Headers.Add("Authorization", token);


        try
        {
            //Place the serialized content of the object to be posted into the request stream
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {


                string asb = json;
                streamWriter.Write(asb);
                streamWriter.Flush();
                streamWriter.Close();
            }



            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                readStream = new StreamReader(receiveStream);
                jsonData = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                var jsonObject = JObject.Parse(jsonData);
            }
        }
        catch (Exception ex)
        {
            return jsonData = "Issue occured, " + ex.Message;
        }
        return jsonData;
    }
    public static string GetAuthToken(string urlAddress, string username, string password)
    {
        string jsonData = string.Empty;
        string authtoken = string.Empty;
        System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(urlAddress);

        request.Method = "POST";
        request.ContentType = "application/json";
        request.Headers.Add("gspappid", username);//Client Id
        request.Headers.Add("gspappsecret", password);//Client Secret
        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            string json = "{\"gspappid\":\"" + username + "\"," +
              "\"gspappsecret\":\"" + password + "\"}";
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        try
        {
            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                jsonData = streamReader.ReadToEnd();
            }
            var jsonObject = JObject.Parse(jsonData);
            jsonData = (string)jsonObject.SelectToken("access_token");
            authtoken = "Bearer " + jsonData;
        }
        catch (Exception ex)
        {
            return jsonData = "Issue occured, " + ex.Message;
        }

        return authtoken;
    }

    public static string GenrateEInvoice(string urlAddress1, string username, string password, string gstin, string requestid, string token, string json)
    {
        string jsonData = string.Empty;
        System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(urlAddress1);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.Headers.Add("user_name", username);
        request.Headers.Add("password", password);
        request.Headers.Add("gstin", gstin);
        request.Headers.Add("requestid", requestid);
        request.Headers.Add("Authorization", token);


        try
        {
            //Place the serialized content of the object to be posted into the request stream
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
               


                string asb = json;
                streamWriter.Write(asb);
                streamWriter.Flush();
                streamWriter.Close();
            }



            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                readStream = new StreamReader(receiveStream);
                jsonData = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                var jsonObject = JObject.Parse(jsonData);
            }
        }
        catch (Exception ex)
        {
            return jsonData = "Issue occured, " + ex.Message;
        }
        return jsonData;
    }

    public static string ExecuteXMLQryJV(string XML, string status, string spname)
    {
        string msgReturn = "";
        try
        {
            //msgReturn = XML.Length.ToString();
            //   XML.Replace("''", """");
            int pos = 0;
            msgReturn = GetWordAtPosition(XML, pos);
            XDocument XDoc = XDocument.Parse(XML, LoadOptions.None);
            XML = XDoc.ToString(SaveOptions.DisableFormatting);
            //SqlCommand cmd1 = new SqlCommand();

            //cmd1.CommandText = spname;
            //cmd1.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            //string spname = "DeliveryOrder";
            string xmlfile = XML;
            string op = "";
            string returnmaxno = "";
            int flag;

            if (status == "Update")
            {
                flag = 2;
            }
            else if (status == "Save")
            {
                flag = 1;
            }
            else if (status == "Transfer")
            {
                flag = 6;
            }
            else
            {
                flag = 3;
            }
            ds = clsDAL.xmlExecuteDMLQry(spname, xmlfile, ref op, flag, ref returnmaxno);

            if (flag != 3)
            {
                return returnmaxno;
            }
            else
            {
                return op;
            }
        }
        catch (Exception exx)
        {

            return "";
        }
    }
}
