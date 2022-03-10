using System.Security.Claims;

namespace VikasFashionsAPI.APIServices.UserService
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _log;
        private readonly IConfiguration _config;
        private readonly VikasFashionsAPI.Data.DataContextVikasFashion _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IConfiguration config, ILogger<UserService> log, DataContextVikasFashion context, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _log = log;
            _context = context;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> AddUserAsync(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException("AddUser");
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while adding user", ex);
            }
            return user;
        }

        public async Task<bool> ChangeUserStatusAsync(int userId , int updatedBy , DateTime updatedOn)
        {
            bool isDeleted = false;
            try
            {
                if (userId == 0)
                    return isDeleted;
                var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == userId);
                if (user == null)
                    return isDeleted;
                user.IsActive = !user.IsActive;
                user.UpdatedBy = updatedBy;
                user.UpdatedOn = updatedOn;
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while changing status of user", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<User>> GetAllAsync(string? keyword)
        {
            _log.LogInformation("User GetAll Called!");
            if (!string.IsNullOrEmpty(keyword))
            {
                return await _context.Users.Where(u => !string.IsNullOrEmpty(keyword) && (u.UserCode.ToLower().Contains(keyword) || u.Name.ToLower().Contains(keyword))).ToListAsync();
            }
            else
            {
                return await _context.Users.ToListAsync();
            }
        }

        public async Task<User?> GetByIdAsync(int userId)
        {
            User? user = null;
            try
            {
                if (userId == 0)
                    return user;
                user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == userId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting user", ex);
            }
            return user;
        }
        public async Task<User?> GetByUserNameAsync(string userCode)
        {
            User? user = null;
            try
            {
                if (string.IsNullOrEmpty(userCode))
                    return user;
                user = await _context.Users.FirstOrDefaultAsync(m => m.UserCode == userCode);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting user", ex);
            }
            return user;
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            User? user = null;
            try
            {
                if (string.IsNullOrEmpty(email))
                    return user;
                user = await _context.Users.FirstOrDefaultAsync(m => m.Email == email);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting user", ex);
            }
            return user;
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            try
            {
                var exisingUser = await _context.Users.FirstOrDefaultAsync(m => m.UserId == user.UserId);
                if (exisingUser == null)
                    return null;
                exisingUser.UserId = user.UserId;
                exisingUser.UserCode = user.UserCode;
                exisingUser.UpdatedBy = user.UpdatedBy;
                exisingUser.UpdatedOn = user.UpdatedOn;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating user", ex);
            }
            return user;
        }
        public async Task<bool> ChangeUserPassAsync(User user)
        {
            bool isUpdated = false;
            try
            {
                var exisingUser = await _context.Users.FirstOrDefaultAsync(m => m.UserId == user.UserId);
                if (exisingUser == null)
                    return isUpdated;
                exisingUser.Password = user.Password;
                exisingUser.PasswordHash = user.PasswordHash;
                exisingUser.PasswordSalt = user.PasswordSalt;
                exisingUser.UpdatedBy = user.UserId;
                exisingUser.UpdatedOn = user.UpdatedOn;

                await _context.SaveChangesAsync();
                isUpdated = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating user", ex);
            }
            return isUpdated;
        }

        public User? GetLoginUser()
        {
            string email = string.Empty;
            User? user = null;
            if (_httpContextAccessor.HttpContext != null)
            {
                email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                user = _context.Users.FirstOrDefault(m => m.Email == email);
            }
            return user;
        }

        public async Task<bool> CheckUserStatusAsync(int userId, string userCode)
        {
            bool isExists = false;
            try
            {
                if (string.IsNullOrEmpty(userCode))
                    return isExists;
                isExists = await _context.Users.AnyAsync(m => m.UserCode == userCode && m.UserId != userId);
            }
            catch (Exception ex)
            {

                _log.LogError("Error while getting user", ex);
            }
            return isExists;
        }
    }
}
