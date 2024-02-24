<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeAccountBill.aspx.cs" Inherits="Sugar_pgeAccountBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function SB(Ac_Code, FromBill, ToBill) {

            window.open('../Report/rptJawakmultipleSalebill.aspx?Ac_Code=' + Ac_Code + '&FromBill=' + FromBill + '&ToBill=' + ToBill);
        }
        function SB1(FromBill, ToBill) {

            window.open('../Report/rptJawakmultipleSalebill.aspx?FromBill=' + FromBill + '&ToBill=' + ToBill);
        }

        function SBD(Ac_Code, FromBill, ToBill) {

            window.open('../Report/rptJaggerryDetailSalebill.aspx?Ac_Code=' + Ac_Code + '&FromBill=' + FromBill + '&ToBill=' + ToBill);
        }
        function SBD1(FromBill, ToBill) {

            window.open('../Report/rptJaggerryDetailSalebill.aspx?FromBill=' + FromBill + '&ToBill=' + ToBill);
        }


        function Stockbook(Fromdate, Todate) {
            window.open('../Report/rptJaggeryStockBook.aspx?Fromdate=' + Fromdate + '&Todate=' + Todate);
        }

        function SM(FromBill, ToBill) {
            window.open('../Report/rptMultipleBillPrint.aspx?FromBill=' + FromBill + '&ToBill=' + ToBill);
        }
        function PS(Fromdate, Todate, tdsrate) {
            window.open('../Report/rptPurchaseStock.aspx?Fromdate=' + Fromdate + '&Todate=' + Todate + '&tdsrate=' + tdsrate);
        }

        function PSGST(Fromdate, Todate, tdsrate) {
            window.open('../Report/rptPurchaseStockGst.aspx?Fromdate=' + Fromdate + '&Todate=' + Todate + '&tdsrate=' + tdsrate);
        }



        function MSOMS(Fromdate, Todate) {
            window.open('../Report/jaggerySaleRegiMSOMS.aspx?Fromdate=' + Fromdate + '&Todate=' + Todate);
        }
        function Bnetwt(Fromdate, Todate) {
            window.open('../Report/rptJaggeryBalanceNetWt.aspx?Fromdate=' + Fromdate + '&Todate=' + Todate);
        }

        function PB() {
            //window.open('../Report/rptPendingBills.aspx?Fromdate=' + Fromdate + '&Todate=' + Todate);
            window.open('../Report/rptPendingBills.aspx?');

        }

        function BillCheck(FromBill, ToBill) {
            window.open('../Report/rptBillWiseCustomer.aspx?FromBill=' + FromBill + '&ToBill=' + ToBill);
        }
        function tdsreport(Fromdate, Todate, tdsrate) {
            window.open('../Report/rptTDSReport.aspx?Fromdate=' + Fromdate + '&Todate=' + Todate + '&tdsrate=' + tdsrate);
        }

        function marketses(Fromdate, Todate) {
            window.open('../Report/rptMarketSesReport.aspx?Fromdate=' + Fromdate + '&Todate=' + Todate);
        }

        function jawaksb(Fromdate, Todate) {
            window.open('../Report/rptSingleJawakSaleBill.aspx?Fromdate=' + Fromdate + '&Todate=' + Todate);
        }
        function jawaksbqty(Fromdate, Todate) {
            window.open('../Report/rptSaleregisterWithQty.aspx?Fromdate=' + Fromdate + '&Todate=' + Todate);
        }
        function Loss(Fromdate, Todate) {
            window.open('../Report/rptJaggerySaudaLoss.aspx?Fromdate=' + Fromdate + '&Todate=' + Todate);
        }
        function Profit(Fromdate, Todate) {
            window.open('../Report/rptJaggerySaudaProfit.aspx?Fromdate=' + Fromdate + '&Todate=' + Todate);
        }
        function PRTDS(Fromdate, Todate, tdsrate) {
            window.open('../Report/rptPurchaseRegisterTDS.aspx?Fromdate=' + Fromdate + '&Todate=' + Todate + '&tdsrate=' + tdsrate);
        }
        function PRWTDS(fromDT, toDT, Ac_Code) {

            window.open('../Report/rptPurchaseRegisterWithoutTDS.aspx?Fromdate=' + fromDT + '&Todate=' + toDT + '&Ac_Code=' + Ac_Code);
        }
    
    </script>
    <script type="text/javascript" language="javascript">

        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;


        function SelectSibling(e) {
            var e = e ? e : window.event;
            var KeyCode = e.which ? e.which : e.keyCode;

            if (KeyCode == 40) {
                SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);
            }
            else if (KeyCode == 38) {
                SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);
            }

            else if (KeyCode == 13) {

                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;

                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";

                document.getElementById("<%=txtSearchText.ClientID %>").value = "";

                var grid = document.getElementById("<%= grdPopup.ClientID %>");

                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
                var pageCount = document.getElementById("<%= hdHelpPageCount.ClientID %>").value;


                pageCount = parseInt(pageCount);
                if (pageCount > 1) {
                    SelectedRowIndex = SelectedRowIndex + 1;
                }
                if (hdnfClosePopupValue == "txtAcCode") {
                    document.getElementById("<%= txtAcCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblAcCodeName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                }

                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }
        }
        function SelectRow(CurrentRow, RowIndex) {
            UpperBound = parseInt('<%= this.grdPopup.Rows.Count %>') - 1;
            LowerBound = 0;

            if (SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound)

                if (SelectedRow != null) {
                    SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
                    SelectedRow.style.color = SelectedRow.originalForeColor;
                }
            if (CurrentRow != null) {
                CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
                CurrentRow.originalForeColor = CurrentRow.style.color;
                CurrentRow.style.backgroundColor = '#DCFC5C';
                CurrentRow.style.color = 'Black';
            }
            SelectedRow = CurrentRow;
            SelectedRowIndex = RowIndex;
            setTimeout("SelectedRow.focus();", 0);
        }
    </script>
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <style type="text/css">
        .style2
        {
            width: 23%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="  Multiple Bill Printing   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:HiddenField ID="hdnfClosePopup" runat="server" />
    <asp:HiddenField ID="hdHelpPageCount" runat="server" />
    <table width="60%" align="center">
        <tr>
            <td align="left" style="width: 5%;">
                Account Code:
            </td>
            <td align="left" colspan="2" class="style2">
                <asp:TextBox ID="txtAcCode" runat="server" Width="80px" CssClass="txt" AutoPostBack="True"
                    OnTextChanged="txtAcCode_TextChanged" Height="24px" TabIndex="1"></asp:TextBox>
                <asp:Button ID="btnAcCode" runat="server" Text="..." CssClass="btnHelp" OnClick="btnAcCode_Click"
                    Height="24px" Width="20px" />
                <asp:Label ID="lblAcCodeName" runat="server" CssClass="lblName"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvtxtAcCode" runat="server" ControlToValidate="txtAcCode"
                    CssClass="validator" Display="Dynamic" Enabled="false" ErrorMessage="Required"
                    SetFocusOnError="true" Text="Required" ValidationGroup="add"> </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 5%;">
                From Bill No:
            </td>
            <td align="left" colspan="2" class="style2">
                <asp:TextBox ID="txtFromBill_No" runat="server" Width="80px" CssClass="txt" Height="24px"
                    TabIndex="2"></asp:TextBox>
                <%-- <asp:Button ID="btnFromBill_No" runat="server" Text="..." CssClass="btnHelp" OnClick="btnFromBill_No_Click"
                    Height="24px" Width="20px" />--%>
                <asp:Label ID="lblFromBill_No" runat="server" CssClass="lblName"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvtxtFromBill_No" runat="server" ControlToValidate="txtFromBill_No"
                    CssClass="validator" Display="Dynamic" Enabled="false" ErrorMessage="Required"
                    SetFocusOnError="true" Text="Required" ValidationGroup="add"> </asp:RequiredFieldValidator>
                To Bill No:
                <asp:TextBox ID="txtToBill_No" runat="server" Width="80px" CssClass="txt" Height="24px"
                    TabIndex="3"></asp:TextBox>
                <%--  <asp:Button ID="btnToBill_No" runat="server" Text="..." CssClass="btnHelp" OnClick="btnToBill_No_Click"
                    Height="24px" Width="20px" />--%>
                <asp:Label ID="lblTOBill_No" runat="server" CssClass="lblName"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvtxtToBill_No" runat="server" ControlToValidate="txtToBill_No"
                    CssClass="validator" Display="Dynamic" Enabled="false" ErrorMessage="Required"
                    SetFocusOnError="true" Text="Required" ValidationGroup="add"> </asp:RequiredFieldValidator>
                TDS Rate:
                <asp:TextBox ID="txttds_rate" runat="server" Width="80px" CssClass="txt" Height="24px"
                    TabIndex="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                From Date:
            </td>
            <td align="left">
                <asp:TextBox ID="txtFromDt" runat="server" Width="80px" CssClass="txt" Height="24px"></asp:TextBox>
                <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                    Width="25px" Height="15px" />
                <ajax1:CalendarExtender ID="calenderExtendertxtFromDt" runat="server" TargetControlID="txtFromDt"
                    PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
            </td>
            <td align="left">
                To Date:
            </td>
            <td align="left">
                <asp:TextBox ID="txtToDt" runat="server" Width="80px" CssClass="txt" Height="24px"></asp:TextBox>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                    Height="15px" />
                <ajax1:CalendarExtender ID="CalendarExtendertxtToDt" runat="server" TargetControlID="txtToDt"
                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="1">
                <%--<asp:Button ID="btnMultiple_Bill" runat="server" Text="Bill Details Report" CssClass="btnHelp"
                    Width="120px" ValidationGroup="save" OnClick="btnMultiple_Bill_Click" Height="50px"
                    TabIndex="4" />--%>
                <asp:Button ID="btndetailsalebillreport" runat="server" Text="Bill Details Report"
                    CssClass="btnHelp" Width="200px" ValidationGroup="save" OnClick="btndetailsalebillreport_Click"
                    Height="50px" TabIndex="4" />
            </td>
            <td align="center" colspan="1">
                <asp:Button ID="btnstock_Book" runat="server" Text="Stock Book" CssClass="btnHelp"
                    Width="200px" ValidationGroup="save" OnClick="btnstock_Book_Click" Height="50px"
                    TabIndex="4" />
            </td>
            <td colspan="1">
                <asp:Button ID="btnAllBill" runat="server" Text="Multiple Bill Print" CssClass="btnHelp"
                    Width="200px" ValidationGroup="save" OnClick="btnAllBill_Click" Height="50px"
                    TabIndex="4" />
            </td>
            <td colspan="1">
                <asp:Button ID="btnpurchasestock" runat="server" Text="Purchase Register" CssClass="btnHelp"
                    Width="200px" ValidationGroup="save" OnClick="btnpurchasestock_Click" Height="50px"
                    TabIndex="4" />
            </td>
              <td colspan="1">
                <asp:Button ID="btnpurchasestockGst" runat="server" Text="Purchase Register GST" CssClass="btnHelp"
                    Width="200px" ValidationGroup="save" OnClick="btnpurchasestockGst_Click" Height="50px"
                    TabIndex="4" />
            </td>
        </tr>
        <tr>
            <td colspan="1">
                <asp:Button ID="btnPendingBill" runat="server" Text="Pending Bills" CssClass="btnHelp"
                    Width="200px" ValidationGroup="save" OnClick="btnPendingBill_Click" Height="50px"
                    TabIndex="4" />
            </td>
            <td colspan="1">
                <asp:Button ID="btnBillWiseCustomer" runat="server" Text="Bills and Customer" CssClass="btnHelp"
                    Width="200px" ValidationGroup="save" OnClick="btnBillWiseCustomer_Click" Height="50px"
                    TabIndex="4" />
            </td>
            <td colspan="1">
                <asp:Button ID="btntds" runat="server" Text="TDS Report" CssClass="btnHelp" Width="200px"
                    ValidationGroup="save" OnClick="btntds_Click" Height="50px" TabIndex="4" />
            </td>
            <td>
                <asp:Button ID="btnmarketses" runat="server" Text="Market Ses Report" CssClass="btnHelp"
                    Width="200px" ValidationGroup="save" OnClick="btnmarketses_Click" Height="50px"
                    TabIndex="4" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="1">
                <asp:Button ID="btnjawaksalebill" runat="server" Text="Jaggary Sale Register" CssClass="btnHelp"
                    Width="200px" ValidationGroup="save" OnClick="btnjawaksalebill_Click" Height="50px"
                    TabIndex="4" />
            </td>
            <td align="center" colspan="1">
                <asp:Button ID="btnjaggeryBalNetWt" runat="server" Text="Closing Stock Jaggary" CssClass="btnHelp"
                    Width="200px" ValidationGroup="save" OnClick="btnjaggeryBalNetWt_Click" Height="50px"
                    TabIndex="4" />
            </td>
            <td align="center" colspan="1">
                <asp:Button ID="btnsaleregisterMSOMS" runat="server" Text="Jaggary Sale Register MS/OMS"
                    CssClass="btnHelp" Width="200px" ValidationGroup="save" OnClick="btnsaleregisterMSOMS_Click"
                    Height="50px" TabIndex="4" />
            </td>
            <td align="left" colspan="1">
                <asp:Button ID="btnJaggrySaleRegisterWithQty" runat="server" Text="Jaggary Sale Register with Qty"
                    CssClass="btnHelp" Width="200px" ValidationGroup="save" OnClick="btnJaggrySaleRegisterWithQty_Click"
                    Height="50px" TabIndex="4" />
            </td>
        </tr>
        <tr>
            <td align="left" colspan="1">
                <asp:Button ID="btnLoss" runat="server" Text="Jaggary Sauda Loss" CssClass="btnHelp"
                    Width="200px" ValidationGroup="save" OnClick="btnLoss_Click" Height="50px" TabIndex="4" />
            </td>
            <td align="left" colspan="1">
                <asp:Button ID="btnProfit" runat="server" Text="Jaggary Sauda Profit" CssClass="btnHelp"
                    Width="200px" ValidationGroup="save" OnClick="btnProfit_Click" Height="50px"
                    TabIndex="4" />
            </td>
              <td align="left" colspan="1">
                <asp:Button ID="btnPurchaseRegisterTDS" runat="server" Text="Purchase Register TDS" CssClass="btnHelp"
                    Width="200px" ValidationGroup="save" OnClick="btnpurchaseRegisterTDS_Click" Height="50px"
                    TabIndex="4" />
            </td>
             <td align="left" colspan="1">
                <asp:Button ID="Button1" runat="server" Text="Purchase Register Without TDS" CssClass="btnHelp"
                    Width="200px" ValidationGroup="save" OnClick="btnpurchaseRegisterWithoutTDS_Click" Height="50px"
                    TabIndex="4" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%"
        align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
        Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px;
        min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center;
        left: 10%; top: 10%;">
        <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg"
            Width="20px" Height="20px" Style="float: right; vertical-align: top;" OnClick="imgBtnClose_Click"
            ToolTip="Close" />
        <table width="95%">
            <tr>
                <td align="center" style="background-color: #F5B540; width: 100%;">
                    <asp:Label ID="lblPopupHead" runat="server" Font-Size="Medium" Font-Names="verdana"
                        Font-Bold="true" ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Search Text:
                    <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                        Width="250px" Height="20px" AutoPostBack="true"></asp:TextBox>
                    <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                        CssClass="btnSubmit" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:Panel ID="pnlScroll" runat="server" Width="680px" ScrollBars="Both" Direction="LeftToRight" BackColor="#FFFFE4" style="z-index:5000 ;  float:right; max-height:380px; height:380px;">--%>
                    <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                        Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                        <asp:GridView ID="grdPopup" CssClass="select" runat="server" AutoGenerateColumns="true"
                            EmptyDataText="No Records Found" ViewStateMode="Disabled" PageSize="20" AllowPaging="true"
                            HeaderStyle-BackColor="#6D8980" HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated"
                            OnPageIndexChanging="grdPopup_PageIndexChanging" Style="table-layout: fixed;"
                            OnRowDataBound="grdPopup_RowDataBound">
                            <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                            <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                            <RowStyle Height="25px" ForeColor="Black" Wrap="false" />
                            <PagerStyle BackColor="Tomato" ForeColor="White" Width="100%" Font-Bold="true" />
                            <PagerSettings Position="TopAndBottom" />
                        </asp:GridView>
                    </asp:Panel>
                    <%--</asp:Panel>--%>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
