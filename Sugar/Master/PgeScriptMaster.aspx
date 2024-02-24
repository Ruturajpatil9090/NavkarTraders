<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PgeScriptMaster.aspx.cs"
    Inherits="Sugar_Master_PgeScriptMaster" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Script/jquery-1.4.2.js"></script>

    <link href="../../CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../../JQuery/jquery-1.4.1.js" type="text/javascript"></script>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../../JQuery/jquery.keynavigation.js" type="text/javascript"></script>
    <link type="text/css" href="../../menu/menu.css" rel="stylesheet" />
    <script type="text/javascript" src="../../menu/jquery.js"></script>
    <script type="text/javascript" src="../../menu/menu.js"></script>


    <script src="../../JS/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../Scripts/selectfirstrow.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        document.addEventListener('keyup', function (event) {
            if (event.defaultPrevented) {
                return;
            }

            var key = event.key || event.keyCode;

            if (key === 'Escape' || key === 'Esc' || key === 27) {
                //                doWhateverYouWantNowThatYourKeyWasHit();

                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                if (hdnfClosePopupValue == "txtScript_Code") {
                    document.getElementById("<%=txtScript_Code.ClientID %>").focus();
                }

                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
    </script>

    <script type="text/javascript" language="javascript">
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
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").disabled = false;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtScript_Code") {
                    document.getElementById("<%= txtScript_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblScript_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtScript_Code.ClientID %>").disabled = false;
                    document.getElementById("<%=txtScript_Code.ClientID %>").focus();
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
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <script type="text/javascript">
        function chanegno(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtEditDoc_No";
                ("#<%=hdnfClosePopup.ClientID %> ").val(edi);
                ("#<%=btnSearch.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                _doPostBack("txtEditDoc_No", "TextChanged");
            }
        }

        function Focusbtn(e) {
            debugger;
            if (e.keyCode == 13) {

                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();

            }
        }

        function Script_Code(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtScript_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtScript_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtScript_Code.ClientID %>").val(unit);
                __doPostBack("txtScript_Code", "TextChanged");

            }

        }

    </script>
    <script type="text/javascript">
        function Back() {
            var Action = 2;
            var Group_Code = 0;
            //alert(td);
            window.open('../Master/PgeScriptMasterUtility.aspx');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 100%; margin-left: 10px; float: left; border-bottom: 0px; padding-top: 10px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 10px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Equity Script Master" Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <table width="80%" align="center">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" TabIndex="8" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" TabIndex="9" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" TabIndex="10" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px"
                            TabIndex="11" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" TabIndex="12" />
                        &nbsp;
                         <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                             TabIndex="14" ValidationGroup="save" Height="24px" OnClientClick="Back()" />
                    </td>
                    <td align="center">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnFirst_Click" Width="90px" Height="24px" TabIndex="13" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="Previous" CssClass="btnHelp"
                            OnClick="btnPrevious_Click" Width="90px" Height="24px" TabIndex="14" />
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="Next" CssClass="btnHelp"
                            OnClick="btnNext_Click" Width="90px" Height="24px" TabIndex="15" />
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="Last" CssClass="btnHelp"
                            OnClick="btnLast_Click" Width="90px" Height="24px" TabIndex="16" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 60%;" align="center" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                        <tr>
                            <td align="right" style="width: 10%;">Change No
                            </td>
                            <td>
                                <asp:TextBox Height="24px" ID="txtEditDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                    Width="90px" Style="text-align: left;" AutoPostBack="true" onKeyDown="changeno(event);"
                                    OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Script Code
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtScript_Code" runat="Server" CssClass="txt" TabIndex="2"
                                    Width="90px" Style="text-align: left;" OnkeyDown="Script_Code(event);" AutoPostBack="false"
                                    OnTextChanged="txtScript_Code_TextChanged"></asp:TextBox>
                                <asp:Button Width="80px" Height="24px" ID="btntxtScript_Code" runat="server" Text="..."
                                    OnClick="btntxtScript_Code_Click" CssClass="btnHelp" />
                                <asp:Label ID="lblScript_Code" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Script Name
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtScript_Name" runat="Server" CssClass="txt" TabIndex="3"
                                    Width="350px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtScript_Name_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Script Type
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:DropDownList ID="drpScript_Type" runat="Server" CssClass="txt" TabIndex="4" Width="92px"
                                    Height="24px" AutoPostBack="false" OnSelectedIndexChanged="drpScript_Type_SelectedIndexChanged">
                                    <asp:ListItem Text="Equity" Value="E" />
                                    <asp:ListItem Text="FNO" Value="F" />
                                    <asp:ListItem Text="Commodity" Value="C" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Lot Size
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtLot_Size" runat="Server" CssClass="txt" TabIndex="5"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtLot_Size_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Face Value
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtFace_Value" runat="Server" CssClass="txt" TabIndex="6"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtFace_Value_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtFace_Value" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtFace_Value">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Script Id
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtScript_Id" runat="Server" CssClass="txt" TabIndex="7"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" onKeyDown="Focusbtn(event);"
                                    OnTextChanged="txtScript_Id_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="filtertxtScript_Id" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtScript_Id">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                </table>
            </asp:Panel>
            <asp:Panel onkeydown="closepopup(event);" ID="pnlPopup" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
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
                        <td>Search Text:
                            <asp:TextBox onkeydown="SelectFirstRow(event);" ID="txtSearchText" runat="server"
                                Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button onkeydown="closepopup(event);" ID="btnSearch" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView Font-Bold="true" CssClass="select" AllowPaging="true" PageSize="20"
                                    OnPageIndexChanging="grdPopup_PageIndexChanging" ID="grdPopup" runat="server"
                                    AutoGenerateColumns="true" Width="100%" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound"
                                    Style="table-layout: fixed;">
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
</asp:Content>

