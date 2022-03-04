using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.BusinessPartnerAddressService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessPartnerAddressController : ControllerBase
    {
        private readonly ILogger<BusinessPartnerAddressController> _logger;
        private readonly IBusinessPartnerAddressService _BusinessPartnerAddressService;
        private readonly IUserService _userService;


        public BusinessPartnerAddressController(ILogger<BusinessPartnerAddressController> logger, IBusinessPartnerAddressService BusinessPartnerAddressService , IUserService userService)
        {
            _logger = logger;
            _BusinessPartnerAddressService = BusinessPartnerAddressService;
        }

        [HttpGet(Name = "GetBusinessPartnerAddress")]
        public async Task<ActionResult<List<BusinessPartnerAddress>>> Get()
        {
            return Ok(await _BusinessPartnerAddressService.GetAllBusinessPartnerAddressAsync());
        }
        [HttpGet("{id}", Name = "GetBusinessPartnerAddressById")]
        public async Task<ActionResult<BusinessPartnerAddress>> Get(int id)
        {
            return Ok(await _BusinessPartnerAddressService.GetByBusinessPartnerAddressIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteBusinessPartnerAddressById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _BusinessPartnerAddressService.DeleteBusinessPartnerAddressAsync(id));
        }

        [HttpPut("{id}", Name = "UpdateBusinessPartnerAddress")]
        public async Task<ActionResult<BusinessPartnerAddress>> Update(int id, BusinessPartnerAddress businessPartnerAddress)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                businessPartnerAddress.UpdatedBy = user.UserId;
                businessPartnerAddress.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _BusinessPartnerAddressService.UpdateBusinessPartnerAddressAsync(businessPartnerAddress));
        }

        [HttpPost(Name = "CreateBusinessPartnerAddress")]
        public async Task<ActionResult<BusinessPartnerAddress>> Create(BusinessPartnerAddress businessPartnerAddress)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                businessPartnerAddress.CreatedBy = user.UserId;
                businessPartnerAddress.CreatedOn = CommonVars.CurrentDateTime;
                businessPartnerAddress.UpdatedBy = user.UserId;
                businessPartnerAddress.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _BusinessPartnerAddressService.AddBusinessPartnerAddressAsync(businessPartnerAddress));
        }
    }
}