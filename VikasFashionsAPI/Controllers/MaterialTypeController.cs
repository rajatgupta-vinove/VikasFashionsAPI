
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.MaterialTypeService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialTypeController : ControllerBase
    {
        private readonly ILogger<MaterialTypeController> _logger;
        private readonly IMaterialTypeService _materialTypeService;
        private readonly IUserService _userService;

        public MaterialTypeController(ILogger<MaterialTypeController> logger, IMaterialTypeService materialTypeService, IUserService userService)
        {
            _logger = logger;
            _materialTypeService = materialTypeService;
            _userService = userService;
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
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                materialType.UpdatedBy = user.UserId;
                materialType.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _materialTypeService.UpdateMaterialTypeAsync(materialType));
        }
        [HttpPost(Name = "CreateMaterialType")]
        public async Task<ActionResult<MaterialType>> Create(MaterialType materialType)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                materialType.CreatedBy = user.UserId;
                materialType.CreatedOn = CommonVars.CurrentDateTime;
                materialType.UpdatedBy = user.UserId;
                materialType.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _materialTypeService.AddMaterialTypeAsync(materialType));
        }
        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<MaterialType>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var materialType = await _materialTypeService.GetByIdAsync(id)
;
            if (materialType != null)
            {
                if (user != null)
                {
                    materialType.UpdatedBy = user.UserId;
                    materialType.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest("No such materialType found");
            }
            return Ok(await _materialTypeService.ChangeMaterialTypeStatusAsync(id, materialType.UpdatedBy, materialType.UpdatedOn));
        }
    }
}
