using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.HSNMasterService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HSNController : ControllerBase
    {
        private readonly ILogger<HSNController> _logger;
        private readonly IHSNMasterService _HSNService;
        private readonly IUserService _userService;

        public HSNController(ILogger<HSNController> logger, IHSNMasterService hsnService, IUserService userService)
        {
            _logger = logger;
            _HSNService = hsnService;
            _userService = userService;
        }

        [HttpGet(Name = "GetHSN")]
        public async Task<ActionResult<List<HSN>>> Get()
        {
            return Ok(await _HSNService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetHSNById")]
        public async Task<ActionResult<HSN>> Get(int id)
        {
            return Ok(await _HSNService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteHSNById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            
            return Ok(await _HSNService.DeleteHsnAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateHSN")]
        public async Task<ActionResult<HSN>> Update(int id, HSN hsn)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                hsn.UpdatedBy = user.UserId;
                hsn.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _HSNService.UpdateHsnAsync(hsn));
        }
        [HttpPost(Name = "CreateHSN")]
        public async Task<ActionResult<HSN>> Create(HSN hsn)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                hsn.CreatedBy = user.UserId;
                hsn.CreatedOn = CommonVars.CurrentDateTime;
                hsn.UpdatedBy = user.UserId;
                hsn.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _HSNService.AddHsnAsync(hsn));
        }
    }
}
