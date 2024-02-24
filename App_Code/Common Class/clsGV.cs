using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// Summary description for clsGV
/// </summary>
public class clsGV
{
    public clsGV()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region public variables
    public static Int32 Company_Code = 0;

    public static Int32 Year_Code = 0;
    public static Int32 Branch_Code = 0;
    public static string user = "";
    public static Int32 User_Id = 0;
    public static string User_Type = "";
    public static string EmailId = "";
    public static string EmailPassword = "";
    public static string smtpServerPort = "";
    public static string Start_Date = "";
    public static string start_date_op = "";
    public static string mystatename = "";
    public static string End_Date = "";
    public static string To_date = "";
    public static string CompanyName = "";
    public static string CompanyAddress = "";
    public static string CompanyPhone = "";
    public static string userInfo = "";
    public static string CompanyOurGSTNo = "";
    public static string BROKRAGE_AC = "";
    public static string SERVICE_CHARGE_AC = "";
    public static string COMMISSION_AC = "";
    public static string QUALITY_DIFF_AC = "";
    public static string BANK_COMMISSION_AC = "";
    public static string INTEREST_AC = "";
    public static string TRANSPORT_AC = "";
    public static string SALE_DALALI_AC = "";
    public static string LOADING_CHARGE_AC = "";
    public static string MOTOR_FREIGHT_AC = "";
    public static string POSTAGE_AC = "";   

    public static string OTHER_AMOUNT_AC = "";
    public static string SELF_AC = "";
    public static string AUTO_VOUCHER = "";
    public static string EXCISE_RATE = "";
    public static int GautamID = 53;
    public static int VinitID = 83;
    public static string Email_Address = "smtp.gmail.com";
    public static string Website = "";

    //API Varible
   // public static string msgAPI = "http://sms-latasoftware.com/api/sms.php?uid=7368616d&pin=4f2f7e89a673b&sender=BHVANI&route=5&tempid=1&mobile=";
    

    ////public static string msgAPI = "http://msg-sanhitainfotech.in/submitsms.jsp?user=shamind&key=d8e52770edXX&";
  //  public static string msgAPI = "http://sms-latasoftware.com/api/sms.php?uid=6e696b6574646f736869&pin=518cd695b18f0&sender=RBCKOP&route=5&tempid=1&mobile=";

    public static string msgAPI = "http://msg-sanhitainfotech.in/submitsms.jsp?user=latasoft&key=28237f1b71XX&";
   
 
    //printing css neccessary for printing
    public static string printcss = "<style type=?text/css? media=?print?>body{visibility: hidden;}.print{visibility: visible;width: 100%;height: auto;margin: 0;float: none;font-size: 11pt;}.print9pt{visibility: visible;width: 100%;height: auto;margin: 0;float: none;font-size: 9pt;}.noprint9pt{visibility: hidden;width: 100%;height: auto;margin: 0;float: none;font-size: 9pt;}.print7pt{visibility: visible;width: 100%;height: auto;margin: 0;float: none;font-size: 7pt;}.print2{visibility: visible;width: 100%;margin: 0;height: auto;" +
        " font-size: 9pt;display: table;float: none;overflow: scroll;position: static; }.print8pt{visibility: visible;width: 100%;margin: 0;height: auto;font-size: 8pt;display: table;float: none;overflow: scroll;position: static;}.print3{visibility: visible;width: 100%;height: auto;margin: 0;font-size: 9pt;}" +
        ".noprint{visibility: hidden;height: auto;margin: 0;float: none;font-size: 11pt;}.noprint2{visibility: hidden;width: 100%;height: auto;margin: 0;float: none;font-size: 11pt;}.printsmall{visibility: visible;width: 100%;height: auto;margin: 0;float: none;font-size: 9pt;}.small{font-size: 10pt;}.noprint7pt{visibility: hidden;width: 100%;height: auto;margin: 0;float: none;font-size: 7pt;}" +
        ".toosmall{visibility: visible;width: 100%;height: auto;margin: 0;float: none;font-size: 7pt;empty-cells: show;}.noprinttoosmall{visibility: hidden;width: 100%;height: auto;margin: 0;float: none;font-size: 7pt;empty-cells: show;}" +
        ".medium{visibility: visible;width: 100%;height: auto;margin: 0;float: none;font-size: 9pt;}.largsize{visibility: visible;width: 100%;height: auto;margin: none;float: none;font-size: 8pt;display: table;}thead{display: table-header-group;}.printwithmargin{visibility: visible;width: 100%;height: auto;margin: 0 0 0 13px;float: none;font-size: 11pt;overflow-y: visible;}" +
        ".printhalfno{visibility: hidden;width: 100%;height: auto;margin: 0;float: none;font-size: 9pt;}.printhalf{visibility: visible;width: 100%;height: auto;margin: 0;float: none;font-size: 8pt;}.divprint{display: inline;}.lbl{visibility: hidden;height: 1px;}</style>";

    #endregion
}