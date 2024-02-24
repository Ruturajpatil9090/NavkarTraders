/// <reference path="../JS/jquery-2.1.3.js" />

function SelectFirstRow(event) {
    var e = e ? e : window.event;
    var KeyCode = e.which ? e.which : e.keyCode;
    if (KeyCode == 40) {
        var $row = $('.select tr').eq(1);
        var eve = $($row).attr('onclick');
        if (eve == undefined) {
            $row = $('.select tr').eq(2);
            eve = $($row).attr('onclick');
            if (eve == undefined) {
                $row = $('.select tr').eq(3);
            }
        }
        $($row).click();
    }
}

