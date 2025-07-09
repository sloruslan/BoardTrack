using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Database.Models
{
    [Table("BOARD_HISTORY")]
    public class BoardHistory
    {
        [Column("id"), PrimaryKey, Identity]
        public long Id { get; set; }

        [Column("from_step_id")]
        public short? FromStepId { get; set; }

        [Column("to_step_id")]
        public short ToStepId { get; set; }

        [Column("moved_at")]
        public DateTime MovedAt { get; set; }

        [Column("moved_by_user_id")]
        public long? MovedByUserId { get; set; }

        [Column("comment")]
        public string? Comment { get; set; }

        [Column("board_id")]
        public long BoardId { get; set; }
    }
}
