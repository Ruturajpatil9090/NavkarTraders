using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for sendsms
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class sendsms : System.Web.Services.WebService
{

    public sendsms()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int SendSMS(string msg, string mobile, string msgAPI, string senderid, string accusage)
    {
        //string msgAPI = clsCommon.getString("select smsApi from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //string senderid = clsCommon.getString("select Sender_id from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
        //string accusage = clsCommon.getString("select Accusage from eway_bill where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));

       // string msgAPI = clsGV.msgAPI;
        //string URL = msgAPI + "mobile=" + mobile + "&message=" + msg + "&senderid=NAVKAR&accusage=1";
        string URL = msgAPI + "mobile=" + mobile + "&message=" + msg + "&senderid=" + senderid + "&accusage=" + accusage + "";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
        HttpWebResponse response = (HttpWebResponse)req.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string read = reader.ReadToEnd();
        reader.Close();
        response.Close();
        return 1;
    }
}
