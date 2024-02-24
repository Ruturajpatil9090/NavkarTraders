<%@ Page Title="Upload Sign" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeUploadSignature.aspx.cs" Inherits="Sugar_Utility_pgeUploadSignature" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Signature Upload   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <br />
    <br />
    <asp:UpdatePanel runat="server" ID="up2">
        <ContentTemplate>
            <table align="center" cellspacing="5">
                <tr>
                    <td>
                        <asp:FileUpload runat="server" ID="fu1" ForeColor="White" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnUpload" CssClass="btnHelp" Text="Upload" Width="100px"
                            Height="25px" OnClick="btnUpload_Click" />
                    </td>

                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

