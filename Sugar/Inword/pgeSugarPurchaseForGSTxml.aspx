<%@ Page Title="Sugar Purchase Xml" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeSugarPurchaseForGSTxml.aspx.cs" Inherits="Sugar_pgeSugarPurchaseForGSTxml" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Script/jquery-1.4.2.js"></script>
    <link href="../../CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../../JQuery/jquery-1.4.1.js" type="text/javascript"></script>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../JQuery/jquery.keynavigation.js" type="text/javascript"></script>
    <link type="text/css" href="../../menu/menu.css" rel="stylesheet" />
    <script type="text/javascript" src="../menu/jquery.js"></script>
    <script type="text/javascript" src="../../menu/menu.js"></script>
    <script type="text/javascript" src="../JS/DateValidation.js"> </script>
    <script src="../../JS/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../Scripts/selectfirstrow.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        debugger;
        document.addEventListener('keyup', function (event) {
            if (event.defaultPrevented) {
                return;
            }

            var key = event.key || event.keyCode;

            if (key === 'Escape' || key === 'Esc' || key === 27) {
                //                doWhateverYouWantNowThatYourKeyWasHit();

                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                if (hdnfClosePopupValue == "txtAC_CODE") {
                    document.getElementById("<%=txtAC_CODE.ClientID %>").focus();
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
                if (hdnfClosePopupValue == "txtBrand_Code") {
                    document.getElementById("<%=txtBrand_Code.ClientID %>").focus();
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
                    document.getElementById("<%=txtDOC_NO.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtITEM_CODE") {
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLITEMNAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%= Hdnfitm.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtGSTRateCode") {
                    document.getElementById("<%=txtGSTRateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblGSTRateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGSTRateCode.ClientID %>").focus();
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
        function PURCNO(e) {

            if (e.keyCode == 112) {

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
        function Brand_Code(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtBrand_Code.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var buss = $("#<%=txtBrand_Code.ClientID %>").val();
                buss = "0" + buss;
                $("#<%=txtBrand_Code.ClientID %>").val(buss);
                __doPostBack("txtBrand_Code", "TextChanged");
            }
        }
        debugger;
        function Ac_Code(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                // document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "txtAC_CODE";
                $("#<%=btntxtAC_CODE.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtAC_CODE.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtAC_CODE.ClientID %>").val(unit);
                __doPostBack("txtAC_CODE", "TextChanged");

            }

        }
        function Unit(e) {

            if (e.keyCode == 112) {

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
        function Mill(e) {

            if (e.keyCode == 112) {

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

            if (e.keyCode == 112) {

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
        function GSTRate(e) {

            if (e.keyCode == 112) {

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
        function DueDays(e) {
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }
        }
        function Item(e) {

            if (e.keyCode == 112) {

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
                $("#<%=txtEway_Bill_No.ClientID %>").focus();
            }

        }
    </script>
    <script type="text/javascript">
        function SB() {
            debugger;
            var billno = document.getElementById('<%=txtDOC_NO.ClientID %>').value;
            window.open('../Report/rptpurchasebill.aspx?billno=' + billno)
        }
    </script>
    <script type="text/javascript">
        function Back() {

            //alert(td);
            window.open('../Inword/PgePurchaseHeadUtility.aspx', '_self');
        }
        function PUrchaseOPen(PurcID) {
            var Action = 1;
            window.open('../Inword/pgeSugarPurchaseForGSTxml.aspx?purchaseid=' + PurcID + '&Action=' + Action, "_self");
        }

    </script>
    <script type="text/javascript">
        function DeleteConform() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= hdnfpurchasedoc.ClientID %>").value;
            var purcid = document.getElementById("<%= hdnfpurchaseid.ClientID %>").value;

            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';

            var XML = "<ROOT><PurchaseHead doc_no='" + DocNo + "' purchaseid='" + purcid + "' Company_Code='" + Company_Code + "' " +
                "Year_Code='" + Year_Code + "'></PurchaseHead></ROOT>";
            var spname = "PurchaseBill";
            var status = document.getElementById("<%= btnDelete.ClientID %>").value;
            ProcessXML(XML, status, spname);
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
          //  $("#<%=txtmill_inv_date.ClientID %>").text(d);
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
            if ($("#<%=txtDOC_DATE.ClientID %>").val() == "") {
                $("#<%=txtDOC_DATE.ClientID %>").focus();
                $("#loader").hide();
                return false;

            }

            if ($("#<%=txtMILL_CODE.ClientID %>").val() == "" || $("#<%=txtMILL_CODE.ClientID %>").val() == "0") {
                $("#<%=txtMILL_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;

            }

            if ($("#<%=txtGSTRateCode.ClientID %>").val() == "" || $("#<%=txtGSTRateCode.ClientID %>").val() == "0") {
                $("#<%=txtGSTRateCode.ClientID %>").focus();
                $("#loader").hide();
                return false;

            }
            if ($("#<%=txtmill_inv_date.ClientID %>").val() == "") {
                $("#<%=txtmill_inv_date.ClientID %>").focus();
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
        debugger;
        function pagevalidation() {
            debugger;

            $("#loader").show();
            var Doc_No = 0, purchaseid = 0, purchasedetailid = 0, GId = 0;
            var spname = "PurchaseBill";
            var XML = "<ROOT>";
            var status = document.getElementById("<%= btnSave.ClientID %>").value;
            if (status == "Update") {
                Doc_No = document.getElementById("<%= hdnfpurchasedoc.ClientID %>").value;
                purchaseid = document.getElementById("<%= hdnfpurchaseid.ClientID %>").value;
            }
            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            var grdrow = gridView.getElementsByTagName("tr");
            var purchase_Id = $("#<%=lblPurchase_Id.ClientID %>").text();
            var doc_no = $("#<%=txtDOC_NO.ClientID %>").val();

            var Tran_Type = "PS";
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';
            var Branch_Code = '<%= Session["Branch_Code"] %>';

            var PURCNO = $("#<%=txtPURCNO.ClientID %>").val() == "" ? 0 : $("#<%=txtPURCNO.ClientID %>").val();
            var d = $("#<%=txtDOC_DATE.ClientID %>").val();
            var doc_date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
            var d = $("#<%=txtmill_inv_date.ClientID %>").val();
            var mill_inv_date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

            var Ac_Code = $("#<%=txtAC_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtAC_CODE.ClientID %>").val();
            var Unit_Code = $("#<%=txtUnit_Code.ClientID %>").val() == "" ? 0 : $("#<%=txtUnit_Code.ClientID %>").val();
            var mill_code = $("#<%=txtMILL_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtMILL_CODE.ClientID %>").val();
            var FROM_STATION = $("#<%=txtFROM_STATION.ClientID %>").val();
            var TO_STATION = $("#<%=txtTO_STATION.ClientID %>").val();
            var LORRYNO = $("#<%=txtLORRYNO.ClientID %>").val();
            var BROKER = $("#<%=txtBROKER.ClientID %>").val() == "" ? 0 : $("#<%=txtBROKER.ClientID %>").val();
            var wearhouse = $("#<%=txtWEARHOUSE.ClientID %>").val();
            var subTotal = parseFloat($("#<%=txtSUBTOTAL.ClientID %>").val() == "" ? 0.00 : $("#<%=txtSUBTOTAL.ClientID %>").val());
            var LESS_FRT_RATE = parseFloat($("#<%=txtLESS_FRT_RATE.ClientID %>").val() == "" ? 0.00 : $("#<%=txtLESS_FRT_RATE.ClientID %>").val());
            var freight = parseFloat($("#<%=txtFREIGHT.ClientID %>").val() == "" ? 0.00 : $("#<%=txtFREIGHT.ClientID %>").val());
            var cash_advance = parseFloat($("#<%=txtCASH_ADVANCE.ClientID %>").val() == "" ? 0.00 : $("#<%=txtCASH_ADVANCE.ClientID %>").val());
            var bank_commission = parseFloat($("#<%= txtBANK_COMMISSION.ClientID %>").val() == "" ? 0.00 : $("#<%= txtBANK_COMMISSION.ClientID %>").val());
            var OTHER_AMT = parseFloat($("#<%=txtOTHER_AMT.ClientID %>").val() == "" ? 0.00 : $("#<%=txtOTHER_AMT.ClientID %>").val());
            var Bill_Amount = parseFloat($("#<%=txtBILL_AMOUNT.ClientID %>").val() == "" ? 0.00 : $("#<%=txtBILL_AMOUNT.ClientID %>").val());
            var Due_Days = $("#<%=txtDUE_DAYS.ClientID %>").val() == "" ? 0 : $("#<%=txtDUE_DAYS.ClientID %>").val();
            var NETQNTL = $("#<%=txtNETQNTL.ClientID %>").val() == "" ? 0 : $("#<%=txtNETQNTL.ClientID %>").val();
            var Bill_No = $("#<%=txtbillNo.ClientID %>").val();
            var CGSTRate = $("#<%=txtCGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtCGSTRate.ClientID %>").val();
            var CGSTAmount = $("#<%=txtCGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtCGSTAmount.ClientID %>").val();
            var IGSTRate = $("#<%=txtIGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtIGSTRate.ClientID %>").val();
            var IGSTAmount = $("#<%=txtIGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtIGSTAmount.ClientID %>").val();
            var SGSTRate = $("#<%=txtSGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtSGSTRate.ClientID %>").val();
            var SGSTAmount = $("#<%=txtSGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtSGSTAmount.ClientID %>").val();
            var GstRateCode = $("#<%=txtGSTRateCode.ClientID %>").val() == "" ? 0 : $("#<%=txtGSTRateCode.ClientID %>").val();
            var EWay_Bill_No = $("#<%=txtEway_Bill_No.ClientID %>").val();
            var USER = '<%= Session["user"] %>';
            var SelfBal = "Y";
            var Grade = $("#<%=txtgrade.ClientID %>").val();
            var BrokerShortName = document.getElementById("<%= hdnfBrokerShort.ClientID %>").value;
            var PartyShortName = $("#<%=LblPartyname.ClientID %>").text();
            var MillShortName = document.getElementById("<%= hdnfMillshort.ClientID %>").value;
            var PurchaseAc = document.getElementById("<%= hdnfPurchaseAc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfPurchaseAc.ClientID %>").value;
            var PurchaseAcid = document.getElementById("<%= hdnfPurchaseAcid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfPurchaseAcid.ClientID %>").value;
            var TCS_Rate = $("#<%=txtTCSRate.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSRate.ClientID %>").val();
            var TCS_Amt = $("#<%=txtTCSAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSAmt.ClientID %>").val();
            var TCS_Net_Payable = $("#<%=txtTCSNet_Payable.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSNet_Payable.ClientID %>").val();

            var TDS_Rate = $("#<%=txtTDS.ClientID %>").val() == "" ? 0 : $("#<%=txtTDS.ClientID %>").val();
            var TDS_Amt = $("#<%=txtTDSAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtTDSAmt.ClientID %>").val();
            var Retail_Stock = $("#<%=drpreatilstock.ClientID %>").val();
            var mc = document.getElementById("<%= hdnfmc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfmc.ClientID %>").value;
            var ac = document.getElementById("<%= hdnfac.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfac.ClientID %>").value;
            var uc = document.getElementById("<%= hdnfuc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfuc.ClientID %>").value;
            var bk = document.getElementById("<%= hdnfbk.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbk.ClientID %>").value;
            var Purcid = document.getElementById("<%= hdnfpurcid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpurcid.ClientID %>").value;
            var PurchaseTDSAc = '<%= Session["PurchaseTDSAc"] %>';

            if (mc == "" || mc == "&nbsp;") {
                mc = 0;
            }
            if (ac == "" || ac == "&nbsp;") {
                ac = 0;
            }
            if (uc == "" || uc == "&nbsp;") {
                uc = 0;
            }
            if (bk == "" || bk == "&nbsp;") {
                bk = 0;
            }
            if (Purcid == "" || Purcid == "&nbsp;") {
                Purcid = 0;
            }

            var DOCNO = "";
            if (status == "Save") {
                DOCNO = "Tran_Type='PS'";
            }
            else {
                DOCNO = "doc_no='" + Doc_No + "' Tran_Type='PS'";
            }
            var XML = XML + "<PurchaseHead " + DOCNO + " PURCNO='" + PURCNO + "' doc_date='" + doc_date + "' Ac_Code ='" + Ac_Code + "' Unit_Code='" + Unit_Code + "' " +
               "mill_code='" + mill_code + "' FROM_STATION='" + FROM_STATION + "' TO_STATION='" + TO_STATION + "' LORRYNO='" + LORRYNO + "' BROKER='" + BROKER + "' " +
               "wearhouse='" + wearhouse + "' subTotal='" + subTotal + "' LESS_FRT_RATE='" + LESS_FRT_RATE + "' freight='" + freight + "' " +
               "cash_advance='" + cash_advance + "' bank_commission='" + bank_commission + "' OTHER_AMT='" + OTHER_AMT + "' Bill_Amount='" + Bill_Amount + "' " +
               "Due_Days='" + Due_Days + "' NETQNTL='" + NETQNTL + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
               "Created_By='" + USER + "' Modified_By='" + USER + "' Bill_No='" + Bill_No + "' GstRateCode='" + GstRateCode + "' CGSTRate='" + CGSTRate + "' CGSTAmount='" + CGSTAmount + "' SGSTRate='" + SGSTRate + "' " +
               "SGSTAmount='" + SGSTAmount + "' IGSTRate='" + IGSTRate + "' IGSTAmount='" + IGSTAmount + "' " +
               "EWay_Bill_No='" + EWay_Bill_No + "' purchaseid='" + purchaseid + "' ac='" + ac + "' uc='" + uc + "' mc='" + mc + "' bk='" + bk + "' grade='" + Grade + "' mill_inv_date='" + mill_inv_date + "' " +
               "Purcid='" + Purcid + "' SelfBal='" + SelfBal + "' TCS_Rate='" + TCS_Rate + "' TCS_Amt='" + TCS_Amt + "' TCS_Net_Payable='" + TCS_Net_Payable + "' TDS_Rate='" + TDS_Rate + "' TDS_Amt='" + TDS_Amt + "' Retail_Stock='" + Retail_Stock + "'>";
            //Detail Calculation
            var ddid = purchasedetailid;
            for (var i = 1; i < grdrow.length; i++) {
                var ID = gridView.rows[i].cells[2].innerHTML;
                var item_code = gridView.rows[i].cells[3].innerHTML;
                var Brand_Code = gridView.rows[i].cells[5].innerHTML;
                var Narration = gridView.rows[i].cells[7].innerHTML;
                if (Narration == "&nbsp;") {
                    Narration = "";
                }
                var Quntal = gridView.rows[i].cells[8].innerHTML;
                var Packing = gridView.rows[i].cells[9].innerHTML;
                var Bags = gridView.rows[i].cells[10].innerHTML;
                var rate = gridView.rows[i].cells[11].innerHTML;
                var item_amount = gridView.rows[i].cells[12].innerHTML;
                var ic = gridView.rows[i].cells[16].innerHTML;

                if (gridView.rows[i].cells[14].innerHTML == "A") {

                    XML = XML + "<PurchaseDetailInsert doc_no='" + doc_no + "' detail_id='" + ID + "' Tran_Type='PS' item_code='" + item_code + "' narration='" + Narration + "' Quantal='" + Quntal + "' packing='" + Packing + "' " +
                    "bags='" + Bags + "' rate='" + rate + "' item_Amount='" + item_amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' " +
                    "Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' purchasedetailid='"
                    + ddid + "' purchaseid='" + purchaseid + "' ic='" + ic + "' Brand_Code='" + Brand_Code + "'/>";
                    ddid = parseInt(ddid) + 1;
                }
                else if (gridView.rows[i].cells[14].innerHTML == "U") {
                    var purcID = gridView.rows[i].cells[13].innerHTML;
                    XML = XML + "<PurchaseDetail doc_no='" + doc_no + "' detail_id='" + ID + "' Tran_Type='PS' item_code='" + item_code + "' narration='" + Narration + "' Quantal='" + Quntal + "' packing='" + Packing + "' " +
                    "bags='" + Bags + "' rate='" + rate + "' item_Amount='" + item_amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' " +
                    "Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' purchasedetailid='" + purcID +
                    "' purchaseid='" + purchaseid + "' ic='" + ic + "' Brand_Code='" + Brand_Code + "'/>";
                }
                else if (gridView.rows[i].cells[14].innerHTML == "D") {
                    var purcID = gridView.rows[i].cells[13].innerHTML;
                    XML = XML + "<PurchaseDetailDelete doc_no='" + doc_no + "' detail_id='" + ID + "' Tran_Type='PS' item_code='" + item_code + "' narration='" + Narration + "' Quantal='" + Quntal + "' packing='" + Packing + "' " +
                    "bags='" + Bags + "' rate='" + rate + "' item_Amount='" + item_amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' " +
                    "Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' purchasedetailid='" + purcID +
                    "' purchaseid='" + purchaseid + "' ic='" + ic + "' Brand_Code='" + Brand_Code + "'/>";
                }
            }

            // Gledger Effect 

            //var CreditNarration = BrokerShortName + " " + Quntal + "L:" + LORRYNO + " R:" + gridView.rows[1].cells[9].innerHTML;
            //var DebitNarration = MillShortName + " " + Quntal + "L:" + LORRYNO + " R:" + gridView.rows[1].cells[9].innerHTML;
            var CreditNarration = MillShortName + " L:" + LORRYNO + " G:" + Grade + " " + Quntal + " R:" + gridView.rows[1].cells[9].innerHTML + " Bill No:" + Bill_No;
            var DebitNarration = MillShortName + " " + PartyShortName + "L:" + LORRYNO + "G:" + Grade + " " + Quntal + " R:" + gridView.rows[1].cells[9].innerHTML + " Bill No:" + Bill_No;
            var TCSNarration = 'TCS' + PartyShortName + " " + Doc_No + " Bill No:" + Bill_No;
            var TDSNarration = 'TDS' + PartyShortName + " " + Doc_No + " Bill No:" + Bill_No;
            var Order_Code = 1;

            if (Bill_Amount > 0) {
                XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                        "UNIT_code='0' NARRATION='" + CreditNarration + "' AMOUNT='" + Bill_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='PS' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='5' tranid='0'/>";

                Order_Code = Order_Code + 1;
                XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + PurchaseAc + "' " +
                                                       "UNIT_code='0' NARRATION='" + DebitNarration + "' AMOUNT='" + subTotal + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                       "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                       "SORT_TYPE='PS' SORT_NO='" + Doc_No + "' ac='" + PurchaseAcid + "' vc='0' progid='5' tranid='0'/>";
            }

            //CGSTAcc Effect
            if (CGSTAmount > 0) {
                Order_Code = Order_Code + 1;
                XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["PurchaseCGSTAc"] %>' + "' " +
                                                      "UNIT_code='0' NARRATION='" + CreditNarration + "' AMOUNT='" + CGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='PS' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["PurchaseCGSTid"] %>' + "' vc='0' progid='5' tranid='0'/>";
            }
            //end CGSTAcc Effect
            //SGSTAcc Effect
            if (SGSTAmount > 0) {
                Order_Code = Order_Code + 1;
                XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["PurchaseSGSTAc"] %>' + "' " +
                                                      "UNIT_code='0' NARRATION='" + CreditNarration + "' AMOUNT='" + SGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='PS' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["PurchaseSGSTid"] %>' + "' vc='0' progid='5' tranid='0'/>";
            }
            //end SGSTAcc Effect

            //IGSTAcc Effect
            if (IGSTAmount > 0) {
                Order_Code = Order_Code + 1;
                XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["PurchaseIGSTAc"] %>' + "' " +
                                                      "UNIT_code='0' NARRATION='" + CreditNarration + "' AMOUNT='" + IGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='PS' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["PurchaseIGSTid"] %>' + "' vc='0' progid='5' tranid='0'/>";
            }
            //end IGSTAcc Effect
            if (TCS_Amt > 0) {

                Order_Code = Order_Code + 1;
                XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                      "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='PS' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='5' tranid='0'/>";

                Order_Code = Order_Code + 1;
                XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["PurchaseTCSAc"] %>' + "' " +
                                                      "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='PS' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["PurchaseTCSAcid"] %>' + "' vc='0' progid='5' tranid='0'/>";
            }


            if (TDS_Amt > 0) {

                Order_Code = Order_Code + 1;
                XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                      "UNIT_code='0' NARRATION='" + TDSNarration + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='PS' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='5' tranid='0'/>";

                Order_Code = Order_Code + 1;
                XML = XML + "<Ledger TRAN_TYPE='PS' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + PurchaseTDSAc + "' " +
                                                      "UNIT_code='0' NARRATION='" + TDSNarration + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='PS' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["PurchaseTDSacid"] %>' + "' vc='0' progid='5' tranid='0'/>";
            }
            XML = XML + "</PurchaseHead></ROOT>";
            debugger;
            ProcessXML(XML, status, spname);


        }
        function ProcessXML(XML, status, spname) {

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
                $("#loader").hide();
                if (status != "Delete") {
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
                        window.open('../Inword/pgeSugarPurchaseForGSTxml.aspx?purchaseid=' + id + '&Action=1', "_self");

                    }
                }
                else {
                    var num = parseInt(response.d);

                    if (isNaN(num)) {
                        //alert(response.d)
                        swal("" + response.d + "", "", "warning");

                    }
                    else {
                        window.open('../Inword/PgePurchaseHeadUtility.aspx', "_self");
                    }
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
            <asp:Label ID="label1" runat="server" Text="   Sugar Purchase For GST   " Font-Names="verdana"
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
            <asp:HiddenField ID="Hdnfitm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfmc" runat="server" />
            <asp:HiddenField ID="hdnfuc" runat="server" />
            <asp:HiddenField ID="hdnfbk" runat="server" />
            <asp:HiddenField ID="hdnfac" runat="server" />
            <asp:HiddenField ID="hdnfpurcid" runat="server" />
            <asp:HiddenField ID="hdnfBrokerShort" runat="server" />
            <asp:HiddenField ID="hdnfMillshort" runat="server" />
            <asp:HiddenField ID="hdnfPurchaseAc" runat="server" />
            <asp:HiddenField ID="hdnfPurchaseAcid" runat="server" />
            <asp:HiddenField ID="hdnfpurchasedoc" runat="server" />
            <asp:HiddenField ID="hdnfpurchaseid" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table width="80%" align="left">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                TabIndex="34" Height="24px" ValidationGroup="save" OnClick="btnAdd_Click" />
                            &nbsp;
                            <asp:Button ID="btnSave" OnClientClick="if (!Validate()) return false;" runat="server"
                                Text="Save" CssClass="btnHelp" Width="90px" TabIndex="38" Height="24px" ValidationGroup="add"
                                OnClick="btnSave_Click" />
                            &nbsp;
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                                TabIndex="36" Height="24px" ValidationGroup="save" OnClick="btnEdit_Click" />
                            &nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                                TabIndex="37" Height="24px" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                                TabIndex="38" Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" />
                            &nbsp;
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                                TabIndex="38" Height="24px" ValidationGroup="save" OnClientClick="Back()" />
                            &nbsp;
                            <asp:Button runat="server" ID="btnPrintSaleBill" Text="Print" CssClass="btnHelp"
                                OnClientClick="SB();" Width="80px" Height="24px" />

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
                <table width="90%" align="left" cellspacing="4">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Change No:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="txtxf" FilterType="Numbers" InvalidChars="."
                                TargetControlID="txtEditDoc_No">
                            </ajax1:FilteredTextBoxExtender>
                            &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Visible="false" Font-Size="Small" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblSelfBal" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Visible="false" Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Bill No.:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDOC_NO" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtDOC_NO_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtdoc_no" runat="server" Text="..." Width="80px" OnClick="btntxtDOC_NO_Click"
                                CssClass="btnHelp" Height="24px" />
                            <asp:Label ID="lblPurchase_Id" runat="server" Font-Bold="true" Font-Italic="true"
                                Font-Names="verdana" Font-Size="Small" ForeColor="Red" Visible="false"></asp:Label>
                            DO No:
                            <asp:TextBox ID="txtPURCNO" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtPURCNO_TextChanged"
                                onkeydown="PURCNO(event);" Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtPURCNO" runat="server" Text="..." OnClick="btntxtPURCNO_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            Date:
                            <asp:TextBox ID="txtDOC_DATE" runat="Server" CssClass="txt" TabIndex="2" Width="100px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtDOC_DATE_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" />
                            <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtDOC_DATE"
                                PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                            &nbsp;Retail Stock:&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="drpreatilstock" runat="server" AutoPostBack="true" Width="120px"
                                CssClass="ddl" OnSelectedIndexChanged="drpreatilstock_SelectedIndexChanged" TabIndex="3">
                                <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">From:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtAC_CODE" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                Style="text-align: right;" OnTextChanged="txtAC_CODE_TextChanged" AutoPostBack="false"
                                onkeydown="Ac_Code(event);" Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtAC_CODE" runat="server" Text="..." OnClick="btntxtAC_CODE_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="LblPartyname" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Unit:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtUnit_Code" runat="Server" CssClass="txt" TabIndex="4" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtUnit_Code_TextChanged"
                                onkeydown="Unit(event);"></asp:TextBox>
                            <asp:Button ID="btntxtUnitcode" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                Width="20px" OnClick="btntxtUnitcode_Click" />
                            <asp:Label ID="lblUnitName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Mill:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtMILL_CODE" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtMILL_CODE_TextChanged"
                                onkeydown="Mill(event);" Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtMILL_CODE" runat="server" Text="..." OnClick="btntxtMILL_CODE_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="LBLMILLNAME" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">From:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFROM_STATION" runat="Server" CssClass="txt" TabIndex="6" Width="200px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtFROM_STATION_TextChanged"
                                Height="24px"></asp:TextBox>
                            To:
                            <asp:TextBox ID="txtTO_STATION" runat="Server" CssClass="txt" TabIndex="7" Width="200px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTO_STATION_TextChanged"
                                Height="24px"></asp:TextBox>
                            Grade:
                            <asp:TextBox ID="txtgrade" runat="Server" CssClass="txt" TabIndex="7" Width="200px"
                                Style="text-align: left;" Height="24px"></asp:TextBox>
                            Lorry No:
                            <asp:TextBox ID="txtLORRYNO" runat="Server" CssClass="txt" TabIndex="8" Width="200px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtLORRYNO_TextChanged"
                                Height="24px"></asp:TextBox>
                            Wear House:
                            <asp:TextBox ID="txtWEARHOUSE" runat="Server" CssClass="txt" TabIndex="9" Width="200px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtWEARHOUSE_TextChanged"
                                Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Broker:
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtBROKER" runat="Server" CssClass="txt" TabIndex="10" Width="80px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBROKER_TextChanged"
                                onkeydown="Broker(event);" Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtBROKER" runat="server" Text="..." OnClick="btntxtBROKER_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="LBLBROKERNAME" runat="server" CssClass="lblName"></asp:Label>
                            GST Rate Code
                            <asp:TextBox ID="txtGSTRateCode" runat="Server" CssClass="txt" TabIndex="11" Width="80px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtGSTRateCode_TextChanged"
                                onkeydown="GSTRate(event);" Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtGSTRateCode" runat="server" Text="..." OnClick="btntxtGSTRateCode_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblGSTRateName" runat="server" CssClass="lblName"></asp:Label>
                            Bill No.:
                            <asp:TextBox ID="txtbillNo" runat="Server" CssClass="txt" TabIndex="12" Width="200px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtbillNo_TextChanged"
                                Height="24px"></asp:TextBox>
                            mill Invoice Date:
                            <asp:TextBox ID="txtmill_inv_date" runat="Server" CssClass="txt" TabIndex="13" Width="100px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtmill_inv_date_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="Imagetxtmill_inv_date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" />
                            <ajax1:CalendarExtender ID="CalendarExtenderMill_Inv" runat="server" TargetControlID="txtmill_inv_date"
                                PopupButtonID="Imagetxtmill_inv_date" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
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
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Item:
                                <asp:TextBox ID="txtITEM_CODE" runat="Server" CssClass="txt" TabIndex="14" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtITEM_CODE_TextChanged"
                                    onkeydown="Item(event);"></asp:TextBox>
                                <asp:Button ID="btntxtITEM_CODE" runat="server" Text="..." OnClick="btntxtITEM_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="LBLITEMNAME" runat="server" CssClass="lblName"></asp:Label>
                                Brand_Code
                                <asp:TextBox ID="txtBrand_Code" runat="Server" AutoPostBack="false" CssClass="txt"
                                    Height="24px" OnTextChanged="txtBrand_Code_TextChanged" Style="text-align: left;"
                                    TabIndex="18" Width="90px" onkeydown="Brand_Code(event);"></asp:TextBox>
                                <asp:Button ID="btntxtBrand_Code" runat="server" CssClass="btnHelp" OnClick="btntxtBrand_Code_Click"
                                    Text="..." />
                                <asp:Label ID="lblBrandname" runat="server" CssClass="lblName"></asp:Label>
                                Quantal:
                                <asp:TextBox ID="txtQUANTAL" runat="Server" CssClass="txt" TabIndex="15" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtQUANTAL_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtQUANTAL" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtQUANTAL">
                                </ajax1:FilteredTextBoxExtender>
                                Packing:
                                <asp:TextBox ID="txtPACKING" runat="Server" CssClass="txt" TabIndex="16" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtPACKING_TextChanged"></asp:TextBox>
                                </ajax1:FilteredTextBoxExtender> Bags:
                                <asp:TextBox ID="txtBAGS" runat="Server" ReadOnly="true" CssClass="txt" TabIndex="17"
                                    Height="24px" Width="80px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtBAGS_TextChanged"></asp:TextBox>
                                Rate:
                                <asp:TextBox ID="txtRATE" runat="Server" CssClass="txt" TabIndex="18" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtRATE_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtRATE" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtRATE">
                                </ajax1:FilteredTextBoxExtender>
                                Item Amount:
                                <asp:TextBox ID="txtITEMAMOUNT" runat="Server" CssClass="txt" TabIndex="19" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="true" ReadOnly="true"
                                    OnTextChanged="txtITEMAMOUNT_TextChanged"></asp:TextBox>
                                Narration:
                                <asp:TextBox ID="txtITEM_NARRATION" runat="Server" CssClass="txt" TabIndex="20" Width="350px"
                                    TextMode="MultiLine" Height="50px" Style="text-align: left;" AutoPostBack="false"
                                    OnTextChanged="txtITEM_NARRATION_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                    OnClick="btnAdddetails_Click" TabIndex="21" Height="24px" />
                                <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp" Width="80px"
                                    OnClick="btnClosedetails_Click" TabIndex="22" Height="24px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table>
                    <tr>
                        <td align="left">
                            <div style="width: 100%; position: relative; margin-top: 0px;">
                                <asp:UpdatePanel ID="upGrid" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlgrdDetail" runat="server" align="left" ScrollBars="Both" Height="200px"
                                            Width="1200px" BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true"
                                            Font-Names="Verdana" Font-Size="11px" BackColor="SeaShell" Style="margin-left: 10px; float: left;">
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
                            <div style="margin-top: 180px; margin-left: 70px;">
                                Net Qntl:<asp:TextBox ID="txtNETQNTL" runat="Server" CssClass="txt" ReadOnly="true"
                                    TabIndex="23" Width="120px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtNETQNTL_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtNETQNTL" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtNETQNTL">
                                </ajax1:FilteredTextBoxExtender>
                                EWayBill NO:
                                <asp:TextBox runat="server" ID="txtEway_Bill_No" CssClass="txt" Width="200px" Height="24px"
                                    TabIndex="24"></asp:TextBox>
                                Due Days:
                                <asp:TextBox ID="txtDUE_DAYS" runat="Server" CssClass="txt" TabIndex="25" Width="120px"
                                    Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtDUE_DAYS_TextChanged"
                                    Height="24px" onkeyDown="DueDays(event);"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                    TargetControlID="txtDUE_DAYS">
                                </ajax1:FilteredTextBoxExtender>
                            </div>
                        </td>
                        <td style="width: 100%;" align="left">
                            <table width="60%">
                                <tr>
                                    <td align="left" style="width: 50%;">Subtotal:
                                    </td>
                                    <td align="left" style="width: 50%;">
                                        <asp:TextBox ID="txtSUBTOTAL" runat="Server" CssClass="txt" ReadOnly="true" Width="120px" TabIndex="26"
                                            Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtSUBTOTAL_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtSUBTOTAL">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>CGST%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtCGSTRate" runat="Server" CssClass="txt" Width="42px" TabIndex="27"
                                            Style="text-align: right;" Height="24px" AutoPostBack="true" OnTextChanged="txtCGSTRate_TextChanged"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtCGSTRate" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtCGSTRate" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtCGSTAmount" runat="Server" CssClass="txt" Width="70px" ReadOnly="true"
                                            Style="text-align: right;" Height="24px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="">SGST%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtSGSTRate" runat="Server" CssClass="txt" Width="42px" TabIndex="28"
                                            AutoPostBack="true" OnTextChanged="txtSGSTRate_TextChanged" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtSGSTRate" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtSGSTRate" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtSGSTAmount" runat="Server" CssClass="txt" Width="70px" ReadOnly="true"
                                            Style="text-align: right;" Height="24px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="">IGST%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtIGSTRate" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                            OnTextChanged="txtIGSTRate_TextChanged" TabIndex="29" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtIGSTRate" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtIGSTRate" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtIGSTAmount" runat="Server" CssClass="txt" Width="70px" ReadOnly="true"
                                            Style="text-align: right;" Height="24px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Less Frt. Rs.
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtLESS_FRT_RATE" runat="Server" CssClass="txt" TabIndex="30" Width="40px"
                                            Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtLESS_FRT_RATE_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtLESS_FRT_RATE" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtLESS_FRT_RATE">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtFREIGHT" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                            OnTextChanged="txtFREIGHT_TextChanged" ReadOnly="true" Style="text-align: right;"
                                            Width="72px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtFREIGHT" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtFREIGHT" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Bank Commission:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBANK_COMMISSION" runat="Server" CssClass="txt" TabIndex="31"
                                            Width="120px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtBANK_COMMISSION_TextChanged"
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
                                        <asp:TextBox ID="txtOTHER_AMT" runat="Server" AutoPostBack="true" CssClass="txt"
                                            Height="24px" OnTextChanged="txtOTHER_AMT_TextChanged" Style="text-align: right;"
                                            TabIndex="32" Width="120px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtOTHER_AMT" ValidChars=".,-">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Cash Advance:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCASH_ADVANCE" runat="Server" CssClass="txt" TabIndex="33" Width="120px"
                                            Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtCASH_ADVANCE_TextChanged"
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
                                            Width="120px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtBILL_AMOUNT_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtBILL_AMOUNT">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>TCS%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtTCSRate" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                            OnTextChanged="txtTCSRate_TextChanged" TabIndex="35" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTCSRate">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtTCSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                            OnTextChanged="txtTCSAmt_TextChanged" ReadOnly="false" Style="text-align: right;"
                                            Width="72px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtTCSAmt" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>TDS%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtTDS" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                            OnTextChanged="txtTDS_TextChanged" TabIndex="36" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTDS">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtTDSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                            OnTextChanged="txtTDSAmt_TextChanged" ReadOnly="false" Style="text-align: right;"
                                            Width="72px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtTDSAmt" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Net Payable:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtTCSNet_Payable" runat="Server" CssClass="txt" ReadOnly="true"
                                            TabIndex="37" Width="120px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtTCSNet_Payable_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTCSNet_Payable">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <%--    <tr>
                                    <td align="left">
                                        TDS%
                                    </td>
                                    <td align="">
                                        <asp:TextBox ID="txtTDS" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                            OnTextChanged="txtTDS_TextChanged" TabIndex="34" Style="text-align: right;" Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTDS">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtTDSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                            TabIndex="35" OnTextChanged="txtTDSAmt_TextChanged" ReadOnly="true" Style="text-align: right;"
                                            Width="98px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtTDSAmt" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>--%>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg"
                    Width="20px" Height="20px" Style="float: right; vertical-align: top;" OnClick="imgBtnClose_Click"
                    ToolTip="Close" />
                <table width="70%">
                    <tr>
                        <td align="center" style="background-color: #F5B540; width: 100%;">
                            <asp:Label ID="lblPopupHead" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Font-Bold="true" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Search Text:
                            <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                                Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="1250px" Direction="LeftToRight" BackColor="#FFFFE4"
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
