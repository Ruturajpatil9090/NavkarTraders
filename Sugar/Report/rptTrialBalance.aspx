<%@ Page Title="rpt:Trial Balance" Language="C#" AutoEventWireup="true" CodeFile="rptTrialBalance.aspx.cs"
    Inherits="Reports_rptTrialBalance" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('sugarStock.html', 'st', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body style="font-family:Calibri;font-size:12px;width:1100px; text-align:center;" >');
            printWindow.document.write(panel.innerHTML);

            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 100);
            return false;
        }
        function sp(accode, fromdt, todt, DrCr) {
            var tn;
            window.open('rptLedger.aspx?accode=' + accode + '&fromdt=' + fromdt + '&todt=' + todt + '&DrCr=' + DrCr);    //R=Redirected  O=Original
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;
        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return PrintPanel();"
            Width="80px" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />&nbsp;
        <asp:Button ID="btnMail" runat="server" Text="Mail" Width="80px" OnClick="btnMail_Click" />
        Email:<asp:TextBox runat="server" ID="txtEmail" Width="300px"></asp:TextBox>
        <asp:Panel ID="pnlMain" runat="server" align="center" Font-Names="Calibri">
            <asp:Label ID="lblCompanyName" Width="70%" runat="server" Text="" Style="text-align: center;"
                CssClass="lblName" Font-Size="16px" Font-Bold="true"></asp:Label>
            <br />
            <asp:Label ID="lblUpto" runat="server" Width="90%"  CssClass="lblName"
                Style="text-align: center;" Font-Size="14px" Font-Bold="true"></asp:Label>
            <table width="90%" align="center">
                <tr>
                    <td align="left" style="width: 6%;">
                        <asp:Label ID="Label1" runat="server" Text="AC Code" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 30%;">
                        <asp:Label ID="Label2" runat="server" ForeColor="Maroon" Text="AC Name" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 10%;">
                        <asp:Label ID="Label3" runat="server" ForeColor="Maroon" Text="City" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 10%;">
                        <asp:Label ID="Label6" runat="server" ForeColor="Maroon" Text="Debit" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 10%;">
                        <asp:Label ID="Label8" runat="server" ForeColor="Maroon" Text="Credit" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="border-bottom: double 2px black;">
                    </td>
                </tr>
            </table>
            <div align="center">
                <asp:DataList ID="dtl" runat="server" Width="90%" OnItemDataBound="DataList1_ItemDataBound">
                    <ItemTemplate>
                        <table width="100%" align="center" cellpadding="1" cellspacing="0">
                            <tr>
                                <td align="left" style="width: 10%;">
                                    <asp:Label ID="lblGroup_Code" runat="server" Text='<%#Eval("Group_Code") %>' Font-Bold="true"></asp:Label>
                                </td>
                                <td align="left" colspan="2" style="width: 40%;">
                                    <asp:Label ID="lblBSGroupName" runat="server" Text='<%#Eval("BSGroupName") %>' Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblGroupType" runat="server" Text='<%#Eval("group_Type") %>'></asp:Label>
                                </td>
                                <td align="right" style="width: 10%;">
                                    <asp:Label ID="lblDebit" runat="server" Text='<%#Eval("Debit") %>' Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 10%;">
                                    <asp:Label ID="lblCredit" runat="server" Text='<%#Eval("Credit") %>' Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" align="left">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 100%;">
                                                <asp:DataList ID="dtlDetails" runat="server" Width="100%">
                                                    <ItemTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="left" style="width: 6%;">
                                                                    <asp:LinkButton Style="text-decoration: none;" runat="server" ID="lnkDO" Text='<%#Eval("AC_CODE") %>'
                                                                        OnClick="lnkDO_Click"></asp:LinkButton>
                                                                    <%-- <asp:Label ID="Label7" runat="server" Font-Bold="false" Text='<%#Eval("AC_CODE") %>'></asp:Label>--%>
                                                                </td>
                                                                <td align="left" style="width: 30%;">
                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="false" Text='<%#Eval("Ac_Name_E") %>'></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 10%;">
                                                                    <asp:Label ID="Label5" runat="server" Font-Bold="false" Text='<%#Eval("CityName") %>'></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 10%;">
                                                                    <asp:Label ID="Label9" runat="server" Font-Bold="false" Text='<%#Eval("DebitD") %>'></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 10%;">
                                                                    <asp:Label ID="Label10" runat="server" Font-Bold="false" Text='<%#Eval("CreditD") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-bottom: dashed 1px black;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td style="width: 10%;">
                                </td>
                                <td style="width: 52%;" align="left">
                                    <asp:Label ID="lblDiff" runat="server" Text="diff" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 10%;">
                                </td>
                                <td align="right" style="width: 10%;">
                                    <asp:Label ID="lblDebitTotal" runat="server" Text="d" Font-Bold="true" Visible="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 10%;">
                                    <asp:Label ID="lblCreditTotal" runat="server" Text="c" Font-Bold="true" Visible="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <table width="90%" align="center" style="background-color: #FFFFCC;">
                <tr>
                    <td style="width: 3%; border-top: solid 2px Navy;" align="right">
                        <b>NET Diff:&nbsp;</b>
                    </td>
                    <td align="left" style="width: 10%; border-top: solid 2px Navy;">
                        <asp:Label runat="server" ID="lblTotalDifference" Font-Bold="true" ForeColor="Navy"></asp:Label>
                    </td>
                    <td align="right" style="width: 10%; border-top: solid 2px Navy;">
                        <asp:Label ID="lblnetDebit" runat="server" Text="ND" Font-Bold="true" ForeColor="Navy"></asp:Label>
                    </td>
                    <td align="right" style="width: 4%; border-top: solid 2px Navy;">
                        <asp:Label ID="lblnetCredit" runat="server" Text="NC" Font-Bold="true" ForeColor="Navy"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="border-bottom: solid 1px Navy;">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
