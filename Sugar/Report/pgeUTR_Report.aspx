<%@ Page Title="UTR Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeUTR_Report.aspx.cs" Inherits="Report_pgeUTR_Report" %>

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
    <script type="text/javascript" src="../JS/DateValidation.js"> </script>
    <script src="../../JS/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../Scripts/selectfirstrow.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <script type="text/javascript" language="javascript">
        function sp(accode, utr_no, AcType, FromDt, ToDt) {
            var tn;
            window.open('rptUTR.aspx?accode=' + accode + '&utr_no=' + utr_no + '&AcType=' + AcType + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }


        function ut(accode, utr_no, AcType, FromDt, ToDt) {
            var tn;
            window.open('rptUTRDetailReportNew.aspx?accode=' + accode + '&utr_no=' + utr_no + '&AcType=' + AcType + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function UTRNo(accode, utr_no, AcType, FromDt, ToDt) {
            var tn;
            window.open('rptUTRDetailLTNoWise.aspx?accode=' + accode + '&utr_no=' + utr_no + '&AcType=' + AcType + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }

        function MillSM(accode, utr_no, AcType, FromDt, ToDt) {
            var tn;
            window.open('rptUTRMillPaymentSummary.aspx?accode=' + accode + '&utr_no=' + utr_no + '&AcType=' + AcType + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function SaudaSM(accode, utr_no, AcType, FromDt, ToDt) {
            var tn;
            window.open('rptSaudaBalanceSummary.aspx?accode=' + accode + '&utr_no=' + utr_no + '&AcType=' + AcType + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function MillDE(accode, utr_no, AcType, FromDt, ToDt) {
            var tn;
            window.open('rptUTRMillPaymentDetail.aspx?accode=' + accode + '&utr_no=' + utr_no + '&AcType=' + AcType + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }

        function SaudaDE(accode, utr_no, AcType, FromDt, ToDt) {
            var tn;
            window.open('rptSaudaBalanceDetail.aspx?accode=' + accode + '&utr_no=' + utr_no + '&AcType=' + AcType + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }

        function ob(accode, utr_no, AcType, FromDt, ToDt) {
            var tn;
            window.open('rptUtrOnlyBalance.aspx?accode=' + accode + '&utr_no=' + utr_no + '&AcType=' + AcType + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }

        function Againtsb(accode, FromDt, ToDt) {
            var tn;
            window.open('rptSaleBillBalance.aspx?Ac_Code=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }

        function Againtsbdetail(accode, FromDt, ToDt) {
            var tn;
            window.open('rptSaleBillDetail.aspx?Ac_Code=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function paymentbalance(accode, FromDt, ToDt) {
            var tn;
            window.open('rptpaymentbalnce.aspx?Ac_Code=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }

        function paymentdetail(accode, FromDt, ToDt) {
            var tn;
            window.open('rptpaymentdetail.aspx?Ac_Code=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function MillCashDiff(accode, FromDt, ToDt) {
            var tn;
            window.open('rptMillCashDiff.aspx?Ac_Code=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function Partybrokercashdiff(accode, FromDt, ToDt) {
            var tn;
            window.open('rptPartybrokercashdiff.aspx?Ac_Code=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }

        function noUTR(FromDt, ToDt) {
            var tn;
            window.open('rptDOwithoutUTR.aspx?FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        //        function TenderBal(accode,FromDt, ToDt) {
        //            var tn;
        //            window.open('rptMillAmtBalance.aspx?accode=' + accode + 'FromDt=' + FromDt + '&ToDt=' + ToDt);
        //                }
        function TenderBal(FromDt, ToDt) {
            var tn;
            window.open('rptMillAmtBalance.aspx?FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function UTRdetail(FromDt, ToDt) {
            var tn;
            window.open('rptUTRDetailReport.aspx?FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function UTRSummary(accode, FromDt, ToDt) {
            var tn;
            window.open('rptUTRSummaryReport.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function UTRdetailbalance(accode, FromDt, ToDt) {
            var tn;
            window.open('rptUTRDetailBalanceReport.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function btnperidoctribalnce(accode, FromDt, ToDt) {
            var tn;
            window.open('rptperidoctribalnce.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function DoCashDiff(accode, FromDt, ToDt) {
            var tn;
            window.open('rptDoCashdiff.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function TenderidwiseSauda(accode, FromDt, ToDt, bss) {
            var tn;
            window.open('rptTenderidwisesauda.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt + ' &bss=' + bss);
        }
        function milldwiseSauda(accode, FromDt, ToDt, bss) {
            var tn;
            window.open('rptMillwisesauda.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt + ' &bss=' + bss);
        }
        function TenderdispatchReg(accode, FromDt, ToDt) {
            var tn;
            window.open('rptTenderidwisedispatch.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function trantype(type, FromDt, ToDt, accode) {
            var tn;
            window.open('rptAllTranTypedata.aspx?type=' + type + '&FromDt=' + FromDt + '&ToDt=' + ToDt + '&accode=' + accode);
        }
        function DoCancle(FromDt, ToDt) {
            var tn;
            window.open('rptDoCancle.aspx?FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function NormalSauda(accode, FromDt, ToDt) {
            var tn;
            window.open('rptNormalSauda.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function retailBillsummary(accode, FromDt, ToDt) {
            var tn;
            window.open('rptRetailBillSummary.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function retailbilldetail(accode, FromDt, ToDt) {
            var tn;
            window.open('rptRetailBillDetail.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function owndo(accode, FromDt, ToDt) {
            var tn;
            window.open('rptOwnDO.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function PendingPurchase(accode, FromDt, ToDt) {
            var tn;
            window.open('rptPendingPurchase.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function PendingSale(accode, FromDt, ToDt) {
            var tn;
            window.open('rptPendingSale.aspx?accode=' + accode + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
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

                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                if (hdnfClosePopupValue == "txtAcCode") {
                    document.getElementById("<%=txtAcCode.ClientID %>").focus();
                }

                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
    </script>
    <script type="text/javascript">
        var isShift = false;
        var seperator = "/";
        function DateFormat(txt, keyCode) {
            if (keyCode == 16)
                isShift = true;
            //Validate that its Numeric
            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
         keyCode <= 37 || keyCode <= 39 ||
         (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
                if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
                    txt.value += seperator;
                }
                return true;
            }
            else {
                return false;
            }
        }

        function ValidateDate(txt, keyCode) {
            if (keyCode == 16)
                isShift = false;
            var val = txt.value;

            document.getElementById(lblmesg);
            if (val.length == 10) {
                var splits = val.split("/");
                var dt = new Date(splits[1] + "/" + splits[0] + "/" + splits[2]);

                //Validation for Dates
                if (dt.getDate() == splits[0] && dt.getMonth() + 1 == splits[1]
            && dt.getFullYear() == splits[2]) {
                    lblmesg.style.color = "green";
                    lblmesg.innerHTML = "Valid Date";
                }
                else {
                    lblmesg.style.color = "red";
                    lblmesg.innerHTML = "Invalid Date";
                    return;
                }

                //Range Validation
                if (txt.id.indexOf("txtRange") != -1)
                    RangeValidation(dt);

                //BirthDate Validation
                if (txt.id.indexOf("txtBirthDate") != -1)
                    BirthDateValidation(dt)
            }
            else if (val.length < 10) {
                lblmesg.style.color = "blue";
                lblmesg.innerHTML =
         "Required dd/mm/yy format. Slashes will come up automatically.";
            }
        }

    </script>
    <script type="text/javascript">
        function payment() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Millwise")) {
                confirm_value.value = "Yes";
                document.getElementById("<%= hdnfpaymentcomnfirm.ClientID %>").value = "Y";
            }
            else {
                confirm_value.value = "No";
                document.getElementById("<%= hdnfpaymentcomnfirm.ClientID %>").value = "N";
            }
            document.forms[0].appendChild(confirm_value);
        }

    </script>
    <script type="text/javascript">
        function Confirm12() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(" Millwise=>Ok / partywise=>Cancel ")) {
                confirm_value.value = "Yes";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            }
            else {
                confirm_value.value = "No";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "N";
            }
            //   document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            // debugger;


            //  if (paymentto != millcode) {
            // payment();
            //  }
            // else {
            //  document.getElementById("<%= hdnfpaymentcomnfirm.ClientID %>").value = "N";
            // }
            //document.forms[0].appendChild(confirm_value);
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

                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;

                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";

                document.getElementById("<%=txtSearchText.ClientID %>").value = "";

                var grid = document.getElementById("<%= grdPopup.ClientID %>");

                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
                var pageCount = document.getElementById("<%= hdHelpPageCount.ClientID %>").value;


                pageCount = parseInt(pageCount);
                if (pageCount > 1) {
                    SelectedRowIndex = SelectedRowIndex + 1;
                }
                if (hdnfClosePopupValue == "txtAcCode") {
                    document.getElementById("<%= txtAcCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblAcCodeName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtAcCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBankCode") {
                    document.getElementById("<%= txtAcCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblAcCodeName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;

                }
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";

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
        function ac_name(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnAcCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtAcCode.ClientID %>").val();


                $("#<%=txtAcCode.ClientID %>").val(unit);
                __doPostBack("txtAcCode", "TextChanged");

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   UTR Report   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:HiddenField ID="hdnfClosePopup" runat="server" />
    <asp:HiddenField ID="hdHelpPageCount" runat="server" />
    <asp:HiddenField ID="hdnfpaymentcomnfirm" runat="server" />
    <asp:HiddenField ID="hdconfirm" runat="server" />
    <center>
        <table width="100%" align="left" cellspacing="5">
            <tr>
                <td align="right" style="width: 40%;">
                    <b>Select Type</b>
                </td>
                <td align="left" colspan="5" style="width: 60%;">
                    <asp:DropDownList runat="server" ID="drpType" CssClass="ddl" Width="170px" TabIndex="1"
                        AutoPostBack="True" OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                        <asp:ListItem Text="Mill wise" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Bank wise" Value="B"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 40%;">
                    <b>Receipt Payment Type</b>
                </td>
                <td align="left" colspan="5" style="width: 60%;">
                    <asp:DropDownList runat="server" ID="drpTranTYpe" CssClass="ddl" Width="170px" TabIndex="1"
                        AutoPostBack="True" OnSelectedIndexChanged="drpTranTYpe_SelectedIndexChanged">
                        <asp:ListItem Text="Against Sauda" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Against SaleBill" Value="B"></asp:ListItem>
                        <asp:ListItem Text="Against Debit Note" Value="D"></asp:ListItem>
                        <asp:ListItem Text="Against Credit Bill" Value="P"></asp:ListItem>
                        <asp:ListItem Text="Against OnAc" Value="O"></asp:ListItem>
                        <asp:ListItem Text="Against RetailSale Bill" Value="R"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 40%;">
                    <b>Ac/Code</b>
                </td>
                <td align="left" colspan="5" style="width: 60%;">
                    <asp:TextBox ID="txtAcCode" runat="server" Width="80px" CssClass="txt" AutoPostBack="false"
                        OnTextChanged="txtAcCode_TextChanged" onkeyDown="ac_name(event);" Height="24px"
                        TabIndex="2"></asp:TextBox>
                    <asp:Button ID="btnAcCode" runat="server" Text="..." CssClass="btnHelp" OnClick="btnAcCode_Click"
                        Height="24px" Width="20px" />
                    <asp:Label ID="lblAcCodeName" runat="server" CssClass="lblName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>From:</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                        TabIndex="3" MaxLength="10" onkeyup="ValidateDate(this,event.keyCode);" onkeydown="DateFormat(this,event.keyCode);"> </asp:TextBox>
                    <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                        Width="25px" Height="15px" />
                    <ajax1:CalendarExtender ID="calenderExtendertxtFromDt" runat="server" TargetControlID="txtFromDate"
                        PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                    </ajax1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>To:</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                        TabIndex="4" MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"> </asp:TextBox>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                        Height="15px" />
                    <ajax1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                        PopupButtonID="Image1" Format="dd/MM/yyyy">
                    </ajax1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 40%;">
                </td>
                <td align="left" colspan="2" style="width: 60%;">
                    <asp:TextBox ID="txtUtrNo" runat="server" Width="80px" CssClass="txt" AutoPostBack="True"
                        TabIndex="5" Height="24px" Visible="false" OnTextChanged="txtUtrNo_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="btnUtrSummary" runat="server" Text="UTR Summary Report" CssClass="btnHelp"
                        Width="170px" Height="24px" Visible="false" TabIndex="10" OnClick="btnUtrSummary_Click" />
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 40%;">
                    <asp:Button ID="btnurtdetailbalance" runat="server" Text="UTR Detail Balance Report"
                        CssClass="btnHelp" Width="120px" Visible="false" Height="24px" TabIndex="10"
                        OnClick="btnurtdetailbalance_Click" />
                </td>
                <td align="left">
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td align="left" style="width: 40%;">
                    <asp:Button ID="btnDetailRptNew" runat="server" Text="UTR Report Summary" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnDetailRptNew_Click" />
                </td>
                <td align="Right">
                    <asp:Button ID="btnUTRNODOWise" runat="server" Text="UTR Detail Report New" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnUTRNODOWise_Click" />
                </td>
            </tr>
            <%--  <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                <legend style="text-align: center;">
                    <h5 style="color: White;" font-names="verdana" font-size="Medium">Mill Payment Detail
                    </h5>
                </legend>
            </fieldset>--%>
            <tr>
                <td align="left" style="width: 40%;">
                    <asp:Button ID="btnMillPaySummary" runat="server" Text="Mill Payment Summary" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnMillPaySummary_Click" />
                </td>
                <td align="Right">
                    <asp:Button ID="btnMillPayDetail" runat="server" Text="Mill Payment Detail" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnMillPayDetail_Click" />
                </td>
            </tr>
            <%-- <fieldset style="border-radius: 3px; width: 90%; margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                <legend style="text-align: center;">
                    <h5 style="color: White;" font-names="verdana" font-size="Medium"></h5>
                </legend>
            </fieldset>--%>
            <tr>
                <td align="left" style="width: 40%;">
                    <asp:Button ID="btnSaudaBalSummary" runat="server" Text="Sauda Summary" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnSaudaBalSummary_Click" />
                </td>
                <td align="Right">
                    <asp:Button ID="btnsaudaBalDetail" runat="server" Text="Sauda Detail" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnsaudaBalDetail_Click" />
                </td>
            </tr>
            <%--  <fieldset style="border-radius: 3px; width: 90%; margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                <legend style="text-align: center;">
                    <h5 style="color: White;" font-names="verdana" font-size="Medium"></h5>
                </legend>
            </fieldset>--%>
            <tr>
                <td align="left" style="width: 40%;">
                    <asp:Button ID="btnAgainstBillSummary" runat="server" Text="Sale Bill Summary" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnAgainstBillSummary_Click" />
                </td>
                <td align="right">
                    <asp:Button ID="btnAgaistSaleBillDetail" runat="server" Text="Sale Bill Detail" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnAgaistSaleBillDetail_Click" />
                </td>
            </tr>
            <%--  </table>
        <table>
            <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                <legend style="text-align: center;">
                    <h5 style="color: White;" font-names="verdana" font-size="Medium">Manually Purchecs Balance
                    </h5>
                </legend>
            </fieldset>--%>
            <tr>
                <td align="left" style="width: 40%;">
                    <asp:Button ID="btnpaymentbalance" runat="server" Text="Payment Balance" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnpaymentbalance_Click" />
                </td>
                <td align="right" style="width: 40%;">
                    <asp:Button ID="btnpaymentdetail" runat="server" Text=" purchase Payment detail "
                        CssClass="btnHelp" Width="170px" Height="24px" TabIndex="6" OnClick="btnpaymentdetail_Click" />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 40%;">
                    <asp:Button ID="btnmillcashdiff" runat="server" Text="Mill Diff" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnmillcashdiff_Click" />
                </td>
                <td align="right" style="width: 50%;">
                    <asp:Button ID="btnpartybrokercashdiff" runat="server" Text="Party Broker  Diff"
                        CssClass="btnHelp" Width="170px" Height="24px" TabIndex="6" OnClick="btnpartybrokercashdiff_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnPeridocTrailblance" runat="server" Text="Peridoc Trial Balnce"
                        CssClass="btnHelp" Width="170px" Height="24px" TabIndex="6" OnClick="btnPeridocTrailblance_Click" />
                </td>
                <td align="right" style="width: 50%;">
                    <asp:Button ID="btnDoCashDiff" runat="server" Text="Do  Diff" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnDoCashDiff_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btntenderidwisesauda" runat="server" Text="TenderId Wise Sauda" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btntenderidwisesauda_Click"
                        OnClientClick="Confirm12()" />
                </td>
                <td align="right" style="width: 50%;">
                    <asp:Button ID="btnalltrantype" runat="server" Text="Tran Type" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnalltrantype_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Mill Wise Sauda" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnmillwisesauda_Click" OnClientClick="Confirm12()" />
                </td>
                <td align="right" style="width: 50%;">
                    <asp:Button ID="btntenderdistpatchreport" runat="server" Text="Tender Dispatch Report"
                        CssClass="btnHelp" Width="170px" Height="24px" TabIndex="6" OnClick="btntenderdistpatchreport_Click" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="btnnormalsauda" runat="server" Text="Normal Sauda" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnnormalsauda_Click" />
                </td>
                <td align="right" style="width: 50%;">
                    <asp:Button ID="btnDoCancle" runat="server" Text="Do Cancle" CssClass="btnHelp" Width="170px"
                        Height="24px" TabIndex="6" OnClick="btnDoCancle_Click" />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 40%;">
                    <asp:Button ID="btnadjustBills" runat="server" Text="Adjust Bills" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnadjustBills_Click" />
                </td>
                <td align="right" style="width: 50%;">
                    <asp:Button ID="btnRetailBillDetail" runat="server" Text="Retail Bill Detail" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnRetailBillDetail_Click" />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 40%;">
                    <asp:Button ID="btnRetailBillSummary" runat="server" Text="Retail Bill Summary" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnRetailBillSummary_Click" />
                </td>
                <td align="right" style="width: 50%;">
                    <asp:Button ID="btnowndo" runat="server" Text="Own DO" CssClass="btnHelp" Width="170px"
                        Height="24px" TabIndex="6" OnClick="btnowndo_Click" />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 40%;">
                    <asp:Button ID="btnPendingPurchase" runat="server" Text="Pending Purchase" CssClass="btnHelp"
                        Width="170px" Height="24px" TabIndex="6" OnClick="btnPendingPurchase_Click" />
                </td>
                <td align="right" style="width: 50%;">
                    <asp:Button ID="btnPendingSale" runat="server" Text="Pending Sale" CssClass="btnHelp" Width="170px"
                        Height="24px" TabIndex="6" OnClick="btnPendingSale_Click" />
                </td>
            </tr>
        </table>
    </center>
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
                            EmptyDataText="No Records Found" ViewStateMode="Disabled" PageSize="20" AllowPaging="true"
                            HeaderStyle-BackColor="#6D8980" HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated"
                            OnPageIndexChanging="grdPopup_PageIndexChanging" Style="table-layout: fixed;"
                            OnRowDataBound="grdPopup_RowDataBound">
                            <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
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
</asp:Content>
