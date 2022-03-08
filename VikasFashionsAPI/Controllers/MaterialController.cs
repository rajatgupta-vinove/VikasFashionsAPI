using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.MaterialService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
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
        private readonly IUserService _userService;

        public MaterialController(ILogger<MaterialController> logger, IMaterialService materialService,IUserService userService)
        {
            _logger = logger;
            _materialService = materialService;
            _userService = userService;
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
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                material.UpdatedBy = user.UserId;
                material.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _materialService.UpdateMaterialAsync(material));
        }
        [HttpPost(Name = "CreatMaterial")]
        public async Task<ActionResult<Material>> Create(Material material)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                material.CreatedBy = user.UserId;
                material.CreatedOn = CommonVars.CurrentDateTime;
                material.UpdatedBy = user.UserId;
                material.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _materialService.AddMaterialAsync(material));
        }
        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<Material>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var material = await _materialService.GetByIdAsync(id)
;
            if (material != null)
            {
                if (user != null)
                {
                    material.UpdatedBy = user.UserId;
                    material.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest("No such material found");
            }
            return Ok(await _materialService.ChangeMaterialStatusAsync(id, material.UpdatedBy, material.UpdatedOn));
        }
    }
}
