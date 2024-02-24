using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sugar_Report_pgeMultiple_SaleBill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Print Salebill
    protected void btnPrintSale_Bill_Click(object sender, EventArgs e)
    {
        string dono = "";
        if (grdprintSalebill.Rows.Count > 0)
        {
            for (int i = 0; i < grdprintSalebill.Rows.Count; i++)
            {
                CheckBox chkcheck = (CheckBox)grdprintSalebill.Rows[i].Cells[5].FindControl("chkcheck");

                if (chkcheck.Checked == true)
                {
                    dono = dono + grdprintSalebill.Rows[i].Cells[4].Text + ",";
                }
                //  TextBox txtPrint = (TextBox)grddetailHeat.Rows[i].Cells[Print].FindControl("txtPrint");
            }

            if (dono.Length > 0)
            {
                dono = dono.Remove(dono.Length - 1);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:SB('" + dono + "')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sms", "javascript:alert('Please Select Atleast One Record!')", true);

            }



        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sms", "javascript:alert('Please Select Atleast One Record!')", true);

        }
    }
    #endregion
    #region btnSalePrint_Click
    protected void btnSalePrint_Click(object sender, EventArgs e)
    {
        string fromdate = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        string Todate = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        string qry = "Select distinct doc_no,billtoname,NETQNTL,salerate,saleid from qrysaleheaddetail where doc_date between '" + fromdate + "' and '" + Todate + "' and Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code=" + Convert.ToInt16(Session["year"].ToString());
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            dt = ds.Tables[0];
            grdprintSalebill.DataSource = dt;
            grdprintSalebill.DataBind();
        }
    }
    #endregion
    protected void grdprintSalebill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].ControlStyle.Width = new Unit("20px");
        e.Row.Cells[1].ControlStyle.Width = new Unit("100px");
        e.Row.Cells[2].ControlStyle.Width = new Unit("30px");
        e.Row.Cells[3].ControlStyle.Width = new Unit("30px");
        e.Row.Cells[4].ControlStyle.Width = new Unit("15px");
        e.Row.Cells[5].ControlStyle.Width = new Unit("20px");

        e.Row.Cells[4].Visible = false;
    }
}