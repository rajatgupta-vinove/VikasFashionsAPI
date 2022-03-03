using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.BusinessPartnerTypeService;
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

        public BusinessPartnerTypeController(ILogger<BusinessPartnerTypeController> logger, IBusinessPartnerTypeService businessPartnerTypeService)
        {
            _logger = logger;
            _businessPartnerTypeService = businessPartnerTypeService;
        }

        [HttpGet(Name = "GetBusinessPartnerType")]
        public async Task<ActionResult<List<BusinessPartnerType>>> Get()
        {
            return Ok(await _businessPartnerTypeService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetBusinessPartnerTypeById")]
        public async Task<ActionResult<BusinessPartnerType>> Get(int id)
        {
            return Ok(await _businessPartnerTypeService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteBusinessPartnerTypeById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _businessPartnerTypeService.DeleteBusinessPartnerTypeAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateBusinessPartnerType")]
        public async Task<ActionResult<BusinessPartnerType>> Update(int id, BusinessPartnerType businessPartnerType)
        {
            return Ok(await _businessPartnerTypeService.UpdateBusinessPartnerTypeAsync(businessPartnerType));
        }
        [HttpPost(Name = "CreateBusinessPartnerType")]
        public async Task<ActionResult<BusinessPartnerType>> Create(BusinessPartnerType businessPartnerType)
        {
            return Ok(await _businessPartnerTypeService.AddBusinessPartnerTypeAsync(businessPartnerType));
        }
    }
}
