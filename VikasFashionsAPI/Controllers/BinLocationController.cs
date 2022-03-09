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
            var result = await _binLocationService.GetAllBinLocationAsync();
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }

        [HttpGet("{binLocId}", Name = "GetBinLocationById")]
        public async Task<ActionResult<BinLocation>> Get(int binLocId)
        {
            var result = await _binLocationService.GetByBinLocationIdAsync(binLocId);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }

        [HttpDelete("{binLocId}", Name = "DeleteBinLocationById")]
        public async Task<ActionResult<bool>> Delete(int binLocId)
        {
            var result = await _binLocationService.DeleteBinLocationAsync(binLocId);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
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
            var result = await _binLocationService.UpdateBinLocationAsync(binLocation);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
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
            var result = await _binLocationService.AddBinLocationAsync(binLocation);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(),
                    Data = result
                });
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
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.RecordNotFound.GetEnumDisplayName() });
            }
            var result = await _binLocationService.ChangeBinlocationStatusAsync(binLocId, binLocation.UpdatedBy, binLocation.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });

        }

    }
}