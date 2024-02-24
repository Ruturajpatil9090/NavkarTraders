<%@ Page Title="Select Company" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeSelectCompany.aspx.cs" Inherits="Sugar_pgeSelectCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="50%" align="center" style="margin: 0 auto; margin-top: 100px;">
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
</asp:Content>
