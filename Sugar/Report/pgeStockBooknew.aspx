<%@ Page Title="Stock Book" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeStockBooknew.aspx.cs" Inherits="Report_pgeStockBooknew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function sb(fromDT, toDT) {
            window.open('../Report/rptStockBook.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }
        function rsb(fromDT, toDT) {
            window.open('../Report/rptReturnStockBook.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }

        function ss(fromDT, toDT) {
            window.open('../Report/rptStockSummary.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }
        function rss(fromDT, toDT) {
            window.open('../Report/rptReturnStockSummary.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }
        function sbr(fromDT, toDT) {
            window.open('../Report/rptRetailSellStock.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }
        function ors(fromDT, toDT) {
            window.open('../Report/rptOnlyRetailStock.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }
        function udr(fromDT, toDT) {
            window.open('../Report/rptUndeliveredReport.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }
        function dr(fromDT, toDT) {
            window.open('../Report/rptDeliveredReport.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }
        function cr(fromDT, toDT) {
            window.open('../Report/rptCashReceiveReport.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }
        function StockDetail(fromDT, toDT,no) {
            window.open('../Report/rptStockDetailrepotnew.aspx?fromDT=' + fromDT + '&toDT=' + toDT +'&no='+ no);
        }
        function cnr(fromDT, toDT) {
            window.open('../Report/rptCashNotReceiveReport.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }
        function PRC(fromDT, toDT) {
            window.open('../Report/rptSugarPurchaseCreditReport.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }
        function stocksummary(fromDT, toDT) {
            window.open('../Report/rptstocksummaryreportnew.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }
        function Itemwisestock(fromDT, toDT) {
            window.open('../Report/rptItemwiseStockDetail.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }
        function MStockDeatil(fromDT, toDT) {
            window.open('../Report/rptMillWiseStockDetail.aspx?fromDT=' + fromDT + '&toDT=' + toDT);
        }
        
    </script>
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
                 if (hdnfClosePopupValue == "txtAcCode") {
                     document.getElementById("<%=txtAcCode.ClientID %>").focus();
                 }
                 if (hdnfClosePopupValue == "txtbrandcode") {
                     document.getElementById("<%=txtbrandcode.ClientID %>").focus();
                 }
                 if (hdnfClosePopupValue == "txtpurno") {
                     document.getElementById("<%=txtpurno.ClientID %>").focus();
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
                    document.getElementById("<%=txtAcCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtbrandcode") {
                    document.getElementById("<%= txtbrandcode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= Label2.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtbrandcode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtpurno") {
                    document.getElementById("<%= txtpurno.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= Label3.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtpurno.ClientID %>").focus();
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
        function ac_name(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnAcCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtAcCode.ClientID %>").val();


                $("#<%=txtAcCode.ClientID %>").val(unit);
                __doPostBack("txtAcCode", "TextChanged");

            }
        }

        function Bc_name(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnBrandcode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtbrandcode.ClientID %>").val();


                $("#<%=txtbrandcode.ClientID %>").val(unit);
                __doPostBack("txtbrandcode", "TextChanged");

            }
        }
        function Pc_name(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnpurno.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtpurno.ClientID %>").val();


                $("#<%=txtpurno.ClientID %>").val(unit);
                __doPostBack("txtpurno", "TextChanged");

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Stock Book   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:HiddenField ID="hdnfClosePopup" runat="server" />
    <asp:HiddenField ID="hdHelpPageCount" runat="server" />
    <br />
    <table align="center" width="40%" cellspacing="5">
        <tr>
            <td align="left" style="width: 40%;">
                <b>Mill Code</b>
            </td>
            <td align="left" colspan="5" style="width: 60%;">
                <asp:TextBox ID="txtAcCode" runat="server" Width="80px" CssClass="txt" AutoPostBack="true"
                    OnTextChanged="txtAcCode_TextChanged" onkeyDown="ac_name(event);" Height="24px"
                    TabIndex="2"></asp:TextBox>
                <asp:Button ID="btnAcCode" runat="server" Text="..." CssClass="btnHelp" OnClick="btnAcCode_Click"
                    Height="24px" Width="20px" />
                <asp:Label ID="lblAcCodeName" runat="server" CssClass="lblName"></asp:Label>
            </td>
        </tr>
          <tr>
            <td align="left" style="width: 40%;">
                <b>Brand Code</b>
            </td>
            <td align="left" colspan="5" style="width: 60%;">
                <asp:TextBox ID="txtbrandcode" runat="server" Width="80px" CssClass="txt" AutoPostBack="true"
                    OnTextChanged="txtbrandcode_TextChanged" onkeyDown="Bc_name(event);" Height="24px"
                    TabIndex="2"></asp:TextBox>
                <asp:Button ID="btnBrandcode" runat="server" Text="..." CssClass="btnHelp" OnClick="btnBrandcode_Click"
                    Height="24px" Width="20px" />
                <asp:Label ID="Label2" runat="server" CssClass="lblName"></asp:Label>
            </td>
        </tr>
         <tr>
            <td align="left" style="width: 40%;">
                <b>Pur Code</b>
            </td>
            <td align="left" colspan="5" style="width: 60%;">
                <asp:TextBox ID="txtpurno" runat="server" Width="80px" CssClass="txt" AutoPostBack="true" Enabled="false"
                    OnTextChanged="txtpurno_TextChanged" onkeyDown="Pc_name(event);" Height="24px"
                    TabIndex="2"></asp:TextBox>
                <asp:Button ID="btnpurno" runat="server" Text="..." CssClass="btnHelp" OnClick="btnpurno_Click"
                    Height="24px" Width="20px" />
                <asp:Label ID="Label3" runat="server" CssClass="lblName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                From Date:
            </td>
            <td align="left">
                <asp:TextBox ID="txtFromDt" runat="server" Width="80px" CssClass="txt" Height="24px"></asp:TextBox>
                <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                    Width="25px" Height="15px" />
                <ajax1:CalendarExtender ID="calenderExtendertxtFromDt" runat="server" TargetControlID="txtFromDt"
                    PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td align="left">
                To Date:
            </td>
            <td align="left">
                <asp:TextBox ID="txtToDt" runat="server" Width="80px" CssClass="txt" Height="24px"></asp:TextBox>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                    Height="15px" />
                <ajax1:CalendarExtender ID="CalendarExtendertxtToDt" runat="server" TargetControlID="txtToDt"
                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                </ajax1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button runat="server" ID="btnundiliveredreport" Text="Undeliverd Report" Height="24px"
                    Width="150px" Font-Bold="true" CssClass="btnHelp" OnClick="btnundiliveredreport_Click" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btndiliveredreport" Text="Deliverd Report" Height="24px"
                    Width="150px" Font-Bold="true" CssClass="btnHelp" OnClick="btndiliveredreport_Click" />&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" ID="btncashreceive" Text="Cash Receive Report" Height="24px"
                    Width="150px" Font-Bold="true" CssClass="btnHelp" OnClick="btncashreceive_Click" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btncashnotreceive" Text="Cash Not Receive Report"
                    Height="24px" Width="170px" Font-Bold="true" CssClass="btnHelp" OnClick="btncashnotreceive_Click" />&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnpurcCreditReport" Text="Credit  Report" Height="24px"
                    Width="150px" Font-Bold="true" CssClass="btnHelp" OnClick="btnpurcCreditReport_Click" />&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnstocksummarynew" Text="Stock Summary Report" Height="24px"
                    Width="150px" Font-Bold="true" CssClass="btnHelp" OnClick="btnstocksummarynew_Click" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btndetailsatockreport" Text=" Purcheswise Stock Detail"
                    Height="24px" Width="180px" Font-Bold="true" CssClass="btnHelp" OnClick="btndetailsatockreport_Click" />&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button runat="server" ID="btnMillWiseStockDeatil" Text="MillWise Stock Detail"
                    Height="24px" Width="180px" Font-Bold="true" CssClass="btnHelp" OnClick="btnMillWiseStockDeatil_Click"
                    Visible="true" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnItemwisestockdetail" Text=" BrandWise Stock Detail"
                    Height="24px" Width="180px" Font-Bold="true" CssClass="btnHelp" OnClick="btnItemwisestockdetail_Click"
                    Visible="true" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button runat="server" ID="btnReturnStockBook" Text="Return Stock Detail" Height="24px"
                    Width="150px" Font-Bold="true" CssClass="btnHelp" OnClick="btnReturnStockBook_Click"
                    Visible="false" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnReturnStockSummary" Text=" Return Stock Summary"
                    Height="24px" Width="150px" Font-Bold="true" CssClass="btnHelp" OnClick="btnReturnStockSummary_Click"
                    Visible="false" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button runat="server" ID="btnRetailStock" Text="Retail Stock Detail" Height="24px"
                    Width="150px" Font-Bold="true" CssClass="btnHelp" OnClick="btnRetailStock_Click"
                    Visible="false" />&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnonlyretailstock" Text="only Retail Stock Detail"
                    Height="24px" Width="150px" Font-Bold="true" CssClass="btnHelp" OnClick="btnonlyretailstock_Click"
                    Visible="false" />
            </td>
        </tr>
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
                        Width="250px" Height="20px" AutoPostBack="true" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                        CssClass="btnSubmit" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:Panel ID="pnlScroll" runat="server" Width="680px" ScrollBars="Both" Direction="LeftToRight" BackColor="#FFFFE4" style="z-index:5000 ;  float:right; max-height:380px; height:380px;">--%>
                    <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                        Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                        <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
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
</asp:Content>
