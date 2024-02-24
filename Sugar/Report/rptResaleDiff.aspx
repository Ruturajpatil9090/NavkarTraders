<%@ Page Title="rpt:Resell Diff" Language="C#" AutoEventWireup="true" CodeFile="rptResaleDiff.aspx.cs" Inherits="Report_rptResaleDiff" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Partywise Resale Diffrence</title>
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
            <asp:Label ID="lblReportName" runat="server" Text="Partywise Resale Difference" Width="100%"
                CssClass="lblName largsize" Font-Bold="true" Font-Size="Large" Style="text-align: center;
                width: 100%;"></asp:Label>
            <table runat="server" width="90%" align="center" cellspacing="2" style="table-layout: fixed;
                border-top: 1px double black; border-bottom: 1px solid black;" class="largsize">
                <tr>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text="Date"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="No"></asp:Label>
                    </td>
                    <td style="width: 4%" align="left">
                        <asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Mill"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Quintal"></asp:Label>
                    </td>
                    <td style="width: 3%" align="center">
                        <asp:Label runat="server" ID="Label4" Font-Bold="true" Text="Mill Rate"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Purc.Rate"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label8" Font-Bold="true" Text="To Pay rate"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label6" Font-Bold="true" Text="To Pay Amt"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label9" Font-Bold="true" Text="to reci. rate"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label7" Font-Bold="true" Text="To Recieve Amt"></asp:Label>
                    </td>
                </tr>
            </table>
            <table runat="server" align="center" width="90%" class="largsize">
                <tr>
                    <td align="center" style="width: 100%">
                        <asp:DataList runat="server" ID="datalist" Width="100%" OnItemDataBound="DataList_ItemDataBound">
                            <ItemTemplate>
                                <table runat="server" width="100%" align="center" style="table-layout: fixed" class="largsize">
                                    <tr>
                                        <td style="width: 30%; border-bottom: 1px solid black;" align="right">
                                            <asp:Label runat="server" ID="lblPartyCode" Text='<%#Eval("partycode") %>' Visible="false"></asp:Label>
                                        </td>
                                        <td style="width: 70%; border-bottom: 1px solid black;" align="left">
                                            <asp:Label runat="server" ID="lblPartyName" Text='<%#Eval("party") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                <ItemTemplate>
                                                    <table id="Table1" runat="server" width="100%" align="left" style="table-layout: fixed;
                                                        background-color: #CCFFFF; border-bottom: 1px dashed black;" cellspacing="2"
                                                        class="largsize">
                                                        <tr>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="lblDates" Font-Bold="true" Text='<%#Eval("tdate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label1s" Font-Bold="true" Text='<%#Eval("tno") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 4%" align="left">
                                                                <asp:Label runat="server" ID="Label2s" Font-Bold="false" Text='<%#Eval("mill") %>'></asp:Label>
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
                                                                <asp:Label runat="server" ID="Label10" Font-Bold="false" Text='<%#Eval("topayrate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label6s" Font-Bold="false" Text='<%#Eval("topay") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label11" Font-Bold="false" Text='<%#Eval("torecieverate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label7s" Font-Bold="false" Text='<%#Eval("torecieve") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8" style="border-bottom: 1px double black;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <table id="Tablsds" runat="server" width="100%" align="right" cellspacing="2" style="background-color: #FFFFCC;
                                                table-layout: fixed;" class="largsize">
                                                <tr>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="lblDates" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="Label1s" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 4%" align="left">
                                                        <asp:Label runat="server" ID="Label2s" Font-Bold="false" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="Label3s" Font-Bold="false" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="center">
                                                        <asp:Label runat="server" ID="Label4s" Font-Bold="false" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="Label5s" Font-Bold="false" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="Label12" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="lblToPayTotal" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="Label13" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="lblToRecieveTotal" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8" style="border-bottom: 1px double black;">
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
