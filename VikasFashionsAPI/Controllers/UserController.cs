using VikasFashionsAPI.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _counfiguration;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IConfiguration counfiguration, IUserService userService)
        {
            _logger = logger;
            _counfiguration = counfiguration;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<User>> Create(UserRegister loginUser)
        {
            var checkUser = await _userService.GetByEmailAsync(loginUser.Email);
            if (checkUser != null)
                return BadRequest("User with this email already exists");
            checkUser = await _userService.GetByUserNameAsync(loginUser.UserName);
            if (checkUser != null)
                return BadRequest("User with this user name already exists");
            DateTime dateTime = DateTime.Now;
            CreatePasswordHash(loginUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User
            {
                Name = loginUser.Name,
                Email = loginUser.Email,
                Phone = loginUser.Phone,
                UserName = loginUser.UserName,
                Password = loginUser.Password,
                RoleId = loginUser.RoleId,
                IsActive = loginUser.IsActive,
                Remark = loginUser.Remark,
                CompanyId = loginUser.CompanyId,
                CompanyUserType = loginUser.CompanyUserType,
                BannerImage = loginUser.BannerImage,
                ProfileImage = loginUser.ProfileImage,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedBy = 0,
                CreatedOn = CommonVars.CurrentDateTime,
                UpdatedBy = 0,
                UpdatedOn = CommonVars.CurrentDateTime,
            };
            await _userService.AddUserAsync(user);
            return Ok(user);
        }
        [HttpGet(Name = "Getusers")]
        public async Task<ActionResult<User>> Get()
        {
            throw new NotImplementedException("No method implemented yet");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<string>> Login(UserLogin userLogin)
        {
            if (userLogin == null)
                return BadRequest("Invalid login details");
            var user = await _userService.GetByEmailAsync(userLogin.Email);
            if (user == null)
                return NotFound("User Not Found!");
            if (!VerifyPasswordHash(userLogin.Password, user.PasswordSalt, user.PasswordHash))
                return BadRequest("Wrong Credentails");
            string token = CreateJWTToken(user);
            return Ok(token);
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        private bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return passwordHash.SequenceEqual(computedHash);
            }

        }
        private string CreateJWTToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "Admin"),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_counfiguration.GetSection("AppSettings:JWTKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(365),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
