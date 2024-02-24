
function selectAll(invoker) {
    var inputElements = document.getElementsByTagName('input');
    for (var i = 0; i < inputElements.length; i++) {
        var myElement = inputElements[i];
        if (myElement.type === "checkbox") {
            myElement.checked = invoker.checked;
        }
    }
} 
