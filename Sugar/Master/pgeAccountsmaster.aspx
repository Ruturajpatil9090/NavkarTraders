<%@ Page Title="Account Master" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="pgeAccountsmaster.aspx.cs" Inherits="pgeAccountsmaster"
    ValidateRequest="false" %>

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
    <script type="text/javascript">
        function EnableDisable(val) {
            if (val == "T") {
                document.getElementById('<%=txtCOMMISSION.ClientID %>').disabled = true;
                document.getElementById('<%=txtInsurance.ClientID %>').disabled = true;
                document.getElementById('<%=txtADDRESS_E.ClientID %>').disabled = true;

                //document.getElementById('<%=txtOPENING_BALANCE.ClientID %>').disabled = true;
                document.getElementById('<%=drpDrCr.ClientID %>').disabled = true;
                document.getElementById('<%=txtLOCAL_LIC_NO.ClientID %>').disabled = true;

                document.getElementById('<%=txtBANK_OPENING.ClientID %>').disabled = true;
                document.getElementById('<%=drpBankDrCr.ClientID %>').disabled = true;
            }
            if (val == "O") {
                document.getElementById('<%=txtBANK_NAME.ClientID %>').disabled = true;
            }
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
                if (hdnfClosePopupValue == "txtCITY_CODE") {
                    document.getElementById("<%=txtCITY_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGstStateCode") {
                    document.getElementById("<%=txtGstStateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGROUP_CODE") {
                    document.getElementById("<%=txtGROUP_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSendingAcCode") {
                    document.getElementById("<%=txtSendingAcCode.ClientID %>").focus();
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
        function AddCity() {
            var city_code = 0;
            var Action = 2;
            window.open('../Master/pgecityMaster.aspx?city_code=' + city_code + '&Action=' + Action);
        }
    </script>
    <script type="text/javascript">
        function SB(billno) {

            var AC_CODE = document.getElementById('<%=txtAC_CODE.ClientID %>').value;
            window.open('../Sugar/rptMysqlDemo.aspx?AC_CODE=' + AC_CODE)
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtAC_NAME_E").focus(function () {
                $("#txtAC_NAME_E").animate({ width: "400px" });
            });
            $("#txtAC_NAME_E").blur(function () {
                $("#txtAC_NAME_E").css("width", "50px");
            });

        });
    </script>
    <%--arrow keydown script--%>
    <script src="../../JS/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="../../Scripts/selectfirstrow.js" type="text/javascript"></script>
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

                var hdnfClosePopupValue = document.getElementById("<%= hdnfClosePopup.ClientID %>").value;

                document.getElementById("<%=pnlPopup.ClientID %>").style.display = "none";

                document.getElementById("<%=txtSearchText.ClientID %>").value = "";

                var grid = document.getElementById("<%= grdPopup.ClientID %>");

                document.getElementById("<%= hdnfClosePopup.ClientID %>").value = "Close";
                var pageCount = document.getElementById("<%= hdHelpPageCount.ClientID %>").value;


                pageCount = parseInt(pageCount);
                if (pageCount > 1) {
                    SelectedRowIndex = SelectedRowIndex + 1;
                }

                if (hdnfClosePopupValue == "txtAC_CODE") {
                    document.getElementById("<%=txtAC_CODE.ClientID %>").disabled = false;
                    document.getElementById("<%=txtAC_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtAC_CODE.ClientID %>").focus();
                }
                
                if (hdnfClosePopupValue == "txtEditDoc_No") {
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").disabled = false;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtEditDoc_No.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtSendingAcCode") {
                    document.getElementById("<%=txtSendingAcCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=txtSendingAcCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtCITY_CODE") {
                    document.getElementById("<%=txtCITY_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblCITYNAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtCITY_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGROUP_CODE") {

                    document.getElementById("<%=txtGROUP_CODE.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblGROUPNAME.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGROUP_CODE.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtGstStateCode") {
                    document.getElementById("<%=txtGstStateCode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lbltxtGstStateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtGstStateCode.ClientID %>").focus();
                }
                if (hdnfClosePopupValue == "txtstatecode") {

                    document.getElementById("<%=txtstatecode.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
                    document.getElementById("<%=lblGstStateName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
                    document.getElementById("<%=txtstatecode.ClientID %>").focus();
                    document.getElementById("<%= modalCity.ClientID %>").style.display = "block";
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
    <style type="text/css">
        .bagroundPopup
        {
            opacity: 0.6;
            background-color: Black;
        }

        #clientsDropDown
        {
            position: absolute;
            bottom: 0;
            width: 400px;
            background-color: Black;
            padding-bottom: 2%;
            z-index: 100;
        }

        #clientsOpen
        {
            background: url("images/open.png") no-repeat scroll 68px 10px #414142;
            color: #ececec;
            cursor: pointer;
            float: right;
            font-size: 26px;
            margin: -2px 0 0 10%;
            padding: 0 15px 2px;
            text-decoration: none;
            width: 63px;
        }

        #clientsCTA
        {
            background: #414142;
            width: 100%;
            color: #CCCCCC;
            text-align: center;
            font-size: 46px;
            margin: 0;
            padding: 30px 0;
            text-decoration: none;
        }

        #clientsDropDown .clientsClose
        {
            background-image: url(images/close.png);
        }

        #clientsDropDown #clientsDashboard
        {
            display: block;
        }
    </style>
    <script type="text/javascript">




        function validate() {
            var cityname = document.getElementById('<%=txtCityName.ClientID %>').value.trim();
            if (cityname == "") {
                alert('City Name Is Required!');
                document.getElementById('<%=txtCityName.ClientID %>').focus();
                $find("mpe").show();
            }
        }


        function state(e) {


            if (e.keyCode == 9) {

                document.getElementById("<%= modalCity.ClientID %>").style.display = "block";
            }
        }

    </script>
    <script type="text/javascript">
        function valid() {
            var Group = document.getElementById('<%=txtGroupName.ClientID %>').value.trim();
            if (Group == "") {
                alert('Group Name Is Required!');
                document.getElementById('<%=txtGroupName.ClientID %>').focus();
                $find("mpes").show();
            }
        }</script>
    <script type="text/javascript">
        function validationAll() {
            var drp = document.getElementById('<%=drpType.ClientID %>');
            var val = drp.options[drp.selectedIndex].value;
            if (val == "BR") {
                var shortname = document.getElementById('<%=txtSHORT_NAME.ClientID %>').value;
                if (shortname == "") {
                    alert('Short Name is Compulsory');
                    document.getElementById('<%=txtSHORT_NAME.ClientID %>').focus();
                    return false;
                }
                else {
                    return true;
                }
            }
        }
    </script>
    <script type="text/javascript">
        function EnableDisable(val) {
            if (val == "") {
                document.getElementById('<%=txtCOMMISSION.ClientID %>').disabled = false;
                document.getElementById('<%=txtInsurance.ClientID %>').disabled = false;
                document.getElementById('<%=txtADDRESS_E.ClientID %>').disabled = false;

                //document.getElementById('<%=txtOPENING_BALANCE.ClientID %>').disabled = true;
                document.getElementById('<%=drpDrCr.ClientID %>').disabled = false;
                document.getElementById('<%=txtLOCAL_LIC_NO.ClientID %>').disabled = false;

                document.getElementById('<%=txtGST_NO.ClientID %>').disabled = false;

                document.getElementById('<%=txtBANK_OPENING.ClientID %>').disabled = false;
                document.getElementById('<%=drpBankDrCr.ClientID %>').disabled = false;
            }
            if (val == "O") {
                document.getElementById('<%=txtBANK_NAME.ClientID %>').disabled = true;
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function citycode(e) {
            debugger;
            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtCITY_CODE.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtCITY_CODE.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtCITY_CODE.ClientID %>").val(unit);
                __doPostBack("txtCITY_CODE", "TextChanged");

            }

        }
        function StateCode(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtCITY_CODE.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtGstStateCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtGstStateCode.ClientID %>").val(unit);
                __doPostBack("txtGstStateCode", "TextChanged");

            }

        }
        function GroupCode(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtGROUP_CODE.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtGROUP_CODE.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtGROUP_CODE.ClientID %>").val(unit);
                __doPostBack("txtGROUP_CODE", "TextChanged");

            }

        }
        function sendingaccode(e) {

            if (e.keyCode == 112) {

                e.preventDefault();
                $("#<%=pnlPopup.ClientID %>").show();
                $("#<%=btntxtSendingAcCode.ClientID %>").click();

            }
            if (e.keyCode == 9) {
                e.preventDefault();
                var unit = $("#<%=txtSendingAcCode.ClientID %>").val();

                unit = "0" + unit;
                $("#<%=txtSendingAcCode.ClientID %>").val(unit);
                __doPostBack("txtSendingAcCode", "TextChanged");

            }
        }

          
       
    </script>
    <%--  <script language="JavaScript" type="text/javascript">

         window.onbeforeunload = function (e) {
             var e = e || window.event;
             if (e) e.returnValue = 'Browser is being closed, is it okay?';//for IE & Firefox
             return 'Browser is being closed, is it okay?';// for Safari and Chrome
         };

    </script>--%>
    <script type="text/javascript">
        function groupmaster() {
            var Action = 2;
            var Group_Code = 0;
            //alert(td);
            window.open('../Master/pgeAccountUtility.aspx');
        }
    </script>
    <script type="text/javascript">
        function BACK() {

            //alert(td);
            window.open('../Master/pgeAccountUtility.aspx', '_self');
        }
    </script>
    <script type="text/javascript">
        function Validate() {
            debugger;
            $("#loader").show();
            // Validation
            debugger;

            var txtAC_CODE = $("#<%=txtAC_CODE.ClientID %>").val();
            if ((txtAC_CODE == "") || txtAC_CODE == "0") {
                $("#<%=txtAC_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;

            }
            var txtAC_NAME_E = $("#<%=txtAC_NAME_E.ClientID %>").val();
            if ((txtAC_NAME_E == "") || txtAC_NAME_E == "0") {
                $("#<%=txtAC_NAME_E.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }

            var txtGROUP_CODE = $("#<%=txtGROUP_CODE.ClientID %>").val();
            if ((txtGROUP_CODE == "") || txtGROUP_CODE == "0") {
                $("#<%=txtGROUP_CODE.ClientID %>").focus();
                $("#loader").hide();
                return false;
            }
            var chke = $("#<%=txtGstStateCode.ClientID %>").is(":disabled");
            if (chke != true) {
                //textbox is disabled

                if ($("#<%=txtGstStateCode.ClientID %>").val() == "" || $("#<%=txtGstStateCode.ClientID %>").val() == "0") {
                    $("#<%=txtGstStateCode.ClientID %>").focus();
                    $("#loader").hide();
                    return false;
                }
            }
            var chke = $("#<%=txtCITY_CODE.ClientID %>").is(":disabled");
            if (chke != true) {
                if ($("#<%=txtCITY_CODE.ClientID %>").val() == "" || $("#<%=txtCITY_CODE.ClientID %>").val() == "0") {
                    $("#<%=txtCITY_CODE.ClientID %>").focus();
                    $("#loader").hide();
                    return false;
                }
            }

            var chke = $("#<%=txtDistance.ClientID %>").is(":disabled");
            //if (chke != true) {
            //  if ($("#<%=txtDistance.ClientID %>").val() == "" || $("#<%=txtDistance.ClientID %>").val() <= "0") {
            //    $("#<%=txtDistance.ClientID %>").focus();
            //  $("#loader").hide();
            //return false;
            //}
            //}

            var chke = $("#<%=txtPINCODE.ClientID %>").is(":disabled");
            if (chke != true) {
                if ($("#<%=txtPINCODE.ClientID %>").val() == "" || $("#<%=txtPINCODE.ClientID %>").val() == "0") {
                    $("#<%=txtPINCODE.ClientID %>").focus();
                    $("#loader").hide();
                    return false;
                }
            }
            var drpTypenew = $("#<%=drpType.ClientID %>").val();
            if (drpTypenew == 'BR') {
                $("#<%=btnSave.ClientID %>").focus();
            }
            else {
                var chke = $("#<%=txtcompanyPan.ClientID %>").is(":disabled");
                if (chke != true) {
                    var UnregisterGSTChk = $("#<%=chkUnregisterGST.ClientID %>").is(":checked");
                    if (UnregisterGSTChk == false) {
                        if ($("#<%=txtcompanyPan.ClientID %>").val() == "" || $("#<%=txtcompanyPan.ClientID %>").val() == "0") {
                            $("#<%=txtcompanyPan.ClientID %>").focus();
                            $("#loader").hide();
                            return false;
                        }
                    }
                }

            }

            var chke = $("#<%=txtSHORT_NAME.ClientID %>").is(":disabled");
            if (chke != true) {
                if ($("#<%=txtSHORT_NAME.ClientID %>").val() == "") {
                    var Name = $("#<%=txtAC_NAME_E.ClientID %>").val();
                    var len = Name.length;
                    if (len > 15) {
                        Name = Name.slice(0, 15);
                    }
                    Name = Name.replace(" ", "");
                    document.getElementById("<%=txtSHORT_NAME.ClientID %>").value = Name;

                }
                else {
                    var Name = $("#<%=txtSHORT_NAME.ClientID %>").val();
                    Name = Name.replace(" ", "");
                    document.getElementById("<%=txtSHORT_NAME.ClientID %>").value = Name;
                }
            }

            var chke = $("#<%=txtGST_NO.ClientID %>").is(":disabled");
            if (chke != true) {
                if ($("#<%=txtGST_NO.ClientID %>").val() == "") {
                    var UnregisterGSTChk = $("#<%=chkUnregisterGST.ClientID %>").is(":checked");
                    if (UnregisterGSTChk == false) {

                        $("#<%=txtGST_NO.ClientID %>").focus();
                        $("#loader").hide();
                        return false;
                    }
                }

                var gst = $("#<%=txtGST_NO.ClientID %>").val();
                var UnregisterGSTChk = $("#<%=chkUnregisterGST.ClientID %>").is(":checked");
                if (gst.length == 15) {
                }
                else {

                    if (UnregisterGSTChk == false) {
                        $("#<%=txtGST_NO.ClientID %>").focus();
                        $("#loader").hide();
                        return false;
                    }
                }

                var address1 = $("#<%=txtADDRESS_E.ClientID %>").val();
                if (address1.length >= 10) {
                }
                else {
                    $("#<%=txtADDRESS_E.ClientID %>").focus();
                    $("#loader").hide();
                    return false;
                }

                debugger;
                if (UnregisterGSTChk == false) {
                    var gstno = $("#<%=txtGST_NO.ClientID %>").val();

                    var mystr = parseInt(gstno.substring(0, 2));
                    var statecode = $("#<%=txtGstStateCode.ClientID %>").val();
                    state = parseInt(statecode);
                    //statecode = statecode.replace('0', '');
                    if (statecode != mystr) {
                        alert('please insert valid state code!');
                        $("#loader").hide();
                        return false;
                    }
                    else {

                    }
                }
                // if (UnregisterGSTChk == false) {
                //   var gstno = $("#<%=txtGST_NO.ClientID %>").val();

                //   var mystr = gstno.substring(0, 2);
                //  var statecode = $("#<%=txtGstStateCode.ClientID %>").val();
                //  statecode = statecode.replace('0', '');
                //  if (statecode != mystr) {
                //      alert('please insert valid state code!');
                //      $("#loader").hide();
                //    return false;
                //   }
                //  else {

                //  }
                // }


            }
            return true;
        }
        function pagevalidation(aarystring) {

            debugger;
            $("#loader").show();
            var Accode = 0, accoid = 0, GId = 0;
            var status = document.getElementById("<%= btnSave.ClientID %>").value;
            var spname = "AccountMaster";
            var XML = "<ROOT>";
            if (status == "Update") {
                Accode = document.getElementById("<%= hdnfacdoc.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfacdoc.ClientID %>").value;
                accoid = document.getElementById("<%= hdnfaccid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfaccid.ClientID %>").value;
            }
            var txtAC_CODE = $("#<%=txtAC_CODE.ClientID %>").val();
            var drpType = $("#<%=drpType.ClientID %>").val();
            var txtDistance = $("#<%=txtDistance.ClientID %>").val();

            //if ((drpType == "") || drpType == "0") {
            // $("#<%=drpType.ClientID %>").focus();
            //return;
            //}
            var txtPINCODE = $("#<%=txtPINCODE.ClientID %>").val();

            $("#loader").show();
            var txtGST_NO = $("#<%=txtGST_NO.ClientID %>").val();
            //var chkUnregisterGST = chkUnregisterGST.val();
            // var chkUnregisterGST = "";


            // if (txtGST_NO.length == 15) {
            //   if (chkUnregisterGST.checked != true) {
            //       $("#<%=txtGST_NO.ClientID %>").focus();
            //      return;
            // }
            //}
            //assign value from control

            var AC_CODE = $("#<%=txtAC_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtAC_CODE.ClientID %>").val();
            var AC_TYPE = $("#<%=drpType.ClientID %>").val();
            var Limit_By = $("#<%=drpLimit.ClientID %>").val() == "" ? 0 : $("#<%=drpLimit.ClientID %>").val();
            var AC_RATE = $("#<%=txtAC_RATE.ClientID %>").val() == "" ? 0 : $("#<%=txtAC_RATE.ClientID %>").val();
            var AC_NAME_E = $("#<%=txtAC_NAME_E.ClientID %>").val().toUpperCase();
            var AC_NAME_R = $("#<%=txtAC_NAME_R.ClientID %>").val();
            if (AC_NAME_R == "") {
                AC_NAME_R = AC_NAME_E;
            }
            var COMMISSION = $("#<%=txtCOMMISSION.ClientID %>").val() == "" ? 0 : $("#<%=txtCOMMISSION.ClientID %>").val();
            var Insurance = $("#<%=txtInsurance.ClientID %>").val() == "" ? 0 : $("#<%=txtInsurance.ClientID %>").val();

            var SHORT_NAME = $("#<%=txtSHORT_NAME.ClientID %>").val().toUpperCase();
            var ADDRESS_E = $("#<%=txtADDRESS_E.ClientID %>").val().toUpperCase();
            var ADDRESS_R = $("#<%=txtADDRESS_R.ClientID %>").val();
            var CITY_CODE = $("#<%=txtCITY_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtCITY_CODE.ClientID %>").val();
            var cityid = $("#<%=hdnfCity.ClientID %>").val() == "" ? 0 : $("#<%=hdnfCity.ClientID %>").val();
            var PINCODE = $("#<%=txtPINCODE.ClientID %>").val() == "" ? 0 : $("#<%=txtPINCODE.ClientID %>").val();
            var OPENING_BALANCE = $("#<%=txtOPENING_BALANCE.ClientID %>").val() == "" ? 0 : $("#<%=txtOPENING_BALANCE.ClientID %>").val();
            var DRCR = $("#<%=drpDrCr.ClientID %>").val();
            var DISTANCE = $("#<%=txtDistance.ClientID %>").val() == "" ? 0 : $("#<%=txtDistance.ClientID %>").val();
            var GSTStateCode = $("#<%=txtGstStateCode.ClientID %>").val() == "" ? 0 : $("#<%=txtGstStateCode.ClientID %>").val();
            var GROUP_CODE = $("#<%=txtGROUP_CODE.ClientID %>").val() == "" ? 0 : $("#<%=txtGROUP_CODE.ClientID %>").val();
            var bsGroup_Id = $("#<%=lblgroupid.ClientID %>").val() == "" ? 0 : $("#<%=lblgroupid.ClientID %>").val();
            var LOCAL_LIC_NO = $("#<%=txtLOCAL_LIC_NO.ClientID %>").val();
            var BANK_NAME = $("#<%=txtBANK_NAME.ClientID %>").val();
            var BANK_AC_NO = $("#<%=txtBANK_AC_NO.ClientID %>").val();
            var EMAIL_ID = $("#<%=txtEMAIL_ID.ClientID %>").val();
            var EMAIL_ID_CC = $("#<%=txtEMAIL_ID_CC.ClientID %>").val();


            var ADHARNO = $("#<%=txtAdhar_No.ClientID %>").val();
            var OTHER_NARRATIOM = $("#<%=txtOTHER_NARRATION.ClientID %>").val().toUpperCase();
            var MOBILE = $("#<%=txtMOBILE.ClientID %>").val();
            var IFSC = $("#<%=txtIfsc.ClientID %>").val().toUpperCase();
            var FSSAI = $("#<%=txtFssaiNo.ClientID %>").val();
            var TANNO = $("#<%=txtTan_No.ClientID %>").val();
            var BANK_OPENING = $("#<%=txtBANK_OPENING.ClientID %>").val() == "" ? 0 : $("#<%=txtBANK_OPENING.ClientID %>").val();
            var BANK_OP_DRCR = $("#<%=drpBankDrCr.ClientID %>").val();
            var WHATSUPNO = $("#<%=txtwhatsup_No.ClientID %>").val()
            var chkCarporate = $("#<%=chkCarporate.ClientID %>").is(":checked");
            var carporate_party = "";
            var UnregisterGSTChk = $("#<%=chkUnregisterGST.ClientID %>").is(":checked");
            var UnregisterGST = 0;
            TANNO = TANNO.trim();
            var locked = 0;
            if (chkCarporate == true) {
                carporate_party = "Y";
            }
            else {
                carporate_party = "N";
            }
            if (UnregisterGSTChk == true) {
                UnregisterGST = 1;
            }

            if ($("#<%=chkLocked.ClientID %>").is(":checked") == true) {
                locked = 1;
            }
            var referBy = $("#<%=txtRefBy.ClientID %>").val();
            var OffPhone = $("#<%=txtOffPhone.ClientID %>").val();
            var Fax = $("#<%=txtfax.ClientID %>").val();
            var PanLink = $("#<%=txtPanLink.ClientID %>").val();
            var CompanyPan = $("#<%=txtcompanyPan.ClientID %>").val().toUpperCase();
            var Company_Code = '<%= Session["Company_Code"] %>';
            var Year_Code = '<%= Session["year"] %>';
            var USER = '<%= Session["user"] %>';
            var Branch_Code = '<%= Session["Branch_Code"] %>';
            var cityid = document.getElementById("<%= hdnfCity.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfCity.ClientID %>").value;
            if (cityid == "" || cityid == "&nbsp;") {
                cityid = 0;
            }
            var gid = document.getElementById("<%= hdnfgid.ClientID %>").value == "" ? 0 : document.getElementById("<%= hdnfgid.ClientID %>").value;
            if (gid == "" || gid == "&nbsp;") {
                gid = 0;
            }

            var AcInsertUpdate = ""; AcDetail_Insert = ""; AcDetail_Update = ""; AcDetail_Delete = "";
            var AcDetail_Value = ""; concatid = "";
            if (status == "Save") {
                AcInsertUpdate = "Created_By='" + USER + "' Modified_By=''";
            }
            else {
                AcInsertUpdate = "Modified_By='" + USER + "'";
            }
            var TDSApplicable = $("#<%=drpTdsApplicable.ClientID %>").val();

            var customercode = document.getElementById("<%= hdnfcustomercode.ClientID  %>").value == "" ? 0 : document.getElementById("<%= hdnfcustomercode.ClientID %>").value;

            XML = XML + "<AccountHead Ac_Code='" + Accode + "' Ac_Name_E='" + AC_NAME_E + "' Ac_Name_R='" + AC_NAME_R + "' Ac_type='" + AC_TYPE + "' " +
                "Ac_rate='" + AC_RATE + "' Address_E='" + ADDRESS_E + "' Address_R='" + ADDRESS_R + "' " +
                   "City_Code='" + CITY_CODE + "' Pincode='" + PINCODE + "' Local_Lic_No='" + LOCAL_LIC_NO + "' Tin_No='' Cst_no='' GST_NO='" + txtGST_NO + "' " +
                   "Email_Id='" + EMAIL_ID + "' EMAIL_ID_CC='" + EMAIL_ID_CC + "' Other_Narration='" + OTHER_NARRATIOM + "' ECC_No='' " +
                    "Bank_Name='" + BANK_NAME + "' Bank_Ac_No='" + BANK_AC_NO + "' Bank_Opening='" + BANK_OPENING + "' bank_Op_Drcr='" + BANK_OP_DRCR + "' " +
                    "Opening_Balance='" + OPENING_BALANCE + "' Drcr='" + DRCR + "' Group_Code='" + GROUP_CODE + "' " + AcInsertUpdate + " " +
                    "Short_Name='" + SHORT_NAME + "' Commission='" + COMMISSION + "' carporate_party='" + carporate_party + "' referBy='" + referBy + "' OffPhone='" + OffPhone + "' " +
                    "Fax='" + Fax + "' PanLink='" + PanLink + "' CompanyPan='" + CompanyPan + "' AC_Pan=''  Mobile_No='" + MOBILE + "' Is_Login='' IFSC='" + IFSC + "' " +
                    "FSSAI='" + FSSAI + "' Branch1OB='0' Branch2OB='0' Branch1Drcr='' Branch2Drcr='' Locked='" + locked + "' GSTStateCode='" + GSTStateCode + "' " +
                    "UnregisterGST='" + UnregisterGST + "' Distance='" + DISTANCE + "' Bal_Limit='0.00' accoid='" + accoid + "' bsid='" + gid + "' cityid='" + cityid + "' " +
                    "whatsup_no='" + WHATSUPNO + "' company_code='" + Company_Code + "' adhar_no='" + ADHARNO +
                    "' Limit_By='" + Limit_By + "' Tan_no='" + TANNO + "' TDSApplicable='" + TDSApplicable + "' CustomerCode='" + customercode + "' Insurance='" + Insurance + "'>";

            debugger;
            var gridView = document.getElementById("<%=grdDetail.ClientID %>");
            if (gridView != null) {
                var grdrow = gridView.getElementsByTagName("tr");
                for (var i = 1; i < grdrow.length; i++) {
                    var personId = gridView.rows[i].cells[2].innerHTML;
                    var personName = gridView.rows[i].cells[3].innerHTML;
                    if (personName == "&nbsp;" || personName == "") {
                        personName = "";
                    }
                    var mobileno = gridView.rows[i].cells[4].innerHTML;
                    if (mobileno == "&nbsp;" || mobileno == "") {
                        mobileno = "";
                    }
                    var email = gridView.rows[i].cells[5].innerHTML;
                    if (email == "&nbsp;" || email == "") {
                        email = "";
                    }
                    var pan = gridView.rows[i].cells[6].innerHTML;
                    if (pan == "&nbsp;" || pan == "") {
                        pan = "";
                    }
                    var other = gridView.rows[i].cells[7].innerHTML;
                    if (other == "&nbsp;" || other == "") {
                        other = "";
                    }
                    if (gridView.rows[i].cells[9].innerHTML != "R" && gridView.rows[i].cells[9].innerHTML != "D") {
                        XML = XML + "<AccountDetail PersonId='" + personId + "' Company_Code='" + Company_Code + "' Ac_Code='" + Accode + "' Person_Name='" + personName + "' " +
                               "Person_Mobile='" + mobileno + "' Person_Email='" + email + "' Person_Pan='" + pan + "' " +
                               "Other='" + other + "' accoid='" + accoid + "'/>";

                    }

                }
            }

            var GroupValues = ""; GroupDelete = ""; GroupInsert = "";
            var Gledger_Insert = ""; Gledger_values = ""; GLEDGER_Delete = "";

            var Group_gridView = document.getElementById("<%=grdGroup.ClientID %>");
            //checking checkbox aaray

            var array = aarystring.split(",");
            //var tet
            var text = JSON.stringify(JSON.stringify(array))
            if (Group_gridView != null) {
                var Groupgrdrow = Group_gridView.getElementsByTagName("tr");
                // if checkboc check then

                var GridView1 = document.getElementById("<%=grdGroup.ClientID %>");
                var GroupCodes = "";
                var cont = 1;
                //array.replace("'", "");
                for (i = 0; i < array.length; i++) {
                    GroupCodes = array[i];
                    if (GroupCodes == "[0" || GroupCodes == "0]" || GroupCodes == "0") {
                    }
                    else {
                        GroupCodes.replace(/"/g, "");
                        debugger;

                        //GroupCodes.replace(/^"|"$/g, '');
                        XML = XML + "<AccountGroup GroupID='" + cont + "' Company_Code='" + Company_Code + "' Ac_Code='" + AC_CODE + "' Group_Code='" + GroupCodes + "' accoid='" + accoid + "'/>";
                        XML = XML.replace(/"/g, "");
                        XML = XML.replace(/[\]}[{]/g, '');
                        cont = cont + 1;
                    }

                }

            }


            if (OPENING_BALANCE != 0) {
                XML = XML + "<Ledger TRAN_TYPE='OP' CASHCREDIT='' DOC_NO='" + Accode + "' DOC_DATE='2015/03/31' AC_CODE='" + Accode + "' " +
                                                        "UNIT_code='0' NARRATION='Opening Balance' AMOUNT='" + OPENING_BALANCE + "' TENDER_ID='0' TENDER_ID_DETAIL='0' VOUCHER_ID='0' COMPANY_CODE='" + Company_Code + "' " +
                                                        "YEAR_CODE='" + Year_Code + "' ORDER_CODE='1' DRCR='" + DRCR + "' DRCR_HEAD='0' ADJUSTED_AMOUNT='0' Branch_Code='1' " +
                                                        "SORT_TYPE='OP' SORT_NO='" + Accode + "' ac='" + accoid + "' vc='0' progid='2' tranid='0'/>";
            }
            XML = XML + "</AccountHead></ROOT>";
            debugger;
            var RecordStatus = $("#<%=btnSave.ClientID %>").val();
            ProcessXML(XML, RecordStatus, spname);


        }
        function DeleteConform() {
            debugger;
            $("#loader").show();
            var DocNo = document.getElementById("<%= hdnfacdoc.ClientID %>").value;
            var accoid = document.getElementById("<%= hdnfaccid.ClientID %>").value;

            var Company_Code = '<%= Session["Company_Code"] %>';

            var XML = "<ROOT><AccountHead Ac_Code='" + DocNo + "' accoid='" + accoid + "' company_code='" + Company_Code + "' ></AccountHead></ROOT>";
            var spname = "AccountMaster";
            var RecordStatus = document.getElementById("<%= btnDelete.ClientID %>").value;
            ProcessXML(XML, RecordStatus, spname);


        }
        function ProcessXML(XML, RecordStatus, spname) {
            debugger;
            $.ajax({
                type: "POST",
                //url: "../BussinessRelated/pgeDeliveryOrderForGSTxmlNew.aspx/NewInsert",
                url: "../xmlExecuteDMLQry.asmx/ExecuteXMLQry",
                data: '{XML:"' + XML + '",status:"' + RecordStatus + '",spname:"' + spname + '"}',
                //data: '{DoInsert: "' + DOInsert + '", DoDetail_Insert: "' + DoDetail_Insert + '",PurchaseInsert: "' + PurchaseInsert + '",PurchaseInsertDetail: "' + PurchaseInsertDetail + '",SaleBillInsert: "' + SaleBillInsert + '",SaleBillDetailInsert: "' + SaleBillDetailInsert + '",TenderInsert: "' + TenderInsert + '",TenderUpdate: "' + TenderUpdate + '",DoDetailUpdate:"' + DoDetailUpdate + '",DoDetailDelete:"' + DoDetailDelete + '",GLEDGERDO_Delete:"' + GLEDGERDO_Delete + '",GLEDGER_Insert:"' + GLEDGER_Insert + '",RecordStatus:"' + RecordStatus + '"}',
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
                if (RecordStatus != "Delete") {
                    if (response.d.length > 0) {
                        var word = response.d;
                        var len = word.length;
                        var pos = word.indexOf(",");
                        var id = word.slice(0, pos);
                        var doc = word.slice(pos + 1, len);
                        if (status == "Save") {
                            //alert('Sucessfully Added Record !!! Doc_no=' + doc)
                            swal("Sucessfully Added Record!", " Doc_no=" + doc + "", "success");
                        }
                        else {
                            //alert('Sucessfully Updated Record !!! Doc_no=' + doc)
                            swal("Sucessfully Updated Record!", " Doc_no=" + doc + "", "success");

                        }

                        window.open('../Master/pgeAccountsmaster.aspx?Ac_Code=' + id + '&Action=1', "_self");


                    }
                }
                else {
                    var num = parseInt(response.d);

                    if (isNaN(num)) {
                        //alert(response.d)
                        swal("" + response.d + "", "", "warning");
                        //   window.open('../Master/pgeAccountUtility.aspx', "_self");
                    }
                    else {
                        window.open('../Master/pgeAccountUtility.aspx', "_self");
                    }
                }

            }
        }

        function OnSuccess(response) {

            $("#loader").hide();
            if (RecordStatus != "Delete") {
                if (response.d.length > 0) {
                    var word = response.d;
                    var len = word.length;
                    var pos = word.indexOf(",");
                    var id = word.slice(0, pos);
                    var doc = word.slice(pos + 1, len);
                    if (status == "Save") {
                        //alert('Sucessfully Added Record !!! Doc_no=' + doc)
                        swal("Sucessfully Added Record!", " Doc_no=" + doc + "", "success");
                    }
                    else {
                        //alert('Sucessfully Updated Record !!! Doc_no=' + doc)
                        swal("Sucessfully Updated Record!", " Doc_no=" + doc + "", "success");

                    }

                    window.open('../Master/pgeAccountsmaster.aspx?Ac_Code=' + id + '&Action=1', "_self");

                }
            }
            else {
                var num = parseInt(response.d);

                if (isNaN(num)) {
                    //alert(response.d)
                    swal("" + response.d + "", "", "warning");

                }
                else {
                    window.open('../Master/pgeAccountUtility.aspx', "_self");
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <fieldset style="border-top: 1px dotted rgb(131, 127, 130); width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 5px; border-left: 0px; border-right: 0px; height: 7px;">
                <legend style="text-align: center;">
                    <asp:Label ID="label1" runat="server" Text="   Account Master   " Font-Names="verdana"
                        ForeColor="White" Font-Bold="true" Font-Size="12px"></asp:Label></legend>
            </fieldset>
            <div id="loader" align="center" style="display: none; width: 90%; margin-left: 30px; float: left; border-bottom: 0px; padding-top: 0px; padding-bottom: 10px; border-left: 0px; border-right: 0px;">
                <img height="40%" width="40%" src="../Images/ajax-loader3.gif" />
            </div>
            <asp:HiddenField ID="hdconfirm" runat="server" />
            <asp:HiddenField ID="hdnfClosePopup" runat="server" />
            <asp:HiddenField ID="hdnf" runat="server" />
            <asp:HiddenField ID="hdnfSuffix" runat="server" />
            <asp:HiddenField ID="hdHelpPageCount" runat="server" />
            <asp:HiddenField ID="hdnfCity" runat="server" />
            <asp:HiddenField ID="hdnfgid" runat="server" />
            <asp:HiddenField ID="hdnfacdoc" runat="server" />
            <asp:HiddenField ID="hdnfaccid" runat="server" />
            <asp:HiddenField ID="hdnfyearcode" runat="server" />
            <asp:HiddenField ID="hdnfcompanycode" runat="server" />
             <asp:HiddenField ID="hdnfcustomercode" runat="server" />
            <table width="100%" align="left">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnAdd_Click" Height="22px" Visible="true" />
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnHelp" Width="90px"
                            OnClientClick="if (!Validate()) return false;" ValidationGroup="add" OnClick="btnSave_Click"
                            Height="22px" TabIndex="43" />
                        &nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnEdit_Click" Height="22px" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnHelp" Width="90px"
                            ValidationGroup="add" OnClick="btnDelete_Click" OnClientClick="return Confirm(this, event);" Height="22px" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" OnClick="btnCancel_Click" Height="22px" />
                        &nbsp;
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnHelp" Width="90px"
                            ValidationGroup="save" Height="24px" OnClientClick="BACK()" />
                    </td>
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
            <asp:Panel ID="pnlMain" runat="server" Font-Names="verdana" Font-Bold="true" ForeColor="Black"
                Font-Size="Small" Style="margin-left: 30px; margin-top: 0px; z-index: 100;">
                <table cellspacing="3">
                    <tr>
                        <td align="left">Change No:
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox runat="server" ID="txtEditDoc_No" CssClass="txt" Width="100px" Height="24px"
                                TabIndex="0" AutoPostBack="true" OnTextChanged="txtEditDoc_No_TextChanged"></asp:TextBox>
                            <%-- <ajax1:FilteredTextBoxExtender runat="server" ID="txtxf" FilterType="Numbers" InvalidChars="."
                                    TargetControlID="txtEditDoc_No">
                                </ajax1:FilteredTextBoxExtender>--%>
                            &nbsp;
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Account Code:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtAC_CODE" runat="Server" AutoPostBack="True" CssClass="txt" Height="22px"
                                OnTextChanged="txtAC_CODE_TextChanged" Style="text-align: right;" TabIndex="0"
                                Width="80px"></asp:TextBox>
                            <asp:Button ID="btntxtAC_CODE" runat="server" CssClass="btnHelp" Height="22px" OnClick="btntxtAC_CODE_Click"
                                Text="..." Width="80px" />
                            <asp:Label ID="lblAc_Code" runat="server" Font-Bold="true" Font-Italic="true" Font-Names="verdana"
                                Font-Size="Small" ForeColor="Blue" Visible="false"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%;">Type:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:DropDownList ID="drpType" runat="server" AutoPostBack="true" CssClass="ddl"
                                Height="25px" OnSelectedIndexChanged="drpType_SelectedIndexChanged" TabIndex="1"
                                Width="200px">
                                <asp:ListItem Selected="True" Text="Party" Value="P"></asp:ListItem>
                                <asp:ListItem Text="Supplier" Value="S"></asp:ListItem>
                                <asp:ListItem Text="Bank" Value="B"></asp:ListItem>
                                <asp:ListItem Text="Cash" Value="C"></asp:ListItem>
                                <asp:ListItem Text="Relative" Value="R"></asp:ListItem>
                                <asp:ListItem Text="Fixed Assets" Value="F"></asp:ListItem>
                                <asp:ListItem Text="Interest Party" Value="I"></asp:ListItem>
                                <asp:ListItem Text="Income/Expenses" Value="E"></asp:ListItem>
                                <asp:ListItem Text="Trading" Value="O"></asp:ListItem>
                                <asp:ListItem Text="Mill" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Transport" Value="T"></asp:ListItem>
                                <asp:ListItem Text="Broker" Value="BR"></asp:ListItem>
                                <asp:ListItem Text="Retail Party" Value="RP"></asp:ListItem>
                                <asp:ListItem Text="Cash Retail Party" Value="CR"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">
                            <asp:Label Text="" runat="server" ID="lblinterestrate" />
                        </td>
                        <td align="left" style="width: 10%;">
                         <asp:TextBox ID="txtAC_RATE" runat="Server" AutoPostBack="True" CssClass="txt" Height="22px"
                                OnTextChanged="txtAC_RATE_TextChanged" Style="text-align: right;" TabIndex="2"
                                Width="80px"></asp:TextBox>

                           
                        </td>
                        <td align="left" style="width: 10%;">Limit:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:DropDownList ID="drpLimit" runat="server" AutoPostBack="false" CssClass="ddl"
                                Height="25px" TabIndex="2" Width="200px">
                                <asp:ListItem Text="By Limit" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="No Limit" Selected="True" Value="N"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Name of account:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtAC_NAME_E" runat="Server" CssClass="txt" AutoPostBack="false"
                                Height="22px" OnTextChanged="txtAC_NAME_E_TextChanged" Style="text-align: left;"
                                TabIndex="3" Width="250px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">Regional Name:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtAC_NAME_R" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="22px" OnTextChanged="txtAC_NAME_R_TextChanged" Style="text-align: left;"
                                TabIndex="4" Width="200px"></asp:TextBox>
                        </td>
                        <td align="center" colspan="2" rowspan="6" valign="top">
                            <asp:Panel ID="pnlGroup" runat="server" BorderColor="Navy" BorderWidth="1px" Height="200px"
                                ScrollBars="Auto" Width="350px">
                                <asp:GridView ID="grdGroup" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                                    EmptyDataText="No Records Found" Height="112px" Width="90%">
                                    <Columns>
                                        <%--<asp:BoundField ControlStyle-CssClass="invisible" ControlStyle-Width="5px" DataField="System_Code"
                                            HeaderStyle-CssClass="invisible" HeaderStyle-Width="5px" HeaderText="code" ItemStyle-CssClass="invisible"
                                            ItemStyle-Width="5px" />--%>
                                        <asp:BoundField ControlStyle-Width="5px" DataField="System_Code" HeaderStyle-Width="5px"
                                            HeaderText="code" ItemStyle-Width="5px" />
                                        <asp:BoundField DataField="System_Name_E" HeaderText="Group Name" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Checked="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="MediumOrchid" ForeColor="White" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Commission Rate:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtCOMMISSION" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="22px" OnTextChanged="txtCOMMISSION_TextChanged" Style="text-align: right;"
                                TabIndex="5" Width="103px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="filtertxtCOMMISSION" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtCOMMISSION" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Insurance Rate:
                            <asp:TextBox ID="txtInsurance" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="22px" OnTextChanged="txtInsurance_TextChanged" Style="text-align: right;"
                                TabIndex="5" Width="103px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtInsurance" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                        <td align="left" style="width: 10%;">Short Name:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtSHORT_NAME" runat="Server" AutoPostBack="false" CssClass="txtUpper"
                                Height="22px" OnTextChanged="txtSHORT_NAME_TextChanged" Style="text-align: left;" MaxLength="17"
                                TabIndex="6" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Address:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtADDRESS_E" runat="Server" AutoPostBack="false" CssClass="txtUpper"
                                Height="50px" OnTextChanged="txtADDRESS_E_TextChanged" Style="text-align: left;"
                                TabIndex="7" TextMode="MultiLine" Width="450px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">Address 2:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtADDRESS_R" runat="Server" AutoPostBack="false" CssClass="txtUpper"
                                Height="50px" OnTextChanged="txtADDRESS_R_TextChanged" Style="text-align: left;"
                                TabIndex="8" TextMode="MultiLine" Width="450px"></asp:TextBox>
                        </td>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">City Code:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtCITY_CODE" runat="Server" onkeydown="citycode(event);" CssClass="txt"
                                AutoPostBack="false" Height="22px" OnTextChanged="txtCITY_CODE_TextChanged" Style="text-align: right;"
                                TabIndex="9" Width="80px"></asp:TextBox>
                            <asp:Button ID="btntxtCITY_CODE" runat="server" CssClass="btnHelp" Height="22px"
                                OnClick="btntxtCITY_CODE_Click" Text="..." Width="20px" />
                            <asp:Button ID="btnAddCity" runat="server" CssClass="btnHelp" OnClientClick="AddCity()"
                                Text="Add New City" Width="100px" />
                            <asp:Label ID="lblCITYNAME" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblcityid" runat="server" CssClass="lblName" Visible="false"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%;">Pin Code:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtPINCODE" runat="Server" AutoPostBack="false" CssClass="txt" Height="22px"
                                OnTextChanged="txtPINCODE_TextChanged" Style="text-align: right;" TabIndex="10"
                                Width="80px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="rgdt" runat="server" FilterType="Numbers" TargetControlID="txtPINCODE">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">GST State Code:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtGstStateCode" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="24px" OnTextChanged="txtGstStateCode_TextChanged" Style="text-align: right;"
                                TabIndex="11" Width="80px" onkeydown="StateCode(event);"></asp:TextBox>
                            <asp:Button ID="btntxtGstStateCode" runat="server" CssClass="btnHelp" Height="24px"
                                OnClick="btntxtGstStateCode_Click" Text="..." Width="20px" />
                            <asp:Label ID="lbltxtGstStateName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%; vertical-align: top;">Distance:
                        </td>
                        <td align="left" style="width: 10%; vertical-align: top;">
                            <asp:TextBox ID="txtDistance" runat="Server" AutoPostBack="false" CssClass="txtUpper"
                                Height="24px" OnTextChanged="txtDistance_TextChanged" Style="text-align: left;"
                                TabIndex="12" Width="80px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom"
                                ValidChars="." TargetControlID="txtDistance">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Opening Balance:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtOPENING_BALANCE" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="22px" OnTextChanged="txtOPENING_BALANCE_TextChanged" Style="text-align: right;"
                                TabIndex="13" Width="103px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtOPENING_BALANCE" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            &nbsp; DRCR:
                            <asp:DropDownList ID="drpDrCr" runat="server" OnSelectedIndexChanged="drpDrCr_SelectedIndexChanged"
                                TabIndex="14" Width="80px">
                                <asp:ListItem Text="Debit" Value="D"></asp:ListItem>
                                <asp:ListItem Text="Credit" Value="C"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 10%;">Group Code:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtGROUP_CODE" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="22px" OnTextChanged="txtGROUP_CODE_TextChanged" Style="text-align: right;"
                                TabIndex="19" Width="80px" onkeydown="GroupCode(event);"></asp:TextBox>
                            <asp:Button ID="btntxtGROUP_CODE" runat="server" CssClass="btnHelp" Height="22px"
                                OnClick="btntxtGROUP_CODE_Click" Text="..." Width="20px" />
                            <asp:Button ID="btnAddGroup" runat="server" CssClass="btnHelp" OnClick="btnAddGroup_Click1"
                                Text="Add Group" Width="100px" />
                            <asp:Label ID="lblGROUPNAME" runat="server" CssClass="lblName"></asp:Label>
                            <asp:Label ID="lblgroupid" runat="server" CssClass="lblName" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Bank Opening Bal:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtBANK_OPENING" runat="Server" AutoPostBack="false" CssClass="txt"
                                OnTextChanged="txtBANK_OPENING_TextChanged" Style="text-align: right;" TabIndex="20"
                                Visible="true" Width="103px" Height="22px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="filtertxtBANK_OPENING" runat="server" FilterType="Numbers,Custom"
                                TargetControlID="txtBANK_OPENING" ValidChars=".">
                            </ajax1:FilteredTextBoxExtender>
                            Bank DrCr:
                            <asp:DropDownList ID="drpBankDrCr" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpBankDrCr_SelectedIndexChanged"
                                TabIndex="21" Visible="true" Width="100px">
                                <asp:ListItem Text="Debit" Value="D"></asp:ListItem>
                                <asp:ListItem Text="Credit" Value="C"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">Is Carporate Party:
                        </td>
                        <td align="left">
                            <asp:CheckBox ID="chkCarporate" TabIndex="22" runat="server" Checked="false" Text="Yes" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Sugar Lic No:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtLOCAL_LIC_NO" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="22px" OnTextChanged="txtLOCAL_LIC_NO_TextChanged" Style="text-align: left;"
                                TabIndex="23" Width="200px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">Ref By:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtRefBy" runat="server" CssClass="txt" Height="22px" OnTextChanged="txtRefBy_TextChanged"
                                TabIndex="28" Width="200px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">Email:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtEMAIL_ID" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="22px" OnTextChanged="txtEMAIL_ID_TextChanged" Style="text-align: left;"
                                TabIndex="33" ToolTip="You can add multiple email using comma after one emailid"
                                Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">GST:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtGST_NO" runat="Server" AutoPostBack="true" CssClass="txtUpper"
                                Height="22px" OnTextChanged="txtGST_NO_TextChanged" Style="text-align: left;"
                                TabIndex="24" Width="200px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">Bank IFSC Code:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtIfsc" runat="Server" AutoPostBack="false" CssClass="txtUpper"
                                Height="23px" OnTextChanged="txtIfsc_TextChanged" Style="text-align: left;" TabIndex="29"
                                Width="200px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">CC Email:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtEMAIL_ID_CC" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="26px" OnTextChanged="txtEMAIL_ID_CC_TextChanged" Style="text-align: left;"
                                TabIndex="34" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Company Pan:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtcompanyPan" runat="server" CssClass="txtUpper" Height="22px"
                                AutoPostBack="false" OnTextChanged="txtcompanyPan_TextChanged" TabIndex="25"
                                Width="200px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">Bank Ac No:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtBANK_AC_NO" runat="Server" AutoPostBack="false" CssClass="txt"
                                Height="22px" OnTextChanged="txtBANK_AC_NO_TextChanged" Style="text-align: left;"
                                TabIndex="30" Width="200px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">Off. Phone:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtOffPhone" runat="server" CssClass="txt" Height="22px" OnTextChanged="txtOffPhone_TextChanged"
                                TabIndex="35" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%; vertical-align: top;">Tan No:
                        </td>
                        <td align="left" style="width: 10%; vertical-align: top;">
                            <asp:TextBox ID="txtTan_No" runat="Server" AutoPostBack="false" CssClass="txtUpper"
                                Height="22px" OnTextChanged="txtTan_No_TextChanged" Style="text-align: left;"
                                TabIndex="26" Width="200px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">Bank Name:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtBANK_NAME" runat="Server" AutoPostBack="false" CssClass="txtUpper"
                                Height="22px" OnTextChanged="txtBANK_NAME_TextChanged" Style="text-align: left;"
                                TabIndex="31" Width="200px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">TCS/TDS Link:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtfax" runat="server" CssClass="txt" Height="22px" OnTextChanged="txtfax_TextChanged"
                                TabIndex="36" Width="200px"></asp:TextBox><asp:HyperLink target="_blank" ID="HyperLink1" runat="server">GO</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%; vertical-align: top;">FSSAI Lic No:
                        </td>
                        <td align="left" style="width: 10%; vertical-align: top;">
                            <asp:TextBox ID="txtFssaiNo" runat="Server" AutoPostBack="false" CssClass="txtUpper"
                                Height="22px" OnTextChanged="txtFssaiNo_TextChanged" Style="text-align: left;"
                                TabIndex="26" Width="200px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                        <td align="left" style="width: 10%;">Pan Link:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtPanLink" runat="server" CssClass="txt" Height="22px" OnTextChanged="txtPanLink_TextChanged"
                                TabIndex="36" Width="200px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Adhar No:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtAdhar_No" runat="Server" AutoPostBack="false" CssClass="txtUpper"
                                Height="22px" OnTextChanged="txtAdhar_No_TextChanged" Style="text-align: left;"
                                TabIndex="27" Width="200px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%; vertical-align: top;">Other Narration:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtOTHER_NARRATION" runat="Server" AutoPostBack="false"
                                Height="50px" OnTextChanged="txtOTHER_NARRATION_TextChanged" Style="text-align: left;"
                                TabIndex="32" TextMode="MultiLine" Width="200px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%;">Mobile No.:
                        </td>
                        <td>
                            <asp:TextBox ID="txtMOBILE" runat="server" AutoPostBack="false" CssClass="txt" Height="22px"
                                OnTextChanged="txtMOBILE_TextChanged" Style="text-align: left;" TabIndex="37"
                                Width="200px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                ValidChars="," TargetControlID="txtMOBILE">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%; vertical-align: top;">Unregister For GST:
                        </td>
                        <td align="left" style="width: 10%; vertical-align: top;">
                            <asp:CheckBox ID="chkUnregisterGST" runat="server" Text="" />
                        </td>
                        <td align="left" style="width: 10%; vertical-align: top;">Locked:
                        </td>
                        <td align="left" style="width: 10%; vertical-align: top;">
                            <asp:CheckBox ID="chkLocked" runat="server" Text="" />
                        </td>
                        <td align="left" style="width: 10%;">WhatsUp_No:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtwhatsup_No" runat="Server" AutoPostBack="false" CssClass="txtUpper"
                                Height="22px" OnTextChanged="txtwhatsup_No_TextChanged" Style="text-align: left;"
                                TabIndex="38" Width="200px"></asp:TextBox>
                            <ajax1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers,Custom"
                                ValidChars="," TargetControlID="txtwhatsup_No">
                            </ajax1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>TDS Applicable
                            <asp:DropDownList ID="drpTdsApplicable" runat="server" AutoPostBack="True"
                                TabIndex="21" Visible="true" Width="100px">
                                <asp:ListItem Text="Lock" Value="L"></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnOpenDetailsPopup" runat="server" CssClass="btnHelp" Height="25px"
                                OnClick="btnOpenDetailsPopup_Click" TabIndex="39" Text="Add Contacts" Width="90px" />
                        </td>
                        <td>DRCR DIFF:<asp:Label ID="lblDRCRDiff" runat="server"></asp:Label>
                        </td>
                        <td></td>
                        <td></td>
                        <td align="right">
                            <asp:TextBox ID="txtSendingAcCode" runat="server" AutoPostBack="true" CssClass="txt"
                                Height="24px" OnTextChanged="txtSendingAcCode_TextChanged" Width="60px" onkeydown="sendingaccode(event);"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:Button ID="btntxtSendingAcCode" runat="server" CssClass="btnHelp" Height="24px"
                                OnClick="btntxtSendingAcCode_Click" Text="..." Width="20px" />
                            &nbsp;<asp:Label ID="lblSendingAcCode" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    </tr>
                </table>
            </asp:Panel>
            <div style="width: 100%; position: relative; top: 0px; left: 0px;">
                <%-- <asp:UpdatePanel ID="upGrid" runat="server">
                    <ContentTemplate>--%>
                <asp:Panel ID="pnlgrdDetail" runat="server" ScrollBars="Both" Height="120px" Width="1000px"
                    BorderColor="Maroon" BorderWidth="1px" BorderStyle="Solid" Font-Bold="true" Font-Names="Verdana"
                    Font-Size="11px" BackColor="SeaShell" Style="margin-left: 30px; float: left;">
                    <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="true" HeaderStyle-BackColor="#397CBB"
                        HeaderStyle-ForeColor="White" HeaderStyle-Height="30px" GridLines="Both" Width="100%"
                        OnRowCommand="grdDetail_RowCommand" CellPadding="5" CellSpacing="5" OnRowDataBound="grdDetail_RowDataBound"
                        Style="table-layout: fixed;" OnRowCreated="grdDetail_RowCreated">
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
                <%--    </ContentTemplate>
                </asp:UpdatePanel>--%>
                <%-- <asp:TextBox ID="TextBox1" runat="Server" CssClass="txt" TabIndex="0" Width="80px"
                    Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtAC_CODE_TextChanged"
                    Height="22px"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="..." Width="80px" OnClick="btntxtAC_CODE_Click"
                    CssClass="btnHelp" Height="22px" />--%>
                <!-- //clientsDropDown -->
                <%--<div style="width: 200px;height:100px; float: right; margin-bottom: 0px;background-color:White;"></div>--%>
                <br />
                <asp:TextBox Style="margin-left: -40px" ReadOnly="false" ToolTip="Mobile" runat="server"
                    ID="txtSendingMobile" Width="120px" CssClass="txt" Height="24px"></asp:TextBox>&nbsp;
                <asp:TextBox ReadOnly="false" ToolTip="Email" runat="server" ID="txtSendingEmail"
                    Width="120px" CssClass="txt" Height="24px"></asp:TextBox><br />
                <br />
                <asp:CheckBox runat="server" ID="chkAddressDetails" Text="Address" AutoPostBack="true"
                    TextAlign="Left" Font-Bold="true" OnCheckedChanged="chkAddressDetails_CheckedChanged" />&nbsp;<asp:CheckBox
                        AutoPostBack="true" runat="server" ID="chkBankDetails" Text="Bank Details" TextAlign="Left"
                        Font-Bold="true" OnCheckedChanged="chkBankDetails_CheckedChanged" />
                <br />
                <asp:Button Text="SMS" ID="btnSendSMS" CommandName="sms" CssClass="btnHelp" Height="24px"
                    Width="80px" runat="server" OnCommand="btnSendSMS_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button Text="E-Mail" ID="btnEmail" CssClass="btnHelp" Height="24px" Width="80px"
                    CommandName="email" runat="server" OnCommand="btnSendSMS_Click" />
                <asp:Button runat="server" ID="btnPrintSaleBill" Text="Print" CssClass="btnHelp"
                    Width="80px" Height="24px" OnClientClick="SB();" />
            </div>
            <asp:Panel ID="pnlPopup" onkeydown="closepopup(event);" runat="server" Width="70%"
                align="center" ScrollBars="None" BackColor="#FFFFE4" Direction="LeftToRight"
                Style="z-index: 5000; position: absolute; display: none; float: right; max-height: 500px; min-height: 500px; box-shadow: 1px 1px 8px 2px; background-position: center; left: 10%; top: 10%;">
                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Images/closebtn.jpg"
                    OnClick="imgBtnClose_Click" Width="20px" Height="20px" Style="float: right; vertical-align: top;"
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
                                Width="250px" Height="20px" AutoPostBack="true" OnTextChanged="txtSearchText_TextChanged"
                                OnKeyPressed="txtSearch_KeyPressed()"></asp:TextBox>
                            <asp:Button ID="btnSearch" onkeydown="SelectFirstRow(event);" runat="server" Text="Search"
                                CssClass="btnSubmit" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlInner" runat="server" Width="100%" Direction="LeftToRight" BackColor="#FFFFE4"
                                Style="z-index: 5000; float: right; overflow: auto; height: 400px">
                                <asp:GridView ID="grdPopup" CssClass="select" runat="server" AutoGenerateColumns="true"
                                    AllowPaging="true" PageSize="30" EmptyDataText="No Records Found" HeaderStyle-BackColor="#6D8980"
                                    HeaderStyle-ForeColor="White" OnPageIndexChanging="grdPopup_PageIndexChanging"
                                    OnRowCreated="grdPopup_RowCreated" OnRowDataBound="grdPopup_RowDataBound" Style="table-layout: fixed;">
                                    <HeaderStyle Height="30px" ForeColor="White" BackColor="#6D8980" />
                                    <RowStyle Height="25px" Width="100px" ForeColor="Black" Wrap="true" />
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
                <table width="80%" align="center" cellspacing="4">
                    <tr>
                        <td colspan="2" align="center" style="background-color: Silver; height: 15px; color: White;">
                            <asp:Label ID="lblTenderDetails" runat="server" Font-Size="Medium" Font-Names="verdana"
                                Text="Contact Details"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblID" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblNo" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Person Name:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtPERSON_NAME" runat="Server" CssClass="txtUpper" TabIndex="40"
                                Width="250px" Height="22px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPERSON_NAME_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Mobile:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtPERSON_MOBILE" runat="Server" CssClass="txt" TabIndex="41" Width="200px"
                                Height="22px" Style="text-align: right;" AutoPostBack="false" OnTextChanged="txtPERSON_MOBILE_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Email:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtPERSON_EMAIL" runat="Server" CssClass="txt" TabIndex="42" Width="200px"
                                Height="22px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPERSON_EMAIL_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">Pan:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtPerson_PAN" runat="Server" CssClass="txt" TabIndex="43" Width="200px"
                                Height="22px" Style="text-align: left;" AutoPostBack="false" OnTextChanged="txtPerson_PAN_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 10%;">OTHER:
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:TextBox ID="txtPERSON_OTHER" runat="Server" CssClass="txt" TabIndex="44" Width="200px"
                                Height="60px" TextMode="MultiLine" Style="text-align: left;" AutoPostBack="false"
                                OnTextChanged="txtPERSON_OTHER_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Button ID="btnAdddetails" runat="server" Text="ADD" CssClass="btnSubmit" Width="80px"
                                Height="25px" OnClick="btnAdddetails_Click" TabIndex="45" />
                            <asp:Button ID="btnClosedetails" runat="server" Text="Close" CssClass="btnSubmit"
                                Width="80px" Height="25px" OnClick="btnClosedetails_Click" TabIndex="46" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div id="pnlCity" class="city" style="display: none;">
                <asp:ImageButton runat="server" ID="imgClose" ImageUrl="~/Images/closebtn.jpg" Width="20px"
                    Height="20px" Style="float: right; vertical-align: top;" ToolTip="Close" OnClick="imgClose_Click" />
                <table cellspacing="7">
                    <tr>
                        <td colspan="2" align="center">
                            <h3 style="color: White; margin-top: 2px;">CITY MASTER
                            </h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblCityCode" ForeColor="White" Text="City Code:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtCityCode" Height="24px" Width="80px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label2" ForeColor="White" Text="City Name:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtCityName" CssClass="txtUpper" Height="24px" Width="200px"
                                onkeyup="javascript:onfocus();"></asp:TextBox>
                            <asp:Label runat="server" ID="lblErr" Text="" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label3" ForeColor="White" Text="Regional Name:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtRegionalName" CssClass="txt" Height="24px" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label10" ForeColor="White" Text="Pincode:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtpincodecity" runat="server" CssClass="txtUpper" Width="200px"
                                AutoPostBack="false" Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label11" ForeColor="White" Text="Sub Area:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSubArea" runat="server" CssClass="txtUpper" Width="200px" AutoPostBack="false"
                                Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label4" ForeColor="White" Text="State:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="txtState" CssClass="ddl" Width="200px" Height="24px">
                                <asp:ListItem>ANDRAPRADESH</asp:ListItem>
                                <asp:ListItem>BANGAL</asp:ListItem>
                                <asp:ListItem>CHATTISGAD</asp:ListItem>
                                <asp:ListItem>DELHI</asp:ListItem>
                                <asp:ListItem>GUJRAT</asp:ListItem>
                                <asp:ListItem>HARIYANA</asp:ListItem>
                                <asp:ListItem>HIMACHAL PRADESH</asp:ListItem>
                                <asp:ListItem>JAMMU KASHMIR</asp:ListItem>
                                <asp:ListItem>JHARKHAND</asp:ListItem>
                                <asp:ListItem>KARNATAKA</asp:ListItem>
                                <asp:ListItem>MADHYA PRADESH</asp:ListItem>
                                <asp:ListItem>MAHARASHTRA</asp:ListItem>
                                <asp:ListItem>PUNJAB</asp:ListItem>
                                <asp:ListItem>RAJASTHAN</asp:ListItem>
                                <asp:ListItem>TAMIL NADU</asp:ListItem>
                                <asp:ListItem>ODISA</asp:ListItem>
                                <asp:ListItem>UTTARPRADESH</asp:ListItem>
                                <asp:ListItem>GOA</asp:ListItem>
                                <asp:ListItem>KERALA</asp:ListItem>
                                <asp:ListItem>TELANGANA</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <%-- <td align="left">
                            <asp:TextBox runat="server" ID="txtState" CssClass="txtUpper" Height="24px" Width="200px"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label runat="server" ID="Label12" ForeColor="White" Text="Gst State Code:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtstatecode" runat="Server" CssClass="txt" TabIndex="47" Width="80px"
                                Style="text-align: right;" AutoPostBack="True" OnTextChanged="txtstatecode_TextChanged"
                                Height="24px" OnKeyDown="state(event);"></asp:TextBox>
                            <asp:Button ID="btngststatecode" runat="server" Text="..." OnClick="btngststatecode_Click"
                                CssClass="btnHelp" Height="24px" Width="20px" />
                            <asp:Label ID="lblGstStateName" runat="server" CssClass="lblName"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label13" ForeColor="White" Text="Distance:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtdist" runat="server" CssClass="txtUpper" Width="120px" AutoPostBack="false"
                                Height="24px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button runat="server" ID="btnSaveCity" Text="SAVE" CssClass="button" OnClick="btnSaveCity_Click" />
                        </td>
                        <td align="center">
                            <asp:Button runat="server" ID="btnCancelCity" Text="Cancel" CssClass="button" />
                        </td>
                    </tr>
                </table>
                <asp:Button runat="server" ID="btn12" Style="display: none;" />
                <ajax1:ModalPopupExtender ID="modalCity" BackgroundCssClass="bagroundPopup" TargetControlID="btn12"
                    BehaviorID="mpe" PopupControlID="pnlCity" runat="server">
                </ajax1:ModalPopupExtender>
            </div>
            <div id="BSGroup" class="city" style="display: none;">
                <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Images/closebtn.jpg"
                    Width="20px" Height="20px" Style="float: right; vertical-align: top;" ToolTip="Close"
                    OnClick="ImageButton1_Click" />
                <table cellspacing="7">
                    <tr>
                        <td colspan="2" align="center">
                            <h3 style="color: White; margin-top: 2px;">Balance Sheet Group Master
                            </h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label5" ForeColor="White" Text="Group Code:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtGroupCode" Height="24px" Width="80px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label6" ForeColor="White" Text="Group Name:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtGroupName" Height="24px" Width="200px"></asp:TextBox><asp:Label
                                runat="server" ID="lblGrr" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label8" ForeColor="White" Text="Group Summary:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpGroupSummary" runat="server" CssClass="ddl" Width="100px"
                                TabIndex="48" Height="24px" OnSelectedIndexChanged="drpGroupSummary_SelectedIndexChanged">
                                <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label9" ForeColor="White" Text="Group Section:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpgroupSection" runat="server" CssClass="ddl" Width="200px"
                                TabIndex="49" Height="24px" OnSelectedIndexChanged="drpgroupSection_SelectedIndexChanged">
                                <asp:ListItem Text="Trading" Value="T" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Profit & Loss" Value="P"></asp:ListItem>
                                <asp:ListItem Text="Balance Sheet" Value="B"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label7" ForeColor="White" Text="Group Order:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtGroupOrder" Height="24px" Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button runat="server" ID="btnSaveGroup" Text="SAVE" CssClass="button" OnClick="btnSaveGroup_Click" />
                        </td>
                        <td align="center">
                            <asp:Button runat="server" ID="Button2" Text="Cancel" CssClass="button" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label runat="server" ID="lblGropCodeexist" ForeColor="Yellow"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Button runat="server" ID="btn13" Style="display: none;" />
                <ajax1:ModalPopupExtender ID="ModalGroupMaster" BackgroundCssClass="bagroundPopup"
                    TargetControlID="btn13" PopupControlID="BSGroup" runat="server" BehaviorID="mpes">
                </ajax1:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
