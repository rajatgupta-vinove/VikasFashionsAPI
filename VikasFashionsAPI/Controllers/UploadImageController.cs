using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using VikasFashionsAPI.Common;

namespace VikasFashionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        private readonly DataContextVikasFashion _context;
        private readonly ILogger<UploadImageController> _log;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        private readonly IImageUploadService _ImageUploadService;
        public UploadImageController(DataContextVikasFashion context, ILogger<UploadImageController> log ,  Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IImageUploadService ImageUploadService)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
            _log = log;
            _ImageUploadService = ImageUploadService;
        }
        [HttpPost]
        public async Task<ActionResult> UploadImage(IFormFile PostImage)
        {
            var message = "Failed to save file!";
            var AllowedImageType = new[] { "JPG", "PNG", "JPEG" };
            var Postimageminlength = 2;
            var Postimagemaxlength = 10;
            if (Postimageminlength > 0)
            {
                if (Postimagemaxlength < 10)
                {
                    message = await _ImageUploadService.UploadImage(PostImage, AllowedImageType, Postimagemaxlength, Postimageminlength, hostingEnvironment);
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.RecordNotFound.GetEnumDisplayName() });
            }
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(), Data = message });
        }
    }
}

