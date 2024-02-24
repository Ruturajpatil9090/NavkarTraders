<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeSharesCompanyParameter.aspx.cs" Inherits="Sugar_Master_pgeSharesCompanyParameter" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>

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
    <script type="text/javascript" src="../Scripts/selectfirstrow.js"></script>
    <script src="../JS/jquery-2.1.3.js" type="text/javascript"></script>
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
                if (hdnfClosePopupValue == "txtEquitySale_ac") {
                    document.getElementById("<%=txtEquitySale_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEquityPurchase_ac") {
                    document.getElementById("<%=txtEquityPurchase_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEquityExpenses_ac") {
                    document.getElementById("<%=txtEquityExpenses_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtFNOSale_ac") {
                    document.getElementById("<%=txtFNOSale_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtFNOPurchase_ac") {
                    document.getElementById("<%=txtFNOPurchase_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtFNOExpenses_ac") {
                    document.getElementById("<%=txtFNOExpenses_ac.ClientID %>").focus();
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
               

                if (hdnfClosePopupValue == "txtEquitySale_ac") {
                    debugger;
                    document.getElementById("<%=txtEquitySale_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblEquitySale_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                 //   document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtEquitySale_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEquityPurchase_ac") {
                    document.getElementById("<%=txtEquityPurchase_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblEquityPurchase_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                   // document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtEquityPurchase_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEquityExpenses_ac") {
                    document.getElementById("<%=txtEquityExpenses_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblEquityExpenses_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                   // document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtEquityExpenses_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtFNOSale_ac") {
                    document.getElementById("<%=txtFNOSale_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblFNOSale_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                   // document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtFNOSale_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtFNOPurchase_ac") {
                    document.getElementById("<%=txtFNOPurchase_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblFNOPurchase_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    //document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtFNOPurchase_ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtFNOExpenses_ac") {
                    document.getElementById("<%=txtFNOExpenses_ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblFNOExpenses_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                   // document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtFNOExpenses_ac.ClientID %>").focus();
                }

            }
}

        function SelectRow(CurrentRow, RowIndex) {
            debugger;
           
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

       

        function EquitySale(e) {
            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtEquitySale_ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var Accode = $("#<%=txtEquitySale_ac.ClientID %>").val();

                Accode = "0" + Accode;
                $("#<%=txtEquitySale_ac.ClientID %>").val(Accode);

                __doPostBack("txtEquitySale_ac", "TextChanged");

            }

        }

        function EquityPurchase(e) {
            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtEquityPurchase_ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var Accode = $("#<%=txtEquityPurchase_ac.ClientID %>").val();

                Accode = "0" + Accode;
                $("#<%=txtEquityPurchase_ac.ClientID %>").val(Accode);

                __doPostBack("txtEquityPurchase_ac", "TextChanged");

            }

        }


        

        function EquityExpenses(e) {

            if (e.keyCode == 112) {
                e.preventDefault();
                 $("#<%=pnlPopup.ClientID %>").show();
              //  $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtEquityExpenses_ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtEquityExpenses_ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtEquityExpenses_ac.ClientID %>").val(unit);
                __doPostBack("txtEquityExpenses_ac", "TextChanged");

            }
        }

        function FNOSale(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
               // $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtFNOSale_ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtFNOSale_ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtFNOSale_ac.ClientID %>").val(unit);
                __doPostBack("txtFNOSale_ac", "TextChanged");

            }
        }

        function FNOPurchase(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
               // $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtFNOPurchase_ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtFNOPurchase_ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtFNOPurchase_ac.ClientID %>").val(unit);
                __doPostBack("txtFNOPurchase_ac", "TextChanged");

            }
        }

        function FNOExpenses(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
              //  $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtFNOExpenses_ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtFNOExpenses_ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtFNOExpenses_ac.ClientID %>").val(unit);
                __doPostBack("txtFNOExpenses_ac", "TextChanged");

            }
        }
    </script>
    <script type="text/javascript">
        $("#btntxtcommission_ac").click(function () {
            debugger;
            $("#<%=hdnfpopup.ClientID%>").val("0");
        });
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
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Company Parameters   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdnfBranch1Code" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfpopup" runat="server" />

            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 10px; margin-top: 0px; z-index: 100;">
                <table width="100%" align="left" cellspacing="5px">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                        <tr>
                            <td align="right" style="width: 10%;">Equity Sale A/c:
                            </td>
                            <td align="left" style="width: 10%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtEquitySale_ac" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                    Height="24px" Style="text-align: right;" OnkeyDown="EquitySale(event);" AutoPostBack="true" OnTextChanged="txtEquitySale_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtEquitySale_ac" runat="server" Text="..." OnClick="btntxtEquitySale_ac_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblEquitySale_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Equity Purchase A/c:
                            </td>
                            <td align="left" style="width: 10%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtEquityPurchase_ac" runat="Server" CssClass="txt" TabIndex="2" Width="80px"
                                    Height="24px" Style="text-align: right;" OnkeyDown="EquityPurchase(event);" AutoPostBack="true" OnTextChanged="txtEquityPurchase_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtEquityPurchase_ac" runat="server" Text="..." OnClick="btntxtEquityPurchase_ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblEquityPurchase_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Equity Expenses A/c:
                            </td>
                            <td align="left" style="width: 10%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtEquityExpenses_ac" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                    Height="24px" Style="text-align: right;" OnkeyDown="EquityExpenses(event);" AutoPostBack="true" OnTextChanged="txtEquityExpenses_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtEquityExpenses_ac" runat="server" Text="..." OnClick="btntxtEquityExpenses_ac_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblEquityExpenses_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">FNO Sale A/c:
                            </td>
                            <td align="left" style="width: 10%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtFNOSale_ac" runat="Server" CssClass="txt" TabIndex="4" Width="80px"
                                    Height="24px" Style="text-align: right;" OnkeyDown="FNOSale(event);" AutoPostBack="true" OnTextChanged="txtFNOSale_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtFNOSale_ac" runat="server" Text="..." OnClick="btntxtFNOSale_ac_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblFNOSale_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">FNO Purchase A/c:
                            </td>
                            <td align="left" style="width: 10%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtFNOPurchase_ac" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                    Height="24px" Style="text-align: right;" OnkeyDown="FNOPurchase(event);" AutoPostBack="true" OnTextChanged="txtFNOPurchase_ac_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtFNOPurchase_ac" runat="server" Text="..." OnClick="btntxtFNOPurchase_ac_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblFNOPurchase_name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                        </tr>
                </table>
                <table width="100%" align="left" cellspacing="5px">
                    <tr>
                        <td align="right" style="width: 10%;">FNO Expenses A/c:
                        </td>
                        <td align="left" style="width: 10%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:TextBox ID="txtFNOExpenses_ac" runat="Server" CssClass="txt" TabIndex="6" Width="80px"
                                 Height="24px" Style="text-align: right;" OnkeyDown="FNOExpenses(event);" AutoPostBack="true" OnTextChanged="txtFNOExpenses_ac_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtFNOExpenses_ac" runat="server" Text="..." OnClick="btntxtFNOExpenses_ac_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblFNOExpenses_name" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%;"></td>
                        <td align="left" style="width: 10%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="save" OnClick="btnUpdate_Click" Visible="true" /></td>
                    </tr>
                </table>

            </asp:Panel>

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
            <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="GhostWhite" Width="955px"
                BorderColor="Teal" BorderWidth="1px" Height="300px" BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
                <table width="80%" align="center">
                    <tr>
                        <td colspan="4" align="center" style="background-color: lightslategrey; color: White;">
                            <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Text="Tender Details"></asp:Label>
                        </td>
                    </tr>

                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

