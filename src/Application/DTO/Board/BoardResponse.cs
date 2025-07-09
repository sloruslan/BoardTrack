using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTO.Board
{
    public class BoardResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("update_at")]
        public DateTime UpdateAt { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("serial")]
        public string Serial { get; set; } = default!;

        [JsonPropertyName("type_id")]
        public long TypeId { get; set; }

        [JsonPropertyName("current_step_id")]
        public short CurrentStepId { get; set; }

        [JsonPropertyName("current_step_name")]
        public string? CurrentStepName { get; set; }
    }
}
