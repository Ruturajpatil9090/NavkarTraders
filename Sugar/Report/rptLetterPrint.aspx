<%@ Page Title="rpt:Letter Print" Language="C#" AutoEventWireup="true" CodeFile="rptLetterPrint.aspx.cs" Inherits="Report_rptLetterPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('do.html', 'do', 'height=600,width=1360');
            printWindow.document.write('<html><head><link href="../print.css" rel="stylesheet" type="text/css" media="print" />');
            printWindow.document.write('</head><body class="print">');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 100);
            return false;
        }
    </script>
    <script type="text/javascript">
        function PrintPanel2() {
            var panel = document.getElementById("<%=pnlMain2.ClientID %>");
            var printWindow = window.open('do.html', 'do', 'height=600,width=1360');
            printWindow.document.write('<html><head><link href="../print.css" rel="stylesheet" type="text/css" media="print" />');
            printWindow.document.write('</head><body class="print">');
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
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" />
        &nbsp;
        <asp:Button ID="btnPrePrinted" runat="server" Text="Pre-Printed" CssClass="btnHelp"
            OnClientClick="return PrintPanel2();" Width="80px" />&nbsp;
        <asp:Button ID="btnMail" runat="server" Text="Mail" CssClass="btnHelp" Width="80px"
            OnClientClick="CheckEmail();" OnClick="btnMail_Click" />&nbsp;<asp:TextBox runat="server"
                ID="txtEmail" Width="226px"></asp:TextBox>
    </div>
    <div>
        <asp:Panel runat="server" ID="pnlMain" CssClass="print">
            <table width="60%" style="table-layout: fixed; height: 125px;" class="print9pt" align="center">
                <tr>
                    <td style="width: 20%; vertical-align: top;" align="center">
                        <asp:Image runat="server" ID="imgLogo" ImageUrl="~/Images/Logo.jpg" Width="100%"
                            Height="90%" />
                    </td>
                    <td style="width: 80%; vertical-align: top;" align="left">
                        <table width="100%" style="table-layout: fixed;">
                            <tr>
                                <td align="left" style="width: 100%; text-transform: uppercase; color: Red;" class="print9pt">
                                    <asp:Label ID="lblCompany" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; text-transform: uppercase; color: Red;" class="print9pt">
                                    <asp:Label ID="Label7" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; font-family: Verdana;" class="toosmall">
                                    <asp:Label runat="server" ID="lblAl1" ForeColor="Blue"> </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; font-family: Verdana;" class="toosmall">
                                    <asp:Label runat="server" ID="lblAl2" ForeColor="Blue"> </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; font-family: Verdana;" class="toosmall">
                                    <asp:Label runat="server" ID="lblAl3" ForeColor="Blue"> </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; font-family: Verdana;" class="toosmall">
                                    <asp:Label runat="server" ID="lblAl4" ForeColor="Blue"> </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; font-family: Verdana;" class="toosmall">
                                    <asp:Label runat="server" ID="lblOtherDetails" ForeColor="Blue"> </asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="60%" align="center" class="print" style="border-top: 4px solid black;">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataList runat="server" ID="dtl" Width="100%" OnItemDataBound="DataList_ItemDataBound">
                            <ItemTemplate>
                                <table runat="server" align="left" width="100%" cellpadding="1" id="tblMain" class="print"
                                    style="table-layout: fixed;">
                                    <tr style="height: 30px; vertical-align: top;">
                                        <td align="left" style="width: 50%">
                                            &nbsp; Ref.No:<asp:Label runat="server" ID="lblRefNo" Text='<%#Eval("RefNo") %>'
                                                Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 50%">
                                            Date:<asp:Label runat="server" ID="lblLetterDate" Text='<%#Eval("LetterDate") %>'></asp:Label>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 100%">
                                            To,
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 100%">
                                            <asp:Label runat="server" ID="lblPartyName" Font-Bold="true" Text='<%#Eval("Party") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 40%; height: auto;">
                                            <asp:Label runat="server" ID="lblAddress" Text='<%#Eval("address") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 100%; vertical-align: top;">
                                            <asp:Label runat="server" ID="lblPartyCity" Font-Bold="true" Text='<%#Eval("City") %>'></asp:Label>&nbsp;&nbsp;<asp:Label
                                                runat="server" ID="lblPincode" Text='<%#Eval("Pincode") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;">
                                            <table width="100%;" align="center" class="print">
                                                <tr>
                                                    <td style="width: 30%" align="right">
                                                        Kind Attention:
                                                    </td>
                                                    <td style="width: 70%" align="left">
                                                        &nbsp;&nbsp;<asp:Label runat="server" ID="lblKindAtt" Text='<%#Eval("kind_att") %>'
                                                            Font-Bold="true" Font-Underline="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%" align="right">
                                                        Subject:
                                                    </td>
                                                    <td style="width: 70%" align="left">
                                                        &nbsp;&nbsp;<asp:Label runat="server" ID="lblSubject" Text='<%#Eval("Subject") %>'
                                                            Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%" align="right">
                                                        Your Ref No:
                                                    </td>
                                                    <td style="width: 70%" align="left">
                                                        &nbsp;&nbsp;<asp:Label runat="server" ID="lblYourRefNo" Text='<%#Eval("Your_Refno") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 10%; height: 40px; vertical-align: top;" colspan="2">
                                            <asp:Label runat="server" ID="lblDearsir" Text="Dear Sir," Font-Bold="true"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Dated:&nbsp;
                                            <asp:Label runat="server" ID="Label1" Text='<%#Eval("Dated") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;" align="center" colspan="2">
                                            <table width="100%" style="table-layout: fixed;" align="center" class="print">
                                                <tr>
                                                    <td style="width: 5%" align="left" colspan="2">
                                                    </td>
                                                    <td style="width: 95%; word-wrap: break-word; text-wrap: normal;" align="left" colspan="2">
                                                        <asp:Label runat="server" ID="lblMatter" Text='<%#Eval("Matter") %>' Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="height: 50px; vertical-align: bottom;">
                                            Thanking You,
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Yours Faithfully,
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                             <asp:Image runat="server" ID="imgSign" Height="80px" Width="150px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="height: 40px; vertical-align: top;">
                                            <asp:Label runat="server" ID="Label2" Text='<%#Eval("CmpName") %>' Font-Bold="true"></asp:Label>
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
    <div style="display: none;">
        <asp:Panel runat="server" ID="pnlMain2" CssClass="print">
            <table width="60%" style="table-layout: fixed; height: 125px;" class="print9pt" align="center">
                <tr>
                    <td style="width: 20%; vertical-align: top;" align="center">
                    </td>
                    <td style="width: 80%; vertical-align: top;" align="left">
                        <table width="100%" style="table-layout: fixed;">
                            <tr>
                                <td align="left" style="width: 100%; text-transform: uppercase; color: Red;" class="noprint9pt">
                                    <asp:Label ID="Label3" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; font-family: Verdana;" class="noprinttoosmall">
                                    <asp:Label runat="server" ID="Label5" ForeColor="Blue"> </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; font-family: Verdana;" class="noprinttoosmall">
                                    <asp:Label runat="server" ID="Label6" ForeColor="Blue"> </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; font-family: Verdana;" class="noprinttoosmall">
                                    <asp:Label runat="server" ID="Label8" ForeColor="Blue"> </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; font-family: Verdana;" class="noprinttoosmall">
                                    <asp:Label runat="server" ID="Label9" ForeColor="Blue"> </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; font-family: Verdana;" class="noprinttoosmall">
                                    <asp:Label runat="server" ID="Label10" ForeColor="Blue"> </asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="60%" align="center" class="print" style="border-top: 4px solid black;">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataList runat="server" ID="dtlist2" Width="100%" OnItemDataBound="DataList_ItemDataBound">
                            <ItemTemplate>
                                <table runat="server" align="left" width="100%" cellpadding="1" id="tblMain" class="print">
                                    <tr style="height: 30px; vertical-align: top;">
                                        <td align="left" style="width: 50%">
                                            &nbsp; Ref.No:<asp:Label runat="server" ID="lblRefNo" Text='<%#Eval("RefNo") %>'
                                                Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 50%">
                                            Date:<asp:Label runat="server" ID="lblLetterDate" Text='<%#Eval("LetterDate") %>'></asp:Label>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 100%">
                                            To,
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 100%">
                                            <asp:Label runat="server" ID="lblPartyName" Font-Bold="true" Text='<%#Eval("Party") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 40%; height: auto;">
                                            <asp:Label runat="server" ID="lblAddress" Text='<%#Eval("address") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 100%; vertical-align: top;">
                                            <asp:Label runat="server" ID="lblPartyCity" Font-Bold="true" Text='<%#Eval("City") %>'></asp:Label>&nbsp;&nbsp;<asp:Label
                                                runat="server" ID="lblPincode" Text='<%#Eval("Pincode") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;">
                                            <table width="100%;" align="center" class="print">
                                                <tr>
                                                    <td style="width: 30%" align="right">
                                                        Kind Attention:
                                                    </td>
                                                    <td style="width: 70%" align="left">
                                                        &nbsp;&nbsp;<asp:Label runat="server" ID="lblKindAtt" Text='<%#Eval("kind_att") %>'
                                                            Font-Bold="true" Font-Underline="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%" align="right">
                                                        Subject:
                                                    </td>
                                                    <td style="width: 70%" align="left">
                                                        &nbsp;&nbsp;<asp:Label runat="server" ID="lblSubject" Text='<%#Eval("Subject") %>'
                                                            Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%" align="right">
                                                        Your Ref No:
                                                    </td>
                                                    <td style="width: 70%" align="left">
                                                        &nbsp;&nbsp;<asp:Label runat="server" ID="lblYourRefNo" Text='<%#Eval("Your_Refno") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 10%; height: 40px; vertical-align: top;" colspan="2">
                                            <asp:Label runat="server" ID="lblDearsir" Text="Dear Sir," Font-Bold="true"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Dated:&nbsp;
                                            <asp:Label runat="server" ID="Label1" Text='<%#Eval("Dated") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%;" align="center" colspan="2">
                                            <table width="100%" align="center" class="print">
                                                <tr>
                                                    <td style="width: 15%;">
                                                    </td>
                                                    <td style="width: 85%" align="left" colspan="2">
                                                        <asp:Label runat="server" ID="lblMatter" Text='<%#Eval("Matter") %>' Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="height: 50px; vertical-align: bottom;">
                                            Thanking You,
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Yours Faithfully,
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Image runat="server" ID="imgSign" Height="80px" Width="150px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="height: 40px; vertical-align: top;">
                                            <asp:Label runat="server" ID="Label2" Text='<%#Eval("CmpName") %>' Font-Bold="true"></asp:Label>
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
