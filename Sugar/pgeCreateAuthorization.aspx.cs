using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_pgeCreateAuthorization : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string qry = string.Empty;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        user = Session["user"].ToString();
        if (!Page.IsPostBack)
        {
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                this.FillTree();
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
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
    public void AddNodeAndChildNodesToList(TreeNode node)
    {
        #region Child nodes
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
                            string Group_ID = clsCommon.getString("Select MAX(Group_ID) from " + tblPrefix + "AuthorizationGroup");
                            string strRef = string.Empty;
                            DataSet ds1 = new DataSet();
                            obj.flag = 1;
                            obj.tableName = tblPrefix + "Authorization";
                            obj.columnNm = "PageID,Group_ID,IsAuthenticate";
                            obj.values = "'" + pageid + "','" + Convert.ToInt32(Group_ID) + "','" + IsAuthenticate + "'";
                            ds1 = obj.insertAccountMaster(ref strRef);
                        }
                    }
                }
                AddNodeAndChildNodesToList(n);
            }
        }
        #endregion
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
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
                string qry = "SELECT Name,Under_id FROM " + tblPrefix + "ApplicationPages WHERE Under_id = " + child.Value + "";
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

    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        string str = "";
        string Group_Name = txtGroupName.Text;
        using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
        {
            obj.flag = 1;
            obj.tableName = tblPrefix + "AuthorizationGroup";
            obj.columnNm = "Group_Name,Created_By";
            obj.values = "'" + Group_Name + "','" + user + "'";
            obj.insertAccountMaster(ref str);

            foreach (TreeNode node in TreeSelectPages.Nodes)
            {
                string text = node.Text.ToString();
                AddNodeAndChildNodesToList(node);
            }
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('User Successfully Created!');", true);
        txtGroupName.Text = string.Empty;

    }
    protected void btnCancelUser_Click(object sender, EventArgs e)
    {

    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        pnlTree.Style["display"] = "none";
    }
}