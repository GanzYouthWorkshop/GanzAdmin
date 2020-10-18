function onBlazorReady()
{
	onBlazorRender();

	alertify.init();

	$('#loading-overlay').hide();
}

function onMenuReady()
{
	$('header nav #menu').click(function (e) {
		e.stopPropagation();
		$('header nav ul').toggle();
	});

	$('header, main, aside').click(function () {
		$('header nav > ul').hide();
	});
}

function onBlazorRender()
{
	setupDataGrid();

	document.querySelectorAll('code pre').forEach((block) => {
		hljs.highlightBlock(block);
	});}

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
				image:
				{
					class: ImageTool,
					config:
					{
						endpoints:
						{
							byFile: './api/upload', // Your backend file uploader endpoint
							byUrl: './api/fetch-url', // Your endpoint that provides uploading by Url
						}
					}
				},
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
						services:
						{
							youtube: true,
							coub: true,

							codepen: {
								regex: /https?:\/\/codepen.io\/([^\/\?\&]*)\/pen\/([^\/\?\&]*)/,
								embedUrl: 'https://codepen.io/<%= remote_id %>?height=300&theme-id=0&default-tab=css,result&embed-version=2',
								html: "<iframe height='300' scrolling='no' frameborder='no' allowtransparency='true' allowfullscreen='true' style='width: 100%;'></iframe>",
								height: 300,
								width: 600,
								id: (groups) => groups.join('/embed/')
							},

							falstad:
							{
								regex: /https?:\/\/(www\.)?falstad.com\/circuit\/circuitjs.html\?ctz=(.*)/,
								embedUrl: 'https://www.falstad.com/circuit/circuitjs.html?ctz=<%= remote_id %>',
								html: "<iframe height='300' scrolling='no' frameborder='no' allowtransparency='true' allowfullscreen='true' style='width: 100%;'></iframe>",
								height: 400,
								width: 800,
								id: (groups) => groups[1]
							},
						}
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
		htmlMode: true,
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

function createHlsPlayer(id, url)
{
	if (Hls.isSupported())
	{
		var video = document.getElementById(id);
		var hls = new Hls(
		{
			debug: false
		});
		hls.loadSource(url);
		hls.attachMedia(video);
		hls.on(Hls.Events.MEDIA_ATTACHED, function ()
		{
			video.muted = true;
			video.play();
		});
	}
}

function createPost(url, values)
{
	var form = document.createElement("form");
	form.target = "_blank";
	form.method = "POST";
	form.action = url;

	var json = JSON.parse(values); 
	console.log(values);
	console.log(json);
	for (i in json)
	{
		var input = document.createElement("input");
		input.type = "text";
		input.name = i;
		input.value = json[i];
		form.appendChild(input);
    }

	document.body.appendChild(form);
	form.submit();
	document.body.removeChild(form);
}

function createUploader(form)
{
	if (form)
	{
		$(form).on('drag dragstart dragend dragover dragenter dragleave drop', function (e)
		{
			e.preventDefault();
			e.stopPropagation();
		});
		
		$(form).on('dragover dragenter', function ()
		{
			$(this).parent().addClass('is-dragover');
		});
		
		$(form).on('dragleave dragend drop', function ()
		{
			$(this).parent().removeClass('is-dragover');
		});

		$(form).on('drop', function (e)
		{
			console.log(e);
			droppedFiles = e.originalEvent.dataTransfer.files;

			var jform = $(form);
			var ajaxData = new FormData(jform.get(0));

			if (droppedFiles)
			{
				var input = jform.find('input').first();
				$.each(droppedFiles, function (i, file)
				{
					ajaxData.append(input.attr('name'), file);
				});
			}

			$.ajax(
			{
				url: jform.attr('action'),
				type: jform.attr('method'),
				data: ajaxData,
				dataType: 'json',
				cache: false,
				contentType: false,
				processData: false,
				complete: function ()
				{
					jform.parent().removeClass('is-uploading');
				},
				success: function(data)
				{
					var hidden = jform.find("input[type='hidden']").first();
					hidden.val(data.file.url);

					var event = new Event('change');
					hidden[0].dispatchEvent(event);

					jform.parent().addClass(data.success == true ? 'is-success' : 'is-error');
					if (!data.success) $errorMsg.text(data.error);
				},
				error: function()
				{
					// Log the error, show an alert, whatever works for you
					alert("hiba");
				}
			});
		});

    }
}

function createDatePicker(el)
{
	$(el).datetimepicker(
	{
		onChangeDateTime: function (date, inst)
		{
			var d = new Date(Date.parse(date));
			var datestring = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds();

			el.value = datestring;


			if ("createEvent" in document)
			{
				//Ez itt valamiért nem mindig működik...
				var evt = document.createEvent("HTMLEvents");
				evt.initEvent("change", false, true);
				el.dispatchEvent(evt);
			}
			else
			{
				el.fireEvent("onchange");
			}
		},

		format: 'Y. m. d. H:i'
	});
}

function getValue(input)
{
	console.log(input);
	return input.innertHtml;
}