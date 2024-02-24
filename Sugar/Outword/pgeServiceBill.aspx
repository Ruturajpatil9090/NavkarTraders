<%@ Page Title="Service Bill" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeServiceBill.aspx.cs" Inherits="Sugar_pgeServiceBill" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                if (hdnfClosePopupValue == "txtGSTRateCode") {
                    document.getElementById("<%=txtGSTRateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTDS_AC") {
                    document.getElementById("<%=txtTDS_AC.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtITEM_CODE") {
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").focus();
                }
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
    </script>
    <script type="text/javascript">

        function SB() {
            var billno = document.getElementById('<%=txtDOC_NO.ClientID %>').value;

            window.open('../Report/rptServiceBill.aspx?billno=' + billno);
        }
        function EInovice() {
            var dono = document.getElementById('<%=txtDOC_NO.ClientID %>').value;
            var ID = document.getElementById('<%=hdnf.ClientID %>').value;
            window.open('../Utility/pgeEInovice.aspx?dono=' + dono + '&Type=RB&ID=' + ID, "_self");
        }
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

        function Confirm_print() {
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
                debugger;
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
                if (hdnfClosePopupValue == "txtDOC_NO") {

                    document.getElementById("<%=txtDOC_NO.ClientID %>").disabled = true;
                    document.getElementById("<%=txtDOC_NO.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                }
                if (hdnfClosePopupValue == "txtITEM_CODE") {
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLITEMNAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").focus();
                    document.getElementById("<%=hdnff1.ClientID %>").value = "0";

                }
                if (hdnfClosePopupValue == "txtGSTRateCode") {
                    document.getElementById("<%=txtGSTRateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblGSTRateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGSTRateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").disabled = false;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
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
    <script type="text/javascript" language="javascript">

        function Focusbtn(e) {
            debugger;
            if (e.keyCode == 13) {
                debugger;
                e.preventDefault();
                $("#<%=drpTDS_Applicable.ClientID %>").focus();
            }

        }

    </script>
    <script type="text/javascript">
        function accode(e) {
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
        function item(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();

                $("#<%=hdnff1.ClientID%>").val("0");
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
                debugger;
                e.preventDefault();
                $("#<%=drpTDS_Applicable.ClientID %>").focus();
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


        function gstratecode(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtGSTRateCode.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtGSTRateCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtGSTRateCode.ClientID %>").val(unit);
                __doPostBack("txtGSTRateCode", "TextChanged");
            }
        }
        function Back() {

            //alert(td);
            window.open('../Outword/PgeServiceBillUtility.aspx', '_self');
        }
        function SBPen(DO) {
            var Action = 1;
            window.open('../Outword/pgeServiceBill.aspx?rbid=' + DO + '&Action=' + Action, "_self");
        }
    </script>
    <script type="text/javascript">
        function DeleteConform() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= servicebilldoc.ClientID %>").value;
            var Autoid = document.getElementById("<%= servicebillid.ClientID %>").value;

            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';

            var XML = "<ROOT><ServiceBillHead Doc_No='" + DocNo + "' rbid='" + Autoid + "' Company_Code='" + Company_Code + "' " +
                   "Year_Code='" + Year_Code + "'></ServiceBillHead></ROOT>";
            var spname = "ServiceBill";
            var status = document.getElementById("<%= btnDelete.ClientID %>").value;
            ProcessXML(XML, status, spname);
        }
        function Validate() {
            debugger;
            $("#loader").show();

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

            var Outword_Date = '<%= Session["Outword_Date"] %>';
            var DocDate = $("#<%=txtDOC_DATE.ClientID %>").val();
            //Outword_Date = Outword_Date.slice(6, 11) + Outword_Date.slice(3, 5) + "/" + Outword_Date.slice(0, 2);
            Outword_Date = Outword_Date.slice(6, 11) + "/" + Outword_Date.slice(3, 5) + "/" + Outword_Date.slice(0, 2);
            DocDate = DocDate.slice(6, 11) + "/" + DocDate.slice(3, 5) + "/" + DocDate.slice(0, 2);
            if (DocDate < Outword_Date) {
                alert('GST Return Fined please Do not edit Record');
                $("#loader").hide();
                return false;
            }

            if ($("#<%=txtAC_CODE.ClientID %>").val() == "") {
                $("#<%=txtAC_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtGSTRateCode.ClientID %>").val() == "") {
                $("#<%=txtGSTRateCode.ClientID %>").focus();
                return;
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
            var count = 0;
            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            if (gridView == null) {
                alert('Please Enter  Details!');
                $("#<%=txtITEM_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            var grdrow = gridView.getElementsByTagName("tr");
            var ro = (grdrow.length - 1);
            if (ro == 0) {
                alert('Please Enter  Details!');
                $("#<%=txtITEM_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if (ro >= 1) {
                for (var i = 1; i < grdrow.length; i++) {
                    var action = gridView.rows[i].cells[8].innerHTML;
                    gridView.rows[i].cells[9].disabled = false;
                    if (gridView.rows[i].cells[8].innerHTML == "D") {
                        count = count + 1;
                    }
                }
                if (ro == count) {
                    alert('Minimum One  Details is compulsory!');
                    $("#<%=txtITEM_CODE.ClientID %>").focus();
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
                var Doc_No = 0, rbid = 0, rbdid = 0;
                var spname = "ServiceBill";
                var XML = "<ROOT>";
                var status = document.getElementById("<%= btnSave.ClientID %>").value;
                if (status == "Update") {
                    Doc_No = document.getElementById("<%= servicebilldoc.ClientID %>").value;
                    rbid = document.getElementById("<%= servicebillid.ClientID %>").value;
                }
                var gridView = document.getElementById("<%=grdDetail.ClientID %>");
                var grdrow = gridView.getElementsByTagName("tr");

                //var DOC_NO = $("#<%=txtDOC_NO.ClientID %>").val() == "" ? 0 : $("#<%=txtDOC_NO.ClientID%>").val();
                var d = $("#<%=txtDOC_DATE.ClientID %>").val();
                var DOC_DATE = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                var AC_CODE = $("#<%=txtAC_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtAC_CODE.ClientID%>").val();
                var GSTRateCode = $("#<%=txtGSTRateCode.ClientID %>").val() == "" ? 0 : $("#<%=txtGSTRateCode.ClientID%>").val();
                var SUBTOTAL = $("#<%=txtSubtotal.ClientID %>").val() == "" ? 0 : $("#<%=txtSubtotal.ClientID%>").val();
                var CGSTRate = $("#<%=txtCGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtCGSTRate.ClientID%>").val();
                var CGSTAmount = $("#<%=txtCGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtCGSTAmount.ClientID%>").val();
                var IGSTRate = $("#<%=txtIGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtIGSTRate.ClientID%>").val();
                var IGSTAmount = $("#<%=txtIGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtIGSTAmount.ClientID%>").val();
                var SGSTRate = $("#<%=txtSGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtSGSTRate.ClientID%>").val();
                var SGSTAmount = $("#<%=txtSGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtSGSTAmount.ClientID%>").val();
                var TOTAL = $("#<%=txtTotal.ClientID %>").val() == "" ? 0 : $("#<%=txtTotal.ClientID%>").val();
                var ROUND_OFF = $("#<%=txtRoundOff.ClientID %>").val() == "" ? 0 : $("#<%=txtRoundOff.ClientID%>").val();
                var BILL_AMOUNT = $("#<%=txtBILL_AMOUNT.ClientID %>").val() == "" ? 0 : $("#<%=txtBILL_AMOUNT.ClientID%>").val();
                var isTDS = $("#<%=drpTDS_Applicable.ClientID %>").val();
                var TDS_AC = $("#<%=txtTDS_AC.ClientID %>").val() == "" ? 0 : $("#<%=txtTDS_AC.ClientID%>").val();
                var TDSPer = $("#<%=txtTDS_Perc.ClientID %>").val() == "" ? 0 : $("#<%=txtTDS_Perc.ClientID%>").val();
                var TDSApplicableAmt = $("#<%=txtTDSApplicalbeAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtTDSApplicalbeAmount.ClientID%>").val();
                var TDS = $("#<%=txtTDS_Amount.ClientID %>").val() == "" ? 0 : $("#<%=txtTDS_Amount.ClientID%>").val();

                var TCS_Rate = $("#<%=txtTCSRate.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSRate.ClientID %>").val();
                var TCS_Amt = $("#<%=txtTCSAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSAmt.ClientID %>").val();
                var TCS_Net_Payable = $("#<%=txtTCSNet_Payable.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSNet_Payable.ClientID %>").val();

                var billNo = $("#<%=txtbillNo.ClientID%>").val();
                var ac = document.getElementById("<%= hdnfac.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfac.ClientID %>").value;
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
                var Company_Code = '<%= Session["Company_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';
                var Branch_Code = '<%= Session["Branch_Code"] %>';

                var DOCNO = "";
                if (status == "Save") {
                    DOCNO = "Date='" + DOC_DATE + "'";
                }
                else {
                    DOCNO = "Doc_No='" + Doc_No + "' Date='" + DOC_DATE + "'";
                }

                XML = XML + "<ServiceBillHead " + DOCNO + " Customer_Code='" + AC_CODE + "' GstRateCode='" + GSTRateCode + "' Subtotal='" + SUBTOTAL + "' " +
                        "CGSTRate='" + CGSTRate + "' CGSTAmount='" + CGSTAmount + "' SGSTRate='" + SGSTRate + "' SGSTAmount='" + SGSTAmount + "' " +
                        "IGSTRate='" + IGSTRate + "' IGSTAmount='" + IGSTAmount + "' Total='" + TOTAL + "' Round_Off='" + ROUND_OFF + "' " +
                        "Final_Amount='" + BILL_AMOUNT + "' IsTDS='" + isTDS + "' TDS_Ac='" + TDS_AC + "' TDS_Per='" + TDSPer + "' " +
                        "TDSAmount='" + TDSApplicableAmt + "' TDS='" + TDS + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
                        "Created_By='" + USER + "' Modified_By='" + USER + "' billno='" + billNo + "' " +
                        "cc='" + ac + "' ta='" + ta + "' rbid='" + rbid + "' TCS_Rate='" + TCS_Rate + "' TCS_Amt='" + TCS_Amt + "' TCS_Net_Payable='" + TCS_Net_Payable + "' einvoiceno='" + einvoiceno + "' ackno='" + ackno + "' IsDeleted='1'>";

                //Detail Calculation
                var gridViews = document.getElementById("<%=grdDetail.ClientID %>");
                var grdrows = gridView.getElementsByTagName("tr");
                var ORDER_CODE = 0;
                var narrationForServicebill = "Service bill no:" + Doc_No + " " + $("#<%=LblPartyname.ClientID %>").text();
                var TCSNarration = 'TCS' + $("#<%=LblPartyname.ClientID %>").text() + " " + Doc_No;
                var ddid = rbdid;
                for (var i = 1; i < grdrows.length; i++) {
                    var ID = gridViews.rows[i].cells[2].innerHTML;
                    var item_code = gridViews.rows[i].cells[3].innerHTML;
                    var description = gridViews.rows[i].cells[5].innerHTML;
                    if (description == "&nbsp;") {
                        description = "";
                    }
                    var amount = gridViews.rows[i].cells[6].innerHTML == "" ? 0.00 : gridViews.rows[i].cells[6].innerHTML;
                    var ic = gridViews.rows[i].cells[10].innerHTML == "" ? 0 : gridViews.rows[i].cells[10].innerHTML;
                    if (ic == "" || ic == "&nbsp;") {
                        ic = 0;
                    }
                    var saleac = gridViews.rows[i].cells[11].innerHTML == "" ? 0 : gridViews.rows[i].cells[11].innerHTML;
                    if (saleac == "" || saleac == "&nbsp;") {
                        saleac = 0;
                    }
                    var saleid = gridViews.rows[i].cells[12].innerHTML == "" ? 0 : gridViews.rows[i].cells[12].innerHTML;
                    if (saleid == "" || saleid == "&nbsp;") {
                        saleid = 0;
                    }

                    var action = gridViews.rows[i].cells[8].innerHTML;
                    if (gridViews.rows[i].cells[8].innerHTML == "A") {
                        XML = XML + "<ServiceBillDetailInsert Doc_No='" + Doc_No + "' Detail_Id='" + ID + "' Item_Code='" + item_code + "' Description='" + description + "' " +
                             "Amount='" + amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' ic='" + ic + "' rbid='" + rbid + "' rbdid='" + ddid + "'/>";

                        ddid = parseInt(ddid) + 1;
                    }
                    else if (gridViews.rows[i].cells[8].innerHTML == "U") {
                        var detailid = gridViews.rows[i].cells[7].innerHTML;
                        XML = XML + "<ServiceBillDetail Doc_No='" + Doc_No + "' Detail_Id='" + ID + "' Item_Code='" + item_code + "' Description='" + description + "' " +
                             "Amount='" + amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' ic='" + ic + "' rbid='" + rbid + "' rbdid='" + detailid + "'/>";
                    }
                    else if (gridViews.rows[i].cells[8].innerHTML == "D") {
                        var detailid = gridViews.rows[i].cells[7].innerHTML;
                        XML = XML + "<ServiceBillDetailDelete Doc_No='" + Doc_No + "' Detail_Id='" + ID + "' Item_Code='" + item_code + "' Description='" + description + "' " +
                             "Amount='" + amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' ic='" + ic + "' rbid='" + rbid + "' rbdid='" + detailid + "'/>";
                    }

                    if (amount > 0) {
                        ORDER_CODE = parseInt(ORDER_CODE) + 1;

                        XML = XML + "<Ledger TRAN_TYPE='RB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + saleac + "' " +
                                                        "UNIT_code='0' NARRATION='" + narrationForServicebill + "' AMOUNT='" + amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='RB' SORT_NO='" + Doc_No + "' ac='" + saleid + "' vc='0' progid='10' tranid='0'/>";
                    }

                }

                // CGSTAmount Effect
                if (CGSTAmount > 0) {
                    ORDER_CODE = parseInt(ORDER_CODE) + 1;
                    XML = XML + "<Ledger TRAN_TYPE='RB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%= Session["SaleCGSTAc"] %>' + "' " +
                                                    "UNIT_code='0' NARRATION='" + narrationForServicebill + "' AMOUNT='" + CGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                    "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                    "SORT_TYPE='RB' SORT_NO='" + Doc_No + "' ac='" + '<%= Session["SaleCGSTid"] %>' + "' vc='0' progid='10' tranid='0'/>";
                }
                //End

                //SGSTAmount Effect
                if (SGSTAmount > 0) {
                    ORDER_CODE = parseInt(ORDER_CODE) + 1;
                    XML = XML + "<Ledger TRAN_TYPE='RB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%= Session["SaleSGSTAc"] %>' + "' " +
                                                    "UNIT_code='0' NARRATION='" + narrationForServicebill + "' AMOUNT='" + SGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                    "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                    "SORT_TYPE='RB' SORT_NO='" + Doc_No + "' ac='" + '<%= Session["SaleSGSTid"] %>' + "' vc='0' progid='10' tranid='0'/>";
                }
                //end

                //IGSTAmount Effect
                if (IGSTAmount > 0) {
                    ORDER_CODE = parseInt(ORDER_CODE) + 1;
                    XML = XML + "<Ledger TRAN_TYPE='RB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%= Session["SaleIGSTAc"] %>' + "' " +
                                                   "UNIT_code='0' NARRATION='" + narrationForServicebill + "' AMOUNT='" + IGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                   "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                   "SORT_TYPE='RB' SORT_NO='" + Doc_No + "' ac='" + '<%= Session["SaleIGSTid"] %>' + "' vc='0' progid='10' tranid='0'/>";
                }
                //end

                //Ac Code Effect
                if (BILL_AMOUNT > 0) {
                    ORDER_CODE = parseInt(ORDER_CODE) + 1;
                    XML = XML + "<Ledger TRAN_TYPE='RB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + AC_CODE + "' " +
                                                  "UNIT_code='0' NARRATION='" + narrationForServicebill + "' AMOUNT='" + BILL_AMOUNT + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                  "SORT_TYPE='RB' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='10' tranid='0'/>";
                }
                //end

                //TDS Ac
                if (TDS > 0) {
                    ORDER_CODE = parseInt(ORDER_CODE) + 1;
                    XML = XML + "<Ledger TRAN_TYPE='RB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + AC_CODE + "' " +
                                                  "UNIT_code='0' NARRATION='" + narrationForServicebill + "' AMOUNT='" + TDS + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                  "SORT_TYPE='RB' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='10' tranid='0'/>";

                    ORDER_CODE = parseInt(ORDER_CODE) + 1;
                    XML = XML + "<Ledger TRAN_TYPE='RB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + TDS_AC + "' " +
                                                 "UNIT_code='0' NARRATION='" + narrationForServicebill + "' AMOUNT='" + TDS + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                 "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                 "SORT_TYPE='RB' SORT_NO='" + Doc_No + "' ac='" + ta + "' vc='0' progid='10' tranid='0'/>";
                }

                if (TCS_Amt > 0) {
                    ORDER_CODE = parseInt(ORDER_CODE) + 1;

                    XML = XML + "<Ledger TRAN_TYPE='RB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                                         "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                         "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                         "SORT_TYPE='RB' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleTCSAcid"] %>' + "' vc='0' progid='10' tranid='0'/>";

                    ORDER_CODE = parseInt(ORDER_CODE) + 1;

                    XML = XML + "<Ledger TRAN_TYPE='RB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + AC_CODE + "' " +
                                                        "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + ORDER_CODE + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='RB' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='10' tranid='0'/>";
                }

                XML = XML + "</ServiceBillHead></ROOT>";
                ProcessXML(XML, status, spname);

            }
            catch (exx) {
                $("#loader").hide();
                alert(exx)
            }
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
                        //window.open('../Transaction/pgeReceiptPaymentxml.aspx?tranid=' + tranid + '&Action=1', "_self");
                        window.open('../Outword/pgeServiceBill.aspx?rbid=' + id + '&Action=1', "_self");

                    }
                }
                else {
                    var num = parseInt(response.d);

                    if (isNaN(num)) {
                        alert(response.d)

                    }
                    else {
                        window.open('../Outword/PgeServiceBillUtility.aspx', "_self");
                    }
                }

            }
        }
    </script>
    <style>
        #loader
        {
            width: 100%;
            height: 100%;
            background-color: gray;
            position: fixed;
            margin: -0.7%;
            opacity: 0.6;
        }
    </style>
    <%--<script language="JavaScript" type="text/javascript">

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
            <asp:Label ID="label1" runat="server" Text="   Service Bill   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px;
        float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px;
        border-right: 0px;">
        <img height="20%" width="20%" src="../Images/ajax-loader3.gif" />
    </div>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnff1" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfac" runat="server" />
            <asp:HiddenField ID="hdnftds" runat="server" />
            <asp:HiddenField ID="servicebilldoc" runat="server" />
            <asp:HiddenField ID="servicebillid" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 0px; margin-top: 0px; z-index: 100;">
                <table width="80%" align="left">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="save" OnClick="btnAdd_Click" TabIndex="27" />
                            &nbsp;
                            <asp:Button ID="btnSave" OnClientClick="if (!Validate()) return false;" runat="server"
                                Text="Save" CssClass="btnHelp" Width="90px" Height="24px" ValidationGroup="add"
                                OnClick="btnSave_Click" TabIndex="26" />
                            &nbsp;
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="save" OnClick="btnEdit_Click" TabIndex="28" />
                            &nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()"
                                TabIndex="29" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" TabIndex="30" />
                            &nbsp;
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                                TabIndex="31" Height="24px" ValidationGroup="save" OnClientClick="Back()" />
                            &nbsp;
                            <asp:Button runat="server" ID="btnPrintSaleBill" Text="Print" Visible="true" CssClass="btnHelp"
                                Width="80px" Height="24px" OnClientClick="SB();" />
                            &nbsp;
                            <asp:Button runat="server" ID="btnGenEinvoice" Text="Generate EInvoice" CssClass="btnHelp"
                                Width="120px" Height="24px" OnClientClick="EInovice();" />
                             &nbsp;
                            <asp:Button runat="server" ID="btnCancleEinvoice" Text="Cancle EInvoice" CssClass="btnHelp"
                                Width="120px" Height="24px" OnClientClick="ConfirmCancle();" OnClick="btnCancleEInvoice_Click" />
                        </td>
                        <td align="center">
                            &nbsp;<asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                                OnClick="btnFirst_Click" Width="90px" Visible="false" Height="24px" />
                            &nbsp;<asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                                Visible="false" OnClick="btnPrevious_Click" Width="90px" Height="24px" />
                            &nbsp;<asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                                Visible="false" OnClick="btnNext_Click" Width="90px" Height="24px" />
                            &nbsp;<asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                                Visible="false" OnClick="btnLast_Click" Width="90px" Height="24px" />
                        </td>
                    </tr>
                    <table width="90%" align="left" cellspacing="5">
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                    Font-Size="Small" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Change No:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                    TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                                &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                    Font-Size="Small" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">
                                Bill No.:
                            </td>
                            <td align="left" style="width: 10%;" colspan="2">
                                <asp:TextBox ID="txtDOC_NO" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtDOC_NO_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtdoc_no" runat="server" Text="..." Width="80px" OnClick="btntxtDOC_NO_Click"
                                    CssClass="btnHelp" Height="24px" />
                                <asp:Label ID="lbldocid" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                    Font-Size="Small" Visible="false" ForeColor="Red"></asp:Label>
                            </td>
                            <td align="right" style="width: 5%;">
                                Bill Date:
                            </td>
                            <td align="left" style="width: 10%;">
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
                            <td align="right" style="width: 10%;">
                                Customer:
                            </td>
                            <td align="left" style="width: 10%;" colspan="2">
                                <asp:TextBox ID="txtAC_CODE" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                    onkeydown="accode(event);" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtAC_CODE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtAC_CODE" runat="server" Text="..." OnClick="btntxtAC_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="LblPartyname" runat="server" CssClass="lblName"></asp:Label>
                                <%--  Purc No:<asp:TextBox ID="txtPURCNO" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtPURCNO_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtPURCNO" runat="server" Text="..." OnClick="btntxtPURCNO_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                &nbsp;DO:<asp:Label runat="server" ID="lblDONo" ForeColor="Yellow"></asp:Label>--%>
                            </td>
                            <td align="right" style="width: 5%;">
                                State Code:
                            </td>
                            <td align="left" style="width: 10%;" colspan="2">
                                <asp:TextBox ID="txtstate_code" runat="Server" CssClass="txt" TabIndex="4" Width="80px"
                                    Style="text-align: right;" AutoPostBack="True" Height="24px" ReadOnly="true"></asp:TextBox>
                                <asp:Label ID="lblstatename" runat="server" CssClass="lblName"></asp:Label>
                                <%--  Purc No:<asp:TextBox ID="txtPURCNO" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtPURCNO_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtPURCNO" runat="server" Text="..." OnClick="btntxtPURCNO_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                &nbsp;DO:<asp:Label runat="server" ID="lblDONo" ForeColor="Yellow"></asp:Label>--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                GST Rate Code
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txtGSTRateCode" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                    onkeydown="gstratecode(event);" Style="text-align: right;" AutoPostBack="false"
                                    OnTextChanged="txtGSTRateCode_TextChanged" Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtGSTRateCode" runat="server" Text="..." OnClick="btntxtGSTRateCode_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblGSTRateName" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="right">
                                Bill No.:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtbillNo" runat="Server" CssClass="txt" TabIndex="6" Width="200px"
                                    Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtbillNo_TextChanged"
                                    Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="visibility: hidden;">
                            <td style="width: 100px; vertical-align: top;" align="center">
                                <asp:Button ID="btnOpenDetailsPopup" runat="server" Text="ADD" CssClass="btnHelp"
                                    Width="80px" Height="24px" OnClick="btnOpenDetailsPopup_Click" TabIndex="7" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td align="center" style="text-decoration: underline; margin-left: 50px; color: White;">
                                Item Details
                            </td>
                            <td>
                                <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblNo" runat="server" ForeColor="Azure" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <table width="70%" align="center" cellspacing="5" style="margin-left: 100px;">
                                    <tr>
                                        <td align="left" style="width: 3%;">
                                            Item:
                                        </td>
                                        <td align="left" style="width: 10%;" colspan="3">
                                            <asp:TextBox ID="txtITEM_CODE" runat="Server" CssClass="txt" TabIndex="7" Width="80px"
                                                onkeydown="item(event);" Height="24px" Style="text-align: right;" AutoPostBack="false"
                                                OnTextChanged="txtITEM_CODE_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btntxtITEM_CODE" runat="server" Text="..." OnClick="btntxtITEM_CODE_Click"
                                                CssClass="btnHelp" Height="24px" Width="20px" />
                                            <asp:Label ID="LBLITEMNAME" runat="server" CssClass="lblName"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 5%;">
                                            Description:
                                        </td>
                                        <td align="left" style="width: 10%;">
                                            <asp:TextBox ID="txtDescription" runat="Server" CssClass="txt" TabIndex="8" Width="200px"
                                                TextMode="MultiLine" OnTextChanged="txtDescription_TextChanged"   AutoPostBack="false"  Style="text-align: left;" Height="50px"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 5%;">
                                            Amount:
                                        </td>
                                        <td align="left" style="width: 10%;">
                                            <asp:TextBox ID="txtAmount" runat="Server" CssClass="txt" TabIndex="9" Width="80px"
                                                Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtAmount_extChanged"></asp:TextBox>
                                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtRATE" runat="server" FilterType="Numbers,Custom"
                                                ValidChars="." TargetControlID="txtAmount">
                                            </ajax1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                                            Height="25px" OnClick="btnAdddetails_Click" TabIndex="10" />
                                                    </td>
                                                    <td style="margin-left: 10px;">
                                                        <asp:Button ID="btnClosedetails" runat="server" Text="Reset" CssClass="btnHelp" Width="80px"
                                                            Height="25px" OnClick="btnClosedetails_Click" TabIndex="11" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" style="width: 100%; margin-left: 50px;">
                                <div style="width: 100%; position: relative; vertical-align: top;">
                                    <asp:UpdatePanel ID="upGrid" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlgrdDetail" runat="server" align="left" ScrollBars="Both" Height="200px"
                                                Width="800px" BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true"
                                                Font-Names="Verdana" Font-Size="11px" BackColor="SeaShell" Style="margin-left: 30px;
                                                float: left;">
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
                            </td>
                            <td style="width: 100%;" align="left">
                                <table width="100%" cellspacing="5">
                                    <tr>
                                        <td align="left">
                                            Sub Total:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtSubtotal" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                                OnTextChanged="txtSubtotal_TextChanged" Style="text-align: right;" TabIndex="12"
                                                Width="120px" ReadOnly="true"></asp:TextBox>
                                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtOTHER_AMT" runat="server" FilterType="Numbers,Custom"
                                                TargetControlID="txtSubtotal" ValidChars=".">
                                            </ajax1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CGST%
                                        </td>
                                        <td style="">
                                            <asp:TextBox ID="txtCGSTRate" runat="Server" CssClass="txt" Width="40px" TabIndex="13"
                                                Style="text-align: right;" Height="24px" AutoPostBack="true" OnTextChanged="txtCGSTRate_TextChanged"
                                                ReadOnly="true"></asp:TextBox>
                                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtCGSTRate" runat="server" FilterType="Numbers,Custom"
                                                TargetControlID="txtCGSTRate" ValidChars=".">
                                            </ajax1:FilteredTextBoxExtender>
                                            <asp:TextBox ID="txtCGSTAmount" runat="Server" CssClass="txt" Width="75px" ReadOnly="true"
                                                Style="text-align: right;" Height="24px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="">
                                            SGST%
                                        </td>
                                        <td style="">
                                            <asp:TextBox ID="txtSGSTRate" runat="Server" CssClass="txt" Width="40px" TabIndex="14"
                                                AutoPostBack="true" OnTextChanged="txtSGSTRate_TextChanged" Style="text-align: right;"
                                                Height="24px" ReadOnly="true"></asp:TextBox>
                                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtSGSTRate" runat="server" FilterType="Numbers,Custom"
                                                TargetControlID="txtSGSTRate" ValidChars=".">
                                            </ajax1:FilteredTextBoxExtender>
                                            <asp:TextBox ID="txtSGSTAmount" runat="Server" CssClass="txt" Width="75px" ReadOnly="true"
                                                Style="text-align: right;" Height="24px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="">
                                            IGST%
                                        </td>
                                        <td style="">
                                            <asp:TextBox ID="txtIGSTRate" runat="Server" CssClass="txt" Width="40px" AutoPostBack="true"
                                                OnTextChanged="txtIGSTRate_TextChanged" TabIndex="15" Style="text-align: right;"
                                                Height="24px" ReadOnly="true"></asp:TextBox>
                                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtIGSTRate" runat="server" FilterType="Numbers,Custom"
                                                TargetControlID="txtIGSTRate" ValidChars=".">
                                            </ajax1:FilteredTextBoxExtender>
                                            <asp:TextBox ID="txtIGSTAmount" runat="Server" CssClass="txt" Width="75px" ReadOnly="true"
                                                Style="text-align: right;" Height="24px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 40%;">
                                            Total:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtTotal" runat="Server" CssClass="txt" TabIndex="16" ReadOnly="true"
                                                Width="120px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtTotal_TextChanged"
                                                Height="24px"></asp:TextBox>
                                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtSUBTOTAL" runat="server" FilterType="Numbers,Custom"
                                                ValidChars="." TargetControlID="txtTotal">
                                            </ajax1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Round Off:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtRoundOff" runat="Server" CssClass="txt" TabIndex="17" Width="120px"
                                                Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtRoundOff_TextChanged"
                                                Height="24px"></asp:TextBox>
                                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtCASH_ADVANCE" runat="server" FilterType="Numbers,Custom"
                                                ValidChars=".,-,+" TargetControlID="txtRoundOff">
                                            </ajax1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Final Amount:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBILL_AMOUNT" runat="Server" CssClass="txt" ReadOnly="true" Width="120px"
                                                Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtBILL_AMOUNT_TextChanged"
                                                Height="24px" TabIndex="18"></asp:TextBox>
                                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtBILL_AMOUNT" runat="server" FilterType="Numbers,Custom"
                                                ValidChars="." TargetControlID="txtBILL_AMOUNT">
                                            </ajax1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            TCS%
                                        </td>
                                        <td style="">
                                            <asp:TextBox ID="txtTCSRate" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                                OnTextChanged="txtTCSRate_TextChanged" TabIndex="44" Style="text-align: right;"
                                                Height="24px"></asp:TextBox>
                                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers,Custom"
                                                ValidChars="." TargetControlID="txtTCSRate">
                                            </ajax1:FilteredTextBoxExtender>
                                            <asp:TextBox ID="txtTCSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                                TabIndex="45" OnTextChanged="txtTCSAmt_TextChanged" ReadOnly="true" Style="text-align: right;"
                                                Width="98px"></asp:TextBox>
                                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers,Custom"
                                                TargetControlID="txtTCSAmt" ValidChars=".">
                                            </ajax1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Net Payable:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtTCSNet_Payable" runat="Server" CssClass="txt" ReadOnly="true"
                                                TabIndex="46" Width="140px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtTCSNet_Payable_TextChanged"
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
            <asp:Panel ID="Panel1" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 60%;" align="Left" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left">
                            TDS Applicable
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
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
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
                            TDS Amount
                            <asp:TextBox Height="24px" ID="txtTDS_Amount" runat="Server" CssClass="txt" TabIndex="23"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTDS_Amount_TextChanged"
                                ReadOnly="true" onKeyDown="Focusbtn(event);"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="fildtxtTDS_Amount" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtTDS_Amount">
                            </ajax1:FilteredTextBoxExtender>
                            EInvoice No:
                            <asp:TextBox ID="txteinvoiceno" runat="Server" CssClass="txt" TabIndex="24" Width="120px"
                                Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                            ACKNo:
                            <asp:TextBox ID="txtackno" runat="Server" CssClass="txt" TabIndex="25" Width="150px"
                                Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
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
                BorderColor="Teal" BorderWidth="1px" Height="420px" BorderStyle="Solid" Style="z-index: 4999;
                left: 20%; top: 10%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
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
