using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sugar_BussinessRelated_pgeMultipleBillPrinting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDOprint_Click(object sender, EventArgs e)
    {
        string fromdate = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        string Todate = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        string qry = "Select doc_no,voucherbyname,quantal,sale_rate from qrydohead where doc_date between '" + fromdate + "' and '" + Todate + "' and Year_Code=" + Convert.ToInt16(Session["year"].ToString());
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            dt = ds.Tables[0];
            grddetailHeat.DataSource = dt;
            grddetailHeat.DataBind();
        }
    }
    protected void grddetailHeat_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void grddetailHeat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].ControlStyle.Width = new Unit("20px");
        e.Row.Cells[1].ControlStyle.Width = new Unit("100px");
        e.Row.Cells[2].ControlStyle.Width = new Unit("30px");
        e.Row.Cells[3].ControlStyle.Width = new Unit("30px");
        e.Row.Cells[4].ControlStyle.Width = new Unit("15px");
    }
    protected void btnprintDO_Click(object sender, EventArgs e)
    {
        string dono = "";
        if (grddetailHeat.Rows.Count > 0)
        {
            for (int i = 0; i < grddetailHeat.Rows.Count; i++)
            {
                CheckBox chkcheck = (CheckBox)grddetailHeat.Rows[i].Cells[4].FindControl("chkcheck");

                if (chkcheck.Checked == true)
                {
                    dono = dono + grddetailHeat.Rows[i].Cells[0].Text + ",";
                }
                //  TextBox txtPrint = (TextBox)grddetailHeat.Rows[i].Cells[Print].FindControl("txtPrint");
            }

            if (dono.Length > 0)
            {
                dono = dono.Remove(dono.Length - 1);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:PSauda('" + dono + "')", true);
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

}