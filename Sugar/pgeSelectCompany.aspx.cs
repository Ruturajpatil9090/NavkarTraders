using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_pgeSelectCompany : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.fillGrid();
        }
    }

    private void fillGrid()
    {
        try
        {
            string qry = "select Company_Name_E,Company_Code from Company where Group_Code='" + Session["Group_Code"].ToString() + "' order by Company_Code asc";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        grdCompany.DataSource = dt;
                        grdCompany.DataBind();
                    }
                }
            }
        }
        catch
        {

        }
    }

    protected void grdCompany_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int rowindex = row.RowIndex;
            Int32 companyCode = 0;
            companyCode = Convert.ToInt32(grdCompany.Rows[rowindex].Cells[0].Text);
            Session["Company_Code"] = companyCode;
            Session["Company_Name"] = clsCommon.getString("select Company_Name_E from Company where Company_Code=" + companyCode);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string qry = "select * from NT_1_CompanyParameters where Company_Code=" + Session["Company_Code"].ToString() + " and Year_Code=" + Session["year"].ToString();

            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                      
                        Session["COMMISSION_AC"] = dt.Rows[0]["COMMISSION_AC"].ToString();
                      
                        Session["INTEREST_AC"] = dt.Rows[0]["INTEREST_AC"].ToString();
                        Session["TRANSPORT_AC"] = dt.Rows[0]["TRANSPORT_AC"].ToString();
                     
                        Session["POSTAGE_AC"] = dt.Rows[0]["POSTAGE_AC"].ToString();
                
                        Session["SELF_AC"] = dt.Rows[0]["SELF_AC"].ToString();
                        Session["AUTO_VOUCHER"] = dt.Rows[0]["AutoVoucher"].ToString();
                  
                        string companygstStateCode = dt.Rows[0]["GSTStateCode"].ToString();
                        Session["CompanyGSTStateCode"] = companygstStateCode;
                        string statename = clsCommon.getString("select State_Name from GSTStateMaster where State_Code=" + companygstStateCode + "");
                        Session["CompanyState"] = statename;
                        Session["SaleCGSTAc"] = dt.Rows[0]["CGSTAc"].ToString();
                        Session["SaleSGSTAc"] = dt.Rows[0]["SGSTAc"].ToString();
                        Session["SaleIGSTAc"] = dt.Rows[0]["IGSTAc"].ToString();
                        Session["PurchaseCGSTAc"] = dt.Rows[0]["PurchaseCGSTAc"].ToString();
                        Session["PurchaseSGSTAc"] = dt.Rows[0]["PurchaseSGSTAc"].ToString();
                        Session["PurchaseIGSTAc"] = dt.Rows[0]["PurchaseIGSTAc"].ToString();
                    }
                }
            }
            Response.Redirect("../Sugar/pgeHome.aspx", false);
        }
        catch
        {

        }
    }
}