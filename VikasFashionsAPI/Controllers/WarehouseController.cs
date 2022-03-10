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
            var result = await _warehouseService.GetAllWarehouseAsync();
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet]
        [Route("CheckWarehouseExists")]
        public async Task<ActionResult<bool>> CheckWarehouseExists([FromQuery] int id,[FromQuery] string code )
        {
            _logger.LogInformation($"Check warehouse existance called with id: {id}, code: {code}");
            var result = await _warehouseService.CheckWarehouseStatusAsync(id, code);
            return Ok(
                new ResponseGlobal() { 
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result 
                });
        }



        [HttpGet("{id}", Name = "GetWarehouseById")]
        public async Task<ActionResult<Warehouse>> Get(int id)
        {
            var result = await _warehouseService.GetByWarehouseIdAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpDelete("{id}", Name = "DeleteWarehouseById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _warehouseService.DeleteWarehouseAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
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
            var result = await _warehouseService.UpdateWarehouseAsync(Warehouse);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
        }

        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<Warehouse>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var warehouse = await _warehouseService.GetByWarehouseIdAsync(id);
            if (warehouse != null)
            {
                if (user != null)
                {
                    warehouse.UpdatedBy = user.UserId;
                    warehouse.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.RecordNotFound.GetEnumDisplayName() });
            }
            var result = await _warehouseService.ChangeWarehouseStatusAsync(id, warehouse.UpdatedBy, warehouse.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });

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

            var result = await _warehouseService.AddWarehouseAsync(Warehouse);
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