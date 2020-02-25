window.Blazor =
{
    createCookie: function (name, value, expireDays)
    {
        var expires = "";
        if (expireDays)
        {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        }

        document.cookie = name + "=" + value + expires + "; path=/";
    },

    deleteCookie: function(name)
    {
        document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    }
}