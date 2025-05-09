using API.Extensions;
using Application.Interfaces;
using Application.Models;
using FinanceWebApp.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MovementController(IMovementService movementService) : BaseApiController
    {
        [HttpPost]
        public async Task<IResult> CreateMovement([FromBody] MovementRequest movementCreateRequest)
        {
            var response = await movementService.CreateAsync(movementCreateRequest);
            return response.ToHttpResponse();
        }
    }
}
