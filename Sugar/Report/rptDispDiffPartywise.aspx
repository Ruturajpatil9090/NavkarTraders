<%@ Page Title="rpt:Partywise Dispatch Difference" Language="C#" AutoEventWireup="true" CodeFile="rptDispDiffPartywise.aspx.cs"
    Inherits="Report_rptDispDiffPartywise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Partywise Dispatch Difference</title>
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
        <asp:Panel runat="server" ID="pnlMain" align="center" Font-Names="calibri">
            <asp:Label ID="lblReportName" runat="server" Text="Partywise Dispatch Difference"
                Width="100%" CssClass="lblName" Font-Bold="true" Font-Size="Large" Style="text-align: center;
                width: 100%;"></asp:Label>
            <table id="Table1" runat="server" width="90%" align="center" cellspacing="2" style="table-layout: fixed;
                border-top: 1px double black; border-bottom: 1px double black;" class="largsize">
                <tr>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text="Date"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="No"></asp:Label>
                    </td>
                    <td style="width: 4%" align="left">
                        <asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Mill"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Quantal"></asp:Label>
                    </td>
                    <td style="width: 3%" align="center">
                        <asp:Label runat="server" ID="Label4" Font-Bold="true" Text="Mill Rate"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Purc.Rate"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label6" Font-Bold="true" Text="To Pay"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label7" Font-Bold="true" Text="To Recieve"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label8" Font-Bold="true" Text="LV"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="Table12" runat="server" align="center" width="90%" class="largsize">
                <tr>
                    <td align="center" style="width: 100%">
                        <asp:DataList runat="server" ID="datalist" Width="100%" OnItemDataBound="DataList_ItemDataBound">
                            <ItemTemplate>
                                <table id="Table3" runat="server" width="100%" align="center" style="table-layout: fixed"
                                    class="largsize">
                                    <tr>
                                        <td style="width: 30%" align="right">
                                            <asp:Label runat="server" ID="lblPartyCode" Text='<%#Eval("partycode") %>' Visible="false"></asp:Label>
                                        </td>
                                        <td style="width: 70%" align="left">
                                            <asp:Label runat="server" ID="lblPartyName" Text='<%#Eval("party") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                <ItemTemplate>
                                                    <table id="Table1" runat="server" width="100%" align="left" style="table-layout: fixed;
                                                        background-color: #CCFFFF; border-bottom: 1px dashed black;" cellspacing="2"
                                                        class="largsize">
                                                        <tr>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="lblDates" Font-Bold="true" Text='<%#Eval("tdate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label1s" Font-Bold="true" Text='<%#Eval("tno") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 4%" align="left">
                                                                <asp:Label runat="server" ID="Label2s" Font-Bold="false" Text='<%#Eval("mill") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label3s" Font-Bold="false" Text='<%#Eval("quantal") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="center">
                                                                <asp:Label runat="server" ID="Label4s" Font-Bold="false" Text='<%#Eval("millrate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label5s" Font-Bold="false" Text='<%#Eval("salerate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label6s" Font-Bold="false" Text='<%#Eval("topay") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label7s" Font-Bold="false" Text='<%#Eval("torecieve") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:LinkButton runat="server" ID="lnkOV" Text='<%#Eval("OV") %>' OnClick="lnkOV_Click"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8" style="border-bottom: 1px double black;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <table id="Tablsds" runat="server" width="100%" align="right" cellspacing="2" style="background-color: #FFFFCC;
                                                table-layout: fixed;" class="largsize">
                                                <tr>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="lblDates" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="Label1s" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 4%" align="left">
                                                        <asp:Label runat="server" ID="Label2s" Font-Bold="false" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="Label3s" Font-Bold="false" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="center">
                                                        <asp:Label runat="server" ID="Label4s" Font-Bold="false" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="Label5s" Font-Bold="false" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="lblToPayTotal" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="lblToRecieveTotal" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                     <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="Label9" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8" style="border-bottom: 1px double black;">
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <%-- <tr>
                    <td colspan="2">
                        <tr>
                            <td colspan="8" style="border-bottom: 1px double black;">
                            </td>
                        </tr>
                       
                        <tr>
                            <td colspan="7" style="border-bottom: 1px double black;">
                            </td>
                        </tr>
                    </td>
                </tr>--%>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
