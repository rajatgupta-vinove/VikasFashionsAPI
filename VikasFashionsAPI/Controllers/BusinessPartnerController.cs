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
            return Ok(await _businessPartnerService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetBusinessPartnerById")]
        public async Task<ActionResult<BusinessPartner>> Get(int id)
        {
            return Ok(await _businessPartnerService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteBusinessPartnerById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _businessPartnerService.DeleteBusinessPartnerAsync(id));
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
            return Ok(await _businessPartnerService.UpdateBusinessPartnerAsync(businessPartner));
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
            return Ok(await _businessPartnerService.AddBusinessPartnerAsync(businessPartner));
        }
    }
}
