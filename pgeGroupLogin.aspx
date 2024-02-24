<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pgeGroupLogin.aspx.cs" Inherits="pgeGroupLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Group Login</title>
    <link href="CSS/cssCommon.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center">
            <tr>
                <td>
                    <asp:Panel runat="server" align="center" BorderColor="Black" BorderStyle="Double"
                        BorderWidth="2px" Width="350px" Height="200px" Font-Names="verdana">
                        <table width="100%" align="center">
                            <tr>
                                <td colspan="2" align="center" style="background-color: Olive; width: 20px;">
                                    Group Login
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 30px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Group UserName:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtGroupUsername" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Group Password:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnLogin" runat="server" CssClass="btnSubmit" Text="Login" OnClick="btnLogin_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btnSubmit" Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
