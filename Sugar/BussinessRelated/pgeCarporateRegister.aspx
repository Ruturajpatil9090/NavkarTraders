<%@ Page Title="Carporate Register" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeCarporateRegister.aspx.cs" Inherits="Sugar_pgeCarporateRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="../CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Script/jquery-1.4.2.js"></script>

    <link href="../CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../JQuery/jquery-1.4.1.js" type="text/javascript"></script>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../JQuery/jquery.keynavigation.js" type="text/javascript"></script>
    <link type="text/css" href="../menu/menu.css" rel="stylesheet" />
    <script type="text/javascript" src="../menu/jquery.js"></script>
    <script type="text/javascript" src="../menu/menu.js"></script>
    <script type="text/javascript" src="../JS/DateValidation.js"> </script>

    <script src="../JS/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../Scripts/selectfirstrow.js" type="text/javascript"></script>

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

                  if (hdnfClosePopupValue == "txtac_code") {
                    document.getElementById("<%=txtac_code.ClientID %>").focus();
                }

                    document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
    </script>

    <script type="text/javascript">
        function br(fromDt, toDt, Branch_Code, ac_code) {
            window.open('../Report/rptCarporateBalance.aspx?fromDt=' + fromDt + '&toDt=' + toDt + '&Branch_Code=' + Branch_Code + '&ac_code=' + ac_code);
        }

        function csd(fromDt, toDt, PDS, Branch_Code, ac_code, lotno) {
            window.open('../Report/rptCarporateSaleDetail.aspx?fromDt=' + fromDt + '&toDt=' + toDt + '&PDS=' + PDS + '&Branch_Code=' + Branch_Code + '&ac_code=' + ac_code + '&lotno=' + lotno);
        }
    </script>
    <script type="text/javascript" src="../JS/DateValidation.js"></script>
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
                if (hdnfClosePopupValue == "txtac_code") {
                    debugger;
                    document.getElementById("<%=txtac_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblParty_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=F1Hdnf.ClientID %>").value = "0";
                    document.getElementById("<%=txtac_code.ClientID %>").focus();
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
        function Party(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                // $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=F1Hdnf.ClientID%>").val("0");
                $("#<%=btntxtac_code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtac_code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtac_code.ClientID %>").val(unit);
                __doPostBack("txtac_code", "TextChanged");

            }

        }
    </script>
     <script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdconfirm" runat="server" />
    <asp:HiddenField ID="hdnfClosePopup" runat="server" />
    <asp:HiddenField ID="hdnf" runat="server" />
    <asp:HiddenField ID="F1Hdnf" runat="server" />
    <asp:HiddenField ID="hdnfSuffix" runat="server" />
    <asp:HiddenField ID="hdHelpPageCount" runat="server" />
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Carporate Register   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel runat="server" ID="updatepnlMain" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="50%" align="center" cellspacing="10">
                <tr>
                    <td align="center" colspan="2" style="width: 100%">
                        <asp:Label ID="lblBranch" Text="Select Branch:" ForeColor="White" Font-Bold="true"
                            runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="drpBranch" runat="server" Width="200px" CssClass="ddl">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="width: 100%">
                        <asp:Label ID="Label2" Text="Select Sell Type:" ForeColor="White" Font-Bold="true"
                            runat="server"></asp:Label>
                        <asp:DropDownList ID="drpSellingType" runat="server" Width="200px" CssClass="ddl">
                            <asp:ListItem Text="Carporate Sell" Value="C"></asp:ListItem>
                            <asp:ListItem Text="PDS Sell" Value="P"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 100%;" colspan="2">
                        <asp:Label ID="Label3" Text="Party:" ForeColor="White" Font-Bold="true" runat="server"></asp:Label><asp:TextBox
                            ID="txtac_code" runat="Server" CssClass="txt" TabIndex="1" Width="90px" onKeyDown="Party(event);" Style="text-align: right;"
                            AutoPostBack="True" OnTextChanged="txtac_code_TextChanged" Height="24px"></asp:TextBox>
                        <asp:Button ID="btntxtac_code" runat="server" Text="..." OnClick="btntxtac_code_Click"
                            CssClass="btnHelp" Height="24px" Width="20px" />
                        <asp:Label ID="lblParty_name" runat="server" CssClass="lblName"></asp:Label>
                        <asp:Label Text="" ID="lblPartyCommission" Font-Bold="true" ForeColor="Yellow" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">From Date:
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                            TabIndex="4" MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                        <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                            Width="25px" Height="15px" />
                        <ajax1:CalendarExtender ID="calendertxtFromDate" runat="server" TargetControlID="txtFromDate"
                            PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                        </ajax1:CalendarExtender>
                        To Date:
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                            TabIndex="5" MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                            Height="15px" />
                        <ajax1:CalendarExtender ID="CalendarExtendertxtToDate" runat="server" TargetControlID="txtToDate"
                            PopupButtonID="Image1" Format="dd/MM/yyyy">
                        </ajax1:CalendarExtender>
                        Lot No
                        <asp:TextBox ID="txtLot_No" runat="server" CssClass="txt" Width="80px" Height="24px"
                            TabIndex="5" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 50%">
                        <asp:Button runat="server" ID="btnBalanceReport" Text="Balance Report" Width="150px"
                            Height="24px" TabIndex="6" CssClass="btnHelp" OnClick="btnBalanceReport_Click" />
                    </td>
                    <td align="center" style="width: 50%">
                        <asp:Button runat="server" ID="Button1" Text="Lotwise Detail" Width="150px" CssClass="btnHelp"
                            Height="24px" TabIndex="7" OnClick="Button1_Click" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="90%"
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
