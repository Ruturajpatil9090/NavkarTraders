<%@ Page Title="PurchaseReturn" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeSugarPurchaseReturnForGST.aspx.cs" Inherits="Sugar_pgeSugarPurchaseReturnForGST" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Script/jquery-1.4.2.js"></script>

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



    <%--<script type="text/javascript">
        window.onbeforeunload = function (event) {
            var message = 'All changes will get lost!';
            if (typeof event == 'undefined') {
                event = window.event;
            }
            if (event) {
                event.returnValue = message;
            }
            return message;
        }
    </script>--%>
    <script type="text/javascript">
        function SB() {
            var billno = document.getElementById('<%=txtDOC_NO.ClientID %>').value;
            window.open('../Report/rptreturnsugarpurchase.aspx?billno=' + billno)
        }
        function EInovice() {
            var dono = document.getElementById('<%=txtDOC_NO.ClientID %>').value;
            var ID = document.getElementById('<%=hdnf.ClientID %>').value;
            window.open('../Utility/pgeEInovice.aspx?dono=' + dono + '&Type=PR&ID=' + ID, "_self");
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
                if (hdnfClosePopupValue == "txtPURCNO") {

                    document.getElementById("<%=txtPURCNO.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= hdnfTranType.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%= hdHelpyearcode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[7].innerText;
                    document.getElementById("<%= hdSaleID.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[8].innerText;
                    document.getElementById("<%=txtPURCNO.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtAC_CODE") {
                    document.getElementById("<%=txtAC_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LblPartyname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtAC_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtUnit_Code") {
                    document.getElementById("<%=txtUnit_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblUnitName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtUnit_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtMILL_CODE") {
                    document.getElementById("<%=txtMILL_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLMILLNAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtMILL_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBROKER") {
                    document.getElementById("<%=txtBROKER.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLBROKERNAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBROKER.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtDOC_NO") {

                    document.getElementById("<%=txtDOC_NO.ClientID %>").disabled = true;
                    document.getElementById("<%=txtDOC_NO.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtPURCNO.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtITEM_CODE") {
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLITEMNAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGSTRateCode") {
                    document.getElementById("<%=txtGSTRateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblGSTRateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtbillNo.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtBillTo") {
                    document.getElementById("<%=txtBillTo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBillTo.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtUnit_Code.ClientID %>").focus();
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
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <script type="text/javascript">
    
    </script>
    <script type="text/javascript">
        function Ac_Code(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtAC_CODE.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtAC_CODE.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtAC_CODE.ClientID %>").val(unit);
                __doPostBack("txtAC_CODE", "TextChanged");

            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }

        }
        function BillTo(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtBillTo.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBillTo.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtBillTo.ClientID %>").val(unit);
                __doPostBack("txtBillTo", "TextChanged");

            }
        }
        function UnitCode(e) {
            debugger;
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
        function MillCode(e) {
            debugger;
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
        function Broker(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtBROKER.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBROKER.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtBROKER.ClientID %>").val(unit);
                __doPostBack("txtBROKER", "TextChanged");

            }
        }
        function GSTRateCode(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtGSTRateCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtGSTRateCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtGSTRateCode.ClientID %>").val(unit);
                __doPostBack("txtGSTRateCode", "TextChanged");

            }
        }
        function Item(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtITEM_CODE.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtITEM_CODE.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtITEM_CODE.ClientID %>").val(unit);
                __doPostBack("txtITEM_CODE", "TextChanged");

            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=txtNETQNTL.ClientID %>").focus();
            }
        }
        function save(e) {
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }
        }
        function PurcNo(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtPURCNO.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtPURCNO.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtPURCNO.ClientID %>").val(unit);
                __doPostBack("txtPURCNO", "TextChanged");

            }
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
                if (hdnfClosePopupValue == "txtAC_CODE") {
                    document.getElementById("<%=txtAC_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPURCNO") {
                    document.getElementById("<%=txtPURCNO.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBillTo") {
                    document.getElementById("<%=txtBillTo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtUnit_Code") {
                    document.getElementById("<%=txtUnit_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtMILL_CODE") {
                    document.getElementById("<%=txtMILL_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBROKER") {
                    document.getElementById("<%=txtBROKER.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtDOC_NO") {
                    document.getElementById("<%=txtDOC_NO.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtITEM_CODE") {
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtGSTRateCode") {
                    document.getElementById("<%=txtGSTRateCode.ClientID %>").focus();
                }
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
    </script>
    <script type="text/javascript">
        function Back() {

            //alert(td);
            window.open('../Inword/PgeShugarPurchHeadUtility.aspx', '_self');
        }
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function Validate() {
            debugger;
            $("#loader").show();

            // validation

            if ($("#<%=txtITEM_CODE.ClientID %>").val() != "") {
                alert('Please add details');
                $("#<%=txtITEM_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }

            var Inword_Date = '<%= Session["Inword_Date"] %>';
            var DocDate = $("#<%=txtDOC_DATE.ClientID %>").val();

            var StartDate = '<%= Session["Start_Date"] %>';
            var EndDate = '<%= Session["End_Date"] %>';

            var d = $("#<%=txtDOC_DATE.ClientID %>").val();
            var DocDates = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

            StartDate = StartDate.slice(6, 11) + "/" + StartDate.slice(3, 5) + "/" + StartDate.slice(0, 2);
            EndDate = EndDate.slice(6, 11) + "/" + EndDate.slice(3, 5) + "/" + EndDate.slice(0, 2);

            Inword_Date = Outword_Date.slice(6, 11) + Outword_Date.slice(3, 5) + "/" + Outword_Date.slice(0, 2);
            DocDate = DocDate.slice(6, 11) + "/" + DocDate.slice(3, 5) + "/" + DocDate.slice(0, 2);
            if (DocDate < Outword_Date) {
                alert('GST Return Fined please Do not edit Record');
                $("#loader").hide();
                return false;
            }

            if (DocDates >= StartDate && DocDates <= EndDate) {
            }
            else {
                $("#<%=txtDOC_DATE.ClientID %>").focus();
                alert('Not A Valid Date Range')
                $("#loader").hide();
                return false;
            }



            if ($("#<%=txtAC_CODE.ClientID %>").val() == "") {
                $("#<%=txtAC_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtMILL_CODE.ClientID %>").val() == "") {
                $("#<%=txtMILL_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            var count = 0;
            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            if (gridView == null) {
                alert('Please Enter Purchase Details!');
                $("#loader").hide();
                return false;
            }
            var grdrow = gridView.getElementsByTagName("tr");
            var ro = (grdrow.length - 1);
            if (ro == 0) {
                alert('Please Enter Purchase Details!');
                $("#loader").hide();
                return false;
            }
            if (ro >= 1) {
                for (var i = 1; i < grdrow.length; i++) {
                    var action = gridView.rows[i].cells[12].innerHTML;
                    if (gridView.rows[i].cells[12].innerHTML == "D") {
                        count = count + 1;
                    }
                }
                if (ro == count) {
                    alert('Minimum One Purchase Details is compulsory!');
                    $("#loader").hide();
                    return false;
                }
            }
            return true;
        }

        function pagevalidation() {
            debugger;
            try {
                $("#loader").show();
                var Doc_No = 0, prid = 0, prdetailid = 0;
                var spname = "PurchaseReturn";
                var XML = "<ROOT>";
                var status = document.getElementById("<%= btnSave.ClientID %>").value;
                if (status == "Update") {
                    Doc_No = document.getElementById("<%= hdnfdocno.ClientID %>").value;
                    prid = document.getElementById("<%= hdnfid.ClientID %>").value;
                }
                var gridView = document.getElementById("<%=grdDetail.ClientID %>");
                var grdrow = gridView.getElementsByTagName("tr");
                var PURCNO = $("#<%=txtPURCNO.ClientID %>").val() == "" ? 0 : $("#<%=txtPURCNO.ClientID%>").val();
                var PurcTranType = $("#<%=lblTranType.ClientID %>").text() == "&nbsp" ? "" : $("#<%=lblTranType.ClientID %>").text();
                var d = $("#<%=txtDOC_DATE.ClientID %>").val();
                var doc_date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                var Ac_Code = $("#<%=txtAC_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtAC_CODE.ClientID%>").val();
                var Unit_Code = $("#<%=txtUnit_Code.ClientID %>").val() == "" ? 0 : $("#<%=txtUnit_Code.ClientID%>").val();
                var Bill_To = $("#<%=txtBillTo.ClientID %>").val() == "" ? 0 : $("#<%=txtBillTo.ClientID%>").val();

                var mill_code = $("#<%=txtMILL_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtMILL_CODE.ClientID%>").val();
                var FROM_STATION = $("#<%=txtFROM_STATION.ClientID%>").val();
                var TO_STATION = $("#<%=txtTO_STATION.ClientID %>").val();
                var LORRYNO = $("#<%=txtLORRYNO.ClientID%>").val();
                var BROKER = $("#<%=txtBROKER.ClientID %>").val() == "" ? 0 : $("#<%=txtBROKER.ClientID%>").val();
                var wearhouse = $("#<%=txtWEARHOUSE.ClientID%>").val()
                var subTotal = $("#<%=txtSUBTOTAL.ClientID %>").val() == "" ? 0 : $("#<%=txtSUBTOTAL.ClientID%>").val();
                var LESS_FRT_RATE = $("#<%=txtLESS_FRT_RATE.ClientID %>").val() == "" ? 0 : $("#<%=txtLESS_FRT_RATE.ClientID%>").val();
                var freight = $("#<%=txtFREIGHT.ClientID %>").val() == "" ? 0 : $("#<%=txtFREIGHT.ClientID%>").val();
                var cash_advance = $("#<%=txtCASH_ADVANCE.ClientID %>").val() == "" ? 0 : $("#<%=txtCASH_ADVANCE.ClientID%>").val();
                var bank_commission = $("#<%=txtBANK_COMMISSION.ClientID %>").val() == "" ? 0 : $("#<%=txtBANK_COMMISSION.ClientID%>").val();
                var OTHER_AMT = $("#<%=txtOTHER_AMT.ClientID %>").val() == "" ? 0 : $("#<%=txtOTHER_AMT.ClientID%>").val();
                var Bill_Amount = $("#<%=txtBILL_AMOUNT.ClientID %>").val() == "" ? 0 : $("#<%=txtBILL_AMOUNT.ClientID%>").val();
                var Due_Days = $("#<%=txtDUE_DAYS.ClientID %>").val() == "" ? 0 : $("#<%=txtDUE_DAYS.ClientID%>").val();
                var NETQNTL = $("#<%=txtNETQNTL.ClientID %>").val() == "" ? 0 : $("#<%=txtNETQNTL.ClientID%>").val();
                var Bill_No = $("#<%=txtbillNo.ClientID%>").val();

                var Company_Code = '<%= Session["Company_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';
                var USER = '<%= Session["user"] %>';
                var Branch_Code = '<%= Session["Branch_Code"] %>';
                var CGSTRate = $("#<%=txtCGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtCGSTRate.ClientID%>").val();
                var CGSTAmount = $("#<%=txtCGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtCGSTAmount.ClientID%>").val();
                var IGSTRate = $("#<%=txtIGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtIGSTRate.ClientID%>").val();
                var IGSTAmount = $("#<%=txtIGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtIGSTAmount.ClientID%>").val();
                var SGSTRate = $("#<%=txtSGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtSGSTRate.ClientID%>").val();
                var SGSTAmount = $("#<%=txtSGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtSGSTAmount.ClientID%>").val();
                var GstRateCode = $("#<%=txtGSTRateCode.ClientID %>").val() == "" ? 0 : $("#<%=txtGSTRateCode.ClientID%>").val();
                var purcyearcode = $("#<%=lblyearcode.ClientID %>").text() == "" ? 0 : $("#<%=lblyearcode.ClientID%>").text();
                if (purcyearcode == "" || purcyearcode == "&nbsp;") {
                    purcyearcode = 0;
                }
                var mc = document.getElementById("<%= hdnfmc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfmc.ClientID %>").value;
                var ac = document.getElementById("<%= hdnfac.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfac.ClientID %>").value;
                var uc = document.getElementById("<%= hdnfuc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfuc.ClientID %>").value;
                var bc = document.getElementById("<%= hdnfbc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbc.ClientID %>").value;
                var billid = document.getElementById("<%= hdnfbillid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbillid.ClientID %>").value;
                var saleid = document.getElementById("<%= hdnfsaleid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfsaleid.ClientID %>").value;
                var PurchaseAc = document.getElementById("<%= hdnfPurchaseAc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfPurchaseAc.ClientID %>").value;
                var Purcacid = document.getElementById("<%= hdnfpurcacid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpurcacid.ClientID %>").value;

                var TCS_Rate = $("#<%=txtTCSRate.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSRate.ClientID %>").val();
                var TCS_Amt = $("#<%=txtTCSAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSAmt.ClientID %>").val();
                var TCS_Net_Payable = $("#<%=txtTCSNet_Payable.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSNet_Payable.ClientID %>").val();

                var TDS_Rate = parseFloat($("#<%=txtTDS.ClientID %>").val() == "" ? 0 : $("#<%=txtTDS.ClientID %>").val());
                var TDS_Amt = parseFloat($("#<%=txtTDSAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtTDSAmt.ClientID %>").val());

                var MillShortName = document.getElementById("<%= hdnfMillshort.ClientID %>").value;
                var BrokerShort = document.getElementById("<%= hdnfBrokerShort.ClientID %>").value;
                var PartyShort = document.getElementById("<%= hdnfPartyShort.ClientID %>").value;

                if (mc == "" || mc == "&nbsp;") {
                    mc = "0";
                }
                if (ac == "" || ac == "&nbsp;") {
                    ac = "0";
                }
                if (uc == "" || uc == "&nbsp;") {
                    uc = "0";
                }
                if (bc == "" || bc == "&nbsp;") {
                    bc = "0";
                }
                if (billid == "" || billid == "&nbsp;") {
                    billid = "0";
                }
                if (saleid == "" || saleid == "&nbsp;") {
                    saleid = "0";
                }

                var einvoiceno = $("#<%=txteinvoiceno.ClientID %>").val();
                if (einvoiceno == "&nbsp;") {
                    einvoiceno = "";
                }
                var ackno = $("#<%=txtackno.ClientID %>").val();
                if (ackno == "&nbsp;") {
                    ackno = "";
                }

                var PurchaseReturnInsertUpdate = ""; PurchaseReturnDetail_Insert = ""; PurchaseReturnDetail_Update = ""; PurchaseReturnDetail_Delete = "";
                var PurchaseReturnDetail_Value = ""; concatid = "";
                var DOCNO = "";
                if (status == "Save") {
                    DOCNO = "PURCNO='" + PURCNO + "'";
                }
                else {
                    DOCNO = "doc_no='" + Doc_No + "' PURCNO='" + PURCNO + "'";
                }
                XML = XML + "<PurchaseReturn " + DOCNO + " PurcTranType='" + PurcTranType + "' Tran_Type='PR' doc_date='" + doc_date + "' Ac_Code= '" + Ac_Code + "' " +
                   "Unit_Code='" + Unit_Code + "' mill_code='" + mill_code + "' FROM_STATION='" + FROM_STATION + "' " +
                   "TO_STATION='" + TO_STATION + "' LORRYNO='" + LORRYNO + "' BROKER='" + BROKER + "' wearhouse='" + wearhouse + "' " +
                   "subTotal='" + subTotal + "' LESS_FRT_RATE='" + LESS_FRT_RATE + "' freight='" + freight + "' cash_advance='" + cash_advance + "' " +
                  "bank_commission='" + bank_commission + "' OTHER_AMT='" + OTHER_AMT + "' Bill_Amount='" + Bill_Amount + "' " +
                   "Due_Days='" + Due_Days + "' NETQNTL='" + NETQNTL + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
                   "Created_By='" + USER + "' Modified_By='" + USER + "' Bill_No='" + Bill_No + "' CGSTRate='" + CGSTRate + "' CGSTAmount='" + CGSTAmount + "' SGSTRate='" + SGSTRate + "' " +
                   "SGSTAmount='" + SGSTAmount + "' IGSTRate='" + IGSTRate + "' IGSTAmount='" + IGSTAmount + "' GstRateCode='" + GstRateCode + "' " +
                   "purcyearcode='" + purcyearcode + "' Bill_To='" + Bill_To + "' prid='" + prid + "' srid='0' ac='" + ac + "' uc='" + uc + "' mc='" + mc + "' bc='" + bc + "' " +
                   "bt='" + billid + "' sbid='" + saleid + "' TCS_Rate='" + TCS_Rate + "' TCS_Amt='" + TCS_Amt + "' TCS_Net_Payable='" + TCS_Net_Payable + "' einvoiceno='" + einvoiceno + "' ackno='" + ackno + "' TDS_Rate='" + TDS_Rate + "' TDS_Amt='" + TDS_Amt + "'>";
                // Detail Part
                var ddid = prdetailid;
                for (var i = 1; i < grdrow.length; i++) {
                    // Assign Value
                    var ID = gridView.rows[i].cells[2].innerHTML;
                    var item_code = gridView.rows[i].cells[3].innerHTML;
                    var narration = gridView.rows[i].cells[5].innerHTML;
                    if (narration == "&nbsp;") {
                        narration = "";
                    }
                    var Quntal = gridView.rows[i].cells[6].innerHTML;
                    var packing = gridView.rows[i].cells[7].innerHTML;
                    var bags = gridView.rows[i].cells[8].innerHTML;
                    var rate = gridView.rows[i].cells[9].innerHTML;
                    var item_amount = gridView.rows[i].cells[10].innerHTML;
                    var ic = gridView.rows[i].cells[14].innerHTML;

                    if (gridView.rows[i].cells[12].innerHTML == "A") {
                        XML = XML + "<PurchaseReturnInsert doc_no='" + Doc_No + "' detail_id='" + ID + "' Tran_Type='PR' item_code='" + item_code + "' narration='" + narration + "' Quantal='" + Quntal + "' " +
                        "packing='" + packing + "' bags='" + bags + "' rate='" + rate + "' item_Amount='" + item_amount + "'  Company_Code='" + Company_Code + "' " +
                        "Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' ic='" + ic + "' prdid='" + ddid + "' prid='" + prid + "'/>";
                        ddid = parseInt(ddid) + 1;
                    }
                    else if (gridView.rows[i].cells[12].innerHTML == "U") {
                        var prdetail = gridView.rows[i].cells[11].innerHTML;
                        XML = XML + "<PurchaseDetail doc_no='" + Doc_No + "' detail_id='" + ID + "' Tran_Type='PR' item_code='" + item_code + "' narration='" + narration + "' Quantal='" + Quntal + "' " +
                        "packing='" + packing + "' bags='" + bags + "' rate='" + rate + "' item_Amount='" + item_amount + "'  Company_Code='" + Company_Code + "' " +
                        "Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' ic='" + ic + "' prdid='" + prdetail + "' prid='" + prid + "'/>";
                    }
                    else if (gridView.rows[i].cells[12].innerHTML == "D") {
                        var prdetail = gridView.rows[i].cells[11].innerHTML;
                        XML = XML + "<PurchaseReturnDelete doc_no='" + Doc_No + "' detail_id='" + ID + "' Tran_Type='PR' item_code='" + item_code + "' narration='" + narration + "' Quantal='" + Quntal + "' " +
                        "packing='" + packing + "' bags='" + bags + "' rate='" + rate + "' item_Amount='" + item_amount + "'  Company_Code='" + Company_Code + "' " +
                        "Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' ic='" + ic + "' prdid='" + prdetail + "' prid='" + prid + "'/>";
                    }
                }

                // GLedger
                var Order_Code = 1;


                var DebitNarration = "" + MillShortName + " " + NETQNTL + " Lorry:" + LORRYNO + "" + BrokerShort + "" + PartyShort + "" + PURCNO + ":" + PurcTranType + "";
                var CreditNarration = "" + PartyShort + " " + NETQNTL + "Lorry:" + subTotal + "/" + NETQNTL + "" + PURCNO + ":" + PurcTranType + "";
                var TCSNarration = 'TCS' + PartyShort + " " + Doc_No;
                var TDSNarration = 'TDS' + PartyShort + " " + Doc_No;
                if (Ac_Code != 0) {
                    //Gledger_values = Gledger_values + "('PR','','" + Doc_No + "','" + doc_date + "','" + Ac_Code + "','0','','" + Bill_Amount + "', " +
                    //                                      " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C','" + PurchaseAc + "',0,'" + Branch_Code + "','PR','" + Doc_No + "'," +
                    //                                      " case when 0='" + ac + "' then null else '" + ac + "' end,'0','7','0')";

                    XML = XML + "<Ledger TRAN_TYPE='PR' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                           "UNIT_code='0' NARRATION='" + CreditNarration + "' AMOUNT='" + Bill_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + PurchaseAc + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='PR' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='7' tranid='0'/>";
                    Order_Code = Order_Code + 1;
                    //Gledger_values = Gledger_values + ",('PR','','" + Doc_No + "','" + doc_date + "','" + PurchaseAc + "','0','','" + Bill_Amount + "', " +
                    //                                           " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','D','" + Ac_Code + "',0,'" + Branch_Code + "','PR','" + Doc_No + "'," +
                    //                                           " case when 0='" + Purcacid + "' then null else '" + Purcacid + "' end,'0','7','0')";

                    XML = XML + "<Ledger TRAN_TYPE='PR' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + PurchaseAc + "' " +
                                                           "UNIT_code='0' NARRATION='" + DebitNarration + "' AMOUNT='" + subTotal + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + Ac_Code + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='PR' SORT_NO='" + Doc_No + "' ac='" + Purcacid + "' vc='0' progid='7' tranid='0'/>";
                }
                if (CGSTAmount > 0) {
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='PR' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["ReturnSaleCGST"] %>' + "' " +
                                                      "UNIT_code='0' NARRATION='" + CreditNarration + "' AMOUNT='" + CGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='PR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["Return_Sale_CGST_Acid"] %>' + "' vc='0' progid='7' tranid='0'/>";
                }
                if (SGSTAmount > 0) {
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='PR' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["ReturnSaleSGST"] %>' + "' " +
                                                      "UNIT_code='0' NARRATION='" + CreditNarration + "' AMOUNT='" + SGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='PR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["Return_Sale_SGST_Acid"] %>' + "' vc='0' progid='7' tranid='0'/>";
                }
                if (IGSTAmount > 0) {
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='PR' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["ReturnSaleIGST"] %>' + "' " +
                                                      "UNIT_code='0' NARRATION='" + CreditNarration + "' AMOUNT='" + IGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='PR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["Return_Sale_IGST_Acid"] %>' + "' vc='0' progid='7' tranid='0'/>";
                }
                if (TCS_Amt > 0) {

                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='PR' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                          "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                          "SORT_TYPE='PR' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='7' tranid='0'/>";

                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='PR' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["PurchaseTCSAc"] %>' + "' " +
                                                      "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='PR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["PurchaseTCSAcid"] %>' + "' vc='0' progid='7' tranid='0'/>";
                }
                if (TDS_Amt > 0) {

                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='PR' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                          "UNIT_code='0' NARRATION='" + TDSNarration + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                          "SORT_TYPE='PR' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='7' tranid='0'/>";

                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='PR' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["SaleTDSAc"] %>' + "' " +
                                                      "UNIT_code='0' NARRATION='" + TDSNarration + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='PR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleTDSacid"] %>' + "' vc='0' progid='7' tranid='0'/>";
                }
                //Gledger_Insert = "insert into nt_1_gledger (TRAN_TYPE,CASHCREDIT,DOC_NO,DOC_DATE,AC_CODE,UNIT_code,NARRATION,AMOUNT,TENDER_ID,TENDER_ID_DETAIL,VOUCHER_ID,COMPANY_CODE," +
                //         " YEAR_CODE,ORDER_CODE,DRCR,DRCR_HEAD,ADJUSTED_AMOUNT,Branch_Code,SORT_TYPE,SORT_NO,ac,vc,progid,tranid) values " + Gledger_values + "";

                XML = XML + "<MultipleDetail Tran_Type='PR' DOC_NO='" + Doc_No + "' detail_Id='1' " +
                       "Bill_No='" + PURCNO + "' Bill_Tran_Type='" + PurcTranType + "' Party_Code='" + Ac_Code + "' pc='" + ac +
                                      "' Bill_Receipt='0' Value='" + TCS_Net_Payable + "' Adj_Value='0' Narration='Purchase Return' " +
                       "Bill_Year_Code='" + purcyearcode + "' Bill_Auto_Id='" + saleid + "' Year_Code='" + Year_Code +
                       "' Doc_Date='" + doc_date + "' OnAc='0.00' AcadjAmt='0' AcAdjAc='0'/>";

                XML = XML + "</PurchaseReturn></ROOT>";

                $.ajax({
                    type: "POST",
                    url: "../xmlExecuteDMLQry.asmx/ExecuteXMLQry",
                    data: '{XML:"' + XML + '",status:"' + status + '",spname:"' + spname + '"}',
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
                        window.open('../Inword/pgeSugarPurchaseReturnForGST.aspx?prid=' + id + '&Action=1', "_self");

                    }

                }
            }
            catch (exx) {
                alert(exx)
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
            <asp:Label ID="label1" runat="server" Text="   Sale Return   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px;">
        <img height="40%" width="40%" src="../Images/ajax-loader3.gif" />
    </div>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfTranType" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdHelpyearcode" runat="server" />
            <asp:HiddenField ID="hdSaleID" runat="server" />
            <asp:HiddenField ID="hdnfsaleid" runat="server" />
            <asp:HiddenField ID="hdnfac" runat="server" />
            <asp:HiddenField ID="hdnfmc" runat="server" />
            <asp:HiddenField ID="hdnfuc" runat="server" />
            <asp:HiddenField ID="hdnfbc" runat="server" />
            <asp:HiddenField ID="hdnfbillid" runat="server" />
            <asp:HiddenField ID="hdnfPurchaseAc" runat="server" />
            <asp:HiddenField ID="hdnfpurcacid" runat="server" />
            <asp:HiddenField ID="hdnfdocno" runat="server" />
            <asp:HiddenField ID="hdnfid" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />

            <asp:HiddenField ID="hdnfMillshort" runat="server" />
            <asp:HiddenField ID="hdnfBrokerShort" runat="server" />
            <asp:HiddenField ID="hdnfPartyShort" runat="server" />

            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table width="80%" align="left">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="save" OnClick="btnAdd_Click" />
                            &nbsp;
                        <asp:Button ID="btnSave" OnClientClick="if (!Validate()) return false;" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            TabIndex="37" Height="24px" ValidationGroup="add" OnClick="btnSave_Click" />
                            &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="save" OnClick="btnEdit_Click" />
                            &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" />
                            &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" />&nbsp;<asp:Button
                                runat="server" ID="btnPrintSaleBill" Text="Print" CssClass="btnHelp" Width="80px"
                                Height="24px" OnClientClick="SB();" Visible="false" />
                            <asp:Button runat="server" ID="btnPrintPurchaseReturn" Text="Print" CssClass="btnHelp"
                                Width="80px" Height="24px" OnClientClick="SB();" />
                            &nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                            TabIndex="38" Height="24px" ValidationGroup="save" OnClientClick="Back()" />
                            &nbsp;
                             <asp:Button runat="server" ID="btnGenEinvoice" Text="Generate EInvoice" CssClass="btnHelp"
                                 Width="120px" Height="24px" OnClientClick="EInovice();" />
                        </td>

                    </tr>
                </table>
                <table width="90%" align="left" cellspacing="4">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                        <tr>
                            <td align="right">Change No:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                    TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                                &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                    Font-Size="Small" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Bill No.:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtDOC_NO" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtDOC_NO_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtdoc_no" runat="server" Text="..." Width="80px" OnClick="btntxtDOC_NO_Click"
                                    CssClass="btnHelp" Height="24px" />
                                <asp:Label ID="lblDoc_Id" runat="server" CssClass="lblName" Visible="false"></asp:Label>
                            </td>
                            <td align="right" style="width: 10%;">Purc No:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtPURCNO" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                    Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtPURCNO_TextChanged"
                                    Height="24px" onKeyDown="PurcNo(event);"></asp:TextBox>
                                <asp:Button ID="btntxtPURCNO" runat="server" Text="..." OnClick="btntxtPURCNO_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />&nbsp;<asp:Label runat="server" ID="lblTranType"
                                        ForeColor="Yellow" Font-Bold="true"></asp:Label>
                                Year:
                                <asp:Label runat="server" ID="lblyearcode" ForeColor="Yellow" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="right" style="width: 10%;">Date:
                            </td>
                            <td align="left" style="width: 12%;">
                                <asp:TextBox ID="txtDOC_DATE" runat="Server" CssClass="txt" TabIndex="2" Width="100px"
                                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                    Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtDOC_DATE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                    Width="25px" Height="15px" />
                                <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtDOC_DATE"
                                    PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">From:
                            </td>
                            <td align="left" colspan="4" style="width: 10%;">
                                <asp:TextBox ID="txtAC_CODE" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                    Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtAC_CODE_TextChanged"
                                    Height="24px" onKeyDown="Ac_Code(event);"></asp:TextBox>
                                <asp:Button ID="btntxtAC_CODE" runat="server" Text="..." OnClick="btntxtAC_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="LblPartyname" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Bill To:
                            </td>
                            <td align="left" colspan="4" style="width: 10%;">
                                <asp:TextBox ID="txtBillTo" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                    Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtBillTo_TextChanged"
                                    Height="24px" onKeyDown="BillTo(event);"></asp:TextBox>
                                <asp:Button ID="btntxtBillTo" runat="server" Text="..." OnClick="btntxtBillTo_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblBillTo" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Unit:
                            </td>
                            <td align="left" colspan="4" style="width: 10%;">
                                <asp:TextBox ID="txtUnit_Code" runat="Server" CssClass="txt" TabIndex="4" Width="80px"
                                    Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtUnit_Code_TextChanged" onKeyDown="UnitCode(event);"></asp:TextBox>
                                <asp:Button ID="btntxtUnitcode" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                    Width="20px" OnClick="btntxtUnitcode_Click" />
                                <asp:Label ID="lblUnitName" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Mill:
                            </td>
                            <td align="left" colspan="4" style="width: 10%;">
                                <asp:TextBox ID="txtMILL_CODE" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                    Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtMILL_CODE_TextChanged"
                                    Height="24px" onKeyDown="MillCode(event);"></asp:TextBox>
                                <asp:Button ID="btntxtMILL_CODE" runat="server" Text="..." OnClick="btntxtMILL_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="LBLMILLNAME" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">From:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtFROM_STATION" runat="Server" CssClass="txt" TabIndex="6" Width="200px"
                                    Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtFROM_STATION_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <td align="right">To:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTO_STATION" runat="Server" CssClass="txt" TabIndex="7" Width="200px"
                                        Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtTO_STATION_TextChanged"
                                        Height="24px"></asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td align="right">Lorry No:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLORRYNO" runat="Server" CssClass="txt" TabIndex="8" Width="200px"
                                    Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtLORRYNO_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <td align="right">Wear House:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWEARHOUSE" runat="Server" CssClass="txt" TabIndex="9" Width="200px"
                                        Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtWEARHOUSE_TextChanged"
                                        Height="24px"></asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Broker:
                            </td>
                            <td align="left" colspan="3" style="width: 10%;">
                                <asp:TextBox ID="txtBROKER" runat="Server" CssClass="txt" TabIndex="10" Width="80px"
                                    Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBROKER_TextChanged"
                                    Height="24px" onKeyDown="Broker(event);"></asp:TextBox>
                                <asp:Button ID="btntxtBROKER" runat="server" Text="..." OnClick="btntxtBROKER_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="LBLBROKERNAME" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">GST Rate Code
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txtGSTRateCode" runat="Server" CssClass="txt" TabIndex="11" Width="80px"
                                    Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtGSTRateCode_TextChanged"
                                    Height="24px" onKeyDown="GSTRateCode(event);"></asp:TextBox>
                                <asp:Button ID="btntxtGSTRateCode" runat="server" Text="..." OnClick="btntxtGSTRateCode_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblGSTRateName" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="right">Bill No.:
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txtbillNo" runat="Server" CssClass="txt" TabIndex="12" Width="200px"
                                    Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtbillNo_TextChanged"
                                    Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                </table>
                <fieldset style="border-top: 3px dotted rgb(131, 127, 130); border-radius: 3px; width: 172%; margin-left: -1160px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 5px;">
                    <legend style="text-align: left;">
                        <h2 style="color: Black; text-align: left;" font-names="verdana" font-size="large">Detail Section</h2>
                    </legend>
                </fieldset>
                <asp:Panel ID="Panel2" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                    Font-Size="Small" Style="margin-top: 0px; z-index: 100;">
                    <table width="100%" align="center" cellspacing="5">

                        <tr>
                            <td align="left">ID:
                           
                                <asp:Label ID="lblID" runat="server"></asp:Label>
                                <asp:Label ID="lblNo" runat="server" ForeColor="Azure"></asp:Label>
                                Item:
                           
                                <asp:TextBox ID="txtITEM_CODE" runat="Server" CssClass="txt" TabIndex="13" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtITEM_CODE_TextChanged" onKeyDown="Item(event);"></asp:TextBox>
                                <asp:Button ID="btntxtITEM_CODE" runat="server" Text="..." OnClick="btntxtITEM_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="LBLITEMNAME" runat="server" CssClass="lblName"></asp:Label>
                                Quantal:
                            
                                <asp:TextBox ID="txtQUANTAL" runat="Server" CssClass="txt" TabIndex="14" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtQUANTAL_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtQUANTAL" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtQUANTAL">
                                </ajax1:FilteredTextBoxExtender>
                                Packing:
                           
                                <asp:TextBox ID="txtPACKING" runat="Server" CssClass="txt" TabIndex="15" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtPACKING_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtPACKING" runat="server" FilterType="Numbers"
                                    TargetControlID="txtPACKING">
                                </ajax1:FilteredTextBoxExtender>
                                Bags:
                           
                                <asp:TextBox ID="txtBAGS" runat="Server" ReadOnly="true" CssClass="txt" TabIndex="16"
                                    Height="24px" Width="80px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtBAGS_TextChanged"></asp:TextBox>
                                Rate:
                           
                                <asp:TextBox ID="txtRATE" runat="Server" CssClass="txt" TabIndex="17" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtRATE_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtRATE" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtRATE">
                                </ajax1:FilteredTextBoxExtender>
                                Item Amount:
                           
                                <asp:TextBox ID="txtITEMAMOUNT" runat="Server" CssClass="txt" TabIndex="18" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" ReadOnly="true"
                                    OnTextChanged="txtITEMAMOUNT_TextChanged"></asp:TextBox>
                                Narration:
                            
                                <asp:TextBox ID="txtITEM_NARRATION" runat="Server" CssClass="txt" TabIndex="19" Width="350px"
                                    TextMode="MultiLine" Height="50px" Style="text-align: left;" AutoPostBack="True"
                                    OnTextChanged="txtITEM_NARRATION_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                    OnClick="btnAdddetails_Click" TabIndex="20" Height="24px" />
                                <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp"
                                    Width="80px" OnClick="btnClosedetails_Click" TabIndex="21" Height="24px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table width="90%" align="left" cellspacing="4">
                    <tr>
                        <td align="left" colspan="5" style="width: 100%;">
                            <div style="width: 100%; position: relative; margin-top: -30px;">
                                <asp:UpdatePanel ID="upGrid" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlgrdDetail" runat="server" align="left" ScrollBars="Both" Height="180px"
                                            Width="1000px" BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true"
                                            Font-Names="Verdana" Font-Size="11px" BackColor="SeaShell" Style="margin-left: 30px; float: left;">
                                            <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" HeaderStyle-BackColor="#397CBB"
                                                HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="100%"
                                                OnRowCommand="grdDetail_RowCommand" CellPadding="5" CellSpacing="5" OnRowDataBound="grdDetail_RowDataBound"
                                                Style="table-layout: fixed; float: left">
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
                            <div style="margin-top: 200px; margin-left: 70px;">
                                Net Qntl:<asp:TextBox ID="txtNETQNTL" runat="Server" CssClass="txt" ReadOnly="true"
                                    Width="120px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtNETQNTL_TextChanged"
                                    Height="24px" onKeyDown="save(event);" TabIndex="22"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtNETQNTL" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtNETQNTL">
                                </ajax1:FilteredTextBoxExtender>
                                Due Days:
                                    <asp:TextBox ID="txtDUE_DAYS" runat="Server" CssClass="txt" Width="120px"
                                        Style="text-align: right;" AutoPostBack="True" TabIndex="23" OnTextChanged="txtDUE_DAYS_TextChanged"
                                        Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                    TargetControlID="txtDUE_DAYS">
                                </ajax1:FilteredTextBoxExtender>
                                EInvoice No:
                                <asp:TextBox ID="txteinvoiceno" runat="Server" CssClass="txt" TabIndex="24" Width="120px"
                                    Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                                ACKNo:
                                <asp:TextBox ID="txtackno" runat="Server" CssClass="txt" TabIndex="25" Width="150px"
                                    Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                            </div>
                        </td>
                        <td style="width: 100%;" align="left">
                            <table width="100%" cellspacing="4" cellpadding="3">
                                <tr>
                                    <td align="left" style="width: 50%;">Subtotal:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSUBTOTAL" runat="Server" CssClass="txt" ReadOnly="true" Width="140px"
                                            Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtSUBTOTAL_TextChanged"
                                            Height="24px" TabIndex="26"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtSUBTOTAL">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">CGST%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtCGSTRate" runat="Server" CssClass="txt" Width="52px" TabIndex="27"
                                            Style="text-align: right;" Height="24px" AutoPostBack="true" OnTextChanged="txtLCST_TextChanged"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtLCST" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtCGSTRate" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtCGSTAmount" runat="Server" CssClass="txt" Width="80px" ReadOnly="true"
                                            Style="text-align: right;" Height="24px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="">SGST%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtSGSTRate" runat="Server" CssClass="txt" Width="50px" TabIndex="28"
                                            AutoPostBack="true" OnTextChanged="txtLSGST_TextChanged" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtLSGST" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtSGSTRate" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtSGSTAmount" runat="Server" CssClass="txt" Width="82px" ReadOnly="true"
                                            Style="text-align: right;" Height="24px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="">IGST%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtIGSTRate" runat="Server" CssClass="txt" Width="52px" AutoPostBack="true"
                                            OnTextChanged="txtLIGST_TextChanged" TabIndex="29" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtLIGST" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtIGSTRate" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtIGSTAmount" runat="Server" CssClass="txt" Width="80px" ReadOnly="true"
                                            Style="text-align: right;" Height="24px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Less Frt. Rs.
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtLESS_FRT_RATE" runat="Server" CssClass="txt" TabIndex="30" Width="52px"
                                            Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtLESS_FRT_RATE_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtLESS_FRT_RATE" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtLESS_FRT_RATE">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtFREIGHT" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                            OnTextChanged="txtFREIGHT_TextChanged" ReadOnly="true" Style="text-align: right;"
                                            Width="80px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtFREIGHT" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtFREIGHT" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Bank Commission
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBANK_COMMISSION" runat="Server" CssClass="txt" TabIndex="31"
                                            Width="140px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtBANK_COMMISSION_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtBANK_COMMISSION">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Other +/-:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtOTHER_AMT" runat="Server" AutoPostBack="True" CssClass="txt"
                                            Height="24px" OnTextChanged="txtOTHER_AMT_TextChanged" Style="text-align: right;"
                                            TabIndex="32" Width="140px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtOTHER_AMT" ValidChars=".,-">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Cash Advance:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCASH_ADVANCE" runat="Server" CssClass="txt" TabIndex="33" Width="140px"
                                            Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtCASH_ADVANCE_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtCASH_ADVANCE">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Bill Amount:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBILL_AMOUNT" runat="Server" CssClass="txt" ReadOnly="true" TabIndex="34"
                                            Width="140px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtBILL_AMOUNT_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtBILL_AMOUNT">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">TCS%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtTCSRate" runat="Server" CssClass="txt" Width="52px" AutoPostBack="true"
                                            OnTextChanged="txtTCSRate_TextChanged" TabIndex="35" Style="text-align: right;"
                                            Height="24px" Visible="true"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTCSRate">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtTCSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                            OnTextChanged="txtTCSAmt_TextChanged" ReadOnly="false" Style="text-align: right;"
                                            Width="80px" Visible="true"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtTCSAmt" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                      <tr>
                                    <td align="left">TDS%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtTDS" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                            OnTextChanged="txtTDS_TextChanged" TabIndex="53" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTDS">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtTDSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                            TabIndex="54" OnTextChanged="txtTDSAmt_TextChanged" ReadOnly="false" Style="text-align: right;"
                                            Width="80px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtTDSAmt" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Net Payable:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtTCSNet_Payable" runat="Server" CssClass="txt" ReadOnly="true"
                                            TabIndex="36" Width="140px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtTCSNet_Payable_TextChanged"
                                            Height="24px" Visible="true"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTCSNet_Payable">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--<tr>
                            <td align="center" style="width: 10%;">
                                Subtotal:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtSUBTOTAL" runat="Server" CssClass="txt" ReadOnly="true" TabIndex="22"
                                    Width="120px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtSUBTOTAL_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtSUBTOTAL" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtSUBTOTAL">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                            <td align="center" style="width: 10%;">
                                Bank Commission:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtBANK_COMMISSION" runat="Server" CssClass="txt" TabIndex="27"
                                    Width="120px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtBANK_COMMISSION_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtBANK_COMMISSION" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtBANK_COMMISSION">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 10%;">
                                Due Days:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtDUE_DAYS" runat="Server" CssClass="txt" TabIndex="23" Width="120px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtDUE_DAYS_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="filtertxtDUE_DAYS" runat="server" FilterType="Numbers"
                                    TargetControlID="txtDUE_DAYS">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                            <td align="center" style="width: 10%;">
                                Other:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtOTHER_AMT" runat="Server" CssClass="txt" TabIndex="28" Width="120px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtOTHER_AMT_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtOTHER_AMT" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtOTHER_AMT">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 10%;">
                                Cash Advance:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtCASH_ADVANCE" runat="Server" CssClass="txt" TabIndex="24" Width="120px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtCASH_ADVANCE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtCASH_ADVANCE" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtCASH_ADVANCE">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                            <td align="center" style="width: 10%;">
                                Bill Amount:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtBILL_AMOUNT" runat="Server" CssClass="txt" ReadOnly="true" TabIndex="29"
                                    Width="120px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtBILL_AMOUNT_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtBILL_AMOUNT" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtBILL_AMOUNT">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>--%>
                </table>

            </asp:Panel>

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
            <%--  <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="GhostWhite" Width="955px"
                BorderColor="Teal" BorderWidth="1px" Height="320px" BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
            </asp:Panel>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
