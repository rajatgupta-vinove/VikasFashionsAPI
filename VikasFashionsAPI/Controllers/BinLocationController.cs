using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.BinLocationService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BinLocationController : ControllerBase
    {
        private readonly ILogger<BinLocationController> _logger;
        private readonly IBinLocationService _binLocationService;
        private readonly IUserService _userService;


        public BinLocationController(ILogger<BinLocationController> logger, IBinLocationService binLocationService , IUserService userService)
        {
            _logger = logger;
            _binLocationService = binLocationService;
        }

        [HttpGet(Name = "GetBinLocation")]
        public async Task<ActionResult<List<BinLocation>>> Get()
        {
            return Ok(await _binLocationService.GetAllBinLocationAsync());
        }

        [HttpGet("{binLocId}", Name = "GetBinLocationById")]
        public async Task<ActionResult<BinLocation>> Get(int binLocId)
        {
            return Ok(await _binLocationService.GetByBinLocationIdAsync(binLocId));
        }

        [HttpDelete("{binLocId}", Name = "DeleteBinLocationById")]
        public async Task<ActionResult<bool>> Delete(int binLocId)
        {
            return Ok(await _binLocationService.DeleteBinLocationAsync(binLocId));
        }

        [HttpPut("{binLocId}", Name = "UpdateBinLocation")]
        public async Task<ActionResult<BinLocation>> Update(int binLocId, BinLocation binLocation )
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                binLocation.UpdatedBy = user.UserId;
                binLocation.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _binLocationService.UpdateBinLocationAsync(binLocation));
        }

        [HttpPost(Name = "CreateBinLocation")]
        public async Task<ActionResult<BinLocation>> Create(BinLocation binLocation)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                binLocation.CreatedBy = user.UserId;
                binLocation.CreatedOn = CommonVars.CurrentDateTime;
                binLocation.UpdatedBy = user.UserId;
                binLocation.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _binLocationService.AddBinLocationAsync(binLocation));
        }
        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<BinLocation>> ChangeStatus(int binLocId)
        {
            var user = _userService.GetLoginUser();
            var binLocation = await _binLocationService.GetByBinLocationIdAsync(binLocId);
            if (binLocation != null)
            {
                if (user != null)
                {
                    binLocation.UpdatedBy = user.UserId;
                    binLocation.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest("No such Bin Location found");
            }
            return Ok(await _binLocationService.ChangeBinlocationStatusAsync(binLocId, binLocation.UpdatedBy, binLocation.UpdatedOn));
        }

    }
}