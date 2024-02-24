<%@ Page Title="rpt:Vasuli Register New " Language="C#" AutoEventWireup="true" CodeFile="rptVasuliRegisterNew.aspx.cs"
    Inherits="Report_rptVasuliRegisterNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DO Vasuli</title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('sugarStock.html', 'st', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body style="font-family:Calibri;font-size:12px;width:1100px; text-align:center;" >');
            printWindow.document.write(panel.innerHTML);

            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 100);
            return false;
        }



        function calculate() {
            var label = document.getElementById('<%=lblUpdatedAmt.ClientID %>');
            var dtl = document.getElementById('<%=dtl.ClientID %>');
            document.getElementById('<%=lblUpdatedAmt.ClientID %>').innerText = "patil";

            for (var i = 0; i < dtl.options.length; i++) {
                document.getElementById('<%=lblUpdatedAmt.ClientID %>').innerText = "ankush";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" />&nbsp;&nbsp;
        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnHelp" Width="80px"
            OnClick="btnUpdate_Click" />&nbsp;<asp:Label runat="server" ID="lblUpdatedAmt" ForeColor="Blue"></asp:Label>
    </div>
    <div>
        <asp:Panel ID="pnlMain" runat="server" align="center" Font-Names="Calibri">
            <asp:Label ID="lblCompanyName" Width="90%" runat="server" Text="" Style="text-align: center;"
                CssClass="lblName" Font-Size="16px" Font-Bold="true"></asp:Label>
            <br />
            <asp:Label ID="Label15" runat="server" Width="90%" Text="Vasuli Register from DO"
                CssClass="lblName" Style="text-align: center;" Font-Size="14px" Font-Bold="true"></asp:Label>
            <br />
            <table width="90%" align="center" cellpadding="1" style="table-layout: fixed; border-top: 2px solid black;
                border-bottom: 2px solid black;">
                <tr>
                    <td align="right" style="width: 2%;">
                        <asp:Label ID="lblDOType" runat="server" Text="RefNo" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="lblDoDate" runat="server" Text="Date" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 6%;">
                        <asp:Label ID="lblMillName" runat="server" Text="Mill" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 3%;">
                        <asp:Label ID="lblDoQntl" runat="server" Text="Quintal" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 8%;">
                        <asp:Label ID="lblGetpass" runat="server" Text="Customer Name" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="lblDoTruckNo" runat="server" Text="Truck No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 3%;">
                        <asp:Label ID="lblAmount" runat="server" Text="Vasuli" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="right" style="width: 2%;" class="noprint2">
                        <asp:Label ID="Label1" runat="server" Text="Recieve" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="90%" align="center" cellpadding="1">
                <tr>
                    <td style="width: 100%" align="left">
                        <asp:DataList runat="server" Width="100%" ID="dtl" OnItemDataBound="DataList_OnItemDataBound">
                            <ItemTemplate>
                                <table align="left" width="100%" cellpadding="1">
                                    <tr>
                                        <td align="left" style="width: 50%">
                                            <asp:Label runat="server" ID="lblTrasportCode" Visible="false" Text='<%#Eval("Transport_Code") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lbltransport" Font-Bold="true" Text='<%#Eval("Transport_Name") %>'></asp:Label>
                                        </td>
                                        <td align="right" style="width: 43%">
                                            <asp:Label runat="server" ID="lblVasuliTotal" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 7%" class="noprint2">
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" cellspacing="1">
                                    <tr>
                                        <td style="width: 100%;">
                                            <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                <ItemTemplate>
                                                    <table align="left" width="100%" cellpadding="2" style="table-layout: fixed; background-color: #FFFFCC;
                                                        border-bottom: 1px dashed black;">
                                                        <tr>
                                                            <td align="right" style="width: 2%;">
                                                                <asp:Label ID="lbldtlrefno" runat="server" Text='<%#Eval("doc_no") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 3%;">
                                                                <asp:Label ID="lbldtldate" runat="server" Text='<%#Eval("doc_date") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 6%;">
                                                                <asp:Label ID="lbldtMillName" runat="server" Text='<%#Eval("millShortName") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 3%;">
                                                                <asp:Label ID="lbldtDoQntl" runat="server" Text='<%#Eval("quantal") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 8%;">
                                                                <asp:Label ID="lbldtGetpass" runat="server" Text='<%#Eval("PartyName") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="center" style="width: 3%;">
                                                                <asp:Label ID="lbldtDoTruckNo" runat="server" Text='<%#Eval("truck_no") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="right" style="width: 3%;">
                                                                <asp:Label ID="lbldtAmount" runat="server" Text='<%#Eval("vasuli_amount1") %>' Font-Bold="false"></asp:Label>
                                                            </td>
                                                            <td align="right" style="width: 2%;" class="noprint2">
                                                                <asp:CheckBox runat="server" ID="chkRecieve" OnCheckedChanged="chkRecieve_OnCheckedChanged"
                                                                    AutoPostBack="true" />
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
                    <td style="width: 90%" align="left">
                        <table align="center" width="100%" cellpadding="5" style="table-layout: fixed; border-top: 2px double black;">
                            <tr>
                                <td align="right" style="width: 2%;">
                                    <asp:Label ID="lbldstlrefno" runat="server" Text="" Font-Bold="false"></asp:Label>
                                </td>
                                <td align="center" style="width: 3%;">
                                    <asp:Label ID="lbldtsldate" runat="server" Text="" Font-Bold="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 6%;">
                                    <asp:Label ID="lbldtsMillName" runat="server" Text="" Font-Bold="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 3%;">
                                    <asp:Label ID="lbldtsDoQntl" runat="server" Text="" Font-Bold="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 8%;">
                                    <asp:Label ID="lbldtGestpass" runat="server" Text="" Font-Bold="false"></asp:Label>
                                </td>
                                <td align="center" style="width: 3%;">
                                    <asp:Label ID="lbldtDosTruckNo" runat="server" Text="" Font-Bold="false"></asp:Label>
                                </td>
                                <td align="right" style="width: 3%;">
                                    <asp:Label ID="lblTotalVasuli" runat="server" Text="vasuli" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="width: 2%;" class="noprint2">
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
