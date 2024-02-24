<%@ Page Title="Check Printing Utility" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeCheckPrintingUtility.aspx.cs" Inherits="Foundman_Transaction_pgeCheckPrintingUtility" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                url: "../Transaction/pgeCheckPrintingUtility.aspx/GetCustomers",
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
                    $("td", row).eq(0).html($(this).find("Doc_No").text());
                    $("td", row).eq(1).html($(this).find("Doc_Date").text());
                    $("td", row).eq(2).html($(this).find("Cash_BankName").text());
                    $("td", row).eq(3).html($(this).find("Ac_Code_Name").text());
                    $("td", row).eq(4).html($(this).find("ChqCaption").text());
                    $("td", row).eq(5).html($(this).find("Amount").text());
                    $("td", row).eq(6).html($(this).find("Company_Code").text());
                    $("td", row).eq(7).html($(this).find("Year_Code").text());
                    $("td", row).eq(8).html($(this).find("Check_Id").text());

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

                $(".Doc_No").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });

                $(".Ac_Code_Name").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });

                $(".ChqCaption").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });

                $(".Amount").each(function () {
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
                var row_index = $(this).index(); ;
                var Action = 1;
                var Doc_No = tableData[0];
                var Tran_Type = "CQ";
                if (row_index > 0) {

                    if (isNaN(Doc_No)) {

                    }
                    else {
                        window.open('../Transaction/pgeCheckPrinting.aspx?Doc_No=' + Doc_No + '&Action=' + Action + '&Tran_Type=' + Tran_Type);
                    }

                }

            });


        });


    </script>
    <script type="text/javascript">
        function Accmaster() {
            var Action = 2;
            var Doc_No = 0;
            var Tran_Type = "CQ";
            window.open("../Transaction/pgeCheckPrintingUtility.aspx", "_self")
            window.open('../Transaction/pgeCheckPrinting.aspx?Doc_No=' + Doc_No + '&Action=' + Action + '&Tran_Type=' + Tran_Type);
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <br />
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
            <asp:Button ID="btnAdd" runat="server" Text="Add New" class="btnHelp" OnClientClick="Accmaster()"
                ValidationGroup="save" Width="90px" Height="24px" OnClick="btnAdd_Click" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Label ID="Label3" runat="server" Text="  Search:" Font-Bold="True" ForeColor="#CC3300"
                Font-Size="Medium"></asp:Label>
            <asp:TextBox ID="txtSearch" runat="server" Height="25px" AutoPostBack="false" />
            <div id="popup" style="max-height: 600px; overflow-y: scroll;">
                <%--<div>--%>
                <center>
                    &nbsp;
                    <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" PageSize="25"
                        AllowPaging="true" Width="1500px" Height="80" RowStyle-CssClass="MouseOver" ClientIDMode="Static"
                        RowStyle-Height="30px" HeaderStyle-CssClass="f">
                        <Columns>
                            <asp:BoundField HeaderStyle-Width="20px" DataField="Doc_No" HeaderText="Doc No"
                                ItemStyle-CssClass="Ac_Code" ControlStyle-BackColor="#ff0066" FooterStyle-BorderWidth="15"
                                FooterStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderStyle-Width="70px" DataField="Doc_Date" HeaderText="Doc Date"
                                AccessibleHeaderText="center" />
                            <asp:BoundField HeaderStyle-Width="100px" DataField="Cash_BankName" HeaderText="Cash_BankName " />
                            <asp:BoundField HeaderStyle-Width="150px" DataField="Ac_Code_Name" HeaderText="Ac_Code_Name " />
                            <asp:BoundField HeaderStyle-Width="70px" DataField="ChqCaption" HeaderText="ChqCaption" />
                              <asp:BoundField HeaderStyle-Width="70px" DataField="Amount" HeaderText="Amount" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="Company_Code" HeaderText="Company Code" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="Year_Code" HeaderText="Year Code" />
                            <asp:BoundField HeaderStyle-Width="20px" DataField="Check_Id" HeaderText="Check Id" />
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

