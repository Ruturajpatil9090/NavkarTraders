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

        var pagecount = document.getElementById("<%=hdnpagecount.ClientID %>").value;
        pagecount = parseInt(pagecount);
        if (pagecount > 1) {
            SelectedRowIndex = SelectedRowIndex + 1;
        }


        if (hdnfClosePopupValue == "txtParty") {
            document.getElementById("<%=txtParty.ClientID %>").value = grid.rows[SelectedRowIndex + 1].cells[0].innerText;
            document.getElementById("<%=lblPartyName.ClientID %>").innerText = grid.rows[SelectedRowIndex + 1].cells[1].innerText;
            document.getElementById("<%=txtParty.ClientID %>").focus();

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