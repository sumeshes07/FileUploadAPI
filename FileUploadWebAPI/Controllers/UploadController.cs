using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FileUploadWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();

                if (file.Length > 0)
                {
                    using (var client = new HttpClient())
                    {

                        client.BaseAddress = new Uri("http://localhost:5000/");

                        byte[] data;
                        using (var br = new BinaryReader(file.OpenReadStream()))
                            data = br.ReadBytes((int)file.OpenReadStream().Length);

                        ByteArrayContent bytes = new ByteArrayContent(data);
                        MultipartFormDataContent multiContent = new MultipartFormDataContent
                        {
                            { bytes, "file", file.FileName }
                        };
                        var result = client.PostAsync("api/Values", multiContent).Result;
                        return Ok();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
