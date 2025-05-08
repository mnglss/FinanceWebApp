using Application.Common.Results;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWebApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        public IResult ProblemDetails(ValidationResult validationResult)
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

        public IResult ProblemDetails(Result result)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = result.Error?.Message,
                Detail = "An error occurred while processing your request.",
                Instance = HttpContext.Request.Path
            };
            return Results.Problem(problemDetails);
        }
    }
}