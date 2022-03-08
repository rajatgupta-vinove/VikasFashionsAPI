using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.CountryService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly ICountryService _countryService;
        private readonly IUserService _userService;

        public CountryController(ILogger<CountryController> logger, ICountryService countryService, IUserService userService)
        {
            _logger = logger;
            _countryService = countryService;
            _userService = userService;
        }

        [HttpGet(Name = "GetCountry")]
        public async Task<ActionResult<List<Country>>> Get()
        {
            var result = await _countryService.GetAllAsync();
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
        }
        [HttpGet("{id}", Name = "GetCountryById")]
        public async Task<ActionResult<Country>> Get(int id)
        {
            var result = await _countryService.GetByIdAsync(id);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
        }
        [HttpDelete("{id}", Name = "DeleteCountryById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _countryService.DeleteCountryAsync(id);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(), Data = result });
        }
        [HttpPut("{id}", Name = "UpdateCountry")]
        public async Task<ActionResult<Country>> Update(int id, Country country)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                country.UpdatedBy = user.UserId;
                country.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _countryService.UpdateCountryAsync(country);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });
        }
        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<Country>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var country = await _countryService.GetByIdAsync(id);
            if (country != null)
            {
                if (user != null)
                {
                    country.UpdatedBy = user.UserId;
                    country.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {              
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.CountryNotFound.GetEnumDisplayName() });
            }
            var result = await _countryService.ChangeCountryStatusAsync(id, country.UpdatedBy, country.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });
        }
        [HttpPost(Name = "CreateCountry")]
        public async Task<ActionResult<Country>> Create(Country country)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                country.CreatedBy = user.UserId;
                country.CreatedOn = CommonVars.CurrentDateTime;
                country.UpdatedBy = user.UserId;
                country.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _countryService.AddCountryAsync(country);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(), Data = result });
        }
    }
}