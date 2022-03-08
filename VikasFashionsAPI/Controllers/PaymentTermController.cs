using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.PaymentTermService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
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
        private readonly IUserService _userService;

        public PaymentTermController(ILogger<PaymentTermController> logger, IPaymentTermService paymentTermService, IUserService userService)
        {
            _logger = logger;
            _paymentTermService = paymentTermService;
            _userService = userService;
        }

        [HttpGet(Name = "GetPaymentTerm")]
        public async Task<ActionResult<List<PaymentTerm>>> Get()
        {
            var result = await _paymentTermService.GetAllAsync();
            return Ok(new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                } );
        }
        [HttpGet("{id}", Name = "GetPaymentTermById")]
        public async Task<ActionResult<PaymentTerm>> Get(int id)
        {
            var result = await _paymentTermService.GetByIdAsync(id);
            return Ok(new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpDelete("{id}", Name = "DeletePaymentTermById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _paymentTermService.DeletePaymentTermAsync(id);
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPut("{id}", Name = "UpdatePaymentTerm")]
        public async Task<ActionResult<PaymentTerm>> Update(int id, PaymentTerm paymentTerm)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                paymentTerm.UpdatedBy = user.UserId;
                paymentTerm.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _paymentTermService.UpdatePaymentTermAsync(paymentTerm);
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                } );
        }
        [HttpPost(Name = "CreatePaymentTerm")]
        public async Task<ActionResult<PaymentTerm>> Create(PaymentTerm paymentTerm)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                paymentTerm.CreatedBy = user.UserId;
                paymentTerm.CreatedOn = CommonVars.CurrentDateTime;
                paymentTerm.UpdatedBy = user.UserId;
                paymentTerm.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _paymentTermService.AddPaymentTermAsync(paymentTerm);
            return Ok( new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessSave.GetEnumDisplayName(),
                    Data = result
                });
        }

        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<PaymentTerm>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var paymentTerm = await _paymentTermService.GetByIdAsync(id)
;
            if (paymentTerm != null)
            {
                if (user != null)
                {
                    paymentTerm.UpdatedBy = user.UserId;
                    paymentTerm.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest("No such PaymentTerm found");
            }
            return Ok(await _paymentTermService.ChangePaymentTermStatusAsync(id, paymentTerm.UpdatedBy, paymentTerm.UpdatedOn));
        }

    }
}
