using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class pgeloginForm : System.Web.UI.Page
{
    string qry = string.Empty;
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    static WebControl objAsp = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtPassword.Attributes.Add("onmousedown", "return noCopyMouse(event);");
        txtPassword.Attributes.Add("onkeydown", "return noCopyKey(event);");
        //setFocusControl(txtLoginName);
    }

    //#region [setFocusControl]
    //private void setFocusControl(WebControl wc)
    //{
    //    objAsp = wc;
    //    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    //}
    //#endregion

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string groupCode = "0";
            ds = new DataSet();
            qry = "select Group_Code from GroupUser where Login_Name COLLATE Latin1_general_CS_AS='" + txtLoginName.Text + "' and Password COLLATE Latin1_general_CS_AS='" + txtPassword.Text + "' and userType='A'";
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
                        string pass = En.Encrypt(txtPassword.Text);
                        string nm = En.Encrypt(txtLoginName.Text);
                        //connect to another database of specific group
                        /// API Call to pass group code
                        Response.Redirect("Sugar/pgeStartup.aspx?GC=" + groupCode + "&nm=" + nm + "&pd=" + pass + "", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Wrong User Name Or Password!');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    protected void btnCoustomerLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string Ac_Code = string.Empty;
            qry = "select * from tblUser where User_Name COLLATE Latin1_general_CS_AS=" + txtCustomerLoginName.Text + " AND Password COLLATE Latin1_general_CS_AS='" + txtCustomerPassword.Text + "' AND User_Type='C'";
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        Ac_Code = dt.Rows[0]["User_Name"].ToString();
                        Response.Redirect("Customer Page/pgeCustStartup.aspx?Ac_Code=" + Ac_Code + "", false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Wrong User Name or Password!')", true);
                    }
                }
            }
        }
        catch
        {
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/login.aspx", false);
    }
}