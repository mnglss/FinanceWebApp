using Application.Interfaces;
using Coravel.Invocable;

namespace Application.ScheduledJob
{
    public class HealthJob(IHealthService healthService) : IInvocable
    {
        public async Task Invoke()
        {
            await healthService.RunHealthJob();
            return;
        }
    }
}
