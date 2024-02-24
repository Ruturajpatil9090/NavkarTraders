using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AccountContacts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            this.fillContactsGrid();
        }
    }

    private void fillContactsGrid()
    {
        try
        {
            string qry = "";
            qry = "select ROW_NUMBER() OVER(ORDER BY Ac_Code,Company_Code)  as 'RowNumber',PersonName, MobileNo,EmailID from AccountContacts ";
            DataSet ds = new DataSet();
            ds = clsDAL.SimpleQuery(qry);
            DataTable dt = new DataTable();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dt.Rows.Add(dr);
                        ViewState["CurrentTable"] = dt;
                        grdContacts.DataSource = dt;
                        grdContacts.DataBind();
                    }
                    else
                    {
                        SetInitialRow();
                    }
                }
                else
                {
                    SetInitialRow();
                }
            }
            else
            {
                SetInitialRow();
            }
        }
        catch
        {

        }
    }

    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("PersonName", typeof(string)));
        dt.Columns.Add(new DataColumn("MobileNo", typeof(string)));
        dt.Columns.Add(new DataColumn("EmailID", typeof(string)));

        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["PersonName"] = string.Empty;
        dr["MobileNo"] = string.Empty;
        dr["EmailID"] = string.Empty;

        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        grdContacts.DataSource = dt;
        grdContacts.DataBind();
    }

    private void AddNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox box1 = (TextBox)grdContacts.Rows[rowIndex].Cells[1].FindControl("txtName");
                    TextBox box2 = (TextBox)grdContacts.Rows[rowIndex].Cells[2].FindControl("txtMobile");
                    TextBox box3 = (TextBox)grdContacts.Rows[rowIndex].Cells[3].FindControl("txtEmail");


                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["PersonName"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["MobileNo"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["EmailID"] = box3.Text;


                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                grdContacts.DataSource = dtCurrentTable;
                grdContacts.DataBind();

                grdContacts.Rows[grdContacts.Rows.Count - 1].Cells[0].FindControl("PersonName").Focus();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)grdContacts.Rows[rowIndex].Cells[1].FindControl("txtName");
                    TextBox box2 = (TextBox)grdContacts.Rows[rowIndex].Cells[2].FindControl("txtMobile");
                    TextBox box3 = (TextBox)grdContacts.Rows[rowIndex].Cells[3].FindControl("txtEmail");




                    box1.Text = dt.Rows[i]["PersonName"].ToString();
                    box2.Text = dt.Rows[i]["MobileNo"].ToString();
                    box3.Text = dt.Rows[i]["EmailID"].ToString();


                    rowIndex++;
                }
            }
        }
    }
}