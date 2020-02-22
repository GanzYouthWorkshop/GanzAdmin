Array.prototype.select = function(expr)
{
	var arr = this;
	
	switch(typeof expr)
	{
		case 'function':
			return $.map(arr, expr);
			break;

		case 'string':

			try
			{
				var func = new Function(expr.split('.')[0], 'return ' + expr + ';');
				return $.map(arr, func);
			}
			catch(e)
			{
				return null;
			}
			break;

		default:
			throw new ReferenceError('func must be a function or string!');
    }
};

Array.prototype.where = function (filter)
{
	var collection = this;
	
	switch(typeof filter)
	{ 
		case 'function': 
			return $.grep(collection, filter); 

        case 'object':
			for(var property in filter)
			{
				if(!filter.hasOwnProperty(property)) 
				{
					continue;
				}
				
				collection = $.grep(collection, function (item) 
				{
					return item[property] === filter[property];
				});
			}
			return collection.slice(0);

		default: 
			throw new TypeError('func must be a function or object!'); 
	}
};

Array.prototype.firstOrDefault = function(func)
{
	return this.where(func)[0] || null;
};