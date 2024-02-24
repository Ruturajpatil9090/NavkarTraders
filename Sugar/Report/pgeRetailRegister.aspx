<%@ Page Title="Retail Register" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeRetailRegister.aspx.cs" Inherits="Report_pgeRetailRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script type="text/javascript" src="http://code.jquery.com/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <link href="../CSS/tooltip.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/tooltip.js">
    </script>
    <script type="text/javascript" src="../JS/select all.js"></script>
    <script type="text/javascript">
        function retail(FromDT, ToDt) {
            var tn;
            window.open('rptRetailregister.aspx?FromDT=' + FromDT + '&ToDt=' + ToDt);
        }

        function retailtaxwise(FromDT, ToDt) {
            var tn;
            window.open('rptRetailregistertaxwise.aspx?FromDT=' + FromDT + '&ToDt=' + ToDt);
        }

       </script>
     
    <script type="text/javascript" src="../JS/DateValidation.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Retail Register  " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <table width="30%" align="center">
        <tr>
            <td align="left">From Date:
            </td>
            <td align="left">
                <asp:TextBox ID="txtFromDt" runat="server" Width="80px" CssClass="txt" Height="24px"
                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                    Width="25px" Height="15px" />
                <ajax1:calendarextender id="calenderExtendertxtFromDt" runat="server" targetcontrolid="txtFromDt"
                    popupbuttonid="imgcalender" format="dd/MM/yyyy">
                </ajax1:calendarextender>
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
                <ajax1:calendarextender id="CalendarExtendertxtToDt" runat="server" targetcontrolid="txtToDt"
                    popupbuttonid="Image1" format="dd/MM/yyyy">
                </ajax1:calendarextender>
            </td>
        </tr>
        <tr>
            <td>
                 <asp:Button ID="btnReailSaleRegister" runat="server" Text="Sale Register Report"
                    CssClass="btnHelp" Width="200px" Height="24px" TabIndex="6" OnClick="btnReailSaleRegister_Click" />
            </td>

        </tr>

        <tr>
            <td>
                 <asp:Button ID="btnReailSaleRegistertaxwise" runat="server" Text="Exps wise Sale "
                    CssClass="btnHelp" Width="200px" Height="24px" TabIndex="6" OnClick="btnReailSaleRegistertaxwise_Click" />
            </td>

        </tr>

    </table>
</asp:Content>

