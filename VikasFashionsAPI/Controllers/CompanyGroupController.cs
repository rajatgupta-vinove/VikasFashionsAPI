using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.CompanyGroupService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyGroupController : ControllerBase
    {
        private readonly ILogger<CompanyGroupController> _logger;
        private readonly ICompanyGroupService _companyGroupService;

        public CompanyGroupController(ILogger<CompanyGroupController> logger, ICompanyGroupService companyGroupService)
        {
            _logger = logger;
            _companyGroupService = companyGroupService;
        }

        [HttpGet(Name = "GetCompanyGroup")]
        public async Task<ActionResult<List<CompanyGroup>>> Get()
        {
            return Ok(await _companyGroupService.GetAllAsync());
        }

        [HttpPost(Name = "CreateCompanyGroup")]
        public async Task<ActionResult<CompanyGroup>> Create(CompanyGroup companyGroup)
        {
            return Ok(await _companyGroupService.AddCompanyGroupAsync(companyGroup));
        }

        [HttpGet("{id}", Name = "GetCompanyGroupById")]
        public async Task<ActionResult<CompanyGroup>> Get(int id)
        {
            return Ok(await _companyGroupService.GetByIdAsync(id));
        }

        [HttpDelete("{id}", Name = "DeleteCompanyGroupById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _companyGroupService.DeleteCompanyGroupAsync(id));
        }

        [HttpPut("{id}", Name = "UpdateCompanyGroup")]
        public async Task<ActionResult<CompanyGroup>> Update(int id, CompanyGroup companyGroup)
        {
            return Ok(await _companyGroupService.UpdateCompanyGroupAsync(companyGroup));
        }
    }
}
