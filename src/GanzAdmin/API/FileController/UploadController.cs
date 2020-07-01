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
    [Route("api/upload")]
    [ApiController]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment environment;

        public UploadController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(IFormFile[] files, string currentDirectory)
        {
            try
            {
                string result = "";

                if (HttpContext.Request.Form.Files.Any())
                {
                    foreach (var file in HttpContext.Request.Form.Files)
                    {
                        string requestedPath = "\\";

                        if (currentDirectory != null)
                        {
                        }

                        string filename = DateTime.Now.ToString("yyMMdd_hhmm") + "_" + file.FileName;
                        string path = GanzUtils.ProperPathCombine('\\', environment.WebRootPath, "content\\uploads", requestedPath, filename);

                        using (FileStream stream =  new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        result = $"{{ \"success\" : 1, \"file\": {{ \"url\" : \"./content/uploads/{requestedPath}/{filename}\" }} }}";
                    }

                    return this.StatusCode(200, result);
                }
                else
                {

                }

                return this.StatusCode(404);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }
    }
}
