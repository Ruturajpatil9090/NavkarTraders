<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeGstr3B.aspx.cs" Inherits="pgeGstr3B" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                if (hdnfClosePopupValue == "txtEdiDoc_No") {

                    document.getElementById("<%=txtEdiDoc_No.ClientID %>").value = "";
                    document.getElementById("<%=txtEdiDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEdiDoc_No.ClientID %>").focus();
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
        function chanegno(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtEdiDoc_No";
                $("#<%=hdnfClosePopup.ClientID %> ").val(edi);
                $("#<%=btnSearch.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtEdiDoc_No", "TextChanged");
            }
        }
        function Focusbtn(e) {
            debugger;
            if (e.keyCode == 13) {

                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Gstr3B " Font-Names="verdana" ForeColor="White"
                Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <table width="90%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" />
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
                <table style="width: 100%;" align="center" cellpadding="5" cellspacing="6">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                        <tr>
                            <td align="right">
                                Change_No
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtEdiDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEdiDoc_No_TextChanged"
                                    onKeyDown="chanegno(event);"></asp:TextBox>
                            </td>
                            <td align="right">
                                Entry No
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtDoc_No" runat="Server" CssClass="txt" TabIndex="2"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDoc_No_TextChanged"></asp:TextBox>
                                <asp:Button Width="80px" Height="24px" ID="btntxtDoc_No" runat="server" Text="..."
                                    OnClick="btntxtDoc_No_Click" CssClass="btnHelp" onkeydown="closepopup(event);" />
                            </td>
                            <td align="right">
                                Date
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtDoc_Date" runat="Server" CssClass="txt" TabIndex="3"
                                    Width="90px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtDoc_Date_TextChanged"
                                    onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                                <td>
                                    <asp:Image ID="imgcalendertxtDoc_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                        Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtDoc_Date"
                                            runat="server" TargetControlID="txtDoc_Date" PopupButtonID="imgcalendertxtDoc_Date"
                                            Format="dd/MM/yyyy">
                                        </ajax1:CalendarExtender>
                                </td>
                            </td>
                            <td align="right">
                                From Date
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtFrom_Date" runat="Server" CssClass="txt" TabIndex="4"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtFrom_Date_TextChanged"
                                    onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                                <asp:Image ID="imgcalendertxtFrom_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                    Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtFrom_Date"
                                        runat="server" TargetControlID="txtFrom_Date" PopupButtonID="imgcalendertxtFrom_Date"
                                        Format="dd/MM/yyyy">
                                    </ajax1:CalendarExtender>
                            </td>
                            <td align="right">
                                To Date
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtTo_Date" runat="Server" CssClass="txt" TabIndex="5"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTo_Date_TextChanged"
                                    onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"
                                    cellpadding="4" cellspacing="4"></asp:TextBox>
                                <asp:Image ID="imgcalendertxtTo_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                    Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtTo_Date"
                                        runat="server" TargetControlID="txtTo_Date" PopupButtonID="imgcalendertxtTo_Date"
                                        Format="dd/MM/yyyy">
                                    </ajax1:CalendarExtender>
                            </td>
                            </td>
                            <td align="left">
                                <asp:Button Width="80px" Height="24px" ID="txtDisplay" runat="server" Text="Display"
                                    OnClick="btntxtDisplay_Click" CssClass="btnHelp" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Sale Detail Taxable
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtSale_Taxable" runat="Server" CssClass="txt" TabIndex="6"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSale_Taxable_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Igst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtSale_Igst" runat="Server" CssClass="txt" TabIndex="7"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSale_Igst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Cgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtSale_Cgst" runat="Server" CssClass="txt" TabIndex="8"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSale_Cgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Sgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtSale_Sgst" runat="Server" CssClass="txt" TabIndex="9"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSale_Sgst_TextChanged"></asp:TextBox>
                            </td>
                        </tr>

                         <tr>
                            <td align="right">
                               Retail Sale Detail Taxable
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtretail_taxable" runat="Server" CssClass="txt" TabIndex="6"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtretail_taxable_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Igst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtretailsaleIgst" runat="Server" CssClass="txt" TabIndex="7"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtretailsaleIgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Cgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtretailsaleCgst" runat="Server" CssClass="txt" TabIndex="8"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtretailsaleCgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Sgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtretailsaleSgst" runat="Server" CssClass="txt" TabIndex="9"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtretailsaleSgst_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                              Less-Sales Ret Credit Note To Customer
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtCNCTaxableAmount" runat="Server" CssClass="txt" TabIndex="10"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCNCTaxableAmount_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Igst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtCNCIgst" runat="Server" CssClass="txt" TabIndex="11"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCNCIgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Cgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtCNCCgst" runat="Server" CssClass="txt" TabIndex="12"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCNCCgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Sgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtCNCSgst" runat="Server" CssClass="txt" TabIndex="13"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCNCSgst_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                             Add Debit Note To Customar
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtDNCTaxable" runat="Server" CssClass="txt" TabIndex="14"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDNCTaxable_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Igst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtDNCIgst" runat="Server" CssClass="txt" TabIndex="15"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDNCIgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Cgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtDNCCgst" runat="Server" CssClass="txt" TabIndex="16"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDNCCgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Sgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtDNCSgst" runat="Server" CssClass="txt" TabIndex="17"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDNCSgst_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Export Taxable
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtExport_Amount" runat="Server" CssClass="txt" TabIndex="18"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtExport_Amount_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Reverse charge Taxable
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtRC_Taxable" runat="Server" CssClass="txt" TabIndex="19"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtRC_Taxable_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Igst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtRC_IGST" runat="Server" CssClass="txt" TabIndex="20"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtRC_IGST_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Cgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtRC_CGCT" runat="Server" CssClass="txt" TabIndex="21"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtRC_CGCT_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Sgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtRC_Sgst" runat="Server" CssClass="txt" TabIndex="22"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtRC_Sgst_TextChanged"></asp:TextBox>
                            </td>
                            </td>
                            <td align="right">
                                Narration
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox Height="50px" ID="TextBox1" runat="Server" CssClass="txt" TabIndex="23"
                                    Width="300px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtRC_Sgst_TextChanged"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Other Reverse Charge
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtO_RC_Taxable" runat="Server" CssClass="txt" TabIndex="24"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtO_RC_Taxable_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                ORC Igst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtO_RC_Igst" runat="Server" CssClass="txt" TabIndex="25"
                                    Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtO_RC_Igst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                ORC Cgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtO_RC_Cgst" runat="Server" CssClass="txt" TabIndex="26"
                                    Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtO_RC_Cgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                ORC Sgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtO_RC_Sgst" runat="Server" CssClass="txt" TabIndex="27"
                                    Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtO_RC_Sgst_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Reverse Charge
                            </td>
                            <td align="left">
                            </td>
                            <td align="right">
                                IGST
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtIrc_Igst" runat="Server" CssClass="txt" TabIndex="28"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtIrc_Igst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                RC Cgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtIrc_Cgst" runat="Server" CssClass="txt" TabIndex="29"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtIrc_Cgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                RC Sgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtIrc_Sgst" runat="Server" CssClass="txt" TabIndex="30"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtIrc_Sgst_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Purchase Detail
                            </td>
                            <td align="right">
                            </td>
                            <td align="right">
                                IGST
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtPurchase_Igst" runat="Server" CssClass="txt" TabIndex="31"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPurchase_Igst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                PD Cgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtPurchase_Cgst" runat="Server" CssClass="txt" TabIndex="32"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPurchase_Cgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                PD Sgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtPuchase_Sgst" runat="Server" CssClass="txt" TabIndex="33"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPuchase_Sgst_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                          <tr>
                            <td align="right">
                               Add Credit Note to Supplier
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtPCNSTaxable" runat="Server" CssClass="txt" TabIndex="34"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPCNSTaxable_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Igst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtPCNSIgst" runat="Server" CssClass="txt" TabIndex="35"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPCNSIgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Cgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtPCNSCgst" runat="Server" CssClass="txt" TabIndex="36"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPCNSCgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Sgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtPCNSSgst" runat="Server" CssClass="txt" TabIndex="37"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPCNSSgst_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                          <tr>
                            <td align="right">
                              Less Purchase Return Debit Note 18%
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtPCNSTaxableA" runat="Server" CssClass="txt" TabIndex="38"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPCNSTaxableA_TextChanged"></asp:TextBox>
                            
                            <td align="right">
                                Igst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtPRCNSIgst" runat="Server" CssClass="txt" TabIndex="39"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPRCNSIgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Cgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtPRCNSCgst" runat="Server" CssClass="txt" TabIndex="40"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPRCNSCgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Sgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtPRCNSSgst" runat="Server" CssClass="txt" TabIndex="41"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPRCNSSgst_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                            <tr>
                            <td align="right">
                             Other Inputs
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtotherInput" runat="Server" CssClass="txt" TabIndex="42"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtotherInput_TextChanged"></asp:TextBox>
                            
                            <td align="right">
                                Igst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtOIIgst" runat="Server" CssClass="txt" TabIndex="43"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtOIIgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Cgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtOICgst" runat="Server" CssClass="txt" TabIndex="44"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtOICgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                Sgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtOISgst" runat="Server" CssClass="txt" TabIndex="45"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtOISgst_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Ineligible ITC
                            </td>
                            <td align="right">
                            </td>
                            <td align="right">
                                IGST
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtIn_RC_Igst" runat="Server" CssClass="txt" TabIndex="46"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtIn_RC_Igst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                IN Cgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtIn_RC_Cgst" runat="Server" CssClass="txt" TabIndex="47"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtIn_RC_Cgst_TextChanged"></asp:TextBox>
                            </td>
                            <td align="right">
                                IN Sgst
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtIn_RC_Sgst" runat="Server" CssClass="txt" TabIndex="48"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtIn_RC_Sgst_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Intra State Non GST supply
                            </td>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtIntra_State_Supply" runat="Server" CssClass="txt"
                                    TabIndex="49" Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtIntra_State_Supply_TextChanged"
                                    onKeyDown="Focusbtn(event);"></asp:TextBox>
                                <asp:Button Width="118px" Height="24px" ID="btntxtShow3B" runat="server" Text="Show3B"
                                    OnClick="btntxtShow3B_Click" CssClass="btnHelp" />
                            </td>
                        </tr>
                </table>
            </asp:Panel>
            <asp:Panel onkeydown="closepopup(event);" ID="pnlPopup" runat="server" Width="70%"
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
                            <asp:TextBox onkeydown="SelectFirstRow(event);" ID="txtSearchText" runat="server"
                                Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button onkeydown="closepopup(event);" ID="btnSearch" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView Font-Bold="true" CssClass="select" AllowPaging="true" PageSize="20"
                                    OnPageIndexChanging="grdPopup_PageIndexChanging" ID="grdPopup" runat="server"
                                    AutoGenerateColumns="true" Width="100%" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
