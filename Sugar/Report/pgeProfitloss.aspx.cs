using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;

public partial class Report_pgeProfitloss : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string searchStr = string.Empty;
    string strTextbox = string.Empty;
    static WebControl objAsp = null;
    string AccountMasterTable = string.Empty;
    string qry = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    string Broker_Code = string.Empty;
    string fromDT = string.Empty;
    string toDT = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["user"].ToString();
        tblPrefix = Session["tblPrefix"].ToString();
        AccountMasterTable = tblPrefix + "AccountMaster";
        if (!Page.IsPostBack)
        {

            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }

        }
    }
    protected void txtAcCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "none";
            if (txtAcCode.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtAcCode.Text);
                if (a == false)
                {
                    searchStr = txtAcCode.Text;
                    strTextbox = "txtAcCode";
                    btnAcCode_Click(this, new EventArgs());
                }
                else
                {
                    searchStr = txtAcCode.Text;
                    strTextbox = "txtAcCode";
                    string str = clsCommon.getString("select Ac_Name_E from " + tblPrefix + "AccountMaster where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAcCode.Text);
                    if (str != string.Empty)
                    {
                        lblAcCodeName.Text = str;
                        setFocusControl(txtLotNo);
                    }
                    else
                    {
                        txtAcCode.Text = string.Empty;
                        lblAcCodeName.Text = string.Empty;
                    }
                }
            }
        }
        catch
        {

        }
    }

    protected void txtLotNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "none";
            if (txtLotNo.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtLotNo.Text);
                if (a == false)
                {
                   // searchStr = txtLotNo.Text;
                   // strTextbox = "txtLotNo";
                    btnLotNo_Click(this, new EventArgs());
                }
                else
                {
                   // searchStr = txtLotNo.Text;
                    //strTextbox = "txtLotNo";
                    string str = clsCommon.getString("select convert(varchar(10),Tender_Date,103) as Tender_Date from nt_1_tender where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tender_No=" + txtLotNo.Text);                
                    if (str != string.Empty)
                    {
                        lbllotno.Text = str;
                        setFocusControl(txtFromDate);
                    }
                    else
                    {
                        txtLotNo.Text = string.Empty;
                        lbllotno.Text = string.Empty;
                    }
                }
            }
        }
        catch
        {

        }
    }
    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion
    protected void btnAcCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtAcCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void btnLotNo_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    pnlPopup.Style["display"] = "block";
        //    hdnfClosePopup.Value = "txtLotNo";
        //    btnSearch_Click(sender, e);
        //}
        //catch
        //{
        //}
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtLotNo";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSearchText.Text != string.Empty)
            {
                searchStr = txtSearchText.Text;
            }
            if (searchStr != string.Empty && strTextbox == hdnfClosePopup.Value)
            {
                txtSearchText.Text = searchStr;
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
            }
            if (hdnfClosePopup.Value == "txtAcCode")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Account--";
                string qry = "select Ac_Code as Account_Code, Ac_Name_E as Account_Name,Short_Name as Short_Name from " + AccountMasterTable + " where (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtLotNo")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Account--";
                qry = "select Tender_No,convert(varchar(10),Tender_Date,103) as Tender_Date from nt_1_tender" + " where (Tender_No like '%" + txtSearchText.Text + "%' or Tender_Date like '%" + txtSearchText.Text + "%') and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                 " and Mill_Code=" + txtAcCode.Text;
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtPartyCode")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Account--";
                string qry = "select Ac_Code as [Account Code], Ac_Name_E as [Account Name],Short_Name as [Short Name] from " + AccountMasterTable + " where (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                this.showPopup(qry);
            }
        }
        catch
        {

        }
    }
    private void showPopup(string qry)
    {
        try
        {
            this.setFocusControl(txtSearchText);
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
                        grdPopup.DataSource = dt;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = grdPopup.PageCount.ToString();
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = "0";
                    }
                }
            }
        }
        catch
        {
        }
    }
    protected void imgBtnClose_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "none";
            txtSearchText.Text = string.Empty;
            grdPopup.DataSource = null;
            grdPopup.DataBind();
        }
        catch
        {
        }
    }
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }
    protected void grdPopup_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
            (e.Row.RowState == DataControlRowState.Normal ||
            e.Row.RowState == DataControlRowState.Alternate))
            {
                e.Row.TabIndex = -1;

                e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
                e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
                e.Row.Attributes["onselectstart"] = "javascript:return true;";
            }
        }
        catch
        {
            throw;
        }
    }
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[0].Width = new Unit("60px");
        //    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //    e.Row.Cells[1].Width = new Unit("250px");
        //    e.Row.Cells[2].Width = new Unit("100px");
        //    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
        //}
        try
        {
            string v = hdnfClosePopup.Value;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Width = new Unit("50px");
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            if (e.Row.RowType != DataControlRowType.Pager)
            {
                if (v == "txtAcCode")
                {

                    e.Row.Cells[0].Width = new Unit("30px");
                    e.Row.Cells[1].Width = new Unit("100px");
                    e.Row.Cells[2].Width = new Unit("80px");
                   // e.Row.Cells[4].Width = new Unit("100px");

                }
                if (v == "txtLotNo")
                {
                    e.Row.Cells[0].Width = new Unit("50px");
                    e.Row.Cells[1].Width = new Unit("150px");                    
                }
               
            }
        }
        catch
        {
        }
    }
    protected void btnProfitLoss_Click(object sender, EventArgs e)
    {
        try
        {
            string Ac_Code = string.Empty;
            string lotno = txtLotNo.Text;
            if (txtFromDate.Text != string.Empty)
            {
                fromDT = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            }
            else
            {
                fromDT = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            }
            if (txtToDate.Text != string.Empty)
            {
                toDT = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            }
            else
            {
                toDT = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            }

            if (txtAcCode.Text != string.Empty)
            {
                Ac_Code = txtAcCode.Text;
            }
            else
            {
                setFocusControl(txtAcCode);
                return;
            }
            //if (txtLotNo.Text != string.Empty)
            //{
            //    lotno = txtLotNo.Text;
            //}
            //else
            //{
            //    lotno = txtLotNo.Text;
            //    setFocusControl(txtLotNo);
            //    return;

            //}
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:Profitloss('" + lotno + "','" + Ac_Code + "','" + fromDT + "','" + toDT + "')", true);
        }
        catch
        {

        }
    }


    protected void btntenderpurchesregister_Click(object sender, EventArgs e)
    {
        try
        {
            string Ac_Code = string.Empty;
            string lotno = txtLotNo.Text;
            if (txtFromDate.Text != string.Empty)
            {
                fromDT = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            }
            else
            {
                fromDT = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            }
            if (txtToDate.Text != string.Empty)
            {
                toDT = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            }
            else
            {
                toDT = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            }

            if (txtAcCode.Text != string.Empty)
            {
                Ac_Code = txtAcCode.Text;
            }
            else
            {
                setFocusControl(txtAcCode);
                return;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:TenderpurchesProfitloss('" + lotno + "','" + Ac_Code + "','" + fromDT + "','" + toDT + "')", true);
        }
        catch
        {

        }
    }
}