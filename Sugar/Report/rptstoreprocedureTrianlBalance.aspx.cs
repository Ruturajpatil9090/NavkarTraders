using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.IO;
using System.Xml.Linq;


public partial class Report_rptstoreprocedureTrianlBalance : System.Web.UI.Page
{
    string salebill = string.Empty;
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    string tblPrefix = string.Empty;
    DataSet ds = null;
    DataTable dt = null;
    double netdebit = 0.00; double netcredit = 0.00;
    double netdifference = 0.0;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblCompanyName.Text = Session["Company_Name"].ToString();
        this.BindList();
    }
    private void BindList()
    {
        #region
        //try
        //{
        //    //qry = "select AC_CODE,Ac_Name_E,Group_Code,BSGroupName,CityName,group_Type,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance,"+
        //    //    "'' as DebitD,'' as CreditD from qryGledgernew " +
        //    //       " where COMPANY_CODE=4 and DOC_DATE<='2018/11/03' " +
        //    //       " group by AC_CODE,Ac_Name_E,Group_Code,BSGroupName,CityName,group_Type" +
        //    //        " having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) != 0";

        //    qry = "select AC_CODE,Ac_Name_E,Group_Code,BSGroupName,CityName,group_Type,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Balance," +
        //       "'' as DebitD,'' as CreditD from qryGledgernew " +
        //          " where COMPANY_CODE=1 and DOC_DATE<='2018/03/31' " +
        //          " group by AC_CODE,Ac_Name_E,Group_Code,BSGroupName,CityName,group_Type" +
        //           " having SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) != 0";

        //    ds = clsDAL.SimpleQuery(qry);
        //    dt = ds.Tables[0];

        //    DataView view = new DataView(dt);
        //    view.Sort = "BSGroupName asc";
        //    DataTable distinctValues = view.ToTable(true, "Group_Code", "BSGroupName");

        //    dtl.DataSource = distinctValues;
        //    dtl.DataBind();

        //    lblnetDebit.Text = netdebit.ToString();
        //    lblnetCredit.Text = netcredit.ToString();


        //    double totaldiffrenceabs = Math.Abs(Math.Round(netdifference, 2));
        //    lblTotalDifference.Text = Convert.ToString(totaldiffrenceabs);
        //}
        //catch (Exception)
        //{
        //    throw;
        //}
        #endregion
        try
        {
            XElement root = new XElement("ROOT");
            XElement child1 = new XElement("Head");
            int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
            int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
            // billno = Request.QueryString["billno"];
            //string from_date = "2018/04/01";
            //string to_date = "2018/11/15";
            //string Group_Type = "";
            //string Group_Code = "";

            string from_date = Request.QueryString["FromDt"];
            string to_date = Request.QueryString["ToDt"];
            string ac_type = Request.QueryString["ac_type"];
            string Group_Code = Request.QueryString["Cwhere"];
            string group_type = Request.QueryString["group_type"];

            child1.SetAttributeValue("Company_Code", Company_Code);
            child1.SetAttributeValue("year", Year_Code);
            child1.SetAttributeValue("from_date", from_date);
            child1.SetAttributeValue("to_date", to_date);
            child1.SetAttributeValue("ac_type", ac_type);
            child1.SetAttributeValue("Group_Code", Group_Code);
            child1.SetAttributeValue("group_type", group_type);


            root.Add(child1);
            string XMLReport = root.ToString();
            XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
            XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
            string xmlfile = XMLReport;

            ds = clsDAL.xmlExecuteDMLQryReport1("Report_TrialBalance", xmlfile);

            dt = ds.Tables[0];

            DataView view = new DataView(dt);
            view.Sort = "BSGroupName asc";
            DataTable distinctValues = view.ToTable(true, "Group_Code", "BSGroupName", "group_Type");

            dtl.DataSource = distinctValues;
            dtl.DataBind();

            lblnetDebit.Text = netdebit.ToString();
            lblnetCredit.Text = netcredit.ToString();


            double totaldiffrenceabs = Math.Abs(Math.Round(netdifference, 2));
            lblTotalDifference.Text = Convert.ToString(totaldiffrenceabs);
        }
        catch (Exception)
        {
            throw;
        }

    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            DataList dtl1 = (DataList)e.Item.FindControl("dtlDetails");
            Label lblgroupCode = (Label)e.Item.FindControl("lblGroup_Code");

            DataTable tblMEN = ds.Tables[0];
            DataTable dtT = new DataTable();

            dtT.Columns.Add("AC_CODE", typeof(string));
            dtT.Columns.Add("Ac_Name_E", typeof(string));
            dtT.Columns.Add("CityName", typeof(string));
            dtT.Columns.Add("DebitD", typeof(string));
            dtT.Columns.Add("CreditD", typeof(string));
            string sortOrder = "CityName,Ac_Name_E asc";
            DataRow[] results = tblMEN.Select("Group_Code=" + lblgroupCode.Text, sortOrder);

            double balminus;
            double bal = 0.00;
            double groupdebitamt = 0.00;
            double groupcreditamt = 0.00;
            foreach (DataRow row in results)
            {
                bal = Convert.ToDouble(row[6]);

                if (bal > 0)
                {
                    row["DebitD"] = bal.ToString();
                    row["CreditD"] = "";
                    groupdebitamt += bal;
                }
                else
                {
                    balminus = Math.Abs(bal);
                    groupcreditamt += balminus;
                    row["DebitD"] = "";
                    row["CreditD"] = balminus.ToString();
                }
                dtT.ImportRow(row);


            }

            Label lblDebitTotal = (Label)e.Item.FindControl("lblDebitTotal");
            Label lblCreditTotal = (Label)e.Item.FindControl("lblCreditTotal");
            Label lblDiff = (Label)e.Item.FindControl("lblDiff");

            lblDebitTotal.Text = groupdebitamt.ToString();
            lblCreditTotal.Text = groupcreditamt.ToString();
            lblDiff.Text = "Diff:" + (Convert.ToDouble(lblDebitTotal.Text) - Convert.ToDouble(lblCreditTotal.Text)).ToString();

            netdebit += groupdebitamt;
            netcredit += groupcreditamt;
            netdifference = netdebit - netcredit;
            dtl1.DataSource = dtT;
            dtl1.DataBind();

        }
        catch
        {
        }
    }
    protected void lnkDO_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkOV = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkOV.NamingContainer;
            string fromdt1 = clsGV.Start_Date;
            string todt1 = clsGV.To_date;
            string accode = lnkOV.Text;
            // Session["VOUC_NO"] = no;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + accode + "','" + fromdt1 + "','" + todt1 + "','DrCr')", true);
            lnkOV.Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }
}