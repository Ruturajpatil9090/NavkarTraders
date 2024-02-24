<%@ Page Title="rpt:Trnasport Ac" Language="C#" AutoEventWireup="true" CodeFile="rptTransportAc.aspx.cs" Inherits="Report_rptTransportAc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transport Account</title>
    <link rel="stylesheet" href="../print.css" type="text/css" media="print" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head><link rel="stylesheet" href="../print.css" type="text/css" media="print" />');
            printWindow.document.write('</head><body>');
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
    <div>
        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return PrintPanel();"
            Width="80px" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
        &nbsp;&nbsp;<asp:Button runat="server" ID="btnSendEmail" Text="Email" OnClick="btnSendEmail_Click"
            Width="79px" />
        &nbsp;<asp:TextBox runat="server" ID="txtEmail" Width="300px" Height="23px"></asp:TextBox>
    </div>
    <div>
        <asp:Panel runat="server" ID="pnlMain" CssClass="print">
            <table align="center" width="90%" class="print" style="border-bottom: double 1px gray;
                table-layout: fixed;">
                <tr>
                    <td style="width: 100%;" align="center">
                        <asp:Label ID="lblCompanyName" Width="70%" runat="server" Text="" Style="text-align: center;"
                            CssClass="lblName" Font-Size="16px" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" align="center">
                        <asp:Label ID="Label15" runat="server" Width="70%" Text="Transport A/c Balance" CssClass="lblName"
                            Style="text-align: center;" Font-Size="14px" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table align="center" width="90%" class="print" style="border-bottom: double 1px gray;
                table-layout: fixed;">
                <tr>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="Label1" runat="server" Text="Memo#" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="Label2" runat="server" Text="Date" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 10%;">
                        <asp:Label ID="Label3" runat="server" Text="Party Name" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="Label4" runat="server" Text="Mill" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 4%;">
                        <asp:Label ID="Label5" runat="server" Text="Truck No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="Label6" runat="server" Text="Qntl" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="Label8" runat="server" Text="Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 3%;">
                        <asp:Label ID="Label9" runat="server" Text="Frieght" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 3%;">
                        <asp:Label ID="Label10" runat="server" Text="Paid" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 3%;">
                        <asp:Label ID="Label11" runat="server" Text="Balance" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table align="center" width="90%" class="print">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataList runat="server" ID="dtlist" Width="100%" OnItemDataBound="dtlist_OnitemDatabound">
                            <ItemTemplate>
                                <table width="100%" class="print" align="center" style="table-layout: fixed; background-color: #FFFFCC;">
                                    <tr>
                                        <td align="left" style="width: 14%;">
                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("TransportName") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 4%;">
                                            <asp:Label ID="lblTransportCode" runat="server" Text='<%#Eval("TransportCode") %>'
                                                Font-Bold="true" Visible="false"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 4%;">
                                        </td>
                                        <td align="center" style="width: 3%;">
                                        </td>
                                        <td align="right" style="width: 3%;">
                                        </td>
                                        <td align="right" style="width: 3%;">
                                        </td>
                                        <td align="right" style="width: 3%;">
                                        </td>
                                        <td align="right" style="width: 3%;">
                                            <asp:Label ID="lblTotalBalance" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" class="print">
                                    <tr>
                                        <td style="width: 100%;">
                                            <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                <ItemTemplate>
                                                    <table width="100%" align="center" class="print" style="table-layout: fixed; border-bottom: 1px solid black;">
                                                        <tr>
                                                            <td align="left" style="width: 2%; vertical-align: top;">
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("#") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 2%; vertical-align: top;">
                                                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("dt") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 10%; vertical-align: top;">
                                                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("VoucherBy") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 4%; vertical-align: top;">
                                                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("MillShort") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 4%; vertical-align: top;">
                                                                <asp:Label ID="Label5" runat="server" Text='<%#Eval("lorry") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 3%; vertical-align: top;">
                                                                <asp:Label ID="Label6" runat="server" Text='<%#Eval("Qntl") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 3%; vertical-align: top;">
                                                                <asp:Label ID="Label8" runat="server" Text='<%#Eval("Rate") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="right" style="width: 3%; vertical-align: top;">
                                                                <asp:Label ID="Label9" runat="server" Text='<%#Eval("Freight") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="right" style="width: 3%; vertical-align: top;">
                                                                <asp:Label ID="lblPaid1" runat="server" Text='<%#Eval("Paid1") %>' Font-Bold="false"></asp:Label><br />
                                                                <asp:Label ID="lblPaid2" runat="server" Text='<%#Eval("Paid2") %>' Font-Bold="false"></asp:Label><br />
                                                                <asp:Label ID="lblPaid3" runat="server" Text='<%#Eval("Paid3") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="right" style="width: 3%; vertical-align: top;">
                                                                <asp:Label ID="Label11" runat="server" Text='<%#Eval("balance") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
