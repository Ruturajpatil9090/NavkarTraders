<%@ Page Title="EInvoice" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pgeEInovice.aspx.cs" Inherits="Sugar_pgeEInovice" %>

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
        function ColdStorageOpen(DO, Action, trantype) {
            window.open('../Outword/pgeServiceBillTwo.aspx?csid=' + DO + '&Action=' + Action, "_self");
        }
        function RROpen(DO, Action, trantype) {
            window.open('../Outword/pgeRetailSale.aspx?Retailid=' + DO + '&Action=' + Action + '&Tran_Type=' + trantype, "_self");
        }
        
        function RBOpen(DO, Action) {
            window.open('../Outword/pgeServiceBill.aspx?rbid=' + DO + '&Action=' + Action, "_self");
        }
        function PROpen(DO, Action) {
          window.open('../Inword/pgeSugarPurchaseReturnForGST.aspx?prid=' + DO + '&Action=' + Action, "_self");
        }
        function DNOpen(DO, Action, type) {
            window.open('../Transaction/pgeDebitCreditNote.aspx?dcid=' + DO + '&Action=' + Action + '&tran_type=' + type, "_self");
        }
        function JSOpen() {
            window.open('../Jaggary/pgeJawakSaleBill.aspx');

        }
    </script>


    <script language="JavaScript" type="text/javascript">

        window.onbeforeunload = function (e) {
            var e = e || window.event;
            if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
            return 'Browser is being closed, is it okay?'; // for Safari and Chrome
        };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="E-Invoice" Font-Names="verdana" ForeColor="White"
                Font-Bold="true" Font-Size="12px"></asp:Label></legend>
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
            <asp:HiddenField ID="hdnfTaxSch" runat="server" />
            <asp:HiddenField ID="hdnfType" runat="server" />
            <asp:HiddenField ID="hdnfSlNo" runat="server" />
            <table width="80%" align="left">
                <tr>
                    <td align="center">

                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />


                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnGenEwayBill" runat="server" Text="Generate EInvoice" CssClass="btnHelp"
                            Width="120px" ValidationGroup="save" OnClick="btnGenEwayBill_Click" Height="24px" />

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
                            <asp:TextBox Height="24px" ID="txtEditDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEditDoc_No_TextChanged"
                                Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Supply Type
                            <asp:DropDownList ID="drpSupply_Type" runat="Server" Height="24px" CssClass="txt"
                                TabIndex="2" Width="90px" AutoPostBack="false" OnSelectedIndexChanged="drpSupply_Type_SelectedIndexChanged">
                                <asp:ListItem Text="B2B" Value="B2B" />
                                <asp:ListItem Text="SEZWP" Value="SEZWP" />
                                <asp:ListItem Text="SEZWOP" Value="SEZWOP" />
                                <asp:ListItem Text="EXP" Value="EXP" />
                                <asp:ListItem Text="WP" Value="WP" />
                                <asp:ListItem Text="EXPWOP" Value="EXPWOP" />
                                <asp:ListItem Text="DXEP" Value="DXEP" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Reverse Charge
                            <asp:DropDownList ID="drpRegRev_Type" Height="24px" runat="Server" CssClass="txt"
                                TabIndex="3" Width="150px" AutoPostBack="false" OnSelectedIndexChanged="drpRegRev_Type_SelectedIndexChanged">
                                <asp:ListItem Text="Yes" Value="Y" />
                                <asp:ListItem Text="No" Value="N" />
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            IGSTOnIntra
                            <asp:DropDownList ID="drpIGSTOnIntra" runat="Server" CssClass="txt" Height="24px"
                                TabIndex="4" Width="120px" AutoPostBack="false" OnSelectedIndexChanged="drpIGSTOnIntra_SelectedIndexChanged">
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
                                OnClick="btntxtDoc_No_Click" CssClass="btnHelp" Visible="false" />
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
                            Document Type
                            <asp:DropDownList ID="drpDoc_Type" runat="Server" Height="24px" CssClass="txt" TabIndex="7"
                                Width="170px" AutoPostBack="false" OnSelectedIndexChanged="drpDoc_Type_SelectedIndexChanged">
                                <asp:ListItem Text="Invoice" Value="INV" />
                                <asp:ListItem Text="Credit Note" Value="CRN" />
                                <asp:ListItem Text="Debit Note" Value="DBN" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table align="Left" width="100%" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left">
                            <h3 style="color: White; font-family: Verdana; border-top: 1px solid rgb(131, 127, 130); border-bottom: 1px solid rgb(131, 127, 130); text-align: left">Seller Details:</h3>
                            <br />
                            GSTIN
                            <asp:TextBox Height="24px" ID="txtSellerGST_No" runat="Server" CssClass="txt" TabIndex="10"
                                Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSellerGST_No_TextChanged"></asp:TextBox>
                            Legal Name
                            <asp:TextBox Height="24px" ID="txtSeller_Name" runat="Server" CssClass="txt" TabIndex="8"
                                Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSeller_Name_TextChanged"></asp:TextBox>
                        </td>
                        <td align="left">
                            <h3 style="color: White; font-family: Verdana; border-top: 1px solid rgb(131, 127, 130); border-bottom: 1px solid rgb(131, 127, 130); text-align: left">Buyer Details:</h3>
                            <br />
                            GSTIN
                            <asp:TextBox Height="24px" ID="txtBuyerGST_No" runat="Server" CssClass="txt" TabIndex="10"
                                Width="200px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            Legal Name
                            <asp:TextBox Height="24px" ID="txtBuyerName" runat="Server" CssClass="txt" TabIndex="8"
                                Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSeller_Name_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Address:
                            <asp:TextBox Height="24px" ID="txtSeller_Address" runat="Server" CssClass="txt" TabIndex="9"
                                Width="300px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSeller_Address_TextChanged"
                                TextMode="MultiLine"></asp:TextBox>
                            <asp:TextBox Height="24px" ID="txtSeller_Address2" runat="Server" CssClass="txt"
                                TabIndex="9" Width="250px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSeller_Address_TextChanged"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td align="left">Address:
                            <asp:TextBox Height="24px" ID="txtBuyer_Address" runat="Server" CssClass="txt" TabIndex="9"
                                Width="300px" Style="text-align: left;" AutoPostBack="false" TextMode="MultiLine"></asp:TextBox>
                            <asp:TextBox Height="24px" ID="txtBuyer_Address2" runat="Server" CssClass="txt" TabIndex="9"
                                Width="250px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSeller_Address_TextChanged"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Location:
                            <asp:TextBox Height="24px" ID="txtSeller_Location" runat="Server" CssClass="txt"
                                TabIndex="11" Width="200px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            PIN:
                            <asp:TextBox Height="24px" ID="txtSeller_PIN" runat="Server" CssClass="txt" TabIndex="11"
                                Width="120px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            State Name:
                            <asp:TextBox Height="24px" ID="txtSeller_StateName" runat="Server" CssClass="txt"
                                TabIndex="12" Width="120px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                        </td>
                        <td align="left">Location:
                            <asp:TextBox Height="24px" ID="txtBuyer_Location" runat="Server" CssClass="txt" TabIndex="11"
                                Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBuyer_Location_TextChanged"></asp:TextBox>
                            PIN:
                            <asp:TextBox Height="24px" ID="txtBuyer_PIN" runat="Server" CssClass="txt" TabIndex="11"
                                Width="120px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            State Name:
                            <asp:TextBox Height="24px" ID="txtBuyer_StateName" runat="Server" CssClass="txt"
                                TabIndex="12" Width="120px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">State Code:
                            <asp:TextBox Height="24px" ID="txtSeller_StateCode" runat="Server" CssClass="txt"
                                TabIndex="12" Width="120px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSeller_StateCode_TextChanged"></asp:TextBox>
                            Phone No:
                            <asp:TextBox Height="24px" ID="txtSeller_Phno" runat="Server" CssClass="txt" TabIndex="12"
                                Width="120px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            Email:
                            <asp:TextBox Height="24px" ID="txtSeller_Email" runat="Server" CssClass="txt" TabIndex="12"
                                Width="200px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                        </td>
                        <td align="left">State Code:
                            <asp:TextBox Height="24px" ID="txtBuyer_StateCode" runat="Server" CssClass="txt"
                                TabIndex="13" Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtBuyer_StateCode_TextChanged"></asp:TextBox>
                            Pos:
                            <asp:TextBox Height="24px" ID="txtBuyer_Pos" runat="Server" CssClass="txt" TabIndex="13"
                                Width="90px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            Phone No:
                            <asp:TextBox Height="24px" ID="txtByuer_Phno" runat="Server" CssClass="txt" TabIndex="12"
                                Width="120px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            Email:
                            <asp:TextBox Height="24px" ID="txtBuyer_Email" runat="Server" CssClass="txt" TabIndex="12"
                                Width="200px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table align="Left" width="100%" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left">
                            <h3 style="color: White; font-family: Verdana; border-top: 1px solid rgb(131, 127, 130); border-bottom: 1px solid rgb(131, 127, 130); text-align: left">Dispatch Details:</h3>
                            <br />
                            GSTIN
                            <asp:TextBox Height="24px" ID="txtDispatch_GSTNo" runat="Server" CssClass="txt" TabIndex="16"
                                Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDispatch_GSTNo_TextChanged"></asp:TextBox>
                            Name
                            <asp:TextBox Height="24px" ID="txtDispatch_Name" runat="Server" CssClass="txt" TabIndex="14"
                                Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDispatch_Name_TextChanged"></asp:TextBox>
                        </td>
                        <td align="left">
                            <h3 style="color: White; font-family: Verdana; border-top: 1px solid rgb(131, 127, 130); border-bottom: 1px solid rgb(131, 127, 130); text-align: left">Ship To Details:</h3>
                            <br />
                            GSTIN
                            <asp:TextBox Height="24px" ID="txtShip_To_GSTNo" runat="Server" CssClass="txt" TabIndex="16"
                                Width="200px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            Legal Name
                            <asp:TextBox Height="24px" ID="txtShip_Name" runat="Server" CssClass="txt" TabIndex="15"
                                Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtShip_Name_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Address:
                            <asp:TextBox Height="24px" ID="txtDispatch_Address" runat="Server" CssClass="txt"
                                TabIndex="9" Width="300px" Style="text-align: left;" AutoPostBack="false" TextMode="MultiLine"></asp:TextBox>
                            <asp:TextBox Height="24px" ID="txtDispatch_Address2" runat="Server" CssClass="txt"
                                TabIndex="9" Width="300px" Style="text-align: left;" AutoPostBack="false" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td align="left">Address:
                            <asp:TextBox Height="24px" ID="txtShipTo_Address" runat="Server" CssClass="txt" TabIndex="15"
                                Width="300px" Style="text-align: left;" AutoPostBack="false" TextMode="MultiLine"></asp:TextBox>
                            <asp:TextBox Height="24px" ID="txtShipTo_Address2" runat="Server" CssClass="txt"
                                TabIndex="15" Width="300px" Style="text-align: left;" AutoPostBack="false" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Location
                            <asp:TextBox Height="24px" ID="txtDispatch_Location" runat="Server" CssClass="txt"
                                TabIndex="17" Width="200px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            State Code
                            <asp:TextBox Height="24px" ID="txtDispatch_StateCode" runat="Server" CssClass="txt"
                                TabIndex="18" Width="120px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtDispatch_StateCode_TextChanged"></asp:TextBox>
                            Pincode
                            <asp:TextBox Height="24px" ID="txtDispatch_PincodeCode" runat="Server" CssClass="txt"
                                TabIndex="19" Width="90px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                        </td>
                        <td align="left">Location
                            <asp:TextBox Height="24px" ID="txtShip_To_Location" runat="Server" CssClass="txt"
                                TabIndex="17" Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtShip_To_Location_TextChanged"></asp:TextBox>
                            State Code
                            <asp:TextBox Height="24px" ID="txtShip_TostateCode" runat="Server" CssClass="txt"
                                TabIndex="18" Width="120px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            Pincode
                            <asp:TextBox Height="24px" ID="txtShip_To_PinCode" runat="Server" CssClass="txt"
                                TabIndex="19" Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtShip_To_PinCode_TextChanged"></asp:TextBox>
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
                            &nbsp;&nbsp; Description
                            <asp:TextBox Height="24px" ID="txtItem_Description" runat="Server" CssClass="txt"
                                TabIndex="21" Width="150px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtItem_Description_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp; HSN/SAC
                            <asp:TextBox Height="24px" ID="txtHSN" runat="Server" CssClass="txt" TabIndex="22"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtHSN_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;Is Service
                            <asp:DropDownList ID="drpIs_Service" runat="Server" CssClass="txt" Height="24px"
                                TabIndex="4" Width="90px" AutoPostBack="false" OnSelectedIndexChanged="drpIs_Service_SelectedIndexChanged">
                                <asp:ListItem Text="Yes" Value="Y" />
                                <asp:ListItem Text="No" Value="N" />
                            </asp:DropDownList>
                            &nbsp;&nbsp; Quantity
                            <asp:TextBox Height="24px" ID="txtQty" runat="Server" CssClass="txt" TabIndex="23"
                                Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp; Unit
                            <asp:TextBox Height="24px" ID="txtUnit" runat="Server" CssClass="txt" TabIndex="24"
                                Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtUnit_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp; Unit Price
                            <asp:TextBox Height="24px" ID="txtUnit_Price" runat="Server" CssClass="txt" TabIndex="24"
                                Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtUnit_Price_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Assessable Value
                            <asp:TextBox Height="24px" ID="txtAssessable_Value" runat="Server" CssClass="txt"
                                TabIndex="30" Width="90px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; CGST Value
                            <asp:TextBox Height="24px" ID="txtCGST_Value" runat="Server" CssClass="txt" TabIndex="30"
                                Width="90px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; SGST Value
                            <asp:TextBox Height="24px" ID="txtSGST_Value" runat="Server" CssClass="txt" TabIndex="30"
                                Width="90px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; IGST Value
                            <asp:TextBox Height="24px" ID="txtIGST_Value" runat="Server" CssClass="txt" TabIndex="30"
                                Width="90px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Cess Value
                            <asp:TextBox Height="24px" ID="txtCess_Value" runat="Server" CssClass="txt" TabIndex="30"
                                Width="90px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Total Invoice Value
                            <asp:TextBox Height="24px" ID="txtTotInv_Value" runat="Server" CssClass="txt" TabIndex="25"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTotInv_Value_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; State Cess Value
                            <asp:TextBox Height="24px" ID="txtStCesVal" runat="Server" CssClass="txt" TabIndex="30"
                                Width="90px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Discount
                            <asp:TextBox Height="24px" ID="txtDiscount" runat="Server" CssClass="txt" TabIndex="30"
                                Width="90px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Other Charges
                            <asp:TextBox Height="24px" ID="txtOther_Charges" runat="Server" CssClass="txt" TabIndex="30"
                                Width="90px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            <asp:TextBox Height="24px" ID="txtTaxable_Amt" runat="Server" CssClass="txt" TabIndex="30"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTaxable_Amt_TextChanged"
                                Visible="false"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;GST Rate
                            <asp:TextBox Height="24px" ID="txtGST_Rate" runat="Server" CssClass="txt" TabIndex="22"
                                Width="90px" Style="text-align: left;" AutoPostBack="true" OnTextChanged="txtGST_Rate_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp; CGST Amount
                            <asp:TextBox Height="24px" ID="txtCGST_Amt" runat="Server" CssClass="txt" TabIndex="31"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCGST_Amt_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            SGST Amount
                            <asp:TextBox Height="24px" ID="txtSGST_Amt" runat="Server" CssClass="txt" TabIndex="32"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtSGST_Amt_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; IGST Amount
                            <asp:TextBox Height="24px" ID="txtIGST_Amt" runat="Server" CssClass="txt" TabIndex="33"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtIGST_Amt_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">CESS Amount
                            <asp:TextBox Height="24px" ID="txtCESS_Advol_Amt" runat="Server" CssClass="txt" TabIndex="34"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtCESS_Advol_Amt_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Assessable Amount
                            <asp:TextBox Height="24px" ID="txtAssessable_Amount" runat="Server" CssClass="txt"
                                TabIndex="30" Width="90px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            Total Item Amount
                            <asp:TextBox Height="24px" ID="txtTotal_Item_Amt" runat="Server" CssClass="txt" TabIndex="36"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTotal_Item_Amt_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            Total Inv Amount
                            <asp:TextBox Height="24px" ID="txtTotal_Bill_Amt" runat="Server" CssClass="txt" TabIndex="37"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTotal_Bill_Amt_TextChanged"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Transporter Name
                            <asp:TextBox Height="24px" ID="txtTransporter_Name" runat="Server" CssClass="txt"
                                TabIndex="38" Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTransporter_Name_TextChanged"></asp:TextBox>
                            Transporter ID
                            <asp:TextBox Height="24px" ID="txtTransporter_ID" runat="Server" CssClass="txt" TabIndex="39"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtTransporter_ID_TextChanged"></asp:TextBox>
                            Approximate Distance(in KM)
                            <asp:TextBox Height="24px" ID="txtApproximate_Distance" runat="Server" CssClass="txt"
                                TabIndex="40" Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtApproximate_Distance_TextChanged"></asp:TextBox>
                            Mode
                            <asp:DropDownList ID="drpTrance_Mode" runat="Server" CssClass="txt" TabIndex="41"
                                Width="90px" AutoPostBack="false" Height="24px" OnSelectedIndexChanged="drpTrance_Mode_SelectedIndexChanged">
                                <asp:ListItem Text="Road" Value="1" />
                                <asp:ListItem Text="Rail" Value="2" />
                                <asp:ListItem Text="Air" Value="3" />
                                <asp:ListItem Text="Ship" Value="4" />
                            </asp:DropDownList>
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
                </table>
                <table align="Left" width="100%" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left">
                            <h3 style="color: White; font-family: Verdana; border-top: 1px solid rgb(131, 127, 130); border-bottom: 1px solid rgb(131, 127, 130); text-align: left">Patment Details:</h3>
                            Mode of Payment
                            <asp:TextBox Height="24px" ID="txtMode_of_Payment" runat="Server" CssClass="txt"
                                TabIndex="38" Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtMode_of_Payment_TextChanged"></asp:TextBox>
                            Account Details
                            <asp:TextBox Height="24px" ID="txtAccount_Details" runat="Server" CssClass="txt"
                                TabIndex="39" Width="200px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtAccount_Details_TextChanged"></asp:TextBox>
                            Branch
                            <asp:TextBox Height="24px" ID="txtBranch" runat="Server" CssClass="txt" TabIndex="39"
                                Width="200px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                            Payee Name
                            <asp:TextBox Height="24px" ID="txtPayeeName" runat="Server" CssClass="txt" TabIndex="39"
                                Width="200px" Style="text-align: left;" AutoPostBack="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
