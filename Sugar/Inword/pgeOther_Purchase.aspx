<%@ Page Title="Other Purchase" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeOther_Purchase.aspx.cs" Inherits="pgeOther_Purchase" %>

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
    <script type="text/javascript" language="javascript">

        document.addEventListener('keyup', function (event) {
            if (event.defaultPrevented) {
                return;
            }

            var key = event.key || event.keyCode;

            if (key === 'Escape' || key === 'Esc' || key === 27) {
                //                doWhateverYouWantNowThatYourKeyWasHit();

                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                if (hdnfClosePopupValue == "txtSupplier_Code") {
                    document.getElementById("<%=txtSupplier_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtExp_Ac") {
                    document.getElementById("<%=txtExp_Ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGST_RateCode") {
                    document.getElementById("<%=txtGST_RateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTDSCuttAc_Code") {
                    document.getElementById("<%=txtTDSCuttAc_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTDSAc_Code") {
                    document.getElementById("<%=txtTDSAc_Code.ClientID %>").focus();
                }
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
            }

        });
    </script>
    <script type="text/javascript">
        function Back() {
            window.open('../Inword/pgeOtherPurchaseUtility.aspx', '_self');
        }

        function OPPen(DO) {
            var Action = 1;
            window.open('../Inword/pgeOther_Purchase.aspx?opid=' + DO + '&Action=' + Action, "_self");
        }
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
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = "";
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSupplier_Code") {                  
                    document.getElementById("<%= txtSupplier_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblSupplier_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;

                    document.getElementById("<%= txtTDSCuttAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblTDSCuttAc_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtExp_Ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtExp_Ac") {
                    document.getElementById("<%= txtExp_Ac.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblExp_Ac.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtExp_Ac.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGST_RateCode") {
                    document.getElementById("<%= txtGST_RateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblGST_RateCode.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtGST_RateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTDSCuttAc_Code") {
                    document.getElementById("<%= txtTDSCuttAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblTDSCuttAc_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtTDSCuttAc_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtTDSAc_Code") {
                    document.getElementById("<%= txtTDSAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblTDSAc_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtTDSAc_Code.ClientID %>").focus();
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
        function chanegno(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtEditDoc_No";
                ("#<%=hdnfClosePopup.ClientID %> ").val(edi);
                ("#<%=btnSearch.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtEditDoc_No", "TextChanged");
            }
        }

        function supplier(e) {

            if (e.keyCode == 112) {
                e.preventDefault();
                // $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtSupplier_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtSupplier_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtSupplier_Code.ClientID %>").val(unit);
                __doPostBack("txtSupplier_Code", "TextChanged");

            }
        }

        function expaccount(e) {

            if (e.keyCode == 112) {
                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtExp_Ac.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtExp_Ac.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtExp_Ac.ClientID %>").val(unit);
                __doPostBack("txtExp_Ac", "TextChanged");

            }

        }

        function gstcode(e) {

            if (e.keyCode == 112) {
                e.preventDefault();
                // $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtGST_RateCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtGST_RateCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtGST_RateCode.ClientID %>").val(unit);
                __doPostBack("txtGST_RateCode", "TextChanged");

            }
        }

        function tdscutting(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtCuttTDSAc_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtTDSCuttAc_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtTDSCuttAc_Code.ClientID %>").val(unit);
                __doPostBack("txtTDSCuttAc_Code", "TextChanged");

            }
        }
        function tdsac(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtTDSAc_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtTDSAc_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtTDSAc_Code.ClientID %>").val(unit);
                __doPostBack("txtTDSAc_Code", "TextChanged");

            }


        }     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Other Purchase" Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfpopup" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
            <table width="80%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            TabIndex="24" ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            TabIndex="25" ValidationGroup="add" OnClick="btnSave_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            TabIndex="26" ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            TabIndex="27" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()"
                            Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            TabIndex="28" ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                            TabIndex="29" ValidationGroup="save" Height="24px" OnClientClick="Back();" />
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
                        <td align="left">
                            Change No
                        </td>
                        <td align="left">
                            <asp:TextBox Height="24px" ID="txtEditDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Entry No:
                        </td>
                        <td>
                            <asp:TextBox Height="24px" ID="txtDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtDoc_No_TextChanged"></asp:TextBox>
                            <asp:Button Width="70px" Height="24px" ID="btntxtDoc_No" runat="server" Text="..."
                                OnClick="btntxtDoc_No_Click" CssClass="btnHelp" Visible="false" />
                            <asp:Label ID="lblDoc_No" runat="server" CssClass="lblName" Visible="false"></asp:Label>&nbsp;&nbsp;
                            Date:
                            <asp:TextBox Height="24px" ID="txtDoc_Date" runat="Server" CssClass="txt" TabIndex="2"
                                Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtDoc_Date_TextChanged"
                                onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtDoc_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtDoc_Date"
                                    runat="server" TargetControlID="txtDoc_Date" PopupButtonID="imgcalendertxtDoc_Date"
                                    Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Supplier:
                        </td>
                        <td>
                            <asp:TextBox Height="24px" ID="txtSupplier_Code" runat="Server" CssClass="txt" TabIndex="3"
                                Width="90px" Style="text-align: left;" OnkeyDown="supplier(event);" AutoPostBack="false"
                                OnTextChanged="txtSupplier_Code_TextChanged"></asp:TextBox>
                            <asp:Button Width="20px" Height="24px" ID="btntxtSupplier_Code" runat="server" Text="..."
                                OnClick="btntxtSupplier_Code_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblSupplier_Code" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Exp A/C:
                        </td>
                        <td>
                            <asp:TextBox Height="24px" ID="txtExp_Ac" runat="Server" CssClass="txt" TabIndex="4"
                                Width="90px" Style="text-align: left;" OnkeyDown="expaccount(event);" AutoPostBack="false"
                                OnTextChanged="txtExp_Ac_TextChanged"></asp:TextBox>
                            <asp:Button Width="20px" Height="24px" ID="btntxtExp_Ac" runat="server" Text="..."
                                OnClick="btntxtExp_Ac_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblExp_Ac" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            GST Code:
                        </td>
                        <td>
                            <asp:TextBox Height="24px" ID="txtGST_RateCode" runat="Server" CssClass="txt" TabIndex="5"
                                Width="90px" Style="text-align: left;" OnkeyDown="gstcode(event);" AutoPostBack="false"
                                OnTextChanged="txtGST_RateCode_TextChanged"></asp:TextBox>
                            <asp:Button Width="20px" Height="24px" ID="btntxtGST_RateCode" runat="server" Text="..."
                                OnClick="btntxtGST_RateCode_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblGST_RateCode" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Taxable Amount:
                        </td>
                        <td>
                            <asp:TextBox Height="24px" ID="txtTaxable_Amount" runat="Server" CssClass="txt" TabIndex="6"
                                Width="90px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtTaxable_Amount_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            CGST %:
                        </td>
                        <td>
                            <asp:TextBox Height="24px" ID="txtCGST_Rate" runat="Server" CssClass="txt" TabIndex="7"
                                Width="40px" ReadOnly="true" Style="text-align: right;" AutoPostBack="false"
                                OnTextChanged="txtCGST_Rate_TextChanged"></asp:TextBox>
                            <asp:TextBox Height="24px" ReadOnly="true" ID="txtCGST_Amount" runat="Server" CssClass="txt"
                                TabIndex="8" Width="90px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtCGST_Amount_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            SGST %:
                        </td>
                        <td>
                            <asp:TextBox Height="24px" ID="txtSGST_Rate" runat="Server" CssClass="txt" TabIndex="9"
                                Width="40px" ReadOnly="true" Style="text-align: right;" AutoPostBack="false"
                                OnTextChanged="txtSGST_Rate_TextChanged"></asp:TextBox>
                            <asp:TextBox Height="24px" ReadOnly="true" ID="txtSGST_Amount" runat="Server" CssClass="txt"
                                TabIndex="10" Width="90px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtSGST_Amount_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            IGST %:
                        </td>
                        <td>
                            <asp:TextBox Height="24px" ID="txtIGST_Rate" runat="Server" CssClass="txt" TabIndex="11"
                                Width="40px" ReadOnly="true" Style="text-align: right;" AutoPostBack="false"
                                OnTextChanged="txtIGST_Rate_TextChanged"></asp:TextBox>
                            <asp:TextBox Height="24px" ID="txtIGST_Amount" runat="Server" CssClass="txt" TabIndex="12"
                                Width="90px" ReadOnly="true" Style="text-align: right;" AutoPostBack="false"
                                OnTextChanged="txtIGST_Amount_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Other Amount:
                        </td>
                        <td>
                            <asp:TextBox Height="24px" ID="txtOther_Amount" runat="Server" CssClass="txt" TabIndex="13"
                                Width="90px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtOther_Amount_TextChanged"></asp:TextBox>
                            Bill Amount
                            <asp:TextBox Height="24px" ID="txtBill_Amount" runat="Server" CssClass="txt" TabIndex="14"
                                Width="90px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtBill_Amount_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            TDS Amt:
                        </td>
                        <td>
                            <asp:TextBox Height="24px" ID="txtTDS_Amt" runat="Server" CssClass="txt" TabIndex="15"
                                Width="90px" Style="text-align: right;" AutoPostBack="true" OnTextChanged="txtTDS_Amt_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            TDS %:
                        </td>
                        <td>
                            <asp:TextBox Height="24px" ID="txtTDS_Per" runat="Server" CssClass="txt" TabIndex="16"
                                Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtTDS_Per_TextChanged"></asp:TextBox>
                            TDS
                            <asp:TextBox Height="24px" ID="txtTDS" runat="Server" CssClass="txt" TabIndex="17"
                                Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtTDS_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            TDS Cutting Ac:
                        </td>
                        <td>
                            <asp:TextBox Height="24px" ID="txtTDSCuttAc_Code" runat="Server" CssClass="txt" TabIndex="18"
                                Width="90px" Style="text-align: left;" OnkeyDown="tdscutting(event);" AutoPostBack="false"
                                OnTextChanged="txtTDSCuttAc_Code_TextChanged"></asp:TextBox>
                            <asp:Button Width="20px" Height="24px" ID="btntxtCuttTDSAc_Code" runat="server" Text="..."
                                OnClick="btntxtCuttTDSAc_Code_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblTDSCuttAc_Code" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            TDS Ac:
                        </td>
                        <td>
                            <asp:TextBox Height="24px" ID="txtTDSAc_Code" runat="Server" CssClass="txt" TabIndex="19"
                                Width="90px" Style="text-align: left;" OnkeyDown="tdsac(event);" AutoPostBack="false"
                                OnTextChanged="txtTDSAc_Code_TextChanged"></asp:TextBox>
                            <asp:Button Width="20px" Height="24px" ID="btntxtTDSAc_Code" runat="server" Text="..."
                                OnClick="btntxtTDSAc_Code_Click" CssClass="btnHelp" />
                            <asp:Label ID="lblTDSAc_Code" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Bill No:
                        </td>
                        <td>
                            <asp:TextBox Height="24px" ID="txtbillno" runat="Server" CssClass="txt" TabIndex="20"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtbillno_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            ASN No.:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtasnno" CssClass="txt" Width="300px" Height="24px"
                                TabIndex="21"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            EInvoice No.:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txteinvoice" CssClass="txt" Width="300px" Height="24px"
                                TabIndex="22"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Narration:
                        </td>
                        <td>
                            <asp:TextBox Height="70px" ID="txtNarration" runat="Server" CssClass="txt" TabIndex="23"
                                Width="300px" Style="text-align: left;" AutoPostBack="false" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <%--<fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
                    margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
                    border-left: 0px; border-right: 0px; height: 7px;">
                    <legend style="text-align: center;">
                        <h5 style="color: White;" font-names="verdana" font-size="Medium">
                            Detail Entry</h5>
                </fieldset>--%>
            </asp:Panel>
            <asp:Panel onkeydown="closepopup(event);" ID="pnlPopup" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px;
                min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center;
                left: 10%; top: 10%;">
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
                        <td>
                            Search Text:
                            <asp:TextBox onkeydown="SelectFirstRow(event);" ID="txtSearchText" runat="server"
                                Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button onkeydown="closepopup(event);" ID="btnSearch" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView Font-Bold="true" CssClass="select" AllowPaging="true" PageSize="20"
                                    OnPageIndexChanging="grdPopup_PageIndexChanging" ID="grdPopup" runat="server"
                                    AutoGenerateColumns="true" Width="100%" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
