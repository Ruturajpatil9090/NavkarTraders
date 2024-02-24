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
/// <summary>
/// Summary description for emailsending
/// </summary>
public class emailsending
{
    public emailsending()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void sendmail(string html, string email, string reportName)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter tw = new HtmlTextWriter(sw);
            //pnlMain.RenderControl(tw);
            //string s = sw.ToString();
            //s = s.Replace("../Images", "http://" + clsGV.Website + "/Images");

            byte[] array = Encoding.UTF8.GetBytes(html);
            ms.Write(array, 0, array.Length);
            ms.Seek(0, SeekOrigin.Begin);
            ContentType contentType = new ContentType();
            contentType.MediaType = MediaTypeNames.Application.Octet;
            contentType.Name = ".htm";
            Attachment attachment = new Attachment(ms, contentType);

            string mailFrom = HttpContext.Current.Session["EmailId"].ToString();
            string smtpPort = "587";
            string emailPassword = HttpContext.Current.Session["EmailPassword"].ToString();
            MailMessage msg = new MailMessage();
            //byte[] array = Encoding.ASCII.GetBytes(s);
            //MemoryStream ms = new MemoryStream(File.ReadAllBytes(array.ToString()));

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            SmtpServer.Host = clsGV.Email_Address;
            msg.From = new MailAddress(mailFrom);
            msg.To.Add(email);
            msg.Body = "Way Bill";

            msg.Attachments.Add(attachment);
            //if (msg.Attachments[0].ContentStream == ms)
            //{
            //    long ss = msg.Attachments[0].ContentStream.Length;
            //}
            msg.IsBodyHtml = true;
            //msg.Body = "<html><body>" + txtEmail.Text.ToString() + "</body></html>";

            msg.Subject = "Way Bill " + "ON" + " " + DateTime.Now.ToString("dd/MM/yyyy");
            msg.IsBodyHtml = true;
            if (smtpPort != string.Empty)
            {
                SmtpServer.Port = Convert.ToInt32(smtpPort);
            }
                                SmtpServer.EnableSsl = true;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object k,
                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain,
                System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            SmtpServer.Send(msg);
        }
    }
}