using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.Data;
using VikasFashionsAPI.APIServices.StateService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using VikasFashionsAPI.APIServices.AuthService;

namespace VikasFashionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly ILogger<StateController> _logger;
        private readonly IStateService _stateService;
        private readonly IAuthService _authService;

        public StateController(ILogger<StateController> logger, IStateService stateService, IAuthService authService)
        {
            _logger = logger;
            _stateService = stateService;
            _authService = authService;
        }

        [HttpGet(Name = "GetState")]
        public async Task<ActionResult<List<State>>> Get()
        {
            return Ok(await _stateService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetStateById")]
        public async Task<ActionResult<State>> Get(int id)
        {
            return Ok(await _stateService.GetByIdAsync(id));
        }
        [HttpGet,Authorize(Roles ="Admin")]
        [Route("GetStateByCountryId/{id}")]
        public async Task<ActionResult<State>> GetStateByCountryId(int id)
        {
            string? userName = User?.Identity.Name;
            var name = User.FindFirstValue(ClaimTypes.Name);
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userServiceName = _authService.GetLoginUserName();
            return Ok(await _stateService.GetAllByCountryAndStatusAsync(id, true));
        }
        [HttpDelete("{id}", Name = "DeleteStateById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _stateService.DeleteStateAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateState")]
        public async Task<ActionResult<State>> Update(int id, State state)
        {
            return Ok(await _stateService.UpdateStateAsync(state));
        }
        [HttpPost(Name = "CreateState")]
        public async Task<ActionResult<State>> Create(State state)
        {
            return Ok(await _stateService.AddStateAsync(state));
        }

    }
}
