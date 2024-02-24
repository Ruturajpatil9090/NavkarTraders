<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptdd1summeryreport.aspx.cs" Inherits="Sugar_Report_rptdd1summeryreport" Title="rpt:DD1 Summery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DD1 1Summery Report</title>
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
                <asp:Label ID="lblReportName" runat="server" Text="DD1 Summery Report" Width="100%"
                    CssClass="lblName" Font-Bold="true" Font-Size="Medium" Style="text-align: center; width: 100%;"></asp:Label>
                <table id="Table1" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed; border-bottom: 1px double black; border-top: 1px solid black;"
                    class="largsize">
                    <tr>
                        <td style="width: 30%" align="left">
                            <asp:Label runat="server" ID="lblMillname" Font-Bold="true" Text="Mill Name"></asp:Label>
                        </td>
                        <td style="width: 10%" align="right">
                            <asp:Label runat="server" ID="lblopeningbal" Font-Bold="true" Text="Opening Bal"></asp:Label>
                        </td>
                        <td style="width: 10%" align="right">
                            <asp:Label runat="server" ID="lblAmount" Font-Bold="true" Text="Amount"></asp:Label>
                        </td>
                        <td style="width: 10%" align="right">
                            <asp:Label runat="server" ID="lblPayment" Font-Bold="true" Text="Payment"></asp:Label>
                        </td>
                        <td style="width: 10%" align="right">
                            <asp:Label runat="server" ID="lblBalance" Font-Bold="true" Text="Balance"></asp:Label>
                        </td>
                        <td style="width: 10%" align="right">
                            <asp:Label runat="server" ID="lblDDDiff" Font-Bold="true" Text="DD Diff"></asp:Label>
                        </td>
                        <td style="width: 10%" align="right">
                            <asp:Label runat="server" ID="lblSale" Font-Bold="true" Text="Sale"></asp:Label>
                        </td>
                        <td style="width: 10%" align="right">
                            <asp:Label runat="server" ID="lblBags" Font-Bold="true" Text="Bags"></asp:Label>
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
                                            <td style="width: 30%;" align="left">
                                                <asp:Label runat="server" ID="lblPayment_To" Visible="true" Text='<%#Eval("paymentto_name") %>'></asp:Label>
                                            </td>
                                            <td style="width: 10%;" align="right">
                                                <asp:Label runat="server" ID="lblpaymentto_name" Visible="true" Font-Bold="true" Text='<%#Eval("openingbal") %>'></asp:Label>
                                            </td>
                                            <td style="width: 10%;" align="right">
                                                <asp:Label runat="server" ID="Label2" Visible="true" Font-Bold="true" Text='<%#Eval("amount") %>'></asp:Label>
                                            </td>
                                            <td style="width: 10%;" align="right">
                                                <asp:Label runat="server" ID="Label3" Visible="true" Font-Bold="true" Text='<%#Eval("payment") %>'></asp:Label>
                                            </td>
                                            <td style="width: 10%;" align="right">
                                                <asp:Label runat="server" ID="Label5" Visible="true" Font-Bold="true" Text='<%#Eval("balance") %>'></asp:Label>
                                            </td>
                                            <td style="width: 10%;" align="right">
                                                <asp:Label runat="server" ID="Label6" Visible="true" Font-Bold="true" Text='<%#Eval("DDDiff") %>'></asp:Label>
                                            </td>
                                            <td style="width: 10%;" align="right">
                                                <asp:Label runat="server" ID="Label7" Visible="true" Font-Bold="true" Text='<%#Eval("sale") %>'></asp:Label>
                                            </td>
                                            <td style="width: 10%;" align="right">
                                                <asp:Label runat="server" ID="Label8" Visible="true" Font-Bold="true" Text='<%#Eval("bags") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="8" style="border-bottom: 1px double black;"></td>
                                        </tr>


                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <table id="Table2" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed;"
                                class="largsize">
                                <tr>
                                    <td style="width: 30%" align="left">
                                        <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text="Total"></asp:Label>
                                    </td>
                                    <td style="width: 10%" align="right">
                                        <asp:Label runat="server" ID="lblopeningbalTot" Font-Bold="true" Text=""></asp:Label>
                                    </td>
                                    <td style="width: 10%" align="right">
                                        <asp:Label runat="server" ID="lblAmountTot" Font-Bold="true" Text=""></asp:Label>
                                    </td>

                                    <td style="width: 10%" align="right">
                                        <asp:Label runat="server" ID="lblPaymentTot" Font-Bold="true" Text=""></asp:Label>
                                    </td>
                                    <td style="width: 10%" align="right">
                                        <asp:Label runat="server" ID="lblBalanceTot" Font-Bold="false" Text=""></asp:Label>
                                    </td>
                                    <td style="width: 10%" align="right">
                                        <asp:Label runat="server" ID="lblDDDiffTot" Font-Bold="true" Text=""></asp:Label>
                                    </td>
                                    <td style="width: 10%" align="right">
                                        <asp:Label runat="server" ID="lblSaleTot" Font-Bold="false" Text=""></asp:Label>
                                    </td>
                                    <td style="width: 10%" align="right">
                                        <asp:Label runat="server" ID="lblBagsTot" Font-Bold="true" Text=""></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="8" style="border-bottom: 1px double black;"></td>
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
