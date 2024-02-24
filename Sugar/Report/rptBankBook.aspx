<%@ Page Title="rpt:Bank Book" Language="C#" AutoEventWireup="true" CodeFile="rptBankBook.aspx.cs" Inherits="Report_rptBankBook" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../print.css" media="print" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=PrintPanel.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head><link href="../print.css" media="print" rel="Stylesheet" type="text/css" />');
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
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSendEmail" runat="server" Text="Send To Email" CssClass="btnHelp"
            OnClick="btnSendEmail_Click" />&nbsp;&nbsp;
        <asp:TextBox runat="server" ID="txtEmail" Width="200px"></asp:TextBox>&nbsp;&nbsp;</div>
    <div>
        <asp:Panel ID="PrintPanel" runat="server" align="center" Font-Names="Calibri">
            <table width="70%" align="center" id="tblMain" runat="server" style="table-layout: fixed;"
                cellspacing="5" class="print">
                <tr>
                    <td align="center" class="medium">
                        <asp:Label runat="server" ID="lblCompany" Font-Bold="true"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Bank Name:<asp:Label runat="server" ID="lblBankAndDate"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="70%" align="center" id="Table1" runat="server" style="table-layout: fixed;
                border-top: 1px solid Black; border-bottom: 1px solid Black;" cellspacing="1"
                class="print">
                <tr>
                    <td style="width: 10%;" align="center">
                        Tran_Type
                    </td>
                    <td style="width: 5%;" align="center">
                        No
                    </td>
                    <td style="width: 40%;" align="left">
                        Narration
                    </td>
                    <td style="width: 10%;" align="center">
                        Debit
                    </td>
                    <td style="width: 10%;" align="center">
                        Credit
                    </td>
                    <td style="width: 10%;" align="center">
                        Balance
                    </td>
                    <td style="width: 2%;" align="center">
                    </td>
                </tr>
            </table>
            <table width="70%" align="center" class="print">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataList runat="server" ID="dtlist" OnItemDataBound="dtlist_OnItemDataBound"
                            Width="100%">
                            <ItemTemplate>
                                <table width="100%" align="center" id="Table2" runat="server" style="table-layout: fixed;"
                                    cellspacing="1" class="print">
                                    <tr>
                                        <td style="width: 10%;" align="center">
                                            <asp:Label runat="server" ID="lblOpDate" Text='<%#Eval("Date") %>'></asp:Label>
                                        </td>
                                        <td style="width: 5%;" align="center">
                                            <asp:Label runat="server" ID="Label7" Text="0"></asp:Label>
                                        </td>
                                        <td style="width: 40%;" align="left">
                                            <asp:Label runat="server" ID="lblOpBala" Text="Opening Balance"></asp:Label>
                                        </td>
                                        <td style="width: 10%;" align="center">
                                            <asp:Label runat="server" ID="lblOpDebit" Text='<%#Eval("OpDebit") %>'></asp:Label>
                                        </td>
                                        <td style="width: 10%;" align="center">
                                            <asp:Label runat="server" ID="lblOpCredit" Text='<%#Eval("OpCredit") %>'></asp:Label>
                                        </td>
                                        <td style="width: 10%;" align="center">
                                            <asp:Label runat="server" ID="lblOpBal" Text='<%#Eval("OpBalance") %>'></asp:Label>
                                        </td>
                                        <td style="width: 2%;" align="center">
                                            <asp:Label runat="server" ID="lblOpDrCr" Text='<%#Eval("OpDrCr") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" class="print">
                                    <tr>
                                        <td style="width: 100%;">
                                            <asp:DataList runat="server" ID="dtlListdate" Width="100%" OnItemDataBound="dtlListdate_OnItemDataBound">
                                                <ItemTemplate>
                                                    <table width="100%" align="center" id="Table1" runat="server" style="table-layout: fixed;
                                                        border-bottom: 1px solid black;" cellspacing="1" class="print">
                                                        <tr>
                                                            <td style="width: 15%; background-color: #FFFFCC;" align="center">
                                                                <asp:Label runat="server" ID="lblDocDate" Text='<%#Eval("Doc_Date") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 40%;" align="center">
                                                            </td>
                                                            <td style="width: 10%;" align="center">
                                                            </td>
                                                            <td style="width: 10%;" align="center">
                                                            </td>
                                                            <td style="width: 10%;" align="center">
                                                            </td>
                                                            <td style="width: 2%;" align="center">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%" align="center" class="print">
                                                        <tr>
                                                            <td style="width: 100%;">
                                                                <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                                    <ItemTemplate>
                                                                        <table width="100%" align="center" runat="server" style="table-layout: fixed; border-bottom: 1px dashed Black;"
                                                                            cellspacing="1" class="print">
                                                                            <tr>
                                                                                <td style="width: 10%;" align="center">
                                                                                    <asp:Label runat="server" ID="Label1" Text='<%#Eval("TranType") %>' Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 5%;" align="center">
                                                                                    <asp:Label runat="server" ID="Label2" Text='<%#Eval("DocNo") %>' Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 40%;" align="left" class="medium">
                                                                                    <asp:Label runat="server" ID="Label3" Text='<%#Eval("Narration") %>' Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10%;" align="center">
                                                                                    <asp:Label runat="server" ID="Label4" Text='<%#Eval("Debit") %>' Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10%;" align="center">
                                                                                    <asp:Label runat="server" ID="Label5" Text='<%#Eval("Credit") %>' Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10%;" align="center">
                                                                                    <asp:Label runat="server" ID="Label6" Text='<%#Eval("Balance") %>' Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 2%;" align="center">
                                                                                    <asp:Label runat="server" ID="lblOpDrCr" Text='<%#Eval("DrCr") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%;">
                                                                <table id="Table3" width="100%" align="center" runat="server" style="table-layout: fixed;
                                                                    border-bottom: 1px double Black; border-top: 1px double Black;" cellspacing="1"
                                                                    class="print">
                                                                    <tr>
                                                                        <td style="width: 10%;" align="center">
                                                                            <asp:Label runat="server" ID="Label1" Text="" Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 5%;" align="center">
                                                                            <asp:Label runat="server" ID="Label2" Text="" Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 40%;" align="right">
                                                                            <asp:Label runat="server" ID="Label3" Text="Total:" Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%;" align="center">
                                                                            <asp:Label runat="server" ID="lblDebitTotal" Text="a" Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%;" align="center">
                                                                            <asp:Label runat="server" ID="lblCreditTotal" Text="a" Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 10%;" align="center">
                                                                            <asp:Label runat="server" ID="lblBalanceTotal" Text="a" Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 2%;" align="center">
                                                                            <asp:Label runat="server" ID="lblDrCr" Text="a" Font-Bold="true"></asp:Label>
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
                                            <table id="Table3" width="100%" align="center" runat="server" style="table-layout: fixed;
                                                background-color: #CCFFFF; border-bottom: 1px double Black;" cellspacing="1"
                                                class="print">
                                                <tr>
                                                    <td style="width: 10%;" align="center">
                                                        <asp:Label runat="server" ID="Label1" Text="" Font-Bold="false"></asp:Label>
                                                    </td>
                                                    <td style="width: 5%;" align="center">
                                                        <asp:Label runat="server" ID="Label2" Text="" Font-Bold="false"></asp:Label>
                                                    </td>
                                                    <td style="width: 40%;" align="center">
                                                        <asp:Label runat="server" ID="Label3" Text="Net Total:" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%;" align="center">
                                                        <asp:Label runat="server" ID="lblNetDebitTotal" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%;" align="center">
                                                        <asp:Label runat="server" ID="lblNetCreditTotal" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%;" align="center">
                                                        <asp:Label runat="server" ID="lblNetBalanceTotal" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td style="width: 2%;" align="center">
                                                        <asp:Label runat="server" ID="lblNetDrCr" Text="" Font-Bold="true"></asp:Label>
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
        </asp:Panel>
    </div>
    </form>
</body>
</html>
