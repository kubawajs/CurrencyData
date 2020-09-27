﻿using CurrencyData.Infrastructure.Domain;
using CurrencyData.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyData.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User loginUser)
        {
            var authenticationResponse = await _authenticationService.Authenticate(loginUser);
            if (!authenticationResponse.Result)
            {
                _logger.LogWarning("User not authorized.");
                return new UnauthorizedResult();
            }

            _logger.LogInformation($"User {authenticationResponse.User.Username} successfully logged in.");
            return new OkObjectResult(authenticationResponse);
        }
    }
}