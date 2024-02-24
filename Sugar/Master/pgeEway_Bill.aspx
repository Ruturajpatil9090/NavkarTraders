<%@ Page Title="Eway_Bill" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeEway_Bill.aspx.cs" Inherits="Sugar_Master_pgeEway_Bill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="E-Way Bill" Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <br />
    <br />
    <asp:Panel runat="server" ID="pnlMain">
        <table width="60%" align="center" cellspacing="5">
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Client Id:</b>
                </td>
                <td align="left" style="width: 30%;">
                    <asp:TextBox runat="server" ID="txtclientid" Width="150px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Client Secret Key:</b>
                </td>
                <td align="left" style="width: 30%;">
                    <asp:TextBox runat="server" ID="txtclientsecretkey" Width="150px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Token URL:</b>
                </td>
                <td align="left" style="width: 30%;">
                    <asp:TextBox runat="server" ID="txttokenurl" Width="250px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>GENEwayBill URL:</b>
                </td>
                <td align="left" style="width: 30%;">
                    <asp:TextBox runat="server" ID="txtgenewaybillurl" Width="250px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>GENE_Envoice URL:</b>
                </td>
                <td align="left" style="width: 30%;">
                    <asp:TextBox runat="server" ID="txte_envoice" Width="250px" Height="24px"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td align="right" style="width: 30%;">
                    <b>Cancle_Envoice URL:</b>
                </td>
                <td align="left" style="width: 30%;">
                    <asp:TextBox runat="server" ID="txtCancle_Envoice" Width="250px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>User Name:</b>
                </td>
                <td align="left" style="width: 30%;">
                    <asp:TextBox runat="server" ID="txtusername" Width="150px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Password:</b>
                </td>
                <td align="left" style="width: 30%;">
                    <asp:TextBox runat="server" ID="txtpassword" Width="150px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Company GSTIN:</b>
                </td>
                <td align="left" style="width: 40%;">
                    <asp:TextBox runat="server" ID="txtgstin" Width="150px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>SMS API:</b>
                </td>
                <td align="left" style="width: 40%;">
                    <asp:TextBox runat="server" ID="txtsmsApi" Width="250px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>SMS Sender Id:</b>
                </td>
                <td align="left" style="width: 40%;">
                    <asp:TextBox runat="server" ID="txtSenderid" Width="150px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Accusage:</b>
                </td>
                <td align="left" style="width: 40%;">
                    <asp:TextBox runat="server" ID="txtAccusage" Width="150px" Height="24px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Mode of Payment</b>
                </td>
                <td align="left" style="width: 40%;">
                    <asp:TextBox Height="24px" ID="txtMode_of_Payment" runat="Server"
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Account Details</b>
                </td>
                <td align="left" style="width: 40%;">
                    <asp:TextBox Height="24px" ID="txtAccount_Details" runat="Server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 30%;">
                    <b>Branch</b>
                </td>
                <td align="left" style="width: 40%;">
                    <asp:TextBox Height="24px" ID="txtBranch" runat="Server"
                        Width="200px" Style="text-align: left;"></asp:TextBox>
                </td>
            </tr>


            <tr>
                <td align="center" colspan="2">
                    <asp:Button runat="server" ID="btnUpdate" Text="Update" CssClass="btnHelp" Width="100px" OnClick="btnUpdate_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

