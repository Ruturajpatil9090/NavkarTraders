<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptMultiplePartyLedger.aspx.cs"
    Inherits="Report_rptMultiplePartyLedger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
    <script src="../JS/emailValidation.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('', 'st', 'height=400,width=800');
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="btnPrint" Text="Print" OnClientClick="PrintPanel();" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
        &nbsp;
        <asp:Button runat="server" ID="btnPDFMail" Text="Export To PDF" Width="120px" OnClick="btnPDFMail_Click" />
        <%-- <asp:Button ID="btnMail" runat="server" Text="Mail" Width="80px" OnClientClick="return CheckEmail();"
            OnClick="btnMail_Click" />
        &nbsp;
        <asp:TextBox runat="server" ID="txtEmail" Width="300px"></asp:TextBox></div>--%>
    </div>
    <div>
        <div id="dvInfo">
            <asp:Panel ID="pnlMain" runat="server" align="center" Font-Names="Calibri" CssClass="medium"
                Width="100%">
                <asp:Panel ID="pnlsub" runat="server" align="center" Font-Names="Calibri" CssClass="medium"
                    Width="100%">
                    <asp:Label ID="lblCompanyName" Width="100%" runat="server" Text="" Style="text-align: center;"
                        CssClass="lblName" Font-Size="16px" Font-Bold="true"></asp:Label>
                    <br />
                    <asp:Label ID="Label15" runat="server" Width="100%" Text="Multiple Ledger Report"
                        CssClass="lblName" Style="text-align: center;" Font-Size="14px" Font-Bold="true"></asp:Label><br />
                    <asp:Label ID="lblType" runat="server" Width="100%" Text="" CssClass="lblName" Style="text-align: center;"
                        Font-Size="14px" Font-Bold="true"></asp:Label>
                        <br />
                    <asp:Label ID="lblDate" runat="server" Width="100%" Text="" CssClass="lblName" Style="text-align: center;"
                        Font-Size="14px" Font-Bold="true"></asp:Label>
                </asp:Panel>
                <table width="75%" align="center" cellspacing="0" class="largsize" style="border-bottom: 1px solid black;
                    border-top: 1px solid black; table-layout: fixed; text-align: center;">
                    <tr>
                        <td align="right" style="font-size: small;">
                            <asp:Label ID="Label2" runat="server" Text="Ac_Code" Font-Bold="true"></asp:Label>
                        </td>
                         <td>
                        </td>
                        <td align="left" style="font-size: small;">
                            <asp:Label ID="Label3" runat="server" Text="Party Name" Font-Bold="true"></asp:Label>
                        </td>
                       
                    </tr>
                </table>
                <table width="100%" align="center" class="largsize" style="float: left;">
                    <tr align="center">
                        <td style="width: 100%;" align="center">
                            <asp:DataList ID="dtl" runat="server" Width="100%" OnItemDataBound="DataList_ItemDataBound">
                                <ItemTemplate>
                                    <table width="75%" align="center" cellpadding="1" cellspacing="0" class="largsize"
                                        style="table-layout: fixed; border-bottom: 1px solid black; border-top: 1px solid black;
                                        text-align: center;">
                                        <tr style="background-color: #FFFFCC;">
                                            <td align="right" style="font-size: medium;">
                                                <asp:Label ID="lbldoc_no" runat="server" Text='<%#Eval("Doc_No") %>' Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="font-size: medium;" align="left">
                                                <asp:Label ID="lblAc_Name_E" runat="server" Text='<%#Eval("Ac_Name_E") %>' Font-Bold="true"></asp:Label>
                                            </td>
                                            
                                        </tr>
                                    </table>
                                    </tr>
                                      <tr align="center">
                                    <asp:Panel runat="server" ID="pnltable">
                                        <table width="100%" align="center" cellspacing="0" class="largsize">
                                            <tr align="center">
                                                <td>
                                                    <%-- <asp:DataList runat="server" ID="dtlDetails" Width="100%">--%>
                                                    <%--  <ItemTemplate>--%>
                                                    <%-- <table width="100%" align="center" style="background-color: #CCFFFF; border-bottom: 1px dashed black;"
                                                    class="largsize">--%>
                                                    <%--  <tr align="center">--%>
                                                    <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="White"
                                                        GridLines="Both" HeaderStyle-ForeColor="Black" HeaderStyle-Height="20px" EmptyDataText="No Records found"
                                                        Width="900px" CellPadding="5" CellSpacing="0" Font-Bold="false" ForeColor="Black"
                                                        RowStyle-Height="25px" ShowFooter="true" Font-Names="Verdana" Font-Size="14px"
                                                        OnRowDataBound="grdDetail_RowDataBound" CssClass="print" AllowPaging="false"
                                                        PageSize="50" OnPageIndexChanging="grdDetail_PageIndexChanging">
                                                        <Columns>
                                                            <asp:BoundField DataField="TranType" HeaderText="Type" ItemStyle-HorizontalAlign="Center"
                                                                ItemStyle-Width="40px" HeaderStyle-CssClass="thead" />
                                                            <asp:BoundField DataField="DocNo" HeaderText="DocNo" ItemStyle-HorizontalAlign="Center"
                                                                ItemStyle-Width="40px" HeaderStyle-CssClass="thead" />
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
                                                        <RowStyle Height="7px" Wrap="true" ForeColor="Black" />
                                                        <FooterStyle BackColor="Yellow" Font-Bold="true" />
                                                    </asp:GridView>
                                                    <table width="100%" align="center" class="largsize">
                                                        <tr>
                                                            <td align="center" style="width: 100%;">
                                                                <%-- <asp:Label runat="server" ID="lblBillAmount_Total" Font-Bold="true" Text="Total:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                                                <%--<asp:Label runat="server" ID="lblUtrUsedAmount" Font-Bold="true" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label runat="server" ID="lblUtrBal" Font-Bold="true" Text=""></asp:Label>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%-- </tr>--%>
                                                    <%--  </table>--%>
                                                    <%--  </ItemTemplate>--%>
                                                    <%-- </asp:DataList>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>
</html>
