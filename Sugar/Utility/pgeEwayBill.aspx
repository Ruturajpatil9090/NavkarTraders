<%@ Page Title="E-WayBill" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeEwayBill.aspx.cs" Inherits="pgeEwayBill" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

        function DoOpen(DO, Action) {
            window.open('../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx?DO=' + DO + '&Action=' + Action, "_self");;
        }
        function SBOpen(DO, Action) {
            window.open('../Outword/pgrSugarsaleForGSTxml.aspx?saleid=' + DO + '&Action=' + Action, "_self");
        }
        function RSOpen(DO, Action) {
            window.open('../Outword/pgeSugarsaleReturnForGST.aspx?srid=' + DO + '&Action=' + Action, "_self");
        }
        function RROpen(DO, Action, trantype) {
            window.open('../Outword/pgeRetailSale.aspx?Retailid=' + DO + '&Action=' + Action + '&Tran_Type=' + trantype, "_self");
        }
        function JSOpen() {
            window.open('../Jaggary/pgeJawakSaleBill.aspx');

        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="E-WayBill Entry" Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfCGST_Rate" runat="server" />
            <asp:HiddenField ID="hdnfSGST_Rate" runat="server" />
            <asp:HiddenField ID="hdnfIGST_Rate" runat="server" />
            <asp:HiddenField ID="hdnfState_Code_BillTo" runat="server" />
            <asp:HiddenField ID="hdnfMillState_Code" runat="server" />
            <asp:HiddenField ID="hdnfState_Code" runat="server" />
            <asp:HiddenField ID="hdnfmillCode" runat="server" />
            <asp:HiddenField ID="hdnfUnitCode" runat="server" />
            <table width="80%" align="left">
                <tr>
                    <td align="center">

                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                        &nbsp;

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnGenEwayBill" runat="server" Text="Generate EWayBill" CssClass="btnHelp"
                            Width="120px" ValidationGroup="save" OnClick="btnGenEwayBill_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnUpdatePincode" runat="server" Text="Update Pincode" CssClass="btnHelp"
                            Width="120px" ValidationGroup="save" OnClick="btnUpdatePincode_Click" Height="24px" />
                    </td>
                    
                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table align="Left" width="100%" cellpadding="4" cellspacing="4">
                    <%--<asp:Label runat="ser1ver" Text="Transaction Details" ID="lblTranDetails" ForeColor="White"></asp:Label> --%>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <h3 style="color: White; font-family: Verdana; border-top: 1px solid rgb(131, 127, 130); border-bottom: 1px solid rgb(131, 127, 130); text-align: left">Transaction Details:</h3>
                            <br />
                            Change No
                            <asp:TextBox Height="24px" ID="txtEditDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Supply Type
                            <asp:DropDownList ID="drpSupply_Type" runat="Server" Height="24px" CssClass="txt"
                                TabIndex="2" Width="90px" AutoPostBack="false" OnSelectedIndexChanged="drpSupply_Type_SelectedIndexChanged">
                                <asp:ListItem Text="Outward" Value="O" />
                                <asp:ListItem Text="Inward" Value="I" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            Sub Type
                            <asp:DropDownList ID="drpSub_Type" Height="24px" runat="Server" CssClass="txt" TabIndex="3"
                                Width="150px" AutoPostBack="false" OnSelectedIndexChanged="drpSub_Type_SelectedIndexChanged">
                                <asp:ListItem Text="Supply" Value="1" />
                                <asp:ListItem Text="Export" Value="3" />
                                <asp:ListItem Text="Job Work" Value="4" />
                                <asp:ListItem Text="SKD/CKD/Lots" Value="9" />
                                <asp:ListItem Text="Recipient Not Known" Value="11" />
                                <asp:ListItem Text="For Own Use" Value="5" />
                                <asp:ListItem Text="Exhibition or Fairs" Value="12" />
                                <asp:ListItem Text="Line Sales" Value="10" />
                                <asp:ListItem Text="Others" Value="8" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            Document Type
                            <asp:DropDownList ID="drpDoc_Type" runat="Server" CssClass="txt" Height="24px" TabIndex="4"
                                Width="120px" AutoPostBack="false" OnSelectedIndexChanged="drpDoc_Type_SelectedIndexChanged">
                                <asp:ListItem Text="Tax Invoice" Value="INV" />
                                <asp:ListItem Text="Yes" Value="Y" />
                                <asp:ListItem Text="No" Value="N" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Document No
                            <asp:TextBox Height="24px" ID="txtDoc_No" runat="Server" CssClass="txt" TabIndex="5"
                                Width="125px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDoc_No_TextChanged"></asp:TextBox>
                            <asp:Button Width="75px" Height="24px" ID="btntxtDoc_No" runat="server" Text="..."
                                OnClick="btntxtDoc_No_Click" CssClass="btnHelp" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Document Date
                            <asp:TextBox Height="24px" ID="txtDoc_Date" runat="Server" CssClass="txt" TabIndex="6"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDoc_Date_TextChanged"
                                onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtDoc_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtDoc_Date"
                                    runat="server" TargetControlID="txtDoc_Date" PopupButtonID="imgcalendertxtDoc_Date"
                                    Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            Transaction Type
                            <asp:DropDownList ID="drpTran_Type" runat="Server" Height="24px" CssClass="txt" TabIndex="7"
                                Width="170px" AutoPostBack="false" OnSelectedIndexChanged="drpTran_Type_SelectedIndexChanged">
                                <asp:ListItem Text="Regular" Value="1" />
                                <asp:ListItem Text="Bill To-Ship To" Value="2" />
                                <asp:ListItem Text="Bill Form-Dispatch From" Value="3" />
                                <asp:ListItem Text="Combination of 2 and 3" Value="4" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table align="Left" width="100%" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left">
                            <h3 style="color: White; font-family: Verdana; border-top: 1px solid rgb(131, 127, 130); border-bottom: 1px solid rgb(131, 127, 130); text-align: left">Bill From:</h3>
                            <br />
                            Name
                            <asp:TextBox Height="24px" ID="txtBill_From_Name" runat="Server" CssClass="txt" TabIndex="8"
                                Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_From_Name_TextChanged"></asp:TextBox>
                        </td>
                        <td align="left">
                            <h3 style="color: White; font-family: Verdana; border-top: 1px solid rgb(131, 127, 130); border-bottom: 1px solid rgb(131, 127, 130); text-align: left">Dispatch From:</h3>
                            <br />
                            Address
                            <asp:TextBox Height="24px" ID="txtBill_From_Address" runat="Server" CssClass="txt"
                                TabIndex="9" Width="300px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_From_Address_TextChanged"
                                TextMode="MultiLine"></asp:TextBox>
                            <asp:TextBox Height="24px" ID="txtBill_From_Address2" runat="Server" CssClass="txt"
                                TabIndex="9" Width="300px" Style="text-align: left;" AutoPostBack="false" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">GSTIN
                            <asp:TextBox Height="24px" ID="txtBill_FromGST_No" runat="Server" CssClass="txt"
                                TabIndex="10" Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_FromGST_No_TextChanged"></asp:TextBox>
                        </td>
                        <td align="left">Place
                            <asp:TextBox Height="24px" ID="txtBill_From_Place" runat="Server" CssClass="txt"
                                TabIndex="11" Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_From_Place_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">State
                            <asp:TextBox Height="24px" ID="txtBill_From_State" runat="Server" CssClass="txt"
                                TabIndex="12" Width="120px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_From_State_TextChanged"></asp:TextBox>
                        </td>
                        <td align="left">PinCode
                            <asp:TextBox Height="24px" ID="txtBill_From_PinCode" runat="Server" CssClass="txt"
                                TabIndex="13" Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_From_PinCode_TextChanged"></asp:TextBox>
                            <asp:TextBox Height="24px" ID="txtBill_From_State2" runat="Server" CssClass="txt"
                                TabIndex="12" Width="120px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table align="Left" width="100%" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left">
                            <h3 style="color: White; font-family: Verdana; border-top: 1px solid rgb(131, 127, 130); border-bottom: 1px solid rgb(131, 127, 130); text-align: left">Bill To:</h3>
                            <br />
                            Name
                            <asp:TextBox Height="24px" ID="txtBill_To_Name" runat="Server" CssClass="txt" TabIndex="14"
                                Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_To_Name_TextChanged"></asp:TextBox>
                        </td>
                        <td align="left">
                            <h3 style="color: White; font-family: Verdana; border-top: 1px solid rgb(131, 127, 130); border-bottom: 1px solid rgb(131, 127, 130); text-align: left">Ship To:</h3>
                            <br />
                            Address
                            <asp:TextBox Height="24px" ID="txtBill_To_Add" runat="Server" CssClass="txt" TabIndex="15"
                                Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_To_Add_TextChanged"
                                TextMode="MultiLine"></asp:TextBox>
                            <asp:TextBox Height="24px" ID="txtBill_To_Add2" runat="Server" CssClass="txt" TabIndex="15"
                                Width="200px" Style="text-align: left;" AutoPostBack="false" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">GSTIN
                            <asp:TextBox Height="24px" ID="txtBill_TO_GSTNo" runat="Server" CssClass="txt" TabIndex="16"
                                Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_TO_GSTNo_TextChanged"></asp:TextBox>
                        </td>
                        <td align="left">Place
                            <asp:TextBox Height="24px" ID="txtBill_To_Place" runat="Server" CssClass="txt" TabIndex="17"
                                Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_To_Place_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">State
                            <asp:TextBox Height="24px" ID="txtBill_To_State" runat="Server" CssClass="txt" TabIndex="18"
                                Width="120px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_To_State_TextChanged"></asp:TextBox>
                        </td>
                        <td align="left">Pincode
                            <asp:TextBox Height="24px" ID="txtBill_To_PinCode" runat="Server" CssClass="txt"
                                TabIndex="19" Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBill_To_PinCode_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table align="Left" width="100%" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left">
                            <h3 style="color: White; border-top: 1px solid rgb(131, 127, 130); border-bottom: 1px solid rgb(131, 127, 130); font-family: Verdana; text-align: left">Item Details:</h3>
                            Product Name
                            <asp:TextBox Height="24px" ID="txtItem_Name" runat="Server" CssClass="txt" TabIndex="20"
                                Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtItem_Name_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Description
                            <asp:TextBox Height="24px" ID="txtItem_Description" runat="Server" CssClass="txt"
                                TabIndex="21" Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtItem_Description_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; HSN
                            <asp:TextBox Height="24px" ID="txtHSN" runat="Server" CssClass="txt" TabIndex="22"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtHSN_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Quantity
                            <asp:TextBox Height="24px" ID="txtQty" runat="Server" CssClass="txt" TabIndex="23"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Unit
                            <asp:TextBox Height="24px" ID="txtUnit" runat="Server" CssClass="txt" TabIndex="24"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtUnit_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Value/Taxable Value(Rs)
                            <asp:TextBox Height="24px" ID="txtTaxable_Value" runat="Server" CssClass="txt" TabIndex="25"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTaxable_Value_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">CGST+SGST Rate(%)
                            <asp:DropDownList ID="drpCGST_SGST_Rate" runat="Server" CssClass="txt" TabIndex="26"
                                Width="90px" AutoPostBack="false" Height="24px" OnSelectedIndexChanged="drpCGST_SGST_Rate_SelectedIndexChanged">
                                <asp:ListItem Text="Yes" Value="Y" />
                                <asp:ListItem Text="No" Value="N" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; IGST Rate(%)
                            <asp:DropDownList ID="drpIGST_Rate" Height="24px" runat="Server" CssClass="txt" TabIndex="27"
                                Width="90px" AutoPostBack="false" OnSelectedIndexChanged="drpIGST_Rate_SelectedIndexChanged">
                                <asp:ListItem Text="Yes" Value="Y" />
                                <asp:ListItem Text="No" Value="N" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            CESS Advol Rate(%)
                            <asp:DropDownList ID="drpCESS_Advol_Rate" Height="24px" runat="Server" CssClass="txt"
                                TabIndex="28" Width="90px" AutoPostBack="false" OnSelectedIndexChanged="drpCESS_Advol_Rate_SelectedIndexChanged">
                                <asp:ListItem Text="Yes" Value="Y" />
                                <asp:ListItem Text="No" Value="N" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            CESS non Advol Rate
                            <asp:DropDownList ID="drpCESS_NonAdvol_Rate" Height="24px" runat="Server" CssClass="txt"
                                TabIndex="29" Width="90px" AutoPostBack="false" OnSelectedIndexChanged="drpCESS_NonAdvol_Rate_SelectedIndexChanged">
                                <asp:ListItem Text="Yes" Value="Y" />
                                <asp:ListItem Text="No" Value="N" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Total Taxable Amount
                            <asp:TextBox Height="24px" ID="txtTaxable_Amt" runat="Server" CssClass="txt" TabIndex="30"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTaxable_Amt_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; CGST Amount
                            <asp:TextBox Height="24px" ID="txtCGST_Amt" runat="Server" CssClass="txt" TabIndex="31"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCGST_Amt_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            SGST Amount
                            <asp:TextBox Height="24px" ID="txtSGST_Amt" runat="Server" CssClass="txt" TabIndex="32"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSGST_Amt_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            IGST Amount
                            <asp:TextBox Height="24px" ID="txtIGST_Amt" runat="Server" CssClass="txt" TabIndex="33"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtIGST_Amt_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">CESS Advol Amount
                            <asp:TextBox Height="24px" ID="txtCESS_Advol_Amt" runat="Server" CssClass="txt" TabIndex="34"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCESS_Advol_Amt_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; CESS non Advol Amount
                            <asp:TextBox Height="24px" ID="txtCESS_non_Advol_Amt" runat="Server" CssClass="txt"
                                TabIndex="35" Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCESS_non_Advol_Amt_TextChanged"></asp:TextBox>
                            Other Amount(+/-)
                            <asp:TextBox Height="24px" ID="txtOther_Amt" runat="Server" CssClass="txt" TabIndex="36"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtOther_Amt_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            Total Inv Amount
                            <asp:TextBox Height="24px" ID="txtTotal_Bill_Amt" runat="Server" CssClass="txt" TabIndex="37"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTotal_Bill_Amt_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <table align="Left" width="100%" cellpadding="4" cellspacing="4">
                        <tr>
                            <td align="left">
                                <h3 style="color: White; font-family: Verdana; border-top: 1px solid rgb(131, 127, 130); border-bottom: 1px solid rgb(131, 127, 130); text-align: left">Transportation Details:</h3>
                                Transporter Name
                                <asp:TextBox Height="24px" ID="txtTransporter_Name" runat="Server" CssClass="txt"
                                    TabIndex="38" Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTransporter_Name_TextChanged"></asp:TextBox>
                                Transporter ID
                                <asp:TextBox Height="24px" ID="txtTransporter_ID" runat="Server" CssClass="txt" TabIndex="39"
                                    Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTransporter_ID_TextChanged"></asp:TextBox>
                                Approximate Distance(in KM)
                                <asp:TextBox Height="24px" ID="txtApproximate_Distance" runat="Server" CssClass="txt"
                                    TabIndex="40" Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtApproximate_Distance_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">Mode
                                <asp:DropDownList ID="drpTrance_Mode" runat="Server" CssClass="txt" TabIndex="41"
                                    Width="90px" AutoPostBack="false" Height="24px" OnSelectedIndexChanged="drpTrance_Mode_SelectedIndexChanged">
                                    <asp:ListItem Text="Road" Value="1" />
                                    <asp:ListItem Text="Rail" Value="2" />
                                    <asp:ListItem Text="Air" Value="3" />
                                    <asp:ListItem Text="Ship" Value="4" />
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                Vehicle Type
                                <asp:DropDownList ID="drpVehicle_Type" runat="Server" CssClass="txt" TabIndex="42"
                                    Width="90px" AutoPostBack="false" Height="24px" OnSelectedIndexChanged="drpVehicle_Type_SelectedIndexChanged">
                                    <asp:ListItem Text="Regular" Value="R" />
                                    <asp:ListItem Text="Over Dimensional Cargo" Value="O" />
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp; Vehicle No
                                <asp:TextBox Height="24px" ID="txtVehicle_No" runat="Server" CssClass="txt" TabIndex="43"
                                    Width="110px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtVehicle_No_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                        </tr>
                    </table>
            </asp:Panel>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
