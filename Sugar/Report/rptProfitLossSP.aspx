<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptProfitLossSP.aspx.cs"
    Inherits="Report_rptProfitLossSP" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Profit Loss</title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=PrintPanel.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body style="font-family:Calibri;font-size:12px; width:100%;text-align:center; " >');
            printWindow.document.write('<style type = "text/css">thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript">
        function sp(accode, fromdt, todt, DrCr) {
            var tn;
            window.open('rptLedger.aspx?accode=' + accode + '&fromdt=' + fromdt + '&todt=' + todt + '&DrCr=' + DrCr);    //R=Redirected  O=Original
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div align="left">
            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
                Width="80px" />
            &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
                Width="120px" OnClick="btnExportToExcel_Click" />
            &nbsp;
            <asp:Button runat="server" ID="btnSendEmail" Text="Email" OnClick="btnSendEmail_Click"
                Width="79px" />
            Email:<asp:TextBox runat="server" ID="txtEmail" Width="300px"></asp:TextBox>
        </div>
        <div>
            <asp:Panel ID="PrintPanel" runat="server" align="center" Font-Names="Calibri">
               
                <table id="tbMain" runat="server" align="center" style="page-break-after: avoid;
                    width: 1000px;">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblCompany" runat="server" Text="" Font-Bold="true" Font-Size="12pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblCompanyAddr" runat="server" Text="Kolhapur"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <asp:Label ID="lblUpto" runat="server"  Width="100%" CssClass="lblName"
                    Font-Bold="true" Font-Size="12pt" Style="text-align: center; width: 100%;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblCompanyMobile" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 1000px;" align="center">
                    <tr>
                        <td valign="top" style="width: 50%;">
                            <table style="width: 100%; border-left: solid 1px black; border-right: solid 1px black;">
                                <tr>
                                    <td align="left" style="width: 70%; border-bottom: 1px solid black; border-top: 1px solid black;">
                                        Purchase
                                    </td>
                                    <td align="right" style="width: 30%; border-bottom: 1px solid black; border-top: 1px solid black;">
                                        Amount
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%; border-right: solid 1px black;">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="left" style="width: 70%; border-bottom: 1px solid black; border-top: 1px solid black;">
                                        Sales
                                    </td>
                                    <td align="right" style="width: 30%; border-bottom: 1px solid black; border-top: 1px solid black;">
                                        Amount
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--***************************************************************** Trading  ***************************************************************--%>
                    <tr>
                        <td valign="top" style="width: 50%; border-right: solid 1px black; border-left: solid 1px black;">
                            <%-------------------------  Left Datalist ------------------------%>
                            <asp:DataList ID="dtl_TradingCredit" runat="server" Width="100%" OnItemDataBound="dtl_TradingCredit_ItemDataBound">
                                <ItemTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td align="left" style="width: 70%;">
                                                <asp:Label ID="lblGroupCodeR" runat="server" Text='<%#Eval("Group_Code") %>' Font-Bold="true"
                                                    Visible="false" Font-Size="11px"></asp:Label><asp:Label ID="lblGroupNameR" runat="server"
                                                        Text='<%#Eval("groupname") %>' Font-Bold="true" Font-Size="12px"></asp:Label>&nbsp;<asp:Label
                                                            ID="lblsummaryR" runat="server" Font-Size="11px" Text='<%#Eval("summary") %>'
                                                            Font-Bold="true" Visible="false"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 30%;">
                                                <asp:Label ID="lblGroupAmountR" runat="server" Text='<%#Eval("groupamount") %>' Font-Bold="true"
                                                    Font-Size="14px"></asp:Label>&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:DataList ID="dtl_TradingCreditInner" runat="server" Width="100%">
                                        <ItemTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="left" style="width: 70%;">
                                                        &nbsp;&nbsp;&nbsp;<%--<asp:Label ID="lblAcNameR" runat="server" Text='<%#Eval("acname") %>'
                                                            Font-Bold="false" Font-Size="12px"></asp:Label>--%>
                                                        <asp:Label ID="lblaccode" runat="server" Text='<%#Eval("AC_CODE") %>' Font-Bold="false"
                                                            Font-Size="12px" Visible="false"></asp:Label>
                                                        <asp:LinkButton Style="text-decoration: none;" runat="server" ID="lblAcNameR" Text='<%#Eval("acname") %>'
                                                            OnClick="lnkDO_Click"></asp:LinkButton>
                                                    </td>
                                                    <td align="right" style="width: 30%;">
                                                        <asp:Label ID="lblACAmountR" runat="server" Text='<%#Eval("acamount") %>' Font-Bold="false"
                                                            Font-Size="12px"></asp:Label><asp:Label ID="lblblank" runat="server" Width="80px"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ItemTemplate>
                            </asp:DataList>
                            <%-------------------------  Right Datalist ------------------------%>
                        </td>
                        <td valign="top" style="width: 50%; border-right: solid 1px black;">
                            <%-------------------------  Right Datalist ------------------------%>
                            <asp:DataList ID="dtl_TradingDebit" runat="server" Width="100%" OnItemDataBound="dtl_TradingDebit_ItemDataBound">
                                <ItemTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td align="left" style="width: 70%;">
                                                <asp:Label ID="lblGroupCodeL" runat="server" Text='<%#Eval("Group_Code") %>' Font-Bold="true"
                                                    Visible="false"></asp:Label><asp:Label ID="lblGroupNameL" runat="server" Font-Size="12px"
                                                        Text='<%#Eval("groupname") %>' Font-Bold="true"></asp:Label>&nbsp;<asp:Label ID="lblsummaryL"
                                                            runat="server" Text='<%#Eval("summary") %>' Font-Bold="true" Visible="false"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 30%;">
                                                <asp:Label ID="lblGroupAmountL" runat="server" Text='<%#Eval("groupamount") %>' Font-Bold="true"
                                                    Font-Size="14px"></asp:Label>&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:DataList ID="dtl_TradingDebitInner" runat="server" Width="100%">
                                        <ItemTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="left" style="width: 70%;">
                                                        &nbsp;&nbsp;&nbsp;<%--<asp:Label ID="lblAcNameL" runat="server" Text='<%#Eval("acname") %>'
                                                            Font-Bold="false" Font-Size="12px"></asp:Label>--%>
                                                        <asp:Label ID="lblaccode" runat="server" Text='<%#Eval("AC_CODE") %>' Font-Bold="false"
                                                            Font-Size="12px" Visible="false"></asp:Label>
                                                        <asp:LinkButton Style="text-decoration: none;" runat="server" ID="lblAcNameL" Text='<%#Eval("acname") %>'
                                                            OnClick="lnkDO_Click"></asp:LinkButton>
                                                    </td>
                                                    <td align="right" style="width: 30%;">
                                                        <asp:Label ID="lblACAmountL" runat="server" Text='<%#Eval("acamount") %>' Font-Bold="false"
                                                            Font-Size="12px"></asp:Label><asp:Label ID="lblblank" runat="server" Width="80px"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ItemTemplate>
                            </asp:DataList>
                            <%-------------------------  Left Datalist ------------------------%>
                        </td>
                    </tr>
                    <%------------------------------- Footer Part ----------------------%>
                    <tr>
                        <td colspan="2" style="width: 100%; border-bottom: solid 1px black;">
                        </td>
                    </tr>
                    <%-----------------------------Group Total ------------------------%>
                    <tr>
                        <td align="right" style="width: 50%; border-right: solid 1px black; border-left: solid 1px black;">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="left" style="width: 70%;">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Net Purchase:"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 30%;">
                                        <asp:Label ID="lblNetDebit" runat="server" Font-Bold="true" Text="Net Credit"></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" style="width: 50%; border-right: solid 1px black;">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="left" style="width: 70%;">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Net Sale:"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 30%;">
                                        <asp:Label ID="lblNetCredit" runat="server" Font-Bold="true" Text="Net debit"></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%-----------------------------Net Profit & loss ------------------------%>
                    <tr>
                        <td align="right" style="width: 50%; border-right: solid 1px black; min-height: 50px;
                            vertical-align: bottom; border-left: solid 1px black;">
                            <table style="width: 100%; table-layout: fixed; height: 40px; vertical-align: bottom;">
                                <tr>
                                    <td align="left" style="width: 70%; border-bottom: solid 1px black;">
                                        <asp:Label ID="lblnetprofithead" runat="server" Font-Bold="true" Text="Gross Profit"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 30%; border-bottom: solid 1px black;">
                                        <asp:Label ID="lblnetProfit" runat="server" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" style="width: 50%; border-right: solid 1px black; min-height: 50px;">
                            <table style="width: 100%; table-layout: fixed; height: 40px;">
                                <tr>
                                    <td align="left" style="width: 70%; border-bottom: solid 1px black;">
                                        <asp:Label ID="lblnetlosshead" runat="server" Font-Bold="true" Text="Gross Loss"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 30%; border-bottom: solid 1px black;">
                                        <asp:Label ID="lblnetLoss" runat="server" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%-----------------------------Last amount ------------------------%>
                    <tr>
                        <td align="right" style="width: 50%; border-right: solid 1px black; border-left: solid 1px black;">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="left" style="width: 70%; border-bottom: solid 1px black;">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Total"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 30%; border-bottom: solid 1px black;">
                                        <asp:Label ID="lblTotal_L" runat="server" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" style="width: 50%; border-right: solid 1px black;">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="left" style="width: 70%; border-bottom: solid 1px black;">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Total Debit"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 30%; border-bottom: solid 1px black;">
                                        <asp:Label ID="lblTotal_R" runat="server" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--***************************************************************** Profit  ***************************************************************--%>
                    <tr>
                        <td valign="top" style="width: 50%; border-right: solid 1px black; border-left: solid 1px black;">
                            <%-------------------------  Right Datalist ------------------------%>
                            <asp:DataList ID="dtl_ProfitCredit" runat="server" Width="100%" OnItemDataBound="dtl_ProfitCredit_ItemDataBound">
                                <ItemTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td align="left" style="width: 70%;">
                                                <asp:Label ID="lblGroupCodeR" runat="server" Text='<%#Eval("Group_Code") %>' Font-Bold="true"
                                                    Visible="false" Font-Size="11px"></asp:Label><asp:Label ID="lblGroupNameR" runat="server"
                                                        Text='<%#Eval("groupname") %>' Font-Bold="true" Font-Size="12px"></asp:Label>&nbsp;<asp:Label
                                                            ID="lblsummaryR" runat="server" Font-Size="11px" Text='<%#Eval("summary") %>'
                                                            Font-Bold="true" Visible="false"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 30%;">
                                                <asp:Label ID="lblGroupAmountR" runat="server" Text='<%#Eval("groupamount") %>' Font-Bold="true"
                                                    Font-Size="14px"></asp:Label>&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:DataList ID="dtl_ProfitCreditInner" runat="server" Width="100%">
                                        <ItemTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="left" style="width: 70%;">
                                                        &nbsp;&nbsp;&nbsp;<%--<asp:Label ID="lblAcNameR" runat="server" Text='<%#Eval("acname") %>'
                                                            Font-Bold="false" Font-Size="12px"></asp:Label>--%>
                                                        <asp:Label ID="lblaccode" runat="server" Text='<%#Eval("AC_CODE") %>' Font-Bold="false"
                                                            Font-Size="12px" Visible="false"></asp:Label>
                                                        <asp:LinkButton Style="text-decoration: none;" runat="server" ID="lblAcNameR" Text='<%#Eval("acname") %>'
                                                            OnClick="lnkDO_Click"></asp:LinkButton>
                                                    </td>
                                                    <td align="right" style="width: 30%;">
                                                        <asp:Label ID="lblACAmountR" runat="server" Text='<%#Eval("acamount") %>' Font-Bold="false"
                                                            Font-Size="12px"></asp:Label><asp:Label ID="lblblank" runat="server" Width="80px"></asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ItemTemplate>
                            </asp:DataList>
                            <%-------------------------  Right Datalist ------------------------%>
                        </td>
                        <td valign="top" style="width: 50%; border-right: solid 1px black;">
                            <%-------------------------  Left Datalist ------------------------%>
                            <asp:DataList ID="dtl_ProfitDebit" runat="server" Width="100%" OnItemDataBound="dtl_ProfitDebit_ItemDataBound">
                                <ItemTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td align="left" style="width: 70%;">
                                                <asp:Label ID="lblGroupCodeL" runat="server" Text='<%#Eval("Group_Code") %>' Font-Bold="true"
                                                    Visible="false"></asp:Label><asp:Label ID="lblGroupNameL" runat="server" Font-Size="12px"
                                                        Text='<%#Eval("groupname") %>' Font-Bold="true"></asp:Label>&nbsp;<asp:Label ID="lblsummaryL"
                                                            runat="server" Text='<%#Eval("summary") %>' Font-Bold="true" Visible="false"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 30%;">
                                                <asp:Label ID="lblGroupAmountL" runat="server" Text='<%#Eval("groupamount") %>' Font-Bold="true"
                                                    Font-Size="14px"></asp:Label>&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:DataList ID="dtl_ProfitDebitInner" runat="server" Width="100%">
                                        <ItemTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="left" style="width: 70%;">
                                                        &nbsp;&nbsp;&nbsp;<%--<asp:Label ID="lblAcNameL" runat="server" Text='<%#Eval("acname") %>'
                                                            Font-Bold="false" Font-Size="12px"></asp:Label>--%>
                                                        <asp:Label ID="lblaccode" runat="server" Text='<%#Eval("AC_CODE") %>' Font-Bold="false"
                                                            Font-Size="12px" Visible="false"></asp:Label>
                                                        <asp:LinkButton Style="text-decoration: none;" runat="server" ID="lblAcNameL" Text='<%#Eval("acname") %>'
                                                            OnClick="lnkDO_Click"></asp:LinkButton>
                                                    </td>
                                                    <td align="right" style="width: 30%;">
                                                        <asp:Label ID="lblACAmountL" runat="server" Text='<%#Eval("acamount") %>' Font-Bold="false"
                                                            Font-Size="12px"></asp:Label><asp:Label ID="lblblank" runat="server" Width="80px"></asp:Label>&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ItemTemplate>
                            </asp:DataList>
                            <%-------------------------  Left Datalist ------------------------%>
                        </td>
                    </tr>
                    <%------------------------------- Footer Part ----------------------%>
                    <tr>
                        <td colspan="2" style="width: 100%; border-bottom: solid 1px black;">
                        </td>
                    </tr>
                    <%-----------------------------Group Total ------------------------%>
                    <tr>
                        <td align="right" style="width: 50%; border-right: solid 1px black; border-left: solid 1px black;">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="left" style="width: 70%;">
                                        <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Total:"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 30%;">
                                        <asp:Label ID="lblNetCredit1" runat="server" Font-Bold="true" Text="Net Credit"></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" style="width: 50%; border-right: solid 1px black;">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="left" style="width: 70%;">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Total:"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 30%;">
                                        <asp:Label ID="lblNetDebit1" runat="server" Font-Bold="true" Text="Net debit"></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%-----------------------------Net Profit & loss ------------------------%>
                    <tr>
                        <td align="right" style="width: 50%; border-right: solid 1px black; min-height: 50px;
                            border-left: solid 1px black;">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="left" style="width: 70%; border-bottom: solid 1px black;">
                                        <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Net Profit"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 30%; border-bottom: solid 1px black;">
                                        <asp:Label ID="lblNetProfitAnkush" runat="server" Font-Bold="true" Text="0"></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" style="width: 50%; border-right: solid 1px black; min-height: 50px;">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="left" style="width: 70%; border-bottom: solid 1px black;">
                                        <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="Net Loss"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 30%; border-bottom: solid 1px black;">
                                        <asp:Label ID="lblNetLossAnkush" runat="server" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%-----------------------------Last amount ------------------------%>
                    <tr>
                        <td align="right" style="width: 50%; border-right: solid 1px black; border-left: solid 1px black;">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="left" style="width: 70%; border-bottom: solid 1px black;">
                                        <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Total Credit"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 30%; border-bottom: solid 1px black;">
                                        <asp:Label ID="lblTotalCreditAll" runat="server" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" style="width: 50%; border-right: solid 1px black;">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="left" style="width: 70%; border-bottom: solid 1px black;">
                                        <asp:Label ID="Label15" runat="server" Font-Bold="true" Text="Total Debit"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 30%; border-bottom: solid 1px black;">
                                        <asp:Label ID="lblTotalDebitAll" runat="server" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>
</html>
