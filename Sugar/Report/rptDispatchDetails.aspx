<%@ Page Title="rpt:Dispatch Details" Language="C#" AutoEventWireup="true" CodeFile="rptDispatchDetails.aspx.cs"
    Inherits="Report_rptDispatchDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dispatch Details</title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body style="font-family:Calibri;font-size:10pt;width:1100px; text-align:center;">');
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
    <script type="text/javascript" src="../JS/emailValidation.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="left" style="width: 80%;">
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
        &nbsp;&nbsp;<asp:Button runat="server" ID="btnSendEmail" Text="Email" OnClick="btnSendEmail_Click"
            OnClientClick="CheckEmail();" Width="79px" />
        &nbsp;<asp:TextBox runat="server" ID="txtEmail" Width="300px" Height="23px"></asp:TextBox>
    </div>
    <div>
        <asp:Panel ID="pnlMain" runat="server" align="center" Font-Names="Calibri">
            <asp:Label ID="lblCompanyName" Width="90%" runat="server" Text="" Style="text-align: center;"
                CssClass="lblName" Font-Size="16px" Font-Bold="true"></asp:Label>
            <br />
            <asp:Label ID="Label15" runat="server" Width="90%" Text="Dispatch Details Report"
                CssClass="lblName" Style="text-align: center;" Font-Size="14px" Font-Bold="true"></asp:Label>
            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;">
                <tr>
                    <td style="border-bottom: double 2px black;">
                    </td>
                </tr>
            </table>
            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;">
                <tr>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="lblNo" runat="server" Text="No." Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 3%;">
                        <asp:Label ID="lblDispatchDate" runat="server" Text="Date" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 6%;">
                        <asp:Label ID="lblMill" runat="server" Text="MILL" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="lblGrade" runat="server" Text="Grade" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="lblNetQntl" runat="server" Text="Net Qntl" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="lblMillRate" runat="server" Text="Mill Rt" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="lblQnty" runat="server" Text="Qty" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="lblDispatched" runat="server" Text="Disptch" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="lblBalLeft" runat="server" Text="Bal" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="lblLiftingDate" runat="server" Text="Lifting" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 6%;">
                        <asp:Label ID="lblDO" runat="server" Text="D.O" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="11" style="border-bottom: double 2px black;">
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
                                        <td align="left" style="width: 6%;">
                                            <asp:Label ID="lblTenderMill" runat="server" Text='<%# Eval("Mill") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 4%;">
                                            <asp:Label ID="lblTenderGrade" runat="server" Text='<%# Eval("Grade") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 4%;">
                                            <asp:Label ID="lblTenderNetQntl" runat="server" Text='<%# Eval("Quantal") %>' Font-Bold="true"></asp:Label>
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
                                            <asp:Label ID="lblTenderDO" runat="server" Visible="false" Text='<%# Eval("Tender_DO") %>'
                                                Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="11" style="border-bottom: double 2px black;">
                                        </td>
                                    </tr>
                                </table>
                                <tr>
                                    <td colspan="12">
                                        <%-- <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;">
                                            <tr>
                                                <td colspan="9">--%>
                                        <asp:DataList runat="server" ID="dtlTenderDetails" Width="100%" OnItemDataBound="dtlTenderDetails_ItemDatBound">
                                            <ItemTemplate>
                                                <table width="100%" align="left" cellpadding="1" cellspacing="0" style="table-layout: fixed;">
                                                    <tr>
                                                        <td align="left" style="width: 2%;">
                                                            <asp:Label ID="lblTenderDetailNo" runat="server" Text='<%# Eval("ID") %>' Font-Bold="false"
                                                                Visible="true"></asp:Label>
                                                        </td>

                                                        <td align="left" style="width: 3%;">
                                                            <asp:Label ID="lblTenderDispatchDate" Visible="false" runat="server" Text="" Font-Bold="false"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 18%;">
                                                            <asp:Label ID="lblTenderMill" runat="server" Text='<%# Eval("Buyer") %>' Font-Bold="false"></asp:Label>
                                                        </td>
                                                        <%--<td align="left" style="width: 4%;">
                                                            <asp:Label ID="lblTenderGrade" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 4%;">
                                                            <asp:Label ID="lblTenderNetQntl" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 5%;">
                                                            <asp:Label ID="lblTenderMillRate" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        --%>
                                                        <td align="left" style="width: 4%;">
                                                            <asp:Label ID="lblTenderQnty" runat="server" Text='<%#Eval("Qty") %>' Font-Bold="false"></asp:Label>
                                                            <%--</td>
                                                            <td align="right" style="width: 1%;">--%>
                                                            (<asp:Label ID="lblTenderSaleRate" runat="server" Text='<%# Eval("Sale_Rate") %>'
                                                                Font-Bold="false"></asp:Label>)
                                                        </td>
                                                        <td align="left" style="width: 4%;">
                                                            <asp:Label runat="server" ID="lblTenderDetailsDispatch" Text='<%#Eval("TD_Dispatch") %>'
                                                                Font-Bold="false"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 2%;">
                                                            <asp:Label runat="server" ID="lbltdbal" Text='<%#Eval("TD_Bal") %>' Font-Bold="false"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 4%;">
                                                            <asp:Label ID="lblTenderLiftingDate" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 6%;">
                                                            <asp:Label ID="lblTenderDO" runat="server" Visible="false" Text='<%# Eval("Tender_DO") %>'
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <tr>
                                                    <td colspan="11" style="border-bottom: dashed 2px black;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="12">
                                                        <%--<table width="100%" align="center" cellpadding="1" cellspacing="0">
                                                            <tr>
                                                                <td colspan="9">--%>
                                                        <asp:DataList runat="server" ID="dtlDispatch" Width="100%">
                                                            <ItemTemplate>
                                                                <table width="100%" align="center" cellpadding="1" cellspacing="0" style="background-color: #00FFFF">
                                                                    <tr>
                                                                        <td align="left" style="width: 1.2%;">
                                                                            <asp:Label ID="lblPurcOrder" runat="server" Text='<%# Eval("detail_id") %>' Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 1.2%;">
                                                                            <asp:Label ID="lblDIDate" runat="server" Text='<%#Eval("DI_Date") %>' Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 10%;">
                                                                            <asp:Label ID="lblGetPass" runat="server" Text='<%# Eval("Getpass") %>' Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                         <td align="left" style="width: 10%;">
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("shiptoname") %>' Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 8%;">
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("truck_no") %>' Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 8%;">
                                                                            <asp:Label ID="lblDIQnty" runat="server" Text='<%#Eval("DI_Qty") %>' Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 5%;">
                                                                            <asp:Label ID="lblTenderDO" runat="server" Text='<%# Eval("DI_DO") %>' Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="11" style="border-bottom: dashed 2px black;">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                        <%--       </td>
                                                            </tr>
                                                        </table>--%>
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <tr>
                                            <td colspan="11" style="border-bottom: double 2px black;">
                                            </td>
                                        </tr>
                                        <%--</td>
                                            </tr>
                                        </table>--%>
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
