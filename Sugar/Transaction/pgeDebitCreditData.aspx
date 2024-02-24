<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeDebitCreditData.aspx.cs" Inherits="Sugar_Transaction_pgeDebitCreditData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" type="text/css" href="../CSS/jquery.dataTables.css">
    <script type="text/javascript" charset="utf8" src="../JS/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" charset="utf8" src="../JS/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="../CSS/bootstrap.min.css">
    <script src="../JS/bootstrap.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#datatable').dataTable();
        });


    </script>
    <script type="text/javascript">
        debugger;
        function Tender() {
            var Action = 2;
            var Utr_Id = 0;
            //alert(td);
            var tran_type = document.getElementById("<%= drpSub_Type.ClientID %>").value;
            window.open('../Transaction/pgeDebitCreditNote.aspx?utrid=' + Utr_Id + '&Action=' + Action + '&tran_type=' + tran_type);
        }

    </script>
    <script type="text/javascript">
        debugger;
        $(document).ready(function () {
            //=================================================================
            //click on table body
            //$("#tableMain tbody tr").click(function () {
            $('#datatable tbody').on('click', 'tr', function () {
                //get row contents into an array
                var tableData = $(this).children("td").map(function () {
                    return $(this).text();
                }).get();
                var td = tableData[0] + '*' + tableData[1] + '*' + tableData[2] + '*' + tableData[3] + '*' + tableData[4];
                var Action = 1;
                var Utr_Id = tableData[6];
                var tran_type = tableData[3];
                //alert(td);
                //                window.open('pgeBSgroupMaster.aspx?Ac_Code=' + Group_Code + '&Action=' + Action);
                window.open('../Transaction/pgeDebitCreditNote.aspx?dcid=' + Utr_Id + '&Action=' + Action + '&tran_type=' + tran_type);
            });


        });
    </script>
    <style>
        .bottom-two
        {
            margin-bottom: 1cm;
        }

        .scrollit
        {
            min-height: 90%;
            width: 50%;
            position: relative;
            top: 162%;
            overflow: scroll;
        }
    </style>

     <script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 60%;" align="center" cellpadding="4" cellspacing="4" class="bottom-two">
                </table>
                <table style="width: 60%;" align="center" cellpadding="4" cellspacing="4" class="bottom-two">
                    <tr>
                        <td>
                            <asp:DropDownList ID="drpSub_Type" runat="Server" CssClass="txt" TabIndex="1" Width="230px"
                                AutoPostBack="true" Height="30px">
                                 <asp:ListItem Text="Debit Note To Customer" Value="DN" />
                                <asp:ListItem Text="Credit Note To Customer" Value="CN" />
                                <asp:ListItem Text="GST Debit Note To Customer" Value="D1" />
                                <asp:ListItem Text="GST Credit Note To Customer" Value="C1" />
                                <asp:ListItem Text="GST Debit Note To Supplier" Value="D2" />
                                <asp:ListItem Text="GST Credit Note To Supplier" Value="C2" />
                            </asp:DropDownList>
                            &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" class="btnHelp" OnClientClick="Tender()"
                                ValidationGroup="save" Width="90px" Height="24px" />
                        </td>
                        <td>
                            <asp:Button ID="btnPrintGroup_Master" runat="server" Text="Group Master Print" class="btnHelp" OnClientClick="groupmasterPrint()"
                                ValidationGroup="save" Height="24px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container" style="width: 1600px;">
        <div class="table table-responsive" style="overflow: auto;">
            <table id="datatable" class="table table-striped table-bordered table-sm " width="1800px">
                <thead>
                    <tr>
                        <td class="danger">Sr No
                        </td>
                        <td class="danger">Doc No
                        </td>
                        <td class="danger">Doc Date
                        </td>
                        <td class="danger">Tran_Type
                        </td>
                        <td class="danger">Ac_Code
                        </td>
                        <td class="danger">Ac_Name
                        </td>
                        <td class="danger">ID
                        </td>


                    </tr>
                </thead>
                <tbody>
                    <%Response.Write(getData()); %>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>


