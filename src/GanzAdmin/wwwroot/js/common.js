function onBlazorReady()
{
	$('header nav #menu').click(function (e)
	{
		e.stopPropagation();
		$('header nav ul').toggle();
	});

	$('header, main, aside').click(function()
	{
		$('header nav > ul').hide();
	});
}

function onBlazorRender()
{
	setupDataGrid();
}