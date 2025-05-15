using Application.Models;
using FinanceWebApp.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class HelperController : BaseApiController
    {
        [HttpGet("Categories")]
        public IResult GetColors()
        {
            return Results.Ok(Category.Index.Keys.Order());
        }
    }
}
