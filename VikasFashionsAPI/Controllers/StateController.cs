using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.Data;
using VikasFashionsAPI.APIServices.StateService;
using Microsoft.EntityFrameworkCore;

namespace VikasFashionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly ILogger<StateController> _logger;
        private readonly IStateService _stateService;

        public StateController(ILogger<StateController> logger, IStateService stateService)
        {
            _logger = logger;
            _stateService = stateService;
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
        [HttpGet("{id}", Name = "GetStateByCountryId")]
        [Route("GetStateByCountryId")]
        public async Task<ActionResult<State>> GetStateByCountryId(int id)
        {
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
