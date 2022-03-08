
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
            var result = await _materialTypeService.GetAllAsync();
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet("{id}", Name = "GetMaterialTypeById")]
        public async Task<ActionResult<MaterialType>> Get(int id)
        {
            var result = await _materialTypeService.GetByIdAsync(id);
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpDelete("{id}", Name = "DeleteMaterialTypeById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _materialTypeService.DeleteMaterialTypeAsync(id);
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
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
            var result = await _materialTypeService.UpdateMaterialTypeAsync(materialType);
            return Ok(new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                } );
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
            var result = await _materialTypeService.AddMaterialTypeAsync(materialType);
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(),
                    Data = result
                });
        }
    }
}
