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
            var result = await _BusinessPartnerAddressService.GetAllBusinessPartnerAddressAsync();
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet("{id}", Name = "GetBusinessPartnerAddressById")]
        public async Task<ActionResult<BusinessPartnerAddress>> Get(int id)
        {
            var result = await _BusinessPartnerAddressService.GetByBusinessPartnerAddressIdAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpDelete("{id}", Name = "DeleteBusinessPartnerAddressById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _BusinessPartnerAddressService.DeleteBusinessPartnerAddressAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
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
            var result = await _BusinessPartnerAddressService.UpdateBusinessPartnerAddressAsync(businessPartnerAddress);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
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
            var result = await _BusinessPartnerAddressService.AddBusinessPartnerAddressAsync(businessPartnerAddress);
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
        public async Task<ActionResult<BusinessPartnerAddress>> ChangeStatus(int businessPartnerAddressId)
        {
            var user = _userService.GetLoginUser();
            var businessPartnerAddress = await _BusinessPartnerAddressService.GetByBusinessPartnerAddressIdAsync(businessPartnerAddressId);
            if (businessPartnerAddress != null)
            {
                if (user != null)
                {
                    businessPartnerAddress.UpdatedBy = user.UserId;
                    businessPartnerAddress.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.RecordNotFound.GetEnumDisplayName() });
            }
            var result = await _BusinessPartnerAddressService.ChangeBusinessPartnerAddressStatusAsync(businessPartnerAddressId, businessPartnerAddress.UpdatedBy, businessPartnerAddress.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });

        }


    }
}