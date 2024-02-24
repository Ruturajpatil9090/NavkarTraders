<%@ Page Title="Trial Balance Screen" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="pgeTrialBalanceScreen.aspx.cs"
    Inherits="Report_pgeTrialBalanceScreen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../print.css" rel="stylesheet" type="text/css" media="print" />
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
    <script type="text/javascript" language="javascript">
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <script type="text/javascript" language="javascript">
        function sp(accode, fromdt, todt, DrCr) {
            var tn;
            window.open('rptLedger.aspx?accode=' + accode + '&fromdt=' + fromdt + '&todt=' + todt + '&DrCr=' + DrCr);    //R=Redirected  O=Original
        }
    </script>
    <script language="javascript" type="text/javascript">
        document.body.style.cursor = 'pointer';
        var oldColor = '';

        function ChangeRowColor(rowID) {
            var color = document.getElementById(rowID).style.backgroundColor;
            if (color != 'yellow')
                oldColor = color;
            if (color == 'yellow')
                document.getElementById(rowID).style.backgroundColor = oldColor;
            else document.getElementById(rowID).style.backgroundColor = 'yellow';
        }

        //        function PrintPage() {
        //            var printContent = document.getElementById
        //            ('<%= grdDetail.ClientID %>');
        //            var printWindow = window.open("All Records",
        //            "Print Panel", 'left=50000,top=50000,width=0,height=0');
        //            printWindow.document.write(printContent.innerHTML);
        //            printWindow.document.close();
        //            printWindow.focus();
        //            printWindow.print();
        //        }
    </script>
    <script language="javascript" type="text/javascript">
        function PrintPage() {
            var printContent = document.getElementById('<%= pnlGrid.ClientID %>');
            var printWindow = window.open("All Records", "Print Panel", 'left=50000,top=50000,width=0,height=0');

            printWindow.document.write(printContent.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Trial Balance Screen   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <table width="80%" align="center" cellspacing="10">
        <tr>
            <td align="left">Upto Date:
            </td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"> </asp:TextBox>
                <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                    Width="25px" Height="15px" />
                <ajax1:CalendarExtender ID="calenderExtendertxtFromDt" runat="server" TargetControlID="txtDate"
                    PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
            </td>
            <td align="left">Type:&nbsp;<asp:DropDownList ID="drpType" runat="server" Width="200px" Height="25px"
                CssClass="ddl">
                <asp:ListItem Text="All" Value="A" Selected="True"></asp:ListItem>
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
            </asp:DropDownList>
            </td>
            <td colspan="2">
                <asp:Button ID="btnGet" runat="server" CssClass="btnHelp" Text="Get Data" Width="80px"
                    CommandName="DrCr" OnCommand="Command_Click" Height="24px" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnOnlyDr" runat="server" CssClass="btnHelp" Text="Only Dr" Width="80px"
                    Height="24px" CommandName="Dr" OnCommand="Command_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnOnlyCr" runat="server" CssClass="btnHelp" Text="Only Cr" Width="80px"
                    Height="24px" CommandName="Cr" OnCommand="Command_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSendSMS" runat="server" CssClass="btnHelp" Text="Send" Width="80px"
                    OnClick="btnSendSMS_Click" Height="24px" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnExportToExcel" runat="server" CssClass="btnHelp" Text="Export To Excel"
                    Width="120px" OnClick="btnExportToExcel_Click" Height="24px" />
                &nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <b>From (Rs):</b>
                <asp:TextBox runat="server" ID="txtFromRs" CssClass="txt" Width="80px" Height="24px"></asp:TextBox>&nbsp;<b>To</b>&nbsp;<asp:TextBox
                    runat="server" ID="txtToRs" CssClass="txt" Width="80px" Height="24px"></asp:TextBox>
            </td>
            <td align="left">&nbsp;<b>Filter:</b>&nbsp;<asp:DropDownList runat="server" ID="drpFilter" CssClass="ddl"
                Width="120px" AutoPostBack="true" Height="24px" OnSelectedIndexChanged="drpFilter_SelectedIndexChanged">
                <asp:ListItem Text="--Select Filter--" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Debit" Value="D"></asp:ListItem>
                <asp:ListItem Text="Credit" Value="C"></asp:ListItem>
            </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnPrintGrid" runat="server" CssClass="btnHelp" Text="PRINT" Width="120px"
                    OnClick="btnPrintGrid_Click" Height="24px" OnClientClick="return PrintPage();" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">From Date:
                <asp:TextBox ID="txtFromDt" runat="server" CssClass="txt" Width="80px" Height="24px"
                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"> </asp:TextBox>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                    Width="25px" Height="15px" />
                <ajax1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDt"
                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
            </td>
            <td align="left">To Date:
                <asp:TextBox ID="txtToDt" runat="server" CssClass="txt" Width="80px" Height="24px"
                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"> </asp:TextBox>
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                    Width="25px" Height="15px" />
                <ajax1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDt"
                    PopupButtonID="Image2" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
            </td>
            <td>
                <asp:Button ID="btnfrmdtToTodt" runat="server" CssClass="btnHelp" Text="FromDt To ToDt" Width="120px"
                    CommandName="DrCr" OnCommand="Command1_Click" Height="24px" /></td>
            <td>
            <asp:Button ID="btntransfertoJV" runat="server" CssClass="btnHelp" Text="Transfer To JV" Width="120px"
                    CommandName="DrCr" OnCommand="btntransfertoJV_Command" Height="24px" OnClientClick="window.location.reload()"/></td>
            <td>
            <asp:Button ID="btndepriciation" runat="server" CssClass="btnHelp" Text="Depreciation" Width="120px"
                    OnClick="btndepriciation_Click" Height="24px" /></td>
            <td>
            <asp:Button ID="btndeppost" runat="server" CssClass="btnHelp" Text="Depreciation post" Width="120px"
                    OnClick="btndeppost_Click" Height="24px" Visible="false"/></td>
                  <td>
            <asp:Button ID="btndeletedeppost" runat="server" CssClass="btnHelp" Text="Depreciation post delete" Width="120px"
                    OnClick="btndeletedeppost_Click" Height="24px" /></td>
        </tr>
    </table>
    <br />
    <asp:Panel ID="pnlGrid" runat="server" Height="500px" ScrollBars="Both" BorderStyle="Double"
        BackColor="White" BorderWidth="1px" BorderColor="Blue" Width="1200">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="false" OnRowCommand="grdDetail_OnRowCommand"
                    HeaderStyle-BackColor="#397CBB" HeaderStyle-ForeColor="White" HeaderStyle-Height="30px"
                    GridLines="Both" EmptyDataText="No Records found" Width="100%" CellPadding="5"
                    CellSpacing="5" Font-Bold="true" OnRowDataBound="grdDetail_RowDataBound" ForeColor="Black"
                    Font-Names="Verdana" Font-Size="12px" Style="overflow: hidden; table-layout: auto;"
                    OnSelectedIndexChanged="grdDetail_SelectedIndexChanged" OnRowCreated="grdDetail_RowCreated"
                    AllowSorting="true" OnSorting="grdDetail_Sorting">
                    <Columns>
                        <asp:TemplateField HeaderText="Ac_Code" SortExpression="accode">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAc_Code" Text='<%#Eval("accode") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Account Name" SortExpression="acname">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkAcName" Text='<%#Eval("acname") %>' OnClick="lnkAcName_Click"
                                    Style="text-decoration: none;" ToolTip="Select Name For Open Ledger..">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City" SortExpression="city">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCity" Text='<%#Eval("city") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Debit Amount" SortExpression="debitAmt">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDebit" Text='<%#Eval("debitAmt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit Amount" SortExpression="creditAmt">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCredit" Text='<%#Eval("creditAmt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mobile" ControlStyle-Width="120px" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMobile" runat="server" Width="120px" Height="25px" CssClass="txt noprint"
                                    Font-Bold="true" BorderWidth="0px" Style="text-align: right;">
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="mobile" HeaderText="Mobile" ItemStyle-CssClass="invisible"
                            HeaderStyle-CssClass="invisible" ItemStyle-HorizontalAlign="Right" />
                        <asp:TemplateField HeaderText="Is Print">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" runat="server" Checked="false" AutoPostBack="true" CssClass="noprint" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsPrint" runat="server" Checked="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--  <asp:BoundField DataField="" HeaderText="Status" NullDisplayText="N" ControlStyle-Width="10px"
                            ItemStyle-HorizontalAlign="Left" />--%>
                    </Columns>
                    <RowStyle Height="25px" Wrap="false" ForeColor="Black" />
                </asp:GridView>

                   <asp:GridView ID="grddepreciation" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="White"
                                                GridLines="Both" HeaderStyle-ForeColor="Black" HeaderStyle-Height="30px" EmptyDataText="No Records found"
                                                Width="1050px" CellPadding="9" CellSpacing="0" Font-Bold="false" ForeColor="Black"
                                                RowStyle-Height="30px" ShowFooter="true" Font-Names="Verdana" Font-Size="Medium"
                                                CssClass="print" AllowPaging="false" >
                                               
                                                <Columns>                                                   
                                                    <asp:BoundField DataField="accode" HeaderText="accode" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="30px" HeaderStyle-CssClass="thead" />                                                    
                                                    <asp:BoundField DataField="acname" HeaderText="acname" ItemStyle-HorizontalAlign="left"
                                                        ItemStyle-Width="150px" HeaderStyle-CssClass="thead" />
                                                   
                                                      <asp:BoundField DataField="OpeningBalance" HeaderText="OpeningBalance" ItemStyle-HorizontalAlign="Right"
                                                        ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead"
                                                        DataFormatString="{0:f2}" />
                                                    <asp:BoundField DataField="Before" HeaderText="Before" ItemStyle-HorizontalAlign="Right"
                                                        ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead"
                                                        DataFormatString="{0:f2}" />
                                                    <asp:BoundField DataField="After" HeaderText="After" ItemStyle-HorizontalAlign="Right"
                                                        ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead"
                                                        DataFormatString="{0:f2}" />
                                                     <asp:BoundField DataField="Deletion" HeaderText="Deletion" ItemStyle-HorizontalAlign="Right"
                                                        ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead"
                                                        DataFormatString="{0:f2}" />
                                                    <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-HorizontalAlign="Right"
                                                        ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead"
                                                        DataFormatString="{0:f2}" />
                                                    <asp:BoundField DataField="Depamount" HeaderText="Depamount" ItemStyle-HorizontalAlign="Right"
                                                        ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead"
                                                        DataFormatString="{0:f2}" />
                                                     <asp:BoundField DataField="Finalamount" HeaderText="Finalamount" ItemStyle-HorizontalAlign="Right"
                                                        ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead"
                                                        DataFormatString="{0:f2}" />
                                                   <asp:BoundField DataField="InterestRate" HeaderText="InterestRate" ItemStyle-HorizontalAlign="Right"
                                                        ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="thead"
                                                        DataFormatString="{0:f2}" />
                                                </Columns>
                                                <RowStyle Height="10px" Wrap="true" ForeColor="Black" />
                                                <FooterStyle BackColor="Yellow" Font-Bold="true" />
                                            </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExportToExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
