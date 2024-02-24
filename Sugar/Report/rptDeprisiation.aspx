<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDeprisiation.aspx.cs"
    Inherits="Report_rptDeprisiation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Date Wise Sale Register</title>
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('sugarStock.html', 'st', 'height=400,width=800');
            printWindow.document.write('<html><head> <link href="../print.css" rel="stylesheet" type="text/css" media="print" />');
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
        <asp:Panel ID="Panel1" runat="server" align="center" Font-Names="Calibri" CssClass="largsize"
            Width="100%">
            <asp:Panel ID="pnlMain" runat="server" align="center" Font-Names="Calibri" CssClass="largsize"
                Width="100%">
                <asp:Label ID="lblCompanyName" Width="100%" runat="server" Text="" Style="text-align: center;"
                    CssClass="lblName" Font-Size="16px" Font-Bold="true"></asp:Label>
                <br />
                <asp:Label ID="Label15" runat="server" Width="100%" Text="Depriciation Report" CssClass="lblName"
                    Style="text-align: center;" Font-Size="14px" Font-Bold="true"></asp:Label>
                <table width="100%" align="center" cellpadding="1" cellspacing="0" style="border-bottom: 1px solid black;
                    table-layout: fixed;" class="largsize">
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" ID="lblBrokerName" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" ID="lblDate1" Font-Bold="true" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%" align="left" cellpadding="1" cellspacing="0" style="border-bottom: 1px solid black;
                    table-layout: fixed;" class="largsize">
                    <tr>
                        <td align="left" style="width: 2%;">
                            <asp:Label ID="lblNo" runat="server" Text="Ac Code" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" style="width: 5%;">
                            <asp:Label ID="lblMill" runat="server" Text="Ac_Name_E" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="right" style="width: 3%;">
                            <asp:Label ID="Label1" runat="server" Text="OpBal" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="right" style="width: 3%;">
                            <asp:Label ID="Label7" runat="server" Text="Additionalamnt" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="right" style="width: 3%;">
                            <asp:Label ID="Label8" runat="server" Text="Delection" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="right" style="width: 3%;">
                            <asp:Label ID="Label12" runat="server" Text="Date" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="right" style="width: 3%;">
                            <asp:Label ID="lblNetQntl" runat="server" Text="ClosingBalance" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="right" style="width: 3%;">
                            <asp:Label ID="lblRate" runat="server" Text="Ac_rate" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="right" style="width: 3%;">
                            <asp:Label ID="lblSubTotal" runat="server" Text="Depriciation" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="right" style="width: 3%;">
                            <asp:Label ID="lblExtraExp" runat="server" Text="balance" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="100%" align="left" cellpadding="1" cellspacing="0" style="border-bottom: 1px solid black;
                    table-layout: fixed;" class="largsize">
                    <tr>
                        <td style="width: 100%;">
                            <asp:DataList runat="server" ID="dtl" Width="100%" CellPadding="1" OnItemDataBound="dtl_OnItemDataBound">
                                <ItemTemplate>
                                    <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;"
                                        class="largsize">
                                        <tr>
                                            <td align="left" style="width: 2%; background-color: #FFFFCC;">
                                                <asp:Label ID="lblaccode" runat="server" Text='<%#Eval("Ac_Code")%>' Font-Bold="true"
                                                    Visible="false"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 5%;">
                                                <asp:Label ID="lblacname" runat="server" Text='<%#Eval("Ac_Name_E")%>' Font-Bold="true"
                                                    Visible="false"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 3%;">
                                            </td>
                                            <td align="right" style="width: 3%;">
                                            </td>
                                            <td align="right" style="width: 3%;">
                                            </td>
                                            <td align="right" style="width: 3%;">
                                            </td>
                                            <td align="right" style="width: 3%;">
                                            </td>
                                            <td align="right" style="width: 3%;">
                                            </td>
                                            <td align="right" style="width: 3%;">
                                            </td>
                                            <td align="right" style="width: 3%;">
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" align="center" cellpadding="1" cellspacing="0" class="largsize">
                                        <tr>
                                            <td style="width: 100%;">
                                                <asp:DataList runat="server" ID="dtlDetails" Width="100%" CellPadding="1" OnItemDataBound="dtlDetails_OnItemDataBound">
                                                    <ItemTemplate>
                                                        <table width="100%" align="center" cellpadding="1" cellspacing="0" style="border-bottom: 1px dashed black;
                                                            table-layout: fixed; border-top: 1px solid black;" class="largsize">
                                                            <tr>
                                                                <td align="left" style="width: 2%;">
                                                                    <asp:Label ID="lbldtlNo" runat="server" Text='<%#Eval("AC_CODE")%>' Font-Bold="false"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 5%;">
                                                                    <asp:Label ID="lbldtlMill" runat="server" Text='<%#Eval("Ac_Name_E")%>' Font-Bold="false"></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 3%;">
                                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("OpBal")%>' Font-Bold="false"></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 3%;">
                                                                    <asp:Label ID="Label9" runat="server" Text='<%#Eval("Additionalamnt")%>' Font-Bold="false"></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 3%;">
                                                                    <asp:Label ID="Label10" runat="server" Text='<%#Eval("Delection")%>' Font-Bold="false"></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 3%;">
                                                                    <asp:Label ID="Label12" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 3%;">
                                                                    <asp:Label ID="lbldtlNetQntl" runat="server" Text='<%#Eval("ClosingBalance")%>' Font-Bold="false"></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 3%;">
                                                                    <asp:Label ID="lbldtlRate" runat="server" Text='<%#Eval("Ac_rate")%>' Font-Bold="false"></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 3%;">
                                                                    <asp:Label ID="lbldtlSubTotal" runat="server" Text='<%#Eval("Depriciation")%>' Font-Bold="false"></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 3%;">
                                                                    <asp:Label ID="lbldtlExtraExp" runat="server" Text='<%#Eval("balance")%>' Font-Bold="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%" align="center" cellpadding="1" cellspacing="0" class="largsize">
                                                            <tr>
                                                                <td style="width: 100%;">
                                                                    <asp:DataList runat="server" ID="dtlDetails1" Width="100%" CellPadding="1">
                                                                        <ItemTemplate>
                                                                            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;"
                                                                                class="largsize">
                                                                                <%--<tr>
                                                                                <td align="left" style="width: 2%;">
                                                                                    <asp:Label ID="Label12" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 5%;">
                                                                                </td>
                                                                                <td align="right" style="width: 3%;">
                                                                                    <asp:Label ID="Label13" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td align="right" style="width: 3%;">
                                                                                    <asp:Label ID="ll1" runat="server" Text="D Amount" Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td align="right" style="width: 3%;">
                                                                                    <asp:Label ID="ll2" runat="server" Text="C Amount" Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td align="right" style="width: 3%;">
                                                                                    <asp:Label ID="ll3" runat="server" Text="Date" Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td align="right" style="width: 3%;">
                                                                                </td>
                                                                                <td align="right" style="width: 3%;">
                                                                                    <asp:Label ID="ll4" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td align="right" style="width: 3%;">
                                                                                    <asp:Label ID="Label19" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                            </tr>--%>
                                                                                <tr>
                                                                                    <td align="left" style="width: 2%;">
                                                                                        <asp:Label ID="l1" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" style="width: 5%;">
                                                                                    </td>
                                                                                    <td align="right" style="width: 3%;">
                                                                                        <asp:Label ID="l2" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                                                    </td>
                                                                                    <td align="right" style="width: 3%;">
                                                                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("DebitAmount")%>' Font-Bold="false"></asp:Label>
                                                                                    </td>
                                                                                    <td align="right" style="width: 3%;">
                                                                                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("CreditAmount")%>' Font-Bold="false"></asp:Label>
                                                                                    </td>
                                                                                    <td align="right" style="width: 3%;">
                                                                                        <asp:Label ID="Label11" runat="server" Text='<%#Eval("Date")%>' Font-Bold="false"></asp:Label>
                                                                                    </td>
                                                                                    <td align="right" style="width: 3%;">
                                                                                        <asp:Label ID="Label12" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td align="right" style="width: 3%;">
                                                                                    </td>
                                                                                    <td align="right" style="width: 3%;">
                                                                                        <asp:Label ID="l6" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                                                    </td>
                                                                                    <td align="right" style="width: 3%;">
                                                                                        <asp:Label ID="l7" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ItemTemplate>
                                                                    </asp:DataList>
                                                                </td>
                                                            </tr>
                                                            <%--<tr>
                                                            <td style="width: 100%;" colspan="8">
                                                                <table width="100%" align="center" cellspacing="0" style="background-color: #CCFFFF;
                                                                    table-layout: fixed;" class="largsize">
                                                                    <tr>
                                                                        <td align="left" style="width: 2%;">
                                                                            <asp:Label ID="l1" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 5%;">
                                                                        </td>
                                                                        <td align="right" style="width: 3%;">
                                                                            <asp:Label ID="l2" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 3%;">
                                                                            <asp:Label ID="Label5" runat="server" Text='<%#Eval("DebitAmount")%>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 3%;">
                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("CreditAmount")%>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 3%;">
                                                                            <asp:Label ID="Label11" runat="server" Text='<%#Eval("Date")%>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 3%;">
                                                                        </td>
                                                                        <td align="right" style="width: 3%;">
                                                                            <asp:Label ID="l6" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 3%;">
                                                                            <asp:Label ID="l7" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>--%>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; visibility: hidden;">
                                                <table width="100%" align="center" cellspacing="0" style="background-color: #CCFFFF;
                                                    table-layout: fixed;" class="largsize">
                                                    <tr>
                                                        <td align="left" style="width: 2%; visibility: hidden;">
                                                            <asp:Label ID="lbldtlNo" runat="server" Text="Total" Font-Bold="false" Visible="false"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 5%; visibility: hidden;">
                                                        </td>
                                                        <td align="right" style="width: 3%; visibility: hidden;">
                                                            <asp:Label ID="lnlinneropbal" runat="server" Text="" Font-Bold="false" Visible="false"></asp:Label>
                                                        </td>
                                                        <td align="right" style="width: 3%; visibility: hidden;">
                                                            <asp:Label ID="lblinnerAdditionalamnt" runat="server" Text="" Font-Bold="false" Visible="false"></asp:Label>
                                                        </td>
                                                        <td align="right" style="width: 3%; visibility: hidden;">
                                                            <asp:Label ID="lblinnerDelection" runat="server" Text="" Font-Bold="false" Visible="false"></asp:Label>
                                                        </td>
                                                        <td align="right" style="width: 3%;">
                                                            <asp:Label ID="Label12" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                        </td>
                                                        <td align="right" style="width: 3%; visibility: hidden;">
                                                            <asp:Label ID="lblinnerClosingBalance" runat="server" Text="" Font-Bold="false" Visible="false"></asp:Label>
                                                        </td>
                                                        <td align="right" style="width: 3%; visibility: hidden;">
                                                        </td>
                                                        <td align="right" style="width: 3%; visibility: hidden;">
                                                            <asp:Label ID="lblinnerDepriciation" runat="server" Text="" Font-Bold="false" Visible="false"></asp:Label>
                                                        </td>
                                                        <td align="right" style="width: 3%; visibility: hidden;">
                                                            <asp:Label ID="lblinnerbalance" runat="server" Text="" Font-Bold="false" Visible="false"></asp:Label>
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
                            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="border-bottom: 1px solid  black;
                                background-color: #CCFFFF; table-layout: fixed;" class="largsize">
                                <tr>
                                    <td align="left" style="width: 2%;">
                                        <asp:Label ID="Label3" runat="server" Text="Grand Total" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 5%;">
                                        <asp:Label ID="Label4" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 3%;">
                                        <asp:Label ID="lblouterOpBal" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 3%;">
                                        <asp:Label ID="lblouterAdditionalamnt" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 3%;">
                                        <asp:Label ID="lblouterDelection" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 3%;">
                                        <asp:Label ID="Label13" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 3%;">
                                        <asp:Label ID="lblouterClosingBalance" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 3%;">
                                    </td>
                                    <td align="right" style="width: 3%;">
                                        <asp:Label ID="lblouterDepriciation" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 3%;">
                                        <asp:Label ID="lblouterbalance" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
