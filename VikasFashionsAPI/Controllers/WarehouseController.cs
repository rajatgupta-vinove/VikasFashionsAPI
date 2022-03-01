using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.WarehouseService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly ILogger<WarehouseController> _logger;
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(ILogger<WarehouseController> logger, IWarehouseService warehouseService)
        {
            _logger = logger;
            _warehouseService = warehouseService;
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
            return Ok(await _warehouseService.UpdateWarehouseAsync(Warehouse));
        }
        [HttpPost(Name = "CreateWarehouse")]
        public async Task<ActionResult<Warehouse>> Create(Warehouse Warehouse)
        {
            return Ok(await _warehouseService.AddWarehouseAsync(Warehouse));
        }
    }
}