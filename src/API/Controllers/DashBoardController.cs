using API.Controllers.Request;
using API.Extensions;
using Application.Interfaces;
using Application.Models;
using FinanceWebApp.API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    public class DashBoardController(IDashBoardService dashBoardService) : BaseApiController
    {
        [HttpPost()]
        [Authorize(Roles = $"{FinanceAppRoles.Admin},{FinanceAppRoles.User}")]
        public async Task<IResult> GetMovements([FromBody, Required] MovementUserRequest movementUserRequest)
        {
            var request = new MovementByUserIdRequest(
                movementUserRequest.UserId,
                movementUserRequest.Year,
                movementUserRequest.Month
                );
            var response = await dashBoardService.GetDashBoardDataAsync(request);
            return response.ToHttpResponse();
        }
    }
}
