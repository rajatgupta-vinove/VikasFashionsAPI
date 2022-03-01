
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.MaterialTypeService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialTypeController : ControllerBase
    {
        private readonly ILogger<MaterialTypeController> _logger;
        private readonly IMaterialTypeService _materialTypeService;

        public MaterialTypeController(ILogger<MaterialTypeController> logger, IMaterialTypeService materialTypeService)
        {
            _logger = logger;
            _materialTypeService = materialTypeService;
        }

        [HttpGet(Name = "GetMaterialType")]
        public async Task<ActionResult<List<MaterialType>>> Get()
        {
            return Ok(await _materialTypeService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetMaterialTypeById")]
        public async Task<ActionResult<MaterialType>> Get(int id)
        {
            return Ok(await _materialTypeService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteMaterialTypeById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _materialTypeService.DeleteMaterialTypeAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateMaterialType")]
        public async Task<ActionResult<MaterialType>> Update(int id, MaterialType materialType)
        {
            return Ok(await _materialTypeService.UpdateMaterialTypeAsync(materialType));
        }
        [HttpPost(Name = "CreateMaterialType")]
        public async Task<ActionResult<MaterialType>> Create(MaterialType materialType)
        {
            return Ok(await _materialTypeService.AddMaterialTypeAsync(materialType));
        }
    }
}
