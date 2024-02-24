<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptLedger.aspx.cs" Inherits="Reports_rptLedger"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ledger</title>
    <%--    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    --%>
    <script src="../JQuery/jquery.js" type="text/javascript"></script>
    <link href="../print.css" media="print" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function PrintPanel() {
            debugger;
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
        function DOxml(accode, Action) {
            window.open('../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx?DO=' + accode + '&Action=' + Action);
        }

        function LVxml(accode, Action, Tran_Type) {
            window.open('../Outword/pgeLocalVoucherForGSTxmlNew.aspx?commissionid=' + accode + '&Action=' + Action + '&Tran_Type=' + Tran_Type);
        }
        function CVxml(accode, Action, Tran_Type) {
            window.open('../Outword/pgeLocalVoucherForGSTxmlNew.aspx?commissionid=' + accode + '&Action=' + Action + '&Tran_Type=' + Tran_Type);
        }
        function SBxml(accode, Action) {
            window.open('../Outword/pgrSugarsaleForGSTxml.aspx?saleid=' + accode + '&Action=' + Action);
        }
        function PSxml(accode, Action) {
            window.open('../Inword/pgeSugarPurchaseForGSTxml.aspx?purchaseid=' + accode + '&Action=' + Action);
        }
        function UTxml(accode, Action) {
            window.open('../Transaction/pgeUtrentryxml.aspx?utrid=' + accode + '&Action=' + Action);
        }
        function xml(accode, Action) {
            window.open('../Transaction/pgeJournal_Voucher.aspx?tranid=' + accode + '&Action=' + Action);
        }
        function PRxml(accode, Action) {
            window.open('../Inword/pgeSugarPurchaseReturnForGST.aspx?prid=' + accode + '&Action=' + Action);
        }
        function RSxml(accode, Action) {
            window.open('../Outword/pgeSugarsaleReturnForGST.aspx?srid=' + accode + '&Action=' + Action);
        }
        function RPxml(accode, Action, tran_type) {
            window.open('../Transaction/pgeReceiptPaymentxml.aspx?tranid=' + accode + '&Action=' + Action + '&tran_type=' + tran_type);
        }
        function DNxml(accode, Action, tran_type) {
            window.open('../Transaction/pgeDebitCreditNote.aspx?dcid=' + accode + '&Action=' + Action + '&tran_type=' + tran_type);
        }
        function RBxml(accode, Action) {
            window.open('../Outword/pgeServiceBill.aspx?rbid=' + accode + '&Action=' + Action);
        }


        function RPBxml(accode, Action) {
            window.open('../Outword/pgeRetailPurchase.aspx?retailid=' + accode + '&Action=' + Action);
        }
        function RRxml(accode, Action, tran_type) {
            window.open('../Outword/pgeRetailSale.aspx?retailid=' + accode + '&Action=' + Action + '&tran_type=' + tran_type);
        }
        function CBxml(accode, Action) {
            window.open('../Inword/pgeColdStorage.aspx?csid=' + accode + '&Action=' + Action);
        }

        function OIxml(accode, Action) {
            window.open('../Inword/pgeOther_GST_Input.aspx?Oid=' + accode + '&Action=' + Action);
        }
        function JSxml(accode, Action) {
            window.open('../Jaggary/pgeJawakSaleBill.aspx?billno=' + accode + '&Action=' + Action);
        }
        function AWxml(accode, Action) {
            window.open('../Jaggary/pgeAwak.aspx?doc_no=' + accode + '&Action=' + Action);
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#scrollToBottom').bind("click", function () {
                $('html, body').animate({ scrollTop: $(document).height() }, 0);
                return false;
            });
            $('#scrollToTop').bind("click", function () {
                $('html, body').animate({ scrollTop: 0 }, 0);
                return false;
            });
        });
    </script>
    <style type="text/css">
        .style1 {
            width: 74%;
        }

        .style2 {
            width: 21%;
        }
    </style>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        #scrollToTop, #scrollToBottom {
            cursor: pointer;
            background-color: #ed87c0;
            display: inline-block;
            height: 40px;
            width: 40px;
            color: #fff;
            font-size: 16pt;
            text-align: center;
            text-decoration: none;
            line-height: 40px;
            -webkit-border-radius: 60px;
            -moz-border-radius: 60px;
            border-radius: 60px;
        }

            #scrollToTop:hover {
                color: #000;
                background-color: #fff;
            }

            #scrollToBottom:hover {
                color: #000;
                background-color: #fff;
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
        <asp:Button ID="btnPDFDownload" runat="server" Width="110px" Text="PDF Download"
            OnClick="btnPDfDownload_Click" />
            <asp:Button ID="btnpdf" runat="server" Width="70px" Text="PDF Mail" OnClick="btnPDf_Click" />
            <asp:Button ID="btnSendEmail" runat="server" Text="Send To Email" CssClass="btnHelp"
                OnClick="btnSendEmail_Click" />&nbsp;&nbsp; Email:<asp:TextBox runat="server" ID="txtEmail"
                    Width="300px"></asp:TextBox>&nbsp;&nbsp;<asp:Button runat="server" ID="btpGrdprint"
                        Text="PrintGrid" OnClick="btpGrdprint_Click" Visible="false" />
            <div align="right">
                <a href="javascript:;" id="scrollToBottom">&#x25BC;</a>
            </div>
        </div>
        <div>
            <asp:Panel runat="server" ID="PrintPanelmain" align="center" Font-Names="Calibri"
                Width="100%">
                <asp:Panel ID="PrintPanel" runat="server" align="center" Font-Names="Calibri">
                    <table width="80%" align="center" class="printwithmargin" id="tblMain" runat="server"
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
                                        <td align="left" style="width: 70%;">&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label1" runat="server" Text="Account Statement of :" Font-Bold="true"
                                            Font-Size="Medium"></asp:Label>
                                            <asp:Label ID="lblParty" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label><asp:Label
                                                ID="lblAcCode" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>

                                            <asp:Label ID="Label25" runat="server" Text="City :" Font-Bold="true"
                                                Font-Size="Medium"></asp:Label>
                                            <asp:Label ID="lblcity" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>

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
                            <td>
                                <table width="150%">
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="White"
                                                GridLines="Both" HeaderStyle-ForeColor="Black" HeaderStyle-Height="30px" EmptyDataText="No Records found"
                                                Width="1000px" CellPadding="9" CellSpacing="0" Font-Bold="false" ForeColor="Black"
                                                RowStyle-Height="30px" ShowFooter="true" Font-Names="Verdana" Font-Size="Medium"
                                                OnRowDataBound="grdDetail_RowDataBound" CssClass="print" AllowPaging="false"
                                                PageSize="50" OnPageIndexChanging="grdDetails_PageIndexChanging">
                                                <Columns>
                                                    <%-- <asp:TemplateField HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkDONOGo" OnClick="lnkDONOGo_Click" Style="color: Black;
                                                            text-decoration: none;" Text='<%#Eval("DONO") %>' ToolTip="Go To Document"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <asp:BoundField DataField="TranType" HeaderText="Type" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30px" HeaderStyle-CssClass="thead" />
                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lnkGo" OnClick="lnkGo_Click" Style="color: Black; text-decoration: none;"
                                                                Text='<%#Eval("DocNo") %>' ToolTip="Go To Document"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="left"
                                                        ItemStyle-Width="150px" HeaderStyle-CssClass="thead" />
                                                    <asp:BoundField DataField="Narration" HeaderText="Narration" ItemStyle-HorizontalAlign="Left"
                                                        ItemStyle-VerticalAlign="Bottom" ItemStyle-Width="300px" HeaderStyle-CssClass="thead" />
                                                    <asp:BoundField DataField="Debit" HeaderText="Debit" ItemStyle-HorizontalAlign="Right"
                                                        ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead"
                                                        DataFormatString="{0:f2}" />
                                                    <asp:BoundField DataField="Credit" HeaderText="Credit" ItemStyle-HorizontalAlign="Right"
                                                        ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead"
                                                        DataFormatString="{0:f2}" />
                                                    <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-HorizontalAlign="Right"
                                                        ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead"
                                                        DataFormatString="{0:f2}" />
                                                    <asp:BoundField DataField="DrCr" HeaderText="DrCr" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="50px" HeaderStyle-CssClass="thead" />
                                                    <asp:TemplateField HeaderText="Do_No">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lnkGo1" OnClick="lnkGo1_Click" Style="color: Black; text-decoration: none;"
                                                                Text='<%#Eval("DO_NO") %>' ToolTip="Go To Document"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:BoundField DataField="Do_No" HeaderText="Do_No" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="50px" HeaderStyle-CssClass="thead" />--%>
                                                </Columns>
                                                <RowStyle Height="10px" Wrap="true" ForeColor="Black" />
                                                <FooterStyle BackColor="Yellow" Font-Bold="true" />
                                            </asp:GridView>
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
    <div align="right">
        <a href="javascript:;" id="scrollToTop">&#x25B2;</a>
    </div>
</body>
</html>
