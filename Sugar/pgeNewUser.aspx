<%@ Page Title="User" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeNewUser.aspx.cs" Inherits="Sugar_pgeNewUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        function CheckEmail() {
            var value = document.getElementById("<%= txtUserEmail.ClientID %>").value;
            var atPosition = value.indexOf("@");
            var dotPosition = value.lastIndexOf(".");

            if (atPosition < 1 || dotPosition < atPosition + 2 || dotPosition + 2 >= value.length) {
                alert("Please Enter Valid Email Address!");
                return false;
            }
            else {
                return true;
            }
        }</script>
    <script language="javascript" type="text/javascript">
        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                {
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                    {
                        //check or uncheck children at all levels
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
                //check or uncheck parents at all levels
                CheckUncheckParents(src, src.checked);
            }
        }

        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }

        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;

            if (parentNodeTable) {
                var checkUncheckSwitch;

                if (check) //checkbox checked
                {
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                    if (isAllSiblingsChecked)
                        checkUncheckSwitch = true;
                    else
                        return; //do not need to check parent if any(one or more) child not checked
                }
                else //checkbox unchecked
                {
                    checkUncheckSwitch = false;
                }

                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;
                    //do the same recursively
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (!prevChkBox.checked) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }

    </script>
    <script type="text/javascript">
        function Validate() {
            var gridView = document.getElementById("<%=grdAuthoGroup.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    return true;
                }
            }
            alert("Please Select atleast One Group");
            return false;
        }
    </script>
    <script type="text/javascript">
        function chkcontrol(j) {
            var total = 0;
            var gridView = document.getElementById("<%=grdAuthoGroup.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked) {
                    total = total + 1;
                }
                if (total > 2) {
                    alert("Please Select only two groups");
                    checkBoxes[i].checked = false;
                    return false;
                }
            }
        } </script>
    <script type="text/javascript">
        function passwordCheck() {
            var txtPassword = document.getElementById("<%= txtpass1.ClientID %>").value;
            var txtConfirmpass = document.getElementById("<%= txtConfirmPass.ClientID %>").value;
            if (txtPassword != txtConfirmpass) {
                alert("password does not match!");
                //document.getElementById("<%= txtConfirmPass.ClientID %>").focus();
                return false;
            }
            else {
                return true;
            }
        }</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnf" runat="server" />
    <asp:HiddenField ID="hdconfirm" runat="server" />
    <center>
        <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
            margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
            border-left: 0px; border-right: 0px; height: 7px;">
            <legend style="text-align: center;">
                <asp:Label ID="label1" runat="server" Text="   New User  " Font-Names="verdana" ForeColor="White"
                    Font-Bold="true" Font-Size="12px"></asp:Label></legend>
        </fieldset>
    </center>
    <br />
    <asp:Label Width="400px" ID="lblmsg" CssClass="lblName" runat="server" Font-Bold="true"
        ForeColor="Red"></asp:Label>
    <br />
    <asp:Panel ID="pnlCreateUser" runat="server" Font-Bold="true" Font-Size="12px" BackColor="White"
        BorderColor="Olive" BorderWidth="2px" Font-Names="verdana" Width="400px">
        <table width="100%" cellpadding="10px" cellspacing="10px">
            <tr>
                <td colspan="2" style="background-color: Olive; height: 30px; color: Yellow;">
                    &nbsp;Create User
                </td>
            </tr>
            <tr>
                <td align="right">
                    User Name:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtUname1" runat="server" Height="30px" BorderColor="Olive" BorderWidth="1px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    User Type:
                </td>
                <td align="left">
                    <asp:DropDownList ID="drpUserType" runat="server" Height="30px" AutoPostBack="true"
                        BorderColor="Olive" OnSelectedIndexChanged="drpUserType_SelectedIndexChanged">
                        <asp:ListItem Text="Administrator" Value="A"></asp:ListItem>
                        <asp:ListItem Text="User" Value="U"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" id="trgroup">
                <td align="right">
                    Assign Group:
                </td>
                <td align="left">
                    <asp:ImageButton runat="server" ID="SelectGroup" Width="16px" ImageUrl="~/Images/add.png"
                        Style="height: 16px" OnClick="SelectGroup_Click" />
                </td>
            </tr>
            <tr runat="server" id="trpages">
                <td align="right">
                    Select Pages:
                </td>
                <td align="left">
                    <asp:ImageButton runat="server" ID="imgSelectPages" Width="16px" ImageUrl="~/Images/add.png"
                        OnClick="imgSelectPages_Click" />
                    <%--  <ajax1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlTree" CancelControlID="imgBtnClose"
                        TargetControlID="imgSelectPages">
                    </ajax1:ModalPopupExtender>--%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Password:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtpass1" runat="server" Height="30px" BorderColor="Olive" BorderWidth="1px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Confirm Password:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtConfirmPass" runat="server" Height="30px" BorderColor="Olive"
                        onblur="javascript:return passwordCheck();" BorderWidth="1px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Email ID:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtUserEmail" runat="server" Height="30px" Width="200px" BorderColor="Olive"
                        BorderWidth="1px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Email Password:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEmailPassword" runat="server" Height="30px" Width="150px" BorderColor="Olive"
                        BorderWidth="1px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Mobile:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtUserMobil" runat="server" Height="30px" Width="150px" BorderColor="Olive"
                        MaxLength="10" BorderWidth="1px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Branch:
                </td>
                <td align="left">
                    <asp:DropDownList ID="drpUserBranch" runat="server" Height="30px" Width="240px" BorderColor="Olive">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button runat="server" ID="btnAdd" Text="Add New" Width="80px" BorderWidth="1px"
                        ForeColor="Yellow" Height="25px" BorderColor="Olive" BackColor="Olive" Font-Bold="true"
                        OnClick="btnAdd_Click" />&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="80px" BorderWidth="1px"
                        ForeColor="Yellow" Height="25px" BorderColor="Olive" BackColor="Olive" Font-Bold="true"
                        OnClick="btnAddUser_Click" OnClientClick="javascript:return CheckEmail();" />
                    &nbsp;
                    <asp:Button runat="server" ID="btnEdit" Text="Edit" Width="80px" BorderWidth="1px"
                        ForeColor="Yellow" Height="25px" BorderColor="Olive" BackColor="Olive" Font-Bold="true"
                        OnClick="btnEdit_Click" />&nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="80px" BorderWidth="1px"
                        ForeColor="Yellow" Height="25px" BorderColor="Olive" BackColor="Olive" Font-Bold="true"
                        OnClick="btnDelete_Click" OnClientClick="Confirm()" />&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button runat="server" ID="btnCancel" Text="Cancel" Width="80px" BorderWidth="1px"
                        ForeColor="Yellow" Height="25px" BorderColor="Olive" BackColor="Olive" Font-Bold="true"
                        OnClick="btnCancel_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="left">
                    &nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnFirst" Text="<<" Width="80px" BorderWidth="1px"
                        ForeColor="Yellow" Height="25px" BorderColor="Olive" BackColor="Olive" Font-Bold="true"
                        OnClick="btnFirst_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnPrevious" Text="<" Width="80px" BorderWidth="1px"
                        ForeColor="Yellow" Height="25px" BorderColor="Olive" BackColor="Olive" Font-Bold="true"
                        OnClick="btnPrevious_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnNext" Text=">" Width="80px" BorderWidth="1px" ForeColor="Yellow"
                        Height="25px" BorderColor="Olive" BackColor="Olive" Font-Bold="true" OnClick="btnNext_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnLast" Text=">>" Width="80px" BorderWidth="1px"
                        ForeColor="Yellow" Height="25px" BorderColor="Olive" BackColor="Olive" Font-Bold="true"
                        OnClick="btnLast_Click" />&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlTree" runat="server" Width="30%" align="left" ScrollBars="Horizontal"
        BackColor="#FFFFE4" Direction="LeftToRight" Style="z-index: 5000; position: absolute;
        display: none; float: right; max-height: 600px; min-height: 600px; box-shadow: 1px 1px 8px 2px;
        background-position: center; left: 38%; top: 7%;">
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/closebtn.jpg"
            Width="20px" Height="20px" Style="float: right; vertical-align: top;" ToolTip="Close"
            OnClick="ImageButton1_Click" />
        <asp:TreeView runat="server" ID="TreeSelectPages" ShowCheckBoxes="All" ImageSet="Custom"
            onclick="OnTreeClick(event)" NodeIndent="5" Font-Names="Times New Roman" Font-Size="Medium"
            Font-Strikeout="False" Width="200px">
        </asp:TreeView>
        <asp:Button runat="server" ID="btnOk" Text="OK" Width="80px" OnClick="btnOk_Click" />
    </asp:Panel>
    <asp:Panel ID="pnlSelectGroup" runat="server" Width="30%" align="left" ScrollBars="Horizontal"
        BackColor="#FFFFE4" Direction="LeftToRight" Style="z-index: 5000; position: absolute;
        display: none; float: right; max-height: 400px; min-height: 400px; box-shadow: 1px 1px 8px 2px;
        background-position: center; left: 36%; top: 20%;">
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/closebtn.jpg"
            Width="20px" Height="20px" Style="float: right; vertical-align: top;" ToolTip="Close"
            OnClick="ImageButton2_Click" />
        <asp:GridView runat="server" ID="grdAuthoGroup" AutoGenerateColumns="false" Width="300px"
            SkinID="Professional" Font-Name="Verdana" Font-Size="10pt" CellPadding="4" HeaderStyle-BackColor="#444444"
            HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd" OnRowDataBound="grdAuthoGroup_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="grdCB" runat="server" onClick="chkcontrol(this)" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Group_ID" HeaderText="ID" />
                <asp:BoundField DataField="Group_Name" HeaderText="Group" />
            </Columns>
        </asp:GridView>
        <asp:Button runat="server" ID="btnSelectedgroup" Text="OK" Width="60px" OnClick="btnSelectedgroup_Click"
            OnClientClick="return Validate();" />
    </asp:Panel>
</asp:Content>
