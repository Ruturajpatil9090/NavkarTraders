using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;

public partial class Report_pgeBrokerReport : System.Web.UI.Page
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
                txtFromDt.Text = clsGV.Start_Date;
                txtToDt.Text = clsGV.End_Date;
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
                    string str = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster  where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtAcCode.Text);
                    if (str != string.Empty)
                    {
                        lblAcCodeName.Text = str;
                        setFocusControl(btnBrokerDetail);
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
    protected void btnTenderDOReport_Click(object sender, EventArgs e)
    {

        string accode = txtAcCode.Text;


        if (accode == string.Empty)
        {
            accode = "0";

        }
        pnlPopup.Style["display"] = "none";

        string FromDt = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:TenderDOReport('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
    }

    protected void btndobrokerreport_Click(object sender, EventArgs e)
    {

        string accode = txtAcCode.Text;

        if (accode == string.Empty)
        {
            accode = "0";

        }

        pnlPopup.Style["display"] = "none";

        string FromDt = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:Dobrokerreport('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
    }

    protected void btnbrokerwisependingbill_Click(object sender, EventArgs e)
    {

        string accode = txtAcCode.Text;

        if (accode == string.Empty)
        {
            accode = "0";

        }

        pnlPopup.Style["display"] = "none";

        string FromDt = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:brokerwisependibgbil('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
    }


    protected void btnTenderPSBrokerReport_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;


        if (accode == string.Empty)
        {
            accode = "0";
        }
        pnlPopup.Style["display"] = "none";

        string FromDt = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:PSBroker('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
    }
    protected void btnDetailSellBrokerReport_Click(object sender, EventArgs e)
    {

        string accode = txtAcCode.Text;


        if (accode == string.Empty)
        {
            accode = "0";
        }
        pnlPopup.Style["display"] = "none";

        string FromDt = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:DetailSellbrokerReport('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
    }
    protected void btnTenderPSBrokerRptDetails_Click(object sender, EventArgs e)
    {
        string accode = txtAcCode.Text;


        if (accode == string.Empty)
        {
            accode = "0";
        }
        pnlPopup.Style["display"] = "none";

        string FromDt = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        string ToDt = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sh", "javascript:PSBrokerDetails('" + accode + "','" + FromDt + "','" + ToDt + "')", true);
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //if (txtSearchText.Text != string.Empty)
            //{
            //    searchStr = txtSearchText.Text;
            //}
            if (searchStr != string.Empty && strTextbox == hdnfClosePopup.Value)
            {
                txtSearchText.Text = searchStr;
            }
            else
            {
                txtSearchText.Text = searchStr;
            }
            if (hdnfClosePopup.Value == "txtAcCode")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Account--";
                string qry = "select Ac_Code , Ac_Name_E ,Short_Name ,cityname from qrymstaccountmaster where (cityname like '%" + txtSearchText.Text + "%' or Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' order by Ac_Name_E asc";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtPartyCode")
            {
                pnlPopup.Style["display"] = "block";
                lblPopupHead.Text = "--Select Account--";
                string qry = "select Ac_Code , Ac_Name_E ,Short_Name  ,cityname from qrymstaccountmaster where (cityname like '%" + txtSearchText.Text + "%' or Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%') and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' order by Ac_Name_E asc";
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Width = new Unit("60px");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].Width = new Unit("250px");
            e.Row.Cells[2].Width = new Unit("100px");
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
        }
    }
    protected void drpFilter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Command_Click(object sender, CommandEventArgs e)
    {
        try
        {
            Broker_Code = txtAcCode.Text;
            string fromDT = "";
            string toDT = "";
            if (txtFromDt.Text != string.Empty)
            {
                fromDT = DateTime.Parse(txtFromDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }
            else
            {
                fromDT = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }
            if (txtToDt.Text != string.Empty)
            {
                toDT = DateTime.Parse(txtToDt.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }
            else
            {
                toDT = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }

            switch (e.CommandName)
            {
                case "BD":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:br('" + Broker_Code + "','" + fromDT + "','" + toDT + "')", true);
                    break;
                case "BWSP":
                    if (Broker_Code != string.Empty)
                    {
                        qry = "select * from " + tblPrefix + "qryBrokerWiseShortPay WHERE Bill_Amount>0 and " +
                            " doc_date between '" + fromDT + "' and '" + toDT + "' and Brokercode=" + Broker_Code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
                    }
                    else
                    {
                        qry = "select * from " + tblPrefix + "qryBrokerWiseShortPay WHERE Bill_Amount>0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_date between '" + fromDT + "' and '" + toDT + "'";
                    }
                    DataSet ds = new DataSet();
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add(new DataColumn("Broker", typeof(string)));
                        dt.Columns.Add(new DataColumn("Message", typeof(string)));
                        dt.Columns.Add(new DataColumn("Party_Mobile", typeof(string)));
                        dt.Columns.Add(new DataColumn("Sending_Mobile", typeof(string)));
                        dt.Columns.Add(new DataColumn("Short", typeof(string)));
                        dt.Columns.Add(new DataColumn("Short_Payment", typeof(string)));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                DataRow dr = dt.NewRow();
                                string broker = ds.Tables[0].Rows[i]["Broker"].ToString();
                                dr["Broker"] = broker;
                                string v_no = ds.Tables[0].Rows[i]["doc_no"].ToString();
                                string party = ds.Tables[0].Rows[i]["Unit_Name"].ToString();
                                string qntl = ds.Tables[0].Rows[i]["From_Station"].ToString();
                                string vouc_amt = ds.Tables[0].Rows[i]["Bill_Amount"].ToString();
                                string recieved = ds.Tables[0].Rows[i]["Recieved"].ToString();
                                string balance = ds.Tables[0].Rows[i]["Balance"].ToString();
                                string salerate = ds.Tables[0].Rows[i]["Sale_Rate"].ToString();
                                string frieght = ds.Tables[0].Rows[i]["freight"].ToString();
                                double frieght_total = frieght != string.Empty ? Convert.ToDouble(frieght) : 0.00;
                                double sale_rate = salerate != string.Empty ? Convert.ToDouble(salerate) : 0.00;
                                dr["Message"] = "Balance Payment" + " Voc.No-" + v_no + " " + party + "-" + qntl + " " + "Voc.amt " + vouc_amt + " recived " + recieved + " Balance " + balance + "(" + Math.Abs(sale_rate) + "-" + Math.Abs(frieght_total) + ")";
                                dr["Party_Mobile"] = ds.Tables[0].Rows[i]["Unit_Mobile"].ToString();
                                dr["Sending_Mobile"] = ds.Tables[0].Rows[i]["Broker_Mobile"].ToString();
                                dr["Short"] = balance;
                                dr["Short_Payment"] = "Short Payment " + " Voc.No-" + v_no + " " + broker + "-" + qntl + " " + "Voc.amt " + vouc_amt + " recived " + recieved + " Balance " + balance + "(" + Math.Abs(sale_rate) + "-" + Math.Abs(frieght_total) + ")";
                                dt.Rows.Add(dr);
                            }
                            if (dt.Rows.Count > 0)
                            {
                                grdDetail.DataSource = dt;
                                grdDetail.DataBind();
                            }
                            else
                            {
                                grdDetail.DataSource = null;
                                grdDetail.DataBind();
                            }
                        }
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kt", "javascript:bwsp('" + Broker_Code + "','" + fromDT + "','" + toDT + "')", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ksa", "javascript:bwspd('" + Broker_Code + "','" + fromDT + "','" + toDT + "')", true);
                    break;
                case "BWLP":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:bwlp('" + fromDT + "','" + toDT + "','" + Broker_Code + "')", true);
                    break;
                case "BWLPA":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:bwlpa('" + fromDT + "','" + toDT + "','" + Broker_Code + "')", true);
                    break;
                case "BWSPA":
                    if (Broker_Code != string.Empty)
                    {
                        qry = "select * from " + tblPrefix + "qryBrokerWiseShortPayAll WHERE Bill_Amount>0 and " +
                            " doc_date between '" + fromDT + "' and '" + toDT + "' and Brokercode=" + Broker_Code + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "";
                    }
                    else
                    {
                        qry = "select * from " + tblPrefix + "qryBrokerWiseShortPayAll WHERE Bill_Amount>0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_date between '" + fromDT + "' and '" + toDT + "'";
                    }
                    ds = new DataSet();
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add(new DataColumn("Broker", typeof(string)));
                        dt.Columns.Add(new DataColumn("Message", typeof(string)));
                        dt.Columns.Add(new DataColumn("Party_Mobile", typeof(string)));
                        dt.Columns.Add(new DataColumn("Sending_Mobile", typeof(string)));
                        dt.Columns.Add(new DataColumn("Short", typeof(string)));
                        dt.Columns.Add(new DataColumn("Short_Payment", typeof(string)));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                DataRow dr = dt.NewRow();
                                string broker = ds.Tables[0].Rows[i]["Broker"].ToString();
                                dr["Broker"] = broker;
                                string v_no = ds.Tables[0].Rows[i]["doc_no"].ToString();
                                string party = ds.Tables[0].Rows[i]["Unit_Name"].ToString();
                                string qntl = ds.Tables[0].Rows[i]["From_Station"].ToString();
                                string vouc_amt = ds.Tables[0].Rows[i]["Bill_Amount"].ToString();
                                string recieved = ds.Tables[0].Rows[i]["Recieved"].ToString();
                                string balance = ds.Tables[0].Rows[i]["Balance"].ToString();
                                string salerate = ds.Tables[0].Rows[i]["Sale_Rate"].ToString();
                                string frieght = ds.Tables[0].Rows[i]["freight"].ToString();
                                double frieght_total = frieght != string.Empty ? Convert.ToDouble(frieght) : 0.00;
                                double sale_rate = salerate != string.Empty ? Convert.ToDouble(salerate) : 0.00;
                                dr["Message"] = "Balance Payment" + " Voc.No-" + v_no + " " + party + "-" + qntl + " " + "Voc.amt " + vouc_amt + " recived " + recieved + " Balance " + balance + "(" + Math.Abs(sale_rate) + "-" + Math.Abs(frieght_total) + ")";
                                dr["Party_Mobile"] = ds.Tables[0].Rows[i]["Unit_Mobile"].ToString();
                                dr["Sending_Mobile"] = ds.Tables[0].Rows[i]["Broker_Mobile"].ToString();
                                dr["Short"] = balance;
                                dr["Short_Payment"] = "Short Payment " + " Voc.No-" + v_no + " " + broker + "-" + qntl + " " + "Voc.amt " + vouc_amt + " recived " + recieved + " Balance " + balance + "(" + Math.Abs(sale_rate) + "-" + Math.Abs(frieght_total) + ")";
                                dt.Rows.Add(dr);
                            }
                            if (dt.Rows.Count > 0)
                            {
                                grdDetail.DataSource = dt;
                                grdDetail.DataBind();
                            }
                            else
                            {
                                grdDetail.DataSource = null;
                                grdDetail.DataBind();
                            }
                        }
                    }

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kt", "javascript:bwspa('" + Broker_Code + "','" + fromDT + "','" + toDT + "')", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ksa", "javascript:bwspda('" + Broker_Code + "','" + fromDT + "','" + toDT + "')", true);
                    break;
                case "ABD":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ku", "javascript:abr('" + fromDT + "','" + toDT + "')", true);
                    break;
                case "PBR":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kusad", "javascript:pbr('" + fromDT + "','" + toDT + "','" + Broker_Code + "')", true);
                    break;
            }
            pnlPopup.Style["display"] = "none";
        }
        catch
        {

        }
    }
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[4].Width = new Unit("90px");
        e.Row.Cells[6].Width = new Unit("50px");
        e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[2].Width = new Unit("120px");
        e.Row.Cells[3].Width = new Unit("120px");
        e.Row.Cells[5].Width = new Unit("220px");
        e.Row.Cells[0].Width = new Unit("220px");
        e.Row.Cells[0].Style["overflow"] = "hidden";
        int i = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                i++;
                string width = cell.Width.ToString();
                string s = cell.Text;
                if (cell.Text.Length > 30)
                {
                    cell.Text = cell.Text.Substring(0, 30) + "....";
                    cell.ToolTip = s;
                }
            }
        }
    }
    protected void btnEnter_Click(object sender, EventArgs e) { }

    protected void btnSendSms_Click(object sender, EventArgs e)
    {
        try
        {
            if (grdDetail.Rows.Count > 0)
            {
                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    CheckBox grdCB = (CheckBox)grdDetail.Rows[i].Cells[6].FindControl("grdCB");
                    if (grdCB.Checked == true)
                    {
                        TextBox txtSendingMobile = (TextBox)grdDetail.Rows[i].Cells[3].FindControl("txtSendingMobile");
                        TextBox txtPartyMobile = (TextBox)grdDetail.Rows[i].Cells[2].FindControl("txtPartyMobile");
                        string sendingMobile = txtSendingMobile.Text;
                        string Party_Mobile = txtPartyMobile.Text;
                        string msg = grdDetail.Rows[i].Cells[1].ToolTip.ToString();
                        string API = clsGV.msgAPI;
                        string Url = API + "mobile=" + sendingMobile + "," + Party_Mobile + "&message=" + msg + "&senderid=NAVKAR&accusage=1";
                        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(Url);
                        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                        StreamReader reader = new StreamReader(myResp.GetResponseStream());
                        string respString = reader.ReadToEnd();
                        reader.Close();
                        myResp.Close();
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnBrokerWiseShortNew_Click(object sender, EventArgs e)
    {

    }
    protected void txtPartyCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtPartyCode.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtPartyCode.Text);
                if (a == false)
                {
                    //searchStr = txtAcCode.Text;
                    strTextbox = "txtAcCode";
                    searchStr = txtPartyCode.Text;
                    btnPartyCode_Click(this, new EventArgs());
                }
                else
                {
                    searchStr = txtPartyCode.Text;
                    strTextbox = "txtPartyCode";
                    string str = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster order by asc where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code=" + txtPartyCode.Text);
                    if (str != string.Empty)
                    {
                        lblPartyName.Text = str;
                        setFocusControl(txtFromDt);
                    }
                    else
                    {
                        txtPartyCode.Text = string.Empty;
                        lblPartyName.Text = string.Empty;
                    }
                }
            }
        }
        catch
        {

        }
    }
    protected void btnPartyCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtPartyCode";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
}