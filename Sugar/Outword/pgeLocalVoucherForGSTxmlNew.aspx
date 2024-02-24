<%@ Page Title="Local Voucher" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeLocalVoucherForGSTxmlNew.aspx.cs" Inherits="Sugar_pgeLocalVoucherForGSTxml" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

    </script>--%>
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
                if (hdnfClosePopupValue == "txtUnit_Code") {
                    document.getElementById("<%=txtUnit_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBroker_CODE") {
                    document.getElementById("<%=txtBroker_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtMILL_CODE") {
                    document.getElementById("<%=txtMILL_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtdoc_no") {
                    //document.getElementById("<%=txtdoc_no.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    // document.getElementById("<%=txtSUFFIX.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtdoc_no.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtGRADE") {
                    document.getElementById("<%=txtGRADE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNarration1") {
                    document.getElementById("<%=txtNarration1.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNarration2") {
                    document.getElementById("<%=txtNarration2.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNarration3") {
                    document.getElementById("<%=txtNarration3.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNarration4") {
                    document.getElementById("<%=txtNarration4.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtDONO") {
                    document.getElementById("<%=txtDONO.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtITEM_CODE") {
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTDS_AC") {
                    document.getElementById("<%=txtTDS_AC.ClientID %>").focus();
                }
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }
        });
    </script>
    <%-- <script type="text/javascript" src="../JS/DateValidation.js">
    </script>--%>
    <script type="text/javascript">
        function p(VNO, type, party) {
            window.open('../Report/rptCommisionBill.aspx?VNO=' + VNO + '&type=' + type + '&party=' + party);
        }
        function EInovice() {
            var dono = document.getElementById('<%=txtdoc_no.ClientID %>').value;
            var ID = document.getElementById('<%=hdnf.ClientID %>').value;
            var DoIDNo = document.getElementById('<%=txtDONO.ClientID %>').value;
            window.open('../Utility/pgeEInovice.aspx?dono=' + dono + '&Type=LV&ID=' + ID + '&DoIDNo=' + DoIDNo);
        }
        function Transfer() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= txtdoc_no.ClientID %>").value;
            var Autoid = document.getElementById("<%= hdnflvid.ClientID %>").value;

            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';

            var XML = "<ROOT><Commission doc_no='" + DocNo + "' commissionid='" + Autoid + "' Company_Code='" + Company_Code + "' " +
                   "Year_Code='" + Year_Code + "'></Commission></ROOT>";
            var spname = "Commission";
            var status = "Transfer";
            ProcessXML(XML, status, spname);
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

        function Back() {
            window.open('../Outword/PgeLocalVoucherUtility.aspx', '_self');
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
                    document.getElementById("<%=lblAc_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtAC_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtUnit_Code") {
                    document.getElementById("<%=txtUnit_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblUnitName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtUnit_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBroker_CODE") {
                    document.getElementById("<%=txtBroker_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBroker_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBroker_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtMILL_CODE") {
                    document.getElementById("<%=txtMILL_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblMill_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtMILL_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtdoc_no") {
                    document.getElementById("<%=txtdoc_no.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtSUFFIX.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtdoc_no.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtGRADE") {
                    document.getElementById("<%=txtGRADE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtTRANSPORT_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNarration1") {
                    document.getElementById("<%=txtNarration1.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtNarration1.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNarration2") {
                    document.getElementById("<%=txtNarration2.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtNarration2.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNarration3") {
                    document.getElementById("<%=txtNarration3.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtNarration3.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtNarration4") {
                    document.getElementById("<%=txtNarration4.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtNarration4.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtDONO") {
                    document.getElementById("<%=txtDONO.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtDONO.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTRANSPORT_CODE") {
                    document.getElementById("<%=txtTRANSPORT_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtTRANSPORT_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGSTRateCode") {
                    document.getElementById("<%=txtGSTRateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblGSTRateName.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGSTRateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtITEM_CODE") {
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLITEMNAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").focus();

                }
                if (hdnfClosePopupValue == "txtTDS_AC") {
                    document.getElementById("<%= txtTDS_AC.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblTDS_AC_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtTDS_AC.ClientID %>").focus();
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
    <script language="JavaScript" type="text/javascript">
        //  window.onbeforeunload = confirmExit;
        // function confirmExit() {
        //    return "Are you sure you want to exit this page?";
        //}
    </script>
    <script type="text/javascript">
        debugger;
        function Party(e) {
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
        function Unit(e) {
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
        function Broker(e) {
            debugger;
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
        function Grade(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtGRADE.ClientID %>").click();

            }
            //  if (e.keyCode == 9) {
            //    e.preventDefault();
            //   var unit = $("#<%=txtGRADE.ClientID %>").val();

            //   unit = "0" + unit;
            //   $("#<%=txtGRADE.ClientID %>").val(unit);
            //   __doPostBack("txtGRADE", "TextChanged");

            // }

        }
        function Transport(e) {
            debugger;
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
        function Mill(e) {
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
        function GstRate(e) {
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
        function Narration1(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtNarration1.ClientID %>").click();

            }
            //  if (e.keyCode == 9) {
            //  e.preventDefault();
            // var unit = $("#<%=txtNarration1.ClientID %>").val();

            //    unit = "0" + unit;
            //   $("#<%=txtNarration1.ClientID %>").val(unit);
            // __doPostBack("txtNarration1", "TextChanged");

            // }

        }
        function Narration2(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtNarration2.ClientID %>").click();

            }
            //if (e.keyCode == 9) {
            //   e.preventDefault();
            //  var unit = $("#<%=txtNarration2.ClientID %>").val();

            //  unit = "0" + unit;
            //  $("#<%=txtNarration2.ClientID %>").val(unit);
            //     __doPostBack("txtNarration2", "TextChanged");
            //
            // }

        }
        function Narration3(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtNarration3.ClientID %>").click();

            }
            //if (e.keyCode == 9) {
            //   e.preventDefault();
            //   var unit = $("#<%=txtNarration3.ClientID %>").val();

            //  unit = "0" + unit;
            // $("#<%=txtNarration3.ClientID %>").val(unit);
            // __doPostBack("txtNarration3", "TextChanged");

            // }

        }
        function Narration4(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtNarration4.ClientID %>").click();

            }
            // if (e.keyCode == 9) {
            //  e.preventDefault();
            //  var unit = $("#<%=txtNarration4.ClientID %>").val();

            // unit = "0" + unit;
            // $("#<%=txtNarration4.ClientID %>").val(unit);
            //  __doPostBack("txtNarration4", "TextChanged");

            //}

        }
        function DO(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtDONO.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtDONO.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtDONO.ClientID %>").val(unit);
                __doPostBack("txtDONO", "TextChanged");

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


        }

        function tdsac(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtTDS_AC.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtTDS_AC.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtTDS_AC.ClientID %>").val(unit);
                __doPostBack("txtTDS_AC", "TextChanged");

            }

        }

    </script>
    <script type="text/javascript">
        function DeleteConform() {
            debugger;
            $("#loader").show();

            if ($("#<%=txteinvoiceno.ClientID %>").val() != "" && $("#<%=txtackno.ClientID %>").val() != "") {
                alert('The EInvoice Bill is Generated so it cannot be Deleted');
                $("#loader").hide();
                return false;
            }
            else {

                var DocNo = document.getElementById("<%= hdnflvdoc.ClientID %>").value;
                var Autoid = document.getElementById("<%= hdnflvid.ClientID %>").value;
                var Vouchertype = $("#<%=drpVouchertype.ClientID %>").val();
                var Branch_Code = '<%= Session["Branch_Code"] %>';
                var Company_Code = '<%= Session["Company_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';

                var XML = "<ROOT><Commission doc_no='" + DocNo + "' commissionid='" + Autoid + "' Company_Code='" + Company_Code + "' " +
                       "Year_Code='" + Year_Code + "' Tran_Type='" + Vouchertype + "'></Commission></ROOT>";
                var spname = "Commission";
                var status = document.getElementById("<%= btnDelete.ClientID %>").value;
                $.ajax({
                    type: "POST",
                    url: "../xmlExecuteDMLQry.asmx/ExecuteXMLQry",
                    data: '{XML:"' + XML + '",status:"' + status + '",spname:"' + spname + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //success: OnSuccess,
                    //failure: function (response) {
                    //    alert(response.d);
                    //    $("#loader").hide();
                    //},
                    //error: function (response) {
                    //    alert(response.d);
                    //    $("#loader").hide();
                    //}
                });
                window.open('../Outword/PgeLocalVoucherUtility.aspx', "_self");
                //   ProcessXML(XML, status, spname);
            }
        }
        function Validate() {
            $("#loader").show();
            debugger;

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

            if ($("#<%=txtAC_CODE.ClientID %>").val() == "") {
                $("#<%=txtAC_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }

            if ($("#<%=txtQNTL.ClientID %>").val() == "" || $("#<%=txtQNTL.ClientID %>").val() == "0") {
                $("#<%=txtQNTL.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtPACKING.ClientID %>").val() == "" || $("#<%=txtPACKING.ClientID %>").val() == "0") {
                $("#<%=txtPACKING.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }

            if ($("#<%=txtBAGS.ClientID %>").val() == "" || $("#<%=txtBAGS.ClientID %>").val() == "0") {
                $("#<%=txtBAGS.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }

            if ($("#<%=txtMILL_CODE.ClientID %>").val() == "" || $("#<%=txtMILL_CODE.ClientID %>").val() == "0") {
                $("#<%=txtMILL_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }

            if ($("#<%=drpTDS_Applicable.ClientID %>").val() == "Y") {
                if ($("#<%=txtTDS_AC.ClientID %>").val() == "" || $("#<%=txtTDS_AC.ClientID %>").val() == "0") {
                    $("#<%=txtTDS_AC.ClientID %>").focus();
                    $("#loader").hide();
                    return false;
                }

                if ($("#<%=txtTDSApplicalbeAmount.ClientID %>").val() == "" || $("#<%=txtTDSApplicalbeAmount.ClientID %>").val() == "0.00") {
                    $("#<%=txtTDSApplicalbeAmount.ClientID %>").focus();
                    $("#loader").hide();
                    return false;
                }
                if ($("#<%=txtTDS_Amount.ClientID %>").val() == "" || $("#<%=txtTDS_Amount.ClientID %>").val() == "0.00") {
                    $("#<%=txtTDS_Amount.ClientID %>").focus();
                    $("#loader").hide();
                    return false;
                }

                if ($("#<%=txtTDS_Perc.ClientID %>").val() == "" || $("#<%=txtTDS_Perc.ClientID %>").val() == "0.00") {
                    $("#<%=txtTDS_Perc.ClientID %>").focus();
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
                var voucher_type = "";
                var doc_no = 0, Comissionid = 0;
                var status = document.getElementById("<%= btnSave.ClientID %>").value;
                var spname = "Commission";
                var XML = "<ROOT>";
                if (status == "Update") {
                    doc_no = document.getElementById("<%= hdnflvdoc.ClientID %>").value;
                    Comissionid = document.getElementById("<%= hdnflvid.ClientID %>").value;
                }
                var Suffix = $("#<%=txtSUFFIX.ClientID%>").val();
                var DO_No = $("#<%=txtDONO.ClientID  %>").val() == "" ? 0 : $("#<%=txtDONO.ClientID %>").val();
                var d = $("#<%=txtDOC_DATE.ClientID %>").val();
                var Doc_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                var Vouchertype = $("#<%=drpVouchertype.ClientID %>").val();
                var linkno = 0;
                var linkid = 0;
                var linktype = '';

                var Ac_Code = $("#<%=txtAC_CODE.ClientID  %>").val() == "" ? 0 : $("#<%=txtAC_CODE.ClientID %>").val();
                var Unit_Code = $("#<%=txtUnit_Code.ClientID  %>").val() == "" ? 0 : $("#<%=txtUnit_Code.ClientID %>").val();
                var Broker_CODE = $("#<%=txtBroker_CODE.ClientID  %>").val() == "" ? 0 : $("#<%=txtBroker_CODE.ClientID %>").val();
                var ITEM_CODE = $("#<%=txtITEM_CODE.ClientID  %>").val() == "" ? 0 : $("#<%=txtITEM_CODE.ClientID %>").val();
                var Quantal = $("#<%=txtQNTL.ClientID  %>").val() == "" ? 0 : $("#<%=txtQNTL.ClientID %>").val();
                var PACKING = $("#<%=txtPACKING.ClientID  %>").val() == "" ? 0 : $("#<%=txtPACKING.ClientID %>").val();
                var BAGS = $("#<%=txtBAGS.ClientID  %>").val() == "" ? 0 : $("#<%=txtBAGS.ClientID %>").val();
                var Grade = $("#<%=txtGRADE.ClientID %>").val();
                var HSN = $("#<%=txtHSN.ClientID %>").val();
                var Mill_Code = $("#<%=txtMILL_CODE.ClientID  %>").val() == "" ? 0 : $("#<%=txtMILL_CODE.ClientID %>").val();
                var Mill_Rate = $("#<%=txtMILL_RATE.ClientID  %>").val() == "" ? 0 : $("#<%=txtMILL_RATE.ClientID %>").val();
                var Sale_Rate = $("#<%=txtSALE_RATE.ClientID  %>").val() == "" ? 0 : $("#<%=txtSALE_RATE.ClientID %>").val();
                var Purchase_Rate = $("#<%=txtPURCHASE_RATE.ClientID  %>").val() == "" ? 0 : $("#<%=txtPURCHASE_RATE.ClientID %>").val();
                var RDiffTender = $("#<%=txtRDiffTender.ClientID  %>").val() == "" ? 0 : $("#<%=txtRDiffTender.ClientID %>").val();
                var RDiffTender1 = $("#<%=txtRDiffTender1.ClientID  %>").val() == "" ? 0 : $("#<%=txtRDiffTender1.ClientID %>").val();

                var Narration1 = $("#<%=txtNarration1.ClientID %>").val();
                var POSTAGE = $("#<%=txtPostage.ClientID  %>").val() == "" ? 0 : $("#<%=txtPostage.ClientID %>").val();
                var Narration2 = $("#<%=txtNarration2.ClientID %>").val();
                var Resale_Commisson = $("#<%=txtResale_Commisson.ClientID  %>").val() == "" ? 0 : $("#<%=txtResale_Commisson.ClientID %>").val();
                var Narration3 = $("#<%=txtNarration3.ClientID %>").val();
                var BANK_COMMISSION = $("#<%=txtBANK_COMMISSION.ClientID  %>").val() == "" ? 0 : $("#<%=txtBANK_COMMISSION.ClientID %>").val();
                var Narration4 = $("#<%=txtNarration4.ClientID %>").val();

                var Transport_Code = $("#<%=txtTRANSPORT_CODE.ClientID  %>").val() == "" ? 0 : $("#<%=txtTRANSPORT_CODE.ClientID %>").val();
                var OTHER_Expenses = $("#<%=txtOTHER_Expenses.ClientID  %>").val() == "" ? 0 : $("#<%=txtOTHER_Expenses.ClientID %>").val();
                var Voucher_Amount = $("#<%=txtVoucher_Amount.ClientID  %>").val() == "" ? 0 : $("#<%=txtVoucher_Amount.ClientID %>").val();
                var Diff_Amount = $("#<%=lblDiff.ClientID  %>").val() == "" ? 0 : $("#<%=lblDiff.ClientID %>").val();
                var Commission_Rate = $("#<%=txtCommissionPerQntl.ClientID  %>").val() == "" ? 0 : $("#<%=txtCommissionPerQntl.ClientID %>").val();

                var DO_No = $("#<%=txtDONO.ClientID  %>").val() == "" ? 0 : $("#<%=txtDONO.ClientID %>").val();

                var CGSTRate = $("#<%=txtCGSTRate.ClientID  %>").val() == "" ? 0 : $("#<%=txtCGSTRate.ClientID %>").val();
                var CGSTAmount = $("#<%=txtCGSTAmount.ClientID  %>").val() == "" ? 0 : $("#<%=txtCGSTAmount.ClientID %>").val();
                var IGSTRate = $("#<%=txtIGSTRate.ClientID  %>").val() == "" ? 0 : $("#<%=txtIGSTRate.ClientID %>").val();
                var IGSTAmount = $("#<%=txtIGSTAmount.ClientID  %>").val() == "" ? 0 : $("#<%=txtIGSTAmount.ClientID %>").val();
                var SGSTRate = $("#<%=txtSGSTRate.ClientID  %>").val() == "" ? 0 : $("#<%=txtSGSTRate.ClientID %>").val();
                var SGSTAmount = $("#<%=txtSGSTAmount.ClientID  %>").val() == "" ? 0 : $("#<%=txtSGSTAmount.ClientID %>").val();
                var GstRateCode = $("#<%=txtGSTRateCode.ClientID  %>").val() == "" ? 0 : $("#<%=txtGSTRateCode.ClientID %>").val();
                var TaxableAmount = $("#<%=txtTaxableAmount.ClientID  %>").val() == "" ? 0 : $("#<%=txtTaxableAmount.ClientID %>").val();

                var mc = document.getElementById("<%= hdnfmillcode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfmillcode.ClientID %>").value;
                var ac = document.getElementById("<%= hdnfAcode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfAcode.ClientID %>").value;
                var uc = document.getElementById("<%= hdnfunitcode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfunitcode.ClientID %>").value;
                var bk = document.getElementById("<%= hdnfBrokercode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfBrokercode.ClientID %>").value;
                var TR = document.getElementById("<%= hdnftransportcode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnftransportcode.ClientID %>").value;
                var ic = document.getElementById("<%= hdnfItemid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfItemid.ClientID %>").value;


                var TCS_Rate = $("#<%=txtTCSRate.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSRate.ClientID %>").val();
                var TCS_Amt = $("#<%=txtTCSAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSAmt.ClientID %>").val();
                var TCS_Net_Payable = $("#<%=txtTCSNet_Payable.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSNet_Payable.ClientID %>").val();

                var FRIEGHT_RATE = parseFloat($("#<%=txtFrieght.ClientID %>").val() == "" ? 0 : $("#<%=txtFrieght.ClientID %>").val());
                var FRIEGHT_AMOUNT = parseFloat($("#<%=txtFrieghtAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtFrieghtAmount.ClientID %>").val());
                var subtotal = $("#<%=txtSubtotal.ClientID  %>").val() == "" ? 0 : $("#<%=txtSubtotal.ClientID %>").val();

                var isTDS = $("#<%=drpTDS_Applicable.ClientID %>").val();
                var TDS_AC = $("#<%=txtTDS_AC.ClientID %>").val() == "" ? 0 : $("#<%=txtTDS_AC.ClientID%>").val();
                var TDSPer = $("#<%=txtTDS_Perc.ClientID %>").val() == "" ? 0 : $("#<%=txtTDS_Perc.ClientID%>").val();
                var TDSApplicableAmt = $("#<%=txtTDSApplicalbeAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtTDSApplicalbeAmount.ClientID%>").val();
                var TDSamt = $("#<%=txtTDS_Amount.ClientID %>").val() == "" ? 0 : $("#<%=txtTDS_Amount.ClientID%>").val();
                var TDS = 0;
                var ta = document.getElementById("<%= hdnftds.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnftds.ClientID %>").value;

                var einvoiceno = $("#<%=txteinvoiceno.ClientID %>").val();
                if (einvoiceno == "&nbsp;") {
                    einvoiceno = "";
                }
                var ackno = $("#<%=txtackno.ClientID %>").val();
                if (ackno == "&nbsp;") {
                    ackno = "";
                }

                var USER = '<%= Session["user"] %>';
                var Branch_Id = '<%= Session["Branch_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';
                var Company_Code = '<%= Session["Company_Code"] %>';
                var drcr;
                var AcShort_Name = $("#<%=lblAc_name.ClientID %>").text();
                var TCSNarration = 'TCS:' + AcShort_Name + " " + doc_no;
                var TDSNarration = 'TDS:' + AcShort_Name + " " + doc_no;
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
                if (TR == "" || TR == "&nbsp;") {
                    TR = 0;
                }
                if (ic == "" || ic == "&nbsp;") {
                    ic = 0;
                }
                if (ta == "" || ta == "&nbsp;") {
                    ta = 0;
                }
                var LocalVoucherInsertUpdet;
                var Gledger_Insert = ""; Gledger_values = "";
                var Gledger_Delete = "";
                debugger;
                var DOCNO = "";
                if (status == "Save") {
                    LocalVoucherInsertUpdet = "Created_By='" + USER + "' Modified_By=''";
                    DOCNO = "doc_date='" + Doc_Date + "'";
                }
                else {
                    LocalVoucherInsertUpdet = "Modified_By='" + USER + "' Created_By=''";
                    DOCNO = "doc_no='" + doc_no + "' doc_date='" + Doc_Date + "'";
                }
                debugger;

                if (TCS_Net_Payable > 0) {
                    voucher_type = "LV";
                }
                else {
                    voucher_type = "CV";
                }
                XML = XML + "<Commission " + DOCNO + " link_no='" + linkno + "' link_type='" + linktype + "' link_id='" + linkid + "' ac_code='" + Ac_Code + "' " +
                    "unit_code='" + Unit_Code + "' broker_code='" + Broker_CODE + "' " +
                        "qntl='" + Quantal + "' packing='" + PACKING + "' bags='" + BAGS + "' HSN='" + HSN + "' grade='" + Grade + "' transport_code='" + Transport_Code + "' mill_rate='" + Mill_Rate + "' sale_rate='" + Sale_Rate + "' purc_rate='" + Purchase_Rate + "' commission_amount='" + RDiffTender1 + "' "
                    + "resale_rate='" + Commission_Rate + "' resale_commission='" + Resale_Commisson + "' misc_amount='" + OTHER_Expenses + "' "
                    + "texable_amount='" + TaxableAmount + "' gst_code='" + GstRateCode + "' cgst_rate='" + CGSTRate + "' cgst_amount='" + CGSTAmount + "' sgst_rate='" + SGSTRate + "' sgst_amount='" + SGSTAmount + "' igst_rate='" + IGSTRate + "' igst_amount='" + IGSTAmount + "' bill_amount='" + Voucher_Amount + "' " +
                    "Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Id + "' " + LocalVoucherInsertUpdet + " commissionid='" + Comissionid + "' ac='" + ac + "' uc='" + uc + "' bc='" + bk + "' tc='" + TR + "' mill_code='" + Mill_Code + "' mc='" + mc + "' "
                    + "narration1='" + Narration1 + "' narration2='" + Narration2 + "' narration3='" + Narration3 +
                    "' narration4='" + Narration4 + "' TCS_Rate='" + TCS_Rate + "' TCS_Amt='" + TCS_Amt + "' TCS_Net_Payable='" + TCS_Net_Payable +
                    "' BANK_COMMISSION='" + BANK_COMMISSION + "' einvoiceno='" + einvoiceno + "' ackno='" + ackno + "' item_code='" + ITEM_CODE +
                    "' ic='" + ic + "' Tran_Type='" + voucher_type + "' Frieght_Rate='" + FRIEGHT_RATE + "' Frieght_amt='" + FRIEGHT_AMOUNT +
                    "' subtotal='" + subtotal + "' IsTDS='" + isTDS + "' TDS_Ac='" + TDS_AC + "' TDS_Per='" + TDSPer +
                    "' TDSAmount='" + TDSamt + "' TDS='" + TDSPer + "' ta='" + ta + "' >";


                var Order_Code = 1;

                if (Voucher_Amount > 0) {
                    drcr = "D";
                }
                else {
                    drcr = "C";
                }

                //Gledger_values = Gledger_values + "('LV','','" + doc_no + "','" + Doc_Date + "','" + Ac_Code + "','0','','" + Voucher_Amount + "', " +
                //                                      " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','" + drcr + "','0',0,'" + Branch_Id + "','LV','" + doc_no + "'," +
                //                                      "  case when 0='" + ac + "' then null else '" + ac + "' end ,'0','11','0')";

                XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + Ac_Code + "' " +
                                                            "UNIT_code='0' NARRATION='" + Narration1 + "' AMOUNT='" +Math.abs( Voucher_Amount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr + "' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                            "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + ac + "' vc='0' progid='11' tranid='0'/>";

                if (DO_No == 0) {
                    Order_Code = Order_Code + 1;
                    if (FRIEGHT_AMOUNT > 0) {
                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["Freight_Ac"] %>' + "' " +
                                                             "UNIT_code='0' NARRATION='Frieght' AMOUNT='" + Math.abs(FRIEGHT_AMOUNT) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                             "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                             "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + '<%=Session["Freight_Acid"] %>' + "' vc='0' progid='11' tranid='0'/>";
                    }
                    else {

                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["Freight_Ac"] %>' + "' " +
                                                           "UNIT_code='0' NARRATION='Frieght' AMOUNT='" + Math.abs(FRIEGHT_AMOUNT) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                           "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + '<%=Session["Freight_Acid"] %>' + "' vc='0' progid='11' tranid='0'/>";

                    }
                }

               
                if (Voucher_Amount > 0) {
                   // Order_Code = parseInt(Order_Code) + 1;
                    //Gledger_values = Gledger_values + ",('LV','','" + doc_no + "','" + Doc_Date + "','" + '<%=Session["COMMISSION_AC"] %>' + "','0','','" + (TaxableAmount - Resale_Commisson) + "', " +
                    //                                    " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C','0',0,'" + Branch_Id + "','LV','" + doc_no + "'," +
                    //                                  "  case when 0=" + '<%=Session["commissionid"] %>' + " then null else '" + '<%=Session["commissionid"] %>' + "' end,'0','11','0')";

//                    XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["COMMISSION_AC"] %>' + "' " +
  //                                                         "UNIT_code='0' NARRATION='" + Narration1 + "' AMOUNT='" + Math.abs((TaxableAmount - Resale_Commisson)) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
    //                                                       "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
      //                                                     "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + '<%=Session["commissionid"] %>' + "' vc='0' progid='11' tranid='0'/>";

                    if (CGSTAmount > 0) {
                        Order_Code = Order_Code + 1;
                        // Gledger_values = Gledger_values + ",('LV','','" + doc_no + "','" + Doc_Date + "','" + '<%=Session["SaleCGSTAc"] %>' + "','0','','" + CGSTAmount + "', " +
                        //                                   " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C','0',0,'" + Branch_Id + "','LV','" + doc_no + "'," +
                        //                                 "  case when 0=" + '<%=Session["SaleCGSTid"] %>' + " then null else '" + '<%=Session["SaleCGSTid"] %>' + "' end,'0','11','0')";

                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["SaleCGSTAc"] %>' + "' " +
                                                           "UNIT_code='0' NARRATION='" + Narration1 + "' AMOUNT='" + Math.abs(CGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                           "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + '<%=Session["SaleCGSTid"] %>' + "' vc='0' progid='11' tranid='0'/>";

                    }
                    if (SGSTAmount > 0) {
                        Order_Code = Order_Code + 1;
                        //Gledger_values = Gledger_values + ",('LV','','" + doc_no + "','" + Doc_Date + "','" + '<%=Session["SaleSGSTAc"] %>' + "','0','','" + SGSTAmount + "', " +
                        //                                   " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C','0',0,'" + Branch_Id + "','LV','" + doc_no + "'," +
                        //                                 " case when 0=" + '<%=Session["SaleSGSTid"] %>' + " then null else '" + '<%=Session["SaleSGSTid"] %>' + "' end,'0','11','0')";

                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["SaleSGSTAc"] %>' + "' " +
                                                           "UNIT_code='0' NARRATION='" + Narration1 + "' AMOUNT='" + Math.abs(SGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                           "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + '<%=Session["SaleSGSTid"] %>' + "' vc='0' progid='11' tranid='0'/>";
                    }

                    //IGST Acc
                    if (IGSTAmount > 0) {
                        Order_Code = Order_Code + 1;
                        //Gledger_values = Gledger_values + ",('LV','','" + doc_no + "','" + Doc_Date + "','" + '<%=Session["SaleIGSTAc"] %>' + "','0','','" + IGSTAmount + "', " +
                        //                                   " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C','0',0,'" + Branch_Id + "','LV','" + doc_no + "'," +
                        //                                 " case when 0=" + '<%=Session["SaleIGSTid"] %>' + " then null else '" + '<%=Session["SaleIGSTid"] %>' + "' end,'0','11','0')";

                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["SaleIGSTAc"] %>' + "' " +
                                                           "UNIT_code='0' NARRATION='" + Narration1 + "' AMOUNT='" + Math.abs(IGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                           "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + '<%=Session["SaleIGSTid"] %>' + "' vc='0' progid='11' tranid='0'/>";
                    }

                    if (TCS_Amt > 0) {
                        Order_Code = parseInt(Order_Code) + 1;

                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                                              "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + Math.abs(TCS_Amt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                              "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + '<%=Session["SaleTCSAcid"] %>' + "' vc='0' progid='6' tranid='0'/>";

                        Order_Code = parseInt(Order_Code) + 1;

                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + Ac_Code + "' " +
                                                            "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + Math.abs(TCS_Amt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                            "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + ac + "' vc='0' progid='6' tranid='0'/>";
                    }
                }
                else {
                    if (Voucher_Amount != 0) {
                        //Gledger_values = Gledger_values + ",('LV','','" + doc_no + "','" + Doc_Date + "','" + '<%=Session["COMMISSION_AC"] %>' + "','0','','" + (TaxableAmount - Resale_Commisson) + "', " +
                        //                             " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C','0',0,'" + Branch_Id + "','LV','" + doc_no + "'," +
                        //                           "  case when 0=" + '<%=Session["commissionid"] %>' + " then null else '" + '<%=Session["commissionid"] %>' + "' end,'0','11','0')";

                        //XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["COMMISSION_AC"] %>' + "' " +
                            //                              "UNIT_code='0' NARRATION='" + Narration1 + "' AMOUNT='" +Math.abs((TaxableAmount - Resale_Commisson)) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              //                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                //                          "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + '<%=Session["commissionid"] %>' + "' vc='0' progid='11' tranid='0'/>";
                    }
                    // CGST Acc Effect
                    if (CGSTAmount != 0) {
                        Order_Code = Order_Code + 1;
                        // Gledger_values = Gledger_values + ",('LV','','" + doc_no + "','" + Doc_Date + "','" + '<%=Session["PurchaseCGSTAc"] %>' + "','0','','" + CGSTAmount + "', " +
                        //                                 " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C','0',0,'" + Branch_Id + "','LV','" + doc_no + "'," +
                        //                               "  case when 0=" + '<%=Session["PurchaseCGSTid"] %>' + " then null else '" + '<%=Session["PurchaseCGSTid"] %>' + "' end,'0','11','0')";

                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["PurchaseCGSTAc"] %>' + "' " +
                                                                              "UNIT_code='0' NARRATION='" + Narration1 + "' AMOUNT='" + Math.abs(CGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                                              "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + '<%=Session["PurchaseCGSTid"] %>' + "' vc='0' progid='11' tranid='0'/>";
                    }
                    //SGST Acc
                    if (SGSTAmount != 0) {
                        Order_Code = Order_Code + 1;
                        //Gledger_values = Gledger_values + ",('LV','','" + doc_no + "','" + Doc_Date + "','" + '<%=Session["PurchaseSGSTAc"] %>' + "','0','','" + SGSTAmount + "', " +
                        //                                  " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C','0',0,'" + Branch_Id + "','LV','" + doc_no + "'," +
                        //                                " case when 0=" + '<%=Session["PurchaseSGSTid"] %>' + " then null else '" + '<%=Session["PurchaseSGSTid"] %>' + "' end,'0','11','0')";

                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["PurchaseSGSTAc"] %>' + "' " +
                                                                              "UNIT_code='0' NARRATION='" + Narration1 + "' AMOUNT='" + Math.abs(CGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                                              "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + '<%=Session["PurchaseSGSTid"] %>' + "' vc='0' progid='11' tranid='0'/>";
                    }

                    //IGST Acc
                    if (IGSTAmount != 0) {
                        Order_Code = Order_Code + 1;
                        // Gledger_values = Gledger_values + ",('LV','','" + doc_no + "','" + Doc_Date + "','" + '<%=Session["PurchaseIGSTAc"] %>' + "','0','','" + IGSTAmount + "', " +
                        //                                   " null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C','0',0,'" + Branch_Id + "','LV','" + doc_no + "'," +
                        //                                 " case when 0=" + '<%=Session["PurchaseIGSTid"] %>' + " then null else '" + '<%=Session["PurchaseIGSTid"] %>' + "' end,'0','11','0')";

                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["PurchaseIGSTAc"] %>' + "' " +
                                                                              "UNIT_code='0' NARRATION='" + Narration1 + "' AMOUNT='" + Math.abs(IGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                                              "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + '<%=Session["PurchaseIGSTid"] %>' + "' vc='0' progid='11' tranid='0'/>";
                    }

                    if (TCS_Amt != 0) {
                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                                              "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + Math.abs(TCS_Amt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                              "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + '<%=Session["SaleTCSAcid"] %>' + "' vc='0' progid='6' tranid='0'/>";

                        Order_Code = Order_Code + 1;

                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + Ac_Code + "' " +
                                                            "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + Math.abs(TCS_Amt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                            "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + ac + "' vc='0' progid='6' tranid='0'/>";
                    }
                }
                debugger;

                //TDS Ac
                if (TDSamt != 0) {
                    if (TDSamt > 0) {
                        Order_Code = Order_Code + 1;
                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + Ac_Code + "' " +
                                                      "UNIT_code='0' NARRATION='" + TDSNarration + "' AMOUNT='" + Math.abs(TDSamt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                      "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + ac + "' vc='0' progid='10' tranid='0'/>";

                        Order_Code = Order_Code + 1;
                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + TDS_AC + "' " +
                                                     "UNIT_code='0' NARRATION='" + TDSNarration + "' AMOUNT='" + Math.abs(TDSamt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                     "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                     "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + ta + "' vc='0' progid='10' tranid='0'/>";
                    }
                    else {
                        Order_Code = Order_Code + 1;
                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + Ac_Code + "' " +
                                                      "UNIT_code='0' NARRATION='" + TDSNarration + "' AMOUNT='" + Math.abs(TDSamt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                      "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + ac + "' vc='0' progid='10' tranid='0'/>";

                        Order_Code = Order_Code + 1;
                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + TDS_AC + "' " +
                                                     "UNIT_code='0' NARRATION='" + TDSNarration + "' AMOUNT='" + Math.abs(TDSamt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                     "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                     "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + ta + "' vc='0' progid='10' tranid='0'/>";

                    }
                }
                // Resale Acc Effect
                if (Resale_Commisson != 0) {
                    if (Resale_Commisson > 0) {
                        drcr = "C";
                    }
                    else {
                        drcr = "D";
                    }
                    Order_Code = Order_Code + 1;
                   
                    XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["COMMISSION_AC"] %>' + "' " +
                                                                              "UNIT_code='0' NARRATION='" + Narration1 + "' AMOUNT='" + Math.abs(Resale_Commisson) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr + "' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                                                              "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + '<%=Session["commissionid"] %>' + "' vc='0' progid='11' tranid='0'/>";
                }
                if (RDiffTender != 0) {
                    Order_Code = parseInt(Order_Code) + 1;
                    if (RDiffTender > 0) {

                        //   var amt = Math.round(parseFloat(DiffMemo) - parseFloat(Tender_Commission_Amount));

                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + <%=Session["RateDiffAc"] %> + "' " +
                                          "UNIT_code='0' NARRATION='" + Narration1 + "' AMOUNT='" + Math.abs(RDiffTender) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                          "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + <%=Session["RateDiffAcid"] %> + "' vc='0' progid='11' tranid='0'/>";
                    }
                    else {
                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_NO='" + doc_no + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + <%=Session["RateDiffAc"] %> + "' " +
                                        "UNIT_code='0' NARRATION='" + Narration1 + "' AMOUNT='" + Math.abs(RDiffTender) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                        "SORT_TYPE='" + voucher_type + "' SORT_NO='" + doc_no + "' ac='" + <%=Session["RateDiffAcid"] %> + "' vc='0' progid='11' tranid='0'/>";
                   
                    }
                }
                XML = XML + "</Commission></ROOT>";
                ProcessXML(XML, status, spname);
            }
            catch (exx) {
                $("#loader").hide();
                alert(exx)
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
                            window.open('../Outword/pgeLocalVoucherForGSTxmlNew.aspx?commissionid=' + id + '&Action=1&Tran_Type=' + voucher_type, "_self");

                        }
                    }
                    else {
                        var num = parseInt(response.d);

                        if (isNaN(num)) {
                            alert(response.d)

                        }
                        else {
                            window.open('../Outword/PgeLocalVoucherUtility.aspx', "_self");
                        }
                    }

                }


            }




        }

    </script>
    <style type="text/css">
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
    <div class="content">
        <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
            <legend style="text-align: center;">
                <asp:Label ID="label1" runat="server" Text="   Local Voucher One   " Font-Names="verdana"
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
                <asp:HiddenField ID="hdnfAcode" runat="server" />
                <asp:HiddenField ID="hdnfBrokercode" runat="server" />
                <asp:HiddenField ID="hdnfunitcode" runat="server" />
                <asp:HiddenField ID="hdnfmillcode" runat="server" />
                <asp:HiddenField ID="hdnftransportcode" runat="server" />
                <asp:HiddenField ID="hdnfdiffqulity" runat="server" />
                <asp:HiddenField ID="hdnfdiffqulityid" runat="server" />
                <asp:HiddenField ID="hdnflvdoc" runat="server" />
                <asp:HiddenField ID="hdnflvid" runat="server" />
                <asp:HiddenField ID="hdnfyearcode" runat="server" />
                <asp:HiddenField ID="hdnfcompanycode" runat="server" />
                <asp:HiddenField ID="hdnfItemid" runat="server" />
                <asp:HiddenField ID="hdnfcommisiontype" runat="server" />
                <asp:HiddenField ID="hdnftds" runat="server" />
                <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                    Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;" BackColor="Yellow">
                    <table width="80%" align="left">
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                    ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                                &nbsp;
                                <asp:Button ID="btnSave" runat="server" OnClientClick="if (!Validate()) return false;"
                                    Text="Save" CssClass="btnHelp" Width="90px" TabIndex="37" Height="24px" ValidationGroup="add"
                                    OnClick="btnSave_Click" />
                                &nbsp;
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                                    TabIndex="38" Height="24px" ValidationGroup="save" OnClick="btnEdit_Click" />
                                &nbsp;
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                                    Height="24px" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()"
                                    TabIndex="39" />
                                &nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                                    TabIndex="40" Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                                    ValidationGroup="save" Height="24px" OnClientClick="Back();" />
                                &nbsp;<asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" Width="90px"
                                    TabIndex="41" Height="24px" OnClick="btnPrint_Click" />
                                &nbsp;
                                <asp:Button runat="server" ID="btnGenEinvoice" Text="Generate EInvoice" CssClass="btnHelp"
                                    Width="120px" Height="24px" OnClientClick="EInovice();" />
                                <asp:Button runat="server" ID="btnCancleEinvoice" Text="Cancle EInvoice" CssClass="btnHelp"
                                    Width="120px" Height="24px" OnClientClick="ConfirmCancle();" OnClick="btnCancleEInvoice_Click" />
                                <%--  <asp:Button runat="server" ID="BtnTransfer" Text="Transfer" CssClass="btnHelp"
                                    Width="120px" Height="24px" OnClientClick="Transfer();" />--%>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 80%;" align="left" cellpadding="2" cellspacing="5">
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                    Font-Size="Small" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Voucher Type:
                            </td>
                            <td align="left">
                                <asp:DropDownList runat="server" ID="drpVouchertype" Width="100px" CssClass="ddl"
                                    AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="drpVouchertype_SelectedIndexChanged">
                                    <asp:ListItem Text="LV" Value="LV" />
                                    <asp:ListItem Text="CV" Value="CV" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Change No:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                    TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender runat="server" ID="txtxf" FilterType="Numbers" InvalidChars="."
                                    TargetControlID="txtEditDoc_No">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                    Font-Size="Small" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Note No.
                            </td>
                            <td colspan="6" align="left" style="width: 80%;">
                                <asp:TextBox ID="txtdoc_no" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                    Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtdoc_no_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtDOC_NO" runat="server" Text="..." Width="80px" OnClick="btntxtDOC_NO_Click"
                                    CssClass="btnHelp" Height="24px" />
                                <asp:Label runat="server" ID="lblLV_Id" ForeColor="Black" Visible="false"></asp:Label>
                                &nbsp; Suffix: &nbsp;
                                <asp:TextBox ID="txtSUFFIX" runat="Server" CssClass="txt" TabIndex="2" Width="20px"
                                    Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtSUFFIX_TextChanged"
                                    Height="24px"></asp:TextBox>
                                &nbsp; D.O No.:
                                <asp:TextBox ID="txtDONO" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                    onkeydown="DO(event);" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtDONO_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtDONO" runat="server" Text="..." OnClick="btntxtDONO_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />&nbsp; Tender No(R):<asp:Label runat="server"
                                        ID="lblTenderNo" ForeColor="Black"></asp:Label>
                            </td>
                            <%--<td align="left">
                        </td>--%>
                        </tr>
                        <tr>
                            <td align="left">Date:
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txtDOC_DATE" runat="Server" CssClass="txt" TabIndex="4" Width="90px"
                                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtDOC_DATE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                    Width="25px" Height="15px" />
                                <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtDOC_DATE"
                                    PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Party:
                            </td>
                            <td align="left" colspan="4" style="width: 10%;">
                                <asp:TextBox ID="txtAC_CODE" runat="Server" CssClass="txt" TabIndex="5" Width="90px"
                                    onkeydown="Party(event);" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtAC_CODE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtAC_CODE" runat="server" Text="..." OnClick="btntxtAC_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblAc_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Unit:
                            </td>
                            <td align="left" colspan="4" style="width: 10%;">
                                <asp:TextBox ID="txtUnit_Code" runat="Server" CssClass="txt" TabIndex="6" Width="90px"
                                    onkeydown="Unit(event);" Style="text-align: right;" AutoPostBack="false" Height="24px"
                                    OnTextChanged="txtUnit_Code_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtUnitcode" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                    Width="20px" OnClick="btntxtUnitcode_Click" />
                                <asp:Label ID="lblUnitName" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Broker:
                            </td>
                            <td align="left" colspan="4" style="width: 10%;">
                                <asp:TextBox ID="txtBroker_CODE" runat="Server" CssClass="txt" TabIndex="7" Width="90px"
                                    onkeydown="Broker(event);" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtBroker_CODE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtBroker_CODE" runat="server" Text="..." OnClick="btntxtBroker_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblBroker_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Item:
                            </td>
                            <td align="left" colspan="6" style="width: 10%;">
                                <asp:TextBox ID="txtITEM_CODE" runat="Server" CssClass="txt" TabIndex="8" Width="90px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtITEM_CODE_TextChanged"
                                    onKeyDown="Item(event);"></asp:TextBox>
                                <asp:Button ID="btntxtITEM_CODE" runat="server" Text="..." OnClick="btntxtITEM_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="LBLITEMNAME" runat="server" CssClass="lblName"></asp:Label>
                                &emsp;&emsp;&emsp;Quantal:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtQNTL" runat="Server" CssClass="txt" TabIndex="9" Width="115px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtQNTL_TextChanged"
                                    Height="24px"></asp:TextBox><ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBags"
                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtQNTL">
                                    </ajax1:FilteredTextBoxExtender>
                                &emsp;&emsp;&emsp; Packing: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtPACKING" runat="Server" CssClass="txt" TabIndex="10" Width="80px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtPACKING_TextChanged"
                                    Height="24px"></asp:TextBox><ajax1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1"
                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtPACKING">
                                    </ajax1:FilteredTextBoxExtender>
                                &nbsp; Bags: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtBAGS" runat="Server" CssClass="txt" TabIndex="11" Width="80px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtBAGS_TextChanged"
                                    ReadOnly="true" Height="24px"></asp:TextBox><ajax1:FilteredTextBoxExtender runat="server"
                                        ID="FilteredTextBoxExtender2" FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtBAGS">
                                    </ajax1:FilteredTextBoxExtender>
                                &nbsp; HSN: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtHSN" runat="Server" CssClass="txt" TabIndex="12" Width="80px"
                                    Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtHSN_TextChanged"
                                    Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Grade:
                            </td>
                            <td align="left" colspan="6" style="width: 10%;">
                                <asp:TextBox ID="txtGRADE" runat="Server" CssClass="txt" TabIndex="13" Width="90px"
                                    onkeydown="Grade(event);" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtGRADE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtGRADE" runat="server" Text="..." OnClick="btntxtGRADE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                &emsp;&emsp;&emsp; Transport: &nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtTRANSPORT_CODE" runat="Server" CssClass="txt" TabIndex="14" Width="80px"
                                    onkeydown="Transport(event);" Style="text-align: right;" AutoPostBack="false"
                                    OnTextChanged="txtTRANSPORT_CODE_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtTRANSPORT_CODE" runat="server" Text="..." OnClick="btntxtTRANSPORT_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="LBLTRANSPORT_NAME" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Mill:
                            </td>
                            <td align="left" colspan="3" style="width: 10%;">
                                <asp:TextBox ID="txtMILL_CODE" runat="Server" CssClass="txt" TabIndex="15" Width="90px"
                                    onkeydown="Mill(event);" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtMILL_CODE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtMILL_CODE" runat="server" Text="..." OnClick="btntxtMILL_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblMill_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Mill Rate:
                            </td>
                            <td align="left" colspan="6" style="width: 10%;">
                                <asp:TextBox ID="txtMILL_RATE" runat="Server" CssClass="txt" TabIndex="16" Width="110px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtMILL_RATE_TextChanged"
                                    Height="24px"></asp:TextBox><ajax1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3"
                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtMILL_RATE">
                                    </ajax1:FilteredTextBoxExtender>
                                &emsp;&emsp;&emsp; Sale Rate:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtSALE_RATE" runat="Server" CssClass="txt" TabIndex="17" Width="80px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtSALE_RATE_TextChanged"
                                    Height="24px"></asp:TextBox><ajax1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender11"
                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtSALE_RATE">
                                    </ajax1:FilteredTextBoxExtender>
                                &nbsp; Purc Rate: &nbsp;
                                <asp:TextBox ID="txtPURCHASE_RATE" runat="Server" CssClass="txt" TabIndex="18" Width="80px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtPURCHASE_RATE_TextChanged"
                                    Height="24px"></asp:TextBox><ajax1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender4"
                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtPURCHASE_RATE">
                                    </ajax1:FilteredTextBoxExtender>
                                &nbsp; Diff.: &nbsp;
                                <asp:Label ID="lblDiff" runat="server" CssClass="lblName"></asp:Label>
                                &nbsp; GST Rate Code &emsp;
                                <asp:TextBox ID="txtGSTRateCode" runat="Server" CssClass="txt" TabIndex="19" Width="80px"
                                    onkeydown="GstRate(event);" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtGSTRateCode_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtGSTRateCode" runat="server" Text="..." OnClick="btntxtGSTRateCode_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblGSTRateName" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">R.DIFF.Tender:
                            </td>
                            <td align="left" colspan="6" style="width: 10%;">
                                <asp:TextBox ID="txtRDiffTender1" runat="Server" CssClass="txt" TabIndex="20" Width="90px"
                                    Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtRDiffTender1_TextChanged"
                                    ReadOnly="true" Height="24px"></asp:TextBox>
                                <asp:TextBox ID="txtRDiffTender" runat="Server" CssClass="txt" TabIndex="20" Width="110px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtRDiffTender_TextChanged"
                                    ReadOnly="true" Height="24px"></asp:TextBox><ajax1:FilteredTextBoxExtender runat="server"
                                        ID="FilteredTextBoxExtender5" FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtRDiffTender">
                                    </ajax1:FilteredTextBoxExtender>
                                &emsp;&emsp;&emsp; Narration1: &emsp;
                                <asp:TextBox ID="txtNarration1" runat="Server" CssClass="txt" TabIndex="21" Width="320px"
                                    onkeydown="Narration1(event);" Style="text-align: left;" AutoPostBack="false"
                                    OnTextChanged="txtNarration1_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtNarration1" runat="server" Text="..." OnClick="btntxtNarration1_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                Narration2: &emsp;&emsp;
                                <asp:TextBox ID="txtNarration2" runat="Server" CssClass="txt" TabIndex="22" Width="300px"
                                    onkeydown="Narration2(event);" Style="text-align: left;" AutoPostBack="false"
                                    OnTextChanged="txtNarration2_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtNarration2" runat="server" Text="..." OnClick="btntxtNarration2_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;"></td>
                            <td align="left" colspan="6" style="width: 10%;">
                                <asp:TextBox ID="txtPostage" runat="Server" CssClass="txt" TabIndex="23" Width="110px"
                                    Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtPostage_TextChanged"
                                    Visible="false" Height="24px"></asp:TextBox><ajax1:FilteredTextBoxExtender runat="server"
                                        ID="FilteredTextBoxExtender6" FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtPostage">
                                    </ajax1:FilteredTextBoxExtender>
                                &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Narration3:
                                &emsp;
                                <asp:TextBox ID="txtNarration3" runat="Server" CssClass="txt" TabIndex="24" Width="320px"
                                    onkeydown="Narration3(event);" Style="text-align: left;" AutoPostBack="false"
                                    OnTextChanged="txtNarration3_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtNarration3" runat="server" Text="..." OnClick="btntxtNarration3_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                Narration4: &emsp; &emsp;
                                <asp:TextBox ID="txtNarration4" runat="Server" CssClass="txt" TabIndex="25" Width="300px"
                                    onkeydown="Narration4(event);" Style="text-align: left;" AutoPostBack="false"
                                    OnTextChanged="txtNarration4_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtNarration4" runat="server" Text="..." OnClick="btntxtNarration4_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Resell Comm'n:
                            </td>
                            <td align="left" colspan="6" style="width: 10%;">
                                <asp:TextBox runat="server" ID="txtCommissionPerQntl" Width="40px" TabIndex="26"
                                    AutoPostBack="true" Height="20px" OnTextChanged="txtCommissionPerQntl_TextChanged"></asp:TextBox>
                                <asp:TextBox ID="txtResale_Commisson" runat="Server" CssClass="txt" TabIndex="27"
                                    Width="110px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtResale_Commisson_TextChanged"
                                    Height="24px"></asp:TextBox><ajax1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender7"
                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtResale_Commisson">
                                    </ajax1:FilteredTextBoxExtender>
                                Bank Commission:
                                <asp:TextBox ID="txtBANK_COMMISSION" runat="Server" CssClass="txt" TabIndex="28"
                                    Width="110px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtBANK_COMMISSION_TextChanged"
                                    Height="24px"></asp:TextBox><ajax1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender12"
                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtBANK_COMMISSION">
                                    </ajax1:FilteredTextBoxExtender>
                                SubTotal:
                                <asp:TextBox ID="txtSubtotal" runat="Server" CssClass="txt" Width="140px" Style="text-align: right;"
                                    ReadOnly="True" TabIndex="29" Height="24px"></asp:TextBox>
                                Frieght:
                                <asp:TextBox ID="txtFrieght" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                    OnTextChanged="txtFrieght_TextChanged" TabIndex="30" Style="text-align: right;"
                                    Height="24px"></asp:TextBox>
                                <%--  <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtTCSRate">
                                </ajax1:FilteredTextBoxExtender>--%>
                                <asp:TextBox ID="txtFrieghtAmount" runat="Server" AutoPostBack="True" CssClass="txt"
                                    Height="24px" TabIndex="31" OnTextChanged="txtFrieghtAmount_TextChanged" ReadOnly="true"
                                    Style="text-align: right;" Width="98px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%;">Taxable Amount:
                            </td>
                            <td align="left" colspan="6" style="width: 10%;">
                                <asp:TextBox ID="txtTaxableAmount" runat="Server" CssClass="txt" Width="140px" Style="text-align: right;"
                                    ReadOnly="True" Height="24px" TabIndex="32"></asp:TextBox>
                                CGST%
                                <asp:TextBox ID="txtCGSTRate" runat="Server" CssClass="txt" Width="52px" TabIndex="33"
                                    Style="text-align: right;" Height="24px" AutoPostBack="true" OnTextChanged="txtCGSTRate_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtCGSTRate" runat="server" FilterType="Numbers,Custom"
                                    TargetControlID="txtCGSTRate" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtCGSTAmount" runat="Server" CssClass="txt" Width="80px" ReadOnly="true"
                                    Style="text-align: right;" Height="24px"></asp:TextBox>
                                SGST%
                                <asp:TextBox ID="txtSGSTRate" runat="Server" CssClass="txt" Width="50px" TabIndex="34"
                                    AutoPostBack="true" OnTextChanged="txtSGSTRate_TextChanged" Style="text-align: right;"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtSGSTRate" runat="server" FilterType="Numbers,Custom"
                                    TargetControlID="txtSGSTRate" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtSGSTAmount" runat="Server" CssClass="txt" Width="82px" ReadOnly="true"
                                    Style="text-align: right;" Height="24px"></asp:TextBox>
                                IGST%
                                <asp:TextBox ID="txtIGSTRate" runat="Server" CssClass="txt" Width="52px" AutoPostBack="true"
                                    OnTextChanged="txtIGSTRate_TextChanged" TabIndex="35" Style="text-align: right;"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtIGSTRate" runat="server" FilterType="Numbers,Custom"
                                    TargetControlID="txtIGSTRate" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtIGSTAmount" runat="Server" CssClass="txt" Width="80px" ReadOnly="true"
                                    Style="text-align: right;" Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <%--      <tr>
                            <td align="left" style="width: 10%;">
                            </td>
                            <td align="left" style="width: 10%;">
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="left" style="width: 10%;">Voucher Amount:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtVoucher_Amount" runat="Server" CssClass="txt" Width="140px" Style="text-align: right;"
                                    AutoPostBack="True" TabIndex="36" OnTextChanged="txtVoucher_Amount_TextChanged"
                                    ReadOnly="True" Height="24px"></asp:TextBox>
                            </td>
                            <td>TCS%
                                <asp:TextBox ID="txtTCSRate" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                    OnTextChanged="txtTCSRate_TextChanged" TabIndex="37" Style="text-align: right;"
                                    Height="24px"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtTCSRate">
                                </ajax1:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtTCSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                    TabIndex="38" OnTextChanged="txtTCSAmt_TextChanged" ReadOnly="true" Style="text-align: right;"
                                    Width="98px"></asp:TextBox>
                                Net Payable:
                                <asp:TextBox ID="txtTCSNet_Payable" runat="Server" CssClass="txt" ReadOnly="true"
                                    TabIndex="39" Width="140px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtTCSNet_Payable_TextChanged"
                                    Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Misc Exps:
                            </td>
                            <td align="left" colspan="4">
                                <asp:TextBox ID="txtOTHER_Expenses" runat="Server" CssClass="txt" TabIndex="40" Width="110px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtOTHER_Expenses_TextChanged"
                                    Height="24px"></asp:TextBox><ajax1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender10"
                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtOTHER_Expenses">
                                    </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>

                            <td colspan="4">TDS Applicable
                            <asp:DropDownList ID="drpTDS_Applicable" runat="Server" CssClass="txt" TabIndex="19"
                                Width="90px" AutoPostBack="true" OnSelectedIndexChanged="drpTDS_Applicable_SelectedIndexChanged"
                                onKeyDown="Focusbtn(event);" Height="24">
                                <asp:ListItem Text="No" Value="N" />
                                <asp:ListItem Text="Yes" Value="Y" />
                            </asp:DropDownList>
                                TDS A/C
                            <asp:TextBox Height="24px" ID="txtTDS_AC" runat="Server" CssClass="txt" TabIndex="20"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTDS_AC_TextChanged"
                                onKeyDown="tdsac(event);"></asp:TextBox>
                                <asp:Button Width="20px" Height="24px" ID="btntxtTDS_AC" runat="server" Text="..."
                                    OnClick="btntxtTDS_AC_Click" CssClass="btnHelp" />
                                <asp:Label ID="lblTDS_AC_Code" runat="server" CssClass="lblName"></asp:Label>
                                TDS %
                            <asp:TextBox Height="24px" ID="txtTDS_Perc" runat="Server" CssClass="txt" TabIndex="21"
                                Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtTDS_Perc_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender runat="server" ID="fildtxtTDS_Perc" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtTDS_Perc">
                                </ajax1:FilteredTextBoxExtender>
                                TDS Applicable Amount &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox Height="24px" ID="txtTDSApplicalbeAmount" runat="Server" CssClass="txt"
                                TabIndex="22" Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtTDSApplicalbeAmount_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender runat="server" ID="fildtxtTDSApplicalbeAmount" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtTDSApplicalbeAmount">
                                </ajax1:FilteredTextBoxExtender>
                                <%--TDS Amount--%>tdsamount
                                <asp:TextBox Height="24px" ID="txtTDS_Amount" runat="Server" CssClass="txt" TabIndex="23"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTDS_Amount_TextChanged"
                                    ReadOnly="true" onKeyDown="Focusbtn(event);" Visible="true"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender runat="server" ID="fildtxtTDS_Amount" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtTDS_Amount">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">EInvoice No:
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txteinvoiceno" runat="Server" CssClass="txt" TabIndex="41" Width="160px"
                                    Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                            </td>
                            <td>ACKNo:
                                <asp:TextBox ID="txtackno" runat="Server" CssClass="txt" TabIndex="42" Width="250px"
                                    Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <br />
                <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" autosize="true"
                    Width="80%" align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
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
                                <asp:Panel ID="pnlInner" runat="server" ScrollBars="Auto" autosize="true" Width="100%"
                                    Direction="LeftToRight" BackColor="#FFFFE4" Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                    <asp:GridView ID="grdPopup" CssClass="select" runat="server" AutoGenerateColumns="true"
                                        AllowPaging="true" PageSize="25" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
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
                    BorderColor="Teal" BorderWidth="1px" Height="300px" BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
                    <table width="80%" align="center">
                        <tr>
                            <td colspan="2" align="center" style="background-color: lightslategrey; color: White;">
                                <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana"
                                    Text="Tender Details"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
