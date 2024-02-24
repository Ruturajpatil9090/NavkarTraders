<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeFuturetranstion.aspx.cs" Inherits="pgeFuturetranstion" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <script src="../JQuery/jquery-1.4.1.js" type="text/javascript"></script>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../../JQuery/jquery.keynavigation.js" type="text/javascript"></script>
    <link type="text/css" href="../../menu/menu.css" rel="stylesheet" />
    <script type="text/javascript" src="../menu/jquery.js"></script>
    <script type="text/javascript" src="../menu/menu.js"></script>
    <script src="../../JS/jquery-2.1.3.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>
     <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="../../JS/DateValidation.js"> </script>
     <script src="../../Scripts/selectfirstrow.js" type="text/javascript"></script>
       <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript" language="javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete data?")) {
                confirm_value.value = "Yes";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            }
            else {
                confirm_value.value = "No";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
            }
            document.forms[0].appendChild(confirm_value);
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

                if (hdnfClosePopupValue == "txtScript_Code") {

                    document.getElementById("<%=txtScript_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBroker_code") {

                    document.getElementById("<%=txtBroker_code.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtExpiry_date") {

                    document.getElementById("<%=txtExpiry_date.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtEditDoc_No") {

                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";

            }

        });
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
                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                var grid = document.getElementById("<%= grdPopup.ClientID %>");
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
                var pageCount = document.getElementById("<%= hdHelpPageCount.ClientID %>").value;
                pageCount = parseInt(pageCount);
                if (pageCount > 1) {
                    SelectedRowIndex = SelectedRowIndex + 1;
                }
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = "";
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtDoc_No") {
                    document.getElementById("<%= txtDoc_No.ClientID %>").value = "";
                    document.getElementById("<%= txtDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtExpiry_date") {
                    document.getElementById("<%= txtExpiry_date.ClientID %>").value = "";
                    document.getElementById("<%= txtExpiry_date.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtExpiry_date.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBroker_code") {
                    document.getElementById("<%= txtBroker_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= txtBroker_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtBroker_code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtScript_Code") {
                    document.getElementById("<%=txtScript_Code.ClientID%>").disabled = false;
                    document.getElementById("<%= txtScript_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblscrpit_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%= txtLot_Size.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%= txtExpiry_date.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[4].innerText;
                    document.getElementById("<%=txtScript_Code.ClientID %>").focus();
                }
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
    <script type="text/javascript" language="javascript">

        function Focusbtn(e) {
            debugger;

            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }

        }

        function Broker_code(e) {
            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtBroker_code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var Accode = $("#<%=txtBroker_code.ClientID %>").val();

                Accode = "0" + Accode;
                $("#<%=txtBroker_code.ClientID %>").val(Accode);

                __doPostBack("txtBroker_code", "TextChanged");

            }

        }
        function Expriydate(e) {
            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnexpriydate.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var Accode = $("#<%=txtExpiry_date.ClientID %>").val();

                //Accode = "0" + Accode;
                $("#<%=txtExpiry_date.ClientID %>").val(Accode);

                __doPostBack("txtExpiry_date", "TextChanged");

            }

        }

        function Remark(e) {
            if (e.keyCode == 9) {
                e.preventDefault();
                $("#<%=btntxtScript_Code.ClientID %>").focus();

            }
        }

        function script(e) {
            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtScript_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var Accode = $("#<%=txtScript_Code.ClientID %>").val();

                Accode = "0" + Accode;
                $("#<%=txtScript_Code.ClientID %>").val(Accode);

                __doPostBack("txtScript_Code", "TextChanged");

            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }

        }


        function DocNo(e) {
            if (e.keyCode == 112) {


                e.preventDefault();

                $("#<%=btntxtDoc_No.ClientID %>").click();

            }
            if (e.keyCode == 9) {

                __doPostBack("txtDoc_No", "TextChanged");

            }

        }

        function changeno(e) {
            debugger;
            if (e.keyCode == 112) {

                e.preventDefault();

                var edi = "txtEditDoc_No"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtEditDoc_No", "TextChanged");

            }
        }

        function FocusTOT_Days(e, obj) {
            debugger;

            if (e.keyCode == 40) {
                var td = $(obj).parent().prev("td");

                var tr = $(obj).parent().parent().prev("tr");
                if (tr.length > 0) {
                    var $ReciveNo_zero = $(obj).find('input');
                    //var $usr = $(obj).find('input[name$=openid_username]');
                    var aaa = $(obj).closest("tr").next().find("input[type=text]");


                    aaa[0].focus();

                }

            }
            if (e.keyCode == 38) {
                var td = $(obj).parent().prev("td");

                var tr = $(obj).parent().parent().prev("tr");
                if (tr.length > 0) {
                    var $ReciveNo_zero = $(obj).find('input');
                    //var $usr = $(obj).find('input[name$=openid_username]');
                    var aaa = $(obj).closest("tr").prev().find("input[type=text]");
                    aaa[0].focus();

                }

            }


        }

        function changeno(e) {
            debugger;
            if (e.keyCode == 112) {

                e.preventDefault();
                var help = "f1";
                $("#<%=hdnfhelp.ClientID %>").val(help);
                var edi = "txtEditDoc_No"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();

            }
            if (e.keyCode == 113) {

                e.preventDefault();
                var help = "113";
                $("#<%=hdnfhelp.ClientID %>").val(help);
                var edi = "txtEditDoc_No"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtEditDoc_No", "TextChanged");

            }


        }

    </script>
    <script type="text/javascript" language="javascript">
        function calculateProfit_Loss(obj) {
            debugger;
            if (obj.type == "text") {

                var RowProfitLoss = 0.00;
                var RowSaleValue = 0.00;
                var RowSaleQty = 0.00;
                var RowClosingRate = 0.00;
                var RowLotSize = 0.00;

                var RowClosingQty = 0.00;
                var RowBuyValue = 0.00;
                var RowNetProfitLoss = 0.00;
                var ProfitLoss = 0.00;

                RowSaleValue = parseFloat($(obj).closest('td').prev('td').prev('td').prev('td').text());
                RowSaleQty = parseFloat($(obj).closest('td').prev('td').prev('td').prev('td').prev('td').prev('td').text());
                RowClosingRate = parseFloat($(obj).closest('td').find('input').val() != '' ? $(obj).closest('td').find('input').val() : 0.00);
                RowLotSize = parseFloat($(obj).closest('td').prev('td').prev('td').prev('td').prev('td').prev('td').prev('td').prev('td').prev('td').prev('td').prev('td').text());
                RowBuyValue = parseFloat($(obj).closest('td').prev('td').prev('td').prev('td').prev('td').prev('td').prev('td').text());
                RowClosingQty = parseFloat($(obj).closest('td').prev('td').text());



                //                if (RowClosingRate < 0) {
                //                    RowProfitLoss = RowSaleValue + (RowSaleQty * RowClosingRate * RowLotSize)

                //                    var $ProfitLoss = $(obj).closest('td').next('td').find('input');
                //                    $($ProfitLoss).val(RowProfitLoss);


                //                }
                //                else {
                //                    RowProfitLoss = (RowSaleQty * RowClosingRate * RowLotSize) - RowBuyValue

                //                    var $ProfitLoss = $(obj).closest('td').next('td').find('input');
                //                    $($ProfitLoss).val(RowProfitLoss);
                //                }

                $('#<%=txtNetProfitLoss.ClientID %>').val('');
                $(".Recived_no_class").each(function () {

                    if (RowClosingQty < 0) {
                        RowProfitLoss = (RowSaleQty * RowClosingRate * RowLotSize) - RowSaleValue
                        //// $(obj).closest('td').prev('td').next('td').next('td').text(parseFloat(RowProfitLoss));


                        //                        var $ProfitLoss = $(obj).closest('td').prev('td').next('td').next('td');
                        //                        $($ProfitLoss).val(parseFloat(RowProfitLoss));


                        $(obj).closest('td').next('td').find('input').val(parseFloat(RowProfitLoss));
                    }
                    else {
                        RowProfitLoss = RowBuyValue - (RowClosingQty * RowClosingRate * RowLotSize)
                        ////$(obj).closest('td').prev('td').next('td').next('td').text(parseFloat(RowProfitLoss));
                        //                        var $ProfitLoss = $(obj).closest('td').next('td').find('input');
                        //                        $($ProfitLoss).val(parseFloat(RowProfitLoss));

                        $(obj).closest('td').next('td').find('input').val(parseFloat(RowProfitLoss));
                    }


                    RowNetProfitLoss = parseFloat($(obj).closest('td').next('td').text());




                });



            }
            TotalProfitLoss();
        }

        function TotalProfitLoss(e) {
            debugger;
            var ProfitLoss = 0;
            var NetProfitLoss = 0;
            var NetExpenses = 0;
            var Netpnl = 0;
            var gridView = document.getElementById("<%=grddetailFuture.ClientID %>");
            var grdrow = gridView.getElementsByTagName("tr");
            for (var i = 0; i < grdrow.length - 1; i++) {
                //ProfitLoss = parseFloat(ProfitLoss + parseFloat(gridView.rows[i].cells[15].innerHTML));




                var val = $("input[id*=txtProfitLoss]")
                ProfitLoss = val[i].value;

                NetProfitLoss = NetProfitLoss + parseFloat(ProfitLoss);
            }
            document.getElementById("<%= txtNetProfitLoss.ClientID %>").value = NetProfitLoss.toFixed(2);
//            NetExpenses = parseFloat(document.getElementById("<%= txtNetExpenses.ClientID %>").value)

            NetExpenses = parseFloat($("#<%=txtNetExpenses.ClientID %>").val() == "" ? 0 : $("#<%=txtNetExpenses.ClientID %>").val());

            Netpnl = NetProfitLoss - NetExpenses;

            document.getElementById("<%= txtNet_pnl.ClientID %>").value = Netpnl.toFixed(2);
        }

        function Focus(e, obj) {
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=txtNetbrokrage.ClientID %>").focus();


            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Enter page name " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfhelp" runat="server" />
            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" TabIndex="32" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" />
                    </td>
                    <td align="center">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnFirst_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnPrevious_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnNext_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnLast_Click" Width="90px" Height="24px" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 100%;" align="Left" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Names="verdana" Font-Italic="true"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">
                            Change No
                            <asp:TextBox Height="24px" ID="txtEditDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEditDoc_No_TextChanged"
                                onKeyDown="changeno(event);"></asp:TextBox>
                            Doc No
                            <asp:TextBox Height="24px" ID="txtDoc_No" runat="Server" CssClass="txt" TabIndex="2"
                                Width="90px" Style="text-align: left;" OnkeyDown="Doc_No(event);" AutoPostBack="false"
                                OnTextChanged="txtDoc_No_TextChanged"></asp:TextBox>
                            <asp:Button Width="20px" Height="24px" ID="btntxtDoc_No" runat="server" Text="..."
                                OnClick="btntxtDoc_No_Click" CssClass="btnHelp" />
                            Doc Date
                            <asp:TextBox Height="24px" ID="txtDoc_Date" runat="Server" CssClass="txt" TabIndex="3"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDoc_Date_TextChanged"
                                onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtDoc_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtDoc_Date"
                                    runat="server" TargetControlID="txtDoc_Date" PopupButtonID="imgcalendertxtDoc_Date"
                                    Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                            Broker Code
                            <asp:TextBox Height="24px" ID="txtBroker_code" runat="Server" CssClass="txt" TabIndex="4"
                                Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtBroker_code_TextChanged"
                                MaxLength="18" OnKeyDown="Broker_code(event);"></asp:TextBox>
                            <asp:Button Width="20px" Height="24px" ID="btntxtBroker_code" runat="server" Text="..."
                                OnClick="btntxtBroker_code_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblParty_Code" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">
                            Bill No
                            <asp:TextBox Height="24px" ID="txtBill_No" runat="Server" CssClass="txt" TabIndex="5"
                                Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_No_TextChanged"></asp:TextBox>
                            Sattlement No
                            <asp:TextBox Height="24px" ID="txtsattlement_No" runat="Server" CssClass="txt" TabIndex="6"
                                Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtsattlement_No_TextChanged"></asp:TextBox>
                            Remark
                            <asp:TextBox Height="40px" ID="txtRemark" runat="Server" CssClass="txt" TabIndex="7"
                                Width="400px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtRemark_TextChanged"
                                OnKeyDown="Remark(event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            <asp:Button ID="btnOpenDetailsPopup" runat="server" Text="ADD" CssClass="btnHelp"
                                Width="80px" Height="25px" OnClick="btnOpenDetailsPopup_Click" TabIndex="33"
                                Visible="false" />
                        </td>
                    </tr>
                </table>
                <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
                    margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
                    border-left: 0px; border-right: 0px; height: 5px;">
                    <legend style="text-align: center;">
                        <h5 style="color: White;" font-names="verdana" font-size="Medium">
                            Detail Entry</h5>
                    </legend>
                </fieldset>
                <table width="80%" align="left">
                    <tr>
                        <td align="left" style="width: 10%;">
                            <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Text="Enter Name"></asp:Label>
                            <asp:Label ID="lblID" runat="server" CssClass="lblName" Font-Names="verdana" Text=""
                                Visible="false"></asp:Label>
                            <asp:Label ID="lblNo" runat="server" CssClass="lblName" Font-Names="verdana" Text=""
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">
                            Script Code
                            <asp:TextBox ID="txtScript_Code" runat="Server" CssClass="txt" TabIndex="8" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtScript_Code_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtScript_Code" runat="server" Text="..." OnClick="btntxtScript_Code_Click"
                                CssClass="btnHelp" />
                            <asp:Label ID="lblscrpit_Code" runat="server" CssClass="lblName"></asp:Label>
                            Expiry Date
                            <asp:TextBox ID="txtExpiry_date" runat="Server" CssClass="txt" TabIndex="9" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtExpiry_date_TextChanged"
                                OnkeyDown="Expriydate(event);" Height="24px"></asp:TextBox>
                            <asp:Button ID="btnexpriydate" runat="server" Text="..." OnClick="btnexpriydate_Click"
                                CssClass="btnHelp" />
                            <asp:Image ID="imgcalendertxtExpiry_date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" Visible="false" />
                            <%-- <ajax1:CalendarExtender ID="CalendarExtenderDatetxtExpiry_date" runat="server" TargetControlID="txtExpiry_date"
                                PopupButtonID="imgcalendertxtExpiry_date" Format="dd/MM/yyyy" >
                            </ajax1:CalendarExtender>--%>
                            Lot Size
                            <asp:TextBox ID="txtLot_Size" runat="Server" CssClass="txt" TabIndex="10" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtLot_Size_TextChanged"
                                Height="24px"></asp:TextBox>
                            Future Type:
                            <asp:DropDownList ID="drpFuture_Type" runat="server" AutoPostBack="true" CssClass="ddl"
                                Height="25px" OnSelectedIndexChanged="drpFuture_Type_SelectedIndexChanged" TabIndex="11"
                                Width="200px">
                                <asp:ListItem Selected="True" Text="Future" Value="F"></asp:ListItem>
                                <asp:ListItem Text="Call" Value="C"></asp:ListItem>
                                <asp:ListItem Text="Put" Value="P"></asp:ListItem>
                            </asp:DropDownList>
                            BuySale:
                            <asp:DropDownList ID="drpBuy_Sale" runat="server" AutoPostBack="true" CssClass="ddl"
                                Height="25px" OnSelectedIndexChanged="drpBuy_Sale_SelectedIndexChanged" TabIndex="12"
                                Width="200px">
                                <asp:ListItem Selected="true" Text="Buy" Value="B"></asp:ListItem>
                                <asp:ListItem Text="Sale" Value="S"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">
                            Lot Qty
                            <asp:TextBox ID="txtlot_qty" runat="Server" CssClass="txt" TabIndex="13" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtlot_qty_TextChanged"
                                Height="24px"></asp:TextBox>
                            Rate
                            <asp:TextBox ID="txtRate" runat="Server" CssClass="txt" TabIndex="14" Width="90px"
                                Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtRate_TextChanged"
                                Height="24px"></asp:TextBox>
                            value
                            <asp:TextBox ID="txtValue" runat="Server" CssClass="txt" TabIndex="15" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtValue_TextChanged"
                                Height="24px"></asp:TextBox>
                            Strike Price
                            <asp:TextBox ID="txtStrike_Price" runat="Server" CssClass="txt" TabIndex="16" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtStrike_Price_TextChanged"
                                Height="24px"></asp:TextBox>
                            AutoCarry
                            <asp:Label ID="lblAutoCarry" runat="Server" Text="    "></asp:Label>
                            <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnSubmit" Width="80px"
                                Height="25px" OnClick="btnAdddetails_Click" TabIndex="17" />
                            <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnSubmit"
                                Width="80px" Height="25px" OnClick="btnClosedetails_Click" TabIndex="18" />
                            <asp:Button ID="btnDaily_Positoin" runat="server" Text="Daily Positoin" CssClass="btnHelp"
                                Width="90px" ValidationGroup="save" OnClick="btnDaily_Positoin_Click"  Height="24px" />
                            <asp:Button ID="btnDeletebtnDaily_Positoin" runat="server" Text="Delete Daily Positoin"
                                CssClass="btnHelp" Width="120px" ValidationGroup="save" OnClick="DeletebtnDaily_Positoin_Click"
                                Height="24px" OnClientClick="Confirm()" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="width: 100%; position: relative;">
                <asp:UpdatePanel ID="upGrid" runat="server">
                    <ContentTemplate>
                        <table cellpadding="4" cellspacing="4" align="left">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlgrdDetail" runat="server" ScrollBars="Both" Height="200px" Width="1200px"
                                        BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true" Font-Names="Verdana"
                                        Font-Size="11px" BackColor="SeaShell" Style="margin-left: 30px; float: left;">
                                        <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" HeaderStyle-BackColor="#397CBB"
                                            HeaderStyle-ForeColor="White" HeaderStyle-Height="10px" GridLines="Both" Width="100%"
                                            OnRowCommand="grdDetail_RowCommand" CellPadding="5" CellSpacing="5" OnRowDataBound="grdDetail_RowDataBound"
                                            Style="table-layout: fixed;">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" Text="Edit"
                                                            CommandArgument="lnk"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord" Text="Delete"
                                                            CommandArgument="lnk"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle Height="25px" Wrap="false" ForeColor="Black" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="200px" Width="1500px"
                                        BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true" Font-Names="Verdana"
                                        Font-Size="11px" BackColor="SeaShell" Style="margin-left: 30px; float: left;">
                                        <asp:GridView ID="grddetailFuture" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#397CBB"
                                            HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="100%"
                                            OnRowCommand="grddetailFuture_RowCommand" CellPadding="5" CellSpacing="5" OnRowDataBound="grddetailFuture_RowDataBound"
                                            Style="table-layout: fixed;">
                                            <Columns>
                                                <asp:BoundField DataField="Detail_ID" HeaderText="Detail_ID" ItemStyle-Width="150px" />
                                                <asp:BoundField DataField="Script_Code" HeaderText="Script_Code" ItemStyle-Width="150px" />
                                                <asp:BoundField DataField="Script_Name" HeaderText="Script_Name" ItemStyle-Width="150px" />
                                                <asp:BoundField DataField="Expiry_Date" HeaderText="Expiry_Date" ItemStyle-Width="90px" />
                                                <asp:BoundField DataField="Lot_Size" HeaderText="Lot_Size" ItemStyle-Width="150px" />
                                                <asp:BoundField DataField="Type" HeaderText="Type" ItemStyle-Width="150px" />
                                                <asp:BoundField DataField="Buy_Qty" HeaderText="Buy_Qty" ItemStyle-Width="150px" />
                                                <asp:BoundField DataField="Buy_Rate" HeaderText="Buy_Rate" ItemStyle-Width="150px" />
                                                <asp:BoundField DataField="Buy_Value" HeaderText="Buy_Value" ItemStyle-Width="150px" />
                                                <asp:BoundField DataField="Sale_Qty" HeaderText="Sale_Qty" ItemStyle-Width="150px" />
                                                <asp:BoundField DataField="Sale_Rate" HeaderText="Sale_Rate" ItemStyle-Width="150px" />
                                                <asp:BoundField DataField="Sale_Value" HeaderText="Sale_Value" ItemStyle-Width="150px" />
                                                <asp:BoundField DataField="SP" HeaderText="SP" ItemStyle-Width="150px" />
                                                <asp:BoundField DataField="Closing_Qty" HeaderText="Closing_Qty" ItemStyle-Width="150px" />
                                                <%--<asp:BoundField DataField="Closing_Rate" HeaderText="Closing_Rate" ItemStyle-Width="150px" />--%>
                                                <asp:TemplateField HeaderText="Closing_Rate" ItemStyle-Width="150">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtClosing_Rate" BorderStyle="None" Font-Bold="true"
                                                            Width="110px" Height="25px" Text='<%#Eval("Closing_Rate") %>' AutoPostBack="false"
                                                            onchange="calculateProfit_Loss(this);" class="Recived_no_class" onKeyDown="Focus(event,this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="ProfitLoss" HeaderText="ProfitLoss" ItemStyle-Width="150px" />--%>
                                                <asp:TemplateField HeaderText="ProfitLoss" ItemStyle-Width="150">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtProfitLoss" BorderStyle="None" Font-Bold="true"
                                                            Width="110px" Height="25px" Text='<%#Eval("ProfitLoss") %>' AutoPostBack="false"
                                                            Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:BoundField DataField="rowAction" HeaderText="rowAction" ItemStyle-Width="90px" />--%>
                                            </Columns>
                                            <RowStyle Height="25px" Wrap="false" ForeColor="Black" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr style="font-family: Verdana; color: Black;">
                                <td align="left" style="width: 10%;">
                                    <b>Net Brokrage</b>
                                    <asp:TextBox Height="24px" ID="txtNetbrokrage" runat="Server" CssClass="txt" TabIndex="19"
                                        Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtNetbrokrage_TextChanged"></asp:TextBox>
                                    <b>Net Exchange Charges</b>
                                    <asp:TextBox Height="24px" ID="txtnetExchangecharges" runat="Server" CssClass="txt"
                                        TabIndex="20" Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtnetExchangecharges_TextChanged"></asp:TextBox>
                                    <b>Net STF</b>
                                    <asp:TextBox Height="24px" ID="txtnetSTF" runat="Server" CssClass="txt" TabIndex="21"
                                        Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtnetSTF_TextChanged"></asp:TextBox>
                                    <b>Net STT</b>
                                    <asp:TextBox Height="24px" ID="txtNet_Stt" runat="Server" CssClass="txt" TabIndex="22"
                                        Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtNet_Stt_TextChanged"></asp:TextBox>
                                    <b>Net StampDuty</b>
                                    <asp:TextBox Height="24px" ID="txtNet_StampDuty" runat="Server" CssClass="txt" TabIndex="23"
                                        Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtNet_StampDuty_TextChanged"></asp:TextBox>
                                    <b>Net CM Charge</b>
                                    <asp:TextBox Height="24px" ID="txtcmcharge" runat="Server" CssClass="txt" TabIndex="24"
                                        Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtcmcharge_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="font-family: Verdana; color: Black;">
                                <td align="left" style="width: 10%;">
                                    <b>Net Cgst</b>
                                    <asp:TextBox Height="24px" ID="txtNet_Cgst" runat="Server" CssClass="txt" TabIndex="25"
                                        Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtNet_Cgst_TextChanged"></asp:TextBox>
                                    <b>Net Sgst</b>
                                    <asp:TextBox Height="24px" ID="txtNet_Sgst" runat="Server" CssClass="txt" TabIndex="26"
                                        Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtNet_Sgst_TextChanged"></asp:TextBox>
                                    <b>Net Igst</b>
                                    <asp:TextBox Height="24px" ID="txtNet_Igst" runat="Server" CssClass="txt" TabIndex="27"
                                        Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtNet_Igst_TextChanged"></asp:TextBox>
                                    <b>Net Postage</b>
                                    <asp:TextBox Height="24px" ID="txtNet_Postage" runat="Server" CssClass="txt" TabIndex="28"
                                        Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtNet_Postage_TextChanged"></asp:TextBox>
                                    <b>Net ProfitLoss</b>
                                    <asp:TextBox Height="24px" ID="txtNetProfitLoss" runat="Server" CssClass="txt" TabIndex="29"
                                        Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtNetProfitLoss_TextChanged"></asp:TextBox>
                                    <b>Net Expenses</b>
                                    <asp:TextBox Height="24px" ID="txtNetExpenses" runat="Server" CssClass="txt" TabIndex="30"
                                        Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtNetExpenses_TextChanged"></asp:TextBox>
                                    <b>Net pnl</b>
                                    <asp:TextBox Height="24px" ID="txtNet_pnl" runat="Server" CssClass="txt" TabIndex="31"
                                        Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtNet_pnl_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:Panel onkeydown="closepopup(event);" ID="pnlPopup" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 1000px;
                min-height: 700px; box-shadow: 1px 1px 8px 2px; background-position: center;
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
                            <asp:TextBox onkeydown="SelectFirstRow(event);" ID="txtSearchText" runat="server"
                                Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button onkeydown="closepopup(event);" ID="btnSearch" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 680">
                                <asp:GridView Font-Bold="true" CssClass="select" AllowPaging="true" PageSize="25"
                                    OnPageIndexChanging="grdPopup_PageIndexChanging" ID="grdPopup" runat="server"
                                    AutoGenerateColumns="true" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound"
                                    Style="table-layout: fixed;">
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
            <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="GhostWhite" Width="955px"
                BorderColor="Teal" BorderWidth="1px" Height="300px" BorderStyle="Solid" Style="z-index: 4999;
                left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
