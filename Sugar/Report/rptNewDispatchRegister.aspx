<%@ Page Title="rpt:Dispatch Register" Language="C#" AutoEventWireup="true" CodeFile="rptNewDispatchRegister.aspx.cs"
    Inherits="Report_rptNewDispatchRegister" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dispatch Register</title>
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
    <%-- <style type="text/css" media="print">
        div#pagewidth
        {
            margin: 0px auto 0px auto;
            overflow: hidden;
            width: 500px;
            display: inline;
        }
    </style>--%>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pagewidth.ClientID %>");
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
    <script type="text/javascript">
        function vp(VNO, type) {
            window.open('../Report/rptVouchersNew.aspx?VNO=' + VNO + '&type=' + type);
        }
        function sb(VNO, type) {
            window.open('../Report/rptSaleBillNew.aspx?billno=' + VNO);
        }
        function DO(DO, Action) {
            window.open('../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx?DO=' + DO + '&Action=' + Action);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="border-bottom: 2px dashed black; height: 30px;">
            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
                Width="80px" />
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnPrintOrMail" Text="Print or Mail"
                Width="120px" OnClick="btnPrintOrMail_Click" />
            <br />
        </div>
        <div class="print2" id="pagewidth" runat="server">
            <asp:Panel runat="server" ID="pnlMain">
                <table width="90%" style="table-layout: fixed;" align="center" class="print2">
                    <tr>
                        <td align="center">
                            <b>Date Wise Dispatch Register</b>
                        </td>
                    </tr>
                </table>
                <table width="90%" style="table-layout: fixed;" align="center" class="print2">
                    <tr>
                        <td align="left" style="width: 3%; border-bottom: 1px solid black; border-top: 1px solid black;">#
                        </td>
                        <td align="left" style="width: 15%; border-bottom: 1px solid black; border-top: 1px solid black;">Mill
                        </td>
                        <td align="left" style="width: 8%; border-bottom: 1px solid black; border-top: 1px solid black;">Rate
                        </td>
                        <td align="center" style="width: 7%; border-bottom: 1px solid black; border-top: 1px solid black;">Quantal
                        </td>
                        <td align="left" style="width: 20%; border-bottom: 1px solid black; border-top: 1px solid black;">Name of Party
                        </td>
                        <td align="left" style="width: 10%; border-bottom: 1px solid black; border-top: 1px solid black;">Truck
                        </td>
                        <td align="left" style="width: 18%; border-bottom: 1px solid black; border-top: 1px solid black;">Transport
                        </td>
                        <td align="left" style="width: 9%; border-bottom: 1px solid black; border-top: 1px solid black;">DO
                        </td>
                        <td align="right" style="width: 3%; border-bottom: 1px solid black; border-top: 1px solid black;"
                            class="noprinttoosmall">Print
                        </td>
                        <td align="right" style="width: 2%; border-bottom: 1px solid black; border-top: 1px solid black;"
                            class="noprinttoosmall">Mail
                        </td>
                    </tr>
                </table>
                <asp:DataList runat="server" ID="dtlist" Width="100%" OnItemDataBound="dtlist_OnItemDataBound">
                    <ItemTemplate>
                        <table width="90%" align="center" class="print2" id="tblMain" runat="server" cellspacing="1"
                            cellpadding="0">
                            <tr>
                                <td style="width: 100%;">
                                    <table width="100%" align="center" class="print2">
                                        <tr>
                                            <td style="width: 100%;">
                                                <table width="100%" align="center" style="table-layout: fixed;" cellspacing="1" class="print2">
                                                    <tr style="border-bottom: 1px solid black;">
                                                        <td align="left" style="width: 3%;"></td>
                                                        <td align="center" style="width: 15%; background-color: #FFFFCC; font-size: large; border-bottom: 1px solid black;">
                                                            <asp:Label runat="server" ID="lblDate" Text='<%#Eval("Do_Date") %>' Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 8%;"></td>
                                                        <td align="center" style="width: 7%;">
                                                            <asp:Label runat="server" ID="lblQntlTotal" Text="" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 20%;"></td>
                                                        <td align="left" style="width: 10%;"></td>
                                                        <td align="left" style="width: 18%;"></td>
                                                        <td align="left" style="width: 9%;"></td>
                                                        <td align="right" style="width: 3%;"></td>
                                                        <td align="right" style="width: 2%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="10">
                                                            <table width="100%" align="center" class="print2">
                                                                <tr>
                                                                    <td style="width: 100%;">
                                                                        <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                                            <ItemTemplate>
                                                                                <table width="100%" style="table-layout: fixed;" align="center" cellspacing="2" class="print2">
                                                                                    <tr>
                                                                                        <td align="left" style="width: 3%;">
                                                                                          <%--  <asp:LinkButton runat="server" ID="lblDoNo" Text='<%#Eval("doc_no") %>' OnClick="lnkTenderNo_Click"></asp:LinkButton>--%>
                                                                                            <asp:Label runat="server" ID="lblDoNo" Font-Bold="true" Text='<%#Eval("doc_no") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" style="width: 15%;">
                                                                                            <asp:Label runat="server" ID="Label1" Font-Bold="true" Text='<%#Eval("mill") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" style="width: 8%;">
                                                                                            <asp:Label runat="server" ID="Label2" Font-Bold="false" Text='<%#Eval("millrate") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" style="width: 7%;">
                                                                                            <asp:Label runat="server" ID="Label3" Font-Bold="false" Text='<%#Eval("qntl") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" style="width: 20%;" class="toosmall">
                                                                                            <asp:Label runat="server" ID="Label4" Font-Bold="true" Text='<%#Eval("party") %>'
                                                                                                Font-Size="Small"></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" style="width: 10%;">
                                                                                            <asp:Label runat="server" ID="Label5" Font-Bold="false" Text='<%#Eval("lorry") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" style="width: 18%;" class="printtoosmall">
                                                                                            <asp:Label runat="server" ID="Label6" Font-Bold="false" Text='<%#Eval("transport") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="left" style="width: 9%;">
                                                                                            <asp:Label runat="server" ID="Label7" Font-Bold="false" Text='<%#Eval("DO") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td align="right" style="width: 3%;" class="noprinttoosmall">
                                                                                            <asp:CheckBox runat="server" ID="chkPrint" />
                                                                                        </td>
                                                                                        <td align="right" style="width: 2%;" class="noprinttoosmall">
                                                                                            <asp:CheckBox runat="server" ID="chkMail" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="10">
                                                                                            <table width="100%" align="center" cellspacing="2" class="print2" style="table-layout: fixed;">
                                                                                                <tr>
                                                                                                    <td style="width: 5%;" align="center">Broker:
                                                                                                    </td>
                                                                                                    <td style="border-bottom: 1px solid black; width: 10%;">
                                                                                                        <asp:Label runat="server" ID="lblBroker" Text='<%#Eval("brokername") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 2%;">S.R.
                                                                                                    </td>
                                                                                                    <td style="border-bottom: 1px solid black; width: 8%;">
                                                                                                        <asp:Label runat="server" ID="lblSR" Text='<%#Eval("salerate") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 12%;" align="left">
                                                                                                        <asp:Label runat="server" ID="lblGrade" Text='<%#Eval("grade") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 3%;" align="right">
                                                                                                        <asp:Label runat="server" ID="Label12" Text="Frieght:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="border-bottom: 1px solid black; width: 5%;" align="center">
                                                                                                        <asp:Label runat="server" ID="Label8" Text='<%#Eval("frieght") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 3%;" align="right">
                                                                                                        <asp:Label runat="server" ID="lbll" Text="Ref.No:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="border-bottom: 1px solid black; width: 5%;" align="center">
                                                                                                        <asp:Label runat="server" ID="Label10" Text='<%#Eval("refno") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="right" style="width: 3%;" class="noprint"></td>
                                                                                                    <td align="right" style="width: 2%;" class="noprint"></td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="10">
                                                                                            <table width="100%" align="center" style="table-layout: fixed;" cellspacing="2" class="print2">
                                                                                                <tr>
                                                                                                    <td style="width: 2%; border-left: 2px double black;" align="center">
                                                                                                        <asp:Label runat="server" ID="lblPurcNo" Text='<%#Eval("tn") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 2%; border-right: 2px double black;" align="center">
                                                                                                        <asp:Label runat="server" ID="lblOrder" Text='<%#Eval("tdn") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 13%;">
                                                                                                        <asp:Label runat="server" ID="lblnarration" Text='<%#Eval("narration") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 5%;" align="right">
                                                                                                        <asp:Label runat="server" ID="Label13" Text="Adv.Frieght:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 5%; border-bottom: 1px solid black;" align="center">
                                                                                                        <asp:Label runat="server" ID="Label9" Text='<%#Eval("advancefrieght") %>' Font-Bold="true"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 4%;" align="right">
                                                                                                        <asp:Label runat="server" ID="Label14" Text="Voc.No:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 5%; border-bottom: 1px solid black;" align="center">
                                                                                                        <asp:Label runat="server" ID="lblVoucNo" Text='<%#Eval("SB_No") %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 4%;" align="right">
                                                                                                        <asp:Label runat="server" ID="Label19" Text="TCS Rate:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 5%; border-bottom: 1px solid black;" align="center">
                                                                                                        <asp:Label runat="server" ID="Label20" Text='<%#Eval("tscrate") %>' Font-Bold="true"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 4%;" align="right">
                                                                                                        <asp:Label runat="server" ID="Label21" Text="Bill Amount:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 5%; border-bottom: 1px solid black;" align="center">
                                                                                                        <asp:Label runat="server" ID="Label22" Text='<%#Eval("netpayble") %>' Font-Bold="true"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 5%;" align="right">
                                                                                                        <asp:Label runat="server" ID="Label23" Text="TCS Amount:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 5%; border-bottom: 1px solid black;" align="center">
                                                                                                        <asp:Label runat="server" ID="Label24" Text='<%#Eval("tcsamount") %>' Font-Bold="true"></asp:Label>
                                                                                                    </td>

                                                                                                    <td style="width: 4%;" align="right">
                                                                                                        <asp:Label runat="server" ID="Label15" Text="Final Total:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 5%; border-bottom: 1px solid black;" align="center">
                                                                                                        <asp:Label runat="server" ID="Label11" Text='<%#Eval("FinalAmount") %>' Font-Bold="true"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="right" style="width: 2%;" class="noprint"></td>
                                                                                                    <td align="right" style="width: 2%;" class="noprint"></td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="10">
                                                                                            <table width="100%" align="center" style="border-bottom: 1px dashed black; table-layout: fixed;"
                                                                                                cellspacing="1" class="print2">
                                                                                                <tr>
                                                                                                    <td style="width: 100%;" align="left">
                                                                                                        <asp:Label runat="server" ID="lblGetpass" Text='<%#Eval("getpass") %>' Font-Bold="true"></asp:Label>&nbsp;&nbsp;<asp:Label
                                                                                                            runat="server" ID="Label17" Text='<%#Eval("PaymentTo") %>' Font-Bold="false"></asp:Label>&nbsp;&nbsp;<asp:Label
                                                                                                                runat="server" ID="Label16" Text='<%#Eval("narr4") %>' Font-Bold="false"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label
                                                                                                                    runat="server" ID="Label18" Text='<%#Eval("shiptoname") %>' Font-Bold="false"></asp:Label>
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
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </asp:Panel>
        </div>
        <div id="vouchermail" style="display: none;">
            <asp:Panel ID="pnlVoucher" runat="server" align="center" Font-Names="Calibri">
                <asp:DataList runat="server" ID="dtlVoucher" Width="100%" OnItemDataBound="dtlVoucher_OnItemDataBound">
                    <ItemTemplate>
                        <table id="tbMain" runat="server" align="center" style="table-layout: fixed;" width="1000px"
                            class="print">
                            <tr>
                                <td style="width: 100%;" colspan="2">
                                    <table width="100%" style="table-layout: fixed; height: 125px;" class="print9pt">
                                        <tr>
                                            <td style="width: 20%; vertical-align: top;" align="center">
                                                <asp:Image runat="server" ID="imgLogo" ImageUrl="~/Images/Logo.jpg" Width="100%"
                                                    Height="30%" />
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
                                                            <asp:Label ID="Label5" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"></asp:Label>
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
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-top: solid 1px black; border-bottom: solid 1px black;">Voucher No:<asp:Label runat="server" ID="lblVoucherNo" Font-Bold="true" Text='<%#Eval("VoucherNo") %>'></asp:Label>
                                    <asp:Label ID="lblDocNo" runat="server" Text='<%#Eval("doc_no") %>' Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" style="border-top: solid 1px black; border-bottom: solid 1px black;">Date:
                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Doc_Date") %>' Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left" class="toosmall" style="border-bottom: 1px solid black;">
                                    <table width="100%" align="center" style="height: 165px; table-layout: fixed;" class="toosmall">
                                        <tr>
                                            <td align="left" style="width: 50%; vertical-align: top;" class="toosmall">
                                                <table width="100%" align="center" class="toosmall" cellspacing="2" style="table-layout: fixed; height: 100%;">
                                                    <tr>
                                                        <td align="left" style="font-size: small;">Buyer,
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="print9pt">
                                                            <asp:Label ID="lblPartyName" runat="server" Font-Bold="true" Text='<%#Eval("PartyName") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="print9pt">
                                                            <asp:Label ID="lblPartyAddr" runat="server" Text='<%#Eval("PartyAddress") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="font-size: small;" class="toosmall">City:&nbsp;<asp:Label ID="lblPartyCity" runat="server" Font-Bold="true" Text='<%#Eval("party_city") %>'></asp:Label>
                                                            &nbsp;State:&nbsp;<asp:Label runat="server" ID="lblPartyState" Text='<%#Eval("party_state") %>'
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top; height: 50px;" class="toosmall">
                                                            <table width="100%" align="center" class="toosmall" style="table-layout: fixed;">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblCSTNo" runat="server" Text='<%#Eval("Cst_no") %>'></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblGSTNo" runat="server" Text='<%#Eval("Gst_No") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblTinNo" runat="server" Text='<%#Eval("Tin_No") %>'></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblLICNo" runat="server" Text='<%#Eval("Local_Lic_No") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblECCNo" runat="server" Text='<%#Eval("ECC_No") %>'></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("CompanyPan") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="left" style="width: 50%; vertical-align: top;" class="toosmall">
                                                <table width="100%" align="center" class="toosmall" cellspacing="2" style="table-layout: fixed; height: 100%;">
                                                    <tr>
                                                        <td align="left" class="print9pt">
                                                            <asp:Label runat="server" ID="lblConsignedto" Text='<%#Eval("CT") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="print9pt">
                                                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text='<%#Eval("PartyNameC") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="print9pt">
                                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("PartyAddressC") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="font-size: small;" class="toosmall">
                                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("party_cityC") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top; height: 50px;" class="toosmall">
                                                            <table width="100%" align="center" class="toosmall" style="table-layout: fixed;">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("Cst_noC") %>'></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("Gst_NoC") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label8" runat="server" Text='<%#Eval("Tin_NoC") %>'></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label9" runat="server" Text='<%#Eval("Local_Lic_NoC") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label10" runat="server" Text='<%#Eval("ECC_NoC") %>'></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label11" runat="server" Text='<%#Eval("CompanyPanC") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <table width="100%" cellspacing="2" cellpadding="0" style="table-layout: fixed; height: 25px;"
                                        class="print9pt">
                                        <tr>
                                            <td style="width: 50%" align="left">Dispatched From: &nbsp;<asp:Label ID="lblDispatchFrom" runat="server" Text='<%#Eval("From_Place") %>'></asp:Label>
                                            </td>
                                            <td style="width: 50%" align="left">To:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTo" runat="server"
                                                Text='<%#Eval("To_Place") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%" align="left">Lorry No:&nbsp;&nbsp;
                                            <asp:Label ID="lblLorryNo" runat="server" Text='<%#Eval("Lorry_No") %>'></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                                ID="lblDriverMobile" runat="server" Text='<%#Eval("driver_no") %>'></asp:Label>
                                            </td>
                                            <td style="width: 50%" align="left">
                                                <asp:Label runat="server" ID="lblBroker" Text='<%#Eval("BrokerShortNew") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-bottom: solid 1px black;"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-bottom: solid 1px black; height: 15px;">Particulars:-&nbsp;Quantal : &nbsp;&nbsp;<asp:Label ID="lblQuantal" runat="server"
                                    Text='<%#Eval("Quantal") %>'></asp:Label>&nbsp;&nbsp; Bags:&nbsp;&nbsp;<asp:Label
                                        ID="lblBags" runat="server" Text='<%#Eval("BAGS") %>'></asp:Label>&nbsp;&nbsp;
                                Grade:&nbsp;&nbsp;<asp:Label ID="lblGrade" runat="server" Text='<%#Eval("Grade") %>'></asp:Label>&nbsp;&nbsp;
                                Mill Rate:&nbsp;&nbsp;<asp:Label ID="lblRate" runat="server" Text='<%#Eval("Mill_Rate") %>'></asp:Label>&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-bottom: solid 1px black; height: 70px; vertical-align: top;"
                                    class="print9pt">We have paid on your behalf in account of<br />
                                    <asp:Label ID="lblMillNameFull" runat="server" Text='<%#Eval("MillName") %>' Font-Bold="true"></asp:Label>&nbsp;Credit
                                the same to our account & debit to mill's account
                                </td>
                                <td align="right" style="border-bottom: solid 1px black;" class="print9pt">
                                    <asp:Label ID="lblMillAmount" runat="server" Text='<%#Eval("Mill_Amount") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-bottom: solid 1px black;" align="right">
                                    <table width="50%" align="right" class="small" style="table-layout: fixed; height: 145px;">
                                        <tr>
                                            <td align="left">rate diff debit/credit your a/c:
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblRateDiff" runat="server" Text='<%#Eval("Diff_Rate").ToString()=="0.00" || Eval("Diff_Rate").ToString()=="0"?"":Eval("Diff_Rate","{0}") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">Bank Commission:
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblBankCommission" runat="server" Text='<%#Eval("BANK_COMMISSION").ToString()=="0.00" || Eval("BANK_COMMISSION").ToString()=="0"?"":Eval("BANK_COMMISSION","{0}") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">Brokrage:
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblBrokrage" runat="server" Text='<%#Eval("Brokrage").ToString()=="0.00" || Eval("Brokrage").ToString()=="0"?"":Eval("Brokrage","{0}")%>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">Quality Difference:
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblQualityDiff" runat="server" Text='<%#Eval("RATEDIFF").ToString()=="0.00" || Eval("RATEDIFF").ToString()=="0"?"":Eval("RATEDIFF","{0}") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">Commission:
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblCommission" runat="server" Text='<%#Eval("Commission_Amount").ToString()=="0.00" || Eval("Commission_Amount").ToString()=="0"?"":Eval("Commission_Amount","{0}") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">Freight & Other Exp:
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblFreight" runat="server" Text='<%#Eval("FREIGHT").ToString()=="0.00" || Eval("FREIGHT").ToString()=="0"?"":Eval("FREIGHT","{0}") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">Post and Phone:
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblPostAmt" runat="server" Text='<%#Eval("Postage").ToString()=="0.00" || Eval("Postage").ToString()=="0"?"":Eval("Postage","{0}") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">Interest:
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblInterest" runat="server" Text='<%#Eval("Interest").ToString()=="0.00" || Eval("Interest").ToString()=="0"?"":Eval("Interest","{0}") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">Transport:
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblTransport" runat="server" Text='<%#Eval("Cash_Ac_Amount").ToString()=="0.00" || Eval("Cash_Ac_Amount").ToString()=="0"?"":Eval("Cash_Ac_Amount","{0}") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 40%;">Other:
                                            </td>
                                            <td align="right" style="width: 20%;">
                                                <asp:Label ID="lblOther" runat="server" Text='<%#Eval("OTHER_Expenses").ToString()=="0.00" || Eval("OTHER_Expenses").ToString()=="0"?"":Eval("OTHER_Expenses","{0}") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-bottom: solid 1px black;">
                                    <table width="100%" align="right" class="print" style="table-layout: fixed; height: 20px;">
                                        <tr>
                                            <td align="left" style="width: 40%;">Total:
                                            </td>
                                            <td align="right" style="width: 20%;">
                                                <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Voucher_Amount") %>'></asp:Label>&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-bottom: solid 1px black;">
                                    <table width="100%" align="right" style="table-layout: fixed; height: 20px;" class="print">
                                        <tr>
                                            <td align="left" style="width: 40%;">Credit account total
                                            </td>
                                            <td align="left" style="width: 40%;">RTGS Rs.:
                                            </td>
                                            <td align="right" style="width: 20%;">
                                                <asp:Label ID="lblTotalAmt" Font-Bold="true" runat="server" Text='<%#Eval("Voucher_Amount") %>'></asp:Label>&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="border-bottom: 1px solid black;">In Words:<asp:Label runat="server" ID="lblInWords" Font-Bold="true" Text='<%#Eval("InWords") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-bottom: solid 1px black;">
                                    <asp:Label ID="lblNote" Font-Bold="true" runat="server" Text="Note:"></asp:Label>&nbsp;&nbsp;
                                After dispatch of the goods we are not responsible for non-delivery or any kind
                                of damage or demand.
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-bottom: solid 1px black;">
                                    <table width="100%" class="print" style="table-layout: fixed; height: 50px;">
                                        <tr>
                                            <td style="height: 50px; width: 100%; vertical-align: top;">
                                                <table width="100%" align="left" style="table-layout: fixed;" class="print">
                                                    <tr>
                                                        <td style="width: 70%;" align="left">
                                                            <asp:Label ID="lblNarration1" Font-Bold="true" runat="server" Text='<%#Eval("Narration1") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 30%;" rowspan="2" align="right">
                                                            <asp:Image runat="server" ID="imgSign" Height="40px" Width="150px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%;" align="left">
                                                            <asp:Label ID="lblNarration2" Font-Bold="true" runat="server" Text='<%#Eval("ASN_No") %>'
                                                                Visible="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%;" align="right">For,<asp:Label runat="server" ID="lblSignCmpName"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-bottom: solid 1px black; height: 25px;" class="print9pt">1)Please credit the amount in our account and send the amount by RTGS immediately.
                                <br />
                                    2)If the amount is not sent before the due date payment charges 24% will be charged.
                                <br />
                                    3)This is computer generated print No Signature Required.
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </asp:Panel>
        </div>
        <div id="salebillmail" style="display: none;">
            <asp:Panel runat="server" ID="pnlSaleBill" CssClass="print">
                <table width="70%" align="center" cellspacing="4" cellpadding="0" class="print">
                    <tr>
                        <td style="width: 100%;">
                            <asp:DataList runat="server" ID="dtlSaleBill" Width="100%" OnItemDataBound="dtlSaleBill_OnItemDataBound">
                                <ItemTemplate>
                                    <table width="100%" style="table-layout: fixed; height: 125px;" class="print9pt">
                                        <tr>
                                            <td style="width: 20%; vertical-align: top;" align="center">
                                                <asp:Image runat="server" ID="imgLogo" ImageUrl="~/Images/Logo.jpg" Width="100%"
                                                    Height="30%" />
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
                                                            <asp:Label ID="Label8" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"></asp:Label>
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
                                    <table width="100%" align="center" cellspacing="4" style="table-layout: fixed; border-bottom: 1px solid black; border-top: 1px solid black;"
                                        class="print">
                                        <tr>
                                            <td align="left">Invoice No: &nbsp;
                                            <asp:Label runat="server" ID="lblSB_No" Text='<%#Eval("#") %>' Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="right">Invoice Date:&nbsp;<asp:Label runat="server" ID="lblBillDate" Text='<%#Eval("dt") %>'
                                                Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="border-top: 1px solid black;">
                                                <table width="100%" style="table-layout: fixed; height: 160px;">
                                                    <tr>
                                                        <td align="left" style="width: 65%;">Buyers Name & Address,
                                                        </td>
                                                        <td align="left" style="width: 35%; vertical-align: top;" rowspan="5">
                                                            <table width="100%" align="center" style="table-layout: fixed; vertical-align: text-top;">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label runat="server" ID="lblPartySLN" Text='<%#Eval("Party_SLN") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label runat="server" ID="lblPartyTIN" Text='<%#Eval("Party_TIN") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label runat="server" ID="Label5" Text='<%#Eval("Party_Cst") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label runat="server" ID="Label6" Text='<%#Eval("Party_Gst") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label runat="server" ID="Label4" Text='<%#Eval("Party_Ecc") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label runat="server" ID="Label7" Text='<%#Eval("Party_PAN") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label runat="server" ID="lblBuyerName" Text='<%#Eval("Party_Name") %>' Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="3" align="left" style="vertical-align: top;">
                                                            <asp:Label runat="server" ID="lblPartyAddress" Text='<%#Eval("Party_Address") %>'></asp:Label><br />
                                                            <asp:Label runat="server" ID="lblPhone" Text='<%#Eval("Party_Phone") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">City:
                                                        <asp:Label runat="server" ID="lblPartyCity" Text='<%#Eval("Party_City") %>' Font-Bold="true"></asp:Label>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; State:
                                                        <asp:Label runat="server" ID="lblPartyState" Font-Bold="true" Text='<%#Eval("Party_State") %>'></asp:Label>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pin_Code:<asp:Label runat="server"
                                                                ID="lblPartyPincode" Font-Bold="true" Text='<%#Eval("Party_Pin") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" align="center" cellspacing="4" style="table-layout: fixed; border-bottom: 1px solid black;"
                                        class="print">
                                        <tr>
                                            <td colspan="2" align="left">Mill Name:&nbsp;<asp:Label runat="server" ID="lblMillName" Text='<%#Eval("Mill_Name") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;" align="left">Dispatched From:&nbsp;<asp:Label runat="server" ID="lblDispatchedFrom" Text='<%#Eval("From_Place") %>'
                                                Font-Bold="true"></asp:Label>
                                            </td>
                                            <td style="width: 50%;" align="left">To:&nbsp;<asp:Label runat="server" ID="Label3" Text='<%#Eval("To_Place") %>' Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">Lorry No:&nbsp;<asp:Label runat="server" ID="lbllorry" Text='<%#Eval("lorry") %>'></asp:Label>
                                            </td>
                                            <td align="left">Driver Mobile:&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" align="center" style="table-layout: fixed; border-bottom: 1px solid black;"
                                        class="print">
                                        <tr>
                                            <td style="width: 30%;" align="left">
                                                <b>Particulars</b>
                                            </td>
                                            <td style="width: 10%;" align="center">
                                                <b>Quintal</b>
                                            </td>
                                            <td style="width: 10%;" align="center">
                                                <b>Packing(kg)</b>
                                            </td>
                                            <td style="width: 10%;" align="center">
                                                <b>Bags</b>
                                            </td>
                                            <td style="width: 10%;" align="center">
                                                <b>Rate</b>
                                            </td>
                                            <td style="width: 10%;" align="center">
                                                <b>Value</b>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" style="table-layout: fixed;" align="center">
                                        <tr>
                                            <td style="width: 100%; vertical-align: top; height: 95px;">
                                                <asp:DataList runat="server" ID="dtItemDetails" Width="100%">
                                                    <ItemTemplate>
                                                        <table width="100%" align="center" cellspacing="0px" style="table-layout: fixed; border-bottom: 1px dashed black;"
                                                            class="print">
                                                            <tr>
                                                                <td style="width: 30%;" align="left">
                                                                    <asp:Label runat="server" ID="lblGrade" Text='<%#Eval("Item") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;" align="center">
                                                                    <asp:Label runat="server" ID="lblQntl" Text='<%#Eval("Qntl") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;" align="center">
                                                                    <asp:Label runat="server" ID="lblPacking" Text='<%#Eval("Packing") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;" align="center">
                                                                    <asp:Label runat="server" ID="lblBags" Text='<%#Eval("Bags") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;" align="center">
                                                                    <asp:Label runat="server" ID="lblRate" Text='<%#Eval("Rate") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 10%;" align="center">
                                                                    <asp:Label runat="server" ID="lblvalue" Text='<%#Eval("Value") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" align="center" cellspacing="2" style="font-size: medium; table-layout: fixed; margin-top: 45px; border-bottom: 1px solid black; height: 190px;"
                                        class="print">
                                        <tr>
                                            <td style="width: 80%;" align="right">
                                                <b>Sub Total:</b>
                                            </td>
                                            <td style="width: 20%;" align="right">
                                                <asp:Label runat="server" ID="Label2" Text='<%#Eval("Sub_Total") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 80%;" align="right">
                                                <b>Vat 0%:</b>
                                            </td>
                                            <td style="width: 20%;" align="right">
                                                <asp:Label runat="server" ID="Label1" Text="Nil"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 80%;" align="right">
                                                <b>Less Frieght:</b>
                                            </td>
                                            <td style="width: 20%;" align="right">
                                                <asp:Label runat="server" ID="lblLessFrieght" Text='<%#Eval("Less_Frieght").ToString()=="0.00" || Eval("Less_Frieght").ToString()=="0"?"":Eval("Less_Frieght","{0}") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 80%;" align="right">
                                                <b>Cash Advance:</b>
                                            </td>
                                            <td style="width: 20%;" align="right">
                                                <asp:Label runat="server" ID="lblCashAdvance" Text='<%#Eval("Cash_Advance").ToString()=="0.00" || Eval("Cash_Advance").ToString()=="0"?"":Eval("Cash_Advance","{0}") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 80%;" align="right">
                                                <b>Rate Diff:</b><asp:Label runat="server" ID="lblBankCommRate" Text='<%#Eval("RateDiff").ToString()=="0.00"?"":Eval("RateDiff","{0}") %>'></asp:Label>/Qntl:
                                            </td>
                                            <td style="width: 20%;" align="right">
                                                <asp:Label runat="server" ID="lblBankComm" Text='<%#Eval("Bank_Commission").ToString()=="0.00"?"":Eval("Bank_Commission","{0}") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 80%;" align="right">
                                                <b>Other Expense:</b>
                                            </td>
                                            <td style="width: 20%;" align="right">
                                                <asp:Label runat="server" ID="lblOtherExpe" Text='<%#Eval("Other_Expenses").ToString()=="0.00"?"":Eval("Other_Expenses","{0}") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 80%; border-bottom: 1px solid black;" align="right">
                                                <b>Total Amount:</b>
                                            </td>
                                            <td style="width: 20%; border-bottom: 1px solid black;" align="right">
                                                <asp:Label runat="server" ID="lblBillAmount" Text='<%#Eval("Bill_Amount") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="left">Rs.:<asp:Label runat="server" ID="lblInwords" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" align="center" cellspacing="0" class="print9pt" style="table-layout: fixed; height: 120px;">
                                        <tr>
                                            <td align="left">Our TIN No.:
                                            <asp:Label runat="server" ID="lblCmptinNo" Text="27770980728" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 70%;" align="left">
                                                <b><u>Note:</u></b>&nbsp;After Dispatch of the goods we are not responsible for
                                            non delivery or any kind of damage.
                                            </td>
                                            <td rowspan="3" align="right">
                                                <asp:Image runat="server" ID="imgSign" Height="40px" Width="150px" /><br />
                                                For,<asp:Label runat="server" ID="lblNameCmp" Font-Italic="true" Font-Bold="true"></asp:Label><br />
                                                <p style="font-size: large; font-style: italic;">
                                                    Authorised Signatory
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">1) Please credit the amount in our account and send the amount by RTGS immediately.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">2) If the amount is not sent before the due date payment Interest 24% will be charged.
                                            <br />
                                                3)This is computer generated print No Signature Required.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 30px;"></td>
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
