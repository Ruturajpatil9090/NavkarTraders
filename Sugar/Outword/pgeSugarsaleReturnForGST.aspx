<%@ Page Title="Sale Return" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeSugarsaleReturnForGST.aspx.cs" Inherits="Sugar_pgeSugarsaleReturnForGST" %>

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
    <script type="text/javascript">
        //        function a(checkbox) {
        //            debugger;
        //            var id = checkbox.parentNode.parentNode.index;
        //            var qntl = $('<%=grdPopup.ClientID %>').find("tr:eq(1)").find("td:eq(2)").text();
        //            alert(id);
        //        }
        $(document).ready(function () {
            $('#chkSelect').change(function () {
                debugger;
                var counter = 0;
                $("#<%=grdPopup.ClientID%> input[id*='chkSelect']:checkbox").each(function (index) {
                    if ($(this).is(':checked'))
                        counter++;
                });
                var the_label = document.getElementById('spnQntl');
                the_label.textContent = counter;
                //alert(counter);
            })
        });

    </script>
    <script type="text/javascript">
        function SB() {
            var billno = document.getElementById('<%=txtDOC_NO.ClientID %>').value;

            window.open('../Report/rptReturnSaleBillForGST.aspx?billno=' + billno);
        }
        function RSBPen(DO) {
            var Action = 1;
            window.open('../Outword/pgeSugarsaleReturnForGST.aspx?srid=' + DO + '&Action=' + Action, "_self");
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
                if (hdnfClosePopupValue == "txtTransportCode") {
                    document.getElementById("<%=txtTransportCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblTransportName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtTransportCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBROKER") {
                    document.getElementById("<%=txtBROKER.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLBROKERNAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBROKER.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtFromAc") {
                    document.getElementById("<%=txtFromAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblFrom_Ac.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtFromAc.ClientID %>").focus();
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
                if (hdnfClosePopupValue == "txtPURCNO") {
                    document.getElementById("<%=txtPURCNO.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfyearcode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[7].innerText;
                    document.getElementById("<%=hdnfQty.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[6].innerText;
                    document.getElementById("<%=txtPURCNO.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtGSTRateCode") {
                    document.getElementById("<%=txtGSTRateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblGSTRateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGSTRateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEditDoc_No") {
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



        function PurNo(e) {
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



        function party(e) {
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

        }

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

        }

        function Mill(e) {
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

        function UnitCode(e) {
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
        function Brokercode(e) {
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
        function fromac(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnFromAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtFromAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtFromAc.ClientID %>").val(unit);
                __doPostBack("txtFromAc", "TextChanged");

            }
        }
        function gstcode(e) {
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
        function transport(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnTransport.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtTransportCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtTransportCode.ClientID %>").val(unit);
                __doPostBack("txtTransportCode", "TextChanged");

            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }
        }
        function itmcode(e) {
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
                if (hdnfClosePopupValue == "txtPURCNO") {
                    document.getElementById("<%=txtPURCNO.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtUnit_Code") {
                    document.getElementById("<%=txtUnit_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtAC_CODE") {
                    document.getElementById("<%=txtAC_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBROKER") {
                    document.getElementById("<%=txtBROKER.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTransportCode") {
                    document.getElementById("<%=txtTransportCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtITEM_CODE") {
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtFromAc") {
                    document.getElementById("<%=txtFromAc.ClientID %>").focus();
                }
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";

            }

        });
    </script>
    <script type="text/javascript">
        function Back() {

            //alert(td);
            window.open('../Outword/PgeShugarSalerUtility.aspx', '_self');
        }
        function EInovice() {
            var dono = document.getElementById('<%=txtDOC_NO.ClientID %>').value;
            var ID = document.getElementById('<%=hdnf.ClientID %>').value;
            window.open('../Utility/pgeEInovice.aspx?dono=' + dono + '&Type=RS&ID=' + ID, "_self");
        }
        function EInoviceEwayBill() {
            var dono = document.getElementById('<%=txtDOC_NO.ClientID %>').value;
            var ID = document.getElementById('<%=hdnf.ClientID %>').value;
            window.open('../Utility/pgeEInoviceAndEwayBill.aspx?dono=' + dono + '&Type=RS&ID=' + ID ,"_self");
        }
        function GEway() {
            var dono = document.getElementById('<%=txtDOC_NO.ClientID %>').value;
            var ID = document.getElementById('<%=hdnf.ClientID %>').value;
            window.open('../Utility/pgeEwayBill.aspx?dono=' + dono + '&Type=RS&ID=' + ID, "_self");
        }
    </script>
    <script type="text/javascript">
        function DeleteConform() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= hdnfreturndoc.ClientID %>").value;
            var Autoid = document.getElementById("<%= hdnfreturnid.ClientID %>").value;

            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';

            var XML = "<ROOT><SaleReturnHead doc_no='" + DocNo + "' srid='" + Autoid + "' Company_Code='" + Company_Code + "' " +
                    "Year_Code='" + Year_Code + "'></SaleReturnHead></ROOT>";
            var spname = "SaleReturn";
            var status = document.getElementById("<%= btnDelete.ClientID %>").value;
            ProcessXML(XML, status, spname);
        }
        function Validate() {
            $("#loader").show();

            if ($("#<%=txtITEM_CODE.ClientID %>").val() != "") {
                alert('Please add details');
                $("#<%=txtITEM_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }


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

            if ($("#<%=txtAC_CODE.ClientID %>").val() == "" || $("#<%=txtAC_CODE.ClientID %>").val() == "0") {
                $("#<%=txtAC_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtFromAc.ClientID %>").val() == "" || $("#<%=txtFromAc.ClientID %>").val() == "0") {
                $("#<%=txtFromAc.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtUnit_Code.ClientID %>").val() == "" || $("#<%=txtUnit_Code.ClientID %>").val() == "0") {
                $("#<%=txtUnit_Code.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtMILL_CODE.ClientID %>").val() == "" || $("#<%=txtMILL_CODE.ClientID %>").val() == "0") {
                $("#<%=txtMILL_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }

            return true;
        }
        function pagevalidation() {
            debugger;
            $("#loader").show();
            var Doc_no = 0, SaleId = 0, SaleDetailId = 0;
            var XML = "<ROOT>";
            var spname = "SaleReturn";
            var status = document.getElementById("<%= btnSave.ClientID %>").value;
            if (status == "Update") {
                var Doc_no = document.getElementById("<%= hdnfreturndoc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfreturndoc.ClientID %>").value;
                var SaleId = document.getElementById("<%= hdnfreturnid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfreturnid.ClientID %>").value;
            }
            var PURCNO = parseFloat($("#<%=txtPURCNO.ClientID %>").val() == "" ? 0 : $("#<%=txtPURCNO.ClientID %>").val());
            //var PURCNO = $("#<%=txtPURCNO.ClientID %>").val() == "" ? 0 : $("#<%=txtPURCNO.ClientID %>").val());
            var d = $("#<%=txtDOC_DATE.ClientID %>").val();
            var DOC_DATE = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
            var AC_CODE = parseFloat($("#<%=txtAC_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtAC_CODE.ClientID %>").val());
            var Unit_Code = parseFloat($("#<%=txtUnit_Code.ClientID %>").val() == "" ? 0 : $("#<%=txtUnit_Code.ClientID %>").val());
            var Mill_Code = parseFloat($("#<%=txtMILL_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtMILL_CODE.ClientID %>").val());
            var TRANSPORT_CODE = parseFloat($("#<%=txtTransportCode.ClientID %>").val() == "" ? 0 : $("#<%=txtTransportCode.ClientID %>").val());
            var Mill_Code = parseFloat($("#<%=txtMILL_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtMILL_CODE.ClientID %>").val());

            var FROM_STATION = $("#<%=txtFROM_STATION.ClientID %>").val() == "" ? 0 : $("#<%=txtFROM_STATION.ClientID %>").val();
            var TO_STATION = ($("#<%=txtTO_STATION.ClientID %>").val() == "" ? 0 : $("#<%=txtTO_STATION.ClientID %>").val());
            var LORRYNO = $("#<%=txtLORRYNO.ClientID %>").val();
            var BROKER = parseFloat($("#<%=txtBROKER.ClientID %>").val() == "" ? 0 : $("#<%=txtBROKER.ClientID %>").val());
            var WEARHOUSE = $("#<%=txtWEARHOUSE.ClientID %>").val();
            var SUBTOTAL = parseFloat($("#<%=txtSUBTOTAL.ClientID %>").val() == "" ? 0 : $("#<%=txtSUBTOTAL.ClientID %>").val());
            var LESS_FRT_RATE = parseFloat($("#<%=txtLESS_FRT_RATE.ClientID %>").val() == "" ? 0 : $("#<%=txtLESS_FRT_RATE.ClientID %>").val());

            var FREIGHT = parseFloat($("#<%=txtFREIGHT.ClientID %>").val() == "" ? 0 : $("#<%=txtFREIGHT.ClientID %>").val());
            var CASH_ADVANCE = parseFloat($("#<%=txtCASH_ADVANCE.ClientID %>").val() == "" ? 0 : $("#<%=txtCASH_ADVANCE.ClientID %>").val());
            var BANK_COMMISSION = parseFloat($("#<%=txtBANK_COMMISSION.ClientID %>").val() == "" ? 0 : $("#<%=txtBANK_COMMISSION.ClientID %>").val());
            var OTHER_AMT = parseFloat($("#<%=txtOTHER_AMT.ClientID %>").val() == "" ? 0 : $("#<%=txtOTHER_AMT.ClientID %>").val());
            var BILL_AMOUNT = parseFloat($("#<%=txtBILL_AMOUNT.ClientID %>").val() == "" ? 0 : $("#<%=txtBILL_AMOUNT.ClientID %>").val());
            var DUE_DAYS = parseFloat($("#<%=txtDUE_DAYS.ClientID %>").val() == "" ? 0 : $("#<%=txtDUE_DAYS.ClientID %>").val());
            var fromAc = $("#<%=txtFromAc.ClientID %>").val() == "" ? 0 : $("#<%=txtFromAc.ClientID %>").val()
            var NETQNTL = parseFloat($("#<%=txtNETQNTL.ClientID %>").val() == "" ? 0 : $("#<%=txtNETQNTL.ClientID %>").val());
            var CGSTRate = parseFloat($("#<%=txtCGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtCGSTRate.ClientID %>").val());
            var CGSTAmount = parseFloat($("#<%=txtCGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtCGSTAmount.ClientID %>").val());
            var IGSTRate = parseFloat($("#<%=txtIGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtIGSTRate.ClientID %>").val());
            var IGSTAmount = parseFloat($("#<%=txtIGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtIGSTAmount.ClientID %>").val());
            var SGSTRate = parseFloat($("#<%=txtSGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtSGSTRate.ClientID %>").val());
            var SGSTAmount = parseFloat($("#<%=txtSGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtSGSTAmount.ClientID %>").val());
            var GstRateCode = parseFloat($("#<%=txtGSTRateCode.ClientID %>").val() == "" ? 0 : $("#<%=txtGSTRateCode.ClientID %>").val());

            var DO_NO = $("#<%=lblDONo.ClientID %>").text() == "" ? 0 : $("#<%=lblDONo.ClientID %>").text();
            //   purcyearcode = lblyearcode.Text != string.Empty ? Convert.ToInt32(lblyearcode.Text) : 0;
            var purcyearcode = $("#<%=lblyearcode.ClientID %>").text() == "" ? 0.00 : $("#<%=lblyearcode.ClientID %>").text();
            var Year_Code = '<%= Session["year"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var USER = '<%= Session["user"] %>';
            var Branch_Id = '<%= Session["Branch_Code"] %>';
            var trntype = "RS";
            var mc = document.getElementById("<%= hdnfmc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfmc.ClientID %>").value;
            if (mc == "" || mc == "&nbsp;") {
                mc = 0;
            }
            var Ac = document.getElementById("<%= hdnfacode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfacode.ClientID %>").value;
            if (Ac == "" || Ac == "&nbsp;") {
                Ac = 0;
            }
            var UN = document.getElementById("<%= hdnfunitcode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfunitcode.ClientID %>").value;
            if (UN == "" || UN == "&nbsp;") {
                UN = 0;
            }
            var Tr = document.getElementById("<%= hdnftransportcode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnftransportcode.ClientID %>").value;
            if (Tr == "" || Tr == "&nbsp;") {
                Tr = 0;
            }
            var Gc = document.getElementById("<%= hdnfGstCode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfGstCode.ClientID %>").value;
            if (Gc == "" || Gc == "&nbsp;") {
                Gc = 0;
            }
            var Br = document.getElementById("<%= hdnfbROKERCODE.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbROKERCODE.ClientID %>").value;
            if (Br == "" || Br == "&nbsp;") {
                Br = 0;
            }
            var fa = document.getElementById("<%= hdnffromac.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnffromac.ClientID %>").value;
            if (fa == "" || fa == "&nbsp;") {
                fa = 0;
            }

            var saleAc = document.getElementById("<%= hdnfsaleAc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfsaleAc.ClientID %>").value;
            var saleAcid = document.getElementById("<%= hdnfsaleAcid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfsaleAcid.ClientID %>").value;

            var saleReturnInsertUpdate = ""; SaleReturnDetail_Insert = ""; saleReturnDetail_Update = ""; SaleReturnDetail_Delete = "";
            var DOCNO = "";
            if (status == "Save") {
                saleReturnInsertUpdate = "Created_By='" + USER + "' Modified_By=''";
                DOCNOC = "PURCNO='" + PURCNO + "'";
            }
            else {
                saleReturnInsertUpdate = "Modified_By='" + USER + "' Created_By=''";
                DOCNOC = "doc_no='" + Doc_no + "' PURCNO='" + PURCNO + "'";
            }

            var ASN_No = $("#<%=txtASNGRNNo.ClientID%>").val();
            var PO_Details = $("#<%=txtPO_Details.ClientID%>").val();
            var TCS_Rate = $("#<%=txtTCSRate.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSRate.ClientID %>").val();
            var TCS_Amt = $("#<%=txtTCSAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSAmt.ClientID %>").val();
            var TCS_Net_Payable = $("#<%=txtTCSNet_Payable.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSNet_Payable.ClientID %>").val();
            var EwayBill_No = $("#<%=txtEwaybill.ClientID %>").val() == "&nbsp" ? "" : $("#<%=txtEwaybill.ClientID %>").val();
            var einvoiceno = $("#<%=txteinvoiceno.ClientID %>").val();
            if (einvoiceno == "&nbsp;") {
                einvoiceno = "";
            }
            var ackno = $("#<%=txtackno.ClientID %>").val();
            if (ackno == "&nbsp;") {
                ackno = "";
            }
            var TDS_Rate = $("#<%=txtTDS.ClientID %>").val() == "" ? 0 : $("#<%=txtTDS.ClientID %>").val();
            var TDS_Amt = $("#<%=txtTDSAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtTDSAmt.ClientID %>").val();

            var Partyshort = document.getElementById("<%= hdnfPartyshort.ClientID %>").value == "&nbsp;" ? "" : document.getElementById("<%= hdnfPartyshort.ClientID %>").value;
            var Brokershort = document.getElementById("<%= hdnfBrokershort.ClientID %>").value == "&nbsp;" ? "" : document.getElementById("<%= hdnfBrokershort.ClientID %>").value;
            var TransportShort_Name = document.getElementById("<%= hdnnfTransportshort.ClientID %>").value == "&nbsp;" ? "" : document.getElementById("<%= hdnnfTransportshort.ClientID %>").value;
            var MillShort_Name = document.getElementById("<%= hdnfMillShort.ClientID %>").value == "&nbsp;" ? "" : document.getElementById("<%= hdnfMillShort.ClientID %>").value;

            XML = XML + "<SaleReturnHead " + DOCNOC + " PurcTranType='' doc_date='" + DOC_DATE + "' Ac_Code='" + AC_CODE + "' Unit_Code='" + Unit_Code + "' " +
               "mill_code='" + Mill_Code + "' FROM_STATION='" + FROM_STATION + "' TO_STATION='" + TO_STATION + "' LORRYNO='" + LORRYNO + "' BROKER='" + BROKER + "' " +
               "wearhouse='" + WEARHOUSE + "' subTotal='" + SUBTOTAL + "' LESS_FRT_RATE='" + LESS_FRT_RATE + "' freight='" + FREIGHT + "' " +
               "cash_advance='" + CASH_ADVANCE + "' bank_commission='" + BANK_COMMISSION + "' OTHER_AMT='" + OTHER_AMT + "' Bill_Amount='" + BILL_AMOUNT + "' " +
               "Due_Days='" + DUE_DAYS + "' NETQNTL='" + NETQNTL + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Id + "' " +
               "" + saleReturnInsertUpdate + " Tran_Type='" + trntype + "'  DO_No='" + DO_NO + "' " +
               "Transport_Code='" + TRANSPORT_CODE + "' CGSTRate='" + CGSTRate + "' CGSTAmount='" + CGSTAmount + "' " +
               "SGSTRate='" + SGSTRate + "' SGSTAmount='" + SGSTAmount + "' IGSTRate='" + IGSTRate + "' IGSTAmount='" + IGSTAmount + "' GstRateCode='" + GstRateCode + "' " +
               "purcyearcode='" + purcyearcode + "' ac='" + Ac + "' uc='" + UN + "' mc='" + mc + "' bc='" + Br + "' srid='" + SaleId + "' sbid='0' bill_to='0' bt='0' gc='" + Gc + "' " +
               "tc='" + Tr + "' FromAc='" + fromAc + "' fa='" + fa + "' PO_Details='" + PO_Details + "' ASN_No='" + ASN_No + "' " +
               "Eway_Bill_No='" + EwayBill_No +  "' IsDeleted='1' TCS_Rate='" + TCS_Rate + "' TCS_Amt='" + TCS_Amt + "' TCS_Net_Payable='" + TCS_Net_Payable + "' einvoiceno='" + einvoiceno + "' ackno='" + ackno + "' TDS_Rate='" + TDS_Rate + "' TDS_Amt='" + TDS_Amt + "'>";

            var saleDetail_Value = ""; concatid = "";
            var Gledger_Insert = ""; Gledger_values = "";
            var Gledger_Delete = "";
            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            var grdrow = gridView.getElementsByTagName("tr");

            var ddid = SaleDetailId;
            debugger;
            for (var i = 1; i < grdrow.length; i++) {

                var detail_id = gridView.rows[i].cells[2].innerHTML;
                var item_code = gridView.rows[i].cells[3].innerHTML;
                var itemname = gridView.rows[i].cells[4].innerHTML;
                var narration = gridView.rows[i].cells[5].innerHTML;
                if (narration == "&nbsp;") {
                    narration = "";
                }

                var Quantal = gridView.rows[i].cells[6].innerHTML;
                var packing = gridView.rows[i].cells[7].innerHTML;
                var bags = gridView.rows[i].cells[8].innerHTML;
                var rate = gridView.rows[i].cells[9].innerHTML;
                var item_Amount = gridView.rows[i].cells[10].innerHTML;
                var id = gridView.rows[i].cells[11].innerHTML;
                var itemId = gridView.rows[i].cells[14].innerHTML;

                if (gridView.rows[i].cells[12].innerHTML == "A") {
                    debugger;
                    XML = XML + "<ReturnDetailInsert doc_no='" + Doc_no + "' detail_id='" + detail_id + "' Tran_Type='" + trntype + "' " +
                        "item_code='" + item_code + "' narration='" + narration + "' Quantal='" + Quantal + "' packing='" + packing + "' " +
                    "bags='" + bags + "' rate='" + rate + "' item_Amount='" + item_Amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' " +
                    "Branch_Code='" + Branch_Id + "' Created_By='" + USER + "' Modified_By='' srid='" + SaleId + "' srdtid='" + ddid + "' ic='" + itemId + "'/>";
                    ddid = ddid + 1;
                }

                else if (gridView.rows[i].cells[12].innerHTML == "U") {
                    var srdid = gridView.rows[i].cells[11].innerHTML;

                    XML = XML + "<ReturnDetail doc_no='" + Doc_no + "' detail_id='" + detail_id + "' Tran_Type='" + trntype + "' " +
                        "item_code='" + item_code + "' narration='" + narration + "' Quantal='" + Quantal + "' packing='" + packing + "' " +
                    "bags='" + bags + "' rate='" + rate + "' item_Amount='" + item_Amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' " +
                    "Branch_Code='" + Branch_Id + "' Created_By='" + USER + "' Modified_By='' srid='" + SaleId + "' srdtid='" + srdid + "' ic='" + itemId + "'/>";
                }
                else if (gridView.rows[i].cells[12].innerHTML == "D") {
                    var srdid = gridView.rows[i].cells[11].innerHTML;

                    XML = XML + "<ReturnDetailDelete doc_no='" + Doc_no + "' detail_id='" + detail_id + "' Tran_Type='" + trntype + "' " +
                        "item_code='" + item_code + "' narration='" + narration + "' Quantal='" + Quantal + "' packing='" + packing + "' " +
                    "bags='" + bags + "' rate='" + rate + "' item_Amount='" + item_Amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' " +
                    "Branch_Code='" + Branch_Id + "' Created_By='" + USER + "' Modified_By='' srid='" + SaleId + "' srdtid='" + srdid + "' ic='" + itemId + "'/>";
                }
            }






            var Debitnara = "" + MillShort_Name + " " + NETQNTL + " Lorry:" + LORRYNO + " Brok:" + Brokershort + " Party:" + Partyshort + " Purc.No:" + PURCNO + "";
            var Creditnara = "" + Partyshort + "" + NETQNTL + " Lorry:" + SUBTOTAL + " /" + NETQNTL + "  Purc.No:" + PURCNO + "";
            var TransportNarration = "" + NETQNTL + "" + CASH_ADVANCE + "" + MillShort_Name + "" + TransportShort_Name + "Lorry:" + LORRYNO + "" + Partyshort + "";
            var TCSNarration = "TCS " + Partyshort + " " + Doc_no + "";
            var TDSNarration = "TDS " + Partyshort + " " + Doc_no + "";

            var ORDER_CODE = 1;

            //Gledger_values = Gledger_values + "('RS','','" + Doc_no + "','" + DOC_DATE + "','" + AC_CODE + "','0','','" + BILL_AMOUNT + "', " +
            //                                         " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + ORDER_CODE + "','D', case when 0='" + saleAc + "' then null else '" + saleAc + "' end,0,'" + Branch_Id + "','RS','" + Doc_no + "'," +
            //                                         " '" + Ac + "','0','8','0')";

            XML = XML + "<Ledger TRAN_TYPE='RS' CASHCREDIT='' DOC_NO='" + Doc_no + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + AC_CODE + "' " +
                                                        "UNIT_code='0' NARRATION='" + Debitnara + "' AMOUNT='" + BILL_AMOUNT + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='D' DRCR_HEAD='" + saleAc + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                        "SORT_TYPE='RS' SORT_NO='" + Doc_no + "' ac='" + Ac + "' vc='0' progid='8' tranid='0'/>";

            ORDER_CODE = ORDER_CODE + 1;
            //Gledger_values = Gledger_values + ",('RS','','" + Doc_no + "','" + DOC_DATE + "',case when 0='" + saleAcid + "' then null else '" + saleAcid + "' end,'0','','" + BILL_AMOUNT + "', " +
            //                                            " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + ORDER_CODE + "','C','" + AC_CODE + "',0,'" + Branch_Id + "','RS','" + Doc_no + "'," +
            //                                            " case when 0='" + saleAcid + "' then null else '" + saleAcid + "' end,'0','8','0'),";

            XML = XML + "<Ledger TRAN_TYPE='RS' CASHCREDIT='' DOC_NO='" + Doc_no + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + saleAc + "' " +
                                                        "UNIT_code='0' NARRATION='" + Creditnara + "' AMOUNT='" + SUBTOTAL + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='C' DRCR_HEAD='" + AC_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                        "SORT_TYPE='RS' SORT_NO='" + Doc_no + "' ac='" + saleAcid + "' vc='0' progid='8' tranid='0'/>";

            if (FREIGHT > 0) {
                ORDER_CODE = ORDER_CODE + 1;
                //Gledger_values = Gledger_values + ",('RS','','" + Doc_no + "','" + DOC_DATE + "','" + TRANSPORT_CODE + "','0','','" + CASH_ADVANCE + "', " +
                //                                        " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + ORDER_CODE + "','C','" + AC_CODE + "',0,'" + Branch_Id + "','RS','" + Doc_no + "'," +
                //                                        " '" + Tr + "','0','8','0'),";

                XML = XML + "<Ledger TRAN_TYPE='RS' CASHCREDIT='' DOC_NO='" + Doc_no + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["TRANSPORT_AC"] %>' + "' " +
                                                       "UNIT_code='0' NARRATION='" + TransportNarration + "' AMOUNT='" + FREIGHT + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                       "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='D' DRCR_HEAD='" + AC_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                       "SORT_TYPE='RS' SORT_NO='" + Doc_no + "' ac='" + '<%=Session["TRANSPORT_ACid"] %>' + "' vc='0' progid='8' tranid='0'/>";

            }

            if (CASH_ADVANCE > 0) {
                ORDER_CODE = ORDER_CODE + 1;

                XML = XML + "<Ledger TRAN_TYPE='RS' CASHCREDIT='' DOC_NO='" + Doc_no + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + TRANSPORT_CODE + "' " +
                                                       "UNIT_code='0' NARRATION='" + TransportNarration + "' AMOUNT='" + CASH_ADVANCE + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                       "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='C' DRCR_HEAD='" + AC_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                       "SORT_TYPE='RS' SORT_NO='" + Doc_no + "' ac='" + Tr + "' vc='0' progid='8' tranid='0'/>";

            }

            if (CGSTAmount > 0) {
                ORDER_CODE = ORDER_CODE + 1;

                XML = XML + "<Ledger TRAN_TYPE='RS' CASHCREDIT='' DOC_NO='" + Doc_no + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["ReturnSaleCGST"] %>' + "' " +
                                                       "UNIT_code='0' NARRATION='" + Creditnara + "' AMOUNT='" + CGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                       "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='C' DRCR_HEAD='" + AC_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                       "SORT_TYPE='RS' SORT_NO='" + Doc_no + "' ac='" + '<%=Session["Return_Sale_CGST_Acid"] %>' + "' vc='0' progid='8' tranid='0'/>";
            }
            if (SGSTAmount > 0) {
                ORDER_CODE = ORDER_CODE + 1;

                XML = XML + "<Ledger TRAN_TYPE='RS' CASHCREDIT='' DOC_NO='" + Doc_no + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["ReturnSaleSGST"] %>' + "' " +
                                                       "UNIT_code='0' NARRATION='" + Creditnara + "' AMOUNT='" + SGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                       "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='C' DRCR_HEAD='" + AC_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                       "SORT_TYPE='RS' SORT_NO='" + Doc_no + "' ac='" + '<%=Session["Return_Sale_SGST_Acid"] %>' + "' vc='0' progid='8' tranid='0'/>";
            }
            if (IGSTAmount > 0) {
                ORDER_CODE = ORDER_CODE + 1;

                XML = XML + "<Ledger TRAN_TYPE='RS' CASHCREDIT='' DOC_NO='" + Doc_no + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["ReturnSaleIGST"] %>' + "' " +
                                                       "UNIT_code='0' NARRATION='" + Creditnara + "' AMOUNT='" + IGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                       "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='C' DRCR_HEAD='" + AC_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                       "SORT_TYPE='RS' SORT_NO='" + Doc_no + "' ac='" + '<%=Session["Return_Sale_IGST_Acid"] %>' + "' vc='0' progid='8' tranid='0'/>";
            }
            if (TCS_Amt > 0) {
                ORDER_CODE = ORDER_CODE + 1;

                XML = XML + "<Ledger TRAN_TYPE='RS' CASHCREDIT='' DOC_NO='" + Doc_no + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + AC_CODE + "' " +
                                                       "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                       "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='D' DRCR_HEAD='" + AC_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                       "SORT_TYPE='RS' SORT_NO='" + Doc_no + "' ac='" + Ac + "' vc='0' progid='8' tranid='0'/>";

                ORDER_CODE = ORDER_CODE + 1;

                XML = XML + "<Ledger TRAN_TYPE='RS' CASHCREDIT='' DOC_NO='" + Doc_no + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                                        "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='C' DRCR_HEAD='" + AC_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                        "SORT_TYPE='RS' SORT_NO='" + Doc_no + "' ac='" + <%=Session["SaleTCSAcid"] %> + "' vc='0' progid='8' tranid='0'/>";
            }
            if (TDS_Amt > 0) {
                ORDER_CODE = ORDER_CODE + 1;

                XML = XML + "<Ledger TRAN_TYPE='RS' CASHCREDIT='' DOC_NO='" + Doc_no + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + AC_CODE + "' " +
                                                       "UNIT_code='0' NARRATION='" + TDSNarration + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                       "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='C' DRCR_HEAD='" + AC_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                       "SORT_TYPE='RS' SORT_NO='" + Doc_no + "' ac='" + Ac + "' vc='0' progid='8' tranid='0'/>";

                ORDER_CODE = ORDER_CODE + 1;

                XML = XML + "<Ledger TRAN_TYPE='RS' CASHCREDIT='' DOC_NO='" + Doc_no + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleTDSAc"] %>' + "' " +
                                                        "UNIT_code='0' NARRATION='" + TDSNarration + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='D' DRCR_HEAD='" + AC_CODE + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                        "SORT_TYPE='RS' SORT_NO='" + Doc_no + "' ac='" + <%=Session["SaleTDSacid"] %> + "' vc='0' progid='8' tranid='0'/>";
            }

            XML = XML + "</SaleReturnHead></ROOT>";
            ProcessXML(XML, status, spname);


        }
        function ProcessXML(XML, status, spname) {
            debugger;
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
                        window.open('../Outword/pgeSugarsaleReturnForGST.aspx?srid=' + id + '&Action=1', "_self");

                    }
                }
                else {
                    var num = parseInt(response.d);

                    if (isNaN(num)) {
                        alert(response.d)

                    }
                    else {
                        window.open('../Outword/PgeShugarSalerUtility.aspx', "_self");
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
    <%-- <script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Return Sale   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px;">
        <img height="20%" width="20%" src="../Images/ajax-loader3.gif" />
    </div>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfmc" runat="server" />
            <asp:HiddenField ID="hdnfunitcode" runat="server" />
            <asp:HiddenField ID="hdnfacode" runat="server" />
            <asp:HiddenField ID="hdnfGstCode" runat="server" />
            <asp:HiddenField ID="hdnftransportcode" runat="server" />
            <asp:HiddenField ID="hdnfbROKERCODE" runat="server" />
            <asp:HiddenField ID="hdnffromac" runat="server" />
            <asp:HiddenField ID="hdnfreturndoc" runat="server" />
            <asp:HiddenField ID="hdnfreturnid" runat="server" />

            <asp:HiddenField ID="hdnfsaleAc" runat="server" />
            <asp:HiddenField ID="hdnfsaleAcid" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />

            <asp:HiddenField ID="hdnfMillShort" runat="server" />
            <asp:HiddenField ID="hdnnfTransportshort" runat="server" />
            <asp:HiddenField ID="hdnfPartyshort" runat="server" />
            <asp:HiddenField ID="hdnfBrokershort" runat="server" />
            <asp:HiddenField ID="hdnfQty" runat="server" />

            <asp:HiddenField ID="hdnfSaleTDS" runat="server" />
            <asp:HiddenField ID="hdnfTCSRate" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table width="80%" align="left">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                TabIndex="33" Height="24px" ValidationGroup="save" OnClick="btnAdd_Click" />
                            &nbsp;
                        <asp:Button OnClientClick="if (!Validate()) return false;" ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="add" OnClick="btnSave_Click" TabIndex="34" />
                            &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            TabIndex="35" Height="24px" ValidationGroup="save" OnClick="btnEdit_Click" />
                            &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            TabIndex="36" Height="24px" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" />
                            &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            TabIndex="37" Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" />&nbsp;
                        <asp:Button runat="server" ID="btnPrintSaleBill" Text="Print" CssClass="btnHelp"
                            Width="80px" Height="24px" OnClientClick="SB();" />
                            &nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                            TabIndex="38" Height="24px" ValidationGroup="save" OnClientClick="Back()" />
                            <asp:Button runat="server" ID="btnGenEinvoice" Text="Generate EInvoice" CssClass="btnHelp"
                                Width="120px" Height="24px" OnClientClick="EInovice();" />
                            <asp:Button runat="server" ID="btnGenEInvoiceEwaybill" Text="Gen EInvoice & EwayBill" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClientClick="EInoviceEwayBill();" />
                            <asp:Button runat="server" ID="btnGentare_EWayBill" Text="Gentare EWayBill" CssClass="btnHelp"
                                Width="90px" Height="24px" OnClientClick="GEway();" />
                            <asp:Button runat="server" ID="btnCancleEinvoice" Text="Cancle EInvoice" CssClass="btnHelp"
                                Width="120px" Height="24px" OnClientClick="ConfirmCancle();" OnClick="btnCancleEInvoice_Click" />
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
                <table width="100%" align="left" cellspacing="5">
                    <tr>
                        <td align="right">Change No:</td>
                        <td colspan="4">
                            <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="txtxf" FilterType="Numbers" InvalidChars="."
                                TargetControlID="txtEditDoc_No">
                            </ajax1:FilteredTextBoxExtender>

                            &nbsp;<asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
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
                            <asp:Label ID="lblDoc_Id" runat="server" Text="" Font-Names="verdana"
                                ForeColor="Blue" Font-Bold="true" Font-Size="12px" Visible="false"></asp:Label>
                            <asp:Button ID="btntxtdoc_no" runat="server" Text="..." Width="80px" OnClick="btntxtDOC_NO_Click"
                                CssClass="btnHelp" Visible="true" Height="24px" />
                        </td>
                        <td align="right" style="width: 10%;">Purc No:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtPURCNO" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" onkeydown="PurNo(event);" OnTextChanged="txtPURCNO_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtPURCNO" runat="server" Text="..." OnClick="btntxtPURCNO_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            &nbsp;DO:<asp:Label runat="server" ID="lblDONo" ForeColor="Yellow"></asp:Label>
                            &nbsp;Year:<asp:Label runat="server" ID="lblyearcode" ForeColor="Yellow"></asp:Label>
                        </td>
                        <td align="left" colspan="1">Date:
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
                        <td align="right" style="width: 10%;">From Ac:
                        </td>
                        <td align="left" colspan="4" style="width: 10%;">
                            <asp:TextBox ID="txtAC_CODE" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" onkeydown="party(event);" OnTextChanged="txtAC_CODE_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtAC_CODE" runat="server" Text="..." OnClick="btntxtAC_CODE_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="LblPartyname" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;">Bill To:</td>
                        <td align="left" colspan="4" style="width: 10%;">
                            <asp:TextBox ID="txtFromAc" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" onkeydown="fromac(event);" OnTextChanged="txtFromAc_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btnFromAc" runat="server" Text="..." OnClick="btnFromAc_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblFrom_Ac" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;">Ship To:
                        </td>
                        <td align="left" colspan="4" style="width: 10%;">
                            <asp:TextBox ID="txtUnit_Code" runat="Server" CssClass="txt" TabIndex="4" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" onkeydown="UnitCode(event);" Height="24px" OnTextChanged="txtUnit_Code_TextChanged"></asp:TextBox>
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
                                Style="text-align: right;" AutoPostBack="false" onkeydown="Mill(event);" OnTextChanged="txtMILL_CODE_TextChanged"
                                Height="24px"></asp:TextBox>
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
                        </td>
                        <td align="right">To:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTO_STATION" runat="Server" CssClass="txt" TabIndex="7" Width="200px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTO_STATION_TextChanged"
                                Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Lorry No:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtLORRYNO" runat="Server" CssClass="txt" TabIndex="8" Width="200px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtLORRYNO_TextChanged"
                                Height="24px"></asp:TextBox>
                        </td>
                        <td align="right">Wear House:
                        </td>
                        <td align="left">
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
                                Style="text-align: left;" AutoPostBack="false" onkeydown="Brokercode(event);" OnTextChanged="txtBROKER_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtBROKER" runat="server" Text="..." OnClick="btntxtBROKER_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="LBLBROKERNAME" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">GST Rate Code
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtGSTRateCode" runat="Server" CssClass="txt" TabIndex="11" Width="80px"
                                Style="text-align: left;" AutoPostBack="false" onkeydown="gstcode(event);" OnTextChanged="txtGSTRateCode_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtGSTRateCode" runat="server" Text="..." OnClick="btntxtGSTRateCode_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblGSTRateName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px; vertical-align: top;" align="center">
                            <asp:Button ID="btnOpenDetailsPopup" runat="server" Text="ADD" CssClass="btnHelp"
                                Width="80px" Height="24px" OnClick="btnOpenDetailsPopup_Click" TabIndex="12" Visible="false" />
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
                                    Height="24px" Style="text-align: right;" AutoPostBack="false" onkeydown="itmcode(event);" OnTextChanged="txtITEM_CODE_TextChanged"></asp:TextBox>
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
                                    TextMode="MultiLine" Height="50px" Style="text-align: left;" AutoPostBack="false"
                                    OnTextChanged="txtITEM_NARRATION_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                    Height="25px" OnClick="btnAdddetails_Click" TabIndex="20" />
                                <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp"
                                    Width="80px" Height="25px" OnClick="btnClosedetails_Click" TabIndex="21" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table width="100%" align="left" cellspacing="5">
                    <tr>
                        <td align="left" colspan="4" style="width: 70%;">
                            <div style="width: 80%; position: relative; vertical-align: top;">
                                <asp:UpdatePanel ID="upGrid" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlgrdDetail" runat="server" align="left" ScrollBars="Both" Height="180px"
                                            Width="1000px" BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true"
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
                            <div style="margin-top: 200px; margin-left: 80px;">
                                Net Qntl:
                            <asp:TextBox ID="txtNETQNTL" runat="Server" CssClass="txt" ReadOnly="true" Width="120px"
                                Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtNETQNTL_TextChanged"
                                TabIndex="22" Height="24px" onKeyDown="save(event);"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtNETQNTL" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtNETQNTL">
                                </ajax1:FilteredTextBoxExtender>
                                Due Days:
                            <asp:TextBox ID="txtDUE_DAYS" runat="Server" CssClass="txt" TabIndex="23" Width="120px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtDUE_DAYS_TextChanged"
                                Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="filtertxtDUE_DAYS" runat="server" FilterType="Numbers"
                                    TargetControlID="txtDUE_DAYS">
                                </ajax1:FilteredTextBoxExtender>
                                PO Details:
                                <asp:TextBox runat="server" ID="txtPO_Details" CssClass="txt" Width="120px" Height="24px"
                                    TabIndex="33"></asp:TextBox>
                            </div>
                            <div style="margin-top: 10px; margin-left: 70px;">
                                Transport:
                            <asp:TextBox ID="txtTransportCode" runat="Server" CssClass="txt" TabIndex="24" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" onkeydown="transport(event);" Height="24px" OnTextChanged="txtTransportCode_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnTransport" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                    Width="20px" OnClick="btnTransport_Click" />
                                <asp:Label ID="lblTransportName" runat="server" CssClass="lblName"></asp:Label>
                                ASN/GRN No:
                                <asp:TextBox runat="server" ID="txtASNGRNNo" CssClass="txt" Width="120px" Height="24px"
                                    TabIndex="35"></asp:TextBox>
                                EwayBill No:&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtEwaybill" runat="Server" CssClass="txt" TabIndex="36" Width="200px"
                                    Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                            </div>
                            <div style="margin-top: 10px; margin-left: 70px;">
                                EInvoice No:
                                <asp:TextBox ID="txteinvoiceno" runat="Server" CssClass="txt" TabIndex="4" Width="120px"
                                    Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                                ACKNo:
                                <asp:TextBox ID="txtackno" runat="Server" CssClass="txt" TabIndex="4" Width="150px"
                                    Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                            </div>
                        </td>
                        <td style="width: 70%;" align="right">
                            <table width="100%" cellspacing="4" cellpadding="3">
                                <tr>
                                    <td align="left" style="width: 30%;">Subtotal:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSUBTOTAL" runat="Server" CssClass="txt" ReadOnly="true" Width="140px"
                                            Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtSUBTOTAL_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtSUBTOTAL" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtSUBTOTAL">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>CGST%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtCGSTRate" runat="Server" CssClass="txt" Width="42px" TabIndex="25"
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
                                        <asp:TextBox ID="txtSGSTRate" runat="Server" CssClass="txt" Width="42px" TabIndex="26"
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
                                            OnTextChanged="txtIGSTRate_TextChanged" TabIndex="27" Style="text-align: right;"
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
                                        <asp:TextBox ID="txtLESS_FRT_RATE" runat="Server" CssClass="txt" TabIndex="28" Width="52px"
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
                                    <td align="left">Bank Commission:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBANK_COMMISSION" runat="Server" CssClass="txt" TabIndex="29"
                                            Width="140px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtBANK_COMMISSION_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtBANK_COMMISSION" runat="server" FilterType="Numbers,Custom"
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
                                            TabIndex="30" Width="140px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtOTHER_AMT" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtOTHER_AMT" ValidChars=".,-">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Cash Advance:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCASH_ADVANCE" runat="Server" CssClass="txt" TabIndex="31" Width="140px"
                                            Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtCASH_ADVANCE_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtCASH_ADVANCE" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtCASH_ADVANCE">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Bill Amount:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBILL_AMOUNT" runat="Server" CssClass="txt" ReadOnly="true" TabIndex="32"
                                            Width="140px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtBILL_AMOUNT_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtBILL_AMOUNT" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtBILL_AMOUNT">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">TCS%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtTCSRate" runat="Server" CssClass="txt" Width="52px" AutoPostBack="true"
                                            OnTextChanged="txtTCSRate_TextChanged" TabIndex="30" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTCSRate">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtTCSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                            OnTextChanged="txtTCSAmt_TextChanged" ReadOnly="false" Style="text-align: right;"
                                            Width="80px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtTCSAmt" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">TDS%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtTDS" runat="Server" CssClass="txt" Width="52px" AutoPostBack="true"
                                            OnTextChanged="txtTDS_TextChanged" TabIndex="53" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTDS">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtTDSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                            TabIndex="54" OnTextChanged="txtTDSAmt_TextChanged" ReadOnly="false" Style="text-align: right;"
                                            Width="80px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtTDSAmt" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Net Payable:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtTCSNet_Payable" runat="Server" CssClass="txt" ReadOnly="true"
                                            TabIndex="31" Width="140px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtTCSNet_Payable_TextChanged"
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
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Multiple Vouchers:<asp:TextBox runat="server" ID="txtMultipleVouchers"
                                ReadOnly="true" placeholder="Please Enter Comma separate numbers." Width="300px"
                                Height="20px" />
                            <ajax1:FilteredTextBoxExtender runat="server" ID="dsda" TargetControlID="txtMultipleVouchers"
                                FilterType="Numbers,Custom" ValidChars=",">
                            </ajax1:FilteredTextBoxExtender>
                            &nbsp;<asp:Button Text="Select" runat="server" ID="btnSelectVoucers" CssClass="btnSubmit"
                                Enabled="false" OnClick="btnSelectVoucers_Click" />&nbsp;Quintal:<span id="spnQntl"></span>
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
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSelect" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%-- <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="GhostWhite" Width="955px"
                BorderColor="Teal" BorderWidth="1px" Height="320px" BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
            </asp:Panel>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
