<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeCustomerEntry.aspx.cs" Inherits="Sugar_Utility_pgeCustomerEntry" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Script/jquery-1.4.2.js"></script>

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
<%--    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Add Account?")) {
                confirm_value.value = "Yes";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            }
            else {
                confirm_value.value = "No";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }
    </script>--%>

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
                if (hdnfClosePopupValue == "txtac_code") {
                    document.getElementById("<%=txtac_code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtWrongAccoun") {
                    document.getElementById("<%=txtWrongAccoun.ClientID %>").focus();
                }

                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
    </script>

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


                if (hdnfClosePopupValue == "txtac_code") {
                    debugger;
                    document.getElementById("<%=txtac_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblParty_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=lblaccoid.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtWrongAccoun.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtWrongAccoun") {
                    document.getElementById("<%=txtWrongAccoun.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblWrongAccount.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=btnClubAccount.ClientID %>").focus();
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

        function accode(e) {

            if (e.keyCode == 112) {
                e.preventDefault();
                // $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
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


        function wrongacc(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
        $("#<%=hdnfpopup.ClientID%>").val("0");
        $("#<%=btnWrongAccount.ClientID %>").click();

    }
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txtWrongAccoun.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtWrongAccoun.ClientID %>").val(unit);
                __doPostBack("txtWrongAccoun", "TextChanged");

            }

        }

        function Accmaster(Accoid, Action) {
            window.open('../Master/pgeAccountsmaster.aspx?Ac_Code=' + Accoid + '&Action=' + Action);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Update Account   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <br />
    <br />
    <asp:HiddenField ID="hdconfirm" runat="server" />
    <asp:HiddenField ID="hdnfClosePopup" runat="server" />
    <asp:HiddenField ID="hdnf" runat="server" />
    <asp:HiddenField ID="hdnfSuffix" runat="server" />
    <asp:HiddenField ID="hdHelpPageCount" runat="server" />
    <asp:HiddenField ID="hdnfpopup" runat="server" />
     <asp:HiddenField ID="hdnfchk" runat="server" />

    <table align="center" width="50%" cellpadding="5" cellspacing="5">
        <tr>
            <td align="center" style="width: 10%;">Client Account:
            </td>
            <td align="left" style="width: 10%;">
                <asp:TextBox ID="txtac_code" runat="Server" CssClass="txt" TabIndex="2" Width="90px"
                    Style="text-align: right;" OnkeyDown="accode(event);" AutoPostBack="false" OnTextChanged="txtac_code_TextChanged"
                    Height="24px"></asp:TextBox>
                <asp:Button ID="btntxtac_code" runat="server" Text="..." OnClick="btntxtac_code_Click"
                    CssClass="btnHelp" Height="24px" Width="20px" />
                <asp:Label ID="lblParty_name" runat="server" CssClass="lblName"></asp:Label>
                <asp:Label ID="lblaccoid" runat="server" CssClass="lblName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 10%;" align="center">Upadate Client Account:
            </td>
            <td align="left" style="width: 10%;">
                <asp:TextBox ID="txtWrongAccoun" runat="Server" CssClass="txt" TabIndex="2" Width="90px"
                    Style="text-align: right;" OnkeyDown="wrongacc(event);" AutoPostBack="false" Height="24px" OnTextChanged="txtWrongAccoun_TextChanged"></asp:TextBox>
                <asp:Button ID="btnWrongAccount" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                    Width="20px" OnClick="btnWrongAccount_Click" />
                <asp:Label ID="lblWrongAccount" runat="server" CssClass="lblName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnClubAccount" runat="server" Text="Update Account" CssClass="btnHelp"
                    Height="30px" OnClientClick="return Confirm();" OnClick="btnClubAccount_Click" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%" align="center" ScrollBars="None"
        BackColor="#FFFFE4" Direction="LeftToRight" Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
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
                    <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                        Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                        <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true" AllowPaging="true"
                            PageSize="20" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
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
</asp:Content>
