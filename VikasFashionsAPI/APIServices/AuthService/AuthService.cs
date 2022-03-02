using System.Security.Claims;

namespace VikasFashionsAPI.APIServices.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        public string GetLoginUserName()
        {
            string userName = string.Empty; 
            if(_httpContextAccessor.HttpContext != null)
            {
                userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return userName;
        }
    }
}
