using API.Extensions;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceWebApp.API.Controllers
{
    public class UserController(IUserService userService) : BaseApiController
    {
        [HttpGet()]
        [Authorize(Roles = FinanceAppRoles.Admin)]
        public async Task<IResult> GetAllUsers([FromQuery] string? search, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await userService.GetAllAsync(search, pageNumber, pageSize);
            if (response == null)
                return Results.NotFound();
            return response.ToHttpResponse();
        }

        [HttpGet("ById/{id:int}")]
        [Authorize(Roles = FinanceAppRoles.PowerAdmin)]
        public async Task<IResult> GetUserById(int id)
        {
            var response = await userService.GetByIdAsync(id);
            return response.ToHttpResponse();
        }

        [HttpGet("ByEmail/{email}")]
        [Authorize(Roles = FinanceAppRoles.PowerAdmin)]
        //public async Task<IResult> GetUserByEmail([FromQuery, Required] string email)
        public async Task<IResult> GetUserByEmail(string email)
        {
            var response = await userService.GetByEmailAsync(email);
            return response.ToHttpResponse();
        }

        [HttpGet("UserData")]
        [Authorize(Roles = $"{FinanceAppRoles.PowerAdmin},{FinanceAppRoles.Admin},{FinanceAppRoles.User}")]
        public async Task<IResult> GetUserData()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var response = await userService.GetByEmailAsync(email!);
            return response.ToHttpResponse();
        }

        [HttpPut]
        public async Task<IResult> UpdateUser([FromBody] UserUpdateRequest userUpdateRequest)
        {
            var response = await userService.UpdateAsync(userUpdateRequest);
            return response.ToHttpResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteUser(int id)
        {
            var response = await userService.DeleteAsync(id);
            return response.ToHttpResponse();
        }

        [HttpPost("assign-role")]
        public async Task<IResult> AssignRole([FromBody] AssignRoleRequest roleRequest)
        {
            var response = await userService.AssignRoleAsync(roleRequest);
            return response.ToHttpResponse();
        }

        [HttpDelete("remove-role")]
        public async Task<IResult> RemoveRole([FromBody] RemoveRoleRequest roleRequest)
        {
            var response = await userService.RemoveRoleAsync(roleRequest);
            return response.ToHttpResponse();
        }
    }
}