<%@ Page Title="rpt:Multiple Ledger" Language="C#" AutoEventWireup="true" CodeFile="rptmultipleLedger1.aspx.cs"
    Inherits="Report_rptmultipleLedger1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multiple Ledger</title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=PrintPanel.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body style="font-family:Calibri;font-size:12px;width:1100px; text-align:center;">');
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
    </div>
    <div align="center">
        <asp:Panel ID="PrintPanel" runat="server" align="center" Font-Names="Calibri">
            <asp:Label ID="Label4" runat="server" Text="Balance Sheet Group:" Font-Bold="true"
                Font-Size="Large" Style="text-align: left;"></asp:Label>
            <asp:Label ID="lblGroup" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
            <asp:DataList ID="dtl_Group" runat="server" OnItemDataBound="dtl_Group_ItemDataBound">
                <ItemTemplate>
                    <table width="1000px" align="center" style="page-break-before: always;">
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Label ID="lblCompany" runat="server" Text="" Font-Bold="true"
                                    Font-Size="Large"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label1" runat="server" Text="Account Statement of :" Font-Bold="true"
                                    Font-Size="Medium"></asp:Label>
                                <asp:Label ID="lblPartyCode" runat="server" Text='<%#Eval("Ac_Code") %>' Font-Bold="true"
                                    Font-Size="Medium">-></asp:Label>
                                <asp:Label ID="lblParty" runat="server" Text='<%#Eval("Ac_Name_E") %>' Font-Bold="true"
                                    Font-Size="Medium"></asp:Label>
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
                            <td align="center" colspan="2">
                                <asp:Label ID="lblAcCode" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#397CBB"
                        HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" EmptyDataText="No Records found"
                        HeaderStyle-BorderColor="#397CBB" HeaderStyle-BorderWidth="1px" HeaderStyle-BorderStyle="Solid"
                        FooterStyle-BorderColor="#397CBB" FooterStyle-BorderStyle="Solid" FooterStyle-BorderWidth="1px"
                        Width="1000px" CellPadding="0" CellSpacing="0" Font-Bold="false" ForeColor="Black"
                        ShowFooter="true" Font-Names="Verdana" Font-Size="12px" OnRowDataBound="grdDetail_RowDataBound"
                        GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="TranType" HeaderText="TranType" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-CssClass="thead" />
                            <asp:BoundField DataField="DocNo" HeaderText="DocNo" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-CssClass="thead" />
                            <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-CssClass="thead" />
                            <asp:BoundField DataField="Narration" HeaderText="Narration" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-CssClass="thead" />
                            <asp:BoundField DataField="Debit" HeaderText="Debit" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead" />
                            <asp:BoundField DataField="Credit" HeaderText="Credit" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead" />
                            <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead" />
                            <asp:BoundField DataField="DrCr" HeaderText="DrCr" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-CssClass="thead" />
                        </Columns>
                        <RowStyle Height="10px" Wrap="true" ForeColor="Black" />
                        <FooterStyle BackColor="Yellow" Font-Bold="true" />
                    </asp:GridView>
                </ItemTemplate>
            </asp:DataList>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
