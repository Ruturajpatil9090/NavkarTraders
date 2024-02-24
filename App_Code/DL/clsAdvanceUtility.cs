using System;
using System.Data;
using System.Configuration;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Globalization;
using System.Net;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
//using System.Web.Script.Serialization;
//using System.IO;
//using System.Xml;
using System.Collections.Specialized;


/// <summary>
/// Summary description for clsUtility
/// </summary>
public static class clsAdvanceUtility
{
    public static string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    public static string UppercaseWords(string value)
    {
        char[] array = value.ToCharArray();
        // Handle the first letter in the string.
        if (array.Length >= 1)
        {
            if (char.IsLower(array[0]))
            {
                array[0] = char.ToUpper(array[0]);
            }
        }
        // Scan through the letters, checking for spaces.
        // ... Uppercase the lowercase letters following spaces.
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1] == ' ')
            {
                if (char.IsLower(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }
        }
        return new string(array);
    }

    public static string GenerateClientPassword()
    {
        try
        {
            Random rng = new Random();
            char[] valid = new char[62];
            valid = "a3456bcdelmnopTUV89WXYqIJKrstuABCDEhijkFGHLfgMNOPQRSvwxyzZ1270".ToCharArray();

            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < 8; i++)
            {
                sb.Append(valid[rng.Next(valid.Length)]);
            }
            return sb.ToString();
        }
        catch
        {
            return "1a7rel";
        }
    }

    public static string GenerateClientUserName()
    {
        try
        {
            Random rng = new Random();
            char[] valid = new char[62];
            valid = "a3456bcdelmnopTUV89WXYqIJKrstuABCDEhijkFGHLfgMNOPQRSvwxyzZ1270".ToCharArray();

            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < 5; i++)
            {
                sb.Append(valid[rng.Next(valid.Length)]);
            }
            return sb.ToString();
        }
        catch
        {
            return "1a7rel";
        }
    }

    public static string GetCurrentPageName()
    {
        try
        {
            String sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string currentpage = oInfo.Name.ToString().Trim().ToLower();

            return currentpage;

        }
        catch (Exception)
        {

            return "";
        }
    }

    public static string GetCurrentPagePath()
    {
        try
        {

            String sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            return sPath;

        }
        catch (Exception)
        {

            return "";
        }
    }

    public static string ShowCurrentDate()
    {

        try
        {
            // Creates and initializes a DateTimeFormatInfo associated with the en-US culture.
            DateTimeFormatInfo myDTFI = new CultureInfo("en-US", false).DateTimeFormat;

            // Creates a DateTime with the Gregorian date January 3, 2002 (year=2002, month=1, day=3).
            // The Gregorian calendar is the default calendar for the en-US culture.
            DateTime myDT = new DateTime();
            myDT = DateTime.Now;
            // Displays the format pattern associated with each format character.
            return myDT.ToString("D", myDTFI);
        }
        catch (Exception ex)
        {
            clsLog.Publish(ex);
            return DateTime.Now.ToString();
        }

    }

    public static string GenerateCardNo(String CardNO)
    {
        try
        {
            if (CardNO.Length == 16)
            {
                string generateSSN = "XXXXXXXXXXX";

                generateSSN += CardNO.Substring(12, 4);

                return generateSSN;
            }
            else
            {
                return CardNO;
            }
        }
        catch (Exception ex)
        {
            return CardNO;
        }
    }

    public static string GetDefaultPositionSoughtList()
    {
        StringBuilder myList = new StringBuilder();

        myList.Append("Sr. Tax Analyst");
        myList.AppendLine();
        myList.Append("Customer Service Representative");
        myList.AppendLine();
        myList.Append("Company Driver (Class A CDL)");
        myList.AppendLine();
        myList.Append("Human Resource Point Person Cincinnati (Hebron, KY)");
        myList.AppendLine();
        myList.Append("Logistics Accounting Analyst");
        myList.AppendLine();
        myList.Append("Logistics Analyst");
        myList.AppendLine();
        myList.Append("Transportation Operations Manager");
        myList.AppendLine();
        myList.Append("Warehouse Operations Manager");
        myList.AppendLine();
        myList.Append("Freight Pricing Manager");
        myList.AppendLine();
        myList.Append("National Accounts Manager");
        myList.AppendLine();
        myList.Append("Distribution Center Maintenance Supervisor");
        myList.AppendLine();
        myList.Append("Distribution Systems Supervisor");
        myList.AppendLine();
        myList.Append("Business Development Manager");
        myList.AppendLine();
        myList.Append("Supervisor, CAR");
        myList.AppendLine();
        myList.Append("Transportation Supervisor");
        myList.AppendLine();
        myList.Append("Merchandising Analyst Home Office");
        myList.AppendLine();
        myList.Append("Independent Sales Representative");
        myList.AppendLine();
        myList.Append("Retail Sales Representative");
        myList.AppendLine();
        myList.Append("Manager, Host Account");
        myList.AppendLine();
        myList.Append("Host Account Representative");
        myList.AppendLine();
        myList.Append("Independent Sales Representative - North Dakota");
        myList.AppendLine();
        myList.Append("Maintenance");
        myList.AppendLine();
        myList.Append("Gardener");
        myList.AppendLine();
        myList.Append("Maintenance Mechanic");
        myList.AppendLine();
        myList.Append("Maintenance Tech");
        myList.AppendLine();
        myList.Append("Business Analyst");
        myList.AppendLine();
        myList.Append("Associate Help Desk Analyst");
        myList.AppendLine();
        myList.Append("Entry Java Programmer Information Systems");
        myList.AppendLine();
        myList.Append("Field Support Technician");
        myList.AppendLine();
        myList.Append("Unix Administrator");
        myList.AppendLine();
        myList.Append("Technical Java Lead Programmer Analyst");
        myList.AppendLine();
        myList.Append("Administrator, Windows");
        myList.AppendLine();
        myList.Append("Yard Driver");
        myList.AppendLine();
        myList.Append("Diesel Mechanic");
        myList.AppendLine();
        myList.Append("Fuel/ Washer");
        myList.AppendLine();
        myList.Append("Mechanic");
        myList.AppendLine();
        myList.Append("Warehouse Specialist");
        myList.AppendLine();
        myList.Append("Warehouse Selector");
        myList.AppendLine();
        myList.Append("Ship Confirmation Specialist");
        myList.AppendLine();
        myList.Append("Warehouse Department Supervisor");
        myList.AppendLine();
        myList.Append("Selector");
        myList.AppendLine();
        myList.Append("Warehouse");


        return myList.ToString();

    }

    public static string GenerateSSN(String SSN)
    {
        try
        {
            if (SSN.Length == 11)
            {
                string generateSSN = "XXX-XX-";

                generateSSN += SSN.Substring(7, 4);

                return generateSSN;
            }
            else if (SSN.Length == 9)
            {
                string generateSSN = "XXX-XX-";

                generateSSN += SSN.Substring(5, 4);

                return generateSSN;
            }
            else
            {
                return SSN;
            }
        }
        catch (Exception ex)
        {
            return SSN;
        }
    }

    public static bool CheckExistListItemInList(string[] lst, System.Collections.Generic.List<string[]> myList)
    {

        bool isReturn = true;
        for (int i = 0; i < myList.Count; ++i)
        {
            int myColList = lst.Length;
            for (int j = 0; j < myColList; ++j)
            {
                if (lst[j] != myList[i][j].ToString())
                {
                    isReturn = false;
                    break;
                }
            }
            if (isReturn == false)
            {
                break;
            }
        }
        return isReturn;
    }

    public static string StripXMLCData(string inString) //INPUT xml withCDATA:  OUT: plain XML
    {
        string beginCData = "<![CDATA[";
        string endCData = "]]>";
        int start = inString.IndexOf(beginCData);
        if (start >= 0)
            start += beginCData.Length;
        else start = 0;
        int end = inString.LastIndexOf(endCData);
        int len = inString.Length;
        len = end - start;
        if (len > 0)
            return inString.Substring(start, len);
        else return inString;
    }

    public static string GetInquiryNumber(string applicantId, string clientName) //INPUT xml withCDATA:  OUT: plain XML
    {
        string clientAbbr = clientName;
        if (clientName.Length > 3)
        {
            clientAbbr = clientName.Substring(0, 4).Trim();
        }

        return clientAbbr + "_" + applicantId.ToString().PadLeft(4, '0');
    }

    //public static void GenerateHeaderMenu(string HeaderMenuId, DataTable dtHeaderMenu, DataRow drHeaderMenu, StringBuilder sbHeaderMenu, Dictionary<string, int> userPermissions, StringBuilder sbLeftNavIShortcuts)
    //{
    //    string _Where = "MenuId = '" + HeaderMenuId + "'";

    //    DataTable dtSubMenu = new DataTable();
    //    try
    //    {
    //        dtSubMenu = dtHeaderMenu.Select(_Where).CopyToDataTable();
    //    }
    //    catch (Exception)
    //    {
    //        dtSubMenu = new DataTable();
    //    }


    //    if (HeaderMenuId == "1")
    //    {
    //        string dashBoardRedirectUrl = "Dashboard.aspx";//Fixed page
    //        if (dtSubMenu.Rows.Count > 0)
    //        {
    //            foreach (DataRow drSubMenu in dtSubMenu.Rows)
    //            {
    //                string subMenuId = drSubMenu["PageId"].ToString();
    //                string pageUrl = drSubMenu["PageName"].ToString();
    //                dashBoardRedirectUrl = pageUrl;
    //                if (subMenuId == "1")// Admin dashboard
    //                    break;
    //            }
    //        }

    //        //Set permission to verify

    //        if (!userPermissions.ContainsKey(dashBoardRedirectUrl.ToLower()))
    //            userPermissions.Add(dashBoardRedirectUrl.ToLower(), 1);

    //        string currentPage = GetCurrentPageName();
    //        string activeClass = "";
    //        if (currentPage == dashBoardRedirectUrl.ToLower())
    //        {
    //            activeClass = "class='active'";
    //        }

    //        sbHeaderMenu.Append("<li><a " + activeClass + " href='" + dashBoardRedirectUrl + "' title='Dashboard' ><i class='fa fa-dashboard'></i>Dashboard</a></li>");
    //    }
    //    else
    //    {
    //        GenerateHeaderMenuOptions(sbHeaderMenu, sbHeaderMenu, drHeaderMenu, dtHeaderMenu, userPermissions, sbLeftNavIShortcuts);
    //    }
    //}

    //public static void GenerateHeaderMenuOptions(StringBuilder sbHeaderMenu, StringBuilder sbHeaderSubMenu, DataRow drHeaderMenu, DataTable dtHeaderMenu, Dictionary<string, int> userPermissions, StringBuilder sbLeftNavIShortcuts)
    //{
    //    string HeaderMenuId = drHeaderMenu["PageId"].ToString();
    //    string _Where = "MenuId = '" + HeaderMenuId + "'";

    //    DataTable dtSubMenu = new DataTable();
    //    try
    //    {
    //        dtSubMenu = dtHeaderMenu.Select(_Where).CopyToDataTable();
    //    }
    //    catch (Exception)
    //    {
    //        dtSubMenu = new DataTable();
    //    }

    //    if (dtSubMenu.Rows.Count > 0)
    //    {
    //        //----------Header menu option------------------------------------------------------------------------------------
    //        string HeaderMenuTitle = drHeaderMenu["PageDisplayName"].ToString();
    //        string HeaderMenuUrl = drHeaderMenu["PageName"].ToString();
    //        string iconClass = drHeaderMenu["Icon"].ToString().Trim();

    //        int MenuId = 0;
    //        int.TryParse(drHeaderMenu["PageId"].ToString(), out MenuId);

    //        //Add carrot icon for collapse
    //        string iconCollapase = "";
    //        if (dtSubMenu.Rows.Count > 1 || (dtSubMenu.Rows.Count == 1 && HeaderMenuUrl.Trim() == ""))
    //        {
    //            iconCollapase = "<i class='fa fa-caret-left'></i>";
    //        }

    //        if (HeaderMenuUrl.Trim() == "")
    //        {
    //            HeaderMenuUrl = "javascript:void(0);";
    //        }
    //        else
    //        {
    //            if (!userPermissions.ContainsKey(HeaderMenuUrl.ToLower()))
    //                userPermissions.Add(HeaderMenuUrl.ToLower(), MenuId);
    //        }
    //        //-----------------------------------------------------------------------------------------------------------------
    //        //Create child menu
    //        string childMenuLi = "";
    //        bool containsChild = false;
    //        string activeClass = "";
    //        string currentPage = GetCurrentPageName();
    //        //Left shortcuts saperator
    //        bool showSaperator = true;
    //        bool showViewSectionSaperator = true;

    //        foreach (DataRow drSubMenu in dtSubMenu.Rows)
    //        {
    //            string menuName = drSubMenu["PageDisplayName"].ToString();
    //            string pageUrl = drSubMenu["PageName"].ToString().Trim();

    //            int childPageId = 0;
    //            int.TryParse(drSubMenu["PageId"].ToString().Trim(), out childPageId);

    //            if (childPageId == 47)
    //            {
    //                pageUrl = "http://169.54.222.231:90/Default.aspx";
    //            }

    //            if (pageUrl == "") //Skip sub option
    //            {
    //                continue;
    //            }
    //            iconClass = drSubMenu["Icon"].ToString().Trim();

    //            containsChild = true;
    //            childMenuLi += ("<li><a href='" + pageUrl + "'><i class='fa fa-" + iconClass + "'></i> " + menuName + "</a></li>");
    //            //Add list for access mangement
    //            //string urlKey = pageUrl.Contains('?') ? (pageUrl.Split('?'))[0].ToLower() : pageUrl.ToLower();
    //            if (!userPermissions.ContainsKey(pageUrl.ToLower()))
    //                userPermissions.Add(pageUrl.ToLower(), MenuId);
    //            //Add active class
    //            if (currentPage == pageUrl.ToLower() && activeClass == "")
    //            {
    //                activeClass = "class='active'";
    //            }
    //            //----------------------------------------------------------------------
    //            //Now Add Left Icon Shortcuts
    //            //----------------------------------------------------------------------
    //            if (clsGlobal.LeftNavShortcutMaster.ShortcutIconPageId.Keys.Contains(childPageId))
    //            {
    //                //Show saperator
    //                if (showSaperator && HeaderMenuId != "2")//Skip 1st saprerator
    //                {
    //                    showSaperator = false;
    //                    sbLeftNavIShortcuts.Append("<span class='section'></span>");
    //                }
    //                else
    //                {
    //                    if (HeaderMenuId == "2" && showViewSectionSaperator)//Add saperator for view section
    //                    {
    //                        if (childPageId == 4 || childPageId == 5 || childPageId == 27 || childPageId == 33)
    //                        {
    //                            showViewSectionSaperator = false;
    //                            sbLeftNavIShortcuts.Append("<span class='section'></span>");
    //                        }

    //                    }
    //                }
    //                sbLeftNavIShortcuts.Append("<a href='" + pageUrl + "' title='" + menuName + "'> <i class='fa md-48 " + clsGlobal.LeftNavShortcutMaster.ShortcutIconPageId[childPageId] + "'></i></a>");
    //            }
    //            //----------------------------------------------------------------------
    //        }
    //        //Set Active class
    //        if (!containsChild)
    //        {
    //            //Add active class
    //            if (currentPage == HeaderMenuUrl.ToLower())
    //            {
    //                activeClass = "class='active'";
    //            }
    //        }
    //        sbHeaderMenu.Append("<li><a " + activeClass + " href='" + HeaderMenuUrl + "' title='" + HeaderMenuTitle +
    //                            "' ><i class='fa fa-" + iconClass + "'></i>" + HeaderMenuTitle + iconCollapase + "</a>");

    //        if (containsChild)
    //        {
    //            sbHeaderMenu.Append("<ul id='submenu_" + MenuId + "' style='display:none;'>");
    //            sbHeaderMenu.Append(childMenuLi);
    //            sbHeaderMenu.Append("</ul></li>");
    //        }
    //    }
    //}

    public static Dictionary<string, string> GetDictionaryFromDataTable(DataTable dtSource, string keyColumnName, string valueColumnName)
    {
        Dictionary<string, string> dicDataValues = new Dictionary<string, string>();

        foreach (DataRow drDetails in dtSource.Rows)
        {
            string key = drDetails[keyColumnName].ToString().Trim();
            string value = drDetails[valueColumnName].ToString().Trim();

            if (key != "" && !dicDataValues.Keys.Contains(key))
                dicDataValues.Add(key, value);
        }
        return dicDataValues;
    }

    //Get selected columns from a datatable
    public static DataTable GetSelectedColumnsFromDataTable(DataTable dtSource, string[] columnNames)
    {
        //Create datatable
        DataTable dtNewTable = new DataTable();
        dtNewTable = new DataView(dtSource).ToTable(false, columnNames);

        return dtNewTable;
    }

    //public static void DisableForm(ControlCollection ctrls)
    //{
    //    foreach (Control ctrl in ctrls)
    //    {
    //        if (ctrl is TextBox)
    //            ((TextBox)ctrl).Enabled = false;
    //        if (ctrl is Button)
    //            ((Button)ctrl).Enabled = false;
    //        else if (ctrl is DropDownList)
    //            ((DropDownList)ctrl).Enabled = false;
    //        else if (ctrl is CheckBox)
    //            ((CheckBox)ctrl).Enabled = false;
    //        else if (ctrl is RadioButton)
    //            ((RadioButton)ctrl).Enabled = false;
    //        else if (ctrl is HtmlInputButton)
    //            ((HtmlInputButton)ctrl).Disabled = true;
    //        else if (ctrl is HtmlInputText)
    //            ((HtmlInputText)ctrl).Disabled = true;
    //        else if (ctrl is HtmlSelect)
    //            ((HtmlSelect)ctrl).Disabled = true;
    //        else if (ctrl is HtmlInputCheckBox)
    //            ((HtmlInputCheckBox)ctrl).Disabled = true;
    //        else if (ctrl is HtmlInputRadioButton)
    //            ((HtmlInputRadioButton)ctrl).Disabled = true;

    //        DisableForm(ctrl.Controls);
    //    }
    //}

    //Generate client menu
    //public static void GenerateClientHeaderMenu(DataTable dtHeaderMenu, StringBuilder sbHeaderMenu)
    //{

    //    if (dtHeaderMenu.Rows.Count > 0)
    //    {
    //        //int RowCount = 1;
    //        foreach (DataRow drHeaderMenu in dtHeaderMenu.Rows)
    //        {
    //            //Get header parent menu
    //            int parentMenuId = 0;
    //            int.TryParse(drHeaderMenu["MenuId"].ToString(), out parentMenuId);

    //            //Skip sub menu
    //            if (parentMenuId > 0)
    //            {
    //                break;
    //            }

    //            //Skipe menu from dynmic list
    //            List<int> SkippedMenus = new List<int>();
    //            SkippedMenus.AddRange(new int[] { 3, 6, 7, 8, 9, 10, 11, 12 });// 5,

    //            int HeaderMenuId = 0;
    //            int.TryParse(drHeaderMenu["PageId"].ToString(), out HeaderMenuId);

    //            if (HeaderMenuId == 4 || HeaderMenuId == 13) //Skip online applicat from menu for inactive clients
    //            {
    //                bool IsClientActive = clsLoginCookie.LoginClientIsActive();
    //                if (!IsClientActive)
    //                {
    //                    SkippedMenus.Add(HeaderMenuId);
    //                }
    //            }

    //            if (SkippedMenus.Contains(HeaderMenuId))
    //            {
    //                continue;
    //            }

    //            string HeaderMenuIconClass = drHeaderMenu["Icon"].ToString();
    //            //---------------------------------------------------------------------------------------------------
    //            string _Where = "MenuId = '" + HeaderMenuId + "'";

    //            DataTable dtSubMenu = new DataTable();
    //            try
    //            {
    //                dtSubMenu = dtHeaderMenu.Select(_Where).CopyToDataTable();
    //            }
    //            catch (Exception)
    //            {
    //                dtSubMenu = new DataTable();
    //            }
    //            //---------------------------------------------------------------------------------------------------
    //            //---------------------------------------------------------------------------------------------------
    //            //Generate header menu with submenus
    //            //---------------------------------------------------------------------------------------------------
    //            if (dtSubMenu.Rows.Count > 0)
    //            {
    //                //----------Header menu option------------------------------------------------------------------------------------
    //                string HeaderMenuTitle = drHeaderMenu["PageDisplayName"].ToString();
    //                string HeaderMenuUrl = drHeaderMenu["PageName"].ToString();
    //                string iconClass = drHeaderMenu["Icon"].ToString().Trim();

    //                int MenuId = 0;
    //                int.TryParse(drHeaderMenu["PageId"].ToString(), out MenuId);

    //                if (HeaderMenuUrl.Trim() == "")
    //                {
    //                    HeaderMenuUrl = "javascript:void(0);";
    //                }
    //                //-----------------------------------------------------------------------------------------------------------------
    //                //Create child menu
    //                string childMenuLi = "";
    //                bool containsChild = false;
    //                //string activeClass = "";
    //                string currentPage = GetCurrentPageName();

    //                foreach (DataRow drSubMenu in dtSubMenu.Rows)
    //                {
    //                    string menuName = drSubMenu["PageDisplayName"].ToString();
    //                    string pageUrl = drSubMenu["PageName"].ToString().Trim();
    //                    iconClass = drSubMenu["Icon"].ToString().Trim();

    //                    if (dtSubMenu.Rows.Count == 1)//Single Header Menu
    //                    {
    //                        if (HeaderMenuUrl == "javascript:void(0);" && pageUrl != "")
    //                        {
    //                            HeaderMenuUrl = pageUrl;
    //                        }
    //                        continue;
    //                    }
    //                    else if (pageUrl == "") //Skip sub option
    //                    {
    //                        continue;
    //                    }

    //                    containsChild = true;
    //                    childMenuLi += "<li ><a href='" + pageUrl + "'><i class='" + iconClass + "'></i> " + menuName + "</a></li>";
    //                }

    //                if (containsChild) // has submenu
    //                {
    //                    string headerMenuWithChild = "<li class='no-padding' id='left_nav_active_" + HeaderMenuId + "' runat='server'>" +
    //                    "<ul class='collapsible collapsible-accordion'>" +
    //                    "<li class='bold'><a class='collapsible-header waves-effect waves-cyan'><i class='" + HeaderMenuIconClass + "'></i> " +
    //                    HeaderMenuTitle + "</a>" +
    //                    "<div class='collapsible-body'>" +
    //                    "<ul>" + childMenuLi + "</ul>" +
    //                    "</div>" +
    //                    "</li></ul></li>";
    //                    sbHeaderMenu.Append(headerMenuWithChild);
    //                }
    //                else //Stand alone
    //                {
    //                    sbHeaderMenu.Append("<li class='bold ' runat='server' >" +
    //                                        "<a href='" + HeaderMenuUrl + "' class='waves-effect waves-cyan '>" +
    //                                        "<i class='" + HeaderMenuIconClass + "'></i> " + HeaderMenuTitle + "</a></li>");
    //                }

    //            }

    //        }
    //    }
    //}

    //Get List from Data table
    public static List<string> GetListFromDataTable(DataTable dtTable, string columnName)
    {
        List<string> convertedList = new List<string>();
        convertedList = (from row in dtTable.AsEnumerable() select Convert.ToString(row[columnName].ToString().ToLower())).ToList();
        return convertedList;
    }

    //Validate Email
    public static bool IsValidEmailString(string email)
    {
        email = email.Trim();
        string expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(email, expresion))
        {
            if (Regex.Replace(email, expresion, string.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    //Validate Email and Get Comma string
    public static string GetValidEmailListAsCommaSeperatedString(string commaSapredateEmail)
    {
        //Explode and get multiple emails
        string[] emailList = { "" };
        if (commaSapredateEmail.Contains(","))
        {
            emailList = commaSapredateEmail.Split(',');
        }
        else//Single email
        {
            emailList[0] = commaSapredateEmail;
        }

        string ToEmailValidList = "";
        int validEmailCount = 0;
        for (int count = 0; count < emailList.Length; count++)
        {
            string emailAddress = emailList[count];

            if (emailAddress.Trim() == "" || !clsAdvanceUtility.IsValidEmailString(emailAddress))
            {
                continue;//Skip
            }

            //Add email as multiple recepients
            if (count > 0 && validEmailCount > 0)
            {
                ToEmailValidList += ",";
            }
            ToEmailValidList += emailAddress;
            validEmailCount++;
        }
        return ToEmailValidList;
    }

    public static string GetParsedDate(string dbDate, string format)//Validte and get correct date string
    {
        DateTime dtFormattedDate = new DateTime();
        DateTime.TryParse(dbDate, out dtFormattedDate);

        if (dtFormattedDate.Year < 1900)
            return "";
        else
            return dtFormattedDate.ToString(format);
    }

    //public static bool ForceDownloadFile(string FileAbsolutePath)
    //{
    //    System.IO.FileInfo file = new System.IO.FileInfo(FileAbsolutePath);
    //    string fileType = file.Extension;
    //    try
    //    {
    //        if (File.Exists(FileAbsolutePath))//Check File exists
    //        {
    //            System.Web.HttpContext.Current.Response.Clear();
    //            System.Web.HttpContext.Current.Response.AddHeader(clsGlobal.CompanyDetails.CompanyName, clsGlobal.CompanyDetails.CompanyName);// Invoice Number 1
    //            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename =" + file.Name);
    //            System.Web.HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
    //            System.Web.HttpContext.Current.Response.ContentType = "application/" + fileType;
    //            System.Web.HttpContext.Current.Response.WriteFile(file.FullName);
    //            System.Web.HttpContext.Current.Response.TransmitFile(FileAbsolutePath);
    //            System.Web.HttpContext.Current.Response.Flush();
    //            System.Web.HttpContext.Current.Response.End();
    //        }
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //}

    //public static void CopyListBoxItem(ListBox lstBoxFrom, ListBox lstBoxTo, bool move)
    //{
    //    //lstBoxTo.Items.Clear();
    //    foreach (ListItem lstItem in lstBoxFrom.Items)
    //    {
    //        lstBoxTo.Items.Add(lstItem);
    //    }
    //    //Clear from list
    //    if (move)
    //    {
    //        lstBoxFrom.Items.Clear();
    //    }
    //}

    public static void DeleteAllFilesInFolder(string folderPath)
    {
        try
        {
            string absolutePath = HttpContext.Current.Server.MapPath(folderPath);
            if (Directory.Exists(absolutePath))
            {
                Array.ForEach(Directory.GetFiles(@absolutePath), File.Delete);

                Directory.Delete(@absolutePath);
            }
        }
        catch (Exception ex)
        {
        }
    }

    //Remove html from string
    public static string StripHTML(string input)
    {
        return Regex.Replace(input, "<.*?>", String.Empty);
    }

    /// <summary>
    /// Get wheather details for a given location
    /// </summary>
    /// <param name="location"></param>
    /// <returns>Location </returns>
    //public static WeatherInfo GetWeatherInfo(string location)
    //{
    //    try
    //    {
    //        string appId = System.Configuration.ConfigurationManager.AppSettings["WeatherAPIID"].ToString();
    //        string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt=1&APPID={1}", location.Trim(), appId);
    //        using (WebClient client = new WebClient())
    //        {
    //            string json = client.DownloadString(url);
    //            WeatherInfo weatherInfo = (new JavaScriptSerializer()).Deserialize<WeatherInfo>(json);
    //            return weatherInfo;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return null;
    //    }

    //}
    /// <summary>
    /// Get current machine IP address
    /// </summary>
    /// <returns></returns>
    public static string GetUserIP()
    {

        string myExternalIP;
        string strHostName = System.Net.Dns.GetHostName();
        string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
        string clientip = clientIPAddress.ToString();
        System.Net.HttpWebRequest request =
    (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create("http://www.whatismyip.org");
        request.UserAgent = "User-Agent: Mozilla/4.0 (compatible; MSIE" +
            "6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        System.Net.HttpWebResponse response =
        (System.Net.HttpWebResponse)request.GetResponse();
        using (System.IO.StreamReader reader = new
        StreamReader(response.GetResponseStream()))
        {
            myExternalIP = reader.ReadToEnd();
            reader.Close();
        }

        return myExternalIP;

    }

    public static string GetCountryByIP()
    {
        try
        {
            string strReturnVal;
            string ipAddress = "";
            ipAddress = System.Web.HttpContext.Current.Request.Params["HTTP_CLIENT_IP"] ?? System.Web.HttpContext.Current.Request.UserHostAddress;
            //ipAddress = "103.208.116.73";

            string ipResponse = IPRequestHelper("http://ip-api.com/xml/" + ipAddress);

            //return ipResponse;
            XmlDocument ipInfoXML = new XmlDocument();
            ipInfoXML.LoadXml(ipResponse);
            XmlNodeList responseXML = ipInfoXML.GetElementsByTagName("query");

            NameValueCollection dataXML = new NameValueCollection();

            dataXML.Add(responseXML.Item(0).ChildNodes[2].InnerText, responseXML.Item(0).ChildNodes[2].Value);

            strReturnVal = responseXML.Item(0).ChildNodes[5].InnerText.ToString(); //  City
            strReturnVal += responseXML.Item(0).ChildNodes[1].InnerText.ToString(); // Contry
            strReturnVal += "(" +
            responseXML.Item(0).ChildNodes[2].InnerText.ToString() + ")";  // Contry Code 

            return strReturnVal;
        }
        catch (Exception ex)
        {
            return "Texas,US";
        }
    }

    /*public static string GetCountryByIP()
    {
        string ipAddress = HttpContext.Current.Request.Params["HTTP_CLIENT_IP"] ?? HttpContext.Current.Request.UserHostAddress;

        ipAddress = "103.208.116.73";
        string ipResponse = IPRequestHelper("http://ipinfodb.com/ip_query_country.php?ip=", ipAddress);
        XmlDocument ipInfoXML = new XmlDocument();
        ipInfoXML.LoadXml(ipResponse);
        XmlNodeList responseXML = ipInfoXML.GetElementsByTagName("Response");
        NameValueCollection dataXML = new NameValueCollection();
        dataXML.Add(responseXML.Item(0).ChildNodes[2].InnerText, responseXML.Item(0).ChildNodes[2].Value);
        string xmlValue = dataXML.Keys[0];

      return xmlValue;
    }
    */
    private static string IPRequestHelper(string checkURL)
    {
        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(checkURL);
        HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
        StreamReader responseStream = new StreamReader(objResponse.GetResponseStream());
        string responseRead = responseStream.ReadToEnd();
        responseStream.Close();
        responseStream.Dispose();
        return responseRead;
    }


    //Check Valid CouponDate
    public static bool CheckValidCouponDate()
    {
        String CouponValidityDate = ConfigurationManager.AppSettings["DefaultClientCouponValidityDate"];
        DateTime dtCouponDate = DateTime.Now;
        DateTime.TryParse(CouponValidityDate, out dtCouponDate);

        if (dtCouponDate.Date >= DateTime.Now.Date)
        {
            return true;
        }
        return false;
    }
    public static byte[] GetFileByte(string filePath)
    {
        byte[] buffer = null;
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            buffer = new byte[fs.Length];
            fs.Read(buffer, 0, (int)fs.Length);
        }
        return buffer;
    }

    //Set API credentials Automtically


    public static string GetXMLTagValue(DataTable dtXMLResult, string TagName)
    {
        if (dtXMLResult.Columns.Contains(TagName))
        {
            return dtXMLResult.Rows[0][TagName].ToString().Trim();
        }
        return "-";
    }

    //Get image from string CHUNK
    public static bool SaveByteArrayAsFile(string fullOutputPath, string base64imageString)
    {
        try
        {
            File.WriteAllBytes(fullOutputPath, Convert.FromBase64String(base64imageString));
            return true;
        }
        catch (Exception ex)
        {
            clsLog.Publish(ex);
            return false;
        }

    }
}
