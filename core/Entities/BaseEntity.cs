using System.ComponentModel.DataAnnotations;

namespace core.Entities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
