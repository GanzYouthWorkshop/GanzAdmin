using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GanzAdmin.API.LinkTranslation
{
    [Route("api_linktranslation")]
    [ApiController]
    public class LinkTranslationProvider : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            string url = Request.Query["url"];
            LinkTranslatorDefinitions.Result result = null;
            try
            {
                result = LinkTranslatorDefinitions.Translate(url);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return $"{{ \"success\" : 1, \"meta\": {{ \"title\" : \"{result.Title}\", \"description\" : \"{result.Description}\", \"image\" : {{ \"url\" : \"{result.ImageUrl}\"}}}}}}";
        }
    }
}
