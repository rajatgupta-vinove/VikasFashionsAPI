using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.ShadeService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShadeController : ControllerBase
    {
        private readonly ILogger<ShadeController> _logger;
        private readonly IShadeService _shadeService;

        public ShadeController(ILogger<ShadeController> logger, IShadeService shadeService)
        {
            _logger = logger;
            _shadeService = shadeService;
        }

        [HttpGet(Name = "GetShade")]
        public async Task<ActionResult<List<Shade>>> Get()
        {
            return Ok(await _shadeService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetShadeById")]
        public async Task<ActionResult<Shade>> Get(int id)
        {
            return Ok(await _shadeService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteShadeById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _shadeService.DeleteShadeAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateShade")]
        public async Task<ActionResult<Shade>> Update(int id, Shade shade)
        {
            return Ok(await _shadeService.UpdateShadeAsync(shade));
        }
        [HttpPost(Name = "CreateShade")]
        public async Task<ActionResult<Shade>> Create(Shade shade)
        {
            return Ok(await _shadeService.AddShadeAsync(shade));
        }
    }
}
