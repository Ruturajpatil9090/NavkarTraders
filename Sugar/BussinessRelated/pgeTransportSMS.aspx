<%@ Page Title="Transport SMS" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeTransportSMS.aspx.cs" Inherits="Sugar_pgeTransportSMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../JS/select all.js"></script>
    <script type="text/javascript">
        function checkAll(invoker) {
            var gridView = document.getElementById("<%=grdAccounts.ClientID %>");
            var checkBox = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBox.length; i++) {
                var myElement = checkBox[i];
                if (myElement.type == "checkbox") {
                    myElement.checked = invoker.checked;
                }
            }
        }

        function validate() {
            var gridView = document.getElementById("<%=grdAccounts.ClientID %>");
            var checkBox = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBox.length; i++) {
                if (checkBox[i].type == "checkbox" && checkBox[i].checked) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

    </script>
    <script type="text/javascript" language="javascript">

        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;
        function SelectSibling(e) {
            var e = e ? e : window.event;
            var KeyCode = e.which ? e.which : e.keyCode;
            if (KeyCode == 40) {
                SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);
            }
            else if (KeyCode == 38) {
                SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);
            }
            else if (KeyCode == 13) {
                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                var grid = document.getElementById("<%= grdPopup.ClientID %>");
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";

                var pageCount = document.getElementById("<%= hdHelpPageCount.ClientID %>").value;
                pageCount = parseInt(pageCount);
                if (pageCount > 1) {
                    SelectedRowIndex = SelectedRowIndex + 1;
                }
                if (hdnfClosePopupValue == "txtSmsGroup") {
                    document.getElementById("<%=txtSmsGroup.ClientID %>").disabled = false;
                    document.getElementById("<%=txtSmsGroup.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtSmsGroup.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtCityCode") {
                    document.getElementById("<%=txtCityCode.ClientID %>").disabled = false;
                    document.getElementById("<%=txtCityCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtCityCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtAc_Code") {
                    document.getElementById("<%=txtAc_Code.ClientID %>").disabled = false;
                    document.getElementById("<%=txtAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtAc_Code.ClientID %>").focus();
                }

            }
        }

        function SelectRow(CurrentRow, RowIndex) {
            UpperBound = parseInt('<%= this.grdPopup.Rows.Count %>') - 1;
            LowerBound = 0;
            if (SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound)

                if (SelectedRow != null) {
                    SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
                    SelectedRow.style.color = SelectedRow.originalForeColor;
                }
            if (CurrentRow != null) {
                CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
                CurrentRow.originalForeColor = CurrentRow.style.color;
                CurrentRow.style.backgroundColor = '#DCFC5C';
                CurrentRow.style.color = 'Black';
            }
            SelectedRow = CurrentRow;
            SelectedRowIndex = RowIndex;
            setTimeout("SelectedRow.focus();", 0);
        }
    </script>
    <script type="text/javascript">
        function SMS(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtSmsGroup.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtSmsGroup.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtSmsGroup.ClientID %>").val(unit);
                __doPostBack("txtSmsGroup", "TextChanged");

            }

        }
        function CityCode(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtCityCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtCityCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtCityCode.ClientID %>").val(unit);
                __doPostBack("txtCityCode", "TextChanged");

            }

        }
        function Ac_Code(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtAc_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtAc_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtAc_Code.ClientID %>").val(unit);
                __doPostBack("txtAc_Code", "TextChanged");

            }

        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Transport SMS   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <br />
    <br />
    <asp:UpdatePanel runat="server" ID="updatepnlMain" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <table width="90%" align="center" cellpadding="2">
                <tr>
                    <td align="left" style="width: 100%">
                        <asp:Button runat="server" ID="btnGetTransport" Text="Refresh" CssClass="btnHelp"
                            Width="90px" Height="25px" OnClick="btnGetTransport_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" align="left">
                        <asp:Panel ID="pnlGrid" runat="server" align="left" BorderColor="Blue" BorderWidth="1px"
                            Height="250px" ScrollBars="Both" Style="text-align: left; background-color: White;"
                            Width="900px">
                            <asp:GridView runat="server" ID="grdDetails" AutoGenerateColumns="false" CellPadding="6"
                                Font-Bold="true" ForeColor="Black" GridLines="Both" HeaderStyle-BackColor="#397CBB"
                                HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" RowStyle-Height="25px"
                                Style="table-layout: fixed;" Width="80%" OnRowDataBound="grdDetails_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtmillname" Text='<%#Eval("millname") %>' Width="100%"
                                                Height="100%" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtPartyCity" Text='<%#Eval("CityName") %>' Width="100%"
                                                Height="100%" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Sugar Balance" DataField="balance" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="chkAll" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" /></HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="grdCB" AutoPostBack="true" OnCheckedChanged="grdCB_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" align="left">
                        <asp:TextBox runat="server" ID="txtMessage" TextMode="MultiLine" Width="900px" Height="100px"
                            Enabled="true" Style="background-color: White;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 60%" align="left">
                        <asp:Label runat="server" ID="lblSmsgroup" Text="SMS GROUP:" Font-Bold="true" ForeColor="Black"> </asp:Label>&nbsp;
                        <asp:TextBox runat="server" ID="txtSmsGroup" Width="80px" Height="24px" CssClass="txt"
                            AutoPostBack="true" OnTextChanged="txtSmsGroup_TextChanged" onKeyDown="SMS(event);"></asp:TextBox>&nbsp;<asp:Button
                                runat="server" ID="btntxtSmsGroup" CssClass="btnHelp" Text=". . ." Width="20px"
                                Height="24px" OnClick="btntxtSmsGroup_Click" />&nbsp;<asp:Label runat="server" ID="lblGroupName"
                                    CssClass="lblName"></asp:Label>&nbsp; &nbsp;
                        <asp:Label runat="server" ID="Label2" Text="City:" Font-Bold="true" ForeColor="Black"> </asp:Label>&nbsp;
                        <asp:TextBox runat="server" ID="txtCityCode" Width="80px" Height="24px" CssClass="txt"
                            AutoPostBack="true" OnTextChanged="txtCityCode_TextChanged" onKeyDown="CityCode(event);"></asp:TextBox>&nbsp;<asp:Button
                                runat="server" ID="btntxtCityCode" CssClass="btnHelp" Text=". . ." Width="20px"
                                Height="24px" OnClick="btntxtCityCode_Click" />&nbsp;<asp:Label runat="server" ID="lblCityName"
                                    CssClass="lblName"></asp:Label>
                        &nbsp;&nbsp;<b>Ac_Code:</b>&nbsp;
                        <asp:TextBox AutoPostBack="true" ID="txtAc_Code"
                            Width="80px" Height="24px" CssClass="txt" runat="server" OnTextChanged="txtAc_Code_TextChanged" onKeyDown="Ac_Code(event);"></asp:TextBox>&nbsp;<asp:Button
                                runat="server" ID="btntxtAc_Code" Text="..." CssClass="btnHelp" Width="20px"
                                Height="24px" OnClick="btntxtAc_Code_Click" />&nbsp;<asp:Label runat="server" ID="lblAc_Name"
                                    CssClass="lblName"></asp:Label>&nbsp;<asp:Button runat="server" ID="btnAddNames"
                                        Text="Add" CssClass="btnHelp" Width="70px" Height="24px" OnClick="btnAddNames_Click" />&nbsp;<asp:Button
                                            runat="server" ID="btnSendSms" Text="SEND" CssClass="btnHelp" Width="100px" Height="24px"
                                            OnClick="btnSendSms_Click" OnClientClick="Validate();" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60%;">
                        <asp:Panel runat="server" ID="pnlNames" Width="900px" Height="180px" Style="border: 1px solid blue;
                            background-color: White;" DefaultButton="btnClick" ScrollBars="Vertical">
                            <asp:GridView runat="server" ID="grdAccounts" AutoGenerateColumns="false" HeaderStyle-HeaderStyle-Height="30px"
                                HeaderStyle-BorderColor="White" GridLines="Both" RowStyle-Height="24px" CellPadding="6"
                                RowStyle-ForeColor="Black" Font-Bold="true" HeaderStyle-BackColor="Blue" HeaderStyle-ForeColor="White"
                                OnRowDataBound="grdAccounts_RowDataBound">
                                <Columns>
                                    <asp:BoundField HeaderText="Ac_Code" DataField="Ac_Code" />
                                    <asp:BoundField HeaderText="Name" DataField="Ac_Name" />
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtMobile" Width="150px" Height="24px" Text='<%#Eval("Mobile") %>'
                                                CssClass="txt"></asp:TextBox>
                                            <%-- <ajax1:FilteredTextBoxExtender runat="server" ID="tfsd" TargetControlID="txtMobile">
                                            </ajax1:FilteredTextBoxExtender>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="chkCheckAll" OnClick="checkAll(this);" AutoPostBack="true" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkCheck" Checked='<%#Eval("IsChecked") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Button runat="server" ID="btnClick" Style="display: none;" OnClick="btnClick_Click" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px;
                min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center;
                left: 10%; top: 10%;">
                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg"
                    Width="20px" Height="20px" Style="float: right; vertical-align: top;" OnClick="imgBtnClose_Click"
                    ToolTip="Close" />
                <table width="95%">
                    <tr>
                        <td align="center" style="background-color: #F5B540; width: 100%;">
                            <asp:Label ID="lblPopupHead" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Font-Bold="true" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Search Text:
                            <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                                Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    AllowPaging="true" PageSize="30" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                    OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
                                    <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                                    <RowStyle Height="25px" Width="100px" ForeColor="Black" Wrap="true" />
                                    <PagerStyle BackColor="Tomato" ForeColor="White" Width="100%" Font-Bold="true" />
                                    <PagerSettings Position="TopAndBottom" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
