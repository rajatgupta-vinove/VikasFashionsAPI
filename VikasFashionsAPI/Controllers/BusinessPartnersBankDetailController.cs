using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.BusinessPartnersBankDetailService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessPartnersBankDetailController : ControllerBase
    {
        private readonly ILogger<BusinessPartnersBankDetailController> _logger;
        private readonly IBusinessPartnersBankDetailService _businessPartnersBankDetailService;

        public BusinessPartnersBankDetailController(ILogger<BusinessPartnersBankDetailController> logger, IBusinessPartnersBankDetailService businessPartnersBankDetailService)
        {
            _logger = logger;
            _businessPartnersBankDetailService = businessPartnersBankDetailService;
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
            return Ok(await _businessPartnersBankDetailService.UpdateBusinessPartnersBankDetailAsync(businessPartnersBankDetail));
        }
        [HttpPost(Name = "CreateBusinessPartnersBankDetail")]
        public async Task<ActionResult<BusinessPartnersBankDetail>> Create(BusinessPartnersBankDetail businessPartnersBankDetail)
        {
            return Ok(await _businessPartnersBankDetailService.AddBusinessPartnersBankDetailAsync(businessPartnersBankDetail));
        }
    }
}