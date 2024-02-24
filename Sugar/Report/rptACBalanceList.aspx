<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptACBalanceList.aspx.cs"
    Inherits="Report_rptACBalanceList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AC Detail Report</title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=PrintPanel.ClientID %>");
            var printWindow = window.open('sugarStock.html', 'st', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body style="font-family:Calibri; font-size:12px;width:1100px;text-align:center;" >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 100);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div align="left">
            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
                Width="80px" />
            &nbsp;
            <asp:Button Text="Mail" runat="server" ID="btnSendEmail" OnClick="btnSendEmail_Click"
                Width="67px" />Email:<asp:TextBox runat="server" ID="txtEmail" Width="300px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
        </div>
        <asp:Panel ID="PrintPanel" runat="server" align="center" Font-Names="Calibri" Font-Size="12px">
            <asp:Label ID="lblReportName" runat="server" Text="Account Transaction" Width="100%"
                CssClass="lblName" Font-Bold="true" Font-Size="14px" Style="text-align: center;
                width: 100%;"></asp:Label>
            <table id="tbMain" runat="server" width="1000px" align="center" style="page-break-after: avoid;">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblCompany" runat="server" Text="" Font-Bold="true" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblCompanyAddr" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblCompanyMobile" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="Label1" runat="server" Text="From" Font-Size="14px"></asp:Label>
                        <asp:Label ID="lblfrmdt" runat="server" Text="" Font-Size="14px"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text="To" Font-Size="14px"></asp:Label>
                        <asp:Label ID="lbltodt" runat="server" Text="" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblgroupcodename" runat="server" Text="" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="1000px" border="1" align="center">
                <tr>
                    <td align="center" rowspan="2" style="width: 90px">
                        Account Code
                    </td>
                    <td align="center" rowspan="2" style="width: 200px">
                        Account Name
                    </td>
                    <td colspan="2" align="center">
                        Opening
                    </td>
                    <td colspan="2" align="center">
                        Transaction
                    </td>
                    <td colspan="2" align="center">
                        Closing
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 100px">
                        Debit
                    </td>
                    <td align="right" style="width: 100px">
                        Credit
                    </td>
                    <td align="right" style="width: 100px">
                        Debit
                    </td>
                    <td align="right" style="width: 100px">
                        Credit
                    </td>
                    <td align="right" style="width: 100px">
                        Debit
                    </td>
                    <td align="right" style="width: 100px">
                        Credit
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="width: 100%;">
                        <asp:DataList ID="dtl_AccountBalance" runat="server" Width="100%" OnItemDataBound="dtl_AccountBalance_ItemDataBound">
                            <ItemTemplate>
                                <table width="1000px" cellspacing="2" cellpadding="0" border="0" style="border-style: solid;
                                    border-left: 0; border-right: 0;">
                                    <tr>
                                        <td align="center" style="width: 90px;">
                                            <asp:Label ID="lblAC_Code" runat="server" Text='<%#Eval("Ac_Code") %>'></asp:Label>
                                        </td>
                                        <td align="left" style="width: 200px;">
                                            <asp:Label ID="lblAc_Name" runat="server" Text='<%#Eval("Ac_Name") %>' Font-Bold="true"
                                                Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 100px">
                                            <asp:Label ID="lblOp_Debit" runat="server" Text='<%#Eval("Op_Debit") %>'></asp:Label>
                                        </td>
                                        <td align="right" style="width: 100px;">
                                            <asp:Label ID="lblOp_Credit" runat="server" Text='<%#Eval("Op_Credit") %>'></asp:Label>
                                        </td>
                                        <td align="right" style="width: 100px">
                                            <asp:Label ID="lblTran_Debit" runat="server" Text='<%#Eval("Tran_Debit") %>'></asp:Label>
                                        </td>
                                        <td align="right" style="width: 100px;">
                                            <asp:Label ID="lblTran_Credit" runat="server" Text='<%#Eval("Tran_Credit") %>'></asp:Label>
                                        </td>
                                        <td align="right" style="width: 100px">
                                            <asp:Label ID="lblClosing_Debit" runat="server" Text='<%#Eval("Closing_Debit") %>'></asp:Label>
                                        </td>
                                        <td align="right" style="width: 100px">
                                            <asp:Label ID="lblClosing_Credit" runat="server" Text='<%#Eval("Closing_Credit") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="width: 100%;">
                        <table width="1000px" cellspacing="2" cellpadding="0" border="0" style="border-style: solid;
                            border-left: 0; border-right: 0;">
                            <tr>
                                <td align="center" style="width: 90px;">
                                    <asp:Label ID="Label11" runat="server" Font-Bold="false" CssClass="print"></asp:Label>
                                </td>
                                <td align="left" style="width: 200px;">
                                    <asp:Label ID="Label3" runat="server" Font-Bold="true" CssClass="print" Text="Grand Total"></asp:Label>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:Label ID="lblGrandOpDebit" runat="server" Font-Bold="true" CssClass="print"></asp:Label>
                                </td>
                                <td align="center" style="width: 100px;">
                                    <asp:Label ID="lblGrandOpCredit" runat="server" Font-Bold="true" CssClass="print"></asp:Label>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:Label ID="lblGrandTraDebit" runat="server" Font-Bold="true" CssClass="print"></asp:Label>
                                </td>
                                <td align="center" style="width: 100px;">
                                    <asp:Label ID="lblGrandTraCredit" runat="server" Font-Bold="true" CssClass="print"></asp:Label>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:Label ID="lblGrandCloDebit" runat="server" Font-Bold="true" CssClass="print"></asp:Label>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:Label ID="lblGrandCloCredit" runat="server" Font-Bold="true" CssClass="print"></asp:Label>
                                </td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
