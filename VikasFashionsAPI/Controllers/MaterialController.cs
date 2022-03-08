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
            var result = await _materialService.GetAllAsync();
            return Ok(new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet("{id}", Name = "GetMaterialById")]
        public async Task<ActionResult<Material>> Get(int id)
        {
            var result = await _materialService.GetByIdAsync(id);
            return Ok(   new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpDelete("{id}", Name = "DeleteMaterialById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _materialService.DeleteMaterialAsync(id);
            return Ok(  new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
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
            var result = await _materialService.UpdateMaterialAsync(material);
            return Ok(   new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
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
            var result = await _materialService.AddMaterialAsync(material);
            return Ok(   new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(),
                    Data = result
                });
        }
    }
}
