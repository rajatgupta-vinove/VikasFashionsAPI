using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.CountryService;
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

        public CountryController(ILogger<CountryController> logger, ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        [HttpGet(Name = "GetCountry")]
        public async Task<ActionResult<List<Country>>> Get()
        {
            return Ok(await _countryService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetCountryById")]
        public async Task<ActionResult<Country>> Get(int id)
        {
            return Ok(await _countryService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteCountryById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _countryService.DeleteCountryAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateCountry")]
        public async Task<ActionResult<Country>> Update(int id, Country country)
        {
            return Ok(await _countryService.UpdateCountryAsync(country));
        }
        [HttpPost(Name = "CreateCountry")]
        public async Task<ActionResult<Country>> Create(Country country)
        {
            return Ok(await _countryService.AddCountryAsync(country));
        }
    }
}