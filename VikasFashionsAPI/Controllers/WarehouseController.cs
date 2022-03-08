using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.APIServices.WarehouseService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly ILogger<WarehouseController> _logger;
        private readonly IWarehouseService _warehouseService;
        private readonly IUserService _userService;

        public WarehouseController(ILogger<WarehouseController> logger, IWarehouseService warehouseService, IUserService userService)
        {
            _logger = logger;
            _warehouseService = warehouseService;
            _userService = userService;
        }

        [HttpGet(Name = "GetWarehouse")]
        public async Task<ActionResult<List<Warehouse>>> Get()
        {
            return Ok(await _warehouseService.GetAllWarehouseAsync());
        }
        [HttpGet("{id}", Name = "GetWarehouseById")]
        public async Task<ActionResult<Warehouse>> Get(int id)
        {
            return Ok(await _warehouseService.GetByWarehouseIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteWarehouseById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _warehouseService.DeleteWarehouseAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateWarehouse")]
        public async Task<ActionResult<Warehouse>> Update(int id, Warehouse Warehouse)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                Warehouse.UpdatedBy = user.UserId;
                Warehouse.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _warehouseService.UpdateWarehouseAsync(Warehouse));
        }
        [HttpPost(Name = "CreateWarehouse")]
        public async Task<ActionResult<Warehouse>> Create(Warehouse Warehouse)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                Warehouse.CreatedBy = user.UserId;
                Warehouse.CreatedOn = CommonVars.CurrentDateTime;
                Warehouse.UpdatedBy = user.UserId;
                Warehouse.UpdatedOn = CommonVars.CurrentDateTime;
            }

            return Ok(await _warehouseService.AddWarehouseAsync(Warehouse));
        }
    }
}