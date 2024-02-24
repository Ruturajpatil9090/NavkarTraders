using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class popup : System.Web.UI.Page
{
    DataSet ds = null;
    DataTable dt = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        string ss = hdnKeyCode.Value;
        if (Session["test1"] != null)
        {
            if (Session["test1"].ToString() == "Y")
            {

            }
        }

        if (!Page.IsPostBack)
        {
            string strAPI = "http://Popup.aspx?query=#query#";
            ViewState["query"] = Request.QueryString["query"];
            ViewState["tblName"] = Request.QueryString["tblName"];
            
            // ViewState["page"] = Request.QueryString["page"];

            
            
            this.fillGrid();
        }
    }

    private void fillGrid()          
    {
        try
        {
            ds = new DataSet();
            dt = new DataTable();
            string qry = "";
            if (ViewState["query"] != null)
            {
                qry = ViewState["query"].ToString();
            }
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        //GridView1.SelectedIndex = 1;
                        //GridView1.Rows[0].Style["backgroundColor"] = "#DCFC5C";
                        
                      //  GridView1.Attributes.Add("OnLoad", "javascript:SelectRow('"+gr+"', {0})");
                    }
                }
            }
        }
        catch
        {

        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        // Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex.ToString());

        if (e.Row.RowType == DataControlRowType.DataRow &&
        (e.Row.RowState == DataControlRowState.Normal ||
        e.Row.RowState == DataControlRowState.Alternate))
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onclick"] =string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);


            if (e.Row.RowIndex == 0)
            {
                GridViewRow gr = e.Row;
                GridView1.Attributes["onload"] = string.Format("javascript:SelectRow("+gr+", {0});", gr.RowIndex);
            }


            //if (hdnKeyCode.Value != "Y")
            //{
            e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
            //}
            //else
            //{
            //    e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex.ToString()));
            //}

                if (hdnKeyCode.Value != "0")
                {
                    Session["rowvalue"] = hdnKeyCode.Value;
                }
                

            e.Row.Attributes["onselectstart"] = "javascript:return false;";
         
            e.Row.Attributes["onkeyup"] = "javascript:return selectRow(event);";



           // e.Row.Attributes.Add("onkeydown", Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex.ToString()));
           // e.Row.Attributes.Add("onkeyup", "javascript:if (event.keyCode == 13) {__doPostBack('" + GridView1 + "', 'Select$" + e.Row.RowIndex.ToString() + "'); return false; }");
           // e.Row.Attributes.Add("onkeyup", "javascript:if (event.keyCode == 13) {'"+ Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex.ToString()));
           //   e.Row.Attributes.Add("onkeydown", "javascript:if (event.keyCode == 13) {__doPostBack('" + GridView1 + "', 'Select$" + e.Row.RowIndex.ToString() + "')");


           //try
           //{
           //    if (e.Row.RowType == DataControlRowType.DataRow)
           //    {
           //        e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex.ToString()));
           //    }
           //}
           //catch (Exception exx)
           //{
           //    Response.Write(exx);
           //}
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            // if enter key is pressed (keycode==13) call __doPostBack on grid and with 
            // 1st param = gvChild.UniqueID (Gridviews UniqueID)
            // 2nd param = CommandName=Update$  +  CommandArgument=RowIndex
            //if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate)))
            //{
            //    e.Row.Cells[0].Width = Unit.Pixel(35);
            //    e.Row.Attributes.Add("onkeypress", "javascript:if (event.keyCode == 13) {__doPostBack('" + GridView1.UniqueID + "', 'Update$" + e.Row.RowIndex.ToString() + "'); return false; }");
            //}
        }
        catch
        {
            throw;
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        // int index = int.Parse(gv.SelectedDataKey.Value.ToString());
        GridViewRow myRow = (GridViewRow)GridView1.SelectedRow;
        int RID = myRow.RowIndex;
        string idField = GridView1.Rows[RID].Cells[0].Text;
        // string nmField = GridView1.Rows[RID].Cells[1].Text;
        Session["id"] = idField;
        // Session["nm"] = nmField;
        ////pnlGrid.Visible = false;
        ////pnlShow.Visible = true;
        ////lblSelectedID.Text = Session["id"].ToString();
        ////lblSelectedName.Text = Session["nm"].ToString();        

        System.Web.HttpContext.Current.Response.Write("<script>self.close();</script>");
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
}