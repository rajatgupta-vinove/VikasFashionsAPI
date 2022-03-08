﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikasFashionsAPI.APIServices.DesignService;
using VikasFashionsAPI.APIServices.UserService;
using VikasFashionsAPI.Common;
using VikasFashionsAPI.Data;

namespace VikasFashionsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DesignController : ControllerBase
    {
        private readonly ILogger<DesignController> _logger;
        private readonly IDesignService _designService;
        private readonly IUserService _userService;

        public DesignController(ILogger<DesignController> logger, IDesignService designService , IUserService userService)
        {
            _logger = logger;
            _designService = designService;
            _userService = userService;
        }

        [HttpGet(Name = "GetDesign")]
        public async Task<ActionResult<List<Design>>> Get()
        {
            return Ok(await _designService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetDesignById")]
        public async Task<ActionResult<Design>> Get(int id)
        {
            return Ok(await _designService.GetByIdAsync(id));
        }
        [HttpDelete("{id}", Name = "DeleteDesignById")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return Ok(await _designService.DeleteDesignAsync(id));
        }
        [HttpPut("{id}", Name = "UpdateDesign")]
        public async Task<ActionResult<Design>> Update(int id, Design design)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                design.UpdatedBy = user.UserId;
                design.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _designService.UpdateDesignAsync(design));
        }
        [HttpPost(Name = "CreateDesign")]
        public async Task<ActionResult<Design>> Create(Design design)
        {
            var user = _userService.GetLoginUser();
            if (user != null)
            {
                design.CreatedBy = user.UserId;
                design.CreatedOn = CommonVars.CurrentDateTime;
                design.UpdatedBy = user.UserId;
                design.UpdatedOn = CommonVars.CurrentDateTime;
            }
            return Ok(await _designService.AddDesignAsync(design));
        }
        [HttpPut]
        [Route("ChangeStatus/{id}")]
        public async Task<ActionResult<Design>> ChangeStatus(int id)
        {
            var user = _userService.GetLoginUser();
            var design = await _designService.GetByIdAsync(id)
;
            if (design != null)
            {
                if (user != null)
                {
                    design.UpdatedBy = user.UserId;
                    design.UpdatedOn = CommonVars.CurrentDateTime;
                }
            }
            else
            {
                return BadRequest("No such Design found");
            }
            return Ok(await _designService.ChangeDesignStatusAsync(id, design.UpdatedBy, design.UpdatedOn));
        }
    }
}
