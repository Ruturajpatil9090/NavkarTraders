<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTDSReport.aspx.cs" Inherits="Sugar_Report_rptTDSReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Jaggary Stock Report</title>
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
                        <asp:Label runat="server" ID="Label11" Font-Bold="true" Text="TDS Report"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" align="center">
                        <asp:Label runat="server" ID="lblfrotodate" Font-Bold="true" Text="TDS Report"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="80%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                 border-bottom: solid 2px black;border-top: solid 2px black;" class="print">
                <tr>
                    <td align="left" style="width: 7%;">
                        <asp:Label ID="lblParty" runat="server" Text="Supplier Name" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="Label1" runat="server" Text="PAN No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 2%;">
                        <asp:Label ID="lblpnm" runat="server" Text="Purchasevalue" Font-Bold="true"></asp:Label>
                    </td>
                    
                    <td align="right" style="width: 2%;">
                        <asp:Label ID="Label5" runat="server" Text="Adat" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 2%;">
                        <asp:Label ID="Label8" runat="server" Text="TDS Amt" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="80%" align="center" cellpadding="1" cellspacing="0" class="print">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataList ID="dtl" runat="server" Width="100%" CellSpacing="4" OnItemDataBound="dtl_OnItemDataBound">
                            <ItemTemplate>
                                <table align="center" width="100%" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                                    " class="print">
                                    <tr>
                                     <td align="left" style="width: 0%;">
                                            <asp:Label ID="lblaccode" runat="server" Text='<%#Eval("AC_CODE") %>' Font-Bold="false" Visible="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 7%;">
                                            <asp:Label ID="lblsupplier" runat="server" Text='<%#Eval("supplier") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 2%;">
                                            <asp:Label ID="lblpartypan" runat="server" Text="" Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 2%;">
                                            <asp:Label ID="lbltrantype" runat="server" Text='<%#Eval("Item_vaue") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 2%;">
                                            <asp:Label ID="lblmillname" runat="server" Text='<%#Eval("adat") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 2%;">
                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("tds") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;">
                                            <table align="center" width="100%" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                                                " class="print millnames">
                                                <tr>
                                                    <td style="width: 100%;" class="tddetails">
                                                        <asp:DataList runat="server" ID="dtlDetails" Width="100%" Visible="true">
                                                            <ItemTemplate>
                                                                <%--<table align="center" width="100%" cellpadding="1" cellspacing="2" style="table-layout: fixed;"
                                                                    class="print">
                                                                    <tr>
                                                                       
                                                                        <td align="center" style="width: 2%;">
                                                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("supplier") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="center" style="width: 2%;">
                                                                            <asp:Label ID="lbltrantype" runat="server" Text='<%#Eval("Item_vaue") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="center" style="width: 2%;">
                                                                            <asp:Label ID="lblmillname" runat="server" Text='<%#Eval("tds") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="center" style="width: 2%;">
                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("adat") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>--%>
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
                                    <asp:Label ID="Label2" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 7%;">
                                    <asp:Label ID="Label14" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 2%;">
                                    <asp:Label ID="lblpurchasevalue" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 2%;">
                                    <asp:Label ID="lbladat" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 2%;">
                                    <asp:Label ID="lbltds" runat="server" Text="" Font-Bold="true"></asp:Label>
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
