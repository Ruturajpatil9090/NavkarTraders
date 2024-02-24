<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeAccountType.aspx.cs" Inherits="Report_pgeAccountType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function sp(ac_type) {
            var tn;

            window.open('rptAccountList.aspx?ac_type=' + ac_type);    //R=Redirected  O=Original
        }

       

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Trial Balance Report  " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:Panel ID="pnlMain" runat="server" ForeColor="Black" Font-Size="14px">
        <table width="60%" align="center" cellpadding="10" cellspacing="5">
            <tr>
                <td align="left">
                    Select AC Type:
                </td>
                <td align="left">
                    <asp:DropDownList ID="drpAcType" runat="server" CssClass="ddl" Height="24px" Width="250px"
                        OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                        <asp:ListItem Text="All" Value="A" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Party" Value="P"></asp:ListItem>
                        <asp:ListItem Text="Supplier" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Bank" Value="B"></asp:ListItem>
                        <asp:ListItem Text="Cash" Value="C"></asp:ListItem>
                        <asp:ListItem Text="Relative" Value="R"></asp:ListItem>
                        <asp:ListItem Text="Fixed Assets" Value="F"></asp:ListItem>
                        <asp:ListItem Text="Interest Party" Value="I"></asp:ListItem>
                        <asp:ListItem Text="Income/Expenses" Value="E"></asp:ListItem>
                        <asp:ListItem Text="Trading" Value="O"></asp:ListItem>
                        <asp:ListItem Text="Mill" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Transport" Value="T"></asp:ListItem>
                        <asp:ListItem Text="Broker" Value="BR"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <asp:Button ID="btnGetData" runat="server" Text="Get Data" CssClass="btnHelp" Width="100px"
                        Height="24px" OnClick="btnReport_Click" />
                    &nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
