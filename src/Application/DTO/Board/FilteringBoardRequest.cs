using Application.DTO.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTO.Board
{
    public class FilteringBoardRequest : PageModel
    {
        [BindProperty(Name = "id")]
        public long? Id { get; set; }

        [BindProperty(Name = "created_at")]
        public DateTime? CreatedAt { get; set; }

        [BindProperty(Name = "updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [BindProperty(Name = "name")]
        public string? Name { get; set; }

        [BindProperty(Name = "serial")]
        public string? Serial { get; set; }

        [BindProperty(Name = "type_id")]
        public long? TypeId { get; set; }

        [BindProperty(Name = "current_step_id")]
        public short? CurrentStepId { get; set; }

        [BindProperty(Name = "sort")]
        public SortModel? Sort { get; set; } = new SortModel();

    }
}
