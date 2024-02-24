<%@ Page Title="rpt:With Payment Recieve" Language="C#" AutoEventWireup="true" CodeFile="rptWithpaymentRecieve.aspx.cs"
    Inherits="Report_rptWithpaymentRecieve" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Resale Diff To Recieve</title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head><link href="../print.css" rel="stylesheet" type="text/css" media="print" />');
            printWindow.document.write('</head><body>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        function DebitNote() {
            window.open('../Sugar/pgeLocalvoucher.aspx');    //R=Redirected  O=Original
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
        <asp:Panel runat="server" ID="pnlMain" align="center" Font-Names="calibri">
            <asp:Label ID="lblReportName" runat="server" Text="Detail WithPayment Difference"
                Width="100%" CssClass="lblName largsize" Font-Bold="true" Font-Size="Large" Style="text-align: center;
                width: 100%;"></asp:Label>
            <table id="Table1" runat="server" width="80%" align="center" cellspacing="2" style="table-layout: fixed;
                border-top: 1px double black;" class="largsize">
                <tr>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text="Date"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="No"></asp:Label>
                    </td>
                    <td style="width: 3%" align="left">
                        <asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Mill"></asp:Label>
                    </td>
                    <td style="width: 5%" align="left">
                        <asp:Label runat="server" ID="Label9" Font-Bold="true" Text="Party"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Quantal"></asp:Label>
                    </td>
                    <td style="width: 3%" align="center">
                        <asp:Label runat="server" ID="Label4" Font-Bold="true" Text="Mill Rate"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Purc.Rate"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label6" Font-Bold="true" Text="Diff"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label16" Font-Bold="true" Text="Amount"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="Table3" runat="server" align="center" width="80%" class="largsize">
                <tr>
                    <td style="border-bottom: 1px double black;">
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 100%">
                        <asp:Label runat="server" ID="lblPartyCode" Text="Diff From Recieve Report" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 100%">
                        <table id="Table4" runat="server" width="100%" align="left" style="table-layout: fixed;"
                            cellspacing="2" class="largsize">
                            <tr>
                                <td style="width: 2%" align="center">
                                    <asp:Label runat="server" ID="lblDates" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td style="width: 2%" align="center">
                                    <asp:Label runat="server" ID="Label1s" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td style="width: 3%" align="left">
                                    <asp:Label runat="server" ID="Label2s" Font-Bold="false" Text=""></asp:Label>
                                </td>
                                <td style="width: 5%" align="left">
                                    <asp:Label runat="server" ID="Label14" Font-Bold="false" Text=""></asp:Label>
                                </td>
                                <td style="width: 2%" align="center">
                                    <asp:Label runat="server" ID="Label3s" Font-Bold="false" Text=""></asp:Label>
                                </td>
                                <td style="width: 3%" align="center">
                                    <asp:Label runat="server" ID="Label4s" Font-Bold="false" Text=""></asp:Label>
                                </td>
                                <td style="width: 2%; background-color: #FFFFCC;" align="center">
                                    <asp:Label runat="server" ID="Label5s" Font-Bold="true" Text="Net Diff"></asp:Label>
                                </td>
                                <td style="width: 2%; background-color: #FFFFCC;" align="center">
                                    <asp:Label runat="server" ID="lblDiffAmount" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td style="width: 2%; background-color: #FFFFCC;" align="center">
                                    <asp:Label runat="server" ID="lblFinalAmount" Font-Bold="true" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 100%">
                        <asp:DataList runat="server" ID="datalist" Width="100%" OnItemDataBound="DataList_ItemDataBound">
                            <ItemTemplate>
                                <table id="Table1" runat="server" width="100%" align="left" style="table-layout: fixed;"
                                    cellspacing="2" class="largsize">
                                    <tr style="background-color: #CCFFFF;">
                                        <td style="width: 2%" align="center">
                                            <asp:Label runat="server" ID="lblDates" Font-Bold="true" Text='<%#Eval("tdate") %>'></asp:Label>
                                        </td>
                                        <td style="width: 2%" align="center">
                                            <asp:Label runat="server" ID="Label1s" Font-Bold="true" Text='<%#Eval("tno") %>'></asp:Label>
                                        </td>
                                        <td style="width: 3%" align="left">
                                            <asp:Label runat="server" ID="Label2s" Font-Bold="false" Text='<%#Eval("mill") %>'></asp:Label>
                                        </td>
                                        <td style="width: 5%" align="left">
                                            <asp:Label runat="server" ID="Label15" Font-Bold="false" Text='<%#Eval("party") %>'></asp:Label>
                                        </td>
                                        <td style="width: 2%" align="center">
                                            <asp:Label runat="server" ID="Label3s" Font-Bold="false" Text='<%#Eval("quantal") %>'></asp:Label>
                                        </td>
                                        <td style="width: 3%" align="center">
                                            <asp:Label runat="server" ID="Label4s" Font-Bold="false" Text='<%#Eval("millrate") %>'></asp:Label>
                                        </td>
                                        <td style="width: 2%" align="center">
                                            <asp:Label runat="server" ID="Label5s" Font-Bold="false" Text='<%#Eval("purcrate") %>'></asp:Label>
                                        </td>
                                        <td style="width: 2%" align="center">
                                            <asp:Label runat="server" ID="Label6s" Font-Bold="false" Text='<%#Eval("amount") %>'></asp:Label>
                                        </td>
                                        <td style="width: 2%" align="center">
                                            <asp:Label runat="server" ID="Label17" Font-Bold="false" Text='<%#Eval("finalAmt") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-bottom: 1px dashed black;">
                                        <td style="width: 2%" align="center">
                                            <asp:Label runat="server" ID="Label7" Font-Bold="true" Text='<%#Eval("Broker") %>'></asp:Label>
                                        </td>
                                        <td style="width: 2%" align="right">
                                            <asp:Label runat="server" ID="Label8" Font-Bold="true" Text="Voc.No"></asp:Label>
                                        </td>
                                        <td style="width: 4%; border-bottom: 1px double black;" align="left">
                                            <asp:LinkButton runat="server" ID="lnkLV" Font-Bold="false" Style="text-decoration: none;"
                                                OnClick="lnkLV_Click" Text='<%#Eval("voucher") %>'></asp:LinkButton>
                                        </td>
                                        <td style="width: 4%; border-bottom: 1px double black;" align="left">
                                        </td>
                                        <td style="width: 2%" align="right">
                                            <asp:Label runat="server" ID="Label10" Font-Bold="false" Text="Chq.No"></asp:Label>
                                        </td>
                                        <td style="width: 3%; border-bottom: 1px double black;" align="center">
                                            <asp:Label runat="server" ID="Label11" Font-Bold="false" Text=""></asp:Label>
                                        </td>
                                        <td style="width: 2%" align="right">
                                            <asp:Label runat="server" ID="Label12" Font-Bold="false" Text="Date"></asp:Label>
                                        </td>
                                        <td style="width: 2%; border-bottom: 1px double black;" align="center">
                                            <asp:Label runat="server" ID="Label13" Font-Bold="false" Text=""></asp:Label>
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
