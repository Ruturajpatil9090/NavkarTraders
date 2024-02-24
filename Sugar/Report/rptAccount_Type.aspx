<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptAccount_Type.aspx.cs" Inherits="Sugar_Report_rptAccount_Type" %>

<%@ Register Assembly="CrystalDecisions.Web,Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="btnPDF" runat="server" Text="Open PDF" Width="80px" OnClientClick="return CheckEmail();"
                OnClick="btnPDF_Click" />
            &nbsp;
        <asp:Button ID="btnMail" runat="server" Text="Mail PDF" Width="80px" OnClientClick="return CheckEmail();"
            OnClick="btnMail_Click" />
            &nbsp;
        <asp:TextBox runat="server" ID="txtEmail" Width="300px"></asp:TextBox>

            &nbsp;&nbsp;

           <CR:CrystalReportViewer ID="CryAccountType" runat="server" AutoDataBind="true" ShowAllPageIds="True" HasPageNavigationButtons="True" BestFitPage="False" Width="1500px" />
        </div>
    </form>
</body>
</html>
