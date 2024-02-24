<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountContacts.aspx.cs"
    Inherits="AccountContacts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="JQuery/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="JQuery/jquery.keynavigation.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            var gridview = $("#grdContacts");
            $.keynavigation(gridview);
        });

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //Specific code for partial postbacks can go in here.
                var gridview = $("#grdContacts");
                $.keynavigation(gridview);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel runat="server" ID="up1">
            <ContentTemplate>
                <asp:Panel ID="pnlContacts" runat="server" ScrollBars="Vertical" Style="max-height: 200px;
                    height: 200px; min-height: 200px; border: 1px solid black; width: 600px;">
                    <asp:GridView ID="grdContacts" runat="server" AutoGenerateColumns="false" Width="250px">
                        <Columns>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server" Text='<%#Eval("PersonName") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMobile" runat="server" Text='<%#Eval("MobileNo") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEmail" runat="server" Text='<%#Eval("EmailID") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
