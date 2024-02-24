<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptBalanceStockSummary.aspx.cs"
    Inherits="Report_rptBalanceStockSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../JS/emailValidation.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body style="font-family:Calibri; font-size:12px;width:1100px;" >');
            printWindow.document.write('<style type="text/css">thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>');
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
    <div>
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel"
                Text="Export To Excel" Width="120px" OnClick="btnExportToExcel_Click" />
        &nbsp;&nbsp;<asp:Button runat="server" ID="btnSendEmail" Text="Email" OnClick="btnSendEmail_Click"
            OnClientClick="CheckEmail();" Width="79px" />
        &nbsp;<asp:TextBox runat="server" ID="txtEmail" Width="300px" Height="23px"></asp:TextBox>
    </div>
    <div>
        <asp:Panel ID="pnlMain" runat="server" align="center" Font-Names="Calibri">
            <asp:Label ID="lblCompanyName" Width="90%" runat="server" Text="" Style="text-align: center;"
                CssClass="lblName" Font-Size="16px" Font-Bold="true"></asp:Label>
            <br />
            <asp:Label ID="Label15" runat="server" Width="90%" Text="Balance Stock Summary" CssClass="lblName"
                Style="text-align: center;" Font-Size="14px" Font-Bold="true"></asp:Label>
            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;">
                <tr>
                    <td style="border-bottom: double 2px black;">
                    </td>
                </tr>
            </table>
            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                background-color: Black;">
                <tr>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="lblNo" runat="server" Text="TenderNo." Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td align="left" style="width: 3%;">
                        <asp:Label ID="lblDispatchDate" runat="server" Text="Date" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td align="left" style="width: 8%;">
                        <asp:Label ID="lblMill" runat="server" Text="MILL" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="lblGrade" runat="server" Text="Grade" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td align="center" style="width: 4%;">
                        <asp:Label ID="lblNetQntl" runat="server" Text="NetQntl / SaleRate" Font-Bold="true"
                            ForeColor="White"></asp:Label>
                    </td>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="Label1" runat="server" Text="" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="lblMillRate" runat="server" Text="Mill Rate" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="lblQnty" runat="server" Text="Qty" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="lblDispatched" runat="server" Text="Disptch" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="lblBalLeft" runat="server" Text="Bal" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="lblLiftingDate" runat="server" Text="Lifting" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td align="left" style="width: 6%;">
                        <asp:Label ID="lblDO" runat="server" Text="D.O" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="12" style="border-bottom: double 2px black;">
                    </td>
                </tr>
            </table>
            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="font-size: 15px;">
                <tr>
                    <td align="center" style="width: 100%">
                        <asp:DataList runat="server" ID="dtl" Width="100%" OnItemDataBound="DataList_ItemDataBound">
                            <ItemTemplate>
                                <table width="100%" align="center" cellpadding="2" cellspacing="0" style="table-layout: fixed;
                                    background-color: #FFFFCC;">
                                    <tr>
                                        <td align="left" style="width: 2%;">
                                            <asp:Label ID="lblTenderNo" runat="server" Text='<%# Eval("Tender_No") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 3%;">
                                            <asp:Label ID="lblTenderDispatchDate" runat="server" Text='<%# Eval("Tender_Date") %>'
                                                Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 8%;">
                                            <asp:Label ID="lblTenderMill" runat="server" Text='<%# Eval("Mill") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 4%;">
                                            <asp:Label ID="lblTenderGrade" runat="server" Text='<%# Eval("Grade") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 4%;">
                                            <asp:Label ID="lblTenderNetQntl" runat="server" Text='<%# Eval("Quantal") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 2%;">
                                            <asp:Label ID="Label1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 5%;">
                                            <asp:Label ID="lblTenderMillRate" runat="server" Text='<%# Eval("Mill_Rate") %>'
                                                Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 4%;">
                                            <asp:Label ID="lblTenderQnty" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 4%;">
                                            <asp:Label ID="lblTenderDispatched" runat="server" Text='<%#Eval("Dispatched") %>'
                                                Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 2%;">
                                            <asp:Label ID="lblTenderBalLeft" runat="server" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 4%;">
                                            <asp:Label ID="lblTenderLiftingDate" runat="server" Text='<%# Eval("Lifting_Date") %>'
                                                Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 6%;">
                                            <asp:Label ID="lblTenderDO" runat="server" Text='<%# Eval("Tender_DO") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12" style="border-bottom: double 2px black;">
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" cellpadding="2" cellspacing="0">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:DataList runat="server" ID="DatalistTenderDetails" Width="100%" OnItemDataBound="DatalistTenderDetails_OnItemDataBound">
                                                <ItemTemplate>
                                                    <table width="100%" align="center" cellpadding="2" cellspacing="0" style="background-color: #FFF;
                                                        table-layout: fixed;">
                                                        <tr>
                                                            <td align="center" style="width: 2%;">
                                                                <asp:Label ID="lblTenderNod" runat="server" Text='<%# Eval("TDetailId") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 15%;">
                                                                <asp:Label ID="lblTenderDispatchDated" runat="server" Text='<%# Eval("TDetailMill") %>'
                                                                    Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <%--<td align="left" style="width: 8%;">
                                                                <asp:Label ID="lblTenderMilld" runat="server" Text='<%# Eval("Mill") %>' Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 4%;">
                                                                <asp:Label ID="lblTenderGraded" runat="server" Text='<%# Eval("Grade") %>' Font-Bold="true"></asp:Label>
                                                            </td>--%>
                                                            <td align="center" style="width: 4%;">
                                                                <asp:Label ID="lblTenderNetQntld" runat="server" Text='<%# Eval("TDeatailSaleRate") %>'
                                                                    Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 2%;">
                                                                <asp:Label ID="Label1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 5%;">
                                                                <asp:Label ID="lblTenderMillRated" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 4%;">
                                                                <asp:Label ID="lblTenderQntyd" runat="server" Text='<%# Eval("TDeatailQntl") %>'
                                                                    Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 4%;">
                                                                <asp:Label ID="lblTenderDispatchedd" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 2%;">
                                                                <asp:Label ID="lblTenderBalLeftd" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 4%;">
                                                                <asp:Label ID="lblTenderLiftingDated" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 6%;">
                                                                <asp:Label ID="lblTenderDOd" runat="server" Visible="false" Text="" Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%" align="center">
                                                        <tr>
                                                            <td style="width: 100%">
                                                                <asp:DataList runat="server" ID="DtDispDetails" Width="100%">
                                                                    <ItemTemplate>
                                                                        <table width="100%" align="center" cellpadding="2" cellspacing="0" style="background-color: #CCFFFF;
                                                                            table-layout: fixed;">
                                                                            <tr>
                                                                                <td align="center" style="width: 2%;">
                                                                                    <asp:Label ID="lblTenderNodi" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 3%;">
                                                                                    <asp:Label ID="lblTenderDispatchDatedi" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 4%;">
                                                                                    <asp:Label ID="lblTenderMilldi" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 11%;">
                                                                                    <asp:Label ID="lblTenderGradedi" runat="server" Text='<%# Eval("DIGetPass") %>' Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <%-- <td align="center" style="width: 4%;">
                                                                                <asp:Label ID="lblTenderNetQntldi" runat="server" Text='<%# Eval("TDeatailSaleRate") %>'
                                                                                    Font-Bold="false"></asp:Label>
                                                                            </td>--%>
                                                                                <td style="width: 4%;" align="right">
                                                                                    <asp:Label runat="server" ID="Label19" Text="DO#:" Font-Bold="true"></asp:Label>
                                                                                    <asp:Label ID="lblDINo" runat="server" Text='<%# Eval("DINo") %>' Font-Bold="true"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 4%;" align="right">
                                                                                    <asp:Label runat="server" ID="Label2" Text="SR#:" Font-Bold="true"></asp:Label>
                                                                                    <asp:Label ID="lblTenderMillRatedi" runat="server" Text='<%# Eval("DISaleRate") %>'
                                                                                        Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 4%;">
                                                                                    <asp:Label ID="lblTenderQntydi" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 4%;">
                                                                                    <asp:Label ID="lblTenderDispatcheddi" runat="server" Text='<%# Eval("DIQntl") %>'
                                                                                        Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 5%;">
                                                                                    <asp:Label ID="lbldocdate" runat="server" Text='<%# Eval("DIDocDate") %>' Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 4%;">
                                                                                    <asp:Label ID="lblTno" runat="server" Text='<%# Eval("DITruckNo") %>' Font-Bold="false"></asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 2%;">
                                                                                    <asp:Label ID="lblTenderBalLeftdi" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 4%;">
                                                                                    <asp:Label ID="lblTenderLiftingDatedi" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                </td>
                                                                                <td align="left" style="width: 6%;">
                                                                                    <asp:Label ID="lblTenderDOdi" runat="server" Visible="false" Text="" Font-Bold="true"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="12" style="border-bottom: dashed 2px black;">
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
