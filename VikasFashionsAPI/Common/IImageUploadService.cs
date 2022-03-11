namespace VikasFashionsAPI.Common
{
    public interface IImageUploadService
    {

        Task<String> UploadImage(IFormFile PostImage, string[] allowedImageType, int postimagemaxlength, int postimageminlength, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment);
    }
}
