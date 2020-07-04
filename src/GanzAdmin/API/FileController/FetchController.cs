using GanzAdmin.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GanzAdmin.API.FileController
{
    [Route("api/fetch-url")]
    [ApiController]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class FethController : Controller
    {
        public class FetchUrlModel
        {
            public string url { get; set; }
        }

        private IWebHostEnvironment environment;
        private IHttpContextAccessor context;

        public FethController(IWebHostEnvironment environment, IHttpContextAccessor contextAccessor)
        {
            this.environment = environment;
            this.context = contextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> FetchFiles(FetchUrlModel model)
        {
            try
            {
                var t = HttpContext.Request.Form;
                string result = "";

                result = $"{{ \"success\" : 1, \"file\": {{ \"url\" : \"{model.url}\" }} }}";

                return this.StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }
    }
}
