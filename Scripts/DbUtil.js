function setQuery(el)
{
	document.getElementById("TxtQuery").value = el.childNodes[0].nodeValue
	document.getElementById("TxtQuery").focus()
}

function showPanel(pos)
{
	document.getElementById("divHistoryQueries").style.display = ((pos == 1) ? "block" : "none")
	document.getElementById("divFavQueries").style.display = ((pos == 2) ? "block" : "none")
	document.getElementById("aHist").style.fontWeight = ((pos == 1) ? "bold" : "normal")
	document.getElementById("aFav").style.fontWeight = ((pos == 2) ? "bold" : "normal")
}

function setDB(which)
{
	var rblArr = document.getElementsByName("RblWhichDb")
	for (i=0; i<rblArr.length; i++)
	{
		if (rblArr[i].value == which)
		{
			rblArr[i].checked = true;
			break;
		}
	}
}

var schemaPopup = null
var prevPopupUrl = ''
function showSchema(db)
{
	var popupUrl = 'SqlSchema.aspx?DB=' + db
	try
	{
		schemaPopup.focus()
		
		// don't reload same schema if already open
		if (prevPopupUrl.indexOf(popupUrl) == -1)
		{
			schemaPopup.window.document.body.innerHTML = "Loading..."
			schemaPopup.window.location.href = popupUrl
			prevPopupUrl = popupUrl
		}
	}
	catch (err)
	{
		schemaPopup = window.open(popupUrl, '', 'width=800, height=600, scrollbars=1, resizable=1, status=1')
		prevPopupUrl = popupUrl
	}
}

function openFullHistory()
{
	window.open('SqlHistory.aspx', 'DbUtilHistory', 'width=800, height=600, scrollbars=1, resizable=1, status=1')
}

var prevDiv = null;
var prevColor = null;
function selectRow(div)
{
	if (prevDiv != null)
		prevDiv.style.backgroundColor = prevColor
	prevDiv = div
	prevColor = div.style.backgroundColor
	div.style.backgroundColor = "#ffeeee"
}

function toggleWrap(checked)
{
	if (window.navigator.userAgent.indexOf("Gecko") != -1)
	{
		// workaround for Firefox because setting 'wrap' attribute just doesn't work :(
		var val = document.getElementById("TxtQuery").value
		var html = document.getElementById("TxtQuery").parentNode.innerHTML
		html = html.replace('wrap="off"', '')
		if (!checked)
			html = html.substring(0, html.indexOf('<textarea') + 9) + ' wrap="off" ' + html.substring(html.indexOf('<textarea') + 9)
		document.getElementById("TxtQuery").parentNode.innerHTML = html
		document.getElementById("TxtQuery").value = val
	}
	else
		document.getElementById('TxtQuery').wrap = (checked) ? 'soft' : 'off'
}
