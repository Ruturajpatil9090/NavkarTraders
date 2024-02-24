using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Mail;


public partial class Report_rptACBalanceList : System.Web.UI.Page
{
    string tblPrefix = string.Empty;
    string tblHead = "";
    string qryCommon = "";
    string f = "../GSReports/DetailBalnce_" + clsGV.user + ".htm";
    string f_Main = "../Report/rptACBalanceList";
    string email = string.Empty;
    IFormatProvider ifrDT = CultureInfo.CreateSpecificCulture("en-GB");


    double Op_debit = 0.00;
    double Op_credit = 0.00;
    double Tran_debit = 0.00;
    double Tran_credit = 0.00;

    double grandOp_Debit = 0.00;
    double grandOp_Credit = 0.00;
    double grandTra_Debit = 0.00;
    double grandTra_Credit = 0.00;
    double grandClo_Debit = 0.00;
    double grandClo_Credit = 0.00;
    protected void Page_Load(object sender, EventArgs e)
    {
        qryCommon = "dbo.qryGledgernew";
        tblPrefix = Session["tblPrefix"].ToString();

        ViewState["fromDt"] = Request.QueryString["fromDt"];
        ViewState["ToDt"] = Request.QueryString["ToDt"];
        ViewState["whr1"] = Request.QueryString["whr1"];

        if (!Page.IsPostBack)
        {
            lblCompany.Text = Session["Company_Name"].ToString();
            lblCompanyAddr.Text = clsGV.CompanyAddress;
            string groupcode = Request.QueryString["whr1"];
            string accode2 = groupcode.Replace(" and Group_Code=", "");
            string groupcodename = clsCommon.getString("select group_Name_E from NT_1_BSGroupMaster where group_Code='"
                + accode2 + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
            lblgroupcodename.Text = groupcodename;
            lblfrmdt.Text = DateTime.Parse(ViewState["fromDt"].ToString(), ifrDT).ToString("dd/MM/yyyy");
            lbltodt.Text = DateTime.Parse(ViewState["ToDt"].ToString(), ifrDT).ToString("dd/MM/yyyy");
            this.bindData();
        }
    }

    #region
    //private void bindData()
    //{
    //    try
    //    {
    //        string qry = "";
    //        DataSet ds = new DataSet();
    //        DataTable dt = new DataTable();
    //        DataTable dtBind = new DataTable();
    //        dtBind.Columns.Add("Ac_Code", typeof(Int32));
    //        dtBind.Columns.Add("Ac_Name", typeof(string));
    //        dtBind.Columns.Add("Op_Debit", typeof(double));
    //        dtBind.Columns.Add("Op_Credit", typeof(double));
    //        dtBind.Columns.Add("Tran_Debit", typeof(double));
    //        dtBind.Columns.Add("Tran_Credit", typeof(double));
    //        dtBind.Columns.Add("Closing_Debit", typeof(double));
    //        dtBind.Columns.Add("Closing_Credit", typeof(double));

    //        if (ViewState["fromDt"] != null && ViewState["ToDt"] != null)
    //        {
    //            string fromdt = DateTime.Parse(ViewState["fromDt"].ToString(), ifrDT).ToString("yyyy/MM/dd");
    //            string todt = DateTime.Parse(ViewState["ToDt"].ToString(), ifrDT).ToString("yyyy/MM/dd");

    //            qry = "select AC_CODE,Ac_Name_E,SUM(case drcr when 'D' then AMOUNT when 'C' then -amount end) as Op_Balance "
    //                + " from qryGledgernew  "
    //                + " where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE<'" + fromdt + "' " + ViewState["whr1"].ToString() + ""
    //                + " group by AC_CODE,Ac_Name_E ";

    //            ds = clsDAL.SimpleQuery(qry);
    //            if (ds != null)
    //            {
    //                if (ds.Tables.Count > 0)
    //                {
    //                    dt = ds.Tables[0];
    //                    if (dt.Rows.Count > 0)
    //                    {
    //                        for (int i = 0; i < dt.Rows.Count; i++)
    //                        {

    //                            double Op_Balance = Convert.ToDouble(dt.Rows[i]["Op_Balance"].ToString());
    //                            Op_debit = 0;
    //                            Tran_debit = 0;
    //                            Op_credit = 0;
    //                            Tran_credit = 0;

    //                            //double Tran_Credit = Convert.ToDouble(dt.Rows[i]["Op_Balance"].ToString());
    //                            //double Closing_Debit = Convert.ToDouble(dt.Rows[i]["Op_Balance"].ToString());
    //                            //double Closing_Credit = Convert.ToDouble(dt.Rows[i]["Op_Balance"].ToString());



    //                            DataRow dr = dtBind.NewRow();
    //                            dr["Ac_Code"] = dt.Rows[i]["AC_CODE"].ToString();
    //                            dr["Ac_Name"] = dt.Rows[i]["Ac_Name_E"].ToString();

    //                            if (Op_Balance > 0)
    //                            {
    //                                dr["Op_Debit"] = Op_Balance;
    //                                dr["Op_Credit"] = 0.00;

    //                                Op_debit = Op_Balance;
    //                                Op_credit = 0.00;
    //                            }
    //                            else if (Op_Balance < 0)
    //                            {
    //                                dr["Op_Debit"] = 0.00;
    //                                dr["Op_Credit"] = -Op_Balance;

    //                                Op_debit = 0.00;
    //                                Op_credit = -Op_Balance;
    //                            }
    //                            else
    //                            {
    //                                dr["Op_Debit"] = 0.00;
    //                                dr["Op_Credit"] = 0.00;

    //                                Op_debit = 0.00;
    //                                Op_credit = 0.00;
    //                            }



    //                            qry = " select SUM(case drcr when 'D' then AMOUNT when 'C' then 0 end) as Tran_Balance"
    //                              + " from qryGledgernew  "
    //                              + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE between '"
    //                              + fromdt + "' and '" + todt + "' and AC_CODE=" + dt.Rows[i]["AC_CODE"].ToString() + ""
    //                              + " group by AC_CODE,Ac_Name_E ";
    //                            string str = clsCommon.getString(qry);

    //                            double Tran_Balance = 0.00;
    //                            if (str != string.Empty)
    //                            {
    //                                Tran_Balance = Convert.ToDouble(str);
    //                            }
    //                            if (Tran_Balance > 0)
    //                            {
    //                                dr["Tran_Debit"] = Tran_Balance;

    //                                Tran_debit = Tran_Balance;
    //                                //Tran_credit = 0.00;
    //                            }

    //                            qry = " select SUM(case drcr when 'C' then AMOUNT when 'D' then 0 end) as Tran_Balance"
    //                              + " from qryGledgernew  "
    //                              + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE between '"
    //                              + fromdt + "' and '" + todt + "' and AC_CODE=" + dt.Rows[i]["AC_CODE"].ToString() + ""
    //                              + " group by AC_CODE,Ac_Name_E ";
    //                            string str1 = clsCommon.getString(qry);


    //                            if (str1 != string.Empty)
    //                            {
    //                                Tran_Balance = Convert.ToDouble(str1);
    //                            }
    //                            if (Tran_Balance > 0)
    //                            {
    //                                // dr["Tran_Debit"] = 0.00;
    //                                dr["Tran_Credit"] = Tran_Balance;

    //                                //Tran_debit = 0.00;
    //                                Tran_credit = Tran_Balance;
    //                            }
    //                            else
    //                            {
    //                                //  dr["Tran_Debit"] = 0.00;
    //                                // dr["Tran_Credit"] = 0.00;

    //                                //Tran_debit = 0.00;
    //                                //ran_credit = 0.00;
    //                            }

    //                            //Calculate Closing Debit/Credit

    //                            double Closing_Balance = 0.00;
    //                            Closing_Balance = (Op_debit + Tran_debit) - (Op_credit + Tran_credit);
    //                            if (Closing_Balance > 0)
    //                            {
    //                                dr["Closing_Debit"] = Closing_Balance;
    //                                dr["Closing_Credit"] = 0.00;
    //                            }
    //                            else if (Closing_Balance < 0)
    //                            {
    //                                dr["Closing_Debit"] = 0.00;
    //                                dr["Closing_Credit"] = -Closing_Balance;
    //                            }
    //                            else
    //                            {
    //                                dr["Closing_Debit"] = 0.00;
    //                                dr["Closing_Credit"] = 0.00;
    //                            }


    //                            dtBind.Rows.Add(dr);

    //                        }//end for loop
    //                        dtl_AccountBalance.DataSource = dtBind;
    //                        dtl_AccountBalance.DataBind();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception eex)
    //    {
    //        Response.Write(eex.Message);
    //    }
    //}
    #endregion
    private void bindData()
    {
        try
        {
            string qry = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dtBind = new DataTable();
            dtBind.Columns.Add("Ac_Code", typeof(Int32));
            dtBind.Columns.Add("Ac_Name", typeof(string));
            dtBind.Columns.Add("Op_Debit", typeof(double));
            dtBind.Columns.Add("Op_Credit", typeof(double));
            dtBind.Columns.Add("Tran_Debit", typeof(double));
            dtBind.Columns.Add("Tran_Credit", typeof(double));
            dtBind.Columns.Add("Closing_Debit", typeof(double));
            dtBind.Columns.Add("Closing_Credit", typeof(double));

            if (ViewState["fromDt"] != null && ViewState["ToDt"] != null)
            {
                string fromdt = DateTime.Parse(ViewState["fromDt"].ToString(), ifrDT).ToString("yyyy/MM/dd");
                string todt = DateTime.Parse(ViewState["ToDt"].ToString(), ifrDT).ToString("yyyy/MM/dd");

                //qry = "select AC_CODE,Ac_Name_E,group_Type"+
                //     " from qryGledgernew  "
                //    + " where   Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " " + ViewState["whr1"].ToString() + ""
                //    + " group by AC_CODE,Ac_Name_E,group_Type ";

                //qry = "select ac_code,SUM(case when DOC_DATE <'" + fromdt + "' then case when DRCR='D' then AMOUNT else 0 end else 0 end ) as opdr ," +
                //    "SUM(case when DOC_DATE <'" + fromdt + "' then case when DRCR='C' then AMOUNT else 0 end else 0 end ) as opcr ," +
                //    "SUM(case when DOC_DATE between '" + fromdt + "' and '" + todt + "' then case when DRCR='D' then AMOUNT else 0 end else 0 end ) as debit, " +
                //    "SUM(case when DOC_DATE between '" + fromdt + "' and '" + todt + "' then case when DRCR='C' then AMOUNT else 0 end else 0 end ) as credit" +
                //    ",Ac_Name_E,group_Type from qryGledgernew  where DOC_DATE between '" + fromdt + "' and '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "" +
                //    " group by ac_code,Ac_Name_E,group_Type";
                qry = "select ac_code,SUM(case when DOC_DATE < '" + fromdt + "' then case when DRCR='D' then AMOUNT  else -amount end else 0 end ) as opbal ," +
                       "SUM(case when DOC_DATE between '" + fromdt + "' and '" + todt + "' then case when DRCR='D' then AMOUNT else 0 end else 0 end ) as debit, " +
                       "SUM(case when DOC_DATE between '" + fromdt + "' and '" + todt + "' then case when DRCR='C' then AMOUNT else 0 end else 0 end ) as credit," +
                       "Ac_Name_E,group_Type from qryGledgernew  where DOC_DATE <= '" + todt + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " group by ac_code,Ac_Name_E,group_Type " +
                       " having SUM(case when DOC_DATE < '" + fromdt + "' then case when DRCR='D' then AMOUNT  else -amount end else 0 end ) <> 0 and " +
                       " SUM(case when DOC_DATE between '" + fromdt + "' and '" + todt + "' then case when DRCR='D' then AMOUNT else 0 end else 0 end ) <>0  and " +
                       " SUM(case when DOC_DATE between '" + fromdt + "' and '" + todt + "' then case when DRCR='C' then AMOUNT else 0 end else 0 end ) <> 0 ";

                ds = clsDAL.SimpleQuery(qry);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            double Tran_Balance = 0.00;
                            double Op_Balance = 0.00;
                            double Closing_Balance = 0.00;
                            //Fill Grid
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                #region opbal
                                //// Assign zero to openig balance when account group type is trading or profit loss
                                //if (dt.Rows[i]["group_Type"].ToString() == "B")
                                //{
                                //    // Calculate opening balance
                                //    //qry = " select SUM(  AMOUNT ) as opbalance"
                                //    //  + " from qryGledgernew  "
                                //    //  + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and " +
                                //    //  " DOC_DATE<'" + fromdt + "' and AC_CODE=" + dt.Rows[i]["AC_CODE"].ToString() + ""
                                //    //  + " group by AC_CODE,Ac_Name_E ";
                                //    //string strop = clsCommon.getString(qry);


                                //    //if (strop != string.Empty)
                                //    //{
                                //    //    Op_Balance = Convert.ToDouble(strop);

                                //    //}

                                //    Op_Balance = 0;
                                   
                                //}
                                //else
                                //{
                                //    Op_Balance = 0;

                                //}
                                //// Assigning opening balance to opening drbit and openng credit lables
                                //if (Op_Balance != 0)
                                //{
                                //    if (Op_Balance > 0)
                                //    {

                                //        Op_debit = Op_Balance;
                                //        Op_credit = 0.00;
                                //    }
                                //    else if (Op_Balance < 0)
                                //    {

                                //        Op_debit = 0.00;
                                //        Op_credit = Op_Balance;
                                //    }
                                //}
                                //else
                                //{
                                //    Op_debit = 0.00;
                                //    Op_credit = 0.00;
                                //}


                                //// Calulate tran debit values 
                                //qry = " select SUM(case drcr when 'D' then AMOUNT when 'C' then 0 end) as Tran_Balance"
                                //  + " from qryGledgernew  "
                                //  + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE between '"
                                //  + fromdt + "' and '" + todt + "' and AC_CODE=" + dt.Rows[i]["AC_CODE"].ToString() + ""
                                //  + " group by AC_CODE,Ac_Name_E ";
                                //string str = clsCommon.getString(qry);


                                //if (str != string.Empty)
                                //{
                                //    Tran_Balance = Convert.ToDouble(str);
                                //    Tran_debit = Tran_Balance;
                                //}


                                //// Calculate tran credit values
                                //qry = " select SUM(case drcr when 'C' then AMOUNT when 'D' then 0 end) as Tran_Balance"
                                //  + " from qryGledgernew  "
                                //  + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and DOC_DATE between '"
                                //  + fromdt + "' and '" + todt + "' and AC_CODE=" + dt.Rows[i]["AC_CODE"].ToString() + ""
                                //  + " group by AC_CODE,Ac_Name_E ";
                                //string str1 = clsCommon.getString(qry);


                                //if (str1 != string.Empty)
                                //{
                                //    Tran_Balance = Convert.ToDouble(str1);
                                //    Tran_credit = Tran_Balance;
                                //}
                                #endregion
                                if (dt.Rows[i]["group_Type"].ToString() == "B")
                                {
                                    Op_Balance = Convert.ToDouble(dt.Rows[i]["opbal"]);
                                   if (Op_Balance > 0)
                                   {

                                       Op_debit = Op_Balance;
                                       Op_credit = 0.00;
                                   }
                                   else if (Op_Balance < 0)
                                   {

                                       Op_debit = 0.00;
                                       Op_credit = Op_Balance;
                                   }
                                    
                                    

                                }
                                else
                                {
                                    Op_debit = 0.00;
                                    Op_credit = 0.00;

                                }


                                // Calulate tran debit values 

                                string str = dt.Rows[i]["debit"].ToString();


                                if (str != string.Empty)
                                {
                                    Tran_Balance = Convert.ToDouble(str);
                                    Tran_debit = Tran_Balance;
                                }


                                // Calculate tran credit values

                                string str1 = dt.Rows[i]["credit"].ToString();


                                if (str1 != string.Empty)
                                {
                                    Tran_Balance = Convert.ToDouble(str1);
                                    Tran_credit = Tran_Balance;
                                }

                                //Calculate Closing Debit/Credit
                                Closing_Balance = Math.Round    ((Op_debit + Tran_debit) - (  -Op_credit + Tran_credit),2);
                                if (Tran_debit == 0 && Tran_credit == 0 && Op_credit == 0 && Op_debit == 0)
                                {
                                }
                                else
                                {
                                    // Assign values to grid
                                    DataRow dr = dtBind.NewRow();
                                    dr["Ac_Code"] = dt.Rows[i]["AC_CODE"].ToString();
                                    dr["Ac_Name"] = dt.Rows[i]["Ac_Name_E"].ToString();
                                    dr["Op_Debit"] = Op_debit;
                                    dr["Op_Credit"] = Math.Abs(Op_credit);
                                    dr["Tran_Debit"] = Tran_debit;
                                    dr["Tran_Credit"] = Tran_credit;
                                    if (Closing_Balance > 0)
                                    {
                                        dr["Closing_Debit"] = Closing_Balance;
                                        dr["Closing_Credit"] = 0.00;
                                    }
                                    else if (Closing_Balance < 0)
                                    {
                                        dr["Closing_Debit"] = 0.00;
                                        dr["Closing_Credit"] = -Closing_Balance;
                                    }
                                    else
                                    {
                                        dr["Closing_Debit"] = 0.00;
                                        dr["Closing_Credit"] = 0.00;
                                    }
                                    dtBind.Rows.Add(dr);
                                    // Initilizing varibles
                                    Op_Balance = 0;
                                    Op_credit = 0;
                                    Op_debit = 0;
                                    Tran_credit = 0;
                                    Tran_debit = 0;
                                    Closing_Balance = 0;
                                }
                            }//end for loop

                            double Opdebit = Convert.ToDouble(dtBind.Compute("SUM(Op_Debit)", string.Empty));
                            grandOp_Debit += Opdebit;
                            lblGrandOpDebit.Text = grandOp_Debit.ToString();

                            double OpCredit = Convert.ToDouble(dtBind.Compute("SUM(Op_Credit)", string.Empty));
                            grandOp_Credit += OpCredit;
                            lblGrandOpCredit.Text = grandOp_Debit.ToString();

                            double TanDebit = Convert.ToDouble(dtBind.Compute("SUM(Tran_Debit)", string.Empty));
                            grandTra_Debit += TanDebit;
                            lblGrandTraDebit.Text = grandTra_Debit.ToString();

                            double TanCredit = Convert.ToDouble(dtBind.Compute("SUM(Tran_Credit)", string.Empty));
                            grandTra_Credit += TanCredit;
                            lblGrandTraCredit.Text = grandTra_Credit.ToString();

                            double CloDebit = Convert.ToDouble(dtBind.Compute("SUM(Closing_Debit)", string.Empty));
                            grandClo_Debit += CloDebit;
                            lblGrandCloDebit.Text = grandClo_Debit.ToString();


                            double CloCredit = Convert.ToDouble(dtBind.Compute("SUM(Closing_Credit)", string.Empty));
                            grandClo_Credit += CloCredit;
                            lblGrandCloCredit.Text = grandClo_Credit.ToString();
                            dtl_AccountBalance.DataSource = dtBind;
                            dtl_AccountBalance.DataBind();
                        }
                    }
                }
            }
        }
        catch (Exception eex)
        {
            Response.Write(eex.Message);
        }
    }
    protected void dtl_AccountBalance_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            Label lblOp_Debit = (Label)e.Item.FindControl("lblOp_Debit");
            Label lblOp_Credit = (Label)e.Item.FindControl("lblOp_Credit");
            Label lblTran_Debit = (Label)e.Item.FindControl("lblTran_Debit");
            Label lblTran_Credit = (Label)e.Item.FindControl("lblTran_Credit");
            Label lblClosing_Debit = (Label)e.Item.FindControl("lblClosing_Debit");
            Label lblClosing_Credit = (Label)e.Item.FindControl("lblClosing_Credit");
            if (lblOp_Debit.Text == "0")
            {
                lblOp_Debit.Text = "";
            }
            if (lblOp_Credit.Text == "0")
            {
                lblOp_Credit.Text = "";
            }
            if (lblTran_Debit.Text == "0")
            {
                lblTran_Debit.Text = "";
            }
            if (lblTran_Credit.Text == "0")
            {
                lblTran_Credit.Text = "";
            }
            if (lblClosing_Debit.Text == "0")
            {
                lblClosing_Debit.Text = "";
            }
            if (lblClosing_Credit.Text == "0")
            {
                lblClosing_Credit.Text = "";
            }
        }
        catch
        {

        }
    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            email = txtEmail.Text.ToString();
            WebClient client = new WebClient();
            Stream data = client.OpenRead(HttpContext.Current.Request.Url.AbsoluteUri);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
            try
            {
                using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        w.WriteLine(s);
                    }
                }
            }
            catch (Exception ee)
            {
                f = f_Main + ".htm";
                using (FileStream fs = new FileStream(Server.MapPath(f), FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        w.WriteLine(s);
                    }
                }
            }
            string mailFrom = Session["EmailId"].ToString();
            string smtpPort = "587";
            string emailPassword = Session["EmailPassword"].ToString();
            MailMessage msg = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            msg.From = new MailAddress(mailFrom);
            msg.To.Add(email);
            msg.Body = "Detailed Balance";
            msg.Attachments.Add(new Attachment(Server.MapPath(f)));
            msg.IsBodyHtml = true;
            //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";
            msg.Subject = "Detailed Balance Report " + " " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
            msg.IsBodyHtml = true;
            if (smtpPort != string.Empty)
            {
                SmtpServer.Port = Convert.ToInt32(smtpPort);
            }
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
            SmtpServer.Send(msg);
        }
        catch (Exception e1)
        {
            //Response.Write("mail err:" + e1);
            Response.Write("<script>alert('Error sending Mail');</script>");
            return;
        }
        Response.Write("<script>alert('Mail sent successfully');</script>");
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        string Name = "Report";

        StringBuilder StrHtmlGenerate = new StringBuilder();
        StringBuilder StrExport = new StringBuilder();
        StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
        StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
        StrExport.Append("<DIV  style='font-size:12px;'>");
        StringWriter sw = new StringWriter();
        HtmlTextWriter tw = new HtmlTextWriter(sw);
        PrintPanel.RenderControl(tw);
        string sim = sw.ToString();
        StrExport.Append(sim);
        StrExport.Append("</div></body></html>");
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + Name + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        string style = @"<style> .textmode { } </style>";
        Response.Write(style);
        Response.Output.Write(StrExport.ToString());
        Response.Flush();
        Response.End();
    }
}