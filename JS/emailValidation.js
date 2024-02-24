function CheckEmail() {
    var value = document.getElementById("txtEmail").value;
    var atPosition = value.indexOf("@");
    var dotPosition = value.lastIndexOf(".");

    if (atPosition < 1 || dotPosition < atPosition + 2 || dotPosition + 2 >= value.length) {
        alert("Please Enter Valid Email Address!");
        document.getElementById("txtEmail").focus();
        return false;
    }
    else {
        return true;
    }
}