<%@ Page Title="Trial Balance" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeTrialBalance.aspx.cs" Inherits="Report_pgeTrialBalance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function TBS(Doc_Date, Cwhere, FromDt, ToDt, ac_type, group_type) {

            window.open('rptTrialBalance.aspx?Doc_Date=' + Doc_Date + '&Cwhere=' + Cwhere + '&FromDt=' + FromDt
             + '&ToDt=' + ToDt + '&ac_type=' + ac_type + '&group_type=' + group_type, '_blank');    //R=Redirected  O=Original

        }
        function CTBS(Doc_Date, Cwhere, FromDt, ToDt, ac_type, group_type) {

            window.open('rptTransactionTrialBalance.aspx?Doc_Date=' + Doc_Date + '&Cwhere=' + Cwhere + '&FromDt=' + FromDt
             + '&ToDt=' + ToDt + '&ac_type=' + ac_type + '&group_type=' + group_type, '_blank');    //R=Redirected  O=Original

        }
        function sp(Doc_Date, Cwhere, FromDt, ToDt, GroupCode) {
            var tn;

            window.open('rptTrialBalance.aspx?Doc_Date=' + Doc_Date + '&Cwhere=' + Cwhere + '&FromDt=' + FromDt + '&ToDt=' + ToDt + '&GroupCode=' + GroupCode, '_blank');    //R=Redirected  O=Original
        }


        function op(ToDt, whr1) {
            window.open('rptOpeningBalance.aspx?ToDt=' + ToDt + '&whr1=' + whr1);    //R=Redirected  O=Original
        }

        function DetailReport(fromDt, ToDt, whr1) {
            var tn;

            window.open('rptACBalanceList.aspx?fromDt=' + fromDt + '&ToDt=' + ToDt + '&whr1=' + whr1, '_blank');    //R=Redirected  O=Original
        }

        function TFormat(ToDt, whr1, Ac_Type) {
            var tn;

            window.open('rptTFormat.aspx?ToDt=' + ToDt + '&whr1=' + whr1 + '&Ac_Type=' + Ac_Type, '_blank');    //R=Redirected  O=Original
        }
        function CTBS(Doc_Date, Cwhere, FromDt, ToDt, ac_type, group_type) {

            window.open('rptTransactionTrialBalance.aspx?Doc_Date=' + Doc_Date + '&Cwhere=' + Cwhere + '&FromDt=' + FromDt
             + '&ToDt=' + ToDt + '&ac_type=' + ac_type + '&group_type=' + group_type, '_blank');    //R=Redirected  O=Original

        }

        function multipleledger(fromDt, ToDt) {
            var tn;

            window.open('rptmultipleledegrnew.aspx?fromDt=' + fromDt + '&ToDt=' + ToDt, '_blank');    //R=Redirected  O=Original
        }
        function NJV(Fromdate, Todate) {
            window.open('../Report/rptJVNew.aspx?Fromdate=' + Fromdate + '&Todate=' + Todate);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Trial Balance Report  " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:Panel ID="pnlMain" runat="server" ForeColor="Black" Font-Size="14px">
        <table width="60%" align="center" cellpadding="10" cellspacing="5">
            <tr>
                <td align="left" colspan="2">From Date:
                    <asp:TextBox ID="txtFromDate" runat="server" Width="80px" CssClass="txt" MaxLength="10"
                        onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                        Height="24px"></asp:TextBox>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                        Height="15px" />
                    <ajax1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                        PopupButtonID="Image1" Format="dd/MM/yyyy">
                    </ajax1:CalendarExtender>
                    &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; To Date:
                    <asp:TextBox ID="txtToDt" runat="server" Width="80px" CssClass="txt" MaxLength="10"
                        onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                        Height="24px"></asp:TextBox>
                    <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                        Width="25px" Height="15px" />
                    <ajax1:CalendarExtender ID="calenderExtendertxtToDt" runat="server" TargetControlID="txtToDt"
                        PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                    </ajax1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:RadioButtonList ID="radioFilter" runat="server" RepeatDirection="Horizontal"
                        CellPadding="6" CellSpacing="6" AutoPostBack="True" OnSelectedIndexChanged="radioFilter_SelectedIndexChanged">
                        <asp:ListItem Text="Balance Sheet Group" Value="B" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Account Type" Value="A"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td align="left">Select Group:
                </td>
                <td align="left">
                    <asp:DropDownList ID="drpGroup" runat="server" CssClass="ddl" Height="24px" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">Select AC Type:
                </td>
                <td align="left">
                    <asp:DropDownList ID="drpAcType" runat="server" CssClass="ddl" Height="24px" Width="250px">
                        <asp:ListItem Text="All" Value="Q"></asp:ListItem>
                        <asp:ListItem Text="Party" Value="P" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Local" Value="L"></asp:ListItem>
                        <asp:ListItem Text="Party & Mill" Value="PM"></asp:ListItem>
                        <asp:ListItem Text="Supplier" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Bank" Value="B"></asp:ListItem>
                        <asp:ListItem Text="Cash" Value="C"></asp:ListItem>
                        <asp:ListItem Text="Relative" Value="R"></asp:ListItem>
                        <asp:ListItem Text="Fixed Assets" Value="F"></asp:ListItem>
                        <asp:ListItem Text="Interest Party" Value="I"></asp:ListItem>
                        <asp:ListItem Text="Income/Expenses" Value="E"></asp:ListItem>
                        <asp:ListItem Text="Trading" Value="O"></asp:ListItem>
                        <asp:ListItem Text="Mill" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Transport" Value="T"></asp:ListItem>
                        <asp:ListItem Text="Broker" Value="BR"></asp:ListItem>
                        <asp:ListItem Text="Retail Party" Value="RP"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <asp:Button ID="btnGetData" runat="server" Text="Trail Balance" CssClass="btnHelp"
                        Width="100px" OnClick="btnGetData_Click" Height="24px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnDetailReport" runat="server" Text="Detail Report" CssClass="btnHelp"
                        Width="100px" OnClick="btnDetailReport_Click" Height="24px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnTFormat" runat="server" Text="T-Format" CssClass="btnHelp" Width="100px"
                        OnClick="btnTFormat_Click" Height="24px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCheckdiff" runat="server" Text="Check Difference" CssClass="btnHelp"
                        Width="100px" Height="24px" OnClick="btnCheckdiff_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <asp:Button Text="Opening Balance" runat="server" CssClass="btnHelp" ID="btnOpeningBalance"
                        OnClick="btnOpeningBalance_Click" />

                    <asp:Button ID="btnCashdiffTrailBalance" runat="server" Text="Trail Balance1" CssClass="btnHelp"
                        Width="120px" OnClick="btnCashdiffTrailBalance_Click" Height="24px" />

                    <asp:Button ID="btnmultiplelegerfinal" runat="server" Text="Multiple Ledger New" CssClass="btnHelp"
                        Width="150px" OnClick="btnmultiplelegerfinal_Click" Height="24px" />
                     &nbsp;&nbsp;
                    <asp:Button ID="btnnewjv" runat="server" Text="JV Report" CssClass="btnHelp" Width="100px"
                        Height="24px" OnClick="btnnewjv_Click" />
                </td>
            </tr>
            <tr>
                 <td>
                    <asp:Button Text="Manage trade Creditors Group" runat="server" CssClass="btnHelp" ID="btnManage" Height="24px"  Width="200px"
                        OnClick="btnManage_Click" />
                </td>
                <td>
                   <asp:Button Text="Manage Trade Debitors Group" runat="server" CssClass="btnHelp" ID="btnManageDebit" Height="24px"  Width="200px"
                        OnClick="btnManageDebit_Click" />
                </td>
                </tr>
        </table>
    </asp:Panel>
</asp:Content>
