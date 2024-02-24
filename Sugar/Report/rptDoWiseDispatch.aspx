<%@ Page Title="rpt:DO Wise Dispatch" Language="C#" AutoEventWireup="true" CodeFile="rptDoWiseDispatch.aspx.cs"
    Inherits="Report_rptDoWiseDispatch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DO Wise Dispatch</title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('', '', 'height=660,width=1350');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body style="font-family:Calibri;font-size:12px;width:1100px; text-align:center;">');
            printWindow.document.write('<style type = "text/css">thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>

    <script type="text/javascript" src="../JS/emailValidation.js"></script>
    <script type="text/javascript" language="javascript">
//        function memo() {
//            window.open('../Sugar/pgeMotorMemo.aspx');    //R=Redirected  O=Original
//        }
//        function sugarpurchase() {
//            window.open('../Sugar/pgeSugarPurchase.aspx');    //R=Redirected  O=Original
//        }
//        function loadingvoucher() {
//            window.open('../Sugar/pgeLoadingvoucher.aspx');    //R=Redirected  O=Original
        //        }

        function Donew(DO,Action) {
            window.open('../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx?DO=' + DO + '&Action=' + Action);    //R=Redirected  O=Original
        }
        function salebill() {
            window.open('../Sugar/pgeSugarsale.aspx');    //R=Redirected  O=Original
        }

        function salebillnew(saleid,Action) {
            window.open('../Outword/pgrSugarsaleForGSTxml.aspx?saleid=' + saleid + '&Action=' + Action);    //R=Redirected  O=Original
        }

        function DO() {
            window.open('../Sugar/pgeDeliveryOrder2.aspx');    //R=Redirected  O=Original
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
            Width="80px" />
        &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
            Width="120px" OnClick="btnExportToExcel_Click" />
        &nbsp;&nbsp;<asp:Button runat="server" ID="btnSendEmail" Text="Email" OnClick="btnSendEmail_Click"
            OnClientClick="CheckEmail();" Width="79px" />
        &nbsp;<asp:TextBox runat="server" ID="txtEmail" Width="300px" Height="23px"></asp:TextBox>
    </div>
    <div>
        <asp:Panel runat="server" ID="pnlMain">
            <asp:Label ID="lblCompanyName" Width="90%" runat="server" Text="" Style="text-align: center;"
                CssClass="lblName" Font-Size="16px" Font-Bold="true"></asp:Label>
            <br />
            <table width="120%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblfromtodate" Font-Bold="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border-bottom: double 2px black;">
                    </td>
                </tr>
            </table>
            <table width="120%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                font-size: 10pt;">
                <tr>
                    <td align="center" style="width: 2%;">
                        <asp:Label ID="lblNo" runat="server" Text="doc_no" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width:3%;">
                        <asp:Label ID="lblDispatchDate" runat="server" Text="Date" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="lblMill" runat="server" Text="MILL" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="lblGrade" runat="server" Text="Vouc By" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="lblNetQntl" runat="server" Text="Get Pass" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 4%;">
                        <asp:Label ID="Label8" runat="server" Text="Bill To" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="Label9" runat="server" Text="Bill No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="Label10" runat="server" Text="Driver No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="lblMillRate" runat="server" Text="Mill Rt" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="lblQnty" runat="server" Text="Qntl" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="lblDispatched" runat="server" Text="Sell Rate" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 2%;">
                        <asp:Label ID="Label18" runat="server" Text="Comm." Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="lblBalLeft" runat="server" Text="Lorry No" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="lblLiftingDate" runat="server" Text="Frt" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 2%;">
                        <asp:Label ID="Label6" runat="server" Text="Memo" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="center" style="width: 3%;">
                        <asp:Label ID="lblDO" runat="server" Text="Vasuli" Font-Bold="true"></asp:Label>
                    </td>
                  <%--  <td align="center" style="width: 5%;">
                        <asp:Label ID="Label6" runat="server" Text="Vasuli Ac" Font-Bold="true"></asp:Label>
                    </td>--%>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label1" runat="server" Text="Transport" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 5%;">
                        <asp:Label ID="Label2" runat="server" Text="DO" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 2%;">
                        <asp:Label ID="Label3" runat="server" Text="Tender" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" style="width: 1%;">
                        <asp:Label ID="s" runat="server" Text="SMS" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="120%" align="center" cellpadding="1" cellspacing="0">
                <tr>
                    <td style="border-bottom: double 2px black;">
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%;">
                        <asp:DataList runat="server" ID="Datalist1" Width="100%" OnItemDataBound="Datalist1_OnItemDataBound">
                            <ItemTemplate>
                                <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                                    font-size: 10pt;">
                                    <tr>
                                        <td align="center" style="width: 2%;">
                                            <asp:LinkButton Style="text-decoration: none;" runat="server" ID="lnkDO" Text='<%#Eval("#") %>'
                                                OnClick="lnkDO_Click"></asp:LinkButton>
                                        </td>
                                        <%--<td align="center" style="width: 2%;">
                                            <asp:Label ID="lblNos" runat="server" Text='<%#Eval("#") %>' Font-Bold="false"></asp:Label>
                                        </td>--%>
                                        <td align="left" style="width: 3%;">
                                            <asp:Label ID="lblDispatchDates" runat="server" Text='<%#Eval("dodate") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 4%;">
                                            <asp:Label ID="lblMills" runat="server" Text='<%#Eval("millShortName") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 5%;">
                                            <asp:Label ID="lblGrades" runat="server" Text='<%#Eval("VocBy") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 4%;">
                                            <asp:Label ID="lblNetQntls" runat="server" Text='<%#Eval("GetPass") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 5%;">
                                            <asp:Label ID="Label11" runat="server" Text='<%#Eval("BillToName") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 2%;">
                                         <asp:LinkButton Style="text-decoration: none;" runat="server" ID="lnkSB" Text='<%#Eval("SB_No") %>'
                                                                    OnClick="lnkSB_Click"></asp:LinkButton>
                                           <%-- <asp:Label ID="Label12" runat="server" Text='<%#Eval("SB_No") %>' Font-Bold="false"></asp:Label>--%>
                                        </td>
                                        <td align="left" style="width: 2%;">
                                            <asp:Label ID="Label13" runat="server" Text='<%#Eval("driver_no") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 3%;">
                                            <asp:Label ID="lblMillRates" runat="server" Text='<%#Eval("MR") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 3%;">
                                            <asp:Label ID="lblQnty" runat="server" Text='<%#Eval("Qntl") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 3%;">
                                            <asp:Label ID="lblDispatcheds" runat="server" Text='<%#Eval("SR") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 2%;">
                                            <asp:Label ID="Label17" runat="server" Text='<%#Eval("Tender_Commission") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 2%;">
                                            <asp:Label ID="lblBalLefts" runat="server" Text='<%#Eval("lorry") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 3%;">
                                            <asp:Label ID="lblLiftingDates" runat="server" Text='<%#Eval("frt") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                         <td align="center" style="width: 2%;">
                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("MM_Rate") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 3%;">
                                            <asp:Label ID="lblDOs" runat="server" Text='<%#Eval("vasuli") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                       <%-- <td align="left" style="width: 5%; overflow: hidden;">
                                            <asp:Label ID="Label7" runat="server" Text='<%#Eval("Vasuli_Ac_Name") %>' Font-Bold="false"></asp:Label>
                                        </td>--%>
                                        <td align="left" style="width: 5%; overflow: hidden;">
                                            <asp:Label ID="Label1s" runat="server" Text='<%#Eval("transport") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 5%;">
                                            <asp:Label ID="Label2s" runat="server" Text='<%#Eval("do") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 2%;">
                                            <asp:Label ID="Label3s" runat="server" Text='<%#Eval("tender") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 1%;">
                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("LoadingSms") %>' Font-Bold="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                        <td style="border-bottom: double 2px black;">
                    </td>
                </tr>
                <tr>
                    <td style="border-bottom: double 2px black;">
                        <table width="100%">
                            <tr>
                                <td style="width: 100%;">
                                    <table width="100%" align="center" cellpadding="1" cellspacing="0" style="table-layout: fixed;
                                        font-size: 13pt;">
                                        <tr>
                                            <td align="center" style="width: 2%;">
                                                <asp:Label ID="lblNoa" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 2%;">
                                                <asp:Label ID="lblDispatchDatea" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 5%;">
                                                <asp:Label ID="lblMilla" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 5%;">
                                                <asp:Label ID="lblGradea" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 9%;">
                                                <asp:Label ID="lblNetQntla" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 5%;">
                                                <asp:Label ID="Label14" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 2%;">
                                                <asp:Label ID="Label15" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 4%;">
                                                <asp:Label ID="Label16" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 2%;">
                                                <asp:Label ID="lblMillRateaa" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 8%;">
                                                <asp:Label ID="lblQntlTotala" runat="server" Text="Qntl" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 3%;">
                                                <asp:Label ID="lblDispatcheda" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 2%;">
                                                <asp:Label ID="lblBalLefta" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                              <td align="center" style="width: 2%;"> 
                                            </td>
                                            </td>
                                              <td align="center" style="width: 3%;"> 
                                            </td></td>
                                              <td align="center" style="width: 2%;"> 
                                            </td>
                                               <td align="left" style="width: 0%;"> 
                                            </td>
                                            <td align="center" style="width: 7%;">
                                                <asp:Label ID="lblLiftingDates" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                           
                                           
                                            <td align="center" style="width: 2%;">
                                                <asp:Label ID="lblTotalVasuliAmount" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 11%;">
                                                <asp:Label ID="Label1a" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 5%;">
                                                <asp:Label ID="Label2a" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 2%;">
                                                <asp:Label ID="Label3a" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 1%;">
                                                <asp:Label ID="Label5" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-bottom: double 2px black;">
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
