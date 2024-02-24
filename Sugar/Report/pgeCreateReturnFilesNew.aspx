<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pgeCreateReturnFilesNew.aspx.cs"
    Inherits="Sugar_pgeCreateReturnFilesNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GST Returns</title>
    <script type="text/javascript" src="../JS/DateValidation.js">
    </script>
    <script type="text/javascript" src="<%# ResolveUrl("~../Scripts/jquery-1.4.1.min.js") %>"></script>
    <script type="text/javascript" src="../../Script/jquery-1.4.2.js"></script>
</head>
<body style="background-color: White;">
    <script type="text/javascript" language="javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Create CSV File?")) {
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
        function SelectAllCheckboxes(chk, selector) {
            debugger;
            $('#<%=grdDetail_Report.ClientID%>').find(selector + " input:checkbox").each(function () {
                $(this).prop("checked", $(chk).prop("checked"));
            });
        }

        function SelectAllCheckboxes(chk, selector) {
            debugger;
            $('#<%=grdOurDO.ClientID%>').find(selector + " input:checkbox").each(function () {
                $(this).prop("checked", $(chk).prop("checked"));
            });
        }
    </script>
    <script type="text/javascript">
        function SelectCheckboxes(headerChk, grdId, columnIndex) {
            debugger;
            var gridView = document.getElementById("<%=grdDetail_Report.ClientID %>");
            var IsChecked = headerChk.checked;
            var tbl = document.getElementById(gridView);

            for (i = 1; i < gridView.rows.length; i++) {
                var curTd = gridView.rows[i].cells[columnIndex];
                var item = curTd.getElementsByTagName('input');
                for (j = 0; j < item.length; j++) {
                    if (item[j].type == "checkbox") {
                        if (item[j].checked != IsChecked) {
                            item[j].click();
                        }
                    }
                }
            }

        }
    </script>
    <script type="text/javascript">
        function SelectCheckboxes1(headerChk, grdId, columnIndex) {
            debugger;
            var gridView = document.getElementById("<%=grdOurDO.ClientID %>");
            var IsChecked = headerChk.checked;
            var tbl = document.getElementById(gridView);

            for (i = 1; i < gridView.rows.length; i++) {
                var curTd = gridView.rows[i].cells[columnIndex];
                var item = curTd.getElementsByTagName('input');
                for (j = 0; j < item.length; j++) {
                    if (item[j].type == "checkbox") {
                        if (item[j].checked != IsChecked) {
                            item[j].click();
                        }
                    }
                }
            }

        }
    </script>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdconfirm" runat="server" />
        <div>
            <ajax1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
            </ajax1:ToolkitScriptManager>
            <table border="0" cellpadding="5" cellspacing="5" style="margin: 0 auto;">
                <tr>
                    <td align="left">From Date:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtFromDt" runat="server" Width="80px" CssClass="txt" Height="24px"
                            MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                        <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                            Width="25px" Height="15px" />
                        <ajax1:CalendarExtender ID="calenderExtendertxtFromDt" runat="server" TargetControlID="txtFromDt"
                            PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                        </ajax1:CalendarExtender>
                    </td>
                    <td align="left">To Date:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtToDt" runat="server" Width="80px" CssClass="txt" Height="24px"
                            MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"></asp:TextBox>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                            Height="15px" />
                        <ajax1:CalendarExtender ID="CalendarExtendertxtToDt" runat="server" TargetControlID="txtToDt"
                            PopupButtonID="Image1" Format="dd/MM/yyyy">
                        </ajax1:CalendarExtender>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button Text="CREATE B2B FILE" runat="server" CssClass="btnHelp" ID="btnCreateb2b"
                            OnClick="btnCreateb2b_Click" />&nbsp;
                    <asp:Button Text="CREATE B2CL FILE" runat="server" CssClass="btnHelp" ID="btnCreateb2cl"
                        OnClick="btnCreateb2cl_Click" />
                        &nbsp;
                    <asp:Button Text="CREATE B2CS FILE" runat="server" CssClass="btnHelp" ID="btnCreateb2cs"
                        OnClick="btnCreateb2cs_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="1">
                        <asp:Button ID="btnCreatePurchaseBillSummary" Text="PURCHASE BILL SUMMARY" runat="server"
                            OnClick="btnCreatePurchaseBillSummary_Click" />
                    </td>
                    <td colspan="1">
                        <asp:Button ID="btnCreateSaleBillSummary" Text="SALE BILL SUMMARY" runat="server"
                            OnClick="btnCreateSaleBillSummary_Click" />
                    </td>
                    <td colspan="1">
                        <asp:Button ID="btlCreateRetailSaleSummary" Text="RETAIL SALE BILL SUMMARY" runat="server"
                            OnClick="btlCreateRetailSaleSummary_Click" />
                    </td>
                    <td colspan="1">
                        <asp:Button ID="btnFrieghtSummary" Text="FRIEGHT SUMMARY" runat="server" OnClick="btnFrieghtSummary_Click" />
                    </td>
                    <td colspan="1">
                        <asp:Button ID="btnDebitNoteSummary" Text="DEBIT NOTE SUMMARY" runat="server" OnClick="btnDebitNoteSummary_Click" />
                    </td>
                    <td colspan="1">
                        <asp:Button ID="btnCreditNoteSummary" Text="CREDIT NOTE SUMMARY" runat="server" OnClick="btnCreditNoteSummary_Click" />
                    </td>
                    <td colspan="1">
                        <asp:Button Text="E Way Bill" runat="server" ID="btnEwayBill" OnClick="btnEwayBill_Click" />
                    </td>
                    <td colspan="1">
                        <asp:Button Text="Empty E Way Bill" runat="server" ID="btnEmpty_E_Way_Bil" OnClick="btnEmpty_E_Way_Bil_Click" />
                    </td>
                    <td colspan="1">
                        <asp:Button Text="Sale BIll Checking" runat="server" ID="btnSale_Bill_Checking" OnClick="btnSale_Bill_Checking_Click" />
                    </td>
                    <td colspan="1">
                        <asp:Button Text="Summary" runat="server" ID="btnSummary" OnClick="btnSummary_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="1">
                        <asp:Button Text="Other Purchase" runat="server" ID="btnOtherPur" OnClick="btnOtherPur_Click" />
                    </td>
                    <td colspan="1">
                        <asp:Button Text="Service Bill" runat="server" ID="btnServiceBill" OnClick="btnServiceBill_Click" />
                    </td>
                    <td colspan="2">Sale TCS
                    <asp:DropDownList runat="server" ID="drpSaleTCS" OnSelectedIndexChanged="drpSaleTCS_SelectedIndexChanged">
                        <asp:ListItem Text="All" Value="All" Selected="True" />
                        <asp:ListItem Text="Sale Bill" Value="SB" />
                        <asp:ListItem Text="Sale Bill Corporate" Value="SC" />
                        <asp:ListItem Text="Sale Bill Non Corporate" Value="NC" />
                        <asp:ListItem Text="Sale Return Sale" Value="RS" />
                        <asp:ListItem Text="Retail Sale" Value="RR" />
                        <asp:ListItem Text="Commission Bill" Value="LV" />
                        <asp:ListItem Text="Cold Storage Sale" Value="CB" />
                        <asp:ListItem Text="Rent Bill" Value="RB" />
                        <asp:ListItem Text="Debit  Note to Customer" Value="DN"></asp:ListItem>
                        <asp:ListItem Text="Debit Note to Supplier" Value="DS"></asp:ListItem>
                    </asp:DropDownList>
                        <asp:Button Text="Sale TCS" runat="server" ID="btnSaleTCS" OnClick="btnSaleTCS_Click" OnClientClick="Confirm();" />
                        <asp:Button Text="Sale TDS" runat="server" ID="btnSaleTDS" OnClick="btnSaleTDS_Click" OnClientClick="Confirm();" />
                    </td>
                    <td colspan="2">Purchase TCS
                    <asp:DropDownList runat="server" ID="drppurchasrtcs" OnSelectedIndexChanged="drppurchasrtcs_SelectedIndexChanged">
                        <asp:ListItem Text="All" Value="All" Selected="False" />
                        <asp:ListItem Text="Purchase Bill" Value="PS" />
                        <asp:ListItem Text="Retail Purchase" Value="RP" />
                         <asp:ListItem Text="Credit Note to Customer" Value="CN"></asp:ListItem>
                        <asp:ListItem Text="Credit Note to Supplier" Value="CS"></asp:ListItem>
                         <asp:ListItem Text="Commission Bill" Value="CV" />
                         <asp:ListItem Text=" Sugar Sale Return Purchase" Value="PR" />
                         <asp:ListItem Text="Other Purchase" Value="XP" />
                    </asp:DropDownList>
                        <asp:Button Text="Purchase TCS" runat="server" ID="btnPurchaseTCS" OnClick="btnPurchaseTCS_Click"
                            OnClientClick="Confirm();" />
                        <asp:Button Text="Purchase TDS" runat="server" ID="btnPurchaseTDS" OnClick="btnPurchaseTDS_Click"
                            OnClientClick="Confirm();" />
                        <asp:Button Text="Transport TDS" runat="server" ID="btnTransportTDS" OnClick="btnTransportTDS_Click"
                            OnClientClick="Confirm();" />
                    </td>
                    <td colspan="1">
                        <asp:Button Text="GSTR2" runat="server" ID="Button1" OnClick="btnGSTR2_Click" Visible="false" />
                    </td>
                    <td colspan="1">
                        <asp:Button Text="HSN Wise Sale" runat="server" ID="btnhsnsale" OnClick="btnhsnsale_Click"
                            OnClientClick="Confirm();" />
                    </td>
                    <td colspan="1">
                        <asp:Button Text="Return Sale" runat="server" ID="btnReturnSale" OnClick="btnReturnSale_Click" />
                    </td>
                    <td colspan="1">
                        <asp:Button Text="Return Purchase" runat="server" ID="btnpurchaseReturn" OnClick="btnpurchaseReturn_Click" />
                    </td>

                </tr>
                <tr>
                    <td colspan="2">Gst Rate
                    <asp:DropDownList runat="server" ID="drpGstRate" Width="100px" CssClass="ddl" AutoPostBack="true"
                        TabIndex="1" OnSelectedIndexChanged="drpGstRate_SelectedIndexChanged">
                    </asp:DropDownList>
                        <asp:Button Text="Gst Rate wise Sale" runat="server" ID="btnGstRate" OnClick="btnGstRate_Click" />
                    </td>

                    <td>
                        <asp:Button Text="Show Entry no" runat="server" ID="btnShowentryno" OnClick="btnShowentryno_Click" />
                    </td>

                    <td>
                        <asp:DropDownList runat="server" ID="drpdebitcreditnote" Width="100px" CssClass="ddl">
                            <asp:ListItem Text="All" Value="All" Selected="False" />
                            <asp:ListItem Text="Debit note to Customer" Value="DN" />
                            <asp:ListItem Text="Credit note to Customer" Value="CN" />
                            <asp:ListItem Text="Debit note to Supplier" Value="DS" />
                            <asp:ListItem Text="Customer note to Supplier" Value="CS" />

                        </asp:DropDownList>
                        <asp:Button Text="Debit Credit Note" runat="server" ID="btnDebitCreditNote" OnClick="btnDebitCreditNote_Click" />
                    </td>
                    <td>
                        <asp:Button Text="Generate RCM No to MEMO Advance" runat="server" ID="btngeneratememoadvance" OnClick="btngeneratememoadvance_Click" />
                        <asp:Button Text="Generate RCM Number" runat="server" ID="btngenerateRCMnum" OnClick="btngenerateRCMnum_Click" Visible="false" />

                    </td>
                    <td>
                        <asp:Button ID="btnClodstorageInword" Text="ColdStorage Outword SUMMARY" runat="server"
                            OnClick="btnClodstorageInword_Click" /></td>
                    <td>
                        <asp:Button ID="btnAllTCSTDS" Text="Sale TCSTDS" runat="server"
                            OnClick="btnAllTCSTDS_Click" /></td>
                    <td>
                        <asp:Button ID="btnPSTCSTDS" Text="PS TCSTDS" runat="server"
                            OnClick="btnPSTCSTDS_Click" /></td>
                    <td>
                        <asp:Button ID="btnSendEmail" Text="Send Email" runat="server"
                            OnClick="btnSendEmail_Click" /></td>
                    <td>
                        <asp:Button ID="btnOurDO" Text="Our DO" runat="server"
                            OnClick="btnOurDO_Click" />

                        <asp:Button ID="btnDOSendEmail" Text="DO Send Email" runat="server"
                            OnClick="btnDOSendEmail_Click" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button Text="Credit Note GSTR1" runat="server" ID="btncreditenote" OnClick="btncreditenote_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Panel runat="server" ID="pnlSale" BorderColor="Blue" Style="margin: 0 auto;"
                Width="100%">
                <br />
                <h3>
                    <asp:Label Text="" ID="lblSummary" runat="server" /></h3>
                <asp:Button Text="EXPORT TO EXCEL" ID="btnExportToexcel" OnClick="btnExportToexcel_Click"
                    runat="server" />
                <asp:GridView runat="server" ID="grdAll" AutoGenerateColumns="true" GridLines="Both"
                    HeaderStyle-Font-Bold="true" RowStyle-Height="30px" ShowFooter="true">
                    <%--<Columns>
                    <asp:BoundField DataField="SR_No" HeaderText="SR_No" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="30px" />
                    <asp:BoundField DataField="Invoice_No" HeaderText="Invoice No" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="80px" />
                    <asp:BoundField DataField="PartyGSTNo" HeaderText="GSTIN/UIN of Recipient" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="140px" />
                    <asp:BoundField DataField="PartyStateCode" HeaderText="State Code" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="40px" />
                    <asp:BoundField DataField="Invoice_Date" HeaderText="Invoice Date" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="100px" />
                    <asp:BoundField DataField="Vehicle_No" HeaderText="Vehicle No" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="140px" />
                    <asp:BoundField DataField="Quintal" HeaderText="Quintal" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="80px" />
                    <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="100px" />
                    <asp:BoundField DataField="TaxableAmount" HeaderText="Taxable Amount" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="120px" />
                    <asp:BoundField DataField="CGST" HeaderText="CGST" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="120px" />
                    <asp:BoundField DataField="SGST" HeaderText="SGST" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="120px" />
                    <asp:BoundField DataField="IGST" HeaderText="IGST" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="120px" />
                    <asp:BoundField DataField="Payable_Amount" HeaderText="Final Amount" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="120px" />
                </Columns>--%>
                    <FooterStyle BackColor="Yellow" Font-Bold="true" />
                </asp:GridView>
                <asp:GridView ID="grdDetail_Report" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#397CBB"
                    HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="100%"
                    OnRowCommand="grdDetail_Report_RowCommand" CellPadding="5" CellSpacing="5" OnRowDataBound="grdDetail_Report_RowDataBound"
                    Style="table-layout: fixed;" ShowFooter="true">

                    <Columns>
                        <asp:BoundField ControlStyle-Width="10px" DataField="SR_No" HeaderText="SR_No" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="fromDate" HeaderText="fromDate" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="ToDate" HeaderText="ToDate" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="Name_Of_Party" HeaderText="Name_Of_Party" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="PartyGst_No" HeaderText="PartyGst_No" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="Pan" HeaderText="Pan" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="Tan" HeaderText="Tan" />
                        <asp:BoundField ControlStyle-Width="25px" DataField="Taxable_Amt" HeaderText="Taxable_Amt" />
                        <asp:BoundField ControlStyle-Width="25px" DataField="CGST" HeaderText="CGST" />
                        <asp:BoundField ControlStyle-Width="25px" DataField="SGST" HeaderText="SGST" />
                        <asp:BoundField ControlStyle-Width="25px" DataField="IGST" HeaderText="IGST" />
                        <asp:BoundField ControlStyle-Width="25px" DataField="TCS" HeaderText="TCS" />
                        <asp:BoundField ControlStyle-Width="25px" DataField="TDS" HeaderText="TDS" />
                        <asp:BoundField ControlStyle-Width="25px" DataField="Bill_Amt" HeaderText="Bill_Amt" />
                        <%--   <asp:BoundField ControlStyle-Width="10px" DataField="Party_Email" HeaderText="Party_Email" />--%>
                        <asp:TemplateField HeaderText="Party_Email">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtemail" BorderStyle="None" Text='<%#Eval("Party_Email") %>'
                                    Height="20px" Width="90%"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="10px" HeaderText="GSTPermission">
                            <HeaderTemplate>

                                <asp:CheckBox ID="checkGST" runat="server" onclick="SelectCheckboxes(this,'<%=grdDetail_Report.ClientID %>', 15)" Text="GSTPer" CssClass="chkclass" />

                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkpermissionGST" runat="server" Text='<%# Eval("GSTPermission") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="10px" HeaderText="TDSPermission">
                            <HeaderTemplate>

                                <asp:CheckBox ID="checkTDS" runat="server" onclick="SelectCheckboxes(this,'<%=grdDetail_Report.ClientID %>', 16)" Text="TDSPer" />

                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkpermissionTDS" runat="server" Text='<%# Eval("TDSPermission") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="10px" HeaderText="TCSPermission">
                            <HeaderTemplate>

                                <asp:CheckBox ID="checkTCS" runat="server" onclick="SelectCheckboxes(this,'<%=grdDetail_Report.ClientID %>', 17)" Text="TCSPer" />

                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkpermissionTCS" runat="server" Text='<%# Eval("TCSPermission") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="10px" HeaderText="ALLPermission">
                            <HeaderTemplate>

                                <asp:CheckBox ID="checkAll" runat="server" onclick="SelectCheckboxes(this,'<%=grdDetail_Report.ClientID %>', 18)" Text="ALLPer" />

                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkpermission" runat="server" Text='<%# Eval("Permission") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                    <FooterStyle BackColor="Yellow" Font-Bold="true" />
                </asp:GridView>

                <asp:GridView ID="grdOurDO" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#397CBB"
                    HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="100%"
                    OnRowCommand="grdOurDO_RowCommand" CellPadding="5" CellSpacing="5" OnRowDataBound="grdOurDO_RowDataBound"
                    Style="table-layout: fixed;" ShowFooter="true">

                    <Columns>
                        <asp:BoundField ControlStyle-Width="10px" DataField="SR_No" HeaderText="SR_No" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="fromDate" HeaderText="fromDate" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="ToDate" HeaderText="ToDate" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="doc_no" HeaderText="doc_no" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="Do_Date" HeaderText="Do_Date" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="shiptoname" HeaderText="shiptoname" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="shiptogstno" HeaderText="shiptogstno" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="shiptoemail" HeaderText="shiptoemail" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="quantal" HeaderText="quantal" />
                        <asp:BoundField ControlStyle-Width="10px" DataField="mill_rate" HeaderText="mill_rate" />
                        <asp:BoundField ControlStyle-Width="25px" DataField="sale_rate" HeaderText="sale_rate" />
                        <asp:BoundField ControlStyle-Width="25px" DataField="final_amout" HeaderText="final_amout" />
                        <asp:BoundField ControlStyle-Width="25px" DataField="tenderdoname" HeaderText="tenderdoname" />
                        <asp:TemplateField ControlStyle-Width="10px" HeaderText="SendEmail">
                            <HeaderTemplate>

                                <asp:CheckBox ID="checksendemail" runat="server" onclick="SelectCheckboxes1(this,'<%=grdOurDO.ClientID %>', 13)" Text="SendEmail" CssClass="chkclass" />

                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkpermissionsendemail" runat="server" Text='<%# Eval("SendEmail") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                    <FooterStyle BackColor="Yellow" Font-Bold="true" />
                </asp:GridView>
            </asp:Panel>
            <%--<asp:Panel runat="server" ID="pnlPurchaseSummary" BorderColor="Blue" Style="margin: 0 auto;"
            Width="100%">
            <br />
            <h3>
                Purchase Summary</h3>
            <asp:Button Text="EXPORT TO EXCEL" ID="btnPSExport" OnClick="btnPSExport_Click" runat="server" />
            <asp:GridView runat="server" ID="grdPurchaseSummary" AutoGenerateColumns="false"
                GridLines="Both" HeaderStyle-Font-Bold="true" RowStyle-Height="30px" ShowFooter="true">
                <Columns>
                    <asp:BoundField DataField="SR_No" HeaderText="SR_No" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="30px" />
                    <asp:BoundField DataField="OurNo" HeaderText="Our No" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="80px" />
                    <asp:BoundField DataField="MillInvoiceNo" HeaderText="Mill Invoice No" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="80px" />
                    <asp:BoundField DataField="FromGSTNo" HeaderText="GSTIN" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="140px" />
                    <asp:BoundField DataField="FromStateCode" HeaderText="State Code" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="40px" />
                    <asp:BoundField DataField="Date" HeaderText="Invoice Date" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="100px" />
                    <asp:BoundField DataField="Vehicle_No" HeaderText="Vehicle No" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="140px" />
                    <asp:BoundField DataField="Quintal" HeaderText="Quintal" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="80px" />
                    <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="100px" />
                    <asp:BoundField DataField="TaxableAmount" HeaderText="Taxable Amount" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="120px" />
                    <asp:BoundField DataField="CGST" HeaderText="CGST" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="120px" />
                    <asp:BoundField DataField="SGST" HeaderText="SGST" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="120px" />
                    <asp:BoundField DataField="IGST" HeaderText="IGST" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="120px" />
                    <asp:BoundField DataField="Payable_Amount" HeaderText="Final Amount" ItemStyle-HorizontalAlign="Right"
                        ItemStyle-Width="120px" />
                </Columns>
                <FooterStyle BackColor="Yellow" Font-Bold="true" />
            </asp:GridView>
        </asp:Panel>--%>
        </div>
    </form>
</body>
</html>
