<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeJaggarySystemMaster.aspx.cs" Inherits="Sugar_Master_pgeJaggarySystemMaster" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

                if (hdnfClosePopupValue == "txtsystemcode") {
                    document.getElementById("<%=txtsystemcode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;

                    document.getElementById("<%=txtsystemcode.ClientID %>").disabled = false;
                    document.getElementById("<%=txtsystemName.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurchaseAc") {
                    document.getElementById("<%=txtPurchaseAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblPurchaseACName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtSaleAC.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSaleAC") {
                    document.getElementById("<%=txtSaleAC.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblSaleACName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtvatAC.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtvatAC") {
                    document.getElementById("<%=txtvatAC.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblVatACName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtOpeningBal.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtInsurance") {
                    document.getElementById("<%=txtInsurance.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblInsuranceRate.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%=txtInsurance.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtLevi") {
                    document.getElementById("<%=txtLevi.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblLeviRate.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%=txtLevi.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtgstratecode") {
                    document.getElementById("<%=txtgstratecode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblgstratecodename.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%=txtgstratecode.ClientID %>").focus();
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
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete data?")) {
                confirm_value.value = "Yes";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            }
            else {
                confirm_value.value = "No";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
            margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
            border-left: 0px; border-right: 0px; height: 7px;">
            <legend style="text-align: center;">
                <asp:Label ID="label1" runat="server" Text="   System Master   " Font-Names="verdana"
                    ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hdconfirm" runat="server" />
                <asp:HiddenField ID="hdnfClosePopup" runat="server" />
                <asp:HiddenField ID="hdnf" runat="server" />
                <asp:HiddenField ID="hdHelpPageCount" runat="server" />
                <center>
                    <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                        Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                        <table style="width: 80%;" align="left" cellpadding="3" cellspacing="5">
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                        Font-Size="Small" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%;">
                                    System Type:
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList ID="drpSystype" runat="server" CssClass="ddl" Width="200px" TabIndex="1"
                                        Height="25px" AutoPostBack="true" OnTextChanged="drpSystype_TextChanged">
                                        <asp:ListItem Text="Mobile Group" Value="G"></asp:ListItem>
                                        <asp:ListItem Text="Narration" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Vat" Value="V"></asp:ListItem>
                                        <asp:ListItem Text="Item" Value="I"></asp:ListItem>
                                        <asp:ListItem Text="Grade" Value="S"></asp:ListItem>
                                        <asp:ListItem Text="Hamali" Value="H"></asp:ListItem>
                                        <asp:ListItem Text="Packing" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Levi" Value="L"></asp:ListItem>
                                        <asp:ListItem Text="Insurance" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="Sale Bill" Value="B"></asp:ListItem>
                                        <asp:ListItem Text="Return Retail Bill" Value="R"></asp:ListItem>
                                        <asp:ListItem Text="Main Group" Value="O"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    System Code:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtsystemcode" runat="Server" CssClass="txt" TabIndex="2" Width="80px"
                                        Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtsystemcode_TextChanged"
                                        Height="24px"></asp:TextBox>
                                    <asp:Button ID="btntxtsystemcode" runat="server" Text="..." Width="80px" OnClick="btntxttxtsystemcode_click"
                                        CssClass="btnHelp" Height="24px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    System Name:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtsystemName" runat="Server" CssClass="txt" TabIndex="3" Width="200px"
                                        Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtsystemName_TextChanged"
                                        Height="24px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%;">
                                    Related To:
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList ID="drpRelatedTo" runat="server" CssClass="ddl" Width="200px" TabIndex="1"
                                        Height="25px" AutoPostBack="true">
                                        <asp:ListItem Text="Salect" Value="V"></asp:ListItem>
                                        <asp:ListItem Text="Purchase" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Sale" Value="S"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    System Rate:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSysRate" runat="Server" CssClass="txt" TabIndex="4" Width="102px"
                                        AutoPostBack="true" Style="text-align: right;" OnTextChanged="txtSysRate_TextChanged"
                                        Height="24px"></asp:TextBox>
                                    <ajax1:FilteredTextBoxExtender ID="filterrate" runat="server" FilterType="Numbers,Custom"
                                        ValidChars="." TargetControlID="txtSysRate">
                                    </ajax1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Purchase Account:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPurchaseAc" runat="server" CssClass="txt" Width="80px" AutoPostBack="True"
                                        OnTextChanged="txtPurchaseAc_TextChanged" TabIndex="5" Height="24px"></asp:TextBox>
                                    <asp:Button ID="btntxtPurchaseAc" runat="server" CssClass="btnHelp" Text="..." OnClick="btntxtPurchaseAc_Click"
                                        Height="24px" Width="20px" />
                                    <asp:Label ID="lblPurchaseACName" runat="server" CssClass="lblName"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Sale Account:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSaleAC" runat="server" CssClass="txt" Width="80px" AutoPostBack="True"
                                        OnTextChanged="txtSaleAC_TextChanged" TabIndex="6" Height="24px"></asp:TextBox>
                                    <asp:Button ID="btntxtSaleAC" runat="server" CssClass="btnHelp" Text="..." OnClick="btntxtSaleAC_Click"
                                        Height="24px" Width="20px" />
                                    <asp:Label ID="lblSaleACName" runat="server" CssClass="lblName"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Vat Account:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtvatAC" runat="server" CssClass="txt" Width="80px" AutoPostBack="True"
                                        OnTextChanged="txtvatAC_TextChanged" TabIndex="7" Height="24px"></asp:TextBox>
                                    <asp:Button ID="btntxtvatAC" runat="server" CssClass="btnHelp" Text="..." OnClick="btntxtvatAC_Click"
                                        Height="24px" Width="20px" />
                                    <asp:Label ID="lblVatACName" runat="server" CssClass="lblName"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Levi Account:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLevi" runat="server" CssClass="txt" Width="80px" AutoPostBack="True"
                                        OnTextChanged="txtLevi_TextChanged" TabIndex="8" Height="24px"></asp:TextBox>
                                    <asp:Button ID="btntxtLevi" runat="server" CssClass="btnHelp" Text="..." OnClick="btntxtLevi_Click"
                                        Height="24px" Width="20px" />
                                    <asp:Label ID="lblLeviRate" runat="server" CssClass="lblName"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Insurance Account:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtInsurance" runat="server" CssClass="txt" Width="80px" AutoPostBack="True"
                                        OnTextChanged="txtInsurance_TextChanged" TabIndex="9" Height="24px"></asp:TextBox>
                                    <asp:Button ID="btntxtInsurance" runat="server" CssClass="btnHelp" Text="..." OnClick="btntxtInsurance_Click"
                                        Height="24px" Width="20px" />
                                    <asp:Label ID="lblInsuranceRate" runat="server" CssClass="lblName"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Opening Balance:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOpeningBal" runat="server" CssClass="txt" Width="103px" AutoPostBack="True"
                                        OnTextChanged="txtOpeningBal_TextChanged" TabIndex="8" Height="24px"></asp:TextBox>
                                    <ajax1:FilteredTextBoxExtender ID="FilteretxtOpeningBal" runat="server" FilterType="Numbers,Custom"
                                        ValidChars="." TargetControlID="txtOpeningBal">
                                    </ajax1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    HSN / ASC :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtHsnNumber" runat="server" CssClass="txt" Width="150px" TabIndex="8"
                                        Height="24px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Weight :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtweight" runat="server" CssClass="txt" Width="150px" TabIndex="8"
                                        Height="24px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    GST Rate Code:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtgstratecode" runat="server" CssClass="txt" Width="80px" AutoPostBack="True"
                                        OnTextChanged="txtgstratecode_TextChanged" TabIndex="8" Height="24px"></asp:TextBox>
                                    <asp:Button ID="btntxtgstratecode" runat="server" CssClass="btnHelp" Text="..." OnClick="btntxtgstratecode_Click"
                                        Height="24px" Width="20px" />
                                    <asp:Label ID="lblgstratecodename" runat="server" CssClass="lblName"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%;">
                                    Category:
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList ID="drpCategory" runat="server" CssClass="ddl" Width="200px" TabIndex="1"
                                        Height="25px">
                                        <asp:ListItem Text="Select" Value="D" Selected="True"></asp:ListItem>
                                        <%--<asp:ListItem Text="Small" Value="S"></asp:ListItem>
                                        <asp:ListItem Text="Big" Value="B"></asp:ListItem>--%>
                                        <asp:ListItem Text="Box" Value="X"></asp:ListItem>
                                        <asp:ListItem Text="5kg" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="10kg" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="30kg" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="5kgBox" Value="6"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50%;">
                                    Main Group:
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList runat="server" ID="drpsalebill" Width="100px" CssClass="ddl" AutoPostBack="true"
                                        TabIndex="1" OnSelectedIndexChanged="drpsalebill_SelectedIndexChanged">
                                        <asp:ListItem Text="Select Value" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </center>
                <table width="80%" align="left">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                            &nbsp;
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                                TabIndex="9" ValidationGroup="add" OnClick="btnSave_Click" Height="24px" />
                            &nbsp;
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                                ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                            &nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                                ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                                TabIndex="10" ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                                OnClick="btnFirst_Click" Width="90px" Height="24px" />
                            <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                                OnClick="btnPrevious_Click" Width="90px" Height="24px" />
                            <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                                OnClick="btnNext_Click" Width="90px" Height="24px" />
                            <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                                OnClick="btnLast_Click" Width="90px" Height="24px" />
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
                                    Width="250px" Height="20px" AutoPostBack="true" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                                    CssClass="btnSubmit" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                    Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                    <asp:GridView ID="grdPopup" CssClass="select" runat="server" AutoGenerateColumns="true"
                                        AllowPaging="true" PageSize="20" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                        HeaderStyle-ForeColor="White" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                        OnRowCreated="grdPopup_RowCreated" Style="table-layout: fixed;" OnRowDataBound="grdPopup_RowDataBound">
                                        <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                                        <RowStyle Height="25px" ForeColor="Black" Wrap="false" />
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
    </center>
</asp:Content>

