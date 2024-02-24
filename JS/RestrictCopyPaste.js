function noCopyMouse(e) {
    var isRight = (e.button) ? (e.button == 2) : (e.which == 3);


    if (isRight) {
        alert('You are prompted to type this twice for a reason!');
        return false;
    }
    return true;
}


function noCopyKey(e) {
    var forbiddenKeys = new Array('c', 'x', 'v');
    var keyCode = (e.keyCode) ? e.keyCode : e.which;
    var isCtrl;


    if (window.event)
        isCtrl = e.ctrlKey
    else
        isCtrl = (window.Event) ? ((e.modifiers & Event.CTRL_MASK) == Event.CTRL_MASK) : false;


    if (isCtrl) {
        for (i = 0; i < forbiddenKeys.length; i++) {
            if (forbiddenKeys[i] == String.fromCharCode(keyCode).toLowerCase()) {
                alert('You are prompted to type this twice for a reason!');
                return false;
            }
        }
    }
    return true;
}