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

function createEditorJS(initialData, callbackObj)
{
	if ($('#editorjs').length > 0)
	{
		var obj = null;
		try
		{
			obj = JSON.parse(initialData);
		}
		catch(ex)
		{
			console.log("Valami nemigazán sikerült a JSON.parse-ban! " + ex);
		}
		const editor = new EditorJS(
		{
			holder: 'editorjs',
			placeholder: 'Tartalom',
			//autofocus: true,

			data: obj,
			onChange: () =>
			{
				editor.save().then((outputData) =>
				{
					callbackObj.invokeMethodAsync('OnJsChanged', JSON.stringify(outputData));
				})
			},

			tools:
			{
				header: { class: Header, inlineToolbar: true },
				list: { class: List, inlineToolbar: true },
				checklist: { class: Checklist, inlineToolbar: true },
				image: ImageTool,
				delimiter: Delimiter,
				table: { class: Table, inlineToolbar: true },
				code: { class: CodeTool, inlineToolbar: true },
				raw: { class: RawTool, inlineToolbar: true },
				link: { class: LinkTool, inlineToolbar: true },
				quote: { class: Quote, inlineToolbar: true },
				marker: { class: Marker, inlineToolbar: true },
				warning: { class: Warning, inlineToolbar: true },
				personality: { class: Personality, inlineToolbar: true },
				embed: { class: Embed, inlineToolbar: true },

				paragraph: { class: Paragraph, inlineToolbar: true },
			},
		})	
	}
}

