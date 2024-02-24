<%@ Page Language="C#" Title="U-Carporate Sale" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="PgeCarporatesaleUtility.aspx.cs" Inherits="Sugar_Master_PgeCarporatesaleUtility" %>



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
        //var val = $('#drpTrnType').val();
        function GetCustomers(pageIndex) {
            debugger;
            $.ajax({
                type: "POST",
                url: "../BussinessRelated/PgeCarporatesaleUtility.aspx/GetCustomers",
                data: '{searchTerm: "' + SearchTerm() + '", pageIndex: "' + pageIndex + '",PageSize: "' + $("#<%=drpPagesize.ClientID %>").val() + '",Company_Code: "' + '<%= Session["Company_Code"] %>' + '"}',
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
                    $("td", row).eq(2).html($(this).find("carporatepartyaccountname").text());
                    $("td", row).eq(3).html($(this).find("carporatepartyunitname").text());
                    $("td", row).eq(4).html($(this).find("carporatepartybrokername").text());
                    $("td", row).eq(5).html($(this).find("pono").text());
                    $("td", row).eq(6).html($(this).find("quantal").text());
                    $("td", row).eq(7).html($(this).find("sell_rate").text());
                    $("td", row).eq(8).html($(this).find("remark").text());
                    $("td", row).eq(9).html($(this).find("carpid").text());
                    $("td", row).eq(10).html($(this).find("selling_type").text());
                    $("td", row).eq(11).html($(this).find("DeliveryType").text());


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

                $(".carporatepartyaccountname").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });

                $(".carporatepartyunitname").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });

                $(".carporatepartybrokername").each(function () {
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
        debugger;
        $(document).ready(function () {
            //=================================================================
            //click on table body
            //$("#tableMain tbody tr").click(function () {
            $('#gvCustomers ').on('dblclick', 'tr', function () {
                //get row contents into an array
                var tableData = $(this).children("td").map(function () {
                    return $(this).text();
                }).get();
                var row_index = $(this).index();;
                var Action = 1;
                var Ac_Code = tableData[9];
                if (row_index > 0) {
                    if (isNaN(Ac_Code)) {
                    }
                    else {
                        window.open('../BussinessRelated/pgeCarporatesell.aspx?carpid=' + Ac_Code + '&Action=' + Action);
                    }
                }
            });


        });
    </script>
    <script type="text/javascript">
        function Accmaster() {
            var Action = 2;
            var Ac_Code = 0;
            window.open("../BussinessRelated/PgeCarporatesaleUtility.aspx", "_self")
            window.open('../BussinessRelated/pgeCarporatesell.aspx?doc_no=' + Ac_Code + '&Action=' + Action);
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <br />
            <br />
            <br />
            <br />

            <div>
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
                <asp:Button ID="btnAdd" runat="server" Text="Add New" class="btnHelp" OnClientClick="Accmaster()"
                    ValidationGroup="save" Width="90px" Height="24px" OnClick="btnAdd_Click" />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;    
                 <asp:Label ID="lblUtilityName" runat="server" Text="Carporate Sale" Font-Bold="True"
                     ForeColor="#000080" Font-Size="X-Large"></asp:Label>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
          
            <asp:Label ID="Label3" runat="server" Text="Search:" Font-Bold="True"
                ForeColor="#CC3300" Font-Size="Medium"></asp:Label>
                <asp:TextBox ID="txtSearch" runat="server" Height="25px" />

                <center>
                    &nbsp;
              
                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" PageSize="10" Width="1500px" Height="60"
                    RowStyle-CssClass="MouseOver" ClientIDMode="Static" RowStyle-Height="30px">

                    <Columns>
                        <asp:BoundField HeaderStyle-Width="50px" DataField="doc_no" HeaderText="Doc No"
                            ItemStyle-CssClass="Ac_Code" ControlStyle-BackColor="#ff0066" FooterStyle-BorderWidth="15" FooterStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderStyle-Width="20px" DataField="doc_date" HeaderText="Doc Date" AccessibleHeaderText="center" />
                        <asp:BoundField HeaderStyle-Width="100px" DataField="carporatepartyaccountname" HeaderText="Carporate Party Acc Name" />
                        <asp:BoundField HeaderStyle-Width="100px" DataField="carporatepartyunitname" HeaderText="Carporate Party Unit Name" />
                        <asp:BoundField HeaderStyle-Width="100px" DataField="carporatepartybrokername" HeaderText="Carporate Party Broker Name" />
                        <asp:BoundField HeaderStyle-Width="20px" DataField="pono" HeaderText="Po No" />
                        <asp:BoundField HeaderStyle-Width="20px" DataField="quantal" HeaderText="Quantal" />
                        <asp:BoundField HeaderStyle-Width="20px" DataField="sell_rate" HeaderText="Sell Rate" />
                        <asp:BoundField HeaderStyle-Width="20px" DataField="remark" HeaderText=" Narration" />
                        <asp:BoundField HeaderStyle-Width="20px" DataField="carpid" HeaderText="CareId" />
                        <asp:BoundField HeaderStyle-Width="20px" DataField="selling_type" HeaderText="Selling Type" />
                        <asp:BoundField HeaderStyle-Width="20px" DataField="DeliveryType" HeaderText="Delivery Type" />
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

