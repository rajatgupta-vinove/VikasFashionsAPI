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
            return Ok(await _plantBranchService.GetAllPlantBranchAsync());
        }
        [HttpGet("{id}", Name = "GetPlantBranchById")]
        public async Task<ActionResult<PlantBranch>> Get(int id)
        {
            return Ok(await _plantBranchService.GetByPlantBranchIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeletePlantBranchById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _plantBranchService.DeletePlantBranchAsync(id));
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
            return Ok(await _plantBranchService.UpdatePlantBranchAsync(plantBranch));
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
            return Ok(await _plantBranchService.AddPlantBranchAsync(plantBranch));
        }

    }
}