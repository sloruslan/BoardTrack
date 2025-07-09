using System.Text.Json.Serialization;

namespace Application.DTO
{
    public class WithCountResponse<T>
    {
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        [JsonPropertyName("payload")]
        public List<T> Payload { get; set; } = new List<T>();
    }
}
