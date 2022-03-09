using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.Data;
using VikasFashionsAPI.APIServices.CityService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private readonly ICityService _cityService;
        private readonly IUserService _userService;

        public CityController(ILogger<CityController> logger, ICityService cityService, IUserService userService)
        {
            _logger = logger;
            _cityService = cityService;
            _userService = userService;
        }

        [HttpGet(Name = "GetCity")]
        public async Task<ActionResult<List<City>>> Get()
        {
            var result = await _cityService.GetAllAsync();
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
        }
        [HttpGet("{id}", Name = "GetCityById")]
        public async Task<ActionResult<City>> Get(int id)
        {
            var result = await _cityService.GetByIdAsync(id);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
        }
        [HttpGet]
        [Route("GetCityByStateId/{id}")]
        public async Task<ActionResult<City>> GetCityByStateId(int id)
        {
            var result = await _cityService.GetAllByStateAndStatusAsync(id, true);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
        }
        [HttpDelete("{id}", Name = "DeleteCityById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _cityService.DeleteCityAsync(id);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(), Data = result });
        }
        [HttpPut("{id}", Name = "UpdateCity")]
        public async Task<ActionResult<City>> Update(int id, City city)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                city.UpdatedBy = user.UserId;
                city.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _cityService.UpdateCityAsync(city);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });
        }
        [HttpPost(Name = "CreateCity")]
        public async Task<ActionResult<City>> Create(City city)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                city.CreatedBy = user.UserId;
                city.CreatedOn = CommonVars.CurrentDateTime;
                city.UpdatedBy = user.UserId;
                city.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _cityService.AddCityAsync(city);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(), Data = result });
        }

        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<City>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var city = await _cityService.GetByIdAsync(id)
;
            if (city != null)
            {
                if (user != null)
                {
                    city.UpdatedBy = user.UserId;
                    city.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.RecordNotFound.GetEnumDisplayName() });
            }
            var result = await _cityService.ChangeCityStatusAsync(id, city.UpdatedBy, city.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });

        }

    }
}
