using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cart.Core.Entities;

public class ListItem: BaseEntity
{
    [Required]
    [MinLength(256)]
    [MaxLength(256)]
    public string ProductId { get; set; }

    [ForeignKey("Cart")]
    public Guid CartId { get; set; }

    public Cart? Cart { get; set; }

    public int Amount { get; set; }
}
