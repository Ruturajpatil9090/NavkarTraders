<%@ Page Title="rpt:Ledger Short Payment With Zero Balance" Language="C#"  AutoEventWireup="true" CodeFile="rptShortPaymentLedgerWithZeroBalance.aspx.cs" Inherits="Report_rptShortPaymentLedgerWithZeroBalance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="../JS/emailValidation.js"></script>
    <link rel="stylesheet" type="text/css" href="../print.css" media="print" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('sugarStock.html', 'st', 'height=400,width=800');
            printWindow.document.write('<html><head><link rel="stylesheet" type="text/css" href="../print.css" media="print" />');
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
        <asp:Button runat="server" ID="btnPrint" OnClientClick="PrintPanel();" Text="Print" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
        &nbsp;&nbsp;<asp:Button runat="server" ID="btnSendEmail" Text="Email" OnClick="btnSendEmail_Click"
            OnClientClick="CheckEmail();" Width="79px" />
        &nbsp;<asp:TextBox runat="server" ID="txtEmail" Width="300px" Height="23px"></asp:TextBox>
    </div>
    <div>
        <asp:Panel ID="pnlMain" runat="server" align="center" Font-Names="Calibri" CssClass="largsize">
            <asp:Label ID="lblCompanyName" Width="90%" runat="server" Text="" Style="text-align: center;"
                CssClass="lblName" Font-Size="16px" Font-Bold="true"></asp:Label>
            <br />
            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                border-bottom: 2px solid black;" class="largsize">
                <tr>
                    <td align="right">
                        <table align="left" class="largsize">
                            <tr>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblBrokerName" Text="" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblDate" Font-Bold="false" Text="Date"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    herewith sending payment and shortpayment list.Please verify and send D.D Amount
                                    Urgently.
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                border-bottom: 2px solid black;" class="largsize">
                <tr>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="lblspdate" runat="server" Text="Date" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="Label8" runat="server" Text="PODetails" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 1%;">
                        <asp:Label ID="Label1" runat="server" Text="Ref" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 1%;">
                        <asp:Label ID="lblRefNo" runat="server" Text="No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 10%;">
                        <asp:Label ID="lblParty" runat="server" Text="Customer Name" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="Label9" runat="server" Text="Lorry" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="lblQntl" runat="server" Text="Quintal" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 6%;">
                        <asp:Label ID="lblMillName" runat="server" Text="Mill" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 3%;">
                        <asp:Label ID="lblAmount" runat="server" Text="Voc.Amount" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 2%;">
                        <asp:Label ID="lblRecieved" runat="server" Text="Recieved" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="lblShort" runat="server" Text="Short" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="Label6" runat="server" Text="ASN/GRN No." Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%" align="center" cellpadding="1" cellspacing="0" class="largsize">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataList runat="server" ID="dtlDetails" Width="100%" OnItemDataBound="dtlDetails_OnItemDataBound">
                            <ItemTemplate>
                                <table width="100%" align="center" cellpadding="1" cellspacing="3" style="table-layout: fixed;
                                    border-bottom: 1px dashed black; background-color: #CCFFFF;" class="largsize">
                                    <tr>
                                        <td align="center" style="width: 3%;">
                                            <asp:Label ID="lblDt" runat="server" Text='<%#Eval("dt") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 3%;">
                                            <asp:Label ID="Label8" runat="server" Text='<%#Eval("PODetails") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 1%;">
                                            <asp:Label ID="lblTtype" runat="server" Text='<%#Eval("ttype") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 1%;">
                                            <asp:Label ID="lblno" runat="server" Text='<%#Eval("#") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 10%;">
                                            <asp:Label ID="lblParty1" runat="server" Text='<%#Eval("Party") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 4%;">
                                            <asp:Label ID="Label9" runat="server" Text='<%#Eval("lorry") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 2%;">
                                            <asp:Label ID="lblQntl1" runat="server" Text='<%#Eval("Qntl") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 6%;">
                                            <asp:Label ID="lblMillName1" runat="server" Text='<%#Eval("Mill") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 3%;">
                                            <asp:Label ID="lblAmount1" runat="server" Text='<%#Eval("VocAmount") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 2%;">
                                            <asp:Label ID="lblRecieved1" runat="server" Text='<%#Eval("Recieved") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 3%;">
                                            <asp:Label ID="lblShort1" runat="server" Text='<%#Eval("Short") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 3%;">
                                            <asp:Label ID="Label7" runat="server" Text='<%#Eval("ASN") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" class="largsize">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:DataList runat="server" Width="100%" ID="detailsDtl">
                                                <ItemTemplate>
                                                    <table width="100%" align="center" cellpadding="1" cellspacing="2" style="table-layout: fixed;
                                                        border-bottom: 1px dashed black; background-color: #FFFFFF;" class="largsize">
                                                        <tr>
                                                            <td style="width: 10%;" align="center">
                                                                <asp:Label runat="server" ID="lbl1" Text='<%#Eval("#") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 10%;" align="center">
                                                                <asp:Label runat="server" ID="Label2" Text='<%#Eval("Ptype") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 10%;" align="center">
                                                                <asp:Label runat="server" ID="Label3" Text='<%#Eval("tran_date") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 35%;" align="left">
                                                                <asp:Label runat="server" ID="Label4" Text='<%#Eval("narration") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 18%;" align="center">
                                                                <asp:Label runat="server" ID="Label5" Text='<%#Eval("PayAmt") %>' Font-Bold="true"></asp:Label>
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
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>

