using Application.Interfaces;
using Application.Models;
using FinanceWebApp.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class HelperController(IMovementService movementService) : BaseApiController
    {
        [HttpGet("Categories")]
        public async Task<IResult> GetColors()
        {
            var response = await movementService.GetCategoryColorAsync();
            if (response.IsFailure)
                return Results.Ok("Categorie non presenti!");
            return Results.Ok(response.Value);
        }
    }
}
