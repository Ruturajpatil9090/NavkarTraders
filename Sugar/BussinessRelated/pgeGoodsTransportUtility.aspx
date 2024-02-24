<%@ Page Title="Goods Transport" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeGoodsTransportUtility.aspx.cs" Inherits="Sugar_BussinessRelated_pgeGoodsTransportUtility" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }

        table
        {
            border: 1px solid #ccc;
        }

            table th
            {
                background-color: #F7F7F7;
                color: #333;
                font-weight: bold;
                height: 30px;
                font-size: 18px;
                font-family: 'Times New Roman';
                text-align: center;
            }

            table th, table td
            {
                padding: 5px;
                border-color: #ccc;
                font-weight: bolder;
            }

        .Pager span
        {
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

        .Pager a
        {
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

        .highlight
        {
            background-color: #FFFFAF;
        }
        /*#gvCustomers th
          {
        background-color:;
        color:#ffffff;
         }*/
        #gvCustomers tr:nth-child(even)
        {
            background-color: #ffffff;
        }

        #gvCustomers tr:nth-child(odd)
        {
            /*background-color: #cccccc;*/
            background-color: lightblue;
        }

        #gvCustomers tr.MouseOver:hover
        {
            background-color: coral;
        }

        td
        {
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
        //var value = $('#drpdisp-options option:checked').val();
        //var val = $('#drpTrnType').val();

        function GetCustomers(pageIndex) {
            debugger;


            $.ajax({
                type: "POST",
                url: "../BussinessRelated/pgeGoodsTransportUtility.aspx/GetCustomers",
                data: '{searchTerm: "' + SearchTerm() + '", pageIndex: "' + pageIndex + '",Trantype: "' + $("#<%=drpTrnType.ClientID %>").val() + '",PageSize: "' + $("#<%=drpPagesize.ClientID %>").val() + '",Company_Code: "' + '<%= Session["Company_Code"] %>' + '",year: "' + '<%= Session["year"] %>' + '"}',
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
                    $("td", row).eq(1).html($(this).find("Do_DATE").text());
                    $("td", row).eq(2).html($(this).find("doc_date").text());
                    $("td", row).eq(3).html($(this).find("driver_no").text());
                    $("td", row).eq(4).html($(this).find("truck_no").text()); 
                    $("td", row).eq(5).html($(this).find("millshortname").text());
                    $("td", row).eq(6).html($(this).find("grade").text());
                    $("td", row).eq(7).html($(this).find("quantal").text());
                    $("td", row).eq(8).html($(this).find("billtoshortname").text());
                    $("td", row).eq(9).html($(this).find("Freight_Amount ").text());
                    $("td", row).eq(10).html($(this).find("memo_advance").text());
                    $("td", row).eq(11).html($(this).find("transportshortname").text());
                    $("td", row).eq(12).html($(this).find("reached_date").text());
                    $("td", row).eq(13).html($(this).find("EWay_Bill_No").text()); 
                    $("td", row).eq(14).html($(this).find("doid").text());

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
              var trantype = $("#<%=drpTrnType.ClientID %>").val();
              window.open("../BussinessRelated/pgeGoodsTransportUtility.aspx", "_self")
              window.open('../Report/rptGoodsTransport.aspx?FromDate=' + fromdate + '&ToDate=' + todate + '&trantype=' + trantype);
            //window.open('../BussinessRelated/PgeDoInformationUtility.aspx?FromDate=' + fromdate + '&ToDate=' + todate);
        }

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
                var DO = tableData[14];
                var Tran_type = tableData[12];
                if (row_index > 0) {
                    if (isNaN(DO)) {
                    }
                    else {
                        window.open('../BussinessRelated/pgeGoodsTransport.aspx?DO=' + DO + '&Action=' + Action);
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
            window.open("../BussinessRelated/PgeDoHeadUtility.aspx", "_self")
            window.open('../BussinessRelated/pgeGoodsTransport.aspx?DO=' + DO + '&Action=' + Action);
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
            <br />
            <br />
            <asp:Label ID="lblUtilityName" runat="server" Text="Goods Transport" Font-Bold="True"
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
            <asp:Label ID="Label2" runat="server" Text="Reached" Font-Bold="True" ForeColor="#CC3300"
                Font-Size="Medium"></asp:Label>
            <asp:DropDownList ID="drpTrnType" runat="server" CssClass="ddl" Width="200px" Height="24px"
                AutoPostBack="false" onchange="functionToTriggerClick();">
                <asp:ListItem Text="All" Value="AI"></asp:ListItem>
                <asp:ListItem Text="Reached" Value="Y"></asp:ListItem>
                <asp:ListItem Text="UnReached" Value="N"></asp:ListItem>
                <asp:ListItem Text="UnReached & No SB" Value="US"></asp:ListItem>
            </asp:DropDownList>
            &nbsp; &nbsp; &nbsp; &nbsp;     
             
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;          
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;      
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;           
        
            <asp:Label ID="Label3" runat="server" Text=" Search:" Font-Bold="True" ForeColor="#CC3300"
                Font-Size="Medium"></asp:Label>
            <asp:TextBox ID="txtSearch" runat="server" Height="25px" />
            <%--<div style="max-width"> </div>--%>
            <%--<div id="popup" style="max-width:2000px;overflow-y:scroll;" >--%>
              <tr>
                     <td>From Date:
                     </td>
                     <td align="left">
                         <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                             MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                         <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                             Width="25px" Height="15px" />
                         <ajax1:CalendarExtender ID="calendertxtFromDate" runat="server" TargetControlID="txtFromDate"
                             PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                         </ajax1:CalendarExtender>
                         To Date:
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" Width="80px" Height="24px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                         <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                             Height="15px" />
                         <ajax1:CalendarExtender ID="CalendarExtendertxtToDate" runat="server" TargetControlID="txtToDate"
                             PopupButtonID="Image1" Format="dd/MM/yyyy">
                         </ajax1:CalendarExtender>
                     </td>
                 </tr>
            <asp:Button runat="server" ID="btnPrintSaleBill" Text="Report" CssClass="btnHelp"
                Width="100px" Height="24px" OnClientClick="Confirm()" />
            <div id="popup" style="max-height: 600px; overflow-y: scroll;">
                <center>
                    &nbsp;
                    <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" PageSize="10"
                        Width="1700px" Height="60" RowStyle-CssClass="MouseOver" ClientIDMode="Static"
                        RowStyle-Height="30px">
                        <Columns>
                            <asp:BoundField HeaderStyle-Width="20px" DataField="doc_no" HeaderText="Do No" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="Do_DATE" HeaderText="Do Date" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="doc_date" HeaderText="Lifting Date" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="driver_no" HeaderText="Driver No" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="truck_no" HeaderText="Vehicle No" /> 
                            <asp:BoundField HeaderStyle-Width="100px" DataField="millshortname" HeaderText="Mill Name" />
                            <asp:BoundField HeaderStyle-Width="120px" DataField="grade" HeaderText="Grade" />
                            <asp:BoundField HeaderStyle-Width="50px" DataField="quantal" HeaderText="Quantal" />
                            <asp:BoundField HeaderStyle-Width="50px" DataField="billtoshortname" HeaderText="Sale Bill Name" />
                            <asp:BoundField HeaderStyle-Width="50px" DataField="Freight_Amount" HeaderText="Freight" />
                            <asp:BoundField HeaderStyle-Width="50px" DataField="memo_advance" HeaderText="Advance" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="transportshortname" HeaderText="Transport Name" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="reached_date" HeaderText="Reached Date" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="EWay_Bill_No" HeaderText="EwayBill No" />
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
