<%@ Page Title="System Master" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeSystemMaster.aspx.cs" Inherits="Sugar_pgeSystemMaster" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


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
                if (hdnfClosePopupValue == "txtPurchaseAc") {
                    document.getElementById("<%=txtPurchaseAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSaleAC") {
                    document.getElementById("<%=txtSaleAC.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtvatAC") {
                    document.getElementById("<%=txtvatAC.ClientID %>").focus();
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

                if (hdnfClosePopupValue == "txtsystemcode") {
                    document.getElementById("<%=txtsystemcode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;

                    document.getElementById("<%=txtsystemcode.ClientID %>").disabled = false;
                    document.getElementById("<%=txtsystemName.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtPurchaseAc") {
                    document.getElementById("<%=txtPurchaseAc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblPurchaseACName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtPurchaseAc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSaleAC") {
                    document.getElementById("<%=txtSaleAC.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblSaleACName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtSaleAC.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtvatAC") {
                    document.getElementById("<%=txtvatAC.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblVatACName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtvatAC.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtgst_Code") {
                    document.getElementById("<%=txtgst_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblgstcode.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtgst_Code.ClientID %>").focus();
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
        function purchesAmount(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtPurchaseAc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtPurchaseAc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtPurchaseAc.ClientID %>").val(unit);
                __doPostBack("txtPurchaseAc", "TextChanged");

            }

        }
        function saleAmount(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtSaleAC.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtSaleAC.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtSaleAC.ClientID %>").val(unit);
                __doPostBack("txtSaleAC", "TextChanged");

            }

        }

        function vatamount(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtvatAC.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtvatAC.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtvatAC.ClientID %>").val(unit);
                __doPostBack("txtvatAC", "TextChanged");

            }

        }
        function gstcode(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtgst_code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtgst_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtgst_Code.ClientID %>").val(unit);
                __doPostBack("txtgst_Code", "TextChanged");

            }

        }
    </script>
    <script type="text/javascript">
        function systemmaster() {
            var Action = 2;
            var Group_Code = 0;
            //alert(td);
            window.open('../Master/PgeSystemMasterUtility.aspx', '_self');
        }
        function SY(System_Code, Action, System_Type) {

            window.open('../Master/pgeSystemMaster.aspx?systemid=' + System_Code + '&Action=' + Action + '&System_Type=' + System_Type, '_self');

        }
    </script>
    <script type="text/javascript">
        function validation() {
            debugger;
            var txtsystemName = $("#<%=txtsystemName.ClientID %>").val();
            if ((txtsystemName == "") || txtsystemName == "0") {
                $("#<%=txtsystemName.ClientID %>").focus();
                return;
            }
            var txtHsnNumber = $("#<%=txtHsnNumber.ClientID %>").val();
            if ((txtHsnNumber == "") || txtHsnNumber == "0") {
                $("#<%=txtHsnNumber.ClientID %>").focus();
                return;
            }
            var txtgst_Code = $("#<%=txtgst_Code.ClientID %>").val();
            if ((txtgst_Code == "") || txtgst_Code == "0") {
                $("#<%=txtgst_Code.ClientID %>").focus();
                return;
            }

            var drpSystype = $("#<%=drpSystype.ClientID %>").val();
            if ((drpSystype == "V")) {
                // $("#<%=drpSystype.ClientID %>").focus();
                var txtSysRate = $("#<%=txtSysRate.ClientID %>").val()
                if ((txtSysRate == "") || txtSysRate == "0") {
                    $("#<%=txtSysRate.ClientID %>").focus();
                    return;
                }

            }


            var insertrecord = "";
            var systemcode = 0, systemid = 0;
            var DOC_NO = $("#<%=txtsystemcode.ClientID %>").val();

            var status = document.getElementById("<%= btnSave.ClientID %>").value;
            debugger;
            if (status == "Update") {
                systemcode = document.getElementById("<%= hdnfsystemdoc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfsystemdoc.ClientID %>").value;
                systemid = document.getElementById("<%= hdnfsystemid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfsystemid.ClientID %>").value;
            }
            var spname = "SystemMaster";
            var XML = "<ROOT>";
            var System_Type = $("#<%=drpSystype.ClientID %>").val() == "" ? 0 : $("#<%=drpSystype.ClientID %>").val();
            var System_Name_E = $("#<%=txtsystemName.ClientID %>").val() == "" ? 0 : $("#<%=txtsystemName.ClientID %>").val();
            var  System_Name_R = $("#<%=txtSystemNameR.ClientID %>").val() == "" ? 0 : $("#<%=txtSystemNameR.ClientID %>").val();
            var System_Rate = 0;
            var Purchase_AC = $("#<%=txtPurchaseAc.ClientID %>").val() == "" ? 0 : $("#<%=txtPurchaseAc.ClientID %>").val();
            var Sale_AC = $("#<%=txtSaleAC.ClientID %>").val() == "" ? 0 : $("#<%=txtSaleAC.ClientID %>").val();
            var Vat_AC = 0;
            var Opening_Bal = $("#<%=txtOpeningBal.ClientID %>").val() == "" ? 0 : $("#<%=txtOpeningBal.ClientID %>").val();
            var KgPerKatta = $("#<%=txtKgPerKatta.ClientID %>").val() == "" ? 0 : $("#<%=txtKgPerKatta.ClientID %>").val();
            var minRate = $("#<%=txtminRate.ClientID %>").val() == "" ? 0 : $("#<%=txtminRate.ClientID %>").val();
            var maxRate = $("#<%=txtmaxRate.ClientID %>").val() == "" ? 0 : $("#<%=txtmaxRate.ClientID %>").val();
            var HSNNumber = $("#<%=txtHsnNumber.ClientID %>").val() == "" ? 0 : $("#<%=txtHsnNumber.ClientID %>").val();
            var Opening_Value = $("#<%=txtOpening_Value.ClientID %>").val() == "" ? 0 : $("#<%=txtOpening_Value.ClientID %>").val();
            var pac = document.getElementById("<%= hdnfpac.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpac.ClientID %>").value;
            var sac = document.getElementById("<%= hdnfsac.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfsac.ClientID %>").value;
            var MarkaSet = $("#<%=drpMarketSet.ClientID %>").val();
            if (MarkaSet == " " || MarkaSet == "&nbsp;") {
                MarkaSet = "";
            }
            var Supercost = $("#<%=drpsupercost.ClientID %>").val();
            if (Supercost == " " || Supercost == "&nbsp;") {
                Supercost = "";
            }
            var Packing = $("#<%=drpPacking.ClientID %>").val();
            if (Packing == " " || Packing == "&nbsp;") {
                Packing = "";
            }
            var LodingGst = $("#<%=drpLodingGst.ClientID %>").val();
            if (LodingGst == " " || LodingGst == "&nbsp;") {
                LodingGst = "";
            }
            var RatePer = $("#<%=drpRatePer.ClientID %>").val();
            if (RatePer == " " || RatePer == "&nbsp;") {
                RatePer = "";
            }
            var MarkaPerc = $("#<%=txtMarketSale_Perc.ClientID %>").val() == "" ? 0 : $("#<%=txtMarketSale_Perc.ClientID %>").val();
            var SuperPerc = $("#<%=txtSupercost_Perc.ClientID %>").val() == "" ? 0 : $("#<%=txtSupercost_Perc.ClientID %>").val();
            var Gst_Code = $("#<%=txtgst_Code.ClientID %>").val() == "" ? 0 : $("#<%=txtgst_Code.ClientID %>").val();

            var IsService = $("#<%=drpIsService.ClientID %>").val();
            if (IsService == " " || IsService == "&nbsp;") {
                IsService = "";
            }


            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';
            var USER = '<%= Session["user"] %>';
            if (status == "Save") {
                insertrecord = "Created_By='" + USER + "' Modified_By=''";

            }
            else {

                insertrecord = "Created_By='" + USER + "' Modified_By='" + USER + "'";
            }
            XML = XML + "<SystemHead System_Type='" + System_Type + "' System_Code='" + systemcode + "' System_Name_E='" + System_Name_E + "' System_Name_R='" + System_Name_R + "' " +
                "System_Rate='" + System_Rate + "' Purchase_AC='" + Purchase_AC + "' pac='" + pac + "' Sale_AC='" + Sale_AC + "' sac='" +sac + "' Vat_AC='" + Vat_AC + "' Opening_Bal='" + Opening_Bal + "' " +
                "KgPerKatta='" + KgPerKatta + "' minRate='" + minRate + "' maxRate='" + maxRate + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='0' " + insertrecord + " " +
                "HSN='" + HSNNumber + "' systemid='" + systemid + "' Opening_Value='" + Opening_Value + "' Gst_Code='" + Gst_Code + "' MarkaSet='" + MarkaSet + "' Supercost='" + Supercost + "' " +
                "Packing='" + Packing + "' LodingGst='" + LodingGst + "' MarkaPerc='" + MarkaPerc + "' SuperPerc='" + SuperPerc + "' RatePer='" + RatePer + "' IsService='" + IsService + "'/></ROOT>";
            $.ajax({
                type: "POST",
                url: "../xmlExecuteDMLQry.asmx/ExecuteXMLQry",
                data: '{XML:"' + XML + '",status:"' + status + '",spname:"' + spname + '"}',
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

            function OnSuccess(response) {
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
                    var action = 1;

                    window.open('../Master/pgeSystemMaster.aspx?systemid=' + id + '&Action=' + action + '&System_Type=' + System_Type, "_self");
                }
            }


        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
            <legend style="text-align: center;">
                <asp:Label ID="label1" runat="server" Text="   System Master   " Font-Names="verdana"
                    ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
        </fieldset>


        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hdconfirm" runat="server" />
                <asp:HiddenField ID="hdnfClosePopup" runat="server" />
                <asp:HiddenField ID="hdnf" runat="server" />
                <asp:HiddenField ID="hdnfTranType" runat="server" />
                <asp:HiddenField ID="hdHelpPageCount" runat="server" />
                <asp:HiddenField ID="hdnfsystemdoc" runat="server" />
                <asp:HiddenField ID="hdnfsystemid" runat="server" />
                <asp:HiddenField ID="hdnfyearcode" runat="server" />
                <asp:HiddenField ID="hdnfcompanycode" runat="server" />
                <asp:HiddenField ID="hdnfpac" runat="server" />
                <asp:HiddenField ID="hdnfsac" runat="server" />
                <table width="80%" align="left">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                            &nbsp;
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                                TabIndex="19" ValidationGroup="add" OnClick="btnSave_Click" Height="24px" />
                            &nbsp;
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                                ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                            &nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                                ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                                TabIndex="19" ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" />
                            &nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" Height="24px" TabIndex="20" OnClientClick="systemmaster()" />
                        </td>

                    </tr>
                </table>
                <center>
                    <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                        Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                        <table style="width: 80%;" align="left" cellpadding="3" cellspacing="5">
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                        Font-Size="Small" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" style="width: 30%;">Change No
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                        TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged" Visible="true"></asp:TextBox>
                                    &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                        Font-Size="Small" ForeColor="Red" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%;">System Type:
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList ID="drpSystype" runat="server" CssClass="ddl" Width="200px" TabIndex="1"
                                        Height="25px" AutoPostBack="true" OnTextChanged="drpSystype_TextChanged">
                                        <asp:ListItem Text="Mobile Group" Value="G"></asp:ListItem>
                                        <asp:ListItem Text="Narration" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Vat" Value="V"></asp:ListItem>
                                        <asp:ListItem Text="Item" Value="I"></asp:ListItem>
                                        <asp:ListItem Text="Grade" Value="S"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">System Code:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtsystemcode" runat="Server" CssClass="txt" TabIndex="2" Width="80px"
                                        Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtsystemcode_TextChanged"
                                        Height="24px"></asp:TextBox>
                                    <asp:Label ID="lblbsid" runat="server" Font-Size="Medium" Font-Names="verdana"
                                        Font-Bold="true" ForeColor="Blue" Visible="false"></asp:Label>
                                    <asp:Button ID="btntxtsystemcode" runat="server" Text="..." Width="80px" OnClick="btntxttxtsystemcode_click"
                                        CssClass="btnHelp" Visible="false" Height="24px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">System Name:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtsystemName" runat="Server" TabIndex="3" CssClass="txt"
                                        Width="500px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtsystemName_TextChanged"
                                        Height="24px"></asp:TextBox>
                                    <%-- <ajax1:FilteredTextBoxExtender ID="FilteredtxtsystemName" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                        ValidChars="." TargetControlID="txtsystemName">
                                    </ajax1:FilteredTextBoxExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">System Name Regional:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSystemNameR" runat="Server" TabIndex="4" CssClass="txt"
                                        Width="500px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSystemNameR_TextChanged"
                                        Height="24px"></asp:TextBox>
                                    <%-- <ajax1:FilteredTextBoxExtender ID="FilteredtxtsystemName" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                        ValidChars="." TargetControlID="txtsystemName">
                                    </ajax1:FilteredTextBoxExtender>--%>
                                </td>
                            </tr>

                            <tr>
                                <td align="right">Purchase Account:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPurchaseAc" runat="server" CssClass="txt" Width="80px" AutoPostBack="false"
                                        OnTextChanged="txtPurchaseAc_TextChanged" TabIndex="5" Height="24px" onkeydown="purchesAmount(event);"></asp:TextBox>
                                    <asp:Button ID="btntxtPurchaseAc" runat="server" CssClass="btnHelp" Text="..." OnClick="btntxtPurchaseAc_Click"
                                        Height="24px" Width="20px" />
                                    <asp:Label ID="lblPurchaseACName" runat="server" CssClass="lblName"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Sale Account:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSaleAC" runat="server" CssClass="txt" Width="80px" AutoPostBack="false"
                                        OnTextChanged="txtSaleAC_TextChanged" TabIndex="6" Height="24px" onkeydown="saleAmount(event);"></asp:TextBox>
                                    <asp:Button ID="btntxtSaleAC" runat="server" CssClass="btnHelp" Text="..." OnClick="btntxtSaleAC_Click"
                                        Height="24px" Width="20px" />
                                    <asp:Label ID="lblSaleACName" runat="server" CssClass="lblName"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td align="right">Opening Balance:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOpeningBal" runat="server" CssClass="txt" Width="103px" AutoPostBack="True"
                                        OnTextChanged="txtOpeningBal_TextChanged" TabIndex="8" Height="24px"></asp:TextBox>
                                    <ajax1:FilteredTextBoxExtender ID="FilteretxtOpeningBal" runat="server" FilterType="Numbers,Custom"
                                        ValidChars="." TargetControlID="txtOpeningBal">
                                    </ajax1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Katt/Kg:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtKgPerKatta" runat="server" CssClass="txt" Width="103px" AutoPostBack="false"
                                        Enabled="false" TabIndex="9" Height="24px"></asp:TextBox>
                                    <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                        ValidChars="." TargetControlID="txtKgPerKatta">
                                    </ajax1:FilteredTextBoxExtender>

                                  &emsp; &emsp;    Min Rate:
                                <asp:TextBox ID="txtminRate" runat="Server" CssClass="txt" TabIndex="10" Width="130px"
                                Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtminRate_TextChanged"
                                Height="24px"></asp:TextBox>
                                  &emsp; &emsp;   Max Rate:
                                <asp:TextBox ID="txtmaxRate" runat="Server" CssClass="txt" TabIndex="11" Width="130px"
                                     Style="text-align: left;" Height="24px"></asp:TextBox>
                           
                                </td>
                            </tr>
                            <tr>
                                <td align="right">HSN / ASC :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtHsnNumber" runat="server" CssClass="txt" Width="150px" TabIndex="12"
                                        Height="24px"></asp:TextBox>
                                    <ajax1:FilteredTextBoxExtender ID="FilteredtxtHsnNumber" runat="server" FilterType="Numbers,Custom"
                                        ValidChars="." TargetControlID="txtHsnNumber">
                                    </ajax1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Opening Value :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOpening_Value" runat="server" CssClass="txt" Width="150px" TabIndex="12"
                                        Height="24px"></asp:TextBox>
                                    <ajax1:FilteredTextBoxExtender ID="FilteredtxtOpening_Value" runat="server" FilterType="Numbers,Custom"
                                        ValidChars="." TargetControlID="txtOpening_Value">
                                    </ajax1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">GST Code:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtgst_Code" runat="server" CssClass="txt" Width="80px" onkeydown="gstcode(event);" AutoPostBack="false"
                                        OnTextChanged="txtgst_Code_TextChanged" TabIndex="13" Height="24px"></asp:TextBox>
                                    <asp:Button ID="btntxtgst_code" runat="server" CssClass="btnHelp" Text="..." OnClick="btntxtgst_code_Click"
                                        Height="24px" Width="20px" />
                                    <asp:Label ID="lblgstcode" runat="server" CssClass="lblName"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%;">Market Sales:
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList ID="drpMarketSet" runat="server" CssClass="ddl" Width="180px" TabIndex="14"
                                        Height="25px">
                                        <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="N" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="" Value=""></asp:ListItem>

                                    </asp:DropDownList>
                                    &emsp;
                                     <asp:TextBox ID="txtMarketSale_Perc" runat="server" CssClass="txt" Width="80px" AutoPostBack="false"
                                         TabIndex="15" Height="24px"></asp:TextBox>
                                    &nbsp;<b>%</b>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%;">Suparcost:
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList ID="drpsupercost" runat="server" CssClass="ddl" Width="180px" TabIndex="15"
                                        Height="25px">

                                        <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="N" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    &emsp;
                                    <asp:TextBox ID="txtSupercost_Perc" runat="server" CssClass="txt" Width="80px" AutoPostBack="false"
                                        TabIndex="16" Height="24px"></asp:TextBox>
                                    &nbsp;<b>%</b>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%;">Packing:
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList ID="drpPacking" runat="server" CssClass="ddl" Width="180px" TabIndex="15"
                                        Height="25px">

                                        <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="N" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%;">Including GST:
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList ID="drpLodingGst" runat="server" CssClass="ddl" Width="180px" TabIndex="16"
                                        Height="25px">

                                        <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="N" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%;">Rate Per:
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList ID="drpRatePer" runat="server" CssClass="ddl" Width="180px" TabIndex="17"
                                        Height="25px">

                                        <asp:ListItem Text="Quantity" Value="Q" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Quital" Value="K"></asp:ListItem>
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%;">Is Service:
                                </td>
                                <td align="left" style="width: 50%;">
                                    <asp:DropDownList ID="drpIsService" runat="server" CssClass="ddl" Width="180px" TabIndex="18"
                                        Height="25px">
                                        <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="N" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>

                                <td align="left">
                                    <asp:TextBox ID="txtSysRate" runat="Server" CssClass="txt" TabIndex="4" Width="102px"
                                        AutoPostBack="true" Style="text-align: right;" OnTextChanged="txtSysRate_TextChanged"
                                        Height="24px" Visible="false"></asp:TextBox>
                                    <ajax1:FilteredTextBoxExtender ID="filterrate" runat="server" FilterType="Numbers,Custom"
                                        ValidChars="." TargetControlID="txtSysRate">
                                    </ajax1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>

                                <td align="left">
                                    <asp:TextBox ID="txtvatAC" runat="server" CssClass="txt" Width="80px" AutoPostBack="false" Visible="false"
                                        OnTextChanged="txtvatAC_TextChanged" TabIndex="7" Height="24px" onkeydown="vatamount(event);"></asp:TextBox>
                                    <asp:Button ID="btntxtvatAC" runat="server" CssClass="btnHelp" Text="..." OnClick="btntxtvatAC_Click"
                                        Height="24px" Width="20px" Visible="false" />
                                    <asp:Label ID="lblVatACName" runat="server" CssClass="lblName"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </center>

                <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%"
                    align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
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
                                <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                    Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                    <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                                        AllowPaging="true" PageSize="20" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                        HeaderStyle-ForeColor="White" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                        OnRowCreated="grdPopup_RowCreated" Style="table-layout: fixed;" OnRowDataBound="grdPopup_RowDataBound">
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


    </center>
</asp:Content>
