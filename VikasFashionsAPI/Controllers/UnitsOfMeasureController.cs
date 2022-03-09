using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.UnitsOfMeasureService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsOfMeasureController : ControllerBase
    {
        private readonly ILogger<UnitsOfMeasureController> _logger;
        private readonly IUnitsOfMeasureService _unitsOfMeasureService;
        private readonly IUserService _userService;

        public UnitsOfMeasureController(ILogger<UnitsOfMeasureController> logger, IUnitsOfMeasureService unitsOfMeasureService , IUserService userService)
        {
            _logger = logger;
            _unitsOfMeasureService = unitsOfMeasureService;
            _userService = userService;
        }

        [HttpGet(Name = "GetUnitsOfMeasure")]
        public async Task<ActionResult<List<UnitsOfMeasure>>> Get()
        {
            var result = await _unitsOfMeasureService.GetAllAsync();
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet("{id}", Name = "GetUnitsOfMeasureById")]
        public async Task<ActionResult<UnitsOfMeasure>> Get(int id)
        {
            var result = await _unitsOfMeasureService.GetByIdAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpDelete("{id}", Name = "DeleteUnitsOfMeasureById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _unitsOfMeasureService.DeleteUnitsOfMeasureAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPut("{id}", Name = "UpdateUnitsOfMeasure")]
        public async Task<ActionResult<UnitsOfMeasure>> Update(int id, UnitsOfMeasure unitsOfMeasure)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                unitsOfMeasure.UpdatedBy = user.UserId;
                unitsOfMeasure.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _unitsOfMeasureService.UpdateUnitsOfMeasureAsync(unitsOfMeasure);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPost(Name = "CreateUnitsOfMeasure")]
        public async Task<ActionResult<UnitsOfMeasure>> Create(UnitsOfMeasure unitsOfMeasure)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                unitsOfMeasure.CreatedBy = user.UserId;
                unitsOfMeasure.CreatedOn = CommonVars.CurrentDateTime;
                unitsOfMeasure.UpdatedBy = user.UserId;
                unitsOfMeasure.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _unitsOfMeasureService.AddUnitsOfMeasureAsync(unitsOfMeasure);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(),
                    Data = result
                });
        }

        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<UnitsOfMeasure>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var unitsOfMeasure = await _unitsOfMeasureService.GetByIdAsync(id)
;
            if (unitsOfMeasure != null)
            {
                if (user != null)
                {
                    unitsOfMeasure.UpdatedBy = user.UserId;
                    unitsOfMeasure.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.RecordNotFound.GetEnumDisplayName() });
            }
            var result = await _unitsOfMeasureService.ChangeUnitsOfMeasureStatusAsync(id, unitsOfMeasure.UpdatedBy, unitsOfMeasure.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });

        }
    }
}
