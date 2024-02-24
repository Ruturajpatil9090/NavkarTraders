<%@ Page Title="Retail Sell For GST" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeSugarRetailForGSTnew.aspx.cs" Inherits="Sugar_pgeSugarRetailForGSTnew" %>

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
    <script type="text/javascript">
        if (+localStorage.tabCount > 0) {
            var style = document.createElement("style");
            style.innerHTML = "body { display:none !important; }";
            document.getElementsByTagName("HEAD")[0].appendChild(style);
            alert('Page Already open please close page !!!');
        }
        else {
            localStorage.tabCount = 0;
        }
        localStorage.tabCount = +localStorage.tabCount + 1;
        window.onunload = function () {
            localStorage.tabCount = +localStorage.tabCount - 1;
        };

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
                if (hdnfClosePopupValue == "txtMillCode") {
                    document.getElementById("<%=txtMillCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBroker_Code") {
                    document.getElementById("<%=txtBroker_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGSTRateCode") {
                    document.getElementById("<%=txtGSTRateCode.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtITEM_CODE") {
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtparty_Name_New") {
                    document.getElementById("<%=txtparty_Name_New.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurchaseno") {
                    document.getElementById("<%=btntxtPurchaseno.ClientID %>").focus();
                }
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
    </script>
    <script type="text/javascript">
        function SB() {
            var billno = document.getElementById('<%=txtDOC_NO.ClientID %>').value;
            var type = document.getElementById('<%=drpCashCredit.ClientID %>').value;
            window.open('../Report/rptRetailSellPrintForGST.aspx?billno=' + billno + '&type=' + type);
        }
        function CRP() {
            var billno = document.getElementById('<%=txtDOC_NO.ClientID %>').value;
            var type = document.getElementById('<%=drpCashCredit.ClientID %>').value;
            window.open('../Report/rptCashRecivePrint.aspx?billno=' + billno + '&type=' + type);
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
    <script type="text/javascript" language="javascript">
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;
        debugger;
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
                if (hdnfClosePopupValue == "txtAC_CODE") {
                    document.getElementById("<%=txtAC_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LblPartyname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtAC_CODE.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtMillCode") {

                    document.getElementById("<%=txtMillCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblMillName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    // document.getElementById("<%= hdnfDoNo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    document.getElementById("<%=txtMillCode.ClientID %>").focus();


                }
                if (hdnfClosePopupValue == "txtPurchaseno") {


                    document.getElementById("<%=txtPurchaseno.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblpurchaseno.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[7].innerText;
                    document.getElementById("<%= hdnfDoNo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= hdconfirm.ClientID %>").value = "Close";
                    document.getElementById("<%=btntxtPurchaseno.ClientID %>").focus();

                }
                if (hdnfClosePopupValue == "txtBroker_Code") {

                    document.getElementById("<%=txtBroker_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblbrokerName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;

                    document.getElementById("<%=txtBroker_Code.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtDOC_NO") {

                    document.getElementById("<%=txtDOC_NO.ClientID %>").disabled = true;
                    document.getElementById("<%=txtDOC_NO.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                }
                if (hdnfClosePopupValue == "txtITEM_CODE") {
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLITEMNAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").focus();

                }
                if (hdnfClosePopupValue == "txtGSTRateCode") {
                    document.getElementById("<%=txtGSTRateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblGSTRateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGSTRateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtparty_Name") {

                    document.getElementById("<%=txtparty_Name.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblCashParty_Name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;

                    document.getElementById("<%=txtparty_Name.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtparty_Name_New") {

                    document.getElementById("<%=txtparty_Name_New.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblParty_Name_New.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtparty_Name_New.ClientID %>").focus();
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
                $("#<%=drpDelivered.ClientID %>").focus();
            }

    }

    function ac_name(e) {
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
    }



    function ac_name_Part_Code(e) {
        if (e.keyCode == 112) {
            debugger;
            e.preventDefault();
            $("#<%=pnlPopup.ClientID %>").show();
            $("#<%=btntxtParty_Name.ClientID %>").click();

        }
        if (e.keyCode == 9) {
            e.preventDefault();
            var unit = $("#<%=txtparty_Name_New.ClientID %>").val();

            unit = "0" + unit;
            $("#<%=txtparty_Name_New.ClientID %>").val(unit);
            __doPostBack("txtparty_Name_New", "TextChanged");

        }
    }

    function GstRateCode(e) {
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
    function Broker_code(e) {
        if (e.keyCode == 112) {
            debugger;
            e.preventDefault();
            $("#<%=pnlPopup.ClientID %>").show();
            $("#<%=btntxtBroker_Code.ClientID %>").click();

        }
        if (e.keyCode == 9) {
            e.preventDefault();
            var unit = $("#<%=txtBroker_Code.ClientID %>").val();

            unit = "0" + unit;
            $("#<%=txtBroker_Code.ClientID %>").val(unit);
            __doPostBack("txtBroker_Code", "TextChanged");

        }
    }
    function mill_code(e) {
        if (e.keyCode == 112) {
            debugger;
            e.preventDefault();
            $("#<%=pnlPopup.ClientID %>").show();
            $("#<%=btntxtMillCode.ClientID %>").click();

        }
        if (e.keyCode == 9) {
            e.preventDefault();
            var unit = $("#<%=txtMillCode.ClientID %>").val();

            unit = "0" + unit;
            $("#<%=txtMillCode.ClientID %>").val(unit);
            __doPostBack("txtMillCode", "TextChanged");

        }

        if (e.keyCode == 13) {
            e.preventDefault();
            $("#<%=txtDUE_DAYS.ClientID %>").focus();

        }
    }
    function purchesId(e) {
        if (e.keyCode == 112) {
            debugger;
            e.preventDefault();
            $("#<%=pnlPopup.ClientID %>").show();
            $("#<%=btntxtPurchaseno.ClientID %>").click();

        }
        if (e.keyCode == 9) {
            e.preventDefault();
            var unit = $("#<%=txtPurchaseno.ClientID %>").val();


            __doPostBack("txtPurchaseno", "TextChanged");

        }
    }
    function item_code(e) {
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

    }
    function save(e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#<%=btnSave.ClientID %>").focus();
        }
    }
    function Back() {

        //alert(td);
        window.open('../Outword/PgeRetailSellUtility.aspx', '_self');
    }
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function Validate() {
            debugger;

            var Outword_Date = '<%= Session["Outword_Date"] %>';
            var Post_Date = '<%= Session["Post_Date"] %>';
            var DocDate = $("#<%=txtDOC_DATE.ClientID %>").val();

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

            if (Post_Date == "") {
                alert('Update Post Date');
                $("#loader").hide();
                return false;
            }

            Outword_Date = Outword_Date.slice(6, 10) + Outword_Date.slice(2, 5) + "/" + Outword_Date.slice(0, 2);
            DocDate = DocDate.slice(6, 10) + DocDate.slice(2, 5) + "/" + DocDate.slice(0, 2);
            Post_Date = Post_Date.slice(6, 10) + Post_Date.slice(2, 5) + "/" + Post_Date.slice(0, 2);

            if (DocDate > Post_Date) {

            }
            else {
                alert('Post Date Error');
                $("#loader").hide();
                return false;
            }

            if (DocDate > Outword_Date) {

            }
            else {
                alert('GST Return Fined please Do not edit Record');
                $("#loader").hide();
                return false;
            }

            if ($("#<%=txtparty_Name_New.ClientID %>").val() == "") {
                $("#<%=txtparty_Name_New.ClientID %>").focus();
                $("#loader").hide(); return false;
            }
            if ($("#<%=txtAC_CODE.ClientID %>").val() == "") {
                $("#<%=txtAC_CODE.ClientID %>").focus();
                 $("#loader").hide(); return false;

             }

             if ($("#<%=txtDOC_DATE.ClientID %>").val() == "") {
                $("#<%=txtDOC_DATE.ClientID %>").focus();
                $("#loader").hide(); return false;

            }
            if ($("#<%=txtGSTRateCode.ClientID %>").val() == "") {
                $("#<%=txtGSTRateCode.ClientID %>").focus();
                $("#loader").hide(); return false;

            }
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to print data?")) {
                confirm_value.value = "Yes";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            }
            else {
                confirm_value.value = "No";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
            }
            document.forms[0].appendChild(confirm_value);
            // elem.disabled = true;
            $("#loader").show();
            return true;
        }
        function pagevalidation() {
            debugger;
            $("#loader").show();
            var Doc_No = 0, retailid = 0, retailDetailid = 0;
            var status = document.getElementById("<%= btnSave.ClientID %>").value;
            if (status == "Update") {
                Doc_No = document.getElementById("<%= hdnfretaildoc.ClientID %>").value;
                retailid = document.getElementById("<%= hdnfretailid.ClientID %>").value;
             }
            var spname = "RetailSell";
            var XML = "<ROOT>";
            var deliver_type = $("#<%=drpDelivered.ClientID %>").val();
            var d = $("#<%=txtDOC_DATE.ClientID %>").val();
            var DOC_DATE = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

            var CHALLAN_NO = $("#<%=txtChallanNo.ClientID %>").val();
            var d = $("#<%=txtChallanDate.ClientID %>").val();
            var CHALLAN_DATE = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

            var LORRYNO = $("#<%=txtLORRYNO.ClientID %>").val();
            LORRYNO = LORRYNO.toUpperCase();
            var AC_CODE = $("#<%=txtAC_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtAC_CODE.ClientID %>").val();
            var DUE_DAYS = $("#<%=txtDUE_DAYS.ClientID %>").val() == "" ? 0 : $("#<%=txtDUE_DAYS.ClientID %>").val();
            var d = $("#<%=txtDueDate.ClientID %>").val();
            var DUE_DATE = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

            var TOTAL = $("#<%=txtTotal.ClientID %>").val() == "" ? 0 : $("#<%=txtTotal.ClientID %>").val();
            var SUBTOTAL = $("#<%=txtSubtotal.ClientID %>").val() == "" ? 0 : $("#<%=txtSubtotal.ClientID %>").val();
            var VAT = $("#<%=txtVatAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtVatAmount.ClientID %>").val();
            var ROUND_OFF = $("#<%=txtRoundOff.ClientID %>").val() == "" ? 0 : $("#<%=txtRoundOff.ClientID %>").val();
            var hamali_amount = $("#<%=txthamliAmount.ClientID %>").val() == "" ? 0 : $("#<%=txthamliAmount.ClientID %>").val();
            var GRAND_TOTAL = $("#<%=txtBILL_AMOUNT.ClientID %>").val() == "" ? 0 : $("#<%=txtBILL_AMOUNT.ClientID %>").val();
            var CGSTRate = $("#<%=txtCGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtCGSTRate.ClientID %>").val();
            var CGSTAmount = $("#<%=txtCGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtCGSTAmount.ClientID %>").val();
            var TRAN_TYPE = $("#<%=drpCashCredit.ClientID %>").val();
            var IGSTRate = $("#<%=txtIGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtIGSTRate.ClientID %>").val();
            var IGSTAmount = $("#<%=txtIGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtIGSTAmount.ClientID %>").val();
            var SGSTRate = $("#<%=txtSGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtSGSTRate.ClientID %>").val();
            var SGSTAmount = $("#<%=txtSGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtSGSTAmount.ClientID %>").val();
            var GSTRateCode = $("#<%=txtGSTRateCode.ClientID %>").val() == "" ? 0 : $("#<%=txtGSTRateCode.ClientID %>").val();

            // Broker_CODE = $("#<%=txtBroker_Code.ClientID %>").val() ==""? 0 : $("#<%=txtBroker_Code.ClientID %>").val();
            var Broker_CODE = $("#<%=txtBroker_Code.ClientID %>").val()=="" ? 2 : $("#<%=txtBroker_Code.ClientID %>").val();
            var DELIVERED = 0;

            if (deliver_type == "1") {
                DELIVERED = "1";
            }
            var cashcredit = "";
            if (TRAN_TYPE == "CS" || TRAN_TYPE == "CS1") {
                cashcredit = "CS";
            }
            else {
                cashcredit = "CR";
            }
            var cashrecive = 1;

            var narrtion = $("#<%=txtnarration.ClientID %>").val();
            var Party_Name = $("#<%=txtparty_Name.ClientID %>").val();
            var Party_Name_New = $("#<%=txtparty_Name_New.ClientID %>").val() == "" ? 0 : $("#<%=txtparty_Name_New.ClientID %>").val();
            var BILL_AMOUNT = $("#<%=txtBILL_AMOUNT.ClientID %>").val() == "" ? 0.00 : $("#<%=txtBILL_AMOUNT.ClientID %>").val();
            var saleAc = document.getElementById("<%= hdnfSaleAc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfSaleAc.ClientID %>").value;
            var saleAcid = document.getElementById("<%= hdnfSaleAcid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfSaleAcid.ClientID %>").value;
            var TCS_Rate = $("#<%=txtTCSRate.ClientID %>").val() == "" ? 0.00 : $("#<%=txtTCSRate.ClientID %>").val();
            var TCS_Amt = $("#<%=txtTCSAmt.ClientID %>").val() == "" ? 0.00 : $("#<%=txtTCSAmt.ClientID %>").val();
            var TCS_Net_Payable = $("#<%=txtTCSNet_Payable.ClientID %>").val() == "" ? 0.00 : $("#<%=txtTCSNet_Payable.ClientID %>").val();
            var ac = document.getElementById("<%= hdnfto.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfto.ClientID %>").value;
            if (ac == "" || ac == "&nbsp;") {
                ac = 0;
            }
            var bc = document.getElementById("<%= hdnfbc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbc.ClientID %>").value;
            if (bc == "" || bc == "&nbsp;") {
                bc = 0;
            }
            var USER = '<%= Session["user"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';
            var Branch_Code = '<%= Session["Branch_Code"] %>';

            
            var HeadInsertUpdate = ""; Detail_Insert = ""; Detail_Update = ""; Detail_Delete = "";
            var Detail_Value = ""; concatid = "";
            var Gledger_Insert = ""; Gledger_values = "";
            var Gledger_Delete = "";


            if (status == "Save") {
                HeadInsertUpdate = "Created_By='" + USER + "' Modified_By=''";
            }
            else {
                HeadInsertUpdate = "Modified_By='" + USER + "' Created_By=''";
            }

            XML = XML + "<RetailHead Tran_Type='" + TRAN_TYPE + "' doc_no='" + Doc_No + "' doc_date='" + DOC_DATE + "' Challan_No='" + CHALLAN_NO + "' Challan_Date='" + CHALLAN_DATE + "' Vehical_No='" + LORRYNO + "' " +
                    "Party_Code='" + AC_CODE + "' Due_Days='" + DUE_DAYS + "' Due_Date='" + DUE_DATE + "' Total='" + TOTAL + "' Subtotal='" + SUBTOTAL + "' Vat='" + VAT + "' "+
                    "Round_Off='" + ROUND_OFF + "' Grand_Total='" + GRAND_TOTAL + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' "+HeadInsertUpdate+" "+
                    "Branch_Code='" + Branch_Code + "' Delivered='" + DELIVERED + "' GstRateCode='" + GSTRateCode + "' CGSTRate='" + CGSTRate + "' CGSTAmount='" + CGSTAmount + "' "+
                    "SGSTRate='" + SGSTRate + "' SGSTAmount='" + SGSTAmount + "' IGSTRate='" + IGSTRate + "' IGSTAmount='" + IGSTAmount + "' TaxableAmount='0.00' " +    
               "Party_Name='" + Party_Name + "' Broker_Code='" + Broker_CODE + "' HamaliAmount='" + hamali_amount + "' CashRecive='" + cashrecive + "' Party_Name_New='" + Party_Name_New + "' " +
                "ac='0' bc='" + bc + "' retailid='" + retailid + "' purchaseid='0' Narration='" + narrtion + "' TCS_Rate='" + TCS_Rate + "' TCS_Amt='" + TCS_Amt + "' TCS_Net_Payable='" + TCS_Net_Payable + "'>";
            //Detail Calculation
            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            var grdrow = gridView.getElementsByTagName("tr");

            var ddid = retailDetailid;
            for (var i = 1; i < grdrow.length; i++) {
                var ID = gridView.rows[i].cells[2].innerHTML;
                var Mill_Code = gridView.rows[i].cells[3].innerHTML;
                var Purchase_No = gridView.rows[i].cells[5].innerHTML;
                var grade = gridView.rows[i].cells[6].innerHTML;
                var item_code = gridView.rows[i].cells[7].innerHTML;
                var Quntal = gridView.rows[i].cells[9].innerHTML;
                var billing_no = gridView.rows[i].cells[10].innerHTML;
                var rate = gridView.rows[i].cells[11].innerHTML;
                var value = gridView.rows[i].cells[12].innerHTML;
                var vat_ac = gridView.rows[i].cells[13].innerHTML;
                var vat_percent = gridView.rows[i].cells[14].innerHTML;
                var vat_amount = gridView.rows[i].cells[15].innerHTML;
                var gross = gridView.rows[i].cells[16].innerHTML;
                var mc = gridView.rows[i].cells[20].innerHTML;
                if (mc == "" || mc == "&nbsp;") {
                    mc = 0;
                }

                var ic = gridView.rows[i].cells[21].innerHTML;
                if (ic == "" || ic == "&nbsp;") {
                    ic = 0;
                }
                if (gridView.rows[i].cells[18].innerHTML == "A") {
                   
                    XML = XML + "<RetailDetailInsert detail_id='" + ID + "' doc_no='" + Doc_No + "' Tran_Type='" + TRAN_TYPE + "' Mill_Code='" + Mill_Code + "' Item_Code='" + item_code + "' Quantity='" + Quntal + "' Billing_No='" + billing_no + "' " +
                          "Rate='" + rate + "' Value='" + value + "' Vat_Ac='" + vat_ac + "' vat_percent='" + vat_percent + "' vat_amount='" + vat_amount + "' " +
                          "Gross='" + gross + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' grade='" + grade + "' "+
                          "retailid='" + retailid + "' retaildetailid='" + ddid + "' ic='" + ic + "' mc='" + mc + "' purchaseid='" + Purchase_No + "'/>";
                    ddid = parseInt(ddid) + 1;
                }
                else if (gridView.rows[i].cells[18].innerHTML == "U") {
                    var dId = gridView.rows[i].cells[17].innerHTML;
                    XML = XML + "<RetailDetail detail_id='" + ID + "' doc_no='" + Doc_No + "' Tran_Type='" + TRAN_TYPE + "' Mill_Code='" + Mill_Code + "' Item_Code='" + item_code + "' Quantity='" + Quntal + "' Billing_No='" + billing_no + "' " +
                           "Rate='" + rate + "' Value='" + value + "' Vat_Ac='" + vat_ac + "' vat_percent='" + vat_percent + "' vat_amount='" + vat_amount + "' " +
                           "Gross='" + gross + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' grade='" + grade + "' " +
                           "retailid='" + retailid + "' retaildetailid='" + dId + "' ic='" + ic + "' mc='" + mc + "' purchaseid='" + Purchase_No + "'/>";
                }
                else if (gridView.rows[i].cells[18].innerHTML == "D") {
                    var dId = gridView.rows[i].cells[17].innerHTML;
                    XML = XML + "<RetailDetailDelete detail_id='" + ID + "' doc_no='" + Doc_No + "' Tran_Type='" + TRAN_TYPE + "' Mill_Code='" + Mill_Code + "' Item_Code='" + item_code + "' Quantity='" + Quntal + "' Billing_No='" + billing_no + "' " +
                           "Rate='" + rate + "' Value='" + value + "' Vat_Ac='" + vat_ac + "' vat_percent='" + vat_percent + "' vat_amount='" + vat_amount + "' " +
                           "Gross='" + gross + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' grade='" + grade + "' " +
                           "retailid='" + retailid + "' retaildetailid='" + dId + "' ic='" + ic + "' mc='" + mc + "' purchaseid='" + Purchase_No + "'/>";
                }
            }
            Gledger_Delete = "delete from nt_1_gledger where TRAN_TYPE='RR' and Doc_No=" + Doc_No + " and Company_Code=" + Company_Code + " and Year_Code=" + Year_Code + "";
            var Order_Code = 1;
            var TCSNarration = "TCS:Retail sell";
            // Ac Code
            if (TRAN_TYPE == "CS" || TRAN_TYPE == "CS1") {
               // Gledger_values = Gledger_values + "('RR','CS','" + Doc_No + "','" + DOC_DATE + "','1','0','','" + GRAND_TOTAL + "', " +
                 //                                         " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','D','1',0,'" + Branch_Code + "','RR','" + Doc_No + "'," +
                   //                                       "'1','0','9','0'),";

                XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='CS' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='1' " +
                                                        "UNIT_code='0' NARRATION='' AMOUNT='" + GRAND_TOTAL + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='1' vc='0' progid='9' tranid='0'/>";
            }
            else {
                //Gledger_values = Gledger_values + "('RR','CR','" + Doc_No + "','" + DOC_DATE + "','" + AC_CODE + "','0','','" + GRAND_TOTAL + "', " +
                  //                                      " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','D','" + AC_CODE + "',0,'" + Branch_Code + "','RR','" + Doc_No + "'," +
                    //                                    " case when 0='" + ac + "' then null else '" + ac + "' end,'0','9','0'),";

                XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='CR' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + AC_CODE + "' " +
                                                        "UNIT_code='0' NARRATION='' AMOUNT='" + GRAND_TOTAL + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='"+AC_CODE+"' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='9' tranid='0'/>";
            }
            if (CGSTAmount > 0) {
                Order_Code = parseInt(Order_Code) + 1;
                //Gledger_values = Gledger_values + "('RR','CR','" + Doc_No + "','" + DOC_DATE + "','" + '<%=Session["SaleCGSTAc"] %>' + "','0','','" + CGSTAmount + "', " +
                  //                                          " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C',0,0,'" + Branch_Code + "','RR','" + Doc_No + "'," +
                    //                                        " case when 0='" + '<%=Session["SaleCGSTid"] %>' + "' then null else '" + '<%=Session["SaleCGSTid"] %>' + "' end,'0','9','0'),";

                XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='CR' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleCGSTAc"] %>' + "' " +
                                                        "UNIT_code='0' NARRATION='' AMOUNT='" + CGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleCGSTid"] %>' + "' vc='0' progid='9' tranid='0'/>";
            }
            // End SaleCGST

            // SaleSGST Acc
            if (SGSTAmount > 0) {
                Order_Code = parseInt(Order_Code) + 1;
                //Gledger_values = Gledger_values + "('RR','CR','" + Doc_No + "','" + DOC_DATE + "','" + '<%=Session["SaleSGSTAc"] %>' + "','0','','" + SGSTAmount + "', " +
                  //                                         " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C',0,0,'" + Branch_Code + "','RR','" + Doc_No + "'," +
                    //                                       " case when 0='" + '<%=Session["SaleSGSTid"] %>' + "' then null else '" + '<%=Session["SaleSGSTid"] %>' + "' end,'0','9','0'),";

                XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='CR' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleSGSTAc"] %>' + "' " +
                                                        "UNIT_code='0' NARRATION='' AMOUNT='" + SGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleSGSTid"] %>' + "' vc='0' progid='9' tranid='0'/>";
            }
            // End SaleSGST

            // SaleIGST Acc
            if (IGSTAmount > 0) {
                Order_Code = parseInt(Order_Code) + 1;
                Gledger_values = Gledger_values + "('RR','CR','" + Doc_No + "','" + DOC_DATE + "','" + '<%=Session["SaleIGSTAc"] %>' + "','0','','" + IGSTAmount + "', " +
                                                             " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C',0,0,'" + Branch_Code + "','RR','" + Doc_No + "'," +
                                                             " case when 0='" + '<%=Session["SaleIGSTid"] %>' + "' then null else '" + '<%=Session["SaleIGSTid"] %>' + "' end,'0','9','0'),";

                XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='CR' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleIGSTAc"] %>' + "' " +
                                                        "UNIT_code='0' NARRATION='' AMOUNT='" + IGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleIGSTid"] %>' + "' vc='0' progid='9' tranid='0'/>";
            }
            Order_Code = parseInt(Order_Code) + 1;
            var amt = parseFloat(TOTAL) + parseFloat(hamali_amount);
            //Gledger_values = Gledger_values + "('RR','CR','" + Doc_No + "','" + DOC_DATE + "','" + saleAc + "','0','','" + amt + "', " +
              //                                        " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C',0,0,'" + Branch_Code + "','RR','" + Doc_No + "'," +
                //                                      " case when 0='" + saleAcid + "' then null else '" + saleAcid + "' end,'0','9','0'),";

            XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='CR' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + saleAc + "' " +
                                                        "UNIT_code='0' NARRATION='' AMOUNT='" + amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + saleAcid + "' vc='0' progid='9' tranid='0'/>";

            if (AC_CODE != 1 && TCS_Amt > 0) {
                Order_Code = parseInt(Order_Code) + 1;
                //Gledger_values = Gledger_values + "('RR','CR','" + Doc_No + "','" + DOC_DATE + "','" + AC_CODE + "','0','" + TCSNarration + "','" + TCS_Amt + "', " +
                  //                                            " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','D',0,0,'" + Branch_Code + "','RR','" + Doc_No + "'," +
                    //                                          " case when 0='" + ac + "' then null else '" + ac + "' end,'0','9','0'),";

                XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='CR' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + AC_CODE + "' " +
                                                        "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='9' tranid='0'/>";
            }
            if (TCS_Amt > 0) {
                Order_Code = parseInt(Order_Code) + 1;
                Gledger_values = Gledger_values + "('RR','CR','" + Doc_No + "','" + DOC_DATE + "','" + '<%=Session["SaleTCSAc"] %>' + "','0','" + TCSNarration + "','" + TCS_Amt + "', " +
                                                              " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C',0,0,'" + Branch_Code + "','RR','" + Doc_No + "'," +
                                                              " case when 0='" + '<%=Session["SaleTCSAcid"] %>' + "' then null else '" + '<%=Session["SaleTCSAcid"] %>' + "' end,'0','9','0'),";

                XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='CR' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                                        "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleTCSAcid"] %>' + "' vc='0' progid='9' tranid='0'/>";
            }
           
            XML = XML + "</RetailHead></ROOT>";

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
                    if (status == "Save") {
                        alert('Sucessfully Added Record !!! Doc_no=' + response.d)
                    }
                    else {
                        alert('Sucessfully Updated Record !!! Doc_no=' + response.d)
                    }
                    //window.open('../Transaction/pgeReceiptPaymentxml.aspx?tranid=' + tranid + '&Action=1', "_self");
                    window.open('../Outword/pgeSugarRetailForGSTnew.aspx?retailid=' + response.d + '&Action=1&tran_type=' + TRAN_TYPE, "_self");

                   // alert(response.d)
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
     <script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Retail Sell   " Font-Names="verdana"
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
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfDoNo" runat="server" />
            <asp:HiddenField ID="hdnfdrpVal" runat="server" />
            <asp:HiddenField ID="hdnfto" runat="server" />
            <asp:HiddenField ID="hdnfbc" runat="server" />
            <asp:HiddenField ID="hdnfpn" runat="server" />
            <asp:HiddenField ID="hdnfSaleAc" runat="server" />
            <asp:HiddenField ID="hdnfSaleAcid" runat="server" />
              <asp:HiddenField ID="hdnfretaildoc" runat="server" />
              <asp:HiddenField ID="hdnfretailid" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 0px; margin-top: 0px; z-index: 100;">
                <table width="80%" align="center">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="save" OnClick="btnAdd_Click" />
                            &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="add" OnClick="btnSave_Click" TabIndex="24" OnClientClick="if (!Validate()) return false;" />
                            &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="save" OnClick="btnEdit_Click" TabIndex="25" />
                            &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()"
                            TabIndex="26" />
                            &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" />&nbsp;

                             <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                                 TabIndex="38" Height="24px" ValidationGroup="save" OnClientClick="Back()" />&nbsp;
                        <asp:Button runat="server" ID="btnPrintSaleBill" Text="Print" Visible="true" CssClass="btnHelp"
                            Width="80px" Height="24px" OnClientClick="SB();" TabIndex="20" />
                            <asp:Button runat="server" ID="btncashRecivePrint" Text=" Cash Recive Print" Visible="true"
                                CssClass="btnHelp" Width="120px" Height="24px" OnClientClick="CRP();" TabIndex="21" />
                        </td>
                        <td align="center">&nbsp;<asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnFirst_Click" Width="90px" Height="24px" Visible="false" />
                            &nbsp;<asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                                OnClick="btnPrevious_Click" Width="90px" Height="24px" Visible="false" />
                            &nbsp;<asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                                OnClick="btnNext_Click" Width="90px" Height="24px" Visible="false" />
                            &nbsp;<asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                                OnClick="btnLast_Click" Width="90px" Height="24px" Visible="false" />
                        </td>
                    </tr>
                </table>


                <table width="90%" align="left" cellspacing="5">
                    <tr>
                        <td align="left"></td>
                        <td>
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                            <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                TabIndex="0" AutoPostBack="false" OnTextChanged="txtEditDoc_No_TextChanged" Visible="false"></asp:TextBox>
                            <%--<ajax1:FilteredTextBoxExtender runat="server" ID="txtxf" FilterType="Numbers" InvalidChars="."
                                TargetControlID="txtEditDoc_No">
                            </ajax1:FilteredTextBoxExtender>--%>
                            &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td align="right" style="width: 10%;">Cash / Credit:
                        </td>
                        <td align="left" colspan="4">
                            <asp:DropDownList runat="server" ID="drpCashCredit" Width="100px" CssClass="ddl"
                                AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="drpCashCredit_SelectedIndexChanged">

                                <asp:ListItem Text="Credit" Value="CR"></asp:ListItem>
                                <asp:ListItem Text="Cash" Value="CS"></asp:ListItem>

                                <%--   <asp:ListItem Text="Cash" Value="CH" />--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">To:</td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtAC_CODE" runat="Server" CssClass="txt" TabIndex="2" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtAC_CODE_TextChanged"
                                Height="24px" onkeydown="ac_name(event);"></asp:TextBox>
                            <asp:Button ID="btntxtAC_CODE" runat="server" Text="..." OnClick="btntxtAC_CODE_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="LblPartyname" runat="server" CssClass="lblName"></asp:Label>
                            <%--  Purc No:<asp:TextBox ID="txtPURCNO" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtPURCNO_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtPURCNO" runat="server" Text="..." OnClick="btntxtPURCNO_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                &nbsp;DO:<asp:Label runat="server" ID="lblDONo" ForeColor="Yellow"></asp:Label>--%>
                        Bill No.:
                       
                            <asp:TextBox ID="txtDOC_NO" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtDOC_NO_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Label ID="lblDoc_Id" runat="server" Text="" Font-Names="verdana"
                                ForeColor="Blue" Font-Bold="true" Font-Size="12px" Visible="false"></asp:Label>
                            <asp:Button ID="btntxtdoc_no" runat="server" Text="..." Width="80px" OnClick="btntxtDOC_NO_Click"
                                CssClass="btnHelp" Height="24px" Visible="false" />
                            Bill Date:&nbsp;
                            <asp:TextBox ID="txtDOC_DATE" runat="Server" CssClass="txt" TabIndex="4" Width="100px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDOC_DATE_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" />
                            <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtDOC_DATE"
                                PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Party Name:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtparty_Name" runat="Server" CssClass="txt" TabIndex="5" Width="200px"
                                Style="text-align: left;" Height="24px" OnTextChanged="txtparty_Name_TextChanged"
                                AutoPostBack="false"></asp:TextBox>
                            <%--<asp:Button ID="btntxtParty_Name" runat="server" Text="..." OnClick="btntxtParty_Name_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />--%>
                            <asp:Label ID="lblCashParty_Name" runat="server" CssClass="lblName" Visible="false"></asp:Label>
                            Party Name New:
                            <asp:TextBox ID="txtparty_Name_New" runat="Server" CssClass="txt" TabIndex="6" Width="80px"
                                Style="text-align: left;" Height="24px" OnTextChanged="txtparty_Name_New_TextChanged"
                                AutoPostBack="false" onkeydown="ac_name_Part_Code(event);"></asp:TextBox>
                            <asp:Button ID="btntxtParty_Name" runat="server" Text="..." OnClick="btntxtParty_Name_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblParty_Name_New" runat="server" CssClass="lblName"></asp:Label>
                            Address:
                            <asp:Label Text="" runat="server" ID="lblPartyAddress" CssClass="lblName" />
                        </td>
                    </tr>

                    <tr>
                        <td align="right">Challan No.:
                       
                        </td>
                        <td align="left" colspan="6">

                            <asp:TextBox ID="txtChallanNo" runat="Server" CssClass="txt" Width="120px" Style="text-align: right;"
                                TabIndex="7" AutoPostBack="false" Height="24px" OnTextChanged="txtChallanNo_TextChanged"></asp:TextBox>
                            Challan Date:
                            <asp:TextBox ID="txtChallanDate" runat="Server" CssClass="txt" Width="100px" MaxLength="10"
                                TabIndex="8" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtChallanDate_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="imgcalender2" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" />
                            <ajax1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtChallanDate"
                                PopupButtonID="imgcalender2" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                            Lorry No:
                             <asp:TextBox ID="txtLORRYNO" runat="Server" CssClass="txt" Width="200px" Style="text-align: left; text-transform: uppercase;"
                                 AutoPostBack="false" OnTextChanged="txtLORRYNO_TextChanged"
                                 Height="24px" TabIndex="9"></asp:TextBox>
                            GST Rate Code
                       
                            <asp:TextBox ID="txtGSTRateCode" runat="Server" CssClass="txt" Width="80px" Style="text-align: left;"
                                TabIndex="10" AutoPostBack="false" onkeydown="GstRateCode(event);" OnTextChanged="txtGSTRateCode_TextChanged" Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtGSTRateCode" runat="server" Text="..." OnClick="btntxtGSTRateCode_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblGSTRateName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td align="right">Broker Code:
                        </td>
                        <td align="left" colspan="6">
                            <asp:TextBox ID="txtBroker_Code" runat="Server" CssClass="txt" Width="80px" Style="text-align: right;"
                                AutoPostBack="false" OnTextChanged="txtBroker_Code_TextChanged" Height="24px" onkeyDown="Broker_code(event);" TabIndex="11"></asp:TextBox>
                            <asp:Button ID="btntxtBroker_Code" runat="server" Text="..." OnClick="btntxtBroker_Code_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblbrokerName" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblbroker_id" runat="server" CssClass="lblName"></asp:Label>

                            <%--  Purc No:<asp:TextBox ID="txtPURCNO" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtPURCNO_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtPURCNO" runat="server" Text="..." OnClick="btntxtPURCNO_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                        ++++++++++++++++++++++++++++++++++++++++++        &nbsp;DO:<asp:Label runat="server" ID="lblDONo" ForeColor="Yellow"></asp:Label>--%>
                       Cash Recive:
                        &nbsp;<asp:CheckBox ID="chkcashrecive" runat="server"
                            Text="" />
                            Narration  
                            <asp:TextBox ID="txtnarration" runat="Server" CssClass="txt" Width="250px" Style="text-align: left;"
                                AutoPostBack="True" OnTextChanged="txtnarration_TextChanged"
                                Height="34px" TabIndex="12"></asp:TextBox>
                            <%-- Narration:<asp:TextBox ID="txtnarration" runat="Server" AutoPostBack="True" CssClass="txt" Height="35px" OnTextChanged="txtnarration_TextChanged" Style="text-align: right;" TabIndex="12" Width="300px"></asp:TextBox>--%>
                        </td>
                    </tr>
                    </tr>
                    <tr style="visibility: hidden;">

                        <td align="center" style="width: 100px; vertical-align: top;">
                            <asp:Button ID="btnOpenDetailsPopup" runat="server" CssClass="btnHelp" Height="24px" OnClick="btnOpenDetailsPopup_Click" Text="ADD" Width="80px" />
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

                            <td align="left">
                                <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblNo" runat="server" ForeColor="Azure" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Mill: 
                                <asp:TextBox ID="txtMillCode" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px" onKeyDown="mill_code(event);" OnTextChanged="txtMillCode_TextChanged" Style="text-align: right;" TabIndex="13" Width="80px"></asp:TextBox>
                                <asp:Button ID="btntxtMillCode" runat="server" CssClass="btnHelp" Height="24px" OnClick="btntxtMillCode_Click" Text="..." Width="20px" />
                                <asp:Label ID="lblMillName" runat="server" CssClass="lblName"></asp:Label>
                                <asp:Label ID="lblgrade" runat="server" CssClass="lblName"></asp:Label>
                                Purchase:
                                <asp:TextBox ID="txtPurchaseno" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px" onKeyDown="purchesId(event);" OnTextChanged="txtPurchaseno_TextChanged" Style="text-align: right;" Width="80px"></asp:TextBox>
                                <asp:Button ID="btntxtPurchaseno" runat="server" CssClass="btnHelp" Height="24px" OnClick="btntxtPurchaseno_Click" Text="..." Width="20px" TabIndex="14" />
                                <asp:Label ID="lblpurchaseno" runat="server" CssClass="lblName"></asp:Label>
                                Item:
                                <asp:TextBox ID="txtITEM_CODE" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px" onkeyDown="item_code(event);" OnTextChanged="txtITEM_CODE_TextChanged" Style="text-align: right;" TabIndex="15" Width="80px"></asp:TextBox>
                                <asp:Button ID="btntxtITEM_CODE" runat="server" CssClass="btnHelp" Height="24px" OnClick="btntxtITEM_CODE_Click" Text="..." Width="20px" />
                                <asp:Label ID="LBLITEMNAME" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td align="left">Qty:
                                <asp:TextBox ID="txtQUANTAL" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" OnTextChanged="txtQUANTAL_TextChanged" Style="text-align: right;" TabIndex="16" Width="80px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtQUANTAL" runat="server" FilterType="Numbers,Custom" TargetControlID="txtQUANTAL" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                Rate:
                                <asp:TextBox ID="txtRATE" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" OnTextChanged="txtRATE_TextChanged" Style="text-align: right;" TabIndex="17" Width="80px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtRATE" runat="server" FilterType="Numbers,Custom" TargetControlID="txtRATE" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                Value: 
                                <asp:TextBox ID="txtITEMAMOUNT" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" OnTextChanged="txtITEMAMOUNT_TextChanged" ReadOnly="true" Style="text-align: right;" TabIndex="18" Width="80px"></asp:TextBox>
                                Gross:
                                <asp:TextBox ID="txtGross" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" ReadOnly="true" Style="text-align: left;" TabIndex="19" Width="80px"></asp:TextBox>

                                <asp:Button ID="btnAdddetails" runat="server" CssClass="btnHelp" Height="25px" OnClick="btnAdddetails_Click" TabIndex="20" Text="ADD" Width="80px" />

                                <asp:Button ID="btnClosedetails" runat="server" CssClass="btnHelp" Height="25px" OnClick="btnClosedetails_Click" TabIndex="21" Text="Reset" Width="80px" />
                            </td>
                            <td align="left" style="visibility: hidden;">Billing No:
                                <asp:TextBox ID="txtBillingNo" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px" OnTextChanged="txtBillingNo_TextChanged" Style="text-align: right;" Width="80px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtPACKING" runat="server" FilterType="Numbers" TargetControlID="txtBillingNo">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table>
                    <tr>
                        <td align="left">
                            <div style="width: 100%; position: relative; vertical-align: top; margin-top: -20px;">
                                <asp:UpdatePanel ID="upGrid" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlgrdDetail" runat="server" align="left" BackColor="SeaShell" BorderColor="Maroon" BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" Font-Names="Verdana" Font-Size="11px" Height="200px" ScrollBars="Both" Style="margin-left: 30px; float: left;" Width="1100px">
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
                            <div style="margin-top: 180px; margin-left: 70px; width: 90%">
                                Due Days:
                                        <asp:TextBox ID="txtDUE_DAYS" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" onKeyDown="save(event);" OnTextChanged="txtDUE_DAYS_TextChanged" Style="text-align: right;" TabIndex="21 " Width="120px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="filtertxtDUE_DAYS" runat="server" FilterType="Numbers" TargetControlID="txtDUE_DAYS">
                                </ajax1:FilteredTextBoxExtender>
                                Due Date:&nbsp;
                                        <asp:TextBox ID="txtDueDate" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" MaxLength="10" onkeydown="return DateFormat(this,event.keyCode)" onkeyup="ValidateDate(this,event.keyCode)" OnTextChanged="txtDueDate_TextChanged" Style="text-align: left;" TabIndex="22" Width="100px"></asp:TextBox>
                                <asp:Image ID="imgDueCalendar" runat="server" Height="15px" ImageUrl="~/Images/calendar_icon1.png" Width="25px" />
                                <ajax1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDueCalendar" TargetControlID="txtDueDate">
                                </ajax1:CalendarExtender>
                                Delivered:
                                        <asp:DropDownList ID="drpDelivered" runat="server" AutoPostBack="true" CssClass="ddl" OnSelectedIndexChanged="drpDelivered_SelectedIndexChanged" TabIndex="23" Width="100px">
                                            <asp:ListItem Selected="True" Text="Select" />
                                            <asp:ListItem Text="YES" Value="1" />
                                            <asp:ListItem Text="NO" Value="0" />
                                        </asp:DropDownList>
                                Vat_Ac:
                                        <asp:TextBox ID="txtVatAc" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" OnTextChanged="txtVatAc_TextChanged" Style="text-align: right;" TabIndex="24" Width="80px"></asp:TextBox>
                                <asp:Button ID="btntxtVatAc" runat="server" CssClass="btnHelp" Height="24px" OnClick="btntxtVatAc_Click" Text="..." Width="20px" />
                                <asp:Label ID="lblVatAcName" runat="server" CssClass="lblName"></asp:Label>
                                Vat %:
                                        <asp:TextBox ID="txtVatPercent" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" ReadOnly="true" Style="text-align: left;" TabIndex="25" Width="80px"></asp:TextBox>
                                Vat Amount:
                                        <asp:TextBox ID="txtVatTotal" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" OnTextChanged="txtVatTotal_TextChanged" Style="text-align: left;" TabIndex="26" Width="80px"></asp:TextBox>

                            </div>
                        </td>
                        <td align="left" style="width: 100%;">
                            <table width="60%">
                                <tr>
                                    <td align="left" style="width: 40%;">Total: </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtTotal" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" OnTextChanged="txtTotal_TextChanged" ReadOnly="true" Style="text-align: right;" Width="120px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtSUBTOTAL" runat="server" FilterType="Numbers,Custom" TargetControlID="txtTotal" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>CGST% </td>
                                    <td style="">
                                        <asp:TextBox ID="txtCGSTRate" runat="Server" AutoPostBack="true" CssClass="txt" Height="24px" OnTextChanged="txtCGSTRate_TextChanged" Style="text-align: right;" Width="40px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtCGSTRate" runat="server" FilterType="Numbers,Custom" TargetControlID="txtCGSTRate" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtCGSTAmount" runat="Server" CssClass="txt" Height="24px" ReadOnly="true" Style="text-align: right;" Width="75px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="">SGST% </td>
                                    <td style="">
                                        <asp:TextBox ID="txtSGSTRate" runat="Server" AutoPostBack="true" CssClass="txt" Height="24px" OnTextChanged="txtSGSTRate_TextChanged" Style="text-align: right;" Width="40px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtSGSTRate" runat="server" FilterType="Numbers,Custom" TargetControlID="txtSGSTRate" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtSGSTAmount" runat="Server" CssClass="txt" Height="24px" ReadOnly="true" Style="text-align: right;" Width="75px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="">IGST% </td>
                                    <td style="">
                                        <asp:TextBox ID="txtIGSTRate" runat="Server" AutoPostBack="true" CssClass="txt" Height="24px" OnTextChanged="txtIGSTRate_TextChanged" Style="text-align: right;" Width="40px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtIGSTRate" runat="server" FilterType="Numbers,Custom" TargetControlID="txtIGSTRate" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtIGSTAmount" runat="Server" CssClass="txt" Height="24px" ReadOnly="true" Style="text-align: right;" Width="75px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Vat@: </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtVatAmount" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" OnTextChanged="txtVatAmount_TextChanged" ReadOnly="true" Style="text-align: right;" Width="120px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtFREIGHT" runat="server" FilterType="Numbers,Custom" TargetControlID="txtVatAmount" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Sub-Total: </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSubtotal" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" OnTextChanged="txtSubtotal_TextChanged" Style="text-align: right;" Width="120px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtOTHER_AMT" runat="server" FilterType="Numbers,Custom" TargetControlID="txtSubtotal" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Round Off: </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtRoundOff" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" OnTextChanged="txtRoundOff_TextChanged" Style="text-align: right;" Width="120px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom" TargetControlID="txtRoundOff" ValidChars=".,-">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Hamali Amount: </td>
                                    <td align="left">
                                        <asp:TextBox ID="txthamliAmount" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" OnTextChanged="txthamliAmount_TextChanged" Style="text-align: right;" Width="120px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom" TargetControlID="txthamliAmount" ValidChars=".,-">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Grand Total: </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBILL_AMOUNT" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px" OnTextChanged="txtBILL_AMOUNT_TextChanged" ReadOnly="true" Style="text-align: right;" Width="120px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtBILL_AMOUNT" runat="server" FilterType="Numbers,Custom" TargetControlID="txtBILL_AMOUNT" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">TCS%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtTCSRate" runat="Server" CssClass="txt" Width="40px" AutoPostBack="true"
                                            OnTextChanged="txtTCSRate_TextChanged" TabIndex="12" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTCSRate">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtTCSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                            OnTextChanged="txtTCSAmt_TextChanged" ReadOnly="true" Style="text-align: right;"
                                            Width="75px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtTCSAmt" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Net Payable:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtTCSNet_Payable" runat="Server" CssClass="txt" ReadOnly="true"
                                            Width="120px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtTCSNet_Payable_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTCSNet_Payable">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
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
            <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="GhostWhite" Width="955px"
                BorderColor="Teal" BorderWidth="1px" Height="420px" BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 10%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
                <table width="80%" align="center" cellspacing="5">
                    <tr>
                        <td colspan="2" align="center" style="background-color: lightslategrey; color: White;">
                            <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Text="Item Details"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
