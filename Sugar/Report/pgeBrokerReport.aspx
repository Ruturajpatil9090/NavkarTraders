<%@ Page Title="Broker Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeBrokerReport.aspx.cs" Inherits="Report_pgeBrokerReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <link href="../CSS/tooltip.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/tooltip.js">
    </script>
    <script type="text/javascript" src="../JS/select all.js"></script>
    <script type="text/javascript">
        function br(Broker_Code, FromDT, ToDt) {
            var tn;
            window.open('rptBrokerDetail.aspx?Broker_Code=' + Broker_Code + '&FromDT=' + FromDT + '&ToDt=' + ToDt);
        }
        function abr(FromDT, ToDt) {
            window.open('rptAllBrokerReport.aspx?FromDT=' + FromDT + '&ToDt=' + ToDt);
        }
        function bwsp1(Broker_Code, FromDT, ToDt) {
            var df;
            window.open('rptBrokerWiseShort.aspx?Broker_Code=' + Broker_Code + '&FromDT=' + FromDT + '&ToDt=' + ToDt);
        }
        function bwsp(Broker_Code, FromDT, ToDt) {
            var df2;
            window.open('rptBrokerWiseShortPayNew.aspx?Broker_Code=' + Broker_Code + '&FromDT=' + FromDT + '&ToDt=' + ToDt);
        }
        function bwspd(Broker_Code, FromDT, ToDt) {
            var df3;
            window.open('rptBrokerWiseShortDetail.aspx?Broker_Code=' + Broker_Code + '&FromDT=' + FromDT + '&ToDt=' + ToDt);
        }

        function bwspa(Broker_Code, FromDT, ToDt) {
            var df2;
            window.open('rptBrokerWiseShortPayNewAll.aspx?Broker_Code=' + Broker_Code + '&FromDT=' + FromDT + '&ToDt=' + ToDt);
        }
        function bwspda(Broker_Code, FromDT, ToDt) {
            var df3;
            window.open('rptBrokerWiseShortDetailAll.aspx?Broker_Code=' + Broker_Code + '&FromDT=' + FromDT + '&ToDt=' + ToDt);
        }

        function bwlp(FromDT, ToDt, Broker_Code) {
            window.open('rptBrokerWiseLatePay.aspx?FromDT=' + FromDT + '&ToDt=' + ToDt + '&Broker_Code=' + Broker_Code);
        }
        function bwlpa(FromDT, ToDt, Broker_Code) {
            window.open('rptBrokerWiseLatePayAll.aspx?FromDT=' + FromDT + '&ToDt=' + ToDt + '&Broker_Code=' + Broker_Code);
        }

        function pbr(FromDT, ToDt, Broker_Code) {
            window.open('rptPurchaseBrokrage.aspx?FromDT=' + FromDT + '&ToDt=' + ToDt + '&Broker_Code=' + Broker_Code);
        }
        function PSBroker(accode, FromDt, ToDt) {
            var tn;
            window.open('rptTenderPurchaseBrokerReport.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function PSBrokerDetails(accode, FromDt, ToDt) {
            var tn;
            window.open('rptTenderPurchaseBrokerDetailReport.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function DetailSellbrokerReport(accode, FromDt, ToDt) {
            var tn;
            window.open('rptDetailSellBrokerReport.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function TenderDOReport(accode, FromDt, ToDt) {
            var tn;
            window.open('rptTenderDOReport.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }


        function Dobrokerreport(accode, FromDt, ToDt) {
            var tn;
            window.open('rptDobrokerreport.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }

        function brokerwisependibgbil(accode, FromDt, ToDt) {
            var tn;
            window.open('rptbrokerwisependibgbil.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }

    </script>
    <script type="text/javascript" language="javascript">
        debugger;
        document.addEventListener('keyup', function (event) {
            if (event.defaultPrevented) {
                return;
            }

            var key = event.key || event.keyCode;

            if (key === 'Escape' || key === 'Esc' || key === 27) {
                //                doWhateverYouWantNowThatYourKeyWasHit();
                debugger;
                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                if (hdnfClosePopupValue == "txtAcCode") {
                    document.getElementById("<%=txtAcCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPartyCode") {
                    document.getElementById("<%=txtPartyCode.ClientID %>").focus();
                }

                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
    </script>
    <script type="text/javascript" language="javascript">
        function ac_name(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                //$("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnAcCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtAcCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtAcCode.ClientID %>").val(unit);
                __doPostBack("txtAcCode", "TextChanged");

            }
        }

        function party(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                //$("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnPartyCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtPartyCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtPartyCode.ClientID %>").val(unit);
                __doPostBack("txtPartyCode", "TextChanged");

            }
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
                if (hdnfClosePopupValue == "txtPartyCode") {
                    document.getElementById("<%= txtPartyCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblPartyName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   GLedger Report   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:HiddenField ID="hdnfClosePopup" runat="server" />
    <asp:HiddenField ID="hdHelpPageCount" runat="server" />
    <asp:HiddenField ID="hdnfpopup" runat="server" />
    <table width="60%" align="center">
        <tr>
            <td align="left" style="width: 40%;">Broker Code:
            </td>
            <td align="left" colspan="2" style="width: 60%;">
                <asp:TextBox ID="txtAcCode" runat="server" Width="80px" CssClass="txt" AutoPostBack="false"
                    OnTextChanged="txtAcCode_TextChanged" Height="24px" onkeydown="ac_name(event);"></asp:TextBox>
                <asp:Button ID="btnAcCode" runat="server" Text="..." CssClass="btnHelp" OnClick="btnAcCode_Click"
                    Height="24px" Width="20px" />
                <asp:Label ID="lblAcCodeName" runat="server" CssClass="lblName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 40%;">Party Code:
            </td>
            <td align="left" colspan="2" style="width: 60%;">
                <asp:TextBox ID="txtPartyCode" runat="server" Width="80px" CssClass="txt" AutoPostBack="false"
                    Height="24px" OnTextChanged="txtPartyCode_TextChanged" onkeydown="party(event);"></asp:TextBox>
                <asp:Button ID="btnPartyCode" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                    Width="20px" OnClick="btnPartyCode_Click" />
                <asp:Label ID="lblPartyName" runat="server" CssClass="lblName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">From Date:
            </td>
            <td align="left">
                <asp:TextBox ID="txtFromDt" runat="server" Width="80px" CssClass="txt" Height="24px"
                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                    Width="25px" Height="15px" />
                <ajax1:CalendarExtender ID="calenderExtendertxtFromDt" runat="server" TargetControlID="txtFromDt"
                    PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td align="left">To Date:
            </td>
            <td align="left">
                <asp:TextBox ID="txtToDt" runat="server" Width="80px" CssClass="txt" Height="24px"
                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                    Height="15px" />
                <ajax1:CalendarExtender ID="CalendarExtendertxtToDt" runat="server" TargetControlID="txtToDt"
                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
            </td>
        </tr>
    </table>
    <table width="60%" cellpadding="5" cellspacing="5" align="center">
        <tr>
            <td>
                <asp:Button runat="server" ID="btnBrokerDetail" CssClass="btnHelp" OnCommand="Command_Click"
                    Height="24px" CommandName="BD" Text="Broker Detail" Visible="false" Width="185px" />
                &nbsp;&nbsp;
                <asp:Button runat="server" ID="btnBrokerWiseShortNew" Text="Broker Wise Short Payment"
                    CssClass="btnHelp" Visible="false" OnCommand="Command_Click" CommandName="BWSP" Width="185px"
                    Height="24px" />
                &nbsp;&nbsp;
                <asp:Button runat="server" ID="btnBrokerWiseLatePayment" CssClass="btnHelp"
                    Height="24px" Visible="false" OnCommand="Command_Click" CommandName="BWLP" Text="Broker Wise Late Payment"
                    Width="185px" />
                &nbsp;&nbsp;
                <asp:Button runat="server" ID="btnPurchaseBrokrage" CssClass="btnHelp" Visible="false"
                    Height="24px" Text="Purchase Brokrage Details" Width="185px" OnCommand="Command_Click"
                    CommandName="PBR" />
            </td>
            </tr>
        <tr>
            <td>
                <asp:Button ID="btnTenderPSBrokerReport" runat="server" Text="Purcahse Broker Report"
                    CssClass="btnHelp" Width="200px" Height="24px" TabIndex="6" OnClick="btnTenderPSBrokerReport_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnTenderPSBrokerRptDetails" runat="server" Text=" Sale Broker Report"
                    CssClass="btnHelp" Width="200px" Height="24px" TabIndex="6" OnClick="btnTenderPSBrokerRptDetails_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDetailSellBrokerReport" runat="server" Text="Retail Sell broker Report"
                    CssClass="btnHelp" Width="200px" Height="24px" TabIndex="6" OnClick="btnDetailSellBrokerReport_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnTenderDOReport" runat="server" Text="Tender DO Report"
                    CssClass="btnHelp" Width="200px" Height="24px" TabIndex="6" OnClick="btnTenderDOReport_Click" />
            </td>
            </tr>
        <tr>
            <td>
                <asp:Button ID="btndobrokerreport" runat="server" Text="Do Broker Report"
                    CssClass="btnHelp" Width="200px" Height="24px" TabIndex="6" OnClick="btndobrokerreport_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnbrokerwisependingbill" runat="server" Text="Brokerwise Pending Bill"
                    CssClass="btnHelp" Width="200px" Height="24px" TabIndex="6" OnClick="btnbrokerwisependingbill_Click" />
                &nbsp;&nbsp;&nbsp;
                <%--<asp:Button ID="btnAllBrokerDetail" runat="server" Text="All Broker Detail"
                     CssClass="btnHelp" Width="200px" Height="24px" TabIndex="6" OnClick="btnAllBrokerDetail_Click" />    --%>
                <asp:Button runat="server" ID="btnAllBrokerDetail" CssClass="btnHelp" OnCommand="Command_Click"
                    Height="24px" CommandName="ABD" Text="All Broker Detail" Width="200px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="btnBrokerWiseShortPayAll" CssClass="btnHelp" Visible="false" Text="Broker Wise ShortPay All"
                    Height="24px" OnCommand="Command_Click" CommandName="BWSPA" Width="185px" />
                &nbsp;&nbsp;
                <asp:Button runat="server" ID="btnBrokerWiseLatePayAll" CssClass="btnHelp"
                    Height="24px" OnCommand="Command_Click" Visible="false" CommandName="BWLPA" Text="Broker Wise Late Pay All"
                    Width="185px" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button runat="server" ID="btnBrokerWiseShortPayment" CssClass="btnHelp" Visible="false"
                    Height="24px" Text="Broker Wise Short Payment" Width="185px" OnCommand="Command_Click"
                    CommandName="BWSP1" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td align="left">
                <asp:Panel ID="pnlGrid" runat="server" align="left" BorderColor="Blue" BorderWidth="1px"
                    DefaultButton="btnEnter" BackColor="White" Height="300px" ScrollBars="Both" Style="text-align: left"
                    Width="1100px">
                    <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="false" CellPadding="6"
                        Font-Bold="true" ForeColor="Black" GridLines="Both" HeaderStyle-BackColor="#397CBB"
                        HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" RowStyle-Height="30px"
                        BackColor="White" Width="1080px" RowStyle-Wrap="false" Style="table-layout: fixed;"
                        OnRowDataBound="grdDetail_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="Broker" DataField="Broker" />
                            <asp:BoundField HeaderText="Message" DataField="Message" />
                            <asp:TemplateField HeaderText="Unit Mobile">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtPartyMobile" MaxLength="10" BorderStyle="None"
                                        Font-Bold="true" Text='<%#Eval("Party_Mobile") %>' Width="110px" Height="25px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Broker Mobile">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtSendingMobile" MaxLength="10" BorderStyle="None"
                                        Font-Bold="true" Text='<%#Eval("Sending_Mobile") %>' Width="110px" Height="25px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Short" HeaderText="Short" />
                            <asp:BoundField HeaderText="Short Payment" DataField="Short_Payment" />
                            <asp:TemplateField ItemStyle-Width="50px">
                                <HeaderTemplate>
                                    <asp:CheckBox runat="server" ID="chkAll" Text="SMS" OnClick="selectAll(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="grdCB" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Button runat="server" ID="btnEnter" Style="display: none;" OnClick="btnEnter_Click" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button runat="server" ID="btnSendSms" Text="Send SMS" CssClass="btnHelp" Width="100px"
                    Height="24px" OnClick="btnSendSms_Click" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%" align="center" ScrollBars="None"
        BackColor="#FFFFE4" Direction="LeftToRight" Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
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
                <td>Search Text:
                    <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server" Width="250px" Height="20px" AutoPostBack="false"></asp:TextBox>
                    <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search" CssClass="btnSubmit" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                        Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                        <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true" EmptyDataText="No Records Found"
                            ViewStateMode="Disabled" PageSize="20" AllowPaging="true" HeaderStyle-BackColor="#6D8980"
                            HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated" OnPageIndexChanging="grdPopup_PageIndexChanging"
                            Style="table-layout: fixed;" OnRowDataBound="grdPopup_RowDataBound">
                            <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                            <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                            <RowStyle Height="25px" ForeColor="Black" Wrap="false" />
                            <PagerStyle BackColor="Tomato" ForeColor="White" Width="100%" Font-Bold="true" />
                            <PagerSettings Position="TopAndBottom" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
