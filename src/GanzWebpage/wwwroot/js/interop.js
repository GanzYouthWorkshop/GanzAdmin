function setScriptedPageHeight(el)
{
	if (el)
	{
		el.height = "";
		el.height = el.contentWindow.document.body.scrollHeight + "px";
	}
}
