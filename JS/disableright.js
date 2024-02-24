document.onmousedown = disableclick;
status = "This Function Not Allowed";
function disableclick(event) {
    if (event.button == 2) {
        alert(status);
        return false;
    }
}