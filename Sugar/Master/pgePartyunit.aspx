<%@ Page Title="Party Unit" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgePartyunit.aspx.cs" Inherits="pgePartyunit" %>

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
                    document.getElementById("<%=txtac_code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtac_code") {
                    document.getElementById("<%=txtac_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblParty_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtac_code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPartycode") {
                    document.getElementById("<%=txtPartycode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblpartyname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtremarks.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtcity_code") {
                    document.getElementById("<%=txtcity_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblCityName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtvat_no.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtunit_name") {
                    document.getElementById("<%=txtunit_name.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblUnitName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtunit_name.ClientID %>").focus();
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
        function AcCode(e) {
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

        function UnitCode(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtunit_name.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtunit_name.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtunit_name.ClientID %>").val(unit);
                __doPostBack("txtunit_name", "TextChanged");

            }

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
                if (hdnfClosePopupValue == "txtunit_name") {
                    document.getElementById("<%=txtunit_name.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtac_code") {
                    document.getElementById("<%=txtac_code.ClientID %>").focus();
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


        function au() {
            window.open('../Report/rptPartyUnitDetails.aspx');
        }

        function pd(Party_Code) {
            window.open('../Report/rptParty.aspx?Party_Code=' + Party_Code);
        }
    </script>
    <script type="text/javascript">
        function Back() {
            var Action = 2;
            var Group_Code = 0;
            //alert(td);
            window.open('../Master/pgePartyunitUtility.aspx');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Party Unit Master   " Font-Names="verdana"
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
            <table width="100%" align="left" style="width: 83%">
                <tr>
                    <td align="center" colspan="5">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="25px" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            TabIndex="12" ValidationGroup="add" OnClick="btnSave_Click" Height="25px" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="25px" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="25px" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            TabIndex="13" ValidationGroup="save" OnClick="btnCancel_Click" Height="25px" />
                        &nbsp;
                         <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                             TabIndex="14" ValidationGroup="save" Height="24px" OnClientClick="Back()" />
                    </td>

                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table cellspacing="5">
                     <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <tr>
                         <td align="right" style="width: 10%;">Change No:
                        </td>
                          <td align="right" style="width: 10%;">
                            <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                           
                            &nbsp;
                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
					
                   
                        <td align="right" style="width: 10%;">Entry No:-
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtdoc_no" runat="Server" CssClass="txt" TabIndex="0" Width="90px"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtdoc_no_TextChanged"
                                Height="25px"></asp:TextBox>
                            <asp:Button ID="btntxtdoc_no" runat="server" Text="..." Width="80px" OnClick="btntxtdoc_no_Click"
                                CssClass="btnHelp" Height="25px" />
                            <asp:Label ID="lblbsid" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Font-Bold="true" ForeColor="Blue" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;">Party Code:-
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtac_code" runat="Server" CssClass="txt" TabIndex="1" Width="90px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtac_code_TextChanged"
                                Height="25px" onkeydown="AcCode(event);"></asp:TextBox>
                            <asp:Button ID="btntxtac_code" runat="server" Text="..." OnClick="btntxtac_code_Click"
                                CssClass="btnHelp" Height="25px" Width="20px" />
                            <asp:Label ID="lblParty_name" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;">Unit Name:-
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtunit_name" runat="Server" CssClass="txt" TabIndex="2" Width="90px"
                                Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtunit_name_TextChanged"
                                Height="25px" onKeyDown="UnitCode(event);"></asp:TextBox>
                            <asp:Button ID="btntxtunit_name" runat="server" Text="..." CssClass="btnHelp" OnClick="btntxtunit_name_Click"
                                Height="25px" Width="20px" />
                            <asp:Label ID="lblUnitName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;"></td>
                        <td>
                            <asp:Panel ID="pnlhiding" runat="server" Visible="false">
                                <tr>
                                    <td align="right" style="width: 10%;">Unit Address:-
                                    </td>
                                    <td align="left" style="width: 10%;">
                                        <asp:TextBox ID="txtunit_address" runat="Server" CssClass="txt" TabIndex="3" Width="320px"
                                            Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtunit_address_TextChanged"
                                            Height="25px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 10%;">City Code:-
                                    </td>
                                    <td align="left" style="width: 10%;">
                                        <asp:TextBox ID="txtcity_code" runat="Server" CssClass="txt" TabIndex="4" Width="90px"
                                            Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtcity_code_TextChanged"
                                            Height="25px"></asp:TextBox>
                                        <asp:Button ID="btntxtcity_code" runat="server" Text="..." OnClick="btntxtcity_code_Click"
                                            CssClass="btnHelp" Height="25px" Width="20px" />
                                        <asp:Label ID="lblCityName" runat="server" CssClass="lblName"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 10%;">V.A.T. No.:-
                                    </td>
                                    <td align="left" style="width: 10%;">
                                        <asp:TextBox ID="txtvat_no" runat="Server" CssClass="txt" TabIndex="5" Width="113px"
                                            Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtvat_no_TextChanged"
                                            Height="25px"></asp:TextBox>
                                        E.C.C. No:-
                                        <asp:TextBox ID="txtecc_no" runat="Server" CssClass="txt" TabIndex="6" Width="120px"
                                            Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtecc_no_TextChanged"
                                            Height="25px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 10%;">Mobile Person1 Name:-
                                    </td>
                                    <td align="left" style="width: 10%;">
                                        <asp:TextBox ID="txtperson1" runat="Server" CssClass="txt" TabIndex="7" Width="320px"
                                            Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtperson1_TextChanged"
                                            Height="25px"></asp:TextBox>
                                    </td>
                                    <td align="left" style="width: 10%;">Mobile No:-
                                        <asp:TextBox ID="txtperson1_mobile" runat="Server" CssClass="txt" TabIndex="8" Width="120px"
                                            Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtperson1_mobile_TextChanged"
                                            Height="25px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 10%;">Mobile Person1 Name:-
                                    </td>
                                    <td align="left" style="width: 10%;">
                                        <asp:TextBox ID="txtperson2" runat="Server" CssClass="txt" TabIndex="9" Width="320px"
                                            Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtperson2_TextChanged"
                                            Height="25px"></asp:TextBox>
                                    </td>
                                    <td align="left" style="width: 10%;">Mobile No:-
                                        <asp:TextBox ID="txtperson2_mobile" runat="Server" CssClass="txt" TabIndex="10" Width="120px"
                                            Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtperson2_mobile_TextChanged"
                                            Height="25px"></asp:TextBox>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;">Remarks:-
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtremarks" runat="Server" CssClass="txt" TabIndex="11" Width="320px"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtremarks_TextChanged"
                                Height="25px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <br />
            <br />
            <br />
            <br />
            <table align="left" style="width: 83%" cellspacing="5">
                <tr>
                    <td align="right" style="width: 10%;">
                        <b>Party Code:</b>
                    </td>
                    <td align="left" style="width: 10%;">
                        <asp:TextBox ID="txtPartycode" runat="Server" CssClass="txt" Width="90px" Style="text-align: right;"
                            AutoPostBack="True" Height="25px" OnTextChanged="txtPartycode_TextChanged"></asp:TextBox>
                        <asp:Button ID="btnGetparty" runat="server" Text="..." CssClass="btnHelp" Height="25px"
                            Width="20px" OnClick="btnGetparty_Click" />
                        <asp:Label ID="lblpartyname" runat="server" CssClass="lblName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 10%;"></td>
                    <td align="left">
                        <asp:Button runat="server" ID="btnReport" CssClass="btnHelp" Text="Show" Height="24px"
                            Width="100px" OnClick="btnReport_Click" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%; margin: 0 auto;">
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
                            <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                                Width="250px" Height="20px" AutoPostBack="true" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" ScrollBars="Both" Width="100%" Direction="LeftToRight"
                                BackColor="#FFFFE4" Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    AllowPaging="true" PageSize="13" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
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
