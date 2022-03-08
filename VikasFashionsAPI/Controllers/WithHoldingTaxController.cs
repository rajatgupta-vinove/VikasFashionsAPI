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
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                WithHoldingTax.UpdatedBy = user.UserId;
                WithHoldingTax.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _WithHoldingTaxService.UpdateWithHoldingTaxAsync(WithHoldingTax));
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
            return Ok(await _WithHoldingTaxService.AddWithHoldingTaxAsync(WithHoldingTax));
        }

        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<WithHoldingTax>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var WithHoldingTax = await _WithHoldingTaxService.GetByIdAsync(id)
;
            if (WithHoldingTax != null)
            {
                if (user != null)
                {
                    WithHoldingTax.UpdatedBy = user.UserId;
                    WithHoldingTax.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest("No such WithHoldingTax found");
            }
            return Ok(await _WithHoldingTaxService.ChangeWithHoldingTaxStatusAsync(id, WithHoldingTax.UpdatedBy, WithHoldingTax.UpdatedOn));
        }

    }
}
