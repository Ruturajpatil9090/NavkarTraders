<%@ Page Title="Rawanagi Book" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeRawanagiBook.aspx.cs" Inherits="Sugar_Inword_pgeRawanagiBook" %>

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
    <script type="text/javascript" src="../JS/DateValidation.js"> </script>

    <script src="../../JS/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../Scripts/selectfirstrow.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;
        function SelectSibling(e) {
            debugger;
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

                if (hdnfClosePopupValue == "txtMILL_CODE") {
                    document.getElementById("<%=txtMILL_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=LBLMILL_NAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtMILL_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtParty_Code") {
                    document.getElementById("<%=txtParty_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblParty_Name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtParty_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTransport_code") {
                    document.getElementById("<%=txtTransport_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtTrannsport_Name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtTransport_code.ClientID %>").focus();
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
        function mill(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtMILL_CODE.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtMILL_CODE.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtMILL_CODE.ClientID %>").val(unit);
                __doPostBack("txtMILL_CODE", "TextChanged");

            }

        }
        function party(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtParty_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtParty_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtParty_Code.ClientID %>").val(unit);
                __doPostBack("txtParty_Code", "TextChanged");

            }

        }
        function transport(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtTransport.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtTransport_code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtTransport_code.ClientID %>").val(unit);
                __doPostBack("txtTransport_code", "TextChanged");

            }

        }
    </script>

    <script type="text/javascript">
        function Back() {

            //alert(td);
            window.open('../Inword/PgeRawanagiBook_Utility.aspx', '_self');
        }

    </script>
    <script type="text/javascript" language="javascript">
        debugger;
        document.addEventListener('keyup', function (event) {
            if (event.defaultPrevented) {
                return;
            }

            var key = event.key || event.keyCode;

            if (key === 'Escape' || key === 'Esc' || key === 27) {
                //                doWhateverYouWantNowThatYourKeyWasHit();
                debugger;
                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                if (hdnfClosePopupValue == "txtMILL_CODE") {
                    document.getElementById("<%=txtMILL_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtParty_Code") {
                    document.getElementById("<%=txtParty_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTransport_code") {
                    document.getElementById("<%=txtTransport_code.ClientID %>").focus();
                }
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
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
    <%-- <script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfRef_No" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />

            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            TabIndex="34" Height="24px" ValidationGroup="save" OnClick="btnAdd_Click" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            TabIndex="35" Height="24px" ValidationGroup="add" OnClick="btnSave_Click" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            TabIndex="36" Height="24px" ValidationGroup="save" OnClick="btnEdit_Click" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            TabIndex="37" Height="24px" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            TabIndex="38" Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" />

                        &nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                            TabIndex="38" Height="24px" ValidationGroup="save" OnClientClick="Back()" />

                        &nbsp;
                            Date:
                            <asp:TextBox ID="txtDOC_DATE" runat="Server" CssClass="txt" TabIndex="1" Width="100px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDOC_DATE_TextChanged"
                                Height="24px"></asp:TextBox>
                        <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                            Width="25px" Height="15px" />
                        <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtDOC_DATE"
                            PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                        </ajax1:CalendarExtender>

                        Narration:
                            
                                <asp:TextBox ID="txtNARRATION" runat="Server" CssClass="txt" TabIndex="2" Width="350px"
                                    TextMode="MultiLine" Height="30px" Style="text-align: left;" AutoPostBack="false"
                                    OnTextChanged="txtNARRATION_TextChanged"></asp:TextBox>

                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                            Font-Size="Small" ForeColor="Red"></asp:Label>
                    </td>

                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">

                <fieldset style="border-top: 3px dotted rgb(131, 127, 130); border-radius: 3px; width: 172%; margin-left: -1160px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 5px;">
                    <legend style="text-align: left;">
                        <h2 style="color: Black; text-align: left;" font-names="verdana" font-size="large">Detail Section</h2>
                    </legend>
                </fieldset>
                <table style="width: 100%;" align="Left" cellpadding="4" cellspacing="4">
                    <tr align="left">
                        <td align="left">

                            <asp:Label ID="lblID" runat="server"></asp:Label>
                            <asp:Label ID="lblNo" runat="server" ForeColor="Azure"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left">Mill:
                           
                                <asp:TextBox ID="txtMILL_CODE" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtMILL_CODE_TextChanged" onkeydown="mill(event);"></asp:TextBox>
                            <asp:Button ID="btntxtMILL_CODE" runat="server" Text="..." OnClick="btntxtMILL_CODE_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="LBLMILL_NAME" runat="server" CssClass="lblName"></asp:Label>

                            Station City:
                                     <asp:TextBox ID="txtStationCity" runat="Server" CssClass="txt" TabIndex="4" Width="200px"
                                         Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtStationCity_TextChanged"></asp:TextBox>

                            Qty:
                                     <asp:TextBox ID="txtQty" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                         Height="24px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtQty" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtQty">
                            </ajax1:FilteredTextBoxExtender>

                            Dispatch:
                                     <asp:TextBox ID="txtDispatch" runat="Server" CssClass="txt" TabIndex="6" Width="80px"
                                         Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtDispatch_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtDispatch" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtDispatch">
                            </ajax1:FilteredTextBoxExtender>

                            Balance:
                                    <asp:TextBox ID="txtBalance" runat="Server" CssClass="txt" TabIndex="7" Width="80px"
                                        Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtBalance_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtBalance" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtBalance">
                            </ajax1:FilteredTextBoxExtender>

                            Appx Freight:
                                    <asp:TextBox ID="txtAppxFreight" runat="Server" CssClass="txt" TabIndex="8" Width="80px"
                                        Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtAppxFreight_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtAppxFreight" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtAppxFreight">
                            </ajax1:FilteredTextBoxExtender>

                            Party:

                                     <asp:TextBox ID="txtParty_Code" runat="Server" CssClass="txt" TabIndex="9" Width="80px"
                                         Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtParty_Code_TextChanged" onkeydown="party(event);"></asp:TextBox>
                            <asp:Button ID="btntxtParty_Code" runat="server" Text="..." OnClick="btntxtParty_Code_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblParty_Name" runat="server" CssClass="lblName"></asp:Label>

                            <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                OnClick="btnAdddetails_Click" TabIndex="10" Height="24px" />
                            <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp"
                                Width="80px" OnClick="btnClosedetails_Click" TabIndex="11" Height="24px" />
                        </td>

                    </tr>

                </table>

                <div style="width: 100%; position: relative;">
                    <asp:UpdatePanel ID="upGrid" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlgrdDetail" runat="server" align="left" ScrollBars="Both" Height="290px"
                                Width="1700px" BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true"
                                Font-Names="Verdana" Font-Size="11px" BackColor="SeaShell" Style="margin-left: 10px; float: left;">
                                <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" HeaderStyle-BackColor="#397CBB"
                                    HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="100%"
                                    OnRowCommand="grdDetail_RowCommand" CellPadding="5" CellSpacing="5" OnRowDataBound="grdDetail_RowDataBound"
                                    Style="table-layout: fixed; float: left">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkShow" runat="server" CommandName="ShowRecord" Text="Show"
                                                    CommandArgument="lnk"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" Text="Edit"
                                                    CommandArgument="lnk"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord" Text="Delete"
                                                    CommandArgument="lnk"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Height="25px" Wrap="false" ForeColor="Black" />
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <table width="100%" align="Left">
                    <tr align="left">
                        <td align="left">

                            <asp:Label ID="lblID_Grd" runat="server"></asp:Label>
                            <asp:Label ID="lblNo_Grd" runat="server" ForeColor="Azure"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left">
                        <td>Mill:
                                <asp:TextBox ID="txtmill" runat="Server" CssClass="txt" TabIndex="12" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtmill_TextChanged"></asp:TextBox>
                            <asp:Label ID="lblmill" runat="server" CssClass="lblName"></asp:Label>

                            To Station:
                                <asp:TextBox ID="txtStation" runat="Server" CssClass="txt" TabIndex="13" Width="150px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="false"></asp:TextBox>

                            Truck No:
                                <asp:TextBox ID="txtTruck_No" runat="Server" CssClass="txt" TabIndex="14" Width="100px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="false"></asp:TextBox>
                            Qty:
                                     <asp:TextBox ID="txtD_Qty" runat="Server" CssClass="txt" TabIndex="15" Width="80px"
                                         Height="24px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtD_Qty_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtD_Qty" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtD_Qty">
                            </ajax1:FilteredTextBoxExtender>

                            Frt Qty:
                                     <asp:TextBox ID="txtFrt_Qty" runat="Server" CssClass="txt" TabIndex="16" Width="80px"
                                         Height="24px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtFrt_Qty_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtFrt_Qty" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtFrt_Qty">
                            </ajax1:FilteredTextBoxExtender>

                            Net Frt:
                                     <asp:TextBox ID="txtNet_frt" runat="Server" CssClass="txt" TabIndex="17" Width="80px"
                                         Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtNet_frt_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtNet_frt" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtNet_frt">
                            </ajax1:FilteredTextBoxExtender>

                            Transport:
                                     <asp:TextBox ID="txtTransport_code" runat="Server" CssClass="txt" TabIndex="18" Width="80px"
                                         Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtTransport_code_TextChanged" onkeydown="transport(event);"></asp:TextBox>
                            <asp:Button ID="btntxtTransport" runat="server" Text="..." OnClick="btntxtTransport_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:TextBox ID="txtTrannsport_Name" runat="Server" CssClass="txt" TabIndex="19" Width="200px"
                                Height="24px" Style="text-align: right;" AutoPostBack="false"></asp:TextBox>

                            Vasuli:
                                    <asp:TextBox ID="txtVasuli" runat="Server" CssClass="txt" TabIndex="20" Width="80px"
                                        Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtVasuli_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredtxtVasuli" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtVasuli">
                            </ajax1:FilteredTextBoxExtender>

                            Remark:
                                     <asp:TextBox ID="txtRemark" runat="Server" CssClass="txt" TabIndex="21" Width="200px"
                                         TextMode="MultiLine" Height="30px" Style="text-align: right;" AutoPostBack="false"></asp:TextBox>

                            <asp:Button ID="btnAdddetail_Grd" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                OnClick="btnAdddetail_Grd_Click" TabIndex="22" Height="24px" />
                            <asp:Button ID="btnclosedetail_Grd" runat="server" Text="Close" CssClass="btnHelp"
                                Width="80px" OnClick="btnclosedetail_Grd_Click" TabIndex="23" Height="24px" />
                        </td>
                    </tr>

                </table>
                <div style="width: 100%; position: relative;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlgrddetail_Grd" runat="server" align="left" ScrollBars="Both" Height="290px"
                                Width="1700px" BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true"
                                Font-Names="Verdana" Font-Size="11px" BackColor="SeaShell" Style="margin-left: 10px; float: left;">
                                <asp:GridView ID="grdDetail_Grd" runat="server" AutoGenerateColumns="true" HeaderStyle-BackColor="#397CBB"
                                    HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="100%"
                                    OnRowCommand="grdDetail_Grd_RowCommand" CellPadding="5" CellSpacing="5" OnRowDataBound="grdDetail_Grd_RowDataBound"
                                    Style="table-layout: fixed; float: left">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" Text="Edit"
                                                    CommandArgument="lnk"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord" Text="Delete"
                                                    CommandArgument="lnk"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Height="25px" Wrap="false" ForeColor="Black" />
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </asp:Panel>


            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg"
                    Width="20px" Height="20px" Style="float: right; vertical-align: top;" OnClick="imgBtnClose_Click"
                    ToolTip="Close" />
                <table width="70%">
                    <tr>
                        <td align="center" style="background-color: #F5B540; width: 100%;">
                            <asp:Label ID="lblPopupHead" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Font-Bold="true" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Search Text:
                            <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                                Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="1250px" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    AllowPaging="true" PageSize="20" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                    OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
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

