using Application.DTO.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTO.BoardHistory
{
    public class FilteringBoardHistoryRequest : PageModel
    {
        [BindProperty(Name = "id")]
        public long? Id { get; set; }

        [BindProperty(Name = "from_step_id")]
        public short? FromStepId { get; set; }

        [BindProperty(Name = "to_step_id")]
        public short? ToStepId { get; set; }

        [BindProperty(Name = "moved_at")]
        public DateTime? MovedAt { get; set; }

        [BindProperty(Name = "moved_by_user_id")]
        public long? MovedByUserId { get; set; }

        [BindProperty(Name = "comment")]
        public string? Comment { get; set; }

        [BindProperty(Name = "board_id")]
        public long? BoardId { get; set; }

        [BindProperty(Name = "sort")]
        public SortModel? Sort { get; set; } = new SortModel();

    }
}
