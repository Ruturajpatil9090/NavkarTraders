<%@ Page Title="User Creation" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeUser_Creation.aspx.cs" Inherits="Sugar_Utility_pgeUser_Creation" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
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

        function checkAll(objRef) {
            debugger;
            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {


                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {


                        //  row.style.backgroundColor = "aqua";

                        inputList[i].checked = true;

                    }

                    else {



                        if (row.rowIndex % 2 == 0) {



                            row.style.backgroundColor = "#C2D69B";

                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }

    </script>
    <script type="text/javascript">
        function validation() {
            try {
                debugger;
                $("#loader").show();
                var UserId = 0, UID = 0;

                var DocNo = $("#<%=txtUser_Id.ClientID %>").val();
                var status = document.getElementById("<%= btnSave.ClientID %>").value;
                var spname = "UserCreation";
                var XML = "<ROOT>";
                if (status == "Update") {
                    UserId = document.getElementById("<%= hdnfdoc.ClientID %>").value;
                    UID = document.getElementById("<%= hdnfid.ClientID %>").value;
                }
                var FullName = $("#<%=txtuserfullname.ClientID %>").val() == "&nbsp;" ? "" : $("#<%=txtuserfullname.ClientID %>").val();
                var UserName = $("#<%=txtUser_Name.ClientID %>").val() == "&nbsp;" ? "" : $("#<%=txtUser_Name.ClientID %>").val();
                var Password = document.getElementById("<%= hdnfpass.ClientID %>").value;
                var Email_Id = $("#<%=txtEmailId.ClientID %>").val() == "&nbsp;" ? "" : $("#<%=txtEmailId.ClientID %>").val();
                var EmailPassword = document.getElementById("<%= hdnfEpass.ClientID %>").value;;
                var MobileNo = $("#<%=txtMobile.ClientID %>").val() == "&nbsp;" ? "" : $("#<%=txtMobile.ClientID %>").val();
                var UserType = $("#<%=drpUser_Type.ClientID %>").val();
                var Branch_Code = '<%= Session["Branch_Code"] %>';
                var Company_Code = '<%= Session["Company_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';
                var USER = '<%= Session["user"] %>';
                var DOCNO = "";
                var User_Security = $("#<%=drpUserSecurity.ClientID %>").val();
                var smtpServerPort = '';
                var AuthoGroupID = 0;
                var Ac_Code = 0;

                var LastActivityDate = '2021/04/01';
                var RetryAttempts = 0;
                var IsLocked = '0';
                var LockedDateTime = '2021/04/01';

                XML = XML + "<UserHead User_Id='" + UserId + "' User_Name='" + UserName + "' User_Type='" + UserType + "' Password='" + Password + "' EmailId='" + Email_Id + "' " +
                    "EmailPassword='" + EmailPassword + "' Company_Code='" + Company_Code + "' Mobile='" + MobileNo + "' Branch_Code='" + Branch_Code + "' uid='" + UID + "' userfullname='" + FullName + "' " +
                    "smtpServerPort='" + smtpServerPort + "' AuthoGroupID='" + AuthoGroupID + "' Ac_Code='" + Ac_Code + "' LastActivityDate='" + LastActivityDate + "' " +
                    "RetryAttempts='" + RetryAttempts + "' IsLocked='" + IsLocked + "' LockedDateTime='" + LockedDateTime + "' User_Security='" + User_Security + "'>";
                //Detail Calculate
                var gridViewDetail = document.getElementById("<%=grdDetail.ClientID %>");
                var grdrow = gridViewDetail.getElementsByTagName("tr");
                var checkBoxes = gridViewDetail.getElementsByTagName("INPUT");

                for (var i = 1; i < grdrow.length; i++) {
                    var ID = gridViewDetail.rows[i].cells[0].innerHTML;
                    var ProgramName = gridViewDetail.rows[i].cells[1].innerHTML;
                    var TranType = gridViewDetail.rows[i].cells[2].innerHTML;
                    if (TranType == "&nbsp;") {
                        TranType = "";
                    }
                    var chkpermission = "N";
                    if (checkBoxes[i].checked) {
                        chkpermission = "Y";
                    }

                    XML = XML + "<Detail Detail_Id='" + ID + "' User_Id='" + UserId + "' Program_Name='" + ProgramName + "' Tran_Type='" + TranType + "' Permission='" + chkpermission + "' Company_Code='" + Company_Code + "' " +
                        "Created_By='" + USER + "' Modified_By='" + USER + "' Year_code='" + Year_Code + "' udid='0' uid='" + UID + "'/>";

                }

                //Detail Report Calculate
                var gridViewReport = document.getElementById("<%=grdDetail_Report.ClientID %>");
                var grdrowReport = gridViewReport.getElementsByTagName("tr");
                var checkBoxes = gridViewReport.getElementsByTagName("INPUT");

                for (var i = 1; i < grdrowReport.length; i++) {
                    var ID = gridViewReport.rows[i].cells[0].innerHTML;
                    var ProgramName = gridViewReport.rows[i].cells[1].innerHTML;
                    var TranType = gridViewReport.rows[i].cells[2].innerHTML;
                    if (TranType == "&nbsp;") {
                        TranType = "";
                    }
                    var chkpermission = "N";
                    if (checkBoxes[i].checked) {
                        chkpermission = "Y";
                    }

                    XML = XML + "<DetailReport Detail_Id='" + ID + "' User_Id='" + UserId + "' Program_Name='" + ProgramName + "' Tran_Type='" + TranType + "' Permission='" + chkpermission + "' Company_Code='" + Company_Code + "' " +
                        "Created_By='" + USER + "' Modified_By='" + USER + "' Year_code='" + Year_Code + "' udrid='0' uid='" + UID + "'/>";

                }
                XML = XML + "</UserHead></ROOT>";

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
                        var len = word.length;
                        var pos = word.indexOf(",");
                        var id = word.slice(0, pos);
                        var doc = word.slice(pos + 1, len);
                        if (status == "Save") {
                            alert('Sucessfully Added Record !!! Doc_no=' + doc)
                        }
                        else {
                            alert('Sucessfully Updated Record !!! Doc_no=' + doc)
                        }
                        window.open('../Utility/pgeUser_Creation.aspx?uid=' + id + '&Action=1', "_self");

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
            window.open('../Utility/pgeUserCreation_Utility.aspx', '_self');
        }
    </script>
    <style>
        #loader {
            width: 100%;
            height: 100%;
            background-color: gray;
            position: fixed;
            margin: -0.7%;
            opacity: 0.6;
        }
    </style>

    <%--  <script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="User Creation" Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px;">
        <img height="40%" width="40%" src="../Images/ajax-loader3.gif" />
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

            <table width="70%" align="center">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" />
                        &nbsp;
                         <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                             ValidationGroup="save" Height="24px" OnClientClick="BACK()" />
                        &nbsp;
                         <asp:Button ID="Button1" runat="server" Text="Check" CssClass="btnHelp" Width="90px"
                             ValidationGroup="save" Height="24px" OnClientClick="validation()" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 40%;" align="center" cellpadding="4" cellspacing="4">
                    <td align="left" colspan="2">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                            Font-Size="Small" ForeColor="Red"></asp:Label>
                    </td>
                    <tr>
                        <td align="left" style="width: 10%;">User Id:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtUser_Id" runat="Server" CssClass="txt" TabIndex="2"
                                Width="110px" Style="text-align: left;" AutoPostBack="true"
                                OnTextChanged="txtUser_Id_TextChanged"></asp:TextBox>
                            <asp:Label ID="lbluid" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Font-Bold="true" ForeColor="Blue" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">User Full Name:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtuserfullname" runat="Server" CssClass="txt" TabIndex="3"
                                Width="300px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">User Name:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtUser_Name" runat="Server" CssClass="txt" TabIndex="4"
                                Width="300px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtUser_Name_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Password:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtPassword" runat="Server" CssClass="txt" TabIndex="5"
                                TextMode="Password" Width="150px" Style="text-align: left;" AutoPostBack="false"
                                OnTextChanged="txtPassword_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Email Id:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtEmailId" runat="Server" CssClass="txt" TabIndex="6"
                                Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEmailId_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Email Password:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtEmailPassword" runat="Server" CssClass="txt" TabIndex="7"
                                TextMode="Password" Width="150px" Style="text-align: left;" AutoPostBack="false"
                                OnTextChanged="txtEmailPassword_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Mobile No:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox Height="24px" ID="txtMobile" runat="Server" CssClass="txt" TabIndex="8"
                                Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtMobile_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender6" FilterType="Numbers"
                                TargetControlID="txtMobile">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">User Type:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:DropDownList ID="drpUser_Type" runat="Server" CssClass="txt" TabIndex="9" Width="150px"
                                Height="24px" AutoPostBack="false" OnSelectedIndexChanged="drpUser_Type_SelectedIndexChanged">
                                <asp:ListItem Text="User" Value="U" />
                                <asp:ListItem Text="Admin" Value="A" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">User Security:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:DropDownList ID="drpUserSecurity" runat="Server" CssClass="txt" TabIndex="9" Width="150px"
                                Height="24px" AutoPostBack="false" OnSelectedIndexChanged="drpUserSecurity_SelectedIndexChanged">
                                <asp:ListItem Text="YES" Value="Y" />
                                <asp:ListItem Text="No" Value="N" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td align="left" style="width: 10%;">User Authority:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:DropDownList ID="drpUserAuthority" runat="Server" CssClass="txt" TabIndex="10"
                                Width="150px" AutoPostBack="false" OnSelectedIndexChanged="drpUseAuthority_SelectedIndexChanged"
                                Height="24px">
                                <asp:ListItem Text="MD Related" Value="M" />
                                <asp:ListItem Text="Account Related" Value="A" />
                                <asp:ListItem Text="Production Related" Value="P" />
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="4" align="left">
                            <asp:Button ID="btnOpenDetailsPopup" runat="server" Text="ADD" CssClass="btnHelp"
                                Width="80px" Height="25px" OnClick="btnOpenDetailsPopup_Click" TabIndex="28"
                                Visible="false" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 100%; margin-left: 0px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                <legend style="text-align: center;">
                    <h5 style="color: White;" font-names="verdana" font-size="Medium">Detail Entry</h5>
                </legend>
            </fieldset>
            <table>
                <tr>
                    <td colspan="4" align="left">
                        <div style="width: 80%; position: relative;">
                            <asp:UpdatePanel ID="upGrid" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlgrdDetail" runat="server" ScrollBars="Both" Height="350px" Width="700px"
                                        BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true" Font-Names="Verdana"
                                        Font-Size="11px" BackColor="SeaShell" Style="margin-right: 30px; float: left;">
                                        <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#397CBB"
                                            HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="100%"
                                            OnRowCommand="grdDetail_RowCommand" CellPadding="5" CellSpacing="5" OnRowDataBound="grdDetail_RowDataBound"
                                            Style="table-layout: fixed;" alin="left">
                                            <Columns>
                                                <asp:BoundField DataField="Detail_Id" HeaderText="Detail_ID" ControlStyle-Width="10px" />
                                                <asp:BoundField DataField="Program_Name" HeaderText="Program_Name" ControlStyle-Width="500px" />
                                                <asp:BoundField DataField="Tran_Type" HeaderText="Tran_Type" ControlStyle-Width="10px" />
                                                <asp:TemplateField HeaderText="Permission" ControlStyle-Width="10px">
                                                    <HeaderTemplate>

                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" Text="Permission" />

                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <asp:CheckBox Text='<%# Eval("Permission") %>' runat="server" ID="chkpermission" Checked="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="rowAction" HeaderText="rowAction" />
                                                <asp:BoundField DataField="SrNo" HeaderText="SrNo" />
                                            </Columns>
                                            <RowStyle Height="27px" Wrap="true" ForeColor="Black" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>

                    <td colspan="4" align="right">
                        <div style="width: 80%; position: relative;">
                            <asp:UpdatePanel ID="upgridreport" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlgrdDetailReport" runat="server" BackColor="SeaShell" BorderColor="Maroon"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"
                                        Height="350px" ScrollBars="Both" Style="margin-right: 30px; float: right;"
                                        Width="700px">

                                        <asp:GridView ID="grdDetail_Report" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#397CBB"
                                            HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="100%"
                                            OnRowCommand="grdDetail_Report_RowCommand" CellPadding="5" CellSpacing="5" OnRowDataBound="grdDetail_Report_RowDataBound"
                                            Style="table-layout: fixed;">

                                            <Columns>
                                                <asp:BoundField ControlStyle-Width="10px" DataField="Detail_Id_Report" HeaderText="Detail_ID" />
                                                <asp:BoundField ControlStyle-Width="500px" DataField="Program_Name_Report" HeaderText="Program_Name" />
                                                <asp:BoundField ControlStyle-Width="10px" DataField="Tran_Type_Report" HeaderText="Tran_Type" />
                                                <asp:TemplateField ControlStyle-Width="10px" HeaderText="Permission">
                                                    <HeaderTemplate>

                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" Text="Permission" />

                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkpermission" runat="server" Text='<%# Eval("Permission_report") %>' Checked="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="rowAction" HeaderText="rowAction" />
                                                <asp:BoundField DataField="SrNo" HeaderText="SrNo" />
                                            </Columns>
                                            <RowStyle ForeColor="Black" Height="27px" Wrap="true" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>

            </table>
            <asp:Panel onkeydown="closepopup(event);" ID="pnlPopup" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 1000px; min-height: 700px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
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
                            <asp:TextBox onkeydown="SelectFirstRow(event);" ID="txtSearchText" runat="server"
                                Width="250px" Height="20px" AutoPostBack="false"></asp:TextBox>
                            <asp:Button onkeydown="closepopup(event);" ID="btnSearch" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 680px">
                                <asp:GridView Font-Bold="true" CssClass="select" AllowPaging="true" PageSize="25"
                                    OnPageIndexChanging="grdPopup_PageIndexChanging" ID="grdPopup" runat="server"
                                    AutoGenerateColumns="true" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound"
                                    Style="table-layout: fixed;">
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
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

