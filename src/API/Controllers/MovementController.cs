﻿using API.Controllers.Request;
using API.Extensions;
using Application.Interfaces;
using Application.Models;
using FinanceWebApp.API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    public class MovementController(IMovementService movementService) : BaseApiController
    {
        [HttpPost]
        [Authorize(Roles = $"{FinanceAppRoles.Admin},{FinanceAppRoles.User}")]
        public async Task<IResult> CreateMovement([FromBody, Required] MovementRequest movementCreateRequest)
        {
            var response = await movementService.CreateAsync(movementCreateRequest);
            return response.ToHttpResponse();
        }

        [HttpPost("ByUserId")]
        [Authorize(Roles = $"{FinanceAppRoles.Admin},{FinanceAppRoles.User}")]
        public async Task<IResult> GetMovements([FromBody, Required] MovementByUserIdRequest movementUserRequest)
        {
            var response = await movementService.GetByUserIdAsync(movementUserRequest);
            return response!.ToHttpResponse();
        }
    }
}
