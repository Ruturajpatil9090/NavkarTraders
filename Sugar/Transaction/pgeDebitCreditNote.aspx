<%@ Page Title="Debit Credit Note" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeDebitCreditNote.aspx.cs" Inherits="pgeDebitCreditNote" %>

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
                if (hdnfClosePopupValue == "txtAc_Code") {
                    document.getElementById("<%=txtAc_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtAc_CodeDetails") {
                    document.getElementById("<%=txtAc_CodeDetails.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGST_Rate_Code") {
                    document.getElementById("<%=txtGST_Rate_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtShipTo") {
                    document.getElementById("<%=txtShipTo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtMill_Code") {
                    document.getElementById("<%=txtMill_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtItem_Code") {
                    document.getElementById("<%=txtItem_Code.ClientID %>").focus();
                }
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
                document.getElementById("<%= txtSearchText.ClientID %>").value = "";
            }

        });
    </script>
    <script type="text/javascript" language="javascript">
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
        function SB() {
            debugger;
            var billno = document.getElementById('<%=txtDoc_No.ClientID %>').value;
            var type = document.getElementById('<%=drpSub_Type.ClientID %>').value;
            var partycode = document.getElementById('<%=txtAc_Code.ClientID %>').value;


            window.open('../Report/rptDebitCreditNote.aspx?billno=' + billno + '&type=' + type + '&partycode=' + partycode)
        }
        function Back() {
            var tran_type = $("#<%=drpSub_Type.ClientID %>").val();
            window.open('../Transaction/PgeDebitCreditNoteUtility.aspx?tran_type=' + tran_type, "_self")

        }
        function EInovice() {
            var dono = document.getElementById('<%=txtDoc_No.ClientID %>').value;
            var tran_type = $("#<%=drpSub_Type.ClientID %>").val();
            window.open('../Utility/pgeEInovice.aspx?dono=' + dono + '&Type=' + tran_type, "_self");
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
            debugger;
            var e = e ? e : window.event;
            var KeyCode = e.which ? e.which : e.keyCode;
            if (KeyCode == 40) {
                debugger;
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
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = "";
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtDoc_No") {
                    document.getElementById("<%= txtDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblDoc_No.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtDoc_No.ClientID %>").focus();
                }



                if (hdnfClosePopupValue == "txtAc_Code") {
                    document.getElementById("<%= txtAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblAc_code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtAc_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBillNo") {
                    document.getElementById("<%= txtBillNo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblBillid.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%= hdnfbillno.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[1].innerText;

                    document.getElementById("<%= hdnfbillid.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= hdnfBilltype.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[6].innerText;

                    document.getElementById("<%= txtRef_Date.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[2].innerText;

                    document.getElementById("<%= txtShipTo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[11].innerText;
                    document.getElementById("<%= lblShipTo.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[12].innerText;
                    document.getElementById("<%= txtMill_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[9].innerText;
                    document.getElementById("<%= lblMill_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[10].innerText;
                    document.getElementById("<%= lblShipId.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[13].innerText;
                    document.getElementById("<%= lblMillId.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[14].innerText;
                    document.getElementById("<%= txtUnit.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[7].innerText;
                    document.getElementById("<%= lblUnit.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[8].innerText;
                    document.getElementById("<%= hdnfsalebillyearcde.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[17].innerText;

                    var billno = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    var billdate = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%= txtnarration.ClientID %>").value = "as per bill no " + billno + " and bill date " + billdate;

                    document.getElementById("<%= lblQty.ClientID %>").innerText = "Qty:" + grid.rows[SelectedRowIndex + 1].cells[16].innerText;
                    document.getElementById("<%= hdnfqty.ClientID %>").value = "Qty:" + grid.rows[SelectedRowIndex + 1].cells[16].innerText;
                    document.getElementById("<%=txtRef_Date.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtGST_Rate_Code") {
                    document.getElementById("<%= txtGST_Rate_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblGST_Rate_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGST_Rate_Code.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtAc_CodeDetails") {
                    document.getElementById("<%= txtAc_CodeDetails.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblAc_CodeDetails.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtAc_CodeDetails.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtShipTo") {
                    document.getElementById("<%= txtShipTo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblShipTo.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtShipTo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtMill_Code") {
                    document.getElementById("<%= txtMill_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblMill_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtMill_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtUnit") {
                    document.getElementById("<%= txtUnit.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblUnit.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtUnit.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtItem_Code") {
                    document.getElementById("<%= txtItem_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblItem_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%= txtHSN.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[2].innerText;

                    document.getElementById("<%=txtItem_Code.ClientID %>").focus();
                }
            }
}
function SelectRow(CurrentRow, RowIndex) {
    debugger;
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
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }

        }

        function AcCode(e) {
            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtAc_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var Accode = $("#<%=txtAc_Code.ClientID %>").val();

                Accode = "0" + Accode;
                $("#<%=txtAc_Code.ClientID %>").val(Accode);

                __doPostBack("txtAc_Code", "TextChanged");

            }

        }

        function ShipTo(e) {
            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtShipTo.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var Accode = $("#<%=txtShipTo.ClientID %>").val();

                Accode = "0" + Accode;
                $("#<%=txtShipTo.ClientID %>").val(Accode);

                __doPostBack("txtShipTo", "TextChanged");

            }

        }
        function Mill_Code(e) {
            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtMill_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var Accode = $("#<%=txtMill_Code.ClientID %>").val();

                Accode = "0" + Accode;
                $("#<%=txtMill_Code.ClientID %>").val(Accode);

                __doPostBack("txtMill_Code", "TextChanged");

            }

        }




        function GSTRate(e) {
            if (e.keyCode == 112) {


                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtGST_Rate_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var GSTRate = $("#<%=txtGST_Rate_Code.ClientID %>").val();

                GSTRate = "0" + GSTRate;
                $("#<%=txtGST_Rate_Code.ClientID %>").val(GSTRate);
                __doPostBack("txtGST_Rate_Code", "TextChanged");

            }

        }

        function AcCodeDet(e) {
            if (e.keyCode == 112) {


                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtAc_CodeDetails.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var AcCode = $("#<%=txtAc_CodeDetails.ClientID %>").val();

                AcCode = "0" + AcCode;
                $("#<%=txtAc_CodeDetails.ClientID %>").val(AcCode);
                __doPostBack("txtAc_CodeDetails", "TextChanged");

            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=txtGST_Rate_Code.ClientID %>").focus();
            }

        }
        function DocNo(e) {
            if (e.keyCode == 112) {


                e.preventDefault();

                $("#<%=btntxtDoc_No.ClientID %>").click();

            }
            if (e.keyCode == 9) {

                __doPostBack("txtDoc_No", "TextChanged");

            }

        }

        function changeno(e) {
            debugger;
            if (e.keyCode == 112) {

                e.preventDefault();

                var edi = "txtEditDoc_No"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtEditDoc_No", "TextChanged");

            }
        }
        function Billno(e) {
            debugger;
            if (e.keyCode == 112) {

                e.preventDefault();

                var edi = "txtBillNo"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btntxtBillNo.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtBillNo", "TextChanged");

            }
        }
        function Unit(e) {
            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtUnit.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var Accode = $("#<%=txtUnit.ClientID %>").val();

                Accode = "0" + Accode;
                $("#<%=txtUnit.ClientID %>").val(Accode);

                __doPostBack("txtUnit", "TextChanged");

            }

        }
        function Item(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtItem_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtItem_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtItem_Code.ClientID %>").val(unit);
                __doPostBack("txtItem_Code", "TextChanged");

            }


        }
    </script>
    <script type="text/javascript">

        function auth() {
            window.open('../Master/pgeAuthentication.aspx', '_self');
        }
        function authenticate() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Authenticate data?")) {
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
    <script type="text/javascript">
        function disableClick(elem) {
            elem.disabled = true;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function DeleteConform() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= hdnfdebitdoc.ClientID %>").value;
            var autoid = document.getElementById("<%= hdnfdebitid.ClientID %>").value;
            var Tran_Type = $("#<%=drpSub_Type.ClientID %>").val();
            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';

            var XML = "<ROOT><DebitHead doc_no='" + DocNo + "' dcid='" + autoid + "' Company_Code='" + Company_Code + "' " +
                  "Year_Code='" + Year_Code + "' tran_type='" + Tran_Type + "'></DebitHead></ROOT>";
            var spname = "DebitCredit";
            var status = document.getElementById("<%= btnDelete.ClientID %>").value;
            ProcessXML(XML, status, spname, Tran_Type);
        }
        function Validate() {
            debugger;

            $("#loader").show();

            var StartDate = '<%= Session["Start_Date"] %>';
            var EndDate = '<%= Session["End_Date"] %>';

            var d = $("#<%=txtDoc_Date.ClientID %>").val();
            var DocDates = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

            StartDate = StartDate.slice(6, 11) + "/" + StartDate.slice(3, 5) + "/" + StartDate.slice(0, 2);
            EndDate = EndDate.slice(6, 11) + "/" + EndDate.slice(3, 5) + "/" + EndDate.slice(0, 2);
            if (DocDates >= StartDate && DocDates <= EndDate) {
            }
            else {
                $("#<%=txtDoc_Date.ClientID %>").focus();
                alert('Not A Valid Date Range')
                $("#loader").hide();
                return false;
            }

            if ($("#<%=txtAc_Code.ClientID %>").val() == "") {
                $("#<%=txtAc_Code.ClientID %>").focus();
                $("#loader").hide();
                return false;

            }
            if ($("#<%=txtDoc_Date.ClientID %>").val() == "") {
                $("#<%=txtDoc_Date.ClientID %>").focus();
                $("#loader").hide();
                return false;

            }
            var AcCode = $("#<%=txtAc_Code.ClientID %>").val() == "" ? 0 : $("#<%=txtAc_Code.ClientID%>").val();
            if (AcCode == 0) {
                alert('Please enter correct Ac Code!!');
                $("#loader").hide();
                return false;
            }
            var ShipTo = $("#<%=txtShipTo.ClientID %>").val() == "" ? 0 : $("#<%=txtShipTo.ClientID%>").val();
            if (ShipTo == 0) {
                alert('Please enter correct BillTo Code!!');
                $("#loader").hide();
                return false;
            }
            var Gst_Code = $("#<%=txtGST_Rate_Code.ClientID %>").val() == "" ? 0 : $("#<%=txtGST_Rate_Code.ClientID%>").val();
            if (Gst_Code == 0) {
                alert('Please enter correct GST Rate Code!!');
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtAc_CodeDetails.ClientID %>").val() != "") {
                alert('Please add details');
                $("#<%=txtAc_CodeDetails.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            return true;

        }
        function pagevalidation() {
            debugger;
            $("#loader").show();
            var Docno = 0, Docnoid = 0, DetailId = 0;
            var DOC_NO = $("#<%=txtDoc_No.ClientID %>").val();
            var status = document.getElementById("<%= btnSave.ClientID %>").value;
            if (status == "Update") {
                Docno = document.getElementById("<%= hdnfdebitdoc.ClientID %>").value;
                Docnoid = document.getElementById("<%= hdnfdebitid.ClientID %>").value;
            }
            var spname = "DebitCredit";
            var XML = "<ROOT>";
            var Tran_Type = $("#<%=drpSub_Type.ClientID %>").val() == "" ? 0 : $("#<%=drpSub_Type.ClientID %>").val();
            var d = $("#<%=txtDoc_Date.ClientID %>").val();
            var Entry_date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

            var AcCode = $("#<%=txtAc_Code.ClientID %>").val() == "" ? 0 : $("#<%=txtAc_Code.ClientID%>").val();

            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';
            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var USER = '<%= Session["user"] %>';
            var Bill_No = $("#<%=txtBillNo.ClientID %>").val() == "" ? 0 : $("#<%=txtBillNo.ClientID%>").val();
            var d = $("#<%=txtRef_Date.ClientID %>").val();
            var Bill_date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';
            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var USER = '<%= Session["user"] %>';
            var Bill_Id = $("#<%=lblBillid.ClientID %>").text() == "" ? 0 : $("#<%=lblBillid.ClientID%>").text();
            // var Bill_Id = lblBillid.Text != string.Empty ? Convert.ToInt32(lblBillid.Text) : 0;
            var Gst_Code = $("#<%=txtGST_Rate_Code.ClientID %>").val() == "" ? 0 : $("#<%=txtGST_Rate_Code.ClientID%>").val();

            var TaxableAmount = $("#<%=txtTaxable_Amount %>").val() == "" ? 0 : $("#<%=txtTaxable_Amount.ClientID%>").val();

            var CgstRate = $("#<%=txtCGST_Rate.ClientID %>").val() == "" ? 0 : $("#<%=txtCGST_Rate.ClientID%>").val();
            var CgstAmount = $("#<%=txtCGST_Amount.ClientID%>").val() == "" ? 0 : $("#<%=txtCGST_Amount.ClientID%>").val();
            var SgstRate = $("#<%=txtSGST_Rate.ClientID%>").val() == "" ? 0 : $("#<%=txtSGST_Rate.ClientID%>").val();
            var SgstAmount = $("#<%=txtSGST_Amount.ClientID%>").val() == "" ? 0 : $("#<%=txtSGST_Amount.ClientID%>").val();
            var IgstRate = $("#<%=txtIGST_Rate.ClientID%>").val() == "" ? 0 : $("#<%=txtIGST_Rate.ClientID%>").val();
            var IgstAmount = $("#<%=txtIGST_Amount.ClientID%>").val() == "" ? 0 : $("#<%=txtIGST_Amount.ClientID%>").val();
            var MiscAmount = $("#<%=txtMISC.ClientID%>").val() == "" ? 0 : $("#<%=txtMISC.ClientID%>").val();
            var ASNNo = $("#<%=txtAsnNo.ClientID%>").val() == "" ? 0 : $("#<%=txtAsnNo.ClientID%>").val();
            var ewaybillno = $("#<%=txtEwayBillno.ClientID%>").val() == "" ? 0 : $("#<%=txtEwayBillno.ClientID%>").val();

            var FinalAmount = $("#<%=txtfinalAmount.ClientID%>").val() == "" ? 0 : $("#<%=txtfinalAmount.ClientID%>").val();
            var ac = document.getElementById("<%= hdnfAccode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfAccode.ClientID %>").value;
            var NARRATION = $("#<%=txtnarration.ClientID %>").val() == "" ? "" : $("#<%=txtnarration.ClientID%>").val();
            var billtype = document.getElementById("<%= hdnfBilltype.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfBilltype.ClientID %>").value;
            var billno = document.getElementById("<%= hdnfbillno.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbillno.ClientID %>").value;
            var salebillyearcode = document.getElementById("<%= hdnfsalebillyearcde.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfsalebillyearcde.ClientID %>").value;

            var shipto = $("#<%=txtShipTo.ClientID %>").val() == "" ? 0 : $("#<%=txtShipTo.ClientID%>").val();
            var st = document.getElementById("<%= hdnfShipTo.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfShipTo.ClientID %>").value;
            if (st == "" || st == "&nbsp;") {
                st = 0;
            }
            var Millcode = $("#<%=txtMill_Code.ClientID %>").val() == "" ? 0 : $("#<%=txtMill_Code.ClientID%>").val();
            var mc = document.getElementById("<%= hdnfMill.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfMill.ClientID %>").value;
            if (mc == "" || mc == "&nbsp;") {
                mc = 0;
            }

            var Unitcode = $("#<%=txtUnit.ClientID %>").val() == "" ? 0 : $("#<%=txtUnit.ClientID%>").val();
            var uc = document.getElementById("<%= hdnfUnit.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfUnit.ClientID %>").value;
            if (uc == "" || uc == "&nbsp;") {
                uc = 0;
            }

            var ackno = $("#<%=txtackno.ClientID%>").val() == "" ? 0 : $("#<%=txtackno.ClientID%>").val();
            var drcr = 0;
            if (ac == "" || ac == "&nbsp;") {
                ac = 0;
            }

            var TCS_Rate = $("#<%=txtTCSRate.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSRate.ClientID %>").val();
            var TCS_Amt = $("#<%=txtTCSAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSAmt.ClientID %>").val();
            var TCS_Net_Payable = $("#<%=txtTCSNet_Payable.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSNet_Payable.ClientID %>").val();

            var TDS_Rate = $("#<%=txtTDS.ClientID %>").val() == "" ? 0 : $("#<%=txtTDS.ClientID %>").val();
            var TDS_Amt = $("#<%=txtTDSAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtTDSAmt.ClientID %>").val();

            var HeadInsertUpdate = ""; Detail_Insert = ""; Detail_Update = ""; Detail_Delete = "";
            if (status == "Save") {
                HeadInsertUpdate = "tran_type='" + Tran_Type + "'";
            }
            else {
                HeadInsertUpdate = "tran_type='" + Tran_Type + "' doc_no='" + Docno + "'";
            }

            XML = XML + "<DebitHead " + HeadInsertUpdate + " doc_date='" + Entry_date + "' ac_code='" + AcCode + "' bill_no='" + Bill_No + "' bill_date='" + Bill_date + "' " +
                "bill_id='" + billno + "' bill_type='" + billtype + "' texable_amount='" + TaxableAmount + "' gst_code='" + Gst_Code + "' cgst_rate='" + CgstRate + "' cgst_amount='" + CgstAmount + "' sgst_rate='" + SgstRate + "' sgst_amount='" + SgstAmount + "' igst_rate='" + IgstRate + "' "
                + "igst_amount='" + IgstAmount + "' bill_amount='" + FinalAmount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' Created_By='" + USER + "' " +
                "Modified_By='" + USER + "' misc_amount='" + MiscAmount + "' ac='" + ac + "' ASNNO='" + ASNNo + "' dcid='" + Docnoid + "' Ewaybillno='" + ewaybillno + "' Narration='" + NARRATION + "' Shit_To='" + shipto + "' " +
                "Mill_Code='" + Millcode + "' st='" + st + "' mc='" + mc + "' ackno='" + ackno + "' Unit_Code='" + Unitcode + "' uc='" + uc + "' TCS_Rate='" + TCS_Rate + "' TCS_Amt='" + TCS_Amt + "' TCS_Net_Payable='" + TCS_Net_Payable + "' TDS_Rate='" + TDS_Rate + "' TDS_Amt='" + TDS_Amt + "' IsDeleted='1'>";

            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            var grdrow = gridView.getElementsByTagName("tr");

            var Detail_Value = ""; concatid = "";
            var Gledger_Insert = ""; Gledger_values = ""; Gledger_Delete = "";
            var ddid = DetailId;
            for (var i = 1; i < grdrow.length; i++) {

                var DetailIdNew = gridView.rows[i].cells[2].innerHTML;
                var ExpAccode = gridView.rows[i].cells[3].innerHTML;
                var value = gridView.rows[i].cells[5].innerHTML;
                var Item_Code = gridView.rows[i].cells[6].innerHTML;
                var Qty = gridView.rows[i].cells[9].innerHTML;
                var id = gridView.rows[i].cells[10].innerHTML;
                var ExAccodeid = gridView.rows[i].cells[13].innerHTML;
                var ic = gridView.rows[i].cells[14].innerHTML;
                if (gridView.rows[i].cells[11].innerHTML == "A") {

                    XML = XML + "<DebitDetailInsert tran_type='" + Tran_Type + "' doc_no='" + Docno + "' dcid='" + Docnoid + "' expac_code='" + ExpAccode + "' value='" + value + "' " +
                     "expac='" + ExAccodeid + "' dcdetailid='" + ddid + "' detail_Id='" + DetailIdNew + "' company_code='" + Company_Code + "' year_code='" + Year_Code + "' Item_Code='" + Item_Code + "' " +
                     "Quantal='" + Qty + "' ic='" + ic + "'/>";
                    ddid = parseInt(ddid) + 1;

                }
                else if (gridView.rows[i].cells[11].innerHTML == "U") {
                    var id = gridView.rows[i].cells[10].innerHTML;
                    XML = XML + "<DebitDetail tran_type='" + Tran_Type + "' doc_no='" + Docno + "' dcid='" + Docnoid + "' expac_code='" + ExpAccode + "' value='" + value + "' " +
                      "expac='" + ExAccodeid + "' dcdetailid='" + id + "' detail_Id='" + DetailIdNew + "' company_code='" + Company_Code + "' year_code='" + Year_Code + "' Item_Code='" + Item_Code + "' " +
                      "Quantal='" + Qty + "' ic='" + ic + "'/>";

                }
                else if (gridView.rows[i].cells[11].innerHTML == "D") {
                    var id = gridView.rows[i].cells[10].innerHTML;
                    XML = XML + "<DebitDetailDelete tran_type='" + Tran_Type + "' doc_no='" + Docno + "' dcid='" + Docnoid + "' expac_code='" + ExpAccode + "' value='" + value + "' " +
                      "expac='" + ExAccodeid + "' dcdetailid='" + id + "' detail_Id='" + DetailIdNew + "' company_code='" + Company_Code + "' year_code='" + Year_Code + "' Item_Code='" + Item_Code + "' " +
                      "Quantal='" + Qty + "' ic='" + ic + "'/>";
                }

            }
            debugger;
            var Gledger_Delete = "delete from nt_1_gledger where TRAN_TYPE='PR' and Doc_No=" + Docno + " and Company_Code=" + Company_Code + " and Year_Code=" + Year_Code + "";

            var Order_Code = 1;
            var drcrM = 0;
            if (Tran_Type == 'DN' || Tran_Type == 'DN' || Tran_Type == 'DS') {
                drcr = "D";
                drcr0 = "C";
                if (MiscAmount > 0) {
                    drcrM = "C";
                }
                else {
                    drcrM = "D";
                }
            }
            else if (Tran_Type == 'CN' || Tran_Type == 'CN' || Tran_Type == 'CS') {
                drcr = "C";
                drcr0 = "D";
                if (MiscAmount > 0) {
                    drcrM = "D";
                }
                else {
                    drcrM = "C";
                }
            }
            // Acc Code Effect
            Order_Code = Order_Code + 1;
            var cgstac = ""; cgstacid = ""; sgstac = ""; sgstacid = ""; igstac = ""; igstacid = "";
            if (Tran_Type == "DS") {
                cgstac = '<%= Session["PurchaseCGSTAc"] %>';
                cgstacid = '<%= Session["PurchaseCGSTid"] %>';
                sgstac = '<%= Session["PurchaseSGSTAc"] %>';
                sgstacid = '<%= Session["PurchaseSGSTid"] %>';
                igstac = '<%= Session["PurchaseIGSTAc"] %>';
                igstacid = '<%= Session["PurchaseIGSTid"] %>';
            }
            else {
                cgstac = '<%= Session["SaleCGSTAc"] %>';
                cgstacid = '<%= Session["SaleCGSTid"] %>';
                sgstac = '<%= Session["SaleSGSTAc"] %>';
                sgstacid = '<%= Session["SaleSGSTid"] %>';
                igstac = '<%= Session["SaleIGSTAc"] %>';
                igstacid = '<%= Session["SaleIGSTid"] %>';
            }

            var AcShort_Name = document.getElementById("<%=lblAc_code.ClientID %>").innerText;
            var TCSNarration = 'TCS' + AcShort_Name + " " + Docno;
            var TDSNarration = 'TDS' + AcShort_Name + " " + Docno;

            XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + AcCode + "' " +
                                                        "UNIT_code='0' NARRATION='" + NARRATION + "' AMOUNT='" + FinalAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr + "' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + ac + "' vc='0' progid='0' tranid='0'/>";


            for (var i = 1; i < grdrow.length; i++) {
                var ExpAccode = gridView.rows[i].cells[3].innerHTML;
                var value = gridView.rows[i].cells[5].innerHTML;
                var ExAccodeid = gridView.rows[i].cells[13].innerHTML;
                Order_Code = Order_Code + 1;

                if (gridView.rows[i].cells[7].innerHTML != "D") {
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + ExpAccode + "' " +
                                                           "UNIT_code='0' NARRATION='" + NARRATION + "' AMOUNT='" + value + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr0 + "' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + ExAccodeid + "' vc='0' progid='0' tranid='0'/>";
                }
            }

            if (CgstAmount > 0) {
                Order_Code = Order_Code + 1;

                XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + cgstac + "' " +
                                                       "UNIT_code='0' NARRATION='" + NARRATION + "' AMOUNT='" + CgstAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                       "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr0 + "' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                       "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + cgstacid + "' vc='0' progid='0' tranid='0'/>";
            }


            if (SgstAmount > 0) {

                Order_Code = Order_Code + 1;

                XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + sgstac + "' " +
                                                      "UNIT_code='0' NARRATION='" + NARRATION + "' AMOUNT='" + CgstAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr0 + "' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + sgstacid + "' vc='0' progid='0' tranid='0'/>";

            }
            if (IgstAmount > 0) {
                Order_Code = Order_Code + 1;

                XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + igstac + "' " +
                                                      "UNIT_code='0' NARRATION='" + NARRATION + "' AMOUNT='" + IgstAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr0 + "' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + igstacid + "' vc='0' progid='0' tranid='0'/>";
            }


            if (MiscAmount != 0) {
                Order_Code = Order_Code + 1;
              
                XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + '<%=Session["POSTAGE_AC"] %>' + "' " +
                                                      "UNIT_code='0' NARRATION='" + NARRATION + "' AMOUNT='" + Math.abs(MiscAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcrM + "' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + '<%=Session["POSTAGE_ACid"] %>' + "' vc='0' progid='0' tranid='0'/>";
            }



            if (TCS_Amt > 0) {
                if (Tran_Type == "DN") {
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                                        "UNIT_code='0' NARRATION='" + TCSNarration + NARRATION + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + '<%=Session["SaleTCSAcid"] %>' + "' vc='0' progid='0' tranid='0' />";
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + AcCode + "' " +
                                                       "UNIT_code='0' NARRATION='" + TCSNarration + NARRATION + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                       "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                       "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + ac + "' vc='0' progid='0' tranid='0' />";
                }
                if (Tran_Type == "CN") {
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                                         "UNIT_code='0' NARRATION='" + TCSNarration + NARRATION + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                         "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                         "SORT_TYPE='DN' SORT_NO='" + Docno + "' ac='" + '<%=Session["SaleTCSAcid"] %>' + "' vc='0' progid='0' tranid='0' />";
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + AcCode + "' " +
                                                       "UNIT_code='0' NARRATION='" + TCSNarration + NARRATION + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                       "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                       "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + ac + "' vc='0' progid='0' tranid='0' />";
                }
                if (Tran_Type == "CS") {
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + AcCode + "' " +
                                                          "UNIT_code='0' NARRATION='" + TCSNarration + NARRATION + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                          "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + ac + "' vc='0' progid='0' tranid='0'/>";

                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + '<%=Session["PurchaseTCSAc"] %>' + "' " +
                                                      "UNIT_code='0' NARRATION='" + TCSNarration + NARRATION + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + '<%=Session["PurchaseTCSAcid"] %>' + "' vc='0' progid='0' tranid='0'/>";
                }
                if (Tran_Type == "DS") {
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + AcCode + "' " +
                                                          "UNIT_code='0' NARRATION='" + TCSNarration + NARRATION + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                          "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + ac + "' vc='0' progid='0' tranid='0'/>";

                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + '<%=Session["PurchaseTCSAc"] %>' + "' " +
                                                      "UNIT_code='0' NARRATION='" + TCSNarration + NARRATION + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + '<%=Session["PurchaseTCSAcid"] %>' + "' vc='0' progid='0' tranid='0'/>";
                }
            }

            if (TDS_Amt > 0) {
                if (Tran_Type == "DN") {
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + '<%=Session["SaleTDSAc"] %>' + "' " +
                                                        "UNIT_code='0' NARRATION='" + TDSNarration + NARRATION + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + '<%=Session["SaleTDSacid"] %>' + "' vc='0' progid='0' tranid='0' />";
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + AcCode + "' " +
                                                       "UNIT_code='0' NARRATION='" + TDSNarration + NARRATION + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                       "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                       "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + ac + "' vc='0' progid='0' tranid='0' />";
                }
                if (Tran_Type == "CN") {
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + '<%=Session["SaleTDSAc"] %>' + "' " +
                                                         "UNIT_code='0' NARRATION='" + TDSNarration + NARRATION + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                         "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                         "SORT_TYPE='DN' SORT_NO='" + Docno + "' ac='" + '<%=Session["SaleTDSacid"] %>' + "' vc='0' progid='0' tranid='0' />";
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + AcCode + "' " +
                                                       "UNIT_code='0' NARRATION='" + TDSNarration + NARRATION + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                       "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                       "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + ac + "' vc='0' progid='0' tranid='0' />";
                }
                if (Tran_Type == "CS") {
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + AcCode + "' " +
                                                          "UNIT_code='0' NARRATION='" + TDSNarration + NARRATION + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                          "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + ac + "' vc='0' progid='0' tranid='0'/>";

                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + '<%=Session["PurchaseTDSAc"] %>' + "' " +
                                                      "UNIT_code='0' NARRATION='" + TDSNarration + NARRATION + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + '<%=Session["PurchaseTDSacid"] %>' + "' vc='0' progid='0' tranid='0'/>";
                }
                if (Tran_Type == "DS") {
                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + AcCode + "' " +
                                                          "UNIT_code='0' NARRATION='" + TDSNarration + NARRATION + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                          "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + ac + "' vc='0' progid='0' tranid='0'/>";

                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='" + Tran_Type + "' CASHCREDIT='" + Tran_Type + "' DOC_NO='" + Docno + "' DOC_DATE='" + Entry_date + "' AC_CODE='" + '<%=Session["PurchaseTDSAc"] %>' + "' " +
                                                      "UNIT_code='0' NARRATION='" + TDSNarration + NARRATION + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                      "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                      "SORT_TYPE='" + Tran_Type + "' SORT_NO='" + Docno + "' ac='" + '<%=Session["PurchaseTDSacid"] %>' + "' vc='0' progid='0' tranid='0'/>";
                }
            }


            if (Tran_Type == 'CN') {

                XML = XML + "<MultipleDetail Tran_Type='" + Tran_Type + "' DOC_NO='" + Docno + "' detail_Id='1' " +
                    "Bill_No='" + Bill_Id + "' Bill_Tran_Type='" + billtype + "' Party_Code='" + AcCode + "' pc='" + ac +
                                   "' Bill_Receipt='0' Value='" + FinalAmount + "' Adj_Value='0' Narration='' " +
                    "Bill_Year_Code='" + salebillyearcode + "' Bill_Auto_Id='" + Bill_No + "' Year_Code='" + Year_Code +
                    "' Doc_Date='" + Entry_date + "' OnAc='0.00' AcadjAmt='0' AcAdjAc='0'/>";

            }
            XML = XML + "</DebitHead></ROOT>";
            ProcessXML(XML, status, spname, Tran_Type)


        }
        function ProcessXML(XML, status, spname, Tran_Type) {
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

            function OnSuccess(response) {
                debugger;
                $("#loader").hide();
                if (status != "Delete") {
                    if (response.d.length > 0) {
                        var action = 1;
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
                        window.open('../Transaction/pgeDebitCreditNote.aspx?dcid=' + id + '&Action=' + action + '&tran_type=' + Tran_Type, "_self");


                    }
                }
                else {
                    var num = parseInt(response.d);

                    if (isNaN(num)) {
                        alert(response.d)

                    }
                    else {
                        window.open('../Transaction/PgeDebitCreditNoteUtility.aspx', "_self");
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
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Debit Credit Note " Font-Names="verdana"
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
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfTran_type" runat="server" />
            <asp:HiddenField ID="hdnfAccode" runat="server" />
            <asp:HiddenField ID="hdnfdebitdoc" runat="server" />
            <asp:HiddenField ID="hdnfdebitid" runat="server" />
            <asp:HiddenField ID="hdnfbillid" runat="server" />
            <asp:HiddenField ID="hdnfBilltype" runat="server" />
            <asp:HiddenField ID="hdnfbillno" runat="server" />
            <asp:HiddenField ID="hdnfsalebillyearcde" runat="server" />
            <asp:HiddenField ID="hdnfShipTo" runat="server" />
            <asp:HiddenField ID="hdnfMill" runat="server" />
            <asp:HiddenField ID="hdnfUnit" runat="server" />
            <asp:HiddenField ID="hdnfqty" runat="server" />

            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" TabIndex="1" />
                        &nbsp;
                        <%--<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" />
                        &nbsp;--%>
                        <asp:Button OnClientClick="if (!Validate()) return false;" OnClick="btnSave_Click" runat="server"
                            Text="Save" UseSubmitBehavior="false" ID="btnSave" CssClass="btnHelp" ValidationGroup="add"
                            Width="90px" Height="24px" TabIndex="25" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" TabIndex="24" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" Height="24px" TabIndex="26" OnClientClick="Back();" />
                        &nbsp;
                        <asp:Button runat="server" ID="btnPrintSaleBill" Text="Print" CssClass="btnHelp"
                            Width="80px" Height="24px" OnClientClick="SB();" />

                        &nbsp;&nbsp;
                             <asp:Button runat="server" ID="btnGenEinvoice" Text="Generate EInvoice" CssClass="btnHelp"
                                 Width="120px" Height="24px" OnClientClick="EInovice();" />
                        &nbsp;
                             <asp:Button runat="server" ID="btnCancleEinvoice" Text="Cancle EInvoice" CssClass="btnHelp"
                                 Width="120px" Height="24px" OnClientClick="ConfirmCancle();" OnClick="btnCancleEInvoice_Click" />
                    </td>

                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 70%;" align="left" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left">Change No:
                        </td>
                        <td align="left">

                            <asp:TextBox Height="24px" ID="txtEditDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEditDoc_No_TextChanged"
                                onKeyDown="changeno(event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>

                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td align="left">Type
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpSub_Type" runat="Server" CssClass="txt" TabIndex="1" Width="230px"
                                AutoPostBack="true" OnSelectedIndexChanged="drpSub_Type_SelectedIndexChanged"
                                Height="30px">
                                <asp:ListItem Text="Debit  Note to Customer" Value="DN"></asp:ListItem>
                                <asp:ListItem Text="Credit Note to Customer" Value="CN"></asp:ListItem>
                                <asp:ListItem Text="Debit Note to Supplier" Value="DS"></asp:ListItem>
                                <asp:ListItem Text="Credit Note to Supplier" Value="CS"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">Entry No
                        </td>
                        <td align="left">
                            <asp:TextBox Height="24px" ID="txtDoc_No" runat="Server" CssClass="txt" TabIndex="2"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDoc_No_TextChanged"
                                onKeyDown="DocNo(event);"></asp:TextBox>
                            <asp:Button Width="70px" Height="24px" ID="btntxtDoc_No" runat="server" Text="..."
                                OnClick="btntxtDoc_No_Click" CssClass="btnHelp" onKeyDown="DocNo(event);" Visible="false" />
                            <asp:Label ID="lblDoc_No" runat="server" CssClass="lblName" Visible="false"></asp:Label>
                        </td>

                    </tr>
                    <tr>

                        <td align="left">Entry Date
                        </td>
                        <td align="left">
                            <asp:TextBox Height="24px" ID="txtDoc_Date" runat="Server" CssClass="txt" TabIndex="3"
                                Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtDoc_Date_TextChanged"
                                onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtDoc_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtDoc_Date"
                                    runat="server" TargetControlID="txtDoc_Date" PopupButtonID="imgcalendertxtDoc_Date"
                                    Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                        </td>
                        <td align="left">Bill From
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAc_Code" runat="Server" CssClass="txt" TabIndex="4" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtAc_Code_TextChanged"
                                Height="24px" MaxLength="18" onKeyDown="AcCode(event);"></asp:TextBox>
                            <asp:Button ID="btntxtAc_Code" runat="server" Text="..." OnClick="btntxtAc_Code_Click"
                                CssClass="btnHelp" Width="20px" />
                            <asp:Label ID="lblAc_code" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblAcId" runat="server" CssClass="lblName"></asp:Label>
                        </td>


                    </tr>
                    <tr>
                        <td align="left">Bill No
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBillNo" runat="Server" CssClass="txt" TabIndex="5" Width="90px" Enabled="false"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtBillNo_TextChanged"
                                Height="24px" MaxLength="18" onKeyDown="Billno(event);"></asp:TextBox>
                            <asp:Button ID="btntxtBillNo" runat="server" Text="..." OnClick="btntxtBillNo_Click"
                                CssClass="btnHelp" Width="20px" />
                            Bill Id
                       
                            <asp:Label ID="lblBillid" runat="server" CssClass="lblName" Font-Names="verdana" Text=""></asp:Label>
                        </td>
                        <td align="left">Bill Date
                        </td>
                        <td align="left">
                            <asp:TextBox Height="24px" ID="txtRef_Date" runat="Server" CssClass="txt" TabIndex="6"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtRef_Date_TextChanged"
                                onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtRef_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtRef_Date"
                                    runat="server" TargetControlID="txtRef_Date" PopupButtonID="imgcalendertxtRef_Date"
                                    Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                        </td>

                    </tr>
                    <tr>
                        <td align="left">Bill To
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtShipTo" runat="Server" CssClass="txt" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtShipTo_TextChanged"
                                Height="24px" MaxLength="18" onKeyDown="ShipTo(event);" TabIndex="7"></asp:TextBox>
                            <asp:Button ID="btntxtShipTo" runat="server" Text="..." OnClick="btntxtShipTo_Click"
                                CssClass="btnHelp" Width="20px" />
                            <asp:Label ID="lblShipTo" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblShipId" runat="server" CssClass="lblName"></asp:Label></td>
                        <td align="left">Mill
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMill_Code" runat="Server" CssClass="txt" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtMill_Code_TextChanged"
                                Height="24px" MaxLength="18" onKeyDown="Mill_Code(event);" TabIndex="8"></asp:TextBox>
                            <asp:Button ID="btntxtMill_Code" runat="server" Text="..." OnClick="btntxtMill_Code_Click"
                                CssClass="btnHelp" Width="20px" />
                            <asp:Label ID="lblMill_Code" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblMillId" runat="server" CssClass="lblName"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="left">Ship To
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUnit" runat="Server" CssClass="txt" TabIndex="9" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtUnit_TextChanged"
                                Height="24px" MaxLength="18" onKeyDown="Unit(event);"></asp:TextBox>
                            <asp:Button ID="btntxtUnit" runat="server" Text="..." OnClick="btntxtUnit_Click"
                                CssClass="btnHelp" Width="20px" />
                            <asp:Label ID="lblUnit" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>



                    <tr>
                        <td align="left">
                            <asp:Button ID="btnOpenDetailsPopup" runat="server" Text="ADD" CssClass="btnHelp"
                                Width="80px" Height="25px" OnClick="btnOpenDetailsPopup_Click" Visible="false" />
                        </td>
                    </tr>
                </table>
                <fieldset style="border-top: 2px dotted rgb(249, 6, 197); border-radius: 3px; width: 90%; margin-left: -131px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                    <legend style="text-align: center;">
                        <h3 style="color: Purple; font-family: verdana; font-weight: bold;">Debit Credit Note Detail</h3>
                </fieldset>
                <table width="100%" align="left">
                    <tr>

                        <td align="left">
                            <asp:Label ID="lblID" runat="server" CssClass="lblName" Font-Names="verdana" Text=""></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblNo" runat="server" CssClass="lblName" Font-Names="verdana" Text="" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Exp Ac Code
                            <asp:TextBox ID="txtAc_CodeDetails" runat="Server" CssClass="txt" TabIndex="10" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtAc_CodeDetails_TextChanged"
                                Height="24px" MaxLength="18" onKeyDown="AcCodeDet(event);"></asp:TextBox>
                            <asp:Button ID="btntxtAc_CodeDetails" runat="server" Text="..." OnClick="btntxtAc_CodeDetails_Click"
                                CssClass="btnHelp" />
                            <asp:Label ID="lblAc_CodeDetails" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblExAcId" runat="server" CssClass="lblName"></asp:Label>
                            Value
                            <asp:TextBox ID="txtvalue" runat="Server" CssClass="txt" TabIndex="11" Width="90px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtvalue_TextChanged"
                                Height="24px" MaxLength="18"></asp:TextBox>
                            Item Code
                             <asp:TextBox ID="txtItem_Code" runat="Server" CssClass="txt" TabIndex="12" Width="90px"
                                 Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtItem_Code_TextChanged"
                                 Height="24px" MaxLength="18" onKeyDown="Item(event);"></asp:TextBox>
                            <asp:Button ID="btntxtItem_Code" runat="server" Text="..." OnClick="btntxtItem_Code_Click"
                                CssClass="btnHelp" />
                            <asp:Label ID="lblItem_Code" runat="server" CssClass="lblName"></asp:Label>
                            HSN
                            <asp:TextBox Height="24px" ID="txtHSN" runat="Server" CssClass="txt" TabIndex="13"
                                Width="100px" Style="text-align: right;" AutoPostBack="true"></asp:TextBox>
                            Qty
                             <asp:TextBox Height="24px" ID="txtQty" runat="Server" CssClass="txt" TabIndex="14"
                                 Width="90px" Style="text-align: right;" AutoPostBack="true"></asp:TextBox>
                            <asp:Button ID="btnAdddetails" runat="server" Text="ADD" Width="80px" Height="25px"
                                OnClick="btnAdddetails_Click" TabIndex="15" CssClass="btnHelp" />
                            <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp" Width="80px"
                                Height="25px" OnClick="btnClosedetails_Click" TabIndex="16" />
                        </td>
                    </tr>
                </table>
                <div style="width: 100%; position: relative;">
                    <asp:UpdatePanel ID="upGrid" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlgrdDetail" runat="server" ScrollBars="Both" Height="200px" Width="1000px"
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
                <asp:Panel ID="Panel1" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                    Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                    <table style="width: 74%;" align="left" cellpadding="4" cellspacing="4">
                        <tr>
                            <td style="width: 100%">
                                <table style="width: 50%;" align="left">
                                    <tr>
                                        <td align="left">GST Rate Code
                                            <asp:TextBox Height="24px" ID="txtGST_Rate_Code" runat="Server" CssClass="txt" TabIndex="17"
                                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtGST_Rate_Code_TextChanged"
                                                onKeyDown="GSTRate(event);"></asp:TextBox>
                                            <asp:Button Width="20px" Height="24px" ID="btntxtGST_Rate_Code" runat="server" Text="..."
                                                OnClick="btntxtGST_Rate_Code_Click" CssClass="btnHelp" />
                                            <asp:Label ID="lblGST_Rate_Code" runat="server" CssClass="lblName"></asp:Label>

                                            <asp:TextBox Height="24px" ID="txtGST" runat="Server" CssClass="txt" TabIndex="18"
                                                Width="90px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtGST_TextChanged"
                                                MaxLength="18" Visible="false"></asp:TextBox>
                                            ASN NO:
                                             <asp:TextBox Height="24px" ID="txtAsnNo" runat="Server" CssClass="txt" TabIndex="19"
                                                 Width="120px" Style="text-align: right;" AutoPostBack="true"
                                                 MaxLength="18" OnTextChanged="txtAsnNo_TextChanged"></asp:TextBox>
                                            <asp:Label ID="lblQty" runat="server" CssClass="lblName"></asp:Label>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Einvoice NO:
                                             <asp:TextBox Height="24px" ID="txtEwayBillno" runat="Server" CssClass="txt" TabIndex="20"
                                                 Width="200px" Style="text-align: right;" AutoPostBack="true"></asp:TextBox>
                                            ACKNo:
                                <asp:TextBox ID="txtackno" runat="Server" CssClass="txt" TabIndex="21" Width="150px"
                                    Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Narration:
                                             <asp:TextBox Height="50px" ID="txtnarration" runat="Server" CssClass="txt" TabIndex="38"
                                                 Width="300px" Style="text-align: left;" AutoPostBack="true" TextMode="MultiLine"
                                                 MaxLength="22" OnTextChanged="txtnarration_TextChanged"></asp:TextBox>

                                        </td>
                                    </tr>

                                </table>
                                <table style="width: 50%;" align="right">
                                    <tr>
                                        <td style="width: 5%;" align="right">Taxable Amount:
                                        </td>
                                        <td style="width: 5%;" align="left">
                                            <asp:TextBox Height="24px" ID="txtTaxable_Amount" runat="Server" CssClass="txt" TabIndex="23"
                                                Width="100px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtTaxable_Amount_TextChanged"
                                                MaxLength="19"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%;" align="right">CGST%
                                        </td>
                                        <td style="width: 5%;" align="left">
                                            <asp:TextBox Height="24px" ID="txtCGST_Rate" runat="Server" CssClass="txt" TabIndex="24"
                                                Width="30px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtCGST_Rate_TextChanged"
                                                MaxLength="18"></asp:TextBox>
                                            <asp:TextBox Height="24px" ID="txtCGST_Amount" runat="Server" CssClass="txt" TabIndex="25"
                                                Width="65px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtCGST_Amount_TextChanged"
                                                MaxLength="18"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%;" align="right">SGST%
                                        </td>
                                        <td style="width: 5%;" align="left">
                                            <asp:TextBox Height="24px" ID="txtSGST_Rate" runat="Server" CssClass="txt" TabIndex="26"
                                                Width="30px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtSGST_Rate_TextChanged"
                                                MaxLength="18"></asp:TextBox>
                                            <asp:TextBox Height="24px" ID="txtSGST_Amount" runat="Server" CssClass="txt" TabIndex="27"
                                                Width="65px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtSGST_Amount_TextChanged"
                                                MaxLength="18"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%;" align="right">IGST%
                                        </td>
                                        <td style="width: 5%;" align="left">
                                            <asp:TextBox Height="24px" ID="txtIGST_Rate" runat="Server" CssClass="txt" TabIndex="28"
                                                Width="30px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtIGST_Rate_TextChanged"
                                                MaxLength="18"></asp:TextBox>
                                            <asp:TextBox Height="24px" ID="txtIGST_Amount" runat="Server" CssClass="txt" TabIndex="29"
                                                Width="65px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtIGST_Amount_TextChanged"
                                                MaxLength="18"></asp:TextBox>

                                        </td>
                                        <tr>
                                            <td style="width: 5%;" align="right">MISC%
                                            </td>
                                            <td style="width: 5%;" align="left">
                                                <asp:TextBox Height="24px" ID="txtMISC" runat="Server" CssClass="txt" TabIndex="30"
                                                    Width="80px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtMISC_TextChanged"
                                                    MaxLength="18"></asp:TextBox>



                                            </td>
                                        </tr>

                                        <tr>
                                            <%-- <td style="width: 5%;" align="right">
                                        Gross:
                                    </td>--%>
                                            <td style="width: 5%;" align="left">
                                                <asp:TextBox Height="24px" ID="txtGross_Value" runat="Server" CssClass="txt" TabIndex="31"
                                                    Width="100px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtGross_Value_TextChanged"
                                                    MaxLength="18" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 5%;" align="right">final Amount
                                            </td>
                                            <td style="width: 5%;" align="left">
                                                <asp:TextBox Height="24px" ID="txtfinalAmount" runat="server" CssClass="txt" TabIndex="32"
                                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtfinalAmount_textchanged"
                                                    MaxLength="18"></asp:TextBox>
                                                <%--<asp:Label ID="lblFinalAmount" runat="server" CssClass="lblName" Font-Names="verdana"
                                                    Text="" Font-Size="Medium" Style="text-align: right;" Width="105px"></asp:Label>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 5%;" align="right">TCS%
                                            </td>
                                            <td style="width: 5%;" align="left">
                                                <asp:TextBox ID="txtTCSRate" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                                    OnTextChanged="txtTCSRate_TextChanged" TabIndex="33" Style="text-align: right;"
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
                                            <td style="width: 5%;" align="right">Net Payable:
                                            </td>
                                            <td style="width: 5%;" align="left">
                                                <asp:TextBox ID="txtTCSNet_Payable" runat="Server" CssClass="txt" ReadOnly="true" TabIndex="34"
                                                    Width="120px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtTCSNet_Payable_TextChanged"
                                                    Height="24px"></asp:TextBox>
                                                <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers,Custom"
                                                    ValidChars="." TargetControlID="txtTCSNet_Payable">
                                                </ajax1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                    <td style="width: 5%;" align="right">
                                        TDS%
                                    </td>
                                    <td style="width: 5%;" align="left">
                                        <asp:TextBox ID="txtTDS" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                            OnTextChanged="txtTDS_TextChanged" TabIndex="53" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTDS">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtTDSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                            TabIndex="54" OnTextChanged="txtTDSAmt_TextChanged" ReadOnly="true" Style="text-align: right;"
                                            Width="72px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtTDSAmt" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel onkeydown="closepopup(event);" ID="pnlPopup" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 1000px; min-height: 700px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
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
                            <asp:TextBox onkeydown="SelectFirstRow(event);" ID="txtSearchText" runat="server"
                                Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button onkeydown="closepopup(event);" ID="btnSearch" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 680">
                                <asp:GridView Font-Bold="true" CssClass="select" AllowPaging="true" PageSize="25"
                                    OnPageIndexChanging="grdPopup_PageIndexChanging" ID="grdPopup" runat="server"
                                    AutoGenerateColumns="true" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound"
                                    Style="table-layout: fixed;">
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
