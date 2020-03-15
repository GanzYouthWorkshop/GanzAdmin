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

	alertify.init();

	alertify.alert("A GanzAdmin még erősen fejlesztés alatt áll! Hogy pontosan mik a tervek és most éppen hogy áll a projekt, a Súgóban tudod megnézni!");
}

function onBlazorRender()
{
	setupDataGrid();

	$('form .dtp').each(function (i)
	{
		$(this).datetimepicker(
		{
				format: 'Y. m. d. H:i'
		});
	});
}

