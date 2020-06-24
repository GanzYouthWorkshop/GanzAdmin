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
				embed:
				{
					class: Embed,
					inlineToolbar: itb,
					config:
					{
						youtube: true,
						coub: true,
						falstad:
						{
							regex: /http.*(falstad).*/,
							embedUrl: 'https://www.falstad.com/circuit/circuitjs.html<%= remote_id %>',
							html: "<iframe height='300' scrolling='no' frameborder='no' allowtransparency='true' allowfullscreen='true' style='width: 100%;'></iframe>",
							height: 400,
							width: 800,
							id: (groups) => groups.join('')
						},

						codepen:
						{
							regex: /https?:\/\/codepen.lol\/([^\/\?\&]*)\/pen\/([^\/\?\&]*)/,
							embedUrl: 'https://codepen.io/<%= remote_id %>?height=300&theme-id=0&default-tab=css,result&embed-version=2',
							html: "<iframe height='300' scrolling='no' frameborder='no' allowtransparency='true' allowfullscreen='true' style='width: 100%;'></iframe>",
							height: 300,
							width: 600,
							id: (groups) => groups.join('/embed/')
						},
					}
				},
				quote: { class: Quote, inlineToolbar: itb },
				marker: { class: Marker, inlineToolbar: itb },
				warning: { class: Warning, inlineToolbar: itb },
				personality: { class: Personality, inlineToolbar: itb },
			},
		})	
	}
}

function createCmEditor(el, language)
{
	var editor = CodeMirror.fromTextArea(el, {
		lineNumbers: true,
		mode: language,
		gutters: ["CodeMirror-lint-markers"],
		lint: true,
	});

	editor.on('change', function (cMirror)
	{
		el.value = cMirror.getValue();

		if ("createEvent" in document)
		{
			var evt = document.createEvent("HTMLEvents");
			evt.initEvent("change", false, true);
			el.dispatchEvent(evt);
		}
		else
		{
			el.fireEvent("onchange");
		}
	});
}

function setScriptedPageHeight(el)
{
	if (el)
	{
		el.height = "";
		el.height = el.contentWindow.document.body.scrollHeight + "px";
	} 
}