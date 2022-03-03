﻿using VikasFashionsAPI.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> ChangeUserStatusAsync(int userId)
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
                await _context.SaveChangesAsync();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                _log.LogError("Error while changing status of user", ex);
            }
            return isDeleted;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            _log.LogInformation("User GetAll Called!");
            return await _context.Users.ToListAsync();
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
        public async Task<User?> GetByUserNameAsync(string userName)
        {
            User? user = null;
            try
            {
                if (string.IsNullOrEmpty(userName))
                    return user;
                user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == userName);
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
                exisingUser.UserName = user.UserName;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.LogError("Error while updating user", ex);
            }
            return user;
        }

        public User? GetLoginUser()
        {
            string email = string.Empty;
            User? user = null;
            if (_httpContextAccessor.HttpContext != null)
            {
                email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                user = _context.Users.FirstOrDefault(m => m.Email == email);
            }
            return user;
        }
    }
}