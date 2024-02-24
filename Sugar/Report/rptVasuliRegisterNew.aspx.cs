using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Report_rptVasuliRegisterNew : System.Web.UI.Page
{
    string fromDate = string.Empty;
    string toDate = string.Empty;
    string qry = string.Empty;
    DataSet ds;
    DataTable dt;
    DataSet databind;
    DataTable SecoundQry;

    string tblPrefix = string.Empty;
    string Branch_Code = string.Empty;
    double totalAmt = 0.00;
    string isAuthenticate = string.Empty;
    string user = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        tblPrefix = Session["tblPrefix"].ToString();
        fromDate = Request.QueryString["FromDT"].ToString();
        toDate = Request.QueryString["ToDt"].ToString();
        Branch_Code = Request.QueryString["Branch_Code"];
        if (!Page.IsPostBack)
        {


            user = Session["user"].ToString();
            isAuthenticate = Security.Authenticate(tblPrefix, user);
            string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
            if (isAuthenticate == "1" || User_Type == "A")
            {
                lblCompanyName.Text = Session["Company_Name"].ToString();
                this.BindList();
            }
            else
            {
                Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
            }
        }
    }

    private void BindList()
    {
        try
        {
            using (clsDataProvider obj = new clsDataProvider())
            {

                qry = "Select distinct(transport),transportname from  qrydohead where tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  and doc_date between '" + fromDate + "' and '" + toDate + "' and vasuli_amount1!=0 and purc_no!=0 order by transportname asc";

                ds = new DataSet();
                ds = obj.GetDataSet(qry);
                //ds = clsDAL.SimpleQuery(qry);

                qry = "Select doc_no,doc_dateConverted as doc_date,millShortName,quantal,voucherbyshortname as PartyName,truck_no,vasuli_amount1,transport from qrydohead " +
                     " where vasuli_amount1!=0 and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and tran_type='DO' and doc_date between '" + fromDate + "' and '" + toDate + "' and purc_no!=0";

                databind = obj.GetDataSet(qry);
                SecoundQry = databind.Tables[0];

                if (ds != null)
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Transport_Code", typeof(Int32)));
                    dt.Columns.Add(new DataColumn("Transport_Name", typeof(string)));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            string transcode = ds.Tables[0].Rows[i]["transport"].ToString();
                            dr["transport_Code"] = transcode;
                            string transport_Name = clsCommon.getString("Select Ac_Name_E from " + tblPrefix + "AccountMaster where Ac_code=" + transcode + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_type='T'");
                            dr["Transport_Name"] = transport_Name;
                            dt.Rows.Add(dr);
                        }

                        if (dt.Rows.Count > 0)
                        {

                            lblTotalVasuli.Text = clsCommon.getString("select isnull((vasuli_amount1),0) from qrydohead where tran_type='DO' and transport!=0 and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and doc_date between '" + fromDate + "' and '" + toDate + "' and purc_no!=0 ");


                            dtl.DataSource = dt;
                            dtl.DataBind();
                        }
                        else
                        {

                            dtl.DataSource = null;
                            dtl.DataBind();
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void DataList_OnItemDataBound(object sender, DataListItemEventArgs e)
    {
        using (clsDataProvider obj = new clsDataProvider())
        {
            DataList dtlDetails = (DataList)e.Item.FindControl("dtlDetails");
            Label lblTransportcode = (Label)e.Item.FindControl("lblTrasportCode");
            string trans_code = lblTransportcode.Text.ToString();

            if (trans_code != string.Empty)
            {
                DataView view = new DataView(SecoundQry, "transport='" + lblTransportcode.Text + "'", "transport", DataViewRowState.CurrentRows);
                DataTable selectedvalue = view.ToTable(true, "doc_no", "doc_date", "millShortName", "quantal", "PartyName", "truck_no", "vasuli_amount1", "transport");




                if (selectedvalue.Rows.Count > 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add(new DataColumn("doc_no", typeof(string)));
                    dt.Columns.Add(new DataColumn("doc_date", typeof(string)));
                    dt.Columns.Add(new DataColumn("millShortName", typeof(string)));
                    dt.Columns.Add(new DataColumn("quantal", typeof(double)));
                    dt.Columns.Add(new DataColumn("PartyName", typeof(string)));
                    dt.Columns.Add(new DataColumn("truck_no", typeof(string)));
                    dt.Columns.Add(new DataColumn("vasuli_amount1", typeof(double)));

                    if (selectedvalue.Rows.Count > 0)
                    {
                        for (int i = 0; i < selectedvalue.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["doc_no"] = selectedvalue.Rows[i]["doc_no"].ToString();
                            dr["doc_date"] = selectedvalue.Rows[i]["doc_date"].ToString();
                            dr["millShortName"] = selectedvalue.Rows[i]["millShortName"].ToString();
                            dr["quantal"] = selectedvalue.Rows[i]["quantal"].ToString();
                            dr["PartyName"] = selectedvalue.Rows[i]["PartyName"].ToString();
                            dr["truck_no"] = selectedvalue.Rows[i]["truck_no"].ToString();
                            dr["vasuli_amount1"] = selectedvalue.Rows[i]["vasuli_amount1"].ToString();
                            dt.Rows.Add(dr);
                        }
                        Label lblVasuliTotal = (Label)e.Item.FindControl("lblVasuliTotal");

                        if (dt.Rows.Count > 0)
                        {
                            lblVasuliTotal.Text = Convert.ToString(dt.Compute("SUM(vasuli_amount1) ", string.Empty));

                            dtlDetails.DataSource = dt;
                            dtlDetails.DataBind();
                        }
                        else
                        {
                            dtlDetails.DataSource = null;
                            dtlDetails.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            List<string> doNoList = new List<string>();
            double totalAmt = 0.00;
            for (int i = 0; i < dtl.Items.Count; i++)
            {
                DataList dtlDetails = (DataList)dtl.Items[i].FindControl("dtlDetails");

                for (int j = 0; j < dtlDetails.Items.Count; j++)
                {
                    CheckBox chkRecieve = dtlDetails.Items[j].FindControl("chkRecieve") as CheckBox;
                    Label lbldtlrefno = (Label)dtlDetails.Items[j].FindControl("lbldtlrefno");
                    Label lbldtAmount = (Label)dtlDetails.Items[j].FindControl("lbldtAmount");
                    if (chkRecieve.Checked == true)
                    {
                        string dono = lbldtlrefno.Text;
                        doNoList.Add(dono);
                        double amt = Convert.ToDouble(lbldtAmount.Text);
                        totalAmt += amt;
                        count++;
                    }
                }
            }
            if (count != 0)
            {
                string allDo = String.Join(",", doNoList);
                string qry = "Update " + tblPrefix + "deliveryorder SET vasuli_rate1=0.00,vasuli_amount1=0.00 where doc_no in (" + allDo + ") and tran_type='DO' and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " and purc_no!=0";
                ds = new DataSet();
                ds = clsDAL.SimpleQuery(qry);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfule Updated!');", true);
                this.BindList();
                lblUpdatedAmt.Text = "Total Updated Amount Is " + totalAmt.ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please Select Atleast One Checkbox!');", true);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void chkRecieve_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            for (int i = 0; i < dtl.Items.Count; i++)
            {
                DataList dtlDetails = (DataList)dtl.Items[i].FindControl("dtlDetails");
                for (int j = 0; j < dtlDetails.Items.Count; j++)
                {
                    CheckBox chkRecieve = dtlDetails.Items[j].FindControl("chkRecieve") as CheckBox;
                    Label lbldtlrefno = (Label)dtlDetails.Items[j].FindControl("lbldtlrefno");
                    Label lbldtAmount = (Label)dtlDetails.Items[j].FindControl("lbldtAmount");
                    if (chkRecieve.Checked)
                    {
                        double amt = Convert.ToDouble(lbldtAmount.Text);
                        RecievedAmount(amt);
                        count++;
                    }
                }
            }

            if (count == 0)
            {
                lblUpdatedAmt.Text = "";
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

    private void RecievedAmount(double amt)
    {
        totalAmt += amt;
        lblUpdatedAmt.Text = "Total Recieved Amount Is: " + totalAmt.ToString();
    }
}