<%@ Page Title="rpt:T Format" Language="C#" AutoEventWireup="true" CodeFile="rptTFormat.aspx.cs"
    Inherits="Report_rptTFormat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>T Format</title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=PrintPanel.ClientID %>");
            var printWindow = window.open('tformat.html', 'st', 'height=400,width=800');
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div align="left">
            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return PrintPanel();"
                Width="80px" />
            &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
                Width="120px" OnClick="btnExportToExcel_Click" />
            &nbsp;&nbsp;
             <asp:Button ID="btnPDFDownload" runat="server" Width="110px" Text="PDF Download"
                OnClick="btnPDfDownload_Click" />
            <asp:Button ID="btnpdf" runat="server" Width="70px" Text="PDF Mail To Party" OnClick="btnPDf_Click" />
            <asp:Button runat="server" ID="btnSendEmail" Text="Mail" OnClick="btnSendEmail_Click"
                Width="74px" />
            Email:<asp:TextBox runat="server" ID="txtEmail" Width="300px"></asp:TextBox>
            &nbsp;<asp:Button Text="Sort By Balance ASC" ID="btnSortByBalance" OnClick="btnSortByBalance_Click"
                runat="server" />
            &nbsp;<asp:Button Text="Sort By Name ASC" ID="btnSortByName" OnClick="btnSortByName_Click"
                runat="server" />
        </div>
        <asp:Panel ID="PrintPanel" runat="server" align="center" Font-Names="Calibri">
            <asp:Label ID="lblReportName" runat="server" Text="Trial Balance T Format" Width="100%"
                CssClass="lblName" Font-Bold="true" Font-Size="14px" Style="text-align: center;
                width: 100%;"></asp:Label>
            <table id="tbMain" runat="server" width="1000px" align="center" style="page-break-after: avoid;">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblCompany" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblCompanyAddr" runat="server" Text="Kolhapur"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblCompanyMobile" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="1000px" align="center">
                <tr>
                    <td style="width: 50%; border-bottom: double 3px black; border-top: double 3px black;">
                        <table width="100%" cellspacing="2">
                            <tr>
                                <td align="center" style="width: 10%;">
                                    <asp:Label ID="lblAC_Code" runat="server" Text="Ac_Code" Font-Bold="true" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="left" style="width: 40%;">
                                    <asp:Label ID="lblAC_Name" runat="server" Text="Account Name" Font-Bold="true" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="left" style="width: 20%;">
                                    <asp:Label ID="Label5" runat="server" Text="City Name" Font-Bold="true" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="center" style="width: 30%; border-right: solid 1px black;">
                                    <asp:Label ID="lblACBalance" runat="server" Text="Balance" Font-Bold="true" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; border-bottom: double 3px black; border-top: double 3px black;">
                        <table width="100%" cellspacing="2">
                            <tr>
                                <td align="center" style="width: 10%;">
                                    <asp:Label ID="Label1" runat="server" Text="Ac_Code" Font-Bold="true" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="left" style="width: 40%;">
                                    <asp:Label ID="Label2" runat="server" Text="Account Name" Font-Bold="true" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="left" style="width: 20%;">
                                    <asp:Label ID="Label6" runat="server" Text="City Name" Font-Bold="true" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="center" style="width: 30%;">
                                    <asp:Label ID="Label3" runat="server" Text="Balance" Font-Bold="true" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="width: 50%; border-right: solid 1px black;">
                        <asp:DataList ID="dtl_Credit" runat="server" Width="100%">
                            <ItemTemplate>
                                <table width="100%" cellspacing="2">
                                    <tr>
                                        <td align="center" style="width: 10%;">
                                            <asp:Label ID="lblAC_Code" runat="server" Text='<%#Eval("Ac_Code") %>' Font-Bold="true"
                                                Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 40%;">
                                            <asp:Label ID="lblAC_Name" runat="server" Text='<%#Eval("Ac_Name") %>' Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 20%; text-align: left;">
                                            <asp:Label ID="Label7" runat="server" Text='<%#Eval("CityName") %>' Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 30%;">
                                            <asp:Label ID="lblACBalance" runat="server" Text='<%#Eval("Balance") %>' Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                    <td valign="top" style="width: 50%;">
                        <asp:DataList ID="dtl_Debit" runat="server" Width="100%">
                            <ItemTemplate>
                                <table width="100%" cellspacing="2">
                                    <tr>
                                        <td align="center" style="width: 10%;">
                                            <asp:Label ID="lblAC_Code" runat="server" Text='<%#Eval("AC_Code") %>' Font-Bold="true"
                                                Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 40%;">
                                            <asp:Label ID="lblAC_Name" runat="server" Text='<%#Eval("Ac_Name") %>' Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 20%; text-align: left;">
                                            <asp:Label ID="Label7" runat="server" Text='<%#Eval("CityName") %>' Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 30%;">
                                            <asp:Label ID="lblACBalance" runat="server" Text='<%#Eval("Balance") %>' Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="width: 50%; border-top: 1px solid Black; border-bottom: 1px solid Black;">
                        <table width="100%">
                            <tr>
                                <td align="center" style="width: 10%;">
                                    <asp:Label ID="lbltotalamount" runat="server" Text="" Font-Bold="true" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="left" style="width: 40%;">
                                </td>
                                <td align="left" style="width: 20%;">
                                </td>
                                <td align="center" style="width: 30%;">
                                    <asp:Label ID="lblCreditTotal" runat="server" Text="" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" style="width: 50%; border-top: 1px solid Black; border-bottom: 1px solid Black;">
                        <table width="100%">
                            <tr>
                                <td align="center" style="width: 10%;">
                                    <asp:Label ID="Label4" runat="server" Text="" Font-Bold="true" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="left" style="width: 40%;">
                                </td>
                                <td align="left" style="width: 20%;">
                                </td>
                                <td align="center" style="width: 30%;">
                                    <asp:Label ID="lblDebitTotal" runat="server" Text="" Font-Size="12px"></asp:Label>
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
