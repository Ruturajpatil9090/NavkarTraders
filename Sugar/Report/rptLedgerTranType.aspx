<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptLedgerTranType.aspx.cs" Inherits="Report_rptLedgerTranType" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ledger</title>
    <link href="../print.css" media="print" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=PrintPanel.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head><link href="../print.css" media="print" rel="Stylesheet" type="text/css" />');
            printWindow.document.write('</head><body>');
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
        function OV() {
            window.open('../Sugar/pgeLoadingvoucher.aspx');
        }

        function JS() {
            window.open('../Sugar/pgeJawakSaleBill.aspx');
        }
        function DO() {
            window.open('../Sugar/pgeDeliveryOrderForGST.aspx');

        }
        function DOOld() {
            window.open('../Sugar/pgeDeliveryOrder2.aspx');
        }
        function LV() {
            window.open('../Sugar/pgeLocalVoucherForGST.aspx');
        }
        function LVOld() {
            window.open('../Sugar/pgeLocalvoucher.aspx');
        }
        function SB() {
            window.open('../Sugar/pgrSugarsaleForGST.aspx');
        }
        function SBOld() {
            window.open('../Sugar/pgeSugarsale.aspx');
        }
        function PS() {
            window.open('../Sugar/pgeSugarPurchaseForGST.aspx');
        }
        function PSOld() {
            window.open('../Sugar/pgeSugarPurchase.aspx');
        }
        function JV() {
            window.open('../Sugar/pgeJournalvoucher.aspx');
        }
        function RS() {
            window.open('../Sugar/pgeSugarsaleReturnForGST.aspx');
        }
        function RSOld() {
            window.open('../Sugar/pgeSugarsaleReturn.aspx');
        }

        function PR() {
            window.open('../Sugar/pgeSugarPurchaseReturn.aspx');
        }
        function UT() {
            window.open('../Sugar/pgeUtrentry.aspx');
        }
        function RP() {
            window.open('../Sugar/pgeReceiptPayment.aspx');

        }
        function AW() {
            window.open('../Sugar/pgeAwak.aspx');

        }
        function Cheq() {
            window.open('../Sugar/pgeChqPrinting.aspx');

        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 74%;
        }
        .style2
        {
            width: 21%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="left">
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" OnClick="btnPrint_Click" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSendEmail" runat="server" Text="Send To Email" CssClass="btnHelp"
            OnClick="btnSendEmail_Click" />&nbsp;&nbsp; Email:<asp:TextBox runat="server" ID="txtEmail"
                Width="300px"></asp:TextBox>&nbsp;&nbsp;<asp:Button runat="server" ID="btpGrdprint"
                    Text="PrintGrid" OnClick="btpGrdprint_Click" Visible="false" />
        <asp:Button Text="CREATE FILES" runat="server" CssClass="btnHelp" ID="btnCreateb2b"
            OnClick="btnCreateb2b_Click" />
        <asp:GridView ID="grdCsv" runat="server" AutoGenerateColumns="true">
        </asp:GridView>
    </div>
    <div>
        <asp:Panel ID="PrintPanel" runat="server" align="center" Font-Names="Calibri">
            <table width="68%" align="center" class="printwithmargin" id="tblMain" runat="server"
                style="table-layout: fixed;" cellspacing="2">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblCompany" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" align="center">
                            <tr>
                                <td align="left" style="width: 70%;">
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label1" runat="server" Text="Account Statement of :" Font-Bold="true"
                                        Font-Size="Medium"></asp:Label>
                                    <asp:Label ID="lblParty" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label><asp:Label
                                        ID="lblAcCode" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                </td>
                                <td align="right" style="width: 30%;">
                                    <asp:Label ID="Label2" runat="server" Text="From:" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                    &nbsp; &nbsp;<asp:Label ID="lblFromDt" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                    &nbsp;
                                    <asp:Label ID="Label3" runat="server" Text="To:" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                    &nbsp;&nbsp;<asp:Label ID="lblToDt" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="White"
                            GridLines="Both" HeaderStyle-ForeColor="Black" HeaderStyle-Height="30px" EmptyDataText="No Records found"
                            Width="1000px" CellPadding="9" CellSpacing="0" Font-Bold="false" ForeColor="Black"
                            RowStyle-Height="30px" ShowFooter="true" Font-Names="Verdana" Font-Size="15px"
                            OnRowDataBound="grdDetail_RowDataBound" CssClass="print">
                            <Columns>
                                <asp:BoundField DataField="TranType" HeaderText="Type" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="40px" HeaderStyle-CssClass="thead" />
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkGo" OnClick="lnkGo_Click" Style="color: Black;
                                            text-decoration: none;" Text='<%#Eval("DocNo") %>' ToolTip="Go To Document"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="150px" HeaderStyle-CssClass="thead" />
                                <asp:BoundField DataField="Narration" HeaderText="Narration" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-VerticalAlign="Bottom" ItemStyle-Width="800px" HeaderStyle-CssClass="thead" />
                                <asp:BoundField DataField="Debit" HeaderText="Debit" ItemStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead" />
                                <asp:BoundField DataField="Credit" HeaderText="Credit" ItemStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead" />
                                <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead" />
                                <asp:BoundField DataField="DrCr" HeaderText="DrCr" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="50px" HeaderStyle-CssClass="thead" />
                            </Columns>
                            <RowStyle Height="10px" Wrap="true" ForeColor="Black" />
                            <FooterStyle BackColor="Yellow" Font-Bold="true" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
