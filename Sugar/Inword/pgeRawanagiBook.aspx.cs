using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sugar_Inword_pgeRawanagiBook : System.Web.UI.Page
{
    #region Data Section
    string qry = string.Empty;
    static WebControl objAsp = null;
    string tblPrefix = string.Empty;
    string user = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string tblGrid_Details = string.Empty;
    string isAuthenticate = string.Empty;
    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;

    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string Created_By = string.Empty;
    string Modified_By = string.Empty;
    string Created_Date = string.Empty;
    string Modified_Date = string.Empty;
    int Company_Code = 0;
    int flag = 0;
    #endregion
    #region Detail Column
    int Detail_Id = 3;
    int GMill_Code = 4;
    int GMill_Name = 5;
    int GTo_Station = 6;
    int GQty = 7;
    int GDispatch = 8;
    int GBalance = 9;
    int GAppx = 10;
    int GParty_Code = 11;
    int GParty_Name = 12;
    int rowAction = 13;
    int SrNo = 14;
    #endregion

    #region 2nd Detail Column
    int DID = 2;
    int Ref_No = 3;
    int Mill = 4;
    int Mill_Name = 5;
    int Station = 6;
    int Truck_No = 7;
    int Qntl = 8;
    int Frt_Qntl = 9;
    int Net_freiget = 10;
    int Transport_Code = 11;
    int Transport_Name = 12;
    int Vasuli = 13;
    int Remarks = 14;

    int D_rowAction = 15;
    int D_SrNo = 16;
    #endregion
    #region Head
    string Doc_date = string.Empty;
    string Narration = string.Empty;

    StringBuilder Head_Qry = null;
    string Head_Insert = string.Empty;
    string Head_Delete = string.Empty;
    StringBuilder Head_Values = null;
    StringBuilder Head_Fields = null;
    #endregion
    #region Detail
    int Mill_Code = 0;
    string To_Station = string.Empty;
    double Qty = 0.00;
    double Dispatch = 0.00;
    double Balance = 0.00;
    double Appx = 0.00;
    int Party = 0;
    string party_name = string.Empty;
    int mc = 0;

    string Action = string.Empty;
    StringBuilder Detail_Update = null;
    StringBuilder Detail_Delete = null;
    StringBuilder Detail_Fields = null;
    StringBuilder Detail_Values = null;
    #endregion

    #region Grid Detail

    int Grid_Ref_No = 0;
    int Grid_Mill = 0;
    string Grid_Mill_Name = string.Empty;
    string Grid_Station = string.Empty;
    string Grid_Truck_No = string.Empty;
    double Grid_Qntl = 0.00;
    double Grid_Frt_Qntl = 0.00;
    double Grid_Net_freiget = 0.00;
    int Grid_Transport_Code = 0;
    string Grid_Transport_Name = string.Empty;
    double Grid_Vasuli = 0.00;
    string Grid_Remarks = string.Empty;

    StringBuilder Grid_Update = null;
    StringBuilder Grid_Delete = null;
    StringBuilder Grid_Fields = null;
    StringBuilder Grid_Values = null;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            user = Session["user"].ToString();
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "rawangiline_head";
            tblDetails = "rawangiline_detail";
            tblGrid_Details = "rawangi_line";
            pnlPopup.Style["display"] = "none";

            Head_Fields = new StringBuilder();
            Head_Values = new StringBuilder();
            // Head_Update = new StringBuilder();
            Detail_Update = new StringBuilder();
            Detail_Delete = new StringBuilder();
            Detail_Fields = new StringBuilder();
            Detail_Values = new StringBuilder();

            Grid_Update = new StringBuilder();
            Grid_Delete = new StringBuilder();
            Grid_Fields = new StringBuilder();
            Grid_Values = new StringBuilder();
            Maindt = new DataTable();
            dr = null;
            Maindt.Columns.Add("Querys", typeof(string));
            dr = Maindt.NewRow();
            if (!Page.IsPostBack)
            {
                hdnfyearcode.Value = Session["year"].ToString();
                hdnfcompanycode.Value = Session["Company_Code"].ToString();
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    Action = Request.QueryString["Action"];
                    if (Action == "1")
                    {
                        hdnf.Value = Request.QueryString["city_code"];
                        hdnf.Value = Convert.ToDateTime(hdnf.Value).ToString("yyyy/MM/dd");
                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        clsButtonNavigation.enableDisable("N");
                        this.makeEmptyForm("N");

                        ViewState["mode"] = "I";
                        this.showLastRecord();
                        setFocusControl(btnEdit);
                    }
                    else
                    {
                        string docno = string.Empty;
                        clsButtonNavigation.enableDisable("A");
                        ViewState["mode"] = null;
                        ViewState["mode"] = "I";
                        this.makeEmptyForm("A");

                        setFocusControl(txtDOC_DATE);
                    }
                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
            }
            if (objAsp != null)
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
            if (hdnfClosePopup.Value == "Close" || hdnfClosePopup.Value == "")
            {
                pnlPopup.Style["display"] = "none";
            }
            else
            {
                pnlPopup.Style["display"] = "block";
                objAsp = btnSearch;
            }
        }
        catch
        {
        }
    }
    #region Narration
    protected void txtNARRATION_TextChanged(object sender, EventArgs e)
    {

    }
    #endregion
    #region Date
    protected void txtDOC_DATE_TextChanged(object sender, EventArgs e)
    {

    }
    #endregion

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("A");
        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");
        setFocusControl(txtDOC_DATE);

    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        pnlgrdDetail.Enabled = true;
        this.makeEmptyForm("E");

        setFocusControl(txtNARRATION);

    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                Doc_date = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
                string DetailDelete = "delete from rawangiline_detail where Doc_Date='" + Doc_date + "' and Company_Code=" + Session["Company_Code"].ToString() + " ";

                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = DetailDelete;
                Maindt.Rows.Add(dr);

                string Detailrawanagi = "delete from rawangi_line where Doc_Date='" + Doc_date + "' and Company_Code=" + Session["Company_Code"].ToString() + " ";

                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = Detailrawanagi;
                Maindt.Rows.Add(dr);

                string HeadDelete = "delete from rawangiline_head where Doc_Date='" + Doc_date + "' and Company_Code=" + Session["Company_Code"].ToString() + " ";

                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = HeadDelete;
                Maindt.Rows.Add(dr);

                flag = 3;
                string msg = DOPurcSaleCRUD.DOPurcSaleCRUD_OP(flag, Maindt);
                if (msg == "Delete")
                {
                    Response.Redirect("../Inword/PgeRawanagiBook_Utility.aspx");
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Doc_date = clsCommon.getString("select isnull(max(Date_Format(Doc_Date,'%Y/%m/%d')),0) as Date from rawangiline_head ");

        hdnf.Value = Doc_date;
        clsButtonNavigation.enableDisable("S");
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);
    }
    #endregion

    #region [makeEmptyForm]
    private void makeEmptyForm(string dAction)
    {
        try
        {
            if (dAction == "N")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                    if (c is System.Web.UI.WebControls.Label)
                    {
                        ((System.Web.UI.WebControls.Label)c).Text = "";
                    }
                }
                pnlPopup.Style["display"] = "none";
                btnSave.Text = "Save";
                ViewState["currentTable"] = null;
                ViewState["currentTableNew"] = null;

                txtDOC_DATE.Enabled = false;
                txtNARRATION.Enabled = false;
                calenderExtenderDate.Enabled = false;
                txtMILL_CODE.Enabled = false;
                txtStationCity.Enabled = false;
                txtQty.Enabled = false;
                txtDispatch.Enabled = false;
                txtBalance.Enabled = false;
                txtAppxFreight.Enabled = false;
                txtParty_Code.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;

                btntxtMILL_CODE.Enabled = false;
                btntxtParty_Code.Enabled = false;

                txtmill.Enabled = false;
                txtStation.Enabled = false;
                txtTruck_No.Enabled = false;
                txtD_Qty.Enabled = false;
                txtFrt_Qty.Enabled = false;
                txtNet_frt.Enabled = false;
                txtTransport_code.Enabled = false;
                txtTrannsport_Name.Enabled = false;
                btntxtTransport.Enabled = false;
                txtVasuli.Enabled = false;
                txtRemark.Enabled = false;
                btnAdddetail_Grd.Enabled = false;
                btnclosedetail_Grd.Enabled = false;
            }
            if (dAction == "A")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                btnSave.Text = "Save";
                txtDOC_DATE.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ViewState["currentTable"] = null;
                ViewState["currentTableNew"] = null;
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                pnlgrdDetail.Enabled = true;

                grdDetail_Grd.DataSource = null;
                grdDetail_Grd.DataBind();
                pnlgrddetail_Grd.Enabled = true;

                txtDOC_DATE.Enabled = true;
                txtNARRATION.Enabled = true;
                calenderExtenderDate.Enabled = true;
                txtMILL_CODE.Enabled = true;
                txtStationCity.Enabled = true;
                txtQty.Enabled = true;
                txtDispatch.Enabled = true;
                txtBalance.Enabled = true;
                txtAppxFreight.Enabled = true;
                txtParty_Code.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;

                btntxtMILL_CODE.Enabled = true;
                btntxtParty_Code.Enabled = true;

                txtmill.Enabled = true;
                txtStation.Enabled = true;
                txtTruck_No.Enabled = true;
                txtD_Qty.Enabled = true;
                txtFrt_Qty.Enabled = true;
                txtNet_frt.Enabled = true;
                txtTransport_code.Enabled = true;
                txtTrannsport_Name.Enabled = true;
                btntxtTransport.Enabled = true;
                txtVasuli.Enabled = true;
                txtRemark.Enabled = true;

                btnAdddetail_Grd.Enabled = true;
                btnclosedetail_Grd.Enabled = true;
            }
            if (dAction == "S")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                    if (c is System.Web.UI.WebControls.Label)
                    {
                        ((System.Web.UI.WebControls.Label)c).Text = "";
                    }
                }
                pnlPopup.Style["display"] = "none";
                btnSave.Text = "Save";

                txtDOC_DATE.Enabled = false;
                txtNARRATION.Enabled = false;
                calenderExtenderDate.Enabled = false;
                txtMILL_CODE.Enabled = false;
                txtStationCity.Enabled = false;
                txtQty.Enabled = false;
                txtDispatch.Enabled = false;
                txtBalance.Enabled = false;
                txtAppxFreight.Enabled = false;
                txtParty_Code.Enabled = false;
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;

                btntxtMILL_CODE.Enabled = false;
                btntxtParty_Code.Enabled = false;

                txtmill.Enabled = false;
                txtStation.Enabled = false;
                txtTruck_No.Enabled = false;
                txtD_Qty.Enabled = false;
                txtFrt_Qty.Enabled = false;
                txtNet_frt.Enabled = false;
                txtTransport_code.Enabled = false;
                txtTrannsport_Name.Enabled = false;
                btntxtTransport.Enabled = false;
                txtVasuli.Enabled = false;
                txtRemark.Enabled = false;

                btnAdddetail_Grd.Enabled = false;
                btnclosedetail_Grd.Enabled = false;
            }
            if (dAction == "E")
            {
                //foreach (System.Web.UI.Control c in pnlMain.Controls)
                //{
                //    if (c is System.Web.UI.WebControls.TextBox)
                //    {
                //        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                //        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                //    }
                //}

                pnlgrdDetail.Enabled = true;
                pnlgrddetail_Grd.Enabled = true;
                txtDOC_DATE.Enabled = false;
                txtNARRATION.Enabled = true;
                calenderExtenderDate.Enabled = false;
                txtMILL_CODE.Enabled = true;
                txtStationCity.Enabled = true;
                txtQty.Enabled = true;
                txtDispatch.Enabled = true;
                txtBalance.Enabled = true;
                txtAppxFreight.Enabled = true;
                txtParty_Code.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;

                btntxtMILL_CODE.Enabled = true;
                btntxtParty_Code.Enabled = true;

                txtmill.Enabled = true;
                txtStation.Enabled = true;
                txtTruck_No.Enabled = true;
                txtD_Qty.Enabled = true;
                txtFrt_Qty.Enabled = true;
                txtNet_frt.Enabled = true;
                txtTransport_code.Enabled = true;
                txtTrannsport_Name.Enabled = true;
                btntxtTransport.Enabled = true;
                txtVasuli.Enabled = true;
                txtRemark.Enabled = true;

                btnAdddetail_Grd.Enabled = true;
                btnclosedetail_Grd.Enabled = true;
            }
        }
        catch
        {
        }
    }
    #endregion
    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = "select * from rawangiline_head where Doc_date='" + hdnf.Value + "' and Company_Code=" + Session["Company_Code"].ToString() + "";
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion

    #region [fetchrecord]
    private bool fetchRecord(string qry)
    {
        try
        {
            bool recordExist = false;

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
                        string comcode = Session["Company_Code"].ToString();
                        if (hdnfcompanycode.Value != comcode)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same !')", true);
                            return false;
                        }

                       
                        hdnfcompanycode.Value = dt.Rows[0]["Company_Code"].ToString();
                        txtDOC_DATE.Text = dt.Rows[0]["doc_date"].ToString();
                        txtNARRATION.Text = dt.Rows[0]["Remark"].ToString();

                        recordExist = true;

                        #region ---------- Details -------------
                        //                       qry = "select detail_id as ID,credit_ac as AcCode,creditAcName as Name,Unit_Code,Unit_Name,Voucher_No,Voucher_Type,[Tender_No] as TenderNo,[TenderDetail_ID] as DetailID, amount,Adjusted_Amount,narration,narration2,drpFilterValue,Branch_name,isnull(YearCodeDetail,0) as YearCodeDetail,trandetailid" +
                        //" from " + qryDetail + " where doc_no=" + txtdoc_no.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tran_Type='" + trntype + "'";

                        qry = "select Detail_Id as ID,Mill_Code,'' as Mill_Name,To_Station as ToStation,Net_Qntl as Qty,Despatch as Dispatch, " +
                            " Balance,Appx_freiget as Appx_freight,Party_Code,Party_Name from " + tblDetails + " where Doc_Date='" + hdnf.Value + "' and Company_Code=" + Session["Company_Code"] + "";
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                    dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        dt.Rows[i]["rowAction"] = "N";
                                        dt.Rows[i]["SrNo"] = i + 1;
                                    }

                                    grdDetail.DataSource = dt;
                                    grdDetail.DataBind();

                                    ViewState["currentTable"] = dt;
                                }
                                else
                                {
                                    grdDetail.DataSource = null;
                                    grdDetail.DataBind();
                                    ViewState["currentTable"] = null;
                                }
                            }
                            else
                            {
                                grdDetail.DataSource = null;
                                grdDetail.DataBind();
                                ViewState["currentTable"] = null;
                            }
                        }
                        else
                        {
                            grdDetail.DataSource = null;
                            grdDetail.DataBind();
                            ViewState["currentTable"] = null;
                        }
                        #endregion

                        #region ---------- Details -------------
                        //                       qry = "select detail_id as ID,credit_ac as AcCode,creditAcName as Name,Unit_Code,Unit_Name,Voucher_No,Voucher_Type,[Tender_No] as TenderNo,[TenderDetail_ID] as DetailID, amount,Adjusted_Amount,narration,narration2,drpFilterValue,Branch_name,isnull(YearCodeDetail,0) as YearCodeDetail,trandetailid" +
                        //" from " + qryDetail + " where doc_no=" + txtdoc_no.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and Tran_Type='" + trntype + "'";

                        qry = "select Detail_Id as ID,Ref_No,Mill_Code,Mill_Name,To_Station,Truck_No,Qntl,Frt_Qntl,Net_freiget," +
                            " Transport_Code,Transport_Name,Vasuli,Remark from " + tblGrid_Details + " where Doc_Date='" + hdnf.Value + "' and Company_Code=" + Session["Company_Code"] + "";
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                    dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        dt.Rows[i]["rowAction"] = "N";
                                        dt.Rows[i]["SrNo"] = i + 1;
                                    }

                                    grdDetail_Grd.DataSource = dt;
                                    grdDetail_Grd.DataBind();

                                    ViewState["currentTableNew"] = dt;
                                }
                                else
                                {
                                    grdDetail_Grd.DataSource = null;
                                    grdDetail_Grd.DataBind();
                                    ViewState["currentTableNew"] = null;
                                }
                            }
                            else
                            {
                                grdDetail_Grd.DataSource = null;
                                grdDetail_Grd.DataBind();
                                ViewState["currentTableNew"] = null;
                            }
                        }
                        else
                        {
                            grdDetail_Grd.DataSource = null;
                            grdDetail_Grd.DataBind();
                            ViewState["currentTableNew"] = null;
                        }
                        #endregion
                        pnlgrdDetail.Enabled = false;
                        pnlgrddetail_Grd.Enabled = false;
                    }
                }
            }
            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion

    #region txtMill
    protected void txtMILL_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMILL_CODE.Text;
        strTextBox = "txtMILL_CODE";
        csCalculations();
    }
    #endregion
    #region btntxtMill
    protected void btntxtMILL_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtMILL_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region Station City
    protected void txtStationCity_TextChanged(object sender, EventArgs e)
    {

    }
    #endregion
    #region Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Validation
        bool isValidated = true;

        if (hdnfcompanycode.Value != Session["Company_Code"].ToString())
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Selected Records company code & current company code is not same!')", true);
            return;

        }
        if (txtDOC_DATE.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtDOC_DATE);
            return;
        }

        #endregion

        #region Declaration
        Doc_date = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
        Narration = txtNARRATION.Text;

        Created_By = Session["user"].ToString();
        Modified_By = Session["user"].ToString();
        Created_Date = DateTime.Now.ToString("yyyy/MM/dd");
        Modified_Date = DateTime.Now.ToString("yyyy/MM/dd");


        string retValue = string.Empty;
        string strRev = string.Empty;
        Company_Code = Convert.ToInt32(Session["Company_Code"].ToString());
        #endregion

        #region Detail Fields
        Detail_Fields.Append("Doc_Date,");
        Detail_Fields.Append("Detail_Id,");
        Detail_Fields.Append("Mill_Code,");
        Detail_Fields.Append("mc,");
        Detail_Fields.Append("To_Station,");
        Detail_Fields.Append("Net_Qntl,");
        Detail_Fields.Append("Despatch,");
        Detail_Fields.Append("Balance,");
        Detail_Fields.Append("Appx_freiget,");
        Detail_Fields.Append("Party_Code,");
        Detail_Fields.Append("Party_Name,");
        Detail_Fields.Append("Company_Code,");
        Detail_Fields.Append("Created_By");

        #endregion

        #region Grid Fields
        Grid_Fields.Append("Doc_Date,");
        Grid_Fields.Append("Detail_Id,");
        Grid_Fields.Append("Ref_No,");
        Grid_Fields.Append("Mill_Code,");
        Grid_Fields.Append("Mill_Name,");
        Grid_Fields.Append("To_Station,");
        Grid_Fields.Append("Truck_No,");
        Grid_Fields.Append("Qntl,");
        Grid_Fields.Append("Frt_Qntl,");
        Grid_Fields.Append("Net_freiget,");
        Grid_Fields.Append("Transport_Code,");
        Grid_Fields.Append("Transport_Name,");
        Grid_Fields.Append("Vasuli,");
        Grid_Fields.Append("Remark,");
        Grid_Fields.Append("Company_Code");
        #endregion
        if (btnSave.Text == "Save")
        {
            #region Head Fields Assign Values
            Head_Fields.Append("Doc_Date,");
            Head_Values.Append("'" + Doc_date + "',");
            Head_Fields.Append("Remark,");
            Head_Values.Append("'" + Narration + "',");
            Head_Fields.Append("Company_Code");
            Head_Values.Append("'" + Company_Code + "'");
            #endregion
            Head_Insert = "insert into  " + tblHead + " (" + Head_Fields + ") values (" + Head_Values + ")";

            dr = null;
            dr = Maindt.NewRow();
            dr["Querys"] = Head_Insert;
            Maindt.Rows.Add(dr);

            #region 1st Grid Save Part
            #region Main Logic
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                #region Values
                int ID = Convert.ToInt32(grdDetail.Rows[i].Cells[Detail_Id].Text);
                Mill_Code = Convert.ToInt32(grdDetail.Rows[i].Cells[GMill_Code].Text);
                To_Station = Server.HtmlDecode(grdDetail.Rows[i].Cells[GTo_Station].Text);
                try
                {
                    Qty = Convert.ToDouble(grdDetail.Rows[i].Cells[GQty].Text);
                }
                catch
                {
                    Qty = 0.00;
                }
                try
                {
                    Dispatch = Convert.ToDouble(grdDetail.Rows[i].Cells[GDispatch].Text);
                }
                catch
                {
                    Dispatch = 0.00;
                }
                try
                {
                    Balance = Convert.ToDouble(grdDetail.Rows[i].Cells[GBalance].Text);
                }
                catch
                {
                    Balance = 0.00;
                }
                try
                {
                    Appx = Convert.ToDouble(grdDetail.Rows[i].Cells[GAppx].Text);
                }
                catch
                {
                    Appx = 0.00;
                }
                Party = Convert.ToInt32(grdDetail.Rows[i].Cells[GParty_Code].Text);
                party_name = Server.HtmlDecode(grdDetail.Rows[i].Cells[GParty_Name].Text);

                try
                {
                    mc = Convert.ToInt32(clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where Ac_Code=" + Mill_Code + " " +
                        " and Company_Code=" + Company_Code + ""));
                }
                catch
                {
                }

                #endregion
                Detail_Values.Append("('" + Doc_date + "','" + ID + "','" + Mill_Code + "',case when 0='" + mc + "' then null else '" + mc + "' end, " +
                                       " '" + To_Station + "','" + Qty + "','" + Dispatch + "','" + Balance + "','" + Appx + "','" + Party + "','" + party_name + "', " +
                                       " '" + Company_Code + "','" + user + "'),");
            }
            #endregion

            if (Detail_Values.Length > 0)
            {
                Detail_Values.Remove(Detail_Values.Length - 1, 1);
                string Detail_Insert = "insert into " + tblDetails + "(" + Detail_Fields + ") values " + Detail_Values + "";

                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = Detail_Insert;
                Maindt.Rows.Add(dr);
            }
            #endregion

            #region 2nd Grid Save Part
            for (int i = 0; i < grdDetail_Grd.Rows.Count; i++)
            {
                #region Values
                int ID = Convert.ToInt32(grdDetail_Grd.Rows[i].Cells[DID].Text);
                Grid_Ref_No = Convert.ToInt32(grdDetail_Grd.Rows[i].Cells[Ref_No].Text);
                Grid_Mill = Convert.ToInt32(grdDetail_Grd.Rows[i].Cells[Mill].Text);
                Grid_Mill_Name = Server.HtmlDecode(grdDetail_Grd.Rows[i].Cells[Mill_Name].Text);
                Grid_Station = Server.HtmlDecode(grdDetail_Grd.Rows[i].Cells[Station].Text);
                Grid_Truck_No = Server.HtmlDecode(grdDetail_Grd.Rows[i].Cells[Truck_No].Text);
                Grid_Qntl = Convert.ToDouble(grdDetail_Grd.Rows[i].Cells[Qntl].Text);
                Grid_Frt_Qntl = Convert.ToDouble(grdDetail_Grd.Rows[i].Cells[Frt_Qntl].Text);
                Grid_Net_freiget = Convert.ToDouble(grdDetail_Grd.Rows[i].Cells[Net_freiget].Text);
                Grid_Transport_Code = Convert.ToInt32(grdDetail_Grd.Rows[i].Cells[Transport_Code].Text);
                Grid_Transport_Name = Server.HtmlDecode(grdDetail_Grd.Rows[i].Cells[Transport_Name].Text);
                Grid_Vasuli = Convert.ToDouble(grdDetail_Grd.Rows[i].Cells[Vasuli].Text);
                Grid_Remarks = Server.HtmlDecode(grdDetail_Grd.Rows[i].Cells[Remarks].Text);
                #endregion

                Grid_Values.Append("('" + Doc_date + "','" + ID + "','" + Grid_Ref_No + "','" + Grid_Mill + "','" + Grid_Mill_Name + "','" + Grid_Station + "','" + Grid_Truck_No + "'," +
                    " '" + Grid_Qntl + "','" + Grid_Frt_Qntl + "','" + Grid_Net_freiget + "','" + Grid_Transport_Code + "','" + Grid_Transport_Name + "','" + Grid_Vasuli + "','" + Grid_Remarks + "','" + Company_Code + "'),");
            }
            #endregion

            if (Grid_Values.Length > 0)
            {
                Grid_Values.Remove(Grid_Values.Length - 1, 1);
                string Detail_Insert = "insert into " + tblGrid_Details + "(" + Grid_Fields + ") values " + Grid_Values + "";

                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = Detail_Insert;
                Maindt.Rows.Add(dr);
            }
            flag = 1;
        }
        else
        {
            #region Head Update

            string Head_Update = "update " + tblHead + " set Remark='" + Narration + "' where Doc_Date='" + Doc_date + "' and Company_Code=" + Company_Code + "";
            dr = null;
            dr = Maindt.NewRow();
            dr["Querys"] = Head_Update;
            Maindt.Rows.Add(dr);
            string concatid = string.Empty;
            string Grid_concatid = string.Empty;
            #endregion

            #region 1st Grid
            #region Main Logic
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                #region Values
                int ID = Convert.ToInt32(grdDetail.Rows[i].Cells[Detail_Id].Text);
                Mill_Code = Convert.ToInt32(grdDetail.Rows[i].Cells[GMill_Code].Text);
                To_Station = Server.HtmlDecode(grdDetail.Rows[i].Cells[GTo_Station].Text);
                try
                {
                    Qty = Convert.ToDouble(grdDetail.Rows[i].Cells[GQty].Text);
                }
                catch
                {
                    Qty = 0.00;
                }
                try
                {
                    Dispatch = Convert.ToDouble(grdDetail.Rows[i].Cells[GDispatch].Text);
                }
                catch
                {
                    Dispatch = 0.00;
                }
                try
                {
                    Balance = Convert.ToDouble(grdDetail.Rows[i].Cells[GBalance].Text);
                }
                catch
                {
                    Balance = 0.00;
                }
                try
                {
                    Appx = Convert.ToDouble(grdDetail.Rows[i].Cells[GAppx].Text);
                }
                catch
                {
                    Appx = 0.00;
                }
                Party = Convert.ToInt32(grdDetail.Rows[i].Cells[GParty_Code].Text);
                party_name = Server.HtmlDecode(grdDetail.Rows[i].Cells[GParty_Name].Text);

                try
                {
                    mc = Convert.ToInt32(clsCommon.getString("select isnull(accoid,0) as id from qrymstaccountmaster where Ac_Code=" + Mill_Code + " " +
                        " and Company_Code=" + Company_Code + ""));
                }
                catch
                {
                }

                #endregion

                #region Insert
                if (grdDetail.Rows[i].Cells[rowAction].Text == "A")
                {
                    Detail_Values.Append("('" + Doc_date + "','" + ID + "','" + Mill_Code + "',case when 0='" + mc + "' then null else '" + mc + "' end, " +
                                           " '" + To_Station + "','" + Qty + "','" + Dispatch + "','" + Balance + "','" + Appx + "','" + Party + "','" + party_name + "', " +
                                           " '" + Company_Code + "','" + user + "'),");
                }
                #endregion
                #region Update
                else if (grdDetail.Rows[i].Cells[rowAction].Text == "U")
                {
                    Detail_Update.Append("Mill_Code=case Detail_Id when '" + ID + "' then '" + Mill_Code + "'  ELSE Mill_Code END,");
                    Detail_Update.Append("mc=case Detail_Id when '" + ID + "' then case when 0='" + mc + "' then null else '" + mc + "' end  ELSE mc END,");
                    Detail_Update.Append("To_Station=case Detail_Id when '" + ID + "' then '" + To_Station + "'  ELSE To_Station END,");
                    Detail_Update.Append("Net_Qntl=case Detail_Id when '" + ID + "' then '" + Qty + "'  ELSE Net_Qntl END,");
                    Detail_Update.Append("Despatch=case Detail_Id when '" + ID + "' then '" + Dispatch + "'  ELSE Despatch END,");
                    Detail_Update.Append("Balance=case Detail_Id when '" + ID + "' then '" + Balance + "'  ELSE Balance END,");
                    Detail_Update.Append("Appx_freiget=case Detail_Id when '" + ID + "' then '" + Appx + "'  ELSE Appx_freiget END,");
                    Detail_Update.Append("Party_Code=case Detail_Id when '" + ID + "' then '" + Party + "'  ELSE Party_Code END,");
                    Detail_Update.Append("Party_Name=case Detail_Id when '" + ID + "' then '" + party_name + "'  ELSE Party_Name END,");

                    concatid = concatid + "'" + ID + "',";

                }
                #endregion

                #region Delete
                else
                {
                    if (grdDetail.Rows[i].Cells[rowAction].Text == "D")
                    {
                        Detail_Delete.Append("'" + grdDetail.Rows[i].Cells[Detail_Id].Text + "',");
                    }
                }
                #endregion
            }
            #endregion

            if (Detail_Values.Length > 0)
            {
                Detail_Values.Remove(Detail_Values.Length - 1, 1);
                string Detail_Insert = "insert into " + tblDetails + "(" + Detail_Fields + ") values " + Detail_Values + "";

                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = Detail_Insert;
                Maindt.Rows.Add(dr);

            }
            if (Detail_Delete.Length > 0)
            {
                Detail_Delete.Remove(Detail_Delete.Length - 1, 1);
                string Detail_Deleteqry = "delete from " + tblDetails + " where Doc_Date='" + Doc_date + "' and Detail_Id in(" + Detail_Delete + ")";

                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = Detail_Deleteqry;
                Maindt.Rows.Add(dr);
            }
            if (Detail_Update.Length > 0)
            {
                concatid = concatid.Remove(concatid.Length - 1);
                Detail_Update.Remove(Detail_Update.Length - 1, 1);
                string Detail_Updateqry = "update " + tblDetails + " set " + Detail_Update + " where Doc_Date='" + Doc_date + "' and Detail_Id in(" + concatid + ")";

                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = Detail_Updateqry;
                Maindt.Rows.Add(dr);
            }
            #endregion

            #region 2nd Grid Save Part
            for (int i = 0; i < grdDetail_Grd.Rows.Count; i++)
            {
                #region Values
                int ID = Convert.ToInt32(grdDetail_Grd.Rows[i].Cells[DID].Text);
                Grid_Ref_No = Convert.ToInt32(grdDetail_Grd.Rows[i].Cells[Ref_No].Text);
                Grid_Mill = Convert.ToInt32(grdDetail_Grd.Rows[i].Cells[Mill].Text);
                Grid_Mill_Name = Server.HtmlDecode(grdDetail_Grd.Rows[i].Cells[Mill_Name].Text);
                Grid_Station = Server.HtmlDecode(grdDetail_Grd.Rows[i].Cells[Station].Text);
                Grid_Truck_No = Server.HtmlDecode(grdDetail_Grd.Rows[i].Cells[Truck_No].Text);
                Grid_Qntl = Convert.ToDouble(grdDetail_Grd.Rows[i].Cells[Qntl].Text);
                Grid_Frt_Qntl = Convert.ToDouble(grdDetail_Grd.Rows[i].Cells[Frt_Qntl].Text);
                Grid_Net_freiget = Convert.ToDouble(grdDetail_Grd.Rows[i].Cells[Net_freiget].Text);
                Grid_Transport_Code = Convert.ToInt32(grdDetail_Grd.Rows[i].Cells[Transport_Code].Text);
                Grid_Transport_Name = Server.HtmlDecode(grdDetail_Grd.Rows[i].Cells[Transport_Name].Text);
                Grid_Vasuli = Convert.ToDouble(grdDetail_Grd.Rows[i].Cells[Vasuli].Text);
                Grid_Remarks = Server.HtmlDecode(grdDetail_Grd.Rows[i].Cells[Remarks].Text);
                #endregion

                #region Insert
                if (grdDetail_Grd.Rows[i].Cells[D_rowAction].Text == "A")
                {
                    Grid_Values.Append("('" + Doc_date + "','" + ID + "','" + Grid_Ref_No + "','" + Grid_Mill + "','" + Grid_Mill_Name + "','" + Grid_Station + "','" + Grid_Truck_No + "'," +
                        " '" + Grid_Qntl + "','" + Grid_Frt_Qntl + "','" + Grid_Net_freiget + "','" + Grid_Transport_Code + "','" + Grid_Transport_Name + "','" + Grid_Vasuli + "','" + Grid_Remarks + "','" + Company_Code + "'),");

                }
                #endregion
                #region Update
                else if (grdDetail_Grd.Rows[i].Cells[D_rowAction].Text == "U")
                {
                    Grid_Update.Append("Ref_No=case Detail_Id when '" + ID + "' then '" + Grid_Ref_No + "'  ELSE Ref_No END,");
                    Grid_Update.Append("Mill_Code=case Detail_Id when '" + ID + "' then '" + Grid_Mill + "'  ELSE Mill_Code END,");
                    Grid_Update.Append("Mill_Name=case Detail_Id when '" + ID + "' then '" + Grid_Mill_Name + "'  ELSE Mill_Name END,");
                    Grid_Update.Append("To_Station=case Detail_Id when '" + ID + "' then '" + Grid_Station + "'  ELSE To_Station END,");
                    Grid_Update.Append("Truck_No=case Detail_Id when '" + ID + "' then '" + Grid_Truck_No + "'  ELSE Truck_No END,");
                    Grid_Update.Append("Qntl=case Detail_Id when '" + ID + "' then '" + Grid_Qntl + "'  ELSE Qntl END,");
                    Grid_Update.Append("Frt_Qntl=case Detail_Id when '" + ID + "' then '" + Grid_Frt_Qntl + "'  ELSE Frt_Qntl END,");
                    Grid_Update.Append("Net_freiget=case Detail_Id when '" + ID + "' then '" + Grid_Net_freiget + "'  ELSE Net_freiget END,");
                    Grid_Update.Append("Transport_Code=case Detail_Id when '" + ID + "' then '" + Grid_Transport_Code + "'  ELSE Transport_Code END,");
                    Grid_Update.Append("Transport_Name=case Detail_Id when '" + ID + "' then '" + Grid_Transport_Name + "'  ELSE Transport_Name END,");
                    Grid_Update.Append("Vasuli=case Detail_Id when '" + ID + "' then '" + Grid_Vasuli + "'  ELSE Vasuli END,");
                    Grid_Update.Append("Remark=case Detail_Id when '" + ID + "' then '" + Grid_Remarks + "'  ELSE Remark END,");

                    Grid_concatid = Grid_concatid + "'" + ID + "',";

                }
                #endregion

                #region Delete
                else
                {
                    if (grdDetail_Grd.Rows[i].Cells[D_rowAction].Text == "D")
                    {
                        Grid_Delete.Append("'" + grdDetail.Rows[i].Cells[DID].Text + "',");
                    }
                }
                #endregion
            }
            #endregion

            if (Grid_Values.Length > 0)
            {
                Grid_Values.Remove(Grid_Values.Length - 1, 1);
                string Detail_Insert = "insert into " + tblGrid_Details + "(" + Grid_Fields + ") values " + Grid_Values + "";

                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = Detail_Insert;
                Maindt.Rows.Add(dr);
            }
            if (Grid_Delete.Length > 0)
            {
                Grid_Delete.Remove(Grid_Delete.Length - 1, 1);
                string Detail_Deleteqry = "delete from " + tblGrid_Details + " where Doc_Date='" + Doc_date + "' and Detail_Id in(" + Grid_Delete + ")";

                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = Detail_Deleteqry;
                Maindt.Rows.Add(dr);
            }
            if (Grid_Update.Length > 0)
            {
                Grid_concatid = Grid_concatid.Remove(Grid_concatid.Length - 1);
                Grid_Update.Remove(Grid_Update.Length - 1, 1);
                string Detail_Updateqry = "update " + tblGrid_Details + " set " + Grid_Update + " where Doc_Date='" + Doc_date + "' and Detail_Id in(" + Grid_concatid + ")";

                dr = null;
                dr = Maindt.NewRow();
                dr["Querys"] = Detail_Updateqry;
                Maindt.Rows.Add(dr);
            }
            flag = 2;
        }

        string msg = DOPurcSaleCRUD.DOPurcSaleCRUD_OP(flag, Maindt);
        if (msg == "Insert")
        {
            hdnf.Value = Doc_date;
            clsButtonNavigation.enableDisable("S");
            this.makeEmptyForm("S");
            qry = getDisplayQuery();
            this.fetchRecord(qry);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Added !')", true);
        }
        else if (msg == "Update")
        {
            hdnf.Value = Doc_date;
            clsButtonNavigation.enableDisable("S");
            this.makeEmptyForm("S");
            qry = getDisplayQuery();
            this.fetchRecord(qry);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Update !')", true);
        }
    }
    #endregion

    #region Qty
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        txtBalance.Text = txtQty.Text;
        setFocusControl(txtDispatch);
    }
    #endregion
    #region Dispatch
    protected void txtDispatch_TextChanged(object sender, EventArgs e)
    {

    }
    #endregion
    #region Balance
    protected void txtBalance_TextChanged(object sender, EventArgs e)
    {

    }
    #endregion
    #region Freight
    protected void txtAppxFreight_TextChanged(object sender, EventArgs e)
    {

    }
    #endregion
    #region party
    protected void txtParty_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtParty_Code.Text;
        strTextBox = "txtParty_Code";
        csCalculations();
    }
    #endregion
    #region btnParty
    protected void btntxtParty_Code_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtParty_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region btnAddDetail
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        try
        {
            bool isValidated = true;
            if (txtMILL_CODE.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtMILL_CODE);
                return;
            }

            if (txtStationCity.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtStationCity);
                return;
            }
            if (txtQty.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtQty);
                return;
            }
            if (txtDispatch.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                txtDispatch.Text = "0.00";
            }

            if (txtBalance.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                txtBalance.Text = "0.00";
            }

            if (txtAppxFreight.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                txtAppxFreight.Text = "0.00";
            }

            if (txtParty_Code.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtParty_Code);
                return;
            }
            int rowIndex = 1;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();

            Doc_date = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            if (ViewState["currentTable"] != null)
            {
                dt = (DataTable)ViewState["currentTable"];
                if (dt.Rows[0]["ID"].ToString().Trim() != "")
                {
                    if (btnAdddetails.Text == "ADD")
                    {
                        dr = dt.NewRow();
                        #region calculate rowindex
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                        }
                        if (index.Length > 0)
                        {
                            for (int i = 0; i < index.Length; i++)
                            {
                                if (index[i] > maxIndex)
                                {
                                    maxIndex = index[i];
                                }
                            }
                            rowIndex = maxIndex + 1;
                        }
                        else
                        {
                            rowIndex = maxIndex;          //1
                        }
                        #endregion
                        //     rowIndex = dt.Rows.Count + 1;
                        dr["ID"] = rowIndex;     //auto
                        dr["rowAction"] = "A";
                        dr["SrNo"] = 0;
                    }
                    else
                    {
                        int n = Convert.ToInt32(lblNo.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["ID"] = rowIndex;
                        dr["SrNo"] = 0;
                        #region decide whether actual row is updating or virtual [rowAction]
                        string id = clsCommon.getString("select Detail_Id from rawangiline_detail where  Detail_ID=" + lblID.Text + " And Company_Code = " + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Doc_Date='" + Doc_date + "'");
                        if (id != "0")
                        {
                            dr["rowAction"] = "U";   //actual row
                        }
                        else
                        {
                            dr["rowAction"] = "A";    //virtual row
                        }
                        #endregion
                    }
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add((new DataColumn("ID", typeof(Int32))));

                    #region [Write here columns]
                    dt.Columns.Add((new DataColumn("Mill_Code", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("Mill_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("ToStation", typeof(string))));
                    dt.Columns.Add((new DataColumn("Qty", typeof(double))));
                    dt.Columns.Add((new DataColumn("Dispatch", typeof(double))));
                    dt.Columns.Add((new DataColumn("Balance", typeof(double))));
                    dt.Columns.Add((new DataColumn("Appx_freight", typeof(double))));
                    dt.Columns.Add((new DataColumn("Party_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Party_Name", typeof(string))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dr = dt.NewRow();
                    dr["ID"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("ID", typeof(Int32))));

                #region [Write here columns]
                dt.Columns.Add((new DataColumn("Mill_Code", typeof(Int32))));
                dt.Columns.Add((new DataColumn("Mill_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("ToStation", typeof(string))));
                dt.Columns.Add((new DataColumn("Qty", typeof(double))));
                dt.Columns.Add((new DataColumn("Dispatch", typeof(double))));
                dt.Columns.Add((new DataColumn("Balance", typeof(double))));
                dt.Columns.Add((new DataColumn("Appx_freight", typeof(double))));
                dt.Columns.Add((new DataColumn("Party_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Party_Name", typeof(string))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dr = dt.NewRow();
                dr["ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            dr["Mill_Code"] = txtMILL_CODE.Text;
            dr["Mill_Name"] = LBLMILL_NAME.Text;
            dr["ToStation"] = txtStationCity.Text;
            dr["Qty"] = txtQty.Text;
            dr["Dispatch"] = txtDispatch.Text;
            dr["Balance"] = txtBalance.Text;
            dr["Appx_freight"] = txtAppxFreight.Text;
            dr["Party_Code"] = txtParty_Code.Text;
            dr["Party_Name"] = lblParty_Name.Text;
            #endregion

            if (btnAdddetails.Text == "ADD")
            {
                dt.Rows.Add(dr);
            }
            #region set sr no
            DataRow drr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    drr = (DataRow)dt.Rows[i];
                    drr["SrNo"] = i + 1;
                }
            }
            #endregion
            grdDetail.DataSource = dt;
            grdDetail.DataBind();
            ViewState["currentTable"] = dt;

            txtMILL_CODE.Text = string.Empty;
            LBLMILL_NAME.Text = string.Empty;
            txtStationCity.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtDispatch.Text = string.Empty;
            txtBalance.Text = string.Empty;
            txtAppxFreight.Text = string.Empty;
            txtParty_Code.Text = string.Empty;
            lblParty_Name.Text = string.Empty;

            btnAdddetails.Text = "ADD";
            setFocusControl(txtMILL_CODE);
        }
        catch
        {
        }
    }
    #endregion
    #region Closedetail
    protected void btnClosedetails_Click(object sender, EventArgs e)
    {
        txtMILL_CODE.Text = string.Empty;
        LBLMILL_NAME.Text = string.Empty;
        txtStationCity.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtDispatch.Text = string.Empty;
        txtBalance.Text = string.Empty;
        txtAppxFreight.Text = string.Empty;
        txtParty_Code.Text = string.Empty;
        lblParty_Name.Text = string.Empty;

        btnAdddetails.Text = "ADD";
    }
    #endregion

    #region grd RowCommand
    protected void grdDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int rowindex = row.RowIndex;
            if (e.CommandArgument == "lnk")
            {
                switch (e.CommandName)
                {
                    case "EditRecord":
                        if (grdDetail.Rows[rowindex].Cells[rowAction].Text != "D" && grdDetail.Rows[rowindex].Cells[rowAction].Text != "R")
                        {
                            // pnlPopupDetails.Style["display"] = "block";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "Update";
                        }
                        break;

                    case "ShowRecord":
                        if (grdDetail.Rows[rowindex].Cells[rowAction].Text != "D" && grdDetail.Rows[rowindex].Cells[rowAction].Text != "R")
                        {
                            // pnlPopupDetails.Style["display"] = "block";
                            this.PutRecordInDetail(grdDetail.Rows[rowindex]);

                        }
                        break;
                    case "DeleteRecord":
                        string action = "";
                        LinkButton lnkDelete = (LinkButton)e.CommandSource;
                        if (lnkDelete.Text == "Delete")
                        {
                            action = "Delete";
                            lnkDelete.Text = "Open";
                        }
                        else
                        {
                            action = "Open";
                            lnkDelete.Text = "Delete";
                        }
                        this.DeleteDetailsRow(grdDetail.Rows[rowindex], action);

                        break;
                }
            }
        }
        catch
        {
        }
    }
    #endregion
    #region grd DataBound
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].ControlStyle.Width = new Unit("50px");
        e.Row.Cells[1].ControlStyle.Width = new Unit("50px");
        e.Row.Cells[2].ControlStyle.Width = new Unit("50px");
        e.Row.Cells[Detail_Id].ControlStyle.Width = new Unit("40px");
        e.Row.Cells[GMill_Code].ControlStyle.Width = new Unit("70px");
        e.Row.Cells[GMill_Name].ControlStyle.Width = new Unit("250px");
        e.Row.Cells[GTo_Station].ControlStyle.Width = new Unit("100px");
        e.Row.Cells[GQty].ControlStyle.Width = new Unit("60px");
        e.Row.Cells[GDispatch].ControlStyle.Width = new Unit("60px");
        e.Row.Cells[GBalance].ControlStyle.Width = new Unit("60px");
        e.Row.Cells[GAppx].ControlStyle.Width = new Unit("90px");
        e.Row.Cells[GParty_Code].ControlStyle.Width = new Unit("80px");
        e.Row.Cells[GParty_Name].ControlStyle.Width = new Unit("100px");

        e.Row.Cells[rowAction].ControlStyle.Width = new Unit("60px");
        e.Row.Cells[SrNo].ControlStyle.Width = new Unit("60px");
        e.Row.Cells[GMill_Name].Style["overflow"] = "hidden";
        e.Row.Cells[GParty_Name].Style["overflow"] = "hidden";
    }
    #endregion
    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[SrNo].Text);
        lblID.Text = Server.HtmlDecode(gvrow.Cells[Detail_Id].Text);

        txtMILL_CODE.Text = Server.HtmlDecode(gvrow.Cells[GMill_Code].Text);
        LBLMILL_NAME.Text = Server.HtmlDecode(gvrow.Cells[GMill_Name].Text);
        txtStationCity.Text = Server.HtmlDecode(gvrow.Cells[GTo_Station].Text);
        txtQty.Text = Server.HtmlDecode(gvrow.Cells[GQty].Text);
        txtDispatch.Text = Server.HtmlDecode(gvrow.Cells[GDispatch].Text);
        txtBalance.Text = Server.HtmlDecode(gvrow.Cells[GBalance].Text);
        txtAppxFreight.Text = Server.HtmlDecode(gvrow.Cells[GAppx].Text);
        txtParty_Code.Text = Server.HtmlDecode(gvrow.Cells[GParty_Code].Text);
        lblParty_Name.Text = Server.HtmlDecode(gvrow.Cells[GParty_Name].Text);
    }
    #endregion

    #region [showDetailsRow Grd]
    private void showDetailsRow_Grd(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[D_SrNo].Text);
        lblID.Text = Server.HtmlDecode(gvrow.Cells[DID].Text);

        txtmill.Text = Server.HtmlDecode(gvrow.Cells[Mill].Text);
        lblmill.Text = Server.HtmlDecode(gvrow.Cells[Mill_Name].Text);
        txtStation.Text = Server.HtmlDecode(gvrow.Cells[Station].Text);
        txtD_Qty.Text = Server.HtmlDecode(gvrow.Cells[Qntl].Text);
        txtFrt_Qty.Text = Server.HtmlDecode(gvrow.Cells[Frt_Qntl].Text);
        txtNet_frt.Text = Server.HtmlDecode(gvrow.Cells[Net_freiget].Text);
        txtTransport_code.Text = Server.HtmlDecode(gvrow.Cells[Transport_Code].Text);
        txtTrannsport_Name.Text = Server.HtmlDecode(gvrow.Cells[Transport_Name].Text);
        txtVasuli.Text = Server.HtmlDecode(gvrow.Cells[Vasuli].Text);
        txtRemark.Text = Server.HtmlDecode(gvrow.Cells[Remarks].Text);
        txtTruck_No.Text = Server.HtmlDecode(gvrow.Cells[Truck_No].Text);
    }
    #endregion

    #region [PutOn Detail]
    private void PutRecordInDetail(GridViewRow gvrow)
    {
        // lblNo.Text = Server.HtmlDecode(gvrow.Cells[SrNo].Text);
        // lblID.Text = Server.HtmlDecode(gvrow.Cells[Detail_Id].Text);
        hdnfRef_No.Value = Server.HtmlDecode(gvrow.Cells[Detail_Id].Text);
        txtmill.Text = Server.HtmlDecode(gvrow.Cells[GMill_Code].Text);
        lblmill.Text = Server.HtmlDecode(gvrow.Cells[GMill_Name].Text);
        txtD_Qty.Text = Server.HtmlDecode(gvrow.Cells[GQty].Text);
        txtStation.Text = Server.HtmlDecode(gvrow.Cells[GTo_Station].Text);

        setFocusControl(txtmill);
    }
    #endregion

    #region [DeleteDetailsRow]
    private void DeleteDetailsRow(GridViewRow gridViewRow, string action)
    {
        try
        {
            Doc_date = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            int rowIndex = gridViewRow.RowIndex;
            if (ViewState["currentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["currentTable"];
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["ID"].ToString());
                string IDExisting = clsCommon.getString("select detail_id from rawangiline_detail where detail_id=" + ID + " and Doc_Date='" + Doc_date + "' and Company_Code=" + Session["Company_Code"].ToString() + "");

                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[rowAction].Text = "D";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[rowAction].Text = "N";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "N";
                    }
                }
                else
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[rowAction].Text = "R";       //R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[rowAction].Text = "A";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "A";
                    }
                }
                ViewState["currentTable"] = dt;
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [DeleteDetailsRow Grd]
    private void DeleteDetailsRow_Grd(GridViewRow gridViewRow, string action)
    {
        try
        {
            Doc_date = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            int rowIndex = gridViewRow.RowIndex;
            if (ViewState["currentTableNew"] != null)
            {
                DataTable dt = (DataTable)ViewState["currentTableNew"];
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["ID"].ToString());
                string IDExisting = clsCommon.getString("select detail_id from rawangi_line where detail_id=" + ID + " and Doc_Date='" + Doc_date + "' and Company_Code=" + Session["Company_Code"].ToString() + "");
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail_Grd.Rows[rowIndex].Cells[D_rowAction].Text = "D";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail_Grd.Rows[rowIndex].Cells[D_rowAction].Text = "N";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "N";
                    }
                }
                else
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail_Grd.Rows[rowIndex].Cells[D_rowAction].Text = "R";       //R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail_Grd.Rows[rowIndex].Cells[D_rowAction].Text = "A";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "A";
                    }
                }
                ViewState["currentTableNew"] = dt;
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [showLastRecord]
    private void showLastRecord()
    {
        try
        {
            string qry = string.Empty;
            qry = getDisplayQuery();
            bool recordExist = this.fetchRecord(qry);
            if (recordExist == true)
            {
                btnAdd.Focus();
            }
            else                     //new code
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }
        catch
        {
        }
    }
    #endregion
    #region Search
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
                setFocusControl(btnSearch);
            }
        }
        catch
        {
        }
    }
    #endregion
    #region Search
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            string searchtxt = searchString;
            string delimStr = "";
            char[] delimiter = delimStr.ToCharArray();
            string words = searchString;
            string[] split = null;
            string name = string.Empty;

            if (searchString != string.Empty)
            {
                txtSearchText.Text = searchString;
                searchtxt = txtSearchText.Text;
                words = txtSearchText.Text;
                split = words.Split(delimiter);
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
                searchtxt = txtSearchText.Text;
                words = txtSearchText.Text;
                split = words.Split(delimiter);
            }

            if (hdnfClosePopup.Value == "txtMILL_CODE")
            {
                lblPopupHead.Text = "--Select Party Code--";
                if (txtMILL_CODE.Text != string.Empty)
                {
                    split = txtMILL_CODE.Text.Split(delimiter);
                }
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);

                //string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " " +
                //    " and Locked=0 and " + name + " and Ac_type='M' order by Ac_Name_E";
                //this.showPopup(qry);

                string qry = " Locked=0 and Ac_type='M' and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                   + " and (" + name + ") order by Ac_Name_E ";

                this.showPopupAccountMaster(qry);
            }

            if (hdnfClosePopup.Value == "txtTransport_code")
            {
                lblPopupHead.Text = "--Select Party Code--";
                if (txtTransport_code.Text != string.Empty)
                {
                    split = txtTransport_code.Text.Split(delimiter);
                }
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);

                //string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " " +
                //    " and Locked=0 and " + name + " and Ac_type='T' order by Ac_Name_E";
                //this.showPopup(qry);
                string qry = " Locked=0 and Ac_type='T' and  dbo.nt_1_accountmaster.Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                   + " and (" + name + ") order by Ac_Name_E ";

                this.showPopupAccountMaster(qry);
            }

            if (hdnfClosePopup.Value == "txtParty_Code")
            {
                lblPopupHead.Text = "--Select Party Code--";
                if (txtParty_Code.Text != string.Empty)
                {
                    split = txtParty_Code.Text.Split(delimiter);
                }
                foreach (var s in split)
                {
                    string aa = s.ToString();
                    //name += " Branch_Code Like '%" + aa + "%'or Branch_Name_E Like '%" + aa + "%'or Branch_Address Like '%" + aa + "%'or";
                    name += "( Ac_Code like '%" + aa + "%' or Ac_Name_E like '%" + aa + "%' or Address_E like '%" + aa + "%' ) and";
                }
                name = name.Remove(name.Length - 3);

                string qry = "select Ac_Code,Ac_Name_E,CityName from qrymstaccountmaster where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " " +
                    " and Locked=0 and " + name + "  order by Ac_Name_E";
                this.showPopup(qry);
            }
        }
        catch { }
    }
    #endregion
    protected void imgBtnClose_Click(object sender, ImageClickEventArgs e)
    {
        hdnfClosePopup.Value = "Close";
        pnlPopup.Style["display"] = "none";
        txtSearchText.Text = string.Empty;
        grdPopup.DataSource = null;
        grdPopup.DataBind();
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
            if (v == "txtMILL_CODE" || v == "txtParty_Code")
            {
                e.Row.Cells[0].Width = new Unit("60px");
                e.Row.Cells[1].Width = new Unit("300px");
                e.Row.Cells[2].Width = new Unit("100px");

            }
        }
    }
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }

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
                        pnlPopup.Style["display"] = "block";
                        txtSearchText.Text = string.Empty;
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = "0";
                        pnlPopup.Style["display"] = "block";
                    }
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [Account Master Popup Button Code]
    protected void showPopupAccountMaster(string qry)
    {
        try
        {
            setFocusControl(txtSearchText);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.myaccountmaster(qry);

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
                        pnlPopup.Style["display"] = "block";
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = "0";
                        pnlPopup.Style["display"] = "block";
                    }
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtParty_Code")
            {
                string acname = "";
                if (txtParty_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtParty_Code.Text);
                    if (a == false)
                    {
                        btntxtParty_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtParty_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {

                            lblParty_Name.Text = acname;

                            setFocusControl(btnAdddetails);
                        }
                        else
                        {
                            txtParty_Code.Text = string.Empty;
                            lblParty_Name.Text = acname;
                            setFocusControl(txtParty_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtParty_Code);
                }
            }

            if (strTextBox == "txtMILL_CODE")
            {
                string acname = "";
                if (txtMILL_CODE.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtMILL_CODE.Text);
                    if (a == false)
                    {
                        btntxtMILL_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtMILL_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {

                            LBLMILL_NAME.Text = acname;

                            setFocusControl(txtStationCity);
                        }
                        else
                        {
                            txtMILL_CODE.Text = string.Empty;
                            LBLMILL_NAME.Text = acname;
                            setFocusControl(txtMILL_CODE);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtMILL_CODE);
                }
            }

            if (strTextBox == "txtTransport_code")
            {
                string acname = "";
                if (txtTransport_code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtTransport_code.Text);
                    if (a == false)
                    {
                        btntxtTransport_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from qrymstaccountmaster where Ac_Code=" + txtTransport_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {

                            txtTrannsport_Name.Text = acname;

                            setFocusControl(txtTrannsport_Name);
                        }
                        else
                        {
                            txtTransport_code.Text = string.Empty;
                            txtTrannsport_Name.Text = acname;
                            setFocusControl(txtTransport_code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtTransport_code);
                }
            }
        }
        catch
        {
        }
    }
    #endregion
    protected void txtmill_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtD_Qty_TextChanged(object sender, EventArgs e)
    {
        Grid_Qntl = Convert.ToDouble(txtD_Qty.Text != string.Empty ? txtD_Qty.Text : "0.00");
        Grid_Frt_Qntl = Convert.ToDouble(txtFrt_Qty.Text != string.Empty ? txtFrt_Qty.Text : "0.00");

        double net = Grid_Qntl * Grid_Frt_Qntl;
        txtNet_frt.Text = net.ToString();

        setFocusControl(txtFrt_Qty);
    }
    protected void txtFrt_Qty_TextChanged(object sender, EventArgs e)
    {
        Grid_Qntl = Convert.ToDouble(txtD_Qty.Text != string.Empty ? txtD_Qty.Text : "0.00");
        Grid_Frt_Qntl = Convert.ToDouble(txtFrt_Qty.Text != string.Empty ? txtFrt_Qty.Text : "0.00");

        double net = Grid_Qntl * Grid_Frt_Qntl;
        txtNet_frt.Text = net.ToString();

        setFocusControl(txtNet_frt);
    }
    protected void txtNet_frt_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtTransport_code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTransport_code.Text;
        strTextBox = "txtTransport_code";
        csCalculations();
    }
    protected void btntxtTransport_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearchText.Text = string.Empty;
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtTransport_code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void txtVasuli_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnAdddetail_Grd_Click(object sender, EventArgs e)
    {
        try
        {
            bool isValidated = true;
            if (txtmill.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtmill);
                return;
            }

            if (txtStation.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtStation);
                return;
            }
            if (txtD_Qty.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtD_Qty);
                return;
            }
            if (txtFrt_Qty.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtFrt_Qty);
                return;
            }

            if (txtNet_frt.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtNet_frt);
                return;
            }



            if (txtTransport_code.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtTransport_code);
                return;
            }

            if (txtTruck_No.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtTruck_No);
                return;
            }

            if (txtVasuli.Text != string.Empty)
            {
                isValidated = true;
            }
            else
            {
                txtVasuli.Text = "0.00";
            }
            int rowIndex = 1;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();

            Doc_date = DateTime.Parse(txtDOC_DATE.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd");
            if (ViewState["currentTableNew"] != null)
            {
                dt = (DataTable)ViewState["currentTableNew"];
                if (dt.Rows[0]["ID"].ToString().Trim() != "")
                {
                    if (btnAdddetail_Grd.Text == "ADD")
                    {
                        dr = dt.NewRow();
                        #region calculate rowindex
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                        }
                        if (index.Length > 0)
                        {
                            for (int i = 0; i < index.Length; i++)
                            {
                                if (index[i] > maxIndex)
                                {
                                    maxIndex = index[i];
                                }
                            }
                            rowIndex = maxIndex + 1;
                        }
                        else
                        {
                            rowIndex = maxIndex;          //1
                        }
                        #endregion
                        //     rowIndex = dt.Rows.Count + 1;
                        dr["ID"] = rowIndex;     //auto
                        dr["rowAction"] = "A";
                        dr["SrNo"] = 0;
                    }
                    else
                    {
                        int n = Convert.ToInt32(lblNo.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["ID"] = rowIndex;
                        dr["SrNo"] = 0;
                        #region decide whether actual row is updating or virtual [rowAction]
                        string id = clsCommon.getString("select Detail_Id from rawangi_line where  Detail_ID=" + lblID.Text + " And Company_Code = " + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Doc_Date='" + Doc_date + "'");
                        if (id != "0")
                        {
                            dr["rowAction"] = "U";   //actual row
                        }
                        else
                        {
                            dr["rowAction"] = "A";    //virtual row
                        }
                        #endregion
                    }
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add((new DataColumn("ID", typeof(Int32))));

                    #region [Write here columns]
                    dt.Columns.Add((new DataColumn("Ref_No", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("Mill_Code", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("Mill_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("To_Station", typeof(string))));
                    dt.Columns.Add((new DataColumn("Truck_No", typeof(string))));
                    dt.Columns.Add((new DataColumn("Qntl", typeof(double))));
                    dt.Columns.Add((new DataColumn("Frt_Qntl", typeof(double))));
                    dt.Columns.Add((new DataColumn("Net_freiget", typeof(double))));
                    dt.Columns.Add((new DataColumn("Transport_Code", typeof(Int32))));
                    dt.Columns.Add((new DataColumn("Transport_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Vasuli", typeof(double))));
                    dt.Columns.Add((new DataColumn("Remark", typeof(string))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dr = dt.NewRow();
                    dr["ID"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("ID", typeof(Int32))));

                #region [Write here columns]
                dt.Columns.Add((new DataColumn("Ref_No", typeof(Int32))));
                dt.Columns.Add((new DataColumn("Mill_Code", typeof(Int32))));
                dt.Columns.Add((new DataColumn("Mill_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("To_Station", typeof(string))));
                dt.Columns.Add((new DataColumn("Truck_No", typeof(string))));
                dt.Columns.Add((new DataColumn("Qntl", typeof(double))));
                dt.Columns.Add((new DataColumn("Frt_Qntl", typeof(double))));
                dt.Columns.Add((new DataColumn("Net_freiget", typeof(double))));
                dt.Columns.Add((new DataColumn("Transport_Code", typeof(Int32))));
                dt.Columns.Add((new DataColumn("Transport_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Vasuli", typeof(double))));
                dt.Columns.Add((new DataColumn("Remark", typeof(string))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dr = dt.NewRow();
                dr["ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            dr["Ref_No"] = hdnfRef_No.Value;
            dr["Mill_Code"] = txtmill.Text;
            dr["Mill_Name"] = lblmill.Text;
            dr["To_Station"] = txtStation.Text;
            dr["Truck_No"] = txtTruck_No.Text;
            dr["Qntl"] = txtD_Qty.Text;
            dr["Frt_Qntl"] = txtFrt_Qty.Text;
            dr["Net_freiget"] = txtNet_frt.Text;
            dr["Transport_Code"] = txtTransport_code.Text;
            dr["Transport_Name"] = txtTrannsport_Name.Text;
            dr["Vasuli"] = txtVasuli.Text;
            dr["Remark"] = txtRemark.Text;
            #endregion

            if (btnAdddetail_Grd.Text == "ADD")
            {
                dt.Rows.Add(dr);
            }
            #region set sr no
            DataRow drr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    drr = (DataRow)dt.Rows[i];
                    drr["SrNo"] = i + 1;
                }
            }
            #endregion
            grdDetail_Grd.DataSource = dt;
            grdDetail_Grd.DataBind();
            ViewState["currentTableNew"] = dt;

            txtmill.Text = string.Empty;
            lblmill.Text = string.Empty;
            txtStation.Text = string.Empty;
            txtD_Qty.Text = string.Empty;
            txtFrt_Qty.Text = string.Empty;
            txtNet_frt.Text = string.Empty;
            txtTransport_code.Text = string.Empty;
            txtTrannsport_Name.Text = string.Empty;
            txtVasuli.Text = string.Empty;
            txtRemark.Text = string.Empty;
            txtTruck_No.Text = string.Empty;
            btnAdddetail_Grd.Text = "ADD";
            this.CalculateDiffBal();
            setFocusControl(txtmill);
        }
        catch
        {
        }
    }

    private void CalculateDiffBal()
    {
        try
        {
            DataTable dt = (DataTable)ViewState["currentTable"];
            double TotalGridAmt = 0.00;
            for (int i = 0; i < grdDetail_Grd.Rows.Count; i++)
            {
                if (grdDetail_Grd.Rows[i].Cells[Ref_No].Text == hdnfRef_No.Value)
                {
                    TotalGridAmt += Convert.ToDouble(grdDetail_Grd.Rows[i].Cells[Qntl].Text);
                }
            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ID"].ToString() == hdnfRef_No.Value)
                    {
                        Double TotalBal = Convert.ToDouble(dt.Rows[i]["Qty"].ToString()) - TotalGridAmt;
                        dt.Rows[i]["Dispatch"] = TotalGridAmt.ToString();
                        dt.Rows[i]["Balance"] = TotalBal.ToString();
                        if (btnSave.Text != "Save")
                        {
                            #region decide whether actual row is updating or virtual [rowAction]
                            string id = clsCommon.getString("select Detail_Id from rawangiline_detail where  Detail_ID=" + hdnfRef_No.Value + " And Company_Code = " + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Doc_Date='" + Doc_date + "'");
                            if (id != "0")
                            {
                                dt.Rows[i]["rowAction"] = "U";   //actual row
                            }
                            else
                            {
                                dt.Rows[i]["rowAction"] = "A";    //virtual row
                            }
                            #endregion

                        }
                    }
                }
                grdDetail.DataSource = dt;
                grdDetail.DataBind();
                ViewState["currentTable"] = dt;
            }


        }
        catch
        {
        }
    }
    protected void btnclosedetail_Grd_Click(object sender, EventArgs e)
    {

    }
    protected void grdDetail_Grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int rowindex = row.RowIndex;
            if (e.CommandArgument == "lnk")
            {
                switch (e.CommandName)
                {
                    case "EditRecord":
                        if (grdDetail_Grd.Rows[rowindex].Cells[D_rowAction].Text != "D" && grdDetail_Grd.Rows[rowindex].Cells[D_rowAction].Text != "R")
                        {
                            // pnlPopupDetails.Style["display"] = "block";
                            this.showDetailsRow_Grd(grdDetail_Grd.Rows[rowindex]);
                            btnAdddetail_Grd.Text = "Update";
                        }
                        break;

                    case "DeleteRecord":
                        string action = "";
                        LinkButton lnkDelete = (LinkButton)e.CommandSource;
                        if (lnkDelete.Text == "Delete")
                        {
                            action = "Delete";
                            lnkDelete.Text = "Open";
                        }
                        else
                        {
                            action = "Open";
                            lnkDelete.Text = "Delete";
                        }
                        // this.DeleteDetailsRow_Grd(grdDetail_Grd.Rows[rowindex], action);

                        break;
                }
            }
        }
        catch
        {
        }
    }
    protected void grdDetail_Grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        e.Row.Cells[0].ControlStyle.Width = new Unit("50px");
        e.Row.Cells[1].ControlStyle.Width = new Unit("50px");

        e.Row.Cells[DID].ControlStyle.Width = new Unit("40px");
        e.Row.Cells[Ref_No].ControlStyle.Width = new Unit("70px");
        e.Row.Cells[Mill].ControlStyle.Width = new Unit("100px");
        e.Row.Cells[Mill_Name].ControlStyle.Width = new Unit("250px");
        e.Row.Cells[Station].ControlStyle.Width = new Unit("200px");
        e.Row.Cells[Truck_No].ControlStyle.Width = new Unit("150px");
        e.Row.Cells[Qntl].ControlStyle.Width = new Unit("60px");
        e.Row.Cells[Frt_Qntl].ControlStyle.Width = new Unit("90px");
        e.Row.Cells[Net_freiget].ControlStyle.Width = new Unit("80px");
        e.Row.Cells[Transport_Code].ControlStyle.Width = new Unit("100px");

        e.Row.Cells[Transport_Name].ControlStyle.Width = new Unit("250px");
        e.Row.Cells[Vasuli].ControlStyle.Width = new Unit("60px");
        e.Row.Cells[Remarks].ControlStyle.Width = new Unit("120px");
        e.Row.Cells[D_rowAction].ControlStyle.Width = new Unit("50px");
        e.Row.Cells[D_SrNo].ControlStyle.Width = new Unit("80px");
        e.Row.Cells[Remarks].Style["overflow"] = "hidden";

        e.Row.Cells[Transport_Name].Style["overflow"] = "hidden";
        e.Row.Cells[Mill_Name].Style["overflow"] = "hidden";

        e.Row.Cells[D_rowAction].Visible = false;
        e.Row.Cells[D_SrNo].Visible = false;

    }
}
