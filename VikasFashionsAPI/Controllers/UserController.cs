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
using VikasFashionsAPI.Common;

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
                return BadRequest(
                    new ResponseGlobal()
                    {
                        ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest),
                        Message = Common.CommonVars.MessageResults.UserDuplicateEmail.GetEnumDisplayName()
                    });
            checkUser = await _userService.GetByUserNameAsync(loginUser.UserCode);
            if (checkUser != null)
                return BadRequest(
                    new ResponseGlobal()
                    {
                        ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest),
                        Message = Common.CommonVars.MessageResults.UserDuplicateCode.GetEnumDisplayName()
                    });
            CreatePasswordHash(loginUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User
            {
                Name = loginUser.Name,
                Email = loginUser.Email,
                Phone = loginUser.Phone,
                UserCode = loginUser.UserCode,
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
            var result = await _userService.AddUserAsync(user);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(),
                    Data = result
                });
        }

        [HttpGet(Name = "GetUsers/{keyword?}")]
        public async Task<ActionResult<List<User>>> Get(string? keyword)
        {
            _logger.LogInformation($"Get users called with keyword {keyword}");
            var result = await _userService.GetAllAsync(keyword);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }

        [HttpGet]
        [Route("CheckUserExists")]
        public async Task<ActionResult<bool>> CheckUserExists([FromQuery] int id, [FromQuery] string code)
        {
            _logger.LogInformation($"Check user existance called with id: {id}, code: {code}");
            var result = await _userService.CheckUserStatusAsync(id, code);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var result = await _userService.GetByIdAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<string>> Login(UserLogin userLogin)
        {
            if (userLogin == null)
                return BadRequest(
                    new ResponseGlobal()
                    {
                        ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest),
                        Message = Common.CommonVars.MessageResults.InvalidLogin.GetEnumDisplayName()
                    });
            var user = await _userService.GetByEmailAsync(userLogin.Email);
            if (user == null)
                return BadRequest(
                    new ResponseGlobal()
                    {
                        ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest),
                        Message = Common.CommonVars.MessageResults.InvalidLogin.GetEnumDisplayName()
                    });
            if (!VerifyPasswordHash(userLogin.Password, user.PasswordSalt, user.PasswordHash))
                return BadRequest(
                    new ResponseGlobal()
                    {
                        ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest),
                        Message = Common.CommonVars.MessageResults.InvalidLogin.GetEnumDisplayName()
                    });
            string token = CreateJWTToken(user);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = token
                });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<ActionResult<string>> ResetPassword(UserResetPassword userResetPassword)
        {
            if (userResetPassword == null)
                return BadRequest(
                    new ResponseGlobal()
                    {
                        ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest),
                        Message = Common.CommonVars.MessageResults.ErrorGet.GetEnumDisplayName()
                    });
            var user = await _userService.GetByIdAsync(userResetPassword.UserId);
            if (user == null)
                return BadRequest(
                    new ResponseGlobal()
                    {
                        ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest),
                        Message = Common.CommonVars.MessageResults.ErrorGet.GetEnumDisplayName()
                    });
            CreatePasswordHash(userResetPassword.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.Password = userResetPassword.NewPassword;
            user.PasswordSalt = passwordHash;
            user.PasswordHash = passwordHash;
            user.UpdatedBy = userResetPassword.UserId;
            user.UpdatedOn = CommonVars.CurrentDateTime;
            var result = await _userService.ChangeUserPassAsync(user);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
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
                new Claim(ClaimTypes.NameIdentifier, user.UserCode),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Role, "Admin"),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_counfiguration.GetSection("AppSettings:JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                issuer: _counfiguration.GetSection("AppSettings:JWT:Issuer").Value,
                audience: _counfiguration.GetSection("AppSettings:JWT:Audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(365),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<User>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var userObj = await _userService.GetByIdAsync(id);
            if (userObj != null)
            {
                if (user != null)
                {
                    userObj.UpdatedBy = user.UserId;
                    userObj.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.RecordNotFound.GetEnumDisplayName() });
            }
            var result = await _userService.ChangeUserStatusAsync(id, userObj.UpdatedBy, userObj.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });

        }
    }
}
