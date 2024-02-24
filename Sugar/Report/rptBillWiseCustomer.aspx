<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptBillWiseCustomer.aspx.cs" Inherits="Sugar_Report_rptBillWiseCustomer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Jaggary</title>
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
    <script src="../Scripts/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head><link href="../print.css" rel="stylesheet" type="text/css" media="print" />');
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" />
        <%--  &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />--%>
    </div>
    <div>
        <asp:Panel runat="server" ID="pnlMain" align="center" Font-Names="calibri" CssClass="largsize">
            <table width="80%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;"
                class="print">
                <tr>
                    <td style="width: 80%;" align="center">
                        <asp:Label runat="server" ID="lblCompanyName" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;" align="center">
                        <asp:Label runat="server" ID="Label11" Font-Bold="true" Text="Bill and Customer"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="60%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;background-color:Aqua;
                border-bottom: solid 2px black; border-top: solid 2px black;" class="print">
                <tr>
                    <%--<td align="left" style="width: 3%;">
                        <asp:Label ID="Label4" runat="server" Text="Type" Font-Bold="true"></asp:Label>
                    </td>--%>
                    <td align="left" style="width: 2%;">
                        
                    </td>
                    <td align="left" style="width: 3%;">
                        <asp:Label ID="lblMemono" runat="server" Text="Bill No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 10%;">
                        <asp:Label ID="lblpnm" runat="server" Text="Customer name" Font-Bold="true"></asp:Label>
                    </td>
                   
                    
                </tr>
            </table>
            <table width="80%" align="center" cellpadding="1" cellspacing="0" class="print">
                <tr>
                    <td style="width: 70%;">
                        <asp:DataList ID="dtl" runat="server" Width="100%" CellSpacing="4" OnItemDataBound="dtl_OnItemDataBound">
                            <ItemTemplate>
                                <table align="center" width="80%" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                                    border-bottom: dashed 2px black;" class="print">
                                    <tr>
                                        <td style="width: 80%; border-bottom: solid 1px black;background-color:White;font-weight:bold;font-size:x-large">
                                            <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("Bill_No") %>' Visible="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%;">
                                            <table align="center" width="100%" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                                                border-bottom: dashed 2px black;" class="print millnames">
                                                <tr>
                                                    <td style="width: 80%;" class="tddetails">
                                                        <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                            <ItemTemplate>
                                                                <table align="center" width="80%" cellpadding="1" cellspacing="2" style="table-layout: fixed;"
                                                                    class="print">
                                                                    <tr style="">
                                                                        <%--<td align="center" style="width: 2%;border-bottom:1px dashed black;">
                                                                            <asp:Label ID="lbldocno" runat="server" Text='<%#Eval("TRAN_TYPE") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="center" style="width: 2%;border-bottom:1px dashed black;">
                                                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("doc_no") %>' Font-Bold="false"></asp:Label>
                                                                        </td>--%>

                                                                        <td align="center" style="width: 2%;border-bottom:1px dashed black;color:Red;">
                                                                            <asp:Label ID="lbldate" runat="server" Text='<%#Eval("Bill_No") %>' Font-Bold="false"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 10%;border-bottom:1px dashed black;">
                                                                            <asp:Label ID="lbltrantype" runat="server" Text='<%#Eval(" Customer_name") %>' Font-Bold="false"></asp:Label>
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
                                        <td style="width: 100%;">
                                            <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                                                border-bottom: solid 1px black; border-top: solid 1px black;" class="print">
                                                
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

