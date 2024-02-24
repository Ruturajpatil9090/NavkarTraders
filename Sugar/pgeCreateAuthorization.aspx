<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeCreateAuthorization.aspx.cs" Inherits="Sugar_pgeCreateAuthorization" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Create Authorization Group  " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <br />
    <br />
    <asp:Panel ID="pnlCreateUser" runat="server" Font-Bold="true" Font-Size="12px" BackColor="White"
        BorderColor="Olive" BorderWidth="2px" Font-Names="verdana" Width="400px">
        <table width="100%" cellpadding="10px" cellspacing="10px">
            <tr>
                <td colspan="2" style="background-color: Olive; height: 30px; color: Yellow;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AUTHORIZATION
                </td>
            </tr>
            <tr>
                <td align="right">
                    Group Name:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtGroupName" runat="server" Height="30px" BorderColor="Olive" BorderWidth="1px"
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Select Pages:
                </td>
                <td align="left">
                    <asp:ImageButton runat="server" ID="imgSelectPages" Width="16px" ImageUrl="~/Images/add.png"
                        OnClick="imgSelectPages_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnAddUser" runat="server" Text="Create" Width="80px" BorderWidth="1px"
                        ForeColor="Yellow" Height="25px" BorderColor="Olive" BackColor="Olive" Font-Bold="true"
                        OnClick="btnAddUser_Click" />
                    &nbsp;
                    <asp:Button ID="btnCancelUser" runat="server" Text="Cancel" Width="80px" BorderWidth="1px"
                        ForeColor="Yellow" Height="25px" BorderColor="Olive" BackColor="Olive" Font-Bold="true"
                        OnClick="btnCancelUser_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlTree" runat="server" Width="30%" align="left" ScrollBars="Horizontal"
        BackColor="#FFFFE4" Direction="LeftToRight" Style="z-index: 5000; position: absolute;
        display: none; float: right; max-height: 600px; min-height: 600px; box-shadow: 1px 1px 8px 2px;
        background-position: center; left: 36%; top: 7%;">
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/closebtn.jpg"
            Width="20px" Height="20px" Style="float: right; vertical-align: top;" ToolTip="Close"
            OnClick="ImageButton1_Click" />
        <asp:TreeView runat="server" ID="TreeSelectPages" ShowCheckBoxes="All" ImageSet="Custom"
            onclick="OnTreeClick(event)" NodeIndent="5" Font-Names="Times New Roman" Font-Size="Medium"
            Font-Strikeout="False" Width="200px">
        </asp:TreeView>
        <asp:Button runat="server" ID="btnOk" Text="OK" Width="80px" OnClick="btnOk_Click" />
    </asp:Panel>
</asp:Content>
