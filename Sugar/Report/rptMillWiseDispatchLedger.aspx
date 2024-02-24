<%@ Page Title="rpt:Millwise Dispatch Ledger" Language="C#" AutoEventWireup="true" CodeFile="rptMillWiseDispatchLedger.aspx.cs"
    Inherits="Report_rptMillWiseDispatchLedger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dispatch Millwise</title>
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('a.html', 'st', 'height=400,width=800');
            printWindow.document.write('<html><head><link href="../print.css" rel="stylesheet" type="text/css" media="print" />');
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
    <script type="text/javascript" src="../JS/emailValidation.js"></script>
    <script type="text/javascript" language="javascript">
       
        function openDO() {
            window.open('../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx'); // //R=Redirected  O=Original
        }

    </script>
</head>
<body>
    <form id="frm" runat="server">
    <div>
        <asp:Button runat="server" ID="btnPrint" OnClientClick="PrintPanel();" Text="Print" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
        &nbsp;&nbsp;<asp:Button runat="server" ID="btnSendEmail" Text="Email" OnClick="btnSendEmail_Click"
            OnClientClick="CheckEmail();" Width="79px" />
        &nbsp;<asp:TextBox runat="server" ID="txtEmail" Width="300px" Height="23px"></asp:TextBox>
    </div>
    <div>
        <asp:Panel runat="server" ID="pnlMain" CssClass="largsize">
            <table width="90%" style="table-layout: fixed;" align="center">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblReportName" runat="server" Text="Dispatch MillWise" Width="100%"
                            CssClass="lblName" Font-Bold="true" Font-Size="Medium" Style="text-align: center;
                            width: 100%;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label runat="server" ID="lblPartyName"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="Table1" runat="server" width="90%" align="center" cellspacing="2" style="table-layout: fixed;
                border-bottom: 1px double black; border-top: 1px solid black;" class="largsize">
                <tr>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text="DO No"></asp:Label>
                    </td>
                    <td style="width: 2%" align="left">
                        <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="date"></asp:Label>
                    </td>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="Label3" Font-Bold="true" Text="qntl"></asp:Label>
                    </td>
                    <td style="width: 3%" align="center">
                        <asp:Label runat="server" ID="Label4" Font-Bold="true" Text="millrate"></asp:Label>
                    </td>
                    <td style="width: 3%" align="center">
                        <asp:Label runat="server" ID="Label5" Font-Bold="true" Text="amount"></asp:Label>
                    </td>
                    <td style="width: 8%" align="left">
                        <asp:Label runat="server" ID="Label6" Font-Bold="true" Text="Narration"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="Table12" runat="server" align="center" width="90%" class="largsize">
                <tr>
                    <td align="center" style="width: 100%">
                        <asp:DataList runat="server" ID="datalist" Width="100%" OnItemDataBound="DataList_ItemDataBound">
                            <ItemTemplate>
                                <table id="Table3" runat="server" width="100%" align="center" class="largsize">
                                    <tr>
                                        <td style="width: 30%; border-bottom: 1px solid black;" align="right">
                                            <asp:Label runat="server" ID="lblMillCode" Visible="false" Text='<%#Eval("millcode") %>'></asp:Label>
                                        </td>
                                        <td style="width: 70%; border-bottom: 1px solid black;" align="left">
                                            <asp:Label runat="server" ID="lblMillName" Visible="true" Font-Bold="true" Text='<%#Eval("mill") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                <ItemTemplate>
                                                    <table id="Table1" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed;
                                                        background-color: #FFFFCC;" class="largsize">
                                                        <tr>
                                                            <td style="width: 2%" align="center">
                                                                <%--<asp:Label runat="server" ID="lblDate" Font-Bold="true" Text='<%#Eval("do_no") %>'></asp:Label>--%>
                                                                 <asp:LinkButton runat="server" ID="lnkDO" Text='<%#Eval("do_no") %>' Font-Bold="false"
                                                                    Style="text-decoration: none;" OnClick="lnkDO_Click"></asp:LinkButton>
                                                            </td>
                                                            <td style="width: 2%" align="left">
                                                                <asp:Label runat="server" ID="Label1" Font-Bold="false" Text='<%#Eval("do_date") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="center">
                                                                <asp:Label runat="server" ID="Label3" Font-Bold="false" Text='<%#Eval("quantal") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="center">
                                                                <asp:Label runat="server" ID="Label4" Font-Bold="false" Text='<%#Eval("millrate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="center">
                                                                <asp:Label runat="server" ID="Label5" Font-Bold="false" Text='<%#Eval("amount") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 8%" align="left">
                                                                <asp:Label runat="server" ID="Label6" Font-Bold="false" Text='<%#Eval("narration") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" style="border-bottom: 1px double black;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <table id="Table1" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed;
                                                background-color: #CCFFFF;" class="largsize">
                                                <tr>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="left">
                                                        <asp:Label runat="server" ID="Label1" Font-Bold="false" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="lblqntltotal" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="center">
                                                        <asp:Label runat="server" ID="Label4" Font-Bold="false" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="center">
                                                        <asp:Label runat="server" ID="lblamounttotal" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 8%" align="left">
                                                        <asp:Label runat="server" ID="Label6" Font-Bold="false" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="border-bottom: 1px double black;">
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
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
