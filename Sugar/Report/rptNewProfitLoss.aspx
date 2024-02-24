<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptNewProfitLoss.aspx.cs"
    Inherits="Report_rptNewProfitLoss" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('sugarStock.html', 'st', 'height=400,width=800');
            printWindow.document.write('<html><head><link href="../print.css" rel="stylesheet" type="text/css"  media="print"/>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 100);
            return false;
        }

        function TN(accode, Action) {
            window.open('../BussinessRelated/pgeTenderPurchasexml.aspx?tenderid=' + accode + '&Action=' + Action);
        }
    </script>
    <title>Sugar Balance Stock</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="noprint7pt">
        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return PrintPanel();"
            Width="80px" OnClick="btnPrint_Click" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
        &nbsp; &nbsp;
        <asp:Button ID="btnMail" runat="server" Text="Mail" Width="80px" OnClick="btnMail_Click" />
        &nbsp;Email:
        <asp:TextBox runat="server" ID="txtEmail" Width="300px"></asp:TextBox></div>
    <div>
        <asp:Panel ID="pnlMain" runat="server" align="center" Font-Names="Calibri" CssClass="largsize">
            <table align="center" width="90%" class="print" style="border-bottom: double 1px gray;
                table-layout: fixed;">
                <tr>
                    <td style="width: 100%;" align="center">
                        <asp:Label ID="lblCompanyName" Width="70%" runat="server" Text="" Style="text-align: center;"
                            CssClass="lblName" Font-Size="16px" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" align="center">
                        <asp:Label ID="Label15" runat="server" Width="70%" Text="Sugar Balance Stock" CssClass="lblName"
                            Style="text-align: center;" Font-Size="14px" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="90%" align="center" style="table-layout: fixed; background-color: Black;
                border-bottom: 1px solid black; border-top: 1px solid black; table-layout: fixed;"
                class="largsize">
                <tr>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="Label1" runat="server" Text="T.No." Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td align="center" style="width: 22%;">
                        <asp:Label ID="Label2" runat="server" ForeColor="White" Text="Date" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 22%;">
                        <asp:Label ID="Label3" runat="server" ForeColor="White" Text="Mill" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 25%;">
                        <asp:Label ID="Label4" runat="server" ForeColor="White" Text="Quantal" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 22%;">
                        <asp:Label ID="Label5" runat="server" ForeColor="White" Text="Mill_Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 20%;">
                        <asp:Label ID="Label6" runat="server" ForeColor="White" Text="Grade" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 20%;">
                        <asp:Label ID="Label8" runat="server" ForeColor="White" Text="Mill Name" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 7%;">
                        <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Mill Amt" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="90%" align="center" class="largsize">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataList ID="DataList1" runat="server" Width="100%" OnItemDataBound="DataList1_ItemDataBound"
                            OnSelectedIndexChanged="DataList1_SelectedIndexChanged">
                            <ItemTemplate>
                                <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                                    background-color: #CCFFFF;" class="largsize">
                                    <tr>
                                        <td align="left" style="width: 2%;">
                                            <asp:LinkButton runat="server" ID="lbkTenderNo" Text='<%#Eval("Tender_No") %>' OnClick="lnkTenderNo_Click"></asp:LinkButton>
                                            <%--<asp:Label ID="Label1" runat="server" Text='<%#Eval("Tender_No") %>' Font-Bold="true"></asp:Label>--%>
                                        </td>
                                        <td align="center" style="width: 22%;">
                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Tender_Date") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 22%; word-wrap: break-word; text-wrap: normal;">
                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Mill_Code") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 25%;">
                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("Quantal") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 22%;">
                                            <asp:Label ID="lblMillLot" runat="server" Text='<%#Eval("Mill_Rate") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 20%;">
                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("Grade") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 20%;">
                                            <asp:Label ID="Label8" runat="server" Text='<%#Eval("Short_Name") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <%-- <td align="right" style="width: 5%;">
                                        </td>
                                        <td align="right" style="width: 7%;">
                                        </td>
                                        <td align="right" style="width: 7%;">
                                            <asp:Label ID="lblDispatch1" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                        </td>
                                        --%>
                                        <td align="right" style="width: 7%;">
                                            <asp:Label ID="lblBalance1" runat="server" Text="" Font-Bold="true" Visible="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="13" align="left">
                                            <table width="100%" class="largsize">
                                                <tr>
                                                    <td style="width: 100%;">
                                                        <asp:DataList ID="dtl" runat="server" Width="100%">
                                                            <ItemTemplate>
                                                                <table width="100%" style="table-layout: fixed; background-color: #FFFFCC; border-bottom: 1px dashed black;"
                                                                    align="center" class="largsize">
                                                                    <tr>
                                                                        <td align="center" style="width: 5%;">
                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("dono") %>' Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 7%;">
                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("dodate") %>' Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 5%;">
                                                                            <asp:Label ID="lblQntaltotal" runat="server" Font-Bold="false" Text='<%#Eval("doqntl") %>'></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 5%;">
                                                                            <asp:Label ID="Label14" runat="server" Font-Bold="false" Text='<%#Eval("billno") %>'></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 10%;">
                                                                            <asp:Label ID="lbldespatchqty" runat="server" Font-Bold="false" Text='<%#Eval("billdate") %>'></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 8%;">
                                                                            <asp:Label ID="lblbalance" runat="server" Font-Bold="false" Text='<%#Eval("billto") %>'></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 30%;">
                                                                            <asp:Label ID="Label9" runat="server" Font-Bold="false" Text='<%#Eval("billtoname") %>'></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 9%;">
                                                                            <asp:Label ID="Label10" runat="server" Font-Bold="false" Text='<%#Eval("saleqty") %>'></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 9%;">
                                                                            <asp:Label ID="Label11" runat="server" Font-Bold="false" Text='<%#Eval("salerate") %>'></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 9%;">
                                                                            <asp:Label ID="Label12" runat="server" Font-Bold="false" Text='<%#Eval("profit") %>'></asp:Label>
                                                                        </td>
                                                                        <td align="right" style="width: 7%;">
                                                                        </td>
                                                                        <td align="right" style="width: 8%;">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="13" align="left">
                                            <table width="100%" class="largsize">
                                                <tr>
                                                    <td style="width: 100%;">
                                                        <table width="100%" style="table-layout: fixed; background-color: #FFFFCC; border-bottom: 1px dashed black;"
                                                            align="center" class="largsize">
                                                            <tr>
                                                                <td align="center" style="width: 5%;">
                                                                    <asp:Label ID="Label7" runat="server" Font-Bold="false" Text=""></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 8%;">
                                                                    <asp:Label ID="Label16" runat="server" Font-Bold="false" Text=""></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 5%;">
                                                                 <asp:Label ID="doqntal" runat="server" Text="" Font-Bold="true" Visible="true"></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 7%;">
                                                                </td>
                                                                <td align="left" style="width: 10%;">
                                                                </td>
                                                                <td align="left" style="width: 8%;">
                                                                </td>
                                                                <td align="left" style="width: 30%;">
                                                                </td>
                                                                <td align="left" style="width: 2%;">
                                                                   <asp:Label ID="lblqtl" runat="server" Text="" Font-Bold="true" Visible="true"></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 9%;">
                                                                    <asp:Label ID="lblDispatch1" runat="server" Text="" Font-Bold="true" Visible="true"></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 9%;">
                                                                    <asp:Label ID="lblProfitTot" runat="server" Text="" Font-Bold="true" Visible="true"></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 7%;">
                                                                </td>
                                                                <td align="right" style="width: 8%;">
                                                                </td>
                                                            </tr>
                                                        </table>
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
                        <table width="100%" style="table-layout: fixed; border-top: 1px solid black; border-bottom: 1px solid black;"
                            align="center" class="largsize">
                            <tr>
                                <td align="left" style="width: 5%;">
                                    <asp:Label ID="Label7" runat="server" Font-Bold="false" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 8%;">
                                    <asp:Label ID="lblBuyer" runat="server" Font-Bold="false" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 5%;">
                                </td>
                                <td align="right" style="width: 7%;">
                                </td>
                                <td align="right" style="width: 10%;">
                                </td>
                                <td align="right" style="width: 8%;">
                                </td>
                                <td align="right" style="width: 30%;">
                                </td>
                                <td align="right" style="width: 9%;">
                                    <asp:Label ID="lblQntaltotal" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td align="right" style="width: 9%;">
                                    <asp:Label ID="lbldespatchqty" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td align="right" style="width: 9%;">
                                    <asp:Label ID="lblbalance" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td align="right" style="width: 7%;">
                                </td>
                                <td align="right" style="width: 8%;">
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
