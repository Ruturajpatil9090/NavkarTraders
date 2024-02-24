using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_pgeNewUser : System.Web.UI.Page
{
    string isAuthenticate = string.Empty;
    string tblPrefix = string.Empty;
    string user = string.Empty;
    string qry = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["user"].ToString();
        tblPrefix = Session["tblPrefix"].ToString();
        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                trgroup.Visible = false;
                trpages.Visible = false;
                this.FillTree();
                this.fillBranches();
                clsButtonNavigation.enableDisable("N");
                this.makeEmptyForm("N");
                this.showLastRecord();
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }
    #region [showLastRecord]
    private void showLastRecord()
    {
        try
        {
            string qry = string.Empty;
            qry = "select max(User_Id) as User_Id from tblUser where User_Type!='C'";
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
                        hdnf.Value = dt.Rows[0]["User_Id"].ToString();
                        qry = getDisplayQuery();
                        bool recordExist = this.fetchRecord(qry);
                        if (recordExist == true)
                        {
                            btnAdd.Focus();
                        }
                        else                     //new code
                        {
                            btnEdit.Enabled = true;
                            btnDelete.Enabled = true;
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
    #endregion
    #region getDisplayQuery
    protected string getDisplayQuery()
    {
        try
        {
            string qryDisplay = "select * from " + tblPrefix + "qryUsers where User_Id=" + hdnf.Value + " and User_Type!='C'";
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
            DataSet ds1 = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                        string User_id = dt.Rows[0]["User_Id"].ToString();
                        qry = "select A.PageID,B.Name from " + tblPrefix + "Authorization A inner join " + tblPrefix + "ApplicationPages B ON A.PageID=B.Page_ID WHERE User_id=" + User_id + "";
                        ds1 = clsDAL.SimpleQuery(qry);
                        if (ds1 != null)
                        {
                            if (ds1.Tables.Count > 0)
                            {
                                dt1 = ds1.Tables[0];
                                if (dt1.Rows.Count > 0)
                                {
                                    foreach (TreeNode parent in TreeSelectPages.Nodes)
                                    {
                                        foreach (TreeNode child in parent.ChildNodes)
                                        {
                                            for (int j = 0; j < dt1.Rows.Count; j++)
                                            {
                                                if (child.Text.Trim() == dt1.Rows[j]["Name"].ToString().Trim())
                                                {
                                                    child.Checked = true;
                                                    //parent.Checked = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        txtUname1.Text = dt.Rows[0]["User_Name"].ToString();
                        txtpass1.Text = dt.Rows[0]["Password"].ToString();
                        txtUserEmail.Text = dt.Rows[0]["EmailId"].ToString();
                        txtEmailPassword.Text = dt.Rows[0]["EmailPassword"].ToString();
                        txtUserMobil.Text = dt.Rows[0]["Mobile"].ToString();
                        string User_Type = dt.Rows[0]["User_Type"].ToString();
                        if (User_Type == "U")
                        {
                            drpUserType.SelectedValue = "U";
                            trgroup.Visible = true;
                            trpages.Visible = true;
                        }
                        else
                        {
                            drpUserType.SelectedValue = "A";
                            trgroup.Visible = false;
                            trpages.Visible = false;
                        }
                        string Branch_Code = dt.Rows[0]["Branch_Code"].ToString();
                        drpUserBranch.SelectedValue = Branch_Code;
                        //drpUserType.SelectedIndex = drpUserType.Items.IndexOf(drpUserType.Items.FindByValue(Branch_Code));
                        recordExist = true;
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
    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        try
        {
            #region validation
            string str = "";
            int Group_Code = Convert.ToInt32(Session["Group_Code"].ToString());
            string User_Name = txtUname1.Text;
            string Password = txtpass1.Text;
            string Branch_Code = drpUserBranch.SelectedValue;
            string EmailId = txtUserEmail.Text;
            string EmailPassword = txtEmailPassword.Text;
            string User_Type = drpUserType.SelectedValue;
            string Mobile = txtUserMobil.Text;
            string qry = "";
            if (ViewState["mode"] != null)
            {
                if (ViewState["mode"].ToString() == "I")
                {
                    qry = "select User_Id from tblUser where User_Name='" + txtUname1.Text + "'";
                    string id = clsCommon.getString(qry);
                    if (id == string.Empty)
                    {

                    }
                    else
                    {
                        lblmsg.Text = "UserName already exist!choose another";
                        SetFocus(txtUname1);
                        return;
                    }
                    if (txtpass1.Text == string.Empty)
                    {
                        SetFocus(txtpass1);
                        return;
                    }
                    if (txtConfirmPass.Text == string.Empty)
                    {
                        SetFocus(txtpass1);
                        return;
                    }
                    else
                    {
                        if (txtpass1.Text != txtConfirmPass.Text)
                        {
                            txtConfirmPass.Text = string.Empty;
                            SetFocus(txtConfirmPass);
                            return;
                        }
                    }
                    //List<TreeNode> abc = new List<TreeNode>();
                    //if (User_Type == "U")
                    //{
                    //    foreach (TreeNode node in TreeSelectPages.Nodes)
                    //    {
                    //        foreach (TreeNode child in node.ChildNodes)
                    //        {
                    //            CheckBoxList chklist=new CheckBoxList();
                    //            abc.Add(child);
                    //        }
                    //    }

                    //}
                    if (drpUserBranch.SelectedIndex == 0)
                    {
                        SetFocus(drpUserBranch);
                        return;
                    }
            #endregion
                    using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                    {
                        obj.flag = 1;
                        obj.tableName = "tblUser";
                        obj.columnNm = "User_Name,Password,User_Type,Branch_Code,EmailId,EmailPassword,Company_Code,Mobile";
                        obj.values = "'" + User_Name + "','" + Password + "','" + User_Type + "','" + Branch_Code + "','" + EmailId + "','" + EmailPassword + "','" + Convert.ToInt32(Session["Company_Code"].ToString()) + "','" + Mobile + "'";
                        obj.insertAccountMaster(ref str);

                        if (User_Type == "U")
                        {
                            foreach (TreeNode node in TreeSelectPages.Nodes)
                            {
                                string text = node.Text.ToString();
                                AddNodeAndChildNodesToList(node);
                            }
                        }
                    }
                    clsButtonNavigation.enableDisable("S");
                    this.enableDisableNavigateButtons();
                    this.makeEmptyForm("S");
                    qry = getDisplayQuery();
                    this.fetchRecord(qry);
                    this.showLastRecord();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('User Successfully Created!');", true);
                    txtUname1.Text = string.Empty;
                    txtpass1.Text = string.Empty;
                    txtUserEmail.Text = string.Empty;
                    txtEmailPassword.Text = string.Empty;
                    txtConfirmPass.Text = string.Empty;
                }
                else
                {
                    using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                    {
                        obj.flag = 2;
                        obj.tableName = "tblUser";
                        obj.columnNm = "User_Name='" + User_Name + "',Password='" + Password + "',User_Type='" + User_Type + "',Branch_Code='" + Branch_Code + "',EmailId='" + EmailId + "',EmailPassword='" + EmailPassword + "',Mobile='" + Mobile + "' where User_Name='" + User_Name + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
                        obj.values = "none";
                        obj.insertAccountMaster(ref str);

                        string userid = clsCommon.getString("Select User_Id from tblUser where User_Name='" + User_Name + "'");
                        qry = "Delete From " + tblPrefix + "Authorization where User_id=" + userid + "";
                        clsDAL.SimpleQuery(qry);
                        if (User_Type == "U")
                        {
                            foreach (TreeNode node in TreeSelectPages.Nodes)
                            {
                                string text = node.Text.ToString();
                                AddNodeAndChildNodesToList(node);
                            }
                        }
                    }
                    clsButtonNavigation.enableDisable("S");
                    this.enableDisableNavigateButtons();
                    this.makeEmptyForm("S");
                    qry = getDisplayQuery();
                    this.fetchRecord(qry);
                    this.showLastRecord();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('User Successfully Update!');", true);
                }
            }
        }
        catch
        {
        }
    }
    public void AddNodeAndChildNodesToList(TreeNode node)
    {
        #region selecting child nodes
        string pageid = string.Empty;
        int IsAuthenticate = 0;
        string text = string.Empty;
        List<TreeNode> child = new List<TreeNode>();
        if (node.ChildNodes.Count == 0)
        {
            text = node.Text.ToString();
            child.Add(node);
        }
        else
        {
            foreach (TreeNode n in node.ChildNodes)
            {
                string user = clsGV.user;
                if (n.Checked == true)
                {
                    text = n.Text.ToString();
                    qry = "Select Page_ID from " + tblPrefix + "ApplicationPages WHERE Name='" + text + "'";
                    DataSet ds = new DataSet();
                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                pageid = dt.Rows[i]["Page_ID"].ToString();
                            }
                        }
                        IsAuthenticate = 1;
                        using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                        {
                            string userid = "0";
                            if (ViewState["mode"].ToString() == "I")
                            {
                                userid = clsCommon.getString("Select MAX(User_Id) from tblUser");
                            }
                            else
                            {
                                userid = clsCommon.getString("Select User_Id from tblUser where User_Name='" + txtUname1.Text + "' and User_Type!='C'");
                            }
                            string strRef = string.Empty;
                            DataSet ds1 = new DataSet();
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "Authorization";
                            obj.columnNm = "PageID,User_id,IsAuthenticate";
                            obj.values = "'" + pageid + "','" + Convert.ToInt32(userid) + "','" + IsAuthenticate + "'";
                            ds1 = obj.insertAccountMaster(ref strRef);
                        }
                    }
                }
                AddNodeAndChildNodesToList(n);
            }
        }
        #endregion
        #region parent and child nodes
        //listBox1.Items.Add(TreeView1.Text);    // Adding current nodename to ListBox     
        //List<string> selectedPages = new List<string>();
        //foreach (TreeNode actualNode in Node.ChildNodes)
        //{
        //    if (actualNode.Checked == true)
        //    {
        //        string a = actualNode.Value.ToString();
        //        selectedPages.Add(a);
        //    }
        //    for (int i = 0; i < selectedPages.Count; i++)
        //    {
        //        string abc = selectedPages[i];
        //    }
        //    AddNodeAndChildNodesToList(actualNode); // recursive call
        //}
        #endregion
    }


    private void fillBranches()
    {
        try
        {
            ListItem li = new ListItem("---Select---", "0");
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            drpUserBranch.Items.Clear();
            string qry = "select Branch_Id,Branch from BranchMaster where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        drpUserBranch.DataSource = dt;
                        drpUserBranch.DataTextField = "Branch";
                        drpUserBranch.DataValueField = "Branch_Id";
                        drpUserBranch.DataBind();
                    }
                }
            }

            drpUserBranch.Items.Insert(0, li);
        }
        catch
        {

        }
    }
    protected void imgSelectPages_Click(object sender, ImageClickEventArgs e)
    {
        pnlTree.Style["display"] = "block";
    }

    private void FillTree()
    {
        DataSet ds = new DataSet();
        ds = clsDAL.SimpleQuery("Select Under_id,Name from " + tblPrefix + "Under ");
        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            this.PopulateTreeView(dt, 0, null);
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        pnlTree.Style["display"] = "none";
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        //foreach (TreeNode actualNode in TreeSelectPages.Nodes)         // Begin with Nodes from TreeView
        //{
        //    AddNodeAndChildNodesToList(actualNode);
        //}
        pnlTree.Style["display"] = "none";

    }

    private void PopulateTreeView(DataTable dtParent, int parentId, TreeNode treeNode)
    {
        foreach (DataRow row in dtParent.Rows)
        {
            TreeNode child = new TreeNode
            {
                Text = row["Name"].ToString(),
                Value = row["Under_id"].ToString()
            };
            if (parentId == 0)
            {
                string qry = "SELECT Page_ID,Name,Under_id FROM " + tblPrefix + "ApplicationPages WHERE Under_id = " + child.Value + "";
                TreeSelectPages.Nodes.Add(child);
                DataSet ds = new DataSet();
                ds = clsDAL.SimpleQuery(qry);
                if (ds != null)
                {
                    DataTable dtChild = ds.Tables[0];
                    PopulateTreeView(dtChild, int.Parse(child.Value), child);
                }
            }
            else
            {
                treeNode.ChildNodes.Add(child);
            }
        }
    }

    protected void SelectGroup_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        qry = "Select Group_ID,Group_Name from " + tblPrefix + "AuthorizationGroup";
        ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            dt = ds.Tables[0];
            grdAuthoGroup.DataSource = dt;
            grdAuthoGroup.DataBind();
        }
        else
        {
            grdAuthoGroup.DataSource = null;
            grdAuthoGroup.DataBind();
        }

        pnlSelectGroup.Style["display"] = "block";
    }
    protected void grdAuthoGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //CheckBox chk = (CheckBox)e.Row.FindControl("grdCB");
            //chk.Attributes.Add("onpropertychange",string.Format("CounterCheckBox('{0}');",chk.ClientID));
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[0].Width = new Unit("50px");

            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].Width = new Unit("80px");

            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].Width = new Unit("200px");
        }
    }
    protected void btnSelectedgroup_Click(object sender, EventArgs e)
    {
        List<string> a = new List<string>();
        string id = string.Empty;
        string pageid = string.Empty;
        string dtpagevalue = string.Empty;
        string[] ab = new string[] { };
        string groups = string.Empty;

        foreach (GridViewRow gr in grdAuthoGroup.Rows)
        {
            CheckBox chk = (gr.Cells[0].FindControl("grdCB") as CheckBox);
            for (int i = 0; i < grdAuthoGroup.Rows.Count; i++)
            {
                CheckBox chk1 = grdAuthoGroup.Rows[i].FindControl("grdCB") as CheckBox;
                if (chk1 != null && chk1.Checked == true)
                {
                    id = grdAuthoGroup.Rows[i].Cells[1].Text.ToString();
                    a.Add(id);
                }
            }

            ab = a.ToArray();
            groups = string.Join(",", ab);

            qry = "Select PageID from " + tblPrefix + "Authorization Where Group_ID in(" + groups + ")";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];

                foreach (TreeNode node in TreeSelectPages.Nodes)
                {
                    string parentid = node.Value.ToString();
                    qry = "Select Page_ID from " + tblPrefix + "ApplicationPages where Under_id=" + parentid + "";
                    DataSet dspage = new DataSet();
                    dspage = clsDAL.SimpleQuery(qry);
                    if (dspage != null)
                    {
                        DataTable dtpage = new DataTable();
                        dtpage = dspage.Tables[0];
                        for (int i = 0; i < dtpage.Rows.Count; i++)
                        {
                            dtpagevalue = dtpage.Rows[i]["Page_ID"].ToString();
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                string dtvalue = dt.Rows[j]["PageID"].ToString();
                                if (dtpagevalue == dtvalue)
                                {
                                    foreach (TreeNode child in node.ChildNodes)
                                    {
                                        child.Checked = true;
                                    }
                                }
                            }
                        }
                    }

                }
                //if (TreeSelectPages.Nodes.Count > 0)
                //{
                //    for (int i = 0; i < TreeSelectPages.Nodes.Count; i++)
                //    {
                //        DisplayChildNodeValue(TreeSelectPages.Nodes[i]);
                //    }
                //}
            }
        }
        pnlSelectGroup.Style["display"] = "none";
    }
    void DisplayChildNodeValue(TreeNode node)
    {
        string nodeValue = node.Value.ToString();
        for (int i = 0; i < node.ChildNodes.Count; i++)
        {
            DisplayChildNodeValue(node.ChildNodes[i]);
            string val = node.ChildNodes[i].Value.ToString();
        }
    }
    protected void drpUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string User_Type = drpUserType.SelectedValue;
        if (User_Type == "U")
        {
            trgroup.Visible = true;
            trpages.Visible = true;
        }
        else
        {
            trgroup.Visible = false;
            trpages.Visible = false;
        }

    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        pnlSelectGroup.Style["display"] = "none";
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select User_Id from tblUser where User_Id=(select MIN(User_Id) from tblUser where ser_Type!='C') and  User_Type!='C'";
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtUname1.Text != string.Empty)
            {
                string query = "SELECT top 1 [User_Id] from tblUser where User_Id<" + Convert.ToInt32(hdnf.Value) +
                                " and User_Type!='C' order by User_Id desc";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
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
            if (txtUname1.Text != string.Empty)
            {
                string query = "SELECT top 1 [User_Id] from tblUser where User_Id>" + Convert.ToInt32(hdnf.Value) +
                                " and User_Type!='C' order by User_Id asc";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
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
            string query = "";
            query = "select User_Id from tblUser where User_Id=(select MAX(User_Id) from tblUser where ser_Type!='C') and  User_Type!='C'";
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #region navigateRecord
    private void navigateRecord()
    {
        try
        {
            if (hdnf.Value != string.Empty)
            {
                ViewState["mode"] = "U";

                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    //btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    //btnEdit.Focus();
                }
                this.enableDisableNavigateButtons();
                this.makeEmptyForm("S");
            }
            else
            {
                showLastRecord();
            }
        }
        catch
        {

        }
    }
    #endregion
    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        query = "select count(*) from tblUser";
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
        }
        if (hdnf.Value.Trim() != string.Empty)
        {
            if (hdnf.Value != string.Empty)
            {
                #region check for next or previous record exist or not
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [User_Id] from tblUser where User_Id>" + Convert.ToInt32(hdnf.Value) +
                    "  ORDER BY User_Id asc  ";
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
                            btnLast.Enabled = true;
                        }
                        else
                        {
                            //next record does not exist
                            btnNext.Enabled = false;
                            btnLast.Enabled = false;
                        }
                    }
                }
                ds = new DataSet();
                dt = new DataTable();
                query = "SELECT top 1 [User_Id] from tblUser where User_Id<" + Convert.ToInt32(hdnf.Value) +
                    "  ORDER BY User_Id asc  ";
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
                            btnFirst.Enabled = true;
                        }
                        else
                        {
                            btnPrevious.Enabled = false;
                            btnFirst.Enabled = false;
                        }
                    }
                }
                #endregion
            }

        }
        #endregion
    }
    #endregion
    #region [makeEmptyForm]
    private void makeEmptyForm(string dAction)
    {
        try
        {
            if (dAction == "N")
            {
                foreach (System.Web.UI.Control c in pnlCreateUser.Controls)
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

                btnSave.Text = "Save";

                #region logic
                txtUname1.Enabled = false;
                txtpass1.Enabled = false;
                drpUserType.Enabled = false;
                drpUserBranch.Enabled = false;
                txtUserEmail.Enabled = false;
                SelectGroup.Enabled = false;
                imgSelectPages.Enabled = false;
                #endregion
            }
            if (dAction == "A")
            {
                drpUserBranch.SelectedIndex = 0;
                foreach (System.Web.UI.Control c in pnlCreateUser.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                btnSave.Text = "Save";
                txtUname1.Text = string.Empty;
                txtpass1.Text = string.Empty;
                txtEmailPassword.Text = string.Empty;
                txtUserEmail.Text = string.Empty;
                drpUserType.Enabled = true;
                drpUserBranch.Enabled = true;
                drpUserBranch.SelectedIndex = 0;
                ViewState["currentTable"] = null;
                SelectGroup.Enabled = true;
                imgSelectPages.Enabled = true;
                foreach (TreeNode parent in TreeSelectPages.Nodes)
                {
                    foreach (TreeNode child in parent.ChildNodes)
                    {
                        if (child.Checked == true)
                        {
                            child.Checked = false;
                        }
                    }
                }

            }
            if (dAction == "S")
            {
                foreach (System.Web.UI.Control c in pnlCreateUser.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                }

                txtUserEmail.Enabled = false;
                txtUname1.Enabled = false;

                #region logic
                drpUserType.Enabled = false;
                drpUserBranch.Enabled = false;
                txtpass1.Enabled = false;
                txtUserEmail.Enabled = false;
                txtEmailPassword.Enabled = false;
                SelectGroup.Enabled = false;
                imgSelectPages.Enabled = false;
                #endregion
            }
            if (dAction == "E")
            {
                foreach (System.Web.UI.Control c in pnlCreateUser.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                string ustype = clsCommon.getString("Select User_Type from tblUser where User_Name='" + txtUname1.Text + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                txtEmailPassword.Enabled = true;
                txtUserEmail.Enabled = true;
                txtUname1.Enabled = true;
                drpUserBranch.Enabled = true;
                drpUserType.Enabled = true;
                txtpass1.Enabled = true;
                txtConfirmPass.Enabled = true;
                txtEmailPassword.Enabled = true;
                SelectGroup.Enabled = true;
                imgSelectPages.Enabled = true;
            }

        }
        catch { }
    }
    #endregion
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                string currentuserid = hdnf.Value.ToString().Trim();
                DataSet ds = new DataSet();
                qry = "Delete from tblUser where User_Name='" + txtUname1.Text + "' and User_type!='C'";
                ds = clsDAL.SimpleQuery(qry);


                string query = "Select TOP 1 [User_Id] from tblUser Where User_Id<" + Convert.ToInt32(currentuserid) + " and User_Type!='C' order by User_Id asc";
                hdnf.Value = clsCommon.getString(query);

                if (hdnf.Value == string.Empty)
                {
                    query = "Select TOP 1 [User_Id] from tblUser Where User_Id<" + Convert.ToInt32(currentuserid) + " and User_Type!='C' order by User_Id desc";
                    hdnf.Value = clsCommon.getString(query);

                }
                if (hdnf.Value != string.Empty)
                {
                    query = getDisplayQuery();
                    bool recordExist = this.fetchRecord(query);
                    this.makeEmptyForm("S");
                    clsButtonNavigation.enableDisable("S");
                }
                else
                {
                    this.makeEmptyForm("N");
                    //new code
                    this.showLastRecord();
                    clsButtonNavigation.enableDisable("N");         //No record exist  Last record deleted.

                }
                this.enableDisableNavigateButtons();
            }
        }
        catch (Exception xxx)
        {
            throw xxx;
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            clsButtonNavigation.enableDisable("E");
            ViewState["mode"] = null;
            ViewState["mode"] = "U";
            this.makeEmptyForm("E");
        }
        catch
        { }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            clsButtonNavigation.enableDisable("A");
            ViewState["mode"] = null;
            ViewState["mode"] = "I";
            this.makeEmptyForm("A");
        }
        catch
        { }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnf.Value != string.Empty)
            {
                string query = getDisplayQuery(); ;
                bool recordExist = this.fetchRecord(query);
            }
            else
            {
                this.showLastRecord();
            }
            string str = clsCommon.getString("select count(*) from tblUser where User_Type!='C'");

            if (str != "0")
            {
                clsButtonNavigation.enableDisable("S");
                this.enableDisableNavigateButtons();
                this.makeEmptyForm("S");
            }
            else
            {
                clsButtonNavigation.enableDisable("N");
                this.enableDisableNavigateButtons();
                this.makeEmptyForm("N");

                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }
        catch
        {

        }
    }
}