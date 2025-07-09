using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LinqToDB.Sql;

namespace Domain.DTO.Database.Models
{
    [Table("USER")]
    public class User
    {
        [Column("id"), PrimaryKey, Identity]
        public long Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; } = default!;

        [Column("second_name")]
        public string SecondName { get; set; } = default!;

        [Column("patronymic")]
        public string? Patronymic { get; set; }

        [Column("email")]
        public string Email { get; set; } = default!;

        [Column("password")]
        public string Password { get; set; } = default!;

        [Column("role_id")]
        public long RoleId { get; set; }

        [Association(ThisKey = nameof(RoleId), OtherKey = nameof(Role.Id))]
        public Role? Role { get; set; }
    }
}
