<%@ Page Title="rpt:Carporate Balance" Language="C#" AutoEventWireup="true" CodeFile="rptCarporateBalance.aspx.cs"
    Inherits="Report_rptCarporateBalance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body  style="font-family:Calibri; font-size:12px;width:1100px;" >');
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
        <div>
            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
                Width="80px" />
            &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
                Width="120px" OnClick="btnExportToExcel_Click" />
            &nbsp;&nbsp;<asp:Button runat="server" ID="btnSendEmail" Text="Email" OnClick="btnSendEmail_Click"
                OnClientClick="CheckEmail();" Width="79px" />
            &nbsp;<asp:TextBox runat="server" ID="txtEmail" Width="300px" Height="23px"></asp:TextBox>
            <asp:Panel ID="pnlMain" runat="server" align="center" Font-Names="Calibri">
                <table id="Table2" runat="server" width="80%" align="center" cellspacing="2" style="table-layout: fixed">
                    <tr>
                        <td style="width: 100%;" align="center">
                            <asp:Label runat="server" ID="lblCompany" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" align="left">
                            <asp:Label runat="server" ID="Label6" Text="Carporate Sell Balance Stock Report"
                                Font-Bold="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; border-bottom: 1px double black;"></td>
                    </tr>
                </table>
                <table id="Table1" runat="server" width="80%" align="center" cellspacing="2" style="table-layout: fixed">
                    <tr>
                        <td style="width: 2%" align="center">
                            <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text="No"></asp:Label>
                        </td>
                        <td style="width: 3%" align="center">
                            <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Date"></asp:Label>
                        </td>

                        <td style="width: 9%" align="left">
                            <asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Unit Name"></asp:Label>
                        </td>
                        <td style="width: 3%" align="center">
                            <asp:Label runat="server" ID="Label9" Font-Bold="true" Text="Sale Rate"></asp:Label>
                        </td>
                        <td style="width: 3%" align="center">
                            <asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Quintal"></asp:Label>
                        </td>
                        <td style="width: 2%" align="center">
                            <asp:Label runat="server" ID="Label4" Font-Bold="true" Text="Dispatch"></asp:Label>
                        </td>
                        <td style="width: 2%" align="center">
                            <asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Balance"></asp:Label>
                        </td>
                        <td style="width: 5%" align="left">
                            <asp:Label runat="server" ID="Label7" Font-Bold="true" Text="PO Details"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table runat="server" width="80%" align="center">
                    <tr>
                        <td style="width: 100%;" align="center">
                            <asp:DataList runat="server" ID="dtlist" Width="100%" OnItemDataBound="dtlist_ItemDataBound">
                                <ItemTemplate>
                                    <table width="100%" align="center">
                                        <tr>
                                            <td style="width: 100%" align="center">
                                                <table id="Table4" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed; border-top: 1px solid black; border-bottom: 1px solid black;">
                                                    <tr>
                                                        <td style="width: 2%" align="center">
                                                            <asp:Label runat="server" ID="lblPartyCode" Visible="false" Text='<%#Eval("Party_Code") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 3%" align="center"></td>
                                                        <td style="width: 15%" align="center">
                                                            <asp:Label runat="server" ID="lblParty" Text='<%#Eval("party") %>' Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td style="width: 2%" align="center"></td>
                                                        <td style="width: 2%" align="center"></td>
                                                        <td style="width: 5%" align="left"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%" align="left">
                                                <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                    <ItemTemplate>
                                                        <table runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed; border-bottom: 1px solid black; background-color: #CCFFFF;">
                                                            <tr>
                                                                <td style="width: 2%" align="center">
                                                                    <asp:Label runat="server" ID="lblDate" Font-Bold="false" Text='<%#Eval("Doc_No") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="center">
                                                                    <asp:Label runat="server" ID="Label1" Font-Bold="false" Text='<%#Eval("Doc_Date") %>'></asp:Label>
                                                                </td>

                                                                <td style="width: 9%" align="left">
                                                                    <asp:Label runat="server" ID="Label2" Font-Bold="false" Text='<%#Eval("Unit") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="center">
                                                                    <asp:Label runat="server" ID="Label9" Font-Bold="false" Text='<%#Eval("Sale_Rate") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="center">
                                                                    <asp:Label runat="server" ID="Label3" Font-Bold="false" Text='<%#Eval("Qntl") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 2%" align="center">
                                                                    <asp:Label runat="server" ID="Label4" Font-Bold="false" Text='<%#Eval("desp") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 2%" align="center">
                                                                    <asp:Label runat="server" ID="Label5" Font-Bold="false" Text='<%#Eval("balance") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 5%" align="left">
                                                                    <asp:Label runat="server" ID="Label7" Font-Bold="false" Text='<%#Eval("podetail") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%;">
                                                <table id="Table3" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed; border-bottom: 1px solid black;">
                                                    <tr>
                                                        <td style="width: 2%" align="center"></td>
                                                        <td style="width: 3%" align="center"></td>
                                                        <td style="width: 9%" align="left"></td>
                                                        <td style="width: 3%" align="center"></td>
                                                        <td style="width: 3%; background-color: #FFFFCC;" align="center">
                                                            <asp:Label runat="server" ID="lblQuatalTotal" Font-Bold="true" Text=""></asp:Label>
                                                        </td>
                                                        <td style="width: 2%" align="center"></td>
                                                        <td style="width: 2%; background-color: #FFFFCC;" align="center">
                                                            <asp:Label runat="server" ID="lblBalanceTotal" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td style="width: 5%" align="left"></td>
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
                            <table id="Table3" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed; border-bottom: 1px solid black;">
                                <tr>
                                    <td style="width: 2%" align="center"></td>
                                    <td style="width: 3%" align="center"></td>
                                    <td style="width: 9%" align="center">Grand Total:
                                    </td>
                                    <td style="width: 3%" align="center"></td>
                                    <td style="width: 3%; background-color: #CCFFFF;" align="center">
                                        <asp:Label runat="server" ID="lblGrandQntlTotal" Font-Bold="true" Text=""></asp:Label>
                                    </td>
                                    <td style="width: 2%" align="center"></td>
                                    <td style="width: 2%; background-color: #CCFFFF;" align="center">
                                        <asp:Label runat="server" ID="lblGrandBalTotal" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="width: 5%" align="left"></td>
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
