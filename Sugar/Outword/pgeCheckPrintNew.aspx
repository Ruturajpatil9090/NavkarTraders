<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeCheckPrintNew.aspx.cs" Inherits="pgeCheckPrintNew" %>

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
             if (hdnfClosePopupValue == "txtEditDoc_no ") {
                 document.getElementById("<%=txtEditDoc_no.ClientID %>").value = "";
                 document.getElementById("<%=txtEditDoc_no.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                 document.getElementById("<%=txtEditDoc_no.ClientID %>").focus();
             }
             if (hdnfClosePopupValue == "txtDoc_No") {
                 document.getElementById("<%= txtDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                 document.getElementById("<%= lbldoc_no.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                 document.getElementById("<%=txtDoc_No.ClientID %>").focus();
             }
             if (hdnfClosePopupValue == "txtPayment_No") {
                 document.getElementById("<%= txtPayment_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                 document.getElementById("<%= lblPayment_No.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                 document.getElementById("<%=txtPayment_No.ClientID %>").focus();
             }
             if (hdnfClosePopupValue == "txtCashBank") {
                 document.getElementById("<%= txtCashBank.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                 document.getElementById("<%= lblCashBank.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                 document.getElementById("<%=txtCashBank.ClientID %>").focus();
             }
             if (hdnfClosePopupValue == "txtAc_Code") {
                 document.getElementById("<%= txtAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                 document.getElementById("<%= lblAc_Name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                 document.getElementById("<%=txtAc_Code.ClientID %>").focus();
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
    <script type="text/javascript" language="javascript">
        document.addEventListener('keyup', function (event) {
            if (event.defaultPrevented) {
                return;
            }
            var key = event.key || event.keyCode;
            if (key === 'Escape' || key === 'Esc' || key === 27) {
                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
       document.getElementById("<%=hdnfClosePopup.ClientID %>").value
       if (hdnfClosePopupValue == "txtPayment_No") {
           document.getElementById("<%=txtPayment_No.ClientID %>").focus();
             }
             if (hdnfClosePopupValue == "txtCashBank") {
                 document.getElementById("<%=txtCashBank.ClientID %>").focus();
             }
             if (hdnfClosePopupValue == "txtAc_Code") {
                 document.getElementById("<%=txtAc_Code.ClientID %>").focus();
             }
             document.getElementById("<%=txtSearchText.ClientID %>").value
       document.getElementById("<%=hdnfClosePopup.ClientID %>").value = "Close";
   }
 });
    </script>
    <script type="text/javascript" language="javascript">
        function Payment_No(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("<%=pnlPopup.ClientID %>").show();
$("#<%=btntxtPayment_No.ClientID %>").click();
}
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txtPayment_No.ClientID %>").val();
unit = "0" + unit;
$("#<%=txtPayment_No.ClientID %>").val(unit);
__doPostBack("txtPayment_No %>", "TextChanged");
}
}
function CashBank(e) {
    if (e.keyCode == 112) {
        e.preventDefault();
        $("<%=pnlPopup.ClientID %>").show();
$("#<%=btntxtCashBank.ClientID %>").click();
}
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txtCashBank.ClientID %>").val();
unit = "0" + unit;
$("#<%=txtCashBank.ClientID %>").val(unit);
__doPostBack("txtCashBank %>", "TextChanged");
}
}
function Ac_Code(e) {
    if (e.keyCode == 112) {
        e.preventDefault();
        document.getElementById("<%=pnlPopup.ClientID %>").show();
$("#<%=btntxtAc_Code.ClientID %>").click();
}
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txtAc_Code.ClientID %>").val();
unit = "0" + unit;
var unit = $("#<%=txtAc_Code.ClientID %>").val(unit);
var unit = __doPostBack("txtAc_Code %>", "TextChanged");
}
    if (e.keyCode == 13) {
        e.preventDefault();
        var unit = $("#<%=txtAc_Code.ClientID %>").focus();
}
}
    </script>
    <script type="text/javascript" language="javascript">
        function DeleteConform() {
            $("#loader").show();
            var DocNo = document.getElementById("#<%=hdnfdocno.ClientID %>").value();
 var purcid = document.getElementById("#<%=hdnfautoid.ClientID %>").value();
     var Branch_Code = '<%= Session["Branch_Code"] %>';
     var Company_Code = '<%= Session["Company_Code"] %>';
     var Year_Code = '<%= Session["year"] %>';
     var XML = "<ROOT><CheckHeadNew='" + DocNo + "' Check_Id='" + purcid + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "'></CheckHeadNew></ROOT>";
     var spname = "CheckHeadNew";
     var status = document.getElementById("#<%= btnDelete.ClientID %>").value();
  ProcessXML(XML, status, spname);
}
function Validate() {
    $("#loader").show();
    var StartDate = '<%= Session["Start_Date"] %>';
 var EndDate = '<%= Session["End_Date"] %>';
     return true;
 }
        function pagevalidation() {
            debugger;
     $("#loader").hide();
     var Doc_No = 0, headautoid = 0, autodetailid = 0, GId = 0;
     var spname = "CheckHeadNew";
     var XML = "<ROOT>";
     var status = document.getElementById("<%=btnSave.ClientID %>").value;
if (status == "Update") {
    Doc_No = document.getElementById("<%=hdnfdocno.ClientID %>").value;
    headautoid = document.getElementById("<%=hdnfautoid.ClientID %>").value;
}
    var gridView = document.getElementById("<%=grdDetail.ClientID %>");
    
    var headauto_id = $("<%=txtCheck_Id.ClientID %>").val();
var doc_no = $("<%=txtDoc_No.ClientID %>").val();
    var Tran_Type = "PS";
    var Company_Code = '<%= Session["Company_Code"] %>';
var Year_Code = '<%= Session["year"] %>';
    var Branch_Code = '<%= Session["Branch_Code"] %>';
    var Check_Id = $(" #<%=txtCheck_Id.ClientID%>").val() == "" ? 0 : $(" #<%=txtCheck_Id.ClientID%>").val();
    var CheckFrom = $(" #<%=txtCheckFrom.ClientID%>").val() == "" ? 0 : $(" #<%=txtCheckFrom.ClientID%>").val();
    var CheckTo = $(" #<%=txtCheckTo.ClientID%>").val() == "" ? 0 : $(" #<%=txtCheckTo.ClientID%>").val();
    var d = $("#<%=txtDoc_Date.ClientID %>").val();
    var Doc_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
    var Payment_No = $(" #<%=txtPayment_No.ClientID%>").val() == "" ? 0 : $(" #<%=txtPayment_No.ClientID%>").val();
    var CashBank = $(" #<%=txtCashBank.ClientID%>").val() == "" ? 0 : $(" #<%=txtCashBank.ClientID%>").val();
    var Qty = $(" #<%=txtQty.ClientID%>").val() == "" ? 0.00 : $(" #<%=txtQty.ClientID%>").val();     var pid = document.getElementById("<%= hdnfpidid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpidid.ClientID %>").value;
     var cbid = document.getElementById("<%= hdnfcbidid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfcbidid.ClientID %>").value;
    var Doc_No = "0";
    if (status == "Save") {
        DOCNO = "Check_Id='" + Check_Id + "'";
    }
    else {
        DOCNO = "Doc_No='" + doc_no + "' Check_Id='" + Check_Id + "'";
    }
    var XML = XML + "<CheckHeadNew " + DOCNO + " CheckFrom ='" + CheckFrom + "' CheckTo ='" + CheckTo + "' Doc_Date ='" + Doc_Date
        + "' Payment_No ='" + Payment_No + "' CashBank ='" + CashBank + "' Qty ='" + Qty + "' pid ='" + pid + "' cbid ='" + cbid
        + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "'>";
    var RowAction = 10;
    var gridView = document.getElementById("<%=grdDetail.ClientID %>");
     var grdrow = gridView.getElementsByTagName("tr");

    var ddid = CheckDetail_Id;
    for (var i = 1; i < grdrow.length; i++) {
        var Detail_Id = gridView.Rows[i].Cells[2].innerHTML;
        var CheckDetail_Id = gridView.rows[i].Cells[3].innerHTML;
        var Ac_Code = gridView.rows[i].Cells[4].innerHTML;
        var Chq_Caption = gridView.rows[i].Cells[6].innerHTML;
        var Amount = gridView.rows[i].Cells[7].innerHTML;
        var Narration = gridView.rows[i].Cells[8].innerHTML;
        var d = gridView.rows[i].Cells[9].innerHTML;
        var Bank_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
        var Ac_Id = gridView.rows[i].Cells[10].innerHTML;
        if (gridView.rows[i].cells[11].innerHTML == "A") {
            XML = XML + "<DetailInsert Doc_No='" + Doc_No + "' Detail_Id ='" + Detail_Id + "' CheckDetail_Id ='" + CheckDetail_Id
                + "' Ac_Code ='" + Ac_Code + "' Chq_Caption ='" + Chq_Caption + "' Amount ='" + Amount + "' Narration ='" + Narration
                + "' Bank_Date ='" + Bank_Date + "' Ac_Id ='" + Ac_Id + "' Company_Code='" + Company_Code
                + "' Year_Code='" + Year_Code + "' Check_Id='" + Check_Id + "' />";
        }
        if (gridView.rows[i].cells[RowAction].innerHTML == "U") {
            XML = XML + "<DetailUpdate Doc_No='" + Doc_No + "' Detail_Id ='" + Detail_Id + "' CheckDetail_Id ='" + CheckDetail_Id
                + "' Ac_Code ='" + Ac_Code + "' Chq_Caption ='" + Chq_Caption + "' Amount ='" + Amount + "' Narration ='" + Narration
                + "' Bank_Date ='" + Bank_Date + "' Ac_Id ='" + Ac_Id + "' Company_Code='" + Company_Code
                + "' Year_Code='" + Year_Code + "' Check_Id='" + Check_Id + "' />";
        }
        if (gridView.rows[i].cells[RowAction].innerHTML == "D") {
            XML = XML + "<DetailDelete Doc_No='" + Doc_No + "' Detail_Id ='" + Detail_Id + "' CheckDetail_Id ='" + CheckDetail_Id
                + "' Ac_Code ='" + Ac_Code + "' Chq_Caption ='" + Chq_Caption + "' Amount ='" + Amount + "' Narration ='" + Narration
                + "' Bank_Date ='" + Bank_Date + "' Ac_Id ='" + Ac_Id + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code
                + "'Check_Id='" + Check_Id + "'/>";
        }
    }
    XML = XML + "</CheckHeadNew></ROOT>"
    ProcessXML(XML, status, spname);
}
function ProcessXML(XML, status, spname) {
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
    function OnSuccess(response) {
        $("#loader").hide();
        if (status != "Delete") {
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
                window.open('..//pgeCheckPrintNew.aspx?Check_Id=' + id + '&Action=1', "_self");
            }
        }
        else {
            var num = parseInt(response.d);
            if (isNaN(num)) {
                swal("" + response.d + "", "", "warning");
            }
            else {
                //Enter utility page name with folder location
                window.open('..//.aspx', "self");
            }
        }
    }
}
    </script>
    <style type="text/css">
        #loader {
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
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Enter page name " Font-Names="verdana" ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfdocno" runat="server" />
            <asp:HiddenField ID="hdnfautoid" runat="server" />
            <asp:HiddenField ID="hdnfpidid" runat="server" />
            <asp:HiddenField ID="hdnfcbidid" runat="server" />
            <table width="80%" align="left">
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
                    </td>
                    <td align="center">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp" OnClick="btnFirst_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp" OnClick="btnPrevious_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp" OnClick="btnNext_Click" Width="90px" Height="24px" />
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp" OnClick="btnLast_Click" Width="90px" Height="24px" />
                    </td>
                </tr>
            </table>

            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black" Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">

                <table style="width: 60%;" align="Left" cellpadding="4" cellspacing="4">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana" Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                        <tr>
                            <td align="left" style="width: 10%;">changeno
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtEditDoc_no" runat="Server" CssClass="txt" TabIndex="1" Width="30px" Style="text-align: left;"
                                    AutoPostBack="false" OnTextChanged="txtEditDoc_no_TextChanged"></asp:TextBox>
                            </td>



                            <td align="left" style="width: 10%;">Doc_No
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtDoc_No" runat="Server" CssClass="txt" TabIndex="2" Width="30px" Style="text-align: left;"
                                    AutoPostBack="false" OnTextChanged="txtDoc_No_TextChanged"
                                    onkeydown="Doc_No(event)"></asp:TextBox>
                                <asp:Button Width="20px" Height="24px" ID="btntxtDoc_No" runat="server" Text="..."
                                    OnClick="btntxtDoc_No_Click" CssClass="btnHelp" />
                                <asp:Label ID="lbldoc_no" runat="server" CssClass="lblName"></asp:Label></td>



                            <td align="left" style="width: 10%;">Check_Id
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtCheck_Id" runat="Server" CssClass="txt" TabIndex="3" Width="90px" Style="text-align: left;"
                                    AutoPostBack="false" OnTextChanged="txtCheck_Id_TextChanged"></asp:TextBox>
                            </td>



                            <td align="left" style="width: 10%;">CheckFrom
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtCheckFrom" runat="Server" CssClass="txt" TabIndex="4" Width="90px" Style="text-align: left;"
                                    AutoPostBack="false" OnTextChanged="txtCheckFrom_TextChanged"></asp:TextBox>
                            </td>



                            <td align="left" style="width: 10%;">CheckTo
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtCheckTo" runat="Server" CssClass="txt" TabIndex="5" Width="90px" Style="text-align: left;"
                                    AutoPostBack="false" OnTextChanged="txtCheckTo_TextChanged"></asp:TextBox>
                            </td>



                            <td align="left" style="width: 10%;">DocDate
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtDoc_Date" runat="Server" CssClass="txt" TabIndex="6" Width="90px" Style="text-align: left;"
                                    AutoPostBack="false" OnTextChanged="txtDoc_Date_TextChanged" onkeyup="ValiddateDate(this,event.keyCode)" onkeydown="retun DateFormat(this,event.keyCode)"></asp:TextBox>
                                <asp:Image ID="imgcalendertxtDoc_Date" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px" Height="15px" /><ajax1:CalendarExtender ID="CalendarExtenderDatetxtDoc_Date" runat="server" TargetControlID="txtDoc_Date" PopupButtonID="imgcalendertxtDoc_Date" Format="dd/MM/yyyy"></ajax1:CalendarExtender>
                            </td>


                        </tr>
                        <tr>

                            <td align="left" style="width: 10%;">PaymentNo
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtPayment_No" runat="Server" CssClass="txt" TabIndex="7" Width="90px" Style="text-align: left;"
                                    AutoPostBack="false" OnTextChanged="txtPayment_No_TextChanged"
                                    onkeydown="Payment_No(event)"></asp:TextBox>
                                <asp:Button Width="20px" Height="24px" ID="btntxtPayment_No" runat="server" Text="..."
                                    OnClick="btntxtPayment_No_Click" CssClass="btnHelp" />
                                <asp:Label ID="lblPayment_No" runat="server" CssClass="lblName"></asp:Label></td>



                            <td align="left" style="width: 10%;">Cash Bank
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtCashBank" runat="Server" CssClass="txt" TabIndex="8" Width="90px" Style="text-align: left;"
                                    AutoPostBack="false" OnTextChanged="txtCashBank_TextChanged"
                                    onkeydown="CashBank(event)"></asp:TextBox>
                                <asp:Button Width="20px" Height="24px" ID="btntxtCashBank" runat="server" Text="..."
                                    OnClick="btntxtCashBank_Click" CssClass="btnHelp" />
                                <asp:Label ID="lblCashBank" runat="server" CssClass="lblName"></asp:Label></td>



                            <td align="left" style="width: 10%;">Qty
                            </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox Height="24px" ID="txtQty" runat="Server" CssClass="txt" TabIndex="9" Width="90px" Style="text-align: left;"
                                    AutoPostBack="false" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                            </td>


                            <tr>
                                <td colspan="4" align="left">
                                    <asp:Button ID="btnOpenDetailsPopup" runat="server" Text="ADD" CssClass="btnHelp" Width="80px" Height="25px"
                                        OnClick="btnOpenDetailsPopup_Click" TabIndex="28" Visible="false" />
                                </td>

                            </tr>
                </table>
                <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                    <legend style="text-align: center;">
                        <h5 style="color: White;" font-names="verdana" font-size="Medium">Detail Entry</h5>
                </fieldset>
            </asp:Panel>

            <div style="width: 100%; position: relative;">
                <table width="80%" align="Left">

                    <tr>
                        <td colspan="2" align="center" style="background-color: lightslategrey; color: White;">
                            <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana" Text="Enter Name"></asp:Label>
                        </td>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblID" runat="server" CssClass="lblName" Font-Names="verdana" Text="" Visible="false"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblNo" runat="server" CssClass="lblName" Font-Names="verdana" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td align="left" style="width: 10%;">CheckDetail Id </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtCheckDetail_Id" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px" OnTextChanged="txtCheckDetail_Id_TextChanged" Style="text-align: left;" TabIndex="3" Width="90px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 10%;">Ac Code </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtAc_Code" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px" onkeydown="AC_CODE(event)" OnTextChanged="txtAc_Code_TextChanged" Style="text-align: left;" TabIndex="4" Width="90px"></asp:TextBox>
                                <asp:Button ID="btntxtAc_Code" runat="server" CssClass="btnHelp" OnClick="btntxtAc_Code_Click" Text="..." />
                                <asp:Label ID="lblAc_Name" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                            <td align="left" style="width: 10%;">Chq Caption </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtChq_Caption" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px" OnTextChanged="txtChq_Caption_TextChanged" Style="text-align: left;" TabIndex="5" Width="90px"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 10%;">Amount </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtAmount" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px" OnTextChanged="txtAmount_TextChanged" Style="text-align: left;" TabIndex="6" Width="90px"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 10%;">Narration </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtNarration" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px" OnTextChanged="txtNarration_TextChanged" Style="text-align: left;" TabIndex="7" Width="90px"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 10%;">Bank Date </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtBank_Date" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px" onkeydown="retun DateFormat(this,event.keyCode)" onkeyup="ValiddateDate(this,event.keyCode)" OnTextChanged="txtBank_Date_TextChanged" Style="text-align: left;" TabIndex="8" Width="90px"></asp:TextBox>
                                <asp:Image ID="imgcalendertxtBank_Date" runat="server" Height="15px" ImageUrl="~/Images/calendar_icon1.png" Width="25px" />
                                <ajax1:CalendarExtender ID="CalendarExtenderDatetxtBank_Date" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgcalendertxtBank_Date" TargetControlID="txtBank_Date">
                                </ajax1:CalendarExtender>
                            </td>
                            <td align="left" style="width: 10%;">AcId </td>
                            <td align="left" style="width: 10%;">
                                <asp:TextBox ID="txtAc_Id" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px" OnTextChanged="txtAc_Id_TextChanged" Style="text-align: left;" TabIndex="9" Width="30px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Button ID="btnAdddetails" runat="server" CssClass="btnHelp" Height="25px" OnClick="btnAdddetails_Click" TabIndex="28" Text="ADD" Width="80px" />
                                <asp:Button ID="btnClosedetails" runat="server" CssClass="btnHelp" Height="25px" OnClick="btnClosedetails_Click" TabIndex="28" Text="Close" Width="80px" />
                            </td>
                        </tr>


                    </tr>
                </table>
                <asp:UpdatePanel ID="upGrid" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlgrdDetail" runat="server" ScrollBars="Both" Height="200px" Width="1000px" BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true" Font-Names="Verdana" Font-Size="11px" BackColor="SeaShell" Style="margin-left: 30px; float: left;">
                            <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" HeaderStyle-BackColor="#397CBB"
                                HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both"
                                Width="100%" OnRowCommand="grdDetail_RowCommand" CellPadding="5" CellSpacing="5"
                                OnRowDataBound="grdDetail_RowDataBound" Style="table-layout: fixed;">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" Text="Edit" CommandArgument="lnk"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord" Text="Delete" CommandArgument="lnk"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="25px" Wrap="false" ForeColor="Black" />
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <asp:Panel onkeydown="closepopup(event);" ID="pnlPopup" runat="server" Width="70%" align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight" Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg" Width="20px" Height="20px" Style="float: right; vertical-align: top;" OnClick="imgBtnClose_Click" ToolTip="Close" />
                <table width="95%">
                    <tr>
                        <td align="center" style="background-color: #F5B540; width: 100%;">
                            <asp:Label ID="lblPopupHead" runat="server" Font-Size="Medium" Font-Names="verdana" Font-Bold="true" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Search Text:
                            <asp:TextBox onkeydown="closepopup(event);" ID="txtSearchText" runat="server" Width="250px" Height="20px" AutoPostBack="false" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button onkeydown="closepopup(event);" ID="btnSearch" runat="server" Text="Search" CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4" Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView Font-Bold="true" CssClass="select" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdPopup_PageIndexChanging" ID="grdPopup" runat="server" AutoGenerateColumns="true" Width="100%" EmptyDataText="No Records Found"
                                    HeaderStyle-BackColor="#6D8980" HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
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
            <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="GhostWhite" Width="955px" BorderColor="Teal" BorderWidth="1px" Height="300px"
                BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

