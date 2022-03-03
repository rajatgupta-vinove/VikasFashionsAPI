using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.PaymentTermService;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTermController : ControllerBase
    {
        private readonly ILogger<PaymentTermController> _logger;
        private readonly IPaymentTermService _paymentTermService;

        public PaymentTermController(ILogger<PaymentTermController> logger, IPaymentTermService paymentTermService)
        {
            _logger = logger;
            _paymentTermService = paymentTermService;
        }

        [HttpGet(Name = "GetPaymentTerm")]
        public async Task<ActionResult<List<PaymentTerm>>> Get()
        {
            return Ok(await _paymentTermService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetPaymentTermById")]
        public async Task<ActionResult<PaymentTerm>> Get(int id)
        {
            return Ok(await _paymentTermService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeletePaymentTermById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _paymentTermService.DeletePaymentTermAsync(id));
        }
        [HttpPut("{id}", Name = "UpdatePaymentTerm")]
        public async Task<ActionResult<PaymentTerm>> Update(int id, PaymentTerm paymentTerm)
        {
            return Ok(await _paymentTermService.UpdatePaymentTermAsync(paymentTerm));
        }
        [HttpPost(Name = "CreatePaymentTerm")]
        public async Task<ActionResult<PaymentTerm>> Create(PaymentTerm paymentTerm)
        {
            return Ok(await _paymentTermService.AddPaymentTermAsync(paymentTerm));
        }

    }
}
