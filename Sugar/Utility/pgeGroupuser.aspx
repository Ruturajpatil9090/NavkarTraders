<%@ Page Title="Group User" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeGroupuser.aspx.cs" Inherits="Sugar_Utility_pgeGroupuser_" %>

<%@ MasterType VirtualPath="~/MasterPage2.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="../CSS/NewModalPopup.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Script/jquery-1.4.2.js"></script>

    <link href="../../CSS/cssCommon.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../../JQuery/jquery-1.4.1.js" type="text/javascript"></script>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../../JQuery/jquery.keynavigation.js" type="text/javascript"></script>
    <link type="text/css" href="../../menu/menu.css" rel="stylesheet" />
    <script type="text/javascript" src="../../menu/jquery.js"></script>
    <script type="text/javascript" src="../../menu/menu.js"></script>
    <script type="text/javascript" src="../JS/DateValidation.js"> </script>

    <script src="../../JS/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../Scripts/selectfirstrow.js" type="text/javascript"></script>
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
            if (confirm("Do you want to delete data?")) {
                confirm_value.value = "Yes";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "Yes";
            }
            else {
                confirm_value.value = "No";
                document.getElementById("<%= hdconfirm.ClientID %>").value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">
        function pagevalidation() {
            try {
                debugger;
                $("#loader").show();
                var UserId = 0, UID = 0;

                var DocNo = $("#<%=txtGroup_Code.ClientID %>").val();
                var status = document.getElementById("<%= btnSave.ClientID %>").value;
                var spname = "SP_GroupUser";
                var XML = "<ROOT>";
                if (status == "Update") {
                    UserId = document.getElementById("<%= hdnfdoc.ClientID %>").value;
                    UID = document.getElementById("<%= hdnfid.ClientID %>").value;
                }
                var Group_Name = $("#<%=txtGroup_Name.ClientID %>").val() == "&nbsp;" ? "" : $("#<%=txtGroup_Name.ClientID %>").val();
                var Login_Name = $("#<%=txtLogin_Name.ClientID %>").val() == "&nbsp;" ? "" : $("#<%=txtLogin_Name.ClientID %>").val();
                var Password = document.getElementById("<%= hdnfpass.ClientID %>").value;
                var UserType = $("#<%=drpUser_Type.ClientID %>").val();
                //var Branch_Code = '<%= Session["Branch_Code"] %>';
                //var Company_Code = '<%= Session["Company_Code"] %>';
                //var Year_Code = '<%= Session["year"] %>';
                //var USER = '<%= Session["user"] %>';
                  var DOCNO = "";

                var smtpServerPort = '';
                var AuthoGroupID = 0;
                var Ac_Code = 0;

                var LastActivityDate = '2021/04/01';
                var RetryAttempts = 0;
                var IsLocked = '0';
                var LockedDateTime = '2021/04/01';
                if (status == "Save")
                {
                    XML = XML + "<Head  Group_Code='101' Group_Name='" + Group_Name + "' Login_Name='" + Login_Name + "' Password='" + Password + "' UserType='" + UserType + "' >";
                }
                else{
                    XML = XML + "<Head Doc_No='" + DocNo + "' Group_Code='101' Group_Name='" + Group_Name + "' Login_Name='" + Login_Name + "' Password='" + Password + "' UserType='" + UserType + "' >";
                }
                
                XML = XML + "</Head></ROOT>";

                $.ajax({
                    type: "POST",
                    url: "../xmlExecuteDMLQry.asmx/ExecuteXMLQry",
                    data: '{XML:"' + XML + '",status:"' + status + '",spname:"' + spname + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        $("#loader").hide();
                        alert(response.d);
                    },
                    error: function (response) {
                        $("#loader").hide();
                        alert(response.d);
                    }
                });


                var action = 1;
                function OnSuccess(response) {
                    debugger;
                    $("#loader").hide();
                    if (response.d.length > 0) {
                        var word = response.d;
                        //var len = word.length;
                        //var pos = word.indexOf(",");
                        //var id = word.slice(0, pos);
                        //var doc = word.slice(pos + 1, len);
                        if (status == "Save") {
                            alert('Sucessfully Added Record !!! Doc_no=' + word)
                        }
                        else {
                            alert('Sucessfully Updated Record !!! Doc_no=' + word)
                        }
                        window.open('../Utility/pgeGroupuser.aspx?Doc_No=' + word + '&Action=1', "_self");

                    }
                }
            }
            catch (exx) {
                $("#loader").hide();
                alert(exx)
            }
        }
    </script>
    <script type="text/javascript">
        function BACK() {
            window.open('../Utility/pgeGroupuser_Utility.aspx', '_self');
        }
    </script>
    <style>
        #loader
        {
            width: 100%;
            height: 100%;
            background-color: gray;
            position: fixed;
            margin: -0.7%;
            opacity: 0.6;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
        <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
            <legend style="text-align: center;">
                <asp:Label ID="label1" runat="server" Text="   Unregister Bill  " Font-Names="verdana"
                    ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
        </fieldset>
        <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px;">
            <img height="20%" width="20%" src="../Images/ajax-loader3.gif" />
        </div>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                 <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfdoc" runat="server" />
            <asp:HiddenField ID="hdnfid" runat="server" />
            <asp:HiddenField ID="hdnfpass" runat="server" />
            <asp:HiddenField ID="hdnfEpass" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />

                <table width="80%" align="left">
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                            &nbsp;
                                <asp:Button ID="btnSave" runat="server" OnClientClick="if (!Validate()) return false;"
                                    Text="Save" CssClass="btnHelp" Width="90px" TabIndex="8" Height="24px" ValidationGroup="add"
                                    OnClick="btnSave_Click" />
                            &nbsp;
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                                    TabIndex="38" Height="24px" ValidationGroup="save" OnClick="btnEdit_Click" />
                            &nbsp;
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                                    Height="24px" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()"
                                    TabIndex="39" />
                            &nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                                    TabIndex="40" Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" />
                            &nbsp;&nbsp;
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                                    ValidationGroup="save" Height="24px" OnClientClick="Back();" />
                            &nbsp;<asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btnHelp" Width="90px"
                                TabIndex="41" Height="24px" OnClick="btnPrint_Click" Visible="false" />
                            &nbsp;
                                <asp:Button runat="server" ID="btnGenEinvoice" Text="Generate EInvoice" CssClass="btnHelp"
                                    Width="120px" Height="24px" OnClientClick="EInovice();" Visible="false" />
                            <asp:Button runat="server" ID="btnCancleEinvoice" Text="Cancle EInvoice" CssClass="btnHelp"
                                Width="120px" Height="24px" OnClientClick="ConfirmCancle();" Visible="false" />
                            <%--  <asp:Button runat="server" ID="BtnTransfer" Text="Transfer" CssClass="btnHelp"
                                    Width="120px" Height="24px" OnClientClick="Transfer();" />--%>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                    Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                    <table style="width: 50%;" align="center" cellpadding="4" cellspacing="4">
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                    Font-Size="Small" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td align="left">Change No:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                    TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender runat="server" ID="txtxf" FilterType="Numbers" InvalidChars="."
                                    TargetControlID="txtEditDoc_No">
                                </ajax1:FilteredTextBoxExtender>
                                &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                    Font-Size="Small" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Group Code
                            </td>
                            <td>
                                <asp:TextBox ID="txtGroup_Code" runat="Server" CssClass="txt" TabIndex="1" Width="80px"
                                    Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtGroup_Code_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtGroup_Code" runat="server" Text="..." Width="80px" OnClick="btntxtGroup_Code_Click"
                                    CssClass="btnHelp" Height="24px" />
                                <asp:Label runat="server" ID="lblBill_Id" ForeColor="Black" Visible="false"></asp:Label>

                            </td>

                        </tr>

                        <tr>
                            <td align="left">Group Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtGroup_Name" runat="Server" CssClass="txt" TabIndex="3" Width="90px"
                                    Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtGroup_Name_TextChanged"
                                    Height="24px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td align="left">Login Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtLogin_Name" runat="Server" CssClass="txt" TabIndex="4" Width="90px"
                                   Style="text-align: left;" AutoPostBack="false" Height="24px"
                                    OnTextChanged="txtLogin_Name_TextChanged"></asp:TextBox>
                              
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Password:
                            </td>
                            <td>
                                 <asp:TextBox Height="24px" ID="txtPassword" runat="Server" CssClass="txt" TabIndex="5"
                                TextMode="Password" Width="150px" Style="text-align: left;" AutoPostBack="false"
                                OnTextChanged="txtPassword_TextChanged"></asp:TextBox>

                            </td>
                        </tr>

                        <tr>
                            <td align="left">UserType:
                            </td>
                            <td>
                                 <asp:DropDownList ID="drpUser_Type" runat="Server" CssClass="txt" TabIndex="9" Width="150px"
                                Height="24px" AutoPostBack="false" OnSelectedIndexChanged="drpUser_Type_SelectedIndexChanged">
                                <asp:ListItem Text="User" Value="U" />
                                <asp:ListItem Text="Admin" Value="A" />
                            </asp:DropDownList>

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

