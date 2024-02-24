<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PgeEquityPurchaseSale.aspx.cs" Inherits="PgeEquityPurchaseSale" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Script/jquery-1.4.2.js"></script>

    <link href="../../CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../../JQuery/jquery-1.4.1.js" type="text/javascript"></script>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../../JQuery/jquery.keynavigation.js" type="text/javascript"></script>
    <link type="text/css" href="../../menu/menu.css" rel="stylesheet" />
    <script type="text/javascript" src="../../menu/jquery.js"></script>
    <script type="text/javascript" src="../../menu/menu.js"></script>


    <script src="../../JS/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../Scripts/selectfirstrow.js" type="text/javascript"></script>
    <%--  <script type="text/javascript">
        window.onbeforeunload = function (event) {
            var message = 'All changes will get lost!';
            if (typeof event == 'undefined') {
                event = window.event;
            }
            if (event) {
                event.returnValue = message;
            }
            return message;
        }
    </script>--%>
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
                if (hdnfClosePopupValue == "txtScript_Code") {
                    document.getElementById("<%=txtScript_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurc_No") {
                    document.getElementById("<%=txtPurc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSale_To") {
                    document.getElementById("<%=txtSale_To.ClientID %>").focus();
                }
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
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
                    document.getElementById("<%= lbldoc_no.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtScript_Code") {
                    document.getElementById("<%= txtScript_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblScriptCode.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtScript_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurc_No") {
                    document.getElementById("<%= txtPurc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblPurc_Year_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[5].innerText;
                    document.getElementById("<%= lblPurc_Company_code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[6].innerText;
                    document.getElementById("<%= txtQty.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%= txtBonus.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[6].innerText;
                    document.getElementById("<%=txtPurc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSale_To") {
                    document.getElementById("<%= txtSale_To.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblSaleTo.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtSale_To.ClientID %>").focus();
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
        function chanegno(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtEditDoc_No";
                ("#<%=hdnfClosePopup.ClientID %> ").val(edi);
                ("#<%=btnSearch.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                _doPostBack("txtEditDoc_No", "TextChanged");
            }
        }
        function Doc_No(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                ("#<%=btntxtDoc_No.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                _doPostBack("txtDoc_No", "TextChanged");
            }
        }

        function Script_Code(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtScript_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtScript_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtScript_Code.ClientID %>").val(unit);
                __doPostBack("txtScript_Code", "TextChanged");

            }

        }

        function Purc_No(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtPurc_No.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtPurc_No.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtPurc_No.ClientID %>").val(unit);
                __doPostBack("txtPurc_No", "TextChanged");

            }

        }
        function Sale_To(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnSale_To.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtSale_To.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtSale_To.ClientID %>").val(unit);
                __doPostBack("txtSale_To", "TextChanged");

            }

        }
    </script>
    <script type="text/javascript">
        function Back() {
            var Action = 2;
            var Group_Code = 0;
            //alert(td);
            window.open('../Master/PgeEquityPurchaseSaleUtility.aspx');
        }
    </script>
    <%--<script type="text/javascript">
        function Validate() {
            debugger;
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
            return true;
        }

        function validation() {
            debugger;
            $("#loader").show();
            var PSDoc = 0, SaleId = 0;
            var insertrecord = "";
            var status = document.getElementById("<%= btnSave.ClientID %>").value;
            var spname = "SP_EquityPurchaseSale";
            var XML = "<ROOT>";
            
            var Doc_No = $("#<%=txtDoc_No.ClientID %>").val() == "" ? 0 : $("#<%=txtDoc_No.ClientID %>").val();
            if (status == "Update") {
                PSDoc = document.getElementById("<%= hdnf.ClientID %>").value;
                SaleId = document.getElementById("<%= hdnfSaleId.ClientID %>").value;
            }
            var d = $("#<%=txtDate.ClientID %>").val();
            var doc_date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
            var drpTranType = $("#<%=drpTran_Type.ClientID %>").val();                      
            var drpLedgerEntry = $("#<%=drpLedgerEntry.ClientID %>").val(); 
            var ScriptCode = $("#<%=txtScript_Code.ClientID %>").val() == "" ? 0 : $("#<%=txtScript_Code.ClientID %>").val();
            var PurcNo = $("#<%=txtPurc_No.ClientID %>").val() == "" ? 0 : $("#<%=txtPurc_No.ClientID %>").val();
            var PurcYearCode = $("#<%=lblPurc_Year_Code.ClientID %>").val() == "" ? 0 : $("#<%=lblPurc_Year_Code.ClientID %>").val();
            var PurcCompanycode = $("#<%=lblPurc_Company_code.ClientID %>").val() == "" ? 0 : $("#<%=lblPurc_Company_code.ClientID %>").val();
            var Qty = $("#<%=txtQty.ClientID %>").val() == "" ? 0 : $("#<%=txtQty.ClientID %>").val();  
            var Bonus = $("#<%=txtBonus.ClientID %>").val() == "" ? 0 : $("#<%=txtBonus.ClientID %>").val();  
            var Rate = $("#<%=txtRate.ClientID %>").val() == "" ? 0 : $("#<%=txtRate.ClientID %>").val();         
            var Value = parseFloat($("#<%=txtValue.ClientID %>").val() == "" ? 0 : $("#<%=txtValue.ClientID %>").val()); 
            var ProfitLoss = parseFloat($("#<%=txtProfit_Loss.ClientID %>").val() == "" ? 0 : $("#<%=txtProfit_Loss.ClientID %>").val()); 
            var SaleTo = $("#<%=txtSale_To.ClientID %>").val() == "" ? 0 : $("#<%=txtSale_To.ClientID %>").val();
            var Brokage = parseFloat($("#<%=txtBrokage.ClientID %>").val() == "" ? 0 : $("#<%=txtBrokage.ClientID %>").val()); 
            var Cess = parseFloat($("#<%=txtCess_Tax.ClientID %>").val() == "" ? 0 : $("#<%=txtCess_Tax.ClientID %>").val());
            var STT = parseFloat($("#<%=txtSTT.ClientID %>").val() == "" ? 0 : $("#<%=txtSTT.ClientID %>").val());
            var Service = parseFloat($("#<%=txtService_Tax.ClientID %>").val() == "" ? 0 : $("#<%=txtService_Tax.ClientID %>").val());
            var StampCharge = parseFloat($("#<%=txtStamp_Charge.ClientID %>").val() == "" ? 0 : $("#<%=txtStamp_Charge.ClientID %>").val());
            var TurnOver = parseFloat($("#<%=txtTurnOver_Tax.ClientID %>").val() == "" ? 0 : $("#<%=txtTurnOver_Tax.ClientID %>").val());
            var TaxTotal = Brokage + Cess + STT + Service + StampCharge + TurnOver;

            var NetValue = parseFloat($("#<%=txtNet_Value.ClientID %>").val() == "" ? 0 : $("#<%=txtNet_Value.ClientID %>").val()); 
            var NetRate = $("#<%=txtNet_Rate.ClientID %>").val() == "" ? 0 : $("#<%=txtNet_Rate.ClientID %>").val();
            var narration = $("#<%=txtNarration.ClientID %>").val();
            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';
            var USER = '<%= Session["user"] %>';
            var EquitySale_ac = '<%= Session["EquitySale_ac"] %>';
            var EquityPurchase_ac = '<%= Session["EquityPurchase_ac"] %>';
            var EquitySale_acid = '<%= Session["EquitySale_acid"] %>';
            var EquityPurchase_acid = '<%= Session["EquityPurchase_acid"] %>';
            var EquityExpenses_ac = '<%= Session["EquityExpenses_ac"] %>';
            var EquityExpenses_acid = '<%= Session["EquityExpenses_acid"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';     
           
            var drcr = "", drcrac = "" , drcracid = "";
            if (drpTranType == "P")
            {
                drcr = "C";
               
                drcrac = EquityPurchase_ac;
                drcracid = EquityPurchase_acid; 
            }
            else
            {
                drcr = "D";
                
                drcrac = EquitySale_ac;
                drcracid = EquitySale_acid;
            }
            var SaleId = document.getElementById("<%= hdnfSaleId.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfSaleId.ClientID %>").value;

            // detail varibles
            var DOCNOC = "";
            if (status == "Save") {
                insertrecord = "Created_By='" + USER + "' Modified_By=''";
                DOCNOC = "Date='" + doc_date + "'";
            }
            else {
                insertrecord = "Modified_By='" + USER + "' Created_By=''";
                DOCNOC = "Doc_No='" + Doc_No + "' Date='" + doc_date + "'";
            } 

            XML = XML + "<EquityHead " + DOCNOC + " Tran_Type='" + drpTranType + "' LedgerEntry='" + drpLedgerEntry + "' Script_Code='" + ScriptCode + "' Purc_No='" + PurcNo + "' Purc_Year_Code='" + PurcYearCode + "' " +
                "Purc_Company_code='" + PurcCompanycode + "' Qty='" + Qty + "' Bonus='" + Bonus + "' Rate='" + Rate + "' " +
                  "Value='" + Value + "' Profit_Loss='" + ProfitLoss + "' Sale_To='" + SaleTo + "' Brokage='" + Brokage + "' " +
                   "Cess_Tax='" + Cess + "' STT='" + STT + "' Service_Tax='" + Service + "' Stamp_Charge='" + StampCharge + "' " +
                   "TurnOver_Tax='" + TurnOver + "' Net_Value='" + NetValue + "' Net_Rate='" + NetRate + "' Narration='" + narration + "' " +
                "" + insertrecord + " Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "'>";


            var Order_Code = 0;
            if (NetValue > 0)
            {
                Order_Code = Order_Code + 1;
                XML = XML + "<Ledger TRAN_TYPE='EQ' CASHCREDIT='" + drpTranType + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + doc_date + "' AC_CODE='" + SaleTo + "' " +
                                                           "UNIT_code='1' NARRATION='" + narration + "' AMOUNT='" + NetValue + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr + "' DRCR_HEAD='99999' ADJUSTED_AMOUNT='0' Branch_Code='0' " +
                                                           "SORT_TYPE='EQ' SORT_NO='0' ac='" + SaleId + "' vc='0' progid='77' tranid='0'/>";
            }

            if (drpTranType == "P") {
                drcr = "D";
            }
            else
            {
                drcr = "C";
            }

            if (Value > 0)
            {
                Order_Code = Order_Code + 1;
                XML = XML + "<Ledger TRAN_TYPE='EQ' CASHCREDIT='" + drpTranType + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Date + "' AC_CODE='" + drcrac + "' " +
                                                           "UNIT_code='1' NARRATION='" + narration + "' AMOUNT='" + Value + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr + "' DRCR_HEAD='99999' ADJUSTED_AMOUNT='0' Branch_Code='0' " +
                                                           "SORT_TYPE='EQ' SORT_NO='0' ac='" + drcracid + "' vc='0' progid='77' tranid='0'/>";
            }

            if (TaxTotal > 0) {
                Order_Code = Order_Code + 1;
                XML = XML + "<Ledger TRAN_TYPE='EQ' CASHCREDIT='" + drpTranType + "' DOC_NO='" + Doc_No + "' DOC_DATE='" + Date + "' AC_CODE='" + EquityExpenses_ac + "' " +
                                                           "UNIT_code='1' NARRATION='" + narration + "' AMOUNT='" + TaxTotal + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='" + drcr + "' DRCR_HEAD='99999' ADJUSTED_AMOUNT='0' Branch_Code='0' " +
                                                           "SORT_TYPE='EQ' SORT_NO='0' ac='" + EquityExpenses_acid + "' vc='0' progid='77' tranid='0'/>";
            }
            XML = XML + "</EquityHead></ROOT>";

            $.ajax({
               
                type: "POST",
                url: "../xmlExecuteDMLQry.asmx/ExecuteXMLQry",
                data: '{XML:"' + XML + '",status:"' + status + '",spname:"' + spname + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    debugger;
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
                    window.open('../Share/PgeEquityPurchaseSale.aspx?PSDoc=' + id + '&Action=1', "_self");

                }
            }

        }

    </script>--%>

     <%-- <style>
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
            <asp:Label ID="label1" runat="server" Text="Enter page name " Font-Names="verdana" ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfSaleId" runat="server" />
            <%--<asp:HiddenField ID="R_CompanyCode" runat="server" />
            <asp:HiddenField ID="R_YearCode" runat="server" />--%>
            <table width="100%" align="center">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px" TabIndex="25"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" TabIndex="23" />
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
                             TabIndex="14" ValidationGroup="save" Height="24px" OnClientClick="Back()" />
                    </td>
                    <%-- <td align="center">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp" OnClick="btnFirst_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp" OnClick="btnPrevious_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp" OnClick="btnNext_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp" OnClick="btnLast_Click" Width="90px" Height="24px" />
                    </td>--%>
                </tr>
            </table>

            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black" Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">

                <table style="width: 50%;" align="center" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana" Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                        <tr>
                            <td align="left">Change No&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                          
                                <asp:TextBox Height="24px" ID="txtEditDoc_No" runat="Server" CssClass="txt" TabIndex="1" Width="90px" Style="text-align: left;"
                                    AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Entry No &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                             
                                <asp:TextBox Height="24px" ID="txtDoc_No" runat="Server" CssClass="txt" TabIndex="2" Width="90px" Style="text-align: left;"
                                    OnkeyDown="Doc_No(event);"
                                    AutoPostBack="false" OnTextChanged="txtDoc_No_TextChanged"></asp:TextBox>
                                <asp:Button Width="70px" Height="24px" ID="btntxtDoc_No" runat="server" Text="..."
                                    OnClick="btntxtDoc_No_Click" CssClass="btnHelp" />
                                <asp:Label ID="lbldoc_no" runat="server" CssClass="lblName"></asp:Label>
                                LedgerEntry                          
                                <asp:DropDownList ID="drpLedgerEntry" runat="Server" CssClass="txt" TabIndex="3" Width="90px" AutoPostBack="false" OnSelectedIndexChanged="drpLedgerEntry_SelectedIndexChanged">
                                    <asp:ListItem Text="Yes" Value="Y" />
                                    <asp:ListItem Text="No" Value="N" />
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left">Tran Type&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                          
                                <asp:DropDownList ID="drpTran_Type" runat="Server" CssClass="txt" TabIndex="4" Width="90px" AutoPostBack="true" OnSelectedIndexChanged="drpTran_Type_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="Purchase" Value="P"></asp:ListItem>
                                    <%--<asp:ListItem Text="Purchase" Value="P" />--%>
                                    <asp:ListItem Text="Sale" Value="S" />
                                </asp:DropDownList>
                                Date                          
                                <asp:TextBox Height="24px" ID="txtDate" runat="Server" CssClass="txt" TabIndex="5" Width="90px" Style="text-align: left;"
                                    AutoPostBack="false" OnTextChanged="txtDate_TextChanged" onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                                <asp:Image ID="imgcalendertxtDate" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtDate" runat="server" TargetControlID="txtDate" PopupButtonID="imgcalendertxtDate" Format="dd/MM/yyyy"></ajax1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Script Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                       
                                <asp:TextBox Height="24px" ID="txtScript_Code" runat="Server" CssClass="txt" TabIndex="6" Width="90px" Style="text-align: left;"
                                    OnkeyDown="Script_Code(event);"
                                    AutoPostBack="false" OnTextChanged="txtScript_Code_TextChanged"></asp:TextBox>
                                <asp:Button Width="20px" Height="24px" ID="btntxtScript_Code" runat="server" Text="..."
                                    OnClick="btntxtScript_Code_Click" CssClass="btnHelp" />
                                <asp:Label ID="lblScriptCode" runat="server" CssClass="lblName"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left">Purc No&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                          
                                <asp:TextBox Height="24px" ID="txtPurc_No" runat="Server" CssClass="txt" TabIndex="7" Width="90px" Style="text-align: left;"
                                    OnkeyDown="Purc_No(event);"
                                    AutoPostBack="false" OnTextChanged="txtPurc_No_TextChanged"></asp:TextBox>
                                <asp:Button Width="20px" Height="24px" ID="btntxtPurc_No" runat="server" Text="..."
                                    OnClick="btntxtPurc_No_Click" CssClass="btnHelp" />
                                <asp:Label ID="lblPurc_Year_Code" runat="Server" Text="    "></asp:Label>
                                <asp:Label ID="lblPurc_Company_code" runat="Server" Text="    "></asp:Label>
                        </tr>
                        <tr>
                            <td align="left">Qty&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                          
                                <asp:TextBox Height="24px" ID="txtQty" runat="Server" CssClass="txt" TabIndex="8" Width="90px" Style="text-align: left;"
                                    AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                Bonus                          
                                <asp:TextBox Height="24px" ID="txtBonus" runat="Server" CssClass="txt" TabIndex="9" Width="90px" Style="text-align: left;"
                                    AutoPostBack="true" OnTextChanged="txtBonus_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Rate&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox Height="24px" ID="txtRate" runat="Server" CssClass="txt" TabIndex="10" Width="90px" Style="text-align: left;"
                                    AutoPostBack="true" OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                Value                         
                                <asp:TextBox Height="24px" ID="txtValue" runat="Server" CssClass="txt" TabIndex="11" Width="90px" Style="text-align: left;"
                                    AutoPostBack="false" OnTextChanged="txtValue_TextChanged"></asp:TextBox>
                                Profit/Loss                           
                                <asp:TextBox Height="24px" ID="txtProfit_Loss" runat="Server" CssClass="txt" TabIndex="12" Width="90px" Style="text-align: left;"
                                    AutoPostBack="false" OnTextChanged="txtProfit_Loss_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Sale To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                            
                                <asp:TextBox Height="24px" ID="txtSale_To" runat="Server" CssClass="txt" TabIndex="13" Width="90px" Style="text-align: left;"
                                    OnkeyDown="Sale_To(event);"
                                    AutoPostBack="false" OnTextChanged="txtSale_To_TextChanged"></asp:TextBox>
                                <asp:Button Width="20px" Height="24px" ID="btnSale_To" runat="server" Text="..."
                                    OnClick="btnSale_To_Click" CssClass="btnHelp" />
                                <asp:Label ID="lblSaleTo" runat="Server" Text="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Brokage&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox Height="24px" ID="txtBrokage" runat="Server" CssClass="txt" TabIndex="14" Width="90px" Style="text-align: left;"
                                    AutoPostBack="true" OnTextChanged="txtBrokage_TextChanged"></asp:TextBox>
                                Cess Tax                          
                                <asp:TextBox Height="24px" ID="txtCess_Tax" runat="Server" CssClass="txt" TabIndex="15" Width="90px" Style="text-align: left;"
                                    AutoPostBack="true" OnTextChanged="txtCess_Tax_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">STT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                          
                                <asp:TextBox Height="24px" ID="txtSTT" runat="Server" CssClass="txt" TabIndex="16" Width="90px" Style="text-align: left;"
                                    AutoPostBack="true" OnTextChanged="txtSTT_TextChanged"></asp:TextBox>
                                Service Tax                          
                                <asp:TextBox Height="24px" ID="txtService_Tax" runat="Server" CssClass="txt" TabIndex="17" Width="90px" Style="text-align: left;"
                                    AutoPostBack="true" OnTextChanged="txtService_Tax_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Stamp Charge                        
                                <asp:TextBox Height="24px" ID="txtStamp_Charge" runat="Server" CssClass="txt" TabIndex="18" Width="90px" Style="text-align: left;"
                                    AutoPostBack="true" OnTextChanged="txtStamp_Charge_TextChanged"></asp:TextBox>
                                TurnOver Tax                           
                                <asp:TextBox Height="24px" ID="txtTurnOver_Tax" runat="Server" CssClass="txt" TabIndex="19" Width="90px" Style="text-align: left;"
                                    AutoPostBack="true" OnTextChanged="txtTurnOver_Tax_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Net Value&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                          
                                <asp:TextBox Height="24px" ID="txtNet_Value" runat="Server" CssClass="txt" TabIndex="20" Width="90px" Style="text-align: left;"
                                    AutoPostBack="true" OnTextChanged="txtNet_Value_TextChanged"></asp:TextBox>
                                Net Rate                          
                                <asp:TextBox Height="24px" ID="txtNet_Rate" runat="Server" CssClass="txt" TabIndex="21" Width="90px" Style="text-align: left;"
                                    AutoPostBack="true" OnTextChanged="txtNet_Rate_TextChanged" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Narration&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                         
                                <asp:TextBox Height="24px" ID="txtNarration" runat="Server" CssClass="txt" TabIndex="22" Width="550px" Style="text-align: left;"
                                    AutoPostBack="false" OnTextChanged="txtNarration_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td align="left">Balance&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                         
                               <asp:Label ID="lblbalannce" runat="Server" Text=""></asp:Label>
                            </td>
                        </tr>
                </table>
                <%-- <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                    <legend style="text-align: center;">
                        <h5 style="color: White;" font-names="verdana" font-size="Medium">Detail Entry</h5>
                    </legend>
                </fieldset>--%>
            </asp:Panel>

            <asp:Panel onkeydown="closepopup(event);" ID="pnlPopup" runat="server" Width="70%" align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight" Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg" Width="20px" Height="20px" Style="float: right; vertical-align: top;" OnClick="imgBtnClose_Click" ToolTip="Close" />
                <table width="95%">
                    <tr>
                        <td align="center" style="background-color: #F5B540; width: 100%;">
                            <asp:Label ID="lblPopupHead" runat="server" Font-Size="Medium" Font-Names="verdana" Font-Bold="true" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Search Text:
                            <asp:TextBox onkeydown="SelectFirstRow(event);" ID="txtSearchText" runat="server" Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button onkeydown="closepopup(event);" ID="btnSearch" runat="server" Text="Search" CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4" Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView Font-Bold="true" CssClass="select" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdPopup_PageIndexChanging" ID="grdPopup" runat="server" AutoGenerateColumns="true" Width="100%" EmptyDataText="No Records Found"
                                    HeaderStyle-BackColor="#6D8980" HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
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

