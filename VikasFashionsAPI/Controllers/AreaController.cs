using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.AreaService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly ILogger<AreaController> _logger;
        private readonly IAreaService _areaService;

        public AreaController(ILogger<AreaController> logger, IAreaService areaService)
        {
            _logger = logger;
            _areaService = areaService;
        }

        [HttpGet(Name = "GetArea")]
        public async Task<ActionResult<List<Area>>> Get()
        {
            return Ok(await _areaService.GetAllAsync());
        }

        [HttpPost(Name = "CreateArea")]
        public async Task<ActionResult<Area>> Create(Area area)
        {
            return Ok(await _areaService.AddAreaAsync(area));
        }

        [HttpGet("{id}", Name = "GetAreaById")]
        public async Task<ActionResult<Area>> Get(int id)
        {
            return Ok(await _areaService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteAreaById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _areaService.DeleteAreaAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateArea")]
        public async Task<ActionResult<Area>> Update(int id, Area area)
        {
            return Ok(await _areaService.UpdateAreaAsync(area));
        }
    }
}
