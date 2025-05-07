using ApplicationLayer = Application.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using FluentValidation;
using Application.Models.Request;

namespace FinanceWebApp.API.Controllers
{
    public class AuthController(IAuthenticationService _authService, IValidator<RegisterRequest> _validator) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] ApplicationLayer.RegisterRequest registerRequest)
        {
            var validationResult = await _validator.ValidateAsync(registerRequest);
            var errors = new List<string>();
            if (!validationResult.IsValid)
            {
                var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation failed",
                    Detail = "One or more validation errors occurred.",
                    Instance = HttpContext.Request.Path
                };
                return Results.Problem(problemDetails);
            }


            var response = await _authService.RegisterAsync(registerRequest);

            return Results.Ok(response);
        }

        [HttpPost("login")]
        public IResult Login([FromBody] ApplicationLayer.LoginRequest loginRequest)
        {
            // Validate the request
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }

            // // Check if the user exists
            // var user = await _userManager.FindByEmailAsync(loginDto.Email);
            // if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            // {
            //     return Unauthorized("Invalid credentials");
            // }

            // // Generate JWT token and return it
            // var token = await _tokenService.CreateToken(user);
            // return Ok(new { token });
            return Results.Ok();
        }
    }
}