<%@ Page Title="Check Printing" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeCheckPrinting.aspx.cs" Inherits="Sugar_Transaction_pgeCheckPrinting" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="/Script/jquery-1.4.2.js"></script>--%>
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
        function CheckPrinting(Doc_No) {
            var tn;
            window.open('../Report/rptChqPrinting_Register.aspx?Doc_No=' + Doc_No);       //R=Redirected  O=Original 
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
                if (hdnfClosePopupValue == "txtCash_Bank") {
                    document.getElementById("<%= txtCash_Bank.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblCash_Bank.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtCash_Bank.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtpaymentNo") {
                    document.getElementById("<%= txtpaymentNo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lbltxtpayment.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtpaymentNo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtAc_Code") {
                    document.getElementById("<%= txtAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblAcname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtAc_Code.ClientID %>").focus();
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
        function CashBank(e) {
            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtCash_Bank.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var Accode = $("#<%=txtCash_Bank.ClientID %>").val();

                Accode = "0" + Accode;
                $("#<%=txtCash_Bank.ClientID %>").val(Accode);

                __doPostBack("txtCash_Bank", "TextChanged");

            }


        }
        function Payment(e) {
            if (e.keyCode == 112) {

                e.preventDefault();

                $("#<%=btntxtpaymentNo.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var Accode = $("#<%=txtpaymentNo.ClientID %>").val();

                Accode = "0" + Accode;
                $("#<%=txtpaymentNo.ClientID %>").val(Accode);

                __doPostBack("txtpaymentNo", "TextChanged");

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
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }

        }
        function changeno(e) {
            debugger;
            if (e.keyCode == 112) {

                e.preventDefault();
                // $("#<%=pnlPopup.ClientID %>").show();
                var edi = "txtEditDoc_No"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtEditDoc_No", "TextChanged");

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

        function SB() {

            var billno = document.getElementById('<%=txtDoc_No.ClientID %>').value;
            window.open('../Report/rptChequePrinting.aspx?billno=' + billno)
        }
        function MP() {

            var billno = document.getElementById('<%=txtDoc_No.ClientID %>').value;
            window.open('../Report/rptmultipleprint.aspx?billno=' + billno)
        }

    </script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 500px;
            height: 250px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" language="javascript">
        function DeleteConform() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= hdnfCheckDoc_No.ClientID %>").value;
            var Autoid = document.getElementById("<%= hdnfCheck_id.ClientID %>").value;

            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';
            var USER = '<%= Session["user"] %>';

            var XML = "<ROOT><CheckHead Doc_No='" + DocNo + "' Check_Id='" + Autoid + "' Company_Code='" + Company_Code + "' " +
                 "Year_Code='" + Year_Code + "'></CheckHead></ROOT>";
            var spname = "SP_Checkprinting_Head";
            var status = document.getElementById("<%= btnDelete.ClientID %>").value;
              ProcessXML(XML, status, spname);
          }
          function Validate() {
              debugger;
              $("#loader").show();
              // Validation
              var Outword_Date = '<%= Session["Outword_Date"] %>';
              var DocDate = $("#<%=txtDoc_Date.ClientID %>").val();

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
            if (Outword_Date == "") {
                alert('Update Post Date');
                $("#loader").hide();
                return false;
            }


              // Outword_Date = Outword_Date.slice(6, 11) + Outword_Date.slice(3, 5) + "/" + Outword_Date.slice(0, 2);
            DocDate = DocDate.slice(6, 11) + "/" + DocDate.slice(3, 5) + "/" + DocDate.slice(0, 2);
            if (DocDate < Outword_Date) {
                alert('GST Return Fined please Do not edit Record');
                $("#loader").hide();
                return false;
            }


            var count = 0;
            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            if (gridView == null) {
                alert('Please Enter  Details!');
                $("#loader").hide();
                return false;
            }
            var grdrow = gridView.getElementsByTagName("tr");
            var ro = (grdrow.length - 1);
            if (ro == 0) {
                alert('Please Enter  Details!');
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

            }
            return true;
        }
        function pagevalidation() {
            debugger;
            try {
                $("#loader").show();
                var Doc_No = 0, Check_Id = 0, Check_Detail_Id = 0;
                var XML = "<ROOT>";
                var spname = "SP_Checkprinting_Head";
                var Tran_Type = "CQ";
                var status = document.getElementById("<%= btnSave.ClientID %>").value;
                if (status == "Update") {
                    Doc_No = document.getElementById("<%= hdnfCheckDoc_No.ClientID %>").value;
                    Check_Id = document.getElementById("<%= hdnfCheck_id.ClientID %>").value; 
                }
                var Payment_No = $("#<%=txtpaymentNo.ClientID %>").val() == "" ? 0 : $("#<%=txtpaymentNo.ClientID%>").val();
                var d = $("#<%=txtDoc_Date.ClientID %>").val();
                var doc_date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                var Authorised = $("#<%=txtAuthorised.ClientID %>").val() == "" ? 0 : $("#<%=txtAuthorised.ClientID%>").val();
                var Cash_Bank = $("#<%=txtCash_Bank.ClientID %>").val() == "" ? 0 : $("#<%=txtCash_Bank.ClientID%>").val();
                var USER = '<%= Session["user"] %>';
                var Company_Code = '<%= Session["Company_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';
                var pid = document.getElementById("<%= hdnfpaymentcode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpaymentcode.ClientID %>").value;
                var cbid = document.getElementById("<%= hdnfcbcode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfcbcode.ClientID %>").value;
                debugger;
                var DOCNO = "";
                //if (status == "Save") {
                //    HeadInsertUpdate = "Modified_By='" + USER + "' Created_By=''";
                //    DOCNO = "Doc_No='" + Doc_No + "'";
                //}
                //else {
                //    HeadInsertUpdate = "Modified_By='" + USER + "' Created_By=''";
                //    DOCNO = "Doc_No='" + Doc_No + "'";
                //}
                HeadInsertUpdate = "Modified_By='" + USER + "' Created_By=''";
                if (status == "Update") {
                    DOCNO = "Doc_No='" + Doc_No + "'";
                }
                XML = XML + "<CheckHead " + DOCNO + " Doc_Date ='" + doc_date + "' Tran_Type ='" + Tran_Type + "' Cash_Bank = '" + Cash_Bank + "' Authorised ='" + Authorised
                + "' Company_Code = '" + Company_Code + "'  " + HeadInsertUpdate + " Year_Code = '" + Year_Code + "' Payment_No= '" + Payment_No + "' Check_Id = '" + Check_Id + "'  pid = '" + pid + "' cbid = '" + cbid + "'>";

                var gridView = document.getElementById("<%=grdDetail.ClientID %>");
                var grdrow = gridView.getElementsByTagName("tr");

                var ddid = Check_Detail_Id;
                for (var i = 1; i < grdrow.length; i++) {
                    var ID = gridView.rows[i].cells[2].innerHTML;
                    var Ac_Code = gridView.rows[i].cells[3].innerHTML;
                    var ChqCaption = gridView.rows[i].cells[5].innerHTML;
                    var Amount = gridView.rows[i].cells[6].innerHTML;
                    var Narration = gridView.rows[i].cells[7].innerHTML;
                    if (Narration == "&nbsp;") {
                        Narration = "";
                    }
                    var detailBank_Date = gridView.rows[i].cells[8].innerHTML;
                    var Bank_Date = detailBank_Date.slice(6, 11) + "/" + detailBank_Date.slice(3, 5) + "/" + detailBank_Date.slice(0, 2);
                    var Mark = gridView.rows[i].cells[9].innerHTML;
                    if (Mark == "&nbsp;") {
                        Mark = "";
                    }
                    var Cheque_No = gridView.rows[i].cells[10].innerHTML;
                    var Berror = gridView.rows[i].cells[11].innerHTML;
                    var Mobile_No = gridView.rows[i].cells[12].innerHTML;
                    var acid = gridView.rows[i].cells[16].innerHTML;

                    //var ic = gridView.rows[i].cells[16].innerHTML;

                    if (gridView.rows[i].cells[14].innerHTML == "A") {

                        XML = XML + "<CheckDetailInsert " + DOCNO + "  Detail_ID= '" + ID + "' Ac_Code= '" + Ac_Code + "' Amount= '" + Amount
                            + "' Narration = '" + Narration + "' Bank_Date= '" + Bank_Date + "' Mark= '" + Mark + "' Cheque_No= '" + Cheque_No
                            + "' Berror= '" + Berror + "' Mobile_No= '" + Mobile_No + "' Tran_Type='" + Tran_Type
                            + "' Company_Code = '" + Company_Code + "' Created_By = '" + USER + "' Modified_By ='" + USER + "' Year_Code ='" + Year_Code
                            + "' Check_Detail_Id = '" + ddid + "' Check_Id = '" + Check_Id + "' acid = '" + acid + "' ChqCaption= '" + ChqCaption + "'/>";
                        ddid = parseInt(ddid) + 1;
                    }
                    else if (gridView.rows[i].cells[14].innerHTML == "U") {
                        var Check_Id = gridView.rows[i].cells[13].innerHTML;
                        XML = XML + "<CheckDetail  " + DOCNO + " Detail_ID= '" + ID + "'   Ac_Code= '" + Ac_Code + "' Amount= '" + Amount
                       + "' Narration = '" + Narration + "' Bank_Date= '" + Bank_Date + "' Mark= '" + Mark + "' Cheque_No= '" + Cheque_No
                       + "' Berror= '" + Berror + "' Mobile_No= '" + Mobile_No + "' Tran_Type='" + Tran_Type
                       + "' Company_Code = '" + Company_Code + "' Created_By = '" + USER + "' Modified_By ='" + USER + "' Year_Code ='" + Year_Code
                       + "' Check_Detail_Id = '" + Check_Id + "' Check_Id = '" + Check_Id + "' acid = '" + acid + "' ChqCaption= '" + ChqCaption + "'/>";
                    }
                    else if (gridView.rows[i].cells[14].innerHTML == "D") {
                        var Check_Id = gridView.rows[i].cells[13].innerHTML;
                        XML = XML + "<CheckDetailDelete " + DOCNO + " Detail_ID= '" + ID + "'  Ac_Code= '" + Ac_Code + "' Amount= '" + Amount
                       + "' Narration = '" + Narration + "' Bank_Date= '" + Bank_Date + "' Mark= '" + Mark + "' Cheque_No= '" + Cheque_No
                       + "' Berror= '" + Berror + "' Mobile_No= '" + Mobile_No + "' Tran_Type='" + Tran_Type
                       + "' Company_Code = '" + Company_Code + "' Created_By = '" + USER + "' Modified_By ='" + USER + "' Year_Code ='" + Year_Code
                       + "' Check_Detail_Id = '" + Check_Id + "' Check_Id = '" + Check_Id + "' acid = '" + acid + "' ChqCaption= '" + ChqCaption + "'/>";
                    }

                    if (gridView.rows[i].cells[14].innerHTML != "D") {
                        var Order_Code = 1;
                        Creditnara = "" + Narration + "  Cheque_No:" + Cheque_No + "";
                        Debitnara = "" + Narration + "   Cheque_No:" + Cheque_No + "";

                        XML = XML + "<Ledger TRAN_TYPE='CQ' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Cash_Bank + "' " +
                                                                  "UNIT_code='0' NARRATION='" + Creditnara + "' AMOUNT='" + Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                  "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='0' " +
                                                                  "SORT_TYPE='CQ' SORT_NO='" + Doc_No + "' ac='" + cbid + "' vc='0' progid='6' tranid='0' saleid='" + Check_Id + "'/>";
                        Order_Code = Order_Code + 1;
                        XML = XML + "<Ledger TRAN_TYPE='CQ' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                                                                 "UNIT_code='0' NARRATION='" + Debitnara + "' AMOUNT='" + Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                                 "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='0' " +
                                                                 "SORT_TYPE='CQ' SORT_NO='" + Doc_No + "' ac='" + acid + "' vc='0' progid='6' tranid='0' saleid='" + Check_Id + "'/>";
                    }
                    //if (Amount > 0) {
                    //    Order_Code = parseInt(Order_Code) + 1;
                    //    XML = XML + "<Ledger TRAN_TYPE='CQ' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                    //                                         "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                    //                                         "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='0' " +
                    //                                         "SORT_TYPE='CQ' SORT_NO='" + Doc_No + "' ac='" + acid + "' vc='0' progid='6' tranid='0' saleid='" + Check_Id + "'/>";


                    //    //XML = XML + "<Ledger TRAN_TYPE='CQ' CASHCREDIT='' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Ac_Code + "' " +
                    //    //                                          "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                    //    //                                          "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='0' " +
                    //    //                                          "SORT_TYPE='CQ' SORT_NO='" + Doc_No + "' ac='" + acid + "' vc='0' progid='6' tranid='0' saleid='" + Check_Id + "'/>";
                }
                XML = XML + "</CheckHead></ROOT>";
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
                            alert('Sucessfully Added Record !!! Doc_No=' + doc)
                        }
                        else {
                            alert('Sucessfully Updated Record !!! Doc_No=' + doc)
                        }
                        window.open('../Transaction/pgeCheckPrinting.aspx?Doc_No=' + doc + '&Action=1', "_self");

                    }
                }
                else {
                    var num = parseInt(response.d);

                    if (isNaN(num)) {
                        alert(response.d)

                    }
                    else {
                        window.open('../Transaction/pgeCheckPrintingUtility.aspx', "_self");
                    }
                }

            }
        }
    </script>
    <script type="text/javascript">
        function ShowProgress() {
            debugger;
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 10);
            window.location.reload();
        }

        function displaynone() {
            debugger;
            $("#masterdiv").css("Display", "none");
        }
        //        $('form').live("submit", function () {
        //            ShowProgress();
        //        });
    </script>
    <style type="text/css">
        .bagroundPopup {
            opacity: 0.6;
            background-color: Black;
        }

        #clientsDropDown {
            position: absolute;
            bottom: 0;
            width: 400px;
            background-color: Black;
            padding-bottom: 2%;
            z-index: 100;
        }

        #clientsOpen {
            background: url("images/open.png") no-repeat scroll 68px 10px #414142;
            color: #ececec;
            cursor: pointer;
            float: right;
            font-size: 26px;
            margin: -2px 0 0 10%;
            padding: 0 15px 2px;
            text-decoration: none;
            width: 63px;
        }

        #clientsCTA {
            background: #414142;
            width: 100%;
            color: #CCCCCC;
            text-align: center;
            font-size: 46px;
            margin: 0;
            padding: 30px 0;
            text-decoration: none;
        }

        #clientsDropDown .clientsClose {
            background-image: url(images/close.png);
        }

        #clientsDropDown #clientsDashboard {
            display: block;
        }
    </style>
    <script type="text/javascript">
        function disableClick(elem) {
            elem.disabled = true;
        }
    </script>
    <script type="text/javascript">
        function BACK() {

            window.open('../Transaction/pgeCheckPrintingUtility.aspx', '_self');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Check Printing " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
            <asp:HiddenField ID="hdnfCheckDoc_No" runat="server" />
            <asp:HiddenField ID="hdnfCheck_id" runat="server" />
            hdnfaccode
            <asp:HiddenField ID="hdnfpaymentcode" runat="server" />
            <asp:HiddenField ID="hdnfaccode" runat="server" />
            <asp:HiddenField ID="hdnfcbcode" runat="server" />

            <table width="100%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />

                        <asp:Button OnClientClick="disableClick(this)" OnClick="btnSave_Click" runat="server"
                            Text="Save" UseSubmitBehavior="false" ID="btnSave" CssClass="btnHelp" ValidationGroup="add"
                            Width="90px" Height="24px" />

                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />

                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px" />

                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" Height="24px" TabIndex="48" OnClientClick="BACK()" />

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" />

                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnFirst_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnPrevious_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnNext_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnLast_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" Width="90px"
                            ValidationGroup="Save" Height="24px" OnClientClick="SB();" />
                        <asp:Button ID="btnmultipleprint" runat="server" Text="Multiple Print" CssClass="btnHelp"
                            Width="90px" ValidationGroup="Save" Height="24px" OnClientClick="MP();" />
                        <asp:Button ID="btnCheck_Printing" runat="server" Text="Print List" CssClass="btnHelp"
                            Height="24px" OnClick="btnCheck_Printing_Click" Width="90px" />
                        <asp:Button ID="btnAuthentication" runat="server" CssClass="btnHelp" OnClick="btnAuthentication_Click"
                            Width="200px" Height="24px" TabIndex="23" Text="Authentication" OnClientClick="authenticate()"
                            Visible="false" />
                        <asp:Button Text="SMS" ID="btnpartysendsms" CommandName="sms" CssClass="btnHelp"
                            Height="30px" Width="90px" runat="server" OnCommand="btnSMS_Click" />

                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 100%;" align="left" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                        <tr>
                            <td align="left">Change No
                                <asp:TextBox Height="24px" ID="txtEditDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEditDoc_No_TextChanged"
                                    onKeyDown="changeno(event);"></asp:TextBox>
                                Doc No
                                <asp:TextBox Height="24px" ID="txtDoc_No" runat="Server" CssClass="txt" TabIndex="2"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDoc_No_TextChanged"></asp:TextBox>
                                <asp:Button Width="90px" Height="24px" ID="btntxtDoc_No" runat="server" Text="..."
                                    OnClick="btntxtDoc_No_Click" CssClass="btnHelp" />
                                <asp:Label ID="lblDoc_No" runat="server" CssClass="lblName"></asp:Label>
                                Doc Date
                                <asp:TextBox Height="24px" ID="txtDoc_Date" runat="Server" CssClass="txt" TabIndex="3"
                                    Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtDoc_Date_TextChanged"
                                    onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                                <asp:Image ID="imgcalendertxtDoc_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                    Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtDoc_Date"
                                        runat="server" TargetControlID="txtDoc_Date" PopupButtonID="imgcalendertxtDoc_Date"
                                        Format="dd/MM/yyyy">
                                    </ajax1:CalendarExtender>
                                From
                                <asp:TextBox Height="24px" ID="txtfrom" runat="Server" CssClass="txt" TabIndex="1"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtfrom_TextChanged"
                                    onKeyDown="changeno(event);"></asp:TextBox>
                                To
                                <asp:TextBox Height="24px" ID="txtTo" runat="Server" CssClass="txt" TabIndex="1"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTo_TextChanged"
                                    onKeyDown="changeno(event);"></asp:TextBox>
                            </td>
                            <%--<td align="left" style="width: 10%;">
                                Tran Type
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtTran_Type" runat="Server" CssClass="txt" TabIndex="3"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTran_Type_TextChanged"></asp:TextBox>
                            </td>--%>
                        </tr>
                        <tr>
                            <td align="left">Payment No
                                <asp:TextBox Height="24px" ID="txtpaymentNo" runat="Server" CssClass="txt" TabIndex="4"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtpaymentNo_TextChanged"
                                    onKeyDown="Payment(event);"></asp:TextBox>
                                <asp:Button Width="20px" Height="24px" ID="btntxtpaymentNo" runat="server" Text="..."
                                    OnClick="btntxtpaymentNo_Click" CssClass="btnHelp" />
                                <asp:Label ID="lbltxtpayment" runat="server" CssClass="lblName"></asp:Label>
                                Cash Bank
                                <asp:TextBox Height="24px" ID="txtCash_Bank" runat="Server" CssClass="txt" TabIndex="5"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCash_Bank_TextChanged"
                                    onKeyDown="CashBank(event);"></asp:TextBox>
                                <asp:Button Width="20px" Height="24px" ID="btntxtCash_Bank" runat="server" Text="..."
                                    OnClick="btntxtCash_Bank_Click" CssClass="btnHelp" />
                                <asp:Label ID="lblCash_Bank" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:TextBox Height="24px" ID="txtAuthorised" runat="Server" CssClass="txt" TabIndex="66"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtAuthorised_TextChanged"
                                    Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left">
                                <asp:Button ID="btnOpenDetailsPopup" runat="server" Text="ADD" CssClass="btnHelp"
                                    Width="80px" Height="25px" OnClick="btnOpenDetailsPopup_Click" TabIndex="28"
                                    Visible="false" />
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
                        <td colspan="2" align="center" style="background-color: lightslategrey; color: White;">
                            <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Text="Enter Name"></asp:Label>
                        </td>
                    </tr>
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
                        <td align="left">Ac Code
                            <asp:TextBox ID="txtAc_Code" runat="Server" CssClass="txt" TabIndex="7" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtAc_Code_TextChanged"
                                Height="24px" onKeyDown="AcCode(event);"></asp:TextBox>
                            <asp:Button ID="btntxtAc_Code" runat="server" Text="..." OnClick="btntxtAc_Code_Click"
                                CssClass="btnHelp" />
                            <asp:Label ID="lblAcname" runat="server" CssClass="lblName"></asp:Label>
                            Chq Caption
                            <asp:TextBox ID="txtCqucaption" runat="Server" CssClass="txt" TabIndex="8" Width="220px"
                                Style="text-align: left;" AutoPostBack="false" Height="24px"></asp:TextBox>
                            Amount
                            <asp:TextBox ID="txtValue" runat="Server" CssClass="txt" TabIndex="9" Width="120px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtValue_TextChanged"
                                Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="filterAmount" runat="server" TargetControlID="txtValue"
                                FilterType="Custom,Numbers" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Narration
                            <asp:TextBox ID="txtNarration" runat="Server" CssClass="txt" TabIndex="10" Width="120px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtNarration_TextChanged"
                                Height="24px"></asp:TextBox>
                            Bank Date
                            <asp:TextBox ID="txtBank_Date" runat="Server" CssClass="txt" TabIndex="11" Width="90px"
                                Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtBank_Date_TextChanged"
                                onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtBank_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtBank_Date"
                                    runat="server" TargetControlID="txtBank_Date" PopupButtonID="imgcalendertxtBank_Date"
                                    Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Mark
                            <asp:TextBox ID="txtMark" runat="Server" CssClass="txt" TabIndex="11" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtMark_TextChanged"
                                Height="24px"></asp:TextBox>
                            Cheque No
                            <asp:TextBox ID="txtCheque_No" runat="Server" CssClass="txt" TabIndex="12" Width="300px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCheque_No_TextChanged"
                                Height="24px"></asp:TextBox>
                            Berror
                            <asp:TextBox ID="txtBerror" runat="Server" CssClass="txt" TabIndex="13" Width="90px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBerror_TextChanged"
                                Height="24px"></asp:TextBox>
                            Mobile No
                            <asp:TextBox ID="txtMobile_No" runat="Server" CssClass="txt" TabIndex="14" Width="120px"
                                Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtMobile_No_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                Height="25px" OnClick="btnAdddetails_Click" TabIndex="15" />
                            <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp" Width="80px"
                                Height="25px" OnClick="btnClosedetails_Click" TabIndex="16" />
                            <asp:Button ID="btnCheck" runat="server" Text="Check All" CssClass="btnHelp" Width="100px"
                                Height="25px" OnClick="btnCheck_Click" TabIndex="30" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="width: 100%; position: relative;">
                <asp:UpdatePanel ID="upGrid" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlgrdDetail" runat="server" ScrollBars="Both" Height="350px" Width="1500px"
                            BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true" Font-Names="Verdana"
                            Font-Size="11px" BackColor="SeaShell" Style="margin-left: 30px; float: left;">
                            <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#397CBB"
                                HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="150%"
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
                                    <asp:BoundField DataField="Detail_ID" HeaderText="Detail_ID" />
                                    <asp:BoundField DataField="Ac_Code" HeaderText="Ac_Code" />
                                    <asp:BoundField DataField="Ac_Code_Name" HeaderText="Ac_Code_Name" />
                                    <asp:BoundField DataField="ChqCaption" HeaderText="ChqCaption" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                    <asp:BoundField DataField="Narration" HeaderText="Narration" />
                                    <asp:BoundField DataField="Bank_Date" HeaderText="Bank_Date" />
                                    <asp:BoundField DataField="Mark" HeaderText="Mark" />
                                    <asp:BoundField DataField="Cheque_No" HeaderText="Cheque_No" />
                                    <asp:BoundField DataField="Berror" HeaderText="Berror" />
                                    <asp:BoundField DataField="Mobile_No" HeaderText="Mobile_No" />
                                    <asp:BoundField DataField="Check_Detail_Id" HeaderText="Check_Detail_Id" />
                                    <asp:BoundField DataField="rowAction" HeaderText="rowAction" />
                                    <asp:BoundField DataField="SrNo" HeaderText="SrNo" />
                                    <asp:BoundField DataField="acid" HeaderText="acid" />
                                    <asp:TemplateField HeaderText="SMS">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkMsg" Checked="false" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="25px" Wrap="false" ForeColor="Black" />
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
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
                                Style="z-index: 5000; float: right; overflow: auto; height: 680px">
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
            <div id="pnlCity" class="city" style="display: none; width: 300px; height: 100px; background-image: url('http://localhost:1145/accowebBhavaniNew/Images/loader.png'); background-repeat: no-repeat; display: inline-block;">
                <asp:ImageButton runat="server" ID="imgClose" ImageUrl="~/Images/closebtn.jpg" Width="20px"
                    Height="20px" Style="float: right; vertical-align: top;" ToolTip="Close" OnClick="imgClose_Click" />
                <table cellspacing="7">
                    <tr>
                        <td colspan="2" align="center">
                            <h3 style="color: White; margin-top: 20px;">
                                <asp:Label Text="Please Wait........." runat="server" ID="lblsucess" Style="color: Orange;" />
                            </h3>
                        </td>
                    </tr>
                </table>
                <asp:Button runat="server" ID="btn12" Style="display: none;" />
                <ajax1:ModalPopupExtender ID="modalCity" BackgroundCssClass="bagroundPopup" TargetControlID="btn12"
                    BehaviorID="mpe" PopupControlID="pnlCity" runat="server">
                </ajax1:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="80%" align="left">
        <tr>
            <td style="width: 80%;"></td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Cheque Amount" Font-Bold="true" />
                <asp:Label runat="server" ID="lblChqAmountBalance" Font-Bold="true" Font-Size="X-Large"
                    ForeColor="Blue" />
                <asp:Label runat="server" ID="lblDeb_Credit" Font-Bold="true" Font-Size="X-Large"
                    ForeColor="Blue" />
            </td>
        </tr>
    </table>
    <%--  <div class="loading" align="center" id="masterdiv" >
        Loading. Please wait.<br />
        <br />
        <img src="../../Images/loader.png" alt="" />
    </div>--%>
</asp:Content>
