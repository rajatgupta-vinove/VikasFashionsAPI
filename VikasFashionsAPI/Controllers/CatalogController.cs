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
            return Ok(await _catalogService.GetAllAsync());
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
            return Ok(await _catalogService.AddCatalogAsync(catalog));
        }
        [HttpGet("{id}", Name = "GetCatalogById")]
        public async Task<ActionResult<Catalog>> Get(int id)
        {
            return Ok(await _catalogService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteCatalogById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _catalogService.DeleteCatalogAsync(id));
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
            return Ok(await _catalogService.UpdateCatalogAsync(catalog));
        }
        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<Catalog>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var catalog = await _catalogService.GetByIdAsync(id)
;
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
                return BadRequest("No such catalog found");
            }
            return Ok(await _catalogService.ChangeCatalogStatusAsync(id, catalog.UpdatedBy, catalog.UpdatedOn));
        }

    }
}
