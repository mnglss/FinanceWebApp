using FinanceWebApp.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class HealthController : BaseApiController
    {
        [HttpGet]
        public async Task<IResult> Healt()
        {

            return Results.Ok();
        }
    }
}
