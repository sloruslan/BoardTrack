﻿using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Database.Models
{
    [Table("PRODUCTION_STEP")]
    public class ProductionStep : BaseEntity
    {
        [Column("id"), PrimaryKey]
        public long Id { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        [Column("name")]
        public string Name { get; set; } = default!;
    }
}
