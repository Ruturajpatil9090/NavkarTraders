<%@ Page Title="Balance Sheet" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeBalanceSheet.aspx.cs" Inherits="Report_pgeBalanceSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function sp(dt) {
            var tn;

            window.open('rptBalanceSheet.aspx?Cwhere=' + dt, '_blank');    //R=Redirected  O=Original
        }
        function PL(dt) {
            var tn;

            window.open('rptProfitLoss.aspx?dt=' + dt, '_blank');    //R=Redirected  O=Original
        }
        function sp1(dt) {
            var tn;

            window.open('rptStoreProcedureBalanceSheet.aspx?Cwhere=' + dt, '_blank');    //R=Redirected  O=Original
        }
        function PLSP(dt) {
            var tn;

            window.open('rptProfitLossSP.aspx?dt=' + dt, '_blank');    //R=Redirected  O=Original
        }
        function PLSP2(fdt, tdt) {
            var tn;

            window.open('rptProfitLossSPNew.aspx?fdt=' + fdt + '&tdt=' + tdt, '_blank');    //R=Redirected  O=Original
        }
        function dd(dt) {
           window.open('rptddreport.aspx?dt=' + dt, '_blank');    //R=Redirected  O=Original
        }
        function dd1(dt) {
            window.open('rptdd1report.aspx?dt=' + dt, '_blank');    //R=Redirected  O=Original
        }
        function dd1sum(dt) {
            window.open('rptdd1summeryreport.aspx?dt=' + dt, '_blank');    //R=Redirected  O=Original
        }
        function balsheet1(dt) {
            var tn;

            window.open('rptSPBalanceSheet.aspx?Cwhere=' + dt, '_blank');    //R=Redirected  O=Original
        }
        function crystal(dt) {
            var tn;

            window.open('rptSPBalanceSheet.aspx?Cwhere=' + dt, '_blank');    //R=Redirected  O=Original
        }
    </script>
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Balance Sheet  " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:Panel ID="pnlMain" runat="server" ForeColor="Black" Font-Size="14px">
        <table width="60%" align="center" cellpadding="10" cellspacing="5">
            <tr>
                <td align="left">Upto Date:
               
                    <asp:TextBox ID="txtToDt" runat="server" Width="80px" CssClass="txt" MaxLength="10"
                        onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                        Height="24px"></asp:TextBox>
                    <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                        Width="25px" Height="15px" />
                    <ajax1:CalendarExtender ID="calenderExtendertxtToDt" runat="server" TargetControlID="txtToDt"
                        PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                    </ajax1:CalendarExtender>
                </td>
                <td></td>
                <td align="center">
                    <asp:Button ID="btnBalanceSheet" runat="server" Width="130px" Text="Balance Sheet"
                        CssClass="btnHelp" OnClick="btnBalanceSheet_Click" Height="24px" Visible="false" />
                    &nbsp;
                    <asp:Button ID="btnProfitLoss" runat="server" Width="130px" Text="Profit & Loss"
                        CssClass="btnHelp" OnClick="btnProfitLoss_Click" Height="24px" />
                </td>
                <td align="center">
                    <asp:Button ID="btnBalanceSheetNew" runat="server" Width="130px" Text="Balance Sheet New"
                        CssClass="btnHelp" OnClick="btnBalanceSheetNew_Click" Height="24px" />
                </td>
                 <td align="center">
                    <asp:Button ID="btnBalSheetNew" runat="server" Width="130px" Text="Balance Sheet"
                        CssClass="btnHelp" OnClick="btnBalSheetNew_Click" Height="24px" />
                </td>
                <td align="center">
                    <asp:Button ID="btndd1report" runat="server" Width="130px" Text="DD1 Report"
                        CssClass="btnHelp" OnClick="btndd1report_Click" Height="24px" />
                     <asp:Button ID="btndd1summeryrpt" runat="server" Width="150px" Text="DD1 Summery Report"
                        CssClass="btnHelp" OnClick="btndd1summeryrpt_Click" Height="24px" />
                </td>
                <td align="center">
                    <asp:Button ID="btnddreport" runat="server" Width="130px" Text="DD Report"
                        CssClass="btnHelp" OnClick="btnddreport_Click" Height="24px" Visible="false"/>
                </td>
            </tr>
            <tr>
                <td align="left">From Date:
                
                    <asp:TextBox ID="txtFromDate" runat="server" Width="80px" CssClass="txt" MaxLength="10"
                        onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                        Height="24px"></asp:TextBox>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                        Width="25px" Height="15px" />
                    <ajax1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                        PopupButtonID="Image1" Format="dd/MM/yyyy">
                    </ajax1:CalendarExtender>
                </td>
                <td>To Date:
                
                    <asp:TextBox ID="txtTooDate" runat="server" Width="80px" CssClass="txt" MaxLength="10"
                        onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                        Height="24px"></asp:TextBox>
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                        Width="25px" Height="15px" />
                    <ajax1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTooDate"
                        PopupButtonID="Image2" Format="dd/MM/yyyy">
                    </ajax1:CalendarExtender>
                </td>
                <td align="center">

                    <asp:Button ID="btnProfitLoss2" runat="server" Width="130px" Text="Profit & Loss"
                        CssClass="btnHelp" OnClick="btnProfitLoss2_Click" Height="24px" />
                </td>
                <td align="center">
                    <asp:Button ID="Button3" runat="server" Width="130px" Text="Balance Sheet New"
                        CssClass="btnHelp" OnClick="btnBalanceSheetNew_Click" Height="24px" Visible="false" />
                </td>

                 <td align="center">
                    <asp:Button ID="btncrystal" runat="server" Width="130px" Text="Crystal"
                        CssClass="btnHelp" OnClick="btncrystal_Click" Height="24px" />
                </td>
                
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
