using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.WithHoldingTaxService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithHoldingTaxController : ControllerBase
    {
        private readonly ILogger<WithHoldingTaxController> _logger;
        private readonly IWithHoldingTaxService _WithHoldingTaxService;

        public WithHoldingTaxController(ILogger<WithHoldingTaxController> logger, IWithHoldingTaxService WithHoldingTaxService)
        {
            _logger = logger;
            _WithHoldingTaxService = WithHoldingTaxService;
        }

        [HttpGet(Name = "GetWithHoldingTax")]
        public async Task<ActionResult<List<WithHoldingTax>>> Get()
        {
            return Ok(await _WithHoldingTaxService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetWithHoldingTaxById")]
        public async Task<ActionResult<WithHoldingTax>> Get(int id)
        {
            return Ok(await _WithHoldingTaxService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteWithHoldingTaxById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _WithHoldingTaxService.DeleteWithHoldingTaxAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateWithHoldingTax")]
        public async Task<ActionResult<WithHoldingTax>> Update(int id, WithHoldingTax WithHoldingTax)
        {
            return Ok(await _WithHoldingTaxService.UpdateWithHoldingTaxAsync(WithHoldingTax));
        }
        [HttpPost(Name = "CreateWithHoldingTax")]
        public async Task<ActionResult<WithHoldingTax>> Create(WithHoldingTax WithHoldingTax)
        {
            return Ok(await _WithHoldingTaxService.AddWithHoldingTaxAsync(WithHoldingTax));
        }

    }
}
