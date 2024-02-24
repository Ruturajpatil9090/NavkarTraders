<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pgeCustStartup.aspx.cs" Inherits="Customer_Page_pgeCustStartup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
    </div>
    </form>
</body>
</html>
