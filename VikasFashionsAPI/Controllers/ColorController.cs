using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.ColorService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly ILogger<ColorController> _logger;
        private readonly IColorService _colorService;

        public ColorController(ILogger<ColorController> logger, IColorService colorService)
        {
            _logger = logger;
            _colorService = colorService;
        }

        [HttpGet(Name = "GetColor")]
        public async Task<ActionResult<List<Color>>> Get()
        {
            return Ok(await _colorService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetColorById")]
        public async Task<ActionResult<Color>> Get(int id)
        {
            return Ok(await _colorService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteColorById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _colorService.DeleteColorAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateColor")]
        public async Task<ActionResult<Color>> Update(int id, Color color)
        {
            return Ok(await _colorService.UpdateColorAsync(color));
        }
        [HttpPost(Name = "CreateColor")]
        public async Task<ActionResult<Color>> Create(Color color)
        {
            return Ok(await _colorService.AddColorAsync(color));
        }
    }
}
