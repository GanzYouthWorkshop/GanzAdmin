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

		var itb = ['bold', 'italic', 'link'];
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
				paragraph: { class: Paragraph, inlineToolbar: itb },
				header: { class: Header, inlineToolbar: itb },
				list: { class: List, inlineToolbar: itb },
				checklist: { class: Checklist, inlineToolbar: itb },
				image: ImageTool,
				delimiter: Delimiter,
				table: { class: Table, inlineToolbar: itb },
				code: { class: CodeTool, inlineToolbar: itb },
				raw: { class: RawTool, inlineToolbar: itb },
				url:
				{
					class: LinkTool,
					inlineToolbar: itb,
					config:
					{
						endpoint: './api_linktranslation'
					}
				},
				embed: { class: Embed, inlineToolbar: itb },
				quote: { class: Quote, inlineToolbar: itb },
				marker: { class: Marker, inlineToolbar: itb },
				warning: { class: Warning, inlineToolbar: itb },
				personality: { class: Personality, inlineToolbar: itb },
			},
		})	
	}
}

