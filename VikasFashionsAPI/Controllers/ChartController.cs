using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.ChartService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;


namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly ILogger<ChartController> _logger;
        private readonly IChartService _chartService;
        private readonly IUserService _userService;


        public ChartController(ILogger<ChartController> logger, IChartService chartService, IUserService userService)
        {
            _logger = logger;
            _chartService = chartService;
            _userService = userService;

        }

        [HttpGet(Name = "GetChart")]
        public async Task<ActionResult<List<Chart>>> Get()
        {
            return Ok(await _chartService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetChartById")]
        public async Task<ActionResult<Chart>> Get(int id)
        {
            return Ok(await _chartService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteChartById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _chartService.DeleteChartAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateChart")]
        public async Task<ActionResult<Chart>> Update(int id, Chart chart)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                chart.UpdatedBy = user.UserId;
                chart.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _chartService.UpdateChartAsync(chart));
        }
        [HttpPost(Name = "CreateChart")]
        public async Task<ActionResult<Chart>> Create(Chart chart)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                chart.CreatedBy = user.UserId;
                chart.CreatedOn = CommonVars.CurrentDateTime;
                chart.UpdatedBy = user.UserId;
                chart.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _chartService.AddChartAsync(chart));
        }
    }
}
