
/// <reference path="../JS/jquery-2.1.3.js" />

function closepopup(event) {
    var e = e ? e : window.event;
    var KeyCode = e.which ? e.which : e.keyCode;
    if (KeyCode == 27) {
        var hdnfClosePopupValue = document.getElementById("ContentPlaceHolder1_hdnfClosePopup").value;
        $('#ContentPlaceHolder1_pnlPopup').css('display', 'none');
        $('#ContentPlaceHolder1_txtSearchText').attr('value', '');
        $('#ContentPlaceHolder1_' + hdnfClosePopupValue).focus();
        $('#ContentPlaceHolder1_hdnfClosePopup').val('Close');
    }
    if (KeyCode == 9) {
        e.preventDefault();
    }
}