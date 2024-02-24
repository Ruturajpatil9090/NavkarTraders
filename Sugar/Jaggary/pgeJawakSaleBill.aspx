<%@ Page Title="Jawak Sale Bill" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeJawakSaleBill.aspx.cs" Inherits="Sugar_pgeJawakSaleBill" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <script type="text/javascript">
        function SB() {
            var billno = document.getElementById('<%=txtdoc_no.ClientID %>').value;
            window.open('../Report/rptJawakSaleBill.aspx?billno=' + billno)
        }
        function SB1() {
            var billno = document.getElementById('<%=txtdoc_no.ClientID %>').value;
            window.open('../Report/rptJawakSaleBillCry.aspx?billno=' + billno)
        }
        function GEway() {
            var dono = document.getElementById('<%=txtdoc_no.ClientID %>').value;
            var type = 'JS'
            window.open('../Utility/pgeEwayBill.aspx?dono=' + dono + '&Type=' + type);
        }
        function EInovice() {
            var dono = document.getElementById('<%=txtdoc_no.ClientID %>').value;
            var type = 'JS'
            window.open('../Utility/pgeEInovice.aspx?dono=' + dono + '&Type=' + type);
        }
    </script>
    <script type="text/javascript">
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

        function printChq() {
            window.open('../Report/rptChqPrint.aspx');    //R=Redirected  O=Original
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

                if (hdnfClosePopupValue == "txtCustCode") {
                    document.getElementById("<%=txtCustCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblCustomer.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtCustCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtItemCode") {
                    document.getElementById("<%=txtItemCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblItem.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtItemCode.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtdoc_no") {
                    document.getElementById("<%=txtdoc_no.ClientID %>").value = "";
                    document.getElementById("<%=txtdoc_no.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtdoc_no.ClientID %>").disabled = false;
                    document.getElementById("<%=txtdoc_no.ClientID %>").focus();
                }


                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGST_RateCode") {
                    document.getElementById("<%=txtGST_RateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblGST_RateCode.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGST_RateCode.ClientID %>").focus();
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
                debugger;
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();

            }
            else if (e.keyCode == 27) {
                e.preventDefault();

            }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Jawak   " Font-Names="verdana" ForeColor="White"
                Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfTransportBalance" runat="server" />
            <asp:HiddenField ID="hdnfSaleTCSRate" runat="server" />
            <asp:HiddenField ID="hdnfSaleTDSRate" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                &nbsp;
                <table style="float: left;" width="100%" cellspacing="3">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblNo" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblID" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Change No:
                            <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="20px"
                                TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                            &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 30%;">Cash/Credit: &nbsp;<asp:DropDownList ID="drpCsCr" runat="server" CssClass="txt" TabIndex="1"
                            Width="100px" Height="20px">
                            <asp:ListItem Text="Cash" Value="CS" />
                            <asp:ListItem Text="Credit" Value="CR" Selected="True" />
                        </asp:DropDownList>
                            Bill No:
                            <asp:TextBox ID="txtdoc_no" runat="Server" CssClass="txt" TabIndex="2" Width="50px"
                                Style="text-align: right;" Height="16px" AutoPostBack="true" OnTextChanged="txtdoc_no_TextChanged"></asp:TextBox>
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                            &nbsp;<asp:Button ID="btntxtdoc_no" runat="server" Text="..." Width="80px" OnClick="btntxtdoc_no_Click"
                                CssClass="btnHelp" Height="16px" />
                            Date:
                            <asp:TextBox ID="txtdoc_date" runat="Server" CssClass="txt" TabIndex="3" Width="70px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                Style="text-align: left;" Height="16px"></asp:TextBox>
                            <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="15px" Height="16px" />
                            <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtdoc_date"
                                PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%;">Customer Code: &nbsp;
                            <asp:TextBox ID="txtCustCode" runat="server" CssClass="txt" Style="text-align: right;"
                                onKeyDown="Focusbtn(event);" AutoPostBack="True" TabIndex="4" Height="16px" Width="50px"
                                OnTextChanged="txtCustCode_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtCustCode" runat="server" Text="..." CssClass="btnHelp" Height="16px"
                                Width="10px" OnClick="btntxtCustCode_Click" />&nbsp;
                            <asp:Label ID="lblCustomer" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblCustomerGSTStateCode" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%;">GST Rate: &nbsp;
                            <asp:TextBox ID="txtGST_RateCode" runat="server" CssClass="txt" Style="text-align: right;"
                                onKeyDown="Focusbtn(event);" AutoPostBack="True" TabIndex="5" Height="16px" Width="50px"
                                OnTextChanged="txtGST_RateCode_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnGST_RateCode" runat="server" Text="..." CssClass="btnHelp" Height="16px"
                                Width="10px" OnClick="btnGST_RateCode_Click" />&nbsp;
                            <asp:Label ID="lblGST_RateCode" runat="server" CssClass="lblName"></asp:Label>
                            Vehicle No:
                            <asp:TextBox ID="txtVehicleNo" runat="Server" CssClass="txt" TabIndex="6" Width="100px"
                                Style="text-align: right;" AutoPostBack="false" Height="16px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; border-top: 1px solid black; visibility: hidden;">Type: &nbsp;
                            <asp:TextBox ID="txtType" runat="server" CssClass="txt" Style="text-align: right;"
                                AutoPostBack="True" TabIndex="7" Height="16px" Width="50px" onKeyDown="Focusbtn(event);"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtAwakno" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Bill No: &nbsp;
                            <asp:TextBox ID="txtAwakno" runat="server" CssClass="txt" Style="text-align: right;"
                                AutoPostBack="True" TabIndex="8" Height="16px" Width="50px" onKeyDown="Focusbtn(event);"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTtxtAwakno" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtAwakno" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Item code:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtItemCode" runat="server" AutoPostBack="True" CssClass="txt" Height="16px"
                                OnTextChanged="txtItemCode_TextChanged" Style="text-align: right;" TabIndex="9"
                                Width="50px"></asp:TextBox>
                            <asp:Button ID="btntxtItemCode" runat="server" CssClass="btnHelp" Height="16px" OnClick="btntxtItem_Click"
                                Text="..." Width="10px" />
                            &nbsp;
                            <asp:Label ID="lblItem" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="visibility: hidden;">Qty:&nbsp;&nbsp;
                            <asp:TextBox ID="txtQty" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px"
                                OnTextChanged="txtQty_TextChanged" Style="text-align: right;" TabIndex="10" Width="70px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtQty" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtQty" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Net Wt:&nbsp;&nbsp;
                            <asp:TextBox ID="txtNetWt" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px"
                                OnTextChanged="txtNetWt_TextChanged" Style="text-align: right;" TabIndex="11"
                                Width="70px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtNetWt" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtNetWt" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Rate:&nbsp;&nbsp;
                            <asp:TextBox ID="txtRate" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px"
                                OnTextChanged="txtRate_TextChanged" Style="text-align: right;" TabIndex="12"
                                Width="70px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtRate" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtRate" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Sale Value:&nbsp;&nbsp;
                            <asp:TextBox ID="txtSaleValue" runat="Server" AutoPostBack="true" CssClass="txt"
                                Height="16px" OnTextChanged="txtSaleValue_TextChanged" Style="text-align: right;"
                                TabIndex="13" Width="70px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtSaleValue" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtSaleValue" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="color: Blue; border-bottom: 1px solid black;">Detail Information
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="visibility: hidden;">
                            <asp:Button ID="btnAdddetails" runat="server" CssClass="btnHelp" Height="16px" OnClick="btnAdddetails_Click"
                                TabIndex="14" Text="ADD" Width="50px" />
                            &nbsp;
                            <asp:Button ID="btnClosedetails" runat="server" CssClass="btnHelp" Height="16px"
                                OnClick="btnClosedetails_Click" TabIndex="15" Text="Reset" Width="50px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 80%;">
                            <br />
                            <br />
                            <div style="width: 70%; position: relative; vertical-align: top; margin-top: -20px;">
                                <asp:UpdatePanel ID="upGrid" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlgrdDetail" runat="server" align="left" BackColor="SeaShell" BorderColor="Maroon"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"
                                            Height="200px" ScrollBars="Both" Style="margin-left: 00px; float: left;" Width="797px">
                                            <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" CellPadding="5"
                                                CellSpacing="5" GridLines="Both" HeaderStyle-BackColor="#397CBB" HeaderStyle-ForeColor="White"
                                                HeaderStyle-Height="30px" OnRowCommand="grdDetail_RowCommand" OnRowDataBound="grdDetail_RowDataBound"
                                                Style="table-layout: fixed; float: left" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="lnk" CommandName="EditRecord"
                                                                Text="Edit" Visible="false"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument="lnk" CommandName="DeleteRecord"
                                                                Text="Delete" Visible="false"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle ForeColor="Black" Height="25px" Wrap="false" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel1" runat="server" align="left" BackColor="SeaShell" BorderColor="Maroon"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"
                                            Height="200px" ScrollBars="Vertical" Style="margin-left: 10px; float: left; margin-top: -15px;"
                                            Width="343px">
                                            <asp:GridView ID="grdtotal" runat="server" AutoGenerateColumns="true" CellPadding="5"
                                                CellSpacing="5" GridLines="Both" HeaderStyle-BackColor="#397CBB" HeaderStyle-ForeColor="White"
                                                HeaderStyle-Height="30px" Style="table-layout: fixed; float: left" Width="100%"
                                                OnRowDataBound="grdtotal_RowDataBound">
                                                <RowStyle ForeColor="Black" Height="25px" Wrap="false" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">Total
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="txt" Height="16px" AutoPostBack="true"
                                OnTextChanged="txtTotal_TextChanged" Style="text-align: right;" TabIndex="16"
                                Width="100px" Enabled="false"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtTotal" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtTotal" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Khajrat:
                            <asp:TextBox ID="txtKhajrat" runat="server" CssClass="txt" Height="16px" OnTextChanged="txtKhajrat_TextChanged"
                                Style="text-align: right; margin-left: 10px;" TabIndex="17" Width="100px" AutoPostBack="true"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtKhajrat" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtKhajrat" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Shub Amount
                            <asp:TextBox ID="txtShubAmount" runat="server" CssClass="txt" Height="16px" OnTextChanged="txtShubAmount_TextChanged"
                                Style="text-align: right;" TabIndex="18" Width="100px" AutoPostBack="true"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtShubAmount" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtShubAmount" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            P.Pol Amount
                            <asp:TextBox ID="txtPpolamount" runat="server" CssClass="txt" Height="16px" OnTextChanged="txtPpolamount_TextChanged"
                                Style="text-align: right;" AutoPostBack="true" TabIndex="19" Width="100px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtPpolamount" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtPpolamount" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Post Phone
                            <asp:TextBox ID="txtPostPhone" runat="server" CssClass="txt" Height="16px" OnTextChanged="txtPostPhone_TextChanged"
                                Style="text-align: right; margin-left: 10px;" TabIndex="20" Width="100px" AutoPostBack="true"></asp:TextBox>
                            Taxable Amount
                            <asp:TextBox ID="txtTaxableAmount" runat="server" CssClass="txt" Height="16px" OnTextChanged="txtTaxableAmount_TextChanged"
                                Style="text-align: right; margin-left: 10px;" TabIndex="21" Width="100px" AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>IGST Rate:
                            <asp:TextBox ID="txtIGSTRate" runat="server" CssClass="txt" Height="16px" OnTextChanged="txtIGSTRate_TextChanged"
                                Style="text-align: right; margin-left: 10px;" TabIndex="22" Width="100px" AutoPostBack="true"></asp:TextBox>
                            IGST Amount:
                            <asp:TextBox ID="txtIGSTAmount" runat="server" CssClass="txt" Height="16px" OnTextChanged="txtIGSTAmount_TextChanged"
                                Style="text-align: right; margin-left: 10px;" TabIndex="23" Width="100px" AutoPostBack="true"></asp:TextBox>
                            CGST Rate:
                            <asp:TextBox ID="txtCGSTRate" runat="server" CssClass="txt" Height="16px" OnTextChanged="txtCGSTRate_TextChanged"
                                Style="text-align: right; margin-left: 10px;" TabIndex="24" Width="100px" AutoPostBack="true"></asp:TextBox>
                            CGST Amount:
                            <asp:TextBox ID="txtCGSTAmount" runat="server" CssClass="txt" Height="16px" OnTextChanged="txtCGSTAmount_TextChanged"
                                Style="text-align: right; margin-left: 10px;" TabIndex="25" Width="100px" AutoPostBack="true"></asp:TextBox>
                            SGST Rate:
                            <asp:TextBox ID="txtSGSTRate" runat="server" CssClass="txt" Height="16px" OnTextChanged="txtSGSTRate_TextChanged"
                                Style="text-align: right; margin-left: 10px;" TabIndex="26" Width="100px" AutoPostBack="true"></asp:TextBox>
                            SGST Amount:
                            <asp:TextBox ID="txtSGSTAmount" runat="server" CssClass="txt" Height="16px" OnTextChanged="txtSGSTAmount_TextChanged"
                                Style="text-align: right; margin-left: 10px;" TabIndex="27" Width="100px" AutoPostBack="true"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;TCS%:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTCSRate" runat="Server"
                                CssClass="txt" Width="42px" AutoPostBack="true" OnTextChanged="txtTCSRate_TextChanged"
                                TabIndex="28" Style="text-align: right;" Height="16px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtTCSRate">
                            </ajax1:FilteredTextBoxExtender>
                            <asp:TextBox ID="txtTCSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="16px"
                                OnTextChanged="txtTCSAmt_TextChanged" Style="text-align: right;" Width="72px"
                                TabIndex="29" ReadOnly="true"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtTCSAmt" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            &nbsp;&nbsp;&nbsp; TDS%&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTDSRate" runat="Server"
                                CssClass="txt" Width="52px" AutoPostBack="true" OnTextChanged="txtTDSRate_TextChanged"
                                TabIndex="30" Style="text-align: right;" Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtTDSRate">
                            </ajax1:FilteredTextBoxExtender>
                            <asp:TextBox ID="txtTDSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                OnTextChanged="txtTDSAmt_TextChanged" ReadOnly="true" Style="text-align: right;"
                                Width="80px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtTDSAmt" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            &nbsp;&nbsp;&nbsp; Net Payable: &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTCSNet_Payable"
                                runat="Server" CssClass="txt" ReadOnly="true" TabIndex="31" Width="120px" Style="text-align: right;"
                                AutoPostBack="True" OnTextChanged="txtTCSNet_Payable_TextChanged" Height="16px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtTCSNet_Payable">
                            </ajax1:FilteredTextBoxExtender>
                            Roundoff:
                            <asp:TextBox ID="txtRoundoff" runat="server" CssClass="txt" Height="16px" OnTextChanged="txtRoundoff_TextChanged"
                                Style="text-align: right; margin-left: 10px;" TabIndex="20" Width="100px" AutoPostBack="true"></asp:TextBox>
                            Bill Amount:&nbsp;
                            <asp:TextBox ID="txtbillamnt" runat="server" AutoPostBack="true" CssClass="txt" Height="16px"
                                OnTextChanged="txtbillamnt_TextChanged" TabIndex="32" Width="100px" Enabled="false"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="Filteredtxtbillamnt" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtbillamnt" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            <asp:TextBox ID="txtTotalqty" runat="server" CssClass="txt" Height="16px" TabIndex="33"
                                Width="100px" Enabled="false" align="left"></asp:TextBox>
                            EwayBill No:&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtEwaybill" runat="Server" CssClass="txt" TabIndex="34" Width="200px"
                                Style="text-align: right;" AutoPostBack="false" Height="24px"></asp:TextBox>
                            EInvoice No:
                            <asp:TextBox ID="txteinvoiceno" runat="Server" CssClass="txt" TabIndex="35" Width="120px"
                                Style="text-align: right;" AutoPostBack="false" Height="24px" ></asp:TextBox>
                            ACKNo:
                            <asp:TextBox ID="txtackno" runat="Server" CssClass="txt" TabIndex="36" Width="150px"
                                Style="text-align: right;" AutoPostBack="False" Height="24px" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            TabIndex="37" ValidationGroup="save" Height="20px" OnClick="btnAdd_Click" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            TabIndex="38" ValidationGroup="add" Height="21px" OnClick="btnSave_Click" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            TabIndex="39" ValidationGroup="save" Height="22px" OnClick="btnEdit_Click" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            TabIndex="40" ValidationGroup="add" OnClientClick="Confirm()" Height="24px" OnClick="btnDelete_Click" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            TabIndex="41" Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" />
                        <asp:Button runat="server" ID="btnPrintSaleBill" Text="Print" CssClass="btnHelp"
                            Width="80px" Height="24px" OnClientClick="SB();" Visible="false" />
                        <asp:Button runat="server" ID="btnPrintSaleBill1" Text="Print" CssClass="btnHelp"
                            Width="80px" Height="24px" OnClientClick="SB1();" />
                        &nbsp;
                        <asp:Button runat="server" ID="btnGentare_EWayBill" Text="Gentare EWayBill" CssClass="btnHelp"
                            Width="130px" Height="24px" OnClientClick="GEway();" />
                        &nbsp;
                        <asp:Button ID="btnGenEinvoice" runat="server" Text="Generate EInvoice" CssClass="btnHelp"
                            Width="120px" ValidationGroup="save" OnClientClick="EInovice();" Height="24px" />
                    </td>
                    <td align="center">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            TabIndex="42" Height="24px" Width="90px" OnClick="btnFirst_Click" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                            TabIndex="43" Height="24px" Width="90px" OnClick="btnPrevious_Click" />
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                            TabIndex="44" Height="24px" Width="90px" OnClick="btnNext_Click" />
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                            TabIndex="45" Height="24px" Width="90px" OnClick="btnLast_Click" />
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:TextBox runat="server" ID="txtpartyMobno" Height="30px" Width="200px" />
                        <asp:Button Text="SMS" ID="btnpartysendsms" CommandName="sms" CssClass="btnHelp"
                            Height="30px" Width="80px" runat="server" OnCommand="btnpartysendsms_Click" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="80%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
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
                            <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                                Width="250px" Height="20px" AutoPostBack="true" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980" HeaderStyle-ForeColor="White"
                                    AllowPaging="true" PageSize="20" OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound"
                                    OnPageIndexChanging="grdPopup_PageIndexChanging" Style="table-layout: fixed;"
                                    OnSelectedIndexChanged="grdPopup_SelectedIndexChanged">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
