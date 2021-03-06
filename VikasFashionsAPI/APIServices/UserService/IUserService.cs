namespace VikasFashionsAPI.APIServices.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync(string? keyword);
        Task<User?> GetByIdAsync(int userId);
        Task<User?> GetByUserCodeAsync(string userName);
        Task<User?> GetByEmailAsync(string email);
        Task<User> AddUserAsync(User user);
        Task<User?> UpdateUserAsync(User user);
        Task<bool> ChangeUserStatusAsync(int userId , int updatedBy , DateTime updatedOn);
        Task<bool> CheckUserStatusAsync(int userId, string userCode);
        Task<bool> ChangeUserPassAsync(User user);
        User? GetLoginUser();
    }
}
