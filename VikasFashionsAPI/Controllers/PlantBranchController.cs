using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.PlantBranchService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PlantBranchController : ControllerBase
    {
        private readonly ILogger<PlantBranchController> _logger;
        private readonly IPlantBranchService _plantBranchService;
        private readonly IUserService _userService;

        public PlantBranchController(ILogger<PlantBranchController> logger, IPlantBranchService plantBranchService , IUserService userService)
        {
            _logger = logger;
            _plantBranchService = plantBranchService;
            _userService = userService;
        }

        [HttpGet(Name = "GetPlantBranch")]
        public async Task<ActionResult<List<PlantBranch>>> Get()
        {
            var result = await _plantBranchService.GetAllPlantBranchAsync();
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet]
        [Route("CheckPlantbranchExists")]
        public async Task<ActionResult<bool>> CheckPlantBranchExists([FromQuery] int id , [FromQuery] string code)
        {
            _logger.LogInformation($"Check plantBranch existance called with id: {id}, code: {code}");
            var result = await _userService.CheckUserStatusAsync(id, code);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet("{id}", Name = "GetPlantBranchById")]
        public async Task<ActionResult<PlantBranch>> Get(int id)
        {
            var result = await _plantBranchService.GetByPlantBranchIdAsync(id);
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpDelete("{id}", Name = "DeletePlantBranchById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _plantBranchService.DeletePlantBranchAsync(id);
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPut("{id}", Name = "UpdatePlantBranch")]
        public async Task<ActionResult<PlantBranch>> Update(int id, PlantBranch plantBranch)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                plantBranch.UpdatedBy = user.UserId;
                plantBranch.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _plantBranchService.UpdatePlantBranchAsync(plantBranch);
            return Ok(new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<PlantBranch>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var plantBranch = await _plantBranchService.GetByPlantBranchIdAsync(id);
            if (plantBranch != null)
            {
                if (user != null)
                {
                    plantBranch.UpdatedBy = user.UserId;
                    plantBranch.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.PlantBranchNotFound.GetEnumDisplayName() });
            }
            var result = await _plantBranchService.ChangePlantBranchStatusAsync(id, plantBranch.UpdatedBy, plantBranch.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });

        }

        [HttpPost(Name = "CreatePlantBranch")]
        public async Task<ActionResult<PlantBranch>> Create(PlantBranch plantBranch)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                plantBranch.CreatedBy = user.UserId;
                plantBranch.CreatedOn = CommonVars.CurrentDateTime;
                plantBranch.UpdatedBy = user.UserId;
                plantBranch.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _plantBranchService.AddPlantBranchAsync(plantBranch);
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(),
                    Data = result
                });
        }

    }
}