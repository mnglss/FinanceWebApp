using Application.Interfaces;
using FinanceWebApp.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class HealthController(IHealthService healthService) : BaseApiController
    {
        [HttpGet]
        public async Task<IResult> Health()
        {
            try
            {
                var lastCheck = await healthService.GetLastCheckAsync();
                await healthService.UpdateAsync();
                await healthService.DeleteAsync();
                var updateCheck = await healthService.AddAsync();
                return Results.Ok(new
                {
                    LastCheck = lastCheck,
                    UpdateCheck = updateCheck,
                    Result = "Healthy"
                });
            }
            catch (Exception ex)
            {
                return Results.Ok(new { Result = $"Error: {ex.Message}" });
            }
        }
    }
}
