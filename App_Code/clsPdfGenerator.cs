using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using iTextSharp.text.pdf.draw;
using System.Xml;

/// <summary>
/// Summary description for clsPdfGenerator
/// </summary>
public class clsPdfGenerator:IDisposable
{

    public string imageLogoPath = "";
    public string signPath = "";
	public clsPdfGenerator()
	{
        try
        {
            
        }
        catch (Exception)
        {
            imageLogoPath = "";
            signPath = "";
        }
	}

    ~clsPdfGenerator()
    {
    }

    public void Dispose()
    {
        System.GC.SuppressFinalize(this);
    }


    Document doc = new Document(PageSize.A4, 15, 15, 25, 25);
    string reportType = "";
    float[] masterColumnWidths = { 20f, 20f };

    //Set Fonts
    iTextSharp.text.Font fontInvoiceTopHeading = FontFactory.GetFont(BaseFont.COURIER_BOLD, 16, iTextSharp.text.Font.NORMAL);
    iTextSharp.text.Font fontInvoiceNormal = FontFactory.GetFont(BaseFont.COURIER, 10, iTextSharp.text.Font.NORMAL);
    iTextSharp.text.Font fontInvoiceBold = FontFactory.GetFont(BaseFont.COURIER_BOLD, 10, iTextSharp.text.Font.NORMAL);
    iTextSharp.text.Font fontInvoiceBoldSmall = FontFactory.GetFont(BaseFont.COURIER_BOLD, 8, iTextSharp.text.Font.NORMAL);

    iTextSharp.text.Font fontInvoiceTotal = FontFactory.GetFont(BaseFont.COURIER_BOLD, 10, iTextSharp.text.Font.NORMAL);
    iTextSharp.text.Font fontInvoiceSpecialBold = FontFactory.GetFont(BaseFont.COURIER_BOLD, 9, iTextSharp.text.Font.UNDERLINE);

    iTextSharp.text.Font fontInvoiceHeading = FontFactory.GetFont(BaseFont.COURIER_BOLD, 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.WHITE);
    iTextSharp.text.Font fontInvoiceHeadingSmall = FontFactory.GetFont(BaseFont.COURIER_BOLD, 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.WHITE);

    iTextSharp.text.Font fontInvoiceDetails = FontFactory.GetFont(BaseFont.COURIER, 10, iTextSharp.text.Font.NORMAL);
    iTextSharp.text.Font fontInvoiceDetailsSmall = FontFactory.GetFont(BaseFont.COURIER, 8, iTextSharp.text.Font.NORMAL);


    //Parent table
    PdfPTable ParentTable_IndividualTrace;
    iTextSharp.text.Font globalPdfFont = FontFactory.GetFont(BaseFont.COURIER_BOLD, 14, iTextSharp.text.Font.NORMAL);
    float globalTableCellBorder = 0f;
    int globalAlignment = Element.ALIGN_MIDDLE;
    string globalTraceOffenderServiceName = "";


    public void CreateVoucher(string number,string path)
    {
        try
        {

        }
        catch (Exception)
        {
            throw;
        }
    }

}