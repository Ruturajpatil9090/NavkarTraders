using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Sugar_Master_pgeGroup_Master : System.Web.UI.Page
{
    int group_code = 0;
    string group_name = string.Empty;
    string html = string.Empty;
    string Category = string.Empty;
    string group_summary = string.Empty;
    string Group_Order = string.Empty;
    string bsid = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        SetFocus(btnAdd);
    }

    public string getData()
    {
        // string cs = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        try
        {
            DataTable dt = new DataTable();

            DataSet ds = clsDAL.SimpleQuery("select group_Code,group_Name_E as Group_Name,group_Summary,group_Type as Category,group_Order,bsid" +
                " from nt_1_bsgroupmaster order by group_Code desc ");
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                group_code = Convert.ToInt32(dt.Rows[i]["group_Code"].ToString());

                group_name = dt.Rows[i]["Group_Name"].ToString();
                group_summary = dt.Rows[i]["group_Summary"].ToString();
                Category = dt.Rows[i]["Category"].ToString();
                Group_Order = dt.Rows[i]["group_Order"].ToString();
                bsid = dt.Rows[i]["bsid"].ToString();
                html += "<tr><td>" + i + "</td><td>" + group_code + "</td><td>" + group_name + "</td><td>" + group_summary + "</td><td>" + Category + "</td><td>" + Group_Order + "</td><td>" + bsid + "</td></tr>";


            }

            return html;
        }
        catch
        {
            return "";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("pgeGroup_Master.aspx");
    }
}