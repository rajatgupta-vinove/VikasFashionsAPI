using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.CompanyGroupService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyGroupController : ControllerBase
    {
        private readonly ILogger<CompanyGroupController> _logger;
        private readonly ICompanyGroupService _companyGroupService;
        private readonly IUserService _userService;


        public CompanyGroupController(ILogger<CompanyGroupController> logger, ICompanyGroupService companyGroupService, IUserService userService)
        {
            _logger = logger;
            _companyGroupService = companyGroupService;
            _userService = userService;

        }

        [HttpGet(Name = "GetCompanyGroup")]
        public async Task<ActionResult<List<CompanyGroup>>> Get()
        {
            return Ok(await _companyGroupService.GetAllAsync());
        }

        [HttpPost(Name = "CreateCompanyGroup")]
        public async Task<ActionResult<CompanyGroup>> Create(CompanyGroup companyGroup)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                companyGroup.CreatedBy = user.UserId;
                companyGroup.CreatedOn = CommonVars.CurrentDateTime;
                companyGroup.UpdatedBy = user.UserId;
                companyGroup.UpdatedOn = CommonVars.CurrentDateTime;
            }
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
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                companyGroup.UpdatedBy = user.UserId;
                companyGroup.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _companyGroupService.UpdateCompanyGroupAsync(companyGroup));
        }
    }
}
