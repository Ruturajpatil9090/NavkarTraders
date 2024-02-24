<%@ Page Title="rpt:All Broker Report" Language="C#" AutoEventWireup="true" CodeFile="rptAllBrokerReport.aspx.cs"
    Inherits="Report_rptAllBrokerReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../JS/emailValidation.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('sugarStock.html', 'st', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body style="font-family:Calibri; width:100%;font-size:12px; text-align:center;" >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 100);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="btnPrint" OnClientClick="PrintPanel();" Text="Print" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
        &nbsp;&nbsp;<asp:Button runat="server" ID="btnSendEmail" Text="Email" OnClick="btnSendEmail_Click"
            OnClientClick="CheckEmail();" Width="79px" />
        &nbsp;<asp:TextBox runat="server" ID="txtEmail" Width="300px" Height="23px"></asp:TextBox>
    </div>
    <div>
        <asp:Panel ID="pnlMain" runat="server" align="center" Font-Names="Calibri">
            <asp:Label ID="lblCompanyName" Width="90%" runat="server" Text="" Style="text-align: center;"
                CssClass="lblName" Font-Size="16px" Font-Bold="true"></asp:Label>
            <br />
            <asp:Label ID="Label15" runat="server" Width="90%" Text="All Broker Report" CssClass="lblName"
                Style="text-align: center;" Font-Size="14px" Font-Bold="true"></asp:Label>
            <br />
            <table width="40%" align="center" cellpadding="1" cellspacing="0">
                <tr>
                    <td colspan="2" align="right">
                        <table align="left">
                            <tr>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text="Date"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="right" style="border-bottom: double 2px black;">
                    </td>
                </tr>
                <tr>
                     <td align="left" style="width: 2%;">
                        <asp:Label ID="Label2" runat="server" Text="Broker Code" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 3%;">
                        <asp:Label ID="lblBroker" runat="server" Text="Broker Name" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 2%;">
                        <asp:Label ID="lblQntl" runat="server" Text="Qntl" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="right" style="border-bottom: double 2px black;">
                    </td>
                </tr>
            </table>
            <table width="40%" align="center" cellpadding="3" cellspacing="0">
                <tr>
                    <td>
                        <asp:DataList runat="server" ID="dtl" Width="100%">
                            <ItemTemplate>
                                <table width="100%" align="left" cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td align="left" style="width: 20%; vertical-align: top;">
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("brok") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 20%; vertical-align: top;">
                                            <asp:Label ID="lblBrokerName" runat="server" Text='<%# Eval("brok_name") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 20%; vertical-align: top;">
                                            <asp:Label ID="lblBrokerQntl" runat="server" Text='<%# Eval("brok_total") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="border-bottom: double 2px black;">
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" align="left" cellpadding="2" cellspacing="0">
                            <tr>
                                <td align="right" style="width: 80%; vertical-align: top;">
                                    Total:-
                                </td>
                                <td align="right" style="width: 20%; vertical-align: top;">
                                    <asp:Label runat="server" ID="lblBrokerQntlTotal" Font-Bold="true" Text="total"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="border-bottom: double 2px black;">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
