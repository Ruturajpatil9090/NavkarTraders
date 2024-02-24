<%@ Page Title="Reciept Payment Xml" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeReceiptPaymentxml.aspx.cs" Inherits="Sugar_pgeReceiptPaymentxml" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Script/jquery-1.4.2.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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

    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>



    <script type="text/javascript">

        //window.onbeforeunload = function (event) {
        //    var message = 'All changes will get lost!';
        //    if (typeof event == 'undefined') {
        //        event = window.event;
        //    }
        //    if (event) {
        //        event.returnValue = message;
        //    }
        //    return message;
        //}
    </script>
    <script type="text/javascript">
        function SB() {
            var billno = document.getElementById('<%=txtdoc_no.ClientID %>').value;
            var TranType = document.getElementById('<%=drpTrnType.ClientID %>').value;
            window.open('../Report/rptReceiptPayment.aspx?Doc_no=' + billno + '&TranType=' + TranType)
        }</script>
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
        function Back() {
            window.open('../Transaction/PgeReceiptPaymentUtility.aspx', '_self');
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

                if (hdnfClosePopupValue == "txtVoucherNo") {
                    debugger;
                    document.getElementById("<%=txtVoucherNo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=hdnftypenew.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[2].innerText;

                    document.getElementById("<%=txtvoucherType.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[9].innerText;
                    document.getElementById("<%=hdnflblvoucher.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[10].innerText;
                    //  document.getElementById("<%=txtamount.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[8].innerText;
                    document.getElementById("<%=txtadAmount.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[7].innerText;

                    var drp = document.getElementById('<%=drpFilter.ClientID %>');
                    var val = drp.options[drp.selectedIndex].value;
                    document.getElementById("<%=txtamount.ClientID %>").focus();
                    //                    if (val == "T") {
                    //                        document.getElementById("<%= hdnfTransportBalance.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[8].innerText;
                    //                        document.getElementById("<%=txtVoucherNo.ClientID %>").focus();
                    //                    }
                    //                    if (val == "S" || val == "V") {
                    //                        document.getElementById("<%= hdnfTransportBalance.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[11].innerText;
                    //                        document.getElementById("<%=txtVoucherNo.ClientID %>").focus();
                    //                    }

                }
                if (hdnfClosePopupValue == "txtVoucherNo1") {
                    debugger;
                    document.getElementById("<%=txtVoucherNo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtvoucherType.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[9].innerText;
                    document.getElementById("<%=hdnflblvoucher.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[10].innerText;
                    //   document.getElementById("<%=txtamount.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[8].innerText;
                    document.getElementById("<%=txtadAmount.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[7].innerText;

                    var drp = document.getElementById('<%=drpFilter.ClientID %>');
                    var val = drp.options[drp.selectedIndex].value;
                    document.getElementById("<%=txtamount.ClientID %>").focus();
                    //                    if (val == "T") {
                    //                        document.getElementById("<%= hdnfTransportBalance.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[8].innerText;
                    //                        document.getElementById("<%=txtVoucherNo.ClientID %>").focus();
                    //                    }
                    //                    if (val == "S" || val == "V") {
                    //                        document.getElementById("<%= hdnfTransportBalance.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[11].innerText;
                    //                        document.getElementById("<%=txtVoucherNo.ClientID %>").focus();
                    //                    }

                }
                if (hdnfClosePopupValue == "txtCashBank") {
                    document.getElementById("<%=txtCashBank.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblCashBank.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtCashBank.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtACCode") {
                    document.getElementById("<%=txtACCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblACName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtACCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtacadjAccode") {
                    document.getElementById("<%=txtacadjAccode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblacadjname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtacadjAccode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtUnit_Code") {
                    document.getElementById("<%=txtUnit_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblUnitName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtUnit_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtnarration") {
                    document.getElementById("<%=txtnarration.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtNarration2.ClientID %>").focus();
                    //document.getElementById("<%=btnAdddetails.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtdoc_no") {
                    //document.getElementById("<%=txtdoc_no.ClientID %>").disabled = false;
                    document.getElementById("<%=txtdoc_no.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtdoc_date.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = "";
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
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
                if (hdnfClosePopupValue == "txtACCode") {
                    document.getElementById("<%=txtACCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtUnit_Code") {
                    document.getElementById("<%=txtUnit_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtCashBank") {
                    document.getElementById("<%=txtCashBank.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtVoucherNo") {
                    document.getElementById("<%=txtVoucherNo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtVoucherNo1") {
                    document.getElementById("<%=txtVoucherNo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtnarration") {
                    document.getElementById("<%=txtnarration.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtacadjAccode") {
                    document.getElementById("<%=txtacadjAccode.ClientID %>").focus();
                }
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
                document.getElementById("<%= txtSearchText.ClientID %>").value = "";

            }

        });
    </script>
    <script type="text/javascript">
        function cashbank(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtCashBank.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtCashBank.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtCashBank.ClientID %>").val(unit);
                __doPostBack("txtCashBank", "TextChanged");

            }
        }
        function accode(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtACCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtACCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtACCode.ClientID %>").val(unit);
                __doPostBack("txtACCode", "TextChanged");

            }
            if (e.keyCode == 13) {
                e.preventDefault();
                document.getElementById("<%=btnSave.ClientID %>").focus();

            }
        }
        function acadjaccode(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtacadjAccode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtacadjAccode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtacadjAccode.ClientID %>").val(unit);
                __doPostBack("txtacadjAccode", "TextChanged");

            }

        }

        function unit(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtUnitcode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtUnit_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtUnit_Code.ClientID %>").val(unit);
                __doPostBack("txtUnit_Code", "TextChanged");

            }
        }
        function voucherno(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtVoucherNo.ClientID %>").click();

            }
            if (e.keyCode == 113) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnsoudaall.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtVoucherNo.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtVoucherNo.ClientID %>").val(unit);
                __doPostBack("txtVoucherNo", "TextChanged");

            }
        }
        function vouchertype(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnsoudaall.ClientID %>").click();

            }
            //  if (e.keyCode == 9) {
            // e.preventDefault();
            // var unit = $("#<%=txtvoucherType.ClientID %>").val();

            // unit = "0" + unit;
            // $("#<%=txtvoucherType.ClientID %>").val(unit);
            //__doPostBack("txtvoucherType", "TextChanged");

            // }
        }
        function narration(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtnarration.ClientID %>").click();

            }
        }
        function navigate() {
            debugger;
            var unit = $("#<%=txtTReceipt.ClientID %>").val();
            window.URL = unit;
        }
    </script>
    

    <script type="text/javascript">
        function Validate() {
            debugger;
            $("#loader").show();
            //validation

            var StartDate = '<%= Session["Start_Date"] %>';
            var EndDate = '<%= Session["End_Date"] %>';

            var d = $("#<%=txtdoc_date.ClientID %>").val();
            var DocDates = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

            StartDate = StartDate.slice(6, 11) + "/" + StartDate.slice(3, 5) + "/" + StartDate.slice(0, 2);
            EndDate = EndDate.slice(6, 11) + "/" + EndDate.slice(3, 5) + "/" + EndDate.slice(0, 2);

            if (DocDates >= StartDate && DocDates <= EndDate) {
            }
            else {
                $("#<%=txtdoc_date.ClientID %>").focus();
                alert('Not A Valid Date Range')
                $("#loader").hide();
                return false;
            }

            var transaction_type = $("#<%=drpTrnType.ClientID %>").val();
            if (transaction_type == "BP" || transaction_type == "BR") {
                if ($("#<%=txtCashBank.ClientID %>").val() == "" || $("#<%=txtCashBank.ClientID %>").val() == "0") {
                    $("#<%=txtCashBank.ClientID %>").focus();
                    $("#loader").hide(); return false;
                }
            }

            if ($("#<%=txtACCode.ClientID %>").val() != "") {
                alert('Please add details');
                $("#<%=txtACCode.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            return true;
        }
        function pagevalidation() {
            debugger;
            var Doc_No = 0, tranid = 0, trandetailid = 0;
            $("#loader").show();
            var transaction_type = $("#<%=drpTrnType.ClientID %>").val();
            var status = document.getElementById("<%= btnSave.ClientID %>").value;
            if (status == "Update") {
                Doc_No = document.getElementById("<%= hdnfreceiptno.ClientID %>").value;
                tranid = document.getElementById("<%= hdnftranid.ClientID %>").value;
            }
            var spname = "ReciptPayment";
            var XML = "<ROOT>";
            var d = $("#<%=txtdoc_date.ClientID %>").val();
            var doc_date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
            var Cash_Bank = $("#<%=txtCashBank.ClientID %>").val();
            var cb = 0;
            if (Cash_Bank == "") {
                Cash_Bank = 1;
                cb = '<%= Session["CASHID"] %>';
            }
            else {
                cb = document.getElementById("<%= hdnfcb.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfcb.ClientID %>").value;
            }
            var Total = Math.round(parseFloat($("#<%=txtTotal.ClientID %>").val() == "" ? 0.00 : $("#<%=txtTotal.ClientID %>").val()));
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';
            var USER = '<%= Session["user"] %>';
            var Branch_Code = '<%= Session["Branch_Code"] %>';


            debugger;
            var HeadInsertUpdate = ""; Detail_Insert = ""; Detail_Update = ""; Detail_Delete = "";
            var Detail_Value = ""; concatid = "";
            if (status == "Save") {
                HeadInsertUpdate = "doc_date='" + doc_date + "' Created_By='" + USER + "'";
            }
            else {
                HeadInsertUpdate = "doc_no='" + Doc_No + "' doc_date='" + doc_date + "' Modified_By='" + USER + "'";
            }
            XML = XML + "<ReceiptHead tran_type='" + transaction_type + "' " + HeadInsertUpdate + " cashbank='" + Cash_Bank + "' " +
                "total='" + Total + "' company_code='" + Company_Code + "' year_code='" + Year_Code + "' cb='" + cb + "' tranid='" + tranid + "'>";

            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            var grdrow = gridView.getElementsByTagName("tr");

            var Gledger_Insert = ""; Gledger_values = "";

            var drcr = ""; drcrGrid = "";
            if (transaction_type == "CP" || transaction_type == "BP") {
                drcr = "C";
                drcrGrid = "D";
            }
            else {
                drcr = "D";
                drcrGrid = "C";
            }
            // Detail Part
            var ddid = trandetailid;
            debugger;
            for (var i = 1; i < grdrow.length; i++) {
                // Assign Value
                var ID = gridView.rows[i].cells[2].innerHTML;
                var drpFilterValue = gridView.rows[i].cells[15].innerHTML;
                var Ac_Code = gridView.rows[i].cells[3].innerHTML == "" ? 0 : gridView.rows[i].cells[3].innerHTML;
                if (Ac_Code == "" || Ac_Code == "&nbsp;") {
                    Ac_Code = 0;
                }

                var Unit_Code = gridView.rows[i].cells[5].innerHTML == "" ? 0 : gridView.rows[i].cells[5].innerHTML;
                if (Unit_Code == "" || Unit_Code == "&nbsp;") {
                    Unit_Code = 0;
                }
                var Voucher_No = gridView.rows[i].cells[7].innerHTML == "" ? 0 : gridView.rows[i].cells[7].innerHTML;
                if (Voucher_No == "" || Voucher_No == "&nbsp;") {
                    Voucher_No = 0;
                }
                var Voucher_Type = gridView.rows[i].cells[8].innerHTML;
                if (Voucher_Type == "&nbsp;") {
                    Voucher_Type = "";
                }
                var Tender_No = gridView.rows[i].cells[9].innerHTML == "" ? 0 : gridView.rows[i].cells[9].innerHTML;
                if (Tender_No == "" || Tender_No == "&nbsp;") {
                    Tender_No = 0;
                }
                var TenderDetailId = gridView.rows[i].cells[10].innerHTML == "" ? 0 : gridView.rows[i].cells[10].innerHTML;
                if (TenderDetailId == "" || TenderDetailId == "&nbsp;") {
                    TenderDetailId = 0;
                }
                var Amount = gridView.rows[i].cells[11].innerHTML == "" ? 0 : gridView.rows[i].cells[11].innerHTML;
                if (Amount == "" || Amount == "&nbsp;") {
                    Amount = 0;
                }
                var Adjusted_Amount = gridView.rows[i].cells[12].innerHTML == "" ? 0 : gridView.rows[i].cells[12].innerHTML;
                if (Adjusted_Amount == "" || Adjusted_Amount == "&nbsp;") {
                    Adjusted_Amount = 0;
                }
                var Narration = gridView.rows[i].cells[13].innerHTML;
                if (Narration == "&nbsp;") {
                    Narration = "";
                }
                debugger;
                var Narration2 = gridView.rows[i].cells[14].innerHTML;
                if (Narration2 == "&nbsp;") {
                    Narration2 = "";
                }
                var YearCodeDetail = gridView.rows[i].cells[16].innerHTML;
                var a = gridView.rows[i].cells[18].innerHTML;
                if (YearCodeDetail == "&nbsp;") {
                    YearCodeDetail = 0;
                }
                var ac = gridView.rows[i].cells[20].innerHTML == "" ? 0 : gridView.rows[i].cells[20].innerHTML;
                if (ac == "&nbsp;") {
                    ac = 0;
                }
                var uc = gridView.rows[i].cells[21].innerHTML == "" ? 0 : gridView.rows[i].cells[21].innerHTML;
                if (uc == "&nbsp;") {
                    uc = 0;
                }
                var AcAdjusted_Amount = gridView.rows[i].cells[22].innerHTML == "" ? 0 : gridView.rows[i].cells[22].innerHTML;
                if (AcAdjusted_Amount == "" || AcAdjusted_Amount == "&nbsp;") {
                    AcAdjusted_Amount = 0;
                }
                var adjAc_Code = gridView.rows[i].cells[23].innerHTML == "" ? 0 : gridView.rows[i].cells[23].innerHTML;
                if (adjAc_Code == "" || adjAc_Code == "&nbsp;") {
                    adjAc_Code = 0;
                }
                var ad = gridView.rows[i].cells[25].innerHTML == "" ? 0 : gridView.rows[i].cells[25].innerHTML;
                if (ad == "&nbsp;") {
                    ad = 0;
                }

                var TDS_Rate = gridView.rows[i].cells[26].innerHTML == "" ? 0 : gridView.rows[i].cells[26].innerHTML;
                if (TDS_Rate == "" || TDS_Rate == "&nbsp;") {
                    TDS_Rate = 0;
                }
                var TDS_Amt = gridView.rows[i].cells[27].innerHTML == "" ? 0 : gridView.rows[i].cells[27].innerHTML;
                if (TDS_Amt == "" || TDS_Amt == "&nbsp;") {
                    TDS_Amt = 0;
                }
                var GRN = gridView.rows[i].cells[28].innerHTML == "" ? 0 : gridView.rows[i].cells[28].innerHTML;
                if (GRN == "" || GRN == "&nbsp;") {
                    GRN = 0;
                }
                var Treceipt = gridView.rows[i].cells[29].innerHTML == "" ? 0 : gridView.rows[i].cells[29].innerHTML;
                if (Treceipt == "" || Treceipt == "&nbsp;") {
                    Treceipt = 0;
                }

                if (gridView.rows[i].cells[18].innerHTML == "A") {
                    XML = XML + "<ReceiptDetailInsert Tran_Type='" + transaction_type + "' doc_no='" + Doc_No + "' doc_date='" + doc_date + "' detail_id='" + ID + "' debit_ac='" + Cash_Bank + "' " +
                        "credit_ac='" + Ac_Code + "' Unit_Code='" + Unit_Code + "' amount='" + Amount + "' " +
               "narration='" + Narration + "' narration2='" + Narration2 + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
               "Created_By='" + USER + "' Modified_By='" + USER + "' Voucher_No='" + Voucher_No + "' Voucher_Type='" + Voucher_Type + "' Adjusted_Amount='" + Adjusted_Amount + "' " +
               "Tender_No='" + Tender_No + "' TenderDetail_ID='" + TenderDetailId + "' drpFilterValue='" + drpFilterValue + "' CreditAcAdjustedAmount='0.00' Branch_name='' " +
               "YearCodeDetail='" + YearCodeDetail + "' tranid='" + tranid + "' ca='" + ac + "' uc='" + uc + "' tenderdetailid='" + TenderDetailId +
               "' sbid='0' da='0' trandetailid='" + ddid + "' drcr='' AcadjAccode='" + adjAc_Code + "' AcadjAmt='" + AcAdjusted_Amount + "' ac='" + ad +
               "' TDS_Rate='" + TDS_Rate + "' TDS_Amt='" + TDS_Amt + "' GRN='" + GRN + "' TReceipt='" + Treceipt + "'/>";
                    ddid = parseInt(ddid) + 1;

                }
                else if (gridView.rows[i].cells[18].innerHTML == "U") {
                    var detailids = gridView.rows[i].cells[17].innerHTML;
                    XML = XML + "<ReceiptDetail Tran_Type='" + transaction_type + "' doc_no='" + Doc_No + "' doc_date='" + doc_date + "' detail_id='" + ID + "' debit_ac='" + Cash_Bank + "' " +
                         "credit_ac='" + Ac_Code + "' Unit_Code='" + Unit_Code + "' amount='" + Amount + "' " +
                "narration='" + Narration + "' narration2='" + Narration2 + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
                "Created_By='" + USER + "' Modified_By='" + USER + "' Voucher_No='" + Voucher_No + "' Voucher_Type='" + Voucher_Type + "' Adjusted_Amount='" + Adjusted_Amount + "' " +
                "Tender_No='" + Tender_No + "' TenderDetail_ID='" + TenderDetailId + "' drpFilterValue='" + drpFilterValue + "' CreditAcAdjustedAmount='0.00' Branch_name='' " +
                "YearCodeDetail='" + YearCodeDetail + "' tranid='" + tranid + "' ca='" + ac + "' uc='" + uc + "' tenderdetailid='" + TenderDetailId +
                "' sbid='0' da='0' trandetailid='" + detailids + "' drcr='' AcadjAccode='" + adjAc_Code + "' AcadjAmt='" + AcAdjusted_Amount +
                "' ac='" + ad + "' TDS_Rate='" + TDS_Rate + "' TDS_Amt='" + TDS_Amt + "' GRN='" + GRN + "' TReceipt='" + Treceipt + "'/>";

                }
                else if (gridView.rows[i].cells[18].innerHTML == "D") {
                    var detailids = gridView.rows[i].cells[17].innerHTML;
                    XML = XML + "<ReceiptDetailDelete Tran_Type='" + transaction_type + "' doc_no='" + Doc_No + "' doc_date='" + doc_date + "' detail_id='" + ID + "' debit_ac='" + Cash_Bank + "' " +
                         "credit_ac='" + Ac_Code + "' Unit_Code='" + Unit_Code + "' amount='" + Amount + "' " +
                "narration='" + Narration + "' narration2='" + Narration2 + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
                "Created_By='" + USER + "' Modified_By='" + USER + "' Voucher_No='" + Voucher_No + "' Voucher_Type='' Adjusted_Amount='" + Adjusted_Amount + "' " +
                "Tender_No='" + Tender_No + "' TenderDetail_ID='" + TenderDetailId + "' drpFilterValue='" + drpFilterValue + "' CreditAcAdjustedAmount='0.00' Branch_name='' " +
                "YearCodeDetail='" + YearCodeDetail + "' tranid='" + tranid + "' ca='" + ac + "' uc='" + uc + "' tenderdetailid='" + TenderDetailId +
                "' sbid='0' da='0' trandetailid='" + detailids + "' drcr='' AcadjAccode='" + adjAc_Code + "' AcadjAmt='" + AcAdjusted_Amount + "' ac='" + ad + "' TDS_Rate='" + TDS_Rate + "' TDS_Amt='" + TDS_Amt + "' GRN='" + GRN + "' TReceipt='" + Treceipt + "'/>";

                }
                var Ac_name = gridView.rows[i].cells[4].innerHTML == "" ? 0 : gridView.rows[i].cells[4].innerHTML;
                Narration = Ac_name + Narration;
                var Order_Code = 1;
                if (gridView.rows[i].cells[18].innerHTML != "D") {
                    XML = XML + "<Ledger TRAN_TYPE='" + transaction_type + "' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Cash_Bank + "' " +
                                                           "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr + "' DRCR_HEAD='" + Ac_Code + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='" + transaction_type + "' SORT_NO='" + Doc_No + "' ac='" + cb + "' vc='0' progid='2' tranid='0'/>";

                    //Gledger_values = Gledger_values + "('" + transaction_type + "','','" + Doc_No + "','" + doc_date + "','" + Cash_Bank + "','0','" + Narration + "','" + Amount + "', " +
                    //                              " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','" + drcr + "',0,0,'" + Branch_Code + "','" + transaction_type + "','" + Doc_No + "'," +
                    //                              " case when 0='" + cb + "' then null else '" + cb + "' end ,'0','2','0'),";
                    Order_Code = Order_Code + 1;
                    //Gledger_values = Gledger_values + "('" + transaction_type + "','','" + Doc_No + "','" + doc_date + "','" + Ac_Code + "','0','" + Narration + "','" + Amount + "', " +
                    //                                      " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','" + drcrGrid + "',0,0,'" + Branch_Code + "','" + transaction_type + "','" + Doc_No + "'," +
                    //                                      " case when 0='" + ac + "' then null else '" + ac + "' end,'0','2','0'),";

                    XML = XML + "<Ledger TRAN_TYPE='" + transaction_type + "' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                           "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcrGrid + "' DRCR_HEAD='" + Cash_Bank + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='" + transaction_type + "' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='2' tranid='0'/>";

                    Order_Code = Order_Code + 1;
                    if (drpFilterValue == 'Z') {
                        if (TDS_Amt != 0) {
                            XML = XML + "<Ledger TRAN_TYPE='" + transaction_type + "' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                             "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                             "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                             "SORT_TYPE='" + transaction_type + "' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='2' tranid='0'/>";
                            Order_Code = Order_Code + 1;
                            XML = XML + "<Ledger TRAN_TYPE='" + transaction_type + "' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["PurchaseTDSAc"] %>' + "' " +
                                                                                  "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                                  "SORT_TYPE='" + transaction_type + "' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["PurchaseTDSacid"] %>' + "' vc='0' progid='2' tranid='0'/>";

                        }
                    }

                    if (adjAc_Code != "") {
                        if (AcAdjusted_Amount != "") {

                            if (transaction_type == "CR" || transaction_type == "BR") {

                                if (AcAdjusted_Amount > 0) {
                                    XML = XML + "<Ledger TRAN_TYPE='" + transaction_type + "' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                              "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + AcAdjusted_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                              "SORT_TYPE='" + transaction_type + "' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='2' tranid='0'/>";

                                    XML = XML + "<Ledger TRAN_TYPE='" + transaction_type + "' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + adjAc_Code + "' " +
                                                                                          "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + AcAdjusted_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                                          "SORT_TYPE='" + transaction_type + "' SORT_NO='" + Doc_No + "' ac='" + ad + "' vc='0' progid='2' tranid='0'/>";


                                }
                                else {
                                    if (AcAdjusted_Amount != 0) {
                                        XML = XML + "<Ledger TRAN_TYPE='" + transaction_type + "' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                                "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + AcAdjusted_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                "SORT_TYPE='" + transaction_type + "' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='2' tranid='0'/>";

                                        XML = XML + "<Ledger TRAN_TYPE='" + transaction_type + "' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + adjAc_Code + "' " +
                                                                                              "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + AcAdjusted_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                                              "SORT_TYPE='" + transaction_type + "' SORT_NO='" + Doc_No + "' ac='" + ad + "' vc='0' progid='2' tranid='0'/>";

                                    }
                                }
                            }
                            else {
                                if (AcAdjusted_Amount > 0) {
                                    XML = XML + "<Ledger TRAN_TYPE='" + transaction_type + "' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                              "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + AcAdjusted_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                              "SORT_TYPE='" + transaction_type + "' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='2' tranid='0'/>";

                                    XML = XML + "<Ledger TRAN_TYPE='" + transaction_type + "' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + adjAc_Code + "' " +
                                                                                          "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + AcAdjusted_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                                          "SORT_TYPE='" + transaction_type + "' SORT_NO='" + Doc_No + "' ac='" + ad + "' vc='0' progid='2' tranid='0'/>";


                                }
                                else {
                                    if (AcAdjusted_Amount != 0) {
                                        XML = XML + "<Ledger TRAN_TYPE='" + transaction_type + "' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                                "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + AcAdjusted_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                "SORT_TYPE='" + transaction_type + "' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='2' tranid='0'/>";

                                        XML = XML + "<Ledger TRAN_TYPE='" + transaction_type + "' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + adjAc_Code + "' " +
                                                                                              "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + AcAdjusted_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                                              "SORT_TYPE='" + transaction_type + "' SORT_NO='" + Doc_No + "' ac='" + ad + "' vc='0' progid='2' tranid='0'/>";

                                    }
                                }
                            }

                        }
                    }
                    if ( drpFilterValue == 'Y') {

                        XML = XML + "<MultipleDetail Tran_Type='" + transaction_type + "' DOC_NO='" + Doc_No + "' detail_Id='" + ID + "' " +
                            "Bill_No='" + Tender_No + "' Bill_Tran_Type='SB' Party_Code='" + Ac_Code + "' pc='" + ac +
                                           "' Bill_Receipt='0' Value='" + Amount + "' Adj_Value='" + Adjusted_Amount + "' Narration='" + Narration + "' " +
                            "Bill_Year_Code='" + YearCodeDetail + "' Bill_Auto_Id='" + TenderDetailId + "' Year_Code='" + Year_Code +
                            "' Doc_Date='" + doc_date + "' OnAc='0.00' AcadjAmt='" + AcAdjusted_Amount + "' AcAdjAc='" + adjAc_Code + "'/>";

                    }
                    if (drpFilterValue == 'B') {
                        if (vouchertype == '') {
                            vouchertype = 'SB';
                        }
                        XML = XML + "<MultipleDetail Tran_Type='" + transaction_type + "' DOC_NO='" + Doc_No + "' detail_Id='" + ID + "' " +
                            "Bill_No='" + Tender_No + "' Bill_Tran_Type='" + Voucher_Type + "' Party_Code='" + Ac_Code + "' pc='" + ac +
                                           "' Bill_Receipt='0' Value='" + Amount + "' Adj_Value='" + Adjusted_Amount + "' Narration='" + Narration + "' " +
                            "Bill_Year_Code='" + YearCodeDetail + "' Bill_Auto_Id='" + TenderDetailId + "' Year_Code='" + Year_Code +
                            "' Doc_Date='" + doc_date + "' OnAc='0.00' AcadjAmt='" + AcAdjusted_Amount + "' AcAdjAc='" + adjAc_Code + "'/>";

                    }
                    if (drpFilterValue == 'T') {
                        debugger;
                        XML = XML + "<MultipleDetail Tran_Type='" + transaction_type + "' DOC_NO='" + Doc_No + "' detail_Id='" + ID + "' " +
                            "Bill_No='" + Tender_No + "' Bill_Tran_Type='DO' Party_Code='" + Ac_Code + "' pc='" + ac +
                                           "' Bill_Receipt='0' Value='" + Amount + "' Adj_Value='" + Adjusted_Amount + "' Narration='" + Narration + "' " +
                            "Bill_Year_Code='" + YearCodeDetail + "' Bill_Auto_Id='" + TenderDetailId + "' Year_Code='" + Year_Code +
                            "' Doc_Date='" + doc_date + "' OnAc='0.00' AcadjAmt='" + AcAdjusted_Amount + "' AcAdjAc='" + adjAc_Code + "'/>";

                    }
                    if (drpFilterValue == 'R') {
                        debugger;
                        XML = XML + "<MultipleDetail Tran_Type='" + transaction_type + "' DOC_NO='" + Doc_No + "' detail_Id='" + ID + "' " +
                            "Bill_No='" + Tender_No + "' Bill_Tran_Type='RR' Party_Code='" + Ac_Code + "' pc='" + ac +
                                           "' Bill_Receipt='0' Value='" + Amount + "' Adj_Value='" + Adjusted_Amount + "' Narration='" + Narration + "' " +
                            "Bill_Year_Code='" + YearCodeDetail + "' Bill_Auto_Id='" + TenderDetailId + "' Year_Code='" + Year_Code +
                            "' Doc_Date='" + doc_date + "' OnAc='0.00' AcadjAmt='" + AcAdjusted_Amount + "' AcAdjAc='" + adjAc_Code + "'/>";

                    }

                }
                //if (drpFilterValue == 'B' || drpFilterValue == 'Y') {

                //    XML = XML + "<MultipleDetail Tran_Type='" + transaction_type + "' DOC_NO='" + Doc_No + "' detail_Id='" + ID + "' " +
                //        "Bill_No='" + Tender_No + "' Bill_Tran_Type='SB' Party_Code='" + Ac_Code + "' pc='" + ac +
                //                       "' Bill_Receipt='0' Value='" + Amount + "' Adj_Value='" + Adjusted_Amount + "' Narration='" + Narration + "' " +
                //        "Bill_Year_Code='" + YearCodeDetail + "' Bill_Auto_Id='" + TenderDetailId + "' Year_Code='" + Year_Code +
                //        "' Doc_Date='" + doc_date + "' OnAc='0.00' AcadjAmt='" + AcAdjusted_Amount + "' AcAdjAc='" + adjAc_Code + "'/>";

                //}
                //if (drpFilterValue == 'T') {
                //    debugger;
                //    XML = XML + "<MultipleDetail Tran_Type='" + transaction_type + "' DOC_NO='" + Doc_No + "' detail_Id='" + ID + "' " +
                //        "Bill_No='" + Tender_No + "' Bill_Tran_Type='DO' Party_Code='" + Ac_Code + "' pc='" + ac +
                //                       "' Bill_Receipt='0' Value='" + Amount + "' Adj_Value='" + Adjusted_Amount + "' Narration='" + Narration + "' " +
                //        "Bill_Year_Code='" + YearCodeDetail + "' Bill_Auto_Id='" + TenderDetailId + "' Year_Code='" + Year_Code +
                //        "' Doc_Date='" + doc_date + "' OnAc='0.00' AcadjAmt='" + AcAdjusted_Amount + "' AcAdjAc='" + adjAc_Code + "'/>";

                //}
            }

            XML = XML + "</ReceiptHead></ROOT>";
            $.ajax({
                type: "POST",
                url: "../xmlExecuteDMLQry.asmx/ExecuteXMLQry",
                data: '{XML:"' + XML + '",status:"' + status + '",spname:"' + spname + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    $("#loader").hide();
                    alert(response.d);
                },
                error: function (response) {
                    $("#loader").hide();
                    alert(response.d);

                }
            });

            function OnSuccess(response) {
                debugger;
                $("#loader").hide();
                if (response.d.length > 0) {
                    var word = response.d;
                    var len = word.length;
                    var pos = word.indexOf(",");
                    var id = word.slice(0, pos);
                    var doc = word.slice(pos + 1, len);
                    if (status == "Save") {
                        alert('Sucessfully Added Record !!! Doc_no=' + doc)
                    }
                    else {
                        alert('Sucessfully Updated Record !!! Doc_no=' + doc)
                    }
                    //window.open('../Transaction/pgeReceiptPaymentxml.aspx?tranid=' + tranid + '&Action=1', "_self");
                    window.open('../Transaction/pgeReceiptPaymentxml.aspx?tranid=' + id + '&Action=1&tran_type=' + transaction_type, "_self");

                }

            }

        }
    </script>
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
            <asp:Label ID="label1" runat="server" Text="   Receipt/Payment   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px;">
        <img height="40%" width="40%" src="../Images/ajax-loader3.gif" />
    </div>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfTran_type" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfTransportBalance" runat="server" />
            <asp:HiddenField ID="hdnflblvoucher" runat="server" />
            <asp:HiddenField ID="hdnfcb" runat="server" />
            <asp:HiddenField ID="hdnfreceiptno" runat="server" />
            <asp:HiddenField ID="hdnftranid" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
            <asp:HiddenField ID="hhnfad" runat="server" />
            <asp:HiddenField ID="hdnftypenew" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table width="80%" align="left">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                TabIndex="16" ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                            &nbsp;
                        <asp:Button ID="btnSave" OnClientClick="if (!Validate()) return false;" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            TabIndex="17" ValidationGroup="add" OnClick="btnSave_Click" Height="24px" />
                            &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            TabIndex="18" ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                            &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            TabIndex="19" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px" />
                            &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            TabIndex="20" Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" />
                            &nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                            TabIndex="20" Height="24px" ValidationGroup="save" OnClientClick="Back()" />
                            <asp:Button runat="server" ID="btnPrintSaleBill" Text="Print" CssClass="btnHelp"
                                Width="80px" Height="24px" OnClientClick="SB();" />
                        </td>
                          <td align="center">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            Height="24px" OnClick="btnFirst_Click" Width="90px" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                            Height="24px" OnClick="btnPrevious_Click" Width="90px" />
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                            Height="24px" OnClick="btnNext_Click" Width="90px" />
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                            Height="24px" OnClick="btnLast_Click" Width="90px" />
                    </td>
                    </tr>
                </table>
                <br />
                <br />
                <table align="left" cellspacing="5">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Transaction Type:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpTrnType" runat="server" CssClass="ddl" Width="200px" Height="24px"
                                TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="drpTrnType_SelectedIndexChanged">
                                <asp:ListItem Text="Cash Payment" Value="CP"></asp:ListItem>
                                <asp:ListItem Text="Cash Receipt" Value="CR"></asp:ListItem>
                                <asp:ListItem Text="Bank Payment" Value="BP"></asp:ListItem>
                                <asp:ListItem Text="Bank Receipt" Value="BR"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">Change No:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                            &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%;">Entry No:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtdoc_no" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtdoc_no_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtdoc_no" runat="server" Text="..." Width="80px" OnClick="btntxtdoc_no_Click"
                                CssClass="btnHelp" Height="24px" />
                            <asp:Label ID="lblReceiptPayment_Id" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red" Visible="false"></asp:Label>
                            <ajax1:FilteredTextBoxExtender ID="Filteretxtdoc_no" runat="server" TargetControlID="txtdoc_no"
                                FilterType="Numbers">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                        <td align="right" style="width: 10%;">Date:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtdoc_date" runat="Server" CssClass="txt" TabIndex="2" Width="90px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtdoc_date_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" />
                            <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtdoc_date"
                                PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Cash/Bank:
                        </td>
                        <td align="left" colspan="5">
                            <asp:TextBox ID="txtCashBank" runat="server" CssClass="txt" Style="text-align: right;"
                                onkeydown="cashbank(event);" AutoPostBack="false" TabIndex="3" OnTextChanged="txtCashBank_TextChanged" Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtCashBank" runat="server" Text="..." OnClick="btntxtCashBank_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblCashBank" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>

                </table>
                <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                    <legend style="text-align: center;">
                        <h5 style="color: White;" font-names="verdana" font-size="Medium">Detail Entry</h5>
                    </legend>
                </fieldset>
                <table width="90%" align="center">

                    <tr>
                        <td align="right">
                            <asp:Label ID="lblNo" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblID" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">A/C Code:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtACCode" runat="Server" CssClass="txt" Width="80px" Style="text-align: right;"
                                onkeydown="accode(event);" TabIndex="4" AutoPostBack="false" OnTextChanged="txtACCode_TextChanged" Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtACCode" runat="server" Text="..." OnClick="btntxtACCode_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblACName" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblclosingbalance" runat="server" CssClass="lblName" ForeColor="Yellow"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Unit:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtUnit_Code" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                onkeydown="unit(event);" Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtUnit_Code_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtUnitcode" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                Width="20px" OnClick="btntxtUnitcode_Click" />
                            <asp:Label ID="lblUnitName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Select:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpFilter" runat="server" CssClass="ddl" Width="280px" Height="25px"
                                Visible="true" AutoPostBack="true" TabIndex="6" OnSelectedIndexChanged="drpFilter_SelectedIndexChanged"
                                OnTextChanged="drpFilter_SelectedIndexChanged">
                                <asp:ListItem Text="Againt Loading Voucher" Value="V"></asp:ListItem>
                                <asp:ListItem Text="Againt Sauda" Value="S"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label Text="Voucher Number:" runat="server" ID="lblHead"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtVoucherNo" Height="24px" runat="Server" CssClass="txt" Width="80px" Enabled="false"
                                onkeydown="voucherno(event);" Style="text-align: right;" TabIndex="7" AutoPostBack="false" OnTextChanged="txtVoucherNo_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtVoucherNo" runat="server" Text="..." OnClick="btntxtVoucherNo_Click"
                                TabIndex="8" CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:TextBox ID="txtvoucherType" Enabled="false" runat="Server" CssClass="txt" Width="20px"
                                onkeydown="vouchertype(event);" Style="text-align: right;" AutoPostBack="False" Height="24px"></asp:TextBox>
                            <asp:Label ID="lblVoucherBy" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Button ID="btnsoudaall" runat="server" Text="..." OnClick="btnsoudaall_Click"
                                TabIndex="8" CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label runat="server" ID="lbltype" CssClass="lblName" />
                        </td>

                    </tr>
                    <tr>
                        <td align="left">Amount:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtamount" runat="Server" TabIndex="9" Height="24px" CssClass="txt"
                                Width="80px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtamount_TextChanged"></asp:TextBox><asp:Label
                                    runat="server" ID="lblErrorAdvance" Text="" ForeColor="Red"></asp:Label>
                            <ajax1:FilteredTextBoxExtender ID="filterAmount" runat="server" TargetControlID="txtamount"
                                FilterType="Custom,Numbers" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            &nbsp;&nbsp;&nbsp;
                            Adjusted Amount:
                            <asp:TextBox ID="txtadAmount" runat="Server" CssClass="txt" Height="24px" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" TabIndex="10" OnTextChanged="txtadAmount_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FiltertxtadAmount" runat="server" TargetControlID="txtamount"
                                FilterType="Custom,Numbers" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Acc Adjusted Amt
                            <asp:TextBox ID="txtacadjamt" Height="24px" runat="Server" CssClass="txt" Width="80px"
                                Style="text-align: right;" TabIndex="11"></asp:TextBox>
                            Adjusted AC Code:
                        
                            <asp:TextBox ID="txtacadjAccode" runat="Server" CssClass="txt" Width="80px" Style="text-align: right;"
                                onkeydown="acadjaccode(event);" TabIndex="12" AutoPostBack="false" OnTextChanged="txtacadjAccode_TextChanged" Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtacadjAccode" runat="server" Text="..." OnClick="btntxtacadjAccode_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblacadjname" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">TDS%:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTDSRate" runat="Server" TabIndex="13" Height="24px" CssClass="txt"
                                Width="80px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtTDSRate_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtTDSRate" runat="server" TargetControlID="txtTDSRate"
                                FilterType="Custom,Numbers" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            &nbsp;&nbsp;&nbsp;
                            TDS Amount:
                            <asp:TextBox ID="txtTDSAmt" runat="Server" CssClass="txt" Height="24px" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" TabIndex="14" OnTextChanged="txtTDSAmt_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtTDSAmt" runat="server" TargetControlID="txtTDSAmt"
                                FilterType="Custom,Numbers" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <%--<tr>
                        <td align="left" >
                            UTR Narration:
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtUtrNarration" runat="Server" CssClass="txt" Height="24px" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" TabIndex="10" OnTextChanged="txtadAmount_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtamount"
                                FilterType="Custom,Numbers" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>--%>
                    <tr>
                        <td align="left">Narration:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtnarration" runat="Server" CssClass="txt" Width="480px" Height="50px"
                                onkeydown="narration(event);" TextMode="MultiLine" Style="text-align: left;" AutoPostBack="True" TabIndex="15"
                                OnTextChanged="txtnarration_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtnarration" runat="server" Text="..." OnClick="btntxtnarration_Click"
                                CssClass="btnHelp" Width="20px" Height="24px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Narration 2:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtNarration2" runat="Server" CssClass="txt" Width="480px" Height="24px"
                                Style="text-align: left;" AutoPostBack="True" TabIndex="16" OnTextChanged="txtNarration2_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">GRN
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtGRN" runat="Server" CssClass="txt" Width="480px" Height="24px"
                                Style="text-align: left;" TabIndex="16"></asp:TextBox>
                            <asp:HyperLink Target="_blank" ID="HyperLink2" runat="server">GO</asp:HyperLink>

                        </td>
                    </tr>
                    <tr>
                        <td align="left">T Receipt
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTReceipt" runat="Server" CssClass="txt" Width="480px" Height="24px"
                                Style="text-align: left;" TabIndex="16"></asp:TextBox>

                            <asp:HyperLink Target="_blank" ID="HyperLink1" runat="server">GO</asp:HyperLink>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2" align="left">
                            <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                Height="24px" OnClick="btnAdddetails_Click" TabIndex="16" />
                            <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp"
                                Height="24px" Width="80px" OnClick="btnClosedetails_Click" TabIndex="17" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="width: 100%; position: relative;">
                <asp:UpdatePanel ID="upGrid" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlgrdDetail" runat="server" ScrollBars="Both" Height="250px" Width="1500px"
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
            </div>
            <table width="70%" align="left">
                <tr>
                    <td align="right">
                        <asp:Label ID="lblTotal" runat="server" CssClass="lblName" Font-Bold="true" Text="Total:"></asp:Label>&nbsp;<asp:TextBox
                            ID="txtTotal" runat="server" ReadOnly="true" CssClass="txt" Width="100px" Height="25px" Style="text-align: right;"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />

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
            <%-- <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="GhostWhite" Width="955px"
                BorderColor="Teal" BorderWidth="1px" Height="400px" BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
            </asp:Panel>--%>
            <%--<ajax1:ModalPopupExtender runat="server" ID="popup1" CancelControlID="btnClosedetails"
                PopupControlID="pnlPopupDetails" TargetControlID="btnOpenDetailsPopup">
            </ajax1:ModalPopupExtender>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
