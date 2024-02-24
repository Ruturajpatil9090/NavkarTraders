using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sugar_Master_pgeBal_Limit : System.Web.UI.Page
{
    #region data section
    string qry = string.Empty;
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string qryCommon = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string qryAccountList = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    static WebControl objAsp = null;

    DataTable Maindt = null;
    DataTable SalePurcdt = null;
    DataRow dr = null;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = tblPrefix + "AccountMaster";
            tblDetails = "";
            qryCommon = "";
            qryAccountList = "qrymstaccountmaster";
            user = Session["user"].ToString();
            pnlPopup.Style["display"] = "none";

            Maindt = new DataTable();
            dr = null;
            Maindt.Columns.Add("Querys", typeof(string));
            dr = Maindt.NewRow();
            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");


                if (isAuthenticate == "1" || User_Type == "A")
                {
                    //if(user == "amit89")
                    //{
                    pnlPopup.Style["display"] = "none";
                    ViewState["currentTable"] = null;
                    //clsButtonNavigation.enableDisable("N");
                    //this.makeEmptyForm("N");
                    ViewState["mode"] = "U";
                    setFocusControl(txtAc_Code);
                    //this.showLastRecord();
                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
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
        }
        catch
        {
        }

    }
    #region[show last]
    private void showLastRecord()
    {
        try
        {
            string qry = string.Empty;
            qry = "select max(Ac_Code) as Ac_Code from " + tblHead +
                 " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

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
                        hdnf.Value = dt.Rows[0]["Ac_Code"].ToString();
                        qry = getDisplayQuery();
                        bool recordExist = this.fetchRecord(qry);
                        if (recordExist == true)
                        {
                            txtAc_Code.Focus();
                        }
                        else                     //new code
                        {
                            //btnEdit.Enabled = false;
                            //btnDelete.Enabled = false;
                        }
                    }
                }
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
            string qryDisplay = " select * from " + qryCommon + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Ac_Code=" + hdnf.Value;
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
                        Label lblCreated = (Label)Master.FindControl("MasterlblCreatedBy");
                        Label lblModified = (Label)Master.FindControl("MasterlblModifiedBy");
                        if (lblCreated != null)
                        {
                            lblCreated.Text = "Created By: " + dt.Rows[0]["Created_By"].ToString();
                        }
                        if (lblModified != null)
                        {
                            lblModified.Text = "Modified By: " + dt.Rows[0]["Modified_By"].ToString();
                        }
                        Label lblCreatedDate = (Label)Master.FindControl("MasterlblCreatedDate");
                        Label lblModifiedDate = (Label)Master.FindControl("MasterlblModifiedDate");
                        if (lblCreatedDate != null)
                        {
                            if (dt.Rows[0]["Created_Date"].ToString() == string.Empty)
                            {
                                lblModifiedDate.Text = "";
                            }
                            else
                            {
                                lblCreatedDate.Text = "Created Date" + DateTime.Parse(dt.Rows[0]["Created_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                            }
                        }
                        if (lblModifiedDate != null)
                        {
                            if (dt.Rows[0]["Modified_Date"].ToString() == string.Empty)
                            {
                                lblModifiedDate.Text = "";
                            }
                            else
                            {
                                lblModifiedDate.Text = "Modified Date" + DateTime.Parse(dt.Rows[0]["Modified_Date"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                            }
                        }
                        txtAc_Code.Text = dt.Rows[0]["EditDoc_No"].ToString();
                        txtBalance_Limit.Text = dt.Rows[0][""].ToString();


                        recordExist = true;
                        lblMsg.Text = "";
                    }
                }
            }
            hdnf.Value = txtAc_Code.Text;

            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtAc_Code.Enabled = true;
        txtAc_Code.Text = string.Empty;
        txtBalance_Limit.Text = string.Empty;

        setFocusControl(txtAc_Code);
        //if (hdnf.Value != string.Empty)
        //{
        //    string query = getDisplayQuery();
        //    bool recordExist = this.fetchRecord(query);
        //}
        //else
        //{
        //    this.showLastRecord();
        //}

    }
    #endregion
    #region [txtAc_Code_TextChanged]
    protected void txtAc_Code_TextChanged(object sender, EventArgs e)
    {

        searchString = txtAc_Code.Text;
        strTextBox = "txtAc_Code";
        csCalculations();
        //return;

    }
    #endregion
    #region [btntxtAc_Code_Click]
    protected void btntxtAc_Code_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "")
            {
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtAc_Code";
                btnSearch_Click(sender, e);
            }
            else
            {
                hdconfirm.Value = string.Empty;
                setFocusControl(txtAc_Code);
            }


        }
        catch
        {
        }
    }
    #endregion
    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string searchtxt = "";
            string delimStr = " ";
            char[] delimiter = delimStr.ToCharArray();
            string words = "";
            string[] split = null;
            string name = string.Empty;

            if (searchString != string.Empty && strTextBox == hdnfClosePopup.Value)
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
            if (hdnfClosePopup.Value == "txtEditDoc_No")
            {
                //foreach (var s in split)
                //{
                //    string aa = s.ToString();

                //    //name += " Account_Name_E like '%" + aa + "%'  or Account_Code like '%" + aa + "%'  or Account_Name_R like '%" + aa + "%'  or";
                //    name += "( Ac_Name_E like '%" + aa + "%' or Ac_Code like '%" + aa + "%' or Ac_Name_R like '%" + aa + "%' ) and";
                //}
                //name = name.Remove(name.Length - 3);

                //lblPopupHead.Text = "--Select Group--";
                //string qry = "select Ac_Code,Ac_Name_E,Ac_Op_Bal,Ac_Op_Bal_DrCr from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                // + "  and ( " + name + ") order by Ac_Name_E";
                //this.showPopup(qry);
            }

            if (hdnfClosePopup.Value == "txtAc_Code")
            {
                //foreach (var s in split)
                //{
                //    string aa = s.ToString();

                //    //name += " Account_Name_E like '%" + aa + "%'  or Account_Code like '%" + aa + "%'  or Account_Name_R like '%" + aa + "%'  or";
                //    name += "( Ac_Name_E like '%" + aa + "%' or Ac_Code like '%" + aa + "%' or Ac_Name_R like '%" + aa + "%' or Ac_City_E like '%" + aa + "%' ) and";
                //}
                //name = name.Remove(name.Length - 3);

                //lblPopupHead.Text = "--Select Group--";
                //string qry = "select Ac_Code,Ac_Name_E,Ac_City_E from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                // + "  and ( " + name + ") order by Ac_Name_E";
                //this.showPopup(qry);


                lblPopupHead.Text = "--Select Party Code--";
                string qry = "select Ac_Code,Ac_Name_E,CityName from " + qryAccountList + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);

            }
        }
        catch
        {
        }
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

    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion
    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtAc_Code")
            {
                if (txtAc_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtAc_Code.Text);
                    if (a == false)
                    {
                        btntxtAc_Code_Click(this, new EventArgs());
                    }
                    else
                    {

                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        string qry1 = "select Ac_Name_E,isnull(Bal_Limit,0) as Bal_Limit from  NT_1_AccountMaster where Ac_Code='"
                            + txtAc_Code.Text + "' and Company_Code="
                            + Convert.ToInt32(Session["Company_Code"].ToString());
                        ds = clsDAL.SimpleQuery(qry1);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    lblAc_Name.Text = dt.Rows[0]["Ac_Name_E"].ToString();

                                    txtBalance_Limit.Text = dt.Rows[0]["Bal_Limit"].ToString();


                                    setFocusControl(txtBalance_Limit);
                                    txtAc_Code.Enabled = false;
                                    return;
                                }
                                else
                                {
                                    txtAc_Code.Text = string.Empty;
                                    setFocusControl(txtAc_Code);
                                }
                            }
                            else
                            {
                                txtAc_Code.Text = string.Empty;
                                setFocusControl(txtAc_Code);
                            }

                        }

                        else
                        {
                            txtAc_Code.Text = string.Empty;
                            setFocusControl(txtAc_Code);
                        }

                    }
                }
                else
                {
                    setFocusControl(txtAc_Code);
                }

            }
        }
        catch
        {
        }
    }
    #endregion

    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtAc_Code.Enabled != false)
        {
            #region validation
            bool isValidated = true;
            if (txtAc_Code.Text != string.Empty)
            {

                isValidated = true;
            }
            else
            {
                isValidated = false;
                setFocusControl(txtAc_Code);
                return;
            }

            #endregion

            #region -Head part declearation

            string Ac_Code = txtAc_Code.Text;
            double Balance = Convert.ToDouble(txtBalance_Limit.Text != string.Empty ? txtBalance_Limit.Text : "0.00");
            #endregion-End of Head part declearation

            #region save Head Master



            qry = "update " + tblHead + " set Bal_Limit='" + txtBalance_Limit.Text + "' where Ac_Code=" + txtAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());

            dr = null;
            dr = Maindt.NewRow();
            dr["Querys"] = qry;
            Maindt.Rows.Add(dr);
            int flag = 2;
            string msg = DOPurcSaleCRUD.DOPurcSaleCRUD_OP(flag, Maindt);
            if (msg == "Update")
            {
                //ds = clsDAL.SimpleQuery(Group_Insert);
                hdnf.Value = txtAc_Code.ToString();
                clsButtonNavigation.enableDisable("S");

                qry = getDisplayQuery();
                this.fetchRecord(qry);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('Record Successfully Update !')", true);
            }
            #endregion


        }
        else
        {
            txtAc_Code.Enabled = true;
            // this.btnSave_Click(sender, new EventArgs());
            //return;
        }
    }
    #endregion

    #region [txtEditDoc_No_TextChanged]
    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtEditDoc_No.Text;
        //strTextBox = "txtEditDoc_No";
        //csCalculations();

        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);
            if (a == false)
            {
                searchString = txtEditDoc_No.Text;
                strTextBox = "txtEditDoc_No";
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtEditDoc_No";
                btnSearch_Click(this, new EventArgs());
            }
            else
            {
                hdnf.Value = txtEditDoc_No.Text;
                string qry1 = getDisplayQuery();
                fetchRecord(qry1);
                setFocusControl(txtEditDoc_No);
            }
        }
        catch (Exception)
        {
            throw;
        }

    }
    #endregion
    #region [imgBtnClose_Click]
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        try
        {
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
    #endregion

    #region [txtSearchText_TextChanged]
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
                strTextBox = hdnfClosePopup.Value;
                setFocusControl(btnSearch);
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            if (v == "txtEditDoc_No" || v == "txtAc_Code")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("60px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("200px");
                e.Row.Cells[2].ControlStyle.Width = new Unit("160px");

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;

            }

        }
        //    e.Row.Cells[0].Style["overflow" ] = "hiden";
        //    e.Row.Cells[0].Visible =true;

    }
    #endregion
    #region [grdPopup_PageIndexChanging]
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }
    #endregion
    #region [grdPopup_RowCreated]
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
    #endregion
}