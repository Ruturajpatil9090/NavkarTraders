<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptJaggerryDetailSalebill.aspx.cs" Inherits="Sugar_Report_rptJaggerryDetailSalebill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('', 'st', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body style="font-family:Calibri;font-size:12px;width:1500px; text-align:center;" >');
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
        function TN() {
            window.open('../Sugar/pgeTenderPurchase.aspx');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="btnPrint" OnClientClick="PrintPanel();" Text="Print" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
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
                        <asp:Label runat="server" ID="lblReportName" ForeColor="Black" Text="Jaggery Sale Bill Detail"
                            Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left;">
                        <asp:Label runat="server" ID="lblTransportName" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="90%" align="center" cellspacing="1" style="table-layout: fixed; border-top: 2px solid black;
                background-color: #D3D3D3;">
                <tr>
                    <td style="width: 15%;" align="left">
                        <asp:Label runat="server" ID="label1" Text="Item Name" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 5%;" align="right">
                        <asp:Label runat="server" ID="label3" Text="Qty" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;" align="right">
                    </td>
                    <td style="width: 5%;" align="right">
                        <asp:Label runat="server" ID="label4" Text="Net_Wt" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;" align="right">
                    </td>
                    <td style="width: 5%;" align="right">
                        <asp:Label runat="server" ID="label19" Text="S_Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;" align="right">
                    </td>
                    <td style="width: 5%;" align="right">
                        <asp:Label runat="server" ID="label7" Text="C_Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;" align="right">
                    </td>
                    <td style="width: 5%;" align="right">
                        <asp:Label runat="server" ID="label26" Text="CS_Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;" align="right">
                    </td>
                    <td style="width: 5%;" align="right">
                        <asp:Label runat="server" ID="label29" Text="S_value" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;" align="right">
                    </td>
                    <td style="width: 5%;" align="right">
                        <asp:Label runat="server" ID="label8" Text="Levi" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;" align="right">
                    </td>
                    <td style="width: 5%;" align="right">
                        <asp:Label runat="server" ID="label9" Text="P_Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;" align="right">
                    </td>
                    <td style="width: 5%;" align="right">
                        <asp:Label runat="server" ID="label13" Text="P_Amnt" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;" align="right">
                    </td>
                    <td style="width: 5%;" align="right">
                        <asp:Label runat="server" ID="label14" Text="H_Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;" align="right">
                    </td>
                    <td style="width: 5%;" align="right">
                        <asp:Label runat="server" ID="label20" Text="H_Amnt" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;" align="right">
                    </td>
                    <td style="width: 5%;" align="right">
                        <asp:Label runat="server" ID="label22" Text="Ins_Amt" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;" align="right">
                    </td>
                    <td style="width: 0%; visibility: hidden;" align="right">
                        <asp:Label runat="server" ID="label24" Text="Panjar_Amnt" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 2%;" align="right">
                    </td>
                    <td style="width: 8%;" align="right">
                        <asp:Label runat="server" ID="label25" Text="Sale Amnt" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="90%" align="center" cellspacing="4" style="border-top: 2px solid black;">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataList runat="server" ID="dtlist" Width="100%" OnItemDataBound="dtlist_OnItemDataBound">
                            <ItemTemplate>
                                <table width="100%" cellpadding="0" cellspacing="1" align="center" style="background-color: #FFFFCC;
                                    table-layout: fixed; border-bottom: 1px solid black; border-top: 3px solid black;">
                                    <tr>
                                        <td style="width: 5%; font-weight: bold;" align="left">
                                            <%--<asp:Label runat="server" ID="lblitemcode" Text='<%#Eval("Item_Code") %>' Visible="false"> </asp:Label>--%>
                                            Date: &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label runat="server" ID="label30" Text='<%#Eval("Doc_Date") %>' Font-Bold="true"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <%-- </td>
                                        <td style="width: 10%;" align="left">--%>
                                            Bill No:&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label runat="server" ID="lbldocno" Text='<%#Eval("doc_no") %>' Font-Bold="true"> </asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp; Party Name:&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label runat="server" ID="Label10" Text='<%#Eval("PartyName") %>' Font-Bold="true"> </asp:Label>
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
                                                            <td style="width: 15%;" align="left">
                                                                <asp:Label runat="server" ID="label1" Text='<%#Eval("Itemname") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 5%;" align="right">
                                                                <asp:Label runat="server" ID="label3" Text='<%#Eval("Qty") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 2%;" align="right">
                                                            </td>
                                                            <td style="width: 5%;" align="right">
                                                                <asp:Label runat="server" ID="label4" Text='<%#Eval("Net_Wt") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 2%;" align="right">
                                                            </td>
                                                            <td style="width: 5%;" align="right">
                                                                <asp:Label runat="server" ID="label6" Text='<%#Eval("Sale_Rate") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 2%;" align="right">
                                                            </td>
                                                            <td style="width: 5%;" align="right">
                                                                <asp:Label runat="server" ID="label7" Text='<%#Eval("Comm_Rate") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 2%;" align="right">
                                                            </td>
                                                            <td style="width: 5%;" align="right">
                                                                <asp:Label runat="server" ID="label27" Text='<%#Eval("com_sr_rate") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 2%;" align="right">
                                                            </td>
                                                            <td style="width: 5%;" align="right">
                                                                <asp:Label runat="server" ID="label28" Text='<%#Eval("salevalue") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 2%;" align="right">
                                                            </td>
                                                            <td style="width: 5%;" align="right">
                                                                <asp:Label runat="server" ID="label8" Text='<%#Eval("Levi_Amt") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 2%;" align="right">
                                                            </td>
                                                            <td style="width: 5%;" align="right">
                                                                <asp:Label runat="server" ID="label9" Text='<%#Eval("Packing_Rate") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 2%;" align="right">
                                                            </td>
                                                            <td style="width: 5%;" align="right">
                                                                <asp:Label runat="server" ID="label5" Text='<%#Eval("Packing_Amnt") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 2%;" align="right">
                                                            </td>
                                                            <td style="width: 5%;" align="right">
                                                                <asp:Label runat="server" ID="label14" Text='<%#Eval("Hamali_Rate") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 2%;" align="right">
                                                            </td>
                                                            <td style="width: 5%;" align="right">
                                                                <asp:Label runat="server" ID="label20" Text='<%#Eval("Hamali_Amnt") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 2%;" align="right">
                                                            </td>
                                                            <td style="width: 5%;" align="right">
                                                                <asp:Label runat="server" ID="label22" Text='<%#Eval("Insurance_Amt") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 2%;" align="right">
                                                            </td>
                                                            <td style="width: 0%; visibility: hidden;" align="right">
                                                                <asp:Label runat="server" ID="label24" Text='<%#Eval("Panjar_Amnt") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td style="width: 2%;" align="right">
                                                            </td>
                                                            <td style="width: 8%;" align="right">
                                                                <asp:Label runat="server" ID="label25" Text='<%#Eval("Gross_Amnt") %>' Font-Bold="true"></asp:Label>
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
                            border-top: 1px solid black; background-color: #D3D3D3;">
                            <tr>
                                <td style="width: 15%;" align="left">
                                    <asp:Label runat="server" ID="label2" Text="Total" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 5%;" align="right">
                                    <asp:Label runat="server" ID="lbltotalqty" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 2%;" align="right">
                                </td>
                                <td style="width: 5%;" align="right">
                                    <asp:Label runat="server" ID="lblnetwttotal" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 2%;" align="right">
                                </td>
                                <td style="width: 5%;" align="right">
                                    <asp:Label runat="server" ID="label31" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 2%;" align="right">
                                </td>
                                <td style="width: 5%;" align="right">
                                    <asp:Label runat="server" ID="label32" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 2%;" align="right">
                                </td>
                                <td style="width: 5%;" align="right">
                                    <asp:Label runat="server" ID="label33" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 2%;" align="right">
                                </td>
                                <td style="width: 5%;" align="right">
                                    <asp:Label runat="server" ID="lblsvaluetotak" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 2%;" align="right">
                                </td>
                                <td style="width: 5%;" align="right">
                                    <asp:Label runat="server" ID="lbllevitotal" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 2%;" align="right">
                                </td>
                                <td style="width: 5%;" align="right">
                                    <asp:Label runat="server" ID="label36" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 2%;" align="right">
                                </td>
                                <td style="width: 5%;" align="right">
                                    <asp:Label runat="server" ID="lblpamnttotal" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 2%;" align="right">
                                </td>
                                <td style="width: 5%;" align="right">
                                    <asp:Label runat="server" ID="label38" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 2%;" align="right">
                                </td>
                                <td style="width: 5%;" align="right">
                                    <asp:Label runat="server" ID="lblhamnttotal" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 2%;" align="right">
                                </td>
                                <td style="width: 5%;" align="right">
                                    <asp:Label runat="server" ID="lbliamnttotal" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 2%;" align="right">
                                </td>
                                <td style="width: 0%;" align="right">
                                    <asp:Label runat="server" ID="lblpanjmnttotal" Text="" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 2%;" align="right">
                                </td>
                                <td style="width: 8%;" align="right">
                                    <asp:Label runat="server" ID="lblsaleamnttotal" Text="" Font-Bold="true"></asp:Label>
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
