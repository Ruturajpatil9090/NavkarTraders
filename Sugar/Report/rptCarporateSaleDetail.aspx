<%@ Page Title="rpt:Carporate Sell Detail" Language="C#" AutoEventWireup="true" CodeFile="rptCarporateSaleDetail.aspx.cs"
    Inherits="Report_rptCarporateSaleDetail" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../JS/emailValidation.js"></script>
    <script type="text/javascript" language="javascript">
        function memo() {
            window.open('../Sugar/pgeMotorMemo.aspx');    //R=Redirected  O=Original
        }
        function sugarpurchase(Ac_Code, Action) {
            window.open('../Inword/pgeSugarPurchaseForGSTxml.aspx?purchaseid=' + Ac_Code + '&Action=' + Action);    //R=Redirected  O=Original
        }
        function loadingvoucher() {
            window.open('../Sugar/pgeLoadingvoucher.aspx');    //R=Redirected  O=Original
        }
        function salebill(Ac_Code, Action) {
            window.open('../Outword/pgrSugarsaleForGSTxml.aspx?saleid=' + Ac_Code + '&Action=' + Action);    //R=Redirected  O=Original
        }
        function sp(accode, Action) {
            var tn;
            window.open('../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx?DO=' + accode + '&Action=' + Action);    //R=Redirected  O=Original
        }
    </script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlMain.ClientID %>");
            var printWindow = window.open('sugarStock.html', 'st', 'height=400,width=800');
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
            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" OnClientClick="return PrintPanel();"
                Width="80px" />
            &nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnExportToExcel" Text="Export To Excel"
                Width="120px" OnClick="btnExportToExcel_Click" />
            <asp:Button ID="btnPDFDownload" runat="server" Width="110px" Text="PDF Download"
                OnClick="btnPDfDownload_Click" />
            &nbsp;&nbsp;<asp:Button runat="server" ID="btnSendEmail" Text="Email" OnClick="btnSendEmail_Click"
                OnClientClick="CheckEmail();" Width="79px" />
            &nbsp;<asp:TextBox runat="server" ID="txtEmail" Width="300px" Height="23px"></asp:TextBox>
            <asp:Panel ID="pnlMain" runat="server" align="center" Font-Names="Calibri">
                <table id="Table2" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed">
                    <tr>
                        <td style="width: 100%;" align="center">
                            <asp:Label runat="server" ID="lblCompany" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" align="left">
                            <asp:Label runat="server" ID="Label6" Text="Carporate Sell Balance Stock Report"
                                Font-Bold="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; border-bottom: 1px double black;"></td>
                    </tr>
                </table>
                <table id="Table1" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed">
                    <tr>
                        <td style="width: 4%" align="center">
                            <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text="No"></asp:Label>
                        </td>
                        <td style="width: 3%" align="center">
                            <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Date"></asp:Label>
                        </td>
                        <td style="width: 7%" align="left">
                            <asp:Label runat="server" ID="Label2" Font-Bold="true" Text="Unit Name"></asp:Label>
                        </td>
                        <td style="width: 3%" align="center">
                            <asp:Label runat="server" ID="Label9" Font-Bold="true" Text="Sale Rate"></asp:Label>
                        </td>
                        <td style="width: 3%" align="center">
                            <asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Quintal"></asp:Label>
                        </td>
                        <td style="width: 2%" align="center">
                            <asp:Label runat="server" ID="Label4" Font-Bold="true" Text="Dispatch"></asp:Label>
                        </td>
                        <td style="width: 2%" align="center">
                            <asp:Label runat="server" ID="Label5" Font-Bold="true" Text="Balance"></asp:Label>
                        </td>
                        <td style="width: 5%" align="left">
                            <asp:Label runat="server" ID="Label7" Font-Bold="true" Text="PO Details"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table runat="server" width="100%" align="center" cellspacing="2">
                    <tr>
                        <td style="width: 100%; border-bottom: 1px double black;"></td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" align="center">
                            <asp:DataList runat="server" ID="dtlist" OnItemDataBound="dtlist_ItemDataBound" Width="100%">
                                <ItemTemplate>
                                    <table id="Table1" runat="server" width="100%" align="center" cellspacing="2" style="table-layout: fixed; background-color: #CCFFFF;">
                                        <tr>
                                            <td style="width: 4%" align="left">
                                                <asp:Label runat="server" ID="lblCSNo" Font-Bold="false" Visible="true" Text='<%#Eval("CSNO") %>'></asp:Label>
                                                <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text='<%#Eval("CSName") %>'></asp:Label>
                                            </td>
                                            <td style="width: 3%" align="center">
                                                <asp:Label runat="server" ID="Label1" Font-Bold="false" Text='<%#Eval("CSDate") %>'></asp:Label>
                                            </td>
                                            <td style="width: 7%" align="left">
                                                <asp:Label runat="server" ID="Label2" Font-Bold="false" Text='<%#Eval("CSUnitName") %>'></asp:Label>
                                            </td>
                                            <td style="width: 3%" align="center">
                                                <asp:Label runat="server" ID="Label9" Font-Bold="false" Text='<%#Eval("CSSaleRate") %>'></asp:Label>
                                            </td>
                                            <td style="width: 3%" align="center">
                                                <asp:Label runat="server" ID="Label3" Font-Bold="false" Text='<%#Eval("CSQntl") %>'></asp:Label>
                                            </td>
                                            <td style="width: 2%" align="center">
                                                <asp:Label runat="server" ID="Label4" Font-Bold="false" Text='<%#Eval("CSDesp") %>'></asp:Label>
                                            </td>
                                            <td style="width: 2%" align="center">
                                                <asp:Label runat="server" ID="Label5" Font-Bold="false" Text='<%#Eval("CSBalance") %>'></asp:Label>
                                            </td>
                                            <td style="width: 5%" align="left">
                                                <asp:Label runat="server" ID="Label7" Font-Bold="false" Text='<%#Eval("CSPodetails") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="Table3" runat="server" width="100%" style="table-layout: fixed;">
                                        <tr>
                                            <td style="width: 1%" align="center">
                                                <asp:Label runat="server" ID="Label8" Font-Bold="false" Text="D.O.#"></asp:Label>
                                            </td>
                                            <td style="width: 2%" align="center">
                                                <asp:Label runat="server" ID="Label10" Font-Bold="false" Text="Date"></asp:Label>
                                            </td>
                                            <td style="width: 2%" align="center">
                                                <asp:Label runat="server" ID="Label11" Font-Bold="false" Text="Desp"></asp:Label>
                                            </td>
                                            <td style="width: 3%" align="left">
                                                <asp:Label runat="server" ID="Label12" Font-Bold="false" Text="Mill"></asp:Label>
                                            </td>
                                            <td style="width: 3%" align="left">
                                                <asp:Label runat="server" ID="Label19" Font-Bold="false" Text="Ship to"></asp:Label>
                                            </td>
                                            <td style="width: 3%" align="center">
                                                <asp:Label runat="server" ID="Label13" Font-Bold="false" Text="Veh.No"></asp:Label>
                                            </td>
                                            <td style="width: 2%" align="center">
                                                <asp:Label runat="server" ID="Label14" Font-Bold="false" Text="Frt+Vasuli"></asp:Label>
                                            </td>
                                            <td style="width: 2%" align="center">
                                                <asp:Label runat="server" ID="Label15" Font-Bold="false" Text="Transport"></asp:Label>
                                            </td>

                                            <td style="width: 6%" align="left">
                                                <asp:Label runat="server" ID="Label17" Font-Bold="false" Text="Getpass"></asp:Label>
                                            </td>
                                            <td style="width: 2%" align="left">
                                                <asp:Label runat="server" ID="Label21" Font-Bold="false" Text="PS"></asp:Label>
                                            </td>
                                            <td style="width: 2%" align="left">
                                                <asp:Label runat="server" ID="Label22" Font-Bold="false" Text="SB"></asp:Label>
                                            </td>
                                            <td style="width: 2%" align="left">
                                                <asp:Label runat="server" ID="Label18" Font-Bold="false" Text="ASN_No"></asp:Label>
                                            </td>
                                            <%-- <td style="width: 2%" align="left">
                                            <asp:Label runat="server" ID="Label23" Font-Bold="false" Text="MM"></asp:Label>
                                        </td>--%>
                                        </tr>
                                    </table>
                                    <table id="Table4" runat="server" width="100%">
                                        <tr>
                                            <td style="width: 100%; border-bottom: 1px double black;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%;" align="left">
                                                <asp:DataList runat="server" ID="dtlDetails" Width="100%">
                                                    <ItemTemplate>
                                                        <table id="Table3" runat="server" width="100%" style="table-layout: fixed; background-color: #FFFFCC; border-bottom: 1px dashed black;">
                                                            <tr>
                                                                <td style="width: 1%" align="center">
                                                                    <%-- <asp:Label runat="server" ID="Label8" Font-Bold="false" Text='<%#Eval("dispatchno") %>'></asp:Label>--%>

                                                                    <asp:LinkButton Style="text-decoration: none;" runat="server" ID="lnkDO" Text='<%#Eval("dispatchno") %>'
                                                                        OnClick="lnkDO_Click"></asp:LinkButton>
                                                                </td>
                                                                <td style="width: 2%" align="center">
                                                                    <asp:Label runat="server" ID="Label10" Font-Bold="false" Text='<%#Eval("DODate") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 2%" align="center">
                                                                    <asp:Label runat="server" ID="Label11" Font-Bold="false" Text='<%#Eval("DODesp") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="left">
                                                                    <asp:Label runat="server" ID="Label12" Font-Bold="false" Text='<%#Eval("DOMill") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="left">
                                                                    <asp:Label runat="server" ID="Label20" Font-Bold="false" Text='<%#Eval("shiptoshortname") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 3%" align="center">
                                                                    <asp:Label runat="server" ID="Label13" Font-Bold="false" Text='<%#Eval("DOLorryNo") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 2%" align="center">
                                                                    <asp:Label runat="server" ID="Label14" Font-Bold="false" Text='<%#Eval("Addition") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 2%; word-wrap: break-word; line-break: normal;" align="center">
                                                                    <asp:Label runat="server" ID="Label15" Font-Bold="false" Text='<%#Eval("DOTransport") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 6%" align="left">
                                                                    <asp:Label runat="server" ID="Label17" Font-Bold="false" Text='<%#Eval("DOGetpass") %>'></asp:Label>
                                                                </td>
                                                                <td style="width: 2%" align="left">
                                                                    <asp:LinkButton runat="server" ID="lnkPS" Text='<%#Eval("PS") %>' OnClick="lnkPS_Click"></asp:LinkButton>
                                                                </td>
                                                                <td style="width: 2%" align="left">
                                                                    <asp:LinkButton runat="server" ID="lnkSB" Text='<%#Eval("SB") %>' OnClick="lnkSB_Click"></asp:LinkButton>
                                                                </td>
                                                                <td style="width: 2%" align="left">
                                                                    <asp:Label runat="server" ID="Label16" Font-Bold="false" Text='<%#Eval("ASN_No") %>'></asp:Label>
                                                                   <%-- <asp:LinkButton runat="server" ID="lnkOV" Text='<%#Eval("VO") %>' OnClick="lnkOV_Click"></asp:LinkButton>--%>
                                                                </td>
                                                                <%-- <td style="width: 2%;visibility:hidden;" align="left">
                                                                <asp:LinkButton runat="server" ID="lnkMM" Text='<%#Eval("MM") %>' OnClick="lnkMM_Click" Visible="false"></asp:LinkButton>
                                                            </td>--%>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 15px; border-top: 1px double black;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; border-bottom: 1px double black;"></td>
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
