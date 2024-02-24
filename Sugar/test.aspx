<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Sugar_test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #tblDynamic td
        {
            border: 1px solid grey;
        }
        /*Style added to show the actual structure of table*/
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        function FetchData(e) {
            debugger;
            $.ajax({
                method: "POST",
                url: "test.aspx/GetDynamicRows",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    debugger;
                    var jsonObj = data.d; //For storing the response data in jsonObj variable.
                    var strHTML = '<tr><th>ID</th><th>Name</th></tr>';
                    $(jsonObj).each(function () {
                        var row = $(this)[0]; //Extracting out row object one by one.
                        strHTML += '<tr><td>' + row["Id"] + '</td><td>' + row["name"] + '</td></tr>'; //For creating the html part of the table
                    });
                    $('#tblDynamic').html(strHTML);//To append the html part into the table               
                }
               
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="80px" onkeydown="FetchData(event);"
        Height="25px" TabIndex="35" />
    <div>
        <table id="tblDynamic" border="1"></table>
    </div>

</asp:Content>

