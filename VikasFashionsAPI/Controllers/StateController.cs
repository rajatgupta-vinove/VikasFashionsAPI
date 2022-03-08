using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.Data;
using VikasFashionsAPI.APIServices.StateService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly ILogger<StateController> _logger;
        private readonly IStateService _stateService;
        private readonly IUserService _userService;

        public StateController(ILogger<StateController> logger, IStateService stateService, IUserService userService)
        {
            _logger = logger;
            _stateService = stateService;
            _userService = userService;
        }

        [HttpGet(Name = "GetState")]
        public async Task<ActionResult<List<State>>> Get()
        {
            var result = await _stateService.GetAllAsync();
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet("{id}", Name = "GetStateById")]
        public async Task<ActionResult<State>> Get(int id)
        {
            var result = await _stateService.GetByIdAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet]
        [Route("GetStateByCountryId/{id}")]
        public async Task<ActionResult<State>> GetStateByCountryId(int id)
        {
            string? userName = User?.Identity.Name;
            var name = User.FindFirstValue(ClaimTypes.Name);
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userServiceName = _userService.GetLoginUser();
            var result = await _stateService.GetAllByCountryAndStatusAsync(id, true);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpDelete("{id}", Name = "DeleteStateById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _stateService.DeleteStateAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPut("{id}", Name = "UpdateState")]
        public async Task<ActionResult<State>> Update(int id, State state)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                state.UpdatedBy = user.UserId;
                state.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _stateService.UpdateStateAsync(state);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPost(Name = "CreateState")]
        public async Task<ActionResult<State>> Create(State state)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                state.CreatedBy = user.UserId;
                state.CreatedOn = CommonVars.CurrentDateTime;
                state.UpdatedBy = user.UserId;
                state.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _stateService.AddStateAsync(state);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(),
                    Data = result
                });
        }

    }
}
