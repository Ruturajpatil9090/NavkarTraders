using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Net.Mime;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;


public partial class testpdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnPDf_Click(object sender, EventArgs e)
    {

        try
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            pnlmain.RenderControl(hw);
            string s1 = sw.ToString();
            //StringReader sr = new StringReader(s1.ToString());
            StringReader sr = new StringReader(s1.ToString().Replace("../Images", Server.MapPath("~/Images")));

            Document pdfDoc = new Document(iTextSharp.text.PageSize.A4);
            var writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
        catch
        {
        }
    }

}