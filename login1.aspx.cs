using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class login1 : System.Web.UI.Page
{
    string qry = string.Empty;
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    static WebControl objAsp = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        username.Attributes.Add("onmousedown", "return noCopyMouse(event);");
        password.Attributes.Add("onkeydown", "return noCopyKey(event);");
    }

    protected void Login(object sender, EventArgs e)
    {
        try
        {
            if (username.Text != "User Name" || password.Text != "Password")
            {
                string groupCode = "0";
                ds = new DataSet();
                qry = "select Group_Code from GroupUser where Login_Name ='" + username.Text + "' and Password ='" + password.Text + "' and userType='A'";
                ds = clsDAL.SimpleQuery(qry);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            //Response.Write("<script>alert('In');</script>");
                            groupCode = dt.Rows[0]["Group_Code"].ToString();
                            EncryptPass En = new EncryptPass();
                            string pass = En.Encrypt(username.Text);
                            string nm = En.Encrypt(password.Text);
                            //connect to another database of specific group
                            /// API Call to pass group code
                            Response.Redirect("Sugar/pgeStartup.aspx?GC=" + groupCode + "&nm=" + nm + "&pd=" + pass + "", false);
                            username.Text = "User Name";
                            password.Text = "Password";
                        }
                        else
                        {

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Wrong User Name Or Password!');", true);
                        }
                    }
                    else
                    {
                        username.Focus();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Wrong User Name Or Password!');", true);
                    }
                }
                else
                {
                    username.Focus();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Wrong User Name Or Password!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
}