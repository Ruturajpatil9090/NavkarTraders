<%@ Page Title="rpt:Partywise Sugar Stock" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="rptPartyWiseSugarStock.aspx.cs"
    Inherits="Report_rptPartyWiseSugarStock"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('', 'st', 'height=400,width=800');
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

        function pst() {
            window.open('../Report/rptSugarBalanceStocks.aspx');
        }
        function lwst() {
            window.open('../Report/rptLiftingWiseSugarBalanceStock.aspx');
        }
        function sp(Tenderno,accode, Action) {
            var tn;
            window.open('../BussinessRelated/pgeTenderPurchasexml.aspx?Tenderno='+accode+'&tenderid=' + accode + '&Action=' + Action);    //R=Redirected  O=Original
        }
        function sst() {
            window.open('../Report/rptSelfSugarBalanceStock.aspx');
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 2%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="btnPrint" OnClientClick="PrintPanel();" Text="Print" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
        <asp:Button runat="server" ID="btnmillwiseliftingwise" OnClick="btnmillwiseliftingwise_Click" Text="Suger Balance Stock" />
        <asp:Button runat="server" ID="btnmillwise" OnClick="btnmillwise_Click" Text="Mill wise lifting wise" />
        <asp:Button runat="server" ID="btnSelfStock" OnClick="btnSelfStock_Click" Text="Self Stock" />
    </div>
    <div>
        <asp:Panel runat="server" ID="pnlMain">
            <table width="90%" align="center" cellspacing="4">
                <tr>
                    <td style="width: 100%; text-align: center;">
                        <asp:Label runat="server" ID="lblCmpName" ForeColor="Navy" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: center;">
                        <asp:Label runat="server" ID="lblReportName" ForeColor="Black" Text="Partywise Sugar Balance Stock"
                            Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left;">
                        <asp:Label runat="server" ID="lblTransportName" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%" align="center"  style="table-layout: fixed; border-top: 2px solid black;">
                <tr>
                    <td style="width: 5%;" align="left">
                        <asp:Label runat="server" ID="label16" Text="TenderNO" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 4%;" align="left">
                        <asp:Label runat="server" ID="label1" Text="MillName" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 5%;" align="center">
                        <asp:Label runat="server" ID="label2" Text="Grade" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 3%;" align="center">
                        <asp:Label runat="server" ID="label3" Text="M Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 3%;" align="center">
                        <asp:Label runat="server" ID="label4" Text="S Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 3%;" align="center">
                        <asp:Label runat="server" ID="label5" Text="Lifting" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 4%;" align="left">
                        <asp:Label runat="server" ID="label6" Text="Do" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 3%;" align="center">
                        <asp:Label runat="server" ID="label7" Text="Quintal" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 3%;" align="center">
                        <asp:Label runat="server" ID="label8" Text="Desp" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 3%;" align="center">
                        <asp:Label runat="server" ID="label9" Text="Bal" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%" align="center" cellspacing="4" style="border-top: 2px solid black;">
                <tr>
                    <td style="width: 90%;">
                        <asp:DataList runat="server" ID="dtlist" Width="100%" OnItemDataBound="dtlist_OnItemDataBound">
                            <ItemTemplate>
                                <table width="100%" cellpadding="0" cellspacing="1" align="center" style="background-color: #FFFFCC;
                                    table-layout: fixed; border-bottom: 1px solid black;">
                                    <tr>
                                        <td style="width: 2%;" align="left">
                                            <asp:Label runat="server" ID="lblPartyCode" Text='<%#Eval("Buyer") %>' Visible="false"> </asp:Label>
                                        </td>
                                        <td style="width: 62%;" align="left">
                                            <asp:Label runat="server" ID="lblPartyName" Text='<%#Eval("buyername") %>' Font-Bold="true"> </asp:Label>
                                        </td>
                                        <td style="width: 7%;" align="center">
                                            <asp:Label runat="server" ID="lblDispTotal" Text="" Font-Bold="true"> </asp:Label>
                                        </td>
                                        <td style="width: 7%;" align="center">
                                            <asp:Label runat="server" ID="lblBalTotal" Text="" Font-Bold="true"> </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" cellpadding="0" cellspacing="3" align="center">
                                    <tr>
                                        <td style="width: 100%;">
                                            <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                <ItemTemplate>
                                                    <table width="100%" align="center" cellspacing="4" style="table-layout: fixed; background-color: #CCFFFF;
                                                        border-bottom: 1px dashed black;">
                                                        <tr>
                                                            <td style="width: 4%;" align="left">

                                                                <asp:LinkButton Style="text-decoration: none;" runat="server" ID="lnkDO" Text='<%#Eval("Tender_No") %>'
                                                                        OnClick="lnkDO_Click"></asp:LinkButton>
                                                                <%--<asp:Label runat="server" ID="label17" Text='<%#Eval("Tender_No") %>' Font-Bold="false"></asp:Label>--%>
                                                            </td>
                                                            <td style="width: 4%;" align="left">
                                                                <asp:Label runat="server" ID="label1" Text='<%#Eval("millshortname") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 5%;" align="center">
                                                                <asp:Label runat="server" ID="label2" Text='<%#Eval("Grade") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 3%;" align="center">
                                                                <asp:Label runat="server" ID="label3" Text='<%#Eval("Mill_Rate") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 3%;" align="center">
                                                                <asp:Label runat="server" ID="label4" Text='<%#Eval("Sale_Rate") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 3%;" align="center">
                                                                <asp:Label runat="server" ID="label5" Text='<%#Eval("Tender_Date") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 3%;" align="left">
                                                                <asp:Label runat="server" ID="label6" Text='<%#Eval("tenderdoname") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 3%;" align="center">
                                                                <asp:Label runat="server" ID="label7" Text='<%#Eval("Buyer_Quantal") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 3%;" align="center">
                                                                <asp:Label runat="server" ID="label8" Text='<%#Eval("DESPATCH") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 3%;" align="center">
                                                                <asp:Label runat="server" ID="label9" Text='<%#Eval("BALANCE") %>' Font-Bold="false"></asp:Label>
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
                <tr>
                    <td style="width: 100%;">
                        <table width="100%" align="center" cellspacing="4" style="table-layout: fixed; border-bottom: 1px solid black;
                            border-top: 1px solid black;">
                            <tr>
                                <td style="width: 5%;" align="center">
                                    <asp:Label runat="server" ID="label10" Text="" Font-Bold="false"></asp:Label>
                                </td>
                                <td style="width: 5%;" align="center">
                                    <asp:Label runat="server" ID="label11" Text="" Font-Bold="false"></asp:Label>
                                </td>
                                <td style="width: 3%;" align="center">
                                    <asp:Label runat="server" ID="label12" Text="" Font-Bold="false"></asp:Label>
                                </td>
                                <td style="width: 3%;" align="center">
                                    <asp:Label runat="server" ID="label13" Text="" Font-Bold="false"></asp:Label>
                                </td>
                                <td style="width: 3%;" align="center">
                                    <asp:Label runat="server" ID="label14" Text="" Font-Bold="false"></asp:Label>
                                </td>
                                <td style="width: 3%;" align="center">
                                    <asp:Label runat="server" ID="label15" Text="Grand Total:-" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 3%;" align="right">
                                    <asp:Label runat="server" ID="lblQntlGrandTotal" Text="Qntltotal" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 3%;" align="center">
                                    <asp:Label runat="server" ID="lblGrandDispTotal" Text="" Font-Bold="false"></asp:Label>
                                </td>
                                <td style="width: 3%;" align="center">
                                    <asp:Label runat="server" ID="lblGrandBalTotal" Text="" Font-Bold="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="90%" align="center" cellspacing="4" style="border-top: 2px solid black;">
                <%--<tr>
                    <td style="width: 100%;">
                        <iframe runat="server" id="ifrmCorporateSell" src="" width="100%" height="1000px">
                        </iframe>
                    </td>
                </tr>--%>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
