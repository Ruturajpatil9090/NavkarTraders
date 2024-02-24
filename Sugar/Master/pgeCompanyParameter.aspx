<%@ Page Title="Company Parameter" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeCompanyParameter.aspx.cs" Inherits="Sugar_Master_pgeCompanyParameter" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Script/jquery-1.4.2.js"></script>
    <link href="../../CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../../JQuery/jquery-1.4.1.js" type="text/javascript"></script>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../../JQuery/jquery.keynavigation.js" type="text/javascript"></script>
    <link type="text/css" href="../../menu/menu.css" rel="stylesheet" />
    <script type="text/javascript" src="../../menu/jquery.js"></script>
    <script type="text/javascript" src="../../menu/menu.js"></script>
    <script type="text/javascript" src="../JS/DateValidation.js"> </script>
    <script src="../../JS/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../Scripts/selectfirstrow.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        document.addEventListener('keyup', function (event) {
            if (event.defaultPrevented) {
                return;
            }

            var key = event.key || event.keyCode;

            if (key === 'Escape' || key === 'Esc' || key === 27) {
                //                doWhateverYouWantNowThatYourKeyWasHit();

                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                if (hdnfClosePopupValue == "txtcommission_ac") {
                    document.getElementById("<%=txtcommission_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtinterest_ac") {
                    document.getElementById("<%=txtinterest_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txttransport_ac") {
                    document.getElementById("<%=txttransport_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtpostage_ac") {
                    document.getElementById("<%=txtpostage_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtself_ac") {
                    document.getElementById("<%=txtself_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGstStateCode") {
                    document.getElementById("<%=txtGstStateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtCGSTAc") {
                    document.getElementById("<%=txtCGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSGSTAc") {
                    document.getElementById("<%=txtSGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtIGSTAc") {
                    document.getElementById("<%=txtIGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurchaseCGSTAc") {
                    document.getElementById("<%=txtPurchaseCGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurchaseSGSTAc") {
                    document.getElementById("<%=txtPurchaseSGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurchaseIGSTAc") {
                    document.getElementById("<%=txtPurchaseIGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTransport_RCM_GSTRate") {
                    document.getElementById("<%=txtTransport_RCM_GSTRate.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtCGST_RCM_Ac") {
                    document.getElementById("<%=txtCGST_RCM_Ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSGST_RCM_Ac") {
                    document.getElementById("<%=txtSGST_RCM_Ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtIGST_RCM_Ac") {
                    document.getElementById("<%=txtIGST_RCM_Ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtRoundOff") {
                    document.getElementById("<%=txtRoundOff.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtFreight_Ac") {
                    document.getElementById("<%=txtFreight_Ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtother_amount_ac") {
                    document.getElementById("<%=txtother_amount_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtMarketSase") {
                    document.getElementById("<%=txtMarketSase.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSupercost") {
                    document.getElementById("<%=txtSupercost.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPacking") {
                    document.getElementById("<%=txtPacking.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txthamali") {
                    document.getElementById("<%=txthamali.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txttransportTDS") {
                    document.getElementById("<%=txttransportTDS.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txttransportTDSCut") {
                    document.getElementById("<%=txttransportTDSCut.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtReturnSaleCGST") {
                    document.getElementById("<%=txtReturnSaleCGST.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtReturnSaleSGST") {
                    document.getElementById("<%=txtReturnSaleSGST.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtReturnSaleIGST") {
                    document.getElementById("<%=txtReturnSaleIGST.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtReturnPurchaseCGST") {
                    document.getElementById("<%=txtReturnPurchaseCGST.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtReturnPurchaseSGST") {
                    document.getElementById("<%=txtReturnPurchaseSGST.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtReturnPurchaseIGST") {
                    document.getElementById("<%=txtReturnPurchaseIGST.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtRateDiffAc") {
                    document.getElementById("<%=txtRateDiffAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtDepreciationAC") {
                    document.getElementById("<%=txtDepreciationAC.ClientID %>").focus();
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
            debugger;
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


                if (hdnfClosePopupValue == "txtcommission_ac") {
                    debugger;
                    document.getElementById("<%=txtcommission_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblCommission_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtinterest_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtinterest_ac") {
                    document.getElementById("<%=txtinterest_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblInterest_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txttransport_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txttransport_ac") {
                    document.getElementById("<%=txttransport_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblTransport_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtpostage_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtpostage_ac") {
                    document.getElementById("<%=txtpostage_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblPostage_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtself_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtself_ac") {
                    document.getElementById("<%=txtself_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblSelf_ac.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtGstStateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGstStateCode") {
                    document.getElementById("<%=txtGstStateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtGstStateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtCGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtCGSTAc") {
                    document.getElementById("<%=txtCGSTAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtCGSTAcName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtSGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSGSTAc") {
                    document.getElementById("<%=txtSGSTAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtSGSTAcName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtIGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtIGSTAc") {
                    document.getElementById("<%=txtIGSTAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtIGSTAcName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtPurchaseCGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurchaseCGSTAc") {
                    document.getElementById("<%=txtPurchaseCGSTAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtPurchaseCGSTAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtPurchaseSGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurchaseSGSTAc") {
                    document.getElementById("<%=txtPurchaseSGSTAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtPurchaseSGSTAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtPurchaseIGSTAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurchaseIGSTAc") {
                    document.getElementById("<%=txtPurchaseIGSTAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtPurchaseIGSTAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtTransport_RCM_GSTRate.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTransport_RCM_GSTRate") {
                    document.getElementById("<%=txtTransport_RCM_GSTRate.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblTransport_RCM_GSTRate.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtCGST_RCM_Ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtCGST_RCM_Ac") {
                    document.getElementById("<%=txtCGST_RCM_Ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblCGST_RCM_Ac.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtSGST_RCM_Ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSGST_RCM_Ac") {
                    document.getElementById("<%=txtSGST_RCM_Ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblSGST_RCM_Ac.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtIGST_RCM_Ac.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtIGST_RCM_Ac") {
                    document.getElementById("<%=txtIGST_RCM_Ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblIGST_RCM_Ac.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtRoundOff.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtRoundOff") {
                    document.getElementById("<%=txtRoundOff.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblRoundOff.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtRoundOff.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtFreight_Ac") {
                    document.getElementById("<%=txtFreight_Ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblFreight_Ac.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtFreight_Ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurchaseTCSAc") {
                    document.getElementById("<%=txtPurchaseTCSAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtPurchaseTCSAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtPurchaseTCSAc.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtSaleTCSAc") {
                    document.getElementById("<%=txtSaleTCSAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtSaleTCSAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtSaleTCSAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txthamali") {
                    document.getElementById("<%=txthamali.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblhamali.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txthamali.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPacking") {
                    document.getElementById("<%=txtPacking.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblpacking.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtPacking.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSupercost") {
                    document.getElementById("<%=txtSupercost.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblSupercost.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtSupercost.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtMarketSase") {
                    document.getElementById("<%=txtMarketSase.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblMarketSase.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtMarketSase.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtother_amount_ac") {
                    document.getElementById("<%=txtother_amount_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblOther_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtother_amount_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txttransportTDS") {
                    document.getElementById("<%=txttransportTDS.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltransportTDS.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txttransportTDS.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txttransportTDSCut") {
                    document.getElementById("<%=txttransportTDSCut.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txttransportTDSCut.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txttransportTDSCut.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtReturnSaleCGST") {
                    document.getElementById("<%=txtReturnSaleCGST.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblReturnSaleCGST.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtReturnSaleCGST.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtReturnSaleSGST") {
                    document.getElementById("<%=txtReturnSaleSGST.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblReturnSaleSGST.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtReturnSaleSGST.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtReturnSaleIGST") {
                    document.getElementById("<%=txtReturnSaleIGST.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblReturnSaleIGST.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtReturnSaleIGST.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtReturnPurchaseCGST") {
                    document.getElementById("<%=txtReturnPurchaseCGST.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblReturnPurchaseCGST.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtReturnPurchaseCGST.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtReturnPurchaseSGST") {
                    document.getElementById("<%=txtReturnPurchaseSGST.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblReturnPurchaseSGST.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtReturnPurchaseSGST.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtReturnPurchaseIGST") {
                    document.getElementById("<%=txtReturnPurchaseIGST.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblReturnPurchaseIGST.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtReturnPurchaseIGST.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSaletdsAc") {
                    document.getElementById("<%=txtSaletdsAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblsaletdsacname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtSaletdsAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurchasetdsac") {
                    document.getElementById("<%=txtPurchasetdsac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblpurchasetdsname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtPurchasetdsac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtDepreciationAC") {
                    document.getElementById("<%=txtDepreciationAC.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblDepreciationACname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtDepreciationAC.ClientID %>").focus();
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
    <script type="text/javascript">

        function Commission(e) {

            if (e.keyCode == 112) {
                e.preventDefault();
                // $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtcommission_ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtcommission_ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtcommission_ac.ClientID %>").val(unit);
                __doPostBack("txtcommission_ac", "TextChanged");

            }
        }

        function interestac(e) {

            if (e.keyCode == 112) {
                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtinterest_ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtinterest_ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtinterest_ac.ClientID %>").val(unit);
                __doPostBack("txtinterest_ac", "TextChanged");

            }

        }

        function transportac(e) {

            if (e.keyCode == 112) {
                e.preventDefault();
                // $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxttransport_ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txttransport_ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txttransport_ac.ClientID %>").val(unit);
                __doPostBack("txttransport_ac", "TextChanged");

            }
        }

        function postageac(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtpostage_ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtpostage_ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtpostage_ac.ClientID %>").val(unit);
                __doPostBack("txtpostage_ac", "TextChanged");

            }
        }

        function selfac(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtself_ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtself_ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtself_ac.ClientID %>").val(unit);
                __doPostBack("txtself_ac", "TextChanged");

            }
        }

        function gststatecode(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtGstStateCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtGstStateCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtGstStateCode.ClientID %>").val(unit);
                __doPostBack("txtGstStateCode", "TextChanged");

            }
        }

        function salecgst(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtCGSTAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtCGSTAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtCGSTAc.ClientID %>").val(unit);
                __doPostBack("txtCGSTAc", "TextChanged");

            }

        }
        function salesgst(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtSGSTAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtSGSTAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtSGSTAc.ClientID %>").val(unit);
                __doPostBack("txtSGSTAc", "TextChanged");

            }

        }
        function saleigst(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtIGSTAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtIGSTAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtIGSTAc.ClientID %>").val(unit);
                __doPostBack("txtIGSTAc", "TextChanged");

            }

        }
        function purchasecgst(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtPurchaseCGSTAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtPurchaseCGSTAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtPurchaseCGSTAc.ClientID %>").val(unit);
                __doPostBack("txtPurchaseCGSTAc", "TextChanged");

            }

        }
        function purchasesgst(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtPurchaseSGSTAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtPurchaseSGSTAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtPurchaseSGSTAc.ClientID %>").val(unit);
                __doPostBack("txtPurchaseSGSTAc", "TextChanged");

            }

        }
        function purchaseigst(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtPurchaseIGSTAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtPurchaseIGSTAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtPurchaseIGSTAc.ClientID %>").val(unit);
                __doPostBack("txtPurchaseIGSTAc", "TextChanged");

            }

        }
        function transrcmgstrate(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtTransport_RCM_GSTRate.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtTransport_RCM_GSTRate.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtTransport_RCM_GSTRate.ClientID %>").val(unit);
                __doPostBack("txtTransport_RCM_GSTRate", "TextChanged");

            }

        }
        function cgstrcmac(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtCGST_RCM_Ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtCGST_RCM_Ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtCGST_RCM_Ac.ClientID %>").val(unit);
                __doPostBack("txtCGST_RCM_Ac", "TextChanged");

            }

        }
        function sgstrcmac(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtSGST_RCM_Ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtSGST_RCM_Ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtSGST_RCM_Ac.ClientID %>").val(unit);
                __doPostBack("txtSGST_RCM_Ac", "TextChanged");

            }

        }
        function igstrcmac(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtIGST_RCM_Ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtIGST_RCM_Ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtIGST_RCM_Ac.ClientID %>").val(unit);
                __doPostBack("txtIGST_RCM_Ac", "TextChanged");

            }

        }
        function Roundoff(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=txtbtnRoundOff.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtRoundOff.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtRoundOff.ClientID %>").val(unit);
                __doPostBack("txtRoundOff", "TextChanged");

            }

        }

        function freight(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtFreight_Ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtFreight_Ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtFreight_Ac.ClientID %>").val(unit);
                __doPostBack("txtFreight_Ac", "TextChanged");

            }
        }
        function tcsp(e) {
            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtPurchaseTCSAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtPurchaseTCSAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtPurchaseTCSAc.ClientID %>").val(unit);
                __doPostBack("txtPurchaseTCSAc", "TextChanged");

            }
        }
        function tcssale(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtSaleTCSAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtSaleTCSAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtSaleTCSAc.ClientID %>").val(unit);
                __doPostBack("txtSaleTCSAc", "TextChanged");

            }

        }
        function otheramt(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtother_amount_ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtother_amount_ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtother_amount_ac.ClientID %>").val(unit);
                __doPostBack("txtother_amount_ac", "TextChanged");

            }

        }
        function marketsase(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtMarketSase.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtMarketSase.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtMarketSase.ClientID %>").val(unit);
                __doPostBack("txtMarketSase", "TextChanged");

            }

        }
        function supercost(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtSupercost.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtSupercost.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtSupercost.ClientID %>").val(unit);
                __doPostBack("txtSupercost", "TextChanged");

            }

        }
        function packing(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtPacking.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtPacking.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtPacking.ClientID %>").val(unit);
                __doPostBack("txtPacking", "TextChanged");

            }

        }
        function hamali(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxthamali.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txthamali.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txthamali.ClientID %>").val(unit);
                __doPostBack("txthamali", "TextChanged");

            }

        }
        function transTDS(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntransportTDS.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txttransportTDS.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txttransportTDS.ClientID %>").val(unit);
                __doPostBack("txttransportTDS", "TextChanged");

            }

        }
        function transTDSCut(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntransportTDSCut.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txttransportTDSCut.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txttransportTDSCut.ClientID %>").val(unit);
                __doPostBack("txttransportTDSCut", "TextChanged");

            }

        }
        function ReturnSaleCGST(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btnReturnSaleCGST.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtReturnSaleCGST.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtReturnSaleCGST.ClientID %>").val(unit);
                __doPostBack("txtReturnSaleCGST", "TextChanged");

            }

        }
        function ReturnSaleSGST(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btnReturnSaleSGST.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtReturnSaleSGST.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtReturnSaleSGST.ClientID %>").val(unit);
                __doPostBack("txtReturnSaleSGST", "TextChanged");

            }
        }
        function ReturnSaleIGST(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btnReturnSaleIGST.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtReturnSaleIGST.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtReturnSaleIGST.ClientID %>").val(unit);
                __doPostBack("txtReturnSaleSGST", "TextChanged");

            }

        }
        function ReturnPurchaseCGST(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btnReturnPurchaseCGST.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtReturnPurchaseCGST.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtReturnPurchaseCGST.ClientID %>").val(unit);
                __doPostBack("txtReturnPurchaseCGST", "TextChanged");

            }

        }
        function ReturnPurchaseSGST(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btnReturnPurchaseSGST.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtReturnPurchaseSGST.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtReturnPurchaseSGST.ClientID %>").val(unit);
                __doPostBack("txtReturnPurchaseSGST", "TextChanged");

            }
        }
        function ReturnPurchaseIGST(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btnReturnPurchaseIGST.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtReturnPurchaseIGST.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtReturnPurchaseIGST.ClientID %>").val(unit);
                __doPostBack("txtReturnPurchaseIGST", "TextChanged");

            }

        }
        function Saletdsac(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtSaletdsAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtSaletdsAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtSaletdsAc.ClientID %>").val(unit);
                __doPostBack("txtSaletdsAc", "TextChanged");

            }

        }

        function Purchasetdsac(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtPurchasetdsac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtPurchasetdsac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtPurchasetdsac.ClientID %>").val(unit);
                __doPostBack("txtPurchasetdsac", "TextChanged");

            }

        }
        function ratediffac(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtRateDiffAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtRateDiffAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtRateDiffAc.ClientID %>").val(unit);
                __doPostBack("txtRateDiffAc", "TextChanged");

            }

        }
        function DepreciationAC(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btnDepreciationAC.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtDepreciationAC.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtDepreciationAC.ClientID %>").val(unit);
                __doPostBack("txtDepreciationAC", "TextChanged");

            }

        }
        
    
        

    </script>
    <script type="text/javascript">
        $("#btntxtcommission_ac").click(function () {
            debugger;
            $("#<%=hdnfpopup.ClientID%>").val("0");
        });
    </script>
    <%-- <script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Company Parameters   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdnfBranch1Code" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfpopup" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 10px; margin-top: 0px; z-index: 100;">
                <table width="100%" align="left" cellspacing="3px">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Mill Payment Date:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtmillpaymnetdate" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtmillpaymnetdate_TextChanged"></asp:TextBox>
                        </td>
                        <td align="right" style="width: 20%;">
                            Dispatch Type:
                        </td>
                        <td>
                            <asp:DropDownList ID="drpDeliveryType" runat="server" CssClass="ddl" Width="140px"
                                TabIndex="2" AutoPostBack="true" Height="26px" OnSelectedIndexChanged="drpDeliveryType_SelectedIndexChanged">
                                <asp:ListItem Text="Naka Delivery" Value="N" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Naka Delivery without GST" Value="A"></asp:ListItem>
                                <asp:ListItem Text="Commission" Value="C"></asp:ListItem>
                                <asp:ListItem Text="DO" Value="D"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Commission A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtcommission_ac" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="Commission(event);" AutoPostBack="false"
                                OnTextChanged="txtcommission_ac_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtcommission_ac" runat="server" Text="..." OnClick="btntxtcommission_ac_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblCommission_name" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;">
                            Interest A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtinterest_ac" runat="Server" CssClass="txt" TabIndex="4" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="interestac(event);" AutoPostBack="false"
                                OnTextChanged="txtinterest_ac_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtinterest_ac" runat="server" Text="..." OnClick="btntxtinterest_ac_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblInterest_name" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Transport A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txttransport_ac" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="transportac(event);" AutoPostBack="false"
                                OnTextChanged="txttransport_ac_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxttransport_ac" runat="server" Text="..." OnClick="btntxttransport_ac_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblTransport_name" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;">
                            Postage A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtpostage_ac" runat="Server" CssClass="txt" TabIndex="6" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="postageac(event);" AutoPostBack="false"
                                OnTextChanged="txtpostage_ac_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtpostage_ac" runat="server" Text="..." OnClick="btntxtpostage_ac_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblPostage_name" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Self A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtself_ac" runat="Server" CssClass="txt" TabIndex="7" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="selfac(event);" AutoPostBack="false"
                                OnTextChanged="txtself_ac_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtself_ac" runat="server" Text="..." OnClick="btntxtself_ac_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblSelf_ac" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;">
                            GST State Code:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtGstStateCode" runat="Server" CssClass="txt" TabIndex="8" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="gststatecode(event);" AutoPostBack="false"
                                OnTextChanged="txtGstStateCode_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtGstStateCode" runat="server" Text="..." OnClick="btntxtGstStateCode_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lbltxtGstStateName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Sale CGST A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtCGSTAc" runat="Server" CssClass="txt" TabIndex="9" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="salecgst(event);" AutoPostBack="false"
                                OnTextChanged="txtCGSTAc_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtCGSTAc" runat="server" Text="..." OnClick="btntxtCGSTAc_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lbltxtCGSTAcName" runat="server" CssClass="lblName"></asp:Label>
                            <td align="right" style="width: 20%;">
                                Sale SGST A/c:
                            </td>
                            <td align="left" style="width: 30%;">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtSGSTAc" runat="Server" CssClass="txt" TabIndex="10" Width="80px"
                                    Height="24px" Style="text-align: right;" OnkeyDown="salesgst(event);" AutoPostBack="false"
                                    OnTextChanged="txtSGSTAc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtSGSTAc" runat="server" Text="..." OnClick="btntxtSGSTAc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtSGSTAcName" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Sale IGST A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtIGSTAc" runat="Server" CssClass="txt" TabIndex="11" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="saleigst(event);" AutoPostBack="false"
                                OnTextChanged="txtIGSTAc_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtIGSTAc" runat="server" Text="..." OnClick="btntxtIGSTAc_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lbltxtIGSTAcName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;">
                            Purchase CGST A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPurchaseCGSTAc" runat="Server" CssClass="txt" TabIndex="12" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="purchasecgst(event);" AutoPostBack="false"
                                OnTextChanged="txtPurchaseCGSTAc_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtPurchaseCGSTAc" runat="server" Text="..." OnClick="btntxtPurchaseCGSTAc_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lbltxtPurchaseCGSTAc" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Purchase SGST A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPurchaseSGSTAc" runat="Server" CssClass="txt" TabIndex="13" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="purchasesgst(event);" AutoPostBack="false"
                                OnTextChanged="txtPurchaseSGSTAc_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtPurchaseSGSTAc" runat="server" Text="..." OnClick="btntxtPurchaseSGSTAc_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lbltxtPurchaseSGSTAc" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;">
                            Purchase IGST A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPurchaseIGSTAc" runat="Server" CssClass="txt" TabIndex="14" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="purchaseigst(event);" AutoPostBack="false"
                                OnTextChanged="txtPurchaseIGSTAc_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtPurchaseIGSTAc" runat="server" Text="..." OnClick="btntxtPurchaseIGSTAc_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lbltxtPurchaseIGSTAc" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Transport RCM GSTRate:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtTransport_RCM_GSTRate" runat="Server" CssClass="txt" TabIndex="15"
                                Height="24px" Width="80px" Style="text-align: right;" OnkeyDown="transrcmgstrate(event);"
                                AutoPostBack="false" OnTextChanged="txtTransport_RCM_GSTRate_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtTransport_RCM_GSTRate" runat="server" Text="..." OnClick="btntxtTransport_RCM_GSTRate_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblTransport_RCM_GSTRate" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;">
                            CGST RCM Ac:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtCGST_RCM_Ac" runat="Server" CssClass="txt" TabIndex="16" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="cgstrcmac(event);" AutoPostBack="false"
                                OnTextChanged="txtCGST_RCM_Ac_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtCGST_RCM_Ac" runat="server" Text="..." OnClick="btntxtCGST_RCM_Ac_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblCGST_RCM_Ac" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            SGST RCM Ac:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtSGST_RCM_Ac" runat="Server" CssClass="txt" TabIndex="17" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="sgstrcmac(event);" AutoPostBack="false"
                                OnTextChanged="txtSGST_RCM_Ac_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtSGST_RCM_Ac" runat="server" Text="..." OnClick="btntxtSGST_RCM_Ac_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblSGST_RCM_Ac" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;">
                            IGST RCM Ac:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtIGST_RCM_Ac" runat="Server" CssClass="txt" TabIndex="18" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="igstrcmac(event);" AutoPostBack="false"
                                OnTextChanged="txtIGST_RCM_Ac_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtIGST_RCM_Ac" runat="server" Text="..." OnClick="btntxtIGST_RCM_Ac_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblIGST_RCM_Ac" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Autogenearte Voucher:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox runat="server" ID="chkAutoVoucher" Height="10px" TabIndex="19" Width="10px" />
                        </td>
                        <td align="right" style="width: 20%;">
                            Round off:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtRoundOff" runat="Server" CssClass="txt" TabIndex="20" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="Roundoff(event);" AutoPostBack="false"
                                OnTextChanged="txtRoundOff_TextChanged"></asp:TextBox>
                            <asp:Button ID="txtbtnRoundOff" runat="server" Text="..." OnClick="txtbtnRoundOff_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblRoundOff" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Freight A/C:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtFreight_Ac" runat="Server" CssClass="txt" TabIndex="21" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="freight(event);" AutoPostBack="false"
                                OnTextChanged="txtFreight_Ac_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtFreight_Ac" runat="server" Text="..." OnClick="btntxtFreight_Ac_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblFreight_Ac" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                     
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Purchase TCS A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPurchaseTCSAc" runat="Server" CssClass="txt" TabIndex="22" Width="80px"
                                OnkeyDown="tcsp(event);" Height="24px" Style="text-align: right;" AutoPostBack="True"
                                OnTextChanged="txtPurchaseTCSAc_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtPurchaseTCSAc" runat="server" Text="..." OnClick="btntxtPurchaseTCSAc_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lbltxtPurchaseTCSAc" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;">
                            Sale TCS A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtSaleTCSAc" runat="Server" CssClass="txt" TabIndex="23" Width="80px"
                                OnkeyDown="tcssale(event);" Height="24px" Style="text-align: right;" AutoPostBack="True"
                                OnTextChanged="txtSaleTCSAc_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtSaleTCSAc" runat="server" Text="..." OnClick="btntxtSaleTCSAc_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lbltxtSaleTCSAc" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Other A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtother_amount_ac" runat="Server" CssClass="txt" TabIndex="24"
                                Height="24px" Width="80px" Style="text-align: right;" OnkeyDown="otheramt(event);"
                                AutoPostBack="false" OnTextChanged="txtother_amount_ac_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtother_amount_ac" runat="server" Text="..." OnClick="btntxtother_amount_ac_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblOther_name" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;">
                            Market Sase A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtMarketSase" runat="Server" CssClass="txt" TabIndex="25" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="marketsase(event);" AutoPostBack="false"
                                OnTextChanged="txtMarketSase_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtMarketSase" runat="server" Text="..." OnClick="btntxtMarketSase_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblMarketSase" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Supercost A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtSupercost" runat="Server" CssClass="txt" TabIndex="26" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="supercost(event);" AutoPostBack="false"
                                OnTextChanged="txtSupercost_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtSupercost" runat="server" Text="..." OnClick="btntxtSupercost_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblSupercost" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;">
                            Packing A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPacking" runat="Server" CssClass="txt" TabIndex="27" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="packing(event);" AutoPostBack="false"
                                OnTextChanged="txtPacking_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtPacking" runat="server" Text="..." OnClick="btntxtPacking_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblpacking" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Hamali A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txthamali" runat="Server" CssClass="txt" TabIndex="28" Height="24px"
                                Width="80px" Style="text-align: right;" OnkeyDown="hamali(event);" AutoPostBack="false"
                                OnTextChanged="txthamali_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxthamali" runat="server" Text="..." Height="24px" Width="20px"
                                CssClass="btnHelp" OnClick="btntxthamali_Click" />
                            <asp:Label ID="lblhamali" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;">
                            Transport TDS A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txttransportTDS" runat="Server" CssClass="txt" TabIndex="29" Height="24px"
                                Width="80px" Style="text-align: right;" OnkeyDown="transTDS(event);" AutoPostBack="false"
                                OnTextChanged="txttransportTDS_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntransportTDS" runat="server" Text="..." Height="24px" Width="20px"
                                CssClass="btnHelp" OnClick="btntransportTDS_Click" />
                            <asp:Label ID="lbltransportTDS" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Transport TDS Cut by Us A/c:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txttransportTDSCut" runat="Server" CssClass="txt" TabIndex="30"
                                Height="24px" Width="80px" Style="text-align: right;" OnkeyDown="transTDSCut(event);"
                                AutoPostBack="false" OnTextChanged="txttransportTDSCut_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntransportTDSCut" runat="server" Text="..." Height="24px" Width="20px"
                                CssClass="btnHelp" OnClick="btntransportTDSCut_Click" />
                            <asp:Label ID="lblTDSCutByus" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;">
                            Return Sale CGST:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtReturnSaleCGST" runat="Server" CssClass="txt" TabIndex="31" Height="24px"
                                Width="80px" Style="text-align: right;" OnkeyDown="ReturnSaleCGST(event);" AutoPostBack="false"
                                OnTextChanged="txtReturnSaleCGST_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnReturnSaleCGST" runat="server" Text="..." Height="24px" Width="20px"
                                CssClass="btnHelp" OnClick="btnReturnSaleCGST_Click" />
                            <asp:Label ID="lblReturnSaleCGST" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Return Sale SGST:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtReturnSaleSGST" runat="Server" CssClass="txt" TabIndex="32" Height="24px"
                                Width="80px" Style="text-align: right;" OnkeyDown="ReturnSaleSGST(event);" AutoPostBack="false"
                                OnTextChanged="txtReturnSaleSGST_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnReturnSaleSGST" runat="server" Text="..." Height="24px" Width="20px"
                                CssClass="btnHelp" OnClick="btnReturnSaleSGST_Click" />
                            <asp:Label ID="lblReturnSaleSGST" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;">
                            Return Sale IGST:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtReturnSaleIGST" runat="Server" CssClass="txt" TabIndex="33" Height="24px"
                                Width="80px" Style="text-align: right;" OnkeyDown="ReturnSaleIGST(event);" AutoPostBack="false"
                                OnTextChanged="txtReturnSaleIGST_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnReturnSaleIGST" runat="server" Text="..." Height="24px" Width="20px"
                                CssClass="btnHelp" OnClick="btnReturnSaleIGST_Click" />
                            <asp:Label ID="lblReturnSaleIGST" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Return Purchase CGST:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtReturnPurchaseCGST" runat="Server" CssClass="txt" TabIndex="34"
                                Height="24px" Width="80px" Style="text-align: right;" OnkeyDown="ReturnPurchaseCGST(event);"
                                AutoPostBack="false" OnTextChanged="txtReturnPurchaseCGST_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnReturnPurchaseCGST" runat="server" Text="..." Height="24px" Width="20px"
                                CssClass="btnHelp" OnClick="btnReturnPurchaseCGST_Click" />
                            <asp:Label ID="lblReturnPurchaseCGST" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;">
                            Return Purchase SGST:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtReturnPurchaseSGST" runat="Server" CssClass="txt" TabIndex="35"
                                Height="24px" Width="80px" Style="text-align: right;" OnkeyDown="ReturnPurchaseSGST(event);"
                                AutoPostBack="false" OnTextChanged="txtReturnPurchaseSGST_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnReturnPurchaseSGST" runat="server" Text="..." Height="24px" Width="20px"
                                CssClass="btnHelp" OnClick="btnReturnPurchaseSGST_Click" />
                            <asp:Label ID="lblReturnPurchaseSGST" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Return Purchase IGST:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtReturnPurchaseIGST" runat="Server" CssClass="txt" TabIndex="36"
                                Height="24px" Width="80px" Style="text-align: right;" OnkeyDown="ReturnPurchaseIGST(event);"
                                AutoPostBack="false" OnTextChanged="txtReturnPurchaseIGST_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnReturnPurchaseIGST" runat="server" Text="..." Height="24px" Width="20px"
                                CssClass="btnHelp" OnClick="btnReturnPurchaseIGST_Click" />
                            <asp:Label ID="lblReturnPurchaseIGST" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                          <td align="right" style="width: 20%;">
                            Purchase TDS Ac:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPurchasetdsac" runat="Server" CssClass="txt" TabIndex="37" Height="24px"
                                Width="80px" Style="text-align: right;" OnkeyDown="Purchasetdsac(event);" AutoPostBack="false"
                                OnTextChanged="txtPurchasetdsac_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtPurchasetdsac" runat="server" Text="..." Height="24px" Width="20px"
                                CssClass="btnHelp" OnClick="btntxtPurchasetdsac_Click" />
                            <asp:TextBox ID="txtPurchasetdsrate" runat="Server" CssClass="txt" TabIndex="38"
                                Height="24px" Width="50px" Style="text-align: right;" AutoPostBack="false"></asp:TextBox>
                            <asp:Label ID="lblpurchasetdsname" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Sale TDS Ac:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtSaletdsAc" runat="Server" CssClass="txt" TabIndex="39" Height="24px"
                                Width="80px" Style="text-align: right;" OnkeyDown="Saletdsac(event);" AutoPostBack="false"
                                OnTextChanged="txtSaletdsAc_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtSaletdsAc" runat="server" Text="..." Height="24px" Width="20px"
                                CssClass="btnHelp" OnClick="btntxtSaletdsAc_Click" />
                            <asp:TextBox ID="txtsaletdsrate" runat="Server" CssClass="txt" TabIndex="40" Height="24px"
                                Width="50px" Style="text-align: right;" AutoPostBack="false"></asp:TextBox>
                            <asp:Label ID="lblsaletdsacname" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                      <td align="right" style="width: 20%;">
                            Do Date:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtdo_date" runat="Server" CssClass="txt" Width="80px" TabIndex="41"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtdo_date_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="imgdodate" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                Height="15px" />
                            <ajax1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtdo_date"
                                PopupButtonID="imgdodate" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            Rate Diff Ac:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtRateDiffAc" runat="Server" CssClass="txt" TabIndex="42" Height="24px"
                                Width="80px" Style="text-align: right;" OnkeyDown="ratediffac(event);" AutoPostBack="false"
                                OnTextChanged="txtRateDiffAc_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtRateDiffAc" runat="server" Text="..." Height="24px" Width="20px"
                                CssClass="btnHelp" OnClick="btntxtRateDiffAc_Click" />
                            <asp:Label ID="lblratediffacname" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                         <td align="right" style="width: 20%;">
                            DOPages:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtDOPages" runat="Server" CssClass="txt" TabIndex="43" Height="24px"
                                Width="80px" Style="text-align: right;" AutoPostBack="false"
                                OnTextChanged="txtDOPages_TextChanged"></asp:TextBox>
                        </td>
                        </tr>
                 
                        
                    <tr>
                        <td align="right" style="width: 20%;">
                          Purchase TCS %:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPurchaseSaleTcs" runat="Server" CssClass="txt" TabIndex="44" Width="80px"
                                Height="24px" Style="text-align: right;" AutoPostBack="false"></asp:TextBox>
                                    </td>
                          <td align="right" style="width: 20%;">
                           SALE TCS %:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtTCS" runat="Server" CssClass="txt" TabIndex="45" Width="80px"
                                Height="24px" Style="text-align: right;" AutoPostBack="false"></asp:TextBox>
                         </td>
                        </tr>

                        <tr>
                         <td align="right" style="width: 20%;">     
                       TCS Purchase Balance Limit:
                       </td>
                        <td align="left" style="width: 30%;"> 
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            <asp:TextBox ID="txtTCSPurchaseBalanceLimit" runat="Server" CssClass="txt" TabIndex="46" Height="24px"
                                Width="150px" Style="text-align: right;" AutoPostBack="false"></asp:TextBox>
                        </td>
                         <td align="right" style="width: 20%;">
                         TCS SALE Balance Limit
                        </td>
                          <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            <asp:TextBox ID="txtBalancelimit" runat="Server" CssClass="txt" TabIndex="47" Height="24px"
                                Width="150px" Style="text-align: right;" AutoPostBack="false"></asp:TextBox>
                        </td>
                    </tr>
                    <td align="right" style="width: 20%;"> 
                            TDS Purchase Balance Limit: 
                        </td>
                         <td align="left" style="width: 30%;"> 
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtTDSPurchaseBalanceLimit" runat="Server" CssClass="txt" TabIndex="48" Height="24px"
                                Width="150px" Style="text-align: right;" AutoPostBack="false"></asp:TextBox> 
                        </td>
                     <td align="right" style="width: 20%;"> 
                      TDS SALE Balance Limit:
                        </td> 
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        <td align="left" style="width: 30%;"> 
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            <asp:TextBox ID="txtTCSTDSSaleBalanceLimit" runat="Server" CssClass="txt" TabIndex="49" Height="24px"
                                Width="150px" Style="text-align: right;" AutoPostBack="false"></asp:TextBox>
                        </td>
                  
                         
                        </tr>
                     <tr>
                        <td align="right" style="width: 20%;">
                            Depreciation Ac:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtDepreciationAC" runat="Server" CssClass="txt" TabIndex="50" Height="24px"
                                Width="80px" Style="text-align: right;" OnkeyDown="DepreciationAC(event);" AutoPostBack="false"
                                OnTextChanged="txtDepreciationAC_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnDepreciationAC" runat="server" Text="..." Height="24px" Width="20px"
                                CssClass="btnHelp" OnClick="btnDepreciationAC_Click" />
                            <asp:Label ID="lblDepreciationACname" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                         <td align="right" style="width: 20%;">
                             Interest Rate:
                        </td>
                        <td align="left" style="width: 30%;"> 
                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtInterestRate" runat="Server" CssClass="txt" TabIndex="51" Height="24px"
                                Width="80px" Style="text-align: right;"  AutoPostBack="false"
                                OnTextChanged="txtInterestRate_TextChanged"></asp:TextBox>
                        </td>
                        </tr>
                      <tr>
                         <td align="right" style="width: 20%;"> 
                             Interest TDS Ac:
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                             <asp:TextBox ID="txtInterestTDSAc" runat="Server" CssClass="txt" TabIndex="52" Height="24px"
                                Width="80px" Style="text-align: right;"  AutoPostBack="false"
                                OnTextChanged="txtInterestTDSAc_TextChanged"></asp:TextBox>
                        </td>
                        <td>
                          
                        </td>
                    </tr>
                          <tr>
                         <td align="right" style="width: 20%;"> 
                        </td>
                        <td align="left" style="width: 30%;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                        </td>
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnHelp" Width="100px" TabIndex="53"
                                Height="30px" ValidationGroup="save" OnClick="btnUpdate_Click" Visible="true" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%" align="center" ScrollBars="None" BackColor="#FFFFE4"
                Direction="LeftToRight" Style="z-index: 5000; position: absolute; display: none;
                float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px;
                background-position: center; left: 10%; top: 10%;">
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
                        <td colspan="4" align="center" style="background-color: lightslategrey; color: White;">
                            <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Text="Tender Details"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
