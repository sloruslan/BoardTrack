using Application.DTO.Info;
using Application.Interfaces.API;

namespace Application.Services.API
{
    public class InfoService : IInfoService
    {
        public async Task<HealthCheckResponse> HealthCheckAsync()
        {


            return new HealthCheckResponse() { Status = "OK", Message = $"Service configured" };

        }
    }
}

