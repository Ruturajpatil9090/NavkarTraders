using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Report_rptJVNew : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    string stritemcode = "1";
    string fromDTnew;
    string toDTnew;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        if (!IsPostBack)
        {
            fromDT = Request.QueryString["Fromdate"];
            toDT = Request.QueryString["Todate"];
            lblCompanyName.Text = Session["Company_Name"].ToString();
            //fromDTnew = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            //toDTnew = DateTime.Parse(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
            fromDTnew = DateTime.Parse(fromDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM/dd/yyyy");
            toDTnew = DateTime.Parse(toDT, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM/dd/yyyy");
            this.BindList();
        }

    }
    private void BindList()
    {
        try
        {
            //using (clsDataProvider obj = new clsDataProvider())
            //{
            //qry = "Select distinct(CONVERT(VARCHAR(10),DOC_DATE,103)) as DOC_DATE from NT_1_qryJVAll where [DOC_DATE] between'" + fromDTnew
            //    + "' and '" + toDTnew + "' and Company_Code="
            //    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]=" + Convert.ToInt32(Session["year"].ToString()) + " order by DOC_DATE desc";

            qry = "Select distinct(CONVERT(VARCHAR(10),DOC_DATE,103)) as DOC_DATE,doc_date as dt  from NT_1_qryJVAll where [DOC_DATE] between'" + fromDTnew
               + "' and '" + toDTnew + "' and Company_Code="
               + Convert.ToInt32(Session["Company_Code"].ToString()) + " and [Year_Code]=" + Convert.ToInt32(Session["year"].ToString()) + " order by dt asc";
            ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            //ds = obj.GetDataSet(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    dtl.DataSource = dt;
                    dtl.DataBind();


                }
            }
            //}
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            clsDAL.CloseConnection();
        }
    }

    protected void dtl_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            using (clsDataProvider obj = new clsDataProvider())
            {
                Label item_code = (Label)e.Item.FindControl("lblItemCode");
                stritemcode = item_code.Text;

                //string date = DateTime.Parse(stritemcode, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                string date = DateTime.Parse(stritemcode, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("MM/dd/yyyy");

                //stritemcode = item_code.Text.ToString("dd/MM/yyyy");
                DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
                //qry = "select distinct doc_no,Supplier,netqty,purchasevalue,MARKETSES,supercost,levihead,adat,tdsamount,AMOUNT from NT_1_qryAwakPurchase where Company_Code="
                //    + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                //    + " and DOC_DATE='" + date + "'";
                //qry = "select  [TRAN_TYPE],[DOC_NO],[DOC_DATE],[Ac_Name_E],[NARRATION],[AMOUNT],[DRCR] from [NT_1_qryJVAll] where Company_Code="
                //   + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                //   + " and DOC_DATE='" + date + "'";


                qry = "select  [TRAN_TYPE],[DOC_NO],convert(varchar(10),[DOC_DATE],103) as DOC_DATE,[Ac_Name_E],[NARRATION],[AMOUNT],[DRCR] from [NT_1_qryJVAll] where Company_Code="
                  + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                  + " and DOC_DATE='" + date + "'";
                DataSet dsMill = new DataSet();
                dsMill = clsDAL.SimpleQuery(qry);

                // double Totalbalance = 0.0;

                double debittotal = 0.0;

                double credittotal = 0.0;


                if (dsMill != null)
                {
                    if (dsMill.Tables[0].Rows.Count > 0)
                    {
                        dsMill.Tables[0].Columns.Add(new DataColumn("Debitamnt", typeof(double)));
                        dsMill.Tables[0].Columns.Add(new DataColumn("Creditamnt", typeof(double)));
                        string drcr = "";

                        for (int i = 0; i < dsMill.Tables[0].Rows.Count; i++)
                        {
                            drcr = Convert.ToString(dsMill.Tables[0].Rows[i]["DRCR"]);

                            if (drcr == "D")
                            {
                                dsMill.Tables[0].Rows[i]["Debitamnt"] = dsMill.Tables[0].Rows[i]["AMOUNT"];
                                dsMill.Tables[0].Rows[i]["Creditamnt"] = "0.00";
                            }
                            else
                            {
                                dsMill.Tables[0].Rows[i]["Debitamnt"] = "0.00";

                                dsMill.Tables[0].Rows[i]["Creditamnt"] = dsMill.Tables[0].Rows[i]["AMOUNT"];

                            }
                        }
                        Label lbldebittotl = (Label)e.Item.FindControl("lbldebittotl");
                        Label lblcredittotal = (Label)e.Item.FindControl("lblcredittotal");

                        DataTable dtMill = new DataTable();
                        dtMill = dsMill.Tables[0];

                        debittotal = Convert.ToDouble(dtMill.Compute("SUM(Debitamnt)", string.Empty));
                        credittotal = Convert.ToDouble(dtMill.Compute("SUM(Creditamnt)", string.Empty));


                        lbldebittotl.Text = Convert.ToString(debittotal);
                        lblcredittotal.Text = Convert.ToString(credittotal);
                        dtlDetails.DataSource = dtMill;
                        dtlDetails.DataBind();

                    }
                }
                //}
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

  
    protected void lnkDOC_NO_Click(object sender, EventArgs e)
    {
       
            LinkButton lnkTenderNo = (LinkButton)sender;
            string TranType = "JV"; 
            string sessionName = GetSessionName(TranType);
            string No = lnkTenderNo.Text;   
            sessionName = No;

            if (sessionName != "0")
            {
                Int32 action = 1;
                Int32 count = Convert.ToInt32(clsCommon.getString("select tranid from nt_1_transacthead where doc_no='" + No + "'and Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code='" + Convert.ToInt32(Session["year"]).ToString() + "' and tran_type='JV'"));
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:TN('" + count + "','" + action + "');", true);

            }
            else
            {
                Session[sessionName] = No;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), TranType, "javascript:" + TranType + "();", true);
            }
    }
       public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

       private static string GetSessionName(string TranType)
       {
           string SessionName = string.Empty;
           switch (TranType)
           {
               case "JV":
                   SessionName = "JV_NO";
                   break;

           }
           return SessionName;
       }
}