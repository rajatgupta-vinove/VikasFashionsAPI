using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.CompanyFiscalYearService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyFiscalYearController : ControllerBase
    {
        private readonly ILogger<CompanyFiscalYearController> _logger;
        private readonly ICompanyFiscalYearService _companyFiscalYearService;
        private readonly IUserService _userService;

        public CompanyFiscalYearController(ILogger<CompanyFiscalYearController> logger, ICompanyFiscalYearService CompanyFiscalYearService, IUserService userService)
        {
            _logger = logger;
            _companyFiscalYearService = CompanyFiscalYearService;
            _userService = userService;
        }

        [HttpGet(Name = "GetCompanyFiscalYearByUserId")]
        public async Task<ActionResult<List<CompanyFiscalYear>>> GetCompanyFiscalYearByUserId()
        {
            var user = _userService.GetLoginUser();
            if (user == null)
            {
                return BadRequest(
                    new ResponseGlobal()
                    {
                        ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest),
                        Message = Common.CommonVars.MessageResults.AuthenticationFailed.GetEnumDisplayName()
                    });
            }
            List<CompanyFiscalYear> companyFiscalYears = new List<CompanyFiscalYear>();

            companyFiscalYears = await _companyFiscalYearService.GetCompanyFiscalYearByUserIdAsync(user.UserId);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = companyFiscalYears.ToList()
                });

        }



    }
}