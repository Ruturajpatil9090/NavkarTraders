<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pgeloginForm.aspx.cs" Inherits="pgeloginForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <title>Login</title>
    <script type="text/javascript">
        function noCopyMouse(e) {
            var isRight = (e.button) ? (e.button == 2) : (e.which == 3);
            if (isRight) {
                alert('You Cant Copy And Paste The Password!');
                return false;
            }
            return true;
        }
        function noCopyKey(e) {
            var forbiddenKeys = new Array('c', 'x', 'v');
            var keyCode = (e.keyCode) ? e.keyCode : e.which;
            var isCtrl;


            if (window.event)
                isCtrl = e.ctrlKey
            else
                isCtrl = (window.Event) ? ((e.modifiers & Event.CTRL_MASK) == Event.CTRL_MASK) : false;


            if (isCtrl) {
                for (i = 0; i < forbiddenKeys.length; i++) {
                    if (forbiddenKeys[i] == String.fromCharCode(keyCode).toLowerCase()) {
                        alert('You Cant Copy And Paste The Password!');
                        return false;
                    }
                }
            }
            return true;
        }
    </script>
    <script language="javascript" src="JS/disableright.js" type="text/javascript">
    </script>
</head>
<body oncontextmenu="return false">
    <form id="form1" runat="server" align="center">
    <div style="background-color: rgb(243, 248, 247);">
        <table width="72%" align="center" style="background-color: White; margin-top: 0px;
            border: 1px solid rgb(230, 219, 219); height: 100%;">
            <tr>
                <td style="width: 100%; height: 100%;">
                    <table align="center" width="100%" cellpadding="2px" cellspacing="10px">
                        <tr>
                            <td colspan="2" align="left" style="width: 100%; background-color: #000000; color: White;
                                height: 30px; font-weight: bolder; font-size: larger; font-family: Calibri;">
                                Navkar Sugar Trading Company
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 90%;" align="left">
                                <asp:Panel ID="pnlLeft" runat="server" Width="100%" Height="500px" BorderStyle="Solid"
                                    BorderColor="GradientActiveCaption" BorderWidth="1px">
                                </asp:Panel>
                            </td>
                            <td valign="top" style="width: 40%;" align="center">
                                <asp:Panel ID="pnlright" Width="100%" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    BorderColor="Black" Font-Names="verdana">
                                    <table width="100%" cellpadding="4px" cellspacing="3px">
                                        <tr>
                                            <td colspan="2" align="left" style="background-color: lightgoldenrodyellow; color: Black;
                                                height: 30px;">
                                                Carporate Login
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Login Name:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLoginName" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Password:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn" OnClick="btnLogin_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <br />
                                <asp:Panel Width="100%" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"
                                    Font-Names="verdana" ID="pnlCustomer">
                                    <table width="100%" cellpadding="4px" cellspacing="3px">
                                        <tr>
                                            <td colspan="2" align="left" style="background-color: Gray; color: White; height: 30px;">
                                                Customer Login
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Account Code:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCustomerLoginName" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Password:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCustomerPassword" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="btnCoustomerLogin" runat="server" Text="Login" CssClass="btn" OnClick="btnCoustomerLogin_Click" />
                                                <asp:Button ID="btnCancelLogin" runat="server" Text="Cancel" CssClass="btn" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right" style="width: 100%; height: 25px; color: White; background-color: #000000;
                    font-size: smaller; font-family: verdana;">
                    Powered by lata software consultancy
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
