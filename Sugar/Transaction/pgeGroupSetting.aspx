
<%@ Page Title="GroupSetting" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pgeGroupSetting.aspx.cs" Inherits="Sugar_Transaction_pgeGroupSetting" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

        document.addEventListener('keyup', function (event) {
            if (event.defaultPrevented) {
                return;
            }

            var key = event.key || event.keyCode;

            if (key === 'Escape' || key === 'Esc' || key === 27) {
                //                doWhateverYouWantNowThatYourKeyWasHit();

                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";
                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;
                if (hdnfClosePopupValue == "txtparty_pa") {
                    document.getElementById("<%=txtparty_pa.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtsupplier_sa") {
                    document.getElementById("<%=txtsupplier_sa.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtbank_ba") {
                    document.getElementById("<%=txtbank_ba.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtcash_ca") {
                    document.getElementById("<%=txtcash_ca.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtrelatives_re") {
                    document.getElementById("<%=txtrelatives_re.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtfixedAssets_fi") {
                    document.getElementById("<%=txtfixedAssets_fi.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtinterestParty_intr") {
                    document.getElementById("<%=txtinterestParty_intr.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtincomeExpenses_inc") {
                    document.getElementById("<%=txtincomeExpenses_inc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txttrading_tr") {
                    document.getElementById("<%=txttrading_tr.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtmill_mi") {
                    document.getElementById("<%=txtmill_mi.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txttransport_trn") {
                    document.getElementById("<%=txttransport_trn.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtbroker_br") {
                    document.getElementById("<%=txtbroker_br.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtretailparty_ret") {
                    document.getElementById("<%=txtretailparty_ret.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtcashRetailParty_caret") {
                    document.getElementById("<%=txtcashRetailParty_caret.ClientID %>").focus();
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


                if (hdnfClosePopupValue == "txtparty_pa") {
                    debugger;
                    document.getElementById("<%=txtparty_pa.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblparty_pa.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtparty_pa.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtsupplier_sa") {
                    document.getElementById("<%=txtsupplier_sa.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblsupplier_sa.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtsupplier_sa.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtbank_ba") {
                    document.getElementById("<%=txtbank_ba.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblbank_ba.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtbank_ba.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtcash_ca") {
                    document.getElementById("<%=txtcash_ca.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblcash_ca.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtcash_ca.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtrelatives_re") {
                    document.getElementById("<%=txtrelatives_re.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblrelatives_re.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtrelatives_re.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtfixedAssets_fi") {
                    document.getElementById("<%=txtfixedAssets_fi.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblfixedAssets_fi.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtfixedAssets_fi.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtinterestParty_intr") {
                    document.getElementById("<%=txtinterestParty_intr.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblinterestParty_intr.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtincomeExpenses_inc.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtincomeExpenses_inc") {
                    document.getElementById("<%=txtincomeExpenses_inc.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblincomeExpenses_inc.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txttrading_tr.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txttrading_tr") {
                    document.getElementById("<%=txttrading_tr.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltrading_tr.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtmill_mi.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtmill_mi") {
                    document.getElementById("<%=txtmill_mi.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblmill_mi.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txttransport_trn.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txttransport_trn") {
                    document.getElementById("<%=txttransport_trn.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltransport_trn.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtbroker_br.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtbroker_br") {
                    document.getElementById("<%=txtbroker_br.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblbroker_br.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtretailparty_ret.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtretailparty_ret") {
                    document.getElementById("<%=txtretailparty_ret.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblretailparty_ret.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtcashRetailParty_caret.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtcashRetailParty_caret") {
                    document.getElementById("<%=txtcashRetailParty_caret.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblcashRetailParty_caret.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=hdnfpopup.ClientID %>").value = "0";
                    document.getElementById("<%=txtcashRetailParty_caret.ClientID %>").focus();
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

        function Commission(e) {

            if (e.keyCode == 112) {
                e.preventDefault();
                // $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtparty_pa.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtparty_pa.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtparty_pa.ClientID %>").val(unit);
                __doPostBack("txtparty_pa", "TextChanged");

            }
        }

        function interestac(e) {

            if (e.keyCode == 112) {
                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtsupplier_sa.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtsupplier_sa.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtsupplier_sa.ClientID %>").val(unit);
                __doPostBack("txtsupplier_sa", "TextChanged");

            }

        }

        function transportac(e) {

            if (e.keyCode == 112) {
                e.preventDefault();
                // $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtbank_ba.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtbank_ba.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtbank_ba.ClientID %>").val(unit);
                __doPostBack("txtbank_ba", "TextChanged");

            }
        }

        function postageac(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtcash_ca.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtcash_ca.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtcash_ca.ClientID %>").val(unit);
                __doPostBack("txtcash_ca", "TextChanged");

            }
        }
        function pagevalidation() {
        var Company_Code = '<%= Session["Company_Code"] %>';
          var pa = document.getElementById("<%= hdnfpa.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfpa.ClientID %>").value;
            var sa = document.getElementById("<%= hdnfsa.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfsa.ClientID %>").value;
            var ba = document.getElementById("<%= hdnfba.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfba.ClientID %>").value;
            var ca = document.getElementById("<%= hdnfca.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfca.ClientID %>").value;
            var re = document.getElementById("<%= hdnfre.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfre.ClientID %>").value;
             var fi = document.getElementById("<%= hdnffi.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnffi.ClientID %>").value;
            var intr = document.getElementById("<%= hdnfintr.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfintr.ClientID %>").value;
            var inc = document.getElementById("<%= hdnfinc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfinc.ClientID %>").value;
            var tr = document.getElementById("<%= hdnftr.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnftr.ClientID %>").value;
            var mi = document.getElementById("<%= hdnfmi.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfmi.ClientID %>").value;
             var trn = document.getElementById("<%= hdnftrn.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnftrn.ClientID %>").value;
            var br = document.getElementById("<%= hdnfbr.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfbr.ClientID %>").value;
            var ret = document.getElementById("<%= hdnfret.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfret.ClientID %>").value;
            var caret = document.getElementById("<%= hdnfcaret.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfcaret.ClientID %>").value;
            var companyCode = document.getElementById("<%= hdnfcompanyCode.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfcompanyCode.ClientID %>").value;

            if (pa == "" || pa == "&nbsp;") {
                pa = 0;
            }
            if (sa == "" || sa == "&nbsp;") {
                sa = 0;
            }
            if (ba == "" || ba == "&nbsp;") {
                ba = 0;
            }
            if (ca == "" || ca == "&nbsp;") {
                ca = 0;
            }
            if (re == "" || re == "&nbsp;") {
               re = 0;
            }
            if (fi == "" || fi == "&nbsp;") {
                fi = 0;
            }
            if (intr == "" || intr == "&nbsp;") {
                intr = 0;
            }
            if (inc == "" || inc == "&nbsp;") {
                inc = 0;
            }
            if (tr == "" || tr == "&nbsp;") {
                tr = 0;
            }
            if (mi == "" || mi == "&nbsp;") {
                mi = 0;
            }
            if (trn == "" || trn == "&nbsp;") {
                trn = 0;
            }
            if (br == "" || br == "&nbsp;") {
                br = 0;
            }
            if (ret == "" || ret == "&nbsp;") {
                ret = 0;
            }
             if (caret == "" || caret == "&nbsp;") {
                caret = 0;
            }
            if (companyCode == "" || companyCode == "&nbsp;") {
                companyCode = 0;
            }

        function selfac(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtrelatives_re.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtrelatives_re.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtrelatives_re.ClientID %>").val(unit);
                __doPostBack("txtrelatives_re", "TextChanged");

            }
        }

//        function gststatecode(e) {

//            if (e.keyCode == 112) {

//                e.preventDefault();
//                //$("#<%=pnlPopup.ClientID %>").show();
//                $("#<%=hdnfpopup.ClientID%>").val("0");
//                $("#<%=btntxtfixedAssets_fi.ClientID %>").click();

//            }
//            if (e.keyCode == 9) {
//                e.preventDefault();
//                var unit = $("#<%=txtfixedAssets_fi.ClientID %>").val();

//                unit = "0" + unit;
//                $("#<%=txtfixedAssets_fi.ClientID %>").val(unit);
//                __doPostBack("txtfixedAssets_fi", "TextChanged");

//            }
//        }

        function salecgst(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtinterestParty_intr.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtinterestParty_intr.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtinterestParty_intr.ClientID %>").val(unit);
                __doPostBack("txtinterestParty_intr", "TextChanged");

            }

        }
        function salesgst(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtincomeExpenses_inc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtincomeExpenses_inc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtincomeExpenses_inc.ClientID %>").val(unit);
                __doPostBack("txtincomeExpenses_inc", "TextChanged");

            }

        }
        function trading(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
        $("#<%=hdnfpopup.ClientID%>").val("0");
        $("#<%=btntxttrading_tr.ClientID %>").click();

    }y
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txttrading_tr.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txttrading_tr.ClientID %>").val(unit);
        __doPostBack("txttrading_tr", "TextChanged");

    }

}
function mill(e) {

    if (e.keyCode == 112) {

        e.preventDefault();
        //$("#<%=pnlPopup.ClientID %>").show();
        $("#<%=hdnfpopup.ClientID%>").val("0");
        $("#<%=btntxtmill_mi.ClientID %>").click();

    }
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txtmill_mi.ClientID %>").val();

        unit = "0" + unit;
        $("#<%=txtmill_mi.ClientID %>").val(unit);
                __doPostBack("txtmill_mi", "TextChanged");

            }

        }
        function transport(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
           $("#<%=hdnfpopup.ClientID%>").val("0");
           $("#<%=btntxttransport_trn.ClientID %>").click();

       }
       if (e.keyCode == 9) {
           e.preventDefault();
           var unit = $("#<%=txttransport_trn.ClientID %>").val();

           unit = "0" + unit;
           $("#<%=txttransport_trn.ClientID %>").val(unit);
                __doPostBack("txttransport_trn", "TextChanged");

            }

        }
        function broker(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtbroker_br.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtbroker_br.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtbroker_br.ClientID %>").val(unit);
                __doPostBack("txtbroker_br", "TextChanged");

            }

        }
        function retailparty(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtretailparty_ret.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtretailparty_ret.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtretailparty_ret.ClientID %>").val(unit);
                __doPostBack("txtretailparty_ret", "TextChanged");

            }

        }
        function cashRetailParty(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtcashRetailParty_caret.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtcashRetailParty_caret.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtcashRetailParty_caret.ClientID %>").val(unit);
                __doPostBack("txtcashRetailParty_caret", "TextChanged");

            }

</script>
    <script type="text/javascript">
        $("#btntxtparty_pa").click(function () {
            debugger;
            $("#<%=hdnfpopup.ClientID%>").val("0");
        });
    </script>
    <%-- <script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

    </script>--%>
    <script type="text/javascript">

        function Party(e) {

            if (e.keyCode == 112) {
                e.preventDefault();
                // $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtparty_pa.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtparty_pa.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtparty_pa.ClientID %>").val(unit);
                __doPostBack("txtparty_pa", "TextChanged");

            }
        }

        function supplier(e) {

            if (e.keyCode == 112) {
                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtsupplier_sa.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtsupplier_sa.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtsupplier_sa.ClientID %>").val(unit);
                __doPostBack("txtsupplier_sa", "TextChanged");

            }

        }

        function bank(e) {

            if (e.keyCode == 112) {
                e.preventDefault();
                // $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtbank_ba.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtbank_ba.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtbank_ba.ClientID %>").val(unit);
                __doPostBack("txtbank_ba", "TextChanged");

            }
        }

        function cash(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtcash_ca.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtcash_ca.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtcash_ca.ClientID %>").val(unit);
                __doPostBack("txtcash_ca", "TextChanged");

            }
        }

        function relatives(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtrelatives_re.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtrelatives_re.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtrelatives_re.ClientID %>").val(unit);
                __doPostBack("txtrelatives_re", "TextChanged");

            }
        }

        function fixedAssets(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtfixedAssets_fi.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtfixedAssets_fi.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtfixedAssets_fi.ClientID %>").val(unit);
                __doPostBack("txtfixedAssets_fi", "TextChanged");

            }
        }

        function interstParty(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtinterestParty_intr.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtinterestParty_intr.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtinterestParty_intr.ClientID %>").val(unit);
                __doPostBack("txtinterestParty_intr", "TextChanged");

            }

        }
        function incomeExpenses(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtincomeExpenses_inc.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtincomeExpenses_inc.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtincomeExpenses_inc.ClientID %>").val(unit);
                __doPostBack("txtincomeExpenses_inc", "TextChanged");

            }

        }
        function trading(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
        $("#<%=hdnfpopup.ClientID%>").val("0");
        $("#<%=btntxttrading_tr.ClientID %>").click();

    }
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txttrading_tr.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txttrading_tr.ClientID %>").val(unit);
        __doPostBack("txttrading_tr", "TextChanged");

    }

}
        function mill(e) {

    if (e.keyCode == 112) {

        e.preventDefault();
        //$("#<%=pnlPopup.ClientID %>").show();
        $("#<%=hdnfpopup.ClientID%>").val("0");
        $("#<%=btntxtmill_mi.ClientID %>").click();

    }
    if (e.keyCode == 9) {
        e.preventDefault();
        var unit = $("#<%=txtmill_mi.ClientID %>").val();

        unit = "0" + unit;
        $("#<%=txtmill_mi.ClientID %>").val(unit);
                __doPostBack("txtmill_mi", "TextChanged");

            }

        }
        function transport(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
           $("#<%=hdnfpopup.ClientID%>").val("0");
           $("#<%=btntxttransport_trn.ClientID %>").click();

       }
       if (e.keyCode == 9) {
           e.preventDefault();
           var unit = $("#<%=txttransport_trn.ClientID %>").val();

           unit = "0" + unit;
           $("#<%=txttransport_trn.ClientID %>").val(unit);
                __doPostBack("txttransport_trn", "TextChanged");

            }

        }
        function broker(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtbroker_br.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtbroker_br.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtbroker_br.ClientID %>").val(unit);
                __doPostBack("txtbroker_br", "TextChanged");

            }

        }
        function retailparty(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtretailparty_ret.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtretailparty_ret.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtretailparty_ret.ClientID %>").val(unit);
                __doPostBack("txtretailparty_ret", "TextChanged");

            }

        }
        function cashRetailParty(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                //$("#<%=pnlPopup.ClientID %>").show();
                $("#<%=hdnfpopup.ClientID%>").val("0");
                $("#<%=btntxtcashRetailParty_caret.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtcashRetailParty_caret.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtcashRetailParty_caret.ClientID %>").val(unit);
                __doPostBack("txtcashRetailParty_caret", "TextChanged");

            }

        }
       
       

    </script>
    <script type="text/javascript">
        $("#btntxtparty").click(function () {
            debugger;
            $("#<%=hdnfpopup.ClientID%>").val("0");
        });
    </script>
    <%-- <script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?'; //for IE & Firefox
             return 'Browser is being closed, is it okay?'; // for Safari and Chrome
         };

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <fieldset style="border-top: 1px dotted rgb(131, 127, 130); border-radius: 3px; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px; height: 7px;">
        <legend style="text-align: center;">
            <asp:Label ID="label1" runat="server" Text="  Group Setting   " Font-Names="verdana"
                ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdnfBranch1Code" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfpopup" runat="server" />
            <asp:HiddenField ID="hdnfpa" runat="server" />
            <asp:HiddenField ID="hdnfsa" runat="server" />
            <asp:HiddenField ID="hdnfba" runat="server" />
            <asp:HiddenField ID="hdnfca" runat="server" />
            <asp:HiddenField ID="hdnfre" runat="server" />
            <asp:HiddenField ID="hdnffi" runat="server" />
            <asp:HiddenField ID="hdnfintr" runat="server" />
            <asp:HiddenField ID="hdnfinc" runat="server" />
            <asp:HiddenField ID="hdnftr" runat="server" />
            <asp:HiddenField ID="hdnfmi" runat="server" />
            <asp:HiddenField ID="hdnftrn" runat="server" />
            <asp:HiddenField ID="hdnfbr" runat="server" />
            <asp:HiddenField ID="hdnfret" runat="server" />
            <asp:HiddenField ID="hdnfcaret" runat="server" />
            <asp:HiddenField ID="hdnfcompanyCode" runat="server" />

            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 10px; margin-top: 0px; z-index: 100;">
                <table width="100%" align="left" cellspacing="3px">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                   
                    <tr>
                        <td align="right" style="width: 20%;">Party P/A :
                        </td>
                        <td align="left" style="width: 30%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtparty_pa" runat="Server" CssClass="txt" TabIndex="1"  Width="80px"
                                    Height="24px" Style="text-align: right;" OnkeyDown="Party(event);" AutoPostBack="true" OnTextChanged="txtparty_pa_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtparty_pa" runat="server" Text="..." OnClick="btntxtparty_pa_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblparty_pa" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td align="right" style="width: 20%;">Supplier S/A :
                        </td>
                        <td align="left" style="width: 30%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtsupplier_sa" runat="Server" CssClass="txt" TabIndex="2" Width="80px"
                                    Height="24px" Style="text-align: right;" OnkeyDown="supplier(event);" AutoPostBack="true" OnTextChanged="txtsupplier_sa_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtsupplier_sa" runat="server" Text="..." OnClick="btntxtsupplier_sa_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblsupplier_sa" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">Bank B/A :
                        </td>
                        <td align="left" style="width: 30%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtbank_ba" runat="Server" CssClass="txt" TabIndex="3" Width="80px"
                                    Height="24px" Style="text-align: right;" OnkeyDown="bank(event);" AutoPostBack="true" OnTextChanged="txtbank_ba_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtbank_ba" runat="server" Text="..." OnClick="btntxtbank_ba_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblbank_ba" runat="server" CssClass="lblName"></asp:Label>
                       </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">Cash C/A:
                           </td>
                        <td align="left" style="width: 30%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtcash_ca" runat="Server" CssClass="txt" TabIndex="4" Width="80px"
                                    Height="24px" Style="text-align: right;" OnkeyDown="cash(event);" AutoPostBack="true" OnTextChanged="txtcash_ca_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtcash_ca" runat="server" Text="..." OnClick="btntxtcash_ca_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblcash_ca" runat="server" CssClass="lblName"></asp:Label>
                       </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">RelativesR/E:
                        </td>
                        <td align="left" style="width: 30%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtrelatives_re" runat="Server" CssClass="txt" TabIndex="5" Width="80px"
                                    Height="24px" Style="text-align: right;" OnkeyDown="relatives(event);" AutoPostBack="true" OnTextChanged="txtrelatives_re_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtrelatives_re" runat="server" Text="..." OnClick="btntxtrelatives_re_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblrelatives_re" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        </tr>
                    <tr>

                        <td align="right" style="width: 20%;">Fixed Assets F/I:
                        </td>
                        <td align="left" style="width: 30%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:TextBox ID="txtfixedAssets_fi" runat="Server" CssClass="txt" TabIndex="6" Width="80px"
                                 Height="24px" Style="text-align: right;" OnkeyDown="fixedAssets(event);" AutoPostBack="true" OnTextChanged="txtfixedAssets_fi_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtfixedAssets_fi" runat="server" Text="..." OnClick="btntxtfixedAssets_fi_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblfixedAssets_fi" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">Interest Party IN/TR :
                        </td>
                        <td align="left" style="width: 30%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtinterestParty_intr" runat="Server" CssClass="txt" TabIndex="7" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="interstParty(event);" AutoPostBack="true" OnTextChanged="txtinterestParty_intr_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtinterestParty_intr" runat="server" Text="..." OnClick="btntxtinterestParty_intr_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblinterestParty_intr" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                    </tr>
                    <tr>
                            <td align="right" style="width: 20%;">Income Expenses IN/C :
                            </td>                        
                            <td align="left" style="width: 30%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtincomeExpenses_inc" runat="Server" CssClass="txt" TabIndex="8" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="incomeExpenses(event);" AutoPostBack="true" OnTextChanged="txtincomeExpenses_inc_TextChanged"></asp:TextBox>
                                <asp:Button ID="btntxtincomeExpenses_inc" runat="server" Text="..." OnClick="btntxtincomeExpenses_inc_Click"
                                    Height="24px" Width="20px" CssClass="btnHelp" />
                                <asp:Label ID="lblincomeExpenses_inc" runat="server" CssClass="lblName"></asp:Label>
                            </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">Trading T/R :
                        </td>
                        <td align="left" style="width: 30%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txttrading_tr" runat="Server" CssClass="txt" TabIndex="9" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="trading(event);" AutoPostBack="true" OnTextChanged="txttrading_tr_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxttrading_tr" runat="server" Text="..." OnClick="btntxttrading_tr_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lbltrading_tr" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">Mill M/I :
                        </td>
                        <td align="left" style="width: 30%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtmill_mi" runat="Server" CssClass="txt" TabIndex="10" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="mill(event);" AutoPostBack="true" OnTextChanged="txtmill_mi_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtmill_mi" runat="server" Text="..." OnClick="btntxtmill_mi_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblmill_mi" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">Transport TR/N :
                        </td>
                        <td align="left" style="width: 30%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txttransport_trn" runat="Server" CssClass="txt" TabIndex="11" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="transport(event);" AutoPostBack="true" OnTextChanged="txttransport_trn_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxttransport_trn" runat="server" Text="..." OnClick="btntxttransport_trn_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lbltransport_trn" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td align="right" style="width: 20%;">Broker B/R :
                        </td>
                        <td align="left" style="width: 30%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtbroker_br" runat="Server" CssClass="txt" TabIndex="12" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="broker(event);" AutoPostBack="true" OnTextChanged="txtbroker_br_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtbroker_br" runat="server" Text="..." OnClick="btntxtbroker_br_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblbroker_br" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">Retail party RE/T : 
                        </td>
                        <td align="left" style="width: 30%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtretailparty_ret" runat="Server" CssClass="txt" TabIndex="13"
                                Height="24px" Width="80px" Style="text-align: right;" OnkeyDown="retailparty(event);" AutoPostBack="true" OnTextChanged="txtretailparty_ret_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtretailparty_ret" runat="server" Text="..." OnClick="btntxtretailparty_ret_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblretailparty_ret" runat="server" CssClass="lblName"></asp:Label>
                       </td>
                    </tr>
                    <tr>

                        <td align="right" style="width: 20%;">Cash Retail Party CA/RET:
                        </td>
                        <td align="left" style="width: 30%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtcashRetailParty_caret" runat="Server" CssClass="txt" TabIndex="14" Width="80px"
                                Height="24px" Style="text-align: right;" OnkeyDown="cashRetailParty(event);" AutoPostBack="true" OnTextChanged="txtcashRetailParty_caret_TextChanged"></asp:TextBox>
                            <asp:Button ID="btntxtcashRetailParty_caret" runat="server" Text="..." OnClick="btntxtcashRetailParty_caret_Click"
                                Height="24px" Width="20px" CssClass="btnHelp" />
                            <asp:Label ID="lblcashRetailParty_caret" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    

                              <tr>
                              <td></td>
                        <td>
                              <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btnHelp" Width="100px"
                               TabIndex="15" Height="30px" ValidationGroup="save" OnClick="btnUpdate_Click" Visible="true" style="margin-top: 30px;margin-left:30px"/>
                        </td>
                    </tr>
                </table>

            </asp:Panel>

            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg"
                    Width="20px" Height="20px" Style="float: right; vertical-align: top;" OnClick="imgBtnClose_Click"
                    ToolTip="Close" />
                <table width="100%;" align="center">
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
                                <asp:GridView ID="grdPopup" CssClass="select" runat="server" AutoGenerateColumns="true"
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
            <asp:Panel ID="pnlPopupDetails" runat="server" BackColor="GhostWhite" Width="955px"
                BorderColor="Teal" BorderWidth="1px" Height="300px" BorderStyle="Solid" Style="z-index: 4999; left: 20%; top: 20%; position: absolute; display: none; font-weight: bold; box-shadow: 1px 2px 10px 2px;">
                <table width="80%" align="center">
                    <tr>
                        <td colspan="4" align="center" style="background-color: lightslategrey; color: White;">
                            <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Text="Tender Details"></asp:Label>
                        </td>
                    </tr>

                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
