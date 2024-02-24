<%@ Page Title="Brand Master" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeBrand_Master.aspx.cs" Inherits="pgeBrand_Master" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="../../CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery-1.4.2.js"></script>
    <link href="../../CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../../JQuery/jquery-1.4.1.js" type="text/javascript"></script>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../../JQuery/jquery.keynavigation.js" type="text/javascript"></script>
    <link type="text/css" href="../../menu/menu.css" rel="stylesheet" />
    <script type="text/javascript" src="../../menu/jquery.js"></script>
    <script type="text/javascript" src="../../menu/menu.js"></script>
    <script type="text/javascript" src="../../JS/DateValidation.js"> </script>
    <script src="../../JS/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../Scripts/selectfirstrow.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        window.onbeforeunload = function (event) {
            var message = 'All changes will get lost!';
            if (typeof event == 'undefined') {
                event = window.event;
            }
            if (event) {
                event.returnValue = message;
            }
            return message;
        }
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
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = "";
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtCode") {
                    document.getElementById("<%= txtCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblcode.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtMal_Code") {
                    document.getElementById("<%= txtMal_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblmal_code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtMal_Code.ClientID %>").focus();
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
    <script type="text/javascript" language="javascript">
        function Focusbtn(e) {
            debugger;

            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }

        }
        function changeno(e) {
            debugger;
            if (e.keyCode == 112) {

                e.preventDefault();

                var edi = "txtEditDoc_No"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtEditDoc_No", "TextChanged");

            }
        }
        function Code(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                ("#<%=btntxtCode.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                _doPostBack("txtCode", "TextChanged");
            }
        }
        function Mal_Code(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtMal_Code.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var GSTRate = $("#<%=txtMal_Code.ClientID %>").val();

                GSTRate = "0" + GSTRate;
                $("#<%=txtMal_Code.ClientID %>").val(GSTRate);

                __doPostBack("txtMal_Code", "TextChanged");
            }
        }
        function Focusbtn(e) {
            debugger;
            if (e.keyCode == 13) {

                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();

            }
        }
        function Redirect(retValue, returnmaxno) {
            if (retValue == "-1") {
                alert('Sucessfully Added Record !!! Doc_no=' + returnmaxno)
            }
            else {
                alert('Sucessfully Updated Record !!! Doc_no=' + returnmaxno)
            }

            window.open('../Master/pgeBrand_Master.aspx?BrandCode=' + returnmaxno + '&Action=1', "_self");
        }
    </script>
    <script type="text/javascript">
        function disableClick(elem) {
            elem.disabled = true;
        }
    </script>
     <script type="text/javascript">
         function Back() {
          
             window.open('../Master/pgeBrandMaster_Utility.aspx');
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Marka Master " Font-Names="verdana" ForeColor="White"
                Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            TabIndex="10" ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                        &nbsp;
                        <%-- <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" />--%>
                        <asp:Button OnClientClick="disableClick(this)" OnClick="btnSave_Click" runat="server"
                            TabIndex="11" Text="Save" UseSubmitBehavior="false" ID="btnSave" CssClass="btnHelp"
                            ValidationGroup="add" Width="90px" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            TabIndex="12" ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            TabIndex="13" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()"
                            Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            TabIndex="14" ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" />
                         &nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                            TabIndex="14" ValidationGroup="save" Height="24px" OnClientClick="Back()" />
                    </td>
                    <td align="left">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnFirst_Click" Width="90px" Height="24px"  TabIndex="14" Visible="false"/>
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnPrevious_Click" Width="90px" Height="24px"  TabIndex="15"  Visible="false"/>
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnNext_Click" Width="90px" Height="24px"  TabIndex="16"  Visible="false"/>
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnLast_Click" Width="90px" Height="24px"  TabIndex="17"  Visible="false"/>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 60%;" align="left" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                        <tr>
                            <td align="right">
                                Change No &nbsp;:&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox Height="30px" ID="txtEditDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEditDoc_No_TextChanged"
                                    onKeyDown="changeno(event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                              Marka Code &nbsp;:&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox Height="30px" ID="txtCode" runat="Server" CssClass="txt" TabIndex="2"
                                    Width="90px" Style="text-align: left;" OnkeyDown="Code(event);" AutoPostBack="false"
                                    OnTextChanged="txtCode_TextChanged"></asp:TextBox>
                                <asp:Button Width="80px" Height="24px" ID="btntxtCode" runat="server" Text="..."
                                    OnClick="btntxtCode_Click" CssClass="btnHelp" />
                                <asp:Label ID="lblcode" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Marka &nbsp;:&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox Height="30px" ID="txtMarka" runat="Server" CssClass="txt" TabIndex="3"
                                    Width="350px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtMarka_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                English Name &nbsp;:&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox Height="30px" ID="txtEnglish_Name" runat="Server" CssClass="txt" TabIndex="4"
                                    Width="350px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEnglish_Name_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Mal Code &nbsp;:&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox Height="30px" ID="txtMal_Code" runat="Server" CssClass="txt" TabIndex="5"
                                    Width="90px" Style="text-align: left;" OnkeyDown="Mal_Code(event);" AutoPostBack="false"
                                    OnTextChanged="txtMal_Code_TextChanged"></asp:TextBox>
                                <asp:Button Width="20px" Height="24px" ID="btntxtMal_Code" runat="server" Text="..."
                                    OnClick="btntxtMal_Code_Click" CssClass="btnHelp" />
                                <asp:Label ID="lblmal_code" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Aarambhi Nag &nbsp;:&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox Height="30px" ID="txtAarambhi_Nag" runat="Server" CssClass="txt" TabIndex="6"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtAarambhi_Nag_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Nagache Vajan &nbsp;:&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox Height="30px" ID="txtNagache_Vajan" runat="Server" CssClass="txt" TabIndex="7"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtNagache_Vajan_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Type &nbsp;:&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drpType" runat="Server" CssClass="txt" TabIndex="8" Width="90px"
                                   Height="30px" AutoPostBack="false" OnSelectedIndexChanged="drpType_SelectedIndexChanged" onKeyDown="Focusbtn(event);">
                                    <asp:ListItem Text="Grain" Value="G" />
                                    <asp:ListItem Text="Pulses" Value="P" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                               Wt Per &nbsp;:&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox Height="30px" ID="txtwtper" runat="Server" CssClass="txt" TabIndex="9"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" ></asp:TextBox>
                            </td>
                        </tr>
                </table>
            </asp:Panel>
            <asp:Panel onkeydown="closepopup(event);" ID="pnlPopup" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 1000px;
                min-height: 700px; box-shadow: 1px 1px 8px 2px; background-position: center;
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
                            <asp:TextBox onkeydown="SelectFirstRow(event);" ID="txtSearchText" runat="server"
                                Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button onkeydown="closepopup(event);" ID="btnSearch" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 680px">
                                <asp:GridView Font-Bold="true" CssClass="select" AllowPaging="true" PageSize="25"
                                    OnPageIndexChanging="grdPopup_PageIndexChanging" ID="grdPopup" runat="server"
                                    AutoGenerateColumns="true" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
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
