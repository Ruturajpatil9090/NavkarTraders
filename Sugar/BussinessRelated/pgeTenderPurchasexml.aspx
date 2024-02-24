<%@ Page Title="Tender Purchase Xml" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeTenderPurchasexml.aspx.cs" Inherits="Sugar_pgeTenderPurchasexml" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <%--<script type="text/javascript">
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

                if (hdnfClosePopupValue == "GR") {
                    document.getElementById("<%=txtGrade.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "PT") {
                    document.getElementById("<%=txtPaymentTo.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtPaymentTo") {
                    document.getElementById("<%=txtPaymentTo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "TF") {
                    document.getElementById("<%=txtTenderFrom.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtMillCode") {
                    document.getElementById("<%=txtMillCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "DO") {
                    document.getElementById("<%=txtDO.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "VB") {
                    document.getElementById("<%=txtVoucherBy.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "BR") {
                    document.getElementById("<%=txtBroker.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "BU") {
                    document.getElementById("<%=txtBuyer.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "BP") {
                    document.getElementById("<%=txtBuyerParty.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "TN") {
                    document.getElementById("<%=txtTenderNo.ClientID %>").disabled = false;
                    document.getElementById("<%=txtTenderNo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=btnSearch.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "MM") {
                    document.getElementById("<%=txtMillCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "GC") {
                    document.getElementById("<%=txtGstrateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtitem_code") {
                    document.getElementById("<%=txtitem_code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtsubBroker") {
                    document.getElementById("<%=txtsubBroker.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTenderFrom") {
                    document.getElementById("<%=txtTenderFrom.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtVoucherBy") {
                    document.getElementById("<%=txtVoucherBy.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtShipTo") {
                    document.getElementById("<%=txtShipTo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "SubBrker") {
                    document.getElementById("<%=txtsubBroker.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "BPAccount") {
                    document.getElementById("<%=txtBP_Account.ClientID %>").focus();
                  }
                  if (hdnfClosePopupValue == "BPDetail") {
                      document.getElementById("<%=txtBP_Account_Detail.ClientID %>").focus();
                }

                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
    </script>
    <script type="text/javascript">
        function disableClick(elem) {
            debugger;
            elem.disabled = true;
            $("#loader").show();
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




                if (hdnfClosePopupValue == "GR") {
                    document.getElementById("<%=txtGrade.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtGrade.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtPaymentTo") {
                    document.getElementById("<%=txtPaymentTo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblPaymentTo.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtPaymentTo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTenderFrom") {
                    document.getElementById("<%=txtTenderFrom.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblTenderFrom.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtTenderFrom.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "DO") {
                    document.getElementById("<%=txtDO.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblDO.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtDO.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtVoucherBy") {
                    document.getElementById("<%=txtVoucherBy.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblVoucherBy.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtVoucherBy.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "BR") {
                    document.getElementById("<%=txtBroker.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBroker.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBroker.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "BU") {

                    document.getElementById("<%=txtBuyer.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBuyerName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBuyer.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "BP") {

                    document.getElementById("<%=txtBuyerParty.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBuyerPartyName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBuyerParty.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "TN") {
                    document.getElementById("<%=txtTenderNo.ClientID %>").value = "";
                    document.getElementById("<%=txtTenderNo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtTenderNo.ClientID %>").disabled = false;
                    document.getElementById("<%=txtTenderNo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = "";
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                    document.getElementById("<%=btnSearch.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtMillCode") {
                    document.getElementById("<%=txtMillCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblMillName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtMillCode.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "GC") {
                    document.getElementById("<%=txtGstrateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblgstrateCode.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%=txtGstrateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtitem_code") {
                    document.getElementById("<%=txtitem_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblitemname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;

                    document.getElementById("<%=txtitem_code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "SubBrker") {
                    document.getElementById("<%=txtsubBroker.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblsubBroker.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtsubBroker.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtShipTo") {
                    document.getElementById("<%=txtShipTo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                     document.getElementById("<%=lblShiptoname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                     document.getElementById("<%=txtShipTo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "BPAccount") {
                    document.getElementById("<%=txtBP_Account.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                       document.getElementById("<%=lblBP_Account.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                       document.getElementById("<%=txtBP_Account.ClientID %>").focus();
                   }

                   if (hdnfClosePopupValue == "BPDetail") {
                       document.getElementById("<%=txtBP_Account_Detail.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBP_Account_Detail.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBP_Account_Detail.ClientID %>").focus();
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
        function refreshparent(source) {
            if (source == 'R') {
                window.close();
                window.opener.location = "";
                window.opener.location.reload();
            }
        }

    </script>
    <script type="text/javascript">
        function MillCode(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnMillCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtMillCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtMillCode.ClientID %>").val(unit);
                __doPostBack("txtMillCode", "TextChanged");

            }

        }

        function Grade(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnGrade.ClientID %>").click();

            }


        }

        function PaymentTo(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnPaymentTo.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtPaymentTo.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtPaymentTo.ClientID %>").val(unit);
                __doPostBack("txtPaymentTo", "TextChanged");

            }

        }
        function subBroker(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnsubBrker.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtsubBroker.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtsubBroker.ClientID %>").val(unit);
                __doPostBack("txtsubBroker", "TextChanged");

            }

        }
        function tenderDo(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnTenderDO.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtDO.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtDO.ClientID %>").val(unit);
                __doPostBack("txtDO", "TextChanged");

            }

        }
        function tenderfrom(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnTenderFrom.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtTenderFrom.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtTenderFrom.ClientID %>").val(unit);
                __doPostBack("txtTenderFrom", "TextChanged");

            }

        }
        function broker(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnBroker.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBroker.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtBroker.ClientID %>").val(unit);
                __doPostBack("txtBroker", "TextChanged");

            }

        }
        function VoucherBy(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnVoucherBy.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtVoucherBy.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtVoucherBy.ClientID %>").val(unit);
                __doPostBack("txtVoucherBy", "TextChanged");

            }

        }
        function Party(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnBuyer.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBuyer.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtBuyer.ClientID %>").val(unit);
                __doPostBack("txtBuyer", "TextChanged");

            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }
        }
        function Shipto(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtShipTo.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtShipTo.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtShipTo.ClientID %>").val(unit);
                __doPostBack("txtShipTo", "TextChanged");

            }

        }
        function detailBroker(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnBuyerParty.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBuyerParty.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtBuyerParty.ClientID %>").val(unit);
                __doPostBack("txtBuyerParty", "TextChanged");

            }

        }
        function gstRateCode(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnGstrateCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtGstrateCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtGstrateCode.ClientID %>").val(unit);
                __doPostBack("txtGstrateCode", "TextChanged");

            }

        }
        function item(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtitem_code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtitem_code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtitem_code.ClientID %>").val(unit);
                __doPostBack("txtitem_code", "TextChanged");

            }

        }
        function BP_Account(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnBP_Account.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBP_Account.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtBP_Account.ClientID %>").val(unit);
                __doPostBack("txtBP_Account", "TextChanged");

            }

        }
        function BPAccountDetail(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnBP_Account_Detail.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBP_Account_Detail.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtBP_Account_Detail.ClientID %>").val(unit);
                __doPostBack("txtBP_Account_Detail", "TextChanged");

            }

        }
    </script>
    <script type="text/javascript">
        function Back() {

            //alert(td);
            window.open('../BussinessRelated/PgeTenderHeadUtility.aspx', '_self');
        }

        function TenderOPen(TenderID) {
            var Action = 1;
            window.open('../BussinessRelated/pgeTenderPurchasexml.aspx?tenderid=' + TenderID + '&Action=' + Action, "_self");
        }
    </script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function DeleteConform() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= hdnftenderno.ClientID %>").value;
            var Tenderid = document.getElementById("<%= hdnftenderid.ClientID %>").value;
            var voucher = $("#<%=lblVoucherNo.ClientID %>").text();
            var vouchertype = $("#<%=lblVoucherType.ClientID %>").text();

            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';

            var XML = "<ROOT><TenderHead Tender_No='" + DocNo + "' tenderid='" + Tenderid + "' Company_Code='" + Company_Code + "' " +
                "Year_Code='" + Year_Code + "' Voucher_No='" + voucher + "' Voucher_Type='" + vouchertype + "'></TenderHead></ROOT>";
            var spname = "TenderPurchase";
            var status = document.getElementById("<%= btnDelete.ClientID %>").value;
            ProcessXML(XML, status, spname);
        }
        function Validate() {
            $("#loader").show();
            var StartDate = '<%= Session["Start_Date"] %>';
            var EndDate = '<%= Session["End_Date"] %>';

            var d = $("#<%=txtDate.ClientID %>").val();
            var DocDates = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

            StartDate = StartDate.slice(6, 11) + "/" + StartDate.slice(3, 5) + "/" + StartDate.slice(0, 2);
            EndDate = EndDate.slice(6, 11) + "/" + EndDate.slice(3, 5) + "/" + EndDate.slice(0, 2);

            if (DocDates >= StartDate && DocDates <= EndDate) {
            }
            else {
                $("#<%=txtDate.ClientID %>").focus();
                alert('Not A Valid Date Range')
                $("#loader").hide();
                return false;
            }

            if ($("#<%=txtBuyer.ClientID %>").val() != "") {
                alert('Please add details');
                $("#<%=txtBuyer.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }

            if ($("#<%=txtMillCode.ClientID %>").val() == "") {
                $("#<%=txtMillCode.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtGrade.ClientID %>").val() == "") {
                $("#<%=txtGrade.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtQuantal.ClientID %>").val() == "") {
                $("#<%=txtQuantal.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtPacking.ClientID %>").val() == "") {
                $("#<%=txtPacking.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtBags.ClientID %>").val() == "" || $("#<%=txtBags.ClientID %>").val() == "0") {
                $("#<%=txtBags.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtPaymentTo.ClientID %>").val() == "") {
                $("#<%=txtPaymentTo.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            //
            if ($("#<%=txtDO.ClientID %>").val() == "") {
                $("#<%=txtDO.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtVoucherBy.ClientID %>").val() == "") {
                $("#<%=txtVoucherBy.ClientID %>").focus();
                  $("#loader").hide();
                  return false;
              }
              if ($("#<%=txtTenderFrom.ClientID %>").val() == "") {
                $("#<%=txtTenderFrom.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            debugger;
            if ($("#<%=txtCashDiff.ClientID %>").val() != "" && $("#<%=txtCashDiff.ClientID %>").val() != "0" && $("#<%=txtCashDiff.ClientID %>").val() != "0.00") {

                if ($("#<%=txtBP_Account.ClientID %>").val() == "2" || $("#<%=txtBP_Account.ClientID %>").val() == "" || $("#<%=txtBP_Account.ClientID %>").val() == "02") {
                    alert('Please add BP Account');
                    $("#<%=txtBP_Account.ClientID %>").focus();
                      $("#loader").hide();
                      return false;
                  }
               

            }

            var type = $("#<%=drpResale.ClientID %>").val();
            if (type == 'P') {
                if ($("#<%=txtPurcRate.ClientID %>").val() == "") {
                    alert('Enter Purchase Rate');
                    $("#<%=txtPurcRate.ClientID %>").focus();
                    $("#loader").hide();
                    return false;
                }
            }
            return true;
        }
        function pagevalidation() {
            debugger;

            var TenderNo = 0, tenderId = 0, tdetailid = 0, commiDoc_No = 0, commisionid = 0;
            var status = document.getElementById("<%= btnSave.ClientID %>").value;
            if (status == "Update") {
                TenderNo = document.getElementById("<%= hdnftenderno.ClientID %>").value;
                tenderId = document.getElementById("<%= hdnftenderid.ClientID %>").value;
            }
            commiDoc_No = $("#<%=lblVoucherNo.ClientID %>").text() == "" ? 0.00 : $("#<%=lblVoucherNo.ClientID %>").text();
            commisionid = document.getElementById("<%= hdnfcommisionid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfcommisionid.ClientID %>").value;
            if (commisionid == "&nbsp;") {
                commisionid = "0";
            }
            //Validation
            var spname = "TenderPurchase";
            var XML = "<ROOT>";
            var d = $("#<%=txtLiftingDate.ClientID %>").val();
            var Lifting_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
            //var Lifting_Date_Head = ;
            var d = $("#<%=txtDate.ClientID %>").val();
            var Tender_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
            var AUTO_VOUCHER = '<%= Session["AUTO_VOUCHER"] %>';
            var Mill_Code = $("#<%=txtMillCode.ClientID %>").val();
            var Sell_Note_No = $("#<%=txtSellNoteNo.ClientID %>").val();
            var Mill_Rate = parseFloat($("#<%=txtMillRate.ClientID %>").val());
            var party_Bill_rate = parseFloat($("#<%=txtpartybillrate.ClientID %>").val());
            var Grade = $("#<%=txtGrade.ClientID %>").val();
            var Quantal = parseFloat($("#<%=txtQuantal.ClientID %>").val());
            var Packing = parseFloat($("#<%=txtPacking.ClientID %>").val() == "" ? 0 : $("#<%=txtPacking.ClientID %>").val());
            var Bags = parseFloat($("#<%=txtBags.ClientID %>").val() == "" ? 0 : $("#<%=txtBags.ClientID %>").val());
            var VOUCHERAMOUNT = $("#<%=lblAmount.ClientID %>").text() == "" ? 0.00 : $("#<%=lblAmount.ClientID %>").text();
            var Bp_Account = $("#<%=txtBP_Account.ClientID %>").val() == "" ? 0 : $("#<%=txtBP_Account.ClientID %>").val();
            //var DIFF =;
            //var isNumeric;
            //var n;

            var PURCHASE_RATE = parseFloat($("#<%=txtPurcRate.ClientID %>").val() == "" ? 0 : $("#<%=txtPurcRate.ClientID %>").val());
            var Payment_To = $("#<%=txtPaymentTo.ClientID %>").val() == "" ? Mill_Code : $("#<%=txtPaymentTo.ClientID %>").val();
            var Tender_From = $("#<%=txtTenderFrom.ClientID %>").val() == "" ? 0 : $("#<%=txtTenderFrom.ClientID %>").val();
            var Tender_DO = $("#<%=txtDO.ClientID %>").val() == "" ? 0 : $("#<%=txtDO.ClientID %>").val();
            var Voucher_By = $("#<%=txtVoucherBy.ClientID %>").val() == "" ? 0 : $("#<%=txtVoucherBy.ClientID %>").val();
            var Broker = $("#<%=txtBroker.ClientID %>").val() == "" ? 0 : $("#<%=txtBroker.ClientID %>").val();
            var VoucherNo = $("#<%=lblVoucherNo.ClientID %>").text() == "" ? 0 : $("#<%=lblVoucherNo.ClientID %>").text();
            var voucher_type = $("#<%=lblVoucherType.ClientID %>").text();
            // var m = ;
            var Excise_Rate = parseFloat($("#<%=txtExciseRate.ClientID %>").val() == "" ? 0 : $("#<%=txtExciseRate.ClientID %>").val());
            var GstRate_Code = $("#<%=txtGstrateCode.ClientID %>").val() == "" ? 0 : $("#<%=txtGstrateCode.ClientID %>").val();
            var Narration = $("#<%=txtNarration.ClientID %>").val() == "" ? 0 : $("#<%=txtNarration.ClientID %>").val();

            // var Purc_Rate = ;
            var type = $("#<%=drpResale.ClientID %>").val();
            var USER = '<%= Session["user"] %>';
            var Branch_Id = '<%= Session["Branch_Code"] %>';
            // var Created_By = ;
            //var Modified_By = ;
            var myNarration = "";
            var Brokrage = parseFloat($("#<%=txtBrokrage.ClientID %>").val() == "" ? 0 : $("#<%=txtBrokrage.ClientID %>").val());
            var str = "";
            var Diff_Amount = $("#<%=lbldiff.ClientID %>").text() == "" ? 0.00 : $("#<%=lbldiff.ClientID %>").text();
            var docno = 0;
            var Tender_No = $("#<%=txtTenderNo.ClientID %>").val();
            var Year_Code = '<%= Session["year"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var itemcode = $("#<%=txtitem_code.ClientID %>").val() == "" ? 0 : $("#<%=txtitem_code.ClientID %>").val();
            var season = $("#<%=txtSeason.ClientID %>").val();
            var CashDiff = parseFloat($("#<%=txtCashDiff.ClientID %>").val() == "" ? 0 : $("#<%=txtCashDiff.ClientID %>").val());
            var mc = document.getElementById("<%= hdnfmc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfmc.ClientID %>").value;
            if (mc == "" || mc == "&nbsp;") {
                mc = 0;
            }
            var pt = document.getElementById("<%= hdnfpt.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpt.ClientID %>").value;
            if (pt == "" || pt == "&nbsp;") {
                pt = 0;
            }
            var tf = document.getElementById("<%= hdnftf.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnftf.ClientID %>").value;
            if (tf == "" || tf == "&nbsp;") {
                tf = 0;
            }
            var vb = document.getElementById("<%= hdnfvb.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfvb.ClientID %>").value;
            if (vb == "" || vb == "&nbsp;") {
                vb = 0;
            }
            var bk = document.getElementById("<%= hdnfbc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbc.ClientID %>").value;
            if (bk == "" || bk == "&nbsp;") {
                bk = 0;
            }
            var ic = document.getElementById("<%= hdnfic.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfic.ClientID %>").value;
            if (ic == "" || ic == "&nbsp;") {
                ic = 0;
            }
            var tdo = document.getElementById("<%= hdnftdo.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnftdo.ClientID %>").value;
            if (tdo == "" || tdo == "&nbsp;") {
                tdo = 0;
            }
            var voucherbygstcode = document.getElementById("<%= hdnfvoucherbygst.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfvoucherbygst.ClientID %>").value;
            if (voucherbygstcode == "" || voucherbygstcode == "&nbsp;") {
                voucherbygstcode = 0;
            }
            var bp = document.getElementById("<%= hdnfbp.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbp.ClientID %>").value;
            if (bp == "" || bp == "&nbsp;") {
                bp = 0;
            }
            var bpdetail = document.getElementById("<%= hdnfbpdetail.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbpdetail.ClientID %>").value;
            if (bpdetail == "" || bpdetail == "&nbsp;") {
                bpdetail = 0;
            }
            var hsnnumber = document.getElementById("<%= hdnfhsnnumber.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfhsnnumber.ClientID %>").value;

            var TCS_Rate = parseFloat($("#<%=txtTCSRate.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSRate.ClientID %>").val());
            var TCS_Amt = parseFloat($("#<%=txtTCSAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSAmt.ClientID %>").val());


            var TDS_Rate = parseFloat($("#<%=txtTdsrate.ClientID %>").val() == "" ? 0 : $("#<%=txtTdsrate.ClientID %>").val());
            var TDS_Amt = parseFloat($("#<%=txtTdsamount.ClientID %>").val() == "" ? 0 : $("#<%=txtTdsamount.ClientID %>").val());

            var IsTDS = document.getElementById("<%= hdnfistds.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfistds.ClientID %>").value;

            var tdsAc = $("#<%=txtPaymentTo.ClientID %>").val() == "" ? 0 : $("#<%=txtPaymentTo.ClientID %>").val();

            var TDSamount = parseFloat($("#<%=txtTdsamount.ClientID %>").val() == "" ? 0 : $("#<%=txtTdsamount.ClientID %>").val());

            var TDS_Per = parseFloat($("#<%=txtTdsrate.ClientID %>").val() == "" ? 0 : $("#<%=txtTdsrate.ClientID %>").val());

            var tds = parseFloat($("#<%=txtTdsrate.ClientID %>").val() == "" ? 0 : $("#<%=txtTdsrate.ClientID %>").val());

            var CompanyStateCode = '<%= Session["CompanyGSTStateCode"] %>';
            var Temptender = $("#<%=drptempertender.ClientID %>").val();
            var Autopurchase = $("#<%=drpautopurchase.ClientID %>").val();
            var TenderInsertUpdate = ""; TenderDetail_Insert = ""; TenderDetail_Update = ""; TenderDetail_Delete = "";
            if (status == "Save") {
                TenderInsertUpdate = "Created_By='" + USER + "'";
            }
            else {
                TenderInsertUpdate = "Modified_By='" + USER + "'";
            }
            debugger;
            // LV Calculation
            if (AUTO_VOUCHER == "YES" && (type == "R" || type == "W")) {

                var myNarration = "";
                var mill = $("#<%=lblMillName.ClientID %>").text();
                if (PURCHASE_RATE > 0) {
                    myNarration = "Qntl " + Quantal + "  " + mill + " (M.R." + Mill_Rate + " P.R." + PURCHASE_RATE + ")";
                }
                var taxmillamt = parseFloat(Quantal * Diff_Amount);
                var CGSTAmount = 0.00, SGSTAmount = 0.00, IGSTAmount = 0.00, CGSTRate = 0.00, SGSTRate = 0.00, IGSTRate = 0.00;
                var LV_TCSRate = 0, LV_TCSAmt = 0, LV_NETPayble = 0;

                CGSTRate = parseFloat('<%= Session["CGST"] %>');
                SGSTRate = parseFloat('<%= Session["SGST"] %>');
                IGSTRate = parseFloat('<%= Session["IGST"] %>');

                if (CompanyStateCode == voucherbygstcode) {
                    CGSTAmount = Math.round(parseFloat(taxmillamt * CGSTRate / 100));
                    SGSTAmount = Math.round(parseFloat(taxmillamt * SGSTRate / 100));
                    IGSTRate = 0.00;
                }
                else {
                    CGSTRate = 0.00;
                    SGSTRate = 0.00;

                    IGSTAmount = Math.round(parseFloat(taxmillamt * IGSTRate / 100));
                }

                var Voucher_Amt = taxmillamt + CGSTAmount + SGSTAmount + IGSTAmount;
                var commision_Amt = Diff_Amount * Quantal;
                LV_TCSRate = document.getElementById("<%= hdnfVTCSRate.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfVTCSRate.ClientID %>").value;

                //  LV_TCSRate = parseFloat($("#<%=txtTCSRate.ClientID %>").val() == "" ? 0 : $("#<%=txtTCSRate.ClientID %>").val());
                LV_TCSAmt = Math.round(parseFloat((Voucher_Amt * LV_TCSRate) / 100));
                LV_NETPayble = Math.round(parseFloat((Voucher_Amt + LV_TCSAmt)));

                var LV_TDSRate = parseFloat($("#<%=txtvtdsrate.ClientID %>").val() == "" ? 0 : $("#<%=txtvtdsrate.ClientID %>").val());
                var LV_TDSAmt = Math.round(parseFloat(taxmillamt * LV_TDSRate / 100));
                LV_NETPayble = LV_NETPayble - LV_TDSAmt;

                if (voucher_type == "") {
                    if (LV_NETPayble != 0) {
                        if (LV_NETPayble > 0) {
                            voucher_type = "LV";
                        }
                        else {
                            voucher_type = "CV";
                        }
                    }
                }
                if (LV_NETPayble != 0) {

                    if (status == "Update") {


                        if (voucher_type == "LV") {
                            if (LV_NETPayble > 0) {

                            }
                            else {
                                alert('please check amount LV is already generated!!!')
                                $("#loader").hide();
                                return false;

                            }

                        }
                        else {
                            if (LV_NETPayble < 0) {

                            }
                            else {
                                alert('please check amount CV is already generated!!!')
                                $("#loader").hide();
                                return false;
                            }
                        }
                    }
                    else {

                        if (LV_NETPayble > 0) {
                            voucher_type = "LV";
                        }
                        else {
                            voucher_type = "CV";
                        }
                    }
                }



                var DOCNO = "";
                if (commiDoc_No == "0") {
                    DOCNO = "doc_date='" + Tender_Date + "'";
                }
                else {
                    DOCNO = "doc_no='" + commiDoc_No + "' doc_date='" + Tender_Date + "'";
                }
                if (Diff_Amount != 0) {
                    XML = XML + "<Commission " + DOCNO + " link_no='0' link_type='' link_id='" + TenderNo + "' ac_code='" + Voucher_By + "' unit_code='0' " +
                          "broker_code='" + Broker + "' qntl='" + Quantal + "' packing='" + Packing + "' bags='" + Bags + "' grade='" + Grade + "' " +
                          "transport_code='0' mill_rate='" + Mill_Rate + "' sale_rate='0.00' purc_rate='" + PURCHASE_RATE + "' " +
    "commission_amount='" + commision_Amt + "' resale_rate='0.00' resale_commission='0.00' misc_amount='0.00' " +
    "texable_amount='" + taxmillamt + "' gst_code='" + GstRate_Code + "' cgst_rate='" + CGSTRate + "' cgst_amount='" + CGSTAmount + "' " +
    "sgst_rate='" + SGSTRate + "' sgst_amount='" + SGSTAmount + "' igst_rate='" + IGSTRate + "' igst_amount='" + IGSTAmount + "' bill_amount='" + Voucher_Amt + "' " +
    "Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Id + "' " + TenderInsertUpdate + " " +
    "commissionid='" + commisionid + "' ac='" + vb + "' uc='0' bc='" + bk + "' tc='0' mill_code='" + Mill_Code + "' mc='" + mc + "' " +
    "narration1='" + myNarration + "' narration2='' narration3='' narration4='' TCS_Rate='" + LV_TCSRate + "' TCS_Amt='" + LV_TCSAmt + "' TCS_Net_Payable='" + LV_NETPayble + "' " +
    " Tran_Type='" + voucher_type + "' item_code='"
    + itemcode + "' ic='" + ic + "' Frieght_Rate='0' Frieght_amt='0' subtotal='" + taxmillamt + "' IsTDS='" + IsTDS + "' TDS_Ac='" + tdsAc + "' TDS_Per='" + LV_TDSRate +
    "'  TDSAmount='" + LV_TDSAmt + "'  TDS='" + tds + "' ta='" + pt + "' HSN='" + hsnnumber + "'></Commission>";


                    // LV Gledger

                    var drcr = "";
                    Order_Code = 1;
                    if (Diff_Amount > 0) {
                        drcr = "D";
                    }
                    else {
                        drcr = "C";
                    }




                    XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + Voucher_By + "' " +
                          "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(Voucher_Amt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr + "' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                          "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + vb + "' vc='0' progid='11' tranid='0'/>";

                    //TCS Commission Bill Ledger Effect
                    if (LV_TCSAmt != 0) {
                        if (voucher_type == "LV") {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + Voucher_By + "' " +
                                  "UNIT_code='0' NARRATION='TCS' AMOUNT='" + Math.abs(LV_TCSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                  "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + vb + "' vc='0' progid='11' tranid='0'/>";
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                  "UNIT_code='0' NARRATION='TCS' AMOUNT='" + Math.abs(LV_TCSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                  "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + '<%=Session["SaleTCSAcid"] %>' + "' vc='0' progid='11' tranid='0'/>";
                        }
                        else {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + Voucher_By + "' " +
                                  "UNIT_code='0' NARRATION='TCS' AMOUNT='" + Math.abs(LV_TCSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                  "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + vb + "' vc='0' progid='11' tranid='0' />";
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                  "UNIT_code='0' NARRATION='TCS' AMOUNT='" + Math.abs(LV_TCSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                  "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + '<%=Session["SaleTCSAcid"] %>' + "' vc='0' progid='11' tranid='0'/>";
                        }
                    }
                    if (LV_TDSAmt != 0) {
                        if (voucher_type == "LV") {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + Voucher_By + "' " +
                                  "UNIT_code='0' NARRATION='TDS' AMOUNT='" + Math.abs(LV_TDSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                  "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + vb + "' vc='0' progid='11' tranid='0' />";
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + '<%=Session["SaleTDSAc"] %>' + "' " +
                                  "UNIT_code='0' NARRATION='TDS' AMOUNT='" + Math.abs(LV_TDSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                  "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + '<%=Session["SaleTDSacid"] %>' + "' vc='0' progid='11' tranid='0'/>";
                        }
                        else {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + Voucher_By + "' " +
                                  "UNIT_code='0' NARRATION='TDS' AMOUNT='" + Math.abs(LV_TDSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                  "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + vb + "' vc='0' progid='11' tranid='0' />";
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + '<%=Session["SaleTDSAc"] %>' + "' " +
                                  "UNIT_code='0' NARRATION='TDS' AMOUNT='" + Math.abs(LV_TDSAmt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                                  "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + '<%=Session["SaleTDSacid"] %>' + "' vc='0' progid='11' tranid='0'/>";
                        }
                    }


                    // Quality_Diff_Ac Effect Credit
                    if (Diff_Amount > 0) {
                        Order_Code = parseInt(Order_Code) + 1;


                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["COMMISSION_AC"] %> + "' " +
                          "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(taxmillamt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                          "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["commissionid"] %> + "' vc='0' progid='11' tranid='0'/>";

                        if (CGSTAmount != 0) {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["SaleCGSTAc"] %> + "' " +
                          "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(CGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                          "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["SaleCGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";
                        }
                        // SGST Acv

                        if (SGSTAmount != 0) {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["SaleSGSTAc"] %> + "' " +
                         "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(SGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                         "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                         "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["SaleSGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";
                        }
                        //IGST Acc
                        if (IGSTAmount != 0) {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["SaleIGSTAc"] %> + "' " +
                         "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(IGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                         "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                         "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["SaleIGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";
                        }
                    }
                    else {
                        Order_Code = parseInt(Order_Code) + 1;


                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["COMMISSION_AC"] %> + "' " +
                          "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(taxmillamt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                          "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["commissionid"] %> + "' vc='0' progid='11' tranid='0'/>";

                        if (CGSTAmount != 0) {
                            Order_Code = parseInt(Order_Code) + 1;


                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["PurchaseCGSTAc"] %> + "' " +
                          "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(CGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                          "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["PurchaseCGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";

                        }
                        // SGST Acv

                        if (SGSTAmount != 0) {
                            Order_Code = parseInt(Order_Code) + 1;


                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["PurchaseSGSTAc"] %> + "' " +
                         "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(SGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                         "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                         "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["PurchaseSGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";

                        }
                        //IGST Acc
                        if (IGSTAmount != 0) {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["PurchaseIGSTAc"] %> + "' " +
                         "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(IGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                         "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                         "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["PurchaseIGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";

                        }
                    }
                }
                else {
                    if (status == "Update") {
                        XML = XML + "<Commission " + DOCNO + " link_no='0' link_type='' link_id='" + TenderNo + "' ac_code='" + Voucher_By + "' unit_code='0' " +
                          "broker_code='" + Broker + "' qntl='" + Quantal + "' packing='" + Packing + "' bags='" + Bags + "' grade='" + Grade + "' " +
                          "transport_code='0' mill_rate='" + Mill_Rate + "' sale_rate='0.00' purc_rate='" + PURCHASE_RATE + "' " +
    "commission_amount='" + commision_Amt + "' resale_rate='0.00' resale_commission='0.00' misc_amount='0.00' " +
    "texable_amount='" + taxmillamt + "' gst_code='" + GstRate_Code + "' cgst_rate='" + CGSTRate + "' cgst_amount='" + CGSTAmount + "' " +
    "sgst_rate='" + SGSTRate + "' sgst_amount='" + SGSTAmount + "' igst_rate='" + IGSTRate + "' igst_amount='" + IGSTAmount + "' bill_amount='" + Voucher_Amt + "' " +
    "Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Id + "' " + TenderInsertUpdate + " " +
    "commissionid='" + commisionid + "' ac='" + vb + "' uc='0' bc='" + bk + "' tc='0' mill_code='" + Mill_Code + "' mc='" + mc + "' " +
    "narration1='" + myNarration + "' narration2='' narration3='' narration4='' TCS_Rate='" + LV_TCSRate + "' TCS_Amt='" + LV_TCSAmt + "' TCS_Net_Payable='" + LV_NETPayble + "' " +
    " Tran_Type='" + voucher_type + "' item_code='"
    + itemcode + "' ic='" + ic + "' Frieght_Rate='0' Frieght_amt='0' subtotal='" + taxmillamt + "' IsTDS='" + IsTDS + "' TDS_Ac='" + tdsAc + "' TDS_Per='" + LV_TDSRate +
    "'  TDSAmount='" + LV_TDSAmt + "'  TDS='" + tds + "' ta='" + pt + "' HSN='" + hsnnumber + "'></Commission>";


                        // LV Gledger

                        var drcr = "";
                        Order_Code = 1;
                        if (Diff_Amount > 0) {
                            drcr = "D";
                        }
                        else {
                            drcr = "C";
                        }




                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + Voucher_By + "' " +
                              "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(Diff_Amount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr + "' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                              "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + vb + "' vc='0' progid='11' tranid='0' saleid='0'/>";

                        //TCS Commission Bill Ledger Effect


                        // Quality_Diff_Ac Effect Credit
                        if (Diff_Amount > 0) {
                            Order_Code = parseInt(Order_Code) + 1;


                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["COMMISSION_AC"] %> + "' " +
                          "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(taxmillamt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                          "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["commissionid"] %> + "' vc='0' progid='11' tranid='0'/>";

                            if (CGSTAmount != 0) {
                                Order_Code = parseInt(Order_Code) + 1;

                                XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["SaleCGSTAc"] %> + "' " +
                          "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(CGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                          "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["SaleCGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";
                        }
                            // SGST Acv

                        if (SGSTAmount != 0) {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["SaleSGSTAc"] %> + "' " +
                         "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(SGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                         "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                         "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["SaleSGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";
                        }
                            //IGST Acc
                        if (IGSTAmount != 0) {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["SaleIGSTAc"] %> + "' " +
                         "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(IGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                         "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                         "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["SaleIGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";
                        }
                    }
                    else {
                        Order_Code = parseInt(Order_Code) + 1;


                        XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["COMMISSION_AC"] %> + "' " +
                          "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(taxmillamt) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                          "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["commissionid"] %> + "' vc='0' progid='11' tranid='0'/>";

                        if (CGSTAmount != 0) {
                            Order_Code = parseInt(Order_Code) + 1;


                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["PurchaseCGSTAc"] %> + "' " +
                          "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(CGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                          "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["PurchaseCGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";

                        }
                            // SGST Acv

                        if (SGSTAmount != 0) {
                            Order_Code = parseInt(Order_Code) + 1;


                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["PurchaseSGSTAc"] %> + "' " +
                         "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(SGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                         "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                         "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["PurchaseSGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";

                        }
                            //IGST Acc
                        if (IGSTAmount != 0) {
                            Order_Code = parseInt(Order_Code) + 1;

                            XML = XML + "<Ledger TRAN_TYPE='" + voucher_type + "' CASHCREDIT='' DOC_DATE='" + Tender_Date + "' AC_CODE='" + <%=Session["PurchaseIGSTAc"] %> + "' " +
                         "UNIT_code='0' NARRATION='' AMOUNT='" + Math.abs(IGSTAmount) + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                         "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Id + "' " +
                         "SORT_TYPE='" + voucher_type + "' SORT_NO='" + commiDoc_No + "' ac='" + <%=Session["PurchaseIGSTid"] %> + "' vc='0' progid='11' tranid='0'/>";

                        }
                    }
                }
            }
        }

            //LV End    

        XML = XML + "<TenderHead Tender_No='" + TenderNo + "' Company_Code='" + Company_Code + "' Tender_Date='" + Tender_Date + "' Lifting_Date='" + Lifting_Date + "' Mill_Code='" + Mill_Code + "' " +
      "Grade='" + Grade + "' Quantal='" + Quantal + "' Packing='" + Packing + "' Bags='" + Bags + "' " +
      "Payment_To='" + Payment_To + "' Tender_From='" + Tender_From + "' Tender_DO='" + Tender_DO + "' Voucher_By='" + Voucher_By + "' Bp_Account='" + Bp_Account + "' " +
      "Broker='" + Broker + "' Excise_Rate='" + Excise_Rate + "' Narration='" + Narration + "' Mill_Rate='" + Mill_Rate + "' " +
      "" + TenderInsertUpdate + " Year_Code='" + Year_Code + "' Purc_Rate='" + PURCHASE_RATE + "' type='" + type + "' " +
      "Branch_Id='" + Branch_Id + "' Voucher_No='" + VoucherNo + "' Sell_Note_No='" + Sell_Note_No + "' Brokrage='" + Brokrage + "' " +
      "tenderid='" + tenderId + "' mc='" + mc + "' itemcode='" + itemcode + "' season='" + season + "' pt='" + pt + "' " +
      "tf='" + tf + "' td='" + tdo + "' " +
      " vb='" + vb + "' bk='" + bk + "' Bp='" + bp + "' " +
      "ic='" + ic + "' gstratecode = '" + GstRate_Code + "' CashDiff='" + CashDiff + "' TCS_Rate='" + TCS_Rate + "' TCS_Amt='" + TCS_Amt +
      "' Voucher_Type='" + voucher_type + "' commissionid='" + commisionid + "' Party_Bill_Rate='" + party_Bill_rate + "' TDS_Rate='" + TDS_Rate + "' TDS_Amt='" + TDS_Amt + "' Temptender='" + Temptender + "'  AutoPurchaseBill='" + Autopurchase + "'>";
            //Detail Calculation
        var TenderDetail_Value = ""; concatid = "";

        var gridView = document.getElementById("<%=grdDetail.ClientID %>");
        var grdrow = gridView.getElementsByTagName("tr");

        for (var i = 1; i < grdrow.length; i++) {
            // Assign Value
            if (gridView.rows[i].cells[20].innerHTML != "N") {
                var ID = gridView.rows[i].cells[2].innerHTML;
                var Party = gridView.rows[i].cells[3].innerHTML;
                var DBroker = gridView.rows[i].cells[5].innerHTML;
                var Shipto = gridView.rows[i].cells[7].innerHTML;

                var DQty = gridView.rows[i].cells[9].innerHTML;
                var SaleRate = gridView.rows[i].cells[10].innerHTML;
                var Cashdiff = gridView.rows[i].cells[11].innerHTML;
                var Commission = gridView.rows[i].cells[12].innerHTML;
                var d = gridView.rows[i].cells[13].innerHTML;
                var DSauda_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                var d = gridView.rows[i].cells[14].innerHTML;
                var DLifting_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

                var SaudaNarration = gridView.rows[i].cells[15].innerHTML == "&nbsp;" ? "" : gridView.rows[i].cells[15].innerHTML;
                var DeliveryType = gridView.rows[i].cells[16].innerHTML == "&nbsp;" ? "" : gridView.rows[i].cells[16].innerHTML;
                if (DeliveryType == "Commission") {
                    DeliveryType = "C";
                }
                if (DeliveryType == "With GST Naka Delivery") {
                    DeliveryType = "N";
                }
                if (DeliveryType == "DO") {
                    DeliveryType = "D";
                }
                if (DeliveryType == "Naka Delivery without GST Rate") {
                    DeliveryType = "A";
                }
                var SubBroker = gridView.rows[i].cells[17].innerHTML;
                var partyid = gridView.rows[i].cells[22].innerHTML;
                var Dbc = gridView.rows[i].cells[23].innerHTML;
                var Sbc = gridView.rows[i].cells[24].innerHTML;
                var GSTrate1 = gridView.rows[i].cells[25].innerHTML;
                if (GSTrate1 == "&nbsp;") {
                    GSTrate1 = "0";
                }
                var GSTamt1 = gridView.rows[i].cells[26].innerHTML;
                if (GSTamt1 == "&nbsp;") {
                    GSTamt1 = "0";
                }
                var tcsrate1 = gridView.rows[i].cells[27].innerHTML;
                if (tcsrate1 == "&nbsp;") {
                    tcsrate1 = "0";
                }
                var tcsamt1 = gridView.rows[i].cells[28].innerHTML;
                if (tcsamt1 == "&nbsp;") {
                    tcsamt1 = "0";
                }
                var shiptoid = gridView.rows[i].cells[29].innerHTML;
                if (shiptoid == "" || shiptoid == "&nbsp;") {
                    shiptoid = 0;
                }
                if (partyid == "" || partyid == "&nbsp;") {
                    partyid = 0;
                }
                if (Dbc == "" || Dbc == "&nbsp;") {
                    Dbc = 0;
                }
                if (Sbc == "" || Sbc == "&nbsp;") {
                    Sbc = 0;
                }
                var BP_Detail = gridView.rows[i].cells[30].innerHTML;
                if (BP_Detail == "" || BP_Detail == "&nbsp;") {
                    BP_Detail = 0;
                }
                var bpid = gridView.rows[i].cells[32].innerHTML;
                if (bpid == "" || bpid == "&nbsp;") {
                    bpid = 0;
                } 
                var loding_by_us = gridView.rows[i].cells[33].innerHTML;
                if (loding_by_us == "" || loding_by_us == "&nbsp;") {
                    loding_by_us = "";
                }
                debugger;
                var ddi = tdetailid;
                if (gridView.rows[i].cells[20].innerHTML == "A") {
                    XML = XML + "<TenderDetailInsert Tender_No='" + TenderNo + "' Company_Code='" + Company_Code + "' Buyer='" + Party + "' Buyer_Quantal='" + DQty + "' " +
               "Sale_Rate='" + SaleRate + "' Commission_Rate='" + Commission + "' Sauda_Date='" + DSauda_Date + "' Lifting_Date='" + DLifting_Date + "' " +
               "Narration='" + SaudaNarration + "' ID='" + ID + "' Buyer_Party='" + DBroker + "' AutoID='0' IsActive='0' year_code='" + Year_Code + "' Branch_Id='" + Branch_Id + "' Delivery_Type='" + DeliveryType + "' " +
               "tenderid='" + tenderId + "' buyerid='" + partyid + "' buyerpartyid='" + Dbc + "' " +
               "sub_broker='" + SubBroker + "' sbr='" + Sbc + "' tcs_rate='" + tcsrate1 + "' gst_rate='" + GSTrate1 +
               "' tcs_amt='" + tcsamt1 + "' gst_amt='" + GSTamt1 + "' ShipTo='" + Shipto + "' CashDiff='" + Cashdiff + "' shiptoid='" + shiptoid + "' BP_Detail='" + BP_Detail + "' bpid='" + bpid + "' loding_by_us='" + loding_by_us + "'/>";
                    ddi = parseInt(ddi) + 1;
                }
                else if (gridView.rows[i].cells[20].innerHTML == "U") {
                    var tender_detailid = gridView.rows[i].cells[19].innerHTML;
                    XML = XML + "<TenderDetail Tender_No='" + TenderNo + "' Company_Code='" + Company_Code + "' Buyer='" + Party + "' Buyer_Quantal='" + DQty + "' " +
                                       "Sale_Rate='" + SaleRate + "' Commission_Rate='" + Commission + "' Sauda_Date='" + DSauda_Date + "' Lifting_Date='" + DLifting_Date + "' " +
                                       "Narration='" + SaudaNarration + "' ID='" + ID + "' Buyer_Party='" + DBroker + "' AutoID='0' IsActive='0' year_code='" + Year_Code + "' Branch_Id='" + Branch_Id + "' Delivery_Type='" + DeliveryType + "' " +
                                       "tenderid='" + tenderId + "' tenderdetailid='" + tender_detailid + "' buyerid='" + partyid + "' buyerpartyid='" + Dbc + "' " +
                                       "sub_broker='" + SubBroker + "' sbr='" + Sbc + "' tcs_rate='" + tcsrate1 + "' gst_rate='" + GSTrate1 + "' tcs_amt='" + tcsamt1 + "' gst_amt='" + GSTamt1 + "' ShipTo='" + Shipto + "' CashDiff='" + Cashdiff + "' shiptoid='" + shiptoid + "' BP_Detail='" + BP_Detail + "' bpid='" + bpid + "' loding_by_us='" + loding_by_us + "'/>"; 
                }
                else if (gridView.rows[i].cells[20].innerHTML == "D") {
                    var tender_detailid = gridView.rows[i].cells[19].innerHTML;
                    XML = XML + "<TenderDetailDelete Tender_No='" + TenderNo + "' Company_Code='" + Company_Code + "' Buyer='" + Party + "' Buyer_Quantal='" + DQty + "' " +
                                       "Sale_Rate='" + SaleRate + "' Commission_Rate='" + Commission + "' Sauda_Date='" + DSauda_Date + "' Lifting_Date='" + DLifting_Date + "' " +
                                       "Narration='" + SaudaNarration + "' ID='" + ID + "' Buyer_Party='" + DBroker + "' AutoID='0' IsActive='0' year_code='" + Year_Code + "' Branch_Id='" + Branch_Id + "' Delivery_Type='" + DeliveryType + "' " +
                                       "tenderid='" + tenderId + "' tenderdetailid='" + tender_detailid + "' buyerid='" + partyid + "' buyerpartyid='" + Dbc + "' " +
                                       "sub_broker='" + SubBroker + "' sbr='" + Sbc + "' tcs_rate='" + tcsrate1 + "' gst_rate='" + GSTrate1 + "' tcs_amt='" + tcsamt1 + "' gst_amt='" + GSTamt1 + "' ShipTo='" + Shipto + "' CashDiff='" + Cashdiff + "' shiptoid='" + shiptoid + "' BP_Detail='" + BP_Detail + "' bpid='" + bpid + "' loding_by_us='" + loding_by_us + "'/>";
                }
            }
        }

        XML = XML + "</TenderHead></ROOT>";
        debugger;
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
                //$("#loader").hide();
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
                    window.open('../BussinessRelated/pgeTenderPurchasexml.aspx?tenderid=' + id + '&Action=1', "_self");

                }
            }
            else {
                var num = parseInt(response.d);

                if (isNaN(num)) {
                    alert(response.d)

                }
                else {
                    window.open('../BussinessRelated/PgeTenderHeadUtility.aspx', "_self");
                }
            }

        }
    }
    </script>
    <%-- <script language="JavaScript" type="text/javascript">
        window.onbeforeunload = confirmExit;
        function confirmExit() {
            return "Are you sure you want to exit this page?";
        }
    </script>--%>
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

        //window.onbeforeunload = function (e) {
        //    var e = e || window.event;
        //    if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
        //    return 'Browser is being closed, is it okay?'; // for Safari and Chrome
        //};

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Tender Purchase   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px;">
        <img height="40%" width="40%" src="../Images/ajax-loader3.gif" />
    </div>

    <asp:UpdatePanel ID="upPnlPopup" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfIsClick" runat="server" Value="0" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfNextFocus" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="vouchernumber" runat="server" />
            <asp:HiddenField ID="hdnfmc" runat="server" />
            <asp:HiddenField ID="hdnfmillshort" runat="server" />
            <asp:HiddenField ID="hdnfic" runat="server" />
            <asp:HiddenField ID="hdnfpt" runat="server" />
            <asp:HiddenField ID="hdnftf" runat="server" />
            <asp:HiddenField ID="hdnftdo" runat="server" />
            <asp:HiddenField ID="hdnfvb" runat="server" />
            <asp:HiddenField ID="hdnfbc" runat="server" />
            <asp:HiddenField ID="hdnfbp" runat="server" />
            <asp:HiddenField ID="hdnfbpdetail" runat="server" />
            <asp:HiddenField ID="hdnftenderno" runat="server" />
            <asp:HiddenField ID="hdnftenderid" runat="server" />
            <asp:HiddenField ID="hdnfvoucherbygst" runat="server" />
            <asp:HiddenField ID="hdnfcommisionid" runat="server" />
            <asp:HiddenField ID="hdnfmillpaymentdate" runat="server" />
            <asp:HiddenField ID="hdnfSaleTDS" runat="server" />
            <asp:HiddenField ID="hdnfTCSRate" runat="server" />
            <asp:HiddenField ID="hdnfistds" runat="server" />
            <asp:HiddenField ID="hdnfhsnnumber" runat="server" />
            <asp:HiddenField ID="hdnfvouchertds" runat="server" />
            <asp:HiddenField ID="hdnfVSaleTDS" runat="server" />
            <asp:HiddenField ID="hdnfVTCSRate" runat="server" />

            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="True" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table width="100%" align="left" cellspacing="4px" cellpadding="1px">
                    <tr>
                        <td colspan="4">
                            <table width="80%" align="left">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="80px"
                                            Height="25px" OnClick="btnAdd_Click" />
                                        &nbsp;
                                        <asp:Button OnClientClick="if (!Validate()) return false;" ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="80px"
                                            UseSubmitBehavior="false" Height="25px" ValidationGroup="add" OnClick="btnSave_Click" TabIndex="47" />
                                        &nbsp;
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="80px"
                                            Height="25px" ValidationGroup="save" OnClick="btnEdit_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="80px"
                                            Height="25px" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" />
                                        &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="80px"
                                            Height="25px" OnClick="btnCancel_Click" TabIndex="48" />
                                        &nbsp;
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="80px" OnClientClick="Back()"
                                            Height="25px" TabIndex="49" />
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

                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;">Change No:</td>

                        <td colspan="4" align="left">
                            <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged" TabIndex="1"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="txtxf" FilterType="Numbers" InvalidChars="."
                                TargetControlID="txtEditDoc_No">
                            </ajax1:FilteredTextBoxExtender>
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Names="verdana" Font-Italic="true"
                                Font-Size="Small" ForeColor="Yellow" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;">Tender No:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtTenderNo" runat="server" CssClass="txt" Width="100px" TabIndex="1"
                                AutoPostBack="false" OnTextChanged="txtTenderNo_TextChanged" Height="24px"></asp:TextBox>
                            <asp:Button ID="btnChangeNo" runat="server" Text="Change No" CssClass="btnHelp" Width="69px"
                                TabIndex="1" OnClick="changeNo_click" Height="24px" />
                            <asp:Label ID="lblTender_Id" runat="server" Text="" Font-Names="verdana"
                                ForeColor="Blue" Font-Bold="true" Font-Size="12px" Visible="true"></asp:Label></legend>
                            Copy From
                            <asp:TextBox runat="server" ID="txtcopyfrom" CssClass="txt" Width="100px" Height="24px"
                                AutoPostBack="true" OnTextChanged="txtcopyfrom_TextChanged" TabIndex="1"></asp:TextBox>

                            &nbsp;Resale/Mill:&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="drpResale" runat="server" AutoPostBack="true" Width="120px"
                                CssClass="ddl" OnSelectedIndexChanged="drpResale_SelectedIndexChanged" TabIndex="2">
                                <asp:ListItem Text="Resale" Value="R"></asp:ListItem>
                                <asp:ListItem Text="Mill" Value="M" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="With Payment" Value="W"></asp:ListItem>
                                <asp:ListItem Text="Party Bill Rate" Value="P"></asp:ListItem>
                            </asp:DropDownList>

                            &nbsp;Temp Tender:&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="drptempertender" runat="server" AutoPostBack="true" Width="120px"
                                CssClass="ddl" OnSelectedIndexChanged="drptempertender_SelectedIndexChanged" TabIndex="3">
                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>

                            </asp:DropDownList>
                            &nbsp;Auto Purchase Bill:&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="drpautopurchase" runat="server" AutoPostBack="true" Width="120px"
                                CssClass="ddl" OnSelectedIndexChanged="drpautopurchase_SelectedIndexChanged" TabIndex="3">
                                <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="N"></asp:ListItem>

                            </asp:DropDownList>
                            &nbsp; Voucher No:<asp:Label runat="server" ID="lblVoucherNo" CssClass="lblName"></asp:Label>
                            <asp:Label runat="server" ID="lblVoucherType" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Date:
                        </td>
                        <td align="left" colspan="3">
                            <%--<asp:TextBox ID="txtDate" runat="server" CssClass="txt" Width="100px" AutoPostBack="True"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                OnTextChanged="txtDate_TextChanged" TabIndex="3" Height="24px"></asp:TextBox>
                            <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" />
                            <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtDate"
                                PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>--%>


                            <asp:TextBox ID="txtDate" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtDate_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" />
                            <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtDate"
                                PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>

                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Payment Date:
                            &nbsp;&nbsp;<asp:TextBox ID="txtLiftingDate" runat="server" CssClass="txt" Width="100px"
                                AutoPostBack="true" OnTextChanged="txtLiftingDate_TextChanged" TabIndex="4" onkeyup="ValidateDate(this,event.keyCode)"
                                onkeydown="return DateFormat(this,event.keyCode)" MaxLength="10" Height="24px"></asp:TextBox>
                            <asp:Image ID="imgcalender1" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" />
                            <ajax1:CalendarExtender ID="calenderExtenderLiftingdate" runat="server" TargetControlID="txtLiftingDate"
                                PopupButtonID="imgcalender1" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                            <asp:Label runat="server" ID="lblMesg"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Mill Code:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtMillCode" runat="server" CssClass="txt" Width="100px" Style="text-align: right;" onkeydown="MillCode(event);"
                                AutoPostBack="false" OnTextChanged="txtMillCode_TextChanged"
                                TabIndex="5" Height="24px"></asp:TextBox>
                            <asp:Button ID="btnMillCode" runat="server" Text="..." CssClass="btnHelp" OnClick="btnMillCode_Click"
                                Height="24px" Width="20px" />
                            <%--<ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtMillCode" FilterType="Numbers" TargetControlID="txtMillCode"></ajax1:FilteredTextBoxExtender>--%>
                            <asp:Label ID="lblMillName" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblMill_Id" runat="server" CssClass="lblName" Visible="false"></asp:Label>
                            Season: 
                            <asp:TextBox ID="txtSeason" runat="server" CssClass="txt" Width="100px" Style="text-align: right;"
                                AutoPostBack="false"
                                TabIndex="6" Height="24px"></asp:TextBox>
                            Item Code: 
                            <asp:TextBox ID="txtitem_code" runat="server" CssClass="txt" Width="100px" Style="text-align: right;"
                                onkeydown="item(event);" AutoPostBack="false" OnTextChanged="txtitem_code_TextChanged"
                                TabIndex="6" Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtitem_code" runat="server" Text="..." CssClass="btnHelp" OnClick="btntxtitem_code_Click"
                                Height="24px" Width="20px" />
                            <asp:Label ID="lblitemname" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Grade:
                        </td>
                        <td align="left" colspan="6">
                            <asp:TextBox ID="txtGrade" runat="server" CssClass="txt" Width="100px" TabIndex="7"
                                AutoPostBack="false" OnTextChanged="txtGrade_TextChanged" Height="24px" onkeydown="Grade(event);"></asp:TextBox>
                            <asp:Button ID="btnGrade" runat="server" Text="..." CssClass="btnHelp" OnClick="btnGrade_Click"
                                Height="24px" Width="20px" />
                            <asp:Label ID="lblGrade_Id" runat="server" CssClass="lblName" Visible="false"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Quintal:
                            <asp:TextBox ID="txtQuantal" runat="server" CssClass="txt" Width="100px" Style="text-align: left;"
                                AutoPostBack="True" OnTextChanged="txtQuantal_TextChanged" TabIndex="8" Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtQuantal" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtQuantal">
                            </ajax1:FilteredTextBoxExtender>
                            Packing:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPacking" runat="server" CssClass="txt" AutoPostBack="True" Width="100px"
                                Style="text-align: left;" OnTextChanged="txtPacking_TextChanged" TabIndex="9"
                                Height="24px"></asp:TextBox>
                           <%-- <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtPacking" FilterType="Numbers,cutom"
                                TargetControlID="txtPacking">
                            </ajax1:FilteredTextBoxExtender>--%>
                               <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtPacking" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtPacking">
                            </ajax1:FilteredTextBoxExtender>
                            &nbsp;&nbsp;&nbsp;Bags:
                            <asp:TextBox ID="txtBags" runat="server" CssClass="txt" ReadOnly="true" Width="100px"
                                TabIndex="10" Style="text-align: left;" Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBags" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtBags">
                            </ajax1:FilteredTextBoxExtender>
                            Balance Self:
                            <asp:Label ID="lblBalanceSelf" runat="server" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Mill Rate:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtMillRate" runat="server" CssClass="txt" AutoPostBack="true" Width="100px"
                                Style="text-align: right;" OnTextChanged="txtMillRate_TextChanged" TabIndex="11"
                                Height="24px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtMillRate" SetFocusOnError="true" runat="server"
                                ControlToValidate="txtMillRate" CssClass="validator" Display="Dynamic" Text="Required"
                                ErrorMessage="Required" ValidationGroup="add">
                            </asp:RequiredFieldValidator>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtMillRate" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtMillRate">
                            </ajax1:FilteredTextBoxExtender>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Purch Rate:
                            <asp:TextBox ID="txtPurcRate" runat="server" AutoPostBack="True" CssClass="txt" Width="100px"
                                OnTextChanged="txtPurcRate_TextChanged" TabIndex="12" Height="24px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtPurcRate" Enabled="false" SetFocusOnError="true"
                                runat="server" ControlToValidate="txtPurcRate" CssClass="validator" Display="Dynamic"
                                Text="Required" ErrorMessage="Required" ValidationGroup="add">
                            </asp:RequiredFieldValidator>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtPurcRate" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtPurcRate">
                            </ajax1:FilteredTextBoxExtender>

                            Party Bill Rate
                             <asp:TextBox ID="txtpartybillrate" runat="server" AutoPostBack="True" CssClass="txt" Width="100px"
                                 OnTextChanged="txtpartybillrate_TextChanged" TabIndex="13" Height="24px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rftxtpartybillrate" Enabled="false" SetFocusOnError="true"
                                runat="server" ControlToValidate="txtpartybillrate" CssClass="validator" Display="Dynamic"
                                Text="Required" ErrorMessage="Required" ValidationGroup="add">
                            </asp:RequiredFieldValidator>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="Filteredtxtpartybillrate" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtpartybillrate">
                            </ajax1:FilteredTextBoxExtender>
                             </td>
                        </tr>
                    <tr>
                         <td align="right">  BP Account:
                             </td>
                             <td align="left" colspan="4"> 
                             <asp:TextBox ID="txtBP_Account" runat="server" CssClass="txt" Width="100px" Style="text-align: right;"
                                AutoPostBack="false" OnTextChanged="txtBP_Account_TextChanged" TabIndex="14" Height="24px" onkeydown="BP_Account(event);"></asp:TextBox>
                            <asp:Button ID="btnBP_Account" runat="server" Text="..." CssClass="btnHelp" OnClick="btnBP_Account_Click"
                                Height="24px" Width="20px" />
                            <asp:Label ID="lblBP_Account" runat="server" CssClass="lblName"></asp:Label>
                             
                             
                                   &nbsp;
                           B.P:
                             <asp:TextBox ID="txtCashDiff" runat="server" AutoPostBack="false" CssClass="txt" Width="100px"
                                 OnTextChanged="txtCashDiff_TextChanged" TabIndex="15" Height="24px"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;Diff:
                            <asp:Label ID="lbldiff" runat="server" Text="Diff"></asp:Label>
                            Amount:
                            <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
                               
                                
                        </td>
                    
                    <tr>
                        <td align="right">Payment To:
                        </td>
                        <td colspan="6" align="left">
                            <asp:TextBox ID="txtPaymentTo" runat="server" CssClass="txt" Width="100px" Style="text-align: right;"
                                AutoPostBack="false" OnTextChanged="txtPaymentTo_TextChanged" TabIndex="16" Height="24px" onkeydown="PaymentTo(event);"></asp:TextBox>
                            <asp:Button ID="btnPaymentTo" runat="server" Text="..." CssClass="btnHelp" OnClick="btnPaymentTo_Click"
                                Height="24px" Width="20px" />
                            <asp:Label ID="lblPaymentTo" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblPayment_Id" runat="server" CssClass="lblName" Visible="false"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvtxtPaymentTo" runat="server" ControlToValidate="txtPaymentTo"
                                CssClass="validator" Display="Dynamic" Enabled="false" ErrorMessage="Required"
                                SetFocusOnError="true" Text="Required" ValidationGroup="add">
                            </asp:RequiredFieldValidator>
                            Tender From:
                            <asp:TextBox ID="txtTenderFrom" runat="server" CssClass="txt" Width="100px" TabIndex="17"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtTenderFrom_TextChanged"
                                Height="24px" onkeydown="tenderfrom(event);"></asp:TextBox>
                            <asp:Button ID="btnTenderFrom" runat="server" Text="..." CssClass="btnHelp" OnClick="btnTenderFrom_Click"
                                Height="24px" Width="20px" />
                            <asp:Label ID="lblTenderFrom" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblTenderForm_Id" runat="server" CssClass="lblName" Visible="false"></asp:Label>
                            <%--<ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtTenderFrom" FilterType="Numbers"  TargetControlID="txtTenderFrom"></ajax1:FilteredTextBoxExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Tender D.O.:
                        </td>
                        <td colspan="6" align="left">
                            <asp:TextBox ID="txtDO" runat="server" CssClass="txt" Width="100px" TabIndex="18"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtDO_TextChanged"
                                Height="24px" onkeydown="tenderDo(event);"></asp:TextBox>
                            <%--<ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtDO" FilterType="Numbers"  TargetControlID="txtDO"></ajax1:FilteredTextBoxExtender>--%>
                            <asp:Button ID="btnTenderDO" runat="server" Text="..." OnClick="btnTenderDO_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblDO" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblTenderDo_Id" runat="server" CssClass="lblName" Visible="false"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvtxtDO" Enabled="false" SetFocusOnError="true"
                                runat="server" ControlToValidate="txtDO" CssClass="validator" Display="Dynamic"
                                Text="Required" ErrorMessage="Required" ValidationGroup="add">
                            </asp:RequiredFieldValidator>
                            Voucher By: &nbsp;
                            <asp:TextBox ID="txtVoucherBy" runat="server" CssClass="txt" Width="100px" Style="text-align: right;"
                                AutoPostBack="false" OnTextChanged="txtVoucherBy_TextChanged" TabIndex="19" Height="24px" onkeydown="VoucherBy(event);"></asp:TextBox>
                            <%--<ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtVoucherBy" FilterType="Numbers"  TargetControlID="txtVoucherBy"></ajax1:FilteredTextBoxExtender>--%>
                            <asp:Button ID="btnVoucherBy" runat="server" Text="..." CssClass="btnHelp" OnClick="btnVoucherBy_Click"
                                Height="24px" Width="20px" />
                            <asp:Label ID="lblVoucherBy" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblVoucherBy_Id" runat="server" CssClass="lblName" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Broker:
                        </td>
                        <td colspan="4" align="left">
                            <asp:TextBox ID="txtBroker" runat="server" CssClass="txt" Width="100px" Style="text-align: right;"
                                AutoPostBack="false" OnTextChanged="txtBroker_TextChanged" TabIndex="20" Height="24px" onkeydown="broker(event);"></asp:TextBox>
                            <%--<ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBroker" FilterType="Numbers"  TargetControlID="txtBroker"></ajax1:FilteredTextBoxExtender>--%>
                            <asp:Button ID="btnBroker" runat="server" Text="..." OnClick="btnBroker_Click" CssClass="btnHelp"
                                Height="24px" Width="20px" />
                            <asp:Label ID="lblBroker" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblBroker_Id" runat="server" CssClass="lblName"></asp:Label>&nbsp;&nbsp;Brokrage:<asp:TextBox
                                runat="server" ID="txtBrokrage" CssClass="txt" Width="80px" Height="24px" TabIndex="21" />
                            &nbsp;&nbsp; GST Rate Code
                       <asp:TextBox ID="txtGstrateCode" runat="server" CssClass="txt" Width="100px" Style="text-align: right;"
                           AutoPostBack="false" OnTextChanged="txtGstrateCode_TextChanged" TabIndex="22" Height="24px" onkeydown="gstRateCode(event);"></asp:TextBox>
                            <asp:Button ID="btnGstrateCode" runat="server" Text="..." CssClass="btnHelp" OnClick="btnGstrateCode_Click"
                                Height="24px" Width="20px" />
                            <asp:Label ID="lblgstrateCode" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblgstID" runat="server" CssClass="lblName" Visible="false"></asp:Label>

                            Excise / GST Rate:
                       
                            <asp:TextBox ID="txtExciseRate" runat="server" CssClass="txt" TabIndex="23" Width="100px"
                                Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtExciseRate_TextChanged"
                                Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtExciseRate" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtExciseRate">
                            </ajax1:FilteredTextBoxExtender>
                            &nbsp;GST Rate:<asp:Label Text="" runat="server" ID="lblMillRateGst" ForeColor="Yellow" />&nbsp;&nbsp;&nbsp;&nbsp;
                         Value:<asp:Label Text="" runat="server" ID="lblValue" ForeColor="Yellow" />
                            Sell Note No:<asp:TextBox runat="server" ID="txtSellNoteNo" Width="150px" Height="24px"
                                TabIndex="24" Style="text-align: right;" CssClass="txt" OnTextChanged="txtSellNoteNo_TextChanged"></asp:TextBox>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Narration:
                        </td>
                        <td colspan="4" align="left">

                            <asp:TextBox ID="txtNarration" runat="server" CssClass="txt" TabIndex="25" AutoPostBack="True"
                                Width="250px" OnTextChanged="txtNarration_TextChanged" Height="24px"></asp:TextBox>
                            TCS%:
                            <asp:TextBox ID="txtTCSRate" runat="Server" CssClass="txt" Width="50px" AutoPostBack="true"
                                OnTextChanged="txtTCSRate_TextChanged" TabIndex="26" Style="text-align: right;"
                                Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtTCSRate">
                            </ajax1:FilteredTextBoxExtender>
                            TCS Amount:
                            <asp:TextBox ID="txtTCSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                OnTextChanged="txtTCSAmt_TextChanged" ReadOnly="true" Style="text-align: right;"
                                Width="82px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtTCSAmt" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Value with TCSAmt:<asp:Label Text="" runat="server" ID="lblvaluewithtcsamt" ForeColor="Yellow" />

                            TDS%:
                            <asp:TextBox ID="txtTdsrate" runat="Server" CssClass="txt" Width="50px" AutoPostBack="true"
                                OnTextChanged="txtTdsrate_TextChanged" TabIndex="27" Style="text-align: right;"
                                Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtTdsrate">
                            </ajax1:FilteredTextBoxExtender>
                            TDS Amount:
                            <asp:TextBox ID="txtTdsamount" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                OnTextChanged="txtTdsamount_TextChanged" ReadOnly="true" Style="text-align: right;"
                                Width="82px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtTdsamount" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>

                            VTCS Rate:
                            <asp:TextBox ID="txtvtcsrate" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                OnTextChanged="txtvtcsrate_TextChanged" ReadOnly="true" Style="text-align: right;"
                                Width="82px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="Filteredtxtvtcsrate" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtvtcsrate" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>

                            VTDS Rate:
                            <asp:TextBox ID="txtvtdsrate" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                OnTextChanged="txtvtdsrate_TextChanged" ReadOnly="true" Style="text-align: right;"
                                Width="82px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="Filteredtxtvtdsrate" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtvtdsrate" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>

                        </td>
                    </tr>
                </table>

                <fieldset style="border-top: 3px dotted rgb(131, 127, 130); border-radius: 3px; width: 172%; margin-left: -1160px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 5px;">
                    <legend style="text-align: left;">
                        <h2 style="color: Black; text-align: left;" font-names="verdana" font-size="large">Tender Detail Section</h2>
                    </legend>
                </fieldset>
                <asp:Panel ID="Panel2" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                    Font-Size="Small" Style="margin-top: 0px; z-index: 100;">

                    <table width="100%" cellpadding="4px" cellspacing="4px">
                        <tr>
                            <td align="left">ID:
                           
                                <asp:Label ID="lblID" runat="server"></asp:Label>
                                <asp:Label ID="lblno" runat="server" ForeColor="Azure"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Bill To:
                           
                                <asp:TextBox ID="txtBuyer" runat="server" Width="80px" Height="24px" AutoPostBack="false"
                                    CssClass="txt" OnTextChanged="txtBuyer_TextChanged" onkeydown="Party(event);" TabIndex="28"></asp:TextBox>
                                <%--<ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyer" FilterType="Numbers" TargetControlID="txtBuyer"></ajax1:FilteredTextBoxExtender>--%>
                                <asp:Button ID="btnBuyer" runat="server" Text="..." Height="24px" Width="20px" CssClass="btnHelp"
                                    OnClick="btnBuyer_Click" />
                                <asp:Label ID="lblBuyerName" runat="server" CssClass="lblName"></asp:Label>
                                <asp:Label ID="lblbuyer_id" runat="server" Visible="false"></asp:Label>
                                Ship To:
                                 <asp:TextBox ID="txtShipTo" runat="server" Width="80px" Height="24px" AutoPostBack="false"
                                     CssClass="txt" OnTextChanged="txtShipTo_TextChanged" onkeydown="Shipto(event);" TabIndex="29"></asp:TextBox>
                                <%--<ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyer" FilterType="Numbers" TargetControlID="txtBuyer"></ajax1:FilteredTextBoxExtender>--%>
                                <asp:Button ID="btntxtShipTo" runat="server" Text="..." Height="24px" Width="20px" CssClass="btnHelp"
                                    OnClick="btntxtShipTo_Click" />
                                <asp:Label ID="lblShiptoname" runat="server" CssClass="lblName"></asp:Label>
                                <asp:Label ID="lblshiptoid" runat="server" Visible="false"></asp:Label>
                                <asp:RequiredFieldValidator ID="rfvtxtBuyer" runat="server" ControlToValidate="txtBuyer"
                                    CssClass="validator" Display="Dynamic" ErrorMessage="Required" SetFocusOnError="true"
                                    Text="Required" ValidationGroup="addBuyerDetails">
                                </asp:RequiredFieldValidator>
                                Delivery Type:
                                   <asp:DropDownList ID="drpDeliveryType" runat="server" CssClass="ddl" Width="240px"
                                       TabIndex="30" AutoPostBack="true" Height="26px" OnSelectedIndexChanged="drpDeliveryType_SelectedIndexChanged">
                                       <asp:ListItem Text="With GST Naka Delivery" Value="N" Selected="True"></asp:ListItem>
                                       <asp:ListItem Text="Naka Delivery without GST Rate" Value="A"></asp:ListItem>
                                       <asp:ListItem Text="Commission" Value="C"></asp:ListItem>
                                       <asp:ListItem Text="DO" Value="D"></asp:ListItem>
                                   </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td align="left">Broker:
                           
                                <asp:TextBox ID="txtBuyerParty" runat="server" Width="80px" CssClass="txt" Height="24px"
                                    OnTextChanged="txtBuyerParty_TextChanged" AutoPostBack="false" onkeydown="detailBroker(event);" TabIndex="31"></asp:TextBox>
                                <%--<ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyerParty" FilterType="Numbers" TargetControlID="txtBuyerParty"></ajax1:FilteredTextBoxExtender>--%>
                                <asp:Button ID="btnBuyerParty" Height="24px" Width="20px" runat="server" Text="..."
                                    CssClass="btnHelp" OnClick="btnBuyerParty_Click" />
                                <asp:Label ID="lblBuyerPartyName" runat="server" CssClass="lblName"></asp:Label>
                                <asp:Label ID="lblbuyerparty_id" runat="server" Visible="false"></asp:Label>


                                Sub Broker:
                                  <asp:TextBox ID="txtsubBroker" runat="server" Width="80px" CssClass="txt" Height="24px"
                                      OnTextChanged="txtsubBroker_TextChanged" AutoPostBack="false" onkeydown="subBroker(event);" TabIndex="32"></asp:TextBox>
                                <%--<ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyerParty" FilterType="Numbers" TargetControlID="txtBuyerParty"></ajax1:FilteredTextBoxExtender>--%>
                                <asp:Button ID="btnsubBrker" Height="24px" Width="20px" runat="server" Text="..."
                                    CssClass="btnHelp" OnClick="btnsubBrker_Click" />
                                <asp:Label ID="lblsubBroker" runat="server" CssClass="lblName"></asp:Label>
                                <asp:Label ID="lblsubId" runat="server" Visible="false"></asp:Label>


                                Buyer Quantal:
                                

                                <asp:TextBox ID="txtBuyerQuantal" runat="server" Width="80px" Height="24px" CssClass="txt"
                                    AutoPostBack="true" OnTextChanged="txtBuyerQuantal_TextChanged" TabIndex="33"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyerQuantal" FilterType="Custom,Numbers"
                                    ValidChars="." TargetControlID="txtBuyerQuantal">
                                </ajax1:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="rfvtxtBuyerQuantal" runat="server" ControlToValidate="txtBuyerQuantal"
                                    CssClass="validator" Display="Dynamic" ErrorMessage="Required" SetFocusOnError="true"
                                    Text="Required" ValidationGroup="addBuyerDetails">
                                </asp:RequiredFieldValidator>

                                Sale Rate:
                           
                                <asp:TextBox ID="txtBuyerSaleRate" runat="server" Width="80px" Height="24px" CssClass="txt"
                                    AutoPostBack="true" OnTextChanged="txtBuyerSaleRate_TextChanged" TabIndex="34"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyerSaleRate" FilterType="Custom,Numbers"
                                    ValidChars="." TargetControlID="txtBuyerSaleRate">
                                </ajax1:FilteredTextBoxExtender>
                               
                              
                                 Commission:
                          
                                <asp:TextBox ID="txtBuyerCommission" runat="server" CssClass="txt" Height="24px"
                                    TabIndex="35" Width="80px" AutoPostBack="true" OnTextChanged="txtBuyerCommission_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyerCommission" FilterType="Custom,Numbers"
                                    ValidChars="." TargetControlID="txtBuyerCommission">
                                </ajax1:FilteredTextBoxExtender>
                                  BP Account Detail: &nbsp;
                            <asp:TextBox ID="txtBP_Account_Detail" runat="server" CssClass="txt" Width="100px" Style="text-align: right;"
                                AutoPostBack="false" OnTextChanged="txtBP_Account_Detail_TextChanged" TabIndex="36" Height="24px" onkeydown="BPAccountDetail(event);"></asp:TextBox>
                                <asp:Button ID="btnBP_Account_Detail" runat="server" Text="..." CssClass="btnHelp" OnClick="btnBP_Account_Detail_Click"
                                    Height="24px" Width="20px" />
                                <asp:Label ID="lblBP_Account_Detail" runat="server" CssClass="lblName"></asp:Label>

                                <asp:RequiredFieldValidator ID="rfvtxtBuyerSaleRate" runat="server" ControlToValidate="txtBuyerSaleRate"
                                    CssClass="validator" Display="Dynamic" ErrorMessage="Required" SetFocusOnError="true"
                                    Text="Required" ValidationGroup="addBuyerDetails">
                                </asp:RequiredFieldValidator>
                                <asp:Label ID="lblcashdifferencevalue" runat="server" ForeColor="Yellow"></asp:Label>
                                 B.P:
                                <asp:TextBox ID="txtcashdifference" runat="server" Width="80px" Height="24px" CssClass="txt"
                                    AutoPostBack="true" OnTextChanged="txtcashdifference_TextChanged" TabIndex="37"></asp:TextBox>

                            </td>
                        </tr>

                        <tr>
                            <td align="left">Sauda Date:
                          
                                <asp:TextBox ID="txtDetailSaudaDate" runat="server" CssClass="txt" Width="100px"
                                    AutoPostBack="True" MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)"
                                    onkeydown="return DateFormat(this,event.keyCode)" OnTextChanged="txtDetailSaudaDate_TextChanged"
                                    TabIndex="38" Height="24px"></asp:TextBox>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                    Height="15px" />
                                <ajax1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDetailSaudaDate"
                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>

                                Payment Date:
                           
                                <asp:TextBox ID="txtDetailLiftingDate" runat="server" CssClass="txt" Width="100px"
                                    AutoPostBack="True" MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)"
                                    onkeydown="return DateFormat(this,event.keyCode)" OnTextChanged="txtDetailLiftingDate_TextChanged"
                                    TabIndex="39" Height="24px"></asp:TextBox>
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                    Height="15px" />
                                <ajax1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDetailLiftingDate"
                                    PopupButtonID="Image2" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>

                                Narration:
                           
                                <asp:TextBox ID="txtBuyerNarration" runat="server" Width="360px" CssClass="txt" Height="50px"
                                    TextMode="MultiLine" TabIndex="40" AutoPostBack="true" OnTextChanged="txtBuyerNarration_TextChanged"></asp:TextBox>

                                <%--<td colspan="2" align="center">--%>
                                Loading By Us:<asp:CheckBox runat="server"
                                    TabIndex="0" ID="chkLoding_Chk" Width="10px" Height="10px" />
                               
                            </td>
                        </tr>
                        <tr>
                            <td>GST Amount
                           
                                <asp:TextBox ID="txtGSTrate" runat="server" Width="60px" CssClass="txt" Height="24px"
                                    TabIndex="41" AutoPostBack="true" OnTextChanged="txtGSTrate_TextChanged"></asp:TextBox>

                                <asp:TextBox ID="txtgstamt" runat="server" Width="100px" CssClass="txt" Height="24px"
                                    TabIndex="42" AutoPostBack="true"></asp:TextBox>
                                TCS Amount
                           
                                <asp:TextBox ID="txtTCSrate1" runat="server" Width="60px" CssClass="txt" Height="24px"
                                    TabIndex="43" AutoPostBack="true" OnTextChanged="txtTCSrate1_TextChanged"></asp:TextBox>

                                <asp:TextBox ID="txtTCSamount1" runat="server" Width="100px" CssClass="txt" Height="24px"
                                    TabIndex="44" AutoPostBack="true"></asp:TextBox>

                                Net Amount:
                                <asp:Label ID="lblNetAmt" runat="server" ForeColor="Yellow"></asp:Label>
                                <asp:Button ID="btnADDBuyerDetails" runat="server" Text="ADD" CssClass="btnHelp"
                                    Font-Bold="false" OnClick="btnADDBuyerDetails_Click" Width="90px" Height="24px" ValidationGroup="addBuyerDetails"
                                    TabIndex="45" />
                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btnHelp"
                                    TabIndex="46" Font-Bold="false" CausesValidation="false" Width="90px" Height="24px" />

                                <asp:Label ID="lbltenderdetailid" runat="server" Visible="false"></asp:Label></td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
            <div>
                <asp:UpdatePanel ID="upGrid" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlgrdDetail" runat="server" ScrollBars="Both" Height="220px" Width="1500px"
                            BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true" Font-Names="Verdana"
                            Font-Size="11px" BackColor="SeaShell" Style="margin-left: 30px; float: left;">
                            <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" HeaderStyle-BackColor="#397CBB"
                                HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="112%"
                                Height="65%" OnRowCommand="grdDetail_RowCommand" CellPadding="5" CellSpacing="5"
                                OnRowDataBound="grdDetail_RowDataBound" Style="table-layout: fixed;">
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
            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="90%"
                ScrollBars="Both" align="center" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: left; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 5%; top: 10%;">
                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg"
                    Width="20px" Height="20px" Style="float: right; vertical-align: top;" OnClick="imgBtnClose_Click"
                    ToolTip="Close" />
                <table width="95%">
                    <tr>
                        <td align="center" styles="background-color: #F5B540; width: 100%;">
                            <asp:Label ID="lblPopupHead" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Font-Bold="true" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" align="left" colspan="2">
                            <table id="Table1" runat="server" width="100%">
                                <tr>
                                    <td style="width: 40%;">Search Text:
                                        <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                                            Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                                        &nbsp;<asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server"
                                            Text="Search" CssClass="btnSubmit" OnClick="btnSearch_Click" />
                                    </td>
                                    <td align="left" runat="server" id="tdDate" visible="false">From:
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                                            MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                            Height="15px" />
                                        <ajax1:CalendarExtender ID="calendertxtFromDate" runat="server" TargetControlID="txtFromDate"
                                            PopupButtonID="Image1" Format="dd/MM/yyyy">
                                        </ajax1:CalendarExtender>
                                        To:
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                                            MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                            Height="15px" />
                                        <ajax1:CalendarExtender ID="CalendarExtendertxtToDate" runat="server" TargetControlID="txtToDate"
                                            PopupButtonID="Image2" Format="dd/MM/yyyy">
                                        </ajax1:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 500px">
                                <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    AllowPaging="true" PageSize="15" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                    OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
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

            <%-- <asp:Panel ID="pnlPopupTenderDetails" runat="server" BackColor="GhostWhite" Width="800px"
                BorderColor="Teal" BorderWidth="1px" Height="430px" BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 10%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
            </asp:Panel>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
