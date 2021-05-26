using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost]
        public async Task Post()
        {
            if (Request.HasFormContentType)
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
            }
        }
    }
}
