<%@ Page Title="General Transaction" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeGeneralTransaction.aspx.cs" Inherits="Sugar_Transaction_pgeGeneralTransaction" %>

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

        document.addEventListener('keyup', function (event) {
            if (event.defaultPrevented) {
                return;
            }

            var key = event.key || event.keyCode;

            if (key === 'Escape' || key === 'Esc' || key === 27) {
                //                doWhateverYouWantNowThatYourKeyWasHit();

                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                if (hdnfClosePopupValue == "txtCompany_CODE") {
                    document.getElementById("<%=txtCompany_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtYear_Code") {
                    document.getElementById("<%=txtYear_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtDebitAc_Code") {
                    document.getElementById("<%=txtDebitAc_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtCreditAc_Code") {
                    document.getElementById("<%=txtCreditAc_Code.ClientID %>").focus();
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

                if (hdnfClosePopupValue == "txtCompany_CODE") {
                    document.getElementById("<%=txtCompany_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblCompanyName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtCompany_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtYear_Code") {
                    document.getElementById("<%=txtYear_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblYearname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtYear_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtDebitAc_Code") {
                    document.getElementById("<%=txtDebitAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblDebitAc_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtDebitAc_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtCreditAc_Code") {
                    document.getElementById("<%=txtCreditAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblCreditAc_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtCreditAc_Code.ClientID %>").focus();
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


        function Company(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtCompany_CODE .ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtCompany_CODE .ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtCompany_CODE .ClientID %>").val(unit);
                __doPostBack("txtCompany_CODE ", "TextChanged");

            }

        }
        function Year(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtYear_Code .ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtYear_Code .ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtYear_Code .ClientID %>").val(unit);
                __doPostBack("txtYear_Code ", "TextChanged");

            }

        }
        function DebitAc(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtDebitAc_Code .ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtDebitAc_Code .ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtDebitAc_Code .ClientID %>").val(unit);
                __doPostBack("txtDebitAc_Code ", "TextChanged");

            }

        }
        function CreditAc(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtCreditAc_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtCreditAc_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtCreditAc_Code.ClientID %>").val(unit);
                __doPostBack("txtCreditAc_Code", "TextChanged");

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
            window.open('../Transaction/pgeGeneralTransaction_Utility.aspx', '_self');
        }
        function PUrchaseOPen(gtranid) {
            var Action = 1;
            window.open('../Transaction/pgeGeneralTransaction.aspx?gtranid=' + gtranid + '&Action=' + Action, "_self");
        }

    </script>
    <script type="text/javascript">
        function DeleteConform() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= hdnfdocno.ClientID %>").value;
            var gtranid = document.getElementById("<%= hdnfgtranid.ClientID %>").value;

            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';

            var XML = "<ROOT><GenTranHead doc_no='" + DocNo + "' gtranid='" + gtranid + "' Company_Code='" + Company_Code + "' " +
                "Year_Code='" + Year_Code + "'></GenTranHead></ROOT>";
            var spname = "SP_GeneralTransaction";
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

            if (DocDates >= StartDate && DocDates <= EndDate) {
            }
            else {
                $("#<%=txtDOC_DATE.ClientID %>").focus();
                alert('Not A Valid Date Range')
                $("#loader").hide();
                return false;
            }


            if ($("#<%=txtDOC_DATE.ClientID %>").val() == "") {
                $("#<%=txtDOC_DATE.ClientID %>").focus();
                $("#loader").hide();
                return false;

            }



            var count = 0;
            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            if (gridView == null) {
                alert('Please Enter Transaction Details!');
                $("#loader").hide();
                return false;
            }
            var grdrow = gridView.getElementsByTagName("tr");
            var ro = (grdrow.length - 1);
            if (ro == 0) {
                alert('Please Enter Transaction Details!');
                $("#loader").hide();
                return false;
            }
            if (ro >= 1) {
                for (var i = 1; i < grdrow.length; i++) {
                    var action = gridView.rows[i].cells[14].innerHTML;
                    if (gridView.rows[i].cells[14].innerHTML == "D") {
                        count = count + 1;
                    }
                }
                if (ro == count) {
                    alert('Minimum One Transaction Details is compulsory!');
                    $("#loader").hide();
                    return false;
                }
            }
            return true;
        }
        function pagevalidation() {
            debugger;

            $("#loader").show();
            var Doc_No = 0, gtranid = 0, gtrandetailid = 0, GId = 0;
            var spname = "SP_GeneralTransaction";
            var XML = "<ROOT>";
            var status = document.getElementById("<%= btnSave.ClientID %>").value;
            if (status == "Update") {
                Doc_No = document.getElementById("<%= hdnfdocno.ClientID %>").value;
                gtranid = document.getElementById("<%= hdnfgtranid.ClientID %>").value;
            }
            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            var grdrow = gridView.getElementsByTagName("tr");
            var purchase_Id = $("#<%=lblPurchase_Id.ClientID %>").text();
            var doc_no = $("#<%=txtDOC_NO.ClientID %>").val();

            var Tran_Type = "ZT";
            // var Company_Code = '<%= Session["Company_Code"] %>';
            //var Year_Code = '<%= Session["year"] %>';
            var Branch_Code = '<%= Session["Branch_Code"] %>';


            var d = $("#<%=txtDOC_DATE.ClientID %>").val();
            var doc_date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
            var USER = '<%= Session["user"] %>';
            gtranid = document.getElementById("<%= hdnfgtranid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfgtranid.ClientID %>").value;
            if (gtranid == "" || gtranid == "&nbsp;") {
                gtranid = 0;
            }

            var DOCNO = "";
            if (status == "Save") {
                DOCNO = "Tran_Type='ZT'";
            }
            else {
                DOCNO = "doc_no='" + Doc_No + "' Tran_Type='ZT'";
            }
            var XML = XML + "<GenTranHead " + DOCNO + " doc_date='" + doc_date + "' gtranid='" + gtranid + "' " +
              "Created_By='" + USER + "' Modified_By='" + USER + "'>";
            //Detail Calculation
            var ddid = gtrandetailid;
            for (var i = 1; i < grdrow.length; i++) {
                var ID = gridView.rows[i].cells[2].innerHTML;
                var Company_Code = gridView.rows[i].cells[3].innerHTML;
                var Year_Code = gridView.rows[i].cells[5].innerHTML;
                var Debit_ac = gridView.rows[i].cells[7].innerHTML;
                var Credit_ac = gridView.rows[i].cells[9].innerHTML;
                var Amount = gridView.rows[i].cells[11].innerHTML;
                var Narration = gridView.rows[i].cells[12].innerHTML;
                if (Narration == "&nbsp;") {
                    Narration = "";
                }
                var dac = gridView.rows[i].cells[15].innerHTML;
                var cac = gridView.rows[i].cells[16].innerHTML;

                if (gridView.rows[i].cells[14].innerHTML == "A") {

                    XML = XML + "<GenTranDetailInsert doc_no='" + doc_no + "' detail_id='" + ID + "'  Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Debit_ac='" + Debit_ac + "' Credit_ac='" + Credit_ac + "' " +
                    "amount='" + Amount + "' narration='" + Narration + "' gtranid='" + gtranid + "' gtrandetailid='" + ddid + "'  Created_By='" + USER + "' Modified_By='" + USER + "'   dac='" + dac + "' cac='" + cac + "' Tran_Type='ZT'/>";
                    ddid = parseInt(ddid) + 1;
                }
                else if (gridView.rows[i].cells[14].innerHTML == "U") {
                    var gtrandetailid = gridView.rows[i].cells[13].innerHTML;
                    XML = XML + "<GenTranDetail doc_no='" + doc_no + "' detail_id='" + ID + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Debit_ac='" + Debit_ac + "' Credit_ac='" + Credit_ac + "' " +
                    "amount='" + Amount + "' narration='" + Narration + "' gtranid='" + gtranid + "' gtrandetailid='" + gtrandetailid + "'  Created_By='" + USER + "' Modified_By='" + USER + "'   dac='" + dac + "' cac='" + cac + "' Tran_Type='ZT'/>";

                }
                else if (gridView.rows[i].cells[14].innerHTML == "D") {
                    var gtrandetailid = gridView.rows[i].cells[13].innerHTML;
                    XML = XML + "<GenTranDetailDelete doc_no='" + doc_no + "' detail_id='" + ID + "'  Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Debit_ac='" + Debit_ac + "' Credit_ac='" + Credit_ac + "' " +
                  "amount='" + Amount + "' narration='" + Narration + "' gtranid='" + gtranid + "' gtrandetailid='" + gtrandetailid + "' Created_By='" + USER + "' Modified_By='" + USER + "'   dac='" + dac + "' cac='" + cac + "' Tran_Type='ZT'/>";

                }

                if (gridView.rows[i].cells[14].innerHTML != "D") {
                    // Gledger Effect 
                    var Order_Code = 1;

                    XML = XML + "<Ledger TRAN_TYPE='ZT' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Debit_ac + "' " +
                                                           "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='ZT' SORT_NO='" + Doc_No + "' ac='" + dac + "' vc='0' progid='5' tranid='0'/>";

                    Order_Code = Order_Code + 1;
                    XML = XML + "<Ledger TRAN_TYPE='ZT' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Credit_ac + "' " +
                                                           "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='ZT' SORT_NO='" + Doc_No + "' ac='" + cac + "' vc='0' progid='5' tranid='0'/>";
                }
            }

            XML = XML + "</GenTranHead></ROOT>";
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
                        window.open('../Transaction/pgeGeneralTransaction.aspx?gtranid=' + id + '&Action=1', "_self");

                    }
                }
                else {
                    var num = parseInt(response.d);

                    if (isNaN(num)) {
                        //alert(response.d)
                        swal("" + response.d + "", "", "warning");

                    }
                    else {
                        window.open('../Transaction/pgeGeneralTransaction_Utility.aspx', "_self");
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
            <asp:HiddenField ID="hdnfDebitAc" runat="server" />
            <asp:HiddenField ID="hdnfCreditAc" runat="server" />
            <asp:HiddenField ID="hdnfbk" runat="server" />
            <asp:HiddenField ID="hdnfac" runat="server" />
            <asp:HiddenField ID="hdnfpurcid" runat="server" />
            <asp:HiddenField ID="hdnfBrokerShort" runat="server" />
            <asp:HiddenField ID="hdnfMillshort" runat="server" />

            <asp:HiddenField ID="hdnfdocno" runat="server" />
            <asp:HiddenField ID="hdnfgtranid" runat="server" />
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
                                Text="Save" CssClass="btnHelp" Width="90px" TabIndex="35" Height="24px" ValidationGroup="add"
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
                        <td align="right">Entry No.:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDOC_NO" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtDOC_NO_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtdoc_no" runat="server" Text="..." Width="80px" OnClick="btntxtDOC_NO_Click"
                                CssClass="btnHelp" Height="24px" />
                            <asp:Label ID="lblPurchase_Id" runat="server" Font-Bold="true" Font-Italic="true"
                                Font-Names="verdana" Font-Size="Small" ForeColor="Red" Visible="false"></asp:Label>

                            Date:
                            <asp:TextBox ID="txtDOC_DATE" runat="Server" CssClass="txt" TabIndex="2" Width="100px"
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
                            <td align="left">Company Code:
                                <asp:TextBox ID="txtCompany_CODE" runat="Server" CssClass="txt" TabIndex="14" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtCompany_CODE_TextChanged"
                                    onkeydown="Company(event);"></asp:TextBox>
                                <asp:Button ID="btntxtCompany_CODE" runat="server" Text="..." OnClick="btntxtCompany_CODE_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblCompanyName" runat="server" CssClass="lblName"></asp:Label>
                                Year Code:
                                <asp:TextBox ID="txtYear_Code" runat="Server" AutoPostBack="false" CssClass="txt"
                                    Height="24px" OnTextChanged="txtYear_Code_TextChanged" Style="text-align: left;"
                                    TabIndex="18" Width="90px" onkeydown="Year(event);"></asp:TextBox>
                                <asp:Button ID="btntxtYear_Code" runat="server" CssClass="btnHelp" OnClick="btntxtYear_Code_Click"
                                    Text="..." />
                                <asp:Label ID="lblYearname" runat="server" CssClass="lblName"></asp:Label>
                                DebitAc Code:
                                <asp:TextBox ID="txtDebitAc_Code" runat="Server" AutoPostBack="false" CssClass="txt"
                                    Height="24px" OnTextChanged="txtDebitAc_Code_TextChanged" Style="text-align: left;"
                                    TabIndex="18" Width="90px" onkeydown="DebitAc(event);"></asp:TextBox>
                                <asp:Button ID="btntxtDebitAc_Code" runat="server" CssClass="btnHelp" OnClick="btntxtDebitAc_Code_Click"
                                    Text="..." />
                                <asp:Label ID="lblDebitAc_Code" runat="server" CssClass="lblName"></asp:Label>
                                CreditAc Code:
                                <asp:TextBox ID="txtCreditAc_Code" runat="Server" AutoPostBack="false" CssClass="txt"
                                    Height="24px" OnTextChanged="txtCreditAc_Code_TextChanged" Style="text-align: left;"
                                    TabIndex="18" Width="90px" onkeydown="CreditAc(event);"></asp:TextBox>
                                <asp:Button ID="btntxtCreditAc_Code" runat="server" CssClass="btnHelp" OnClick="btntxtCreditAc_Code_Click"
                                    Text="..." />
                                <asp:Label ID="lblCreditAc_Code" runat="server" CssClass="lblName"></asp:Label>
                                Amount:
                                <asp:TextBox ID="txtAMOUNT" runat="Server" CssClass="txt" TabIndex="19" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="true" ReadOnly="false"
                                    OnTextChanged="txtAMOUNT_TextChanged"></asp:TextBox>
                                Narration:
                                <asp:TextBox ID="txtNARRATION" runat="Server" CssClass="txt" TabIndex="20" Width="350px"
                                    TextMode="MultiLine" Height="50px" Style="text-align: left;" AutoPostBack="false"
                                    OnTextChanged="txtNARRATION_TextChanged"></asp:TextBox>
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

                        </td>
                        <td style="width: 100%;" align="left"></td>

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

