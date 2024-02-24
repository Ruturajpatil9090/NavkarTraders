<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptSugarPurchaseReturnForGST.aspx.cs"
    Inherits="Report_rptSugarPurchaseReturnForGST" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>INVOICE</title>
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
    <script type="text/javascript">
        function PrintPanel() {
            debugger;
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
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
        &nbsp;&nbsp;<asp:Button runat="server" ID="Button1" Text="HTML Mail" OnClick="btnHTMLSendEmail_Click"
            Width="80px" />&nbsp; &nbsp;&nbsp;<asp:Button runat="server" ID="btnSendEmail" Text="PDF Mail"
                OnClick="btnSendEmail_Click" Width="70px" />&nbsp;
        <asp:TextBox runat="server" ID="txtEmail" Width="300px"></asp:TextBox>
    </div>
    <br />
    <div>
        <asp:Panel runat="server" ID="pnlMain" CssClass="toosmall" Width="70%" Style="margin: 0 auto;
            font-size: large;">
            <table width="100%" align="center" cellspacing="4" style="font-size: small; font-family: Times New Roman;"
                cellpadding="0" class="toosmall" runat="server" id="maintable">
                <tr>
                    <td style="width: 100%;" class="toosmall">
                        <asp:DataList runat="server" ID="dtlist" Width="100%" OnItemDataBound="dtlist_OnItemDataBound"
                            CssClass="print9pt">
                            <ItemTemplate>
                                <table width="100%" style="table-layout: fixed;" class="toosmall1">
                                    <tr>
                                        <td style="width: 100%; vertical-align: top;" align="center" class="toosmall1">
                                            <asp:Image runat="server" ID="imgLogo" ImageUrl="~/Images/Logo.jpg" Width="100%"
                                                Height="20%" />
                                        </td>
                                        <td style="width: 437%; vertical-align: top;" align="left" class="toosmall1">
                                            <table width="100%" style="table-layout: fixed;" class="toosmall1">
                                                <tr>
                                                    <td align="left" style="text-transform: uppercase; color: Red; font-size: large;"
                                                        class="toosmallcompnm">
                                                        <asp:Label ID="lblCompany" runat="server" Text="" Font-Bold="true" CssClass="toosmallcompnm"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="text-transform: uppercase; color: Red;" class="toosmall1">
                                                        <asp:Label ID="Label8" runat="server" Text="" Font-Bold="true" CssClass="toosmall1"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="font-family: Verdana; font-size: medium;" class="toosmall1">
                                                        <asp:Label runat="server" ID="lblAl1" ForeColor="Blue" CssClass="toosmall1"> </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="font-family: Verdana; font-size: medium;" class="toosmall1">
                                                        <asp:Label runat="server" ID="lblAl2" ForeColor="Blue" CssClass="toosmall1">  </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="font-family: Verdana; font-size: medium;" class="toosmall1">
                                                        <asp:Label runat="server" ID="lblAl3" ForeColor="Blue" CssClass="toosmall1"> </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="font-family: Verdana; font-size: medium;" class="toosmall1">
                                                        <asp:Label runat="server" ID="lblAl4" ForeColor="Blue" CssClass="toosmall1"> </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="font-family: Verdana; font-size: medium;" class="toosmall1">
                                                        <asp:Label runat="server" ID="lblOtherDetails" ForeColor="Blue" CssClass="toosmall1"> </asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%" align="center" cellspacing="4" style="table-layout: fixed; border-bottom: 1px solid black;
                                    border-top: 1px solid black; margin-top: -20px;" class="toosmall">
                                    <tr>
                                        <td style="width: 100%; height: 20px;" align="center">
                                            <b>CREDIT NOTE</b>
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
                                                                    Credit Note No:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <b>RP</b><asp:Label runat="server" ID="lblSB_No" Text='<%#Eval("#") %>' Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: small;">
                                                                    Invoice Date:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label runat="server" ID="lblBillDate" Text='<%#Eval("dt") %>' Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: small;">
                                                                    State:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label ID="Label1" Text='<%#Eval("CompanyState") %>' Font-Bold="true" runat="server" />
                                                                </td>
                                                                <td style="font-size: small;">
                                                                    State Code:
                                                                    <asp:Label ID="Label2" Text='<%#Eval("CompanyGSTStateCode") %>' runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    Against Sell Bill:&nbsp;
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label ID="Label16" Text='<%#Eval("PURCNO") %>' runat="server" class="toosmall"
                                                                        Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 50%; border-bottom: 1px solid black;" align="left" class="toosmall">
                                                        <table style="width: 100%;" class="toosmall">
                                                            <tr>
                                                                <td align="left" style="width: 40%; font-size: small;">
                                                                    Our GST Number:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label ID="Label19" Text='<%#Eval("CompanyGST") %>' runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="font-size: small;">
                                                                    Transportation Mode:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="font-size: small;">
                                                                    Date Of Supply:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label ID="Label3" Text='<%#Eval("dt") %>' runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="font-size: small;">
                                                                    Place Of Supply:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label ID="Label4" Text='<%#Eval("To_State") %>' runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="toosmall" style="height: 100px;">
                                            <table width="100%" align="center" style="height: 100px; table-layout: fixed;" class="toosmall">
                                                <tr>
                                                    <td align="left" style="width: 50%; height: 100px; vertical-align: top; border-right: 1px solid black;"
                                                        class="toosmall">
                                                        <table width="100%" align="center" class="toosmall" cellspacing="2" style="table-layout: fixed;
                                                            height: 100%;">
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    Buyer,
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    <asp:Label ID="lblPartyName" runat="server" Font-Bold="true" Text='<%#Eval("billtoname") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    <asp:Label ID="lblPartyAddr" runat="server" Text='<%#Eval("billaddress") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    &nbsp; City:&nbsp;<asp:Label ID="lblPartyCity" runat="server" Font-Bold="true" Text='<%#Eval("billcityname") %>'
                                                                        class="toosmall"></asp:Label>
                                                                    &nbsp;State:&nbsp;<asp:Label runat="server" ID="lblPartyState" Text='<%#Eval("billtostatename") %>'
                                                                        class="toosmall" Font-Bold="true"></asp:Label>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;State
                                                                    &nbsp;Code:&nbsp;<asp:Label ID="Label5" Text='<%#Eval("billstatecode") %>' runat="server"
                                                                        class="toosmall" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top; height: 40px; font-size: small;" class="toosmall">
                                                                    <table width="100%" align="center" class="toosmall" style="table-layout: fixed;">
                                                                        <tr>
                                                                            <td align="left" style="font-size: small;">
                                                                                <asp:Label ID="lblGSTNo" runat="server" Text='<%#Eval("billgstno") %>' Font-Bold="true"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="font-size: small;">
                                                                                <asp:Label ID="Label12" runat="server" Text='<%#Eval("billcompanypan") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="font-size: small;">
                                                                                <asp:Label ID="lblLICNo" runat="server" Text='<%#Eval("billsln") %>'></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="font-size: small;">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left" style="width: 50%; height: 100px; vertical-align: top;" class="toosmall">
                                                        <table width="100%" align="center" class="toosmall" cellspacing="2" style="table-layout: fixed;
                                                            height: 100%;">
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    <asp:Label runat="server" ID="lblConsignedto" Text='<%#Eval("CT") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    <asp:Label ID="Label6" runat="server" Font-Bold="true" Text='<%#Eval("UnitName") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("UnitAddress") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="font-size: medium; font-size: small;" class="toosmall">
                                                                    <asp:Label ID="Label8s" runat="server" Text='<%#Eval("UnitCity") %>' class="toosmall"></asp:Label>&nbsp;
                                                                    <asp:Label ID="Label9" Text='<%#Eval("UnitGSTStateCode") %>' runat="server" class="toosmall" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top; height: 40px;" class="toosmall">
                                                                    <table width="100%" align="center" class="toosmall" style="table-layout: fixed;">
                                                                        <tr>
                                                                            <td align="left" style="font-size: small;">
                                                                                <asp:Label ID="Label10" runat="server" Text='<%#Eval("UnitGST") %>' Font-Bold="true"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="font-size: small;">
                                                                                <asp:Label ID="Label11" runat="server" Text='<%#Eval("UnitPan") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="font-size: small;">
                                                                                <asp:Label ID="Label12a" runat="server" Text='<%#Eval("UnitLicNo") %>'></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="font-size: small;">
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
                                <table width="100%" align="center" cellspacing="4" style="table-layout: fixed; border-bottom: 1px solid black;"
                                    class="toosmall">
                                    <tr>
                                        <td style="width: 60%; font-size: small; height: 10px;" align="left" class="toosmall">
                                            Mill Name:&nbsp;<asp:Label runat="server" ID="lblMillName" Text='<%#Eval("Mill_Name") %>'></asp:Label>
                                        </td>
                                        <td style="width: 40%; font-size: small;" align="left" class="toosmall">
                                            <asp:Label runat="server" ID="Label9w" Text='<%#Eval("PODetails") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 60%; font-size: small; height: 10px;" align="left" class="toosmall">
                                            Dispatched From:&nbsp;<asp:Label runat="server" ID="lblDispatchedFrom" Text='<%#Eval("From_Place") %>'
                                                Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="width: 40%; font-size: small;" align="left" class="toosmall">
                                            To:&nbsp;<asp:Label runat="server" ID="Labewl9" Text='<%#Eval("To_Place") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="toosmall" style="font-size: small; height: 10px;">
                                            Lorry No:&nbsp;<asp:Label runat="server" ID="lbllorry" Text='<%#Eval("lorry") %>'></asp:Label>
                                        </td>
                                        <td align="left" class="toosmall" style="font-size: small;">
                                            <asp:Label runat="server" ID="Label13" Text='<%#Eval("DriverMobile") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" style="table-layout: fixed; border-bottom: 1px solid black;"
                                    class="toosmall">
                                    <tr>
                                        <td style="width: 10%; height: 25px; font-size: small;" align="left">
                                            <b>Particulars</b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="center">
                                            <b>HSN/ACS</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="center">
                                            <b>Quintal</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="center">
                                            <b>Packing(kg)</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="center">
                                            <b>Bags</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="center">
                                            <b>Rate/Qntl</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="center">
                                            <b>Value</b>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" style="table-layout: fixed;" align="center" class="toosmall">
                                    <tr>
                                        <td style="width: 100%; vertical-align: top; height: 40px;" class="toosmall">
                                            <asp:DataList runat="server" ID="dtItemDetails" Width="100%" class="toosmall">
                                                <ItemTemplate>
                                                    <table width="100%" align="center" cellspacing="0px" style="table-layout: fixed;
                                                        border-bottom: 1px dashed black;" class="toosmall">
                                                        <tr>
                                                            <td style="width: 10%; font-size: small;" align="left">
                                                                <asp:Label runat="server" ID="lblGrade" Text='<%#Eval("Item") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 20%; font-size: small; font-family: 'Times New Roman';" align="center">
                                                                <asp:Label runat="server" ID="Label9" Text='<%#Eval("HSN") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 10%; font-size: small;" align="center">
                                                                <asp:Label runat="server" ID="lblQntl" Text='<%#Eval("Qntl") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 10%; font-size: small;" align="center">
                                                                <asp:Label runat="server" ID="lblPacking" Text='<%#Eval("Packing") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 10%; font-size: small;" align="center">
                                                                <asp:Label runat="server" ID="lblBags" Text='<%#Eval("Bags") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 10%; font-size: small;" align="center">
                                                                <asp:Label runat="server" ID="lblRate" Text='<%#Eval("Rate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 10%; font-size: small;" align="center">
                                                                <asp:Label runat="server" ID="lblvalue" Text='<%#Eval("Value") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" cellspacing="2" style="font-size: medium; table-layout: fixed;
                                    margin-top: -10px; border-bottom: 1px solid black; height: 130px;" class="toosmall">
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <b>Sub Total:</b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="lblSubTotal" Text='<%#Eval("Sub_Total") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <b>CGST %:</b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label16s" Text='<%#Eval("CGSTRate").ToString()=="0.00" || Eval("CGSTRate").ToString()=="0"?"":Eval("CGSTRate","{0}") %>'></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                                runat="server" ID="Label14" Text='<%#Eval("CGSTAmount").ToString()=="0.00" || Eval("CGSTAmount").ToString()=="0"?"":Eval("CGSTAmount","{0}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <b>SGST %: </b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label17" Text='<%#Eval("SGSTRate").ToString()=="0.00" || Eval("SGSTRate").ToString()=="0"?"":Eval("SGSTRate","{0}") %>'></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label runat="server" ID="Label15" Text='<%#Eval("SGSTAmount").ToString()=="0.00" || Eval("SGSTAmount").ToString()=="0"?"":Eval("SGSTAmount","{0}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <b>IGST %:</b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label18" Text='<%#Eval("IGSTRate").ToString()=="0.00" || Eval("IGSTRate").ToString()=="0"?"":Eval("IGSTRate","{0}") %>'></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label runat="server" ID="Label16as" Text='<%#Eval("IGSTAmount").ToString()=="0.00" || Eval("IGSTAmount").ToString()=="0"?"":Eval("IGSTAmount","{0}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <b>Frieght:</b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="lblLessFrieght" Text='<%#Eval("LESS_FRT_RATE").ToString()=="0.00" || Eval("LESS_FRT_RATE").ToString()=="0"?"":Eval("LESS_FRT_RATE","{0}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <b>Bank Commission:</b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="lblBankComm" Text='<%#Eval("Bank_Commission").ToString()=="0.00"?"":Eval("Bank_Commission","{0}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <asp:Label ID="Label29" Font-Bold="true" runat="server" Text='<%#Eval("CarporateLine") %>'
                                                Visible="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <b>Other Expense:</b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="lblOtherExpe" Text='<%#Eval("Other_Expenses").ToString()=="0.00"?"":Eval("Other_Expenses","{0}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <b>Cash Advance:</b>
                                        </td>
                                        <td style="width: 20%;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="lblCashAdvance" Text='<%#Eval("Cash_Advance").ToString()=="0.00"?"":Eval("Cash_Advance","{0}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; border-bottom: 1px solid black; height: 30px;"
                                            align="right" class="toosmall">
                                            <b>Total Amount:</b>
                                        </td>
                                        <td style="width: 20%; font-size: small; border-bottom: 1px solid black;" align="right"
                                            class="toosmall">
                                            <asp:Label runat="server" ID="lblBillAmount" Text='<%#Eval("Bill_Amount") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" class="toosmall" style="font-size: small; height: 23px;">
                                            Rs.:<asp:Label runat="server" ID="lblInwords" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" class="toosmall" style="font-size: small; height: 10px;">
                                            <b>Please credit the above amount to your account and debit the same to our account</b>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" cellspacing="0" style="table-layout: fixed; height: 80px;"
                                    class="toosmallforimg">
                                    <tr>
                                        <td align="left" class="toosmallforimg" style="font-size: small; height: 25px;">
                                            Our TIN No.:
                                            <asp:Label runat="server" ID="lblCmptinNo" Text="" Font-Bold="true"></asp:Label>&nbsp;&nbsp;FSSAI
                                            No:<asp:Label runat="server" ID="lblCompnayFSSAI_No" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%; font-size: small; height: 40px;" align="left" class="toosmallforimg">
                                            <b><u>Note:</u></b>&nbsp;<br />
                                            - After Dispatch of the goods we are not responsible for non delivery or any kind
                                            of damage.<br />
                                            - Certified that the particulars given above are true and correct.<br />
                                            - Please credit the amount in our account and send the amount by RTGS immediately.<br />
                                            - If the amount is not sent before the due date payment Interest 24% will be charged.<br />
                                            - I/We hereby certify that food/foods mentioned in this invoice is/are warranted
                                            to be of the nature and quality which it/these purports/purported to be
                                        </td>
                                        <td align="right" style="vertical-align: top; font-size: small;" class="toosmallforimg">
                                             <asp:Image runat="server" ID="imgSign" ImageUrl="~/Images/STAMP_GSTC.jpg" Width="100%"
                                                Height="20%" />
                                        </td>
                                            <asp:Label runat="server" ID="Label25" Font-Bold="true" CssClass="toosmall" Text="For,"></asp:Label>
                                            <asp:Label runat="server" ID="lblNameCmp" Font-Bold="true" CssClass="toosmall"></asp:Label><br />
                                            <asp:Label runat="server" ID="Label26" Font-Bold="true" CssClass="toosmall" Text="Authorised Signatory"></asp:Label>
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
        <asp:Panel runat="server" ID="pnl2" CssClass="toosmall">
            <table width="70%" align="center" cellspacing="4" cellpadding="0" class="print9pt">
                <tr>
                    <td style="width: 100%;" class="print9pt">
                        <asp:DataList runat="server" ID="dtlist1" Width="100%" OnItemDataBound="dtlist_OnItemDataBound"
                            CssClass="print9pt">
                            <ItemTemplate>
                                <table width="100%" style="table-layout: fixed; height: 100px;" class="noprinttoosmall">
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
                                    border-top: 1px solid black; margin-top: -20px;" class="toosmall">
                                    <tr>
                                        <td style="width: 100%; height: 20px;" align="center">
                                            <b>CREDIT NOTE</b>
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
                                                                    Credit Note No:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <b>RP</b><asp:Label runat="server" ID="lblSB_No" Text='<%#Eval("#") %>' Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: small;">
                                                                    Invoice Date:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label runat="server" ID="lblBillDate" Text='<%#Eval("dt") %>' Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: small;">
                                                                    State:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label ID="Label1" Text='<%#Eval("CompanyState") %>' Font-Bold="true" runat="server" />
                                                                </td>
                                                                <td style="font-size: small;">
                                                                    State Code:
                                                                    <asp:Label ID="Label2" Text='<%#Eval("CompanyGSTStateCode") %>' runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    Against Sell Bill:&nbsp;
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label ID="Label16" Text='<%#Eval("PURCNO") %>' runat="server" class="toosmall"
                                                                        Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 50%; border-bottom: 1px solid black;" align="left" class="toosmall">
                                                        <table style="width: 100%;" class="toosmall">
                                                            <tr>
                                                                <td align="left" style="width: 40%; font-size: small;">
                                                                    Our GST Number:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label ID="Label19" Text='<%#Eval("CompanyGST") %>' runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="font-size: small;">
                                                                    Transportation Mode:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="font-size: small;">
                                                                    Date Of Supply:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label ID="Label3" Text='<%#Eval("dt") %>' runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="font-size: small;">
                                                                    Place Of Supply:
                                                                </td>
                                                                <td align="left" style="font-size: small;">
                                                                    <asp:Label ID="Label4" Text='<%#Eval("To_State") %>' runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="toosmall" style="height: 100px;">
                                            <table width="100%" align="center" style="height: 100px; table-layout: fixed;" class="toosmall">
                                                <tr>
                                                    <td align="left" style="width: 50%; height: 100px; vertical-align: top; border-right: 1px solid black;"
                                                        class="toosmall">
                                                        <table width="100%" align="center" class="toosmall" cellspacing="2" style="table-layout: fixed;
                                                            height: 100%;">
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    Buyer,
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    <asp:Label ID="lblPartyName" runat="server" Font-Bold="true" Text='<%#Eval("billtoname") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    <asp:Label ID="lblPartyAddr" runat="server" Text='<%#Eval("billaddress") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    &nbsp; City:&nbsp;<asp:Label ID="lblPartyCity" runat="server" Font-Bold="true" Text='<%#Eval("billcityname") %>'
                                                                        class="toosmall"></asp:Label>
                                                                    &nbsp;State:&nbsp;<asp:Label runat="server" ID="lblPartyState" Text='<%#Eval("billtostatename") %>'
                                                                        class="toosmall" Font-Bold="true"></asp:Label>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;State
                                                                    &nbsp;Code:&nbsp;<asp:Label ID="Label5" Text='<%#Eval("billstatecode") %>' runat="server"
                                                                        class="toosmall" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top; height: 40px; font-size: small;" class="toosmall">
                                                                    <table width="100%" align="center" class="toosmall" style="table-layout: fixed;">
                                                                        <tr>
                                                                            <td align="left" style="font-size: small;">
                                                                                <asp:Label ID="lblGSTNo" runat="server" Text='<%#Eval("billgstno") %>' Font-Bold="true"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="font-size: small;">
                                                                                <asp:Label ID="Label12" runat="server" Text='<%#Eval("billcompanypan") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="font-size: small;">
                                                                                <asp:Label ID="lblLICNo" runat="server" Text='<%#Eval("billsln") %>'></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="font-size: small;">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left" style="width: 50%; height: 100px; vertical-align: top;" class="toosmall">
                                                        <table width="100%" align="center" class="toosmall" cellspacing="2" style="table-layout: fixed;
                                                            height: 100%;">
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    <asp:Label runat="server" ID="lblConsignedto" Text='<%#Eval("CT") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    <asp:Label ID="Label6" runat="server" Font-Bold="true" Text='<%#Eval("UnitName") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="toosmall" style="font-size: small;">
                                                                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("UnitAddress") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="font-size: medium; font-size: small;" class="toosmall">
                                                                    <asp:Label ID="Label8s" runat="server" Text='<%#Eval("UnitCity") %>' class="toosmall"></asp:Label>&nbsp;
                                                                    <asp:Label ID="Label9" Text='<%#Eval("UnitGSTStateCode") %>' runat="server" class="toosmall" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top; height: 40px;" class="toosmall">
                                                                    <table width="100%" align="center" class="toosmall" style="table-layout: fixed;">
                                                                        <tr>
                                                                            <td align="left" style="font-size: small;">
                                                                                <asp:Label ID="Label10" runat="server" Text='<%#Eval("UnitGST") %>' Font-Bold="true"></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="font-size: small;">
                                                                                <asp:Label ID="Label11" runat="server" Text='<%#Eval("UnitPan") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="font-size: small;">
                                                                                <asp:Label ID="Label12a" runat="server" Text='<%#Eval("UnitLicNo") %>'></asp:Label>
                                                                            </td>
                                                                            <td align="left" style="font-size: small;">
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
                                <table width="100%" align="center" cellspacing="4" style="table-layout: fixed; border-bottom: 1px solid black;"
                                    class="toosmall">
                                    <tr>
                                        <td style="width: 60%; font-size: small; height: 10px;" align="left" class="toosmall">
                                            Mill Name:&nbsp;<asp:Label runat="server" ID="lblMillName" Text='<%#Eval("Mill_Name") %>'></asp:Label>
                                        </td>
                                        <td style="width: 40%; font-size: small;" align="left" class="toosmall">
                                            <asp:Label runat="server" ID="Label9w" Text='<%#Eval("PODetails") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 60%; font-size: small; height: 10px;" align="left" class="toosmall">
                                            Dispatched From:&nbsp;<asp:Label runat="server" ID="lblDispatchedFrom" Text='<%#Eval("From_Place") %>'
                                                Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="width: 40%; font-size: small;" align="left" class="toosmall">
                                            To:&nbsp;<asp:Label runat="server" ID="Labewl9" Text='<%#Eval("To_Place") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="toosmall" style="font-size: small; height: 10px;">
                                            Lorry No:&nbsp;<asp:Label runat="server" ID="lbllorry" Text='<%#Eval("lorry") %>'></asp:Label>
                                        </td>
                                        <td align="left" class="toosmall" style="font-size: small;">
                                            <asp:Label runat="server" ID="Label13" Text='<%#Eval("DriverMobile") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" style="table-layout: fixed; border-bottom: 1px solid black;"
                                    class="toosmall">
                                    <tr>
                                        <td style="width: 10%; height: 25px; font-size: small;" align="left">
                                            <b>Particulars</b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="center">
                                            <b>HSN/ACS</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="center">
                                            <b>Quintal</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="center">
                                            <b>Packing(kg)</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="center">
                                            <b>Bags</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="center">
                                            <b>Rate/Qntl</b>
                                        </td>
                                        <td style="width: 10%; font-size: small;" align="center">
                                            <b>Value</b>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" style="table-layout: fixed;" align="center" class="toosmall">
                                    <tr>
                                        <td style="width: 100%; vertical-align: top; height: 40px;" class="toosmall">
                                            <asp:DataList runat="server" ID="dtItemDetails" Width="100%" class="toosmall">
                                                <ItemTemplate>
                                                    <table width="100%" align="center" cellspacing="0px" style="table-layout: fixed;
                                                        border-bottom: 1px dashed black;" class="toosmall">
                                                        <tr>
                                                            <td style="width: 10%; font-size: small;" align="left">
                                                                <asp:Label runat="server" ID="lblGrade" Text='<%#Eval("Item") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 20%; font-size: small; font-family: 'Times New Roman';" align="center">
                                                                <asp:Label runat="server" ID="Label9" Text='<%#Eval("HSN") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 10%; font-size: small;" align="center">
                                                                <asp:Label runat="server" ID="lblQntl" Text='<%#Eval("Qntl") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 10%; font-size: small;" align="center">
                                                                <asp:Label runat="server" ID="lblPacking" Text='<%#Eval("Packing") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 10%; font-size: small;" align="center">
                                                                <asp:Label runat="server" ID="lblBags" Text='<%#Eval("Bags") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 10%; font-size: small;" align="center">
                                                                <asp:Label runat="server" ID="lblRate" Text='<%#Eval("Rate") %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 10%; font-size: small;" align="center">
                                                                <asp:Label runat="server" ID="lblvalue" Text='<%#Eval("Value") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" cellspacing="2" style="font-size: medium; table-layout: fixed;
                                    margin-top: -10px; border-bottom: 1px solid black; height: 130px;" class="toosmall">
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <b>Sub Total:</b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="lblSubTotal" Text='<%#Eval("Sub_Total") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <b>CGST %:</b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label16s" Text='<%#Eval("CGSTRate").ToString()=="0.00" || Eval("CGSTRate").ToString()=="0"?"":Eval("CGSTRate","{0}") %>'></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                                runat="server" ID="Label14" Text='<%#Eval("CGSTAmount").ToString()=="0.00" || Eval("CGSTAmount").ToString()=="0"?"":Eval("CGSTAmount","{0}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <b>SGST %: </b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label17" Text='<%#Eval("SGSTRate").ToString()=="0.00" || Eval("SGSTRate").ToString()=="0"?"":Eval("SGSTRate","{0}") %>'></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label runat="server" ID="Label15" Text='<%#Eval("SGSTAmount").ToString()=="0.00" || Eval("SGSTAmount").ToString()=="0"?"":Eval("SGSTAmount","{0}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <b>IGST %:</b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="Label18" Text='<%#Eval("IGSTRate").ToString()=="0.00" || Eval("IGSTRate").ToString()=="0"?"":Eval("IGSTRate","{0}") %>'></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label runat="server" ID="Label16as" Text='<%#Eval("IGSTAmount").ToString()=="0.00" || Eval("IGSTAmount").ToString()=="0"?"":Eval("IGSTAmount","{0}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <b>Frieght:</b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="lblLessFrieght" Text='<%#Eval("LESS_FRT_RATE").ToString()=="0.00" || Eval("LESS_FRT_RATE").ToString()=="0"?"":Eval("LESS_FRT_RATE","{0}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <b>Bank Commission:</b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="lblBankComm" Text='<%#Eval("Bank_Commission").ToString()=="0.00"?"":Eval("Bank_Commission","{0}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <asp:Label ID="Label29" Font-Bold="true" runat="server" Text='<%#Eval("CarporateLine") %>'
                                                Visible="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <b>Other Expense:</b>
                                        </td>
                                        <td style="width: 20%; font-size: small;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="lblOtherExpe" Text='<%#Eval("Other_Expenses").ToString()=="0.00"?"":Eval("Other_Expenses","{0}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; height: 20px;" align="right" class="toosmall">
                                            <b>Cash Advance:</b>
                                        </td>
                                        <td style="width: 20%;" align="right" class="toosmall">
                                            <asp:Label runat="server" ID="lblCashAdvance" Text='<%#Eval("Cash_Advance").ToString()=="0.00"?"":Eval("Cash_Advance","{0}") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%; font-size: small; border-bottom: 1px solid black; height: 30px;"
                                            align="right" class="toosmall">
                                            <b>Total Amount:</b>
                                        </td>
                                        <td style="width: 20%; font-size: small; border-bottom: 1px solid black;" align="right"
                                            class="toosmall">
                                            <asp:Label runat="server" ID="lblBillAmount" Text='<%#Eval("Bill_Amount") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" class="toosmall" style="font-size: small; height: 23px;">
                                            Rs.:<asp:Label runat="server" ID="lblInwords" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" class="toosmall" style="font-size: small; height: 10px;">
                                            <b>Please credit the above amount to your account and debit the same to our account</b>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center" cellspacing="0" style="table-layout: fixed; height: 80px;"
                                    class="toosmallforimg">
                                    <tr>
                                        <td align="left" class="toosmallforimg" style="font-size: small; height: 25px;">
                                            Our TIN No.:
                                            <asp:Label runat="server" ID="lblCmptinNo" Text="" Font-Bold="true"></asp:Label>&nbsp;&nbsp;FSSAI
                                            No:<asp:Label runat="server" ID="lblCompnayFSSAI_No" Text="" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 70%; font-size: small; height: 40px;" align="left" class="toosmallforimg">
                                            <b><u>Note:</u></b>&nbsp;<br />
                                            - After Dispatch of the goods we are not responsible for non delivery or any kind
                                            of damage.<br />
                                            - Certified that the particulars given above are true and correct.<br />
                                            - Please credit the amount in our account and send the amount by RTGS immediately.<br />
                                            - If the amount is not sent before the due date payment Interest 24% will be charged.<br />
                                            - I/We hereby certify that food/foods mentioned in this invoice is/are warranted
                                            to be of the nature and quality which it/these purports/purported to be
                                        </td>
                                        <td align="right" style="vertical-align: top; font-size: small;" class="toosmallforimg">
                                              <asp:Image runat="server" ID="imgSign" ImageUrl="~/Images/STAMP_GSTC.jpg" Width="10%"
                                                Height="20%" /><br />
                                            <asp:Label runat="server" ID="Label25" Font-Bold="true" CssClass="toosmall" Text="For,"></asp:Label>
                                            <asp:Label runat="server" ID="lblNameCmp" Font-Bold="true" CssClass="toosmall"></asp:Label><br />
                                            <asp:Label runat="server" ID="Label26" Font-Bold="true" CssClass="toosmall" Text="Authorised Signatory"></asp:Label>
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
