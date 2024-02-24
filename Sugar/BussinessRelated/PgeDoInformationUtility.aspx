<%@ Page Title="U-DO Info" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PgeDoInformationUtility.aspx.cs" Inherits="Sugar_BussinessRelated_PgeDoInformationUtility" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        table {
            border: 1px solid #ccc;
        }

            table th {
                background-color: #F7F7F7;
                color: #333;
                font-weight: bold;
                height: 30px;
                font-size: 18px;
                font-family: 'Times New Roman';
                text-align: center;
            }

            table th, table td {
                padding: 5px;
                border-color: #ccc;
                font-weight: bolder;
            }

        .Pager span {
            color: #333;
            background-color: #F7F7F7;
            font-weight: bold;
            text-align: center;
            display: inline-block;
            width: 50px;
            margin-right: 3px;
            line-height: 150%;
            border: 1px solid #ccc;
        }

        .Pager a {
            text-align: center;
            display: inline-block;
            width: 20px;
            border: 1px solid #ccc;
            color: #fff;
            color: #333;
            margin-right: 3px;
            line-height: 150%;
            text-decoration: none;
        }

        .highlight {
            background-color: #FFFFAF;
        }
        /*#gvCustomers th
          {
        background-color:;
        color:#ffffff;
         }*/
        #gvCustomers tr:nth-child(even) {
            background-color: #ffffff;
        }

        #gvCustomers tr:nth-child(odd) {
            /*background-color: #cccccc;*/
            background-color: lightblue;
        }

        #gvCustomers tr.MouseOver:hover {
            background-color: coral;
        }

        td {
            cursor: pointer;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JQuery/ASPSnippets_Pager.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //    $("#drpTrnType").change(function(){
        //    GetCustomers(1);
        //}
        $(function () {
            GetCustomers(1);
        });
        function functionToTriggerClick() {
            debugger;
            GetCustomers(parseInt(1));

        }

        $("[id*=txtSearch]").live("keyup", function () {
            GetCustomers(parseInt(1));
        });
        $(".Pager .page").live("click", function () {
            GetCustomers(parseInt($(this).attr('page')));
        });
        function SearchTerm() {
            return jQuery.trim($("[id*=txtSearch]").val());
        };
        var value = $("#<%=drpPagesize.ClientID %>").val();
        var value = $('#drpTrnType-options option:checked').val();
        //var value = $('#drpMailSend-options option:checked').val();
        //var value = $('#drpEinvoice-options option:checked').val();
        //var value = $('#drpPayment-options option:checked').val();
        var value = $('#drpOption-options option:checked').val();
        var value = $('#drpYESNo-options option:checked').val();



        function GetCustomers(pageIndex) {
            debugger;


            $.ajax({
                type: "POST",
                url: "../BussinessRelated/PgeDoInformationUtility.aspx/GetCustomers",
                data: '{searchTerm: "' + SearchTerm() + '", pageIndex: "' + pageIndex + '",Trantype: "' + $("#<%=drpTrnType.ClientID %>").val() + '",Trantype1: "'
                    + $("#<%=drpOption.ClientID %>").val() + '",Trantype2: "' + $("#<%=drpYESNo.ClientID %>").val()
                    + '",PageSize: "' + $("#<%=drpPagesize.ClientID %>").val() + '",Company_Code: "' +
                    '<%= Session["Company_Code"] %>' + '",year: "' + '<%= Session["year"] %>' + '",fromdate:"' + $("#<%=txtFromDate.ClientID %>").val() + '",todate:"' + $("#<%=txtToDate.ClientID %>").val() + '"}',

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });

        }
        var row;
        function OnSuccess(response) {
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var customers = xml.find("Customers");
            if (row == null) {
                row = $("[id*=gvCustomers] tr:last-child").clone(true);
            }
            $("[id*=gvCustomers] tr").not($("[id*=gvCustomers] tr:first-child")).remove();
            if (customers.length > 0) {
                $.each(customers, function () {
                    var customer = $(this);
                    $("td", row).eq(0).html($(this).find("doc_no").text());
                    $("td", row).eq(1).html($(this).find("doc_date").text());
                    $("td", row).eq(2).html($(this).find("purc_no").text());
                    $("td", row).eq(3).html($(this).find("purc_order").text());
                    $("td", row).eq(4).html($(this).find("millshortname").text());
                    $("td", row).eq(5).html($(this).find("quantal").text());
                    $("td", row).eq(6).html($(this).find("billtoshortname").text());
                    $("td", row).eq(7).html($(this).find("salebillcityname").text());
                    $("td", row).eq(8).html($(this).find("shiptoshortname").text());
                    $("td", row).eq(9).html($(this).find("shiptocityname").text());
                    $("td", row).eq(10).html($(this).find("sale_rate").text());
                    $("td", row).eq(11).html($(this).find("Tender_Commission").text());
                    $("td", row).eq(12).html($(this).find("desp_type").text());
                    $("td", row).eq(13).html($(this).find("truck_no").text());
                    $("td", row).eq(14).html($(this).find("SB_No").text());
                    $("td", row).eq(15).html($(this).find("EWay_Bill_No").text());
                    $("td", row).eq(16).html($(this).find("Delivery_Type").text());
                    $("td", row).eq(17).html($(this).find("transportshortname").text());
                    $("td", row).eq(18).html($(this).find("MM_Rate").text());
                    $("td", row).eq(19).html($(this).find("doid").text());

                    $("[id*=gvCustomers]").append(row);
                    row = $("[id*=gvCustomers] tr:last-child").clone(true);
                });
                var pager = xml.find("Pager");
                $(".Pager").ASPSnippets_Pager({
                    ActiveCssClass: "current",
                    PagerCssClass: "pager",
                    PageIndex: parseInt(pager.find("PageIndex").text()),
                    PageSize: parseInt(pager.find("PageSize").text()),
                    RecordCount: parseInt(pager.find("RecordCount").text())
                });



                $(".millname").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });
                $(".salebillname").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });
                $(".desp_type").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });

            } else {
                var empty_row = row.clone(true);
                $("td:first-child", empty_row).attr("colspan", $("td", row).length);
                $("td:first-child", empty_row).attr("align", "center");
                $("td:first-child", empty_row).html("No records found for the search criteria.");
                $("td", empty_row).not($("td:first-child", empty_row)).remove();
                $("[id*=gvCustomers]").append(empty_row);
            }
            $('#gvCustomers  tr').each(function () {
                debugger;
                var number = $(this).children('td:eq(3)').text();

                if (number == '0') {
                    $(this).children('td').css('background', 'red');
                }
            })
        };
    </script>

    <script type="text/javascript">

        $(document).ready(function () {
            debugger;
            //=================================================================
            //click on table body
            //$("#tableMain tbody tr").click(function () {
            $('#gvCustomers  tr').each(function () {
                debugger;
                var number = $(this).children('td:eq(1)').text();

                if (number == '1') {
                    $(this).children('td').css('background', 'red');
                }
            })


            $('#gvCustomers ').on('dblclick', 'tr', function () {
                debugger;
                //get row contents into an array
                var tableData = $(this).children("td").map(function () {
                    return $(this).text();
                }).get();
                var row_index = $(this).index();;
                var Action = 1;
                var DO = tableData[19];
                var Tran_type = tableData[12];
                if (row_index > 0) {
                    if (isNaN(DO)) {
                    }
                    else {
                        window.open('../BussinessRelated/pgeDoInformation.aspx?DO=' + DO + '&Action=' + Action);
                    }
                }
            });


        });
    </script>
    <script type="text/javascript">
        function DO() {
            debugger;
            var Action = 2;
            var DO = 0;
            window.open("../BussinessRelated/PgeDoInformationUtility.aspx", "_self")
            window.open('../BussinessRelated/pgeDoInformation.aspx?DO=' + DO + '&Action=' + Action);
        }

    </script>
    <script type="text/javascript">
        function Confirm() {
            debugger;

            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();

            today = dd + '/' + mm + '/' + yyyy;

            // window.open('../Report/rptDOInfo.aspx?date=' + today);
            // window.open("../BussinessRelated/PgeDoInformationUtility.aspx", "_self");

            var Doc_NO = "";
            var Action = 1;
            var fromdate = $("#<%=txtFromDate.ClientID %>").val();
            var todate = $("#<%=txtToDate.ClientID %>").val();

            window.open("../BussinessRelated/PgeDoInformationUtility.aspx", "_self")
            window.open('../Report/rptDOInfo.aspx?FromDate=' + fromdate + '&ToDate=' + todate);
            //window.open('../BussinessRelated/PgeDoInformationUtility.aspx?FromDate=' + fromdate + '&ToDate=' + todate);
        }

    </script>
    <script type="text/javascript">
        function stopEnterKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopEnterKey;
    </script>
    <script type="text/javascript">
        function functionToTriggerClick() {
            debugger;
            var drpval = $('#<%=drpPagesize.ClientID %> OPTION:selected').val();

            GetCustomers(1);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <br />
            <br />
            <asp:Label ID="lblUtilityName" runat="server" Text="DO Information" Font-Bold="True"
                ForeColor="#000080" Font-Size="X-Large"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Show" Font-Bold="True" ForeColor="#CC3300"
                Font-Size="Medium"></asp:Label>
            <asp:DropDownList ID="drpPagesize" runat="server" AutoPostBack="false" onchange="functionToTriggerClick();">
                <asp:ListItem Text="15" Value="15" Selected="True"></asp:ListItem>
                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                <asp:ListItem Text="100" Value="100"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label2" runat="server" Text="Entries" Font-Bold="True" ForeColor="#CC3300"
                Font-Size="Medium"></asp:Label>
            <asp:DropDownList ID="drpTrnType" runat="server" CssClass="ddl" Width="150px" Height="24px"
                AutoPostBack="false" onchange="functionToTriggerClick();">
                <asp:ListItem Text="Dispatch" Value="DI"></asp:ListItem>
                <asp:ListItem Text="DO" Value="DO"></asp:ListItem>
            </asp:DropDownList>
            <%-- &nbsp; &nbsp; &nbsp; &nbsp; 
            <asp:Label ID="Label4" runat="server" Text="Mail Send" Font-Bold="True" ForeColor="#CC3300"
                Font-Size="Medium"></asp:Label>
            <asp:DropDownList ID="drpMailSend" runat="server" CssClass="ddl" Width="70px" Height="24px"
                AutoPostBack="false" onchange="functionToTriggerClick();">
                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
            </asp:DropDownList>
            &nbsp; &nbsp; &nbsp; &nbsp; 
            <asp:Label ID="Label5" runat="server" Text="Einvoice Generated" Font-Bold="True"
                ForeColor="#CC3300" Font-Size="Medium"></asp:Label>
            <asp:DropDownList ID="drpEinvoice" runat="server" CssClass="ddl" Width="70px" Height="24px"
                AutoPostBack="false" onchange="functionToTriggerClick();">
                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
            </asp:DropDownList>
             &nbsp; &nbsp; &nbsp; &nbsp; 
            <asp:Label ID="Label5" runat="server" Text="Payment Received" Font-Bold="True" ForeColor="#CC3300"
                Font-Size="Medium"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ddl" Width="70px" Height="24px"
                AutoPostBack="false" onchange="functionToTriggerClick();">
                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
            </asp:DropDownList>
            &nbsp; &nbsp; &nbsp; &nbsp; --%>
            <asp:Label ID="Label4" runat="server" Text="Option" Font-Bold="True" ForeColor="#CC3300"
                Font-Size="Medium"></asp:Label>
            <asp:DropDownList ID="drpOption" runat="server" CssClass="ddl" Width="70px" Height="24px"
                AutoPostBack="false" onchange="functionToTriggerClick();">
                <asp:ListItem Text="Einvoice Generated" Value="E"></asp:ListItem>
                <asp:ListItem Text="Mail Send" Value="M"></asp:ListItem>
                 <asp:ListItem Text="Payment Received" Value="P"></asp:ListItem>
            </asp:DropDownList>
            &nbsp; &nbsp; &nbsp; &nbsp; 
            <asp:Label ID="Label6" runat="server" Text="YES No" Font-Bold="True" ForeColor="#CC3300"
                Font-Size="Medium"></asp:Label>
            <asp:DropDownList ID="drpYESNo" runat="server" CssClass="ddl" Width="70px" Height="24px"
                AutoPostBack="false" onchange="functionToTriggerClick();">
                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
            </asp:DropDownList>
            &nbsp; &nbsp; &nbsp; &nbsp; 
            <asp:Label ID="Label3" runat="server" Text=" Search:" Font-Bold="True" ForeColor="#CC3300"
                Font-Size="Medium"></asp:Label>
            <asp:TextBox ID="txtSearch" runat="server" Height="25px" />
            &nbsp; &nbsp; &nbsp; &nbsp; 
                 <tr>
                     <td>From Date:
                     </td>
                     <td align="left">
                         <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                             MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)" onchange="functionToTriggerClick();"></asp:TextBox>
                         <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                             Width="25px" Height="15px" />
                         <ajax1:CalendarExtender ID="calendertxtFromDate" runat="server" TargetControlID="txtFromDate"
                             PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                         </ajax1:CalendarExtender>
                         To Date:
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)" onchange="functionToTriggerClick();"></asp:TextBox>
                         <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                             Height="15px" />
                         <ajax1:CalendarExtender ID="CalendarExtendertxtToDate" runat="server" TargetControlID="txtToDate"
                             PopupButtonID="Image1" Format="dd/MM/yyyy">
                         </ajax1:CalendarExtender>
                     </td>
                 </tr>
            <asp:Button runat="server" ID="btnPrintSaleBill" Text="Report" CssClass="btnHelp"
                Width="100px" Height="24px" OnClientClick="Confirm()" />
            <%--<div style="max-width"> </div>--%>
            <%--<div id="popup" style="max-width:2000px;overflow-y:scroll;" >--%>
            <div id="popup" style="max-height: 600px; overflow-y: scroll;">
                <center>
                    &nbsp;
                    <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" PageSize="10"
                        Width="1700px" Height="60" RowStyle-CssClass="MouseOver" ClientIDMode="Static"
                        RowStyle-Height="30px">
                        <Columns>
                            <asp:BoundField HeaderStyle-Width="20px" DataField="doc_no" HeaderText="Doc No" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="doc_date" HeaderText="Doc Date" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="purc_no" HeaderText="Tender No" />
                            <asp:BoundField HeaderStyle-Width="10px" DataField="purc_order" HeaderText="TenderDetail ID" />
                            <asp:BoundField HeaderStyle-Width="150px" DataField="millshortname" HeaderText="Mill Name" />
                            <asp:BoundField HeaderStyle-Width="50px" DataField="quantal" HeaderText="Quantal" />
                            <asp:BoundField HeaderStyle-Width="170px" DataField="billtoshortname" HeaderText="Sale Bill Name" />
                            <asp:BoundField HeaderStyle-Width="30px" DataField="salebillcityname" HeaderText="SBCity" />
                            <asp:BoundField HeaderStyle-Width="105px" DataField="shiptoshortname" HeaderText="Ship To Name" />
                            <asp:BoundField HeaderStyle-Width="30px" DataField="shiptocityname" HeaderText="ShipToCity" />
                            <asp:BoundField HeaderStyle-Width="10px" DataField="sale_rate" HeaderText="Sale Rate" />
                            <asp:BoundField HeaderStyle-Width="10px" DataField="Tender_Commission" HeaderText="Commission    " />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="desp_type" HeaderText="Tran Type" />
                            <asp:BoundField HeaderStyle-Width="10px" DataField="truck_no" HeaderText="Lorry No" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="SB_No" HeaderText="SB No" />
                            <asp:BoundField HeaderStyle-Width="30px" DataField="EWay_Bill_No" HeaderText="EwayBill No" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="Delivery_Type" HeaderText="Delivery Type" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="transportshortname" HeaderText="Transport Name" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="MM_Rate" HeaderText="MemoAdvRate" />
                            <asp:BoundField HeaderStyle-Width="20" DataField="doid" HeaderText="DoID" />
                        </Columns>
                    </asp:GridView>
                </center>
                <br />
            </div>
            <div class="Pager">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
