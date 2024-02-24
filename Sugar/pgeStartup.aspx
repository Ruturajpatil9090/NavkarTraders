<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pgeStartup.aspx.cs" Inherits="pgeStartup" Title="Select Company" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .bagroundPopup
        {
            opacity: 0.6;
            background-color: Black;
        }
        .blur
        {
            alpha: opacity(0.5);
            opacity: 0.5;
            background-color: Black;
        }
        .lnk
        {
            text-decoration: none;
            font-family: Verdana;
            font-size: large;
            color: Maroon;
        }
        .lnk:hover
        {
            text-decoration: underline;
            color: Navy;
        }
        
        .lnks
        {
            text-decoration: none;
            font-family: Verdana;
            font-size: small;
            color: Maroon;
        }
        .lnks:hover
        {
            text-decoration: underline;
            color: Navy;
        }
    </style>
    <title></title>
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
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:UpdatePanel ID="upPopup" runat="server">
        <ContentTemplate>
            <ajax1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </ajax1:ToolkitScriptManager>
            <div>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnkCreateCompany" Text="Create Company" runat="server" CssClass="lnks"
                                OnClick="lnkCreateCompany_Click"></asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lnkCreateBranch" Text="Create Branch" runat="server" CssClass="lnks"
                                OnClick="lnkCreateBranch_Click"></asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lnkNewUser" Text="New User" runat="server" CssClass="lnks" OnClick="lnkNewUser_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; background-color: #3b5998; height: 30px;">
                        </td>
                    </tr>
                </table>
                <br />
                <table width="50%" align="center">
                    <tr>
                        <td style="text-align: center;">
                            <asp:GridView ID="grdCompany" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                Width="500px" OnRowCommand="grdCompany_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="Company_Code" HeaderText="" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCompany" runat="server" Font-Bold="true" CssClass="lnk" Text='<%#Eval("Company_Name_E") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="White" BorderWidth="0px" BorderColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlLoginPopup" runat="server" Width="400px" BackColor="White" BorderColor="Olive"
                    BorderWidth="2px" Font-Names="verdana" Style="position: absolute; display: none;">
                    <table width="100%" cellpadding="6px" cellspacing="6px">
                        <tr>
                            <td colspan="2" style="background-color: Olive; height: 5px; color: Yellow;">
                                Login to
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblLoginFailedMsg" runat="server" Text="Wrong User Name or Password!"
                                    Font-Size="Smaller" Font-Names="verdana" ForeColor="Red" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Login Name:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLoginName" runat="server" Height="30px" BorderColor="Olive" BorderWidth="1px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtLoginName" runat="server" ControlToValidate="txtLoginName"
                                    Text="*" ForeColor="Red" ValidationGroup="login" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Password:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPassword" runat="server" Height="30px" BorderColor="Olive" TextMode="Password"
                                    BorderWidth="1px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fvtxtPassword" runat="server" ControlToValidate="txtPassword"
                                    Text="*" ForeColor="Red" ValidationGroup="login" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Accounting Year:
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drpYear" runat="server" Width="100px" BorderColor="Olive" Height="30px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvdrpYear" runat="server" ControlToValidate="drpYear"
                                    InitialValue="0" Text="*" ForeColor="Red" ValidationGroup="login" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Current Branch:
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drpBranch" runat="server" Width="100px" BorderColor="Olive"
                                    Height="30px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:LinkButton ID="lnkForgotPass" runat="server" Text="Forgot Password" Font-Bold="false"
                                    Font-Names="Calibri" Font-Size="12px" ForeColor="Navy" OnClick="lnkForgotPass_Click"></asp:LinkButton>
                            </td>
                            <td align="right">
                                <asp:LinkButton ID="lnkChangePass" runat="server" Text="Change Password" Font-Bold="false"
                                    Font-Names="Calibri" Font-Size="12px" ForeColor="Navy" OnClick="lnkChangePass_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnLogin" runat="server" Text="Login" BackColor="Olive" ForeColor="Yellow"
                                    Width="60px" BorderColor="Olive" Height="25px" OnClick="btnLogin_Click" ValidationGroup="login" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="60px" ForeColor="Yellow"
                                    Height="25px" BorderColor="Olive" BackColor="Olive" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajax1:ModalPopupExtender ID="modalPoppLogin" PopupControlID="pnlLoginPopup" BackgroundCssClass="bagroundPopup"
                    TargetControlID="btn" runat="server">
                </ajax1:ModalPopupExtender>
                <asp:Button ID="btn" runat="server" Style="display: none;" />
                <div id="forgetPass" class="ForgotPass" style="display: none;">
                    <table width="100%" align="center" cellspacing="5">
                        <tr>
                            <td align="center" colspan="2" style="background-color: Blue;">
                                <p style="color: White; font-weight: bold">
                                    Password will sent back to your register mobile number</p>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                User Name:
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtUserNameForgot" Width="200px" Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:Label runat="server" ID="lblWrongUserName" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button runat="server" ID="btnSendForgetedPass" Text="SUBMIT" CssClass="button"
                                    OnClick="btnSendForgetedPass_Click" />
                            </td>
                            <td align="center">
                                <asp:Button runat="server" ID="btncan" Text="Cancel" CssClass="button" OnClick="btncan_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <ajax1:ModalPopupExtender ID="mpForgetPass" PopupControlID="forgetPass" BackgroundCssClass="bagroundPopup"
                    TargetControlID="btn2" runat="server">
                </ajax1:ModalPopupExtender>
                <asp:Button ID="btn2" runat="server" Style="display: none;" />
                <asp:Panel ID="pnlChangePassword" runat="server" Width="400px" BackColor="White"
                    BorderColor="Olive" BorderWidth="2px" Font-Names="verdana">
                    <table width="100%" cellpadding="6px" cellspacing="6px">
                        <tr>
                            <td colspan="2" style="background-color: Olive; height: 5px; color: Yellow;">
                                Change Password
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblerrChangePassword" runat="server" Text="Wrong Username or Password!"
                                    Font-Size="Smaller" Font-Names="verdana" ForeColor="Red" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                User Name:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCUserName" runat="server" CssClass="txt" Height="25px" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Old Password:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtOldPassword" runat="server" CssClass="txt" Height="25px" Width="200px"
                                    TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                New Password:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="txt" Height="25px" Width="200px"
                                    TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Re-Enter Password:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtnewCPassword" runat="server" CssClass="txt" Height="25px" Width="200px"
                                    TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnSave" runat="server" Text="Update" BackColor="Olive" ForeColor="Yellow"
                                    Width="60px" BorderColor="Olive" Height="25px" OnClick="btnSave_Click" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btnCancelNewPass" runat="server" Text="Cancel" Width="60px" ForeColor="Yellow"
                                    Height="25px" BorderColor="Olive" BackColor="Olive" OnClick="btnCancelNewPass_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajax1:ModalPopupExtender ID="popupChangePassword" PopupControlID="pnlChangePassword"
                    BackgroundCssClass="bagroundPopup" TargetControlID="btnChangePasspop" X="400"
                    Y="200" runat="server">
                </ajax1:ModalPopupExtender>
                <asp:Button ID="btnChangePasspop" runat="server" Style="display: none;" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="upPopup">
        <ProgressTemplate>
            <%--  <div id="Imgt"    style="position: absolute;width:100%; height:100%;" align="center" valign="top" runat="server" class="blur">   --%>
            <img src="~/Images/framely.gif" alt="Loading..." runat="server" align="middle" style="z-index: 8000;
                text-align: center; margin-left: 500px;" /><br />
            <%--   </div>  --%>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>
