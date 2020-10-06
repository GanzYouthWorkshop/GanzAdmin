using GanzAdmin.Configuration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanzAdmin.API.NVR
{
    public class VideoStreamRouter : Controller
    {
        [HttpGet]
        [Route("content/nvr/{filename}")]
        public ContentResult CustomRouting(string filename)
        {
            string mime = filename.Contains("m3u8") ? "application/x-mpegURL" : "video/mp2t";
            string content = System.IO.File.ReadAllText($"{GanzAdminConfiguration.Instance.NvrFolder}\\{filename}");
            return Content(content, mime, Encoding.UTF8);
        }
    }
}
