using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTO.BoardHistory
{
    public class BoardHistoryResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("from_step_id")]
        public short? FromStepId { get; set; }

        [JsonPropertyName("to_step_id")]
        public short ToStepId { get; set; }

        [JsonPropertyName("moved_at")]
        public DateTime MovedAt { get; set; }

        [JsonPropertyName("moved_by_user_id")]
        public long? MovedByUserId { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        [JsonPropertyName("board_id")]
        public long BoardId { get; set; }
    }
}
