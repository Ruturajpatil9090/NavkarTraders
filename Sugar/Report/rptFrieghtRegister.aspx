<%@ Page Title="rpt:Frieght Register 1" Language="C#" AutoEventWireup="true" CodeFile="rptFrieghtRegister.aspx.cs"
    Inherits="Report_rptFrieghtRegister" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Frieght Register</title>
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('sugarStock.html', 'st', 'height=660,width=1360');
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

        function fr2(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptFrieghtRegister2.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }
    </script>
    <script type="text/javascript" src="../JS/emailValidation.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />&nbsp;&nbsp;<asp:Button runat="server"
                ID="btnSendEmail" Text="Email" OnClick="btnSendEmail_Click" OnClientClick="CheckEmail();"
                Width="79px" />
        &nbsp;<asp:TextBox runat="server" ID="txtEmail" Width="300px" Height="23px"></asp:TextBox>
    </div>
    <div>
        <asp:Panel ID="pnlMain" runat="server" align="center" Font-Names="Calibri" CssClass="largsize">
            <asp:Label ID="lblCompanyName" Width="90%" runat="server" Text="" Style="text-align: center;"
                CssClass="lblName" Font-Size="16px" Font-Bold="true"></asp:Label>
            <br />
            <asp:Label ID="Label15" runat="server" Width="90%" Text="Frieght Confirmation Letter"
                CssClass="lblName" Style="text-align: center;" Font-Size="14px" Font-Bold="true"></asp:Label>
            <br />
            <table width="95%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                border-top: 1px solid black; border-bottom: 1px solid black;" class="largsize">
                <tr>
                    <td align="right" style="width: 2%;">
                        <asp:Label ID="lblMemono" runat="server" Text="Memo.No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 4%;">
                        <asp:Label ID="lblMill" runat="server" Text="Mill" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 7%;">
                        <asp:Label ID="lblParty" runat="server" Text="Party" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 7%;">
                        <asp:Label ID="lblTransport" runat="server" Text="Transport" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 4%;">
                        <asp:Label ID="lblQntl" runat="server" Text="Qntl" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 5%;">
                        <asp:Label ID="lblVehNo" runat="server" Text="Veh.No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 4%;">
                        <asp:Label ID="lblAmount" runat="server" Text="Frieght" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 4%;">
                        <asp:Label ID="Label1" runat="server" Text="Frieght.Amt" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 4%;">
                        <asp:Label ID="lblMillName" runat="server" Text="Advance" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="95%" align="center" cellpadding="1" cellspacing="0" class="largsize">
                <tr>
                    <td>
                        <asp:DataList ID="dtl" runat="server" Width="100%" OnItemDataBound="DataList_ItemDataBound">
                            <ItemTemplate>
                                <table width="100%" align="left" cellpadding="1" cellspacing="0" class="largsize">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblDate" Text='<%#Eval("Memo_Date") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <%--  <table align="left" width="100%" cellpadding="1" cellspacing="0">
                                                <tr>
                                                    <td colspan="7">--%>
                                            <asp:DataList runat="server" ID="dtlDetails" Width="100%" CellPadding="3">
                                                <ItemTemplate>
                                                    <table align="center" width="100%" cellpadding="1" cellspacing="0" style="table-layout: fixed;"
                                                        class="largsize">
                                                        <tr>
                                                            <td align="center" style="width: 2%;">
                                                                <asp:Label ID="lblMemonod" runat="server" Text='<%#Eval("Memo_No") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 4%;">
                                                                <asp:Label ID="lblMilld" runat="server" Text='<%#Eval("Mill") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 7%;">
                                                                <asp:Label ID="lblPartyd" runat="server" Text='<%#Eval("Party") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 7%;">
                                                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("Transport") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 4%;">
                                                                <asp:Label ID="lblQntld" runat="server" Text='<%#Eval("quantal") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 5%;">
                                                                <asp:Label ID="lblVehNod" runat="server" Text='<%#Eval("Veh_No") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 4%;">
                                                                <asp:Label ID="lblAmountd" runat="server" Text='<%#Eval("frieght") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 4%;">
                                                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("frieghtAmt") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 4%;">
                                                                <asp:Label ID="lblMillNamed" runat="server" Text='<%#Eval("Advance") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="9" align="right" style="border-bottom: dashed 2px black;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                                <tr>
                                    <td colspan="8" align="right" style="border-bottom: double 2px black;">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8" align="right" style="border-bottom: double 2px black;">
                                        <table align="left" width="100%" cellpadding="1" cellspacing="0" style="table-layout: fixed;"
                                            class="largsize">
                                            <tr>
                                                <td align="right" style="width: 2%;">
                                                    <asp:Label ID="lblq" runat="server" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 4%;">
                                                    <asp:Label ID="lblf" runat="server" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 7%;">
                                                    <asp:Label ID="lbls" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 7%;">
                                                    <asp:Label ID="Label4" runat="server" Text="Total:" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 4%;">
                                                    <asp:Label ID="lblqtltotal" runat="server" Text="qtl" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 5%;">
                                                    <asp:Label ID="lblsd" runat="server" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 4%;">
                                                    <asp:Label ID="lblfrieghttotal" runat="server" Text="frighttotal" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 4%;">
                                                    <asp:Label ID="lblfrightAmttotal" runat="server" Text="frightAmttotal" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="center" style="width: 4%;">
                                                    <asp:Label ID="lbladvtotal" runat="server" Text="advance" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
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
