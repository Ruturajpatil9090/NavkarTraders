<%@ Page Title="Register" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeRegisters.aspx.cs" Inherits="Report_pgeRegisters" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <link href="../CSS/tooltip.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/tooltip.js">
    </script>
    <script type="text/javascript" src="../JS/select all.js"></script>
    <script type="text/javascript" language="javascript">

        function sp(fromDT, toDT, Branch_Code) {
            var tn;
            window.open('../Report/rptNewDispatchRegister.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code, '_blank');    //R=Redirected  O=Original
        }
        function DD(fromDT, toDT, Mill_Code, Lot_No, Sr_No, Branch_Code) {
            window.open('../Report/rptDispatchDetails.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Mill_Code=' + Mill_Code + '&Lot_No=' + Lot_No + '&Sr_No=' + Sr_No + '&Branch_Code=' + Branch_Code);
        }
        function DDN(fromDT, toDT, Mill_Code, Lot_No, Sr_No, Branch_Code) {
            window.open('../Report/rptDispatchDetailsNew.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Mill_Code=' + Mill_Code + '&Lot_No=' + Lot_No + '&Sr_No=' + Sr_No + '&Branch_Code=' + Branch_Code);
        }
        function DDNM(fromDT, toDT, Mill_Code, Lot_No, Sr_No, Branch_Code) {
            window.open('../Report/rptDispatchDetailsNewMill.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Mill_Code=' + Mill_Code + '&Lot_No=' + Lot_No + '&Sr_No=' + Sr_No + '&Branch_Code=' + Branch_Code);
        }
        function DDM(fromDT, toDT, Mill_Code, Lot_No, Sr_No, Branch_Code) {
            window.open('../Report/rptDispatchDetailsForMill.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Mill_Code=' + Mill_Code + '&Lot_No=' + Lot_No + '&Sr_No=' + Sr_No + '&Branch_Code=' + Branch_Code);
        }
        function fr(fromDT, toDT, Branch_Code) {
            var tn;
            window.open('../Report/rptFrieghtRegister.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code, '_blank');    //R=Redirected  O=Original
        }
        function vr(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptVasuliRegister.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }
        function vr2(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptVasuliRegisterNonZero.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }
        function rsdp(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptResaleDiff.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }
        function rsdpWithPay(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptWithPaymentDifference.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }
        function difftopay(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptResaleDiffToPay.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }
        function difftorecieve(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptResaleDiffToRecieve.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }
        function DispDiff(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptDispDiffPartywise.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }
        function DispSummarynew(fromDT, toDT) {
            window.open('../Report/rptDispSummarynew.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }
        function dispdifftopay(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptDispDiffToPay.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }
        function dispdifftorecieve(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptDispDiffToRecieve.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }
        function difftopayWithPay(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptWithPaymentDiff.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }
        function difftorecieveWithPay(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptWithpaymentRecieve.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }
        function DispSummary(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptDispSummary.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }
        function DispSummarySmall(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptDispSummarySmall.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }

        function dispmillwise(fromDT, toDT, Mill_Code, Branch_Code) {
            window.open('../Report/rptDispMillWise.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Mill_Code=' + Mill_Code + '&Branch_Code=' + Branch_Code);
        }

        function dispgradewise(fromDT, toDT, Mill_Code, Branch_Code) {
            window.open('../Report/rptDispGradeWise.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Mill_Code=' + Mill_Code + '&Branch_Code=' + Branch_Code);
        }
        function MWDR(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptMillWiseDispatch.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }

        function TWDR(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptTransportWiseDispatch.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }

        function DOWDR(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptDoWiseDispatch.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }

        function PWDO(fromDT, toDT, Branch_Code, ac_code) {
            window.open('../Report/rptPartyWiseDO.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code + '&ac_code=' + ac_code);
        }

        function PWDOM(fromDT, toDT, Branch_Code, ac_code) {
            window.open('../Report/rptPartyWiseDOWithMill.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code + '&ac_code=' + ac_code);
        }

        function TBR(fromDT, toDT, Transport, Branch_Code) {
            window.open('../Report/rptTransportAc.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Transport=' + Transport + '&Branch_Code=' + Branch_Code);
        }

        function DOV(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptVasuliRegisterNew.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }

        function BSS(fromDT, toDT, Branch_Code, Mill_Code) {
            window.open('../Report/rptBalanceStockSummary.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code + '&Mill_Code=' + Mill_Code);
        }

        function NPW(fromDT, toDT) {
            window.open('../Report/rptNewPartyWisenew1.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }

        function NPW2(fromDT, toDT) {
            window.open('../Report/rptNewPartywise2.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }

        function MP(fromDT, toDT) {
            window.open('../Report/rptMillPayment.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }

        
        function MPForGST(fromDT, toDT, Mill_Code) {
            window.open('../Report/rptMillPaymentForGST.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Mill_Code=' + Mill_Code);
        }

        function onlydo(fromDT, toDT) {
            window.open('../Report/rptOnlyDoDetail.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }

        function DispSummaryForGST(fromDT, toDT, Branch_Code) {
            window.open('../Report/rptDispatchSummaryForGST.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Branch_Code=' + Branch_Code);
        }

        function dispurchesmillwise(fromDT, toDT, Mill_Code, Branch_Code) {
            window.open('../Report/rptmillwisepurchesdispnew.aspx?fromDT=' + fromDT + '&toDT=' + toDT + '&Mill_Code=' + Mill_Code + '&Branch_Code=' + Branch_Code);
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
                if (hdnfClosePopupValue == "MM") {
                    document.getElementById("<%=txtMillCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblMillName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    
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
            if (confirm("Detail Report=>Ok / PartyWise=>Cancel ")) {
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
            function GroupCode(e) {
            if (e.keyCode == 112) { 
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
    </script>
      <%--  function GroupCode(e) {

            if (e.keyCode == 112) {

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

        }--%> 
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
                 if (hdnfClosePopupValue == "txtMillCode") {
                     document.getElementById("<%=txtMillCode.ClientID %>").focus();
                 }
                 
                 document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                 document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
             }

         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Register   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table align="left" cellpadding="3" cellspacing="4" style="margin-left: 60px;" width="60%">
                    <tr>
                        <td>
                            Branch:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpBranch" runat="server" CssClass="ddl" Width="200px" AutoPostBack="true"
                                Height="24px" OnSelectedIndexChanged="drpBranch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            From Date:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                            <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" />
                            <ajax1:CalendarExtender ID="calendertxtFromDate" runat="server" TargetControlID="txtFromDate"
                                PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                            To Date:
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                Height="15px" />
                            <ajax1:CalendarExtender ID="CalendarExtendertxtToDate" runat="server" TargetControlID="txtToDate"
                                PopupButtonID="Image1" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Mill Name:
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtMillCode" Width="80px" Height="24px" CssClass="txt"
                                AutoPostBack="false" OnTextChanged="txtMillCode_TextChanged" onkeydown="GroupCode(event);"></asp:TextBox>
                            <asp:Button ID="btnMillCode" runat="server" Text="..." CssClass="btnHelp" OnClick="btnMillCode_Click"
                                Height="24px" Width="20px" />
                            <asp:Label ID="lblMillName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Lot No:
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtLotNo" Width="80px" Height="24px" CssClass="txt"
                                AutoPostBack="true" OnTextChanged="txtLotNo_TextChanged"></asp:TextBox>
                            <asp:Label ID="lbllotno" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sr. No:
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtSrNo" Width="80px" Height="24px" CssClass="txt"
                                AutoPostBack="true" OnTextChanged="txtSrNo_TextChanged"></asp:TextBox>
                            <asp:Label ID="lblBuyer" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label runat="server" ID="lblSrNotExist" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table width="100%" style="margin-left: 60px;" align="left" cellpadding="5" cellspacing="6">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnResaleDiff" runat="server" Text="Resale Diff" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClick="btnResaleDiff_Click" OnClientClick="Confirm()" />&nbsp;&nbsp;
                            <asp:Button ID="btnWithPayment" runat="server" Text="With Payment Diff" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClientClick="Confirm()" OnClick="btnWithPayment_Click" />
                            &nbsp;
                            <asp:Button ID="btnDispatchDiff" runat="server" Text="Dispatch Diff" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClick="btnDispatchDiff_Click" OnClientClick="Confirm()" />
                            &nbsp;
                            <asp:Button ID="btnDispRegister" runat="server" Text="Desp Register" CssClass="btnHelp"
                                Width="160px" OnClick="btnDispRegister_Click" Height="24px" />
                            &nbsp;
                            <asp:Button ID="btnDispDetails" runat="server" Text="Dispatch Details" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClick="btnDispDetails_Click" />
                            &nbsp;&nbsp;<asp:Button runat="server" ID="btnDispDetailMill" CssClass="btnHelp"
                                Width="160px" Height="24px" Text="Disp Detail For Mill" OnClick="btnDispDetailMill_Click" />
                            &nbsp;
                            <asp:Button ID="btnDispSummary" runat="server" Text="Disp Summary" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClick="btnDispSummary_Click" />
                           
                            &nbsp;
                            <asp:Button ID="btnDispSummaryForGST" runat="server" Text="Disp Summary For GST"
                                CssClass="btnHelp" Width="160px" Height="24px" OnClick="btnDispSummaryForGST_Click" />
                           
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnDispMillWise" runat="server" Text="Desp Mill Wise" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClick="btnDispMillWise_Click" />
                            &nbsp;
                             <asp:Button ID="btnDispGradeWise" runat="server" Text="Desp Grade Wise" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClick="btnDispGradeWise_Click" />
                            &nbsp;
                            <asp:Button ID="btnPartyWiseDO" runat="server" Text="Party Wise DO" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClick="btnPartyWiseDO_Click" />
                            &nbsp;
                            <asp:Button ID="btnFrieghtReg" runat="server" Text="Freight Register" OnClick="btnFrieghtReg_Click"
                                CssClass="btnHelp" Width="160px" Height="24px" />
                            &nbsp;
                            <asp:Button ID="btnTransportBal" runat="server" Text="TransPort A/c" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClick="btnTransportBal_Click" />
                            &nbsp;
                            <asp:Button ID="Button10" runat="server" Text="Utr Register" CssClass="btnHelp" Width="160px"
                                Height="24px" />
                            &nbsp;&nbsp;<asp:Button ID="btnDOVasli" runat="server" Text="DO Vasuli" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClick="btnDOVasli_Click" />
                            &nbsp;
                            <asp:Button ID="btnCatWiseDisp" runat="server" Text="Catagory wise Disp" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClick="btnCatWiseDisp_Click" />
                            &nbsp;
                            <asp:Button ID="btnBSS" runat="server" Text="Balance Stock Summary" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClick="btnBSS_Click" />
                           
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Button Text="Delivery SMS" runat="server" ID="btnDeliverySms" Width="160px"
                                Height="24px" CssClass="btnHelp" OnClick="btnDeliverySms_Click" />
                            &nbsp;
                            <asp:Button Text="New Partywise 1" runat="server" ID="btnNewPartyWise" Width="160px"
                                Height="24px" CssClass="btnHelp" OnClick="btnNewPartyWise_Click" />
                            &nbsp;
                            <asp:Button Text="New Partywise 2" runat="server" ID="btnNewPartywise2" Width="160px"
                                Height="24px" CssClass="btnHelp" OnClick="btnNewPartywise2_Click" />
                            &nbsp;
                            <asp:Button Text="Mill Payment" runat="server" ID="btnMillPayment" Width="160px"
                                Height="24px" CssClass="btnHelp" OnClick="btnMillPayment_Click" />
                            &nbsp;
                            <asp:Button Text="Mill Payment For GST" runat="server" ID="btnMillPaymentForGST"
                                Width="160px" Height="24px" CssClass="btnHelp" OnClick="btnMillPaymentForGST_Click" />
                            &nbsp;
                            <asp:Button Text="Only DO" runat="server" ID="btndo" Width="160px"
                                Height="24px" CssClass="btnHelp" OnClick="btndo_click" />
                             &nbsp;
                            <asp:Button ID="btndispsummarynew" runat="server" Text="Disp Summary new" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClick="btndispsummarynew_Click" />
                              &nbsp;
                            <asp:Button ID="btnVasuliRegister" runat="server" Text="Vasuli Register" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClick="btnVasuliRegister_Click" />
                                 &nbsp;
                             <asp:Button ID="btnpurdescpmillwise" runat="server" Text="Purchase Millwise Desp" CssClass="btnHelp"
                                Width="160px" Height="24px" OnClick="btnpurdescpmillwise_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <br />
            </asp:Panel>
            <table align="left" style="margin-left: 60px;">
                <tr>
                    <td align="left">
                        <asp:Panel ID="pnlGrid" runat="server" align="left" BorderColor="Blue" BorderWidth="1px"
                            DefaultButton="btnenterkey" Height="300px" ScrollBars="Both" Style="text-align: left"
                            Width="1200px" BackColor="White">
                            <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="false" CellPadding="6"
                                Font-Bold="true" ForeColor="Black" GridLines="Both" HeaderStyle-BackColor="#397CBB"
                                HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" OnRowDataBound="grdDetail_RowDataBound"
                                RowStyle-Height="30px" Style="table-layout: fixed;" Width="100%" OnRowCommand="grdDetail_RowCommand"
                                BackColor="White">
                                <Columns>
                                    <asp:BoundField HeaderText="Memo No" DataField="Memo_No" />
                                    <asp:BoundField HeaderText="Party" DataField="Party" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="Name Of Account" DataField="Name Of Account" />
                                    <asp:TemplateField HeaderText="Driver Mobile">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="TextBox1" BorderStyle="None" Text='<%#Eval("DriverMobile") %>'
                                                Height="20px" Width="90%" MaxLength="10"></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Party Mobile">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="TextBox2" BorderStyle="None" Text='<%#Eval("PartMobile") %>'
                                                Height="20px" Width="90%" MaxLength="10"></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" Text="SMS" OnClick="selectAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="grdCB" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="grdDeliverySms" runat="server" AutoGenerateColumns="false" CellPadding="6"
                                Font-Bold="true" ForeColor="Black" GridLines="Both" HeaderStyle-BackColor="#397CBB"
                                HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" OnRowDataBound="grdDeliverySms_RowDataBound"
                                RowStyle-Height="30px" Style="table-layout: fixed;" Width="100%" BackColor="White">
                                <Columns>
                                    <asp:BoundField HeaderText="DO.No" DataField="dono" />
                                    <asp:BoundField HeaderText="Party" DataField="Party" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="Message" DataField="msg" />
                                    <asp:TemplateField HeaderText="Driver Mobile">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtDriverMob" BorderStyle="None" Text='<%#Eval("driver_no") %>'
                                                Height="20px" Width="90%" MaxLength="10"></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Frieght">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtFrieght" BorderStyle="None" Text='<%#Eval("frieght") %>'
                                                Height="20px" Width="90%" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Party Mobile">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="TextBox2" BorderStyle="None" Text='<%#Eval("PartyMobile") %>'
                                                Height="20px" Width="90%" MaxLength="10"></asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" Text="SMS" OnClick="selectAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="grdCB" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Mill" DataField="mill" />
                                </Columns>
                            </asp:GridView>
                            <asp:Button runat="server" ID="btnenterkey" Style="display: none" OnClick="btnenterkey_Click" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Button ID="btnSendSms" runat="server" CssClass="btnHelp" Height="25px" Text="Send SMS"
                            Width="100px" OnClick="btnSendSms_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
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
                    <%--<asp:Panel ID="pnlScroll" runat="server" Width="680px" ScrollBars="Both" Direction="LeftToRight" BackColor="#FFFFE4" style="z-index:5000 ;  float:right; max-height:380px; height:380px;">--%>
                    <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                        Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                        <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                            EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980" HeaderStyle-ForeColor="White"
                            PageSize="20" AllowPaging="true" OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound"
                            Style="table-layout: fixed;" OnPageIndexChanging="grdPopup_PageIndexChanging">
                            <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                            <RowStyle Height="25px" ForeColor="Black" Wrap="false" />
                            <PagerStyle BackColor="Tomato" ForeColor="White" Width="100%" Font-Bold="true" />
                            <PagerSettings Position="TopAndBottom" />
                        </asp:GridView>
                    </asp:Panel>
                    <%--</asp:Panel>--%>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax1:ModalPopupExtender ID="modelPopup1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlEdit" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
    </ajax1:ModalPopupExtender>
    <asp:Panel ID="pnlEdit" runat="server" BackColor="White" Height="269px" Width="400px"
        Style="display: none">
        <table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%"
            cellpadding="0" cellspacing="0">
            <tr style="background-color: #D55500">
                <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                    align="center">
                    Mobile Number:
                </td>
            </tr>
            <tr>
                <td align="right">
                    Driver Mobile:
                </td>
                <td>
                    <asp:TextBox ID="txtDriverMobile" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    Party Mobile:
                </td>
                <td>
                    <asp:TextBox ID="txtPartyMobile" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnOK" CommandName="OK" runat="server" Text="OK" OnClick="btnOK_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
