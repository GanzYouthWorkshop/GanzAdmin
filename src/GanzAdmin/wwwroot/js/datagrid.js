function setupDataGrid()
{
	$('main').delegate('#data th i.collapse', 'click', function()
	{
		var nth = $('th').index($(this).parent()) + 1;
			
		$(this).parent().hasClass('collapsed') ? $(this).html('') : $(this).html('');

		$(this).parent().toggleClass('collapsed');
		$('#data td:nth-child('+nth+')').toggleClass('collapsed');
	});
		
	$('main').delegate('#data th i.sort', 'click', function()
	{
		var direction = false;
		if($(this).parent().hasClass('sorting-asc'))
		{
			direction = false;
			$(this).parent().removeClass('sorting-asc');
			$(this).parent().addClass('sorting-desc');
			$(this).html('');
		}
		else if($(this).parent().hasClass('sorting-desc'))
		{
			direction = true;
			$(this).parent().removeClass('sorting-desc');
			$(this).parent().addClass('sorting-asc');
			$(this).html('');
		}
		else
		{
			direction = true;
			$('#data th').removeClass('sorting-asc');
			$('#data th').removeClass('sorting-desc');
			$('#data th i.sort').html('');
			$(this).html('');
			$(this).parent().addClass('sorting-asc');
		}
			
		var nth = $('th').index($(this).parent());
		sortTable('#data', nth, direction);
	});
		
	$('main').delegate('#data th i.pin', 'click', function()
	{
		var nth = $('th').index($(this).parent()) + 1;

		var rot = $(this).parent().hasClass('pinned') ? 0 : 90;
		console.log(rot);
		$(this).css('transform', 'rotate(' + rot + 'deg)');

		$(this).parent().toggleClass('pinned');
		$('#data > tbody > tr > td:nth-child(' + nth + ')').toggleClass('pinned');
	});
		
	$('main').delegate('#data input[type=checkbox]', 'click', function()
	{
		var command = ($(this).parent().parent().hasClass('selected') ? 'un' : '')+'selectpart';
		decodeLinkCommand(command+':'+$(this).val());
		$(this).parent().parent().toggleClass('selected');
	});
}

function sortTable(selector, column, direction)
{
	var rows = $(selector + ' > tbody > tr').get();
	var dir = direction ? 1 : -1;

	rows.sort(function (a, b)
	{
		var A = $(a).children('td').eq(column).text().toUpperCase();
		var B = $(b).children('td').eq(column).text().toUpperCase();

		if (dir == 1) {
			if (A == '' || A == '-') A = 'ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ';
			if (B == '' || B == '-') B = 'ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ';
		}
		if (dir == -1) {
			if (A == '' || A == '-') A = '';
			if (B == '' || B == '-') B = '';
		}

		return dir * A.localeCompare(B);
	});

	$.each(rows, function (index, row)
	{
		$(selector).children('tbody').append(row);
	});
}
