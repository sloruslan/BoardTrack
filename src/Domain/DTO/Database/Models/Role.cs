using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Database.Models
{
    [Table("ROLE")]
    public class Role
    {
        [Column("id"), PrimaryKey]
        public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = default!;
    }
}
