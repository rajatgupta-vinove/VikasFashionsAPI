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
            var result = await _chartService.GetAllAsync();
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
        }
    
        [HttpGet("{id}", Name = "GetChartById")]
        public async Task<ActionResult<Chart>> Get(int id)
        {
            var result = await _chartService.GetByIdAsync(id);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
        }
        [HttpDelete("{id}", Name = "DeleteChartById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _chartService.DeleteChartAsync(id);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(), Data = result });
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
            var result = await _chartService.UpdateChartAsync(chart);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });
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
            var result = await _chartService.AddChartAsync(chart);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(), Data = result });
        }

        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<Chart>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var chart = await _chartService.GetByIdAsync(id)
;
            if (chart != null)
            {
                if (user != null)
                {
                    chart.UpdatedBy = user.UserId;
                    chart.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.ChartNotFound.GetEnumDisplayName() });
            }
            var result = await _chartService.ChangeChartStatusAsync(id, chart.UpdatedBy, chart.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });

        }
    }
}
