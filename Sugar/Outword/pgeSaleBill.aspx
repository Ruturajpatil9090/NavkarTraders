<%@ Page Title="Sell Bill New" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeSaleBill.aspx.cs" Inherits="Sugar_Outword_pgeSaleBill" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

                if (hdnfClosePopupValue == "txtAc_Code") {
                    document.getElementById("<%=txtAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblPartyName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtAc_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBroker") {
                    document.getElementById("<%=txtBroker.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBrokerName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBroker.ClientID %>").focus();
                }
           
                if (hdnfClosePopupValue == "txtDoc_No") {
                    document.getElementById("<%=txtDoc_No.ClientID %>").disabled = true;
                    document.getElementById("<%=txtDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtItem_Code") {
                    document.getElementById("<%=txtItem_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblItamName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
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
    <script type="text/javascript" src="../JS/DateValidation.js">
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
        function Confirm1() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(" Print=>Ok / preprint=>Cancel ")) {
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
                if (hdnfClosePopupValue == "txtAc_Code") {
                    document.getElementById("<%=txtAc_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBroker") {
                    document.getElementById("<%=txtBroker.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBrand_Code") {
                    document.getElementById("<%=txtBrand_Code.ClientID %>").focus();
                }
                
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtItem_Code") {
                    document.getElementById("<%=txtItem_Code.ClientID %>").focus();
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
                if (hdnfClosePopupValue == "txtAc_Code") {
                    document.getElementById("<%=txtAc_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblPartyName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtAc_Code.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtBroker") {
                    document.getElementById("<%=txtBroker.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblBrokerName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtBroker.ClientID %>").focus();
                }
               
                if (hdnfClosePopupValue == "txtDoc_No") {

                    document.getElementById("<%=txtDoc_No.ClientID %>").disabled = true;
                    document.getElementById("<%=txtDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtItem_Code") {
                    document.getElementById("<%=txtItem_Code.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblItamName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtItem_Code.ClientID %>").focus();

                }
               
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
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


    <script type="text/javascript">

        function Ac_Code(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtAc_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtAc_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtAc_Code.ClientID %>").val(unit);
                __doPostBack("txtAc_Code", "TextChanged");

            }

        }
        function Broker(e) {
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
      
       
       
        function Item(e) {
            debugger;
            if (e.keyCode == 112) {
                debugger;
                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtItem_Code.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtItem_Code.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtItem_Code.ClientID %>").val(unit);
                __doPostBack("txtItem_Code", "TextChanged");

            }
            if (e.keyCode == 13) {
                e.preventDefault();
                $("#<%=txtWt_Qty.ClientID %>").focus();
            }

        }
      
      
    </script>
    <script type="text/javascript" language="javascript">
        function DeleteConform() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= hdnfsaledoc.ClientID %>").value;
            var Autoid = document.getElementById("<%= hdnfSale_Id.ClientID %>").value;

            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';

            var XML = "<ROOT><Sale_Head Doc_No='" + DocNo + "' Sale_Id='" + Autoid + "' Company_Code='" + Company_Code + "' " +
                   "Year_Code='" + Year_Code + "'></Sale_Head></ROOT>";
            var spname = "GrainSaleBill";
            var status = document.getElementById("<%= btnDelete.ClientID %>").value;
            ProcessXML(XML, status, spname);
        }
        function Validate() {
            debugger;
            $("#loader").show();
            // Validation
            var Outword_Date = '<%= Session["Outword_Date"] %>';
            var DocDate = $("#<%=txtDoc_Date.ClientID %>").val();

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
            if (Outword_Date == "") {
                alert('Update Post Date');
                $("#loader").hide();
                return false;
            }
            Outword_Date = Outword_Date.slice(6, 11) + "/" + Outword_Date.slice(3, 5) + "/" + Outword_Date.slice(0, 2);

           // Outword_Date = Outword_Date.slice(6, 11) + Outword_Date.slice(3, 5) + "/" + Outword_Date.slice(0, 2);
            DocDate = DocDate.slice(6, 11) + "/" + DocDate.slice(3, 5) + "/" + DocDate.slice(0, 2);
         

            if ($("#<%=txtAc_Code.ClientID %>").val() == "") {
                $("#<%=txtAc_Code.ClientID %>").focus();
                $("#loader").hide();
                return false;

            }
         
         

            var count = 0;
            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            if (gridView == null) {
                alert('Please Enter Sale Details!');
                $("#loader").hide();
                return false;
            }
            var grdrow = gridView.getElementsByTagName("tr");
            var ro = (grdrow.length - 1);
            if (ro == 0) {
                alert('Please Enter Sale Details!');
                $("#loader").hide();
                return false;
            }
            if (ro >= 1) {
                for (var i = 1; i < grdrow.length; i++) {
                    var action = gridView.rows[i].cells[16].innerHTML;
                    if (gridView.rows[i].cells[16].innerHTML == "D") {
                        count = count + 1;
                    }
                }
                if (ro == count) {
                    alert('Minimum One Sale Details is compulsory!');
                    $("#loader").hide();
                    return false;
                }
            }
            return true;
        }

          function pagevalidation() {
            debugger;
            try {
                $("#loader").show();
                var Doc_No = 0, Sale_Id = 0, saledetailid = 0, GId = 0;
                var XML = "<ROOT>";
                var spname = "GrainSaleBill";
                var Tran_Type = "SB";
                var status = document.getElementById("<%= btnSave.ClientID %>").value;
                if (status == "Update") {
                    Doc_No = document.getElementById("<%= hdnfsaledoc.ClientID %>").value;
                    Sale_Id = document.getElementById("<%= hdnfSale_Id.ClientID %>").value;
                }
               
                var d = $("#<%=txtDoc_Date.ClientID %>").val();
                var Doc_Date = d.slice(6, 11) + "/" + d.slice(3, 5) + "/" + d.slice(0, 2);
                var Ac_Code = $("#<%=txtAc_Code.ClientID %>").val() == "" ? 0 : $("#<%=txtAc_Code.ClientID%>").val();
                var Broker = $("#<%=txtBroker.ClientID %>").val() == "" ? 0 : $("#<%=txtBroker.ClientID%>").val();
                var LR_No = $("#<%=txtLR_No.ClientID %>").val();
              
                var Truck_No = $("#<%=txtTruck_No.ClientID %>").val();
             

                var Taxable_Amount = $("#<%=txtTaxable_Amount.ClientID %>").val() == "" ? 0 : $("#<%=txtTaxable_Amount.ClientID%>").val();
               
                var CGST = $("#<%=txtCGST.ClientID %>").val() == "" ? 0 : $("#<%=txtCGST.ClientID%>").val();
                var CGST_Amount = $("#<%=txtCGST_Amount.ClientID %>").val() == "" ? 0 : $("#<%=txtCGST_Amount.ClientID%>").val();
                var IGST = $("#<%=txtIGST.ClientID %>").val() == "" ? 0 : $("#<%=txtIGST.ClientID%>").val();
                var IGST_Amount = $("#<%=txtIGST_Amount.ClientID %>").val() == "" ? 0 : $("#<%=txtIGST_Amount.ClientID%>").val();
                var SGST = $("#<%=txtSGST.ClientID %>").val() == "" ? 0 : $("#<%=txtSGST.ClientID%>").val();
                var SGST_Amount = $("#<%=txtSGST_Amount.ClientID %>").val() == "" ? 0 : $("#<%=txtSGST_Amount.ClientID%>").val();
                var HamaliAmount = $("#<%=txtHamaliAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtHamaliAmount.ClientID%>").val();
                var Amount = $("#<%=txtAmount.ClientID %>").val() == "" ? 0 : $("#<%=txtAmount.ClientID%>").val();
                var postage = $("#<%=txtpostage.ClientID %>").val() == "" ? 0 : $("#<%=txtpostage.ClientID%>").val();
                var Company_Code = '<%= Session["Company_Code"] %>';
                var Year_Code = '<%= Session["year"] %>';
                var Branch_Code = '<%= Session["Branch_Code"] %>';
                var USER = '<%= Session["user"] %>';

                var TCS_Par = $("#<%=txtTCS_Par.ClientID %>").val() == "" ? 0 : $("#<%=txtTCS_Par.ClientID %>").val();
                var TCS_Amount = $("#<%=txtTCS_Amount.ClientID %>").val() == "" ? 0 : $("#<%=txtTCS_Amount.ClientID %>").val();
               
                var HeadInsertUpdate = ""; Detail_Insert = ""; Detail_Update = ""; Detail_Delete = "";
                var Detail_Value = ""; concatid = "";
                var Gledger_Insert = ""; Gledger_values = ""; Gledger_Delete = "";

                debugger;
                var DOCNO = "";
                if (status == "Save") {
                    HeadInsertUpdate = "Created_By='" + USER + "' Modified_By=''";
                  
                }
                else {
                    HeadInsertUpdate = "Modified_By='" + USER + "' Created_By=''";
                    DOCNO = "Doc_No='" + Doc_No +  "'";
                }
                XML = XML + "<Sale_Head " + DOCNO + " Doc_Date='" + Doc_Date + "' Ac_Code ='" + Ac_Code + "' LR_No='" + LR_No + "'BROKER='" + BROKER + "' Truck_No='" + Truck_No + "'  " +
                    "HamaliAmount='" + HamaliAmount + "' Amount='" + Amount + "' Taxable_Amount='" + Taxable_Amount + "' Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' " +
                    "Branch_Code='" + Branch_Code + "' " +
                   "CGST_Amount='" + CGST_Amount + "' SGST_Amount='" + SGST_Amount + "' IGST_Amount='" + IGST_Amount + "'postage='" + postage + "' Sale_Id='" + Sale_Id + "'  " +
                   "IGST_Amount='" + IGST_Amount + "'postage='" + postage + "' Sale_Id='" + Sale_Id +  "'TCS_Par='" + TCS_Par + "' TCS_Amount='" + TCS_Amount + "'>"; 
                   
                  
                  
                var gridView = document.getElementById("<%=grdDetail.ClientID %>");
                var grdrow = gridView.getElementsByTagName("tr");

                var ddid = saledetailid;
                for (var i = 1; i < grdrow.length; i++) {
                    var Sale_Id = gridView.rows[i].cells[2].innerHTML;
                    var Item_Code = gridView.rows[i].cells[3].innerHTML;
                    var Brand_Code = gridView.rows[i].cells[5].innerHTML;
                    var Qty = gridView.rows[i].cells[7].innerHTML;
                    var Wt_Per = gridView.rows[i].cells[8].innerHTML;
                    var Wt_Qty = gridView.rows[i].cells[9].innerHTML;
                    var Rate = gridView.rows[i].cells[10].innerHTML;
                    var Value = gridView.rows[i].cells[11].innerHTML;
                    var GST_Code = gridView.rows[i].cells[12].innerHTML;
                    var GST_Rate = gridView.rows[i].cells[13].innerHTML;
                    var SGST = gridView.rows[i].cells[14].innerHTML;
                    var CGST = gridView.rows[i].cells[15].innerHTML;
                    var IGST = gridView.rows[i].cells[16].innerHTML;
                    var Hamali_Rate = gridView.rows[i].cells[17].innerHTML;
                    var Hamali = gridView.rows[i].cells[18].innerHTML;
                    if (gridView.rows[i].cells[19].innerHTML == "A") {

                        XML = XML + "<Sale_DetailInsert Doc_No='" + Doc_No + "' Sale_Id='" + Sale_Id + "' Item_Code='" + Item_Code + "' Qty='" + Qty + "' Wt_Per='" + Wt_Per + "' Wt_Qty='" + Wt_Qty + "' Rate='" + Rate + "' " +
                        "Value='" + Value + "' GST_Code='" + GST_Code + "' GST_Rate='" + GST_Rate + "' SGST='" + SGST + "' CGST='" + CGST + "' IGST='" + IGST + "' Hamali_Rate='" + Hamali_Rate + "' Hamali='" + Hamali + "' "+
                         "Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
                        "Created_By='" + USER + "' Modified_By='" + USER + "' SaleDetail_Id='" + Sale_Id +  "' Brand_Code='" + Brand_Code + "'/>";
                        ddid = parseInt(ddid) + 1;
                    }
                    else if (gridView.rows[i].cells[14].innerHTML == "U") {
                        var Sale_Id = gridView.rows[i].cells[13].innerHTML;
                        XML = XML + "<Sale_Detail Doc_No='" + Doc_No + "' detail_id='" + ID + "' Tran_Type='SB' Item_Code='" + Item_Code + "' Qty='" + Qty + "' Wt_Per='" + Wt_Per + "' Wt_Qty='" + Wt_Qty + "' Rate='" + Rate + "' " +
                       "Value='" + Value + "' GST_Code='" + GST_Code + "' GST_Rate='" + GST_Rate + "' SGST='" + SGST + "' CGST='" + CGST + "' IGST='" + IGST + "' Hamali_Rate='" + Hamali_Rate + "' Hamali='" + Hamali + "' "+
                       "Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
                       "Created_By='" + USER + "' Modified_By='" + USER + "' SaleDetail_Id='" + Sale_Id + "' Brand_Code='" + Brand_Code + "'/>";
                    }
                    else if (gridView.rows[i].cells[14].innerHTML == "D") {
                        var Sale_Id = gridView.rows[i].cells[13].innerHTML;
                        XML = XML + "<Sale_DetailDelete Doc_No='" + Doc_No + "' detail_id='" + ID + "' Tran_Type='SB' Item_Code='" + Item_Code + "' Qty='" + Qty + "' Wt_Per='" + Wt_Per + "' Wt_Qty='" + Wt_Qty + "' Rate='" + Rate + "' " +
                       "Value='" + Value + "' GST_Code='" + GST_Code + "' GST_Rate='" + GST_Rate + "' SGST='" + SGST + "' CGST='" + CGST + "' IGST='" + IGST + "' Hamali_Rate='" + Hamali_Rate + "' Hamali='" + Hamali + "' "+ 
                      "Company_Code='" + Company_Code + "' Year_Code='" + Year_Code + "' Branch_Code='" + Branch_Code + "' " +
                       "Created_By='" + USER + "' Modified_By='" + USER + "' SaleDetail_Id='" + Sale_Id +  "' Brand_Code='" + Brand_Code + "'/>";
                    }
                }

                // Gledger
                var Creditnara = ""; var Debitnara = "";
                var rates = parseFloat(gridView.rows[1].cells[9].innerHTML);
                var finalsaleamonut = parseFloat(rates) + parseFloat((rates * 5) / 100);

               
                var saleaccountnarration = "" + MillShort_Name + ", SB:" + AcShort_Name + ", Qntl:" + Taxable_Amount +
                       ",L:" + LR_No + "";
                var TransportNarration = "" + Taxable_Amount + "  " + cash_advance + "  " + MillShort_Name + "  " + TransportShort_Name + "  Lorry:" + LR_No + "  Party:" + AcShort_Name + "";
                var TCSNarration = 'TCS' + AcShort_Name + " " + Doc_No;
                debugger;
                // Ac Code Effect
                var Order_Code = 1;
                if (HamaliAmount > 0) {

                 

                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' Doc_No='" + Doc_No + "' Doc_Date='" + Doc_Date + "' AC_CODE='" + Ac_Code + "' " +
                                                            "UNIT_code='0' NARRATION='" + Debitnara + "' AMOUNT='" + HamaliAmount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                            "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                            "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='6' tranid='0' Sale_Id='" + Sale_Id + "'/>";

                    Order_Code = parseInt(Order_Code) + 1;
                   
                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' Doc_No='" + Doc_No + "' Doc_Date='" + Doc_Date + "' AC_CODE='" + SaleAc + "' " +
                                                           "UNIT_code='0' NARRATION='" + saleaccountnarration + "' AMOUNT='" + TAXABLEAMOUNT + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + SaleAcid + "' vc='0' progid='6' tranid='0' Sale_Id='" + Sale_Id + "'/>";
                }
                
                if (CGST_Amount > 0) {
                    Order_Code = parseInt(Order_Code) + 1;
                   

                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' Doc_No='" + Doc_No + "' Doc_Date='" + Doc_Date + "' AC_CODE='" + '<%=Session["SaleCGSTAc"] %>' + "' " +
                                                           "UNIT_code='0' NARRATION='" + Creditnara + "' AMOUNT='" + CGST_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleCGSTid"] %>' + "' vc='0' progid='6' tranid='0' Sale_Id='" + Sale_Id + "'/>";
                }
               
                if (SGST_Amount > 0) {
                    Order_Code = parseInt(Order_Code) + 1;
                    

                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' Doc_No='" + Doc_No + "' Doc_Date='" + Doc_Date + "' AC_CODE='" + '<%=Session["SaleSGSTAc"] %>' + "' " +
                                                           "UNIT_code='0' NARRATION='" + Creditnara + "' AMOUNT='" + SGST_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleSGSTid"] %>' + "' vc='0' progid='6' tranid='0' Sale_Id='" + Sale_Id + "'/>";
                }
            
                if (IGST_Amount > 0) {
                    Order_Code = parseInt(Order_Code) + 1;
                 

                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' Doc_No='" + Doc_No + "' Doc_Date='" + Doc_Date + "' AC_CODE='" + '<%=Session["SaleIGSTAc"] %>' + "' " +
                                                           "UNIT_code='0' NARRATION='" + Creditnara + "' AMOUNT='" + IGST_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleIGSTid"] %>' + "' vc='0' progid='6' tranid='0' Sale_Id='" + Sale_Id + "'/>";
                }
               
                if (cash_advance > 0) {
                    Order_Code = parseInt(Order_Code) + 1;
             

                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' Doc_No='" + Doc_No + "' Doc_Date='" + Doc_Date + "' AC_CODE='" + TRANSPORT_CODE + "' " +
                                                           "UNIT_code='0' NARRATION='" + TransportNarration + "' AMOUNT='" + cash_advance + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                           "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                           "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + tc + "' vc='0' progid='6' tranid='0' Sale_Id='" + Sale_Id + "'/>";
                }

                if (postage != 0) {
                    if (postage < 0) {
                        Order_Code = parseInt(Order_Code) + 1;
                  
                        XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' Doc_No='" + Doc_No + "' Doc_Date='" + Doc_Date + "' AC_CODE='" + '<%=Session["postage"] %>' + "' " +
                                                              "UNIT_code='0' NARRATION='" + Debitnara + "' AMOUNT='" + postage + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                              "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["postageid"] %>' + "' vc='0' progid='6' tranid='0' Sale_Id='" + Sale_Id + "'/>";
                    }
                    else {
                        Order_Code = parseInt(Order_Code) + 1;
                     

                        XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' Doc_No='" + Doc_No + "' Doc_Date='" + Doc_Date + "' AC_CODE='" + '<%=Session["postage"] %>' + "' " +
                                                              "UNIT_code='0' NARRATION='" + Creditnara + "' AMOUNT='" + postage + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                              "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                              "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["postageid"] %>' + "' vc='0' progid='6' tranid='0' Sale_Id='" + Sale_Id + "'/>";
                    }

                }
                if (TCS_Amount > 0) {
                    Order_Code = parseInt(Order_Code) + 1;
                 
                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' Doc_No='" + Doc_No + "' Doc_Date='" + Doc_Date + "' AC_CODE='" + '<%=Session["SaleTCSAc"] %>' + "' " +
                                                         "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                         "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='C' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                         "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + '<%=Session["SaleTCSAcid"] %>' + "' vc='0' progid='6' tranid='0' Sale_Id='" + Sale_Id + "'/>";

                    Order_Code = parseInt(Order_Code) + 1;
                  
                    XML = XML + "<Ledger TRAN_TYPE='SB' CASHCREDIT='' Doc_No='" + Doc_No + "' Doc_Date='" + Doc_Date + "' AC_CODE='" + Ac_Code + "' " +
                                                        "UNIT_code='0' NARRATION='" + TCSNarration + "' AMOUNT='" + TCS_Amount + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='" + Order_Code + "' DRCR='D' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='" + Branch_Code + "' " +
                                                        "SORT_TYPE='SB' SORT_NO='" + Doc_No + "' ac='" + ac + "' vc='0' progid='6' tranid='0' Sale_Id='" + Sale_Id + "'/>";
                }

              

                XML = XML + "</Sale_Head></ROOT>";
                ProcessXML(XML, status, spname);

            }
            catch (exx) {
                $("#loader").hide();
                alert(exx)
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
                    $("#loader").hide();
                    alert(response.d);
                },
                error: function (response) {
                    $("#loader").hide();
                    alert(response.d);

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
                            alert('Sucessfully Added Record !!! Doc_No=' + doc)
                        }
                        else {
                            alert('Sucessfully Updated Record !!! Doc_No=' + doc)
                        }
                        window.open('../Outword/pgeSaleBill.aspx?Sale_Id=' + id + '&Action=1', "_self");

                    }
                }
                else {
                    var num = parseInt(response.d);

                    if (isNaN(num)) {
                        alert(response.d)

                    }
                    else {
                       // window.open('../Outword/PgeSale_HeadUtility.aspx', "_self");
                    }
                }

            }
        }
    </script>
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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%;
        margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
        border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label4" runat="server" Text="   Sale Bill For GST   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px;
        float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px;
        border-right: 0px;">
        <img height="40%" width="40%" src="../Images/ajax-loader3.gif" />
    </div>

     <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfAcShort" runat="server" />
            <asp:HiddenField ID="hdnfsaledoc" runat="server" />
            <asp:HiddenField ID="hdnfSale_Id" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
          
            <asp:HiddenField ID="hdnfTCSRate" runat="server" />
     <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
 <table width="90%" align="left">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="save" OnClick="btnAdd_Click" />
                            &nbsp;
                            <asp:Button ID="btnSave" OnClientClick="if (!Validate()) return false;" runat="server"
                                Text="Save" CssClass="btnHelp" Width="90px" Height="24px" ValidationGroup="add"
                                OnClick="btnSave_Click" TabIndex="52" />
                            &nbsp;
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="save" OnClick="btnEdit_Click" />
                            &nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="Confirm()" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                                Height="24px" ValidationGroup="save" OnClick="btnCancel_Click" />&nbsp; &nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                                ValidationGroup="save" Height="24px" OnClientClick="Back();" />
                         <%--   <asp:Button runat="server" ID="btnPrintSaleBill" Text="Print" CssClass="btnHelp"
                                OnClick="btnPrintSaleBill_Click" Width="80px" Height="24px" OnClientClick="Confirm1()" />
                            &nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnGenEinvoice" Text="Generate EInvoice" CssClass="btnHelp"
                                Width="120px" Height="24px" OnClientClick="EInovice();" />
                            <asp:Button runat="server" ID="btnGentare_EWayBill" Text="Gentare EWayBill" CssClass="btnHelp"
                                Width="90px" Height="24px" OnClientClick="GEway();" />--%>
                           <%-- &nbsp;
                            <asp:Button runat="server" ID="btnCancleEinvoice" Text="Cancle EInvoice" CssClass="btnHelp"
                                Width="120px" Height="24px" OnClientClick="ConfirmCancle();" OnClick="btnCancleEInvoice_Click" />
                        </td>--%>
                         <td align="center">
                                        <asp:Button ID="btnFirst" runat="server" Text="<<" ToolTip="First" CssClass="btnHelp"
                                            OnClick="btnFirst_Click" Width="90px" Height="24px" />
                                        <asp:Button ID="btnPrevious" runat="server" Text="<" ToolTip="Previous" CssClass="btnHelp"
                                            OnClick="btnPrevious_Click" Width="90px" Height="24px" />
                                        <asp:Button ID="btnNext" runat="server" Text=">" ToolTip="Next" CssClass="btnHelp"
                                            OnClick="btnNext_Click" Width="90px" Height="24px" />
                                        <asp:Button ID="btnLast" runat="server" Text=">>" ToolTip="Last" CssClass="btnHelp"
                                            OnClick="btnLast_Click" Width="90px" Height="24px" />
                                    </td>
                    </tr>
                </table>
                <table width="90%" align="left" cellspacing="3">
                    <tr>
                       <td align="right">Cash / Credit:
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="drpCashCredit" Width="100px" CssClass="ddl"
                                AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="drpCashCredit_SelectedIndexChanged">
                                <asp:ListItem Text="Cash" Value="CS" />
                                <asp:ListItem Text="Credit" Value="CR" />
                            </asp:DropDownList>
                             </td>
                             </tr>
                             <tr>
                       <td align="right">
                            Change No:
                             </td>
                         <td align="left">
                            <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" FilterType="Numbers" InvalidChars="."
                                TargetControlID="txtEditDoc_No">
                            </ajax1:FilteredTextBoxExtender>
                       
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td align="right">
                            Bill No :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDoc_No" runat="Server" CssClass="txt" TabIndex="2" Width="80px"
                                Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtDoc_No_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Button ID="btntxtDoc_No" runat="server" Text="..." Width="80px" OnClick="btntxtDoc_No_Click"
                                CssClass="btnHelp" Height="24px" />
                            <asp:Label ID="lblSale_Id" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red" Visible="false"></asp:Label>
                          
                            &nbsp;
                         
                            Date:
                            <asp:TextBox ID="txtDoc_Date" runat="Server" CssClass="txt" TabIndex="3" Width="100px"
                                MaxLength="10" onkeyup="ValidateDate(this,event.keyCode)" onkeydown="return DateFormat(this,event.keyCode)"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtDoc_Date_TextChanged"
                                Height="24px"></asp:TextBox>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar_icon1.png"
                                Width="25px" Height="15px" />
                            <ajax1:CalendarExtender ID="calenderExtenderDate" runat="server" TargetControlID="txtDoc_Date"
                                PopupButtonID="imgcalender" Format="dd/MM/yyyy">
                            </ajax1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                             Party Code:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtAc_Code" runat="Server" CssClass="txt" TabIndex="4" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtAc_Code_TextChanged"
                                onKeyDown="Ac_Code(event);"></asp:TextBox>
                            <asp:Button ID="btntxtAc_Code" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                Width="20px" OnClick="btntxtAc_Code_Click" />
                            <asp:Label ID="lblPartyName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Broker:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtBroker" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                Style="text-align: right;" AutoPostBack="false" Height="24px" OnTextChanged="txtBroker_TextChanged"
                                onKeyDown="Broker(event);"></asp:TextBox>
                            <asp:Button ID="btntxtBroker" runat="server" Text="..." CssClass="btnHelp" Height="24px"
                                Width="20px" OnClick="btntxtBroker_Click" />
                            <asp:Label ID="lblBrokerName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                   
                    <tr>
                        <td align="right">
                              Lorry_NO:
                       </td>
                         <td align="left">
                        <asp:TextBox ID="txtLR_No" runat="Server" CssClass="txt" TabIndex="6" Width="200px"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtLR_No_TextChanged"
                                Height="24px"></asp:TextBox>
                       
                            Truck_No:
                        <asp:TextBox ID="txtTruck_No" runat="Server" CssClass="txt" TabIndex="7" Width="200px"
                                Style="text-align: left;" AutoPostBack="True" OnTextChanged="txtTruck_No_TextChanged"
                                Height="24px"></asp:TextBox>
                          
                        </td>
                    </tr>
                 </table>
              
                  <fieldset style="border-top: 3px dotted rgb(131, 127, 130); border-radius: 3px; width: 172%;
                    margin-left: -1160px; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px;
                    border-left: 0px; border-right: 0px; height: 5px;">
                    <legend style="text-align: left;">
                        <h2 style="color: Black; text-align: left;" font-names="verdana" font-size="large">
                            Detail Section</h2>
                    </legend>
                </fieldset>
                <asp:Panel ID="Panel2" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                    Font-Size="Small" Style="margin-top: 0px; z-index: 100;">
                    <table width="100%" align="center" cellspacing="5">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblID" runat="server"></asp:Label>
                                <asp:Label ID="lblNo" runat="server" ForeColor="Azure"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Item:
                                <asp:TextBox ID="txtItem_Code" runat="Server" CssClass="txt" TabIndex="8" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtItem_Code_TextChanged"
                                    onKeyDown="Item(event);"></asp:TextBox>
                                <asp:Button ID="btntxtItem_Code" runat="server" Text="..." OnClick="btntxtItem_Code_Click"
                                    CssClass="btnHelp" Height="24px" Width="20px" />
                                <asp:Label ID="lblItamName" runat="server" CssClass="lblName"></asp:Label>
                                Brand_Code
                                <asp:TextBox ID="txtBrand_Code" runat="Server" AutoPostBack="false" CssClass="txt"
                                    Height="24px" OnTextChanged="txtBrand_Code_TextChanged" Style="text-align: left;"
                                    TabIndex="9" Width="90px" onkeydown="Brand_Code(event);"></asp:TextBox>
                                <asp:Button ID="btntxtBrand_Code" runat="server" CssClass="btnHelp" OnClick="btntxtBrand_Code_Click"
                                    Text="..." />
                                <asp:Label ID="lblBrandname" runat="server" CssClass="lblName"></asp:Label>
                                Qty:
                                <asp:TextBox ID="txtQty" runat="Server" CssClass="txt" TabIndex="10" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                               
                                Wet par:
                                <asp:TextBox ID="txtWt_Per" runat="Server" CssClass="txt" TabIndex="11" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtWt_Per_TextChanged"></asp:TextBox>
                               
                               Net Qty:
                                <asp:TextBox ID="txtWt_Qty" runat="Server" ReadOnly="true" CssClass="txt" TabIndex="12"
                                    Height="24px" Width="80px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtWt_Qty_TextChanged"></asp:TextBox>
                                Rate:
                                <asp:TextBox ID="txtRate" runat="Server" CssClass="txt" TabIndex="13" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                             
                                Value:
                                <asp:TextBox ID="txtValue" runat="Server" CssClass="txt" TabIndex="14" Width="80px"
                                    Height="24px" Style="text-align: right;" AutoPostBack="True" ReadOnly="true"
                                    OnTextChanged="txtValue_TextChanged"></asp:TextBox>
                                     &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;   &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;   &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;
                         &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;
                          &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;   &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;   &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;
                         &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp; &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;   &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;   &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;
                         &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp; &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;   &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;   &nbsp;&nbsp;  &nbsp;&nbsp;  &nbsp;&nbsp;
                        

                                GST Rate Code:
                                <asp:TextBox ID="txtGST_Code" runat="Server" CssClass="txt" TabIndex="15" Width="80px"
                                    Height="24px" Style="text-align: left;" AutoPostBack="True"
                                    OnTextChanged="txtGST_Code_TextChanged"></asp:TextBox>

                                      GST Rate :
                                <asp:TextBox ID="txtGST_Rate" runat="Server" CssClass="txt" TabIndex="16" Width="80px"
                                    Height="24px" Style="text-align: left;" AutoPostBack="True"
                                    OnTextChanged="txtGST_Rate_TextChanged"></asp:TextBox>

                              SGST Amount:
                        <asp:TextBox ID="txtSGST" runat="Server" CssClass="txt" Width="80px" TabIndex="17"
                       AutoPostBack="true" OnTextChanged="txtSGST_TextChanged" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>

                           CGST Amount:
                              <asp:TextBox ID="txtCGST" runat="Server" CssClass="txt" Width="80px" TabIndex="18"
                            Style="text-align: right;" Height="24px" AutoPostBack="true" OnTextChanged="txtCGST_TextChanged"></asp:TextBox>
                                       
                           IGST Amount:
                         <asp:TextBox ID="txtIGST" runat="Server" CssClass="txt" Width="80px" AutoPostBack="true"
                          OnTextChanged="txtIGST_TextChanged" TabIndex="19" Style="text-align: right;"
                              Height="24px"></asp:TextBox>
                                       
                                      &nbsp;&nbsp;
                                      Hamali Rate :
                                <asp:TextBox ID="txtHamali_Rate" runat="Server" CssClass="txt" TabIndex="20" Width="80px"
                                    Height="24px" Style="text-align: left;" AutoPostBack="True"
                                    OnTextChanged="txtHamali_Rate_TextChanged"></asp:TextBox>

                                      &nbsp;&nbsp;
                                      Hamali:
                                <asp:TextBox ID="txtHamali" runat="Server" CssClass="txt" TabIndex="21" Width="80px"
                                     Height="24px" Style="text-align: left;" AutoPostBack="True"
                                    OnTextChanged="txtHamali_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                          <tr>
                            <td colspan="2" align="left">
                                <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnHelp" Width="80px"
                                    Height="25px" OnClick="btnAdddetails_Click" TabIndex="22" />
                                <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnHelp" Width="80px"
                                    Height="25px" OnClick="btnClosedetails_Click" TabIndex="23" />
                            </td>
                        </tr>
                    </table>
               </asp:Panel>
                  <table>
                   <tr>
                        <td align="left" style="width: 60%;">
                            <div style="width: 100%; position: relative; margin-top: 0px;">
                                <asp:UpdatePanel ID="upGrid" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlgrdDetail" runat="server" align="left" ScrollBars="Both" Height="200px"
                                            Width="1300px" BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true"
                                            Font-Names="Verdana" Font-Size="11px" BackColor="SeaShell" Style="margin-left: 30px;
                                            float: left;">
                                            <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" HeaderStyle-BackColor="#397CBB"
                                                HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="100%"
                                                OnRowCommand="grdDetail_RowCommand" CellPadding="5" CellSpacing="5" OnRowDataBound="grdDetail_RowDataBound"
                                                Style="table-layout: fixed; float: left">
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
                              <td style="width: 80%;" align="left">
                            <table width="130%" cellspacing="4" cellpadding="3">
                                <tr>
                                    <td align="right" style="width: 35%;">
                                        Taxable Amount:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtTaxable_Amount" runat="Server" CssClass="txt" TabIndex="24" ReadOnly="false"
                                            Width="140px" Style="text-align: right;" AutoPostBack="True" Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtTaxable_Amount" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTaxable_Amount">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                   <td align="right">
                                        CGST Amount :
                                    </td>
                                    <td style="">
                                        
                                        <asp:TextBox ID="txtCGST_Amount" runat="Server" CssClass="txt" Width="80px" TabIndex="25" ReadOnly="true"
                                            Style="text-align: right;" Height="24px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                      <td align="right">
                                        SGST Amount :
                                    </td>
                                    <td style="">
                                       
                                        <asp:TextBox ID="txtSGST_Amount" runat="Server" CssClass="txt" Width="82px" TabIndex="26" ReadOnly="true"
                                            Style="text-align: right;" Height="24px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                      <td align="right">
                                        IGST Amount :
                                    </td>
                                    <td style="">
                                        
                                        <asp:TextBox ID="txtIGST_Amount" runat="Server" CssClass="txt" Width="80px" TabIndex="27" ReadOnly="true"
                                            Style="text-align: right;" Height="24px"></asp:TextBox>
                                    </td>
                                </tr>
                                 </div>
                                <tr>
                                      <td align="right">
                                        Hamali Amount :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtHamaliAmount" runat="Server" AutoPostBack="True" CssClass="txt"
                                            Height="24px" OnTextChanged="txtHamaliAmount_TextChanged" Style="text-align: right;"
                                            TabIndex="28" Width="140px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                   Postage :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtpostage" runat="Server" CssClass="txt" TabIndex="29" Width="140px"
                                            Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtpostage_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="Filteredtxtpostage" runat="server" FilterType="Numbers,Custom"
                                            ValidChars=".,-" TargetControlID="txtpostage">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Amount :
                                    </td>
                                      <td align="left">
                                        <asp:TextBox ID="txtAmount" runat="Server" CssClass="txt" ReadOnly="true" TabIndex="30"
                                            Width="140px" Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtAmount_TextChanged"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtAmount" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtAmount">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        TCS% :
                                    </td>
                                    <td style="">
                                        <asp:TextBox ID="txtTCS_Par" runat="Server" CssClass="txt" Width="42px" AutoPostBack="true"
                                            OnTextChanged="txtTCS_Par_TextChanged" TabIndex="31" Style="text-align: right;"
                                            Height="24px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtTCS_Par" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtTCS_Par">
                                        </ajax1:FilteredTextBoxExtender>
                                        <asp:TextBox ID="txtTCS_Amount" runat="Server" AutoPostBack="True" CssClass="txt" Height="24px"
                                            TabIndex="51" OnTextChanged="txtTCS_Amount_TextChanged" ReadOnly="true" Style="text-align: right;"
                                            Width="98px"></asp:TextBox>
                                        <ajax1:FilteredTextBoxExtender ID="FilteredtxtTCS_Amount" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtTCS_Amount" ValidChars=".">
                                        </ajax1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                
                                        
                                     
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px;
                min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center;
                left: 10%; top: 10%;">
                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg"
                    Width="20px" Height="20px" Style="float: right; vertical-align: top;" OnClick="imgBtnClose_Click"
                    ToolTip="Close" />
                <table width="70%">
                    <tr>
                        <td align="center" style="background-color: #F5B540; width: 100%;">
                            <asp:Label ID="lblPopupHead" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Font-Bold="true" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Search Text:
                            <asp:TextBox ID="txtSearchText" onkeydown="SelectFirstRow(event);" runat="server"
                                Width="250px" Height="20px" AutoPostBack="true" OnTextChanged="txtSearchText_TextChanged"></asp:TextBox>
                            <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="1250px" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" Font-Bold="true" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    AllowPaging="true" PageSize="20" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
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

