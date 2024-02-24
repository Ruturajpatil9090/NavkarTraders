using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sugar_Report_rptcheckpendingsalebill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            this.bindData();
        }
    }

    private void bindData()
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


                //lblcityName.Text = clsCommon.getString("select [city_name_e] from " + cityMasterTable + " where [city_code]=" + cityCode + " and [company_code]=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                string qry = "";
                qry = "SELECT doc_no as DO_NO,doc_dateConverted as doc_Date,Do_Date_Conv as Do_Date,quantal,salebillname,millname FROM qrydohead where  (SB_NO is null or SB_No=0)  and desp_type!='DO' and purc_no!=0 and SaleBillTo!=0 and SaleBillTo!=2 and SaleBillTo!=GETPASSCODE and Company_Code="
                    + Convert.ToInt32(Session["Company_Code"].ToString())+" and Year_Code="+Convert.ToInt32(Session["year"].ToString());
                ds = clsDAL.SimpleQuery(qry);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            dtlAcList.DataSource = dt;
                            dtlAcList.DataBind();
                        }
                    }
                }

            
        }
        catch (Exception eec)
        {
            Response.Write(eec.Message);
        }
    }
    protected void lbkTenderNo_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkTenderNo = (LinkButton)sender;
            DataListItem item = (DataListItem)lnkTenderNo.NamingContainer;
            string No = lnkTenderNo.Text;
           // Session["TN_NO"] = No;
            int do_id = Convert.ToInt32(clsCommon.getString("select doid from nt_1_deliveryorder where doc_no=" + No +
                     " and Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" +
                     " " + Session["year"].ToString() + ""));

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:DoOPen('" + do_id + "')", true);
         //   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tnjhj", "javascript:TN();", true);
        }
        catch (Exception)
        {
            throw;
        }
    }
}