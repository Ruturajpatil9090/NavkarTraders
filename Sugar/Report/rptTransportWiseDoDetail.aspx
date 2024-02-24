<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTransportWiseDoDetail.aspx.cs" Inherits="Sugar_Report_rptTransportWiseDoDetail" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
          
            <CR:CrystalReportViewer ID="CryTransportwiseDoData" runat="server" AutoDataBind="true" />
        </div>
    </form>
</body>
</html>

