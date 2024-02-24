<%@ Page Title="DO Info" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeDoInformation.aspx.cs" Inherits="Sugar_BussinessRelated_pgeDoInformation" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Script/jquery-1.4.2.js"></script>

    <link href="../../CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../../JQuery/jquery-1.4.1.js" type="text/javascript"></script>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../../JQuery/jquery.keynavigation.js" type="text/javascript"></script>
    <link type="text/css" href="../../menu/menu.css" rel="stylesheet" />
    <script type="text/javascript" src="../../menu/jquery.js"></script>
    <script type="text/javascript" src="../../menu/menu.js"></script>


    <script src="../../JS/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../Scripts/selectfirstrow.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        debugger;
        document.addEventListener('keyup', function (event) {
            if (event.defaultPrevented) {
                return;
            }

            var key = event.key || event.keyCode;

            if (key === 'Escape' || key === 'Esc' || key === 27) {
                //                doWhateverYouWantNowThatYourKeyWasHit();
                debugger;
                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
    </script>
       <script type="text/javascript">
        function Transfer() {
            debugger;
            $("#loader").show();
            var doc_no = document.getElementById("<%= txtdoc_no.ClientID %>").value;
            var Autoid = document.getElementById("<%= hdnflvid.ClientID %>").value;
            doid = document.getElementById("<%= hdnfDoid.ClientID %>").value;

            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';

            var XML = "<ROOT><DoHead doc_no='" + doc_no + "' doid='" + doid + "' Company_Code='" + Company_Code + "' " +
                     "Year_Code='" + Year_Code + "'></DoHead></ROOT>";
            var spname = "DeliveryOrder";
            var status = "Transfer";
            ProcessXML(XML, status, spname);
        }

    </script>
    <script type="text/javascript" language="javascript">
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;
        function SelectSibling(e) {
            debugger;
            var e = e ? e : window.event;
            var KeyCode = e.which ? e.which : e.keyCode;
            if (KeyCode == 40) {
                SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);
            }
            else if (KeyCode == 38) {
                SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);
            }
            else if (KeyCode == 13) {
                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                var grid = document.getElementById("<%= grdPopup.ClientID %>");
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";

                var pageCount = document.getElementById("<%= hdHelpPageCount.ClientID %>").value;


                pageCount = parseInt(pageCount);
                if (pageCount > 1) {
                    SelectedRowIndex = SelectedRowIndex + 1;
                }
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
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete data and all Vouchers?")) {
                confirm_value.value = "Yes";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            }
            else {
                confirm_value.value = "No";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function Back() {
            window.open('../BussinessRelated/PgeDoInformationUtility.aspx', '_self');
        }

        function DoOPen(DO) {
            var Action = 1;
            window.open('../BussinessRelated/pgeDoInformation.aspx?DO=' + DO + '&Action=' + Action, "_self");
        }
    </script>

    <script type="text/javascript">
        function pagevalidation() {
            debugger;
            try {
                $("#loader").show();
                var doc_no = 0, doid = 0;
                var status = document.getElementById("<%= btnUpdate.ClientID %>").value;
                var spname = "DOInfo";
                var XML = "<ROOT>";
                if (status == "Update") {
                    doc_no = document.getElementById("<%= hdnfDodoc.ClientID %>").value;
                    doid = document.getElementById("<%= hdnfDoid.ClientID %>").value;
                   
                }
                
                var d = $("#<%=txtDOC_DATE.ClientID %>").val();
                var Doc_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

                var MILLNAME = $("#<%=txtmillname.ClientID %>").val();
                var BILLTO = $("#<%=txtbillto.ClientID %>").val();
                var SHIPTO = $("#<%=txtshipto.ClientID %>").val();
                var QUANTAL = $("#<%=txtqntl.ClientID  %>").val() == "" ? 0 : $("#<%=txtqntl.ClientID %>").val();
                var GRADE = $("#<%=txtgrade.ClientID %>").val();
                var TRUCK_NO = $("#<%=txtvehicle.ClientID %>").val();
                var MAILSEND = $("#<%=drpmailsend.ClientID %>").val();
                var EINVOICE = $("#<%=drpeinvoice.ClientID %>").val();
                var PAYMENT = $("#<%=drppayment.ClientID %>").val();
                var NARRATION1 = $("#<%=txtnarration.ClientID %>").val();

                var USER = '<%= Session["user"] %>';
                var Branch_Id = '<%= Session["Branch_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';
                var Company_Code = '<%= Session["Company_Code"] %>';

                debugger;

                XML = XML + "<DoHead doc_no='" + doc_no + "' doid='" + doid + "' truck_no='" + TRUCK_NO + "' MailSend='" + MAILSEND + "' ISEInvoice='" + EINVOICE + "' IsPayment='" + PAYMENT + "' narration1='" + NARRATION1 + "' " +
                   "Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "'>";
           
                XML = XML + "</DoHead></ROOT>";
                ProcessXML(XML, status, spname);
            }
            catch (exx) {
                $("#loader").hide();
                alert(exx)
            }
        }

        function ProcessXML(XML, status, spname) {
            debugger;

            $.ajax({
                type: "POST",
                url: "../xmlExecuteDMLQry.asmx/ExecuteXMLQry",
                data: '{XML:"' + XML + '",status:"' + status + '",spname:"' + spname + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                    $("#loader").hide();
                },
                error: function (response) {
                    alert(response.d);
                    $("#loader").hide();
                }
            });


            function OnSuccess(response) {
                debugger;
                $("#loader").hide();
                if (status != "Delete") {
                    if (response.d.length > 0) {
                        var word = response.d;
                        var len = word.length;
                        var pos = word.indexOf(",");
                        var id = word.slice(0, pos);
                        var doc = word.slice(pos + 1, len);
                        if (status == "Update") {
                            alert('Sucessfully Updated Record !!! Doc_no=' + doc)
                        }

                        window.open('../BussinessRelated/pgeDoInformation.aspx?DO=' + id + '&Action=1', "_self");

                    }
                }
                else {
                    var num = parseInt(response.d);

                    if (isNaN(num)) {
                        alert(response.d)

                    }
                    else {
                        window.open('../BussinessRelated/PgeDoInformationUtility.aspx', "_self");
                    }
                }

            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
        <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
            <legend style="text-align: center;">
                <asp:Label ID="label1" runat="server" Text="   DO Info   " Font-Names="verdana"
                    ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
        </fieldset>
        <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px;">
            <img height="20%" width="20%" src="../Images/ajax-loader3.gif" />
        </div>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hdconfirm" runat="server" />
                <asp:HiddenField ID="hdnfClosePopup" runat="server" />
                <asp:HiddenField ID="hdnf" runat="server" />
                <asp:HiddenField ID="hdnfSuffix" runat="server" />
                <asp:HiddenField ID="hdHelpPageCount" runat="server" />
                <asp:HiddenField ID="hdnfdocno" runat="server" />
                <asp:HiddenField ID="hdnfbankcode" runat="server" />
                <asp:HiddenField ID="hdnfpayto" runat="server" />
                <asp:HiddenField ID="hdnfyearcode" runat="server" />
                <asp:HiddenField ID="hdnfcompanycode" runat="server" />
                <asp:HiddenField ID="hdnflvdoc" runat="server" />
                <asp:HiddenField ID="hdnflvid" runat="server" />
                <asp:HiddenField ID="hdnfAcode" runat="server" />
                <asp:HiddenField ID="hdnfDodoc" runat="server" />
                <asp:HiddenField ID="hdnfDoid" runat="server" />
                <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                    Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;" BackColor="Yellow">
                    <table style="width: 100%;" align="left" cellpadding="2" cellspacing="5">
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                    Font-Size="Small" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <%--<tr>
                            <td align="right" style="width: 30%;">Change No:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                    TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender runat="server" ID="txtxf" FilterType="Numbers" InvalidChars="."
                                    TargetControlID="txtEditDoc_No">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                    Font-Size="Small" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="right" style="width: 30%;">DocNo:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtdoc_no" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                    Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtdoc_no_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtDOC_NO" runat="server" Text="..." Width="80px" OnClick="btntxtDOC_NO_Click" Visible="false"
                                    CssClass="btnHelp" Height="24px" />
                            </td>                   
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">DocDate:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtDOC_DATE" runat="Server" CssClass="txt" TabIndex="2" Width="90px"
                                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                    AutoPostBack="True" OnTextChanged="txtDOC_DATE_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                    Width="25px" Height="15px" />
                                <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtDOC_DATE"
                                    PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">Mill Name:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtmillname" runat="Server" CssClass="txt" TabIndex="3" Width="350px"
                                    AutoPostBack="false" Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">Bill To:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtbillto" runat="Server" CssClass="txt" TabIndex="4" Width="350px"
                                    AutoPostBack="false" Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">Ship To:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtshipto" runat="Server" CssClass="txt" TabIndex="5" Width="350px"
                                    AutoPostBack="True" Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">Qntl:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtqntl" runat="Server" CssClass="txt" TabIndex="5" Width="120px"
                                    Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                                Grade:                          
                                <asp:TextBox ID="txtgrade" runat="Server" CssClass="txt" TabIndex="5" Width="170px"
                                    AutoPostBack="True" Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">Vehicle No:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtvehicle" runat="Server" CssClass="txt" TabIndex="5" Width="120px"
                                    AutoPostBack="True" Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">Mail Send:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:DropDownList ID="drpmailsend" runat="server" AutoPostBack="true" Width="120px"
                                    CssClass="ddl" OnSelectedIndexChanged="drpmailsend_SelectedIndexChanged" TabIndex="3">
                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">Einvoice Generated By Mill:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:DropDownList ID="drpeinvoice" runat="server" AutoPostBack="true" Width="120px"
                                    CssClass="ddl" OnSelectedIndexChanged="drpeinvoice_SelectedIndexChanged" TabIndex="3">
                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">Payment Received By Party:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:DropDownList ID="drppayment" runat="server" AutoPostBack="true" Width="120px"
                                    CssClass="ddl" OnSelectedIndexChanged="drppayment_SelectedIndexChanged" TabIndex="3">
                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%;">Narration:
                            </td>
                            <td align="left" style="width: 30%;">
                                <asp:TextBox ID="txtnarration" runat="Server" CssClass="txt" TabIndex="6" Width="350px"
                                   TextMode="MultiLine" AutoPostBack="True" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table width="80%" align="left">
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnHelp" Width="90px"
                                    TabIndex="70" ValidationGroup="add" OnClick="btnUpdate_Click" UseSubmitBehavior="false" Height="24px" />
                                &nbsp;                               
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                                    ValidationGroup="save" Height="24px" OnClientClick="Back();" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <br />
                <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" autosize="true"
                    Width="80%" align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
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
                                <asp:Panel ID="pnlInner" runat="server" ScrollBars="Auto" autosize="true" Width="100%"
                                    Direction="LeftToRight" BackColor="#FFFFE4" Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                    <asp:GridView ID="grdPopup" CssClass="select" runat="server" AutoGenerateColumns="true"
                                        AllowPaging="true" PageSize="25" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                        HeaderStyle-ForeColor="White" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                        OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
                                        <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                                        <RowStyle Height="25px" ForeColor="Black" Wrap="false" />
                                        <PagerStyle BackColor="Tomato" ForeColor="White" Width="100%" Font-Bold="true" />
                                        <PagerSettings Position="TopAndBottom" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="GhostWhite" Width="955px"
                    BorderColor="Teal" BorderWidth="1px" Height="300px" BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
                    <table width="80%" align="center">
                        <tr>
                            <td colspan="2" align="center" style="background-color: lightslategrey; color: White;">
                                <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana"
                                    Text="Tender Details"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

