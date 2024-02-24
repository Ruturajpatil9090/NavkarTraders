<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeJaggryCompanyparameter.aspx.cs" Inherits="Sugar_Master_pgeJaggryCompanyparameter" %>

 <%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                var hdnfBranch1Code = document.getElementById("<%= hdnfBranch1Code.ClientID %>").value;
                var grid = document.getElementById("<%= grdPopup.ClientID %>");
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";

                if (hdnfClosePopupValue == "txtbrokrage_ac") {
                    document.getElementById("<%=txtbrokrage_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBrokrage_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtservice_charge_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtservice_charge_ac") {
                    document.getElementById("<%=txtservice_charge_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblService_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtcommission_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtcommission_ac") {
                    document.getElementById("<%=txtcommission_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblCommission_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtquality_diff_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtquality_diff_ac") {
                    document.getElementById("<%=txtquality_diff_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblQuality_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtbank_commission_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtbank_commission_ac") {
                    document.getElementById("<%=txtbank_commission_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBankcommission_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtinterest_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtinterest_ac") {
                    document.getElementById("<%=txtinterest_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblInterest_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txttransport_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txttransport_ac") {
                    document.getElementById("<%=txttransport_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblTransport_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtsale_dalali_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtsale_dalali_ac") {
                    document.getElementById("<%=txtsale_dalali_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblSale_dalali_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtloading_charge_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtloading_charge_ac") {
                    document.getElementById("<%=txtloading_charge_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblLoading_charge_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtmotor_freight_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtmotor_freight_ac") {
                    document.getElementById("<%=txtmotor_freight_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblMotor_freight_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtpostage_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtpostage_ac") {
                    document.getElementById("<%=txtpostage_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblPostage_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtother_amount_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtother_amount_ac") {
                    document.getElementById("<%=txtother_amount_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblOther_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtSaleReturn.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtSaleReturn") {
                    document.getElementById("<%=txtSaleReturn.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblSaleReturnAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtReturnSale.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtReturnSale") {
                    document.getElementById("<%=txtReturnSale.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblReturnSaleAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtself_ac.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtself_ac") {
                    document.getElementById("<%=txtself_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblSelf_ac.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtexcise_rate.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBranch1") {
                    document.getElementById("<%=txtBranch1.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=lblBranch1Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                }
                if (hdnfClosePopupValue == "txtBranch2") {
                    document.getElementById("<%=txtBranch2.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=lblBranch2Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                }

                if (hdnfClosePopupValue == "txtGstStateCode") {
                    document.getElementById("<%=txtGstStateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtGstStateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGstStateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtCGSTAc") {
                    document.getElementById("<%=txtCGSTAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtCGSTAcName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtCGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSGSTAc") {
                    document.getElementById("<%=txtSGSTAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtSGSTAcName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtSGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtIGSTAc") {
                    document.getElementById("<%=txtIGSTAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtIGSTAcName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtIGSTAc.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtPurchaseCGSTAc") {
                    document.getElementById("<%=txtPurchaseCGSTAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtPurchaseCGSTAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtPurchaseCGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurchaseSGSTAc") {
                    document.getElementById("<%=txtPurchaseSGSTAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtPurchaseSGSTAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtPurchaseSGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurchaseIGSTAc") {
                    document.getElementById("<%=txtPurchaseIGSTAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtPurchaseIGSTAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtPurchaseIGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtCGST_RCM_Ac") {
                    document.getElementById("<%=txtCGST_RCM_Ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblCGST_RCM_Ac.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtCGST_RCM_Ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSGST_RCM_Ac") {
                    document.getElementById("<%=txtSGST_RCM_Ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblSGST_RCM_Ac.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtSGST_RCM_Ac.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtIGST_RCM_Ac") {
                    document.getElementById("<%=txtIGST_RCM_Ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblIGST_RCM_Ac.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtIGST_RCM_Ac.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtPurchaseTCSAc") {
                    document.getElementById("<%=txtPurchaseTCSAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtPurchaseTCSAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtPurchaseTCSAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSaleTCSAc") {
                    document.getElementById("<%=txtSaleTCSAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtSaleTCSAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtSaleTCSAc.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtJaggary_SaleTCSAc") {
                    document.getElementById("<%=txtJaggary_SaleTCSAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtJaggary_SaleTCSAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtJaggary_SaleTCSAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtJaggary_PurchaseTCSAc") {
                    document.getElementById("<%=txtJaggary_PurchaseTCSAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtJaggary_PurchaseTCSAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtJaggary_PurchaseTCSAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurchaseTDSAc") {
                    document.getElementById("<%=txtPurchaseTDSAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtPurchaseTDSAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtPurchaseTDSAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSaleTDSAc") {
                    document.getElementById("<%=txtSaleTDSAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtSaleTDSAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtSaleTDSAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtJaggrySaleTDSAc") {
                    document.getElementById("<%=txtJaggrySaleTDSAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtJaggrySaleTDSAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtJaggrySaleTDSAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtJaggary_GSTCode") {
                    document.getElementById("<%=txtJaggary_GSTCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblJaggary_GSTCode.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtJaggary_GSTCode.ClientID %>").focus();
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text=" Jaggry  Company Parameters   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdnfBranch1Code" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table width="80%" align="left" cellspacing="2px">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                        <tr>
                            <td align="left" style="width: 20%;">
                                Brokrage A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtbrokrage_ac" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtbrokrage_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtbrokrage_ac" runat="server" Text="..." OnClick="btntxtbrokrage_ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblBrokrage_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                GST State Code:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtGstStateCode" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtGstStateCode_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtGstStateCode" runat="server" Text="..." OnClick="btntxtGstStateCode_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtGstStateName" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Service Charge:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtservice_charge_ac" runat="Server" CssClass="txt" TabIndex="2"
                                    Height="24px" Width="80px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtservice_charge_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtservice_charge_ac" runat="server" Text="..." OnClick="btntxtservice_charge_ac_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblService_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Sale CGST A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtCGSTAc" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtCGSTAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtCGSTAc" runat="server" Text="..." OnClick="btntxtCGSTAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtCGSTAcName" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Commission A/c:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtcommission_ac" runat="Server" CssClass="txt" TabIndex="4" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtcommission_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtcommission_ac" runat="server" Text="..." OnClick="btntxtcommission_ac_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblCommission_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Sale SGST A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtSGSTAc" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtSGSTAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtSGSTAc" runat="server" Text="..." OnClick="btntxtSGSTAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtSGSTAcName" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Quality Diff A/c:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtquality_diff_ac" runat="Server" CssClass="txt" TabIndex="6" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtquality_diff_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtquality_diff_ac" runat="server" Text="..." OnClick="btntxtquality_diff_ac_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblQuality_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Sale IGST A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtIGSTAc" runat="Server" CssClass="txt" TabIndex="7" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtIGSTAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtIGSTAc" runat="server" Text="..." OnClick="btntxtIGSTAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtIGSTAcName" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Bank Commission A/c:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtbank_commission_ac" runat="Server" CssClass="txt" TabIndex="8"
                                    Height="24px" Width="80px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtbank_commission_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtbank_commission_ac" runat="server" Text="..." OnClick="btntxtbank_commission_ac_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblBankcommission_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Purchase CGST A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtPurchaseCGSTAc" runat="Server" CssClass="txt" TabIndex="9" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtPurchaseCGSTAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtPurchaseCGSTAc" runat="server" Text="..." OnClick="btntxtPurchaseCGSTAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtPurchaseCGSTAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Interest A/c:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtinterest_ac" runat="Server" CssClass="txt" TabIndex="10" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtinterest_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtinterest_ac" runat="server" Text="..." OnClick="btntxtinterest_ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblInterest_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Purchase SGST A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtPurchaseSGSTAc" runat="Server" CssClass="txt" TabIndex="11" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtPurchaseSGSTAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtPurchaseSGSTAc" runat="server" Text="..." OnClick="btntxtPurchaseSGSTAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtPurchaseSGSTAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Transport A/c:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txttransport_ac" runat="Server" CssClass="txt" TabIndex="12" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txttransport_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxttransport_ac" runat="server" Text="..." OnClick="btntxttransport_ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblTransport_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Purchase IGST A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtPurchaseIGSTAc" runat="Server" CssClass="txt" TabIndex="13" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtPurchaseIGSTAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtPurchaseIGSTAc" runat="server" Text="..." OnClick="btntxtPurchaseIGSTAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtPurchaseIGSTAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Sale Dalali A/c:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtsale_dalali_ac" runat="Server" CssClass="txt" TabIndex="14" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtsale_dalali_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtsale_dalali_ac" runat="server" Text="..." OnClick="btntxtsale_dalali_ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblSale_dalali_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                TDS A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtTDS_Ac" runat="Server" CssClass="txt" TabIndex="15" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtTDS_Ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtTds_Ac" runat="server" Text="..." OnClick="btntxtTds_Ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtTds_Ac" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Loadling Charge A/c:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtloading_charge_ac" runat="Server" CssClass="txt" TabIndex="16"
                                    Height="24px" Width="80px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtloading_charge_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtloading_charge_ac" runat="server" Text="..." OnClick="btntxtloading_charge_ac_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblLoading_charge_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Cess A/C
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtCess_Ac" runat="Server" CssClass="txt" TabIndex="17" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtCess_Ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtCess_Ac" runat="server" Text="..." OnClick="btntxtCess_Ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:TextBox ID="txtcessrate" runat="Server" CssClass="txt" Width="50px" Height="24px"
                                    Style="text-align: right;"></asp:TextBox>
                                <asp:Label ID="lblcessAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Motor Freight A/c:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtmotor_freight_ac" runat="Server" CssClass="txt" TabIndex="18"
                                    Height="24px" Width="80px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtmotor_freight_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtmotor_freight_ac" runat="server" Text="..." OnClick="btntxtmotor_freight_ac_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblMotor_freight_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                kharajat A/C
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtKharajat_Ac" runat="Server" CssClass="txt" TabIndex="19" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtKharajat_Ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtKharajat_Ac" runat="server" Text="..." OnClick="btntxtKharajat_Ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblKharajatAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Postahe A/c:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtpostage_ac" runat="Server" CssClass="txt" TabIndex="20" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtpostage_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtpostage_ac" runat="server" Text="..." OnClick="btntxtpostage_ac_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblPostage_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Supercost A/C
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtSupercost_Ac" runat="Server" CssClass="txt" TabIndex="21" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtSupercost_Ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtSupercost_Ac" runat="server" Text="..." OnClick="btntxtSupercost_Ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:TextBox ID="txtsupercostrate" runat="Server" CssClass="txt" Width="50px" Height="24px"
                                    Style="text-align: right;"></asp:TextBox>
                                <asp:Label ID="lblSuperCost" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Other A/c:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtother_amount_ac" runat="Server" CssClass="txt" TabIndex="22"
                                    Height="24px" Width="80px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtother_amount_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtother_amount_ac" runat="server" Text="..." OnClick="btntxtother_amount_ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblOther_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Levi A/C
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtLevi_Ac" runat="Server" CssClass="txt" TabIndex="23" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtLevi_Ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtLevi_Ac" runat="server" Text="..." OnClick="btntxtLevi_Ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblLeviAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Sale Return A/c:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtSaleReturn" runat="Server" CssClass="txt" TabIndex="24" Height="24px"
                                    Width="80px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtSaleReturn_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnSaleReturn" runat="server" Text="..." Height="24px" Width="20px"
                                    CssClass="btnHelp" OnClick="btnSaleReturn_Click" />
                                <asp:Label ID="lblSaleReturnAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Jaggary Commission A/C
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtJaggary_Ac" runat="Server" CssClass="txt" TabIndex="25" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtJaggary_Ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtJaggary_Ac" runat="server" Text="..." OnClick="btntxtJaggary_Ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblJaggryAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Return Sale A/c:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtReturnSale" runat="Server" CssClass="txt" TabIndex="26" Height="24px"
                                    Width="80px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtReturnSale_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnReturnSale" runat="server" Text="..." Height="24px" Width="20px"
                                    CssClass="btnHelp" OnClick="btnReturnSale_Click" />
                                <asp:Label ID="lblReturnSaleAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Jaggary Sale A/C
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtJaggarySale_Ac" runat="Server" CssClass="txt" TabIndex="25" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtJaggarySale_Ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtJaggarySale_Ac" runat="server" Text="..." OnClick="btntxtJaggarySale_Ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbljaggarySaleAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Self A/c:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtself_ac" runat="Server" CssClass="txt" TabIndex="27" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtself_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtself_ac" runat="server" Text="..." OnClick="btntxtself_ac_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblSelf_ac" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Shub A/C
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtShub_Ac" runat="Server" CssClass="txt" TabIndex="25" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtShub_Ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtShub_Ac" runat="server" Text="..." OnClick="btntxtShub_Ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:TextBox ID="txtshubrate" runat="Server" CssClass="txt" TabIndex="25" Width="50px"
                                    Height="24px" Style="text-align: right;"></asp:TextBox>
                                <asp:Label ID="lblshub" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Branch 1:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtBranch1" runat="Server" CssClass="txt" TabIndex="28" Width="180px"
                                    Height="24px" Style="text-align: right;" Enabled="false"></asp:TextBox>
                                <asp:Button ID="btnBranch1" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                    Width="20px" OnClick="btnBranch1_Click" />
                                <asp:TextBox ID="lblBranch1Code" runat="server" Visible="true" BackColor="#66CC99"
                                    Style="border: none;" ForeColor="#66CC99" Enabled="false"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 10%;">
                                P.Pol A/C
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtPPolAc" runat="Server" CssClass="txt" TabIndex="25" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtPPolAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtPPolAc" runat="server" Text="..." OnClick="btntxtPPolAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:TextBox ID="txtPPolrate" runat="Server" CssClass="txt" TabIndex="25" Width="50px"
                                    Height="24px" Style="text-align: right;"></asp:TextBox>
                                <asp:Label ID="lblPPolAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Branch 2:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtBranch2" runat="Server" CssClass="txt" TabIndex="29" Width="180px"
                                    Height="24px" Style="text-align: right;" Enabled="false"></asp:TextBox>
                                <asp:Button ID="btnBranch2" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                    Width="20px" OnClick="btnBranch2_Click" />
                                <asp:TextBox ID="lblBranch2Code" runat="server" Visible="true" BackColor="#66CC99"
                                    Style="border: none;" ForeColor="#66CC99" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Autogenearte Voucher:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:CheckBox runat="server" ID="chkAutoVoucher" Height="10px" Width="10px" />
                            </td>
                            <td align="left" style="width: 20%;">
                                Jaggary Purchase A/C
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtjag_purchase_ac" runat="Server" CssClass="txt" TabIndex="25"
                                    Width="80px" Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtjag_purchase_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnjag_purch_ac" runat="server" Text="..." OnClick="btnjag_purch_ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbljag_purch_acname" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">
                                Excise Rate:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtexcise_rate" runat="Server" CssClass="txt" TabIndex="17" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtexcise_rate_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;">
                                CGST RCM Ac:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtCGST_RCM_Ac" runat="Server" CssClass="txt" TabIndex="25" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtCGST_RCM_Ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtCGST_RCM_Ac" runat="server" Text="..." OnClick="btntxtCGST_RCM_Ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblCGST_RCM_Ac" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                SGST RCM Ac:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtSGST_RCM_Ac" runat="Server" CssClass="txt" TabIndex="25" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtSGST_RCM_Ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtSGST_RCM_Ac" runat="server" Text="..." OnClick="btntxtSGST_RCM_Ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblSGST_RCM_Ac" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;">
                                IGST RCM Ac:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtIGST_RCM_Ac" runat="Server" CssClass="txt" TabIndex="25" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtIGST_RCM_Ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtIGST_RCM_Ac" runat="server" Text="..." OnClick="btntxtIGST_RCM_Ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblIGST_RCM_Ac" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Round Off:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtroundoff" runat="Server" CssClass="txt" TabIndex="25" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtroundoff_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtroundoff" runat="server" Text="..." OnClick="btntxtroundoff_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblroundoff" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;">
                                Sugar TCS %:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtTCS" runat="Server" CssClass="txt" TabIndex="26" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtTCS_TextChanged"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 20%;">
                                Jaggary TCS %:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtJaggary_TCS" runat="Server" CssClass="txt" TabIndex="27" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtJaggary_TCS_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;">
                                Sugar Purchase TCS A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtPurchaseTCSAc" runat="Server" CssClass="txt" TabIndex="28" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtPurchaseTCSAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtPurchaseTCSAc" runat="server" Text="..." OnClick="btntxtPurchaseTCSAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtPurchaseTCSAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Jaggary Purchase TCS A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtJaggary_PurchaseTCSAc" runat="Server" CssClass="txt" TabIndex="29"
                                    Width="80px" Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtJaggary_PurchaseTCSAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnJaggary_PurchaseTCSAc" runat="server" Text="..." OnClick="btnJaggary_PurchaseTCSAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtJaggary_PurchaseTCSAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;">
                                Sugar Sale TCS A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtSaleTCSAc" runat="Server" CssClass="txt" TabIndex="30" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtSaleTCSAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtSaleTCSAc" runat="server" Text="..." OnClick="btntxtSaleTCSAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtSaleTCSAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Jaggary TCS A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtJaggary_SaleTCSAc" runat="Server" CssClass="txt" TabIndex="31"
                                    Width="80px" Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtJaggary_SaleTCSAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtJaggary_SaleTCSAc" runat="server" Text="..." OnClick="txtJaggary_SaleTCSAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtJaggary_SaleTCSAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;">
                                Purchase TDS A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtPurchaseTDSAc" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtPurchaseTDSAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtPurchaseTDCAc" runat="server" Text="..." OnClick="btntxtPurchaseTDSAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtPurchaseTDSAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 20%;">
                                Sale TDS A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtSaleTDSAc" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtSaleTDSAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtSaleTDSAc" runat="server" Text="..." OnClick="btntxtSaleTDSAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtSaleTDSAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;">
                                Sale TDS %:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtSaleTDS" runat="Server" CssClass="txt" TabIndex="7" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtSaleTDS_TextChanged"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 20%;">
                                Purchase TDS %:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtPurchaseTDS" runat="Server" CssClass="txt" TabIndex="7" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtPurchaseTDS_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;">
                                TCS Limit %:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtTCSLimit" runat="Server" CssClass="txt" TabIndex="7" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtTCSLimit_TextChanged"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 20%;">
                                Jaggary Sale TDS A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtJaggrySaleTDSAc" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtJaggrySaleTDSAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtJaggrySaleTDSAc" runat="server" Text="..." OnClick="btntxtJaggrySaleTDSAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtJaggrySaleTDSAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 20%;">
                              Jaggary TDS%:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtJaggarySaleTDSRate" runat="Server" CssClass="txt" TabIndex="7" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtJaggaryTDS_TextChanged"></asp:TextBox>
                            </td> 
                           
                             <td align="left" style="width: 20%;">
                    Jaggary GSTCode :
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtJaggary_GSTCode" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtJaggary_GSTCode_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnJaggary_GSTCode" runat="server" Text="..." OnClick="btnJaggary_GSTCode_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblJaggary_GSTCode" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                </table>
            </asp:Panel>
            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="save" OnClick="btnAdd_Click" Visible="false" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="add" OnClick="btnSave_Click" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="save" OnClick="btnEdit_Click" Visible="false" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="add" OnClick="btnDelete_Click" Visible="false" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            Height="24px" OnClick="btnFirst_Click" Width="90px" Visible="false" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                            Height="24px" OnClick="btnPrevious_Click" Width="90px" Visible="false" />
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                            Height="24px" OnClick="btnNext_Click" Width="90px" Visible="false" />
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                            Height="24px" OnClick="btnLast_Click" Width="90px" Visible="false" />
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
                                Width="250px" Height="20px" AutoPostBack="true" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    AllowPaging="true" PageSize="20" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                    OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
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
                <table width="80%" align="center">
                    <tr>
                        <td colspan="2" align="center" style="background-color: lightslategrey; color: White;">
                            <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Text="Tender Details"></asp:Label>
                        </td>
                    </tr>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
