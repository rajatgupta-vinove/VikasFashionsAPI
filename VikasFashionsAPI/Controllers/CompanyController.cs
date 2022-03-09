using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.CompanyService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;
        private readonly IUserService _userService;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService CompanyService, IUserService userService)
        {
            _logger = logger;
            _companyService = CompanyService;
            _userService = userService;
        }

        [HttpGet(Name = "GetCompany")]
        public async Task<ActionResult<List<Company>>> Get()
        {
            var result = await _companyService.GetAllCompanyAsync();
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
        }
        [HttpGet("{id}", Name = "GetCompanyById")]
        public async Task<ActionResult<Company>> Get(int id)
        {
            var result = await _companyService.GetByCompanyIdAsync(id);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
        }
        [HttpDelete("{id}", Name = "DeleteCompanyById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _companyService.DeleteCompanyAsync(id);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(), Data = result });
        }
        [HttpPut("{id}", Name = "UpdateCompany")]
        public async Task<ActionResult<Company>> Update(int id, Company company)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                company.UpdatedBy = user.UserId;
                company.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _companyService.UpdateCompanyAsync(company);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });
        }
        [HttpPost(Name = "CreateCompany")]
        public async Task<ActionResult<Company>> Create(Company company)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                company.CreatedBy = user.UserId;
                company.CreatedOn = CommonVars.CurrentDateTime;
                company.UpdatedBy = user.UserId;
                company.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _companyService.AddCompanyAsync(company);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(), Data = result });
        }

        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<Company>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var company = await _companyService.GetByCompanyIdAsync(id)
;
            if (company != null)
            {
                if (user != null)
                {
                    company.UpdatedBy = user.UserId;
                    company.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.CompanyNotFound.GetEnumDisplayName() });
            }
            var result = await _companyService.ChangeCompanyStatusAsync(id, company.UpdatedBy, company.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });

        }
    }
}