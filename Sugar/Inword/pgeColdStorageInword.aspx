<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeColdStorageInword.aspx.cs" Inherits="Sugar_Inword_pgeColdStorageInword" %>

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
                if (hdnfClosePopupValue == "txtDoc_No") {
                    document.getElementById("<%= txtDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblDoc_No.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtParty_Code") {
                    document.getElementById("<%= txtParty_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblParty_Code.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtParty_Code.ClientID %>").focus();
                }               
                if (hdnfClosePopupValue == "txtItem_Code") {
                    document.getElementById("<%= txtItem_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblItemname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtItem_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBrand_Code") {
                    document.getElementById("<%= txtBrand_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%= lblBrandname.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBrand_Code.ClientID %>").focus();
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
                //                doWhateverYouWantNowThatYourKeyWasHit();
                debugger;
                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;

                if (hdnfClosePopupValue == "txtParty_Code") {

                    document.getElementById("<%=txtParty_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtItem_Code") {

                    document.getElementById("<%=txtItem_Code.ClientID %>").focus();
                } if (hdnfClosePopupValue == "txtBrand_Code") {

                    document.getElementById("<%=txtBrand_Code.ClientID %>").focus();
                } 
            }
        });

    </script>

    <script type="text/javascript" language="javascript">

        function SB(billno) {
            var billno = document.getElementById('<%=txtDoc_No.ClientID %>').value;
            var partycode = document.getElementById('<%=txtParty_Code.ClientID %>').value;

            window.open('../Report/rptColdStorageInword.aspx?billno=' + billno + '&partycode=' + partycode)

        }
        function chanegno(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                var edi = "txtEditDoc_No";
                $("#<%=hdnfClosePopup.ClientID %> ").val(edi);
                $("#<%=btnSearch.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtEditDoc_No", "TextChanged");
            }
        }
        function Doc_No(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtDoc_No.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtDoc_No", "TextChanged");
            }
        }
        function Party_Code(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtParty_Code.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                debugger;
                e.preventDefault();
                __doPostBack("txtParty_Code", "TextChanged");
            }
        }

        function Item_Code(e) {
            debugger;
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtItem_Code.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                __doPostBack("txtItem_Code", "TextChanged");
            }
            if (e.keyCode == 13) {
                debugger;
                e.preventDefault();
                $("#<%=btnSave.ClientID %> ").focus();
            }      
        }
        
        function Brand_Code(e) {
            if (e.keyCode == 112) {
                e.preventDefault();
                $("#<%=btntxtBrand_Code.ClientID %> ").click();
            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var buss = $("#<%=txtBrand_Code.ClientID %>").val();
                buss = "0" + buss;
                $("#<%=txtBrand_Code.ClientID %>").val(buss);
                __doPostBack("txtBrand_Code", "TextChanged");
            }
        }
        
        function Back() {

        //alert(td);
        window.open('../Inword/pgeColdStorageInwordUtility.aspx', '_self');
        }
    </script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        
         function Calculations(e) {

             if (e.keyCode == 9 || e == 9) {
                 debugger;
                 //declare Require Fields And Assign Values also Check CsCalculation Method In BackEnd              
                 var companystatecode = parseInt('<%= Session["CompanyGSTStateCode"] %>');
                
                 var Qty = $("#<%=txtQty.ClientID %>").val() == "" ? 0 : $("#<%= txtQty.ClientID %>").val();
                 var Wtper = $("#<%=txtWtper.ClientID %>").val() == "" ? 0 : $("#<%= txtWtper.ClientID %>").val();
                 var Netkg = parseFloat(Qty * Wtper);
                 document.getElementById("<%=txtNetkg.ClientID %>").value = Netkg;

             }
         }
         function Validate() {
            
             debugger;
             $("#loader").show();
             var StartDate = '<%= Session["Start_Date"] %>';
             var EndDate = '<%= Session["End_Date"] %>';

             var d = $("#<%=txtDoc_Date.ClientID %>").val();
             var DocDates = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

             StartDate = StartDate.slice(6, 11) + "/" + StartDate.slice(3, 5) + "/" + StartDate.slice(0, 2);
             EndDate = EndDate.slice(6, 11) + "/" + EndDate.slice(3, 5) + "/" + EndDate.slice(0, 2);

             if (DocDates >= StartDate && DocDates <= EndDate) {
             }
             else {
                 $("#<%=txtDoc_Date.ClientID %>").focus();
                 alert('Not A Valid Date Range')
                 $("#loader").hide();
                 return false;
             }
             if ($("#<%=txtParty_Code.ClientID %>").val() == "" || $("#<%=txtParty_Code.ClientID %>").val() == "0") {
                 $("#<%=txtParty_Code.ClientID %>").focus();
                 $("#loader").hide();
                 return false;

             }
             var i = $("#<%=txtChallan_Date.ClientID %>").val();
             var inworddate = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
           
             return true;
         }
         function pagevalidation() {
             debugger;
             try {
                 $("#loader").show();

                 var Doc_No = 0, csid = 0;
                 var status = document.getElementById("<%= btnSave.ClientID %>").value;
                 var spname = "ColdStorageInword";
                 var XML = "<ROOT>";
                 if (status == "Update") {
                     Doc_No = document.getElementById("<%= hdnfdoc.ClientID %>").value;
                     csid = document.getElementById("<%= hdnfid.ClientID %>").value;
                    
                 }
             
                 var d = $("#<%=txtDoc_Date.ClientID %>").val();
                 var Doc_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

                 var Ac_Code = $("#<%=txtParty_Code.ClientID %>").val();
                 if (Ac_Code == "&nbsp;") {
                     Ac_Code = 0;
                 }

                 var acid = document.getElementById("<%= hdnfacid.ClientID %>").value;
                 if (acid == "&nbsp;") {
                     acid = 0;
                 }

                 var Challan_No = $("#<%=txtChallan_No.ClientID %>").val();
                 if (Challan_No == "&nbsp;") {
                     Challan_No = "";
                 }
                 var d = $("#<%=txtChallan_Date.ClientID %>").val();
                 var Challan_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

                 var Vahical_No = $("#<%=txtVahical_No.ClientID %>").val();
                 if (Vahical_No == "&nbsp;") {
                     Vahical_No = "";
                 }
                             
                 var ic = document.getElementById("<%= hdnfic.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfic.ClientID %>").value;               

                 var Branch_Code = '<%= Session["Branch_Code"] %>';
                 var Company_Code = '<%= Session["Company_Code"] %>';
                 var Year_Code = '<%= Session["year"] %>';
                 var USER = '<%= Session["user"] %>';
                 var DOCNO = "";
                
                 XML = XML + "<RetailHead Doc_No='" + Doc_No + "' Doc_Date='" + Doc_Date + "' Ac_Code='" + Ac_Code + "' Ac_Id='" + acid + "' Inword_No='" + Challan_No + 
                      "' Inword_Date='" + Challan_Date + "' Company_Code='" + Company_Code + "'  Year_Code='" + Year_Code + "' Created_By='" + USER + "' " +
                     "Modified_By='" + USER + "' csid='" + csid + "' Vahical_No='" + Vahical_No + "'>";

                 var gridView = document.getElementById("<%=grdDetail.ClientID %>");
                 var grdrow = gridView.getElementsByTagName("tr");
                 var ddid = 1;
                 var Order_Code = 1;
                 for (var i = 1; i < grdrow.length; i++) {

                     var Detail_Id = gridView.rows[i].cells[2].innerHTML;                 
                     var Item_Code = gridView.rows[i].cells[3].innerHTML;
                     var Brand_Code = gridView.rows[i].cells[5].innerHTML;
                     var Qty = gridView.rows[i].cells[7].innerHTML;
                     var Wtper = gridView.rows[i].cells[8].innerHTML;
                     var Netkg = gridView.rows[i].cells[9].innerHTML;
                   
                     var ic = gridView.rows[i].cells[12].innerHTML;
                    
                   
                     if (gridView.rows[i].cells[10].innerHTML == "A") {

                         XML = XML + "<DetailInsert Doc_No='" + Doc_No + "' Detail_Id='" + Detail_Id + "' csid='" + csid + "'  Item_Code='" + Item_Code + "' Brand_Code='" + Brand_Code + "' " +
                             "No_Of_Bags='" + Qty + "' Wt_Per='" + Wtper + "' Net_Wt='" + Netkg + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' ic='" + ic + "' csdetailid='" + ddid + "' />";
                         ddid = parseInt(ddid) + 1;
                     }
                     if (gridView.rows[i].cells[10].innerHTML == "U") {
                         var csdetailid = gridView.rows[i].cells[13].innerHTML;
                         XML = XML + "<Details Doc_No='" + Doc_No + "' Detail_Id='" + Detail_Id + "' csid='" + csid + "'  Item_Code='" + Item_Code + "' Brand_Code='" + Brand_Code + "' " +
                             "No_Of_Bags='" + Qty + "' Wt_Per='" + Wtper + "' Net_Wt='" + Netkg + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' ic='" + ic + "' csdetailid='" + csdetailid + "' />";
                     }
                     if (gridView.rows[i].cells[10].innerHTML == "D") {
                         var csdetailid = gridView.rows[i].cells[13].innerHTML;
                         XML = XML + "<DetailDelete Doc_No='" + Doc_No + "' Detail_Id='" + Detail_Id + "' csid='" + csid + "'  Item_Code='" + Item_Code + "' Brand_Code='" + Brand_Code + "' " +
                             "No_Of_Bags='" + Qty + "' Wt_Per='" + Wtper + "' Net_Wt='" + Netkg + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' ic='" + ic + "' csdetailid='" + csdetailid + "' />";
                     }                   

                 }
                
                 XML = XML + "</RetailHead></ROOT>";

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
                         window.open('../Inword/pgeColdStorageInword.aspx?csid=' + id + '&Action=1', "_self");

                     }
                 }
             }
             catch (exx) {
                 $("#loader").hide();
                 alert(exx)
             }
         }
    </script>

    <%--<script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

    </script>--%>
    <style>
        #loader
        {
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
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 100%; margin-left: 10px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="Cold Storage Inword" Font-Names="verdana"
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
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />
            <asp:HiddenField ID="hdnfdoc" runat="server" />
            <asp:HiddenField ID="hdnfid" runat="server" />
            <asp:HiddenField ID="hdnfdetailid" runat="server" />
            <asp:HiddenField ID="hdnfic" runat="server" />           
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
            <asp:HiddenField ID="hdnfacid" runat="server" />
              <asp:HiddenField ID="hdnfcsdetail" runat="server" />
            <table width="80%" align="center">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px" 
                            
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px" TabIndex="16"
                            ValidationGroup="add" OnClick="btnSave_Click" Height="24px" OnClientClick="if (!Validate()) return false;" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" OnClientClick="if (!Validate()) return false;" />
                        &nbsp;                      
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" Height="24px" OnClientClick="Back()" />
                        &nbsp;
                        <asp:Button runat="server" ID="btnPrintSaleBill" Text="Print" CssClass="btnHelp"
                            Width="80px" Height="24px" OnClientClick="SB();" />
                    </td>
                    <td align="center">
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnFirst_Click" Visible="false" Width="90px" Height="24px" />
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnPrevious_Click" Visible="false" Width="90px" Height="24px" />
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnNext_Click" Visible="false" Width="90px" Height="24px" />
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnLast_Click" Visible="false" Width="90px" Height="24px" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table style="width: 60%;" align="left" cellpadding="4" cellspacing="4">
                    <tr>
                        <td>Change No
                            <asp:TextBox Height="24px" ID="txtEditDoc_No" runat="Server" CssClass="txt" TabIndex="1"
                                Width="90px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Doc No
                            <asp:TextBox ID="txtDoc_No" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                OnkeyDown="Doc_No(event);" OnTextChanged="txtDoc_No_TextChanged" Style="text-align: left;"
                                TabIndex="2" Width="90px"></asp:TextBox>
                            <asp:Button ID="btntxtDoc_No" runat="server" CssClass="btnHelp" Height="24px" OnClick="btntxtDoc_No_Click"
                                Text="..." Width="70px" />
                            <asp:Label ID="lblDoc_No" runat="server" CssClass="lblName"></asp:Label>
                            Doc Date
                            <asp:TextBox ID="txtDoc_Date" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" onkeydown="retun DateFormat(this,event.keyCode)" onkeyup="ValiddateDate(this,event.keyCode)"
                                OnTextChanged="txtDoc_Date_TextChanged" Style="text-align: left;" TabIndex="3"
                                Width="90px"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtDoc_Date" runat="server" Height="15px" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" />
                            <ajax1:CalendarExtender ID="CalendarExtenderDatetxtDoc_Date" runat="server" Format="dd/MM/yyyy"
                                PopupButtonID="imgcalendertxtDoc_Date" TargetControlID="txtDoc_Date">
                            </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>Challan No
                            <asp:TextBox ID="txtChallan_No" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnTextChanged="txtChallan_No_TextChanged" Style="text-align: left;"
                                TabIndex="4" Width="250px"></asp:TextBox>
                            Challan Date
                            <asp:TextBox ID="txtChallan_Date" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" onkeydown="retun DateFormat(this,event.keyCode)" onkeyup="ValiddateDate(this,event.keyCode)"
                                OnTextChanged="txtChallan_Date_TextChanged" Style="text-align: left;" TabIndex="5"
                                Width="90px"></asp:TextBox>
                            <asp:Image ID="imgcalendertxtChallan_Date" runat="server" Height="15px" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" />
                            <ajax1:CalendarExtender ID="CalendarExtenderDatetxtChallan_Date" runat="server" Format="dd/MM/yyyy"
                                PopupButtonID="imgcalendertxtChallan_Date" TargetControlID="txtChallan_Date">
                            </ajax1:CalendarExtender>
                            Vehical No
                            <asp:TextBox ID="txtVahical_No" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnTextChanged="txtVahical_No_TextChanged" Style="text-align: left;"
                                TabIndex="6" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Bill To
                            <asp:TextBox ID="txtParty_Code" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnkeyDown="Party_Code(event);" OnTextChanged="txtParty_Code_TextChanged"
                                Style="text-align: left;" TabIndex="7" Width="90px"></asp:TextBox>
                            <asp:Button ID="btntxtParty_Code" runat="server" CssClass="btnHelp" Height="24px"
                                OnClick="btntxtParty_Code_Click" Text="..." Width="20px" />
                            <asp:Label ID="lblParty_Code" runat="server" CssClass="lblName"></asp:Label>
                            Bill To State Code:<asp:Label ID="lblpartyStatecode" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                </table>
                <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 100%; margin-left: 10px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
                    <legend style="text-align: center;">
                        <h5 style="color: White;" font-names="verdana" font-size="Medium">Detail Entry</h5>
                    </legend>
                </fieldset>
                <table width="80%" align="left">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblID" runat="server" CssClass="lblName" Font-Names="verdana" Text=""
                                Visible="false"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblNo" runat="server" CssClass="lblName" Font-Names="verdana" Text=""
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Item Code
                            <asp:TextBox ID="txtItem_Code" runat="Server" AutoPostBack="false" CssClass="txt"
                                onkeydown="Item_Code(event);" Height="24px" OnTextChanged="txtItem_Code_TextChanged" Style="text-align: left;"
                                TabIndex="8" Width="90px"></asp:TextBox>
                            <asp:Button ID="btntxtItem_Code" runat="server" CssClass="btnHelp" OnClick="btntxtItem_Code_Click"
                                Text="..." />
                            <asp:Label ID="lblItemname" runat="server" CssClass="lblName"></asp:Label>
                            Brand Code
                            <asp:TextBox ID="txtBrand_Code" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnTextChanged="txtBrand_Code_TextChanged" Style="text-align: left;"
                                TabIndex="9" Width="90px" onkeydown="Brand_Code(event);"></asp:TextBox>
                            <asp:Button ID="btntxtBrand_Code" runat="server" CssClass="btnHelp" OnClick="btntxtBrand_Code_Click"
                                Text="..." />
                            <asp:Label ID="lblBrandname" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Qty                       
                            <asp:TextBox ID="txtQty" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                              onkeydown="Calculations(event);"  OnTextChanged="txtQty_TextChanged" Style="text-align: left;" TabIndex="10" Width="90px"></asp:TextBox>
                            Wtper
                            <asp:TextBox ID="txtWtper" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                onkeydown="Calculations(event);" OnTextChanged="txtWtper_TextChanged" Style="text-align: left;" TabIndex="11"
                                Width="90px"></asp:TextBox>
                            Netkg
                            <asp:TextBox ID="txtNetkg" runat="Server" AutoPostBack="false" CssClass="txt" Height="24px"
                                OnTextChanged="txtNetkg_TextChanged" Style="text-align: left;" TabIndex="12"
                                Width="90px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnAdddetails" runat="server" CssClass="btnHelp" Height="25px" OnClick="btnAdddetails_Click"
                                TabIndex="13" Text="ADD" Width="80px" />
                            <asp:Button ID="btnClosedetails" runat="server" CssClass="btnHelp" Height="25px"
                                OnClick="btnClosedetails_Click" TabIndex="14" Text="Close" Width="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="11">
                            <table width="100%">
                                <tr>
                                    <td width="100%" align="left" style="margin-top: 100px;">
                                        <div style="width: 100%; position: relative;">
                                            <asp:UpdatePanel ID="upGrid" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="pnlgrdDetail" runat="server" BackColor="SeaShell" BorderColor="Maroon"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"
                                                        Height="200px" ScrollBars="Both" Style="margin-left: 30px; float: left;" Width="1500px">
                                                        <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" CellPadding="5"
                                                            CellSpacing="5" GridLines="Both" HeaderStyle-BackColor="#397CBB" HeaderStyle-ForeColor="White"
                                                            HeaderStyle-Height="30px" OnRowCommand="grdDetail_RowCommand" OnRowDataBound="grdDetail_RowDataBound"
                                                            Style="table-layout: fixed;" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="lnk" CommandName="EditRecord"
                                                                            Text="Edit"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument="lnk" CommandName="DeleteRecord"
                                                                            Text="Delete"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle ForeColor="Black" Height="25px" Wrap="false" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>

                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel onkeydown="closepopup(event);" ID="pnlPopup" runat="server" Width="70%"
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
            <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="Black" Width="955px"
                BorderColor="" BorderWidth="1px" Height="300px" BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
                <tr>
                    <td align="left">
                        <asp:Button ID="btnOpenDetailsPopup" runat="server" CssClass="btnHelp" Height="25px"
                            OnClick="btnOpenDetailsPopup_Click" TabIndex="15" Text="ADD" Visible="false"
                            Width="80px" />
                    </td>
                </tr>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

