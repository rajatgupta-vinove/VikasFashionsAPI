using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using VikasFashionsAPI.Data;


namespace VikasFashionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _counfiguration;
        private static User _localUser = new User();

        public UserController(ILogger<UserController> logger, IConfiguration counfiguration)
        {
            _logger = logger;
            _counfiguration = counfiguration;
        }
        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<User>> Create(UserLogin user)
        {
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            _localUser.UserName = user.UserName;
            _localUser.PasswordHash = passwordHash;
            _localUser.PasswordSalt = passwordSalt;

            return Ok(_localUser);
        }
        [HttpGet(Name = "Getusers")]
        public async Task<ActionResult<User>> Get()
        {
            throw new NotImplementedException("No method implemented yet");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<string>> Login(UserLogin user)
        {
            if (user.UserName.ToLower() != _localUser.UserName.ToLower())
                return BadRequest("user not found");
            if (!VerifyPasswordHash(user.Password, _localUser.PasswordSalt, _localUser.PasswordHash))
                return BadRequest("wrong password");
            string token = CreateJWTToken(_localUser);
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
                new Claim(ClaimTypes.Name, user.UserName),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_counfiguration.GetSection("AppSettings:JWTKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
