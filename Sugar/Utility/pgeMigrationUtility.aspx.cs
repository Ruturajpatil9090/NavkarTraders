using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.IO;

public partial class Sugar_Utility_pgeMigrationUtility : System.Web.UI.Page
{
    string mysqlconn = ConfigurationManager.ConnectionStrings["sqlconnection1"].ConnectionString;
    string sqlconn = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

    SqlConnection sqlcon;
    SqlConnection mysqcon;
    SqlTransaction transaction = null;
    SqlCommand MySQLcmd = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        sqlcon = new SqlConnection(sqlconn);
        mysqcon = new SqlConnection(mysqlconn);

        if (!Page.IsPostBack)
        {
            string qry = "select * from Company";
            DataSet ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        drpCompany.DataSource = dt;
                        drpCompany.DataValueField = dt.Columns[0].ToString();
                        drpCompany.DataTextField = dt.Columns[1].ToString();

                        drpCompany.DataBind();
                    }
                }
            }
        }
    }

    protected void btnCityMaster_Click(object sender, EventArgs e)
    {
        try
        {
            mysqcon.Open();
            string cityqrymysql = "select * from nt_1_citymaster";
            SqlCommand cmd = new SqlCommand(cityqrymysql, mysqcon);
            SqlDataAdapter returnVal = new SqlDataAdapter(cityqrymysql, mysqcon);
            DataTable dt = new DataTable();
            returnVal.Fill(dt);

            string cityqrysql = "select * from nt_1_citymaster";
            SqlCommand cmdsql = new SqlCommand(cityqrysql, sqlcon);
            SqlDataAdapter returnValsql = new SqlDataAdapter(cityqrysql, sqlcon);
            DataTable dtsql = new DataTable();
            returnValsql.Fill(dtsql);

            DataTable dt3 = new DataTable();

            dt3.Columns.Add((new DataColumn("city_code", typeof(int))));
            dt3.Columns.Add((new DataColumn("city_name_e", typeof(string))));
            dt3.Columns.Add((new DataColumn("pincode", typeof(string))));
            dt3.Columns.Add((new DataColumn("Sub_Area", typeof(string))));
            dt3.Columns.Add((new DataColumn("city_name_r", typeof(string))));
            dt3.Columns.Add((new DataColumn("company_code", typeof(string))));
            dt3.Columns.Add((new DataColumn("state", typeof(string))));
            dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
            dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
            dt3.Columns.Add((new DataColumn("Distance", typeof(string))));
            dt3.Columns.Add((new DataColumn("GstStateCode", typeof(string))));
            dt3.Columns.Add((new DataColumn("cityid", typeof(string))));


            for (int i = 0; i < dtsql.Rows.Count; i++)
            {

                DataRow[] dr = dt.Select("city_code = " + dtsql.Rows[i]["city_code"].ToString());

                if (dr.Length == 0)
                {

                    dt3.ImportRow(dtsql.Rows[i]);

                }
            }


            string columnname = "city_code,city_name_e,pincode,Sub_Area,city_name_r,company_code,state,Created_By,Modified_By,Distance,GstStateCode,cityid";
            string columnvalues = "";
            int cityid = 1;
            int maxLavel;
            if (dt.Rows.Count > 0)
            {
                maxLavel = Convert.ToInt32(dt.Compute("max([cityid])", string.Empty));
                cityid = maxLavel + 1;
            }

            for (int i = 0; i < dt3.Rows.Count; i++)
            {

                columnvalues = columnvalues + "(";
                columnvalues = columnvalues + dt3.Rows[i]["city_code"].ToString() + ",";
                string cityname = dt3.Rows[i]["city_name_e"].ToString();
                cityname = cityname.Replace("'", "");
                cityname = cityname.Replace("(", "");
                cityname = cityname.Replace(")", "");


                columnvalues = columnvalues + "'" + cityname + "'" + ",";
                columnvalues = columnvalues + "'" + dt3.Rows[i]["pincode"].ToString() + "'" + ",";
                columnvalues = columnvalues + "'" + dt3.Rows[i]["Sub_Area"].ToString() + "'" + ",";
                columnvalues = columnvalues + "'" + dt3.Rows[i]["city_name_r"].ToString() + "'" + ",";
                columnvalues = columnvalues + dt3.Rows[i]["company_code"].ToString() + ",";
                string state = dt3.Rows[i]["state"].ToString();
                state = state.Replace("'", "");
                state = state.Replace("(", "");
                state = state.Replace(")", "");
                columnvalues = columnvalues + "'" + state + "'" + ",";
                columnvalues = columnvalues + "'" + dt3.Rows[i]["Created_By"].ToString() + "'" + ",";
                columnvalues = columnvalues + "'" + dt3.Rows[i]["Modified_By"].ToString() + "'" + ",";
                columnvalues = columnvalues + (dt3.Rows[i]["Distance"].ToString() != string.Empty ? dt3.Rows[i]["Distance"].ToString() : "0") + ",";
                columnvalues = columnvalues + (dt3.Rows[i]["GstStateCode"].ToString() != string.Empty ? dt3.Rows[i]["GstStateCode"].ToString() : "0") + ",";
                columnvalues = columnvalues + cityid + "";
                columnvalues = columnvalues + "),";
                cityid = cityid + 1;
                columnvalues = columnvalues.Remove(columnvalues.Length - 1);

                string insert = "insert  into nt_1_citymaster (" + columnname + ") values " + columnvalues;

                SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon);
                MySQLcmd1.ExecuteNonQuery();
                columnvalues = string.Empty;
                // transaction.Commit();
            }
            DataSet ds = new DataSet();
            //if (dt3.Rows.Count > 0)
            //{
            //    columnvalues = columnvalues.Remove(columnvalues.Length - 1);

            //    string insert = "insert  into nt_1_citymaster (" + columnname + ") values " + columnvalues;

            //    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
            //    MySQLcmd1.ExecuteNonQuery();
            //}
            //if (dt.Rows.Count <= 0)
            //{
            //    string maxcount = clsCommon.getString("select max(city_code)+200 from nt_1_citymaster");
            //    string inserttemp = "insert  into nt_1_citymaster (" + columnname + ") values (" + maxcount + " ,'Temp Record','0','',''," + Session["Company_Code"] + ",'Maharashtra','','',0,27 ," + cityid + ")";
            //    //ds = clsDAL.SimpleQuery(inserttemp);
            //    SqlCommand MySQLcmd1 = new SqlCommand(inserttemp, mysqcon, transaction);
            //    MySQLcmd1.ExecuteNonQuery();
            //}
            // transaction.Commit();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('City Master Migration is done!');", true);

        }
        catch
        {
            transaction.Rollback();
        }
        finally
        {
            mysqcon.Close();
        }
    }



    protected void btnGroupMaster_Click(object sender, EventArgs e)
    {
        string cityqrymysql = "select * from nt_1_bsgroupmaster";
        SqlCommand cmd = new SqlCommand(cityqrymysql, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(cityqrymysql, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);

        string cityqrysql = "select * from nt_1_bsgroupmaster";
        SqlCommand cmdsql = new SqlCommand(cityqrysql, sqlcon);
        SqlDataAdapter returnValsql = new SqlDataAdapter(cityqrysql, sqlcon);
        DataTable dtsql = new DataTable();
        returnValsql.Fill(dtsql);

        DataTable dt3 = new DataTable();

        dt3.Columns.Add((new DataColumn("group_Code", typeof(int))));
        dt3.Columns.Add((new DataColumn("group_Name_E", typeof(string))));
        dt3.Columns.Add((new DataColumn("group_Name_R", typeof(string))));
        dt3.Columns.Add((new DataColumn("group_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("group_Summary", typeof(string))));
        dt3.Columns.Add((new DataColumn("group_Order", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("bsid", typeof(string))));


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {

            DataRow[] dr = dt.Select("group_Code = " + dtsql.Rows[i]["group_Code"].ToString());

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }


        string columnname = "group_Code,group_Name_E,group_Name_R,group_Type,group_Summary,group_Order,Company_Code,Created_By,Modified_By,bsid";
        string columnvalues = "";
        int cityid = 1;
        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([bsid])", string.Empty));
            cityid = maxLavel + 1;
        }

        for (int i = 0; i < dt3.Rows.Count; i++)
        {

            columnvalues = columnvalues + "(";
            columnvalues = columnvalues + dt3.Rows[i]["group_Code"].ToString() + ",";
            string cityname = dt3.Rows[i]["group_Name_E"].ToString();
            cityname = cityname.Replace("'", "");
            cityname = cityname.Replace("(", "");
            cityname = cityname.Replace(")", "");
            columnvalues = columnvalues + "'" + cityname + "'" + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["group_Name_R"].ToString() + "'" + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["group_Type"].ToString() + "'" + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["group_Summary"].ToString() + "'" + ",";
            columnvalues = columnvalues + dt3.Rows[i]["group_Order"].ToString() + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["Company_Code"].ToString() + "'" + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["Created_By"].ToString() + "'" + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["Modified_By"].ToString() + "'" + ",";


            columnvalues = columnvalues + cityid + "";
            columnvalues = columnvalues + "),";
            cityid = cityid + 1;
        }
        DataSet ds = new DataSet();
        if (dt3.Rows.Count > 0)
        {
            columnvalues = columnvalues.Remove(columnvalues.Length - 1);

            string insert = "insert  into nt_1_bsgroupmaster (" + columnname + ") values " + columnvalues;

            ds = clsDAL.SimpleQuery(insert);
        }
        if (dt.Rows.Count <= 0)
        {
            string maxcount = clsCommon.getString("select max(group_Code)+200 from nt_1_bsgroupmaster");
            string inserttemp = "insert  into nt_1_bsgroupmaster (" + columnname + ") values (" + maxcount + " ,'Temp Record','','','',0,"
                + Session["Company_Code"] + ",'',''," + cityid + ")";
            ds = clsDAL.SimpleQuery(inserttemp);
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Group Master Migration is done!');", true);
    }

    protected void btnaccountmaster_Click(object sender, EventArgs e)
    {

        string cityqrymysql = "select * from nt_1_accountmaster";
        SqlCommand cmd = new SqlCommand(cityqrymysql, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(cityqrymysql, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);


        string groupmaster = "select * from nt_1_bsgroupmaster";
        SqlCommand cmdgrp = new SqlCommand(groupmaster, mysqcon);
        SqlDataAdapter returnValgrp = new SqlDataAdapter(groupmaster, mysqcon);
        DataTable dtgroup = new DataTable();
        returnValgrp.Fill(dtgroup);

        string citymaster = "select * from nt_1_citymaster";
        SqlCommand cmdcitymaster = new SqlCommand(citymaster, mysqcon);
        SqlDataAdapter returnValcitymaster = new SqlDataAdapter(citymaster, mysqcon);
        DataTable dtcitymaster = new DataTable();
        returnValcitymaster.Fill(dtcitymaster);

        string cityqrysql = "select Ac_Code,Ac_Name_E,Ac_Name_R,Ac_type,Ac_rate,Address_E,Address_R,isnull(City_Code,0) as City_Code,isnull (Pincode,0) as Pincode ,isnull (Local_Lic_No,'0')as Local_Lic_No ,isnull (Tin_No,0) as Tin_No ,isnull (Cst_no,0)as Cst_no ,isnull(Gst_No,0)as Gst_No ,isnull (Email_Id,0)as Email_Id , isnull (Email_Id_cc,0)as Email_Id_cc, isnull (Other_Narration,0)as Other_Narration ,isnull (ECC_No,0)as ECC_No ,isnull (Bank_Name,0)as Bank_Name,isnull (Bank_Ac_No,0)as Bank_Ac_No ,isnull (Bank_Opening,0)as Bank_Opening, isnull (bank_Op_Drcr,0)as bank_Op_Drcr,isnull (Opening_Balance,0)as Opening_Balance, isnull (Drcr,0)as Drcr ,isnull (Group_Code,0)as Group_Code ,isnull (Created_By,0)as Created_By ,isnull (Modified_By,0)as Modified_By ,isnull (Short_Name,0)as Short_Name,   isnull (Commission,0)as Commission,isnull (carporate_party,0)as carporate_party , isnull (referBy,0)as referBy ,isnull (OffPhone,0)as OffPhone ,isnull (Fax,0)as Fax, isnull (CompanyPan,0)as CompanyPan ,isnull (AC_Pan,0)as AC_Pan , isnull (Mobile_No,0)as Mobile_No ,isnull (Is_Login,0)as Is_Login, isnull (IFSC,0)as IFSC ,isnull (FSSAI,0)as FSSAI ,isnull (Branch1OB,0)as Branch1OB,isnull (Branch2OB,0)as Branch2OB,isnull (Branch1Drcr,0)as Branch1Drcr,isnull (Branch2Drcr,0) as Branch2Drcr,isnull (Locked,0)as Locked ,isnull(GSTStateCode,27) as GSTStateCode,isnull ( UnregisterGST,0)as UnregisterGST, isnull (Distance,0)as Distance ,isnull (Bal_Limit,0)as Bal_Limit ,company_code from NT_1_AccountMaster";
        SqlCommand cmdsql = new SqlCommand(cityqrysql, sqlcon);
        SqlDataAdapter returnValsql = new SqlDataAdapter(cityqrysql, sqlcon);
        DataTable dtsql = new DataTable();
        returnValsql.Fill(dtsql);

        DataTable dt3 = new DataTable();

        dt3.Columns.Add((new DataColumn("Ac_Code", typeof(int))));
        dt3.Columns.Add((new DataColumn("Ac_Name_E", typeof(string))));
        dt3.Columns.Add((new DataColumn("Ac_Name_R", typeof(string))));
        dt3.Columns.Add((new DataColumn("Ac_type", typeof(string))));
        dt3.Columns.Add((new DataColumn("Ac_rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("Address_E", typeof(string))));
        dt3.Columns.Add((new DataColumn("Address_R", typeof(string))));
        dt3.Columns.Add((new DataColumn("City_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Pincode", typeof(string))));
        dt3.Columns.Add((new DataColumn("Local_Lic_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tin_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Cst_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("Gst_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Email_Id", typeof(string))));
        dt3.Columns.Add((new DataColumn("Email_Id_cc", typeof(string))));
        dt3.Columns.Add((new DataColumn("Other_Narration", typeof(string))));
        dt3.Columns.Add((new DataColumn("ECC_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Bank_Name", typeof(string))));
        dt3.Columns.Add((new DataColumn("Bank_Ac_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Bank_Opening", typeof(string))));
        dt3.Columns.Add((new DataColumn("bank_Op_Drcr", typeof(string))));
        dt3.Columns.Add((new DataColumn("Opening_Balance", typeof(string))));
        dt3.Columns.Add((new DataColumn("Drcr", typeof(string))));
        dt3.Columns.Add((new DataColumn("Group_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Short_Name", typeof(string))));
        dt3.Columns.Add((new DataColumn("Commission", typeof(string))));
        dt3.Columns.Add((new DataColumn("carporate_party", typeof(string))));
        dt3.Columns.Add((new DataColumn("referBy", typeof(string))));
        dt3.Columns.Add((new DataColumn("OffPhone", typeof(string))));
        dt3.Columns.Add((new DataColumn("Fax", typeof(string))));
        dt3.Columns.Add((new DataColumn("CompanyPan", typeof(string))));
        dt3.Columns.Add((new DataColumn("AC_Pan", typeof(string))));
        dt3.Columns.Add((new DataColumn("Mobile_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Is_Login", typeof(string))));
        dt3.Columns.Add((new DataColumn("IFSC", typeof(string))));
        dt3.Columns.Add((new DataColumn("FSSAI", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch1OB", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch2OB", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch1Drcr", typeof(string))));

        dt3.Columns.Add((new DataColumn("Branch2Drcr", typeof(string))));
        dt3.Columns.Add((new DataColumn("Locked", typeof(string))));
        dt3.Columns.Add((new DataColumn("GSTStateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("UnregisterGST", typeof(string))));
        dt3.Columns.Add((new DataColumn("Distance", typeof(string))));
        dt3.Columns.Add((new DataColumn("Bal_Limit", typeof(string))));
        dt3.Columns.Add((new DataColumn("accoid", typeof(string))));
        dt3.Columns.Add((new DataColumn("bsid", typeof(string))));
        dt3.Columns.Add((new DataColumn("cityid", typeof(string))));
        dt3.Columns.Add((new DataColumn("whatsup_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("company_code", typeof(string))));



        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string groupcode = dtsql.Rows[i]["Group_Code"].ToString();
            string citycode = dtsql.Rows[i]["city_code"].ToString();

            DataRow[] drbsid = dtgroup.Select("group_Code = " + groupcode + " and Company_Code=" + companycode);
            DataRow[] drcityid = dtcitymaster.Select("city_code = " + citycode);
            string bsid = "1";
            string cityid1 = "1";
            if (drbsid.Length > 0)
            {
                bsid = drbsid[0]["bsid"].ToString();
            }
            if (drcityid.Length > 0)
            {
                cityid1 = drcityid[0]["cityid"].ToString();
            }
            DataRow[] dr = dt.Select("Ac_Code = " + dtsql.Rows[i]["Ac_Code"].ToString() + " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString());

            if (dr.Length == 0)
            {

                dt3.ImportRow(dtsql.Rows[i]);
            }
        }


        string columnname = "Ac_Code,Ac_Name_E,Ac_Name_R,Ac_type,Ac_rate,Address_E,Address_R,City_Code,Pincode,Local_Lic_No,Tin_No,Cst_no,Gst_No,Email_Id," +
            "Email_Id_cc,Other_Narration,ECC_No,Bank_Name,Bank_Ac_No,Bank_Opening,bank_Op_Drcr,Opening_Balance,Drcr,Group_Code,Created_By,Modified_By,Short_Name," +
            "Commission,carporate_party,referBy,OffPhone,Fax,CompanyPan,AC_Pan,Mobile_No,Is_Login,IFSC,FSSAI,Branch1OB,Branch2OB,Branch1Drcr,Branch2Drcr,Locked,GSTStateCode," +
            "UnregisterGST,Distance,Bal_Limit,bsid,cityid,whatsup_no,accoid,company_code";
        StringBuilder columnvalues = new StringBuilder();
        int cityid = 1;
        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([accoid])", string.Empty));
            cityid = maxLavel + 1;
        }
        DataSet ds = new DataSet();
        #region
        if (dt3.Rows.Count > 0)
        {
            try
            {
                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["company_code"].ToString();
                    string groupcode = dt3.Rows[i]["Group_Code"].ToString();
                    string citycode = dtsql.Rows[i]["city_code"].ToString();
                    DataRow[] drbsid = dtgroup.Select("group_Code = " + groupcode + " and Company_Code=" + companycode);
                    DataRow[] drcityid = dtcitymaster.Select("city_code = " + citycode);
                    string bsid = "1";
                    string cityid1 = "1";
                    if (drbsid.Length > 0)
                    {
                        bsid = drbsid[0]["bsid"].ToString();
                    }
                    if (drcityid.Length > 0)
                    {
                        cityid1 = drcityid[0]["cityid"].ToString();
                    }
                    columnvalues.Clear();
                    //columnvalues = columnvalues + "(";
                    columnvalues.Append("(");
                    columnvalues.Append(dt3.Rows[i]["Ac_Code"].ToString() + ",");
                    string acname = dt3.Rows[i]["Ac_Name_E"].ToString();
                    acname = acname.Replace("'", "");
                    columnvalues.Append("'" + acname + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Ac_Name_R"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Ac_type"].ToString() + "',");

                    columnvalues.Append(dt3.Rows[i]["Ac_rate"].ToString() + ",");
                    string Address_E = dt3.Rows[i]["Address_E"].ToString();
                    Address_E = Address_E.Replace("'", "");
                    columnvalues.Append("'" + Address_E + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Address_R"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["City_Code"].ToString() + "',");

                    columnvalues.Append("'" + dt3.Rows[i]["Pincode"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Local_Lic_No"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Tin_No"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Cst_no"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Gst_No"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Email_Id"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Email_Id_cc"].ToString() + "',");
                    string Other_Narration = dt3.Rows[i]["Other_Narration"].ToString();
                    Other_Narration = Other_Narration.Replace("'", "");
                    columnvalues.Append("'" + Other_Narration + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["ECC_No"].ToString() + "',");
                    string Bank_Name = dt3.Rows[i]["Bank_Name"].ToString();
                    Bank_Name = Bank_Name.Replace("'", "");
                    columnvalues.Append("'" + Bank_Name + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["ECC_No"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["Bank_Opening"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["bank_Op_Drcr"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["Opening_Balance"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["Drcr"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["Group_Code"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    string Short_Name = dt3.Rows[i]["Short_Name"].ToString();
                    Short_Name = Short_Name.Replace("'", "");
                    columnvalues.Append("'" + Short_Name + "',");
                    columnvalues.Append(dt3.Rows[i]["Commission"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["carporate_party"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["referBy"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["OffPhone"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Fax"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["CompanyPan"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["AC_Pan"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Mobile_No"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Is_Login"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["IFSC"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["FSSAI"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["Branch1OB"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Branch2OB"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["Branch1Drcr"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Branch2Drcr"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Locked"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["GSTStateCode"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["UnregisterGST"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["Distance"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Bal_Limit"].ToString() + ",");
                    columnvalues.Append(bsid + ",");
                    columnvalues.Append(cityid1 + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["whatsup_no"].ToString() + "',");
                    columnvalues.Append(cityid + ",");
                    columnvalues.Append(dt3.Rows[i]["company_code"].ToString());

                    columnvalues.Append(")");

                    cityid = cityid + 1;
                    string insert = "insert  into nt_1_accountmaster (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    columnvalues = new StringBuilder();
                    //  ds = clsDAL.SimpleQuery(insert);
                }


                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Account Master Migration is done!');", true);
            }


            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                mysqcon.Close();
            }
        }
        #endregion


        if (dt.Rows.Count <= 0)
        {
            columnname = "Ac_Code,Ac_Name_E,Ac_type,Address_R,accoid,company_code";

            string maxcount = clsCommon.getString("select max(Ac_Code)+200 from nt_1_accountmaster");
            string inserttemp = "insert  into nt_1_accountmaster (" + columnname + ") values (" + maxcount + ",'Temp Record','P',''," + cityid + ",1)";

            ds = clsDAL.SimpleQuery(inserttemp);
        }


        ////Migrate account master details

        string NT_1_AcContacts = "select * from nt_1_accontacts";
        SqlCommand cmdNT_1_AcContacts = new SqlCommand(NT_1_AcContacts, mysqcon);
        SqlDataAdapter returnValNT_1_AcContacts = new SqlDataAdapter(NT_1_AcContacts, mysqcon);
        DataTable dtNT_1_AcContacts = new DataTable();
        returnValNT_1_AcContacts.Fill(dtNT_1_AcContacts);

        string cityqrysqldetails = "select * from NT_1_AcContacts";
        SqlCommand cmdsqldetail = new SqlCommand(cityqrysqldetails, sqlcon);
        SqlDataAdapter returnValsqldetail = new SqlDataAdapter(cityqrysqldetails, sqlcon);
        DataTable dtsqldetail = new DataTable();
        returnValsqldetail.Fill(dtsqldetail);

        dt.Clear();
        cityqrymysql = "select * from nt_1_accountmaster";
        cmd = new SqlCommand(cityqrymysql, mysqcon);
        returnVal = new SqlDataAdapter(cityqrymysql, mysqcon);
        dt = new DataTable();
        returnVal.Fill(dt);

        DataTable dtdetails = new DataTable();

        dtdetails.Columns.Add((new DataColumn("PersonId", typeof(int))));
        dtdetails.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dtdetails.Columns.Add((new DataColumn("Ac_Code", typeof(string))));
        dtdetails.Columns.Add((new DataColumn("Person_Name", typeof(string))));
        dtdetails.Columns.Add((new DataColumn("Person_Mobile", typeof(string))));
        dtdetails.Columns.Add((new DataColumn("Person_Email", typeof(string))));
        dtdetails.Columns.Add((new DataColumn("Person_Pan", typeof(string))));
        dtdetails.Columns.Add((new DataColumn("Other", typeof(string))));
        dtdetails.Columns.Add((new DataColumn("accoid", typeof(string))));


        for (int i = 0; i < dtsqldetail.Rows.Count; i++)
        {
            string companycode = dtsqldetail.Rows[i]["Company_Code"].ToString();

            DataRow[] dr = dtNT_1_AcContacts.Select("PersonId = " + dtsqldetail.Rows[i]["PersonId"].ToString() + " and Company_code="
                + dtsqldetail.Rows[i]["Company_Code"].ToString() + " and Ac_Code=" + dtsqldetail.Rows[i]["Ac_Code"].ToString());

            if (dr.Length == 0)
            {

                dtdetails.ImportRow(dtsqldetail.Rows[i]);
            }
        }
        string columnnamedetail = "PersonId,Company_Code,Ac_Code,Person_Name,Person_Mobile,Person_Email,Person_Pan,Other,accoid";
        StringBuilder columnvaluesdetail = new StringBuilder();

        //int maxLaveldetails;
        //if (dtNT_1_AcContacts.Rows.Count > 0)
        //{
        //    maxLaveldetails = Convert.ToInt32(dt.Compute("max([accoid])", string.Empty));
        //    cityid = maxLaveldetails + 1;
        //}

        if (dtdetails.Rows.Count > 0)
        {
            try
            {
                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dtdetails.Rows.Count; i++)
                {
                    string companycode = dtdetails.Rows[i]["Company_Code"].ToString();
                    string groupcode = dtdetails.Rows[i]["Ac_Code"].ToString();

                    DataRow[] drbsid = dt.Select("Ac_Code = " + groupcode + " and Company_Code=" + companycode);
                    string accoid = "1";
                    if (drbsid.Length > 0)
                    {
                        accoid = drbsid[0]["accoid"].ToString();
                    }

                    columnvaluesdetail.Clear();
                    //columnvalues = columnvalues + "(";
                    columnvaluesdetail.Append("(");
                    columnvaluesdetail.Append(dtdetails.Rows[i]["PersonId"].ToString() + ",");
                    columnvaluesdetail.Append(dtdetails.Rows[i]["Company_Code"].ToString() + ",");

                    columnvaluesdetail.Append(dtdetails.Rows[i]["Ac_Code"].ToString() + ",");
                    string acname = dtdetails.Rows[i]["Person_Name"].ToString();

                    acname = acname.Replace("'", "");
                    columnvaluesdetail.Append("'" + acname + "',");
                    columnvaluesdetail.Append("'" + dtdetails.Rows[i]["Person_Mobile"].ToString() + "',");
                    columnvaluesdetail.Append("'" + dtdetails.Rows[i]["Person_Email"].ToString() + "',");
                    columnvaluesdetail.Append("'" + dtdetails.Rows[i]["Person_Pan"].ToString() + "',");
                    columnvaluesdetail.Append("'" + dtdetails.Rows[i]["Other"].ToString() + "',");
                    columnvaluesdetail.Append(accoid);


                    columnvaluesdetail.Append(")");




                    string insert = "insert  into nt_1_accontacts (" + columnnamedetail + ") values " + columnvaluesdetail;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();

                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Account Master Migration is done!');", true);

            }


            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                mysqcon.Close();
            }

        }


        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Account Master Migration is done!');", true);
    }
    protected void btnpartyunit_Click(object sender, EventArgs e)
    {
        string cityqrymysql = "select * from nt_1_partyunit";
        SqlCommand cmd = new SqlCommand(cityqrymysql, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(cityqrymysql, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);

        string cityqrysql = "select * from nt_1_partyunit";
        SqlCommand cmdsql = new SqlCommand(cityqrysql, sqlcon);
        SqlDataAdapter returnValsql = new SqlDataAdapter(cityqrysql, sqlcon);
        DataTable dtsql = new DataTable();
        returnValsql.Fill(dtsql);

        string acc = "select * from nt_1_accountmaster";
        SqlCommand cmdacc = new SqlCommand(acc, mysqcon);
        SqlDataAdapter returnValacc = new SqlDataAdapter(acc, mysqcon);
        DataTable dtacc = new DataTable();
        returnValacc.Fill(dtacc);

        DataTable dt3 = new DataTable();

        dt3.Columns.Add((new DataColumn("unit_code", typeof(int))));
        dt3.Columns.Add((new DataColumn("Ac_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Unit_name", typeof(string))));
        dt3.Columns.Add((new DataColumn("Remarks", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));

        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("uc", typeof(string))));
        dt3.Columns.Add((new DataColumn("ucid", typeof(string))));


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {

            DataRow[] dr = dt.Select("unit_code = " + dtsql.Rows[i]["unit_code"].ToString() + " and Company_Code=" + dtsql.Rows[i]["Company_Code"].ToString() +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString());

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }


        string columnname = "unit_code,Ac_Code,Unit_name,Remarks,Company_Code,Year_Code,Created_By,Modified_By,ac,uc,ucid";
        string columnvalues = "";
        int cityid = 1;
        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([ucid])", string.Empty));
            cityid = maxLavel + 1;
        }
        if (dt3.Rows.Count > 0)
        {
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                string companycode = dt3.Rows[i]["Company_Code"].ToString();
                string groupcode = dt3.Rows[i]["Ac_Code"].ToString();
                string unitcode = dt3.Rows[i]["Unit_name"].ToString();

                DataRow[] drbsid = dtacc.Select("Ac_Code = " + groupcode + " and Company_Code=" + companycode);
                string accoid = "1";
                if (drbsid.Length > 0)
                {
                    accoid = drbsid[0]["accoid"].ToString();
                }

                DataRow[] drbsidunit = dtacc.Select("Ac_Code = " + unitcode + " and Company_Code=" + companycode);
                string accoidunit = "1";
                if (drbsidunit.Length > 0)
                {
                    accoidunit = drbsidunit[0]["accoid"].ToString();
                }
                columnvalues = columnvalues + "(";
                columnvalues = columnvalues + dt3.Rows[i]["unit_code"].ToString() + ",";
                columnvalues = columnvalues + "'" + dt3.Rows[i]["Ac_Code"].ToString() + "'" + ",";
                columnvalues = columnvalues + "'" + dt3.Rows[i]["Unit_name"].ToString() + "'" + ",";
                columnvalues = columnvalues + "'" + dt3.Rows[i]["Remarks"].ToString() + "'" + ",";
                columnvalues = columnvalues + "'" + dt3.Rows[i]["Company_Code"].ToString() + "'" + ",";
                columnvalues = columnvalues + dt3.Rows[i]["Year_Code"].ToString() + ",";
                columnvalues = columnvalues + "'" + dt3.Rows[i]["Created_By"].ToString() + "'" + ",";
                columnvalues = columnvalues + "'" + dt3.Rows[i]["Modified_By"].ToString() + "'" + ",";
                columnvalues = columnvalues + accoid + ",";
                columnvalues = columnvalues + accoidunit + ",";



                columnvalues = columnvalues + cityid + "";
                columnvalues = columnvalues + "),";
                cityid = cityid + 1;
            }
            DataSet ds = new DataSet();
            if (dt3.Rows.Count > 0)
            {
                columnvalues = columnvalues.Remove(columnvalues.Length - 1);

                string insert = "insert  into nt_1_partyunit (" + columnname + ") values " + columnvalues;

                ds = clsDAL.SimpleQuery(insert);
            }
        }
        //if (dt.Rows.Count <= 0)
        //{
        //    string maxcount = clsCommon.getString("select max(group_Code)+200 from nt_1_bsgroupmaster");
        //    string inserttemp = "insert  into nt_1_bsgroupmaster (" + columnname + ") values (" + maxcount + " ,'Temp Record','','','',0,"
        //        + Session["Company_Code"] + ",'',''," + cityid + ")";
        //    ds = clsDAL.SimpleQuery(inserttemp);
        //}
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Party Unit Migration is done!');", true);
    }
    protected void btnuser_Click(object sender, EventArgs e)
    {
        string cityqrymysql = "select * from tbluser";
        SqlCommand cmd = new SqlCommand(cityqrymysql, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(cityqrymysql, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);

        string cityqrysql = "select * from tblUser";
        SqlCommand cmdsql = new SqlCommand(cityqrysql, sqlcon);
        SqlDataAdapter returnValsql = new SqlDataAdapter(cityqrysql, sqlcon);
        DataTable dtsql = new DataTable();
        returnValsql.Fill(dtsql);

        DataTable dt3 = new DataTable();
        mysqcon.Open();
        dt3.Columns.Add((new DataColumn("User_Id", typeof(int))));
        dt3.Columns.Add((new DataColumn("User_Name", typeof(string))));
        dt3.Columns.Add((new DataColumn("Password", typeof(string))));
        dt3.Columns.Add((new DataColumn("User_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("EmailId", typeof(string))));
        dt3.Columns.Add((new DataColumn("EmailPassword", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Mobile", typeof(string))));
        dt3.Columns.Add((new DataColumn("uid", typeof(string))));
        // dt3.Columns.Add((new DataColumn("bsid", typeof(string))));


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {

            DataRow[] dr = dt.Select("User_Id = " + dtsql.Rows[i]["User_Id"].ToString());

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }


        string columnname = "User_Id,User_Name,Password,User_Type,EmailId,EmailPassword,Company_Code,Mobile,uid";
        string columnvalues = "";
        int cityid = 1;
        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([uid])", string.Empty));
            cityid = maxLavel + 1;
        }
        EncryptPass enc = new EncryptPass();
        for (int i = 0; i < dt3.Rows.Count; i++)
        {

            columnvalues = columnvalues + "(";
            columnvalues = columnvalues + dt3.Rows[i]["User_Id"].ToString() + ",";
            string cityname = dt3.Rows[i]["User_Name"].ToString();
            cityname = cityname.Replace("'", "");
            cityname = cityname.Replace("(", "");
            cityname = cityname.Replace(")", "");
            columnvalues = columnvalues + "'" + cityname + "'" + ",";
            columnvalues = columnvalues + "'" + enc.Encrypt(dt3.Rows[i]["Password"].ToString()) + "'" + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["User_Type"].ToString() + "'" + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["EmailId"].ToString() + "'" + ",";
            columnvalues = columnvalues + "'" + enc.Encrypt(dt3.Rows[i]["EmailPassword"].ToString()) + "'" + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["Company_Code"].ToString() + "'" + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["Mobile"].ToString() + "'" + ",";
            // columnvalues = columnvalues + "'" + dt3.Rows[i]["Modified_By"].ToString() + "'" + ",";


            columnvalues = columnvalues + cityid + "";
            columnvalues = columnvalues + ")";
            cityid = cityid + 1;
            string insert = "insert  into tbluser (" + columnname + ") values " + columnvalues;

            SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon);
            MySQLcmd1.ExecuteNonQuery();
            columnvalues = string.Empty;
        }
        DataSet ds = new DataSet();
        //if (dt3.Rows.Count > 0)
        //{
        //    columnvalues = columnvalues.Remove(columnvalues.Length - 1);

        //    string insert = "insert  into tblUser (" + columnname + ") values " + columnvalues;

        //    ds = clsDAL.SimpleQuery(insert);
        //}

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(' Migration is done!');", true);
    }
    protected void btnitemmaster_Click(object sender, EventArgs e)
    {
        string itemmastermysql = "select * from nt_1_systemmaster";
        SqlCommand cmd = new SqlCommand(itemmastermysql, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(itemmastermysql, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);

        string cityqrysql = "select * from NT_1_SystemMaster";
        SqlCommand cmdsql = new SqlCommand(cityqrysql, sqlcon);
        SqlDataAdapter returnValsql = new SqlDataAdapter(cityqrysql, sqlcon);
        DataTable dtsql = new DataTable();
        returnValsql.Fill(dtsql);

        DataTable dt3 = new DataTable();
        mysqcon.Open();
        dt3.Columns.Add((new DataColumn("System_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("System_Code", typeof(int))));
        dt3.Columns.Add((new DataColumn("System_Name_E", typeof(string))));
        dt3.Columns.Add((new DataColumn("System_Name_R", typeof(string))));
        dt3.Columns.Add((new DataColumn("System_Rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("Purchase_AC", typeof(string))));
        dt3.Columns.Add((new DataColumn("Sale_AC", typeof(string))));
        dt3.Columns.Add((new DataColumn("Vat_AC", typeof(string))));
        dt3.Columns.Add((new DataColumn("Opening_Bal", typeof(string))));
        dt3.Columns.Add((new DataColumn("KgPerKatta", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));

        dt3.Columns.Add((new DataColumn("HSN", typeof(string))));
        dt3.Columns.Add((new DataColumn("systemid", typeof(string))));
        dt3.Columns.Add((new DataColumn("Opening_Value", typeof(string))));

        for (int i = 0; i < dtsql.Rows.Count; i++)
        {

            DataRow[] dr = dt.Select("System_Type = '" + dtsql.Rows[i]["System_Type"].ToString() + "' and System_Code=" + dtsql.Rows[i]["System_Code"].ToString());

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }
        string columnname = "System_Type,System_Code,System_Name_E,System_Name_R,System_Rate,Purchase_AC,Sale_AC,Vat_AC,Opening_Bal,KgPerKatta,Company_Code,Year_Code,HSN,Opening_Value,systemid";
        string columnvalues = "";
        int cityid = 1;
        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([systemid])", string.Empty));
            cityid = maxLavel + 1;
        }
        EncryptPass enc = new EncryptPass();
        for (int i = 0; i < dt3.Rows.Count; i++)
        {

            columnvalues = columnvalues + "(";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["System_Type"].ToString() + "'" + ",";

            columnvalues = columnvalues + dt3.Rows[i]["System_Code"].ToString() + ",";
            string cityname = dt3.Rows[i]["System_Name_E"].ToString();
            cityname = cityname.Replace("'", "");
            cityname = cityname.Replace("(", "");
            cityname = cityname.Replace(")", "");
            columnvalues = columnvalues + "'" + cityname + "'" + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["System_Name_R"].ToString() + "'" + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["System_Rate"].ToString() + "'" + ",";
            columnvalues = columnvalues + (dt3.Rows[i]["Purchase_AC"].ToString() != string.Empty ? dt3.Rows[i]["Purchase_AC"].ToString() : "0.00") + ",";
            columnvalues = columnvalues + (dt3.Rows[i]["Sale_AC"].ToString() != string.Empty ? dt3.Rows[i]["Sale_AC"].ToString() : "0.00") + ",";
            columnvalues = columnvalues + (dt3.Rows[i]["Vat_AC"].ToString() != string.Empty ? dt3.Rows[i]["Vat_AC"].ToString() : "0.00") + ",";
            columnvalues = columnvalues + (dt3.Rows[i]["Opening_Bal"].ToString() != string.Empty ? dt3.Rows[i]["Opening_Bal"].ToString() : "0.00") + ",";
            columnvalues = columnvalues + (dt3.Rows[i]["KgPerKatta"].ToString() != string.Empty ? dt3.Rows[i]["KgPerKatta"].ToString() : "0.00") + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["Company_Code"].ToString() + "'" + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["Year_Code"].ToString() + "'" + ",";
            columnvalues = columnvalues + "'" + dt3.Rows[i]["HSN"].ToString() + "'" + ",";
            columnvalues = columnvalues + (dt3.Rows[i]["Opening_Value"].ToString() != string.Empty ? dt3.Rows[i]["Opening_Value"].ToString() : "0.00") + ",";


            columnvalues = columnvalues + cityid + "";
            columnvalues = columnvalues + "),";
            cityid = cityid + 1;
            columnvalues = columnvalues.Remove(columnvalues.Length - 1);

            string insert = "insert  into NT_1_SystemMaster (" + columnname + ") values " + columnvalues;

            SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon);
            MySQLcmd1.ExecuteNonQuery();
            columnvalues = string.Empty;
        }
        DataSet ds = new DataSet();
        //if (dt3.Rows.Count > 0)
        //{
        //    columnvalues = columnvalues.Remove(columnvalues.Length - 1);

        //    string insert = "insert  into NT_1_SystemMaster (" + columnname + ") values " + columnvalues;

        //    ds = clsDAL.SimpleQuery(insert);
        //}

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert(' Migration is done!');", true);


    }
    protected void btnupdatecitygst_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        string nullcity = "select * from NT_1_CityMaster where city_code!=0 and GSTStateCode=0";
        SqlCommand nullcity1 = new SqlCommand(nullcity, mysqcon);
        SqlDataAdapter nullcityda = new SqlDataAdapter(nullcity, mysqcon);
        DataTable dtnulllcity = new DataTable();
        nullcityda.Fill(dtnulllcity);

        string acc = "select distinct City_Code,company_code,GSTStateCode from nt_1_accountmaster  where city_code!=0 and GSTStateCode!=0 group by City_Code,company_code order by city_code asc";
        SqlCommand acccmd = new SqlCommand(acc, mysqcon);
        SqlDataAdapter accda = new SqlDataAdapter(acc, mysqcon);
        //DataTable dtnulllcity = new DataTable();
        accda.Fill(dt);
        //dt = dt.AsEnumerable()
        //             .GroupBy(r => new { Itemid = r.Field<int>("City_Code"), PacktypeId = r.Field<int>("company_code"),
        //                 Packtypegst = r.Field<int>("GSTStateCode") })
        //             .Select(g => g.First())
        //             .CopyToDataTable();

        for (int i = 0; i < 2; i++)
        {
            int companycode1 = Convert.ToInt32(dtnulllcity.Rows[i]["company_code"].ToString());
            int groupcode1 = Convert.ToInt32(dtnulllcity.Rows[i]["City_Code"].ToString());
            string GSTStateCode = dtnulllcity.Rows[i]["GSTStateCode"].ToString();
            DataTable dtnew = new DataTable();

            //dtnew = dt.AsEnumerable().Where(row => Convert.ToInt32(row["City_Code"]) == groupcode1 && 
            //    Convert.ToInt32(row["company_code"]) == companycode1)
            //    .Select(row => row.Field<string>("GSTStateCode"));


            //var ss = dt.AsEnumerable()
            //             .GroupBy(r => new
            //             {
            //                 Itemid = r.Field<int>("City_Code"),
            //                 PacktypeId = r.Field<int>("company_code"),
            //                 Packtypegst = r.Field<int>("GSTStateCode")
            //             })
            //            . Where(row => Convert.ToInt32(row["City_Code"] == groupcode1 && 
            // row["company_code"] == companycode1))
            //             .Select(g => g.First())
            //             .CopyToDataTable();

            //name=dt.Copy();
            //dt.DefaultView.Sort = "GSTStateCode desc";
            //dt = dt.DefaultView.ToTable();
            DataRow[] drbsid = dt.Select("City_Code = " + groupcode1 + " and Company_Code=" + companycode1);
            string accoid = "0";
            if (drbsid.Length > 0)
            {
                accoid = drbsid[0]["GSTStateCode"].ToString();
            }

            string column = "";
            column = column + " up  date NT_1_CityMaster set " + " GSTStateCode=" + accoid + " where city_code=" + groupcode1 +
                " and company_code=" + companycode1;
            // string insert = "update  nt_1_accontacts set;

            string qry = clsDAL.GetString(column);

        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('is done!');", true);



    }
    protected void btnpssbid_Click(object sender, EventArgs e)
    {
        string cityqrymysql = "select * from qrydohead";
        SqlCommand cmd = new SqlCommand(cityqrymysql, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(cityqrymysql, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);

        string cityqrymysql1 = "select * from nt_1_sugarpurchase";
        SqlCommand cmd1 = new SqlCommand(cityqrymysql1, mysqcon);
        SqlDataAdapter returnVal1 = new SqlDataAdapter(cityqrymysql1, mysqcon);
        DataTable dt1 = new DataTable();
        returnVal1.Fill(dt1);

        string cityqrymysql11 = "select * from nt_1_sugarsale where Purcid is null";
        SqlCommand cmd11 = new SqlCommand(cityqrymysql1, mysqcon);
        SqlDataAdapter returnVal11 = new SqlDataAdapter(cityqrymysql11, mysqcon);
        DataTable dt11 = new DataTable();
        returnVal11.Fill(dt11);


        //   string columnname = "update nt_1_sugarpurchase set =";
        StringBuilder columnvalues = new StringBuilder();

        //mysqcon.ConnectionString = mysqlconn;
        //mysqcon.Open();
        //transaction = mysqcon.BeginTransaction();
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            string companycode = dt1.Rows[i]["PURCNO"].ToString();
            //string groupcode = dt1.Rows[i]["Ac_Code"].ToString();

            DataRow[] drbsid = dt.Select("doc_no = " + companycode);
            string accoid = "0";
            string accoid1 = "0";

            if (drbsid.Length > 0)
            {
                accoid = drbsid[0]["doc_no"].ToString();
                accoid1 = clsCommon.getString("select doid from qrydohead where doc_no=" + accoid);
                string updateqry = clsCommon.getString("update nt_1_sugarpurchase set Purcid=" + accoid1 + " where PURCNO=" + companycode);
            }
        }
        for (int i = 0; i < dt11.Rows.Count; i++)
        {
            string companycode = dt11.Rows[i]["PURCNO"].ToString();
            //string groupcode = dt1.Rows[i]["Ac_Code"].ToString();

            DataRow[] drbsid = dt1.Select("doc_no = " + companycode);
            string accoid = "0";
            string accoid1 = "0";

            if (drbsid.Length > 0)
            {
                accoid = drbsid[0]["doc_no"].ToString();
                accoid1 = clsCommon.getString("select purchaseid from nt_1_sugarpurchase where doc_no=" + accoid);
                string updateqry = clsCommon.getString("update nt_1_sugarsale set Purcid=" + accoid1 + " where PURCNO=" + companycode);
            }
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('is done!');", true);
    }
    protected void btnTender_Click(object sender, EventArgs e)
    {
        #region[Head]
        string Tenderpurchase = "select * from nt_1_tender where  Company_Code=" + drpCompany.SelectedValue + " order by Tender_No";
        SqlCommand cmd = new SqlCommand(Tenderpurchase, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(Tenderpurchase, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);


        string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue + " ";
        SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);


        string Tenderpurchasesql = "select * from nt_1_tender where  Company_Code=" + drpCompany.SelectedValue + " order by Tender_No";
        SqlCommand cmdsql = new SqlCommand(Tenderpurchasesql, sqlcon);

        SqlDataAdapter returnValSQL = new SqlDataAdapter(Tenderpurchasesql, sqlcon);
        DataTable dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        DataTable dt3 = new DataTable();


        dt3.Columns.Add((new DataColumn("Tender_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tender_Date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Lifting_Date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Mill_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Grade", typeof(string))));
        dt3.Columns.Add((new DataColumn("Quantal", typeof(string))));
        dt3.Columns.Add((new DataColumn("Packing", typeof(string))));
        dt3.Columns.Add((new DataColumn("Bags", typeof(string))));
        dt3.Columns.Add((new DataColumn("Payment_To", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tender_From", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tender_DO", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Broker", typeof(string))));
        dt3.Columns.Add((new DataColumn("Excise_Rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("Narration", typeof(string))));
        dt3.Columns.Add((new DataColumn("Mill_Rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Purc_Rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("type", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Id", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Sell_Note_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Brokrage", typeof(string))));
        dt3.Columns.Add((new DataColumn("mc", typeof(string))));
        dt3.Columns.Add((new DataColumn("itemcode", typeof(string))));
        dt3.Columns.Add((new DataColumn("season", typeof(string))));
        dt3.Columns.Add((new DataColumn("pt", typeof(string))));
        dt3.Columns.Add((new DataColumn("tf", typeof(string))));
        dt3.Columns.Add((new DataColumn("td", typeof(string))));
        dt3.Columns.Add((new DataColumn("vb", typeof(string))));
        dt3.Columns.Add((new DataColumn("bk", typeof(string))));
        dt3.Columns.Add((new DataColumn("ic", typeof(string))));
        dt3.Columns.Add((new DataColumn("gstratecode", typeof(string))));
        dt3.Columns.Add((new DataColumn("CashDiff", typeof(string))));
        dt3.Columns.Add((new DataColumn("tenderid", typeof(string))));


        string columnname = "Tender_No,Company_Code,Tender_Date,Lifting_Date,Mill_Code,Grade,Quantal,Packing,Bags,Payment_To,Tender_From,Tender_DO,Voucher_By" +
            ",Broker,Excise_Rate,Narration,Mill_Rate,Created_By,Modified_By,Year_Code,Purc_Rate,type,Branch_Id,Voucher_No,Sell_Note_No,Brokrage,mc" +
            ",itemcode,season,pt,tf,td,vb,bk,ic,gstratecode,CashDiff,tenderid";


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["Tender_No"].ToString();

            DataRow[] dr = dt.Select("Tender_No = " + dtsql.Rows[i]["Tender_No"].ToString() + " " +
                " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString());

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }


        StringBuilder columnvalues = new StringBuilder();
        int cityid = 1;
        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([tenderid])", string.Empty));
            cityid = maxLavel + 1;
        }
        DataSet ds = new DataSet();
        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                    string Mill_Code = dt3.Rows[i]["Mill_Code"].ToString();
                    string Payment_To = dt3.Rows[i]["Payment_To"].ToString();
                    string Tender_From = dt3.Rows[i]["Tender_From"].ToString();
                    string Tender_DO = dt3.Rows[i]["Tender_DO"].ToString();
                    string Voucher_By = dt3.Rows[i]["Voucher_By"].ToString();
                    string Broker = dt3.Rows[i]["Broker"].ToString();

                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Mill_Code + " and Company_Code=" + companycode);
                    string mc = "2";
                    string pt = "2";
                    string tf = "2";
                    string td = "2";
                    string vb = "2";
                    string bk = "2";


                    if (drbsid.Length > 0)
                    {
                        mc = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Payment_To + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        pt = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Tender_From + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        tf = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Tender_DO + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        td = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Voucher_By + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        vb = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Broker + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        bk = drbsid[0]["accoid"].ToString();
                    }
                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append(dt3.Rows[i]["Tender_No"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Company_Code"].ToString() + ",");
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["Tender_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");

                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["Lifting_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    //columnvalues.Append(dt3.Rows[i]["Lifting_Date"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Mill_Code"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["Grade"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["Quantal"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Packing"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Bags"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Payment_To"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Tender_From"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Tender_DO"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Voucher_By"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Broker"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Excise_Rate"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["Narration"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["Mill_Rate"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["Purc_Rate"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["type"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["Branch_Id"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Voucher_No"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Voucher_No"]) : "0.00");
                    columnvalues.Append(",'" + dt3.Rows[i]["Sell_Note_No"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["Brokrage"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Brokrage"]) : "0.00");
                    columnvalues.Append("," + mc + ",");
                    columnvalues.Append("1,");
                    columnvalues.Append("'" + dt3.Rows[i]["season"].ToString() + "',");

                    columnvalues.Append(pt + ",");
                    columnvalues.Append(tf + ",");
                    columnvalues.Append(td + ",");
                    columnvalues.Append(vb + ",");
                    columnvalues.Append(bk + ",");
                    columnvalues.Append("1,");
                    columnvalues.Append("1,");
                    columnvalues.Append(dt3.Rows[i]["CashDiff"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["CashDiff"]) : "0.00");
                    columnvalues.Append("," + cityid + "");
                    columnvalues.Append(")");
                    cityid = cityid + 1;
                    string insert = "insert  into nt_1_tender (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    columnvalues = new StringBuilder();
                    //  ds = clsDAL.SimpleQuery(insert);
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Tender Migration is done!');", true);
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                mysqcon.Close();
            }
        }
        #endregion


        #region[Deatials]
        Tenderpurchase = "select * from nt_1_tenderdetails where  Company_Code=" + drpCompany.SelectedValue + " order by Tender_No";
        cmd = new SqlCommand(Tenderpurchase, mysqcon);
        returnVal = new SqlDataAdapter(Tenderpurchase, mysqcon);
        dt = new DataTable();
        returnVal.Fill(dt);

        string Tenderpurchaseselect = "select * from nt_1_tender where Company_Code=" + drpCompany.SelectedValue + " order by Tender_No";
        SqlCommand cmdTenderpurchaseselect = new SqlCommand(Tenderpurchaseselect, mysqcon);
        SqlDataAdapter returnValTenderpurchaseselect = new SqlDataAdapter(Tenderpurchaseselect, mysqcon);
        DataTable dtTenderpurchaseselect = new DataTable();
        returnValTenderpurchaseselect.Fill(dtTenderpurchaseselect);


        mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);


        Tenderpurchasesql = "select * from nt_1_tenderdetails where  Company_Code=" + drpCompany.SelectedValue +
           " order by Tender_No";
        cmdsql = new SqlCommand(Tenderpurchasesql, sqlcon);

        returnValSQL = new SqlDataAdapter(Tenderpurchasesql, sqlcon);
        dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        dt3 = new DataTable();


        dt3.Columns.Add((new DataColumn("Tender_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Buyer", typeof(string))));
        dt3.Columns.Add((new DataColumn("Buyer_Quantal", typeof(string))));
        dt3.Columns.Add((new DataColumn("Sale_Rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("Commission_Rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("Sauda_Date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Lifting_Date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Narration", typeof(string))));
        dt3.Columns.Add((new DataColumn("ID", typeof(string))));
        dt3.Columns.Add((new DataColumn("Buyer_Party", typeof(string))));
        dt3.Columns.Add((new DataColumn("AutoID", typeof(string))));
        dt3.Columns.Add((new DataColumn("IsActive", typeof(string))));
        dt3.Columns.Add((new DataColumn("year_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Id", typeof(string))));
        dt3.Columns.Add((new DataColumn("Delivery_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("tenderid", typeof(string))));
        dt3.Columns.Add((new DataColumn("tenderdetailid", typeof(string))));
        dt3.Columns.Add((new DataColumn("buyerid", typeof(string))));
        dt3.Columns.Add((new DataColumn("buyerpartyid", typeof(string))));
        dt3.Columns.Add((new DataColumn("sub_broker", typeof(string))));
        dt3.Columns.Add((new DataColumn("sbr", typeof(string))));

        columnname = "Tender_No,Company_Code,Buyer,Buyer_Quantal,Sale_Rate,Commission_Rate,Narration,ID,Buyer_Party,AutoID," +
           "year_code,Branch_Id,Delivery_Type,tenderid,buyerid,buyerpartyid,sub_broker,sbr,tenderdetailid,Sauda_Date,Lifting_Date";


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["Tender_No"].ToString();

            DataRow[] dr = dt.Select("Tender_No = " + dtsql.Rows[i]["Tender_No"].ToString() +
                " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + " and ID=" + dtsql.Rows[i]["ID"].ToString() + " " +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString());

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }


        columnvalues = new StringBuilder();
        cityid = 1;
        maxLavel = 0;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([tenderdetailid])", string.Empty));
            cityid = maxLavel + 1;
        }
        ds = new DataSet();
        if (dt3.Rows.Count > 0)
        {
            int j = 0;
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                    string Buyer = dt3.Rows[i]["Buyer"].ToString();
                    string Buyer_Party = dt3.Rows[i]["Buyer_Party"].ToString();
                    //string Buyer_Party = dt3.Rows[i]["Buyer_Party"].ToString();
                    string Tender_No = dtsql.Rows[i]["Tender_No"].ToString();



                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Buyer + " and Company_Code=" + companycode);
                    string tenderid = "2";
                    string buyerid = "2";
                    string sbr = "2";
                    string buyerpartyid = "2";



                    if (drbsid.Length > 0)
                    {
                        buyerid = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Buyer_Party + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        buyerpartyid = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtTenderpurchaseselect.Select("Tender_No = " + Tender_No + " and Company_Code=" + companycode + " and Year_Code=" + groupcode);
                    if (drbsid.Length > 0)
                    {
                        tenderid = drbsid[0]["tenderid"].ToString();
                    }



                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append(dt3.Rows[i]["Tender_No"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Company_Code"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["Buyer"].ToString() + "',");
                    if (dt3.Rows[i]["Buyer_Quantal"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Buyer_Quantal"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Sale_Rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Sale_Rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Commission_Rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Commission_Rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    //columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["Sauda_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");

                    ////columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["Lifting_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    //columnvalues.Append("'',");
                    //columnvalues.Append("'',");

                    columnvalues.Append("'" + dt3.Rows[i]["Narration"].ToString() + "',");

                    columnvalues.Append("'" + dt3.Rows[i]["ID"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Buyer_Party"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["AutoID"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    if (dt3.Rows[i]["Branch_Id"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["Branch_Id"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Delivery_Type"].ToString() + "',");
                    columnvalues.Append(tenderid + ",");
                    columnvalues.Append(buyerid + ",");
                    columnvalues.Append(buyerpartyid + ",");
                    columnvalues.Append("2,");
                    columnvalues.Append("2,");
                    //  columnvalues.Append(dt3.Rows[i]["CashDiff"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["CashDiff"]) : "0.00");
                    columnvalues.Append("" + cityid + ",");
                    if (dt3.Rows[i]["Sauda_Date"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["Sauda_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    }
                    else
                    {
                        columnvalues.Append("'" + DateTime.Now.ToString("yyyy/MM/dd") + "',");
                    }
                    if (dt3.Rows[i]["Lifting_Date"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["Lifting_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "'");
                    }
                    else
                    {
                        columnvalues.Append("'" + DateTime.Now.ToString("yyyy/MM/dd") + "'");
                    }
                    columnvalues.Append(")");
                    cityid = cityid + 1;
                    string insert = "insert  into nt_1_tenderdetails (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    columnvalues = new StringBuilder();
                    j = i;
                    //  ds = clsDAL.SimpleQuery(insert);
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Tender details Migration is done!');", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("" + j + "");
                return;
            }
            finally
            {
                mysqcon.Close();
            }
        }
        #endregion
    }

    protected void btnDO_Click(object sender, EventArgs e)
    {
        #region Head
        string mysqlDO = "select * from nt_1_deliveryorder where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmd = new SqlCommand(mysqlDO, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(mysqlDO, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);


        string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);

        string mysqlCarporate = "select * from carporatehead where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlCarporate = new SqlCommand(mysqlCarporate, mysqcon);
        SqlDataAdapter mysqlDaCarporate = new SqlDataAdapter(mysqlCarporate, mysqcon);
        DataTable dtmysqlCarporate = new DataTable();
        mysqlDaCarporate.Fill(dtmysqlCarporate);

        string mysqlTenderDetail = "select * from nt_1_tenderdetails where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmyTender = new SqlCommand(mysqlTenderDetail, mysqcon);
        SqlDataAdapter mysqltender = new SqlDataAdapter(mysqlTenderDetail, mysqcon);
        DataTable dtmysqlTender = new DataTable();
        mysqltender.Fill(dtmysqlTender);

        string mysqlSaleBill = "select * from nt_1_sugarsale where  Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdSaleBill = new SqlCommand(mysqlSaleBill, mysqcon);
        SqlDataAdapter DASaleBill = new SqlDataAdapter(mysqlSaleBill, mysqcon);
        DataTable dtSaleBill = new DataTable();
        DASaleBill.Fill(dtSaleBill);

        string IremID = clsCommon.getString("select systemid from nt_1_systemmaster where System_Code=1 and System_Type='I' and Company_Code=" + drpCompany.SelectedValue + "");
        string DO = "select * from NT_1_deliveryorder where  Company_Code=" + drpCompany.SelectedValue + " and tran_type='DO' order by doc_no";
        SqlCommand cmdsql = new SqlCommand(DO, sqlcon);
        SqlDataAdapter returnValSQL = new SqlDataAdapter(DO, sqlcon);
        DataTable dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        DataTable dt3 = new DataTable();

        #region Mysql Do Table Column
        dt3.Columns.Add((new DataColumn("tran_type", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("desp_type", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("mill_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("grade", typeof(string))));
        dt3.Columns.Add((new DataColumn("quantal", typeof(string))));
        dt3.Columns.Add((new DataColumn("packing", typeof(string))));
        dt3.Columns.Add((new DataColumn("bags", typeof(string))));
        dt3.Columns.Add((new DataColumn("mill_rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("sale_rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tender_Commission", typeof(string))));
        dt3.Columns.Add((new DataColumn("diff_rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("diff_amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("DO", typeof(string))));
        dt3.Columns.Add((new DataColumn("voucher_by", typeof(string))));
        dt3.Columns.Add((new DataColumn("broker", typeof(string))));
        dt3.Columns.Add((new DataColumn("company_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("purc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("purc", typeof(string))));
        dt3.Columns.Add((new DataColumn("purc_order", typeof(string))));
        dt3.Columns.Add((new DataColumn("purc_type", typeof(string))));
        dt3.Columns.Add((new DataColumn("truck_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("transport", typeof(string))));
        dt3.Columns.Add((new DataColumn("less", typeof(string))));
        dt3.Columns.Add((new DataColumn("less_amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("final_amout", typeof(string))));
        dt3.Columns.Add((new DataColumn("vasuli", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration1", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration2", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration3", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration4", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration5", typeof(string))));
        dt3.Columns.Add((new DataColumn("excise_rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("memo_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("freight", typeof(string))));
        dt3.Columns.Add((new DataColumn("adv_freight1", typeof(string))));
        dt3.Columns.Add((new DataColumn("driver_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("driver_Name", typeof(string))));
        dt3.Columns.Add((new DataColumn("voucher_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("voucher_type", typeof(string))));
        dt3.Columns.Add((new DataColumn("GETPASSCODE", typeof(string))));
        dt3.Columns.Add((new DataColumn("tender_Remark", typeof(string))));
        dt3.Columns.Add((new DataColumn("vasuli_rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("vasuli_amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("to_vasuli", typeof(string))));
        dt3.Columns.Add((new DataColumn("naka_delivery", typeof(string))));
        dt3.Columns.Add((new DataColumn("send_sms", typeof(string))));
        dt3.Columns.Add((new DataColumn("Itag", typeof(string))));
        dt3.Columns.Add((new DataColumn("Ac_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("FreightPerQtl", typeof(string))));
        dt3.Columns.Add((new DataColumn("Freight_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Freight_RateMM", typeof(string))));
        dt3.Columns.Add((new DataColumn("Freight_AmountMM", typeof(string))));
        dt3.Columns.Add((new DataColumn("Memo_Advance", typeof(string))));
        dt3.Columns.Add((new DataColumn("Paid_Rate1", typeof(string))));
        dt3.Columns.Add((new DataColumn("Paid_Amount1", typeof(string))));
        dt3.Columns.Add((new DataColumn("Paid_Narration1", typeof(string))));
        dt3.Columns.Add((new DataColumn("Paid_Rate2", typeof(string))));
        dt3.Columns.Add((new DataColumn("Paid_Amount2", typeof(string))));
        dt3.Columns.Add((new DataColumn("Paid_Narration2", typeof(string))));
        dt3.Columns.Add((new DataColumn("Paid_Rate3", typeof(string))));
        dt3.Columns.Add((new DataColumn("Paid_Amount3", typeof(string))));
        dt3.Columns.Add((new DataColumn("Paid_Narration3", typeof(string))));
        dt3.Columns.Add((new DataColumn("MobileNo", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("UTR_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("UTR_Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Carporate_Sale_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Carporate_Sale_Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Delivery_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("WhoseFrieght", typeof(string))));
        dt3.Columns.Add((new DataColumn("SB_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Invoice_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("vasuli_rate1", typeof(string))));
        dt3.Columns.Add((new DataColumn("vasuli_amount1", typeof(string))));
        dt3.Columns.Add((new DataColumn("Party_Commission_Rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("MM_CC", typeof(string))));
        dt3.Columns.Add((new DataColumn("MM_Rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_Brokrage", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_Service_Charge", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_RateDiffRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_RateDiffAmt", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_BankCommRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_BankCommAmt", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_Interest", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_TransportAmt", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_OtherExpenses", typeof(string))));
        dt3.Columns.Add((new DataColumn("CheckPost", typeof(string))));
        dt3.Columns.Add((new DataColumn("SaleBillTo", typeof(string))));
        dt3.Columns.Add((new DataColumn("Pan_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Vasuli_Ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("LoadingSms", typeof(string))));
        dt3.Columns.Add((new DataColumn("GstRateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("GetpassGstStateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("VoucherbyGstStateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("SalebilltoGstStateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("GstAmtOnMR", typeof(string))));
        dt3.Columns.Add((new DataColumn("GstAmtOnSR", typeof(string))));
        dt3.Columns.Add((new DataColumn("GstExlSR", typeof(string))));
        dt3.Columns.Add((new DataColumn("GstExlMR", typeof(string))));
        dt3.Columns.Add((new DataColumn("MillGSTStateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("TransportGSTStateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("EWay_Bill_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Distance", typeof(string))));
        dt3.Columns.Add((new DataColumn("EWayBillChk", typeof(string))));
        dt3.Columns.Add((new DataColumn("MillInvoiceNo", typeof(string))));
        dt3.Columns.Add((new DataColumn("Purchase_Date", typeof(string))));
        dt3.Columns.Add((new DataColumn("doid", typeof(string))));
        dt3.Columns.Add((new DataColumn("mc", typeof(string))));
        dt3.Columns.Add((new DataColumn("gp", typeof(string))));
        dt3.Columns.Add((new DataColumn("st", typeof(string))));
        dt3.Columns.Add((new DataColumn("sb", typeof(string))));
        dt3.Columns.Add((new DataColumn("tc", typeof(string))));
        dt3.Columns.Add((new DataColumn("itemcode", typeof(string))));
        dt3.Columns.Add((new DataColumn("cs", typeof(string))));
        dt3.Columns.Add((new DataColumn("ic", typeof(string))));
        dt3.Columns.Add((new DataColumn("tenderdetailid", typeof(string))));
        dt3.Columns.Add((new DataColumn("bk", typeof(string))));
        dt3.Columns.Add((new DataColumn("docd", typeof(string))));
        dt3.Columns.Add((new DataColumn("vb", typeof(string))));
        dt3.Columns.Add((new DataColumn("va", typeof(string))));
        dt3.Columns.Add((new DataColumn("carporate_ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("ca", typeof(string))));
        dt3.Columns.Add((new DataColumn("mill_inv_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("mill_rcv", typeof(string))));
        dt3.Columns.Add((new DataColumn("saleid", typeof(string))));
        #endregion

        string columnname = "tran_type,doc_no,desp_type,doc_date,mill_code,grade,quantal,packing,bags,mill_rate,sale_rate,Tender_Commission,diff_rate" +
",diff_amount,amount,DO,voucher_by,broker,company_code,Year_Code,Branch_Code,purc_no,purc,purc_order,purc_type,truck_no" +
",transport,less,less_amount,final_amout,vasuli,narration1,narration2,narration3,narration4,narration5,excise_rate,memo_no" +
",freight,adv_freight1,driver_no,driver_Name,voucher_no,voucher_type,GETPASSCODE,tender_Remark,vasuli_rate,vasuli_amount" +
",to_vasuli,naka_delivery,send_sms,Itag,Ac_Code,FreightPerQtl,Freight_Amount,Freight_RateMM,Freight_AmountMM,Memo_Advance" +
",Paid_Rate1,Paid_Amount1,Paid_Narration1,Paid_Rate2,Paid_Amount2,Paid_Narration2,Paid_Rate3,Paid_Amount3,Paid_Narration3" +
",MobileNo,Created_By,Modified_By,UTR_No,UTR_Year_Code,Carporate_Sale_No,Carporate_Sale_Year_Code,Delivery_Type,WhoseFrieght" +
",SB_No,Invoice_No,vasuli_rate1,vasuli_amount1,Party_Commission_Rate,MM_CC,MM_Rate,Voucher_Brokrage,Voucher_Service_Charge" +
",Voucher_RateDiffRate,Voucher_RateDiffAmt,Voucher_BankCommRate,Voucher_BankCommAmt,Voucher_Interest,Voucher_TransportAmt" +
",Voucher_OtherExpenses,CheckPost,SaleBillTo,Pan_No,Vasuli_Ac,LoadingSms,GstRateCode,GetpassGstStateCode,VoucherbyGstStateCode" +
",SalebilltoGstStateCode,GstAmtOnMR,GstAmtOnSR,GstExlSR,GstExlMR,MillGSTStateCode,TransportGSTStateCode,EWay_Bill_No,Distance" +
",EWayBillChk,MillInvoiceNo,Purchase_Date,doid,mc,gp,st,sb,tc,itemcode,cs,ic,tenderdetailid,bk,docd,vb,va,carporate_ac,ca,mill_inv_date,mill_rcv,saleid";


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() + " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + "" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }


        StringBuilder columnvalues = new StringBuilder();
        int cityid = 1;
        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([doid])", string.Empty));
            cityid = maxLavel + 1;
        }
        DataSet ds = new DataSet();
        if (dt3.Rows.Count > 0)
        {
            int j = 0;
            int i = 0;
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                SqlCommand MySQLcmd1 = new SqlCommand();
                for (i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                    string Mill_Code = dt3.Rows[i]["mill_code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["mill_code"]) : "0";
                    string GETPASSCODE = dt3.Rows[i]["GETPASSCODE"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["GETPASSCODE"]) : "0";
                    string VoucherBy = dt3.Rows[i]["voucher_by"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["voucher_by"]) : "0";
                    string SaleBillTo = dt3.Rows[i]["SaleBillTo"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["SaleBillTo"]) : "0";
                    string transport = dt3.Rows[i]["transport"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["transport"]) : "0";
                    string Vasuli_Ac = dt3.Rows[i]["Vasuli_Ac"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Vasuli_Ac"]) : "0";
                    string DOCode = dt3.Rows[i]["DO"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["DO"]) : "0";
                    string broker = dt3.Rows[i]["broker"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["broker"]) : "0";
                    string Carporate = dt3.Rows[i]["Carporate_Sale_No"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Carporate_Sale_No"]) : "0";
                    string TenderNO = dt3.Rows[i]["purc_no"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["purc_no"]) : "0";
                    string TDetailID = dt3.Rows[i]["purc_order"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["purc_order"]) : "0";
                    string SBNO = dt3.Rows[i]["SB_No"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["SB_No"]) : "0";

                    string mc = "2";
                    string gp = "2";
                    string st = "2";
                    string sb = "2";
                    string tc = "2";
                    string bk = "2";
                    string cs = "0";
                    string ic = "2";
                    string docd = "2";
                    string vb = "2";
                    string ca = "2";
                    string va = "2";
                    string tAutoid = "0";
                    string saleid = "0";
                    string CarpBillTo = "0";
                    string carpID = "0";

                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Mill_Code + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        mc = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlTender.Select("Tender_No = " + TenderNO + " and ID=" + TDetailID + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        tAutoid = drbsid[0]["tenderdetailid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + GETPASSCODE + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        gp = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + VoucherBy + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        st = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + SaleBillTo + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        sb = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + transport + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        tc = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Vasuli_Ac + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        va = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + DOCode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        docd = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + broker + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        bk = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlCarporate.Select("doc_no = " + Carporate + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        cs = drbsid[0]["carpid"].ToString();
                        CarpBillTo = drbsid[0]["bill_to"].ToString();

                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + CarpBillTo + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        carpID = drbsid[0]["accoid"].ToString();
                    }


                    drbsid = dtSaleBill.Select("doc_no = " + SBNO + " and Company_Code=" + companycode + " and Year_Code=" + groupcode);
                    if (drbsid.Length > 0)
                    {
                        saleid = drbsid[0]["saleid"].ToString();
                    }

                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["tran_type"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["desp_type"].ToString() + "',");
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["mill_code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["grade"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["quantal"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["packing"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["bags"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["mill_rate"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["sale_rate"].ToString() + "',");
                    if (dt3.Rows[i]["Tender_Commission"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Tender_Commission"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["diff_rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["diff_rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["diff_amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["diff_amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["amount"].ToString() + "',");
                    if (dt3.Rows[i]["DO"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["DO"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["voucher_by"].ToString() + "',");
                    if (dt3.Rows[i]["broker"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["broker"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["company_code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");

                    if (dt3.Rows[i]["Branch_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["purc_no"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["purc"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["purc_order"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["purc_type"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["truck_no"].ToString() + "',");
                    if (dt3.Rows[i]["transport"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["transport"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["less"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["less"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["less_amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["less_amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["final_amout"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["final_amout"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["vasuli"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["vasuli"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["narration1"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["narration2"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["narration3"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["narration4"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["narration5"].ToString() + "',");
                    if (dt3.Rows[i]["excise_rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["excise_rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["memo_no"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["memo_no"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["freight"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["freight"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["adv_freight1"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["adv_freight1"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["driver_no"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["driver_Name"].ToString() + "',");
                    if (dt3.Rows[i]["voucher_no"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["voucher_no"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["voucher_type"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["GETPASSCODE"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["tender_Remark"].ToString() + "',");
                    if (dt3.Rows[i]["vasuli_rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["vasuli_rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["vasuli_amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["vasuli_amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["to_vasuli"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["to_vasuli"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }


                    columnvalues.Append("'" + dt3.Rows[i]["naka_delivery"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["send_sms"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Itag"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Ac_Code"].ToString() + "',");
                    if (dt3.Rows[i]["FreightPerQtl"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["FreightPerQtl"].ToString() + "',");

                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["Freight_Amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Freight_Amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["Freight_RateMM"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Freight_RateMM"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["Freight_AmountMM"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Freight_AmountMM"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["Memo_Advance"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Memo_Advance"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["Paid_Rate1"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Paid_Rate1"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["Paid_Amount1"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Paid_Amount1"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["Paid_Narration1"].ToString() + "',");
                    if (dt3.Rows[i]["Paid_Rate2"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Paid_Rate2"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["Paid_Amount2"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Paid_Amount2"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["Paid_Narration2"].ToString() + "',");

                    if (dt3.Rows[i]["Paid_Rate3"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Paid_Rate3"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["Paid_Amount3"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Paid_Amount3"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["Paid_Narration3"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["MobileNo"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    if (dt3.Rows[i]["UTR_No"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["UTR_No"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["UTR_Year_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["UTR_Year_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }

                    if (dt3.Rows[i]["Carporate_Sale_No"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Carporate_Sale_No"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["Carporate_Sale_Year_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Carporate_Sale_Year_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }


                    columnvalues.Append("'" + dt3.Rows[i]["Delivery_Type"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["WhoseFrieght"].ToString() + "',");
                    if (dt3.Rows[i]["SB_No"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SB_No"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["Invoice_No"].ToString() + "',");
                    if (dt3.Rows[i]["vasuli_rate1"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["vasuli_rate1"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["vasuli_amount1"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["vasuli_amount1"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["Party_Commission_Rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Party_Commission_Rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["MM_CC"].ToString() + "',");

                    if (dt3.Rows[i]["MM_Rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["MM_Rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["Voucher_Brokrage"].ToString() != string.Empty)
                    {

                        columnvalues.Append("'" + dt3.Rows[i]["Voucher_Brokrage"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["Voucher_Service_Charge"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Voucher_Service_Charge"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["Voucher_RateDiffRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Voucher_RateDiffRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["Voucher_RateDiffAmt"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Voucher_RateDiffAmt"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["Voucher_BankCommRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Voucher_BankCommRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["Voucher_BankCommAmt"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Voucher_BankCommAmt"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["Voucher_Interest"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Voucher_Interest"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["Voucher_TransportAmt"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Voucher_TransportAmt"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["Voucher_OtherExpenses"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Voucher_OtherExpenses"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["CheckPost"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["SaleBillTo"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Pan_No"].ToString() + "',");
                    if (dt3.Rows[i]["Vasuli_Ac"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Vasuli_Ac"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["LoadingSms"].ToString() + "',");
                    columnvalues.Append("1,");
                    if (dt3.Rows[i]["GetpassGstStateCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["GetpassGstStateCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    if (dt3.Rows[i]["VoucherbyGstStateCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["VoucherbyGstStateCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    if (dt3.Rows[i]["SalebilltoGstStateCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SalebilltoGstStateCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    if (dt3.Rows[i]["GstAmtOnMR"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["GstAmtOnMR"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["GstAmtOnSR"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["GstAmtOnSR"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }


                    columnvalues.Append("null,");
                    columnvalues.Append("null,");

                    if (dt3.Rows[i]["MillGSTStateCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["MillGSTStateCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    if (dt3.Rows[i]["TransportGSTStateCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["TransportGSTStateCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["EWay_Bill_No"].ToString() + "',");
                    if (dt3.Rows[i]["Distance"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Distance"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["EWayBillChk"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["MillInvoiceNo"].ToString() + "',");
                    if (dt3.Rows[i]["Purchase_Date"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["Purchase_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    }
                    else
                    {

                        columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    }

                    columnvalues.Append("'" + cityid + "',");
                    columnvalues.Append("'" + mc + "',");
                    columnvalues.Append("'" + gp + "',");
                    columnvalues.Append("'" + st + "',");
                    columnvalues.Append("'" + sb + "',");
                    columnvalues.Append("'" + tc + "',");
                    columnvalues.Append("1,");
                    if (cs != "0")
                    {
                        columnvalues.Append("'" + cs + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("'" + IremID + "',");
                    if (tAutoid != "0")
                    {
                        columnvalues.Append("'" + tAutoid + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("'" + bk + "',");
                    columnvalues.Append("'" + docd + "',");
                    columnvalues.Append("'" + st + "',");
                    columnvalues.Append("'" + va + "',");

                    if (CarpBillTo != "0")
                    {
                        columnvalues.Append("'" + CarpBillTo + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (carpID != "0")
                    {
                        columnvalues.Append("'" + carpID + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    columnvalues.Append("'N',");
                    if (saleid != "0")
                    {
                        columnvalues.Append("'" + saleid + "'");
                    }
                    else
                    {
                        columnvalues.Append("null");
                    }
                    columnvalues.Append(")");
                    cityid = cityid + 1;
                    string insert = "insert  into nt_1_deliveryorder (" + columnname + ") values " + columnvalues;

                    MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    columnvalues = new StringBuilder();

                    //  ds = clsDAL.SimpleQuery(insert);
                }



                transaction.Commit();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('DO Migration is done!');", true);
            }
            catch (Exception ex)
            {
                j = i;
                throw ex;
            }
            finally
            {
                mysqcon.Close();
            }

        }
        #endregion


        #region[Deatials]
        string mysqlDOdetail = "select * from nt_1_dodetails where Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        cmd = new SqlCommand(mysqlDOdetail, mysqcon);
        returnVal = new SqlDataAdapter(mysqlDOdetail, mysqcon);
        dt = new DataTable();
        returnVal.Fill(dt);

        string mysqlDo = "select * from nt_1_deliveryorder where  Company_Code=" + drpCompany.SelectedValue +
            " order by doc_no";
        SqlCommand cmdDO = new SqlCommand(mysqlDo, mysqcon);
        SqlDataAdapter returnValDO = new SqlDataAdapter(mysqlDo, mysqcon);
        DataTable dtDO = new DataTable();
        returnValDO.Fill(dtDO);

        mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);


        string mysqlutr = "select * from nt_1_utrdetail where  Company_Code=" + drpCompany.SelectedValue +
            " order by doc_no";
        SqlCommand cmdutr = new SqlCommand(mysqlutr, mysqcon);
        SqlDataAdapter returnValutr = new SqlDataAdapter(mysqlutr, mysqcon);
        DataTable dtutr = new DataTable();
        returnValutr.Fill(dtutr);


        string sqlDO = "select * from nt_1_dodetails where Company_Code=" + drpCompany.SelectedValue +
           " order by doc_no";
        cmdsql = new SqlCommand(sqlDO, sqlcon);

        returnValSQL = new SqlDataAdapter(sqlDO, sqlcon);
        dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        dt3 = new DataTable();


        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("detail_Id", typeof(string))));
        dt3.Columns.Add((new DataColumn("ddType", typeof(string))));
        dt3.Columns.Add((new DataColumn("Bank_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Narration", typeof(string))));
        dt3.Columns.Add((new DataColumn("Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("UTR_NO", typeof(string))));
        dt3.Columns.Add((new DataColumn("DO_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("UtrYearCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("LTNo", typeof(string))));
        dt3.Columns.Add((new DataColumn("doid", typeof(string))));
        dt3.Columns.Add((new DataColumn("dodetailid", typeof(string))));
        dt3.Columns.Add((new DataColumn("bc", typeof(string))));
        dt3.Columns.Add((new DataColumn("utrdetailid", typeof(string))));

        columnname = "doc_no,detail_Id,ddType,Bank_Code,Narration,Amount,Company_Code,Year_Code,Branch_Code,UTR_NO,DO_No, " +
            " UtrYearCode,LTNo,doid,dodetailid,bc,utrdetailid";


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() +
                " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + " and detail_Id=" + dtsql.Rows[i]["detail_Id"].ToString() + "" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }


        columnvalues = new StringBuilder();
        cityid = 1;
        maxLavel = 0;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([dodetailid])", string.Empty));
            cityid = maxLavel + 1;
        }
        ds = new DataSet();
        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                    string BankCode = dt3.Rows[i]["Bank_Code"].ToString();
                    string doc_no = dtsql.Rows[i]["doc_no"].ToString();
                    string utrno = dtsql.Rows[i]["utr_no"].ToString();
                    string DEtailID = dtsql.Rows[i]["detail_id"].ToString();

                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + BankCode + " and Company_Code=" + companycode);
                    string doid = "2";
                    string bc = "2";
                    string utrid = "0";
                    string buyerpartyid = "2";



                    if (drbsid.Length > 0)
                    {
                        bc = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtDO.Select("doc_no = " + doc_no + " and Company_Code=" + companycode + " and Year_Code=" + groupcode);
                    if (drbsid.Length > 0)
                    {
                        doid = drbsid[0]["doid"].ToString();
                    }
                    drbsid = dtutr.Select("doc_no = " + utrno + " and Company_Code=" + companycode + " and Year_Code=" + groupcode);
                    if (drbsid.Length > 0)
                    {
                        utrid = drbsid[0]["utrdetailid"].ToString();
                    }


                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["detail_Id"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["ddType"].ToString() + "',");
                    if (dt3.Rows[i]["Bank_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Bank_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Narration"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Amount"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    if (dt3.Rows[i]["Branch_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["UTR_NO"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["UTR_NO"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    if (dt3.Rows[i]["UtrYearCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["UtrYearCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["LTNo"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["LTNo"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0',");

                    }
                    columnvalues.Append("'" + doid + "',");
                    columnvalues.Append("'" + cityid + "',");
                    columnvalues.Append("'" + bc + "',");

                    if (utrid != "0")
                    {
                        columnvalues.Append("'" + utrid + "'");
                    }
                    else
                    {
                        columnvalues.Append("null");

                    }

                    columnvalues.Append(")");
                    cityid = cityid + 1;
                    string insert = "insert  into nt_1_dodetails (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    columnvalues = new StringBuilder();
                    //  ds = clsDAL.SimpleQuery(insert);
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Do details Migration is done!');", true);
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                mysqcon.Close();
            }
        }
        #endregion
    }
    protected void btnCarporate_Click(object sender, EventArgs e)
    {
        #region[Head]
        string Carporate = "select * from carporatehead where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmd = new SqlCommand(Carporate, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(Carporate, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);


        string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);


        string Carporatesql = "select doc_no,doc_date,ac_code,unit_code,broker,PODETAIL as pono,quantal, " +
            " sell_rate,ASN_no as remark,Company_Code,created_by,modified_by,SellingType as selling_type from NT_1_CarporateSale where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmdsql = new SqlCommand(Carporatesql, sqlcon);

        SqlDataAdapter returnValSQL = new SqlDataAdapter(Carporatesql, sqlcon);
        DataTable dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        DataTable dt3 = new DataTable();


        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("ac_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("unit_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("broker", typeof(string))));
        dt3.Columns.Add((new DataColumn("pono", typeof(string))));
        dt3.Columns.Add((new DataColumn("quantal", typeof(string))));
        dt3.Columns.Add((new DataColumn("sell_rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("remark", typeof(string))));
        dt3.Columns.Add((new DataColumn("company_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("carpid", typeof(string))));
        dt3.Columns.Add((new DataColumn("created_by", typeof(string))));
        dt3.Columns.Add((new DataColumn("modified_by", typeof(string))));
        dt3.Columns.Add((new DataColumn("ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("uc", typeof(string))));
        dt3.Columns.Add((new DataColumn("br", typeof(string))));
        dt3.Columns.Add((new DataColumn("selling_type", typeof(string))));
        dt3.Columns.Add((new DataColumn("bill_to", typeof(string))));
        dt3.Columns.Add((new DataColumn("bt", typeof(string))));
        dt3.Columns.Add((new DataColumn("DeliveryType", typeof(string))));

        string columnname = "doc_no,doc_date,ac_code,unit_code,broker,pono,quantal,sell_rate,remark,company_code,carpid,created_by,modified_by,ac,uc," +
                             " br,selling_type,bill_to,bt,DeliveryType";

        if (dtsql.Rows.Count > 0)
        {
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('No Record Found!');", true);
            return;
        }
        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() + " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString());

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }


        StringBuilder columnvalues = new StringBuilder();
        int cityid = 1;
        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([carpid])", string.Empty));
            cityid = maxLavel + 1;
        }
        DataSet ds = new DataSet();
        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string AcCode = dt3.Rows[i]["ac_code"].ToString();
                    string unitCode = dt3.Rows[i]["unit_code"].ToString();
                    string Broker = dt3.Rows[i]["broker"].ToString();

                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + AcCode + " and Company_Code=" + companycode);
                    string ac = "2";
                    string uc = "2";
                    string br = "2";


                    if (drbsid.Length > 0)
                    {
                        ac = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + unitCode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        uc = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Broker + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        br = drbsid[0]["accoid"].ToString();
                    }
                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");

                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["ac_code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["unit_code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["broker"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["pono"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["quantal"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["sell_rate"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["remark"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["company_code"].ToString() + "',");
                    columnvalues.Append("'" + cityid + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["created_by"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["modified_by"].ToString() + "',");
                    columnvalues.Append("'" + ac + "',");
                    columnvalues.Append("'" + uc + "',");
                    columnvalues.Append("'" + br + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["selling_type"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["ac_code"].ToString() + "',");
                    columnvalues.Append("'" + ac + "',");
                    columnvalues.Append("'N'");


                    columnvalues.Append(")");
                    cityid = cityid + 1;
                    string insert = "insert  into carporatehead (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    columnvalues = new StringBuilder();
                    //  ds = clsDAL.SimpleQuery(insert);
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Carporate Migration is done!');", true);
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                mysqcon.Close();
            }
        }
        #endregion
    }

    protected void btnutr_Click(object sender, EventArgs e)
    {
        string utrentry = "select * from nt_1_utr where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmd = new SqlCommand(utrentry, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(utrentry, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);

        string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);

        string UtrEntrySql = "select * from NT_1_UTR where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmdsql = new SqlCommand(UtrEntrySql, sqlcon);

        SqlDataAdapter returnValSQL = new SqlDataAdapter(UtrEntrySql, sqlcon);
        DataTable dtsql = new DataTable();
        returnValSQL.Fill(dtsql);

        DataTable dt3 = new DataTable();

        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("bank_ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("mill_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("utr_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration_header", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration_footer", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("IsSave", typeof(string))));
        dt3.Columns.Add((new DataColumn("Lott_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("utrid", typeof(string))));
        dt3.Columns.Add((new DataColumn("ba", typeof(string))));
        dt3.Columns.Add((new DataColumn("mc", typeof(string))));
        string columnname = "doc_no,Company_Code,doc_date,bank_ac,mill_code,amount,utr_no,narration_header,narration_footer,Year_Code,Branch_Code," +
          " Created_By,Modified_By,Lott_No,mc,IsSave,ba,utrid";


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string doc_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() + " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + " and Year_code=" + dtsql.Rows[i]["Year_Code"].ToString());

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }

        StringBuilder columnvalues = new StringBuilder();
        int utrid = 1;

        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([utrid])", string.Empty));
            utrid = maxLavel + 1;
        }
        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string Yearcode = dt3.Rows[i]["Year_Code"].ToString();
                    string Mill_Code = dt3.Rows[i]["mill_code"].ToString();
                    string bankCode = dt3.Rows[i]["bank_ac"].ToString();
                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Mill_Code + " and Company_Code=" + companycode);
                    string mc = "2";
                    string ba = "2";



                    if (drbsid.Length > 0)
                    {
                        mc = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + bankCode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        ba = drbsid[0]["accoid"].ToString();
                    }
                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append(dt3.Rows[i]["doc_no"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Company_Code"].ToString() + ",");
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");


                    columnvalues.Append(dt3.Rows[i]["bank_ac"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["mill_code"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["amount"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["utr_no"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["narration_header"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["narration_footer"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["Branch_Code"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["Lott_No"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Lott_No"]) : "0.00");
                    columnvalues.Append("," + mc + ",");
                    columnvalues.Append(dt3.Rows[i]["IsSave"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["IsSave"]) : "0.00");

                    columnvalues.Append("," + ba + ",");
                    columnvalues.Append("" + utrid + "");
                    columnvalues.Append(")");
                    utrid = utrid + 1;
                    string insert = "insert  into nt_1_utr (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    columnvalues = new StringBuilder();
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('utrentry Migration is done!');", true);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mysqcon.Close();
            }

        }

        ////Migrate account utrEntry


        string utrdetail = "select * from nt_1_utrdetail where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        cmd = new SqlCommand(utrdetail, mysqcon);
        returnVal = new SqlDataAdapter(utrdetail, mysqcon);
        dt = new DataTable();
        returnVal.Fill(dt);

        string utrselect = "select * from nt_1_utr where Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmdutrseselect = new SqlCommand(utrselect, mysqcon);
        SqlDataAdapter returnValutrEntryseselect = new SqlDataAdapter(utrselect, mysqcon);
        DataTable dtUTRENTRYselect = new DataTable();
        returnValutrEntryseselect.Fill(dtUTRENTRYselect);




        string Tenderpurchasesql1 = "select * from nt_1_tender where  Company_Code=" + drpCompany.SelectedValue + " order by Tender_No";
        SqlCommand cmdsql1 = new SqlCommand(Tenderpurchasesql1, mysqcon);

        SqlDataAdapter returnValSQL1 = new SqlDataAdapter(Tenderpurchasesql1, mysqcon);
        DataTable dtsql1 = new DataTable();
        returnValSQL.Fill(dtsql1);


        string tenderentry = "select * from nt_1_tender where  Company_Code=" + drpCompany.SelectedValue + " order by Tender_No";
        SqlCommand cmdtenterenrty = new SqlCommand(tenderentry, mysqcon);
        SqlDataAdapter retuntenderentry = new SqlDataAdapter(tenderentry, mysqcon);
        DataTable data = new DataTable();
        retuntenderentry.Fill(data);

        try
        {
            string sqlutrdetail = "select * from NT_1_UTRDETAIL where  Company_Code=" + drpCompany.SelectedValue +
                   " order by doc_no";
            cmdsql = new SqlCommand(sqlutrdetail, sqlcon);

            returnValSQL = new SqlDataAdapter(sqlutrdetail, sqlcon);
            dtsql = new DataTable();
            returnValSQL.Fill(dtsql);
            dt3 = new DataTable();

            dt3.Columns.Add((new DataColumn("Detail_Id", typeof(string))));
            dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
            dt3.Columns.Add((new DataColumn("lot_no", typeof(string))));
            dt3.Columns.Add((new DataColumn("grade_no", typeof(string))));
            dt3.Columns.Add((new DataColumn("amount", typeof(string))));
            dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
            dt3.Columns.Add((new DataColumn("lotCompany_Code", typeof(string))));
            dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
            dt3.Columns.Add((new DataColumn("lotYear_Code", typeof(string))));
            dt3.Columns.Add((new DataColumn("Adjusted_Amt", typeof(string))));
            dt3.Columns.Add((new DataColumn("LTNo", typeof(string))));
            dt3.Columns.Add((new DataColumn("utrdetailid", typeof(string))));
            dt3.Columns.Add((new DataColumn("utrid", typeof(string))));
            dt3.Columns.Add((new DataColumn("ln", typeof(string))));


            columnname = "Detail_Id,doc_no,lot_no,grade_no,amount,Company_Code,lotCompany_Code,Year_Code,lotYear_Code,Adjusted_Amt," +
               "LTNo,utrdetailid,utrid,ln";


            for (int i = 0; i < dtsql.Rows.Count; i++)
            {
                string companycode = dtsql.Rows[i]["Company_Code"].ToString();
                string doc_no = dtsql.Rows[i]["doc_no"].ToString();

                DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() +
                    " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + " and Detail_Id=" + dtsql.Rows[i]["Detail_Id"].ToString() + "" +
                    " and Year_code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

                if (dr.Length == 0)
                {
                    dt3.ImportRow(dtsql.Rows[i]);
                }
            }


            columnvalues = new StringBuilder();
            int cityid = 1;
            maxLavel = 0;
            if (dt.Rows.Count > 0)
            {
                maxLavel = Convert.ToInt32(dt.Compute("max([utrdetailid])", string.Empty));
                cityid = maxLavel + 1;
            }
            //     ds = new DataSet();
            if (dt3.Rows.Count > 0)
            {
                try
                {

                    mysqcon.ConnectionString = mysqlconn;
                    mysqcon.Open();
                    transaction = mysqcon.BeginTransaction();
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        #region
                        string companycode = dt3.Rows[i]["Company_Code"].ToString();
                        string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                        string lotno = dt3.Rows[i]["lot_no"].ToString();
                        string grade = dt3.Rows[i]["grade_no"].ToString();

                        string doc_no = dtsql.Rows[i]["doc_no"].ToString();



                        DataRow[] drbsid = data.Select("Tender_No = " + lotno + " and Company_Code=" + companycode + " and Year_Code=" + groupcode);
                        string utrdetailid = "2";
                        string lotnoid = "2";
                        string gradeid = "2";



                        if (drbsid.Length > 0)
                        {
                            lotnoid = drbsid[0]["Tender_No"].ToString();
                        }

                        //drbsid = data.Select("Ac_Code = " + grade + " and Company_Code=" + companycode);
                        //if (drbsid.Length > 0)
                        //{
                        //    gradeid = drbsid[0]["accoid"].ToString();
                        //}

                        drbsid = dtUTRENTRYselect.Select("doc_no = " + doc_no + " and Company_Code=" + companycode + " and Year_Code=" + groupcode);
                        if (drbsid.Length > 0)
                        {
                            utrdetailid = drbsid[0]["utrid"].ToString();
                        }



                        columnvalues.Clear();
                        columnvalues.Append("(");
                        columnvalues.Append(dt3.Rows[i]["Detail_Id"].ToString() + ",");
                        columnvalues.Append(dt3.Rows[i]["doc_no"].ToString() + ",");
                        columnvalues.Append(dt3.Rows[i]["lot_no"].ToString() + ",");
                        columnvalues.Append("'" + dt3.Rows[i]["grade_no"].ToString() + "',");
                        columnvalues.Append(dt3.Rows[i]["amount"].ToString() + ",");
                        columnvalues.Append(dt3.Rows[i]["Company_Code"].ToString() + ",");
                        columnvalues.Append(dt3.Rows[i]["lotCompany_Code"].ToString() + ",");
                        columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                        columnvalues.Append(dt3.Rows[i]["lotYear_Code"].ToString() + ",");
                        columnvalues.Append(dt3.Rows[i]["Adjusted_Amt"].ToString() + ",");
                        columnvalues.Append(dt3.Rows[i]["LTNo"].ToString() + ",");
                        // columnvalues.Append("'" + dt3.Rows[i]["lotCompany_Code"].ToString() + "',");
                        columnvalues.Append(cityid + ",");
                        columnvalues.Append(utrdetailid + ",");
                        columnvalues.Append(lotnoid + "");

                        columnvalues.Append(")");
                        #endregion
                        cityid = cityid + 1;
                        string insert = "insert  into nt_1_utrdetail (" + columnname + ") values " + columnvalues;

                        SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                        MySQLcmd1.ExecuteNonQuery();
                        columnvalues = new StringBuilder();
                        //  ds = clsDAL.SimpleQuery(insert);
                    }
                    transaction.Commit();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('UtrEntry details Migration is done!');", true);
                }
                catch (Exception ex)
                {
                    return;
                }
                finally
                {
                    mysqcon.Close();
                }
            }
        }
        catch (Exception ex)
        {
            return;
        }

    }
    protected void btnSaleBill_Click(object sender, EventArgs e)
    {
        #region[Head]
        string salebill = "select * from nt_1_sugarsale where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmd = new SqlCommand(salebill, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(salebill, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);


        string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);


        string SalebillSql = "select * from nt_1_sugarsale where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmdsql = new SqlCommand(SalebillSql, sqlcon);
        SqlDataAdapter returnValSQL = new SqlDataAdapter(SalebillSql, sqlcon);
        DataTable dtsql = new DataTable();
        returnValSQL.Fill(dtsql);


        string Purchase = "select * from nt_1_sugarpurchase where Company_Code=" + drpCompany.SelectedValue + " ";
        SqlCommand cmdPurchase = new SqlCommand(Purchase, mysqcon);
        SqlDataAdapter returnPurchase = new SqlDataAdapter(Purchase, mysqcon);
        DataTable dtmysqlPurchase = new DataTable();
        returnPurchase.Fill(dtmysqlPurchase);

        DataTable dt3 = new DataTable();



        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("PURCNO", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Ac_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Unit_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("mill_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("FROM_STATION", typeof(string))));
        dt3.Columns.Add((new DataColumn("TO_STATION", typeof(string))));
        dt3.Columns.Add((new DataColumn("LORRYNO", typeof(string))));
        dt3.Columns.Add((new DataColumn("BROKER", typeof(string))));
        dt3.Columns.Add((new DataColumn("wearhouse", typeof(string))));
        dt3.Columns.Add((new DataColumn("subTotal", typeof(string))));
        dt3.Columns.Add((new DataColumn("LESS_FRT_RATE", typeof(string))));
        dt3.Columns.Add((new DataColumn("freight", typeof(string))));
        dt3.Columns.Add((new DataColumn("cash_advance", typeof(string))));
        dt3.Columns.Add((new DataColumn("bank_commission", typeof(string))));
        dt3.Columns.Add((new DataColumn("OTHER_AMT", typeof(string))));
        dt3.Columns.Add((new DataColumn("Bill_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Due_Days", typeof(string))));
        dt3.Columns.Add((new DataColumn("NETQNTL", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tran_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("DO_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Transport_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("RateDiff", typeof(string))));
        dt3.Columns.Add((new DataColumn("ASN_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("GstRateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("CGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("CGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("SGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("SGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("IGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("IGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("TaxableAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("EWay_Bill_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("EWayBill_Chk", typeof(string))));
        dt3.Columns.Add((new DataColumn("MillInvoiceNo", typeof(string))));
        dt3.Columns.Add((new DataColumn("RoundOff", typeof(string))));
        dt3.Columns.Add((new DataColumn("saleid", typeof(string))));
        dt3.Columns.Add((new DataColumn("ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("uc", typeof(string))));
        dt3.Columns.Add((new DataColumn("mc", typeof(string))));
        dt3.Columns.Add((new DataColumn("bk", typeof(string))));
        dt3.Columns.Add((new DataColumn("tc", typeof(string))));
        dt3.Columns.Add((new DataColumn("Purcid", typeof(string))));


        string columnname = "doc_no,PURCNO,doc_date,Ac_Code,Unit_Code,mill_code,FROM_STATION,TO_STATION,LORRYNO,BROKER,wearhouse,subTotal,LESS_FRT_RATE" +
",freight,cash_advance,bank_commission,OTHER_AMT,Bill_Amount,Due_Days,NETQNTL,Company_Code,Year_Code,Branch_Code,Created_By" +
",Modified_By,Tran_Type,DO_No,Transport_Code,RateDiff,ASN_No,GstRateCode,CGSTRate,CGSTAmount,SGSTRate,SGSTAmount,IGSTRate" +
",IGSTAmount,TaxableAmount,EWay_Bill_No,EWayBill_Chk,MillInvoiceNo,RoundOff,saleid,ac,uc,mc,bk,tc,Purcid";


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string docno = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() + " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + "" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }


        StringBuilder columnvalues = new StringBuilder();
        int cityid = 1;
        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([saleid])", string.Empty));
            cityid = maxLavel + 1;
        }
        DataSet ds = new DataSet();
        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                    string Ac_Code = dt3.Rows[i]["Ac_Code"].ToString();
                    string Unit_Code = dt3.Rows[i]["Unit_Code"].ToString();
                    string mill_code = dt3.Rows[i]["mill_code"].ToString();
                    string BROKER = dt3.Rows[i]["BROKER"].ToString();
                    string transport = dt3.Rows[i]["Transport_Code"].ToString();
                    string PurchaseNO = dt3.Rows[i]["PURCNO"].ToString();


                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Ac_Code + " and Company_Code=" + companycode);
                    string ac = "2";
                    string uc = "2";
                    string mc = "2";
                    string br = "2";
                    string tc = "2";
                    string purcid = "0";


                    if (drbsid.Length > 0)
                    {
                        ac = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Unit_Code + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        uc = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + mill_code + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        mc = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + BROKER + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        br = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + transport + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        tc = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlPurchase.Select("doc_no = " + PurchaseNO + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        purcid = drbsid[0]["purchaseid"].ToString();
                    }

                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    if (dt3.Rows[i]["PURCNO"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["PURCNO"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }

                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Ac_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Unit_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["mill_code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["FROM_STATION"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["TO_STATION"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["LORRYNO"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["BROKER"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["wearhouse"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["subTotal"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["LESS_FRT_RATE"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["freight"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["cash_advance"].ToString() + "',");
                    if (dt3.Rows[i]["bank_commission"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["bank_commission"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    if (dt3.Rows[i]["OTHER_AMT"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["OTHER_AMT"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["Bill_Amount"].ToString() + "',");

                    if (dt3.Rows[i]["Due_Days"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Due_Days"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["NETQNTL"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    columnvalues.Append("null,");
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Tran_Type"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["DO_No"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Transport_Code"].ToString() + "',");
                    if (dt3.Rows[i]["RateDiff"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["RateDiff"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["ASN_No"].ToString() + "',");
                    if (dt3.Rows[i]["GstRateCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["GstRateCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("1,");
                    }
                    if (dt3.Rows[i]["CGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["CGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CGSTAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["SGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["SGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SGSTAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["IGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["IGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["IGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["IGSTAmount"].ToString() + "',");
                    }

                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["TaxableAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["TaxableAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["EWay_Bill_No"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["EWayBill_Chk"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["MillInvoiceNo"].ToString() + "',");
                    if (dt3.Rows[i]["RoundOff"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["RoundOff"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }

                    columnvalues.Append("'" + cityid + "',");
                    columnvalues.Append("'" + ac + "',");
                    columnvalues.Append("'" + uc + "',");
                    columnvalues.Append("'" + mc + "',");
                    columnvalues.Append("'" + br + "',");
                    columnvalues.Append("'" + tc + "',");
                    if (purcid != "0")
                    {
                        columnvalues.Append("'" + purcid + "'");
                    }
                    else
                    {
                        columnvalues.Append("null");
                    }
                    columnvalues.Append(")");
                    cityid = cityid + 1;
                    string insert = "insert  into nt_1_sugarsale (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    columnvalues = new StringBuilder();
                    //  ds = clsDAL.SimpleQuery(insert);
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('SaleBill Migration is done!');", true);
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                mysqcon.Close();
            }
        }
        #endregion

        #region[Deatials]
        string SaleBillDetail = "select * from nt_1_sugarsaledetails where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        cmd = new SqlCommand(SaleBillDetail, mysqcon);
        returnVal = new SqlDataAdapter(SaleBillDetail, mysqcon);
        dt = new DataTable();
        returnVal.Fill(dt);

        string SaleBillHead = "select * from nt_1_sugarsale where  Company_Code=" + drpCompany.SelectedValue +
            " order by doc_no";
        SqlCommand cmdSaleBill = new SqlCommand(SaleBillHead, mysqcon);
        SqlDataAdapter returnValSaleBillHead = new SqlDataAdapter(SaleBillHead, mysqcon);
        DataTable dtSaleBillHead = new DataTable();
        returnValSaleBillHead.Fill(dtSaleBillHead);


        mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);


        string mysqlItem_Master = "select * from nt_1_systemmaster where Company_Code=" + drpCompany.SelectedValue + " and System_Type='I'";
        SqlCommand cmdmysqlItem_Master = new SqlCommand(mysqlItem_Master, mysqcon);
        SqlDataAdapter MysqlDAItem = new SqlDataAdapter(mysqlItem_Master, mysqcon);

        DataTable dtmysqlItem_Master = new DataTable();
        MysqlDAItem.Fill(dtmysqlItem_Master);


        string SaleBillsql = "select * from nt_1_sugarsaledetails where  Company_Code=" + drpCompany.SelectedValue +
            " order by doc_no";
        cmdsql = new SqlCommand(SaleBillsql, sqlcon);

        returnValSQL = new SqlDataAdapter(SaleBillsql, sqlcon);
        dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        dt3 = new DataTable();

        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("detail_id", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tran_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("item_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration", typeof(string))));
        dt3.Columns.Add((new DataColumn("Quantal", typeof(string))));
        dt3.Columns.Add((new DataColumn("packing", typeof(string))));
        dt3.Columns.Add((new DataColumn("bags", typeof(string))));
        dt3.Columns.Add((new DataColumn("rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("item_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("saledetailid", typeof(string))));
        dt3.Columns.Add((new DataColumn("saleid", typeof(string))));
        dt3.Columns.Add((new DataColumn("ic", typeof(string))));

        columnname = "doc_no,detail_id,Tran_Type,item_code,narration,Quantal,packing,bags,rate,item_Amount,Company_Code,Year_Code,Branch_Code," +
            "Created_By,Modified_By,saledetailid,saleid,ic";


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() +
                " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + " and detail_id=" + dtsql.Rows[i]["detail_id"].ToString() + "" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }


        columnvalues = new StringBuilder();
        cityid = 1;
        maxLavel = 0;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([saledetailid])", string.Empty));
            cityid = maxLavel + 1;
        }
        ds = new DataSet();
        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                    string docNO = dtsql.Rows[i]["doc_no"].ToString();
                    string ItemCode = dtsql.Rows[i]["item_code"].ToString();


                    DataRow[] drbsid = dtmysqlItem_Master.Select("System_Code = " + ItemCode + " and Company_Code=" + companycode);
                    string ic = "2";
                    string saleid = "0";



                    if (drbsid.Length > 0)
                    {
                        ic = drbsid[0]["systemid"].ToString();
                    }

                    drbsid = dtSaleBillHead.Select("doc_no = " + docNO + " and Company_Code=" + companycode + " and Year_Code=" + groupcode);
                    if (drbsid.Length > 0)
                    {
                        saleid = drbsid[0]["saleid"].ToString();
                    }




                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["detail_id"].ToString() + "',");

                    columnvalues.Append("'" + dt3.Rows[i]["Tran_Type"].ToString() + "',");
                    if (dt3.Rows[i]["item_code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["item_code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("1,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["narration"].ToString() + "',");
                    if (dt3.Rows[i]["Quantal"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Quantal"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["packing"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["packing"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }

                    if (dt3.Rows[i]["bags"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["bags"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["item_Amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["item_Amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0.00',");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    if (dt3.Rows[i]["Branch_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    columnvalues.Append("'" + cityid + "',");
                    if (saleid != "0")
                    {
                        columnvalues.Append("'" + saleid + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("'" + ic + "'");
                    columnvalues.Append(")");
                    cityid = cityid + 1;
                    string insert = "insert  into nt_1_sugarsaledetails (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    columnvalues = new StringBuilder();
                    //  ds = clsDAL.SimpleQuery(insert);
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Sale details Migration is done!');", true);
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                mysqcon.Close();
            }
        }
        #endregion

    }

    protected void btnpurches_Click(object sender, EventArgs e)
    {
        #region Head
        string purchesEntry = "select * from nt_1_sugarpurchase where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmd = new SqlCommand(purchesEntry, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(purchesEntry, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);

        string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);

        string purchesentry = "select * from NT_1_SugarPurchase where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmdsql = new SqlCommand(purchesentry, sqlcon);
        SqlDataAdapter returnValSQL = new SqlDataAdapter(purchesentry, sqlcon);
        DataTable dtsql = new DataTable();
        returnValSQL.Fill(dtsql);



        string DoEntry = "select * from nt_1_deliveryorder where Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        cmd = new SqlCommand(DoEntry, mysqcon);
        returnVal = new SqlDataAdapter(DoEntry, mysqcon);
        DataTable DoHead = new DataTable();
        returnVal.Fill(DoHead);

        DataTable dt3 = new DataTable();
        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tran_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("PURCNO", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Ac_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Unit_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("mill_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("FROM_STATION", typeof(string))));
        dt3.Columns.Add((new DataColumn("TO_STATION", typeof(string))));
        dt3.Columns.Add((new DataColumn("LORRYNO", typeof(string))));
        dt3.Columns.Add((new DataColumn("BROKER", typeof(string))));
        dt3.Columns.Add((new DataColumn("wearhouse", typeof(string))));
        dt3.Columns.Add((new DataColumn("subTotal", typeof(string))));
        dt3.Columns.Add((new DataColumn("LESS_FRT_RATE", typeof(string))));
        dt3.Columns.Add((new DataColumn("freight", typeof(string))));
        dt3.Columns.Add((new DataColumn("cash_advance", typeof(string))));
        dt3.Columns.Add((new DataColumn("bank_commission", typeof(string))));
        dt3.Columns.Add((new DataColumn("OTHER_AMT", typeof(string))));
        dt3.Columns.Add((new DataColumn("Bill_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Due_Days", typeof(string))));
        dt3.Columns.Add((new DataColumn("NETQNTL", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Bill_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("GstRateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("CGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("CGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("SGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("SGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("IGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("IGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("EWay_Bill_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("purchaseid", typeof(string))));
        dt3.Columns.Add((new DataColumn("ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("uc", typeof(string))));
        dt3.Columns.Add((new DataColumn("mc", typeof(string))));
        dt3.Columns.Add((new DataColumn("bk", typeof(string))));
        dt3.Columns.Add((new DataColumn("grade", typeof(string))));
        dt3.Columns.Add((new DataColumn("mill_inv_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Purcid", typeof(string))));


        string columnname = "doc_no,Tran_Type,PURCNO,doc_date,Ac_Code,Unit_Code,mill_code,FROM_STATION,TO_STATION,LORRYNO,BROKER," +
          " wearhouse,subTotal,LESS_FRT_RATE,freight,cash_advance,bank_commission,OTHER_AMT,Bill_Amount,Due_Days,NETQNTL,Company_Code " +
          ",Year_Code,Branch_Code,Created_By,Modified_By,Bill_No,GstRateCode,CGSTRate,CGSTAmount,SGSTRate,SGSTAmount,IGSTRate,IGSTAmount,EWay_Bill_No," +
          " purchaseid,ac,uc,mc,bk,grade,mill_inv_date,Purcid";

        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string doc_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() + " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + "" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }

        StringBuilder columnvalues = new StringBuilder();
        int purchesid = 1;

        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([purchaseid])", string.Empty));
            purchesid = maxLavel + 1;
        }
        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string Yearcode = dt3.Rows[i]["Year_Code"].ToString();
                    string fromCode = dt3.Rows[i]["Ac_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Ac_Code"]) : "0";
                    string Unit_Code = dt3.Rows[i]["Unit_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Unit_Code"]) : "0";
                    string Mill_Code = dt3.Rows[i]["mill_code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["mill_code"]) : "0";
                    string brokerCode = dt3.Rows[i]["BROKER"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["BROKER"]) : "0";
                    string purcheNo = dt3.Rows[i]["PURCNO"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["PURCNO"]) : "0";
                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Mill_Code + " and Company_Code=" + companycode);
                    string mc = "2";
                    string uc = "2";
                    string bc = "2";
                    string fr = "2";
                    string pid = "0";


                    if (drbsid.Length > 0)
                    {
                        mc = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + fromCode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        fr = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Unit_Code + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        uc = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + brokerCode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        bc = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = DoHead.Select("doc_no = " + purcheNo + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        pid = drbsid[0]["doid"].ToString();
                    }

                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Tran_Type"].ToString() + "',");
                    if (dt3.Rows[i]["PURCNO"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["PURCNO"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Ac_Code"].ToString() + "',");
                    if (dt3.Rows[i]["Unit_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Unit_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["mill_code"].ToString() + "',");
                    if (dt3.Rows[i]["FROM_STATION"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["FROM_STATION"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["TO_STATION"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["TO_STATION"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["LORRYNO"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["BROKER"].ToString() + "',");
                    if (dt3.Rows[i]["wearhouse"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["wearhouse"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["subTotal"].ToString() + "',");
                    if (dt3.Rows[i]["LESS_FRT_RATE"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["LESS_FRT_RATE"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["freight"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["freight"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["cash_advance"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["cash_advance"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["bank_commission"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["bank_commission"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["OTHER_AMT"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["OTHER_AMT"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Bill_Amount"].ToString() + "',");
                    if (dt3.Rows[i]["Due_Days"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Due_Days"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["NETQNTL"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["NETQNTL"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    if (dt3.Rows[i]["Bill_No"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Bill_No"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["GstRateCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["GstRateCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["CGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["CGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CGSTAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["SGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["SGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SGSTAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["IGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["IGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["IGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["IGSTAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["EWay_Bill_No"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["EWay_Bill_No"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + purchesid + "',");
                    columnvalues.Append("'" + fr + "',");
                    columnvalues.Append("'" + uc + "',");
                    columnvalues.Append("'" + mc + "',");
                    columnvalues.Append("'" + bc + "',");
                    if (dt3.Rows[i]["grade"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["grade"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    if (pid != "0")
                    {
                        columnvalues.Append("" + pid + "");
                    }
                    else
                    {
                        columnvalues.Append("null");
                    }

                    columnvalues.Append(")");
                    purchesid = purchesid + 1;
                    string insert = "insert  into nt_1_sugarpurchase (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    columnvalues = new StringBuilder();
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Purches Migration is done!');", true);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mysqcon.Close();
            }

        }
        #endregion

        #region detail
        string mysqlPurcDetail = "select * from nt_1_sugarpurchasedetails where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        cmd = new SqlCommand(mysqlPurcDetail, mysqcon);
        returnVal = new SqlDataAdapter(mysqlPurcDetail, mysqcon);
        dt = new DataTable();
        returnVal.Fill(dt);


        string mysqlpurched = "select * from nt_1_sugarpurchase where  Company_Code=" + drpCompany.SelectedValue +
           " order by doc_no";
        SqlCommand cmdDO = new SqlCommand(mysqlpurched, mysqcon);
        SqlDataAdapter returnValDO = new SqlDataAdapter(mysqlpurched, mysqcon);
        DataTable dtDO = new DataTable();
        returnValDO.Fill(dtDO);



        string mysqlitem = "select * from nt_1_systemmaster where  Company_Code=" + drpCompany.SelectedValue +
           " and System_Type ='I' order by System_Code";
        SqlCommand CmdItem = new SqlCommand(mysqlitem, mysqcon);
        SqlDataAdapter returnpurce = new SqlDataAdapter(mysqlitem, mysqcon);
        DataTable mysqli = new DataTable();
        returnpurce.Fill(mysqli);

        string sqlpurce = "select * from NT_1_SugarPurchaseDetails where  Company_Code=" + drpCompany.SelectedValue +
           " order by doc_no";
        cmdsql = new SqlCommand(sqlpurce, sqlcon);
        returnValSQL = new SqlDataAdapter(sqlpurce, sqlcon);
        dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        dt3 = new DataTable();


        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("detail_Id", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tran_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("item_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration", typeof(string))));
        dt3.Columns.Add((new DataColumn("Quantal", typeof(string))));
        dt3.Columns.Add((new DataColumn("packing", typeof(string))));
        dt3.Columns.Add((new DataColumn("bags", typeof(string))));
        dt3.Columns.Add((new DataColumn("rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("item_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("purchasedetailid", typeof(string))));
        dt3.Columns.Add((new DataColumn("purchaseid", typeof(string))));
        dt3.Columns.Add((new DataColumn("ic", typeof(string))));
        columnname = "doc_no,detail_Id,Tran_Type,item_code,narration,Quantal,packing,bags,rate,item_Amount,Company_Code, " +
            " Year_Code,Branch_Code,Created_By,Modified_By,purchasedetailid,purchaseid,ic";

        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() +
                " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + " and detail_Id=" + dtsql.Rows[i]["detail_Id"].ToString() + "" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }

        }
        columnvalues = new StringBuilder();
        int purAutoid = 1;
        maxLavel = 0;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([purchasedetailid])", string.Empty));
            purAutoid = maxLavel + 1;
        }

        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                    string itemcode = dt3.Rows[i]["item_code"].ToString();
                    string doc_no = dtsql.Rows[i]["doc_no"].ToString();



                    DataRow[] drbsid = mysqli.Select("System_Code = " + itemcode + " and Company_Code=" + companycode);
                    string itemId = "2";
                    string headpurid = "0";

                    if (drbsid.Length > 0)
                    {
                        itemId = drbsid[0]["systemid"].ToString();
                    }

                    drbsid = dtDO.Select("doc_no = " + doc_no + " and Company_Code=" + companycode + " and Year_Code=" + groupcode);
                    if (drbsid.Length > 0)
                    {
                        headpurid = drbsid[0]["purchaseid"].ToString();
                    }
                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["detail_Id"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Tran_Type"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["item_code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["narration"].ToString() + "',");
                    if (dt3.Rows[i]["Quantal"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Quantal"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["packing"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["packing"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }

                    if (dt3.Rows[i]["bags"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["bags"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["item_Amount"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    if (dt3.Rows[i]["Branch_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    columnvalues.Append("'" + purAutoid + "',");
                    if (headpurid != "0")
                    {
                        columnvalues.Append("'" + headpurid + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("'" + itemId + "'");
                    columnvalues.Append(")");
                    purAutoid = purAutoid + 1;
                    string insert = "insert  into nt_1_sugarpurchasedetails (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    columnvalues = new StringBuilder();
                    //  ds = clsDAL.SimpleQuery(insert);
                }
                //string q = "update nt_1_sugarpurchasedetails as p, nt_1_systemmaster as i set p.ic = i.systemid where p.item_code=i.System_Code and i.System_Type='I'";
                //SqlCommand MySQLcmd = new SqlCommand(q, mysqcon, transaction);
                //MySQLcmd.ExecuteNonQuery();
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Purchase details Migration is done!');", true);
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                mysqcon.Close();
            }
        }
        #endregion

    }
    protected void btnService_Bill_Click(object sender, EventArgs e)
    {
        try
        {
            #region Head
            string purchesEntry = "select * from nt_1_rentbillhead where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
            SqlCommand cmd = new SqlCommand(purchesEntry, mysqcon);
            SqlDataAdapter returnVal = new SqlDataAdapter(purchesEntry, mysqcon);
            DataTable dt = new DataTable();
            returnVal.Fill(dt);

            string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
            SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
            SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
            DataTable dtmysqlaccountMaster = new DataTable();
            returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);

            string purchesentry = "select * from nt_1_rentbillhead where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
            SqlCommand cmdsql = new SqlCommand(purchesentry, sqlcon);
            SqlDataAdapter returnValSQL = new SqlDataAdapter(purchesentry, sqlcon);
            DataTable dtsql = new DataTable();
            returnValSQL.Fill(dtsql);

            DataTable dt3 = new DataTable();

            dt3.Columns.Add((new DataColumn("Doc_No", typeof(string))));
            dt3.Columns.Add((new DataColumn("Date", typeof(string))));
            dt3.Columns.Add((new DataColumn("Customer_Code", typeof(string))));
            dt3.Columns.Add((new DataColumn("GstRateCode", typeof(string))));
            dt3.Columns.Add((new DataColumn("Subtotal", typeof(string))));
            dt3.Columns.Add((new DataColumn("CGSTRate", typeof(string))));
            dt3.Columns.Add((new DataColumn("CGSTAmount", typeof(string))));
            dt3.Columns.Add((new DataColumn("SGSTRate", typeof(string))));
            dt3.Columns.Add((new DataColumn("SGSTAmount", typeof(string))));
            dt3.Columns.Add((new DataColumn("IGSTRate", typeof(string))));
            dt3.Columns.Add((new DataColumn("IGSTAmount", typeof(string))));
            dt3.Columns.Add((new DataColumn("Total", typeof(string))));
            dt3.Columns.Add((new DataColumn("Round_Off", typeof(string))));
            dt3.Columns.Add((new DataColumn("Final_Amount", typeof(string))));
            dt3.Columns.Add((new DataColumn("IsTDS", typeof(string))));
            dt3.Columns.Add((new DataColumn("TDS_Ac", typeof(string))));
            dt3.Columns.Add((new DataColumn("TDS_Per", typeof(string))));
            dt3.Columns.Add((new DataColumn("TDSAmount", typeof(string))));
            dt3.Columns.Add((new DataColumn("TDS", typeof(string))));
            dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
            dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
            dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
            dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
            dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
            dt3.Columns.Add((new DataColumn("billno", typeof(string))));
            dt3.Columns.Add((new DataColumn("cc", typeof(string))));
            dt3.Columns.Add((new DataColumn("ta", typeof(string))));
            dt3.Columns.Add((new DataColumn("rbid", typeof(string))));

            string columnname = "Doc_No,Date,Customer_Code,GstRateCode,Subtotal,CGSTRate,CGSTAmount,SGSTRate,SGSTAmount,IGSTRate,IGSTAmount,Total,Round_Off,Final_Amount" +
    ",IsTDS,TDS_Ac,TDS_Per,TDSAmount,TDS,Company_Code,Year_Code,Branch_Code,Created_By,Modified_By,billno,cc,ta,rbid";

            for (int i = 0; i < dtsql.Rows.Count; i++)
            {
                string companycode = dtsql.Rows[i]["Company_Code"].ToString();
                string doc_No = dtsql.Rows[i]["doc_no"].ToString();

                DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() + " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + "" +
                    " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

                if (dr.Length == 0)
                {
                    dt3.ImportRow(dtsql.Rows[i]);
                }
            }

            StringBuilder columnvalues = new StringBuilder();
            int purchesid = 1;

            int maxLavel;
            if (dt.Rows.Count > 0)
            {
                maxLavel = Convert.ToInt32(dt.Compute("max([rbid])", string.Empty));
                purchesid = maxLavel + 1;
            }
            if (dt3.Rows.Count > 0)
            {
                try
                {

                    mysqcon.ConnectionString = mysqlconn;
                    mysqcon.Open();
                    transaction = mysqcon.BeginTransaction();
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        string companycode = dt3.Rows[i]["Company_Code"].ToString();
                        string Yearcode = dt3.Rows[i]["Year_Code"].ToString();
                        string CustomerCode = dt3.Rows[i]["Customer_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Customer_Code"]) : "0";
                        string TDSAC = dt3.Rows[i]["TDS_Ac"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["TDS_Ac"]) : "0";

                        DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + CustomerCode + " and Company_Code=" + companycode);
                        string cid = "2";
                        string tdsid = "2";



                        if (drbsid.Length > 0)
                        {
                            cid = drbsid[0]["accoid"].ToString();
                        }

                        drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + TDSAC + " and Company_Code=" + companycode);
                        if (drbsid.Length > 0)
                        {
                            tdsid = drbsid[0]["accoid"].ToString();
                        }



                        columnvalues.Clear();
                        columnvalues.Append("(");
                        columnvalues.Append("'" + dt3.Rows[i]["Doc_No"].ToString() + "',");
                        columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");

                        columnvalues.Append("'" + dt3.Rows[i]["Customer_Code"].ToString() + "',");
                        if (dt3.Rows[i]["GstRateCode"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["GstRateCode"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("1,");
                        }
                        if (dt3.Rows[i]["Subtotal"].ToString() != string.Empty)
                        {

                            columnvalues.Append("'" + dt3.Rows[i]["Subtotal"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0.00,");
                        }
                        if (dt3.Rows[i]["CGSTRate"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["CGSTRate"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0.00,");
                        }
                        if (dt3.Rows[i]["CGSTAmount"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["CGSTAmount"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0.00,");
                        }
                        if (dt3.Rows[i]["SGSTRate"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["SGSTRate"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0.00,");
                        }
                        if (dt3.Rows[i]["SGSTAmount"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["SGSTAmount"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0.00,");
                        }
                        if (dt3.Rows[i]["IGSTRate"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["IGSTRate"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0.00,");
                        }
                        if (dt3.Rows[i]["IGSTAmount"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["IGSTAmount"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0.00,");
                        }
                        if (dt3.Rows[i]["Total"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["Total"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0.00,");
                        }
                        if (dt3.Rows[i]["Round_Off"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["Round_Off"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0.00,");
                        }
                        if (dt3.Rows[i]["Final_Amount"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["Final_Amount"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0.00,");
                        }

                        columnvalues.Append("'" + dt3.Rows[i]["IsTDS"].ToString() + "',");
                        if (dt3.Rows[i]["TDS_Ac"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["TDS_Ac"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0,");
                        }
                        if (dt3.Rows[i]["TDS_Per"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["TDS_Per"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0.00,");
                        }
                        if (dt3.Rows[i]["TDSAmount"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["TDSAmount"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0.00,");
                        }
                        if (dt3.Rows[i]["TDS"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["TDS"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0.00,");
                        }
                        columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                        columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                        if (dt3.Rows[i]["Branch_Code"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0,");
                        }
                        columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                        columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                        columnvalues.Append("'" + dt3.Rows[i]["billno"].ToString() + "',");
                        columnvalues.Append("'" + cid + "',");
                        columnvalues.Append("'" + tdsid + "',");
                        columnvalues.Append("'" + purchesid + "'");

                        columnvalues.Append(")");
                        purchesid = purchesid + 1;
                        string insert = "insert  into nt_1_rentbillhead (" + columnname + ") values " + columnvalues;

                        SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                        MySQLcmd1.ExecuteNonQuery();

                    }
                    transaction.Commit();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Service Bill is done!');", true);
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    mysqcon.Close();
                }

            }
            #endregion

            #region[Deatials]
            string SaleBillDetail = "select * from nt_1_rentbilldetails where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
            cmd = new SqlCommand(SaleBillDetail, mysqcon);
            returnVal = new SqlDataAdapter(SaleBillDetail, mysqcon);
            dt = new DataTable();
            returnVal.Fill(dt);



            mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
            cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
            dtmysqlaccountMaster = new DataTable();
            returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);


            string mysqlRentHead = "select * from nt_1_rentbillhead where Company_Code=" + drpCompany.SelectedValue + " and Year_Code=1";
            SqlCommand cmdmysqlRentHead = new SqlCommand(mysqlRentHead, mysqcon);
            SqlDataAdapter MymysqlRentHead = new SqlDataAdapter(mysqlRentHead, mysqcon);

            DataTable dtmysqlRentHead = new DataTable();
            MymysqlRentHead.Fill(dtmysqlRentHead);


            string mysqlItem_Master = "select * from nt_1_systemmaster where Company_Code=" + drpCompany.SelectedValue + " and System_Type='I'";
            SqlCommand cmdmysqlItem_Master = new SqlCommand(mysqlItem_Master, mysqcon);
            SqlDataAdapter MysqlDAItem = new SqlDataAdapter(mysqlItem_Master, mysqcon);

            DataTable dtmysqlItem_Master = new DataTable();
            MysqlDAItem.Fill(dtmysqlItem_Master);

            string SaleBillsql = "select * from nt_1_rentbilldetails where  Company_Code=" + drpCompany.SelectedValue +
                " order by doc_no";
            cmdsql = new SqlCommand(SaleBillsql, sqlcon);

            returnValSQL = new SqlDataAdapter(SaleBillsql, sqlcon);
            dtsql = new DataTable();
            returnValSQL.Fill(dtsql);
            dt3 = new DataTable();

            dt3.Columns.Add((new DataColumn("Doc_No", typeof(string))));
            dt3.Columns.Add((new DataColumn("Detail_Id", typeof(string))));
            dt3.Columns.Add((new DataColumn("Item_Code", typeof(string))));
            dt3.Columns.Add((new DataColumn("Description", typeof(string))));
            dt3.Columns.Add((new DataColumn("Amount", typeof(string))));
            dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
            dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
            dt3.Columns.Add((new DataColumn("ic", typeof(string))));
            dt3.Columns.Add((new DataColumn("rbid", typeof(string))));
            dt3.Columns.Add((new DataColumn("rbdid", typeof(string))));

            columnname = "Doc_No,Detail_Id,Item_Code,Description,Amount,Company_Code,Year_Code,ic,rbid,rbdid";


            for (int i = 0; i < dtsql.Rows.Count; i++)
            {
                string companycode = dtsql.Rows[i]["Company_Code"].ToString();
                string Tender_No = dtsql.Rows[i]["Doc_No"].ToString();

                DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["Doc_No"].ToString() +
                    " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + " and Detail_Id=" + dtsql.Rows[i]["Detail_Id"].ToString() + "" +
                    " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

                if (dr.Length == 0)
                {
                    dt3.ImportRow(dtsql.Rows[i]);
                }
            }


            columnvalues = new StringBuilder();
            int cityid = 1;
            maxLavel = 0;
            if (dt.Rows.Count > 0)
            {
                maxLavel = Convert.ToInt32(dt.Compute("max([rbdid])", string.Empty));
                cityid = maxLavel + 1;
            }
            DataSet ds = new DataSet();
            if (dt3.Rows.Count > 0)
            {
                try
                {

                    mysqcon.ConnectionString = mysqlconn;
                    mysqcon.Open();
                    transaction = mysqcon.BeginTransaction();
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        string companycode = dt3.Rows[i]["Company_Code"].ToString();
                        string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                        string docNO = dtsql.Rows[i]["Doc_No"].ToString();
                        string ItemCode = dtsql.Rows[i]["item_code"].ToString();


                        DataRow[] drbsid = dtmysqlItem_Master.Select("System_Code = " + ItemCode + " and Company_Code=" + companycode);
                        string ic = "2";
                        string RentId = "0";



                        if (drbsid.Length > 0)
                        {
                            ic = drbsid[0]["systemid"].ToString();
                        }

                        drbsid = dtmysqlRentHead.Select("doc_no = " + docNO + " and Company_Code=" + companycode + " and Year_Code=" + groupcode);
                        if (drbsid.Length > 0)
                        {
                            RentId = drbsid[0]["rbid"].ToString();
                        }




                        columnvalues.Clear();
                        columnvalues.Append("(");
                        columnvalues.Append("'" + dt3.Rows[i]["Doc_No"].ToString() + "',");
                        columnvalues.Append("'" + dt3.Rows[i]["Detail_Id"].ToString() + "',");
                        if (dt3.Rows[i]["Item_Code"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["Item_Code"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("1,");
                        }
                        columnvalues.Append("'" + dt3.Rows[i]["Description"].ToString() + "',");
                        if (dt3.Rows[i]["Amount"].ToString() != string.Empty)
                        {
                            columnvalues.Append("'" + dt3.Rows[i]["Amount"].ToString() + "',");
                        }
                        else
                        {
                            columnvalues.Append("0.00,");
                        }
                        columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                        columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                        columnvalues.Append("'" + ic + "',");
                        columnvalues.Append("'" + RentId + "',");
                        columnvalues.Append("'" + cityid + "'");

                        columnvalues.Append(")");
                        cityid = cityid + 1;
                        string insert = "insert  into nt_1_rentbilldetails (" + columnname + ") values " + columnvalues;

                        SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                        MySQLcmd1.ExecuteNonQuery();
                        //  ds = clsDAL.SimpleQuery(insert);
                    }
                    transaction.Commit();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('ServiceBill details Migration is done!');", true);
                }
                catch (Exception ex)
                {
                    return;
                }
                finally
                {
                    mysqcon.Close();
                }
            }
            #endregion
        }
        catch
        {
        }
    }

    protected void btnReciptPayment_Click(object sender, EventArgs e)
    {
        #region Head
        string mysqlReciptPayment = "select * from nt_1_transacthead where  Company_Code=" + drpCompany.SelectedValue + " and tran_type!='JV' order by doc_no";
        SqlCommand cmd = new SqlCommand(mysqlReciptPayment, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(mysqlReciptPayment, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);

        string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);

        string Reciptpaymentsql = "select distinct tran_type,doc_no,doc_date,debit_ac,Company_Code,Year_Code from NT_1_Transact where   Company_Code=" + drpCompany.SelectedValue + " and tran_type!='JV'  order by doc_no";
        SqlCommand cmdsql = new SqlCommand(Reciptpaymentsql, sqlcon);
        SqlDataAdapter returnValSQL = new SqlDataAdapter(Reciptpaymentsql, sqlcon);
        DataTable dtsql = new DataTable();
        returnValSQL.Fill(dtsql);


        DataTable dt3 = new DataTable();
        dt3.Columns.Add((new DataColumn("tran_type", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("debit_ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("total", typeof(string))));
        dt3.Columns.Add((new DataColumn("company_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("year_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("cb", typeof(string))));
        dt3.Columns.Add((new DataColumn("tranid", typeof(string))));
        string columnname = "tran_type,doc_no,doc_date,cashbank,total,company_code,year_code,cb,tranid";

        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string doc_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() +
                " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + " and Tran_Type='" + dtsql.Rows[i]["Tran_Type"].ToString() + "' " +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }

        StringBuilder columnvalues = new StringBuilder();
        int ReciptPayment = 1;

        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([tranid])", string.Empty));
            ReciptPayment = maxLavel + 1;
        }

        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string Yearcode = dt3.Rows[i]["Year_Code"].ToString();
                    string Cash = dt3.Rows[i]["debit_ac"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["debit_ac"]) : "0";

                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Cash + " and Company_Code=" + companycode);

                    string Ca = "0";



                    if (drbsid.Length > 0)
                    {
                        Ca = drbsid[0]["accoid"].ToString();
                    }



                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["tran_type"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["doc_no"].ToString() + ",");
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    if (dt3.Rows[i]["debit_ac"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["debit_ac"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["total"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["total"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    columnvalues.Append(dt3.Rows[i]["company_code"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    if (Ca != "0")
                    {
                        columnvalues.Append("'" + Ca + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("" + ReciptPayment + "");
                    columnvalues.Append(")");
                    ReciptPayment = ReciptPayment + 1;
                    string insert = "insert  into nt_1_transacthead (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();

                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Recipt Payment Migration is done!');", true);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mysqcon.Close();
            }

        }
        #endregion
        #region Detail
        string mysqlReciptPaymentHead = "select * from nt_1_transacthead where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmdhead = new SqlCommand(mysqlReciptPaymentHead, mysqcon);
        SqlDataAdapter returnVal1 = new SqlDataAdapter(mysqlReciptPaymentHead, mysqcon);
        DataTable dthead = new DataTable();

        returnVal1.Fill(dthead);

        string mysqlReciptPaymentDetail = "select * from nt_1_transactdetail where Company_Code=" + drpCompany.SelectedValue + " and tran_type!='JV' order by doc_no";
        SqlCommand cmdDeatil = new SqlCommand(mysqlReciptPaymentDetail, mysqcon);
        SqlDataAdapter returnVal2 = new SqlDataAdapter(mysqlReciptPaymentDetail, mysqcon);
        DataTable dtdetail = new DataTable();
        returnVal2.Fill(dtdetail);

        mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMasterq = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMasterq = new DataTable();
        returnValmysqlaccountMasterq.Fill(dtmysqlaccountMasterq);


        string sqlDO = "select * from NT_1_Transact where  Company_Code=" + drpCompany.SelectedValue +
           " and tran_type!='JV' order by doc_no";
        cmdsql = new SqlCommand(sqlDO, sqlcon);

        returnValSQL = new SqlDataAdapter(sqlDO, sqlcon);
        dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        dt3 = new DataTable();


        dt3.Columns.Add((new DataColumn("Tran_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("detail_id", typeof(string))));
        dt3.Columns.Add((new DataColumn("debit_ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("credit_ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("Unit_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration2", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("Adjusted_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tender_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("TenderDetail_ID", typeof(string))));
        dt3.Columns.Add((new DataColumn("drpFilterValue", typeof(string))));
        dt3.Columns.Add((new DataColumn("CreditAcAdjustedAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_name", typeof(string))));
        dt3.Columns.Add((new DataColumn("YearCodeDetail", typeof(string))));
        dt3.Columns.Add((new DataColumn("tranid", typeof(string))));
        dt3.Columns.Add((new DataColumn("ca", typeof(string))));
        dt3.Columns.Add((new DataColumn("uc", typeof(string))));
        dt3.Columns.Add((new DataColumn("tenderdetailid", typeof(string))));
        dt3.Columns.Add((new DataColumn("sbid", typeof(string))));
        dt3.Columns.Add((new DataColumn("da", typeof(string))));
        dt3.Columns.Add((new DataColumn("trandetailid", typeof(string))));
        dt3.Columns.Add((new DataColumn("drcr", typeof(string))));


        columnname = " Tran_Type,doc_no,doc_date,detail_id,debit_ac,credit_ac,Unit_Code,amount,narration,narration2,Company_Code,Year_Code," +
"Branch_Code,Created_By,Modified_By,Voucher_No,Voucher_Type,Adjusted_Amount,Tender_No,TenderDetail_ID,drpFilterValue,CreditAcAdjustedAmount," +
" Branch_name,YearCodeDetail,tranid,ca,uc,tenderdetailid,sbid,da,trandetailid,drcr";


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["doc_no"].ToString();
            string detailId = dtsql.Rows[i]["detail_Id"].ToString();

            DataRow[] dr = dtdetail.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() +
                " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() +
                " and detail_id=" + dtsql.Rows[i]["detail_id"].ToString() + " and Tran_Type='" + dtsql.Rows[i]["Tran_Type"].ToString() + "' " +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }

        }
        columnvalues = new StringBuilder();
        int ReciptId = 1;
        maxLavel = 0;
        if (dtdetail.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dtdetail.Compute("max([trandetailid])", string.Empty));
            ReciptId = maxLavel + 1;
        }

        if (dt3.Rows.Count > 0)
        {
            int i = 0;
            int j = 0;
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                    string doc_No = dt3.Rows[i]["doc_no"].ToString();
                    string TranType = dt3.Rows[i]["Tran_Type"].ToString();
                    string Ac_code = dt3.Rows[i]["credit_ac"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["credit_ac"]) : "0";
                    string Debit_Ac = dt3.Rows[i]["debit_ac"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["debit_ac"]) : "0";
                    string UntiCode = dtsql.Rows[i]["Unit_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Unit_Code"]) : "0";
                    string vNo = dtsql.Rows[i]["Voucher_No"].ToString();
                    string VType = dtsql.Rows[i]["Voucher_Type"].ToString();

                    DataRow[] drbsid = dtmysqlaccountMasterq.Select("Ac_code = " + Ac_code + " and Company_Code=" + companycode);
                    string Ac_Id = "0";
                    string Unit_Id = "0";
                    string headAutoid = "0";
                    string da = "0";

                    if (drbsid.Length > 0)
                    {
                        Ac_Id = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMasterq.Select("Ac_code = " + UntiCode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        Unit_Id = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMasterq.Select("Ac_code = " + Debit_Ac + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        da = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dthead.Select("doc_no = " + doc_No + " and Company_Code=" + companycode + " and Tran_Type='" + TranType + "'" +
                        " and Year_Code=" + groupcode);
                    if (drbsid.Length > 0)
                    {
                        headAutoid = drbsid[0]["tranid"].ToString();
                    }



                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["Tran_Type"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["detail_id"].ToString() + "',");
                    if (dt3.Rows[i]["debit_ac"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["debit_ac"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["credit_ac"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["credit_ac"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Unit_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Unit_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["narration"].ToString() != string.Empty)
                    {
                        string narration = dt3.Rows[i]["narration"].ToString();
                        narration = narration.Replace("'", "");
                        narration = narration.Replace(@"\", "");
                        columnvalues.Append("'" + narration + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["narration2"].ToString() != string.Empty)
                    {
                        string narration2 = dt3.Rows[i]["narration2"].ToString();
                        narration2 = narration2.Replace("'", "");
                        narration2 = narration2.Replace(@"\", "");
                        columnvalues.Append("'" + narration2 + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    if (dt3.Rows[i]["Voucher_No"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Voucher_No"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Voucher_Type"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Voucher_Type"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Adjusted_Amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Adjusted_Amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Tender_No"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Tender_No"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["TenderDetail_ID"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["TenderDetail_ID"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    if (dt3.Rows[i]["drpFilterValue"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["drpFilterValue"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["CreditAcAdjustedAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CreditAcAdjustedAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Branch_name"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Branch_name"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["YearCodeDetail"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["YearCodeDetail"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (headAutoid != "0")
                    {
                        columnvalues.Append("'" + headAutoid + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (Ac_Id != "0")
                    {
                        columnvalues.Append("'" + Ac_Id + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (Unit_Id != "0")
                    {
                        columnvalues.Append("'" + Unit_Id + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["tenderdetailid"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["tenderdetailid"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["sbid"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["sbid"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (da != "0")
                    {
                        columnvalues.Append("'" + da + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("'" + ReciptId + "',");
                    if (dt3.Rows[i]["drcr"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["drcr"].ToString() + "'");
                    }
                    else
                    {
                        columnvalues.Append("null");
                    }

                    columnvalues.Append(")");
                    ReciptId = ReciptId + 1;
                    string insert = "insert  into nt_1_transactdetail (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    //  ds = clsDAL.SimpleQuery(insert);
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Recipt Payment details Migration is done!');", true);
            }
            catch (Exception ex)
            {
                j = i;
                return;
            }
            finally
            {
                mysqcon.Close();
            }

        }



        #endregion
    }
    protected void btnReturn_Purchase_Click(object sender, EventArgs e)
    {
        #region Head
        string purchesEntry = "select * from nt_1_sugarpurchasereturn where Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmd = new SqlCommand(purchesEntry, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(purchesEntry, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);

        string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);

        string purchesentry = "select * from nt_1_sugarpurchasereturn where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmdsql = new SqlCommand(purchesentry, sqlcon);
        SqlDataAdapter returnValSQL = new SqlDataAdapter(purchesentry, sqlcon);
        DataTable dtsql = new DataTable();
        returnValSQL.Fill(dtsql);

        if (dtsql.Rows.Count > 0)
        {
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('No record Found!');", true);
            return;
        }

        string saleEntry = "select * from nt_1_sugarsale where Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        cmd = new SqlCommand(saleEntry, mysqcon);
        SqlDataAdapter saleDa = new SqlDataAdapter(saleEntry, mysqcon);
        DataTable saleHead = new DataTable();
        saleDa.Fill(saleHead);

        DataTable dt3 = new DataTable();
        #region Column
        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("PURCNO", typeof(string))));
        dt3.Columns.Add((new DataColumn("PurcTranType", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tran_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Ac_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Unit_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("mill_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("FROM_STATION", typeof(string))));
        dt3.Columns.Add((new DataColumn("TO_STATION", typeof(string))));
        dt3.Columns.Add((new DataColumn("LORRYNO", typeof(string))));
        dt3.Columns.Add((new DataColumn("BROKER", typeof(string))));
        dt3.Columns.Add((new DataColumn("wearhouse", typeof(string))));
        dt3.Columns.Add((new DataColumn("subTotal", typeof(string))));
        dt3.Columns.Add((new DataColumn("LESS_FRT_RATE", typeof(string))));
        dt3.Columns.Add((new DataColumn("freight", typeof(string))));
        dt3.Columns.Add((new DataColumn("cash_advance", typeof(string))));
        dt3.Columns.Add((new DataColumn("bank_commission", typeof(string))));
        dt3.Columns.Add((new DataColumn("OTHER_AMT", typeof(string))));
        dt3.Columns.Add((new DataColumn("Bill_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Due_Days", typeof(string))));
        dt3.Columns.Add((new DataColumn("NETQNTL", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Bill_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("CGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("CGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("SGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("SGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("IGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("IGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("GstRateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("purcyearcode", typeof(string))));
        dt3.Columns.Add((new DataColumn("Bill_To", typeof(string))));
        dt3.Columns.Add((new DataColumn("prid", typeof(string))));
        dt3.Columns.Add((new DataColumn("srid", typeof(string))));
        dt3.Columns.Add((new DataColumn("ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("uc", typeof(string))));
        dt3.Columns.Add((new DataColumn("mc", typeof(string))));
        dt3.Columns.Add((new DataColumn("bc", typeof(string))));
        dt3.Columns.Add((new DataColumn("bt", typeof(string))));
        dt3.Columns.Add((new DataColumn("sbid", typeof(string))));
        #endregion


        string columnname = "doc_no,PURCNO,PurcTranType,Tran_Type,doc_date,Ac_Code,Unit_Code,mill_code,FROM_STATION,TO_STATION,LORRYNO,BROKER,wearhouse" +
",subTotal,LESS_FRT_RATE,freight,cash_advance,bank_commission,OTHER_AMT,Bill_Amount,Due_Days,NETQNTL,Company_Code,Year_Code" +
",Branch_Code,Created_By,Modified_By,Bill_No,CGSTRate,CGSTAmount,SGSTRate,SGSTAmount,IGSTRate,IGSTAmount,GstRateCode,purcyearcode" +
",Bill_To,prid,srid,ac,uc,mc,bc,bt,sbid";

        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string doc_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() + " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + "" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }

        StringBuilder columnvalues = new StringBuilder();
        int purchesid = 1;

        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([prid])", string.Empty));
            purchesid = maxLavel + 1;
        }
        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string Yearcode = dt3.Rows[i]["Year_Code"].ToString();
                    string fromCode = dt3.Rows[i]["Ac_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Ac_Code"]) : "0";
                    string Unit_Code = dt3.Rows[i]["Unit_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Unit_Code"]) : "0";
                    string Mill_Code = dt3.Rows[i]["mill_code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["mill_code"]) : "0";
                    string brokerCode = dt3.Rows[i]["BROKER"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["BROKER"]) : "0";
                    string BillTO = dt3.Rows[i]["Bill_To"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Bill_To"]) : "0";
                    string purcheNo = dt3.Rows[i]["PURCNO"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["PURCNO"]) : "0";


                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Mill_Code + " and Company_Code=" + companycode);
                    string mc = "0";
                    string uc = "0";
                    string bc = "0";
                    string ac = "0";
                    string billid = "0";
                    string pid = "0";


                    if (drbsid.Length > 0)
                    {
                        mc = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + BillTO + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        billid = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + fromCode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        ac = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Unit_Code + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        uc = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + brokerCode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        bc = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = saleHead.Select("doc_no = " + purcheNo + " and Company_Code=" + companycode + " and Year_Code=" + Yearcode);
                    if (drbsid.Length > 0)
                    {
                        pid = drbsid[0]["saleid"].ToString();
                    }

                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    if (dt3.Rows[i]["PURCNO"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["PURCNO"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["PurcTranType"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Tran_Type"].ToString() + "',");
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    if (dt3.Rows[i]["Ac_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Ac_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Unit_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Unit_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["mill_code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["mill_code"].ToString() + "',");

                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["FROM_STATION"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["TO_STATION"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["LORRYNO"].ToString() + "',");
                    if (dt3.Rows[i]["BROKER"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["BROKER"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["wearhouse"].ToString() + "',");
                    if (dt3.Rows[i]["subTotal"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["subTotal"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["LESS_FRT_RATE"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["LESS_FRT_RATE"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["freight"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["freight"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["cash_advance"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["cash_advance"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["bank_commission"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["bank_commission"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["OTHER_AMT"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["OTHER_AMT"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["Bill_Amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Bill_Amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["Due_Days"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Due_Days"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["NETQNTL"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["NETQNTL"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    if (dt3.Rows[i]["Branch_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Bill_No"].ToString() + "',");
                    if (dt3.Rows[i]["CGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["CGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CGSTAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["SGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["SGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SGSTAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["IGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["IGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["IGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["IGSTAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["GstRateCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["GstRateCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["purcyearcode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["purcyearcode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Bill_To"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Bill_To"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + purchesid + "',");
                    columnvalues.Append("null,");
                    if (ac != "0")
                    {
                        columnvalues.Append("'" + ac + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (uc != "0")
                    {
                        columnvalues.Append("'" + uc + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (mc != "0")
                    {
                        columnvalues.Append("'" + mc + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (bc != "0")
                    {
                        columnvalues.Append("'" + bc + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (billid != "0")
                    {
                        columnvalues.Append("'" + billid + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (pid != "0")
                    {
                        columnvalues.Append("'" + pid + "'");
                    }
                    else
                    {
                        columnvalues.Append("null");
                    }

                    columnvalues.Append(")");
                    purchesid = purchesid + 1;
                    string insert = "insert  into nt_1_sugarpurchasereturn (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();

                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Purches Return Migration is done!');", true);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mysqcon.Close();
            }

        }
        #endregion

        #region detail
        string mysqlPurcDetail = "select * from nt_1_sugarpurchasedetailsreturn where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        cmd = new SqlCommand(mysqlPurcDetail, mysqcon);
        returnVal = new SqlDataAdapter(mysqlPurcDetail, mysqcon);
        dt = new DataTable();
        returnVal.Fill(dt);


        string mysqlpurched = "select * from nt_1_sugarpurchasereturn where  Company_Code=" + drpCompany.SelectedValue +
           " order by doc_no";
        SqlCommand cmdDO = new SqlCommand(mysqlpurched, mysqcon);
        SqlDataAdapter returnValDO = new SqlDataAdapter(mysqlpurched, mysqcon);
        DataTable dtpUrchaseReturn = new DataTable();
        returnValDO.Fill(dtpUrchaseReturn);



        string mysqlitem = "select * from nt_1_systemmaster where  Company_Code=" + drpCompany.SelectedValue +
           " and System_Type ='I' order by System_Code";
        SqlCommand CmdItem = new SqlCommand(mysqlitem, mysqcon);
        SqlDataAdapter returnpurce = new SqlDataAdapter(mysqlitem, mysqcon);
        DataTable mysqli = new DataTable();
        returnpurce.Fill(mysqli);

        string sqlpurce = "select * from nt_1_sugarpurchasedetailsreturn where  Company_Code=" + drpCompany.SelectedValue +
           " order by doc_no";
        cmdsql = new SqlCommand(sqlpurce, sqlcon);
        returnValSQL = new SqlDataAdapter(sqlpurce, sqlcon);
        dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        dt3 = new DataTable();

        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("detail_id", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tran_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("item_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration", typeof(string))));
        dt3.Columns.Add((new DataColumn("Quantal", typeof(string))));
        dt3.Columns.Add((new DataColumn("packing", typeof(string))));
        dt3.Columns.Add((new DataColumn("bags", typeof(string))));
        dt3.Columns.Add((new DataColumn("rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("item_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("ic", typeof(string))));
        dt3.Columns.Add((new DataColumn("prdid", typeof(string))));
        dt3.Columns.Add((new DataColumn("prid", typeof(string))));

        columnname = "doc_no,detail_id,Tran_Type,item_code,narration,Quantal,packing,bags,rate,item_Amount,Company_Code,Year_Code," +
            "Branch_Code,Created_By,Modified_By,ic,prdid,prid";

        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() +
                " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + " and detail_Id=" + dtsql.Rows[i]["detail_Id"].ToString() + "" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }

        }
        columnvalues = new StringBuilder();
        int purAutoid = 1;
        maxLavel = 0;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([prdid])", string.Empty));
            purAutoid = maxLavel + 1;
        }

        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                    string itemcode = dt3.Rows[i]["item_code"].ToString();
                    string doc_no = dtsql.Rows[i]["doc_no"].ToString();



                    DataRow[] drbsid = mysqli.Select("System_Code = " + itemcode + " and Company_Code=" + companycode);
                    string itemId = "2";
                    string headpurid = "2";

                    if (drbsid.Length > 0)
                    {
                        itemId = drbsid[0]["systemid"].ToString();
                    }

                    drbsid = dtpUrchaseReturn.Select("doc_no = " + doc_no + " and Company_Code=" + companycode + " and Year_Code=" + groupcode);
                    if (drbsid.Length > 0)
                    {
                        headpurid = drbsid[0]["prid"].ToString();
                    }
                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["detail_id"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Tran_Type"].ToString() + "',");
                    if (dt3.Rows[i]["item_code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["item_code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("1,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["narration"].ToString() + "',");
                    if (dt3.Rows[i]["Quantal"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Quantal"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["packing"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["packing"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["bags"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["bags"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["item_Amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["item_Amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    if (dt3.Rows[i]["Branch_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    columnvalues.Append("'" + itemId + "',");
                    columnvalues.Append("'" + purAutoid + "',");
                    columnvalues.Append("'" + headpurid + "'");

                    columnvalues.Append(")");
                    purAutoid = purAutoid + 1;
                    string insert = "insert  into nt_1_sugarpurchasedetailsreturn (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    //  ds = clsDAL.SimpleQuery(insert);
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Tender details Migration is done!');", true);
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                mysqcon.Close();
            }
        }
        #endregion
    }

    protected void btnsalereturn_Click(object sender, EventArgs e)
    {
        #region Head
        string mysqlsugarsalereutun = "select * from nt_1_sugarsalereturn where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmd = new SqlCommand(mysqlsugarsalereutun, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(mysqlsugarsalereutun, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);


        string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);


        string sugarsalereutn = "select * from NT_1_SugarSaleReturn where  Company_Code=" + drpCompany.SelectedValue + "  order by doc_no";
        SqlCommand cmdsql = new SqlCommand(sugarsalereutn, sqlcon);

        SqlDataAdapter returnValSQL = new SqlDataAdapter(sugarsalereutn, sqlcon);
        DataTable dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        DataTable dt3 = new DataTable();

        if (dtsql.Rows.Count > 0)
        {
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('No record Found!');", true);
            return;
        }

        #region Mysql sugar sale return Table Column
        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("PURCNO", typeof(string))));
        dt3.Columns.Add((new DataColumn("PurcTranType", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Ac_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Unit_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("mill_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("FROM_STATION", typeof(string))));
        dt3.Columns.Add((new DataColumn("TO_STATION", typeof(string))));
        dt3.Columns.Add((new DataColumn("LORRYNO", typeof(string))));
        dt3.Columns.Add((new DataColumn("BROKER", typeof(string))));
        dt3.Columns.Add((new DataColumn("wearhouse", typeof(string))));
        dt3.Columns.Add((new DataColumn("subTotal", typeof(string))));
        dt3.Columns.Add((new DataColumn("LESS_FRT_RATE", typeof(string))));
        dt3.Columns.Add((new DataColumn("freight", typeof(string))));
        dt3.Columns.Add((new DataColumn("cash_advance", typeof(string))));
        dt3.Columns.Add((new DataColumn("bank_commission", typeof(string))));
        dt3.Columns.Add((new DataColumn("OTHER_AMT", typeof(string))));
        dt3.Columns.Add((new DataColumn("Bill_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Due_Days", typeof(string))));
        dt3.Columns.Add((new DataColumn("NETQNTL", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tran_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("DO_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Transport_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("CGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("CGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("SGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("SGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("IGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("IGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("GstRateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("purcyearcode", typeof(string))));
        dt3.Columns.Add((new DataColumn("ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("uc", typeof(string))));
        dt3.Columns.Add((new DataColumn("mc", typeof(string))));
        dt3.Columns.Add((new DataColumn("bc", typeof(string))));
        dt3.Columns.Add((new DataColumn("srid", typeof(string))));
        dt3.Columns.Add((new DataColumn("sbid", typeof(string))));
        dt3.Columns.Add((new DataColumn("bill_to", typeof(string))));
        dt3.Columns.Add((new DataColumn("bt", typeof(string))));
        dt3.Columns.Add((new DataColumn("gc", typeof(string))));
        dt3.Columns.Add((new DataColumn("tc", typeof(string))));
        dt3.Columns.Add((new DataColumn("FromAc", typeof(string))));
        dt3.Columns.Add((new DataColumn("fa", typeof(string))));
        #endregion

        string columnname = "doc_no,PURCNO,doc_date,PurcTranType,Ac_Code,Unit_Code,mill_code,FROM_STATION,TO_STATION,LORRYNO,BROKER," +
          " wearhouse,subTotal,LESS_FRT_RATE,freight,cash_advance,bank_commission,OTHER_AMT,Bill_Amount,Due_Days,NETQNTL,Company_Code " +
          ",Year_Code,Branch_Code,Created_By,Modified_By,Tran_Type,DO_No,Transport_Code,CGSTRate,CGSTAmount,SGSTRate,SGSTAmount,IGSTRate,IGSTAmount,GstRateCode," +
          " purcyearcode,ac,uc,mc,bc,srid,sbid,bill_to,bt,gc,tc,FromAc,fa";

        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() + " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + "" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }


        StringBuilder columnvalues = new StringBuilder();
        int saleid = 1;
        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([srid])", string.Empty));
            saleid = maxLavel + 1;
        }
        DataSet ds = new DataSet();
        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string Yearcode = dt3.Rows[i]["Year_Code"].ToString();
                    string partCode = dt3.Rows[i]["Ac_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Ac_Code"]) : "0";
                    string Unit_Code = dt3.Rows[i]["Unit_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Unit_Code"]) : "0";
                    string Mill_Code = dt3.Rows[i]["mill_code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["mill_code"]) : "0";
                    string brokerCode = dt3.Rows[i]["BROKER"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["BROKER"]) : "0";
                    string fromac = dt3.Rows[i]["FromAc"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["FromAc"]) : "0";
                    string trnsportCode = dt3.Rows[i]["Transport_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Transport_Code"]) : "0";
                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Mill_Code + " and Company_Code=" + companycode);
                    string mc = "0";
                    string uc = "0";
                    string bc = "0";
                    string ac = "0";
                    string fa = "0";
                    string tc = "0";


                    if (drbsid.Length > 0)
                    {
                        mc = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + partCode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        ac = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Unit_Code + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        uc = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + brokerCode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        bc = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + fromac + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        fa = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + trnsportCode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        tc = drbsid[0]["accoid"].ToString();
                    }


                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    if (dt3.Rows[i]["PURCNO"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["PURCNO"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    if (dt3.Rows[i]["PurcTranType"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["PurcTranType"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Ac_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Ac_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Unit_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Unit_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["mill_code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["mill_code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["FROM_STATION"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["FROM_STATION"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["TO_STATION"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["TO_STATION"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["LORRYNO"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["LORRYNO"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["BROKER"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["BROKER"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["wearhouse"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["wearhouse"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }


                    if (dt3.Rows[i]["subTotal"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["subTotal"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["LESS_FRT_RATE"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["LESS_FRT_RATE"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["freight"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["freight"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["cash_advance"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["cash_advance"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["bank_commission"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["bank_commission"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    if (dt3.Rows[i]["OTHER_AMT"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["OTHER_AMT"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Bill_Amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Bill_Amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Due_Days"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Due_Days"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["NETQNTL"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["NETQNTL"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");

                    columnvalues.Append("'" + dt3.Rows[i]["Tran_Type"].ToString() + "',");
                    if (dt3.Rows[i]["DO_No"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["DO_No"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Transport_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Transport_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["CGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["CGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CGSTAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["SGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["SGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SGSTAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["IGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["IGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["IGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["IGSTAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }


                    if (dt3.Rows[i]["GstRateCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["GstRateCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["purcyearcode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["purcyearcode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (ac != "0")
                    {
                        columnvalues.Append("'" + ac + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (uc != "0")
                    {
                        columnvalues.Append("'" + uc + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (mc != "0")
                    {
                        columnvalues.Append("'" + mc + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (bc != "0")
                    {
                        columnvalues.Append("'" + bc + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("'" + saleid + "',");
                    if (dt3.Rows[i]["sbid"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["sbid"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["bill_to"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["bill_to"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["bt"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["bt"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["gc"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["gc"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (tc != "0")
                    {
                        columnvalues.Append("'" + tc + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["FromAc"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["FromAc"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (fa != "0")
                    {
                        columnvalues.Append("" + fa + "");
                    }
                    else
                    {
                        columnvalues.Append("null");
                    }
                    columnvalues.Append(")");
                    saleid = saleid + 1;
                    string insert = "insert  into nt_1_sugarsalereturn (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();

                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('sugar sale return head Migration is done!');", true);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mysqcon.Close();
            }

        }
        #endregion
        #region detail
        string mysqlsalereDetail = "select * from nt_1_sugarsaledetailsreturn where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        cmd = new SqlCommand(mysqlsalereDetail, mysqcon);
        returnVal = new SqlDataAdapter(mysqlsalereDetail, mysqcon);
        dt = new DataTable();
        returnVal.Fill(dt);


        string mysqlsalerehed = "select * from nt_1_sugarsalereturn where  Company_Code=" + drpCompany.SelectedValue +
           " order by doc_no";
        SqlCommand cmdDO = new SqlCommand(mysqlsalerehed, mysqcon);
        SqlDataAdapter returnValDO = new SqlDataAdapter(mysqlsalerehed, mysqcon);
        DataTable dtDO = new DataTable();
        returnValDO.Fill(dtDO);



        string mysqlitem = "select * from nt_1_systemmaster where  Company_Code=" + drpCompany.SelectedValue +
           " and System_Type ='I' order by System_Code";
        SqlCommand CmdItem = new SqlCommand(mysqlitem, mysqcon);
        SqlDataAdapter returnpurce = new SqlDataAdapter(mysqlitem, mysqcon);
        DataTable mysqli = new DataTable();
        returnpurce.Fill(mysqli);

        string sqlsalere = "select * from NT_1_sugarsaleDetailsReturn where  Company_Code=" + drpCompany.SelectedValue +
           " order by doc_no";
        cmdsql = new SqlCommand(sqlsalere, sqlcon);
        returnValSQL = new SqlDataAdapter(sqlsalere, sqlcon);
        dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        dt3 = new DataTable();


        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("detail_Id", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tran_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("item_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration", typeof(string))));
        dt3.Columns.Add((new DataColumn("Quantal", typeof(string))));
        dt3.Columns.Add((new DataColumn("packing", typeof(string))));
        dt3.Columns.Add((new DataColumn("bags", typeof(string))));
        dt3.Columns.Add((new DataColumn("rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("item_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("srid", typeof(string))));
        dt3.Columns.Add((new DataColumn("srdtid", typeof(string))));
        dt3.Columns.Add((new DataColumn("ic", typeof(string))));
        columnname = "doc_no,detail_Id,Tran_Type,item_code,narration,Quantal,packing,bags,rate,item_Amount,Company_Code, " +
            " Year_Code,Branch_Code,Created_By,Modified_By,srid,srdtid,ic";

        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() +
                " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + " and detail_Id=" + dtsql.Rows[i]["detail_Id"].ToString() + "" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }

        }
        columnvalues = new StringBuilder();
        int saleheadAutoid = 1;
        maxLavel = 0;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([srdtid])", string.Empty));
            saleheadAutoid = maxLavel + 1;
        }

        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                    string itemcode = dt3.Rows[i]["item_code"].ToString();
                    string doc_no = dtsql.Rows[i]["doc_no"].ToString();



                    DataRow[] drbsid = mysqli.Select("System_Code = " + itemcode + " and Company_Code=" + companycode);
                    string itemId = "2";
                    string headpurid = "2";

                    if (drbsid.Length > 0)
                    {
                        itemId = drbsid[0]["systemid"].ToString();
                    }

                    drbsid = dtDO.Select("doc_no = " + doc_no + " and Company_Code=" + companycode + " and Year_Code=" + groupcode);
                    if (drbsid.Length > 0)
                    {
                        headpurid = drbsid[0]["srid"].ToString();
                    }
                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["detail_Id"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Tran_Type"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["item_code"].ToString() + "',");
                    if (dt3.Rows[i]["narration"].ToString() != string.Empty)
                    {
                        string n = dt3.Rows[i]["narration"].ToString();
                        n = n.Replace("'", "");
                        columnvalues.Append("'" + n + "',");

                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["Quantal"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["packing"].ToString() + "',");

                    columnvalues.Append("'" + dt3.Rows[i]["bags"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["rate"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["item_Amount"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    columnvalues.Append("'" + headpurid + "',");
                    columnvalues.Append("'" + saleheadAutoid + "',");
                    columnvalues.Append("'" + itemId + "'");
                    columnvalues.Append(")");
                    saleheadAutoid = saleheadAutoid + 1;
                    string insert = "insert  into nt_1_sugarsaledetailsreturn (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    //  ds = clsDAL.SimpleQuery(insert);
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('sugar sale return details Migration is done!');", true);
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                mysqcon.Close();
            }
        }
        #endregion
    }
    protected void btnLV_Click(object sender, EventArgs e)
    {
        #region[Head]
        string CommisionBill = "select * from commission_bill where  " +
            " Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmd = new SqlCommand(CommisionBill, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(CommisionBill, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);


        string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);


        string Commissionsql = "select * from NT_1_Voucher where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmdsql = new SqlCommand(Commissionsql, sqlcon);

        SqlDataAdapter returnValSQL = new SqlDataAdapter(Commissionsql, sqlcon);
        DataTable dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        DataTable dt3 = new DataTable();

        if (dtsql.Rows.Count > 0)
        {
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('No record Found!');", true);
            return;
        }
        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("link_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("link_type", typeof(string))));
        dt3.Columns.Add((new DataColumn("link_id", typeof(string))));
        dt3.Columns.Add((new DataColumn("ac_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("unit_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("broker_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("quantal", typeof(string))));
        dt3.Columns.Add((new DataColumn("packing", typeof(string))));
        dt3.Columns.Add((new DataColumn("bags", typeof(string))));
        dt3.Columns.Add((new DataColumn("grade", typeof(string))));
        dt3.Columns.Add((new DataColumn("transport_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("mill_rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("sale_rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("Purchase_Rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("commission_amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("resale_rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("resale_commission", typeof(string))));
        dt3.Columns.Add((new DataColumn("misc_amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("TaxableAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("GstRateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("cgstrate", typeof(string))));
        dt3.Columns.Add((new DataColumn("cgstamount", typeof(string))));
        dt3.Columns.Add((new DataColumn("sgstrate", typeof(string))));
        dt3.Columns.Add((new DataColumn("sgstamount", typeof(string))));
        dt3.Columns.Add((new DataColumn("igstrate", typeof(string))));
        dt3.Columns.Add((new DataColumn("igstamount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("commissionid", typeof(string))));
        dt3.Columns.Add((new DataColumn("ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("uc", typeof(string))));
        dt3.Columns.Add((new DataColumn("bc", typeof(string))));
        dt3.Columns.Add((new DataColumn("tc", typeof(string))));
        dt3.Columns.Add((new DataColumn("mill_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("mc", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration1", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration2", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration3", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration4", typeof(string))));


        string columnname = "doc_no,doc_date,link_no,link_type,link_id,ac_code,unit_code,broker_code,qntl,packing,bags,grade,transport_code,mill_rate,sale_rate" +
",purc_rate,commission_amount,resale_rate,resale_commission,misc_amount,texable_amount,gst_code,cgst_rate,cgst_amount,sgst_rate" +
",sgst_amount,igst_rate,igst_amount,bill_amount,Company_Code,Year_Code,Branch_Code,Created_By,Modified_By,commissionid,ac,uc" +
",bc,tc,mill_code,mc,narration1,narration2,narration3,narration4";


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() + " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + "" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }


        StringBuilder columnvalues = new StringBuilder();
        int cityid = 1;
        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([commissionid])", string.Empty));
            cityid = maxLavel + 1;
        }
        int j = 0;
        DataSet ds = new DataSet();
        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();

                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                    string Mill_Code = dt3.Rows[i]["mill_code"].ToString();
                    string AcCode = dt3.Rows[i]["ac_code"].ToString();
                    string UnitCode = dt3.Rows[i]["unit_code"].ToString() != string.Empty ? dt3.Rows[i]["unit_code"].ToString() : "0";
                    string BrokerCode = dt3.Rows[i]["broker_code"].ToString() != string.Empty ? dt3.Rows[i]["broker_code"].ToString() : "0";
                    string Transport = dt3.Rows[i]["transport_code"].ToString() != string.Empty ? dt3.Rows[i]["transport_code"].ToString() : "0";

                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Mill_Code + " and Company_Code=" + companycode);
                    string mc = "0";
                    string ac = "0";
                    string uc = "0";
                    string bc = "0";
                    string tc = "0";



                    if (drbsid.Length > 0)
                    {
                        mc = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + AcCode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        ac = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + UnitCode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        uc = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + BrokerCode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        bc = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Transport + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        tc = drbsid[0]["accoid"].ToString();
                    }

                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");

                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    columnvalues.Append("0,");
                    columnvalues.Append("null,");
                    columnvalues.Append("null,");
                    if (dt3.Rows[i]["ac_code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["ac_code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["unit_code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["unit_code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["broker_code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["broker_code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["quantal"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["quantal"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["packing"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["packing"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["bags"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["bags"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["grade"].ToString() + "',");
                    if (dt3.Rows[i]["transport_code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["transport_code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["mill_rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["mill_rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["sale_rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["sale_rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["purchase_rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["purchase_rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["commission_amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["commission_amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["resale_rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["resale_rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["resale_commission"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["resale_commission"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["misc_amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["misc_amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["TaxableAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["TaxableAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["GstRateCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["GstRateCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["cgstrate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["cgstrate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["cgstamount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["cgstamount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["sgstrate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["sgstrate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["sgstamount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["sgstamount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["igstrate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["igstrate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["igstamount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["igstamount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    if (dt3.Rows[i]["Voucher_Amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Voucher_Amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0.00,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    if (dt3.Rows[i]["Branch_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    columnvalues.Append("'" + cityid + "',");
                    if (ac != "0")
                    {
                        columnvalues.Append("'" + ac + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (uc != "0")
                    {
                        columnvalues.Append("'" + uc + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (bc != "0")
                    {
                        columnvalues.Append("'" + bc + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (tc != "0")
                    {
                        columnvalues.Append("'" + tc + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["mill_code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["mill_code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (mc != "0")
                    {
                        columnvalues.Append("'" + mc + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["narration1"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["narration2"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["narration3"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["narration4"].ToString() + "'");
                    columnvalues.Append(")");
                    cityid = cityid + 1;
                    string insert = "insert  into commission_bill (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    j = i;
                    //  ds = clsDAL.SimpleQuery(insert);
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('LV Migration is done!');", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("" + j + "");
                return;
            }
            finally
            {
                mysqcon.Close();
            }
        }
        #endregion
    }

    protected void btnRatilsale_Click(object sender, EventArgs e)
    {
        #region Head
        string mysqlsugarsalereutun = "select * from nt_1_retailsalehead where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmd = new SqlCommand(mysqlsugarsalereutun, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(mysqlsugarsalereutun, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);


        string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);


        string sugarsalereutn = "select * from NT_1_RetailSaleHead where Company_Code=" + drpCompany.SelectedValue + "  order by doc_no";
        SqlCommand cmdsql = new SqlCommand(sugarsalereutn, sqlcon);

        SqlDataAdapter returnValSQL = new SqlDataAdapter(sugarsalereutn, sqlcon);
        DataTable dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        DataTable dt3 = new DataTable();

        if (dtsql.Rows.Count > 0)
        {
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('No record Found!');", true);
            return;
        }

        #region Mysql sugar sale return Table Column
        dt3.Columns.Add((new DataColumn("Tran_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Challan_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Challan_Date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Vehical_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Party_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Due_Days", typeof(string))));
        dt3.Columns.Add((new DataColumn("Due_Date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Total", typeof(string))));
        dt3.Columns.Add((new DataColumn("Subtotal", typeof(string))));
        dt3.Columns.Add((new DataColumn("Vat", typeof(string))));
        dt3.Columns.Add((new DataColumn("Round_Off", typeof(string))));
        dt3.Columns.Add((new DataColumn("Grand_Total", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));

        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Delivered", typeof(string))));
        dt3.Columns.Add((new DataColumn("GstRateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("CGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("CGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("SGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("SGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("IGSTRate", typeof(string))));
        dt3.Columns.Add((new DataColumn("IGSTAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("TaxableAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Party_Name", typeof(string))));
        dt3.Columns.Add((new DataColumn("Broker_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("HamaliAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("CashRecive", typeof(int))));
        dt3.Columns.Add((new DataColumn("Party_Name_New", typeof(string))));
        dt3.Columns.Add((new DataColumn("ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("bc", typeof(string))));
        dt3.Columns.Add((new DataColumn("retailid", typeof(string))));
        dt3.Columns.Add((new DataColumn("Narration", typeof(string))));
        #endregion

        string columnname = " Tran_Type, doc_no, doc_date, Challan_No, Challan_Date, Vehical_No, Party_Code, Due_Days, Due_Date, Total, Subtotal, Vat, Round_Off, Grand_Total,"
            + " Company_Code, Year_Code, Created_By, Modified_By, Branch_Code, Delivered, GstRateCode, CGSTRate, CGSTAmount, SGSTRate, SGSTAmount, IGSTRate, IGSTAmount, TaxableAmount, Party_Name, Broker_Code, HamaliAmount, CashRecive, Party_Name_New, ac, bc, retailid";


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() + " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + "" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }


        StringBuilder columnvalues = new StringBuilder();
        int ratailid = 1;
        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([retailid])", string.Empty));
            ratailid = maxLavel + 1;
        }
        DataSet ds = new DataSet();
        if (dt3.Rows.Count > 0)
        {
            try
            {


                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string Yearcode = dt3.Rows[i]["Year_Code"].ToString();
                    // string to = dt3.Rows[i]["Ac_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Ac_Code"]) : "0";
                    string partname = dt3.Rows[i]["Party_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Party_Code"]) : "0";
                    string brokerCode = dt3.Rows[i]["Broker_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Broker_Code"]) : "0";
                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + brokerCode + " and Company_Code=" + companycode);
                    string PN = "0";
                    string Bc = "0";



                    if (drbsid.Length > 0)
                    {
                        PN = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + partname + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        Bc = drbsid[0]["accoid"].ToString();
                    }


                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["Tran_Type"].ToString() + "',");
                    if (dt3.Rows[i]["doc_no"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    if (dt3.Rows[i]["Challan_No"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Challan_No"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["Challan_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    if (dt3.Rows[i]["Vehical_No"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Vehical_No"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Party_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Party_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Due_Days"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Due_Days"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["Due_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");

                    if (dt3.Rows[i]["Total"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Total"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Subtotal"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Subtotal"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Vat"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Vat"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Round_Off"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Round_Off"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Grand_Total"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Grand_Total"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");

                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    if (dt3.Rows[i]["Branch_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    //   columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    string deliverd = dt3.Rows[i]["Delivered"].ToString();
                    if (deliverd == "true")
                    {
                        columnvalues.Append("1,");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["GstRateCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["GstRateCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    if (dt3.Rows[i]["CGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["CGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CGSTAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["SGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["SGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SGSTAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["IGSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["IGSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["IGSTAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["IGSTAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }



                    if (dt3.Rows[i]["TaxableAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["TaxableAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Party_Name"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Party_Name"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Broker_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Broker_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["HamaliAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["HamaliAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    string checkvlue = dt3.Rows[i]["CashRecive"].ToString();

                    if (checkvlue == "1")
                    {
                        columnvalues.Append("1,");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Party_Name_New"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Party_Name_New"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    if (PN != "0")
                    {

                        columnvalues.Append("'" + PN + "',");
                    }
                    else
                    {
                        columnvalues.Append("Null,");
                    }
                    if (Bc != "0")
                    {
                        columnvalues.Append("'" + Bc + "',");
                    }
                    else
                    {
                        columnvalues.Append("Null,");
                    }

                    columnvalues.Append("'" + ratailid + "'");


                    columnvalues.Append(")");
                    ratailid = ratailid + 1;
                    string insert = "insert  into nt_1_retailsalehead (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();

                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Retail sale return head Migration is done!');", true);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mysqcon.Close();
            }

        }
        #endregion
        #region detail
        string mysqlsalereDetail = "select * from nt_1_retailselldetails where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        cmd = new SqlCommand(mysqlsalereDetail, mysqcon);
        returnVal = new SqlDataAdapter(mysqlsalereDetail, mysqcon);
        dt = new DataTable();
        returnVal.Fill(dt);



        string mysqlsalerehed = "select * from nt_1_retailsalehead where Company_Code=" + drpCompany.SelectedValue +
           " order by doc_no";
        SqlCommand cmdDO = new SqlCommand(mysqlsalerehed, mysqcon);
        SqlDataAdapter returnValDO = new SqlDataAdapter(mysqlsalerehed, mysqcon);
        DataTable dtDO = new DataTable();
        returnValDO.Fill(dtDO);



        string mysqlaccountMasterdetail = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlaccountMaste1r = new SqlCommand(mysqlaccountMasterdetail, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster1 = new SqlDataAdapter(mysqlaccountMasterdetail, mysqcon);
        DataTable dtmysqlaccountMaster1 = new DataTable();
        returnValmysqlaccountMaster1.Fill(dtmysqlaccountMaster1);


        string mysqlitem = "select * from nt_1_systemmaster where  Company_Code=" + drpCompany.SelectedValue +
          " and System_Type ='I' order by System_Code";
        SqlCommand CmdItem = new SqlCommand(mysqlitem, mysqcon);
        SqlDataAdapter returnpurce = new SqlDataAdapter(mysqlitem, mysqcon);
        DataTable mysqli = new DataTable();
        returnpurce.Fill(mysqli);


        string sqlRetailsale = "select * from NT_1_RetailSellDetails where  Company_Code=" + drpCompany.SelectedValue +
           " order by doc_no";
        cmdsql = new SqlCommand(sqlRetailsale, sqlcon);
        returnValSQL = new SqlDataAdapter(sqlRetailsale, sqlcon);
        dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        dt3 = new DataTable();

        dt3.Columns.Add((new DataColumn("detail_Id", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tran_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("Mill_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("item_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Quantity", typeof(string))));
        dt3.Columns.Add((new DataColumn("Billing_No", typeof(string))));

        dt3.Columns.Add((new DataColumn("Rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("Value", typeof(string))));
        dt3.Columns.Add((new DataColumn("Vat_Ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("vat_percent", typeof(string))));
        dt3.Columns.Add((new DataColumn("vat_amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Gross", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("grade", typeof(string))));
        dt3.Columns.Add((new DataColumn("retailid", typeof(string))));
        dt3.Columns.Add((new DataColumn("retaildetailid", typeof(string))));
        dt3.Columns.Add((new DataColumn("ic", typeof(string))));
        dt3.Columns.Add((new DataColumn("mc", typeof(string))));
        dt3.Columns.Add((new DataColumn("purchaseid", typeof(string))));

        columnname = "detail_id,doc_no,Tran_Type,Mill_Code,Item_Code,Quantity,Billing_No,Rate,Value,Vat_Ac,vat_percent,vat_amount,Gross,Company_Code,Year_Code,Branch_Code" +
",grade,retailid,retaildetailid,ic,mc,purchaseid";

        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["doc_no"].ToString();
            string tran_Type = dtsql.Rows[i]["Tran_Type"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() +
                " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + " and detail_Id=" + dtsql.Rows[i]["detail_Id"].ToString() + "" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }

        }
        columnvalues = new StringBuilder();
        int retaildetailid = 1;
        maxLavel = 0;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([retaildetailid])", string.Empty));
            retaildetailid = maxLavel + 1;
        }

        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                    string itemcode = dt3.Rows[i]["item_code"].ToString();
                    string milcode = dtsql.Rows[i]["Mill_Code"].ToString();
                    string doc_no = dtsql.Rows[i]["doc_no"].ToString();


                    DataRow[] drbsid = mysqli.Select("System_Code = " + itemcode + " and Company_Code=" + companycode);
                    string itemId = "0";
                    string headretailid = "0";
                    string Ac = "0";

                    if (drbsid.Length > 0)
                    {
                        itemId = drbsid[0]["systemid"].ToString();
                    }

                    drbsid = dtDO.Select("doc_no = " + doc_no + " and Company_Code=" + companycode + " and Year_Code=" + groupcode);
                    if (drbsid.Length > 0)
                    {
                        headretailid = drbsid[0]["retailid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster1.Select("Ac_Code = " + milcode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        Ac = drbsid[0]["accoid"].ToString();
                    }
                    columnvalues.Clear();
                    columnvalues.Append("(");


                    columnvalues.Append("'" + dt3.Rows[i]["detail_Id"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Tran_Type"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Mill_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["item_code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Quantity"].ToString() + "',");

                    if (dt3.Rows[i]["Billing_No"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Billing_No"].ToString() + "',");

                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }



                    columnvalues.Append("'" + dt3.Rows[i]["Rate"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Value"].ToString() + "',");
                    if (dt3.Rows[i]["Vat_Ac"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Vat_Ac"].ToString() + "',");

                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["vat_percent"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["vat_percent"].ToString() + "',");

                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["vat_amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["vat_amount"].ToString() + "',");

                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Gross"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Gross"].ToString() + "',");

                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }


                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    if (dt3.Rows[i]["Branch_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");

                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    //   columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    if (dt3.Rows[i]["grade"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["grade"].ToString() + "',");

                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + headretailid + "',");
                    columnvalues.Append("'" + retaildetailid + "',");
                    if (itemId != "0")
                    {
                        columnvalues.Append("'" + itemId + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (Ac != "0")
                    {
                        columnvalues.Append("'" + Ac + "',");

                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("null");
                    columnvalues.Append(")");
                    retaildetailid = retaildetailid + 1;
                    string insert = "insert  into nt_1_retailselldetails (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    //  ds = clsDAL.SimpleQuery(insert);
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Retail Sale details Migration is done!');", true);
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                mysqcon.Close();
            }
        }
        #endregion

    }

    protected void btnJournalvoucher_Click(object sender, EventArgs e)
    {

        #region Head
        string mysqlJournalvoucher = "select * from nt_1_transacthead where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmd = new SqlCommand(mysqlJournalvoucher, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(mysqlJournalvoucher, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);

        string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);

        string mysqljv = "select * from NT_1_Transact where  Company_Code=" + drpCompany.SelectedValue + " and Tran_Type='JV' order by doc_no";
        SqlCommand cmdsql = new SqlCommand(mysqljv, sqlcon);
        SqlDataAdapter returnValSQL = new SqlDataAdapter(mysqljv, sqlcon);
        DataTable dtsql = new DataTable();
        returnValSQL.Fill(dtsql);

        if (dtsql.Rows.Count > 0)
        {
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('No record Found!');", true);
            return;
        }

        DataTable dt3 = new DataTable();
        dt3.Columns.Add((new DataColumn("tran_type", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("cashbank", typeof(string))));
        dt3.Columns.Add((new DataColumn("total", typeof(string))));
        dt3.Columns.Add((new DataColumn("company_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("year_code", typeof(string))));
        dt3.Columns.Add((new DataColumn("cb", typeof(string))));
        dt3.Columns.Add((new DataColumn("tranid", typeof(string))));
        string columnname = "tran_type,doc_no,doc_date,cashbank,total,company_code,year_code,cb,tranid";

        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string doc_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() +
                " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + " and Tran_Type='" + dtsql.Rows[i]["Tran_Type"].ToString() + "' " +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }

        StringBuilder columnvalues = new StringBuilder();
        int JVID = 1;

        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([tranid])", string.Empty));
            JVID = maxLavel + 1;
        }

        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string Yearcode = dt3.Rows[i]["Year_Code"].ToString();
                    string Cash = dt3.Rows[i]["cashbank"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["cashbank"]) : "0";

                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Cash + " and Company_Code=" + companycode);

                    string Ca = "0";



                    if (drbsid.Length > 0)
                    {
                        Ca = drbsid[0]["accoid"].ToString();
                    }



                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["tran_type"].ToString() + "',");
                    columnvalues.Append(dt3.Rows[i]["doc_no"].ToString() + ",");
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    if (dt3.Rows[i]["cashbank"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["cashbank"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["total"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["total"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    columnvalues.Append(dt3.Rows[i]["company_code"].ToString() + ",");
                    columnvalues.Append("'" + dt3.Rows[i]["year_code"].ToString() + "',");
                    if (Ca != "0")
                    {
                        columnvalues.Append("'" + Ca + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("" + JVID + "");
                    columnvalues.Append(")");
                    JVID = JVID + 1;
                    string insert = "insert  into nt_1_transacthead (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();

                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Journal_vouchar Migration is done!');", true);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mysqcon.Close();
            }

        }
        #endregion
        #region Detail
        string mysqlJV = "select * from nt_1_transacthead where  Company_Code=" + drpCompany.SelectedValue + " and Tran_Type='JV' order by doc_no";
        SqlCommand cmdhead = new SqlCommand(mysqlJV, mysqcon);
        SqlDataAdapter returnVal1 = new SqlDataAdapter(mysqlJV, mysqcon);
        DataTable dthead = new DataTable();

        returnVal1.Fill(dthead);

        string mysqlJvDetail = "select * from nt_1_transactdetail where  Company_Code=" + drpCompany.SelectedValue + "  order by doc_no";
        SqlCommand cmdDeatil = new SqlCommand(mysqlJvDetail, mysqcon);
        SqlDataAdapter returnVal2 = new SqlDataAdapter(mysqlJvDetail, mysqcon);
        DataTable dtdetail = new DataTable();
        returnVal2.Fill(dtdetail);

        mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMasterq = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMasterq = new DataTable();
        returnValmysqlaccountMasterq.Fill(dtmysqlaccountMasterq);


        string sqlDO = "select * from NT_1_Transact where  Company_Code=" + drpCompany.SelectedValue +
           "and Tran_Type='JV' order by doc_no";
        cmdsql = new SqlCommand(sqlDO, sqlcon);

        returnValSQL = new SqlDataAdapter(sqlDO, sqlcon);
        dtsql = new DataTable();
        returnValSQL.Fill(dtsql);
        dt3 = new DataTable();


        dt3.Columns.Add((new DataColumn("Tran_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_no", typeof(string))));
        dt3.Columns.Add((new DataColumn("doc_date", typeof(string))));
        dt3.Columns.Add((new DataColumn("detail_id", typeof(string))));
        dt3.Columns.Add((new DataColumn("debit_ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("credit_ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("Unit_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration", typeof(string))));
        dt3.Columns.Add((new DataColumn("narration2", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Voucher_Type", typeof(string))));
        dt3.Columns.Add((new DataColumn("Adjusted_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Tender_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("TenderDetail_ID", typeof(string))));
        dt3.Columns.Add((new DataColumn("drpFilterValue", typeof(string))));
        dt3.Columns.Add((new DataColumn("CreditAcAdjustedAmount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_name", typeof(string))));
        dt3.Columns.Add((new DataColumn("YearCodeDetail", typeof(string))));
        dt3.Columns.Add((new DataColumn("tranid", typeof(string))));
        dt3.Columns.Add((new DataColumn("ca", typeof(string))));
        dt3.Columns.Add((new DataColumn("uc", typeof(string))));
        dt3.Columns.Add((new DataColumn("tenderdetailid", typeof(string))));
        dt3.Columns.Add((new DataColumn("sbid", typeof(string))));
        dt3.Columns.Add((new DataColumn("da", typeof(string))));
        dt3.Columns.Add((new DataColumn("trandetailid", typeof(string))));
        dt3.Columns.Add((new DataColumn("drcr", typeof(string))));


        columnname = " Tran_Type,doc_no,doc_date,detail_id,debit_ac,credit_ac,Unit_Code,amount,narration,narration2,Company_Code,Year_Code," +
"Branch_Code,Created_By,Modified_By,Voucher_No,Voucher_Type,Adjusted_Amount,Tender_No,TenderDetail_ID,drpFilterValue,CreditAcAdjustedAmount," +
" Branch_name,YearCodeDetail,tranid,ca,uc,tenderdetailid,sbid,trandetailid,drcr";


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string Tender_No = dtsql.Rows[i]["doc_no"].ToString();
            string detailId = dtsql.Rows[i]["detail_Id"].ToString();

            DataRow[] dr = dtdetail.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() +
                " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() +
                " and detail_id=" + dtsql.Rows[i]["detail_id"].ToString() + " and Tran_Type='" + dtsql.Rows[i]["Tran_Type"].ToString() + "'" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }

        }
        columnvalues = new StringBuilder();
        int JVDeatilId = 1;
        maxLavel = 0;
        if (dtdetail.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dtdetail.Compute("max([trandetailid])", string.Empty));
            JVDeatilId = maxLavel + 1;
        }

        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string groupcode = dt3.Rows[i]["Year_Code"].ToString();
                    string doc_No = dt3.Rows[i]["doc_no"].ToString();
                    string Ac_code = dt3.Rows[i]["credit_ac"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["credit_ac"]) : "0";
                    string UntiCode = dtsql.Rows[i]["Unit_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Unit_Code"]) : "0";
                    string vNo = dtsql.Rows[i]["Voucher_No"].ToString();
                    string VType = dtsql.Rows[i]["Voucher_Type"].ToString();

                    DataRow[] drbsid = dtmysqlaccountMasterq.Select("Ac_code = " + Ac_code + " and Company_Code=" + companycode);
                    string Ac_Id = "0";
                    string Unit_Id = "0";
                    string headAutoid = "0";

                    if (drbsid.Length > 0)
                    {
                        Ac_Id = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMasterq.Select("Ac_code = " + UntiCode + " and Company_Code=" + companycode);

                    if (drbsid.Length > 0)
                    {
                        Unit_Id = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dthead.Select("doc_no = " + doc_No + " and Company_Code=" + companycode + " and Year_Code=" + groupcode);
                    if (drbsid.Length > 0)
                    {
                        headAutoid = drbsid[0]["tranid"].ToString();
                    }



                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["Tran_Type"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["doc_no"].ToString() + "',");
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["detail_id"].ToString() + "',");
                    if (dt3.Rows[i]["debit_ac"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["debit_ac"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["credit_ac"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["credit_ac"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Unit_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Unit_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["narration"].ToString() != string.Empty)
                    {
                        string narration = dt3.Rows[i]["narration"].ToString();
                        narration = narration.Replace("'", "");
                        columnvalues.Append("'" + narration + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["narration2"].ToString() != string.Empty)
                    {
                        string narration2 = dt3.Rows[i]["narration2"].ToString();
                        narration2 = narration2.Replace("'", "");
                        columnvalues.Append("'" + narration2 + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Branch_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    if (dt3.Rows[i]["Voucher_No"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Voucher_No"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Voucher_Type"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Voucher_Type"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Adjusted_Amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Adjusted_Amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Tender_No"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Tender_No"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["TenderDetail_ID"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["TenderDetail_ID"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    if (dt3.Rows[i]["drpFilterValue"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["drpFilterValue"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["CreditAcAdjustedAmount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CreditAcAdjustedAmount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Branch_name"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Branch_name"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["YearCodeDetail"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["YearCodeDetail"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (headAutoid != "0")
                    {
                        columnvalues.Append("'" + headAutoid + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (Ac_Id != "0")
                    {
                        columnvalues.Append("'" + Ac_Id + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (Unit_Id != "0")
                    {
                        columnvalues.Append("'" + Unit_Id + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["tenderdetailid"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["tenderdetailid"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["sbid"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["sbid"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    //if (dt3.Rows[i]["da"].ToString() != string.Empty)
                    //{
                    //    columnvalues.Append("'" + dt3.Rows[i]["da"].ToString() + "',");
                    //}
                    //else
                    //{
                    //    columnvalues.Append("0,");
                    //}
                    columnvalues.Append("'" + JVDeatilId + "',");
                    if (dt3.Rows[i]["drcr"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["drcr"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0");
                    }

                    columnvalues.Append(")");
                    JVDeatilId = JVDeatilId + 1;
                    string insert = "insert  into nt_1_transactdetail (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                    //  ds = clsDAL.SimpleQuery(insert);
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Journal Voucher details Migration is done!');", true);
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                mysqcon.Close();
            }

        }



        #endregion
    }



    protected void btnGledger_Click(object sender, EventArgs e)
    {
        #region Head
        string mysqlGledegr = "select * from nt_1_gledger where  Company_Code=" + drpCompany.SelectedValue + " and Year_Code=4 order by doc_no";
        SqlCommand cmd = new SqlCommand(mysqlGledegr, mysqcon);
        SqlDataAdapter returnVal1 = new SqlDataAdapter(mysqlGledegr, mysqcon);
        DataTable dt = new DataTable();
        returnVal1.Fill(dt);

        string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);

        string sqlGleder = "select * from NT_1_GLEDGER where  Company_Code=" + drpCompany.SelectedValue + " and Year_Code=4  order by doc_no";
        SqlCommand cmdsql = new SqlCommand(sqlGleder, sqlcon);
        SqlDataAdapter returnValSQL = new SqlDataAdapter(sqlGleder, sqlcon);
        DataTable dtsql = new DataTable();
        returnValSQL.Fill(dtsql);



        DataTable dt3 = new DataTable();
        dt3.Columns.Add((new DataColumn("TRAN_TYPE", typeof(string))));
        dt3.Columns.Add((new DataColumn("CASHCREDIT", typeof(string))));
        dt3.Columns.Add((new DataColumn("DOC_NO", typeof(string))));
        dt3.Columns.Add((new DataColumn("DOC_DATE", typeof(string))));
        dt3.Columns.Add((new DataColumn("AC_CODE", typeof(string))));
        dt3.Columns.Add((new DataColumn("UNIT_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("NARRATION", typeof(string))));
        dt3.Columns.Add((new DataColumn("AMOUNT", typeof(string))));
        dt3.Columns.Add((new DataColumn("TENDER_ID", typeof(string))));
        dt3.Columns.Add((new DataColumn("TENDER_ID_DETAIL", typeof(string))));
        dt3.Columns.Add((new DataColumn("VOUCHER_ID", typeof(string))));
        dt3.Columns.Add((new DataColumn("COMPANY_CODE", typeof(string))));
        dt3.Columns.Add((new DataColumn("YEAR_CODE", typeof(string))));
        dt3.Columns.Add((new DataColumn("ORDER_CODE", typeof(string))));
        dt3.Columns.Add((new DataColumn("DRCR", typeof(string))));
        dt3.Columns.Add((new DataColumn("DRCR_HEAD", typeof(string))));
        dt3.Columns.Add((new DataColumn("ADJUSTED_AMOUNT", typeof(string))));
        dt3.Columns.Add((new DataColumn("Branch_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("SORT_TYPE", typeof(string))));
        dt3.Columns.Add((new DataColumn("SORT_NO", typeof(string))));
        dt3.Columns.Add((new DataColumn("ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("vc", typeof(string))));
        dt3.Columns.Add((new DataColumn("progid", typeof(string))));
        dt3.Columns.Add((new DataColumn("tranid", typeof(string))));

        string columnname = "TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,UNIT_Code,NARRATION,AMOUNT,TENDER_ID,TENDER_ID_DETAIL,VOUCHER_ID,COMPANY_CODE,YEAR_CODE," +
       "ORDER_CODE,DRCR,DRCR_HEAD,ADJUSTED_AMOUNT,Branch_Code,SORT_TYPE,SORT_NO,ac,vc,progid,tranid";

        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string doc_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() +
                " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + " and Tran_Type='" + dtsql.Rows[i]["Tran_Type"].ToString() + "'" +
            " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }

        StringBuilder columnvalues = new StringBuilder();
        int gleger = 1;

        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([progid])", string.Empty));
            gleger = maxLavel + 1;
        }

        if (dt3.Rows.Count > 0)
        {
            int i = 0;
            int j = 0;
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string Yearcode = dt3.Rows[i]["Year_Code"].ToString();
                    string Accode = dt3.Rows[i]["AC_CODE"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["AC_CODE"]) : "0";
                    string unitcode = dt3.Rows[i]["UNIT_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["UNIT_Code"]) : "0";
                    string TranType = dt3.Rows[i]["TRAN_TYPE"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["TRAN_TYPE"]) : "0";

                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + Accode + " and Company_Code=" + companycode);

                    string Ac = "0";
                    string Uc = "0";


                    if (drbsid.Length > 0)
                    {
                        Ac = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + unitcode + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        Uc = drbsid[0]["accoid"].ToString();
                    }


                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["TRAN_TYPE"].ToString() + "',");

                    columnvalues.Append("'" + dt3.Rows[i]["CASHCREDIT"].ToString() + "',");



                    columnvalues.Append(dt3.Rows[i]["DOC_NO"].ToString() + ",");
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["DOC_DATE"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    if (dt3.Rows[i]["AC_CODE"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["AC_CODE"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["UNIT_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["UNIT_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["NARRATION"].ToString() != string.Empty)
                    {
                        string narration = dt3.Rows[i]["NARRATION"].ToString();
                        narration = narration.Replace("'", "");
                        narration = narration.Replace(@"\", "");
                        columnvalues.Append("'" + narration + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }

                    if (dt3.Rows[i]["AMOUNT"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["AMOUNT"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["TENDER_ID"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["TENDER_ID"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["TENDER_ID_DETAIL"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["TENDER_ID_DETAIL"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["VOUCHER_ID"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["VOUCHER_ID"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    columnvalues.Append(dt3.Rows[i]["company_code"].ToString() + ",");
                    columnvalues.Append(dt3.Rows[i]["Year_Code"].ToString() + ",");

                    if (dt3.Rows[i]["ORDER_CODE"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["ORDER_CODE"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["DRCR"].ToString() + "',");

                    if (dt3.Rows[i]["DRCR_HEAD"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["DRCR_HEAD"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["ADJUSTED_AMOUNT"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["ADJUSTED_AMOUNT"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (dt3.Rows[i]["Branch_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["Branch_Code"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }

                    columnvalues.Append("'" + dt3.Rows[i]["SORT_TYPE"].ToString() + "',");

                    if (dt3.Rows[i]["SORT_NO"].ToString() != string.Empty)
                    {
                        columnvalues.Append(dt3.Rows[i]["SORT_NO"].ToString() + ",");
                    }
                    else
                    {
                        columnvalues.Append("'0',");
                    }
                    if (Ac != "0")
                    {
                        columnvalues.Append("'" + Ac + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (Uc != "0")
                    {
                        columnvalues.Append("'" + Uc + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("Null,");
                    columnvalues.Append("'0'");
                    columnvalues.Append(")");

                    string insert = "insert  into nt_1_gledger (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();

                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Glegder Migration is done!');", true);
            }
            catch (Exception ex)
            {
                j = i;
            }
            finally
            {
                mysqcon.Close();
            }

        }
        #endregion
    }

    protected void btnotherPurches_Click(object sender, EventArgs e)
    {
        #region Head
        string otherPEntry = "select * from nt_1_other_purchase where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmd = new SqlCommand(otherPEntry, mysqcon);
        SqlDataAdapter returnVal = new SqlDataAdapter(otherPEntry, mysqcon);
        DataTable dt = new DataTable();
        returnVal.Fill(dt);

        string mysqlaccountMaster = "select * from nt_1_accountmaster where Company_Code=" + drpCompany.SelectedValue;
        SqlCommand cmdmysqlaccountMaster = new SqlCommand(mysqlaccountMaster, mysqcon);
        SqlDataAdapter returnValmysqlaccountMaster = new SqlDataAdapter(mysqlaccountMaster, mysqcon);
        DataTable dtmysqlaccountMaster = new DataTable();
        returnValmysqlaccountMaster.Fill(dtmysqlaccountMaster);

        string purchesentry = "select * from NT_1_Other_Purchase where  Company_Code=" + drpCompany.SelectedValue + " order by doc_no";
        SqlCommand cmdsql = new SqlCommand(purchesentry, sqlcon);
        SqlDataAdapter returnValSQL = new SqlDataAdapter(purchesentry, sqlcon);
        DataTable dtsql = new DataTable();
        returnValSQL.Fill(dtsql);



        //string DoEntry = "select * from nt_1_deliveryorder where Year_Code=1 and Company_Code=" +drpCompany.SelectedValue  + " order by doc_no";
        //cmd = new SqlCommand(DoEntry, mysqcon);
        //returnVal = new SqlDataAdapter(DoEntry, mysqcon);
        //DataTable DoHead = new DataTable();
        //returnVal.Fill(DoHead);

        DataTable dt3 = new DataTable();
        dt3.Columns.Add((new DataColumn("Doc_No", typeof(string))));
        dt3.Columns.Add((new DataColumn("Doc_Date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Supplier_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Exp_Ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("Narration", typeof(string))));
        dt3.Columns.Add((new DataColumn("Taxable_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("GST_RateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("CGST_Rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("CGST_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("SGST_Rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("SGST_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("IGST_Rate", typeof(string))));
        dt3.Columns.Add((new DataColumn("IGST_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Other_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Bill_Amount", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_Date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_Date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("TDS_Amt", typeof(string))));
        dt3.Columns.Add((new DataColumn("TDS_Per", typeof(string))));
        dt3.Columns.Add((new DataColumn("TDS", typeof(string))));
        dt3.Columns.Add((new DataColumn("TDS_Cutt_AcCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("TDS_AcCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("opid", typeof(string))));
        dt3.Columns.Add((new DataColumn("sc", typeof(string))));
        dt3.Columns.Add((new DataColumn("ea", typeof(string))));
        dt3.Columns.Add((new DataColumn("tca", typeof(string))));
        dt3.Columns.Add((new DataColumn("tac", typeof(string))));

        string columnname = "Doc_No ,Doc_Date,Supplier_Code,Exp_Ac,Narration,Taxable_Amount,GST_RateCode,CGST_Rate,CGST_Amount,SGST_Rate," +
" SGST_Amount,IGST_Rate,IGST_Amount,Other_Amount,Bill_Amount,Company_Code,Created_By,Modified_By,Created_Date,Modified_Date,Year_Code," +
"TDS_Amt,TDS_Per,TDS,TDS_Cutt_AcCode,TDS_AcCode,opid,sc,ea,tca,tac";

        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            string companycode = dtsql.Rows[i]["Company_Code"].ToString();
            string doc_No = dtsql.Rows[i]["doc_no"].ToString();

            DataRow[] dr = dt.Select("doc_no = " + dtsql.Rows[i]["doc_no"].ToString() + " and Company_code=" + dtsql.Rows[i]["Company_Code"].ToString() + "" +
                " and Year_Code=" + dtsql.Rows[i]["Year_Code"].ToString() + "");

            if (dr.Length == 0)
            {
                dt3.ImportRow(dtsql.Rows[i]);
            }
        }

        StringBuilder columnvalues = new StringBuilder();
        int otherpurchesid = 1;

        int maxLavel;
        if (dt.Rows.Count > 0)
        {
            maxLavel = Convert.ToInt32(dt.Compute("max([opid])", string.Empty));
            otherpurchesid = maxLavel + 1;
        }
        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string companycode = dt3.Rows[i]["Company_Code"].ToString();
                    string Yearcode = dt3.Rows[i]["Year_Code"].ToString();
                    string suppiler = dt3.Rows[i]["Supplier_Code"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Supplier_Code"]) : "0";
                    string ExpAccount = dt3.Rows[i]["Exp_Ac"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["Exp_Ac"]) : "0";
                    string TDSCutingAccount = dt3.Rows[i]["TDS_Cutt_AcCode"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["TDS_Cutt_AcCode"]) : "0";
                    string TDSAccount = dt3.Rows[i]["TDS_AcCode"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["TDS_AcCode"]) : "0";
                    // string purcheNo = dt3.Rows[i]["PURCNO"].ToString() != string.Empty ? Convert.ToString(dt3.Rows[i]["PURCNO"]) : "0";
                    DataRow[] drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + suppiler + " and Company_Code=" + companycode);
                    string SA = "2";
                    string ExpA = "2";
                    string TDCA = "2";
                    string TDSA = "2";


                    if (drbsid.Length > 0)
                    {
                        SA = drbsid[0]["accoid"].ToString();
                    }

                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + ExpAccount + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        ExpA = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + TDSCutingAccount + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        TDCA = drbsid[0]["accoid"].ToString();
                    }
                    drbsid = dtmysqlaccountMaster.Select("Ac_Code = " + TDSAccount + " and Company_Code=" + companycode);
                    if (drbsid.Length > 0)
                    {
                        TDSA = drbsid[0]["accoid"].ToString();
                    }



                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["Doc_No"].ToString() + "',");
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["doc_date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    if (dt3.Rows[i]["Supplier_Code"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Supplier_Code"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }


                    if (dt3.Rows[i]["Exp_Ac"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Exp_Ac"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Narration"].ToString() != string.Empty)
                    {
                        string narration = dt3.Rows[i]["Narration"].ToString();
                        narration = narration.Replace("'", "");
                        columnvalues.Append("'" + narration + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }

                    if (dt3.Rows[i]["Taxable_Amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Taxable_Amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["GST_RateCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["GST_RateCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["CGST_Rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CGST_Rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["CGST_Amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CGST_Amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["SGST_Rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SGST_Rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["SGST_Amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SGST_Amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["IGST_Rate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["IGST_Rate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["IGST_Amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["IGST_Amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }



                    if (dt3.Rows[i]["Other_Amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Other_Amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["Bill_Amount"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Bill_Amount"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["Created_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    if (dt3.Rows[i]["Modified_Date"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["Modified_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");
                    }
                    else
                    {
                        columnvalues.Append("Null,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");

                    if (dt3.Rows[i]["TDS_Amt"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["TDS_Amt"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    if (dt3.Rows[i]["TDS_Per"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["TDS_Per"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }
                    if (dt3.Rows[i]["TDS"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["TDS"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    if (dt3.Rows[i]["TDS_Cutt_AcCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["TDS_Cutt_AcCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }

                    if (dt3.Rows[i]["TDS_AcCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["TDS_AcCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("0,");
                    }


                    columnvalues.Append("'" + otherpurchesid + "',");
                    if (SA != "0")
                    {
                        columnvalues.Append("'" + SA + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (ExpA != "0")
                    {
                        columnvalues.Append("'" + ExpA + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (TDCA != "0")
                    {
                        columnvalues.Append("'" + TDCA + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (TDSA != "0")
                    {
                        columnvalues.Append("'" + TDSA + "'");
                    }
                    else
                    {
                        columnvalues.Append("null");
                    }


                    columnvalues.Append(")");
                    otherpurchesid = otherpurchesid + 1;
                    string insert = "insert  into nt_1_other_purchase (" + columnname + ") values " + columnvalues;

                    SqlCommand MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();

                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('other Purches Migration is done!');", true);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mysqcon.Close();
            }

        }
        #endregion
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            sqlcon.Open();
            SqlTransaction tran = null;
            tran = sqlcon.BeginTransaction();
            string OtherPurc = "delete from nt_1_other_purchase";
            SqlCommand cmd = new SqlCommand(OtherPurc, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string tran_Detail = "delete from nt_1_transactdetail";
            cmd = new SqlCommand(tran_Detail, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string tran_Head = "delete from nt_1_transacthead";
            cmd = new SqlCommand(tran_Head, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string Retail_Detail = "delete from nt_1_retailselldetails";
            cmd = new SqlCommand(Retail_Detail, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string nt_1_retailsalehead = "delete from nt_1_retailselldetails";
            cmd = new SqlCommand(nt_1_retailsalehead, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string Commision_Bill = "delete from commission_bill";
            cmd = new SqlCommand(Commision_Bill, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string SaleReturn_Detail = "delete from nt_1_sugarsaledetailsreturn";
            cmd = new SqlCommand(SaleReturn_Detail, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string SaleReturn_Head = "delete from nt_1_sugarsalereturn";
            cmd.CommandTimeout = 100;
            cmd = new SqlCommand(SaleReturn_Head, sqlcon, tran);
            cmd.ExecuteNonQuery();

            string PurcReturn_Detail = "delete from nt_1_sugarpurchasedetailsreturn";
            cmd = new SqlCommand(PurcReturn_Detail, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string PurcReturn_Head = "delete from nt_1_sugarpurchasereturn";
            cmd = new SqlCommand(PurcReturn_Head, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();


            string ServiceBill_Detail = "delete from nt_1_rentbilldetails";
            cmd = new SqlCommand(ServiceBill_Detail, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string ServiceBill = "delete from nt_1_rentbillhead";
            cmd = new SqlCommand(ServiceBill, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string do_Detail = "delete from nt_1_dodetails";
            cmd = new SqlCommand(do_Detail, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string do_Head = "delete from nt_1_deliveryorder";
            cmd = new SqlCommand(do_Head, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string sale_Detail = "delete from nt_1_sugarsaledetails";
            cmd = new SqlCommand(sale_Detail, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string sale_Head = "delete from nt_1_sugarsale";
            cmd = new SqlCommand(sale_Head, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string purc_Detail = "delete from nt_1_sugarpurchasedetails";
            cmd = new SqlCommand(purc_Detail, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string purc_Head = "delete from nt_1_sugarpurchase";
            cmd = new SqlCommand(purc_Head, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string carpo_Detail = "delete from carporatedetail";
            cmd = new SqlCommand(carpo_Detail, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string carpo_Head = "delete from carporatehead";
            cmd = new SqlCommand(carpo_Head, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string utr_Detail = "delete from nt_1_utrdetail";
            cmd = new SqlCommand(utr_Detail, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string utr_Head = "delete from nt_1_utr";
            cmd = new SqlCommand(utr_Head, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string tender_Detail = "delete from nt_1_tenderdetails";
            cmd = new SqlCommand(tender_Detail, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string tender_Head = "delete from nt_1_tender";
            cmd = new SqlCommand(tender_Head, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string retail_Detail = "delete from nt_1_retailselldetails";
            cmd = new SqlCommand(retail_Detail, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string retail_Head = "delete from nt_1_retailsalehead";
            cmd = new SqlCommand(retail_Head, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string item = "delete from nt_1_systemmaster";
            cmd = new SqlCommand(item, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string user = "delete from tbluser where User_Name!='demo'";
            cmd = new SqlCommand(user, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string partyUnit = "delete from nt_1_partyunit ";
            cmd = new SqlCommand(partyUnit, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string acc_group = "delete from nt_1_acgroups ";
            cmd = new SqlCommand(acc_group, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string acc_Detail = "delete from nt_1_accontacts ";
            cmd = new SqlCommand(acc_Detail, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string account = "delete from nt_1_accountmaster ";
            cmd = new SqlCommand(account, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string city = "delete from nt_1_citymaster ";
            cmd = new SqlCommand(city, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            string groupMaster = "delete from nt_1_bsgroupmaster ";
            cmd = new SqlCommand(groupMaster, sqlcon, tran);
            cmd.CommandTimeout = 100;
            cmd.ExecuteNonQuery();

            tran.Commit();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('done!');", true);
        }
        catch (Exception ex)
        {
            mysqcon.Close();
        }
        finally
        {
            mysqcon.Close();
        }
    }
    protected void btnYear_Click(object sender, EventArgs e)
    {

        //SqlDataAdapter returnVal = new SqlDataAdapter(cityqrymysql, mysqcon);
        //DataTable dt = new DataTable();
        //returnVal.Fill(dt);
        #region Year
        string cityqrysql = "select * from accountingyear";
        SqlCommand cmdsql = new SqlCommand(cityqrysql, sqlcon);
        SqlDataAdapter returnValsql = new SqlDataAdapter(cityqrysql, sqlcon);
        DataTable dtsql = new DataTable();
        returnValsql.Fill(dtsql);

        DataTable dt3 = new DataTable();

        dt3.Columns.Add((new DataColumn("yearCode", typeof(int))));
        dt3.Columns.Add((new DataColumn("year", typeof(string))));
        dt3.Columns.Add((new DataColumn("Start_Date", typeof(string))));
        dt3.Columns.Add((new DataColumn("End_Date", typeof(string))));
        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));

        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            dt3.ImportRow(dtsql.Rows[i]);

        }

        StringBuilder columnvalues = new StringBuilder();
        string columnname = "yearCode,year,Start_Date,End_Date,Company_Code";
        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();

                string cityqrymysql = "delete  from accountingyear";

                SqlCommand MySQLcmd1 = new SqlCommand(cityqrymysql, mysqcon, transaction);
                MySQLcmd1.ExecuteNonQuery();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {

                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3.Rows[i]["yearCode"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["year"].ToString() + "',");
                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["Start_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");

                    columnvalues.Append("'" + DateTime.Parse(dt3.Rows[i]["End_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd") + "',");

                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "'");
                    columnvalues.Append(")");

                    string insert = "insert  into accountingyear (" + columnname + ") values " + columnvalues;

                    MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch
            {
                mysqcon.Close();
            }
            finally
            {
                mysqcon.Close();
            }

        }
        #endregion

        #region Company
        string company = "select * from company";
        SqlCommand cmdCompanysql = new SqlCommand(company, sqlcon);
        SqlDataAdapter returnValsql1 = new SqlDataAdapter(company, sqlcon);
        DataTable dtsql1 = new DataTable();
        returnValsql1.Fill(dtsql1);

        DataTable dt3Company = new DataTable();

        dt3Company.Columns.Add((new DataColumn("Company_Code", typeof(int))));
        dt3Company.Columns.Add((new DataColumn("Company_Name_E", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("Company_Name_R", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("Address_E", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("Address_R", typeof(string))));

        dt3Company.Columns.Add((new DataColumn("City_E", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("City_R", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("State_E", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("State_R", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("PIN", typeof(string))));

        dt3Company.Columns.Add((new DataColumn("Mobile_No", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("Pan_No", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("Group_Code", typeof(string))));

        dt3Company.Columns.Add((new DataColumn("CST", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("TIN", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("PHONE", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("FSSAI_No", typeof(string))));
        dt3Company.Columns.Add((new DataColumn("GST", typeof(string))));
        for (int i = 0; i < dtsql1.Rows.Count; i++)
        {
            dt3Company.ImportRow(dtsql1.Rows[i]);

        }

        //StringBuilder columnvalues = new StringBuilder();
        string columnnameCompany = "Company_Code,Company_Name_E,Company_Name_R,Address_E,Address_R,City_E,City_R,State_E,State_R," +
            "PIN,Mobile_No,Created_By,Modified_By,Pan_No,Group_Code,CST,TIN,PHONE,FSSAI_No,GST";
        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();

                string cityqrymysql = "delete  from company";

                SqlCommand MySQLcmd1 = new SqlCommand(cityqrymysql, mysqcon, transaction);
                MySQLcmd1.ExecuteNonQuery();
                for (int i = 0; i < dt3Company.Rows.Count; i++)
                {

                    columnvalues.Clear();
                    columnvalues.Append("(");
                    columnvalues.Append("'" + dt3Company.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["Company_Name_E"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["Company_Name_R"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["Address_E"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["Address_R"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["City_E"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["City_R"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["State_E"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["State_R"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["PIN"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["Mobile_No"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["Modified_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["Pan_No"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["Group_Code"].ToString() + "',");

                    columnvalues.Append("'" + dt3Company.Rows[i]["CST"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["TIN"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["PHONE"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["FSSAI_No"].ToString() + "',");
                    columnvalues.Append("'" + dt3Company.Rows[i]["GST"].ToString() + "'");

                    columnvalues.Append(")");

                    string insert = "insert  into company (" + columnnameCompany + ") values " + columnvalues;

                    MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Year Migration is done!');", true);
            }
            catch
            {
                mysqcon.Close();
            }
            finally
            {
                mysqcon.Close();
            }
        #endregion
        }
    }
    protected void btnCompanyParameter_Click(object sender, EventArgs e)
    {
        #region Company parameter
        string cityqrysql = "select * from nt_1_companyparameters";
        SqlCommand cmdsql = new SqlCommand(cityqrysql, sqlcon);
        SqlDataAdapter returnValsql = new SqlDataAdapter(cityqrysql, sqlcon);
        DataTable dtsql = new DataTable();
        returnValsql.Fill(dtsql);

        DataTable dt3 = new DataTable();

        dt3.Columns.Add((new DataColumn("COMMISSION_AC", typeof(string))));
        dt3.Columns.Add((new DataColumn("INTEREST_AC", typeof(string))));
        dt3.Columns.Add((new DataColumn("TRANSPORT_AC", typeof(string))));
        dt3.Columns.Add((new DataColumn("POSTAGE_AC", typeof(string))));
        dt3.Columns.Add((new DataColumn("SELF_AC", typeof(string))));

        dt3.Columns.Add((new DataColumn("Company_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Year_Code", typeof(string))));
        dt3.Columns.Add((new DataColumn("Created_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("Modified_By", typeof(string))));
        dt3.Columns.Add((new DataColumn("AutoVoucher", typeof(string))));

        dt3.Columns.Add((new DataColumn("tblPrefix", typeof(string))));
        dt3.Columns.Add((new DataColumn("GSTStateCode", typeof(string))));
        dt3.Columns.Add((new DataColumn("CGSTAc", typeof(string))));
        dt3.Columns.Add((new DataColumn("SGSTAc", typeof(string))));
        dt3.Columns.Add((new DataColumn("IGSTAc", typeof(string))));

        dt3.Columns.Add((new DataColumn("PurchaseCGSTAc", typeof(string))));
        dt3.Columns.Add((new DataColumn("PurchaseSGSTAc", typeof(string))));
        dt3.Columns.Add((new DataColumn("PurchaseIGSTAc", typeof(string))));
        dt3.Columns.Add((new DataColumn("RoundOff", typeof(string))));
        dt3.Columns.Add((new DataColumn("Transport_RCM_GSTRate", typeof(string))));

        dt3.Columns.Add((new DataColumn("CGST_RCM_Ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("SGST_RCM_Ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("IGST_RCM_Ac", typeof(string))));
        dt3.Columns.Add((new DataColumn("Freight_Ac", typeof(string))));


        for (int i = 0; i < dtsql.Rows.Count; i++)
        {
            dt3.ImportRow(dtsql.Rows[i]);

        }

        StringBuilder columnvalues = new StringBuilder();
        string columnname = "COMMISSION_AC,INTEREST_AC, TRANSPORT_AC,POSTAGE_AC,SELF_AC,Company_Code, Year_Code,Created_By,Modified_By,AutoVoucher," +
 "tblPrefix,GSTStateCode,CGSTAc,SGSTAc,IGSTAc,PurchaseCGSTAc, PurchaseSGSTAc,PurchaseIGSTAc,RoundOff, Transport_RCM_GSTRate," +
      " CGST_RCM_Ac, SGST_RCM_Ac,IGST_RCM_Ac, Freight_Ac";
        if (dt3.Rows.Count > 0)
        {
            try
            {

                mysqcon.ConnectionString = mysqlconn;
                mysqcon.Open();
                transaction = mysqcon.BeginTransaction();

                string cityqrymysql = "delete  from nt_1_companyparameters";

                SqlCommand MySQLcmd1 = new SqlCommand(cityqrymysql, mysqcon, transaction);
                MySQLcmd1.ExecuteNonQuery();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {

                    columnvalues.Clear();
                    columnvalues.Append("(");
                    if (dt3.Rows[i]["COMMISSION_AC"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["COMMISSION_AC"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["INTEREST_AC"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["INTEREST_AC"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["TRANSPORT_AC"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["TRANSPORT_AC"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["POSTAGE_AC"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["POSTAGE_AC"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["SELF_AC"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SELF_AC"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    columnvalues.Append("'" + dt3.Rows[i]["Company_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Year_Code"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Created_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["Modified_By"].ToString() + "',");
                    columnvalues.Append("'" + dt3.Rows[i]["AutoVoucher"].ToString() + "',");

                    columnvalues.Append("'" + dt3.Rows[i]["tblPrefix"].ToString() + "',");
                    if (dt3.Rows[i]["GSTStateCode"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["GSTStateCode"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["CGSTAc"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CGSTAc"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["SGSTAc"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SGSTAc"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["IGSTAc"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["IGSTAc"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["PurchaseCGSTAc"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["PurchaseCGSTAc"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["PurchaseSGSTAc"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["PurchaseSGSTAc"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["PurchaseIGSTAc"].ToString() != string.Empty)
                    {

                        columnvalues.Append("'" + dt3.Rows[i]["PurchaseIGSTAc"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["RoundOff"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["RoundOff"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["Transport_RCM_GSTRate"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Transport_RCM_GSTRate"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["CGST_RCM_Ac"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["CGST_RCM_Ac"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["SGST_RCM_Ac"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["SGST_RCM_Ac"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["IGST_RCM_Ac"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["IGST_RCM_Ac"].ToString() + "',");
                    }
                    else
                    {
                        columnvalues.Append("null,");
                    }
                    if (dt3.Rows[i]["Freight_Ac"].ToString() != string.Empty)
                    {
                        columnvalues.Append("'" + dt3.Rows[i]["Freight_Ac"].ToString() + "'");
                    }
                    else
                    {
                        columnvalues.Append("null");
                    }

                    columnvalues.Append(")");

                    string insert = "insert  into nt_1_companyparameters (" + columnname + ") values " + columnvalues;

                    MySQLcmd1 = new SqlCommand(insert, mysqcon, transaction);
                    MySQLcmd1.ExecuteNonQuery();
                }
                transaction.Commit();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Company Parameter Migration is done!');", true);
            }
            catch
            {
                mysqcon.Close();
            }
            finally
            {
                mysqcon.Close();
            }

        }
        #endregion
    }
}