using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.NetworkInformation;
using Org.BouncyCastle.Asn1.Ocsp;

/// <summary>
/// Summary description for clsMAC
/// </summary>
public class clsMAC
{
    public clsMAC()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string GetMACAddress()
    {
        try
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public string HostName()
    {
        try
        {
            string Computer_User = System.Environment.MachineName;
            return Computer_User;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public string IPAddress()
    {
        try
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }
        catch (Exception)
        {

            throw;
        }
    }
}