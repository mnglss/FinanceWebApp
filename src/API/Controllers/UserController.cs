using API.Extensions;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceWebApp.API.Controllers
{
    public class UserController(IUserService userService) : BaseApiController
    {
        //[Authorize]
        [HttpGet()]
        public async Task<IResult> GetAllUsers([FromQuery] string? search, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await userService.GetAllAsync(search, pageNumber, pageSize);
            if (response == null)
                return Results.NotFound();
            return response.ToHttpResponse();
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetUserById(int id)
        {
            var response = await userService.GetByIdAsync(id);
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