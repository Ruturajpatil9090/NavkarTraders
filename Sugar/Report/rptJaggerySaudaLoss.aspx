<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptJaggerySaudaLoss.aspx.cs"
    Inherits="Report_rptJaggerySaudaLoss" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Jaggary Sale Register</title>
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
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
    </div>
    <div>
        <asp:Panel runat="server" ID="pnlMain" align="center" Font-Names="calibri" CssClass="largsize">
            <table width="80%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;"
                class="print">
                <tr>
                    <td style="width: 100%;" align="center">
                        <asp:Label runat="server" ID="lblCompanyName" Font-Bold="true" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" align="center">
                        <asp:Label runat="server" ID="Label11" Font-Bold="true" Text="Jaggary Sale Register"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" align="center">
                        <asp:Label runat="server" ID="lblfrotodate" Font-Bold="true" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                border-bottom: solid 2px black; border-top: solid 2px black;" class="print">
                <tr>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label4" runat="server" Text="Type" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="lblParty" runat="server" Text="Date" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 1%;">
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label17" runat="server" Text="No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 15%;">
                        <asp:Label ID="Label21" runat="server" Text="Cust Name" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 15%;">
                        <asp:Label ID="lblMemono" runat="server" Text="Item Name" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label18" runat="server" Text="Bill_No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label19" runat="server" Text="Qty" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="lblpnm" runat="server" Text="Net Wt" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 5%;">
                        <asp:Label ID="Label1" runat="server" Text="P Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 5%;">
                        <asp:Label ID="Label2" runat="server" Text="S Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 1%;">
                    </td>
                    <td align="right" style="width: 5%;">
                        <asp:Label ID="Label12" runat="server" Text="P Amnt" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 1%;">
                    </td>
                    <td align="right" style="width: 5%;">
                        <asp:Label ID="Label5" runat="server" Text="S Amnt" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 1%;">
                    </td>
                    <td align="right" style="width: 5%;">
                        <asp:Label ID="Label10" runat="server" Text="Loss" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%" align="center" cellpadding="1" cellspacing="0" class="print">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataList ID="dtl" runat="server" Width="100%" CellSpacing="4" OnItemDataBound="dtl_OnItemDataBound">
                            <ItemTemplate>
                                <table align="center" width="100%" cellpadding="1" cellspacing="0" style="table-layout: fixed;"
                                    class="print">
                                    <tr>
                                        <td style="width: 100%; border-bottom: solid 1px black;">
                                            <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("doc_date") %>' Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;">
                                            <table align="center" width="100%" cellpadding="1" cellspacing="0" style="table-layout: fixed;"
                                                class="print millnames">
                                                <tr>
                                                    <td style="width: 100%;" class="tddetails">
                                                        <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                            <ItemTemplate>
                                                                <table align="center" width="100%" cellpadding="1" cellspacing="2" style="table-layout: fixed;"
                                                                    class="print">
                                                                    <tr>
                                                                        <td align="left" style="width: 5%;">
                                                                            <asp:Label ID="lbldocno" runat="server" Text='<%#Eval("Tran_Type") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 5%;">
                                                                            <asp:Label ID="Label22" runat="server" Text='<%#Eval("doc_date") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 1%;">
                                                                        </td>
                                                                        <td align="left" style="width: 5%;">
                                                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Doc_No") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 15%;">
                                                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("Ac_Name_E") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 15%;">
                                                                            <asp:Label ID="Label23" runat="server" Text='<%#Eval("System_Name_E") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 5%;">
                                                                            <asp:Label ID="Label20" runat="server" Text='<%#Eval("Bill_No") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 5%;">
                                                                            <asp:Label ID="lbltrantype" runat="server" Text='<%#Eval("Qty") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 5%;">
                                                                            <asp:Label ID="lblpartyname" runat="server" Text='<%#Eval("Net_Wt") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 5%;">
                                                                            <asp:Label ID="Label9" runat="server" Text='<%#Eval("Purc_Rate") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 5%;">
                                                                            <asp:Label ID="Label13" runat="server" Text='<%#Eval("Sale_Rate") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 1%;">
                                                                        </td>
                                                                        <td align="right" style="width: 5%;">
                                                                            <asp:Label ID="lblmillname" runat="server" Text='<%#Eval("Purc_Amnt") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 1%;">
                                                                        </td>
                                                                        <td align="right" style="width: 5%;">
                                                                            <asp:Label ID="Label7" runat="server" Text='<%#Eval("Sale_Amnt") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 1%;">
                                                                        </td>
                                                                        <td align="right" style="width: 5%;">
                                                                            <asp:Label ID="Label24" runat="server" Text='<%#Eval("Loss") %>' Font-Bold="false"></asp:Label>
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
                                            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;"
                                                class="print">
                                                <tr>
                                                    <td align="left" style="width: 5%;">
                                                        <asp:Label ID="Label14" runat="server" Text="Total" Font-Bold="true" Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 5%;">
                                                        <asp:Label ID="Label15" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 1%;">
                                                    </td>
                                                    <td align="left" style="width: 5%;">
                                                        <asp:Label ID="lblqty" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 15%;">
                                                        <asp:Label ID="lblitemvalue" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 15%;">
                                                        <asp:Label ID="lblmarketsess" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 5%;">
                                                        <asp:Label ID="lblsupercost" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 5%;">
                                                        <asp:Label ID="lbllevi" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 5%;">
                                                        <asp:Label ID="lbladat" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 5%;">
                                                        <asp:Label ID="lblbillamount" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 5%;">
                                                        <asp:Label ID="Label25" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 1%;">
                                                    </td>
                                                    <td align="left" style="width: 5%;">
                                                        <asp:Label ID="Label26" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 1%;">
                                                    </td>
                                                    <td align="left" style="width: 5%;">
                                                        <asp:Label ID="Label27" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 1%;">
                                                    </td>
                                                    <td align="left" style="width: 5%;">
                                                        <asp:Label ID="Label28" runat="server" Text="" Font-Bold="true"></asp:Label>
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
            </table>
            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                border-bottom: solid 2px black; border-top: solid 2px black;" class="print">
                <tr>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label6" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label8" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 1%;">
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label16" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 15%;">
                        <asp:Label ID="Label29" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 15%;">
                        <asp:Label ID="Label30" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label31" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label32" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label33" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label34" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label35" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 1%;">
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label36" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 1%;">
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label37" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 1%;">
                    </td>
                    <td align="right" style="width: 5%;">
                        <asp:Label ID="lblLoss" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
