<%@ Page Title="rpt:Interest Statement" Language="C#" AutoEventWireup="true" CodeFile="rptInterestStatement.aspx.cs"
    Inherits="Report_rptInterestStatement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Interest Statement</title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=PrintPanel.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body class="container" >');
            printWindow.document.write('<style type = "text/css">thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="left">
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSendEmail" runat="server" Text="Send To Email" CssClass="btnHelp"
            OnClick="btnSendEmail_Click" />&nbsp;&nbsp;Email:<asp:TextBox runat="server" ID="txtEmail"
                Width="300px"></asp:TextBox>
    </div>
    <div>
        <asp:Panel ID="PrintPanel" runat="server" align="center" Font-Names="Calibri">
            <table width="1000px" align="center">
                <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="lblCompany" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label1" runat="server" Text="Interest Statement of :" Font-Bold="true"
                            Font-Size="Medium"></asp:Label>
                        <asp:Label ID="lblParty" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label2" runat="server" Text="From:" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        &nbsp; &nbsp;<asp:Label ID="lblFromDt" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        &nbsp;
                        <asp:Label ID="Label3" runat="server" Text="To:" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        &nbsp;&nbsp;<asp:Label ID="lblToDt" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Interest Rate: &nbsp;
                        <asp:Label ID="lblInterestRate" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#397CBB"
                            HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" EmptyDataText="No Records found"
                            Width="1000px" CellPadding="0" RowStyle-Height="25px" CellSpacing="4" Font-Bold="false"
                            ForeColor="Black" ShowFooter="true" GridLines="Both" Font-Names="Verdana" Font-Size="14px">
                            <Columns>
                                <asp:BoundField DataField="Tran_Type" HeaderText="#" />
                                <asp:BoundField DataField="Date" HeaderText="Date" />
                                <asp:BoundField DataField="Debit_Amount" HeaderText="Debit Amount" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Credit_Amount" HeaderText="Credit Amount" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Bal_DC" HeaderText="D/C" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Days" HeaderText="Days" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Interest" HeaderText="Interest" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Int_DC" HeaderText="D/C" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right" />
                            </Columns>
                            <RowStyle Height="10px" Wrap="true" ForeColor="Black" />
                            <FooterStyle BackColor="Yellow" Font-Bold="true" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
