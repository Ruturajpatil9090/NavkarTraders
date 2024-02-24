<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptMillPaymentForGST.aspx.cs"
    Inherits="Report_rptMillPaymentForGST" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mill Payment</title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body style="font-family:Calibri;font-size:12px;width:1100px; text-align:center;" >');
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
</head>
<body>
    <form id="form1" runat="server">
    <div style="border-bottom: 1px dashed black;">
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" />
    </div>
    <div style="margin: 0 auto; width: 100%; margin-top: 12px;">
        <asp:Panel runat="server" ID="pnlMain">
            <table border="0" cellpadding="1" cellspacing="1" width="100%" style="border-bottom: 1px solid black;
                border-top: 1px solid black;">
                <tr>
                    <td style="width: 10%;">
                        <asp:Label ID="Label1" Text="Mill Code" Font-Bold="true" runat="server" />
                    </td>
                    <td style="width: 10%;">
                        <asp:Label ID="Label2" Text="Mill Name" Font-Bold="true" runat="server" />
                    </td>
                    <td style="width: 3%;" align="left">
                        <asp:Label ID="Label22" Text="Do No" Font-Bold="true" runat="server" />
                    </td>
                    <td style="width: 5%;" align="right">
                        <asp:Label ID="Label3" Text="Quintal" Font-Bold="true" runat="server" />
                    </td>
                    <td style="width: 5%;" align="right">
                        <asp:Label ID="Label4" Text="Mill Rate" Font-Bold="true" runat="server" />
                    </td>
                    <td style="width: 6%;" align="right">
                        <asp:Label ID="Label7" Text="GST Rate" Font-Bold="true" runat="server" />
                    </td>
                    <td style="width: 10%;" align="right">
                        <asp:Label ID="Label8" Text="Total GST Rate" Font-Bold="true" runat="server" />
                    </td>
                    <td style="width: 9%;" align="right">
                        <asp:Label ID="Label5" Text="Mill Amount" Font-Bold="true" runat="server" />
                    </td>
                    <td style="width: 9%;" align="right">
                        <asp:Label ID="Label11" Text="TCS Rate" Font-Bold="true" runat="server" />
                    </td>
                    <td style="width: 9%;" align="right">
                        <asp:Label ID="Label13" Text="TCS Amt" Font-Bold="true" runat="server" />
                    </td>
                    <td style="width: 9%;" align="right">
                        <asp:Label ID="Label18" Text="TDS Rate" Font-Bold="true" runat="server" />
                    </td>
                    <td style="width: 9%;" align="right">
                        <asp:Label ID="Label19" Text="TDS Amt" Font-Bold="true" runat="server" />
                    </td>
                    <td style="width: 9%;" align="right">
                        <asp:Label ID="Label15" Text="TCS NetPayable" Font-Bold="true" runat="server" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataList runat="server" ID="dtlList" Width="100%" OnItemDataBound="dtlList_OnItemDataBound">
                            <ItemTemplate>
                                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                    <tr>
                                        <td style="width: 5%; border-bottom: 1px solid black;">
                                            <asp:Label ID="lblMillCode" Text='<%#Eval("millcode") %>' Font-Bold="true" runat="server" />
                                        </td>
                                        <td style="width: 30%; border-bottom: 1px solid black;">
                                            <asp:Label ID="Label1" Text='<%#Eval("millname") %>' Font-Bold="true" runat="server" />
                                        </td>
                                        <td style="width: 10%; border-bottom: 1px solid black;">
                                            <asp:Label ID="Label5" Text='<%#Eval("Balance") %>' Font-Bold="true" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="width: 100%;">
                                            <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                <ItemTemplate>
                                                    <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                        <tr>
                                                            <td style="width: 10%;">
                                                            </td>
                                                            <td style="width: 10%;">
                                                            </td>
                                                            <td style="width: 3%;" align="left">
                                                                <asp:Label ID="Label23" Text='<%#Eval("dono") %>' Font-Bold="false" runat="server" />
                                                            </td>
                                                            <td style="width: 5%;" align="right">
                                                                <asp:Label ID="Label2" Text='<%#Eval("qtl") %>' Font-Bold="false" runat="server" />
                                                            </td>
                                                            <td style="width: 5%;" align="right">
                                                                <asp:Label ID="Label3" Text='<%#Eval("mill_rate") %>' Font-Bold="false" runat="server" />
                                                            </td>
                                                            <td style="width: 6%;" align="right">
                                                                <asp:Label ID="Label6" Text='<%#Eval("gst_rate") %>' Font-Bold="false" runat="server" />
                                                            </td>
                                                            <td style="width: 10%;" align="right">
                                                                <asp:Label ID="Label9" Text='<%#Eval("totalmillrate") %>' Font-Bold="false" runat="server" />
                                                            </td>
                                                            <td style="width: 9%;" align="right">
                                                                <asp:Label ID="Label4" Text='<%#Eval("millamount") %>' Font-Bold="false" runat="server" />
                                                            </td>
                                                            <td style="width: 9%;" align="right">
                                                                <asp:Label ID="Label12" Text='<%#Eval("TCS_Rate") %>' Font-Bold="false" runat="server" />
                                                            </td>
                                                            <td style="width: 9%;" align="right">
                                                                <asp:Label ID="Label14" Text='<%#Eval("TCSAmt") %>' Font-Bold="false" runat="server" />
                                                            </td>
                                                            <td style="width: 9%;" align="right">
                                                                <asp:Label ID="Label20" Text='<%#Eval("TDSRate") %>' Font-Bold="false" runat="server" />
                                                            </td>
                                                            <td style="width: 9%;" align="right">
                                                                <asp:Label ID="Label21" Text='<%#Eval("TDSAmt") %>' Font-Bold="false" runat="server" />
                                                            </td>
                                                            <td style="width: 9%;" align="right">
                                                                <asp:Label ID="Label16" Text='<%#Eval("TCSNetPayable") %>' Font-Bold="false" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="width: 100%; border: 1px solid black">
                                            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                <tr>
                                                    <td style="width: 10%;">
                                                    </td>
                                                    <td style="width: 10%;">
                                                        Total
                                                    </td>
                                                     <td style="width: 3%;" align="left">
                                                        <asp:Label ID="Label24" Text="" Font-Bold="true" runat="server" />
                                                    </td>
                                                    <td style="width: 5%;" align="right">
                                                        <asp:Label ID="lblTotalQuintal" Text="" Font-Bold="true" runat="server" />
                                                    </td>
                                                    <td style="width: 10%;" align="right">
                                                        <asp:Label ID="Label3" Text="" Font-Bold="false" runat="server" />
                                                    </td>
                                                    <td style="width: 10%;" align="right">
                                                        <asp:Label ID="Label10" Text="" Font-Bold="false" runat="server" />
                                                    </td>
                                                    <td style="width: 10%;" align="right">
                                                        <asp:Label ID="lblTotalAmount" Text="" Font-Bold="true" runat="server" />
                                                    </td>
                                                    <td style="width: 10%;">
                                                    </td>
                                                    <td style="width: 10%;" align="right">
                                                        <asp:Label ID="lblTotalTCSAmt" Text="" Font-Bold="true" runat="server" />
                                                    </td>
                                                    <td style="width: 10%;">
                                                    </td>
                                                    <td style="width: 8%;" align="right">
                                                        <asp:Label ID="lblTotalTDSAmt" Text="" Font-Bold="true" runat="server" />
                                                    </td>
                                                    <td style="width: 10%;" align="right">
                                                        <asp:Label ID="lblTotalNteTCSAmt" Text="" Font-Bold="true" runat="server" />
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
                    <td colspan="3" style="width: 100%; border: 1px solid black">
                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                            <tr>
                                <td style="width: 10%;">
                                </td>
                                <td style="width: 10%;">
                                    Total
                                </td>
                                <td style="width: 10%;" align="right">
                                    <asp:Label ID="lblGrandTotalQuintal" Text="" Font-Bold="true" runat="server" />
                                </td>
                                <td style="width: 10%;" align="right">
                                    <asp:Label ID="Label17" Text="" Font-Bold="false" runat="server" />
                                </td>
                                <td style="width: 10%;" align="right">
                                    <asp:Label ID="Label10" Text="" Font-Bold="false" runat="server" />
                                </td>
                                <td style="width: 10%;">
                                </td>
                                <td style="width: 10%;" align="right">
                                    <asp:Label ID="lblGrandTotalAmount" Text="" Font-Bold="true" runat="server" />
                                </td>
                                <td style="width: 10%;">
                                </td>
                                <td style="width: 10%;" align="right">
                                    <asp:Label ID="lblGrandTotalTCSAmt" Text="" Font-Bold="true" runat="server" />
                                </td>
                                <td style="width: 10%;">
                                </td>
                                <td style="width: 8%;" align="right">
                                    <asp:Label ID="lblGrandTotalTDSAmt" Text="" Font-Bold="true" runat="server" />
                                </td>
                                <td style="width: 10%;" align="right">
                                    <asp:Label ID="lblGrandTotalNteTCSAmt" Text="" Font-Bold="true" runat="server" />
                                </td>
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
