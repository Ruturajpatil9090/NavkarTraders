using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI;

/// <summary>
/// Summary description for clsButtonNavigation
/// </summary>
public class clsButtonNavigation
{
    public clsButtonNavigation()
    {
    }

    public static void demoFun()
    {
    }

    #region [enableDisable]
    public static void enableDisable(string btnAction)
    {
        try
        {
            Page page = (Page)HttpContext.Current.Handler;
            string ss = page.ToString();
            ContentPlaceHolder cp = (ContentPlaceHolder)page.Master.FindControl("ContentPlaceHolder1");
            Button btnAdd = (Button)cp.FindControl("btnAdd");
            Button btnSave = (Button)cp.FindControl("btnSave");
            Button btnDelete = (Button)cp.FindControl("btnDelete");
            Button btnEdit = (Button)cp.FindControl("btnEdit");
            Button btnCancel = (Button)cp.FindControl("btnCancel");


            //Button btnFirst = (Button)cp.FindControl("btnFirst");
            //Button btnPrevious = (Button)cp.FindControl("btnPrevious");
            //Button btnNext = (Button)cp.FindControl("btnNext");
            //Button btnLast = (Button)cp.FindControl("btnLast");
           
            Label lblCreatedBy = (Label)page.Master.FindControl("MasterlblCreatedBy");
            Label lblModifiedBy = (Label)page.Master.FindControl("MasterlblModifiedBy");
            Label lblCreateddate = (Label)page.Master.FindControl("MasterlblCreatedDate");
            Label lblModifieddate = (Label)page.Master.FindControl("MasterlblModifiedDate");

            //btnFirst.Enabled = false;
            //btnPrevious.Enabled = false;
            //btnNext.Enabled = false;
            //btnLast.Enabled = false;

           
            if (btnAction == "N")                   //New
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnCancel.Enabled = false;
                btnSave.Text = "Save";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnAdd.Focus();
            }
            if (btnAction == "A")     //Add New
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnCancel.Enabled = true;
                btnSave.Text = "Save";
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                lblCreatedBy.Visible = false;
                lblModifiedBy.Visible = false;
               // lblCreateddate.Visible = false;
                //lblModifieddate.Visible = false;
            }
            if (btnAction == "S")   //Save
            {
                btnAdd.Enabled = true;
                btnAdd.Focus();
                btnEdit.Enabled = true;
                btnCancel.Enabled = false;
                btnSave.Text = "Save";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                lblCreatedBy.Visible = true;
                lblModifiedBy.Visible = true;
               // lblCreateddate.Visible = true;
                //lblModifieddate.Visible = true;
            }
            if (btnAction == "E")    //Edit
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnCancel.Enabled = true;
                btnSave.Text = "Update";
                //if (HttpContext.Current.Session["enableSave"] != null)
                //{
                //    if (HttpContext.Current.Session["enableSave"].ToString() == "1")
                //    {
                //        btnSave.Enabled = true;
                //    }
                //    else
                //    {
                //        btnSave.Enabled = false;
                //    }
                //}
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
            }
        }
        catch
        {
        }
    }
    private void enableDisableNavigateButtons()
    {

    }
    #endregion

}