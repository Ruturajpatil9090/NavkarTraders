<%@ Page Title="rpt:Disp Millwise" Language="C#" AutoEventWireup="true" CodeFile="rptDispMillWise.aspx.cs"
    Inherits="Report_rptDispMillWise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            <asp:Label ID="lblReportName" runat="server" Text="Dispatch MillWise" Width="100%"
                CssClass="lblName" Font-Bold="true" Font-Size="Medium" Style="text-align: center;
                width: 100%;"></asp:Label>
            <table id="Table1" runat="server" width="90%" align="center" cellspacing="2" style="table-layout: fixed;
                border-bottom: 1px double black; border-top: 1px solid black;" class="largsize">
                <tr>
                    <td style="width: 2%" align="center">
                        <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text=" DoNo"></asp:Label>
                    </td>
                    <td style="width: 2%" align="left">
                        <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Date"></asp:Label>
                    </td>
                    <td style="width: 3%" align="right">
                        <asp:Label runat="server" ID="lblgrade" Font-Bold="true" Text="grade"></asp:Label>
                    </td>
                    <td style="width: 2%" align="right">
                        <asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Qntl"></asp:Label>
                    </td>
                    <td style="width: 3%" align="right">
                        <asp:Label runat="server" ID="Label4" Font-Bold="true" Text="Millrate"></asp:Label>
                    </td>
                    <td style="width: 3%" align="right">
                        <asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Amount"></asp:Label>
                    </td>
                    <td style="width: 3%" align="right">
                        <asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Salerate"></asp:Label>
                    </td>
                    <td style="width: 3%" align="right">
                        <asp:Label runat="server" ID="Label7" Font-Bold="true" Text="TDS Amt"></asp:Label>
                    </td>
                    <td style="width: 3%" align="right">
                        <asp:Label runat="server" ID="Label10" Font-Bold="true" Text="CGST Amt"></asp:Label>
                    </td>
                    <td style="width: 3%" align="right">
                        <asp:Label runat="server" ID="Label11" Font-Bold="true" Text="SGST Amt"></asp:Label>
                    </td>
                    <td style="width: 3%" align="right">
                        <asp:Label runat="server" ID="Label12" Font-Bold="true" Text="IGST Amt"></asp:Label>
                    </td>
                    <td style="width: 3%" align="right">
                        <asp:Label runat="server" ID="Label13" Font-Bold="true" Text="Bill Amt"></asp:Label>
                    </td>
                    <td style="width: 3%" align="right">
                        <asp:Label runat="server" ID="Label14" Font-Bold="true" Text="TCS Amt"></asp:Label>
                    </td>
                    <td style="width: 3%" align="right">
                        <asp:Label runat="server" ID="Label15" Font-Bold="true" Text="NetPayable Amt"></asp:Label>
                    </td>
                    <td style="width: 8%" align="center">
                        <asp:Label runat="server" ID="Label6" Font-Bold="true" Text="Narration"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="Table12" runat="server" align="center" width="90%" class="largsize">
                <tr>
                    <td align="center" style="width: 100%">
                        <asp:DataList runat="server" ID="datalist" Width="100%" OnItemDataBound="DataList_ItemDataBound">
                            <ItemTemplate>
                                <table id="Table3" runat="server" width="100%" align="center" style="background-color: #CCFFFF;"
                                    class="largsize">
                                    <tr>
                                        <td style="width: 30%;" align="right">
                                            <asp:Label runat="server" ID="lblMillCode" Visible="false" Text='<%#Eval("millcode") %>'></asp:Label>
                                        </td>
                                        <td style="width: 70%;" align="left">
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
                                                                <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text='<%#Eval("do_no") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="left">
                                                                <asp:Label runat="server" ID="Label1" Font-Bold="false" Text='<%#Eval("do_date") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="right">
                                                                <asp:Label runat="server" ID="lblgrade" Font-Bold="false" Text='<%#Eval("grade") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 2%" align="right">
                                                                <asp:Label runat="server" ID="Label3" Font-Bold="false" Text='<%#Eval("quantal") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="right">
                                                                <asp:Label runat="server" ID="Label4" Font-Bold="false" Text='<%#Eval("millrate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="right">
                                                                <asp:Label runat="server" ID="Label5" Font-Bold="false" Text='<%#Eval("amount") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="right">
                                                                <asp:Label runat="server" ID="Label8" Font-Bold="false" Text='<%#Eval("salerate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="right">
                                                                <%-- <asp:Label runat="server" ID="Label9" Font-Bold="false" Text='<%#Eval("totalamount") %>'></asp:Label>--%>
                                                                <asp:Label runat="server" ID="Label9" Font-Bold="false" Text='<%#Eval("TDS_Amt") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="right">
                                                                <asp:Label runat="server" ID="Label16" Font-Bold="false" Text='<%#Eval("CGSTAmount") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="right">
                                                                <asp:Label runat="server" ID="Label17" Font-Bold="false" Text='<%#Eval("SGSTAmount") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="right">
                                                                <asp:Label runat="server" ID="Label19" Font-Bold="false" Text='<%#Eval("IGSTAmount") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="right">
                                                                <asp:Label runat="server" ID="Label20" Font-Bold="false" Text='<%#Eval("Bill_Amount") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="right">
                                                                <asp:Label runat="server" ID="Label21" Font-Bold="false" Text='<%#Eval("TCS_Amt") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 3%" align="right">
                                                                <asp:Label runat="server" ID="Label22" Font-Bold="false" Text='<%#Eval("TCS_Net_Payable") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 8%" align="left">
                                                                <asp:Label runat="server" ID="Label6" Font-Bold="false" Text='<%#Eval("narration") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="15" style="border-bottom: 1px double black;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <table id="Table1" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed;"
                                                class="largsize">
                                                <tr>
                                                    <td style="width: 2%" align="center">
                                                        <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 2%" align="left">
                                                        <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Total"></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="right">
                                                        <asp:Label runat="server" ID="lblgrade" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                   
                                                    <td style="width: 2%" align="right">
                                                        <asp:Label runat="server" ID="lblqntltotal" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="right">
                                                        <asp:Label runat="server" ID="Label4" Font-Bold="false" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="right">
                                                        <asp:Label runat="server" ID="lblamounttotal" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="right">
                                                        <asp:Label runat="server" ID="Label18" Font-Bold="false" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="right">
                                                        <asp:Label runat="server" ID="lbltotalTDSAmt" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="right">
                                                        <asp:Label runat="server" ID="lbltotalCGSTAmt" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="right">
                                                        <asp:Label runat="server" ID="lbltotalSGSTAmt" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="right">
                                                        <asp:Label runat="server" ID="lbltotalIGSTAmt" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="right">
                                                        <asp:Label runat="server" ID="lbltotalBillAmt" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="right">
                                                        <asp:Label runat="server" ID="lbltotalTCSAmt" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 3%" align="right">
                                                        <asp:Label runat="server" ID="lbltotalTCSNetpayAmt" Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                    <td style="width: 8%" align="left">
                                                        <asp:Label runat="server" ID="Label6" Font-Bold="false" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="15" style="border-bottom: 1px double black;">
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
                    <td align="center">
                        <table id="Table2" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed;"
                            class="largsize">
                            <tr>
                                <td style="width: 2%" align="center">
                                    <asp:Label runat="server" ID="Label29" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td style="width: 2%" align="left">
                                    <asp:Label runat="server" ID="Label30" Font-Bold="true" Text="Grand Total"></asp:Label>
                                </td>
                               <td style="width: 3%" align="left">
                                    <asp:Label runat="server" ID="Label23" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td style="width: 2%" align="right">
                                    <asp:Label runat="server" ID="lblgrandqntltotal" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td style="width: 3%" align="right">
                                    <asp:Label runat="server" ID="Label32" Font-Bold="false" Text=""></asp:Label>
                                </td>
                                <td style="width: 3%" align="right">
                                    <asp:Label runat="server" ID="lblgrandamounttotal" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td style="width: 3%" align="right">
                                    <asp:Label runat="server" ID="Label34" Font-Bold="false" Text=""></asp:Label>
                                </td>
                                <td style="width: 3%" align="right">
                                    <asp:Label runat="server" ID="lblgrandtotalTDSAmt" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td style="width: 3%" align="right">
                                    <asp:Label runat="server" ID="lblgrandtotalCGSTAmt" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td style="width: 3%" align="right">
                                    <asp:Label runat="server" ID="lblgrandtotalSGSTAmt" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td style="width: 3%" align="right">
                                    <asp:Label runat="server" ID="lblgrandtotalIGSTAmt" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td style="width: 3%" align="right">
                                    <asp:Label runat="server" ID="lblgrandtotalBillAmt" Font-Bold="false" Text=""></asp:Label>
                                </td>
                                <td style="width: 3%" align="right">
                                    <asp:Label runat="server" ID="lblgrandtotalTCSAmt" Font-Bold="false" Text=""></asp:Label>
                                </td>
                                <td style="width: 3%" align="right">
                                    <asp:Label runat="server" ID="lblgrandtotalTCSNetpayAmt" Font-Bold="false" Text=""></asp:Label>
                                </td>
                                <td style="width: 8%" align="left">
                                    <asp:Label runat="server" ID="Label42" Font-Bold="false" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="15" style="border-bottom: 1px double black;">
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
