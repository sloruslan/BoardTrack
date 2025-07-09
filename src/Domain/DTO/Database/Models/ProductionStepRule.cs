using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Database.Models
{
    [Table("PRODUCTION_STEP_RULE")]
    public class ProductionStepRule : BaseEntity
    {
        [Column("current_step_id")]
        public short CurrentStepId { get; set; }

        [Association(ThisKey = nameof(CurrentStepId), OtherKey = nameof(ProductionStep.Id))]
        public ProductionStep? CurrentStep { get; set; }

        [Column("valid_next_step_id")]
        public short ValidNextStepId { get; set; }

        [Association(ThisKey = nameof(CurrentStepId), OtherKey = nameof(ProductionStep.Id))]
        public ProductionStep? ValidNextStep { get; set; }

        [Column("description")]
        public string? Description { get; set; }
    }
}
