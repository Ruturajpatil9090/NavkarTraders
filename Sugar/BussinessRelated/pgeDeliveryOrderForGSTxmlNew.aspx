<%@ Page Title="Delivery Order" Language="C#" MasterPageFile="~/MasterPage.master" Async="true"
    AutoEventWireup="true" CodeFile="pgeDeliveryOrderForGSTxmlNew.aspx.cs" Inherits="Sugar_pgeDeliveryOrderForGSTxmlNew" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<script src="../../JS/jquery-2.1.3.js" type="text/javascript"></script>--%>


    <%-- <script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

    </script>--%>


    <style type="text/css">
        .sms {
            font-size: small;
            font-weight: bold;
            color: Black;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function memo() {
            //            window.open('../Sugar/pgeMotorMemo.aspx');    //R=Redirected  O=Original
            window.open('../Sugar/pgeMotorMemoxml.aspx');    //R=Redirected  O=Original

        }
        function sugarpurchase(Action, vocno) {
            //            window.open('../Sugar/pgeSugarPurchaseForGST.aspx');    //R=Redirected  O=Original
            //R=Redirected  O=Original
            window.open('../Inword/pgeSugarPurchaseForGSTxml.aspx?Action=' + Action + '&purchaseid=' + vocno);

        }

        function LocalVoucher(Action, vocno, Tran_Type) {
            window.open('../Outword/pgeLocalVoucherForGSTxmlNew.aspx?Action=' + Action + '&commissionid=' + vocno + '&Tran_Type=' + Tran_Type);    //R=Redirected  O=Original
        }
        function loadingvoucher() {
            window.open('../Sugar/pgeLoadingvoucher.aspx');    //R=Redirected  O=Original
        }
        function salebill(Action, sbno) {
            //            window.open('../Sugar/pgrSugarsaleForGST.aspx');  
            window.open('../Outword/pgrSugarsaleForGSTxml.aspx?Action=' + Action + '&saleid=' + sbno);   //R=Redirected  O=Original
        }

        function DOParty(do_no, mill, tenderno) {
            var tn;
            //window.open('../Report/rptDeliveryOrderParty.aspx?do_no=' + do_no + '&email=' + email, '_blank');    //R=Redirected  O=Original
            window.open('../Report/rptDeliveryOrderParty.aspx?do_no=' + do_no + '&mill=' + mill + '&tenderno=' + tenderno, '_blank');
        }
        function partybilldo(do_no, mill, PO, a, tenderno, bss, Paymentto) {
            var tn;
            window.open('../Report/rptDeliveryOrderPartyBill.aspx?do_no=' + do_no + '&mill=' + mill + '&PO=' + PO
                + '&a=' + a + '&tenderno=' + tenderno + '&bss=' + bss + '&Paymentto=' + Paymentto);    //R=Redirected  O=Original
        }
        function od(do_no, mill, PO, a, tenderno, bss, Paymentto) {
            var tn;
            window.open('../Report/rptDeliveryOrderForGST.aspx?do_no=' + do_no + '&mill=' + mill + '&PO=' + PO
                + '&a=' + a + '&tenderno=' + tenderno + '&bss=' + bss + '&Paymentto=' + Paymentto);    //R=Redirected  O=Original
        }
        function cd(do_no, mill, PO, a, tenderno, bss, Paymentto) {
            var tn;
            window.open('../Report/rptCustomizeDeliveryOrderForGST.aspx?do_no=' + do_no + '&mill=' + mill + '&PO=' + PO
                + '&a=' + a + '&tenderno=' + tenderno + '&bss=' + bss + '&Paymentto=' + Paymentto);    //R=Redirected  O=Original
        }
        function DC(do_no, email, PO, a) {
            var tn;
            window.open('../Report/rptDelivery_Challan.aspx?do_no=' + do_no + '&email=' + email + '&PO=' + PO + '&a=' + a);    //R=Redirected  O=Original
        }
        function TL(DONO, DOCODE) {
            var Donumber = document.getElementById('<%=txtdoc_no.ClientID %>').value;
            var DOCode = document.getElementById('<%=txtDO_CODE.ClientID %>').value;
            //if (DOCode != '') {
            window.open('../Report/rptNewTransferLetter.aspx?DONO=' + Donumber + '&DOCODE=' + DOCode);
            //}
        }
        function WB(Doc_Code) {
            var Donumber = document.getElementById('<%=txtdoc_no.ClientID %>').value;
            window.open('../Report/rptWayBill.aspx?Doc_No=' + Donumber);
        }

        //function SB(saleid, billto) {

        //    window.open('../Report/pgeSaleBill_Print.aspx?doc_no=' + saleid + '&billto=' + billto);
        //    // window.open('../Report/rptSellBillForGST.aspx?billno=' + billno);
        //}
        function SB(saleid, billto, docnumber, corporatenumber, corporate) {

            window.open('../Report/pgeSaleBill_Print.aspx?doc_no=' + saleid + '&billto=' + billto + '&docnumber=' + docnumber
                + '&corporatenumber=' + corporatenumber + '&corporate=' + corporate);
            // window.open('../Report/rptSellBillForGST.aspx?billno=' + billno);
        }
        function pendingSB() {

            window.open('../Report/rptcheckpendingsalebill.aspx');
            // window.open('../Report/rptSellBillForGST.aspx?billno=' + billno);
        }
        //function SB1(saleid, billto) {

        //    window.open('../Report/pgeSaleBill_Print1.aspx?doc_no=' + saleid + '&billto=' + billto);
        //    // window.open('../Report/rptSellBillForGST.aspx?billno=' + billno);
        //}
        function SB1(saleid, billto, docnumber, corporatenumber, corporate) {

            window.open('../Report/pgeSaleBill_Print1.aspx?doc_no=' + saleid + '&billto=' + billto +
                '&docnumber=' + docnumber + '&corporatenumber=' + corporatenumber + '&corporate=' + corporate);
            // window.open('../Report/rptSellBillForGST.aspx?billno=' + billno);
        }

        function partybilldo1(do_no, mill, PO, a, tenderno) {
            var tn;
            window.open('../Report/rptDeliveryOrderPartyBill1.aspx?do_no=' + do_no + '&mill=' + mill + '&PO=' + PO + '&a=' + a + '&tenderno=' + tenderno);    //R=Redirected  O=Original
        }

        function od1(do_no, mill, PO, a, tenderno) {
            var tn;
            window.open('../Report/rptDeliveryOrderForGST1.aspx?do_no=' + do_no + '&mill=' + mill + '&PO=' + PO + '&a=' + a + '&tenderno=' + tenderno);    //R=Redirected  O=Original
        }
        function cd1(do_no, mill, PO, a, tenderno) {
            var tn;
            window.open('../Report/rptCustomizeDeliveryOrderForGST1.aspx?do_no=' + do_no + '&mill=' + mill + '&PO=' + PO + '&a=' + a + '&tenderno=' + tenderno);    //R=Redirected  O=Original
        }



        function CV() {
            var VNO = document.getElementById('<%=lblVoucherNo.ClientID %>').innerText;
            var type = document.getElementById('<%=lblVoucherType.ClientID %>').innerText;
            window.open('../Report/rptVouchersNew.aspx?VNO=' + VNO + '&type=' + type);
        }

        function ITCV() {
            var VNO = document.getElementById('<%=lblVoucherNo.ClientID %>').innerText;
            window.open('../Report/rptITCVouc.aspx?Doc_No=' + VNO);
        }

        function MM(memono) {

            window.open('../Report/rptMotor_Memo.aspx?do_no=' + memono);
        }
        function DebitNote() {
            //window.open('../Sugar/pgeLocalvoucher.aspx');    //R=Redirected  O=Original
            //            window.open('../Sugar/pgeLocalVoucherForGST.aspx');    //R=Redirected  O=Original
            window.open('../Sugar/pgeLocalVoucherForGSTxml.aspx');    //R=Redirected  O=Original


        }

        function close() {

            document.getElementById('<%=pnlSendSMS.ClientID %>').style.display = "none";
        }
        function GEway() {
            var dono = document.getElementById('<%=txtdoc_no.ClientID %>').value;
            var ID = '';
            var carporateSale = document.getElementById('<%=txtcarporateSale.ClientID %>').value;
            window.open('../Utility/pgeEwayBill.aspx?dono=' + dono + '&Type=SB&ID=' + ID + '&carporateSale=' + carporateSale, "_self");
        }
        function EInovice() {
            var dono = document.getElementById('<%=txtdoc_no.ClientID %>').value;
            var ID = '';
            var carporateSale = document.getElementById('<%=txtcarporateSale.ClientID %>').value;
            window.open('../Utility/pgeEInovice.aspx?dono=' + dono + '&Type=SB&ID=' + ID + '&carporateSale=' + carporateSale, "_self");
        }
        function EInoviceEwayBill() {
            var dono = document.getElementById('<%=txtdoc_no.ClientID %>').value;
            var ID = '';
            var carporateSale = document.getElementById('<%=txtcarporateSale.ClientID %>').value;
            window.open('../Utility/pgeEInoviceAndEwayBill.aspx?dono=' + dono + '&Type=SB&ID=' + ID + '&carporateSale=' + carporateSale, "_self");
        }
        function PI(saleid, billto, docnumber, corporatenumber) {

            window.open('../Report/rptperformainvoice_Print.aspx?doc_no=' + saleid + '&billto=' + billto + '&docnumber=' + docnumber + '&corporatenumber=' + corporatenumber);

        }


        function CI(saleid, billto, docnumber, corporatenumber) {

            window.open('../Report/rptcustomizeperformainvoice_Print.aspx?doc_no=' + saleid + '&billto=' + billto + '&docnumber=' + docnumber + '&corporatenumber=' + corporatenumber);

        }
        function donew(do_no, mill, PO, a, tenderno, bss, Paymentto) {
            debugger;
            var tn;
            window.open('../Report/rptDeliveryOrder.aspx?do_no=' + do_no + '&mill=' + mill + '&PO=' + PO
                + '&a=' + a + '&tenderno=' + tenderno + '&bss=' + bss + '&Paymentto=' + Paymentto);    //R=Redirected  O=Original
        }
        function Customize(do_no, mill, PO, a, tenderno, bss, Paymentto) {
            var tn;
            window.open('../Report/rptCustomerDo.aspx?do_no=' + do_no + '&mill=' + mill + '&PO=' + PO
                + '&a=' + a + '&tenderno=' + tenderno + '&bss=' + bss + '&Paymentto=' + Paymentto);    //R=Redirected  O=Original
        }
    </script>
    <script type="text/javascript">
        function Vasuli() {

            var drp = document.getElementById('<%=drpDOType.ClientID %>');
            var val = drp.options[drp.selectedIndex].value;

            var drp1 = document.getElementById('<%=drpDeliveryType.ClientID %>');
            var val1 = drp1.options[drp1.selectedIndex].value;

            if (val == "DI") {
                if (val1 == "C" || val1 == "N") {
                    var transport = document.getElementById('<%=txtTRANSPORT_CODE.ClientID %>').value;
                    if (transport == "" || transport == "0") {
                        alert('Transport Code Is Compulsory');
                        document.getElementById('<%=txtTRANSPORT_CODE.ClientID %>').focus();
                        document.getElementById("<%=btnSave.ClientID %>").disabled = false;
                        //document.getElementById("<%=btnSave.ClientID %>").value = "";
                        return;
                    }
                    else {
                        return true;
                    }
                }
            }
        }</script>

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete data and all Vouchers?")) {
                confirm_value.value = "Yes";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            }
            else {
                confirm_value.value = "No";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function Back() {
            window.open('../BussinessRelated/PgeDoHeadUtility.aspx', '_self');
        }
        function ConfirmCancle() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Cancle EInvoice...?")) {
                confirm_value.value = "Yes";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
                if (confirm_value = "Yes") {
                    var confirm_value = document.createElement("INPUT");
                    confirm_value.type = "hidden";
                    confirm_value.name = "confirm_value";
                    if (confirm("Do you want to Cancle EInvoice...?")) {
                        confirm_value.value = "Yes";
                        document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
                    }
                    else {
                        confirm_value.value = "No";
                        document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
                    }
                    document.forms[0].appendChild(confirm_value);
                }
            }
            else {
                confirm_value.value = "No";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
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

                if (hdnfClosePopupValue == "txtMILL_CODE") {

                    document.getElementById("<%=txtMILL_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGstRate") {

                    document.getElementById("<%=txtGstRate.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtMemoGSTRate") {

                    document.getElementById("<%=txtMemoGSTRate.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGETPASS_CODE") {

                    document.getElementById("<%=txtGETPASS_CODE.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtGetpassGstStateCode") {

                    document.getElementById("<%=txtGetpassGstStateCode.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtvoucher_by") {

                    document.getElementById("<%=txtvoucher_by.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtVoucherbyGstStateCode") {

                    document.getElementById("<%=txtVoucherbyGstStateCode.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtSalebilltoGstStateCode") {

                    document.getElementById("<%=txtSalebilltoGstStateCode.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtMillGstStateCode") {

                    document.getElementById("<%=txtMillGstStateCode.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtTransportGstStateCode") {

                    document.getElementById("<%=txtTransportGstStateCode.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtVasuliAc") {

                    document.getElementById("<%=txtVasuliAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGRADE") {

                    document.getElementById("<%=txtGRADE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtDO_CODE") {

                    document.getElementById("<%=txtDO_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBroker_CODE") {

                    document.getElementById("<%=txtBroker_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTRANSPORT_CODE") {

                    document.getElementById("<%=txtTRANSPORT_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBANK_CODE") {

                    document.getElementById("<%=txtBANK_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtdoc_no") {

                    document.getElementById("<%=txtdoc_no.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtNARRATION1") {

                    document.getElementById("<%=txtNARRATION1.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNARRATION2") {

                    document.getElementById("<%=txtNARRATION2.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNARRATION3") {

                    document.getElementById("<%=txtNARRATION3.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNARRATION4") {

                    document.getElementById("<%=txtNARRATION4.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNARRATION") {
                    document.getElementById("<%=txtNARRATION.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtBANK_AMOUNT.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtparty") {

                    document.getElementById("<%=txtNARRATION4.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtitem_Code") {

                    document.getElementById("<%=txtitem_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtcarporateSale") {

                    document.getElementById("<%=txtcarporateSale.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtUTRNo") {

                    document.getElementById("<%=txtUTRNo.ClientID%>").focus();
                }
                if (hdnfClosePopupValue == "txtUTRNoU") {

                    document.getElementById("<%=txtNARRATION.ClientID%>").focus();
                }

                if (hdnfClosePopupValue == "txtPurcNo") {

                    document.getElementById("<%=btntxtPurcNo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSaleBillTo") {
                    document.getElementById("<%=txtSaleBillTo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTDSAc") {
                    document.getElementById("<%=txtTDSAc.ClientID %>").focus();
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


                if (hdnfClosePopupValue == "txtMILL_CODE") {
                    document.getElementById("<%=txtMILL_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLMILL_NAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtMILL_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGstRate") {
                    document.getElementById("<%=txtGstRate.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblGstRateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGstRate.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtMemoGSTRate") {
                    document.getElementById("<%=txtMemoGSTRate.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblMemoGSTRate.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtMemoGSTRate.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGETPASS_CODE") {
                    document.getElementById("<%=txtGETPASS_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLGETPASS_NAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGETPASS_CODE.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtGetpassGstStateCode") {
                    document.getElementById("<%=txtGetpassGstStateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtGetpassGstStateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGetpassGstStateCode.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtvoucher_by") {
                    document.getElementById("<%=txtvoucher_by.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblvoucherbyname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtvoucher_by.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtCashDiffAc") {
                    document.getElementById("<%=txtCashDiffAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblCashDiffAcname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtCashDiffAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTDSAc") {
                    document.getElementById("<%=txtTDSAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblTDSAcname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtTDSAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtVoucherbyGstStateCode") {
                    document.getElementById("<%=txtVoucherbyGstStateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtVoucherbyGstStateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtVoucherbyGstStateCode.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtSalebilltoGstStateCode") {
                    document.getElementById("<%=txtSalebilltoGstStateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtSalebilltoGstStateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtSalebilltoGstStateCode.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtMillGstStateCode") {
                    document.getElementById("<%=txtMillGstStateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtMillGstStateCode.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtMillGstStateCode.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtTransportGstStateCode") {
                    document.getElementById("<%=txtTransportGstStateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtTransportGstStateCode.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtTransportGstStateCode.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtVasuliAc") {
                    document.getElementById("<%=txtVasuliAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtVasuliAc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtVasuliAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGRADE") {
                    document.getElementById("<%=txtGRADE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;

                    document.getElementById("<%=txtquantal.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtDO_CODE") {
                    document.getElementById("<%=txtDO_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLDO_NAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtDO_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBroker_CODE") {
                    document.getElementById("<%=txtBroker_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLBROKER_NAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBroker_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTRANSPORT_CODE") {
                    document.getElementById("<%=txtTRANSPORT_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLTRANSPORT_NAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtTRANSPORT_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBANK_CODE") {
                    document.getElementById("<%=txtBANK_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBank_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBANK_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtdoc_no") {
                    document.getElementById("<%=txtdoc_no.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtdoc_no.ClientID %>").disabled = false;
                    document.getElementById("<%=txtDOC_DATE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").disabled = false;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNARRATION1") {
                    document.getElementById("<%=txtNARRATION1.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtNARRATION2.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNARRATION2") {
                    document.getElementById("<%=txtNARRATION2.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtNARRATION3.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNARRATION3") {
                    document.getElementById("<%=txtNARRATION3.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtNarration5.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNARRATION4") {
                    document.getElementById("<%=txtNARRATION4.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtSalebilltoGstStateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNARRATION") {
                    document.getElementById("<%=txtNARRATION.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtBANK_AMOUNT.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtparty") {
                    document.getElementById("<%=txtNARRATION4.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtSaleBillTo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtSaleBillTo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtitem_Code") {
                    document.getElementById("<%=txtitem_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblitem_Name.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtitem_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtcarporateSale") {
                    document.getElementById("<%=txtcarporateSale.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblCSYearCode.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[9].innerText;
                    document.getElementById("<%=txtcarporateSale.ClientID %>").disabled = false;
                    document.getElementById("<%=txtcarporateSale.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtUTRNo") {

                    document.getElementById("<%=txtUTRNo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblUTRYearCode.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[9].innerText;
                    document.getElementById("<%= hdnfUtrBalance.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[5].innerText;
                    document.getElementById("<%=txtNARRATION.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtLT_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[4].innerText;
                    document.getElementById("<%= hdnfUtrdetail.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[11].innerText;
                    document.getElementById("<%=lblUTRCompnyCode.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[12].innerText;
                    document.getElementById("<%=txtNARRATION.ClientID%>").focus();
                }
                if (hdnfClosePopupValue == "txtUTRNoU") {
                    document.getElementById("<%=txtUTRNo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblUTRYearCode.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[6].innerText;
                    document.getElementById("<%= hdnfUtrBalance.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[5].innerText;
                    document.getElementById("<%=txtNARRATION.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtNARRATION.ClientID%>").focus();
                }
                if (hdnfClosePopupValue == "txtBill_To") {
                    document.getElementById("<%=txtBill_To.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBill_To.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBill_To.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSaleBillTo") {
                    document.getElementById("<%=txtSaleBillTo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtNARRATION4.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cell1[2].innerText;
                    document.getElementById("<%=txtSaleBillTo.ClientID%>").focus();
                }
                if (hdnfClosePopupValue == "txtPurcNo") {
                    document.getElementById("<%=txtPurcNo.ClientID %>").disabled = false;
                    document.getElementById("<%=txtPurcOrder.ClientID %>").disabled = false;
                    document.getElementById("<%=txtPurcNo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtPurcOrder.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[12].innerText;
                    document.getElementById("<%= hdnf.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[13].innerText;
                    document.getElementById("<%= hdnfTenderQty.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[9].innerText;
                    document.getElementById("<%= hdnfTenderID.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[14].innerText;
                    document.getElementById("<%= hdnfTenderDetailid.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[13].innerText;
                    var cs = document.getElementById("<%=txtcarporateSale.ClientID %>").value;
                    if (cs == '') {
                        if (cs == 0) {
                            document.getElementById("<%=txtquantal.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[9].innerText;
                        }
                    }
                    document.getElementById("<%=txtPurcNo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBrand_Code") {
                    document.getElementById("<%= txtBrand_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblBrandname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBrand_Code.ClientID %>").focus();
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
    <script type="text/javascript" src="../../JS/DateValidation.js">
    </script>
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSave.ClientID %>").disabled = true;
        }

        function EnableButton() {
            document.getElementById("<%=btnSave.ClientID %>").disabled = false;
        }


    </script>
    <script type="text/javascript" language="javascript">
        function disableClick(elem) {

            elem.disabled = true;
            $("#loader").show();
        }

        function qntl(e) {
            if (e.keyCode == 13) {
                $("#<%=txtTruck_NO.ClientID %>").focus();
            }
        }
        function carporatesale(e) {
            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtcarporateSale.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtcarporateSale.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtcarporateSale.ClientID %>").val(unit);
                __doPostBack("txtcarporateSale", "TextChanged");

            }
        }
        function millcode(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtMILL_CODE.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtMILL_CODE.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtMILL_CODE.ClientID %>").val(unit);
                __doPostBack("txtMILL_CODE", "TextChanged");

            }
        }
        function distance(e) {
            if (e.keyCode == 13) {
                debugger;

                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }

        }
        function Brand_Code(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtBrand_Code.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBrand_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtBrand_Code.ClientID %>").val(unit);
                __doPostBack("txtBrand_Code", "TextChanged");
            }
        }
        function millstatecode(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtMillGstStateCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtMillGstStateCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtMillGstStateCode.ClientID %>").val(unit);
                __doPostBack("txtMillGstStateCode", "TextChanged");

            }
        }
        function purcno(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtPurcNo.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtPurcNo.ClientID %>").val();
                unit = "0" + unit;
                $("#<%=txtPurcNo.ClientID %>").val(unit);
                __doPostBack("txtPurcNo", "TextChanged");

            }
        }
        function gstcode(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtGstRate.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtGstRate.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtGstRate.ClientID %>").val(unit);
                __doPostBack("txtGstRate", "TextChanged");

            }
        }
        function MemoGSTRate(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtMemoGSTRate.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtMemoGSTRate.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtMemoGSTRate.ClientID %>").val(unit);
                __doPostBack("txtMemoGSTRate", "TextChanged");

            }
        }
        function getpass(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtGETPASS_CODE.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtGETPASS_CODE.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtGETPASS_CODE.ClientID %>").val(unit);
                __doPostBack("txtGETPASS_CODE", "TextChanged");

            }

        }
        function statecode(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                function narration1(e) {
                    if (e.keyCode == 112) {
                        debugger;
                        e.preventDefault();
                        $("#<%=pnlPopup.ClientID %>").show();
                        $("#<%=btntxtNARRATION1.ClientID %>").click();

                    }

                }

                function narration3(e) {
                    if (e.keyCode == 112) {
                        debugger;
                        e.preventDefault();
                        $("#<%=pnlPopup.ClientID %>").show();
                        $("#<%=btntxtNARRATION3.ClientID %>").click();

                    }

                } $("#<%=btntxtGetpassGstStateCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtGetpassGstStateCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtGetpassGstStateCode.ClientID %>").val(unit);
                __doPostBack("txtGetpassGstStateCode", "TextChanged");

            }
        }
        function shipto(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtvoucher_by.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtvoucher_by.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtvoucher_by.ClientID %>").val(unit);
                __doPostBack("txtvoucher_by", "TextChanged");

            }
        }
        function shiptostatecode(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtVoucherbyGstStateCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtVoucherbyGstStateCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtVoucherbyGstStateCode.ClientID %>").val(unit);
                __doPostBack("txtVoucherbyGstStateCode", "TextChanged");

            }
        }
        function narration4(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtNARRATION4.ClientID %>").click();

            }

        }
        function narration1(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtNARRATION1.ClientID %>").click();

            }

        }
        function narration2(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtNARRATION2.ClientID %>").click();

            }

        }
        function narration3(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtNARRATION3.ClientID %>").click();

            }

        }
        function salebillstatecode(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtSalebilltoGstStateCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtSalebilltoGstStateCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtSalebilltoGstStateCode.ClientID %>").val(unit);
                __doPostBack("txtSalebilltoGstStateCode", "TextChanged");

            }
        }
        function grade(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtGRADE.ClientID %>").click();

            }

        }
        function transport(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtTRANSPORT_CODE.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtTRANSPORT_CODE.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtTRANSPORT_CODE.ClientID %>").val(unit);
                __doPostBack("txtTRANSPORT_CODE", "TextChanged");

            }
        }
        function transportstatecode(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtTransportGstStateCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtTransportGstStateCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtTransportGstStateCode.ClientID %>").val(unit);
                __doPostBack("txtTransportGstStateCode", "TextChanged");

            }
        }
        function vasuli(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtVasuliAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtVasuliAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtVasuliAc.ClientID %>").val(unit);
                __doPostBack("txtVasuliAc", "TextChanged");

            }
        }
        function docode(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtDO_CODE.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtDO_CODE.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtDO_CODE.ClientID %>").val(unit);
                __doPostBack("txtDO_CODE", "TextChanged");

            }
        }
        function broker(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtBroker_CODE.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBroker_CODE.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtBroker_CODE.ClientID %>").val(unit);
                __doPostBack("txtBroker_CODE", "TextChanged");

            }
        }
        function item(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtitem_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtitem_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtitem_Code.ClientID %>").val(unit);
                __doPostBack("txtitem_Code", "TextChanged");

            }
        }
        function salebillto(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "txtSaleBillTo";
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnSearch.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtSaleBillTo.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtSaleBillTo.ClientID %>").val(unit);
                __doPostBack("txtSaleBillTo", "TextChanged");

            }
        }
        function bankcode(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtBANK_CODE.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBANK_CODE.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtBANK_CODE.ClientID %>").val(unit);
                __doPostBack("txtBANK_CODE", "TextChanged");

            }
        }
        function billto(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtbill_To.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBill_To.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtBill_To.ClientID %>").val(unit);
                __doPostBack("txtBill_To", "TextChanged");

            }
        }
        function UTR(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtUTRNo.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtUTRNo.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtUTRNo.ClientID %>").val(unit);
                __doPostBack("txtUTRNo", "TextChanged");

            }
        }
        function TDSAc(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtTDSAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtTDSAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtTDSAc.ClientID %>").val(unit);
                __doPostBack("txtTDSAc", "TextChanged");

            }
        }
        function CashDiff(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtCashDiffAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtCashDiffAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtCashDiffAc.ClientID %>").val(unit);
                __doPostBack("txtCashDiffAc", "TextChanged");

            }
        }
        function UTRPrint() {
            window.open('../Transaction/pgeUtrentryxml.aspx?utrid=0&Action=2');
        }
        function DoOPen(DO) {
            var Action = 1;
            window.open('../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx?DO=' + DO + '&Action=' + Action, "_self");
        }
        function SBEnable() {
            debugger;
            $("#<%=btntxtCashDiffAc.ClientID %>").EnableButton =false

        }
    </script>

    <script type="text/javascript">
        function Confirm13() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            // if (confirm(" Print=>Ok / preprint=>Cancel ")) {
            //   confirm_value.value = "Yes";
            // document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            //}
            //else {
            //  confirm_value.value = "No";
            //document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
            //}
            document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            debugger;
            var paymentto = document.getElementById("<%= hdnfpaymenttosb.ClientID %>").value;
            var millcode = document.getElementById("<%= txtMILL_CODE.ClientID %>").value;
            var tenderno = document.getElementById("<%= hdnftenderno.ClientID %>").value;

            if (paymentto != millcode || (tenderno != millcode && tenderno != "2")) {
                payment();
            }
            else {
                document.getElementById("<%= hdnfpaymentcomnfirm.ClientID %>").value = "N";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function Confirm12() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            // if (confirm(" Print=>Ok / preprint=>Cancel ")) {
            //   confirm_value.value = "Yes";
            // document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
             //}
             //else {
             //  confirm_value.value = "No";
             //document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
             //}
             document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
             debugger;
             var paymentto = document.getElementById("<%= hdnfpaymenttosb.ClientID %>").value;
            var millcode = document.getElementById("<%= txtMILL_CODE.ClientID %>").value;
             var tenderno = document.getElementById("<%= hdnftenderno.ClientID %>").value;

             if (paymentto != millcode || (tenderno != millcode && tenderno != "2")) {
                 payment();
             }
             else {
                 //document.getElementById("<%= hdnfpaymentcomnfirm.ClientID %>").value = "N";
                 payment();
            }
            document.forms[0].appendChild(confirm_value);
        }
        function Confirm123() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            // if (confirm(" Print=>Ok / preprint=>Cancel ")) {
            //   confirm_value.value = "Yes";
            // document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            //}
            //else {
            //  confirm_value.value = "No";
            //document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
            //}
            document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            debugger;
            var paymentto = document.getElementById("<%= hdnfpaymenttosb.ClientID %>").value;
            var millcode = document.getElementById("<%= txtMILL_CODE.ClientID %>").value;

            if (paymentto != millcode) {
                payment();
            }
            else {
                document.getElementById("<%= hdnfpaymentcomnfirm.ClientID %>").value = "N";
            }
            document.forms[0].appendChild(confirm_value);
        }


        function Confirm1() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            // if (confirm(" Print=>Ok / preprint=>Cancel ")) {
            //   confirm_value.value = "Yes";
            // document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            //}
            //else {
            //  confirm_value.value = "No";
            //  document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
            //}
            document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            document.forms[0].appendChild(confirm_value);
        }

        function Confirm1234() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            // if (confirm(" Print=>Ok / preprint=>Cancel ")) {
            //   confirm_value.value = "Yes";
            // document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            //}
            //else {
            //  confirm_value.value = "No";
            //  document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
            //}
            document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <script type="text/javascript">
        function payment() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("BillTo ShipTo same party")) {
                confirm_value.value = "Yes";
                document.getElementById("<%= hdnfpaymentcomnfirm.ClientID %>").value = "Y";
            }
            else {
                confirm_value.value = "No";
                document.getElementById("<%= hdnfpaymentcomnfirm.ClientID %>").value = "N";
            }
            document.forms[0].appendChild(confirm_value);
        }

    </script>

    <script type="text/javascript">
        function DeleteConform() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= hdnfDodoc.ClientID %>").value;
            var Autoid = document.getElementById("<%= hdnfDoid.ClientID %>").value;
            var Sale = document.getElementById("<%= hdnfsaleid.ClientID %>").value;
            var PSLV = "";
            var voucher = $("#<%=lblVoucherType.ClientID %>").text();
            var voucherNo = $("#<%=lblVoucherNo.ClientID %>").text();
            var DESP_TYPE = $("#<%=drpDOType.ClientID %>").val();
            if (voucher == "PS") {
                PSLV = document.getElementById("<%= hdnfpurcid.ClientID %>").value;

            }
            else {
                PSLV = document.getElementById("<%= hdnfcommid.ClientID %>").value;
            }
            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';
            var AUTO_VOUCHER = '<%= Session["AUTO_VOUCHER"] %>';

            var XML = "<ROOT><DoHead doc_no='" + DocNo + "' doid='" + Autoid + "' company_code='" + Company_Code + "' " +
                "Year_Code='" + Year_Code + "' VoucherType='" + voucher + "' PSLV='" + PSLV + "' SaleId='" + Sale + "' AUTO_VOUCHER='" + AUTO_VOUCHER + "' " +
                " voucherNo='" + voucherNo + "'></DoHead></ROOT>";
            var spname = "DeliveryOrder";
            var RecordStatus = document.getElementById("<%= btnDelete.ClientID %>").value;
            ProcessXML(XML, RecordStatus, spname, DESP_TYPE);
        }
        function Validate() {
            debugger;
            $("#loader").show();
            // Validation
            var StartDate = '<%= Session["Start_Date"] %>';
            var EndDate = '<%= Session["End_Date"] %>';

            var d = $("#<%=txtDOC_DATE.ClientID %>").val();
            var DocDates = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

            StartDate = StartDate.slice(6, 11) + "/" + StartDate.slice(3, 5) + "/" + StartDate.slice(0, 2);
            EndDate = EndDate.slice(6, 11) + "/" + EndDate.slice(3, 5) + "/" + EndDate.slice(0, 2);

            if (DocDates >= StartDate && DocDates <= EndDate) {
            }
            else {
                $("#<%=txtDOC_DATE.ClientID %>").focus();
                alert('Not A Valid Date Range')
                $("#loader").hide();
                return false;
            }


            var txtvoucher_by = $("#<%=txtvoucher_by.ClientID %>").val();
            if (txtvoucher_by == "2") {
                $("#<%=txtvoucher_by.ClientID %>").focus();
                $("#loader").hide(); return false;
            }



            var txtitem_Code = $("#<%=txtitem_Code.ClientID %>").val();
            if ((txtitem_Code == "") || txtitem_Code == "0") {
                $("#<%=txtitem_Code.ClientID %>").focus();
                $("#loader").hide();
                return false;

            }

            var txtGstRate = $("#<%=txtGstRate.ClientID %>").val() == "" ? 0 : $("#<%=txtGstRate.ClientID %>").val();
            if ((txtGstRate == "") || txtGstRate == "0") {
                $("#<%=txtGstRate.ClientID %>").focus();
                $("#loader").hide(); return false;
            }

            var txtMillGstStateCode = $("#<%=txtMillGstStateCode.ClientID %>").val();
            if ((txtMillGstStateCode == "") || txtMillGstStateCode == "0") {
                $("#<%=txtMillGstStateCode.ClientID %>").focus();
                $("#loader").hide(); return false;
            }

            var txtTRANSPORT_CODE = $("#<%=txtTRANSPORT_CODE.ClientID %>").val();
            if ((txtTRANSPORT_CODE == "") || txtTRANSPORT_CODE == "0") {
                $("#<%=txtTRANSPORT_CODE.ClientID %>").focus();
                $("#loader").hide(); return false;
            }

            var txtSaleBillTo = $("#<%=txtSaleBillTo.ClientID %>").val();
            if ((txtSaleBillTo == "") || txtSaleBillTo == "0") {
                $("#<%=txtSaleBillTo.ClientID %>").focus();
                $("#loader").hide(); return false;
            }

            var txtTransportGstStateCode = $("#<%=txtTransportGstStateCode.ClientID %>").val();

            if ((txtTransportGstStateCode == "") || txtTransportGstStateCode == "0") {
                $("#<%=txtTransportGstStateCode.ClientID %>").focus();
                $("#loader").hide(); return false;
            }

            var txtGetpassGstStateCode = $("#<%=txtGetpassGstStateCode.ClientID %>").val();
            if ((txtGetpassGstStateCode == "") || txtGetpassGstStateCode == "0") {
                $("#<%=txtGetpassGstStateCode.ClientID %>").focus();
                $("#loader").hide(); return false;
            }

            var txtVoucherbyGstStateCode = $("#<%=txtVoucherbyGstStateCode.ClientID %>").val();
            if ((txtVoucherbyGstStateCode == "") || txtVoucherbyGstStateCode == "0") {
                $("#<%=txtVoucherbyGstStateCode.ClientID %>").focus();
                $("#loader").hide(); return false;
            }


            //   var txtVoucherbyGstStateCode = $("#<%=txtVoucherbyGstStateCode.ClientID %>").val();
            //  if ((txtVoucherbyGstStateCode == "") || txtVoucherbyGstStateCode == "0") {
            //     $("#<%=txtVoucherbyGstStateCode.ClientID %>").focus();
            //      $("#loader").hide();return false;
            // }

            var txtSalebilltoGstStateCode = $("#<%=txtSalebilltoGstStateCode.ClientID %>").val();

            if ((txtSalebilltoGstStateCode == "") || txtSalebilltoGstStateCode == "0") {
                $("#<%=txtSalebilltoGstStateCode.ClientID %>").focus();
                $("#loader").hide(); return false;
            }

            //var drpDOType = $("#<%=drpDOType.ClientID %>").val();

            //  if (drpDOType != "0") {
            //  $("#<%=drpDOType.ClientID %>").focus();
            //  //    $("#loader").hide();return false;
            //  }
            //   if (txtSalebilltoGstStateCode.Text != string.Empty && txtSalebilltoGstStateCode.Text != "0" && txtSaleBillTo.Text != "0" && txtSaleBillTo.Text != string.Empty)

            var txtMILL_CODE = $("#<%=txtMILL_CODE.ClientID %>").val();

            if ((txtMILL_CODE == "") || txtMILL_CODE == "0") {
                $("#<%=txtMILL_CODE.ClientID %>").focus();
                $("#loader").hide(); return false;
            }
            debugger;
            var txtmemogstrate = $("#<%=txtMemoAdvance.ClientID %>").val();

            if (txtmemogstrate > 0) {

                var txtMemoGSTRate1 = $("#<%=txtMemoGSTRate.ClientID %>").val();

                if ((txtMemoGSTRate1 == "") || txtMemoGSTRate1 == "0") {
                    $("#<%=txtMemoGSTRate.ClientID %>").focus();
                    $("#loader").hide(); return false;
                }
            }


            var txtGstRate = $("#<%=txtGstRate.ClientID %>").val();

            if ((txtGstRate == "") || txtGstRate == "0") {
                $("#<%=txtGstRate.ClientID %>").focus();
                $("#loader").hide(); return false;
            }
            var txtGETPASS_CODE = $("#<%=txtGETPASS_CODE.ClientID %>").val();

            if ((txtGETPASS_CODE == "") || txtGETPASS_CODE == "0" && txtGETPASS_CODE == "2") {
                $("#<%=txtGETPASS_CODE.ClientID %>").focus();
                $("#loader").hide(); return false;
            }

            var txtvoucher_by = $("#<%=txtvoucher_by.ClientID %>").val();
            if ((txtvoucher_by == "") || txtvoucher_by == "0") {
                $("#<%=txtvoucher_by.ClientID %>").focus();
                $("#loader").hide(); return false;
            }

            var txtGRADE = $("#<%=txtGRADE.ClientID %>").val();
            if ((txtGRADE == "") || txtGRADE == "0") {
                $("#<%=txtGRADE.ClientID %>").focus();
                $("#loader").hide(); return false;
            }

            var txtquantal = $("#<%=txtquantal.ClientID %>").val();
            if ((txtquantal == "") || txtquantal == "0") {
                $("#<%=txtquantal.ClientID %>").focus();
                $("#loader").hide(); return false;
            }

            var txtPACKING = $("#<%=txtPACKING.ClientID %>").val();
            if ((txtPACKING == "") || txtPACKING == "0") {
                $("#<%=txtPACKING.ClientID %>").focus();
                $("#loader").hide(); return false;
            }

            var txtexcise_rate = $("#<%=txtexcise_rate.ClientID %>").val();
            if ((txtexcise_rate == "") || txtexcise_rate == "0") {
                $("#<%=txtexcise_rate.ClientID %>").focus();
                $("#loader").hide(); return false;
            }
            var txtPurcNo = $("#<%=txtPurcNo.ClientID %>").val();
            if ((txtPurcNo == "") || txtPurcNo == "0") {
                $("#<%=txtPurcNo.ClientID %>").focus();
                $("#loader").hide(); return false;
            }
            var txtDistance = $("#<%=txtDistance.ClientID %>").val() == "0.00" ? 0 : $("#<%=txtDistance.ClientID %>").val();


            //var drpDOType = $("#<%=drpDOType.ClientID %>").val();
            //if (drpDOType == "DI") {
            //  var count = $("#<%=drpDOType.ClientID %>")
            //$("#<%=drpDOType.ClientID %>").focus();
            // $("#loader").hide();return false;
            // }

            var txtSALE_RATE = $("#<%=txtSALE_RATE.ClientID %>").val();
            var salerate = txtSALE_RATE.replace(".00", "");
            if ((txtSALE_RATE == "") || txtSALE_RATE == "0" || txtSALE_RATE == "0.00" || salerate.length < 4) {
                $("#<%=txtSALE_RATE.ClientID %>").focus();
                $("#loader").hide(); return false;
            }
            var SELFAC = '<%= Session["SELF_AC"] %>';

            if (txtvoucher_by == SELFAC && txtSaleBillTo == SELFAC) {
                var txtBrand_Code = $("#<%=txtBrand_Code.ClientID %>").val();
                if ((txtBrand_Code == "") || txtBrand_Code == "0" || txtBrand_Code == "0.00") {
                    $("#<%=txtBrand_Code.ClientID %>").focus();
                    $("#loader").hide(); return false;
                }
            }
            var txtVasuliAmount1 = $("#<%=txtVasuliAmount1.ClientID %>").val();
            var txtVasuliAc = $("#<%=txtVasuliAc.ClientID %>").val() == "" ? 0 : $("#<%=txtVasuliAc.ClientID %>").val();
            if (txtVasuliAmount1 != "0" && txtVasuliAmount1 != "") {
                if (txtVasuliAc == "" || txtVasuliAc == "0") {
                    $("#<%=txtVasuliAc.ClientID %>").focus();
                    $("#loader").hide(); return false;
                }

            }
            var txtCashDiff = $("#<%=txtCashDiff.ClientID %>").val();
            var txtCashDiffAc = $("#<%=txtCashDiffAc.ClientID %>").val() == "" ? 0 : $("#<%=txtCashDiffAc.ClientID %>").val();
            if (txtCashDiff != "0" && txtCashDiff != "") {
                if (txtCashDiffAc == "" || txtCashDiffAc == "0" || txtCashDiffAc == "2" || txtCashDiffAc == "02") {
                    $("#<%=txtCashDiffAc.ClientID %>").focus();
                    $("#loader").hide(); return false;
                }

            }
            var PSTCS = $("#<%=txtTCSRate.ClientID %>").val();
            var PSTDS = $("#<%=txtPurchaseTDS.ClientID %>").val();
            //if (PSTCS > 0.00 && PSTDS > 0.00) {
            //    alert('Please Check Purchase TCS & TDS!')
            //    $("#loader").hide(); return false;
            //}
            var SBTCS = $("#<%=txtTCSRate_Sale.ClientID %>").val();
            var SBTDS = $("#<%=txtSaleTDS.ClientID %>").val();
            //if (SBTCS > 0.00 && SBTDS > 0.00) {
            //    alert('Please Check Sale TCS & TDS!')
            //    $("#loader").hide(); return false;
            //}
            // if ((txtVasuliAmount1 == "") || txtVasuliAmount1 == "0" || txtVasuliAmount1 == "0.00") {
            //
            //   $("#<%=txtVasuliAmount1.ClientID %>").focus();
            //     $("#loader").hide();return false;
            // }
            // else {
            //
            // }
            var gridView = "";
            var grdrow = "";
            if ($("#<%=drpDOType.ClientID %>").val() == "DI") {
                gridView = document.getElementById("<%=grdDetail.ClientID %>");

                grdrow = gridView.getElementsByTagName("tr");
            }
            var count = 0;
            var amount = 0;
            var millamt = $("#<%=lblMillAmount.ClientID %>").text();
            var saletds = $("#<%=txtSaleTDS.ClientID %>").text();
            var purchasetds = $("#<%=txtPurchaseTDS.ClientID %>").text();
            //  var millamt = $("#<%=lblMillAmtWOTCS.ClientID %>").text();

            var millamtTCS = $("#<%=lblMillAmtWOTCS.ClientID %>").text();
            if (grdrow.length > 2) {
                for (var i = 0; i < grdrow.length; i++) {

                    if (gridView.rows[i].cells[12].innerHTML == "D") {
                        count++;
                    }

                }
                if ((grdrow - 1) == count) {
                    alert('Please Add Dispatch Details!')
                    $("#loader").hide(); return false;
                }
            }

            if (grdrow.length - 1 > 0) {
                for (var i = 1; i < grdrow.length; i++) {

                    if (gridView.rows[i].cells[12].innerHTML != "D" && gridView.rows[i].cells[12].innerHTML != "R") {
                        amount = parseFloat(parseFloat(amount) + parseFloat(gridView.rows[i].cells[7].innerHTML));
                    }

                }
                if (purchasetds != 0) {
                    if (amount != millamt) {
                        alert('Mill Amount Does Not match with detail amount!')
                        $("#loader").hide(); return false;
                    }
                }
                else {
                    if (amount != millamtTCS) {
                        alert('Mill Amount Does Not match with detail amount!')
                        $("#loader").hide(); return false;
                    }
                }
            }
            // if ($("#<%=drpDOType.ClientID %>").val() == 'DI') {

            var unit = $("#<%=lblSB_No.ClientID %>").text();

            if ($("#<%=lblSB_No.ClientID %>") == null || unit == "0" || unit == "") {

                //var confirm_value = document.createElement("INPUT");
                //confirm_value.type = "hidden";
                //confirm_value.name = "confirm_value";
                //if (confirm(" Sale Bill Genearate=>Ok / Not Generate=>Cancel ")) {
                //// confirm_value.value = "Yes";
                //   document.getElementById("<%= hdnfgeneratesalebill.ClientID %>").value = "Yes";
                // }
                //  else {
                //confirm_value.value = "No";
                document.getElementById("<%= hdnfgeneratesalebill.ClientID %>").value = "No";
            }
            //document.forms[0].appendChild(confirm_value);
            //  $("#loader").show();
            //}
            //else {
            //     document.getElementById("<%= hdnfgeneratesalebill.ClientID %>").value = "Yes";
            //   }
            // }
            return true;
            $("#loader").show();
        }
        function pagevalidation() {

            try {
               // $("#<%=txtSALE_RATE.ClientID %>").attr("disabled", "true");
                var doDoc_No = 0, saleDoc_No = 0, purcDoc_No = 0, commiDoc_No = 0, doid = 0, dodetailid = 0, saleid = 0, saledetailid = 0,
                   purcid = 0, purcdetailid = 0, commisionid = 0, GiD = 0, MaxGId = 0;
                var XML = "<ROOT>"; var spname = "DeliveryOrder";
                var RecordStatus = $("#<%=btnSave.ClientID %>").val();
                if (RecordStatus == "Update") {
                    doDoc_No = document.getElementById("<%= hdnfDodoc.ClientID %>").value;
                    doid = document.getElementById("<%= hdnfDoid.ClientID %>").value;
                    saleDoc_No = $("#<%=lblSB_No.ClientID %>").text() == "" ? 0 : $("#<%=lblSB_No.ClientID %>").text();
                    saleid = document.getElementById("<%= hdnfsaleid.ClientID %>").value;
                    saledetailid = document.getElementById("<%= hdnfsaledetailid.ClientID %>").value;
                    //dodetailid=
                    var typ = $("#<%=lblVoucherType.ClientID %>").text();
                    if (typ == "PS") {
                        purcDoc_No = $("#<%=lblVoucherNo.ClientID %>").text() == "" ? 0 : $("#<%=lblVoucherNo.ClientID %>").text();
                        purcid = document.getElementById("<%= hdnfpurcid.ClientID %>").value;
                        commiDoc_No = "0";
                        commisionid = "0";
                        purcdetailid = document.getElementById("<%= hdnfpurcdetailid.ClientID %>").value;
                    }
                    else {
                        purcDoc_No = "0";
                        purcid = "0";
                        commiDoc_No = $("#<%=lblVoucherNo.ClientID %>").text() == "" ? 0 : $("#<%=lblVoucherNo.ClientID %>").text();
                        commisionid = document.getElementById("<%= hdnfcommid.ClientID %>").value;
                        purcDoc_No = commiDoc_No;

                    }
                }


                var txtitem_Code = $("#<%=txtitem_Code.ClientID %>").val();
                var txtGstRate = $("#<%=txtGstRate.ClientID %>").val() == "" ? 0 : $("#<%=txtGstRate.ClientID %>").val();
                var txtMillGstStateCode = $("#<%=txtMillGstStateCode.ClientID %>").val();

                var txtTRANSPORT_CODE = $("#<%=txtTRANSPORT_CODE.ClientID %>").val();
                var txtSaleBillTo = $("#<%=txtSaleBillTo.ClientID %>").val();
                var txtTransportGstStateCode = $("#<%=txtTransportGstStateCode.ClientID %>").val();
                var txtGetpassGstStateCode = $("#<%=txtGetpassGstStateCode.ClientID %>").val();
                var txtVoucherbyGstStateCode = $("#<%=txtVoucherbyGstStateCode.ClientID %>").val();
                var txtSalebilltoGstStateCode = $("#<%=txtSalebilltoGstStateCode.ClientID %>").val();

                var txtMILL_CODE = $("#<%=txtMILL_CODE.ClientID %>").val();
                var txtGstRate = $("#<%=txtGstRate.ClientID %>").val();
                var txtGETPASS_CODE = $("#<%=txtGETPASS_CODE.ClientID %>").val();
                var txtvoucher_by = $("#<%=txtvoucher_by.ClientID %>").val();
                var txtGRADE = $("#<%=txtGRADE.ClientID %>").val();
                var txtquantal = $("#<%=txtquantal.ClientID %>").val();
                var txtPACKING = $("#<%=txtPACKING.ClientID %>").val();
                var txtexcise_rate = $("#<%=txtexcise_rate.ClientID %>").val() == "" ? 0.00 : $("#<%=txtexcise_rate.ClientID %>").val();
                var txtPurcNo = $("#<%=txtPurcNo.ClientID %>").val();
                var txtDistance = $("#<%=txtDistance.ClientID %>").val() == "" ? 0 : $("#<%=txtDistance.ClientID %>").val();
                var txtSALE_RATE = $("#<%=txtSALE_RATE.ClientID %>").val();
                var Purchase_Rate = $("#<%=txtpurchaserate.ClientID %>").val();
                var txtDistance = $("#<%=txtDistance.ClientID %>").val() == "" ? 0 : $("#<%=txtDistance.ClientID %>").val();
                var cashdiffvalue = $("#<%=txtCashDiff.ClientID %>").val() == "" ? 0 : $("#<%=txtCashDiff.ClientID %>").val();
                var txtVasuliAmount1 = $("#<%=txtVasuliAmount1.ClientID %>").val();
                var txtVasuliAc = $("#<%=txtVasuliAc.ClientID %>").val() == "" ? 0 : $("#<%=txtVasuliAc.ClientID %>").val();
                var gridView = "";
                var grdrow = "";
                if ($("#<%=drpDOType.ClientID %>").val() == "DI") {
                    gridView = document.getElementById("<%=grdDetail.ClientID %>");
                    grdrow = gridView.getElementsByTagName("tr");
                }
                var count = 0; var amount = 0; var millamt = $("#<%=lblMillAmount.ClientID %>").text();
                $("#loader").show();
                //Declare variable and values
                document.getElementById("<%=lblSB_No.ClientID %>").innerText = document.getElementById("<%= hdnfSB_No.ClientID %>").value;
                var millamtTCS = $("#<%=lblMillAmtWOTCS.ClientID %>").text() == "" ? 0 : $("#<%=lblMillAmtWOTCS.ClientID %>").text();
                var DOC_NO = $("#<%=txtdoc_no.ClientID %>").val();
                var d = $("#<%=txtDOC_DATE.ClientID %>").val();
                var DOC_DATE = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

                var d = $("#<%=txtdo_date.ClientID %>").val();
                var Do_DATE = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

                var d = $("#<%=txtPurchase_Date.ClientID %>").val();
                var PUR_DATE = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                var DESP_TYPE = $("#<%=drpDOType.ClientID %>").val();
                var Delivery_Type = $("#<%=drpDeliveryType.ClientID %>").val();

                var d = $("#<%=txtMillInv_Date.ClientID %>").val();
                var MillInv_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

                var Inv_Chk = $("#<%=chkInv_Chk.ClientID %>").is(":checked");
                if (Inv_Chk == true) {
                    Inv_Chk = "Y";
                }
                else {
                    Inv_Chk = "N";
                }
                var FRIEGHT_RATE = parseFloat($("#<%=txtFrieght.ClientID %>").val() == "" ? 0 : $("#<%=txtFrieght.ClientID %>").val());
                var FRIEGHT_AMOUNT = parseFloat($("#<%=txtFrieghtAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtFrieghtAmount.ClientID %>").val());
                var VASULI_AMOUNT = parseFloat($("#<%=txtVasuliAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtVasuliAmount.ClientID %>").val());
                var VASULI_RATE = parseFloat($("#<%=txtVasuliRate.ClientID %>").val() == "" ? 0 : $("#<%=txtVasuliRate.ClientID %>").val());
                var MEMO_ADVANCE = parseFloat($("#<%=txtMemoAdvance.ClientID %>").val() == "" ? 0 : $("#<%=txtMemoAdvance.ClientID %>").val());
                var Ac_Code = $("#<%=txtvoucher_by.ClientID %>").val();
                var PACKING = $("#<%=txtPACKING.ClientID %>").val();
                var BAGS = $("#<%=txtBAGS.ClientID %>").val();
                var mill_rate = parseFloat($("#<%=txtmillRate.ClientID %>").val());

                var Tender_Commission = parseFloat($("#<%=txtCommission.ClientID %>").val() == "" ? 0 : $("#<%=txtCommission.ClientID %>").val());
                var SALE_RATE = parseFloat($("#<%=txtSALE_RATE.ClientID %>").val() == "" ? 0 : $("#<%=txtSALE_RATE.ClientID %>").val());
                var MILL_AMOUNT = parseFloat(txtquantal * (mill_rate + txtexcise_rate));

                var DIFF_RATE = parseFloat($("#<%=lblDiffrate.ClientID %>").text() == "" ? 0 : $("#<%=lblDiffrate.ClientID %>").text());
                var DIFF_AMOUNT = parseFloat($("#<%=txtDIFF_AMOUNT.ClientID %>").val() == "" ? 0 : $("#<%=txtDIFF_AMOUNT.ClientID %>").val());
                var VASULI_RATE_1 = parseFloat($("#<%=txtVasuliRate1.ClientID %>").val() == "" ? 0 : $("#<%=txtVasuliRate1.ClientID %>").val());

                var EWayBill_No = $("#<%=txtEWayBill_No.ClientID %>").val();

                var SaleBillTo = $("#<%=txtSaleBillTo.ClientID %>").val() == "" ? 0 : $("#<%=txtSaleBillTo.ClientID %>").val();

                var MM_CC = $("#<%=drpCC.ClientID %>").val();
                // var Party_Commission_Rate = $("#<%=txtVasuliAmount1.ClientID %>").val();
                var MM_Rate = parseFloat($("#<%=txtMemoAdvanceRate.ClientID %>").val() == "" ? 0 : $("#<%=txtMemoAdvanceRate.ClientID %>").val());
                var PAN_NO = $("#<%=txtPanNo.ClientID %>").val();
                var DO_CODE = $("#<%=txtDO_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtDO_CODE.ClientID %>").val();
                var BROKER_CODE = $("#<%=txtBroker_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtBroker_CODE.ClientID %>").val();
                var TRUCK_NO = $("#<%=txtTruck_NO.ClientID %>").val();
                TRUCK_NO = TRUCK_NO.toUpperCase();
                var TRANSPORT_CODE = $("#<%=txtTRANSPORT_CODE.ClientID %>").val();
                var SaleBillTransport = "";
                if (MM_CC == "Credit") {
                    SaleBillTransport = TRANSPORT_CODE;
                } else {
                    SaleBillTransport = 1;
                }

                var Tender_Commission_Amount = parseFloat(Tender_Commission * txtquantal);
                var OVTransportCode = "";
                if (MM_CC == "Cash") {
                    OVTransportCode = TRANSPORT_CODE;
                } else {
                    OVTransportCode = 1;
                }
                var NARRATION1 = $("#<%=txtNARRATION1.ClientID %>").val();
                var NARRATION2 = $("#<%=txtNARRATION2.ClientID %>").val();
                var NARRATION3 = $("#<%=txtNARRATION3.ClientID %>").val();
                var NARRATION4 = $("#<%=txtNARRATION4.ClientID %>").val();
                var NARRATION5 = $("#<%=txtNarration5.ClientID %>").val();
                var SBNARRATION = $("#<%=txtsbnarration.ClientID %>").val();

                var purc_order = $("#<%=txtPurcOrder.ClientID %>").val();

                var VoucherBrokrage = $("#<%=txtVoucherBrokrage.ClientID %>").val() == "" ? 0 : $("#<%=txtVoucherBrokrage.ClientID %>").val();
                var VoucherServiceCharge = $("#<%=txtVoucherServiceCharge.ClientID %>").val() == "" ? 0 : $("#<%=txtVoucherServiceCharge.ClientID %>").val();
                var VoucherRateDiffRate = $("#<%=txtVoucherL_Rate_Diff.ClientID %>").val() == "" ? 0 : $("#<%=txtVoucherL_Rate_Diff.ClientID %>").val();
                var VoucherRateDiffAmt = $("#<%=txtVoucherRATEDIFFAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtVoucherRATEDIFFAmt.ClientID %>").val();
                var VoucherBankCommRate = $("#<%=txtVoucherCommission_Rate.ClientID %>").val() == "" ? 0 : $("#<%=txtVoucherCommission_Rate.ClientID %>").val();
                var VoucherBankCommAmt = $("#<%=txtVoucherBANK_COMMISSIONAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtVoucherBANK_COMMISSIONAmt.ClientID %>").val();
                var VoucherInterest = $("#<%=txtVoucherRATEDIFFAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtVoucherRATEDIFFAmt.ClientID %>").val();
                var VoucherTransport = $("#<%=txtVoucherRATEDIFFAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtVoucherRATEDIFFAmt.ClientID %>").val();
                var VoucherOtherExpenses = $("#<%=txtVoucherRATEDIFFAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtVoucherRATEDIFFAmt.ClientID %>").val();

                var EWay_BillChk = $("#<%=chkEWayBill.ClientID %>").is(":checked");
                if (EWay_BillChk == true) {
                    EWay_BillChk = "Y";
                }
                else {
                    EWay_BillChk = "N";
                }
                var TDSCut_Chk = $("#<%=chkTDSCutByUs.ClientID %>").is(":checked");
                if (TDSCut_Chk == true) {
                    TDSCut_Chk = "Y";
                }
                else {
                    TDSCut_Chk = "N";
                }


                var MillInvoiceno = $("#<%=txtMillInvoiceno.ClientID %>").val();
                var MillEwayBill = $("#<%=txtMillEwayBill_No.ClientID %>").val();
                var ISEInvoice = "";
                if (MillEwayBill == "") {
                    ISEInvoice = "N";
                }
                else {
                    ISEInvoice = "Y";
                }
                var VASULI_AMOUNT_1 = parseFloat($("#<%=txtVasuliAmount1.ClientID %>").val() == "" ? 0 : $("#<%=txtVasuliAmount1.ClientID %>").val());
                var Company_Code = '<%= Session["Company_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';

                var Branch_Code = '<%= Session["Branch_Code"] %>';
                var AUTO_VOUCHER = '<%= Session["AUTO_VOUCHER"] %>';
                var SELFAC = '<%= Session["SELF_AC"] %>';
                var CompanyStateCode = '<%= Session["CompanyGSTStateCode"] %>';
                var USER = '<%= Session["user"] %>';
                var TwoAcId = '<%= Session["SELFID"] %>';
                var CASHID = '<%= Session["CASHID"] %>';
                var freightac = '<%=Session["Freight_Ac"] %>';
                var freightac_id = '<%=Session["Freight_Acid"] %>';
                var Transporttdcac = 0;
                var Transporttdcacid = 0;


                var DIFF = parseFloat($("#<%=lblDiffrate.ClientID %>").text() == "" ? 0 : $("#<%=lblDiffrate.ClientID %>").text());
                var LESS_DIFF = parseFloat(((SALE_RATE - FRIEGHT_RATE) - mill_rate) * txtquantal);
                var LESSDIFFOV = parseFloat(DIFF_RATE * txtquantal);
                var Driver_Mobile = $("#<%=txtDriverMobile.ClientID %>").val();

                var Carporate_Sale_No = $("#<%=txtcarporateSale.ClientID %>").val() == "" ? 0 : $("#<%=txtcarporateSale.ClientID %>").val();
                var WhoseFrieght = $("#<%=ddlFrieghtType.ClientID %>").val();
                var UTR_Year_Code = $("#<%=lblUTRYearCode.ClientID %>").val() == "" ? 0 : $("#<%=lblUTRYearCode.ClientID %>").val();
                var Carporate_Sale_Year_Code = $("#<%=lblCSYearCode.ClientID %>").val();
                if (WhoseFrieght = "undefined") {
                    WhoseFrieght = "";
                }

                var memo_no = $("#<%=lblMemoNo.ClientID %>").text() == "" ? 0 : $("#<%=lblMemoNo.ClientID %>").text();
                var BillTo = $("#<%=txtBill_To.ClientID %>").val() == "" ? 0 : $("#<%=txtBill_To.ClientID %>").val();
                var voucher_type = $("#<%=lblVoucherType.ClientID %>").text();
                var voucherlbl = $("#<%=lblVoucherNo.ClientID %>").text();

                var myNarration = "Please Debit The Same Amount in our A/c";
                if (DESP_TYPE == "DO") {
                    myNarration = "";
                }
                var myNarration2 = "";
                var myNarration3 = "";
                var myNarration4 = "";
                var vouchnarration = "";
                var millShortName = document.getElementById("<%= hdnfmillshortname.ClientID %>").value;
                var hdnfSaleValue = document.getElementById("<%= hdnfgeneratesalebill.ClientID %>").value;
                if (WhoseFrieght == "O") {
                    vouchnarration = millShortName + " (S.R" + SALE_RATE + " " + FRIEGHT_RATE + " M.R" + mill_rate + ")" + txtquantal;
                }
                else {
                    vouchnarration = "Qntl " + txtquantal + "  " + millShortName + "(M.R." + mill_rate + " P.R." + SALE_RATE + ")";
                }

                var VOUCHERAMOUNT = parseFloat(DIFF_AMOUNT);
                //var MILL_AMOUNT = $("#<%=txtVasuliAmount1.ClientID %>").val();
                // var city_code = $("#<%=txtVasuliAmount1.ClientID %>").val();
                // var From_Place = $("#<%=txtVasuliAmount1.ClientID %>").val();
                // var city_code2 = $("#<%=txtVasuliAmount1.ClientID %>").val();
                //var To_Place = $("#<%=txtVasuliAmount1.ClientID %>").val();

                //var newsbno = parseInt($("#<%=txtnewsbno.ClientID %>").val() == "" ? 0 : $("#<%=txtnewsbno.ClientID %>").val());

                var newsbno = 0;
                var d = $("#<%=txtnewsbdate.ClientID %>").val();
                var newsbdate = "";
                if (d != "") {
                    newsbdate = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                }
                if (newsbdate == "1900/01/01") {
                    newsbdate = DOC_DATE;
                }

                var einvoiceno = $("#<%=txteinvoiceno.ClientID %>").val();
                if (einvoiceno == "&nbsp;") {
                    einvoiceno = "";
                }
                var ackno = $("#<%=txtackno.ClientID %>").val();
                if (ackno == "&nbsp;") {
                    ackno = "";
                }
                var d = $("#<%=txtEwayBill_ValidDate.ClientID %>").val();
                var EwayBill_ValidDate = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

                var brandcode = $("#<%=txtBrand_Code.ClientID %>").val() == "" ? 0 : $("#<%=txtBrand_Code.ClientID %>").val()
                var BILL_AMOUNT = parseFloat(txtquantal * mill_rate);
                var SUBTOTAL = BILL_AMOUNT;
                var pdsparty = document.getElementById("<%= hdnfPDSPartyCode.ClientID %>").value;
                var pdsunit = document.getElementById("<%= hdnfPDSUnitCode.ClientID %>").value;


                var PaymentShort = 0, GSTRate = 0, cgstrate = 0, sgstrate = 0, igstrate = 0, PDS = 0, PaymentGst = 0, PaymentTo = 0,
                    PaymentId = 0, TenderQty = 0, TenderID = 0, DetailID = 0, TenderdetailId = 0, pdsAcId = 0, UnitCity = 0;
                var pdsAcStateCode, pdsUnitStateCode, BilltoStateCode, pdsucId = "";
                var gstgridView = document.getElementById("<%=grdGstAutoId.ClientID %>");
                //var gstgrdrow = gstgridView.getElementsByTagName("tr");
                TenderdetailId = document.getElementById("<%= hdnfTenderDetailid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfTenderDetailid.ClientID %>").value;
                var TCS_Rate = parseFloat($("#<%=txtTCSRate.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSRate.ClientID %>").val());
                var TCSRate_sale = parseFloat($("#<%=txtTCSRate_Sale.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSRate_Sale.ClientID %>").val());
                var TDSAc = parseFloat($("#<%=txtTDSAc.ClientID %>").val() == "" ? 0 : $("#<%=txtTDSAc.ClientID %>").val());
                var CashdiffAc = parseFloat($("#<%=txtCashDiffAc.ClientID %>").val() == "" ? 0 : $("#<%=txtCashDiffAc.ClientID %>").val());
                var TDSRate = parseFloat($("#<%=txttdsrate.ClientID %>").val() == "" ? 0 : $("#<%=txttdsrate.ClientID %>").val());
                var TDSAmt = parseFloat($("#<%=txttdsamount.ClientID %>").val() == "" ? 0 : $("#<%=txttdsamount.ClientID %>").val());
                var TDSAmtDO = parseFloat($("#<%=txttdsamount.ClientID %>").val() == "" ? 0 : $("#<%=txttdsamount.ClientID %>").val());

                var TDSID = parseFloat($("#<%=hdnfTDSAcid.ClientID %>").val() == "" ? 0 : $("#<%=hdnfTDSAcid.ClientID %>").val());
                var CashDiffid = parseFloat($("#<%=hdnfCashDiffAcid.ClientID %>").val() == "" ? 0 : $("#<%=hdnfCashDiffAcid.ClientID %>").val());
                var SaleTDSrate = parseFloat($("#<%=txtSaleTDS.ClientID %>").val() == "" ? 0 : $("#<%=txtSaleTDS.ClientID %>").val());
                var PurchaseTDSrate = parseFloat($("#<%=txtPurchaseTDS.ClientID %>").val() == "" ? 0 : $("#<%=txtPurchaseTDS.ClientID %>").val());

                var mc = 0; gp = 0; st = 0; sb = 0; tc = 0; va = 0; bt = 0; ic = 0; docd = 0; bk = 0; cscode = 0; PurchAc = 0; PurcAcid = 0; SaleAc = 0; SaleAccid = 0;
                var Insured = $("#<%=drpInsured.ClientID %>").val();
                var Insurance = parseFloat($("#<%=txtInsurance.ClientID %>").val() == "" ? 0 : $("#<%=txtInsurance.ClientID %>").val()); 
                var SB_Other_Amount =parseFloat ($("#<%=txtSB_Other_Amount.ClientID %>").val()== "" ? 0 : $("#<%=txtSB_Other_Amount.ClientID %>").val()); 

                // Auto IDS

                mc = document.getElementById("<%= hdnfmc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfmc.ClientID %>").value;
                if (mc == "" || mc == "&nbsp;") {
                    mc = 0;
                }
                gp = document.getElementById("<%= hdnfgp.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfgp.ClientID %>").value;
                if (gp == "" || gp == "&nbsp;") {
                    gp = 0;
                }
                st = document.getElementById("<%= hdnfst.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfst.ClientID %>").value;
                if (st == "" || st == "&nbsp;") {
                    st = 0;
                }
                sb = document.getElementById("<%= hdnfsb.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfsb.ClientID %>").value;
                if (sb == "" || sb == "&nbsp;") {
                    sb = 0;
                }
                tc = document.getElementById("<%= hdnftc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnftc.ClientID %>").value;
                if (tc == "" || tc == "&nbsp;") {
                    tc = 0;
                }
                va = document.getElementById("<%= hdnfva.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfva.ClientID %>").value;
                if (va == "" || va == "&nbsp;") {
                    va = 0;
                }
                bt = document.getElementById("<%= hdnfbt.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbt.ClientID %>").value;
                if (bt == "" || bt == "&nbsp;") {
                    bt = 0;
                }
                ic = document.getElementById("<%= hdnfic.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfic.ClientID %>").value;
                if (ic == "" || ic == "&nbsp;") {
                    ic = 0;
                }
                docd = document.getElementById("<%= hdnfdocd.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfdocd.ClientID %>").value;
                if (docd == "" || docd == "&nbsp;") {
                    docd = 0;
                }
                bk = document.getElementById("<%= hdnfbk.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbk.ClientID %>").value;
                if (bt == "" || bt == "&nbsp;") {
                    bt = 0;
                }
                cscode = document.getElementById("<%= hdnfcscode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfcscode.ClientID %>").value;
                if (cscode == "" || cscode == "&nbsp;") {
                    cscode = 0;
                }
                PaymentId = document.getElementById("<%= hdnfpaymentid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpaymentid.ClientID %>").value;
                PaymentTo = document.getElementById("<%= hdnfpaymentTo.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpaymentTo.ClientID %>").value;
                PaymentShort = document.getElementById("<%= hdnfpaymentShort.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpaymentShort.ClientID %>").value;
                TenderQty = document.getElementById("<%= hdnfTenderQty.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfTenderQty.ClientID %>").value;
                TenderID = document.getElementById("<%= hdnfTenderID.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfTenderID.ClientID %>").value;
                PaymentGst = document.getElementById("<%= hdnfPaymentStateCode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfPaymentStateCode.ClientID %>").value;
                PDS = document.getElementById("<%= hdnfPDS.ClientID %>").value;
                pdsAcId = document.getElementById("<%= hdnfpdsacID.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpdsacID.ClientID %>").value;
                pdsucId = document.getElementById("<%= hdnfpdsunitID.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpdsunitID.ClientID %>").value;
                pdsAcStateCode = document.getElementById("<%= hdnfpdspartyStateCode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpdspartyStateCode.ClientID %>").value;
                pdsUnitStateCode = document.getElementById("<%= hdnfpdsunitStateCode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpdsunitStateCode.ClientID %>").value;
                BilltoStateCode = document.getElementById("<%= hdnfbilltoStateCode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbilltoStateCode.ClientID %>").value;
                UnitCity = document.getElementById("<%= hdnfUnitCity.ClientID %>").value;
                minRate = document.getElementById("<%= hdnfminRate.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfminRate.ClientID %>").value;
                maxRate = document.getElementById("<%= hdnfmaxRate.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfmaxRate.ClientID %>").value;
                debugger;

                if (minRate > 0) {
                    if (SALE_RATE >= minRate && SALE_RATE <= maxRate) {

                    }

                    else {
                        alert('Sale Rate is Excluded From Minimum and Maximum Range')
                        $("#loader").hide(); return false;
                    }

                }



                GSTRate = '<%= Session["GSTRate"] %>';
                igstrate = '<%= Session["igstrate"] %>';
                sgstrate = '<%= Session["sgstrate"] %>';
                cgstrate = '<%= Session["cgstrate"] %>';

                PurchAc = '<%= Session["PurchAc"] %>';
                PurcAcid = '<%= Session["Purcid"] %>';
                SaleAc = '<%= Session["SaleAc"] %>';
                SaleAccid = '<%= Session["Saleid"] %>';
                //for (var i = 1; i < gstgrdrow.length; i++) {
                //    GSTRate = gstgridView.rows[i].cells[2].innerHTML;
                //    igstrate = parseFloat(gstgridView.rows[i].cells[3].innerHTML);
                //    sgstrate = parseFloat(gstgridView.rows[i].cells[4].innerHTML);
                //    cgstrate = parseFloat(gstgridView.rows[i].cells[5].innerHTML);

                //}
                if (TDSCut_Chk == 'N') {
                    Transporttdcac = TRANSPORT_CODE;
                    Transporttdcacid = tc;
                }
                else {
                    Transporttdcac = '<%=Session["TransportTDS_AcCut"] %>';
                    Transporttdcacid = '<%=Session["transportTdsCutid"] %>';
                }

                if ($("#<%=btnSave.ClientID %>").val() == "Update") {
                    doid = $("#<%=hdnfDoid.ClientID %>").val();
                }
                var memocgstrate = 0;
                var memosgstrate = 0;
                var memoigstrate = 0;
                var memocgstamt = 0;
                var memosgstamt = 0;
                var memoigstamt = 0;
                debugger;
                if (MEMO_ADVANCE > 0) {
                    memocgstrate = document.getElementById("<%= hdnfmemocgst.ClientID %>").value;
                    memosgstrate = document.getElementById("<%= hdnfmemosgst.ClientID %>").value;
                    memoigstrate = document.getElementById("<%= hdnfmemoigst.ClientID %>").value;
                    if (CompanyStateCode == txtTransportGstStateCode) {

                        memocgstamt = Math.round(parseFloat(MEMO_ADVANCE * memocgstrate / 100));
                        memosgstamt = Math.round(parseFloat(MEMO_ADVANCE * memosgstrate / 100));
                        memoigstamt = 0;
                    }
                    else {
                        memocgstamt = 0;
                        memosgstamt = 0;
                        memoigstamt = Math.round(parseFloat(MEMO_ADVANCE * memoigstrate / 100));
                    }


                }

                var hsnnumber = document.getElementById("<%= hdnfhsnnumber.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfhsnnumber.ClientID %>").value;
                var autopurchasebill = document.getElementById("<%= hdnfAutoPurchaseBill.ClientID %>").value;

                var difrate = "";
                if (Delivery_Type == "N") {
                    difrate = Math.round(parseFloat(((SALE_RATE - FRIEGHT_RATE) - mill_rate) * txtquantal));
                }
                else {
                    difrate = Math.round(parseFloat((SALE_RATE - mill_rate) * txtquantal));
                }

                // Tender Calculation
                var Tenderbuyerid, Tenderbuyer, TenderUpdate = "";
                var TenderInsert = "";
                if (purc_order == "1") {
                    if (PDS == "P") {
                        Tenderbuyerid = pdsAcId;
                        Tenderbuyer = document.getElementById("<%= hdnfPDSPartyCode.ClientID %>").value;
                    }
                    else if (PDS == "C") {
                        if (txtvoucher_by == SELFAC && (txtSaleBillTo == "0" || txtSaleBillTo == "")) {
                            Tenderbuyerid = document.getElementById("<%= hdnfst.ClientID %>").value;
                            Tenderbuyer = txtvoucher_by;
                        }
                        else {
                            Tenderbuyerid = sb;
                            Tenderbuyer = txtSaleBillTo;
                        }
                    }
                    else {
                        if (txtvoucher_by == SELFAC && (txtSaleBillTo == "0" || txtSaleBillTo == "")) {
                            Tenderbuyerid = document.getElementById("<%= hdnfst.ClientID %>").value;
                            Tenderbuyer = txtvoucher_by;
                        }
                        else {
                            Tenderbuyerid = sb;
                            Tenderbuyer = txtSaleBillTo;
                        }
                    }

                XML = XML + "<TenderHead Tender_No='" + txtPurcNo + "' Company_Code='" + Company_Code + "' Buyer='" + Tenderbuyer + "' Buyer_Quantal='" + txtquantal + "' " +
               "Sale_Rate='" + SALE_RATE + "' Commission_Rate='" + Tender_Commission + "' Sauda_Date='" + DOC_DATE + "' Lifting_Date='" + DOC_DATE + "' ID='" + DetailID + "' " +
               "Narration='' Buyer_Party='" + BROKER_CODE + "' year_code='" + Year_Code + "' Branch_Id='" + Branch_Code + "' Delivery_Type='" + Delivery_Type + "' " +
               "tenderid='" + TenderID + "' tenderdetailid='" + TenderdetailId + "' buyerid='" + Tenderbuyerid + "' buyerpartyid='" + bk + "' " +
               "sub_broker='" + bk + "' sbr='" + TwoAcId + "' tcs_rate='0.00' gst_rate='0.00' tcs_amt='0.00' gst_amt='0.00' ShipTo='" + txtvoucher_by + "' shiptoid='" + st + "'> " +
               "<TenderDetail Tender_No='" + txtPurcNo + "' Buyer_Quantal='" + TenderQty + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' ID='1'/> " +
               "</TenderHead>";

            }

                // End Tender Calculation

                // Purchase calculation


            var PS_uc = 0; PS_ac = 0; voucherNo = 0;
            var Ledgerid = 0;
            if (GiD == "") {
                GiD = "0";
            }
            if (GiD != "0") {
                Ledgerid = GiD;

            }
            else {
                Ledgerid = MaxGId;
            }
                // Do Gledger Effect
            var Order_Code = 1;



            debugger;
            var aa = document.getElementById("<%= hdnfsupplieshortname.ClientID %>").value;
            var bb = document.getElementById("<%= hdnfsupplieshortname.ClientID %>").value;
                var CreditNarration = document.getElementById("<%= hdnfmillshortname.ClientID %>").value + " " + txtquantal + "L:" + TRUCK_NO
                    + "DO:" + doDoc_No + "F:" + MEMO_ADVANCE;

                var TransportNarration = txtquantal + " " + document.getElementById("<%= hdnfmillshortname.ClientID %>").value + " " +

                    +document.getElementById("<%= hdnftransportshortname.ClientID %>").value + "Lorry:" + TRUCK_NO + "Party:" + document.getElementById("<%= hdnfsalebilltoshortname.ClientID %>").value;
                var GSTNarrationPS = document.getElementById("<%= hdnfmillshortname.ClientID %>").value + " " + txtquantal + "L:"
                                                                    + TRUCK_NO + "DO:" + doDoc_No + "F:" + MEMO_ADVANCE
                                                                    + "SupplierName:" + aa;
                var GSTNarrationSB = document.getElementById("<%= hdnfmillshortname.ClientID %>").value + " Qntl:" + txtquantal + " GRADE:" + txtGRADE + " SALE RATE:" + SALE_RATE + " Lorry No:" + TRUCK_NO;

                // if (txtGETPASS_CODE != SELFAC) {
                var tdstransportnarration = "Qntl:" + txtquantal + ",LorryNo:" + TRUCK_NO + "," + document.getElementById("<%= hdnftransportshortname.ClientID %>").value + "," + PAN_NO;

                if (txtGETPASS_CODE != SELFAC) {
                    if (DESP_TYPE == "DI") {
                        for (var i = 1; i < grdrow.length; i++) {
                            var bankcode = gridView.rows[i].cells[4].innerHTML;
                            var amount = gridView.rows[i].cells[7].innerHTML;
                            var bc = gridView.rows[i].cells[14].innerHTML;

                            //Gledger_values = Gledger_values + "('DO','','" + doDoc_No + "','" + DOC_DATE + "','" + bankcode + "','0','','" + amount + "', " +
                            //                              " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C','" + txtvoucher_by + "',0,'" + Branch_Code + "','DO','" + doDoc_No + "'," +
                            //                              " '" + bc + "','0','0','0'," + Ledgerid + "),";

                            XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + bankcode + "' " +
                                "UNIT_code='0' NARRATION='' AMOUNT='" + amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + txtvoucher_by + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + bc + "' vc='0' progid='0' tranid='0'/>";

                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + txtvoucher_by + "' " +
                               "UNIT_code='0' NARRATION='' AMOUNT='" + amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                               "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + bankcode + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                               "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + st + "' vc='0' progid='0' tranid='0'/>";
                        }
                    }
                    if (VASULI_AMOUNT_1 > 0) {
                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + txtGETPASS_CODE + "' " +
                               "UNIT_code='0' NARRATION='' AMOUNT='" + VASULI_AMOUNT_1 + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                               "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='9999971' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                               "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + gp + "' vc='0' progid='12' tranid='0'/>";

                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + txtVasuliAc + "' " +
                              "UNIT_code='0' NARRATION='' AMOUNT='" + VASULI_AMOUNT_1 + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + txtGETPASS_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + va + "' vc='0' progid='12' tranid='0'/>";
                    }
                    if (TDSAc > 0) {
                        if (TDSAmt > 0) {
                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + TDSAc + "' " +
                                   "UNIT_code='0' NARRATION='TDS:" + tdstransportnarration + "' AMOUNT='" + TDSAmt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                   "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='9999971' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                   "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + TDSID + "' vc='0' progid='12' tranid='0'/>";

                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + Transporttdcac + "' " +
                                  "UNIT_code='0' NARRATION='TDS:" + tdstransportnarration + "' AMOUNT='" + TDSAmt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + Transporttdcac + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                  "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + Transporttdcacid + "' vc='0' progid='12' tranid='0'/>";
                        }
                    }
                    if (MEMO_ADVANCE > 0) {
                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + freightac + "' " +
                              "UNIT_code='0' NARRATION='" + TransportNarration + "' AMOUNT='" + MEMO_ADVANCE + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + freightac + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + freightac_id + "' vc='0' progid='12' tranid='0'/>";

                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + TRANSPORT_CODE + "' " +
                              "UNIT_code='0' NARRATION='" + TransportNarration + "' AMOUNT='" + MEMO_ADVANCE + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + tc + "' vc='0' progid='12' tranid='0'/>";

                        if (memocgstamt > 0) {
                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["SaleCGSTAc"] %> + "' " +
                              "UNIT_code='0' NARRATION='Memo Advance Sale Ac' AMOUNT='" + memocgstamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + <%=Session["SaleCGSTid"] %> + "' vc='0' progid='12' tranid='0'/>";

                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["CGST_RCM_Ac"] %> + "' " +
                              "UNIT_code='0' NARRATION='Memo Advance RCM' AMOUNT='" + memocgstamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + <%=Session["CGST_RCM_Acid"] %> + "' vc='0' progid='12' tranid='0'/>";

                        }
                        if (memosgstamt > 0) {
                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["SaleSGSTAc"] %> + "' " +
                              "UNIT_code='0' NARRATION='Memo Advance Sale Ac' AMOUNT='" + memosgstamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + <%=Session["SaleSGSTid"] %> + "' vc='0' progid='12' tranid='0'/>";

                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["SGST_RCM_Ac"] %> + "' " +
                              "UNIT_code='0' NARRATION='Memo Advance RCM' AMOUNT='" + memosgstamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + <%=Session["SGST_RCM_Acid"] %> + "' vc='0' progid='12' tranid='0'/>";

                        }
                        if (memoigstamt > 0) {
                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["SaleIGSTAc"] %> + "' " +
                              "UNIT_code='0' NARRATION='Memo Advance Sale Ac' AMOUNT='" + memoigstamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + <%=Session["SaleIGSTid"] %> + "' vc='0' progid='12' tranid='0'/>";

                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["IGST_RCM_Ac"] %> + "' " +
                              "UNIT_code='0' NARRATION='Memo Advance RCM' AMOUNT='" + memoigstamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + <%=Session["IGST_RCM_Acid"] %> + "' vc='0' progid='12' tranid='0'/>";

                        }




                    }
                    if (VASULI_AMOUNT != 0) {
                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='1' " +
                              "UNIT_code='0' NARRATION='" + TransportNarration + "' AMOUNT='" + VASULI_AMOUNT + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='1' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + CASHID + "' vc='0' progid='12' tranid='0'/>";

                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + TRANSPORT_CODE + "' " +
                              "UNIT_code='0' NARRATION='" + TransportNarration + "' AMOUNT='" + VASULI_AMOUNT + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + tc + "' vc='0' progid='12' tranid='0'/>";
                    }
                }
                else {
                    if (MEMO_ADVANCE > 0) {

                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + freightac + "' " +
                              "UNIT_code='0' NARRATION='" + TransportNarration + "' AMOUNT='" + MEMO_ADVANCE + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + freightac + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + freightac_id + "' vc='0' progid='12' tranid='0'/>";

                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + TRANSPORT_CODE + "' " +
                              "UNIT_code='0' NARRATION='" + TransportNarration + "' AMOUNT='" + MEMO_ADVANCE + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + tc + "' vc='0' progid='12' tranid='0'/>";


                        if (memocgstamt > 0) {
                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["SaleCGSTAc"] %> + "' " +
                              "UNIT_code='0' NARRATION='Memo Advance Sale Ac' AMOUNT='" + memocgstamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + <%=Session["SaleCGSTid"] %> + "' vc='0' progid='12' tranid='0'/>";

                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["CGST_RCM_Ac"] %> + "' " +
                              "UNIT_code='0' NARRATION='Memo Advance RCM' AMOUNT='" + memocgstamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + <%=Session["CGST_RCM_Acid"] %> + "' vc='0' progid='12' tranid='0'/>";

                        }
                        if (memosgstamt > 0) {
                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["SaleSGSTAc"] %> + "' " +
                              "UNIT_code='0' NARRATION='Memo Advance Sale Ac' AMOUNT='" + memosgstamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + <%=Session["SaleSGSTid"] %> + "' vc='0' progid='12' tranid='0'/>";

                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["SGST_RCM_Ac"] %> + "' " +
                              "UNIT_code='0' NARRATION='Memo Advance RCM' AMOUNT='" + memosgstamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + <%=Session["SGST_RCM_Acid"] %> + "' vc='0' progid='12' tranid='0'/>";

                    }
                    if (memoigstamt > 0) {
                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["SaleIGSTAc"] %> + "' " +
                              "UNIT_code='0' NARRATION='Memo Advance Sale Ac' AMOUNT='" + memoigstamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + <%=Session["SaleIGSTid"] %> + "' vc='0' progid='12' tranid='0'/>";

                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["IGST_RCM_Ac"] %> + "' " +
                              "UNIT_code='0' NARRATION='Memo Advance RCM' AMOUNT='" + memoigstamt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + <%=Session["IGST_RCM_Acid"] %> + "' vc='0' progid='12' tranid='0'/>";

                    }

                }
                if (TDSAc > 0) {
                    if (TDSAmt > 0) {
                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + TDSAc + "' " +
                               "UNIT_code='0' NARRATION='TDS:" + tdstransportnarration + "' AMOUNT='" + TDSAmt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                               "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='9999971' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                               "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + TDSID + "' vc='0' progid='12' tranid='0'/>";

                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + Transporttdcac + "' " +
                              "UNIT_code='0' NARRATION='TDS:" + tdstransportnarration + "' AMOUNT='" + TDSAmt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + Transporttdcac + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + Transporttdcacid + "' vc='0' progid='12' tranid='0'/>";
                    }
                }
                if (VASULI_AMOUNT != 0) {
                    Order_Code = Order_Code + 1;

                    XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='1' " +
                          "UNIT_code='0' NARRATION='" + TransportNarration + "' AMOUNT='" + VASULI_AMOUNT + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='1' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                          "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + CASHID + "' vc='0' progid='12' tranid='0'/>";
                    Order_Code = Order_Code + 1;

                    XML = XML + "<Ledger TRAN_TYPE='DO' CASHCREDIT='' DOC_NO='" + doDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + TRANSPORT_CODE + "' " +
                          "UNIT_code='0' NARRATION='" + TransportNarration + "' AMOUNT='" + VASULI_AMOUNT + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + TRANSPORT_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                          "SORT_TYPE='DO' SORT_NO='" + doDoc_No + "' ac='" + tc + "' vc='0' progid='12' tranid='0'/>";
                }
            }
            if (RecordStatus == "Update") {
                if (voucherlbl != "0") {
                    autopurchasebill = "Y";
                }
            }
            if (AUTO_VOUCHER == "YES") {
                if (DESP_TYPE == "DI" && (txtGETPASS_CODE == SELFAC || PDS == "P")) {
                    if (autopurchasebill == "Y") {
                        voucher_type = "PS";
                        voucherNo = purcDoc_No;
                        var PS_amount = 0;
                        PS_amount = Math.round(parseFloat(Purchase_Rate * txtquantal + 0));
                        var TOTALPurchase_Amount = "";
                        if (PaymentGst == "" || PaymentGst == "0") {
                            PaymentGst = txtMillGstStateCode;
                        }
                        if (CompanyStateCode == PaymentGst) {

                            PS_CGSTAmount = Math.round(parseFloat(PS_amount * cgstrate / 100));
                            PS_SGSTAmount = Math.round(parseFloat(PS_amount * sgstrate / 100));
                            PS_IGSTAmount = 0;
                            igstrate = 0;
                        }
                        else {
                            PS_CGSTAmount = 0;
                            cgstrate = 0;
                            PS_SGSTAmount = 0;
                            sgstrate = 0;
                            PS_IGSTAmount = Math.round(parseFloat(PS_amount * igstrate / 100));
                        }
                        TOTALPurchase_Amount = Math.round(parseFloat(PS_amount + PS_CGSTAmount + PS_SGSTAmount + PS_IGSTAmount));

                        var TCS_Amt = Math.round((parseFloat(TOTALPurchase_Amount) * TCS_Rate / 100));
                        var TDS_Amt = Math.round((parseFloat(PS_amount) * PurchaseTDSrate / 100));
                        var NetPayble = (parseFloat(TOTALPurchase_Amount) + parseFloat(TCS_Amt)) - parseFloat(TDS_Amt);

                        //var NetPayble = parseFloat(TOTALPurchase_Amount) + parseFloat(TCS_Amt);
                        if (txtGETPASS_CODE == SELFAC && txtSaleBillTo == SELFAC) {
                            PS_SelfBal = "Y";
                        }
                        else {
                            PS_SelfBal = "N";
                        }

                        PS_ac = PaymentId;

                        if (voucherlbl == "" || voucherlbl == "0") {
                            XML = XML + "<PurchaseHead doc_no='" + purcDoc_No + "' Tran_Type='PS' PURCNO='" + doDoc_No + "' doc_date='" + PUR_DATE + "' " +
                                                    "Ac_Code ='" + PaymentTo + "' Unit_Code='0' mill_code='" + txtMILL_CODE + "' FROM_STATION='' TO_STATION='' LORRYNO='" + TRUCK_NO + "' " +
                                                    "BROKER='" + BROKER_CODE + "' wearhouse='' subTotal='" + PS_amount + "' LESS_FRT_RATE='0' freight='0' cash_advance='0' " +
                                                    "bank_commission='0' OTHER_AMT='0' Bill_Amount='" + TOTALPurchase_Amount + "' Due_Days='1' " +
                                                    "NETQNTL='" + txtquantal + "' CGSTRate='" + cgstrate + "' CGSTAmount='" + PS_CGSTAmount + "' SGSTRate='" + sgstrate + "' " +
                                                    "SGSTAmount='" + PS_SGSTAmount + "' IGSTRate='" + igstrate + "' IGSTAmount='" + PS_IGSTAmount + "' " +
                                                    "GstRateCode='" + txtGstRate + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' " +
                                                    "Modified_By='" + USER + "' Bill_No='" + MillInvoiceno + "' EWay_Bill_No='" + MillEwayBill + "' purchaseid='" + purcid + "' ac='" + PaymentId + "' uc='" + PS_uc + "' " +
                                                    "mc='" + mc + "' bk='" + bk + "' mill_inv_date='" + MillInv_Date + "' grade='" + txtGRADE + "' " +
                                                   "Purcid='" + doid + "' SelfBal='" + PS_SelfBal + "' TCS_Rate='" + TCS_Rate +
                                                   "' TCS_Amt='" + TCS_Amt + "' TCS_Net_Payable='" + NetPayble + "' TDS_Rate='" + PurchaseTDSrate + "' TDS_Amt='" + TDS_Amt + "'>" +
                                                   "<PurchaseDetail doc_no='" + purcDoc_No + "' detail_id='1' Tran_Type='PS' item_code='" + txtitem_Code + "' narration='' Quantal='" + txtquantal + "' packing='" + PACKING + "' bags='" + BAGS + "' " +
                                                    "rate='" + Purchase_Rate + "' item_Amount='" + PS_amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' " +
                                                    "Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' Modified_By='" + USER +
                                                    "' purchasedetailid='" + purcdetailid + "' purchaseid='" + purcid + "' ic='" + ic + "' Brand_Code='" + brandcode + "'/></PurchaseHead>";
                        }
                        else {
                            //Purchase Update
                            XML = XML + "<PurchaseHead doc_no='" + purcDoc_No + "' Tran_Type='PS' PURCNO='" + doDoc_No + "' doc_date='" + PUR_DATE + "' " +
                             "Ac_Code ='" + PaymentTo + "' Unit_Code='0' mill_code='" + txtMILL_CODE + "' FROM_STATION='' TO_STATION='' LORRYNO='" + TRUCK_NO + "' " +
                             "BROKER='" + BROKER_CODE + "' wearhouse='' subTotal='" + PS_amount + "' LESS_FRT_RATE='0' freight='0' cash_advance='0' " +
                             "bank_commission='0' OTHER_AMT='0' Bill_Amount='" + TOTALPurchase_Amount + "' Due_Days='1' " +
                             "NETQNTL='" + txtquantal + "' CGSTRate='" + cgstrate + "' CGSTAmount='" + PS_CGSTAmount + "' SGSTRate='" + sgstrate + "' " +
                             "SGSTAmount='" + PS_SGSTAmount + "' IGSTRate='" + igstrate + "' IGSTAmount='" + PS_IGSTAmount + "' " +
                             "GstRateCode='" + txtGstRate + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' " +
                             "Modified_By='" + USER + "' Bill_No='" + MillInvoiceno + "' EWay_Bill_No='" + MillEwayBill + "' purchaseid='" + purcid + "' ac='" + PaymentId + "' uc='" + PS_uc + "' " +
                             "mc='" + mc + "' bk='" + bk + "' mill_inv_date='" + MillInv_Date + "' grade='" + txtGRADE + "' " +
                            "Purcid='" + doid + "' SelfBal='" + PS_SelfBal + "' TCS_Rate='" + TCS_Rate + "' TCS_Amt='" + TCS_Amt + "' TCS_Net_Payable='" + NetPayble + "' TDS_Rate='" + PurchaseTDSrate + "' TDS_Amt='" + TDS_Amt + "'>" +
                            "<PurchaseDetail doc_no='" + purcDoc_No + "' detail_id='1' Tran_Type='PS' item_code='" + txtitem_Code + "' narration='' Quantal='" + txtquantal + "' packing='" + PACKING + "' bags='" + BAGS + "' " +
                             "rate='" + Purchase_Rate + "' item_Amount='" + PS_amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' " +
                             "Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' purchasedetailid='"
                             + purcdetailid + "' purchaseid='" + purcid + "' ic='" + ic + "' Brand_Code='" + brandcode + "'/></PurchaseHead>";

                        }

                        // Purchase Gledger


                        //var CreditNarration = document.getElementById("<%= hdnfbrokershortName.ClientID %>").value + " " + txtquantal + "L:" + TRUCK_NO + "R:" + Purchase_Rate;
                        //var DebitNarration = document.getElementById("<%= hdnfmillshortname.ClientID %>").value + " " + txtquantal + "L:" + TRUCK_NO + "R:" + Purchase_Rate;
                        var CreditNarration = document.getElementById("<%= hdnfmillshortname.ClientID %>").value + " L:" + TRUCK_NO + " G:" + txtGRADE + " " + txtquantal + " R:" + Purchase_Rate + " Bill No:" + MillInvoiceno;
                        var DebitNarration = document.getElementById("<%= hdnfmillshortname.ClientID %>").value + " " + PaymentShort + " L:" + TRUCK_NO + " G:" + txtGRADE + " " + txtquantal + " R:" + Purchase_Rate + " Bill No:" + MillInvoiceno;
                        var TCSNarration = 'TCS' + " " + PaymentShort + " " + purcDoc_No + " Bill No:" + MillInvoiceno;
                        var TDSNarration = 'TDS' + " " + PaymentShort + " " + purcDoc_No + " Bill No:" + MillInvoiceno;

                        Order_Code = 1;
                        // XML = XML + "<PurchaseGledger>";
                        if (TOTALPurchase_Amount > 0) {

                            XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + purcDoc_No + "' DOC_DATE='" + PUR_DATE + "' AC_CODE='" + PaymentTo + "' " +
                              "UNIT_code='0' NARRATION='" + CreditNarration + "' AMOUNT='" + TOTALPurchase_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='PS' SORT_NO='" + purcDoc_No + "' ac='" + PaymentId + "' vc='0' progid='5' tranid='0'/>";

                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + purcDoc_No + "' DOC_DATE='" + PUR_DATE + "' AC_CODE='" + PurchAc + "' " +
                              "UNIT_code='0' NARRATION='" + DebitNarration + "' AMOUNT='" + PS_amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='PS' SORT_NO='" + purcDoc_No + "' ac='" + PurcAcid + "' vc='0' progid='5' tranid='0'/>";
                        }
                        // CGST
                        if (PS_CGSTAmount > 0) {
                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + purcDoc_No + "' DOC_DATE='" + PUR_DATE + "' AC_CODE='" + <%=Session["PurchaseCGSTAc"] %> + "' " +
                                  "UNIT_code='0' NARRATION='" + GSTNarrationPS + "' AMOUNT='" + PS_CGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                  "SORT_TYPE='PS' SORT_NO='" + purcDoc_No + "' ac='" + <%=Session["PurchaseCGSTid"] %> + "' vc='0' progid='5' tranid='0'/>";
                        }

                        //SGSTAcc Effect
                        if (PS_SGSTAmount > 0) {
                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + purcDoc_No + "' DOC_DATE='" + PUR_DATE + "' AC_CODE='" + <%=Session["PurchaseSGSTAc"] %> + "' " +
                              "UNIT_code='0' NARRATION='" + GSTNarrationPS + "' AMOUNT='" + PS_SGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='PS' SORT_NO='" + purcDoc_No + "' ac='" + <%=Session["PurchaseSGSTid"] %> + "' vc='0' progid='5' tranid='0'/>";
                        }

                        //IGSTAcc Effect
                        if (PS_IGSTAmount > 0) {
                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + purcDoc_No + "' DOC_DATE='" + PUR_DATE + "' AC_CODE='" + <%=Session["PurchaseIGSTAc"] %> + "' " +
                              "UNIT_code='0' NARRATION='" + GSTNarrationPS + "' AMOUNT='" + PS_IGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='PS' SORT_NO='" + purcDoc_No + "' ac='" + <%=Session["PurchaseIGSTid"] %> + "' vc='0' progid='5' tranid='0'/>";
                        }

                        //TDS Effect
                        if (TDS_Amt > 0) {
                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + purcDoc_No + "' DOC_DATE='" + PUR_DATE + "' AC_CODE='" + <%=Session["PurchaseTDSAc"] %> + "' " +
                              "UNIT_code='0' NARRATION='" + 'TDS:' + TDSNarration + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='PS' SORT_NO='" + purcDoc_No + "' ac='" + <%=Session["PurchaseTDSacid"] %> + "' vc='0' progid='5' tranid='0'/>";

                            Order_Code = Order_Code + 1;
                            XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + purcDoc_No + "' DOC_DATE='" + PUR_DATE + "' AC_CODE='" + PaymentTo + "' " +
                             "UNIT_code='0' NARRATION='" + 'TDS:' + TDSNarration + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                             "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                             "SORT_TYPE='PS' SORT_NO='" + purcDoc_No + "' ac='" + PaymentId + "' vc='0' progid='5' tranid='0'/>";
                        }


                        // end Purchase Gledger

                        if (TCS_Amt > 0) {

                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + purcDoc_No + "' DOC_DATE='" + PUR_DATE + "' AC_CODE='" + PaymentTo + "' " +
                              "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='PS' SORT_NO='" + purcDoc_No + "' ac='" + PaymentId + "' vc='0' progid='5' tranid='0'/>";

                            Order_Code = Order_Code + 1;

                            XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + purcDoc_No + "' DOC_DATE='" + PUR_DATE + "' AC_CODE='" + <%=Session["PurchaseTCSAc"] %> + "' " +
                              "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='PS' SORT_NO='" + purcDoc_No + "' ac='" + <%=Session["PurchaseTCSAcid"] %> + "' vc='0' progid='5' tranid='0'/>";
                        }
                        // XML = XML + "</PurchaseGledger>";

                        // end Purchase
                    }
                }
                else {
                    // Lv Calculation
                    //else  start
                    GSTRate = '<%= Session["GSTRate"] %>';
                    igstrate = '<%= Session["igstrate"] %>';
                    sgstrate = '<%= Session["sgstrate"] %>';
                    cgstrate = '<%= Session["cgstrate"] %>';
                    var LV_CGSTAmount = 0, LV_SGSTAmount = 0, LV_IGSTAmount = 0, LV_TotalAmount = 0;
                    var lvcgstrate = 0, lvsgstrate = 0, lvigstrate = 0;
                    var LV_TCSRate = 0, LV_TCSAmt = 0, LV_NETPayble = 0;
                    var voucherShortName = $("#<%=lblvoucherbyname.ClientID %>").text();
                    var taxableamount = DIFF_AMOUNT + MEMO_ADVANCE;
                    var BrokerShort = "";
                    if (BROKER_CODE != 2) {
                        BrokerShort = $("#<%=LBLBROKER_NAME.ClientID %>").text();
                    }
                    var LV_Narration1, LV_Narration2, LV_Narration4 = "";
                    LV_Narration1 = "V.No" + vouchnarration + " " + voucherShortName + " " + BrokerShort;
                    LV_Narration2 = myNarration2 + " Lorry No:" + TRUCK_NO;
                    LV_Narration4 = myNarration4 + " " + TRUCK_NO;
                    var vtype = "";
                    voucher_type = $("#<%=lblVoucherType.ClientID %>").text();
                    // voucher_type = "LV";
                    var DiffMemo = DIFF_AMOUNT + MEMO_ADVANCE;
                    if (DiffMemo != 0) {
                        // voucher_type = "LV";
                        voucherNo = commiDoc_No;
                        if (txtVoucherbyGstStateCode == CompanyStateCode) {
                            LV_CGSTAmount = Math.round(parseFloat(((DIFF_AMOUNT + MEMO_ADVANCE) * cgstrate) / 100));
                            LV_SGSTAmount = Math.round(parseFloat(((DIFF_AMOUNT + MEMO_ADVANCE) * sgstrate) / 100));
                            lvcgstrate = cgstrate;
                            lvsgstrate = sgstrate;
                            lvigstrate = 0.00;
                            LV_IGSTAmount = 0;
                        }
                        else {
                            LV_IGSTAmount = Math.round(parseFloat(((DIFF_AMOUNT + MEMO_ADVANCE) * igstrate) / 100));
                            lvcgstrate = 0;
                            lvsgstrate = 0;
                            lvigstrate = igstrate;
                            LV_SGSTAmount = 0.00;
                            LV_CGSTAmount = 0.00;
                        }
                    }
                    debugger;
                    var IsTDS = document.getElementById("<%= hdnfistds.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfistds.ClientID %>").value;

                    LV_TotalAmount = Math.round(parseFloat((DIFF_AMOUNT + MEMO_ADVANCE) + LV_CGSTAmount + LV_SGSTAmount + LV_IGSTAmount));
                    LV_TCSRate = parseFloat($("#<%=txtTCSRate_Sale.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSRate_Sale.ClientID %>").val());
                    // LV_TCSAmt = Math.Round(((Bill_Amt * TCS_Rate) / 100), 2);
                    LV_TCSAmt = Math.round(parseFloat((LV_TotalAmount * LV_TCSRate) / 100));
                    LV_NETPayble = Math.round(parseFloat((LV_TotalAmount + LV_TCSAmt)));
                    var LV_TDSRate = parseFloat($("#<%=txtSaleTDS.ClientID %>").val() == "" ? 0 : $("#<%=txtSaleTDS.ClientID %>").val());
                    var LV_TDSAmt = parseFloat((LV_TotalAmount * LV_TDSRate) / 100);
                    LV_NETPayble = LV_NETPayble;
                    if (voucher_type == "  ") {
                        if (LV_NETPayble != 0) {
                            if (LV_NETPayble > 0) {
                                voucher_type = "LV";
                            }
                            else {
                                voucher_type = "CV";
                            }
                        }
                    }
                    debugger;
                    if (LV_NETPayble >= 0) {

                        if (RecordStatus == "Update") {


                            if (voucher_type == "LV") {
                                if (LV_NETPayble >= 0) {

                                }
                                else {
                                    alert('please check amount LV is already generated!!!')
                                    $("#loader").hide();
                                    return false;

                                }

                            }
                            else {
                                if (LV_NETPayble < 0) {

                                }
                                else {
                                    alert('please check amount CV is already generated!!!')
                                    $("#loader").hide();
                                    return false;
                                }
                            }
                        }
                        else {

                            if (LV_NETPayble > 0) {
                                voucher_type = "LV";
                            }
                            else {
                                voucher_type = "CV";
                            }
                        }
                        var CommTDSAc_Code = 0;
                        var CommTDSAc_ID = 0;
                        if (voucher_type == "LV") {
                            var CommTDSAc_Code = '<%= Session["SaleTDSAc"] %>';;
                            var CommTDSAc_ID = '<%= Session["SaleTDSacid"] %>';;
                        }
                        else {
                            var CommTDSAc_Code = '<%= Session["PurchaseTDSAc"] %>';;
                            var CommTDSAc_ID = '<%= Session["PurchaseTDSacid"] %>';;
                        }
                        debugger;
                        if (voucherlbl == "" || voucherlbl == "0") {
                            if (RecordStatus == "Update") {
                                XML = XML + "<CommissionHead doc_date='" + DOC_DATE + "' link_no='" + DOC_NO + "' link_type='' link_id='0' ac_code='" + SaleBillTo + "' unit_code='" + txtGETPASS_CODE + "' " +
                                    "broker_code='" + BROKER_CODE + "' qntl='" + txtquantal + "' packing='" + PACKING + "' bags='" + BAGS + "' grade='" + txtGRADE + "' " +
                                    "transport_code='" + TRANSPORT_CODE + "' mill_rate='" + mill_rate + "' sale_rate='" + SALE_RATE + "' purc_rate='0' " +
            "commission_amount='" + parseFloat(DIFF_RATE - Tender_Commission) + "' resale_rate='" + Tender_Commission + "' resale_commission='" + Tender_Commission_Amount + "' misc_amount='0.00' " +
            "texable_amount='" + taxableamount + "' gst_code='" + txtGstRate + "' cgst_rate='" + lvcgstrate + "' cgst_amount='" + LV_CGSTAmount + "' " +
             "sgst_rate='" + lvsgstrate + "' sgst_amount='" + LV_SGSTAmount + "' igst_rate='" + lvigstrate + "' igst_amount='" + LV_IGSTAmount + "' bill_amount='" + LV_TotalAmount + "' " +
             "Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' " +
             "commissionid='" + commisionid + "' ac='" + sb + "' uc='" + st + "' bc='" + bk + "' tc='" + tc + "' mill_code='" + txtMILL_CODE + "' mc='" + mc + "' " +
             "narration1='" + LV_Narration1 + "' narration2='" + LV_Narration2 + "' narration3='' narration4='" + LV_Narration4 +
             "' TCS_Rate='" + LV_TCSRate + "' TCS_Net_Payable='" + LV_NETPayble + "' TCS_Amt='" + LV_TCSAmt + "' Tran_Type='" + voucher_type + "' item_code='"
             + txtitem_Code + "' ic='" + ic + "' Frieght_Rate='" + MM_Rate + "' Frieght_amt='" + MEMO_ADVANCE + "' subtotal='" + DIFF_AMOUNT +
             "' IsTDS='" + IsTDS + "' TDS_Ac='" + CommTDSAc_Code + "' TDS_Per='" + LV_TDSRate +
    "'  TDSAmount='" + LV_TDSAmt + "'  TDS='" + LV_TDSRate + "' ta='" + CommTDSAc_ID + "' HSN='" + hsnnumber + "'/>";
                            }

                            else {
                                XML = XML + "<CommissionHead  doc_date='" + DOC_DATE + "' link_no='0' link_type='' link_id='0' ac_code='" + SaleBillTo + "' unit_code='" + txtGETPASS_CODE + "' " +
                                   "broker_code='" + BROKER_CODE + "' qntl='" + txtquantal + "' packing='" + PACKING + "' bags='" + BAGS + "' grade='" + txtGRADE + "' " +
                                   "transport_code='" + TRANSPORT_CODE + "' mill_rate='" + mill_rate + "' sale_rate='" + SALE_RATE + "' purc_rate='0' " +
           "commission_amount='" + parseFloat(DIFF_RATE - Tender_Commission) + "' resale_rate='" + Tender_Commission + "' resale_commission='" + Tender_Commission_Amount + "' misc_amount='0.00' " +
           "texable_amount='" + taxableamount + "' gst_code='" + txtGstRate + "' cgst_rate='" + lvcgstrate + "' cgst_amount='" + LV_CGSTAmount + "' " +
            "sgst_rate='" + lvsgstrate + "' sgst_amount='" + LV_SGSTAmount + "' igst_rate='" + lvigstrate + "' igst_amount='" + LV_IGSTAmount + "' bill_amount='" + LV_TotalAmount + "' " +
            "Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' " +
            "commissionid='" + commisionid + "' ac='" + sb + "' uc='" + st + "' bc='" + bk + "' tc='" + tc + "' mill_code='" + txtMILL_CODE + "' mc='" + mc + "' " +
            "narration1='" + LV_Narration1 + "' narration2='" + LV_Narration2 + "' narration3='' narration4='" + LV_Narration4 +
            "' TCS_Rate='" + LV_TCSRate + "' TCS_Net_Payable='" + LV_NETPayble + "' TCS_Amt='" + LV_TCSAmt +
            "' Tran_Type='" + voucher_type + "' item_code='" + txtitem_Code + "' ic='" + ic + "' Frieght_Rate='" + MM_Rate +
            "' Frieght_amt='" + MEMO_ADVANCE + "' subtotal='" + DIFF_AMOUNT +
            "' IsTDS='" + IsTDS + "' TDS_Ac='" + CommTDSAc_Code + "' TDS_Per='" + LV_TDSRate +
    "'  TDSAmount='" + LV_TDSAmt + "'  TDS='" + LV_TDSRate + "' ta='" + CommTDSAc_ID + "' HSN='" + hsnnumber + "'/>";

                            }
                        }
                        else {
                            // LV Update
                            XML = XML + "<CommissionHead doc_no='" + commiDoc_No + "' doc_date='" + DOC_DATE + "' link_no='" + DOC_NO + "' link_type='' link_id='0' ac_code='" + SaleBillTo + "' unit_code='" + txtGETPASS_CODE + "' " +
                                "broker_code='" + BROKER_CODE + "' qntl='" + txtquantal + "' packing='" + PACKING + "' bags='" + BAGS + "' grade='" + txtGRADE + "' " +
                                "transport_code='" + TRANSPORT_CODE + "' mill_rate='" + mill_rate + "' sale_rate='" + SALE_RATE + "' purc_rate='0' " +
        "commission_amount='" + parseFloat(DIFF_RATE - Tender_Commission) + "' resale_rate='" + Tender_Commission + "' resale_commission='" + Tender_Commission_Amount + "' misc_amount='0.00' " +
        "texable_amount='" + taxableamount + "' gst_code='" + txtGstRate + "' cgst_rate='" + lvcgstrate + "' cgst_amount='" + LV_CGSTAmount + "' " +
         "sgst_rate='" + lvsgstrate + "' sgst_amount='" + LV_SGSTAmount + "' igst_rate='" + lvigstrate + "' igst_amount='" + LV_IGSTAmount + "' bill_amount='" + LV_TotalAmount + "' " +
         "Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' " +
         "commissionid='" + commisionid + "' ac='" + sb + "' uc='" + st + "' bc='" + bk + "' tc='" + tc + "' mill_code='" + txtMILL_CODE + "' mc='" + mc + "' " +
         "narration1='" + LV_Narration1 + "' narration2='" + LV_Narration2 + "' narration3='' narration4='" + LV_Narration4 +
         "' TCS_Rate='" + LV_TCSRate + "' TCS_Net_Payable='" + LV_NETPayble + "' TCS_Amt='" + LV_TCSAmt +
         "' Tran_Type='" + voucher_type + "' item_code='" + txtitem_Code + "' ic='" + ic + "' Frieght_Rate='" + MM_Rate + "' Frieght_amt='" + MEMO_ADVANCE +
         "' subtotal='" + DIFF_AMOUNT + "' IsTDS='" + IsTDS + "' TDS_Ac='" + CommTDSAc_Code + "' TDS_Per='" + LV_TDSRate +
    "'  TDSAmount='" + LV_TDSAmt + "'  TDS='" + LV_TDSRate + "' ta='" + CommTDSAc_ID + "' HSN='" + hsnnumber + "'/>";
                        }
                        // LV GLedger

                        var drcr = "";
                        Order_Code = 1;
                        if (DiffMemo > 0) {
                            drcr = "D";
                        }
                        else {
                            drcr = "C";
                        }

                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + SaleBillTo + "' " +
                              "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(LV_TotalAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr + "' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + sb + "' vc='0' progid='11' tranid='0'/>";


                        if (LV_TCSAmt != 0) {
                            if (voucher_type == "LV") {
                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT=''  DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + SaleBillTo + "' " +
                                      "UNIT_code='0' NARRATION='TCS' AMOUNT='" + Math.abs(LV_TCSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                      "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + sb + "' vc='0' progid='11' tranid='0' saleid='0'/>";
                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT=''  DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                  "UNIT_code='0' NARRATION='TCS' AMOUNT='" + Math.abs(LV_TCSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                  "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + '<%=Session["SaleTCSAcid"] %>' + "' vc='0' progid='11' tranid='0' saleid='0'/>";
                            }
                            else {
                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT=''  DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + PaymentTo + "' " +
                                      "UNIT_code='0' NARRATION='TCS' AMOUNT='" + Math.abs(LV_TCSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                      "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + PaymentId + "' vc='0' progid='11' tranid='0' saleid='0'/>";
                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT=''  DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                  "UNIT_code='0' NARRATION='TCS' AMOUNT='" + Math.abs(LV_TCSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                  "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + '<%=Session["SaleTCSAcid"] %>' + "' vc='0' progid='11' tranid='0' saleid='0'/>";
                            }

                        }


                        if (LV_TDSAmt != 0) {
                            if (voucher_type == "LV") {
                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + SaleBillTo + "' " +
                                      "UNIT_code='0' NARRATION='TDS' AMOUNT='" + Math.abs(LV_TDSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                      "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + sb + "' vc='0' progid='11' tranid='0'/>";
                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleTDSAc"] %>' + "' " +
                                  "UNIT_code='0' NARRATION='TDS' AMOUNT='" + Math.abs(LV_TDSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                  "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + '<%=Session["SaleTDSacid"] %>' + "' vc='0' progid='11' tranid='0'/>";
                            }
                            else {
                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + SaleBillTo + "' " +
                                      "UNIT_code='0' NARRATION='TDS' AMOUNT='" + Math.abs(LV_TDSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                      "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + sb + "' vc='0' progid='11' tranid='0'/>";
                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleTDSAc"] %>' + "' " +
                                  "UNIT_code='0' NARRATION='TDS' AMOUNT='" + Math.abs(LV_TDSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                  "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + '<%=Session["SaleTDSacid"] %>' + "' vc='0' progid='11' tranid='0'/>";
                            }
                        }


                        // Quality_Diff_Ac Effect Credit
                        if (DiffMemo > 0) {
                            Order_Code = parseInt(Order_Code) + 1;
                            var amt = Math.round(parseFloat(DiffMemo) - parseFloat(Tender_Commission_Amount));

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["RateDiffAc"] %> + "' " +
                                   "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(amt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                   "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                   "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["RateDiffAcid"] %> + "' vc='0' progid='11' tranid='0'/>";

                            if (LV_CGSTAmount != 0) {
                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["SaleCGSTAc"] %> + "' " +
                              "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(LV_CGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["SaleCGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";
                            }
                            // SGST Acv

                            if (LV_SGSTAmount != 0) {
                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["SaleSGSTAc"] %> + "' " +
                             "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(LV_SGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                             "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                             "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["SaleSGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";
                            }
                            //IGST Acc
                            if (LV_IGSTAmount != 0) {
                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["SaleIGSTAc"] %> + "' " +
                             "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(LV_IGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                             "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                             "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["SaleIGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";
                            }

                        }
                        else {
                            if (DiffMemo != 0) {

                                Order_Code = parseInt(Order_Code) + 1;
                                var amt = Math.round(parseFloat(DiffMemo) - parseFloat(Tender_Commission_Amount));

                                XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["RateDiffAc"] %> + "' " +
                                  "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(amt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                  "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["RateDiffAcid"] %> + "' vc='0' progid='11' tranid='0'/>";

                                if (LV_CGSTAmount != 0) {
                                    Order_Code = parseInt(Order_Code) + 1;

                                    XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["PurchaseCGSTAc"] %> + "' " +
                                  "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(LV_CGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                  "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["PurchaseCGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";

                                }
                                // SGST Acv

                                if (LV_SGSTAmount != 0) {
                                    Order_Code = parseInt(Order_Code) + 1;

                                    XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["PurchaseSGSTAc"] %> + "' " +
                                 "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(LV_SGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                 "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                 "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["PurchaseSGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";

                                }
                                //IGST Acc
                                if (LV_IGSTAmount != 0) {
                                    Order_Code = parseInt(Order_Code) + 1;

                                    XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["PurchaseIGSTAc"] %> + "' " +
                                 "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(LV_IGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                 "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                 "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["PurchaseIGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";

                                }
                            }
                        }
                        // Resale Commis
                        if (Tender_Commission_Amount > 0) {
                            if (Tender_Commission_Amount > 0) {
                                drcr = "C";
                            } else {
                                drcr = "D";
                            }
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + commiDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["COMMISSION_AC"] %>' + "' " +
                                 "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(Tender_Commission_Amount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                 "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr + "' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                 "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["commissionid"] %> + "' vc='0' progid='11' tranid='0'/>";
                        }
                    }
                }

                ////end else
            }


                // SaleBill Calculation
                debugger;
            GSTRate = '<%= Session["GSTRate"] %>';
                igstrate = '<%= Session["igstrate"] %>';
                sgstrate = '<%= Session["sgstrate"] %>';
                cgstrate = '<%= Session["cgstrate"] %>';
                var SbNo = $("#<%=lblSB_No.ClientID %>").text();
                var RATES = SALE_RATE + Tender_Commission + Insurance + 0;
                var lessfrtwithgst = (SALE_RATE - FRIEGHT_RATE) + Tender_Commission + Insurance -VASULI_RATE_1+ 0;
              //  var SaleForNaka = (RATES - FRIEGHT_RATE) + MM_Rate-VASULI_RATE_1;
                var SaleForNaka = (RATES - FRIEGHT_RATE) + MM_Rate;

                var SB_SaleRate, TaxableAmountForSB, SB_FROM_STATION, SB_TO_STATION, SB_CGSTAmount, SB_SGSTAmount, SB_IGSTAmount, Sb_CheckState = "";
                var SB_Ac_Code, SB_Unit_Code, SB_SubTotal, SB_Less_Frt_Rate, SB_freight, SB_ac, SB_uc, item_Amount, SaleDetail_Rate = "";
                var SaleBillInsert, SaleBillUpdate, SaleBillDetailInsert = "";
                var expbamt = 0.00;
                var BillRoundOff = 0.00;
                if (Delivery_Type == "C") {
                    TaxableAmountForSB = Math.round(parseFloat((RATES * txtquantal) + MEMO_ADVANCE + VASULI_AMOUNT_1));

                }
                else {

                    if (Carporate_Sale_No == "0" || Carporate_Sale_No == "") {
                        if (Delivery_Type == "N") {
                            //  SB_SaleRate = Math.round(parseFloat((((SaleForNaka / (SaleForNaka + (SaleForNaka * GSTRate / 100))) * SaleForNaka))));
                            SB_SaleRate = parseFloat((((SaleForNaka / (SaleForNaka + (SaleForNaka * GSTRate / 100))) * SaleForNaka)));
                            SB_SaleRate = Math.round((SB_SaleRate + Number.EPSILON) * 100) / 100;
                            expbamt = parseFloat(SaleForNaka * txtquantal);

                        }
                        else if (Delivery_Type == "A") {
                            
                            SB_SaleRate = SaleForNaka;
                            var frieght = (VASULI_RATE_1*txtquantal);
                            TaxableAmountForSB = (SaleForNaka*txtquantal)+frieght;


                        }
                        else {
                            SB_SaleRate = lessfrtwithgst;

                        }

                        if (Delivery_Type == "N") {

                            TaxableAmountForSB = Math.round(parseFloat(((SB_SaleRate + VASULI_RATE_1) ) * txtquantal));
                        }
                        else if (Delivery_Type == "A") {

                        }
                        else {
                            TaxableAmountForSB = Math.round(parseFloat(SB_SaleRate * txtquantal));
                        }
                    }
                    else {
                        if (Delivery_Type == "N") {
                            //  SB_SaleRate = Math.round(parseFloat((((SaleForNaka / (SaleForNaka + (SaleForNaka * GSTRate / 100))) * SaleForNaka))));
                            SB_SaleRate = parseFloat((((SaleForNaka / (SaleForNaka + (SaleForNaka * GSTRate / 100))) * SaleForNaka)));
                            SB_SaleRate = Math.round((SB_SaleRate + Number.EPSILON) * 100) / 100;
                            expbamt = parseFloat(SaleForNaka * txtquantal);

                        }
                        else if (Delivery_Type == "A") {

                            SB_SaleRate = SaleForNaka;
                            SB_SaleRate = Math.round((SB_SaleRate + Number.EPSILON) * 100) / 100;
                            expbamt = parseFloat(SaleForNaka * txtquantal);

                        }
                        else {
                            SB_SaleRate = lessfrtwithgst;

                        }
                        //SB_SaleRate = SaleForNaka;
                        if (Delivery_Type == "A") {
                            TaxableAmountForSB = Math.round(parseFloat(((SB_SaleRate - (VASULI_RATE_1 + FRIEGHT_RATE)) + MM_Rate) * txtquantal));
                        }
                        else {
                            TaxableAmountForSB = Math.round(parseFloat(SB_SaleRate * txtquantal));
                        }
                        // TaxableAmountForSB = Math.round(parseFloat(SB_SaleRate * txtquantal));
                        //expbamt = parseFloat(SB_SaleRate * txtquantal);
                    }
                }

                // Check State Code
                if (pdsAcStateCode != "0" && pdsAcStateCode != "") {
                    Sb_CheckState = pdsAcStateCode;
                }
                else if (BilltoStateCode != "0" && BilltoStateCode != "") {
                    Sb_CheckState = BilltoStateCode;
                }
                else {
                    Sb_CheckState = txtSalebilltoGstStateCode;
                }
                // End
                if (CompanyStateCode == Sb_CheckState) {

                    SB_CGSTAmount = parseFloat(TaxableAmountForSB * cgstrate / 100);
                    SB_CGSTAmount = Math.round((SB_CGSTAmount + Number.EPSILON) * 100) / 100;

                    SB_SGSTAmount = parseFloat(TaxableAmountForSB * sgstrate / 100);
                    SB_SGSTAmount = Math.round((SB_SGSTAmount + Number.EPSILON) * 100) / 100;
                    SB_IGSTAmount = 0.00;
                    igstrate = 0;
                }
                else {
                    SB_CGSTAmount = 0.00;
                    cgstrate = 0;
                    SB_SGSTAmount = 0.00;
                    sgstrate = 0;
                    SB_IGSTAmount = parseFloat(TaxableAmountForSB * igstrate / 100);
                    SB_IGSTAmount = Math.round((SB_IGSTAmount + Number.EPSILON) * 100) / 100;
                }

                var TotalGstSaleBillAmount = 0;
                TotalGstSaleBillAmount = parseFloat(TaxableAmountForSB + SB_CGSTAmount + SB_SGSTAmount + SB_IGSTAmount + SB_Other_Amount);

                var Roundoff = 0;
                debugger;
                if (PDS == "P") {
                    SB_Ac_Code = pdsparty;
                    SB_Unit_Code = pdsunit;
                    SB_ac = pdsAcId;
                    SB_uc = pdsucId;
                    if (Delivery_Type == "C") {
                        Roundoff = Math.round(parseFloat(TotalGstSaleBillAmount - (TaxableAmountForSB + SB_CGSTAmount + SB_SGSTAmount + SB_IGSTAmount + SB_Other_Amount)));

                        SB_SubTotal = Math.round(parseFloat(txtquantal * RATES));
                    }
                    else {
                        Roundoff = Math.round(parseFloat(TotalGstSaleBillAmount - (TaxableAmountForSB + SB_CGSTAmount + SB_SGSTAmount + SB_IGSTAmount + SB_Other_Amount)));

                        SB_SubTotal = Math.round(parseFloat(txtquantal * SB_SaleRate));
                      
                    }
                }
                else {
                    SB_Ac_Code = txtSaleBillTo;
                    SB_Unit_Code = txtvoucher_by;
                    SB_ac = sb;
                    SB_uc = document.getElementById("<%= hdnfst.ClientID %>").value;
                    if (Delivery_Type == "C") {
                        Roundoff = Math.round(parseFloat(TotalGstSaleBillAmount - (TaxableAmountForSB + SB_CGSTAmount + SB_SGSTAmount + SB_IGSTAmount)));
                        SB_SubTotal = Math.round(parseFloat(txtquantal * RATES));
                    }
                    else {
                        Roundoff = Math.round(parseFloat(TotalGstSaleBillAmount - (TaxableAmountForSB + SB_CGSTAmount + SB_SGSTAmount + SB_IGSTAmount)));
                        SB_SubTotal = Math.round(parseFloat(txtquantal * SB_SaleRate) - (MEMO_ADVANCE+VASULI_AMOUNT_1));
                    }
                }

                if (Delivery_Type == "C") {
                    SB_Less_Frt_Rate = Math.round(parseFloat(MM_Rate + VASULI_RATE_1));
                    SB_freight = Math.round(parseFloat(MEMO_ADVANCE + VASULI_AMOUNT_1));

                    item_Amount = Math.round(parseFloat(RATES * txtquantal + 0));
                    SaleDetail_Rate = RATES;
                }
                else {
                    SB_Less_Frt_Rate = Math.round(parseFloat(MM_Rate + VASULI_RATE_1));
                    SB_freight = Math.round(parseFloat(MEMO_ADVANCE + VASULI_AMOUNT_1));

                    item_Amount = Math.round(parseFloat((SB_SaleRate * txtquantal) - MEMO_ADVANCE-VASULI_AMOUNT_1 + 0));
                    SB_SaleRate = SB_SubTotal / txtquantal;

                    SaleDetail_Rate = SB_SaleRate;
                }
                var TCSAmt = Math.round(parseFloat(TotalGstSaleBillAmount) * TCSRate_sale / 100);
                var TDSAmt = Math.round(parseFloat(TaxableAmountForSB) * SaleTDSrate / 100);

                var Net_Payble = Math.round(parseFloat(TotalGstSaleBillAmount) + TCSAmt);
                // Calculate roundoff amount;

                if (Delivery_Type == "N") {
                    //  BillRoundOff = expbamt - TotalGstSaleBillAmount;
                    BillRoundOff = Math.round(TotalGstSaleBillAmount) - TotalGstSaleBillAmount
                }
                else {
                    BillRoundOff = Math.round(TotalGstSaleBillAmount) - TotalGstSaleBillAmount;

                }
                Roundoff = BillRoundOff;
                TotalGstSaleBillAmount = TotalGstSaleBillAmount + Roundoff;
                //if (PDS == "P") { 
                var sbbillto = BillTo;
                var sbbt = bt
                if (Carporate_Sale_No == "" || Carporate_Sale_No == "0") {
                    sbbillto = SB_Ac_Code;
                    sbbt = SB_ac;
                } else {
                    sbbillto = BillTo;
                    sbbt = bt;
                }
                debugger;
                if (AUTO_VOUCHER == "YES") {
                    if (DESP_TYPE != "DO" && txtSaleBillTo != "0" && txtSaleBillTo != SELFAC && txtSaleBillTo != "2") {
                        if (SbNo == "" || SbNo == "0") {

                            XML = XML + "<SaleHead doc_no='" + saleDoc_No + "' Tran_Type='SB' PURCNO='" + purcDoc_No + "' Purcid='" + purcid + "' doc_date='" + DOC_DATE +
                                "' Ac_Code ='" + SB_Ac_Code + "' " +
                                "Unit_Code='" + SB_Unit_Code + "' mill_code='" + txtMILL_CODE + "' FROM_STATION='' TO_STATION='' LORRYNO='" + TRUCK_NO + "' " +
                                "BROKER='" + BROKER_CODE + "' wearhouse='' subTotal='" + SB_SubTotal + "' LESS_FRT_RATE='" + SB_Less_Frt_Rate + "' " +
                                "freight='" + SB_freight + "' cash_advance='0' bank_commission='0' OTHER_AMT='" + SB_Other_Amount + "' Bill_Amount='" + TotalGstSaleBillAmount + "' " +
                                "Due_Days='0' NETQNTL='" + txtquantal + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
                                "Created_By='" + USER + "' Modified_By='" + USER + "' DO_No='" + doDoc_No + "' Transport_Code='" + TRANSPORT_CODE + "' " +
                                "RateDiff='0' ASN_No='' GstRateCode='" + txtGstRate + "' CGSTRate='" + cgstrate + "' CGSTAmount='" + SB_CGSTAmount + "' SGSTRate='" + sgstrate + "' " +
                                "SGSTAmount='" + SB_SGSTAmount + "' IGSTRate='" + igstrate + "' IGSTAmount='" + SB_IGSTAmount + "' TaxableAmount='" + TaxableAmountForSB + "' " +
                                "EWay_Bill_No='" + EWayBill_No + "' EWayBill_Chk='" + EWay_BillChk + "' MillInvoiceNo='" + MillInvoiceno + "' RoundOff='" + Roundoff + "' " +
                                "saleid='" + saleid + "' ac='" + SB_ac + "' uc='" + SB_uc + "' mc='" + mc + "' bk='" + bk + "' " +
                                "tc='" + tc + "' DoNarrtion='" + NARRATION3 + "' TCS_Rate='" + TCSRate_sale + "' TCS_Amt='" + TCSAmt +
                                "' TCS_Net_Payable='" + Net_Payble + "' newsbno='" + newsbno + "' newsbdate='" + newsbdate + "' einvoiceno='" + einvoiceno +
                                "' ackno='" + ackno + "' Delivery_type='" + Delivery_Type + "' Bill_To='" + sbbillto + "' bt='" + sbbt +
                                "' EwayBillValidDate='" + DOC_DATE + "' IsDeleted='1' TDS_Rate='" + SaleTDSrate + "' TDS_Amt='" + TDSAmt +
                                "' SBNarration='" + SBNARRATION + "' Insured='" + Insured + "'>" +
                                "<SaleDetail doc_no='" + saleDoc_No + "' detail_id='1' Tran_Type='SB' item_code='" + txtitem_Code + "' narration='' Quantal='" + txtquantal + "' packing='" + PACKING + "'  " +
                                "bags='" + BAGS + "' rate='" + SaleDetail_Rate + "'  " +
                                "item_Amount='" + item_Amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + brandcode + "' Created_By='" + USER + "' Modified_By='" + USER + "' " +
                                "saledetailid='" + saledetailid + "' saleid='" + saleid + "' ic='" + ic + "' Brand_Code='" + brandcode + "'/></SaleHead>";

                        }
                        else {
                            XML = XML + "<SaleHead doc_no='" + saleDoc_No + "' Tran_Type='SB' PURCNO='" + purcDoc_No + "' Purcid='" + purcid + "' doc_date='" + DOC_DATE + "' Ac_Code ='" + SB_Ac_Code + "' " +
                                "Unit_Code='" + SB_Unit_Code + "' mill_code='" + txtMILL_CODE + "' FROM_STATION='' TO_STATION='' LORRYNO='" + TRUCK_NO + "' " +
                                "BROKER='" + BROKER_CODE + "' wearhouse='' subTotal='" + SB_SubTotal + "' LESS_FRT_RATE='" + SB_Less_Frt_Rate + "' " +
                                "freight='" + SB_freight + "' cash_advance='0' bank_commission='0' OTHER_AMT='" + SB_Other_Amount + "' Bill_Amount='" + TotalGstSaleBillAmount + "' " +
                                "Due_Days='0' NETQNTL='" + txtquantal + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
                                "Created_By='" + USER + "' Modified_By='" + USER + "' DO_No='" + doDoc_No + "' Transport_Code='" + TRANSPORT_CODE + "' " +
                                "RateDiff='0' ASN_No='' GstRateCode='" + txtGstRate + "' CGSTRate='" + cgstrate + "' CGSTAmount='" + SB_CGSTAmount + "' SGSTRate='" + sgstrate + "' " +
                                "SGSTAmount='" + SB_SGSTAmount + "' IGSTRate='" + igstrate + "' IGSTAmount='" + SB_IGSTAmount + "' TaxableAmount='" + TaxableAmountForSB + "' " +
                                "EWay_Bill_No='" + EWayBill_No + "' EWayBill_Chk='" + EWay_BillChk + "' MillInvoiceNo='" + MillInvoiceno + "' RoundOff='" + Roundoff + "' " +
                                "saleid='" + saleid + "' ac='" + SB_ac + "' uc='" + SB_uc + "' mc='" + mc + "' bk='" + bk + "' " +
                                "tc='" + tc + "' DoNarrtion='" + NARRATION3 + "' TCS_Rate='" + TCSRate_sale + "' TCS_Amt='" + TCSAmt +
                                "' TCS_Net_Payable='" + Net_Payble + "' newsbno='" + newsbno + "' newsbdate='" + newsbdate +
                                "' einvoiceno='" + einvoiceno + "' ackno='" + ackno + "' Delivery_type='" + Delivery_Type + "' Bill_To='" + sbbillto
                                + "' bt='" + sbbt + "' EwayBillValidDate='" + EwayBill_ValidDate + "' IsDeleted='1' TDS_Rate='" + SaleTDSrate +
                                "' TDS_Amt='" + TDSAmt + "' SBNarration='" + SBNARRATION + "' Insured='" + Insured + "'>" +
                                "<SaleDetail doc_no='" + saleDoc_No + "' detail_id='1' Tran_Type='SB' item_code='" + txtitem_Code + "' narration='' Quantal='" + txtquantal + "' packing='" + PACKING + "'  " +
                                "bags='" + BAGS + "' rate='" + SaleDetail_Rate + "'  " +
                                "item_Amount='" + item_Amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' " +
                                "saledetailid='" + saledetailid + "' saleid='" + saleid + "' ic='" + ic + "' Brand_Code='" + brandcode + "'/></SaleHead>";
                        }


                        var CreditNarration = ""; DebitNarration = "";
                        var finalsaleamonut = Math.round(parseFloat(((SaleDetail_Rate * 5) / 100) + SaleDetail_Rate));
                        if (SB_Ac_Code == SB_Unit_Code) {
                            CreditNarration = document.getElementById("<%= hdnfmillshortname.ClientID %>").value + " Qntl:" + txtquantal + " GRADE:" + txtGRADE + " SALE RATE:" + SALE_RATE + " Lorry No:" + TRUCK_NO;
                            DebitNarration = document.getElementById("<%= hdnfmillshortname.ClientID %>").value + " Qntl:" + txtquantal + " GRADE:" + txtGRADE + " SALE RATE:" + SALE_RATE + " Lorry No:" + TRUCK_NO;
                        }
                        else {
                            CreditNarration = document.getElementById("<%= hdnfmillshortname.ClientID %>").value + " Qntl:" + txtquantal + " GRADE:" + txtGRADE + " SALE RATE:" + SALE_RATE + " Lorry No:" + TRUCK_NO;
                            DebitNarration = document.getElementById("<%= hdnfmillshortname.ClientID %>").value + " Qntl:" + txtquantal + " GRADE:" + txtGRADE + " SALE RATE:" + SALE_RATE + " Lorry No:" + TRUCK_NO;
                        }
                        var saleaccountnarration = document.getElementById("<%= hdnfmillshortname.ClientID %>").value + ",SB:" + document.getElementById("<%= hdnfsalebilltoshortname.ClientID %>").value + ",QNT:" + txtquantal + "L:" + TRUCK_NO + "R:" + parseFloat(parseFloat(SaleDetail_Rate) + parseFloat(VASULI_AMOUNT)) + "MM:" + MM_Rate;
                        var TransportNarration = "" + txtquantal + "0.00" + document.getElementById("<%= hdnfmillshortname.ClientID %>").value + "  " + document.getElementById("<%= hdnftransportshortname.ClientID %>").value + "  Lorry:" + TRUCK_NO + "  Party:" + document.getElementById("<%= hdnfsalebilltoshortname.ClientID %>").value + "";
                        var TCS_Narration = 'TCS' + document.getElementById("<%= hdnfsalebilltoshortname.ClientID %>").value + " " + saleDoc_No;
                        var TDS_Narration = 'TDS' + document.getElementById("<%= hdnfsalebilltoshortname.ClientID %>").value + " " + saleDoc_No;

                        // Ac Code Effect
                        Order_Code = 1;
                        // XML = XML + "<SaleGledger>";
                        if (TotalGstSaleBillAmount > 0) {


                            XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + saleDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + SB_Ac_Code + "' " +
                                 "UNIT_code='0' NARRATION='" + DebitNarration + "' AMOUNT='" + TotalGstSaleBillAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                 "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                 "SORT_TYPE='SB' SORT_NO='" + saleDoc_No + "' ac='" + SB_ac + "' vc='0' progid='16' tranid='0' saleid='" + saleid + "'/>";

                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + saleDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + SaleAc + "' " +
                                "UNIT_code='0' NARRATION='" + saleaccountnarration + "' AMOUNT='" + TaxableAmountForSB + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                "SORT_TYPE='SB' SORT_NO='" + saleDoc_No + "' ac='" + SaleAccid + "' vc='0' progid='16' tranid='0' saleid='" + saleid + "'/>";
                        }
                        //end Ac Code
                        if (SB_Other_Amount != 0) {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + saleDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["OTHER_Ac"] %>' + "' " +
                             "UNIT_code='0' NARRATION='" + GSTNarrationSB + "' AMOUNT='" + SB_Other_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                             "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                             "SORT_TYPE='SB' SORT_NO='" + saleDoc_No + "' ac='" + '<%=Session["OTHER_Acid"] %>' + "' vc='0' progid='16' tranid='0' saleid='" + saleid + "'/>";

                        }
                        //CGSTAcc Effect
                        if (SB_CGSTAmount > 0) {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + saleDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleCGSTAc"] %>' + "' " +
                            "UNIT_code='0' NARRATION='" + GSTNarrationSB + "' AMOUNT='" + SB_CGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                            "SORT_TYPE='SB' SORT_NO='" + saleDoc_No + "' ac='" + '<%=Session["SaleCGSTid"] %>' + "' vc='0' progid='16' tranid='0' saleid='" + saleid + "'/>";

                        }
                        //end CGSTAcc Effect

                        //SGSTAcc Effect
                        if (SB_SGSTAmount > 0) {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + saleDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleSGSTAc"] %>' + "' " +
                                "UNIT_code='0' NARRATION='" + GSTNarrationSB + "' AMOUNT='" + SB_SGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                "SORT_TYPE='SB' SORT_NO='" + saleDoc_No + "' ac='" + '<%=Session["SaleSGSTid"] %>' + "' vc='0' progid='16' tranid='0' saleid='" + saleid + "'/>";

                        }
                        //end SGSTAcc Effect

                        //IGSTAcc Effect
                        if (SB_IGSTAmount > 0) {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + saleDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleIGSTAc"] %>' + "' " +
                                "UNIT_code='0' NARRATION='" + GSTNarrationSB + "' AMOUNT='" + SB_IGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                "SORT_TYPE='SB' SORT_NO='" + saleDoc_No + "' ac='" + '<%=Session["SaleIGSTid"] %>' + "' vc='0' progid='16' tranid='0' saleid='" + saleid + "'/>";

                        }
                        //end IGSTAcc Effect
                        if (Roundoff != 0) {
                            if (Roundoff < 0) {

                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + saleDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["RoundOff"] %>' + "' " +
                                                            "UNIT_code='0' NARRATION='" + DebitNarration + "' AMOUNT='" + Math.abs(Roundoff) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                            "SORT_TYPE='SB' SORT_NO='" + saleDoc_No + "' ac='" + '<%=Session["RoundOffid"] %>' + "' vc='0' progid='16' tranid='0' saleid='" + saleid + "'/>";

                            }
                            else {
                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + saleDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["RoundOff"] %>' + "' " +
                                                            "UNIT_code='0' NARRATION='" + CreditNarration + "' AMOUNT='" + Roundoff + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                            "SORT_TYPE='SB' SORT_NO='" + saleDoc_No + "' ac='" + '<%=Session["RoundOffid"] %>' + "' vc='0' progid='16' tranid='0' saleid='" + saleid + "'/>";

                            }
                        }
                        if (TCSAmt > 0) {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + saleDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                                            "UNIT_code='0' NARRATION='" + TCS_Narration + "' AMOUNT='" + TCSAmt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                            "SORT_TYPE='SB' SORT_NO='" + saleDoc_No + "' ac='" + '<%=Session["SaleTCSAcid"] %>' + "' vc='0' progid='16' tranid='0' saleid='" + saleid + "'/>";

                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + saleDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + SB_Ac_Code + "' " +
                                                            "UNIT_code='0' NARRATION='" + TCS_Narration + "' AMOUNT='" + TCSAmt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                            "SORT_TYPE='SB' SORT_NO='" + saleDoc_No + "' ac='" + SB_ac + "' vc='0' progid='16' tranid='0' saleid='" + saleid + "'/>";

                        }
                        if (TDSAmt > 0) {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + purcDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + <%=Session["SaleTDSAc"] %> + "' " +
                              "UNIT_code='0' NARRATION='" + 'TDS:' + GSTNarrationSB + "' AMOUNT='" + TDSAmt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                              "SORT_TYPE='SB' SORT_NO='" + purcDoc_No + "' ac='" + <%=Session["SaleTDSacid"] %> + "' vc='0' progid='5' tranid='0' saleid='" + saleid + "'/>";

                            Order_Code = parseInt(Order_Code) + 1;
                            XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + purcDoc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + SB_Ac_Code + "' " +
                             "UNIT_code='0' NARRATION='" + 'TDS:' + GSTNarrationSB + "' AMOUNT='" + TDSAmt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                             "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                             "SORT_TYPE='SB' SORT_NO='" + purcDoc_No + "' ac='" + SB_ac + "' vc='0' progid='5' tranid='0' saleid='" + saleid + "'/>";
                        }
                        // XML = XML + "</SaleGledger>";

                    }
                }
                //}

                //end SaleBill Calculation

                // Memo Calculate
                if (AUTO_VOUCHER == "YES") {
                    if (parseFloat(MEMO_ADVANCE + VASULI_AMOUNT_1)) {
                        if (memo_no == "" && memo_no == "0") {
                            memo_no = DOC_NO;
                        }
                    }
                }

                // End 

                // Do Calculation 

                if (txtSaleBillTo == "2") {
                    hdnfSaleValue = "NO";
                }
                if (hdnfSaleValue == "No") {
                    saleDoc_No = 0;
                }

                var memogstrate = $("#<%=txtMemoGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtMemoGSTRate.ClientID %>").val();

        if (RecordStatus == "Save") {

            XML = XML + "<DoHead tran_type='DO' doc_no='" + DOC_NO + "' desp_type='" + DESP_TYPE + "' doc_date='" + DOC_DATE + "' mill_code='" + txtMILL_CODE + "' grade='" + txtGRADE + "' " +
                "quantal='" + txtquantal + "' packing='" + PACKING + "' bags='" + BAGS + "' mill_rate='" + mill_rate + "' sale_rate='" + SALE_RATE + "' Tender_Commission='" + Tender_Commission + "' " +
                "diff_rate='" + DIFF_RATE + "' diff_amount='" + DIFF_AMOUNT + "' amount='" + millamt + "' DO='" + DO_CODE + "' voucher_by='" + txtvoucher_by + "' broker='" + BROKER_CODE + "' " +
                "company_code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' purc_no='" + txtPurcNo + "' purc='' purc_order='" + purc_order + "' purc_type='' " +
                "truck_no='" + TRUCK_NO + "' transport='" + TRANSPORT_CODE + "' less='0.00' less_amount='0.00' final_amout='" + millamt + "' vasuli='0.00' " +
                "narration1='" + myNarration + "' narration2='" + NARRATION2 + "' narration3='" + NARRATION3 + "' narration4='" + NARRATION4 + "' narration5='" + NARRATION5 + "' " +
                "excise_rate='" + txtexcise_rate + "' memo_no='" + memo_no + "' freight='0.00' adv_freight1='0.00' driver_no='" + Driver_Mobile + "' driver_Name='' voucher_no='" + purcDoc_No + "' " +
                "voucher_type='" + voucher_type + "' GETPASSCODE='" + txtGETPASS_CODE + "' tender_Remark='' vasuli_rate='" + VASULI_RATE + "' vasuli_amount= '" + VASULI_AMOUNT + "' " +
                "to_vasuli='0' naka_delivery='' send_sms='' Itag='' Ac_Code='" + Ac_Code + "' FreightPerQtl='" + FRIEGHT_RATE + "' Freight_Amount='" + FRIEGHT_AMOUNT + "' " +
            "Freight_RateMM='0.00' Freight_AmountMM='0.00' Memo_Advance='" + MEMO_ADVANCE + "' Paid_Rate1='0.00' Paid_Amount1='0.00' Paid_Narration1='' Paid_Rate2='0.00' Paid_Amount2='0.00' " +
            "Paid_Narration2='' Paid_Rate3='0.00' Paid_Amount3='0.00' Paid_Narration3='0.00' MobileNo='' Created_By='" + USER + "' Modified_By='" + USER + "' UTR_No='' UTR_Year_Code='0' " +
            "Carporate_Sale_No='" + Carporate_Sale_No + "' Carporate_Sale_Year_Code='0' Delivery_Type='" + Delivery_Type + "'  WhoseFrieght='" + WhoseFrieght + "' SB_No='" + saleDoc_No + "' " +
            "Invoice_No='' vasuli_rate1='" + VASULI_RATE_1 + "' vasuli_amount1='" + VASULI_AMOUNT_1 + "' Party_Commission_Rate='0.00' MM_CC='" + MM_CC + "' MM_Rate='" + MM_Rate + "' Voucher_Brokrage='" + VoucherBrokrage + "' " +
            "Voucher_Service_Charge='" + VoucherServiceCharge + "' Voucher_RateDiffRate='" + VoucherRateDiffRate + "' Voucher_RateDiffAmt='" + VoucherRateDiffAmt + "' " +
            "Voucher_BankCommRate='" + VoucherBankCommRate + "' Voucher_BankCommAmt='" + VoucherBankCommAmt + "' Voucher_Interest='" + VoucherInterest + "' " +
          "Voucher_TransportAmt='" + VoucherTransport + "' Voucher_OtherExpenses='" + VoucherOtherExpenses + "' CheckPost='' SaleBillTo='" + txtSaleBillTo + "' " +
            "Pan_No='" + PAN_NO + "' Vasuli_Ac='" + txtVasuliAc + "' LoadingSms='' GstRateCode='" + txtGstRate + "' GetpassGstStateCode='" + txtGetpassGstStateCode + "' VoucherbyGstStateCode='" + txtVoucherbyGstStateCode + "' " +
          "SalebilltoGstStateCode='" + txtSalebilltoGstStateCode + "' GstAmtOnMR='0.00' GstAmtOnSR='0.00' GstExlSR='0.00' GstExlMR='0.00' " +
           "MillGSTStateCode='" + txtMillGstStateCode + "' TransportGSTStateCode='" + txtTransportGstStateCode + "' EWay_Bill_No='" + EWayBill_No + "' Distance='" + txtDistance + "' " +
           "EWayBillChk='" + EWay_BillChk + "' MillInvoiceNo='" + MillInvoiceno + "' Purchase_Date='" + PUR_DATE + "' doid='" + doid + "'  mc='" + mc + "' gp='" + gp + "' st='" + st + "' sb='" + sb + "' " +
           "tc='" + tc + "' itemcode='" + txtitem_Code + "' cs='" + cscode + "' ic='" + ic + "' tenderdetailid='" + TenderdetailId + "' bk='" + bk + "' docd='" + docd + "' vb='" + st + "' " +
           "va='" + va + "' carporate_ac='" + BillTo + "' ca='" + bt + "' mill_inv_date='" + MillInv_Date + "' mill_rcv='" + Inv_Chk + "' saleid='" + saleid + "' MillEwayBill='" + MillEwayBill + "' " +
            "TCS_Rate='" + TCS_Rate + "' Sale_TCS_Rate='" + TCSRate_sale + "' Mill_AmtWO_TCS='" + millamtTCS + "' salecreate='" + hdnfSaleValue +
            "' newsbno='" + newsbno + "' newsbdate='" + newsbdate + "' einvoiceno='" + einvoiceno + "' ackno='" + ackno +
            "' commisionid='" + commisionid + "' brandcode='" + brandcode + "' Cash_diff='" + cashdiffvalue +
            "' CashDiffAc='" + CashdiffAc + "' TDSAc='" + TDSAc + "' CashDiffAcId='" + CashDiffid + "' TDSAcId='"
            + TDSID + "' TDSRate='" + TDSRate + "' TDSAmt='" + TDSAmtDO + "' TDSCut='" + TDSCut_Chk + "' tenderid='" + TenderID +
            "' MemoGSTRate='" + memogstrate + "' RCMCGSTAmt='" + memocgstamt + "' RCMSGSTAmt='" + memosgstamt + "' RCMIGSTAmt='" + memoigstamt +
            "' RCMNumber='0' EwayBillValidDate='" + DOC_DATE + "' PurchaseTDSRate='" + PurchaseTDSrate + "' SaleTDSRate='" + SaleTDSrate +
            "' PurchaseRate='" + Purchase_Rate + "' SBNarration='" + SBNARRATION + "' Do_DATE='" + Do_DATE + "' Insured='" + Insured + "' Insurance='" + Insurance + "' SB_Other_Amount='" + SB_Other_Amount + "' ISEInvoice='" + ISEInvoice + "'>";

        }
        else {
            XML = XML + "<DoHead tran_type='DO' doc_no='" + DOC_NO + "' desp_type='" + DESP_TYPE + "' doc_date='" + DOC_DATE + "' mill_code='" + txtMILL_CODE + "' grade='" + txtGRADE + "' " +
                 "quantal='" + txtquantal + "' packing='" + PACKING + "' bags='" + BAGS + "' mill_rate='" + mill_rate + "' sale_rate='" + SALE_RATE + "' Tender_Commission='" + Tender_Commission + "' " +
                 "diff_rate='" + DIFF_RATE + "' diff_amount='" + DIFF_AMOUNT + "' amount='" + millamt + "' DO='" + DO_CODE + "' voucher_by='" + txtvoucher_by + "' broker='" + BROKER_CODE + "' " +
                 "company_code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' purc_no='" + txtPurcNo + "' purc='' purc_order='" + purc_order + "' purc_type='' " +
                 "truck_no='" + TRUCK_NO + "' transport='" + TRANSPORT_CODE + "' less='0.00' less_amount='0.00' final_amout='" + millamt + "' vasuli='0.00' " +
                 "narration1='" + myNarration + "' narration2='" + NARRATION2 + "' narration3='" + NARRATION3 + "' narration4='" + NARRATION4 + "' narration5='" + NARRATION5 + "' " +
                 "excise_rate='" + txtexcise_rate + "' memo_no='" + memo_no + "' freight='0.00' adv_freight1='0.00' driver_no='" + Driver_Mobile + "' driver_Name='' voucher_no='" + purcDoc_No + "' " +
                 "voucher_type='" + voucher_type + "' GETPASSCODE='" + txtGETPASS_CODE + "' tender_Remark='' vasuli_rate='" + VASULI_RATE + "' vasuli_amount= '" + VASULI_AMOUNT + "' " +
                 "to_vasuli='0' naka_delivery='' send_sms='' Itag='' Ac_Code='" + Ac_Code + "' FreightPerQtl='" + FRIEGHT_RATE + "' Freight_Amount='" + FRIEGHT_AMOUNT + "' " +
             "Freight_RateMM='0.00' Freight_AmountMM='0.00' Memo_Advance='" + MEMO_ADVANCE + "' Paid_Rate1='0.00' Paid_Amount1='0.00' Paid_Narration1='' Paid_Rate2='0.00' Paid_Amount2='0.00' " +
             "Paid_Narration2='' Paid_Rate3='0.00' Paid_Amount3='0.00' Paid_Narration3='0.00' MobileNo='' Created_By='" + USER + "' Modified_By='" + USER + "' UTR_No='' UTR_Year_Code='0' " +
             "Carporate_Sale_No='" + Carporate_Sale_No + "' Carporate_Sale_Year_Code='0' Delivery_Type='" + Delivery_Type + "'  WhoseFrieght='" + WhoseFrieght + "' SB_No='" + saleDoc_No + "' " +
             "Invoice_No='' vasuli_rate1='" + VASULI_RATE_1 + "' vasuli_amount1='" + VASULI_AMOUNT_1 + "' Party_Commission_Rate='0.00' MM_CC='" + MM_CC + "' MM_Rate='" + MM_Rate + "' Voucher_Brokrage='" + VoucherBrokrage + "' " +
             "Voucher_Service_Charge='" + VoucherServiceCharge + "' Voucher_RateDiffRate='" + VoucherRateDiffRate + "' Voucher_RateDiffAmt='" + VoucherRateDiffAmt + "' " +
             "Voucher_BankCommRate='" + VoucherBankCommRate + "' Voucher_BankCommAmt='" + VoucherBankCommAmt + "' Voucher_Interest='" + VoucherInterest + "' " +
           "Voucher_TransportAmt='" + VoucherTransport + "' Voucher_OtherExpenses='" + VoucherOtherExpenses + "' CheckPost='' SaleBillTo='" + txtSaleBillTo + "' " +
             "Pan_No='" + PAN_NO + "' Vasuli_Ac='" + txtVasuliAc + "' LoadingSms='' GstRateCode='" + txtGstRate + "' GetpassGstStateCode='" + txtGetpassGstStateCode + "' VoucherbyGstStateCode='" + txtVoucherbyGstStateCode + "' " +
           "SalebilltoGstStateCode='" + txtSalebilltoGstStateCode + "' GstAmtOnMR='0.00' GstAmtOnSR='0.00' GstExlSR='0.00' GstExlMR='0.00' " +
            "MillGSTStateCode='" + txtMillGstStateCode + "' TransportGSTStateCode='" + txtTransportGstStateCode + "' EWay_Bill_No='" + EWayBill_No + "' Distance='" + txtDistance + "' " +
            "EWayBillChk='" + EWay_BillChk + "' MillInvoiceNo='" + MillInvoiceno + "' Purchase_Date='" + PUR_DATE + "' doid='" + doid + "'  mc='" + mc + "' gp='" + gp + "' st='" + st + "' sb='" + sb + "' " +
            "tc='" + tc + "' itemcode='" + txtitem_Code + "' cs='" + cscode + "' ic='" + ic + "' tenderdetailid='" + TenderdetailId + "' bk='" + bk + "' docd='" + docd + "' vb='" + st + "' " +
            "va='" + va + "' carporate_ac='" + BillTo + "' ca='" + bt + "' mill_inv_date='" + MillInv_Date + "' mill_rcv='" + Inv_Chk + "' saleid='" + saleid + "' MillEwayBill='" + MillEwayBill + "' " +
             "TCS_Rate='" + TCS_Rate + "' Sale_TCS_Rate='" + TCSRate_sale + "' Mill_AmtWO_TCS='" + millamtTCS + "' salecreate='"
             + hdnfSaleValue + "' newsbno='" + newsbno + "' newsbdate='" + newsbdate + "' einvoiceno='" + einvoiceno +
             "' ackno='" + ackno + "' commisionid='" + commisionid + "' brandcode='" + brandcode + "' Cash_diff='" + cashdiffvalue +
              "' CashDiffAc='" + CashdiffAc + "' TDSAc='" + TDSAc + "' CashDiffAcId='" + CashDiffid + "' TDSAcId='"
            + TDSID + "' TDSRate='" + TDSRate + "' TDSAmt='" + TDSAmtDO + "' TDSCut='" + TDSCut_Chk + "' tenderid='" + TenderID +
            "' MemoGSTRate='" + memogstrate + "' RCMCGSTAmt='" + memocgstamt + "' RCMSGSTAmt='" + memosgstamt +
            "' RCMIGSTAmt='" + memoigstamt + "' EwayBillValidDate='" + EwayBill_ValidDate +
            "' PurchaseTDSRate='" + PurchaseTDSrate + "' SaleTDSRate='" + SaleTDSrate + "' PurchaseRate='" + Purchase_Rate +
            "' SBNarration='" + SBNARRATION + "' Do_DATE='" + Do_DATE + "' Insured='" + Insured + "' Insurance='" + Insurance + "' SB_Other_Amount='" + SB_Other_Amount + "' ISEInvoice='" + ISEInvoice + "'>";
        }

        var CheckingFlag = "";
        for (var i = 1; i < grdrow.length; i++) {
            var doDetail = gridView.rows[i].cells[2].innerHTML;
            var ddtype = gridView.rows[i].cells[3].innerHTML;
            var Bank_Code = gridView.rows[i].cells[4].innerHTML;
            var Narra = gridView.rows[i].cells[6].innerHTML;
            var bc = gridView.rows[i].cells[14].innerHTML;
            var Amount = gridView.rows[i].cells[7].innerHTML;
            var Utr_no = gridView.rows[i].cells[8].innerHTML;
            var LT_no = gridView.rows[i].cells[9].innerHTML;
            var UTRDetail_ID = gridView.rows[i].cells[11].innerHTML;
            var doautoid = "";
            if (UTRDetail_ID == "" || UTRDetail_ID == "&nbsp;") {
                UTRDetail_ID = 0;
            }
            var UtrYearCode = gridView.rows[i].cells[15].innerHTML;
            if (UtrYearCode == "" || UtrYearCode == "&nbsp;") {
                UtrYearCode = 0;
            }
            var UtrCompanyCode = gridView.rows[i].cells[16].innerHTML;
            if (UtrCompanyCode == "" || UtrCompanyCode == "&nbsp;") {
                UtrCompanyCode = 0;
            }

            var ddid = dodetailid;
            if (gridView.rows[i].cells[12].innerHTML == "A") {
                XML = XML + "<DoDetailInsert doc_no='" + DOC_NO + "' detail_Id='" + doDetail + "' ddType='" + ddtype + "' Bank_Code='" + Bank_Code + "' Narration='" + Narra + "' Amount='" + Amount + "' " +
              "Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' UTR_NO='" + Utr_no + "' " +
              "DO_No='" + DOC_NO + "' UtrYearCode='" + UtrYearCode + "' UtrCompanyCode='" + UtrCompanyCode + "' LTNo='" + LT_no + "' bc='" + bc + "' utrdetailid='" + UTRDetail_ID + "' dodetailid='" + ddid + "' doid='" + doid + "'/>";
                ddid = parseInt(ddid) + 1;
            }
            else if (gridView.rows[i].cells[12].innerHTML == "U") {
                var doautoid = gridView.rows[i].cells[10].innerHTML;
                XML = XML + "<DoDetail doc_no='" + DOC_NO + "' detail_Id='" + doDetail + "' ddType='" + ddtype + "' Bank_Code='" + Bank_Code + "' Narration='" + Narra + "' Amount='" + Amount + "' " +
              "Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' UTR_NO='" + Utr_no
              + "' DO_No='" + DOC_NO + "' UtrYearCode='" + UtrYearCode + "' UtrCompanyCode='" + UtrCompanyCode + "' LTNo='" + LT_no + "' bc='" + bc + "' utrdetailid='" + UTRDetail_ID +
              "' dodetailid='" + doautoid + "' doid='" + doid + "'/>";
            }
            else if (gridView.rows[i].cells[12].innerHTML == "D") {
                var doautoid = gridView.rows[i].cells[10].innerHTML;
                XML = XML + "<DoDetailInsert doc_no='" + DOC_NO + "' detail_Id='" + doDetail + "' ddType='" + ddtype + "' Bank_Code='" + Bank_Code + "' Narration='" + Narra + "' Amount='" + Amount + "' " +
              "Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' UTR_NO='" + Utr_no + "' " +
              "DO_No='" + DOC_NO + "' UtrYearCode='" + UtrYearCode + "' UtrCompanyCode='" + UtrCompanyCode + "' LTNo='" + LT_no + "' bc='" + bc + "' utrdetailid='" + UTRDetail_ID + "' dodetailid='" + ddid + "' doid='" + doid + "'/>";

            }


        }

        XML = XML + "</DoHead></ROOT>";

      



        ProcessXML(XML, RecordStatus, spname, DESP_TYPE);

    }
    catch (err) {

        $("#loader").hide();
        alert(err)


    }

}
function ProcessXML(XML, RecordStatus, spname, DESP_TYPE) {
    debugger;
    $.ajax({
        type: "POST",
        //url: "../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx/NewInsert",
        url: "../xmlExecuteDMLQry.asmx/ExecuteXMLQryDO",
        data: '{XML:"' + XML + '",status:"' + RecordStatus + '",spname:"' + spname + '",Type:"' + DESP_TYPE + '"}',
        //data: '{DoInsert: "' + DOInsert + '", DoDetail_Insert: "' + DoDetail_Insert + '",PurchaseInsert: "' + PurchaseInsert + '",PurchaseInsertDetail: "' + PurchaseInsertDetail + '",SaleBillInsert: "' + SaleBillInsert + '",SaleBillDetailInsert: "' + SaleBillDetailInsert + '",TenderInsert: "' + TenderInsert + '",TenderUpdate: "' + TenderUpdate + '",DoDetailUpdate:"' + DoDetailUpdate + '",DoDetailDelete:"' + DoDetailDelete + '",GLEDGERDO_Delete:"' + GLEDGERDO_Delete + '",GLEDGER_Insert:"' + GLEDGER_Insert + '",RecordStatus:"' + RecordStatus + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
            $("#loader").hide();
        },
        error: function (response) {
            alert(response.d);
            $("#loader").hide();

        }
    });

    function OnSuccess(response) {
        debugger;
        $("#loader").hide();
        if (RecordStatus != "Delete") {
            if (response.d.length > 0) {
                var word = response.d;
                var len = word.length;
                var pos = word.indexOf(",");
                var id = word.slice(0, pos);
                var doc = word.slice(pos + 1, len);
                if (status == "Save") {
                    //alert('Sucessfully Added Record !!! Doc_no=' + doc)
                    swal("Sucessfully Added Record!", " Doc_no=" + doc + "", "success");
                }
                else {
                    //alert('Sucessfully Updated Record !!! Doc_no=' + doc)
                    swal("Sucessfully Updated Record!", " Doc_no=" + doc + "", "success");

                }

                window.open('../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx?DO=' + id + '&Action=1', "_self");

            }
            else {
                var num = parseInt(response.d);

                if (isNaN(num)) {
                    //alert(response.d)
                    swal("Network Error!!!", "", "warning");

                }
            }
        }
        else {
            var num = parseInt(response.d);

            if (isNaN(num)) {
                //alert(response.d)
                swal("" + response.d + "", "", "warning");

            }
            else {
                window.open('../BussinessRelated/PgeDoHeadUtility.aspx', "_self");
            }
        }

    }
}
    </script>
    <%-- <script language="JavaScript" type="text/javascript">
        window.onbeforeunload = confirmExit;
        function confirmExit() {
            return "Are you sure you want to exit this page?";
        }
    </script>--%>
    <style>
        #loader {
            width: 100%;
            height: 100%;
            background-color: gray;
            position: fixed;
            margin: -0.7%;
            opacity: 0.6;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Delivery Order For GST   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px;">
        <img height="40%" width="40%" src="../Images/ajax-loader3.gif" />
    </div>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfpurcid" runat="server" />
            <asp:HiddenField ID="hdnfsaleid" runat="server" />
            <asp:HiddenField ID="hdnfcommid" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnvouchernumber" runat="server" />
            <asp:HiddenField ID="hdnmemonumber" runat="server" />
            <asp:HiddenField ID="hdnfpacking" runat="server" />
            <asp:HiddenField ID="hdnfPDSPartyCode" runat="server" />
            <asp:HiddenField ID="hdnfPDSUnitCode" runat="server" />
            <asp:HiddenField ID="hdnfSB_No" runat="server" />
            <asp:HiddenField ID="hdnfSaleRate" runat="server" />
            <asp:HiddenField ID="hdnfUtrBalance" runat="server" />
            <asp:HiddenField ID="hdnfMainBankAmount" runat="server" />
            <asp:HiddenField ID="hdnfOldBillAmt" runat="server" />
            <asp:HiddenField ID="hdnfTenderQty" runat="server" />
            <asp:HiddenField ID="hdnfTenderID" runat="server" />
            <asp:HiddenField ID="hdnfTenderDetailid" runat="server" />
            <asp:HiddenField ID="hdnfUtrdetail" runat="server" />
            <asp:HiddenField ID="hdnfgeneratesalebill" runat="server" />
            <asp:HiddenField ID="hdnfmillshortname" runat="server" />
            <asp:HiddenField ID="hdnfshiptoshortname" runat="server" />
            <asp:HiddenField ID="hdnfgetpassshortname" runat="server" />
            <asp:HiddenField ID="hdnftransportshortname" runat="server" />
            <asp:HiddenField ID="hdnfbilltoshortname" runat="server" />
            <asp:HiddenField ID="hdnfsalebilltoshortname" runat="server" />
            <asp:HiddenField ID="hdnfbrokershortName" runat="server" />
            <asp:HiddenField ID="hdnfmc" runat="server" />
            <asp:HiddenField ID="hdnfgp" runat="server" />
            <asp:HiddenField ID="hdnfst" runat="server" />
            <asp:HiddenField ID="hdnfsb" runat="server" />
            <asp:HiddenField ID="hdnftc" runat="server" />
            <asp:HiddenField ID="hdnfbk" runat="server" />
            <asp:HiddenField ID="hdnfva" runat="server" />
            <asp:HiddenField ID="hdnfbt" runat="server" />
            <asp:HiddenField ID="hdnfdocd" runat="server" />
            <asp:HiddenField ID="hdnfcscode" runat="server" />
            <asp:HiddenField ID="hdnfic" runat="server" />
            <asp:HiddenField ID="hdnfsaledetailid" runat="server" />
            <asp:HiddenField ID="hdnfpurcdetailid" runat="server" />
            <asp:HiddenField ID="hdnfTDSAcid" runat="server" />
            <asp:HiddenField ID="hdnfCashDiffAcid" runat="server" />
            <asp:HiddenField ID="hdnfDodoc" runat="server" />
            <asp:HiddenField ID="hdnfDoid" runat="server" />
            <asp:HiddenField ID="hdnfpaymentid" runat="server" />
            <asp:HiddenField ID="hdnfpaymentTo" runat="server" />
            <asp:HiddenField ID="hdnfpaymentShort" runat="server" />
            <asp:HiddenField ID="hdnfPaymentStateCode" runat="server" />
            <asp:HiddenField ID="hdnfPDS" runat="server" />
            <asp:HiddenField ID="hdnfpdsacID" runat="server" />
            <asp:HiddenField ID="hdnfpdsunitID" runat="server" />
            <asp:HiddenField ID="hdnfpdspartyStateCode" runat="server" />
            <asp:HiddenField ID="hdnfpdsunitStateCode" runat="server" />
            <asp:HiddenField ID="hdnfbilltoStateCode" runat="server" />
            <asp:HiddenField ID="hdnfQTY" runat="server" />
            <asp:HiddenField ID="hdnfUnitCity" runat="server" />
            <asp:HiddenField ID="hdnfpaymenttosb" runat="server" />
            <asp:HiddenField ID="hdnfpaymentcomnfirm" runat="server" />
            <asp:HiddenField ID="hdnfmemogstrate" runat="server" />
            <asp:HiddenField ID="hdnfmemocgst" runat="server" />
            <asp:HiddenField ID="hdnfmemosgst" runat="server" />
            <asp:HiddenField ID="hdnfmemoigst" runat="server" />
            <asp:HiddenField ID="hdnfTCSRate" runat="server" />
            <asp:HiddenField ID="hdnfTCSratepur" runat="server" />
            <asp:HiddenField ID="hdnfsupplieshortname" runat="server" />
            <asp:HiddenField ID="hdnfSaleTDS" runat="server" />
            <asp:HiddenField ID="hdnfPurchaseTDS" runat="server" />
            <asp:HiddenField ID="hdnfsalebillto" runat="server" />
            <asp:HiddenField ID="hdnftenderno" runat="server" />
            <asp:HiddenField ID="hdnfhsnnumber" runat="server" />
            <asp:HiddenField ID="hdnfistds" runat="server" />
            <asp:HiddenField ID="hdnfminRate" runat="server" />
            <asp:HiddenField ID="hdnfmaxRate" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />


             <asp:HiddenField ID="hdnfoldsalebillto" runat="server" />
            <asp:HiddenField ID="hdnfoldqntl" runat="server" />
            <asp:HiddenField ID="hdnfoldsalerate" runat="server" />
            <asp:HiddenField ID="hdnfoldfrieght" runat="server" />
            <asp:HiddenField ID="hdnfoldmemoadv" runat="server" />
            <asp:HiddenField ID="hdnfstatuscheck" runat="server" />

            <asp:HiddenField ID="hdnfAutoPurchaseBill" runat="server" />
            <div>
                <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                    Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 90; float: left">
                    <table cellspacing="5" align="left">
                        <tr>
                            <td style="width: 100%;" colspan="6">
                                <table width="100%" align="left" style="border: 1px solid white;">
                                    <tr>
                                        <td align="left" colspan="8">
                                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                                TabIndex="64" ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />&nbsp;
                                            <asp:Button OnClientClick="if (!Validate()) return false;" ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                                                TabIndex="71" ValidationGroup="add" OnClick="btnSave_Click" UseSubmitBehavior="false"
                                                Height="24px" />
                                            &nbsp;
                                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                                                TabIndex="71" ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                                            &nbsp;
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                                                TabIndex="72" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="return Confirm(this, event);" Height="24px" />
                                            &nbsp;
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                                                TabIndex="73" ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" />
                                            &nbsp;
                                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                                                TabIndex="74" ValidationGroup="save" Height="24px" OnClientClick="Back()" />

                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                                                OnClick="btnFirst_Click" Width="90px" Height="24px" />
                                            <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="Previous" CssClass="btnHelp"
                                                OnClick="btnPrevious_Click" Width="90px" Height="24px" />
                                            <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="Next" CssClass="btnHelp"
                                                OnClick="btnNext_Click" Width="90px" Height="24px" />
                                            <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="Last" CssClass="btnHelp"
                                                OnClick="btnLast_Click" Width="90px" Height="24px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Change No:</td>
                            <td align="left" colspan="4">
                                <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                    TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender runat="server" ID="txtxf" FilterType="Numbers" InvalidChars="."
                                    TargetControlID="txtEditDoc_No">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;<asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                    Font-Size="Small" ForeColor="Red"></asp:Label>

                                <asp:TextBox ID="txtnewsbno" runat="Server" CssClass="txt" Width="80px" Text="0"
                                    TabIndex="70" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtnewsbno_TextChanged"
                                    Height="24px" Visible="false"></asp:TextBox>
                                Date:
                                <asp:TextBox ID="txtnewsbdate" runat="Server" CssClass="txt" Width="80px"
                                    TabIndex="71" MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                    Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtnewsbdate_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Image ID="imgcalSB" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                    Height="15px" />
                                <ajax1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtnewsbdate"
                                    PopupButtonID="imgcalSB" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>

                                EInvoice No:
                                <asp:TextBox ID="txteinvoiceno" runat="Server" CssClass="txt" Width="120px"
                                    TabIndex="72" Style="text-align: right;" AutoPostBack="True"
                                    Height="24px"></asp:TextBox>
                                ACKNo:
                                <asp:TextBox ID="txtackno" runat="Server" CssClass="txt" Width="150px"
                                    TabIndex="73" Style="text-align: right;" AutoPostBack="True"
                                    Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Entry No: &nbsp;
                            </td>
                            <td align="left" colspan="7" style="width: 100%;">
                                <asp:TextBox ID="txtdoc_no" runat="Server" CssClass="txt" Width="80px" Style="text-align: right;"
                                    AutoPostBack="True" OnTextChanged="txtdoc_no_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtDOC_NO" runat="server" Text="..." Width="80px" OnClick="btntxtdoc_no_Click"
                                    CssClass="btnHelp" Height="24px" />
                                <asp:Label ID="lbldoid" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                    Font-Size="Small" ForeColor="Red" Visible="false"></asp:Label>
                                Date:
                                <asp:TextBox ID="txtDOC_DATE" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                    Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtDOC_DATE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                    Width="25px" Height="15px" />
                                <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtDOC_DATE"
                                    PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                                Do Date:
                                 <asp:TextBox ID="txtdo_date" runat="Server" CssClass="txt" Width="80px"
                                     TabIndex="71" MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                     Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtdo_date_TextChanged"
                                     Height="24px"></asp:TextBox>
                                <asp:Image ID="imgdodate" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                    Height="15px" />
                                <ajax1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtdo_date"
                                    PopupButtonID="imgdodate" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                                &nbsp;Carporate Sale:<asp:TextBox ID="txtcarporateSale" CssClass="txt" Width="80px"
                                    TabIndex="2" onkeydown="carporatesale(event);" runat="server" AutoPostBack="false" OnTextChanged="txtcarporateSale_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtcarporateSale" runat="server" CssClass="btnHelp" Text="C. Sale"
                                    Width="80px" OnClick="btntxtcarporateSale_Click" Height="24px" />
                                <asp:Label ID="lblCSYearCode" runat="server" CssClass="lblName"></asp:Label>
                                <asp:Label runat="server" ID="lblPDSParty" CssClass="lblName" ForeColor="Yellow"></asp:Label>
                                &nbsp; Do Type:
                                <asp:DropDownList ID="drpDOType" runat="server" CssClass="ddl" Width="100px" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpDOType_SelectedIndexChanged" Height="26px" TabIndex="3">
                                    <asp:ListItem Text="Dispatch" Value="DI"></asp:ListItem>
                                    <asp:ListItem Text="D.O." Value="DO"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtDESP_TYPE" runat="Server" CssClass="txt" TabIndex="2" Width="80px" style="text-align:left;"
                                      AutoPostBack="True" ontextchanged="txtDESP_TYPE_TextChanged"></asp:TextBox>--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Mill Code:
                            </td>
                            <td align="left" colspan="2" style="width: 10%;">
                                <asp:TextBox ID="txtMILL_CODE" runat="Server" CssClass="txt" TabIndex="4" Width="80px"
                                    onkeydown="millcode(event);" Style="text-align: right;" AutoPostBack="false"
                                    OnTextChanged="txtMILL_CODE_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtMILL_CODE" runat="server" Text="..." OnClick="btntxtMILL_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="LBLMILL_NAME" runat="server" CssClass="lblName"></asp:Label>
                                &nbsp; &nbsp;<asp:Label runat="server" ID="lblPaymenToLegBal" ForeColor="Yellow"
                                    Text=""></asp:Label>
                                Insured
                                <asp:DropDownList ID="drpInsured" runat="server" CssClass="ddl" Width="100px"
                                    Height="26px" TabIndex="3" OnSelectedIndexChanged="drpInsured_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                                Mill State Code:
                                <asp:TextBox ID="txtMillGstStateCode" runat="Server" CssClass="txt" TabIndex="5"
                                    onkeydown="millstatecode(event);" Width="80px" Style="text-align: right;" AutoPostBack="false"
                                    OnTextChanged="txtMillGstStateCode_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtMillGstStateCode" runat="server" Text="..." OnClick="btntxtMillGstStateCode_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lbltxtMillGstStateCode" runat="server" CssClass="lblName"></asp:Label>&nbsp;&nbsp;
                                <asp:TextBox ID="txtMillEmailID" runat="server" Visible="false" CssClass="txt" Width="200px"
                                    AutoPostBack="True" OnTextChanged="txtMillEmailID_TextChanged" TabIndex="5" Height="24px"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;Mill Mobile:&nbsp;<asp:TextBox runat="server" ID="txtMillMobile"
                                    Width="100px" Height="24px" MaxLength="11" CssClass="txt"></asp:TextBox>&nbsp;<asp:Button
                                        runat="server" ID="btnSendSms" Text="Send Sms" CssClass="btnHelp" Height="24px"
                                        Width="80px" OnClick="btnSendSms_Click" />
                            </td>
                        </tr>
                        <tr>
                            <%-- <td align="left">
                            UTR No:
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtUTRNo" CssClass="txt" Width="80px" runat="server" AutoPostBack="true"
                                OnTextChanged="txtUTRNo_TextChanged" TabIndex="6" Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtUTRNo" runat="server" CssClass="btnHelp" Text="UTR No" Width="80px"
                                OnClick="btntxtUTRNo_Click" Height="24px" />
                            <asp:Label ID="lblUTRYearCode" runat="server" CssClass="lblName"></asp:Label>
                            &nbsp;
                        </td>--%>
                            <td align="left">Purc. No:
                            </td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="txtPurcNo" runat="Server" Enabled="false" CssClass="txt" Width="80px"
                                    onkeydown="purcno(event);" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtPurcNo_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtPurcNo" runat="server" Text="..." OnClick="btntxtPurcNo_Click"
                                    TabIndex="6" CssClass="btnHelp" Height="24px" Width="20px" />
                                &nbsp;
                                <asp:TextBox ID="txtPurcOrder" runat="Server" Enabled="false" AutoPostBack="false"
                                    OnTextChanged="txtPurcNo_TextChanged" CssClass="txt" Width="90px" Style="text-align: right;"
                                    Height="24px" TabIndex="6"></asp:TextBox>
                                <asp:Label ID="lbltenderDetailID" runat="server" CssClass="lblName"></asp:Label>
                                &nbsp; Delivery Type:
                                <asp:DropDownList ID="drpDeliveryType" runat="server" CssClass="ddl" Width="240px"
                                    TabIndex="7" Enabled="true" AutoPostBack="true" Height="26px" OnSelectedIndexChanged="drpDeliveryType_SelectedIndexChanged">
                                    <asp:ListItem Text="Commission" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="With GST Naka Delivery" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="Naka Delivery without GST Rate" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="DO" Value="D"></asp:ListItem>
                                </asp:DropDownList>
                                GST Code:
                                <asp:TextBox ID="txtGstRate" runat="Server" CssClass="txt" TabIndex="8" Width="80px"
                                    onkeydown="gstcode(event);" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtGstRate_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtGstRate" runat="server" Text="..." OnClick="btntxtGstRate_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblGstRateName" runat="server" CssClass="lblName"></asp:Label>
                                <asp:Label runat="server" ID="lblPoDetails" CssClass="lblName" ForeColor="Yellow"></asp:Label>
                                &nbsp;Purchase Date:
                                <asp:TextBox ID="txtPurchase_Date" runat="Server" CssClass="txt" TabIndex="9" Width="80px"
                                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                    Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtPurchase_Date_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Image ID="imgcal" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                    Height="15px" />
                                <ajax1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPurchase_Date"
                                    PopupButtonID="imgcal" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Get Pass:
                            </td>
                            <td align="left" colspan="4" style="width: 10%;">
                                <asp:TextBox ID="txtGETPASS_CODE" runat="Server" CssClass="txt" TabIndex="10" Width="80px"
                                    onkeydown="getpass(event);" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtGETPASS_CODE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtGETPASS_CODE" runat="server" Text="..." OnClick="btntxtGETPASS_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="LBLGETPASS_NAME" runat="server" CssClass="lblName"></asp:Label>

                                &nbsp;State Code:
                                <asp:TextBox runat="server" ID="txtGetpassGstStateCode" OnTextChanged="txtGetpassGstStateCode_TextChanged"
                                    TabIndex="11" onkeydown="statecode(event);" AutoPostBack="false" CssClass="txt"
                                    Height="24px" Width="30px" />
                                <asp:Button Text="..." runat="server" ID="btntxtGetpassGstStateCode" OnClick="btntxtGetpassGstStateCode_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label Text="" runat="server" ID="lbltxtGetpassGstStateName" CssClass="lblName" />
                                &nbsp; Item Code
                                <asp:TextBox ID="txtitem_Code" runat="Server" CssClass="txt" TabIndex="12" Width="80px"
                                    onkeydown="item(event);" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtitem_Code_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtitem_Code" runat="server" Text="..." OnClick="btntxtitem_Code_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblitem_Name" runat="server" CssClass="lblName"></asp:Label>
                                &nbsp;
                                  Brand_Code
                            <asp:TextBox ID="txtBrand_Code" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnTextChanged="txtBrand_Code_TextChanged" Style="text-align: left;"
                                TabIndex="13" Width="90px" onkeydown="Brand_Code(event);"></asp:TextBox>
                                <asp:Button ID="btntxtBrand_Code" runat="server" CssClass="btnHelp" OnClick="btntxtBrand_Code_Click"
                                    Text="..." />
                                <asp:Label ID="lblBrandname" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Shipped To:
                            </td>
                            <td align="left" colspan="4" style="width: 10%;">
                                <asp:TextBox ID="txtvoucher_by" runat="Server" CssClass="txt" TabIndex="14" Width="80px"
                                    onkeydown="shipto(event);" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtvoucher_by_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtvoucher_by" runat="server" Text="..." OnClick="btntxtvoucher_by_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblvoucherbyname" runat="server" CssClass="lblName"></asp:Label>
                                &nbsp;&nbsp;<asp:Label runat="server" ID="lblVoucherLedgerByBalance" ForeColor="Yellow"
                                    Text=""></asp:Label>
                                &nbsp;State Code:
                                <asp:TextBox runat="server" ID="txtVoucherbyGstStateCode" OnTextChanged="txtVoucherbyGstStateCode_TextChanged"
                                    TabIndex="15" onkeydown="shiptostatecode(event);" AutoPostBack="true" CssClass="txt"
                                    Height="24px" Width="30px" />
                                <asp:Button Text="..." runat="server" ID="btntxtVoucherbyGstStateCode" OnClick="btntxtVoucherbyGstStateCode_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label Text="" runat="server" ID="lbltxtVoucherbyGstStateName" CssClass="lblName" />
                                &nbsp;Bill To:
                                <asp:TextBox ID="txtBill_To" runat="Server" CssClass="txt" TabIndex="16" Width="90px"
                                    onkeydown="billto(event);" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtBill_To_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtbill_To" runat="server" Text="..." OnClick="btntxtbill_To_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblBill_To" runat="server" CssClass="lblName"></asp:Label>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Sale Bill To:
                            </td>
                            <td align="left" colspan="4" style="width: 10%;">
                                <asp:TextBox runat="server" ID="txtSaleBillTo" Height="24px" Width="80px" Enabled="false"
                                    onkeydown="salebillto(event);" CssClass="txt" OnTextChanged="txtSaleBillTo_TextChanged" AutoPostBack="false"
                                    TabIndex="17"></asp:TextBox>&nbsp;
                                <asp:TextBox ID="txtNARRATION4" runat="Server" CssClass="txt" TabIndex="18" Width="200px"
                                    onkeydown="narration4(event);" Style="text-align: left;" AutoPostBack="false"
                                    OnTextChanged="txtNARRATION4_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtNARRATION4" runat="server" Text="..." OnClick="btntxtNARRATION4_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                &nbsp; &nbsp;<asp:Label runat="server" ID="lblSaleBillToLedgerByBalance" ForeColor="Yellow"
                                    Text=""></asp:Label>
                                &nbsp; State Code:
                                <asp:TextBox runat="server" ID="txtSalebilltoGstStateCode" OnTextChanged="txtSalebilltoGstStateCode_TextChanged"
                                    TabIndex="19" onkeydown="salebillstatecode(event);" AutoPostBack="false" CssClass="txt"
                                    Height="24px" Width="30px" />
                                <asp:Button Text="..." runat="server" ID="btntxtSalebilltoGstStateCode" OnClick="btntxtSalebilltoGstStateCode_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label Text="" runat="server" ID="lbltxtSalebilltoGstStateName" CssClass="lblName" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Grade:
                            </td>
                            <td align="left" colspan="6">
                                <asp:TextBox ID="txtGRADE" runat="Server" CssClass="txt" TabIndex="20" Width="150px"
                                    onkeydown="grade(event);" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtGRADE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtGRADE" runat="server" Text="..." OnClick="btntxtGRADE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                Quantal:
                                <asp:TextBox ID="txtquantal" runat="Server" CssClass="txt" TabIndex="21" Width="100px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtquantal_TextChanged"
                                    Height="24px" onkeydown="qntl(event);"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="filtertxtquantal" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtquantal">
                                </ajax1:FilteredTextBoxExtender>
                                <asp:Label runat="server" ID="count" ForeColor="White" Visible="false"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Packing:&nbsp;&nbsp;
                                <asp:TextBox ID="txtPACKING" runat="Server" CssClass="txt" Width="100px" Style="text-align: right;"
                                    TabIndex="0" AutoPostBack="True" OnTextChanged="txtPACKING_TextChanged" Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteretxtPACKING" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtPACKING">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;Bags:&nbsp;&nbsp;<asp:TextBox ID="txtBAGS" runat="Server" CssClass="txt"
                                    TabIndex="0" Width="100px" ReadOnly="true" Style="text-align: right;" AutoPostBack="True"
                                    OnTextChanged="txtBAGS_TextChanged" Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteretxtBAGS" runat="server" FilterType="Numbers"
                                    TargetControlID="txtBAGS">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;Excise:&nbsp;
                                <asp:TextBox ID="txtexcise_rate" runat="Server" CssClass="txt" Width="100px" Style="text-align: right;"
                                    TabIndex="0" AutoPostBack="True" OnTextChanged="txtexcise_rate_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="Filteretxtexcise_rate" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtexcise_rate">
                                </ajax1:FilteredTextBoxExtender>
                                Mill Amount:
                                <asp:Label ID="lblMillAmount" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Mill Rate:
                            </td>
                            <td align="left" colspan="6" style="vertical-align: top;">
                                <asp:TextBox ID="txtmillRate" runat="Server" CssClass="txt" Width="100px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtmillRate_TextChanged"
                                    Height="24px" TabIndex="0" ReadOnly="true"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteretxtmillRate" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtmillRate">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Sale
                                Rate:
                                <asp:TextBox ID="txtSALE_RATE" runat="Server" CssClass="txt" Width="100px" Style="text-align: right;"
                                    TabIndex="0" AutoPostBack="True" OnTextChanged="txtSALE_RATE_TextChanged" Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteretxtSALE_RATE" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtSALE_RATE">
                                </ajax1:FilteredTextBoxExtender>

                                Purchase Rate:
                                <asp:TextBox ID="txtpurchaserate" runat="Server" CssClass="txt" Width="100px" Style="text-align: right;"
                                    TabIndex="0" AutoPostBack="True" OnTextChanged="txtpurchaserate_TextChanged" Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="Filteredtxtpurchaserate" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtpurchaserate">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;&nbsp;
                                 B.P:
                                <asp:TextBox ID="txtCashDiff" runat="Server" CssClass="txt" Width="100px" Style="text-align: right;"
                                    TabIndex="0" AutoPostBack="True" OnTextChanged="txtCashDiff_TextChanged" Height="24px"></asp:TextBox>


                                <asp:Label ID="lblCashdiffvalue" runat="server" CssClass="lblName" ForeColor="Yellow"></asp:Label>
                                &nbsp;&nbsp; 
                                B.P Ac:
                                <asp:TextBox ID="txtCashDiffAc" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                    onkeydown="CashDiff(event);" Style="text-align: right;" AutoPostBack="True"
                                    OnTextChanged="txtCashDiffAc_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtCashDiffAc" runat="server" Text="..." OnClick="btntxtCashDiffAc_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblCashDiffAcname" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Commission:
                            </td>
                            <td align="left" colspan="6" style="vertical-align: top;">
                                <asp:TextBox runat="server" ID="txtCommission" Width="50px"
                                    TabIndex="0" AutoPostBack="true" Height="24px" CssClass="txt" OnTextChanged="txtCommission_TextChanged" />
                                &nbsp;&nbsp;&nbsp;
                                  Diff:
                                <asp:Label ID="lblDiffrate" runat="server" CssClass="lblName"></asp:Label>
                                &nbsp;&nbsp;&nbsp;Insurance
                                <asp:TextBox runat="server" ID="txtInsurance" Width="90px"
                                    TabIndex="0" AutoPostBack="true" Height="24px" CssClass="txt" OnTextChanged="txtInsurance_TextChanged" />

                                &nbsp;&nbsp;&nbsp;Purchase TCS%
                                <asp:TextBox ID="txtTCSRate" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                    OnTextChanged="txtTCSRate_TextChanged" TabIndex="0" Style="text-align: right;"
                                    Height="24px"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;Sale TCS%
                                <asp:TextBox ID="txtTCSRate_Sale" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                    OnTextChanged="txtTCSRate_Sale_TextChanged" TabIndex="0" Style="text-align: right;"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtTCSRate">
                                </ajax1:FilteredTextBoxExtender>



                                &nbsp;&nbsp;&nbsp;Purchase TDS%
                                <asp:TextBox ID="txtPurchaseTDS" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                    OnTextChanged="txtPurchaseTDS_TextChanged" TabIndex="0" Style="text-align: right;"
                                    Height="24px"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;Sale TDS%
                                <asp:TextBox ID="txtSaleTDS" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                    OnTextChanged="txtSaleTDS_TextChanged" TabIndex="0" Style="text-align: right;"
                                    Height="24px"></asp:TextBox>

                                Mill Amt With TCS:
                                <asp:Label ID="lblMillAmtWOTCS" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td align="left" style="border-bottom: 3px solid white; width: 10%;">Truck No:
                            </td>
                            <td align="left" style="width: 20%; border-bottom: 3px solid white;" colspan="6">
                               <asp:TextBox ID="txtTruck_NO" runat="Server" CssClass="txt" TabIndex="35" Width="140px"  MaxLength="10"
                                   ToolTip="seperate numbers by comma" Style="text-align: left; text-transform: uppercase;" AutoPostBack="false"
                                    Height="24px"></asp:TextBox>&nbsp; 
                                    <ajax1:FilteredTextBoxExtender ID="FiltertxtTruck_NO" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters"
                                    ValidChars="," TargetControlID="txtTruck_NO">
                                </ajax1:FilteredTextBoxExtender>&nbsp;
                                
                                
                                 Driver Mobile:
                                <asp:TextBox runat="server" ID="txtDriverMobile" CssClass="txt" TabIndex="36" Width="140px" MaxLength="10"
                                    ToolTip="seperate numbers by comma" Style="text-align: left;" Height="24px" OnTextChanged="txtDriverMobile_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="ajxmob" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="," TargetControlID="txtDriverMobile">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp; Transport:
                                <asp:TextBox ID="txtTRANSPORT_CODE" runat="Server" CssClass="txt" TabIndex="37" Width="80px"
                                    onkeydown="transport(event);" Style="text-align: right;" AutoPostBack="True"
                                    OnTextChanged="txtTRANSPORT_CODE_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtTRANSPORT_CODE" runat="server" Text="..." OnClick="btntxtTRANSPORT_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="LBLTRANSPORT_NAME" runat="server" CssClass="lblName"></asp:Label>&nbsp;PAN:<asp:TextBox
                                    ID="txtPanNo" Width="150px" Height="24px" CssClass="txt" runat="server" />&nbsp;
                                State Code:
                                <asp:TextBox ID="txtTransportGstStateCode" runat="Server" CssClass="txt" Width="80px"
                                    TabIndex="38" onkeydown="transportstatecode(event);" Style="text-align: right;"
                                    AutoPostBack="True" OnTextChanged="txtTransportGstStateCode_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtTransportGstStateCode" runat="server" Text="..." OnClick="btntxtTransportGstStateCode_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lbltxtTransportGstStateCode" runat="server" CssClass="lblName"></asp:Label>


                            </td>
                        </tr>
                        <tr>
                            <td align="left">Diff Amount:
                            </td>
                            <td align="left" style="width: 10%;" colspan="3">
                                <asp:TextBox ID="txtDIFF_AMOUNT" runat="Server" CssClass="txt" Width="100px" ReadOnly="true"
                                    TabIndex="39" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtDIFF_AMOUNT_TextChanged"
                                    Height="24px"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Frieght:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox
                                    TabIndex="40" runat="server" ID="txtFrieght" CssClass="txt" Width="100px" Style="text-align: right;"
                                    AutoPostBack="true" Height="24px" OnTextChanged="txtFrieght_TextChanged"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txtFrieghtAmount" CssClass="txt" AutoPostBack="false"
                                    TabIndex="0" ReadOnly="true" Height="24px" Width="100px" Style="text-align: right;"
                                    OnTextChanged="txtFrieghtAmount_TextChanged"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp; Memo Advance:
                                <asp:DropDownList runat="server" ID="drpCC" CssClass="ddl" Width="70px" Height="24px"
                                    TabIndex="0">
                                    <asp:ListItem Text="Credit" Value="Credit"></asp:ListItem>
                                    <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;rate:<asp:TextBox runat="server" ID="txtMemoAdvanceRate" Width="40px" Height="24px"
                                    TabIndex="43" AutoPostBack="true" CssClass="txt" OnTextChanged="txtMemoAdvanceRate_TextChanged"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txtMemoAdvance" CssClass="txt" Width="100px" Style="text-align: right;"
                                    TabIndex="0" AutoPostBack="true" Height="24px" OnTextChanged="txtMemoAdvance_TextChanged"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblFrieghtToPay" ForeColor="Yellow"></asp:Label>
                                MemoGSTRate Code:
                                <asp:TextBox ID="txtMemoGSTRate" runat="Server" CssClass="txt" Width="80px"
                                    TabIndex="0" onkeydown="MemoGSTRate(event);" Style="text-align: right;"
                                    AutoPostBack="True" OnTextChanged="txtMemoGSTRate_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtMemoGSTRate" runat="server" Text="..." OnClick="btntxtMemoGSTRate_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblMemoGSTRate" runat="server" CssClass="lblName"></asp:Label>
                                RCM No:<asp:TextBox runat="server" ID="txtRCM_No" Width="50px" Height="24px"
                                    AutoPostBack="true" CssClass="txt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">TDS Ac:
                            </td>
                            <td align="left" style="width: 10%;" colspan="3">
                                <asp:TextBox ID="txtTDSAc" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                    onkeydown="TDSAc(event);" Style="text-align: right;" AutoPostBack="True"
                                    OnTextChanged="txtTDSAc_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtTDSAc" runat="server" Text="..." OnClick="btntxtTDSAc_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblTDSAcname" runat="server" CssClass="lblName"></asp:Label>
                                TDS %:<asp:TextBox runat="server" ID="txttdsrate" Width="40px" Height="24px"
                                    TabIndex="0" AutoPostBack="true" CssClass="txt" OnTextChanged="txttdsrate_TextChanged"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txttdsamount" CssClass="txt" Width="100px" Style="text-align: right;"
                                    TabIndex="0" AutoPostBack="true" Height="24px" OnTextChanged="txttdsamount_TextChanged"></asp:TextBox>
                                TDS Cut By Us<asp:CheckBox runat="server"
                                    ID="chkTDSCutByUs" Width="10px" Height="10px" />
                                <asp:DropDownList
                                    runat="server" Visible="false" Width="100px" ID="ddlFrieghtType" CssClass="ddl">
                                    <asp:ListItem Text="Own" Value="O" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Party" Value="P"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Freight Paid:
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox runat="server" ID="txtVasuliRate" CssClass="txt" Width="100px" Style="text-align: right;"
                                    TabIndex="49" AutoPostBack="true" Height="24px" OnTextChanged="txtVasuliRate_TextChanged"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txtVasuliAmount" CssClass="txt" AutoPostBack="true"
                                    TabIndex="0" OnTextChanged="txtVasuliAmount_TextChanged" Height="24px" Width="100px"
                                    Style="text-align: right;"></asp:TextBox>&nbsp;&nbsp;&nbsp; Vasuli Rate:<asp:TextBox
                                        runat="server" ID="txtVasuliRate1" CssClass="txt" Width="80px" TabIndex="51"
                                        Style="text-align: right;" AutoPostBack="true" Height="24px" OnTextChanged="txtVasuliRate1_TextChanged"></asp:TextBox>&nbsp;
                                <asp:TextBox runat="server" ID="txtVasuliAmount1" CssClass="txt" Width="100px" Style="text-align: right;"
                                    TabIndex="0" AutoPostBack="true" OnTextChanged="txtVasuliAmount1_TextChanged"
                                    Height="24px"></asp:TextBox>&nbsp;&nbsp;<asp:Button runat="server" ID="btnVoucherOtherAmounts"
                                        Visible="false" Text="Other" CssClass="btnHelp" Width="70px" Height="24px" OnClick="btnVoucherOtherAmounts_Click" />&nbsp;&nbsp;
                                Vasuli A/c:
                                <asp:TextBox ID="txtVasuliAc" runat="Server" CssClass="txt" Width="80px" Style="text-align: right;"
                                    TabIndex="53" onkeydown="vasuli(event);" AutoPostBack="false" OnTextChanged="txtVasuliAc_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtVasuliAc" runat="server" Text="..." OnClick="btntxtVasuliAc_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lbltxtVasuliAc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">D.O.
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox ID="txtDO_CODE" runat="Server" CssClass="txt" Width="120px" Style="text-align: right;"
                                    TabIndex="0" onkeydown="docode(event);" AutoPostBack="false" OnTextChanged="txtDO_CODE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtDO_CODE" runat="server" Text="..." OnClick="btntxtDO_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="LBLDO_NAME" runat="server" CssClass="lblName">
                                </asp:Label>
                                Mill EWay Bill No:
                           
                                <asp:TextBox ID="txtMillEwayBill_No" runat="Server" CssClass="txt" Width="150px"
                                    TabIndex="0" Style="text-align: left;" Height="24px"></asp:TextBox>
                                <asp:TextBox ID="txtMillInvoiceno" runat="Server" CssClass="txt" Width="150px" Style="text-align: left;"
                                    TabIndex="0" Height="24px"></asp:TextBox>

                                mill Invoice Date:
                                  <asp:TextBox ID="txtMillInv_Date" runat="Server" CssClass="txt" Width="80px"
                                      TabIndex="0" MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                      Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtMillInv_Date_TextChanged"
                                      Height="24px"></asp:TextBox>
                                <asp:Image ID="imgtxtMillInv_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                    Height="15px" />
                                <ajax1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtMillInv_Date"
                                    PopupButtonID="imgtxtMillInv_Date" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>

                                <asp:CheckBox runat="server" ID="chkEWayBill" TabIndex="0" AutoPostBack="true" OnCheckedChanged="chkEWayBill_CheckedChanged" />
                                <asp:Label runat="server" ID="lblchkEWayBill" CssClass="lblName"></asp:Label>
                                My EWay Bill No: 
                                  <asp:TextBox ID="txtEWayBill_No" runat="Server" CssClass="txt" Width="150px" Style="text-align: left;"
                                      TabIndex="0" Height="24px"></asp:TextBox>
                                EWayBill ValidDate : 
                                  <asp:TextBox ID="txtEwayBill_ValidDate" runat="Server" CssClass="txt" Width="80px" Style="text-align: left;"
                                      Height="24px"></asp:TextBox>
                                <asp:Image ID="imgtxtEwayBill_ValidDate" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                    Height="15px" />
                                <ajax1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEwayBill_ValidDate"
                                    PopupButtonID="imgtxtEwayBill_ValidDate" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>


                            </td>

                        </tr>
                        <tr>
                            <td>Broker:
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox ID="txtBroker_CODE" runat="Server" CssClass="txt" Width="120px" Style="text-align: right;"
                                    TabIndex="0" onkeydown="broker(event);" AutoPostBack="false" OnTextChanged="txtBroker_CODE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtBroker_CODE" runat="server" Text="..." OnClick="btntxtBroker_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="LBLBROKER_NAME" runat="server" CssClass="lblName"></asp:Label>
                                Distance:
                                <asp:TextBox ID="txtDistance" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                    onkeydown="distance(event);" Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtDistance_TextChanged"></asp:TextBox>
                                Invoice Checked:<asp:CheckBox runat="server"
                                    TabIndex="0" ID="chkInv_Chk" Width="10px" Height="10px" />
                                Season
                                <asp:TextBox ID="txtseasons" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                    onkeydown="distance(event);" Style="text-align: left;" AutoPostBack="false" Height="24px"></asp:TextBox>
                                SB Other Amount
                                <asp:TextBox ID="txtSB_Other_Amount" runat="Server" CssClass="txt" TabIndex="0" Width="80px"  OnTextChanged="txtSB_Other_Amount_TextChanged"
                                     Style="text-align: left;" AutoPostBack="false" Height="24px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;"></td>
                            <td align="left" colspan="3" style="width: 10%;"></td>
                        </tr>
                        <tr>
                            <td align="left">UTR Narration:
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox ID="txtNARRATION1" runat="Server" CssClass="txt" Width="400px" Style="text-align: left;"
                                    TabIndex="0" onkeydown="narration1(event);" AutoPostBack="True" OnTextChanged="txtNARRATION1_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtNARRATION1" runat="server" Text="..." OnClick="btntxtNARRATION1_Click"
                                    CssClass="btnHelp" Heignht="24px" Width="20px" />&nbsp;<asp:CheckBox runat="server"
                                        ID="chkNoprintondo" Width="10px" Height="10px" />
                                B.P. Narration:
                           
                                <asp:TextBox ID="txtNARRATION2" runat="Server" CssClass="txt" Width="200px" Style="text-align: left;"
                                    TabIndex="0" onkeydown="narration2(event);" AutoPostBack="True" OnTextChanged="txtNARRATION2_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtNARRATION2" runat="server" Text="..." OnClick="btntxtNARRATION2_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />

                                DO Narration:
                           
                                <asp:TextBox ID="txtNARRATION3" runat="Server" CssClass="txt" Width="200px" Style="text-align: left;"
                                    TabIndex="0" onkeydown="narration3(event);" AutoPostBack="True" OnTextChanged="txtNARRATION3_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtNARRATION3" runat="server" Text="..." OnClick="btntxtNARRATION3_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />

                            </td>
                            </td>
                        </tr>
                        <tr>

                            <td align="left" style="width: 10%;">Fright Narration:
                            </td>
                            <td align="left" style="width: 70%;">
                                <asp:TextBox TextMode="MultiLine" runat="server" ID="txtNarration5" CssClass="txt"
                                    TabIndex="0" Width="280px" Height="50px" />
                                SB Narration:
                                  <asp:TextBox TextMode="MultiLine" runat="server" ID="txtsbnarration" CssClass="txt"
                                      TabIndex="0" Width="280px" Height="50px" />
                                <asp:LinkButton runat="server" ID="lnkMemo" Text="Memo No:" Style="color: Black; text-decoration: none;"
                                    ToolTip="Click to Go On Motor Memo" OnClick="lnkMemo_Click"></asp:LinkButton>
                                &nbsp;<asp:Label ID="lblMemoNo" runat="server" CssClass="lblName"></asp:Label>&nbsp;
                                <asp:LinkButton runat="server" ID="lnkVoucOrPurchase" Text="Number:" Style="color: Black; text-decoration: none;"
                                    ToolTip="Click to Go On Respective Page" OnClick="lnkVoucOrPurchase_Click"></asp:LinkButton>&nbsp;<asp:Label
                                        ID="lblVoucherNo" runat="server" CssClass="lblName"></asp:Label>&nbsp;&nbsp;<asp:Label
                                            ID="lblVoucherType" runat="server" CssClass="lblName"></asp:Label>
                                &nbsp;&nbsp;<asp:LinkButton runat="server" ID="lblsbnol" Text="" Style="color: Black; text-decoration: none;"
                                    ToolTip="Click to Go On Sale Bill" OnClick="lblsbnol_Click"></asp:LinkButton>
                                &nbsp;<asp:Label ID="lblSB_No" runat="server" CssClass="lblName"></asp:Label>&nbsp;&nbsp;&nbsp;
                                Loading Sms Sent:
                                <asp:Label Text="" ID="lblLoadingSms" Font-Bold="true" ForeColor="Yellow" runat="server" />
                                 &nbsp;&nbsp;
                                Email Sent:
                                <asp:Label Text="" ID="lblEmail" Font-Bold="true" ForeColor="Yellow" runat="server" />
                                 <asp:Label Text="" ID="lblDNCNNo" Font-Bold="true" ForeColor="Yellow" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnEditDetails" runat="server" Text="EDIT" CssClass="btnHelp" Width="50px"
                                    Height="25px" OnClick="btnEditDetails_Click" TabIndex="70" />
                                <asp:Button ID="btnOpenDetailsPopup" runat="server" Text="ADD" CssClass="btnHelp"
                                    TabIndex="69" Width="50px" Height="24px" OnClick="btnOpenDetailsPopup_Click" />

                            </td>
                            <td colspan="4" align="left">
                                <asp:Button ID="btnCustomizeDO" runat="server" Text="Customize DO" CssClass="btnHelp" Width="70px"
                                    Height="24px" OnClick="btnCustomizeDO_Click" OnClientClick="Confirm13()" Visible="true" />

                                <asp:Button ID="btnOurDO" runat="server" Text="Our DO" CssClass="btnHelp" Width="70px"
                                    Height="24px" OnClick="btnOurDO_Click" OnClientClick="Confirm12()" />

                                <asp:Button ID="btnDO" runat="server" Text="DO" CssClass="btnHelp" Width="70px"
                                    Height="24px" OnClick="btnDO_Click" />

                                <asp:Button ID="btnpreprintourdo" runat="server" Text="Our DO preprint " CssClass="btnHelp" Width="120px"
                                    Height="24px" OnClick="btnpreprintourdo_Click" OnClientClick="Confirm123()" />

                                <asp:Button ID="btnMail" runat="server" Text="Party DO" CssClass="btnHelp" Width="70px"
                                    ValidationGroup="save" OnClick="btnMail_Click" Height="24px" />


                                <asp:Button ID="btnPartyBillDO" runat="server" Text="Party Bill DO" CssClass="btnHelp" Width="90px"
                                    Height="24px" OnClick="btnPartyBillDO_Click" OnClientClick="Confirm12()" />

                                <asp:Button ID="btnpendingsale" runat="server" Text="Pending SB" CssClass="btnHelp" Width="90px"
                                    Height="24px" OnClick="btnpendingsale_Click" />
                                <asp:Button ID="btngenratesalebill" runat="server" Text="Genearate SB" CssClass="btnHelp" Width="90px"
                                    Height="24px" OnClick="btngenratesalebill_Click" OnClientClick="this.disabled='true';"  UseSubmitBehavior="false"/>
                                <asp:Button runat="server" ID="btnGentare_EWayBill" Text="Gentare EWayBill" CssClass="btnHelp"
                                    Width="90px" Height="24px" OnClientClick="GEway();" />
                                <asp:Button runat="server" ID="btnGenEinvoice" Text="Generate EInvoice" CssClass="btnHelp"
                                    Width="120px" Height="24px" OnClientClick="EInovice();" />
                                <asp:Button runat="server" ID="btnGenEInvoiceEwaybill" Text="Gen EInvoice & EwayBill" CssClass="btnHelp"
                                    Width="160px" Height="24px" OnClientClick="EInoviceEwayBill();" />
                                <asp:Button runat="server" ID="btnCancleEinvoice" Text="Cancle EInvoice" CssClass="btnHelp"
                                    Width="120px" Height="24px" OnClientClick="ConfirmCancle();" OnClick="btnCancleEInvoice_Click" />
                                <asp:Button runat="server" ID="btnPrintSaleBill" Text="SB Print" CssClass="btnHelp"
                                    OnClick="btnPrintSaleBill_Click" Width="70px" Height="24px" OnClientClick="Confirm1()" />
                               
                                 <asp:Button runat="server" ID="btnPrintSaleBillfright" Text="COR SB" CssClass="btnHelp"
                                    OnClick="btnPrintSaleBillfright_Click" Width="70px" Height="24px" OnClientClick="Confirm1()" />

                                <asp:Button runat="server" ID="btnpreprintSB" Text="SB PrePrint " CssClass="btnHelp"
                                    OnClick="btnpreprintSB_Click" Width="90px" Height="24px" OnClientClick="Confirm1234()" />

                                <asp:Button ID="btnperforminvoice" runat="server" Text="Porforma Invoice" CssClass="btnHelp" Width="120px"
                                    ValidationGroup="save" OnClick="btnperforminvoice_Click" Height="24px" />

                                <asp:Button ID="btnCustomizeSaleBill" runat="server" Text="Customize SaleBill" CssClass="btnHelp" Width="120px"
                                    ValidationGroup="save" OnClick="btnCustomizeSaleBill_Click" Height="24px" />

                                <input type="button" id="btnOpenSendsmspoup" onclick="showsmspopup();" runat="server"
                                    value="SMS Screen" class="btnHelp" style="width: 80px; height: 24px;" />
                                <asp:Button ID="btnUtrShortCut" runat="server" Text="Make UTR" CssClass="btnHelp"
                                    Width="70px" Height="24px" OnClientClick="UTRPrint()" />
                                <asp:Button runat="server" ID="btnPrintMotorMemo" Text="Motor Memo" CssClass="btnHelp"
                                    OnClick="btnPrintMotorMemo_Click" Width="80px" Height="24px" />

                                <asp:Button ID="btnDeliveryChallan" runat="server" Text="Delivery Challan" CssClass="btnHelp"
                                    Height="24px" OnClick="btnDeliveryChallan_Click" />


                                <asp:Button ID="btnWayBill" runat="server" Text="Way Bill" CssClass="btnHelp" Width="70px"
                                    ValidationGroup="save" OnClientClick="return WB();" Height="24px" />


                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div style="width: 100%; position: relative; top: 0px; left: 0px;">
                <table width="100%" style="vertical-align: top;" align="left" cellspacing="2">
                    <tr>
                        <td rowspan="6" style="vertical-align: top;" align="left">
                            <asp:UpdatePanel ID="upGrid" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlgrdDetail" runat="server" ScrollBars="Both" Height="150px" Width="1000px"
                                        BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true" Font-Names="Verdana"
                                        Font-Size="11px" BackColor="SeaShell" Style="margin-left: 30px; float: left;">
                                        <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" HeaderStyle-BackColor="#397CBB"
                                            HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="100%"
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>


                </table>
            </div>
            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="90%"
                ScrollBars="Both" align="center" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: left; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 5%; top: 10%;">
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
                        <td style="width: 100%;" align="left" colspan="2">
                            <table id="Table1" runat="server" width="100%">
                                <tr>
                                    <td style="width: 40%;">Search Text:
                                        <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                                            Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                                        &nbsp;<asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server"
                                            Text="Search" CssClass="btnSubmit" OnClick="btnSearch_Click" />
                                    </td>
                                    <td align="left" runat="server" id="tdDate" visible="false">From:
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                                            MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                            Height="15px" />
                                        <ajax1:CalendarExtender ID="calendertxtFromDate" runat="server" TargetControlID="txtFromDate"
                                            PopupButtonID="Image1" Format="dd/MM/yyyy">
                                        </ajax1:CalendarExtender>
                                        To:
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                                            MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                            Height="15px" />
                                        <ajax1:CalendarExtender ID="CalendarExtendertxtToDate" runat="server" TargetControlID="txtToDate"
                                            PopupButtonID="Image2" Format="dd/MM/yyyy">
                                        </ajax1:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 500px">
                                <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    AllowPaging="true" PageSize="15" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                    OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
                                    <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                                    <RowStyle Height="25px" ForeColor="Black" Wrap="false" />
                                    <PagerStyle BackColor="Tomato" ForeColor="White" Width="100%" Font-Bold="true" />
                                    <PagerSettings Position="TopAndBottom" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                        <td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:GridView ID="grdGstAutoId" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                AllowPaging="true" PageSize="15" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                HeaderStyle-ForeColor="White" Style="table-layout: fixed;">
                <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                <RowStyle Height="25px" ForeColor="Black" Wrap="false" />
                <PagerStyle BackColor="Tomato" ForeColor="White" Width="100%" Font-Bold="true" />
                <PagerSettings Position="TopAndBottom" />
            </asp:GridView>
            </td>
            <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="GhostWhite" Width="800px"
                BorderColor="Teal" BorderWidth="1px" Height="320px" BorderStyle="Solid" Style="z-index: 4999; left: 15%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
                <table width="80%" align="center" cellspacing="5px">
                    <tr>
                        <td colspan="2" align="center" style="background-color: lightslategrey; color: White;">
                            <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Text="D.O. Details"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>ID&nbsp;<asp:Label ID="lblID" runat="server"></asp:Label>
                        </td>
                        <td>NO&nbsp;<asp:Label ID="lblNo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Type:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:DropDownList ID="drpddType" runat="server" CssClass="ddl" TabIndex="71" Width="200px"
                                Height="30px">
                                <asp:ListItem Text="transfer Letter" Value="T"></asp:ListItem>
                                <asp:ListItem Text="Demand Draft" Value="D"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">BANK CODE:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtBANK_CODE" runat="Server" CssClass="txt" TabIndex="72" Width="60px"
                                onkeydown="bankcode(event);" Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtBANK_CODE_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtBANK_CODE" runat="server" Height="24px" Width="20px" Text="..."
                                OnClick="btntxtBANK_CODE_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblBank_name" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">UTR No:
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtUTRNo" CssClass="txt" Width="80px" runat="server" AutoPostBack="true"
                                onkeydown="UTR(event);" OnTextChanged="txtUTRNo_TextChanged" TabIndex="73" Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtUTRNo" runat="server" CssClass="btnHelp" Text="UTR No" Width="80px"
                                TabIndex="66" OnClick="btntxtUTRNo_Click" Height="24px" />
                            <asp:Label ID="lblUTRYearCode" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblUTRCompnyCode" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">DD/CHQ/RTGS No:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtNARRATION" runat="Server" CssClass="txt" TabIndex="74" Height="24px"
                                Width="200px" Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtNARRATION_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtNARRATION" runat="server" Text="..." Height="24px" Width="20px"
                                OnClick="btntxtNARRATION_Click" CssClass="btnHelp" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Amount:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtBANK_AMOUNT" runat="Server" CssClass="txt" TabIndex="75" Height="24px"
                                Width="200px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtBANK_AMOUNT_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteretxtBANK_AMOUNT" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtBANK_AMOUNT">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">With GST Amount per Quantal:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtwithGst_Amount" runat="Server" CssClass="txt" TabIndex="76" Height="24px"
                                Width="200px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtwithGst_Amount_TextChanged"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">LT No:
                        </td>
                        <td>
                            <asp:TextBox ID="txtLT_No" runat="Server" CssClass="txt" TabIndex="77" Height="24px"
                                Width="200px" Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtLT_No_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Label runat="server" ID="lblUtrBalnceError" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnSubmit" Width="80px"
                                Height="25px" OnClick="btnAdddetails_Click" TabIndex="78" />

                            <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnSubmit"
                                Width="80px" Height="25px" OnClick="btnClosedetails_Click" TabIndex="79" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlVoucherEntries" runat="server" Width="500px" align="center" ScrollBars="None"
                Direction="LeftToRight" Style="z-index: 5000; position: absolute; display: none; background-color: White; float: right; max-height: 300px; min-height: 300px; background-position: center; left: 50%; margin-left: -400px; top: 30%;"
                Height="400px"
                BorderStyle="Groove" BorderColor="Blue" BorderWidth="2px">
                <table width="80%" align="center" cellspacing="5">
                    <tr>
                        <td align="center" colspan="2" style="border: 1px solid blue;">
                            <asp:Label runat="server" ID="lblKa" Text="Amounts For Voucher" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <b>Brokrage:</b>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtVoucherBrokrage" CssClass="txt" Width="120px"
                                Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtVoucherBrokrage_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="filtertxtBrokrage" TargetControlID="txtVoucherBrokrage"
                                FilterType="Numbers,Custom" runat="server" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <b>Service Charge:</b>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtVoucherServiceCharge" CssClass="txt" Width="120px"
                                Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtVoucherServiceCharge_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtVoucherServiceCharge"
                                FilterType="Numbers,Custom" runat="server" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <b>Rate Diff:</b>
                            <asp:TextBox ID="txtVoucherL_Rate_Diff" runat="Server" CssClass="txt" Width="80px"
                                Style="text-align: right;" AutoPostBack="True" Height="24px" OnTextChanged="txtVoucherL_Rate_Diff_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="filtertxtL_Rate_Diff" TargetControlID="txtVoucherL_Rate_Diff"
                                FilterType="Numbers,Custom" runat="server" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            <b>Amount:</b>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtVoucherRATEDIFFAmt" runat="Server" CssClass="txt" Width="120px"
                                Style="text-align: right;" AutoPostBack="True" ReadOnly="true" Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="filtertxtRATEDIFF" TargetControlID="txtVoucherRATEDIFFAmt"
                                FilterType="Numbers,Custom" runat="server" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <b>Bank Comm:</b>
                            <asp:TextBox ID="txtVoucherCommission_Rate" runat="Server" CssClass="txt" Width="80px"
                                Style="text-align: right;" AutoPostBack="True" Height="24px" OnTextChanged="txtVoucherCommission_Rate_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="filtertxtCommission_Rate" TargetControlID="txtVoucherCommission_Rate"
                                FilterType="Numbers,Custom" runat="server" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            <b>Amount:</b>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtVoucherBANK_COMMISSIONAmt" runat="Server" CssClass="txt" Width="120px"
                                ReadOnly="true" Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="filtertxtBANK_COMMISSION" TargetControlID="txtVoucherBANK_COMMISSIONAmt"
                                FilterType="Numbers,Custom" runat="server" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <b>Interest:</b>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtVoucherInterest" runat="Server" CssClass="txt" Width="120px"
                                Style="text-align: right;" AutoPostBack="True" Height="24px" OnTextChanged="txtVoucherInterest_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="filtertxtInterest" TargetControlID="txtVoucherInterest"
                                FilterType="Numbers,Custom" runat="server" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <b>Transport:</b>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtVoucherTransport_Amount" runat="Server" CssClass="txt" Width="120px"
                                Style="text-align: right;" AutoPostBack="True" Height="24px" OnTextChanged="txtVoucherTransport_Amount_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="filtertxtTransport_Amount" TargetControlID="txtVoucherTransport_Amount"
                                FilterType="Numbers,Custom" runat="server" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <b>Other Expenses:</b>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtVoucherOTHER_Expenses" runat="Server" CssClass="txt" Width="120px"
                                Style="text-align: right;" AutoPostBack="True" Height="24px" OnTextChanged="txtVoucherOTHER_Expenses_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="filtertxtOTHER_Expenses" TargetControlID="txtVoucherOTHER_Expenses"
                                FilterType="Numbers,Custom" runat="server" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button runat="server" ID="btnOk" Text="OK" CssClass="btnHelp" Width="60px" Height="24px"
                                OnClick="btnOk_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlSendSMS" runat="server" Width="60%" align="center" ScrollBars="Vertical"
                BackColor="White" Direction="LeftToRight" Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
                <table cellpadding="5" cellspacing="5" style="width: 100%; border: 1px solid black;"
                    class="smstable">
                    <thead style="background-color: Blue; color: White; font-weight: bold; height: 25px;">
                        <tr>
                            <th align="left">Message
                            </th>
                            <th align="left">To
                            </th>
                            <th align="left">Mobile
                            </th>
                            <th align="center">Send
                            </th>
                        </tr>
                    </thead>
                    <tbody id="table-body-id">
                        <tr>
                            <td style="width: 300px;">
                                <asp:Label ID="partysms" runat="server" CssClass="sms"></asp:Label>
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="partyname" runat="server"></asp:Label>
                            </td>
                            <td style="width: 120px;">
                                <input type="text" id="partymobile" class="textbox" />
                            </td>
                            <td style="width: 30px;" id="partyyesno" class="sendyesno">Y
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 300px;">
                                <asp:Label ID="millsms" runat="server" CssClass="sms" />
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="millname" runat="server" />
                            </td>
                            <td style="width: 120px;">
                                <input type="text" id="millmobile" class="textbox" />
                            </td>
                            <td style="width: 30px;" class="sendyesno">N
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 300px;">
                                <asp:Label ID="driversms" runat="server" CssClass="sms" />
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="transportname" runat="server" />
                            </td>
                            <td style="width: 120px;">
                                <input type="text" id="txtdriverno" class="textbox" />
                            </td>
                            <td style="width: 30px;" class="sendyesno">Y
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 300px;">
                                <asp:Label ID="transportsms" runat="server" CssClass="sms" />
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="party" runat="server" />
                            </td>
                            <td style="width: 120px;">
                                <input type="text" id="txtpartymobile" class="textbox" />
                            </td>
                            <td style="width: 30px;" class="sendyesno">N
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="2" align="center">
                                <input type="button" id="btnInvoke" value="SEND" class="btn btnHelp btn-primary"
                                    onclick="return send();" />
                                <asp:Button Text="CLOSE" CssClass="btn btnHelp btn-primary" runat="server" ID="btnClosePopup"
                                    OnClick="btnClosePopup_Click" />
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {

            //            $('#<%=btnOpenDetailsPopup.ClientID %>').focusout(function () {
            //                debugger;
            //                $('#ContentPlaceHolder1_grdDetail_lnkEdit_0').focus();
            //            });

            $('body').on("dblclick", "#table-body-id tr", function () {

                var rowindex = $(this).index();
                var cell = $(this).closest('tr').children('td.sendyesno').text().trim();
                if (cell == "Y") {
                    $(this).closest('tr').children('td.sendyesno').text("N");
                }
                else {
                    $(this).closest('tr').children('td.sendyesno').text("Y");
                }
            });
        });

        function showsmspopup() {

            var dono = $('#<%=txtdoc_no.ClientID %>').val();
            var company_code = '<%= Session["Company_Code"] %>';
            var year_code = '<%= Session["year"] %>';
            var param = 'Doc_No=' + dono + '&Company_Code=' + company_code + '&Year_Code=' + year_code;
            $.ajax({
                type: 'GET',
                url: '../Handlers/dodetails.ashx',
                data: param,
                success: function (data) {

                    if (

                        data != null) {
                        var obj = JSON.parse(data);
                        var dono = obj[0].doc_no;
                        var qntl = obj[0].quantal;
                        var millshort = obj[0].millshortname;
                        var date = obj[0].doc_dateConverted;
                        var grade = obj[0].grade;
                        var lorry = obj[0].truck_no;
                        var voucherbyname = obj[0].voucherbyname;
                        var voucherbyshort = obj[0].voucherbyname;
                        var voucherbymobile = obj[0].voucherbymobno;
                        var voucherbyaddress = obj[0].vouvherbyaddress;
                        var voucherbycity = obj[0].voucherbycityname;
                        var voucherbystate = obj[0].voucherbycitystate;


                        var ShippetdToLICNO = obj[0].ShippetdToLICNO;
                        var shippedTo_Address = obj[0].shiptoaddress;
                        var ShippetdToTin_no = obj[0].ShippetdToTin_no;
                        var ShippetdToFSSAI = obj[0].shiptofssai;
                        var ShippetdToCompanyPan = obj[0].shiptopanno;
                        var ShippetdToECC = obj[0].shiptoeccno;
                        var ShippetdToCST_No = obj[0].ShippetdToCST_No;
                        var shippedTo_City_Name = obj[0].shiptocityname;
                        var ShippedtTo_City_State = obj[0].shiptocitystate;
                        var shippedTo_Mobileno = obj[0].shiptomobno;
                        var Shipped_ToName = obj[0].shiptoname;
                        var ShippedTo_GSt = obj[0].shiptogstno;



                        var getpassshort = obj[0].getpassname;
                        var getpasscity = obj[0].getpasscityname;
                        var getpassstate = obj[0].getpasscitystate;
                        var getpassmobile = obj[0].getpassmobno;
                        var millrate = obj[0].mill_rate;
                        var millmobile = obj[0].millmobno;
                        var drivermobile = obj[0].driver_no;
                        var frtperqtl = obj[0].freight;
                        var vasulirate = obj[0].vasuli_rate;
                        var frtplusvasuli = frtperqtl + vasulirate;


                        var getpassname = obj[0].getpassname;
                        var millname = obj[0].millname;
                        var transportname = obj[0].transportname;
                        var transportmobile = obj[0].transportmobno;
                        var getpasssln = obj[0].getpasssln;
                        var getpasstin = obj[0].getpasstin;
                        var getpasscst = obj[0].getpasscstno;
                        var getpassgst = obj[0].getpassgstno;
                        var getpassecc = obj[0].getpassecc;
                        var getpasspan = obj[0].getpasspanno;
                        var getpassfssai = obj[0].getpassfssai;


                        var voucherbysln = obj[0].voucherbysln;
                        var voucherbytin = obj[0].voucherbytin;
                        var voucherbycst = obj[0].voucherbycstno;
                        var voucherbygst = obj[0].voucherbygstno;
                        var voucherbyecc = obj[0].voucherbyecc;
                        var voucherbypan = obj[0].voucherbypan;
                        var voucherbyfssai = obj[0].voucherbyfssai;

                        var voucherbynumber = obj[0].voucherbymobno;
                        var voucher_no = obj[0].voucher_no;
                        var voucher_type = obj[0].voucher_type;
                        var memo_no = obj[0].memo_no;
                        var SB_No = obj[0].SB_No;

                        var brokermobile = obj[0].brokermobno;
                        var salebilltoMobile = obj[0].salebillmobno;
                        var sms1mobile = brokermobile;
                        if (brokermobile == '') {
                            sms1mobile = salebilltoMobile;
                            if (salebilltoMobile == '') {
                                sms1mobile = getpassmobile;
                            }
                        }

                        var vtype = "";
                        if (voucher_no != "0" && voucher_no != "") {
                            if (voucher_type == "PS") {
                                //vtype = "Sale Bill No: " + SB_No;
                            }
                            else if (voucher_type == "OV") {
                                vtype = "Voucher No: " + voucher_no;
                            }
                            else if (voucher_type == "LV") {
                                vtype = "Debit Note No: " + voucher_no;
                            }
                        }

                        var memonumber = "";
                        if (memo_no != "0") {
                            memonumber = "Motor Memo: " + memo_no;
                        }

                        var transordrivermobile = "";
                        if (drivermobile != "") {
                            transordrivermobile = " Driver Mob:" + drivermobile;
                        }
                        else if (transportmobile != "") {
                            transordrivermobile = " Transport Mob:" + transportmobile;
                        }

                        var getpassnumbers = getpasssln + ' ' + getpasstin + ' ' + getpasscst + ' ' + getpassgst + ' ' + getpassecc
                         + ' ' + getpasspan + ' ' + getpassfssai;
                        var getpassonlyonenumber = '';
                        var vocuherbyonemobile = '';


                        //var getpassonlyonenumber = getpassmobile.split(',')[0];
                        //var vocuherbyonemobile = voucherbymobile.split(',')[0];

                        var voucherbynumbers = voucherbysln + ' ' + voucherbytin + ' ' + voucherbycst + ' ' + voucherbygst + ' ' + voucherbyecc
                        + ' ' + voucherbypan + ' ' + voucherbyfssai;


                        var vocuherbydetails = " Shipped To: " + voucherbyname + " Address:" + voucherbyaddress + " City:" + voucherbycity + " State:"
                        + voucherbystate + " " + voucherbynumbers;


                        var Shippedbynumbers = ShippetdToLICNO + ' ' + ShippetdToTin_no + ' ' + ShippetdToCST_No + ' ' + ShippedTo_GSt + ' ' + ShippetdToECC
                        + ' ' + ShippetdToCompanyPan + ' ' + ShippetdToFSSAI;


                        var Shippeddbydetails = " Shipped To: " + Shipped_ToName + " Address:" + shippedTo_Address + " City:" + shippedTo_City_Name + " State:"
                        + ShippedtTo_City_State + " " + Shippedbynumbers;



                        var vocuherbydetailsfortansport = " Address:" + voucherbyaddress;

                        var Shippedbydetailsfortansport = " Address:" + shippedTo_Address;

                        //.......................................................

                        //                        var partysms = "Truck is send for loading.DO." + dono + " " + millshort + " Qntl:" + qntl + " Date:" + date + " " + grade + " "
                        //                         + lorry + " " + voucherbyshort + " Sale Bill No:" + SB_No;

                        var partysms = "Truck is send for loading.DO." + dono + " " + millshort + " Qntl:" + qntl + " Date:" + date + " " + grade + " "
                         + lorry + " " + Shipped_ToName + " Sale Bill No:" + SB_No;
                        //.......................................................
                        //                        var millsms = "DO." + dono + " Getpass:" + getpassname + " " + getpasscity + " " + getpassstate + " " + getpassnumbers
                        //                        + vocuherbydetails + ' ' + " Qntl:" + qntl + " Date:" + date + " M.R." + millrate + " " + grade + " " + lorry;


                        //                        var millsms = "DO." + dono + " Getpass:" + getpassname + " " + getpasscity + " " + getpassstate + " " + getpassnumbers
                        //                        + vocuherbydetails + " Qntl:" + qntl + " Date:" + date + " M.R." + millrate + " " + grade + " " + lorry;


                        var millsms = "DO." + dono + " Getpass:" + getpassname + " " + getpasscity + " " + getpassstate + " " + getpassnumbers
                        + Shippeddbydetails + " Qntl:" + qntl + " Date:" + date + " M.R." + millrate + " " + grade + " " + lorry;

                        //...........................................................

                        //                        var driversms = "DO." + dono + " " + millshort + " " + voucherbyname + " " + voucherbycity + " " + voucherbystate + " " + vocuherbydetailsfortansport
                        //                         + " " + voucherbytin + " " + voucherbygst + " " + salebilltoMobile + "  Qntl:" + qntl + " date:" + date + ' Freight:' + frtperqtl + " Lorry:" + lorry;




                        var driversms = "DO." + dono + " " + millshort + " " + Shipped_ToName + " " + shippedTo_City_Name + " " + ShippedtTo_City_State
                        + " " + Shippedbydetailsfortansport
                         + " " + ShippetdToTin_no + " " + ShippedTo_GSt + " " + salebilltoMobile + "  Qntl:" + qntl
                         + " date:" + date + ' Freight:' + frtperqtl + " Lorry:" + lorry;




                        //--------------------------------------------------------------------------------


                        //                        var transportsms = "DO." + dono + " The truck is confirm load dt." + date + " " + millshort + " Getpass:" + getpassname + " Shipped To:"
                        //                         + voucherbyshort + " qntl:" + qntl + " " + grade + " " + lorry + ' Freight:' + frtplusvasuli + transordrivermobile + " Sale Bill No:"
                        //                         + SB_No + " " + vtype;


                        var transportsms = "DO." + dono + " The truck is confirm load dt." + date + " " + millshort + " Getpass:" + getpassname + " Shipped To:"
                         + Shipped_ToName + " qntl:" + qntl + " " + grade + " " + lorry + ' Freight:' + frtplusvasuli + transordrivermobile + " Sale Bill No:"
                         + SB_No + " " + vtype;


                        //party
                        $('#<%=partysms.ClientID %>').html(partysms);
                        $('#<%=partyname.ClientID %>').html(voucherbyname);
                        $('#partymobile').val(sms1mobile);

                        //mill
                        $('#<%=millsms.ClientID %>').html(millsms);
                        $('#<%=millname.ClientID %>').html(millname);
                        $('#millmobile').val(millmobile);

                        //driver
                        $('#<%=driversms.ClientID %>').html(driversms);
                        $('#<%=transportname.ClientID %>').html(transportname);
                        $('#txtdriverno').val(transportmobile);

                        //transportsms 
                        $('#<%=transportsms.ClientID %>').html(transportsms);
                        $('#<%=party.ClientID %>').html(voucherbyname);
                        $('#txtpartymobile').val(sms1mobile);
                    }


                    document.getElementById('<%=pnlSendSMS.ClientID %>').style.display = "block";
                    document.getElementById('btnInvoke').focus();
                }
            });
        }

        function send() {
            debugger;
            var partymsg = $('#<%=partysms.ClientID %>').html();
            var partymobile = $('#partymobile').val();

            var msgAPI = '<%= Session["smsApi"] %>';
            var senderid = '<%= Session["Sender_id"] %>';
            var accusage = '<%= Session["Accusage"] %>';
            $('#table-body-id tr').each(function () {
                debugger;
                var a = $(this).closest('tr').children('td.sendyesno').text().trim();
                if (a == "Y") {
                    debugger;
                    var mobile = $(this).closest('tr').find('.textbox').val();
                    var msg = $(this).closest('tr').find('.sms').html();
                    if (mobile != "") {
                        $.ajax({
                            type: "POST",
                            url: "../sendsms.asmx/SendSMS",
                            data: "{'msg':'" + msg + "','mobile':'" + mobile + "','msgAPI':'" + msgAPI + "','senderid':'" + senderid + "','accusage':'" + accusage + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                if (data == 1) {

                                }
                            }
                        });
                    }
                }
            });

            if (confirm("back to DO->OK, close->cancel")) {

                document.getElementById('<%=pnlSendSMS.ClientID %>').style.display = "none";
                document.getElementById('<%=btnAdd.ClientID %>').focus();
            }
        }
    </script>
</asp:Content>
