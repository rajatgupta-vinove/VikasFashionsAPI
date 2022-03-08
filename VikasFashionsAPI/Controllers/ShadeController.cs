using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.ShadeService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShadeController : ControllerBase
    {
        private readonly ILogger<ShadeController> _logger;
        private readonly IShadeService _shadeService;
        private readonly IUserService _userService;

        public ShadeController(ILogger<ShadeController> logger, IShadeService shadeService , IUserService userService)
        {
            _logger = logger;
            _shadeService = shadeService;
            _userService = userService;
        }

        [HttpGet(Name = "GetShade")]
        public async Task<ActionResult<List<Shade>>> Get()
        {
            return Ok(await _shadeService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetShadeById")]
        public async Task<ActionResult<Shade>> Get(int id)
        {
            return Ok(await _shadeService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteShadeById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _shadeService.DeleteShadeAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateShade")]
        public async Task<ActionResult<Shade>> Update(int id, Shade shade)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                shade.UpdatedBy = user.UserId;
                shade.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _shadeService.UpdateShadeAsync(shade));
        }
        [HttpPost(Name = "CreateShade")]
        public async Task<ActionResult<Shade>> Create(Shade shade)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                shade.CreatedBy = user.UserId;
                shade.CreatedOn = CommonVars.CurrentDateTime;
                shade.UpdatedBy = user.UserId;
                shade.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _shadeService.AddShadeAsync(shade));
        }

        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<Shade>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var shade = await _shadeService.GetByIdAsync(id)
;
            if (shade != null)
            {
                if (user != null)
                {
                    shade.UpdatedBy = user.UserId;
                    shade.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest("No such shade found");
            }
            return Ok(await _shadeService.ChangeShadeStatusAsync(id, shade.UpdatedBy, shade.UpdatedOn));
        }
    }
}
