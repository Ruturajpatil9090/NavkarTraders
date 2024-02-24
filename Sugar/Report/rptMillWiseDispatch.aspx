<%@ Page Title="rpt:Millwise Dispatch" Language="C#" AutoEventWireup="true" CodeFile="rptMillWiseDispatch.aspx.cs"
    Inherits="Report_rptMillWiseDispatch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mill Wise Dispatch</title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body style="font-family:Calibri;font-size:12px;width:1100px; text-align:center;" >');
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
    </div>
    <div>
        <asp:Panel runat="server" ID="pnlMain">
            <asp:Label ID="lblCompanyName" Width="90%" runat="server" Text="" Style="text-align: center;"
                CssClass="lblName" Font-Size="16px" Font-Bold="true"></asp:Label>
            <br />
            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;">
                <tr>
                    <td colspan="11">
                        <asp:Label runat="server" ID="lblfromtodate" Font-Bold="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="11" style="border-bottom: double 2px black;">
                    </td>
                </tr>
            </table>
            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                font-size: 10pt;">
                <tr>
                    <td align="center" style="width: 2%;">
                        <asp:Label ID="lblNo" runat="server" Text="#" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="lblDispatchDate" runat="server" Text="Date" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="lblMill" runat="server" Text="MILL" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="lblGrade" runat="server" Text="Vouc By" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 9%;">
                        <asp:Label ID="lblNetQntl" runat="server" Text="Get Pass" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="lblMillRate" runat="server" Text="Mill Rt" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="lblQnty" runat="server" Text="Qntl" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 4%;">
                        <asp:Label ID="lblDispatched" runat="server" Text="Sell Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="lblBalLeft" runat="server" Text="Lorry No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 2%;">
                        <asp:Label ID="lblLiftingDate" runat="server" Text="Frt" Font-Bold="true"></asp:Label>
                    </td>
                     <td align="center" style="width: 2%;">
                        <asp:Label ID="Label7" runat="server" Text="Memo" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="lblDO" runat="server" Text="Vasuli" Font-Bold="true"></asp:Label>
                    </td>
                    <%--  <td align="center" style="width: 3%;">
                        <asp:Label ID="Label9" runat="server" Text="Vasuli Account" Font-Bold="true"></asp:Label>
                    </td>--%>
                    <td align="left" style="width: 6%;">
                        <asp:Label ID="Label1" runat="server" Text="Transport" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label2" runat="server" Text="DO" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="Label3" runat="server" Text="Tender" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 1%;">
                        <asp:Label ID="Label4" runat="server" Text="SMS" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%" align="center" cellpadding="1" cellspacing="0">
                <tr>
                    <td colspan="11" style="border-bottom: double 2px black;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;">
                        <asp:DataList runat="server" ID="Datalist1" Width="100%" OnItemDataBound="Datalist1_OnItemDataBound">
                            <ItemTemplate>
                                <table width="100%" align="center">
                                    <tr>
                                        <td style="width: 70%;" align="left">
                                            <asp:Label runat="server" ID="lblMillName" Font-Bold="true" Text='<%#Eval("MillName") %>'></asp:Label>
                                        </td>
                                        <td style="width: 30%;" align="left">
                                            <asp:Label runat="server" ID="lblMillCode" Text='<%#Eval("MillCode") %>' Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" cellpadding="1" cellspacing="0">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                <ItemTemplate>
                                                    <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                                                        font-size: 10pt;">
                                                        <tr>
                                                            <td align="center" style="width: 2%;">
                                                                <asp:Label ID="lblNo" runat="server" Text='<%#Eval("#") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 2%;">
                                                                <asp:Label ID="lblDispatchDate" runat="server" Text='<%#Eval("dodate") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 4%;">
                                                                <asp:Label ID="lblMill" runat="server" Text='<%#Eval("millShortName") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 5%;">
                                                                <asp:Label ID="lblGrade" runat="server" Text='<%#Eval("VocBy") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 9%;">
                                                                <asp:Label ID="lblNetQntl" runat="server" Text='<%#Eval("GetPass") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 3%;">
                                                                <asp:Label ID="lblMillRate" runat="server" Text='<%#Eval("MR") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 3%;">
                                                                <asp:Label ID="lblQnty" runat="server" Text='<%#Eval("Qntl") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 4%;">
                                                                <asp:Label ID="lblDispatched" runat="server" Text='<%#Eval("SR") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 4%;">
                                                                <asp:Label ID="lblBalLeft" runat="server" Text='<%#Eval("lorry") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 2%;">
                                                                <asp:Label ID="lblLiftingDate" runat="server" Text='<%#Eval("frt") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                             <td align="center" style="width: 2%;">
                                                                <asp:Label ID="Label8" runat="server" Text='<%#Eval("MM_Rate") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 3%;">
                                                                <asp:Label ID="lblDO" runat="server" Text='<%#Eval("vasuli") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <%-- <td align="center" style="width: 3%;">
                                                                <asp:Label ID="Label7" runat="server" Text='<%#Eval("Vasuli_Ac_Name") %>' Font-Bold="false"></asp:Label>
                                                            </td>--%>
                                                            <td align="left" style="width: 6%; overflow: hidden;">
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("transport") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 5%;">
                                                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("do") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 2%;">
                                                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("tender") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 1%;">
                                                                <asp:Label ID="Label5" runat="server" Text='<%#Eval("LoadingSms") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="11" style="border-bottom: double 2px black;">
                                        </td>
                                    </tr>
                                    <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                                        font-size: 13pt;">
                                        <tr>
                                            <td align="center" style="width: 2%;">
                                                <asp:Label ID="lblNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 2%;">
                                                <asp:Label ID="lblDispatchDate" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 4%;">
                                                <asp:Label ID="lblMill" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 5%;">
                                                <asp:Label ID="lblGrade" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 9%;">
                                                <asp:Label ID="lblNetQntl" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 3%;">
                                                <asp:Label ID="lblMillRate" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 3%;">
                                                <asp:Label ID="lblQntlTotal" runat="server" Text="Qntl" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 4%;">
                                                <asp:Label ID="lblDispatched" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 4%;">
                                                <asp:Label ID="lblBalLeft" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 2%;">
                                                <asp:Label ID="lblLiftingDate" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 3%;">
                                                <asp:Label ID="lblDO" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                           <%-- <td align="center" style="width: 3%;">
                                                <asp:Label ID="Label8" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>--%>
                                            <td align="left" style="width: 6%;">
                                                <asp:Label ID="Label1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 5%;">
                                                <asp:Label ID="Label2" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 2%;">
                                                <asp:Label ID="Label3" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 1%;">
                                                <asp:Label ID="Label6" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <tr>
                                        <td colspan="11" style="border-bottom: double 2px black;">
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
