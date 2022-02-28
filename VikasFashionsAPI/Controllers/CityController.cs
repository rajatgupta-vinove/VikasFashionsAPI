using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.Data;
using VikasFashionsAPI.APIServices.CityService;
using Microsoft.EntityFrameworkCore;

namespace VikasFashionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private readonly ICityService _cityService;

        public CityController(ILogger<CityController> logger, ICityService cityService)
        {
            _logger = logger;
            _cityService = cityService;
        }

        [HttpGet(Name = "GetCity")]
        public async Task<ActionResult<List<City>>> Get()
        {
            return Ok(await _cityService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetCityById")]
        public async Task<ActionResult<City>> Get(int id)
        {
            return Ok(await _cityService.GetByIdAsync(id));
        }
        [HttpGet("{id}", Name = "GetCityByStateId")]
        [Route("GetCityByStateId")]
        public async Task<ActionResult<City>> GetCityByStateId(int id)
        {
            return Ok(await _cityService.GetAllByStateAndStatusAsync(id, true));
        }
        [HttpDelete("{id}", Name = "DeleteCityById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _cityService.DeleteCityAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateCity")]
        public async Task<ActionResult<City>> Update(int id, City city)
        {
            return Ok(await _cityService.UpdateCityAsync(city));
        }
        [HttpPost(Name = "CreateCity")]
        public async Task<ActionResult<City>> Create(City city)
        {
            return Ok(await _cityService.AddCityAsync(city));
        }

    }
}
