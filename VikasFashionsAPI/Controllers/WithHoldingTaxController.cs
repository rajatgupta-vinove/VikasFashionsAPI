using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.APIServices.WithHoldingTaxService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WithHoldingTaxController : ControllerBase
    {
        private readonly ILogger<WithHoldingTaxController> _logger;
        private readonly IWithHoldingTaxService _WithHoldingTaxService;
        private readonly IUserService _userService;

        public WithHoldingTaxController(ILogger<WithHoldingTaxController> logger, IWithHoldingTaxService WithHoldingTaxService , IUserService userService)
        {
            _logger = logger;
            _WithHoldingTaxService = WithHoldingTaxService;
            _userService = userService;
        }

        [HttpGet(Name = "GetWithHoldingTax")]
        public async Task<ActionResult<List<WithHoldingTax>>> Get()
        {
            var result = await _WithHoldingTaxService.GetAllAsync();
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpGet("{id}", Name = "GetWithHoldingTaxById")]
        public async Task<ActionResult<WithHoldingTax>> Get(int id)
        {
            var result = await _WithHoldingTaxService.GetByIdAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessGet.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpDelete("{id}", Name = "DeleteWithHoldingTaxById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _WithHoldingTaxService.DeleteWithHoldingTaxAsync(id);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessDelete.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPut("{id}", Name = "UpdateWithHoldingTax")]
        public async Task<ActionResult<WithHoldingTax>> Update(int id, WithHoldingTax WithHoldingTax)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                WithHoldingTax.UpdatedBy = user.UserId;
                WithHoldingTax.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _WithHoldingTaxService.UpdateWithHoldingTaxAsync(WithHoldingTax);
            return Ok(
                new ResponseGlobal()
                {
                    ResponseCode = ((int)System.Net.HttpStatusCode.OK),
                    Message = Common.CommonVars.MessageResults.SuccessUpdate.GetEnumDisplayName(),
                    Data = result
                });
        }
        [HttpPost(Name = "CreateWithHoldingTax")]
        public async Task<ActionResult<WithHoldingTax>> Create(WithHoldingTax WithHoldingTax)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                WithHoldingTax.CreatedBy = user.UserId;
                WithHoldingTax.CreatedOn = CommonVars.CurrentDateTime;
                WithHoldingTax.UpdatedBy = user.UserId;
                WithHoldingTax.UpdatedOn = CommonVars.CurrentDateTime;
            }
            var result = await _WithHoldingTaxService.AddWithHoldingTaxAsync(WithHoldingTax);
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
