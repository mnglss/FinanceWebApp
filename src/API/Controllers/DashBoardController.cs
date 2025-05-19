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
        public async Task<IResult> GetMovements([FromBody, Required] MovementByUserIdRequest movementUserRequest)
        {
            var response = await dashBoardService.GetDashBoardDataAsync(movementUserRequest);
            return response.ToHttpResponse();
        }
    }
}
