<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptRetailSellPrintForGST.aspx.cs"
    Inherits="Report_rptRetailSellPrintForGST" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Retail</title>
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
    <script type="text/javascript">
        function PrintPanel() {
            debugger;
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open();
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
    <script type="text/javascript">
        function PrintPanel2() {
            var panel = document.getElementById("<%=dtlist.ClientID %>");
            var printWindow = window.open('do.html', 'do', 'height=660,width=1350');
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
            <asp:Button ID="btnPrint" runat="server" Text="Print" Width="80px" OnClientClick="return PrintPanel();" />
            &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnPriPrinted" Text="Pre-Printed"
                OnClick="btnPriPrinted_Click" OnClientClick="return PrintPanel2();" />
            &nbsp;&nbsp;<asp:Button runat="server" ID="btnSendEmail" Text="Mail" OnClick="btnSendEmail_Click"
                Width="58px" />&nbsp;
        <asp:TextBox runat="server" ID="txtEmail" Width="300px"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Panel runat="server" ID="pnlMain" CssClass="print">
                <table width="100%" align="center" cellspacing="4" cellpadding="0" class="print">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblTime" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;">
                            <asp:DataList runat="server" ID="dtlist" Width="100%" OnItemDataBound="dtlist_OnItemDataBound">
                                <ItemTemplate>
                                    <table width="100%" style="table-layout: fixed; height: 125px;" class="print9pt">
                                        <tr>
                                            <td style="width: 20%; vertical-align: top;" align="center">
                                                <asp:Image runat="server" ID="imgLogo" ImageUrl="~/Sugar/Images/Logo.jpg" Width="100%"
                                                    Height="100%" />
                                            </td>
                                            <td style="width: 80%; vertical-align: top;" align="left">
                                                <table width="100%" style="table-layout: fixed;">
                                                    <tr>
                                                        <td align="left" style="width: 100%; height: 20; text-transform: uppercase; color: Red;"
                                                            class="print9pt">
                                                            <asp:Label ID="lblCompany" runat="server" Text="" Font-Bold="true" Font-Size="Xx-Large"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 100%; text-transform: uppercase; color: Red;" class="print9pt">
                                                            <asp:Label ID="Label8" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 100%; font-family: Verdana;" class="toosmall">
                                                            <asp:Label runat="server" ID="lblAl1" Text="396 A 4TH LANE BEHIND MAHESH BANK MARKET YARD GULTEKDI PUNE-37"
                                                                ForeColor="Blue"> </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 100%; font-family: Verdana;" class="toosmall">
                                                            <asp:Label runat="server" ID="lblAl2" Text="GST NO:-27AABHJ9303C1ZM TIN NO:-27661041850V FSSAI LIC NO:- 11516035000705"
                                                                ForeColor="Blue"> </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 100%; font-family: Verdana;" class="toosmall">
                                                            <asp:Label runat="server" ID="lblAl3" Text="LANDLINE:-020-24271689/8275879052 FAX NO:- 02024275689 MOB NO:-9403336789 centrex -4936"
                                                                ForeColor="Blue"> </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 100%; font-family: Verdana;" class="toosmall">
                                                            <asp:Label runat="server" ID="lblAl4" Text="MAIL ID- navkartraders89@yahoo.com" ForeColor="Blue"> </asp:Label>
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
                                    <table width="90%" cellpadding="3" cellspacing="5" style="table-layout: fixed; border-top: 2px solid black; border-bottom: 2px solid black;"
                                        class="print9pt">
                                        <tr>
                                            <td style="width: 60%; vertical-align: top;">
                                                <table style="width: 100%; vertical-align: top;" class="print9pt">
                                                    <tr>
                                                        <td>To,
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="print9pt">
                                                            <asp:Label ID="Label1" Text='<%#Eval("Party_Name") %>' Font-Bold="true" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="print9pt">
                                                            <asp:Label ID="lblPartyName" Text='<%#Eval("Party_Name") %>' Font-Bold="true" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="print9pt">
                                                            <asp:Label ID="Label7" Text='<%#Eval("Party_Name_New") %>' Font-Bold="true" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="print9pt">
                                                            <asp:Label ID="Label2" Text='<%#Eval("cityname") %>' Font-Bold="false" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="print9pt">
                                                            <asp:Label ID="Label3" Text='<%#Eval("partygst") %>' Font-Bold="false" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 40%;" class="print9pt">
                                                <table style="width: 80%;" class="print9pt">
                                                    <tr>
                                                        <td align="left" class="print9pt" style="visibility: hidden;">Our GST Number:
                                                        </td>
                                                        <td align="right" class="print9pt" style="visibility: hidden;">
                                                            <asp:Label Text='<%#Eval("CompanyGST") %>' runat="server" ID="Label27" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="print9pt">
                                                            <strong>
                                                                <asp:Label Text="" runat="server" ID="lblBillType" />
                                                                Tax Invoice :</strong>
                                                        </td>
                                                        <td align="right" class="print9pt">
                                                            <asp:Label Text='<%#Eval("billno") %>' runat="server" ID="lblBillNo" Font-Bold="true"
                                                                Font-Size="X-Large" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="print9pt">
                                                            <asp:Label Text="" runat="server" ID="Label4" />
                                                            Date:
                                                        </td>
                                                        <td align="right" class="print9pt">
                                                            <asp:Label Text='<%#Eval("doc_dateConverted") %>' runat="server" ID="Label5" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <%--
                                                    <td align="left" class="print9pt">
                                                        <asp:Label Text="" runat="server" ID="Label6" />
                                                        Due.Days:
                                                    </td>
                                                   <td align="right" class="print9pt">
                                                        <asp:Label Text='<%#Eval("duedays") %>' runat="server" ID="Label7" Font-Bold="true" />
                                                    </td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="print9pt">
                                                            <asp:Label Text="" runat="server" ID="Label8s" />
                                                            Veh.No:
                                                        </td>
                                                        <td align="right" class="print9pt">
                                                            <asp:Label Text='<%#Eval("lorry") %>' runat="server" ID="Label9" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="print9pt">
                                                            <asp:Label Text="" runat="server" ID="Label28" />
                                                            Narration:
                                                        </td>
                                                        <td align="right" class="print9pt">
                                                            <asp:Label Text='<%#Eval("Narration") %>' runat="server" ID="Label31" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%;" class="print9pt">
                                        <tr>
                                            <td>
                                                <table style="width: 100%; table-layout: fixed; border: 1px solid Black" class="print9pt">
                                                    <tr>
                                                        <td class="print9pt" style="width: 20%;">Item Description
                                                        </td>
                                                        <td class="print9pt" style="width: 10%;">HSN Code
                                                        </td>
                                                        <td class="print9pt" style="width: 20%;">Mill
                                                        </td>
                                                        <td class="print9pt" style="width: 20%;">Grade
                                                        </td>
                                                        <td class="print9pt" style="width: 10%;">Qty
                                                        </td>
                                                        <td class="print9pt" style="width: 10%;">GST Rate
                                                        </td>
                                                        <td class="print9pt" style="width: 10%;">Weight(kg)
                                                        </td>
                                                        <td class="print9pt" style="width: 10%;">Rate
                                                        </td>

                                                        <td class="print9pt" style="width: 10%;">Gross
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 90%;" class="print9pt">
                                                <asp:DataList runat="server" Width="100%" ID="dtlDetails" class="print9pt">
                                                    <ItemTemplate>
                                                        <table style="width: 100%; table-layout: fixed;" class="print9pt">
                                                            <tr>
                                                                <td class="print9pt" style="width: 20%;">
                                                                    <asp:Label ID="Label9" Text='<%#Eval("itemname") %>' runat="server" />
                                                                </td>
                                                                <td class="print9pt" style="width: 10%;">
                                                                    <asp:Label ID="Label14" Text='<%#Eval("HSN") %>' runat="server" />
                                                                </td>
                                                                <td class="print9pt" style="width: 20%;">
                                                                    <asp:Label ID="Label24" Text='<%#Eval("Mill_Name") %>' runat="server" />
                                                                </td>
                                                                <td class="print9pt" style="width: 20%;">
                                                                    <asp:Label ID="Label26" Text='<%#Eval("grade") %>' runat="server" />
                                                                </td>
                                                                <td class="print9pt" style="width: 10%; height: 20px">
                                                                    <strong>
                                                                        <asp:Label ID="Label10" Text='<%#Eval("qty") %>' runat="server" />
                                                                    </strong>
                                                                </td>
                                                                <td class="print9pt" style="width: 10%;">
                                                                    <asp:Label ID="Label12" Text='<%#Eval("GSTRate") %>' runat="server" />
                                                                </td>
                                                                <td class="print9pt" style="width: 10%;">
                                                                    <asp:Label ID="Label25" Text='<%#Eval("Kg") %>' runat="server" />
                                                                </td>
                                                                <td class="print9pt" style="width: 10%;">
                                                                    <asp:Label ID="Label11" Text='<%#Eval("rate") %>' runat="server" />
                                                                </td>
                                                                <%--  <td class="print9pt" style="width: 10%;">
                                                                <asp:Label ID="Label12" Text='<%#Eval("value") %>' runat="server" />
                                                            </td>--%>
                                                                <td class="print9pt" style="width: 10%;">
                                                                    <asp:Label ID="Label15" Text='<%#Eval("Gross") %>' runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%; table-layout: fixed; border: 2px solid Black; margin-top: 20px;"
                                                    class="print9pt">
                                                    <tr>
                                                        <td class="print9pt" style="width: 20%;"></td>
                                                        <td class="print9pt" style="width: 10%;"></td>
                                                        <td class="print9pt" style="width: 20%;"></td>
                                                        <td class="print9pt" style="width: 20%;"></td>
                                                        <td class="print9pt" style="width: 10%;">
                                                            <asp:Label Text="" ID="lblTotalQty" Font-Bold="true" runat="server" />
                                                        </td>
                                                        <td class="print9pt" style="width: 10%;"></td>
                                                        <td class="print9pt" style="width: 10%;"></td>
                                                        <td class="print9pt" style="width: 10%;">
                                                            <asp:Label Text="" ID="lblTotalValue" Font-Bold="true" runat="server" />
                                                        </td>
                                                        <td class="print9pt" style="width: 10%;">
                                                            <asp:Label Text="" ID="lblTotalGross" Font-Bold="true" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;" class="print9pt">
                                                    <tr>
                                                        <td style="width: 50%; vertical-align: top;" align="left" class="print9pt">Bill Amount In Words:
                                                        <asp:Label Text="" Font-Bold="true" ID="lblBillAmountInWords" runat="server" /><br />
                                                            <br />
                                                            <span style="vertical-align: bottom; bottom: 0; font-size: xx-large;">
                                                                <asp:Label Text='<%#Eval("isdel") %>' Font-Bold="true" Font-Size="Larger" runat="server" />&nbsp;&nbsp;
                                                            <asp:Label ID="Label6" Text='<%#Eval("cashnotrecive") %>' Font-Bold="true" runat="server"
                                                                Font-Size="Larger" />
                                                            </span>
                                                        </td>
                                                        <td style="width: 50%;" class="print9pt">
                                                            <table border="0" style="width: 100%;" class="print9pt">
                                                                <tr>
                                                                    <td class="print9pt">Taxable_Amount
                                                                    </td>
                                                                    <td></td>
                                                                    <td align="right" class="print9pt">
                                                                        <asp:Label runat="server" ID="Label21" Text='<%#Eval("Total").ToString()=="0.00" || Eval("Total").ToString()=="0"?"":Eval("Total","{0}") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="print9pt">CGST%:
                                                                    </td>
                                                                    <td align="right" class="print9pt">
                                                                        <asp:Label runat="server" ID="Label16" Text='<%#Eval("CGSTRate").ToString()=="0.00" || Eval("CGSTRate").ToString()=="0"?"":Eval("CGSTRate","{0}") %>'></asp:Label>
                                                                    </td>
                                                                    <td align="right" class="print9pt">
                                                                        <asp:Label runat="server" ID="Label13" Text='<%#Eval("CGSTAmount").ToString()=="0.00" || Eval("CGSTAmount").ToString()=="0"?"":Eval("CGSTAmount","{0}") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="print9pt">SGST%:
                                                                    </td>
                                                                    <td align="right" class="print9pt">
                                                                        <asp:Label runat="server" ID="Label17" Text='<%#Eval("SGSTRate").ToString()=="0.00" || Eval("SGSTRate").ToString()=="0"?"":Eval("SGSTRate","{0}") %>'></asp:Label>
                                                                    </td>
                                                                    <td align="right" class="print9pt">
                                                                        <asp:Label runat="server" ID="Label18" Text='<%#Eval("SGSTAmount").ToString()=="0.00" || Eval("SGSTAmount").ToString()=="0"?"":Eval("SGSTAmount","{0}") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="print9pt">IGST%:
                                                                    </td>
                                                                    <td align="right" class="print7pt">
                                                                        <asp:Label runat="server" ID="Label19" Text='<%#Eval("IGSTRate").ToString()=="0.00" || Eval("IGSTRate").ToString()=="0"?"":Eval("IGSTRate","{0}") %>'></asp:Label>
                                                                    </td>
                                                                    <td align="right" class="print7pt">
                                                                        <asp:Label runat="server" ID="Label20" Text='<%#Eval("IGSTAmount").ToString()=="0.00" || Eval("IGSTAmount").ToString()=="0"?"":Eval("IGSTAmount","{0}") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="print9pt">Off:
                                                                    </td>
                                                                    <td></td>
                                                                    <td align="right" class="print9pt">
                                                                        <asp:Label runat="server" Font-Bold="false" ID="Label23" Text='<%#Eval("Round_Off").ToString()=="0.00" || Eval("Round_Off").ToString()=="0"?"":Eval("Round_Off","{0}") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="print9pt">Hamali_Amount:
                                                                    </td>
                                                                    <td></td>
                                                                    <td align="right" class="print9pt">
                                                                        <asp:Label runat="server" Font-Bold="false" ID="Label30" Text='<%#Eval("HamaliAmount").ToString()=="0.00" || Eval("HamaliAmount").ToString()=="0"?"":Eval("HamaliAmount","{0}") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="print9pt">
                                                                        <strong>Bill_Amount: </strong>
                                                                    </td>
                                                                    <td></td>
                                                                    <td align="right" class="print9pt">
                                                                        <asp:Label runat="server" Font-Bold="true" Font-Size="X-Large" ID="Label22" Text='<%#Eval("Grand_Total").ToString()=="0.00" || Eval("Grand_Total").ToString()=="0"?"":Eval("Grand_Total","{0}") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                <td class="print9pt">
                                                                    TCS%:
                                                                </td>
                                                                <td align="right" class="print7pt">
                                                                    <asp:Label runat="server" ID="Label32" Text='<%#Eval("TCS_Rate").ToString()=="0.00" || Eval("TCS_Rate").ToString()=="0"?"":Eval("TCS_Rate","{0}") %>'></asp:Label>
                                                                </td>
                                                                <td align="right" class="print7pt">
                                                                    <asp:Label runat="server" ID="Label33" Text='<%#Eval("TCS_Amt").ToString()=="0.00" || Eval("TCS_Amt").ToString()=="0"?"":Eval("TCS_Amt","{0}") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="print9pt">
                                                                    <strong>Net Payable With TCS: </strong>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td align="right" class="print9pt">
                                                                    <asp:Label runat="server" Font-Bold="true" Font-Size="X-Large" ID="lblTCSNetPayable" Text='<%#Eval("TCS_Net_Payable").ToString()=="0.00" || Eval("TCS_Net_Payable").ToString()=="0"?"":Eval("TCS_Net_Payable","{0}") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="print9pt" align="left">
                                                            <asp:Label Text='<%#Eval("BrokerName") %>' Font-Bold="true" ID="Label29" runat="server" />
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 40%; border-top: 1px solid black;" align="right" colspan="2" class="print9pt">
                                                            <span style="float: left;">THIS IS COMPUTER GENERATED PRINT SO DOES NOT NEED SIGNATURE</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblbankdetails" Text="Bank Details: Navkar Traders Bank-State Bank Of India Branch-Paravati Area"
                                                                Font-Bold="true" runat="server" /><br />
                                                            <asp:Label Text=" Pune A/C No-61025337545 IFSC Code-SBIN0011782" runat="server" Font-Bold="true"
                                                                ID="lblbnk" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Image runat="server" ID="imgSign" Height="40px" Width="100px" CssClass="toosmall" /><br />
                                                            For,<asp:Label Text="" ID="lblCompanyName" Font-Bold="true" runat="server" />
                                                            <br />
                                                            Proprietor
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                    <td style="width: 40%;" align="right" colspan="2" class="print9pt">
                                                        For,<asp:Label Text="" ID="lblCompanyName" Font-Bold="true" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 40%;" align="right" colspan="2" class="print9pt">
                                                        Proprietor
                                                    </td>
                                                </tr>--%>
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
