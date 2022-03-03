using Microsoft.AspNetCore.Http;
using VikasFashionsAPI.APIServices.CatalogMasterService;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly ICatalogMasterService _catalogService;

        public CatalogController(ILogger<CatalogController> logger, ICatalogMasterService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        [HttpGet(Name = "GetCatalog")]
        public async Task<ActionResult<List<Catalog>>> Get()
        {
            return Ok(await _catalogService.GetAllAsync());
        }
        [HttpPost(Name = "CreateCatalog")]
        public async Task<ActionResult<Catalog>> Create(Catalog catalog)
        {
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
            return Ok(await _catalogService.UpdateCatalogAsync(catalog));
        }

    }
}
