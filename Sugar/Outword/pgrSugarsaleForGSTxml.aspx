<%@ Page Title="Sugar Sale Xml" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgrSugarsaleForGSTxml.aspx.cs" Inherits="Sugar_pgrSugarsaleForGSTxml" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                if (hdnfClosePopupValue == "txtBill_To") {
                    document.getElementById("<%=txtBill_To.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBill_To.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBill_To.ClientID %>").focus();
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
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <script type="text/javascript">

        function SB(saleid, billto, docnumber, corporatenumber, corporate) {
            window.open('../Report/pgeSaleBill_Print.aspx?doc_no=' + saleid + '&billto=' + billto + '&docnumber=' + docnumber
                + '&corporatenumber=' + corporatenumber + '&corporate=' + corporate);

        }
        function SB1(saleid, billto, docnumber, corporatenumber, corporate) {
            window.open('../Report/pgeSaleBill_Print1.aspx?doc_no=' + saleid + '&billto=' + billto + '&docnumber=' + docnumber
                + '&corporatenumber=' + corporatenumber + '&corporate=' + corporate);

        }
        function DO(Action, DO) {
            window.open('../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx?DO=' + DO + '&Action=' + Action);
        }
        function Back() {
            window.open('../Outword/PgeSaleHeadUtility.aspx', '_self');
        }
        function SBPen(DO) {
            var Action = 1;
            window.open('../Outword/pgrSugarsaleForGSTxml.aspx?saleid=' + DO + '&Action=' + Action, "_self");
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
    <script type="text/javascript">
        function Confirm1() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(" Print=>Ok / preprint=>Cancel ")) {
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
                if (hdnfClosePopupValue == "txtBill_To") {
                    document.getElementById("<%=txtBill_To.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBrand_Code") {
                    document.getElementById("<%=txtBrand_Code.ClientID %>").focus();
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
                if (hdnfClosePopupValue == "txtPURCNO") {
                    document.getElementById("<%=txtPURCNO.ClientID %>").focus();
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
                if (hdnfClosePopupValue == "txtTransportCode") {
                    document.getElementById("<%=txtTransportCode.ClientID %>").focus();
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
                if (hdnfClosePopupValue == "txtBill_To") {
                    document.getElementById("<%=txtBill_To.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LblPartyname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBill_To.ClientID %>").focus();
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
                    document.getElementById("<%=txtPURCNO.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
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
        function PURCNO(e) {
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
        function Bill_To(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtBill_To.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBill_To.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtBill_To.ClientID %>").val(unit);
                __doPostBack("txtBill_To", "TextChanged");

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
        function GSTRate(e) {
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
        function Transport(e) {
            debugger;
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

        }
        function Nqty(e) {
            debugger;
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }
        }
        function SaleOPen(SaleID) {
            var Action = 1;
            window.open('../Outword/pgrSugarsaleForGSTxml.aspx?saleid=' + SaleID + '&Action=' + Action, "_self");
        }
        function EInovice() {
            var dono = document.getElementById('<%=txtDOC_NO.ClientID %>').value;
            var ID = document.getElementById('<%=hdnf.ClientID %>').value;
            window.open('../Utility/pgeEInovice.aspx?dono=' + dono + '&Type=SB&ID=' + ID, "_self");
        }
        function GEway() {
            var dono = document.getElementById('<%=txtDOC_NO.ClientID %>').value;
            var ID = document.getElementById('<%=hdnf.ClientID %>').value;
            window.open('../Utility/pgeEwayBill.aspx?dono=' + dono + '&Type=SB&ID=' + ID, "_self");
        }
    </script>
    <script type="text/javascript" language="javascript">
        function DeleteConform() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= hdnfsaledoc.ClientID %>").value;
            var Autoid = document.getElementById("<%= hdnfsaleid.ClientID %>").value;

            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';

            var XML = "<ROOT><SaleHead doc_no='" + DocNo + "' saleid='" + Autoid + "' Company_Code='" + Company_Code + "' " +
                   "Year_Code='" + Year_Code + "'></SaleHead></ROOT>";
            var spname = "SaleBill";
            var status = document.getElementById("<%= btnDelete.ClientID %>").value;
            ProcessXML(XML, status, spname);
        }
        function Validate() {
            debugger;
            $("#loader").show();
            // Validation
            var Outword_Date = '<%= Session["Outword_Date"] %>';
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
            if (Outword_Date == "") {
                alert('Update Post Date');
                $("#loader").hide();
                return false;
            }
            Outword_Date = Outword_Date.slice(6, 11) + "/" + Outword_Date.slice(3, 5) + "/" + Outword_Date.slice(0, 2);

           // Outword_Date = Outword_Date.slice(6, 11) + Outword_Date.slice(3, 5) + "/" + Outword_Date.slice(0, 2);
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
                $("#loader").hide();
                return false;

            }
            if ($("#<%=txtTransportCode.ClientID %>").val() == "") {
                $("#<%=txtTransportCode.ClientID %>").focus();
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
                alert('Please Enter Sale Details!');
                $("#loader").hide();
                return false;
            }
            var grdrow = gridView.getElementsByTagName("tr");
            var ro = (grdrow.length - 1);
            if (ro == 0) {
                alert('Please Enter Sale Details!');
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
                    alert('Minimum One Sale Details is compulsory!');
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
                var Doc_No = 0, saleid = 0, saledetailid = 0, GId = 0;
                var XML = "<ROOT>";
                var spname = "SaleBill";
                var Tran_Type = "SB";
                var status = document.getElementById("<%= btnSave.ClientID %>").value;
                if (status == "Update") {
                    Doc_No = document.getElementById("<%= hdnfsaledoc.ClientID %>").value;
                    saleid = document.getElementById("<%= hdnfsaleid.ClientID %>").value;
                }
                var PURCno = $("#<%=txtPURCNO.ClientID %>").val() == "" ? 0 : $("#<%=txtPURCNO.ClientID%>").val();
                var DONO = $("#<%=lblDONo.ClientID %>").text() == "" ? 0 : $("#<%=lblDONo.ClientID%>").text();
                var d = $("#<%=txtDOC_DATE.ClientID %>").val();
                var doc_date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                var Ac_Code = $("#<%=txtAC_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtAC_CODE.ClientID%>").val();
                var Bill_To = $("#<%=txtBill_To.ClientID %>").val() == "" ? 0 : $("#<%=txtBill_To.ClientID%>").val();

                var Unit_Code = $("#<%=txtUnit_Code.ClientID %>").val() == "" ? 0 : $("#<%=txtUnit_Code.ClientID%>").val();
                var mill_code = $("#<%=txtMILL_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtMILL_CODE.ClientID%>").val();
                var TRANSPORT_CODE = $("#<%=txtTransportCode.ClientID %>").val() == "" ? 0 : $("#<%=txtTransportCode.ClientID%>").val();
                var FROM_STATION = $("#<%=txtFROM_STATION.ClientID  %>").val();
                var TO_STATION = $("#<%=txtTO_STATION.ClientID %>").val();
                var LORRYNO = $("#<%=txtLORRYNO.ClientID %>").val();
                var BROKER = $("#<%=txtBROKER.ClientID %>").val() == "" ? 0 : $("#<%=txtBROKER.ClientID%>").val();
                var wearhouse = $("#<%=txtWEARHOUSE.ClientID %>").val();
                var subTotal = $("#<%=txtSUBTOTAL.ClientID %>").val() == "" ? 0 : $("#<%=txtSUBTOTAL.ClientID%>").val();
                var TAXABLEAMOUNT = $("#<%=txtTaxableAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtTaxableAmount.ClientID%>").val();
                var LESS_FRT_RATE = $("#<%=txtLESS_FRT_RATE.ClientID %>").val() == "" ? 0 : $("#<%=txtLESS_FRT_RATE.ClientID%>").val();
                var freight = $("#<%=txtFREIGHT.ClientID %>").val() == "" ? 0 : $("#<%=txtFREIGHT.ClientID%>").val();
                var cash_advance = $("#<%=txtCASH_ADVANCE.ClientID %>").val() == "" ? 0 : $("#<%=txtCASH_ADVANCE.ClientID%>").val();
                var bank_commission = $("#<%=txtBANK_COMMISSION.ClientID %>").val() == "" ? 0 : $("#<%=txtBANK_COMMISSION.ClientID%>").val();
                var OTHER_AMT = $("#<%=txtOTHER_AMT.ClientID %>").val() == "" ? 0 : $("#<%=txtOTHER_AMT.ClientID%>").val();
                var Bill_Amount = $("#<%=txtBILL_AMOUNT.ClientID %>").val() == "" ? 0 : $("#<%=txtBILL_AMOUNT.ClientID%>").val();
                var Due_Days = $("#<%=txtDUE_DAYS.ClientID %>").val() == "" ? 0 : $("#<%=txtDUE_DAYS.ClientID%>").val();

                var NETQNTL = $("#<%=txtNETQNTL.ClientID %>").val() == "" ? 0 : $("#<%=txtNETQNTL.ClientID%>").val();
                var RateDiff = $("#<%=txtBankCommRate.ClientID %>").val() == "" ? 0 : $("#<%=txtBankCommRate.ClientID%>").val();
                var ASN_No = $("#<%=txtASNGRNNo.ClientID%>").val();
                var CGSTRate = $("#<%=txtCGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtCGSTRate.ClientID%>").val();
                var CGSTAmount = $("#<%=txtCGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtCGSTAmount.ClientID%>").val();
                var IGSTRate = $("#<%=txtIGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtIGSTRate.ClientID%>").val();
                var IGSTAmount = $("#<%=txtIGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtIGSTAmount.ClientID%>").val();
                var SGSTRate = $("#<%=txtSGSTRate.ClientID %>").val() == "" ? 0 : $("#<%=txtSGSTRate.ClientID%>").val();
                var SGSTAmount = $("#<%=txtSGSTAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtSGSTAmount.ClientID%>").val();
                var GstRateCode = $("#<%=txtGSTRateCode.ClientID %>").val() == "" ? 0 : $("#<%=txtGSTRateCode.ClientID%>").val();
                var EwayBill_No = $("#<%=txtEway_Bill_No.ClientID %>").val() == "&nbsp" ? "" : $("#<%=txtEway_Bill_No.ClientID %>").val();
                var EWay_BillChk = $("#<%=chkEWayBill.ClientID %>").is(":checked");
                if (EWay_BillChk == true) {
                    EWay_BillChk = "Y";
                }
                else {
                    EWay_BillChk = "N";
                }
                var MillInvoiceno = $("#<%=txtMillInvoiceno.ClientID %>").val();
                var Roundoff = $("#<%=txtRoundOff.ClientID %>").val() == "" ? 0 : $("#<%=txtRoundOff.ClientID%>").val();
                var Company_Code = '<%= Session["Company_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';
                var Branch_Code = '<%= Session["Branch_Code"] %>';
                var USER = '<%= Session["user"] %>';

                var ac = document.getElementById("<%= hdnfac.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfac.ClientID %>").value;
                var uc = document.getElementById("<%= hdnfuc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfuc.ClientID %>").value;
                var bk = document.getElementById("<%= hdnfbk.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbk.ClientID %>").value;
                var Purcid = document.getElementById("<%= hdnfpurcid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpurcid.ClientID %>").value;
                var tc = document.getElementById("<%= hdnftc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnftc.ClientID %>").value;
                var SaleAc = document.getElementById("<%= hdnfSaleAc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfSaleAc.ClientID %>").value;
                var SaleAcid = document.getElementById("<%= hdnfSaleAcid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfSaleAcid.ClientID %>").value;
                var mc = document.getElementById("<%= hdnfmc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfmc.ClientID %>").value;
                var bt = document.getElementById("<%= hdnfbt.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbt.ClientID %>").value;

                var AcShort_Name = document.getElementById("<%= hdnfAcShort.ClientID %>").value == "&nbsp;" ? "" : document.getElementById("<%= hdnfAcShort.ClientID %>").value;
                var UnitShort_Name = document.getElementById("<%= hdnfUnitshort.ClientID %>").value == "&nbsp;" ? "" : document.getElementById("<%= hdnfUnitshort.ClientID %>").value;
                var TransportShort_Name = document.getElementById("<%= hdnnfTransportshort.ClientID %>").value == "&nbsp;" ? "" : document.getElementById("<%= hdnnfTransportshort.ClientID %>").value;
                var MillShort_Name = document.getElementById("<%= hdnfMillShort.ClientID %>").value == "&nbsp;" ? "" : document.getElementById("<%= hdnfMillShort.ClientID %>").value;
                var TCS_Rate = $("#<%=txtTCSRate.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSRate.ClientID %>").val();
                var TCS_Amt = $("#<%=txtTCSAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSAmt.ClientID %>").val();
                var TCS_Net_Payable = $("#<%=txtTCSNet_Payable.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSNet_Payable.ClientID %>").val();
                var d = $("#<%=txtEwayBill_ValidDate.ClientID %>").val();
                var EwayBill_ValidDate = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

                var newsbno = parseInt($("#<%=txtnewsbno.ClientID %>").val() == "" ? 0 : $("#<%=txtnewsbno.ClientID %>").val());
                var d = $("#<%=txtnewsbdate.ClientID %>").val();
                var newsbdate = "";

                var TDS_Rate = $("#<%=txtTDS.ClientID %>").val() == "" ? 0 : $("#<%=txtTDS.ClientID %>").val();
                var TDS_Amt = $("#<%=txtTDSAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtTDSAmt.ClientID %>").val();
                var SBNARRATION = $("#<%=txtsbnarration.ClientID %>").val();
                var Insured = $("#<%=drpInsured.ClientID %>").val();
                if (d != "") {
                    newsbdate = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                }
                var einvoiceno = $("#<%=txteinvoiceno.ClientID %>").val();
                if (einvoiceno == "&nbsp;") {
                    einvoiceno = "";
                }
                var ackno = $("#<%=txtackno.ClientID %>").val();
                if (ackno == "&nbsp;") {
                    ackno = "";
                }

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
                if (tc == "" || tc == "&nbsp;") {
                    tc = 0;
                }
                if (Purcid == "" || Purcid == "&nbsp;") {
                    Purcid = 0;
                }
                if (SaleAc == "" || SaleAc == "&nbsp;") {
                    SaleAc = 0;
                }
                if (SaleAcid == "" || SaleAcid == "&nbsp;") {
                    SaleAcid = 0;
                }

                if (bt == "" || bt == "&nbsp;") {
                    bt = 0;
                }

                var HeadInsertUpdate = ""; Detail_Insert = ""; Detail_Update = ""; Detail_Delete = "";
                var Detail_Value = ""; concatid = "";
                var Gledger_Insert = ""; Gledger_values = ""; Gledger_Delete = "";

                debugger;
                var DOCNO = "";
                if (status == "Save") {
                    HeadInsertUpdate = "Created_By='" + USER + "' Modified_By=''";
                    DOCNO = "PURCNO='" + PURCno + "'";
                }
                else {
                    HeadInsertUpdate = "Modified_By='" + USER + "' Created_By=''";
                    DOCNO = "doc_no='" + Doc_No + "' PURCNO='" + PURCno + "'";
                }
                XML = XML + "<SaleHead " + DOCNO + " doc_date='" + doc_date + "' Ac_Code ='" + Ac_Code + "' Unit_Code='" + Unit_Code + "' mill_code='" + mill_code + "' " +
                   "FROM_STATION='" + FROM_STATION + "' TO_STATION='" + TO_STATION + "' LORRYNO='" + LORRYNO + "' " +
                    "BROKER='" + BROKER + "' wearhouse='" + wearhouse + "' subTotal='" + subTotal + "' LESS_FRT_RATE='" + LESS_FRT_RATE + "' " +
                   "freight='" + freight + "' cash_advance='" + cash_advance + "' bank_commission='" + bank_commission + "' OTHER_AMT='" + OTHER_AMT + "' " +
                    "Bill_Amount='" + Bill_Amount + "' Due_Days='" + Due_Days + "' NETQNTL='" + NETQNTL + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' " +
                    "Branch_Code='" + Branch_Code + "' " + HeadInsertUpdate + " Tran_Type='SB' DO_No='" + DONO + "' " +
                   "Transport_Code='" + TRANSPORT_CODE + "' RateDiff='" + RateDiff + "' ASN_No='" + ASN_No + "' GstRateCode='" + GstRateCode + "' " +
                   "CGSTRate='" + CGSTRate + "' CGSTAmount='" + CGSTAmount + "' SGSTRate='" + SGSTRate + "' SGSTAmount='" + SGSTAmount + "' IGSTRate='" + IGSTRate + "' " +
                   "IGSTAmount='" + IGSTAmount + "' TaxableAmount='" + TAXABLEAMOUNT + "' EWay_Bill_No='" + EwayBill_No + "' EWayBill_Chk='" + EWay_BillChk + "' " +
                    "MillInvoiceNo='" + MillInvoiceno + "' " +
                   "RoundOff='" + Roundoff + "' saleid='" + saleid + "' ac='" + ac + "' uc='" + uc + "' mc='" + mc + "' " +
                   "bk='" + bk + "' tc='" + tc + "' Purcid='" + Purcid + "' DoNarrtion='' TCS_Rate='" + TCS_Rate + "' TCS_Amt='" + TCS_Amt +
                   "' TCS_Net_Payable='" + TCS_Net_Payable + "' newsbno='" + newsbno + "' newsbdate='" + newsbdate + "' einvoiceno='"
                   + einvoiceno + "' ackno='" + ackno + "' Bill_To='" + Bill_To + "' bt='" + bt + "' EwayBillValidDate='" + EwayBill_ValidDate +
                   "' IsDeleted='1' TDS_Rate='" + TDS_Rate + "' TDS_Amt='" + TDS_Amt + "' SBNarration='" + SBNARRATION + "' Insured='" + Insured + "'>";
                var gridView = document.getElementById("<%=grdDetail.ClientID %>");
                var grdrow = gridView.getElementsByTagName("tr");

                var ddid = saledetailid;
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

                        XML = XML + "<SaleDetailInsert doc_no='" + Doc_No + "' detail_id='" + ID + "' Tran_Type='SB' item_code='" + item_code + "' narration='" + Narration + "' Quantal='" + Quntal + "' packing='" + Packing + "' bags='" + Bags + "' " +
                        "rate='" + rate + "' item_Amount='" + item_amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
                        "Created_By='" + USER + "' Modified_By='" + USER + "' saledetailid='" + ddid + "' saleid='" + saleid + "' ic='" + ic + "' Brand_Code='" + Brand_Code + "'/>";
                        ddid = parseInt(ddid) + 1;
                    }
                    else if (gridView.rows[i].cells[14].innerHTML == "U") {
                        var SaleID = gridView.rows[i].cells[13].innerHTML;
                        XML = XML + "<SaleDetail doc_no='" + Doc_No + "' detail_id='" + ID + "' Tran_Type='SB' item_code='" + item_code + "' narration='" + Narration + "' Quantal='" + Quntal + "' packing='" + Packing + "' bags='" + Bags + "' " +
                       "rate='" + rate + "' item_Amount='" + item_amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
                       "Created_By='" + USER + "' Modified_By='" + USER + "' saledetailid='" + SaleID + "' saleid='" + SaleID + "' ic='" + ic + "' Brand_Code='" + Brand_Code + "'/>";
                    }
                    else if (gridView.rows[i].cells[14].innerHTML == "D") {
                        var SaleID = gridView.rows[i].cells[13].innerHTML;
                        XML = XML + "<SaleDetailDelete doc_no='" + Doc_No + "' detail_id='" + ID + "' Tran_Type='SB' item_code='" + item_code + "' narration='" + Narration + "' Quantal='" + Quntal + "' packing='" + Packing + "' bags='" + Bags + "' " +
                       "rate='" + rate + "' item_Amount='" + item_amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
                       "Created_By='" + USER + "' Modified_By='" + USER + "' saledetailid='" + SaleID + "' saleid='" + SaleID + "' ic='" + ic + "' Brand_Code='" + Brand_Code + "'/>";
                    }
                }

                // Gledger
                var Creditnara = ""; var Debitnara = "";
                var rates = parseFloat(gridView.rows[1].cells[9].innerHTML);
                var finalsaleamonut = parseFloat(rates) + parseFloat((rates * 5) / 100);

                if (Ac_Code == Unit_Code) {
                    Creditnara = "" + MillShort_Name + "  " + NETQNTL + "  L:" + LORRYNO + "  PB:" + PURCno + "  R:" + finalsaleamonut + "  F:" + LESS_FRT_RATE + "";
                    Debitnara = "" + MillShort_Name + "  " + NETQNTL + "  L:" + LORRYNO + "  PB:" + PURCno + "  R:" + finalsaleamonut + "  F:" + LESS_FRT_RATE + "";
                }
                else {
                    Creditnara = "" + MillShort_Name + "  " + NETQNTL + "  L:" + LORRYNO + "  PB:" + PURCno + "  R:" + finalsaleamonut + "  F:" + LESS_FRT_RATE + " ShipToName::" + UnitShort_Name + "";
                    Debitnara = "" + MillShort_Name + "  " + NETQNTL + "  L:" + LORRYNO + "  PB:" + PURCno + "  R:" + finalsaleamonut + "  F:" + LESS_FRT_RATE + "  ShipToName::" + UnitShort_Name + "";
                }
                var saleaccountnarration = "" + MillShort_Name + ", SB:" + AcShort_Name + ", Qntl:" + NETQNTL +
                       ",L:" + LORRYNO + "";
                var TransportNarration = "" + NETQNTL + "  " + cash_advance + "  " + MillShort_Name + "  " + TransportShort_Name + "  Lorry:" + LORRYNO + "  Party:" + AcShort_Name + "";
                var TCSNarration = 'TCS' + AcShort_Name + " " + Doc_No;
                debugger;
                // Ac Code Effect
                var Order_Code = 1;
                if (Bill_Amount > 0) {

                    // Gledger_values = Gledger_values + "('SB','','" + Doc_No + "','" + doc_date + "','" + Ac_Code + "','0','" + Debitnara + "'," +
                    //   " '" + Bill_Amount + "', null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','D',null,0,'1','SB','" + Doc_No + "'," +
                    //                                  " case when 0='" + ac + "' then null else '" + ac + "' end,'0','6','0','" + GId + "')";

                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                            "UNIT_code='0' NARRATION='" + Debitnara + "' AMOUNT='" + Bill_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                            "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='6' tranid='0' saleid='" + saleid + "'/>";

                    Order_Code = parseInt(Order_Code) + 1;
                    //Gledger_values = Gledger_values + ",('SB','','" + Doc_No + "','" + doc_date + "','" + SaleAc + "','0','" + saleaccountnarration + "'," +
                    //  " '" + TAXABLEAMOUNT + "', null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C',null,0,'1','SB','" + Doc_No + "'," +
                    //                                 "  case when 0='" + SaleAcid + "' then null else '" + SaleAcid + "' end,'0','6','0','" + GId + "')";

                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + SaleAc + "' " +
                                                           "UNIT_code='0' NARRATION='" + saleaccountnarration + "' AMOUNT='" + TAXABLEAMOUNT + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + SaleAcid + "' vc='0' progid='6' tranid='0' saleid='" + saleid + "'/>";
                }
                //end Ac Code
                //CGSTAcc Effect
                if (CGSTAmount > 0) {
                    Order_Code = parseInt(Order_Code) + 1;
                    // Gledger_values = Gledger_values + ",('SB','','" + Doc_No + "','" + doc_date + "','" + '<%=Session["SaleCGSTAc"] %>' + "','0','" + Creditnara + "'," +
                    // " '" + CGSTAmount + "', null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C',null,0,'1','SB','" + Doc_No + "'," +
                    //                                " case when 0='" + '<%=Session["SaleSGSTid"] %>' + "' then null else '" + '<%=Session["SaleSGSTid"] %>' + "' end,'0','6','0','" + GId + "')";

                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["SaleCGSTAc"] %>' + "' " +
                                                           "UNIT_code='0' NARRATION='" + Creditnara + "' AMOUNT='" + CGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleCGSTid"] %>' + "' vc='0' progid='6' tranid='0' saleid='" + saleid + "'/>";
                }
                //end CGSTAcc Effect

                //SGSTAcc Effect
                if (SGSTAmount > 0) {
                    Order_Code = parseInt(Order_Code) + 1;
                    //Gledger_values = Gledger_values + ",('SB','','" + Doc_No + "','" + doc_date + "','" + '<%=Session["SaleSGSTAc"] %>' + "','0','" + Creditnara + "'," +
                    //" '" + SGSTAmount + "', null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C',null,0,'1','SB','" + Doc_No + "'," +
                    //                               " case when 0='" + '<%=Session["SaleSGSTid"] %>' + "' then null else '" + '<%=Session["SaleSGSTid"] %>' + "' end,'0','6','0','" + GId + "')";

                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["SaleSGSTAc"] %>' + "' " +
                                                           "UNIT_code='0' NARRATION='" + Creditnara + "' AMOUNT='" + SGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleSGSTid"] %>' + "' vc='0' progid='6' tranid='0' saleid='" + saleid + "'/>";
                }
                //end SGSTAcc Effect

                //IGSTAcc Effect
                if (IGSTAmount > 0) {
                    Order_Code = parseInt(Order_Code) + 1;
                    // Gledger_values = Gledger_values + ",('SB','','" + Doc_No + "','" + doc_date + "','" + '<%=Session["SaleIGSTAc"] %>' + "','0','" + Creditnara + "'," +
                    // " '" + IGSTAmount + "', null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C',null,0,'1','SB','" + Doc_No + "'," +
                    //                                " case when 0='" + '<%=Session["SaleSGSTid"] %>' + "' then null else '" + '<%=Session["SaleSGSTid"] %>' + "' end,'0','6','0','" + GId + "')";

                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["SaleIGSTAc"] %>' + "' " +
                                                           "UNIT_code='0' NARRATION='" + Creditnara + "' AMOUNT='" + IGSTAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleIGSTid"] %>' + "' vc='0' progid='6' tranid='0' saleid='" + saleid + "'/>";
                }
                //end IGSTAcc Effect

                //Transport Advance
                if (cash_advance > 0) {
                    Order_Code = parseInt(Order_Code) + 1;
                    //Gledger_values = Gledger_values + ",('SB','','" + Doc_No + "','" + doc_date + "','" + TRANSPORT_CODE + "','0','" + TransportNarration + "'," +
                    // " '" + cash_advance + "', null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C',null,0,'1','SB','" + Doc_No + "'," +
                    //                                  " case when 0='" + tc + "' then null else '" + tc + "' end,'0','6','0','" + GId + "')";

                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + TRANSPORT_CODE + "' " +
                                                           "UNIT_code='0' NARRATION='" + TransportNarration + "' AMOUNT='" + cash_advance + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + tc + "' vc='0' progid='6' tranid='0' saleid='" + saleid + "'/>";
                }

                if (Roundoff != 0) {
                    if (Roundoff < 0) {
                        Order_Code = parseInt(Order_Code) + 1;
                        // Gledger_values = Gledger_values + ",('SB','','" + Doc_No + "','" + doc_date + "','" + '<%=Session["RoundOff"] %>' + "','0','" + Debitnara + "'," +
                        // " '" + Roundoff + "', null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','D',null,0,'1','SB','" + Doc_No + "'," +
                        //                                " case when 0='" + '<%=Session["RoundOffid"] %>' + "' then null else '" + '<%=Session["RoundOffid"] %>' + "' end,'0','6','0','" + GId + "')";

                        XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["RoundOff"] %>' + "' " +
                                                              "UNIT_code='0' NARRATION='" + Debitnara + "' AMOUNT='" + Roundoff + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                              "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["RoundOffid"] %>' + "' vc='0' progid='6' tranid='0' saleid='" + saleid + "'/>";
                    }
                    else {
                        Order_Code = parseInt(Order_Code) + 1;
                        //Gledger_values = Gledger_values + ",('SB','','" + Doc_No + "','" + doc_date + "','" + '<%=Session["RoundOff"] %>' + "','0','" + Creditnara + "'," +
                        //" '" + Roundoff + "', null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C',null,0,'1','SB','" + Doc_No + "'," +
                        //                               " case when 0='" + '<%=Session["RoundOffid"] %>' + "' then null else '" + '<%=Session["RoundOffid"] %>' + "' end,'0','6','0','" + GId + "')";

                        XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["RoundOff"] %>' + "' " +
                                                              "UNIT_code='0' NARRATION='" + Creditnara + "' AMOUNT='" + Roundoff + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                              "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["RoundOffid"] %>' + "' vc='0' progid='6' tranid='0' saleid='" + saleid + "'/>";
                    }

                }
                if (TCS_Amt > 0) {
                    Order_Code = parseInt(Order_Code) + 1;
                    //Gledger_values = Gledger_values + ",('SB','','" + Doc_No + "','" + doc_date + "','" + '<%=Session["SaleTCSAc"] %>' + "','0','" + TCSNarration + "'," +
                    //  " '" + TCS_Amt + "', null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','C',null,0,'1','SB','" + Doc_No + "'," +
                    //                                 " case when 0='" + '<%=Session["SaleTCSAcid"] %>' + "' then null else '" + '<%=Session["SaleTCSAcid"] %>' + "' end,'0','6','0','" + GId + "')";

                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                                         "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                         "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                         "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleTCSAcid"] %>' + "' vc='0' progid='6' tranid='0' saleid='" + saleid + "'/>";

                    Order_Code = parseInt(Order_Code) + 1;
                    //Gledger_values = Gledger_values + ",('SB','','" + Doc_No + "','" + doc_date + "','" + Ac_Code + "','0','" + TCSNarration + "'," +
                    //   " '" + TCS_Amt + "', null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','D',null,0,'1','SB','" + Doc_No + "'," +
                    //                                  " case when 0='" + ac + "' then null else '" + ac + "' end,'0','6','0','" + GId + "')";

                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                        "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='6' tranid='0' saleid='" + saleid + "'/>";
                }

                if (TDS_Amt > 0) {
                    Order_Code = parseInt(Order_Code) + 1;
                  
                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                          "UNIT_code='0' NARRATION='" + Creditnara + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                          "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='6' tranid='0' saleid='" + saleid + "'/>";

                    Order_Code = parseInt(Order_Code) + 1;
                    
                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + <%=Session["SaleTDSAc"] %> + "' " +
                                                        "UNIT_code='0' NARRATION='" + Debitnara + "' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + <%=Session["SaleTDSacid"] %> + "' vc='0' progid='6' tranid='0' saleid='" + saleid + "'/>";
                }

                XML = XML + "</SaleHead></ROOT>";
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
                        window.open('../Outword/pgrSugarsaleForGSTxml.aspx?saleid=' + id + '&Action=1', "_self");

                    }
                }
                else {
                    var num = parseInt(response.d);

                    if (isNaN(num)) {
                        alert(response.d)

                    }
                    else {
                        window.open('../Outword/PgeSaleHeadUtility.aspx', "_self");
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
            <asp:Label ID="label1" runat="server" Text="   Sale Bill For GST   " Font-Names="verdana"
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
            <asp:HiddenField ID="hdnfac" runat="server" />
            <asp:HiddenField ID="hdnfbt" runat="server" />
            <asp:HiddenField ID="hdnfmc" runat="server" />
            <asp:HiddenField ID="hdnfuc" runat="server" />
            <asp:HiddenField ID="hdnfbk" runat="server" />
            <asp:HiddenField ID="hdnftc" runat="server" />
            <asp:HiddenField ID="hdnfpurcid" runat="server" />
            <asp:HiddenField ID="hdnfSaleAc" runat="server" />
            <asp:HiddenField ID="hdnfSaleAcid" runat="server" />
            <asp:HiddenField ID="hdnfAcShort" runat="server" />
            <asp:HiddenField ID="hdnfUnitshort" runat="server" />
            <asp:HiddenField ID="hdnfMillShort" runat="server" />
            <asp:HiddenField ID="hdnnfTransportshort" runat="server" />
            <asp:HiddenField ID="hdnfsaledoc" runat="server" />
            <asp:HiddenField ID="hdnfsaleid" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
            <asp:HiddenField ID="hdnfSaleTDS" runat="server" />
            <asp:HiddenField ID="hdnfTCSRate" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table width="90%" align="left">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="save" OnClick="btnAdd_Click" />
                            &nbsp;
                            <asp:Button ID="btnSave" OnClientClick="if (!Validate()) return false;" runat="server"
                                Text="Save" CssClass="btnHelp" Width="90px" Height="24px" ValidationGroup="add"
                                OnClick="btnSave_Click" TabIndex="55" />
                            &nbsp;
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="save" OnClick="btnEdit_Click" />
                            &nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" />&nbsp; &nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                                ValidationGroup="save" Height="24px" OnClientClick="Back();" />
                            <asp:Button runat="server" ID="btnPrintSaleBill" Text="Print" CssClass="btnHelp"
                                OnClick="btnPrintSaleBill_Click" Width="80px" Height="24px" OnClientClick="Confirm1()" />
                            &nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnGenEinvoice" Text="Generate EInvoice" CssClass="btnHelp"
                                Width="120px" Height="24px" OnClientClick="EInovice();" />
                            <asp:Button runat="server" ID="btnGentare_EWayBill" Text="Gentare EWayBill" CssClass="btnHelp"
                                Width="90px" Height="24px" OnClientClick="GEway();" />
                            &nbsp;
                            <asp:Button runat="server" ID="btnCancleEinvoice" Text="Cancle EInvoice" CssClass="btnHelp"
                                Width="120px" Height="24px" OnClientClick="ConfirmCancle();" OnClick="btnCancleEInvoice_Click" />
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
                <table width="90%" align="left" cellspacing="3">
                    <tr>
                        <td align="right">
                            Change No:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="txtxf" FilterType="Numbers" InvalidChars="."
                                TargetControlID="txtEditDoc_No">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Bill No.:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDOC_NO" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtDOC_NO_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtdoc_no" runat="server" Text="..." Width="80px" OnClick="btntxtDOC_NO_Click"
                                CssClass="btnHelp" Height="24px" />
                            <asp:Label ID="lblSale_Id" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red" Visible="false"></asp:Label>
                            Purc No:
                            <asp:TextBox ID="txtPURCNO" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtPURCNO_TextChanged"
                                Height="24px" onKeyDown="PURCNO(event);"></asp:TextBox>
                            <asp:Button ID="btntxtPURCNO" runat="server" Text="..." OnClick="btntxtPURCNO_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            &nbsp;
                            <%--DO:<%--<asp:Label runat="server" ID="lblDONo" ForeColor="Yellow"></asp:Label><asp:LinkButton runat="server" ID="lblDoNo" Text="" Style="color: Black;
                                    text-decoration: none;" ToolTip="Click to Go On Delivery Order" OnClick="lblDoNo_Click"></asp:LinkButton>
                                    <asp:Label ID="lblDo_No" runat="server" CssClass="lblName"></asp:Label>--%>
                            &nbsp;&nbsp;<asp:LinkButton runat="server" ID="lnkDo" Text="DO" ForeColor="Yellow"
                                ToolTip="Click to Go On Delivery Order" OnClick="lnkDo_Click"></asp:LinkButton>
                            &nbsp;<asp:Label ID="lblDONo" runat="server" CssClass="lblName"></asp:Label>&nbsp;&nbsp;&nbsp;
                            Date:
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
                        <td align="right">
                            Bill From:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtAC_CODE" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtAC_CODE_TextChanged"
                                Height="24px" onKeyDown="Ac_Code(event);"></asp:TextBox>
                            <asp:Button ID="btntxtAC_CODE" runat="server" Text="..." OnClick="btntxtAC_CODE_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="LblPartyname" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Bill To:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtBill_To" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtBill_To_TextChanged"
                                Height="24px" onKeyDown="Bill_To(event);"></asp:TextBox>
                            <asp:Button ID="btntxtBill_To" runat="server" Text="..." OnClick="btntxtBill_To_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblBill_To" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            ShipTo:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtUnit_Code" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtUnit_Code_TextChanged"
                                onKeyDown="Unit(event);"></asp:TextBox>
                            <asp:Button ID="btntxtUnitcode" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                Width="20px" OnClick="btntxtUnitcode_Click" />
                            <asp:Label ID="lblUnitName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Mill:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtMILL_CODE" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtMILL_CODE_TextChanged"
                                Height="24px" onKeyDown="Mill(event);"></asp:TextBox>
                            <asp:Button ID="btntxtMILL_CODE" runat="server" Text="..." OnClick="btntxtMILL_CODE_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="LBLMILLNAME" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            From:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFROM_STATION" runat="Server" CssClass="txt" TabIndex="6" Width="200px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtFROM_STATION_TextChanged"
                                Height="24px"></asp:TextBox>
                            To:
                            <asp:TextBox ID="txtTO_STATION" runat="Server" CssClass="txt" TabIndex="7" Width="200px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTO_STATION_TextChanged"
                                Height="24px"></asp:TextBox>
                            Lorry No:
                            <asp:TextBox ID="txtLORRYNO" runat="Server" CssClass="txt" TabIndex="8" Width="200px"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtLORRYNO_TextChanged"
                                Height="24px"></asp:TextBox>
                            Wear House:
                            <asp:TextBox ID="txtWEARHOUSE" runat="Server" CssClass="txt" TabIndex="9" Width="200px"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtWEARHOUSE_TextChanged"
                                Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Broker:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBROKER" runat="Server" CssClass="txt" TabIndex="10" Width="80px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBROKER_TextChanged"
                                Height="24px" onKeyDown="Broker(event);"></asp:TextBox>
                            <asp:Button ID="btntxtBROKER" runat="server" Text="..." OnClick="btntxtBROKER_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="LBLBROKERNAME" runat="server" CssClass="lblName"></asp:Label>
                            GST Rate Code
                            <asp:TextBox ID="txtGSTRateCode" runat="Server" CssClass="txt" TabIndex="11" Width="80px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtGSTRateCode_TextChanged"
                                Height="24px" onKeyDown="GSTRate(event);"></asp:TextBox>
                            <asp:Button ID="btntxtGSTRateCode" runat="server" Text="..." OnClick="btntxtGSTRateCode_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblGSTRateName" runat="server" CssClass="lblName"></asp:Label>
                             Insured
                                <asp:DropDownList ID="drpInsured" runat="server" CssClass="ddl" Width="100px"
                                    Height="26px" TabIndex="3">
                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                        </td>
                    </tr>
                   
                </table>
                <fieldset style="border-top: 3px dotted rgb(131, 127, 130); border-radius: 3px; width: 172%;
                    margin-left: -1160px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
                    border-left: 0px; border-right: 0px; height: 5px;">
                    <legend style="text-align: left;">
                        <h2 style="color: Black; text-align: left;" font-names="verdana" font-size="large">
                            Detail Section</h2>
                    </legend>
                </fieldset>
                <asp:Panel ID="Panel2" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                    Font-Size="Small" Style="margin-top: 0px; z-index: 100;">
                    <table width="100%" align="center" cellspacing="5">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblID" runat="server"></asp:Label>
                                <asp:Label ID="lblNo" runat="server" ForeColor="Azure"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Item:
                                <asp:TextBox ID="txtITEM_CODE" runat="Server" CssClass="txt" TabIndex="13" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtITEM_CODE_TextChanged"
                                    onKeyDown="Item(event);"></asp:TextBox>
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
                                    Height="25px" OnClick="btnAdddetails_Click" TabIndex="20" />
                                <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp" Width="80px"
                                    Height="25px" OnClick="btnClosedetails_Click" TabIndex="21" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table>
                    <tr>
                        <td align="left" style="width: 60%;">
                            <div style="width: 100%; position: relative; margin-top: 0px;">
                                <asp:UpdatePanel ID="upGrid" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlgrdDetail" runat="server" align="left" ScrollBars="Both" Height="150px"
                                            Width="1050px" BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true"
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
                            <div style="margin-top: 160px; margin-left: 70px;">
                                Net Qntl:
                                <asp:TextBox ID="txtNETQNTL" runat="Server" CssClass="txt" ReadOnly="true" Width="120px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtNETQNTL_TextChanged"
                                    TabIndex="22" Height="24px" onKeyDown="Nqty(event);"></asp:TextBox>
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
                                ASN/GRN No:
                                <asp:TextBox runat="server" ID="txtASNGRNNo" CssClass="txt" Width="120px" Height="24px"
                                    TabIndex="24"></asp:TextBox>
                                Transport:
                                <asp:TextBox ID="txtTransportCode" runat="Server" CssClass="txt" TabIndex="25" Width="80px"
                                    Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtTransportCode_TextChanged"
                                    onKeyDown="Transport(event);"></asp:TextBox>
                                <asp:Button ID="btnTransport" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                    Width="20px" OnClick="btnTransport_Click" />
                                <asp:Label ID="lblTransportName" runat="server" CssClass="lblName"></asp:Label>
                                EWay Bill No.:
                                <asp:TextBox runat="server" ID="txtEway_Bill_No" CssClass="txt" Width="200px" Height="24px"
                                    TabIndex="26"></asp:TextBox>
                                <asp:CheckBox runat="server" ID="chkEWayBill" AutoPostBack="true" OnCheckedChanged="chkEWayBill_CheckedChanged" />
                                <asp:Label runat="server" ID="lblchkEWayBill" CssClass="lblName"></asp:Label>
                                <asp:TextBox ID="txtMillInvoiceno" runat="Server" CssClass="txt" Width="200px" Style="text-align: left;"
                                    Height="24px" TabIndex="27"></asp:TextBox>
                                 EWayBill ValidDate : 
                                  <asp:TextBox ID="txtEwayBill_ValidDate" runat="Server" CssClass="txt" Width="80px" Style="text-align: left;"
                                       Height="24px"></asp:TextBox>
                                 <asp:Image ID="imgtxtEwayBill_ValidDate" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                    Height="15px" />
                                <ajax1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEwayBill_ValidDate"
                                    PopupButtonID="imgtxtEwayBill_ValidDate" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                            </div>
                            <div>
                                Party
                                <asp:TextBox runat="server" ID="txtPartyMobno" TabIndex="27" />
                                Transport
                                <asp:TextBox runat="server" ID="txtTransportMobno" TabIndex="28" />
                                Driver
                                <asp:TextBox runat="server" ID="txtDriverMobno" TabIndex="29" />
                                GST No
                                <asp:TextBox runat="server" ID="txtGStno" Text="" TabIndex="30" />
                                Unit
                                <asp:TextBox runat="server" ID="txtunit" Text="" TabIndex="31" />
                                <asp:Button Text="SMS" ID="btnSendSMS" CommandName="sms" CssClass="btnHelp" Height="24px"
                                    Width="80px" runat="server" OnCommand="btnSendSMS_Click" />
                            </div>
                            <div style="margin-left: 35px; margin-top: 20px;">
                                New SB No:
                                <asp:TextBox ID="txtnewsbno" runat="Server" CssClass="txt" TabIndex="32" Width="80px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtnewsbno_TextChanged"
                                    Height="24px"></asp:TextBox>
                                Date:
                                <asp:TextBox ID="txtnewsbdate" runat="Server" CssClass="txt" TabIndex="33" Width="80px"
                                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                    Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtnewsbdate_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Image ID="imgcalSB" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                    Height="15px" />
                                <ajax1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtnewsbdate"
                                    PopupButtonID="imgcalSB" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                                EInvoice No:
                                <asp:TextBox ID="txteinvoiceno" runat="Server" CssClass="txt" TabIndex="34" Width="120px"
                                    Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                                ACKNo:
                                <asp:TextBox ID="txtackno" runat="Server" CssClass="txt" TabIndex="35" Width="150px"
                                    Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>                               
                                SB Narration:
                                  <asp:TextBox TextMode="MultiLine" runat="server" ID="txtsbnarration" CssClass="txt"
                                    TabIndex="36" Width="280px" Height="50px" />
                            </div>
                        </td>
                        <td style="width: 80%;" align="left">
                            <table width="130%" cellspacing="4" cellpadding="3">
                                <tr>
                                    <td align="left" style="width: 35%;">
                                        Subtotal:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSUBTOTAL" runat="Server" CssClass="txt" ReadOnly="true" Width="140px"
                                            Style="text-align: right;" AutoPostBack="True" TabIndex="37" OnTextChanged="txtSUBTOTAL_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtSUBTOTAL" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtSUBTOTAL">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Add Frt. Rs.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLESS_FRT_RATE" runat="Server" CssClass="txt" TabIndex="38" Width="50px"
                                            Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtLESS_FRT_RATE_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtLESS_FRT_RATE" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtLESS_FRT_RATE">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtFREIGHT" runat="Server" AutoPostBack="True" TabIndex="39" CssClass="txt"
                                            Height="24px" OnTextChanged="txtFREIGHT_TextChanged" ReadOnly="true" Style="text-align: right;"
                                            Width="82px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtFREIGHT" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtFREIGHT" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 35%;">
                                        Taxable Amount:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtTaxableAmount" runat="Server" CssClass="txt" TabIndex="40" ReadOnly="false"
                                            Width="140px" Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTaxableAmount">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CGST%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtCGSTRate" runat="Server" CssClass="txt" Width="52px" TabIndex="41"
                                            Style="text-align: right;" Height="24px" AutoPostBack="true" OnTextChanged="txtCGSTRate_TextChanged"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtCGSTRate" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtCGSTRate" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtCGSTAmount" runat="Server" CssClass="txt" Width="80px" ReadOnly="true"
                                            Style="text-align: right;" Height="24px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="">
                                        SGST%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtSGSTRate" runat="Server" CssClass="txt" Width="50px" TabIndex="42"
                                            AutoPostBack="true" OnTextChanged="txtSGSTRate_TextChanged" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtSGSTRate" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtSGSTRate" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtSGSTAmount" runat="Server" CssClass="txt" Width="82px" ReadOnly="true"
                                            Style="text-align: right;" Height="24px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="">
                                        IGST%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtIGSTRate" runat="Server" CssClass="txt" Width="52px" AutoPostBack="true"
                                            OnTextChanged="txtIGSTRate_TextChanged" TabIndex="43" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtIGSTRate" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtIGSTRate" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtIGSTAmount" runat="Server" CssClass="txt" Width="80px" ReadOnly="true"
                                            Style="text-align: right;" Height="24px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Rate diff:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBankCommRate" runat="Server" CssClass="txt" Width="50px" Style="text-align: right;"
                                            AutoPostBack="True" Height="24px" TabIndex="44" OnTextChanged="txtBankCommRate_TextChanged"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtBankCommRate">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtBANK_COMMISSION" runat="Server" CssClass="txt" TabIndex="45"
                                            Width="82px" Style="text-align: right;" AutoPostBack="True" ReadOnly="true" OnTextChanged="txtBANK_COMMISSION_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtBANK_COMMISSION" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtBANK_COMMISSION">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Other +/-:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtOTHER_AMT" runat="Server" AutoPostBack="True" CssClass="txt"
                                            Height="24px" OnTextChanged="txtOTHER_AMT_TextChanged" Style="text-align: right;"
                                            TabIndex="46" Width="140px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtOTHER_AMT" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtOTHER_AMT" ValidChars=".,-">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Cash Advance:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCASH_ADVANCE" runat="Server" CssClass="txt" TabIndex="47" Width="140px"
                                            Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtCASH_ADVANCE_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtCASH_ADVANCE" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtCASH_ADVANCE">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Round Off:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtRoundOff" runat="Server" CssClass="txt" TabIndex="48" Width="140px"
                                            Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtRoundOff_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers,Custom"
                                            ValidChars=".,-" TargetControlID="txtRoundOff">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Bill Amount:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBILL_AMOUNT" runat="Server" CssClass="txt" ReadOnly="true" TabIndex="49"
                                            Width="140px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtBILL_AMOUNT_TextChanged"
                                            Height="24px"></asp:TextBox>
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
                                            OnTextChanged="txtTCSRate_TextChanged" TabIndex="50" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTCSRate">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtTCSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                            TabIndex="51" OnTextChanged="txtTCSAmt_TextChanged" ReadOnly="true" Style="text-align: right;"
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
                                            TabIndex="52" Width="140px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtTCSNet_Payable_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTCSNet_Payable">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="left">
                                        TDS%
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtTDS" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                            OnTextChanged="txtTDS_TextChanged" TabIndex="53" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTDS">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtTDSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                            TabIndex="54" OnTextChanged="txtTDSAmt_TextChanged" ReadOnly="true" Style="text-align: right;"
                                            Width="98px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtTDSAmt" ValidChars=".">
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
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px;
                min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center;
                left: 10%; top: 10%;">
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
