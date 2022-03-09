using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.BusinessPartnerTypeService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessPartnerTypeController : ControllerBase
    {
        private readonly ILogger<BusinessPartnerTypeController> _logger;
        private readonly IBusinessPartnerTypeService _businessPartnerTypeService;
        private readonly IUserService _userService;


        public BusinessPartnerTypeController(ILogger<BusinessPartnerTypeController> logger, IBusinessPartnerTypeService businessPartnerTypeService , IUserService userService)
        {
            _logger = logger;
            _businessPartnerTypeService = businessPartnerTypeService;
            _userService = userService;

        }

        [HttpGet(Name = "GetBusinessPartnerType")]
        public async Task<ActionResult<List<BusinessPartnerType>>> Get()
        {
            var result = await _businessPartnerTypeService.GetAllAsync();
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet("{id}", Name = "GetBusinessPartnerTypeById")]
        public async Task<ActionResult<BusinessPartnerType>> Get(int id)
        {
            var result = await _businessPartnerTypeService.GetByIdAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpDelete("{id}", Name = "DeleteBusinessPartnerTypeById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _businessPartnerTypeService.DeleteBusinessPartnerTypeAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPut("{id}", Name = "UpdateBusinessPartnerType")]
        public async Task<ActionResult<BusinessPartnerType>> Update(int id, BusinessPartnerType businessPartnerType)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                businessPartnerType.UpdatedBy = user.UserId;
                businessPartnerType.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _businessPartnerTypeService.UpdateBusinessPartnerTypeAsync(businessPartnerType);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPost(Name = "CreateBusinessPartnerType")]
        public async Task<ActionResult<BusinessPartnerType>> Create(BusinessPartnerType businessPartnerType)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                businessPartnerType.CreatedBy = user.UserId;
                businessPartnerType.CreatedOn = CommonVars.CurrentDateTime;
                businessPartnerType.UpdatedBy = user.UserId;
                businessPartnerType.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _businessPartnerTypeService.AddBusinessPartnerTypeAsync(businessPartnerType);
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
        public async Task<ActionResult<BusinessPartnerType>> ChangeStatus(int businessPartnerTypeId)
        {
            var user = _userService.GetLoginUser();
            var businessPartnerType = await _businessPartnerTypeService.GetByIdAsync(businessPartnerTypeId);
            if (businessPartnerType != null)
            {
                if (user != null)
                {
                    businessPartnerType.UpdatedBy = user.UserId;
                    businessPartnerType.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.RecordNotFound.GetEnumDisplayName() });
            }
            return Ok(await _businessPartnerTypeService.ChangeBusinessPartnerTypeStatusAsync(businessPartnerTypeId, businessPartnerType.UpdatedBy, businessPartnerType.UpdatedOn));
        }
    }
}
