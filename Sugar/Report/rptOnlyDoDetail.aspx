<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptOnlyDoDetail.aspx.cs"
    Inherits="Report_rptOnlyDoDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

        function DO() {
            window.open('../Sugar/pgeDeliveryOrderForGST.aspx');    //R=Redirected  O=Original
        }
        function LV() {
            window.open('../Sugar/pgeLocalvoucherForGST.aspx');    //R=Redirected  O=Original
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
        <%-- &nbsp;&nbsp;<asp:Button runat="server" ID="btnSendEmail" Text="Email" OnClick="btnSendEmail_Click"
            OnClientClick="CheckEmail();" Width="79px" />
        &nbsp;<asp:TextBox runat="server" ID="txtEmail" Width="300px" Height="23px"></asp:TextBox>--%>
    </div>
    <div>
        <asp:Panel runat="server" ID="pnlMain" align="center" Font-Names="calibri" CssClass="print3">
            <asp:Label ID="lblReportName" runat="server" Text="Dispatch Summary" Width="100%"
                CssClass="lblName" Font-Bold="true" Font-Size="Large" Style="text-align: center;
                width: 100%;"></asp:Label>
            <table id="Table1" runat="server" width="95%" align="center" cellspacing="2" style="table-layout: fixed;
                border-top: 1px solid black; border-bottom: 1px solid black;" class="print3">
                <tr>
                    <td style="width: 2%" align="left">
                        <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text="DO No"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Voucher No"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Desp Type"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Mill"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label4" Font-Bold="true" Text="GetPass"></asp:Label>
                    </td>
                    <td style="width: 3%" align="center">
                        <asp:Label runat="server" ID="Label5" Font-Bold="true" Text="VoucherBy"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label6" Font-Bold="true" Text="Grade"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label8" Font-Bold="true" Text="Qntl"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label17" Font-Bold="true" Text="MR"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label18" Font-Bold="true" Text="SR"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label19" Font-Bold="true" Text="TC"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label20" Font-Bold="true" Text="Truck No"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label21" Font-Bold="true" Text="Transpoter"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label22" Font-Bold="true" Text="Freight Per Qtl"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label23" Font-Bold="true" Text="MM Rate"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label24" Font-Bold="true" Text="Vasuli Rate"></asp:Label>
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
                                            <asp:Label runat="server" ID="lbldodate" Text='<%#Eval("doc_date") %>' Visible="true"
                                                Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="width: 7%" align="center">
                                            <%-- <asp:Label runat="server" ID="lblTotalQntl" Text="" Font-Bold="true"></asp:Label>--%>
                                        </td>
                                        <td style="width: 65%" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center">
                                            <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                <ItemTemplate>
                                                    <table id="Table1" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed;
                                                        background-color: #FFFFCC; border-bottom: 1px solid black;" class="print3">
                                                        <tr>
                                                            <td style="width: 2%" align="center">
                                                                <asp:LinkButton Style="text-decoration: none;" runat="server" ID="lnkDO" Text='<%#Eval("doc_no") %>'
                                                                    OnClick="lnkDO_Click"></asp:LinkButton>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label12" Font-Bold="false" Text='<%#Eval("voucher_no") %>'></asp:Label>
                                                                <%-- <asp:LinkButton Style="text-decoration: none;" runat="server" ID="lnkVO" Text='<%#Eval("voucher_no") %>'
                                                                    OnClick="lnkVO_Click"></asp:LinkButton>--%>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label16" Font-Bold="false" Text='<%#Eval("desp_type") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label1" Font-Bold="false" Text='<%#Eval("millName") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label2" Font-Bold="false" Text='<%#Eval("GetPassName") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="center">
                                                                <asp:Label runat="server" ID="Label3" Font-Bold="false" Text='<%#Eval("VoucherByname") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label4" Font-Bold="false" Text='<%#Eval("grade") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label5" Font-Bold="false" Text='<%#Eval("quantal") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label6" Font-Bold="false" Text='<%#Eval("mill_rate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label7" Font-Bold="false" Text='<%#Eval("sale_rate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label9" Font-Bold="false" Text='<%#Eval("Tender_Commission") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label10" Font-Bold="false" Text='<%#Eval("truck_no") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label11" Font-Bold="false" Text='<%#Eval("TransportName") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label13" Font-Bold="false" Text='<%#Eval("FreightPerQtl") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label14" Font-Bold="false" Text='<%#Eval("MM_Rate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label15" Font-Bold="false" Text='<%#Eval("vasuli_rate1") %>'></asp:Label>
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
