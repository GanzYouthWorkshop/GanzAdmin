function motionjpeg(el)
{
    console.log("mjpeg");
    var image = $(el);
    var src;

    if (!image.length)
    {
        return;
    }

    src = image.attr("src");
    if (src.indexOf("?") < 0)
    {
        image.attr("src", src + "?a=a"); // must have querystring
    }

    if (src.indexOf("&d=") < 0)
    {
        image.attr("src", src + "&d=0"); // must have querystring
    }

    image.on("load", function ()
    {
        // this cause the load event to be called "recursively"
        // 'this' refers to the image
        if (typeof (this) != 'undefined' && this != null)
        {
            this.src = this.src.replace(/&d=\d+/, "&d=") + (new Date()).getTime();
        }
    });
}
