<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PgeEquityPurchaseSaleUtility.aspx.cs" Inherits="Sugar_Master_PgeEquityPurchaseSaleUtility" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

        .ct-active {
            color: blue;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JQuery/ASPSnippets_Pager.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        //setTimeout(function () {
        //    debugger;
        //    location.reload();

        //}, 5000);

    </script>
    <script type="text/javascript">

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
        //var val = $('#drpTrnType').val();
        function GetCustomers(pageIndex) {
            debugger;
            $.ajax({
                type: "POST",
                url: "../Master/PgeEquityPurchaseSaleUtility.aspx/GetCustomers",
                data: '{searchTerm: "' + SearchTerm() + '", pageIndex: "' + pageIndex + '",PageSize: "' + $("#<%=drpPagesize.ClientID %>").val() + '",Company_Code: "' + '<%= Session["Company_Code"] %>' + '",year: "' + '<%= Session["year"] %>' + '"}',
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
            debugger;

            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var customers = xml.find("Customers");

            if (row == null) {
                row = $("[id*=gvCustomers] tr:last-child").clone(true);
            }
            $("[id*=gvCustomers] tr").not($("[id*=gvCustomers] tr:first-child")).remove();
            if (customers.length > 0) {
                var a = $("td", row).eq(0).html($(this).find("Doc_No").text());
                $.each(customers, function () {
                    var customer = $(this);
                    $("td", row).eq(0).html($(this).find("Doc_No").text());
                    $("td", row).eq(1).html($(this).find("Date").text());
                    $("td", row).eq(2).html($(this).find("Script_Code").text());
                    $("td", row).eq(3).html($(this).find("Script_Name").text());
                    $("td", row).eq(4).html($(this).find("Tran_Type").text());
                    $("td", row).eq(5).html($(this).find("Qty").text());
                    $("td", row).eq(6).html($(this).find("Rate").text());
                    $("td", row).eq(7).html($(this).find("Ac_Name_E").text());



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

                $(".Script_Name").each(function () {
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
        };
    </script>
    <script type="text/javascript">
        function PurcSale() {
            var Action = 2;
            var Doc_No = 0;
            window.open("../Master/PgeEquityPurchaseSaleUtility.aspx", "_self")
            window.open('../Master/PgeEquityPurchaseSale.aspx?Doc_No=' + Doc_No + '&Action=' + Action);
        }

    </script>
    <script type="text/javascript">
        debugger;
        $(document).ready(function () {

            $('#gvCustomers').on('dblclick', 'tr', function () {
                debugger;
                //get row contents into an array
                var tableData = $(this).children("td").map(function () {
                    return $(this).text();
                }).get();
                var row_index = $(this).index();;
                var Action = 1;
                var Doc_No = tableData[0];

                if (row_index > 0) {

                    if (isNaN(Doc_No)) {

                    }
                    else {
                        window.open('../Master/PgeEquityPurchaseSale.aspx?Doc_No=' + Doc_No + '&Action=' + Action);
                    }

                }

            });


        });


    </script>
    <style>
        tr {
            cursor: pointer;
            transition: all .25s ease-in-out;
        }

        .selected {
            background-color: red;
            font-weight: bold;
            color: black;
        }
    </style>

    <script type="text/javascript">

        function down(e) {
            debugger;
            var index,
                table = document.getElementById("gvCustomers");

            for (var i = 1; i < table.rows.length; i++) {
                // table.rows[i].onclick = function () {
                // remove the background from the previous selected row
                //if (typeof index !== "undefined") {
                //    table.rows[index].classList.toggle("selected");
                //}
                console.log(typeof index);
                // get the selected row index
                index = this.rowIndex;
                // add class selected to the row

                //this.classList.toggle("selected");
                console.log(typeof index);
                //};
            }

        }
        selectedRow();
    </script>

    <script type="text/javascript">
        function stopEnterKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopEnterKey;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <br />
            <br />
            <br />
            <br />
            <div id="Auto">
                <asp:Label ID="Label1" runat="server" Text="Show" Font-Bold="True"
                    ForeColor="#CC3300" Font-Size="Medium"></asp:Label>
                <asp:DropDownList ID="drpPagesize" runat="server" AutoPostBack="false" onchange="functionToTriggerClick();">
                    <asp:ListItem Text="15" Value="15" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label2" runat="server" Text="Entries" Font-Bold="True"
                    ForeColor="#CC3300" Font-Size="Medium"></asp:Label>
                <asp:Button ID="btnAdd" runat="server" Text="Add New" class="btnHelp" OnClientClick="PurcSale()"
                    Width="90px" Height="24px" OnClick="btnAdd_Click" />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;   
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;   
             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 

           <asp:Label ID="Label3" runat="server" Text="Search:" Font-Bold="True"
               ForeColor="#CC3300" Font-Size="Medium"></asp:Label>
                <asp:TextBox ID="txtSearch" runat="server" Height="25px" onkeydown="down(event);" />


                <center>
                    &nbsp;
               
                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" PageSize="10" Width="1500px" Height="60"
                    RowStyle-CssClass="MouseOver" ClientIDMode="Static" RowStyle-Height="30px">

                    <Columns>
                        <asp:BoundField HeaderStyle-Width="20px" DataField="Doc_No" HeaderText="Doc No" />
                        <asp:BoundField HeaderStyle-Width="50px" DataField="Date" HeaderText="Date" AccessibleHeaderText="center" />
                        <asp:BoundField HeaderStyle-Width="50px" DataField="Script_Code" HeaderText="Script Code" />
                        <asp:BoundField HeaderStyle-Width="150px" DataField="Script_Name" HeaderText=" Script Name" />
                        <asp:BoundField HeaderStyle-Width="20px" DataField="Tran_Type" HeaderText="Tran Type" />
                        <asp:BoundField HeaderStyle-Width="50px" DataField="Qty" HeaderText="Qty" />
                        <asp:BoundField HeaderStyle-Width="50px" DataField="Rate" HeaderText="Rate" />
                        <asp:BoundField HeaderStyle-Width="150px" DataField="Ac_Name_E" HeaderText="Ac Name" />

                    </Columns>
                </asp:GridView>
                </center>
                <br />
                <div class="Pager">
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

