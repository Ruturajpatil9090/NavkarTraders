using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for clsCheckDatabase
/// </summary>
public class clsCheckDatabase
{
	public clsCheckDatabase()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string checkDBExist()
    {

        string result = "";
        try
        {
            bool bRet = false;

            string database = "";
            database = "AccoWebDemo";

            string serverName = "";

            string connString = "Data Source=ASHISH\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;";
            string cmdText = "select * from master.dbo.sysdatabases where name=\'" + database + "\'";

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    bRet = reader.HasRows;
                    reader.Close();
                    if (bRet == false)
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "Create Database " + database + ";";
                        cmd.ExecuteNonQuery();
                        result = "Database Created Successfully";
                    }
                    else
                    {
                        result = "Database Already Exists";

                        //query to create new table as existing table structure
                        //query=select * into newAcMaster from AccountMaster where 1=2;

                        //query to check whether table exist in database
                        //query="select * from AccoWeb.sys.tables where name = 'GLcxgedger'"

                        //                        Copy the table structure:-
                        //select * into newtable from oldtable where 1=2;

                        //Copy the table structure along with table data:-
                        //select * into newtable from oldtable where 1=1;
                    }
                }
                con.Close();
                
            }

            return result;
        }
        catch
        {
            return "Error";
        }
    }

    public static string createCompanyTables(string company_Code,string companyName)
    {
        try
        {
            DataSet ds = null;
            DataTable dt = null;

            string tblPrefix = string.Empty;
            string[] initials = companyName.Split(' ');
            for (int i = 0; i < initials.Length; i++)
            {
                tblPrefix = tblPrefix + initials[i].Substring(0, 1);
            }
            tblPrefix = tblPrefix + "_" + company_Code + "_";



            string qry = "select * into " + tblPrefix + "AccountMaster from AccountMaster where 1=1";
            qry = qry + " alter table " + tblPrefix + "AccountMaster ADD CONSTRAINT pk_accode" + tblPrefix + " primary key(Ac_Code) ";  //adding primary key
            ds = clsDAL.SimpleQuery(qry);

            qry = "insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(1,'Cash AC','C','Cash',101,"+clsGV.Company_Code+")" +

            " insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(2,'Self','C','Self',101," + clsGV.Company_Code + ")" +

            " insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(3,'Office Expenses','E','OE',101," + clsGV.Company_Code + ")" +

            " insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(4,'Bank Commission','I','BC',101," + clsGV.Company_Code + ")" +

            " insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(5,'Sugar Rate Difference A/C','C','SRD',101," + clsGV.Company_Code + ")" +

            " insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(6,'Sugar Purchase A/C','E','Purchase',101," + clsGV.Company_Code + ")" +

            " insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(7,'Sugar Sale A/C','I','Sale',101," + clsGV.Company_Code + ")" +

            " insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(8,'Brokrage A/C','E','Brokerage',101," + clsGV.Company_Code + ")" +

            " insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(9,'Depreciation A/C','E','Depreciation',101," + clsGV.Company_Code + ")" +

            " insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(10,'Interest Received A/C','E','Interest',101," + clsGV.Company_Code + ")" +

            " insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(11,'Interest Paid A/C','E','Interest Paid',101," + clsGV.Company_Code + ")" +

            " insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(12,'TDS Receivable A/C','A','TDS Receivable',101," + clsGV.Company_Code + ")" +

            " insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(13,'TDS Payable A/C','L','TDS Payable',101," + clsGV.Company_Code + ")" +

            " insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(14,'Next Profit A/C','I','Next Profit',101," + clsGV.Company_Code + ")" +

            " insert into DC_2_AccountMaster (Ac_Code,Ac_Name_E,Ac_type,Short_Name,Group_Code,Company_Code)" +
            " values(15,'Next Loss A/C','E','Next Loss',101," + clsGV.Company_Code + ")";
            ds = clsDAL.SimpleQuery(qry);


            qry = "select * into " + tblPrefix + "Tender from Tender where 1=1";
            qry = qry + " alter table " + tblPrefix + "Tender ADD CONSTRAINT pk_tender"+tblPrefix+" primary key(Tender_No,Company_Code,Year_Code)";
            ds = clsDAL.SimpleQuery(qry);

            qry = "select * into " + tblPrefix + "TenderDetails  from TenderDetails where 1=1";
            qry = qry + " alter table " + tblPrefix + "TenderDetails ADD CONSTRAINT pk_tenderdetails" + tblPrefix + " primary key(Tender_No,Company_Code,ID)";
            ds = clsDAL.SimpleQuery(qry); 

            qry = "select * into " + tblPrefix + "deliveryOrder  from deliveryOrder where 1=2";
            qry = qry + " alter table " + tblPrefix + "deliveryOrder ADD CONSTRAINT pk_delivery" + tblPrefix + " primary key(doc_no,company_code,Year_Code)";
            ds = clsDAL.SimpleQuery(qry);

            qry = "select * into " + tblPrefix + "DODetails  from DODetails where 1=2";
            qry = qry + " alter table " + tblPrefix + "DODetails ADD CONSTRAINT pk_deliveryOrder" + tblPrefix + " primary key(doc_no,detail_Id,company_code,Year_Code)";
            ds = clsDAL.SimpleQuery(qry); 

             qry = "select * into "+tblPrefix+"GLedger from GLedger where 1=1";
             qry = qry + " alter table  " + tblPrefix + "GLedger ADD CONSTRAINT pk_GLedger" + tblPrefix + " primary key(TRAN_TYPE,CASHCREDIT,DOC_NO,COMPANY_CODE,YEAR_CODEORDER_CODE)";
            ds = clsDAL.SimpleQuery(qry);

             qry = "select * into "+tblPrefix+"SystemMaster from SystemMaster where 1=1";
             qry = qry + " alter table " + tblPrefix + "SystemMaster ADD CONSTRAINT pk_system" + tblPrefix + " primary key(System_Type,System_Code)";
            ds = clsDAL.SimpleQuery(qry);

            qry = "select * into " + tblPrefix + "CityMaster from CityMaster where 1=1";
            qry = qry + " alter table " + tblPrefix + "CityMaster ADD CONSTRAINT pk_group" + tblPrefix + " primary key(city_code,company_code)";
            ds = clsDAL.SimpleQuery(qry);

            qry = "select * into " + tblPrefix + "BSGroupMaster from BSGroupMaster where 1=1";
            qry = qry + " alter table " + tblPrefix + "BSGroupMaster ADD CONSTRAINT pk_group" + tblPrefix + " primary key(group_Code,Company_Code)";
            ds = clsDAL.SimpleQuery(qry);

            qry = "select * into " + tblPrefix + "AcContacts from AcContacts where 1=1";
            qry = qry + " alter table " + tblPrefix + "AcContacts ADD CONSTRAINT pk_group" + tblPrefix + " primary key(PersonId,Company_Code,Ac_Code)";
            ds = clsDAL.SimpleQuery(qry);
            return "yes";
        }
        catch
        {
            return "";
        }
    }
}