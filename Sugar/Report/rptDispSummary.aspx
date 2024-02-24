<%@ Page Title="rpt:Dispatch Summary" Language="C#" AutoEventWireup="true"  EnableEventValidation="false" CodeFile="rptDispSummary.aspx.cs"
    Inherits="Report_rptDispSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dispatch Summary</title>
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('', 'st', 'height=400,width=800');
            printWindow.document.write('<html><head><link href="../print.css" rel="stylesheet" type="text/css" media="print" />');
            printWindow.document.write('</head><body class="print3">');
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
        function sugarpurchase(doc_no, Action) {
            window.open('../Inword/pgeSugarPurchaseForGSTxml.aspx?purchaseid=' + doc_no + '&Action=' + Action);    //R=Redirected  O=Original
        }
        function loadingvoucher() {
            window.open('../Sugar/pgeLoadingvoucher.aspx');    //R=Redirected  O=Original
        }
        function salebill(doc_no, Action) {
            window.open('../Outword/pgrSugarsaleForGSTxml.aspx?saleid=' + doc_no + '&Action=' + Action);    //R=Redirected  O=Original
        }
        function DO(DO, Action) {
            window.open('../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx?DO=' + DO + '&Action=' + Action);     //R=Redirected  O=Original
        }
    </script>
    <script type="text/javascript" src="../JS/emailValidation.js"></script>
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
            <asp:Panel runat="server" ID="pnlMain" align="center" Font-Names="calibri" CssClass="print3">
                <asp:Label ID="lblReportName" runat="server" Text="Dispatch Summary" Width="100%"
                    CssClass="lblName" Font-Bold="true" Font-Size="Large" Style="text-align: center; width: 100%;"></asp:Label>
                <table id="Table1" runat="server" width="95%" align="center" cellspacing="2" style="table-layout: fixed; border-top: 1px solid black; border-bottom: 1px solid black;"
                    class="print3">
                    <tr>
                        <td style="width: 1%" align="center">
                            <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text="No"></asp:Label>
                        </td>
                        <td style="width: 2%" align="left">
                            <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Mill"></asp:Label>
                        </td>
                        <td style="width: 1%" align="left">
                            <asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Rate"></asp:Label>
                        </td>
                        <td style="width: 2%" align="center">
                            <asp:Label runat="server" ID="Label3" Font-Bold="true" Text="qntl"></asp:Label>
                        </td>
                        <td style="width: 5%" align="left">
                            <asp:Label runat="server" ID="Label4" Font-Bold="true" Text="Name Of Party"></asp:Label>
                        </td>
                        <td style="width: 5%" align="left">
                            <asp:Label runat="server" ID="Label14" Font-Bold="true" Text="Bill To"></asp:Label>
                        </td>
                        <td style="width: 5%" align="left">
                            <asp:Label runat="server" ID="Label15" Font-Bold="true" Text="Shift to"></asp:Label>
                        </td>
                        <td style="width: 3%" align="center">
                            <asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Truck No"></asp:Label>
                        </td>
                        <td style="width: 2%" align="center">
                            <asp:Label runat="server" ID="Label6" Font-Bold="true" Text="Transport"></asp:Label>
                        </td>
                        <td style="width: 2%" align="center">
                            <asp:Label runat="server" ID="Label7" Font-Bold="true" Text="DO"></asp:Label>
                        </td>
                        <td style="width: 1%" align="center">
                            <asp:Label runat="server" ID="Label8" Font-Bold="true" Text=" T_No"></asp:Label>
                        </td>
                        <td style="width: 1%" align="center">
                            <asp:Label runat="server" ID="Label9" Font-Bold="true" Text="T_Id"></asp:Label>
                        </td>
                        <td style="width: 1%" align="center">
                            <asp:Label runat="server" ID="Label10" Font-Bold="true" Text="PS"></asp:Label>
                        </td>
                        <td style="width: 1%" align="center">
                            <asp:Label runat="server" ID="Label11" Font-Bold="true" Text="SB"></asp:Label>
                        </td>
                        <td style="width: 1%" align="center">
                            <asp:Label runat="server" ID="Label12" Visible="false" Font-Bold="true" Text="OV"></asp:Label>
                        </td>
                        <td style="width: 1%" align="right">
                            <asp:Label runat="server" ID="Label13" Visible="false" Font-Bold="true" Text="MM"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="Table12" runat="server" align="center" width="95%" class="print3">
                    <tr>
                        <td align="center" style="width: 100%">
                            <asp:DataList runat="server" ID="datalist" Width="100%" OnItemDataBound="DataList_ItemDataBound">
                                <ItemTemplate>
                                    <table id="Table3" runat="server" width="100%" align="center" style="background-color: #CCFFFF;"
                                        class="print3">
                                        <tr>
                                            <td style="width: 25%" align="left">
                                                <asp:Label runat="server" ID="lbldodate" Text='<%#Eval("do_date") %>' Visible="true"
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td style="width: 65%" align="left">
                                                <asp:Label runat="server" ID="lblTotalQntl" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td style="width: 65%" align="left"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center">
                                                <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                    <ItemTemplate>
                                                        <table id="Table1" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed; background-color: #FFFFCC; border-bottom: 1px solid black;"
                                                            class="print3">
                                                            <tr>
                                                                <td style="width: 1%" align="center">
                                                                    <asp:LinkButton Style="text-decoration: none;" runat="server" ID="lnkDO" Text='<%#Eval("no") %>'
                                                                        OnClick="lnkDO_Click"></asp:LinkButton>
                                                                </td>
                                                                <td style="width: 2%" align="left">
                                                                    <asp:Label runat="server" ID="Label1" Font-Bold="false" Text='<%#Eval("mill") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 1%" align="left">
                                                                    <asp:Label runat="server" ID="Label2" Font-Bold="false" Text='<%#Eval("millrate") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 2%" align="center">
                                                                    <asp:Label runat="server" ID="Label3" Font-Bold="false" Text='<%#Eval("quantal") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 5%" align="left">
                                                                    <asp:Label runat="server" ID="Label4" Font-Bold="false" Text='<%#Eval("getpass") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 5%" align="left">
                                                                    <asp:Label runat="server" ID="Label16" Font-Bold="false" Text='<%#Eval("billto") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 5%" align="left">
                                                                    <asp:Label runat="server" ID="Label17" Font-Bold="false" Text='<%#Eval("shifto") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="center">
                                                                    <asp:Label runat="server" ID="Label5" Font-Bold="false" Text='<%#Eval("truck") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 2%" align="center">
                                                                    <asp:Label runat="server" ID="Label6" Font-Bold="false" Text='<%#Eval("transport") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 2%" align="center">
                                                                    <asp:Label runat="server" ID="Label7" Font-Bold="false" Text='<%#Eval("do") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 1%" align="right">
                                                                    <asp:Label runat="server" ID="Label8" Font-Bold="false" Text='<%#Eval("purcno") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 1%" align="right">
                                                                    <asp:Label runat="server" ID="Label9" Font-Bold="false" Text='<%#Eval("purcorder") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 1%" align="right">
                                                                    <asp:LinkButton Style="text-decoration: none;" runat="server" ID="lnkPS" Text='<%#Eval("PS") %>'
                                                                        OnClick="lnkPS_Click"></asp:LinkButton>
                                                                </td>
                                                                <td style="width: 1%" align="right">
                                                                    <asp:LinkButton Style="text-decoration: none;" runat="server" ID="lnkSB" Text='<%#Eval("SB") %>'
                                                                        OnClick="lnkSB_Click"></asp:LinkButton>
                                                                </td>
                                                                <td style="width: 1%" align="center">
                                                                    <asp:LinkButton Style="text-decoration: none;" runat="server" Visible="false" ID="lnkOV" Text='<%#Eval("VO") %>'
                                                                        OnClick="lnkOV_Click"></asp:LinkButton>
                                                                </td>
                                                                <td style="width: 1%" align="center">
                                                                    <asp:LinkButton Style="text-decoration: none;" Visible="false" runat="server" ID="lnkMM" Text='<%#Eval("MM") %>'
                                                                        OnClick="lnkMM_Click"></asp:LinkButton>
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
