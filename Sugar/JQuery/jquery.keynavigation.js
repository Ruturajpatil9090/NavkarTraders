//Author: Manjunath 
//Date: 16-Sep-2010

jQuery.keynavigation = function(table) {
    var rowColorStyle = "BACKGROUND-COLOR: white; COLOR: #003399";
    var rowHighlightColor = "#aaa";


    table.find("input[type='text'],input[type='checkbox'], select").keydown(
    function(event) {
        //For nvaigating using right key
        if ((event.keyCode == 13) || (event.keyCode == 9 && event.shiftKey == false)) {
        
            var inputs = $(this).parents("table").eq(0).find("input[type='text'],input[type='checkbox'], select");
            var idx = inputs.index(this);
            if (idx == inputs.length - 1) {

                inputs[0].select()
            } else {
                $(this).parents("table").eq(0).find("tr").not(':first').each(function() {

                    $(this).attr("style", rowColorStyle);
                });
                inputs[idx + 1].parentNode.parentNode.style.backgroundColor = rowHighlightColor;
                inputs[idx + 1].focus();
                inputs[idx].value = inputs[idx].value;
                if (inputs[idx + 1].type != "select-one") {
                    inputs[idx + 1].select();
                }
            }
            event.preventDefault();
        }

        //For nvaigating using left key
        if (((event.keyCode == 37 && event.ctrlKey == true)) || (event.keyCode == 9 && event.shiftKey == true)) {
            var inputs = $(this).parents("table").eq(0).find("input[type='text'],input[type='checkbox'], select");
            var idx = inputs.index(this);
            if (idx > 0) {
                $(this).parents("table").eq(0).find("tr").not(':first').each(function() {
                    $(this).attr("style", rowColorStyle);
                });
                inputs[idx - 1].parentNode.parentNode.style.backgroundColor = rowHighlightColor;

                inputs[idx - 1].focus();
                inputs[idx].value = inputs[idx].value;
                if (inputs[idx - 1].type != "select-one")
                { inputs[idx - 1].select(); }
            }
            event.preventDefault();
        }
    });

    //For navigating using up and down arrow of the keyboard
    table.find("input[type='text'],input[type='checkbox']").keydown(
    function(event) {
        if ((event.keyCode == 40)) {
            if ($(this).parents("tr").next() != null) {
                var nextTr = $(this).parents("tr").next();
                var inputs = $(this).parents("tr").eq(0).find("input[type='text'],input[type='checkbox']");
                var idx = inputs.index(this);
                nextTrinputs = nextTr.find("input[type='text'], input[type='checkbox']");
                if (nextTrinputs[idx] != null) {
                    $(this).parents("table").eq(0).find("tr").not(':first').each(function() {
                        $(this).attr("style", rowColorStyle);
                    });
                    nextTrinputs[idx].parentNode.parentNode.style.backgroundColor = rowHighlightColor;
                    nextTrinputs[idx].focus();
                    nextTrinputs[idx].value = nextTrinputs[idx].value;
                    if (nextTrinputs[idx].type != "select-one")
                    { nextTrinputs[idx].select(); }
                }
            }
            else {
                $(this).focus();
                if ($(this).type != "select-one")
                { $(this).select(); }
            }
            event.preventDefault();
        }

        if ((event.keyCode == 38)) {
            if ($(this).parents("tr").prev() != null) {
                var nextTr = $(this).parents("tr").prev();
                var inputs = $(this).parents("tr").eq(0).find("input[type='text'],input[type='checkbox']");
                var idx = inputs.index(this);
                nextTrinputs = nextTr.find("input[type='text'],input[type='checkbox']");
                if (nextTrinputs[idx] != null) {
                    $(this).parents("table").eq(0).find("tr").not(':first').each(function() {

                        $(this).attr("style", rowColorStyle);
                    });
                    nextTrinputs[idx].parentNode.parentNode.style.backgroundColor = rowHighlightColor;
                    nextTrinputs[idx].focus();
                    nextTrinputs[idx].value = nextTrinputs[idx].value;
                    if (nextTrinputs[idx].type != "select-one")
                    { nextTrinputs[idx].select(); }
                }
                return false;
            }
            else {
                $(this).focus();
                if ($(this).type != "select-one")
                { $(this).select(); }
            }
            event.preventDefault();
        }
    });

    //Navigate to the next same column in the next row when the enter key is clicked.
//    table.find("input[type='text'], input[type='checkbox'], select").keypress(
//    function(event) {
//        //When enter key clicked
//        if (event.keyCode == 13) {
//            if ($(this).parents("tr").next() != null) {
//                var nextTr = $(this).parents("tr").next();
//                var inputs = $(this).parents("tr").eq(0).find("input[type='text'], input[type='checkbox'], select");
//                var idx = inputs.index(this);
//                nextTrinputs = nextTr.find("input[type='text'], input[type='checkbox'], select");
//                if (nextTrinputs[idx] != null) {
//                    $(this).parents("table").eq(0).find("tr").not(':first').each(function() {

//                        $(this).attr("style", rowColorStyle);
//                    });
//                    nextTrinputs[idx].parentNode.parentNode.style.backgroundColor = rowHighlightColor;
//                    nextTrinputs[idx].focus();
//                    // nextTrinputs[idx].value = inputs[idx].value;
//                    if (nextTrinputs[idx].type != "select-one")
//                    { nextTrinputs[idx].select(); }
//                }
//                return false;
//            }
//            else {
//                $(this).focus();
//                if ($(this).type != "select-one")
//                { $(this).select(); }
//            }
//            event.preventDefault();
//        }
//    });

}
