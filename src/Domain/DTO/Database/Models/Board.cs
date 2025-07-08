using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Database.Models
{
    [Table("BOARD")]
    public class Board : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = default!;

        [Column("serial")]
        public string Serial { get; set; } = default!;

        [Column("type_id")]
        public long TypeId { get; set; }

        [Association(ThisKey = nameof(TypeId), OtherKey = nameof(BoardType.Id))]
        public BoardType? Type { get; set; } 

        [Column("current_step_id")]
        public short CurrentStepId { get; set; }

        [Association(ThisKey = nameof(CurrentStepId), OtherKey = nameof(ProductionStep.Id))]
        public ProductionStep? Step { get; set; }
    }
}
