using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BunnyUpload.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("FILE_NOT_FOUND");
                }

                // Replace the following values with your FTP server details
                string ftpHost = "sg.storage.bunnycdn.com";
                string ftpUsername = "bunnystoragemn";
                string ftpPassword = "75da06fd-b133-4a88-8d659c61939a-9db7-43f4";
                string ftpRemotePath = "any";

                // Get the full FTP path
                string ftpFullPath = $"ftp://{ftpHost}/{ftpRemotePath}/{file.FileName}";

                // Create the FTP request
                if (WebRequest.Create(ftpFullPath) is not FtpWebRequest ftpRequest)
                {
                    return StatusCode(500, "FTP_REQUEST_NULL");
                }

                ftpRequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.UseBinary = true;

                // Copy the file content to the FTP server
                using (Stream ftpStream = await ftpRequest.GetRequestStreamAsync())
                using (Stream fileStream = file.OpenReadStream())
                {
                    await fileStream.CopyToAsync(ftpStream);
                }

                // Optionally, you can return the URL of the uploaded file
                return Ok(new { url = $"https://bunnystoragemn.b-cdn.net/any/{file.FileName}" });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
