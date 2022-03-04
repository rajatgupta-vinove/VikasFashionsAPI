using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.AreaService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly ILogger<AreaController> _logger;
        private readonly IAreaService _areaService;
        private readonly IUserService _userService;


        public AreaController(ILogger<AreaController> logger, IAreaService areaService , IUserService userService)
        {
            _logger = logger;
            _areaService = areaService;
            _userService = userService;

        }

        [HttpGet(Name = "GetArea")]
        public async Task<ActionResult<List<Area>>> Get()
        {
            return Ok(await _areaService.GetAllAsync());
        }

        [HttpPost(Name = "CreateArea")]
        public async Task<ActionResult<Area>> Create(Area area)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                area.CreatedBy = user.UserId;
                area.CreatedOn = CommonVars.CurrentDateTime;
                area.UpdatedBy = user.UserId;
                area.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _areaService.AddAreaAsync(area));
        }

        [HttpGet("{id}", Name = "GetAreaById")]
        public async Task<ActionResult<Area>> Get(int id)
        {
            return Ok(await _areaService.GetByIdAsync(id));
        }

        [HttpDelete("{id}", Name = "DeleteAreaById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _areaService.DeleteAreaAsync(id));
        }

        [HttpPut("{id}", Name = "UpdateArea")]
        public async Task<ActionResult<Area>> Update(int id, Area area)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                area.UpdatedBy = user.UserId;
                area.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _areaService.UpdateAreaAsync(area));
        }
    }
}
