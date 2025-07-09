using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTO.Board
{
    public class MoveBoardRequest
    {
        [JsonPropertyName("board_id")]
        public long BoardId { get; set; }

        [JsonPropertyName("next_step_id")]
        public short NextStepId { get; set; }
    }
}
