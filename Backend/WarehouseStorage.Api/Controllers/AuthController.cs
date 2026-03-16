using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using WarehouseStorage.Domain.Enums;
using WarehouseStorage.Domain.Exceptions;
using WarehouseStorage.Services.Security;

namespace WarehouseStorage.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [Authorize]
        [HttpGet("Ping")]
        public ActionResult Ping()
        {
            return Ok();
        }

        [Authorize(nameof(Policy.RequireAdministratorRole))]
        [HttpGet("Ping-admin")]
        public ActionResult PingAdmin()
        {
            return Ok();
        }

        [Authorize(nameof(Policy.RequireEmployeeRole))]
        [HttpGet("Ping-emp")]
        public ActionResult PingEmp()
        {
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                var result = await _authService.RegisterAsync(request);
                return Ok(result);
            }
            catch (UserAlreadyExistsException e)
            {
                return Conflict(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var result = await _authService.LoginAsync(request);
                return Ok(result);
            }
            catch (InvalidCredentialsException exception)
            {
                return Unauthorized(exception.Message);
            }
        }
    }
}