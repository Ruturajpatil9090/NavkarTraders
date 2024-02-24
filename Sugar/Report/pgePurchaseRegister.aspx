<%@ Page Title="Purchase Register" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgePurchaseRegister.aspx.cs" Inherits="Report_pgePurchaseRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function pr(fromdt, todt, Ac_Code) {
            window.open('rptPurchaseRegister.aspx?fromdt=' + fromdt + '&todt=' + todt + '&Ac_Code=' + Ac_Code);    //R=Redirected  O=Original
        }

        function sr(fromdt, todt, Ac_Code) {
            //window.open('rptDateWiseSaleRegister.aspx?AcCode=' + Ac_Code + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
            window.open('rptSaleRegisterDateWise.aspx?fromdt=' + fromdt + '&todt=' + todt + ',&Ac_Code=' + Ac_Code);
        }

        function rpr(fromdt, todt) {
            // window.open('rptDateWiseReturnPurchaseRegister.aspx?AcCode=' + Ac_Code + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
            window.open('rptPurchaseReturnRegister_Datewise.aspx?fromdt=' + fromdt + '&todt=' + todt);
        }

        function rsr(fromdt, todt) {
            //window.open('rptDateWiseReturnSaleRegister.aspx?AcCode=' + Ac_Code + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
            window.open('rptSaleReturnRegister_DateWise.aspx?fromdt=' + fromdt + '&todt=' + todt);
        }
        function rs(fromdt, todt) {
            //window.open('rptDateWiseReturnSaleRegister.aspx?AcCode=' + Ac_Code + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
            window.open('rptRetailSaleRegister_Datewise.aspx?fromdt=' + fromdt + '&todt=' + todt);
        }

        function RCM(fromdt, todt, accode) {
            // window.open('rptDateWiseReturnPurchaseRegister.aspx?AcCode=' + Ac_Code + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
            window.open('rptRCM.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }
        function st(fromdt, todt, accode) {
            // window.open('rptDateWiseReturnPurchaseRegister.aspx?AcCode=' + Ac_Code + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
            window.open('rptsaletcs.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }
        function st1(fromdt, todt, accode) {
            // window.open('rptDateWiseReturnPurchaseRegister.aspx?AcCode=' + Ac_Code + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
            window.open('rptsaletcs1.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }
        function pt(fromdt, todt, accode) {
            // window.open('rptDateWiseReturnPurchaseRegister.aspx?AcCode=' + Ac_Code + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
            window.open('rptpurchasetcs.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }
        function pt1(fromdt, todt, accode) {
            // window.open('rptDateWiseReturnPurchaseRegister.aspx?AcCode=' + Ac_Code + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
            window.open('rptpurchasetcs1.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }
        function DetailReport(fromDt, ToDt) {
            var tn;

            window.open('rptReverseCharge.aspx?fromDt=' + fromDt + '&ToDt=' + ToDt, '_blank');    //R=Redirected  O=Original
        }
        function millsalereg(fromdt, todt, accode) {

            window.open('rptDateWiseSaleRegisterforMill.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }

        function stds(fromdt, todt, accode) {

            window.open('rptsaletds.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }
        function stds1(fromdt, todt, accode) {

            window.open('rptsaletds1.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }
        function ptds(fromdt, todt, accode) {

            window.open('rptpurchasetds.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }
        function ptds1(fromdt, todt, accode) {

            window.open('rptpurchasetds1.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }

        function salemonwise(fromdt, toDate) {
            var tn;

            window.open('rptsalemonwise.aspx?fromDt=' + fromdt + '&ToDt=' + toDate, '_blank');    //R=Redirected  O=Original
        }
        function purcmonwise(fromdt, toDate) {
            var tn;

            window.open('rptpurcmonwise.aspx?fromDt=' + fromdt + '&ToDt=' + toDate, '_blank');    //R=Redirected  O=Original
        }
        function jsale(Ac_Code, FromDt, ToDt) {
            window.open('rptjaggarysaleReport.aspx?AcCode=' + Ac_Code + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
        }
        function Saletcs(fromdt, todt, accode) {
            // window.open('rptDateWiseReturnPurchaseRegister.aspx?AcCode=' + Ac_Code + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
            window.open('rptsaletcsNew.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode + '&TCS=' + 0);
        }
        function Saletcs1(fromdt, todt, accode) {
            // window.open('rptDateWiseReturnPurchaseRegister.aspx?AcCode=' + Ac_Code + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
            window.open('rptsaletcsNew1.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode + '&TCS=' + 0);
        }
        function stds2(fromdt, todt, accode) { 

            window.open('rptsaletds2.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }
        function stds3(fromdt, todt, accode) { 
            window.open('rptsaletds3.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }
        function ptcs(fromdt, todt, accode) {
            // window.open('rptDateWiseReturnPurchaseRegister.aspx?AcCode=' + Ac_Code + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
            window.open('rptpurchasetcs2.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }
        function ptcs2(fromdt, todt, accode) {
            // window.open('rptDateWiseReturnPurchaseRegister.aspx?AcCode=' + Ac_Code + '&FromDt=' + FromDt + '&ToDt=' + ToDt);
            window.open('rptpurchasetcs3.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }
        function ptds2(fromdt, todt, accode) {

            window.open('rptpurchasetds2.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
        }
        function ptds3(fromdt, todt, accode) {

            window.open('rptpurchasetds3.aspx?fromdt=' + fromdt + '&todt=' + todt + '&accode=' + accode);
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
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <script type="text/javascript" language="javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Summary Report=>Ok / PartyWise=>Cancel ")) {
                confirm_value.value = "Yes";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            }
            else {
                confirm_value.value = "No";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function Accountcode(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnAcCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtAcCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtAcCode.ClientID %>").val(unit);
                __doPostBack("txtAcCode", "TextChanged");

            }

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
                if (hdnfClosePopupValue == "txtAcCode") {
                    document.getElementById("<%=txtAcCode.ClientID %>").focus();
                }
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="  Purchase And Sale Details Report   "
                Font-Names="verdana" ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:HiddenField ID="hdnfClosePopup" runat="server" />
    <asp:HiddenField ID="hdHelpPageCount" runat="server" />
    <asp:HiddenField ID="hdconfirm" runat="server" />
    <table width="60%" align="center">
        <tr>
            <td align="left" style="width: 40%;">Account Code:
            </td>
            <td align="left" colspan="2" style="width: 60%;">
                <asp:TextBox ID="txtAcCode" runat="server" Width="80px" CssClass="txt" AutoPostBack="false"
                    OnTextChanged="txtAcCode_TextChanged" Height="24px" onkeydown="Accountcode(event);"></asp:TextBox>
                <asp:Button ID="btnAcCode" runat="server" Text="..." CssClass="btnHelp" OnClick="btnAcCode_Click"
                    Height="24px" Width="20px" />
                <asp:Label ID="lblAcCodeName" runat="server" CssClass="lblName"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvtxtAcCode" runat="server" ControlToValidate="txtAcCode"
                    CssClass="validator" Display="Dynamic" Enabled="false" ErrorMessage="Required"
                    SetFocusOnError="true" Text="Required" ValidationGroup="add"> </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left">From Date:
            </td>
            <td align="left">
                <asp:TextBox ID="txtFromDt" runat="server" Width="80px" CssClass="txt" Height="24px"
                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                    Width="25px" Height="15px" />
                <ajax1:CalendarExtender ID="calenderExtendertxtFromDt" runat="server" TargetControlID="txtFromDt"
                    PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td align="left">To Date:
            </td>
            <td align="left">
                <asp:TextBox ID="txtToDt" runat="server" Width="80px" CssClass="txt" Height="24px"
                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                    Height="15px" />
                <ajax1:CalendarExtender ID="CalendarExtendertxtToDt" runat="server" TargetControlID="txtToDt"
                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnPurchaseReport" CssClass="btnHelp" Text="Purchase Report"
                    OnClick="btnPurchaseReport_Click" Width="180px" Height="24px" OnClientClick="purchaseReprt();" />
            </td>
            <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnSaleReport" CssClass="btnHelp" Text="Sale Report"
                    Width="180px" OnClick="btnSaleReport_Click" Height="24px" />
            </td>
        </tr>
        <tr>
            <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnReturnPurchaseReport" CssClass="btnHelp" Text="Return Purchase Report"
                    Width="180px" Height="24px" OnClick="btnReturnPurchaseReport_Click" />
            </td>
            <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnReturnSaleReport" CssClass="btnHelp" Text="Return Sale Report"
                    Width="180px" Height="24px" OnClick="btnReturnSaleReport_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnServiceBillReport" CssClass="btnHelp" Text="Service Bill Report"
                    Width="180px" Height="24px" OnClick="btnServiceBillReport_Click" />
            </td>
            <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnRetailSaleReport" CssClass="btnHelp" Text="Retail Sale Report"
                    Width="180px" Height="24px" OnClick="btnRetailSaleReport_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnRCM" CssClass="btnHelp" Text="RCM" Width="180px"
                    Height="24px" OnClick="btnRCM_Click" />
            </td>
            <td colspan="1" align="center">
                <asp:Button Text="Reverse Charge" runat="server" CssClass="btnHelp" ID="btnRerversechge"
                    OnClick="btnRerversechge_Click" Height="24px" Width="180px" />
            </td>
        </tr>
        <tr>
            <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnpurchasetcs" CssClass="btnHelp" Text="Sale TCS Report"
                    Width="180px" Height="24px" OnClick="btnsaletcsReport_Click" OnClientClick="Confirm()" />
            </td>
            <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnsaletcs" CssClass="btnHelp" Text="Purchase TCS Report"
                    Width="180px" Height="24px" OnClick="btnpurchasetcsReport_Click" OnClientClick="Confirm()" />
            </td>
        </tr>
        <tr>
            <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnmillsalereport" CssClass="btnHelp" Text="Mill Sale Report"
                    Width="180px" Height="24px" OnClick="btnmillsalereport_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnsaletdsReport" CssClass="btnHelp" Text="Sale TDS Report"
                    Width="180px" Height="24px" OnClick="btnsaletdsReport_Click" OnClientClick="Confirm()" />
            </td>
            <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnpurchasetdsReport" CssClass="btnHelp" Text="Purchase TDS Report"
                    Width="180px" Height="24px" OnClick="btnpurchasetdsReport_Click" OnClientClick="Confirm()" />
            </td>
        </tr>
         <tr>
            <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnsalemonwise" CssClass="btnHelp" Text="Sale Qty Monthwise"
                    Width="180px" Height="24px" OnClick="btnsalemonwise_Click"  />
            </td>
            <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnpurcmonwise" CssClass="btnHelp" Text="Purchase Qty Monthwise"
                    Width="180px" Height="24px" OnClick="btnpurcmonwise_Click"  />
            </td>
              </tr>
          <tr>
              <td colspan="1" align="center">
             <asp:Button runat="server" ID="btnjaggarysaleReport" CssClass="btnHelp" Text="Jaggary Sale Report"
                    OnClick="btnjaggarysaleReport_Click" Width="180px" Height="24px" />
            </td>

              <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnSaletcsrpt" CssClass="btnHelp" Text="Sale TCS Report = 0"
                    Width="180px" Height="24px" OnClick="btnSaletcsrpt_Click" OnClientClick="Confirm()" />
            </td>
              </tr>
                  <tr>
               <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnsaletdsReport2" CssClass="btnHelp" Text="Sale TDS Report = 0"
                    Width="180px" Height="24px" OnClick="btnsaletdsReport2_Click" OnClientClick="Confirm()" />
            </td>
                       <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnpurchasetcsReport2" CssClass="btnHelp" Text="Purchase TCS Report = 0"
                    Width="180px" Height="24px" OnClick="btnpurchasetcsReport2_Click" OnClientClick="Confirm()" />
            </td>
                        </tr>
                  <tr>
                          <td colspan="1" align="center">
                <asp:Button runat="server" ID="btnpurchasetdsReport2" CssClass="btnHelp" Text="Purchase TDS Report = 0"
                    Width="180px" Height="24px" OnClick="btnpurchasetdsReport2_Click" OnClientClick="Confirm()" />
            </td>
        </tr>
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
                        Width="250px" Height="20px" AutoPostBack="true"></asp:TextBox>
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
