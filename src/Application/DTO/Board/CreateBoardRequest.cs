using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTO.Board
{
    public class CreateBoardRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("serial")]
        public string Serial { get; set; } = default!;

        [JsonPropertyName("type_id")]
        public long TypeId { get; set; }
    }
}
