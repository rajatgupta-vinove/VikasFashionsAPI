using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.CompanyService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService CompanyService)
        {
            _logger = logger;
            _companyService = CompanyService;
        }

        [HttpGet(Name = "GetCompany")]
        public async Task<ActionResult<List<Company>>> Get()
        {
            return Ok(await _companyService.GetAllCompanyAsync());
        }
        [HttpGet("{id}", Name = "GetCompanyById")]
        public async Task<ActionResult<Company>> Get(int id)
        {
            return Ok(await _companyService.GetByCompanyIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteCompanyById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _companyService.DeleteCompanyAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateCompany")]
        public async Task<ActionResult<Company>> Update(int id, Company company)
        {
            return Ok(await _companyService.UpdateCompanyAsync(company));
        }
        [HttpPost(Name = "CreateCompany")]
        public async Task<ActionResult<Company>> Create(Company company)
        {
            return Ok(await _companyService.AddCompanyAsync(company));
        }
    }
}