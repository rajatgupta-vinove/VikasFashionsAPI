using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.BusinessPartnerService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessPartnerController : ControllerBase
    {
        private readonly ILogger<BusinessPartnerController> _logger;
        private readonly IBusinessPartnerService _businessPartnerService;
        private readonly IUserService _userService;


        public BusinessPartnerController(ILogger<BusinessPartnerController> logger, IBusinessPartnerService businessPartnerService, IUserService userService)
        {
            _logger = logger;
            _businessPartnerService = businessPartnerService;
            _userService = userService;

        }

        [HttpGet(Name = "GetBusinessPartner")]
        public async Task<ActionResult<List<BusinessPartner>>> Get()
        {
            var result = await _businessPartnerService.GetAllAsync();
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet("{id}", Name = "GetBusinessPartnerById")]
        public async Task<ActionResult<BusinessPartner>> Get(int id)
        {
            var result = await _businessPartnerService.GetByIdAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpDelete("{id}", Name = "DeleteBusinessPartnerById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _businessPartnerService.DeleteBusinessPartnerAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
        }

        [HttpPut("{id}", Name = "UpdateBusinessPartner")]
        public async Task<ActionResult<BusinessPartner>> Update(int id, BusinessPartner businessPartner)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                businessPartner.UpdatedBy = user.UserId;
                businessPartner.UpdatedOn = CommonVars.CurrentDateTime;
            }

            var result = await _businessPartnerService.UpdateBusinessPartnerAsync(businessPartner);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
        }

        [HttpPost(Name = "CreateBusinessPartner")]
        public async Task<ActionResult<BusinessPartner>> Create(BusinessPartner businessPartner)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                businessPartner.CreatedBy = user.UserId;
                businessPartner.CreatedOn = CommonVars.CurrentDateTime;
                businessPartner.UpdatedBy = user.UserId;
                businessPartner.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _businessPartnerService.AddBusinessPartnerAsync(businessPartner);
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
        public async Task<ActionResult<BusinessPartner>> ChangeStatus(int businessPartnerId)
        {
            var user = _userService.GetLoginUser();
            var businessPartner = await _businessPartnerService.GetByIdAsync(businessPartnerId);
            if (businessPartner != null)
            {
                if (user != null)
                {
                    businessPartner.UpdatedBy = user.UserId;
                    businessPartner.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.RecordNotFound.GetEnumDisplayName() });
            }
            var result = await _businessPartnerService.ChangeBusinessPartnerStatusAsync(businessPartnerId, businessPartner.UpdatedBy, businessPartner.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });

        }
    }
}
