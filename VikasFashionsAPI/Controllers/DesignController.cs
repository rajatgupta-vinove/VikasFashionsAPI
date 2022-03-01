using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.DesignService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignController : ControllerBase
    {
        private readonly ILogger<DesignController> _logger;
        private readonly IDesignService _designService;

        public DesignController(ILogger<DesignController> logger, IDesignService designService)
        {
            _logger = logger;
            _designService = designService;
        }

        [HttpGet(Name = "GetDesign")]
        public async Task<ActionResult<List<Design>>> Get()
        {
            return Ok(await _designService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetDesignById")]
        public async Task<ActionResult<Design>> Get(int id)
        {
            return Ok(await _designService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteDesignById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _designService.DeleteDesignAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateDesign")]
        public async Task<ActionResult<Design>> Update(int id, Design design)
        {
            return Ok(await _designService.UpdateDesignAsync(design));
        }
        [HttpPost(Name = "CreateDesign")]
        public async Task<ActionResult<Design>> Create(Design design)
        {
            return Ok(await _designService.AddDesignAsync(design));
        }
    }
}
