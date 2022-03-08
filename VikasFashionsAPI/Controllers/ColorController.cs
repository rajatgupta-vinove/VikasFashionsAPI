using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.ColorService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly ILogger<ColorController> _logger;
        private readonly IColorService _colorService;
        private readonly IUserService _userService;

        public ColorController(ILogger<ColorController> logger, IColorService colorService, IUserService userService)
        {
            _logger = logger;
            _colorService = colorService;
            _userService = userService;

        }

        [HttpGet(Name = "GetColor")]
        public async Task<ActionResult<List<Color>>> Get()
        {
            var result = await _colorService.GetAllAsync();
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
        }

        [HttpGet("{id}", Name = "GetColorById")]
        public async Task<ActionResult<Color>> Get(int id)
        {
            var result = await _colorService.GetByIdAsync(id);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
        }

        [HttpDelete("{id}", Name = "DeleteColorById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _colorService.DeleteColorAsync(id);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(), Data = result });
        }

        [HttpPut("{id}", Name = "UpdateColor")]
        public async Task<ActionResult<Color>> Update(int id, Color color)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                color.UpdatedBy = user.UserId;
                color.UpdatedOn = CommonVars.CurrentDateTime;
            }
             var result = await _colorService.UpdateColorAsync(color);
             return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });
        }
        [HttpPost(Name = "CreateColor")]
        public async Task<ActionResult<Color>> Create(Color color)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                color.CreatedBy = user.UserId;
                color.CreatedOn = CommonVars.CurrentDateTime;
                color.UpdatedBy = user.UserId;
                color.UpdatedOn = CommonVars.CurrentDateTime;
            }
              var result = await _colorService.AddColorAsync(color);
              return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(), Data = result });
        }
    }
}
