<%@ Page Title="Letter" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeLetter.aspx.cs" Inherits="Sugar_pgeLetter" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function pl(doc_no) {
            window.open('../Report/pgeLetterprint.aspx?doc_no=' + doc_no);
        } 
        function prepl(doc_no) {
            window.open('../Report/pgeLetterprint1.aspx?doc_no=' + doc_no);
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

                var pagecount = document.getElementById("<%=hdnfpagecount.ClientID %>").value;
                if (pagecount > 1) {
                    SelectedRowIndex = SelectedRowIndex + 1;
                }

                if (hdnfClosePopupValue == "txtdoc_no") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").disabled = false;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                //                if (hdnfClosePopupValue == "txtdoc_no") {
                //                    document.getElementById("<%=txtDoc_no.ClientID %>").disabled = false;
                //                    document.getElementById("<%=txtDoc_no.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                //                    document.getElementById("<%=txtdoc_date.ClientID %>").focus();
                //                }
                if (hdnfClosePopupValue == "txtac_code") {
                    document.getElementById("<%=txtac_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtac_name.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtac_code.ClientID %>").focus();
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

        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete?")) {
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

    <script type="text/javascript">
        function Ac_Code(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtac_code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtac_code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtac_code.ClientID %>").val(unit);
                __doPostBack("txtac_code", "TextChanged");

            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Letter   " Font-Names="verdana" ForeColor="White"
                Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdnfpagecount" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table width="80%" align="left" cellspacing="5">
                    <tr>
                        <td align="left">Change No:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                            &nbsp;<asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Letter No
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtDoc_no" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                                Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtdoc_no_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtdoc_no" runat="server" Text="..." Width="80px" OnClick="btntxtdoc_no_Click"
                                CssClass="btnHelp" Height="24px" />
                        </td>
                        <td align="left" style="width: 10%;" colspan="2">&nbsp; Date:
                            <asp:TextBox ID="txtdoc_date" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtdoc_date_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" />
                            <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtdoc_date"
                                PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Party
                        </td>
                        <td align="left" style="width: 10%;" colspan="4">
                            <asp:TextBox ID="txtac_code" runat="Server" CssClass="txt" TabIndex="2" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtac_code_TextChanged"
                                Height="24px" onkeydown="Ac_Code(event);"></asp:TextBox>
                            <asp:Button ID="btntxtac_code" runat="server" Text="..." OnClick="btntxtac_code_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:TextBox ID="txtac_name" runat="Server" CssClass="txt" TabIndex="3" Width="300px"
                                Enabled="false" Style="text-align: left;" Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Address:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtaddress" runat="Server" CssClass="txt" TabIndex="4" Width="300px"
                                Height="24px" Style="text-align: left; vertical-align: text-top;" AutoPostBack="false"
                                OnTextChanged="txtaddress_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">City:
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox runat="server" ID="txtCity" TabIndex="5" Width="200px" Height="24px"
                                CssClass="txt"></asp:TextBox>
                            PinCode:
                            <asp:TextBox runat="server" ID="txtPinCode" TabIndex="6" Width="136px" Height="24px"
                                CssClass="txt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Kind Attention:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtkind_att" runat="Server" CssClass="txt" TabIndex="7" Width="200px"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtkind_att_TextChanged"
                                Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Subject:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtsubject" runat="Server" CssClass="txt" TabIndex="8" Width="200px"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtsubject_TextChanged"
                                Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Ref No:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtref_no" runat="Server" CssClass="txt" TabIndex="9" Width="200px"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtref_no_TextChanged"
                                Height="24px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;" colspan="2">Dated:
                            <asp:TextBox ID="txtref_dt" runat="Server" CssClass="txt" TabIndex="10" Width="80px"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtref_dt_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="img1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                Height="15px" />
                            <ajax1:CalendarExtender ID="CalendarExtendertxtref_dt" runat="server" TargetControlID="txtref_dt"
                                PopupButtonID="img1" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" style="width: 10%;">Matter
                        </td>
                        <td align="left" colspan="4" style="width: 10%;">
                            <asp:TextBox ID="txtmatter" runat="Server" CssClass="txt" TabIndex="11" Width="900px"
                                TextMode="MultiLine" Height="300px" Style="text-align: left;" AutoPostBack="false"
                                OnTextChanged="txtmatter_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Authorises Person:
                        </td>
                        <td align="left" style="width: 10%;" colspan="4">
                            <asp:TextBox ID="txtauthorised_person" runat="Server" CssClass="txt" TabIndex="12"
                                Height="24px" Width="200px" Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtauthorised_person_TextChanged"></asp:TextBox>
                            Designation:
                            <asp:TextBox ID="txtdesignation" runat="Server" CssClass="txt" TabIndex="13" Width="200px"
                                Height="24px" Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtdesignation_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="save" OnClick="btnAdd_Click" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            TabIndex="14" Height="24px" ValidationGroup="add" OnClick="btnSave_Click" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="save" OnClick="btnEdit_Click" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="add" OnClientClick="Confirm()" OnClick="btnDelete_Click" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" />
                        &nbsp;
                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" Width="90px"
                            Height="24px" OnClick="btnPrint_Click" />
                         &nbsp;
                        <asp:Button ID="btnPrePrint" runat="server" Text="PrePrint" CssClass="btnHelp" Width="90px"
                            Height="24px" OnClick="btnPrePrint_Click" />
                        &nbsp;
                        <asp:Button runat="server" ID="btnSaveNew" Text="Save To New" Width="100px" CssClass="btnHelp"
                            Height="24px" OnClick="btnSaveNew_Click" Visible="true" />
                    </td>
                    <td align="center">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            Height="24px" OnClick="btnFirst_Click" Width="90px" />
                        &nbsp;<asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                            Height="24px" OnClick="btnPrevious_Click" Width="90px" />
                        &nbsp;<asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                            Height="24px" OnClick="btnNext_Click" Width="90px" />
                        &nbsp;<asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                            Height="24px" OnClick="btnLast_Click" Width="90px" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="80%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg"
                    Width="20px" Height="20px" Style="float: right; vertical-align: top;" OnClick="imgBtnClose_Click"
                    ToolTip="Close" />
                <table width="95%">
                    <tr>
                        <td align="center" style="background-color: #F5B540; width: 10%;">
                            <asp:Label ID="lblPopupHead" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Font-Bold="true" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Search Text:
                            <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                                Width="250px" Height="20px" AutoPostBack="true" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="85%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: left; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    EmptyDataText="No Records Found" AllowPaging="true" PageSize="20" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                    OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
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
            <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="GhostWhite" Width="955px"
                BorderColor="Teal" BorderWidth="1px" Height="300px" BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
                <table width="70%" align="center">
                    <tr>
                        <td colspan="2" align="center" style="background-color: lightslategrey; color: White;">
                            <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Text="Tender Details"></asp:Label>
                        </td>
                    </tr>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
