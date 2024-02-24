<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pgeSaleBill_Print1.aspx.cs" Inherits="Sugar_Report_pgeSaleBill_Print1" %>

<%@ Register Assembly="CrystalDecisions.Web,Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.2.61/jspdf.min.js"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <title></title>
    <script type="text/javascript">
        function Print() {
            debugger;
            var dvReport = document.getElementById("printReady");
            for (var i = 0; i < 8; i++) {
                if (i % 2 == 0) {
                    var frame1 = printReady.getElementsByTagName("iframe")[i];
                    if (navigator.appName.indexOf("Chrome") != -1) {
                        frame1.name = frame1.id;
                        window.frames[frame1.id].focus();
                        window.frames[frame1.id].print();
                    }
                    else {
                        var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                        frameDoc.print();
                    }
                }
            }

        }

        function Print1() {
            debugger;
            var dvReport = document.getElementById("Div1");
            for (var i = 0; i < 8; i++) {
                if (i % 2 == 0) {
                    var frame1 = printReady.getElementsByTagName("iframe")[i];
                    if (navigator.appName.indexOf("Chrome") != -1) {
                        frame1.name = frame1.id;
                        window.frames[frame1.id].focus();
                        window.frames[frame1.id].print();
                    }
                    else {
                        var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                        frameDoc.print();
                    }
                }
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="printReady">
            <asp:Button ID="btnprintdialog" Text="Print" runat="server" OnClientClick="return Print()" />
            <asp:Button ID="btnPDF" runat="server" Text="Open PDF" Width="80px" OnClientClick="return CheckEmail();"
                OnClick="btnPDF_Click" />
            <asp:Button ID="btnMail" runat="server" Text="Mail PDF" Width="80px" OnClientClick="return CheckEmail();"
                OnClick="btnMail_Click" />
            <asp:TextBox runat="server" ID="txtEmail" Width="300px"></asp:TextBox>

            <CR:CrystalReportViewer ID="crySaleBill_Print1" runat="server" AutoDataBind="false" />
        </div>

    </form>
</body>
</html>
