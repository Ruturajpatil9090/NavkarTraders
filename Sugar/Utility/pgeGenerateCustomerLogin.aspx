<%@ Page Title="Generate Customer Login" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeGenerateCustomerLogin.aspx.cs" Inherits="Sugar_Utility_pgeGenerateCustomerLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style type="text/css">
         #grdGeneratedAccounts tr:nth-child(even) {
                background-color: #ffffff;
            }

            #grdGeneratedAccounts tr:nth-child(odd) {
                background-color: lightblue;
            }
        </style>
    <link href="../../CSS/Grid.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function clearWaterMark(defaultText, textBox) {
            if (textBox.value == defaultText) {
                textBox.value = "";
                textBox.style.color = "Black";
            }
        }
        function createWaterMark(defaultText, textBox) {
            if (textBox.value.length == 0) {
                textBox.value = defaultText;
                textBox.style.color = "Gray";
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function validateCheckBoxes() {
            var isValid = false;
            var gridView = document.getElementById('<%= grdAccounts.ClientID %>');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;
                            return true;
                        }
                    }
                }
            }
            alert("Please select atleast one Admin!");
            return false;
        }
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 5px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Create Login For Customer   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <br />
    <br />
    <br />
    <b>Search Account:</b><asp:TextBox runat="server" ID="txtSearch" Width="250px" ForeColor="Gray"
        Height="24px"></asp:TextBox>&nbsp;
    <asp:Button runat="server" ID="btnSearch" onkeydown="SelectFirstRow(event);" Text="Search"
        Height="24px" Width="80px" OnClick="btnSearch_Click" />
    <br />
    <br />
    <asp:Panel ID="pnlAccounts" runat="server" Font-Names="verdana" Font-Bold="true"
        Width="900px" BorderColor="Blue" BorderWidth="1px" ForeColor="Black" Font-Size="Small"
        Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
        <br />
        <asp:Label runat="server" ID="Label2" Text="NOT GENERATED ACCOUNTS" Font-Bold="true"
            ForeColor="Red"></asp:Label>
        <asp:GridView ID="grdAccounts" runat="server" AutoGenerateColumns="false" AllowPaging="true"
            EmptyDataText="Nothing Account Has generated" PageSize="10" CssClass="Grid" AlternatingRowStyle-CssClass="alt"
            PagerStyle-CssClass="pgr" Width="800px" OnPageIndexChanging="grdAccounts_PageIndexChanging"
            OnRowDataBound="grdAccounts_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="Account No" DataField="Ac_Code" />
                <asp:BoundField HeaderText="Name" DataField="Ac_Name_E" />
                <asp:BoundField HeaderText="Email" DataField="Email_Id" />
                <asp:BoundField HeaderText="Mobile" DataField="Mobile_No" />
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="grdAccountsCB" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlGeneratedAccounts" runat="server" Font-Names="verdana" Font-Bold="true"
        ForeColor="Black" Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;"
        Width="700px" BorderColor="Blue" BorderWidth="1px">
        <br />
        <asp:Label runat="server" ID="lblgeneratedAccount" Text="GENERATED ACCOUNTS" Font-Bold="true"
            ForeColor="Red"></asp:Label>
        <asp:GridView ID="grdGeneratedAccounts" runat="server" AutoGenerateColumns="false"
            AllowPaging="true" PageSize="10" CssClass="Grid" AlternatingRowStyle-CssClass="alt"
            EmptyDataText="Nothing Account Has generated" PagerStyle-CssClass="pgr" Width="600px">
            <Columns>
                <asp:BoundField HeaderText="Account No" DataField="Ac_Code" />
                <asp:BoundField HeaderText="Name" DataField="Ac_Name_E" />
                <asp:BoundField HeaderText="Email" DataField="Email_Id" />
                <asp:BoundField HeaderText="Mobile" DataField="Mobile_No" />
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="grdAccountsCB" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Button runat="server" ID="btnGenerate" OnClick="btnGenerate_Click" CssClass="btnHelp"
        OnClientClick="javascript:validateCheckBoxes();" Width="100px" Height="25px" Text="Create Login" />&nbsp;<asp:Button
            runat="server" ID="btnShowGeneratedAccounts" Text="Show Generated" Width="120px"
            CssClass="btnHelp" Height="25px" OnClick="btnShowGeneratedAccounts_Click" />&nbsp;<asp:Button
                Width="50px" runat="server" ID="btnCancel" Height="25px" Text="Cancel" CssClass="btnHelp"
                OnClick="btnCancel_Click" />
    <br />
</asp:Content>

