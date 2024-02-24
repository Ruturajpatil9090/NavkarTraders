<%@ Page Title="GST Rate Master" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeGSTRateMaster.aspx.cs" Inherits="Sugar_pgeGSTRateMaster" %>

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
                if (hdnfClosePopupValue == "txtAC_CODE") {
                    //document.getElementById("<%//=txtGST_Code.ClientID %>").focus();
                 }

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

                if (hdnfClosePopupValue == "txtdoc_no") {
                    document.getElementById("<%=txtdoc_no.ClientID %>").disabled = false;
                    document.getElementById("<%=txtdoc_no.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtdoc_no.ClientID %>").focus();
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
    <script type="text/javascript">
        function gstratemaster() {
            var Action = 2;
            var Group_Code = 0;
            //alert(td);
            window.open('../Master/PgeGSTRateMasterUtility.aspx');
        }
    </script>

     <script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Goods and Services Rate Master   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
            <table width="80%" align="left" cellpadding="10px" cellspacing="10px">
                <tr>
                    <td style="height: 30px;"></td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" TabIndex="9" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnDelete_Click" Height="24px" OnClientClick="Confirm()" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Height="24px" CssClass="btnHelp"
                            Width="90px" ValidationGroup="save" OnClick="btnCancel_Click" TabIndex="10" />
                        &nbsp;
                         <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                             ValidationGroup="save" Height="24px" OnClientClick="gstratemaster()" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 80%; margin-left: 70px; margin-top: 50px;" align="right" cellpadding="5" cellspacing="5">
                    <tr>
                        <td align="Right">Change No:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>

                            &nbsp;<asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">DOC NO:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtdoc_no" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtdoc_no_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtDOC_NO" runat="server" Text="..." Width="80px" OnClick="btntxtDOC_NO_Click"
                                CssClass="btnHelp" Height="24px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">GST Name
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtGST_Name" runat="server" CssClass="txtUpper" Width="438px" TabIndex="2"
                                Height="24px" AutoPostBack="true"></asp:TextBox>
                          <%--  <ajax1:FilteredTextBoxExtender ID="FilteredtxtGST_Name" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                ValidChars="." TargetControlID="txtGST_Name">
                            </ajax1:FilteredTextBoxExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Rate %
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRate" runat="server" CssClass="txt" Width="10%" TabIndex="3"
                                Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server"
                                ID="FilteredtxtRate" FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtRate">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">IGST %
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtIGST" runat="server" CssClass="txt" Width="10%" TabIndex="4"
                                Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server"
                                ID="FilteredtxtIGST" FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtIGST">
                            </ajax1:FilteredTextBoxExtender>

                        </td>
                    </tr>

                    <tr>
                        <td align="right">SGST %
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSGST" runat="server" CssClass="txt" Width="10%" TabIndex="5"
                                Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server"
                                ID="FilteredtxtSGST" FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtSGST">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">CGST %
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCGST" runat="server" CssClass="txt" Width="10%" TabIndex="6"
                                Height="24px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server"
                                ID="FilteredtxtCGST" FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtCGST">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Remark
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="txt" Width="30%" TabIndex="7" TextMode="MultiLine"
                                Height="40px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="filtertxtRemark" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                ValidChars="." TargetControlID="txtRemark">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td align="right">GST Code
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtGST_Code" runat="server" CssClass="txt" Width="10%" AutoPostBack="false" TabIndex="8"
                                OnTextChanged="txtGST_Code_TextChanged" Height="24px" onkeydown="StateCode(event);"></asp:TextBox>
                            <asp:Button ID="btntxtGST_Code" runat="server" Text="..." Width="30px" OnClick="btntxtGST_Code_Click"
                                CssClass="btnHelp" Height="24px" />
                            <asp:Label ID="lblCode_Name" runat="server" CssClass="lblName"></asp:Label>
                            <ajax1:FilteredTextBoxExtender runat="server"
                                ID="FilteredtxtGST_Code" FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtGST_Code">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>--%>
                </table>
            </asp:Panel>
            <br />
            <br />

            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%" align="center" ScrollBars="None"
                BackColor="#FFFFE4" Direction="LeftToRight" Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%; margin: 0 auto;">
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
                            <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server" Width="250px" Height="20px" AutoPostBack="true"
                                OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search" CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" ScrollBars="Both" Width="100%" Direction="LeftToRight"
                                BackColor="#FFFFE4" Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true" AllowPaging="true"
                                    PageSize="13" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
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

