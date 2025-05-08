using API.Extensions;
using Application.Interfaces;
using Application.Models;
using FinanceWebApp.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    public class AuthController(IAuthenticationService authService) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<IResult> Register([FromBody, Required] RegisterRequest registerRequest)
        {
            var response = await authService.RegisterAsync(registerRequest);

            return response.ToHttpResponse();
        }

        [HttpPost("login")]
        public async Task<IResult> Login([FromBody, Required] LoginRequest loginRequest)
        {
            var response = await authService.LoginAsync(loginRequest);

            return response.ToHttpResponse();
        }


    }
}