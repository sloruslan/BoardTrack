namespace Application.DTO.Info
{
    public class HealthCheckResponse
    {
        public string Status { get; set; } = "ok";
        public string? Message { get; set; } = null;
    }
}
