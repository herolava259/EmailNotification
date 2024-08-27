using System.ComponentModel.DataAnnotations;

namespace Cart.Core.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }
}
