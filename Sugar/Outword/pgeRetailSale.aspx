<%@ Page Title="RetailSale" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeRetailSale.aspx.cs" Inherits="pgeRetailSale" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <script type="text/javascript" language="javascript">
        function Confirm() {
            var Envoiceno= document.getElementById("<%=txtEnvoiceno.ClientID %>").value
            var ACK=document.getElementById("<%=txtACK.ClientID %>").value
            var EwayBillNo = document.getElementById("<%=txtEwayBillNo.ClientID %>").value
            if (Envoiceno == "" || EwayBillNo == "") {
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
            else {
                alert('Einvoice Already generated!!!!!');
                $("#loader").hide();
                return false;
            }
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
                if (hdnfClosePopupValue == "txtParty_Code") {
                    document.getElementById("<%= txtParty_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblParty_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtParty_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBroker_Code") {
                    document.getElementById("<%= txtBroker_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblbrokername.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBroker_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtItem_Code") {
                    document.getElementById("<%= txtItem_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblItemname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtItem_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBrand_Code") {
                    document.getElementById("<%= txtBrand_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblBrandname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%= txtWtper.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%=txtBrand_Code.ClientID %>").focus();

                }
                if (hdnfClosePopupValue == "txtGST_Code") {
                    document.getElementById("<%= txtGST_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblgstname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%=txtGST_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtParty_name_new") {

                    document.getElementById("<%=txtParty_name_new.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtParty_name_new.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtParty_name_new.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtmillcode") {

                    document.getElementById("<%=txtmillcode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblmill.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtmillcode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtShipto") {

                    document.getElementById("<%=txtShipto.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblshipto.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtShipto.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurc_No") {

                    document.getElementById("<%=txtPurc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=hdnfpurcno.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtmillcode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%=lblmill.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    document.getElementById("<%=txtItem_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[4].innerText;
                    document.getElementById("<%=lblItemname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[5].innerText;
                    document.getElementById("<%=txtBrand_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[6].innerText;
                    document.getElementById("<%=lblBrandname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[7].innerText;
                    //document.getElementById("<%=txtQty.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[8].innerText;
                    document.getElementById("<%=txtGST_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[15].innerText;
                    document.getElementById("<%=lblgstname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[16].innerText;
                    document.getElementById("<%=txtWtper.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[17].innerText;
                    document.getElementById("<%=hdnfincludinggst.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[18].innerText;
                    document.getElementById("<%=hdnfpurcid.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[14].innerText;

                    document.getElementById("<%=txtQty.ClientID %>").focus();
                    __doPostBack("txtItem_Code", "TextChanged");

                }
                if (hdnfClosePopupValue == "txtTransportCode") {
                    document.getElementById("<%=txtTransportCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblTransportName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtTransportCode.ClientID %>").focus();
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

                if (hdnfClosePopupValue == "txtParty_Code") {

                    document.getElementById("<%=txtParty_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtShipto") {

                    document.getElementById("<%=txtShipto.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBroker_Code") {

                    document.getElementById("<%=txtBroker_Code.ClientID %>").focus();
                } if (hdnfClosePopupValue == "txtParty_name_new") {

                    document.getElementById("<%=txtParty_name_new.ClientID %>").focus();
                } if (hdnfClosePopupValue == "txtItem_Code") {

                    document.getElementById("<%=txtItem_Code.ClientID %>").focus();
                } if (hdnfClosePopupValue == "txtBrand_Code") {

                    document.getElementById("<%=txtBrand_Code.ClientID %>").focus();
                } if (hdnfClosePopupValue == "txtGST_Code") {

                    document.getElementById("<%=txtGST_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTransportCode") {
                    document.getElementById("<%=txtTransportCode.ClientID %>").focus();
                }
            }
        });

    </script>

    <script type="text/javascript" language="javascript">

        function SB(billno) {
            var billno = document.getElementById('<%=txtDoc_No.ClientID %>').value;
            var partycode = document.getElementById('<%=txtParty_Code.ClientID %>').value;

            window.open('../Report/rptRetailsale.aspx?billno=' + billno + '&partycode=' + partycode)

        }
        function EInovice() {
            debugger;
            var dono = document.getElementById('<%=txtDoc_No.ClientID %>').value;
            var type = document.getElementById('<%=drpCashCredit.ClientID %>').value;
            window.open('../Utility/pgeEInovice.aspx?dono=' + dono + '&Type=RR&trntype=' + type, "_self");
        }
        function GEway() {
            var dono = document.getElementById('<%=txtDoc_No.ClientID %>').value;
            var type = document.getElementById('<%=drpCashCredit.ClientID %>').value;
            window.open('../Utility/pgeEwayBill.aspx?dono=' + dono + '&Type=RR&trntype=' + type, "_self");
        }
        function tab(e) {
            if (e.keyCode == 9) {
                e.preventDefault();

                $("#<%=btnSave.ClientID %> ").focus();
            }
        }
        function chanegno(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtEditDoc_No";
                $("#<%=hdnfClosePopup.ClientID %> ").val(edi);
                $("#<%=btnSearch.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtEditDoc_No", "TextChanged");
            }
        }
        function Doc_No(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtDoc_No.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtDoc_No", "TextChanged");
            }
        }
        function Party_Code(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtParty_Code.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtParty_Code", "TextChanged");
            }
        }
        function shipto(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtShipto.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var buss = $("#<%=txtShipto.ClientID %>").val();
                buss = "0" + buss;
                $("#<%=txtShipto.ClientID %>").val(buss);
                __doPostBack("txtShipto", "TextChanged");
            }
        }
        function Party_name_new(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtParty_name_new.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var buss = $("#<%=txtParty_name_new.ClientID %>").val();
                buss = "0" + buss;
                $("#<%=txtParty_name_new.ClientID %>").val(buss);
                __doPostBack("txtParty_name_new", "TextChanged");
            }
        }
        function millcode(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btnmillcode.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var buss = $("#<%=txtmillcode.ClientID %>").val();
                buss = "0" + buss;
                $("#<%=txtmillcode.ClientID %>").val(buss);
                __doPostBack("txtmillcode", "TextChanged");
            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %> ").focus();
            }
        }
        function Broker_Code(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                //document.getElementById("<%=txtBroker_Code.ClientID %>").value = "0";
                $("#<%=btntxtBroker_Code.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var buss = $("#<%=txtBroker_Code.ClientID %>").val();
                buss = "0" + buss;
                $("#<%=txtBroker_Code.ClientID %>").val(buss);
                __doPostBack("txtBroker_Code", "TextChanged");
            }
        }
        function Item_Code(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtItem_Code.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtItem_Code", "TextChanged");
            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=txtTCS_Rate.ClientID %>").focus();
            }
        }
        function tcs(e) {
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
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
        function GST_Code(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtGST_Code.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtGST_Code", "TextChanged");
            }

        }
        function Back() {
            window.open('../Outword/PgeRetailUtility.aspx', '_self');
        }
        function rate(e) {
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnAdddetails.ClientID %> ").focus();
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

    </script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function GridCalculations() {
            debugger;
            // Grid Total Calculations
            var netgst = 0.00, totalvalue = 0.00, netvalue = 0.00, Totaltaxable = 0.00, netwt = 0.00, cgstamt1 = 0.00, sgstamt1 = 0.00, Igstamt1 = 0.00, tcsnetpayable = 0.00,
                tcsrate = 0.00, tcsamt = 0.00, tdsamt = 0.00, nethamali = 0.00, roundoff = 0.00, netmarketsess = 0.00, netother = 0.00, netFrieght = 0.00, netpacking = 0.00, netsupercost = 0.00, netExp = 0.00;
            var tcsrate = parseFloat($("#<%=txtTCS_Rate.ClientID %>").val() == "" ? 0 : $("#<%= txtTCS_Rate.ClientID %>").val());
            var tdsrate = parseFloat($("#<%=txtTDS.ClientID %>").val() == "" ? 0 : $("#<%= txtTDS.ClientID %>").val());

            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            var grdrow = gridView.getElementsByTagName("tr");
            var ddid = 1;
            var Order_Code = 1;
            var balance = 0.00;
            var balancelimt = 0.00;
            balance = document.getElementById("<%= hdnfbalance.ClientID %>").value;
            balancelimt = '<%= Session["balancelimit"] %>';
            for (var i = 1; i < grdrow.length; i++) {

                if (gridView.rows[i].cells[30].innerHTML != "D") {
                    netgst = netgst + parseFloat(gridView.rows[i].cells[28].innerHTML);
                    totalvalue = totalvalue + parseFloat(gridView.rows[i].cells[12].innerHTML);
                    netvalue = netvalue + parseFloat(gridView.rows[i].cells[29].innerHTML);
                    Totaltaxable = Totaltaxable + parseFloat(gridView.rows[i].cells[19].innerHTML);
                    netwt = netwt + parseFloat(gridView.rows[i].cells[8].innerHTML);
                    //netgst = netgst + parseFloat(gridView.rows[i].cells[28].innerHTML);
                    cgstamt1 = cgstamt1 + parseFloat(gridView.rows[i].cells[23].innerHTML);
                    sgstamt1 = sgstamt1 + parseFloat(gridView.rows[i].cells[25].innerHTML);
                    Igstamt1 = Igstamt1 + parseFloat(gridView.rows[i].cells[27].innerHTML);
                    netpacking = netpacking + parseFloat(gridView.rows[i].cells[15].innerHTML);
                    netother = netother + parseFloat(gridView.rows[i].cells[18].innerHTML);
                    netmarketsess = netmarketsess + parseFloat(gridView.rows[i].cells[13].innerHTML);

                    netsupercost = netsupercost + parseFloat(gridView.rows[i].cells[14].innerHTML);
                    netFrieght = netFrieght + parseFloat(gridView.rows[i].cells[17].innerHTML);
                    nethamali = nethamali + parseFloat(gridView.rows[i].cells[16].innerHTML);

                }
            }
            if (tcsrate != 0) {
                tcsamt = Math.round(((Totaltaxable + cgstamt1 + sgstamt1 + Igstamt1) / 100) * tcsrate);
            }
            if (tdsrate != 0) {
                tdsamt = Math.round(((Totaltaxable) / 100) * tdsrate);
            }


            if (balance < balancelimt) {
                tcsrate = 0.00;
                tdsrate = 0.00;

            }
            tcsnetpayable = Totaltaxable + tcsamt;

            //Assign Values to Fields
            document.getElementById("<%=txtNetGST.ClientID %>").value = netgst;
            document.getElementById("<%=txtTotal.ClientID %>").value = totalvalue;
            document.getElementById("<%=txtItemvalue.ClientID %>").value = totalvalue;

            document.getElementById("<%=txtTaxable_Amount.ClientID %>").value = Totaltaxable;
            document.getElementById("<%=txtNetWeight.ClientID %>").value = netwt;
            document.getElementById("<%=txtTCS_Amount.ClientID %>").value = tcsamt;
            document.getElementById("<%=txtTDSAmt.ClientID %>").value = tdsamt;

            document.getElementById("<%=txtTotalIGST.ClientID %>").value = Igstamt1;
            document.getElementById("<%=txtTotalSGST.ClientID %>").value = sgstamt1;

            document.getElementById("<%=txtTotalCGST.ClientID %>").value = cgstamt1;

            document.getElementById("<%=txtNetMarketsess.ClientID %>").value = netmarketsess;
            document.getElementById("<%=txtNetHamali.ClientID %>").value = nethamali;
            document.getElementById("<%=txtNetFrieght.ClientID %>").value = netFrieght;
            document.getElementById("<%=txtNetSuperCost.ClientID %>").value = netsupercost;
            document.getElementById("<%=txtNetpacking.ClientID %>").value = netpacking;


            var taxableround = Totaltaxable + cgstamt1 + sgstamt1 + Igstamt1;
            var result = parseInt(taxableround);
            var b = parseFloat((Totaltaxable + cgstamt1 + sgstamt1 + Igstamt1) - result);
            var c = 0.00;
            if (b < 0) {
                c = 1 + b;
            }
            else {
                c = 1 - b;
            }
            roundoff = c;
            document.getElementById("<%=txtNetValue.ClientID %>").value = Math.round((netvalue + roundoff + +Number.EPSILON) * 100) / 100;
            document.getElementById("<%=txtRoundoff.ClientID %>").value = Math.round((roundoff + Number.EPSILON) * 100) / 100;
            document.getElementById("<%=txtNetOther.ClientID %>").value = netother;
            document.getElementById("<%=txtNetPayble.ClientID %>").value = Totaltaxable + cgstamt1 + sgstamt1 + Igstamt1 + tcsamt + roundoff;
            document.getElementById("<%=hdnfnetPayble.ClientID %>").value = Totaltaxable + cgstamt1 + sgstamt1 + Igstamt1 + tcsamt + roundoff;
            document.getElementById("<%=txtNetExp.ClientID %>").value = netFrieght + nethamali + netmarketsess + netsupercost + netother + netpacking;

        }
        function Calculations(e) {

            if (e.keyCode == 9 || e == 9) {
                debugger;
                //declare Require Fields And Assign Values also Check CsCalculation Method In BackEnd
                var rateper = document.getElementById("<%= hdnfrateper.ClientID %>").value;
                var includeGST = document.getElementById("<%= hdnfincludinggst.ClientID %>").value;
                var partystatecode = parseInt(document.getElementById("<%= hdnfpartystatecode.ClientID %>").value);
                var companystatecode = parseInt('<%= Session["CompanyGSTStateCode"] %>');

                var hamali = parseFloat($("#<%=txtHamali.ClientID %>").val() == "" ? 0 : $("#<%= txtHamali.ClientID %>").val());
                var other = parseFloat($("#<%=txtOther.ClientID %>").val() == "" ? 0 : $("#<%= txtOther.ClientID %>").val());
                var packing = parseFloat($("#<%=txtPacking.ClientID %>").val() == "" ? 0 : $("#<%= txtPacking.ClientID %>").val());

                var freight = parseFloat($("#<%=txtFreight.ClientID %>").val() == "" ? 0 : $("#<%= txtFreight.ClientID %>").val());
                var Qty = $("#<%=txtQty.ClientID %>").val() == "" ? 0 : $("#<%= txtQty.ClientID %>").val();
                var Wtper = $("#<%=txtWtper.ClientID %>").val() == "" ? 0 : $("#<%= txtWtper.ClientID %>").val();
                var Netkg = parseFloat(Qty * Wtper);

                var salerate = 0, Rate = 0, Values = 0, itemvalue = 0;
                var Market_Cess = 0, Super_Cost = 0, Super_Cost = 0, Taxable_Amount = 0, GST = 0;
                var cgstrate = 0, sgstrate = 0, igstrate = 0, cgstamt = 0, sgstamt = 0, igstamt = 0;

                Rate = parseFloat($("#<%=txtRate.ClientID %>").val() == "" ? 0 : $("#<%= txtRate.ClientID %>").val());
                var rate1 = parseFloat($("#<%=txtRate.ClientID %>").val() == "" ? 0 : $("#<%= txtRate.ClientID %>").val());
                salerate = rate1;

                var GSTRATE = parseFloat($("#<%=lblgstname.ClientID %>").text() == "" ? 0 : $("#<%=lblgstname.ClientID %>").text());

                //Check Value And Calculate saleRate

                //Math.round((Rate + Number.EPSILON) * 100) / 100; is Used For Calculating RoundOff 

                if (includeGST == "Y") {
                    debugger;
                    var a = Rate * (GSTRATE / 100);
                    a = a + Rate;
                    a = Rate / a;
                    Rate = a * Rate;
                    Rate = Math.round((Rate + Number.EPSILON) * 100) / 100;
                    salerate = Rate;
                    var gstvalue = ((rate1 / 100) * Netkg);
                    var basevalue = gstvalue;
                    gstvalue = gstvalue+( (gstvalue * GSTRATE) / 100);
                    Values = basevalue / gstvalue;
                    Values = Math.round((Values * basevalue + Number.EPSILON) * 100) / 100;



                }
                if (rateper == "Q") {
                    Values = Math.round((Qty * Rate + Number.EPSILON) * 100) / 100;
                    itemvalue = Math.round(((Netkg / 100) * rate1 + Number.EPSILON) * 100) / 100;

                }
                else {
                   // Values = Math.round(((Netkg / 100) * Rate + Number.EPSILON) * 100) / 100;
                    itemvalue = Math.round(((Netkg / 100) * rate1 + Number.EPSILON) * 100) / 100;

                }

                //Check MarketSess Value And Get the MarketSess RoundOff
                if ($("#<%=txtMarket_Cess.ClientID %>").val() == "" || $("#<%=txtMarket_Cess.ClientID %>").val() == "0") {
                    Market_Cess = 0.00;
                }
                else {
                    var hdnfmarket = parseFloat(document.getElementById("<%= hdnfmarketsessrate.ClientID %>").value);

                    Market_Cess = Math.round(((Values * hdnfmarket + Number.EPSILON) / 100) * 100) / 100;
                }

                if ($("#<%=txtSuper_Cost.ClientID %>").val() == "" || $("#<%=txtSuper_Cost.ClientID %>").val() == "0") {
                    Super_Cost = 0.00;
                }
                else {
                    var hdnfsuper = parseFloat(document.getElementById("<%= hdnfsupercostrate.ClientID %>").value);

                    Super_Cost = Math.round(((Values * hdnfsuper + Number.EPSILON) / 100) * 100) / 100;
                }
                var am = Values + Market_Cess + Super_Cost + packing + hamali + freight + other;
                Taxable_Amount = Math.round((am + Number.EPSILON) * 100) / 100;

                // Check Company And Party StateCode And Calculate Gst Amt.
                if (companystatecode == partystatecode) {
                    cgstrate = GSTRATE / 2;
                    sgstrate = GSTRATE / 2;

                    cgstamt = Math.round(((Taxable_Amount * cgstrate) / 100 + Number.EPSILON) * 100) / 100;
                    sgstamt = Math.round(((Taxable_Amount * sgstrate) / 100 + Number.EPSILON) * 100) / 100;
                }
                else {
                    igstrate = GSTRATE;
                    igstamt = Math.round(((Taxable_Amount * igstrate) / 100 + Number.EPSILON) * 100) / 100;
                }

                GST = cgstamt + sgstamt + igstamt;

                // Finally Assign Values To TextBox.
                //document.getElementById("<%=txtTaxable_Amount.ClientID %>").value = Taxable_Amount;
                document.getElementById("<%=txtNetkg.ClientID %>").value = Netkg;
                var c = itemvalue - (cgstamt + sgstamt + igstamt);
                document.getElementById("<%=txtValue.ClientID %>").value = itemvalue - (cgstamt + sgstamt + igstamt);
                debugger;
                Taxable_Amount = itemvalue - (cgstamt + sgstamt + igstamt) + Market_Cess + Super_Cost + packing + hamali + freight + other;
                Taxable_Amount = Math.round((Taxable_Amount + Number.EPSILON) * 100) / 100;

                var net_val = Math.round(((Taxable_Amount + GST) + Number.EPSILON) * 100) / 100;
                var value = document.getElementById("<%=txtValue.ClientID %>").value

                if (companystatecode == partystatecode) {
                    cgstrate = GSTRATE / 2;
                    sgstrate = GSTRATE / 2;

                    cgstamt = Math.round(((value * cgstrate) / 100 + Number.EPSILON) * 100) / 100;
                    sgstamt = Math.round(((value * sgstrate) / 100 + Number.EPSILON) * 100) / 100;
                }
                else {
                    igstrate = GSTRATE;
                    igstamt = Math.round(((value * igstrate) / 100 + Number.EPSILON) * 100) / 100;
                }

                document.getElementById("<%=txtRate.ClientID %>").value = rate1;
                document.getElementById("<%=txtsalerate.ClientID %>").value = salerate;
                document.getElementById("<%=txtMarket_Cess.ClientID %>").value = Market_Cess;
                document.getElementById("<%=txtSuper_Cost.ClientID %>").value = Super_Cost;
                document.getElementById("<%=txtTaxable_Amount1.ClientID %>").value = Taxable_Amount;
                document.getElementById("<%=txtGST.ClientID %>").value = GST;
                document.getElementById("<%=txtNet_Value.ClientID %>").value = net_val;
                document.getElementById("<%=txtCGST_Rate.ClientID %>").value = cgstrate;
                document.getElementById("<%=txtCGST_Amount.ClientID %>").value = cgstamt;
                document.getElementById("<%=txtSGST_Rate.ClientID %>").value = sgstrate;
                document.getElementById("<%=txtSGST_Amount.ClientID %>").value = sgstamt;

                document.getElementById("<%=txtIGST_Rate.ClientID %>").value = igstrate;
                document.getElementById("<%=txtIGST_Amount.ClientID %>").value = igstamt;

            }


        }
        function DeleteConform() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= hdnfdoc.ClientID %>").value;
            var Autoid = document.getElementById("<%= hdnfid.ClientID %>").value;
            var CashCredit = $("#<%=drpCashCredit.ClientID %>").val();
            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';

            var XML = "<ROOT><RetailHead Doc_No='" + DocNo + "' Retailid='" + Autoid + "' Company_Code='" + Company_Code + "' " +
                   "Year_Code='" + Year_Code + "' CashCredit='" + CashCredit + "'></RetailHead></ROOT>";
            var spname = "RetailSale";
            var status = document.getElementById("<%= btnDelete.ClientID %>").value;
            ProcessXML(XML, status, spname, CashCredit);
        }
        function Validate() {
            $("#loader").show();
            debugger;
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
            if ($("#<%=txtmillcode.ClientID %>").val() != "") {
                alert('Please add details');
                $("#<%=txtmillcode.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtParty_Code.ClientID %>").val() == "" || $("#<%=txtParty_Code.ClientID %>").val() == "0") {
                $("#<%=txtParty_Code.ClientID %>").focus();
                $("#loader").hide();
                return false;

            }
            if ($("#<%=txtShipto.ClientID %>").val() == "" || $("#<%=txtShipto.ClientID %>").val() == "0") {
                $("#<%=txtShipto.ClientID %>").focus();
                $("#loader").hide();
                return false;

            }
            if ($("#<%=txtDue_date.ClientID %>").val() == "" || $("#<%=txtDue_date.ClientID %>").val() == "0") {
                document.getElementById("<%= txtDue_date.ClientID %>").value = DocDates;

            }
            if ($("#<%=txtTaxable_Amount.ClientID %>").val() == "" || $("#<%=txtTaxable_Amount.ClientID %>").val() == "0") {
                $("#<%=txtTaxable_Amount.ClientID %>").focus();
                $("#loader").hide();
                return false;

            }
            return true;
        }
        function pagevalidation() {
            debugger;
            try {
                $("#loader").show();

                var Doc_No = 0, Retailid = 0;
                var status = document.getElementById("<%= btnSave.ClientID %>").value;
                var spname = "RetailSale";
                var XML = "<ROOT>";
                if (status == "Update") {
                    Doc_No = document.getElementById("<%= hdnfdoc.ClientID %>").value;
                    Retailid = document.getElementById("<%= hdnfid.ClientID %>").value;
                }
                var Tran_Type = 'RR';

                var d = $("#<%=txtDoc_Date.ClientID %>").val();
                var Doc_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                var Challan_No = $("#<%=txtChallan_No.ClientID %>").val();
                if (Challan_No == "&nbsp;") {
                    Challan_No = "";
                }
                var d = $("#<%=txtChallan_Date.ClientID %>").val();
                var Challan_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                var Vahical_No = $("#<%=txtVahical_No.ClientID %>").val();
                if (Vahical_No == "&nbsp;") {
                    Vahical_No = "";
                }
                var Party_Code = $("#<%=txtParty_Code.ClientID %>").val() == "" ? 0 : $("#<%=txtParty_Code.ClientID %>").val();
                var Due_Days = $("#<%=txtDue_Days.ClientID %>").val() == "" ? 0 : $("#<%=txtDue_Days.ClientID %>").val();
                var d = $("#<%=txtDue_date.ClientID %>").val();
                var Due_date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                var Total = $("#<%=txtTotal.ClientID %>").val() == "" ? 0 : $("#<%= txtTotal.ClientID %>").val();
                var Subtotal = '0.00';

                var Roundoff = $("#<%=txtRoundoff.ClientID %>").val() == "" ? 0 : $("#<%=txtRoundoff.ClientID %>").val();
                var Grand_total = '0.00';
                var Delivered = $("#<%=drpDelivered.ClientID %>").val();
                var CGST_Rate = '0.00';
                var CGST_Amount = $("#<%=txtTotalCGST.ClientID %>").val() == "" ? 0 : $("#<%= txtTotalCGST.ClientID %>").val();
                var SGST_Rate = '0.00';
                var SGST_Amount = $("#<%=txtTotalSGST.ClientID %>").val() == "" ? 0 : $("#<%= txtTotalSGST.ClientID %>").val();
                var IGST_Rate = '0.00';
                var IGST_Amount = $("#<%=txtTotalIGST.ClientID %>").val() == "" ? 0 : $("#<%= txtTotalIGST.ClientID %>").val();
                var Taxable_Amount = $("#<%=txtTaxable_Amount.ClientID %>").val() == "" ? 0 : $("#<%= txtTaxable_Amount.ClientID %>").val();
                var Party_Name = $("#<%=txtParty_Name.ClientID %>").val();
                if (Party_Name == "&nbsp;") {
                    Party_Name = "";
                }
                var Broker_Code = $("#<%=txtBroker_Code.ClientID %>").val() == "" ? 0 : $("#<%= txtBroker_Code.ClientID %>").val();
                var CashRecieve = $("#<%=drpCashRecieve.ClientID %>").is(":checked");
                var Party_name_new = $("#<%=txtParty_name_new.ClientID %>").val();
                var Narration = $("#<%=txtNarration.ClientID %>").val();
                if (Narration == "&nbsp;") {
                    Narration = "";
                }
                var TCS_Rate = $("#<%=txtTCS_Rate.ClientID %>").val() == "" ? 0 : $("#<%= txtTCS_Rate.ClientID %>").val();
                var TCS_Amount = $("#<%=txtTCS_Amount.ClientID %>").val() == "" ? 0 : $("#<%= txtTCS_Amount.ClientID %>").val();
                //var TCS_Net_Payble = $("#<%=txtTCS_Net_Payble.ClientID %>").val() == "" ? 0 : $("#<%= txtTCS_Net_Payble.ClientID %>").val();
                var TCS_Net_Payble = '0';
                var NewSBNo = $("#<%=txtNewSBNo.ClientID %>").val() == "" ? 0 : $("#<%= txtNewSBNo.ClientID %>").val();
                if (NewSBNo == undefined) {
                    NewSBNo = '0';
                }
                var d = $("#<%=txtDoc_Date.ClientID %>").val();
                var NewSBDate = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                var Envoiceno = $("#<%=txtEnvoiceno.ClientID %>").val();
                var ACK = $("#<%=txtACK.ClientID %>").val();
                debugger;
                var NetGST = $("#<%=txtNetGST.ClientID %>").val() == "" ? 0 : $("#<%= txtNetGST.ClientID %>").val();
                var NetExp = $("#<%=txtNetExp.ClientID %>").val() == "" ? 0 : $("#<%= txtNetExp.ClientID %>").val();
                var NetWeight = $("#<%=txtNetWeight.ClientID %>").val() == "" ? 0 : $("#<%= txtNetWeight.ClientID %>").val();
                var NetValue = $("#<%=txtNetValue.ClientID %>").val() == "" ? 0 : $("#<%= txtNetValue.ClientID %>").val();
                var NetPayble = $("#<%=txtNetPayble.ClientID %>").val() == "" ? 0 : $("#<%= txtNetPayble.ClientID %>").val();
                // NetPayble = document.getElementById("<%= hdnfnetPayble.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfnetPayble.ClientID %>").value;

                var CashCredit = $("#<%=drpCashCredit.ClientID %>").val();
                var Shipto = $("#<%=txtShipto.ClientID %>").val() == "" ? 0 : $("#<%= txtShipto.ClientID %>").val();
                var NetHamali = $("#<%=txtNetHamali.ClientID %>").val() == "" ? 0 : $("#<%= txtNetHamali.ClientID %>").val();
                var NetOther = $("#<%=txtNetOther.ClientID %>").val() == "" ? 0 : $("#<%= txtNetOther.ClientID %>").val();
                var NetPacking = $("#<%=txtNetpacking.ClientID %>").val() == "" ? 0 : $("#<%= txtNetpacking.ClientID %>").val();
                var NetMarketSess = $("#<%=txtNetMarketsess.ClientID %>").val() == "" ? 0 : $("#<%= txtNetMarketsess.ClientID %>").val();
                var NetSuperCost = $("#<%=txtNetSuperCost.ClientID %>").val() == "" ? 0 : $("#<%= txtNetSuperCost.ClientID %>").val();
                var NetFrieght = $("#<%=txtNetFrieght.ClientID %>").val() == "" ? 0 : $("#<%= txtNetFrieght.ClientID %>").val();
                var Item_value = $("#<%=txtItemvalue.ClientID %>").val() == "" ? 0 : $("#<%= txtItemvalue.ClientID %>").val();
                var EwayBillNo = $("#<%=txtEwayBillNo.ClientID %>").val() == "" ? 0 : $("#<%= txtEwayBillNo.ClientID %>").val();



                var pc = document.getElementById("<%= hdnfpc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpc.ClientID %>").value;
                var st = document.getElementById("<%= hdnfst.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfst.ClientID %>").value;
                var bc = document.getElementById("<%= hdnfbc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbc.ClientID %>").value;
                var pcn = document.getElementById("<%= hdnfpcn.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpcn.ClientID %>").value;

                var date = new Date()
                var hours = date.getHours();
                var minutes = date.getMinutes();
                var ampm = hours >= 12 ? 'pm' : 'am';
                hours = hours % 12;
                hours = hours ? hours : 12; // the hour '0' should be '12'
                minutes = minutes < 10 ? '0' + minutes : minutes;
                var strTime = hours + ':' + minutes + ' ' + ampm;

                var transportcode = $("#<%=txtTransportCode.ClientID %>").val() == "" ? 0 : $("#<%= txtTransportCode.ClientID %>").val();
                var tc = document.getElementById("<%= hdnftc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnftc.ClientID %>").value;

                var Branch_Code = '<%= Session["Branch_Code"] %>';
                var Company_Code = '<%= Session["Company_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';
                var USER = '<%= Session["user"] %>';
                var DOCNO = "";
                if (status == "Save") {
                    DOCNO = "Tran_Type='RR'";
                }
                else {
                    DOCNO = "Doc_No='" + Doc_No + "' Tran_Type='RR'";
                }

                var TDS_Rate = $("#<%=txtTDS.ClientID %>").val() == "" ? 0 : $("#<%=txtTDS.ClientID %>").val();
                var TDS_Amt = $("#<%=txtTDSAmt.ClientID %>").val() == "" ? 0 : $("#<%=txtTDSAmt.ClientID %>").val();


                XML = XML + "<RetailHead " + DOCNO + " Doc_Date='" + Doc_Date + "' Challan_No='" + Challan_No + "' Challan_Date='" + Challan_Date + "' Vahical_No='" + Vahical_No + "' " +
                    "Party_Code='" + Party_Code + "' Due_Days='" + Due_Days + "' Due_date='" + Due_date + "' Total='" + Total + "' Subtotal='" + Subtotal + "' Vat='0' Roundoff='" + Roundoff + "' " +
                    "Grand_total='" + Grand_total + "' Delivered='" + Delivered + "' CGST_Rate='" + CGST_Rate + "' CGST_Amount='" + CGST_Amount + "' SGST_Rate='" + SGST_Rate + "' SGST_Amount='" + SGST_Amount + "' " +
                    "IGST_Rate='" + IGST_Rate + "' IGST_Amount='" + IGST_Amount + "' Taxable_Amount='" + Taxable_Amount + "' Party_Name='" + Party_Name + "' Broker_Code='" + Broker_Code + "' " +
                    "CashRecieve='" + CashRecieve + "' Party_name_new='" + Party_name_new + "' Narration='" + Narration + "' TCS_Rate='" + TCS_Rate + "' TCS_Amount='" + TCS_Amount + "' " +
                    "TCS_Net_Payble='" + NetPayble + "' NewSBNo='" + NewSBNo + "' NewSBDate='" + NewSBDate + "' Envoiceno='" + Envoiceno + "' ACK='" + ACK + "' NetGST='" + NetGST + "' " +
                    "NetExp='" + NetExp + "' NetWeight='" + NetWeight + "' NetValue='" + NetValue + "' NetPayble='" + NetPayble + "' Company_Code='" + Company_Code + "' Created_By='" + USER + "' " +
                    "Modified_By='" + USER + "' Created_Date='" + Doc_Date + "' Modified_Date='" + Doc_Date + "' Year_Code='" + Year_Code + "' CashCredit='" + CashCredit + "' NetHamali='" + NetHamali + "' " +
                    "NetOther='" + NetOther + "' NetPacking='" + NetPacking + "' NetMarketSess='" + NetMarketSess + "' NetSuperCost='" + NetSuperCost + "' NetFrieght='" + NetFrieght + "' " +
                    "Shipto='" + Shipto + "' Item_value='" + Item_value + "' EwayBillNo='" + EwayBillNo +
                    "' Retailid='" + Retailid + "' pc='" + pc + "' st='" + st + "' bc='" + bc + "' pcn='" + pcn + "' time='" + strTime +
                    "' Transport_Code='" + transportcode + "' tc='" + tc + "' IsDelete='1' TDS_Rate='" + TDS_Rate + "' TDS_Amt='" + TDS_Amt + "'>";

                var gridView = document.getElementById("<%=grdDetail.ClientID %>");
                var grdrow = gridView.getElementsByTagName("tr");
                var ddid = 1;
                var Order_Code = 1;
                for (var i = 1; i < grdrow.length; i++) {

                    var Detail_Id = gridView.rows[i].cells[2].innerHTML;
                    var Tran_Type = 'RR';
                    var Item_Code = gridView.rows[i].cells[3].innerHTML;
                    var Brand_Code = gridView.rows[i].cells[5].innerHTML;
                    var Qty = gridView.rows[i].cells[7].innerHTML;
                    var Wtper = gridView.rows[i].cells[8].innerHTML;
                    var Netkg = gridView.rows[i].cells[9].innerHTML;
                    var Rate = gridView.rows[i].cells[10].innerHTML;
                    var DSalerate = gridView.rows[i].cells[11].innerHTML;
                    var Value = gridView.rows[i].cells[12].innerHTML;
                    var Market_Cess = gridView.rows[i].cells[13].innerHTML;
                    var Super_Cost = gridView.rows[i].cells[14].innerHTML;
                    var Packing = gridView.rows[i].cells[15].innerHTML;
                    var Hamali = gridView.rows[i].cells[16].innerHTML;
                    var Freight = gridView.rows[i].cells[17].innerHTML;
                    var Other = gridView.rows[i].cells[18].innerHTML;
                    var DTaxable_Amount = gridView.rows[i].cells[19].innerHTML;
                    var GST_Code = gridView.rows[i].cells[20].innerHTML;;
                    var DCGST_Rate = gridView.rows[i].cells[22].innerHTML;
                    var DCGST_Amount = gridView.rows[i].cells[23].innerHTML;
                    var DSGST_Rate = gridView.rows[i].cells[24].innerHTML;
                    var DSGST_Amount = gridView.rows[i].cells[25].innerHTML;
                    var DIGST_Rate = gridView.rows[i].cells[26].innerHTML;
                    var DIGST_Amount = gridView.rows[i].cells[27].innerHTML;
                    var GST = gridView.rows[i].cells[28].innerHTML;
                    var DNet_Value = gridView.rows[i].cells[29].innerHTML;
                    var row = gridView.rows[i].cells[30].innerHTML;
                    var sr = gridView.rows[i].cells[31].innerHTML;

                    var ic = gridView.rows[i].cells[32].innerHTML;
                    var retaildid = gridView.rows[i].cells[33].innerHTML;
                    var saleac = gridView.rows[i].cells[34].innerHTML;
                    var sid = gridView.rows[i].cells[35].innerHTML;
                    debugger;
                    var PurcNo = gridView.rows[i].cells[36].innerHTML;
                    var MillCode = gridView.rows[i].cells[37].innerHTML;
                    var mc = gridView.rows[i].cells[39].innerHTML;
                    var PD_ID = gridView.rows[i].cells[40].innerHTML;



                    if (gridView.rows[i].cells[30].innerHTML == "A") {

                        XML = XML + "<DetailInsert Doc_No='" + Doc_No + "' Detail_Id='" + Detail_Id + "' Tran_Type='RR' Item_Code='" + Item_Code + "' Brand_Code='" + Brand_Code + "' " +
                            "Qty='" + Qty + "' Wtper='" + Wtper + "' Netkg='" + Netkg + "' Rate='" + Rate + "' Value='" + Value + "' Market_Cess='" + Market_Cess + "' Super_Cost='" + Super_Cost + "' " +
                            "Packing='" + Packing + "' Hamali='" + Hamali + "' Freight='" + Freight + "' Other='" + Other + "' Taxable_Amount='" + DTaxable_Amount + "' GST_Code='" + GST_Code + "' " +
                            "GST='" + GST + "' Net_Value='" + DNet_Value + "' Company_Code='" + Company_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' Created_Date='" + Doc_Date + "' " +
                            "Modified_Date='" + Doc_Date + "' Year_Code='" + Year_Code + "' CGST_Rate='" + DCGST_Rate + "' CGST_Amount='" + DCGST_Amount + "' SGST_Rate='" + DSGST_Rate + "' " +
                            "SGST_Amount='" + DSGST_Amount + "' IGST_Rate='" + DIGST_Rate + "' IGST_Amount='" + DIGST_Amount + "' CashCreditType='" + CashCredit + "' Salerate='" + DSalerate + "' " +
                            "Retailid='" + Retailid + "' retaildid='" + retaildid + "' ic='" + ic + "' saleac='" + saleac + "' sid='" + sid + "' PurcNo='" + PurcNo + "' MillCode='" + MillCode + "' mc='" + mc + "' PD_ID='" + PD_ID + "' />";
                    }
                    if (gridView.rows[i].cells[30].innerHTML == "U") {

                        XML = XML + "<Details Doc_No='" + Doc_No + "' Detail_Id='" + Detail_Id + "' Tran_Type='RR' Item_Code='" + Item_Code + "' Brand_Code='" + Brand_Code + "' " +
                            "Qty='" + Qty + "' Wtper='" + Wtper + "' Netkg='" + Netkg + "' Rate='" + Rate + "' Value='" + Value + "' Market_Cess='" + Market_Cess + "' Super_Cost='" + Super_Cost + "' " +
                            "Packing='" + Packing + "' Hamali='" + Hamali + "' Freight='" + Freight + "' Other='" + Other + "' Taxable_Amount='" + DTaxable_Amount + "' GST_Code='" + GST_Code + "' " +
                            "GST='" + GST + "' Net_Value='" + DNet_Value + "' Company_Code='" + Company_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' Created_Date='" + Doc_Date + "' " +
                            "Modified_Date='" + Doc_Date + "' Year_Code='" + Year_Code + "' CGST_Rate='" + DCGST_Rate + "' CGST_Amount='" + DCGST_Amount + "' SGST_Rate='" + DSGST_Rate + "' " +
                            "SGST_Amount='" + DSGST_Amount + "' IGST_Rate='" + DIGST_Rate + "' IGST_Amount='" + DIGST_Amount + "' CashCreditType='" + CashCredit + "' Salerate='" + DSalerate + "' " +
                            "Retailid='" + Retailid + "' retaildid='" + retaildid + "' ic='" + ic + "' saleac='" + saleac + "' sid='" + sid + "' PurcNo='" + PurcNo + "' MillCode='" + MillCode + "' mc='" + mc + "' PD_ID='" + PD_ID + "'/>";
                    }
                    if (gridView.rows[i].cells[30].innerHTML == "D") {

                        XML = XML + "<DetailDelete Doc_No='" + Doc_No + "' Detail_Id='" + Detail_Id + "' Tran_Type='RR' Item_Code='" + Item_Code + "' Brand_Code='" + Brand_Code + "' " +
                            "Qty='" + Qty + "' Wtper='" + Wtper + "' Netkg='" + Netkg + "' Rate='" + Rate + "' Value='" + Value + "' Market_Cess='" + Market_Cess + "' Super_Cost='" + Super_Cost + "' " +
                            "Packing='" + Packing + "' Hamali='" + Hamali + "' Freight='" + Freight + "' Other='" + Other + "' Taxable_Amount='" + DTaxable_Amount + "' GST_Code='" + GST_Code + "' " +
                            "GST='" + GST + "' Net_Value='" + DNet_Value + "' Company_Code='" + Company_Code + "' Created_By='" + USER + "' Modified_By='" + USER + "' Created_Date='" + Doc_Date + "' " +
                            "Modified_Date='" + Doc_Date + "' Year_Code='" + Year_Code + "' CGST_Rate='" + DCGST_Rate + "' CGST_Amount='" + DCGST_Amount + "' SGST_Rate='" + DSGST_Rate + "' " +
                            "SGST_Amount='" + DSGST_Amount + "' IGST_Rate='" + DIGST_Rate + "' IGST_Amount='" + DIGST_Amount + "' CashCreditType='" + CashCredit + "' Salerate='" + DSalerate + "' " +
                            "Retailid='" + Retailid + "' retaildid='" + retaildid + "' ic='" + ic + "' saleac='" + saleac + "' sid='" + sid + "' PurcNo='" + PurcNo + "' MillCode='" + MillCode + "' mc='" + mc + "' PD_ID='" + PD_ID + "'/>";
                    }

                    if (gridView.rows[i].cells[30].innerHTML != "D") {
                        if (Value != 0) {
                            XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + saleac + "' " +
                                                            "UNIT_code='0' NARRATION='Retail sale' AMOUNT='" + Value + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                            "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + sid + "' vc='0' progid='9' tranid='0'/>";
                            Order_Code = Order_Code + 1;
                        }
                    }

                }
                debugger;
                if (CGST_Amount != 0) {
                    XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["SaleCGSTAc"] %>' + "' " +
                                                           "UNIT_code='0' NARRATION='Retail sale' AMOUNT='" + CGST_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleCGSTid"] %>' + "' vc='0' progid='9' tranid='0'/>";
                    Order_Code = Order_Code + 1;
                }
                if (SGST_Amount != 0) {
                    XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["SaleSGSTAc"] %>' + "' " +
                                                           "UNIT_code='0' NARRATION='Retail sale' AMOUNT='" + SGST_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleSGSTid"] %>' + "' vc='0' progid='9' tranid='0'/>";
                    Order_Code = Order_Code + 1;
                }
                if (IGST_Amount != 0) {
                    XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["SaleIGSTAc"] %>' + "' " +
                                                            "UNIT_code='0' NARRATION='Retail sale' AMOUNT='" + IGST_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                            "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleIGSTid"] %>' + "' vc='0' progid='9' tranid='0'/>";
                    Order_Code = Order_Code + 1;
                }
                if (Party_Code != 1) {
                    if (TCS_Amount > 0) {

                        XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + Party_Code + "' " +
                                                                 "UNIT_code='0' NARRATION='TCS:Retail sale' AMOUNT='" + TCS_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                 "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                 "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + pc + "' vc='0' progid='9' tranid='0'/>";
                        Order_Code = Order_Code + 1;

                    }
                    if (TCS_Amount > 0) {

                        XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                                                 "UNIT_code='0' NARRATION='TCS:Retail sale' AMOUNT='" + TCS_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                 "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + Party_Code + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                 "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleTCSAcid"] %>' + "' vc='0' progid='9' tranid='0'/>";
                        Order_Code = Order_Code + 1;

                    }


                    if (TDS_Amt > 0) {
                        Order_Code = parseInt(Order_Code) + 1;

                        XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + Party_Code + "' " +
                                                              "UNIT_code='0' NARRATION='TDS Retail Sale' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                              "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + pc + "' vc='0' progid='9' tranid='0'/>";

                        Order_Code = parseInt(Order_Code) + 1;

                        XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + <%=Session["SaleTDSAc"] %> + "' " +
                                                        "UNIT_code='0' NARRATION='TDS Retail Sale' AMOUNT='" + TDS_Amt + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + <%=Session["SaleTDSacid"] %> + "' vc='0' progid='9' tranid='0'/>";
                    }
                }

                if (NetMarketSess > 0) {

                    XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["MarketSase"] %>' + "' " +
                                                              "UNIT_code='0' NARRATION='MarketSess' AMOUNT='" + NetMarketSess + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + Party_Code + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                              "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["MarketSaseid"] %>' + "' vc='0' progid='9' tranid='0'/>";
                    Order_Code = Order_Code + 1;

                }
                if (NetOther > 0) {

                    XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["OTHER_Ac"] %>' + "' " +
                                                              "UNIT_code='0' NARRATION='Other' AMOUNT='" + NetOther + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + Party_Code + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                              "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["OTHER_Acid"] %>' + "' vc='0' progid='9' tranid='0'/>";
                    Order_Code = Order_Code + 1;

                }
                if (NetSuperCost > 0) {

                    XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["SuperCost"] %>' + "' " +
                                                               "UNIT_code='0' NARRATION='SuperCost' AMOUNT='" + NetSuperCost + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                               "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + Party_Code + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                               "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SuperCostid"] %>' + "' vc='0' progid='9' tranid='0'/>";
                    Order_Code = Order_Code + 1;

                }
                if (NetHamali > 0) {

                    XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["Hamali"] %>' + "' " +
                                                                "UNIT_code='0' NARRATION='Hamali' AMOUNT='" + NetHamali + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + Party_Code + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["Hamaliid"] %>' + "' vc='0' progid='9' tranid='0'/>";
                    Order_Code = Order_Code + 1;

                }
                if (NetFrieght > 0) {

                    XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["Freight_Ac"] %>' + "' " +
                                                                 "UNIT_code='0' NARRATION='Freight' AMOUNT='" + NetFrieght + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                 "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + Party_Code + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                 "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["Freight_Acid"] %>' + "' vc='0' progid='9' tranid='0'/>";
                    Order_Code = Order_Code + 1;

                }
                if (NetPacking > 0) {

                    XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["Packing"] %>' + "' " +
                                                                  "UNIT_code='0' NARRATION='Packing' AMOUNT='" + NetPacking + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + Party_Code + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                  "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["Packingid"] %>' + "' vc='0' progid='9' tranid='0'/>";
                    Order_Code = Order_Code + 1;

                }
                if (Roundoff > 0) {
                    XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["RoundOff"] %>' + "' " +
                                                                   "UNIT_code='0' NARRATION='Packing' AMOUNT='" + Roundoff + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                   "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='" + Party_Code + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                   "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["RoundOffid"] %>' + "' vc='0' progid='9' tranid='0'/>";
                    Order_Code = Order_Code + 1;


                }
                else {
                    XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + '<%=Session["RoundOff"] %>' + "' " +
                                                                     "UNIT_code='0' NARRATION='Packing' AMOUNT='" + Roundoff + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                     "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + Party_Code + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                     "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["RoundOffid"] %>' + "' vc='0' progid='9' tranid='0'/>";
                    Order_Code = Order_Code + 1;
                }
                if (NetValue > 0) {

                    XML = XML + "<Ledger TRAN_TYPE='RR' CASHCREDIT='" + CashCredit + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Doc_Date + "' AC_CODE='" + Party_Code + "' " +
                                                                   "UNIT_code='0' NARRATION='Packing' AMOUNT='" + NetValue + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                   "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='" + Party_Code + "' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                                   "SORT_TYPE='RR' SORT_NO='" + Doc_No + "' ac='" + pc + "' vc='0' progid='9' tranid='0'/>";
                    Order_Code = Order_Code + 1;

                }




                XML = XML + "</RetailHead></ROOT>";
                ProcessXML(XML, status, spname, CashCredit);

            }
            catch (exx) {
                $("#loader").hide();
                alert(exx)
            }
        }
        function ProcessXML(XML, status, spname, CashCredit) {
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
                        window.open('../Outword/pgeRetailSale.aspx?retailid=' + id + '&Action=1&tran_type=' + CashCredit, "_self");
                        SB();
                    }
                }
                else {
                    var num = parseInt(response.d);

                    if (isNaN(num)) {
                        alert(response.d)

                    }
                    else {
                        window.open('../Outword/PgeRetailUtility.aspx', "_self");
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
            <asp:Label ID="label1" runat="server" Text="Retail Entry " Font-Names="verdana"
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
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />
            <asp:HiddenField ID="hdnfmarketsessrate" runat="server" />
            <asp:HiddenField ID="hdnfpartystatecode" runat="server" />
            <asp:HiddenField ID="hdnfsupercostrate" runat="server" />
            <asp:HiddenField ID="hdnfrateper" runat="server" />
            <asp:HiddenField ID="hdnfincludinggst" runat="server" />
            <asp:HiddenField ID="hdnfdoc" runat="server" />
            <asp:HiddenField ID="hdnfid" runat="server" />
            <asp:HiddenField ID="hdnfpc" runat="server" />
            <asp:HiddenField ID="hdnfmillid" runat="server" />
            <asp:HiddenField ID="hdnfst" runat="server" />
            <asp:HiddenField ID="hdnfbc" runat="server" />
            <asp:HiddenField ID="hdnfpcn" runat="server" />
            <asp:HiddenField ID="hdnfdrpVal" runat="server" />
            <asp:HiddenField ID="hdnfwt" runat="server" />
            <asp:HiddenField ID="hdnfnetPayble" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
            <asp:HiddenField ID="hdnfpurcno" runat="server" />
            <asp:HiddenField ID="hdnfpurcid" runat="server" />

            <asp:HiddenField ID="hdnftc" runat="server" />
            <asp:HiddenField ID="hdnfTCSRate" runat="server" />
            <asp:HiddenField ID="hdnfbalance" runat="server" />
            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" OnClientClick="if (!Validate()) return false;" TabIndex="51" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" Height="24px" OnClientClick="Back()" />
                        &nbsp;
                        <asp:Button runat="server" ID="btnPrintSaleBill" Text="Print" CssClass="btnHelp"
                            Width="80px" Height="24px" OnClientClick="SB();" />
                        &nbsp;
                         <asp:Button runat="server" ID="btnGenEinvoice" Text="Generate EInvoice" CssClass="btnHelp"
                             Width="120px" Height="24px" OnClientClick="EInovice();" />
                        &nbsp;
                        <asp:Button runat="server" ID="btnGentare_EWayBill" Text="Gentare EWayBill" CssClass="btnHelp"
                            Width="90px" Height="24px" OnClientClick="GEway();" />
                        &nbsp;
                          <asp:Button runat="server" ID="btnCancleEinvoice" Text="Cancle EInvoice" CssClass="btnHelp"
                              Width="120px" Height="24px" OnClientClick="ConfirmCancle();" OnClick="btnCancleEInvoice_Click" />
                    </td>
                    <td align="center">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnFirst_Click" Width="90px" Height="24px"  />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="Previous" CssClass="btnHelp"
                            OnClick="btnPrevious_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="Next" CssClass="btnHelp"
                            OnClick="btnNext_Click" Width="90px" Height="24px"  />
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="Last" CssClass="btnHelp"
                            OnClick="btnLast_Click" Width="90px" Height="24px"/>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 80%;" align="left" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="right">Cash / Credit:
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="drpCashCredit" Width="100px" CssClass="ddl"
                                AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="drpCashCredit_SelectedIndexChanged">
                                <asp:ListItem Text="Cash" Value="CS" />
                                <asp:ListItem Text="Credit" Value="CR" />
                            </asp:DropDownList>
                            Change No
                            <asp:TextBox Height="24px" ID="txtEditDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Doc No
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDoc_No" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                OnkeyDown="Doc_No(event);" OnTextChanged="txtDoc_No_TextChanged" Style="text-align: left;"
                                TabIndex="2" Width="90px"></asp:TextBox>
                            <asp:Button ID="btntxtDoc_No" runat="server" CssClass="btnHelp" Height="24px" OnClick="btntxtDoc_No_Click"
                                Text="..." Width="20px" />
                            <asp:Label ID="lblDoc_No" runat="server" CssClass="lblName"></asp:Label>
                            Doc Date:
                            <asp:TextBox ID="txtDoc_Date" runat="Server" AutoPostBack="true" CssClass="txt"
                                Height="24px" onkeydown="retun DateFormat(this,event.keyCode)" onkeyup="ValiddateDate(this,event.keyCode)"
                                OnTextChanged="txtDoc_Date_TextChanged" Style="text-align: left;" TabIndex="4"
                                Width="90px"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtDoc_Date" runat="server" Height="15px" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" />
                            <ajax1:CalendarExtender ID="CalendarExtenderDatetxtDoc_Date" runat="server" Format="dd/MM/yyyy"
                                PopupButtonID="imgcalendertxtDoc_Date" TargetControlID="txtDoc_Date">
                            </ajax1:CalendarExtender>
                            Time:
                            <asp:TextBox ID="txtTime" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" Style="text-align: left;" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Challan No
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtChallan_No" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnTextChanged="txtChallan_No_TextChanged" Style="text-align: left;"
                                TabIndex="5" Width="250px"></asp:TextBox>
                            Challan Date
                            <asp:TextBox ID="txtChallan_Date" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" onkeydown="retun DateFormat(this,event.keyCode)" onkeyup="ValiddateDate(this,event.keyCode)"
                                OnTextChanged="txtChallan_Date_TextChanged" Style="text-align: left;" TabIndex="6"
                                Width="90px"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtChallan_Date" runat="server" Height="15px" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" />
                            <ajax1:CalendarExtender ID="CalendarExtenderDatetxtChallan_Date" runat="server" Format="dd/MM/yyyy"
                                PopupButtonID="imgcalendertxtChallan_Date" TargetControlID="txtChallan_Date">
                            </ajax1:CalendarExtender>
                            Vehical No
                            <asp:TextBox ID="txtVahical_No" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnTextChanged="txtVahical_No_TextChanged" Style="text-align: left;"
                                TabIndex="7" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Bill To
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtParty_Code" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnkeyDown="Party_Code(event);" OnTextChanged="txtParty_Code_TextChanged"
                                Style="text-align: left;" TabIndex="8" Width="90px"></asp:TextBox>
                            <asp:Button ID="btntxtParty_Code" runat="server" CssClass="btnHelp" Height="24px"
                                OnClick="btntxtParty_Code_Click" Text="..." Width="20px" />
                            <asp:Label ID="lblParty_Code" runat="server" CssClass="lblName"></asp:Label>
                            Bill To State Code:<asp:Label ID="lblpartyStatecode" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Ship To
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtShipto" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnkeyDown="shipto(event);" OnTextChanged="txtShipto_TextChanged"
                                Style="text-align: left;" TabIndex="8" Width="90px"></asp:TextBox>
                            <asp:Button ID="btntxtShipto" runat="server" CssClass="btnHelp" Height="24px"
                                OnClick="btntxtShipto_Click" Text="..." Width="20px" />
                            <asp:Label ID="lblshipto" runat="server" CssClass="lblName"></asp:Label>
                            Ship To State Code:<asp:Label ID="lblshiptostatecode" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Due Days
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDue_Days" runat="Server" AutoPostBack="true" CssClass="txt"
                                Height="24px" OnTextChanged="txtDue_Days_TextChanged" Style="text-align: left;"
                                TabIndex="9" Width="90px"></asp:TextBox>
                            Due Date:
                            <asp:TextBox ID="txtDue_date" runat="Server" AutoPostBack="true" CssClass="txt"
                                Height="24px" OnTextChanged="txtDue_date_TextChanged" Style="text-align: left;"
                                TabIndex="10" Width="90px"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtDue_date" runat="server" Height="15px" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" />
                            <ajax1:CalendarExtender ID="CalendarExtenderDatetxtDue_date" runat="server" Format="dd/MM/yyyy"
                                PopupButtonID="imgcalendertxtDue_date" TargetControlID="txtDue_date">
                            </ajax1:CalendarExtender>
                            Delivered
                            <asp:DropDownList ID="drpDelivered" runat="Server" AutoPostBack="false" CssClass="txt"
                                OnSelectedIndexChanged="drpDelivered_SelectedIndexChanged" TabIndex="11" Width="90px">
                                <asp:ListItem Text="Yes" Value="0" />
                                <asp:ListItem Text="No" Value="1" />
                            </asp:DropDownList>
                            Cash Recived
                            <asp:CheckBox Text="" runat="server" TabIndex="12" ID="drpCashRecieve" OnCheckedChanged="drpCashRecieve_CheckedChanged" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Broker_Code
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBroker_Code" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnkeyDown="Broker_Code(event);" OnTextChanged="txtBroker_Code_TextChanged"
                                Style="text-align: left;" TabIndex="13" Width="90px"></asp:TextBox>
                            <asp:Button ID="btntxtBroker_Code" runat="server" CssClass="btnHelp" Height="24px"
                                OnClick="btntxtBroker_Code_Click" Text="..." Width="20px" />
                            <asp:Label ID="lblbrokername" runat="server" CssClass="lblName"></asp:Label>
                            Transport:
                                <asp:TextBox ID="txtTransportCode" runat="Server" CssClass="txt" TabIndex="25" Width="80px"
                                    Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtTransportCode_TextChanged" onKeyDown="Transport(event);"></asp:TextBox>
                            <asp:Button ID="btnTransport" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                Width="20px" OnClick="btnTransport_Click" />
                            <asp:Label ID="lblTransportName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Party_Name
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtParty_Name" runat="Server" AutoPostBack="true" CssClass="txt"
                                Height="24px" OnTextChanged="txtParty_Name_TextChanged" Style="text-align: left;"
                                TabIndex="14" Width="250px"></asp:TextBox>
                            Party_Name_new
                           <%-- <asp:TextBox ID="txtParty_name_new" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnTextChanged="txtParty_name_new_TextChanged" Style="text-align: left;"
                                TabIndex="15" Width="230px"></asp:TextBox>--%>

                            <asp:TextBox ID="txtParty_name_new" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnkeyDown="Party_name_new(event);" OnTextChanged="txtParty_name_new_TextChanged"
                                Style="text-align: left;" TabIndex="15" Width="90px"></asp:TextBox>
                            <asp:Button ID="btntxtParty_name_new" runat="server" CssClass="btnHelp" Height="24px"
                                OnClick="btntxtParty_name_new_Click" Text="..." Width="20px" />
                            <asp:Label ID="lblpartynamenew" runat="server" CssClass="lblName"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td align="right">Narration
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtNarration" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="50px" OnTextChanged="txtNarration_TextChanged" Style="text-align: left;"
                                TabIndex="16" Width="300px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                    <legend style="text-align: center;">
                        <h5 style="color: White;" font-names="verdana" font-size="Medium">Detail Entry</h5>
                    </legend>
                </fieldset>
                <table width="80%" align="left">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblID" runat="server" CssClass="lblName" Font-Names="verdana" Text=""
                                Visible="false"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblNo" runat="server" CssClass="lblName" Font-Names="verdana" Text=""
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Purc No:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPurc_No" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnTextChanged="txtPurc_No_TextChanged"
                                Style="text-align: left;" Width="50px"></asp:TextBox>
                            <asp:Button ID="btntxtPurc_No" runat="server" CssClass="btnHelp" Height="24px"
                                OnClick="btntxtPurc_No_Click" Text="..." Width="20px" TabIndex="17" OnClientClick="tab(event);" />
                            Mill Code
                            <asp:TextBox ID="txtmillcode" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnkeyDown="millcode(event);" OnTextChanged="txtmillcode_TextChanged"
                                Style="text-align: left;" TabIndex="18" Width="90px"></asp:TextBox>
                            <asp:Button ID="btnmillcode" runat="server" CssClass="btnHelp" Height="24px"
                                OnClick="btnmillcode_Click" Text="..." Width="20px" />
                            <asp:Label ID="lblmill" runat="server" CssClass="lblName"></asp:Label>
                            Item_Code
                            <asp:TextBox ID="txtItem_Code" runat="Server" AutoPostBack="false" CssClass="txt"
                                onkeydown="Item_Code(event);" Height="24px" OnTextChanged="txtItem_Code_TextChanged" Style="text-align: left;"
                                TabIndex="19" Width="90px"></asp:TextBox>
                            <asp:Button ID="btntxtItem_Code" runat="server" CssClass="btnHelp" OnClick="btntxtItem_Code_Click"
                                Text="..." />
                            <asp:Label ID="lblItemname" runat="server" CssClass="lblName"></asp:Label>
                            Brand_Code
                            <asp:TextBox ID="txtBrand_Code" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnTextChanged="txtBrand_Code_TextChanged" Style="text-align: left;"
                                TabIndex="20" Width="90px" onkeydown="Brand_Code(event);"></asp:TextBox>
                            <asp:Button ID="btntxtBrand_Code" runat="server" CssClass="btnHelp" OnClick="btntxtBrand_Code_Click"
                                Text="..." />
                            <asp:Label ID="lblBrandname" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Qty
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtQty" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                onkeydown="Calculations(event);" OnTextChanged="txtQty_TextChanged" Style="text-align: left;" TabIndex="21" Width="90px"></asp:TextBox>
                            Wtper
                            <asp:TextBox ID="txtWtper" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                onkeydown="Calculations(event);" OnTextChanged="txtWtper_TextChanged" Style="text-align: left;"
                                Width="90px"></asp:TextBox>
                            Netkg
                            <asp:TextBox ID="txtNetkg" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                onkeydown="Calculations(event);" OnTextChanged="txtNetkg_TextChanged" Style="text-align: left;"
                                Width="90px"></asp:TextBox>
                            Rate
                            <asp:TextBox ID="txtRate" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                onkeydown="Calculations(event),rate(event);" OnTextChanged="txtRate_TextChanged" Style="text-align: left;" TabIndex="22" Width="90px"></asp:TextBox>
                            Sale Rate
                            <asp:TextBox ID="txtsalerate" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                Style="text-align: left;" Width="90px" onkeydown="rate(event);"></asp:TextBox>
                            Value
                            <asp:TextBox ID="txtValue" runat="Server" AutoPostBack="true" CssClass="txt" Height="24px"
                                OnTextChanged="txtValue_TextChanged" Style="text-align: left;"
                                Width="90px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Market_Cess
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMarket_Cess" runat="Server" AutoPostBack="false" CssClass="txt"
                                onkeydown="Calculations(event);" Height="24px" OnTextChanged="txtMarket_Cess_TextChanged" Style="text-align: left;"
                                Width="90px"></asp:TextBox>
                            Super_Cost
                            <asp:TextBox ID="txtSuper_Cost" runat="Server" AutoPostBack="false" CssClass="txt"
                                onkeydown="Calculations(event);" Height="24px" OnTextChanged="txtSuper_Cost_TextChanged" Style="text-align: left;"
                                Width="90px"></asp:TextBox>
                            Packing
                            <asp:TextBox ID="txtPacking" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                onkeydown="Calculations(event);" OnTextChanged="txtPacking_TextChanged" Style="text-align: left;"
                                Width="90px"></asp:TextBox>
                            Hamali
                            <asp:TextBox ID="txtHamali" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                onkeydown="Calculations(event);" OnTextChanged="txtHamali_TextChanged" Style="text-align: left;"
                                Width="90px"></asp:TextBox>
                            Freight
                            <asp:TextBox ID="txtFreight" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                onkeydown="Calculations(event);" OnTextChanged="txtFreight_TextChanged" Style="text-align: left;"
                                Width="90px"></asp:TextBox>
                            Other
                            <asp:TextBox ID="txtOther" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                onkeydown="Calculations(event);" OnTextChanged="txtOther_TextChanged" Style="text-align: left;"
                                Width="90px"></asp:TextBox>
                            Taxable_Amount
                            <asp:TextBox ID="txtTaxable_Amount1" runat="Server" AutoPostBack="true" CssClass="txt"
                                Height="24px" OnTextChanged="txtTaxable_Amount1_TextChanged" Style="text-align: left;"
                                Width="90px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">GST_Code
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtGST_Code" runat="Server" AutoPostBack="true" CssClass="txt" Height="24px"
                                OnTextChanged="txtGST_Code_TextChanged" Style="text-align: left;"
                                Width="90px" onkeydown="Calculations(event);"></asp:TextBox>
                            <asp:Button ID="btntxtGST_Code" runat="server" CssClass="btnHelp" OnClick="btntxtGST_Code_Click"
                                Text="..." />
                            <asp:Label ID="lblgstname" runat="server" CssClass="lblName"></asp:Label>
                            CGST_Rate
                            <asp:TextBox ID="txtCGST_Rate" runat="Server" AutoPostBack="true" CssClass="txt"
                                Height="24px" OnTextChanged="txtCGST_Rate_TextChanged" Style="text-align: left;"
                                Width="30px"></asp:TextBox>
                            <asp:TextBox ID="txtCGST_Amount" runat="Server" AutoPostBack="true" CssClass="txt"
                                Height="24px" OnTextChanged="txtCGST_Amount_TextChanged" Style="text-align: left;"
                                Width="70px"></asp:TextBox>
                            SGST_Rate
                            <asp:TextBox ID="txtSGST_Rate" runat="Server" AutoPostBack="true" CssClass="txt"
                                Height="24px" OnTextChanged="txtSGST_Rate_TextChanged" Style="text-align: left;"
                                Width="30px"></asp:TextBox>
                            <asp:TextBox ID="txtSGST_Amount" runat="Server" AutoPostBack="true" CssClass="txt"
                                Height="24px" OnTextChanged="txtSGST_Amount_TextChanged" Style="text-align: left;"
                                Width="70px"></asp:TextBox>
                            IGST_Rate
                            <asp:TextBox ID="txtIGST_Rate" runat="Server" AutoPostBack="true" CssClass="txt"
                                Height="24px" OnTextChanged="txtIGST_Rate_TextChanged" Style="text-align: left;"
                                Width="30px"></asp:TextBox>
                            <asp:TextBox ID="txtIGST_Amount" runat="Server" AutoPostBack="true" CssClass="txt"
                                Height="24px" OnTextChanged="txtIGST_Amount_TextChanged" Style="text-align: left;"
                                Width="70px"></asp:TextBox>
                            GST
                            <asp:TextBox ID="txtGST" runat="Server" AutoPostBack="true" CssClass="txt" Height="24px"
                                OnTextChanged="txtGST_TextChanged" Style="text-align: left;" Width="90px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Net_Value
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtNet_Value" runat="Server" AutoPostBack="true" CssClass="txt"
                                Height="24px" OnTextChanged="txtNet_Value_TextChanged" Style="text-align: left;"
                                TabIndex="23" Width="90px"></asp:TextBox>
                            <asp:Button ID="btnAdddetails" runat="server" CssClass="btnHelp" Height="25px" OnClick="btnAdddetails_Click"
                                TabIndex="24" Text="ADD" Width="80px" />
                            <asp:Button ID="btnClosedetails" runat="server" CssClass="btnHelp" Height="25px"
                                OnClick="btnClosedetails_Click" TabIndex="25" Text="Close" Width="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="11">
                            <table width="100%">
                                <tr>
                                    <td width="100%" align="left" style="margin-top: 100px;">
                                        <div style="width: 100%; position: relative;">
                                            <asp:UpdatePanel ID="upGrid" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="pnlgrdDetail" runat="server" BackColor="SeaShell" BorderColor="Maroon"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"
                                                        Height="200px" ScrollBars="Both" Style="margin-left: 30px; float: left;" Width="1800px">
                                                        <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" CellPadding="5"
                                                            CellSpacing="5" GridLines="Both" HeaderStyle-BackColor="#397CBB" HeaderStyle-ForeColor="White"
                                                            HeaderStyle-Height="30px" OnRowCommand="grdDetail_RowCommand" OnRowDataBound="grdDetail_RowDataBound"
                                                            Style="table-layout: fixed;" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="lnk" CommandName="EditRecord"
                                                                            Text="Edit"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument="lnk" CommandName="DeleteRecord"
                                                                            Text="Delete"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle ForeColor="Black" Height="25px" Wrap="false" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>

                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel onkeydown="closepopup(event);" ID="pnlPopup" runat="server" Width="1300px"
                align="center" ScrollBars="Both" BackColor="#FFFFE4" Direction="LeftToRight"
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
                            <asp:TextBox onkeydown="SelectFirstRow(event);" ID="txtSearchText" runat="server"
                                Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button onkeydown="closepopup(event);" ID="btnSearch" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="1300px" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px" ScrollBars="Vertical">
                                <%-- <asp:GridView Font-Bold="true" CssClass="select" AllowPaging="true" PageSize="20"
                                    OnPageIndexChanging="grdPopup_PageIndexChanging" ID="grdPopup" runat="server"
                                    AutoGenerateColumns="true" Width="2100px" EmptyDataText="No Records Found"   HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound"
                                    Style="table-layout: fixed;">
                                    <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                                    <RowStyle Height="25px" ForeColor="Black" Wrap="false" />
                                    <PagerStyle BackColor="Tomato" ForeColor="White" Width="100%" Font-Bold="true" />
                                    <PagerSettings Position="TopAndBottom" />
                                </asp:GridView>--%>

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
            <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="Black" Width="955px"
                BorderColor="" BorderWidth="1px" Height="300px" BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
            </asp:Panel>
            <table style="width: 100%; font-weight: bolder; font: bolder; font-size: large; color: black;" align="left">

                <tr>
                    <td align="left">Item Value
                        <asp:TextBox ID="txtItemvalue" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" Style="text-align: left;" ReadOnly="true"
                            TabIndex="26" Width="90px"></asp:TextBox>
                        Net Exp
                        <asp:TextBox ID="txtNetExp" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                            OnTextChanged="txtNetExp_TextChanged" Style="text-align: left;" TabIndex="27"
                            Width="90px"></asp:TextBox>
                        Taxable Amount
                    
                      <asp:TextBox ID="txtTaxable_Amount" runat="Server" AutoPostBack="false" CssClass="txt"
                          Height="24px" OnTextChanged="txtTaxable_Amount_TextChanged" Style="text-align: left;"
                          TabIndex="28" Width="90px"></asp:TextBox>
                        Net GSt
                        <asp:TextBox ID="txtNetGST" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                            OnTextChanged="txtNetGST_TextChanged" Style="text-align: left;" TabIndex="29"
                            Width="90px"></asp:TextBox>
                        Round Off
                        <asp:TextBox ID="txtRoundoff" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" OnTextChanged="txtRoundoff_TextChanged" Style="text-align: left;"
                            TabIndex="30" Width="90px" Visible="true"></asp:TextBox>
                        Bill Amount
                        <asp:TextBox ID="txtNetValue" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" OnTextChanged="txtNetValue_TextChanged" Style="text-align: left;"
                            TabIndex="31" Width="90px"></asp:TextBox>
                        TCS%
                        <asp:TextBox ID="txtTCS_Rate" runat="Server" AutoPostBack="true" CssClass="txt"
                            onkeydown="tcs(event);" Height="24px" OnTextChanged="txtTCS_Rate_TextChanged" Style="text-align: left;"
                            TabIndex="32" Width="90px"></asp:TextBox>
                        TCS
                        <asp:TextBox ID="txtTCS_Amount" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" OnTextChanged="txtTCS_Amount_TextChanged" Style="text-align: left;"
                            TabIndex="33" Width="90px"></asp:TextBox>
                        Net Payble with TCS
                        <asp:TextBox ID="txtNetPayble" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" OnTextChanged="txtNetPayble_TextChanged" ReadOnly="false"
                            Style="text-align: left; font-size: x-large; font-weight: bold; color: blue;"
                            TabIndex="34" Width="150px"></asp:TextBox>

                        TDS%
                                   
                                        <asp:TextBox ID="txtTDS" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                            OnTextChanged="txtTDS_TextChanged" TabIndex="52" Style="text-align: right;"
                                            Height="24px" onkeydown="tds(event);"></asp:TextBox>
                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom"
                            ValidChars="." TargetControlID="txtTDS">
                        </ajax1:FilteredTextBoxExtender>
                        <asp:TextBox ID="txtTDSAmt" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                            TabIndex="53" OnTextChanged="txtTDSAmt_TextChanged" Style="text-align: right;"
                            Width="98px"></asp:TextBox>
                        <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers,Custom"
                            TargetControlID="txtTDSAmt" ValidChars=".">
                        </ajax1:FilteredTextBoxExtender>
                </tr>
                <tr>
                    <td align="left">Market Ses
                        <asp:TextBox ID="txtNetMarketsess" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" OnTextChanged="txtNetMarketsess_TextChanged" Style="text-align: left;"
                            TabIndex="35" Width="90px"></asp:TextBox>
                        Super Cost
                        <asp:TextBox ID="txtNetSuperCost" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" OnTextChanged="txtNetSuperCost_TextChanged" Style="text-align: left;"
                            TabIndex="36" Width="90px"></asp:TextBox>
                        Packing
                        <asp:TextBox ID="txtNetpacking" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" OnTextChanged="txtNetpacking_TextChanged" Style="text-align: left;"
                            TabIndex="37" Width="90px"></asp:TextBox>
                        Hamali
                        <asp:TextBox ID="txtNetHamali" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" OnTextChanged="txtNetHamali_TextChanged" Style="text-align: left;"
                            TabIndex="38" Width="90px"></asp:TextBox>



                        Net Frieght
                        <asp:TextBox ID="txtNetFrieght" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" OnTextChanged="txtNetFrieght_TextChanged" Style="text-align: left;"
                            TabIndex="39" Width="90px"></asp:TextBox>
                        Net Other
                        <asp:TextBox ID="txtNetOther" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" OnTextChanged="txtNetOther_TextChanged" Style="text-align: left;"
                            TabIndex="40" Width="90px"></asp:TextBox>
                        Total
                    
                        <asp:TextBox ID="txtTotal" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                            OnTextChanged="txtTotal_TextChanged" Style="text-align: left;" TabIndex="41"
                            Width="90px"></asp:TextBox>




                        NetWeight
                        <asp:TextBox ID="txtNetWeight" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" OnTextChanged="txtNetWeight_TextChanged" Style="text-align: left;"
                            TabIndex="42" Width="90px"></asp:TextBox>
                        <asp:TextBox ID="txtTCS_Net_Payble" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" OnTextChanged="txtTCS_Net_Payble_TextChanged" Style="text-align: left;"
                            Width="90px" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">CGST
                        <asp:TextBox ID="txtTotalCGST" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" Style="text-align: left;" TabIndex="43" Width="70px"></asp:TextBox>
                        SGST
                        <asp:TextBox ID="txtTotalSGST" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" Style="text-align: left;" TabIndex="44" Width="70px"></asp:TextBox>
                        IGST
                        <asp:TextBox ID="txtTotalIGST" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" Style="text-align: left;" TabIndex="45" Width="70px"></asp:TextBox>

                        <asp:TextBox ID="txtNewSBNo" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                            OnTextChanged="txtNewSBNo_TextChanged" Style="text-align: left;" TabIndex="46"
                            Width="90px" Visible="false"></asp:TextBox>

                        <asp:TextBox ID="txtNewSBDate" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" onkeydown="retun DateFormat(this,event.keyCode)" onkeyup="ValiddateDate(this,event.keyCode)"
                            OnTextChanged="txtNewSBDate_TextChanged" Style="text-align: left;" TabIndex="47"
                            Width="90px" Visible="false"></asp:TextBox>
                        <asp:Image ID="imgcalendertxtNewSBDate" runat="server" Height="15px" ImageUrl="~/Images/calendar_icon1.png"
                            Width="25px" Visible="false" />
                        <ajax1:CalendarExtender ID="CalendarExtenderDatetxtNewSBDate" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="imgcalendertxtNewSBDate" TargetControlID="txtNewSBDate">
                        </ajax1:CalendarExtender>
                        EnvoiceNo
                        <asp:TextBox ID="txtEnvoiceno" runat="Server" AutoPostBack="false" CssClass="txt"
                            Height="24px" OnTextChanged="txtEnvoiceno_TextChanged" Style="text-align: left;"
                            TabIndex="48" Width="90px"></asp:TextBox>
                        ACK
                        <asp:TextBox ID="txtACK" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                            OnTextChanged="txtACK_TextChanged" Style="text-align: left;" TabIndex="49" Width="90px"></asp:TextBox>

                        Eway Bill No
                        <asp:TextBox ID="txtEwayBillNo" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                            Style="text-align: left;" TabIndex="50" Width="90px"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td align="left">
                        <asp:Button ID="btnOpenDetailsPopup" runat="server" CssClass="btnHelp" Height="25px"
                            OnClick="btnOpenDetailsPopup_Click" TabIndex="71" Text="ADD" Visible="false"
                            Width="80px" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
