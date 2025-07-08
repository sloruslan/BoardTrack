using Application.DTO.Info;

namespace Application.Interfaces.API
{
    public interface IInfoService
    {
        Task<HealthCheckResponse> HealthCheckAsync();
    }
}
