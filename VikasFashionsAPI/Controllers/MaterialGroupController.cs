
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.MaterialGroupService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialGroupController : ControllerBase
    {
        private readonly ILogger<MaterialGroupController> _logger;
        private readonly IMaterialGroupService _materialGroupService;
        private readonly IUserService _userService;

        public MaterialGroupController(ILogger<MaterialGroupController> logger, IMaterialGroupService materialGroupService,IUserService userService)
        {
            _logger = logger;
            _materialGroupService = materialGroupService;
            _userService = userService;
        }

        [HttpGet(Name = "GetMaterialGroup")]
        public async Task<ActionResult<List<MaterialGroup>>> Get()
        {
            var result = await _materialGroupService.GetAllAsync();
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet("{id}", Name = "GetMaterialGroupById")]
        public async Task<ActionResult<MaterialGroup>> Get(int id)
        {
            var result = await _materialGroupService.GetByIdAsync(id);
            return Ok(new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpDelete("{id}", Name = "DeleteMaterialGroupById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _materialGroupService.DeleteMaterialGroupAsync(id);
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPut("{id}", Name = "UpdateMaterialGroup")]
        public async Task<ActionResult<MaterialGroup>> Update(int id, MaterialGroup materialGroup)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                materialGroup.UpdatedBy = user.UserId;
                materialGroup.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _materialGroupService.UpdateMaterialGroupAsync(materialGroup);
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPost(Name = "CreateMaterialGroup")]
        public async Task<ActionResult<MaterialGroup>> Create(MaterialGroup materialGroup)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                materialGroup.CreatedBy = user.UserId;
                materialGroup.CreatedOn = CommonVars.CurrentDateTime;
                materialGroup.UpdatedBy = user.UserId;
                materialGroup.UpdatedOn = CommonVars.CurrentDateTime;
            }
              var result = await _materialGroupService.AddMaterialGroupAsync(materialGroup);
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(),
                    Data = result
                });
        }

        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<MaterialGroup>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var materialGroup = await _materialGroupService.GetByIdAsync(id)
;
            if (materialGroup != null)
            {
                if (user != null)
                {
                    materialGroup.UpdatedBy = user.UserId;
                    materialGroup.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.RecordNotFound.GetEnumDisplayName() });
            }
            var result = await _materialGroupService.ChangeMaterialGroupStatusAsync(id, materialGroup.UpdatedBy, materialGroup.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });

        }
    }
}
