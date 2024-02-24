<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeUserSecurity.aspx.cs" Inherits="Sugar_Utility_pgeUserSecurity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnfyearcode" runat="server" />
    <asp:HiddenField ID="hdnfcompanycode" runat="server" />
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Header Address   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <br />
    <br />
    <asp:Panel runat="server" ID="pnlMain">
        <table width="60%" align="center" cellspacing="5">
            <tr>
                <td align="right" style="width: 30%;">
                    <b>IP Address 1:</b>
                </td>
                <td align="left" style="width: 70%;">
                    <asp:TextBox runat="server" ID="txtIPAddress1" Width="600px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>IP Address 2:</b>
                </td>
                <td align="left" style="width: 70%;">
                    <asp:TextBox runat="server" ID="txtIPAddress2" Width="600px" Height="24px"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td align="center" colspan="2">
                    <asp:Button runat="server" ID="btnSave" Text="SAVE" CssClass="btnHelp" Width="100px"
                        OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

