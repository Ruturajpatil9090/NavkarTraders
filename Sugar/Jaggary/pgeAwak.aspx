<%@ Page Title="Awak Book" Language="C#" AutoEventWireup="true" CodeFile="pgeAwak.aspx.cs"
    Inherits="Sugar_pgeAwakBook" MasterPageFile="~/MasterPage.master" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <%--    <script type="text/javascript">
           function SB() {
               var billno = document.getElementById('<%=txtdoc_no.ClientID %>').value;
               window.open('../Report/rptChqPrint.aspx?billno=' + billno)
           }</script>--%>
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


                if (hdnfClosePopupValue == "txtSupplier") {
                    document.getElementById("<%=txtSupplier.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblSupplier.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtSupplier.ClientID %>").focus();
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



                if (hdnfClosePopupValue == "txtBrand_Code") {
                    document.getElementById("<%=txtBrand_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBrandName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBrand_Code.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtHamali_Code") {
                    document.getElementById("<%=txtHamali_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblHamali_Name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtHamali_Code.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtPacking_Code") {
                    document.getElementById("<%=txtPacking_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblPacking_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtPacking_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtdoc_no") {
                    document.getElementById("<%=txtdoc_no.ClientID %>").value = "";
                    document.getElementById("<%=txtdoc_no.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtdoc_no.ClientID %>").disabled = false;
                    document.getElementById("<%=txtdoc_no.ClientID %>").focus();
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
                $("#<%=txtAddLess.ClientID %>").focus();
            }
            else if (e.keyCode == 113) {
                debugger;
                e.preventDefault();
                $("#<%=btnAdddetails.ClientID %>").focus();
            }

        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Awak   " Font-Names="verdana" ForeColor="White"
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
                        <td align="left">
                            Change No:
                            <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="20px"
                                TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                            &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%;">
                            Awak No:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtdoc_no" runat="Server" CssClass="txt" TabIndex="0" Width="50px"
                                Style="text-align: right;" AutoPostBack="True" Height="16px" OnTextChanged="txtdoc_no_TextChanged"></asp:TextBox>
                            &nbsp;<asp:Button ID="btntxtdoc_no" runat="server" Text="..." Width="80px" OnClick="btntxtdoc_no_Click"
                                CssClass="btnHelp" Height="16px" />
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%;">
                            Cash/Credit:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <%--         <asp:TextBox ID="txtCsCr" runat="Server" CssClass="txt" Width="20px" Style="text-align: right;"
                                AutoPostBack="True" Height="16px"></asp:TextBox>--%>
                            &nbsp;<asp:DropDownList ID="drpCsCr" runat="server" CssClass="txt" TabIndex="1" Width="100px"
                                Height="20px">
                                <asp:ListItem Text="Cash" Value="CS" />
                                <asp:ListItem Text="Credit" Value="CR" Selected="True" />
                            </asp:DropDownList>
                            &nbsp;&nbsp; Date:
                            <%--  <asp:TextBox ID="txtdoc_date" runat="Server" CssClass="txt" TabIndex="2" Width="90px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                Style="text-align: left;" Height="16px" AutoPostBack="True" OnTextChanged="txtdoc_date_TextChanged"></asp:TextBox>--%>
                            <%--<asp:TextBox ID="TextBox1" runat="Server" CssClass="txt" TabIndex="2" Width="90px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtdoc_date_TextChanged"
                                Height="24px"></asp:TextBox>--%>
                            <%--<asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="15px" Height="16px" />
                            <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtdoc_date"
                                PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>--%>
                            <asp:TextBox ID="txtdoc_date" runat="Server" CssClass="txt" TabIndex="2" Width="100px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtdoc_date_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" />
                            <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtdoc_date"
                                PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                            Bill No:
                            <asp:TextBox ID="txtBillNo" runat="Server" CssClass="txt" TabIndex="3" Width="50px"
                                Style="text-align: right;" Height="16px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%;">
                            Vehicle No:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtVehicleNo" runat="Server" CssClass="txt" TabIndex="4" Width="100px"
                                Style="text-align: right;" AutoPostBack="false" Height="16px"></asp:TextBox>
                            &nbsp;&nbsp; Supplier:
                            <asp:TextBox ID="txtSupplier" runat="server" CssClass="txt" Style="text-align: right;"
                                AutoPostBack="True" TabIndex="5" Height="16px" Width="50px" OnTextChanged="txtSupplier_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtSupplier" runat="server" Text="..." CssClass="btnHelp" Height="16px"
                                Width="10px" OnClick="btntxtSupplier_Click" />&nbsp;
                            <asp:Label ID="lblSupplier" runat="server" CssClass="lblName"></asp:Label>
                             <asp:Label ID="lblCustomerGSTStateCode" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr> 
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%;">
                            GST Rate: &nbsp;
                            <asp:TextBox ID="txtGST_RateCode" runat="server" AutoPostBack="True" CssClass="txt" Height="16px" onKeyDown="Focusbtn(event);" OnTextChanged="txtGST_RateCode_TextChanged" Style="text-align: right;" TabIndex="6" Width="50px"></asp:TextBox>
                            <asp:Button ID="btnGST_RateCode" runat="server" CssClass="btnHelp" Height="16px" OnClick="btnGST_RateCode_Click" Text="..." Width="10px" />
                            &nbsp;
                            <asp:Label ID="lblGST_RateCode" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="color: Blue;">Detail Information </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border-top: 3px solid black;">Customer Code: &nbsp;
                                <asp:TextBox ID="txtCustCode" runat="server" AutoPostBack="True" CssClass="txt" Height="16px" onKeyDown="Focusbtn(event);" OnTextChanged="txtCustCode_TextChanged" Style="text-align: right;" TabIndex="7" Width="50px"></asp:TextBox>
                                <asp:Button ID="btntxtCustCode" runat="server" CssClass="btnHelp" Height="16px" OnClick="btntxtCustomer_Click" Text="..." Width="10px" />
                                &nbsp;
                                <asp:Label ID="lblCustomer" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">Item code:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtItemCode" runat="server" AutoPostBack="True" CssClass="txt" Height="16px" OnTextChanged="txtItemCode_TextChanged" Style="text-align: right;" TabIndex="8" Width="50px"></asp:TextBox>
                                <asp:Button ID="btntxtItemCode" runat="server" CssClass="btnHelp" Height="16px" OnClick="btntxtItem_Click" Text="..." Width="10px" />
                                &nbsp;
                                <asp:Label ID="lblItem" runat="server" CssClass="lblName"></asp:Label>
                                Brand code:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtBrand_Code" runat="server" AutoPostBack="True" CssClass="txt" Height="16px" OnTextChanged="txtBrand_Code_TextChanged" Style="text-align: right;" TabIndex="9" Width="50px"></asp:TextBox>
                                <asp:Button ID="btntxtBrand_Code" runat="server" CssClass="btnHelp" Height="16px" OnClick="btntxtBrand_Code_Click" Text="..." Width="10px" />
                                &nbsp;
                                <asp:Label ID="lblBrandName" runat="server" CssClass="lblName"></asp:Label>
                                Poch Rate  <asp:TextBox ID="txtpochrate" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtpochrate_TextChanged" Style="text-align: right;" TabIndex="10" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="Filteredtxtpochrate" runat="server" FilterType="Numbers,Custom" TargetControlID="txtpochrate" ValidChars=".">

                                </ajax1:FilteredTextBoxExtender>
                                 Frieght Rate  <asp:TextBox ID="txtfrieghtRate" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtfrieghtRate_TextChanged" Style="text-align: right;" TabIndex="11" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtfrieghtRate" runat="server" FilterType="Numbers,Custom" TargetControlID="txtfrieghtRate" ValidChars=".">

                                </ajax1:FilteredTextBoxExtender>

                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">B. No:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtBNo" runat="Server" CssClass="txt" Height="16px" Style="text-align: right;" TabIndex="12" Width="70px"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Qty:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtQty" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtQty_TextChanged" Style="text-align: right;" TabIndex="13" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtQty" runat="server" FilterType="Numbers,Custom" TargetControlID="txtQty" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Net Wt:&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtNetWt" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtNetWt_TextChanged" Style="text-align: right;" TabIndex="14" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtNetWt" runat="server" FilterType="Numbers,Custom" TargetControlID="txtNetWt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">P.Rate:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtPRate" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" onKeyDown="Focusbtn(event);" OnTextChanged="txtPRate_TextChanged" Style="text-align: right;" TabIndex="15" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtPRate" runat="server" FilterType="Numbers,Custom" TargetControlID="txtPRate" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; P.Amount:&nbsp;<asp:TextBox ID="txtPAmt" runat="Server" AutoPostBack="false" CssClass="txt" Height="16px" ReadOnly="true" Style="text-align: right;" TabIndex="16" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtPAmt" runat="server" FilterType="Numbers,Custom" TargetControlID="txtPAmt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; S.Rate:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtSRate" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" onKeyDown="Focusbtn(event);" OnTextChanged="txtSRate_TextChanged" Style="text-align: right;" TabIndex="17" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtSRate" runat="server" FilterType="Numbers,Custom" TargetControlID="txtSRate" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp; S.Amount:&nbsp;<asp:TextBox ID="txtSAmt" runat="Server" AutoPostBack="false" CssClass="txt" Height="16px" ReadOnly="true" Style="text-align: right;" TabIndex="18" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtSAmt" runat="server" FilterType="Numbers,Custom" TargetControlID="txtSAmt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Commishion %:&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtPer" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" onKeyDown="Focusbtn(event);" OnTextChanged="txtPer_TextChanged" Style="text-align: right;" TabIndex="19" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtPer" runat="server" FilterType="Numbers,Custom" TargetControlID="txtPer" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp; Comm Amt:&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtcomm_amnt" runat="Server" AutoPostBack="false" CssClass="txt" Height="16px" ReadOnly="true" Style="text-align: right;" TabIndex="20" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="Filteredtxtcomm_amnt" runat="server" FilterType="Numbers,Custom" TargetControlID="txtcomm_amnt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">Hamali code:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtHamali_Code" runat="server" AutoPostBack="True" CssClass="txt" Height="16px" onKeyDown="Focusbtn(event);" OnTextChanged="txtHamali_Code_TextChanged" Style="text-align: right;" TabIndex="21" Width="50px"></asp:TextBox>
                                <asp:Button ID="btntxtHamali_Code" runat="server" CssClass="btnHelp" Height="16px" OnClick="btntxtHamali_Code_Click" Text="..." Width="10px" />
                                &nbsp;
                                <asp:Label ID="lblHamali_Name" runat="server" CssClass="lblName"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Hamali Amount
                                <asp:TextBox ID="txtHamali_amnt" runat="server" AutoPostBack="True" CssClass="txt" Height="16px" onKeyDown="Focusbtn(event);" OnTextChanged="txtHamali_amnt_TextChanged" Style="text-align: right;" TabIndex="22" Width="50px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtHamali_amnt" runat="server" FilterType="Numbers,Custom" TargetControlID="txtHamali_amnt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                Packing code:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtPacking_Code" runat="server" AutoPostBack="True" CssClass="txt" Height="16px" onKeyDown="Focusbtn(event);" OnTextChanged="txtPacking_Code_TextChanged" Style="text-align: right;" TabIndex="23" Width="50px"></asp:TextBox>
                                <asp:Button ID="btntxtPacking_Code" runat="server" CssClass="btnHelp" Height="16px" OnClick="btntxtPacking_Code_Click" Text="..." Width="10px" />
                                &nbsp;
                                <asp:Label ID="lblPacking_name" runat="server" CssClass="lblName"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Packing Amount:
                                <asp:TextBox ID="txtPacking_amnt" runat="server" AutoPostBack="True" CssClass="txt" Height="16px" onKeyDown="Focusbtn(event);" OnTextChanged="txtPacking_amnt_TextChanged" Style="text-align: right;" TabIndex="24" Width="50px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtPacking_amnt" runat="server" FilterType="Numbers,Custom" TargetControlID="txtPacking_amnt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">Other %:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtOther_Per" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtOther_Per_TextChanged" Style="text-align: right;" TabIndex="25" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtOther_Per" runat="server" FilterType="Numbers,Custom" TargetControlID="txtOther_Per" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Other % Amount:&nbsp;<asp:TextBox ID="txtOther_Per_Amnt" runat="Server" AutoPostBack="false" CssClass="txt" Height="16px" Style="text-align: right;" TabIndex="26" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtOther_Per_Amnt" runat="server" FilterType="Numbers,Custom" TargetControlID="txtOther_Per_Amnt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Other +/-:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtOther_PM" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtOther_PM_TextChanged" Style="text-align: right;" TabIndex="27" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtOther_PM" runat="server" FilterType="Numbers,Custom" TargetControlID="txtOther_PM" ValidChars=".,-">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Levi&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtLevidetails" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtLevidetails_TextChanged" ReadOnly="false" Style="text-align: right;" TabIndex="28" Width="50px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtLevidetails" runat="server" FilterType="Numbers,Custom" TargetControlID="txtLevidetails" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtLevidetailsAmt" runat="server" AutoPostBack="True" CssClass="txt" Height="16px" OnTextChanged="txtLevidetailsAmt_TextChanged" ReadOnly="true" Style="text-align: right;" TabIndex="29" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtLevidetailsAmt" runat="server" FilterType="Numbers,Custom" TargetControlID="txtLevidetailsAmt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Insurance&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtInsurance" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtInsurance_TextChanged" Style="text-align: right;" TabIndex="30" Width="50px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtInsurance" runat="server" FilterType="Numbers,Custom" TargetControlID="txtInsurance" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtInsuranceAmt" runat="server" AutoPostBack="True" CssClass="txt" Height="16px" OnTextChanged="txtInsuranceAmt_TextChanged" ReadOnly="true" Style="text-align: right;" TabIndex="31" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtInsuranceAmt" runat="server" FilterType="Numbers,Custom" TargetControlID="txtInsuranceAmt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">Gross Sale Rate:&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtNetRate" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtNetRate_TextChanged" Style="text-align: right;" TabIndex="32" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtNetRate" runat="server" FilterType="Numbers,Custom" TargetControlID="txtNetRate" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp; Gross&nbsp; Amount:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtgross_sale_amnt" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtgross_sale_amnt_TextChanged" Style="text-align: right;" TabIndex="33" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="Filteredtxtgross_sale_amnt" runat="server" FilterType="Numbers,Custom" TargetControlID="txtgross_sale_amnt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sr:&nbsp;
                                <asp:TextBox ID="txtSr" runat="Server" AutoPostBack="false" CssClass="txt" Height="16px" Style="text-align: right;" TabIndex="34" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtSr" runat="server" FilterType="Numbers,Custom" TargetControlID="txtSr" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Shub %:&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtShub_Rate" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtShub_Rate_TextChanged" Style="text-align: right;" TabIndex="35" Width="31px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtShub_Rate" runat="server" FilterType="Numbers,Custom" TargetControlID="txtShub_Rate" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp; ShubAmt:&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtShub_Amnt" runat="Server" AutoPostBack="false" CssClass="txt" Height="16px" Style="text-align: right;" TabIndex="36" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtShub_Amnt" runat="server" FilterType="Numbers,Custom" TargetControlID="txtShub_Amnt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp; P.Pol :&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="TxtPanjar_Rate" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="TxtPanjar_Rate_TextChanged" Style="text-align: right;" TabIndex="37" Width="41px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredTxtPanjar_Rate" runat="server" FilterType="Numbers,Custom" TargetControlID="TxtPanjar_Rate" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp; P.Amt:&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtPanjar_Amnt" runat="Server" AutoPostBack="false" CssClass="txt" Height="16px" Style="text-align: right;" TabIndex="38" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtPanjar_Amnt" runat="server" FilterType="Numbers,Custom" TargetControlID="txtPanjar_Amnt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp; </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnAdddetails" runat="server" CssClass="btnHelp" Height="16px" OnClick="btnAdddetails_Click" TabIndex="39" Text="ADD" Width="50px" />
                                &nbsp;
                                <asp:Button ID="btnClosedetails" runat="server" CssClass="btnHelp" Height="16px" OnClick="btnClosedetails_Click" TabIndex="40" Text="Reset" Width="50px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">
                                <br />
                                <br />
                                <div style="width: 100%; position: relative; vertical-align: top; margin-top: -20px;">
                                    <asp:UpdatePanel ID="upGrid" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlgrdDetail" runat="server" align="left" BackColor="SeaShell" BorderColor="Maroon" BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" Font-Names="Verdana" Font-Size="11px" Height="200px" ScrollBars="Both" Style="margin-left: 00px;
                                            float: left;" Width="1400px">
                                                <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" CellPadding="5" CellSpacing="5" GridLines="Both" HeaderStyle-BackColor="#397CBB" HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" OnRowCommand="grdDetail_RowCommand" OnRowDataBound="grdDetail_RowDataBound" Style="table-layout: fixed; float: left" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="lnk" CommandName="EditRecord" Text="Edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument="lnk" CommandName="DeleteRecord" Text="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle ForeColor="Black" Height="25px" Wrap="false" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">Purchase Value:&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="lblPurchaseValue" runat="server" AutoPostBack="true" Enabled="false" OnTextChanged="lblPurchaseValue_TextChanged" ReadOnly="true" />
                                <%--<asp:Label ID="lblPurchaseValue" runat="server" CssClass="lblName"></asp:Label>--%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Sale Value:&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="lblSaleValue" runat="server" AutoPostBack="true" Enabled="false" OnTextChanged="lblSaleValue_TextChanged" ReadOnly="true" />
                                <%--<asp:Label ID="lblSaleValue" runat="server" CssClass="lblName"></asp:Label>--%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Stock Value:&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="lblStockValue" runat="server" AutoPostBack="true" Enabled="false" OnTextChanged="lblStockValue_TextChanged" ReadOnly="true" />
                                <%--<asp:Label ID="lblStockValue" runat="server" CssClass="lblName"></asp:Label>--%></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">Add Less:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtAddLess" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtAddLess_TextChanged" Style="text-align: right;" TabIndex="41" Width="100px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtAddLess" runat="server" FilterType="Numbers,Custom" TargetControlID="txtAddLess" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Net Exps:&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblNetExps" runat="server" CssClass="lblName"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">Cess:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtCess" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtCess_TextChanged" Style="text-align: right;" TabIndex="42" Width="100px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtCess" runat="server" FilterType="Numbers,Custom" TargetControlID="txtCess" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Net Qty:&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblqty" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">Super Cost:&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtSuperCost" runat="server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtSuperCost_TextChanged" Style="text-align: right;" TabIndex="43" Width="100px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtSuperCost" runat="server" FilterType="Numbers,Custom" TargetControlID="txtSuperCost" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">Khajrat:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtKhajrat" runat="server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtKhajrat_TextChanged" Style="text-align: right;" TabIndex="44" Width="100px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtKhajrat" runat="server" FilterType="Numbers,Custom" TargetControlID="txtKhajrat" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Levi:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtLevi" runat="server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtLevi_TextChanged" Style="text-align: right;" TabIndex="45" Width="100px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtLevi" runat="server" FilterType="Numbers,Custom" TargetControlID="txtLevi" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">Adat:&nbsp;&nbsp;
                                <asp:TextBox ID="txtAdat" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtAdat_TextChanged" Style="text-align: right;" TabIndex="46" Width="100px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtAdat" runat="server" FilterType="Numbers,Custom" TargetControlID="txtAdat" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp; &nbsp; &nbsp; Taxable Amount:
                                <asp:TextBox ID="txtTaxableAmount" runat="server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtTaxableAmount_TextChanged" Style="text-align: right; margin-left: 10px;" TabIndex="47" Width="100px"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp; IGST Rate:
                                <asp:TextBox ID="txtIGSTRate" runat="server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtIGSTRate_TextChanged" Style="text-align: right; margin-left: 10px;" TabIndex="48" Width="100px"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp; IGST Amount:
                                <asp:TextBox ID="txtIGSTAmount" runat="server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtIGSTAmount_TextChanged" Style="text-align: right; margin-left: 10px;" TabIndex="49" Width="100px"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp; CGST Rate:
                                <asp:TextBox ID="txtCGSTRate" runat="server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtCGSTRate_TextChanged" Style="text-align: right; margin-left: 10px;" TabIndex="50" Width="100px"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp; CGST Amount:
                                <asp:TextBox ID="txtCGSTAmount" runat="server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtCGSTAmount_TextChanged" Style="text-align: right; margin-left: 10px;" TabIndex="51" Width="100px"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp; SGST Rate:
                                <asp:TextBox ID="txtSGSTRate" runat="server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtSGSTRate_TextChanged" Style="text-align: right; margin-left: 10px;" TabIndex="52" Width="100px"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp; SGST Amount:
                                <asp:TextBox ID="txtSGSTAmount" runat="server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtSGSTAmount_TextChanged" Style="text-align: right; margin-left: 10px;" TabIndex="53" Width="100px"></asp:TextBox>
                                &nbsp;&nbsp;TDS:
                                <asp:TextBox ID="txtTDSper" runat="server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtTDSper_TextChanged" TabIndex="54" Width="50px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtTDSper" runat="server" FilterType="Numbers,Custom" TargetControlID="txtTDSper" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;
                                <asp:TextBox ID="txtTDSAmt" runat="server" CssClass="txt" Height="16px" TabIndex="55" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtTDSAmt" runat="server" FilterType="Numbers,Custom" TargetControlID="txtTDSAmt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                <%-- <asp:button id="btnclosedetails" runat="server" text="reset" cssclass="btnhelp" width="50px"
                                height="16px" tabindex="16" />--%></td>
                        </tr>
                        <tr>
                            <td>&nbsp;&nbsp;&nbsp;TCS%:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTCSRate" runat="Server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtTCSRate_TextChanged" Style="text-align: right;" TabIndex="56" Width="42px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers,Custom" TargetControlID="txtTCSRate" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtTCSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="16px" OnTextChanged="txtTCSAmt_TextChanged" Style="text-align: right;" TabIndex="57" Width="72px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers,Custom" TargetControlID="txtTCSAmt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                Bill Amount:&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtbillamnt" runat="server" AutoPostBack="true" CssClass="txt" Height="16px" OnTextChanged="txtbillamnt_TextChanged" ReadOnly="true" TabIndex="58" Width="70px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="Filteredtxtbillamnt" runat="server" FilterType="Numbers,Custom" TargetControlID="txtbillamnt" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp; Net Payable: &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTCSNet_Payable" runat="Server" AutoPostBack="True" CssClass="txt" Height="16px" OnTextChanged="txtTCSNet_Payable_TextChanged" ReadOnly="true" Style="text-align: right;" TabIndex="59" Width="120px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers,Custom" TargetControlID="txtTCSNet_Payable" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                    </tr>
                </table>
            </asp:Panel>
            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            TabIndex="58" ValidationGroup="save" Height="24px" OnClick="btnAdd_Click" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            TabIndex="59" ValidationGroup="add" Height="24px" OnClick="btnSave_Click" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            TabIndex="60" ValidationGroup="save" Height="24px" OnClick="btnEdit_Click" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            TabIndex="61" ValidationGroup="add" OnClientClick="Confirm()" Height="24px" OnClick="btnDelete_Click" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            TabIndex="62" Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" />
                        &nbsp;
                        <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="btnHelp" Width="90px"
                            TabIndex="63" Height="24px" ValidationGroup="save" OnClientClick="printChq()"
                            Visible="false" />
                        &nbsp;
                    </td>
                    <td align="center">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            Height="24px" Width="90px" OnClick="btnFirst_Click" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                            Height="24px" Width="90px" OnClick="btnPrevious_Click" />
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                            Height="24px" Width="90px" OnClick="btnNext_Click" />
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                            Height="24px" Width="90px" OnClick="btnLast_Click" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="80%"
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
