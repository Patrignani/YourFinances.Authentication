using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourFinances.Authentication.Domain.Core.DTOs;
using YourFinances.Authentication.Domain.Core.Interfaces.Services;

namespace YourFinances.Authentication.Server.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Post([FromBody]UserRegister value)
        {
            return Ok(await _userService.BasicRegisterAsync(value));
        }

    }
}
