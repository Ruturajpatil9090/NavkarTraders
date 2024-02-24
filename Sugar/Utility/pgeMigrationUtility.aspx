<%@ Page Title="Migration" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeMigrationUtility.aspx.cs" Inherits="Sugar_Utility_pgeMigrationUtility" %>


<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table style="margin-top: 50px;">
        <tr>
            <td style="padding-right: 20px;">Select Company:
            </td>
            <td>
                <asp:TextBox ID="txtYear" Height="24px" Width="90px" runat="server" Visible="false"></asp:TextBox></td>
            <td>
                <asp:DropDownList ID="drpCompany" runat="server" Width="200px" BorderColor="Olive" Height="30px">
                </asp:DropDownList></td>
            <td>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" class="btnHelp"
                    ValidationGroup="save" Width="90px" Height="40px" OnClick="btnDelete_Click" />
            </td>
        </tr>

        <tr>
            <td style="padding-right: 20px;">
                 <asp:Button ID="btnGroupMaster" runat="server" Text="Group Master" class="btnHelp"
                    ValidationGroup="save" Width="90px" Height="40px" OnClick="btnGroupMaster_Click" />
                </td>
            &emsp;
            <td>
                <asp:Button ID="btnuser" runat="server" Text="User" class="btnHelp"
                    ValidationGroup="save" Width="90px" Height="40px" OnClick="btnuser_Click" Visible="false" />
            </td>
        </tr>
        <tr>
            <td style="padding-right: 20px;">
                <asp:Button ID="btnCityMaster" runat="server" Text="City Master" class="btnHelp"
                    ValidationGroup="save" Width="90px" Height="40px" OnClick="btnCityMaster_Click" />
               
            </td>
            <td>
                <asp:Button ID="btnTender" runat="server" Text="Tender Purchase" class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnTender_Click" Visible="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnaccountmaster" runat="server" Text="Account Master" class="btnHelp"
                    ValidationGroup="save" Width="150px" Height="40px" OnClick="btnaccountmaster_Click" />

            </td>
            <td>
                <asp:Button ID="btnupdatecitygst" runat="server" Text="Update City" class="btnHelp"
                    Visible="false" ValidationGroup="save" Width="90px" Height="40px" OnClick="btnupdatecitygst_Click"  />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnpartyunit" runat="server" Text="Party Unit" class="btnHelp"
                    ValidationGroup="save" Width="90px" Height="40px" OnClick="btnpartyunit_Click" />

            </td>
            <td>
                <asp:Button ID="btnpssbid" runat="server" Text="purchase sale id update" class="btnHelp"
                    Visible="false" ValidationGroup="save" Width="120px" Height="40px" OnClick="btnpssbid_Click"  />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnitemmaster" runat="server" Text="Item Master" class="btnHelp"
                    ValidationGroup="save" Width="90px" Height="40px" OnClick="btnitemmaster_Click" />

            </td>
            <td>
                <asp:Button ID="btnutr" runat="server" Text="UTR" class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnutr_Click" Visible="false" />
            </td>

        </tr>
        <tr>
            <td>
                <asp:Button ID="btnCompanyParameter" runat="server" Text="Company Parameter" class="btnHelp"
                    ValidationGroup="save" Width="150px" Height="40px" OnClick="btnCompanyParameter_Click" />

            </td>
            <td>
                <asp:Button ID="btnpurches" runat="server" Text="Purchase " class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnpurches_Click" Visible="false" />
            </td>
            <td>
                <asp:Button ID="btnSaleBill" runat="server" Text="SaleBill" class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnSaleBill_Click" Visible="false" />
            </td>


        </tr>
        <tr>

            <td>
                <asp:Button ID="btnDO" runat="server" Text="DO" class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnDO_Click" Visible="false" />
            </td>

            <td>
                <asp:Button ID="btnReciptPayment" runat="server" Text="ReciptPayment" class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnReciptPayment_Click" Visible="false" />
            </td>
            <td>
                <asp:Button ID="btnService_Bill" runat="server" Text="Service Bill " class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnService_Bill_Click" Visible="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnReturn_Purchase" runat="server" Text="Purchase Return" class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnReturn_Purchase_Click" Visible="false" />
            </td>
            <td>
                <asp:Button ID="btnsalereturn" runat="server" Text="Sale Return" class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnsalereturn_Click" Visible="false" />
            </td>
            <td>
                <asp:Button ID="btnLV" runat="server" Text="Local Voucher" class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnLV_Click" Visible="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnRatilsale" runat="server" Text="Retail Sell" class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnRatilsale_Click" Visible="false" />
            </td>
            <td>
                <asp:Button ID="btnJournalvoucher" runat="server" Text="JV" class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnJournalvoucher_Click" Visible="false" />
            </td>
            <td>
                <asp:Button ID="btnGledger" runat="server" Text="Gledger" class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnGledger_Click" Visible="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnotherPurches" runat="server" Text="Other Purchase" class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnotherPurches_Click" Visible="false" />
            </td>
            <td>
                <asp:Button ID="btnYear" runat="server" Text="Year" class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnYear_Click" Visible="false" />
            </td>
            <td>
                <asp:Button ID="btnCarporate" runat="server" Text="Carporate Sell" class="btnHelp"
                    ValidationGroup="save" Width="120px" Height="40px" OnClick="btnCarporate_Click" Visible="false" />
            </td>
        </tr>
    </table>

</asp:Content>

