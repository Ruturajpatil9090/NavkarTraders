<%@ Page Title="rpt:Partywise DO" Language="C#" AutoEventWireup="true" CodeFile="rptPartyWiseDO.aspx.cs"
    Inherits="Report_rptPartyWiseDO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Partywise DO</title>
    <link rel="stylesheet" href="../print.css" type="text/css" media="print" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('sugarStock.html', 'st', 'height=400,width=800');
            printWindow.document.write('<html><head><link rel="stylesheet" href="../print.css" type="text/css" media="print" />');
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
    <script type="text/javascript" language="javascript">
        function memo() {
            window.open('../Sugar/pgeMotorMemo.aspx');    //R=Redirected  O=Original
        }
        function sugarpurchase() {
            window.open('../Sugar/pgeSugarPurchase.aspx');    //R=Redirected  O=Original
        }
        function loadingvoucher() {
            window.open('../Sugar/pgeLoadingvoucher.aspx');    //R=Redirected  O=Original
        }
        function salebill(DO, Action) {
            // window.open('../Sugar/pgeSugarsale.aspx');
            window.open('../Outword/pgrSugarsaleForGSTxml.aspx?saleid=' + DO + '&Action=' + Action);     //R=Redirected  O=Original
            //R=Redirected  O=Original
        }
        function openDO(DO, Action) {
            window.open('../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx?DO=' + DO + '&Action=' + Action);     //R=Redirected  O=Original

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="btnPrint" OnClientClick="PrintPanel();" Text="Print" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" /></div>
    <div>
        <asp:Panel runat="server" ID="pnlMain" CssClass="largsize">
            <table width="90%" align="center" cellspacing="4" style="border-bottom: 1px solid black;
                table-layout: fixed;" class="largsize">
                <tr>
                    <td style="width: 100%; text-align: center;">
                        <asp:Label runat="server" ID="lblCmpName" ForeColor="Navy" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: center;">
                        <asp:Label runat="server" ID="lblReportName" ForeColor="Black" Text="Partywise Delivery Order"
                            Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left;">
                        <asp:Label runat="server" ID="lblTransportName" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="90%" align="center" cellspacing="1" style="table-layout: fixed; border-top: 1px solid black;"
                class="largsize">
                <tr>
                    <td style="width: 2%;" align="center">
                        <asp:Label runat="server" ID="label1" Text="DO.#" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 4%;" align="center">
                        <asp:Label runat="server" ID="label2" Text="Date" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 6%;" align="left">
                        <asp:Label runat="server" ID="label3" Text="Mill" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 4%;" align="center">
                        <asp:Label runat="server" ID="label4" Text="Grade" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 3%;" align="center">
                        <asp:Label runat="server" ID="label5" Text="Quintal" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 3%;" align="center">
                        <asp:Label runat="server" ID="label6" Text="M Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 5%;" align="center">
                        <asp:Label runat="server" ID="label7" Text="Lorry No" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 3%;" align="center">
                        <asp:Label runat="server" ID="label8" Text="S Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 8%;" align="left">
                        <asp:Label runat="server" ID="label9" Text="Dispatch To" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;visibility:hidden;" align="left">
                        <asp:Label runat="server" ID="label18" Text="OV" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;" align="left">
                        <asp:Label runat="server" ID="label19" Text="SB" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="90%" cellpadding="0" cellspacing="1" style="border-top: 1px solid black;"
                align="center" class="largsize">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataList runat="server" ID="dtlist" Width="100%" OnItemDataBound="dtlist_OnItemDataBound">
                            <ItemTemplate>
                                <table width="100%" align="center" cellspacing="2" style="background-color: #FFFFCC;
                                    table-layout: fixed;" class="largsize">
                                    <tr>
                                        <td style="width: 1%;" align="center">
                                            <asp:Label runat="server" ID="lblPartyCode" Text='<%#Eval("code") %>' Visible="false"
                                                Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="width: 15%;" align="left">
                                            <asp:Label runat="server" ID="lblPartyName" Text='<%#Eval("getpass") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="width: 7%;" align="center">
                                            <asp:Label runat="server" ID="lblQntlTotal" Text="Qntl" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="width: 3%;" align="center">
                                            <asp:Label runat="server" ID="label6" Text=""></asp:Label>
                                        </td>
                                        <td style="width: 5%;" align="center">
                                            <asp:Label runat="server" ID="label7" Text=""></asp:Label>
                                        </td>
                                        <td style="width: 3%;" align="center">
                                            <asp:Label runat="server" ID="label8" Text=""></asp:Label>
                                        </td>
                                        <td style="width: 8%;" align="left">
                                            <asp:Label runat="server" ID="label9" Text=""></asp:Label>
                                        </td>
                                        <td style="width: 2%;visibility:hidden;" align="left">
                                            <asp:Label runat="server" ID="label18" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="width: 2%;" align="left">
                                            <asp:Label runat="server" ID="label19" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" cellspacing="3" class="largsize">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                <ItemTemplate>
                                                    <table width="100%" align="center" cellspacing="3" style="table-layout: fixed; background-color: #CCFFFF;
                                                        border-bottom: 1px dashed black;" class="largsize">
                                                        <tr>
                                                            <td style="width: 2%;" align="center">
                                                              <%-- <asp:Label runat="server" ID="label10" Text='<%#Eval("#") %>' Font-Bold="false"></asp:Label>--%>
                                                                <asp:LinkButton runat="server" ID="lnkDO" Text='<%#Eval("doc_no") %>' Font-Bold="false"
                                                                    Style="text-decoration: none;" OnClick="lnkDO_Click"></asp:LinkButton>
                                                            </td>
                                                            <td style="width: 4%;" align="center">
                                                                <asp:Label runat="server" ID="label11" Text='<%#Eval("dt") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 6%;" align="left">
                                                                <asp:Label runat="server" ID="label3" Text='<%#Eval("mill") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 4%;" align="center">
                                                                <asp:Label runat="server" ID="label12" Text='<%#Eval("Grade") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 3%;" align="center">
                                                                <asp:Label runat="server" ID="label13" Text='<%#Eval("Qntl") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 3%;" align="center">
                                                                <asp:Label runat="server" ID="label14" Text='<%#Eval("MR") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 5%;" align="center">
                                                                <asp:Label runat="server" ID="label15" Text='<%#Eval("lorry") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 3%;" align="center">
                                                                <asp:Label runat="server" ID="label16" Text='<%#Eval("SR") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 8%;" align="left">
                                                                <asp:Label runat="server" ID="label17" Text='<%#Eval("DispTo") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 2%;" align="left">
                                                                <asp:LinkButton runat="server" ID="lnkOV" Text='<%#Eval("OV") %>' Font-Bold="false"
                                                                    Style="text-decoration: none;" OnClick="lnkOV_Click" Visible="false"></asp:LinkButton>
                                                            </td>
                                                            <td style="width: 2%;" align="left">
                                                                <asp:LinkButton runat="server" ID="lnkSB" Text='<%#Eval("SB") %>' Font-Bold="false"
                                                                    Style="text-decoration: none;" OnClick="lnkSB_Click"></asp:LinkButton>
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
