<%@ Page Title="Account List" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeAccountType.aspx.cs" Inherits="Sugar_Report_pgeAccountType" %>

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
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function Ac(ac_type) {
            var tn;

            window.open('rptAccount_Type.aspx?Ac_type=' + ac_type);    //R=Redirected  O=Original
        }
        function Group_type(group_Type) {
            var tn;

            window.open('rptgroup_type.aspx?group_Type=' + group_Type);    //R=Redirected  O=Original
        }
        function stateAc() {
            var tn;
            window.open('rptStateWiseAcList.aspx?');    //R=Redirected  O=Original
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

                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;

                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";

                document.getElementById("<%=txtSearchText.ClientID %>").value = "";

                var grid = document.getElementById("<%= grdPopup.ClientID %>");

                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
                var pageCount = document.getElementById("<%= hdHelpPageCount.ClientID %>").value;


                pageCount = parseInt(pageCount);
                if (pageCount > 1) {
                    SelectedRowIndex = SelectedRowIndex + 1;
                }
                if (hdnfClosePopupValue == "txtAcCode") {
                    document.getElementById("<%= txtAcCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblAcCodeName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%= hdnfpodetailid.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                }


                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
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

        function AcCode(e) {
            if (e.keyCode == 112) {
                debugger;

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnAcCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var accode = $("#<%=txtAcCode.ClientID %>").val();

                accode = "0" + accode;
                $("#<%=txtAcCode.ClientID %>").val(accode);
                __doPostBack("txtAcCode", "TextChanged");

            }

        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Account List Report " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:Panel ID="pnlMain" runat="server" ForeColor="Black" Font-Size="14px">
        <asp:HiddenField ID="hdnfClosePopup" runat="server" />
        <asp:HiddenField ID="hdnfpodetailid" runat="server" />
        <asp:HiddenField ID="hdHelpPageCount" runat="server" />
        <table width="60%" align="center" cellpadding="10" cellspacing="5">
            <tr>
                <td>Select AC Type:
            &emsp; &emsp; &emsp; 
                <asp:DropDownList ID="drpAcType" runat="server" CssClass="ddl" Height="24px" Width="250px"
                    OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                    <asp:ListItem Text="All" Value="Z" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Party" Value="P"></asp:ListItem>
                    <asp:ListItem Text="Supplier" Value="S"></asp:ListItem>
                    <asp:ListItem Text="Bank" Value="B"></asp:ListItem>
                    <asp:ListItem Text="Cash" Value="C"></asp:ListItem>
                    <asp:ListItem Text="Relative" Value="R"></asp:ListItem>
                    <asp:ListItem Text="Fixed Assets" Value="F"></asp:ListItem>
                    <asp:ListItem Text="Interest Party" Value="I"></asp:ListItem>
                    <asp:ListItem Text="Income/Expenses" Value="E"></asp:ListItem>
                    <asp:ListItem Text="Trading" Value="O"></asp:ListItem>
                    <asp:ListItem Text="Mill" Value="M"></asp:ListItem>
                    <asp:ListItem Text="Transport" Value="T"></asp:ListItem>
                    <asp:ListItem Text="Broker" Value="BR"></asp:ListItem>
                    <asp:ListItem Text="Retail Party" Value="RP"></asp:ListItem>
                    <asp:ListItem Text="Cash Retail Party" Value="CR"></asp:ListItem>
                </asp:DropDownList>
                    &emsp; &emsp; &emsp; 
                    <asp:Button ID="btnAccountList" runat="server" Text="Account Type Wise Report" CssClass="btnHelp" Width="200"
                        Height="24px" OnClick="btnAccountList_Click" />
                </td>
            </tr>
            <caption>

                <tr>
                    <td align="left">Group Code: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtAcCode" runat="server" AutoPostBack="false" CssClass="txt" Height="24px" onKeyDown="AcCode(event);" OnTextChanged="txtAcCode_TextChanged" Width="80px"></asp:TextBox>
                        <asp:Button ID="btnAcCode" runat="server" CssClass="btnHelp" Height="24px" OnClick="btnAcCode_Click" Text="..." Width="20px" />
                        <asp:Label ID="lblAcCodeName" runat="server" CssClass="lblName"></asp:Label>

                        <asp:Button ID="btnGroup_type" runat="server" CssClass="btnHelp" Height="24px" OnClick="btnGroup_type_Click" Text="Group Code Wise Report" Width="200" />
                        <asp:Button ID="btnStateWise" runat="server" CssClass="btnHelp" Height="24px" OnClick="btnStateWise_Click" Text="State Wise Report" Width="200" />
                         
                    </td>
                </tr>
            </caption>

        </table>
        <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%"
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
                    <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                        Width="250px" Height="20px" AutoPostBack="false"></asp:TextBox>
                        <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                            CssClass="btnSubmit" OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<asp:Panel ID="pnlScroll" runat="server" Width="680px" ScrollBars="Both" Direction="LeftToRight" BackColor="#FFFFE4" style="z-index:5000 ;  float:right; max-height:380px; height:380px;">--%>
                        <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                            Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                            <asp:GridView ID="grdPopup" CssClass="select" runat="server" AutoGenerateColumns="true"
                                EmptyDataText="No Records Found" ViewStateMode="Disabled" PageSize="20" AllowPaging="true"
                                HeaderStyle-BackColor="#6D8980" HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated"
                                OnPageIndexChanging="grdPopup_PageIndexChanging" Style="table-layout: fixed;"
                                OnRowDataBound="grdPopup_RowDataBound">
                                <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                                <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                                <RowStyle Height="25px" ForeColor="Black" Wrap="false" />
                                <PagerStyle BackColor="Tomato" ForeColor="White" Width="100%" Font-Bold="true" />
                                <PagerSettings Position="TopAndBottom" />
                            </asp:GridView>
                        </asp:Panel>
                        <%--</asp:Panel>--%>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
</asp:Content>


