<%@ Page Title="Other Utility" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeOther_Utility.aspx.cs" Inherits="Sugar_Utility_pgeOther_Utility" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Enter page name " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <table width="20%" align="center">
                <tr>
                    <td align="left" style="width: 5%;">From Date
                    </td>
                    <td align="left" style="width: 10%;">
                        <asp:TextBox Height="24px" ID="txtFrom_Date" runat="Server" CssClass="txt" TabIndex="4"
                            Width="90px" Style="text-align: left;" AutoPostBack="false" onkeyup="ValiddateDate(this,event.keyCode)"
                            onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                        <asp:Image ID="imgcalendertxtFrom_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                            Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtFrom_Date"
                                runat="server" TargetControlID="txtFrom_Date" PopupButtonID="imgcalendertxtFrom_Date"
                                Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 5%;">To Date
                    </td>
                    <td align="left" style="width: 10%;">
                        <asp:TextBox Height="24px" ID="txtTo_Date" runat="Server" CssClass="txt" TabIndex="4"
                            Width="90px" Style="text-align: left;" AutoPostBack="false" onkeyup="ValiddateDate(this,event.keyCode)"
                            onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                        <asp:Image ID="ImagetxttxtTo_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                            Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtendertxtTo_Date"
                                runat="server" TargetControlID="txtTo_Date" PopupButtonID="ImagetxtInwordDate"
                                Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Button ID="btnSalePosting" runat="server" Text="SB Posting" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSalePosting_Click" Height="24px" TabIndex="25" />
                        <asp:Label ID="lbltotcount" runat="server" CssClass="lblName" ForeColor="Red"></asp:Label>
                        <asp:Label ID="lblcount" runat="server" CssClass="lblName"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="btnPurchasePosting" runat="server" Text="PS Posting" CssClass="btnHelp"
                            Width="90px" ValidationGroup="add" OnClick="btnPurchasePosting_Click" Height="24px"
                            TabIndex="25" />

                    </td>
                     <td>
                        <asp:Button ID="btndotds" runat="server" Text="DO TDS Posting" CssClass="btnHelp"
                            Width="90px" ValidationGroup="add" OnClick="btndotds_Click" Height="24px"
                            TabIndex="25" />

                    </td>

                     <td>
                        <asp:Button ID="btntenderUpdate" runat="server" Text="Tender Update" CssClass="btnHelp"
                            Width="90px" ValidationGroup="add" OnClick="btntenderUpdate_Click" Height="24px"
                            TabIndex="25" />

                    </td>

                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

