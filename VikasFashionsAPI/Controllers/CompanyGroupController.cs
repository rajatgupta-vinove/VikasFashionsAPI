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
            var result = await _companyGroupService.GetAllAsync();
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
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
            var result = await _companyGroupService.AddCompanyGroupAsync(companyGroup);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(), Data = result });
        }

        [HttpGet("{id}", Name = "GetCompanyGroupById")]
        public async Task<ActionResult<CompanyGroup>> Get(int id)
        {
            var result = await _companyGroupService.GetByIdAsync(id);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
        }

        [HttpDelete("{id}", Name = "DeleteCompanyGroupById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _companyGroupService.DeleteCompanyGroupAsync(id);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(), Data = result });
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
            var result = await _companyGroupService.UpdateCompanyGroupAsync(companyGroup);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });
        }

        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<CompanyGroup>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var companyGroup = await _companyGroupService.GetByIdAsync(id)
;
            if (companyGroup != null)
            {
                if (user != null)
                {
                    companyGroup.UpdatedBy = user.UserId;
                    companyGroup.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest("No such company group found");
            }
            return Ok(await _companyGroupService.ChangeCompanyGroupStatusAsync(id, companyGroup.UpdatedBy, companyGroup.UpdatedOn));
        }
    }
}
