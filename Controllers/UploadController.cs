using BunnyUpload.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunnyUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public UploadResult Upload()
        {
            return new UploadResult
            {
                Url = "Hello World!"
            };
        }
    }
}
