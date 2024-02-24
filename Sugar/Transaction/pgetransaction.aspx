<%@ Page Title="Transaction" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgetransaction.aspx.cs" Inherits="Sugar_Transaction_pgetransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>

    <link href="../../CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../../JQuery/jquery-1.4.1.js" type="text/javascript"></script>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../../JQuery/jquery.keynavigation.js" type="text/javascript"></script>
    <link type="text/css" href="../../menu/menu.css" rel="stylesheet" />
    <script type="text/javascript" src="../../menu/jquery.js"></script>
    <script type="text/javascript" src="../../menu/menu.js"></script>
    <script type="text/javascript" src="../../JS/DateValidation.js"> </script>

    <script src="../../JS/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../Scripts/selectfirstrow.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../JS/DateValidation.js">
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
        function Back() {
            window.open('../Transaction/PgeJVUtility.aspx', '_self');
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
                if (hdnfClosePopupValue == "txtdebit_ac") {
                    document.getElementById("<%=txtdebit_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtnarration") {
                    document.getElementById("<%=txtnarration.ClientID %>").focus();
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

                if (hdnfClosePopupValue == "txtdebit_ac") {
                    document.getElementById("<%=txtdebit_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblDebit_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtdebit_ac.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtnarration") {
                    document.getElementById("<%=txtnarration.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;

                    document.getElementById("<%=txtnarration.ClientID %>").focus();

                }
                if (hdnfClosePopupValue == "txtdoc_no") {
                    document.getElementById("<%=txtdoc_no.ClientID %>").disabled = false;
                    document.getElementById("<%=txtdoc_no.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;

                    document.getElementById("<%=txtdoc_date.ClientID %>").focus();
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
        function Ac_Code(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtdebit_ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtdebit_ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtdebit_ac.ClientID %>").val(unit);
                __doPostBack("txtdebit_ac", "TextChanged");

            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }

        }
        function Narration(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtnarration.ClientID %>").click();

            }


        }
    </script>
  <script type="text/javascript">
      function DeleteConform() {
          debugger;
          $("#loader").show();
          var DocNo = document.getElementById("<%= hdnfjvdocno.ClientID %>").value;
          var jvid = document.getElementById("<%= hdnfjvid.ClientID %>").value;

          var Branch_Code = '<%= Session["Branch_Code"] %>';
          var Company_Code = '<%= Session["Company_Code"] %>';
          var Year_Code = '<%= Session["year"] %>';

          var XML = "<ROOT><JVHead doc_no='" + DocNo + "' tranid='" + jvid + "' company_code='" + Company_Code + "' " +
               "year_code='" + Year_Code + "'></JVHead></ROOT>";
          var spname = "transactionDetail";
          var status = document.getElementById("<%= btnDelete.ClientID %>").value;
            ProcessXML(XML, status, spname);
        }
        function Validate() {
            $("#loader").show();

            var StartDate = '<%= Session["Start_Date"] %>';
             var EndDate = '<%= Session["End_Date"] %>';

             var d = $("#<%=txtdoc_date.ClientID %>").val();
             var DocDates = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

             StartDate = StartDate.slice(6, 11) + "/" + StartDate.slice(3, 5) + "/" + StartDate.slice(0, 2);
             EndDate = EndDate.slice(6, 11) + "/" + EndDate.slice(3, 5) + "/" + EndDate.slice(0, 2);

             if (DocDates >= StartDate && DocDates <= EndDate) {
             }
             else {
                 $("#<%=txtdoc_date.ClientID %>").focus();
                alert('Not A Valid Date Range')
                $("#loader").hide();
                return false;
            }

            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
             var grdrow = gridView.getElementsByTagName("tr");
             var count = 0;
             if (grdrow.length > 1) {

                 for (var i = 0; i < grdrow.length; i++) {
                     if (gridView.rows[i].cells[9].innerHTML == "D" || gridView.rows[i].cells[9].innerHTML == "R") {
                         count++;
                     }
                     if (grdrow.length == count) {
                         $("#loader").hide();
                         alert('Please Enter Purchase Details!')

                         return false;
                     }
                 }

             }
             var lbldiff = $("#<%=lbldiff.ClientID %>").text();
            //if (lbldiff !== "0.00" && lbldiff != "0") {
            //    $("#loader").hide();
            //    alert('Difference must bo zero!')
            //    return false;
            //}
            return true;
        }
        function pagevalidation() {
            debugger;
            try {
                $("#loader").show();
                var Doc_no = 0, JVId = 0, JVDetailId = 0;
                var status = document.getElementById("<%= btnSave.ClientID %>").value;
                if (status == "Update") {
                    Doc_no = document.getElementById("<%= hdnfjvdocno.ClientID %>").value;
                    JVId = document.getElementById("<%= hdnfjvid.ClientID %>").value;
                }
                var spname = "transactionDetail";
                var XML = "<ROOT>";
                var gridView = document.getElementById("<%=grdDetail.ClientID %>");
                var grdrow = gridView.getElementsByTagName("tr");

                var Doc_no = $("#<%=txtdoc_no.ClientID %>").val();

                var Tran_Type = "JV";
                var Company_Code = '<%= Session["Company_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';
                var Branch_Code = '<%= Session["Branch_Code"] %>';
                var cashbank = 0;
                var USER = '<%= Session["user"] %>';
                var totalamount = 0.00;
                var final_total = 0.00;
                var d = $("#<%=txtdoc_date.ClientID %>").val();
                var doc_date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);


                var JVInsertUpdate = "";


                for (var i = 1; i < grdrow.length; i++) {


                    if (gridView.rows[i].cells[9].innerHTML != "D") {

                        totalamount = gridView.rows[i].cells[6].innerHTML;
                        final_total = parseFloat(+final_total) + (+totalamount);

                    }
                }
                total = final_total;
                if (status == "Save") {
                    JVInsertUpdate = "tran_type='" + Tran_Type + "'";
                }
                else {
                    JVInsertUpdate = "tran_type='" + Tran_Type + "' doc_no='" + Doc_no + "'";
                }

                XML = XML + "<JVHead " + JVInsertUpdate + " doc_date='" + doc_date + "' cashbank='" + cashbank + "' " +
               "total='" + total + "' company_code='" + Company_Code + "' year_code='" + Year_Code + "' cb='0' tranid='" + JVId + "'>";

                var Order_Code = 1;
                var Gledger_Insert = ""; Gledger_values = "";
                var Gledger_Delete = "";

                var ddid = JVDetailId;
                for (var i = 1; i < grdrow.length; i++) {
                    var Grid_Id = gridView.rows[i].cells[2].innerHTML;
                    var Dr_Ac_Code = gridView.rows[i].cells[3].innerHTML;
                    var Cr_Ac_Code = 0;
                    var CA_Id = 0;
                    var Grid_amount = gridView.rows[i].cells[6].innerHTML;
                    var DRCRGrid = gridView.rows[i].cells[5].innerHTML;
                    var Narration = gridView.rows[i].cells[7].innerHTML;
                    var jvdetailid = gridView.rows[i].cells[8].innerHTML;
                    var Ac_id = gridView.rows[i].cells[11].innerHTML;

                    if (gridView.rows[i].cells[9].innerHTML == "A") {

                        XML = XML + "<JVDetailInsert Tran_Type='" + Tran_Type + "' doc_no='" + Doc_no + "' doc_date='" + doc_date + "' detail_id='" + Grid_Id + "' debit_ac='" + Dr_Ac_Code + "' " +
                        "credit_ac='" + Cr_Ac_Code + "' Unit_Code='0' amount='" + Grid_amount + "' " +
               "narration='" + Narration + "' narration2='' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
               "Created_By='" + USER + "' Modified_By='" + USER + "' Voucher_No='0' Voucher_Type='' Adjusted_Amount='0.00' " +
               "Tender_No='0' TenderDetail_ID='0' drpFilterValue='' CreditAcAdjustedAmount='0.00' Branch_name='' " +
               "YearCodeDetail='0' tranid='" + JVId + "' ca='0' uc='0' tenderdetailid='0' sbid='0' da='" + Ac_id + "' trandetailid='" + ddid + "' drcr='" + DRCRGrid + "'/>";
                        ddid = parseInt(ddid) + 1;
                    }
                    else if (gridView.rows[i].cells[9].innerHTML == "U") {
                        var id = gridView.rows[i].cells[8].innerHTML;
                        XML = XML + "<JVDetailUpdate Tran_Type='" + Tran_Type + "' doc_no='" + Doc_no + "' doc_date='" + doc_date + "' detail_id='" + Grid_Id + "' debit_ac='" + Dr_Ac_Code + "' " +
                        "credit_ac='" + Cr_Ac_Code + "' Unit_Code='0' amount='" + Grid_amount + "' " +
               "narration='" + Narration + "' narration2='' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
               "Created_By='" + USER + "' Modified_By='" + USER + "' Voucher_No='0' Voucher_Type='' Adjusted_Amount='0.00' " +
               "Tender_No='0' TenderDetail_ID='0' drpFilterValue='' CreditAcAdjustedAmount='0.00' Branch_name='' " +
               "YearCodeDetail='0' tranid='" + JVId + "' ca='0' uc='0' tenderdetailid='0' sbid='0' da='" + Ac_id + "' trandetailid='" + id + "' drcr='" + DRCRGrid + "'/>";
                    }
                    else if (gridView.rows[i].cells[9].innerHTML == "D") {
                        var id = gridView.rows[i].cells[8].innerHTML;
                        XML = XML + "<JVDetailDelete Tran_Type='" + Tran_Type + "' doc_no='" + Doc_no + "' doc_date='" + doc_date + "' detail_id='" + Grid_Id + "' debit_ac='" + Dr_Ac_Code + "' " +
                        "credit_ac='" + Cr_Ac_Code + "' Unit_Code='0' amount='" + Grid_amount + "' " +
               "narration='" + Narration + "' narration2='' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
               "Created_By='" + USER + "' Modified_By='" + USER + "' Voucher_No='0' Voucher_Type='' Adjusted_Amount='0.00' " +
               "Tender_No='0' TenderDetail_ID='0' drpFilterValue='' CreditAcAdjustedAmount='0.00' Branch_name='' " +
               "YearCodeDetail='0' tranid='" + JVId + "' ca='0' uc='0' tenderdetailid='0' sbid='0' da='" + Ac_id + "' trandetailid='" + id + "' drcr='" + DRCRGrid + "'/>";
                    }

                    Order_Code = Order_Code + 1;
                    //Gledger_values = Gledger_values + "('JV','','" + Doc_no + "','" + doc_date + "','" + Dr_Ac_Code + "','0','" + Narration + "'," +
                    //   " '" + Grid_amount + "', null,null,null,'" + Company_Code + "','" + Year_Code + "','" + Order_Code + "','" + DRCRGrid + "',0,'0','" + Branch_Code + "','JV','" + Doc_no + "'," +
                    //                                    "  case when 0='" + Ac_id + "' then null else '" + Ac_id + "' end,'0','3','0','" + GId + "'),";

                    XML = XML + "<Ledger TRAN_TYPE='JV' CASHCREDIT='' DOC_NO='" + Doc_no + "' DOC_DATE='" + doc_date + "' AC_CODE='" + Dr_Ac_Code + "' " +
                                                        "UNIT_code='0' NARRATION='" + Narration + "' AMOUNT='" + Grid_amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + DRCRGrid + "' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='JV' SORT_NO='" + Doc_no + "' ac='" + Ac_id + "' vc='0' progid='3' tranid='0'/>";
                }
                XML = XML + "</JVHead></ROOT>";
                ProcessXML(XML, status, spname)

            }
            catch (exx) {
                $("#loader").hide();
                alert(exx);
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
                        window.open('../Transaction/pgetransaction.aspx?tranid=' + id + '&Action=1', "_self");

                    }

                }
                else {
                    var num = parseInt(response.d);

                    if (isNaN(num)) {
                        alert(response.d)

                    }
                    else {
                        window.open('../Transaction/pgeTransactionUtility.aspx', "_self");
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
            <asp:Label ID="label1" runat="server" Text="   Transaction   " Font-Names="verdana"
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
            <asp:HiddenField ID="hdnfAccode" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfjvdocno" runat="server" />
            <asp:HiddenField ID="hdnfjvid" runat="server" />
              <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table width="80%" align="left">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                            &nbsp;
                        <asp:Button ID="btnSave" OnClientClick="if (!Validate()) return false;" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" />
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
                                ValidationGroup="save" OnClientClick="Back()" Height="24px" />
                        </td>
                    </tr>

                </table>
                <table width="90%" align="left" cellspacing="5">
                    <tr>
                        <td align="left">Change No:
                            <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                            &nbsp;<asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>

                    </tr>
                    <tr>

                        <tr>
                            <td align="left">Entry No: &nbsp;&nbsp;
                                <asp:TextBox ID="txtdoc_no" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtdoc_no_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtdoc_no" runat="server" Text="..." Width="80px" OnClick="btntxtdoc_no_Click"
                                    CssClass="btnHelp" Height="24px" />
                                <asp:Label ID="lblJV_Id" runat="server" CssClass="lblName" Visible="false"></asp:Label>
                                <ajax1:FilteredTextBoxExtender ID="Filteretxtdoc_no" runat="server" TargetControlID="txtdoc_no"
                                    FilterType="Numbers">
                                </ajax1:FilteredTextBoxExtender>
                                Date: &nbsp;&nbsp;
                                <asp:TextBox ID="txtdoc_date" runat="Server" CssClass="txt" TabIndex="2" Width="90px"
                                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                    Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtdoc_date_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                    Width="25px" Height="15px" />
                                <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtdoc_date"
                                    PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                            </td>
                            <tr>
                                <td align="left" colspan="4">
                                    <%--<asp:Button ID="btnOpenDetailsPopup" runat="server" Text="ADD" CssClass="btnHelp"
                                        Width="80px" Height="24px" OnClick="btnOpenDetailsPopup_Click" TabIndex="28" />--%>
                                </td>
                            </tr>
                           
                        </tr>
                </table>
                <fieldset style="border-top: 3px dotted rgb(131, 127, 130); border-radius: 3px; width: 172%; margin-left: -1160px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 5px;">
                    <legend style="text-align: left;">
                        <h2 style="color: Black; text-align: left;" font-names="verdana" font-size="large">Detail Section</h2>
                    </legend>
                </fieldset>
                <asp:Panel ID="Panel2" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                    Font-Size="Small" Style="margin-top: 0px; z-index: 100;">
                </asp:Panel>
                <table width="100%" align="center" cellspacing="5">

                    <tr>
                        <td align="right">
                            <asp:Label ID="lblNo" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblID" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">A/C:
                       
                            <asp:TextBox ID="txtdebit_ac" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtdebit_ac_TextChanged" onKeyDown="Ac_Code(event);"></asp:TextBox>
                            <asp:Button ID="btntxtdebit_ac" runat="server" Text="..." OnClick="btntxtdebit_ac_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblDebit_name" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblclosingbal" runat="server" CssClass="lblName"></asp:Label>
                            DRCR:
                        
                            <asp:DropDownList ID="drpDrCr" runat="server" OnSelectedIndexChanged="drpDrCr_SelectedIndexChanged"
                                TabIndex="4" Width="80px">
                                <asp:ListItem Text="Debit" Value="D"></asp:ListItem>
                                <asp:ListItem Text="Credit" Value="C"></asp:ListItem>
                            </asp:DropDownList>
                            Amount:
                       
                            <asp:TextBox ID="txtamount" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtamount_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="filterAmount" runat="server" TargetControlID="txtamount"
                                FilterType="Custom,Numbers" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Narration:
                       
                            <asp:TextBox ID="txtnarration" runat="Server" CssClass="txt" Width="480px" Height="50px"
                                TextMode="MultiLine" Style="text-align: left;" AutoPostBack="false" TabIndex="6"
                                OnTextChanged="txtnarration_TextChanged" onKeyDown="Narration(event);"></asp:TextBox>
                            <asp:Button ID="btntxtnarration" runat="server" Text="..." OnClick="btntxtnarration_Click"
                                CssClass="btnHelp" Width="20px" Height="24px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                Height="25px" OnClick="btnAdddetails_Click" TabIndex="7" />
                            <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp"
                                Width="80px" Height="25px" OnClick="btnClosedetails_Click" TabIndex="8" />
                        </td>
                    </tr>
                    <tr>
                         <td align="left" colspan="2" style="font-size: large;">Debit Total:
                                    <asp:Label ID="lbldebittotal" runat="server" CssClass="lblName" Font-Size="Large"
                                        ForeColor="Black"></asp:Label>
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; Credit Total:
                                    <asp:Label ID="lblcredittotal" runat="server" CssClass="lblName" Font-Size="Large"
                                        ForeColor="Black"></asp:Label>
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; Difference:
                                    <asp:Label ID="lbldiff" runat="server" CssClass="lblName" Font-Size="Large" ForeColor="Black"></asp:Label>
                                </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="width: 100%; position: relative;">
                <asp:UpdatePanel ID="upGrid" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlgrdDetail" runat="server" ScrollBars="Both" Height="300px" Width="1000px"
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
            <table width="80%" align="left">
            </table>

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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    EmptyDataText="No Records Found" AllowPaging="true" PageSize="20" HeaderStyle-BackColor="#6D8980"
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
