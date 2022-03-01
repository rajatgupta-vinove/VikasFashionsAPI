using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.PlantBranchService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantBranchController : ControllerBase
    {
        private readonly ILogger<PlantBranchController> _logger;
        private readonly IPlantBranchService _plantBranchService;

        public PlantBranchController(ILogger<PlantBranchController> logger, IPlantBranchService plantBranchService)
        {
            _logger = logger;
            _plantBranchService = plantBranchService;
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
            return Ok(await _plantBranchService.UpdatePlantBranchAsync(plantBranch));
        }
        [HttpPost(Name = "CreatePlantBranch")]
        public async Task<ActionResult<PlantBranch>> Create(PlantBranch plantBranch)
        {
            return Ok(await _plantBranchService.AddPlantBranchAsync(plantBranch));
        }
    }
}