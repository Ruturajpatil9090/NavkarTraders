<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptdd1report.aspx.cs" Inherits="Sugar_Report_rptdd1report" Title="rpt:DD1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DD1 Report</title>
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('a.html', 'st', 'height=400,width=800');
            printWindow.document.write('<html><head><link href="../print.css" rel="stylesheet" type="text/css" media="print" />');
            printWindow.document.write('</head><body class="largsize">');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 100);
            return false;
        }
    </script>
    <script type="text/javascript" src="../JS/emailValidation.js"></script>
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
            <asp:Panel runat="server" ID="pnlMain" CssClass="largsize">
                <asp:Label ID="lblReportName" runat="server" Text="DD1 Report" Width="100%"
                    CssClass="lblName" Font-Bold="true" Font-Size="Medium" Style="text-align: center; width: 100%;"></asp:Label>
                <table id="Table1" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed; border-bottom: 1px double black; border-top: 1px solid black;"
                    class="largsize">
                    <tr>
                        <td style="width: 2%" align="center">
                            <asp:Label runat="server" ID="lblSBNo" Font-Bold="true" Text="Bill No"></asp:Label>
                        </td>
                        <td style="width: 5%" align="left">
                            <asp:Label runat="server" ID="lblSBTo_Name" Font-Bold="true" Text="NameofParty"></asp:Label>
                        </td>
                        <td style="width: 3%" align="center">
                            <asp:Label runat="server" ID="lblSBTo_City" Font-Bold="true" Text="Place"></asp:Label>
                        </td>
                        <td style="width: 2%" align="right">
                            <asp:Label runat="server" ID="lblQty" Font-Bold="true" Text="Qty"></asp:Label>
                        </td>
                        <td style="width: 3%" align="right">
                            <asp:Label runat="server" ID="lblSaleRate" Font-Bold="true" Text="Rate"></asp:Label>
                        </td>
                        <td style="width: 3%" align="right">
                            <asp:Label runat="server" ID="lblDoRate" Font-Bold="true" Text="Do Rate"></asp:Label>
                        </td>
                        <td style="width: 4%" align="right">
                            <asp:Label runat="server" ID="lblGSTRate" Font-Bold="true" Text="RateWithGST"></asp:Label>
                        </td>
                        <td style="width: 3%" align="right">
                            <asp:Label runat="server" ID="lblDoGSTRate" Font-Bold="true" Text="Do With GST"></asp:Label>
                        </td>
                        <td style="width: 3%" align="right">
                            <asp:Label runat="server" ID="lblSaleRoundOff" Font-Bold="true" Text="SaleRoundOff"></asp:Label>
                        </td>
                        <td style="width: 3%" align="right">
                            <asp:Label runat="server" ID="lblPurRoundOff" Font-Bold="true" Text="PurRoundOff"></asp:Label>
                        </td>
                        <td style="width: 3%" align="right">
                            <asp:Label runat="server" ID="lblPSTCS" Font-Bold="true" Text="TCS On Pur"></asp:Label>
                        </td>
                        <td style="width: 3%" align="right">
                            <asp:Label runat="server" ID="lblSBTCS" Font-Bold="true" Text="TCS On Sale"></asp:Label>
                        </td>
                        <td style="width: 3%" align="right">
                            <asp:Label runat="server" ID="lblPSAmt" Font-Bold="true" Text="Pur Amt"></asp:Label>
                        </td>
                        <td style="width: 3%" align="right">
                            <asp:Label runat="server" ID="lblSBAmt" Font-Bold="true" Text="Sale Amt"></asp:Label>
                        </td>
                        <td style="width: 3%" align="right">
                            <asp:Label runat="server" ID="lblRecdAmt" Font-Bold="true" Text="Recd Amt"></asp:Label>
                        </td>
                        <td style="width: 3%" align="right">
                            <asp:Label runat="server" ID="lblbal" Font-Bold="true" Text="Balance"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="Table12" runat="server" align="center" width="100%" class="largsize">
                    <tr>
                        <td align="center" style="width: 100%">
                            <asp:DataList runat="server" ID="datalist" Width="100%" OnItemDataBound="DataList_ItemDataBound">
                                <ItemTemplate>
                                    <table id="Table3" runat="server" width="100%" align="center" style="background-color: #CCFFFF;"
                                        class="largsize">
                                        <tr>
                                            <td style="width: 30%;" align="right">
                                                <asp:Label runat="server" ID="lblPayment_To" Visible="false" Text='<%#Eval("Payment_To") %>'></asp:Label>
                                            </td>
                                            <td style="width: 70%;" align="left">
                                                <asp:Label runat="server" ID="lblpaymentto_name" Visible="true" Font-Bold="true" Text='<%#Eval("paymentto_name") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                    <ItemTemplate>
                                                        <table id="Table1" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed; background-color: #FFFFCC;"
                                                            class="largsize">
                                                            <tr>
                                                                <td style="width: 2%" align="center">
                                                                    <asp:Label runat="server" ID="Label2" Font-Bold="true" Text='<%#Eval("SB_No") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 5%" align="left">
                                                                    <asp:Label runat="server" ID="Label1" Font-Bold="false" Text='<%#Eval("salebillto_name") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="left">
                                                                    <asp:Label runat="server" ID="Label7" Font-Bold="false" Text='<%#Eval("salebillto_city") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 2%" align="right">
                                                                    <asp:Label runat="server" ID="Label3" Font-Bold="false" Text='<%#Eval("quantal") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="right">
                                                                    <asp:Label runat="server" ID="Label4" Font-Bold="false" Text='<%#Eval("sale_rate") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="right">
                                                                    <asp:Label runat="server" ID="Label5" Font-Bold="false" Text='<%#Eval("PurchaseRate") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 4%" align="right">
                                                                    <asp:Label runat="server" ID="Label8" Font-Bold="false" Text='<%#Eval("ratewithGST") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="right">
                                                                    <asp:Label runat="server" ID="Label10" Font-Bold="false" Text='<%#Eval("dowithGST") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="right">
                                                                    <asp:Label runat="server" ID="Label9" Font-Bold="false" Text='<%#Eval("sbroundoff") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="right">
                                                                    <asp:Label runat="server" ID="Label16" Font-Bold="false" Text='<%#Eval("psroundoff") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="right">
                                                                    <asp:Label runat="server" ID="Label17" Font-Bold="false" Text='<%#Eval("pstcs_amt") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="right">
                                                                    <asp:Label runat="server" ID="Label19" Font-Bold="false" Text='<%#Eval("saletcs_amt") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="right">
                                                                    <asp:Label runat="server" ID="Label20" Font-Bold="false" Text='<%#Eval("PSAmt") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="right">
                                                                    <asp:Label runat="server" ID="Label21" Font-Bold="false" Text='<%#Eval("SBAmt") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="right">
                                                                    <asp:Label runat="server" ID="Label22" Font-Bold="false" Text='<%#Eval("RecdAmt") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="right">
                                                                    <asp:Label runat="server" ID="Label6" Font-Bold="false" Text='<%#Eval("Bal") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="16" style="border-bottom: 1px double black;"></td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <table id="Table1" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed;"
                                                    class="largsize">
                                                    <tr>
                                                        <td style="width: 2%" align="center">
                                                            <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 5%" align="left">
                                                            <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Total"></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label11" Font-Bold="true" Text=""></asp:Label>
                                                        </td>

                                                        <td style="width: 2%" align="right">
                                                            <asp:Label runat="server" ID="Label12" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label4" Font-Bold="false" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label13" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label18" Font-Bold="false" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label113" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label14" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label15" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label16" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label17" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="lblPSAmtTotal" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="lblSBAmtTotal" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label20" Font-Bold="false" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label24" Font-Bold="false" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="16" style="border-bottom: 1px double black;"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <table id="Table2" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed;"
                                                    class="largsize">
                                                    <tr>
                                                        <td style="width: 2%" align="center">
                                                            <asp:Label runat="server" ID="Label23" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 5%" align="left">
                                                            <asp:Label runat="server" ID="Label25" Font-Bold="true" Text="Difference"></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label26" Font-Bold="true" Text=""></asp:Label>
                                                        </td>

                                                        <td style="width: 2%" align="right">
                                                            <asp:Label runat="server" ID="Label27" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label28" Font-Bold="false" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label29" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label30" Font-Bold="false" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label31" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label32" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label33" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label34" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label35" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="lblDiff" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label37" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label38" Font-Bold="false" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="right">
                                                            <asp:Label runat="server" ID="Label39" Font-Bold="false" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="16" style="border-bottom: 1px double black;"></td>
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
            </asp:Panel>
        </div>
    </form>
</body>
</html>
