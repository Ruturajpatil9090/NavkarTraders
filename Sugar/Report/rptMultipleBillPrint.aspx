<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptMultipleBillPrint.aspx.cs" Inherits="Sugar_Report_rptMultipleBillPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>INVOICE</title>
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
    <script type="text/javascript">
        function PrintPanel() {

            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('jwaksalebill.html', 'do', 'height=660,width=1350');
            printWindow.document.write('<html><head><link href="../print.css" rel="stylesheet" type="text/css" media="print"/>');
            printWindow.document.write('</head><body class="print">');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 100);
            return false;
        }

        function PrintPanel2() {
            var panel = document.getElementById("<%=pnl2.ClientID %>");
            var printWindow = window.open('salebill.html', 'do', 'height=660,width=1350');
            printWindow.document.write('<html><head><link href="../print.css" rel="stylesheet" type="text/css" media="print"/>');
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnPrint" runat="server" Text="Print" Width="80px" OnClientClick="return PrintPanel();"
            OnClick="btnPrint_Click" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnPriPrinted" Text="Pre-Printed"
            OnClick="btnPriPrinted_Click" OnClientClick="return PrintPanel2();" />
        <asp:Button ID="btnPDFDownload" runat="server" Width="110px" Text="PDF Download"
            OnClick="btnPDfDownload_Click" />
        &nbsp;&nbsp;<asp:Button runat="server" ID="btnSendEmail" Text="HTML Mail" OnClick="btnSendEmail_Click"
            Width="80px" />&nbsp;
        <asp:Button ID="btnpdf" runat="server" Width="70px" Text="PDF Mail" OnClick="btnPDf_Click" />
        <asp:TextBox runat="server" ID="txtEmail" Width="300px"></asp:TextBox>
    </div>
    <br />
    <div>
        <asp:Panel runat="server" ID="pnlMain" CssClass="toosmall" Style="width: 70%; margin: 0 auto;">
            <table width="80%" align="center" cellspacing="4" cellpadding="0" class="toosmall"
                runat="server" id="tblmn">
                <tr>
                    <td style="width: 100%;" class="toosmall">
                        <asp:DataList runat="server" ID="dtlist" Width="100%" OnItemDataBound="dtlist_OnItemDataBound"
                            CssClass="print9pt">
                            <ItemTemplate>
                                <table width="100%" style="table-layout: fixed; height: 80px;" class="toosmall" id="tblinner">
                                    <tr>
                                        <td style="width: 0%; vertical-align: top; visibility: hidden;" align="center" class="toosmall">
                                            <asp:Image runat="server" ID="imgLogo" ImageUrl="" Width="100%" Height="25%" />
                                        </td>
                                        <td style="width: 80%; vertical-align: top;" align="left" class="toosmall">
                                            <table width="100%" style="table-layout: fixed;" class="toosmall">
                                                <tr>
                                                    <td align="center" style="width: 100%; text-transform: uppercase; color: Red; font-size: xx-large;"
                                                        class="toosmall">
                                                        <asp:Label ID="lblCompany" runat="server" Text="" Font-Bold="true" CssClass="toosmall"
                                                            Style="font-size: x-large;" align="center"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 100%; text-transform: uppercase; color: Red; font-size: small;"
                                                        class="toosmall">
                                                        <asp:Label ID="Label8" runat="server" Text="" Font-Bold="true" CssClass="toosmall"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 100%; font-family: Verdana; font-size: medium;"
                                                        class="toosmall">
                                                        <asp:Label runat="server" ID="lblAl1" ForeColor="Blue" CssClass="toosmall"> </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 100%; font-family: Verdana; font-size: medium;"
                                                        class="toosmall">
                                                        <asp:Label runat="server" ID="lblAl2" ForeColor="Blue" CssClass="toosmall">  </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 100%; font-family: Verdana; font-size: medium;"
                                                        class="toosmall">
                                                        <asp:Label runat="server" ID="lblAl3" ForeColor="Blue" CssClass="toosmall"> </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 100%; font-family: Verdana; font-size: medium;"
                                                        class="toosmall">
                                                        <asp:Label runat="server" ID="lblAl4" ForeColor="Blue" CssClass="toosmall"> </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width: 100%; font-family: Verdana; font-size: medium;"
                                                        class="toosmall">
                                                        <asp:Label runat="server" ID="lblOtherDetails" ForeColor="Blue" CssClass="toosmall"> </asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" cellspacing="4" style="table-layout: fixed; border-bottom: 1px solid black;
                                    border-top: 1px solid black;" class="toosmall">
                                    <tr>
                                        <td style="width: 100%; height: 15px; font-size: large; text-align: center; padding-top: 5px;
                                            padding-bottom: 5px;">
                                            <b>TAX INVOICE</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="toosmall" style="height: 80px; border-top: 1px solid black;">
                                            <table cellspacing="0" style="width: 100%; table-layout: fixed; height: 80px;" class="toosmall">
                                                <tr>
                                                    <td style="width: 50%; border-right: 1px solid black; border-bottom: 1px solid black;"
                                                        align="left" class="toosmall">
                                                        <table style="width: 100%; table-layout: fixed;" class="toosmall">
                                                            <tr>
                                                                <td style="width: 30%; font-size: small;">
                                                                    Reverse Charge:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    NO
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: small;">
                                                                    Invoice No:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label runat="server" ID="lblSB_No" Text='<%#Eval("doc_no") %>' Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: small;">
                                                                    Invoice Date:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label runat="server" ID="lblBillDate" Text='<%#Eval("doc_date") %>' Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 50%; border-bottom: 1px solid black;" align="left" class="toosmall">
                                                        <table style="width: 100%;" class="toosmall">
                                                            <tr>
                                                                <td align="left" style="width: 100%; font-size: small;">
                                                                    Consignee Name:
                                                                    <asp:Label ID="Label3" Text='<%#Eval("PartyName") %>' runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="width: 30%; font-size: small;">
                                                                    GST Number:
                                                                    <asp:Label ID="Label19" Text='<%#Eval("Party_Gst") %>' runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="width: 40%; font-size: small;">
                                                                    HSN NO:
                                                                    <asp:Label ID="Label6" Text="17011410" runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: small;">
                                                                    State Code:
                                                                    <asp:Label ID="Label1" Text='<%#Eval("GSTStateCode") %>' Font-Bold="true" runat="server" />
                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; State:
                                                                    <asp:Label ID="Label2" Text='<%#Eval("GSTStateName") %>' runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" style="table-layout: fixed; border-bottom: 1px solid black;"
                                    class="toosmall">
                                    <tr>
                                        <td style="width: 5%; font-size: small;" align="left">
                                            <b>Item Description</b>
                                        </td>
                                        <%-- <td style="width: 5%; font-size: small;" align="left">
                                            <b>Brnd name</b>
                                        </td>--%>
                                        <td style="width: 5%; font-size: small;" align="right">
                                            <b>Quantity</b>
                                        </td>
                                        <td style="width: 5%; font-size: small;" align="right">
                                            <b>Net Weight</b>
                                        </td>
                                        <td style="width: 5%; font-size: small;" align="right">
                                            <b>Rate</b>
                                        </td>
                                        <td style="width: 5%; font-size: small;" align="right">
                                            <b>Sale Value</b>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" style="table-layout: fixed;" align="center" class="toosmall">
                                    <tr>
                                        <td style="width: 100%; vertical-align: top; height: 30px;" class="toosmall">
                                            <asp:DataList runat="server" ID="dtItemDetails" Width="100%" class="toosmall">
                                                <ItemTemplate>
                                                    <table width="100%" align="center" cellspacing="0px" style="table-layout: fixed;"
                                                        class="toosmall">
                                                        <tr>
                                                            <td style="width: 5%; font-size: small;" align="left">
                                                                <asp:Label runat="server" ID="Label9" Text='<%#Eval("Itemname") %>'></asp:Label>
                                                            </td>
                                                            <%-- <td style="width: 5%; font-size: small;" align="left">
                                                                <asp:Label runat="server" ID="Label10" Text='<%#Eval("Brand_Name") %>'></asp:Label>
                                                            </td>--%>
                                                            <td style="width: 5%; font-size: small;" align="right">
                                                                <asp:Label runat="server" ID="lblQntl" Text='<%#Eval("Qty") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 5%; font-size: small;" align="right">
                                                                <asp:Label runat="server" ID="Label7" Text='<%#Eval("Net_Wt") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 5%; font-size: small;" align="right">
                                                                <asp:Label runat="server" ID="lblBags" Text='<%#Eval("Rate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 5%; font-size: small;" align="right">
                                                                <asp:Label runat="server" ID="lblRate" Text='<%#Eval("Gross_Amnt") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" cellspacing="2" style="font-size: medium; table-layout: fixed;
                                    border-bottom: 1px solid black; height: 110px;" class="toosmall">
                                    <tr>
                                        <td colspan="11">
                                            <table width="100%" align="center" style="table-layout: fixed; border-top: 1px double black;
                                                border-bottom: 1px double black;" class="toosmall">
                                                <tr>
                                                    <td style="width: 20%; font-size: small;" align="right">
                                                    </td>
                                                    <td style="width: 20%; font-size: small;" align="right">
                                                        <b>Total Quantity:-</b>
                                                    </td>
                                                    <td style="width: 10%; font-size: small;" align="left">
                                                        <asp:Label Text="" runat="server" ID="lbltotalqauantity" />
                                                    </td>
                                                    <td style="width: 20%; font-size: small;" align="right">
                                                        <b>Total Net Weight:-</b>
                                                    </td>
                                                    <td style="width: 10%; font-size: small;" align="left">
                                                        <asp:Label Text="" runat="server" ID="lbltotalnetwt" />
                                                    </td>
                                                    <td style="width: 20%; font-size: small;" align="right">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%; font-size: small;" align="right" class="toosmall" colspan="10">
                                            <b>Sub Total:</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="lblSubTotal" Text='<%#Eval("Total") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%; font-size: small;" align="right" class="toosmall" colspan="10">
                                            <span style="text-align: left; float: left;" class="toosmall"></span><b>Shub </b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label5" Text='<%#Eval("Shub_Amount") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <span style="text-align: left; float: left;" class="toosmall"></span><b>Kharajat</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label4" Text='<%#Eval("Khajarat") %>'></asp:Label>
                                        </td>
                                        </tr>--%>
                                    <tr>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall" colspan="10">
                                            <span style="text-align: left; float: left;" class="toosmall"></span><b>P.Pol</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label11" Text='<%#Eval("P_Pol_Amount") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-top: 1px solid black;">
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall" colspan="10">
                                            <span style="text-align: left; float: right;" class="toosmall"></span><b>Post Phone</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label12" Text='<%#Eval("PostPhone") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall" colspan="10">
                                            <span style="text-align: left; float: left;" class="toosmall"></span><b>Round Off</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label13" Text='<%#Eval("roundoff") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%; border-bottom: 1px solid black; font-size: x-large; font-weight: bold;
                                            font-size: small;" align="right" class="toosmall" colspan="10">
                                            <span style="text-align: left; float: right;" class="toosmall"></span><b>Total Amount:</b>
                                        </td>
                                        <td style="width: 20%; border-bottom: 1px solid black; font-size: medium; font-weight: bold;"
                                            align="right" class="toosmall">
                                            <asp:Label runat="server" ID="lblBillAmount" Text='<%#Eval("BillAmt") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="11" align="left" class="toosmall" style="font-size: small; border-top: 1px solid black;
                                            padding-top: 6px;">
                                            Rs.:<asp:Label runat="server" ID="lblInwords" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" cellspacing="0" style="table-layout: fixed; height: 80px;"
                                    class="toosmallforimg">
                                    <tr>
                                        <td align="left" class="toosmallforimg" style="padding-top: 10px;">
                                            <asp:Label runat="server" ID="lblCmptinNo" Text="" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;FSSAI
                                            No:<asp:Label runat="server" ID="lblCompnayFSSAI_No" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%;" align="left" class="toosmallforimg">
                                        </td>
                                        <td rowspan="4" align="right" style="vertical-align: top;" class="toosmallforimg">
                                            <asp:Image runat="server" ID="imgSign" Height="40px" Width="100px" CssClass="toosmall" /><br />
                                            For,M/S.
                                            <asp:Label runat="server" ID="lblNameCmp" Font-Bold="true" CssClass="toosmall"></asp:Label><br />
                                            <%--<p style="font-size: small; font-style: italic;">--%>
                                            Authorised Signatory<%--</p>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="toosmallforimg" style="font-size: small;">
                                            1)This Invoice is for Goods sold to the above named party only.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: small;">
                                            2)We are not responsible for quality and contents of the Jaggery as it is processed
                                            by farmers & purchased directly from APMC market yard sangali.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="toosmallforimg" style="font-size: small;">
                                            3)Our Reponsibility entirely ceases as soon as the goods have been handed over to
                                            the carrier/transporter.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="toosmallforimg" style="font-size: small;">
                                            4)Bil not paid within 10 days by D.D will be subject to interest @ 18%p.a.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="toosmallforimg" colspan="2" style="">
                                            <asp:Label runat="server" ID="Label14" Text="Bank Details:" Font-Bold="true" Font-Size="12px"></asp:Label>
                                            <br />
                                            <asp:Label runat="server" ID="lblbank" Font-Bold="true" Text="1-" Style="font-size: small;">  </asp:Label>
                                            <asp:Label runat="server" ID="lblbnkdetails" Font-Bold="true" Style="font-size: small;">  </asp:Label>
                                            <br />
                                            <asp:Label runat="server" ID="Label20" Font-Bold="true" Text="2-" Style="font-size: small;">  </asp:Label>
                                            <asp:Label runat="server" ID="lblbnkdetails2" Font-Bold="true" Style="font-size: small;">  </asp:Label>
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
        <asp:Panel runat="server" ID="pnl2" CssClass="toosmall" Style="width: 70%; margin: 0 auto;">
            <table width="80%" align="center" cellspacing="4" cellpadding="0" class="print9pt"
                runat="server" id="Table1">
                <tr>
                    <td style="width: 100%;" class="toosmall">
                        <asp:DataList runat="server" ID="dtlist1" Width="100%" OnItemDataBound="dtlist_OnItemDataBound"
                            CssClass="print9pt">
                            <ItemTemplate>
                                <table width="100%" style="table-layout: fixed; height: 90px;" class="noprinttoosmall"
                                    id="tblinner">
                                    <tr>
                                        <td style="width: 20%; vertical-align: top;" align="center" class="noprinttoosmall">
                                        </td>
                                        <td style="width: 80%; vertical-align: top;" align="left" class="noprinttoosmall">
                                            <table width="100%" style="table-layout: fixed;" class="noprinttoosmall">
                                                <tr>
                                                    <td align="left" style="width: 100%; text-transform: uppercase; color: Red;" class="noprinttoosmall">
                                                        <asp:Label ID="lblCompany" runat="server" Text="" Font-Bold="true" CssClass="noprinttoosmall"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 100%; text-transform: uppercase; color: Red;" class="noprinttoosmall">
                                                        <asp:Label ID="Label8" runat="server" Text="" Font-Bold="true" CssClass="noprinttoosmall"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 100%; font-family: Verdana;" class="noprinttoosmall">
                                                        <asp:Label runat="server" ID="lblAl1" ForeColor="Blue" CssClass="noprinttoosmall"> </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 100%; font-family: Verdana;" class="noprinttoosmall">
                                                        <asp:Label runat="server" ID="lblAl2" ForeColor="Blue" CssClass="noprinttoosmall">  </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 100%; font-family: Verdana;" class="noprinttoosmall">
                                                        <asp:Label runat="server" ID="lblAl3" ForeColor="Blue" CssClass="noprinttoosmall"> </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 100%; font-family: Verdana;" class="noprinttoosmall">
                                                        <asp:Label runat="server" ID="lblAl4" ForeColor="Blue" CssClass="noprinttoosmall"> </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 100%; font-family: Verdana;" class="noprinttoosmall">
                                                        <asp:Label runat="server" ID="lblOtherDetails" ForeColor="Blue" CssClass="noprinttoosmall"> </asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" cellspacing="4" style="table-layout: fixed; border-bottom: 1px solid black;
                                    border-top: 1px solid black;" class="toosmall">
                                    <tr>
                                        <td style="width: 100%; height: 15px; font-size: large; text-align: center; padding-top: 5px;
                                            padding-bottom: 5px;">
                                            <b>TAX INVOICE</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="toosmall" style="height: 80px; border-top: 1px solid black;">
                                            <table cellspacing="0" style="width: 100%; table-layout: fixed; height: 80px;" class="toosmall">
                                                <tr>
                                                    <td style="width: 50%; border-right: 1px solid black; border-bottom: 1px solid black;"
                                                        align="left" class="toosmall">
                                                        <table style="width: 100%; table-layout: fixed;" class="toosmall">
                                                            <tr>
                                                                <td style="width: 30%; font-size: small;">
                                                                    Reverse Charge:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    NO
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: small;">
                                                                    Invoice No:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label runat="server" ID="lblSB_No" Text='<%#Eval("doc_no") %>' Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: small;">
                                                                    Invoice Date:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label runat="server" ID="lblBillDate" Text='<%#Eval("doc_date") %>' Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 50%; border-bottom: 1px solid black;" align="left" class="toosmall">
                                                        <table style="width: 100%;" class="toosmall">
                                                            <tr>
                                                                <td align="left" style="width: 100%; font-size: small;">
                                                                    Consignee Name:
                                                                    <asp:Label ID="Label3" Text='<%#Eval("PartyName") %>' runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="width: 30%; font-size: small;">
                                                                    GST Number:
                                                                    <asp:Label ID="Label19" Text='<%#Eval("Party_Gst") %>' runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="width: 40%; font-size: small;">
                                                                    HSN NO:
                                                                    <asp:Label ID="Label6" Text="17011410" runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: small;">
                                                                    State Code:
                                                                    <asp:Label ID="Label1" Text='<%#Eval("GSTStateCode") %>' Font-Bold="true" runat="server" />
                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; State:
                                                                    <asp:Label ID="Label2" Text='<%#Eval("GSTStateName") %>' runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" style="table-layout: fixed; border-bottom: 1px solid black;"
                                    class="toosmall">
                                    <tr>
                                        <td style="width: 5%; font-size: small;" align="left">
                                            <b>Item Description</b>
                                        </td>
                                        <%-- <td style="width: 5%; font-size: small;" align="left">
                                            <b>Brnd name</b>
                                        </td>--%>
                                        <td style="width: 5%; font-size: small;" align="right">
                                            <b>Quantity</b>
                                        </td>
                                        <td style="width: 5%; font-size: small;" align="right">
                                            <b>Net Weight</b>
                                        </td>
                                        <td style="width: 5%; font-size: small;" align="right">
                                            <b>Rate</b>
                                        </td>
                                        <td style="width: 5%; font-size: small;" align="right">
                                            <b>Sale Value</b>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" style="table-layout: fixed;" align="center" class="toosmall">
                                    <tr>
                                        <td style="width: 100%; vertical-align: top; height: 30px;" class="toosmall">
                                            <asp:DataList runat="server" ID="dtItemDetails" Width="100%" class="toosmall">
                                                <ItemTemplate>
                                                    <table width="100%" align="center" cellspacing="0px" style="table-layout: fixed;"
                                                        class="toosmall">
                                                        <tr>
                                                            <td style="width: 5%; font-size: small;" align="left">
                                                                <asp:Label runat="server" ID="Label9" Text='<%#Eval("Itemname") %>'></asp:Label>
                                                            </td>
                                                            <%-- <td style="width: 5%; font-size: small;" align="left">
                                                                <asp:Label runat="server" ID="Label10" Text='<%#Eval("Brand_Name") %>'></asp:Label>
                                                            </td>--%>
                                                            <td style="width: 5%; font-size: small;" align="right">
                                                                <asp:Label runat="server" ID="lblQntl" Text='<%#Eval("Qty") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 5%; font-size: small;" align="right">
                                                                <asp:Label runat="server" ID="Label7" Text='<%#Eval("Net_Wt") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 5%; font-size: small;" align="right">
                                                                <asp:Label runat="server" ID="lblBags" Text='<%#Eval("Rate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 5%; font-size: small;" align="right">
                                                                <asp:Label runat="server" ID="lblRate" Text='<%#Eval("Gross_Amnt") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" cellspacing="2" style="font-size: medium; table-layout: fixed;
                                    border-bottom: 1px solid black; height: 110px;" class="toosmall">
                                    <tr>
                                        <td colspan="11">
                                            <table width="100%" align="center" style="table-layout: fixed; border-top: 1px double black;
                                                border-bottom: 1px double black;" class="toosmall">
                                                <tr>
                                                    <td style="width: 20%; font-size: small;" align="right">
                                                    </td>
                                                    <td style="width: 20%; font-size: small;" align="right">
                                                        <b>Total Quantity:-</b>
                                                    </td>
                                                    <td style="width: 10%; font-size: small;" align="left">
                                                        <asp:Label Text="" runat="server" ID="lbltotalqauantity" />
                                                    </td>
                                                    <td style="width: 20%; font-size: small;" align="right">
                                                        <b>Total Net Weight:-</b>
                                                    </td>
                                                    <td style="width: 10%; font-size: small;" align="left">
                                                        <asp:Label Text="" runat="server" ID="lbltotalnetwt" />
                                                    </td>
                                                    <td style="width: 20%; font-size: small;" align="right">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%; font-size: small;" align="right" class="toosmall" colspan="10">
                                            <b>Sub Total:</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="lblSubTotal" Text='<%#Eval("Total") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%; font-size: small;" align="right" class="toosmall" colspan="10">
                                            <span style="text-align: left; float: left;" class="toosmall"></span><b>Shub </b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label5" Text='<%#Eval("Shub_Amount") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <span style="text-align: left; float: left;" class="toosmall"></span><b>Kharajat</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label4" Text='<%#Eval("Khajarat") %>'></asp:Label>
                                        </td>
                                        </tr>--%>
                                    <tr>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall" colspan="10">
                                            <span style="text-align: left; float: left;" class="toosmall"></span><b>P.Pol</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label11" Text='<%#Eval("P_Pol_Amount") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="border-top: 1px solid black;">
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall" colspan="10">
                                            <span style="text-align: left; float: right;" class="toosmall"></span><b>Post Phone</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label12" Text='<%#Eval("PostPhone") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall" colspan="10">
                                            <span style="text-align: left; float: left;" class="toosmall"></span><b>Round Off</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label13" Text='<%#Eval("roundoff") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%; border-bottom: 1px solid black; font-size: x-large; font-weight: bold;
                                            font-size: small;" align="right" class="toosmall" colspan="10">
                                            <span style="text-align: left; float: right;" class="toosmall"></span><b>Total Amount:</b>
                                        </td>
                                        <td style="width: 20%; border-bottom: 1px solid black; font-size: medium; font-weight: bold;"
                                            align="right" class="toosmall">
                                            <asp:Label runat="server" ID="lblBillAmount" Text='<%#Eval("BillAmt") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="11" align="left" class="toosmall" style="font-size: small; border-top: 1px solid black;
                                            padding-top: 6px;">
                                            Rs.:<asp:Label runat="server" ID="lblInwords" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" cellspacing="0" style="table-layout: fixed; height: 80px;"
                                    class="toosmallforimg">
                                    <tr>
                                        <td align="left" class="toosmallforimg" style="padding-top: 10px;">
                                            <asp:Label runat="server" ID="lblCmptinNo" Text="" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;FSSAI
                                            No:<asp:Label runat="server" ID="lblCompnayFSSAI_No" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%;" align="left" class="toosmallforimg">
                                        </td>
                                        <td rowspan="4" align="right" style="vertical-align: top;" class="toosmallforimg">
                                            <asp:Image runat="server" ID="imgSign" Height="40px" Width="100px" CssClass="toosmall" /><br />
                                            For,M/S.
                                            <asp:Label runat="server" ID="lblNameCmp" Font-Bold="true" CssClass="toosmall"></asp:Label><br />
                                            <%--<p style="font-size: small; font-style: italic;">--%>
                                            Authorised Signatory<%--</p>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="toosmallforimg" style="font-size: small;">
                                            1)This Invoice is for Goods sold to the above named party only.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: small;">
                                            2)We are not responsible for quality and contents of the Jaggery as it is processed
                                            by farmers & purchased directly from APMC market yard sangali.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="toosmallforimg" style="font-size: small;">
                                            3)Our Reponsibility entirely ceases as soon as the goods have been handed over to
                                            the carrier/transporter.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="toosmallforimg" style="font-size: small;">
                                            4)Bil not paid within 10 days by D.D will be subject to interest @ 18%p.a.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="toosmallforimg" colspan="2" style="">
                                            <asp:Label runat="server" ID="Label14" Text="Bank Details:" Font-Bold="true" Font-Size="12px"></asp:Label>
                                            <br />
                                            <asp:Label runat="server" ID="lblbank" Font-Bold="true" Text="1-" Style="font-size: small;">  </asp:Label>
                                            <asp:Label runat="server" ID="lblbnkdetails" Font-Bold="true" Style="font-size: small;">  </asp:Label>
                                            <br />
                                            <asp:Label runat="server" ID="Label20" Font-Bold="true" Text="2-" Style="font-size: small;">  </asp:Label>
                                            <asp:Label runat="server" ID="lblbnkdetails2" Font-Bold="true" Style="font-size: small;">  </asp:Label>
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
