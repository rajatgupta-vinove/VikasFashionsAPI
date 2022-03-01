
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.MaterialGroupService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialGroupController : ControllerBase
    {
        private readonly ILogger<MaterialGroupController> _logger;
        private readonly IMaterialGroupService _materialGroupService;

        public MaterialGroupController(ILogger<MaterialGroupController> logger, IMaterialGroupService materialGroupService)
        {
            _logger = logger;
            _materialGroupService = materialGroupService;
        }

        [HttpGet(Name = "GetMaterialGroup")]
        public async Task<ActionResult<List<MaterialGroup>>> Get()
        {
            return Ok(await _materialGroupService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetMaterialGroupById")]
        public async Task<ActionResult<MaterialGroup>> Get(int id)
        {
            return Ok(await _materialGroupService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteMaterialGroupById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _materialGroupService.DeleteMaterialGroupAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateMaterialGroup")]
        public async Task<ActionResult<MaterialGroup>> Update(int id, MaterialGroup materialGroup)
        {
            return Ok(await _materialGroupService.UpdateMaterialGroupAsync(materialGroup));
        }
        [HttpPost(Name = "CreateMaterialGroup")]
        public async Task<ActionResult<MaterialGroup>> Create(MaterialGroup materialGroup)
        {
            return Ok(await _materialGroupService.AddMaterialGroupAsync(materialGroup));
        }
    }
}
