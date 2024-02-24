<%@ Page Title="Interest Calculation" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeInterestCalculation.aspx.cs" Inherits="Report_pgeInterestCalculation" %>

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
        function sp(accode, fromdt, todt, intRate, intDays, DrCr) {
            var tn;
            window.open('rptInterestStatement.aspx?accode=' + accode + '&fromdt=' + fromdt + '&todt=' + todt + '&intRate=' + intRate + '&intDays=' + intDays + '&DrCr=' + DrCr, '_blank');    //R=Redirected  O=Original
        }
    </script>
    <script type="text/javascript" language="javascript">
        function Ac_Code(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=hdnfpopup.ClientID%>").val("0");
              //  $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnACCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtAc_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtAc_Code.ClientID %>").val(unit);
                __doPostBack("txtAc_Code", "TextChanged");

            }

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

                var grid = document.getElementById("<%= grdPopup.ClientID %>");

                document.getElementById("<%= txtAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                document.getElementById("<%= lblAC_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                document.getElementById("<%= txtAc_Code.ClientID %>").focus();
                document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                
            }
}

function SelectRow(CurrentRow, RowIndex) {

    UpperBound = parseInt('<%= this.grdPopup.Rows.Count %>') - 1;
    LowerBound = 0;

    if (SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound)

        //    return;


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
                var hdnfClosePopupValue = document.getElementById("<%= hdnfclosepopup.ClientID %>").value;
                if (hdnfClosePopupValue == "txtAc_Code") {
                    document.getElementById("<%=txtAc_Code.ClientID %>").focus();
                }
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfclosepopup.ClientID %>").value = "Close";
            }

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Interest Calculation   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:HiddenField runat="server" ID="hdnfclosepopup" />
    <asp:HiddenField ID="hdHelpPageCount" runat="server" />
     <asp:HiddenField ID="hdnfpopup" runat="server" />
    <table width="60%" align="center" cellspacing="5" style="font-family: Calibri; font-size: 14px;">
        <tr>
            <td align="left">Ac_Code:
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtAc_Code" runat="server" CssClass="txt" Width="90px" Height="24px"
                    AutoPostBack="false" OnTextChanged="txtAc_Code_TextChanged" onKeyDown="Ac_Code(event);"></asp:TextBox>
                <asp:Button ID="btnACCode" runat="server" CssClass="btnHelp" Text="..." Height="24px"
                    Width="25px" OnClick="btnACCode_Click" />
                <asp:Label ID="lblAC_Code" runat="server" CssClass="lblName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">From:
            </td>
            <td align="left">
                <asp:TextBox ID="txtFromDt" runat="server" Width="80px" CssClass="txt" Height="24px"></asp:TextBox>
                <asp:Image ID="imgtxtFromDt" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                    Width="25px" Height="15px" TabIndex="4" />
                <ajax1:CalendarExtender ID="CalendarExtendertxtFromDt" runat="server" TargetControlID="txtFromDt"
                    PopupButtonID="imgtxtFromDt" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
                &nbsp;&nbsp; To:
                <asp:TextBox ID="txtToDt" runat="server" Width="80px" CssClass="txt" Height="24px"></asp:TextBox>
                <asp:Image ID="imgtxtToDt" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                    Width="25px" Height="15px" TabIndex="4" />
                <ajax1:CalendarExtender ID="CalendarExtendertxtToDt" runat="server" TargetControlID="txtToDt"
                    PopupButtonID="imgtxtToDt" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td align="left">EMail-ID:
            </td>
            <td align="left">
                <asp:TextBox Height="24px" ID="txtEmailID" runat="server" Width="300px" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">Interest Rate:
            </td>
            <td align="left">
                <asp:TextBox ID="txtInterestRate" Height="24px" runat="server" Width="100px" CssClass="txt"
                    Style="text-align: right;"></asp:TextBox>
                <ajax1:FilteredTextBoxExtender ID="filtertxtInterestRate" runat="server" TargetControlID="txtInterestRate"
                    FilterType="Numbers,Custom" ValidChars=".">
                </ajax1:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td align="left">Days:
            </td>
            <td align="left">
                <asp:TextBox ID="txtDays" runat="server" Width="100px" Height="24px" CssClass="txt"
                    Style="text-align: right;"></asp:TextBox>
                <ajax1:FilteredTextBoxExtender ID="FiltertxtDays" runat="server" TargetControlID="txtDays"
                    FilterType="Numbers">
                </ajax1:FilteredTextBoxExtender>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3">
                <asp:Button ID="btnShow" runat="server" CssClass="btnHelp" Text="Show" Width="80px"
                    OnClick="btnShow_Click" Height="24px" />
                &nbsp;&nbsp;
                <asp:Button ID="btnOnlyDR" runat="server" CssClass="btnHelp" Text="Only Dr." Width="80px"
                    Height="24px" OnClick="btnOnlyDR_Click" />
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
                    <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server" Width="250px" Height="20px" AutoPostBack="true"></asp:TextBox>
                    <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search" CssClass="btnSubmit" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:Panel ID="pnlScroll" runat="server" Width="680px" ScrollBars="Both" Direction="LeftToRight" BackColor="#FFFFE4" style="z-index:5000 ;  float:right; max-height:380px; height:380px;">--%>
                    <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                        Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                        <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true" EmptyDataText="No Records Found"
                            PageSize="50" AllowPaging="true" HeaderStyle-BackColor="#6D8980" HeaderStyle-ForeColor="White"
                            OnRowCreated="grdPopup_RowCreated" OnPageIndexChanging="grdPopup_PageIndexChanging"
                            Style="table-layout: fixed;">
                            <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                            <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                            <RowStyle Height="25px" ForeColor="Black" Wrap="false" />
                            <PagerStyle BackColor="Tomato" ForeColor="White" Font-Bold="true" />
                            <PagerSettings Position="TopAndBottom" />
                        </asp:GridView>
                    </asp:Panel>
                    <%--</asp:Panel>--%>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
