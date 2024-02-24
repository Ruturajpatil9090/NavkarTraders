<%@ Page Title="rpt:Day Book" Language="C#" AutoEventWireup="true" CodeFile="rptDayBook.aspx.cs" Inherits="Report_rptDayBook" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Day Book</title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=PrintPanel.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body  style="font-family:Calibri;font-size:12px;width:1100px; text-align:center;" >');
            printWindow.document.write('<style type = "text/css">thead {display:table-header-group; } tfoot{display:table-footer-group;}</style>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript" src="../JS/emailValidation.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="left">
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" />&nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel"
                Text="Export To Excel" Width="120px" OnClick="btnExportToExcel_Click" />&nbsp;<asp:Button
                    runat="server" ID="btnSendMail" Text="Mail" OnClick="btnSendMail_Click" Width="88px"
                    OnClientClick="CheckEmail();" />&nbsp;<asp:TextBox runat="server" ID="txtEmail" Width="300px"></asp:TextBox>
    </div>
    <div>
        <asp:Panel ID="PrintPanel" runat="server" align="center" Font-Names="Calibri">
            <table width="1000px" align="center">
                <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="lblCompany" runat="server" Text="" Font-Bold="true"
                            Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="Label1" runat="server" Text="Day Book" Font-Bold="true" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%; border: solid 1px black;">
                        <table width="100%">
                            <tr>
                                <td style="width: 40%;">
                                    Credit Amount
                                </td>
                                <td style="width: 40%;">
                                    Description
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; border: solid 1px black;">
                        <table width="100%">
                            <tr>
                                <td style="width: 40%;">
                                    Debit Amount
                                </td>
                                <td style="width: 40%;">
                                    Description
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:DataList ID="dtlMain" runat="server" Width="1000px" align="center" OnItemDataBound="dtlMain_ItemDataBound">
                <ItemTemplate>
                    <table width="100%" align="center">
                        <tr>
                            <td style="width: 100%;">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 100%;" colspan="2">
                                            <tr>
                                                <td colspan="2" style="border-bottom: double 2px black; border-top: double 2px black;">
                                                    <asp:Label ID="lblOpBal" runat="server" Text='<%#Eval("AMOUNT") %>' Font-Bold="true"></asp:Label>
                                                    &nbsp;&nbsp;Opening Balance&nbsp;&nbsp;<asp:Label ID="lblDate" runat="server" Text='<%#Eval("DOC_DATE") %>'
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" style="width: 50%;">
                                                    <table width="100%">
                                                        <tr>
                                                            <td valign="top" style="width: 100%;">
                                                                <asp:DataList ID="dtl_Credit" runat="server" OnItemDataBound="dtl_Credit_ItemDataBound"
                                                                    Width="100%">
                                                                    <ItemTemplate>
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td align="left" colspan="2" style="width: 100%; border-right: dashed 1px black;">
                                                                                    <asp:Label ID="lblAcCreditAmt" Text='<%#Eval("amt") %>' Font-Bold="true" runat="server"></asp:Label>
                                                                                    &nbsp;
                                                                                    <asp:Label ID="lblCreditAcName" Text='<%#Eval("acname") %>' Font-Bold="true" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <asp:DataList ID="dtl_CreditDetail" runat="server" Width="100%">
                                                                            <ItemTemplate>
                                                                                <table width="100%" style="vertical-align: top;">
                                                                                    <tr>
                                                                                        <td align="right" style="width: 30%; vertical-align: top;">
                                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("amt") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" style="width: 70%; border-right: dashed 1px black;">
                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label3" Text='<%#Eval("narration") %>'
                                                                                                runat="server"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="top" style="width: 50%;">
                                                    <asp:DataList ID="dtl_debit" runat="server" OnItemDataBound="dtl_debit_ItemDataBound"
                                                        Width="100%">
                                                        <ItemTemplate>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td valign="top" align="left" colspan="2" style="width: 100%;">
                                                                                    <asp:Label ID="lblAcDebitAmt" Text='<%#Eval("amt") %>' Font-Bold="true" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lblDebitAcName" Text='<%#Eval("acname") %>' Font-Bold="true" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <asp:DataList ID="dtl_DebitDetail" runat="server" Width="100%">
                                                                            <ItemTemplate>
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td align="right" style="width: 30%;">
                                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("amt") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" style="width: 70%;">
                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label3" Text='<%#Eval("narration") %>'
                                                                                                runat="server"></asp:Label>
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
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" align="left">
                                <table width="100%" style="border-top: solid 1px black; border-bottom: solid 1px black;
                                    border-right: solid 2px black; border-left: solid 2px black; vertical-align: top;">
                                    <tr>
                                        <td style="width: 50%;">
                                            <asp:Label ID="lblDayCreditTotal" runat="server" Font-Bold="true"></asp:Label>&nbsp;
                                            Day Credit Total
                                        </td>
                                        <td style="width: 50%; border-left: solid 2px black;">
                                            <asp:Label ID="lblDayDebitTotal" runat="server" Font-Bold="true"></asp:Label>&nbsp;
                                            Day Debit Total
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%; vertical-align: bottom; height: 50px;">
                                            <asp:Label ID="lblTotalDayCredit" runat="server" Font-Bold="true"></asp:Label>&nbsp;Total
                                            Credit
                                        </td>
                                        <td style="width: 50%; border-left: solid 2px black; height: 50px; vertical-align: bottom;">
                                            <asp:Label ID="lblClosingDebit" runat="server" Font-Bold="true"></asp:Label>&nbsp;
                                            Closing
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%; vertical-align: bottom; height: 30px; border-top: solid 2px black;">
                                            <asp:Label ID="lblGrandCredit" runat="server" Font-Bold="true"></asp:Label>&nbsp;Grand
                                            Credit Total
                                        </td>
                                        <td style="width: 50%; border-left: solid 2px black; vertical-align: bottom; height: 30px;
                                            border-top: solid 2px black;">
                                            <asp:Label ID="lblGrandDebit" runat="server" Font-Bold="true"></asp:Label>&nbsp;
                                            Grand Debit Total
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
