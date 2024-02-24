using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;


public partial class pgeCreateCompany : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            clsButtonNavigation.enableDisable("N");
            this.makeEmptyForm("N");
            ViewState["mode"] = "I";
            this.showLastRecord();
        }
    }

    private void showLastRecord()
    {
        try
        {
            string qry = string.Empty;
            qry = "select max(Company_Code) as Company_Code from Company";
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
                        bool recordExist= this.fetchRecord(dt.Rows[0]["Company_Code"].ToString());
                        if (recordExist == true)
                        {
                            btnEdit.Focus();
                        }
                    }
                }
            }

            this.enableDisableNavigateButtons();
        }
        catch
        {

        }
    }

    #region ---- makeEmptyForm ----
    private void makeEmptyForm(string dAction)
    {
        try
        {
            if (dAction == "N")
            {
                foreach (System.Web.UI.Control c in Panel1.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                    //if (c is System.Web.UI.WebControls.DropDownList)
                    //    ((System.Web.UI.WebControls.DropDownList)c).SelectedIndex = 0;
                    //if (c is System.Web.UI.WebControls.CheckBox)
                    //    ((System.Web.UI.WebControls.CheckBox)c).Checked = false;                
                }
                btnSave.Text = "Save";
            }

            if (dAction == "A")
            {
                foreach (System.Web.UI.Control c in Panel1.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
            }

            if (dAction == "S")
            {
                foreach (System.Web.UI.Control c in Panel1.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                }
            }

            if (dAction == "E")
            {
                foreach (System.Web.UI.Control c in Panel1.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                txtCompanyCode.Enabled = false;
            }

            lblMsg.Text = "";
        }
        catch
        {

        }
    }
    #endregion

    #region btnAdd_Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {        
        clsButtonNavigation.enableDisable("A");

        ViewState["mode"] = null;
        ViewState["mode"] = "I";
        this.makeEmptyForm("A");        

        this.getMaxCode();
        
        txtCompanyCode.Focus();        
    }
    #endregion

    #region --- getMaxCode ---
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;

            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = "Company";
                obj.code = "Company_Code";

                ds = new DataSet();
                ds = obj.getMaxCode();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ViewState["mode"] != null)
                            {
                                if (ViewState["mode"].ToString() == "I")
                                {
                                    txtCompanyCode.Text = ds.Tables[0].Rows[0][0].ToString();
                                }
                                
                            }
                           
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

    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string retValue= saveData();
        clsButtonNavigation.enableDisable("S");
        this.makeEmptyForm("S");

        if (retValue != "-3" && retValue != "-2" && retValue!="")
        {
            lblMsg.Text = "Successfully Added!";
            lblMsg.ForeColor = System.Drawing.Color.LimeGreen;

            clsCheckDatabase.createCompanyTables(retValue,txtCompanyName.Text);
            Response.Redirect("pgeStartup.aspx?groupcode=" + Session["GroupCode"].ToString());
        }
        if (retValue == "-2")
        {
            lblMsg.Text = "Successfully Updated!";
            lblMsg.ForeColor = System.Drawing.Color.LimeGreen;
        }
    }
    #endregion

    #region [saveData]
    private string saveData()
    {
        try
        {
            string str="";
            DataSet ds = null;

            using (clsCompany obj = new clsCompany())
            {
                if (ViewState["mode"] != null)
                {
                    if (ViewState["mode"].ToString() == "I")
                    {
                        obj.flag = 1;
                    }
                    if (ViewState["mode"].ToString() == "U")
                    {
                        obj.flag = 2;
                    }
                }
                obj.Company_Code = Convert.ToInt32(txtCompanyCode.Text);
                obj.Company_Name_E = txtCompanyName.Text;
                obj.Company_Name_R = txtRegionalName.Text;
                obj.Address_E = txtAddress.Text;
                obj.Address_R = txtRegionalAddress.Text;
                obj.City_E = txtcityE.Text;
                obj.City_R = txtcityR.Text;
                obj.State_E = txtstateE.Text;
                obj.State_R = txtstateR.Text;
                obj.Mobile_No = txtMob.Text;
                obj.PIN = txtpin.Text;
                obj.Pan_No = txtPanNo.Text;
                obj.Group_Code = Session["GroupCode"].ToString();
                obj.Created_By = Session["user"].ToString();
                obj.Modified_By = "1";
                ds = new DataSet();

                ds = obj.saveCompany(ref str);
                string retValue = str;
                if (retValue != "-3" && retValue != "-2")
                {
                    hdnf.Value = retValue;
                }

                return retValue;
            }
        }
        catch
        {
            return "";
        }
    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";

        clsButtonNavigation.enableDisable("E");

       // this.enableDisableNavigateButtons();

        if (txtCompanyCode.Text == string.Empty)
        {
            txtCompanyCode.Enabled = true;
            txtCompanyCode.Focus();
        }
        else
        {
            this.makeEmptyForm("E");
            txtCompanyCode.Enabled = true;
            //btnSave.Enabled = true;
            //btnDelete.Enabled = true;
            txtCompanyCode.Focus();
            
        }              
    }
    #endregion

    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clsButtonNavigation.enableDisable("N");
        this.makeEmptyForm("N");
        this.showLastRecord();
    }
    #endregion

    #region [txtCompanyName_TextChanged]
    protected void txtCompanyName_TextChanged(object sender, EventArgs e)
    {
        if (txtCompanyName.Text != string.Empty)
        {
            btnSave.Enabled = true;
            txtRegionalName.Focus();
        }
        else
        {
            btnSave.Enabled = false;
        }
    }
    #endregion

    #region [txtCompanyCode_TextChanged]
    protected void txtCompanyCode_TextChanged(object sender, EventArgs e)
    {
        #region code
        try
        {
            int n;
            bool isNumeric = int.TryParse(txtCompanyCode.Text, out n);
            //if(isiNan()
            if (isNumeric == true)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string txtValue = "";
                if (txtCompanyCode.Text != string.Empty)
                {
                    txtValue = txtCompanyCode.Text;

                    string qry = "select * from Company where Company_Code='" + txtValue + "' ";
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                if (ViewState["mode"] != null)
                                {
                                    if (ViewState["mode"].ToString() == "I")
                                    {
                                        //Response.Write("<script>alert('Code Already Exist');</script>");
                                        lblMsg.Text = "** Company Code Already Exist";
                                        lblMsg.ForeColor = System.Drawing.Color.Red;
                                        this.getMaxCode();
                                        txtCompanyCode.Focus();
                                    }

                                    if (ViewState["mode"].ToString() == "U")
                                    {
                                        //fetch record
                                        bool recordExist = this.fetchRecord(txtValue);
                                        if (recordExist == true)
                                        {
                                            // btnEdit.Enabled = true;
                                            // btnEdit.Focus();
                                            txtCompanyName.Focus();
                                            txtCompanyCode.Enabled = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (ViewState["mode"].ToString() == "I")
                                {
                                    lblMsg.Text = "";
                                    txtCompanyName.Focus();
                                    txtCompanyCode.Enabled = false;
                                }
                                if (ViewState["mode"].ToString() == "U")
                                {
                                    // Response.Write("<script>alert('Record Not Found');</script>");

                                    this.makeEmptyForm("A");
                                    lblMsg.Text = "** Record Not Found";
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    txtCompanyCode.Text = string.Empty;
                                    txtCompanyCode.Focus();
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = string.Empty;
                    txtCompanyCode.Focus();
                }
            }
            else
            {
                this.makeEmptyForm("A");
                lblMsg.Text = "Company code is numeric";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                clsButtonNavigation.enableDisable("E");
                txtCompanyCode.Text = string.Empty;
                txtCompanyCode.Focus();

            }
        }
        catch
        {

        }
        #endregion
    }
    #endregion

    #region [fetchRecord]
    public bool fetchRecord(string txtValue)
    {
        try
        {
            bool recordExist = false;
            string qry = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            qry = "select * from Company where Company_Code=" + txtValue;
            ds = clsDAL.SimpleQuery(qry);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        hdnf.Value = txtValue;
                        txtCompanyCode.Text = txtValue;
                        txtCompanyName.Text = dt.Rows[0]["Company_Name_E"].ToString();
                        txtRegionalName.Text = dt.Rows[0]["Company_Name_R"].ToString();
                        txtAddress.Text = dt.Rows[0]["Address_E"].ToString();
                        txtRegionalAddress.Text = dt.Rows[0]["Address_R"].ToString();
                        txtcityE.Text = dt.Rows[0]["City_E"].ToString();
                        txtcityR.Text = dt.Rows[0]["City_R"].ToString();
                        txtstateE.Text = dt.Rows[0]["State_E"].ToString();
                        txtstateR.Text = dt.Rows[0]["State_R"].ToString();
                        txtMob.Text = dt.Rows[0]["Mobile_No"].ToString();
                        txtpin.Text = dt.Rows[0]["PIN"].ToString();
                        txtPanNo.Text = dt.Rows[0]["Pan_No"].ToString();
                        recordExist = true;
                        lblMsg.Text = "";

                        
                    }

                }
            }

         //   this.enableDisableNavigateButtons();
            return recordExist;
              
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons

        //if (ViewState["mode"].ToString() == "U")
        //{

        int RecordCount = 0;

        string query = "";
        query = "select count(*) from Company";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(query);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    RecordCount = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
        }

        if (RecordCount != 0 && RecordCount == 1)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
        }
        else if (RecordCount != 0 && RecordCount > 1)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = true;
          //  btnLast.Focus();
        }

        if (txtCompanyCode.Text != string.Empty)
        {

            #region check for next or previous record exist or not
            ds = new DataSet();
            dt = new DataTable();

            query = "SELECT top 1 [Company_Code] from Company where Company_Code>" + Convert.ToInt32(hdnf.Value) + " ORDER BY Company_Code asc  ";
            ds = clsDAL.SimpleQuery(query);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //next record exist

                        btnNext.Enabled = true;
                    }
                    else
                    {
                        //next record does not exist
                        btnNext.Enabled = false;
                    }
                }
            }

            ds = new DataSet();
            dt = new DataTable();

            query = "SELECT top 1 [Company_Code] from Company where Company_Code<" + Convert.ToInt32(hdnf.Value) + " ORDER BY Company_Code asc  ";
            ds = clsDAL.SimpleQuery(query);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //previous record exist

                        btnPrevious.Enabled = true;
                    }
                    else
                    {
                        btnPrevious.Enabled = false;
                    }
                }
            }

        }
            #endregion
        // }

        #endregion
    }
    #endregion

    #region [btnFirst_Click]
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select Company_Code from Company where Company_Code=(select MIN(Company_Code) from Company) ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(query);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtCompanyCode.Text = dt.Rows[0][0].ToString();
                        ViewState["mode"] = "U";
                        clsButtonNavigation.enableDisable("E");
                       bool recordExist= this.fetchRecord(dt.Rows[0][0].ToString());
                        if (recordExist == true)
                        {
                            btnEdit.Enabled = true;
                            btnEdit.Focus();
                        }
                        
                        this.enableDisableNavigateButtons();
                        this.makeEmptyForm("S");

                    }
                }
            }

            
        }
        catch
        {

        }
    }
    #endregion

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCompanyCode.Text != string.Empty)
            {
                string query = "";
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                query = "SELECT top 1 [Company_Code] from Company where Company_Code<" + Convert.ToInt32(hdnf.Value) + " ORDER BY Company_Code DESC  ";

                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["mode"] = "U";
                            clsButtonNavigation.enableDisable("E");
                            bool recordExist = this.fetchRecord(dt.Rows[0]["Company_Code"].ToString());
                            if (recordExist == true)
                            {
                                btnEdit.Enabled = true;
                                btnEdit.Focus();
                            }
                            this.enableDisableNavigateButtons();
                            this.makeEmptyForm("S");
                        }
                    }
                }
            }
            
        }
        catch
        {

        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCompanyCode.Text != string.Empty)
            {
                string query = "";
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                query = "SELECT top 1 [Company_Code] from Company where Company_Code>" + Convert.ToInt32(hdnf.Value) + " ORDER BY Company_Code asc  ";

                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["mode"] = "U";
                            clsButtonNavigation.enableDisable("E");
                            bool recordExist = this.fetchRecord(dt.Rows[0]["Company_Code"].ToString());
                            if (recordExist == true)
                            {
                                btnEdit.Enabled = true;
                                btnEdit.Focus();
                            }
                            this.enableDisableNavigateButtons();
                            this.makeEmptyForm("S");
                        }
                    }
                }
            }
           
        }
        catch
        {

        }
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCompanyCode.Text != string.Empty)
            {
                string query = "";
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                query = "select Company_Code from Company where Company_Code=(select MAX(Company_Code) from Company) ";

                ds = clsDAL.SimpleQuery(query);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            ViewState["mode"] = "U";
                            clsButtonNavigation.enableDisable("E");
                            bool recordExist = this.fetchRecord(dt.Rows[0]["Company_Code"].ToString());
                            if (recordExist == true)
                            {
                                btnEdit.Enabled = true;
                                btnEdit.Focus();
                            }
                            this.enableDisableNavigateButtons();
                            this.makeEmptyForm("S");
                        }
                    }
                }
            }
            
        }
        catch
        {

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                string query = string.Empty;
                query = "delete from Company where Company_Code =" + hdnf.Value;
                DataSet ds = clsDAL.SimpleQuery(query);
                DataTable dt = new DataTable();
                lblMsg.Text = "Successfully Deleted";

                query = "SELECT top 1 [Company_Code] from Company where Company_Code>" + Convert.ToInt32(hdnf.Value) + " ORDER BY Company_Code asc  ";
                ds = clsDAL.SimpleQuery(query);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            fetchRecord(dt.Rows[0]["Company_Code"].ToString());
                        }
                        else
                        {
                            query = "SELECT top 1 [Company_Code] from Company where Company_Code<" + Convert.ToInt32(hdnf.Value) + " ORDER BY Company_Code DESC  ";
                            
                            ds = clsDAL.SimpleQuery(query);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    dt = ds.Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        fetchRecord(dt.Rows[0]["Company_Code"].ToString());
                                    }
                                    else
                                    {
                                        this.makeEmptyForm("N");
                                    }
                                }
                            }
                        }

                    }
                }
                clsButtonNavigation.enableDisable("S");
               
            }
        }
        catch
        {

        }

    }




    //private void HandleCustomPostbackEvent(string ctrlName, string args)
    //{
    //     onblur="__doPostBack('tbOnBlur','OnBlur');" 
    //    //Since this will get called for every postback, we only
    //    // want to handle a specific combination of control
    //    // and argument.
    //    if (ctrlName == txtCompanyCode.UniqueID && args == "OnBlur")
    //    {
            
    //        if (ViewState["mode"] != null)
    //        {

    //            if (ViewState["mode"].ToString() == "U")
    //            {
    //                if (txtCompanyCode.Text != string.Empty)
    //                {
    //                    bool recordExist = this.fetchRecord(txtCompanyCode.Text);
    //                    if (recordExist == true)
    //                    {
    //                        txtCompanyCode.Enabled = false;
    //                        txtCompanyName.Focus();
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                txtCompanyCode.Enabled = false;
    //                txtCompanyName.Focus();
    //            }

    //        }
    //    }
    //}


    [WebMethod]
    public static clsCompany[] BindDatatable(string str)
    {
        DataTable dt = new DataTable();
        List<clsCompany> details = new List<clsCompany>();

        clsCompany obj = new clsCompany();

        string qry = "select * from Company where Company_Code="+str;
        DataSet ds = new DataSet();
       //
        //DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    obj.Company_Name_E =dt.Rows[0]["Company_Name_E"].ToString();
                    obj.Company_Name_R = dt.Rows[0]["Company_Name_R"].ToString();
                    obj.City_E = dt.Rows[0]["City_E"].ToString();
                    obj.City_R = dt.Rows[0]["City_R"].ToString();
                    obj.State_E = dt.Rows[0]["State_E"].ToString();
                    obj.State_R = dt.Rows[0]["State_R"].ToString();
                    obj.Mobile_No = dt.Rows[0]["Mobile_No"].ToString();
                    obj.Pan_No = dt.Rows[0]["Pan_No"].ToString();
                    obj.PIN = dt.Rows[0]["PIN"].ToString();
                    obj.Address_E = dt.Rows[0]["Address_E"].ToString();
                    obj.Address_R = dt.Rows[0]["Address_R"].ToString();

                    details.Add(obj);
                }
            }
        }
        return details.ToArray();

    }

    //public class CompanyDetails
    //{
    //    public string companyName { get; set; }

    //}
}