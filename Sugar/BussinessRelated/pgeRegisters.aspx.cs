using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;

public partial class Report_pgeRegisters : System.Web.UI.Page
{
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    string tblPrefix = string.Empty;
    string searchString = string.Empty;
    string AccountMasterTable = string.Empty;
    string LotNo = string.Empty;
    static WebControl objAsp = null;
    string strTextbox = string.Empty;
    string Sr_No = string.Empty;
    string fromDT = string.Empty;
    string toDT = string.Empty;
    string Branch_Code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["user"].ToString();
        tblPrefix = Session["tblPrefix"].ToString();
        AccountMasterTable = tblPrefix + "AccountMaster";
        if (!Page.IsPostBack)
        {
            //txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyy");
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                fillBranches();
                drpBranch.SelectedValue = Session["Branch_Code"].ToString();
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                setFocusControl(txtMillCode);
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false); 
            }
        }
    }
    private void fillBranches()
    {
        try
        {
            ListItem li = new ListItem("All", "0");
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string qry = "select * from BranchMaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
            ds = clsDAL.SimpleQuery(qry);
            drpBranch.Items.Clear();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        drpBranch.DataSource = dt;
                        drpBranch.DataTextField = "Branch";
                        drpBranch.DataValueField = "Branch_Id";
                        drpBranch.DataBind();
                    }
                }
            }
            drpBranch.Items.Insert(0, li);
        }
        catch
        {
             
        }
    }

    protected void btnDispRegister_Click(object sender, EventArgs e)
    {
        try
        {
            datefunction();
            BranchCode();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:sp('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
        }
        catch
        {

        }
    }

    private void datefunction()
    {
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
    }
    protected void btndispsummarynew_Click(object sender, EventArgs e)
    {
        datefunction();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "poi", "javascript:DispSummarynew('" + fromDT + "','" + toDT + "')", true);
    }
    protected void btnResaleDiff_Click(object sender, EventArgs e)
    {
        datefunction();
        BranchCode();
        if (hdconfirm.Value == "Yes")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "aqw", "javascript:difftopay('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "asad", "javascript:difftorecieve('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "liii", "javascript:rsdp('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
        }
    }
    protected void btnMillCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "MM";
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
            if (searchString != string.Empty)
            {
                txtSearchText.Text = searchString;
            }
            if (hdnfClosePopup.Value == "MM")
            {
                lblPopupHead.Text = "--Select Mill--";

                //string qry = "select Ac_Code as [Account Code], Ac_Name_E as [Account Name],Short_Name as [Short Name] from " + AccountMasterTable + " where (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%') and Ac_type='M' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' order by Ac_Name_E asc";

                txtSearchText.Text = txtMillCode.Text;
                string qry = "SELECT dbo.NT_1_AccountMaster.Ac_Code as Account_Code,dbo.NT_1_AccountMaster.Ac_Name_E as Account_Name, dbo.NT_1_AccountMaster.Short_Name as Short_Name," +
                    "dbo.NT_1_CityMaster.city_name_e as City_Name FROM  dbo.NT_1_AccountMaster LEFT OUTER JOIN  dbo.NT_1_CityMaster ON dbo.NT_1_AccountMaster.City_Code = dbo.NT_1_CityMaster.city_code AND " +
        " dbo.NT_1_AccountMaster.Company_Code = dbo.NT_1_CityMaster.company_code  WHERE (Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or Short_Name like '%" + txtSearchText.Text + "%' or city_name_e like '%" + txtSearchText.Text + "%') and (dbo.NT_1_AccountMaster.Ac_type = 'M') and  dbo.NT_1_AccountMaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " ORDER BY dbo.NT_1_AccountMaster.Ac_Name_E";

               
                this.showPopup(qry);
            }
        }
        catch
        { }
    }
    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (hdnfClosePopup.Value == "Close")
            {
                txtSearchText.Text = string.Empty;
                pnlPopup.Style["display"] = "none";
                grdPopup.DataSource = null;
                grdPopup.DataBind();

                if (objAsp != null)

                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
            }
            else
            {
                pnlPopup.Style["display"] = "block";

                searchString = txtSearchText.Text;
                strTextbox = hdnfClosePopup.Value;

                setFocusControl(btnSearch);
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
            if (hdnfClosePopup.Value == "MM")
            {
                setFocusControl(txtMillCode);
            }
            hdnfClosePopup.Value = "Close";
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
                e.Row.Attributes["onselectstart"] = "javascript:return false;";

                // e.Row.Attributes["onkeyup"] = "javascript:return selectRow(event);";
            }
        }
        catch
        {
            throw;
        }
    }
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            if (v == "MM")
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Width = new Unit("100px");
                    e.Row.Cells[1].Width = new Unit("400px");
                    e.Row.Cells[2].Width = new Unit("100px");
                }
                if (e.Row.RowType != DataControlRowType.Pager)
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }
    }
    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion
    #region [Popup Button Code]
    protected void showPopup(string qry)
    {
        try
        {
            setFocusControl(txtSearchText);
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
    #endregion
    protected void txtMillCode_TextChanged(object sender, EventArgs e)
    {
        strTextbox = "txtMillCode";
        searchString = txtMillCode.Text;
        csCalculations();
       
    }

    #region csCalculations
    private void csCalculations()
    {
        try
        {
            hdnfClosePopup.Value = "Close";
            string millName = string.Empty;
            searchString = txtMillCode.Text;
            if (txtMillCode.Text != string.Empty)
            {
                bool a = clsCommon.isStringIsNumeric(txtMillCode.Text);
                if (a == false)
                {
                    btnMillCode_Click(this, new EventArgs());
                }
                else
                {
                    millName = clsCommon.getString("select Ac_Name_E from " + AccountMasterTable + " where Ac_Code='" + txtMillCode.Text + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Ac_type='M'");
                    if (millName != string.Empty)
                    {
                        lblMillName.Text = millName;
                        setFocusControl(txtLotNo);
                    }
                    else
                    {
                        txtMillCode.Text = string.Empty;
                        lblMillName.Text = string.Empty;
                        setFocusControl(txtMillCode);
                    }
                }
            }
            else
            {
                txtMillCode.Text = string.Empty;
                lblMillName.Text = millName;
                setFocusControl(txtMillCode);
            }

        }
        catch
        {
        }
    }
            #endregion

    protected void txtLotNo_TextChanged(object sender, EventArgs e)
    {
        if (txtLotNo.Text != string.Empty)
        {
            lbllotno.Text = "";
            setFocusControl(txtSrNo);
        }
    }
    protected void txtSrNo_TextChanged(object sender, EventArgs e)
    {
        LotNo = txtLotNo.Text;
        if (LotNo != string.Empty)
        {
            // qry = "Select ID from " + tblPrefix + "Tenderdetails WHERE Tender_No=" + LotNo + "";
            Sr_No = txtSrNo.Text;
            qry = "Select A.Ac_Name_E from " + tblPrefix + "Tenderdetails T left outer join " + AccountMasterTable + " A on T.Buyer=A.Ac_Code WHERE T.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and T.year_code=" + Convert.ToInt32(Session["year"].ToString()) + " and T.ID=" + Sr_No + " AND T.Tender_No=" + LotNo + "";
            string buyer = clsCommon.getString(qry);
            if (buyer != string.Empty)
            {
                lblBuyer.Text = buyer;
                lblSrNotExist.Text = "";
            }
            else
            {
                lblSrNotExist.Text = "Serial Number Not Exist!";
                lblBuyer.Text = "";
                txtSrNo.Text = string.Empty;
                setFocusControl(txtSrNo);
            }
        }
        else
        {
            lbllotno.Text = "Please Enter Lot No!";
            txtSrNo.Text = string.Empty;
            setFocusControl(txtLotNo);
        }
    }
    protected void btnDispDetails_Click(object sender, EventArgs e)
    {
        try
        {
            datefunction();
            BranchCode();
            string Mill_Code = txtMillCode.Text;
            LotNo = txtLotNo.Text;
            Sr_No = txtSrNo.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ky", "javascript:DD('" + fromDT + "','" + toDT + "','" + Mill_Code + "','" + LotNo + "','" + Sr_No + "','" + Branch_Code + "')", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kysds", "javascript:DDN('" + fromDT + "','" + toDT + "','" + Mill_Code + "','" + LotNo + "','" + Sr_No + "','" + Branch_Code + "')", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nnnn", "javascript:DDNM('" + fromDT + "','" + toDT + "','" + Mill_Code + "','" + LotNo + "','" + Sr_No + "','" + Branch_Code + "')", true);
            
            pnlPopup.Style["Display"] = "None";
        }
        catch
        {
        }
    }
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[0].Width = new Unit("40px");
        e.Row.Cells[1].Width = new Unit("200px");
        e.Row.Cells[1].Style.Add("overflow", "hidden");
        e.Row.Cells[2].Width = new Unit("250px");
        e.Row.Cells[3].Width = new Unit("80px");
        e.Row.Cells[4].Width = new Unit("80px");
        e.Row.Cells[5].Width = new Unit("80px");
        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;

        int i = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                i++;
                string s = cell.Text;
                if (cell.Text.Length > 38)
                {
                    cell.Text = cell.Text.Substring(0, 38) + "....";
                    cell.ToolTip = s;
                }
            }
        }
    }
    protected void imgEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDetails = sender as ImageButton;
        GridViewRow gr = (GridViewRow)btnDetails.NamingContainer;
        txtDriverMobile.Text = gr.Cells[4].Text;
        txtPartyMobile.Text = gr.Cells[5].Text;
        this.modelPopup1.Show();
    }
    protected void grdDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        this.modelPopup1.Hide();
    }
    protected void btnFrieghtReg_Click(object sender, EventArgs e)
    {
        datefunction();
        BranchCode();
        Branch_Code =Session["Branch_Code"].ToString();
        if (string.IsNullOrEmpty(Branch_Code))
        {
            qry = "select d.doc_no as Memo_No,a.Ac_Name_E as Party,Convert(varchar(10),d.doc_date,103) as dt,b.Short_Name as mill,d.quantal,d.truck_no,ISNULL((d.Memo_Advance-d.vasuli_amount),0) as Advance,d.MobileNo as [Driver Mobile],a.Mobile_No as [Party Mobile]," +
            " d.FreightPerQtl as frieght" +
            " from " + tblPrefix + "deliveryorder d  left outer join " + tblPrefix + "AccountMaster a on d.GETPASSCODE=a.Ac_Code AND d.company_code=a.Company_Code" +
            " left outer join " + tblPrefix + "AccountMaster b on d.mill_code=b.Ac_Code AND d.company_code=b.Company_Code where d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.tran_type='DO' and d.doc_date between '" + fromDT + "' and '" + toDT + "'";
        }
        else
        {
            qry = "select d.doc_no as Memo_No,a.Ac_Name_E as Party,Convert(varchar(10),d.doc_date,103) as dt,b.Short_Name as mill,d.quantal,d.truck_no,ISNULL((d.Memo_Advance-d.vasuli_amount),0) as Advance,d.MobileNo as [Driver Mobile],a.Mobile_No as [Party Mobile]," +
                       " d.FreightPerQtl as frieght" +
                       " from " + tblPrefix + "deliveryorder d  left outer join " + tblPrefix + "AccountMaster a on d.GETPASSCODE=a.Ac_Code AND d.company_code=a.Company_Code" +
                       " left outer join " + tblPrefix + "AccountMaster b on d.mill_code=b.Ac_Code AND d.company_code=b.Company_Code where d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and d.Branch_Code=" + Branch_Code + " and d.tran_type='DO' and d.doc_date between '" + fromDT + "' and '" + toDT + "'";

        }
        string DriverMobile = "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            dt.Columns.Add(new DataColumn("Memo_No", typeof(string)));
            dt.Columns.Add(new DataColumn("Party", typeof(string)));
            dt.Columns.Add(new DataColumn("Name Of Account", typeof(string)));
            dt.Columns.Add(new DataColumn("DriverMobile", typeof(string)));
            dt.Columns.Add(new DataColumn("PartMobile", typeof(string)));
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Memo_No"] = ds.Tables[0].Rows[i]["Memo_No"].ToString();
                    dr["Party"] = ds.Tables[0].Rows[i]["Party"].ToString();
                    string date = ds.Tables[0].Rows[i]["dt"].ToString();
                    string mill = ds.Tables[0].Rows[i]["mill"].ToString();
                    string qntl = ds.Tables[0].Rows[i]["quantal"].ToString();
                    string truckno = ds.Tables[0].Rows[i]["truck_no"].ToString();
                    string frieght = ds.Tables[0].Rows[i]["frieght"].ToString();
                    string advance = ds.Tables[0].Rows[i]["Advance"].ToString();
                    dr["Name Of Account"] = "dt-" + date + "-" + mill + "-" + Math.Abs(double.Parse(qntl)) + "-" + truckno + "-" + "frieght" + " " + Math.Abs(double.Parse(frieght)) + "-" + "Advance" + " " + advance;
                    DriverMobile = ds.Tables[0].Rows[i]["Driver Mobile"].ToString();
                    dr["DriverMobile"] = DriverMobile;
                    dr["PartMobile"] = ds.Tables[0].Rows[i]["Party Mobile"].ToString();
                    dt.Rows.Add(dr);
                }
                if (dt.Rows.Count > 0)
                {
                    //TemplateField drivermob = new TemplateField();
                    //drivermob.HeaderText = "drivermob Mobile";
                    //drivermob.ItemTemplate = new GridViewTemplate(DataControlRowType.DataRow, "DriverMobile", dt.Columns["DriverMobile"].ToString(), new TextBox());
                    //grdDetail.Columns.Add(drivermob);   
                    grdDetail.DataSource = dt;
                    grdDetail.DataBind();
                    grdDetail.Visible = true;
                    grdDeliverySms.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "li", "javascript:fr('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
                }
                else
                {
                    grdDetail.DataSource = null;
                    grdDetail.DataBind();
                }
            }
            else
            {
                grdDetail.DataSource = null;
                grdDetail.DataBind();
            }
        }
    }
    protected void btnVasuliRegister_Click(object sender, EventArgs e)
    {
        datefunction();
        BranchCode();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "po", "javascript:vr('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "po2", "javascript:vr2('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
    }
    protected void btnenterkey_Click(object sender, EventArgs e)
    { }
    protected void btnSendSms_Click(object sender, EventArgs e)
    {
        if (grdDetail.Visible)
        {
            try
            {
                foreach (GridViewRow gr in grdDetail.Rows)
                {
                    CheckBox grdCB = gr.Cells[5].FindControl("grdCB") as CheckBox;
                    TextBox txtDriverMobile = gr.Cells[3].FindControl("TextBox1") as TextBox;
                    TextBox txtPartyMobile = gr.Cells[3].FindControl("TextBox2") as TextBox;
                    if (grdCB.Checked == true)
                    {
                        string msg = gr.Cells[2].ToolTip.ToString();
                        string driverMobile = txtDriverMobile.Text;
                        string mobile = txtPartyMobile.Text;
                        string message = msg + " Driver Mob:" + driverMobile;
                        SendSms(mobile, message);
                    }
                }
            }
            catch
            {
            }
        }
        else if (grdDeliverySms.Visible)
        {
            try
            {
                foreach (GridViewRow gr in grdDeliverySms.Rows)
                {
                    CheckBox grdCB = gr.Cells[6].FindControl("grdCB") as CheckBox;
                    TextBox txtPartyMobile = gr.Cells[5].FindControl("TextBox2") as TextBox;
                    if (grdCB.Checked == true)
                    {
                        string msg = gr.Cells[2].ToolTip.ToString();
                        string mobile = txtPartyMobile.Text;
                        string message = msg;
                        string donumber = gr.Cells[0].Text.ToString();
                        SendSms(mobile, message);
                        DataSet dssent = new DataSet();
                        dssent = clsDAL.SimpleQuery("Update " + tblPrefix + "deliveryorder set LoadingSms='Y' where company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_no=" + donumber + " and tran_type='DO'");
                    }
                }
            }
            catch
            {
            }
        }

    }

    private static void SendSms(string mobile, string message)
    {
        try
        {
            string API = clsGV.msgAPI;
            string Url = API + "mobile=" + mobile + "&message=" + message + "&senderid=NAVKAR&accusage=1";
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(Url);
            HttpWebResponse resp = (HttpWebResponse)myReq.GetResponse();
            StreamReader reder = new StreamReader(resp.GetResponseStream());
            string respString = reder.ReadToEnd();
            reder.Close();
            resp.Close();
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnDispatchDiff_Click(object sender, EventArgs e)
    {
        datefunction();
        BranchCode();
        if (hdconfirm.Value == "Yes")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "qaqw", "javascript:dispdifftopay('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "assaad", "javascript:dispdifftorecieve('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "popya", "javascript:DispDiff('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
        }
    }
    protected void btnDispSummary_Click(object sender, EventArgs e)
    {
        datefunction();
        BranchCode();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "poisd", "javascript:DispSummarySmall('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "poi", "javascript:DispSummary('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
    }
    protected void btnDispMillWise_Click(object sender, EventArgs e)
    {
        datefunction();
        BranchCode();
        string Mill_Code = txtMillCode.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "posi", "javascript:dispmillwise('" + fromDT + "','" + toDT + "','" + Mill_Code + "','" + Branch_Code + "')", true);
    }

    protected void btnDispGradeWise_Click(object sender, EventArgs e)
    {
        datefunction();
        BranchCode();
        string Mill_Code = txtMillCode.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "posi", "javascript:dispgradewise('" + fromDT + "','" + toDT + "','" + Mill_Code + "','" + Branch_Code + "')", true);
    }

    protected void btnpurdescpmillwise_Click(object sender, EventArgs e)
    {
        datefunction();
        BranchCode();
        string Mill_Code = txtMillCode.Text;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "posi", "javascript:dispurchesmillwise('" + fromDT + "','" + toDT + "','" + Mill_Code + "','" + Branch_Code + "')", true);
    }
    protected void drpBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private string BranchCode()
    {
        try
        {
            string branchname = drpBranch.SelectedItem.ToString();
            qry = "select Branch_Id from BranchMaster where Branch='" + branchname + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "";
            Branch_Code = clsCommon.getString(qry);

        }
        catch (Exception)
        {
            throw;
        }
        return Branch_Code;
    }
    protected void btnCatWiseDisp_Click(object sender, EventArgs e)
    {
        datefunction();
        Branch_Code = BranchCode();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hgyf", "javascript:MWDR('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hgyasaf", "javascript:TWDR('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hgyasf", "javascript:DOWDR('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnBSS_Click(object sender, EventArgs e)
    {
        datefunction();
        Branch_Code = BranchCode();
        string Mill_Code = string.Empty;
        if (txtMillCode.Text != string.Empty)
        {
            Mill_Code = txtMillCode.Text;
        }
        else
        {
            Mill_Code = "0";
        }
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hgyasdasf", "javascript:BSS('" + fromDT + "','" + toDT + "','" + Branch_Code + "','" + Mill_Code + "')", true);
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnPartyWiseDO_Click(object sender, EventArgs e)
    {
        try
        {
            datefunction();
            Branch_Code = BranchCode();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hgyaf", "javascript:PWDO('" + fromDT + "','" + toDT + "','" + Branch_Code + "','')", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hassdaf", "javascript:PWDOM('" + fromDT + "','" + toDT + "','" + Branch_Code + "','')", true);
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnTransportBal_Click(object sender, EventArgs e)
    {
        datefunction();
        Branch_Code = BranchCode();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "hgysdaf", "javascript:TBR('" + fromDT + "','" + toDT + "','','" + Branch_Code + "')", true);

    }
    protected void btnDOVasli_Click(object sender, EventArgs e)
    {
        datefunction();
        Branch_Code = BranchCode();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "anbgs", "javascript:DOV('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
    }

    protected void btnDispDetailMill_Click(object sender, EventArgs e)
    {
        try
        {
            datefunction();
            BranchCode();
            string Mill_Code = txtMillCode.Text;
            LotNo = txtLotNo.Text;
            Sr_No = txtSrNo.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kysadsd", "javascript:DDM('" + fromDT + "','" + toDT + "','" + Mill_Code + "','" + LotNo + "','" + Sr_No + "','" + Branch_Code + "')", true);
        }
        catch
        {
        }
    }
    protected void btnWithPayment_Click(object sender, EventArgs e)
    {
        try
        {
            datefunction();
            BranchCode();
            if (hdconfirm.Value == "Yes")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "aqdsw", "javascript:difftopayWithPay('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "asasdsd", "javascript:difftorecieveWithPay('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "liii", "javascript:rsdpWithPay('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnDeliverySms_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text != string.Empty)
            {
                fromDT = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }
            else
            {
                fromDT = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }
            if (txtToDate.Text != string.Empty)
            {
                toDT = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }
            else
            {
                toDT = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }

            //qry = "select d.doc_no,CONVERT(VARCHAR(10),d.doc_date,103) as dt,d.PartyShortName as Party,d.millShortName as millshortname,d.GetPassName,d.quantal as qtl,d.grade,d.truck_no,d.voucher_by,v.Short_Name as vouchershortnm," +
            //        " (d.FreightPerQtl+d.vasuli_rate1) as frieght,d.driver_no,a.Mobile_No as PartyMobile,d.SB_No from " 
            //        + tblPrefix + "qryDeliveryOrderListReport d left outer join " + tblPrefix 
            //        + "AccountMaster a ON d.company_code=a.Company_Code and d.voucher_by=a.Ac_Code LEFT outer join NT_1_AccountMaster v on d.voucher_by=v.Ac_Code and d.company_code=v.Company_Code "
            //        +"where tran_type='DO' and " +
            //        " d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) 
            //        + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
            //        " and d.doc_date between '" + fromDT + "' and '" + toDT + "' and  ISNULL(d.LoadingSms,'N')!='Y'";

            qry = "SELECT     d.doc_no, CONVERT(VARCHAR(10), d.doc_date, 103) AS dt, d.PartyShortName AS Party, d.millShortName AS millshortname, d.GetPassName, d.quantal AS qtl, d.grade, d.truck_no, d.voucher_by, "
                      + " v.Short_Name AS vouchershortnm, d.FreightPerQtl + d.vasuli_rate1 AS frieght, d.driver_no, a.Mobile_No AS PartyMobile, d.SB_No,isnull((t.Mobile_No +','+SalebillTO.Mobile_No),'') as transportmob  "
                     + " FROM  dbo.NT_1_qryDeliveryOrderListReport AS d LEFT OUTER JOIN   dbo.NT_1_AccountMaster AS SalebillTO ON d.SaleBillTo = SalebillTO.Ac_Code AND d.company_code = SalebillTO.Company_Code LEFT OUTER JOIN " +
                    "  dbo.NT_1_AccountMaster AS t ON d.transport = t.Ac_Code AND d.company_code = t.Company_Code LEFT OUTER JOIN " +
                     " dbo.NT_1_AccountMaster AS a ON d.company_code = a.Company_Code AND d.voucher_by = a.Ac_Code LEFT OUTER JOIN " +
                     " dbo.NT_1_AccountMaster AS v ON d.voucher_by = v.Ac_Code AND d.company_code = v.Company_Code" +
                        "  where tran_type='DO' and " +
                  " d.company_code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                  + " and d.Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                  " and d.doc_date between '" + fromDT + "' and '" + toDT + "' and  ISNULL(d.LoadingSms,'N')!='Y'";



            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("dono", typeof(string)));
                dt.Columns.Add(new DataColumn("Party", typeof(string)));
                dt.Columns.Add(new DataColumn("msg", typeof(string)));
                dt.Columns.Add(new DataColumn("driver_no", typeof(string)));
                dt.Columns.Add(new DataColumn("frieght", typeof(string)));
                dt.Columns.Add(new DataColumn("PartyMobile", typeof(string)));
                dt.Columns.Add(new DataColumn("mill", typeof(string)));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    string dono = ds.Tables[0].Rows[i]["doc_no"].ToString();
                    string party = ds.Tables[0].Rows[i]["Party"].ToString();
                    string date = ds.Tables[0].Rows[i]["dt"].ToString();
                    string millshort = ds.Tables[0].Rows[i]["millshortname"].ToString();
                    string getpassname = ds.Tables[0].Rows[i]["GetPassName"].ToString();
                    string frieght = ds.Tables[0].Rows[i]["frieght"].ToString();
                    string qntl = ds.Tables[0].Rows[i]["qtl"].ToString();
                    string grade = ds.Tables[0].Rows[i]["grade"].ToString();
                    string truck_no = ds.Tables[0].Rows[i]["truck_no"].ToString();
                    string driver_no = ds.Tables[0].Rows[i]["driver_no"].ToString();
                    string vouchershortnm = ds.Tables[0].Rows[i]["vouchershortnm"].ToString();
                    string salebillno = ds.Tables[0].Rows[i]["SB_No"].ToString();
                    string transportmob = ds.Tables[0].Rows[i]["transportmob"].ToString();
                    //string msg = "Do." + dono + ".The truck is confirm load Dt. " + date + " " + millshort + " Getpass:" + getpassname
                    //    + " Shipped to:" + vouchershortnm + " Qntl:" + qntl + " " + grade + " " + truck_no + " Frieght(" + frieght + ") Drviver Mob:"
                    //    + driver_no + " Sale Bill No: " + salebillno + "";
                    string msg = string.Empty;
                    if (driver_no != string.Empty)
                    {
                         msg = "Do." + dono + ".The truck is confirm load Dt. " + date + " " + millshort + " Getpass:" + getpassname
                            + " Shipped to:" + vouchershortnm + " Qntl:" + qntl + " " + grade + " " + truck_no + " Frieght(" + frieght + ") Drviver Mob:"
                            + driver_no + " Sale Bill No: " + salebillno + "";
                    }
                    else
                    {
                         msg = "Do." + dono + ".The truck is confirm load Dt. " + date + " " + millshort + " Getpass:" + getpassname
                           + " Shipped to:" + vouchershortnm + " Qntl:" + qntl + " " + grade + " " + truck_no + " Frieght(" + frieght + ") Transport Mob:"
                           + transportmob + " Sale Bill No: " + salebillno + "";
                    }

                    //var transportsms = "DO." + dono + " The truck is confirm load dt." + date + " " + millshort + " Getpass:" 
                    //    + getpassname + " Shipped To:" + voucherbyshort + " qntl:" + qntl + " " + grade + " " + lorry + ' Freight:' 
                    //    + frtplusvasuli + transordrivermobile + " Sale Bill No:" + SB_No + " " + vtype;


                    dr["dono"] = dono;
                    dr["Party"] = party;
                    dr["msg"] = msg;
                    dr["driver_no"] = driver_no;
                    dr["PartyMobile"] = ds.Tables[0].Rows[i]["PartyMobile"].ToString();
                    dr["mill"] = millshort;
                    dr["frieght"] = ds.Tables[0].Rows[i]["frieght"].ToString();
                    dt.Rows.Add(dr);
                }

                if (dt.Rows.Count > 0)
                {
                    grdDeliverySms.DataSource = dt;
                    grdDeliverySms.DataBind();
                    grdDetail.Visible = false;
                    grdDeliverySms.Visible = true;
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void grdDeliverySms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[0].Width = new Unit("40px");
        e.Row.Cells[1].Width = new Unit("200px");
        e.Row.Cells[1].Style.Add("overflow", "hidden");
        e.Row.Cells[2].Width = new Unit("260px");
        e.Row.Cells[3].Width = new Unit("120px");
        e.Row.Cells[4].Width = new Unit("80px");
        e.Row.Cells[5].Width = new Unit("120px");
        e.Row.Cells[6].Width = new Unit("30px");
        e.Row.Cells[7].Width = new Unit("200px");
        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;

        int i = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                i++;
                string s = cell.Text;
                if (cell.Text.Length > 38)
                {
                    cell.Text = cell.Text.Substring(0, 38) + "....";
                    cell.ToolTip = s;
                }
            }
        }
    }

    protected void btnNewPartyWise_Click(object sender, EventArgs e)
    {
        try
        {
            datefunction();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NPW", "javascript:NPW('" + fromDT + "','" + toDT + "')", true);
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnNewPartywise2_Click(object sender, EventArgs e)
    {
        try
        {
            datefunction();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NPW2", "javascript:NPW2('" + fromDT + "','" + toDT + "')", true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btndo_click(object sender, EventArgs e)
    {
        try
        {
            datefunction();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "onjk", "javascript:onlydo('" + fromDT + "','" + toDT + "')", true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnMillPayment_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text != string.Empty)
            {
                fromDT = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }
            else
            {
                fromDT = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }
            if (txtToDate.Text != string.Empty)
            {
                toDT = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }
            else
            {
                toDT = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mp", "javascript:MP('" + fromDT + "','" + toDT + "')", true);
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnMillPaymentForGST_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text != string.Empty)
            {
                fromDT = DateTime.Parse(txtFromDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }
            else
            {
                fromDT = DateTime.Parse(clsGV.Start_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }
            if (txtToDate.Text != string.Empty)
            {
                toDT = DateTime.Parse(txtToDate.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }
            else
            {
                toDT = DateTime.Parse(clsGV.End_Date, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy-MM-dd");
            }

            string Mill_Code = txtMillCode.Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mpfg", "javascript:MPForGST('" + fromDT + "','" + toDT + "','" + Mill_Code + "')", true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnDispSummaryForGST_Click(object sender, EventArgs e)
    {
        datefunction();
        BranchCode();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "poi", "javascript:DispSummaryForGST('" + fromDT + "','" + toDT + "','" + Branch_Code + "')", true);
    }
}