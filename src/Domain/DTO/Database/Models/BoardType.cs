using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Database.Models
{
    [Table("BOARD_TYPE")]
    public class BoardType : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = default!;

        [Column("description")]
        public string Description { get; set; } = default!;
    }
}
