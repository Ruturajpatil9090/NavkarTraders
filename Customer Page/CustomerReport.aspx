<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerReport.aspx.cs" Inherits="Customer_Page_CustomerReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/CustomerPage.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="header">
            <span class="headerspan"><b>Welcome To
                <asp:Label runat="server" ID="lblCompanyName"></asp:Label></b> </span>
                
                <span class="headerspanlogut">
                    <asp:Label runat="server" ID="lblUser"></asp:Label>
                    <asp:LinkButton ID="lnk" runat="server" Text="LogOut" OnClick="lnk_Click"></asp:LinkButton></span>
        </div>
        <div id="main">
            <ajax1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
            </ajax1:ToolkitScriptManager>
            <center>
                <fieldset style="width: 500px;">
                    <legend>Welcome Customer</legend>
                    <table cellpadding="5">
                        <tr>
                            <td>
                                Account No:
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtCustAccNo" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Name Of Account:
                            </td>
                            <td align="left">
                                <asp:Label runat="server" ID="lblCustName" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address:
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtCustAddress" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Mobile:
                            </td>
                            <td align="left">
                                <asp:Label runat="server" ID="lblCustMobile" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </center>
            <br />
            <asp:Panel runat="server" ID="pnlMain">
                <center>
                    <table>
                        <tr>
                            <td>
                                From Date:
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtFromDate" Width="80px" CssClass="txt" Height="24px"></asp:TextBox>
                                <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                    Width="25px" Height="15px" />
                                <ajax1:CalendarExtender ID="calenderExtendertxtFromDt" runat="server" TargetControlID="txtFromDate"
                                    PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                            </td>
                            <td>
                                To Date:
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtToDate" Width="80px" CssClass="txt" Height="24px"></asp:TextBox>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                    Height="15px" />
                                <ajax1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </center>
                <center>
                    <table>
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="btnLedger" Text="Ledger" Height="24px" />&nbsp;
                                <asp:Button runat="server" ID="btnLocalVoucher" Text="Local Voucher" Height="24px" />&nbsp;
                                <asp:Button runat="server" ID="btnCommissionVoucher" Text="Commission Voucher" Height="24px" />&nbsp;
                                <asp:Button runat="server" ID="btnDO" Text="Delivery Order" Height="24px" />&nbsp;
                                <asp:Button runat="server" ID="btnMotorMemo" Text="Motor Memo" Height="24px" />&nbsp;
                                <asp:Button runat="server" ID="btnMillWiseDispatch" Text="Mill Wise Dispatch" Height="24px" />&nbsp;
                            </td>
                        </tr>
                    </table>
                </center>
            </asp:Panel>
        </div>
        <div id="footer">
        </div>
    </div>
    </form>
</body>
</html>
