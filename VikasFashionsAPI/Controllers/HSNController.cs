using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.HSNMasterService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HSNController : ControllerBase
    {
        private readonly ILogger<HSNController> _logger;
        private readonly IHSNMasterService _HSNService;
        private readonly IUserService _userService;

        public HSNController(ILogger<HSNController> logger, IHSNMasterService hsnService, IUserService userService)
        {
            _logger = logger;
            _HSNService = hsnService;
            _userService = userService;
        }

        [HttpGet(Name = "GetHSN")]
        public async Task<ActionResult<List<HSN>>> Get()
        {
            var result = await _HSNService.GetAllAsync();
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet("{id}", Name = "GetHSNById")]
        public async Task<ActionResult<HSN>> Get(int id)
        {
            var result = await _HSNService.GetByIdAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpDelete("{id}", Name = "DeleteHSNById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            
            var result = await _HSNService.DeleteHsnAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPut("{id}", Name = "UpdateHSN")]
        public async Task<ActionResult<HSN>> Update(int id, HSN hsn)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                hsn.UpdatedBy = user.UserId;
                hsn.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _HSNService.UpdateHsnAsync(hsn);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPost(Name = "CreateHSN")]
        public async Task<ActionResult<HSN>> Create(HSN hsn)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                hsn.CreatedBy = user.UserId;
                hsn.CreatedOn = CommonVars.CurrentDateTime;
                hsn.UpdatedBy = user.UserId;
                hsn.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _HSNService.AddHsnAsync(hsn);
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
        public async Task<ActionResult<HSN>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var hsn = await _HSNService.GetByIdAsync(id)
;
            if (hsn != null)
            {
                if (user != null)
                {
                    hsn.UpdatedBy = user.UserId;
                    hsn.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.RecordNotFound.GetEnumDisplayName() });
            }
            var result = await _HSNService.ChangeHSNStatusAsync(id, hsn.UpdatedBy, hsn.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });

        }
    }
}
