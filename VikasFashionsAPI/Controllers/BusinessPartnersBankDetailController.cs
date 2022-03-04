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
            return Ok(await _businessPartnersBankDetailService.GetAllBusinessPartnersBankDetailAsync());
        }
        [HttpGet("{id}", Name = "GetBusinessPartnersBankDetailById")]
        public async Task<ActionResult<BusinessPartnersBankDetail>> Get(int id)
        {
            return Ok(await _businessPartnersBankDetailService.GetByBusinessPartnersBankDetailIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteBusinessPartnersBankDetailById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _businessPartnersBankDetailService.DeleteBusinessPartnersBankDetailAsync(id));
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
            return Ok(await _businessPartnersBankDetailService.UpdateBusinessPartnersBankDetailAsync(businessPartnersBankDetail));
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
            return Ok(await _businessPartnersBankDetailService.AddBusinessPartnersBankDetailAsync(businessPartnersBankDetail));
        }
    }
}