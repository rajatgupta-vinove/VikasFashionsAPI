using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.MaterialService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly ILogger<MaterialController> _logger;
        private readonly IMaterialService _materialService;

        public MaterialController(ILogger<MaterialController> logger, IMaterialService materialService)
        {
            _logger = logger;
            _materialService = materialService;
        }

        [HttpGet(Name = "GetMaterial")]
        public async Task<ActionResult<List<Material>>> Get()
        {
            return Ok(await _materialService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetMaterialById")]
        public async Task<ActionResult<Material>> Get(int id)
        {
            return Ok(await _materialService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteMaterialById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _materialService.DeleteMaterialAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateMaterial")]
        public async Task<ActionResult<Material>> Update(int id, Material material)
        {
            return Ok(await _materialService.UpdateMaterialAsync(material));
        }
        [HttpPost(Name = "CreatMaterial")]
        public async Task<ActionResult<Material>> Create(Material material)
        {
            return Ok(await _materialService.AddMaterialAsync(material));
        }
    }
}
