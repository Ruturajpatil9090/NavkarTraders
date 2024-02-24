<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptPurchaseRegisterTDS.aspx.cs" Inherits="Sugar_Report_rptPurchaseRegisterTDS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Jaggary Purchase Register</title>
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
                        <asp:Label runat="server" ID="Label11" Font-Bold="true" Text="Jaggary Purchase Register"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" align="center">
                        <asp:Label runat="server" ID="lbldaterange" Font-Bold="true" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="90%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                border-bottom: solid 2px black; border-top: solid 2px black;" class="print">
                <tr>
                    <td align="center" style="width: 1%;">
                        <asp:Label ID="Label4" runat="server" Text="Awak No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 1%;">
                        <asp:Label ID="Label22" runat="server" Text="Bill No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="Label18" runat="server" Text="Date" Font-Bold="true"></asp:Label>
                    </td>
                       <td align="center" style="width: 2%;">
                        <asp:Label ID="Label10" runat="server" Text="Account Code" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 6%;">
                        <asp:Label ID="lblParty" runat="server" Text="Supplier Name" Font-Bold="true"></asp:Label>
                    </td>
                  <%--  <td align="center" style="width: 1%;">
                        <asp:Label ID="lblMemono" runat="server" Text="NetQty" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 1%;">
                        <asp:Label ID="lblpnm" runat="server" Text="Item value" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 1%;">
                        <asp:Label ID="Label1" runat="server" Text="M.cess" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 1%;">
                        <asp:Label ID="Label2" runat="server" Text="S.cost" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 1%;">
                        <asp:Label ID="Label12" runat="server" Text="Levi" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 1%;">
                        <asp:Label ID="Label5" runat="server" Text="Adat" Font-Bold="true"></asp:Label>
                    </td>--%>
                 
                    <td align="right" style="width: 1%;">
                        <asp:Label ID="Label8" runat="server" Text="TDS Amt" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 2%;">
                        <asp:Label ID="Label16" runat="server" Text="Payable Amnt" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="90%" align="center" cellpadding="1" cellspacing="0" class="print">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataList ID="dtl" runat="server" Width="100%" CellSpacing="4" OnItemDataBound="dtl_OnItemDataBound">
                            <ItemTemplate>
                                <table align="center" width="100%" cellpadding="1" cellspacing="0" style="table-layout: fixed;"
                                    class="print">
                                    <tr>
                                        <td style="width: 100%; border-bottom: solid 1px black; visibility: hidden;">
                                            <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("DOC_DATE") %>' Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;">
                                            <table align="center" width="100%" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                                                border-bottom: 1px solid black;" class="print millnames">
                                                <tr>
                                                    <td style="width: 100%;" class="tddetails">
                                                        <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                            <ItemTemplate>
                                                                <table align="center" width="100%" cellpadding="1" cellspacing="2" style="table-layout: fixed;"
                                                                    class="print">
                                                                    <tr>
                                                                        <td align="center" style="width: 1%;">
                                                                            <asp:Label ID="lbldocno" runat="server" Text='<%#Eval("doc_no") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                         <td align="center" style="width: 1%;">
                                                                            <asp:Label ID="Label23" runat="server" Text='<%#Eval("billno") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="center" style="width: 3%;">
                                                                            <asp:Label ID="Label18" runat="server" Text='<%#Eval("DOC_DATE") %>' Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                         <td align="center" style="width: 2%;">
                                                                            <asp:Label ID="Label7" runat="server" Text='<%#Eval("AC_CODE") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 6%;">
                                                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Supplier") %>' Font-Bold="false"></asp:Label>
                                                                        </td>

                                                                     <%--   <td align="center" style="width: 1%;">
                                                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("netqty") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 1%;">
                                                                            <asp:Label ID="lbltrantype" runat="server" Text='<%#Eval("purchasevalue") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 1%;">
                                                                            <asp:Label ID="lblpartyname" runat="server" Text='<%#Eval("MARKETSES") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 1%;">
                                                                            <asp:Label ID="Label9" runat="server" Text='<%#Eval("supercost") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 1%;">
                                                                            <asp:Label ID="Label13" runat="server" Text='<%#Eval("levihead") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 1%;">
                                                                            <asp:Label ID="lblmillname" runat="server" Text='<%#Eval("adat") %>' Font-Bold="false"></asp:Label>
                                                                        </td>--%>
                                                                       
                                                                        <td align="right" style="width: 1%;">
                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("tdsamount") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 2%;">
                                                                            <asp:Label ID="Label17" runat="server" Text='<%#Eval("payable") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;">
                                            <table width="100%" align="center" cellspacing="0" cellpadding="1" style="table-layout: fixed;"
                                                class="print">
                                                <tr>
                                                    <td align="left" style="width: 6%;" colspan="2">
                                                        <asp:Label ID="Label14" runat="server" Text="Total" Font-Bold="true" Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 6%;" colspan="2">
                                                        <asp:Label ID="Label24" runat="server" Text="Total" Font-Bold="true" Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="center" style="width: 3%;">
                                                        <asp:Label ID="Label18" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td align="right" style="width: 1%;">
                                                        <asp:Label ID="Label15" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                   <%-- <td align="center" style="width: 1%;">
                                                        <asp:Label ID="lblqty" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="right" style="width: 1%;">
                                                        <asp:Label ID="lblitemvalue" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="right" style="width: 1%;">
                                                        <asp:Label ID="lblmarketsess" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="right" style="width: 1%;">
                                                        <asp:Label ID="lblsupercost" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="right" style="width: 1%;">
                                                        <asp:Label ID="lbllevi" runat="server" Text="" Font-Bold="true" Visible="false"> </asp:Label>
                                                    </td>
                                                    <td align="right" style="width: 1%;">
                                                        <asp:Label ID="lbladat" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                    </td>--%>
                                                    <td align="right" style="width: 2%;">
                                                        <asp:Label ID="lblAC_CODE" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="right" style="width: 1%;">
                                                        <asp:Label ID="lbltdsamt" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="right" style="width: 2%;">
                                                        <asp:Label ID="lblpayable" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
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
                    <td style="width: 100%;">
                        <table align="center" width="100%" cellpadding="1" cellspacing="2" style="table-layout: fixed;
                            border-bottom: 1px solid black; border-top: 1px solid black" class="print">
                            <tr>
                                <td align="center" style="width: 1%;">
                                    <asp:Label ID="Label19" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="center" style="width: 1%;">
                                    <asp:Label ID="Label25" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="center" style="width: 3%;">
                                    <asp:Label ID="Label20" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="left" style="width: 6%;">
                                    <asp:Label ID="Label21" runat="server" Text="Grand Total" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="center" style="width: 1%;">
                                    <asp:Label ID="lblgrandnetwt" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 1%;">
                                    <asp:Label ID="lblgranditemvalue" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 1%;">
                                    <asp:Label ID="lblgrandmcess" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 1%;">
                                    <asp:Label ID="lblgrandscost" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 1%;">
                                    <asp:Label ID="lblgrandlevi" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 1%;">
                                    <asp:Label ID="lblgrnadadat" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 2%;">
                                    <asp:Label ID="lblgrandamount" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 1%;">
                                    <asp:Label ID="lblgrandtdsamnt" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 2%;">
                                    <asp:Label ID="lblgrandpayable" runat="server" Text="Payable Amnt" Font-Bold="true"></asp:Label>
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
