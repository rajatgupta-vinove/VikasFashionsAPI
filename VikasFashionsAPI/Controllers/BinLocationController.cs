using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.BinLocationService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BinLocationController : ControllerBase
    {
        private readonly ILogger<BinLocationController> _logger;
        private readonly IBinLocationService _binLocationService;

        public BinLocationController(ILogger<BinLocationController> logger, IBinLocationService binLocationService)
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
            return Ok(await _binLocationService.UpdateBinLocationAsync(binLocation));
        }
        [HttpPost(Name = "CreateBinLocation")]
        public async Task<ActionResult<BinLocation>> Create(BinLocation binLocation)
        {
            return Ok(await _binLocationService.AddBinLocationAsync(binLocation));
        }
    }
}