using System;
using System.Net;
using System.Globalization;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Empresapi.Controllers
{
    using Authentication;
    using Models;
    using Models.Requests;
    using Validators;
    using Services.Interfaces;

    public class AuthController : BaseController
    {
        private readonly ILogger<AuthController> logger;
        private readonly IUserService userService;
        private readonly IConfiguration config;

        public AuthController(
            ILogger<AuthController> logger,
            IConfiguration config,
            IUserService userService)
        {
            this.logger = logger;
            this.config = config;
            this.userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var validator = new LoginModelValidator();
                if (model == null || !validator.Validate(model))
                    return BadRequest();

                var user = await userService.GetWhere(new { Email = model.Email });
                if (user == null)
                    return NotFound();

                if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                    return Unauthorized();

                if (user.DeactivatedOn != null)
                    return Forbid();

                var tokenManager = new JwtManager(config, user.Email, user.Id.ToString(CultureInfo.InvariantCulture));
                if(user.Staff)
                    tokenManager.Claim(ClaimTypes.Role, "Staff");

                tokenManager.Build();

                user.Email = null;
                user.Password = null;
                return Ok(new
                {
                    Token = tokenManager.Token.Value,
                    TokenValidTo = tokenManager.Token.ValidTo,
                    User = user
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return StatusCode(500);
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var validator = new RegisterModelValidator();
                if (model == null || !validator.Validate(model))
                    return BadRequest();


                var userExists = await userService.ExistsWhere(new { Email = model.Email }) ?? true;
                if (userExists)
                    return Forbid();

                model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
                var response = await userService.Add(new User(model));
                if (response != HttpStatusCode.OK)
                    return StatusCode((int)response);

                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return StatusCode(500);
            }

        }
    }
}