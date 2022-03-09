using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.BusinessPartnersBankDetailService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessPartnersBankDetailController : ControllerBase
    {
        private readonly ILogger<BusinessPartnersBankDetailController> _logger;
        private readonly IBusinessPartnersBankDetailService _businessPartnersBankDetailService;
        private readonly IUserService _userService;


        public BusinessPartnersBankDetailController(ILogger<BusinessPartnersBankDetailController> logger, IBusinessPartnersBankDetailService businessPartnersBankDetailService, IUserService userService)
        {
            _logger = logger;
            _businessPartnersBankDetailService = businessPartnersBankDetailService;
            _userService = userService;

        }

        [HttpGet(Name = "GetBusinessPartnersBankDetail")]
        public async Task<ActionResult<List<BusinessPartnersBankDetail>>> Get()
        {
            var result = await _businessPartnersBankDetailService.GetAllBusinessPartnersBankDetailAsync();
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet("{id}", Name = "GetBusinessPartnersBankDetailById")]
        public async Task<ActionResult<BusinessPartnersBankDetail>> Get(int id)
        {
            var result = await _businessPartnersBankDetailService.GetByBusinessPartnersBankDetailIdAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpDelete("{id}", Name = "DeleteBusinessPartnersBankDetailById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _businessPartnersBankDetailService.DeleteBusinessPartnersBankDetailAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPut("{id}", Name = "UpdateBusinessPartnersBankDetail")]
        public async Task<ActionResult<BusinessPartnersBankDetail>> Update(int id, BusinessPartnersBankDetail businessPartnersBankDetail)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                businessPartnersBankDetail.UpdatedBy = user.UserId;
                businessPartnersBankDetail.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _businessPartnersBankDetailService.UpdateBusinessPartnersBankDetailAsync(businessPartnersBankDetail);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPost(Name = "CreateBusinessPartnersBankDetail")]
        public async Task<ActionResult<BusinessPartnersBankDetail>> Create(BusinessPartnersBankDetail businessPartnersBankDetail)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                businessPartnersBankDetail.CreatedBy = user.UserId;
                businessPartnersBankDetail.CreatedOn = CommonVars.CurrentDateTime;
                businessPartnersBankDetail.UpdatedBy = user.UserId;
                businessPartnersBankDetail.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _businessPartnersBankDetailService.AddBusinessPartnersBankDetailAsync(businessPartnersBankDetail);
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
        public async Task<ActionResult<BusinessPartnersBankDetail>> ChangeStatus(int BusinessPartnersBankDetailId)
        {
            var user = _userService.GetLoginUser();
            var BusinessPartnersBankDetail = await _businessPartnersBankDetailService.GetByBusinessPartnersBankDetailIdAsync(BusinessPartnersBankDetailId);
            if (BusinessPartnersBankDetail != null)
            {
                if (user != null)
                {
                    BusinessPartnersBankDetail.UpdatedBy = user.UserId;
                    BusinessPartnersBankDetail.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.BusinessPartnerBankDetailsNotFound.GetEnumDisplayName() });
            }
            var result = await _businessPartnersBankDetailService.ChangeBusinessPartnersBankDetailStatusAsync(BusinessPartnersBankDetailId, BusinessPartnersBankDetail.UpdatedBy, BusinessPartnersBankDetail.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });

        }
    }
}