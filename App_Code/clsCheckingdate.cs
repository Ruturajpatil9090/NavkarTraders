using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsCheckingdate
/// </summary>
public class clsCheckingdate
{
    public clsCheckingdate()
    {
        //
        // TODO: Add constructor logic here
        //
    }
        public static string checkdate(string docdate,string postdate,string outword,string inword,string returnmessage,int companycode,int yearcode )
        {


            #region Check PostDate
            string Post_date = string.Empty;
            if (postdate != null && postdate != "")
            {
                Post_date = postdate;
            }
            else
            {
                returnmessage = "Update Post Date";
                //setFocusControl(txtDOC_DATE);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Update Post Date !!!');", true);
                return returnmessage;
            }
            string dd = "";
            string format = "MM/dd/yyyy";
            dd = DateTime.Parse(Post_date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            DateTime dt1 = Convert.ToDateTime(dd);

            // string dd = "";
            dd = DateTime.Parse(docdate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

            DateTime dtdate = Convert.ToDateTime(dd);
            string Docdate = dtdate.ToString("yyyy-MM-dd HH:mm:ss.fff");

            if (dtdate > dt1)
            {
                // isValidated = true;
            }
            else
            {
                returnmessage = "Post Date Error";
                //setFocusControl(txtDOC_DATE);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Post Date Error!!!!!');", true);
                return returnmessage;
            }
            if (outword != string.Empty)
            {
                string Outword_Date = clsCommon.getString("select date_format(Outword_Date,'%d/%m/%Y') as Outword from Post_Date where Company_Code=" +companycode);

                // value = Outword_Date;
                Outword_Date = DateTime.Parse(Outword_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

                dt1 = Convert.ToDateTime(Outword_Date);
                Outword_Date = dt1.ToString("yyyy-MM-dd HH:mm:ss.fff");

                if (dtdate > dt1)
                {

                }
                else
                {
                    returnmessage = "GST Return Fined please Do not edit Record";
                    //setFocusControl(txtDOC_DATE);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('GST Return Fined please Do not Delete Record!!!!!');", true);
                    return returnmessage;
                }

            }

            if (inword != string.Empty)
            {
                string Inword_Date = clsCommon.getString("select date_format(Inword_Date,'%d/%m/%Y') as Inword from Post_Date where Company_Code=" + companycode);

                // value = Outword_Date;
                Inword_Date = DateTime.Parse(Inword_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");

                dt1 = Convert.ToDateTime(Inword_Date);
                Inword_Date = dt1.ToString("yyyy-MM-dd HH:mm:ss.fff");

                if (dtdate > dt1)
                {

                }
                else
                {
                    returnmessage = "GST Return Fined please Do not edit Record";
                    //setFocusControl(txtDOC_DATE);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('GST Return Fined please Do not Delete Record!!!!!');", true);
                    return returnmessage;
                }
            }

            #endregion


            return returnmessage;

        }


}