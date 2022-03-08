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


        public AreaController(ILogger<AreaController> logger, IAreaService areaService, IUserService userService)
        {
            _logger = logger;
            _areaService = areaService;
            _userService = userService;

        }

        [HttpGet(Name = "GetArea")]
        public async Task<ActionResult<List<Area>>> Get()
        {
            var result = await _areaService.GetAllAsync();
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
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
            var result = await _areaService.AddAreaAsync(area);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(),
                    Data = result
                });
        }

        [HttpGet("{id}", Name = "GetAreaById")]
        public async Task<ActionResult<Area>> Get(int id)
        {
            var result = await _areaService.GetByIdAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }

        [HttpDelete("{id}", Name = "DeleteAreaById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _areaService.DeleteAreaAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
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
            var result = await _areaService.UpdateAreaAsync(area);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<Area>> ChangeStatus(int areaId)
        {
            var user = _userService.GetLoginUser();
            var area = await _areaService.GetByIdAsync(areaId);
            if (area != null)
            {
                if (user != null)
                {
                    area.UpdatedBy = user.UserId;
                    area.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest("No such area found");
            }
            return Ok(await _areaService.ChangeAreaStatusAsync(areaId,area.UpdatedBy,area.UpdatedOn));
        }
    }
}
