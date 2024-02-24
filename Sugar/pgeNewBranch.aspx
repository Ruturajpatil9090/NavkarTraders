<%@ Page Title="Branch Master" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeNewBranch.aspx.cs" Inherits="Sugar_pgeNewBranch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset style="border-top:1px dotted rgb(131, 127, 130); border-radius:3px; width:90%; margin-left:30px; float:left;  border-bottom:0px; padding-top:0px; padding-bottom:10px; border-left:0px; border-right:0px; height:7px; ">
<legend style="text-align:center;"><asp:Label ID="label1" runat="server" Text="   New Branch   " Font-Names="verdana" ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
</fieldset>
<br /><br /><br /><br />
<asp:Panel ID="pnlAddBranch" runat="server" BackColor="White" BorderColor="Olive" Font-Bold="true" BorderWidth="2px" Font-Names="verdana" Width="400px" Font-Size="12px">
    <table  width="100%" cellpadding="10px" cellspacing="10px">
    <tr>
    <td colspan="2" style="background-color:Olive; height:25px; color:Yellow;" >
    Create Branch 
    </td>
    </tr>
    <tr>
    <td align="left">
    Select Company:
    </td>
    <td align="left">
    <asp:DropDownList ID="drpCompany" runat="server" Height="30px" BorderColor="Olive"></asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td align="left">
    Branch:
    </td>
    <td align="left">
   <asp:TextBox ID="txtBranchName" runat="server" Height="30px" BorderColor="Olive" BorderWidth="1px"></asp:TextBox>
    </td>
    </tr>
        <tr>
    <td colspan="2" align="center">
    <asp:Button ID="btnAddBranch" runat="server" Text="Add Branch" Width="80px" 
            ForeColor="Yellow" Height="25px" BorderColor="Olive" BackColor="Olive" BorderWidth="1px"
            onclick="btnAddBranch_Click" Font-Bold="true" />
            &nbsp;
    <asp:Button ID="btnCancelBranch" runat="server" Text="Cancel" Width="80px" 
            ForeColor="Yellow" Height="25px" BorderColor="Olive" BackColor="Olive" BorderWidth="1px"
            onclick="btnCancelBranch_Click" Font-Bold="true" />
    </td>
    </tr>
    </table>
    </asp:Panel>
</asp:Content>

