<%@ Page Language="C#" Title="U-Account Master" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="pgeAccountUtility.aspx.cs" Inherits="Sugar_Master_pgeAccountUtility" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
            overflow-x: auto;
            overflow-y: hidden;
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
                height: 35px;
                font-size: 18px;
                font-family: 'Times New Roman';
                text-align: center;
            }

            table th, table td
            {
                padding: 0px;
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

        #gvCustomers
        {
            margin-left: auto;
            margin-right: auto;
            width: 200px;
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





    <script type="text/javascript" lang="js">

        $(function () {

            $("#<%=txtSearch.ClientID %>").keydown(function () {
                debugger;

                if (event.which == 40) {
                    debugger;
                    $("#gvCustomers").on(function () {
                        // get the current row
                        var currentRow = $(this).closest("tr");

                        var col1 = currentRow.find("td:eq(0)").text(); // get current row 1st TD value
                        var col2 = currentRow.find("td:eq(1)").text(); // get current row 2nd TD
                        var col3 = currentRow.find("td:eq(2)").text(); // get current row 3rd TD
                        var data = col1 + "\n" + col2 + "\n" + col3;

                        alert(data);
                    });

                }

            });

        });


    </script>




    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JQuery/ASPSnippets_Pager.min.js" type="text/javascript"></script>


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

        function GetCustomers(pageIndex) {
            debugger;
            $.ajax({
                type: "POST",
                url: "../Master/pgeAccountUtility.aspx/GetCustomers",
                data: '{searchTerm: "' + SearchTerm() + '", pageIndex: "' + pageIndex + '",Trantype: "' + $("#<%=drpTrnType.ClientID %>").val() + '",PageSize: "' + $("#<%=drpPagesize.ClientID %>").val() + '",Company_Code: "' + '<%= Session["Company_Code"] %>' + '"}',
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
                    $("td", row).eq(0).html($(this).find("Ac_Code").text());
                    $("td", row).eq(1).html($(this).find("Ac_type").text());
                    $("td", row).eq(2).html($(this).find("Ac_Name_E").text());
                    $("td", row).eq(3).html($(this).find("Short_Name").text());
                    $("td", row).eq(4).html($(this).find("Commission").text());
                    $("td", row).eq(5).html($(this).find("Address_E").text());
                    $("td", row).eq(6).html($(this).find("city_name_e").text());
                    $("td", row).eq(7).html($(this).find("GstStateCode").text());
                    $("td", row).eq(8).html($(this).find("Gst_No").text());
                    $("td", row).eq(9).html($(this).find("AC_Pan").text());
                    $("td", row).eq(10).html($(this).find("FSSAI").text());
                    $("td", row).eq(11).html($(this).find("adhar_no").text());
                    $("td", row).eq(12).html($(this).find("Mobile_No").text());
                    $("td", row).eq(13).html($(this).find("accoid").text());


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

                $(".Ac_Name_E").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });

                $(".cityname").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });

                $(".group_Name_E").each(function () {
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

        $(document).ready(function () {

            $('#gvCustomers').on('dblclick', 'tr', function () {
                debugger;
                var tableData = $(this).children("td").map(function () {
                    return $(this).text();
                }).get();
                var row_index = $(this).index();;
                var Action = 1;
                var Ac_Code = tableData[13];

                if (row_index > 0) {

                    if (isNaN(Ac_Code)) {

                    }
                    else {
                        window.open('../Master/pgeAccountsmaster.aspx?Ac_Code=' + Ac_Code + '&Action=' + Action);
                    }

                }

            });


        });


    </script>

    <script type="text/javascript">
        function Accmaster() {
            var Action = 2;
            var Ac_Code = 0;
            window.open("../Master/pgeAccountUtility.aspx", "_self")
            window.open('../Master/pgeAccountsmaster.aspx?Ac_Code=' + Ac_Code + '&Action=' + Action);
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
        $(function () {

            $("body").mousewheel(function (event, delta) {

                this.scrollLeft -= (delta * 30);

                event.preventDefault();

            });

        });
    </script>
    <%-- <script type="text/javascript">
        setTimeout(function () {
            location.reload();
        }, 5000);
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <br />
            <br />
            <br />
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

            <asp:DropDownList ID="drpTrnType" runat="server" CssClass="ddl"
                Height="25px" TabIndex="1" Width="200px" onchange="functionToTriggerClick();">
                <asp:ListItem Selected="True" Text="All" Value="A"></asp:ListItem>
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

            <asp:Button ID="btnAdd" runat="server" Text="Add New" class="btnHelp" OnClientClick="Accmaster()"
                ValidationGroup="save" Width="90px" Height="24px" OnClick="btnAdd_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;          
            <asp:Label ID="lblUtilityName" runat="server" Text="Account Master" Font-Bold="True"
                ForeColor="#000080" Font-Size="X-Large"></asp:Label>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;           
                <asp:Label ID="Label3" runat="server" Text="  Search:" Font-Bold="True"
                    ForeColor="#CC3300" Font-Size="Medium"></asp:Label>
            <asp:TextBox ID="txtSearch" runat="server" Height="25px" AutoPostBack="false" />
            <div id="popup" style="max-height: 600px; overflow-y: scroll;">
                <%--<div>--%>

                <center>
                    &nbsp;
                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" PageSize="25" AllowPaging="true" Width="1700px" Height="80"
                    RowStyle-CssClass="MouseOver" ClientIDMode="Static" RowStyle-Height="30px" HeaderStyle-CssClass="f">

                    <Columns>

                        <asp:BoundField HeaderStyle-Width="20px" DataField="Ac_Code" HeaderText="Acc Code"
                            ItemStyle-CssClass="Ac_Code" ControlStyle-BackColor="#ff0066" FooterStyle-BorderWidth="15" FooterStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderStyle-Width="10px" DataField="Ac_type" HeaderText="Ac type" />
                        <asp:BoundField HeaderStyle-Width="150px" DataField="Ac_Name_E" HeaderText="Account Name" AccessibleHeaderText="center" />
                        <asp:BoundField HeaderStyle-Width="70px" DataField="Short_Name" HeaderText="Short Name" />
                           <asp:BoundField HeaderStyle-Width="20px" DataField="Commission" HeaderText="Commission" />
                        <asp:BoundField HeaderStyle-Width="200px" DataField="Address_E" HeaderText="Address" />
                        <asp:BoundField HeaderStyle-Width="70px" DataField="city_name_e" HeaderText="City Name" />
                        <asp:BoundField HeaderStyle-Width="20px" DataField="GstStateCode" HeaderText="State Code" />
                        <asp:BoundField HeaderStyle-Width="50px" DataField="Gst_No" HeaderText="GST No" />
                        <asp:BoundField HeaderStyle-Width="50px" DataField="AC_Pan" HeaderText="Pan" />
                        <asp:BoundField HeaderStyle-Width="50px" DataField="FSSAI" HeaderText="FSSAI" />
                        <asp:BoundField HeaderStyle-Width="50px" DataField="adhar_no" HeaderText="Adhar no" />
                        <asp:BoundField HeaderStyle-Width="50px" DataField="Mobile_No" HeaderText="Mobile No" />
                        <asp:BoundField HeaderStyle-Width="20px" DataField="accoid" HeaderText="Ac Id" />
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
