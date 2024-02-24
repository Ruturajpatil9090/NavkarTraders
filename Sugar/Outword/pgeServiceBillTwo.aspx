<%@ Page Title="Cold Storage" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeServiceBillTwo.aspx.cs" Inherits="Sugar_pgeServiceBillTwo" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <script type="text/javascript">
        function SB() {
            var billno = document.getElementById('<%=txtDOC_NO.ClientID %>').value;
            window.open('../Report/rptColdStorageBill.aspx?billno=' + billno);
        }
        
        function EInovice() {
            debugger;
            var dono = document.getElementById('<%=txtDOC_NO.ClientID %>').value;
                    var ID = document.getElementById('<%=hdnf.ClientID %>').value;
                   
            window.open('../Utility/pgeEInovice.aspx?dono=' + dono + '&Type=CS', "_self");
        }

        function CSPen(id) {
            var Action = 1;
            window.open('../Outword/pgeServiceBillTwo.aspx?csid=' + id + '&Action=' + Action, "_self");
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

        function Confirm() {
            debugger;
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
                if (hdnfClosePopupValue == "txtITEM_CODE") {
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblitem_Name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBrand_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%=lblband_Name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    document.getElementById("<%=txtlot_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[8].innerText;                   
                    document.getElementById("<%=lblLotId.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[10].innerText;
                    document.getElementById("<%= hdnfLotId.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[10].innerText;
                    document.getElementById("<%=txtBags.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[7].innerText;
                    document.getElementById("<%=txtwt_per.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[5].innerText;
                    document.getElementById("<%=txtarrival_Date.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[9].innerText;
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBrand_Code") {
                    document.getElementById("<%=txtBrand_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblband_Name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBrand_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtDOC_NO") {

                    document.getElementById("<%=txtDOC_NO.ClientID %>").disabled = true;
                    document.getElementById("<%=txtDOC_NO.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtDOC_NO.ClientID %>").focus();
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
                if (hdnfClosePopupValue == "txtAC_CODE") {
                    document.getElementById("<%=txtAC_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtITEM_CODE") {
                    document.getElementById("<%=txtITEM_CODE.ClientID %>").focus();
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

        function Focusbtn(e) {
            debugger;
            if (e.keyCode == 13) {
                debugger;
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }

        }

        function itemcode(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtItem_Code.ClientID %> ").click();
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
                $("#<%=btnSave.ClientID %>").focus();
            }

        }
        function brandcode(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtBrand_Code.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtBrand_Code", "TextChanged");
            }

        }
        function gstrate(e) {
            debugger;
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
        function customer(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtAC_CODE.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtAC_CODE.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtAC_CODE.ClientID %>").val(unit);
                __doPostBack("txtAC_CODE", "TextChanged");
            }

        }
        function Back() {

            //alert(td);
            window.open('../Outword/pgeColdStorage_Utility.aspx', '_self');
        }
    </script>
    <script type="text/javascript">
        function Calculations(e) {

            if (e.keyCode == 9) {
                debugger;
                var Qty = $("#<%=txtBags.ClientID %>").val() == "" ? 0 : $("#<%= txtBags.ClientID %>").val();
                var Wtper = $("#<%=txtwt_per.ClientID %>").val() == "" ? 0 : $("#<%= txtwt_per.ClientID %>").val();
                var Netkg = parseFloat(Qty * Wtper);

                document.getElementById("<%=txtNet_wt.ClientID %>").value = Netkg;
              
                var rate = $("#<%=txtRate.ClientID %>").val() == "" ? 0 : $("#<%= txtRate.ClientID %>").val();
                var amount = parseFloat(Netkg * rate);

                document.getElementById("<%=txtAmount.ClientID %>").value = amount;

            }

        }

        function DeleteConform() {
            debugger;
            var DocNo = document.getElementById("<%= hdnfdocno.ClientID %>").value;
            var coldid = document.getElementById("<%= hdnfcoldid.ClientID %>").value;

            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';

            var XML = "<ROOT><Head Doc_No='" + DocNo + "' csid='" + coldid + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "'></Head></ROOT>";
            var spname = "ColdStorage";
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

            if ($("#<%=txtAC_CODE.ClientID %>").val() == "" || $("#<%=txtAC_CODE.ClientID %>").val() == "0") {
                $("#<%=txtAC_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;

            }
            if ($("#<%=txtGSTRateCode.ClientID %>").val() == "" || $("#<%=txtGSTRateCode.ClientID %>").val() == "0") {
                $("#<%=txtGSTRateCode.ClientID %>").focus();
                $("#loader").hide();
                return false;

            }
            var count = 0;
            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            if (gridView == null) {
                alert('Please Enter Details!');
                $("#loader").hide();
                return false;
            }
            var grdrow = gridView.getElementsByTagName("tr");
            var ro = (grdrow.length - 1);
            if (ro == 0) {
                alert('Please Enter Details!');
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
                    alert('Minimum One  Details is compulsory!');
                    $("#loader").hide();
                    return false;
                }
            }
            return true;
        }
        function validation() {
            try {
                debugger;
                $("#loader").show();
                var DocNo = 0, coldid = 0;
                var insertrecord = "";
                var status = document.getElementById("<%= btnSave.ClientID %>").value;
                var spname = "ColdStorage";
                var XML = "<ROOT>";
                if (status == "Update") {
                    DocNo = document.getElementById("<%= hdnfdocno.ClientID %>").value;
                    coldid = document.getElementById("<%= hdnfcoldid.ClientID %>").value;
                }

                var d = $("#<%=txtDOC_DATE.ClientID %>").val();
                var DOC_DATE = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

                var TRAN_TYPE = $("#<%=drpCashCredit.ClientID %>").val();
                var AC_CODE = $("#<%=txtAC_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtAC_CODE.ClientID %>").val();

                var GSTRateCode = $("#<%=txtGSTRateCode.ClientID %>").val() == "" ? 0 : $("#<%=txtGSTRateCode.ClientID %>").val();

                var SUBTOTAL = $("#<%=txtSubtotal.ClientID %>").val() == "" ? 0 : $("#<%=txtSubtotal.ClientID %>").val();
                var CGSTRate = $("#<%=txtCGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtCGSTRate.ClientID %>").val();
                var CGSTAmount = $("#<%=txtCGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtCGSTAmount.ClientID %>").val();
                var IGSTRate = $("#<%=txtIGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtIGSTRate.ClientID %>").val();
                var IGSTAmount = $("#<%=txtIGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtIGSTAmount.ClientID %>").val();
                var SGSTRate = $("#<%=txtSGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtSGSTRate.ClientID %>").val();
                var SGSTAmount = $("#<%=txtSGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtSGSTAmount.ClientID %>").val();
                var TOTAL = $("#<%=txtTotal.ClientID %>").val() == "" ? 0 : $("#<%=txtTotal.ClientID %>").val();
                var ROUND_OFF = $("#<%=txtRoundOff.ClientID %>").val() == "" ? 0 : $("#<%=txtRoundOff.ClientID %>").val();
                var BILL_AMOUNT = $("#<%=txtBILL_AMOUNT.ClientID %>").val() == "" ? 0 : $("#<%=txtBILL_AMOUNT.ClientID %>").val();
                var billNo = $("#<%=txtbillNo.ClientID %>").val();
                var Branch_Code = '<%= Session["Branch_Code"] %>';
                var Company_Code = '<%= Session["Company_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';
                var USER = '<%= Session["user"] %>';
                var customerid = document.getElementById("<%= hdnfcustomerid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfcustomerid.ClientID %>").value;
                var einvoiceno = document.getElementById("<%= txteinvoiceno.ClientID  %>").value == "" ? 0 : document.getElementById("<%= txteinvoiceno.ClientID %>").value;
                var ackno = document.getElementById("<%= txtackno.ClientID   %>").value == "" ? 0 : document.getElementById("<%= txtackno.ClientID %>").value;
                var qrcode = "";
                var narrationForServicebill = $("#<%=LblPartyname.ClientID %>").text();
                if (customerid == "" || customerid == "&nbsp;") {
                    customerid = 0;
                }
                var DOCNO = "";
                if (status == "Save") {
                    DOCNOC = "Date='" + DOC_DATE + "'";
                }
                else {
                    DOCNOC = "Doc_No='" + DocNo + "' Date='" + DOC_DATE + "'";
                }

                XML = XML + "<Head " + DOCNOC + " Cash_Credit='" + TRAN_TYPE + "' Customer_Code='" + AC_CODE + "' GstRateCode='" + GSTRateCode + "' Subtotal='" + SUBTOTAL + "' CGSTRate='" + CGSTRate + "' CGSTAmount='"
                            + CGSTAmount + "' SGSTRate='" + SGSTRate + "' SGSTAmount='" + SGSTAmount + "' IGSTRate='" + IGSTRate + "' IGSTAmount='"
                            + IGSTAmount + "' Total='" + TOTAL + "' Round_off='" + ROUND_OFF + "' Final_Amount='" + BILL_AMOUNT + "' Company_Code='" + Company_Code
                            + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' billno='" + billNo + "' ccid='"
                            + customerid + "' csid='" + coldid + "' einvoiceno='" + einvoiceno + "' ackno='" + ackno + "'>";

                var gridView = document.getElementById("<%=grdDetail.ClientID %>");
                var grdrow = gridView.getElementsByTagName("tr");
                var ddid = 0;
                var Order_Code = 0;
                for (var i = 1; i < grdrow.length; i++) {
                    var i_d = gridView.rows[i].cells[2].innerHTML;
                    var item = gridView.rows[i].cells[3].innerHTML;
                    var Brand = gridView.rows[i].cells[5].innerHTML;
                    var lotno = gridView.rows[i].cells[7].innerHTML;

                    var d = gridView.rows[i].cells[8].innerHTML;
                    var Arrival_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

                    var d = gridView.rows[i].cells[9].innerHTML;
                    var Delivered_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

                    var Period = gridView.rows[i].cells[10].innerHTML;
                    var Bags = gridView.rows[i].cells[11].innerHTML;
                    var Weight = gridView.rows[i].cells[12].innerHTML;
                    var Rate = gridView.rows[i].cells[13].innerHTML;
                    var wt_per = gridView.rows[i].cells[14].innerHTML;
                    var net_wt = gridView.rows[i].cells[15].innerHTML;
                    var amount = gridView.rows[i].cells[16].innerHTML;

                    var itid = gridView.rows[i].cells[20].innerHTML;
                    var ltid = gridView.rows[i].cells[21].innerHTML;


                    if (gridView.rows[i].cells[17].innerHTML == "A") {

                        XML = XML + "<DetailInsert Doc_No='" + DocNo + "' Detail_Id='" + i_d + "' Item='" + item + "' Lot_No='" + lotno + "' Arrival_Date='" + Arrival_Date + "' Delivered_Date='" + Delivered_Date
                                         + "' Period='" + Period + "' Bags='" + Bags + "' Weight='" + Weight + "' Rate='" + Rate + "' Amount='" + amount
                                         + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' csid='" + coldid + "' ltid='" + ltid + "' " +
                                         "itid='" + itid + "' csdetailid='" + ddid + "' Wt_Per='" + wt_per + "' Brand_Code='" + Brand + "' Net_Wt='" + net_wt + "'/>";
                        ddid = parseInt(ddid) + 1;
                    }
                    if (gridView.rows[i].cells[17].innerHTML == "U") {
                        var colddid = gridView.rows[i].cells[19].innerHTML;
                        XML = XML + "<Details Doc_No='" + DocNo + "' Detail_Id='" + i_d + "' Item='" + item + "' Lot_No='" + lotno + "' Arrival_Date='" + Arrival_Date + "' Delivered_Date='" + Delivered_Date
                                        + "' Period='" + Period + "' Bags='" + Bags + "' Weight='" + Weight + "' Rate='" + Rate + "' Amount='" + amount
                                        + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' csid='" + coldid + "' ltid='" + ltid + "' " +
                                        "itid='" + itid + "' csdetailid='" + colddid + "' Wt_Per='" + wt_per + "' Brand_Code='" + Brand + "' Net_Wt='" + net_wt + "'/>";

                    }
                    if (gridView.rows[i].cells[17].innerHTML == "D") {
                        var colddid = gridView.rows[i].cells[19].innerHTML;
                        XML = XML + "<DetailDelete Doc_No='" + DocNo + "' Detail_Id='" + i_d + "' Item='" + item + "' Lot_No='" + lotno + "' Arrival_Date='" + Arrival_Date + "' Delivered_Date='" + Delivered_Date
                                       + "' Period='" + Period + "' Bags='" + Bags + "' Weight='" + Weight + "' Rate='" + Rate + "' Amount='" + amount
                                       + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' csid='" + coldid + "' ltid='" + ltid + "' " +
                                       "itid='" + itid + "' csdetailid='" + colddid + "' Wt_Per='" + wt_per + "' Brand_Code='" + Brand + "' Net_Wt='" + net_wt + "'/>";

                    }
                    if (amount > 0) {
                        Order_Code = (Order_Code) + 1;
                        var SaleAc = gridView.rows[i].cells[22].innerHTML;
                        var saleid = gridView.rows[i].cells[23].innerHTML;
                        XML = XML + "<Ledger TRAN_TYPE='CB' CASHCREDIT='' DOC_NO='" + DocNo + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + SaleAc + "' " +
                                                        "UNIT_code='0' NARRATION='" + narrationForServicebill + "' AMOUNT='" + amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='CB' SORT_NO='" + DocNo + "' ac='" + saleid + "' vc='0' progid='89' tranid='0'/>";
                    }
                }
                if (CGSTAmount > 0) {
                    Order_Code = (Order_Code) + 1;
                    XML = XML + "<Ledger TRAN_TYPE='CB' CASHCREDIT='' DOC_NO='" + DocNo + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleCGSTAc"] %>' + "' " +
                                                        "UNIT_code='0' NARRATION='" + narrationForServicebill + "' AMOUNT='" + CGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='CB' SORT_NO='" + DocNo + "' ac='" + '<%=Session["SaleCGSTid"] %>' + "' vc='0' progid='89' tranid='0'/>";

                }
                if (SGSTAmount > 0) {
                    Order_Code = (Order_Code) + 1;
                    XML = XML + "<Ledger TRAN_TYPE='CB' CASHCREDIT='' DOC_NO='" + DocNo + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleSGSTAc"] %>' + "' " +
                                                        "UNIT_code='0' NARRATION='" + narrationForServicebill + "' AMOUNT='" + SGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='CB' SORT_NO='" + DocNo + "' ac='" + '<%=Session["SaleSGSTid"] %>' + "' vc='0' progid='89' tranid='0'/>";

                }
                if (IGSTAmount > 0) {
                    Order_Code = (Order_Code) + 1;
                    XML = XML + "<Ledger TRAN_TYPE='CB' CASHCREDIT='' DOC_NO='" + DocNo + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["SaleIGSTAc"] %>' + "' " +
                                                        "UNIT_code='0' NARRATION='" + narrationForServicebill + "' AMOUNT='" + IGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='CB' SORT_NO='" + DocNo + "' ac='" + '<%=Session["SaleIGSTid"] %>' + "' vc='0' progid='89' tranid='0'/>";

                }
                if (TRAN_TYPE == "CS") {
                    AC_CODE = "1";
                    customerid = '<%= Session["CASHID"] %>';
                }
                if (BILL_AMOUNT > 0) {
                    Order_Code = (Order_Code) + 1;
                    XML = XML + "<Ledger TRAN_TYPE='CB' CASHCREDIT='' DOC_NO='" + DocNo + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + AC_CODE + "' " +
                                                        "UNIT_code='0' NARRATION='" + narrationForServicebill + "' AMOUNT='" + BILL_AMOUNT + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='CB' SORT_NO='" + DocNo + "' ac='" + customerid + "' vc='0' progid='89' tranid='0'/>";

                }
                if (ROUND_OFF != 0) {
                    if (ROUND_OFF < 0) {

                        Order_Code = parseInt(Order_Code) + 1;

                        XML = XML + "<Ledger TRAN_TYPE='CB' CASHCREDIT='' DOC_NO='" + DocNo + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["RoundOff"] %>' + "' " +
                                                            "UNIT_code='0' NARRATION='" + narrationForServicebill + "' AMOUNT='" + Math.abs(ROUND_OFF) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                            "SORT_TYPE='CB' SORT_NO='" + DocNo + "' ac='" + '<%=Session["RoundOffid"] %>' + "' vc='0' progid='89' tranid='0' saleid='" + saleid + "'/>";

                            }
                            else {
                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='CB' CASHCREDIT='' DOC_NO='" + DocNo + "' DOC_DATE='" + DOC_DATE + "' AC_CODE='" + '<%=Session["RoundOff"] %>' + "' " +
                                                            "UNIT_code='0' NARRATION='" + narrationForServicebill + "' AMOUNT='" + ROUND_OFF + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                            "SORT_TYPE='CB' SORT_NO='" + DocNo + "' ac='" + '<%=Session["RoundOffid"] %>' + "' vc='0' progid='89' tranid='0' saleid='" + saleid + "'/>";

                            }
                        }
                XML = XML + "</Head></ROOT>";
                debugger;
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
                    $("#loader").hide();
                    alert(response.d);
                },
                error: function (response) {
                    $("#loader").hide();
                    alert(response.d);
                }
            });

            var action = 1;
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
                        window.open('../Outword/pgeServiceBillTwo.aspx?csid=' + id + '&Action=1', "_self");
                    }
                }
                else {
                    var num = parseInt(response.d);

                    if (num == "NaN") {
                        alert(response.d)

                    }
                    else {
                        window.open('../Outword/pgeColdStorage_Utility.aspx', "_self");
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Cold Storage   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px;
        float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px;
        border-right: 0px;">
        <img height="40%" width="40%" src="../Images/ajax-loader3.gif" />
    </div>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfcustomerid" runat="server" />
            <asp:HiddenField ID="hdnfdocno" runat="server" />
            <asp:HiddenField ID="hdnfcoldid" runat="server" />
             <asp:HiddenField ID="hdnfLotId" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 0px; margin-top: 0px; z-index: 100;">
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
                        <td align="right" style="width: 10%;">
                            Cash / Credit:
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="drpCashCredit" Width="100px" CssClass="ddl"
                                AutoPostBack="true" TabIndex="1">
                                <asp:ListItem Text="Cash" Value="CS" />
                                <asp:ListItem Text="Credit" Value="CR" />
                                <%--<asp:ListItem Text="Cash 1" Value="CS1" />
                                <asp:ListItem Text="Credit 1" Value="CR1" />--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;">
                            Bill No.:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtDOC_NO" runat="Server" CssClass="txt" TabIndex="2" Width="80px"
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
                            <asp:TextBox ID="txtDOC_DATE" runat="Server" CssClass="txt" TabIndex="3" Width="100px"
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
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtAC_CODE" runat="Server" CssClass="txt" TabIndex="4" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" onkeydown="customer(event);"
                                OnTextChanged="txtAC_CODE_TextChanged" Height="24px"></asp:TextBox>
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
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtstate_code" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
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
                        <td align="left">
                            <asp:TextBox ID="txtGSTRateCode" runat="Server" CssClass="txt" TabIndex="6" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" onkeydown="gstrate(event);" OnTextChanged="txtGSTRateCode_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtGSTRateCode" runat="server" Text="..." OnClick="btntxtGSTRateCode_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblGSTRateName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="right">
                            Bill No.:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtbillNo" runat="Server" CssClass="txt" TabIndex="7" Width="200px"
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
                        <td colspan="6">
                            <table width="80%" align="center" cellspacing="5" style="margin-left: 100px;">
                                <tr>
                                    <td align="left">
                                        Item Code:
                                        <asp:TextBox ID="txtITEM_CODE" runat="Server" CssClass="txt" Width="80px"
                                            Height="24px" Style="text-align: right;" onkeydown="itemcode(event);" AutoPostBack="false"
                                            OnTextChanged="txtITEMCODE_OnTextChanged"></asp:TextBox>
                                        <asp:Button Width="20px" Height="24px" ID="btntxtItem_Code" runat="server" Text="..."
                                            OnClick="btntxtItem_Code_Click" CssClass="btnHelp" TabIndex="8"  />
                                        <asp:Label ID="lblitem_Name" runat="server" CssClass="lblName"></asp:Label>&emsp;
                                        Brand Code:
                                        <asp:TextBox ID="txtBrand_Code" runat="Server" CssClass="txt" TabIndex="9" Width="80px"
                                            Height="24px" Style="text-align: right;" onkeydown="brandcode(event);" AutoPostBack="false"
                                            OnTextChanged="txtBrand_Code_TextChanged"></asp:TextBox>
                                        <asp:Button Width="20px" Height="24px" ID="btntxtBrand_Code" runat="server" Text="..."
                                            OnClick="btntxtBrand_Code_Click" CssClass="btnHelp" />
                                        <asp:Label ID="lblband_Name" runat="server" CssClass="lblName"></asp:Label>
                                        Lot No:
                                        <asp:TextBox ID="txtlot_No" runat="Server" CssClass="txt" TabIndex="10" Width="80px"
                                            Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtlotNo_OnTextChanged"></asp:TextBox>
                                        <asp:Label ID="lblLotId" runat="server" CssClass="lblName"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Arrival Date:
                                        <asp:TextBox ID="txtarrival_Date" runat="Server" CssClass="txt" TabIndex="11" Width="100px"
                                            MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                            Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtArrival_DATE_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <asp:Image ID="imgcalenderArrival" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                            Width="25px" Height="15px" />
                                        <ajax1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtarrival_Date"
                                            PopupButtonID="imgcalenderArrival" Format="dd/MM/yyyy">
                                        </ajax1:CalendarExtender>
                                        Delivered Date:
                                        <asp:TextBox ID="txtdelivered_date" runat="Server" CssClass="txt" TabIndex="12" Width="100px"
                                            MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                            Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtDelivered_DATE_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <asp:Image ID="imgcalenderdelivered" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                            Width="25px" Height="15px" />
                                        <ajax1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdelivered_date"
                                            PopupButtonID="imgcalenderdelivered" Format="dd/MM/yyyy">
                                        </ajax1:CalendarExtender>
                                        Days/Period:
                                        <asp:TextBox ID="txtdays" runat="Server" CssClass="txt" TabIndex="13" Width="80px"
                                            Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtdays_OnTextChanged"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtRATE" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtdays">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        No of Bags:
                                        <asp:TextBox ID="txtBags" runat="Server" CssClass="txt" TabIndex="14" Width="80px"
                                            Style="text-align: left;" Height="24px" OnTextChanged="txtBags_OnTextChanged"
                                            onkeydown="Calculations(event);" AutoPostBack="false"></asp:TextBox>
                                        <asp:TextBox ID="txtWeight" runat="Server" CssClass="txt" TabIndex="15" Width="80px"
                                            Style="text-align: left;" Height="24px" OnTextChanged="txtWeight_TextChanged"
                                            Visible="false" AutoPostBack="false"></asp:TextBox>
                                        Wt Per:
                                        <asp:TextBox ID="txtwt_per" runat="Server" CssClass="txt" Width="80px" Style="text-align: left;"
                                            onkeydown="Calculations(event);" Height="24px" AutoPostBack="false" TabIndex="16"
                                            OnTextChanged="txtwt_per_TextChanged"></asp:TextBox>
                                        Net wt:
                                        <asp:TextBox ID="txtNet_wt" runat="Server" CssClass="txt" Width="80px" Style="text-align: left;"
                                            Height="24px" AutoPostBack="false" TabIndex="17" OnTextChanged="txtNet_wt_TextChanged"></asp:TextBox>
                                        Rate:
                                        <asp:TextBox ID="txtRate" runat="Server" CssClass="txt" Width="80px" Style="text-align: left;"
                                            onkeydown="Calculations(event);" Height="24px" AutoPostBack="false" TabIndex="18"
                                            OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                        Amount:
                                        <asp:TextBox ID="txtAmount" runat="Server" CssClass="txt" TabIndex="19" Width="80px"
                                            Style="text-align: left;" Height="24px" ReadOnly="false" OnTextChanged="txtAmount_OnTextChanged"
                                            AutoPostBack="false"></asp:TextBox>
                                        <asp:TextBox ID="txtKg" runat="Server" CssClass="txt" Width="80px" Visible="false"
                                            Style="text-align: left;" Height="24px" AutoPostBack="true" OnTextChanged="txtKg_TextChanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                            Height="25px" OnClick="btnAdddetails_Click" TabIndex="20" />
                                    </td>
                                    <td style="margin-left: 10px;">
                                        <asp:Button ID="btnClosedetails" runat="server" Text="Reset" CssClass="btnHelp" Width="80px"
                                            Height="25px" OnClick="btnClosedetails_Click" TabIndex="21" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="5" style="width: 100%; margin-left: 50px;">
                            <div style="width: 100%; position: relative; vertical-align: top;">
                                <asp:UpdatePanel ID="upGrid" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlgrdDetail" runat="server" align="left" ScrollBars="Both" Height="200px"
                                            Width="1110px" BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true"
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
                                    <td align="left" style="">
                                        Sub Total:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSubtotal" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                            OnTextChanged="txtSubtotal_TextChanged" Style="text-align: right;" TabIndex="22"
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
                                        <asp:TextBox ID="txtCGSTRate" runat="Server" CssClass="txt" Width="40px" TabIndex="23"
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
                                        <asp:TextBox ID="txtSGSTRate" runat="Server" CssClass="txt" Width="40px" TabIndex="24"
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
                                            OnTextChanged="txtIGSTRate_TextChanged" TabIndex="25" Style="text-align: right;"
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
                                        <asp:TextBox ID="txtTotal" runat="Server" CssClass="txt" TabIndex="26" ReadOnly="true"
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
                                        <asp:TextBox ID="txtRoundOff" runat="Server" CssClass="txt" TabIndex="27" Width="120px"
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
                                            Height="24px" TabIndex="28"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtBILL_AMOUNT" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtBILL_AMOUNT">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                            </table>
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
            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="save" OnClick="btnAdd_Click" TabIndex="29" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" OnClientClick="if (!Validate()) return false;"
                            Text="Save" CssClass="btnHelp" Width="90px" Height="24px" ValidationGroup="add"
                            OnClick="btnSave_Click" TabIndex="30" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="save" OnClick="btnEdit_Click" TabIndex="31" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()"
                            TabIndex="32" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" TabIndex="33" />
                        &nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="80px"
                            OnClientClick="Back()" Height="25px" TabIndex="34" />
                        &nbsp;
                        <asp:Button runat="server" ID="btnPrintSaleBill" Text="Print" Visible="true" CssClass="btnHelp"
                            Width="80px" Height="24px" OnClientClick="SB();" />
                                       &nbsp;
                                <asp:Button runat="server" ID="btnGenEinvoice" Text="Generate EInvoice" CssClass="btnHelp"
                                    Width="120px" Height="24px" OnClientClick="EInovice();" />
                                <asp:Button runat="server" ID="btnCancleEinvoice" Text="Cancle EInvoice" CssClass="btnHelp"
                                    Width="120px" Height="24px" OnClientClick="ConfirmCancle();" OnClick="btnCancleEInvoice_Click" />
                    </td>
                    <td align="center">
                        &nbsp;<asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnFirst_Click" Visible="false" Width="90px" Height="24px" />
                        &nbsp;<asp:Button ID="btnPrevious" Visible="false" runat="server" Text="<" ToolTip="First"
                            CssClass="btnHelp" OnClick="btnPrevious_Click" Width="90px" Height="24px" />
                        &nbsp;<asp:Button ID="btnNext" Visible="false" runat="server" Text=">" ToolTip="First"
                            CssClass="btnHelp" OnClick="btnNext_Click" Width="90px" Height="24px" />
                        &nbsp;<asp:Button ID="btnLast" Visible="false" runat="server" Text=">>" ToolTip="First"
                            CssClass="btnHelp" OnClick="btnLast_Click" Width="90px" Height="24px" />
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
