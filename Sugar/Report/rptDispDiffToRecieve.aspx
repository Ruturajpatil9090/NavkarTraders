<%@ Page Title="rpt:Disp Diff To Recieve" Language="C#" AutoEventWireup="true" CodeFile="rptDispDiffToRecieve.aspx.cs"
    Inherits="Report_rptDispDiffToRecieve" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dispatch Diff To Recieve</title>
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
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

        function DebitNote() {
            window.open('../Sugar/pgeLocalvoucher.aspx');    //R=Redirected  O=Original
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
            Width="120px" OnClick="btnExportToExcel_Click" />
        &nbsp;&nbsp;<asp:Button runat="server" ID="btnSendEmail" Text="Email" OnClick="btnSendEmail_Click"
            OnClientClick="CheckEmail();" Width="79px" />
        &nbsp;<asp:TextBox runat="server" ID="txtEmail" Width="300px" Height="23px"></asp:TextBox>
    </div>
    <div>
        <asp:Panel runat="server" ID="pnlMain" align="center" Font-Names="calibri" CssClass="largsize">
            <asp:Label ID="lblReportName" runat="server" Text="Detail Resale Difference" Width="100%"
                CssClass="lblName largsize" Font-Bold="true" Font-Size="Medium" Style="text-align: center;
                width: 100%;"></asp:Label>
            <table id="Table1" runat="server" width="90%" align="center" cellspacing="2" style="table-layout: fixed;
                border-top: 1px solid black; border-bottom: 1px solid black;" class="largsize">
                <tr>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text="Date"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="No"></asp:Label>
                    </td>
                    <td style="width: 5%" align="left">
                        <asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Name Of Account"></asp:Label>
                    </td>
                    <td style="width: 4%" align="left">
                        <asp:Label runat="server" ID="Label9" Font-Bold="true" Text="Mill"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Quantal"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label4" Font-Bold="true" Text="Mill Rate"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label5" Font-Bold="true" Text="SaleRate"></asp:Label>
                    </td>
                    <td style="width: 3%" align="left">
                        <asp:Label runat="server" ID="Label14" Font-Bold="true" Text="Broker"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label6" Font-Bold="true" Text="Amount"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="Table3" runat="server" align="center" width="90%" class="largsize">
                <tr>
                    <td align="center" style="width: 100%">
                        <asp:Label runat="server" ID="lblPartyCode" Text="Diff To Recieve Report" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 100%">
                        <asp:DataList runat="server" ID="datalist" Width="100%" OnItemDataBound="DataList_ItemDataBound">
                            <ItemTemplate>
                                <table id="Table1" runat="server" width="100%" align="left" style="table-layout: fixed;
                                    border-bottom: 1px dashed black;" cellspacing="2" class="largsize">
                                    <tr style="background-color: #CCFFFF;">
                                        <td style="width: 2%" align="center">
                                            <asp:Label runat="server" ID="lblDates" Font-Bold="true" Text='<%#Eval("tdate") %>'></asp:Label>
                                        </td>
                                        <td style="width: 2%" align="center">
                                            <asp:Label runat="server" ID="Label1s" Font-Bold="true" Text='<%#Eval("tno") %>'></asp:Label>
                                        </td>
                                        <td style="width: 5%; overflow: hidden;" align="left">
                                            <asp:Label runat="server" ID="Label2s" Font-Bold="false" Text='<%#Eval("getpass") %>'></asp:Label>
                                        </td>
                                        <td style="width: 4%; overflow: hidden;" align="left">
                                            <asp:Label runat="server" ID="Label15" Font-Bold="false" Text='<%#Eval("mill") %>'></asp:Label>
                                        </td>
                                        <td style="width: 2%" align="center">
                                            <asp:Label runat="server" ID="Label3s" Font-Bold="false" Text='<%#Eval("quantal") %>'></asp:Label>
                                        </td>
                                        <td style="width: 2%" align="center">
                                            <asp:Label runat="server" ID="Label4s" Font-Bold="false" Text='<%#Eval("millrate") %>'></asp:Label>
                                        </td>
                                        <td style="width: 2%" align="center">
                                            <asp:Label runat="server" ID="Label5s" Font-Bold="false" Text='<%#Eval("salerate") %>'></asp:Label>
                                        </td>
                                        <td style="width: 3%; overflow: hidden;" align="left">
                                            <asp:Label runat="server" ID="Label16" Font-Bold="false" Text='<%#Eval("broker") %>'></asp:Label>
                                        </td>
                                        <td style="width: 2%" align="center">
                                            <asp:Label runat="server" ID="Label6s" Font-Bold="false" Text='<%#Eval("amount") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2%" align="right">
                                            <asp:Label runat="server" ID="Label8" Font-Bold="true" Text="Voc.No"></asp:Label>
                                        </td>
                                        <td style="width: 2%; border-bottom: 1px double black;" align="center">
                                            <asp:LinkButton runat="server" ID="lnkLV" Font-Bold="true" Text='<%#Eval("voucher") %>'
                                                Style="text-decoration: none;" OnClick="lnkLV_Click"></asp:LinkButton>
                                        </td>
                                        <td style="width: 4%; border-bottom: 1px double black;" align="left">
                                            <asp:Label runat="server" ID="Label10" Font-Bold="false" Text="Chq.No"></asp:Label>
                                        </td>
                                        <td style="width: 2%;" align="right">
                                            <asp:Label runat="server" ID="Label12" Font-Bold="false" Text="Date"></asp:Label>
                                        </td>
                                        <td style="width: 3%; border-bottom: 1px double black;" align="right">
                                        </td>
                                        <td style="width: 2%;" align="right">
                                        </td>
                                        <td style="width: 2%;" align="center">
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
