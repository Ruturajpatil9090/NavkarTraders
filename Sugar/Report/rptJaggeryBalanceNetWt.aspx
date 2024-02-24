<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptJaggeryBalanceNetWt.aspx.cs" Inherits="Sugar_Report_rptJaggeryBalanceNetWt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Closing Stock Jaggary</title>
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
    <script src="../Scripts/jquery-1.11.2.min.js" type="text/javascript"></script>
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
        <%--  &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />--%>
    </div>
    <div>
        <asp:Panel runat="server" ID="pnlMain" align="center" Font-Names="calibri" CssClass="largsize">
            <table width="80%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;"
                class="print">
                <tr>
                    <td style="width: 100%;" align="center">
                        <asp:Label runat="server" ID="lblCompanyName" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" align="center">
                        <asp:Label runat="server" ID="Label11" Font-Bold="true" Text="Closing Stock Jaggary"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" align="center">
                        <asp:Label runat="server" ID="lblfrotodate" Font-Bold="true" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="80%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                border-bottom: solid 2px black; border-top: solid 2px black;" class="print">
                <tr>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="lblParty" runat="server" Text="Aw No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="Label1" runat="server" Text="Date" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 2%;">
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="lbldt" runat="server" Text="Item Name" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 10%;">
                        <asp:Label ID="Label5" runat="server" Text="Supplier Name" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 3%;">
                        <asp:Label ID="Label8" runat="server" Text="P.Qty" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 3%;">
                        <asp:Label ID="Label2" runat="server" Text="P.Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 3%;">
                        <asp:Label ID="Label7" runat="server" Text="P.NetWt" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 3%;">
                        <asp:Label ID="Label9" runat="server" Text="B.Qty" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 3%;">
                        <asp:Label ID="Label10" runat="server" Text="B.NetWt" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 3%;">
                        <asp:Label ID="Label17" runat="server" Text="Balance" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="80%" align="center" cellpadding="1" cellspacing="0" class="print">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataList ID="dtl" runat="server" Width="100%" CellSpacing="4" OnItemDataBound="dtl_OnItemDataBound">
                            <ItemTemplate>
                                <table align="center" width="100%" cellpadding="1" cellspacing="0" style="table-layout: fixed;"
                                    class="print">
                                    <tr>
                                        <td align="left" style="width: 2%;">
                                            <asp:Label ID="lblaccode" runat="server" Text='<%#Eval("doc_no") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 2%;">
                                            <asp:Label ID="lblsupplier" runat="server" Text='<%#Eval("DOC_DATE") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 2%;">
                                        </td>
                                        <td align="left" style="width: 5%;">
                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("itemname") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 10%;">
                                            <asp:Label ID="lbltrantype" runat="server" Text='<%#Eval("supplierName") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 3%;">
                                            <asp:Label ID="lblmillname" runat="server" Text='<%#Eval("awakqty") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 3%;">
                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("Purc_Rate") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 3%;">
                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("Net_Wt") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 3%;">
                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("balnceqty") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 3%;">
                                            <asp:Label ID="lblscost" runat="server" Text='<%#Eval("balNetweight") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 3%;">
                                            <asp:Label ID="Label18" runat="server" Text='<%#Eval("bal") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;">
                                            <table align="center" width="100%" cellpadding="1" cellspacing="0" style="table-layout: fixed;"
                                                class="print millnames">
                                                <tr>
                                                    <td style="width: 100%;" class="tddetails">
                                                        <asp:DataList runat="server" ID="dtlDetails" Width="100%" Visible="true">
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 90%;">
                        <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                            border-bottom: solid 1px black; border-top: solid 1px black;" class="print">
                            <tr>
                                <td align="right" style="width: 2%;">
                                    <asp:Label ID="Label12" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 2%;">
                                    <asp:Label ID="Label13" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 2%;">
                                </td>
                                <td align="right" style="width: 5%;">
                                    <asp:Label ID="Label15" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 10%;">
                                    <asp:Label ID="Label14" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 3%;">
                                    <asp:Label ID="lblpqty" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 3%;">
                                    <asp:Label ID="Label16" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 3%;">
                                    <asp:Label ID="lblpnetwt" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 3%;">
                                    <asp:Label ID="lblbqty" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 3%;">
                                    <asp:Label ID="lblbnetwt" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 3%;">
                                    <asp:Label ID="lblbal" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
