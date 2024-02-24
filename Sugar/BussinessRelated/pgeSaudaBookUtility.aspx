<%@ Page Title="Sauda Book" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeSaudaBookUtility.aspx.cs" Inherits="Sugar_BussinessRelated_pgeSaudaBookUtility"
    ValidateRequest="false" %>

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
                if (hdnfClosePopupValue == "txtmillcode") {
                    document.getElementById("<%=txtmillcode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "BU") {
                    document.getElementById("<%=txtBuyer.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtShipTo") {
                    document.getElementById("<%=txtShipTo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "BP") {
                    document.getElementById("<%=txtBuyerParty.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtsubBroker") {
                    document.getElementById("<%=txtsubBroker.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBP_Account") {
                    document.getElementById("<%=txtBP_Account.ClientID %>").focus();
                 }
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
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
                if (hdnfClosePopupValue == "txtmillcode") {

                    document.getElementById("<%=txtmillcode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[10].innerText;
                    document.getElementById("<%=lblmillname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[2].innerText;
                    document.getElementById("<%=txttender.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=hdnftenderno.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=hdnfID.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[14].innerText;
                    document.getElementById("<%=lblselfqty.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[7].innerText;
                    document.getElementById("<%=lblgrade.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[5].innerText;
                    document.getElementById("<%=lbllifting.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    document.getElementById("<%=lblmillrate.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[6].innerText;
                    document.getElementById("<%=lblseason.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[12].innerText;
                    document.getElementById("<%=lblpurcrate.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[13].innerText;
                    document.getElementById("<%=txtDetailLiftingDate.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtmillcode.ClientID %>").focus();

                }
                if (hdnfClosePopupValue == "BU") {

                    document.getElementById("<%=txtBuyer.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBuyerName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=lblbuyercity.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtBuyer.ClientID %>").focus();
                }

                if (hdnfClosePopupValue == "txtShipTo") {
                    document.getElementById("<%=txtShipTo.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblShiptoname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=lblshiptocity.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[3].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtShipTo.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "BP") {

                    document.getElementById("<%=txtBuyerParty.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBuyerPartyName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtBuyerParty.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "SubBrker") {
                    document.getElementById("<%=txtsubBroker.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblsubBroker.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtsubBroker.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBP_Account") {
                    document.getElementById("<%=txtBP_Account.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBP_Account.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtBP_Account.ClientID %>").focus();
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


function sendingaccode(e) {
    debugger;
    if (e.keyCode == 112) {
        debugger;
        e.preventDefault();
        $("#<%=pnlPopup.ClientID %>").show();

        $("#<%=btntxtmillcode.ClientID %>").click();

    }
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txtmillcode.ClientID %>").val();

        unit = "0" + unit;
        $("#<%=txtmillcode.ClientID %>").val(unit);
        __doPostBack("txtmillcode", "TextChanged");

    }

}
function qtl(e) {
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txtBuyerQuantal.ClientID %>").val();

        unit = "0" + unit;
        $("#<%=txtBuyerQuantal.ClientID %>").val(unit);
                __doPostBack("txtBuyerQuantal", "TextChanged");

            }
        }
        function Party(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
        $("#<%=btnBuyer.ClientID %>").click();

    }
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txtBuyer.ClientID %>").val();

        unit = "0" + unit;
        $("#<%=txtBuyer.ClientID %>").val(unit);
        __doPostBack("txtBuyer", "TextChanged");

    }
    if (e.keyCode == 13) {
        e.preventDefault();
        $("#<%=btnUpdate.ClientID %>").focus();
    }
}
function ShipTo(e) {
    debugger;
    if (e.keyCode == 112) {
        debugger;
        e.preventDefault();
        $("#<%=pnlPopup.ClientID %>").show();
        $("#<%=btntxtShipTo.ClientID %>").click();

    }
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txtShipTo.ClientID %>").val();

        unit = "0" + unit;
        $("#<%=txtShipTo.ClientID %>").val(unit);
        __doPostBack("txtShipTo", "TextChanged");

    }

}
function detailBroker(e) {
    debugger;
    if (e.keyCode == 112) {
        debugger;
        e.preventDefault();
        $("#<%=pnlPopup.ClientID %>").show();
        $("#<%=btnBuyerParty.ClientID %>").click();

    }
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txtBuyerParty.ClientID %>").val();

        unit = "0" + unit;
        $("#<%=txtBuyerParty.ClientID %>").val(unit);
        __doPostBack("txtBuyerParty", "TextChanged");

    }

}
function subBroker(e) {
    debugger;
    if (e.keyCode == 112) {
        debugger;
        e.preventDefault();
        $("#<%=pnlPopup.ClientID %>").show();
        $("#<%=btnsubBrker.ClientID %>").click();

    }
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txtsubBroker.ClientID %>").val();

        unit = "0" + unit;
        $("#<%=txtsubBroker.ClientID %>").val(unit);
                __doPostBack("txtsubBroker", "TextChanged");

            }

        }
        function bpac(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btnBP_Account.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBP_Account.ClientID %>").val();

        unit = "0" + unit;
        $("#<%=txtBP_Account.ClientID %>").val(unit);
                __doPostBack("txtBP_Account", "TextChanged");

    }

}
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                <legend style="text-align: center;">
                    <asp:Label ID="label1" runat="server" Text="Sauda Book Utility" Font-Names="verdana"
                        ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
            </fieldset>
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfpopup" runat="server" />
            <asp:HiddenField ID="hdnftenderno" runat="server" />
            <asp:HiddenField ID="hdnftenderdetailid" runat="server" />
            <asp:HiddenField ID="hdnfID" runat="server" />
            <asp:HiddenField ID="HiddenField1" runat="server" />

            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 60%;" align="center" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="right" style="width: 30%;">Mill Code:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox Height="24px" ID="txtmillcode" runat="Server" CssClass="txt" TabIndex="1"
                                Width="90px" Style="text-align: left;" OnkeyDown="sendingaccode(event);" AutoPostBack="false"
                                OnTextChanged="txtmillcode_TextChanged"></asp:TextBox>
                            <asp:Button Width="20px" Height="24px" ID="btntxtmillcode" runat="server" Text="..."
                                OnClick="btntxtmillcode_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblmillname" runat="server" CssClass="lblName"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Tender No:
                        </td>
                        <td align="left" style="width: 30%;">
                            <asp:TextBox Height="24px" ID="txttender" runat="Server" CssClass="txt" TabIndex="2"
                                Width="90px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            <asp:Label ID="lblselfqty" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblgrade" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lbllifting" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblmillrate" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblseason" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblpurcrate" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <br />
                    <br />
                    <tr>
                        <td align="right" style="width: 30%;">Bill To:
                        </td>
                        <td align="left" style="width: 20%;">

                            <asp:TextBox ID="txtBuyer" runat="server" Width="80px" Height="24px" AutoPostBack="false"
                                CssClass="txt" OnTextChanged="txtBuyer_TextChanged" onkeydown="Party(event);" TabIndex="3"></asp:TextBox>
                            <%--<ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyer" FilterType="Numbers" TargetControlID="txtBuyer"></ajax1:FilteredTextBoxExtender>--%>
                            <asp:Button ID="btnBuyer" runat="server" Text="..." Height="24px" Width="20px" CssClass="btnHelp"
                                OnClick="btnBuyer_Click" />
                            <asp:Label ID="lblBuyerName" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblbuyer_id" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblbuyercity" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Ship To:
                        </td>
                        <td align="left" style="width: 20%;">

                            <asp:TextBox ID="txtShipTo" runat="server" Width="80px" Height="24px" AutoPostBack="false"
                                CssClass="txt" OnTextChanged="txtShipTo_TextChanged" onkeydown="ShipTo(event);" TabIndex="4"></asp:TextBox>
                            <%--<ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyer" FilterType="Numbers" TargetControlID="txtBuyer"></ajax1:FilteredTextBoxExtender>--%>
                            <asp:Button ID="btntxtShipTo" runat="server" Text="..." Height="24px" Width="20px" CssClass="btnHelp"
                                OnClick="btntxtShipTo_Click" />
                            <asp:Label ID="lblShiptoname" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblshiptoid" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblshiptocity" runat="server" CssClass="lblName"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvtxtBuyer" runat="server" ControlToValidate="txtBuyer"
                                CssClass="validator" Display="Dynamic" ErrorMessage="Required" SetFocusOnError="true"
                                Text="Required" ValidationGroup="addBuyerDetails">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Delivery Type:
                        </td>
                        <td align="left" style="width: 20%;">

                            <asp:DropDownList ID="drpDeliveryType" runat="server" CssClass="ddl" Width="240px"
                                TabIndex="5" AutoPostBack="true" Height="26px" OnSelectedIndexChanged="drpDeliveryType_SelectedIndexChanged">
                                <asp:ListItem Text="With GST Naka Delivery" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Naka Delivery without GST Rate" Value="A"></asp:ListItem>
                                <asp:ListItem Text="Commission" Value="C" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="DO" Value="D"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Broker:
                        </td>
                        <td align="left" style="width: 20%;">

                            <asp:TextBox ID="txtBuyerParty" runat="server" Width="80px" CssClass="txt" Height="24px"  TabIndex="6"
                                OnTextChanged="txtBuyerParty_TextChanged" AutoPostBack="false" onkeydown="detailBroker(event);"></asp:TextBox>
                            <%--<ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyerParty" FilterType="Numbers" TargetControlID="txtBuyerParty"></ajax1:FilteredTextBoxExtender>--%>
                            <asp:Button ID="btnBuyerParty" Height="24px" Width="20px" runat="server" Text="..."
                                CssClass="btnHelp" OnClick="btnBuyerParty_Click" />
                            <asp:Label ID="lblBuyerPartyName" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblbuyerparty_id" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Sub Broker:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtsubBroker" runat="server" Width="80px" CssClass="txt" Height="24px"  TabIndex="7"
                                OnTextChanged="txtsubBroker_TextChanged" AutoPostBack="false" onkeydown="subBroker(event);"></asp:TextBox>
                            <%--<ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyerParty" FilterType="Numbers" TargetControlID="txtBuyerParty"></ajax1:FilteredTextBoxExtender>--%>
                            <asp:Button ID="btnsubBrker" Height="24px" Width="20px" runat="server" Text="..."
                                CssClass="btnHelp" OnClick="btnsubBrker_Click" />
                            <asp:Label ID="lblsubBroker" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblsubId" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Buyer Quantal:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtBuyerQuantal" runat="server" Width="80px" Height="24px" CssClass="txt"
                                AutoPostBack="true" OnTextChanged="txtBuyerQuantal_TextChanged" TabIndex="8"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyerQuantal" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtBuyerQuantal">
                            </ajax1:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="rfvtxtBuyerQuantal" runat="server" ControlToValidate="txtBuyerQuantal"
                                CssClass="validator" Display="Dynamic" ErrorMessage="Required" SetFocusOnError="true"
                                Text="Required" ValidationGroup="addBuyerDetails">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Sale Rate:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtBuyerSaleRate" runat="server" Width="80px" Height="24px" CssClass="txt"
                                AutoPostBack="true" OnTextChanged="txtBuyerSaleRate_TextChanged" TabIndex="9"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyerSaleRate" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtBuyerSaleRate">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">BP Account:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtBP_Account" runat="server" CssClass="txt" Width="100px" Style="text-align: right;"
                                AutoPostBack="false" OnTextChanged="txtBP_Account_TextChanged" TabIndex="10" Height="24px" onkeydown="bpac(event);"></asp:TextBox>
                            <asp:Button ID="btnBP_Account" runat="server" Text="..." CssClass="btnHelp" OnClick="btnBP_Account_Click"
                                Height="24px" Width="20px" />
                            <asp:Label ID="lblBP_Account" runat="server" CssClass="lblName"></asp:Label>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">B.P
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtcashdifference" runat="server" Width="80px" Height="24px" CssClass="txt"
                                AutoPostBack="true" OnTextChanged="txtcashdifference_TextChanged" TabIndex="11"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtBuyerSaleRate" runat="server" ControlToValidate="txtBuyerSaleRate"
                                CssClass="validator" Display="Dynamic" ErrorMessage="Required" SetFocusOnError="true"
                                Text="Required" ValidationGroup="addBuyerDetails">
                            </asp:RequiredFieldValidator>
                            <asp:Label ID="lblcashdifferencevalue" runat="server" ForeColor="Yellow"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Commission:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtBuyerCommission" runat="server" CssClass="txt" Height="24px"
                                TabIndex="12" Width="80px" AutoPostBack="true" OnTextChanged="txtBuyerCommission_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="filtertxtBuyerCommission" FilterType="Custom,Numbers"
                                ValidChars="." TargetControlID="txtBuyerCommission">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Sauda Date:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtDetailSaudaDate" runat="server" CssClass="txt" Width="100px"
                                AutoPostBack="True" MaxLength="13" onkeyup="ValidateDate(this,event.keyCode)"
                                onkeydown="return DateFormat(this,event.keyCode)" OnTextChanged="txtDetailSaudaDate_TextChanged"
                                TabIndex="10" Height="24px"></asp:TextBox>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                Height="15px" />
                            <ajax1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDetailSaudaDate"
                                PopupButtonID="Image1" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Payment Date:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtDetailLiftingDate" runat="server" CssClass="txt" Width="100px"
                                AutoPostBack="True" MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)"
                                onkeydown="return DateFormat(this,event.keyCode)" OnTextChanged="txtDetailLiftingDate_TextChanged"
                                TabIndex="14" Height="24px"></asp:TextBox>
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                Height="15px" />
                            <ajax1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDetailLiftingDate"
                                PopupButtonID="Image2" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Narration:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtBuyerNarration" runat="server" Width="360px" CssClass="txt" Height="50px"
                                TextMode="MultiLine" TabIndex="15" AutoPostBack="true" OnTextChanged="txtBuyerNarration_TextChanged"></asp:TextBox>
                            <%--<td colspan="2" align="center">--%>                                                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">GST Amount:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtGSTrate" runat="server" Width="60px" CssClass="txt" Height="24px"
                                TabIndex="16" AutoPostBack="true" OnTextChanged="txtGSTrate_TextChanged"></asp:TextBox>

                            <asp:TextBox ID="txtgstamt" runat="server" Width="100px" CssClass="txt" Height="24px"
                                TabIndex="17" AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">TCS Amount:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:TextBox ID="txtTCSrate1" runat="server" Width="60px" CssClass="txt" Height="24px"
                                TabIndex="18" AutoPostBack="true" OnTextChanged="txtTCSrate1_TextChanged"></asp:TextBox>

                            <asp:TextBox ID="txtTCSamount1" runat="server" Width="100px" CssClass="txt" Height="24px"
                                TabIndex="19" AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Net Amount:
                        </td>
                        <td align="left" style="width: 20%;">

                            <asp:Label ID="lblNetAmt" runat="server" ForeColor="Yellow"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 30%;">Loading By Us:
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:CheckBox runat="server"
                                TabIndex="20" ID="chkLoding_Chk" Width="10px" Height="10px"  />
                        </td>
                    </tr>
                    <tr align="right">
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnHelp" TabIndex="21"
                                Width="90px" Height="24px" ValidationGroup="save" OnClick="btnUpdate_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <br />
            <br />

            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="85%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%; margin: 0 auto;">
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
                            <asp:Panel ID="pnlInner" runat="server" ScrollBars="Both" Width="100%" Direction="LeftToRight"
                                BackColor="#FFFFE4" Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    AllowPaging="true" PageSize="13" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

