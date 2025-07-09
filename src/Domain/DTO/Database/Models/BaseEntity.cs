using LinqToDB.Mapping;

namespace Domain.DTO.Database.Models;

public class BaseEntity
{
    [Column("id"), PrimaryKey, Identity]
    public long Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}