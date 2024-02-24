<%@ Page Title="Other GST" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeOther_GST_Input.aspx.cs" Inherits="Sugar_Inword_pgeOther_GST_Input" %>

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
    <%--<script type="text/javascript">
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
    </script>--%>
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
                if (hdnfClosePopupValue == "txtDoc_No") {

                    document.getElementById("<%=txtDoc_No.ClientID %>").focus();
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

                if (hdnfClosePopupValue == "txtExps") {
                    document.getElementById("<%=txtExps.ClientID %>").value = "";
                    document.getElementById("<%=txtExps.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblExps.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtExps.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = "";
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();

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
        function changeno(e) {
            debugger;
            if (e.keyCode == 112) {

                e.preventDefault();
                // $("#<%=pnlPopup.ClientID %>").show();
                var edi = "txtEditDoc_No"
                $("#<%=hdnfClosePopup.ClientID %>").val(edi);
                $("#<%= btnSearch.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtEditDoc_No", "TextChanged");

            }
        }



        function EmpCode(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtExps.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtExps.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtExps.ClientID %>").val(unit);
                __doPostBack("txtExps", "TextChanged");

            }

        }




    </script>
     <script type="text/javascript">
         function Back() {
             var Action = 2;
             var Group_Code = 0;
             //alert(td);
             window.open('../Inword/pgeOtherGSTInput_Utility.aspx', '_self');
         }
    </script>

     <script language="JavaScript" type="text/javascript">

         //window.onbeforeunload = function (e) {
         //    var e = e || window.event;
         //    if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
         //    return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         //};

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Other GST Input   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
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
                            ValidationGroup="save" OnClick="btnAdd_Click" TabIndex="9" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" TabIndex="10" OnClick="btnSave_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" TabIndex="11" OnClick="btnEdit_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" TabIndex="12" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" TabIndex="13" OnClick="btnCancel_Click" Height="24px" />
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" Height="24px" OnClientClick="Back()" />
                    </td>

                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 60%;" align="center" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true"
                                Font-Names="verdana" Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                        <tr>
                            <%-- <td align="right" style="width: 10%;">Change No
                            </td>--%>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtEditDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEditDoc_No_TextChanged" OnKeyDown="changeno(event);" Visible="false"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Doc No
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtDoc_No" runat="Server" CssClass="txt" TabIndex="2"
                                    Width="90px" Style="text-align: left;" OnkeyDown="Doc_No(event);" AutoPostBack="false"
                                    OnTextChanged="txtDoc_No_TextChanged"></asp:TextBox>
                                <asp:Button Widt="90px" Height="24px" ID="btntxtDoc_No" runat="server" Text="..."
                                    OnClick="btntxtDoc_No_Click" CssClass="btnHelp" Visible="false" />

                                <asp:Label ID="lblDocNo" runat="server" Font-Size="Medium" Font-Names="verdana"
                                    Font-Bold="true" ForeColor="Blue" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Date
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtDoc_Date" runat="Server" CssClass="txt" TabIndex="3"
                                    Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtDoc_Date_TextChanged"
                                    onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                                <asp:Image ID="imgcalendertxtDoc_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                    Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtDoc_Date"
                                        runat="server" TargetControlID="txtDoc_Date" PopupButtonID="imgcalendertxtDoc_Date"
                                        Format="dd/MM/yyyy">
                                    </ajax1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">CGST Amount
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtCGST_Amt" runat="Server" CssClass="txt" TabIndex="4"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCGST_Amt_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtCGST_Amt" TargetControlID="txtCGST_Amt"
                                    FilterType="Numbers,Custom" runat="server" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">SGST Amount
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtSGST_Amt" runat="Server" CssClass="txt" TabIndex="5"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSGST_Amt_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtSGST_Amt" TargetControlID="txtSGST_Amt"
                                    FilterType="Numbers,Custom" runat="server" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">IGST Amount
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtIGST_Amt" runat="Server" CssClass="txt" TabIndex="6"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtIGST_Amt_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilteredtxtIGST_Amt" TargetControlID="txtIGST_Amt"
                                    FilterType="Numbers,Custom" runat="server" ValidChars=".">
                                </ajax1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Exps A/C
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtExps" runat="Server" CssClass="txt" TabIndex="7"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtExps_TextChanged" onKeyDown="EmpCode(event);"></asp:TextBox>
                                <asp:Button ID="btntxtExps" runat="server" Text="..." OnClick="btntxtExps_Click"
                                    CssClass="btnHelp" />
                                <asp:Label ID="lblExps" runat="server" CssClass="lblName"></asp:Label>
                                <asp:Label ID="lblexpid" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Narration
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="50px" ID="txtNarration" runat="Server" CssClass="txt" TabIndex="8"
                                    Width="200px" Style="text-align: left;" TextMode="MultiLine" AutoPostBack="false" OnTextChanged="txtNarration_TextChanged"></asp:TextBox>
                               <%-- <ajax1:FilteredTextBoxExtender ID="FilteredtxtNarration" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                    ValidChars="." TargetControlID="txtNarration">
                                </ajax1:FilteredTextBoxExtender>--%>
                            </td>
                        </tr>
                </table>
                <%--<fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
                    margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
                    border-left: 0px; border-right: 0px; height: 7px;">
                    <legend style="text-align: center;">
                        <h5 style="color: White;" font-names="verdana" font-size="Medium">
                            Detail Entry</h5>
                    </legend>
                </fieldset>--%>
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

