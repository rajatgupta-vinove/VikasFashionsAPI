namespace VikasFashionsAPI.Common
{
    public class ImageUploadService:IImageUploadService
    {
        private readonly ILogger<ImageUploadService> _log;
        public ImageUploadService(IConfiguration config, ILogger<ImageUploadService> log)
        {
            _log = log;
        }
        public async Task<String> UploadImage(IFormFile PostImage , string[] allowedImageType ,  int postimagemaxlength, int postimageminlength, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            var AllowedImageType = new[] { "JPG", "PNG", "JPEG" };
            var ImageExt = Path.GetExtension(PostImage.FileName).Substring(1);
            var ImageName = Path.GetFileNameWithoutExtension(PostImage.FileName);
            var Postimageminlength = postimageminlength;
            var Postimagemaxlength = postimagemaxlength;
            var Timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string message = "Failed to save file!";
            string folderPath = hostingEnvironment.WebRootPath + "\\UploadedImage/UserImage\\";
            string fileName = Guid.NewGuid().ToString() + "_" + ImageName + "_" + Timestamp + "." + ImageExt;
            try
            {
                if (AllowedImageType.Any(m => m.ToLower().Equals(ImageExt.ToLower())))
                {
                    if (PostImage.Length >= Postimageminlength * 1024 * 8)
                    {
                        if (PostImage.Length <= Postimagemaxlength * 1024 * 8)
                        {
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            using (FileStream fileStream = System.IO.File.Create(folderPath + fileName))
                            {
                                PostImage.CopyTo(fileStream);
                                fileStream.Flush();
                                message = fileName;
                            }
                        }
                        else
                        {
                            message = $"Allowed max file size is {Postimagemaxlength } kb!";
                        }
                    }
                    else
                    {
                        message = $"Allowed min file size is {Postimageminlength} kb!";
                    }
                }
                else
                {
                    message = $"This file type is not a valid file type, File must be any of {String.Join(", ", AllowedImageType)}";
                }
            }
            catch (Exception ex)
            {
                _log.LogError("Error while Uploading Image", ex);
            }

            return message;

        }
    }
}
