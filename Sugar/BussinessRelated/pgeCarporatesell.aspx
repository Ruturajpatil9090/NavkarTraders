<%@ Page Title="Carporate Sell" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeCarporatesell.aspx.cs" Inherits="pgeCarporatesell" %>

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
                if (hdnfClosePopupValue == "txtac_code") {
                    document.getElementById("<%=txtac_code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtunit_code") {
                    document.getElementById("<%=txtunit_code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBroker") {
                    document.getElementById("<%=txtBroker.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBill_To") {
                    document.getElementById("<%=txtBill_To.ClientID %>").focus();
                }
                document.getElementById("<%=txtSearchText.ClientID %>").value = "";
                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";

            }

        });
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


                if (hdnfClosePopupValue == "txtdoc_no") {
                    document.getElementById("<%=txtdoc_no.ClientID %>").disabled = false;
                    document.getElementById("<%=txtdoc_no.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;

                    ///// document.getElementById("<%=txtac_code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtac_code") {
                    document.getElementById("<%=txtac_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblParty_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtac_code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtunit_code") {
                    document.getElementById("<%=txtunit_code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblUnit_name.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtunit_code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBroker") {
                    document.getElementById("<%=txtBroker.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBroker.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBroker.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBill_To") {
                    document.getElementById("<%=txtBill_To.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBill_To.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBill_To.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = "";
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();

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
        function ac_name(e) {
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtac_code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtac_code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtac_code.ClientID %>").val(unit);
                __doPostBack("txtac_code", "TextChanged");

            }
        }




        function Unitname(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtunit_code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtunit_code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtunit_code.ClientID %>").val(unit);
                __doPostBack("txtunit_code", "TextChanged");

            }
        }
        function billto(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtbill_To.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBill_To.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtBill_To.ClientID %>").val(unit);
                __doPostBack("txtBill_To", "TextChanged");

            }
        }

        function Brokername(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtBroker.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtBroker.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtBroker.ClientID %>").val(unit);
                __doPostBack("txtBroker", "TextChanged");

            }
        }
        function save(e) {
            debugger;

            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=btnSave.ClientID %>").focus();
            }

        }
    </script>
    <script type="text/javascript">
        function Back() {

            //alert(td);
            window.open('../BussinessRelated/PgeCarporatesaleUtility.aspx', '_self');
        }
        function Focusbtn(e) {
            debugger;

            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=txtSCDate.ClientID %>").focus();
            }

        }
        function calculateFinanceDetail() {
            debugger;
        }

        function CarporateOPen(CarpID) {
            var Action = 1;
            window.open('../BussinessRelated/pgeCarporatesell.aspx?carpid=' + CarpID + '&Action=' + Action, "_self");
        }
    </script>
    <script type="text/javascript">
        function DeleteConform() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= hdnfcarpdoc.ClientID %>").value;
            var Autoid = document.getElementById("<%= hdnfcarpid.ClientID %>").value;

            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';

            var XML = "<ROOT><CarporateHead doc_no='" + DocNo + "' carpid='" + Autoid + "' company_code='" + Company_Code + "' " +
                   "Year_Code='" + Year_Code + "'></CarporateHead></ROOT>";
            var spname = "Carporatesell";
            var status = document.getElementById("<%= btnDelete.ClientID %>").value;
             ProcessXML(XML, status, spname);
         }
         function Validate() {
             $("#loader").show();

             var StartDate = '<%= Session["Start_Date"] %>';
            var EndDate = '<%= Session["End_Date"] %>';

            var d = $("#<%=txtdoc_date.ClientID %>").val();
            var DocDates = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

            StartDate = StartDate.slice(6, 11) + "/" + StartDate.slice(3, 5) + "/" + StartDate.slice(0, 2);
            EndDate = EndDate.slice(6, 11) + "/" + EndDate.slice(3, 5) + "/" + EndDate.slice(0, 2);

         //   if (DocDates >= StartDate && DocDates <= EndDate) {
        //    }
          //  else {
            //    $("#<%=txtdoc_date.ClientID %>").focus();
                //alert('Not A Valid Date Range')
              //  $("#loader").hide();
                //return false;
             //}

             if (DocDates == "") {
                 $("#<%=txtdoc_date.ClientID %>").focus();
                  alert('Not A Valid Date Range')
                  $("#loader").hide();
                  return false;
              }

            if ($("#<%=txtpodetail.ClientID %>").val() == "") {
                $("#<%=txtpodetail.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }

            if ($("#<%=txtquantal.ClientID %>").val() == "" || $("#<%=txtquantal.ClientID %>").val() == "0") {
                $("#<%=txtquantal.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            if ($("#<%=txtsell_rate.ClientID %>").val() == "" || $("#<%=txtsell_rate.ClientID %>").val() == "0") {
                $("#<%=txtsell_rate.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }

            if ($("#<%=txtBill_To.ClientID %>").val() == "" || $("#<%=txtBill_To.ClientID %>").val() == "0") {
                $("#<%=txtBill_To.ClientID %>").focus();
                    $("#loader").hide();
                    return false;
                }

             return true;
             if ($("#<%=txtCommision.ClientID %>").val() == "" || $("#<%=txtCommision.ClientID %>").val() == "0") {
                 $("#<%=txtCommision.ClientID %>").focus();
                  $("#loader").hide();
                  return false;
              }
         }

            function pagevalidation() {
                debugger;
                try {
                    var doc_no = 0, carpid = 0, carpdetailid = 0;
                    var status = document.getElementById("<%= btnSave.ClientID %>").value;
                    var spname = "Carporatesell";
                    var XML = "<ROOT>";
                    if (status == "Update") {
                        doc_no = document.getElementById("<%= hdnfcarpdoc.ClientID %>").value;
                        carpid = document.getElementById("<%= hdnfcarpid.ClientID %>").value;
                    }
                    var DeliveryType = $("#<%=drpDeliveryType.ClientID %>").val();
                    var d = $("#<%=txtdoc_date.ClientID %>").val();
                    var DOC_DATE = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);

                    var AC_CODE = $("#<%=txtac_code.ClientID  %>").val() == "" ? 0 : $("#<%=txtac_code.ClientID  %>").val();
                var UNIT_CODE = $("#<%=txtunit_code.ClientID  %>").val() == "" ? 0 : $("#<%=txtunit_code.ClientID  %>").val();

                    var PODETAIL = $("#<%=txtpodetail.ClientID %>").val();
                    PODETAIL = PODETAIL.toUpperCase();
                    var QUANTAL = $("#<%=txtquantal.ClientID  %>").val() == "" ? 0 : $("#<%=txtquantal.ClientID%>").val();
                var SELL_RATE = $("#<%=txtsell_rate.ClientID  %>").val() == "" ? 0 : $("#<%=txtsell_rate.ClientID%>").val();
                    var ASN_NO = $("#<%=txtasn_no.ClientID%>").val();
                    var Selling_Type = $("#<%=drpSellingType.ClientID %>").val();
                    var Broker = $("#<%=txtBroker.ClientID  %>").val() == "" ? 0 : $("#<%=txtBroker.ClientID%>").val();
                    var Commissionrate = $("#<%=txtCommision.ClientID  %>").val() == "" ? 0 : $("#<%=txtCommision.ClientID%>").val();
                    var Bill_To = $("#<%=txtBill_To.ClientID  %>").val() == "" ? 0 : $("#<%=txtBill_To.ClientID%>").val();
                    var USER = '<%= Session["user"] %>';
                    var Branch_Id = '<%= Session["Branch_Code"] %>';
                    var Year_Code = '<%= Session["year"] %>';
                    var Company_Code = '<%= Session["Company_Code"] %>';

                    var ac = document.getElementById("<%= hdnfac.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfac.ClientID %>").value;
                    if (ac == "" || ac == "&nbsp;") {
                        ac = 0;
                    }
                    var bt = document.getElementById("<%= hdnfbt.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbt.ClientID %>").value;
                if (bt == "" || bt == "&nbsp;") {
                    bt = 0;
                }
                var uc = document.getElementById("<%= hdnfuc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfuc.ClientID %>").value;
                if (uc == "" || uc == "&nbsp;") {
                    uc = 0;
                }
                var bk = document.getElementById("<%= hdnfbk.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbk.ClientID %>").value;
                if (bk == "" || bk == "&nbsp;") {
                    bk = 0;
                }


                var HeadInsertUpdate = ""; Detail_Insert = ""; Detail_Update = ""; Detail_Delete = "";
                var Detail_Value = ""; concatid = "";
                var Gledger_Insert = ""; Gledger_values = "";
                var Gledger_Delete = "";
                if (status == "Save") {
                    HeadInsertUpdate = "created_by='" + USER + "' modified_by=''";
                }
                else {
                    HeadInsertUpdate = "modified_by='" + USER + "' created_by=''";
                }

                XML = XML + "<CarporateHead doc_no='" + doc_no + "' doc_date='" + DOC_DATE + "' ac_code='" + AC_CODE + "' unit_code='" + UNIT_CODE + "' broker='" + Broker + "' " +
                        "pono='" + PODETAIL + "' quantal='" + QUANTAL + "' " +
                    "sell_rate='" + SELL_RATE + "' remark='" + ASN_NO + "' company_code='" + Company_Code + "' carpid='" + carpid + "' " + HeadInsertUpdate + " ac='" + ac + "' uc='" + uc + "' br='" + bk + "' " +
                    "selling_type='" + Selling_Type + "' bill_to='" + Bill_To + "' bt='" + bt + "' DeliveryType='" + DeliveryType + "' CommissionRate='" + Commissionrate + "'>";

                    // Detail Calculatiuon
                var ddid = carpdetailid;
                var gridViews = document.getElementById("<%=grdDetail.ClientID %>");
            if (gridViews != null) {
                var grdrows = gridViews.getElementsByTagName("tr");

                for (var i = 1; i < grdrows.length; i++) {
                    var ID = gridViews.rows[i].cells[2].innerHTML;

                    var d = gridViews.rows[i].cells[3].innerHTML;
                    var ScheduleDate = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                    var scheduleQuntal = gridViews.rows[i].cells[4].innerHTML;
                    var TransitDay = gridViews.rows[i].cells[5].innerHTML;

                    var d = gridViews.rows[i].cells[6].innerHTML;
                    var RemindDate = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                    if (gridViews.rows[i].cells[8].innerHTML == "A") {
                        XML = XML + "<CarporateDetailInsert carpid='" + carpid + "' schedule_date='" + ScheduleDate + "' scheduale_qntl='" + scheduleQuntal + "' transit_days='" + TransitDay + "' " +
                            "carpdetailid='" + ddid + "' detail_Id='" + ID + "' doc_no='" + doc_no + "'/>";
                        ddid = parseInt(ddid) + 1;
                    }
                    else if (gridViews.rows[i].cells[8].innerHTML == "U") {
                        var DetailId = gridViews.rows[i].cells[7].innerHTML;
                        XML = XML + "<CarporateDetail carpid='" + carpid + "' schedule_date='" + ScheduleDate + "' scheduale_qntl='" + scheduleQuntal + "' transit_days='" + TransitDay + "' " +
                            "carpdetailid='" + DetailId + "' detail_Id='" + ID + "' doc_no='" + doc_no + "'/>";
                    }
                    else if (gridViews.rows[i].cells[8].innerHTML == "D") {
                        var DetailId = gridViews.rows[i].cells[7].innerHTML;
                        XML = XML + "<CarporateDetailDelete carpid='" + carpid + "' schedule_date='" + ScheduleDate + "' scheduale_qntl='" + scheduleQuntal + "' transit_days='" + TransitDay + "' " +
                            "carpdetailid='" + DetailId + "' detail_Id='" + ID + "' doc_no='" + doc_no + "'/>";
                    }
                }

            }
            XML = XML + "</CarporateHead></ROOT>";
            ProcessXML(XML, status, spname);

        }
        catch (exx) {
            $("#loader").hide();
            alert(exx);
        }
        finally {
            $("#loader").hide();

        }
    }
    function ProcessXML(XML, status, spname) {
        debugger;

        $.ajax({
            type: "POST",
            url: "../xmlExecuteDMLQry.asmx/ExecuteXMLQry",
            data: '{XML:"' + XML + '",status:"' + status + '",spname:"' + spname + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {
                alert(response.d);
                $("#loader").hide();

            },
            error: function (response) {
                alert(response.d);
                $("#loader").hide();


            }
        });

        function OnSuccess(response) {
            debugger;
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
                    //window.open('../Transaction/pgeReceiptPaymentxml.aspx?tranid=' + tranid + '&Action=1', "_self");
                    window.open('../BussinessRelated/pgeCarporatesell.aspx?carpid=' + id + '&Action=1', "_self");
                }
            }
            else {
                var num = parseInt(response.d);

                if (isNaN(num)) {
                    alert(response.d)

                }
                else {
                    window.open('../BussinessRelated/PgeCarporatesaleUtility.aspx', "_self");
                }
            }

        }
    }
    </script>
    <style>
        #loader {
            width: 100%;
            height: 100%;
            background-color: gray;
            position: fixed;
            margin: -0.7%;
            opacity: 0.6;
        }
    </style>
    <%--<script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="   Carporate Sale   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px;">
        <img height="20%" width="20%" src="../Images/ajax-loader3.gif" />
    </div>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfac" runat="server" />
            <asp:HiddenField ID="hdnfbt" runat="server" />
            <asp:HiddenField ID="hdnfuc" runat="server" />
            <asp:HiddenField ID="hdnfbk" runat="server" />
            <asp:HiddenField ID="hdnfcarpdoc" runat="server" />
            <asp:HiddenField ID="hdnfcarpid" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table width="100%" align="left" cellspacing="4px" cellpadding="1px">
                    <tr>
                        <td colspan="4">
                            <table width="80%" align="left">
                                <td align="center">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                        ValidationGroup="save" OnClick="btnAdd_Click" Height="24px" />
                                    &nbsp;
                        <asp:Button ID="btnSave" runat="server" TabIndex="18" Text="Save" CssClass="btnHelp"
                            OnClientClick="if (!Validate()) return false;" Width="90px" ValidationGroup="add" OnClick="btnSave_Click" Height="24px" />
                                    &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="24px" />
                                    &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" Height="24px" />
                                    &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnCancel_Click" Height="24px" />

                                    &nbsp;
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="80px" OnClientClick="Back()"
                                            Height="25px" TabIndex="35" />
                                </td>
                                <td align="center">&nbsp;
                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnFirst_Click" Width="90px" Height="24px" Visible="false" />
                                    &nbsp;
                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnPrevious_Click" Width="90px" Height="24px" Visible="false" />
                                    &nbsp;
                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnNext_Click" Width="90px" Height="24px" Visible="false" />
                                    &nbsp;
                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="First" CssClass="btnHelp"
                            OnClick="btnLast_Click" Width="90px" Height="24px" Visible="false" />
                                </td>
                    </tr>
                </table>
                </td>
                </table>

                <table width="100%" align="left" cellspacing="5">
                    <tr>

                        <td>Change No:
                             <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                 AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged" TabIndex="1"></asp:TextBox>
                            &nbsp;<asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="txtxf" FilterType="Numbers" InvalidChars="."
                                TargetControlID="txtEditDoc_No">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>Entry No:
                       
                            <asp:TextBox ID="txtdoc_no" runat="Server" CssClass="txt" TabIndex="0" Width="90px"
                                Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtdoc_no_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtdoc_no" runat="server" Text="..." Width="80px" OnClick="btntxtdoc_no_Click"
                                CssClass="btnHelp" Visible="false" Height="24px" />
                            <asp:Label ID="lblCor_Id" runat="server" CssClass="lblName" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Date:
                       &emsp; &emsp;
                            <asp:TextBox ID="txtdoc_date" runat="Server" CssClass="txt" TabIndex="1" Width="90px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtdoc_date_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="imgcalender" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" />
                            <ajax1:CalendarExtender ID="calenderExtendertxtdoc_date" runat="server" TargetControlID="txtdoc_date"
                                PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                            &emsp; &emsp;
                            Selling Type:
                       
                            <asp:DropDownList runat="server" ID="drpSellingType" CssClass="ddl" Height="24px"
                                AutoPostBack="true" TabIndex="2" Width="200px" OnSelectedIndexChanged="drpSellingType_SelectedIndexChanged">
                                <asp:ListItem Text="Carporate Sell" Value="C"></asp:ListItem>
                                <asp:ListItem Text="PDS Sell" Value="P"></asp:ListItem>
                                <asp:ListItem Text="Party Sell" Value="PS"></asp:ListItem>
                            </asp:DropDownList>
                            &emsp; &emsp;
                            Delivery Type:
                       
                            <asp:DropDownList runat="server" ID="drpDeliveryType" CssClass="ddl" Height="24px"
                                AutoPostBack="true" TabIndex="3" Width="250px" OnSelectedIndexChanged="drpDeliveryType_SelectedIndexChanged">
                                <asp:ListItem Text="With GST Naka Delivery" Value="N"></asp:ListItem>
                                 <asp:ListItem Text="Naka Delivery without GST Rate" Value="A" ></asp:ListItem>
                                <asp:ListItem Text="Commision" Value="C"></asp:ListItem>

                            </asp:DropDownList>
                        </td>
                        <tr>
                            <td>Party:
                            &emsp; &emsp;
                                <asp:TextBox ID="txtac_code" runat="Server" CssClass="txt" TabIndex="4" Width="90px"
                                    Style="text-align: right;" OnTextChanged="txtac_code_TextChanged"
                                    Height="24px" onkeydown="ac_name(event);"></asp:TextBox>
                                <asp:Button ID="btntxtac_code" runat="server" Text="..." OnClick="btntxtac_code_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblParty_name" runat="server" CssClass="lblName"></asp:Label>
                                <asp:Label Text="" ID="lblPartyCommission" Font-Bold="true" ForeColor="Yellow" runat="server" />
                                <asp:Label Text="" ID="lblpartyId" Font-Bold="true" ForeColor="Yellow" runat="server" />
                                &emsp; &emsp; &emsp; &emsp; &emsp;
                                Bill To:
                           
                                <asp:TextBox ID="txtBill_To" runat="Server" CssClass="txt" TabIndex="5" Width="90px"
                                    onkeydown="billto(event);" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtBill_To_TextChanged"
                                    Height="24px"></asp:TextBox>
                                <asp:Button ID="btntxtbill_To" runat="server" Text="..." OnClick="btntxtbill_To_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblBill_To" runat="server" CssClass="lblName"></asp:Label>
                                &emsp; &emsp;
                                Ship To:
                          
                                <asp:TextBox ID="txtunit_code" runat="Server" CssClass="txt" TabIndex="6" Width="90px"
                                    Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtunit_code_TextChanged"
                                    Height="24px" onkeydown="Unitname(event);"></asp:TextBox>
                                <asp:Button ID="btntxtunit_code" runat="server" Text="..." OnClick="btntxtunit_code_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblUnit_name" runat="server" CssClass="lblName"></asp:Label>
                                <asp:Label ID="unitid" runat="server" CssClass="lblName"></asp:Label>
                                &emsp; &emsp;
                                Broker:
                           
                                <asp:TextBox ID="txtBroker" runat="Server" CssClass="txt" TabIndex="7" Width="90px"
                                    Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtBroker_TextChanged"
                                    onkeydown="Brokername(event);"></asp:TextBox>
                                <asp:Button ID="btntxtBroker" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                    Width="20px" OnClick="btntxtBroker_Click" />
                                <asp:Label ID="lblBroker" runat="server" CssClass="lblName"></asp:Label>
                                <asp:Label ID="lblBrokerId" runat="server" CssClass="lblName"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td>P.O.Detail:
                           
                                <asp:TextBox ID="txtpodetail" runat="Server" CssClass="txt" TabIndex="8" Width="200px"
                                    Style="text-align: left; text-transform: uppercase;" AutoPostBack="True" OnTextChanged="txtpodetail_TextChanged"
                                    Height="24px"></asp:TextBox>
                                Quantal:
                           
                                <asp:TextBox ID="txtquantal" runat="Server" CssClass="txt" TabIndex="9" Width="90px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtquantal_TextChanged"
                                    Height="24px"></asp:TextBox>
                                Sell Rate:
                                 &emsp;
                                <asp:TextBox ID="txtsell_rate" runat="Server" CssClass="txt" TabIndex="10" Width="90px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtsell_rate_TextChanged"
                                    Height="24px"></asp:TextBox>
                                 Commission:
                                 &emsp;
                                <asp:TextBox ID="txtCommision" runat="Server" CssClass="txt" TabIndex="11" Width="90px"
                                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtCommision_TextChanged"
                                    Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>

                            <td>Remark:
                           &emsp;
                                <asp:TextBox ID="txtasn_no" runat="Server" CssClass="txt" TabIndex="12" Width="200px"
                                    Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtasn_no_TextChanged"
                                    Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnOpenDetailsPopup" runat="server" Text="ADD" CssClass="btnHelp"
                                    Width="80px" Height="24px" OnClick="btnOpenDetailsPopup_Click" Visible="false" />
                            </td>
                </table>
                <fieldset style="border-top: 3px dotted rgb(131, 127, 130); border-radius: 3px; width: 172%; margin-left: -1160px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 5px;">
                    <legend style="text-align: left;">
                        <h2 style="color: Black; text-align: left;" font-names="verdana" font-size="large">Carporate Sell Detail Section</h2>
                    </legend>
                </fieldset>
                <asp:Panel ID="Panel2" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                    Font-Size="Small" Style="margin-top: 0px; z-index: 100;">

                    <table width="80%" align="left" cellpadding="3" cellspacing="5">

                        <tr>
                            <td>
                                <asp:Label ID="lblID" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 10%;">Schedule Date:
                            </td>
                            <td align="left" colspan="4">
                                <asp:TextBox ID="txtSCDate" runat="Server" CssClass="txt" TabIndex="13" Width="90px"
                                    MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode);save(event);"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtSCDate_TextChanged"></asp:TextBox>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png" Width="25px"
                                    Height="15px" />
                                <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtSCDate"
                                    PopupButtonID="Image1" Format="dd/MM/yyyy">
                                </ajax1:CalendarExtender>
                                Quantal:
                          
                           
                                <asp:TextBox ID="txtSCQuantal" runat="Server" CssClass="txt" TabIndex="14" Width="90px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtSCQuantal_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilterertxtSCQuantal" runat="server" FilterType="Numbers,Custom"
                                    ValidChars="." TargetControlID="txtSCQuantal">
                                </ajax1:FilteredTextBoxExtender>

                                Transit days:
                               
                                    <asp:TextBox ID="txtTransitDays" runat="Server" CssClass="txt" TabIndex="15" Width="90px"
                                        Height="24px" Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtTransitDays_TextChanged"></asp:TextBox>
                                <ajax1:FilteredTextBoxExtender ID="FilterertxtTransitDays" runat="server" FilterType="Numbers"
                                    TargetControlID="txtTransitDays">
                                </ajax1:FilteredTextBoxExtender>


                                Remind Date:
                              
                                    <asp:TextBox ID="txtRemindDate" runat="Server" CssClass="txt" TabIndex="16" Width="90px"
                                        MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                        Height="24px" ReadOnly="true" Style="text-align: right;" OnTextChanged="txtRemindDate_TextChanged"></asp:TextBox>

                                &nbsp;&nbsp;
                                <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                    Height="24px" OnClick="btnAdddetails_Click" TabIndex="17" />
                                <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp"
                                    Height="24px" Width="80px" OnClick="btnClosedetails_Click" TabIndex="15" />

                                &nbsp;&nbsp;&nbsp;&nbsp;
                                Diff:<asp:Label runat="server" ID="lblQntlDiff" ForeColor="Yellow"></asp:Label>
                        </tr>


                    </table>
                </asp:Panel>
            </asp:Panel>
            <div>
                <asp:UpdatePanel ID="upGrid" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlgrdDetail" runat="server" ScrollBars="Both" Height="220px" Width="1200px"
                            BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true" Font-Names="Verdana"
                            Font-Size="11px" BackColor="SeaShell" Style="margin-left: 30px; float: left;">
                            <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" HeaderStyle-BackColor="#397CBB"
                                HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="112%"
                                Height="65%" OnRowCommand="grdDetail_RowCommand" CellPadding="5" CellSpacing="5"
                                OnRowDataBound="grdDetail_RowDataBound" Style="table-layout: fixed;">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" Text="Edit"
                                                CommandArgument="lnk"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord" Text="Delete"
                                                CommandArgument="lnk"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle Height="25px" Wrap="false" ForeColor="Black" />
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="80%"
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
                                    HeaderStyle-ForeColor="White" OnRowCreated="grdPopup_RowCreated" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                    OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
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
