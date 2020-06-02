using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourFinances.Authentication.Domain.Core.DTOs;
using YourFinances.Authentication.Domain.Core.Interfaces.Services;

namespace YourFinances.Authentication.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/User
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]UserRegister value)
        {
            return Ok(await _userService.BasicRegisterAsync(value));
        }

        [HttpPost("RegisterInternal")]
        public async Task<IActionResult> RegisterInternal([FromBody]UserRegister value)
        {
            return Ok(await _userService.InternalRegisterAsync(value));
        }

        [HttpPut("KeepConnected")]
        public async Task<IActionResult> KeepConnectedAsync([FromQuery]bool value)
        {
            return Ok(await _userService.KeepConnectedAsync(value));
        }

        [HttpPut("AccepTerm")]
        public async Task<IActionResult> AccepTermAsync()
        {
            return Ok(await _userService.AccepTermAsync());
        }

    }
}
