using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.UnitsOfMeasureService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsOfMeasureController : ControllerBase
    {
        private readonly ILogger<UnitsOfMeasureController> _logger;
        private readonly IUnitsOfMeasureService _unitsOfMeasureService;

        public UnitsOfMeasureController(ILogger<UnitsOfMeasureController> logger, IUnitsOfMeasureService unitsOfMeasureService)
        {
            _logger = logger;
            _unitsOfMeasureService = unitsOfMeasureService;
        }

        [HttpGet(Name = "GetUnitsOfMeasure")]
        public async Task<ActionResult<List<UnitsOfMeasure>>> Get()
        {
            return Ok(await _unitsOfMeasureService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetUnitsOfMeasureById")]
        public async Task<ActionResult<UnitsOfMeasure>> Get(int id)
        {
            return Ok(await _unitsOfMeasureService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteUnitsOfMeasureById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _unitsOfMeasureService.DeleteUnitsOfMeasureAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateUnitsOfMeasure")]
        public async Task<ActionResult<UnitsOfMeasure>> Update(int id, UnitsOfMeasure unitsOfMeasure)
        {
            return Ok(await _unitsOfMeasureService.UpdateUnitsOfMeasureAsync(unitsOfMeasure));
        }
        [HttpPost(Name = "CreateUnitsOfMeasure")]
        public async Task<ActionResult<UnitsOfMeasure>> Create(UnitsOfMeasure unitsOfMeasure)
        {
            return Ok(await _unitsOfMeasureService.AddUnitsOfMeasureAsync(unitsOfMeasure));
        }
    }
}
