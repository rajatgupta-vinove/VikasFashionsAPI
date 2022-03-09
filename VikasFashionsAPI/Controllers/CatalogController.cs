using Microsoft.AspNetCore.Http;
using VikasFashionsAPI.APIServices.CatalogMasterService;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.Data;
using Microsoft.AspNetCore.Authorization;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly ICatalogMasterService _catalogService;
        private readonly IUserService _userService;


        public CatalogController(ILogger<CatalogController> logger, ICatalogMasterService catalogService, IUserService userService)
        {
            _logger = logger;
            _catalogService = catalogService;
            _userService = userService;

        }

        [HttpGet(Name = "GetCatalog")]
        public async Task<ActionResult<List<Catalog>>> Get()
        {
            var result = await _catalogService.GetAllAsync();            
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
         }
    
        [HttpPost(Name = "CreateCatalog")]
        public async Task<ActionResult<Catalog>> Create(Catalog catalog)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                catalog.CreatedBy = user.UserId;
                catalog.CreatedOn = CommonVars.CurrentDateTime;
                catalog.UpdatedBy = user.UserId;
                catalog.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _catalogService.AddCatalogAsync(catalog);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(), Data = result });
        }
        [HttpGet("{id}", Name = "GetCatalogById")]
        public async Task<ActionResult<Catalog>> Get(int id)
        {
            var result = await _catalogService.GetByIdAsync(id);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(), Data = result });
        }

        [HttpDelete("{id}", Name = "DeleteCatalogById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _catalogService.DeleteCatalogAsync(id);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(), Data = result });
        }

        [HttpPut("{id}", Name = "UpdateCatalog")]
        public async Task<ActionResult<Country>> Update(int id, Catalog catalog)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                catalog.UpdatedBy = user.UserId;
                catalog.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _catalogService.UpdateCatalogAsync(catalog);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });
        }
        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<Catalog>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var catalog = await _catalogService.GetByIdAsync(id);
            if (catalog != null)
            {
                if (user != null)
                {
                    catalog.UpdatedBy = user.UserId;
                    catalog.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.BadRequest), Message = Common.CommonVars.MessageResults.CatalogNotFound.GetEnumDisplayName() });
            }
            var result = await _catalogService.ChangeCatalogStatusAsync(id, catalog.UpdatedBy, catalog.UpdatedOn);
            return Ok(new ResponseGlobal() { ResponseCode = ((int)System.Net.HttpStatusCode.OK), Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(), Data = result });
        }

    }
}

